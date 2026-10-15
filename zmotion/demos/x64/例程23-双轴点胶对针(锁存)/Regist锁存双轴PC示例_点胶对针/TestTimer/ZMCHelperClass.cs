using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Collections;

using cszmcaux;


//******ZMCHelperClass非官方文件，仅为客户使用方便封装了一些函数*******
//******************可以根据自己的需要自行修改*************************
namespace AxisStatus_Scan
{

    public enum ZMCAxisMask
    {
        ALL = 0xFFFF,
        None = 0x0,
        FieldbusDisconnect = 0x4,
        HomeAndPosLimit = 0x50,
        HomeAndNegLimit = 0x60,
        CommFault = 0x64563C,
        CommFaultandHome = 0x64567C,
    };

    public static class ZMCHelperClass
    {
        //新版函数库中有官方批量读取轴参数函数可供调用，这个函数返回的是整型数组
        //axisParam为轴参数，例如axisstatus(不区分大小写)，iMaxAxis为轴数，axisParaArray为用户自己创建的数组
        public static Int32 GetMultiParam(IntPtr handle, String axisParam, UInt16 iMaxAxis, Int32[] axisParaArray)
        {

            string cmdString = "?*" + axisParam;
            UInt32 uiResponseLength = 2048;
            StringBuilder psResponse = new StringBuilder((Int32)uiResponseLength);            
            int iReturn = zmcaux.ZAux_DirectCommand(handle, cmdString, psResponse, uiResponseLength);
            if (iReturn == 0 && iMaxAxis != 0)
            {
                string repString = psResponse.ToString();
                String[] subStringArray = repString.Split(new Char[] { ' ' });
                int index = 0;
                foreach (string s in subStringArray)
                {
                    if (s.Trim() != "")
                    {
                        axisParaArray[index++] = Convert.ToInt32(s);
                        if (index >= iMaxAxis) { break; }
                    }
                }
                return 0;
            }
            else
                return iReturn;
        }


        //只有4系列以上控制器(且较新固件)才能使用这个函数（双精度浮点数Table寄存器），其他系列请使用辅助库开放的标准函数
        //tableStart是Table寄存器的起始地址，tableCount是Table数量，pcDoubleArray是用户PC上的Double数组的地址，pcArrayStart是PC上Double数组的起始索引
        public static UInt32 TableWriteDouble(IntPtr handle, UInt32 tableStart, UInt16 tableCount, Double[] pcDoubleArray, UInt32 pcArrayStart)
        {
            UInt32 iResult;
            if (tableCount > 25)
            {
                //单次传输数据不能超过200个字节(25个double)
                iResult = 0xFFFF;
            }
            else
            {
                byte[] byteArray = new byte[tableCount * 8];
                //static void BlockCopy (Array src, int srcOffset, Array dst, int dstOffset, int count);
                Buffer.BlockCopy(pcDoubleArray, (Int32)(pcArrayStart * 8), byteArray, 0, tableCount * 8);
                iResult = ZMC_RegisterWrite(handle, "DT", 64, tableStart, tableCount, byteArray);
            }
            return iResult;
        }

        public static UInt32 TableWriteDouble2(IntPtr handle, UInt32 tableStart, UInt16 tableCount, Double[] pcDoubleArray, UInt32 pcArrayStart)
        {
            UInt32 iResult = 0;
            Int32 fullLoadTims, remaning, maxTbsPerTrans, bytesCtsInDouble;
            maxTbsPerTrans = 25;     //单次传输数据不能超过200个字节(25个double)
            fullLoadTims = tableCount / maxTbsPerTrans;  
            remaning = tableCount % maxTbsPerTrans;
            bytesCtsInDouble = 8;
            byte[] byteArray = new byte[maxTbsPerTrans * bytesCtsInDouble];
            //byte[] byteArray = new byte[tableCount * 8];
            Int32 index = 0;
            while (index < fullLoadTims)
            {
                //static void BlockCopy (Array src, int srcOffset, Array dst, int dstOffset, int count);
                Buffer.BlockCopy(pcDoubleArray, (Int32)((pcArrayStart + index*maxTbsPerTrans)*bytesCtsInDouble), byteArray, 0, maxTbsPerTrans * bytesCtsInDouble);
                iResult = ZMC_RegisterWrite(handle, "DT", 64, (UInt32)(tableStart + index * maxTbsPerTrans), (UInt16)maxTbsPerTrans, byteArray);
                index++;
            }
            if (remaning != 0)
            {
                //static void BlockCopy (Array src, int srcOffset, Array dst, int dstOffset, int count);
                Buffer.BlockCopy(pcDoubleArray, (Int32)((pcArrayStart + index * maxTbsPerTrans) * bytesCtsInDouble), byteArray, 0, remaning * bytesCtsInDouble);
                iResult = ZMC_RegisterWrite(handle, "DT", 64, (UInt32)(tableStart + index * maxTbsPerTrans), (UInt16)remaning, byteArray);

            }
            return iResult;
        }


