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

namespace test_faction3
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号

        public Form1()
        {
            InitializeComponent();
            //链接控制器 
            this.Text = "未链接";
            MessageBox.Show("请手动链接控制器"); 
        }

        //关闭软件
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //断开链接
            timer1.Enabled = false;
            zmcaux.ZAux_Close(g_handle);
            g_handle = (IntPtr)0;
        }

        //定时器刷新
        private void timer1_Tick(object sender, EventArgs e)
        {
            uint[] iostate = new uint[4];
            int axisstate =0;

            for (int i = 0; i < 4; i++)
            {
                zmcaux.ZAux_Direct_GetIn(g_handle, i, ref iostate[i]);
            }
                zmcaux.ZAux_Direct_GetAxisStatus(g_handle, 0, ref axisstate);

            label_in.Text = "输入0 - (" + iostate[0] + ") " + "输入1 - (" + iostate[1] + ") " + "输入2 - (" + iostate[2] + ") " + "输入3 - (" + iostate[3] + ") ";

            if (axisstate == 0)
            {
                label_axis0.Text = "轴状态：正  常";
            }
            else if(((axisstate >> 4) & 1) == 1)
            {
                label_axis0.Text = "轴状态：正向硬限位报警！";              
            }
            else if (((axisstate >> 5) & 1) == 1)
            {
                label_axis0.Text = "轴状态：负向硬限位报警！";
            }
            else if (((axisstate >> 22) & 1) == 1)
            {
                label_axis0.Text = "轴状态：伺服报警！";
            }
        }

        //特殊信号反转
        private void checkBox_home_CheckedChanged(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            if (checkBox_home.Checked)
            {
                zmcaux.ZAux_Direct_SetInvertIn(g_handle, 0, 1);
                checkBox_home.Text = "常闭";
            }
            else
            {
                zmcaux.ZAux_Direct_SetInvertIn(g_handle, 0, 0);
                checkBox_home.Text = "常开";
            }
        }

        //特殊信号反转
        private void checkBox_alm_CheckedChanged(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            if (checkBox_alm.Checked)
            {
                zmcaux.ZAux_Direct_SetInvertIn(g_handle, 1, 1);
                checkBox_alm.Text = "常闭";
            }
            else
            {
                zmcaux.ZAux_Direct_SetInvertIn(g_handle, 1, 0);
                checkBox_alm.Text = "常开";
            }
        }

        //特殊信号反转
        private void checkBox_fwd_CheckedChanged(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }

            if (checkBox_fwd.Checked)
            {
                zmcaux.ZAux_Direct_SetInvertIn(g_handle, 2, 1);
                checkBox_fwd.Text = "常闭";
            }
            else
            {
                zmcaux.ZAux_Direct_SetInvertIn(g_handle, 2, 0);
                checkBox_fwd.Text = "常开";
            }
        }

        //特殊信号反转
        private void checkBox_rev_CheckedChanged(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }

            if (checkBox_rev.Checked)
            {
                zmcaux.ZAux_Direct_SetInvertIn(g_handle, 3, 1);
                checkBox_rev.Text = "常闭";
            }
            else
            {
                zmcaux.ZAux_Direct_SetInvertIn(g_handle, 3, 0);
                checkBox_rev.Text = "常开";
            }
        }

        //out0
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }

            if (checkBox1.Checked)
            {
                zmcaux.ZAux_Direct_SetOp(g_handle,0, 1);
                checkBox1.Text = "out0 打开";
            }
            else
            {
                zmcaux.ZAux_Direct_SetOp(g_handle, 0, 0);
                checkBox1.Text = "out0 关闭";
            }
        }

        //out1
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }

            if (checkBox2.Checked)
            {
                zmcaux.ZAux_Direct_SetOp(g_handle, 1, 1);
                checkBox2.Text = "out1 打开";
            }
            else
            {
                zmcaux.ZAux_Direct_SetOp(g_handle, 1, 0);
                checkBox2.Text = "out1 关闭";
            }
        }

        //out2
        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }

            if (checkBox3.Checked)
            {
                zmcaux.ZAux_Direct_SetOp(g_handle, 2, 1);
                checkBox3.Text = "out2 打开";
            }
            else
            {
                zmcaux.ZAux_Direct_SetOp(g_handle, 2, 0);
                checkBox3.Text = "out2 关闭";
            }
        }

        //out3
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }

            if (checkBox4.Checked)
            {
                zmcaux.ZAux_Direct_SetOp(g_handle, 3, 1);
                checkBox4.Text = "out3 打开";
            }
            else
            {
                zmcaux.ZAux_Direct_SetOp(g_handle, 3, 0);
                checkBox4.Text = "out3 关闭";
            }
        }

        //out all
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            byte[] iostate = new byte[4];

            if (checkBox5.Checked)
            {
                iostate[0] = 15;
                zmcaux.ZAux_Modbus_Set0x(g_handle, 20000,4,iostate);
                checkBox5.Text = "全部打开";
                checkBox1.Checked = true;
                checkBox2.Checked = true;
                checkBox3.Checked = true;
                checkBox4.Checked = true;
            }
            else
            {
                iostate[0] = 0;
                zmcaux.ZAux_Modbus_Set0x(g_handle, 20000,4 ,iostate);
                checkBox5.Text = "全部关闭";
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;
            }
        }


        

        private void OnSelect(object sender, EventArgs e)
        {
            C_Ip_Address.SelectedIndex = -1;
        }

        private void OnScan(object sender, EventArgs e)
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
            }
            else if (String == "PCI")
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
                C_Ip_Address.Items.Insert(i, sArray[i].ToString());
            }
        }

        private void Com_SCAN()
        {
            string[] ports = SerialPort.GetPortNames();

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
                tmpstr = string.Format("PCI{0:D}", i + 1);
                C_Ip_Address.Items.Insert(i, tmpstr);
            }
        }

        private void OnOpen(object sender, EventArgs e)
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

                zmcaux.ZAux_Direct_SetDatumIn(g_handle, 0, 0);        //原点in(0)
                zmcaux.ZAux_Direct_SetAlmIn(g_handle, 0, 1);          //伺服报警in(1)
                zmcaux.ZAux_Direct_SetFwdIn(g_handle, 0, 2);          //正限位in(2)
                zmcaux.ZAux_Direct_SetRevIn(g_handle, 0, 3);          //负限位in(3)

                checkBox_alm.Checked = true;                    //ZMC控制器默认信号OFF时有效
                checkBox_fwd.Checked = true;
                checkBox_rev.Checked = true;              
            }
        }

        private void OnClose(object sender, EventArgs e)
        {
            //断开链接
            timer1.Enabled = false;
            zmcaux.ZAux_Close(g_handle);
            g_handle = (IntPtr)0;
            this.Text = "未连接";
        }  
    }
}
