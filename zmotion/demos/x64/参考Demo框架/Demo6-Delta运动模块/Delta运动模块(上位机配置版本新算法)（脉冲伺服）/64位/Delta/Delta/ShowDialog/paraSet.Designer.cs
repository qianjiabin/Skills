namespace Delta
{
    partial class paraSet
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
            this.RB_L = new Sunny.UI.UIRadioButton();
            this.RB_J = new Sunny.UI.UIRadioButton();
            this.btnCancel = new Sunny.UI.UISymbolButton();
            this.btnOK = new Sunny.UI.UISymbolButton();
            this.TX_Accel = new Sunny.UI.UITextBox();
            this.TX_Speed = new Sunny.UI.UITextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // RB_L
            // 
            this.RB_L.Checked = true;
            this.RB_L.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RB_L.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RB_L.ImageSize = 20;
            this.RB_L.Location = new System.Drawing.Point(68, 35);
            this.RB_L.MinimumSize = new System.Drawing.Size(1, 1);
            this.RB_L.Name = "RB_L";
            this.RB_L.Size = new System.Drawing.Size(163, 63);
            this.RB_L.Style = Sunny.UI.UIStyle.Custom;
            this.RB_L.TabIndex = 0;
            this.RB_L.Text = "直角坐标运动";
            this.RB_L.CheckedChanged += new System.EventHandler(this.RB_L_CheckedChanged);
            // 
            // RB_J
            // 
            this.RB_J.Cursor = System.Windows.Forms.Cursors.Hand;
            this.RB_J.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.RB_J.ImageSize = 20;
            this.RB_J.Location = new System.Drawing.Point(254, 35);
            this.RB_J.MinimumSize = new System.Drawing.Size(1, 1);
            this.RB_J.Name = "RB_J";
            this.RB_J.Size = new System.Drawing.Size(158, 63);
            this.RB_J.Style = Sunny.UI.UIStyle.Custom;
            this.RB_J.TabIndex = 1;
            this.RB_J.Text = "关节坐标运动";
            this.RB_J.CheckedChanged += new System.EventHandler(this.RB_J_CheckedChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnCancel.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(269, 219);
            this.btnCancel.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.btnCancel.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(115)))), ((int)(((byte)(115)))));
            this.btnCancel.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(184)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnCancel.Size = new System.Drawing.Size(126, 35);
            this.btnCancel.Style = Sunny.UI.UIStyle.Red;
            this.btnCancel.StyleCustomMode = true;
            this.btnCancel.Symbol = 61453;
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消";
            this.btnCancel.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.btnOK.FillColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.btnOK.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(203)))), ((int)(((byte)(83)))));
            this.btnOK.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.btnOK.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Location = new System.Drawing.Point(58, 219);
            this.btnOK.MinimumSize = new System.Drawing.Size(1, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(190)))), ((int)(((byte)(40)))));
            this.btnOK.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(203)))), ((int)(((byte)(83)))));
            this.btnOK.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.btnOK.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(152)))), ((int)(((byte)(32)))));
            this.btnOK.Size = new System.Drawing.Size(125, 35);
            this.btnOK.Style = Sunny.UI.UIStyle.Green;
            this.btnOK.StyleCustomMode = true;
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "确定";
            this.btnOK.TipsFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // TX_Accel
            // 
            this.TX_Accel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TX_Accel.DoubleValue = 200D;
            this.TX_Accel.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TX_Accel.IntValue = 200;
            this.TX_Accel.Location = new System.Drawing.Point(335, 133);
            this.TX_Accel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TX_Accel.MinimumSize = new System.Drawing.Size(1, 16);
            this.TX_Accel.Name = "TX_Accel";
            this.TX_Accel.Padding = new System.Windows.Forms.Padding(5);
            this.TX_Accel.ShowText = false;
            this.TX_Accel.Size = new System.Drawing.Size(61, 39);
            this.TX_Accel.Style = Sunny.UI.UIStyle.Custom;
            this.TX_Accel.TabIndex = 15;
            this.TX_Accel.Text = "200";
            this.TX_Accel.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TX_Accel.Watermark = "";
            // 
            // TX_Speed
            // 
            this.TX_Speed.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.TX_Speed.DoubleValue = 10D;
            this.TX_Speed.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TX_Speed.IntValue = 10;
            this.TX_Speed.Location = new System.Drawing.Point(103, 131);
            this.TX_Speed.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TX_Speed.MinimumSize = new System.Drawing.Size(1, 16);
            this.TX_Speed.Name = "TX_Speed";
            this.TX_Speed.Padding = new System.Windows.Forms.Padding(5);
            this.TX_Speed.ShowText = false;
            this.TX_Speed.Size = new System.Drawing.Size(44, 39);
            this.TX_Speed.Style = Sunny.UI.UIStyle.Custom;
            this.TX_Speed.TabIndex = 13;
            this.TX_Speed.Text = "10";
            this.TX_Speed.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.TX_Speed.Watermark = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(45, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 28);
            this.label1.TabIndex = 16;
            this.label1.Text = "速度";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(197, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 28);
            this.label2.TabIndex = 17;
            this.label2.Text = "加减速时间(ms)";
            // 
            // paraSet
            // 
            this.AllowShowTitle = false;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(438, 288);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TX_Accel);
            this.Controls.Add(this.TX_Speed);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.RB_J);
            this.Controls.Add(this.RB_L);
            this.Name = "paraSet";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.ShowTitle = false;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "paraSet";
            this.ZoomScaleRect = new System.Drawing.Rectangle(22, 22, 800, 450);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Sunny.UI.UIRadioButton RB_L;
        private Sunny.UI.UIRadioButton RB_J;
        private Sunny.UI.UISymbolButton btnCancel;
        private Sunny.UI.UISymbolButton btnOK;
        private Sunny.UI.UITextBox TX_Accel;
        private Sunny.UI.UITextBox TX_Speed;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}