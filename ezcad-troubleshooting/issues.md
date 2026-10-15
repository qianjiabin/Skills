# EZCAD 问题知识库（EZCAD2 + EZCAD3）

> 在对应 **版本 + 分类** 下追加条目。ID 全局递增：`ISSUE-001`、`ISSUE-002` …

---

## EZCAD2

### 初始化 / 连接

### [ISSUE-005] Initialize 返回 21（错误的硬件参数）

**版本**：EZCAD2
**症状**：`JczLmc.Initialize` 返回 21，提示错误的硬件参数
**原因**：开发库与板卡不匹配；金橙子发给每个客户的开发库均为唯一定制库，仅限客户自己公司使用
**处理**：
1. 确认开发库压缩包为邮件附件（**特别是 MarkEzd.dll**），重新解压到二开程序目录
2. 打标卡正面条码拍照发给金橙子销售
3. 打开程序目录下 **J8Ext**，点击联网（需外网），确认打标卡是否匹配
4. 授权码须以购买开发库的公司抬头名义购买，与开发库一致
**关键词**：EZCAD2, Initialize, 返回值21, 硬件参数, MarkEzd, 开发库, 板卡不匹配, J8Ext
**关联**：`JczLmc.Initialize` / 官方 FAQ
**记录日期**：2026-09-21

### [ISSUE-006] Initialize 返回 1（EZCADRUN）

**版本**：EZCAD2
**症状**：`JczLmc.Initialize` 返回 1
**原因**：二开软件与 EZCAD2 共用一个库文件，二者不可同时打开
**处理**：
1. 关闭 EzCad2.exe 或二开程序，保证同一时间只打开一个
2. 重新初始化
**关键词**：EZCAD2, Initialize, 返回值1, EZCADRUN, 主程序占用
**关联**：`JczLmc.Initialize`
**记录日期**：2026-09-21

### [ISSUE-007] Initialize 返回 6（找不到设备配置文件）

**版本**：EZCAD2
**症状**：`JczLmc.Initialize` 返回 6，找不到设备配置文件
**原因**：板卡参数保存在 `PLUG/markcfg` 中；电脑首次使用 EZCAD2 时程序目录下可能没有 markcfg
**处理**：
1. 只打开 EZCAD2 程序（不打开二开），软件会自动生成 markcfg 到 PLUG 文件夹
2. 关闭 EZCAD2 后再次打开二开软件
**关键词**：EZCAD2, Initialize, 返回值6, markcfg, PLUG, 设备配置文件
**关联**：`PLUG/markcfg` / `JczLmc.Initialize`
**记录日期**：2026-09-21

### DLL / 部署 / 环境

（待添加）

### 标刻 / 红光

### [ISSUE-021] 加工过程中偶发不出光

**版本**：EZCAD2
**症状**：标刻时偶发不出光；替换对象内容后也不出光
**原因**：二代卡绘图采用 GDI+；内存泄漏或预览时对对象的其他操作导致绘图失败、对象为空；替换内容后生成预览图过程也可能失败
**处理**：
1. 加工前判断对象尺寸，为 0 则报警
2. 如有替换对象内容操作，重复替换一次，一般可解决
3. 排查内存泄漏和预览阶段的额外对象操作
**关键词**：EZCAD2, 不出光, GDI+, 对象为空, 尺寸为0, 预览, 替换内容
**关联**：对象尺寸检查 / 预览
**记录日期**：2026-09-21

### 预览 / 图档

### [ISSUE-015] 标准软件调试工艺与二开加工效果/尺寸不一致

**版本**：EZCAD2
**症状**：标准 EZCAD2 调试好的工艺，二开软件加工效果和尺寸均不对
**原因**：控制卡参数（校正、激光器类型等）保存在 `PLUG/Markcfg` + 板卡 SN；标准软件与二开程序路径不同则参数不同
**处理**：
1. 使用二开程序目录下的 EZCAD2 进行工艺调试
2. 确保桌面 EZCAD2 快捷方式与二开程序指向**同一路径**
**关键词**：EZCAD2, 工艺, 尺寸不对, 效果不对, PLUG, markcfg, 路径不一致
**关联**：`PLUG/markcfg` / 官方 FAQ
**记录日期**：2026-09-21

### IO / 飞行 / 扩展轴

（待添加）

### 对象 / 文本 / 条码

（待添加）

### 编译 / 平台 / P/Invoke

### [ISSUE-016] 程序运行报 P/Invoke 签名错误

**版本**：EZCAD2
**症状**：运行时抛出 P/Invoke 签名相关异常
**原因**：Visual Studio 调试器对 P/Invoke 签名不匹配中断
**处理**：
1. 打开 VS「异常设置」
2. 搜索 `voke`，在 P/Invoke 那一栏**取消勾选**
**关键词**：EZCAD2, P/Invoke, 签名错误, 异常设置, voke
**关联**：调试配置
**记录日期**：2026-09-21

