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


namespace test_cam
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
       private void C_connect_type_TextChanged(object sender, EventArgs e)
       {
            string connectType = C_connect_type.Text;
            cc = new ConnectContext(connectType);
       }
        public const int MAX_POINT = 720;
        public int m_AxisMaster = 0;
        public int m_axis_Slave = 1;
        public float m_CamMoveSp = 100.0f;
        public float m_CamMoveDist = 1000.0f;
        public bool m_SynMode = true;
        public float[] m_MoveLinkPara = new float[12];
        public float[] m_MoveSLinkPara = new float[12];
        public float m_LinkPos = 1000.0f;
        public float m_LinkSp = 100.0f;
        public float[] fPointPos = new float[MAX_POINT];
        
       

        public Form1()
        {
            InitializeComponent();
            C_connect_type.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)         //初始化
        {
            
            m_MoveLinkPara[0] = 50.0f;
            m_MoveLinkPara[1] = 100.0f;
            m_MoveLinkPara[2] = 100.0f;
            m_MoveLinkPara[3] = 0f;

            m_MoveLinkPara[4] = 200.0f;
            m_MoveLinkPara[5] = 200.0f;
            m_MoveLinkPara[6] = 0f;
            m_MoveLinkPara[7] = 0f;

            m_MoveLinkPara[8] = 50.0f;
            m_MoveLinkPara[9] = 100.0f;
            m_MoveLinkPara[10] = 0.0f;
            m_MoveLinkPara[11] = 100f;

            m_MoveSLinkPara[0] = 50.0f;
            m_MoveSLinkPara[1] = 100.0f;
            m_MoveSLinkPara[2] = 0f;
            m_MoveSLinkPara[3] = 1f;

            m_MoveSLinkPara[4] = 200.0f;
            m_MoveSLinkPara[5] = 200.0f;
            m_MoveSLinkPara[6] = 1.0f;
            m_MoveSLinkPara[7] = 1.0f;

            m_MoveSLinkPara[8] = 50.0f;
            m_MoveSLinkPara[9] = 100.0f;
            m_MoveSLinkPara[10] = 1.0f;
            m_MoveSLinkPara[11] = 0.0f;

            ShowPswtichList();
            
            timer1.Enabled = true;
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
            this.Text = "未连接";
            timer1.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            //int iret = 0;
            update_link();
            float AxisSlave_Units = 100;
            controlReturn = zmcaux.ZAux_Direct_SetUnits(g_handle, m_AxisMaster, 1000);
            controlReturn = zmcaux.ZAux_Direct_SetUnits(g_handle, m_axis_Slave, AxisSlave_Units);
            for (int i = 0; i < MAX_POINT; i++)
            {
                fPointPos[i] = Convert.ToSingle(DataGridView1.Rows[i].Cells["位置"].Value);
            }
            controlReturn = zmcaux.ZAux_Direct_SetTable(g_handle, 0, MAX_POINT, fPointPos);                                                 //填写从轴凸轮轨迹到TABLE
            controlReturn = zmcaux.ZAux_Direct_Cambox(g_handle, m_axis_Slave, 0, MAX_POINT - 1, AxisSlave_Units, m_CamMoveDist, m_AxisMaster, 0, 0);
            zmcaux.ZAux_Trigger(g_handle);
            controlReturn = zmcaux.ZAux_Direct_SetSpeed(g_handle, m_AxisMaster, m_CamMoveSp);                        //启动主轴运动
            controlReturn = zmcaux.ZAux_Direct_Single_Move(g_handle, m_AxisMaster, m_CamMoveDist);  
        }

        private void Button2_Click(object sender, EventArgs e)              //停止
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int iret = 0;
            iret = zmcaux.ZAux_Direct_Single_Cancel(g_handle, m_AxisMaster, 2);
        }

        private void Button3_Click(object sender, EventArgs e)              //运动
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int iret = 0;
            update_link();
            if (m_SynMode == true)
            {
                iret = zmcaux.ZAux_Direct_SetMerge(g_handle, m_axis_Slave, 1);
                iret = zmcaux.ZAux_Direct_Movelink(g_handle, m_axis_Slave, m_MoveLinkPara[0], m_MoveLinkPara[1], m_MoveLinkPara[2], m_MoveLinkPara[3], m_AxisMaster, 0, 0);      //从轴加速运动自同步
                iret = zmcaux.ZAux_Direct_MoveOp(g_handle, m_axis_Slave, 0, 1);                //速度同步后打开飞剪
                iret = zmcaux.ZAux_Direct_Movelink(g_handle, m_axis_Slave, m_MoveLinkPara[4], m_MoveLinkPara[5], m_MoveLinkPara[6], m_MoveLinkPara[7], m_AxisMaster, 0, 0);       //匀速同步跟随段
                iret = zmcaux.ZAux_Direct_MoveOp(g_handle, m_axis_Slave, 0, 0);                //关闭飞剪
                iret = zmcaux.ZAux_Direct_Movelink(g_handle, m_axis_Slave, m_MoveLinkPara[8], m_MoveLinkPara[9], m_MoveLinkPara[10], m_MoveLinkPara[11], m_AxisMaster, 0, 0);         //减速段
                zmcaux.ZAux_Trigger(g_handle);
                iret = zmcaux.ZAux_Direct_SetSpeed(g_handle, m_AxisMaster, m_LinkSp);                                 //启动主轴运动，模拟触发同步跟随，
                iret = zmcaux.ZAux_Direct_Single_Move(g_handle, m_AxisMaster, m_LinkPos);
            }
            else
            {
                iret = zmcaux.ZAux_Direct_SetMerge(g_handle, m_axis_Slave, 1);
                iret = zmcaux.ZAux_Direct_Moveslink(g_handle, m_axis_Slave, m_MoveSLinkPara[0], m_MoveSLinkPara[1], m_MoveSLinkPara[2], m_MoveSLinkPara[3], m_AxisMaster, 0, 0);        //从轴加速运动自同步
                iret = zmcaux.ZAux_Direct_MoveOp(g_handle, m_axis_Slave, 0, 1);         //速度同步后打开飞剪
                iret = zmcaux.ZAux_Direct_Moveslink(g_handle, m_axis_Slave, m_MoveSLinkPara[4], m_MoveSLinkPara[5], m_MoveSLinkPara[6], m_MoveSLinkPara[7], m_AxisMaster, 0, 0);        //匀速同步跟随段
                iret = zmcaux.ZAux_Direct_MoveOp(g_handle, m_axis_Slave, 0, 0);         //关闭飞剪
                iret = zmcaux.ZAux_Direct_Moveslink(g_handle, m_axis_Slave, m_MoveSLinkPara[8], m_MoveSLinkPara[9], m_MoveSLinkPara[10], m_MoveSLinkPara[11], m_AxisMaster, 0, 0);        //减速段
                zmcaux.ZAux_Trigger(g_handle);
                iret = zmcaux.ZAux_Direct_SetSpeed(g_handle, m_AxisMaster, m_LinkSp);                                 //启动主轴运动，模拟触发同步跟随，
                iret = zmcaux.ZAux_Direct_Single_Move(g_handle, m_AxisMaster, m_LinkPos);
            }
        }

        private void Button4_Click(object sender, EventArgs e)          //停止
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int iret = 0;
            iret = zmcaux.ZAux_Direct_Single_Cancel(g_handle, m_AxisMaster, 2);
        }

        private void update_link()                  //更新运动参数
        {
            m_AxisMaster = Convert.ToInt32(TextBox1.Text);
            m_axis_Slave = Convert.ToInt32(TextBox2.Text);
            m_CamMoveSp = Convert.ToSingle(TextBox3.Text);
            m_CamMoveDist = Convert.ToSingle(TextBox4.Text);
            m_SynMode = RadioButton1.Checked;
            m_MoveLinkPara[0] = Convert.ToSingle(m0.Text);
            m_MoveLinkPara[1] = Convert.ToSingle(m1.Text);
            m_MoveLinkPara[2] = Convert.ToSingle(m2.Text);
            m_MoveLinkPara[3] = Convert.ToSingle(m3.Text);

            m_MoveLinkPara[4] = Convert.ToSingle(m4.Text);
            m_MoveLinkPara[5] = Convert.ToSingle(m5.Text);
            m_MoveLinkPara[6] = Convert.ToSingle(m6.Text);
            m_MoveLinkPara[7] = Convert.ToSingle(m7.Text);

            m_MoveLinkPara[8] = Convert.ToSingle(m8.Text);
            m_MoveLinkPara[9] = Convert.ToSingle(m9.Text);
            m_MoveLinkPara[10] = Convert.ToSingle(m10.Text);
            m_MoveLinkPara[11] = Convert.ToSingle(m11.Text);

            m_MoveSLinkPara[0] = Convert.ToSingle(ms0.Text);
            m_MoveSLinkPara[1] = Convert.ToSingle(ms1.Text);
            m_MoveSLinkPara[2] = Convert.ToSingle(ms2.Text);
            m_MoveSLinkPara[3] = Convert.ToSingle(ms3.Text);

            m_MoveSLinkPara[4] = Convert.ToSingle(ms4.Text);
            m_MoveSLinkPara[5] = Convert.ToSingle(ms5.Text);
            m_MoveSLinkPara[6] = Convert.ToSingle(ms6.Text);
            m_MoveSLinkPara[7] = Convert.ToSingle(ms7.Text);

            m_MoveSLinkPara[8] = Convert.ToSingle(ms8.Text);
            m_MoveSLinkPara[9] = Convert.ToSingle(ms9.Text);
            m_MoveSLinkPara[10] = Convert.ToSingle(ms10.Text);
            m_MoveSLinkPara[11] = Convert.ToSingle(ms11.Text);
            m_LinkPos = Convert.ToSingle(TextBox38.Text);
            m_LinkSp = Convert.ToSingle(TextBox37.Text);
        }

        private void ShowPswtichList()
        {
            while (this.DataGridView1.Rows.Count != 1)
            {
                this.DataGridView1.Rows.RemoveAt(1);
            }

            for (int iItem = 0; iItem < MAX_POINT; iItem++)
            {
                int index = this.DataGridView1.Rows.Add();

                this.DataGridView1.Rows[index].Cells[0].Value = index + 1;

                this.DataGridView1.Rows[index].Cells[1].Value = iItem + 1000 * (1 - Math.Cos(iItem * Math.PI / 180)); ;

            }
        }



        CustomMessageBox customMessageBox = new CustomMessageBox();
        public int controlReturn = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {

            StringBuilder[]str = new StringBuilder[4];
            str[0] = new StringBuilder("no reply");
            str[1] = new StringBuilder();
            str[2] = new StringBuilder();
            if (zmcaux.ZAux_GetControllerInfo(g_handle, str[0], str[1], str[2]) != 0 || str[0].ToString() == "no reply")
            {
                this.Text = "未连接";
            }
            else {
                this.Text = "已连接";
            }

            
            if (controlReturn != 0) {
                string message = string.Format("操作失败,错误码{0:D}",controlReturn);

                if (customMessageBox.messageBoxClosed == false) {
                    customMessageBox.SetDisplayText(message);
                    customMessageBox.ShowDialog();
                }
                controlReturn = 0;
            }
        }
        private void ControllerInit(){

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
