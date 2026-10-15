---
name: laser-winforms-base-ui
description: >-
  Scaffold WinForms laser marking base UI (.NET Framework 4.8): 嘉泰标准主界面模板
  (toolbar icons, left preview+log, right reserved/position/mark panels),
  LogView logging, and JSON persistence via SystemMgr/ProductMgr.
  Use when creating a new laser project base form, 基础界面, 主界面模板,
  日志, 数据保存, TableLayoutPanel 布局, or 嘉泰0907-style UI.
  Does NOT include TCP, MES, or EZCAD marking logic (use ezcad2-test / ezcad3-test).
---

# Laser WinForms Base UI

**默认主界面**：嘉泰0907 标准布局（见 [templates/layout-spec.md](templates/layout-spec.md)）。

后续无特殊需求时，**所有工程均按此模板生成主窗体**，不得回退到旧版「仅左工作区+日志」两栏布局。

## 范围

| 包含 | 不包含 |
|------|--------|
| 嘉泰标准 FormMain 布局（TableLayoutPanel） | TCP / MES 通信 |
| 工具栏图片按钮 + UiIconHelper | EZCAD 打标 API 实现 |
| 左预览+日志 / 右预留+位置+加工 | WorkParam、DataMgr 业务 |
| LogView 日志面板 | 非标准布局（除非用户明确要求） |
| SystemMgr + ProductMgr JSON 持久化 | |

## 技术栈

- .NET Framework 4.8，WinForms，x86（EZCAD2）或 x64（EZCAD3）
- 命名空间：`LaserMark`（Manager / Log），项目名（Form / Program / UI）
- 第三方：`Learol.App.Controls`（LogView）、`Newtonsoft.Json`

## 模板文件（直接复制）

目录：`C:\Users\SZJCZ\.cursor\skills\laser-winforms-base-ui\templates\`

| 文件 | 用途 |
|------|------|
| layout-spec.md | 布局规范（控件树、尺寸、TableLayoutPanel 规则） |
| FormMain.Designer.cs | 主窗体 Designer（替换 `JiaTaiMark` → 项目命名空间） |
| FormMain.cs | 主窗体骨架（替换 `{ProjectNamespace}`） |
| UiIconHelper.cs | 图片按钮加载（替换命名空间） |

参考完整实现：`d:\LaserDevelopment\EZCAD2\2026\嘉泰0907\嘉泰Mark\`

## 界面布局（标准）

```
FormMain  1080×661
├── statusStrip1
├── panelToolbar → tlbToolbar (TableLayoutPanel)
│   └── 图片按钮：打开模板 | WebApi | 系统参数
└── splitContainerMain
    ├── 左 splitContainerLeft
    │   ├── gbPreview（lblTemplate + pbPreview Dock）
    │   └── splitContainerLog → LogView
    └── 右 splitContainerRightOuter
        ├── gbReserved（预留）
        └── splitContainerRightInner
            ├── gbPosition → tlbPosition 十字键 + 偏移 + 复位
            └── gbMarkButtons → tlbMarkButtons 红光/标刻/停止
```

详见 [templates/layout-spec.md](templates/layout-spec.md)。

## 共享资源

**DLL**：`C:\Users\SZJCZ\.cursor\skills\libs`

- Learol.App.Controls.dll、Newtonsoft.Json.dll

**图标**：`C:\Users\SZJCZ\.cursor\skills\libs\icons`

| 文件 | 用途 |
|------|------|
| open_template.png | 打开模板 |
| web_api.png | WebApi |
| system_settings.png | 系统参数 |

项目内 `Assets\icons\` + csproj `CopyToOutputDirectory`；`UiIconHelper.ApplyImageButton` 统一加载。

**EZCAD 运行环境**（整目录复制到输出目录，与 libs 分离）：

| 版本 | 目录 | Skill |
|------|------|-------|
| EZCAD2 | `skills\ezcad2` | ezcad2-test |
| EZCAD3 | `skills\ezcad3` | ezcad3-test |

## 生成前确认

1. **项目名称/命名空间**
2. **窗体名称**（默认 `FormMain`）
3. **Product 路径**（默认 `.\Product\data.ldoc`）
4. **是否有布局特殊需求**（无则严格使用嘉泰模板）

## 目录结构

```
{ProjectName}/
├── FormMain.cs / FormMain.Designer.cs
├── UI/UiIconHelper.cs
├── Assets/icons/*.png
├── Manager/SystemMgr.cs, ProductMgr.cs
├── Log/JLog.cs
└── Program.cs
```

## 生成步骤

### 1. 复制模板

从 `templates\` 复制 `FormMain.Designer.cs`、`FormMain.cs`、`UiIconHelper.cs`，替换命名空间。

从 `libs\icons\` 复制图标到 `Assets\icons\`。

### 2. csproj 引用与图标

```xml
<Reference Include="Learol.App.Controls">
  <HintPath>C:\Users\SZJCZ\.cursor\skills\libs\Learol.App.Controls.dll</HintPath>
</Reference>
<Content Include="Assets\icons\open_template.png">
  <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
</Content>
<!-- web_api.png, system_settings.png 同理 -->
```

### 3. Form_Load 必做

```csharp
InitToolbarIcons();
splitContainerLog.Panel2.Controls.Add(Log.GetLogView());
Log.Init();
pbPreview.Resize += (s, ev) => RefreshPreview();
```

**不要在 Designer 中放置 LogView。**

### 4. 叠加 EZCAD 逻辑

- EZCAD2：配合 [ezcad2-test](../ezcad2-test/SKILL.md) 接入 JczLmc、预览、红光、标刻
- EZCAD3：配合 [ezcad3-test](../ezcad3-test/SKILL.md)

## 注意事项

- 工具栏/位置/加工区 **必须用 TableLayoutPanel**，禁止绝对坐标排布
- 工具栏按钮 **必须** 用图片按钮 + ToolTip
- 仅当用户**明确要求**不同布局时才偏离嘉泰模板
- Manager namespace 固定 `LaserMark`
- 不要修改 `~/.cursor/skills-cursor/` 内置 skill
