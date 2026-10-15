using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices; 
using cszmcaux;
using System.IO.Ports;

namespace Test_AutoUp
{
    public partial class Form1 : Form
    {

        public  IntPtr g_handle;
       private void C_connect_type_TextChanged(object sender, EventArgs e)
       {
            string connectType = C_connect_type.Text;
            cc = new ConnectContext(connectType);
       }
        public GCHandle gch;
        public static string AutoUpStr ="";
        public static Int32 AutoUpLen = 0;

        public Form1()
        {
            InitializeComponent();
            C_connect_type.SelectedIndex = 0;
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

        private void button1_Click(object sender, EventArgs e)              //下载BAS
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                int ret = 0;
                string strFilePath;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                openFileDialog1.InitialDirectory = "\\";
                openFileDialog1.Filter = "配置文件(*.bas)|*.bas";
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.FilterIndex = 1;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)            //打开配置文件
                {
                    strFilePath = openFileDialog1.FileName;
                    ret = zmcaux.ZAux_BasDown(g_handle, strFilePath, 1);               //下载到ROM
                    if (ret != 0)
                    {
                        MessageBox.Show("文件下载失败!", "提示");
                    }
                }
            }
        }

        

        private void button8_Click(object sender, EventArgs e)              //清空
        {
            textBox4.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)                //定时器
        {
            if (AutoUpLen != 0)
            {
                textBox4.Text += AutoUpStr;
                AutoUpLen = 0;
            }

        }

        public static void ZmcAutoCallBaceTest(IntPtr handle, Int32 itypecode, Int32 idatalength, StringBuilder pdata)
        {
            AutoUpStr = "Code: " + itypecode.ToString() + "\tInfo: " + pdata.ToString() + "\r\n";
            AutoUpLen = idatalength;
            return;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)       //启动上报
        {
            int iresult = 0;
            if (checkBox1.CheckState == CheckState.Checked)
            {
		        string cmdbuff;
                StringBuilder cmdbuffAck = new StringBuilder(1024);
                cmdbuff = "AutoCmdString =" + textCmd.Text;                     //生成主动上报命令
               iresult = zmcaux.ZAux_Execute(g_handle, cmdbuff, cmdbuffAck, 1024);

                zmcaux.ZAuxCallBack mycall = new zmcaux.ZAuxCallBack(ZmcAutoCallBaceTest);
                gch = GCHandle.Alloc(mycall);
                zmcaux.ZAux_SetAutoUpCallBack(g_handle, (zmcaux.ZAuxCallBack)gch.Target);

                //修改BAS全局变量,
                iresult = zmcaux.ZAux_Direct_SetUserVar(g_handle, "AutoUpTime", Convert.ToSingle(textTime.Text));				//上报间隔时间
                iresult = zmcaux.ZAux_Direct_SetUserVar(g_handle, "AutoUpCode", Convert.ToSingle(textCode.Text));				//上报功能码
                iresult = zmcaux.ZAux_Direct_SetUserVar(g_handle, "If_EnableAutoUp", 1);			//上报使能		
                timer1.Enabled = true;

            }
            else
            {
                iresult = zmcaux.ZAux_Direct_SetUserVar(g_handle, "If_EnableAutoUp", 0);			//关闭上报使能		
                timer1.Enabled = false;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer2.Enabled = true;

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