        //只有4系列以上控制器才能使用这个函数（双精度浮点数Table寄存器），其他系列请使用辅助库开放的标准函数
        //tableStart是Table寄存器的起始地址，tableCount是Table数量，pcDoubleArray是用户PC上的Double数组的地址，pcArrayStart是PC上Double数组的起始索引
        public static UInt32 TableReadDouble(IntPtr handle, UInt32 tableStart, UInt16 tableCount, Double[] pcDoubleArray, UInt32 pcArrayStart)
        {
            UInt32 iResult;
            if (tableCount > 25)
            {
                //单次传输数据不能超过200个字节(25个double)
                iResult = 0xFFFF;
            }
            else
            {
                byte[] byteArray = new byte[tableCount * 8];
                iResult = ZMC_RegisterRead(handle, "DT", 64, tableStart, tableCount, byteArray);
                if (iResult == 0)
                {
                    //static void BlockCopy (Array src, int srcOffset, Array dst, int dstOffset, int count);
                    Buffer.BlockCopy(byteArray, 0, pcDoubleArray, (Int32)(pcArrayStart * 8), tableCount * 8);
                }
            }
            return iResult;
        }

        //这个回零函数(Home Without Alarm)自动设置回零状态标志(homeStatus数组),防止碰到限位误报警.homeAxisList为轴列表,homeMethodList为回零模式列表.
        //使用EtherCAT总线驱动的回零模式时,因为考虑到总线通信速度问题,所以每个回零函数调用后最好加点延时
        //这里使用List而不使用Array的原因是删除起来比较方便(检测回原点结束).
        public static Int32 HomeWoAlarm(IntPtr handle, List<Int32> homeAxisList, List<Int32> homeMethodList, UInt32[] homeStatus)
        {
            Int32 errorCode = 0;
            if (homeAxisList.Count != homeMethodList.Count)
            {
                errorCode = 0xFFFF;
                return errorCode;
            }              
            Int32 axisCounts = homeAxisList.Count;
            for(Int32 i = 0; i < axisCounts; i++)
            {
                Int32 axisNum = homeAxisList[i];
                Int32 homeMethod = homeMethodList[i];
                //AXIS_STOPREASON可以用于debug,检测停止原因,这里用来做回零标志,回零前先清零.
                zmcaux.ZAux_Direct_SetParam(handle, "AXIS_STOPREASON", axisNum, 0);
                //为对应的回零模式设置掩码,防止碰到限位误报警. 如果使用驱动的回零模式,并且限位映射到了控制器的话,请自行查阅驱动手册设置掩码
                switch(homeMethod)
                {
                    case 11:
                    case 111:
                    case 13:
                    case 113:
                    case 15:
                    case 115:
                    case 18:
                    case 118:
                        homeStatus[axisNum] = (UInt32)ZMCAxisMask.HomeAndPosLimit;  //原点和正限位,防止正限位报警
                        break;
                    case 12:
                    case 112:
                    case 14:
                    case 114:
                    case 16:
                    case 116:
                    case 19:
                    case 119:
                        homeStatus[axisNum] = (UInt32)ZMCAxisMask.HomeAndNegLimit;  //原点和正限位,防止负限位报警
                        break;               
                }
                Int32 ret = zmcaux.ZAux_Direct_Single_Datum(handle, axisNum, homeMethod);
                if (ret != 0)
                {
                    //添加代码 
                    errorCode = axisNum;
                    break;
                }
            }
            return errorCode;
        }

        //封装这个函数是因为有些总线电机没开放到位信号可供查询(脉冲电机一般有一个到位信号输入)
        //不能用于Datum回零函数到位判断, 请使用GetIfIdle. 不能用于电机暂停情况时的到位判断 
        //axisNum为轴号; posErrorLimit为允许的最大位置误差阈值, 大小根据自己设备和电机情况设置, 太小的话将永远为True
        //检测到位后最好等待一小会(10-100ms)再执行其他动作, 防止电机还在抖动中
        public static Int32 CheckINPos(IntPtr handle, Int32 axisNum, Double posErrorLimit)
        {
            Int32 idleStatus = 0;
            float commandEndPos = 0, currentMotorPos = 0;

            //先等待命令位置到达
            zmcaux.ZAux_Direct_GetIfIdle(handle, axisNum, ref idleStatus);
            if (idleStatus == -1)
            {
                zmcaux.ZAux_Direct_GetEndMoveBuffer(handle, axisNum, ref commandEndPos);
                zmcaux.ZAux_Direct_GetMpos(handle, axisNum, ref currentMotorPos);
                if (commandEndPos - currentMotorPos <= posErrorLimit)
                    return -1;
            }
            return 0;
        }

