using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Delta
{
    public partial class Delta臂长参数 : UIPage
    {   
       
        MainForm mainForm;

        private float[] l_para = new float[7]; //上间距半径R 下间距半径r 上杆长度 下杆长度，LX,LY,LZ

        public Delta臂长参数(MainForm form)
        {
            InitializeComponent();

            this.mainForm = form;

            Task.Run(() =>
            {
                Thread.Sleep(500);

                if (mainForm.Delta.IntPtr != null)
                {

                    Read_parameter();
                }

            });

            this.InitTimers();

            BindTimers();

        }


        //任务触发事件
        public System.Timers.Timer Para_Tri;

        private string InitTimers()
        {
            try
            {
                this.Para_Tri = new System.Timers.Timer();

                return null;
            }
            catch (Exception ex)
            {
                return "实例化计时器失败:" + ex.Message;
            }
        }

        private string BindTimers()
        {
            try
            {

                this.Para_Tri.Interval = 2000;
                this.Para_Tri.Elapsed += Read_T1_Tri_Elapsed;
                this.Para_Tri.AutoReset = false;
                this.Para_Tri.Enabled = true;

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

                this.Para_Tri.Elapsed -= Read_T1_Tri_Elapsed;
                this.Para_Tri.Enabled = false;
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
         
            this.Para_Tri.AutoReset = false;

            if (mainForm.Delta.IntPtr == IntPtr.Zero)
            {

                this.Para_Tri.AutoReset = true;
                return;

            }
            {
                if (mainForm.Delta.Sign == 2)
                {
                    Read_parameter();
                    mainForm.Delta.Sign = 1;
                }          
            }

            this.Para_Tri.AutoReset = true;

        }


        private void Btn_Save_Click(object sender, EventArgs e)
        {

            int _res;

            DialogResult dr=MessageBox.Show("确定保存？", "保存确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr==DialogResult.Yes)
            {
                int num = 0;

                try
                {
                    foreach (Control control in this.uiGroupBox1.Controls)
                    {
                        //遍历所有TextBox...

                        if (control is UITextBox)
                        {

                            UITextBox t = (UITextBox)control;

                            l_para[num] = Convert.ToSingle(t.Text);

                            num++;
                        }

                    }

                   _res = mainForm.Delta.SetVR(90, 7, l_para);
                   if (_res != 0) 
                   {

                        MessageBox.Show("参数保存出错");
                   }
                         
                }
                catch(Exception ex)     
                {
                    MessageBox.Show(ex.Message);
                }
            }
          
        }

        private int Read_parameter()
        {

            int _res;

            _res = mainForm.Delta.ReadVR(90, 7, ref l_para);
            if (_res != 0)
            {

                return _res;
              
            }

            this.Invoke(new Action(() =>
            {
                try
                {

                    int num = 0;

                    foreach (Control control in this.uiGroupBox1.Controls)
                    {
                        //遍历所有TextBox...
                     
                        if (control is UITextBox)
                        {
                            UITextBox t = (UITextBox)control;

                            t.Text = l_para[num].ToString();
                          
                            num++;
                        }

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }));


            return 0;

        }

        private void Delta臂长参数_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnBindTimers();
        }
    }
}
