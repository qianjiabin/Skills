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

namespace test_faction4
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
        public int run_mode = 1;         //运动方式 相当OR绝对
        public int move_mode = 1;         //运行模式

        public Form1()
        {
            InitializeComponent();
        }

        //关闭窗口
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //断开链接
            timer1.Enabled = false;
            zmcaux.ZAux_Close(g_handle);
            g_handle = (IntPtr)0;
        }

        //启动
        private void Button_start_Click(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                int[] axislist  = {0,1,2,3};
                float[] poslist = { Convert.ToSingle(destpos1.Text), Convert.ToSingle(destpos2.Text), Convert.ToSingle(destpos3.Text), Convert.ToSingle(destpos4.Text)};
                float[] midlist = { Convert.ToSingle(midpos1.Text), Convert.ToSingle(midpos2.Text), Convert.ToSingle(midpos3.Text), Convert.ToSingle(midpos4.Text) };
                float[] endmove = { 0,0,0,0 };              //相当绝对转换

                zmcaux.ZAux_Direct_Base(g_handle, 4, axislist); //选择运动轴列表

                //插补运动使用的是主轴参数，及BASE的第一个轴
                zmcaux.ZAux_Direct_SetSpeed(g_handle, axislist[0],Convert.ToSingle(textBox_speed.Text));
                zmcaux.ZAux_Direct_SetAccel(g_handle, axislist[0], Convert.ToSingle(textBox_acc.Text));
                zmcaux.ZAux_Direct_SetDecel(g_handle, axislist[0], Convert.ToSingle(textBox_dec.Text));

                if (run_mode == 1)          //绝对
                {
                    switch (move_mode)
                    { 
                        case 1:     //XY直线
                            zmcaux.ZAux_Direct_MoveAbs(g_handle, 2, axislist, poslist);
                            break;
                        case 2:     //XY圆弧
                            zmcaux.ZAux_Direct_MoveCirc2Abs(g_handle,2,axislist, midlist[0], midlist[1],poslist[0], poslist[1]);
                            break;
                        case 3:     //XYZU直线
                            zmcaux.ZAux_Direct_MoveAbs(g_handle, 4,axislist, poslist);
                            break;
                        case 4:      //空间圆弧 	XYZ空间圆弧插补U螺旋 没有绝对指令，需要用ENDMOVE_BUFF转相对
                            for (int i = 0; i < 4; i++)
                            {
                                zmcaux.ZAux_Direct_GetEndMoveBuffer(g_handle, i, ref endmove[i]);	//计算相对位移
                                poslist[i] = poslist[i] - endmove[i];
                                midlist[i] = midlist[i] - endmove[i];
                            }

                            zmcaux.ZAux_Direct_MSpherical(g_handle, 4,axislist, poslist[0], poslist[1], poslist[2], midlist[0], midlist[1], midlist[2], 0, poslist[3], 0);
                            break;
                        default:
                            break;
                    }

                }
                else
                {
                    switch (move_mode)
                    {
                        case 1:
                            zmcaux.ZAux_Direct_Move(g_handle, 2, axislist, poslist);
                            break;
                        case 2:
                            zmcaux.ZAux_Direct_MoveCirc2(g_handle, 2, axislist, midlist[0], midlist[1], poslist[0], poslist[1]);
                            break;
                        case 3:
                            zmcaux.ZAux_Direct_Move(g_handle, 4, axislist, poslist);
                            break;
                        case 4:
                            zmcaux.ZAux_Direct_MSpherical(g_handle, 4, axislist, poslist[0], poslist[1], poslist[2], midlist[0], midlist[1], midlist[2], 0, poslist[3], 0);	
                            break;
                        default:
                            break;
                    }        
                }
            }
        }

        //停止
        private void Button_stop_Click(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, 0, 2);        //取消主轴运动
            }
        }

        //清零坐标
        private void Button_zero_Click(object sender, EventArgs e)
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

        //定时器刷新
        private void timer1_Tick(object sender, EventArgs e)
        {
            int[] runstate = new int[4];
            float[] curpos = new float[4];

            for (int i = 0; i < 4; i++)
            {
                zmcaux.ZAux_Direct_GetIfIdle(g_handle, i, ref runstate[i]);
                zmcaux.ZAux_Direct_GetDpos(g_handle, i, ref curpos[i]);
            }

            label_runstate.Text = "x:" + curpos[0] + "  y:" + curpos[1] + "  z:" + curpos[2] + "  u:" + curpos[3] + Convert.ToString(runstate[0] == 0 ? "   运行状态：运行" : "    运行状态：停止");

        }

        //绝对
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            run_mode = 1;
        }

        //相对
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            run_mode = 2;
        }

        //模式1
        private void radioMode1_CheckedChanged(object sender, EventArgs e)
        {
            move_mode = 1;
        }

        private void radioMode2_CheckedChanged(object sender, EventArgs e)
        {
            move_mode = 2;
        }

        private void radioMode3_CheckedChanged(object sender, EventArgs e)
        {
            move_mode = 3;
        }

        private void radioMode4_CheckedChanged(object sender, EventArgs e)
        {
            move_mode = 4;
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

                for (int i = 0; i < 4; i++)     //初始化轴参数
                {
                    zmcaux.ZAux_Direct_SetAtype(g_handle, i, 1);        //轴类型 脉冲轴
                    zmcaux.ZAux_Direct_SetUnits(g_handle, i, 1);          //脉冲当量 一 脉冲为单位
                }
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
