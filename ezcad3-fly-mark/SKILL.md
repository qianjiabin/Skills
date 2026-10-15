---
name: ezcad3-fly-mark
description: >-
  EZCAD3 飞行加工列表逻辑：流水线(飞行距离)复位与振镜补偿复位、FlyWaitForDist 累计距离、
  IO触发/图档完成等待距离、对象坐标计入规则。Use when implementing or debugging 飞行加工,
  FlyMark, FlyWaitForDist, 复位流水线, 振镜补偿复位, ResetFlyAndGalvo, 等待距离零点,
  多头多文档飞行, RunFly, FlyMarkWithSplit.
---

# EZCAD3 飞行加工 Skill

适用：EZCAD3 `ListReadyMode.FlyMark` 列表飞行、C# `EzdKernel`、多头多文档项目（如 `WorkControl.RunFly`）。

## 两个复位动作（必须区分）

| 动作 | 典型 API 组合 | 作用 |
|------|----------------|------|
| **复位流水线** | `FlyEnable(false)` → `FlyResetDistance` → `FlyEnable(true)` | 飞行编码器/流水线**距离零点**回到启动时刻；之后 `FlyWaitForDist` 从**本次流水线启动位置**起算 |
| **复位振镜补偿** | `EnableFlyCorrection(false)` → `ResetDistance(true,true)` → `EnableFlyCorrection(true,false)` | 振镜飞行补偿坐标归零；振镜正下方对应**振镜 0 点** |

项目中封装：

- `ResetFlyAndGalvoToList()` = 复位流水线 + 振镜补偿（列表开启、每轮图档开始前）
- `ResetGalvoCorrectionToList()` = **仅**振镜补偿复位（图层等待距离之后、逐对象加工之前）

**不可混淆**：只做流水线复位不会把振镜补偿零点拉回；图层等待后若不再做振镜补偿复位，后续对象等待会叠在错误的振镜位置上。

## 等待距离与流水线零点的关系

流水线启动后，**等待距离的零点已经随流水线往前走**。所有 `FlyWaitForDistToList` 的值都是**从本次流水线启动位置起的绝对累计距离**，不是对象间的相对增量（除非业务刻意设计成相对）。

### 数值示例（从左往右）

设对象左下角 X 坐标 = **50**（图档坐标）。要让该点到达振镜正下方，流水线累计等待距离应为 **50**。

若还需先等待 **10**（IO 触发等待 / 图层基准等待 `flyIoWaitDist`），再加工该对象：

1. `FlyWaitForDist(10)` — 流水线先走 10
2. **`ResetGalvoCorrectionToList()`** — 振镜补偿复位（**必须**，见下节）
3. `FlyWaitForDist(10 + 50)` — 累计等到 60，对象左下角到振镜下

对象等待通用公式（距离模式）：

```text
FlyWaitForDist( layerBaseWait + objectAnchorX )
```

- **首轮**：`layerBaseWait = flyIoWaitDist`（IO触发等待距离）
- **第 2 轮起**：`layerBaseWait = flyCycleWaitDist`（图档完成等待距离）

`objectAnchorX`：右→左取 `|minX|`，左→右取 `|maxX|`（与飞行方向锚点一致）。

## 为何「等 10」之后必须振镜补偿复位

若 **先等 10、再等 50、中间不复位振镜**：

- 等 10 后振镜补偿已随流水线偏移，振镜正下方对应图档约 **X=10** 处
- 再等 50 时，正下方约在 **X=60**，而不是 X=50

若在 10+50 之后才同时复位流水线和振镜：

- 流水线零点、振镜零点都回到 0
- 要加工 X=50 的对象，需把对象平移到振镜 0 点（`MoveRotateEnt`），或按正确顺序在 **等 10 之后** 先 `ResetGalvoCorrectionToList()`，再 `FlyWaitForDist(10+50)`

**正确列表顺序（单轮图档）**：

```text
[可选] ResetFlyAndGalvoToList + SentBuf     ← 列表开启或第2轮图档前：流水线+振镜一起复位
→ FlyWaitForDist(layerBaseWait)            ← 图层级等待（IO触发 或 图档完成距离）
→ ResetGalvoCorrectionToList()             ← 仅振镜补偿复位
→ 对每个对象:
    FlyWaitForDist(layerBaseWait + anchorX)
    MarkEntToList2（图档原始坐标，不做偏移）
```

## 完整会话流程（IO 电平保持连续加工）

### 列表开启（一次）

```text
ListReady(FlyMark)
→ ResetFlyAndGalvoToList + SentBuf
→ WaitForInput（IO 触发）
```

### 第 1 轮图档

```text
FlyWaitForDist(flyIoWaitDist)
→ ResetGalvoCorrectionToList
→ 逐对象: FlyWaitForDist(flyIoWaitDist + anchorX) → MarkEntToList2 → SentBuf
```

### 第 2 轮及以后（IO 仍保持）

```text
ResetFlyAndGalvoToList + SentBuf    ← 流水线零点重新从本轮起点计
→ FlyWaitForDist(flyCycleWaitDist)
→ ResetGalvoCorrectionToList
→ 逐对象: FlyWaitForDist(flyCycleWaitDist + anchorX) → MarkEntToList2 → SentBuf
```

### IO 松开 / 再次触发

- IO 松开：本工位 `StopCard`（等同停卡），当前列表结束
- 再次触发：重新 `ListReady`，`isFirstCycle=true`，仍用 `flyIoWaitDist`

## 配置参数（MutiMarkConfig）

| 界面列 | 字段 | 用途 |
|--------|------|------|
| IO触发等待(mm) | `flyIoWaitDist[sn]` | 首轮图层等待 + 首轮对象 `layerBaseWait` |
| 图档完成等待(mm) | `flyCycleWaitDist[sn]` | 第 2 轮起图层等待 + 第 2 轮起对象 `layerBaseWait` |

## 参考实现

- 多头项目：`LaserMark\Laser\WorkControl.cs` — `RunFly`, `FlyMarkWithSplit`, `ResetFlyAndGalvoToList`, `ResetGalvoCorrectionToList`
- Demo：`d:\LaserDevelopment\测试程序\飞行测试`、`KMHAFlyMark`
- 卷尺飞行：`Ruler_dn`（分割盒 + `MoveRotateEnt` 临时对齐，与本项目「图档坐标直接加工」策略不同）

## 常见错误

1. **图层等待后跳过振镜补偿复位** → 对象累计等待错位（正下方偏一个 layerBaseWait）
2. **第 2 轮图档前不做流水线复位** → `FlyWaitForDist` 仍从上轮累计零点计，距离全错
3. **对象等待只用 anchorX、不加 layerBaseWait** → 与 KMHAFlyMark `waitCoord + waitDist` 不一致
4. **把 flyCycleWaitDist 放在图档加工完之后等** → 应放在**下一轮加工开始前**（与 `flyIoWaitDist` 对称）

## SDK API 速查

```csharp
// 流水线复位
EzdKernel.E3_MarkerFlyEnableToList(idMarker, false);
EzdKernel.E3_MarkerFlyResetDistanceToList(idMarker);
EzdKernel.E3_MarkerFlyEnableToList(idMarker, true);

// 振镜补偿复位
EzdKernel.E3_MarkerEnableFlyCorrectionToList(idMarker, false, false);
EzdKernel.E3_MarkerResetDistanceToList(idMarker, true, true);
EzdKernel.E3_MarkerEnableFlyCorrectionToList(idMarker, true, false);

// 绝对累计等待（从本次流水线启动零点起）
EzdKernel.E3_MarkerFlyWaitForDistToList(idMarker, distance, false);
```
