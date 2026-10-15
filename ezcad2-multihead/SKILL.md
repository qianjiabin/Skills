---
name: ezcad2-multihead
description: >-
  Scaffold and develop EZCAD2 multi-head (MultiMarkEzd) laser marking in C# WinForms (.NET 4.8, x86).
  Covers MultiMarkEzd init, card SN sorting/index mapping, per-card ezd load/preview/mark/IO,
  and multi-head multi-document patterns. Use when MultiMarkEzd, 多头, 多卡, 卡序号排序,
  多头多文档, nCard, GetAllCardSN, or EZCAD2 multi-scanner integration.
---

# EZCAD2 多头开发（MultiMarkEzd）

与单头 `MarkEzd` / `JczLmc` **不同**：多头使用 **`MultiMarkEzd.dll`**，绝大多数 API 带 **卡索引 `nCard`**。

单头脚手架见 [ezcad2-test](../ezcad2-test/SKILL.md)。排错见 [ezcad-troubleshooting](../ezcad-troubleshooting/SKILL.md)。

## 权威参考（必读）

| 用途 | 路径 |
|------|------|
| 多头库 API 文档 | `d:\EZCAD2\开发\MultiMarkEzd多头库文档.doc` |
| 官方 Demo（卡序号排序） | `d:\EZCAD2\开发\Demo（可发客户）\C#\BJJCZ_EZCAD2.0_多头-卡序号排序` |
| 多头多文档工程参考 | `d:\LaserDevelopment\通用软件\多头多文档优化\常规` |

同目录相关：`d:\EZCAD2\开发\关于多头开发库接口中索引参数的解释.doc`（`nCard` / 索引含义）。

开发前先打开文档与 Demo；业务实现对照「常规」工程，勿凭单头 `JczLmc` 接口臆造。

## DLL 与运行时

1. **平台**：x86，.NET Framework 4.8，WinForms
2. **库文件**：源文件常为  
   `d:\EZCAD2\开发\MultiMarkEzd(多头库，客户ID9999，使用时需要连同括号一起删除).dll`  
   **使用时必须改名为 `MultiMarkEzd.dll`**（去掉括号及其中文字）放到 exe 目录
3. **运行时**：仍需 EZCAD2 运行环境（`EZCAD.CFG`、`plug/`、`FONT/`、`PARAM/` 等）。可从 `C:\Users\SZJCZ\.cursor\skills\ezcad2` 复制，并额外放入 `MultiMarkEzd.dll`
4. **初始化路径**：`PathName` 为 DLL 所在目录；Demo 用 `Application.StartupPath + "\\"`（注意末尾 `\`）
5. **前置**：关闭 EzCad2.exe；功能号/客户 ID 须与多头库匹配（工程里常见功能号 30）

## 封装对照

| 来源 | 封装类 | 文件 |
|------|--------|------|
| 官方 Demo | `MultiMarkEzd`（嵌在 Form1） | `...\BJJCZ_EZCAD2.0_多头-卡序号排序\BJJCZ_MultiHead\Form1.cs` |
| 多头多文档 | `LmcMulti` | `...\常规\LaserMark\Laser\LmcLib2.cs` |
| 卡排序 / 业务 | `Laser.GetAllSn` 等 | `...\常规\LaserMark\Laser\Laser.cs` |

`DllImport("MultiMarkEzd", EntryPoint = "lmc1_...")`，**不要**与单头 `MarkEzd` 混在同一套调用里。

## 标准流程

```csharp
// 1. 初始化（第三个参数 handle 多为 0）
LmcErrCode err = MultiMarkEzd.Initialize(Application.StartupPath + "\\", false, 0);

// 2. 取卡 SN → 排序 → 建立「显示序 → 库索引」映射（见下节）
GetAllCardSN(...); BubbleSort(...); 建立 nCardIndex[]

// 3. 按卡操作：一律传库索引 nCardIndex[i]，不是排序后的 SN 值本身
LoadEzdFile(nCard, path);
Mark(nCard, false);          // 非阻塞
while (!IsMarkFinish(nCard)) { ... }
StopMark(nCard);
```

关闭：`Close()`。

## 卡序号排序（关键）

`GetAllCardSN` 返回的数组下标是 **库内部卡索引**；值为卡 SN（-1=无卡，0–7=有效）。

Demo / 常规工程做法：

1. 拷贝一份 `OldCarSN` / `oldCardSn`（原始数组，下标=库索引）
2. 对另一份 `CardSn` **冒泡升序**（按 SN 排序，便于 UI 固定顺序）
3. 对每个排序后的 SN，在 `OldCarSN` 里找原下标 → 得到 `nCardIndex[]`
4. **所有 API 的 `nCard` 用 `nCardIndex[显示序号]`**，不要用 SN 当索引

对照：

- Demo：`Form1.buttonInitial_Click` + 各按钮里的 `nCardIndex[NowSn]`
- 工程：`Laser.GetAllSn()`（`CardSn` / `CardIndex`）

## 常用 API（均带 nCard）

| API | 作用 |
|-----|------|
| `lmc1_Initial` / `Close` | 初始化 / 释放 |
| `lmc1_GetAllCardSN` | 所有卡 SN |
| `lmc1_LoadEzdFile` | 按卡加载 ezd |
| `lmc1_Mark` / `StopMark` / `IsMarkFinish` | 标刻（非阻塞）/ 停止 / 空闲 |
| `lmc1_GetPrevBitmap2` | 预览 |
| `lmc1_SetDevCfg` | 设备参数对话框 |
| `lmc1_ChangeTextByName` / `GetTextByName` | 改字 |
| `lmc1_ReadPort` / `WritePort` / `GetOutPort` | IO |
| `lmc1_SetPenParam` / `GetPenParam` | 笔参 |
| `lmc1_SaveEntLibToFile` | 保存 |
| `lmc1_RedLightMark` 等 | 红光 |

完整列表以 `MultiMarkEzd多头库文档.doc` 为准。

## 工程入口（多头多文档）

`d:\LaserDevelopment\通用软件\多头多文档优化\常规\`

- 解决方案：`LaserMark.sln`
- 主业务：`LaserMark\`（`FormMutiHeadMutiFile`、`Laser\Laser.cs`、`Laser\LmcLib2.cs`、`Mgr\MutiMarkConfig.cs`）
- 精简试验：`MultiHeadTest\`

## 与单头差异速查

| | 单头 MarkEzd | 多头 MultiMarkEzd |
|--|-------------|-------------------|
| DLL | MarkEzd.dll | MultiMarkEzd.dll |
| 封装 | JczLmc | MultiMarkEzd / LmcMulti |
| 卡参数 | 无 | 几乎每 API 有 `nCard` |
| Initialize | `(path, bTestMode)` | `(path, bTestMode, handle)` |
| 标刻 | 常阻塞式用法 | `Mark` 非阻塞，需 `IsMarkFinish` |

## 新建多头测试工程

1. WinForms，**x86**，.NET 4.8
2. 从 Demo `Form1.cs` 抽取 `MultiMarkEzd` 类，或参考 `LmcLib2.cs`
3. 复制 `skills\ezcad2\*` + 改名后的 `MultiMarkEzd.dll` → `bin\Debug\`
4. 实现：Init → GetAllCardSN → 排序映射 → 分卡 Load/Mark
5. 关 EzCad2 后运行

细节与文件索引见 [reference.md](reference.md)。
