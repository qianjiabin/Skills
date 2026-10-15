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

namespace test_fanction7
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
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

        

        private void OnSelect(object sender, EventArgs e)
        {
            C_Ip_Address.SelectedIndex = -1;
        }

        ConnectContext cc;
        private void OnScan(object sender, EventArgs e)
        {

            string connectType = C_connect_type.Text;
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

            cc.CloseConnect(g_handle);
            g_handle = (IntPtr)0;
            this.Text = "未连接";
            timer1.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)                 //下载BAS文件到控制器ROM
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

        private void button1_Click(object sender, EventArgs e)      //动作1
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
	            float[] array =new float[3] ;
	            array[0] =  Convert.ToSingle(array_set1.Text);
	            array[1] =  Convert.ToSingle(array_set2.Text);
	            array[2] =  Convert.ToSingle(array_set3.Text);

	            zmcaux.ZAux_Direct_SetUserArray(g_handle,"array_para",0,3,array);		//发送数据

	            float cmd = 1;
                zmcaux.ZAux_Direct_SetUserVar(g_handle, "cmd_data", cmd);          //发送命令
            }
        }

        private void button2_Click(object sender, EventArgs e)      //动作2
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                float[] array = new float[3];
                array[0] = Convert.ToSingle(array_set4.Text);
                array[1] = Convert.ToSingle(array_set5.Text);
                array[2] = Convert.ToSingle(array_set6.Text);
                zmcaux.ZAux_Direct_SetUserArray(g_handle, "array_para", 5, 3, array);		//发送数据

                float cmd = 2;
                zmcaux.ZAux_Direct_SetUserVar(g_handle, "cmd_data", cmd);                //发送命令
            }
        }

        private void button3_Click(object sender, EventArgs e)      //停止
        {

            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, 0, 2);        //取消主轴运动
            }
        }

        private void button4_Click(object sender, EventArgs e)      //位置清零
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    zmcaux.ZAux_Direct_SetDpos(g_handle, i, 0);
                }
            }
        }

        private void button_read1_Click(object sender, EventArgs e)     //读取全全局数组值与变量
        {
            int ret = 0;
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                float[] m_ArrayShow = new float[10];
                float cmd_data = 0;
                ret = zmcaux.ZAux_Direct_GetUserVar(g_handle, "cmd_data", ref cmd_data);          //读取全局变量
                ret += zmcaux.ZAux_Direct_GetUserArray(g_handle, "array_para", 0, 9, m_ArrayShow);		//读取全局数组,注意BASIC定义的数组最好比PC端实际数据大1，BASIC数组最后一个元素访问时会越界

                if (ret == 0)
                {
                    m_cmd.Text = cmd_data.ToString();
                    array_get1.Text = m_ArrayShow[0].ToString();
                    array_get2.Text = m_ArrayShow[1].ToString();
                    array_get3.Text = m_ArrayShow[2].ToString();
                    array_get4.Text = m_ArrayShow[5].ToString();
                    array_get5.Text = m_ArrayShow[6].ToString();
                    array_get6.Text = m_ArrayShow[7].ToString();
                }
            }
        }


        private void timer1_Tick(object sender, EventArgs e)        //定时器
        {
            float[] curpos = new float[5];

            for (int i = 0; i < 3; i++)
            {
                zmcaux.ZAux_Direct_GetDpos(g_handle, i, ref curpos[i]);
            }

            label_x.Text = "X轴:" + curpos[0];
            label_y.Text = "Y轴:" + curpos[1];
            label_z.Text = "Z轴:" + curpos[2];
        }

        private void button_read2_Click(object sender, EventArgs e)             //FLASH块读取
        {
            int ret = 0;
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            ushort m_FlashStart = Convert.ToUInt16(flash_start.Text);
            float[] m_FlashShow = new float[4];
            uint getnum = 0;
            ret = zmcaux.ZAux_FlashReadf(g_handle, m_FlashStart, 4, m_FlashShow, ref getnum);
            if (ret == 0)
            {
                flash_get1.Text = m_FlashShow[0].ToString();
                flash_get2.Text = m_FlashShow[1].ToString();
                flash_get3.Text = m_FlashShow[2].ToString();
                flash_get4.Text = m_FlashShow[3].ToString();
            }

        }

        private void button_write2_Click(object sender, EventArgs e)            //FLASH块写入
        {
            int ret = 0;
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            ushort m_FlashStart = Convert.ToUInt16(flash_start.Text);
            float[] m_FlashSet = new float[4];

            m_FlashSet[0] = Convert.ToSingle(flash_set1.Text);
            m_FlashSet[1] = Convert.ToSingle(flash_set2.Text);
            m_FlashSet[2] = Convert.ToSingle(flash_set3.Text);
            m_FlashSet[3] = Convert.ToSingle(flash_set4.Text);
            ret = zmcaux.ZAux_FlashWritef(g_handle, m_FlashStart, 4, m_FlashSet);

        }

        private void button_read3_Click(object sender, EventArgs e)             //MODBUS读取
        {
            int ret = 0;
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }

            ushort m_ModbusStart = Convert.ToUInt16(modbus_start.Text);
            if (radioButton1.Checked)       //BIT
            {
                byte[] pdata = new byte[1];         
                ret = zmcaux.ZAux_Modbus_Get0x(g_handle, m_ModbusStart, 4, pdata);   //pdata按位存储MODBUS_BIT        
                modbus_get1.Text = (pdata[0] & 0x01).ToString();               
                modbus_get2.Text = (pdata[0]>>1 & 0x01).ToString();
                modbus_get3.Text = (pdata[0]>> 2 & 0x01).ToString();
                modbus_get4.Text = (pdata[0]>>3 & 0x01).ToString();
            }
            else if (radioButton2.Checked)  //REG
            {
                UInt16[] pdata = new UInt16[4];
                ret = zmcaux.ZAux_Modbus_Get4x(g_handle, m_ModbusStart, 4, pdata);
                modbus_get1.Text = pdata[0].ToString();
                modbus_get2.Text = pdata[1].ToString();
                modbus_get3.Text = pdata[2].ToString();
                modbus_get4.Text = pdata[3].ToString();      
            }
            else if (radioButton3.Checked)  //LONG
            {
                int[] pdata = new int[4];
                ret = zmcaux.ZAux_Modbus_Get4x_Long(g_handle, m_ModbusStart, 4, pdata);
                modbus_get1.Text = pdata[0].ToString();
                modbus_get2.Text = pdata[1].ToString();
                modbus_get3.Text = pdata[2].ToString();
                modbus_get4.Text = pdata[3].ToString();  
            }
            else if (radioButton4.Checked)  //IEEE
            {
                float[] pdata = new float[4];
                ret = zmcaux.ZAux_Modbus_Get4x_Float(g_handle, m_ModbusStart, 4, pdata);
                modbus_get1.Text = pdata[0].ToString();
                modbus_get2.Text = pdata[1].ToString();
                modbus_get3.Text = pdata[2].ToString();
                modbus_get4.Text = pdata[3].ToString();  
            }
        }

        private void button_write3_Click(object sender, EventArgs e)             //MODBUS写入
        {
            int ret = 0;
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            ushort m_ModbusStart = Convert.ToUInt16(modbus_start.Text);
            if (radioButton1.Checked)       //BIT
            {
                float[] idata = new float[4];
                idata[0] = Convert.ToSingle(modbus_set1.Text);
                idata[1] = Convert.ToSingle(modbus_set2.Text);
                idata[2] = Convert.ToSingle(modbus_set3.Text);
                idata[3] = Convert.ToSingle(modbus_set4.Text);

                byte[] pdata = new byte[1];
                pdata[0] = (byte)0;
                for(int i =0;i<4;i++)
                {
                    if (idata[i] != 0)
                    {
                        pdata[0] += (byte)(0x1 << i );               
                    }


                }

                ret = zmcaux.ZAux_Modbus_Set0x(g_handle, m_ModbusStart, 4, pdata);   //pdata按位存储MODBUS_BIT        

            }
            else if (radioButton2.Checked)  //REG
            {
                UInt16[] pdata = new UInt16[4];
                pdata[0] = Convert.ToUInt16(modbus_set1.Text);
                pdata[1] = Convert.ToUInt16(modbus_set2.Text);
                pdata[2] = Convert.ToUInt16(modbus_set3.Text);
                pdata[3] = Convert.ToUInt16(modbus_set4.Text);

                ret = zmcaux.ZAux_Modbus_Set4x(g_handle, m_ModbusStart, 4, pdata);

            }
            else if (radioButton3.Checked)  //LONG
            {
                int[] pdata = new int[4];
                pdata[0] = Convert.ToInt32(modbus_set1.Text);
                pdata[1] = Convert.ToInt32(modbus_set2.Text);
                pdata[2] = Convert.ToInt32(modbus_set3.Text);
                pdata[3] = Convert.ToInt32(modbus_set4.Text);

                ret = zmcaux.ZAux_Modbus_Set4x_Long(g_handle, m_ModbusStart, 4, pdata);
            }
            else if (radioButton4.Checked)  //IEEE
            {
                float[] pdata = new float[4];
                pdata[0] = Convert.ToSingle(modbus_set1.Text);
                pdata[1] = Convert.ToSingle(modbus_set2.Text);
                pdata[2] = Convert.ToSingle(modbus_set3.Text);
                pdata[3] = Convert.ToSingle(modbus_set4.Text);

                ret = zmcaux.ZAux_Modbus_Set4x_Float(g_handle, m_ModbusStart, 4, pdata);
            }
        }

        private void button_read4_Click(object sender, EventArgs e)             //VR读取
        {
            int ret = 0;
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }

            ushort m_VrStart = Convert.ToUInt16(vr_start.Text);
            float[] m_VrShow = new float[4];
            ret = zmcaux.ZAux_Direct_GetVrf(g_handle, m_VrStart, 4, m_VrShow);

            vr_get1.Text = m_VrShow[0].ToString();
            vr_get2.Text = m_VrShow[1].ToString();
            vr_get3.Text = m_VrShow[2].ToString();
            vr_get4.Text = m_VrShow[3].ToString();
        }

        private void button_write4_Click(object sender, EventArgs e)            //VR写入
        {
            int ret = 0;
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }

            ushort m_VrStart = Convert.ToUInt16(vr_start.Text);
            float[] m_VrSet = new float[4];
            m_VrSet[0] = Convert.ToSingle(vr_set1.Text);
            m_VrSet[1] = Convert.ToSingle(vr_set2.Text);
            m_VrSet[2] = Convert.ToSingle(vr_set3.Text);
            m_VrSet[3] = Convert.ToSingle(vr_set4.Text);

            ret = zmcaux.ZAux_Direct_SetVrf(g_handle, m_VrStart, 4, m_VrSet);
        }

        private void button_read5_Click(object sender, EventArgs e)             //TABLE读取
        {
            int ret = 0;
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            ushort m_TableStart = Convert.ToUInt16(table_strat.Text);
            float[] m_TableShow = new float[4];
            ret = zmcaux.ZAux_Direct_GetTable(g_handle, m_TableStart, 4, m_TableShow);

            table_get1.Text = m_TableShow[0].ToString();
            table_get2.Text = m_TableShow[1].ToString();
            table_get3.Text = m_TableShow[2].ToString();
            table_get4.Text = m_TableShow[3].ToString();
        }

        private void button_write5_Click(object sender, EventArgs e)            //TABLE写入
        {
            int ret = 0;
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            ushort m_TableStart = Convert.ToUInt16(table_strat.Text);
            float[] m_TableSet = new float[4];
            m_TableSet[0] = Convert.ToSingle(table_set1.Text);
            m_TableSet[1] = Convert.ToSingle(table_set2.Text);
            m_TableSet[2] = Convert.ToSingle(table_set3.Text);
            m_TableSet[3] = Convert.ToSingle(table_set4.Text);

            ret = zmcaux.ZAux_Direct_SetTable(g_handle, m_TableStart, 4, m_TableSet);
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
