using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;


using cszmcaux;

namespace test_faction5
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
        public int corner_mode = 0;         //拐角模式

        public Form1()
        {
            InitializeComponent();
            checkBox1.CheckedChanged += CheckBox_CheckedChanged;
            checkBox2.CheckedChanged += CheckBox_CheckedChanged;
            checkBox3.CheckedChanged += CheckBox_CheckedChanged;
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // 每次状态变化时重新计算总和
            corner_mode = 0;
            if (checkBox1.Checked) corner_mode += 2;  // 自动拐角减速对应10
            if (checkBox2.Checked) corner_mode += 8;  // 自动小圆限速对应20
            if (checkBox3.Checked) corner_mode += 32;  // 自动倒角对应30

            // 这里可以把 totalParam 赋值给你的业务变量
            // 例如：plc.Write("D0", totalParam);
            Console.WriteLine("当前共享参数值：" + corner_mode);
        }

        //启动
        private void button_start_Click(object sender, EventArgs e)
        {
            ThreadStart threadStart = new ThreadStart(run_move);
            Thread thread = new Thread(threadStart);

            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                thread.Start();
            }

        }

        //停止
        private void button_stop_Click(object sender, EventArgs e)
        {
            ThreadStart threadStart = new ThreadStart(run_move);
            Thread thread = new Thread(threadStart);

            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                thread.Abort();
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, 0, 2);        //取消主轴运动
            }
        }

        //坐标清零
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

        //关闭窗口
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

            int runstate = 0;
            float[] curpos = new float[4];
            float vspeed = 0;
            int remin_buff = 0;
            int curmark = 0;

            for (int i = 0; i < 4; i++)
            {

                zmcaux.ZAux_Direct_GetDpos(g_handle, i, ref curpos[i]);
            }
            zmcaux.ZAux_Direct_GetIfIdle(g_handle, 0, ref runstate);
            zmcaux.ZAux_Direct_GetVpSpeed(g_handle, 0, ref vspeed);
            //判断存放直线的剩余缓冲 ，ZAux_Direct_GetRemain_Buffer判断的空间圆弧的缓冲，也是占缓冲最大的运动
            zmcaux.ZAux_Direct_GetRemain_LineBuffer(g_handle, 0, ref remin_buff);
            //判断当前运动到第几条运动，
            zmcaux.ZAux_Direct_GetMoveCurmark(g_handle, 0, ref curmark);

            label_pos.Text = "X:" + curpos[0] + "  Y:" + curpos[1] + "  Z:" + curpos[2] + "  U:" + curpos[3];
            label_state.Text = Convert.ToString(runstate == 0 ? "   运行状态：运行中" : "    运行状态：停止中");
            label_vspeed.Text = "当前速度：" + vspeed;
            label_buff.Text = "剩余缓冲：" + remin_buff;
            label_mark.Text = "当前MARK：" + curmark;
        }

        //发送运动
        public void run_move()
        {
            int[] axislist = { 0, 1, 2, 3 };     //轴列表
            int iresult = 0;
            //int iremain = 0;
            float[] destdis = { 0, 0, 0, 0, 100 };

            zmcaux.ZAux_Direct_SetSpeed(g_handle, axislist[0], Convert.ToSingle(textBox_sp.Text)); //设置插补速度
            zmcaux.ZAux_Direct_SetAccel(g_handle, axislist[0], Convert.ToSingle(textBox_acc.Text)); //设置插补加速度
            zmcaux.ZAux_Direct_SetDecel(g_handle, axislist[0], Convert.ToSingle(textBox_dec.Text)); //设置插补减速度

            zmcaux.ZAux_Direct_SetMerge(g_handle, axislist[0], 1); //打开连续插补
            zmcaux.ZAux_Direct_SetLspeed(g_handle, axislist[0], Convert.ToSingle(textBox_lsp.Text)); ////起始速度 ，拐角减速由 运动速度-起始速度 线性减速的
            zmcaux.ZAux_Direct_SetCornerMode(g_handle, axislist[0], corner_mode);//拐角模式
            zmcaux.ZAux_Direct_SetDecelAngle(g_handle, axislist[0], (float)(Convert.ToSingle(textBox_ang1.Text) * 3.14 / 180));//开始减速角度，转换为弧度
            zmcaux.ZAux_Direct_SetStopAngle(g_handle, axislist[0], (float)(Convert.ToSingle(textBox_ang2.Text) * 3.14 / 180)); //停止减速角度，转换为弧度
            zmcaux.ZAux_Direct_SetFullSpRadius(g_handle, axislist[0], Convert.ToSingle(textBox_radio.Text)); //小圆半径
            zmcaux.ZAux_Direct_SetZsmooth(g_handle, axislist[0], Convert.ToSingle(textBox_zsmooth.Text)); //倒角

            zmcaux.ZAux_Direct_SetMovemark(g_handle, axislist[0], 0);	//设置MARK = 0 ，来通过读取CURMARK实现判断当前执行到那里

            zmcaux.ZAux_Direct_Base(g_handle, 4, axislist); //base轴

            for (int i = 0; i < 200; i++)
            {
                zmcaux.ZAux_Direct_SetForceSpeed(g_handle, 0, destdis[4]);    //设置每段的速度
                iresult = zmcaux.ZAux_Direct_MoveAbsSp(g_handle, 4, axislist, destdis);
                while (iresult != 0)            //函数返回非0 则表示发送不成功，缓冲区可能满了，重新发送
                {
                    zmcaux.ZAux_Direct_SetForceSpeed(g_handle, 0, destdis[4]);    //设置每段的速度
                    iresult = zmcaux.ZAux_Direct_MoveAbsSp(g_handle, 4, axislist, destdis);
                }

                destdis[0] = destdis[0] + 20;
                destdis[1] = destdis[1] + 20;
                destdis[2] = destdis[2] + 20;
                destdis[3] = destdis[3] + 20;
                destdis[4] = 100 + (i % 10) * 10;
            }

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

        private void OnSelect(object sender, EventArgs e)
        {
            C_Ip_Address.SelectedIndex = -1;
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
