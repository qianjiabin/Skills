namespace Delta
{
    partial class Delta标定
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Delta标定));
            this.uiGroupBox1 = new Sunny.UI.UIGroupBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Btn_Confirm = new Sunny.UI.UISymbolButton();
            this.uiGroupBox4 = new Sunny.UI.UIGroupBox();
            this.TX_HomeState = new Sunny.UI.UITextBox();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.TX_Mpos3 = new Sunny.UI.UITextBox();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.TX_Mpos2 = new Sunny.UI.UITextBox();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.TX_Mpos1 = new Sunny.UI.UITextBox();
            this.uiLabel16 = new Sunny.UI.UILabel();
            this.TX_Mpos0 = new Sunny.UI.UITextBox();
            this.uiGroupBox2 = new Sunny.UI.UIGroupBox();
            this.uiLabel9 = new Sunny.UI.UILabel();
            this.TX_Mode = new Sunny.UI.UITextBox();
            this.uiLabel10 = new Sunny.UI.UILabel();
            this.TX_Creep = new Sunny.UI.UITextBox();
            this.uiLabel11 = new Sunny.UI.UILabel();
            this.TX_Speed = new Sunny.UI.UITextBox();
            this.uiLabel13 = new Sunny.UI.UILabel();
            this.uiLabel12 = new Sunny.UI.UILabel();
            this.uiLabel7 = new Sunny.UI.UILabel();
            this.TX_Offset3 = new Sunny.UI.UITextBox();
            this.TX_Offset2 = new Sunny.UI.UITextBox();
            this.TX_Offset1 = new Sunny.UI.UITextBox();
            this.uiLabel8 = new Sunny.UI.UILabel();
            this.TX_Offset0 = new Sunny.UI.UITextBox();
            this.uiGroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.uiGroupBox4.SuspendLayout();
            this.uiGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiGroupBox1
            // 
            this.uiGroupBox1.Controls.Add(this.uiLabel2);
            this.uiGroupBox1.Controls.Add(this.uiLabel1);
            this.uiGroupBox1.Controls.Add(this.pictureBox1);
            this.uiGroupBox1.FillColor = System.Drawing.Color.White;
            this.uiGroupBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox1.Location = new System.Drawing.Point(13, 14);
            this.uiGroupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox1.Name = "uiGroupBox1";
            this.uiGroupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox1.Size = new System.Drawing.Size(601, 699);
            this.uiGroupBox1.Style = Sunny.UI.UIStyle.Custom;
            this.uiGroupBox1.TabIndex = 155;
            this.uiGroupBox1.Text = "1:原点说明";
            this.uiGroupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLabel2
            // 
            this.uiLabel2.BackColor = System.Drawing.Color.White;
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.Red;
            this.uiLabel2.Location = new System.Drawing.Point(13, 520);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(579, 172);
            this.uiLabel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel2.StyleCustomMode = true;
            this.uiLabel2.TabIndex = 4;
            this.uiLabel2.Text = "原点位置说明:\r\n1:各关节轴连杆 L1处于水平位置(通过定位销或打水平仪确定)\r\n,认为是关节零点位置。\r\n2:此时轴 0 和轴 1 连线方向为直角坐标系 X " +
    "方向\r\n,直角坐标系零点位于连杆L1水平面的中心。";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel1
            // 
            this.uiLabel1.BackColor = System.Drawing.Color.White;
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.Red;
            this.uiLabel1.Location = new System.Drawing.Point(16, 41);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(505, 29);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel1.StyleCustomMode = true;
            this.uiLabel1.TabIndex = 2;
            this.uiLabel1.Text = "回零偏移是指关节轴碰到原点传感器的位置对比图中实际delta原点的偏差角度\r\n";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(17, 83);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(504, 434);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // Btn_Confirm
            // 
            this.Btn_Confirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Btn_Confirm.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.Btn_Confirm.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.Btn_Confirm.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(203)))), ((int)(((byte)(83)))));
            this.Btn_Confirm.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.Btn_Confirm.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.Btn_Confirm.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Confirm.Location = new System.Drawing.Point(325, 294);
            this.Btn_Confirm.MinimumSize = new System.Drawing.Size(1, 1);
            this.Btn_Confirm.Name = "Btn_Confirm";
            this.Btn_Confirm.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.Btn_Confirm.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(203)))), ((int)(((byte)(83)))));
            this.Btn_Confirm.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.Btn_Confirm.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.Btn_Confirm.Size = new System.Drawing.Size(123, 41);
            this.Btn_Confirm.Style = Sunny.UI.UIStyle.Green;
            this.Btn_Confirm.StyleCustomMode = true;
            this.Btn_Confirm.TabIndex = 3;
            this.Btn_Confirm.Text = "开始回零";
            this.Btn_Confirm.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Btn_Confirm.Click += new System.EventHandler(this.Btn_Confirm_Click);
            // 
            // uiGroupBox4
            // 
            this.uiGroupBox4.Controls.Add(this.TX_HomeState);
            this.uiGroupBox4.Controls.Add(this.uiLabel6);
            this.uiGroupBox4.Controls.Add(this.uiLabel5);
            this.uiGroupBox4.Controls.Add(this.TX_Mpos3);
            this.uiGroupBox4.Controls.Add(this.uiLabel4);
            this.uiGroupBox4.Controls.Add(this.TX_Mpos2);
            this.uiGroupBox4.Controls.Add(this.uiLabel3);
            this.uiGroupBox4.Controls.Add(this.TX_Mpos1);
            this.uiGroupBox4.Controls.Add(this.uiLabel16);
            this.uiGroupBox4.Controls.Add(this.TX_Mpos0);
            this.uiGroupBox4.FillColor = System.Drawing.Color.White;
            this.uiGroupBox4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox4.Location = new System.Drawing.Point(618, 378);
            this.uiGroupBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox4.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox4.Name = "uiGroupBox4";
            this.uiGroupBox4.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox4.Size = new System.Drawing.Size(478, 335);
            this.uiGroupBox4.Style = Sunny.UI.UIStyle.Custom;
            this.uiGroupBox4.TabIndex = 156;
            this.uiGroupBox4.Text = "3:回零结果";
            this.uiGroupBox4.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TX_HomeState
            // 
            this.TX_HomeState.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TX_HomeState.FillDisableColor = System.Drawing.Color.White;
            this.TX_HomeState.FillReadOnlyColor = System.Drawing.Color.White;
            this.TX_HomeState.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TX_HomeState.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_HomeState.ForeReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_HomeState.Location = new System.Drawing.Point(311, 272);
            this.TX_HomeState.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TX_HomeState.MinimumSize = new System.Drawing.Size(1, 16);
            this.TX_HomeState.Name = "TX_HomeState";
            this.TX_HomeState.Padding = new System.Windows.Forms.Padding(5);
            this.TX_HomeState.ReadOnly = true;
            this.TX_HomeState.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_HomeState.RectReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_HomeState.ShowText = false;
            this.TX_HomeState.Size = new System.Drawing.Size(129, 41);
            this.TX_HomeState.Style = Sunny.UI.UIStyle.Custom;
            this.TX_HomeState.StyleCustomMode = true;
            this.TX_HomeState.TabIndex = 23;
            this.TX_HomeState.Text = "未回零";
            this.TX_HomeState.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TX_HomeState.Watermark = "";
            // 
            // uiLabel6
            // 
            this.uiLabel6.BackColor = System.Drawing.Color.White;
            this.uiLabel6.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel6.Location = new System.Drawing.Point(23, 272);
            this.uiLabel6.Name = "uiLabel6";
            this.uiLabel6.Size = new System.Drawing.Size(257, 39);
            this.uiLabel6.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel6.TabIndex = 23;
            this.uiLabel6.Text = "当前回零状态";
            this.uiLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel5
            // 
            this.uiLabel5.BackColor = System.Drawing.Color.White;
            this.uiLabel5.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel5.Location = new System.Drawing.Point(23, 216);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(257, 39);
            this.uiLabel5.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel5.TabIndex = 21;
            this.uiLabel5.Text = "旋转轴当前位置(角度值)";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TX_Mpos3
            // 
            this.TX_Mpos3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TX_Mpos3.FillDisableColor = System.Drawing.Color.White;
            this.TX_Mpos3.FillReadOnlyColor = System.Drawing.Color.White;
            this.TX_Mpos3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TX_Mpos3.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Mpos3.ForeReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Mpos3.Location = new System.Drawing.Point(311, 214);
            this.TX_Mpos3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TX_Mpos3.MinimumSize = new System.Drawing.Size(1, 16);
            this.TX_Mpos3.Name = "TX_Mpos3";
            this.TX_Mpos3.Padding = new System.Windows.Forms.Padding(5);
            this.TX_Mpos3.ReadOnly = true;
            this.TX_Mpos3.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Mpos3.RectReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Mpos3.ShowText = false;
            this.TX_Mpos3.Size = new System.Drawing.Size(129, 41);
            this.TX_Mpos3.Style = Sunny.UI.UIStyle.Custom;
            this.TX_Mpos3.StyleCustomMode = true;
            this.TX_Mpos3.TabIndex = 22;
            this.TX_Mpos3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TX_Mpos3.Watermark = "";
            // 
            // uiLabel4
            // 
            this.uiLabel4.BackColor = System.Drawing.Color.White;
            this.uiLabel4.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel4.Location = new System.Drawing.Point(23, 161);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(257, 39);
            this.uiLabel4.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel4.TabIndex = 19;
            this.uiLabel4.Text = "关节轴2当前位置(角度值)";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TX_Mpos2
            // 
            this.TX_Mpos2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TX_Mpos2.FillDisableColor = System.Drawing.Color.White;
            this.TX_Mpos2.FillReadOnlyColor = System.Drawing.Color.White;
            this.TX_Mpos2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TX_Mpos2.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Mpos2.ForeReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Mpos2.Location = new System.Drawing.Point(311, 159);
            this.TX_Mpos2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TX_Mpos2.MinimumSize = new System.Drawing.Size(1, 16);
            this.TX_Mpos2.Name = "TX_Mpos2";
            this.TX_Mpos2.Padding = new System.Windows.Forms.Padding(5);
            this.TX_Mpos2.ReadOnly = true;
            this.TX_Mpos2.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Mpos2.RectReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Mpos2.ShowText = false;
            this.TX_Mpos2.Size = new System.Drawing.Size(129, 41);
            this.TX_Mpos2.Style = Sunny.UI.UIStyle.Custom;
            this.TX_Mpos2.StyleCustomMode = true;
            this.TX_Mpos2.TabIndex = 20;
            this.TX_Mpos2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TX_Mpos2.Watermark = "";
            // 
            // uiLabel3
            // 
            this.uiLabel3.BackColor = System.Drawing.Color.White;
            this.uiLabel3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel3.Location = new System.Drawing.Point(23, 105);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(257, 39);
            this.uiLabel3.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel3.TabIndex = 17;
            this.uiLabel3.Text = "关节轴1当前位置(角度值)";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TX_Mpos1
            // 
            this.TX_Mpos1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TX_Mpos1.FillDisableColor = System.Drawing.Color.White;
            this.TX_Mpos1.FillReadOnlyColor = System.Drawing.Color.White;
            this.TX_Mpos1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TX_Mpos1.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Mpos1.ForeReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Mpos1.Location = new System.Drawing.Point(311, 103);
            this.TX_Mpos1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TX_Mpos1.MinimumSize = new System.Drawing.Size(1, 16);
            this.TX_Mpos1.Name = "TX_Mpos1";
            this.TX_Mpos1.Padding = new System.Windows.Forms.Padding(5);
            this.TX_Mpos1.ReadOnly = true;
            this.TX_Mpos1.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Mpos1.RectReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Mpos1.ShowText = false;
            this.TX_Mpos1.Size = new System.Drawing.Size(129, 41);
            this.TX_Mpos1.Style = Sunny.UI.UIStyle.Custom;
            this.TX_Mpos1.StyleCustomMode = true;
            this.TX_Mpos1.TabIndex = 18;
            this.TX_Mpos1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TX_Mpos1.Watermark = "";
            // 
            // uiLabel16
            // 
            this.uiLabel16.BackColor = System.Drawing.Color.White;
            this.uiLabel16.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel16.Location = new System.Drawing.Point(23, 46);
            this.uiLabel16.Name = "uiLabel16";
            this.uiLabel16.Size = new System.Drawing.Size(257, 39);
            this.uiLabel16.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel16.TabIndex = 13;
            this.uiLabel16.Text = "关节轴0当前位置(角度值)";
            this.uiLabel16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TX_Mpos0
            // 
            this.TX_Mpos0.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TX_Mpos0.FillDisableColor = System.Drawing.Color.White;
            this.TX_Mpos0.FillReadOnlyColor = System.Drawing.Color.White;
            this.TX_Mpos0.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TX_Mpos0.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Mpos0.ForeReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Mpos0.Location = new System.Drawing.Point(311, 46);
            this.TX_Mpos0.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TX_Mpos0.MinimumSize = new System.Drawing.Size(1, 16);
            this.TX_Mpos0.Name = "TX_Mpos0";
            this.TX_Mpos0.Padding = new System.Windows.Forms.Padding(5);
            this.TX_Mpos0.ReadOnly = true;
            this.TX_Mpos0.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Mpos0.RectReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Mpos0.ShowText = false;
            this.TX_Mpos0.Size = new System.Drawing.Size(129, 41);
            this.TX_Mpos0.Style = Sunny.UI.UIStyle.Custom;
            this.TX_Mpos0.StyleCustomMode = true;
            this.TX_Mpos0.TabIndex = 14;
            this.TX_Mpos0.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TX_Mpos0.Watermark = "";
            // 
            // uiGroupBox2
            // 
            this.uiGroupBox2.Controls.Add(this.uiLabel9);
            this.uiGroupBox2.Controls.Add(this.TX_Mode);
            this.uiGroupBox2.Controls.Add(this.uiLabel10);
            this.uiGroupBox2.Controls.Add(this.TX_Creep);
            this.uiGroupBox2.Controls.Add(this.uiLabel11);
            this.uiGroupBox2.Controls.Add(this.Btn_Confirm);
            this.uiGroupBox2.Controls.Add(this.TX_Speed);
            this.uiGroupBox2.Controls.Add(this.uiLabel13);
            this.uiGroupBox2.Controls.Add(this.uiLabel12);
            this.uiGroupBox2.Controls.Add(this.uiLabel7);
            this.uiGroupBox2.Controls.Add(this.TX_Offset3);
            this.uiGroupBox2.Controls.Add(this.TX_Offset2);
            this.uiGroupBox2.Controls.Add(this.TX_Offset1);
            this.uiGroupBox2.Controls.Add(this.uiLabel8);
            this.uiGroupBox2.Controls.Add(this.TX_Offset0);
            this.uiGroupBox2.FillColor = System.Drawing.Color.White;
            this.uiGroupBox2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiGroupBox2.Location = new System.Drawing.Point(618, 14);
            this.uiGroupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiGroupBox2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiGroupBox2.Name = "uiGroupBox2";
            this.uiGroupBox2.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.uiGroupBox2.Size = new System.Drawing.Size(478, 354);
            this.uiGroupBox2.Style = Sunny.UI.UIStyle.Custom;
            this.uiGroupBox2.TabIndex = 157;
            this.uiGroupBox2.Text = "2:回零参数";
            this.uiGroupBox2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLabel9
            // 
            this.uiLabel9.BackColor = System.Drawing.Color.White;
            this.uiLabel9.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel9.Location = new System.Drawing.Point(23, 296);
            this.uiLabel9.Name = "uiLabel9";
            this.uiLabel9.Size = new System.Drawing.Size(202, 35);
            this.uiLabel9.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel9.TabIndex = 31;
            this.uiLabel9.Text = "回零模式(向上回零)";
            this.uiLabel9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TX_Mode
            // 
            this.TX_Mode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TX_Mode.DoubleValue = 9D;
            this.TX_Mode.FillDisableColor = System.Drawing.Color.White;
            this.TX_Mode.FillReadOnlyColor = System.Drawing.Color.White;
            this.TX_Mode.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TX_Mode.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Mode.ForeReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Mode.IntValue = 9;
            this.TX_Mode.Location = new System.Drawing.Point(237, 293);
            this.TX_Mode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TX_Mode.MinimumSize = new System.Drawing.Size(1, 16);
            this.TX_Mode.Name = "TX_Mode";
            this.TX_Mode.Padding = new System.Windows.Forms.Padding(5);
            this.TX_Mode.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Mode.RectReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Mode.ShowText = false;
            this.TX_Mode.Size = new System.Drawing.Size(61, 41);
            this.TX_Mode.Style = Sunny.UI.UIStyle.Custom;
            this.TX_Mode.StyleCustomMode = true;
            this.TX_Mode.TabIndex = 32;
            this.TX_Mode.Text = "9";
            this.TX_Mode.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TX_Mode.Watermark = "";
            // 
            // uiLabel10
            // 
            this.uiLabel10.BackColor = System.Drawing.Color.White;
            this.uiLabel10.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel10.Location = new System.Drawing.Point(242, 235);
            this.uiLabel10.Name = "uiLabel10";
            this.uiLabel10.Size = new System.Drawing.Size(138, 39);
            this.uiLabel10.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel10.TabIndex = 29;
            this.uiLabel10.Text = "爬行速度(°/s)";
            this.uiLabel10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TX_Creep
            // 
            this.TX_Creep.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TX_Creep.DoubleValue = 1D;
            this.TX_Creep.FillDisableColor = System.Drawing.Color.White;
            this.TX_Creep.FillReadOnlyColor = System.Drawing.Color.White;
            this.TX_Creep.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TX_Creep.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Creep.ForeReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Creep.IntValue = 1;
            this.TX_Creep.Location = new System.Drawing.Point(387, 235);
            this.TX_Creep.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TX_Creep.MinimumSize = new System.Drawing.Size(1, 16);
            this.TX_Creep.Name = "TX_Creep";
            this.TX_Creep.Padding = new System.Windows.Forms.Padding(5);
            this.TX_Creep.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Creep.RectReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Creep.ShowText = false;
            this.TX_Creep.Size = new System.Drawing.Size(61, 41);
            this.TX_Creep.Style = Sunny.UI.UIStyle.Custom;
            this.TX_Creep.StyleCustomMode = true;
            this.TX_Creep.TabIndex = 30;
            this.TX_Creep.Text = "1";
            this.TX_Creep.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TX_Creep.Watermark = "";
            // 
            // uiLabel11
            // 
            this.uiLabel11.BackColor = System.Drawing.Color.White;
            this.uiLabel11.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel11.Location = new System.Drawing.Point(22, 235);
            this.uiLabel11.Name = "uiLabel11";
            this.uiLabel11.Size = new System.Drawing.Size(138, 39);
            this.uiLabel11.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel11.TabIndex = 27;
            this.uiLabel11.Text = "回零速度(°/s)";
            this.uiLabel11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TX_Speed
            // 
            this.TX_Speed.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TX_Speed.DoubleValue = 10D;
            this.TX_Speed.FillDisableColor = System.Drawing.Color.White;
            this.TX_Speed.FillReadOnlyColor = System.Drawing.Color.White;
            this.TX_Speed.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TX_Speed.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Speed.ForeReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Speed.IntValue = 10;
            this.TX_Speed.Location = new System.Drawing.Point(164, 235);
            this.TX_Speed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TX_Speed.MinimumSize = new System.Drawing.Size(1, 16);
            this.TX_Speed.Name = "TX_Speed";
            this.TX_Speed.Padding = new System.Windows.Forms.Padding(5);
            this.TX_Speed.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Speed.RectReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Speed.ShowText = false;
            this.TX_Speed.Size = new System.Drawing.Size(61, 41);
            this.TX_Speed.Style = Sunny.UI.UIStyle.Custom;
            this.TX_Speed.StyleCustomMode = true;
            this.TX_Speed.TabIndex = 28;
            this.TX_Speed.Text = "10";
            this.TX_Speed.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TX_Speed.Watermark = "";
            // 
            // uiLabel13
            // 
            this.uiLabel13.BackColor = System.Drawing.Color.White;
            this.uiLabel13.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel13.Location = new System.Drawing.Point(24, 179);
            this.uiLabel13.Name = "uiLabel13";
            this.uiLabel13.Size = new System.Drawing.Size(251, 36);
            this.uiLabel13.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel13.TabIndex = 26;
            this.uiLabel13.Text = "旋转轴3回零偏移(角度值)";
            this.uiLabel13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel12
            // 
            this.uiLabel12.BackColor = System.Drawing.Color.White;
            this.uiLabel12.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel12.Location = new System.Drawing.Point(24, 133);
            this.uiLabel12.Name = "uiLabel12";
            this.uiLabel12.Size = new System.Drawing.Size(251, 36);
            this.uiLabel12.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel12.TabIndex = 25;
            this.uiLabel12.Text = "关节轴2回零偏移(角度值)";
            this.uiLabel12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel7
            // 
            this.uiLabel7.BackColor = System.Drawing.Color.White;
            this.uiLabel7.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel7.Location = new System.Drawing.Point(24, 87);
            this.uiLabel7.Name = "uiLabel7";
            this.uiLabel7.Size = new System.Drawing.Size(251, 36);
            this.uiLabel7.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel7.TabIndex = 24;
            this.uiLabel7.Text = "关节轴1回零偏移(角度值)";
            this.uiLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TX_Offset3
            // 
            this.TX_Offset3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TX_Offset3.FillDisableColor = System.Drawing.Color.White;
            this.TX_Offset3.FillReadOnlyColor = System.Drawing.Color.White;
            this.TX_Offset3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TX_Offset3.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Offset3.ForeReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Offset3.Location = new System.Drawing.Point(282, 179);
            this.TX_Offset3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TX_Offset3.MinimumSize = new System.Drawing.Size(1, 16);
            this.TX_Offset3.Name = "TX_Offset3";
            this.TX_Offset3.Padding = new System.Windows.Forms.Padding(5);
            this.TX_Offset3.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Offset3.RectReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Offset3.ShowText = false;
            this.TX_Offset3.Size = new System.Drawing.Size(165, 38);
            this.TX_Offset3.Style = Sunny.UI.UIStyle.Custom;
            this.TX_Offset3.StyleCustomMode = true;
            this.TX_Offset3.TabIndex = 23;
            this.TX_Offset3.Text = "0";
            this.TX_Offset3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TX_Offset3.Watermark = "";
            // 
            // TX_Offset2
            // 
            this.TX_Offset2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TX_Offset2.FillDisableColor = System.Drawing.Color.White;
            this.TX_Offset2.FillReadOnlyColor = System.Drawing.Color.White;
            this.TX_Offset2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TX_Offset2.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Offset2.ForeReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Offset2.Location = new System.Drawing.Point(282, 133);
            this.TX_Offset2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TX_Offset2.MinimumSize = new System.Drawing.Size(1, 16);
            this.TX_Offset2.Name = "TX_Offset2";
            this.TX_Offset2.Padding = new System.Windows.Forms.Padding(5);
            this.TX_Offset2.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Offset2.RectReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Offset2.ShowText = false;
            this.TX_Offset2.Size = new System.Drawing.Size(165, 38);
            this.TX_Offset2.Style = Sunny.UI.UIStyle.Custom;
            this.TX_Offset2.StyleCustomMode = true;
            this.TX_Offset2.TabIndex = 23;
            this.TX_Offset2.Text = "0";
            this.TX_Offset2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TX_Offset2.Watermark = "";
            // 
            // TX_Offset1
            // 
            this.TX_Offset1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TX_Offset1.FillDisableColor = System.Drawing.Color.White;
            this.TX_Offset1.FillReadOnlyColor = System.Drawing.Color.White;
            this.TX_Offset1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TX_Offset1.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Offset1.ForeReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Offset1.Location = new System.Drawing.Point(282, 88);
            this.TX_Offset1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TX_Offset1.MinimumSize = new System.Drawing.Size(1, 16);
            this.TX_Offset1.Name = "TX_Offset1";
            this.TX_Offset1.Padding = new System.Windows.Forms.Padding(5);
            this.TX_Offset1.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Offset1.RectReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Offset1.ShowText = false;
            this.TX_Offset1.Size = new System.Drawing.Size(165, 38);
            this.TX_Offset1.Style = Sunny.UI.UIStyle.Custom;
            this.TX_Offset1.StyleCustomMode = true;
            this.TX_Offset1.TabIndex = 23;
            this.TX_Offset1.Text = "0";
            this.TX_Offset1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TX_Offset1.Watermark = "";
            // 
            // uiLabel8
            // 
            this.uiLabel8.BackColor = System.Drawing.Color.White;
            this.uiLabel8.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel8.Location = new System.Drawing.Point(24, 41);
            this.uiLabel8.Name = "uiLabel8";
            this.uiLabel8.Size = new System.Drawing.Size(251, 36);
            this.uiLabel8.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel8.TabIndex = 21;
            this.uiLabel8.Text = "关节轴0回零偏移(角度值)";
            this.uiLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TX_Offset0
            // 
            this.TX_Offset0.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TX_Offset0.FillDisableColor = System.Drawing.Color.White;
            this.TX_Offset0.FillReadOnlyColor = System.Drawing.Color.White;
            this.TX_Offset0.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TX_Offset0.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Offset0.ForeReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TX_Offset0.Location = new System.Drawing.Point(282, 41);
            this.TX_Offset0.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TX_Offset0.MinimumSize = new System.Drawing.Size(1, 16);
            this.TX_Offset0.Name = "TX_Offset0";
            this.TX_Offset0.Padding = new System.Windows.Forms.Padding(5);
            this.TX_Offset0.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Offset0.RectReadOnlyColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
            this.TX_Offset0.ShowText = false;
            this.TX_Offset0.Size = new System.Drawing.Size(165, 38);
            this.TX_Offset0.Style = Sunny.UI.UIStyle.Custom;
            this.TX_Offset0.StyleCustomMode = true;
            this.TX_Offset0.TabIndex = 22;
            this.TX_Offset0.Text = "0";
            this.TX_Offset0.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TX_Offset0.Watermark = "";
            // 
            // Delta标定
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1107, 755);
            this.Controls.Add(this.uiGroupBox2);
            this.Controls.Add(this.uiGroupBox4);
            this.Controls.Add(this.uiGroupBox1);
            this.Name = "Delta标定";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "原点标定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Delta标定_FormClosing);
            this.uiGroupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.uiGroupBox4.ResumeLayout(false);
            this.uiGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Sunny.UI.UIGroupBox uiGroupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UISymbolButton Btn_Confirm;
        private Sunny.UI.UIGroupBox uiGroupBox4;
        private Sunny.UI.UILabel uiLabel16;
        private Sunny.UI.UITextBox TX_Mpos0;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UITextBox TX_Mpos3;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UITextBox TX_Mpos2;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UITextBox TX_Mpos1;
        private Sunny.UI.UITextBox TX_HomeState;
        private Sunny.UI.UILabel uiLabel6;
        private Sunny.UI.UIGroupBox uiGroupBox2;
        private Sunny.UI.UILabel uiLabel8;
        private Sunny.UI.UITextBox TX_Offset0;
        private Sunny.UI.UITextBox TX_Offset3;
        private Sunny.UI.UITextBox TX_Offset2;
        private Sunny.UI.UITextBox TX_Offset1;
        private Sunny.UI.UILabel uiLabel13;
        private Sunny.UI.UILabel uiLabel12;
        private Sunny.UI.UILabel uiLabel7;
        private Sunny.UI.UILabel uiLabel9;
        private Sunny.UI.UITextBox TX_Mode;
        private Sunny.UI.UILabel uiLabel10;
        private Sunny.UI.UITextBox TX_Creep;
        private Sunny.UI.UILabel uiLabel11;
        private Sunny.UI.UITextBox TX_Speed;
    }
}