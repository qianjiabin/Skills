namespace test_faction5
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
            this.label_pos = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox_dec = new System.Windows.Forms.TextBox();
            this.textBox_acc = new System.Windows.Forms.TextBox();
            this.textBox_sp = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label_mark = new System.Windows.Forms.Label();
            this.label_buff = new System.Windows.Forms.Label();
            this.label_vspeed = new System.Windows.Forms.Label();
            this.label_state = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.textBox_zsmooth = new System.Windows.Forms.TextBox();
            this.textBox_radio = new System.Windows.Forms.TextBox();
            this.textBox_ang2 = new System.Windows.Forms.TextBox();
            this.textBox_ang1 = new System.Windows.Forms.TextBox();
            this.textBox_lsp = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button_start = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.button_zero = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.C_Close_Card = new System.Windows.Forms.Button();
            this.C_Open_Eth = new System.Windows.Forms.Button();
            this.C_Ip_Address = new System.Windows.Forms.ComboBox();
            this.C_connect_type = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_pos);
            this.groupBox1.Location = new System.Drawing.Point(5, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(375, 61);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "运动坐标";
            // 
            // label_pos
            // 
            this.label_pos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_pos.Location = new System.Drawing.Point(24, 21);
            this.label_pos.Name = "label_pos";
            this.label_pos.Size = new System.Drawing.Size(333, 29);
            this.label_pos.TabIndex = 0;
            this.label_pos.Text = "X:0  Y:0  Z:0  U:0";
            this.label_pos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox_dec);
            this.groupBox2.Controls.Add(this.textBox_acc);
            this.groupBox2.Controls.Add(this.textBox_sp);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(8, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(182, 116);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "插补参数";
            // 
            // textBox_dec
            // 
            this.textBox_dec.Location = new System.Drawing.Point(75, 79);
            this.textBox_dec.Name = "textBox_dec";
            this.textBox_dec.Size = new System.Drawing.Size(58, 21);
            this.textBox_dec.TabIndex = 5;
            this.textBox_dec.Text = "1000";
            // 
            // textBox_acc
            // 
            this.textBox_acc.Location = new System.Drawing.Point(75, 51);
            this.textBox_acc.Name = "textBox_acc";
            this.textBox_acc.Size = new System.Drawing.Size(58, 21);
            this.textBox_acc.TabIndex = 4;
            this.textBox_acc.Text = "1000";
            // 
            // textBox_sp
            // 
            this.textBox_sp.Location = new System.Drawing.Point(75, 23);
            this.textBox_sp.Name = "textBox_sp";
            this.textBox_sp.Size = new System.Drawing.Size(58, 21);
            this.textBox_sp.TabIndex = 3;
            this.textBox_sp.Text = "100";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "减速度：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "加速度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "速度：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label_mark);
            this.groupBox3.Controls.Add(this.label_buff);
            this.groupBox3.Controls.Add(this.label_vspeed);
            this.groupBox3.Controls.Add(this.label_state);
            this.groupBox3.Location = new System.Drawing.Point(198, 158);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(182, 116);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "运动状态";
            // 
            // label_mark
            // 
            this.label_mark.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_mark.Location = new System.Drawing.Point(6, 91);
            this.label_mark.Name = "label_mark";
            this.label_mark.Size = new System.Drawing.Size(167, 18);
            this.label_mark.TabIndex = 3;
            this.label_mark.Text = "当前MARK：0";
            // 
            // label_buff
            // 
            this.label_buff.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_buff.Location = new System.Drawing.Point(6, 67);
            this.label_buff.Name = "label_buff";
            this.label_buff.Size = new System.Drawing.Size(167, 18);
            this.label_buff.TabIndex = 2;
            this.label_buff.Text = "剩余缓冲：0";
            // 
            // label_vspeed
            // 
            this.label_vspeed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_vspeed.Location = new System.Drawing.Point(6, 43);
            this.label_vspeed.Name = "label_vspeed";
            this.label_vspeed.Size = new System.Drawing.Size(167, 18);
            this.label_vspeed.TabIndex = 1;
            this.label_vspeed.Text = "当前速度：0";
            // 
            // label_state
            // 
            this.label_state.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_state.Location = new System.Drawing.Point(6, 19);
            this.label_state.Name = "label_state";
            this.label_state.Size = new System.Drawing.Size(167, 18);
            this.label_state.TabIndex = 0;
            this.label_state.Text = "运动状态：停止中";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.textBox_zsmooth);
            this.groupBox4.Controls.Add(this.textBox_radio);
            this.groupBox4.Controls.Add(this.textBox_ang2);
            this.groupBox4.Controls.Add(this.textBox_ang1);
            this.groupBox4.Controls.Add(this.textBox_lsp);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label5);
            this.groupBox4.Controls.Add(this.checkBox3);
            this.groupBox4.Controls.Add(this.checkBox2);
            this.groupBox4.Controls.Add(this.checkBox1);
            this.groupBox4.Location = new System.Drawing.Point(8, 275);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(372, 151);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "拐角减速设置";
            // 
            // textBox_zsmooth
            // 
            this.textBox_zsmooth.Location = new System.Drawing.Point(271, 120);
            this.textBox_zsmooth.Name = "textBox_zsmooth";
            this.textBox_zsmooth.Size = new System.Drawing.Size(78, 21);
            this.textBox_zsmooth.TabIndex = 15;
            this.textBox_zsmooth.Text = "5";
            // 
            // textBox_radio
            // 
            this.textBox_radio.Location = new System.Drawing.Point(271, 93);
            this.textBox_radio.Name = "textBox_radio";
            this.textBox_radio.Size = new System.Drawing.Size(78, 21);
            this.textBox_radio.TabIndex = 14;
            this.textBox_radio.Text = "10";
            // 
            // textBox_ang2
            // 
            this.textBox_ang2.Location = new System.Drawing.Point(271, 66);
            this.textBox_ang2.Name = "textBox_ang2";
            this.textBox_ang2.Size = new System.Drawing.Size(78, 21);
            this.textBox_ang2.TabIndex = 13;
            this.textBox_ang2.Text = "60";
            // 
            // textBox_ang1
            // 
            this.textBox_ang1.Location = new System.Drawing.Point(271, 39);
            this.textBox_ang1.Name = "textBox_ang1";
            this.textBox_ang1.Size = new System.Drawing.Size(78, 21);
            this.textBox_ang1.TabIndex = 12;
            this.textBox_ang1.Text = "15";
            // 
            // textBox_lsp
            // 
            this.textBox_lsp.Location = new System.Drawing.Point(271, 12);
            this.textBox_lsp.Name = "textBox_lsp";
            this.textBox_lsp.Size = new System.Drawing.Size(78, 21);
            this.textBox_lsp.TabIndex = 11;
            this.textBox_lsp.Text = "100";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(174, 128);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 10;
            this.label9.Text = "倒角半径：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(174, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 12);
            this.label8.TabIndex = 9;
            this.label8.Text = "小圆限速半径：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(174, 72);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "停止减速速度：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(174, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 7;
            this.label6.Text = "起始减速角度：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(174, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "起始速度：";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(20, 106);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 16);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "自动倒角";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(20, 65);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(96, 16);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "自动小圆限速";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(20, 25);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(96, 16);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.Text = "自动拐角减速";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.CheckBox_CheckedChanged);
            // 
            // button_start
            // 
            this.button_start.Location = new System.Drawing.Point(21, 432);
            this.button_start.Name = "button_start";
            this.button_start.Size = new System.Drawing.Size(84, 24);
            this.button_start.TabIndex = 4;
            this.button_start.Text = "启  动";
            this.button_start.UseVisualStyleBackColor = true;
            this.button_start.Click += new System.EventHandler(this.button_start_Click);
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(150, 432);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(84, 24);
            this.button_stop.TabIndex = 5;
            this.button_stop.Text = "停  止";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // button_zero
            // 
            this.button_zero.Location = new System.Drawing.Point(279, 432);
            this.button_zero.Name = "button_zero";
            this.button_zero.Size = new System.Drawing.Size(84, 24);
            this.button_zero.TabIndex = 6;
            this.button_zero.Text = "位置清零";
            this.button_zero.UseVisualStyleBackColor = true;
            this.button_zero.Click += new System.EventHandler(this.button_zero_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.C_Close_Card);
            this.groupBox5.Controls.Add(this.C_Open_Eth);
            this.groupBox5.Controls.Add(this.C_Ip_Address);
            this.groupBox5.Controls.Add(this.C_connect_type);
            this.groupBox5.Location = new System.Drawing.Point(5, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(372, 68);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "连接控制器";
            // 
            // C_Close_Card
            // 
            this.C_Close_Card.Location = new System.Drawing.Point(305, 24);
            this.C_Close_Card.Name = "C_Close_Card";
            this.C_Close_Card.Size = new System.Drawing.Size(55, 21);
            this.C_Close_Card.TabIndex = 29;
            this.C_Close_Card.Text = "断开";
            this.C_Close_Card.UseVisualStyleBackColor = true;
            this.C_Close_Card.Click += new System.EventHandler(this.OnClose);
            // 
            // C_Open_Eth
            // 
            this.C_Open_Eth.Location = new System.Drawing.Point(221, 24);
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
            this.C_Ip_Address.Location = new System.Drawing.Point(86, 24);
            this.C_Ip_Address.Name = "C_Ip_Address";
            this.C_Ip_Address.Size = new System.Drawing.Size(105, 20);
            this.C_Ip_Address.TabIndex = 27;
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
            this.C_connect_type.Location = new System.Drawing.Point(7, 24);
            this.C_connect_type.Margin = new System.Windows.Forms.Padding(2);
            this.C_connect_type.Name = "C_connect_type";
            this.C_connect_type.Size = new System.Drawing.Size(59, 20);
            this.C_connect_type.TabIndex = 26;
            this.C_connect_type.DropDown += new System.EventHandler(this.OnSelect);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(389, 466);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.button_zero);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.button_start);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "连续插补测试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_pos;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button_start;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button_zero;
        private System.Windows.Forms.TextBox textBox_dec;
        private System.Windows.Forms.TextBox textBox_acc;
        private System.Windows.Forms.TextBox textBox_sp;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_mark;
        private System.Windows.Forms.Label label_buff;
        private System.Windows.Forms.Label label_vspeed;
        private System.Windows.Forms.Label label_state;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox_zsmooth;
        private System.Windows.Forms.TextBox textBox_radio;
        private System.Windows.Forms.TextBox textBox_ang2;
        private System.Windows.Forms.TextBox textBox_ang1;
        private System.Windows.Forms.TextBox textBox_lsp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button C_Close_Card;
        private System.Windows.Forms.Button C_Open_Eth;
        private System.Windows.Forms.ComboBox C_Ip_Address;
        private System.Windows.Forms.ComboBox C_connect_type;
    }
}

