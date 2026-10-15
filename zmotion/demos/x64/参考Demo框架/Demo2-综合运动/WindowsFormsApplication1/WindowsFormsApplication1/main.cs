using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;             //线程声明
using System.Text.RegularExpressions;//正则表达式声明

using System.IO;    //文件操作声明

using cszmcaux;

/***************************************************************************/
//创建人: 杨家健
/***************************************************************************/
namespace WindowsFormsApplication1
{
    public partial class main : Form
    {


        /*
        const string Strmax_axisg = "max_axis";
        const string Strmax_motor = "max_motor";
        const string Strmax_movebuff = "max_movebuff";
        const string Strmax_in = "max_in";
        const string Strmax_out = "max_out";
        const string Strmax_ain = "max_ain";
        const string Strmax_aout = "max_aout";
        const string Strmax_pwm = "max_pwm";
        const string Strmax_slot = "max_slot";
        const string Strmax_slotecat = "max_slotecat";
        const string Strmax_comport = "max_comport";
        const string Strmax_ethport = "max_ethport";
        const string Strmax_ethcustom = "max_ethcustom";
        const string Strmax_ethiport = "max_ethiport";
        const string Strmax_flashnum = "max_flashnum";
        const string Strmax_flashsize = "max_flashsize";
        const string Strmax_nand = "max_nand";
        const string Strmax_nandremain = "max_nandremain";
        const string Strmax_softhwout = "max_softhwout";
        const string Strmax_pswitch = "max_pswitch";
        const string Strmax_file = "max_file";
        const string Strmax_3file = "max_3file";
        const string Strmax_task = "max_task";
        const string Strmax_timer = "max_timer";
        const string Strmax_loopnest = "max_loopnest";
        const string Strmax_callstack = "max_callstack";
        const string Strmax_local_of_one_sub = "max_local_of_one_sub";
        const string Strmax_vr = "max_vr";
        const string Strmax_table = "max_table";
        const string Strmax_modbusbit = "max_modbusbit";
        const string Strmax_modbusreg = "max_modbusreg";
        const string Strmax_var = "max_var";
        const string Strmax_array = "max_array";
        const string Strmax_arrayspace = "max_arrayspace";
        const string Strmax_sub = "max_sub";
        const string Strmax_edgescan = "max_edgescan";
        const string Strmax_lablelength = "max_lablelength";
        const string Strmax_hmi = "max_hmi";
        const string Strmax_zvlatch = "max_zvlatch";
        const string Strmax_zvtask = "max_zvtask";*/

        const int max_axis = 0; //常量定义
        const int max_motor = 1;
        const int max_movebuff = 2;
        const int max_in = 3;
        const int max_out = 4;
        const int max_ain = 5;
        const int max_aout = 6;
        const int max_pwm = 7;
        const int max_slot = 8;
        const int max_slotecat = 9;
        const int max_comport = 10;
        const int max_ethport = 11;
        const int max_ethcustom = 12;
        const int max_ethiport = 13;
        const int max_flashnum = 14;
        const int max_flashsize = 15;
        const int max_nand = 16;
        const int max_nandremain = 17;
        const int max_softhwout = 18;
        const int max_pswitch = 19;
        const int max_file = 20;
        const int max_3file = 21;
        const int max_task = 22;
        const int max_timer = 23;
        const int max_loopnest = 24;
        const int max_callstack = 25;
        const int max_local_of_one_sub = 26;
        const int max_vr = 27;
        const int max_table = 28;
        const int max_modbusbit = 29;
        const int max_modbusreg = 30;
        const int max_var = 31;
        const int max_array = 32;
        const int max_arrayspace = 33;
        const int max_sub = 34;
        const int max_edgescan = 35;
        const int max_lablelength = 36;
        const int max_hmi = 37;
        const int max_zvlatch = 38;
        const int max_zvtask = 39;
        public string[] Max_ParamName ={ "max_axis","max_motor", "max_movebuff","max_in", "max_out","max_ain", "max_aout",
        "max_pwm","max_slot", "max_slotecat", "max_comport", "max_ethport", "max_ethcustom","max_ethiport", "max_flashnum",
        "max_flashsize", "max_nand","max_nandremain","max_softhwout","max_pswitch", "max_file","max_3file","max_task", 
        "max_timer","max_loopnest", "max_callstack","max_local of one sub", "max_vr", "max_table","max_modbusbit","max_modbusreg",
        "max_var","max_array","max_arrayspace","max_sub","max_edgescan","max_lablelength","max_hmi","max_zvlatch", "max_zvtask"};

        public int[] Max_ParamArry = new int[64];

        public IntPtr g_handle; //声明控制器连接句柄
        public int ret;         //调试时，查看返回值
        public int ret1;         //掉线检测返回值
        public string Adrr;     //连接的IP地址
        public int connect_mode = 0; //'链接方式，1-网口，2-串口,3-pci,4-local
        public uint com;            //串口号
        public uint baudrate;       //波特率
        public uint pci;            //pci号
        public int loaded;

        ushort Virtuaxiss;  //最大轴数
        byte[] Motor = new byte[1];//实际轴数
        byte[] max_io = new byte[4];//最大IO
        public bool Thread_flag1 = true;         //调试时，查看返回值

        //轴参数定义
        public int Aparanum = 0;                    //轴号切换技术变量
        public bool Set_spara = false;              //单次设置标志位
        public int[] atype = new int[128];          //轴类型
        public int[] pul_mode = new int[128];       //脉冲输出模式

        public int[] rev_in = new int[128];         //正限位
        public int[] fwd_in = new int[128];         //负限位
        public int[] datum_in = new int[128];       //原点
        public int[] alm_in = new int[128];       //原点

        public int[] idle = new int[128];         //负限位

        public Int32[] axisstatus = new Int32[128];       //轴状态显示
        public Int32[] mtype = new Int32[128];       //轴当前运动类型显示

        public float[] units = new float[128];      //脉冲当量
        public float[] speed = new float[128];      //速度
        public float[] accel = new float[128];      //加速度
        public float[] decel = new float[128];      //减速度
        public float[] fastdec = new float[128];    //快减减速度
        public float[] sramp = new float[128];      //S曲线时间

        public float[] vp_speed = new float[128];       //控制器命令速度
        public float[] mspeed = new float[128];       //编码器反馈速度

        public float[] dpos = new float[128];       //控制器命令位置
        public float[] mpos = new float[128];       //编码器反馈位置

        public int move_mode = 0;                //运动方式相对还是绝对
        public int singlemove_mode = 0;                //运动方式相对还是绝对
        public int[] axis_list = new int[4];     //运动轴列表
        public int[] axis_clist = new int[2];     //运动轴列表
        public int single_axis = 0;              //单轴轴号
        public float[] single_speed = new float[128];  //单轴运动位置
        public float[] single_dist = new float[128];   //单轴运动位置
        public float[] v_dist = new float[4];    //直线插补位置
        public float[] v_cdist = new float[6];    //直线插补位置
        public float cmove_speed = 100;           //插补速度



        //控件数组定义
        ComboBox[] c_atype = new ComboBox[4];           //轴类型
        TextBox[] c_pulsemode = new TextBox[4];         //脉冲模式
        TextBox[] c_units = new TextBox[4];             //脉冲当量
        TextBox[] c_speed = new TextBox[4];             //速度
        TextBox[] c_acc = new TextBox[4];              //加速度
        TextBox[] c_dec = new TextBox[4];              //减速度
        TextBox[] c_rev = new TextBox[4];              //正限位
        TextBox[] c_fwd = new TextBox[4];              //负限位
        TextBox[] c_sramp = new TextBox[4];            //S曲线时间
        TextBox[] c_datumin = new TextBox[4];            //回零IN
        TextBox[] c_almin = new TextBox[4];            //报警IN
        TextBox[] c_fastdec = new TextBox[4];            //报警IN

        Label[] c_axisn = new Label[4];               //轴参数轴号显示
        Label[] c_dpos = new Label[4];               //控制器命令坐标显示
        Label[] c_mpos = new Label[4];               //编码器反馈坐标显示
        Label[] c_idle = new Label[4];               //运动状态显示
        Label[] c_axisstatus = new Label[4];         //轴状态显示

        Label[] cout_op = new Label[30];               //输出口状态显示面板，画图用
        Label[] name_op = new Label[30];               //输出口状态显示面板，画图用
        Label[] name_in = new Label[30];               //输出口状态显示面板，画图用
        public byte[] in_state = new byte[32];                  //输入口状态
        public byte[] out_state = new byte[16];                //输出口状态
        public byte[] Control_state = new byte[1];                //输出口状态
        public int[] even_state = new int[32];                  //反转状态

        public int home_axiss;
        public uint dowm_satus = 0;
        public float[] home_speed = new float[128];
        public float[] home_creep = new float[128];
        public int[] home_mode = new int[128];
        public Thread thread;
        public int thread_flag = 0;

