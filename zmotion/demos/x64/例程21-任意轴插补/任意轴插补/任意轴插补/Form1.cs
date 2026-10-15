using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

using cszmcaux;

namespace 任意轴插补
{
    public partial class Form1 : Form
    {


        /* 声明信息 */

        //作者：陈亮任

        //用途：多开后验证客户反馈的多轴组插补有干扰问题，如检查  0 - 1这组轴插补运动时，对 2- 3组轴的运动是否存在影响

        public IntPtr g_handle;
        public string g_command;       //发送的命令
        public const int g_maxtime = 500;       //最大等待时间
        public int aaa;             //    '调试时，查看返回值
        public float fValue;          //浮点参数
        public string Adrr;             //连接的IP地址





        public Form1()
        {
            InitializeComponent();
            this.Text = System.AppDomain.CurrentDomain.FriendlyName.ToString() + "- - - 未连接";
        }

        private void comboBox2_DropDown(object sender, EventArgs e)
        {
            int i, num,aaa;
            string[] sArray;
            StringBuilder buffer = new StringBuilder(10240);
            string buff = "";
            aaa = zmcaux.ZAux_SearchEthlist(buffer, 10230, 200);
            buff += buffer;
            sArray = buff.Split(' ');
            num = buff.Split(' ').Length;

            comboBox2.Items.Clear();

            comboBox2.Items.Insert(0, "127.0.0.1");
            for (i = 0; i < num - 1; i++)
            {
                comboBox2.Items.Insert(i + 1, sArray[i].ToString());
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            byte[] tempbuff = new byte[1024];               //接收的返回的字符
            if (g_handle == (IntPtr)0)
            {
                Adrr = comboBox2.Text;
                if (Adrr == "127.0.0.1")
                {
                    zmcaux.ZAux_OpenEth(Adrr, out g_handle);
                }
                else
                {
                    aaa = zmcaux.ZAux_SearchEth(Adrr, 3000);     //搜索控制器
                    if (aaa == 0)
                    {
                        zmcaux.ZAux_OpenEth(Adrr, out g_handle);
                    }
                    else
                    {
                        MessageBox.Show("找不到控制器！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            if (g_handle != (IntPtr)0)
            {

                this.Text = System.AppDomain.CurrentDomain.FriendlyName.ToString() + " - - - 已连接";

                move_axis_select(move_axis);

                move_axis_select(move_axis2);

                axis_move.Enabled = true;

                get_param();
                get_param2();

            }
            else
            {
                this.Text = System.AppDomain.CurrentDomain.FriendlyName.ToString() + "- - - 未连接";
                MessageBox.Show("控制器链接失败！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {

                run_timer.Enabled = false;

                int[] axislist = { Convert.ToInt16(move_axis.Text), Convert.ToInt16(move_axis2.Text) };

                zmcaux.ZAux_Direct_CancelAxisList(g_handle, 2, axislist, 2);

                zmcaux.ZAux_Close(g_handle);
                g_handle = (IntPtr)0;

                axis_move.Enabled = false;

            }

            this.Text = System.AppDomain.CurrentDomain.FriendlyName.ToString() + "- - - 未连接";
        }



        //运动轴选择
        public void move_axis_select(ComboBox dd)
        {
            int i;
            for (i = 0; i < 128; i++)
            {
                dd.Items.Add(i.ToString());
            }
        }

        private void axis_move_Tick(object sender, EventArgs e)
        {
            float re_dpos = 0;

            float re_mpos = 0;

            int re_idle = 0;

            float re_vpspeed = 0;

            float re_mspeed = 0;

            zmcaux.ZAux_Direct_GetDpos(g_handle, Convert.ToInt16(move_axis.Text), ref re_dpos);

            zmcaux.ZAux_Direct_GetMpos(g_handle, Convert.ToInt16(move_axis.Text), ref re_mpos);


            cur_dpos.Text = re_dpos.ToString();

            cur_mpos.Text = re_mpos.ToString();

            zmcaux.ZAux_Direct_GetIfIdle(g_handle, Convert.ToInt16(move_axis.Text), ref re_idle);


           if (re_idle == 0)
           {
               cur_idle.Text = "正在运动";
           }
           else if (re_idle == -1)
           {
              cur_idle.Text = "停止状态";
           }


            zmcaux.ZAux_Direct_GetVpSpeed(g_handle, Convert.ToInt16(move_axis.Text), ref re_vpspeed);

            zmcaux.ZAux_Direct_GetMspeed(g_handle, Convert.ToInt16(move_axis.Text), ref re_mspeed);

            vp_speed.Text = re_vpspeed.ToString();

            mspeed.Text = re_mspeed.ToString();






            zmcaux.ZAux_Direct_GetDpos(g_handle, Convert.ToInt16(move_axis2.Text), ref re_dpos);

            zmcaux.ZAux_Direct_GetMpos(g_handle, Convert.ToInt16(move_axis2.Text), ref re_mpos);


            cur_dpos2.Text = re_dpos.ToString();

            cur_mpos2.Text = re_mpos.ToString();

            zmcaux.ZAux_Direct_GetIfIdle(g_handle, Convert.ToInt16(move_axis2.Text), ref re_idle);


            if (re_idle == 0)
            {
                cur_idle2.Text = "正在运动";
            }
            else if (re_idle == -1)
            {
                cur_idle2.Text = "停止状态";
            }


            zmcaux.ZAux_Direct_GetVpSpeed(g_handle, Convert.ToInt16(move_axis2.Text), ref re_vpspeed);

            zmcaux.ZAux_Direct_GetMspeed(g_handle, Convert.ToInt16(move_axis2.Text), ref re_mspeed);

            vp_speed2.Text = re_vpspeed.ToString();

            mspeed2.Text = re_mspeed.ToString();

        }

        private void star_run_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                if (Convert.ToInt16(move_axis.Text) != Convert.ToInt16(move_axis2.Text))
                {
                    run_timer.Enabled = true;

                    move_axis.Enabled = false;

                    move_axis2.Enabled = false;

                    atc.Enabled = false;

                    atc2.Enabled = false;

                    axis_dis.Enabled = false;

                    axis_dis2.Enabled = false;

                }
            }
        }

        private void run_timer_Tick(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                int[] axislist = { Convert.ToInt16(move_axis.Text), Convert.ToInt16(move_axis2.Text)};

                float[] dposlist = { Convert.ToInt16(axis_dis.Text), Convert.ToInt16(axis_dis2.Text) };

                int remain = 0;
                zmcaux.ZAux_Direct_GetRemain_LineBuffer(g_handle, axislist[0], ref remain);

                while (remain < 2)
                {
                    zmcaux.ZAux_Direct_GetRemain_LineBuffer(g_handle, axislist[0], ref remain);
                }

                zmcaux.ZAux_Direct_Move(g_handle, 2, axislist, dposlist);
            }
        }

        private void stop_run_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                run_timer.Enabled = false;

                int[] axislist = { Convert.ToInt16(move_axis.Text), Convert.ToInt16(move_axis2.Text) };

                zmcaux.ZAux_Direct_CancelAxisList(g_handle, 2, axislist, 2);

                move_axis.Enabled = true;

                move_axis2.Enabled = true;

                atc.Enabled = true;

                atc2.Enabled = true;

                axis_dis.Enabled = true;

                axis_dis2.Enabled = true;
            }
        }

        private void atc_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_SetUnits(g_handle, Convert.ToInt16(move_axis.Text), Convert.ToSingle(units.Text));

                zmcaux.ZAux_Direct_SetSpeed(g_handle, Convert.ToInt16(move_axis.Text), Convert.ToSingle(speed.Text));

                zmcaux.ZAux_Direct_SetAccel(g_handle, Convert.ToInt16(move_axis.Text), Convert.ToSingle(accel.Text));

                zmcaux.ZAux_Direct_SetDecel(g_handle, Convert.ToInt16(move_axis.Text), Convert.ToSingle(decel.Text));

                zmcaux.ZAux_Direct_SetMerge(g_handle, Convert.ToInt16(move_axis.Text), 1);

            }
        }

