using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace Assemble
{
    public class Tools
    {
        //ini文件读写
        #region ini 文件读写函数

        /// <summary>
        /// 读取INI文件中指定的Key的值
        /// </summary>
        /// <param name="lpAppName">节点名称。如果为null,则读取INI中所有节点名称,每个节点名称之间用\0分隔</param>
        /// <param name="lpKeyName">Key名称。如果为null,则读取INI中指定节点中的所有KEY,每个KEY之间用\0分隔</param>
        /// <param name="lpDefault">读取失败时的默认值</param>
        /// <param name="lpReturnedString">读取的内容缓冲区，读取之后，多余的地方使用\0填充</param>
        /// <param name="nSize">内容缓冲区的长度</param>
        /// <param name="lpFileName">INI文件名</param>
        /// <returns>实际读取到的长度</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, [In, Out] char[] lpReturnedString, uint nSize, string lpFileName);

        //另一种声明方式,使用 StringBuilder 作为缓冲区类型的缺点是不能接受\0字符，会将\0及其后的字符截断,
        //所以对于lpAppName或lpKeyName为null的情况就不适用
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        //再一种声明，使用string作为缓冲区的类型同char[]
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpReturnedString, uint nSize, string lpFileName);

        /// <summary>
        /// 将指定的键和值写到指定的节点，如果已经存在则替换
        /// </summary>
        /// <param name="lpAppName">节点名称</param>
        /// <param name="lpKeyName">键名称。如果为null，则删除指定的节点及其所有的项目</param>
        /// <param name="lpString">值内容。如果为null，则删除指定节点中指定的键。</param>
        /// <param name="lpFileName">INI文件</param>
        /// <returns>操作是否成功</returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        public double GetPrivateProfileDouble(string lpAppName, string lpKeyName, double Default, string lpFileName)
        {
            // char[] lpReturnedString = new char[1024];
            StringBuilder lpReturnedString = new StringBuilder(1024);
            GetPrivateProfileString(lpAppName, lpKeyName, Convert.ToString(Default), lpReturnedString, 1024, lpFileName);
            return Convert.ToDouble(lpReturnedString.ToString());
        }
        public int GetPrivateProfileInt(string lpAppName, string lpKeyName, int Default, string lpFileName)
        {
            //char[] lpReturnedString = new char[1024];
            StringBuilder lpReturnedString = new StringBuilder(1024);
            GetPrivateProfileString(lpAppName, lpKeyName, Convert.ToString(Default), lpReturnedString, 1024, lpFileName);
            return Convert.ToInt32(lpReturnedString.ToString());
        }

        public bool GetPrivateProfileBool(string lpAppName, string lpKeyName, bool Default, string lpFileName)
        {
            //char[] lpReturnedString = new char[1024];
            StringBuilder lpReturnedString = new StringBuilder(1024);
            GetPrivateProfileString(lpAppName, lpKeyName, Convert.ToString(Default), lpReturnedString, 1024, lpFileName);
            return Convert.ToBoolean(lpReturnedString.ToString());
        }

        public string GetPrivateProfileString(string lpAppName, string lpKeyName, string Default, string lpFileName)
        {
            StringBuilder lpReturnedString = new StringBuilder(1024);
            GetPrivateProfileString(lpAppName, lpKeyName, Default, lpReturnedString, 1024, lpFileName);
            return lpReturnedString.ToString();
        }

        public void WritePrivateProfileStringEx(string lpAppName, string lpKeyName, string lpString, string lpFileName)
        {
            try
            {
                WritePrivateProfileString(lpAppName, lpKeyName, lpString, lpFileName);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
        /////////////////////////////////////////////////////////////////////////////////

       

        public void ErrorLog(string str)
        {
            Log(Directory.GetCurrentDirectory() + "\\Log\\ErrorLog.csv", str, "描述");
        }

        //创建路径
        public void CreateAllDirectory(string strPath)
        {
            try
            {
                string str1, str2;
                int nPos = strPath.LastIndexOf("\\");
                if (nPos > -1)
                {
                    str1 = strPath.Substring(nPos + 1);
                    int nPos2 = str1.LastIndexOf(".");
                    if (nPos2 > -1)
                        str2 = strPath.Substring(0, nPos);
                    else
                        str2 = strPath;
                    if (!Directory.Exists(str2))
                    {
                        Directory.CreateDirectory(str2);
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }

        //获取时间
        public string GetCurrentTimeAsString()
        {
            return string.Format("{0}", DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff"));
        }

        //获取路径
        public string GetDirectory()
        {
           // return Directory.GetCurrentDirectory();
            return Application.StartupPath;
        }


        //写日志  strPath="C:\\csv\\xxx.csv"
        public void Log(string strPath, string strText, string strTitle)
        {

            if (!File.Exists(strPath))
            {
                FileInfo FInfo1 = new FileInfo(strPath);
                CreateAllDirectory(FInfo1.DirectoryName);
                //创建文件流写入对象,向CSV文件中写入数据内容
                StreamWriter SW1 = new StreamWriter(strPath, true);
                //创建数据对象
                StringBuilder SB1 = new StringBuilder();
                SB1.Append("时间,").Append(strTitle);
                //把数据内容写入文件中
                SW1.WriteLine(SB1);
                SW1.Flush();
                SW1.Close();
            }

            //创建文件流写入对象,向CSV文件中写入数据内容
            StreamWriter SW2 = new StreamWriter(strPath, true);
            //创建数据对象
            StringBuilder SB2 = new StringBuilder();
            SB2.Append(GetCurrentTimeAsString()).Append(",").Append(strText);
            //把数据内容写入文件中
            SW2.WriteLine(SB2);
            SW2.Flush();
            SW2.Close();


            //文件大小超过2M,备份文件（文件名+时间做文件名）
            FileInfo FInfo = new FileInfo(strPath);
            if (FInfo.Exists && FInfo.Length > 2 * 1024 * 1024)
            {
                string strName;
                int nPos = strPath.LastIndexOf(".");
                strName = strPath.Substring(0, nPos) + "_" + GetCurrentTimeAsString() + strPath.Substring(nPos);
                FInfo.MoveTo(strName);
            }

        }       
    }

}
