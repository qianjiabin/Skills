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
using System.Reflection;

namespace test_Pitchset
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;
       private void C_connect_type_TextChanged(object sender, EventArgs e)
       {
            string connectType = C_connect_type.Text;
            cc = new ConnectContext(connectType);
       }
        public tableData tabledata = new tableData() ;
        public cardData carddata = new cardData();
        public Form1()
        {
            InitializeComponent();
            C_connect_type.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //锁存表格数据绑定
            textBox1.DataBindings.Add("Text", tabledata, "startPos");
            textBox2.DataBindings.Add("Text", tabledata, "maxPoint");
            textBox3.DataBindings.Add("Text", tabledata, "disOne");

            tabledata.PropertyChanged += ShowPosList;

            PropertyChangedEventArgs args = new PropertyChangedEventArgs("startPos");
            ShowPosList(tabledata, args);

            //控制器数据绑定
            textBox8.DataBindings.Add("Text", carddata, "units");
            textBox9.DataBindings.Add("Text", carddata, "speed");
            textBox11.DataBindings.Add("Text", carddata,"acc");
            textBox10.DataBindings.Add("Text", carddata, "dec");
            textBox13.DataBindings.Add("Text", carddata, "creep");
            textBox12.DataBindings.Add("Text", carddata, "datumIn");
            textBox15.DataBindings.Add("Text", carddata, "datumType");
            checkBox2.DataBindings.Add("Checked", carddata, "reversal", true, DataSourceUpdateMode.OnPropertyChanged);
            textBox14.DataBindings.Add("Text", carddata, "delayTime");
            textBox5.DataBindings.Add("Text", carddata, "axis");
            textBox16.DataBindings.Add("Text", carddata,"axsType");

            timer3.Start();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        //扫描函数 2024/4/30
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

            //ControllerInit();
        }

        private void OnClose(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
                return;

            cc.CloseConnect(g_handle);
            g_handle = (IntPtr)0;
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
                string message = string.Format("操作失败 错误码：{0:D}", controlReturn);

                if (customMessageBox.messageBoxClosed == false)
                {
                    customMessageBox.SetDisplayText(message);
                    customMessageBox.ShowDialog();
                }
                controlReturn = 0;
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

        public void ShowPosList(object sender, PropertyChangedEventArgs e)
        {
            while (this.DataGridView1.Rows.Count != 0)
            {
                this.DataGridView1.Rows.RemoveAt(0);
            }
            //绑定变量
            for (int iItem = 0; iItem < tabledata.maxPoint; iItem++)
            {
                int index = this.DataGridView1.Rows.Add();

                this.DataGridView1.Rows[index].Cells[0].Value = index + 1;

                this.DataGridView1.Rows[index].Cells[1].Value = tabledata.startPos + ((iItem+1) * tabledata.disOne);

                this.DataGridView1.Rows[index].Cells[2].Value = 0;

                this.DataGridView1.Rows[index].Cells[3].Value = 0;
            }
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            float[] pfDisancelist = new float[tabledata.maxPoint];
            float[] revDisancelist = new float[tabledata.maxPoint];
            
            for (int i = 0; i < tabledata.maxPoint; i++){
                pfDisancelist[i] = Convert.ToSingle(this.DataGridView1.Rows[i].Cells[2].Value) * carddata.units;
                revDisancelist[i] = Convert.ToSingle(this.DataGridView1.Rows[i].Cells[3].Value) * carddata.units;
            }
         
            if (checkBox1.CheckState == CheckState.Checked)
            {
                if (radioButton1.Checked == true)
                {
                    controlReturn = zmcaux.ZAux_Direct_Pitchset(g_handle, Convert.ToInt32(textBox5.Text), 1, tabledata.startPos, Convert.ToUInt32(tabledata.maxPoint), tabledata.disOne, Convert.ToUInt32(textBox5.Text), pfDisancelist);
                }
                else if (radioButton2.Checked == true)
                {
                    controlReturn = zmcaux.ZAux_Direct_Pitchset2(g_handle, Convert.ToInt32(textBox5.Text), 1, tabledata.startPos, Convert.ToUInt32(tabledata.maxPoint), tabledata.disOne, Convert.ToUInt32(textBox5.Text), pfDisancelist, Convert.ToUInt32(textBox5.Text) + Convert.ToUInt32(tabledata.maxPoint), revDisancelist);
                }
            }
            else {
                controlReturn = zmcaux.ZAux_Direct_Pitchset(g_handle, Convert.ToInt32(textBox5.Text), 0, tabledata.startPos, Convert.ToUInt32(tabledata.maxPoint), tabledata.disOne, Convert.ToUInt32(textBox5.Text), pfDisancelist);
            }
        }

        private void timer1_Tick(object sender, EventArgs e) 
        { 
            float fval = 0;
            controlReturn = zmcaux.ZAux_Direct_GetDpos(g_handle, Convert.ToInt32(textBox5.Text), ref fval);
            textBox6.Text = fval.ToString();
            controlReturn = zmcaux.ZAux_Direct_GetMpos(g_handle, Convert.ToInt32(textBox5.Text), ref fval);
            textBox7.Text = fval.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            controlReturn = zmcaux.ZAux_Direct_SetDatumIn(g_handle, Convert.ToInt32(carddata.axis), carddata.datumIn);
            controlReturn = zmcaux.ZAux_Direct_SetCreep(g_handle, Convert.ToInt32(carddata.axis), carddata.creep);
            controlReturn = zmcaux.ZAux_Direct_Single_Datum(g_handle, Convert.ToInt32(carddata.axis), Convert.ToInt32(carddata.datumType));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            controlReturn = zmcaux.ZAux_Direct_Single_Cancel(g_handle,Convert.ToInt32(carddata.axis),3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            controlReturn = zmcaux.ZAux_Direct_SetAtype(g_handle, Convert.ToInt32(carddata.axis), carddata.axsType);
            controlReturn = zmcaux.ZAux_Direct_SetUnits(g_handle, Convert.ToInt32(carddata.axis), carddata.units);
            controlReturn = zmcaux.ZAux_Direct_SetSpeed(g_handle, Convert.ToInt32(carddata.axis), carddata.speed);
            controlReturn = zmcaux.ZAux_Direct_SetAccel(g_handle, Convert.ToInt32(carddata.axis), carddata.acc);
            controlReturn = zmcaux.ZAux_Direct_SetDecel(g_handle, Convert.ToInt32(carddata.axis), carddata.dec);

            checkBox1_CheckStateChanged(new object(),new EventArgs());

            if (checkBox3.CheckState == CheckState.Checked)
            {
                controlReturn = zmcaux.ZAux_Direct_Single_MoveAbs(g_handle, Convert.ToInt32(carddata.axis), tabledata.startPos);
                for (int i = 0; i < tabledata.maxPoint; i++)
                {
                    controlReturn = zmcaux.ZAux_Direct_MoveAbs(g_handle, 1, new int[1] { Convert.ToInt32(carddata.axis) }, new float[1] { Convert.ToSingle(this.DataGridView1.Rows[i].Cells[1].Value) });
                    controlReturn = zmcaux.ZAux_Direct_MoveDelay(g_handle, Convert.ToInt32(carddata.axis), carddata.delayTime);
                }
            }
            else
            {
                for (int i = tabledata.maxPoint - 1; i >= 0; i--)
                {
                    controlReturn = zmcaux.ZAux_Direct_MoveAbs(g_handle, 1, new int[1] { Convert.ToInt32(carddata.axis) }, new float[1] { Convert.ToSingle(this.DataGridView1.Rows[i].Cells[1].Value) });
                    controlReturn = zmcaux.ZAux_Direct_MoveDelay(g_handle, Convert.ToInt32(carddata.axis), carddata.delayTime);
                }
                controlReturn = zmcaux.ZAux_Direct_Single_MoveAbs(g_handle, Convert.ToInt32(carddata.axis), tabledata.startPos);
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            controlReturn = zmcaux.ZAux_Direct_SetInvertIn(g_handle, carddata.datumIn, carddata.reversal ? 0 : 1);
        }
    }
}

public class tableData : INotifyPropertyChanged
{
    private int _startPos;
    public int startPos
    {
        get { return _startPos; }
        set { _startPos = value;
              OnPropertyChanged(startPos.ToString());
            } 
    }
    private int _maxPoint;
    public int maxPoint
    {
        get { return _maxPoint; }
        set
        {
            _maxPoint = value;
            OnPropertyChanged(maxPoint.ToString());
        }
    }
    private int _disOne;
    public int disOne 
    { get { return _disOne; } 
      set { _disOne = value;
            OnPropertyChanged(disOne.ToString());  
          } 
    }

    public tableData() { startPos = 0; maxPoint = 20; _disOne = 10; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

public class cardData
{
    public float units { set; get; }
    public float speed { set; get; }
    public float acc { set; get; }
    public float dec { set; get; }
    public float creep { set; get; }
    public int datumIn { set; get; }
    public uint datumType { set; get; }
    public bool reversal { set; get; }
    public int delayTime { set; get; }
    public uint axis { set; get; }
    public int axsType { set; get; }

    public cardData() 
    { 
        units = 1; 
        speed = 100; 
        acc = 1000;
        dec = 0;
        creep = 10;
        datumIn = 0;
        reversal = false;
        delayTime = 1000;
        axis = 0;
        axsType = 5;
        datumType = 103;
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
