using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using cszmcaux;

namespace test_faction6
{
    public partial class Form1 : Form
    {
        //用户可以修改这里进行对应的配置
        public const int DEFAULT_HAND_AXIS = 6;     //控制器默认手轮轴

        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
        public bool link_state = false; //状态
        public int hand_axis = DEFAULT_HAND_AXIS;       //手轮轴
        public float link_rate = 1;    //倍率
        public int iaxis = 0;           //当前轴号（X,Y,X）
        public int htype = 1;           //手轮类型  1-AB相手轮     2-脉冲+方向手轮
        public int[] axis_in = new int[] { 41, 42, 43 };        //默认的轴选输入口，用来添加输入口选择轴号的功能
        public int[] rate_in = new int[] { 38, 39, 40 };        //默认的倍率选择输入口，用来添加输入口选择倍率的功能
        public int H_stop = 47;                                 //紧急停止按钮对应输入口，用来添加紧急停止功能

        public Form1()
        {
            InitializeComponent();

            radioButton_rate_x1.CheckedChanged += radioButton_rate_CheckedChanged;
            radioButton_rate_x10.CheckedChanged += radioButton_rate_CheckedChanged;
            radioButton_rate_x100.CheckedChanged += radioButton_rate_CheckedChanged;

            radioButton_Axis_X.CheckedChanged += radioButton_axis_CheckedChanged;
            radioButton_Axis_Y.CheckedChanged += radioButton_axis_CheckedChanged;
            radioButton_Axis_Z.CheckedChanged += radioButton_axis_CheckedChanged;

            radioButton_Handweel_type1.CheckedChanged += radioButton_type_ChekedChanged;
            radioButton_Handweel_type2.CheckedChanged += radioButton_type_ChekedChanged;

            textBox_HandMove_axis.KeyPress += textBox_KeyPress;
            textBox_IN_X.KeyPress += textBox_KeyPress;
            textBox_IN_Y.KeyPress += textBox_KeyPress;
            textBox_IN_Z.KeyPress += textBox_KeyPress;
            textBox_IN_rate1.KeyPress += textBox_KeyPress;
            textBox_IN_rate10.KeyPress += textBox_KeyPress;
            textBox_IN_rate100.KeyPress += textBox_KeyPress;

            Thread scan_IN_thread = new Thread(ScanInTask);
            scan_IN_thread.IsBackground = true;
            scan_IN_thread.Start();
        }


        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //只允许输入数字、退格键和负号
            if(!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-')
            {
                //如果输入的不是合法字符，取消输入
                e.Handled = true;
            }
            //确保负号只能在开头输入
            if(e.KeyChar == '-' && ((TextBox)sender).Text.Length > 0)
            {
                e.Handled = true;
            }
        }


