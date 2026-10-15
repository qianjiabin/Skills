namespace test_faction6
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_rapidstop = new System.Windows.Forms.Button();
            this.C_Close_Card = new System.Windows.Forms.Button();
            this.C_Open_Eth = new System.Windows.Forms.Button();
            this.C_Ip_Address = new System.Windows.Forms.ComboBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.C_connect_type = new System.Windows.Forms.ComboBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button_clear = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_HandMove_connect = new System.Windows.Forms.Button();
            this.radioButton_Handweel_type2 = new System.Windows.Forms.RadioButton();
            this.radioButton_Handweel_type1 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton_rate_x1 = new System.Windows.Forms.RadioButton();
            this.radioButton_rate_x100 = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButton_rate_x10 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.textBox_HandMove_axis = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_state = new System.Windows.Forms.Label();
            this.label_e = new System.Windows.Forms.Label();
            this.label_z = new System.Windows.Forms.Label();
            this.label_y = new System.Windows.Forms.Label();
            this.label_x = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_IN_stop = new System.Windows.Forms.TextBox();
            this.textBox_IN_rate100 = new System.Windows.Forms.TextBox();
            this.textBox_IN_rate10 = new System.Windows.Forms.TextBox();
            this.textBox_IN_rate1 = new System.Windows.Forms.TextBox();
            this.textBox_IN_Z = new System.Windows.Forms.TextBox();
            this.textBox_IN_Y = new System.Windows.Forms.TextBox();
            this.textBox_IN_X = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButton_Axis_Z = new System.Windows.Forms.RadioButton();
            this.radioButton_Axis_Y = new System.Windows.Forms.RadioButton();
            this.radioButton_Axis_X = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.button_default = new System.Windows.Forms.Button();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_rapidstop
            // 
            this.button_rapidstop.Location = new System.Drawing.Point(6, 111);
            this.button_rapidstop.Name = "button_rapidstop";
            this.button_rapidstop.Size = new System.Drawing.Size(70, 30);
            this.button_rapidstop.TabIndex = 20;
            this.button_rapidstop.Text = "紧急停止";
            this.button_rapidstop.UseVisualStyleBackColor = true;
            this.button_rapidstop.Click += new System.EventHandler(this.btn_rapidstop);
            // 
            // C_Close_Card
            // 
            this.C_Close_Card.Location = new System.Drawing.Point(314, 20);
            this.C_Close_Card.Name = "C_Close_Card";
            this.C_Close_Card.Size = new System.Drawing.Size(55, 21);
            this.C_Close_Card.TabIndex = 29;
            this.C_Close_Card.Text = "断开";
            this.C_Close_Card.UseVisualStyleBackColor = true;
            this.C_Close_Card.Click += new System.EventHandler(this.OnClose);
            // 
            // C_Open_Eth
            // 
            this.C_Open_Eth.Location = new System.Drawing.Point(235, 20);
            this.C_Open_Eth.Name = "C_Open_Eth";
            this.C_Open_Eth.Size = new System.Drawing.Size(55, 21);
            this.C_Open_Eth.TabIndex = 28;
            this.C_Open_Eth.Text = "连接";
            this.C_Open_Eth.UseVisualStyleBackColor = true;
            this.C_Open_Eth.Click += new System.EventHandler(this.OnOpen);
            // 
            // C_Ip_Address
            // 
            this.C_Ip_Address.FormattingEnabled = true;
            this.C_Ip_Address.Location = new System.Drawing.Point(99, 20);
            this.C_Ip_Address.Name = "C_Ip_Address";
            this.C_Ip_Address.Size = new System.Drawing.Size(105, 20);
            this.C_Ip_Address.TabIndex = 27;
            this.C_Ip_Address.DropDown += new System.EventHandler(this.OnScan);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.C_Close_Card);
            this.groupBox5.Controls.Add(this.C_Open_Eth);
            this.groupBox5.Controls.Add(this.C_Ip_Address);
            this.groupBox5.Controls.Add(this.C_connect_type);
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(378, 49);
            this.groupBox5.TabIndex = 19;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "连接控制器";
            // 
            // C_connect_type
            // 
            this.C_connect_type.FormattingEnabled = true;
            this.C_connect_type.Items.AddRange(new object[] {
            "网口",
            "LOCAL",
            "PCI",
            "串口"});
            this.C_connect_type.Location = new System.Drawing.Point(10, 20);
            this.C_connect_type.Margin = new System.Windows.Forms.Padding(2);
            this.C_connect_type.Name = "C_connect_type";
            this.C_connect_type.Size = new System.Drawing.Size(59, 20);
            this.C_connect_type.TabIndex = 26;
            this.C_connect_type.DropDown += new System.EventHandler(this.OnSelect);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button_clear
            // 
            this.button_clear.Location = new System.Drawing.Point(6, 151);
            this.button_clear.Name = "button_clear";
            this.button_clear.Size = new System.Drawing.Size(70, 30);
            this.button_clear.TabIndex = 18;
            this.button_clear.Text = "位置清零";
            this.button_clear.UseVisualStyleBackColor = true;
            this.button_clear.Click += new System.EventHandler(this.all_clear);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(6, 71);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(70, 30);
            this.button_cancel.TabIndex = 17;
            this.button_cancel.Text = "断开连接";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_stop_click);
            // 
            // button_HandMove_connect
            // 
            this.button_HandMove_connect.Location = new System.Drawing.Point(6, 31);
            this.button_HandMove_connect.Name = "button_HandMove_connect";
            this.button_HandMove_connect.Size = new System.Drawing.Size(70, 30);
            this.button_HandMove_connect.TabIndex = 16;
            this.button_HandMove_connect.Text = "手轮链接";
            this.button_HandMove_connect.UseVisualStyleBackColor = true;
            this.button_HandMove_connect.Click += new System.EventHandler(this.button_HandMove_connect_Click);
            // 
            // radioButton_Handweel_type2
            // 
            this.radioButton_Handweel_type2.AutoSize = true;
            this.radioButton_Handweel_type2.Location = new System.Drawing.Point(5, 56);
            this.radioButton_Handweel_type2.Name = "radioButton_Handweel_type2";
            this.radioButton_Handweel_type2.Size = new System.Drawing.Size(101, 16);
            this.radioButton_Handweel_type2.TabIndex = 4;
            this.radioButton_Handweel_type2.Text = "脉冲+方向手轮";
            this.radioButton_Handweel_type2.UseVisualStyleBackColor = true;
            // 
            // radioButton_Handweel_type1
            // 
            this.radioButton_Handweel_type1.AutoSize = true;
            this.radioButton_Handweel_type1.Checked = true;
            this.radioButton_Handweel_type1.Location = new System.Drawing.Point(5, 32);
            this.radioButton_Handweel_type1.Name = "radioButton_Handweel_type1";
            this.radioButton_Handweel_type1.Size = new System.Drawing.Size(71, 16);
            this.radioButton_Handweel_type1.TabIndex = 3;
            this.radioButton_Handweel_type1.TabStop = true;
            this.radioButton_Handweel_type1.Text = "AB相手轮";
            this.radioButton_Handweel_type1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton_Handweel_type1);
            this.groupBox4.Controls.Add(this.radioButton_Handweel_type2);
            this.groupBox4.Location = new System.Drawing.Point(5, 125);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(110, 98);
            this.groupBox4.TabIndex = 12;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "手轮类型";
            // 
            // radioButton_rate_x1
            // 
            this.radioButton_rate_x1.AutoSize = true;
            this.radioButton_rate_x1.Checked = true;
            this.radioButton_rate_x1.Location = new System.Drawing.Point(29, 317);
            this.radioButton_rate_x1.Name = "radioButton_rate_x1";
            this.radioButton_rate_x1.Size = new System.Drawing.Size(35, 16);
            this.radioButton_rate_x1.TabIndex = 5;
            this.radioButton_rate_x1.TabStop = true;
            this.radioButton_rate_x1.Text = "x1";
            this.radioButton_rate_x1.UseVisualStyleBackColor = true;
            // 
            // radioButton_rate_x100
            // 
            this.radioButton_rate_x100.AutoSize = true;
            this.radioButton_rate_x100.Location = new System.Drawing.Point(29, 367);
            this.radioButton_rate_x100.Name = "radioButton_rate_x100";
            this.radioButton_rate_x100.Size = new System.Drawing.Size(47, 16);
            this.radioButton_rate_x100.TabIndex = 7;
            this.radioButton_rate_x100.Text = "x100";
            this.radioButton_rate_x100.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "紧急停止";
            // 
            // radioButton_rate_x10
            // 
            this.radioButton_rate_x10.AutoSize = true;
            this.radioButton_rate_x10.Location = new System.Drawing.Point(29, 342);
            this.radioButton_rate_x10.Name = "radioButton_rate_x10";
            this.radioButton_rate_x10.Size = new System.Drawing.Size(41, 16);
            this.radioButton_rate_x10.TabIndex = 6;
            this.radioButton_rate_x10.Text = "x10";
            this.radioButton_rate_x10.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.groupBox6);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Location = new System.Drawing.Point(171, 160);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(120, 235);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "手轮设置";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.textBox_HandMove_axis);
            this.groupBox6.Location = new System.Drawing.Point(5, 25);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(110, 81);
            this.groupBox6.TabIndex = 21;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "手轮轴号";
            // 
            // textBox_HandMove_axis
            // 
            this.textBox_HandMove_axis.Location = new System.Drawing.Point(21, 36);
            this.textBox_HandMove_axis.Name = "textBox_HandMove_axis";
            this.textBox_HandMove_axis.Size = new System.Drawing.Size(72, 21);
            this.textBox_HandMove_axis.TabIndex = 0;
            this.textBox_HandMove_axis.Text = "6";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_state);
            this.groupBox1.Controls.Add(this.label_e);
            this.groupBox1.Controls.Add(this.label_z);
            this.groupBox1.Controls.Add(this.label_y);
            this.groupBox1.Controls.Add(this.label_x);
            this.groupBox1.Location = new System.Drawing.Point(12, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 86);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "轴状态";
            // 
            // label_state
            // 
            this.label_state.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_state.Location = new System.Drawing.Point(8, 54);
            this.label_state.Name = "label_state";
            this.label_state.Size = new System.Drawing.Size(216, 22);
            this.label_state.TabIndex = 4;
            this.label_state.Text = "手轮链接状态：未链接";
            // 
            // label_e
            // 
            this.label_e.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_e.Location = new System.Drawing.Point(286, 22);
            this.label_e.Name = "label_e";
            this.label_e.Size = new System.Drawing.Size(88, 22);
            this.label_e.TabIndex = 3;
            this.label_e.Text = "编码轴:0";
            // 
            // label_z
            // 
            this.label_z.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_z.Location = new System.Drawing.Point(198, 22);
            this.label_z.Name = "label_z";
            this.label_z.Size = new System.Drawing.Size(69, 22);
            this.label_z.TabIndex = 2;
            this.label_z.Text = "Z轴:0";
            // 
            // label_y
            // 
            this.label_y.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_y.Location = new System.Drawing.Point(105, 22);
            this.label_y.Name = "label_y";
            this.label_y.Size = new System.Drawing.Size(69, 22);
            this.label_y.TabIndex = 1;
            this.label_y.Text = "Y轴:0";
            // 
            // label_x
            // 
            this.label_x.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_x.Location = new System.Drawing.Point(12, 22);
            this.label_x.Name = "label_x";
            this.label_x.Size = new System.Drawing.Size(69, 22);
            this.label_x.TabIndex = 0;
            this.label_x.Text = "X轴:0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_IN_stop);
            this.groupBox2.Controls.Add(this.textBox_IN_rate100);
            this.groupBox2.Controls.Add(this.textBox_IN_rate10);
            this.groupBox2.Controls.Add(this.textBox_IN_rate1);
            this.groupBox2.Controls.Add(this.textBox_IN_Z);
            this.groupBox2.Controls.Add(this.textBox_IN_Y);
            this.groupBox2.Controls.Add(this.textBox_IN_X);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.radioButton_Axis_Z);
            this.groupBox2.Controls.Add(this.radioButton_Axis_Y);
            this.groupBox2.Controls.Add(this.radioButton_Axis_X);
            this.groupBox2.Location = new System.Drawing.Point(12, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(142, 235);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "轴选择";
            // 
            // textBox_IN_stop
            // 
            this.textBox_IN_stop.Location = new System.Drawing.Point(76, 129);
            this.textBox_IN_stop.Name = "textBox_IN_stop";
            this.textBox_IN_stop.Size = new System.Drawing.Size(50, 21);
            this.textBox_IN_stop.TabIndex = 6;
            this.textBox_IN_stop.Text = "47";
            // 
            // textBox_IN_rate100
            // 
            this.textBox_IN_rate100.Location = new System.Drawing.Point(76, 206);
            this.textBox_IN_rate100.Name = "textBox_IN_rate100";
            this.textBox_IN_rate100.Size = new System.Drawing.Size(50, 21);
            this.textBox_IN_rate100.TabIndex = 5;
            this.textBox_IN_rate100.Text = "40";
            // 
            // textBox_IN_rate10
            // 
            this.textBox_IN_rate10.Location = new System.Drawing.Point(76, 180);
            this.textBox_IN_rate10.Name = "textBox_IN_rate10";
            this.textBox_IN_rate10.Size = new System.Drawing.Size(50, 21);
            this.textBox_IN_rate10.TabIndex = 5;
            this.textBox_IN_rate10.Text = "39";
            // 
            // textBox_IN_rate1
            // 
            this.textBox_IN_rate1.Location = new System.Drawing.Point(76, 156);
            this.textBox_IN_rate1.Name = "textBox_IN_rate1";
            this.textBox_IN_rate1.Size = new System.Drawing.Size(50, 21);
            this.textBox_IN_rate1.TabIndex = 5;
            this.textBox_IN_rate1.Text = "38";
            // 
            // textBox_IN_Z
            // 
            this.textBox_IN_Z.Location = new System.Drawing.Point(76, 102);
            this.textBox_IN_Z.Name = "textBox_IN_Z";
            this.textBox_IN_Z.Size = new System.Drawing.Size(50, 21);
            this.textBox_IN_Z.TabIndex = 4;
            this.textBox_IN_Z.Text = "43";
            // 
            // textBox_IN_Y
            // 
            this.textBox_IN_Y.Location = new System.Drawing.Point(76, 75);
            this.textBox_IN_Y.Name = "textBox_IN_Y";
            this.textBox_IN_Y.Size = new System.Drawing.Size(50, 21);
            this.textBox_IN_Y.TabIndex = 4;
            this.textBox_IN_Y.Text = "42";
            // 
            // textBox_IN_X
            // 
            this.textBox_IN_X.Location = new System.Drawing.Point(76, 48);
            this.textBox_IN_X.Name = "textBox_IN_X";
            this.textBox_IN_X.Size = new System.Drawing.Size(50, 21);
            this.textBox_IN_X.TabIndex = 4;
            this.textBox_IN_X.Text = "41";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(71, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "对应输入口";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "轴选择";
            // 
            // radioButton_Axis_Z
            // 
            this.radioButton_Axis_Z.AutoSize = true;
            this.radioButton_Axis_Z.Location = new System.Drawing.Point(17, 103);
            this.radioButton_Axis_Z.Name = "radioButton_Axis_Z";
            this.radioButton_Axis_Z.Size = new System.Drawing.Size(41, 16);
            this.radioButton_Axis_Z.TabIndex = 2;
            this.radioButton_Axis_Z.Text = "Z轴";
            this.radioButton_Axis_Z.UseVisualStyleBackColor = true;
            // 
            // radioButton_Axis_Y
            // 
            this.radioButton_Axis_Y.AutoSize = true;
            this.radioButton_Axis_Y.Location = new System.Drawing.Point(17, 76);
            this.radioButton_Axis_Y.Name = "radioButton_Axis_Y";
            this.radioButton_Axis_Y.Size = new System.Drawing.Size(41, 16);
            this.radioButton_Axis_Y.TabIndex = 1;
            this.radioButton_Axis_Y.Text = "Y轴";
            this.radioButton_Axis_Y.UseVisualStyleBackColor = true;
            // 
            // radioButton_Axis_X
            // 
            this.radioButton_Axis_X.AutoSize = true;
            this.radioButton_Axis_X.Checked = true;
            this.radioButton_Axis_X.Location = new System.Drawing.Point(17, 49);
            this.radioButton_Axis_X.Name = "radioButton_Axis_X";
            this.radioButton_Axis_X.Size = new System.Drawing.Size(41, 16);
            this.radioButton_Axis_X.TabIndex = 0;
            this.radioButton_Axis_X.TabStop = true;
            this.radioButton_Axis_X.Text = "X轴";
            this.radioButton_Axis_X.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.button_default);
            this.groupBox7.Controls.Add(this.button_rapidstop);
            this.groupBox7.Controls.Add(this.button_HandMove_connect);
            this.groupBox7.Controls.Add(this.button_cancel);
            this.groupBox7.Controls.Add(this.button_clear);
            this.groupBox7.Location = new System.Drawing.Point(311, 160);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(82, 235);
            this.groupBox7.TabIndex = 21;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "按钮";
            // 
            // button_default
            // 
            this.button_default.Location = new System.Drawing.Point(6, 192);
            this.button_default.Name = "button_default";
            this.button_default.Size = new System.Drawing.Size(70, 30);
            this.button_default.TabIndex = 21;
            this.button_default.Text = "恢复默认";
            this.button_default.UseVisualStyleBackColor = true;
            this.button_default.Click += new System.EventHandler(this.resume_default);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 401);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.radioButton_rate_x1);
            this.Controls.Add(this.radioButton_rate_x100);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.radioButton_rate_x10);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.MaximumSize = new System.Drawing.Size(420, 440);
            this.MinimumSize = new System.Drawing.Size(420, 440);
            this.Name = "Form1";
            this.Text = "手轮运动";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_rapidstop;
        private System.Windows.Forms.Button C_Close_Card;
        private System.Windows.Forms.Button C_Open_Eth;
        private System.Windows.Forms.ComboBox C_Ip_Address;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.ComboBox C_connect_type;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button_clear;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_HandMove_connect;
        private System.Windows.Forms.RadioButton radioButton_Handweel_type2;
        private System.Windows.Forms.RadioButton radioButton_Handweel_type1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton_rate_x1;
        private System.Windows.Forms.RadioButton radioButton_rate_x100;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButton_rate_x10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_state;
        private System.Windows.Forms.Label label_e;
        private System.Windows.Forms.Label label_z;
        private System.Windows.Forms.Label label_y;
        private System.Windows.Forms.Label label_x;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_IN_Z;
        private System.Windows.Forms.TextBox textBox_IN_Y;
        private System.Windows.Forms.TextBox textBox_IN_X;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButton_Axis_Z;
        private System.Windows.Forms.RadioButton radioButton_Axis_Y;
        private System.Windows.Forms.RadioButton radioButton_Axis_X;
        private System.Windows.Forms.TextBox textBox_IN_rate100;
        private System.Windows.Forms.TextBox textBox_IN_rate10;
        private System.Windows.Forms.TextBox textBox_IN_rate1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox textBox_IN_stop;
        private System.Windows.Forms.TextBox textBox_HandMove_axis;
        private System.Windows.Forms.Button button_default;
    }
}