        private void atc2_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                zmcaux.ZAux_Direct_SetUnits(g_handle, Convert.ToInt16(move_axis2.Text), Convert.ToSingle(units2.Text));

                zmcaux.ZAux_Direct_SetSpeed(g_handle, Convert.ToInt16(move_axis2.Text), Convert.ToSingle(speed2.Text));

                zmcaux.ZAux_Direct_SetAccel(g_handle, Convert.ToInt16(move_axis2.Text), Convert.ToSingle(accel2.Text));

                zmcaux.ZAux_Direct_SetDecel(g_handle, Convert.ToInt16(move_axis2.Text), Convert.ToSingle(decel2.Text));

                zmcaux.ZAux_Direct_SetMerge(g_handle, Convert.ToInt16(move_axis2.Text), 1);

            }
        }


        public void get_param()
        {

            if (g_handle != (IntPtr)0)
            {
                float get_units, get_speed, get_accel, get_decel;

                get_units = 0;
                get_accel = 0;
                get_decel = 0;
                get_speed = 0;

                zmcaux.ZAux_Direct_GetUnits(g_handle, Convert.ToInt16(move_axis.Text), ref get_units);

                zmcaux.ZAux_Direct_GetSpeed(g_handle, Convert.ToInt16(move_axis.Text), ref get_speed);

                zmcaux.ZAux_Direct_GetDecel(g_handle, Convert.ToInt16(move_axis.Text), ref get_decel);

                zmcaux.ZAux_Direct_GetAccel(g_handle, Convert.ToInt16(move_axis.Text), ref get_accel);

                units.Text = get_units.ToString();

                speed.Text = get_speed.ToString();

                accel.Text = get_accel.ToString();

                decel.Text = get_decel.ToString();
            }
        }

        public void get_param2()
        {

            if (g_handle != (IntPtr)0)
            {
                float get_units, get_speed, get_accel, get_decel;

                get_units = 0;
                get_accel = 0;
                get_decel = 0;
                get_speed = 0;

                zmcaux.ZAux_Direct_GetUnits(g_handle, Convert.ToInt16(move_axis2.Text), ref get_units);

                zmcaux.ZAux_Direct_GetSpeed(g_handle, Convert.ToInt16(move_axis2.Text), ref get_speed);

                zmcaux.ZAux_Direct_GetDecel(g_handle, Convert.ToInt16(move_axis2.Text), ref get_decel);

                zmcaux.ZAux_Direct_GetAccel(g_handle, Convert.ToInt16(move_axis2.Text), ref get_accel);

                units2.Text = get_units.ToString();

                speed2.Text = get_speed.ToString();

                accel2.Text = get_accel.ToString();

                decel2.Text = get_decel.ToString();
            }
        }

        private void move_axis_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_param();
        }

        private void move_axis2_SelectedIndexChanged(object sender, EventArgs e)
        {
            get_param2();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }
    }
}
