using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cszmcaux;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO.Ports;

namespace test_Pswitch
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
       private void C_connect_type_TextChanged(object sender, EventArgs e)
       {
            string connectType = C_connect_type.Text;
            cc = new ConnectContext(connectType);
       }
        public int m_AxisNum = 0;
	    public int m_PSO_dir = -1;
	    public int m_POS_out = 0;
	    public float m_End_Pos = 5000.0f;
	    public int m_Timer_Num = 1;
	    public int m_POS_EndTable = 10;
	    public float m_Start_Pos = 0.0f;
	    public int m_POS_StartTable = 0;
	    public int m_Timer_Valid = 5000;
	    public int m_Timer_Cycle = 10000;
	    public int m_POS_OutStatus = 1;
	    public bool m_POS_IfOpen = true;
	    public bool m_Timer_IfOpen = true;
	    public bool m_Regist_IfOpen = false;
	    public int iTotalPoint = 1000;
	    public int m_RegistAxis = 1;
	    public int m_RegistCount = 0;
	    public int RegistMode = 1;
        public int m_RegistAtype = 1;

        public float[] fPointPos = new float[1000];

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
            if (connectType == "")
            {
                MessageBox.Show("请先选择连接方式");
                return;
            }
            C_Ip_Address.Items.Clear();
            cc = new ConnectContext(connectType);
            string[] scanResult = cc.ScanConnect();
            for (int i = 0; i < scanResult.Length; i++)
            {
                C_Ip_Address.Items.Insert(i, scanResult[i].ToString());
            }
        }



        private void OnOpen(object sender, EventArgs e)
        {
            string Buffer;
            Buffer = C_Ip_Address.Text;
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
        private void Form1_Load(object sender, EventArgs e)             //初始化
        {
            ComboBox1.Items.Clear();
            ComboBox1.Items.Add("1 Z脉冲上升沿锁存");
            ComboBox1.Items.Add("2 Z脉冲下降沿锁存");
            ComboBox1.Items.Add("3 R0上升沿锁存");
            ComboBox1.Items.Add("4 R0下降沿锁存");
            ComboBox1.Items.Add("14 R1上升沿锁存");
            ComboBox1.Items.Add("15 R1下降沿锁存");
            ComboBox1.Items.Add("18 R2上升沿锁存");
            ComboBox1.Items.Add("19 R2下降沿锁存");
            ComboBox1.Items.Add("20 R3上升沿锁存");
            ComboBox1.Items.Add("21 R3下降沿锁存");
            ComboBox1.SelectedIndex = 0;
            start_add();
        }


       

        private void start_add()            //初始化DataGridView数据
        {   string[] tempstr = new string[2];
            int i;
            float tempvalue = 0;
            for (i = 0; i < iTotalPoint; i++)
            {
                tempvalue = 100 * (i + 1);
                tempstr[0] = i.ToString();
                tempstr[1] = tempvalue.ToString();
                DataGridView1.Rows.Add(tempstr);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)        //刷新状态显示
        {
            float AxisDpos = 0;
            int iret = 0;
            UInt32 Op_status = 0;

            updata_value();
            if (RadioButton1.Checked == true)       //位置比较是否开启
                m_POS_IfOpen = false;
            if (RadioButton2.Checked == true)
                m_POS_IfOpen = true;
            if (RadioButton4.Checked == true)       //定时器输出反转是否开启
                m_Timer_IfOpen = false;
            if (RadioButton3.Checked == true)
                m_Timer_IfOpen = true;
            iret = zmcaux.ZAux_Direct_GetDpos(g_handle, m_AxisNum, ref AxisDpos);
            iret = zmcaux.ZAux_Direct_GetOp(g_handle, m_POS_out, ref Op_status);
            label_dpos.Text = "轴" + m_AxisNum.ToString() + "坐标：  " + AxisDpos.ToString();
            label_out.Text = "输出口" + m_POS_out.ToString() + "状态： 关闭";
            if (Op_status == 1)
                label_out.Text = "输出口" + m_POS_out.ToString() + "状态： 打开";
   
        }

        private void updata_value()                 //更新参数
        {
            m_AxisNum = Convert.ToInt32(TextBox2.Text);
            m_POS_out = Convert.ToInt32(TextBox3.Text);
            m_POS_StartTable = Convert.ToInt32(TextBox4.Text);
            m_POS_EndTable = Convert.ToInt32(TextBox5.Text);
            m_POS_OutStatus = Convert.ToInt32(TextBox6.Text);
            m_PSO_dir = Convert.ToInt32(TextBox7.Text);
            m_Timer_Cycle = Convert.ToInt32(TextBox10.Text);
            m_Timer_Valid = Convert.ToInt32(TextBox9.Text);
            m_Timer_Num = Convert.ToInt32(TextBox8.Text);
            m_Start_Pos = Convert.ToSingle(TextBox12.Text);
            m_End_Pos = Convert.ToSingle(TextBox11.Text);
            m_RegistAxis = Convert.ToInt32(TextBox1.Text);
            m_RegistAtype = Convert.ToInt32(textBox13.Text);

            for (int i = 0; i < iTotalPoint; i++)
            {
                fPointPos[i] = Convert.ToSingle(DataGridView1.Rows[i].Cells["位置"].Value);
            }
       
        }

        private void Button2_Click(object sender, EventArgs e)          //运动
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int iret = 0;
            iret = zmcaux.ZAux_Direct_SetAtype(g_handle, m_AxisNum, 1);				//必须是编码器轴才可以锁存
            iret = zmcaux.ZAux_Direct_SetUnits(g_handle, m_AxisNum, 100);
            iret = zmcaux.ZAux_Direct_SetSpeed(g_handle, m_AxisNum, 100);
            iret = zmcaux.ZAux_Direct_SetAccel(g_handle, m_AxisNum, 2000);
            iret = zmcaux.ZAux_Direct_HwPswitch2(g_handle, m_AxisNum, 2, 0, 0, 0, 0, 0, 0);     //清除前面的比较输出指令
            iret = zmcaux.ZAux_Direct_SetOp(g_handle, m_POS_out, 0);
            if (m_POS_IfOpen == false)          //比较完成一次后需要重新调用HwPswitch  
            {
                iret = zmcaux.ZAux_Direct_SetTable(g_handle, m_POS_StartTable, m_POS_EndTable, fPointPos);          //将比较点填入TABLE
                if (iret != 0)
                {
                    string tempstr;
                    tempstr = "SetTable失败 返回值：" + iret.ToString();
                    MessageBox.Show(tempstr, "提示");
                    return;
                }
                iret = zmcaux.ZAux_Direct_HwPswitch2(g_handle, m_AxisNum, 1, m_POS_out, m_POS_OutStatus, m_POS_StartTable, m_POS_EndTable, m_PSO_dir, 0);
                if (iret != 0)
                {
                    string tempstr;
                    tempstr = "HwPswitch2失败 返回值：" + iret.ToString();
                    MessageBox.Show(tempstr, "提示");
                    return;
                }
            }
            else 
            {
                iret = 0;
                iret = zmcaux.ZAux_Direct_HwPswitch2(g_handle, m_AxisNum, 2, 0, 0, 0, 0, 0, 0);         //清除比较输出指令
                if (iret != 0)
                {
                    string tempstr;
                    tempstr = "HwPswitch2失败 返回值：" + iret.ToString();
                    MessageBox.Show(tempstr, "提示");
                    return;
                }
            }


            int tempoutstatus = 0;
            if (m_POS_OutStatus == 0)
                tempoutstatus = 1;
            else
                tempoutstatus = 0;
            if (m_Timer_IfOpen == false)
            {
                iret = zmcaux.ZAux_Direct_HwTimer(g_handle, 2, m_Timer_Cycle, m_Timer_Valid, m_Timer_Num, tempoutstatus, m_POS_out);
                if (iret != 0)
                {
                    string tempstr;
                    tempstr = "HwTimer失败 返回值：" + iret.ToString();
                    MessageBox.Show(tempstr, "提示");
                    //return;
                }
            }
            else
            {
                iret = zmcaux.ZAux_Direct_HwTimer(g_handle, 0, m_Timer_Cycle, m_Timer_Valid, m_Timer_Num, tempoutstatus, m_POS_out);
                if (iret != 0)
                {
                    string tempstr;
                    tempstr = "HwTimer失败 返回值：" + iret.ToString();
                    MessageBox.Show(tempstr, "提示");
                    //return;
                }
            }
            zmcaux.ZAux_Trigger(g_handle);
            iret = zmcaux.ZAux_Direct_SetDpos(g_handle, m_AxisNum, 0);
            iret = zmcaux.ZAux_Direct_Single_MoveAbs(g_handle, m_AxisNum, m_Start_Pos);
            iret = zmcaux.ZAux_Direct_Single_MoveAbs(g_handle, m_AxisNum, m_End_Pos);
        
        }

        private void Button3_Click(object sender, EventArgs e)              //停止
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int iret = 0;
            iret = zmcaux.ZAux_Direct_Single_Cancel(g_handle, m_AxisNum, 2);
        }

        private void Button1_Click(object sender, EventArgs e)      //启动锁存
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int iret = 0;
            if (m_Regist_IfOpen == false)
            {
                m_RegistCount = 0;

                zmcaux.ZAux_Direct_SetAtype(g_handle, m_RegistAxis, m_RegistAtype);				//必须是编码器轴才可以锁存

                TextBox1.Enabled = false;
                textBox13.Enabled = false;

                int ReglistListSel = ComboBox1.SelectedIndex;
                if (ReglistListSel >= 0 && ReglistListSel <= 3)
                {
                    RegistMode = ReglistListSel + 1;
                }
                else if (ReglistListSel == 4 || ReglistListSel == 5)
                {
                    RegistMode = 10 + ReglistListSel;
                }
                else if (ReglistListSel > 5 || ReglistListSel < 9)
                {
                    RegistMode = 12 + ReglistListSel;
                }
                iret = zmcaux.ZAux_Direct_Regist(g_handle, m_RegistAxis, RegistMode);
                timer2.Start();

                m_Regist_IfOpen = true;
                ComboBox1.Enabled = false;
                Button1.Text = "停止锁存";

            }
            else
            {
                timer2.Stop();
                m_Regist_IfOpen = false;
                ComboBox1.Enabled = true;
                TextBox1.Enabled = true;
                textBox13.Enabled = true;
                Button1.Text = "启动锁存";
                Label3.Text = "";
                DataGridView2.Rows.Clear();
               
            }
        }

        private void timer2_Tick(object sender, EventArgs e)                    //定时器刷新
        {
            int iret = 0;
			int MarkStatus = 0;
			float RegistPos=0;
			if(RegistMode >= 0 && RegistMode <= 4)
			{
				iret = zmcaux.ZAux_Direct_GetMark(g_handle,m_RegistAxis,ref MarkStatus);	
			}
			else if(RegistMode >= 14 || RegistMode < 16)
			{
                iret = zmcaux.ZAux_Direct_GetMarkB(g_handle, m_RegistAxis, ref MarkStatus);	
			}
			else if(RegistMode >= 18 || RegistMode < 20)
			{	
				float tempc=0;
                iret = zmcaux.ZAux_Direct_GetParam(g_handle, "MARKC", m_RegistAxis, ref tempc);	
				MarkStatus = (int)tempc;
			}
			else if(RegistMode >= 20 || RegistMode < 22)
			{
				float tempd=0;
                iret = zmcaux.ZAux_Direct_GetParam(g_handle, "MARKD", m_RegistAxis, ref tempd);	
				MarkStatus = (int)tempd;
			}


            if (MarkStatus == -1)
            {
                if (RegistMode >= 0 && RegistMode <= 4)
                {
                    iret = zmcaux.ZAux_Direct_GetRegPos(g_handle, m_RegistAxis, ref RegistPos);				//获取锁存位置
                }
                else if (RegistMode >= 14 || RegistMode < 16)
                {
                    iret = zmcaux.ZAux_Direct_GetRegPosB(g_handle, m_RegistAxis, ref RegistPos);
                }
                else if (RegistMode >= 18 || RegistMode < 20)
                {
                    iret = zmcaux.ZAux_Direct_GetParam(g_handle, "REG_POSC", m_RegistAxis, ref RegistPos);
                }
                else if (RegistMode >= 20 || RegistMode < 22)
                {
                    iret = zmcaux.ZAux_Direct_GetParam(g_handle, "REG_POSD", m_RegistAxis, ref RegistPos);
                }
                string[] tempstr = new string[2];
                tempstr[0] = m_RegistCount.ToString();
                tempstr[1] = RegistPos.ToString();
                DataGridView2.Rows.Add(tempstr);
                m_RegistCount = m_RegistCount + 1;
                iret = zmcaux.ZAux_Direct_Regist(g_handle, m_RegistAxis, RegistMode);            //重新触发锁存
            }
            string temps;
            temps = "锁存触发状态：" + MarkStatus.ToString() + "  次数： " + m_RegistCount.ToString();
            Label3.Text = temps;
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
