using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

using cszmcaux;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace test_function1
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
        public int nAxis = 0;           //轴号
        public int dir = 1;             //运动方向
        public Form1()
        {
            InitializeComponent();
            C_connect_type.SelectedIndex = 0;
            this.Text = "未链接";
            MessageBox.Show("请手动链接控制器");
            
           
        }
        //关闭窗口，关闭链接，释放HANDLE
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //断开链接
            timer1.Enabled = false;
            zmcaux.ZAux_Close(g_handle);
            g_handle = (IntPtr)0;
        }

        //开始运动
        private void OnStart(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                //设置轴参数
                zmcaux.ZAux_Direct_SetAtype(g_handle, nAxis, 1);
                zmcaux.ZAux_Direct_SetUnits(g_handle, nAxis, Convert.ToSingle(TextBox_units.Text));
                zmcaux.ZAux_Direct_SetLspeed(g_handle, nAxis, Convert.ToSingle(TextBox_lspeed.Text));
                zmcaux.ZAux_Direct_SetSpeed(g_handle, nAxis, Convert.ToSingle(TextBox_speed.Text));
                zmcaux.ZAux_Direct_SetAccel(g_handle, nAxis, Convert.ToSingle(TextBox_accel.Text));
                zmcaux.ZAux_Direct_SetDecel(g_handle, nAxis, Convert.ToSingle(TextBox_decel.Text));
                zmcaux.ZAux_Direct_SetSramp(g_handle, nAxis, Convert.ToSingle(TextBox_sramp.Text));

                if (radioButton5.Checked)       //连续运动
                {
                    zmcaux.ZAux_Direct_Single_Vmove(g_handle, nAxis, dir);
                }
                else                               //寸动
                {
                    zmcaux.ZAux_Direct_Single_Move(g_handle, nAxis, dir * Convert.ToSingle(TextBox_step.Text));
                }
            }

        }

        //停止运动
        private void OnStop(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, nAxis, 2);
            }
        }

        //坐标清零
        private void OnZero(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    zmcaux.ZAux_Direct_SetDpos(g_handle, i, 0);
                }
            }
        }

        //定时器
        private void timer1_Tick(object sender, EventArgs e)
        {
            int runstate = 0;
            float curpos = 0;
            float curspeed = 0;

            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("链接断开");
                return;
            }

            zmcaux.ZAux_Direct_GetIfIdle(g_handle, nAxis, ref runstate);
            zmcaux.ZAux_Direct_GetDpos(g_handle, nAxis, ref curpos);
            zmcaux.ZAux_Direct_GetVpSpeed(g_handle, nAxis, ref curspeed);

            label_runstate.Text = "运动状态：" + Convert.ToString(runstate == 0 ? "运行中" : "停止中");
            label_curpos.Text = "当前轴坐标：" + curpos;
            label_cursp.Text = "当前轴速度：" + curspeed;
        }

        //X轴
        private void OnRadio1X(object sender, EventArgs e)
        {
            nAxis = 0;
        }

        //Y轴
        private void OnRadio1Y(object sender, EventArgs e)
        {
            nAxis = 1;
        }

        //Z轴
        private void OnRadio1Z(object sender, EventArgs e)
        {
            nAxis = 2;
        }

        //R轴
        private void OnRadio1R(object sender, EventArgs e)
        {
            nAxis = 3;
        }

        private void OnCheckDir(object sender, EventArgs e)
        {
            if (checkBox1.Checked == false)
            {
                checkBox1.Text = "运动方向：正";
                dir = 1;
            }
            else
            {
                checkBox1.Text = "运动方向：负";
                dir = -1;
            }
        }  

        private void C_Ip_Address_DropDown(object sender, EventArgs e)    //下拉搜索控制器IP
        {
            string String;
            String = C_connect_type.Text;
            C_Ip_Address.Items.Clear();
            if (String == "网口")
            {
                IP_SCAN();
            }
            else if (String == "串口")
            {
                Com_SCAN();
            }else if(String == "PCI")
            {
                PCI_SCAN();
            }
            else if (String == "LOCAL")
            {
                C_Ip_Address.Items.Add("LOCAL1");
            }
            else
            {
                MessageBox.Show("请选择正确的链接类型！");
                return;
            }   
        }

        private void OnOpen(object sender, EventArgs e)       //网口连接控制器
        {
            if (g_handle != (IntPtr)0)
            {
                OnClose(sender, e);
            }

           
            int iresult;
            string String;
            String = C_connect_type.Text;

            string Buffer;
            Buffer = C_Ip_Address.Text;

            int ctype = 0;
            if (String == "网口")
            {
                ctype = 2;
            }
            else if (String == "串口")
            {
                ctype = 1;
            }
            else if (String == "PCI")
            {
                ctype = 4;
            }
            else if (String == "LOCAL")
            {
                ctype = 5;
            }
            else
            {
                MessageBox.Show("请选择正确的链接类型！");
                return;
            }

            iresult = zmcaux.ZAux_FastOpen(ctype, Buffer, 1000, out g_handle);

            if (iresult != 0)
            {
                g_handle = (IntPtr)0;
                MessageBox.Show("链接失败");
                this.Text = "未连接";
                return;
            }
            else 
            {
                this.Text = "已连接";
                timer1.Enabled = true;
            }
        }

        private void OnClose(object sender, EventArgs e) //断开链接
        {
            //断开链接
            timer1.Enabled = false;
            zmcaux.ZAux_Close(g_handle);
            g_handle = (IntPtr)0;
            this.Text = "未连接";
        }
        
        private void IP_SCAN()
        {
            int i, num;
            string[] sArray;
            StringBuilder buffer = new StringBuilder(10240);
            string buff = "";
            zmcaux.ZAux_SearchEthlist(buffer, 10230, 200);
            buff += buffer;
            sArray = buff.Split(' ');
            num = buff.Split(' ').Length;

            for (i = 0; i < num - 1; i++)
            {
                C_Ip_Address.Items.Insert(i , sArray[i].ToString());
            }
        }

        private void Com_SCAN()
        {           
            string[] ports =  SerialPort.GetPortNames();

            foreach (string s in ports)
			{
                C_Ip_Address.Items.Add(s);
			} 
            
               
            
        }

        private void PCI_SCAN()
        {
            string tmpstr;
            int Card;
            Card = zmcaux.ZAux_GetMaxPciCards();

            for (int i = 0; i < Card; i++)
            {
                tmpstr = string.Format("PCI{0:D}", i+1);
                C_Ip_Address.Items.Insert(i, tmpstr);
            }
        }

        private void C_connect_type_DropDown(object sender, EventArgs e)
        {
            C_Ip_Address.SelectedIndex = -1;
        }

        
    }

    
}

