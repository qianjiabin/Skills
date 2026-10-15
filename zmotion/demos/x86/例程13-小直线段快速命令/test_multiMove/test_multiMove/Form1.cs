using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cszmcaux;
using System.IO.Ports;

namespace test_multiMove
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
       private void C_connect_type_TextChanged(object sender, EventArgs e)
       {
            string connectType = C_connect_type.Text;
            cc = new ConnectContext(connectType);
       }
        public float m_speed = 100.0f;
        public float m_accel = 1000.0f;
        public float m_decel = 1000.0f;
        public int g_curseges = 0;
        public float[] destdis = new float[5000];

        public const int LEGS_MAX = 1000;
       



        public Form1()
        {
            InitializeComponent();
            C_connect_type.SelectedIndex = 0;
            timer3.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)            //定时刷新
        {
            update_value();
            show_value();

        }

        //连接函数
        private void OnSelect(object sender, EventArgs e)
        {
            C_Ip_Address.SelectedIndex = -1;
        }

        ConnectContext cc;
        private void OnScan(object sender, EventArgs e)
        {

            string connectType = C_connect_type.Text;
            C_Ip_Address.Items.Clear();
            try
            {
                cc = new ConnectContext(connectType);
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show(string.Format("{0}: {1}", ae.GetType().Name, ae.Message));
                return;
            }
            string[] scanResult = cc.ScanConnect();
            for (int i = 0; i < scanResult.Length; i++)
            {
                C_Ip_Address.Items.Insert(i, scanResult[i].ToString());
            }
        }

        private void OnOpen(object sender, EventArgs e)
        {
            string Buffer = C_Ip_Address.Text;

            try
            {
                controlReturn = cc.OpenConnect(ref g_handle, Buffer);

            }
            catch (NullReferenceException ne)
            {
                MessageBox.Show(string.Format("{0}: {1}", ne.GetType().Name, ne.Message));
                return;
            }

            ControllerInit();

        }

        private void OnClose(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
                return;

            cc.CloseConnect(g_handle);
            g_handle = (IntPtr)0;
        }
            
        private void update_value()             //更新参数
        { 
             m_speed = Convert.ToSingle(TextBox8.Text);
             m_accel = Convert.ToSingle(TextBox5.Text);
             m_decel = Convert.ToSingle(TextBox6.Text);
        }

        private void show_value()           //状态显示
        {   int i ;
            int iret = 0;
            string AxisPos;
            string AxisStatus;
            string AxisIdle;
            string AxisBuffer;

            float[] showpos = new float[4];
            float[] showstatus = new float[4];
            float[] showidle = new float[4];
            int[] showbuff = new int[4];
            string[] idleshow = new string[4];

            iret = zmcaux.ZAux_Direct_GetAllAxisPara(g_handle, "DPOS", 4, showpos);          //获取当前轴位置
            iret = zmcaux.ZAux_Direct_GetAllAxisPara(g_handle, "AXISSTATUS", 4, showstatus);          //获取当前轴状态
            iret = zmcaux.ZAux_Direct_GetAllAxisPara(g_handle, "IDLE", 4, showidle);         //获取当前轴运动状态
           
            iret = zmcaux.ZAux_Direct_GetRemain_LineBuffer(g_handle, 0, ref showbuff[0]);
            iret = zmcaux.ZAux_Direct_GetRemain_LineBuffer(g_handle, 1, ref showbuff[1]);
            iret = zmcaux.ZAux_Direct_GetRemain_LineBuffer(g_handle, 2, ref showbuff[2]);
            iret = zmcaux.ZAux_Direct_GetRemain_LineBuffer(g_handle, 3, ref showbuff[3]);

            for (i = 0; i < 4; i++)
            {
                if (showidle[i] == 0)
                    idleshow[i] = "运动中";
                else
                    idleshow[i] = "停止";
            }

            AxisPos = "轴坐标   X:" + showpos[0].ToString() + "     Y:" + showpos[1].ToString() + "     Z:" + showpos[2].ToString() + "     U:" + showpos[3].ToString();
            AxisStatus = "轴状态   X:" + showstatus[0].ToString() + "     Y:" + showstatus[1].ToString() + "     Z:" + showstatus[2].ToString() + "     U:" + showstatus[3].ToString();
            AxisIdle = "运动状态   X:" + idleshow[0] + "  Y:" + idleshow[1] + "  Z:" + idleshow[2] + "  U:" + idleshow[3];
            AxisBuffer = "剩余直线缓冲   X:" + showbuff[0].ToString() + "  Y:" + showbuff[1].ToString() + "  Z:" + showbuff[2].ToString() + "  U:" + showbuff[3].ToString();

            TextBox1.Text = AxisPos;
            TextBox2.Text = AxisStatus;
            TextBox3.Text = AxisIdle;
            TextBox4.Text = AxisBuffer;
        }

        private void Button1_Click(object sender, EventArgs e)              //启动
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            else
            {
                int corner_mode = 0;
                int[] axislist = { 0, 1, 2, 3 };
                //选择参与运动的轴，第一个轴为主轴，插补参数全用主轴参数	
                zmcaux.ZAux_Direct_SetSpeed(g_handle, axislist[0], m_speed);              //速度	UNITS / S
                zmcaux.ZAux_Direct_SetAccel(g_handle, axislist[0], m_accel);              //加速度
                zmcaux.ZAux_Direct_SetDecel(g_handle, axislist[0], m_decel);          //减速度UpdateData(true);//刷新参数

                zmcaux.ZAux_Direct_SetMerge(g_handle, axislist[0], 1);                        //连续插补开关
                zmcaux.ZAux_Direct_SetLspeed(g_handle, axislist[0], 0);               //起始速度 ，拐角减速由 运动速度-起始速度 线性减速的
                zmcaux.ZAux_Direct_SetCornerMode(g_handle, axislist[0], corner_mode);                   //拐角模式 0-开启
                zmcaux.ZAux_Direct_SetDecelAngle(g_handle, axislist[0], Convert.ToSingle(60 * Math.PI / 180)); //开始减速角度，转换为弧度	
                zmcaux.ZAux_Direct_SetStopAngle(g_handle, axislist[0], Convert.ToSingle(120 * Math.PI / 180));
                zmcaux.ZAux_Direct_SetFullSpRadius(g_handle, axislist[0], 5);
                zmcaux.ZAux_Direct_SetZsmooth(g_handle, axislist[0], 5);
                g_curseges = 0;
                timer2.Start();
            }
        }

        private void Button2_Click(object sender, EventArgs e)          //停止
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            timer2.Stop();
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, 0, 2);
        }

        private void Button3_Click(object sender, EventArgs e)              //坐标清零
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }

            for (int i = 0; i < 4; i++)
            {
                zmcaux.ZAux_Direct_SetDpos(g_handle, i, 0);
            }
        }

        private void Form1_Load(object sender, EventArgs e)         //初始化
        {
            
            for (int i = 0; i < 1000; i++)
            {
                destdis[i*5+0] = i * 1;
                destdis[i * 5 + 1] = i * 2;
                destdis[i * 5 + 2] = i * 3;
                destdis[i * 5 + 3] = i * 4;
                destdis[i * 5 + 4] = i * 5;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            unsafe
            {
                int[] axislist = { 0, 1, 2, 3 };            //运动BASE轴列表
                int iresult = 0;
                int iLen = 10;          //单次发送运动长度
              
                float[] tempdes = new float[5000 - 5*g_curseges + 1];
                for(int i = 5*g_curseges;i < 5000;i++)
                    tempdes[i-5*g_curseges] = destdis[i];
                if (LEGS_MAX < g_curseges)          //超过长度范围
                {
                    iLen = g_curseges - LEGS_MAX;

                }
                iresult = zmcaux.ZAux_Direct_MultiMoveAbs(g_handle, iLen, 4, axislist, tempdes);        //绝对
                //iresult = zmcaux.ZAux_Direct_MultiMove(g_handle, iLen, 4, axislist, tempdes);         //相对
                if (iresult != 0)               //运动发送失败，
                {
                    string tempstr;
                    tempstr = iresult.ToString();
                }
                else
                {
                    g_curseges += iLen;
                    if (g_curseges > LEGS_MAX)          //是否填完
                    {
                        timer2.Stop();
                        return;
                    }
                }
            }
        }

        CustomMessageBox customMessageBox = new CustomMessageBox();
        public int controlReturn = 0;
        private void timer3_Tick(object sender, EventArgs e)
        {

            StringBuilder[] str = new StringBuilder[4];
            str[0] = new StringBuilder("no reply");
            str[1] = new StringBuilder();
            str[2] = new StringBuilder();
            if (zmcaux.ZAux_GetControllerInfo(g_handle, str[0], str[1], str[2]) != 0 || str[0].ToString() == "no reply")
            {
                this.Text = "未连接";
                if (timer1.Enabled == true)
                    timer1.Enabled = false;
            }
            else
            {
                this.Text = "已连接";
                if (timer1.Enabled == false)
                    timer1.Enabled = true;
            }


            if (controlReturn != 0)
            {
                string message = string.Format("操作失败 错误码{0:D}", controlReturn);

                if (customMessageBox.messageBoxClosed == false)
                {
                    customMessageBox.SetDisplayText(message);
                    customMessageBox.ShowDialog();
                }
                controlReturn = 0;
            }
        }

        //初始化控制器
        private void ControllerInit()
        {

            for (int i = 0; i < 3; i++)
            {
                controlReturn = zmcaux.ZAux_Direct_SetAtype(g_handle, i, 1); //设置轴 0 轴 类型为 1
                controlReturn = zmcaux.ZAux_Direct_SetUnits(g_handle, i, 100); //设置轴 0 脉冲当量为 100
                controlReturn = zmcaux.ZAux_Direct_SetSpeed(g_handle, i, 200); //设置轴 0 速度为 200units/s
                controlReturn = zmcaux.ZAux_Direct_SetAccel(g_handle, i, 2000); //设置轴 0 加速度为 2000units/s/s
                controlReturn = zmcaux.ZAux_Direct_SetDecel(g_handle, i, 2000); //设置轴 0 减速度为 2000units/s/s
            }
        }

       
    }
}


