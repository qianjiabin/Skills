namespace test_faction4
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_runstate = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioMode4 = new System.Windows.Forms.RadioButton();
            this.radioMode3 = new System.Windows.Forms.RadioButton();
            this.radioMode2 = new System.Windows.Forms.RadioButton();
            this.radioMode1 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox_dec = new System.Windows.Forms.TextBox();
            this.textBox_acc = new System.Windows.Forms.TextBox();
            this.textBox_speed = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.midpos4 = new System.Windows.Forms.TextBox();
            this.midpos3 = new System.Windows.Forms.TextBox();
            this.midpos2 = new System.Windows.Forms.TextBox();
            this.midpos1 = new System.Windows.Forms.TextBox();
            this.destpos4 = new System.Windows.Forms.TextBox();
            this.destpos3 = new System.Windows.Forms.TextBox();
            this.destpos2 = new System.Windows.Forms.TextBox();
            this.destpos1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.Button_start = new System.Windows.Forms.Button();
            this.Button_stop = new System.Windows.Forms.Button();
            this.Button_zero = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.C_Close_Card = new System.Windows.Forms.Button();
            this.C_Open_Eth = new System.Windows.Forms.Button();
            this.C_Ip_Address = new System.Windows.Forms.ComboBox();
            this.C_connect_type = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.groupBox1.Controls.Add(this.label_runstate);
            this.groupBox1.Location = new System.Drawing.Point(8, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(393, 54);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "轴坐标状态";
            // 
            // label_runstate
            // 
            this.label_runstate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_runstate.Location = new System.Drawing.Point(20, 21);
            this.label_runstate.Name = "label_runstate";
            this.label_runstate.Size = new System.Drawing.Size(356, 23);
            this.label_runstate.TabIndex = 0;
            this.label_runstate.Text = "x:0  y:0  z:0  u:0      运动状态：停止";
            this.label_runstate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioMode4);
            this.groupBox2.Controls.Add(this.radioMode3);
            this.groupBox2.Controls.Add(this.radioMode2);
            this.groupBox2.Controls.Add(this.radioMode1);
            this.groupBox2.Location = new System.Drawing.Point(13, 185);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(215, 189);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "运动类型";
            // 
            // radioMode4
            // 
            this.radioMode4.AutoSize = true;
            this.radioMode4.Location = new System.Drawing.Point(15, 155);
            this.radioMode4.Name = "radioMode4";
            this.radioMode4.Size = new System.Drawing.Size(161, 16);
            this.radioMode4.TabIndex = 3;
            this.radioMode4.Text = "x,y,z 空间圆弧 + u 螺旋";
            this.radioMode4.UseVisualStyleBackColor = true;
            this.radioMode4.CheckedChanged += new System.EventHandler(this.radioMode4_CheckedChanged);
            // 
            // radioMode3
            // 
            this.radioMode3.AutoSize = true;
            this.radioMode3.Location = new System.Drawing.Point(15, 116);
            this.radioMode3.Name = "radioMode3";
            this.radioMode3.Size = new System.Drawing.Size(119, 16);
            this.radioMode3.TabIndex = 2;
            this.radioMode3.Text = "x,y,z,u 直线插补";
            this.radioMode3.UseVisualStyleBackColor = true;
            this.radioMode3.CheckedChanged += new System.EventHandler(this.radioMode3_CheckedChanged);
            // 
            // radioMode2
            // 
            this.radioMode2.AutoSize = true;
            this.radioMode2.Location = new System.Drawing.Point(15, 77);
            this.radioMode2.Name = "radioMode2";
            this.radioMode2.Size = new System.Drawing.Size(101, 16);
            this.radioMode2.TabIndex = 1;
            this.radioMode2.Text = "x,y 3点画圆弧";
            this.radioMode2.UseVisualStyleBackColor = true;
            this.radioMode2.CheckedChanged += new System.EventHandler(this.radioMode2_CheckedChanged);
            // 
            // radioMode1
            // 
            this.radioMode1.AutoSize = true;
            this.radioMode1.Checked = true;
            this.radioMode1.Location = new System.Drawing.Point(15, 38);
            this.radioMode1.Name = "radioMode1";
            this.radioMode1.Size = new System.Drawing.Size(95, 16);
            this.radioMode1.TabIndex = 0;
            this.radioMode1.TabStop = true;
            this.radioMode1.Text = "x,y 直线插补";
            this.radioMode1.UseVisualStyleBackColor = true;
            this.radioMode1.CheckedChanged += new System.EventHandler(this.radioMode1_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Location = new System.Drawing.Point(13, 120);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(215, 63);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "运动模式";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(119, 27);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(59, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.Text = "相  对";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(36, 27);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(59, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "绝  对";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox_dec);
            this.groupBox4.Controls.Add(this.textBox_acc);
            this.groupBox4.Controls.Add(this.textBox_speed);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(234, 120);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(167, 143);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "插补运动参数";
            // 
            // textBox_dec
            // 
            this.textBox_dec.Location = new System.Drawing.Point(71, 107);
            this.textBox_dec.Name = "textBox_dec";
            this.textBox_dec.Size = new System.Drawing.Size(76, 21);
            this.textBox_dec.TabIndex = 5;
            this.textBox_dec.Text = "1000";
            // 
            // textBox_acc
            // 
            this.textBox_acc.Location = new System.Drawing.Point(71, 69);
            this.textBox_acc.Name = "textBox_acc";
            this.textBox_acc.Size = new System.Drawing.Size(76, 21);
            this.textBox_acc.TabIndex = 4;
            this.textBox_acc.Text = "1000";
            // 
            // textBox_speed
            // 
            this.textBox_speed.Location = new System.Drawing.Point(71, 31);
            this.textBox_speed.Name = "textBox_speed";
            this.textBox_speed.Size = new System.Drawing.Size(76, 21);
            this.textBox_speed.TabIndex = 3;
            this.textBox_speed.Text = "100";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "减速度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "加速度：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "速度：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.midpos4);
            this.groupBox5.Controls.Add(this.midpos3);
            this.groupBox5.Controls.Add(this.midpos2);
            this.groupBox5.Controls.Add(this.midpos1);
            this.groupBox5.Controls.Add(this.destpos4);
            this.groupBox5.Controls.Add(this.destpos3);
            this.groupBox5.Controls.Add(this.destpos2);
            this.groupBox5.Controls.Add(this.destpos1);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(13, 380);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(388, 150);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "运动距离";
            // 
            // midpos4
            // 
            this.midpos4.Location = new System.Drawing.Point(303, 88);
            this.midpos4.Name = "midpos4";
            this.midpos4.Size = new System.Drawing.Size(48, 21);
            this.midpos4.TabIndex = 18;
            this.midpos4.Text = "400";
            // 
            // midpos3
            // 
            this.midpos3.Location = new System.Drawing.Point(232, 88);
            this.midpos3.Name = "midpos3";
            this.midpos3.Size = new System.Drawing.Size(48, 21);
            this.midpos3.TabIndex = 17;
            this.midpos3.Text = "300";
            // 
            // midpos2
            // 
            this.midpos2.Location = new System.Drawing.Point(161, 88);
            this.midpos2.Name = "midpos2";
            this.midpos2.Size = new System.Drawing.Size(48, 21);
            this.midpos2.TabIndex = 16;
            this.midpos2.Text = "200";
            // 
            // midpos1
            // 
            this.midpos1.Location = new System.Drawing.Point(90, 88);
            this.midpos1.Name = "midpos1";
            this.midpos1.Size = new System.Drawing.Size(48, 21);
            this.midpos1.TabIndex = 15;
            this.midpos1.Text = "100";
            // 
            // destpos4
            // 
            this.destpos4.Location = new System.Drawing.Point(305, 48);
            this.destpos4.Name = "destpos4";
            this.destpos4.Size = new System.Drawing.Size(48, 21);
            this.destpos4.TabIndex = 14;
            this.destpos4.Text = "100";
            // 
            // destpos3
            // 
            this.destpos3.Location = new System.Drawing.Point(234, 48);
            this.destpos3.Name = "destpos3";
            this.destpos3.Size = new System.Drawing.Size(48, 21);
            this.destpos3.TabIndex = 13;
            this.destpos3.Text = "100";
            // 
            // destpos2
            // 
            this.destpos2.Location = new System.Drawing.Point(163, 48);
            this.destpos2.Name = "destpos2";
            this.destpos2.Size = new System.Drawing.Size(48, 21);
            this.destpos2.TabIndex = 12;
            this.destpos2.Text = "100";
            // 
            // destpos1
            // 
            this.destpos1.Location = new System.Drawing.Point(92, 48);
            this.destpos1.Name = "destpos1";
            this.destpos1.Size = new System.Drawing.Size(48, 21);
            this.destpos1.TabIndex = 6;
            this.destpos1.Text = "100";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 95);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 11;
            this.label9.Text = "圆弧中间点";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "运动终点";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(324, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "U";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(252, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "Z";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(180, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(11, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(108, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "X";
            // 
            // Button_start
            // 
            this.Button_start.Location = new System.Drawing.Point(263, 268);
            this.Button_start.Name = "Button_start";
            this.Button_start.Size = new System.Drawing.Size(98, 25);
            this.Button_start.TabIndex = 5;
            this.Button_start.Text = "启  动";
            this.Button_start.UseVisualStyleBackColor = true;
            this.Button_start.Click += new System.EventHandler(this.Button_start_Click);
            // 
            // Button_stop
            // 
            this.Button_stop.Location = new System.Drawing.Point(263, 311);
            this.Button_stop.Name = "Button_stop";
            this.Button_stop.Size = new System.Drawing.Size(98, 25);
            this.Button_stop.TabIndex = 6;
            this.Button_stop.Text = "停  止";
            this.Button_stop.UseVisualStyleBackColor = true;
            this.Button_stop.Click += new System.EventHandler(this.Button_stop_Click);
            // 
            // Button_zero
            // 
            this.Button_zero.Location = new System.Drawing.Point(263, 354);
            this.Button_zero.Name = "Button_zero";
            this.Button_zero.Size = new System.Drawing.Size(98, 25);
            this.Button_zero.TabIndex = 7;
            this.Button_zero.Text = "坐标清零";
            this.Button_zero.UseVisualStyleBackColor = true;
            this.Button_zero.Click += new System.EventHandler(this.Button_zero_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.C_Close_Card);
            this.groupBox6.Controls.Add(this.C_Open_Eth);
            this.groupBox6.Controls.Add(this.C_Ip_Address);
            this.groupBox6.Controls.Add(this.C_connect_type);
            this.groupBox6.Location = new System.Drawing.Point(13, 2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(388, 60);
            this.groupBox6.TabIndex = 10;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "连接控制器";
            // 
            // C_Close_Card
            // 
            this.C_Close_Card.Location = new System.Drawing.Point(313, 26);
            this.C_Close_Card.Name = "C_Close_Card";
            this.C_Close_Card.Size = new System.Drawing.Size(55, 21);
            this.C_Close_Card.TabIndex = 25;
            this.C_Close_Card.Text = "断开";
            this.C_Close_Card.UseVisualStyleBackColor = true;
            this.C_Close_Card.Click += new System.EventHandler(this.OnClose);
            // 
            // C_Open_Eth
            // 
            this.C_Open_Eth.Location = new System.Drawing.Point(234, 26);
            this.C_Open_Eth.Name = "C_Open_Eth";
            this.C_Open_Eth.Size = new System.Drawing.Size(55, 21);
            this.C_Open_Eth.TabIndex = 24;
            this.C_Open_Eth.Text = "连接";
            this.C_Open_Eth.UseVisualStyleBackColor = true;
            this.C_Open_Eth.Click += new System.EventHandler(this.OnOpen);
            // 
            // C_Ip_Address
            // 
            this.C_Ip_Address.FormattingEnabled = true;
            this.C_Ip_Address.Location = new System.Drawing.Point(98, 26);
            this.C_Ip_Address.Name = "C_Ip_Address";
            this.C_Ip_Address.Size = new System.Drawing.Size(105, 20);
            this.C_Ip_Address.TabIndex = 23;
            this.C_Ip_Address.DropDown += new System.EventHandler(this.OnScan);
            // 
            // C_connect_type
            // 
            this.C_connect_type.FormattingEnabled = true;
            this.C_connect_type.Items.AddRange(new object[] {
            "网口",
            "LOCAL",
            "PCI",
            "串口"});
            this.C_connect_type.Location = new System.Drawing.Point(9, 26);
            this.C_connect_type.Margin = new System.Windows.Forms.Padding(2);
            this.C_connect_type.Name = "C_connect_type";
            this.C_connect_type.Size = new System.Drawing.Size(59, 20);
            this.C_connect_type.TabIndex = 22;
            this.C_connect_type.DropDown += new System.EventHandler(this.OnSelect);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(413, 502);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.Button_zero);
            this.Controls.Add(this.Button_stop);
            this.Controls.Add(this.Button_start);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "直线圆弧测试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_runstate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioMode4;
        private System.Windows.Forms.RadioButton radioMode3;
        private System.Windows.Forms.RadioButton radioMode2;
        private System.Windows.Forms.RadioButton radioMode1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBox_dec;
        private System.Windows.Forms.TextBox textBox_acc;
        private System.Windows.Forms.TextBox textBox_speed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox midpos4;
        private System.Windows.Forms.TextBox midpos3;
        private System.Windows.Forms.TextBox midpos2;
        private System.Windows.Forms.TextBox midpos1;
        private System.Windows.Forms.TextBox destpos4;
        private System.Windows.Forms.TextBox destpos3;
        private System.Windows.Forms.TextBox destpos2;
        private System.Windows.Forms.TextBox destpos1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button Button_start;
        private System.Windows.Forms.Button Button_stop;
        private System.Windows.Forms.Button Button_zero;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button C_Close_Card;
        private System.Windows.Forms.Button C_Open_Eth;
        private System.Windows.Forms.ComboBox C_Ip_Address;
        private System.Windows.Forms.ComboBox C_connect_type;
    }
}

