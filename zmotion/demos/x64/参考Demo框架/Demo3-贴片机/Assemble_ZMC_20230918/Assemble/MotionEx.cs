/*********************************************************************************
 * 正运动 C#运动控制类
 * Update:20230630
 * Powerby niaoshu&ZMotion   
 * 
 * Update:20220805
 * Powerby hgzhu@szvbt.com   
 * 
 * Update:201801051715
 * Powerby niaoshu
 * https://51halcon.com/
 * *******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using cszmcaux;

namespace Assemble
{
    public class MotionEx
    {
        #region 定义板卡参数
        public IntPtr g_handle;             //运动控制卡连接句柄
        public bool m_bInited = false;      //运动控制卡初始化标志位
        public bool m_bHomed = false;       //回零完成标志位
        public bool m_bPaused = false;      //暂停中标志位
        public bool m_nStoped = false;      //停止中标志位


        //以下参数运动控制参数文件(MoveParams.ini)里可配置

        //定义控制器ip地址
        public string ZMotion_IpAddr = "192.168.0.11";

        //定义输入口
        public int m_nIn_Start = 0;             //启动
        public int m_nIn_Stop = 1;              //停止
        public int m_nIn_EmergStop = 2;         //紧急停止

        //定义输出口
        public short m_nOut_Inhale = 0;         //吸气
        public short m_nOut_LED = 1;            //Led

        //定义轴号
        public short m_nAxis_X = 0;
        public short m_nAxis_Y = 1;
        public short m_nAxis_Z = 2;
        public short m_nAxis_U = 3;

        //定义X轴正负限位与原点端口号
        public int m_nAxis_X_Home = 0;
        public int m_nXLimit_Positive = 2;
        public int m_nXLimit_Negative = 1;

        //定义Y轴正负限位与原点端口号
        public int m_nAxis_Y_Home = 3;
        public int m_nYLimit_Positive = 5;
        public int m_nYLimit_Negative = 4;

        //定义Z轴正负限位与原点端口号
        public int m_nAxis_Z_Home = 6;
        public int m_nZLimit_Positive = 8;
        public int m_nZLimit_Negative = 7;

        //定义U轴原点端口号
        public int m_nAxis_U_Home = 9;

        //定义脉冲轴使能对应的输出口
        //举例1: 正运动ECI1408 使能可以接到任意输出口端子; 
        //举例2: 正运动ECI2828 使能接到轴接口内针脚(可以查看用户手册上轴接口的说明),使能固定对应输出口(16,18,20,22,24,26,28,30);
        public int m_nAxis_ServoX = 8;
        public int m_nAxis_ServoY = 9;
        public int m_nAxis_ServoZ = 10;
        public int m_nAxis_ServoU = 11;

        //定义轴脉冲当量
        public double m_dAxis_X_Units = 400;   //X轴多少个脉冲走1mm
        public double m_dAxis_Y_Units = 400;   //Y轴多少个脉冲走1mm
        public double m_dAxis_Z_Units = 400;   //Z轴多少个脉冲走1mm
        public double m_dAxis_U_Units = 16;    //U轴多少个脉冲走1度

        //定义编码器反馈的脉冲比例
        //步进驱动器一般没有编码器反馈,伺服驱动器一般反馈都是1:1(控制电机轴转1000个脉冲,电机编码器反馈1000个脉冲给控制卡)
        //特殊情况:假如电机转一圈需要6400个脉冲,编码器转一圈反馈4000个脉冲,比率为6400/4000
        public double m_dAxis_X_Ratio = 1 / 1;
        public double m_dAxis_Y_Ratio = 1 / 1;
        public double m_dAxis_Z_Ratio = 1 / 1;
        public double m_dAxis_U_Ratio = 1 / 1;

        //定义复位方向:1正向复位；-1负向复位
        public int m_nAxis_X_Home_Dir = -1;     //X复位方向
        public int m_nAxis_Y_Home_Dir = -1;     //Y复位方向
        public int m_nAxis_Z_Home_Dir = -1;     //Z复位方向
        public int m_nAxis_U_Home_Dir = -1;     //U复位方向

        //定义轴运动速度
        public double m_dAxis_X_Speed = 5;       //x轴运动速度（单位：mm/s）
        public double m_dAxis_X_Auto_Speed = 15; //x轴自动运行速度（单位：mm/s）
        public double m_dAxis_X_Home_Speed = 10; //x轴回零运行速度（单位：mm/s）
        public double m_dAxis_Y_Speed = 5;       //y轴运动速度（单位：mm/s）
        public double m_dAxis_Y_Auto_Speed = 15; //y轴自送运行速度（单位：mm/s）
        public double m_dAxis_Y_Home_Speed = 10; //y轴回零运行速度（单位：mm/s）

        public double m_dAxis_Z_Speed = 5;       //z轴运动速度（单位：mm/s）
        public double m_dAxis_Z_Auto_Speed = 15; //z轴自送运行速度（单位：mm/s）
        public double m_dAxis_Z_Home_Speed = 10; //z轴回零运行速度（单位：mm/s）
        public double m_dAxis_U_Speed = 5;       //u轴运动速度（单位du/s)
        public double m_dAxis_U_Auto_Speed = 15; //u轴自送运行速度（单位：du/s）
        public double m_dAxis_U_Home_Speed = 10; //u轴回零运行速度（单位：du/s）

        //定义运动加减速度
        public double m_dAxis_X_Acc = 500;     //x轴运动加减速度
        public double m_dAxis_Y_Acc = 500;     //y轴运动加减速度
        public double m_dAxis_Z_Acc = 500;     //z轴运动加减速度
        public double m_dAxis_U_Acc = 500;     //u轴运动加减速度

        //定义工具：ini文件读写
        Tools m_Tools = new Tools();

        #endregion

        #region 日志输出
        public LogDelegate Motion_Log;
        public MotionEx(LogDelegate Motion_Log)
        {
            this.Motion_Log = Motion_Log;
        }
        #endregion

        #region 检测错误代码（根据指令返回值判断错误）
        //返回值非0说明有错误，在《ZMotion PC函数手册》指令返回值表格查看详细说明
        private void CommandHandler(string strCmd, int sRtn)
        {
            string str = "";
            if (sRtn != 0)
            {
                str = string.Format("{0} return code is {1} ！", strCmd, sRtn);
                Motion_Log(str);

                if (sRtn == 20009)
                {
                    m_bInited = false;
                }

            }
        }
        #endregion

        #region 读取运动控制参数（配置文件路径"$\\data\\MoveParams.ini"）
        //（先把已有MoveParams.ini文件复制过来）
        public void ReadMoveParams()
        {

            //获取exe所在路径
            string str = System.Environment.CurrentDirectory + "\\";
            //运动参数文件路径
            string strFile = str + "data\\MoveParams.ini";

            //创建文件夹
            m_Tools.CreateAllDirectory(strFile);


            //[SYSTEM]
            //获取控制器IP
            ZMotion_IpAddr = m_Tools.GetPrivateProfileString("SYSTEM", "IPAddr", "192.168.0.11", strFile);
            m_Tools.WritePrivateProfileStringEx("SYSTEM", "IPAddr", ZMotion_IpAddr.ToString(), strFile);


            //[IN]
            //获取启动输入口端口号
            m_nIn_Start = m_Tools.GetPrivateProfileInt("In", "Start", m_nIn_Start, strFile);
            m_Tools.WritePrivateProfileStringEx("In", "Start", m_nIn_Start.ToString(), strFile);

            //获取停止输入口端口号
            m_nIn_Stop = m_Tools.GetPrivateProfileInt("In", "Stop", m_nIn_Stop, strFile);
            m_Tools.WritePrivateProfileStringEx("In", "Stop", m_nIn_Stop.ToString(), strFile);

            //获取急停输入口端口号
            m_nIn_EmergStop = m_Tools.GetPrivateProfileInt("In", "Emergency_Stop", m_nIn_EmergStop, strFile);
            m_Tools.WritePrivateProfileStringEx("In", "Stop", m_nIn_EmergStop.ToString(), strFile);


            //[OUT]
            //获取吸气输出口端口号
            m_nOut_Inhale = (short)m_Tools.GetPrivateProfileInt("Out", "Out_Inhale", m_nOut_Inhale, strFile);
            m_Tools.WritePrivateProfileStringEx("Out", "Out_Inhale", m_nOut_Inhale.ToString(), strFile);

            //获取LED光源输出口端口号
            m_nOut_LED = (short)m_Tools.GetPrivateProfileInt("Out", "m_nOut_LED", m_nOut_LED, strFile);
            m_Tools.WritePrivateProfileStringEx("Out", "m_nOut_LED", m_nOut_LED.ToString(), strFile);


            //[AxisIndex]
            //获取X轴轴号
            m_nAxis_X = (short)m_Tools.GetPrivateProfileInt("AxisIndex", "m_nAxis_X", m_nAxis_X, strFile);
            m_Tools.WritePrivateProfileStringEx("AxisIndex", "m_nAxis_X", m_nAxis_X.ToString(), strFile);

            //获取Y轴轴号
            m_nAxis_Y = (short)m_Tools.GetPrivateProfileInt("AxisIndex", "m_nAxis_Y", m_nAxis_Y, strFile);
            m_Tools.WritePrivateProfileStringEx("AxisIndex", "m_nAxis_Y", m_nAxis_Y.ToString(), strFile);

            //获取Z轴轴号
            m_nAxis_Z = (short)m_Tools.GetPrivateProfileInt("AxisIndex", "m_nAxis_Z", m_nAxis_Z, strFile);
            m_Tools.WritePrivateProfileStringEx("AxisIndex", "m_nAxis_Z", m_nAxis_Z.ToString(), strFile);

            //获取U轴轴号
            m_nAxis_U = (short)m_Tools.GetPrivateProfileInt("AxisIndex", "m_nAxis_U", m_nAxis_U, strFile);
            m_Tools.WritePrivateProfileStringEx("AxisIndex", "m_nAxis_U", m_nAxis_U.ToString(), strFile);


            //[Servo]
            //获取X轴使能输出口
            m_nAxis_ServoX = (short)m_Tools.GetPrivateProfileInt("Servo", "X", m_nAxis_ServoX, strFile);
            m_Tools.WritePrivateProfileStringEx("Servo", "X", m_nAxis_ServoX.ToString(), strFile);

            //获取Y轴使能输出口
            m_nAxis_ServoY = (short)m_Tools.GetPrivateProfileInt("Servo", "Y", m_nAxis_ServoY, strFile);
            m_Tools.WritePrivateProfileStringEx("Servo", "Y", m_nAxis_ServoY.ToString(), strFile);

            //获取Z轴使能输出口
            m_nAxis_ServoZ = (short)m_Tools.GetPrivateProfileInt("Servo", "Z", m_nAxis_ServoZ, strFile);
            m_Tools.WritePrivateProfileStringEx("Servo", "Z", m_nAxis_ServoZ.ToString(), strFile);

            //获取U轴使能输出口
            m_nAxis_ServoU = (short)m_Tools.GetPrivateProfileInt("Servo", "U", m_nAxis_ServoU, strFile);
            m_Tools.WritePrivateProfileStringEx("Servo", "U", m_nAxis_ServoU.ToString(), strFile);


            //[Units]
            //获取X轴脉冲当量       
            m_dAxis_X_Units = m_Tools.GetPrivateProfileDouble("Units", "X", m_dAxis_X_Units, strFile);
            m_Tools.WritePrivateProfileStringEx("Units", "X", m_dAxis_X_Units.ToString(), strFile);

            //获取Y轴脉冲当量 
            m_dAxis_Y_Units = m_Tools.GetPrivateProfileDouble("Units", "Y", m_dAxis_Y_Units, strFile);
            m_Tools.WritePrivateProfileStringEx("Units", "Y", m_dAxis_Y_Units.ToString(), strFile);

            //获取Z轴脉冲当量 
            m_dAxis_Z_Units = m_Tools.GetPrivateProfileDouble("Units", "Z", m_dAxis_Z_Units, strFile);
            m_Tools.WritePrivateProfileStringEx("Units", "Z", m_dAxis_Z_Units.ToString(), strFile);

            //获取U轴脉冲当量 
            m_dAxis_U_Units = m_Tools.GetPrivateProfileDouble("Units", "U", m_dAxis_U_Units, strFile);
            m_Tools.WritePrivateProfileStringEx("Units", "U", m_dAxis_U_Units.ToString(), strFile);


            //[HomeDir]
            //获取X轴回零方向
            m_nAxis_X_Home_Dir = m_Tools.GetPrivateProfileInt("HomeDir", "X", m_nAxis_X_Home_Dir, strFile);
            m_Tools.WritePrivateProfileStringEx("HomeDir", "X", m_nAxis_X_Home_Dir.ToString(), strFile);

            //获取Y轴回零方向
            m_nAxis_Y_Home_Dir = m_Tools.GetPrivateProfileInt("HomeDir", "Y", m_nAxis_Y_Home_Dir, strFile);
            m_Tools.WritePrivateProfileStringEx("HomeDir", "Y", m_nAxis_Y_Home_Dir.ToString(), strFile);

            //获取Z轴回零方向
            m_nAxis_Z_Home_Dir = m_Tools.GetPrivateProfileInt("HomeDir", "Z", m_nAxis_Z_Home_Dir, strFile);
            m_Tools.WritePrivateProfileStringEx("HomeDir", "Z", m_nAxis_Z_Home_Dir.ToString(), strFile);

            //获取U轴回零方向
            m_nAxis_U_Home_Dir = m_Tools.GetPrivateProfileInt("HomeDir", "U", m_nAxis_U_Home_Dir, strFile);
            m_Tools.WritePrivateProfileStringEx("HomeDir", "U", m_nAxis_U_Home_Dir.ToString(), strFile);


            //[Speed]
            //获取X轴速度、自动运行速度、回零运行速度
            m_dAxis_X_Speed = m_Tools.GetPrivateProfileDouble("Speed", "X", m_dAxis_X_Speed, strFile);
            m_Tools.WritePrivateProfileStringEx("Speed", "X", m_dAxis_X_Speed.ToString(), strFile);

            m_dAxis_X_Auto_Speed = m_Tools.GetPrivateProfileDouble("Speed", "X_Auto", m_dAxis_X_Auto_Speed, strFile);
            m_Tools.WritePrivateProfileStringEx("Speed", "X_Auto", m_dAxis_X_Auto_Speed.ToString(), strFile);

            m_dAxis_X_Home_Speed = m_Tools.GetPrivateProfileDouble("Speed", "X_Home", m_dAxis_X_Home_Speed, strFile);
            m_Tools.WritePrivateProfileStringEx("Speed", "X_Home", m_dAxis_X_Home_Speed.ToString(), strFile);

            //获取Y轴速度、自动运行速度、回零运行速度
            m_dAxis_Y_Speed = m_Tools.GetPrivateProfileDouble("Speed", "Y", m_dAxis_Y_Speed, strFile);
            m_Tools.WritePrivateProfileStringEx("Speed", "Y", m_dAxis_Y_Speed.ToString(), strFile);

            m_dAxis_Y_Auto_Speed = m_Tools.GetPrivateProfileDouble("Speed", "Y_Auto", m_dAxis_Y_Auto_Speed, strFile);
            m_Tools.WritePrivateProfileStringEx("Speed", "Y_Auto", m_dAxis_Y_Auto_Speed.ToString(), strFile);

            m_dAxis_Y_Home_Speed = m_Tools.GetPrivateProfileDouble("Speed", "Y_Home", m_dAxis_Y_Home_Speed, strFile);
            m_Tools.WritePrivateProfileStringEx("Speed", "Y_Home", m_dAxis_Y_Home_Speed.ToString(), strFile);

            //获取Z轴速度、自动运行速度、回零运行速度
            m_dAxis_Z_Speed = m_Tools.GetPrivateProfileDouble("Speed", "Z", m_dAxis_Z_Speed, strFile);
            m_Tools.WritePrivateProfileStringEx("Speed", "Z", m_dAxis_Z_Speed.ToString(), strFile);

            m_dAxis_Z_Auto_Speed = m_Tools.GetPrivateProfileDouble("Speed", "Z_Auto", m_dAxis_Z_Auto_Speed, strFile);
            m_Tools.WritePrivateProfileStringEx("Speed", "Z_Auto", m_dAxis_Z_Auto_Speed.ToString(), strFile);

            m_dAxis_Z_Home_Speed = m_Tools.GetPrivateProfileDouble("Speed", "Z_Home", m_dAxis_Z_Home_Speed, strFile);
            m_Tools.WritePrivateProfileStringEx("Speed", "Z_Home", m_dAxis_Z_Home_Speed.ToString(), strFile);

            //获取U轴速度、自动运行速度、回零运行速度
            m_dAxis_U_Speed = m_Tools.GetPrivateProfileDouble("Speed", "U", m_dAxis_U_Speed, strFile);
            m_Tools.WritePrivateProfileStringEx("Speed", "U", m_dAxis_U_Speed.ToString(), strFile);

            m_dAxis_U_Auto_Speed = m_Tools.GetPrivateProfileDouble("Speed", "U_Auto", m_dAxis_U_Auto_Speed, strFile);
            m_Tools.WritePrivateProfileStringEx("Speed", "U_Auto", m_dAxis_U_Auto_Speed.ToString(), strFile);

            m_dAxis_U_Home_Speed = m_Tools.GetPrivateProfileDouble("Speed", "U_Home", m_dAxis_U_Home_Speed, strFile);
            m_Tools.WritePrivateProfileStringEx("Speed", "U_Home", m_dAxis_U_Home_Speed.ToString(), strFile);


            //[Acc]
            //获取X轴加速度
            m_dAxis_X_Acc = m_Tools.GetPrivateProfileDouble("Acc", "X", m_dAxis_X_Acc, strFile);
            m_Tools.WritePrivateProfileStringEx("Acc", "X", m_dAxis_X_Acc.ToString(), strFile);

            //获取Y轴加速度
            m_dAxis_Y_Acc = m_Tools.GetPrivateProfileDouble("Acc", "Y", m_dAxis_Y_Acc, strFile);
            m_Tools.WritePrivateProfileStringEx("Acc", "Y", m_dAxis_Y_Acc.ToString(), strFile);

            //获取Z轴加速度
            m_dAxis_Z_Acc = m_Tools.GetPrivateProfileDouble("Acc", "Z", m_dAxis_Z_Acc, strFile);
            m_Tools.WritePrivateProfileStringEx("Acc", "Z", m_dAxis_Z_Acc.ToString(), strFile);

            //获取U轴加速度
            m_dAxis_U_Acc = m_Tools.GetPrivateProfileDouble("Acc", "U", m_dAxis_U_Acc, strFile);
            m_Tools.WritePrivateProfileStringEx("Acc", "U", m_dAxis_U_Acc.ToString(), strFile);


            //[HomeAndLimit]
            //获取X轴原点端口号
            m_nAxis_X_Home = m_Tools.GetPrivateProfileInt("HomeAndLimit", "m_nAxis_X_Home", 0, strFile);
            m_Tools.WritePrivateProfileStringEx("HomeAndLimit", "m_nAxis_X_Home", m_nAxis_X_Home.ToString(), strFile);

            //获取X轴的正限位端口号
            m_nXLimit_Positive = m_Tools.GetPrivateProfileInt("HomeAndLimit", "XLimit_Positive", 1, strFile);
            m_Tools.WritePrivateProfileStringEx("HomeAndLimit", "XLimit_Positive", m_nXLimit_Positive.ToString(), strFile);

            //获取X轴的负限位端口号
            m_nXLimit_Negative = m_Tools.GetPrivateProfileInt("HomeAndLimit", "XLimit_Negative", 3, strFile);
            m_Tools.WritePrivateProfileStringEx("HomeAndLimit", "XLimit_Negative", m_nXLimit_Negative.ToString(), strFile);

            //获取Y轴原点端口号
            m_nAxis_Y_Home = m_Tools.GetPrivateProfileInt("HomeAndLimit", "m_nAxis_Y_Home", 4, strFile);
            m_Tools.WritePrivateProfileStringEx("HomeAndLimit", "m_nAxis_Y_Home", m_nAxis_Y_Home.ToString(), strFile);

            //获取Y轴的正限位端口号
            m_nYLimit_Positive = m_Tools.GetPrivateProfileInt("HomeAndLimit", "YLimit_Positive", 2, strFile);
            m_Tools.WritePrivateProfileStringEx("HomeAndLimit", "YLimit_Positive", m_nYLimit_Positive.ToString(), strFile);

            //获取Y轴的负限位端口号
            m_nYLimit_Negative = m_Tools.GetPrivateProfileInt("HomeAndLimit", "YLimit_Negative", 5, strFile);
            m_Tools.WritePrivateProfileStringEx("HomeAndLimit", "YLimit_Negative", m_nYLimit_Negative.ToString(), strFile);

            //获取Z轴原点端口号
            m_nAxis_Z_Home = m_Tools.GetPrivateProfileInt("HomeAndLimit", "m_nAxis_Z_Home", 6, strFile);
            m_Tools.WritePrivateProfileStringEx("HomeAndLimit", "m_nAxis_Z_Home", m_nAxis_Z_Home.ToString(), strFile);

            //获取Z轴的正限位端口号
            m_nZLimit_Positive = m_Tools.GetPrivateProfileInt("HomeAndLimit", "ZLimit_Positive", 7, strFile);
            m_Tools.WritePrivateProfileStringEx("HomeAndLimit", "ZLimit_Positive", m_nZLimit_Positive.ToString(), strFile);

            //获取Z轴的负限位端口号
            m_nZLimit_Negative = m_Tools.GetPrivateProfileInt("HomeAndLimit", "ZLimit_Negative", 8, strFile);
            m_Tools.WritePrivateProfileStringEx("HomeAndLimit", "ZLimit_Negative", m_nZLimit_Negative.ToString(), strFile);

            //获取U轴原点端口号
            m_nAxis_U_Home = m_Tools.GetPrivateProfileInt("HomeAndLimit", "m_nAxis_U_Home", 9, strFile);
            m_Tools.WritePrivateProfileStringEx("HomeAndLimit", "m_nAxis_U_Home", m_nAxis_U_Home.ToString(), strFile);

        }

        #endregion

        #region 初始化运动控制卡
        public bool InitCard()
        {
            //读取运动控制参数
            ReadMoveParams();

            //定义运动控制指令返回值
            int sRtn;

            //打开运动控制器(ip"192.168.0.11" 正运动控制卡 ; ip"127.0.0.1" 正运动仿真器(可在ZDevelop调试软件中打开))
            //若句柄 g_handle 为0说明链接控制卡失败,返回值sRtn为非0错误码。
            //ZMotion_IpAddr = "192.168.0.11"; //"127.0.0.1"
            if (g_handle == (IntPtr)0)
            {
                sRtn = zmcaux.ZAux_OpenEth(ZMotion_IpAddr, out g_handle); CommandHandler("ZAux_OpenEth", sRtn);
                if (g_handle != (IntPtr)0)
                {
                    Motion_Log("控制器连接成功!");
                }
                else
                {
                    Motion_Log("控制器链接失败，请检测IP地址!");
                    return false;
                }
            }
            
            //设置轴类型: 1-脉冲类型
            sRtn = zmcaux.ZAux_Direct_SetAtype(g_handle, m_nAxis_X, 1); CommandHandler("SetAtypeX", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetAtype(g_handle, m_nAxis_Y, 1); CommandHandler("SetAtypeY", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetAtype(g_handle, m_nAxis_Z, 1); CommandHandler("SetAtypeZ", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetAtype(g_handle, m_nAxis_U, 1); CommandHandler("SetAtypeU", sRtn);

            //设置脉冲输出模式: 改变电机方向。注意正负限位和回零方向要与电机方向一致。
            sRtn = zmcaux.ZAux_Direct_SetInvertStep(g_handle, m_nAxis_X, 2); CommandHandler("SetInvertStepX", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetInvertStep(g_handle, m_nAxis_Y, 2); CommandHandler("SetInvertStepY", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetInvertStep(g_handle, m_nAxis_Z, 2); CommandHandler("SetInvertStepZ", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetInvertStep(g_handle, m_nAxis_U, 0); CommandHandler("SetInvertStepU", sRtn);

            //设置加减速S曲线: 平滑加减速过程,减少机台抖动。一般设100ms不用变动。
            sRtn = zmcaux.ZAux_Direct_SetSramp(g_handle, m_nAxis_X, 100); CommandHandler("SetSrampX", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetSramp(g_handle, m_nAxis_Y, 100); CommandHandler("SetSrampY", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetSramp(g_handle, m_nAxis_Z, 100); CommandHandler("SetSrampZ", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetSramp(g_handle, m_nAxis_U, 100); CommandHandler("SetSrampU", sRtn);

            //设置脉冲当量
            sRtn = zmcaux.ZAux_Direct_SetUnits(g_handle, m_nAxis_X, (float)m_dAxis_X_Units); CommandHandler("SetUnitsX", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetUnits(g_handle, m_nAxis_Y, (float)m_dAxis_Y_Units); CommandHandler("SetUnitsY", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetUnits(g_handle, m_nAxis_Z, (float)m_dAxis_Z_Units); CommandHandler("SetUnitsZ", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetUnits(g_handle, m_nAxis_U, (float)m_dAxis_U_Units); CommandHandler("SetUnitsU", sRtn);

            //配置特殊输入口: 负限位、正限位、原点、轴报警
            sRtn = SetLimitAndHome();

            //初始化速度参数
            sRtn = zmcaux.ZAux_Direct_SetSpeed(g_handle, m_nAxis_X, (float)m_dAxis_X_Speed); CommandHandler("SetSpeedX", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetSpeed(g_handle, m_nAxis_Y, (float)m_dAxis_Y_Speed); CommandHandler("SetSpeedY", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetSpeed(g_handle, m_nAxis_Z, (float)m_dAxis_Z_Speed); CommandHandler("SetSpeedZ", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetSpeed(g_handle, m_nAxis_U, (float)m_dAxis_U_Speed); CommandHandler("SetSpeedU", sRtn);

            //初始化加减速度参数: 减速度为0时表示与加速度相同
            sRtn = zmcaux.ZAux_Direct_SetAccel(g_handle, m_nAxis_X, (float)m_dAxis_X_Acc); CommandHandler("SetAccelX", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetAccel(g_handle, m_nAxis_Y, (float)m_dAxis_Y_Acc); CommandHandler("SetAccelY", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetAccel(g_handle, m_nAxis_Z, (float)m_dAxis_Z_Acc); CommandHandler("SetAccelZ", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetAccel(g_handle, m_nAxis_U, (float)m_dAxis_U_Acc); CommandHandler("SetAccelU", sRtn);

            sRtn = zmcaux.ZAux_Direct_SetDecel(g_handle, m_nAxis_X, 0); CommandHandler("SetDecelX", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetDecel(g_handle, m_nAxis_Y, 0); CommandHandler("SetDecelY", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetDecel(g_handle, m_nAxis_Z, 0); CommandHandler("SetDecelZ", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetDecel(g_handle, m_nAxis_U, 0); CommandHandler("SetDecelU", sRtn);

            //返回结果：如果运动控制指令返回值为0，初始化成功
            return sRtn == 0 ? m_bInited = true : m_bInited = false;

        }
        #endregion

        #region 配置特殊输入口
        public int SetLimitAndHome()
        {
            //定义运动控制指令返回值
            int sRtn;
            
            //所有轴的报警信号AlmIn可以统一设为急停按钮对应的输入口序号；-1表示该信号没有对应的硬件输入口

            //设置接在特殊输入口上的按钮或传感器的极性为默认常开
            StringBuilder psResponse = new StringBuilder(10240);
            sRtn = zmcaux.ZAux_Execute(g_handle, "Sys_IOSet = 2", psResponse, 2048); CommandHandler("Sys_IOSet", sRtn);

            //设置X轴的原点与正负限位和报警信号对应的硬件输入口序号
            sRtn = zmcaux.ZAux_Direct_SetDatumIn(g_handle, m_nAxis_X, m_nAxis_X_Home); CommandHandler("SetXDatumIn", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetFwdIn(g_handle, m_nAxis_X, m_nXLimit_Positive); CommandHandler("SetXFwdIn", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetRevIn(g_handle, m_nAxis_X, m_nXLimit_Negative); CommandHandler("SetXRevIn", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetAlmIn(g_handle, m_nAxis_X, -1); CommandHandler("SetXAlmIn", sRtn);

            //设置Y轴的原点与正负限位和报警信号对应的硬件输入口序号
            sRtn = zmcaux.ZAux_Direct_SetDatumIn(g_handle, m_nAxis_Y, m_nAxis_Y_Home); CommandHandler("SetYDatumIn", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetFwdIn(g_handle, m_nAxis_Y, m_nYLimit_Positive); CommandHandler("SetYFwdIn", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetRevIn(g_handle, m_nAxis_Y, m_nYLimit_Negative); CommandHandler("SetYRevIn", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetAlmIn(g_handle, m_nAxis_Y, -1); CommandHandler("SetYAlmIn", sRtn);

            //设置Z轴的原点与正负限位和报警信号对应的硬件输入口序号 
            sRtn = zmcaux.ZAux_Direct_SetDatumIn(g_handle, m_nAxis_Z, m_nAxis_Z_Home); CommandHandler("SetZDatumIn", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetFwdIn(g_handle, m_nAxis_Z, m_nZLimit_Positive); CommandHandler("SetZFwdIn", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetRevIn(g_handle, m_nAxis_Z, m_nZLimit_Negative); CommandHandler("SetZRevIn", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetAlmIn(g_handle, m_nAxis_Z, -1); CommandHandler("SetZAlmIn", sRtn);

            //设置U轴的原点与正负限位和报警信号对应的硬件输入口序号   
            sRtn = zmcaux.ZAux_Direct_SetDatumIn(g_handle, m_nAxis_U, m_nAxis_U_Home); CommandHandler("SetZDatumIn", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetFwdIn(g_handle, m_nAxis_U, -1); CommandHandler("SetZFwdIn", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetRevIn(g_handle, m_nAxis_U, -1); CommandHandler("SetZRevIn", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetAlmIn(g_handle, m_nAxis_U, -1); CommandHandler("SetUAlmIn", sRtn);

            return sRtn;

        }
        #endregion



        #region 单轴使能
        //nAxis=轴号, nState=使能开关, [nServoOutNum=该轴使能输出口]
        public void Axis_Servo(int nAxis, bool nState, int nServoOutNum = 0)
        {
            if (!m_bInited)
                return;

            //根据轴号确定对应的输出口
            int iOutNum;
            if (nAxis == m_nAxis_X)
            {
                iOutNum = m_nAxis_ServoX;
            }
            else if (nAxis == m_nAxis_Y)
            {
                iOutNum = m_nAxis_ServoY;
            }
            else if (nAxis == m_nAxis_Z)
            {
                iOutNum = m_nAxis_ServoZ;
            }
            else if (nAxis == m_nAxis_U)
            {
                iOutNum = m_nAxis_ServoU;
            }
            else
                iOutNum = nServoOutNum;
            
            
            uint iState;
            if (nState == true)
                iState = 1;
            else
                iState = 0;

            //打开或关闭使能输出口
            int sRtn = zmcaux.ZAux_Direct_SetOp(g_handle, iOutNum, iState);CommandHandler("Axis_Servo", sRtn);
        }

        #endregion

        #region 单轴回零运动
        //nAxis=轴号,dir=回零方向,HomeStatus=回零完成状态, [dVel=回零速度], [dAcc=回零加速度]
        public void Axis_HomeMove(int nAxis, int dir, double dVel = 10,double dAcc = 100)
        {

            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;
            
            //定义函数返回值 
            int sRtn;
            //回零前设置好回零时的速度、爬行速度、加减速度
            sRtn = zmcaux.ZAux_Direct_SetSpeed(g_handle, nAxis, (float)dVel); CommandHandler("Axis_HomeMove SetSpeed", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetAccel(g_handle, nAxis, (float)dAcc); CommandHandler("Axis_HomeMove SetAccel", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetCreep(g_handle,nAxis, (float)(dVel / 5)); CommandHandler("Axis_HomeMove SetCreep", sRtn);

            //根据回零方向选择回零模式 常用13(正向回0,遇正限位后反找) 或14(负向回0,遇负限位后反找)
            if (dir >= 0)
                dir = 13;
            else
                dir = 14;
            sRtn = zmcaux.ZAux_Direct_Single_Datum(g_handle,nAxis, dir); CommandHandler("Axis_HomeMove", sRtn);
            Motion_Log(string.Format("轴{0}{1}向回零中...", nAxis, (dir==13) ? "正":"负"));
        }
        #endregion

        #region 单轴回零完成检测
        public bool Axis_HomeStatus(int nAxis)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return false;

            //等待轴停止
            Axis_WaitStop(nAxis);

            //定义运动控制指令返回值
            int sRtn;

            //定义轴回零状态
            uint nHomeStatus = 0;

            //读取轴回零状态
            sRtn = zmcaux.ZAux_Direct_GetHomeStatus(g_handle, nAxis, ref nHomeStatus); CommandHandler("Axis_HomeStatus", sRtn);

            //轴回零完成状态 0-回零异常, 1-回零正常
            return (nHomeStatus == 1);
        }
        #endregion

        #region 单轴Jog运动
        //nAxis=轴号，dir=JOG方向，[dVel=JOG速度], [dAcc=JOG加速度]
        public void Axis_JogMove(int nAxis, int dir, double dVel = 10, double dAcc = 100)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;

            //定义运动控制指令返回值
            int sRtn;

            //设置Jog运动速度参数
            sRtn = zmcaux.ZAux_Direct_SetSpeed(g_handle, nAxis, (float)dVel); CommandHandler("Axis_JogMove SetSpeed", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetAccel(g_handle, nAxis, (float)dAcc); CommandHandler("Axis_JogMove SetAccel", sRtn);

            //正向JOG dir = 1;负向JOG dir = -1  
            if (dir >= 0)
                dir = 1;
            else
                dir = -1;
            sRtn = zmcaux.ZAux_Direct_Single_Vmove(g_handle, nAxis, dir); CommandHandler("Axis_JogMove", sRtn);
            Motion_Log(string.Format("轴{0}{1}向JOG中...", nAxis, (dir == 1) ? "正" : "负"));

        }
        #endregion

        #region 单轴相对点位运动
        //nAxis轴号，dPos运动距离，dVel速度
        public void Axis_Move(int nAxis, double dPos, double dVel = 10, double dAcc = 100)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;

            //定义运动控制指令返回值
            int sRtn;

            //设置相对运动速度参数
            sRtn = zmcaux.ZAux_Direct_SetSpeed(g_handle, nAxis, (float)dVel); CommandHandler("Axis_Move SetSpeed", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetAccel(g_handle, nAxis, (float)dAcc); CommandHandler("Axis_Move SetAccel", sRtn);

            //启动点位运动
            sRtn = zmcaux.ZAux_Direct_Single_Move(g_handle, nAxis, (float)dPos); CommandHandler("Axis_Move", sRtn);
            Motion_Log(string.Format("轴{0}寸动距离{1}", nAxis, (float)dPos));
        }
        #endregion

        #region 单轴绝对点位运动
        //nAxis=轴号，dPos=运动坐标，dVel=速度, dAcc=加速度
        public void Axis_MoveAbs(int nAxis, double dPos, double dVel = 10, double dAcc = 100)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;

            //定义运动控制指令返回值
            int sRtn;

            //设置相对运动速度参数
            sRtn = zmcaux.ZAux_Direct_SetSpeed(g_handle, nAxis, (float)dVel); CommandHandler("Axis_Move SetSpeed", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetAccel(g_handle, nAxis, (float)dAcc); CommandHandler("Axis_Move SetAccel", sRtn);

            //启动点位运动
            sRtn = zmcaux.ZAux_Direct_Single_MoveAbs(g_handle, nAxis, (float)dPos); CommandHandler("Axis_MoveAbs", sRtn);
            Motion_Log(string.Format("轴{0}运动到位置{1}", nAxis, (float)dPos));
        }
        #endregion

        #region 单轴规划位置清零
        public void Axis_ZeroPos(int nAxis)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;

            //定义运动控制指令返回值
            int sRtn;
            //清零规划位置DPOS 和 实际位MPOS
            sRtn = zmcaux.ZAux_Direct_SetDpos(g_handle, nAxis, 0); CommandHandler("Axis_ZeroPos Dpos", sRtn);
            sRtn = zmcaux.ZAux_Direct_SetMpos(g_handle, nAxis, 0); CommandHandler("Axis_ZeroPos Mpos", sRtn);
            Motion_Log(string.Format("轴{0}位置已清0!", nAxis));
        }
        #endregion

        #region 单轴停止运动
        public void Axis_StopMove(int nAxis)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;

            //停止单轴的规划运动
            //imode模式0：取消轴的当前正在执行运动
            //imode模式1：取消轴的缓冲等待执行运动
            //imode模式2：取消轴的所有运动
            int sRtn = zmcaux.ZAux_Direct_Single_Cancel(g_handle, nAxis, 2); CommandHandler("Axis_StopMove", sRtn);
            Motion_Log(string.Format("轴{0}已停止", nAxis));
        }
        #endregion

        #region 获取单轴规划位置
        public double Axis_GetCmdPos(int nAxis)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return -99999;

            //定义规划位置
            float prfPosf = 0;

            //读取规划位置
            int sRtn = zmcaux.ZAux_Direct_GetDpos(g_handle, nAxis, ref prfPosf); CommandHandler("Axis_GetCmdPos", sRtn);

            //返回规划位置
            return prfPosf;
        }
        #endregion

        #region 获取单轴反馈位置
        public double Axis_GetEncPos(int nAxis)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return -999999;

            //定义编码器位置
            float encPosf = 0;

            int sRtn = zmcaux.ZAux_Direct_GetMpos(g_handle, nAxis, ref encPosf); CommandHandler("Axis_GetEncPos", sRtn);

            //返回位置
            return encPosf;
        }
        #endregion

        #region 获取单轴当前状态
        //当前轴状态
        public int Axis_Status(int nAxis)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return -1;

            //定义轴状态
            int iAixsStatus = 0;

            //读取轴状态
            int sRtn = zmcaux.ZAux_Direct_GetAxisStatus(g_handle, nAxis, ref iAixsStatus); CommandHandler("Axis_Status", sRtn);

            //轴运动状态Idle 0-运动中, -1-无运动
            return iAixsStatus;
        }
        #endregion

        #region 获取单轴运动状态
        //轴运动状态
        public bool Axis_IsMoving(int nAxis)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return false;

            //定义轴运动状态
            int nAixsIfidle = 0;

            //读取轴状态
            int sRtn = zmcaux.ZAux_Direct_GetIfIdle(g_handle, nAxis, ref nAixsIfidle); CommandHandler("Axis_IsMoving", sRtn);

            //轴运动状态Idle nAixsIfidle = 0-运动中, -1-无运动
            return (nAixsIfidle == 0);

        }
        #endregion

        #region 等待单轴运动停止
        public void Axis_WaitStop(int nAxis)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;

            do
            {
                //处理当前在消息队列中的所有 Windows 消息。
                //交出CPU控制权，让系统可以处理队列中的所有Windows消息. 
                //比如在大运算量循环内，加Application.DoEvents()可以防止界面停止响应
                Application.DoEvents();
            }
            while (Axis_IsMoving(nAxis) == true);
        }
        #endregion



        #region 整机回零运动
        public void AllAxis_Home()
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;

            Motion_Log("整机开始回零...");

            //Z轴先复位
            Axis_HomeMove(m_nAxis_Z, m_nAxis_Z_Home_Dir, m_dAxis_Z_Home_Speed, m_dAxis_Z_Acc);
            //判断Z回零是否正常完成
            if (Axis_HomeStatus(m_nAxis_Z) == false)
            {
                Motion_Log("Z轴回零失败!");
                return;
            }

            //XY轴复位
            Axis_HomeMove(m_nAxis_X, m_nAxis_X_Home_Dir, m_dAxis_X_Home_Speed, m_dAxis_X_Acc);
            Axis_HomeMove(m_nAxis_Y, m_nAxis_Y_Home_Dir, m_dAxis_Y_Home_Speed, m_dAxis_Y_Acc);
            //判断XY回零是否正常完成
            if (Axis_HomeStatus(m_nAxis_X) == false)
            {
                Motion_Log("X轴回零失败!");
                return;
            }
            if (Axis_HomeStatus(m_nAxis_Y) == false)
            {
                Motion_Log("Y轴回零失败!");
                return;
            }

            //U轴为零
            Axis_HomeMove(m_nAxis_U, m_nAxis_U_Home_Dir, m_dAxis_U_Home_Speed, m_dAxis_U_Acc);
            //判断U回零是否正常完成
            if (Axis_HomeStatus(m_nAxis_U) == false)
            {
                Motion_Log("U轴回零失败!");
                return;
            }

            //复位成功
            Motion_Log("整机回零完成!");
            m_bHomed = true;

        }
        #endregion

        #region X、Y轴点位运动
        public void Move2D(double[] dPos, double[] dVel)
    {
        //判断控制卡是否初始化成功
        if (!m_bInited)
            return;
        //x轴运动
        Axis_MoveAbs(2, dPos[0], dVel[0], m_dAxis_X_Acc);
        //y轴运动
        Axis_MoveAbs(1, dPos[1], dVel[1], m_dAxis_Y_Acc);
        //等待停止
        Axis_WaitStop(m_nAxis_X);
        Axis_WaitStop(m_nAxis_Y);
            
    }
    #endregion

        #region X、Y、Z轴点位运动
        //运动顺序：为了防止撞机，z轴先运动到安全位置，然后xy轴运动；xy运动到位，再z运动
        public void Move3D(double[] dPos, double[] dVel, double dZSafePos = 0)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;

            //Z轴先到安全位置
            Axis_MoveAbs(m_nAxis_Z, dZSafePos, dVel[2], m_dAxis_Z_Acc);
            //等待停止
            Axis_WaitStop(m_nAxis_Z);

            //运动X、Y轴
            Axis_MoveAbs(m_nAxis_X, dPos[0], dVel[0], m_dAxis_X_Acc);
            Axis_MoveAbs(m_nAxis_Y, dPos[1], dVel[1], m_dAxis_Y_Acc);
            //等待停止
            Axis_WaitStop(m_nAxis_X);
            Axis_WaitStop(m_nAxis_Y);

            //运动Z轴
            Axis_MoveAbs(m_nAxis_Z, dPos[2], dVel[2], m_dAxis_Z_Acc);
            Axis_WaitStop(m_nAxis_Z);
        }
        #endregion

        #region X、Y、Z、U轴点位运动
        //运动顺序：为了防止撞机，z轴先运动到安全位置，u轴运动，然后xy轴运动；xy运动到位，再z运动
        public void Move4D(double[] dPos, double[] dVel, double dZSafePos = 0)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;

            //Z轴先到安全位置
            Axis_MoveAbs(m_nAxis_Z, dZSafePos, dVel[2], m_dAxis_Z_Acc);
            Axis_WaitStop(m_nAxis_Z);
            //运动U轴
            Axis_MoveAbs(m_nAxis_U, dPos[3], dVel[3], m_dAxis_U_Acc);
            Axis_WaitStop(m_nAxis_U);

            //运动X、Y轴
            Axis_MoveAbs(m_nAxis_X, dPos[0], dVel[0], m_dAxis_X_Acc);
            Axis_MoveAbs(m_nAxis_Y, dPos[1], dVel[1], m_dAxis_Y_Acc);
            Axis_WaitStop(m_nAxis_X);
            Axis_WaitStop(m_nAxis_Y);

            //运动Z轴
            Axis_MoveAbs(m_nAxis_Z, dPos[2], dVel[2], m_dAxis_Z_Acc);
            Axis_WaitStop(m_nAxis_Z);
        }
        #endregion

        #region 停止所有轴运动
        public void AllAxis_StopMove()
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;

            //停止所有轴的规划运动
            //imode模式0：取消所有轴的当前正在执行运动
            //imode模式1：取消所有轴的缓冲等待执行运动
            //imode模式2：取消所有轴的所有运动
            int sRtn = zmcaux.ZAux_Direct_Rapidstop(g_handle, 2); CommandHandler("ZAux_Direct_Rapidstop", sRtn);
            AllAxis_WaitStop();
            Motion_Log("所有轴已停止!");
        }
        #endregion

        #region 等待所有轴运动停止
        public void AllAxis_WaitStop()
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;

            Axis_WaitStop(m_nAxis_X);
            Axis_WaitStop(m_nAxis_Y);
            Axis_WaitStop(m_nAxis_Z);
            Axis_WaitStop(m_nAxis_U);

        }
        #endregion

        #region 暂停所有轴运动
        public void AllAxis_PauseMove()
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;

            //判断轴是否暂停
            if (m_bPaused == true)
                return;

            //暂停所有轴的当前运动
            int sRtn = 0;
            sRtn = zmcaux.ZAux_Direct_MovePause(g_handle,m_nAxis_X,3); CommandHandler("MovePauseX", sRtn);
            sRtn = zmcaux.ZAux_Direct_MovePause(g_handle,m_nAxis_Y,3); CommandHandler("MovePauseY", sRtn);
            sRtn = zmcaux.ZAux_Direct_MovePause(g_handle,m_nAxis_Z,3); CommandHandler("MovePauseZ", sRtn);
            sRtn = zmcaux.ZAux_Direct_MovePause(g_handle,m_nAxis_U,3); CommandHandler("MovePauseU", sRtn);
            AllAxis_WaitStop();

            Motion_Log("所有轴已暂停!");
            m_bPaused = true;

        }
        #endregion

        #region 恢复所有轴运动
        public void AllAxis_ResumeMove()
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;

            //判断轴是否暂停
            if (m_bPaused == false)
                return;

            //恢复所有轴的缓存运动
            int sRtn = 0;
            sRtn = zmcaux.ZAux_Direct_MoveResume(g_handle, m_nAxis_X); CommandHandler("MoveResumeX", sRtn);
            sRtn = zmcaux.ZAux_Direct_MoveResume(g_handle, m_nAxis_Y); CommandHandler("MoveResumeY", sRtn);
            sRtn = zmcaux.ZAux_Direct_MoveResume(g_handle, m_nAxis_Z); CommandHandler("MoveResumeZ", sRtn);
            sRtn = zmcaux.ZAux_Direct_MoveResume(g_handle, m_nAxis_U); CommandHandler("MoveResumeU", sRtn);

            Motion_Log("所有轴运动已恢复!");
            m_bPaused = false;

        }
        #endregion


        #region 获取单个输入口状态
        public bool GetInput(int nIndex)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return false;

            //IO输入状态
            uint pValue = 0;

            //读取单个输入口状态(0-OFF 1-ON)
            int sRtn = zmcaux.ZAux_Direct_GetIn(g_handle, nIndex, ref pValue); CommandHandler("GetInput", sRtn);
            
            return (pValue == 1);
        }
        #endregion

        #region 获取单个输出口状态
        public bool GetOutput(int nIndex)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return false;

            //IO输入状态
            uint pValue = 0;

            //读取单个输入口状态(0-OFF 1-ON)
            int sRtn = zmcaux.ZAux_Direct_GetOp(g_handle, nIndex, ref pValue); CommandHandler("GetOutput", sRtn);

            return (pValue == 1);
        }
        #endregion

        #region 设置单个输出口状态
        public void SetOutput(int nIndex, bool bOpen)
        {
            //判断控制卡是否初始化成功
            if (!m_bInited)
                return;

            uint nVal = (uint)(bOpen ? 0 : 1);

            //读取单个输入口状态(0-OFF 1-ON)
            int sRtn = zmcaux.ZAux_Direct_SetOp(g_handle, nIndex, nVal); CommandHandler("SetOutput", sRtn);
        }
        #endregion

       
    }
}
