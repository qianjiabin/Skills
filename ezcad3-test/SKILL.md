---
name: ezcad3-test
description: >-
  Scaffold and test EZCAD3 laser marking SDK integration in C# WinForms (.NET 4.8, x64).
  Covers EzdKernel init lifecycle, ez3 file I/O, preview render, marking, list marking,
  IO/axis, and demo lookup. Use when building EZCAD3 test apps, EzcadKernel API calls,
  打标测试, 列表加工, SDK 联调, AppendData demos, 列表1 demos, 替换矢量图, DXF换图,
  E3_SetEntVectorFileInfo, E3_ImportFileToEntMgr_2, ChangeDXFPath, or integrating Ezcad3Kernel.dll.
---

# EZCAD3 测试 Skill

主 SDK / Demo：`d:\EZCAD3\开发\AppendData (1)\AppendData`（Ver:1.2.9.6）。

列表加工专项 Demo：`d:\EZCAD3\开发\列表1\列表`（工程 `EZCAD3list`，逐步拼装列表指令）。

**运行时环境统一从以下目录复制到 exe 输出目录**：

`C:\Users\SZJCZ\.cursor\skills\ezcad3`

（含 `Ezcad3Kernel.dll`、`Ezcad3KernelNet.dll`、`Ezcad3Base.dll`、`EzdKernel.dll`、`PARAM/`、`FONT/` 等完整运行环境。）

## 范围

| 包含 | 不包含 |
|------|--------|
| EzdKernel 初始化 / 释放 | 完整生产 MES / TCP 业务 |
| ez3 打开、改字、保存、预览 | Qt Demo |
| 标刻 / 红光 / 计算时间 | C++ 自行封装 |
| 列表加工（ListReady→指令→下发→End） | 未在 SDK 出现的 API |
| IO、轴、网口、多头 Demo 对照 | |

## SDK 源码目录

```
AppendData/
├── EzcadKernel.cs
├── 接口列表.txt
└── Ezcad3_C#_Demos/Ezcad3_C#_Demos/
    ├── Ezcad3Demo.sln
    ├── EzdKernel/
    └── {1HelloWorld..28*}   # 含 25List、28AdvanceList_*

列表1/列表/
└── EZCAD3list/               # 列表指令逐步拼装教学 Demo
    ├── Form1.cs
    └── EzdKernel.cs          # 旧式静态封装（E3_Initial），新工程优先用 AppendData 的 EzdKernel
```

## 环境与编译

1. **平台**：`x64`（`#define WIN64`）
2. **框架**：.NET Framework 4.8，WinForms
3. **封装层**：引用 `EzdKernel` 工程，或拷贝 `EzcadKernel.cs`；托管 DLL 在 `skills\ezcad3\EzdKernel.dll`
4. **运行时部署**：将 `C:\Users\SZJCZ\.cursor\skills\ezcad3` 下**全部文件与子目录**复制到 `bin\x64\Debug\`（或 PostBuild xcopy）
5. **初始化路径**：`EzdKernel.Initial(Application.StartupPath, InitialMode.USB, null)` — 读输出目录下 `PARAM/Controller.ini`
6. **前置**：关闭 EzCad3.exe；VC++ 2015 运行时；网口卡配置 IP/SN

## 标准生命周期

```csharp
E3_ID m_idEM = E3_ID.INVALID;
E3_ID m_idMarker = E3_ID.INVALID;
E3_ID m_idCurLayer = E3_ID.INVALID;

E3_ERR err = EzdKernel.Initial(Application.StartupPath, InitialMode.USB, null);
if (err == E3_ERR.ERR_CLIENTID) return;  // License
if (err == E3_ERR.ERR_EZCADRUN) return;  // EzCad3 已运行

m_idEM = EzdKernel.E3_CreateEntMgr(0);
m_idMarker = EzdKernel.E3_MarkerGetFirstValidId();

// FormClosing
EzdKernel.E3_CloseMarker(m_idMarker);
EzdKernel.E3_FreeEntMgr(m_idEM);
EzdKernel.E3_Close();
```

## 常用测试流程

### 打开 ez3 + 预览

```csharp
EzdKernel.E3_OpenFileToEntMgr2(path, m_idEM, false, false, false);
EzdKernel.E3_GetLayerId(m_idEM, 0, ref m_idCurLayer);
// E3_DrawEnt2 画到 PictureBox
```

### 改文本

- `E3_ChangeTextByName(layer, name, text)` — Demo `2ChangeText`
- `E3_SaveEntMgrToFile(path, m_idEM, false, false, "", "", "")`

### 替换矢量图（DXF 等）

**首选修改路径，尽量不删除原对象再插入矢量图。**

运行时批量换图（Excel 驱动、循环加工等）应优先在原矢量对象上换文件，避免 `E3_DeleteEnt` + `E3_ImportFileToEntMgr_2` 反复新建实体。

| 场景 | 推荐做法 | 避免 |
|------|----------|------|
| 纯矢量（type 80） | `E3_SetEntVectorFileInfo` 改路径 → 按需 `E3_MoveRotateEnt` / `E3_ScaleEnt` 对齐尺寸位置 | 删对象后 `E3_ImportFileToEntMgr_2` |
| 带填充矢量（type 32） | `E3_DelEntHatchParam_2` 去填充 → `E3_SetEntVectorFileInfo` 改路径 → 对齐 → `E3_HatchEnt2` 重建填充 | 删对象后 `E3_ImportFileToEntMgr_2` |
| 有投影（type 144） | 先 `E3_ProjectEntDelete`，换图/重建填充后 `E3_ProjectEnt_2` 恢复投影 | 忽略投影直接 import |

```csharp
// 推荐：原地换路径（填充型先 DelEntHatchParam_2）
EzdKernel.E3_GetEntVectorFileInfo(entID, ref strFileName, ref bOptimize, ref bAutoConnect, ref bDynFile,
    ref bFixedX, ref bFixedY, ref dSizeX, ref dSizeY, ref bFixedCoor, ref dFixedCoorX, ref dFixedCoorY, ref nFixedPos);
