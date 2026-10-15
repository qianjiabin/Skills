# EZCAD2 Demo Reference

Demo root: `d:\EZCAD2\开发\BJJCZ_Ezcad2\Demo\C#\`

Runtime env: `C:\Users\SZJCZ\.cursor\skills\ezcad2`

| Demo folder | Topic | Key APIs |
|-------------|-------|----------|
| BJJCZ_EZCAD2.0_基础开发 | 全功能基准 | Initialize, LoadEzdFile, ChangeTextByName, Mark, IO, 对象编辑 |
| BJJCZ_EZCAD2.0_预览 | 预览 | GetCurPreviewImage, GetPrevBitmapByName2 |
| BJJCZ_EZCAD2.0_多头 | 多头（简版） | MultiMarkEzd, 多卡 — **完整参考见 [ezcad2-multihead](../ezcad2-multihead/reference.md)** |
| BJJCZ_EZCAD2.0_快速飞行 | 飞行标刻 | MarkEntityFly, MarkFlyByStartSignal, GetFlySpeed |
| BJJCZ_EZCAD2.0_扩展轴 | 扩展轴 | SetDevCfg2, 轴运动 API |
| BJJCZ_EZCAD2.0_多点校正 | 校正 | 校正表相关 |
| BJJCZ_EZCAD2.0_笔参数 | 笔号 | 笔参读写 |
| BJJCZ_EZCAD2.0_文本参数/文本对象/圆弧文本 | 文本 | 文本创建与参数 |
| BJJCZ_EZCAD2.0_条码 | 条码 | 条码对象 |
| BJJCZ_EZCAD2.0_位图 | 位图 | 位图参数 |
| BJJCZ_EZCAD2.0_填充 | 填充 | Hatch |
| BJJCZ_EZCAD2.0_分割 | 分块 | Split；空盒不生成对应 BOXn 对象（见 SKILL 注意事项） |
| BJJCZ_EZCAD2.0_添加删除对象 | 对象 CRUD | Add/Del/CopyEnt |
| BJJCZ_EZCAD2.0_字体 | 字体 | 字体列表 |
| BJJCZ_EZCAD2.0_USB板卡检测 | USB 检测 | 板卡检测 |
| TestEzdSave | 保存测试 | SaveEntLibToFile |

## JczLmc.cs

共享封装位于 `skills\ezcad2\JczLmc.cs`，DllImport 目标 `MarkEzd.dll`。
源文件：`d:\LaserDevelopment\EZCAD2\2026\申为0903\SWLaser\JczLmc.cs`（UTF-8 BOM；skill 内 namespace 为 `BJJCZ`，拷贝后改为项目名）。

## PostBuild 复制示例

```xml
<Target Name="AfterBuild">
  <Exec Command="xcopy /E /Y /I &quot;C:\Users\SZJCZ\.cursor\skills\ezcad2\*&quot; &quot;$(TargetDir)&quot;" />
</Target>
```