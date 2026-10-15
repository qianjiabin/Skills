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
namespace Robot_5
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
       private void C_connect_type_TextChanged(object sender, EventArgs e)
       {
            string connectType = C_connect_type.Text;
            cc = new ConnectContext(connectType);
       }
        public float[] Robot_para = new float[4];
        public float[] Axis_Speed = new float[12];
        public float[] Axis_Acc = new float[12];
        public int Robot_status = 0;
        public int[] button_status = new int[12];

        public Form1()
        {
            InitializeComponent();
            C_connect_type.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)             //初始化
        {   int i ;
            for (i = 0; i < 12; i++)
            {
                button_status[i] = 0;
            }
            Robot_para[0] = 10.0f;
            Robot_para[1] = 10000.0f;
            Robot_para[2] = 10000.0f;
            Robot_para[3] = 10000.0f;
            for (i = 0; i < 12; i++)
            {
                Axis_Speed[i] = 100;
                Axis_Acc[i] = 1000;
            }
            timer2.Enabled = true;

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

        private void timer1_Tick(object sender, EventArgs e)                //定时刷新
        {
            update_value();
            int iret = 0;
            float[] fDpos = new float[12];
            int[] iAxisMtype = new int[2];
            string tempstr;
            iret = zmcaux.ZAux_Direct_GetAllAxisPara(g_handle, "dpos", 12, fDpos);
            iret = zmcaux.ZAux_Direct_GetMtype(g_handle, 0, ref iAxisMtype[0]);
            iret = zmcaux.ZAux_Direct_GetMtype(g_handle, 6, ref iAxisMtype[1]);

            TextBox1.Text = "J0:" + fDpos[0].ToString() + "  J1:" + fDpos[1].ToString() + "  J2:" + fDpos[2].ToString();
            TextBox2.Text = "X:" + fDpos[6].ToString() + "  Y:" + fDpos[7].ToString() + "  R:" + fDpos[8].ToString();

            if (iAxisMtype[0] == 33)
            { 
                tempstr = "逆解状态";
                Robot_status = 1;
                b0.Enabled = false;
                b1.Enabled = false;
                b3.Enabled = false;
                b2.Enabled = false;
                b5.Enabled = false;
                b4.Enabled = false;
                b11.Enabled = true;
                b10.Enabled = true;
                b9.Enabled = true;
                b8.Enabled = true;
                b7.Enabled = true;
                b6.Enabled = true;
            }
            else if (iAxisMtype[1] == 34)
            {
                tempstr = "正解状态";
                Robot_status = 2;
                b0.Enabled = true;
                b1.Enabled = true;
                b3.Enabled = true;
                b2.Enabled = true;
                b5.Enabled = true;
                b4.Enabled = true;
                b11.Enabled = false;
                b10.Enabled = false;
                b9.Enabled = false;
                b8.Enabled = false;
                b7.Enabled = false;
                b6.Enabled = false;
            }
            else
            { 
                tempstr = "未映射";
                Robot_status = 0;
                b0.Enabled = true;
                b1.Enabled = true;
                b3.Enabled = true;
                b2.Enabled = true;
                b5.Enabled = true;
                b4.Enabled = true;
                b11.Enabled = true;
                b10.Enabled = true;
                b9.Enabled = true;
                b8.Enabled = true;
                b7.Enabled = true;
                b6.Enabled = true;
            }
            TextBox3.Text = tempstr;
        }

        private void update_value()             //更新参数
        { 
            Robot_para[0] = Convert.ToSingle(TextBox4.Text);
            Robot_para[1] = Convert.ToSingle(TextBox5.Text);
            Robot_para[2] = Convert.ToSingle(TextBox6.Text);
            Robot_para[3] = Convert.ToSingle(TextBox7.Text);
            Axis_Speed[0] = Convert.ToSingle(TextBox8.Text);
            Axis_Acc[0] = Convert.ToSingle(TextBox19.Text);
            Axis_Speed[1] = Convert.ToSingle(TextBox9.Text);
            Axis_Acc[1] = Convert.ToSingle(TextBox18.Text);
            Axis_Speed[2] = Convert.ToSingle(TextBox10.Text);
            Axis_Acc[2] = Convert.ToSingle(TextBox17.Text);
            Axis_Speed[6] = Convert.ToSingle(TextBox11.Text);
            Axis_Acc[6] = Convert.ToSingle(TextBox16.Text);
            Axis_Speed[7] = Convert.ToSingle(TextBox12.Text);
            Axis_Acc[7] = Convert.ToSingle(TextBox15.Text);
            Axis_Speed[8] = Convert.ToSingle(TextBox13.Text);
            Axis_Acc[8] = Convert.ToSingle(TextBox14.Text);
        }

        private void Button1_Click(object sender, EventArgs e)          //配置修改机械手参数
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int i;
            int iret = 0;
            int iaxis = 0;
            int[] jAxis_Units = { 100, 100, 100 };
            int[] VirAxis_Units = { 1000, 1000, 100 };      //虚拟XY设置成1000表示精确到0.001MM，旋转轴与实际R轴一致

            //初始化关节轴参数
            for (i = 0; i < 3; i++)
            {
                iret = zmcaux.ZAux_Direct_SetAtype(g_handle, i, 1);
                iret = zmcaux.ZAux_Direct_SetUnits(g_handle, i, jAxis_Units[i]);
                iret = zmcaux.ZAux_Direct_SetClutchRate(g_handle, i, 0);             //使用关节轴的速度和加速度限制 虚拟轴速度
            }
            for (i = 0; i < 3; i++)
            {
                iaxis = i + 6;
                iret = zmcaux.ZAux_Direct_SetAtype(g_handle, iaxis, 0);          //设置成虚拟轴
                iret = zmcaux.ZAux_Direct_SetUnits(g_handle, iaxis, VirAxis_Units[i]);
            }
            iret = zmcaux.ZAux_Direct_SetTable(g_handle, 0, 4, Robot_para);
        }

        private void Button3_Click(object sender, EventArgs e)              //机械手逆解
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int iret = 0;
            int[] jAxis_List = { 0, 1, 2 };
            int[] VirAxis_List = {6,7,8};
            if (Robot_status != 1)                  //当前不在逆解状态
            {
                iret = zmcaux.ZAux_Direct_Connframe(g_handle, 3, jAxis_List, 5, 0, 3, VirAxis_List);
            }
        }

        private void Button2_Click(object sender, EventArgs e)              //机械手正解
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int iret = 0;
            int[] jAxis_List = { 0, 1, 2 };
            int[] VirAxis_List = { 6, 7, 8 };
            if (Robot_status != 2)                  //当前不在正解状态
            {
                iret = zmcaux.ZAux_Direct_Connreframe(g_handle, 3, VirAxis_List, 5, 0, 3, jAxis_List);
            }
        }
        


        //点动处理
        //关节正向按下
        private void b0_MouseDown(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 0, Axis_Speed[0]);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 0, Axis_Acc[0]);
            zmcaux.ZAux_Direct_Single_Vmove(g_handle, 0, 1);
        }

        private void b0_MouseUp(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, 0, 2);
        }

        private void b2_MouseDown(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 1, Axis_Speed[1]);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 1, Axis_Acc[1]);
            zmcaux.ZAux_Direct_Single_Vmove(g_handle, 1, 1);
        }

        private void b2_MouseUp(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, 1, 2);
        }

        private void b4_MouseDown(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 2, Axis_Speed[2]);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 2, Axis_Acc[2]);
            zmcaux.ZAux_Direct_Single_Vmove(g_handle, 2, 1);
        }

        private void b4_MouseUp(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, 2, 2);
        }


        //虚拟正向按下
        private void b6_MouseDown(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 6, Axis_Speed[6]);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 6, Axis_Acc[6]);
            zmcaux.ZAux_Direct_Single_Vmove(g_handle, 6, 1);
        }

        private void b6_MouseUp(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, 6, 2);
        }

        private void b8_MouseDown(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 7, Axis_Speed[7]);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 7, Axis_Acc[7]);
            zmcaux.ZAux_Direct_Single_Vmove(g_handle,7, 1);
        }

        private void b8_MouseUp(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, 7, 2);
        }

        private void b10_MouseDown(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 8, Axis_Speed[8]);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 8, Axis_Acc[8]);
            zmcaux.ZAux_Direct_Single_Vmove(g_handle, 8, 1);
        }

        private void b10_MouseUp(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, 8, 2);
        }



        //关节负向按下
        private void b1_MouseDown(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 0, Axis_Speed[0]);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 0, Axis_Acc[0]);
            zmcaux.ZAux_Direct_Single_Vmove(g_handle, 0, -1);
        }

        private void b1_MouseUp(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, 0, 2);
        }

        private void b3_MouseDown(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 1, Axis_Speed[1]);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 1, Axis_Acc[1]);
            zmcaux.ZAux_Direct_Single_Vmove(g_handle, 1, -1);
        }

        private void b3_MouseUp(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, 1, 2);
        }

        private void b5_MouseDown(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 2, Axis_Speed[2]);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 2, Axis_Acc[2]);
            zmcaux.ZAux_Direct_Single_Vmove(g_handle, 2, -1);
        }

        private void b5_MouseUp(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, 2, 2);
        }


        //虚拟负向按下
        private void b7_MouseDown(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 6, Axis_Speed[6]);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 6, Axis_Acc[6]);
            zmcaux.ZAux_Direct_Single_Vmove(g_handle, 6, -1);
        }

        private void b7_MouseUp(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle,6, 2);
        }

        private void b9_MouseDown(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 7, Axis_Speed[7]);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 7, Axis_Acc[7]);
            zmcaux.ZAux_Direct_Single_Vmove(g_handle, 7, -1);
        }

        private void b9_MouseUp(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, 7, 2);
        }

        private void b11_MouseDown(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 8, Axis_Speed[8]);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 8, Axis_Acc[8]);
            zmcaux.ZAux_Direct_Single_Vmove(g_handle, 8, -1);
        }

        private void b11_MouseUp(object sender, MouseEventArgs e)
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, 8, 2);
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)               
        {
            int ipcicardes = zmcaux.ZAux_GetMaxPciCards();
            for (int i = 0; i < ipcicardes - 1; i++)
            {
                C_Ip_Address.Items.Insert(i, i.ToString());
            }
        }

        //心跳检测和错误扫描
        CustomMessageBox customMessageBox = new CustomMessageBox();
        public int controlReturn = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {

            StringBuilder[] str = new StringBuilder[4];
            str[0] = new StringBuilder("no reply");
            str[1] = new StringBuilder();
            str[2] = new StringBuilder();
            if (zmcaux.ZAux_GetControllerInfo(g_handle, str[0], str[1], str[2]) != 0 || str[0].ToString() == "no reply")
            {
                this.Text = "未连接";
                if(timer1.Enabled == true)
                    timer1.Enabled = false;
            }
            else
            {
                this.Text = "已连接";
                if(timer1.Enabled == false)
                    timer1.Enabled = true;
            }


            if (controlReturn != 0)
            {
                string message = string.Format("操作失败,错误码{0:D}", controlReturn);

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


//连接类

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
        this.StartPosition = FormStartPosition.CenterScreen;
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
