---
name: ezcad2-test
description: >-
  Scaffold and test EZCAD2 laser marking SDK integration in C# WinForms (.NET 4.8, x86).
  Covers JczLmc/MarkEzd init, ezd file I/O, preview, marking, IO, fly mark, and demo lookup.
  Use when building EZCAD2 test apps, MarkEzd API, lmc1_*, 打标测试, BJJCZ Demo, or EzCad2 integration.
  For MultiMarkEzd / 多头 / 多卡 / 卡序号, use ezcad2-multihead instead.
---

# EZCAD2 测试 Skill

Demo 源码：`d:\EZCAD2\开发\BJJCZ_Ezcad2\Demo`（C# / C++ 子目录）。

**运行时环境统一从以下目录复制到 exe 输出目录**：

`C:\Users\SZJCZ\.cursor\skills\ezcad2`

（含 `MarkEzd.dll`、`Lmc1.dll`、`EZCAD.CFG`、`plug/`、`FONT/`、`PARAM/` 等。）

**P/Invoke 封装**：拷贝 `C:\Users\SZJCZ\.cursor\skills\ezcad2\JczLmc.cs` 到项目（源自 `申为0903\SWLaser\JczLmc.cs`，UTF-8 BOM，模板 namespace 为 `BJJCZ`，使用时改为项目名）。

## 范围

| 包含 | 不包含 |
|------|--------|
| MarkEzd 初始化 / Close | 完整 MES / TCP 业务 |
| ezd 加载、保存、改字、预览 | EZCAD3（用 ezcad3-test） |
| 标刻 / 红光 / 飞行 / IO | Laser.dll 二次封装 |
| 扩展轴、填充等 Demo 对照 | 多头 MultiMarkEzd（用 ezcad2-multihead）；C++ Demo 直接迁移 |

## SDK 与 Demo 目录

```
Demo/
├── C#/
│   ├── BJJCZ_EZCAD2.0_基础开发/    # 最全 API 示例
│   ├── BJJCZ_EZCAD2.0_多头/          # 简版；完整多头见 ezcad2-multihead
│   ├── BJJCZ_EZCAD2.0_快速飞行/
│   ├── BJJCZ_EZCAD2.0_扩展轴/
│   └── ...（条码、填充、预览、笔参数等）
└── C++/
    ├── BJJCZ_EZCAD2_Base/
    └── BJJCZ_EZCAD2_Fly_Hatch_Split/
```

## 环境与编译

1. **平台**：`x86`（Demo csproj `PlatformTarget=x86`，MarkEzd 为 32 位）
2. **框架**：.NET Framework 4.8，WinForms
3. **封装**：项目内编译 `JczLmc.cs`（DllImport `MarkEzd.dll`，无单独 C# 引用 DLL）
4. **运行时部署**：将 `C:\Users\SZJCZ\.cursor\skills\ezcad2` **完整复制**到 `bin\Debug\`
5. **初始化**：`JczLmc.Initialize(Application.StartupPath, false)` — PathName 为 MarkEzd.dll 所在目录（即 StartupPath）
6. **前置**：关闭 EzCad2.exe

## 标准生命周期

```csharp
// Form_Load 或按钮
int err = JczLmc.Initialize(Application.StartupPath, false);
if (err != 0) { MessageBox.Show(JczLmc.GetErrorText(err)); return; }

// FormClosing
JczLmc.Close();
```

`bTestMode=true` 时为测试模式，控制卡相关函数不可用。

## 常用测试流程

### 加载 / 保存 ezd

```csharp
JczLmc.LoadEzdFile(path);           // 加载图档
JczLmc.SaveEntLibToFile(path);      // 保存
JczLmc.ChangeTextByName(name, text); // 按对象名改字
```

### 预览

```csharp
pictureBox.Image = JczLmc.GetCurPreviewImage(w, h);
// 或 GetPrevBitmapByName2 / GetCurPreviewImageByName
```

### 标刻（BackgroundWorker，参考基础开发 Form1）

```csharp
// 全图标刻
JczLmc.Mark(false);
// 单对象
JczLmc.MarkEntity(entName);
// 红光
JczLmc.RedLightMarkByEnt(entName, contour);
JczLmc.RedMark();
// 停止
JczLmc.StopMark();
```

### IO

```csharp
ushort uIn = 0;
JczLmc.ReadPort(ref uIn);
JczLmc.GetOutPort(ref uOut);
JczLmc.WritePort(uOut);
```

### 激光调试

```csharp
JczLmc.LaserOn(true);
JczLmc.GotoPos(x, y);
JczLmc.GetCurCoor(ref x, ref y);
JczLmc.MarkPoint(x, y, delayMs, pen);
```

## 错误码（JczLmc.GetErrorText）

| 码 | 含义 |
|----|------|
| 0 | 成功 |
| 1 | EzCad2 已在运行 |
| 2 | 找不到 EZCAD.CFG |
| 3 | 打开 LMC 失败 |
| 4 | 没有 LMC 设备 |
| 11 | 未初始化 |

## 新建测试工程

1. 新建 WinForms，**x86**，.NET 4.8
2. 拷贝 `skills\ezcad2\JczLmc.cs`，namespace 改为项目名
3. xcopy `skills\ezcad2\*` → `bin\Debug\`
4. 以 `BJJCZ_EZCAD2.0_基础开发\Form1.cs` 为模板摘取所需 API
5. 关闭 EzCad2 后运行

## 注意事项：分割盒对象命名

使用分割盒分割对象时，分割后的对象名称按顺序排列（如 BOX1、BOX2、BOX3…）。  
**但是**：若某个分割盒区域内没有对象（例如 BOX1 与 BOX3 之间的盒为空），实际对象列表中**不会出现**对应名称（不会有 BOX2），**不会保留空对象占位**。

按名查找、按序号循环标刻时，必须以图档中真实存在的对象名为准，勿假定分割盒序号连续且一一对应。

## 排错

| 现象 | 处理 |
|------|------|
| err=1 | 关闭 EzCad2.exe |
| err=2 | 确认 EZCAD.CFG 在输出目录 |
| err=6 | 确认 plug/markcfg0 存在 |
| DllNotFoundException MarkEzd | 完整复制 skills\ezcad2 |
| BadImageFormat | 项目必须 x86，不能用 x64/AnyCPU |

Demo 对照表见 [reference.md](reference.md)。

## 多头开发

单头用本 skill（`MarkEzd` / `JczLmc`）。多头用 **[ezcad2-multihead](../ezcad2-multihead/SKILL.md)**（`MultiMarkEzd`），参考：

- `d:\EZCAD2\开发\MultiMarkEzd多头库文档.doc`
- `d:\EZCAD2\开发\Demo（可发客户）\C#\BJJCZ_EZCAD2.0_多头-卡序号排序`
- `d:\LaserDevelopment\通用软件\多头多文档优化\常规`

## 与 laser-winforms-base-ui 协作

- base-ui：**嘉泰标准主界面**（左预览+日志 / 右预留+位置+加工，TableLayoutPanel + 图片工具栏）+ LogView + 配置持久化
- ezcad2-test：JczLmc 打标逻辑（`skills\ezcad2` 运行环境）
- ezcad2-multihead：MultiMarkEzd 多头 / 多文档
- EZCAD2 为 x86，EZCAD3 为 x64，**不可混在同一进程**
- 新建 EZCAD2 工程：先按 `laser-winforms-base-ui\templates\` 生成 FormMain，再接入 JczLmc