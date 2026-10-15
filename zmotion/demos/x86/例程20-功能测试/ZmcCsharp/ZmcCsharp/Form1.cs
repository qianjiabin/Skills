using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

using System.Runtime.InteropServices;//引用命名空间
using System.IO;


using cszmcaux;

namespace ZmcCsharp
{
    public partial class Form1 : Form
    {

        #region "声明变量" 
/// 
/// 写入INI文件
/// 
///节点名称[如[TypeName]] 
///键 
///值 
///文件路径 
/// 
[DllImport("kernel32")]
private static extern long WritePrivateProfileString(string section,string key,string val,string filepath);
/// 
/// 读取INI文件
/// 
///节点名称 
///键 
///值 
///stringbulider对象 
///字节大小 
///文件路径 
/// 
[DllImport("kernel32")]
private static extern int GetPrivateProfileString(string section,string key,string def,StringBuilder retval,int size,string filePath);
private string strFilePath;//获取INI文件路径 
private string strSec =""; //INI段名 
#endregion


        public IntPtr g_handle;
        public string g_command;       //发送的命令
        public const int g_maxtime = 500;       //最大等待时间
        public int aaa;             //    '调试时，查看返回值
        public float fValue;          //浮点参数
        public string Adrr;             //连接的IP地址

        public int connect = 0;             //    '链接方式，1-网口

        public uint com;            //串口号
        public uint baudrate;       //波特率

        public StringBuilder psResponse = new StringBuilder(10240);               //接收的返回的字符
        public UInt32 uiResponseLength ;               //返回字符长度

        public string changps;                  //接收的字符数组转换为字符串

        public byte[] zpjbuff = new byte[20480];
        public byte[] pBasDir = new byte[20480];


        public int[] atype= new int[4];          //轴类型
        public int[] pul_mode = new int[4];          //脉冲输出模式
 
        public float[] units = new float[4];                //脉冲当量
        public float[] speed = new float[4];               //速度
        public float[] accel = new float[4];              //加速度
        public float[] decel = new float[4];               //减速度
        public float[] dpos = new float[4];                //位置
        public float[] fs_limit = new float[4];               //正限位
        public float[] rs_limit = new float[4];                //负限位

        public float[] mpos = new float[4];                //位置
        public float[] e_units = new float[4];                //脉冲当量

        public float v_speed  = 200;               //插补速度

        public int  move_mode  = 0;              //运动方式相对还是绝对
        public int[] axis_list = new int[4];            //运动轴列表

        public int  single_axis =0;                 //单轴轴号

        public float[] single_speed = new float[4];               //单轴运动位置
        public float[] single_dist = new float[4];               //单轴运动位置

        public float[] home_speed = new float[4];
        public float[] slow_speed = new float[4];
        public int[] home_mode = new int[4];
        
        public float[] v_dist = new float[4];                 //直线插补位置

        public float[] v_start = new float[4];                 //插补起点点位置
        public float[] v_mid = new float[4];                 //插补中点位置
        public float[] v_end = new float[4];                 //插补中点位置


        public byte[] in_state = new byte[32];                  //输入口状态
        public byte[] out_state = new byte[16];                //输出口状态

        public int[] even_state = new int[32];                  //反转状态

        public int[] in_home = new int[4];            //原点
        public int[] in_fwd = new int[4];            //正限位
        public int[] in_rev = new int[4];             //负限位
        public int[] in_alm = new int[4];             //告警

        public int[] idle_state = new int[4];             //轴运行状态

        public int[] axisstatus = new int[4];             //轴状态

        public float[] endir = new float[4];                  //编码器状态

        //控件数组定义
        ComboBox[] c_atype = new ComboBox[4];           //轴类型
        TextBox[] c_mode = new TextBox[4];              //脉冲模式
        TextBox[] c_units = new TextBox[4];              //脉冲当量
        TextBox[] c_speed = new TextBox[4];              //速度
        TextBox[] c_acc = new TextBox[4];              //加速度
        TextBox[] c_dec = new TextBox[4];              //减速度
        TextBox[] c_limit0 = new TextBox[4];              //正限位
        TextBox[] c_limit1 = new TextBox[4];              //负限位
        TextBox[] c_dpos = new TextBox[4];              //坐标

        TextBox[] c_eunits = new TextBox[4];              //坐标

        TextBox[] cin_home = new TextBox[4];              //原点坐标
        TextBox[] cin_fwd = new TextBox[4];              //正硬件限位
        TextBox[] cin_rev = new TextBox[4];              //负限位
        TextBox[] cin_alm = new TextBox[4];              //轴告警

        Label[] c_pos = new Label[4];               //坐标显示
        Label[] c_idle = new Label[4];               //运动状态显示
        Label[] c_axisstatus = new Label[4];               //轴状态显示

        Label[] cout_op = new Label[12];               //输出口状态显示面板，画图用

        Label[] c_mpos = new Label[4];              //坐标
        Label[] c_dpos1 = new Label[4];              //坐标

        ComboBox[] en_dir = new ComboBox[4];           //轴类型

        public void init_contrl()                      //控件初始化
        { 

            c_atype[0] = atype0;
            c_atype[1] = atype1;
            c_atype[2] = atype2;
            c_atype[3] = atype3;

            c_mode[0] = pul_mode0;
            c_mode[1] = pul_mode1;
            c_mode[2] = pul_mode2;
            c_mode[3] = pul_mode3;

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

            c_limit0[0] = fslimit0;
            c_limit0[1] = fslimit1;
            c_limit0[2] = fslimit2;
            c_limit0[3] = fslimit3;

            c_limit1[0] = rslimit0;
            c_limit1[1] = rslimit1;
            c_limit1[2] = rslimit2;
            c_limit1[3] = rslimit3;

            c_dpos[0] = dpos0;
            c_dpos[1] = dpos1;
            c_dpos[2] = dpos2;
            c_dpos[3] = dpos3;

            cin_home[0] = home0;
            cin_home[1] = home1;
            cin_home[2] = home2;
            cin_home[3] = home3;

            cin_fwd[0] = fw0;
            cin_fwd[1] = fw1;
            cin_fwd[2] = fw2;
            cin_fwd[3] = fw3;

            cin_rev[0] = rs0;
            cin_rev[1] = rs1;
            cin_rev[2] = rs2;
            cin_rev[3] = rs3;

            cin_alm[0] = alm0;
            cin_alm[1] = alm1;
            cin_alm[2] = alm2;
            cin_alm[3] = alm3;

            c_pos[0] = pos0;
            c_pos[1] = pos1;
            c_pos[2] = pos2;
            c_pos[3] = pos3;

            c_idle[0] = idle0;
            c_idle[1] = idle1;
            c_idle[2] = idle2;
            c_idle[3] = idle3;

            c_axisstatus[0] = axisstatus0;
            c_axisstatus[1] = axisstatus1;
            c_axisstatus[2] = axisstatus2;
            c_axisstatus[3] = axisstatus3;

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

            c_mpos[0] = mpos6;
            c_mpos[1] = mpos7;
            c_mpos[2] = mpos8;
            c_mpos[3] = mpos9;

            c_dpos1[0] = dpos6;
            c_dpos1[1] = dpos7;
            c_dpos1[2] = dpos8;
            c_dpos1[3] = dpos9;

            c_eunits[0] = e0_units;
            c_eunits[1] = e1_units;
            c_eunits[2] = e2_units;
            c_eunits[3] = e3_units;

            en_dir[0] = endir0;
            en_dir[1] = endir1;
            en_dir[2] = endir2;
            en_dir[3] = endir3;

        }
        

