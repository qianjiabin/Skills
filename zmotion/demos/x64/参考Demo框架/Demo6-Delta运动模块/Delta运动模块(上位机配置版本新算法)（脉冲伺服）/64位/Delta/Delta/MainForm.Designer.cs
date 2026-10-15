using Sunny.UI;

namespace Delta
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("Delta");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("运动控制");
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("IO");
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("日志");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.StyleManager = new Sunny.UI.UIStyleManager(this.components);
            this.Header = new Sunny.UI.UINavBar();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiAvatar1 = new Sunny.UI.UIAvatar();
            this.Aside = new Sunny.UI.UINavMenu();
            this.MainContainer = new Sunny.UI.UITabControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LB_Log = new Sunny.UI.UILabel();
            this.Header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Header
            // 
            this.Header.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Header.Controls.Add(this.pictureBox1);
            this.Header.Controls.Add(this.LB_Log);
            this.Header.Controls.Add(this.uiLabel1);
            this.Header.Controls.Add(this.uiAvatar1);
            this.Header.Dock = System.Windows.Forms.DockStyle.Top;
            this.Header.DropMenuFont = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Header.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Header.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.Header.Location = new System.Drawing.Point(0, 35);
            this.Header.MenuHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Header.MenuSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Header.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.Header.Name = "Header";
            treeNode13.Name = "节点3";
            treeNode13.Text = "Delta";
            treeNode14.Name = "节点4";
            treeNode14.Text = "运动控制";
            treeNode15.Name = "节点5";
            treeNode15.Text = "IO";
            treeNode16.Name = "节点6";
            treeNode16.Text = "日志";
            this.Header.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode13,
            treeNode14,
            treeNode15,
            treeNode16});
            this.Header.Size = new System.Drawing.Size(1280, 110);
            this.Header.StyleCustomMode = true;
            this.Header.TabIndex = 1;
            this.Header.Text = "uiNavBar1";
            this.Header.MenuItemClick += new Sunny.UI.UINavBar.OnMenuItemClick(this.Header_MenuItemClick);
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.Location = new System.Drawing.Point(34, 37);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(419, 41);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel1.TabIndex = 1;
            this.uiLabel1.Text = "Zmotion深圳正运动技术";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiAvatar1
            // 
            this.uiAvatar1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiAvatar1.Location = new System.Drawing.Point(1829, 23);
            this.uiAvatar1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiAvatar1.Name = "uiAvatar1";
            this.uiAvatar1.Size = new System.Drawing.Size(60, 60);
            this.uiAvatar1.Style = Sunny.UI.UIStyle.Custom;
            this.uiAvatar1.TabIndex = 0;
            this.uiAvatar1.Text = "uiAvatar1";
            // 
            // Aside
            // 
            this.Aside.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Aside.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.Aside.Dock = System.Windows.Forms.DockStyle.Left;
            this.Aside.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.Aside.FillColor = System.Drawing.Color.White;
            this.Aside.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Aside.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.Aside.FullRowSelect = true;
            this.Aside.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Aside.ItemHeight = 50;
            this.Aside.Location = new System.Drawing.Point(0, 145);
            this.Aside.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.Aside.Name = "Aside";
            this.Aside.ScrollBarColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.Aside.ScrollBarHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.Aside.ScrollBarPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.Aside.ScrollFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Aside.SecondBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(235)))), ((int)(((byte)(235)))));
            this.Aside.SelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Aside.SelectedColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.Aside.ShowLines = false;
            this.Aside.Size = new System.Drawing.Size(169, 755);
            this.Aside.Style = Sunny.UI.UIStyle.Custom;
            this.Aside.TabIndex = 2;
            this.Aside.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // MainContainer
            // 
            this.MainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainContainer.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.MainContainer.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MainContainer.Frame = this;
            this.MainContainer.ItemSize = new System.Drawing.Size(150, 40);
            this.MainContainer.Location = new System.Drawing.Point(169, 145);
            this.MainContainer.MainPage = "";
            this.MainContainer.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.MainContainer.Name = "MainContainer";
            this.MainContainer.SelectedIndex = 0;
            this.MainContainer.Size = new System.Drawing.Size(1111, 755);
            this.MainContainer.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.MainContainer.Style = Sunny.UI.UIStyle.Custom;
            this.MainContainer.TabBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.MainContainer.TabIndex = 3;
            this.MainContainer.TabSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.MainContainer.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.BackgroundImage")));
            this.pictureBox1.Location = new System.Drawing.Point(1195, 54);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 48);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // LB_Log
            // 
            this.LB_Log.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.LB_Log.ForeColor = System.Drawing.Color.Red;
            this.LB_Log.Image = ((System.Drawing.Image)(resources.GetObject("LB_Log.Image")));
            this.LB_Log.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LB_Log.Location = new System.Drawing.Point(464, 37);
            this.LB_Log.Name = "LB_Log";
            this.LB_Log.Size = new System.Drawing.Size(189, 41);
            this.LB_Log.Style = Sunny.UI.UIStyle.Custom;
            this.LB_Log.TabIndex = 2;
            this.LB_Log.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LB_Log.Visible = false;
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1280, 900);
            this.Controls.Add(this.MainContainer);
            this.Controls.Add(this.Aside);
            this.Controls.Add(this.Header);
            this.Name = "MainForm";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "Delta运动模块V1.1";
            this.TitleColor = System.Drawing.Color.MediumSlateBlue;
            this.ZoomScaleRect = new System.Drawing.Rectangle(15, 15, 800, 480);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Header.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion
        private UIStyleManager StyleManager;
        private UINavBar Header;
        private UIAvatar uiAvatar1;
        private UINavMenu Aside;
        private UITabControl MainContainer;
        private UILabel uiLabel1;
        private UILabel LB_Log;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