public abstract class Connect
{
    public abstract string[] scan();
}


class LocalConect : Connect
{
    public override string[] scan()
    {
        string[] stringArray = new string[] { "LOCAL1" };
        return stringArray;
    }
}

class EthConnect : Connect
{
    public override string[] scan()
    {
        int num;
        string[] sArray;
        StringBuilder buffer = new StringBuilder(10240);
        string buff = "";
        zmcaux.ZAux_SearchEthlist(buffer, 10230, 200);
        buff += buffer;
        sArray = buff.Split(' ');
        num = buff.Split(' ').Length;
        sArray = sArray.Take(num - 1).ToArray();
        return sArray;
    }
}

class ComConnect : Connect
{
    public override string[] scan()
    {
        return SerialPort.GetPortNames();
    }
}

class PciConnect : Connect
{
    public override string[] scan()
    {
        int Card;
        Card = zmcaux.ZAux_GetMaxPciCards();
        string[] tmpstr = new string[Card];

        for (int i = 0; i < Card; i++)
        {
            tmpstr[i] = string.Format("PCI{0:D}", i + 1);
        }
        return tmpstr;
    }
}

public class ConnectContext
{
    private Connect cn = null;
    private int ctype = -1;


    public ConnectContext(string type)
    {
        switch (type)
        {
            case "网口":
                cn = new EthConnect();
                ctype = 2;
                break;
            case "串口":
                cn = new ComConnect();
                ctype = 1;
                break;
            case "PCI":
                cn = new PciConnect();
                ctype = 4;
                break;
            case "LOCAL":
                cn = new LocalConect();
                ctype = 5;
                break;
            default:
                throw new ArgumentException("Incorrect type", "type");
        }
    }