        public void set_para()                      //设置控制器参数
        {
            int i = 0;
            if (g_handle != (IntPtr)0)
            {
                for (i = 0; i < 4; i++)
                {
                    zmcaux.ZAux_Direct_SetAtype(g_handle, i,atype[i]);
                    zmcaux.ZAux_Direct_SetInvertStep(g_handle, i, pul_mode[i]);
                    zmcaux.ZAux_Direct_SetUnits(g_handle, i, units[i]);
                    zmcaux.ZAux_Direct_SetSpeed(g_handle, i, speed[i]);
                    zmcaux.ZAux_Direct_SetAccel(g_handle, i, accel[i]);
                    zmcaux.ZAux_Direct_SetDecel(g_handle, i, decel[i]);
                    zmcaux.ZAux_Direct_SetFsLimit(g_handle, i, fs_limit [i]);
                    zmcaux.ZAux_Direct_SetRsLimit(g_handle, i, rs_limit[i]);
                    zmcaux.ZAux_Direct_SetDpos(g_handle, i, dpos[i]);
                }

                for (i = 6; i < 10; i++)
                {
                    zmcaux.ZAux_Direct_SetAtype(g_handle, i, 3);
                    zmcaux.ZAux_Direct_SetUnits(g_handle, i, e_units[i-6]);
                }
            }
        }

        public void get_para()                      //读取控制器参数
        {
            int i = 0;

            if (g_handle != (IntPtr)0)
            {
                for (i = 0; i < 4; i++)
                {
                    zmcaux.ZAux_Direct_GetAtype(g_handle, i, ref atype[i]);
                    zmcaux.ZAux_Direct_GetInvertStep(g_handle, i, ref pul_mode[i]);
                    zmcaux.ZAux_Direct_GetUnits(g_handle, i, ref units[i]);
                    zmcaux.ZAux_Direct_GetSpeed(g_handle, i, ref speed[i]);
                    zmcaux.ZAux_Direct_GetAccel(g_handle, i, ref accel[i]);
                    zmcaux.ZAux_Direct_GetDecel(g_handle, i, ref decel[i]);
                    zmcaux.ZAux_Direct_GetFsLimit(g_handle, i, ref fs_limit[i]);
                    zmcaux.ZAux_Direct_GetRsLimit(g_handle, i, ref rs_limit[i]);
                    zmcaux.ZAux_Direct_GetDpos(g_handle, i, ref dpos[i]);
                    zmcaux.ZAux_Direct_GetIfIdle(g_handle, i, ref idle_state[i]);

                    zmcaux.ZAux_Direct_GetDatumIn(g_handle, i, ref in_home[i]);           //配置原点
                    zmcaux.ZAux_Direct_GetFwdIn(g_handle, i, ref in_fwd[i]);           //配置正限位
                    zmcaux.ZAux_Direct_GetRevIn(g_handle, i, ref in_rev[i]);           //配置负限位
                    zmcaux.ZAux_Direct_GetAlmIn(g_handle, i, ref in_alm[i]);           //配置原点

                }


                for (i = 6; i < 10; i++)
                {
                    zmcaux.ZAux_Direct_GetUnits(g_handle, i,ref e_units[i - 6]);
                }

                for (i = 0; i < 28; i++)            //输入口状态
                {
                    zmcaux.ZAux_Direct_GetInvertIn(g_handle, i, ref even_state[i]);
                }
            }

        }

        public void get_moveval()
        {            
            int i = 0;
            float[] temp_para = new float[16];
            if (g_handle != (IntPtr)0)
            {

                zmcaux.ZAux_Direct_GetTable(g_handle, 0, 16, temp_para);
                zmcaux.ZAux_Direct_GetTable(g_handle, 50, 4, endir);
                for (i = 0; i < 4; i++)
                {
                    single_speed[i] = temp_para[i * 4];
                    single_dist[i] = temp_para[i * 4 + 1];
                    home_mode[i] = (int)temp_para[i * 4 + 2];
                    home_speed[i] = temp_para[i * 4 + 3];
                    zmcaux.ZAux_Direct_GetCreep(g_handle, i, ref slow_speed[i]);           //配置CREEP
                }
            }
        }

        public void set_val()      //显示到界面
        {
             int i = 0;
             for (i = 0; i < 4; i++)
             {
                 c_atype[i].SelectedIndex = atype[i];
                 c_mode[i].Text = pul_mode[i].ToString();
                 c_units[i].Text = units[i].ToString("f");
                 c_speed[i].Text = speed[i].ToString("f");
                 c_acc[i].Text = accel[i].ToString("f");
                 c_dec[i].Text = decel[i].ToString("f");
                 c_limit0[i].Text = fs_limit[i].ToString("f");
                 c_limit1[i].Text = rs_limit[i].ToString("f");
                 c_dpos[i].Text = dpos[i].ToString("f");

                 cin_home[i].Text = in_home[i].ToString();
                 cin_fwd[i].Text = in_fwd[i].ToString();
                 cin_rev[i].Text = in_rev[i].ToString();
                 cin_alm[i].Text = in_alm[i].ToString();

                 c_eunits[i].Text =  e_units[i].ToString("f");
                 en_dir[i].SelectedIndex = (int)endir[i];

             }

        }