        public void init_contrl()    //轴参数控件初始化
        {
            c_axisn[0] = label27;
            c_axisn[1] = label26;
            c_axisn[2] = label25;
            c_axisn[3] = label35;

            c_atype[0] = atype0;
            c_atype[1] = atype1;
            c_atype[2] = atype2;
            c_atype[3] = atype3;

            c_pulsemode[0] = pul_mode0;
            c_pulsemode[1] = pul_mode1;
            c_pulsemode[2] = pul_mode2;
            c_pulsemode[3] = pul_mode3;

            c_units[0] = units0;
            c_units[1] = units1;
            c_units[2] = units2;
            c_units[3] = units3;

            c_speed[0] = speed0;
            c_speed[1] = speed1;
            c_speed[2] = speed2;
            c_speed[3] = speed3;

            c_acc[0] = acc0;
            c_acc[1] = acc1;
            c_acc[2] = acc2;
            c_acc[3] = acc3;

            c_dec[0] = dec0;
            c_dec[1] = dec1;
            c_dec[2] = dec2;
            c_dec[3] = dec3;

            c_fwd[0] = FWD_IN0;
            c_fwd[1] = FWD_IN1;
            c_fwd[2] = FWD_IN2;
            c_fwd[3] = FWD_IN3;

            c_rev[0] = REV_IN0;
            c_rev[1] = REV_IN1;
            c_rev[2] = REV_IN2;
            c_rev[3] = REV_IN3;

            c_datumin[0] = DATUM_IN0;
            c_datumin[1] = DATUM_IN1;
            c_datumin[2] = DATUM_IN2;
            c_datumin[3] = DATUM_IN3;

            c_almin[0] = Alm_IN0;
            c_almin[1] = Alm_IN1;
            c_almin[2] = Alm_IN2;
            c_almin[3] = Alm_IN3;

            c_fastdec[0] = Fastdec0;
            c_fastdec[1] = Fastdec1;
            c_fastdec[2] = Fastdec2;
            c_fastdec[3] = Fastdec3;

            c_sramp[0] = SRAMP0;
            c_sramp[1] = SRAMP1;
            c_sramp[2] = SRAMP2;
            c_sramp[3] = SRAMP3;

            c_dpos[0] = c_dpos0;
            // c_dpos[1] = c_dpos1;
            //c_dpos[2] = c_dpos2;
            //c_dpos[3] = c_dpos3;

            c_mpos[0] = c_mpos0;
            //c_mpos[1] = c_mpos1;
            // c_mpos[2] = c_mpos2;
            //c_mpos[3] = c_mpos3;

            c_idle[0] = c_idle0;
            // c_idle[1] = c_idle1;
            //  c_idle[2] = c_idle2;
            //  c_idle[3] = c_idle3;

            c_axisstatus[0] = c_axisstatus0;
            // c_axisstatus[1] = c_axisstatus1;
            // c_axisstatus[2] = c_axisstatus2;
            //  c_axisstatus[3] = c_axisstatus3;

            cout_op[0] = op0;
            cout_op[1] = op1;
            cout_op[2] = op2;
            cout_op[3] = op3;
            cout_op[4] = op4;
            cout_op[5] = op5;
            cout_op[6] = op6;
            cout_op[7] = op7;
            cout_op[8] = op8;
            cout_op[9] = op9;
            cout_op[10] = op10;
            cout_op[11] = op11;
            cout_op[12] = op12;
            cout_op[13] = op13;
            cout_op[14] = op14;
            cout_op[15] = op15;
            cout_op[16] = op16;
            cout_op[17] = op17;
            cout_op[18] = op18;
            cout_op[19] = op19;
            cout_op[20] = op20;
            cout_op[21] = op21;
            cout_op[22] = op22;
            cout_op[23] = op23;
            cout_op[24] = op24;
            cout_op[25] = op25;
            cout_op[26] = op26;
            cout_op[27] = op27;
            cout_op[28] = op28;
            cout_op[29] = op29;


            name_op[0] = label53;
            name_op[1] = label54;
            name_op[2] = label55;
            name_op[3] = label56;
            name_op[4] = label57;
            name_op[5] = label58;
            name_op[6] = label64;
            name_op[7] = label63;
            name_op[8] = label62;
            name_op[9] = label61;
            name_op[10] = label60;
            name_op[11] = label59;
            name_op[12] = label76;
            name_op[13] = label75;
            name_op[14] = label74;
            name_op[15] = label73;
            name_op[16] = label72;
            name_op[17] = label71;
            name_op[18] = label88;
            name_op[19] = label87;
            name_op[20] = label86;
            name_op[21] = label85;
            name_op[22] = label84;
            name_op[23] = label83;
            name_op[24] = label100;
            name_op[25] = label99;
            name_op[26] = label98;
            name_op[27] = label97;
            name_op[28] = label96;
            name_op[29] = label95;


            name_in[0] = label70;
            name_in[1] = label69;
            name_in[2] = label68;
            name_in[3] = label67;
            name_in[4] = label66;
            name_in[5] = label65;
            name_in[6] = label82;
            name_in[7] = label81;
            name_in[8] = label80;
            name_in[9] = label79;
            name_in[10] = label78;
            name_in[11] = label77;
            name_in[12] = label94;
            name_in[13] = label93;
            name_in[14] = label92;
            name_in[15] = label91;
            name_in[16] = label90;
            name_in[17] = label89;
            name_in[18] = label106;
            name_in[19] = label105;
            name_in[20] = label104;
            name_in[21] = label103;
            name_in[22] = label102;
            name_in[23] = label101;
            name_in[24] = label119;
            name_in[25] = label118;
            name_in[26] = label117;
            name_in[27] = label116;
            name_in[28] = label115;
            name_in[29] = label114;
        }
        //form1窗体构造函数
        public main()
        {
            InitializeComponent();

        }

