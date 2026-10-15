using SG.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SG.Log
{
    /// <summary>
    /// LOG日志记录类
    /// </summary>
    [Serializable]
    internal class SgLog
    {
        #region 构造方法
        /// <summary>
        /// 实例化（开启线程池）
        /// </summary>
        public SgLog()
        {
            //开启线程池来写日志
            ThreadPool.QueueUserWorkItem(a =>
            {
                while (true)
                {
                    LogInfo ex = new LogInfo();

                    lock ("数据出列")
                    {
                        if (LogQueue.Count > 0) //如果队列中有数据，将其出队列
                        {
                            ex = LogQueue.Dequeue();
                            if (!Directory.Exists(ex.FolderPath))
                            {
                                Directory.CreateDirectory(ex.FolderPath);
                                Thread.Sleep(50);
                                WriteLogFile(ex);
                            }
                            else
                            {
                                WriteLogFile(ex);
                            }

                        }
                        else
                        {
                            Thread.Sleep(50); //如果没有数据，让线程睡30毫秒,之后进入下一轮循环
                            continue;
                        }
                    }
                    //需要串行执行的代码
                }
            });
        }
        #endregion
         
        #region 对外方法
        /// <summary>
        /// 写入Log日志
        /// </summary>
        /// <param name="_Type">日志等级</param>
        /// <param name="_Info">日志内容</param>
        /// <param name="_FolderPath">日志存储根目录</param>
        public void WriteLog(DateTime _LogDate, LogType _LogType, string _LogInfo, string _FolderPath)
        {
            LogInfo logInfo = new LogInfo();
            logInfo.NowTime = _LogDate;
            logInfo.FolderPath = _FolderPath;
            logInfo.Type = _LogType;
            logInfo.Info = _LogInfo;

            lock ("数据入列")
            {
                LogQueue.Enqueue(logInfo);
            }
        }
        /// <summary>
        /// Log日志清理
        /// </summary>
        /// <param name="_FolderPath">日志路径</param>
        /// <param name="_SaveLimit">保存限制</param>
        /// <returns>错误信息</returns>
        public string ClearLog(string _FolderPath, int _SaveLimit)
        {
            try
            {
                string _Res = string.Empty;
                _Res = this.DeleteFiles(_FolderPath, "*.txt", _SaveLimit);
                if (_Res != null)
                {
                    return _Res;
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        #endregion

        #region 私有字段和方法
        private Queue<LogInfo> LogQueue = new Queue<LogInfo>();
        private string mType = "";
        private void WriteLogFile(LogInfo _LogInfo)
        {
        busy://如果文件处于打开状态，视为BUSY，循环写入，直至写入OK
            try
            {
                if (!Directory.Exists(_LogInfo.FolderPath))
                {
                    Directory.CreateDirectory(_LogInfo.FolderPath);
                }
                string LogFilePath = _LogInfo.FolderPath + @"\" + _LogInfo.NowTime.ToString("yyyyMMdd") + ".txt";
                FileStream fs = new FileStream(LogFilePath, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);

                switch (_LogInfo.Type)
                {
                    case LogType.Message:
                        mType = "消息";
                        break;
                    case LogType.Warning:
                        mType = "警告";
                        break;
                    case LogType.Error:
                    default:
                        mType = "错误";
                        break;
                }
                sw.WriteLine("【" + _LogInfo.NowTime.ToString("HH:mm:ss-fff") + "】\t[" + mType + "]\t" + _LogInfo.Info);
                sw.Close();
                fs.Close();
            }
            catch (Exception)
            {
                Thread.Sleep(500);
                goto busy;
            }
        }

        #region 【删除历史文件和文件夹】
        /// <summary>
        /// 删除历史文件夹
        /// </summary>
        /// <param name="_Folder"></param>
        /// <param name="_DateLimit"></param>
        /// <returns></returns>
        private string DeleteFolder(string _Folder, DateTime _DateLimit)
        {
            try
            {
                lock (this)
                {
                    DirectoryInfo _BootFolder = new DirectoryInfo(_Folder);
                    DirectoryInfo[] _ChildFolderList = _BootFolder.GetDirectories();
                    foreach (DirectoryInfo item in _ChildFolderList)
                    {
                        DateTime _CurDirCT = item.CreationTime;
                        int _Ret = DateTime.Compare(_CurDirCT, _DateLimit);
                        if (_Ret <= 0)
                        {
                            item.Delete();
                        }
                    }
                    GC.Collect();
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// 删除历史文件
        /// </summary>
        /// <param name="_Folder"></param>
        /// <param name="_FileType"></param>
        /// <param name="_ExistLimit"></param>
        /// <returns></returns>
        private string DeleteFiles(string _Folder, string _FileType, int _ExistLimit)
        {
            try
            {
                lock ("DeleteFiles")
                {
                    FileInfo[] files = new DirectoryInfo(_Folder).GetFiles(_FileType);

                    if (files.Length > _ExistLimit)
                    {
                        FileComparer fc = new FileComparer();
                        Array.Sort(files, fc);

                        int DeleteCount = files.Length - _ExistLimit;
                        List<string> HistoryImagesPath = new List<string>();
                        for (int i = 0; i < DeleteCount + 1; i++)
                        {
                            HistoryImagesPath.Add(files[i].FullName);
                        }
                        for (int i = 0; i < HistoryImagesPath.Count; i++)
                        {
                            FileInfo DeleteInfo = new FileInfo(HistoryImagesPath[i]);
                            DeleteInfo.Delete();//删除子目录和文件
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
        #endregion
    }
}
