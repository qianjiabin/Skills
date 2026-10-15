namespace test_faction2
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
            System.Windows.Forms.Label label11;
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_R = new System.Windows.Forms.Label();
            this.label_Z = new System.Windows.Forms.Label();
            this.label_Y = new System.Windows.Forms.Label();
            this.label_X = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.TextBox_revio = new System.Windows.Forms.TextBox();
            this.TextBox_fwdio = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TextBox_atype = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.TextBox_homeio = new System.Windows.Forms.TextBox();
            this.TextBox_decel = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TextBox_accel = new System.Windows.Forms.TextBox();
            this.TextBox_creep = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBox_speed = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.TextBox_lspeed = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TextBox_units = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton_R = new System.Windows.Forms.RadioButton();
            this.radioButton_Z = new System.Windows.Forms.RadioButton();
            this.radioButton_Y = new System.Windows.Forms.RadioButton();
            this.radioButton_X = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton11 = new System.Windows.Forms.RadioButton();
            this.radioButton10 = new System.Windows.Forms.RadioButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.radioButton9 = new System.Windows.Forms.RadioButton();
            this.radioButton8 = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButton7 = new System.Windows.Forms.RadioButton();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton_mode1 = new System.Windows.Forms.RadioButton();
            this.button_home = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.button_zero = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.C_Close_Card = new System.Windows.Forms.Button();
            this.C_Open_Eth = new System.Windows.Forms.Button();
            this.C_Ip_Address = new System.Windows.Forms.ComboBox();
            this.C_connect_type = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(13, 347);
            label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(65, 12);
            label11.TabIndex = 22;
            label11.Text = "负限位IO：";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox1.Controls.Add(this.label_R);
            this.groupBox1.Controls.Add(this.label_Z);
            this.groupBox1.Controls.Add(this.label_Y);
            this.groupBox1.Controls.Add(this.label_X);
            this.groupBox1.Location = new System.Drawing.Point(9, 69);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(368, 93);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "状态显示";
            // 
            // label_R
            // 
            this.label_R.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label_R.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_R.Location = new System.Drawing.Point(206, 61);
            this.label_R.Name = "label_R";
            this.label_R.Size = new System.Drawing.Size(150, 26);
            this.label_R.TabIndex = 3;
            this.label_R.Text = "R 停止 ：0";
            this.label_R.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Z
            // 
            this.label_Z.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label_Z.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Z.Location = new System.Drawing.Point(12, 61);
            this.label_Z.Name = "label_Z";
            this.label_Z.Size = new System.Drawing.Size(150, 26);
            this.label_Z.TabIndex = 2;
            this.label_Z.Text = "Z 停止 ：0";
            this.label_Z.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Y
            // 
            this.label_Y.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label_Y.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_Y.Location = new System.Drawing.Point(206, 22);
            this.label_Y.Name = "label_Y";
            this.label_Y.Size = new System.Drawing.Size(150, 26);
            this.label_Y.TabIndex = 1;
            this.label_Y.Text = "Y 停止 ：0";
            this.label_Y.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_X
            // 
            this.label_X.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label_X.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label_X.Location = new System.Drawing.Point(12, 21);
            this.label_X.Name = "label_X";
            this.label_X.Size = new System.Drawing.Size(150, 26);
            this.label_X.TabIndex = 0;
            this.label_X.Text = "X 停止 ：0";
            this.label_X.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.TextBox_revio);
            this.groupBox2.Controls.Add(this.TextBox_fwdio);
            this.groupBox2.Controls.Add(label11);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.TextBox_atype);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.TextBox_homeio);
            this.groupBox2.Controls.Add(this.TextBox_decel);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.TextBox_accel);
            this.groupBox2.Controls.Add(this.TextBox_creep);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.TextBox_speed);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.TextBox_lspeed);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.TextBox_units);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(10, 173);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(185, 411);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "参数设置";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(156, 265);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(29, 12);
            this.label12.TabIndex = 12;
            this.label12.Text = "反转";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(167, 348);
            this.checkBox3.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(15, 14);
            this.checkBox3.TabIndex = 26;
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(167, 316);
            this.checkBox2.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 25;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(167, 283);
            this.checkBox1.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 10;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // TextBox_revio
            // 
            this.TextBox_revio.Location = new System.Drawing.Point(80, 345);
            this.TextBox_revio.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_revio.Name = "TextBox_revio";
            this.TextBox_revio.Size = new System.Drawing.Size(75, 21);
            this.TextBox_revio.TabIndex = 24;
            this.TextBox_revio.Text = "6";
            // 
            // TextBox_fwdio
            // 
            this.TextBox_fwdio.Location = new System.Drawing.Point(80, 314);
            this.TextBox_fwdio.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_fwdio.Name = "TextBox_fwdio";
            this.TextBox_fwdio.Size = new System.Drawing.Size(75, 21);
            this.TextBox_fwdio.TabIndex = 23;
            this.TextBox_fwdio.Text = "3";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 316);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 21;
            this.label10.Text = "正限位IO：";
            // 
            // TextBox_atype
            // 
            this.TextBox_atype.Location = new System.Drawing.Point(80, 27);
            this.TextBox_atype.Margin = new System.Windows.Forms.Padding(2);
            this.TextBox_atype.Name = "TextBox_atype";
            this.TextBox_atype.Size = new System.Drawing.Size(75, 21);
            this.TextBox_atype.TabIndex = 20;
            this.TextBox_atype.Text = "7";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 31);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "轴类型：";
            // 
            // TextBox_homeio
            // 
            this.TextBox_homeio.Location = new System.Drawing.Point(80, 279);
            this.TextBox_homeio.Name = "TextBox_homeio";
            this.TextBox_homeio.Size = new System.Drawing.Size(75, 21);
            this.TextBox_homeio.TabIndex = 18;
            this.TextBox_homeio.Text = "0";
            // 
            // TextBox_decel
            // 
            this.TextBox_decel.Location = new System.Drawing.Point(80, 243);
            this.TextBox_decel.Name = "TextBox_decel";
            this.TextBox_decel.Size = new System.Drawing.Size(75, 21);
            this.TextBox_decel.TabIndex = 17;
            this.TextBox_decel.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 173);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 13;
            this.label7.Text = "爬行速度：";
            // 
            // TextBox_accel
            // 
            this.TextBox_accel.Location = new System.Drawing.Point(80, 207);
            this.TextBox_accel.Name = "TextBox_accel";
            this.TextBox_accel.Size = new System.Drawing.Size(75, 21);
            this.TextBox_accel.TabIndex = 16;
            this.TextBox_accel.Text = "1000";
            // 
            // TextBox_creep
            // 
            this.TextBox_creep.Location = new System.Drawing.Point(80, 171);
            this.TextBox_creep.Name = "TextBox_creep";
            this.TextBox_creep.Size = new System.Drawing.Size(75, 21);
            this.TextBox_creep.TabIndex = 15;
            this.TextBox_creep.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 283);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "原点IO：";
            // 
            // TextBox_speed
            // 
            this.TextBox_speed.Location = new System.Drawing.Point(80, 135);
            this.TextBox_speed.Name = "TextBox_speed";
            this.TextBox_speed.Size = new System.Drawing.Size(75, 21);
            this.TextBox_speed.TabIndex = 14;
            this.TextBox_speed.Text = "100";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 245);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "减速度：";
            // 
            // TextBox_lspeed
            // 
            this.TextBox_lspeed.Location = new System.Drawing.Point(80, 99);
            this.TextBox_lspeed.Name = "TextBox_lspeed";
            this.TextBox_lspeed.Size = new System.Drawing.Size(75, 21);
            this.TextBox_lspeed.TabIndex = 13;
            this.TextBox_lspeed.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 209);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "加速度：";
            // 
            // TextBox_units
            // 
            this.TextBox_units.Location = new System.Drawing.Point(80, 63);
            this.TextBox_units.Name = "TextBox_units";
            this.TextBox_units.Size = new System.Drawing.Size(75, 21);
            this.TextBox_units.TabIndex = 12;
            this.TextBox_units.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "运行速度：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "起始速度：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "脉冲当量：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton_R);
            this.groupBox3.Controls.Add(this.radioButton_Z);
            this.groupBox3.Controls.Add(this.radioButton_Y);
            this.groupBox3.Controls.Add(this.radioButton_X);
            this.groupBox3.Location = new System.Drawing.Point(200, 173);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(176, 70);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "轴选择";
            // 
            // radioButton_R
            // 
            this.radioButton_R.AutoSize = true;
            this.radioButton_R.Location = new System.Drawing.Point(106, 43);
            this.radioButton_R.Name = "radioButton_R";
            this.radioButton_R.Size = new System.Drawing.Size(41, 16);
            this.radioButton_R.TabIndex = 3;
            this.radioButton_R.TabStop = true;
            this.radioButton_R.Text = "R轴";
            this.radioButton_R.UseVisualStyleBackColor = true;
            this.radioButton_R.CheckedChanged += new System.EventHandler(this.radioButton_R_CheckedChanged);
            // 
            // radioButton_Z
            // 
            this.radioButton_Z.AutoSize = true;
            this.radioButton_Z.Location = new System.Drawing.Point(26, 43);
            this.radioButton_Z.Name = "radioButton_Z";
            this.radioButton_Z.Size = new System.Drawing.Size(41, 16);
            this.radioButton_Z.TabIndex = 2;
            this.radioButton_Z.TabStop = true;
            this.radioButton_Z.Text = "Z轴";
            this.radioButton_Z.UseVisualStyleBackColor = true;
            this.radioButton_Z.CheckedChanged += new System.EventHandler(this.radioButton_Z_CheckedChanged);
            // 
            // radioButton_Y
            // 
            this.radioButton_Y.AutoSize = true;
            this.radioButton_Y.Location = new System.Drawing.Point(106, 21);
            this.radioButton_Y.Name = "radioButton_Y";
            this.radioButton_Y.Size = new System.Drawing.Size(41, 16);
            this.radioButton_Y.TabIndex = 1;
            this.radioButton_Y.TabStop = true;
            this.radioButton_Y.Text = "Y轴";
            this.radioButton_Y.UseVisualStyleBackColor = true;
            this.radioButton_Y.CheckedChanged += new System.EventHandler(this.radioButton_Y_CheckedChanged);
            // 
            // radioButton_X
            // 
            this.radioButton_X.AutoSize = true;
            this.radioButton_X.Checked = true;
            this.radioButton_X.Location = new System.Drawing.Point(26, 21);
            this.radioButton_X.Name = "radioButton_X";
            this.radioButton_X.Size = new System.Drawing.Size(41, 16);
            this.radioButton_X.TabIndex = 0;
            this.radioButton_X.TabStop = true;
            this.radioButton_X.Text = "X轴";
            this.radioButton_X.UseVisualStyleBackColor = true;
            this.radioButton_X.CheckedChanged += new System.EventHandler(this.radioButton_X_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton11);
            this.groupBox4.Controls.Add(this.radioButton10);
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.groupBox7);
            this.groupBox4.Controls.Add(this.groupBox6);
            this.groupBox4.Controls.Add(this.radioButton5);
            this.groupBox4.Controls.Add(this.radioButton4);
            this.groupBox4.Controls.Add(this.radioButton3);
            this.groupBox4.Controls.Add(this.radioButton2);
            this.groupBox4.Controls.Add(this.radioButton1);
            this.groupBox4.Controls.Add(this.radioButton_mode1);
            this.groupBox4.Location = new System.Drawing.Point(200, 249);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(185, 373);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "回零模式";
            // 
            // radioButton11
            // 
            this.radioButton11.AutoSize = true;
            this.radioButton11.Location = new System.Drawing.Point(17, 296);
            this.radioButton11.Name = "radioButton11";
            this.radioButton11.Size = new System.Drawing.Size(149, 16);
            this.radioButton11.TabIndex = 11;
            this.radioButton11.Text = "原点负向回零+反爬+Z相";
            this.radioButton11.UseVisualStyleBackColor = true;
            this.radioButton11.CheckedChanged += new System.EventHandler(this.radioButton11_CheckedChanged);
            // 
            // radioButton10
            // 
            this.radioButton10.AutoSize = true;
            this.radioButton10.Location = new System.Drawing.Point(17, 271);
            this.radioButton10.Name = "radioButton10";
            this.radioButton10.Size = new System.Drawing.Size(149, 16);
            this.radioButton10.TabIndex = 10;
            this.radioButton10.Text = "原点正向回零+反爬+Z相";
            this.radioButton10.UseVisualStyleBackColor = true;
            this.radioButton10.CheckedChanged += new System.EventHandler(this.radioButton10_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(94, 122);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(68, 21);
            this.textBox1.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(11, 127);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 8;
            this.label8.Text = "总线回零模式";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.radioButton9);
            this.groupBox7.Controls.Add(this.radioButton8);
            this.groupBox7.Location = new System.Drawing.Point(10, 67);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox7.Size = new System.Drawing.Size(152, 46);
            this.groupBox7.TabIndex = 7;
            this.groupBox7.TabStop = false;
            // 
            // radioButton9
            // 
            this.radioButton9.AutoSize = true;
            this.radioButton9.Checked = true;
            this.radioButton9.Location = new System.Drawing.Point(81, 18);
            this.radioButton9.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton9.Name = "radioButton9";
            this.radioButton9.Size = new System.Drawing.Size(71, 16);
            this.radioButton9.TabIndex = 1;
            this.radioButton9.TabStop = true;
            this.radioButton9.Text = "限位反找";
            this.radioButton9.UseVisualStyleBackColor = true;
            // 
            // radioButton8
            // 
            this.radioButton8.AutoSize = true;
            this.radioButton8.Location = new System.Drawing.Point(6, 19);
            this.radioButton8.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton8.Name = "radioButton8";
            this.radioButton8.Size = new System.Drawing.Size(71, 16);
            this.radioButton8.TabIndex = 0;
            this.radioButton8.Text = "限位停止";
            this.radioButton8.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioButton7);
            this.groupBox6.Controls.Add(this.radioButton6);
            this.groupBox6.Location = new System.Drawing.Point(10, 19);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox6.Size = new System.Drawing.Size(165, 45);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            // 
            // radioButton7
            // 
            this.radioButton7.AutoSize = true;
            this.radioButton7.Checked = true;
            this.radioButton7.Location = new System.Drawing.Point(81, 18);
            this.radioButton7.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton7.Name = "radioButton7";
            this.radioButton7.Size = new System.Drawing.Size(83, 16);
            this.radioButton7.TabIndex = 1;
            this.radioButton7.TabStop = true;
            this.radioButton7.Text = "控制器回零";
            this.radioButton7.UseVisualStyleBackColor = true;
            this.radioButton7.CheckedChanged += new System.EventHandler(this.radioButton7_CheckedChanged);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Location = new System.Drawing.Point(6, 18);
            this.radioButton6.Margin = new System.Windows.Forms.Padding(2);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(71, 16);
            this.radioButton6.TabIndex = 0;
            this.radioButton6.Text = "总线回零";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(18, 344);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(95, 16);
            this.radioButton5.TabIndex = 5;
            this.radioButton5.Text = "原点负向回零";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(17, 320);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(95, 16);
            this.radioButton4.TabIndex = 4;
            this.radioButton4.Text = "原点正向回零";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(17, 245);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(125, 16);
            this.radioButton3.TabIndex = 3;
            this.radioButton3.Text = "原点负向回零+反爬";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(17, 217);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(125, 16);
            this.radioButton2.TabIndex = 2;
            this.radioButton2.Text = "原点正向回零+反爬";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(17, 187);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(89, 16);
            this.radioButton1.TabIndex = 1;
            this.radioButton1.Text = "Z相负向回零";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton_mode1
            // 
            this.radioButton_mode1.AutoSize = true;
            this.radioButton_mode1.Checked = true;
            this.radioButton_mode1.Location = new System.Drawing.Point(17, 157);
            this.radioButton_mode1.Name = "radioButton_mode1";
            this.radioButton_mode1.Size = new System.Drawing.Size(89, 16);
            this.radioButton_mode1.TabIndex = 0;
            this.radioButton_mode1.TabStop = true;
            this.radioButton_mode1.Text = "Z相正向回零";
            this.radioButton_mode1.UseVisualStyleBackColor = true;
            this.radioButton_mode1.CheckedChanged += new System.EventHandler(this.radioButton_mode1_CheckedChanged);
            // 
            // button_home
            // 
            this.button_home.Location = new System.Drawing.Point(410, 152);
            this.button_home.Name = "button_home";
            this.button_home.Size = new System.Drawing.Size(100, 30);
            this.button_home.TabIndex = 4;
            this.button_home.Text = "回  零";
            this.button_home.UseVisualStyleBackColor = true;
            this.button_home.Click += new System.EventHandler(this.button_home_Click);
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(410, 233);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(100, 30);
            this.button_stop.TabIndex = 5;
            this.button_stop.Text = "停  止";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // button_zero
            // 
            this.button_zero.Location = new System.Drawing.Point(410, 310);
            this.button_zero.Name = "button_zero";
            this.button_zero.Size = new System.Drawing.Size(100, 30);
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
            this.groupBox5.Location = new System.Drawing.Point(12, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(365, 51);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "连接控制器";
            // 
            // C_Close_Card
            // 
            this.C_Close_Card.Location = new System.Drawing.Point(285, 17);
            this.C_Close_Card.Name = "C_Close_Card";
            this.C_Close_Card.Size = new System.Drawing.Size(55, 21);
            this.C_Close_Card.TabIndex = 17;
            this.C_Close_Card.Text = "断开";
            this.C_Close_Card.UseVisualStyleBackColor = true;
            this.C_Close_Card.Click += new System.EventHandler(this.OnClose);
            // 
            // C_Open_Eth
            // 
            this.C_Open_Eth.Location = new System.Drawing.Point(208, 17);
            this.C_Open_Eth.Name = "C_Open_Eth";
            this.C_Open_Eth.Size = new System.Drawing.Size(55, 21);
            this.C_Open_Eth.TabIndex = 16;
            this.C_Open_Eth.Text = "连接";
            this.C_Open_Eth.UseVisualStyleBackColor = true;
            this.C_Open_Eth.Click += new System.EventHandler(this.OnOpen);
            // 
            // C_Ip_Address
            // 
            this.C_Ip_Address.FormattingEnabled = true;
            this.C_Ip_Address.Location = new System.Drawing.Point(83, 17);
            this.C_Ip_Address.Name = "C_Ip_Address";
            this.C_Ip_Address.Size = new System.Drawing.Size(105, 20);
            this.C_Ip_Address.TabIndex = 15;
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
            this.C_connect_type.Location = new System.Drawing.Point(13, 17);
            this.C_connect_type.Margin = new System.Windows.Forms.Padding(2);
            this.C_connect_type.Name = "C_connect_type";
            this.C_connect_type.Size = new System.Drawing.Size(59, 20);
            this.C_connect_type.TabIndex = 14;
            this.C_connect_type.DropDown += new System.EventHandler(this.OnSelect);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(390, 360);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(173, 60);
            this.label13.TabIndex = 10;
            this.label13.Text = "提示：\r\n控制器回零\r\n参考ZMotionPC函数库手册\r\n查看ZAux_Direct_Single_Datum\r\n函数说明";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(390, 360);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 60);
            this.label14.TabIndex = 11;
            this.label14.Text = "提示：\r\n总线回零\r\n参考驱动器手册\r\n查看6098\r\n数据字典的说明";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(581, 627);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.button_zero);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.button_home);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "回零测试";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_R;
        private System.Windows.Forms.Label label_Z;
        private System.Windows.Forms.Label label_Y;
        private System.Windows.Forms.Label label_X;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox TextBox_homeio;
        private System.Windows.Forms.TextBox TextBox_decel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TextBox_accel;
        private System.Windows.Forms.TextBox TextBox_creep;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBox_speed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox TextBox_lspeed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TextBox_units;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButton_R;
        private System.Windows.Forms.RadioButton radioButton_Z;
        private System.Windows.Forms.RadioButton radioButton_Y;
        private System.Windows.Forms.RadioButton radioButton_X;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton_mode1;
        private System.Windows.Forms.Button button_home;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button button_zero;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton radioButton9;
        private System.Windows.Forms.RadioButton radioButton8;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioButton7;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton radioButton11;
        private System.Windows.Forms.RadioButton radioButton10;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox TextBox_atype;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox TextBox_revio;
        private System.Windows.Forms.TextBox TextBox_fwdio;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox C_connect_type;
        private System.Windows.Forms.Button C_Close_Card;
        private System.Windows.Forms.Button C_Open_Eth;
        private System.Windows.Forms.ComboBox C_Ip_Address;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
    }
}

