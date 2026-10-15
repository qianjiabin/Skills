# EZCAD2 多头参考索引

## 文档

| 文件 | 说明 |
|------|------|
| `d:\EZCAD2\开发\MultiMarkEzd多头库文档.doc` | MultiMarkEzd API 说明（主文档） |
| `d:\EZCAD2\开发\关于多头开发库接口中索引参数的解释.doc` | `nCard` / 索引参数含义 |
| `d:\EZCAD2\开发\MultiMarkEzd(多头库，客户ID9999，使用时需要连同括号一起删除).dll` | 库本体；部署前改名为 `MultiMarkEzd.dll` |

## Demo：卡序号排序

根目录：`d:\EZCAD2\开发\Demo（可发客户）\C#\BJJCZ_EZCAD2.0_多头-卡序号排序`

| 路径 | 说明 |
|------|------|
| `BJJCZ_MultiHead.sln` | 解决方案 |
| `BJJCZ_MultiHead\Form1.cs` | `MultiMarkEzd` P/Invoke + 排序 + UI 按卡操作 |
| `BJJCZ_MultiHead\bin\Debug\` | 运行时样例（含 CFG/plug 等） |

要点代码位置（`Form1.cs`）：

- `MultiMarkEzd` 嵌套类：`Initialize` / `GetAllCardSN` / `LoadEzdFile` / `Mark` / `IsMarkFinish` / `StopMark` / 预览 / IO / 笔参
- `BubbleSort` + `buttonInitial_Click`：SN 排序与 `nCardIndex` 映射
- `FileButton_Click` / `MarkButton_Click`：API 使用 `nCardIndex[NowSn]`

## 工程：多头多文档优化（常规）

根目录：`d:\LaserDevelopment\通用软件\多头多文档优化\常规`

| 路径 | 说明 |
|------|------|
| `LaserMark.sln` | 主解决方案 |
| `LaserMark\Laser\LmcLib2.cs` | `LmcMulti` → MultiMarkEzd |
| `LaserMark\Laser\Laser.cs` | `GetAllSn` / `OpenFile2` / 标刻与功能号检查 |
| `LaserMark\FormMutiHeadMutiFile.cs` | 多头多文档主界面 |
| `LaserMark\Mgr\MutiMarkConfig.cs` | 多头 IO 等配置 |
| `LaserMark\Laser\MutiCardForm.cs` | 多卡相关窗体 |
| `LaserMark\UI\CardFileEdit.cs` | 按卡编辑图档 |
| `MultiHeadTest\` | 精简多头试验工程（含自有 `LmcLib2.cs` / `Laser.cs`） |

`Laser.CheckFunction()`：期望 `GetFuntionID() == 30`（多头多文档功能号；以实际客户库为准）。

## 与单头 Demo 的关系

通用单头 Demo 仍在：`d:\EZCAD2\开发\BJJCZ_Ezcad2\Demo\C#\`（含 `BJJCZ_EZCAD2.0_多头\` 等）。

**卡序号排序 Demo** 与 **多头多文档常规工程** 是多头联调的首选对照；单头 `JczLmc` 仅作环境/错误码参考，不可直接替代 MultiMarkEzd 调用。
