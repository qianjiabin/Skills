# EZCAD3 Demo Reference

主 SDK：`d:\EZCAD3\开发\AppendData (1)\AppendData\Ezcad3_C#_Demos\Ezcad3_C#_Demos\`  
列表专项：`d:\EZCAD3\开发\列表1\列表\EZCAD3list\`  
Runtime：`C:\Users\SZJCZ\.cursor\skills\ezcad3`

## 常用 Demo 索引

| Demo | 用途 |
|------|------|
| 1HelloWorld | 初始化 / 生命周期 |
| 2ChangeText | 按名改文本 |
| 4Mark | 普通标刻 |
| 16IO | IO |
| 24TestLaser | 激光测试 |
| 25List | 列表标刻（BackgroundWorker 循环） |
| 27HardwareState | 硬件状态 |
| 28AdvancedList_IO | 列表 + 外触发/输出口循环 |
| 28AdvanceList_CustomGraphics | 列表自定义图形 |
| 28AdvanceList_Loop_Pause | 列表循环/暂停 |
| **列表1/EZCAD3list** | **列表指令逐步拼装教学（按钮式）** |

## 列表 Demo 对照

| | `列表1\列表\EZCAD3list` | AppendData `25List` | AppendData `28AdvancedList_IO` |
|--|------------------------|---------------------|--------------------------------|
| 风格 | UI 逐步点按钮拼指令 | 后台线程一次跑完 | 外触发循环列表 |
| ListReady | 手动按钮 | 线程内 | 线程内 |
| 指令 | WaitInput / Delay / MarkEnt / Curve / WritePort / Count | 主要 MarkEntToList2 | WaitInput + MarkByPen + 输出口 + Delay |
| 下发执行 | `SentBufToCardAndRunList` 单独按钮 | Ready 后立刻 Sent | 每轮循环末 Sent |
| 结束 | ListEnd → WaitForFinish | ListEndToList → WaitForFinish → ListEnd | ListEnd → WaitForFinish |
| 计数 | Timer 轮询 GetMarkCount | — | — |
| 封装 | 旧 `E3_Initial` / `E3_DrawEnt` | 新 `EzdKernel` | `Initial(USB)` 新封装 |

新工程封装优先用 AppendData 的 `EzdKernel`；**指令顺序与语义以 EZCAD3list 为准学习**。

## 列表指令速查（EZCAD3list）

```
1  ListReady(marker, em, flag)
2  [可选组合]
     SetInputPortWorkMode(pulse, lowValid)   // Ready 前设置一次即可
     WaitForInputToList(mask, level)         // mask 例: 1<<1 = 端口1
     DelayUsToList(us)
     MarkEntToList2(marker, layerOrEnt)
     OneCurveToList2(marker, type, pen, ptNum, flag, pt[n,3])
     WritePortToList(data)                   // data 例: 1 = 端口0
     ChangeMarkCountToList(+n)
3  SentBufToCardAndRunList                   // 立刻执行已缓冲指令
4  ListEnd
5  WaitForFinish
```

输入口：`PORT = 1 << bitIndex`。脉冲模式下 level 无意义；电平模式需设高低有效。

## PostBuild

```xml
<Exec Command="xcopy /E /Y /I &quot;C:\Users\SZJCZ\.cursor\skills\ezcad3\*&quot; &quot;$(TargetDir)&quot;" />
```
