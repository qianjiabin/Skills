using cszmcaux;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Motion
{
    public class Function
    {

        #region 字段
   
        private float clutchRate = 1000000;    //连接速率

        private int tableStaraId = 1000;  //机械手臂长参数table参数起始编号

        private int tcpTableId = 120;   //机械手工件坐标系table参数起始编号

        private int mtype = 0;  //当前运动类型

      

        #region 文件变量

        private string LpAppname = "示教点";

        private string Lpkeynum = "pos";

        #endregion

        #endregion

        #region 数组

        //*************************************************************/
        //临时转换参数

        private float[] temp = new float[4];

        //*************************************************************/
        //关节轴参数

        private int[] jAxisList = new int[4];  //关节轴轴号

        private float[] jOrigiNum = new float[4]; //关节轴原点对应的io编号

        private float[] jOrigiInvert = new float[4]; //关节轴原点对应的io编号

        private float[] jNumPulses = new float[4]; //电机的一圈脉冲数

        private float[] jReducRatio = new float[4]; //电机减速比

        private int[] axisDir = new int[4]; //电机方向

        private float[] f_Limit = new float[4]; //轴的正软限位

        private float[] r_Limit = new float[4]; //轴的负软限位

        private float[] l_para = new float[7]; //上间距半径 下间距半径 上杆长度 下杆长度 末端与下方中心点xyz偏移

        private float[] units = new float[4]; //关节轴脉冲当量

        private float[] DeltaPara = new float[11]; //Delta机械手table参数

        private float[] ConversionValue = new float[6]; //Delta机械手坐标系转换输出后的参数


        //*************************************************************/
        //直角坐标系参数

        public int[] vAxisList = new int[4] { 5, 6, 7, 8 };  //x，y，z，u轴号

        //*************************************************************/
        //示教点位坐标参数

        private int[] idNum = new int[100];  //坐标点编号

        private string[] name = new string[100];  //坐标点名称

        private float[,] posL = new float[100, 4]; //直角坐标

        private float[,] posJ = new float[100, 4]; //关节坐标

        #endregion

        #region 实例化对象

        public IniFile IniFile = new IniFile("DeltaPos");

        #endregion

        #region 结构体

       

        #endregion
  
        #region 属性


        private IntPtr intPtr = IntPtr.Zero;  //控制器连接句柄
        public IntPtr IntPtr
        {
            get { return intPtr; }

            set { intPtr = value; }          
        }

        #region 不可用属性
            private int sign = 0;   //更新界面参数 ,不可使用该属性
            public int Sign
            {
                get { return sign; }

                set { sign = value; }
            }

            private int logStatus = 0;   //更新界面参数 ,不可使用该属性
            public int LogStatus
            {
                get { return logStatus; }

                set { logStatus = value; }
            }
        #endregion

        #endregion

        ///////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////

        //返回值大于0，为函数接口错误码，具体参考正运动错误码列表  返回值为-1，具体看逻辑错误或者运动条件不满足 ！！！！！！！！！！！！！！！！！！！！

        ///////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////

        #region 初始化函数模块


        /// <summary>
        /// 连接控制器
        /// </summary>
        /// <param name="ip">IP地址</param>
        /// <returns></returns>
        public int Connect(string ip)  
        {
            int _res = zmcaux.ZAux_OpenEth(ip, out intPtr);

            if (_res != 0)
            {
                return _res;
            }

            return 0;
        }


        /// <summary>
        /// 断开控制器
        /// </summary>
        /// <returns></returns>
        public int Close()
        {
            int _res = zmcaux.ZAux_Close(intPtr);

            if (_res != 0)
            {
                return _res;
            }

            return 0;
        }


        /// <summary>
        /// 参数初始化
        /// </summary>
        /// <returns></returns>
        public int Initialize()   
        {
            int _res;

            if (intPtr == IntPtr.Zero)
            {
                return -1;
            }
   
            _res = zmcaux.ZAux_Direct_GetVrf(intPtr, 0, 4, temp);     //读取关节轴编号
            if (_res != 0)
            {
                return _res;
            }
            jAxisList = Array.ConvertAll<float, int>(temp, (x) => {
                return Convert.ToInt32(x);
            }) ;


            _res = zmcaux.ZAux_Direct_GetVrf(intPtr, 10, 4, jOrigiNum);     //读取原点传感器信号配置
            if (_res != 0)
            {
                return _res;
            }

            _res = zmcaux.ZAux_Direct_GetVrf(intPtr, 20, 4, jNumPulses);     //读取电机的一圈脉冲数
            if (_res != 0)
            {
                return _res;
            }

            _res = zmcaux.ZAux_Direct_GetVrf(intPtr, 30, 4, jReducRatio);     //读取电机减速比
            if (_res != 0)
            {
                return _res;
            }

            _res = zmcaux.ZAux_Direct_GetVrf(intPtr, 40, 4, temp);     //读取电机方向
            if (_res != 0)
            {
                return _res;
            }
            else
            {
                axisDir = Array.ConvertAll<float, int>(temp, (x) => {
                    return Convert.ToInt32(x);
                });

                for (int i = 0; i < 4; i++)
                {
                    if (axisDir[i] == 0)
                    {
                        axisDir[i] = 1;
                    }
                    else if (axisDir[i] == 1)
                    {
                        axisDir[i] = -1;
                    }
                }
            }

            _res = zmcaux.ZAux_Direct_GetVrf(intPtr, 70, 4, f_Limit);     //读取软件正限位
            if (_res != 0)
            {
                return _res;
            }

            _res = zmcaux.ZAux_Direct_GetVrf(intPtr, 80, 4, r_Limit);     //读取软件负限位
            if (_res != 0)
            {
                return _res;
            }

            _res = zmcaux.ZAux_Direct_GetVrf(intPtr, 90, 7, l_para);     //读取上间距半径 下间距半径 上杆长度 下杆长度 末端与下方中心点xyz偏移
            if (_res != 0)
            {
                return _res;
            }

            _res = zmcaux.ZAux_Direct_GetVrf(intPtr, 100, 4, jOrigiInvert);     //读取上间距半径 下间距半径 上杆长度 下杆长度 末端与下方中心点xyz偏移
            if (_res != 0)
            {
                return _res;
            }


            _res = zmcaux.ZAux_Direct_GetVrf(intPtr, 120, 6, ConversionValue);    //Delta机械手坐标系转换参数获取
            if (_res != 0)
            {
                return _res;
            }

            _res = SetControPara();  //设置机械参数
            if (_res != 0)
            {
                return _res;
            }

            _res = SetDeltaPara();  //设置Delta参数
            if (_res != 0)
            {
                return _res;
            }

            _res = getpos();  //获取Delta示教坐标点数据
            if (_res != 0)
            {
                return _res;
            }

            _res = Merge(1);  //开启连续插补
            if (_res != 0)
            {
                return _res;
            }

            return 0;

        }

        /// <summary>
        /// 设置机械手参数
        /// </summary>
        /// <returns></returns>
        private int SetControPara()
        {
            int _res;

            zmcaux.ZAux_Direct_Rapidstop(intPtr, 2);
            
            ///////////////////////////////////
            //设置关节轴参数
            for (int i = 0; i < 4; i++)    
            {

                if(jReducRatio[i] !=0 )    //设置关节轴脉冲当量，默认换算后单位为角度
                {
                    units[i]= jNumPulses[i] * jReducRatio[i] / 360;
                   
                    _res = zmcaux.ZAux_Direct_SetUnits(intPtr,jAxisList[i], units[i]);
                    if (_res != 0)
                    {
                        return _res;
                    }
                }
                else   
                {
                    units[i] = jNumPulses[i] / 360;

                    _res = zmcaux.ZAux_Direct_SetUnits(intPtr, jAxisList[i], units[i]);
                    if (_res != 0)
                    {
                        return _res;
                    }
                }


                if (i == 3)
                {
                    _res = zmcaux.ZAux_Direct_SetAtype(intPtr, jAxisList[i], 1);
                    if (_res != 0)
                    {
                        return _res;
                    }
                }
                else
                {
                    _res = zmcaux.ZAux_Direct_SetAtype(intPtr, jAxisList[i], 1);
                    if (_res != 0)
                    {
                        return _res;
                    }
                }

                _res = zmcaux.ZAux_Direct_SetDatumIn(intPtr, jAxisList[i], Convert.ToInt32(jOrigiNum[i]));   //设置关节轴原点编号
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetInvertIn(intPtr, Convert.ToInt32(jOrigiNum[i]), Convert.ToInt32(jOrigiInvert[i]));  //设置关节轴原点编号反转状态
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetAccel(intPtr, jAxisList[i], 100);      //设置关节轴加速度
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetDecel(intPtr, jAxisList[i], 100);      //设置关节轴减速度
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetSpeed(intPtr, jAxisList[i], 10);       //设置关节轴速度
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetSramp(intPtr, jAxisList[i], 100);      //设置s曲线加减速
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetFsLimit(intPtr, jAxisList[i], f_Limit[i]);    //设置正向软限位
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetRsLimit(intPtr, jAxisList[i], r_Limit[i]);    //设置负向软限位
                if (_res != 0)
                {
                    return _res;
                }

                if (i == 3)
                {
                    _res = zmcaux.ZAux_Direct_SetInvertStep(intPtr, jAxisList[i], 2);
                    if (_res != 0)
                    {
                        return _res;
                    }
                }
                else
                {
                    _res = zmcaux.ZAux_Direct_StepRatio(intPtr, jAxisList[i], 1, axisDir[i]);   //设置脉冲输出齿轮比
                    if (_res != 0)
                    {
                        return _res;
                    }

                    _res = zmcaux.ZAux_Direct_EncoderRatio(intPtr, jAxisList[i], 1, axisDir[i]); //设置编码器输入齿轮比
                    if (_res != 0)
                    {
                        return _res;
                    }
                }                   

                _res = zmcaux.ZAux_Direct_SetClutchRate(intPtr, jAxisList[i], clutchRate);    //设置同步连接速率
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetMaxSpeed(intPtr, jAxisList[i], 5000000);    //设置脉冲最大频率
                if (_res != 0)
                {
                    return _res;
                }

            }

            ////////////////////////////////
            //设置虚拟轴参数
            for (int i = 0;i<4;i++) 
            {

                _res = zmcaux.ZAux_Direct_SetAtype(intPtr, vAxisList[i], 0);      //设置虚拟轴加速度
                if (_res != 0)
                {
                    return _res;
                }

                if (i != 3)    //设置虚拟轴脉冲当量，默认为1000
                {                   
                    _res = zmcaux.ZAux_Direct_SetUnits(intPtr, vAxisList[i], 1000);
                    if (_res != 0)
                    {
                        return _res;
                    }
                }
                else    //设置虚拟旋转轴脉冲当量， 默认换算后单位为角度和实际轴一致
                {
                    _res = zmcaux.ZAux_Direct_SetUnits(intPtr, vAxisList[i], units[i]);
                    if (_res != 0)
                    {
                        return _res;
                    }
                }

                _res = zmcaux.ZAux_Direct_SetAccel(intPtr,vAxisList[i], 100);      //设置虚拟轴加速度
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetDecel(intPtr, vAxisList[i], 100);      //设置虚拟轴减速度
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetSpeed(intPtr, vAxisList[i], 10);       //设置虚拟轴速度
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetSramp(intPtr, vAxisList[i], 100);      //设置s曲线加减速
                if (_res != 0)
                {
                    return _res;
                }


            }

            return 0;
        }

        /// <summary>
        /// Delta机械手table参数
        /// </summary>
        /// <returns></returns>
        private int SetDeltaPara()
        {
            int _res;

            DeltaPara[0] = l_para[0];
            DeltaPara[1] = l_para[1];
            DeltaPara[2] = l_para[2];
            DeltaPara[3] = l_para[3];
            DeltaPara[4] = jNumPulses[0] * jReducRatio[0];
            DeltaPara[5] = jNumPulses[1] * jReducRatio[1];
            DeltaPara[6] = jNumPulses[2] * jReducRatio[2];
            DeltaPara[7] = l_para[4];
            DeltaPara[8] = l_para[5];
            DeltaPara[9] = l_para[6];
            DeltaPara[10] = jNumPulses[3] * jReducRatio[3];

            _res = zmcaux.ZAux_Direct_SetTable(intPtr,tableStaraId, 11, DeltaPara);
            if (_res != 0)
            {
                return _res;
            }

            _res = zmcaux.ZAux_Direct_SetTable(intPtr, tcpTableId, 6, ConversionValue);
            if (_res != 0)
            {
                return _res;
            }


            return 0;
        }


        /// <summary>
        /// 设置控制器vr寄存器数据（掉电保持）！！！！ 请不要使用前200的地址
        /// </summary>
        /// <param name="start">起始地址</param>
        /// <param name="num">写入个数</param>
        /// <param name="value">写入值</param>
        /// <returns></returns>
        public int SetVR(int start,int num, float[] value)
        {

            int _res;

            _res = zmcaux.ZAux_Direct_SetVrf(intPtr, start, num, value);   
            if (_res != 0)
            {
                return _res;
            }

            return 0;
        }


        /// <summary>
        /// 获取控制器vr寄存器数据（掉电保持）！！！！ 请不要使用前200的地址
        /// </summary>
        /// <param name="start"></param>
        /// <param name="num"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int ReadVR(int start, int num, ref float[] value)
        {

            int _res;

            _res = zmcaux.ZAux_Direct_GetVrf(intPtr, start, num, value);
            if (_res != 0)
            {
                return _res;
            }
          
            return 0;
        }


        #endregion

        #region 算法(不要修改)

        /// <summary>
        /// 建立机械手正解模式
        /// </summary>
        /// <param name="mode">1:正解模式 3：断开机械手模式</param>
        /// <returns></returns>
        private int DeltaEstab(int mode)
        {

            int _res;
   
            if (mode == 1) 
            {
                _res = zmcaux.ZAux_Direct_GetMtype(intPtr, vAxisList[0],ref mtype);
                if (_res != 0)
                {
                    return _res;
                }

                if (mtype != 34)
                {
                    _res = zmcaux.ZAux_Direct_Connreframe(intPtr, 4, vAxisList, 14, tableStaraId, 4, jAxisList);
                    if (_res != 0)
                    {
                        return _res;
                    }
                 
                    _res = WaitLoaded();
                    if (_res != 0)
                    {
                        return _res;
                    }

                }          

            }
            else if (mode == 0)
            {

                _res = zmcaux.ZAux_Direct_Rapidstop(intPtr, 2);
                if (_res != 0)
                {
                    return _res;
                }
                      
            }

            return 0;
        }

        /// <summary>
        /// int拆分成uint数组
        /// </summary>
        /// <param name="inputNum">输入值</param>
        /// <param name="num">转换个数</param>
        /// <param name="value">返回bool数组</param>
        private void IntToUintArray(int inputNum, int num, ref uint[] value)
        {
           
            int endNum;
            for (int i = 0; i < num; i++)
            {
                endNum = 1 << i;
                if (endNum == (endNum & inputNum))
                {
                    value[i] = 1;
                }
                else
                {
                    value[i] = 0;
                }
              
            }
        }

        /// <summary>
        /// delta工件坐标系生成标定结果
        /// </summary>
        /// <param name="posA">示教点A</param>
        /// <param name="posB">示教点B</param>
        /// <param name="posC">示教点C</param>
        public int Demarcate(float[] posA, float[] posB, float[] posC,  ref float[] ConversionValue) 
        {
            int _res;

            StringBuilder sb = new StringBuilder();

            string str;

            uint length = 100;
       
            _res = zmcaux.ZAux_Direct_SetTable(intPtr, 100, 3, posA);
            if (_res != 0)
            {
                return _res;
            }
            _res = zmcaux.ZAux_Direct_SetTable(intPtr, 105, 3, posB);
            if (_res != 0)
            {
                return _res;
            }
            _res = zmcaux.ZAux_Direct_SetTable(intPtr, 110, 3, posC);
            if (_res != 0)
            {
                return _res;
            }
           
            str = "ZCUSTOM (10, 100, 105, 110, 120)";

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            //将新坐标的转换参数从table寄存器中读取出来
            _res = zmcaux.ZAux_Direct_GetTable(intPtr, 120, 6, ConversionValue);
            if (_res != 0)
            {
                return _res;
            }

            //将新坐标的转换参数存入到vr寄存器中
            _res = zmcaux.ZAux_Direct_SetVrf(intPtr, 120, 6, ConversionValue);
            if (_res != 0)
            {
                return _res;
            }

            return 0;
        }

        /// <summary>
        /// delta工件坐标系转换
        /// </summary>
        /// <param name="mode">模式1切换为标定后的坐标系，模式0恢复原坐标系</param>
        /// <returns></returns>
        public int Conversion(int mode)
        {

            StringBuilder sb = new StringBuilder();

            string str;

            uint length = 100;

            DeltaEstab(1);

            int _res;

            if(mode == 0)
            {
                str = "FRAME_ROTATE(0,0,0,0,0,0)";
                _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
                if (_res != 0)
                {
                    return _res;
                }
            }
            else if (mode == 1)
            {
                str = "FRAME_ROTATE(TABLE(120),TABLE(121),TABLE(122),TABLE(123),TABLE(124),TABLE(125))";
                _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
                if (_res != 0)
                {
                    return _res;
                }
            }              
            return 0;
        }

        /// <summary>
        /// 获取文件中示教点位数据
        /// </summary>
        /// <returns></returns>
        public int getpos()
        {
            try
            {
                string[] temp = new string[10];
                string value;

                bool iniexist = IniFile.IniExists();

                if (iniexist==true)
                {
                    for (int i = 0; i < 100; i++)
                    {
                        Lpkeynum = "pos" + i;

                        bool exist = IniFile.KeyExists(Lpkeynum, LpAppname);

                        if (exist == true)
                        {

                            value = IniFile.Read(Lpkeynum, LpAppname);

                            temp = (value.Split(','));

                            idNum[i] = Convert.ToInt32(temp[0]);
                            name[i] = temp[1];

                            for (int j = 0; j < 4; j++)
                            {
                                posL[i, j] = Convert.ToSingle(temp[j + 2]);
                            }
                            for (int j = 0; j < 4; j++)
                            {
                                posJ[i, j] = Convert.ToSingle(temp[j + 6]);
                            }

                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                else
                {
                    return -1;
                }
           
            }
            catch (Exception ex)
            {
               
            }

            return 0;
        }

        /// <summary>
        /// 匹配名称并且输出编号
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private int matching(string temp,ref int num)
        {

            try
            {
                for (int i = 0; i < 50; i++)
                {
                    if (temp == name[i])
                    {
                       num=i;
                       return 0;
                    }
                }
            }
            catch
            {

            }
            return -1;
        }

        /// <summary>
        /// 设置连续插补
        /// </summary>
        /// <param name="mode">连续插补模式1：开启连续插补，0：关闭连续插补</param>
        /// <returns></returns>
        private int Merge(int mode)
        {
            int _res;

            for (int i = 0; i < 4; i++)
            {
                _res = zmcaux.ZAux_Direct_SetMerge(intPtr, vAxisList[i], mode);
                if (_res != 0)
                {
                    return _res;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                _res = zmcaux.ZAux_Direct_SetMerge(intPtr, jAxisList[i], mode);
                if (_res != 0)
                {
                    return _res;
                }
            }

            return 0;
        }

        /// <summary>
        /// 设置速度，缓冲生效，在运动指令之间穿插使用
        /// </summary>
        /// <param name="value">运动速度值，关节运动单位为角度，直角坐标运动单位为mm</param>
        /// <returns></returns>
        private int AxisSpeedBuffer(int axis, float value)
        {
            int _res;
            if (axis == 0)
            {
                _res = zmcaux.ZAux_Direct_SetForceSpeed(intPtr, jAxisList[0], value);
                if (_res != 0)
                {
                    return _res;
                }

            }
            else if (axis == 1)
            {
                _res = zmcaux.ZAux_Direct_SetForceSpeed(intPtr, vAxisList[0], value);
                if (_res != 0)
                {
                    return _res;
                }
            }

            return 0;

        }


        #endregion

        #region 供客户调用函数接口

        #region 运动状态

        /// <summary>
        /// 等待运动停止
        /// </summary>
        /// <returns></returns>
        public int WaitStop()
        {
            int TimeOut = 10000;             //超时时间        
            int AxisIdle = 0; //轴停止状态
            int _res;
            int temp = 0;

            while (TimeOut > 0)
            {
                AxisIdle = 0;
                                            
                for (int i = 0; i < 4; i++)
                {
                    _res = zmcaux.ZAux_Direct_GetIfIdle(intPtr, jAxisList[i], ref temp);
                    if (_res != 0)
                    {
                        return _res;
                    }

                    AxisIdle = AxisIdle + temp;
                }
                
                if (AxisIdle == -4)
                {                  
                    return 0;
                }
                else
                {
                    Thread.Sleep(1);
                    TimeOut = TimeOut - 1;
                }

            }

            return -1;
        }

        /// <summary>
        /// 等待运动缓冲空
        /// </summary>
        /// <returns></returns>
        private int WaitLoaded()
        {

            int TimeOut = 10000;             //超时时间           
            int AxisLoaded = 0; //轴剩余缓冲
            int _res;

            while (TimeOut > 0)
            {
              
                _res = zmcaux.ZAux_Direct_GetLoaded(intPtr, jAxisList[0], ref AxisLoaded);
                if (_res != 0)
                {
                    return _res;
                }
              
                if (AxisLoaded == -1)
                {
                   return 0;
                }
                else 
                {
                    Thread.Sleep(1);
                    TimeOut = TimeOut - 1;
                }

            }
           
            return -1;
           
        }

        /// <summary>
        /// 获取关节坐标值(编码器mpos值)
        /// </summary>
        /// <param name="pos">返回坐标数据</param>
        /// <returns></returns>
        public int Read_PosJ(ref float[] pos)
        {
            int _res;

            for(int i  = 0; i < 4; i++)
            {
                _res = zmcaux.ZAux_Direct_GetMpos(intPtr, jAxisList[i], ref pos[i]);
                if (_res != 0)
                {
                    return _res;
                }
            }

            return 0;
        }

        /// <summary>
        /// 获取直角坐标值(编码器mpos值)
        /// </summary>
        /// <param name="pos">返回坐标数据</param>
        /// <returns></returns>
        public int Read_PosL(ref float[] pos)
        {
            int _res;

            for (int i = 0; i < 4; i++)
            {
                _res = zmcaux.ZAux_Direct_GetMpos(intPtr, vAxisList[i], ref pos[i]);
                if (_res != 0)
                {
                    return _res;
                }
            }

            return 0;
        }

        /// <summary>
        /// 获取关节轴状态，0为正常
        /// </summary>
        /// <param name="value">轴状态值</param>
        /// <returns></returns>
        public int Read_JAxisStatus(ref int[] value)
        {
            int _res;
      
            for( int i = 0;i < 4; i++)
            {
                _res = zmcaux.ZAux_Direct_GetAxisStatus(intPtr, jAxisList[i], ref value[i]);
                if (_res != 0)
                {
                    return _res;
                }

            }
          
            return 0;
        }

        /// <summary>
        /// 获取xyzu轴状态，0为正常
        /// </summary>
        /// <param name="value">轴状态值</param>
        /// <returns></returns>
        public int Read_VAxisStatus(ref int[] value)
        {

            int _res;

            for (int i = 0; i < 4; i++)
            {
                _res = zmcaux.ZAux_Direct_GetAxisStatus(intPtr, vAxisList[i], ref value[i]);
                if (_res != 0)
                {
                    return _res;
                }

            }

            return 0;
        }

        /// <summary>
        /// 清除轴报警
        /// </summary>       
        /// <returns></returns>
        public int Reset()
        {
            int _res;

            for (int i = 0; i < 4; i++)
            {
                _res = zmcaux.ZAux_Direct_Single_Datum(intPtr, jAxisList[i], 0);
                if (_res != 0)
                {
                    return _res;
                }

            }
            for (int j = 0; j < 4; j++)
            {
                _res = zmcaux.ZAux_Direct_Single_Datum(intPtr, vAxisList[j], 0);
                if (_res != 0)
                {
                    return _res;
                }
            }

            return 0;
        }



        #endregion

        #region 运动控制指令

        /// <summary>
        /// 机械手运动停止
        /// </summary>
        /// <param name="mode">默认为立刻停止，mode填写1,当前运动完成后停止</param>
        /// <returns></returns>
        public int Stop(int mode=2)
        {
            int _res;
                 
            _res = zmcaux.ZAux_Direct_CancelAxisList(intPtr, 4, jAxisList, mode);
            if (_res != 0)
            {
                return _res;
            }
                   
            return 0;
        }

        /// <summary>
        /// 急停，所有轴紧急停止（包括扩展轴），会断开机械手正逆解模式，包括工件坐标系变换
        /// </summary>
        /// <returns></returns>
        public int EStop()
        {
            int _res;
        
            _res = zmcaux.ZAux_Direct_Rapidstop(intPtr, 2);
            if (_res != 0)
            {
                return _res;
            }

            return 0;
        }

        /// <summary>
        /// 脉冲型delta机械手回零指令
        /// </summary>
        /// <param name="mode">回零模式，默认9</param>
        /// <param name="speed">回零速度</param>
        /// <param name="creep">爬行速度</param>
        /// <param name="offsetValue">delta回零4个关节轴偏移值</param>
        /// <returns></returns>
        public int GoHome(int mode,float speed,float creep,float[] offsetValue)
        {
            int _res;

            for (int i = 0; i < 4; i++)
            {
                _res = zmcaux.ZAux_Direct_SetDatumIn(intPtr, jAxisList[i], Convert.ToInt32(jOrigiNum[i]));
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetAccel(intPtr, jAxisList[i], speed * 2);
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetDecel(intPtr, jAxisList[i], speed * 2);
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetSpeed(intPtr, jAxisList[i], speed);
                if (_res != 0)
                {
                    return _res;
                }


                _res = zmcaux.ZAux_Direct_SetCreep(intPtr, jAxisList[i], creep);
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_Single_Datum(intPtr, jAxisList[i], mode);
                if (_res != 0)
                {
                    return _res;
                }

            }
            _res = WaitStop();
            if (_res != 0)
            {
                return _res;
            }

            for (int i = 0; i < 4; i++)
            {

                _res = zmcaux.ZAux_Direct_SetDpos(intPtr, jAxisList[i], 0);
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetMpos(intPtr, jAxisList[i], 0);
                if (_res != 0)
                {
                    return _res;
                }

            }

            Thread.Sleep(100);

            _res = zmcaux.ZAux_Direct_MoveAbs(intPtr, 4, jAxisList, offsetValue);
            if (_res != 0)
            {
                return _res;
            }

            _res = WaitStop();
            if (_res != 0)
            {
                return _res;
            }

            Thread.Sleep(50);

            for (int i = 0; i < 4; i++)
            {

                _res = zmcaux.ZAux_Direct_SetDpos(intPtr, jAxisList[i], 0);
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetMpos(intPtr, jAxisList[i], 0);
                if (_res != 0)
                {
                    return _res;
                }

            }

            Thread.Sleep(20);

            return 0;
        }
      
        /// <summary>
        /// 直角坐标系绝对运动(默认开启连续插补且不阻塞线程)
        /// </summary>
        /// <param name = "value" > 坐标参数, xyzr4轴绝对值坐标</param>     
        /// <param name = "zone" > 轨迹平滑，0-100，0为不平滑，-1为等待准确到达(会阻塞当前指令)</param>
        /// <param name = "speed" > 缓冲修改运动速度 ，-1为保持当前速度不做修改</param>
        /// <returns></returns>
        public int MoveLAbs(float[] value,int zone=0, float speed = -1)
        {
            int _res;

            int num;

            StringBuilder sb = new StringBuilder();

            string str;

            uint length = 100;

            if (value.Length < 4)
            {
                return -1;
            }

            _res = DeltaEstab(1);
            if (_res != 0)
            {
                return _res;
            }
            
            if(speed != -1 && speed>0)
            {
                AxisSpeedBuffer(1,speed);
            }
           
            if(zone != -1)
            {
                zmcaux.ZAux_Direct_MovePara(intPtr, (uint)jAxisList[0], "zsmooth", (uint)vAxisList[0], zone);
            }
                 
            str = "base(5,6,7,8)";   // 2025.2.11修改

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            str = "mover2_labs(-1," + value[0].ToString() + "," + value[1].ToString() + "," + value[2].ToString() + "," + value[3].ToString() + ")";

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            if (zone == -1)
            {
                WaitStop();
            }
            
            return 0;
        }

        /// <summary>
        ///直角坐标系绝对运动(默认开启连续插补且不阻塞线程)
        /// </summary>
        /// <param name="posName">示教点位坐标名称</param>
        /// <param name="zone">轨迹平滑，0-100，0为不平滑，-1为等待准确到达(会阻塞当前指令)</param>
        /// <param name="speed">缓冲修改运动速度 ，-1为保持当前速度不做修改</param>
        /// <returns></returns>
        public int MoveLAbs(string posName, int zone = 0, float speed = -1)
        {
            int _res;
                  
            int num=0;

            float[] value = new float[4];

            StringBuilder sb = new StringBuilder();

            string str;

            uint length = 100;

            _res = DeltaEstab(1);
            if (_res != 0)
            {
                return _res;
            }
            try
            {
                _res = matching(posName,ref num);

                if (_res == 0)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        value[i] = posL[num, i];
                    }
                }
                else
                {
                    return -1;
                }             
            }
            catch (Exception ex)
            {
                return -1;
            }

            if (speed != -1 && speed > 0)
            {
                AxisSpeedBuffer(1, speed);
            }

            zmcaux.ZAux_Direct_SetZsmooth(intPtr, vAxisList[0], zone);

            str = "base(5,6,7,8)";   // 2025.2.11修改

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            str = "mover2_labs(-1," + value[0].ToString() + "," + value[1].ToString() + "," + value[2].ToString() + "," + value[3].ToString() + ")";

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            if (zone == -1)
            {
                WaitStop();
            }

            return 0;
        }

        /// <summary>
        /// 直角坐标系相对运动(默认开启连续插补且不阻塞线程)
        /// </summary>
        /// <param name = "value" > 坐标参数, xyzr4轴绝对值坐标</param>     
        /// <param name = "zone" > 轨迹平滑，0-100，0为不平滑，-1为等待准确到达(会阻塞当前指令)</param>
        /// <param name = "speed" > 缓冲修改运动速度 ，-1为保持当前速度不做修改</param>
        /// <returns></returns>
        public int MoveL(float[] value, int zone = 0, float speed = -1)
        {
            int _res;

            int num ;
         
            StringBuilder sb = new StringBuilder();

            string str;

            uint length = 1000;

            if (value.Length < 4)
            {
                return -1;
            }

            _res = DeltaEstab(1);
            if (_res != 0)
            {
                return _res;
            }

            if (speed != -1 && speed > 0)
            {
                AxisSpeedBuffer(1, speed);
            }

            zmcaux.ZAux_Direct_SetZsmooth(intPtr, vAxisList[0], zone);

            str = "base(5,6,7,8)";   // 2025.2.11修改

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }
          
            str = "mover2_l(-1,"+ value[0].ToString() + "," + value[1].ToString() + "," + value[2].ToString() + "," + value[3].ToString() + ")";

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            if (zone == -1)
            {
                WaitStop();
            }
      
            return 0;
       
        }

        /// <summary>
        /// 关节坐标系绝对运动
        /// </summary>
        /// <param name = "value" > 坐标参数, 关节实轴角度绝对值坐标</param>     
        /// <param name = "zone" > 轨迹类型，0为允许预读指令，-1为等待准确到达(会阻塞当前指令)</param>
        /// <param name = "speed" > 缓冲修改运动速度 ，-1为保持当前速度不做修改</param>
        /// <returns></returns>
        public int MoveJAbs(float[] value,int zone = 0, float speed = -1)
        {
            int _res;

            int num ;
          
            StringBuilder sb = new StringBuilder();

            string str;

            uint length = 100;

            if (value.Length < 4)
            {
                return -1;
            }

            _res = DeltaEstab(1);
            if (_res != 0)
            {
                return _res;
            }
         
            if (speed != -1 && speed > 0)
            {
                AxisSpeedBuffer(0, speed);
            }

            zmcaux.ZAux_Direct_SetZsmooth(intPtr, vAxisList[0], zone);

            str = "base(0,1,2,3)";   // 2025.2.11修改

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            str = "MOVER_JABS("+ value[0].ToString() + "," + value[1].ToString() + "," + value[2].ToString() + "," + value[3].ToString() + ")";

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            if (zone == -1)
            {
                WaitStop();
            }

            return 0;

        }

        /// <summary>
        /// 关节坐标系绝对运动
        /// </summary>
        /// <param name="posName">示教点位坐标名称</param>
        /// <param name = "zone" > 轨迹类型，0为允许预读指令，-1为等待准确到达(会阻塞当前指令)</param>
        /// <param name = "speed" > 缓冲修改运动速度 ，-1为保持当前速度不做修改</param>
        /// <returns></returns>
        public int MoveJAbs(string posName, int zone = 0, float speed = -1)
        {
            int _res;

            int num = 0;

            float[] value = new float[4];

            StringBuilder sb = new StringBuilder();

            string str;

            uint length = 100;

            _res = DeltaEstab(1);
            if (_res != 0)
            {
                return _res;
            }
            try
            {
                matching(posName, ref num);

                for (int i = 0; i < 4; i++)
                {
                    value[i] = posJ[num, i];
                }

            }
            catch (Exception ex)
            {
                return -1;
            }

            if (speed != -1 && speed > 0)
            {
                AxisSpeedBuffer(0, speed);
            }

            zmcaux.ZAux_Direct_SetZsmooth(intPtr, vAxisList[0], zone);

            str = "base(0,1,2,3)";   // 2025.2.11修改

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            str = "mover2_labs(-1," + value[0].ToString() + "," + value[1].ToString() + "," + value[2].ToString() + "," + value[3].ToString() + ")";

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            if (zone == -1)
            {
                WaitStop();
            }

            return 0;
        }

        /// <summary>
        /// 关节坐标系运动
        /// </summary>
        /// <param name="value">坐标参数,关节实际轴相对值坐标</param>
        /// <param name = "zone" > 轨迹类型，0为允许预读指令，-1为等待准确到达(会阻塞当前指令)</param>
        /// <param name = "speed" > 缓冲修改运动速度 ，-1为保持当前速度不做修改</param> 
        /// <returns></returns>
        public int MoveJ(float[] value, int zone = 0, float speed = -1)
        {
            int _res;

            int num ;

            StringBuilder sb = new StringBuilder();

            string str;

            uint length = 100;

            if (value.Length < 4)
            {
                return -1;
            }

            _res = DeltaEstab(1);
            if (_res != 0)
            {
                return _res;
            }
          
            if (speed != -1 && speed > 0)
            {
                AxisSpeedBuffer(0, speed);
            }
         
            str = "base(0,1,2,3)";   // 2025.2.11修改

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            str = "MOVER2_J("+value[0].ToString() + "," + value[1].ToString() + "," + value[2].ToString() + "," + value[3].ToString() + ")";

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            if (zone == -1)
            {
                WaitStop();
            }

            return 0;

        }

        /// <summary>
        /// 机械手空间圆弧绝对运动（包含平面圆弧），直角坐标系
        /// </summary>
        /// <param name="endPos">目标点坐标xyz值</param>
        /// <param name="CenterPos">圆弧中间点坐标xyz值</param>
        /// <param name = "zone" > 轨迹类型，0为允许预读指令，-1为等待准确到达(会阻塞当前指令)</param> 
        /// <param name = "speed" > 缓冲修改运动速度 ，-1为保持当前速度不做修改</param>
        /// <returns></returns>
        public int MoveCAbs(float[] endPos, float[] centerPos, int zone = 0, float speed=-1)
        {

            int _res;

            int num;

            StringBuilder sb = new StringBuilder();

            string str;

            uint length = 100;

            _res = DeltaEstab(1);
            if (_res != 0)
            {
                return _res;
            }
         
            if (speed != -1 && speed > 0)
            {
                AxisSpeedBuffer(1, speed);
            }

            str = "base(5,6,7,8)";   // 2025.2.11修改

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            try
            {
                str = "mover2_arcabs(-1," + centerPos[0].ToString() + "," + centerPos[1].ToString() + "," + centerPos[2].ToString() + "," + endPos[0].ToString() + "," + endPos[1].ToString() + "," + endPos[2].ToString() + ")";

                _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
                if (_res != 0)
                {
                    return _res;
                }

                if (zone == -1)
                {
                    WaitStop();
                }
            }
            catch
            {
                return -1;
            }
         
            return 0;

        }

        /// <summary>
        /// 机械手空间圆弧相对运动（包含平面圆弧），直角坐标系
        /// </summary>
        /// <param name="endPos">目标点坐标xyz值</param>
        /// <param name="CenterPos">圆弧中间点坐标xyz值</param>   
        /// <param name = "zone" > 轨迹类型，0为允许预读指令，-1为等待准确到达(会阻塞当前指令)</param> 
        /// <param name = "speed" > 缓冲修改运动速度 ，-1为保持当前速度不做修改</param>
        /// <returns></returns>
        public int MoveC(float[] endPos, float[] centerPos, int zone = 0, float speed = -1)
        {

            int _res;

            int num;

            StringBuilder sb = new StringBuilder();

            string str;

            uint length = 100;

            _res = DeltaEstab(1);
            if (_res != 0)
            {
                return _res;
            }

            if (speed != -1 && speed > 0)
            {
                AxisSpeedBuffer(1, speed);
            }

            str = "base(5,6,7,8)";   // 2025.2.11修改

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            try
            {
                str = "mover2_arc(-1," + centerPos[0].ToString() + "," + centerPos[1].ToString() + "," + centerPos[2].ToString() + "," + endPos[0].ToString() + "," + endPos[1].ToString() + "," + endPos[2].ToString() + ")";

                _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
                if (_res != 0)
                {
                    return _res;
                }

                if (zone == -1)
                {
                    WaitStop();
                }
            }
            catch
            {
                return -1;
            }
         
            return 0;

        }

        /// <summary>
        /// 拱形绝对运动指令，机械手末端拱形方式蛙跳一定高度再下降到目标点
        /// </summary>
        /// <param name="hPos">Z轴最大高度坐标值</param>
        /// <param name="h1">Z轴从起始点的提升高度值</param>
        /// <param name="h2">Z轴到目标点的下降高度值</param>
        /// <param name="endPos">最终目标点位置</param>
        /// <param name = "zone" > 轨迹类型，0为允许预读指令，-1为等待准确到达(会阻塞当前指令)</param>
        /// <param name = "speed" > 缓冲修改运动速度 ，-1为保持当前速度不做修改</param>
        /// <returns></returns>
        public int MoveHLAbs(float hPos, float h1, float h2, float[] endPos, int zone = 0, float speed = -1)
        {
            int _res;

            int num;

            StringBuilder sb = new StringBuilder();

            string str;

            uint length = 100;

            if (endPos.Length < 3)
            {
                return -1;
            }

            _res = DeltaEstab(1);
            if (_res != 0)
            {
                return _res;
            }

            if (speed != -1 && speed > 0)
            {
                AxisSpeedBuffer(0, speed);
            }

            if (zone != -1)
            {
                zmcaux.ZAux_Direct_MovePara(intPtr, (uint)jAxisList[0], "zsmooth", (uint)vAxisList[0], zone);
            }

            str = "base(5,6,7,8)";   

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            try
            {
                str = "MOVER2_ARCHPLABS("+ hPos.ToString() + "," + h1.ToString() + "," + h2.ToString() + "," + "-1" + "," + endPos[0].ToString() + "," + endPos[1].ToString() + "," + endPos[2].ToString() + "," + "0" + ")";

                _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
                if (_res != 0)
                {
                    return _res;
                }

                if (zone == -1)
                {
                    WaitStop();
                }
            }
            catch
            {
                return -1;
            }
         
            return 0;        
        }

        /// <summary>
        /// 拱形相对运动指令，机械手末端拱形方式蛙跳一定高度再下降到目标点
        /// </summary>
        /// <param name="hPos">Z轴最大高度坐标值</param>
        /// <param name="h1">Z轴从起始点的提升高度值</param>
        /// <param name="h2">Z轴到目标点的下降高度值</param>
        /// <param name="endPos">最终目标点位置</param>
        /// <param name = "zone" > 轨迹类型，0为允许预读指令，-1为等待准确到达(会阻塞当前指令)</param>
        /// <param name = "speed" > 缓冲修改运动速度 ，-1为保持当前速度不做修改</param>
        /// <returns></returns>
        public int MoveHL(float hPos, float h1, float h2, float[] endPos, int zone = 0, float speed = -1)
        {
            int _res;

            int num;

            StringBuilder sb = new StringBuilder();

            string str;

            uint length = 100;

            if (endPos.Length < 4)
            {
                return -1;
            }

            _res = DeltaEstab(1);
            if (_res != 0)
            {
                return _res;
            }

            if (speed != -1 && speed > 0)
            {
                AxisSpeedBuffer(0, speed);
            }

            str = "base(5,6,7,8)"; 

            _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
            if (_res != 0)
            {
                return _res;
            }

            try
            {
                str = "MOVER2_ARCHPL(" + hPos.ToString() + "," + h1.ToString() + "," + h2.ToString() + "," + "-1" + endPos[0].ToString() + endPos[1].ToString() + endPos[2].ToString() + endPos[3].ToString() + ")";

                _res = zmcaux.ZAux_Execute(intPtr, str, sb, length);
                if (_res != 0)
                {
                    return _res;
                }

                if (zone == -1)
                {
                    WaitStop();
                }
            }
            catch
            {
                return -1;
            }

            return 0;
        }


        ///////////////////////////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// 扩展轴单轴（单端轴或者差分脉冲轴，非机械手轴）绝对运动，扩展轴号在basic初始化程序中设置
        /// </summary>
        /// <param name="axisnum">轴号</param>
        /// <param name="value">坐标参数</param>
        /// <returns></returns>
        public int MoveAbs(int axisnum , float value)
        {
            int _res;

            _res = zmcaux.ZAux_Direct_SetAtype(intPtr, axisnum, 1);
            if (_res != 0)
            {
                return _res;
            }

            _res = zmcaux.ZAux_Direct_Single_MoveAbs(intPtr, axisnum, value);
            if (_res != 0)
            {
                return _res;
            }

            return 0;

        }

        /// <summary>
        /// 扩展轴单轴（单端轴或者差分脉冲轴，非机械手轴）相对运动，扩展轴号在basic初始化程序中设置
        /// </summary>
        /// <param name="axisnum">轴号</param>
        /// <param name="value">坐标参数</param>
        /// <returns></returns>
        public int Move(int axisnum, float value)
        {
            int _res;

            _res = zmcaux.ZAux_Direct_SetAtype(intPtr, axisnum, 1);
            if (_res != 0)
            {
                return _res;
            }

            _res = zmcaux.ZAux_Direct_Single_Move(intPtr, axisnum, value);
            if (_res != 0)
            {
                return _res;
            }

            return 0;

        }

        /// <summary>
        /// 用于扩展单轴停止，需要填写对应轴号
        /// </summary>
        /// <param name="axisnum">轴号</param>
        /// <returns></returns>
        public int Stop_Single(int axisnum)
        {

            int _res;
            _res = zmcaux.ZAux_Direct_Single_Cancel(intPtr, axisnum, 2);
            if (_res != 0)
            {
                return _res;
            }

            return 0;

        }

        /// <summary>
        /// 运动暂停，立刻暂停当前运动，没有调用运动指令不要调用暂停
        /// </summary>
        /// <returns></returns>
        public int MovePAUSE()
        {
            int _res;

            int temp = 0;
        
            _res = zmcaux.ZAux_Direct_GetIfIdle(intPtr, jAxisList[0], ref temp);
            if (_res != 0)
            {
                return _res;
            }

            if (temp == 0)
            {
                _res = zmcaux.ZAux_Direct_MovePause(intPtr, jAxisList[0], 0);
                if (_res != 0)
                {
                    return _res;
                }

            }                   

            return 0;
        }

        /// <summary>
        /// 运动恢复，调用暂停后恢复运动
        /// </summary>
        /// <returns></returns>
        public int MoveRESUME()
        {
            int _res;

            int temp = 0;
         
            _res = zmcaux.ZAux_Direct_GetAxisStatus(intPtr, jAxisList[0], ref temp);
            if (_res != 0)
            {
                return _res;
            }

            if (temp == 8388608)
            {
                _res = zmcaux.ZAux_Direct_MoveResume(intPtr, jAxisList[0]);
                if (_res != 0)
                {
                    return _res;
                }

            }
           
            return 0;
        }

        #endregion

        #region 运动参数设置

        /// <summary>
        /// 设置运动速度,立刻生效
        /// </summary>
        /// <param name="value">运动速度值，关节运动单位为角度，直角坐标运动单位为mm</param>
        /// <param name="ms">加减速时间,单位毫秒</param>
        /// <param name="sramp">s加减速曲线值,单位毫秒</param>
        /// <returns></returns>
        public int AxisSpeed(float value, float ms = 100, float sramp = 50)
        {

            int _res;
         
            for (int i = 0; i < 4; i++)
            {
                _res = zmcaux.ZAux_Direct_SetAccel(intPtr, vAxisList[i], value / (ms / 1000));
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetDecel(intPtr, vAxisList[i], value / (ms / 1000));
                if (_res != 0)
                {
                    return _res;
                }
            
                _res = zmcaux.ZAux_Direct_SetForceSpeed(intPtr, vAxisList[i], value);
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetSramp(intPtr, vAxisList[i], sramp);
                if (_res != 0)
                {
                    return _res;
                }
            }            
            
            for(int i = 0;i < 4; i++)
            {
                _res = zmcaux.ZAux_Direct_SetAccel(intPtr, jAxisList[i], value / (ms / 1000));
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetDecel(intPtr, jAxisList[i], value / (ms / 1000));
                if (_res != 0)
                {
                    return _res;
                }
           
                _res = zmcaux.ZAux_Direct_SetForceSpeed(intPtr, jAxisList[i], value);
                if (_res != 0)
                {
                    return _res;
                }

                _res = zmcaux.ZAux_Direct_SetSramp(intPtr, jAxisList[i], sramp);
                if (_res != 0)
                {
                    return _res;
                }
            }
            
            return 0;

        }

        #endregion

        #region 输入输出

        /// <summary>
        /// 读取单个普通输入口
        /// </summary>
        /// <param name="address">io地址</param>
        /// <param name="value">返回状态值</param>
        /// <returns></returns>
        public int Read_InSingle(int address, ref uint value)
        {

            int _res;
     
            _res = zmcaux.ZAux_Direct_GetIn(intPtr, address, ref value);
            if (_res != 0)
            {
                return _res;
            }
        
            return 0;

        }


        /// <summary>
        /// 读取多个普通输入口
        /// </summary>
        /// <param name="address">io起始地址</param>
        /// <param name="num">读取数量</param>
        /// <param name="value">返回状态值,按位获取的输入口的状态值。最多存储32个输出口状态</param>
        /// <returns></returns>
        public int Read_InMuti(int address, int num, ref uint[] value)
        {

            int _res;

            int temp;
         
            _res = zmcaux.ZAux_Direct_GetInMulti(intPtr, address, address + num - 1, out temp);
            if (_res != 0)
            {
                return _res;
            }

            IntToUintArray(temp, num, ref value);

            return 0;

        }


        /// <summary>
        /// 读取单个普通输出口
        /// </summary>
        /// <param name="address">io地址</param>
        /// <param name="value">返回状态值</param>
        /// <returns></returns>
        public int Read_OutSingle(int address, ref uint value)
        {

            int _res;
         
            _res = zmcaux.ZAux_Direct_GetOp(intPtr, address,ref value);
            if (_res != 0)
            {
                return _res;
            }
      
            return 0;

        }


        /// <summary>
        /// 读取多个普通输出口
        /// </summary>
        /// <param name="address">io起始地址</param>
        /// <param name="num">读取数量</param>
        /// <param name="value">返回状态值</param>
        /// <returns></returns>
        public int Read_OutMuti(int address, int num, ref uint[] value)
        {

            int _res;

            uint temp;

            _res = zmcaux.ZAux_Direct_GetOutMulti(intPtr, (ushort)address, (ushort)(address + num - 1), out temp);
            if (_res != 0)
            {
                return _res;
            }

            IntToUintArray(Convert.ToInt32(temp), num, ref value);
           
            return 0;

        }


        /// <summary>
        /// 设置单个普通输出口
        /// </summary>
        /// <param name="address">io地址</param>
        /// <param name="value">状态值</param>
        /// <returns></returns>
        public int Set_OutSingle(int address, uint value)
        {

            int _res;

            _res = zmcaux.ZAux_Direct_SetOp(intPtr, address, value);
            if (_res != 0)
            {
                return _res;
            }
       
            return 0;

        }


        /// <summary>
        /// 设置多个普通输出口
        /// </summary>
        /// <param name="address">io起始地址</param>
        /// <param name="num">设置数量</param>
        /// <param name="value">状态值</param>
        /// <returns></returns>
        public int Set_OutMuti(int address, int num,uint[] value)
        {

            int _res;

            _res = zmcaux.ZAux_Direct_SetOutMulti(intPtr, (ushort)address, (ushort)(address + num - 1), value);
            if (_res != 0)
            {
                return _res;
            }
       
            return 0;

        }


        /// <summary>
        /// 设置缓冲输出口
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Set_OutBuffer(int address, uint value)   //进入轴缓冲区，可以控制输出口触发时机
        {

            int _res;
            
            _res = zmcaux.ZAux_Direct_MoveOp(intPtr, jAxisList[0], address, (int)value);
            if (_res != 0)
            {
                return _res;
            }
            
            return 0;

        }

        /// <summary>
        /// 设置缓冲输出口，延时后反转
        /// </summary>
        /// <param name="address"></param>
        /// <param name="value"></param>
        /// <param name="ms"></param>
        /// <returns></returns>
        public int Set_OutBuffer(int address, uint value,int ms)   //进入轴缓冲区，可以控制输出口触发时机以及反转延时
        {

            int _res;
       
            _res = zmcaux.ZAux_Direct_MoveOp2(intPtr, jAxisList[0], address, (int)value, ms);
            if (_res != 0)
            {
                return _res;
            }
            
            return 0;

        }

        #endregion

        #region 高级指令

        /// <summary>
        /// 一维硬件比较触发，只能选择单轴(选择x或者y)
        /// </summary>
        /// <param name="Axisnum">选择比较轴</param>
        /// <param name="opnum">选择比较输出口编号</param>
        /// <param name="cPos">比较点坐标，第一个比较点状态为1，每到一个比较点输出口反转一次</param>
        /// <returns></returns>
        public int HW_PSWITCH(int[] axis, int opnum, float[] cPos)  //注意触发方向，默认不设置触发的运动方向，只针对绝对位置坐标
        {

            int _res;

            int address = 100;

            int temp;

            temp = cPos.Length;

            _res = zmcaux.ZAux_Direct_SetOp(intPtr, opnum, 0);
            if (_res != 0)
            {
                return _res;
            }

            _res = zmcaux.ZAux_Direct_SetTable(intPtr, address, temp, cPos);
            if (_res != 0)
            {
                return _res;
            }

            _res = zmcaux.ZAux_Direct_HwPswitch2(intPtr, vAxisList[axis[0]],2,0,0,0,0,0,0,0,0);
            if (_res != 0)
            {
                return _res;
            }

            _res = zmcaux.ZAux_Direct_HwPswitch2(intPtr, vAxisList[axis[0]], 1, opnum, 1, address, address + temp - 1, -1,0,0,0);
            if (_res != 0)
            {
                return _res;
            }

            return 0;
        }

        #endregion

        #endregion

    }
}
