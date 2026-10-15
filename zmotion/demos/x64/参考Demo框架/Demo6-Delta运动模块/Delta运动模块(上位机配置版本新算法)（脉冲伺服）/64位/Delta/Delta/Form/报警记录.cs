using cszmcaux;
using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Delta
{
    public partial class 报警记录 : UIPage
    {

        MainForm mainForm;

        private int[] jAxisStatus = new int[4];  //关节轴状态

        private int[] vAxisStatus = new int[4];  //虚拟xyzu轴状态

        private string[] status = new string[9];  //轴状态转换字符串

        private int errNum = 0;  //轴报错数量


        private int[] jstatusTemp = new int[4];   //临时对比轴状态，避免重复触发打印
        private int[] vstatusTemp = new int[4];   //临时对比轴状态，避免重复触发打印


        public 报警记录(MainForm form)
        {
            InitializeComponent();

            this.mainForm = form;

            this.txt_Log.Text = "";

            this.InitTimers();

            BindTimers();
        }

        #region 【日志方法】
        public void AddLog(string iLog)
        {
            if (!this.InvokeRequired)
            {
                try
                {
                    this.AddLogT(iLog);
                }
                catch (Exception) { }
            }
            else
            {
                Invoke(new Action(() =>
                {
                    try
                    {
                        this.AddLogT(iLog);
                    }
                    catch (Exception) { }
                }));
            }
        }
        private void AddLogT(string iLog)
        {
            iLog = iLog.Replace("\r\n", "\\r\\n");
            this.txt_Log.AppendText("[" + DateTime.Now.ToString("HH:mm:ss-fff") + "]: " + iLog + "\r\n");
        }
        public void ClearLog()
        {
            if (!this.txt_Log.InvokeRequired)
            {
                try
                {
                    this.ClearLogT();
                }
                catch (Exception) { }
            }
            else
            {
                Invoke(new Action(() =>
                {
                    try
                    {
                        this.ClearLogT();
                    }
                    catch (Exception) { }
                }));
            }
        }
        private void ClearLogT()
        {
            this.txt_Log.Text = "";
        }
        #endregion

        #region 【计时器】  

        //任务触发事件
        public System.Timers.Timer Log_Tri;

        private string InitTimers()
        {
            try
            {
                this.Log_Tri = new System.Timers.Timer();

                return null;
            }
            catch (Exception ex)
            {
                return "实例化计时器失败:" + ex.Message;
            }
        }

        #endregion

        private string BindTimers()
        {
            try
            {

                this.Log_Tri.Interval = 500;
                this.Log_Tri.Elapsed += Read_T1_Tri_Elapsed;
                this.Log_Tri.AutoReset = false;
                this.Log_Tri.Enabled = true;

                return null;

            }
            catch (Exception ex)
            {
                return "绑定计时器失败:" + ex.Message;
            }
        }

        private string UnBindTimers()
        {
            try
            {

                this.Log_Tri.Elapsed -= Read_T1_Tri_Elapsed;
                this.Log_Tri.Enabled = false;
                return null;
            }
            catch (Exception ex)
            {
                return "解绑计时器失败:" + ex.Message;
            }

        }


        private void Read_T1_Tri_Elapsed(object sender, ElapsedEventArgs e)
        {
            int _res;

            int num = 0;

            int temp = 0; 
            int temp1 = 0;

            this.Log_Tri.AutoReset = false;

            if (mainForm.Delta.IntPtr == IntPtr.Zero)
            {

                this.Log_Tri.AutoReset = true;
                return;

            }
            {
                try
                {
                    this.Invoke(new Action(() =>
                    {

                        mainForm.Delta.Read_JAxisStatus(ref jAxisStatus);

                        mainForm.Delta.Read_VAxisStatus(ref vAxisStatus);

                        postback(jAxisStatus, vAxisStatus);

                        for (int i = 0; i < 4; i++)
                        {
                            if (jAxisStatus[i] != 0 && jAxisStatus[i] != jstatusTemp[i]) 
                            {
                               
                                jstatusTemp[i] = jAxisStatus[i];

                                ParseStatus(jAxisStatus[i], ref status,ref errNum);

                                DisPlay(0,i, status, errNum);

                                clear();
                            }                                               
                        }
                 
                        for (int i = 0; i < 4; i++)
                        {
                            if (vAxisStatus[i] != 0 && vAxisStatus[i] != vstatusTemp[i])
                            {
                              
                                vstatusTemp[i] = vAxisStatus[i];

                                ParseStatus(vAxisStatus[i], ref status, ref errNum);

                                DisPlay(1,i,status, errNum);

                                clear();
                            }
                          
                        }

                    }));
                }
                catch (Exception ex)
                {

                }
            }

            this.Log_Tri.AutoReset = true;
        }



        /// <summary>
        /// 解析轴状态并返回字符串
        /// </summary>
        /// <param name="value">传入的轴状态数据</param>
        /// <param name="status">返回对应轴状态的字符串</param>
        /// <param name="num">返回对应轴状态报错个数</param>
        public void ParseStatus(int value, ref string[]status,ref int num)
        {

                bool[] val = new bool[23];

                int temp = 0;

                for (var j = 0; j < 23; j++)
                {                   
                    val[j]= ((value >> j) & 1) == 1;

                    if (val[j]==true) 
                    {
                        switch (j)
                        {
                            case 2:
                                status[temp] = "与远程轴通讯出错\r\n";
                                temp++;
                                break;
                            case 3:
                                status[temp] = "远程驱动器报错\r\n";
                                temp++;
                                break;
                            case 4:
                                status[temp] = "正向硬限位触发\r\n";
                                temp++;
                                break;
                            case 5:
                                status[temp] = "反向硬限位触发\r\n";
                                temp++;
                                break;
                            case 9:
                                status[temp] = "超过正向软限位\r\n";
                                temp++;
                                break;
                            case 10:
                                status[temp] = "超过负向软限位\r\n";
                                temp++;
                                break;
                            case 12:
                                status[temp] = "脉冲频率超过MAX_SPEED\r\n";
                                temp++;
                                break;
                            case 14:
                                status[temp] = "机械手指令坐标错误\r\n";
                                temp++;
                                break;
                            case 23:
                                status[temp] = "轴进入了暂停状态\r\n";
                                temp++;
                                break;
                        }
                    }          
                }
                num = temp;      
        }


        /// <summary>
        /// 回传主界面数据，更新报警界面信息
        /// </summary>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        public void postback(int[] value1, int[] value2)
        {

            int temp1 = 0;
            int temp2 = 0;
          
            for (int i = 0;i<4;i++)
            {
                if (value1[i] != 0 && value1[i] != 2048)
                {
                    temp1 = 1;
                }
            }
            for (int i = 0; i < 4; i++)
            {
                if (value2[i] != 0 && value2[i] != 2048)
                {
                    temp2 = 1;
                }
            }
            if(temp1==0 && temp2 == 0)
            {
                mainForm.Delta.LogStatus = 0;
            }
            else
            {
                mainForm.Delta.LogStatus = 1;
            }


        }



        /// <summary>
        /// 日志显示
        /// </summary>
        /// <param name="axisType"></param>
        /// <param name="status"></param>
        /// <param name="num"></param>
        public void DisPlay(int axisType , int axisNum, string[] status, int num)
        {

            if(axisType == 0)
            {
                for (int j = 0; j < num; j++)
                {
                    string temp = "";
                    if (axisNum == 3)
                    {
                        temp = "旋转轴J" + axisNum.ToString() + ":" + status[j];
                    }
                    else
                    {
                        temp = "关节轴J" + axisNum.ToString() + ":" + status[j];
                    }

                    this.AddLog(temp);
                }
            }
            else if(axisType == 1) 
            {      
                for (int j = 0; j < num; j++)
                {
                    string temp = "";

                    switch (axisNum)
                    {
                        case 0:
                            temp = "虚拟X轴:" + status[j];
                            break;
                        case 1:
                            temp = "虚拟Y轴:" + status[j];
                            break;
                        case 2:
                            temp = "虚拟Z轴:" + status[j];
                            break;
                        case 3:
                            temp = "虚拟U轴:" + status[j];
                            break;                        
                    }

                    this.AddLog(temp);
                }              
            }

        }


        public void clear()
        {         
            for (int j = 0; j < 9; j++)
            {
                status[j] = "";
            }

            errNum = 0;     
        }

        private void Btn_clear_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认清空日志？", "清除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {

                ClearLog();

            }
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确认复位？", "复位确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                mainForm.Delta.Reset();
            }     
        }
    }
}
