---
name: jcz-laser-libraries
description: >-
  Integrate JCZ LaserDevelopment common libraries (.NET Framework 4.6-4.7.2):
  Database (SQL Server/SQLite), LogExcel (NPOI daily logs), MES (HTTP JSON/SOAP),
  Modbus (HslCommunication TCP), TCP (async client/server).
  Use when referencing 通用类库, JCZ.LaserDevelopment.*, 数据库, Excel日志, MES对接,
  Modbus PLC, TCP通信, 江苏罡阳TCP, 常州巨凌, 航奥Modbus, or adding DLL references
  from d:\LaserDevelopment\通用类库.
---

# JCZ 通用类库（LaserDevelopment Libraries）

金橙子激光二次开发 **通用类库**，位于 `d:\LaserDevelopment\通用类库\`。五个库互不引用，由 EZCAD2/EZCAD3 现场工程按需组合。

## 库索引

| 库 | 路径 | 输出 | 参考 |
|----|------|------|------|
| Database | `JCZ.LaserDevelopment.Database` | `JCZ.LaserDevelopment.Database.dll` | [database.md](database.md) |
| LogExcel | `JCZ.LaserDevelopment.LogExcel` | 源码复制（非 DLL 引用） | [logexcel.md](logexcel.md) |
| MES | `JCZ.LaserDevelopment.MES` | `JCZ.LaserDevelopment.MES.dll` | [mes.md](mes.md) |
| Modbus | `JCZ.LaserDevelopment.Modbus` | `JCZ.LaserDevelopment.Modbus.dll` | [modbus.md](modbus.md) |
| TCP | `JCZ.LaserDevelopment.TCP` | `JCZ.LaserDevelopment.TCP.dll` | [tcp.md](tcp.md) |

## 常见组合

| 场景 | 库 | 示例工程 |
|------|-----|----------|
| MES + 上位机 TCP | MES + TCP | 江苏罡阳TCP |
| 仅 MES HTTP | MES | 常州巨凌MES |
| 仅 TCP 客户端 | TCP | 斯莱克 |
| PLC Modbus | Modbus | 江苏航奥多头拼接 |
| 本地打标记录 | Database | 先导多头加工 |
| 生产 Excel 日志 | LogExcel | 宁波禾森 |

## 引用方式

### DLL 引用（Database / MES / Modbus / TCP）

1. 添加项目引用或 DLL 引用
2. 复制依赖：`System.Data.SQLite`（Database）、`Newtonsoft.Json`（MES）、`HslCommunication`（Modbus）
3. 目标框架 >= 库要求（Database 4.7，MES/TCP 4.6，Modbus 4.7.2）

### LogExcel（源码复制）

复制 `LogExcel.cs` 到 `Log\`，命名空间 `LaserMark`，宿主添加 NPOI。模板：`d:\LaserDevelopment\通用类库\其余模板\日志\LogExcel.cs`

## Agent 工作流

1. 确认需求类型（数据库/日志/MES/PLC/TCP）
2. 读对应 reference.md，对照 FormTest Demo
3. 引用库 + 依赖 DLL
4. 与 laser-winforms-base-ui 组合：UI 用 base-ui，通信用本 skill

## 详细 API

- [database.md](database.md)
- [logexcel.md](logexcel.md)
- [mes.md](mes.md)
- [modbus.md](modbus.md)
- [tcp.md](tcp.md)
