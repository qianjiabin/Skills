namespace Test_MoveSync
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
            this.btn_Close = new System.Windows.Forms.Button();
            this.btn_Connet = new System.Windows.Forms.Button();
            this.IPADDRESS = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Btn_Zero = new System.Windows.Forms.Button();
            this.Btn_Rev2 = new System.Windows.Forms.Button();
            this.Btn_Fwd2 = new System.Windows.Forms.Button();
            this.Btn_Rev1 = new System.Windows.Forms.Button();
            this.Btn_Fwd1 = new System.Windows.Forms.Button();
            this.Btn_Rev0 = new System.Windows.Forms.Button();
            this.Btn_Fwd0 = new System.Windows.Forms.Button();
            this.text_Accel2 = new System.Windows.Forms.TextBox();
            this.text_Accel1 = new System.Windows.Forms.TextBox();
            this.text_Accel0 = new System.Windows.Forms.TextBox();
            this.text_Speed2 = new System.Windows.Forms.TextBox();
            this.text_Speed1 = new System.Windows.Forms.TextBox();
            this.text_Speed0 = new System.Windows.Forms.TextBox();
            this.text_Units2 = new System.Windows.Forms.TextBox();
            this.text_Units1 = new System.Windows.Forms.TextBox();
            this.text_Units0 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.text_Mpos2 = new System.Windows.Forms.TextBox();
            this.text_Mpos1 = new System.Windows.Forms.TextBox();
            this.text_Mpos0 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.text_Dpos2 = new System.Windows.Forms.TextBox();
            this.text_Dpos1 = new System.Windows.Forms.TextBox();
            this.text_Dpos0 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Btn_TestMove = new System.Windows.Forms.Button();
            this.TextMoveAcc = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.TextMoveSp = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.TextBackTime = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.BtnVmoveR = new System.Windows.Forms.Button();
            this.Btn_Start = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.TextOffpos = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.TextSyncTime = new System.Windows.Forms.TextBox();
            this.TextAccTime = new System.Windows.Forms.TextBox();
            this.TextYpos = new System.Windows.Forms.TextBox();
            this.TextXpos = new System.Windows.Forms.TextBox();
            this.TextAngle = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.radioBtnY = new System.Windows.Forms.RadioButton();
            this.radioBtnX = new System.Windows.Forms.RadioButton();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.Text_TabNum = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.Text_TabStart = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.DataGridView2 = new System.Windows.Forms.DataGridView();
            this.锁存编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.锁存位置 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Btn_StrartRegist = new System.Windows.Forms.Button();
            this.Cbox_RigeistMode = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(250, 20);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(63, 20);
            this.btn_Close.TabIndex = 18;
            this.btn_Close.Text = "断开连接";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // btn_Connet
            // 
            this.btn_Connet.Location = new System.Drawing.Point(172, 20);
            this.btn_Connet.Name = "btn_Connet";
            this.btn_Connet.Size = new System.Drawing.Size(63, 20);
            this.btn_Connet.TabIndex = 17;
            this.btn_Connet.Text = "连接";
            this.btn_Connet.UseVisualStyleBackColor = true;
            this.btn_Connet.Click += new System.EventHandler(this.btn_Connet_Click);
            // 
            // IPADDRESS
            // 
            this.IPADDRESS.FormattingEnabled = true;
            this.IPADDRESS.Location = new System.Drawing.Point(44, 20);
            this.IPADDRESS.Name = "IPADDRESS";
            this.IPADDRESS.Size = new System.Drawing.Size(114, 20);
            this.IPADDRESS.TabIndex = 16;
            this.IPADDRESS.Text = "127.0.0.1";
            this.IPADDRESS.DropDown += new System.EventHandler(this.IPADDRESS_DropDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 15;
            this.label2.Text = "IP:";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Close);
            this.groupBox1.Controls.Add(this.btn_Connet);
            this.groupBox1.Controls.Add(this.IPADDRESS);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(4, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(328, 55);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控制器链接";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Btn_Zero);
            this.groupBox2.Controls.Add(this.Btn_Rev2);
            this.groupBox2.Controls.Add(this.Btn_Fwd2);
            this.groupBox2.Controls.Add(this.Btn_Rev1);
            this.groupBox2.Controls.Add(this.Btn_Fwd1);
            this.groupBox2.Controls.Add(this.Btn_Rev0);
            this.groupBox2.Controls.Add(this.Btn_Fwd0);
            this.groupBox2.Controls.Add(this.text_Accel2);
            this.groupBox2.Controls.Add(this.text_Accel1);
            this.groupBox2.Controls.Add(this.text_Accel0);
            this.groupBox2.Controls.Add(this.text_Speed2);
            this.groupBox2.Controls.Add(this.text_Speed1);
            this.groupBox2.Controls.Add(this.text_Speed0);
            this.groupBox2.Controls.Add(this.text_Units2);
            this.groupBox2.Controls.Add(this.text_Units1);
            this.groupBox2.Controls.Add(this.text_Units0);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(4, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(328, 529);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数设置";
            // 
            // Btn_Zero
            // 
            this.Btn_Zero.Location = new System.Drawing.Point(8, 227);
            this.Btn_Zero.Name = "Btn_Zero";
            this.Btn_Zero.Size = new System.Drawing.Size(58, 22);
            this.Btn_Zero.TabIndex = 21;
            this.Btn_Zero.Text = "坐标清0";
            this.Btn_Zero.UseVisualStyleBackColor = true;
            this.Btn_Zero.Click += new System.EventHandler(this.Btn_Zero_Click);
            // 
            // Btn_Rev2
            // 
            this.Btn_Rev2.Location = new System.Drawing.Point(216, 244);
            this.Btn_Rev2.Name = "Btn_Rev2";
            this.Btn_Rev2.Size = new System.Drawing.Size(57, 23);
            this.Btn_Rev2.TabIndex = 20;
            this.Btn_Rev2.Text = "皮带反转";
            this.Btn_Rev2.UseVisualStyleBackColor = true;
            this.Btn_Rev2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_fwd0_MouseDown);
            this.Btn_Rev2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_fwd0_MouseUp);
            // 
            // Btn_Fwd2
            // 
            this.Btn_Fwd2.Location = new System.Drawing.Point(216, 205);
            this.Btn_Fwd2.Name = "Btn_Fwd2";
            this.Btn_Fwd2.Size = new System.Drawing.Size(57, 23);
            this.Btn_Fwd2.TabIndex = 19;
            this.Btn_Fwd2.Text = "皮带正转";
            this.Btn_Fwd2.UseVisualStyleBackColor = true;
            this.Btn_Fwd2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_fwd0_MouseDown);
            this.Btn_Fwd2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_fwd0_MouseUp);
            // 
            // Btn_Rev1
            // 
            this.Btn_Rev1.Location = new System.Drawing.Point(146, 244);
            this.Btn_Rev1.Name = "Btn_Rev1";
            this.Btn_Rev1.Size = new System.Drawing.Size(57, 23);
            this.Btn_Rev1.TabIndex = 18;
            this.Btn_Rev1.Text = "Y反转";
            this.Btn_Rev1.UseVisualStyleBackColor = true;
            this.Btn_Rev1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_fwd0_MouseDown);
            this.Btn_Rev1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_fwd0_MouseUp);
            // 
            // Btn_Fwd1
            // 
            this.Btn_Fwd1.Location = new System.Drawing.Point(146, 205);
            this.Btn_Fwd1.Name = "Btn_Fwd1";
            this.Btn_Fwd1.Size = new System.Drawing.Size(57, 23);
            this.Btn_Fwd1.TabIndex = 17;
            this.Btn_Fwd1.Text = "Y正转";
            this.Btn_Fwd1.UseVisualStyleBackColor = true;
            this.Btn_Fwd1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_fwd0_MouseDown);
            this.Btn_Fwd1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_fwd0_MouseUp);
            // 
            // Btn_Rev0
            // 
            this.Btn_Rev0.Location = new System.Drawing.Point(76, 244);
            this.Btn_Rev0.Name = "Btn_Rev0";
            this.Btn_Rev0.Size = new System.Drawing.Size(57, 23);
            this.Btn_Rev0.TabIndex = 16;
            this.Btn_Rev0.Text = "X负转";
            this.Btn_Rev0.UseVisualStyleBackColor = true;
            this.Btn_Rev0.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_fwd0_MouseDown);
            this.Btn_Rev0.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_fwd0_MouseUp);
            // 
            // Btn_Fwd0
            // 
            this.Btn_Fwd0.Location = new System.Drawing.Point(76, 205);
            this.Btn_Fwd0.Name = "Btn_Fwd0";
            this.Btn_Fwd0.Size = new System.Drawing.Size(57, 23);
            this.Btn_Fwd0.TabIndex = 15;
            this.Btn_Fwd0.Text = "X正转";
            this.Btn_Fwd0.UseVisualStyleBackColor = true;
            this.Btn_Fwd0.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Btn_fwd0_MouseDown);
            this.Btn_Fwd0.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Btn_fwd0_MouseUp);
            // 
            // text_Accel2
            // 
            this.text_Accel2.Location = new System.Drawing.Point(216, 146);
            this.text_Accel2.Name = "text_Accel2";
            this.text_Accel2.Size = new System.Drawing.Size(57, 21);
            this.text_Accel2.TabIndex = 14;
            this.text_Accel2.Text = "1000";
            this.text_Accel2.TextChanged += new System.EventHandler(this.text_Accel0_TextChanged);
            // 
            // text_Accel1
            // 
            this.text_Accel1.Location = new System.Drawing.Point(146, 146);
            this.text_Accel1.Name = "text_Accel1";
            this.text_Accel1.Size = new System.Drawing.Size(57, 21);
            this.text_Accel1.TabIndex = 13;
            this.text_Accel1.Text = "1000";
            this.text_Accel1.TextChanged += new System.EventHandler(this.text_Accel0_TextChanged);
            // 
            // text_Accel0
            // 
            this.text_Accel0.Location = new System.Drawing.Point(76, 146);
            this.text_Accel0.Name = "text_Accel0";
            this.text_Accel0.Size = new System.Drawing.Size(57, 21);
            this.text_Accel0.TabIndex = 12;
            this.text_Accel0.Text = "1000";
            this.text_Accel0.TextChanged += new System.EventHandler(this.text_Accel0_TextChanged);
            // 
            // text_Speed2
            // 
            this.text_Speed2.Location = new System.Drawing.Point(216, 100);
            this.text_Speed2.Name = "text_Speed2";
            this.text_Speed2.Size = new System.Drawing.Size(57, 21);
            this.text_Speed2.TabIndex = 11;
            this.text_Speed2.Text = "30";
            this.text_Speed2.TextChanged += new System.EventHandler(this.text_Speed0_TextChanged);
            // 
            // text_Speed1
            // 
            this.text_Speed1.Location = new System.Drawing.Point(146, 100);
            this.text_Speed1.Name = "text_Speed1";
            this.text_Speed1.Size = new System.Drawing.Size(57, 21);
            this.text_Speed1.TabIndex = 10;
            this.text_Speed1.Text = "30";
            this.text_Speed1.TextChanged += new System.EventHandler(this.text_Speed0_TextChanged);
            // 
            // text_Speed0
            // 
            this.text_Speed0.Location = new System.Drawing.Point(76, 100);
            this.text_Speed0.Name = "text_Speed0";
            this.text_Speed0.Size = new System.Drawing.Size(57, 21);
            this.text_Speed0.TabIndex = 9;
            this.text_Speed0.Text = "30";
            this.text_Speed0.TextChanged += new System.EventHandler(this.text_Speed0_TextChanged);
            // 
            // text_Units2
            // 
            this.text_Units2.Location = new System.Drawing.Point(216, 60);
            this.text_Units2.Name = "text_Units2";
            this.text_Units2.Size = new System.Drawing.Size(57, 21);
            this.text_Units2.TabIndex = 8;
            this.text_Units2.Text = "100";
            this.text_Units2.TextChanged += new System.EventHandler(this.text_Units0_TextChanged);
            // 
            // text_Units1
            // 
            this.text_Units1.Location = new System.Drawing.Point(146, 60);
            this.text_Units1.Name = "text_Units1";
            this.text_Units1.Size = new System.Drawing.Size(57, 21);
            this.text_Units1.TabIndex = 7;
            this.text_Units1.Text = "100";
            this.text_Units1.TextChanged += new System.EventHandler(this.text_Units0_TextChanged);
            // 
            // text_Units0
            // 
            this.text_Units0.Location = new System.Drawing.Point(76, 60);
            this.text_Units0.Name = "text_Units0";
            this.text_Units0.Size = new System.Drawing.Size(57, 21);
            this.text_Units0.TabIndex = 6;
            this.text_Units0.Text = "100";
            this.text_Units0.TextChanged += new System.EventHandler(this.text_Units0_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(8, 149);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "加速度";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 109);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "速度";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "脉冲当量";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(232, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "皮带轴";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "Y轴";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "X轴";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.text_Mpos2);
            this.groupBox3.Controls.Add(this.text_Mpos1);
            this.groupBox3.Controls.Add(this.text_Mpos0);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.text_Dpos2);
            this.groupBox3.Controls.Add(this.text_Dpos1);
            this.groupBox3.Controls.Add(this.text_Dpos0);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(338, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(353, 106);
            this.groupBox3.TabIndex = 21;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "状态监控";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(4, 69);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 12);
            this.label12.TabIndex = 26;
            this.label12.Text = "反馈位置";
            // 
            // text_Mpos2
            // 
            this.text_Mpos2.Location = new System.Drawing.Point(212, 65);
            this.text_Mpos2.Name = "text_Mpos2";
            this.text_Mpos2.ReadOnly = true;
            this.text_Mpos2.Size = new System.Drawing.Size(62, 21);
            this.text_Mpos2.TabIndex = 25;
            this.text_Mpos2.Text = "0";
            // 
            // text_Mpos1
            // 
            this.text_Mpos1.Location = new System.Drawing.Point(137, 65);
            this.text_Mpos1.Name = "text_Mpos1";
            this.text_Mpos1.ReadOnly = true;
            this.text_Mpos1.Size = new System.Drawing.Size(62, 21);
            this.text_Mpos1.TabIndex = 24;
            this.text_Mpos1.Text = "0";
            // 
            // text_Mpos0
            // 
            this.text_Mpos0.Location = new System.Drawing.Point(62, 65);
            this.text_Mpos0.Name = "text_Mpos0";
            this.text_Mpos0.ReadOnly = true;
            this.text_Mpos0.Size = new System.Drawing.Size(62, 21);
            this.text_Mpos0.TabIndex = 23;
            this.text_Mpos0.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(4, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 12);
            this.label11.TabIndex = 22;
            this.label11.Text = "命令位置";
            // 
            // text_Dpos2
            // 
            this.text_Dpos2.Location = new System.Drawing.Point(212, 34);
            this.text_Dpos2.Name = "text_Dpos2";
            this.text_Dpos2.ReadOnly = true;
            this.text_Dpos2.Size = new System.Drawing.Size(62, 21);
            this.text_Dpos2.TabIndex = 8;
            this.text_Dpos2.Text = "0";
            // 
            // text_Dpos1
            // 
            this.text_Dpos1.Location = new System.Drawing.Point(137, 34);
            this.text_Dpos1.Name = "text_Dpos1";
            this.text_Dpos1.ReadOnly = true;
            this.text_Dpos1.Size = new System.Drawing.Size(62, 21);
            this.text_Dpos1.TabIndex = 7;
            this.text_Dpos1.Text = "0";
            // 
            // text_Dpos0
            // 
            this.text_Dpos0.Location = new System.Drawing.Point(62, 34);
            this.text_Dpos0.Name = "text_Dpos0";
            this.text_Dpos0.ReadOnly = true;
            this.text_Dpos0.Size = new System.Drawing.Size(62, 21);
            this.text_Dpos0.TabIndex = 6;
            this.text_Dpos0.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(223, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "皮带轴";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(151, 18);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "Y轴";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(79, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "X轴";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Btn_TestMove);
            this.groupBox4.Controls.Add(this.TextMoveAcc);
            this.groupBox4.Controls.Add(this.label27);
            this.groupBox4.Controls.Add(this.TextMoveSp);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.TextBackTime);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.BtnVmoveR);
            this.groupBox4.Controls.Add(this.Btn_Start);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.TextOffpos);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.TextSyncTime);
            this.groupBox4.Controls.Add(this.TextAccTime);
            this.groupBox4.Controls.Add(this.TextYpos);
            this.groupBox4.Controls.Add(this.TextXpos);
            this.groupBox4.Controls.Add(this.TextAngle);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.radioBtnY);
            this.groupBox4.Controls.Add(this.radioBtnX);
            this.groupBox4.Controls.Add(this.label19);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.checkBox2);
            this.groupBox4.Controls.Add(this.checkBox1);
            this.groupBox4.Location = new System.Drawing.Point(338, 114);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(353, 481);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "同步运动";
            // 
            // Btn_TestMove
            // 
            this.Btn_TestMove.Location = new System.Drawing.Point(254, 179);
            this.Btn_TestMove.Name = "Btn_TestMove";
            this.Btn_TestMove.Size = new System.Drawing.Size(86, 23);
            this.Btn_TestMove.TabIndex = 27;
            this.Btn_TestMove.Text = "测试轨迹运动";
            this.Btn_TestMove.UseVisualStyleBackColor = true;
            this.Btn_TestMove.Click += new System.EventHandler(this.Btn_TestMove_Click);
            // 
            // TextMoveAcc
            // 
            this.TextMoveAcc.Location = new System.Drawing.Point(270, 129);
            this.TextMoveAcc.Name = "TextMoveAcc";
            this.TextMoveAcc.Size = new System.Drawing.Size(70, 21);
            this.TextMoveAcc.TabIndex = 26;
            this.TextMoveAcc.Text = "4000";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(199, 132);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(77, 12);
            this.label27.TabIndex = 25;
            this.label27.Text = "轨迹加速度：";
            // 
            // TextMoveSp
            // 
            this.TextMoveSp.Location = new System.Drawing.Point(270, 89);
            this.TextMoveSp.Name = "TextMoveSp";
            this.TextMoveSp.Size = new System.Drawing.Size(70, 21);
            this.TextMoveSp.TabIndex = 24;
            this.TextMoveSp.Text = "800";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(199, 92);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(65, 12);
            this.label26.TabIndex = 23;
            this.label26.Text = "轨迹速度：";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(174, 315);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(17, 12);
            this.label24.TabIndex = 22;
            this.label24.Text = "ms";
            // 
            // TextBackTime
            // 
            this.TextBackTime.Location = new System.Drawing.Point(101, 311);
            this.TextBackTime.Name = "TextBackTime";
            this.TextBackTime.Size = new System.Drawing.Size(70, 21);
            this.TextBackTime.TabIndex = 21;
            this.TextBackTime.Text = "500";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(7, 314);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(101, 12);
            this.label25.TabIndex = 20;
            this.label25.Text = "返回等待位时间：";
            // 
            // BtnVmoveR
            // 
            this.BtnVmoveR.Location = new System.Drawing.Point(21, 364);
            this.BtnVmoveR.Name = "BtnVmoveR";
            this.BtnVmoveR.Size = new System.Drawing.Size(75, 23);
            this.BtnVmoveR.TabIndex = 19;
            this.BtnVmoveR.Text = "启动皮带轴";
            this.BtnVmoveR.UseVisualStyleBackColor = true;
            this.BtnVmoveR.Click += new System.EventHandler(this.BtnVmoveR_Click);
            // 
            // Btn_Start
            // 
            this.Btn_Start.Location = new System.Drawing.Point(21, 418);
            this.Btn_Start.Name = "Btn_Start";
            this.Btn_Start.Size = new System.Drawing.Size(75, 23);
            this.Btn_Start.TabIndex = 18;
            this.Btn_Start.Text = "启动同步";
            this.Btn_Start.UseVisualStyleBackColor = true;
            this.Btn_Start.Click += new System.EventHandler(this.Btn_Start_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(174, 239);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(17, 12);
            this.label23.TabIndex = 17;
            this.label23.Text = "ms";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(174, 277);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(17, 12);
            this.label22.TabIndex = 16;
            this.label22.Text = "ms";
            // 
            // TextOffpos
            // 
            this.TextOffpos.Location = new System.Drawing.Point(101, 200);
            this.TextOffpos.Name = "TextOffpos";
            this.TextOffpos.Size = new System.Drawing.Size(70, 21);
            this.TextOffpos.TabIndex = 15;
            this.TextOffpos.Text = "100";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(7, 203);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(89, 12);
            this.label21.TabIndex = 14;
            this.label21.Text = "同步偏移距离：";
            // 
            // TextSyncTime
            // 
            this.TextSyncTime.Location = new System.Drawing.Point(101, 274);
            this.TextSyncTime.Name = "TextSyncTime";
            this.TextSyncTime.Size = new System.Drawing.Size(70, 21);
            this.TextSyncTime.TabIndex = 13;
            this.TextSyncTime.Text = "1000";
            // 
            // TextAccTime
            // 
            this.TextAccTime.Location = new System.Drawing.Point(101, 237);
            this.TextAccTime.Name = "TextAccTime";
            this.TextAccTime.Size = new System.Drawing.Size(70, 21);
            this.TextAccTime.TabIndex = 12;
            this.TextAccTime.Text = "500";
            // 
            // TextYpos
            // 
            this.TextYpos.Location = new System.Drawing.Point(101, 163);
            this.TextYpos.Name = "TextYpos";
            this.TextYpos.Size = new System.Drawing.Size(70, 21);
            this.TextYpos.TabIndex = 11;
            this.TextYpos.Text = "0";
            // 
            // TextXpos
            // 
            this.TextXpos.Location = new System.Drawing.Point(101, 126);
            this.TextXpos.Name = "TextXpos";
            this.TextXpos.Size = new System.Drawing.Size(70, 21);
            this.TextXpos.TabIndex = 10;
            this.TextXpos.Text = "0";
            // 
            // TextAngle
            // 
            this.TextAngle.Location = new System.Drawing.Point(101, 89);
            this.TextAngle.Name = "TextAngle";
            this.TextAngle.Size = new System.Drawing.Size(70, 21);
            this.TextAngle.TabIndex = 9;
            this.TextAngle.Text = "0";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(7, 92);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(65, 12);
            this.label20.TabIndex = 8;
            this.label20.Text = "皮带夹角：";
            // 
            // radioBtnY
            // 
            this.radioBtnY.AutoSize = true;
            this.radioBtnY.Location = new System.Drawing.Point(92, 67);
            this.radioBtnY.Name = "radioBtnY";
            this.radioBtnY.Size = new System.Drawing.Size(77, 16);
            this.radioBtnY.TabIndex = 7;
            this.radioBtnY.Text = "Y方向跟随";
            this.radioBtnY.UseVisualStyleBackColor = true;
            // 
            // radioBtnX
            // 
            this.radioBtnX.AutoSize = true;
            this.radioBtnX.Checked = true;
            this.radioBtnX.Location = new System.Drawing.Point(9, 67);
            this.radioBtnX.Name = "radioBtnX";
            this.radioBtnX.Size = new System.Drawing.Size(77, 16);
            this.radioBtnX.TabIndex = 6;
            this.radioBtnX.TabStop = true;
            this.radioBtnX.Text = "X方向跟随";
            this.radioBtnX.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 166);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(71, 12);
            this.label19.TabIndex = 5;
            this.label19.Text = "Y等待位置：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 129);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(71, 12);
            this.label18.TabIndex = 4;
            this.label18.Text = "X等待位置：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(7, 277);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(89, 12);
            this.label17.TabIndex = 3;
            this.label17.Text = "同步匀速时间：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(7, 240);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 12);
            this.label14.TabIndex = 2;
            this.label14.Text = "同步加速时间：";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(203, 30);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(138, 16);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "同步跟随 + 轨迹运动";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(10, 30);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "同步跟随";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.Text_TabNum);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.Text_TabStart);
            this.groupBox5.Controls.Add(this.label15);
            this.groupBox5.Controls.Add(this.DataGridView2);
            this.groupBox5.Controls.Add(this.Btn_StrartRegist);
            this.groupBox5.Controls.Add(this.Cbox_RigeistMode);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Location = new System.Drawing.Point(697, 2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(280, 594);
            this.groupBox5.TabIndex = 27;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "编码器锁存";
            // 
            // Text_TabNum
            // 
            this.Text_TabNum.Location = new System.Drawing.Point(204, 54);
            this.Text_TabNum.Name = "Text_TabNum";
            this.Text_TabNum.Size = new System.Drawing.Size(60, 21);
            this.Text_TabNum.TabIndex = 46;
            this.Text_TabNum.Text = "100";
            this.Text_TabNum.TextChanged += new System.EventHandler(this.Text_TabNum_TextChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(140, 57);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 45;
            this.label16.Text = "最大长度：";
            // 
            // Text_TabStart
            // 
            this.Text_TabStart.Location = new System.Drawing.Point(90, 53);
            this.Text_TabStart.Name = "Text_TabStart";
            this.Text_TabStart.Size = new System.Drawing.Size(39, 21);
            this.Text_TabStart.TabIndex = 22;
            this.Text_TabStart.Text = "10";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(11, 56);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(83, 12);
            this.label15.TabIndex = 44;
            this.label15.Text = "Tab数据地址：";
            // 
            // DataGridView2
            // 
            this.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.锁存编号,
            this.锁存位置});
            this.DataGridView2.Location = new System.Drawing.Point(6, 80);
            this.DataGridView2.Name = "DataGridView2";
            this.DataGridView2.RowTemplate.Height = 23;
            this.DataGridView2.Size = new System.Drawing.Size(268, 513);
            this.DataGridView2.TabIndex = 41;
            // 
            // 锁存编号
            // 
            this.锁存编号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.锁存编号.FillWeight = 30F;
            this.锁存编号.HeaderText = "锁存编号";
            this.锁存编号.Name = "锁存编号";
            this.锁存编号.ReadOnly = true;
            // 
            // 锁存位置
            // 
            this.锁存位置.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.锁存位置.FillWeight = 70F;
            this.锁存位置.HeaderText = "锁存位置";
            this.锁存位置.Name = "锁存位置";
            this.锁存位置.ReadOnly = true;
            // 
            // Btn_StrartRegist
            // 
            this.Btn_StrartRegist.Location = new System.Drawing.Point(204, 22);
            this.Btn_StrartRegist.Name = "Btn_StrartRegist";
            this.Btn_StrartRegist.Size = new System.Drawing.Size(75, 23);
            this.Btn_StrartRegist.TabIndex = 23;
            this.Btn_StrartRegist.Text = "启动锁存";
            this.Btn_StrartRegist.UseVisualStyleBackColor = true;
            this.Btn_StrartRegist.Click += new System.EventHandler(this.Btn_StrartRegist_Click);
            // 
            // Cbox_RigeistMode
            // 
            this.Cbox_RigeistMode.FormattingEnabled = true;
            this.Cbox_RigeistMode.Location = new System.Drawing.Point(67, 24);
            this.Cbox_RigeistMode.Name = "Cbox_RigeistMode";
            this.Cbox_RigeistMode.Size = new System.Drawing.Size(132, 20);
            this.Cbox_RigeistMode.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(11, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 1;
            this.label13.Text = "锁存模式：";
            // 
            // timer2
            // 
            this.timer2.Interval = 10;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1017, 601);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "同步跟随测试";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Button btn_Connet;
        private System.Windows.Forms.ComboBox IPADDRESS;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Btn_Rev2;
        private System.Windows.Forms.Button Btn_Fwd2;
        private System.Windows.Forms.Button Btn_Rev1;
        private System.Windows.Forms.Button Btn_Fwd1;
        private System.Windows.Forms.Button Btn_Rev0;
        private System.Windows.Forms.Button Btn_Fwd0;
        private System.Windows.Forms.TextBox text_Accel2;
        private System.Windows.Forms.TextBox text_Accel1;
        private System.Windows.Forms.TextBox text_Accel0;
        private System.Windows.Forms.TextBox text_Speed2;
        private System.Windows.Forms.TextBox text_Speed1;
        private System.Windows.Forms.TextBox text_Speed0;
        private System.Windows.Forms.TextBox text_Units2;
        private System.Windows.Forms.TextBox text_Units1;
        private System.Windows.Forms.TextBox text_Units0;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox text_Dpos2;
        private System.Windows.Forms.TextBox text_Dpos1;
        private System.Windows.Forms.TextBox text_Dpos0;
        private System.Windows.Forms.Button Btn_Zero;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox text_Mpos2;
        private System.Windows.Forms.TextBox text_Mpos1;
        private System.Windows.Forms.TextBox text_Mpos0;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        internal System.Windows.Forms.GroupBox groupBox5;
        internal System.Windows.Forms.DataGridView DataGridView2;
        internal System.Windows.Forms.DataGridViewTextBoxColumn 锁存编号;
        internal System.Windows.Forms.DataGridViewTextBoxColumn 锁存位置;
        internal System.Windows.Forms.Button Btn_StrartRegist;
        private System.Windows.Forms.ComboBox Cbox_RigeistMode;
        internal System.Windows.Forms.Label label13;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.TextBox Text_TabNum;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox Text_TabStart;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox TextSyncTime;
        private System.Windows.Forms.TextBox TextAccTime;
        private System.Windows.Forms.TextBox TextYpos;
        private System.Windows.Forms.TextBox TextXpos;
        private System.Windows.Forms.TextBox TextAngle;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.RadioButton radioBtnY;
        private System.Windows.Forms.RadioButton radioBtnX;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox TextOffpos;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button BtnVmoveR;
        private System.Windows.Forms.Button Btn_Start;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox TextBackTime;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Button Btn_TestMove;
        private System.Windows.Forms.TextBox TextMoveAcc;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox TextMoveSp;
        private System.Windows.Forms.Label label26;
    }
}

