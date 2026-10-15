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

namespace test_UserSub
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
       private void C_connect_type_TextChanged(object sender, EventArgs e)
       {
           string connectType = comboBox1.Text;
            cc = new ConnectContext(connectType);
       }
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void OnSelect(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
        }

        ConnectContext cc;
        private void OnScan(object sender, EventArgs e)
        {

            string connectType = comboBox1.Text;
            if (connectType == "")
            {
                MessageBox.Show("请先选择连接方式");
                return;
            }
            comboBox2.Items.Clear();
            cc = new ConnectContext(connectType);
            string[] scanResult = cc.ScanConnect();
            for (int i = 0; i < scanResult.Length; i++)
            {
                comboBox2.Items.Insert(i, scanResult[i].ToString());
            }
        }



        private void OnOpen(object sender, EventArgs e)
        {
            string Buffer;
            Buffer = comboBox2.Text;
            if (Buffer == "")
            {
                MessageBox.Show("连接地址为空");
                return;
            }
            int iresult = cc.OpenConnect(ref g_handle, Buffer);


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
            if (g_handle == (IntPtr)0)
                return;

            cc.CloseConnect(g_handle);
            g_handle = (IntPtr)0;
            this.Text = "未连接";
            timer1.Enabled = false;
        }

        private void OnClickedClear(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void AppendTextOut(string sTrace)
        {
            string stemp = sTrace;
            stemp = stemp.Replace("\n", "\r\n");

            int nLen = textBox1.Text.Length;

            textBox1.MaxLength = 2000000;

            if (nLen > 1000000)
            {
                // 删除5000行
                int lineIndex = textBox1.GetFirstCharIndexFromLine(1000);
                // 不滚动
                textBox1.Select(0, lineIndex);
                textBox1.SelectedText = "";

                nLen = textBox1.Text.Length;
            }

            textBox1.Select(nLen, nLen);
            textBox1.SelectedText = stemp;
        }

        private void OnBnClickedCmd(object sender, EventArgs e)
        {
            if (IntPtr.Zero == g_handle)
            {
                MessageBox.Show("Connect First!");
                return;
            }
            else if (textBox2.Text.Length != 0)
            {
                string buffer = textBox2.Text;
                StringBuilder cmdbuffAck = new StringBuilder(2048);
                int iresult = zmcaux.ZAux_DirectCommand(g_handle, buffer, cmdbuffAck, 2048);
                if (iresult == 0)
                {
                    string str = string.Format(">> {0}  \r\n返回字符串: {1} \r\n", buffer, cmdbuffAck);
                    AppendTextOut(str);
                }
                else 
                {
                    string str = string.Format(">> {0}  \r\n返回错误码：{1:D} \r\n", buffer,iresult, cmdbuffAck);
                    AppendTextOut(str);
                }
            }
            return;
        }

        private void OnBnClickedExecute(object sender, EventArgs e)
        {
            if (IntPtr.Zero == g_handle)
            {
                MessageBox.Show("Connect First!");
                return;
            }
            else if (textBox2.Text.Length != 0)
            {
                string buffer = textBox2.Text;
                StringBuilder cmdbuffAck = new StringBuilder(2048);
                int iresult = zmcaux.ZAux_Execute(g_handle, buffer, cmdbuffAck, 2048);
                if (iresult == 0)
                {
                    string str = string.Format(">> {0}  \r\n返回字符串: {1} \r\n", buffer, cmdbuffAck);
                    AppendTextOut(str);
                }
                else
                {
                    string str = string.Format(">> {0}  \r\n返回错误码：{1:D} \r\n", buffer, iresult, cmdbuffAck);
                    AppendTextOut(str);
                }
            }
            return;
        }

        int Func_AxisMove(IntPtr Handle, int iAxis, float fSpeed, float fAcc, float fPos, float imode)
        {
            string cmdbuff = string.Empty;
            StringBuilder cmdbuffAck = new StringBuilder();
            string str = string.Format("SPEED({0:D}) = {1:f2}\r\n", iAxis, fSpeed);
            cmdbuff += str;
            str = string.Format("ACCEL({0:D}) = {1:f2}\r\n", iAxis, fAcc);
            cmdbuff += str;
            if (imode == 0)
            {
                str = string.Format("Move({0:f3}) AXIS({1:D})\r\n", fPos, iAxis);
            }
            else
            {
                str = string.Format("MoveABS({0:f3}) AXIS({1:D})\r\n", fPos, iAxis);
            }
            cmdbuff += str;
            return zmcaux.ZAux_DirectCommand(Handle, cmdbuff, cmdbuffAck, 2048);
        }

        private void OnBnClickGosub(object sender, EventArgs e)
        {
            if (IntPtr.Zero != g_handle)
            {
                int ret = Func_AxisMove(g_handle, 0, 50, 1000, 100, 0);
                textBox3.Text = ret.ToString();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (IntPtr.Zero != g_handle)
            {
                float Axisdpos = 0;
                int iret = zmcaux.ZAux_Direct_GetDpos(g_handle, 0, ref Axisdpos);
                textBox4.Text = Axisdpos.ToString();
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
