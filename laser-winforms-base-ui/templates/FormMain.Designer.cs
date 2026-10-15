namespace JiaTaiMark
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.tlbToolbar = new System.Windows.Forms.TableLayoutPanel();
            this.btOpenFile = new System.Windows.Forms.Button();
            this.btWebApi = new System.Windows.Forms.Button();
            this.btSystem = new System.Windows.Forms.Button();
            this.splitContainerMain = new System.Windows.Forms.SplitContainer();
            this.splitContainerLeft = new System.Windows.Forms.SplitContainer();
            this.gbPreview = new System.Windows.Forms.GroupBox();
            this.lblTemplate = new System.Windows.Forms.Label();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.splitContainerLog = new System.Windows.Forms.SplitContainer();
            this.splitContainerRightOuter = new System.Windows.Forms.SplitContainer();
            this.gbReserved = new System.Windows.Forms.GroupBox();
            this.lblReserved = new System.Windows.Forms.Label();
            this.splitContainerRightInner = new System.Windows.Forms.SplitContainer();
            this.gbPosition = new System.Windows.Forms.GroupBox();
            this.btResetOffset = new System.Windows.Forms.Button();
            this.lblOffset = new System.Windows.Forms.Label();
            this.tlbPosition = new System.Windows.Forms.TableLayoutPanel();
            this.btMoveUp = new System.Windows.Forms.Button();
            this.btMoveLeft = new System.Windows.Forms.Button();
            this.btMoveRight = new System.Windows.Forms.Button();
            this.btMoveDown = new System.Windows.Forms.Button();
            this.gbMarkButtons = new System.Windows.Forms.GroupBox();
            this.tlbMarkButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btRedMark = new System.Windows.Forms.Button();
            this.btMarkOnce = new System.Windows.Forms.Button();
            this.btStop = new System.Windows.Forms.Button();
            this.timerPreview = new System.Windows.Forms.Timer(this.components);
            this.toolTipMain = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            this.tlbToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).BeginInit();
            this.splitContainerMain.Panel1.SuspendLayout();
            this.splitContainerMain.Panel2.SuspendLayout();
            this.splitContainerMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).BeginInit();
            this.splitContainerLeft.Panel1.SuspendLayout();
            this.splitContainerLeft.Panel2.SuspendLayout();
            this.splitContainerLeft.SuspendLayout();
            this.gbPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLog)).BeginInit();
            this.splitContainerLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRightOuter)).BeginInit();
            this.splitContainerRightOuter.Panel1.SuspendLayout();
            this.splitContainerRightOuter.Panel2.SuspendLayout();
            this.splitContainerRightOuter.SuspendLayout();
            this.gbReserved.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRightInner)).BeginInit();
            this.splitContainerRightInner.Panel1.SuspendLayout();
            this.splitContainerRightInner.Panel2.SuspendLayout();
            this.splitContainerRightInner.SuspendLayout();
            this.gbPosition.SuspendLayout();
            this.tlbPosition.SuspendLayout();
            this.gbMarkButtons.SuspendLayout();
            this.tlbMarkButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_Status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 639);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1080, 22);
            this.statusStrip1.TabIndex = 2;
            // 
            // tssl_Status
            // 
            this.tssl_Status.Name = "tssl_Status";
            this.tssl_Status.Size = new System.Drawing.Size(32, 17);
            this.tssl_Status.Text = "就绪";
            // 
            // panelToolbar
            // 
            this.panelToolbar.Controls.Add(this.tlbToolbar);
            this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelToolbar.Location = new System.Drawing.Point(0, 0);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(1080, 60);
            this.panelToolbar.TabIndex = 1;
            // 
            // tlbToolbar
            // 
            this.tlbToolbar.ColumnCount = 4;
            this.tlbToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlbToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlbToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tlbToolbar.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlbToolbar.Controls.Add(this.btOpenFile, 0, 0);
            this.tlbToolbar.Controls.Add(this.btWebApi, 1, 0);
            this.tlbToolbar.Controls.Add(this.btSystem, 2, 0);
            this.tlbToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlbToolbar.Location = new System.Drawing.Point(0, 0);
            this.tlbToolbar.Margin = new System.Windows.Forms.Padding(0);
            this.tlbToolbar.Name = "tlbToolbar";
            this.tlbToolbar.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.tlbToolbar.RowCount = 1;
            this.tlbToolbar.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlbToolbar.Size = new System.Drawing.Size(1080, 60);
            this.tlbToolbar.TabIndex = 0;
            // 
            // btOpenFile
            // 
            this.btOpenFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btOpenFile.Location = new System.Drawing.Point(12, 6);
            this.btOpenFile.Margin = new System.Windows.Forms.Padding(0);
            this.btOpenFile.Name = "btOpenFile";
            this.btOpenFile.Size = new System.Drawing.Size(80, 48);
            this.btOpenFile.TabIndex = 0;
            this.btOpenFile.UseVisualStyleBackColor = true;
            this.btOpenFile.Click += new System.EventHandler(this.btOpenFile_Click);
            // 
            // btWebApi
            // 
            this.btWebApi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btWebApi.Location = new System.Drawing.Point(96, 6);
            this.btWebApi.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btWebApi.Name = "btWebApi";
            this.btWebApi.Size = new System.Drawing.Size(76, 48);
            this.btWebApi.TabIndex = 1;
            this.btWebApi.UseVisualStyleBackColor = true;
            this.btWebApi.Click += new System.EventHandler(this.btWebApi_Click);
            // 
            // btSystem
            // 
            this.btSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btSystem.Location = new System.Drawing.Point(176, 6);
            this.btSystem.Margin = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.btSystem.Name = "btSystem";
            this.btSystem.Size = new System.Drawing.Size(76, 48);
            this.btSystem.TabIndex = 2;
            this.btSystem.UseVisualStyleBackColor = true;
            this.btSystem.Click += new System.EventHandler(this.btSystem_Click);
            // 
            // splitContainerMain
            // 
            this.splitContainerMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMain.Location = new System.Drawing.Point(0, 60);
            this.splitContainerMain.Name = "splitContainerMain";
            // 
            // splitContainerMain.Panel1
            // 
            this.splitContainerMain.Panel1.Controls.Add(this.splitContainerLeft);
            // 
            // splitContainerMain.Panel2
            // 
            this.splitContainerMain.Panel2.Controls.Add(this.splitContainerRightOuter);
            this.splitContainerMain.Size = new System.Drawing.Size(1080, 579);
            this.splitContainerMain.SplitterDistance = 808;
            this.splitContainerMain.TabIndex = 0;
            // 
            // splitContainerLeft
            // 
            this.splitContainerLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLeft.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLeft.Name = "splitContainerLeft";
            this.splitContainerLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerLeft.Panel1
            // 
            this.splitContainerLeft.Panel1.Controls.Add(this.gbPreview);
            // 
            // splitContainerLeft.Panel2
            // 
            this.splitContainerLeft.Panel2.Controls.Add(this.splitContainerLog);
            this.splitContainerLeft.Size = new System.Drawing.Size(808, 579);
            this.splitContainerLeft.SplitterDistance = 411;
            this.splitContainerLeft.TabIndex = 0;
            // 
            // gbPreview
            // 
            this.gbPreview.Controls.Add(this.lblTemplate);
            this.gbPreview.Controls.Add(this.pbPreview);
            this.gbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPreview.Location = new System.Drawing.Point(0, 0);
            this.gbPreview.Name = "gbPreview";
            this.gbPreview.Size = new System.Drawing.Size(808, 411);
            this.gbPreview.TabIndex = 0;
            this.gbPreview.TabStop = false;
            this.gbPreview.Text = "EZD图档预览";
            // 
            // lblTemplate
            // 
            this.lblTemplate.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTemplate.Location = new System.Drawing.Point(3, 17);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.lblTemplate.Size = new System.Drawing.Size(802, 23);
            this.lblTemplate.TabIndex = 0;
            this.lblTemplate.Text = "模板: 未加载";
            // 
            // pbPreview
            // 
            this.pbPreview.BackColor = System.Drawing.Color.Black;
            this.pbPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbPreview.Location = new System.Drawing.Point(3, 17);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(802, 391);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbPreview.TabIndex = 1;
            this.pbPreview.TabStop = false;
            // 
            // splitContainerLog
            // 
            this.splitContainerLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerLog.Location = new System.Drawing.Point(0, 0);
            this.splitContainerLog.Name = "splitContainerLog";
            this.splitContainerLog.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainerLog.Panel1Collapsed = true;
            this.splitContainerLog.Size = new System.Drawing.Size(808, 164);
            this.splitContainerLog.SplitterDistance = 25;
            this.splitContainerLog.TabIndex = 0;
            // 
            // splitContainerRightOuter
            // 
            this.splitContainerRightOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRightOuter.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRightOuter.Name = "splitContainerRightOuter";
            this.splitContainerRightOuter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRightOuter.Panel1
            // 
            this.splitContainerRightOuter.Panel1.Controls.Add(this.gbReserved);
            // 
            // splitContainerRightOuter.Panel2
            // 
            this.splitContainerRightOuter.Panel2.Controls.Add(this.splitContainerRightInner);
            this.splitContainerRightOuter.Size = new System.Drawing.Size(268, 579);
            this.splitContainerRightOuter.SplitterDistance = 97;
            this.splitContainerRightOuter.TabIndex = 0;
            // 
            // gbReserved
            // 
            this.gbReserved.Controls.Add(this.lblReserved);
            this.gbReserved.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbReserved.Location = new System.Drawing.Point(0, 0);
            this.gbReserved.Name = "gbReserved";
            this.gbReserved.Size = new System.Drawing.Size(268, 97);
            this.gbReserved.TabIndex = 0;
            this.gbReserved.TabStop = false;
            this.gbReserved.Text = "预留区域";
            // 
            // lblReserved
            // 
            this.lblReserved.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblReserved.Location = new System.Drawing.Point(3, 17);
            this.lblReserved.Name = "lblReserved";
            this.lblReserved.Size = new System.Drawing.Size(262, 77);
            this.lblReserved.TabIndex = 0;
            this.lblReserved.Text = "（预留）";
            this.lblReserved.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainerRightInner
            // 
            this.splitContainerRightInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerRightInner.Location = new System.Drawing.Point(0, 0);
            this.splitContainerRightInner.Name = "splitContainerRightInner";
            this.splitContainerRightInner.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerRightInner.Panel1
            // 
            this.splitContainerRightInner.Panel1.Controls.Add(this.gbPosition);
            // 
            // splitContainerRightInner.Panel2
            // 
            this.splitContainerRightInner.Panel2.Controls.Add(this.gbMarkButtons);
            this.splitContainerRightInner.Size = new System.Drawing.Size(268, 478);
            this.splitContainerRightInner.SplitterDistance = 339;
            this.splitContainerRightInner.TabIndex = 0;
            // 
            // gbPosition
            // 
            this.gbPosition.Controls.Add(this.btResetOffset);
            this.gbPosition.Controls.Add(this.lblOffset);
            this.gbPosition.Controls.Add(this.tlbPosition);
            this.gbPosition.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbPosition.Location = new System.Drawing.Point(0, 0);
            this.gbPosition.Name = "gbPosition";
            this.gbPosition.Size = new System.Drawing.Size(268, 339);
            this.gbPosition.TabIndex = 0;
            this.gbPosition.TabStop = false;
            this.gbPosition.Text = "位置调整（加工时生效）";
            // 
            // btResetOffset
            // 
            this.btResetOffset.Dock = System.Windows.Forms.DockStyle.Top;
            this.btResetOffset.Location = new System.Drawing.Point(3, 190);
            this.btResetOffset.Margin = new System.Windows.Forms.Padding(12, 4, 12, 8);
            this.btResetOffset.Name = "btResetOffset";
            this.btResetOffset.Size = new System.Drawing.Size(262, 32);
            this.btResetOffset.TabIndex = 2;
            this.btResetOffset.Text = "复位偏移";
            this.btResetOffset.Click += new System.EventHandler(this.btResetOffset_Click);
            // 
            // lblOffset
            // 
            this.lblOffset.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOffset.Location = new System.Drawing.Point(3, 167);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Padding = new System.Windows.Forms.Padding(12, 8, 8, 4);
            this.lblOffset.Size = new System.Drawing.Size(262, 23);
            this.lblOffset.TabIndex = 1;
            this.lblOffset.Text = "X:0.000  Y:0.000";
            this.lblOffset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tlbPosition
            // 
            this.tlbPosition.ColumnCount = 3;
            this.tlbPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlbPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tlbPosition.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlbPosition.Controls.Add(this.btMoveUp, 1, 0);
            this.tlbPosition.Controls.Add(this.btMoveLeft, 0, 1);
            this.tlbPosition.Controls.Add(this.btMoveRight, 2, 1);
            this.tlbPosition.Controls.Add(this.btMoveDown, 1, 2);
            this.tlbPosition.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlbPosition.Location = new System.Drawing.Point(3, 17);
            this.tlbPosition.Name = "tlbPosition";
            this.tlbPosition.Padding = new System.Windows.Forms.Padding(8);
            this.tlbPosition.RowCount = 3;
            this.tlbPosition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlbPosition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tlbPosition.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlbPosition.Size = new System.Drawing.Size(262, 150);
            this.tlbPosition.TabIndex = 0;
            // 
            // btMoveUp
            // 
            this.btMoveUp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btMoveUp.Location = new System.Drawing.Point(100, 12);
            this.btMoveUp.Name = "btMoveUp";
            this.btMoveUp.Size = new System.Drawing.Size(60, 36);
            this.btMoveUp.TabIndex = 0;
            this.btMoveUp.Text = "↑";
            this.btMoveUp.Click += new System.EventHandler(this.btMoveUp_Click);
            // 
            // btMoveLeft
            // 
            this.btMoveLeft.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btMoveLeft.Location = new System.Drawing.Point(18, 56);
            this.btMoveLeft.Name = "btMoveLeft";
            this.btMoveLeft.Size = new System.Drawing.Size(60, 36);
            this.btMoveLeft.TabIndex = 1;
            this.btMoveLeft.Text = "←";
            this.btMoveLeft.Click += new System.EventHandler(this.btMoveLeft_Click);
            // 
            // btMoveRight
            // 
            this.btMoveRight.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btMoveRight.Location = new System.Drawing.Point(182, 56);
            this.btMoveRight.Name = "btMoveRight";
            this.btMoveRight.Size = new System.Drawing.Size(60, 36);
            this.btMoveRight.TabIndex = 2;
            this.btMoveRight.Text = "→";
            this.btMoveRight.Click += new System.EventHandler(this.btMoveRight_Click);
            // 
            // btMoveDown
            // 
            this.btMoveDown.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btMoveDown.Location = new System.Drawing.Point(100, 101);
            this.btMoveDown.Name = "btMoveDown";
            this.btMoveDown.Size = new System.Drawing.Size(60, 36);
            this.btMoveDown.TabIndex = 3;
            this.btMoveDown.Text = "↓";
            this.btMoveDown.Click += new System.EventHandler(this.btMoveDown_Click);
            // 
            // gbMarkButtons
            // 
            this.gbMarkButtons.Controls.Add(this.tlbMarkButtons);
            this.gbMarkButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMarkButtons.Location = new System.Drawing.Point(0, 0);
            this.gbMarkButtons.Name = "gbMarkButtons";
            this.gbMarkButtons.Size = new System.Drawing.Size(268, 135);
            this.gbMarkButtons.TabIndex = 0;
            this.gbMarkButtons.TabStop = false;
            this.gbMarkButtons.Text = "加工控制";
            // 
            // tlbMarkButtons
            // 
            this.tlbMarkButtons.ColumnCount = 1;
            this.tlbMarkButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlbMarkButtons.Controls.Add(this.btRedMark, 0, 0);
            this.tlbMarkButtons.Controls.Add(this.btMarkOnce, 0, 1);
            this.tlbMarkButtons.Controls.Add(this.btStop, 0, 2);
            this.tlbMarkButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlbMarkButtons.Location = new System.Drawing.Point(3, 17);
            this.tlbMarkButtons.Name = "tlbMarkButtons";
            this.tlbMarkButtons.Padding = new System.Windows.Forms.Padding(12, 4, 12, 8);
            this.tlbMarkButtons.RowCount = 3;
            this.tlbMarkButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlbMarkButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tlbMarkButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tlbMarkButtons.Size = new System.Drawing.Size(262, 115);
            this.tlbMarkButtons.TabIndex = 0;
            // 
            // btRedMark
            // 
            this.btRedMark.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btRedMark.Location = new System.Drawing.Point(12, 4);
            this.btRedMark.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btRedMark.Name = "btRedMark";
            this.btRedMark.Size = new System.Drawing.Size(238, 30);
            this.btRedMark.TabIndex = 0;
            this.btRedMark.Text = "红光显示";
            this.btRedMark.UseVisualStyleBackColor = true;
            this.btRedMark.Click += new System.EventHandler(this.btRedMark_Click);
            // 
            // btMarkOnce
            // 
            this.btMarkOnce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btMarkOnce.Location = new System.Drawing.Point(12, 38);
            this.btMarkOnce.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.btMarkOnce.Name = "btMarkOnce";
            this.btMarkOnce.Size = new System.Drawing.Size(238, 30);
            this.btMarkOnce.TabIndex = 1;
            this.btMarkOnce.Text = "单次标刻";
            this.btMarkOnce.UseVisualStyleBackColor = true;
            this.btMarkOnce.Click += new System.EventHandler(this.btMarkOnce_Click);
            // 
            // btStop
            // 
            this.btStop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btStop.Location = new System.Drawing.Point(12, 72);
            this.btStop.Margin = new System.Windows.Forms.Padding(0);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(238, 35);
            this.btStop.TabIndex = 2;
            this.btStop.Text = "停止加工";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // timerPreview
            // 
            this.timerPreview.Interval = 800;
            this.timerPreview.Tick += new System.EventHandler(this.timerPreview_Tick);
            // 
            // toolTipMain
            // 
            this.toolTipMain.AutoPopDelay = 8000;
            this.toolTipMain.InitialDelay = 300;
            this.toolTipMain.ReshowDelay = 100;
            this.toolTipMain.ShowAlways = true;
            // 
            // FormMain
            // 
            this.ClientSize = new System.Drawing.Size(1080, 661);
            this.Controls.Add(this.splitContainerMain);
            this.Controls.Add(this.panelToolbar);
            this.Controls.Add(this.statusStrip1);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "嘉泰激光打标系统";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelToolbar.ResumeLayout(false);
            this.tlbToolbar.ResumeLayout(false);
            this.splitContainerMain.Panel1.ResumeLayout(false);
            this.splitContainerMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMain)).EndInit();
            this.splitContainerMain.ResumeLayout(false);
            this.splitContainerLeft.Panel1.ResumeLayout(false);
            this.splitContainerLeft.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLeft)).EndInit();
            this.splitContainerLeft.ResumeLayout(false);
            this.gbPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerLog)).EndInit();
            this.splitContainerLog.ResumeLayout(false);
            this.splitContainerRightOuter.Panel1.ResumeLayout(false);
            this.splitContainerRightOuter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRightOuter)).EndInit();
            this.splitContainerRightOuter.ResumeLayout(false);
            this.gbReserved.ResumeLayout(false);
            this.splitContainerRightInner.Panel1.ResumeLayout(false);
            this.splitContainerRightInner.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerRightInner)).EndInit();
            this.splitContainerRightInner.ResumeLayout(false);
            this.gbPosition.ResumeLayout(false);
            this.tlbPosition.ResumeLayout(false);
            this.gbMarkButtons.ResumeLayout(false);
            this.tlbMarkButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Status;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.Button btOpenFile;
        private System.Windows.Forms.Button btWebApi;
        private System.Windows.Forms.Button btSystem;
        private System.Windows.Forms.SplitContainer splitContainerMain;
        private System.Windows.Forms.SplitContainer splitContainerLeft;
        private System.Windows.Forms.GroupBox gbPreview;
        private System.Windows.Forms.Label lblTemplate;
        private System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.SplitContainer splitContainerLog;
        private System.Windows.Forms.SplitContainer splitContainerRightOuter;
        private System.Windows.Forms.SplitContainer splitContainerRightInner;
        private System.Windows.Forms.GroupBox gbReserved;
        private System.Windows.Forms.Label lblReserved;
        private System.Windows.Forms.GroupBox gbPosition;
        private System.Windows.Forms.Button btMoveLeft;
        private System.Windows.Forms.Button btMoveUp;
        private System.Windows.Forms.Button btMoveRight;
        private System.Windows.Forms.Button btMoveDown;
        private System.Windows.Forms.Label lblOffset;
        private System.Windows.Forms.Button btResetOffset;
        private System.Windows.Forms.GroupBox gbMarkButtons;
        private System.Windows.Forms.Button btRedMark;
        private System.Windows.Forms.Button btMarkOnce;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Timer timerPreview;
        private System.Windows.Forms.ToolTip toolTipMain;
        private System.Windows.Forms.TableLayoutPanel tlbToolbar;
        private System.Windows.Forms.TableLayoutPanel tlbPosition;
        private System.Windows.Forms.TableLayoutPanel tlbMarkButtons;
    }
}
