namespace 任意轴插补
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
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.Button2 = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.mspeed = new System.Windows.Forms.TextBox();
            this.vp_speed = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.cur_idle = new System.Windows.Forms.TextBox();
            this.cur_mpos = new System.Windows.Forms.TextBox();
            this.cur_dpos = new System.Windows.Forms.TextBox();
            this.axis_move = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.mspeed2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.vp_speed2 = new System.Windows.Forms.TextBox();
            this.cur_idle2 = new System.Windows.Forms.TextBox();
            this.cur_mpos2 = new System.Windows.Forms.TextBox();
            this.cur_dpos2 = new System.Windows.Forms.TextBox();
            this.move_axis = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.move_axis2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.axis_dis = new System.Windows.Forms.TextBox();
            this.axis_dis2 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.star_run = new System.Windows.Forms.Button();
            this.run_timer = new System.Windows.Forms.Timer(this.components);
            this.stop_run = new System.Windows.Forms.Button();
            this.units = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.speed = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.accel = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.decel = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.decel2 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.accel2 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.speed2 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.units2 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.atc = new System.Windows.Forms.Button();
            this.atc2 = new System.Windows.Forms.Button();
            this.groupBox11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // comboBox2
            // 
            this.comboBox2.Font = new System.Drawing.Font("宋体", 9F);
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "127.0.0.1"});
            this.comboBox2.Location = new System.Drawing.Point(69, 23);
            this.comboBox2.MaxDropDownItems = 3;
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 76;
            this.comboBox2.Text = "127.0.0.1";
            this.comboBox2.DropDown += new System.EventHandler(this.comboBox2_DropDown);
            // 
            // Button2
            // 
            this.Button2.Font = new System.Drawing.Font("宋体", 9F);
            this.Button2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button2.Location = new System.Drawing.Point(379, 12);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(89, 37);
            this.Button2.TabIndex = 75;
            this.Button2.Text = "断  开";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Button1
            // 
            this.Button1.Font = new System.Drawing.Font("宋体", 9F);
            this.Button1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Button1.Location = new System.Drawing.Point(242, 12);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(95, 37);
            this.Button1.TabIndex = 74;
            this.Button1.Text = "连  接";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Gray;
            this.Label1.Font = new System.Drawing.Font("宋体", 9F);
            this.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.Label1.Location = new System.Drawing.Point(16, 23);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(47, 16);
            this.Label1.TabIndex = 73;
            this.Label1.Text = "I P：";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.label18);
            this.groupBox11.Controls.Add(this.label17);
            this.groupBox11.Controls.Add(this.mspeed);
            this.groupBox11.Controls.Add(this.vp_speed);
            this.groupBox11.Controls.Add(this.label45);
            this.groupBox11.Controls.Add(this.label44);
            this.groupBox11.Controls.Add(this.label43);
            this.groupBox11.Controls.Add(this.cur_idle);
            this.groupBox11.Controls.Add(this.cur_mpos);
            this.groupBox11.Controls.Add(this.cur_dpos);
            this.groupBox11.Location = new System.Drawing.Point(12, 281);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(372, 167);
            this.groupBox11.TabIndex = 77;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "主轴信息：";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label18.Location = new System.Drawing.Point(61, 147);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(101, 12);
            this.label18.TabIndex = 108;
            this.label18.Text = "MSPEED(反馈速度)";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label17.Location = new System.Drawing.Point(45, 120);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(113, 12);
            this.label17.TabIndex = 29;
            this.label17.Text = "VP_speed(指令速度)";
            // 
            // mspeed
            // 
            this.mspeed.Location = new System.Drawing.Point(168, 142);
            this.mspeed.Name = "mspeed";
            this.mspeed.Size = new System.Drawing.Size(134, 21);
            this.mspeed.TabIndex = 107;
            // 
            // vp_speed
            // 
            this.vp_speed.Location = new System.Drawing.Point(168, 115);
            this.vp_speed.Name = "vp_speed";
            this.vp_speed.Size = new System.Drawing.Size(134, 21);
            this.vp_speed.TabIndex = 28;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label45.Location = new System.Drawing.Point(61, 90);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(77, 12);
            this.label45.TabIndex = 27;
            this.label45.Text = "IDLE(轴状态)";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label44.Location = new System.Drawing.Point(37, 61);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(125, 12);
            this.label44.TabIndex = 26;
            this.label44.Text = "MPOS(编码器反馈位置)";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label43.Location = new System.Drawing.Point(61, 27);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(101, 12);
            this.label43.TabIndex = 25;
            this.label43.Text = "DPOS(轴指令位置)";
            // 
            // cur_idle
            // 
            this.cur_idle.Location = new System.Drawing.Point(168, 85);
            this.cur_idle.Name = "cur_idle";
            this.cur_idle.Size = new System.Drawing.Size(134, 21);
            this.cur_idle.TabIndex = 24;
            // 
            // cur_mpos
            // 
            this.cur_mpos.Location = new System.Drawing.Point(168, 55);
            this.cur_mpos.Name = "cur_mpos";
            this.cur_mpos.Size = new System.Drawing.Size(134, 21);
            this.cur_mpos.TabIndex = 23;
            // 
            // cur_dpos
            // 
            this.cur_dpos.Location = new System.Drawing.Point(168, 24);
            this.cur_dpos.Name = "cur_dpos";
            this.cur_dpos.Size = new System.Drawing.Size(134, 21);
            this.cur_dpos.TabIndex = 22;
            // 
            // axis_move
            // 
            this.axis_move.Interval = 10;
            this.axis_move.Tick += new System.EventHandler(this.axis_move_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.mspeed2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.vp_speed2);
            this.groupBox1.Controls.Add(this.cur_idle2);
            this.groupBox1.Controls.Add(this.cur_mpos2);
            this.groupBox1.Controls.Add(this.cur_dpos2);
            this.groupBox1.Location = new System.Drawing.Point(416, 281);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 167);
            this.groupBox1.TabIndex = 78;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "从轴信息：";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label19.Location = new System.Drawing.Point(61, 143);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(101, 12);
            this.label19.TabIndex = 112;
            this.label19.Text = "MSPEED(反馈速度)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label2.Location = new System.Drawing.Point(61, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 27;
            this.label2.Text = "IDLE(轴状态)";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label20.Location = new System.Drawing.Point(45, 113);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(113, 12);
            this.label20.TabIndex = 110;
            this.label20.Text = "VP_speed(指令速度)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label3.Location = new System.Drawing.Point(37, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 12);
            this.label3.TabIndex = 26;
            this.label3.Text = "MPOS(编码器反馈位置)";
            // 
            // mspeed2
            // 
            this.mspeed2.Location = new System.Drawing.Point(168, 138);
            this.mspeed2.Name = "mspeed2";
            this.mspeed2.Size = new System.Drawing.Size(134, 21);
            this.mspeed2.TabIndex = 111;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(61, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "DPOS(轴指令位置)";
            // 
            // vp_speed2
            // 
            this.vp_speed2.Location = new System.Drawing.Point(168, 108);
            this.vp_speed2.Name = "vp_speed2";
            this.vp_speed2.Size = new System.Drawing.Size(134, 21);
            this.vp_speed2.TabIndex = 109;
            // 
            // cur_idle2
            // 
            this.cur_idle2.Location = new System.Drawing.Point(168, 80);
            this.cur_idle2.Name = "cur_idle2";
            this.cur_idle2.Size = new System.Drawing.Size(134, 21);
            this.cur_idle2.TabIndex = 24;
            // 
            // cur_mpos2
            // 
            this.cur_mpos2.Location = new System.Drawing.Point(168, 52);
            this.cur_mpos2.Name = "cur_mpos2";
            this.cur_mpos2.Size = new System.Drawing.Size(134, 21);
            this.cur_mpos2.TabIndex = 23;
            // 
            // cur_dpos2
            // 
            this.cur_dpos2.Location = new System.Drawing.Point(168, 24);
            this.cur_dpos2.Name = "cur_dpos2";
            this.cur_dpos2.Size = new System.Drawing.Size(134, 21);
            this.cur_dpos2.TabIndex = 22;
            // 
            // move_axis
            // 
            this.move_axis.FormattingEnabled = true;
            this.move_axis.Location = new System.Drawing.Point(71, 83);
            this.move_axis.Name = "move_axis";
            this.move_axis.Size = new System.Drawing.Size(120, 20);
            this.move_axis.TabIndex = 80;
            this.move_axis.Text = "0";
            this.move_axis.SelectedIndexChanged += new System.EventHandler(this.move_axis_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(12, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 79;
            this.label9.Text = "主轴选择";
            // 
            // move_axis2
            // 
            this.move_axis2.FormattingEnabled = true;
            this.move_axis2.Location = new System.Drawing.Point(355, 83);
            this.move_axis2.Name = "move_axis2";
            this.move_axis2.Size = new System.Drawing.Size(120, 20);
            this.move_axis2.TabIndex = 82;
            this.move_axis2.Text = "1";
            this.move_axis2.SelectedIndexChanged += new System.EventHandler(this.move_axis2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(296, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 81;
            this.label5.Text = "从轴选择";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(14, 245);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 12);
            this.label7.TabIndex = 83;
            this.label7.Text = "主轴连续插补距离";
            // 
            // axis_dis
            // 
            this.axis_dis.Location = new System.Drawing.Point(121, 242);
            this.axis_dis.Name = "axis_dis";
            this.axis_dis.Size = new System.Drawing.Size(110, 21);
            this.axis_dis.TabIndex = 84;
            this.axis_dis.Text = "100";
            // 
            // axis_dis2
            // 
            this.axis_dis2.Location = new System.Drawing.Point(403, 241);
            this.axis_dis2.Name = "axis_dis2";
            this.axis_dis2.Size = new System.Drawing.Size(110, 21);
            this.axis_dis2.TabIndex = 86;
            this.axis_dis2.Text = "100";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(296, 244);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 85;
            this.label6.Text = "从轴连续插补距离";
            // 
            // star_run
            // 
            this.star_run.Location = new System.Drawing.Point(665, 60);
            this.star_run.Name = "star_run";
            this.star_run.Size = new System.Drawing.Size(114, 59);
            this.star_run.TabIndex = 87;
            this.star_run.Text = "启动";
            this.star_run.UseVisualStyleBackColor = true;
            this.star_run.Click += new System.EventHandler(this.star_run_Click);
            // 
            // run_timer
            // 
            this.run_timer.Tick += new System.EventHandler(this.run_timer_Tick);
            // 
            // stop_run
            // 
            this.stop_run.Location = new System.Drawing.Point(665, 161);
            this.stop_run.Name = "stop_run";
            this.stop_run.Size = new System.Drawing.Size(114, 59);
            this.stop_run.TabIndex = 88;
            this.stop_run.Text = "停止";
            this.stop_run.UseVisualStyleBackColor = true;
            this.stop_run.Click += new System.EventHandler(this.stop_run_Click);
            // 
            // units
            // 
            this.units.Location = new System.Drawing.Point(71, 121);
            this.units.Name = "units";
            this.units.Size = new System.Drawing.Size(110, 21);
            this.units.TabIndex = 90;
            this.units.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(14, 124);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 89;
            this.label8.Text = "脉冲当量";
            // 
            // speed
            // 
            this.speed.Location = new System.Drawing.Point(71, 148);
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(110, 21);
            this.speed.TabIndex = 92;
            this.speed.Text = "1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(14, 151);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 91;
            this.label10.Text = "运动速度";
            // 
            // accel
            // 
            this.accel.Location = new System.Drawing.Point(71, 175);
            this.accel.Name = "accel";
            this.accel.Size = new System.Drawing.Size(110, 21);
            this.accel.TabIndex = 94;
            this.accel.Text = "1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(14, 178);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 93;
            this.label11.Text = "加速度";
            // 
            // decel
            // 
            this.decel.Location = new System.Drawing.Point(71, 202);
            this.decel.Name = "decel";
            this.decel.Size = new System.Drawing.Size(110, 21);
            this.decel.TabIndex = 96;
            this.decel.Text = "1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(14, 205);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 12);
            this.label12.TabIndex = 95;
            this.label12.Text = "减速度";
            // 
            // decel2
            // 
            this.decel2.Location = new System.Drawing.Point(355, 196);
            this.decel2.Name = "decel2";
            this.decel2.Size = new System.Drawing.Size(110, 21);
            this.decel2.TabIndex = 104;
            this.decel2.Text = "1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(298, 199);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 12);
            this.label13.TabIndex = 103;
            this.label13.Text = "减速度";
            // 
            // accel2
            // 
            this.accel2.Location = new System.Drawing.Point(355, 169);
            this.accel2.Name = "accel2";
            this.accel2.Size = new System.Drawing.Size(110, 21);
            this.accel2.TabIndex = 102;
            this.accel2.Text = "1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label14.Location = new System.Drawing.Point(298, 172);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(41, 12);
            this.label14.TabIndex = 101;
            this.label14.Text = "加速度";
            // 
            // speed2
            // 
            this.speed2.Location = new System.Drawing.Point(355, 142);
            this.speed2.Name = "speed2";
            this.speed2.Size = new System.Drawing.Size(110, 21);
            this.speed2.TabIndex = 100;
            this.speed2.Text = "1";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label15.Location = new System.Drawing.Point(298, 145);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(53, 12);
            this.label15.TabIndex = 99;
            this.label15.Text = "运动速度";
            // 
            // units2
            // 
            this.units2.Location = new System.Drawing.Point(355, 115);
            this.units2.Name = "units2";
            this.units2.Size = new System.Drawing.Size(110, 21);
            this.units2.TabIndex = 98;
            this.units2.Text = "1";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label16.Location = new System.Drawing.Point(298, 118);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(53, 12);
            this.label16.TabIndex = 97;
            this.label16.Text = "脉冲当量";
            // 
            // atc
            // 
            this.atc.Location = new System.Drawing.Point(198, 180);
            this.atc.Name = "atc";
            this.atc.Size = new System.Drawing.Size(66, 42);
            this.atc.TabIndex = 105;
            this.atc.Text = "生效参数";
            this.atc.UseVisualStyleBackColor = true;
            this.atc.Click += new System.EventHandler(this.atc_Click);
            // 
            // atc2
            // 
            this.atc2.Location = new System.Drawing.Point(488, 175);
            this.atc2.Name = "atc2";
            this.atc2.Size = new System.Drawing.Size(66, 42);
            this.atc2.TabIndex = 106;
            this.atc2.Text = "生效参数";
            this.atc2.UseVisualStyleBackColor = true;
            this.atc2.Click += new System.EventHandler(this.atc2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.atc2);
            this.Controls.Add(this.atc);
            this.Controls.Add(this.decel2);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.accel2);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.speed2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.units2);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.decel);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.accel);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.speed);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.units);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.stop_run);
            this.Controls.Add(this.star_run);
            this.Controls.Add(this.axis_dis2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.axis_dis);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.move_axis2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.move_axis);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox11);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Button1);
            this.Controls.Add(this.Label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox11.ResumeLayout(false);
            this.groupBox11.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.ComboBox comboBox2;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Button Button1;
        internal System.Windows.Forms.Label Label1;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.TextBox cur_idle;
        private System.Windows.Forms.TextBox cur_mpos;
        private System.Windows.Forms.TextBox cur_dpos;
        private System.Windows.Forms.Timer axis_move;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox cur_idle2;
        private System.Windows.Forms.TextBox cur_mpos2;
        private System.Windows.Forms.TextBox cur_dpos2;
        private System.Windows.Forms.ComboBox move_axis;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox move_axis2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox axis_dis;
        private System.Windows.Forms.TextBox axis_dis2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button star_run;
        private System.Windows.Forms.Timer run_timer;
        private System.Windows.Forms.Button stop_run;
        private System.Windows.Forms.TextBox units;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox speed;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox accel;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox decel;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox decel2;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox accel2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox speed2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox units2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button atc;
        private System.Windows.Forms.Button atc2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox mspeed;
        private System.Windows.Forms.TextBox vp_speed;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox mspeed2;
        private System.Windows.Forms.TextBox vp_speed2;
    }
}