        public void get_val()      //界面到变量
        {
            int i=0;
            for (i = 0; i < 4; i++)
            {
                atype[i] = c_atype[i].SelectedIndex;
                pul_mode[i] = Convert.ToInt32(c_mode[i].Text);
                units[i] = Convert.ToSingle(c_units[i].Text);
                speed[i] = Convert.ToSingle(c_speed[i].Text);
                accel[i] = Convert.ToSingle(c_acc[i].Text);
                decel[i] = Convert.ToSingle(c_dec[i].Text);
                fs_limit[i] = Convert.ToSingle(c_limit0[i].Text);
                rs_limit[i] = Convert.ToSingle(c_limit1[i].Text);
                dpos[i] = Convert.ToSingle(c_dpos[i].Text);

                e_units[i] = Convert.ToSingle(c_eunits[i].Text);
                endir[i] = en_dir[i].SelectedIndex;
            }
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            init_contrl();
        }


        private void timer1_Tick(object sender, EventArgs e)        //定时器刷新参数
        {
            if (g_handle != (IntPtr)0)
            {
                int i = 0;
                if (tabControl1.SelectedIndex == 2)         //运动测试
                {
                    for (i = 0; i < 4; i++)
                    {
                        zmcaux.ZAux_Direct_GetDpos(g_handle, i, ref dpos[i]);
                        zmcaux.ZAux_Direct_GetIfIdle(g_handle, i, ref idle_state[i]);
                        zmcaux.ZAux_Direct_GetAxisStatus(g_handle, i, ref axisstatus[i]);
                    }


                    for (i = 0; i < 4; i++)
                    {
                        c_pos[i].Text = dpos[i].ToString();
                        c_idle[i].Text = idle_state[i].ToString();
                        switch (axisstatus[i])
                        {
                            case 16:
                                c_axisstatus[i].Text = "正限位";
                                break;
                            case 32:
                                c_axisstatus[i].Text = "负限位";
                                break;
                            case 48:
                                c_axisstatus[i].Text = "正限位";
                                break;
                            case 4194304:
                                c_axisstatus[i].Text = "伺服报警";
                                break;
                            default:
                                c_axisstatus[i].Text = "正常";
                                break;
                        }
                    }

                }
                if (tabControl1.SelectedIndex == 4)   //编码器测试
                {
                    for (i = 0; i < 4; i++)
                    {
                        zmcaux.ZAux_Direct_GetDpos(g_handle, i, ref dpos[i]);
                        zmcaux.ZAux_Direct_GetMpos(g_handle, i+6, ref mpos[i]);

                        c_dpos1[i].Text = dpos[i].ToString();
                        c_mpos[i].Text = mpos[i].ToString();
                    }
                }


            }
        }

        private void comboBox2_DropDown(object sender, EventArgs e)     //下拉搜索IP
        {
            int i, num;
            string[] sArray;
            StringBuilder buffer = new StringBuilder(10240);
            string buff = "";
            aaa = zmcaux.ZAux_SearchEthlist(buffer, 10230, 200);
            buff += buffer;
            sArray = buff.Split(' ');
            num = buff.Split(' ').Length;

            comboBox2.Items.Clear();

            comboBox2.Items.Insert(0,"127.0.0.1");
            for (i = 0; i < num - 1; i++)
            {
                comboBox2.Items.Insert(i+1, sArray[i].ToString());
            }
        }

        private void Button1_Click_1(object sender, EventArgs e)        //以太网链接
        {
            byte[] tempbuff = new byte[1024] ;               //接收的返回的字符
            if (g_handle == (IntPtr)0)
            {
                Adrr = comboBox2.Text;
                if (Adrr == "127.0.0.1")
                {
                    zmcaux.ZAux_OpenEth(Adrr, out g_handle);
                }
                 else
                {
                   // aaa = zmcaux.ZAux_SearchEth(Adrr, 100);     //搜索控制器
                    aaa = zmcaux.ZAux_OpenEth(Adrr, out g_handle);
                    if (aaa == 0)
                    {
                        zmcaux.ZAux_OpenEth(Adrr, out g_handle);
                    }
                    else
                    {
                        MessageBox.Show("找不到控制器");
                     }
                }
            }
            if (g_handle != (IntPtr)0)
            {
                connect = 1;
                timer2.Enabled = true;
                this.Text = "已连接";


                 
                StringBuilder SoftType = new StringBuilder(20);
                StringBuilder SoftVersion = new StringBuilder(20);
                StringBuilder ControllerId = new StringBuilder(20);

                zmcaux.ZAux_GetControllerInfo(g_handle, SoftType, SoftVersion, ControllerId);
    
                c_type.Text += SoftType;
                c_id.Text += ControllerId;
                c_version.Text += SoftVersion;

                get_para();
                get_moveval();
                set_val();
            }
            else
            {
                this.Text = "未连接";
                MessageBox.Show("连接失败");
            }


        }

        private void Button2_Click_1(object sender, EventArgs e)        //断开链接
        {
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Close(g_handle);
                g_handle = (IntPtr)0;
            }

            this.Text = "未连接";
            timer2.Enabled = false;

        }

        private void button19_Click(object sender, EventArgs e)         //修改IP
        {

            if (g_handle != (IntPtr)0)
            {
                
               g_command = "IP_ADDRESS  = " + ip_add.Text;


                  zmcaux.ZAux_Execute(g_handle, g_command, psResponse, uiResponseLength); // '执行命令并接受应答
                  MessageBox.Show ("IP修改断，请电重新链接");
                  g_handle = (IntPtr)0;
                  this.Text = "未连接";

            }

        }

        private void Button9_Click_1(object sender, EventArgs e)        //串口链接
        {
            byte[] tempbuff = new byte[1024];               //接收的返回的字符
            if (g_handle == (IntPtr)0)
            {
                com = Convert.ToUInt32(TextBox4.Text);

                baudrate = Convert.ToUInt32(ComboBox1.Text);

                zmcaux.ZAux_SetComDefaultBaud(baudrate, 8, 0, 1);
                zmcaux.ZAux_OpenCom(com, out g_handle);

                if (g_handle != (IntPtr)0)
                {
                    this.Text = "已连接";

                    StringBuilder SoftType = new StringBuilder(20);
                    StringBuilder SoftVersion = new StringBuilder(20);
                    StringBuilder ControllerId = new StringBuilder(20);

                    zmcaux.ZAux_GetControllerInfo(g_handle, SoftType, SoftVersion, ControllerId);

                    c_type.Text += SoftType;
                    c_id.Text += ControllerId;
                    c_version.Text += SoftVersion;

                    get_para();
                    get_moveval();
                    set_val();
                }
                else
                {
                    this.Text = "未连接";
                    MessageBox.Show("连接失败");
                }
            }
        }

