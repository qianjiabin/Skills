// 嘉泰标准主界面骨架 — 复制到项目后替换 {ProjectNamespace}，再叠加打标/通信业务逻辑。
// Designer 使用同目录 FormMain.Designer.cs（同步替换命名空间与事件处理类名）。

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using LaserMark;

namespace {ProjectNamespace}
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void InitToolbarIcons()
        {
            const int iconSize = 36;
            UiIconHelper.ApplyImageButton(btOpenFile, UiIconKind.OpenTemplate, toolTipMain, "打开模板", iconSize);
            UiIconHelper.ApplyImageButton(btWebApi, UiIconKind.WebApi, toolTipMain, "WebApi 测试", iconSize);
            UiIconHelper.ApplyImageButton(btSystem, UiIconKind.SystemSettings, toolTipMain, "系统参数", iconSize);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            InitToolbarIcons();
            splitContainerLog.Panel2.Controls.Add(Log.GetLogView());
            Log.Init();

            if (!SystemMgr.Instance.LoadDocument(@".\Product\data.ldoc"))
            {
                SystemMgr.Instance.EnsureProduct();
            }

            timerPreview.Start();
            pbPreview.Resize += (s, ev) => RefreshPreview();
            BeginInvoke(new Action(RefreshPreview));
            tssl_Status.Text = "就绪";
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerPreview.Stop();
            SystemMgr.Instance.SaveDocument();
        }

        private void RefreshPreview()
        {
            if (pbPreview.ClientSize.Width < 10 || pbPreview.ClientSize.Height < 10)
            {
                return;
            }
            // TODO: 接入 EZCAD2 GetCurPreviewImage 或 EZCAD3 渲染
        }

        private void timerPreview_Tick(object sender, EventArgs e)
        {
            RefreshPreview();
        }

        private void btOpenFile_Click(object sender, EventArgs e)
        {
            // TODO: OpenFileDialog 加载 ezd/ez3
        }

        private void btWebApi_Click(object sender, EventArgs e)
        {
            // TODO: 打开 WebApi 配置/测试窗体
        }

        private void btSystem_Click(object sender, EventArgs e)
        {
            // TODO: 打开系统参数窗体
        }

        private void btRedMark_Click(object sender, EventArgs e) { }
        private void btMarkOnce_Click(object sender, EventArgs e) { }
        private void btStop_Click(object sender, EventArgs e) { }
        private void btMoveLeft_Click(object sender, EventArgs e) { }
        private void btMoveUp_Click(object sender, EventArgs e) { }
        private void btMoveRight_Click(object sender, EventArgs e) { }
        private void btMoveDown_Click(object sender, EventArgs e) { }
        private void btResetOffset_Click(object sender, EventArgs e) { }
    }
}
