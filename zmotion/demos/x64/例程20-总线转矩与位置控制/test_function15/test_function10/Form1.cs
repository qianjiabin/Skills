using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using cszmcaux;

namespace test_function8
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
        public bool g_basflag = false;  //BAS文件加载标志位
        public int g_InitStatus = -1;   //总线初始化完成状态	0-失败 1-成功  -1-初始化未完成
        public float Bus_type = -1;     //BAS文件中变量判断总线类型，也作为BAS文件是否下载成功判断
        public bool if_home = false;

        public int m_axisnum = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void C_Open_Com_Click(object sender, EventArgs e)       //串口连接控制器
        {
            if (g_handle == (IntPtr)0)
            {
                C_Close_Card_Click(sender, e);
            }

            UInt32 comid = Convert.ToUInt32(C_COM_ID.Text);
            zmcaux.ZAux_OpenCom(comid, out g_handle);
            if ((int)g_handle != 0)
            {
                this.Text = "已连接";
                timer1.Enabled = true;
                C_Move_Axis_TextChanged(sender, e);

                int ret = zmcaux.ZAux_Direct_GetUserVar(g_handle, "BUS_TYPE", ref Bus_type);						//读取BAS文件中的变量判断是否有加载BAS文件
                if (ret == 0 && Bus_type != -1)
                {
                    g_basflag = true;			//文件已经加载
                }
            }
        }

        private void C_Ip_Address_DropDown(object sender, EventArgs e)    //下拉搜索控制器IP
        {
            int i, num;
            string[] sArray;
            StringBuilder buffer = new StringBuilder(10240);
            string buff = "";
            zmcaux.ZAux_SearchEthlist(buffer, 10230, 200);
            buff += buffer;
            sArray = buff.Split(' ');
            num = buff.Split(' ').Length;

            C_Ip_Address.Items.Clear();

            C_Ip_Address.Items.Insert(0, "127.0.0.1");
            for (i = 0; i < num - 1; i++)
            {
                C_Ip_Address.Items.Insert(i + 1, sArray[i].ToString());
            }
        }

        private void C_Open_Eth_Click(object sender, EventArgs e)       //网口连接控制器
        {
            if (g_handle == (IntPtr)0)
            {
                C_Close_Card_Click(sender, e);
            }
            zmcaux.ZAux_OpenEth(C_Ip_Address.Text, out g_handle);
            if (g_handle != (IntPtr)0)
            {
                this.Text = "已连接";
                timer1.Enabled = true;
                C_Move_Axis_TextChanged(sender,e);

                int ret = zmcaux.ZAux_Direct_GetUserVar(g_handle, "BUS_TYPE",ref Bus_type);						//读取BAS文件中的变量判断是否有加载BAS文件
                if (ret == 0 && Bus_type != -1)
                {
                    g_basflag = true;			//文件已经加载
                }

            }
        }

        private void C_Pci_Id_DropDown(object sender, EventArgs e)      //下拉搜索PCI卡
        {
            int ipcicardes = zmcaux.ZAux_GetMaxPciCards();
            for (int i = 0; i < ipcicardes - 1; i++)
            {
                C_Ip_Address.Items.Insert(i,i.ToString());
            }
        }

        private void C_Open_Pci_Click(object sender, EventArgs e)       //连接PCI卡
        {
            if (g_handle == (IntPtr)0)
            {
                C_Close_Card_Click(sender, e);
            }
            zmcaux.ZAux_OpenPci(Convert.ToUInt32(C_Pci_Id.Text), out g_handle);
            if (g_handle != (IntPtr)0)
            {
                this.Text = "已连接";
                timer1.Enabled = true;
                C_Move_Axis_TextChanged(sender, e);

                int ret = zmcaux.ZAux_Direct_GetUserVar(g_handle, "BUS_TYPE", ref Bus_type);						//读取BAS文件中的变量判断是否有加载BAS文件
                if (ret == 0 && Bus_type != -1)
                {
                    g_basflag = true;			//文件已经加载
                }
            }
        }

        private void C_Close_Card_Click(object sender, EventArgs e)     //断开控制器连接
        {
            //断开链接
            timer1.Enabled = false;
            zmcaux.ZAux_Close(g_handle);
            g_handle = (IntPtr)0;
            this.Text = "未连接";
        }

        private void C_Download_Click(object sender, EventArgs e)       //下载BAS文件到控制器
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
                    C_BasFile.Text = strFilePath;
                    ret = zmcaux.ZAux_BasDown(g_handle, strFilePath, 1);               //下载到ROM
                    if (ret != 0)
                    {
                        MessageBox.Show("文件下载失败!", "提示");
                    }
                }
            }
        }

        private void C_BusInit_Click(object sender, EventArgs e)            ////重新初始化总线，下载ROM时程序会自动初始化一次
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
            }
            else
            {
                int ret;
                if (g_basflag && (g_InitStatus != -1))				//-1可能正在执行初始化
                {
                    g_InitStatus = -1;
                    StringBuilder buffer = new StringBuilder(10240);
                    C_Bus_Status.Text = "初始化未完成";
                    ret = zmcaux.ZAux_Execute(g_handle, "RUNTASK 1,Ecat_Init", buffer, 0);			//任务1重新运行BAS中的初始化函数
                    if (ret != 0)
                    {
                        return;
                    }

                }
                else
                {
                    MessageBox.Show("Bas文件未加载");
                    return;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)            //定时器
        {
            int ret = 0;
            float[] f_AxisPara = new float[10];
            int[] i_AxisPara =  new int[10];
            if (g_handle != (IntPtr)0)
            {
                m_axisnum = Convert.ToInt32(C_Move_Axis.Text);

                ret += zmcaux.ZAux_Direct_GetAxisEnable(g_handle, m_axisnum, ref i_AxisPara[0]);
                ret += zmcaux.ZAux_Direct_GetMpos(g_handle, m_axisnum, ref f_AxisPara[0]);
                ret += zmcaux.ZAux_Direct_GetDpos(g_handle, m_axisnum, ref f_AxisPara[1]);
                ret += zmcaux.ZAux_Direct_GetAxisStatus(g_handle, m_axisnum, ref i_AxisPara[1]);
                ret += zmcaux.ZAux_Direct_GetIfIdle(g_handle, m_axisnum, ref i_AxisPara[2]);
                ret += zmcaux.ZAux_BusCmd_GetDriveTorque(g_handle, (UInt32)m_axisnum, ref i_AxisPara[3]);
                if (ret == 0)
                {
 
                    C_AxisEnable.Text = (i_AxisPara[0] == 0)?"off":"on";
                    C_AxisMpos.Text = f_AxisPara[0].ToString();
                    C_AxisDpos.Text = f_AxisPara[1].ToString();
                    C_AxisStatus.Text = i_AxisPara[1].ToString();
                    C_AxisIdle.Text = i_AxisPara[2].ToString();
                    CUR_TORQUE.Text = i_AxisPara[3].ToString();
                }

                if (((i_AxisPara[1] & 64) == 64) && (if_home == false))			//第6位是否被置1
                {
                    C_HomeStatus.Text = "回零中";
                    if_home = true;
                }
                else if ((i_AxisPara[1] == 0) && if_home)
                {
                    uint homeStatus = 0;
                    ret = zmcaux.ZAux_BusCmd_GetHomeStatus(g_handle, (uint)m_axisnum, ref homeStatus);
                    C_HomeStatus.Text = (homeStatus == 1) ? "回零完成" : "回零未完成";
                    if_home = false;
                }

                if (g_basflag && g_InitStatus == -1)				//已经加载文件并且正在初始化 读取状态
                {
                    float tempstatus = -1;
                    int m_BusNodeNum = 0;
                    float m_BusAxisNum = 0;
                    ret += zmcaux.ZAux_Direct_GetUserVar(g_handle, "BUS_TYPE", ref Bus_type);						//读取BAS文件中的变量判断总线类型
                    ret += zmcaux.ZAux_Direct_GetUserVar(g_handle, "Bus_InitStatus", ref tempstatus);				//读取BAS文件中的变量判断总线初始化完成状态
                    ret += zmcaux.ZAux_BusCmd_GetNodeNum(g_handle, 0, ref m_BusNodeNum);							//读取槽位0上节点个数。
                    ret += zmcaux.ZAux_Direct_GetUserVar(g_handle, "Bus_TotalAxisnum", ref m_BusAxisNum);			//读取BAS文件中的变量判断扫描的总轴数


                    g_InitStatus = (int)tempstatus;
                    if (ret == 0 && g_InitStatus != -1)		//初始化完成刷新状态	
                    {
                        C_Bus_Status.Text = (g_InitStatus == 1) ? "初始化成功" : "初始化失败";
                        C_NodeNum.Text = m_BusNodeNum.ToString();
                        C_AxisNum.Text = m_BusAxisNum.ToString();
                    }
                }                
            }
        }

        private void C_Move_Axis_TextChanged(object sender, EventArgs e)              //轴被修改
        {
            if (g_handle == (IntPtr)0)
            {
                return;
            }
            int ret = 0;
            float[] f_AxisPara = new float[10];
            int m_atype = 0;
            m_axisnum = Convert.ToInt32(C_Move_Axis.Text);
            ret += zmcaux.ZAux_Direct_GetUnits(g_handle, m_axisnum, ref f_AxisPara[0]);
            ret += zmcaux.ZAux_Direct_GetSpeed(g_handle, m_axisnum, ref f_AxisPara[1]);
            ret += zmcaux.ZAux_Direct_GetAccel(g_handle, m_axisnum, ref f_AxisPara[2]);
            ret += zmcaux.ZAux_Direct_GetAtype(g_handle, m_axisnum, ref m_atype);

            if (ret == 0)
            {
                C_AxisType.Text = m_atype.ToString();
                C_AxisUnits.Text =f_AxisPara[0].ToString();
                C_AxisSpeed.Text =f_AxisPara[1].ToString();
                C_AxisAcc.Text = f_AxisPara[2].ToString();
            }
        }

        private void C_Button_Enable_Click(object sender, EventArgs e)      //切换使能
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int ret = 0;

            if (C_AxisEnable.Text == "on")
            {
                ret = zmcaux.ZAux_Direct_SetAxisEnable(g_handle, m_axisnum, 0);
            }
            else
            {
                ret = zmcaux.ZAux_Direct_SetAxisEnable(g_handle, m_axisnum, 1);
            }
        }

        private void C_Button_Clear_Click(object sender, EventArgs e)       //清除轴报警
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int ret = 0;
            ret = zmcaux.ZAux_BusCmd_DriveClear(g_handle, (uint)m_axisnum, 0);
        }

        private void C_Button_Fwd_Click(object sender, EventArgs e)         //正转
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int ret = 0;
            ret = zmcaux.ZAux_Direct_SetUnits(g_handle, m_axisnum, Convert.ToSingle(C_AxisUnits.Text));
            ret = zmcaux.ZAux_Direct_SetSpeed(g_handle, m_axisnum, Convert.ToSingle(C_AxisSpeed.Text));
            ret = zmcaux.ZAux_Direct_SetAccel(g_handle, m_axisnum, Convert.ToSingle(C_AxisAcc.Text));
            ret = zmcaux.ZAux_Direct_Single_Vmove(g_handle, m_axisnum, 1);	
        }

        private void C_Button_Rev_Click(object sender, EventArgs e)         //反转
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int ret = 0;
            ret = zmcaux.ZAux_Direct_SetUnits(g_handle, m_axisnum, Convert.ToSingle(C_AxisUnits.Text));
            ret = zmcaux.ZAux_Direct_SetSpeed(g_handle, m_axisnum, Convert.ToSingle(C_AxisSpeed.Text));
            ret = zmcaux.ZAux_Direct_SetAccel(g_handle, m_axisnum, Convert.ToSingle(C_AxisAcc.Text));
            ret = zmcaux.ZAux_Direct_Single_Vmove(g_handle, m_axisnum, -1);	
        }

        private void C_Button_Stop_Click(object sender, EventArgs e)        //停止运动
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int ret = 0;
            ret = zmcaux.ZAux_Direct_Single_Cancel(g_handle, m_axisnum, 2);
        }

        private void C_HomeStart_Click(object sender, EventArgs e)      //驱动器回零
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            if (Bus_type == 0)
            {
                int ret = 0;
                ret = zmcaux.ZAux_Direct_SetSpeed(g_handle, m_axisnum, Convert.ToSingle(C_HomeSp1.Text));
                ret = zmcaux.ZAux_Direct_SetCreep(g_handle, m_axisnum, Convert.ToSingle(C_HomeSp2.Text));
                ret = zmcaux.ZAux_BusCmd_SetDatumOffpos(g_handle, (uint)m_axisnum, Convert.ToSingle(C_HomeOffset.Text));
                ret = zmcaux.ZAux_BusCmd_Datum(g_handle, (uint)m_axisnum, Convert.ToUInt32(C_HomeMode.Text));
            }
            else
            {
                MessageBox.Show("rtex不支持");
            }
        }

        private void C_HomeStop_Click(object sender, EventArgs e)       //取消回零
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int ret = 0;
            ret = zmcaux.ZAux_Direct_Single_Cancel(g_handle, m_axisnum, 2);
        }

        private void C_Sdo_Read_Click(object sender, EventArgs e)       //ETHERCAT读取
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int ret = 0;
            uint m_sdo_node2 = Convert.ToUInt32(C_SdoNode1.Text);
            uint m_sdo_index2 = Convert.ToUInt32(C_SdoReg1.Text);
            uint m_sdo_sub2 = Convert.ToUInt32(C_SdoIsub1.Text);
            uint m_sdo_type2 = Convert.ToUInt32(C_SdoType1.SelectedIndex.ToString()) + 1;
            int m_sdo_data2 = 0;
            if (Bus_type == 0)
            {
                ret = zmcaux.ZAux_BusCmd_SDORead(g_handle, 0, m_sdo_node2, m_sdo_index2, m_sdo_sub2, m_sdo_type2, ref m_sdo_data2);
                if (ret != 0)
                {
                    MessageBox.Show("读取失败");
                    return;
                }
                C_Sdodata1.Text = m_sdo_data2.ToString();
            }
            else
            {
                MessageBox.Show("非ETHERCAT模块");
                return;
            }

        }

        private void C_Sdo_Write_Click(object sender, EventArgs e)      //ETHERCAT写
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int ret = 0;
            uint m_sdo_node1 = Convert.ToUInt32(C_SdoNode0.Text);
            uint m_sdo_index1 = Convert.ToUInt32(C_SdoReg0.Text);
            uint m_sdo_sub1 = Convert.ToUInt32(C_SdoIsub0.Text);
            uint m_sdo_type1 = Convert.ToUInt32(C_SdoType0.SelectedIndex.ToString()) + 1;
            int m_sdo_data1 = Convert.ToInt32(C_Sdodata0.Text);
            if (Bus_type == 0)
            {
                ret = zmcaux.ZAux_BusCmd_SDOWrite(g_handle, 0, m_sdo_node1, m_sdo_index1, m_sdo_sub1, m_sdo_type1, m_sdo_data1);
                if (ret != 0)
                {
                    MessageBox.Show("写入失败");
                    return;
                }
            }
            else
            {
                MessageBox.Show("非ETHERCAT模块");
                return;
            }
        }

        private void C_Rtex_Read_Click(object sender, EventArgs e)      //RTEX读取
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int ret = 0;
            uint m_rtex_axis1 = Convert.ToUInt32(C_RtexAxis1.Text);
            uint m_rtex_para1 = Convert.ToUInt32(C_RtexPara1.Text);
            float  m_rtex_data1 = 0;
            if (Bus_type == 1)
            {
                ret = zmcaux.ZAux_BusCmd_RtexRead(g_handle, m_rtex_axis1, m_rtex_para1, ref m_rtex_data1);
                if (ret != 0)
                {
                    MessageBox.Show("读取失败");
                    return;
                }
                C_RtexData0.Text = m_rtex_data1.ToString();
            }
            else
            {
                MessageBox.Show("非RTEX模块");
                return;
            }
        }

        private void C_Rtex_Write_Click(object sender, EventArgs e)     //RTEX写
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int ret = 0;
            uint m_rtex_axis0 = Convert.ToUInt32(C_RtexAxis0.Text);
            uint m_rtex_para0 = Convert.ToUInt32(C_RtexPara0.Text);
            float m_rtex_data0 = Convert.ToSingle(C_RtexData0.Text);
            if (Bus_type == 1)
            {
                ret = zmcaux.ZAux_BusCmd_RtexWrite(g_handle, m_rtex_axis0, m_rtex_para0,  m_rtex_data0);
                if (ret != 0)
                {
                    MessageBox.Show("写入失败");
                    return;
                }
            }
            else
            {
                MessageBox.Show("非RTEX模块");
                return;
            }
        }

        private void BUTTON_CSP_Click(object sender, EventArgs e)               //切换到位置模式
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int ret = zmcaux.ZAux_Direct_SetAtype(g_handle, m_axisnum, 65);
            if (ret == 0)
            {
                C_AxisType.Text = "65";
            }
        }

        private void BUTTON_CST_Click(object sender, EventArgs e)               //切换到转矩模式
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int ret = zmcaux.ZAux_Direct_SetAtype(g_handle, m_axisnum, 67);
            if (ret == 0)
            {
                C_AxisType.Text = "67";
            }
        }

        private void button_get_Click(object sender, EventArgs e)               //读取转矩相关设置参数
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }

            int ret = 0;
            if (Bus_type == 0)              //ECAT驱动
            {
                //通过SDO方式修改驱动器上限制范围值，具体对象字典查看驱动器手册
                //比如松下电机:
              int[] i_SdoPara =  new int[2];
              float fTargetTor = 0;
              ret += zmcaux.ZAux_BusCmd_SDOReadAxis(g_handle, (UInt32)m_axisnum, 0x6080, 0, 7, ref i_SdoPara[0]);      //读取最大转速
              ret += zmcaux.ZAux_BusCmd_SDOReadAxis(g_handle, (UInt32)m_axisnum, 0x6072, 0, 7, ref i_SdoPara[1]);      //读取最大转转矩

              ret += zmcaux.ZAux_Direct_GetDAC(g_handle, (UInt32)m_axisnum, ref fTargetTor);                            //读取设置的目标转矩
              if (ret == 0)
              {
                  TARGET_TORQUE.Text = fTargetTor.ToString();
                  MAX_SPEED.Text = i_SdoPara[0].ToString();
                  MAX_TORQUE.Text = i_SdoPara[1].ToString();
              }

            }
        }

        private void button_set_Click(object sender, EventArgs e)               //修改转矩相关参数
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }

            int ret = 0;
            if (Bus_type == 0)              //ECAT驱动
            {
                int[] i_SdoPara = new int[2];
                float fTargetTor = 0;

                i_SdoPara[0] = Convert.ToInt32(MAX_SPEED.Text);
                i_SdoPara[1] = Convert.ToInt32(MAX_TORQUE.Text);
                fTargetTor = Convert.ToSingle(TARGET_TORQUE.Text);

                ret += zmcaux.ZAux_BusCmd_SDOWriteAxis(g_handle, (UInt32)m_axisnum, 0x6080, 0, 7,  i_SdoPara[0]);      //读取最大转速
                ret += zmcaux.ZAux_BusCmd_SDOWriteAxis(g_handle, (UInt32)m_axisnum, 0x6072, 0, 7,  i_SdoPara[1]);      //读取最大转转矩

                ret += zmcaux.ZAux_Direct_SetDAC(g_handle, (UInt32)m_axisnum,  fTargetTor);                            //读取设置的目标转矩
            }

        }






    }
}