        private void Button7_Click_1(object sender, EventArgs e)        //发送命令
        {
            uiResponseLength = 1024;
            if (g_handle != (IntPtr)0)
            {
                g_command = TextBox1.Text;

                zmcaux.ZAux_Execute(g_handle, g_command, psResponse, uiResponseLength); // '执行命令并接受应答


                changps += psResponse;



                TextBox2.Text = TextBox2.Text + ">>" + g_command + "\r\n";

                if (psResponse[0] != '\0')                 //   '显示接收信息
                {
                    TextBox2.Text = TextBox2.Text + changps + "\r\n";
                }

            }
            else
            {
                TextBox2.Text = TextBox2.Text + "Connect First!" + "\r\n";
            }
            
        }

        private void Button8_Click_1(object sender, EventArgs e)    //清空
        {
            TextBox2.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)      //修改参数
        {

            get_val();            //读取界面上到变量
            set_para();             //发送到控制器

            strFilePath = Application.StartupPath + "\\" + Adrr + ".bas";
            if (File.Exists(strFilePath))
            {
                File.Delete(strFilePath);
                strFilePath = Application.StartupPath + "\\" + Adrr + ".bas";
            }

            save_ini(strFilePath);                      //保存参数
            download(strFilePath);
        }

        private void button4_Click(object sender, EventArgs e)      //初始化参数
        {
            int i;
            i = 0;
            for (i=0; i < 4; i++)
            { 
                atype[i] = 1;
                pul_mode[i] = 0;
                units[i] = 1;
                speed[i] = 100;
                accel[i] = 1000;
                decel[i] = 1000;
                fs_limit[i] = 20000000;
                rs_limit[i] = -20000000;
                dpos[i] = 0;
            }

             set_val();           //变量显示到界面
             set_para();            //发送到控制
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)        //相对运动方式
        {
            move_mode = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)         //绝对运动方式
        {
            move_mode = 1;
        }