### 多头

### [ISSUE-017] 二代多头索引、延时与 Mark 非阻塞

**版本**：EZCAD2
**症状**：多头控制错乱、掉卡、无法判断加工完成
**原因**：
1. 多头库第一个参数是**索引**非 SN；上电顺序变化导致索引对应卡变化
2. 多卡共用一个驱动，连续操作无延时易掉卡
3. Mark 接口非阻塞，须自行轮询完成状态
**处理**：
1. 根据 SN 判断卡在当前第几张位置，再传对应索引（见 [ezcad2-multihead](../ezcad2-multihead/SKILL.md)）
2. 连续操作多张卡（IO、出光等）中间加延时
3. 调用 **isfinished** 判断当前卡是否空闲、加工是否完成
**关键词**：EZCAD2, 多头, MultiMarkEzd, 索引, SN, 掉卡, isfinished, Mark, 延时
**关联**：`MultiMarkEzd` / [ezcad2-multihead](../ezcad2-multihead/SKILL.md)
**记录日期**：2026-09-21

### 无卡测试

### [ISSUE-018] 二代无卡测试模式限制

**版本**：EZCAD2
**症状**：无卡测试正常，连卡再初始化报 LMC 版本错误；或无法保存模板
**原因**：`Initialize` 第二参数 `bTestMode=true` 进入测试模式；不连卡无法保存模板；测试模式下连卡再初始化冲突
**处理**：
1. 无卡开发：`JczLmc.Initialize(path, true)` 可测试对象替换等
2. 不连卡时接受无法保存模板
3. 需要连卡正式使用时，**不要用测试模式**重新初始化
**关键词**：EZCAD2, 无卡测试, bTestMode, 测试模式, LMC版本错误, 保存模板
**关联**：`JczLmc.Initialize(path, bTestMode)`
**记录日期**：2026-09-21

### 视觉定位

### [ISSUE-019] 视觉定位偏移旋转接口选择

**版本**：EZCAD2
**症状**：重复偏移后对象位置累积漂移；或期望仅加工时偏移但对象坐标被永久改变
**原因**：`MoveEnt`/`RoateEnt` 改变实际坐标；`SetRoateMoveParam` 仅加工时生效
**处理**：
1. 需永久改坐标 → `MoveEnt` / `RoateEnt`；重复操作后恢复初始位置
2. 仅需加工时偏移 → `SetRoateMoveParam`（**无返回值**）
3. 偶发未知错误多因掉卡，先查连接
**关键词**：EZCAD2, 视觉定位, MoveEnt, RoateEnt, SetRoateMoveParam, 偏移, 旋转
**关联**：视觉定位 Demo
**记录日期**：2026-09-21

---

## EZCAD3

### 初始化 / 连接

<!-- 示例（可删除或改写）：

### [ISSUE-002] E3_ERR.ERR_EZCADRUN

**版本**：EZCAD3
**症状**：`EzdKernel.Initial` 返回 `ERR_EZCADRUN`
**原因**：EzCad3 主程序仍在运行
**处理**：
1. 关闭 EzCad3.exe
2. 重新调用 `EzdKernel.Initial`
**关键词**：EZCAD3, ERR_EZCADRUN, 初始化失败, EzdKernel
**关联**：1HelloWorld / `EzdKernel.Initial`
**记录日期**：2026-09-01

-->

### [ISSUE-001] EzdKernel.Initial 返回 6（NOCLIENT / ERR_CLIENTID）

**版本**：EZCAD3
**症状**：`EzdKernel.Initial` 返回 `6`（`E3_ERR.ERR_CLIENTID` / NOCLIENT），初始化失败
**原因**：开发库与加密狗不匹配；金橙子发给每个客户的开发库均为唯一定制库，仅限客户自己公司使用
**处理**：
1. 确认开发库压缩包为**邮件附件**，重新解压到二开程序目录（**不可用群里 Ezcad3_Release 压缩包作开发**）
2. 授权码须以购买开发库的公司抬头名义购买，与开发库一致
3. 将授权码拍照发给金橙子销售，确认加密狗是否匹配
4. 涉及加密狗修改时，用 **LicenseManager** 升级
5. 替换后重启程序，确认 `Initial` 不再返回 6
**关键词**：EZCAD3, 初始化, 返回值6, ERR_CLIENTID, NOCLIENT, 客户ID, ClientID, License, 授权码, 开发库, 加密狗
**关联**：`EzdKernel.Initial` / `E3_ERR.ERR_CLIENTID` / 官方 FAQ
**记录日期**：2026-09-01（2026-09-21 补充官方 FAQ）

