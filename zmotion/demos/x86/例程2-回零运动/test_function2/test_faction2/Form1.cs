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

namespace test_faction2
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
        public int nAxis = 0;           //轴号
        public int home_mode = 4;             //回零模式

        public Form1()
        {
            InitializeComponent();
            //链接控制器 
            this.Text = "未链接";
            MessageBox.Show("请手动链接控制器");
            radioButton7_CheckedChanged(new object(),new EventArgs());
        }

        //窗口关闭
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //断开链接
            timer1.Enabled = false;
            zmcaux.ZAux_Close(g_handle);
            g_handle = (IntPtr)0;
        }


        //回零
        private void button_home_Click(object sender, EventArgs e)
        {
            int ret = 0;
            int tmp_mode = 0;

            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            
            int status = 0;
            zmcaux.ZAux_Direct_GetIfIdle(g_handle, nAxis, ref status);
            if (status == 0)
            {
                MessageBox.Show("轴在运动！");
                return;
            }

            
            //设置轴参数
            zmcaux.ZAux_Direct_SetInvertStep(g_handle, nAxis, 0);

            ret = zmcaux.ZAux_Direct_SetAtype(g_handle, nAxis, Convert.ToInt32(TextBox_atype.Text));        //可以用于Z相回零
            if (ret != 0)
            { 
                MessageBox.Show("轴类型设置失败!");
            }

            zmcaux.ZAux_Direct_SetUnits(g_handle, nAxis, Convert.ToSingle(TextBox_units.Text));
            zmcaux.ZAux_Direct_SetLspeed(g_handle, nAxis, Convert.ToSingle(TextBox_lspeed.Text));
            zmcaux.ZAux_Direct_SetSpeed(g_handle, nAxis, Convert.ToSingle(TextBox_speed.Text));
            zmcaux.ZAux_Direct_SetAccel(g_handle, nAxis, Convert.ToSingle(TextBox_accel.Text));
            zmcaux.ZAux_Direct_SetDecel(g_handle, nAxis, Convert.ToSingle(TextBox_decel.Text));
            zmcaux.ZAux_Direct_SetCreep(g_handle, nAxis, Convert.ToSingle(TextBox_creep.Text));

            zmcaux.ZAux_Direct_SetDatumIn(g_handle, nAxis, Convert.ToInt32(TextBox_homeio.Text)); //配置原点信号。ZMC系列默认OFF时信号有效，常开传感器需要反转输入口为ON
            zmcaux.ZAux_Direct_SetFwdIn(g_handle, nAxis, Convert.ToInt32(TextBox_fwdio.Text));
            zmcaux.ZAux_Direct_SetRevIn(g_handle, nAxis,Convert.ToInt32(TextBox_revio.Text));
            bool t_state = checkBox1.Checked;
            //风险代码 需要测试
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, Convert.ToInt32(TextBox_homeio.Text), Convert.ToInt32(t_state));
            t_state = checkBox2.Checked;
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, Convert.ToInt32(TextBox_fwdio.Text), Convert.ToInt32(t_state));
            t_state = checkBox3.Checked;
            zmcaux.ZAux_Direct_SetInvertIn(g_handle, Convert.ToInt32(TextBox_revio.Text), Convert.ToInt32(t_state));
            
            //zmcaux.ZAux_SetTraceFile(3, "预留");
            
            if (radioButton6.Checked == false)
            {
                if (radioButton8.Checked == true)
                {
                    tmp_mode = 0;
                }
                else
                {
                    tmp_mode = 10;
                }

                ret = zmcaux.ZAux_Direct_Single_Datum(g_handle, nAxis, home_mode + tmp_mode);
                if (ret != 0)
                {
                    MessageBox.Show("回零执行失败!");
                }
                
            }
            else if (radioButton6.Checked == true)
            {
                ret = zmcaux.ZAux_BusCmd_Datum(g_handle, Convert.ToUInt32(nAxis), Convert.ToUInt32(textBox1.Text));
                if (ret != 0)
                {
                    MessageBox.Show("回零执行失败!");
                }
            }
            //zmcaux.ZAux_SetTraceFile(0, "预留");
        }

        //停止
        private void button_stop_Click(object sender, EventArgs e)
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

        //位置清零
        private void button_zero_Click(object sender, EventArgs e)
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

        //定时器扫描
        private void timer1_Tick(object sender, EventArgs e)
        {
            int[] runstate = new int[4];
            float[] curpos = new float[4];

            for (int i = 0; i < 4; i++)
            {
                zmcaux.ZAux_Direct_GetIfIdle(g_handle, i, ref runstate[i]);
                zmcaux.ZAux_Direct_GetDpos(g_handle, i, ref curpos[i]);
            }

            label_X.Text = "X " + Convert.ToString(runstate[0] == 0 ? " 运行中 " : " 停止中 ") + curpos[0];
            label_Y.Text = "Y " + Convert.ToString(runstate[1] == 0 ? " 运行中 " : " 停止中 ") + curpos[1];
            label_Z.Text = "Z " + Convert.ToString(runstate[2] == 0 ? " 运行中 " : " 停止中 ") + curpos[2];
            label_R.Text = "R " + Convert.ToString(runstate[3] == 0 ? " 运行中 " : " 停止中 ") + curpos[3];
        }

        //X轴
        private void radioButton_X_CheckedChanged(object sender, EventArgs e)
        {
            nAxis = 0;
            TextBox_homeio.Text = Convert.ToString(0);
            TextBox_fwdio.Text = Convert.ToString(3);
            TextBox_revio.Text = Convert.ToString(6);
        }
        //Y轴
        private void radioButton_Y_CheckedChanged(object sender, EventArgs e)
        {
            nAxis = 1;
            TextBox_homeio.Text = Convert.ToString(1);
            TextBox_fwdio.Text = Convert.ToString(4);
            TextBox_revio.Text = Convert.ToString(7);
        }
        //Z轴
        private void radioButton_Z_CheckedChanged(object sender, EventArgs e)
        {
            nAxis = 2;
            TextBox_homeio.Text = Convert.ToString(2);
            TextBox_fwdio.Text = Convert.ToString(5);
            TextBox_revio.Text = Convert.ToString(8);
        }
        //R轴
        private void radioButton_R_CheckedChanged(object sender, EventArgs e)
        {
            nAxis = 3;
            TextBox_homeio.Text = Convert.ToString(9);
            TextBox_fwdio.Text = Convert.ToString(-1);
            TextBox_revio.Text = Convert.ToString(-1);
        }

        //模式1
        private void radioButton_mode1_CheckedChanged(object sender, EventArgs e)
        {
            home_mode = 1;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            home_mode = 2;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            home_mode = 3;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            home_mode = 4;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            home_mode = 8;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            home_mode = 9;
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            label8.Visible = false;
            textBox1.Visible = false;
            label14.Visible = false;

            groupBox7.Visible = true;
            label13.Visible = true;
            radioButton_mode1.Visible = true;
            radioButton1.Visible = true;
            radioButton2.Visible = true;
            radioButton3.Visible = true;
            radioButton4.Visible = true;
            radioButton5.Visible = true;
            radioButton10.Visible = true;
            radioButton11.Visible = true;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            label8.Visible = true;
            textBox1.Visible = true;
            label14.Visible = true;

            groupBox7.Visible = false;
            label13.Visible = false;
            radioButton_mode1.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            radioButton5.Visible = false;
            radioButton10.Visible = false;
            radioButton11.Visible = false;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            home_mode = 5;
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            home_mode = 6;
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