        private void button5_Click(object sender, EventArgs e)                      //停止所有轴
        {
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_Rapidstop(g_handle,2); 
            }
        }

        private void button13_Click(object sender, EventArgs e)         //单轴停止
        {
            single_axis = Convert.ToInt32(axisnum.Text);
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, single_axis, 2);
            }
        }

        private void button11_Click(object sender, EventArgs e)             //单轴运动
        {
            single_axis = Convert.ToInt32(axisnum.Text);
            single_speed[single_axis] = Convert.ToSingle(single_sp.Text);
            single_dist[single_axis] = Convert.ToSingle(singe_dist.Text);

            if (g_handle != (IntPtr)0)
            {
                if (move_mode == 0) 
                {
                    //相对
                    zmcaux.ZAux_Direct_SetSpeed(g_handle, single_axis,single_speed[single_axis] );         //设置速度

                    zmcaux.ZAux_Direct_Single_Move(g_handle, single_axis, single_dist[single_axis]); 
                }
                else
                {
                    //绝对
                    zmcaux.ZAux_Direct_SetSpeed(g_handle, single_axis, single_speed[single_axis]);         //设置速度

                    zmcaux.ZAux_Direct_Single_MoveAbs(g_handle, single_axis, single_dist[single_axis]);   
                }

            }
        }

        private void button12_Click(object sender, EventArgs e)             //单轴回零
        {

            single_axis = Convert.ToInt32(axisnum.Text);
            home_mode[single_axis] = Convert.ToInt32(datum.Text);
            home_speed[single_axis] = Convert.ToSingle(datumsp.Text);
            slow_speed[single_axis] = Convert.ToSingle(datum_slow.Text);

            zmcaux.ZAux_Direct_SetSpeed(g_handle, single_axis, home_speed[single_axis]);         //设置速度
            zmcaux.ZAux_Direct_SetCreep(g_handle, single_axis, slow_speed[single_axis]);         //2次回零反找速度

            zmcaux.ZAux_Direct_Single_Datum(g_handle, single_axis, home_mode[single_axis]);       //回零
        }

        private void button6_MouseDown(object sender, MouseEventArgs e)            //负向点动动按下
        {
            single_axis = Convert.ToInt32(axisnum.Text);
            single_speed[single_axis] = Convert.ToSingle(single_sp.Text);
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_SetSpeed(g_handle, single_axis, single_speed[single_axis]);         //设置速度
                zmcaux.ZAux_Direct_Single_Vmove(g_handle, single_axis, -1);

            }
        }

        private void button6_MouseUp(object sender, MouseEventArgs e)            //负向点动动松开
        {
            single_axis = Convert.ToInt32(axisnum.Text);
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, single_axis, 2);
            }
        }

        private void button10_MouseDown(object sender, MouseEventArgs e)            //正向按下
        {
            single_axis = Convert.ToInt32(axisnum.Text);
            single_speed[single_axis] = Convert.ToSingle(single_sp.Text);
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_SetSpeed(g_handle, single_axis, single_speed[single_axis]);         //设置速度

                zmcaux.ZAux_Direct_Single_Vmove(g_handle, single_axis, 1);
            }
        }

        private void button10_MouseUp(object sender, MouseEventArgs e)           //正向松开
        {
            single_axis = Convert.ToInt32(axisnum.Text);
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, single_axis, 2);
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)    //窗口切换
        {
            ThreadStart threadStart = new ThreadStart(draw_io);
            Thread thread = new Thread(threadStart);
            if (tabControl1.SelectedIndex == 0)         //控制器
            {
                timer1.Enabled = false ;
                thread.Abort();

            }
            else if (tabControl1.SelectedIndex == 1)     //轴参数
            {
                get_para();
                set_val();
                timer1.Enabled = false;
                thread.Abort();

            }
            else if (tabControl1.SelectedIndex == 2)     //运动
            {
                single_axis = Convert.ToInt32(axisnum.Text);
                single_sp.Text = single_speed[single_axis].ToString("f");
                singe_dist.Text = single_dist[single_axis].ToString("f");
                datum.Text = home_mode[single_axis].ToString();
                datumsp.Text = home_speed[single_axis].ToString("f");
                datum_slow.Text = slow_speed[single_axis].ToString("f");

                timer1.Enabled = true;
                thread.Abort();
            }
            else if (tabControl1.SelectedIndex == 3)     //IO测试
            {
                timer1.Enabled = false;
                thread.Start();

            }
            else if (tabControl1.SelectedIndex == 4)     //编码器测试
            {
                timer1.Enabled = true;
                thread.Abort();

            }
        }

        private void button11_Click_1(object sender, EventArgs e)           //直线插补
        {
            int i;
            for (i = 0; i < 4; i++)         //轴列表
            {
                axis_list[i] = i;
            }
            v_dist[0] = Convert.ToSingle(line0.Text);
            v_dist[1] = Convert.ToSingle(line1.Text);
            v_dist[2] = Convert.ToSingle(line2.Text);
            v_dist[3] = Convert.ToSingle(line3.Text);

                if (g_handle != (IntPtr)0)
                {
                    if (move_mode == 0)
                    {
                        //相对
                        zmcaux.ZAux_Direct_Base(g_handle,4,axis_list);

                        zmcaux.ZAux_Direct_SetSpeed(g_handle, axis_list[0], Convert.ToSingle(vspeed.Text));         //设置速度

                        zmcaux.ZAux_Direct_Move(g_handle, 4, axis_list, v_dist);
                    }
                    else
                    {
                        //绝对
                        zmcaux.ZAux_Direct_Base(g_handle, 4, axis_list);

                        zmcaux.ZAux_Direct_SetSpeed(g_handle, axis_list[0], Convert.ToSingle(vspeed.Text));         //设置速度

                        zmcaux.ZAux_Direct_MoveAbs(g_handle, 4, axis_list, v_dist);
                    }

                }
        }

        private void button15_Click(object sender, EventArgs e)         //空间圆弧
        {
            int i;
            for (i = 0; i < 4; i++)         //轴列表
            {
                axis_list[i] = i;
            }

            v_start[0] = Convert.ToSingle(start0.Text);
            v_start[1] = Convert.ToSingle(start1.Text);
            v_start[2] = Convert.ToSingle(start2.Text);
            v_start[3] = Convert.ToSingle(start3.Text);

            v_mid[0] = Convert.ToSingle(mid0.Text);
            v_mid[1] = Convert.ToSingle(mid1.Text);
            v_mid[2] = Convert.ToSingle(mid2.Text);         
            v_mid[3] = Convert.ToSingle(mid3.Text);


            v_end[0] = Convert.ToSingle(end0.Text);
            v_end[1] = Convert.ToSingle(end1.Text);
            v_end[2] = Convert.ToSingle(end2.Text);
            v_end[3] = Convert.ToSingle(end3.Text);


            if (g_handle != (IntPtr)0)
            {
                if (move_mode == 0)
                {
                    //相对
                    zmcaux.ZAux_Direct_Base(g_handle, 4, axis_list);

                    zmcaux.ZAux_Direct_SetSpeed(g_handle, axis_list[0], Convert.ToSingle(vspeed.Text));         //设置速度

                    zmcaux.ZAux_Direct_MoveAbs(g_handle, 4, axis_list, v_dist);            //先运动到起点

                    zmcaux.ZAux_Direct_MSpherical(g_handle,4,axis_list, v_end[0], v_end[1], v_end[2], v_mid[0], v_mid[1], v_mid[2], 0, 0, 0);
                }
                else
                {
                    for(i=0;i<3;i++)                                //绝对转相对

                    {
                        v_mid[i] = v_mid[i] - v_start[i];
                        v_end[i] = v_end[i] - v_start[i];
                    }
                    //绝对
                    zmcaux.ZAux_Direct_Base(g_handle, 4, axis_list);

                    zmcaux.ZAux_Direct_SetSpeed(g_handle, axis_list[0], Convert.ToSingle(vspeed.Text));         //设置速度

                    zmcaux.ZAux_Direct_MoveAbs(g_handle,4,axis_list, v_dist);            //先运动到起点
                    zmcaux.ZAux_Direct_MSpherical(g_handle, 4, axis_list, v_end[0], v_end[1], v_end[2], v_mid[0], v_mid[1], v_mid[2], 0, 0, 0);
                }

            }

        }

        private void button16_Click(object sender, EventArgs e)         //配置各个轴的特殊信号
        {
            int i;
            i = 0;
            if (g_handle != (IntPtr)0)
            {
               for (i = 0; i < 4; i++)
                {
                    in_home[i] = Convert.ToInt32(cin_home[i].Text);
                    in_fwd[i] = Convert.ToInt32(cin_fwd[i].Text);
                    in_rev[i] = Convert.ToInt32(cin_rev[i].Text);
                    in_alm[i] = Convert.ToInt32(cin_alm[i].Text);

                    zmcaux.ZAux_Direct_SetDatumIn(g_handle,i,in_home[i]);           //配置原点
                    zmcaux.ZAux_Direct_SetFwdIn(g_handle, i, in_fwd[i]);           //配置正限位
                    zmcaux.ZAux_Direct_SetRevIn(g_handle, i, in_rev[i]);           //配置负限位
                    zmcaux.ZAux_Direct_SetAlmIn(g_handle, i, in_alm[i]);           //配置原点
                }
               strFilePath = Application.StartupPath + "\\" + Adrr + ".bas";
               if (File.Exists(strFilePath))
               {
                   File.Delete(strFilePath);
                   strFilePath = Application.StartupPath + "\\" + Adrr + ".bas";
               }

               save_ini(strFilePath);                      //保存参数
               download(strFilePath);
            }


        }

        private void button24_Click(object sender, EventArgs e)                 //保存反转状态
        {
            if (g_handle != (IntPtr)0)
            {
                strFilePath = Application.StartupPath + "\\" + Adrr + ".bas";
                if (File.Exists(strFilePath))
                {
                    File.Delete(strFilePath);
                    strFilePath = Application.StartupPath + "\\" + Adrr + ".bas";
                }

                save_ini(strFilePath);                      //保存参数
                download(strFilePath);
            }
        }

        private void button17_Click(object sender, EventArgs e)     //打开对应输出口
        {
            int io_num,io_state;
            io_num = Convert.ToInt32(outnum.Text);
            io_state = Convert.ToInt32(outstate.SelectedIndex);

            if (g_handle != (IntPtr)0)
            {
                if (io_state == 0)
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, io_num, 1);//打开输出口
                }
                else
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, io_num, 0);//关闭输出口
                }
            }
        }

        private void button18_Click(object sender, EventArgs e)         //反转对应输入口
        {
            int io_num, io_state;
            io_num = Convert.ToInt32(innum.Text);
            io_state = Convert.ToInt32(instate.SelectedIndex);
            even_state[io_num] = io_state;
            if (g_handle != (IntPtr)0)
            {
                if (io_state == 0)
                {
                    zmcaux.ZAux_Direct_SetInvertIn(g_handle, io_num, 0);//低电平有效，无信号时为OFF,有信号时on
                }
                else
                {
                    zmcaux.ZAux_Direct_SetInvertIn(g_handle, io_num, 1);//高电平有效，无信号时为On,有信号时off
                }
            }

        }


        public void draw_io()
        {
            int i;
 
            uint[] out_state = new uint[16];              
            uint[] in_state = new uint[32];

            Graphics g = this.groupBox7.CreateGraphics();
            Pen redPen = new Pen(Color.Red, 10);
            Pen greenPen = new Pen(Color.Green, 10);
            while(true )
            {
                if (g_handle != (IntPtr)0)
                {
                    g = this.groupBox7.CreateGraphics();
                    for (i = 0; i < 12; i++)            //输出口状态
                    {
                        zmcaux.ZAux_Direct_GetOp(g_handle, i, ref  out_state[i]);
                    }


                    for (i = 0; i < 28; i++)            //输入口状态
                    {
                        zmcaux.ZAux_Direct_GetIn(g_handle, i, ref  in_state[i]);
                        zmcaux.ZAux_Direct_GetInvertIn(g_handle, i, ref even_state[i]);
                    }


                    for (i = 0; i < 12; i++)
                    {
                        g = this.cout_op[i].CreateGraphics();
                        if (out_state[i] == 0)
                        {
                            g.DrawEllipse(redPen, new Rectangle(10, 15, 10, 10));   //红色，无信号
                        }
                        else
                        {    
                            g.DrawEllipse(greenPen, new Rectangle(10, 15, 10, 10));   //绿色    
                        }
                            
                    }

                    g = this.groupBox8.CreateGraphics();
                    for (i = 0; i < 28; i++)
                    {
                        if (in_state[i] == 0)
                        {
                            g.DrawEllipse(redPen, new Rectangle(90 + 55 * (i % 6),40 + 80 * (i / 6), 10, 10));   //红色，无信号
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

                     g.DrawEllipse(greenPen, new Rectangle(120, 450, 10, 10));   //绿色     

                    g.DrawEllipse(redPen, new Rectangle(330, 450, 10, 10));   //绿色   

                }

            }
        }

        private void button21_Click(object sender, EventArgs e)         //导出
        {
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();
            SaveFileDialog1.InitialDirectory = "\\";
            SaveFileDialog1.Filter = "配置文件(*.bas)|*.bas";
            SaveFileDialog1.RestoreDirectory = true;
            SaveFileDialog1.FilterIndex = 1;

            if (SaveFileDialog1.ShowDialog() == DialogResult.OK)            //保存文件
            {
                strFilePath = SaveFileDialog1.FileName;
                if (File.Exists(strFilePath))
                {
                    File.Delete(strFilePath);
                    strFilePath = SaveFileDialog1.FileName;
                }
                save_ini(strFilePath);                      //保存参数
                MessageBox.Show("导出完毕");
            }

        }

        private void button20_Click(object sender, EventArgs e)             //打开导入
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "\\";
            openFileDialog1.Filter = "配置文件(*.bas)|*.bas";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)            //打开配置文件
            {
                strFilePath = openFileDialog1.FileName;
                read_ini(strFilePath);
                download(strFilePath);
            }

        }

        private string ContentValue(string Section, string key,string filepath)        
        {
            string path = filepath;
            StringBuilder temp = new StringBuilder(1024);
            GetPrivateProfileString(Section, key, "", temp, 1024, path);
            return temp.ToString();
        }

         private void save_ini(string filepath)        
        {
              int i = 0;
             string path =filepath;
                try
                {
                    //根据INI文件名设置要写入INI文件的节点名称
                    //此处的节点名称完全可以根据实际需要进行配置 
                    //strSec = System.IO.Path.GetFileNameWithoutExtension(strFilePath);


                    for (i = 0; i < 4; i++)
                    {
                        strSec = "axis" + i.ToString();
                        WritePrivateProfileString(strSec, "atype", c_atype[i].SelectedIndex.ToString().Trim(), path);
                        WritePrivateProfileString(strSec, "invert_step", c_mode[i].Text.Trim(), path);
                        WritePrivateProfileString(strSec, "units", c_units[i].Text.Trim(), path);
                        WritePrivateProfileString(strSec, "speed", c_speed[i].Text.Trim(), path);
                        WritePrivateProfileString(strSec, "accel", c_acc[i].Text.Trim(), path);
                        WritePrivateProfileString(strSec, "decel", c_dec[i].Text.Trim(), path);
                        WritePrivateProfileString(strSec, "fs_limit", c_limit0[i].Text.Trim(), path);
                        WritePrivateProfileString(strSec, "rs_limit", c_limit1[i].Text.Trim(), path);
                        WritePrivateProfileString(strSec, "dpos", c_dpos[i].Text.Trim(), path);

                        WritePrivateProfileString(strSec, "datum_in", cin_home[i].Text.Trim(), path);
                        WritePrivateProfileString(strSec, "fwd_in", cin_fwd[i].Text.Trim(), path);
                        WritePrivateProfileString(strSec, "rev_in", cin_rev[i].Text.Trim(), path);
                        WritePrivateProfileString(strSec, "alm_in", cin_alm[i].Text.Trim(), path);

                        WritePrivateProfileString(strSec, "table(" + (i*4).ToString() + ")", single_speed[i].ToString("f").Trim(), path);
                        WritePrivateProfileString(strSec, "table(" + (i*4 +1).ToString() + ")", single_dist[i].ToString("f").Trim(), path);
                        WritePrivateProfileString(strSec, "table(" + (i*4 +2).ToString() + ")", home_mode[i].ToString().Trim(), path);
                        WritePrivateProfileString(strSec, "table(" + (i*4 +3).ToString() + ")", home_speed[i].ToString("f").Trim(), path);
                        WritePrivateProfileString(strSec, "creep", slow_speed[i].ToString("f").Trim(), path);

                        WritePrivateProfileString(strSec, "table(" + (i + 50).ToString() + ")", en_dir[i].SelectedIndex.ToString().Trim(), path);
                    }



                    for (i = 6; i < 10; i++)
                    {
                        strSec = "axis" + i.ToString();
                        WritePrivateProfileString(strSec, "units", c_eunits[i - 6].Text.Trim(), path);
                    }

                    change_bas(filepath);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

        }

        private void read_ini(string filepath)    
        {
            int i = 0;
            string path = filepath;
            change_ini(filepath);
            for (i = 0; i < 4; i++)
                {
                    strSec = "axis" + i.ToString();
                    c_atype[i].SelectedIndex = Convert.ToInt32(ContentValue(strSec,"atype",path));
                    c_mode[i].Text = ContentValue(strSec, "invert_step",path);
                    c_units[i].Text = ContentValue(strSec, "units",path);
                    c_speed[i].Text = ContentValue(strSec, "speed",path);
                    c_acc[i].Text = ContentValue(strSec, "accel",path);
                    c_dec[i].Text = ContentValue(strSec, "decel",path);
                    c_limit0[i].Text = ContentValue(strSec, "fs_limit",path);
                    c_limit1[i].Text = ContentValue(strSec, "rs_limit",path);
                    c_dpos[i].Text = ContentValue(strSec, "dpos",path);
                    cin_home[i].Text = ContentValue(strSec, "datum_in",path);
                    cin_fwd[i].Text = ContentValue(strSec, "fwd_in",path);
                    cin_rev[i].Text = ContentValue(strSec, "rev_in",path);
                    cin_alm[i].Text = ContentValue(strSec, "alm_in",path);

                    single_speed[i] = Convert.ToSingle(ContentValue(strSec, "table(" + (i * 4).ToString() + ")", path));
                    single_dist[i] = Convert.ToSingle(ContentValue(strSec, "table(" + (i * 4 + 1).ToString() + ")", path));
                    home_mode[i] = Convert.ToInt32(ContentValue(strSec, "table(" + (i * 4 + 2).ToString() + ")", path));
                    home_speed[i] = Convert.ToSingle(ContentValue(strSec, "table(" + (i * 4 + 3).ToString() + ")", path));
                    slow_speed[i] = Convert.ToSingle(ContentValue(strSec, "creep", path));

                    en_dir[i].SelectedIndex = Convert.ToInt32(ContentValue(strSec, "table(" + (i + 50).ToString() + ")", path));
                }


            for (i = 6; i < 10; i++)
            {
                strSec = "axis" + i.ToString();
                c_eunits[i - 6].Text = ContentValue(strSec, "units", path);
            }

            get_val();            //读取界面上到变量
            change_bas(filepath);
        }

        private void change_bas(string filepath)            //转换为BAS文件,并下载到控制器
        {
            StreamReader reader = new StreamReader(filepath, Encoding.Default);
            StreamWriter readTxt = new StreamWriter(@"temp.bas", false, Encoding.Default);
            String a = reader.ReadToEnd();
            int i = 0;

            a = a.Replace("[axis0]", "base(0)");
            a = a.Replace("[axis1]", "base(1)");
            a = a.Replace("[axis2]", "base(2)");
            a = a.Replace("[axis3]", "base(3)");
            a = a.Replace("[axis6]", "base(6)");
            a = a.Replace("[axis7]", "base(7)");
            a = a.Replace("[axis8]", "base(8)");
            a = a.Replace("[axis9]", "base(9)");
            readTxt.Write(a);

            for (i = 0; i < 4; i++)
            {
                if (en_dir[i].SelectedIndex == 0)
                {
                    readTxt.Write("ENCODER_RATIO(1,1)" + " axis(" +  (i+6).ToString()  + ")\r\n");
                }
                else
                {
                    readTxt.Write("ENCODER_RATIO(-1,1)" + " axis(" + (i+6).ToString() + ")\r\n");
                }
            }

            for (i = 0; i < 31; i++)
            {
                readTxt.Write("invert_in(" + i.ToString() + "," + even_state[i].ToString() + ")\r\n");
            }

            readTxt.Flush();
            readTxt.Close();
            reader.Close();
            //b.hhp重命名为a.hhp,并删除b.hhp
            File.Copy(@"temp.bas", filepath, true);
            File.Delete(@"temp.bas");     
        }

        private void change_ini(string filepath)            //转换为INI文件,
        {
            StreamReader reader = new StreamReader(filepath, Encoding.Default);
            StreamWriter readTxt = new StreamWriter(@"temp.bas", false, Encoding.Default);
            String a = reader.ReadToEnd();

            a = a.Replace( "base(0)","[axis0]");
            a = a.Replace("base(1)", "[axis1]");
            a = a.Replace("base(2)", "[axis2]");
            a = a.Replace("base(3)", "[axis3]");
            a = a.Replace("base(6)", "[axis6]");
            a = a.Replace("base(7)", "[axis7]");
            a = a.Replace("base(8)", "[axis8]");
            a = a.Replace("base(9)", "[axis9]");
            readTxt.Write(a);

            readTxt.Flush();
            readTxt.Close();
            reader.Close();
            //b.hhp重命名为a.hhp,并删除b.hhp
            File.Copy(@"temp.bas", filepath, true);
            File.Delete(@"temp.bas");
        }

         private void download(string filepath)            //下载BAS程序到控制器
        {

             int ret =0;
             if (g_handle != (IntPtr)0)
            {
                //ret = zmcaux.ZAux_BasDown(g_handle, filepath);
                if (ret == 0)
                {
                    MessageBox.Show("下载完成");
                }
                else
                {
                    MessageBox.Show("下载失败");     
                }
              
            }
            else
            {
                 MessageBox.Show("请先链接控制器！");
            }
        }


        private void op0_Click(object sender, EventArgs e)      //操作输出口
        {
            uint temp_state = 0;
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_GetOp(g_handle, 0, ref  temp_state);
                if (temp_state == 0)
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 0, 1);
                }
                else
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 0, 0);
                }
            }
        }

        private void op1_Click(object sender, EventArgs e)
        {
            uint temp_state = 0;
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_GetOp(g_handle, 1, ref  temp_state);
                if (temp_state == 0)
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 1, 1);
                }
                else
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 1, 0);
                }
            }
        }

        private void op2_Click(object sender, EventArgs e)
        {
            uint temp_state = 0;
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_GetOp(g_handle, 2, ref  temp_state);
                if (temp_state == 0)
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 2, 1);
                }
                else
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 2, 0);
                }
            }
        }

        private void op3_Click(object sender, EventArgs e)
        {
            uint temp_state = 0;
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_GetOp(g_handle, 3, ref  temp_state);
                if (temp_state == 0)
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 3, 1);
                }
                else
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 3, 0);
                }
            }
        }

        private void op4_Click(object sender, EventArgs e)
        {
            uint temp_state = 0;
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_GetOp(g_handle, 4, ref  temp_state);
                if (temp_state == 0)
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 4, 1);
                }
                else
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 4, 0);
                }
            }
        }

        private void op5_Click(object sender, EventArgs e)
        {
            uint temp_state = 0;
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_GetOp(g_handle, 5, ref  temp_state);
                if (temp_state == 0)
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 5, 1);
                }
                else
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 5, 0);
                }
            }
        }

        private void op6_Click(object sender, EventArgs e)
        {
            uint temp_state = 0;
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_GetOp(g_handle, 6, ref  temp_state);
                if (temp_state == 0)
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 6, 1);
                }
                else
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 6, 0);
                }
            }
        }

        private void op7_Click(object sender, EventArgs e)
        {
            uint temp_state = 0;
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_GetOp(g_handle, 7, ref  temp_state);
                if (temp_state == 0)
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 7, 1);
                }
                else
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 7, 0);
                }
            }
        }

        private void op8_Click(object sender, EventArgs e)
        {
            uint temp_state = 0;
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_GetOp(g_handle, 8, ref  temp_state);
                if (temp_state == 0)
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 8, 1);
                }
                else
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 8, 0);
                }
            }
        }

        private void op9_Click(object sender, EventArgs e)
        {
            uint temp_state = 0;
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_GetOp(g_handle, 9, ref  temp_state);
                if (temp_state == 0)
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 9, 1);
                }
                else
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 9, 0);
                }
            }
        }

        private void op10_Click(object sender, EventArgs e)
        {
            uint temp_state = 0;
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_GetOp(g_handle, 10, ref  temp_state);
                if (temp_state == 0)
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 10, 1);
                }
                else
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 10, 0);
                }
            }
        }

        private void op11_Click(object sender, EventArgs e)
        {
            uint temp_state = 0;
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_GetOp(g_handle, 11, ref  temp_state);
                if (temp_state == 0)
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 11, 1);
                }
                else
                {
                    zmcaux.ZAux_Direct_SetOp(g_handle, 11, 0);
                }
            }
        }

        private void button23_Click(object sender, EventArgs e)     //修改编码轴脉冲当量
        {
            int i = 0;
            float temp_units;
            if (g_handle != (IntPtr)0)
            {
                for (i = 6; i < 10; i++)
                {
                    temp_units = Convert.ToSingle(c_eunits[i - 6].Text);
                    zmcaux.ZAux_Direct_SetUnits(g_handle, i, temp_units);            
                }

            }
            strFilePath = Application.StartupPath + "\\" + Adrr + ".bas";
            if (File.Exists(strFilePath))
            {
                File.Delete(strFilePath);
                strFilePath = Application.StartupPath + "\\" + Adrr + ".bas";
            }

            save_ini(strFilePath);                      //保存参数
            download(strFilePath);
        }

        private void timer2_Tick(object sender, EventArgs e)        //检测网口链接是否断线
        {
            if (g_handle != (IntPtr)0)
            {
                aaa = zmcaux.ZAux_SearchEth(Adrr, 2000);     //搜索控制器
                if (aaa != 0)                             //找不到IP了
                {
                    g_handle = (IntPtr)0;
                    this.Text = "未连接";
                    timer2.Enabled = false;
                }
            }
        }

        private void axisnum_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (Convert.ToInt32(axisnum.Text))
            {
                case 0:
                    single_sp.Text = single_speed[0].ToString("f");
                    singe_dist.Text = single_dist[0].ToString("f");
                    datum.Text = home_mode[0].ToString();
                    datumsp.Text = home_speed[0].ToString("f");
                    datum_slow.Text = slow_speed[0].ToString("f");
                    break;
                case 1:
                    single_sp.Text = single_speed[1].ToString("f");
                    singe_dist.Text = single_dist[1].ToString("f");
                    datum.Text = home_mode[1].ToString();
                    datumsp.Text = home_speed[1].ToString("f");
                    datum_slow.Text = slow_speed[1].ToString("f");
                    break;
                case 2:
                    single_sp.Text = single_speed[2].ToString("f");
                    singe_dist.Text = single_dist[2].ToString("f");
                    datum.Text = home_mode[2].ToString();
                    datumsp.Text = home_speed[2].ToString("f");
                    datum_slow.Text = slow_speed[2].ToString("f");
                    break;
                case 3:
                    single_sp.Text = single_speed[3].ToString("f");
                    singe_dist.Text = single_dist[3].ToString("f");
                    datum.Text = home_mode[3].ToString();
                    datumsp.Text = home_speed[3].ToString("f");
                    datum_slow.Text = slow_speed[3].ToString("f");
                    break;
                default:
                    break;
            }
        }

        private void single_sp_TextChanged(object sender, EventArgs e)
        {
            single_axis = Convert.ToInt32(axisnum.Text);
            single_speed[single_axis] = Convert.ToSingle(single_sp.Text);
        }

        private void singe_dist_TextChanged(object sender, EventArgs e)
        {
            single_axis = Convert.ToInt32(axisnum.Text);
            single_dist[single_axis] = Convert.ToSingle(singe_dist.Text);
        }

        private void datum_SelectedIndexChanged(object sender, EventArgs e)
        {
            single_axis = Convert.ToInt32(axisnum.Text);
            home_mode[single_axis] = Convert.ToInt32(datum.Text);

        }

        private void datumsp_TextChanged(object sender, EventArgs e)
        {
            single_axis = Convert.ToInt32(axisnum.Text);
            home_speed[single_axis] = Convert.ToSingle(datumsp.Text);
        }

        private void datum_slow_TextChanged(object sender, EventArgs e)
        {
            single_axis = Convert.ToInt32(axisnum.Text);
            slow_speed[single_axis] = Convert.ToSingle(datum_slow.Text);
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }




    }
    
}