        private void ScanInTask()       //线程扫描输入口
        {
            int[] base_axis = { hand_axis };
            uint[] state_axis_in = new uint[] { 0, 0, 0 };      //存放轴选择对应输入口的状态，方便扫描
            uint[] state_rate_in = new uint[] { 0, 0, 0 };      //存放倍率选择对应输入口状态，方便扫描
            int[] flag_axis_in = new int[] { 0, 0, 0 };         //不能直接获取上升沿状态，这里用标志模拟上升沿
            int[] flag_rate_in = new int[] { 0, 0, 0 };         //同上，倍率选择对应输入口
            uint state_stop_in = 0;                             //存放紧急停止输入口状态
            int flag_stop_in = 0;                               //紧急停止输入口模拟上升沿
            while (true)
            {
                zmcaux.ZAux_Direct_Base(g_handle, 1, base_axis);
                //确定倍率
                for (int i = 0; i < 3; ++i)
                {
                    //排除控制器没有的输入口，防止bug，可以删掉
                    if (rate_in[i] < 0 || (rate_in[i] > 30 && rate_in[i] < 38) || rate_in[i] > 43)
                        break;
                    /*******************************************************************************
                     * 获取输入口状态，axis_in数组分别装了X1, X10, X100倍率对应的输入口，
                     * 这里通过扫描输入口状态变化，从而选择对应倍率，但这里不设置到控制器，需点击手轮链接按钮
                     *******************************************************************************/
                    zmcaux.ZAux_Direct_GetIn(g_handle, rate_in[i], ref state_rate_in[i]);
                    if (state_rate_in[i] == 1 && flag_rate_in[i] == 0)
                    {
                        flag_rate_in[i] = 1;
                        //判断哪一个输入发生变化，将对应的倍率赋值给link_rate，当点击手轮链接后生效
                        link_rate = (i == 0) ? 1 : ((i == 1) ? 10 : 100);
                        //拨动手轮拨钮后，实时更新软件的显示状态
                        if(link_rate == 1)
                        {
                            radioButton_rate_x1.BeginInvoke(new Action(() =>
                            {
                                radioButton_rate_x1.Checked = true;
                            }));
                            radioButton_rate_x1.BeginInvoke(new Action(() =>
                            {
                                radioButton_rate_x10.Checked = false;
                            }));
                            radioButton_rate_x1.BeginInvoke(new Action(() =>
                            {
                                radioButton_rate_x100.Checked = false;
                            }));
                        }
                        else if(link_rate == 10)
                        {
                            radioButton_rate_x1.BeginInvoke(new Action(() =>
                            {
                                radioButton_rate_x1.Checked = false;
                            }));
                            radioButton_rate_x1.BeginInvoke(new Action(() =>
                            {
                                radioButton_rate_x10.Checked = true;
                            }));
                            radioButton_rate_x1.BeginInvoke(new Action(() =>
                            {
                                radioButton_rate_x100.Checked = false;
                            }));
                        }
                        else
                        {
                            radioButton_rate_x1.BeginInvoke(new Action(() =>
                            {
                                radioButton_rate_x1.Checked = false;
                            }));
                            radioButton_rate_x1.BeginInvoke(new Action(() =>
                            {
                                radioButton_rate_x10.Checked = false;
                            }));
                            radioButton_rate_x1.BeginInvoke(new Action(() =>
                            {
                                radioButton_rate_x100.Checked = true;
                            }));
                        }
                        break;
                    }
                    else if (state_rate_in[i] == 0)
                    {
                        flag_rate_in[i] = 0;
                    }
                }
                //确定连接轴，但是这里不负责连接
                for(int i = 0; i < 3; ++i)
                {
                    //排除控制器没有的输入口，防止bug，可以删掉
                    if (axis_in[i] < 0 || (axis_in[i] > 30 && axis_in[i] < 38) || axis_in[i] > 43)
                        break;

                    /*******************************************************************************
                     * 获取输入口状态，axis_in数组分别装了X, Y, Z对应的输入口，
                     * 这里通过扫描输入口状态变化，从而选择对应轴作为被连接轴，但是这里不负责连接，需要点击手轮链接按钮
                     *******************************************************************************/
                    zmcaux.ZAux_Direct_GetIn(g_handle, axis_in[i], ref state_axis_in[i]);
                    if (state_axis_in[i] == 1 && flag_axis_in[i] == 0)      //模拟上升沿
                    {
                        flag_axis_in[i] = 1;

                        //取消轴的运动和连接
                        zmcaux.ZAux_Direct_Single_Cancel(g_handle, iaxis, 2);

                        //更新当前要连接的轴(X, Y, Z)
                        iaxis = i;

                        //更新软件上的单选选项，当手轮拨钮拨动时，软件上实时更新显示状态
                        if (iaxis == 0)
                        {
                            radioButton_Axis_X.BeginInvoke(new Action(() =>
                            {
                                radioButton_Axis_X.Checked = true;
                            }));
                            radioButton_Axis_Y.BeginInvoke(new Action(() =>
                            {
                                radioButton_Axis_Y.Checked = false;
                            }));
                            radioButton_Axis_Z.BeginInvoke(new Action(() =>
                            {
                                radioButton_Axis_Z.Checked = false;
                            }));
                            
                        }
                        else if (iaxis == 1)
                        {
                            radioButton_Axis_X.BeginInvoke(new Action(() =>
                            {
                                radioButton_Axis_X.Checked = false;
                            }));
                            radioButton_Axis_Y.BeginInvoke(new Action(() =>
                            {
                                radioButton_Axis_Y.Checked = true;
                            }));
                            radioButton_Axis_Z.BeginInvoke(new Action(() =>
                            {
                                radioButton_Axis_Z.Checked = false;
                            }));
                        }
                        else
                        {
                            radioButton_Axis_X.BeginInvoke(new Action(() =>
                            {
                                radioButton_Axis_X.Checked = false;
                            }));
                            radioButton_Axis_Y.BeginInvoke(new Action(() =>
                            {
                                radioButton_Axis_Y.Checked = false;
                            }));
                            radioButton_Axis_Z.BeginInvoke(new Action(() =>
                            {
                                radioButton_Axis_Z.Checked = true;
                            }));
                        }
                        break; 
                    }
                    else if (state_axis_in[i] == 0)
                    {
                        flag_axis_in[i] = 0;
                    }
                }
                //查看是否紧急停止
                //获取紧急停止输入口H_stop的状态，保存在state_stop_in中
                zmcaux.ZAux_Direct_GetIn(g_handle, H_stop, ref state_stop_in);
                if(state_stop_in == 1 && flag_stop_in == 0) //用state_stop_in和flag_stop_in模拟输入口上升沿状态变化
                {
                    flag_stop_in = 1;
                    //停止所有轴运动并取消所有轴连接（此指令不会取消重映射）
                    zmcaux.ZAux_Direct_Rapidstop(g_handle, 2);
                }
                else if(state_stop_in == 0)
                {
                    flag_stop_in = 0;
                }

            }
        }


