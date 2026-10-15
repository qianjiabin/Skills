using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Motion
{
    public class IniFile
    {
        //Ini文件路径
        string Path;
        //获取程序名
        string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);


        public IniFile(string IniPath = null)
        {
            if(IniPath == null)
            {
                Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
            }
            else
            {
                Path = new FileInfo(IniPath+ ".ini").FullName;
            }
            
        }
        //读Ini文件的健
        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }
        //写Ini文件的健
        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }
        //删除Ini文件的健
        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }
        //删除Ini文件的段
        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }
        //判断健是否存在
        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }

        public bool IniExists()
        {
            if (File.Exists(Path))
            {
                return true;
            }
            else
            {
                return false;
            }           
        }

    }
}