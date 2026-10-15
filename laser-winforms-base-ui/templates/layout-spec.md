# 嘉泰标准主界面布局（默认模板）

**参考实现**：`d:\LaserDevelopment\EZCAD2\2026\嘉泰0907\嘉泰Mark\FormMain.*`

后续无特殊需求时，所有 EZCAD2/EZCAD3 WinForms 工程 **必须** 按此布局生成主窗体。

## 窗体尺寸

- `ClientSize = 1080 × 661`
- `StartPosition = CenterScreen`

## 控件树

```
FormMain
├── statusStrip1 (Dock=Bottom)
│   └── tssl_Status  Text="就绪"
├── panelToolbar (Dock=Top, Height=60)
│   └── tlbToolbar (TableLayoutPanel, Dock=Fill)
│       ├── btOpenFile      [col0, 80px]  图片按钮 → 打开模板
│       ├── btWebApi        [col1, 80px]  图片按钮 → WebApi
│       ├── btSystem        [col2, 80px]  图片按钮 → 系统参数
│       └── (col3 Percent)  空白填充
└── splitContainerMain (Dock=Fill, Vertical)
    ├── Panel1 (左, SplitterDistance≈808)
    │   └── splitContainerLeft (Horizontal)
    │       ├── Panel1 → gbPreview「EZD图档预览」
    │       │   ├── lblTemplate (Dock=Top)  模板路径
    │       │   └── pbPreview (Dock=Fill, Black, Zoom)
    │       └── Panel2 → splitContainerLog
    │           └── Panel2 → LogView（代码挂载，勿放 Designer）
    └── Panel2 (右, 宽约268)
        └── splitContainerRightOuter (Horizontal)
            ├── Panel1 → gbReserved「预留区域」
            │   └── lblReserved (Dock=Fill, 居中)
            └── Panel2
                └── splitContainerRightInner (Horizontal)
                    ├── Panel1 → gbPosition「位置调整（加工时生效）」
                    │   ├── tlbPosition (TableLayoutPanel 3×3, Dock=Top)
                    │   │   └── ↑ ← → ↓ 十字方向键
                    │   ├── lblOffset (Dock=Top)  X/Y 偏移显示
                    │   └── btResetOffset (Dock=Top)  复位偏移
                    └── Panel2 → gbMarkButtons「加工控制」
                        └── tlbMarkButtons (TableLayoutPanel 1列3行, Dock=Fill)
                            ├── btRedMark   红光显示
                            ├── btMarkOnce  单次标刻
                            └── btStop      停止加工
```

## TableLayoutPanel 规范

| 面板 | 行列 | 说明 |
|------|------|------|
| tlbToolbar | 1×4 | 前三列 Absolute 80px，末列 Percent 100%；按钮 Dock=Fill |
| tlbPosition | 3×3 | 上(1,0)左(0,1)右(2,1)下(1,2)；Anchor=None 居中 |
| tlbMarkButtons | 3×1 | 三行等高 Percent；按钮 Dock=Fill，Padding(12,4,12,8) |

## 图片工具栏

图标目录：`C:\Users\SZJCZ\.cursor\skills\libs\icons`

| 文件 | 按钮 | ToolTip |
|------|------|---------|
| open_template.png | btOpenFile | 打开模板 |
| web_api.png | btWebApi | WebApi 测试 |
| system_settings.png | btSystem | 系统参数 |

- 项目复制到 `Assets\icons\`，编译 CopyToOutputDirectory
- `Form_Load` 调用 `UiIconHelper.ApplyImageButton`（见 templates/UiIconHelper.cs）
- **禁止** 在代码中手动设置工具栏按钮 Location

## 日志挂载（Form_Load）

```csharp
splitContainerLog.Panel2.Controls.Add(Log.GetLogView());
Log.Init();
```

## 预览刷新

```csharp
// pbPreview Dock=Fill；按 ClientSize 取宽高
int w = pbPreview.ClientSize.Width;
int h = pbPreview.ClientSize.Height;
pbPreview.Resize += (s, ev) => RefreshPreview();
BeginInvoke(new Action(RefreshPreview));
```

## 默认 SplitterDistance 参考

| 容器 | Distance | 说明 |
|------|----------|------|
| splitContainerMain | 808 | 左宽右窄 |
| splitContainerLeft | 411 | 上预览下日志 |
| splitContainerRightOuter | 97 | 上预留 |
| splitContainerRightInner | 339 | 上位置下加工 |

## 可定制项（需用户明确要求才改）

- gbReserved 内容（默认保留空白占位）
- 工具栏额外按钮（追加 tlbToolbar 列）
- 右侧宽度比例
- 加工控制按钮文案/数量

## 不可改项（标准模板）

- 左预览+日志 / 右三段的总体结构
- 工具栏必须用 TableLayoutPanel + 图片按钮
- 位置调整必须用 TableLayoutPanel 十字键
- 加工控制必须用 TableLayoutPanel 纵向三按钮
- LogView 挂在 splitContainerLog.Panel2