        private void inspect_IsNullOrEmpty()           //查看各个文本框是否为空，避免文本框为空产生的bug
        {
            if (string.IsNullOrEmpty(textBox_HandMove_axis.Text))
            {
                hand_axis = DEFAULT_HAND_AXIS;
                textBox_HandMove_axis.Text = Convert.ToString(DEFAULT_HAND_AXIS);
            }
            else
            {
                hand_axis = Convert.ToInt32(textBox_HandMove_axis.Text);
            }

            int[] base_axis = { hand_axis };

            if (string.IsNullOrEmpty(textBox_IN_X.Text))
            {
                axis_in[0] = 41;
                textBox_IN_X.Text = "41";
            }
            else
            {
                axis_in[0] = Convert.ToInt32(textBox_IN_X.Text);
            }

            if (string.IsNullOrEmpty(textBox_IN_Y.Text))
            {
                axis_in[1] = 42;
                textBox_IN_Y.Text = "42";
            }
            else
            {
                axis_in[1] = Convert.ToInt32(textBox_IN_Y.Text);
            }

            if (string.IsNullOrEmpty(textBox_IN_Z.Text))
            {
                axis_in[2] = 43;
                textBox_IN_Z.Text = "43";
            }
            else
            {
                axis_in[2] = Convert.ToInt32(textBox_IN_Z.Text);
            }


            if (string.IsNullOrEmpty(textBox_IN_rate1.Text))
            {
                rate_in[0] = 38;
                textBox_IN_rate1.Text = "38";
            }
            else
            {
                rate_in[0] = Convert.ToInt32(textBox_IN_rate1.Text);
            }

            if (string.IsNullOrEmpty(textBox_IN_rate10.Text))
            {
                rate_in[1] = 39;
                textBox_IN_rate10.Text = "39";
            }
            else
            {
                rate_in[1] = Convert.ToInt32(textBox_IN_rate10.Text);
            }

            if (string.IsNullOrEmpty(textBox_IN_rate100.Text))
            {
                rate_in[2] = 40;
                textBox_IN_rate100.Text = "40";
            }
            else
            {
                rate_in[2] = Convert.ToInt32(textBox_IN_rate100.Text);
            }

            if (string.IsNullOrEmpty(textBox_IN_stop.Text))
            {
                H_stop = 47;
                textBox_IN_stop.Text = "47";
            }
            else
            {
                H_stop = Convert.ToInt32(textBox_IN_stop.Text);
            }
        }


        private void OnSelect(object sender, EventArgs e)       //连接方式控件可以进行选择
        {
            C_Ip_Address.SelectedIndex = -1;
        }

        private void OnScan(object sender, EventArgs e)         //确定连接方式并调用该连接方式下的函数，进行该连接方式的扫描
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

        private void IP_SCAN()                                  //IP连接方式
        {
            int i, num;
            string[] sArray;
            StringBuilder buffer = new StringBuilder(10240);
            string buff = "";
            //检索IP列表
            zmcaux.ZAux_SearchEthlist(buffer, 10230, 200);
            buff += buffer;
            sArray = buff.Split(' ');
            num = buff.Split(' ').Length;

            for (i = 0; i < num - 1; i++)
            {
                C_Ip_Address.Items.Insert(i, sArray[i].ToString());
            }
        }

        private void Com_SCAN()                                 //串口连接方式
        {
            string[] ports = SerialPort.GetPortNames();

            foreach (string s in ports)
            {
                C_Ip_Address.Items.Add(s);
            }
        }

