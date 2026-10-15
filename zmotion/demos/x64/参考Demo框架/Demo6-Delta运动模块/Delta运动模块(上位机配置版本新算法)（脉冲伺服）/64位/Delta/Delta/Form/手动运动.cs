using cszmcaux;
using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace Delta
{

    public partial class 手动运动 : UIPage
    {

        private int handSpeedPerc = 0;  //手动速度百分比

        MainForm mainForm;

        private int enable = 1;

        float[] posNow = new float[4];

        public 手动运动(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

            handSpeedPerc = Convert.ToInt32(TX_HandSpeedPerc.Text);

            this.InitTimers();

            BindTimers();

        }

        #region 【计时器】  

        //任务触发事件
        public System.Timers.Timer Mpos_Tri;

        private string InitTimers()
        {
            try
            {
                this.Mpos_Tri = new System.Timers.Timer();

                return null;
            }
            catch (Exception ex)
            {
                return "实例化计时器失败:" + ex.Message;
            }
        }

        #endregion

        private string BindTimers()
        {
            try
            {

                this.Mpos_Tri.Interval = 50;
                this.Mpos_Tri.Elapsed += Read_T1_Tri_Elapsed;
                this.Mpos_Tri.AutoReset = false;
                this.Mpos_Tri.Enabled = true;

                return null;

            }
            catch (Exception ex)
            {
                return "绑定计时器失败:" + ex.Message;
            }
        }

        private string UnBindTimers()
        {
            try
            {

                this.Mpos_Tri.Elapsed -= Read_T1_Tri_Elapsed;
                this.Mpos_Tri.Enabled = false;
                return null;
            }
            catch (Exception ex)
            {
                return "解绑计时器失败:" + ex.Message;
            }

        }

        private void Read_T1_Tri_Elapsed(object sender, ElapsedEventArgs e)
        {
            int _res;

            int num = 0;

            this.Mpos_Tri.AutoReset = false;

            if (mainForm.Delta.IntPtr == IntPtr.Zero)
            {

                this.Mpos_Tri.AutoReset = true;
                return;

            }
            {
            
                try
                {
                    this.Invoke(new Action(() =>
                    {
                        mainForm.Delta.Read_PosJ(ref posNow);

                        for (int i = 0; i < 4; i++)
                        {
                            (this.uiPanel2.Controls["TX_MposJ" + i.ToString()].Text) = posNow[i].ToString();
                        }

                        mainForm.Delta.Read_PosL(ref posNow);

                        for (int i = 0; i < 4; i++)
                        {
                            (this.uiPanel3.Controls["TX_MposL" + i.ToString()].Text) = posNow[i].ToString();
                        }

                    }));
                }
                catch (Exception ex)
                {
                   
                }

            }

            this.Mpos_Tri.AutoReset = true;

        }

        private void Btn_Locked_Click(object sender, EventArgs e)
        {
            uint[] value = new uint[1] {0};
            int _res;
            _res = mainForm.Delta.Set_OutMuti(12, 4, value);
            if (_res != 0)
            {
                MessageBox.Show("上锁失败");
            }
            else
            {
                MessageBox.Show("上锁成功");
                enable = 1;
            }
        }

        private void Btn_Unlock_Click(object sender, EventArgs e)
        {   
            uint[] value = new uint[1] {15};

            int _res;
            _res = mainForm.Delta.Set_OutMuti(12,4,value);
            if (_res != 0)
            {
                MessageBox.Show("解锁失败");
            }
            else
            {
                MessageBox.Show("解锁成功");
                enable = 0;
            }
        }

        private void Btn_EStop_Click(object sender, EventArgs e)
        {
            int _res;
            _res = mainForm.Delta.Stop();
            if (_res != 0)
            {
                MessageBox.Show("停止失败");
            }
            
        }


        private void Btn_SpeedAdd_Click(object sender, EventArgs e)
        {

            if(handSpeedPerc>=100 && handSpeedPerc < 200)
            {
                handSpeedPerc = handSpeedPerc + 50;
            }
            else if(handSpeedPerc >= 50 && handSpeedPerc <100)
            {
                handSpeedPerc = handSpeedPerc + 25;
            }
            else if (handSpeedPerc < 50)
            {
                handSpeedPerc = handSpeedPerc + 10;
            }

            TX_HandSpeedPerc.Text = handSpeedPerc.ToString();
        }

        private void Btn_SpeedSub_Click(object sender, EventArgs e)
        {
            if (handSpeedPerc > 100)
            {
                handSpeedPerc = handSpeedPerc - 50;
            }
            else if (handSpeedPerc > 50 && handSpeedPerc <= 100)
            {
                handSpeedPerc = handSpeedPerc - 25;
            }
            else if (handSpeedPerc > 5 && handSpeedPerc <= 50)
            {
                handSpeedPerc = handSpeedPerc - 10;
                if (handSpeedPerc == 0)
                {
                    handSpeedPerc = 10;
                }
            }

            TX_HandSpeedPerc.Text = handSpeedPerc.ToString();
        }


        private void Btn_JRev_MouseDown(object sender, MouseEventArgs e)
        {

            int _res;

            float[] temp = new float[4] {0,0,0,0};

            if (enable == 0)
            {
                MessageBox.Show("轴未使能");
            }
            {

                int Id = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (((UIButton)sender).Name == ("Btn_JRev" + i))
                    {
                        Id = i;
                    }
                }

                _res = mainForm.Delta.AxisSpeed(Convert.ToSingle(TX_SpeedJ.Text), Convert.ToSingle(TX_AccelJ.Text),50);
                if (_res != 0)
                {
                    MessageBox.Show("速度设置失败");
                }
              
                switch (Id)
                {
                    case 0:

                        temp[0] = -999;
                        _res = mainForm.Delta.MoveJ(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;

                    case 1:
                        temp[1] = -999;
                        _res = mainForm.Delta.MoveJ(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;

                    case 2:
                        temp[2] = -999;
                        _res = mainForm.Delta.MoveJ(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;

                    case 3:
                        temp[3] = -999;
                        _res = mainForm.Delta.MoveJ(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;
                }            

            }
        }

        private void Btn_JRev_MouseUp(object sender, MouseEventArgs e)
        {         
            int _res;
            _res= mainForm.Delta.Stop();
            if (_res != 0)
            {
                return;
            }
        }

        private void Btn_JFwd_MouseDown(object sender, MouseEventArgs e)
        {
            int _res;

            float[] temp = new float[4] {0,0,0,0};

            if (enable == 0)
            {
                MessageBox.Show("轴未使能");
            }
            {

                int Id = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (((UIButton)sender).Name == ("Btn_JFwd" + i))
                    {
                        Id = i;
                    }
                }

                _res = mainForm.Delta.AxisSpeed(Convert.ToSingle(TX_SpeedJ.Text), Convert.ToSingle(TX_AccelJ.Text), 50);
                if (_res != 0)
                {
                    MessageBox.Show("速度设置失败");
                }
              
                switch (Id)
                {
                    case 0:
                        temp[0] = 999;
                        _res = mainForm.Delta.MoveJ(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;

                    case 1:
                        temp[1] = 999;
                        _res = mainForm.Delta.MoveJ(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;

                    case 2:
                        temp[2] = 999;
                        _res = mainForm.Delta.MoveJ(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;

                    case 3:
                        temp[3] = 999;
                        _res = mainForm.Delta.MoveJ(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;
                }
            }
        }

        private void Btn_JFwd_MouseUp(object sender, MouseEventArgs e)
        {
            int _res;
            _res = mainForm.Delta.Stop();
            if (_res != 0)
            {
                return;
            }
        }

        private void Btn_LRev_MouseDown(object sender, MouseEventArgs e)
        {
            int _res;

            float[] temp = new float[4] {0,0,0,0};

            if (enable == 0)
            {
                MessageBox.Show("轴未使能");
            }
            {

                int Id = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (((UIButton)sender).Name == ("Btn_LRev" + i))
                    {
                        Id = i;
                    }
                }

                _res = mainForm.Delta.AxisSpeed(Convert.ToSingle(TX_SpeedJ.Text), Convert.ToSingle(TX_AccelJ.Text), 50);
                if (_res != 0)
                {
                    MessageBox.Show("速度设置失败");
                }
             
                switch (Id)
                {
                    case 0:
                        temp[0] = -999;
                        _res = mainForm.Delta.MoveL(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;

                    case 1:
                        temp[1] = -999;
                        _res = mainForm.Delta.MoveL(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;

                    case 2:
                        temp[2] = -999;
                        _res = mainForm.Delta.MoveL(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;

                    case 3:
                        temp[3] = -999;
                        _res = mainForm.Delta.MoveL(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;
                }

            }
        }

        private void Btn_LRev_MouseUp(object sender, MouseEventArgs e)
        {
            int _res;
            _res = mainForm.Delta.Stop();
            if (_res != 0)
            {
                return;
            }
        }

        private void Btn_LFwd_MouseDown(object sender, MouseEventArgs e)
        {
            int _res;

            float[] temp = new float[4] {0,0,0,0};

            if (enable == 0)
            {
                MessageBox.Show("轴未使能");
            }
            {

                int Id = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (((UIButton)sender).Name == ("Btn_LFwd" + i))
                    {
                        Id = i;
                    }
                }

                _res = mainForm.Delta.AxisSpeed(Convert.ToSingle(TX_SpeedL.Text), Convert.ToSingle(TX_AccelL.Text), 50);
                if (_res != 0)
                {
                    MessageBox.Show("速度设置失败");
                }
              
                switch (Id)
                {
                    case 0:
                        temp[0] = 999;
                        _res = mainForm.Delta.MoveL(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;

                    case 1:
                        temp[1] = 999;
                        _res = mainForm.Delta.MoveL(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;

                    case 2:
                        temp[2] = 999;
                        _res = mainForm.Delta.MoveL(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;

                    case 3:
                        temp[3] = 999;
                        _res = mainForm.Delta.MoveL(temp);
                        if (_res != 0)
                        {
                            MessageBox.Show("运动失败");
                        }
                        break;
                }

            }
        }

        private void Btn_LFwd_MouseUp(object sender, MouseEventArgs e)
        {
            int _res;
            _res = mainForm.Delta.Stop();
            if (_res != 0)
            {
                return;
            }
        }

        private void 手动运动_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnBindTimers();
        }

       
    }
}
