using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using System.Runtime.InteropServices;
using cszmcaux;
using System.IO.Ports;



namespace test_function8
{
    struct ZpjFileInfo
    {
        public string pFileName;
        public int nFileType;
        public int nAutoTask;
    }
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
       private void C_connect_type_TextChanged(object sender, EventArgs e)
       {
            string connectType = C_connect_type.Text;
            cc = new ConnectContext(connectType);
       }
        public bool g_basflag = false;  //BAS文件加载标志位
        public int g_InitStatus = -1;   //总线初始化完成状态	0-失败 1-成功  -1-初始化未完成
        public float Bus_type = -1;     //BAS文件中变量判断总线类型，也作为BAS文件是否下载成功判断
        public bool if_home = false;

        const int MAX_FILE = 128;
        string m_ZpjFilePath;			//ZPJ项目路径
        string m_ZpjFileName;			//ZPJ项目名称
        int m_ZpjFileNum;				//ZPJ中文件数量
        bool m_ZpjOPenFlag = false;				//ZPJ项目打开标记
        
        ZpjFileInfo[] m_ZpjFile_Info = new ZpjFileInfo[MAX_FILE];

        public int m_axisnum = 0;
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

         

        private void C_Download_Click(object sender, EventArgs e)       //下载BAS文件到控制器
        {

            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "\\";
            openFileDialog1.Filter = "配置文件(*.zpj)|*.zpj";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)            //打开配置文件
            {
                m_ZpjFilePath = System.IO.Path.GetDirectoryName(openFileDialog1.FileName);
                m_ZpjFileName = openFileDialog1.FileName;
                if (ReadZpjFile())
                {
                    ShowFileList();
                }
                else
                {
                    MessageBox.Show("ZPJ项目打开失败");
                }
                
            }
            
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)] 
        public static extern int GetPrivateProfileString(string section, string key, string defaultValue, StringBuilder value, int size, string filePath);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
       
        private bool ReadZpjFile()
        {

            string LpAppname = "Controller";
            string Lpkeynum = "Files ";

            StringBuilder value = new StringBuilder(255); // 设置一个足够大的缓冲区

            // 调用GetPrivateProfileString函数读取INI文件中的配置信息
            if (GetPrivateProfileString(LpAppname, Lpkeynum, "", value, value.Capacity, m_ZpjFileName) > 0)
            {
               m_ZpjFileNum =  int.Parse(value.ToString());
               if (m_ZpjFileNum >= MAX_FILE)
               {
                   MessageBox.Show("文件数量超过范围");
                   return false;
               }

               long ilen = 0;
               LpAppname = "FileList";
               for (int i = 0; i < m_ZpjFileNum; i++)
               {
                   Lpkeynum = string.Format("File{0}", i + 1);
                   ilen = GetPrivateProfileString(LpAppname, Lpkeynum, "NULL", value, value.Capacity, m_ZpjFileName);
                   if (ilen > 0)
                   {
                       m_ZpjFile_Info[i].pFileName = value.ToString();

                       string iFileName = Path.Combine(m_ZpjFilePath, m_ZpjFile_Info[i].pFileName);
                       bool bflag = File.Exists(iFileName);
                       if (!bflag)
                       {
                           MessageBox.Show(iFileName + "文件不存在");
                           return false;
                       }
                   }
                   Lpkeynum = string.Format("FileType{0}", i + 1);
                   ilen = GetPrivateProfileString(LpAppname, Lpkeynum, "NULL", value, value.Capacity, m_ZpjFileName);
                   if (ilen > 0)
                   {
                       m_ZpjFile_Info[i].nFileType = int.Parse(value.ToString());
                   }

                   Lpkeynum = string.Format("AutoRun{0}", i + 1);
                   ilen = GetPrivateProfileString(LpAppname, Lpkeynum, "NULL", value, value.Capacity, m_ZpjFileName);
                   if (ilen > 0)
                   {
                       m_ZpjFile_Info[i].nAutoTask = int.Parse(value.ToString());
                   }
               } 
            }
            m_ZpjOPenFlag = true;
            this.Text = m_ZpjFileName;
            return true;
        }

        private void ShowFileList()
        {
            listView1.Items.Clear();
            for (int iItem = 0; iItem < m_ZpjFileNum; iItem++)
            {
                ListViewItem item = new ListViewItem();

                string tempstr = m_ZpjFile_Info[iItem].pFileName;
                item.Text = tempstr;

                if (m_ZpjFile_Info[iItem].nAutoTask >= 0)
                {
                    tempstr = m_ZpjFile_Info[iItem].nAutoTask.ToString();
                    item.SubItems.Add(tempstr);
                }
                listView1.Items.Add(item);
            }
        }

        private void ZML_Click(object sender, EventArgs e)
        {
            if (!m_ZpjOPenFlag)
            {
                MessageBox.Show("先新建/打开ZPJ项目！");
                return;
            }

            if (m_ZpjFileNum >= MAX_FILE)
            {
                MessageBox.Show("文件数量超过范围");
                return;
            }
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "\\";
            openFileDialog1.Filter = "ZML文件(*.zml)|*.zml";
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FilterIndex = 1;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string str = openFileDialog1.SafeFileName;

                for (int i = 0; i < m_ZpjFileNum; i++)
                {
                    if (str == m_ZpjFile_Info[i].pFileName)
                    {
                        MessageBox.Show("重名");
                        return; 
                    }
                }

                m_ZpjFile_Info[m_ZpjFileNum].pFileName = str;
                m_ZpjFile_Info[m_ZpjFileNum].nFileType = 19;
                m_ZpjFile_Info[m_ZpjFileNum].nAutoTask = -1;
                m_ZpjFileNum++;

                SaveZpjFile();
                ShowFileList();
            }


        }

        private bool SaveZpjFile()
        { 
            if(!File.Exists(m_ZpjFileName))
            {
                MessageBox.Show("Open Failed!");
                return false; 
            }
            string LpAppname = "Controller";
            string LpKeynum = "Type";
            WritePrivateProfileString(LpAppname, LpKeynum, "", m_ZpjFileName);
            LpKeynum = "IfUpload";
            WritePrivateProfileString(LpAppname, LpKeynum, "0", m_ZpjFileName);
            LpKeynum = "UserVersion";
            WritePrivateProfileString(LpAppname, LpKeynum, "1.0", m_ZpjFileName);
            LpKeynum = "Files";
            WritePrivateProfileString(LpAppname, LpKeynum, m_ZpjFileNum.ToString(), m_ZpjFileName);
            LpKeynum = "Breaks";
            WritePrivateProfileString(LpAppname, LpKeynum, "0", m_ZpjFileName);
            LpKeynum = "PicPack";
            WritePrivateProfileString(LpAppname, LpKeynum, "0", m_ZpjFileName);
            LpKeynum = "BREAKMODE";
            WritePrivateProfileString(LpAppname, LpKeynum, "1", m_ZpjFileName);

            string iFileName = "";
            bool bflag = false;
            string Lpdata;
            LpAppname = "FileList";
            for (int i = 0; i < m_ZpjFileNum; i++)
            {
                if (m_ZpjFile_Info[i].pFileName == "")
                {
                    Lpdata = "空文件名!";
                }
                iFileName = m_ZpjFilePath + "\\" + m_ZpjFile_Info[i].pFileName;
                bflag = File.Exists(iFileName);
                if (bflag)
                {
                    LpKeynum = "File" + (i + 1);
                    Lpdata = m_ZpjFile_Info[i].pFileName;
                    WritePrivateProfileString(LpAppname, LpKeynum, Lpdata, m_ZpjFileName);

                    LpKeynum = "FileType" + (i + 1);
                    Lpdata = m_ZpjFile_Info[i].nFileType.ToString();
                    WritePrivateProfileString(LpAppname, LpKeynum, Lpdata, m_ZpjFileName);

                    LpKeynum = "AutoRun" + (i + 1);
                    Lpdata = m_ZpjFile_Info[i].nAutoTask.ToString();
                    WritePrivateProfileString(LpAppname, LpKeynum, Lpdata, m_ZpjFileName);
                }
                else
                {
                    MessageBox.Show(m_ZpjFile_Info[i].pFileName+"文件不存在当前目录下");
                    return false; 
                }
            }
            LpAppname = "OpenList";
            m_ZpjOPenFlag = true;
            this.Text = m_ZpjFileName;
            return true;
        }
            
        private void button2_Click(object sender, EventArgs e)
        {
            int Index = 0;
            if (!m_ZpjOPenFlag)
            {
                MessageBox.Show("先新建/打开ZPJ项目！");
                return;
            }
            else if(m_ZpjFileNum <= 0)
                return;
            else if (this.listView1.SelectedItems.Count > 0)
            {
                Index = this.listView1.SelectedItems[0].Index; //取当前选中项的index,SelectedItems[0] 
                String str = listView1.Items[Index].SubItems[0].Text;
                string lastThreeChars = str.Substring(str.Length - 3).ToLower();
                if (lastThreeChars != "zml")
                    MessageBox.Show("请选择zml文件");
            }

            for (int i = Index; i < m_ZpjFile_Info.Length - 1; i++)
            {
                m_ZpjFile_Info[i] = m_ZpjFile_Info[i + 1];
            }
            Array.Resize(ref m_ZpjFile_Info, m_ZpjFile_Info.Length - 1);

            m_ZpjFileNum--;

            SaveZpjFile();

	        ShowFileList();
        }



        private void OnDownload(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            else if (!m_ZpjOPenFlag)
            {
                MessageBox.Show("先新建/打开ZPJ项目！");
                return;
            }
            String m_ZarFileName = m_ZpjFilePath + "\\ZmcZpj.Zar";
            
            int iret = zmcaux.ZAux_ZpjDown(g_handle, m_ZpjFileName, m_ZarFileName, null, 0, 1);
           
            //下载到ROM
            if (iret != 0)
            {
                MessageBox.Show("文件下载失败!", "提示");
                return;
            }
            g_basflag = true;
            //g_InitStatus = -1;
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
            float tempstatus = -1;
            float[] f_AxisPara = new float[10];
            int[] i_AxisPara =  new int[10];
            if (g_handle != (IntPtr)0)
            {
                m_axisnum = Convert.ToInt32(C_Move_Axis.Text);
                ret += zmcaux.ZAux_Direct_GetUserVar(g_handle, "Bus_InitStatus", ref tempstatus);				//读取BAS文件中的变量判断总线初始化完成状态
                ret += zmcaux.ZAux_Direct_GetAxisEnable(g_handle, m_axisnum, ref i_AxisPara[0]);
                ret += zmcaux.ZAux_Direct_GetMpos(g_handle, m_axisnum, ref f_AxisPara[0]);
                ret += zmcaux.ZAux_Direct_GetDpos(g_handle, m_axisnum, ref f_AxisPara[1]);
                ret += zmcaux.ZAux_Direct_GetAxisStatus(g_handle, m_axisnum, ref i_AxisPara[1]);
                ret += zmcaux.ZAux_Direct_GetIfIdle(g_handle, m_axisnum, ref i_AxisPara[2]);
                ret += zmcaux.ZAux_Direct_GetAtype(g_handle, m_axisnum, ref i_AxisPara[3]);
                if (ret == 0)
                {
                    g_InitStatus = (int)tempstatus;
                    C_Bus_Status.Text = (g_InitStatus == 1) ? "初始化成功" : "初始化失败";
                    C_AxisEnable.Text = (i_AxisPara[0] == 0)?"off":"on";
                    C_AxisMpos.Text = f_AxisPara[0].ToString();
                    C_AxisDpos.Text = f_AxisPara[1].ToString();
                    C_AxisStatus.Text = i_AxisPara[1].ToString();
                    C_AxisIdle.Text = i_AxisPara[2].ToString();
                    C_AxisType.Text = i_AxisPara[3].ToString();
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

                if (g_basflag && g_InitStatus == -1 || g_InitStatus == 1)				//已经加载文件并且正在初始化 读取状态
                {
                    
                    int m_BusNodeNum = 0;
                    float m_BusAxisNum = 0;
                    ret += zmcaux.ZAux_Direct_GetUserVar(g_handle, "BUS_TYPE", ref Bus_type);						//读取BAS文件中的变量判断总线类型
                    
                    ret += zmcaux.ZAux_BusCmd_GetNodeNum(g_handle, 0, ref m_BusNodeNum);							//读取槽位0上节点个数。
                    ret += zmcaux.ZAux_Direct_GetUserVar(g_handle, "Bus_TotalAxisnum", ref m_BusAxisNum);			//读取BAS文件中的变量判断扫描的总轴数
                    
                    if (ret == 0 && g_InitStatus != -1)		//初始化完成刷新状态	
                    {
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
            uint m_sdo_index2 = Convert.ToUInt32(C_SdoReg1.Text,16);
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
            uint m_sdo_index1 = Convert.ToUInt32(C_SdoReg0.Text,16);
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
