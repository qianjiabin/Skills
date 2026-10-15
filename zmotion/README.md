# Zmotion runtime for skills

正运动 ZMC 控制器 C# 运行库与例程（来源：`C#.zip`）。

## 部署到工程输出目录

**EZCAD2 / x86：**

```
xcopy /Y /Q "C:\Users\SZJCZ\.cursor\skills\zmotion\x86\zauxdll.dll" "$(TargetDir)"
xcopy /Y /Q "C:\Users\SZJCZ\.cursor\skills\zmotion\x86\zmotion.dll" "$(TargetDir)"
```

**EZCAD3 / x64：** 将路径中的 `x86` 改为 `x64`。

## 封装源码

- `Zmcaux.cs`：官方库文件目录版本，拷贝到项目内编译（DllImport `zauxdll.dll`）
- 例程：`demos\x86`、`demos\x64`（已排除 bin/obj）

## 说明

- `zauxdll.dll` 为辅助库，`zmotion.dll` 为主库，二者同目录
- 进程平台位宽必须与 DLL 一致