        //注意, 只有部分总线电机才支持查询到位信号, 很多电机都不支持, 包括松下, 具体查看驱动手册.
        //bitPosition对应Drive_Status参数上的到位标志的bit位, 也就是对应驱动的6041h的bit位, 从0开始.
        public static Int32 CheckINPosEC(IntPtr handle, Int32 axisNum, Int32 bitPosition)
        {
            float driveStatus = 0;
            zmcaux.ZAux_Direct_GetParam(handle, "DRIVE_STATUS", axisNum, ref driveStatus);
            return ((Convert.ToUInt32(driveStatus) & (1 << bitPosition)) != 0)? -1: 0;
        }


        //测试用（效率可能不高），使打印出来的字符串更整齐，printWidth为每行元素个数
        public static string PrintValues(IEnumerable usrList, UInt16 printCount, int printWidth)
        {
            StringBuilder printString = new StringBuilder();
            int index = 0;
            foreach (var obj in usrList)
            {
                if (index >= printCount)
                    break;
                else if (index % printWidth == 0)
                {
                    printString.Append("\n");
                    printString.Append("(" + index.ToString() + ")" + ":" + obj.ToString() + "\t");
                }
                else
                    printString.Append("(" + index.ToString() + ")" + ":" + obj.ToString() + "\t");
                index++;
            }
            printString.Append("\n");
            return printString.ToString();
        }

        //批量读取MPOS, 一次只能读取32个轴的数据. 
        public static Int32 GetMultiMpos(IntPtr handle, Int32 axisStartNum, Int32 axisCount, float[] mposArrayList, Int32 mposArrayIndex)
        {
            if (axisStartNum < 0 || axisStartNum >=128 || axisCount <= 0 || axisCount > 32)
                return 50000;
            UInt16[] pdata = new UInt16[axisCount * 2];
            Int32 ret = zmcaux.ZAux_Modbus_Get4x(handle, (UInt16)(11000 + axisStartNum * 2), (UInt16)(axisCount * 2), pdata);
            if (ret != 0)
                return ret;
            if(mposArrayList.Length < axisCount + mposArrayIndex)
                return 50001;
            Buffer.BlockCopy(pdata, 0, mposArrayList, mposArrayIndex * 4, axisCount * 4);
            return 0;
        }


        /*************************************************************
        Description:    // 寄存器操作  一次长度不要超过200字节
        Input:          //卡链接handle 寄存器地址
                        pRegName "D" "M" "VR" "VR_INT" "V" "Z"   "Y" "T" "C" "DT"// DT就是对应table 64位
                        iRegbites  0-缺省， "T" "C" 有用

        Output:         //
        Return:         //错误码
        *************************************************************/
        //uint32 __stdcall ZMC_RegisterRead(ZMC_HANDLE handle, const char *pRegName, uint8 iRegbites, uint32 start, uint16 inum, uint8* pdata)
        [DllImport("zmotion.dll", EntryPoint = "ZMC_RegisterRead", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 ZMC_RegisterRead(IntPtr handle, String pRegName, Byte iRegbites, UInt32 start, UInt16 inum, Byte[] pdata);

        /*************************************************************
        Description:    // 寄存器操作  一次长度不要超过200字节
        Input:          //卡链接handle 寄存器地址
                pRegName "D" "M" "VR" "VR_INT" "V" "Z"   "Y" "T" "C" "DT"// DT就是对应table 64位
                iRegbites  0-缺省， "T" "C" 有用

        Output:         //
        Return:         //错误码
        *************************************************************/
        //uint32 __stdcall ZMC_RegisterWrite(ZMC_HANDLE handle, const char* pRegName, uint8 iRegbites, uint32 start, uint16 inum, uint8* pdata);
        [DllImport("zmotion.dll", EntryPoint = "ZMC_RegisterWrite", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 ZMC_RegisterWrite(IntPtr handle, String pRegName, Byte iRegbites, UInt32 start, UInt16 inum, Byte[] pdata);

        /*************************************************************
        Description:    //主动上报回调函数, 需要控制器支持.
        Input:          //卡链接handle
        Output:         //
        Return:         //错误码
        *************************************************************/
        public delegate void ZMCCallBack(IntPtr handle, Int32 itypecode, Int32 idatalength, byte[] pdata);
        //int32 __stdcall ZMC_SetAutoUpCallBack(ZMC_HANDLE handle, PZMCAutoUpCallBack pcallback);
        [DllImport("zmotion.dll", EntryPoint = "ZMC_SetAutoUpCallBack", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 ZMC_SetAutoUpCallBack(IntPtr handle, ZMCCallBack pcallback);

        /*************************************************************
       Description:    //下载包文件
       Input:          //卡链接handle 
                       pfilename  zar文件名
                       pfilenameinControl  BASIC系统只有一个包文件，可以不指定.
       Output:         //
       Return:         //错误码
       *************************************************************/
        //int32 __stdcall ZMC_DownZar(ZMC_HANDLE handle, const char* pfilename, const char* pfilenameinControl);
        [DllImport("zmotion.dll", EntryPoint = "ZMC_DownZar", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern Int32 ZMC_DownZar(IntPtr handle, String pfilename, String pfilenameinControl);

    }
}
