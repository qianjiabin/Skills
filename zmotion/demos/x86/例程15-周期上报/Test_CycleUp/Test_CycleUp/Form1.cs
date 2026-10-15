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

namespace Test_CycleUp
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;
       private void C_connect_type_TextChanged(object sender, EventArgs e)
       {
            string connectType = C_connect_type.Text;
            cc = new ConnectContext(connectType);
       }
        public bool StartUp = false;
        Data[] data = new Data[3];

        public Form1()
        {
            InitializeComponent();
            C_connect_type.SelectedIndex = 0;
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
                if (timer1.Enabled == true)
                    timer1.Enabled = false;
            }
            else
            {
                this.Text = "已连接";
            }


            if (controlReturn != 0)
            {
                string message = string.Format("操作失败 错误码:{0:D}", controlReturn);

                if (customMessageBox.messageBoxClosed == false)
                {
                    customMessageBox.SetDisplayText(message);
                    customMessageBox.ShowDialog();
                }
                else if (customMessageBox.messageBoxClosed == true) {
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

        private void button2_Click(object sender, EventArgs e)
        {
            textBox10.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                controlReturn = zmcaux.ZAux_CycleUpForceOnce(g_handle, Convert.ToUInt32(textBox1.Text));
            }
            catch (FormatException fe)
            {
                MessageBox.Show(string.Format("{0}: {1}", fe.GetType().Name, fe.Message));
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 2;

            checkbox[0] = checkBox2;
            checkbox[1] = checkBox3;
            checkbox[2] = checkBox4;

            combobox[0] = comboBox1;
            combobox[1] = comboBox2;
            combobox[2] = comboBox3;


            TextBox[] textboxStart = { textBox3, textBox4, textBox5 };
            TextBox[] textboxNum = { textBox8, textBox7, textBox6 };
            

            for (int i = 0; i < 3; i++)
            {
                data[i] = new Data();
                textboxStart[i].DataBindings.Add("Text", data[i], "CycleParaStart");
                textboxNum[i].DataBindings.Add("Text", data[i], "CycleParaNum");
               
            }

            timer3.Start();
        }
        DateTime start;
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            StringBuilder psetesname = GetCycleStr();
            if (!StartUp && checkBox1.Checked == true) {
                try
                {
                    controlReturn = zmcaux.ZAux_CycleUpEnable(g_handle, Convert.ToUInt32(textBox1.Text), Convert.ToSingle(textBox2.Text), psetesname.ToString());
                }
                catch (FormatException fe)
                {
                    checkBox1.Checked = false;
                    MessageBox.Show(string.Format("{0}: {1}", fe.GetType().Name, fe.Message));
                    return;
                }
                if (controlReturn != 0)
                {
                    checkBox1.Checked = false;
                    return;    
                }
                textBox9.Text = "0";
                AppendTextOut(string.Format("周期上报开始！\r\n命令：{0}\r\n", psetesname.ToString()));
                start = DateTime.Now;
                timer1.Enabled = true;
                StartUp = true;
            }else if(StartUp && checkBox1.Checked == false){

                controlReturn = zmcaux.ZAux_CycleUpDisable(g_handle, Convert.ToUInt32(textBox1.Text));
                DateTime end = DateTime.Now;
                if (controlReturn != 0) {
                    AppendTextOut(string.Format("周期上报关闭失败！ 错误码：{0:D} \r\n",controlReturn));
                    return;
                }
                timer1.Enabled = false;
                StartUp = false;
                TimeSpan abs = end - start;
                AppendTextOut(string.Format("周期上报关闭 - 上报用时：{0:N3}ms， 上报次数：{1:N3}, 平均时间：{2:N3} \r\n", abs.TotalMilliseconds, Convert.ToDouble(textBox9.Text), abs.TotalMilliseconds / Convert.ToDouble(textBox9.Text)));
            }
        }
        ComboBox[] combobox = new ComboBox[3];
        private StringBuilder GetCycleStr() {

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < 3; i++) {
                if (!checkbox[i].Checked)
                    continue;
                result.AppendFormat("{0}({1},{2})",combobox[i].SelectedItem.ToString(), data[i].CycleParaStart, data[i].CycleParaNum);
                for (int j = i + 1; j < 3 && checkbox[j].Checked; j++){
                    result.Append(",");
                    break;
                }
            }
            return result;
        }

        private void AppendTextOut(string sTrace)
        {
            string stemp = sTrace;
            stemp = stemp.Replace("\n", "\r\n");

            int nLen = textBox10.Text.Length;

            textBox10.MaxLength = 2000000;

            if (nLen > 1000000)
            {
                // 删除5000行
                int lineIndex = textBox10.GetFirstCharIndexFromLine(1000);
                // 不滚动
                textBox1.Select(0, lineIndex);
                textBox1.SelectedText = "";

                nLen = textBox10.Text.Length;
            }

            textBox10.Select(nLen, nLen);
            textBox10.SelectedText = stemp;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int received;
            try{
                 received = zmcaux.ZAux_CycleUpGetRecvTimes(g_handle,Convert.ToUInt32(textBox1.Text));
             }
            catch (FormatException fe)
            {
                timer1.Enabled = false;
                MessageBox.Show(string.Format("{0}: {1}", fe.GetType().Name, fe.Message));
                return;
            }
            if (received != Convert.ToUInt32(textBox9.Text))
            {
                GetCycleInfo();
                textBox9.Text = string.Format("{0:D}", received);
            }
        }
        CheckBox[] checkbox = new CheckBox[3];
        private void GetCycleInfo() 
        {
            StringBuilder showString = new StringBuilder();
           
            int ival = 0;

            for (int num = 0; num < 3; num++) {
                if (checkbox[num].Checked){   
                    showString.Append(combobox[num].SelectedItem.ToString());
                    for (uint i = (uint)data[num].CycleParaStart; i < data[num].CycleParaNum; i++) {
                        controlReturn = zmcaux.ZAux_CycleUpReadBuffInt(g_handle, Convert.ToUInt32(textBox1.Text), combobox[num].SelectedItem.ToString(),i, ref ival);
                        showString.AppendFormat(" {0:D}",ival);
                    }
                    showString.Append("\n");
                }
            }
            AppendTextOut(showString.ToString());
        }
    }
}


public class Data
{

    public int CycleParaStart { get; set; }
    public int CycleParaNum { get; set; }

    public Data() { CycleParaStart = 0; CycleParaNum = 5; }
    
    
}

//主类外面
//连接类 2024/4/30
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
