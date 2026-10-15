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
using System.IO;

namespace Zmc_Pro
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;
       private void C_connect_type_TextChanged(object sender, EventArgs e)
       {
            string connectType = C_connect_type.Text;
            cc = new ConnectContext(connectType);
       }
        public Form1()
        {
            InitializeComponent();
            C_connect_type.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer3.Start();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

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

            SetTimeOutPara();
            ControllerInit();

        }

        private void OnClose(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
                return;

            cc.CloseConnect(g_handle);
            g_handle = (IntPtr)0;
        }


        //心跳函数 和 错误检测函数 2024/4/30
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
                
            }
            else
            {
                this.Text = "已连接";
                
            }
            

            if (controlReturn != 0)
            {
                string message = string.Format("操作失败 错误码：{0:D}", controlReturn);

                if (customMessageBox.messageBoxClosed == false)
                {
                    customMessageBox.SetDisplayText(message);
                    customMessageBox.ShowDialog();
                }
                else if (customMessageBox.messageBoxClosed == true)
                {
                    controlReturn = 0;
                }
            }
        }

        //初始化控制器 2024/4/30
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

        private void SetTimeOutPara() 
        {
            float fval = 0;
            controlReturn = zmcaux.ZAux_Direct_GetUserVar(g_handle,"Heart_Flag",ref fval);
            if (controlReturn == 2033)
            {
                MessageBox.Show("Bas文件未加载，请加载心跳保护程序！");

                string m_strOpenFileName;
                FileStream openFile;

                OpenFileDialog fileOpen = new OpenFileDialog();
                fileOpen.Filter = "Basic Files (*.bas)|*.bas|All Files (*.*)|*.*";
                fileOpen.RestoreDirectory = true;
                fileOpen.FilterIndex = 1;

                if (fileOpen.ShowDialog() == DialogResult.OK)
                {
                    m_strOpenFileName = fileOpen.FileName;
                    try
                    {
                        openFile = new FileStream(fileOpen.FileName, FileMode.Open, FileAccess.Read);
                        openFile.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Open Failed!");
                        return;
                    }
                    controlReturn = zmcaux.ZAux_BasDown(g_handle,m_strOpenFileName,0);
                    if(controlReturn == 0)
                        MessageBox.Show("心跳保护程序文件已加载");

                }      
            }
            float m_TimeOut = 1000;
            controlReturn = zmcaux.ZAux_Direct_SetUserVar(g_handle, "TimeOut",m_TimeOut);

            float m_ErrOut = 10;
            controlReturn = zmcaux.ZAux_Direct_SetUserVar(g_handle,"Out_Num",m_ErrOut);

            float[] m_ErrOutList = new float[Convert.ToInt32(m_ErrOut)];
            for (int i = 0; i < m_ErrOut; i++)										//设定超时控制OUT编号
            {
                m_ErrOutList[i] = i;
            }
            controlReturn = zmcaux.ZAux_Direct_SetUserArray(g_handle, "Out_List", 0, Convert.ToInt32(m_ErrOut), m_ErrOutList);

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            zmcaux.ZAux_Direct_SetUserVar(g_handle,"Heart_Flag",0);
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

//自定义弹窗类 2024/4/30
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
       this.Font = new Font("黑体", 9, this.Font.Style & ~FontStyle.Italic);	

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