EzdKernel.E3_SetEntVectorFileInfo(m_idEM, entID, newDxfPath, bOptimize, bAutoConnect, bDynFile,
    bFixedX, bFixedY, dSizeX, dSizeY, bFixedCoor, dFixedCoorX, dFixedCoorY, nFixedPos);
// 按原对象范围对齐新图后，填充型再 E3_HatchEnt2
```

**原因**：`E3_ImportFileToEntMgr_2` 每次在 EntMgr 新建完整实体，旧内存未必及时释放；频繁换图会导致内存徒增、UI 卡顿，部分环境 `AccessViolationException` 闪退（Event 1026）。

**例外**：图档中尚无矢量占位对象、需从外部首次导入时，才用 `E3_ImportFileToEntMgr` / `E3_ImportFileToEntMgr_2`。

参考实现：`HW2500609(扩展轴阵列加工)` 工程 `Marker3.ChangeDXFPath`。

### 标刻（BackgroundWorker）

```csharp
EzdKernel.E3_MarkerMarkEnt2(m_idMarker, m_idEM, m_idCurLayer, MarkEntMode.StaticMark, 0, 0);
// 停止: E3_MarkerStop
```

### 列表加工（List）

对照源：`列表1\列表\EZCAD3list`（逐步按钮）、AppendData `25List` / `28AdvancedList_IO`（线程循环）。

**固定顺序**（不可乱）：

1. （可选）`E3_SetInputPortWorkMode(marker, bPulseMode, bLowValid)` — 脉冲/电平、高低有效
2. `E3_MarkerListReady(marker, idEM, nMarkFlag)` — 列表开始
3. 向列表追加指令（可多次、任意组合）：
   - `E3_MarkerWaitForInputToList` — 等输入口
   - `E3_MarkerDelayUsToList` — 延时（单位 µs，例 500000 = 500ms）
   - `E3_MarkerMarkEntToList2` — 标刻图层/对象
   - `E3_MarkerOneCurveToList2` — 标刻曲线（3D 点阵 `double[n,3]`）
   - `E3_MarkerWritePortToList` / `E3_MarkerSetOutputPortWithMaskToList` — 输出口
   - `E3_MarkerChangeMarkCountToList` — 列表内计数 +n
4. `E3_MarkerSentBufToCardAndRunList` — **强制下发并执行当前已写指令**
5. `E3_MarkerListEnd` — 列表结束
6. `E3_MarkerWaitForFinish` — 等待板卡跑完

计数查询：`E3_MarkerGetMarkCount` / `E3_MarkerResetMarkCount`。停止：`E3_MarkerStop`。

多卡：循环 `E3_MarkerGetFirstValidId` 收集最多 8 张，再用 `E3_GetMarkerSN` 区分。

## 新建测试工程

1. 添加 `EzdKernel` 工程或拷贝 `EzcadKernel.cs`（优先 AppendData 新封装）
2. WinForms 工程 **x64**，.NET 4.8
3. **xcopy** `skills\ezcad3\*` → 输出目录
4. 普通标刻从 AppendData 对应 Demo 复制；列表加工优先对照 `列表1\列表` 的指令顺序，循环场景再看 `25List`/`28*`
5. 关闭 EzCad3 主程序后运行

## 注意事项：分割盒对象命名

使用分割盒分割对象时，分割后的对象名称按顺序排列（如 BOX1、BOX2、BOX3…）。  
**但是**：若某个分割盒区域内没有对象（例如 BOX1 与 BOX3 之间的盒为空），实际对象列表中**不会出现**对应名称（不会有 BOX2），**不会保留空对象占位**。

按名查找、按序号循环标刻时，必须以图档中真实存在的对象名为准，勿假定分割盒序号连续且一一对应。

## 注意事项：检测功能号 E3_GetFun

检测功能号时，`E3_GetFun` 接口传参需要传 **2**：

```csharp
int funId = EzdKernel.E3_GetFun(2);  // nParam 必须为 2
```

勿传 `0`/`1` 或其他值，否则读到的不是预期功能号。

## 排错

| 现象 | 处理 |
|------|------|
| ERR_CLIENTID | License |
| ERR_EZCADRUN | 关闭 EzCad3.exe |
| DllNotFoundException | 检查是否完整复制 skills\ezcad3 |
| BadImageFormat | 必须 x64 |

Demo 对照表见 [reference.md](reference.md)。

## 与 laser-winforms-base-ui 协作

- base-ui：**嘉泰标准主界面**（`templates\layout-spec.md`）+ LogView（`skills\libs`）
- ezcad3-test：EzdKernel 逻辑（运行环境来自 `skills\ezcad3`）
- 新建 EZCAD3 工程：先按 `laser-winforms-base-ui\templates\` 生成 FormMain，再接入 EzdKernel