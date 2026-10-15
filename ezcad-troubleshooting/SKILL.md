---
name: ezcad-troubleshooting
description: >-
  Diagnose and resolve EZCAD2 (MarkEzd, x86) and EZCAD3 (EzdKernel, x64) SDK integration issues
  in C# WinForms (.NET 4.8). Matches symptoms and error codes against a shared issue knowledge base.
  Use when EZCAD2/EZCAD3 开发报错, MarkEzd/EzdKernel 初始化失败, lmc1 错误, E3_ERR, 打标异常,
  DLL 缺失, BadImageFormat, x86/x64 平台问题, 开发库问题, 常见开发库问题, NOCLIENT, ERRPARAM,
  工艺效果不对, 无卡测试, 多头, 视觉定位, P/Invoke签名, 8009010b, DXF换图, 矢量图替换,
  E3_ImportFileToEntMgr_2, AccessViolation, 内存徒增, or troubleshooting beyond ezcad2-test/ezcad3-test scaffolding.
---

# EZCAD 开发问题处理（EZCAD2 + EZCAD3）

面向 **EZCAD2 / MarkEzd.dll** 与 **EZCAD3 / EzdKernel** 二次开发的故障排查。知识库统一在 [issues.md](issues.md)；金橙子官方 FAQ 全文见 [faq-official.md](faq-official.md)（来源：`d:\LaserDevelopment\常见开发库问题解答.docx`）。

| 版本 | 脚手架 Skill | 运行时目录 | 平台 | 主程序需关闭 |
|------|-------------|-----------|------|-------------|
| EZCAD2 | [ezcad2-test](../ezcad2-test/SKILL.md) | `skills\ezcad2` | **x86** | EzCad2.exe |
| EZCAD3 | [ezcad3-test](../ezcad3-test/SKILL.md) | `skills\ezcad3` | **x64** | EzCad3.exe |

## Agent 处理流程

1. **确认版本**
   - EZCAD2：`JczLmc` / `MarkEzd.dll` / `.ezd` / x86
   - EZCAD3：`EzdKernel` / `Ezcad3Kernel.dll` / `.ez3` / x64 / `E3_ERR`
   - 不确定时先问用户或看项目平台与引用
2. **收集现象**：错误码、API、调用栈、运行时部署、主程序是否已关
3. **检索知识库**：读 [issues.md](issues.md)，按 **版本**、**关键词**、**症状**、**错误码** 匹配
4. **命中条目**：按 **处理** 执行；对照 Demo：
   - EZCAD2 单头：`d:\EZCAD2\开发\BJJCZ_Ezcad2\Demo\C#\`
   - EZCAD2 多头：见 [ezcad2-multihead](../ezcad2-multihead/SKILL.md)（MultiMarkEzd 文档 / 卡序号 Demo / 多头多文档工程）
   - EZCAD3：`d:\EZCAD3\开发\AppendData (1)\AppendData\Ezcad3_C#_Demos\`
5. **未命中**：走下方 [通用排查清单](#通用排查清单)（先选版本），解决后提示用户写入 `issues.md`
6. **回复格式**：版本 · 条目 ID（如有）· 根因 · 操作步骤 · 验证预期

## 通用排查清单

### EZCAD2

| 检查项 | 说明 |
|--------|------|
| 平台 | 项目 **x86**，MarkEzd 为 32 位 |
| 运行时 | `skills\ezcad2` 完整复制到 exe 目录（含 `plug/`、`FONT/`、`PARAM/`、`EZCAD.CFG`） |
| 进程 | 初始化前关闭 EzCad2.exe |
| 路径 | `JczLmc.Initialize(Application.StartupPath, false)` |
| 测试模式 | `bTestMode=true` 时控制卡 API 不可用 |
| 封装 | 使用 `JczLmc.cs`，勿混用 EZCAD3 封装 |

### EZCAD3

| 检查项 | 说明 |
|--------|------|
| 平台 | 项目 **x64**，`#define WIN64` |
| 运行时 | `skills\ezcad3` 完整复制到 exe 目录（含 `PARAM/`、`FONT/` 等） |
| 进程 | 初始化前关闭 EzCad3.exe |
| 路径 | `EzdKernel.Initial(Application.StartupPath, InitialMode.USB, null)` |
| 常见错误 | `ERR_CLIENTID` License；`ERR_EZCADRUN` 主程序占用 |
| 依赖 | VC++ 2015 运行时；网口卡查 `PARAM/Controller.ini` |
| BadImageFormat | 几乎总是平台不匹配（须 x64） |

### 两版本共有

| 检查项 | 说明 |
|--------|------|
| 图档路径 | 含中文/空格时先换纯英文路径排除 |
| 混用 | 同一进程不要同时加载 MarkEzd 与 EzdKernel |

## 维护指南

**新增一条问题记录**：

1. 打开 `C:\Users\SZJCZ\.cursor\skills\ezcad-troubleshooting\issues.md`
2. 在 **EZCAD2** / **EZCAD3** / **通用** 对应分类下追加条目
3. **ID 全局递增**：`ISSUE-001`、`ISSUE-002` …（跨版本不重复）
4. **关键词** 写全：版本号、错误码、API 名、中英文症状
5. 可选：在 `description`（frontmatter）补高频触发词
6. 保存即可，无需重启 Cursor

### 条目模板

```markdown
### [ISSUE-XXX] 简短标题

**版本**：EZCAD2 | EZCAD3 | 通用
**症状**：
**原因**：
**处理**：
1.
2.
**关键词**：
**关联**：Demo 名 / API / 错误码
**记录日期**：YYYY-MM-DD
```

## 知识库

| 文件 | 用途 |
|------|------|
| [issues.md](issues.md) | 结构化条目（ISSUE-XXX），Agent 检索匹配 |
| [faq-official.md](faq-official.md) | 金橙子官方《常见开发库问题解答》全文 |

完整问题列表见 **[issues.md](issues.md)**；需要对照原文或查阅完整 FAQ 时读 **[faq-official.md](faq-official.md)**。