# VisionFlyMark 加工逻辑参考

## 工程文件

| 文件 | 职责 |
|------|------|
| `Form1.cs` | 双线程、背压、矩阵变换计算、加工下发 |
| `VisionTcpServer.cs` | TCP 服务端、坐标/NG 解析 |
| `AppSettingsStore.cs` | `Configs/appSettings.json` 读写 |

## 四次 IO 标准场景

| IO | TCP | 设置矩阵槽 | 板卡选矩阵 | finish 后 |
|----|-----|-----------|-----------|----------|
| 1 | NG | 0-9 全 Avail=0x0 | 0-9 | 10 |
| 2 | NG | 10-19 全无效 | 10-19 | 20 |
| 3 | 5 点 OK | 20-24 有效，25-29 补无效 | 20-29 | 30 |
| 4 | 5 点 OK | 0-4 有效，5-9 补无效（需 finish>=10） | 0-9 | 40 |

## 曾出现的错误模式（勿回归）

| 错误 | 后果 |
|------|------|
| 先出队再 FlyWait | 第 N 次 IO 用到第 N+1 批矩阵 |
| readyMarkQueue 空则跳过加工 | IO 与视觉批次脱节 |
| 无 已下发-finish<=20 背压 | 下发可领先设置 |
| 整批写 10 只检查一次背压 | 写穿背压 |
| Sync 时补写矩阵 | 掩盖 TCP 缺失 |

## 相关工程

- HardwareIoMatrixTest：仅 FlyWait+选矩阵
- VisionTcpClient：TCP 测试客户端
