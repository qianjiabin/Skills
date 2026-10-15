namespace AxisStatus_Scan
{
    partial class Form1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.WatchDogTimer = new System.Windows.Forms.Timer(this.components);
            this.RealControllerRbt = new System.Windows.Forms.RadioButton();
            this.SoftControllerRbn = new System.Windows.Forms.RadioButton();
            this.ControllerIPTbox = new System.Windows.Forms.TextBox();
            this.ConnectControllerBtn = new System.Windows.Forms.Button();
            this.ControllerSelectGroup = new System.Windows.Forms.GroupBox();
            this.DisconControllerBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ControllerStateLable = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.StopMoveCmdsAndClearBufferBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.MoveMark_Gourp0_Label = new System.Windows.Forms.Label();
            this.MoveMark_Gourp1_Label = new System.Windows.Forms.Label();
            this.RemainLineBuffer_Group0_Label = new System.Windows.Forms.Label();
            this.RemainLineBuffer_Group1_Label = new System.Windows.Forms.Label();
            this.RemainBuffer_Group0_Label = new System.Windows.Forms.Label();
            this.RemainBuffer_Group1_Label = new System.Windows.Forms.Label();
            this.CheckBufferTimer = new System.Windows.Forms.Timer(this.components);
            this.MoveCurMark_Gourp1_Label = new System.Windows.Forms.Label();
            this.MoveCurMark_Gourp0_Label = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.MoveCmdRet_Group0_Label = new System.Windows.Forms.Label();
            this.MoveCmdRet_Group1_Label = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Mtype_Group1_Label = new System.Windows.Forms.Label();
            this.Mtype_Group0_Label = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.PauseBetweenRetryTBox = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.MoveCmdCountsTBox = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.MultiGroupConflictBtn = new System.Windows.Forms.Button();
            this.RunMultiGroupBtn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label25 = new System.Windows.Forms.Label();
            this.MovePauseTestBtn = new System.Windows.Forms.Button();
            this.label26 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SetSignalToTrueBtn = new System.Windows.Forms.Button();
            this.MoveWaitTestBtn = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.MoveTaskBtn = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.MoveOP2Btn = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.MoveOPBtn = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // WatchDogTimer
            // 
            this.WatchDogTimer.Interval = 500;
            this.WatchDogTimer.Tick += new System.EventHandler(this.WatchDogTimer_Tick);
            // 
            // RealControllerRbt
            // 
            this.RealControllerRbt.AutoSize = true;
            this.RealControllerRbt.Checked = true;
            this.RealControllerRbt.Location = new System.Drawing.Point(25, 14);
            this.RealControllerRbt.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RealControllerRbt.Name = "RealControllerRbt";
            this.RealControllerRbt.Size = new System.Drawing.Size(59, 16);
            this.RealControllerRbt.TabIndex = 73;
            this.RealControllerRbt.TabStop = true;
            this.RealControllerRbt.Text = "控制器";
            this.RealControllerRbt.UseVisualStyleBackColor = true;
            // 
            // SoftControllerRbn
            // 
            this.SoftControllerRbn.AutoSize = true;
            this.SoftControllerRbn.Location = new System.Drawing.Point(25, 37);
            this.SoftControllerRbn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SoftControllerRbn.Name = "SoftControllerRbn";
            this.SoftControllerRbn.Size = new System.Drawing.Size(65, 16);
            this.SoftControllerRbn.TabIndex = 74;
            this.SoftControllerRbn.Text = "软核RT7";
            this.SoftControllerRbn.UseVisualStyleBackColor = true;
            // 
            // ControllerIPTbox
            // 
            this.ControllerIPTbox.Location = new System.Drawing.Point(119, 14);
            this.ControllerIPTbox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ControllerIPTbox.Name = "ControllerIPTbox";
            this.ControllerIPTbox.Size = new System.Drawing.Size(88, 21);
            this.ControllerIPTbox.TabIndex = 75;
            this.ControllerIPTbox.Text = "192.168.0.11";
            // 
            // ConnectControllerBtn
            // 
            this.ConnectControllerBtn.Location = new System.Drawing.Point(245, 4);
            this.ConnectControllerBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ConnectControllerBtn.Name = "ConnectControllerBtn";
            this.ConnectControllerBtn.Size = new System.Drawing.Size(56, 23);
            this.ConnectControllerBtn.TabIndex = 77;
            this.ConnectControllerBtn.Text = "连接";
            this.ConnectControllerBtn.UseVisualStyleBackColor = true;
            this.ConnectControllerBtn.Click += new System.EventHandler(this.ConnectControllerBtn_Click);
            // 
            // ControllerSelectGroup
            // 
            this.ControllerSelectGroup.Location = new System.Drawing.Point(17, 1);
            this.ControllerSelectGroup.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ControllerSelectGroup.Name = "ControllerSelectGroup";
            this.ControllerSelectGroup.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ControllerSelectGroup.Size = new System.Drawing.Size(210, 58);
            this.ControllerSelectGroup.TabIndex = 80;
            this.ControllerSelectGroup.TabStop = false;
            // 
            // DisconControllerBtn
            // 
            this.DisconControllerBtn.Location = new System.Drawing.Point(245, 32);
            this.DisconControllerBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.DisconControllerBtn.Name = "DisconControllerBtn";
            this.DisconControllerBtn.Size = new System.Drawing.Size(56, 23);
            this.DisconControllerBtn.TabIndex = 81;
            this.DisconControllerBtn.Text = "断开";
            this.DisconControllerBtn.UseVisualStyleBackColor = true;
            this.DisconControllerBtn.Click += new System.EventHandler(this.DisconControllerBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(359, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 83;
            this.label3.Text = "报警历史：";
            // 
            // ControllerStateLable
            // 
            this.ControllerStateLable.AutoSize = true;
            this.ControllerStateLable.ForeColor = System.Drawing.Color.Red;
            this.ControllerStateLable.Location = new System.Drawing.Point(433, 10);
            this.ControllerStateLable.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ControllerStateLable.Name = "ControllerStateLable";
            this.ControllerStateLable.Size = new System.Drawing.Size(113, 12);
            this.ControllerStateLable.TabIndex = 84;
            this.ControllerStateLable.Text = "控制器已断开连接！";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 543);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(269, 12);
            this.label6.TabIndex = 101;
            this.label6.Text = "剩余缓冲数(按复杂运动指令算)(Remain_Buffer):";
            // 
            // StopMoveCmdsAndClearBufferBtn
            // 
            this.StopMoveCmdsAndClearBufferBtn.BackColor = System.Drawing.Color.BurlyWood;
            this.StopMoveCmdsAndClearBufferBtn.Location = new System.Drawing.Point(362, 39);
            this.StopMoveCmdsAndClearBufferBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.StopMoveCmdsAndClearBufferBtn.Name = "StopMoveCmdsAndClearBufferBtn";
            this.StopMoveCmdsAndClearBufferBtn.Size = new System.Drawing.Size(196, 30);
            this.StopMoveCmdsAndClearBufferBtn.TabIndex = 100;
            this.StopMoveCmdsAndClearBufferBtn.Text = "停止指令下发+清空缓冲";
            this.StopMoveCmdsAndClearBufferBtn.UseVisualStyleBackColor = false;
            this.StopMoveCmdsAndClearBufferBtn.Click += new System.EventHandler(this.StopMoveCmdsAndClearBufferBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 518);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(251, 12);
            this.label2.TabIndex = 99;
            this.label2.Text = "剩余缓冲数(按直线计算)(Remain_Buffer(1)):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 469);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 12);
            this.label4.TabIndex = 98;
            this.label4.Text = "缓冲区运动指令队列(Move_Mark)):";
            // 
            // MoveMark_Gourp0_Label
            // 
            this.MoveMark_Gourp0_Label.AutoSize = true;
            this.MoveMark_Gourp0_Label.Location = new System.Drawing.Point(400, 469);
            this.MoveMark_Gourp0_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MoveMark_Gourp0_Label.Name = "MoveMark_Gourp0_Label";
            this.MoveMark_Gourp0_Label.Size = new System.Drawing.Size(17, 12);
            this.MoveMark_Gourp0_Label.TabIndex = 108;
            this.MoveMark_Gourp0_Label.Text = "NA";
            // 
            // MoveMark_Gourp1_Label
            // 
            this.MoveMark_Gourp1_Label.AutoSize = true;
            this.MoveMark_Gourp1_Label.Location = new System.Drawing.Point(724, 469);
            this.MoveMark_Gourp1_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MoveMark_Gourp1_Label.Name = "MoveMark_Gourp1_Label";
            this.MoveMark_Gourp1_Label.Size = new System.Drawing.Size(17, 12);
            this.MoveMark_Gourp1_Label.TabIndex = 109;
            this.MoveMark_Gourp1_Label.Text = "NA";
            // 
            // RemainLineBuffer_Group0_Label
            // 
            this.RemainLineBuffer_Group0_Label.AutoSize = true;
            this.RemainLineBuffer_Group0_Label.Location = new System.Drawing.Point(400, 518);
            this.RemainLineBuffer_Group0_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RemainLineBuffer_Group0_Label.Name = "RemainLineBuffer_Group0_Label";
            this.RemainLineBuffer_Group0_Label.Size = new System.Drawing.Size(17, 12);
            this.RemainLineBuffer_Group0_Label.TabIndex = 110;
            this.RemainLineBuffer_Group0_Label.Text = "NA";
            // 
            // RemainLineBuffer_Group1_Label
            // 
            this.RemainLineBuffer_Group1_Label.AutoSize = true;
            this.RemainLineBuffer_Group1_Label.Location = new System.Drawing.Point(724, 518);
            this.RemainLineBuffer_Group1_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RemainLineBuffer_Group1_Label.Name = "RemainLineBuffer_Group1_Label";
            this.RemainLineBuffer_Group1_Label.Size = new System.Drawing.Size(17, 12);
            this.RemainLineBuffer_Group1_Label.TabIndex = 111;
            this.RemainLineBuffer_Group1_Label.Text = "NA";
            // 
            // RemainBuffer_Group0_Label
            // 
            this.RemainBuffer_Group0_Label.AutoSize = true;
            this.RemainBuffer_Group0_Label.Location = new System.Drawing.Point(400, 543);
            this.RemainBuffer_Group0_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RemainBuffer_Group0_Label.Name = "RemainBuffer_Group0_Label";
            this.RemainBuffer_Group0_Label.Size = new System.Drawing.Size(17, 12);
            this.RemainBuffer_Group0_Label.TabIndex = 112;
            this.RemainBuffer_Group0_Label.Text = "NA";
            // 
            // RemainBuffer_Group1_Label
            // 
            this.RemainBuffer_Group1_Label.AutoSize = true;
            this.RemainBuffer_Group1_Label.Location = new System.Drawing.Point(724, 543);
            this.RemainBuffer_Group1_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RemainBuffer_Group1_Label.Name = "RemainBuffer_Group1_Label";
            this.RemainBuffer_Group1_Label.Size = new System.Drawing.Size(17, 12);
            this.RemainBuffer_Group1_Label.TabIndex = 113;
            this.RemainBuffer_Group1_Label.Text = "NA";
            // 
            // CheckBufferTimer
            // 
            this.CheckBufferTimer.Interval = 500;
            this.CheckBufferTimer.Tick += new System.EventHandler(this.CheckBufferTimer_Tick);
            // 
            // MoveCurMark_Gourp1_Label
            // 
            this.MoveCurMark_Gourp1_Label.AutoSize = true;
            this.MoveCurMark_Gourp1_Label.Location = new System.Drawing.Point(724, 494);
            this.MoveCurMark_Gourp1_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MoveCurMark_Gourp1_Label.Name = "MoveCurMark_Gourp1_Label";
            this.MoveCurMark_Gourp1_Label.Size = new System.Drawing.Size(17, 12);
            this.MoveCurMark_Gourp1_Label.TabIndex = 118;
            this.MoveCurMark_Gourp1_Label.Text = "NA";
            // 
            // MoveCurMark_Gourp0_Label
            // 
            this.MoveCurMark_Gourp0_Label.AutoSize = true;
            this.MoveCurMark_Gourp0_Label.Location = new System.Drawing.Point(400, 494);
            this.MoveCurMark_Gourp0_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MoveCurMark_Gourp0_Label.Name = "MoveCurMark_Gourp0_Label";
            this.MoveCurMark_Gourp0_Label.Size = new System.Drawing.Size(17, 12);
            this.MoveCurMark_Gourp0_Label.TabIndex = 117;
            this.MoveCurMark_Gourp0_Label.Text = "NA";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(15, 494);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(167, 12);
            this.label15.TabIndex = 116;
            this.label15.Text = "当前运行队列(Move_CurMark):";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(400, 444);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 12);
            this.label7.TabIndex = 119;
            this.label7.Text = "轴0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(723, 444);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(23, 12);
            this.label14.TabIndex = 120;
            this.label14.Text = "轴2";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(15, 568);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(155, 12);
            this.label16.TabIndex = 121;
            this.label16.Text = "运动指令返回值(更新较慢):";
            // 
            // MoveCmdRet_Group0_Label
            // 
            this.MoveCmdRet_Group0_Label.AutoSize = true;
            this.MoveCmdRet_Group0_Label.Location = new System.Drawing.Point(400, 568);
            this.MoveCmdRet_Group0_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MoveCmdRet_Group0_Label.Name = "MoveCmdRet_Group0_Label";
            this.MoveCmdRet_Group0_Label.Size = new System.Drawing.Size(17, 12);
            this.MoveCmdRet_Group0_Label.TabIndex = 122;
            this.MoveCmdRet_Group0_Label.Text = "NA";
            // 
            // MoveCmdRet_Group1_Label
            // 
            this.MoveCmdRet_Group1_Label.AutoSize = true;
            this.MoveCmdRet_Group1_Label.Location = new System.Drawing.Point(724, 568);
            this.MoveCmdRet_Group1_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MoveCmdRet_Group1_Label.Name = "MoveCmdRet_Group1_Label";
            this.MoveCmdRet_Group1_Label.Size = new System.Drawing.Size(17, 12);
            this.MoveCmdRet_Group1_Label.TabIndex = 123;
            this.MoveCmdRet_Group1_Label.Text = "NA";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Mtype_Group1_Label);
            this.groupBox1.Controls.Add(this.Mtype_Group0_Label);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Location = new System.Drawing.Point(9, 428);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(923, 188);
            this.groupBox1.TabIndex = 130;
            this.groupBox1.TabStop = false;
            // 
            // Mtype_Group1_Label
            // 
            this.Mtype_Group1_Label.AutoSize = true;
            this.Mtype_Group1_Label.Location = new System.Drawing.Point(716, 163);
            this.Mtype_Group1_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Mtype_Group1_Label.Name = "Mtype_Group1_Label";
            this.Mtype_Group1_Label.Size = new System.Drawing.Size(17, 12);
            this.Mtype_Group1_Label.TabIndex = 134;
            this.Mtype_Group1_Label.Text = "NA";
            // 
            // Mtype_Group0_Label
            // 
            this.Mtype_Group0_Label.AutoSize = true;
            this.Mtype_Group0_Label.Location = new System.Drawing.Point(391, 163);
            this.Mtype_Group0_Label.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Mtype_Group0_Label.Name = "Mtype_Group0_Label";
            this.Mtype_Group0_Label.Size = new System.Drawing.Size(17, 12);
            this.Mtype_Group0_Label.TabIndex = 133;
            this.Mtype_Group0_Label.Text = "NA";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(6, 163);
            this.label23.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(101, 12);
            this.label23.TabIndex = 133;
            this.label23.Text = "运动类型(Mtype):";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(2, 79);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(929, 332);
            this.tabControl1.TabIndex = 131;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label22);
            this.tabPage1.Controls.Add(this.PauseBetweenRetryTBox);
            this.tabPage1.Controls.Add(this.label21);
            this.tabPage1.Controls.Add(this.MoveCmdCountsTBox);
            this.tabPage1.Controls.Add(this.label20);
            this.tabPage1.Controls.Add(this.label19);
            this.tabPage1.Controls.Add(this.label18);
            this.tabPage1.Controls.Add(this.MultiGroupConflictBtn);
            this.tabPage1.Controls.Add(this.RunMultiGroupBtn);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage1.Size = new System.Drawing.Size(921, 302);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "多组多线程";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(458, 194);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(132, 17);
            this.label12.TabIndex = 131;
            this.label12.Text = "联动组2: Base(2, 3)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(323, 194);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(132, 17);
            this.label13.TabIndex = 130;
            this.label13.Text = "联动组1: Base(0, 1)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(323, 170);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 17);
            this.label1.TabIndex = 134;
            this.label1.Text = "多联动系多线程并行运行测试:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 194);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(166, 17);
            this.label5.TabIndex = 140;
            this.label5.Text = "缓冲区满后延时重试(ms):";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(32, 170);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 17);
            this.label11.TabIndex = 132;
            this.label11.Text = "运动指令条数:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(32, 119);
            this.label22.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(696, 17);
            this.label22.TabIndex = 147;
            this.label22.Text = "如果不用这种方法, 在自动运行线程没停掉的情况下直接发Cancel/Rapidstop, 有可能电机停不稳, dpos也会出错.";
            // 
            // PauseBetweenRetryTBox
            // 
            this.PauseBetweenRetryTBox.Location = new System.Drawing.Point(200, 192);
            this.PauseBetweenRetryTBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PauseBetweenRetryTBox.Name = "PauseBetweenRetryTBox";
            this.PauseBetweenRetryTBox.Size = new System.Drawing.Size(76, 23);
            this.PauseBetweenRetryTBox.TabIndex = 141;
            this.PauseBetweenRetryTBox.Text = "10";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(32, 96);
            this.label21.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(786, 17);
            this.label21.TabIndex = 146;
            this.label21.Text = "大致流程是先发MovePause暂停电机, 然后通过CancelToken停止自动运行的线程, 自动运行的线程自身发控制器的cancel指令.";
            // 
            // MoveCmdCountsTBox
            // 
            this.MoveCmdCountsTBox.Location = new System.Drawing.Point(200, 167);
            this.MoveCmdCountsTBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MoveCmdCountsTBox.Name = "MoveCmdCountsTBox";
            this.MoveCmdCountsTBox.Size = new System.Drawing.Size(76, 23);
            this.MoveCmdCountsTBox.TabIndex = 133;
            this.MoveCmdCountsTBox.Text = "10000";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(32, 73);
            this.label20.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(656, 17);
            this.label20.TabIndex = 145;
            this.label20.Text = "这种方法只是演示其中一种方案, 并非强制要求方法, 但这种方法在多线程环境下响应快速安全, 值得参考. ";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(32, 50);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(392, 17);
            this.label19.TabIndex = 144;
            this.label19.Text = "2. 本示例演示了如何快速安全的停止另外一个线程的运动指令.";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(32, 26);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(336, 17);
            this.label18.TabIndex = 143;
            this.label18.Text = "1. 本示例演示了正运动支持多工位多联动组并行工作.";
            // 
            // MultiGroupConflictBtn
            // 
            this.MultiGroupConflictBtn.Location = new System.Drawing.Point(652, 246);
            this.MultiGroupConflictBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MultiGroupConflictBtn.Name = "MultiGroupConflictBtn";
            this.MultiGroupConflictBtn.Size = new System.Drawing.Size(196, 30);
            this.MultiGroupConflictBtn.TabIndex = 139;
            this.MultiGroupConflictBtn.Text = "开始多组冲突测试";
            this.MultiGroupConflictBtn.UseVisualStyleBackColor = true;
            this.MultiGroupConflictBtn.Click += new System.EventHandler(this.MultiGroupConflictBtn_Click);
            // 
            // RunMultiGroupBtn
            // 
            this.RunMultiGroupBtn.Location = new System.Drawing.Point(329, 246);
            this.RunMultiGroupBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.RunMultiGroupBtn.Name = "RunMultiGroupBtn";
            this.RunMultiGroupBtn.Size = new System.Drawing.Size(196, 30);
            this.RunMultiGroupBtn.TabIndex = 138;
            this.RunMultiGroupBtn.Text = "开始多组并行运行";
            this.RunMultiGroupBtn.UseVisualStyleBackColor = true;
            this.RunMultiGroupBtn.Click += new System.EventHandler(this.RunMultiGroupBtn_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(774, 194);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(132, 17);
            this.label10.TabIndex = 137;
            this.label10.Text = "联动组2: Base(2, 1)";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(646, 194);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 17);
            this.label9.TabIndex = 136;
            this.label9.Text = "联动组1: Base(0, 1)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(646, 170);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(176, 17);
            this.label8.TabIndex = 135;
            this.label8.Text = "轴号冲突测试(有相同从轴):";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage2.Controls.Add(this.label25);
            this.tabPage2.Controls.Add(this.MovePauseTestBtn);
            this.tabPage2.Controls.Add(this.label26);
            this.tabPage2.Controls.Add(this.textBox1);
            this.tabPage2.Controls.Add(this.SetSignalToTrueBtn);
            this.tabPage2.Controls.Add(this.MoveWaitTestBtn);
            this.tabPage2.Controls.Add(this.label24);
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(921, 302);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Move_Wait等待";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(572, 140);
            this.label25.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(110, 17);
            this.label25.TabIndex = 144;
            this.label25.Text = "Move_Wait测试:";
            // 
            // MovePauseTestBtn
            // 
            this.MovePauseTestBtn.Location = new System.Drawing.Point(590, 262);
            this.MovePauseTestBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MovePauseTestBtn.Name = "MovePauseTestBtn";
            this.MovePauseTestBtn.Size = new System.Drawing.Size(135, 30);
            this.MovePauseTestBtn.TabIndex = 143;
            this.MovePauseTestBtn.Text = "连续下载执行";
            this.MovePauseTestBtn.UseVisualStyleBackColor = true;
            this.MovePauseTestBtn.Click += new System.EventHandler(this.MovePauseTestBtn_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(572, 226);
            this.label26.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(232, 17);
            this.label26.TabIndex = 141;
            this.label26.Text = "Move_Pause + Move_Resume测试:";
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(566, 5);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(330, 118);
            this.textBox1.TabIndex = 139;
            this.textBox1.Text = "Move_Wait可以在某个条件(例如io状态)未被触发之前下载一系列的运动指令(包括move_op等), 等到这个条件触发后, 快速连贯的执行后续的运动指令. " +
    "在大量小线段或者网络通信不稳定的时候效果显著. \r\nMove_Pause+Move_Resume使用更简单, 但要注意中间的运动指令数量不能超过当前运动控制器的" +
    "运动缓冲大小, 否则将产生死锁.";
            // 
            // SetSignalToTrueBtn
            // 
            this.SetSignalToTrueBtn.Location = new System.Drawing.Point(754, 179);
            this.SetSignalToTrueBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SetSignalToTrueBtn.Name = "SetSignalToTrueBtn";
            this.SetSignalToTrueBtn.Size = new System.Drawing.Size(142, 30);
            this.SetSignalToTrueBtn.TabIndex = 138;
            this.SetSignalToTrueBtn.Text = "将等待条件置True";
            this.SetSignalToTrueBtn.UseVisualStyleBackColor = true;
            this.SetSignalToTrueBtn.Click += new System.EventHandler(this.SetSignalToTrueBtn_Click);
            // 
            // MoveWaitTestBtn
            // 
            this.MoveWaitTestBtn.Location = new System.Drawing.Point(590, 179);
            this.MoveWaitTestBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MoveWaitTestBtn.Name = "MoveWaitTestBtn";
            this.MoveWaitTestBtn.Size = new System.Drawing.Size(142, 30);
            this.MoveWaitTestBtn.TabIndex = 136;
            this.MoveWaitTestBtn.Text = "下载运动指令";
            this.MoveWaitTestBtn.UseVisualStyleBackColor = true;
            this.MoveWaitTestBtn.Click += new System.EventHandler(this.MoveWaitTestBtn_Click);
            // 
            // label24
            // 
            this.label24.Location = new System.Drawing.Point(574, 15);
            this.label24.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label24.MaximumSize = new System.Drawing.Size(450, 0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(334, 0);
            this.label24.TabIndex = 134;
            this.label24.Text = "Move_Wait可以在某个条件触发前下载一系列的运动, 这样当这个信号触发时, 后续的运动指令能否非常快速流畅的执行. ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MoveBuffer.Properties.Resources.move_wait;
            this.pictureBox1.Location = new System.Drawing.Point(-3, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(563, 302);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.MoveTaskBtn);
            this.tabPage3.Controls.Add(this.textBox4);
            this.tabPage3.Controls.Add(this.MoveOP2Btn);
            this.tabPage3.Controls.Add(this.textBox3);
            this.tabPage3.Controls.Add(this.MoveOPBtn);
            this.tabPage3.Controls.Add(this.textBox2);
            this.tabPage3.Controls.Add(this.pictureBox2);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(921, 302);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Move_OP等";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // MoveTaskBtn
            // 
            this.MoveTaskBtn.Location = new System.Drawing.Point(573, 267);
            this.MoveTaskBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MoveTaskBtn.Name = "MoveTaskBtn";
            this.MoveTaskBtn.Size = new System.Drawing.Size(142, 30);
            this.MoveTaskBtn.TabIndex = 145;
            this.MoveTaskBtn.Text = "Move_Task测试";
            this.MoveTaskBtn.UseVisualStyleBackColor = true;
            this.MoveTaskBtn.Click += new System.EventHandler(this.MoveTaskBtn_Click);
            // 
            // textBox4
            // 
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Location = new System.Drawing.Point(408, 210);
            this.textBox4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(502, 54);
            this.textBox4.TabIndex = 144;
            this.textBox4.Text = "MoveTask是在合适的时机调用一个Basic函数, 当找不到合适的Move打头的指令实现想要的功能或者需要执行一系列复杂的操作时, 就可以用Move_Task" +
    "来实现. 比PC回调函数实时性好.";
            // 
            // MoveOP2Btn
            // 
            this.MoveOP2Btn.Location = new System.Drawing.Point(573, 161);
            this.MoveOP2Btn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MoveOP2Btn.Name = "MoveOP2Btn";
            this.MoveOP2Btn.Size = new System.Drawing.Size(142, 30);
            this.MoveOP2Btn.TabIndex = 143;
            this.MoveOP2Btn.Text = "Move_OP2测试";
            this.MoveOP2Btn.UseVisualStyleBackColor = true;
            this.MoveOP2Btn.Click += new System.EventHandler(this.MoveOP2Btn_Click);
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Location = new System.Drawing.Point(408, 107);
            this.textBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(502, 51);
            this.textBox3.TabIndex = 142;
            this.textBox3.Text = "Move_OP2和Move_OP的区别是, Move_OP2不需要用户设置关闭的位置, 控制器将按照用户指定的时间自动关闭.注意不要连续写入多条MoveOP2到运" +
    "动缓冲区,每个轴运动缓冲区仅有一个定时器计时,此时会以最后执行的Move_OP2为准.";
            // 
            // MoveOPBtn
            // 
            this.MoveOPBtn.Location = new System.Drawing.Point(573, 61);
            this.MoveOPBtn.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MoveOPBtn.Name = "MoveOPBtn";
            this.MoveOPBtn.Size = new System.Drawing.Size(142, 30);
            this.MoveOPBtn.TabIndex = 141;
            this.MoveOPBtn.Text = "Move_OP测试";
            this.MoveOPBtn.UseVisualStyleBackColor = true;
            this.MoveOPBtn.Click += new System.EventHandler(this.MoveOPBtn_Click);
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Location = new System.Drawing.Point(408, 10);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(502, 50);
            this.textBox2.TabIndex = 140;
            this.textBox2.Text = "Move_OP经常用于点胶/激光切割等场合, 可以在运动指令中塞入Move_OP, 它将和运动指令一起进入运动缓冲区排队执行. 需要用户指定开启io的位置和关闭的" +
    "位置.";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::MoveBuffer.Properties.Resources.moveop;
            this.pictureBox2.Location = new System.Drawing.Point(-1, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(400, 306);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(954, 640);
            this.Controls.Add(this.MoveCmdRet_Group1_Label);
            this.Controls.Add(this.MoveCmdRet_Group0_Label);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.MoveCurMark_Gourp1_Label);
            this.Controls.Add(this.MoveCurMark_Gourp0_Label);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.RemainBuffer_Group1_Label);
            this.Controls.Add(this.RemainBuffer_Group0_Label);
            this.Controls.Add(this.RemainLineBuffer_Group1_Label);
            this.Controls.Add(this.RemainLineBuffer_Group0_Label);
            this.Controls.Add(this.MoveMark_Gourp1_Label);
            this.Controls.Add(this.MoveMark_Gourp0_Label);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.StopMoveCmdsAndClearBufferBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ControllerStateLable);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DisconControllerBtn);
            this.Controls.Add(this.ConnectControllerBtn);
            this.Controls.Add(this.ControllerIPTbox);
            this.Controls.Add(this.SoftControllerRbn);
            this.Controls.Add(this.RealControllerRbt);
            this.Controls.Add(this.ControllerSelectGroup);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "运动缓冲区相关指令演示";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer WatchDogTimer;
        private System.Windows.Forms.RadioButton RealControllerRbt;
        private System.Windows.Forms.RadioButton SoftControllerRbn;
        private System.Windows.Forms.TextBox ControllerIPTbox;
        private System.Windows.Forms.Button ConnectControllerBtn;
        private System.Windows.Forms.GroupBox ControllerSelectGroup;
        private System.Windows.Forms.Button DisconControllerBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ControllerStateLable;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button StopMoveCmdsAndClearBufferBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label MoveMark_Gourp0_Label;
        private System.Windows.Forms.Label MoveMark_Gourp1_Label;
        private System.Windows.Forms.Label RemainLineBuffer_Group0_Label;
        private System.Windows.Forms.Label RemainLineBuffer_Group1_Label;
        private System.Windows.Forms.Label RemainBuffer_Group0_Label;
        private System.Windows.Forms.Label RemainBuffer_Group1_Label;
        private System.Windows.Forms.Timer CheckBufferTimer;
        private System.Windows.Forms.Label MoveCurMark_Gourp1_Label;
        private System.Windows.Forms.Label MoveCurMark_Gourp0_Label;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label MoveCmdRet_Group0_Label;
        private System.Windows.Forms.Label MoveCmdRet_Group1_Label;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label Mtype_Group1_Label;
        private System.Windows.Forms.Label Mtype_Group0_Label;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox PauseBetweenRetryTBox;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox MoveCmdCountsTBox;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button MultiGroupConflictBtn;
        private System.Windows.Forms.Button RunMultiGroupBtn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button MoveWaitTestBtn;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button SetSignalToTrueBtn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button MovePauseTestBtn;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button MoveTaskBtn;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button MoveOP2Btn;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button MoveOPBtn;
    }
}