        //下拉搜索当前可连接网络
        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            int i, num;
            string[] sArray;
            //一个IP地址字符串最多16个字符，最多同网段下c类ip地址为256个，每个ip地址间隔一个空格，最后换行符。所以最多字符只要16*256+256+1=4096+256+1,
            StringBuilder buffer = new StringBuilder(4096 + 256 + 1);
            string buff = "";
            ret = zmcaux.ZAux_SearchEthlist(buffer, 4096 + 256 + 1, 200);//搜索当前网段下的IP地址
            if (0 != ret)
            {
                MessageBox.Show("检索网段错误！");
            }
            else
            {
                buff += buffer;//转化为string
                sArray = buff.Split(' ');//切割字符串
                num = buff.Split(' ').Length;//获取字符串数量

                comboBox1.Items.Clear();//清空控件内容

                comboBox1.Items.Insert(0, "127.0.0.1");//把仿真IP地址加到控件第0行显示

                //依次把搜索到的IP地址加到控件显示
                for (i = 0; i < num - 1; i++)
                {
                    comboBox1.Items.Insert(i + 1, sArray[i].ToString());
                }
            }


        }
        //网口连接
        private void button1_Click(object sender, EventArgs e)
        {
            byte[] tempbuff = new byte[257];               //接收的返回的字符
            if (g_handle == (IntPtr)0)
            {
                Adrr = comboBox1.Text;
                if (Adrr == "127.0.0.1")//仿真器连接
                {
                    zmcaux.ZAux_OpenEth(Adrr, out g_handle);
                }
                else//控制器连接连接
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(Adrr, "[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}")) //网口字符串格式判断
                    {
                        string[] ips = Adrr.Split('.');
                        if (ips.Length == 4 || ips.Length == 6)
                        {
                            if (System.Int32.Parse(ips[0]) < 256 && System.Int32.Parse(ips[1]) < 256 & System.Int32.Parse(ips[2]) < 256 & System.Int32.Parse(ips[3]) < 256)
                            {
                                //ret = zmcaux.ZAux_OpenEth(Adrr, out g_handle);
                                zmcaux.ZAux_FastOpen(2, Adrr, 1000, out g_handle);
                                if (0 != ret)
                                {
                                    MessageBox.Show("找不到控制器");
                                    return;
                                }

                            }
                            else
                            {
                                MessageBox.Show("Ip地址输入格式错误！");
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Ip地址输入格式错误！");
                            return;

                        }
                    }
                    else
                    {
                        MessageBox.Show("Ip地址输入格式错误！");
                        return;
                    }

                }
            }
            if ((IntPtr)0 != g_handle)
            {
                if (0 != connect_mode)
                {
                    MessageBox.Show("当前已建立连接");
                }
                else
                {
                    connect_mode = 1;
                    timer1.Enabled = true;//使能定时器1
                    timer2.Enabled = true;//使能定时器2
                    this.Text = "已连接,连接方式:网口";


                    StringBuilder SoftType = new StringBuilder(20);
                    StringBuilder SoftVersion = new StringBuilder(20);
                    StringBuilder ControllerId = new StringBuilder(20);

                    Virtuaxiss = 0;

                    zmcaux.ZAux_GetControllerInfo(g_handle, SoftType, SoftVersion, ControllerId);//读取控制器厂商信息
                    zmcaux.ZAux_GetSysSpecification(g_handle, ref Virtuaxiss, Motor, max_io);

                    StringBuilder resstring = new StringBuilder(2048);
                    int i, j;
                    for (i = 0; i < Max_ParamArry.Length - 1; i++)
                    {
                        Max_ParamArry[i] = 0;
                    }
                    ret = zmcaux.ZAux_Execute(g_handle, "?*max", resstring, 2048);//读取控制卡参数最大值

                    j = 0;


                    if (ret == 0)
                    {
                        string tempstring = resstring.ToString();    //StringBuilder转换成string类型
                        string[] arrstring = tempstring.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);//字符串切割，获取max数量
                        // MessageBox.Show(arrstring.Length.ToString());
                        for (i = 0; i < arrstring.Length - 1; i++)
                        {

                            if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_in"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_in]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_out"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_out]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_ain"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_ain]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_aout"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_aout]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_slotecat"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_slotecat]);

                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_nand"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split('K')[0].ToString(), out Max_ParamArry[max_nand]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_nandremain"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split('K')[0].ToString(), out Max_ParamArry[max_nandremain]);
                            }
                            else
                            {
                                j = 0;
                                while (true)
                                {
                                    if (j >= Max_ParamName.Length) //达到长度跳出循环
                                    {
                                        break;
                                    }
                                    if (string.Equals(arrstring[i].Split(':')[0].ToString(), Max_ParamName[j]))//字符串比较
                                    {
                                        Int32.TryParse(arrstring[i].Split(':')[1].ToString(), out Max_ParamArry[j]);
                                        j = j + 1;
                                        break;
                                    }
                                    else
                                    {
                                        j = j + 1;
                                        if (j >= Max_ParamName.Length)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }

                    c_type.Text = "";
                    c_id.Text = "";
                    c_version.Text = "";
                    c_maxVirtuaxiss.Text = "";
                    c_maxMotor.Text = "";
                    c_maxIn.Text = "";
                    c_maxOut.Text = "";
                    c_maxAin.Text = "";
                    c_maxAout.Text = "";
                    c_type.Text += SoftType;
                    c_id.Text += ControllerId;
                    c_version.Text += SoftVersion;
                    c_maxVirtuaxiss.Text += Virtuaxiss;
                    c_maxMotor.Text += Motor[0];
                    c_maxIn.Text += max_io[0];
                    c_maxOut.Text += max_io[1];
                    c_maxAin.Text += max_io[2];
                    c_maxAout.Text += max_io[3];


                    for (i = 0; i < 30; i++)
                    {
                        name_in[i].Text = "";
                        name_op[i].Text = "";
                    }
                    for (i = 0; i < max_io[1]; i++)
                    {
                        if (i < 30)
                        {
                            name_op[i].Text = "out" + i.ToString();
                        }

                    }
                    for (i = 0; i < max_io[0]; i++)
                    {
                        if (i < 30)
                        {
                            name_in[i].Text = "in" + i.ToString();
                        }

                    }
                    single_axis0.Items.Clear();
                    for (i = 0; i < Motor[0]; i++)
                    {
                        single_axis0.Items.Add(i);
                    }
                    get_para();
                    //get_moveval();
                    //set_val();
                }

            }
            else
            {
                this.Text = "未连接";
                MessageBox.Show("连接失败");
            }
        }
        //断开连接
        private void button2_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Close(g_handle);
                g_handle = (IntPtr)0;
            }
            connect_mode = 0;
            c_type.Text = "";
            c_id.Text = "";
            c_version.Text = "";
            c_maxVirtuaxiss.Text = "";
            c_maxMotor.Text = "";
            c_maxIn.Text = "";
            c_maxOut.Text = "";
            c_maxAin.Text = "";
            c_maxAout.Text = "";
            this.Text = "未连接";
            timer1.Enabled = false;
            Aparanum = 0;
        }
        //串口连接
        private void button3_Click(object sender, EventArgs e)
        {
            byte[] tempbuff = new byte[1024];               //接收的返回的字符
            if (g_handle == (IntPtr)0)
            {

                if (UInt32.TryParse(textBox1.Text, out com))
                {
                    if (UInt32.TryParse(comboBox2.Text, out baudrate))
                    {
                        ret = zmcaux.ZAux_SetComDefaultBaud(baudrate, 8, 0, 1);
                        ret = zmcaux.ZAux_OpenCom(com, out g_handle);
                    }
                    else
                    {
                        MessageBox.Show("波特率格式错误！");
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("串口号格式错误！");
                    return;
                }
                /* com = Convert.ToUInt32(textBox1.Text.Trim());

                 baudrate = Convert.ToUInt32(comboBox2.Text);

                 zmcaux.ZAux_SetComDefaultBaud(baudrate, 8, 0, 1);
                 ret=zmcaux.ZAux_OpenCom(com, out g_handle);*/
            }
            if (g_handle != (IntPtr)0)
            {
                if (0 != connect_mode)
                {
                    MessageBox.Show("当前已建立连接");
                }
                else
                {
                    connect_mode = 2;
                    this.Text = "已连接,连接方式:网口";
                    timer1.Enabled = true;//使能定时器1
                    timer2.Enabled = true;//使能定时器2
                    StringBuilder SoftType = new StringBuilder(20);
                    StringBuilder SoftVersion = new StringBuilder(20);
                    StringBuilder ControllerId = new StringBuilder(20);

                    Virtuaxiss = 0;
                    byte[] Motor = new byte[1];
                    byte[] max_io = new byte[4];

                    zmcaux.ZAux_GetControllerInfo(g_handle, SoftType, SoftVersion, ControllerId);//读取控制器厂商信息
                    zmcaux.ZAux_GetSysSpecification(g_handle, ref Virtuaxiss, Motor, max_io);
                    c_type.Text = "";
                    c_id.Text = "";
                    c_version.Text = "";
                    c_maxVirtuaxiss.Text = "";
                    c_maxMotor.Text = "";
                    c_maxIn.Text = "";
                    c_maxOut.Text = "";
                    c_maxAin.Text = "";
                    c_maxAout.Text = "";
                    c_type.Text += SoftType;
                    c_id.Text += ControllerId;
                    c_version.Text += SoftVersion;
                    c_maxVirtuaxiss.Text += Virtuaxiss;
                    c_maxMotor.Text += Motor[0];
                    c_maxIn.Text += max_io[0];
                    c_maxOut.Text += max_io[1];
                    c_maxAin.Text += max_io[2];
                    c_maxAout.Text += max_io[3];
                    StringBuilder resstring = new StringBuilder(2048);
                    int i, j;
                    for (i = 0; i < Max_ParamArry.Length - 1; i++)
                    {
                        Max_ParamArry[i] = 0;
                    }
                    ret = zmcaux.ZAux_Execute(g_handle, "?*max", resstring, 2048);//读取控制卡参数最大值

                    j = 0;


                    if (ret == 0)
                    {
                        string tempstring = resstring.ToString();    //StringBuilder转换成string类型
                        string[] arrstring = tempstring.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);//字符串切割，获取max数量
                        // MessageBox.Show(arrstring.Length.ToString());
                        for (i = 0; i < arrstring.Length - 1; i++)
                        {

                            if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_in"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_in]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_out"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_out]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_ain"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_ain]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_aout"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_aout]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_slotecat"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_slotecat]);

                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_nand"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split('K')[0].ToString(), out Max_ParamArry[max_nand]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_nandremain"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split('K')[0].ToString(), out Max_ParamArry[max_nandremain]);
                            }
                            else
                            {
                                j = 0;
                                while (true)
                                {
                                    if (j >= Max_ParamName.Length) //达到长度跳出循环
                                    {
                                        break;
                                    }
                                    if (string.Equals(arrstring[i].Split(':')[0].ToString(), Max_ParamName[j]))//字符串比较
                                    {
                                        Int32.TryParse(arrstring[i].Split(':')[1].ToString(), out Max_ParamArry[j]);
                                        j = j + 1;
                                        break;
                                    }
                                    else
                                    {
                                        j = j + 1;
                                        if (j >= Max_ParamName.Length)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }

                    for (i = 0; i < 30; i++)
                    {
                        name_in[i].Text = "";
                        name_op[i].Text = "";
                    }
                    for (i = 0; i < max_io[1]; i++)
                    {
                        if (i < 30)
                        {
                            name_op[i].Text = "out" + i.ToString();
                        }

                    }
                    for (i = 0; i < max_io[0]; i++)
                    {
                        if (i < 30)
                        {
                            name_in[i].Text = "in" + i.ToString();
                        }

                    }
                }
                //get_para();
                //get_moveval();
                //set_val();
            }
            else
            {
                this.Text = "未连接";
                MessageBox.Show("连接失败");
            }

        }
        //PCI下拉扫描PCI卡槽
        private void comboBox3_DropDown(object sender, EventArgs e)
        {
            int ipcicardes = zmcaux.ZAux_GetMaxPciCards();
            for (int i = 0; i < ipcicardes - 1; i++)
            {
                C_Pci_Id.Items.Insert(i, i.ToString());
            }
            if (ipcicardes == 0)
            {
                MessageBox.Show("当前无可连接的PCI卡！");
            }
        }

        //PCI连接
        private void button20_Click(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                if (UInt32.TryParse(C_Pci_Id.Text, out pci))
                {
                    zmcaux.ZAux_OpenPci(pci, out g_handle);

                }
                else
                {
                    MessageBox.Show("PCI卡号格式错误！");
                    return;
                }

            }
            if (g_handle != (IntPtr)0)
            {
                if (0 != connect_mode)
                {
                    MessageBox.Show("当前已建立连接");
                }
                else
                {
                    connect_mode = 3;
                    this.Text = "已连接,连接方式:PCI";
                    timer1.Enabled = true;//使能定时器1
                    timer2.Enabled = true;//使能定时器2
                    StringBuilder SoftType = new StringBuilder(20);
                    StringBuilder SoftVersion = new StringBuilder(20);
                    StringBuilder ControllerId = new StringBuilder(20);

                    Virtuaxiss = 0;
                    byte[] Motor = new byte[1];
                    byte[] max_io = new byte[4];

                    zmcaux.ZAux_GetControllerInfo(g_handle, SoftType, SoftVersion, ControllerId);//读取控制器厂商信息
                    zmcaux.ZAux_GetSysSpecification(g_handle, ref Virtuaxiss, Motor, max_io);
                    c_type.Text = "";
                    c_id.Text = "";
                    c_version.Text = "";
                    c_maxVirtuaxiss.Text = "";
                    c_maxMotor.Text = "";
                    c_maxIn.Text = "";
                    c_maxOut.Text = "";
                    c_maxAin.Text = "";
                    c_maxAout.Text = "";
                    c_type.Text += SoftType;
                    c_id.Text += ControllerId;
                    c_version.Text += SoftVersion;
                    c_maxVirtuaxiss.Text += Virtuaxiss;
                    c_maxMotor.Text += Motor[0];
                    c_maxIn.Text += max_io[0];
                    c_maxOut.Text += max_io[1];
                    c_maxAin.Text += max_io[2];
                    c_maxAout.Text += max_io[3];

                    StringBuilder resstring = new StringBuilder(2048);
                    int i, j;
                    for (i = 0; i < Max_ParamArry.Length - 1; i++)
                    {
                        Max_ParamArry[i] = 0;
                    }
                    ret = zmcaux.ZAux_Execute(g_handle, "?*max", resstring, 2048);//读取控制卡参数最大值

                    j = 0;


                    if (ret == 0)
                    {
                        string tempstring = resstring.ToString();    //StringBuilder转换成string类型
                        string[] arrstring = tempstring.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);//字符串切割，获取max数量
                        // MessageBox.Show(arrstring.Length.ToString());
                        for (i = 0; i < arrstring.Length - 1; i++)
                        {

                            if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_in"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_in]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_out"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_out]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_ain"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_ain]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_aout"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_aout]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_slotecat"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_slotecat]);

                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_nand"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split('K')[0].ToString(), out Max_ParamArry[max_nand]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_nandremain"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split('K')[0].ToString(), out Max_ParamArry[max_nandremain]);
                            }
                            else
                            {
                                j = 0;
                                while (true)
                                {
                                    if (j >= Max_ParamName.Length) //达到长度跳出循环
                                    {
                                        break;
                                    }
                                    if (string.Equals(arrstring[i].Split(':')[0].ToString(), Max_ParamName[j]))//字符串比较
                                    {
                                        Int32.TryParse(arrstring[i].Split(':')[1].ToString(), out Max_ParamArry[j]);
                                        j = j + 1;
                                        break;
                                    }
                                    else
                                    {
                                        j = j + 1;
                                        if (j >= Max_ParamName.Length)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }

                    for (i = 0; i < 30; i++)
                    {
                        name_in[i].Text = "";
                        name_op[i].Text = "";
                    }
                    for (i = 0; i < max_io[1]; i++)
                    {
                        if (i < 30)
                        {
                            name_op[i].Text = "out" + i.ToString();
                        }

                    }
                    for (i = 0; i < max_io[0]; i++)
                    {
                        if (i < 30)
                        {
                            name_in[i].Text = "in" + i.ToString();
                        }

                    }
                }
            }

        }
        //form1窗体加载函数
        private void Form1_Load(object sender, EventArgs e)
        {
            init_contrl();
        }

        //获取轴参数界面的界面值到变量
        public void get_val()
        {

            // Int32.TryParse();

            int i = 0;
            int itemp;
            float ftemp;
            bool errtemp = false;
            for (i = 0; i < 4; i++) //参数输入防呆
            {
                if (Int32.TryParse(c_atype[i].Text, out itemp)) { if (itemp < 0) { errtemp = true; } } else { errtemp = true; }
                if (Int32.TryParse(c_pulsemode[i].Text, out itemp)) { if (itemp < 0) { errtemp = true; } } else { errtemp = true; }
                if (Int32.TryParse(c_rev[i].Text, out itemp)) { if (itemp < -1) { errtemp = true; } } else { errtemp = true; }
                if (Int32.TryParse(c_fwd[i].Text, out itemp)) { if (itemp < -1) { errtemp = true; } } else { errtemp = true; }
                if (Int32.TryParse(c_datumin[i].Text, out itemp)) { if (itemp < -1) { errtemp = true; } } else { errtemp = true; }
                if (Int32.TryParse(c_almin[i].Text, out itemp)) { if (itemp < -1) { errtemp = true; } } else { errtemp = true; }

                if (float.TryParse(c_units[i].Text, out ftemp)) { if (ftemp < 0) { errtemp = true; } } else { errtemp = true; }
                if (float.TryParse(c_speed[i].Text, out ftemp)) { if (ftemp < 0) { errtemp = true; } } else { errtemp = true; }
                if (float.TryParse(c_acc[i].Text, out ftemp)) { if (ftemp < 0) { errtemp = true; } } else { errtemp = true; }
                if (float.TryParse(c_dec[i].Text, out ftemp)) { if (ftemp < 0) { errtemp = true; } } else { errtemp = true; }
                if (float.TryParse(c_sramp[i].Text, out ftemp)) { if (ftemp < 0) { errtemp = true; } } else { errtemp = true; }
                if (float.TryParse(c_fastdec[i].Text, out ftemp))
                {
                    if (ftemp < 0)
                    {
                        errtemp = true;
                    }
                }
                else
                {
                    errtemp = true;
                }
            }
            if (errtemp) { MessageBox.Show("参数输入错误！"); return; }
            for (i = 0; i < 4; i++)
            {
                //atype[i + Aparanum*4] = c_atype[i].SelectedIndex;

                atype[i + Aparanum * 4] = Convert.ToInt32(c_atype[i].Text);
                pul_mode[i + Aparanum * 4] = Convert.ToInt32(c_pulsemode[i].Text);
                units[i + Aparanum * 4] = Convert.ToSingle(c_units[i].Text);
                speed[i + Aparanum * 4] = Convert.ToSingle(c_speed[i].Text);
                accel[i + Aparanum * 4] = Convert.ToSingle(c_acc[i].Text);
                decel[i + Aparanum * 4] = Convert.ToSingle(c_dec[i].Text);
                rev_in[i + Aparanum * 4] = Convert.ToInt32(c_rev[i].Text);
                fwd_in[i + Aparanum * 4] = Convert.ToInt32(c_fwd[i].Text);
                sramp[i + Aparanum * 4] = Convert.ToSingle(c_sramp[i].Text);
                datum_in[i + Aparanum * 4] = Convert.ToInt32(c_datumin[i].Text);
                alm_in[i + Aparanum * 4] = Convert.ToInt32(c_almin[i].Text);
                fastdec[i + Aparanum * 4] = Convert.ToSingle(c_fastdec[i].Text);
            }
        }
        //设置变量显示到界面
        public void set_val()
        {
            int i = 0;
            for (i = 0; i < 4; i++)
            {


                if ((i + Aparanum * 4) >= Virtuaxiss)   //大于最大轴数则隐藏
                {
                    c_axisn[i].Visible = false;
                    c_atype[i].Visible = false;
                    c_pulsemode[i].Visible = false;
                    c_rev[i].Visible = false;
                    c_fwd[i].Visible = false;
                    c_datumin[i].Visible = false;

                    c_units[i].Visible = false;
                    c_speed[i].Visible = false;
                    c_acc[i].Visible = false;
                    c_dec[i].Visible = false;
                    c_sramp[i].Visible = false;
                    c_almin[i].Visible = false;
                    c_fastdec[i].Visible = false;
                }
                else
                {
                    c_axisn[i].Visible = true;
                    c_atype[i].Visible = true;
                    c_pulsemode[i].Visible = true;
                    c_rev[i].Visible = true;
                    c_fwd[i].Visible = true;
                    c_datumin[i].Visible = true;

                    c_units[i].Visible = true;
                    c_speed[i].Visible = true;
                    c_acc[i].Visible = true;
                    c_dec[i].Visible = true;
                    c_sramp[i].Visible = true;
                    c_almin[i].Visible = true;
                    c_fastdec[i].Visible = true;

                    c_atype[i].Text = atype[i + Aparanum * 4].ToString();
                    c_pulsemode[i].Text = pul_mode[i + Aparanum * 4].ToString();
                    c_rev[i].Text = rev_in[i + Aparanum * 4].ToString();
                    c_fwd[i].Text = fwd_in[i + Aparanum * 4].ToString();
                    c_datumin[i].Text = datum_in[i + Aparanum * 4].ToString();
                    c_almin[i].Text = alm_in[i + Aparanum * 4].ToString();

                    c_units[i].Text = units[i + Aparanum * 4].ToString("f");
                    c_speed[i].Text = speed[i + Aparanum * 4].ToString("f");
                    c_acc[i].Text = accel[i + Aparanum * 4].ToString("f");
                    c_dec[i].Text = decel[i + Aparanum * 4].ToString("f");
                    c_sramp[i].Text = sramp[i + Aparanum * 4].ToString("f");
                    c_fastdec[i].Text = fastdec[i + Aparanum * 4].ToString("f");
                }


            }

        }
        //设置控制器参数
        public void set_para()
        {
            int i = 0;
            int loopmax;
            bool err_flag;
            err_flag = false;
            if (g_handle != (IntPtr)0)
            {
                if (Set_spara)
                {
                    loopmax = 4;
                }
                else
                {
                    loopmax = Virtuaxiss;
                }
                for (i = 0; i < loopmax; i++)
                {
                    ret = zmcaux.ZAux_Direct_SetAtype(g_handle, i + Aparanum * 4, atype[i + Aparanum * 4]);
                    if (ret != 0) { err_flag = true; }
                    ret = zmcaux.ZAux_Direct_SetInvertStep(g_handle, i + Aparanum * 4, pul_mode[i + Aparanum * 4]);
                    if (ret != 0) { err_flag = true; }
                    ret = zmcaux.ZAux_Direct_SetUnits(g_handle, i + Aparanum * 4, units[i + Aparanum * 4]);
                    if (ret != 0) { err_flag = true; }
                    ret = zmcaux.ZAux_Direct_SetSpeed(g_handle, i + Aparanum * 4, speed[i + Aparanum * 4]);
                    if (ret != 0) { err_flag = true; }
                    ret = zmcaux.ZAux_Direct_SetAccel(g_handle, i + Aparanum * 4, accel[i + Aparanum * 4]);
                    if (ret != 0) { err_flag = true; }
                    ret = zmcaux.ZAux_Direct_SetDecel(g_handle, i + Aparanum * 4, decel[i + Aparanum * 4]);
                    if (ret != 0) { err_flag = true; }
                    ret = zmcaux.ZAux_Direct_SetRevIn(g_handle, i + Aparanum * 4, rev_in[i + Aparanum * 4]);
                    if (ret != 0) { err_flag = true; }
                    ret = zmcaux.ZAux_Direct_SetFwdIn(g_handle, i + Aparanum * 4, fwd_in[i + Aparanum * 4]);
                    if (ret != 0) { err_flag = true; }
                    ret = zmcaux.ZAux_Direct_SetSramp(g_handle, i + Aparanum * 4, sramp[i + Aparanum * 4]);
                    if (ret != 0) { err_flag = true; }
                    ret = zmcaux.ZAux_Direct_SetDatumIn(g_handle, i + Aparanum * 4, datum_in[i + Aparanum * 4]);
                    if (ret != 0) { err_flag = true; }
                    ret = zmcaux.ZAux_Direct_SetAlmIn(g_handle, i + Aparanum * 4, alm_in[i + Aparanum * 4]);
                    if (ret != 0) { err_flag = true; }
                    ret = zmcaux.ZAux_Direct_SetFastDec(g_handle, i + Aparanum * 4, fastdec[i + Aparanum * 4]);
                    if (ret != 0) { err_flag = true; }


                }
                if (err_flag)
                {
                    MessageBox.Show("发送设置失败！");
                }
                else
                {
                    MessageBox.Show("设置成功！");
                }
                Set_spara = true;

            }
        }
        //获取控制器参数
        public void get_para()
        {
            int i = 0;
            bool err_flag;
            err_flag = false;

            if (g_handle != (IntPtr)0)
            {
                for (i = 0; i < 4; i++)
                {
                    if ((i + Aparanum * 4) < Virtuaxiss)
                    {
                        ret = zmcaux.ZAux_Direct_GetAtype(g_handle, i + Aparanum * 4, ref atype[i + Aparanum * 4]);
                        if (ret != 0) { err_flag = true; }
                        ret = zmcaux.ZAux_Direct_GetInvertStep(g_handle, i + Aparanum * 4, ref pul_mode[i + Aparanum * 4]);
                        if (ret != 0) { err_flag = true; }
                        ret = zmcaux.ZAux_Direct_GetUnits(g_handle, i + Aparanum * 4, ref units[i + Aparanum * 4]);
                        if (ret != 0) { err_flag = true; }
                        ret = zmcaux.ZAux_Direct_GetSpeed(g_handle, i + Aparanum * 4, ref speed[i + Aparanum * 4]);
                        if (ret != 0) { err_flag = true; }
                        ret = zmcaux.ZAux_Direct_GetAccel(g_handle, i + Aparanum * 4, ref accel[i + Aparanum * 4]);
                        if (ret != 0) { err_flag = true; }
                        ret = zmcaux.ZAux_Direct_GetDecel(g_handle, i + Aparanum * 4, ref decel[i + Aparanum * 4]);
                        if (ret != 0) { err_flag = true; }
                        ret = zmcaux.ZAux_Direct_GetRevIn(g_handle, i + Aparanum * 4, ref rev_in[i + Aparanum * 4]);
                        if (ret != 0) { err_flag = true; }
                        ret = zmcaux.ZAux_Direct_GetFwdIn(g_handle, i + Aparanum * 4, ref fwd_in[i + Aparanum * 4]);
                        if (ret != 0) { err_flag = true; }
                        ret = zmcaux.ZAux_Direct_GetSramp(g_handle, i + Aparanum * 4, ref sramp[i + Aparanum * 4]);
                        if (ret != 0) { err_flag = true; }
                        ret = zmcaux.ZAux_Direct_GetDatumIn(g_handle, i + Aparanum * 4, ref datum_in[i + Aparanum * 4]);
                        if (ret != 0) { err_flag = true; }
                        ret = zmcaux.ZAux_Direct_GetAlmIn(g_handle, i + Aparanum * 4, ref alm_in[i + Aparanum * 4]);
                        if (ret != 0) { err_flag = true; }
                        ret = zmcaux.ZAux_Direct_GetFastDec(g_handle, i + Aparanum * 4, ref fastdec[i + Aparanum * 4]);
                        if (ret != 0) { err_flag = true; }
                    }

                }
                if (err_flag)
                {
                    MessageBox.Show("获取参数失败！");
                }

            }
        }
        //初始化轴参数
        private void button4_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                int i;
                i = 0;
                for (i = 0; i < Virtuaxiss; i++)
                {
                    if (i > Motor[0] - 1)
                    {
                        atype[i] = 0;
                    }
                    else
                    {
                        atype[i] = 1;
                    }

                    pul_mode[i] = 0;
                    units[i] = i + 1;
                    speed[i] = i + 1;
                    accel[i] = i + 1;
                    decel[i] = i + 1;
                    rev_in[i] = -1;
                    fwd_in[i] = -1;
                    alm_in[i] = -1;
                    datum_in[i] = -1;
                    sramp[i] = i;
                    fastdec[i] = 1000000;
                }
                //get_para();
                set_val();           //变量显示到界面
                set_para();            //发送到控制

            }
            else
            {
                MessageBox.Show("控制器未连接");
            }
        }
        //设置轴参数
        private void button5_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                Set_spara = true;
                get_val();            //读取界面上到变量
                set_para();             //发送到控制器
            }
            else
            {
                MessageBox.Show("控制器未连接");
            }
        }
        //更新界面
        private void AxisPara_Renew()
        {
            c_axisn[0].Text = "(" + (0 + Aparanum * 4) + ")";
            c_axisn[1].Text = "(" + (1 + Aparanum * 4) + ")";
            c_axisn[2].Text = "(" + (2 + Aparanum * 4) + ")";
            c_axisn[3].Text = "(" + (3 + Aparanum * 4) + ")";

            get_para();
            set_val();           //变量显示到界面
        }
        //轴号切换上一页
        private void button21_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {

                if (Aparanum == 0)
                {
                    if ((Virtuaxiss % 4) == 0)
                    {
                        Aparanum = Virtuaxiss / 4 - 1;
                    }
                    else
                    {
                        Aparanum = Virtuaxiss / 4;
                    }

                }
                else
                {
                    Aparanum = Aparanum - 1;
                }

                AxisPara_Renew();//更新界面


            }
            else
            {
                MessageBox.Show("控制器未连接");
            }
        }


        //轴号切换下一页
        private void button23_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {

                if ((Aparanum + 1) * 4 >= Virtuaxiss)
                {
                    Aparanum = 0;

                }
                else
                {
                    Aparanum = Aparanum + 1;
                }

                AxisPara_Renew();//更新界面
            }
            else
            {
                MessageBox.Show("控制器未连接");
            }
        }
        //导出参数
        private void button25_Click(object sender, EventArgs e)
        {
            string NewFilePath = ".\\path\\AxisPara.Bas";
            string str;
            using (StreamWriter sw = new StreamWriter(NewFilePath, false))
            {
                for (int i = 0; i < Virtuaxiss; i++)
                {
                    str = "'##########轴" + i + "##########";
                    sw.WriteLine(str);

                    str = "BASE(" + i + ")";
                    sw.WriteLine(str);
                    str = "ATYPE=" + atype[i];
                    sw.WriteLine(str);
                    str = "INVERT_STEP=" + pul_mode[i];
                    sw.WriteLine(str);
                    str = "UNITS=" + units[i];
                    sw.WriteLine(str);
                    str = "SPEED=" + speed[i];
                    sw.WriteLine(str);
                    str = "ACCEL=" + accel[i];
                    sw.WriteLine(str);
                    str = "DECEL=" + decel[i];
                    sw.WriteLine(str);
                    str = "SRAMP=" + sramp[i];
                    sw.WriteLine(str);
                    str = "FWD_IN=" + fwd_in[i];
                    sw.WriteLine(str);
                    str = "REV_IN=" + rev_in[i];
                    sw.WriteLine(str);
                    str = "DATUM_IN=" + datum_in[i];
                    sw.WriteLine(str);
                    str = "ALM_IN=" + alm_in[i];
                    sw.WriteLine(str);
                    str = "FASTDEC=" + fastdec[i];
                    sw.WriteLine(str);
                    str = "";
                    sw.WriteLine(str);
                }


                //sw.Flush();  
                // sw.WriteLine("456");
                sw.Close();
                MessageBox.Show("导出成功！");
            }

        }
        private void button27_Click(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("控制器未连接!", "提示");
            }
            else
            {
                int ret = 0;
                string strFilePath;
                string tempstr;
                string tempstr1;
                int add = 0;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = "\\";
                openFileDialog1.Filter = "配置文件(*.bas)|*.bas";
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FilterIndex = 1;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)            //打开配置文件
                {
                    strFilePath = openFileDialog1.FileName;
                    BasText.Text = strFilePath;

                    tempstr = strFilePath.Substring(strFilePath.Length - 8, 4);
                    MessageBox.Show(tempstr);
                    if (string.Equals(tempstr, "(VR)"))
                    {
                        //try{int i=0;using (StreamReader sr = new StreamReader("TestFile.txt"))}{String line;while ((line = sr.ReadLine()) != null)
                        try
                        {

                            using (StreamReader sr = new StreamReader(strFilePath))
                            {
                                while (true)
                                {
                                    tempstr1 = sr.ReadLine();
                                    if (tempstr1 != null)
                                    {
                                        add++;
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }


                                sr.Close();
                            }


                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("加载失败！", ex.ToString());
                            return;
                        }
                    }
                    if (add != Max_ParamArry[max_vr])
                    {
                        MessageBox.Show("该文件VR数量大于该控制器最大数量！");
                        return;
                    }
                    ret = zmcaux.ZAux_BasDown(g_handle, strFilePath, dowm_satus);               //下载到ROM
                    if (ret != 0)
                    {
                        MessageBox.Show("文件下载失败!", "提示");
                        BasText.Text = BasText.Text + "\n 文件加载失败！";

                    }
                    else
                    {
                        AxisPara_Renew();//更新页面
                        BasText.Text = BasText.Text + "\n 加载成功！";
                    }
                }

            }
        }
        //ram
        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            dowm_satus = 0;
        }
        //rom
        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            dowm_satus = 1;
        }
        private void single_axis0_SelectedIndexChanged(object sender, EventArgs e)
        {
            single_axis = Convert.ToInt32(single_axis0.Text);
            label19.Text = single_axis0.Text;
            label18.Text = single_axis0.Text;
            label137.Text = single_axis0.Text;
            label143.Text = single_axis0.Text;

            zmcaux.ZAux_Direct_GetDatumIn(g_handle, single_axis, ref datum_in[single_axis]);
            zmcaux.ZAux_Direct_GetRevIn(g_handle, single_axis, ref rev_in[single_axis]);
            zmcaux.ZAux_Direct_GetFwdIn(g_handle, single_axis, ref fwd_in[single_axis]);

            label133.Text = datum_in[single_axis].ToString();
            label134.Text = fwd_in[single_axis].ToString();
            label135.Text = rev_in[single_axis].ToString();



        }
        //更新界面,轴状态运动类型切中文显示
        public void AxisMoveStasus_Renew(int val, int val1)
        {
            switch (val)
            {
                case 0:
                    c_axisstatus[0].Text = "正常";
                    c_axisstatus[0].ForeColor = System.Drawing.Color.White;
                    break;
                case 16:
                    c_axisstatus[0].Text = "正限位";
                    c_axisstatus[0].ForeColor = System.Drawing.Color.Red;
                    break;
                case 32:
                    c_axisstatus[0].Text = "负限位";
                    c_axisstatus[0].ForeColor = System.Drawing.Color.Red;
                    break;
                case 48:
                    c_axisstatus[0].Text = "正负限位";
                    c_axisstatus[0].ForeColor = System.Drawing.Color.Red;
                    break;
                case 64:
                    c_axisstatus[0].Text = "回零运动中";
                    c_axisstatus[0].ForeColor = System.Drawing.Color.White;
                    break;
                case 512:
                    c_axisstatus[0].Text = "正软限位";
                    c_axisstatus[0].ForeColor = System.Drawing.Color.Red;
                    break;
                case 1024:
                    c_axisstatus[0].Text = "负软限位";
                    c_axisstatus[0].ForeColor = System.Drawing.Color.Red;
                    break;
                case 2048:
                    c_axisstatus[0].Text = "停止运动中";
                    c_axisstatus[0].ForeColor = System.Drawing.Color.White;
                    break;
                case 4194304:
                    c_axisstatus[0].Text = "alm报警";
                    c_axisstatus[0].ForeColor = System.Drawing.Color.Red;
                    break;
                case 4194352:
                    c_axisstatus[0].Text = "alm+正负限位";
                    c_axisstatus[0].ForeColor = System.Drawing.Color.Red;
                    break;
                default:
                    c_axisstatus[0].Text = "其他:" + axisstatus[single_axis].ToString("X");
                    c_axisstatus[0].ForeColor = System.Drawing.Color.Red;
                    break;
            }
            switch (val1)
            {
                case 0:
                    c_mtype.Text = "没有运动";
                    break;
                case 1:
                    c_mtype.Text = "直线";
                    break;
                case 2:
                    c_mtype.Text = "绝对直线";
                    break;
                case 3:
                    c_mtype.Text = "圆弧螺旋";
                    break;
                case 4:
                    c_mtype.Text = "圆弧插补";
                    break;
                case 6:
                    c_mtype.Text = "直线SP";
                    break;
                case 10:
                    c_mtype.Text = "正持续运动";
                    break;
                case 11:
                    c_mtype.Text = "负持续运动";
                    break;
                case 12:
                    c_mtype.Text = "回零运动";
                    break;
                case 21:
                    c_mtype.Text = "同步运动";
                    break;
                default:
                    c_mtype.Text = "其他:" + val1.ToString();
                    break;
            }

        }
        //定时器刷新参数
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                int i = 0;
                if (tabControl1.SelectedIndex == 2)         //单轴运动界面生效
                {

                    //参状态获取刷新显示
                    zmcaux.ZAux_Direct_GetDpos(g_handle, single_axis, ref dpos[single_axis]);
                    zmcaux.ZAux_Direct_GetMpos(g_handle, single_axis, ref mpos[single_axis]);
                    zmcaux.ZAux_Direct_GetIfIdle(g_handle, single_axis, ref idle[single_axis]);
                    zmcaux.ZAux_Direct_GetAxisStatus(g_handle, single_axis, ref axisstatus[single_axis]);
                    zmcaux.ZAux_Direct_GetVpSpeed(g_handle, single_axis, ref vp_speed[single_axis]);
                    zmcaux.ZAux_Direct_GetMspeed(g_handle, single_axis, ref mspeed[single_axis]);
                    zmcaux.ZAux_Direct_GetMtype(g_handle, single_axis, ref mtype[single_axis]);

                    c_dpos[0].Text = dpos[single_axis].ToString("f");
                    c_mpos[0].Text = mpos[single_axis].ToString("f");
                    c_idle[0].Text = idle[single_axis].ToString();
                    c_vpseed.Text = vp_speed[single_axis].ToString("f");
                    c_mspeed.Text = mspeed[single_axis].ToString("f");

                    AxisMoveStasus_Renew(axisstatus[single_axis], mtype[single_axis]);//运动类型+轴状态界面显示

                    //轴参数显示：
                    zmcaux.ZAux_Direct_GetAtype(g_handle, single_axis, ref atype[single_axis]);
                    zmcaux.ZAux_Direct_GetInvertStep(g_handle, single_axis, ref pul_mode[single_axis]);
                    zmcaux.ZAux_Direct_GetUnits(g_handle, single_axis, ref units[single_axis]);
                    zmcaux.ZAux_Direct_GetSpeed(g_handle, single_axis, ref speed[single_axis]);
                    zmcaux.ZAux_Direct_GetAccel(g_handle, single_axis, ref accel[single_axis]);
                    zmcaux.ZAux_Direct_GetDecel(g_handle, single_axis, ref decel[single_axis]);

                    a_atype.Text = atype[single_axis].ToString();
                    a_units.Text = units[single_axis].ToString("f");
                    a_accel.Text = accel[single_axis].ToString("f");
                    a_decel.Text = decel[single_axis].ToString("f");
                    a_speed.Text = speed[single_axis].ToString("f");
                    switch (pul_mode[single_axis])
                    {
                        case 0:
                            a_mode.Text = "0脉冲+方向正逻辑正向";
                            break;
                        case 1:
                            a_mode.Text = "1脉冲+方向负逻辑正向";
                            break;
                        case 2:
                            a_mode.Text = "2脉冲+方向正逻辑负向";
                            break;
                        case 3:
                            a_mode.Text = "3脉冲+方向负逻辑负向";
                            break;
                        case 4:
                            a_mode.Text = "4双脉冲+方向正逻辑正向";
                            break;
                        case 5:
                            a_mode.Text = "5双脉冲+方向负逻辑正向";
                            break;
                        case 6:
                            a_mode.Text = "6脉冲+方向正逻辑负向";
                            break;
                        case 7:
                            a_mode.Text = "7脉冲+方向负逻辑负向";
                            break;

                    }
                }
                else if (tabControl1.SelectedIndex == 4)
                {
                    byte[] buff1 = new byte[20480];
                    byte[] buff2 = new byte[20480];
                    UInt32 bufflen = 0;
                    UInt32 bufflen1 = 0;
                    byte temp = 0;
                    ret = zmcaux.ZMC_ReadMessage(g_handle, buff1, 4096, ref bufflen);

                    if (ret == 0)
                    {
                        if (bufflen > 0)
                        {
                            textBox3.Text = textBox3.Text + "\n" + Regex.Replace(Encoding.Default.GetString(buff1), "(?<!\r)\n", "\r\n");
                        }
                    }
                    ret = zmcaux.ZMC_ExecuteGetReceive(g_handle, buff2, 4096, ref bufflen1, ref temp);
                    if (ret == 0)
                    {
                        if (bufflen1 > 0)
                        {
                            textBox3.Text = textBox3.Text + "\n" + Regex.Replace(Encoding.Default.GetString(buff2), "(?<!\r)\n", "\r\n");

                        }
                    }
                }

            }
        }
        //坐标清0
        private void button28_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                //home_axiss = datum_axisnum.SelectedIndex;
                home_axiss = Convert.ToInt32(single_axis0.Text);

                zmcaux.ZAux_Direct_SetDpos(g_handle, home_axiss, 0);         //设置回零高速速度
                zmcaux.ZAux_Direct_SetMpos(g_handle, home_axiss, 0);         //设置回零反找速度


            }
            else
            {
                MessageBox.Show("控制器未连接", "提示");
            }
        }
        //报警清除
        private void button33_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                home_axiss = Convert.ToInt32(single_axis0.Text);
                zmcaux.ZAux_Direct_SetParam(g_handle, "Datum", home_axiss, 0);         //设置回零高速速度

            }
            else
            {
                MessageBox.Show("控制器未连接", "提示");
            }
        }

        //单轴回零
        private void button6_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                int taxix_num, tmod, tms, i;
                float pos1, pos2;
                bool errtemp = false;
                if (Int32.TryParse(single_axis0.Text, out taxix_num)) { if (taxix_num < 0) { errtemp = true; } } else { errtemp = true; }
                if (Int32.TryParse(datum_mode.Text, out tmod)) { if (tmod < 0) { errtemp = true; } } else { errtemp = true; }
                if (float.TryParse(datum_speed.Text, out pos1)) { } else { errtemp = true; }
                if (float.TryParse(datum_creep.Text, out pos2)) { } else { errtemp = true; }


                if (errtemp) { MessageBox.Show("参数输入错误！"); return; }

                //home_axiss = datum_axisnum.SelectedIndex;
                home_axiss = Convert.ToInt32(single_axis0.Text);
                home_mode[home_axiss] = Convert.ToInt32(datum_mode.Text);
                home_speed[home_axiss] = Convert.ToSingle(datum_speed.Text);
                home_creep[home_axiss] = Convert.ToSingle(datum_creep.Text);
                if (home_mode[home_axiss] > 0)
                {
                    zmcaux.ZAux_Direct_SetSpeed(g_handle, home_axiss, home_speed[home_axiss]);         //设置回零高速速度
                    zmcaux.ZAux_Direct_SetCreep(g_handle, home_axiss, home_creep[home_axiss]);         //设置回零反找速度

                    ret = zmcaux.ZAux_Direct_Single_Datum(g_handle, home_axiss, home_mode[home_axiss]);       //回零
                    if (ret != 0)
                    {
                        MessageBox.Show("回零失败：" + ret.ToString());
                    }
                }

            }
            else
            {
                MessageBox.Show("控制器未连接", "提示");
            }

        }
        //回零停止
        private void button13_Click(object sender, EventArgs e)
        {

            if (g_handle != (IntPtr)0)
            {
                home_axiss = Convert.ToInt32(single_axis0.Text);
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, home_axiss, 2);
            }
            else
            {
                MessageBox.Show("控制器未连接", "提示");
            }
        }
        //相对运动方式
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            move_mode = 0;              //运动方式相对还是绝对
        }
        //绝对运动方式
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            move_mode = 1;              //运动方式相对还是绝对
        }
        //直线插补运动
        private void button7_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                int i;
                for (i = 0; i < 4; i++)         //轴列表
                {
                    axis_list[i] = i;
                }
                v_dist[0] = Convert.ToInt32(move_pos0.Text);
                v_dist[1] = Convert.ToSingle(move_pos1.Text);
                v_dist[2] = Convert.ToSingle(move_pos2.Text);
                v_dist[3] = Convert.ToSingle(move_pos3.Text);
                cmove_speed = Convert.ToSingle(move_speed.Text);

                if (move_mode == 0)   //相对运动
                {
                    zmcaux.ZAux_Direct_SetSpeed(g_handle, axis_list[0], cmove_speed);         //设置插补速度

                    zmcaux.ZAux_Direct_Move(g_handle, 4, axis_list, v_dist);
                }
                else               //绝对运动
                {
                    zmcaux.ZAux_Direct_SetSpeed(g_handle, axis_list[0], cmove_speed);         //设置插补速度

                    zmcaux.ZAux_Direct_MoveAbs(g_handle, 4, axis_list, v_dist);
                }

            }
            else
            {
                MessageBox.Show("控制器未连接");
            }
        }

        //圆心画弧
        private void button9_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {

                axis_clist[0] = circ_axis0.SelectedIndex;
                axis_clist[1] = circ_axis1.SelectedIndex;

                v_cdist[0] = Convert.ToSingle(v_cdist0.Text);
                v_cdist[1] = Convert.ToSingle(v_cdist1.Text);
                v_cdist[2] = Convert.ToSingle(v_cdist2.Text);
                v_cdist[3] = Convert.ToSingle(v_cdist3.Text);
                v_cdist[4] = Convert.ToSingle(v_cdist4.Text);
                v_cdist[5] = Convert.ToSingle(v_cdist5.Text);

                cmove_speed = Convert.ToSingle(move_speed.Text);

                if (move_mode == 0)   //相对运动
                {
                    zmcaux.ZAux_Direct_SetSpeed(g_handle, axis_list[0], cmove_speed);         //设置插补速度

                    zmcaux.ZAux_Direct_Move(g_handle, 2, axis_clist, v_cdist);

                    zmcaux.ZAux_Direct_MoveCirc(g_handle, 2, axis_clist, v_cdist[4], v_cdist[5], v_cdist[2], v_cdist[3], 1);
                }
                else               //绝对运动
                {
                    zmcaux.ZAux_Direct_SetSpeed(g_handle, axis_list[0], cmove_speed);         //设置插补速度

                    zmcaux.ZAux_Direct_MoveAbs(g_handle, 2, axis_clist, v_cdist);           //运动到起点

                    zmcaux.ZAux_Direct_MoveCircAbs(g_handle, 2, axis_clist, v_cdist[4], v_cdist[5], v_cdist[2], v_cdist[3], 1);
                }

            }
            else
            {
                MessageBox.Show("控制器未连接");
            }
        }
        //插补停止
        private void button8_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                axis_clist[0] = circ_axis0.SelectedIndex;
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, axis_clist[0], 2);
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, axis_list[0], 2);
            }
            else
            {
                MessageBox.Show("控制器未连接");
            }
        }
        //往复运动
        private void button30_Click(object sender, EventArgs e)
        {

            if (g_handle != (IntPtr)0)
            {
                int taxix_num, loopnum, tms, i;
                float pos1, pos2;
                bool errtemp = false;
                if (Int32.TryParse(label137.Text, out taxix_num)) { if (taxix_num < 0) { errtemp = true; } } else { errtemp = true; }
                if (Int32.TryParse(tmoveloop.Text, out loopnum)) { if (loopnum < 0) { errtemp = true; } } else { errtemp = true; }
                if (float.TryParse(tmove1.Text, out pos1)) { } else { errtemp = true; }
                if (float.TryParse(tmove2.Text, out pos2)) { } else { errtemp = true; }
                if (Int32.TryParse(tmovet.Text, out tms)) { if (tms < 0) { errtemp = true; } } else { errtemp = true; }

                if (errtemp) { MessageBox.Show("参数输入错误！"); return; }
                for (i = 0; i < loopnum - 1; i++)
                {
                    zmcaux.ZAux_Direct_Single_MoveAbs(g_handle, taxix_num, pos1);
                    zmcaux.ZAux_Direct_MoveDelay(g_handle, taxix_num, tms);
                    zmcaux.ZAux_Direct_Single_MoveAbs(g_handle, taxix_num, pos2);
                    zmcaux.ZAux_Direct_MoveDelay(g_handle, taxix_num, tms);
                }

            }
            else
            {
                MessageBox.Show("控制器未连接");
            }
        }
        //停止往复运动
        private void button29_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                int taxix_num;
                bool errtemp = false;
                if (Int32.TryParse(label137.Text, out taxix_num)) { if (taxix_num < 0) { errtemp = true; } } else { errtemp = true; }
                if (errtemp) { MessageBox.Show("参数输入错误！"); return; }
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, taxix_num, 2);
            }
            else
            {
                MessageBox.Show("控制器未连接");
            }
        }
        //启动单轴同步
        private void button31_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                int taxix_num, connect_axis;
                float d_ratio;
                bool errtemp = false;
                if (Int32.TryParse(label143.Text, out taxix_num)) { if (taxix_num < 0) { errtemp = true; } } else { errtemp = true; }
                if (Int32.TryParse(connect.Text, out connect_axis)) { if (taxix_num < 0 || connect_axis == taxix_num) { errtemp = true; } } else { errtemp = true; }

                if (float.TryParse(c_ratio.Text, out d_ratio)) { } else { errtemp = true; }

                if (errtemp) { MessageBox.Show("参数输入错误！"); return; }
                zmcaux.ZAux_Direct_Connect(g_handle, d_ratio, connect_axis, taxix_num);
            }
            else
            {
                MessageBox.Show("控制器未连接");
            }
        }
        //取消单轴同步
        private void button32_Click(object sender, EventArgs e)
        {


            if (g_handle != (IntPtr)0)
            {
                int taxix_num;
                bool errtemp = false;
                if (Int32.TryParse(label137.Text, out taxix_num)) { if (taxix_num < 0) { errtemp = true; } } else { errtemp = true; }
                if (errtemp) { MessageBox.Show("参数输入错误！"); return; }
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, taxix_num, 2);
            }
            else
            {
                MessageBox.Show("控制器未连接");
            }
        }
        //位置清0
        private void button10_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                int i;
                for (i = 0; i < 4; i++)         //轴列表
                {
                    zmcaux.ZAux_Direct_SetDpos(g_handle, i, 0);
                    zmcaux.ZAux_Direct_SetMpos(g_handle, i, 0);
                }
            }
            else
            {
                MessageBox.Show("控制器未连接");
            }
        }
        //点位相对运动方式
        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            singlemove_mode = 0;
        }
        //点位绝对运动方式
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            singlemove_mode = 1;
        }
        //单轴点位运动
        private void button14_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                single_axis = Convert.ToInt32(single_axis0.Text);
                single_speed[single_axis] = Convert.ToSingle(single_speed0.Text);
                single_dist[single_axis] = Convert.ToSingle(singe_dist.Text);
                if (singlemove_mode == 0)   //相对运动
                {
                    zmcaux.ZAux_Direct_SetSpeed(g_handle, single_axis, single_speed[single_axis]);         //设置插补速度

                    zmcaux.ZAux_Direct_Single_Move(g_handle, single_axis, single_dist[single_axis]);
                }
                else               //绝对运动
                {
                    zmcaux.ZAux_Direct_SetSpeed(g_handle, single_axis, single_speed[single_axis]);         //设置插补速度

                    zmcaux.ZAux_Direct_Single_MoveAbs(g_handle, single_axis, single_dist[single_axis]);
                }

            }
            else
            {
                MessageBox.Show("控制器未连接");
            }
        }
        //正向运动按钮松开
        private void button11_MouseUp(object sender, MouseEventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                //single_axis = single_axis0.SelectedIndex;
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, single_axis, 2);
            }
        }
        //正向运动按钮下压
        private void button11_MouseDown(object sender, MouseEventArgs e)
        {

            single_speed[single_axis] = Convert.ToSingle(single_speed0.Text);
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_SetSpeed(g_handle, single_axis, single_speed[single_axis]);         //设置速度

                zmcaux.ZAux_Direct_Single_Vmove(g_handle, single_axis, 1);  //正向运动
            }
        }
        //负向运动按钮松开
        private void button12_MouseUp(object sender, MouseEventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                //single_axis = single_axis0.SelectedIndex;
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, single_axis, 2);
            }
        }
        //负向运动按钮下压
        private void button12_MouseDown(object sender, MouseEventArgs e)
        {
            //single_axis = single_axis0.SelectedIndex;
            single_speed[single_axis] = Convert.ToSingle(single_speed0.Text);
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_SetSpeed(g_handle, single_axis, single_speed[single_axis]);         //设置速度

                zmcaux.ZAux_Direct_Single_Vmove(g_handle, single_axis, -1);  //正向运动
            }
        }

        //IO绘制
        public void draw_io()
        {
            int i;

            uint[] out_state = new uint[32];
            uint[] in_state = new uint[32];

            Graphics g = this.groupBox7.CreateGraphics();
            Pen redPen = new Pen(Color.Red, 10);//红色
            Pen greenPen = new Pen(Color.Green, 10);//绿色


            while (Thread_flag1)
            {
                if (g_handle != (IntPtr)0)
                {
                    // g = this.groupBox7.CreateGraphics();
                    for (i = 0; i < max_io[1]; i++)            //输出口状态
                    {
                        if (i < 30)
                        {
                            zmcaux.ZAux_Direct_GetOp(g_handle, i, ref  out_state[i]);
                        }

                    }


                    for (i = 0; i < max_io[0]; i++)            //输入口状态
                    {
                        if (i < 30)
                        {
                            zmcaux.ZAux_Direct_GetIn(g_handle, i, ref  in_state[i]);
                            zmcaux.ZAux_Direct_GetInvertIn(g_handle, i, ref even_state[i]);
                        }

                    }


                    for (i = 0; i < max_io[1]; i++)
                    {
                        g = this.cout_op[i].CreateGraphics();
                        if (i < 30)
                        {
                            if (out_state[i] == 0)
                            {

                                g.DrawEllipse(redPen, new Rectangle(10, 10, 10, 10));   //红色，无信号
                            }
                            else
                            {
                                g.DrawEllipse(greenPen, new Rectangle(10, 10, 10, 10));   //绿色    
                            }
                        }


                    }

                    g = this.groupBox7.CreateGraphics();
                    for (i = 0; i < max_io[0]; i++)
                    {
                        if (i < 30)
                        {
                            if (in_state[i] == 0)
                            {
                                g.DrawEllipse(redPen, new Rectangle(90 + 55 * (i % 6), 40 + 80 * (i / 6), 10, 10));   //红色，无信号
                            }
                            else
                            {
                                g.DrawEllipse(greenPen, new Rectangle(90 + 55 * (i % 6), 40 + 80 * (i / 6), 10, 10));   //绿色                  
                            }

                            if (even_state[i] == 0)
                            {
                                g.DrawEllipse(redPen, new Rectangle(90 + 55 * (i % 6), 65 + 80 * (i / 6), 10, 10));   //红色，无信号
                            }
                            else
                            {
                                g.DrawEllipse(greenPen, new Rectangle(90 + 55 * (i % 6), 65 + 80 * (i / 6), 10, 10));   //绿色                  
                            }

                        }


                    }

                    g.DrawEllipse(greenPen, new Rectangle(120, 450, 10, 10));   //绿色     

                    g.DrawEllipse(redPen, new Rectangle(330, 450, 10, 10));   //绿色   

                }

            }
        }
        //窗口切换函数
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //使用线程，需声明using System.Threading;

            if (tabControl1.SelectedIndex == 0)         //控制器
            {
                timer1.Enabled = false;
                Thread_flag1 = false;
                if (thread_flag == 1)
                {

                    thread.Abort();
                }
                thread_flag = 0;
            }
            else if (tabControl1.SelectedIndex == 1)     //轴参数
            {
                AxisPara_Renew();//更新界面

                timer1.Enabled = false;
                Thread_flag1 = false;
                if (thread_flag == 1)
                {

                    thread.Abort();
                }

                thread_flag = 0;
            }
            else if (tabControl1.SelectedIndex == 2)     //运动
            {
                label133.Text = "";
                label134.Text = "";
                label135.Text = "";

                home_axiss = Convert.ToInt32(single_axis0.Text);
                label19.Text = single_axis0.Text;
                label18.Text = single_axis0.Text;
                label137.Text = single_axis0.Text;
                label143.Text = single_axis0.Text;

                zmcaux.ZAux_Direct_GetDatumIn(g_handle, home_axiss, ref datum_in[home_axiss]);
                zmcaux.ZAux_Direct_GetRevIn(g_handle, home_axiss, ref rev_in[home_axiss]);
                zmcaux.ZAux_Direct_GetFwdIn(g_handle, home_axiss, ref fwd_in[home_axiss]);

                label133.Text = datum_in[home_axiss].ToString();
                label134.Text = fwd_in[home_axiss].ToString();
                label135.Text = rev_in[home_axiss].ToString();


                timer1.Enabled = true;
                Thread_flag1 = false;
                if (thread_flag == 1)
                {

                    thread.Abort();
                }
                thread_flag = 0;
            }
            else if (tabControl1.SelectedIndex == 3)     //IO测试
            {
                ThreadStart threadStart = new ThreadStart(draw_io);
                thread = new Thread(threadStart);//带多个参数的线程

                timer1.Enabled = false;
                Thread_flag1 = true;
                thread.Start();//启动线程
                thread_flag = 1;
            }
            else if (tabControl1.SelectedIndex == 4)     //IO测试
            {
                timer1.Enabled = true;
                Thread_flag1 = false;
                if (thread_flag == 1)
                {

                    thread.Abort();
                }
                thread_flag = 0;
            }

        }
        //关闭窗体时自动调用一次
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Thread_flag1 = false;
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Close(g_handle);
                g_handle = (IntPtr)0;
            }
            if (thread_flag == 1)
            {

                thread.Abort();
            }

            timer1.Enabled = false;
            //Dispose(true);
            // Form1.Exit();
            // WindowsFormsApplication1.Exit();

            //Application.Exit();
            //Application.ExitThread();
            System.Environment.Exit(0);//把程序结束的很干净
        }
        //IO反转
        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            home_axiss = Convert.ToInt32(single_axis0.Text);
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, rev_in[home_axiss], 1);//高电平有效，无信号时为On,有信号时off
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, fwd_in[home_axiss], 1);//高电平有效，无信号时为On,有信号时off
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, datum_in[home_axiss], 1);//高电平有效，无信号时为On,有信号时off

        }
        //IO反转
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            home_axiss = Convert.ToInt32(single_axis0.Text);
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, rev_in[home_axiss], 0);//低电平有效，无信号时为OFF,有信号时on
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, fwd_in[home_axiss], 0);//高电平有效，无信号时为On,有信号时off
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, datum_in[home_axiss], 0);//高电平有效，无信号时为On,有信号时off
        }
        //更换回零轴号自动更新IO显示
        private void datum_axisnum_SelectedIndexChanged(object sender, EventArgs e)
        {
            label133.Text = "";
            label134.Text = "";
            label135.Text = "";

            home_axiss = single_axis0.SelectedIndex;

            label133.Text = datum_in[home_axiss].ToString();
            label134.Text = fwd_in[home_axiss].ToString();
            label135.Text = rev_in[home_axiss].ToString();
        }
        //设置输出打开
        private void button15_Click(object sender, EventArgs e)
        {
            int outnum;
            outnum = Convert.ToInt32(textBox2.Text);
            if (outnum >= max_io[1])
            {
                MessageBox.Show("outnum错误");
            }
            else if (outnum < 0)
            {
                MessageBox.Show("outnum错误");
            }
            else
            {
                zmcaux.ZAux_Direct_SetOp(g_handle, outnum, 1);
            }



        }
        //设置输出关闭
        private void button16_Click(object sender, EventArgs e)
        {
            int outnum;
            outnum = Convert.ToInt32(textBox2.Text);

            if (outnum >= max_io[1])
            {
                MessageBox.Show("outnum错误");
            }
            else if (outnum < 0)
            {
                MessageBox.Show("outnum错误");
            }
            else
            {
                zmcaux.ZAux_Direct_SetOp(g_handle, outnum, 0);
            }

        }


        /*
        Opnum 	 对应的输出口
        Opstate	第一个比较点的输出状态.
        maxerr	比较位置每个轴左右的脉冲偏差,  进入偏差范围后开始比较.
        num 		TABLE里面存储的比较点个数.
        tablepos 第一个比较点坐标所在TABLE编号*/

        private static int ZAux_HwPswitch2_2D(IntPtr handler, int[] aixslist, ushort opnum, bool opstate, ushort maxerr, ushort num, ushort tablenum)
        {

            string tempstring;
            StringBuilder resstring = new StringBuilder(2048);

            //resstring.Append(0);
            //StringBuilder[] resstring = new StringBuilder[2048];
            //byte[] resstring = new byte[2048];

            int ret1;
            if (aixslist.Length > 2)
            {
                return 1;//轴数量超标
            }


            ret1 = zmcaux.ZAux_Direct_Base(handler, 2, aixslist);//指定轴
            if (ret1 != 0)
            {
                return ret1;
            }

            tempstring = "HW_PSWITCH2(25," + opnum.ToString() + "," + opstate.ToString() + "," + maxerr.ToString() + "," + num.ToString() + "," + tablenum.ToString() + ")";
            //tempstring =$"HW_PSWITCH2(25,{opnum}，";
            ret1 = zmcaux.ZAux_Execute(handler, tempstring, resstring, 2048);
            return ret1;
        }

        private void button17_Click(object sender, EventArgs e)
        {



            // s = ts.Milliseconds;


            textBox3.Text = "";
            DateTime now = DateTime.Now;

            TimeSpan ts = DateTime.Now - now;
            double s;
            s = ts.Milliseconds;

            double dtemp;//浮点型读取周期上报内容
            Int32 itemp;//整型读取周期上报内容
            itemp = 0;
            dtemp = 0;
            Int32[] in_val = new Int32[32];
            Int32[] out_val = new Int32[32];
            Int32[] IDLE_val = new Int32[6];
            double[] dpos_val = new double[6];
            double[] mpos_val = new double[6];

            uint i;

            for (i = 0; i < 31; i++)
            {
                ret = zmcaux.ZAux_CycleUpReadBuffInt(g_handle, 0, "IN", i, ref itemp);//整形型读取IN
                in_val[i] = itemp;
                ret = zmcaux.ZAux_CycleUpReadBuffInt(g_handle, 0, "OUT", i, ref itemp);//整形型读取OUT
                out_val[i] = itemp;
            }
            for (i = 0; i < 6; i++)
            {
                ret = zmcaux.ZAux_CycleUpReadBuffInt(g_handle, 0, "IDLE", i, ref itemp);//整形型读取IN
                IDLE_val[i] = itemp;
                ret = zmcaux.ZAux_CycleUpReadBuff(g_handle, 0, "DPOS", i, ref dtemp);//整形型读取OUT
                dpos_val[i] = dtemp;
                ret = zmcaux.ZAux_CycleUpReadBuff(g_handle, 0, "MPOS", i, ref dtemp);//整形型读取OUT
                mpos_val[i] = dtemp;

                textBox3.Text = textBox3.Text + "MPOS" + i.ToString() + "\t" + mpos_val[i].ToString() + "\r\n";
            }

            ts = DateTime.Now - now;
            s = ts.Milliseconds;



        }
        //上报打开
        private void button18_Click(object sender, EventArgs e)
        {
            //上报内容：轴0-轴5，IDLE,MPOS,DPOS,IO(0-31)
            ret = zmcaux.ZAux_CycleUpEnable(g_handle, 0, 1, "IDLE(0,6),MPOS(0,6),DPOS(0,6),IN(0,32),OUT(0,32)");   //周期上报使能，1ms上报一次

            if (ret != 0)
            {
                MessageBox.Show("上报打开失败！");
            }

            ret = zmcaux.ZAux_CycleUpForceOnce(g_handle, 0);    //默认强制上报一次
            if (ret != 0)
            {
                MessageBox.Show("强制上报一次失败");
            }
            this.Text = this.Text + "周期上报已开启";
        }
        //上报关闭
        private void button19_Click(object sender, EventArgs e)
        {
            ret = zmcaux.ZAux_CycleUpDisable(g_handle, 0);   //关闭周期上报
            if (ret != 0)
            {
                MessageBox.Show("上报关闭失败！");
            }
            this.Text = this.Text + "周期上报已开启";
        }

        public void test_demo()
        {

        }
        //掉线检测
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                ret1 = zmcaux.ZMC_GetState(g_handle, Control_state);
                if (ret1 != 0)                             //找不到IP了
                {
                    zmcaux.ZAux_Close(g_handle);
                    g_handle = (IntPtr)0;
                    this.Text = "未连接";
                    //thread.Abort();
                    Thread_flag1 = false;
                    connect_mode = 0;
                    c_type.Text = "";
                    c_id.Text = "";
                    c_version.Text = "";
                    c_maxVirtuaxiss.Text = "";
                    c_maxMotor.Text = "";
                    c_maxIn.Text = "";
                    c_maxOut.Text = "";
                    c_maxAin.Text = "";
                    c_maxAout.Text = "";
                    this.Text = "未连接";
                    ret1 = 0;
                    timer2.Enabled = false;
                    timer1.Enabled = false;
                    MessageBox.Show("连接不上控制器！");
                }
            }
        }
        //测试
        private void button22_Click(object sender, EventArgs e)
        {
            StringBuilder buff = new StringBuilder();

            ret = zmcaux.ZAux_Execute(g_handle, "?PROC_STATUS(1)", buff, 100);

            MessageBox.Show(buff.ToString());

            ret = zmcaux.ZAux_Execute(g_handle, "STOPTASK  1", buff, 100);

            ret = zmcaux.ZAux_Execute(g_handle, "?PROC_STATUS(1)", buff, 100);
            MessageBox.Show(buff.ToString());
        }
        //测试
        private void button24_Click(object sender, EventArgs e)
        {
            zmcaux.ZAux_Trigger(g_handle);
            zmcaux.ZAux_Direct_SetOp(g_handle, 0, 0);
            ret = zmcaux.ZAux_Direct_HwTimer(g_handle, 2, 1000000, 500000, 2, 0, 0);
            if (ret != 0)
            {
                MessageBox.Show(ret.ToString());
            }
            zmcaux.ZAux_Direct_SetOp(g_handle, 0, 1);
        }

        //485例子
        private void button34_Click(object sender, EventArgs e)
        {
            StringBuilder buff = new StringBuilder();


            ret = zmcaux.ZAux_Execute(g_handle, "RUNTASK 1,Modbus_485_Get", buff, 100);
            if (ret != 0)
            {
                MessageBox.Show(ret.ToString());
            }



        }
        //清空内容
        private void clear_buff_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }
        //发送在线命令
        private void button35_Click(object sender, EventArgs e)
        {
            if (text_send.Text != "")
            {

            }
        }
        //导出VR文件
        private void button35_Click_1(object sender, EventArgs e)
        {
            string NewFilePath;
            if ((VRtext.Text == ""))
            {
                NewFilePath = ".\\path\\Zmotion(VR).bas";
            }
            else
            {
                NewFilePath = ".\\path\\" + VRtext.Text + "(VR).bas";
            }


            try
            {
                FileStream NewBas = File.Create(NewFilePath);
                NewBas.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出失败！", ex.ToString());
                return;
            }
            Thread.Sleep(100);

            string str;

            //自动释放所新建的文件对象
            using (StreamWriter sw = new StreamWriter(NewFilePath, false))
            {
                float[] VrBuff = new float[8000];

                ret = zmcaux.ZAux_Direct_GetVrf(g_handle, 0, Max_ParamArry[max_vr], VrBuff);
                if (ret != 0)
                {
                    MessageBox.Show("导出失败！");
                    return;
                }
                int i = 0;
                for (i = 0; i < Max_ParamArry[max_vr]; i++)
                {

                    str = "";

                    str = "VR(" + i + ")=";
                    str = str + VrBuff[i].ToString();
                    sw.WriteLine(str);


                }
                //MessageBox.Show(i.ToString());
                sw.Flush();
                sw.Close();
            }




            MessageBox.Show("导出成功！");
        }

        private void button36_Click(object sender, EventArgs e)
        {
            int ret = 0;
            //  StringBuilder[] str = new StringBuilder[128];
            StringBuilder str = new StringBuilder(128);


            ret += zmcaux.ZAux_Modbus_Set4x_String(g_handle, 0, 128, "你好！helle,word!");

            ret += zmcaux.ZAux_Modbus_Get4x_String(g_handle, 0, 128, str);
            MessageBox.Show(str.ToString());

            /* fval[0] = 3;
             ret += zmcaux.ZAux_Direct_SetTable(g_handle, 1, 1, fval);

             ret += zmcaux.ZAux_Direct_GetTable(g_handle, 1, 1, fval);
             MessageBox.Show(fval[0].ToString()); */

            if (ret != 0)
            {
                MessageBox.Show("失败！");
                return;
            }


        }
        //local连接
        private void button22_Click_1(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {

                    ret=zmcaux.ZAux_FastOpen(5, "", 1000, out g_handle);

            }
            if (g_handle != (IntPtr)0)
            {
                if (0 != connect_mode)
                {
                    MessageBox.Show("当前已建立连接");
                }
                else
                {
                    connect_mode = 4;
                    this.Text = "已连接,连接方式:local";
                    timer1.Enabled = true;//使能定时器1
                    timer2.Enabled = true;//使能定时器2
                    StringBuilder SoftType = new StringBuilder(20);
                    StringBuilder SoftVersion = new StringBuilder(20);
                    StringBuilder ControllerId = new StringBuilder(20);

                    Virtuaxiss = 0;
                    byte[] Motor = new byte[1];
                    byte[] max_io = new byte[4];

                    zmcaux.ZAux_GetControllerInfo(g_handle, SoftType, SoftVersion, ControllerId);//读取控制器厂商信息
                    zmcaux.ZAux_GetSysSpecification(g_handle, ref Virtuaxiss, Motor, max_io);
                    c_type.Text = "";
                    c_id.Text = "";
                    c_version.Text = "";
                    c_maxVirtuaxiss.Text = "";
                    c_maxMotor.Text = "";
                    c_maxIn.Text = "";
                    c_maxOut.Text = "";
                    c_maxAin.Text = "";
                    c_maxAout.Text = "";
                    c_type.Text += SoftType;
                    c_id.Text += ControllerId;
                    c_version.Text += SoftVersion;
                    c_maxVirtuaxiss.Text += Virtuaxiss;
                    c_maxMotor.Text += Motor[0];
                    c_maxIn.Text += max_io[0];
                    c_maxOut.Text += max_io[1];
                    c_maxAin.Text += max_io[2];
                    c_maxAout.Text += max_io[3];

                    StringBuilder resstring = new StringBuilder(2048);
                    int i, j;
                    for (i = 0; i < Max_ParamArry.Length - 1; i++)
                    {
                        Max_ParamArry[i] = 0;
                    }
                    ret = zmcaux.ZAux_Execute(g_handle, "?*max", resstring, 2048);//读取控制卡参数最大值

                    j = 0;


                    if (ret == 0)
                    {
                        string tempstring = resstring.ToString();    //StringBuilder转换成string类型
                        string[] arrstring = tempstring.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);//字符串切割，获取max数量
                        // MessageBox.Show(arrstring.Length.ToString());
                        for (i = 0; i < arrstring.Length - 1; i++)
                        {

                            if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_in"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_in]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_out"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_out]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_ain"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_ain]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_aout"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_aout]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_slotecat"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split(',')[1].ToString(), out Max_ParamArry[max_slotecat]);

                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_nand"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split('K')[0].ToString(), out Max_ParamArry[max_nand]);
                            }
                            else if (string.Equals(arrstring[i].Split(':')[0].ToString(), "max_nandremain"))
                            {
                                Int32.TryParse(arrstring[i].Split(':')[1].Split('K')[0].ToString(), out Max_ParamArry[max_nandremain]);
                            }
                            else
                            {
                                j = 0;
                                while (true)
                                {
                                    if (j >= Max_ParamName.Length) //达到长度跳出循环
                                    {
                                        break;
                                    }
                                    if (string.Equals(arrstring[i].Split(':')[0].ToString(), Max_ParamName[j]))//字符串比较
                                    {
                                        Int32.TryParse(arrstring[i].Split(':')[1].ToString(), out Max_ParamArry[j]);
                                        j = j + 1;
                                        break;
                                    }
                                    else
                                    {
                                        j = j + 1;
                                        if (j >= Max_ParamName.Length)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }

                        }
                    }

                    for (i = 0; i < 30; i++)
                    {
                        name_in[i].Text = "";
                        name_op[i].Text = "";
                    }
                    for (i = 0; i < max_io[1]; i++)
                    {
                        if (i < 30)
                        {
                            name_op[i].Text = "out" + i.ToString();
                        }

                    }
                    for (i = 0; i < max_io[0]; i++)
                    {
                        if (i < 30)
                        {
                            name_in[i].Text = "in" + i.ToString();
                        }

                    }
                }
            }
        }

        private void button24_Click_1(object sender, EventArgs e)
        {
            ret=zmcaux.ZAux_BusCmd_NodePdoWrite(g_handle, 0, 28704, 1,7,0);//读取控制器厂商信息

            String temp;
            

        }
    }
}
