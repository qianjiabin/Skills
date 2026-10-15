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
using System.Threading;
using System.Threading.Tasks;

namespace Test_MovePt
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;
       private void C_connect_type_TextChanged(object sender, EventArgs e)
       {
            string connectType = C_connect_type.Text;
            cc = new ConnectContext(connectType);
       }
        public tableData tabledata = new tableData();

        public Form1()
        {
            InitializeComponent();
            C_connect_type.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.DataBindings.Add("Text", tabledata, "Data_k");
            textBox2.DataBindings.Add("Text", tabledata, "Data_ticks");
            textBox3.DataBindings.Add("Text", tabledata, "Data_b");
            textBox4.DataBindings.Add("Text", tabledata, "Data_lenth");

            timer3.Start();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            button1_Click(new object(),new EventArgs());
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
                if (timer1.Enabled == false)
                    timer1.Enabled = true;
            }


            if (controlReturn != 0)
            {
                string message = string.Format("操作失败 错误码：{0:D}", controlReturn);

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

        private void button1_Click(object sender, EventArgs e)
        {
            while (this.dataGridView1.Rows.Count != 0)
            {
                this.dataGridView1.Rows.RemoveAt(0);
            }

            for (int iItem = 0; iItem < tabledata.Data_lenth; iItem++)
            {
                int index = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[index].Cells[0].Value = index;
                this.dataGridView1.Rows[index].Cells[1].Value = tabledata.Data_ticks;
                
            }

            if (radioButton1.Checked == true)
            {

                for (int iItem = 0; iItem < tabledata.Data_lenth; iItem++)
                {
                    this.dataGridView1.Rows[iItem].Cells[2].Value = tabledata.Data_k * Math.Sin((double)iItem / (tabledata.Data_lenth - 1) * 2 * 3.1415) + tabledata.Data_b;
                    this.dataGridView1.Rows[iItem].Cells[3].Value = (tabledata.Data_k * (double)1 / (tabledata.Data_lenth - 1) * 2 * 3.1415 * Math.Cos((double)iItem / (tabledata.Data_lenth - 1) * 2 * 3.1415));
                }
            }
            else { 
                for (int iItem = 0; iItem < tabledata.Data_lenth; iItem++)
                {
                    this.dataGridView1.Rows[iItem].Cells[2].Value = tabledata.Data_k * Math.Cos((double)iItem / ((tabledata.Data_lenth - 1)) * 2 * 3.1415) + tabledata.Data_b;
                    this.dataGridView1.Rows[iItem].Cells[3].Value = -1 * (tabledata.Data_k * (double)1 / (tabledata.Data_lenth - 1)) * 2 * 3.1415 * Math.Sin((double)iItem / (tabledata.Data_lenth - 1) * 2 * 3.1415);
                }
            }
                
        }
        //Great moment
        Task taskA ;
        private bool isTaskARunning = false;

        private void button2_Click(object sender, EventArgs e)
        {
            taskA = new Task(move_Pt);
            if (isTaskARunning)
                taskA.ContinueWith(t => { isTaskARunning = false; }, TaskScheduler.FromCurrentSynchronizationContext());
            else {
                taskA.ContinueWith(t => { isTaskARunning = false; }, TaskScheduler.FromCurrentSynchronizationContext());
                taskA.Start();
                isTaskARunning = true;
            }
        }

        private void  move_Pt() {

            int iSendLen = 30;
            int m_CurMark = 0;
            int iReBuff = 0;
            int DataLenth = (tabledata.Data_lenth);

            uint[] uint_arr = new uint[iSendLen];
            float[] float_arr = new float[iSendLen];

            while (m_CurMark < DataLenth)
            {
                controlReturn = zmcaux.ZAux_Direct_GetRemain_LineBuffer(g_handle, 0, ref iReBuff);
                if (DataLenth - m_CurMark < iSendLen)
                    iSendLen = DataLenth - m_CurMark;
                if (iReBuff < iSendLen * 2)
                    iSendLen = iReBuff / 2;
                for (int iItem = 0; iItem < iSendLen; iItem++)
                {
                    uint_arr[iItem] = Convert.ToUInt32(this.dataGridView1.Rows[m_CurMark + iItem].Cells[1].Value);
                    float_arr[iItem] = Convert.ToSingle(this.dataGridView1.Rows[m_CurMark + iItem].Cells[2].Value);
                }
                controlReturn =  zmcaux.ZAux_Direct_MultiMovePtAbs(g_handle, iSendLen, 1, new int[] { 0 }, uint_arr, float_arr);
                if (controlReturn == 0)
                {
                    m_CurMark += iSendLen;
                }
                else { return; }    
            }
        }

        private void move_Pvt()
        {

            int iSendLen = 20;
            int m_CurMark = 0;
            int iReBuff = 0;
            int DataLenth = (tabledata.Data_lenth);

            uint[] uint_Ticks = new uint[iSendLen];
            float[] disList = new float[iSendLen];
            float[] speedList = new float[iSendLen];

            while (m_CurMark < DataLenth)
            {
                controlReturn = zmcaux.ZAux_Direct_GetRemain_LineBuffer(g_handle, 0, ref iReBuff);
                if (DataLenth - m_CurMark < iSendLen)
                    iSendLen = DataLenth - m_CurMark;
                if (iReBuff < iSendLen * 2)
                    iSendLen = iReBuff / 2;
                for (int iItem = 0; iItem < iSendLen; iItem++)
                {
                    uint_Ticks[iItem] = Convert.ToUInt32(this.dataGridView1.Rows[m_CurMark + iItem].Cells[1].Value);
                    disList[iItem] = Convert.ToSingle(this.dataGridView1.Rows[m_CurMark + iItem].Cells[2].Value);
                    speedList[iItem] = Convert.ToSingle(this.dataGridView1.Rows[m_CurMark + iItem].Cells[3].Value);
                }
                
                controlReturn = zmcaux.ZAux_Direct_MultiMovePvtAbs(g_handle, iSendLen, 1, new int[] { 0 }, uint_Ticks, disList, speedList);
               
                if (controlReturn == 0)
                {
                    m_CurMark += iSendLen;
                }
                else { return; }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            taskA = new Task(move_Pvt);
            if (isTaskARunning)
                taskA.ContinueWith(t => { isTaskARunning = false; }, TaskScheduler.FromCurrentSynchronizationContext());
            else
            {
                taskA.ContinueWith(t => { isTaskARunning = false; }, TaskScheduler.FromCurrentSynchronizationContext());
                taskA.Start();
                isTaskARunning = true;
            }
        }
    }
}

public class tableData 
{
    public int Data_lenth { set; get; }
    public int Data_ticks { set; get; }
    public float Data_k { set; get; }
    public float Data_b { set; get; }

    public tableData() { Data_lenth = 1000; Data_ticks = 1; Data_k = 100; Data_b = 0; }
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
