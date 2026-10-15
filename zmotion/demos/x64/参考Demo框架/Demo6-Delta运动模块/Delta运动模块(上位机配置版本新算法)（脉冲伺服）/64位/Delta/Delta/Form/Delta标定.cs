using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Motion;
using System.Collections;
using cszmcaux;
using System.Timers;
using System.Threading;

namespace Delta
{
    public partial class Delta标定 : UIPage
    {

        MainForm mainForm;
  
        float[] posNow= new float[4];

        public Delta标定(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;

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

                this.Mpos_Tri.Interval = 20;
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

                for (int i = 0; i < 4; i++)
                {
                    zmcaux.ZAux_Direct_GetMpos(mainForm.Delta.IntPtr, i, ref posNow[i]);
                }

                try
                {
                    this.Invoke(new Action(() =>
                    {

                        for (int i = 0; i < 4; i++)
                        {
                            (this.uiGroupBox4.Controls["TX_Mpos" + i.ToString()].Text) = posNow[i].ToString();
                        }

                    }));
                }
                catch (Exception ex)
                {

                }

            }

            this.Mpos_Tri.AutoReset = true;
        }


        private void Btn_Confirm_Click(object sender, EventArgs e)
        {

            int _res;

            float[] value = new float[4];

            DialogResult dr = MessageBox.Show("确定机械手回零？", "回零确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {

                TX_HomeState.Text = "回零中";
                TX_HomeState.ForeReadOnlyColor = Color.Orange;

                Task.Run(() =>
                {
                    mainForm.Delta.EStop();
              
                    for (int i = 0; i < 4; i++)
                    {
                        value[i] = Convert.ToSingle(this.uiGroupBox2.Controls["TX_Offset" + i.ToString()].Text);
                    }

                    _res = mainForm.Delta.GoHome(Convert.ToInt32(TX_Mode.Text), Convert.ToSingle(TX_Speed.Text), Convert.ToSingle(TX_Creep.Text), value);         

                    this.Invoke((Action)(() =>
                    {
                    
                        if (_res != 0)
                        {
                            MessageBox.Show("回零失败");
                            TX_HomeState.Text = "回零失败";
                            TX_HomeState.ForeReadOnlyColor = Color.Red;
                        }
                        else
                        {
                            MessageBox.Show("回零完成");
                            TX_HomeState.Text = "回零完成";
                            TX_HomeState.ForeReadOnlyColor = Color.Green;
                            return;
                        }

                    }));
                
                });          
            }
        }

        private void Delta标定_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnBindTimers();
        }
    }
}
