using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using Motion;
using System.Threading;
using cszmcaux;
using System.Timers;

namespace Delta
{
    public partial class MainForm : UIForm
    {  

        public  Function Delta = new Function();

        int pageIndex = 1000;

        int _res;

        public MainForm()   
        {
            InitializeComponent();

     
            #region  节点1

            Header.SetNodePageIndex(Header.Nodes[0], pageIndex);
            Header.SetNodeSymbol(Header.Nodes[0], 61573);      // 设置Header的节点0图标  
            TreeNode parent = Aside.CreateNode("Delta", 61573, 24, pageIndex);   //创建Aside的节点0 
            Aside.CreateChildNode(parent, AddPage(new Delta臂长参数(this), ++pageIndex));   //创建节点0子节点  ,后续根据实际需求修改名称
            Aside.CreateChildNode(parent, AddPage(new 驱动器参数(this), ++pageIndex));   //创建节点1子节点 ，后续根据实际需求修改名称
            Aside.CreateChildNode(parent, AddPage(new Delta标定(this), ++pageIndex));   //创建节点2子节点  ,后续根据实际需求修改名称
            Aside.CreateChildNode(parent, AddPage(new Delta坐标系转换(this), ++pageIndex));   //创建节点3子节点  ,后续根据实际需求修改名称
            #endregion

            #region  节点2

            pageIndex = 2000;
            Header.SetNodePageIndex(Header.Nodes[1], pageIndex);
            Header.SetNodeSymbol(Header.Nodes[1], 61818);
            parent = Aside.CreateNode("运动控制", 61818, 24, pageIndex);
            //通过设置GUID关联，节点字体图标和大小由UIPage设置
            Aside.CreateChildNode(parent, AddPage(new 手动运动(this), Guid.NewGuid()));
            Aside.CreateChildNode(parent, AddPage(new 示教系统(this), Guid.NewGuid()));

            #endregion

            #region 节点3

            pageIndex = 3000;
            Header.SetNodePageIndex(Header.Nodes[2], pageIndex);
            Header.SetNodeSymbol(Header.Nodes[2], 61950);
            parent = Aside.CreateNode("IO", 61950, 24, pageIndex);
            //直接关联（默认自动生成GUID）
            Aside.CreateChildNode(parent, AddPage(new IO监控(this)));


            #endregion

            #region 节点4

            pageIndex = 4000;
            Header.SetNodePageIndex(Header.Nodes[3], pageIndex);
            Header.SetNodeSymbol(Header.Nodes[3], 362614);
            parent = Aside.CreateNode("日志", 57574, 24, pageIndex);
            //直接关联（默认自动生成GUID）

            Aside.CreateChildNode(parent, AddPage(new 报警记录(this)));

            #endregion


            #region 主题设置


            //Header.SetNodeSymbol(Header.Nodes[4], 61502);
            //var styles = UIStyles.PopularStyles();
            //foreach (UIStyle style in styles)
            //{
            //    Header.CreateChildNode(Header.Nodes[4], style.DisplayText(), style.Value());
            //}

            //Header.CreateChildNode(Header.Nodes[4], "多彩主题", UIStyle.Colorful.Value());

            #endregion


            #region 其他

            //选中第一个节点
            Aside.SelectPage(1001);

            #endregion

            _res = Delta.Connect("192.168.0.11");
         
            if (_res !=0)
            {
                this.Text = "Delta运动模块(未连接)";
                MessageBox.Show("控制器连接失败");
                return;
            }
            this.Text = "Delta运动模块(已连接)";

            _res = Delta.Initialize();
            if (_res != 0)
            {
                this.Text = "Delta运动模块(已连接)";
                MessageBox.Show("控制器初始化失败");
            }

            this.InitTimers();

            BindTimers();

        }

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
         
            this.Log_Tri.AutoReset = false;

            if (Delta.IntPtr == IntPtr.Zero)
            {

                this.Log_Tri.AutoReset = true;
                return;

            }
            {
                try
                {
                    this.Invoke(new Action(() =>
                    {

                        if(Delta.LogStatus == 1)
                        {

                           this.TitleColor = Color.Red;
                           LB_Log.Visible = true;
                           LB_Log.Text = "轴报警";

                        }
                        else
                        {
                            this.TitleColor = Color.MediumSlateBlue;
                            LB_Log.Visible = false;
                            LB_Log.Text = "";
                        }

                    }));
                }
                catch (Exception ex)
                {

                }
            }

            this.Log_Tri.AutoReset = true;
        }

        #region  事件
        private void Header_MenuItemClick(string itemText, int menuIndex, int pageIndex)
        {

            switch (menuIndex)
            {
                case 4:
                    UIStyle style = (UIStyle)pageIndex;
                    if (style != UIStyle.Colorful)

                        StyleManager.Style = style;

                    else
                        SelectPage(pageIndex);

                    break;
                default:
                    Aside.SelectPage(pageIndex);
                    break;
            }
        }


        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
             Delta.Close();
        }
    }
}
