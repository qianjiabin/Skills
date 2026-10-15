# Skills shared DLL directory (Learol / JSON only)

`C:\Users\SZJCZ\.cursor\skills\libs`

用于 **laser-winforms-base-ui** 等通用 WinForms 组件，不含 EZCAD 运行环境。

| DLL | 用途 |
|-----|------|
| Learol.App.Controls.dll | LogView |
| Learol.App.Utility.dll | Learol 依赖 |
| Learol.Utility.dll | Learol 依赖 |
| Newtonsoft.Json.dll | JSON 配置 |

## 界面图标（libs\icons）

WinForms 图片按钮统一从此目录加载，项目内通过 `UiIconHelper` 调用。

| 文件 | 用途 |
|------|------|
| open_template.png | 打开模板 |
| web_api.png | WebApi |
| system_settings.png | 系统参数 |

运行时优先使用 `.\Assets\icons\`（编译时从本目录复制到输出目录）。

主界面布局规范见 `skills\laser-winforms-base-ui\templates\layout-spec.md`（嘉泰标准模板）。

## EZCAD 运行环境（勿放 libs）

| 版本 | 复制源目录 |
|------|-----------|
| EZCAD3 | `C:\Users\SZJCZ\.cursor\skills\ezcad3` |
| EZCAD2 | `C:\Users\SZJCZ\.cursor\skills\ezcad2` |

对应 skill：`ezcad3-test`、`ezcad2-test`