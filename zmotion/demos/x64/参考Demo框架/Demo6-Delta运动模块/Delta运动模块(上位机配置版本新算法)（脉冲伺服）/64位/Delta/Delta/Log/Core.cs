using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG.Core
{
    #region 【辅助类】
    /// <summary>
    /// 文件操作辅助API
    /// </summary>
    [Serializable]
    public class FileComparer : System.Collections.IComparer
    {
        int System.Collections.IComparer.Compare(Object o1, Object o2)
        {
            FileInfo fi1 = o1 as FileInfo;
            FileInfo fi2 = o2 as FileInfo;
            ////按修改时间倒序
            //fi1.LastWriteTime.CompareTo(fi2.LastWriteTime)*-1
            ////按修改时间正序
            //fi1.LastWriteTime.CompareTo(fi2.LastWriteTime)
            ////按创建时间倒序
            //fi1.CreationTime.CompareTo(fi2.CreationTime)*-1
            ////按创建时间正序
            //fi1.CreationTime.CompareTo(fi2.CreationTime)
            return fi1.CreationTime.CompareTo(fi2.CreationTime);//按创建时间正序
        }
    }
    #endregion

    /// <summary>
    /// 日志参数
    /// </summary>
    public struct LogInfo
    {
        /// <summary>
        /// 日志生成时间
        /// </summary>
        public DateTime NowTime;
        /// <summary>
        /// 日志存放路径
        /// </summary>
        public string FolderPath;
        /// <summary>
        /// 日志类型
        /// </summary>
        public LogType Type;
        /// <summary>
        /// 日志信息
        /// </summary>
        public string Info;
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 正常消息
        /// </summary>
        Message,
        /// <summary>
        /// 警报消息
        /// </summary>
        Warning,
        /// <summary>
        /// 报错消息
        /// </summary>
        Error
    }
}
