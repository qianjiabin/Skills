---
name: zmotion
description: >-
  Scaffold and integrate Zmotion (正运动 / ZMC) motion controllers in C# WinForms
  via zauxdll.dll + zmotion.dll and Zmcaux.cs P/Invoke. Covers x86/x64 runtime
  deploy, FastOpen/Ethernet connect, axis param, home, JOG, Move/MoveAbs, IO.
  Use when 正运动, Zmotion, ZAux, zauxdll, zmotion.dll, Zmcaux, 轴控, 回零, JOG,
  FastOpen, or EZCAD2/EZCAD3 projects with external ZMC axis cards.
---

# 正运动（Zmotion）Skill

官方 C# 库与例程已整理到本目录（源自 `C#.zip`）。

## 目录

```
skills/zmotion/
├── SKILL.md
├── README.md
├── reference.md          # 例程索引
├── Zmcaux.cs             # P/Invoke 封装（拷贝进项目编译）
├── x86/                  # 32 位运行库 → EZCAD2 / x86 工程
│   ├── zauxdll.dll
│   ├── zmotion.dll
│   └── readme.txt
├── x64/                  # 64 位运行库 → EZCAD3 / x64 工程
│   ├── zauxdll.dll
│   ├── zmotion.dll
│   └── readme.txt
└── demos/
    ├── x86/              # 32 位例程源码（已去掉 bin/obj）
    └── x64/              # 64 位例程源码（已去掉 bin/obj）
```

## 平台选择（关键）

| 宿主工程 | PlatformTarget | 复制目录 |
|----------|----------------|----------|
| EZCAD2 / MarkEzd | **x86** | `skills\zmotion\x86\*` → `bin\Debug\` |
| EZCAD3 / EzdKernel | **x64** | `skills\zmotion\x64\*` → `bin\Debug\` |

**位宽必须一致**：x86 进程配 32 位 DLL，x64 进程配 64 位 DLL。混用会出现 `BadImageFormat` / “Incorrect format” / 连接异常。

`zauxdll.dll`（辅助库）与 `zmotion.dll`（主库）必须放在**同一输出目录**（通常即 exe 旁）。

## 新建工程步骤

1. 平台设为 x86 或 x64（与 EZCAD 版本一致）
2. 拷贝 `Zmcaux.cs` 到项目（namespace `cszmcaux`）
3. 将对应 `x86` 或 `x64` 下的 `zauxdll.dll`、`zmotion.dll` 复制到输出目录（PostBuild / 手动）
4. 对照 `demos\x86\例程0-板卡连接` 或 `demos\x64\例程0-板卡连接`

## 常用 API（ZAux）

```csharp
using cszmcaux;
IntPtr handle = IntPtr.Zero;

// type=2 以太网；ip 为控制器 IP
int err = zmcaux.ZAux_FastOpen(2, "192.168.0.11", 1000, out handle);
// 或 ZAux_OpenEth(ip, out handle)

zmcaux.ZAux_Direct_SetUnits(handle, axis, units);
zmcaux.ZAux_Direct_SetSpeed(handle, axis, speed);
zmcaux.ZAux_Direct_SetAccel(handle, axis, accel);
zmcaux.ZAux_Direct_SetDecel(handle, axis, decel);

zmcaux.ZAux_Direct_Single_Vmove(handle, axis, dir);   // JOG
zmcaux.ZAux_Direct_Single_Move(handle, axis, dist);   // 相对
zmcaux.ZAux_Direct_Single_MoveAbs(handle, axis, pos); // 绝对
zmcaux.ZAux_Direct_Single_Datum(handle, axis, mode);  // 回零
zmcaux.ZAux_Direct_GetDpos(handle, axis, ref pos);
zmcaux.ZAux_Direct_GetIfIdle(handle, axis, ref idle);
zmcaux.ZAux_Direct_Single_Cancel(handle, axis, 2);

zmcaux.ZAux_Close(handle);
```

## 与激光工程协作

- **EZCAD2**：x86 + `ezcad2-test` + 本 skill 的 `x86`
- **EZCAD3**：x64 + `ezcad3-test` + 本 skill 的 `x64`
- 轴控 UI / 参数持久化可参考昆太 Mark 的 `ZmotionMgr` / `FormZmotion`

例程列表见 [reference.md](reference.md)。
