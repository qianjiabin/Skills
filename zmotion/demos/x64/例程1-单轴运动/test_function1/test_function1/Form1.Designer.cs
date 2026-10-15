namespace test_function1
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_cursp = new System.Windows.Forms.Label();
            this.label_curpos = new System.Windows.Forms.Label();
            this.label_runstate = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.TextBox_sramp = new System.Windows.Forms.TextBox();
            this.TextBox_decel = new System.Windows.Forms.TextBox();
            this.TextBox_accel = new System.Windows.Forms.TextBox();
            this.TextBox_speed = new System.Windows.Forms.TextBox();
            this.TextBox_lspeed = new System.Windows.Forms.TextBox();
            this.TextBox_units = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.TextBox_step = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.C_connect_type = new System.Windows.Forms.ComboBox();
            this.C_Close_Card = new System.Windows.Forms.Button();
            this.C_Open_Eth = new System.Windows.Forms.Button();
            this.C_Ip_Address = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label_cursp);
            this.groupBox2.Controls.Add(this.label_curpos);
            this.groupBox2.Controls.Add(this.label_runstate);
            this.groupBox2.Location = new System.Drawing.Point(12, 97);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 115);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "状态位置显示";
            // 
            // label_cursp
            // 
            this.label_cursp.BackColor = System.Drawing.SystemColors.Control;
            this.label_cursp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_cursp.Location = new System.Drawing.Point(16, 85);
            this.label_cursp.Name = "label_cursp";
            this.label_cursp.Size = new System.Drawing.Size(159, 22);
            this.label_cursp.TabIndex = 2;
            this.label_cursp.Text = "当前轴速度：0";
            this.label_cursp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_curpos
            // 
            this.label_curpos.BackColor = System.Drawing.SystemColors.Control;
            this.label_curpos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_curpos.Location = new System.Drawing.Point(16, 54);
            this.label_curpos.Name = "label_curpos";
            this.label_curpos.Size = new System.Drawing.Size(159, 22);
            this.label_curpos.TabIndex = 1;
            this.label_curpos.Text = "当前轴坐标：0";
            this.label_curpos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_runstate
            // 
            this.label_runstate.BackColor = System.Drawing.SystemColors.Control;
            this.label_runstate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_runstate.Location = new System.Drawing.Point(16, 23);
            this.label_runstate.Name = "label_runstate";
            this.label_runstate.Size = new System.Drawing.Size(159, 22);
            this.label_runstate.TabIndex = 0;
            this.label_runstate.Text = "运动状态：停止中";
            this.label_runstate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton3);
            this.groupBox3.Controls.Add(this.radioButton4);
            this.groupBox3.Controls.Add(this.radioButton2);
            this.groupBox3.Controls.Add(this.radioButton1);
            this.groupBox3.Location = new System.Drawing.Point(232, 97);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(155, 88);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "轴选择";
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(80, 53);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(29, 16);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "R";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.OnRadio1R);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(17, 53);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(29, 16);
            this.radioButton4.TabIndex = 2;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Z";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.OnRadio1Z);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(80, 22);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(29, 16);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Y";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.OnRadio1Y);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(17, 22);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(29, 16);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "X";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.OnRadio1X);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.TextBox_sramp);
            this.groupBox4.Controls.Add(this.TextBox_decel);
            this.groupBox4.Controls.Add(this.TextBox_accel);
            this.groupBox4.Controls.Add(this.TextBox_speed);
            this.groupBox4.Controls.Add(this.TextBox_lspeed);
            this.groupBox4.Controls.Add(this.TextBox_units);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Location = new System.Drawing.Point(12, 229);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(189, 241);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "参数设置";
            // 
            // TextBox_sramp
            // 
            this.TextBox_sramp.Location = new System.Drawing.Point(88, 203);
            this.TextBox_sramp.Name = "TextBox_sramp";
            this.TextBox_sramp.Size = new System.Drawing.Size(75, 21);
            this.TextBox_sramp.TabIndex = 11;
            this.TextBox_sramp.Text = "0";
            // 
            // TextBox_decel
            // 
            this.TextBox_decel.Location = new System.Drawing.Point(88, 167);
            this.TextBox_decel.Name = "TextBox_decel";
            this.TextBox_decel.Size = new System.Drawing.Size(75, 21);
            this.TextBox_decel.TabIndex = 10;
            this.TextBox_decel.Text = "1000";
            // 
            // TextBox_accel
            // 
            this.TextBox_accel.Location = new System.Drawing.Point(88, 131);
            this.TextBox_accel.Name = "TextBox_accel";
            this.TextBox_accel.Size = new System.Drawing.Size(75, 21);
            this.TextBox_accel.TabIndex = 9;
            this.TextBox_accel.Text = "1000";
            // 
            // TextBox_speed
            // 
            this.TextBox_speed.Location = new System.Drawing.Point(90, 95);
            this.TextBox_speed.Name = "TextBox_speed";
            this.TextBox_speed.Size = new System.Drawing.Size(75, 21);
            this.TextBox_speed.TabIndex = 8;
            this.TextBox_speed.Text = "100";
            // 
            // TextBox_lspeed
            // 
            this.TextBox_lspeed.Location = new System.Drawing.Point(88, 59);
            this.TextBox_lspeed.Name = "TextBox_lspeed";
            this.TextBox_lspeed.Size = new System.Drawing.Size(75, 21);
            this.TextBox_lspeed.TabIndex = 7;
            this.TextBox_lspeed.Text = "0";
            // 
            // TextBox_units
            // 
            this.TextBox_units.Location = new System.Drawing.Point(89, 23);
            this.TextBox_units.Name = "TextBox_units";
            this.TextBox_units.Size = new System.Drawing.Size(75, 21);
            this.TextBox_units.TabIndex = 6;
            this.TextBox_units.Text = "1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 210);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "S曲线：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 174);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 4;
            this.label6.Text = "减速度：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 3;
            this.label5.Text = "加速度：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "运行速度：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "起始速度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "脉冲当量：";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.TextBox_step);
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.radioButton6);
            this.groupBox5.Controls.Add(this.radioButton5);
            this.groupBox5.Controls.Add(this.checkBox1);
            this.groupBox5.Location = new System.Drawing.Point(235, 195);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(151, 115);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "运动方式";
            // 
            // TextBox_step
            // 
            this.TextBox_step.Location = new System.Drawing.Point(75, 84);
            this.TextBox_step.Name = "TextBox_step";
            this.TextBox_step.Size = new System.Drawing.Size(64, 21);
            this.TextBox_step.TabIndex = 4;
            this.TextBox_step.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "寸动距离";
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(77, 56);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(47, 16);
            this.radioButton6.TabIndex = 2;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "寸动";
            this.radioButton6.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Checked = true;
            this.radioButton5.Location = new System.Drawing.Point(15, 56);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(47, 16);
            this.radioButton5.TabIndex = 1;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "持续";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(25, 22);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "运动方向：正";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.OnCheckDir);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(258, 333);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(92, 26);
            this.button1.TabIndex = 5;
            this.button1.Text = "运  动";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.OnStart);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(258, 384);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 26);
            this.button2.TabIndex = 6;
            this.button2.Text = "停  止";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.OnStop);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(258, 435);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(92, 26);
            this.button3.TabIndex = 7;
            this.button3.Text = "坐标清零";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.OnZero);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.C_connect_type);
            this.groupBox1.Controls.Add(this.C_Close_Card);
            this.groupBox1.Controls.Add(this.C_Open_Eth);
            this.groupBox1.Controls.Add(this.C_Ip_Address);
            this.groupBox1.Location = new System.Drawing.Point(12, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 67);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接控制器";
            // 
            // C_connect_type
            // 
            this.C_connect_type.FormattingEnabled = true;
            this.C_connect_type.Items.AddRange(new object[] {
            "网口",
            "LOCAL",
            "PCI",
            "串口"});
            this.C_connect_type.Location = new System.Drawing.Point(7, 28);
            this.C_connect_type.Margin = new System.Windows.Forms.Padding(2);
            this.C_connect_type.Name = "C_connect_type";
            this.C_connect_type.Size = new System.Drawing.Size(59, 20);
            this.C_connect_type.TabIndex = 13;
            this.C_connect_type.DropDown += new System.EventHandler(this.C_connect_type_DropDown);
            // 
            // C_Close_Card
            // 
            this.C_Close_Card.Location = new System.Drawing.Point(288, 25);
            this.C_Close_Card.Name = "C_Close_Card";
            this.C_Close_Card.Size = new System.Drawing.Size(55, 21);
            this.C_Close_Card.TabIndex = 9;
            this.C_Close_Card.Text = "断开";
            this.C_Close_Card.UseVisualStyleBackColor = true;
            this.C_Close_Card.Click += new System.EventHandler(this.OnClose);
            // 
            // C_Open_Eth
            // 
            this.C_Open_Eth.Location = new System.Drawing.Point(211, 25);
            this.C_Open_Eth.Name = "C_Open_Eth";
            this.C_Open_Eth.Size = new System.Drawing.Size(55, 21);
            this.C_Open_Eth.TabIndex = 7;
            this.C_Open_Eth.Text = "连接";
            this.C_Open_Eth.UseVisualStyleBackColor = true;
            this.C_Open_Eth.Click += new System.EventHandler(this.OnOpen);
            // 
            // C_Ip_Address
            // 
            this.C_Ip_Address.FormattingEnabled = true;
            this.C_Ip_Address.Location = new System.Drawing.Point(85, 28);
            this.C_Ip_Address.Name = "C_Ip_Address";
            this.C_Ip_Address.Size = new System.Drawing.Size(105, 20);
            this.C_Ip_Address.TabIndex = 4;
            this.C_Ip_Address.DropDown += new System.EventHandler(this.C_Ip_Address_DropDown);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(411, 482);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "单轴测试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox TextBox_step;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label_cursp;
        private System.Windows.Forms.Label label_curpos;
        private System.Windows.Forms.Label label_runstate;
        private System.Windows.Forms.TextBox TextBox_sramp;
        private System.Windows.Forms.TextBox TextBox_decel;
        private System.Windows.Forms.TextBox TextBox_accel;
        private System.Windows.Forms.TextBox TextBox_speed;
        private System.Windows.Forms.TextBox TextBox_lspeed;
        private System.Windows.Forms.TextBox TextBox_units;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button C_Close_Card;
        private System.Windows.Forms.Button C_Open_Eth;
        private System.Windows.Forms.ComboBox C_Ip_Address;
        private System.Windows.Forms.ComboBox C_connect_type;
    }
}

