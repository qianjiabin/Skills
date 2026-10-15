namespace test_cam
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
            this.C_connect_type = new System.Windows.Forms.ComboBox();
            this.C_Close_Card = new System.Windows.Forms.Button();
            this.C_Open_Eth = new System.Windows.Forms.Button();
            this.C_Ip_Address = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.DataGridView1 = new System.Windows.Forms.DataGridView();
            this.编号 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.位置 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.TextBox4 = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.TextBox3 = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.Label3 = new System.Windows.Forms.Label();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Button4 = new System.Windows.Forms.Button();
            this.Button3 = new System.Windows.Forms.Button();
            this.TextBox38 = new System.Windows.Forms.TextBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.TextBox37 = new System.Windows.Forms.TextBox();
            this.Label22 = new System.Windows.Forms.Label();
            this.RadioButton2 = new System.Windows.Forms.RadioButton();
            this.RadioButton1 = new System.Windows.Forms.RadioButton();
            this.ms11 = new System.Windows.Forms.TextBox();
            this.ms7 = new System.Windows.Forms.TextBox();
            this.ms3 = new System.Windows.Forms.TextBox();
            this.ms10 = new System.Windows.Forms.TextBox();
            this.ms6 = new System.Windows.Forms.TextBox();
            this.ms2 = new System.Windows.Forms.TextBox();
            this.ms9 = new System.Windows.Forms.TextBox();
            this.ms5 = new System.Windows.Forms.TextBox();
            this.ms1 = new System.Windows.Forms.TextBox();
            this.ms8 = new System.Windows.Forms.TextBox();
            this.ms4 = new System.Windows.Forms.TextBox();
            this.ms0 = new System.Windows.Forms.TextBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.Label15 = new System.Windows.Forms.Label();
            this.Label16 = new System.Windows.Forms.Label();
            this.Label17 = new System.Windows.Forms.Label();
            this.Label19 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.m11 = new System.Windows.Forms.TextBox();
            this.m7 = new System.Windows.Forms.TextBox();
            this.m3 = new System.Windows.Forms.TextBox();
            this.m10 = new System.Windows.Forms.TextBox();
            this.m6 = new System.Windows.Forms.TextBox();
            this.m2 = new System.Windows.Forms.TextBox();
            this.m9 = new System.Windows.Forms.TextBox();
            this.m5 = new System.Windows.Forms.TextBox();
            this.m1 = new System.Windows.Forms.TextBox();
            this.m8 = new System.Windows.Forms.TextBox();
            this.m4 = new System.Windows.Forms.TextBox();
            this.m0 = new System.Windows.Forms.TextBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.Label13 = new System.Windows.Forms.Label();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label11 = new System.Windows.Forms.Label();
            this.Label9 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.Label6 = new System.Windows.Forms.Label();
            this.GroupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // C_connect_type
            // 
            this.C_connect_type.FormattingEnabled = true;
            this.C_connect_type.Items.AddRange(new object[] {
            "网口",
            "LOCAL",
            "PCI",
            "串口"});
            this.C_connect_type.Location = new System.Drawing.Point(5, 19);
            this.C_connect_type.Margin = new System.Windows.Forms.Padding(2);
            this.C_connect_type.Name = "C_connect_type";
            this.C_connect_type.Size = new System.Drawing.Size(59, 20);
            this.C_connect_type.TabIndex = 21;
            this.C_connect_type.DropDown += new System.EventHandler(this.OnSelect);
            this.C_connect_type.TextChanged += new System.EventHandler(this.C_connect_type_TextChanged);
            this.C_connect_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList; // Replaced
            // 
            // C_Close_Card
            // 
            this.C_Close_Card.Location = new System.Drawing.Point(249, 18);
            this.C_Close_Card.Name = "C_Close_Card";
            this.C_Close_Card.Size = new System.Drawing.Size(55, 21);
            this.C_Close_Card.TabIndex = 20;
            this.C_Close_Card.Text = "断开";
            this.C_Close_Card.UseVisualStyleBackColor = true;
            this.C_Close_Card.Click += new System.EventHandler(this.OnClose);
            // 
            // C_Open_Eth
            // 
            this.C_Open_Eth.Location = new System.Drawing.Point(185, 18);
            this.C_Open_Eth.Name = "C_Open_Eth";
            this.C_Open_Eth.Size = new System.Drawing.Size(55, 21);
            this.C_Open_Eth.TabIndex = 19;
            this.C_Open_Eth.Text = "连接";
            this.C_Open_Eth.UseVisualStyleBackColor = true;
            this.C_Open_Eth.Click += new System.EventHandler(this.OnOpen);
            // 
            // C_Ip_Address
            // 
            this.C_Ip_Address.FormattingEnabled = true;
            this.C_Ip_Address.Location = new System.Drawing.Point(74, 19);
            this.C_Ip_Address.Name = "C_Ip_Address";
            this.C_Ip_Address.Size = new System.Drawing.Size(105, 20);
            this.C_Ip_Address.TabIndex = 18;
            this.C_Ip_Address.DropDown += new System.EventHandler(this.OnScan);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.DataGridView1);
            this.GroupBox2.Controls.Add(this.Button2);
            this.GroupBox2.Controls.Add(this.Button1);
            this.GroupBox2.Controls.Add(this.TextBox4);
            this.GroupBox2.Controls.Add(this.Label5);
            this.GroupBox2.Controls.Add(this.TextBox3);
            this.GroupBox2.Controls.Add(this.Label4);
            this.GroupBox2.Location = new System.Drawing.Point(12, 72);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(318, 257);
            this.GroupBox2.TabIndex = 30;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "凸轮运动";
            // 
            // DataGridView1
            // 
            this.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.编号,
            this.位置});
            this.DataGridView1.Location = new System.Drawing.Point(6, 20);
            this.DataGridView1.Name = "DataGridView1";
            this.DataGridView1.RowTemplate.Height = 23;
            this.DataGridView1.Size = new System.Drawing.Size(187, 216);
            this.DataGridView1.TabIndex = 6;
            // 
            // 编号
            // 
            this.编号.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.编号.FillWeight = 30F;
            this.编号.HeaderText = "编号";
            this.编号.Name = "编号";
            this.编号.ReadOnly = true;
            // 
            // 位置
            // 
            this.位置.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.位置.FillWeight = 70F;
            this.位置.HeaderText = "位置";
            this.位置.MaxInputLength = 5;
            this.位置.Name = "位置";
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(217, 197);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(75, 23);
            this.Button2.TabIndex = 5;
            this.Button2.Text = "停止";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(217, 156);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 23);
            this.Button1.TabIndex = 4;
            this.Button1.Text = "运动";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // TextBox4
            // 
            this.TextBox4.Location = new System.Drawing.Point(211, 103);
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.Size = new System.Drawing.Size(93, 21);
            this.TextBox4.TabIndex = 3;
            this.TextBox4.Text = "1000";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(209, 78);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(65, 12);
            this.Label5.TabIndex = 2;
            this.Label5.Text = "运动距离：";
            // 
            // TextBox3
            // 
            this.TextBox3.Location = new System.Drawing.Point(211, 45);
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.Size = new System.Drawing.Size(93, 21);
            this.TextBox3.TabIndex = 1;
            this.TextBox3.Text = "100";
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(209, 20);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(65, 12);
            this.Label4.TabIndex = 0;
            this.Label4.Text = "运动速度：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.C_connect_type);
            this.groupBox1.Controls.Add(this.C_Ip_Address);
            this.groupBox1.Controls.Add(this.C_Open_Eth);
            this.groupBox1.Controls.Add(this.C_Close_Card);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(318, 54);
            this.groupBox1.TabIndex = 31;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接控制器";
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(206, 19);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.Size = new System.Drawing.Size(59, 21);
            this.TextBox2.TabIndex = 35;
            this.TextBox2.Text = "1";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(146, 23);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(65, 12);
            this.Label3.TabIndex = 34;
            this.Label3.Text = "从轴轴号：";
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(71, 20);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.Size = new System.Drawing.Size(52, 21);
            this.TextBox1.TabIndex = 33;
            this.TextBox1.Text = "0";
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(11, 24);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(65, 12);
            this.Label1.TabIndex = 32;
            this.Label1.Text = "主轴轴号：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.TextBox1);
            this.groupBox3.Controls.Add(this.TextBox2);
            this.groupBox3.Controls.Add(this.Label1);
            this.groupBox3.Controls.Add(this.Label3);
            this.groupBox3.Location = new System.Drawing.Point(336, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(286, 54);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "轴选择";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Button4);
            this.groupBox4.Controls.Add(this.Button3);
            this.groupBox4.Controls.Add(this.TextBox38);
            this.groupBox4.Controls.Add(this.Label23);
            this.groupBox4.Controls.Add(this.TextBox37);
            this.groupBox4.Controls.Add(this.Label22);
            this.groupBox4.Controls.Add(this.RadioButton2);
            this.groupBox4.Controls.Add(this.RadioButton1);
            this.groupBox4.Controls.Add(this.ms11);
            this.groupBox4.Controls.Add(this.ms7);
            this.groupBox4.Controls.Add(this.ms3);
            this.groupBox4.Controls.Add(this.ms10);
            this.groupBox4.Controls.Add(this.ms6);
            this.groupBox4.Controls.Add(this.ms2);
            this.groupBox4.Controls.Add(this.ms9);
            this.groupBox4.Controls.Add(this.ms5);
            this.groupBox4.Controls.Add(this.ms1);
            this.groupBox4.Controls.Add(this.ms8);
            this.groupBox4.Controls.Add(this.ms4);
            this.groupBox4.Controls.Add(this.ms0);
            this.groupBox4.Controls.Add(this.Label14);
            this.groupBox4.Controls.Add(this.Label15);
            this.groupBox4.Controls.Add(this.Label16);
            this.groupBox4.Controls.Add(this.Label17);
            this.groupBox4.Controls.Add(this.Label19);
            this.groupBox4.Controls.Add(this.Label20);
            this.groupBox4.Controls.Add(this.Label21);
            this.groupBox4.Controls.Add(this.m11);
            this.groupBox4.Controls.Add(this.m7);
            this.groupBox4.Controls.Add(this.m3);
            this.groupBox4.Controls.Add(this.m10);
            this.groupBox4.Controls.Add(this.m6);
            this.groupBox4.Controls.Add(this.m2);
            this.groupBox4.Controls.Add(this.m9);
            this.groupBox4.Controls.Add(this.m5);
            this.groupBox4.Controls.Add(this.m1);
            this.groupBox4.Controls.Add(this.m8);
            this.groupBox4.Controls.Add(this.m4);
            this.groupBox4.Controls.Add(this.m0);
            this.groupBox4.Controls.Add(this.Label12);
            this.groupBox4.Controls.Add(this.Label13);
            this.groupBox4.Controls.Add(this.Label10);
            this.groupBox4.Controls.Add(this.Label11);
            this.groupBox4.Controls.Add(this.Label9);
            this.groupBox4.Controls.Add(this.Label7);
            this.groupBox4.Controls.Add(this.Label6);
            this.groupBox4.Location = new System.Drawing.Point(336, 72);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(563, 422);
            this.groupBox4.TabIndex = 37;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "同步跟随";
            // 
            // Button4
            // 
            this.Button4.Location = new System.Drawing.Point(349, 373);
            this.Button4.Name = "Button4";
            this.Button4.Size = new System.Drawing.Size(75, 23);
            this.Button4.TabIndex = 6;
            this.Button4.Text = "停止";
            this.Button4.UseVisualStyleBackColor = true;
            this.Button4.Click += new System.EventHandler(this.Button4_Click);
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(195, 373);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(75, 23);
            this.Button3.TabIndex = 6;
            this.Button3.Text = "运动";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // TextBox38
            // 
            this.TextBox38.Location = new System.Drawing.Point(430, 332);
            this.TextBox38.Name = "TextBox38";
            this.TextBox38.Size = new System.Drawing.Size(87, 21);
            this.TextBox38.TabIndex = 56;
            this.TextBox38.Text = "1000";
            // 
            // Label23
            // 
            this.Label23.AutoSize = true;
            this.Label23.Location = new System.Drawing.Point(335, 335);
            this.Label23.Name = "Label23";
            this.Label23.Size = new System.Drawing.Size(89, 12);
            this.Label23.TabIndex = 55;
            this.Label23.Text = "主轴运动距离：";
            // 
            // TextBox37
            // 
            this.TextBox37.Location = new System.Drawing.Point(228, 332);
            this.TextBox37.Name = "TextBox37";
            this.TextBox37.Size = new System.Drawing.Size(87, 21);
            this.TextBox37.TabIndex = 54;
            this.TextBox37.Text = "100";
            // 
            // Label22
            // 
            this.Label22.AutoSize = true;
            this.Label22.Location = new System.Drawing.Point(133, 335);
            this.Label22.Name = "Label22";
            this.Label22.Size = new System.Drawing.Size(89, 12);
            this.Label22.TabIndex = 53;
            this.Label22.Text = "主轴运动速度：";
            // 
            // RadioButton2
            // 
            this.RadioButton2.AutoSize = true;
            this.RadioButton2.Location = new System.Drawing.Point(21, 177);
            this.RadioButton2.Name = "RadioButton2";
            this.RadioButton2.Size = new System.Drawing.Size(95, 16);
            this.RadioButton2.TabIndex = 52;
            this.RadioButton2.Text = "速度规格模式";
            this.RadioButton2.UseVisualStyleBackColor = true;
            // 
            // RadioButton1
            // 
            this.RadioButton1.AutoSize = true;
            this.RadioButton1.Checked = true;
            this.RadioButton1.Location = new System.Drawing.Point(21, 29);
            this.RadioButton1.Name = "RadioButton1";
            this.RadioButton1.Size = new System.Drawing.Size(95, 16);
            this.RadioButton1.TabIndex = 51;
            this.RadioButton1.TabStop = true;
            this.RadioButton1.Text = "位置规划模式";
            this.RadioButton1.UseVisualStyleBackColor = true;
            // 
            // ms11
            // 
            this.ms11.Location = new System.Drawing.Point(453, 285);
            this.ms11.Name = "ms11";
            this.ms11.Size = new System.Drawing.Size(87, 21);
            this.ms11.TabIndex = 49;
            this.ms11.Text = "0";
            // 
            // ms7
            // 
            this.ms7.Location = new System.Drawing.Point(453, 249);
            this.ms7.Name = "ms7";
            this.ms7.Size = new System.Drawing.Size(87, 21);
            this.ms7.TabIndex = 48;
            this.ms7.Text = "1";
            // 
            // ms3
            // 
            this.ms3.Location = new System.Drawing.Point(453, 215);
            this.ms3.Name = "ms3";
            this.ms3.Size = new System.Drawing.Size(87, 21);
            this.ms3.TabIndex = 47;
            this.ms3.Text = "1";
            // 
            // ms10
            // 
            this.ms10.Location = new System.Drawing.Point(349, 285);
            this.ms10.Name = "ms10";
            this.ms10.Size = new System.Drawing.Size(87, 21);
            this.ms10.TabIndex = 45;
            this.ms10.Text = "1";
            // 
            // ms6
            // 
            this.ms6.Location = new System.Drawing.Point(349, 249);
            this.ms6.Name = "ms6";
            this.ms6.Size = new System.Drawing.Size(87, 21);
            this.ms6.TabIndex = 44;
            this.ms6.Text = "1";
            // 
            // ms2
            // 
            this.ms2.Location = new System.Drawing.Point(349, 215);
            this.ms2.Name = "ms2";
            this.ms2.Size = new System.Drawing.Size(87, 21);
            this.ms2.TabIndex = 43;
            this.ms2.Text = "0";
            // 
            // ms9
            // 
            this.ms9.Location = new System.Drawing.Point(244, 285);
            this.ms9.Name = "ms9";
            this.ms9.Size = new System.Drawing.Size(87, 21);
            this.ms9.TabIndex = 41;
            this.ms9.Text = "100";
            // 
            // ms5
            // 
            this.ms5.Location = new System.Drawing.Point(244, 249);
            this.ms5.Name = "ms5";
            this.ms5.Size = new System.Drawing.Size(87, 21);
            this.ms5.TabIndex = 40;
            this.ms5.Text = "200";
            // 
            // ms1
            // 
            this.ms1.Location = new System.Drawing.Point(244, 215);
            this.ms1.Name = "ms1";
            this.ms1.Size = new System.Drawing.Size(87, 21);
            this.ms1.TabIndex = 39;
            this.ms1.Text = "100";
            // 
            // ms8
            // 
            this.ms8.Location = new System.Drawing.Point(137, 285);
            this.ms8.Name = "ms8";
            this.ms8.Size = new System.Drawing.Size(87, 21);
            this.ms8.TabIndex = 37;
            this.ms8.Text = "50";
            // 
            // ms4
            // 
            this.ms4.Location = new System.Drawing.Point(137, 249);
            this.ms4.Name = "ms4";
            this.ms4.Size = new System.Drawing.Size(87, 21);
            this.ms4.TabIndex = 36;
            this.ms4.Text = "200";
            // 
            // ms0
            // 
            this.ms0.Location = new System.Drawing.Point(137, 215);
            this.ms0.Name = "ms0";
            this.ms0.Size = new System.Drawing.Size(87, 21);
            this.ms0.TabIndex = 27;
            this.ms0.Text = "50";
            // 
            // Label14
            // 
            this.Label14.AutoSize = true;
            this.Label14.Location = new System.Drawing.Point(451, 179);
            this.Label14.Name = "Label14";
            this.Label14.Size = new System.Drawing.Size(77, 12);
            this.Label14.TabIndex = 35;
            this.Label14.Text = "减速段距离：";
            // 
            // Label15
            // 
            this.Label15.AutoSize = true;
            this.Label15.Location = new System.Drawing.Point(347, 179);
            this.Label15.Name = "Label15";
            this.Label15.Size = new System.Drawing.Size(77, 12);
            this.Label15.TabIndex = 34;
            this.Label15.Text = "加速段距离：";
            // 
            // Label16
            // 
            this.Label16.AutoSize = true;
            this.Label16.Location = new System.Drawing.Point(242, 179);
            this.Label16.Name = "Label16";
            this.Label16.Size = new System.Drawing.Size(89, 12);
            this.Label16.TabIndex = 33;
            this.Label16.Text = "主轴同步距离：";
            // 
            // Label17
            // 
            this.Label17.AutoSize = true;
            this.Label17.Location = new System.Drawing.Point(135, 179);
            this.Label17.Name = "Label17";
            this.Label17.Size = new System.Drawing.Size(89, 12);
            this.Label17.TabIndex = 32;
            this.Label17.Text = "从轴跟随距离：";
            // 
            // Label19
            // 
            this.Label19.AutoSize = true;
            this.Label19.Location = new System.Drawing.Point(51, 285);
            this.Label19.Name = "Label19";
            this.Label19.Size = new System.Drawing.Size(53, 12);
            this.Label19.TabIndex = 30;
            this.Label19.Text = "减速段：";
            // 
            // Label20
            // 
            this.Label20.AutoSize = true;
            this.Label20.Location = new System.Drawing.Point(51, 252);
            this.Label20.Name = "Label20";
            this.Label20.Size = new System.Drawing.Size(77, 12);
            this.Label20.TabIndex = 29;
            this.Label20.Text = "匀速同步段：";
            // 
            // Label21
            // 
            this.Label21.AutoSize = true;
            this.Label21.Location = new System.Drawing.Point(51, 218);
            this.Label21.Name = "Label21";
            this.Label21.Size = new System.Drawing.Size(65, 12);
            this.Label21.TabIndex = 28;
            this.Label21.Text = "加速度段：";
            // 
            // m11
            // 
            this.m11.Location = new System.Drawing.Point(453, 137);
            this.m11.Name = "m11";
            this.m11.Size = new System.Drawing.Size(87, 21);
            this.m11.TabIndex = 25;
            this.m11.Text = "100";
            // 
            // m7
            // 
            this.m7.Location = new System.Drawing.Point(453, 101);
            this.m7.Name = "m7";
            this.m7.Size = new System.Drawing.Size(87, 21);
            this.m7.TabIndex = 24;
            this.m7.Text = "0";
            // 
            // m3
            // 
            this.m3.Location = new System.Drawing.Point(453, 67);
            this.m3.Name = "m3";
            this.m3.Size = new System.Drawing.Size(87, 21);
            this.m3.TabIndex = 23;
            this.m3.Text = "0";
            // 
            // m10
            // 
            this.m10.Location = new System.Drawing.Point(349, 137);
            this.m10.Name = "m10";
            this.m10.Size = new System.Drawing.Size(87, 21);
            this.m10.TabIndex = 21;
            this.m10.Text = "0";
            // 
            // m6
            // 
            this.m6.Location = new System.Drawing.Point(349, 101);
            this.m6.Name = "m6";
            this.m6.Size = new System.Drawing.Size(87, 21);
            this.m6.TabIndex = 20;
            this.m6.Text = "0";
            // 
            // m2
            // 
            this.m2.Location = new System.Drawing.Point(349, 67);
            this.m2.Name = "m2";
            this.m2.Size = new System.Drawing.Size(87, 21);
            this.m2.TabIndex = 19;
            this.m2.Text = "100";
            // 
            // m9
            // 
            this.m9.Location = new System.Drawing.Point(244, 137);
            this.m9.Name = "m9";
            this.m9.Size = new System.Drawing.Size(87, 21);
            this.m9.TabIndex = 17;
            this.m9.Text = "100";
            // 
            // m5
            // 
            this.m5.Location = new System.Drawing.Point(244, 101);
            this.m5.Name = "m5";
            this.m5.Size = new System.Drawing.Size(87, 21);
            this.m5.TabIndex = 16;
            this.m5.Text = "200";
            // 
            // m1
            // 
            this.m1.Location = new System.Drawing.Point(244, 67);
            this.m1.Name = "m1";
            this.m1.Size = new System.Drawing.Size(87, 21);
            this.m1.TabIndex = 15;
            this.m1.Text = "100";
            // 
            // m8
            // 
            this.m8.Location = new System.Drawing.Point(137, 137);
            this.m8.Name = "m8";
            this.m8.Size = new System.Drawing.Size(87, 21);
            this.m8.TabIndex = 13;
            this.m8.Text = "50";
            // 
            // m4
            // 
            this.m4.Location = new System.Drawing.Point(137, 101);
            this.m4.Name = "m4";
            this.m4.Size = new System.Drawing.Size(87, 21);
            this.m4.TabIndex = 12;
            this.m4.Text = "200";
            // 
            // m0
            // 
            this.m0.Location = new System.Drawing.Point(137, 67);
            this.m0.Name = "m0";
            this.m0.Size = new System.Drawing.Size(87, 21);
            this.m0.TabIndex = 4;
            this.m0.Text = "50";
            // 
            // Label12
            // 
            this.Label12.AutoSize = true;
            this.Label12.Location = new System.Drawing.Point(451, 31);
            this.Label12.Name = "Label12";
            this.Label12.Size = new System.Drawing.Size(77, 12);
            this.Label12.TabIndex = 11;
            this.Label12.Text = "减速段距离：";
            // 
            // Label13
            // 
            this.Label13.AutoSize = true;
            this.Label13.Location = new System.Drawing.Point(347, 31);
            this.Label13.Name = "Label13";
            this.Label13.Size = new System.Drawing.Size(77, 12);
            this.Label13.TabIndex = 10;
            this.Label13.Text = "加速段距离：";
            // 
            // Label10
            // 
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(242, 31);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(89, 12);
            this.Label10.TabIndex = 9;
            this.Label10.Text = "主轴同步距离：";
            // 
            // Label11
            // 
            this.Label11.AutoSize = true;
            this.Label11.Location = new System.Drawing.Point(135, 31);
            this.Label11.Name = "Label11";
            this.Label11.Size = new System.Drawing.Size(89, 12);
            this.Label11.TabIndex = 8;
            this.Label11.Text = "从轴跟随距离：";
            // 
            // Label9
            // 
            this.Label9.AutoSize = true;
            this.Label9.Location = new System.Drawing.Point(51, 137);
            this.Label9.Name = "Label9";
            this.Label9.Size = new System.Drawing.Size(53, 12);
            this.Label9.TabIndex = 6;
            this.Label9.Text = "减速段：";
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(51, 104);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(77, 12);
            this.Label7.TabIndex = 5;
            this.Label7.Text = "匀速同步段：";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(51, 70);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(65, 12);
            this.Label6.TabIndex = 4;
            this.Label6.Text = "加速度段：";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 513);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.GroupBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox C_connect_type;
        private System.Windows.Forms.Button C_Close_Card;
        private System.Windows.Forms.Button C_Open_Eth;
        private System.Windows.Forms.ComboBox C_Ip_Address;
        private System.Windows.Forms.Timer timer1;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.DataGridView DataGridView1;
        internal System.Windows.Forms.DataGridViewTextBoxColumn 编号;
        internal System.Windows.Forms.DataGridViewTextBoxColumn 位置;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.TextBox TextBox4;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox TextBox3;
        internal System.Windows.Forms.Label Label4;
        private System.Windows.Forms.GroupBox groupBox1;
        internal System.Windows.Forms.TextBox TextBox2;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.TextBox TextBox1;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.GroupBox groupBox3;
        internal System.Windows.Forms.GroupBox groupBox4;
        internal System.Windows.Forms.Button Button4;
        internal System.Windows.Forms.Button Button3;
        internal System.Windows.Forms.TextBox TextBox38;
        internal System.Windows.Forms.Label Label23;
        internal System.Windows.Forms.TextBox TextBox37;
        internal System.Windows.Forms.Label Label22;
        internal System.Windows.Forms.RadioButton RadioButton2;
        internal System.Windows.Forms.RadioButton RadioButton1;
        internal System.Windows.Forms.TextBox ms11;
        internal System.Windows.Forms.TextBox ms7;
        internal System.Windows.Forms.TextBox ms3;
        internal System.Windows.Forms.TextBox ms10;
        internal System.Windows.Forms.TextBox ms6;
        internal System.Windows.Forms.TextBox ms2;
        internal System.Windows.Forms.TextBox ms9;
        internal System.Windows.Forms.TextBox ms5;
        internal System.Windows.Forms.TextBox ms1;
        internal System.Windows.Forms.TextBox ms8;
        internal System.Windows.Forms.TextBox ms4;
        internal System.Windows.Forms.TextBox ms0;
        internal System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.Label Label16;
        internal System.Windows.Forms.Label Label17;
        internal System.Windows.Forms.Label Label19;
        internal System.Windows.Forms.Label Label20;
        internal System.Windows.Forms.Label Label21;
        internal System.Windows.Forms.TextBox m11;
        internal System.Windows.Forms.TextBox m7;
        internal System.Windows.Forms.TextBox m3;
        internal System.Windows.Forms.TextBox m10;
        internal System.Windows.Forms.TextBox m6;
        internal System.Windows.Forms.TextBox m2;
        internal System.Windows.Forms.TextBox m9;
        internal System.Windows.Forms.TextBox m5;
        internal System.Windows.Forms.TextBox m1;
        internal System.Windows.Forms.TextBox m8;
        internal System.Windows.Forms.TextBox m4;
        internal System.Windows.Forms.TextBox m0;
        internal System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label11;
        internal System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label Label6;
    }
}