    public string[] ScanConnect()
    {
        return cn.scan();
    }

    public int OpenConnect(ref IntPtr g_handle, string Buffer)
    {
        if (g_handle != (IntPtr)0)
        {
            CloseConnect(g_handle);
        }
        return zmcaux.ZAux_FastOpen(ctype, Buffer, 1000, out g_handle);
    }

    public int CloseConnect(IntPtr g_handle)
    {
        return zmcaux.ZAux_Close(g_handle);
    }
}

class CustomMessageBox : Form
{
    public bool messageBoxClosed = false;
    public string displayText;
    Label label = new Label();
    Button button = new Button();

    public CustomMessageBox()
    {
        this.Size = new System.Drawing.Size(180, 150);
        this.StartPosition = FormStartPosition.CenterParent;
        this.FormClosing += CustomMessageBox_FormClosing;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;

        label.AutoSize = true;
        label.Location = new System.Drawing.Point((this.ClientSize.Width - label.Width) / 2, (this.ClientSize.Height - label.Height) / 2);
        this.Controls.Add(label);


        button.Text = "确定";
        button.Location = new System.Drawing.Point((this.ClientSize.Width - button.Width) / 2, label.Location.Y + label.Height + 20);
        button.Click += (sender, e) =>
        {
            this.Close();
        };
        this.Controls.Add(button);
    }

    private void CustomMessageBox_FormClosing(object sender, FormClosingEventArgs e)
    {
        messageBoxClosed = false;
    }

    public void SetDisplayText(string text)
    {
        displayText = text;
    }

    public new DialogResult ShowDialog()
    {
        messageBoxClosed = true;
        label.Text = displayText;
        return base.ShowDialog();
    }
}