        private void PCI_SCAN()                                 //PCI控制卡连接方式
        {
            string tmpstr;
            int Card;
            //获取PCI控制卡的个数
            Card = zmcaux.ZAux_GetMaxPciCards();

            for (int i = 0; i < Card; i++)
            {
                tmpstr = string.Format("PCI{0:D}", i + 1);
                C_Ip_Address.Items.Insert(i, tmpstr);
            }
        }

        private void OnOpen(object sender, EventArgs e)         //根据连接方式下的数据进行连接
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
            //与控制器建立链接, 可以指定连接的等待时间，可以选择连接方式
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

                for (int i = 0; i < 3; i++)     //初始化轴参数
                {
                    zmcaux.ZAux_Direct_SetAtype(g_handle, i, 1);        //轴类型 脉冲轴
                    zmcaux.ZAux_Direct_SetUnits(g_handle, i, 1);          //脉冲当量 一 脉冲为单位
                    zmcaux.ZAux_Direct_SetSpeed(g_handle, i, 100);				//速度	UNITS / S
                    zmcaux.ZAux_Direct_SetAccel(g_handle, i, 1000);				//加速度
                    zmcaux.ZAux_Direct_SetDecel(g_handle, i, 1000);				//减速度
                }
            }
        }

        private void OnClose(object sender, EventArgs e)        //断开控制器连接
        {
            //断开链接
            timer1.Enabled = false;
            zmcaux.ZAux_Close(g_handle);
            g_handle = (IntPtr)0;
            this.Text = "未连接";
        }

        private void timer1_Tick(object sender, EventArgs e)    //定时器获取X, Y, Z, 编码器轴的位置状态
        {
            float[] curpos = new float[4];

            //获取X(0轴), Y(1轴), Z(2轴)的目标位置数据
            for (int i = 0; i < 3; i++)
            {
                zmcaux.ZAux_Direct_GetDpos(g_handle, i, ref curpos[i]);
            }
            //获取编码器轴的反馈位置数据，hand_axis为手轮轴，即编码器轴
            zmcaux.ZAux_Direct_GetMpos(g_handle, hand_axis, ref curpos[3]);

            label_x.Text = "X轴:" + curpos[0];
            label_y.Text = "Y轴:" + curpos[1];
            label_z.Text = "Z轴:" + curpos[2];
            label_e.Text = "编码轴:" + curpos[3];

            label_state.Text = link_state ? "手轮链接状态：已链接" : "手轮链接状态：未链接";
        }


        private void radioButton_axis_CheckedChanged(object sender, EventArgs e)        //确定轴号并取消原先轴的连接
        {
            /* ZAux_Direct_Single_Cancel库函数主要由于取消运动，这里用于取消轴的连接 */
            if (radioButton_Axis_X.Checked)
            {
                if(g_handle != (IntPtr)0)
                    zmcaux.ZAux_Direct_Single_Cancel(g_handle, iaxis, 2);
                iaxis = 0;
            }
            else if(radioButton_Axis_Y.Checked)
            {
                if (g_handle != (IntPtr)0)
                    zmcaux.ZAux_Direct_Single_Cancel(g_handle, iaxis, 2);
                iaxis = 1;
            }
            else
            {
                if (g_handle != (IntPtr)0)
                    zmcaux.ZAux_Direct_Single_Cancel(g_handle, iaxis, 2);
                iaxis = 2;
            }

            //if(g_handle == (IntPtr)0)
            //{
            //    return;
            //}
        }


        private void radioButton_rate_CheckedChanged(object sender, EventArgs e)        //确定手轮倍率
        {
            if (radioButton_rate_x1.Checked)
            {
                link_rate = 1;
            }
            else if (radioButton_rate_x10.Checked)
            {
                link_rate = 10;
            }
            else
            {
                link_rate = 100;
            }

            //if (g_handle == (IntPtr)0)
            //{
            //    return;
            //}
        }

        
        private void radioButton_type_ChekedChanged(object sender, EventArgs e)         //确定手轮类型
        {
            if(radioButton_Handweel_type1.Checked)
            {
                htype = 1;
            }
            else
            {
                htype = 2;
            }
        }

        private void button_stop_click(object sender, EventArgs e)                      //取消轴运动并断开手轮连接
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, iaxis, 2);        //取消轴运动和手轮连接
                link_state = false;
            }
        }

        private void button_HandMove_connect_Click(object sender, EventArgs e)          //手轮连接
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                //取消重映射，把手轮轴映射到其他轴的功能取消，再操控手轮的话就等于操控原始默认手轮轴（控制器有手轮轴接口的情况下）
                zmcaux.ZAux_Direct_SetAtype(g_handle, hand_axis, 0);
                zmcaux.ZAux_Direct_SetAxisAddress(g_handle, hand_axis, 0);

                //检查文本框是否为空，避免文本框为空导致的bug
                inspect_IsNullOrEmpty();

                //重映射轴号,将手轮轴的功能映射到其他轴上（映射成功后原本的轴就没有手轮轴的功能了），操控手轮等同于操控被映射的那个轴
                zmcaux.ZAux_Direct_SetAtype(g_handle, hand_axis, 0);
                zmcaux.ZAux_Direct_SetAtype(g_handle, DEFAULT_HAND_AXIS, 0);
                zmcaux.ZAux_Direct_SetAxisAddress(g_handle, hand_axis, (-1 << 16) + DEFAULT_HAND_AXIS);

                //根据手轮类型设置对应的轴类型,htype为手轮的类型,1-AB相手轮，2-脉冲+方向手轮
                zmcaux.ZAux_Direct_SetAtype(g_handle, hand_axis, (htype == 1) ? 3 : 6);

                //设置手轮轴（被映射的轴）脉冲当量为1
                zmcaux.ZAux_Direct_SetUnits(g_handle, hand_axis, 1);

                /**********************************************************************************************
                 * 将手轮轴和被连接轴进行连接，g_handle为句柄，link_rate为倍率，hand_axis为手轮轴，iaxis（X, Y, Z）为被连接轴
                 * 连接成功后手轮轴与被连接轴就会将手轮轴和被连接轴的脉冲个数以  1：link_rate  的比例连接起来
                 **********************************************************************************************/
                zmcaux.ZAux_Direct_Connect(g_handle, link_rate, hand_axis, iaxis);

                Console.WriteLine(iaxis);
                link_state = true;
            }
        }

        private void btn_rapidstop(object sender, EventArgs e)                          //紧急停止按钮
        {
            if(g_handle == (IntPtr)0)
            {
                return;
            }

            /*
             * 停止所有轴的运动，g_handle为句柄，2为停止模式
             * 停止模式  0（缺省）取消当前运动 1-取消缓冲的运动 2-取消当前运动和缓冲运动 3-立即中断脉冲发送
             */
            zmcaux.ZAux_Direct_Rapidstop(g_handle, 2);
        }

        private void all_clear(object sender, EventArgs e)                              //位置清除按钮
        {
            if(g_handle == ( IntPtr)0)
            {
                return;
            }

            zmcaux.ZAux_Direct_SetDpos(g_handle, 0, 0);         //设置X轴(0轴)的目标位置（DPOS）为0，会自动将X轴的反馈位置（MPOS）更新为 0 
            zmcaux.ZAux_Direct_SetDpos(g_handle, 1, 0);         //设置Y轴(1轴)的目标位置为 0 ，会自动将Y轴的反馈位置（MPOS）更新为 0 
            zmcaux.ZAux_Direct_SetDpos(g_handle, 2, 0);         //设置Z轴(2轴)的目标位置为 0 ，会自动将Z轴的反馈位置（MPOS）更新为 0 
            zmcaux.ZAux_Direct_SetDpos(g_handle, hand_axis, 0); ////设置编码器轴轴(hand_axis轴)的目标位置为 0 ，会自动将编码器轴的反馈位置（MPOS）更新为 0 
        }

        private void resume_default(object sender, EventArgs e)                         //恢复默认按钮
        {
            for(int i = 0; i < 3; i++)
            {
                axis_in[i] = i + 41;
                rate_in[i] = i + 38;
                
            }
            H_stop = 47;
            textBox_IN_X.Text = axis_in[0].ToString();
            textBox_IN_Y.Text = axis_in[1].ToString();
            textBox_IN_Z.Text = axis_in[2].ToString();
            textBox_IN_rate1.Text = rate_in[0].ToString();
            textBox_IN_rate10.Text = rate_in[1].ToString();
            textBox_IN_rate100.Text = rate_in[2].ToString();
            radioButton_Axis_X.Checked = true;
            radioButton_rate_x1.Checked = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
