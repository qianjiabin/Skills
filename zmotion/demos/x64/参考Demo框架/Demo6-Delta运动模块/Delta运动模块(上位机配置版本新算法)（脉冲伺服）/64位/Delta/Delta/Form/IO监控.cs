using Sunny.UI;
using Sunny.UI.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Delta
{
    public partial class IO监控 : UIPage
    {

        MainForm mainForm;

        uint[] inputValue = new uint[27];
        uint[] outputValue = new uint[18];

        public IO监控(MainForm form)
        {
            InitializeComponent();

            this.mainForm = form;

            this.InitTimers();

            BindTimers();
        }

        #region 【计时器】  

        //任务触发事件
        public System.Timers.Timer IO_Tri;

        private string InitTimers()
        {
            try
            {
                this.IO_Tri = new System.Timers.Timer();

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

                this.IO_Tri.Interval = 50;
                this.IO_Tri.Elapsed += Read_T1_Tri_Elapsed;
                this.IO_Tri.AutoReset = false;
                this.IO_Tri.Enabled = true;

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

                this.IO_Tri.Elapsed -= Read_T1_Tri_Elapsed;
                this.IO_Tri.Enabled = false;
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

            this.IO_Tri.AutoReset = false;

            if (mainForm.Delta.IntPtr == IntPtr.Zero)
            {

                this.IO_Tri.AutoReset = true;
                return;

            }
            {

                mainForm.Delta.Read_InMuti(0, 27, ref inputValue);
               
                mainForm.Delta.Read_OutMuti(0, 18, ref outputValue);

                try
                {
                    this.Invoke(new Action(() =>
                    {                     
                            foreach (Control control in this.uiGroupBox1.Controls)
                            {
                                //遍历所有TextBox...

                                if (control is UILight)
                                {

                                    UILight t = (UILight)control;

                                    if (inputValue[num] == 1)
                                    {
                                        t.State = UILightState.On;
                                    }
                                    else
                                    {
                                        t.State = UILightState.Off;
                                    }

                                    num++;
                                }

                            }

                            num = 0;

                            foreach (Control control in this.uiGroupBox2.Controls)
                            {
                                //遍历所有TextBox...

                                if (control is UILight)
                                {

                                    UILight t = (UILight)control;

                                    if (outputValue[num] == 1)
                                    {
                                        t.State = UILightState.On;
                                    }
                                    else
                                    {
                                        t.State = UILightState.Off;
                                    }

                                    num++;
                                }

                            }
                                        
                    }));
                }
                catch(Exception ex) 
                {
                  
                }
                
            }

            this.IO_Tri.AutoReset = true;
        }

        private void Sw_OUT_ValueChanged(object sender, bool value)
        {

            if (mainForm.Delta.IntPtr == IntPtr.Zero)
            {

                this.IO_Tri.AutoReset = true;
                return;

            }

            int Id = 0;
            for (int i = 0; i < 18; i++)
            {
                if (((UISwitch)sender).Name == ("Sw_OUT" + i))
                {
                    Id = i;

                    if (((UISwitch)sender).Active)
                    {

                        mainForm.Delta.Set_OutSingle(Id,1);

                        return;

                    }
                    else
                    {

                        mainForm.Delta.Set_OutSingle(Id, 0);

                        return;
                    }

                }
            }

        }

        private void IO监控_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnBindTimers();
        }
    }

}
