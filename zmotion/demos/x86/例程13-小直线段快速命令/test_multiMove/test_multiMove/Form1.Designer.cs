namespace test_multiMove
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
            this.Button3 = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.GroupBox3 = new System.Windows.Forms.GroupBox();
            this.TextBox6 = new System.Windows.Forms.TextBox();
            this.TextBox5 = new System.Windows.Forms.TextBox();
            this.TextBox8 = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.TextBox4 = new System.Windows.Forms.TextBox();
            this.TextBox3 = new System.Windows.Forms.TextBox();
            this.TextBox2 = new System.Windows.Forms.TextBox();
            this.TextBox1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.C_connect_type = new System.Windows.Forms.ComboBox();
            this.C_Ip_Address = new System.Windows.Forms.ComboBox();
            this.C_Open_Eth = new System.Windows.Forms.Button();
            this.C_Close_Card = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.GroupBox3.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button3
            // 
            this.Button3.Location = new System.Drawing.Point(234, 332);
            this.Button3.Name = "Button3";
            this.Button3.Size = new System.Drawing.Size(75, 23);
            this.Button3.TabIndex = 17;
            this.Button3.Text = "坐标清零";
            this.Button3.UseVisualStyleBackColor = true;
            this.Button3.Click += new System.EventHandler(this.Button3_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(234, 286);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(75, 23);
            this.Button2.TabIndex = 16;
            this.Button2.Text = "停止";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(234, 242);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 23);
            this.Button1.TabIndex = 15;
            this.Button1.Text = "启动";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // GroupBox3
            // 
            this.GroupBox3.Controls.Add(this.TextBox6);
            this.GroupBox3.Controls.Add(this.TextBox5);
            this.GroupBox3.Controls.Add(this.TextBox8);
            this.GroupBox3.Controls.Add(this.Label6);
            this.GroupBox3.Controls.Add(this.Label5);
            this.GroupBox3.Controls.Add(this.Label3);
            this.GroupBox3.Location = new System.Drawing.Point(9, 223);
            this.GroupBox3.Name = "GroupBox3";
            this.GroupBox3.Size = new System.Drawing.Size(198, 132);
            this.GroupBox3.TabIndex = 14;
            this.GroupBox3.TabStop = false;
            this.GroupBox3.Text = "运动参数";
            // 
            // TextBox6
            // 
            this.TextBox6.Location = new System.Drawing.Point(69, 93);
            this.TextBox6.Name = "TextBox6";
            this.TextBox6.Size = new System.Drawing.Size(111, 21);
            this.TextBox6.TabIndex = 9;
            this.TextBox6.Text = "1000";
            // 
            // TextBox5
            // 
            this.TextBox5.Location = new System.Drawing.Point(69, 60);
            this.TextBox5.Name = "TextBox5";
            this.TextBox5.Size = new System.Drawing.Size(111, 21);
            this.TextBox5.TabIndex = 8;
            this.TextBox5.Text = "1000";
            // 
            // TextBox8
            // 
            this.TextBox8.Location = new System.Drawing.Point(69, 27);
            this.TextBox8.Name = "TextBox8";
            this.TextBox8.Size = new System.Drawing.Size(111, 21);
            this.TextBox8.TabIndex = 7;
            this.TextBox8.Text = "100";
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(16, 96);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(41, 12);
            this.Label6.TabIndex = 4;
            this.Label6.Text = "减速度";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(16, 63);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(53, 12);
            this.Label5.TabIndex = 2;
            this.Label5.Text = "加速度：";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(16, 30);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(41, 12);
            this.Label3.TabIndex = 0;
            this.Label3.Text = "速度：";
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.TextBox4);
            this.GroupBox2.Controls.Add(this.TextBox3);
            this.GroupBox2.Controls.Add(this.TextBox2);
            this.GroupBox2.Controls.Add(this.TextBox1);
            this.GroupBox2.Location = new System.Drawing.Point(9, 78);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(400, 139);
            this.GroupBox2.TabIndex = 13;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "轴状态";
            // 
            // TextBox4
            // 
            this.TextBox4.Location = new System.Drawing.Point(21, 100);
            this.TextBox4.Name = "TextBox4";
            this.TextBox4.ReadOnly = true;
            this.TextBox4.Size = new System.Drawing.Size(350, 21);
            this.TextBox4.TabIndex = 10;
            this.TextBox4.Text = "剩余直线缓冲：";
            // 
            // TextBox3
            // 
            this.TextBox3.Location = new System.Drawing.Point(21, 73);
            this.TextBox3.Name = "TextBox3";
            this.TextBox3.ReadOnly = true;
            this.TextBox3.Size = new System.Drawing.Size(350, 21);
            this.TextBox3.TabIndex = 9;
            this.TextBox3.Text = "运动状态：";
            // 
            // TextBox2
            // 
            this.TextBox2.Location = new System.Drawing.Point(21, 46);
            this.TextBox2.Name = "TextBox2";
            this.TextBox2.ReadOnly = true;
            this.TextBox2.Size = new System.Drawing.Size(350, 21);
            this.TextBox2.TabIndex = 8;
            this.TextBox2.Text = "轴状态：";
            // 
            // TextBox1
            // 
            this.TextBox1.Location = new System.Drawing.Point(21, 19);
            this.TextBox1.Name = "TextBox1";
            this.TextBox1.ReadOnly = true;
            this.TextBox1.Size = new System.Drawing.Size(350, 21);
            this.TextBox1.TabIndex = 7;
            this.TextBox1.Text = "轴坐标：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.C_connect_type);
            this.groupBox1.Controls.Add(this.C_Ip_Address);
            this.groupBox1.Controls.Add(this.C_Open_Eth);
            this.groupBox1.Controls.Add(this.C_Close_Card);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(400, 61);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接控制器";
            // 
            // C_connect_type
            // 
            this.C_connect_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.C_connect_type.FormattingEnabled = true;
            this.C_connect_type.Items.AddRange(new object[] {
            "网口",
            "LOCAL",
            "PCI",
            "串口"});
            this.C_connect_type.Location = new System.Drawing.Point(12, 26);
            this.C_connect_type.Margin = new System.Windows.Forms.Padding(2);
            this.C_connect_type.Name = "C_connect_type";
            this.C_connect_type.Size = new System.Drawing.Size(59, 20);
            this.C_connect_type.TabIndex = 29;
            this.C_connect_type.DropDown += new System.EventHandler(this.OnSelect);
            this.C_connect_type.TextChanged += new System.EventHandler(this.C_connect_type_TextChanged);
            // 
            // C_Ip_Address
            // 
            this.C_Ip_Address.FormattingEnabled = true;
            this.C_Ip_Address.Location = new System.Drawing.Point(81, 26);
            this.C_Ip_Address.Name = "C_Ip_Address";
            this.C_Ip_Address.Size = new System.Drawing.Size(105, 20);
            this.C_Ip_Address.TabIndex = 26;
            this.C_Ip_Address.DropDown += new System.EventHandler(this.OnScan);
            // 
            // C_Open_Eth
            // 
            this.C_Open_Eth.Location = new System.Drawing.Point(192, 25);
            this.C_Open_Eth.Name = "C_Open_Eth";
            this.C_Open_Eth.Size = new System.Drawing.Size(55, 21);
            this.C_Open_Eth.TabIndex = 27;
            this.C_Open_Eth.Text = "连接";
            this.C_Open_Eth.UseVisualStyleBackColor = true;
            this.C_Open_Eth.Click += new System.EventHandler(this.OnOpen);
            // 
            // C_Close_Card
            // 
            this.C_Close_Card.Location = new System.Drawing.Point(256, 25);
            this.C_Close_Card.Name = "C_Close_Card";
            this.C_Close_Card.Size = new System.Drawing.Size(55, 21);
            this.C_Close_Card.TabIndex = 28;
            this.C_Close_Card.Text = "断开";
            this.C_Close_Card.UseVisualStyleBackColor = true;
            this.C_Close_Card.Click += new System.EventHandler(this.OnClose);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 377);
            this.Controls.Add(this.Button3);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.GroupBox3);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "test_multiMove";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.GroupBox3.ResumeLayout(false);
            this.GroupBox3.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button Button3;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.GroupBox GroupBox3;
        internal System.Windows.Forms.TextBox TextBox6;
        internal System.Windows.Forms.TextBox TextBox5;
        internal System.Windows.Forms.TextBox TextBox8;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.Label Label3;
        internal System.Windows.Forms.GroupBox GroupBox2;
        internal System.Windows.Forms.TextBox TextBox4;
        internal System.Windows.Forms.TextBox TextBox3;
        internal System.Windows.Forms.TextBox TextBox2;
        internal System.Windows.Forms.TextBox TextBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ComboBox C_connect_type;
        private System.Windows.Forms.ComboBox C_Ip_Address;
        private System.Windows.Forms.Button C_Open_Eth;
        private System.Windows.Forms.Button C_Close_Card;
        private System.Windows.Forms.Timer timer3;
    }
}