### [ISSUE-008] EzdKernel.Initial 返回 1（ERR_EZCADRUN）

**版本**：EZCAD3
**症状**：`EzdKernel.Initial` 返回 1（`E3_ERR.ERR_EZCADRUN`）
**原因**：二开软件与 EZCAD3 共用一个库文件，二者不可同时打开
**处理**：
1. 关闭 EzCad3.exe 或二开程序，保证同一时间只打开一个
2. 重新调用 `EzdKernel.Initial`
**关键词**：EZCAD3, 初始化, 返回值1, ERR_EZCADRUN, EZCADRUN, 主程序占用
**关联**：`EzdKernel.Initial` / `E3_ERR.ERR_EZCADRUN`
**记录日期**：2026-09-21

### [ISSUE-009] 接口返回 2（ERRPARAM）

**版本**：EZCAD3
**症状**：接口返回 2（`E3_ERR.ERRPARAM`），传入参数错误
**原因**：板卡 ID、对象管理器 ID、图层 ID、对象 ID 混淆；使用了已删除对象；部分接口需开通功能（3D、校正等）
**处理**：
1. 检查各 ID 类型是否正确、对象是否仍存在
2. 确认当前开发库/License 是否包含所需功能（3D、校正等）
**关键词**：EZCAD3, 返回值2, ERRPARAM, 参数错误, 板卡ID, 对象管理器, 图层ID, 对象ID
**关联**：`E3_ERR.ERRPARAM`
**记录日期**：2026-09-21

### DLL / 部署 / 环境

### [ISSUE-011] 标准软件报错 8009010b（软件签名异常）

**版本**：EZCAD3
**症状**：EZCAD3 标准软件启动或运行报 8009010b 等签名相关错误
**原因**：开发库软件签名异常或版本不匹配
**处理**：
1. 联系金橙子销售更新开发库
2. 或联系群中金橙子技术人员
**关键词**：EZCAD3, 8009010b, 签名异常, 软件签名, 开发库更新
**关联**：官方 FAQ
**记录日期**：2026-09-21

### 标刻 / 红光

（待添加）

### 预览 / 图档

### [ISSUE-010] 标准软件调试工艺与二开加工效果/尺寸不一致

**版本**：EZCAD3
**症状**：标准 EZCAD3 调试好的工艺，二开软件加工效果和尺寸均不对
**原因**：控制卡参数（校正、激光器类型等）保存在 `PARAM/Markcfg` + 板卡 SN；标准软件与二开程序路径不同则参数不同
**处理**：
1. 使用二开程序目录下的 EZCAD3 进行工艺调试
2. 确保桌面 EZCAD3 快捷方式与二开程序指向**同一路径**
**关键词**：EZCAD3, 工艺, 尺寸不对, 效果不对, PARAM, Markcfg, 路径不一致
**关联**：`PARAM/Markcfg` / 官方 FAQ
**记录日期**：2026-09-21

### [ISSUE-022] 批量换 DXF 用 Import 导致内存徒增 / AccessViolation 闪退

**版本**：EZCAD3
**症状**：`ChangeDXFPath` 或 Excel 批量换图时 UI 卡顿、内存徒增；部分电脑 `AccessViolationException` 闪退（Event 1026），堆栈在 `E3_ImportFileToEntMgr_2`
**原因**：对已存在的矢量对象采用 `E3_DeleteEnt` + `E3_ImportFileToEntMgr_2` 反复新建实体，EntMgr 内存累积；部分环境 native 层崩溃
**处理**：
1. **首选** `E3_SetEntVectorFileInfo` 在原矢量对象上改路径，保留 optimize/autoConnect/尺寸等参数
2. 填充型（type 32）：`E3_DelEntHatchParam_2` → `E3_SetEntVectorFileInfo` → 对齐 → `E3_HatchEnt2` 重建填充
3. 有投影：换图后 `E3_ProjectEnt_2` 恢复
4. 仅图档无占位、首次从外部导入时才用 `E3_ImportFileToEntMgr_2`
**关键词**：EZCAD3, DXF, 矢量图, 换图, E3_SetEntVectorFileInfo, E3_ImportFileToEntMgr_2, AccessViolation, 内存, ChangeDXFPath, Event 1026
**关联**：`E3_SetEntVectorFileInfo` / `E3_DelEntHatchParam_2` / `E3_HatchEnt2` / ezcad3-test「替换矢量图」
**记录日期**：2026-10-13

### IO / 轴 / 网口

（待添加）

### 无卡测试

### [ISSUE-012] 三代无卡模拟测试

