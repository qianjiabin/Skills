using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using cszmcaux;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Button = System.Windows.Forms.Button;
using TextBox = System.Windows.Forms.TextBox;

/*******************************************************************************************************
**--------------文件信息--------------------------------------------------------------------------------
**例程名: 双旋转台PSO例程
**创建人: 许康君
********************************************************************************************************/



namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
        public static Int32 axisX = 0, axisY = 1, axisZ = 2, axisA = 3, axisC = 4;  //关节轴(物理轴)轴号
        public static Int32 virAxisX = 6, virAxisY = 7, virAxisZ = 8, virAxisA = 9, virAxisC = 10;  //虚拟轴轴号
        public Int32[] virAxisList = new int[] { virAxisX, virAxisY, virAxisZ, virAxisA, virAxisC };
        public Int32[] realAxisList = new int[] { axisX, axisY, axisZ, axisA, axisC };
        public Int32 frameMode = 0, reFrameMode = 0;   //逆解(frameMode)模式, 正解模式(reFrameMode), 注意中文的正反貌似和英文的正反不一致, 没错
        public Int32 frameType = 17;   //机械手类型17，双旋转台,ZROBOT可以仿真演示
        public float centY1, centZ1, pulse1onecircle, ifAntiClock1, centX2, centY2, pulse2onecircle, ifAntiClock2;  //具体变量含义参考机械手手册
        public Int32 frameParamTB = 0;   //要把frame参数传到控制器的table寄存器内, 这个变量就是table寄存器的起始索引, 地址不要冲突就好
        public Int32 frameZeroGot = 0;   //用来做Frame圆心已经获取的标志变量
        public Int32 jogAxisNum = 0;
        public Button[] FButton = new Button[10];
        public Button[] RButton = new Button[10];
        public TextBox[] AxisSpeed = new TextBox[10];
        public TextBox[] AxisAccel = new TextBox[10];
        public TextBox[] AxisDecel = new TextBox[10];
        public TextBox[] AxisDpos = new TextBox[10];
        Thread ThreadDrawId;
        public bool ThreadFlag = false;

        public Form1()
        {
            InitializeComponent();
            Button[] TFButton = { FX, FY, FZ, FA, FC, FVX, FVY, FVZ,FVA,FVC};      //正向走按钮
            Button[] TRButton = { RX, RY, RZ, RA, RC,RVX, RVY, RVZ,RVA,RVC};      //负向走按钮
            TextBox[] TAxisSpeed = {X_Speed,Y_Speed,Z_Speed,A_Speed,C_Speed,VX_Speed,VY_Speed,VZ_Speed,VA_Speed,VC_Speed};
            TextBox[] TAxisAccel = { X_Accel, Y_Accel, Z_Accel, A_Accel, C_Accel, VX_Accel, VY_Accel, VZ_Accel,VA_Accel,VC_Accel};
            TextBox[] TAxisDccel = { X_Decel, Y_Decel, Z_Decel, A_Decel, C_Decel, VX_Decel, VY_Decel, VZ_Decel,VA_Decel,VC_Decel};
            TextBox[] TAxisDpos = { X_Dpos, Y_Dpos, Z_Dpos, A_Dpos, C_Dpos, VX_Dpos, VY_Dpos, VZ_Dpos,VA_Dpos,VC_Dpos};
            FButton = TFButton;
            RButton = TRButton;
            AxisSpeed = TAxisSpeed;
            AxisAccel = TAxisAccel;
            AxisDecel = TAxisDccel;
            AxisDpos = TAxisDpos;

            for(int i = 0; i < FButton.Length; i++)
            {
                FButton[i].MouseDown += new System.Windows.Forms.MouseEventHandler(this.FB_MouseDown);
                FButton[i].MouseUp += new System.Windows.Forms.MouseEventHandler(this.FB_MouseUp);

                RButton[i].MouseDown += new System.Windows.Forms.MouseEventHandler(this.RB_MouseDown);
                RButton[i].MouseUp += new System.Windows.Forms.MouseEventHandler(this.RB_MouseUp);
            }

        }

        private void C_Open_Com_Click(object sender, EventArgs e)       //串口连接控制器
        {
            if (g_handle == (IntPtr)0)
            {
                C_Close_Card_Click(sender, e);
            }

            UInt32 comid = Convert.ToUInt32(C_COM_ID.Text);
            zmcaux.ZAux_OpenCom(comid, out g_handle);
            if ((int)g_handle != 0)
            {
                timer1.Start();
                ParamIni();
                this.Text = "已连接";
            }
        }

        private void C_Ip_Address_DropDown(object sender, EventArgs e)    //下拉搜索控制器IP
        {
            int i, num;
            string[] sArray;
            StringBuilder buffer = new StringBuilder(10240);
            string buff = "";
            zmcaux.ZAux_SearchEthlist(buffer, 10230, 200);
            buff += buffer;
            sArray = buff.Split(' ');
            num = buff.Split(' ').Length;

            C_Ip_Address.Items.Clear();

            C_Ip_Address.Items.Insert(0, "127.0.0.1");
            for (i = 0; i < num - 1; i++)
            {
                C_Ip_Address.Items.Insert(i + 1, sArray[i].ToString());
            }
        }

        private void C_Open_Eth_Click(object sender, EventArgs e)       //网口连接控制器
        {
            if (g_handle == (IntPtr)0)
            {
                C_Close_Card_Click(sender, e);
            }
            zmcaux.ZAux_OpenEth(C_Ip_Address.Text, out g_handle);
            if (g_handle != (IntPtr)0)
            {
                timer1.Start();
                ParamIni();
                this.Text = "已连接";

            }
        }

        private void C_Pci_Id_DropDown(object sender, EventArgs e)      //下拉搜索PCI卡
        {
            int ipcicardes = zmcaux.ZAux_GetMaxPciCards();
            for (int i = 0; i < ipcicardes - 1; i++)
            {
                C_Ip_Address.Items.Insert(i, i.ToString());
            }
        }

        private void C_Open_Pci_Click(object sender, EventArgs e)       //连接PCI卡
        {
            if (g_handle == (IntPtr)0)
            {
                C_Close_Card_Click(sender, e);
            }
            zmcaux.ZAux_OpenPci(Convert.ToUInt32(C_Pci_Id.Text), out g_handle);
            if (g_handle != (IntPtr)0)
            {
                timer1.Start();
                ParamIni();
                this.Text = "已连接";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            float[] currentPosList = new float[11];
            zmcaux.ZAux_GetModbusDpos(g_handle, currentPosList.Length, currentPosList);
            for(int i = 0; i < 10; i++)
            {
                int axisNum = i;
                if (i > 4) axisNum = axisNum + 1;
                AxisDpos[i].Text = currentPosList[axisNum].ToString();
            }
        }

        private void C_Close_Card_Click(object sender, EventArgs e)     //断开控制器连接
        {
            //断开链接
            timer1.Stop();
            zmcaux.ZAux_Close(g_handle);
            g_handle = (IntPtr)0;
            this.Text = "未连接";
        }

        private void Btn_SetZero_Click(object sender, EventArgs e)
        {
            foreach (Int32 virAxisNum in virAxisList)
            {
                zmcaux.ZAux_Direct_SetDpos(g_handle, virAxisNum, 0);
            }
            foreach (Int32 AxisNum in realAxisList)
            {
                zmcaux.ZAux_Direct_SetDpos(g_handle, AxisNum, 0);
            }
        }

        private void C_StopMove_Click(object sender, EventArgs e)
        {
            ThreadFlag = false;
            ThreadDrawId.Abort();
            ExitFrame();
        }

        private void C_Test_Click(object sender, EventArgs e)
        {
            frame();    //进入逆解模式
            ThreadDrawId = new Thread(new ThreadStart(auto_move));
            ThreadDrawId.Start();
            
        }

        private void auto_move()
        {
            
            ThreadFlag = true;
            zmcaux.ZAux_Direct_SetSpeed(g_handle, virAxisX, Convert.ToSingle(C_AutoSpeed.Text));
            zmcaux.ZAux_Direct_SetAccel(g_handle, virAxisX, Convert.ToSingle(C_AutoAccel.Text));
            zmcaux.ZAux_Direct_SetDecel(g_handle, virAxisX, Convert.ToSingle(C_AutoDecel.Text));

            while (ThreadFlag)
            {
                zmcaux.ZAux_Direct_MoveAbs(g_handle, virAxisList.Length, virAxisList, new float[] { 0, 0, 0, 0, 0 });//走到零位
                while (true)
                { if (checkFrameAxisIdleState() == true) break; }
                Thread.Sleep(2);
                string cmdbuff = "VECTOR_MOVED(5)=0  ";
                UInt32 uiResponseLength = 2048;
                StringBuilder psResponse = new StringBuilder((Int32)uiResponseLength);
                Int32 iresult = zmcaux.ZAux_Execute(g_handle, cmdbuff, psResponse, uiResponseLength);

                zmcaux.ZAux_Direct_HwPswitch2(g_handle, virAxisX, 2, 0, 0, 0, 0, 0, 0);     //清除前面的比较输出指令
                zmcaux.ZAux_Direct_HwPswitch2(g_handle, virAxisX, 5, Convert.ToInt16(C_OpNum.Text), 0, 50, Convert.ToSingle(C_repes.Text), Convert.ToSingle(C_cycledis.Text), Convert.ToSingle(C_ondis.Text));
                zmcaux.ZAux_Trigger(g_handle);
                Int32 mSphericalMode = 0;  //MSpherical运动指令所使用的参数, 模式设置为1的时候代表当前点，圆心，终点定圆弧
                                           //相对运动走轨迹
                zmcaux.ZAux_Direct_Move(g_handle, virAxisList.Length, virAxisList, new float[] { 0, -50, 0, 0, 0 });
                zmcaux.ZAux_Direct_Move(g_handle, virAxisList.Length, virAxisList, new float[] { 50, 0, 0, 5, 45 });
                iresult = zmcaux.ZAux_Direct_MSpherical(g_handle, virAxisList.Length, virAxisList, 0, 100, 0, 50, 50, 0, mSphericalMode, 5, 90);
                zmcaux.ZAux_Direct_Move(g_handle, virAxisList.Length, virAxisList, new float[] { -100, 0, 0, 5, 90 });
                iresult = zmcaux.ZAux_Direct_MSpherical(g_handle, virAxisList.Length, virAxisList, 0, -100, 0, -50, -50, 0, mSphericalMode, 5, 90);
                zmcaux.ZAux_Direct_Move(g_handle, virAxisList.Length, virAxisList, new float[] { 50, 0, 0, 5, 45 });
                while (true)
                { if (checkFrameAxisIdleState() == true) break; }
            }
        }
        private void NoFrameModeBtn_Click(object sender, EventArgs e)
        {
            ExitFrame();
        }

        private void frameModeBtn_Click(object sender, EventArgs e)
        {
            frame();
        }

        private void reFrameModeBtn_Click(object sender, EventArgs e)
        {
            reFrame();
        }

        private void ParamIni()
        {
            //所有电机停止, 除了起急停的作用, 还可以解除电机的正反解模式
            zmcaux.ZAux_Direct_Rapidstop(g_handle, 2);
            //虚拟轴的Atype(轴类型)全部设置为0
            zmcaux.ZAux_Direct_SetAtype(g_handle, virAxisX, 0);
            zmcaux.ZAux_Direct_SetAtype(g_handle, virAxisY, 0);
            zmcaux.ZAux_Direct_SetAtype(g_handle, virAxisZ, 0);
            zmcaux.ZAux_Direct_SetAtype(g_handle, virAxisA, 0);
            zmcaux.ZAux_Direct_SetAtype(g_handle, virAxisC, 0);

            //虚拟轴的直线轴的脉冲当量设置为1000, 旋转虚拟轴的脉冲当量设置和物理旋转轴的脉冲当量一致
            zmcaux.ZAux_Direct_SetUnits(g_handle, virAxisX, 1000);
            zmcaux.ZAux_Direct_SetUnits(g_handle, virAxisY, 1000);
            zmcaux.ZAux_Direct_SetUnits(g_handle, virAxisZ, 1000);
            zmcaux.ZAux_Direct_SetUnits(g_handle, virAxisA, 3200/360);
            zmcaux.ZAux_Direct_SetUnits(g_handle, virAxisC, 3200 / 360);

            zmcaux.ZAux_Direct_SetUnits(g_handle, axisX, 3200 / 10);
            zmcaux.ZAux_Direct_SetUnits(g_handle, axisY, 3200 / 10);
            zmcaux.ZAux_Direct_SetUnits(g_handle, axisZ, 3200 / 10);
            zmcaux.ZAux_Direct_SetUnits(g_handle, axisA, 3200 / 360);
            zmcaux.ZAux_Direct_SetUnits(g_handle, axisC, 3200 / 360);


            //详情请参考机械手手册
            centY1 = 0;   //零点时， yz 平面内， rx 旋转轴的旋转中心与原点Y 方向的偏移
            centZ1 = 0;   //零点时， yz 平面内， rx 旋转轴的旋转中心与原点Z 方向的偏移
            centX2 = 0;   //零点时， xy 平面内， rz 旋转轴的旋转中心与原点X 方向的偏移
            centY2 = 0;   //零点时， xy 平面内， rz 旋转轴的旋转中心与原点Y 方向的偏移
            pulse1onecircle = 3200 ;  //旋转台 1 的一圈脉冲数
            pulse2onecircle = 3200 ;  //旋转台 2 的一圈脉冲数

            ifAntiClock1 = 0;
            ifAntiClock2 = 1;
            float[] frameParam = new float[] { centY1, centZ1, pulse1onecircle, ifAntiClock1, centX2, centY2, pulse2onecircle, ifAntiClock2 };
            zmcaux.ZAux_Direct_SetTable(g_handle, frameParamTB, frameParam.Length, frameParam);   //把frame参数放入控制器的Table寄存器内
        }


        //进入正解模式
        private void reFrame()
        {
            ExitFrame();
            ParamIni();
            //进入正逆解模式之前, 电机必须处于停止状态, 可以故意发送cancel指令, 等待处于idle状态后再执行如下指令
            zmcaux.ZAux_Direct_Connreframe(g_handle, virAxisList.Length, virAxisList, frameType, frameParamTB, realAxisList.Length, realAxisList);
            //AC轴同步到虚拟AC上
            zmcaux.ZAux_Direct_Connect(g_handle, 1, axisA, virAxisA);
            zmcaux.ZAux_Direct_Connect(g_handle, 1, axisC, virAxisC);
            //正式工作的时候很多时候都是处于逆解模式. 但不能使用逆解模式后直接使用运动指令, 一定要等待2ms以上.
            Thread.Sleep(2);
            Int32 virAxisLoaded = 0;
            //正解的时候判断虚拟轴Loaded状态, 逆解的时候判断物理轴(关节轴)状态
            while (true)
            {
                zmcaux.ZAux_Direct_GetLoaded(g_handle, virAxisList[0], ref virAxisLoaded);
                if (virAxisLoaded != 0)
                    break;
            }
            frameMode = 0;   //当前处于正解模式标志
            reFrameMode = 1;
            FrameStatus.Text = "当前机械手状态:正解";
            //关节轴让操作
            for (int i = 0; i < 5; i++)
            {
                FButton[i].Enabled = true;
                RButton[i].Enabled = true;
                AxisSpeed[i].Enabled = true;
                AxisAccel[i].Enabled = true;
                AxisDecel[i].Enabled = true;
            }

            //虚拟轴不让操作
            for (int i = 5;i < 10;i++)
            {
                FButton[i].Enabled = false;
                RButton[i].Enabled = false;
                AxisSpeed[i].Enabled = false;
                AxisAccel[i].Enabled = false;
                AxisDecel[i].Enabled = false;
            }

        }

        //进入逆解模式
        private void frame()
        {
            ExitFrame();
            ParamIni();
            //进入正解模式之前, 电机必须处于停止状态, 可以故意发送cancel指令, 等待处于idle状态后再执行如下指令
            zmcaux.ZAux_Direct_Connframe(g_handle, realAxisList.Length, realAxisList, frameType, frameParamTB, virAxisList.Length, virAxisList);
            //虚拟AC轴同步到AC上
            zmcaux.ZAux_Direct_Connect(g_handle, 1, virAxisA, axisA);
            zmcaux.ZAux_Direct_Connect(g_handle, 1, virAxisC, axisC);
            //正式工作的时候很多时候都是处于逆解模式. 但不能使用逆解模式后直接使用运动指令, 一定要等待2ms以上.
            Thread.Sleep(2);
            Int32 realAxisLoaded = 0;
            //正解的时候判断虚拟轴Loaded状态, 逆解的时候判断物理轴(关节轴)状态
            while (true)
            {
                zmcaux.ZAux_Direct_GetLoaded(g_handle, realAxisList[0], ref realAxisLoaded);
                if (realAxisLoaded != 0)
                    break;
            }
            frameMode = 1;   //当前处于逆解模式标志
            reFrameMode = 0;
            FrameStatus.Text = "当前机械手状态:逆解";

            //关节轴不让操作
            for (int i = 0; i < 5; i++)
            {
                FButton[i].Enabled = false;
                RButton[i].Enabled = false;
                AxisSpeed[i].Enabled = false;
                AxisAccel[i].Enabled = false;
                AxisDecel[i].Enabled = false;
            }

            //虚拟轴让操作
            for (int i = 5; i < 10; i++)
            {
                FButton[i].Enabled = true;
                RButton[i].Enabled = true;
                AxisSpeed[i].Enabled = true;
                AxisAccel[i].Enabled = true;
                AxisDecel[i].Enabled = true;
            }
        }

        private void ExitFrame()
        {
            zmcaux.ZAux_Direct_CancelAxisList(g_handle, realAxisList.Length, realAxisList, 2);
            zmcaux.ZAux_Direct_CancelAxisList(g_handle, virAxisList.Length, virAxisList, 2);
            frameMode = 0;
            reFrameMode = 0;
            FrameStatus.Text = "当前机械手状态:无连接";
            Thread.Sleep(10);
            while (true)
            { if (checkFrameAxisIdleState() == true) break; }
            //全部轴让操作
            for (int i = 0; i < 10; i++)
            {
                FButton[i].Enabled = true;
                RButton[i].Enabled = true;
                AxisSpeed[i].Enabled = true;
                AxisAccel[i].Enabled = true;
                AxisDecel[i].Enabled = true;
            }
        }

        private bool checkFrameAxisIdleState()
        {
            Int32[] axisIdleState = new Int32[32];
            //GetMultiParam单个函数可以读取所有轴的轴状态,这里读取32个轴的
            GetMultiParam(g_handle, "Idle", 32, axisIdleState);
            if (frameMode == 1)  //逆解模式下判断虚拟轴
            {
                foreach (Int32 virAxisNum in virAxisList)
                {
                    if (axisIdleState[virAxisNum] != -1)
                    {

                        return false;
                    }
                }
            }
            else if (reFrameMode == 1)  //正解模式下判断物理关节轴
            {
                foreach (Int32 realAxisNum in realAxisList)
                {
                    if (axisIdleState[realAxisNum] != -1)
                    {

                        return false;
                    }
                }
            }
            else  //正逆解模式都不在, 就全判断
            {
                foreach (Int32 virAxisNum in virAxisList)
                {
                    if (axisIdleState[virAxisNum] != -1)
                    {

                        return false;
                    }
                }
                foreach (Int32 realAxisNum in realAxisList)
                {
                    if (axisIdleState[realAxisNum] != -1)
                    {

                        return false;
                    }
                }
            }
            return true;
        }

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

        //点动处理
        //关节正向按下
        private void FB_MouseDown(object sender, MouseEventArgs e)
        {   
            for(int i = 0; i < FButton.Length; i++)
            {
                if((Button)sender == FButton[i])
                {
                    int axisNum = i;
                    if (i > 4) axisNum = axisNum + 1; 
                    zmcaux.ZAux_Direct_SetSpeed(g_handle, axisNum, Convert.ToSingle(AxisSpeed[i].Text));
                    zmcaux.ZAux_Direct_SetAccel(g_handle, axisNum, Convert.ToSingle(AxisAccel[i].Text));
                    zmcaux.ZAux_Direct_SetDecel(g_handle, axisNum, Convert.ToSingle(AxisDecel[i].Text));
                    zmcaux.ZAux_Direct_Single_Vmove(g_handle, axisNum, 1);
                }
            }

        }

        private void FB_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < FButton.Length; i++)
            {
                if ((Button)sender == FButton[i])
                {
                    int axisNum = i;
                    if (i > 4) axisNum = axisNum + 1;
                    zmcaux.ZAux_Direct_Single_Cancel(g_handle, axisNum, 2);
                }
            }
        }

        private void RB_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < RButton.Length; i++)
            {
                if ((Button)sender == RButton[i])
                {
                    int axisNum = i;
                    if (i > 4) axisNum = axisNum + 1;
                    zmcaux.ZAux_Direct_SetSpeed(g_handle, axisNum, Convert.ToSingle(AxisSpeed[i].Text));
                    zmcaux.ZAux_Direct_SetAccel(g_handle, axisNum, Convert.ToSingle(AxisAccel[i].Text));
                    zmcaux.ZAux_Direct_SetDecel(g_handle, axisNum, Convert.ToSingle(AxisDecel[i].Text));
                    zmcaux.ZAux_Direct_Single_Vmove(g_handle, axisNum, -1);
                }
            }

        }

        private void RB_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < RButton.Length; i++)
            {
                if ((Button)sender == RButton[i])
                {
                    int axisNum = i;
                    if (i > 4) axisNum = axisNum + 1;
                    zmcaux.ZAux_Direct_Single_Cancel(g_handle, axisNum, 2);
                }
            }
        }


    }
}