**版本**：EZCAD3
**症状**：无控制卡时初始化报 NODEVICE，不确定能否继续开发
**原因**：三代卡无卡也可模拟测试，但需授权码激活；NODEVICE 为预期行为
**处理**：
1. 初始化报 NODEVICE **不用管**，勿因此中断流程
2. 直接创建对象管理器并获取控制卡 ID，会得到虚拟控制卡 ID
3. 利用对象管理器 ID 与虚拟卡 ID 可编辑模板等
**关键词**：EZCAD3, 无卡测试, NODEVICE, 虚拟卡, 对象管理器, 模拟测试
**关联**：`EzdKernel.Initial` / 官方 FAQ
**记录日期**：2026-09-21

### 多头

### [ISSUE-013] 三代多头对象管理器绑定

**版本**：EZCAD3
**症状**：多头场景下对象编辑或加工互相干扰
**原因**：三代多头 = 哪张卡操作哪个对象管理器中的哪些对象
**处理**：
1. 多头加工相同对象 → 共用一个对象管理器
2. 不同头加工不同对象且需单独编辑 → 创建与控制卡数对应的对象管理器，每卡绑定一个
**关键词**：EZCAD3, 多头, 对象管理器, 控制卡, MultiScanner
**关联**：官方 FAQ
**记录日期**：2026-09-21

### 视觉定位

### [ISSUE-014] 视觉定位偏移旋转接口选择

**版本**：EZCAD3
**症状**：重复偏移后对象位置累积漂移；或期望仅加工时偏移但对象坐标被永久改变
**原因**：`MoveRoateEnt` 改变实际坐标；`MarkerSetRoateMoveParam` 仅加工时生效
**处理**：
1. 需永久改坐标 → `MoveRoateEnt`；重复操作后恢复初始位置
2. 仅需加工时偏移 → `MarkerSetRoateMoveParam`
**关键词**：EZCAD3, 视觉定位, MoveRoateEnt, MarkerSetRoateMoveParam, 偏移, 旋转
**关联**：视觉定位 Demo
**记录日期**：2026-09-21

### 对象 / 文本 / 条码

### [ISSUE-003] E3_GetPenParam 的 dFreq 等 double 读成天文数字

**版本**：EZCAD3
**症状**：`E3_GetPenParam` 调用后 `dFreq` / `dMarkSpeed` 等为类似 `9.9999993314392253E+25` 的乱码；EzCad3 界面频率正常（如 2000）
**原因**：原生签名为 `TCHAR pStrName[256]`，`new StringBuilder()` 默认容量仅 16，缓冲区不足导致内存越界，后续 `double` 出参被破坏
**处理**：
1. 使用 `new StringBuilder(256)`（与官方 `6PenParam` Demo 一致）
2. 工程平台设为 **x64**（`#define WIN64`）
3. 检查 `E3_Initial` / `E3_GetPenParam` 返回值
**关键词**：EZCAD3, E3_GetPenParam, dFreq, 频率, StringBuilder, 256, 笔参数, 乱码, 天文数字
**关联**：`6PenParam` / `E3_GetPenParam` / `TCHAR pStrName[256]`
**记录日期**：2026-09-09

### 编译 / 平台 / License

### [ISSUE-004] 检测功能号 E3_GetFun 传参须为 2

**版本**：EZCAD3
**症状**：调用 `E3_GetFun` 检测功能号时结果不对，或与授权/客户库期望的功能号对不上
**原因**：`E3_GetFun(int nParam)` 在检测功能号场景下 **nParam 必须传 2**；传其他值不会得到正确功能号
**处理**：
1. 使用 `E3_GetFun(2)` 读取功能号
2. 再与当前开发库/客户要求的功能号比对
**关键词**：EZCAD3, E3_GetFun, 功能号, nParam, GetFun, 传参2
**关联**：`E3_GetFun` / `EzcadKernel.cs`
**记录日期**：2026-09-11

（待添加）

---

## 通用（两版本共有）

### 对象 / 分割

### [ISSUE-002] 分割盒空区域不生成 BOXn 对象

**版本**：通用
**症状**：按 BOX1、BOX2、BOX3… 顺序访问分割结果时，中间某个 `BOXn` 找不到；或假定分割盒个数与对象个数一一对应导致越界/空引用
**原因**：使用分割盒分割对象时，分割后的对象名称按顺序排列；若某分割盒区域内没有对象（例如 BOX1 与 BOX3 之间为空），实际对象列表中不会出现对应名称（不会有 BOX2），**不会保留空对象占位**
**处理**：
1. 枚举图档中真实存在的对象名，勿按分割盒格子数硬编码连续 `BOX1..BOXn`
2. 按名查找前先确认对象是否存在；空盒区域跳过即可
3. 对照 Demo：`BJJCZ_EZCAD2.0_分割`（EZCAD2）
**关键词**：分割盒, Split, BOX1, BOX2, BOX3, 空对象, 对象名, 分块
**关联**：分割盒 / Split / 对象列表
**记录日期**：2026-09-03

### 其他

（待添加）