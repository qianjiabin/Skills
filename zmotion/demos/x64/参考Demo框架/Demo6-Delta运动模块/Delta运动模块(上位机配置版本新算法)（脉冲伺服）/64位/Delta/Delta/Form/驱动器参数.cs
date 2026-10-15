using cszmcaux;
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
    public partial class 驱动器参数 : UIPage
    {

        private int[] jAxisList = new int[4];  //关节轴轴号

        private float[] jOrigiNum = new float[4]; //关节轴原点对应的io编号

        private float[] jOrigiInvert = new float[4]; //关节轴原点对应的io信号反转

        private float[] jNumPulses = new float[4]; //电机的一圈脉冲数

        private float[] jReducRatio = new float[4]; //电机减速比

        private int[] axisDir = new int[4]; //电机方向

        private float[] f_Limit = new float[4]; //轴的正软限位

        private float[] r_Limit = new float[4]; //轴的负软限位
 
        private float[] temp = new float[4];

        MainForm mainForm;

        public 驱动器参数(MainForm form)
        {
            InitializeComponent();

            this.mainForm = form;

            Task.Run(() =>
            {
                 Thread.Sleep(500);
                
                if(mainForm.Delta.IntPtr!=null)
                {

                    Read_parameter();
                }

            });

            InitTimers();
            BindTimers();
        }

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
                if (mainForm.Delta.Sign == 1)
                {
                    Read_parameter();
                    mainForm.Delta.Sign = 0;
                }
            }

            this.Para_Tri.AutoReset = true;

        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {

            int _res;

            DialogResult dr = MessageBox.Show("确定保存？", "保存确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    // 更新Delta参数设置界面的参数信息
                    for (int i = 0; i < 4; i++)
                    {
                        //轴列表情况
                        jAxisList[i] = Convert.ToInt32(this.Controls["AxisID" + i.ToString()].Text);
                        //原点io映射
                        jOrigiNum[i] = Convert.ToInt32(this.Controls["origin" + i.ToString()].Text);
                        //电机一圈脉冲数
                        jNumPulses[i] = Convert.ToInt32(this.Controls["PulseNum" + i.ToString()].Text);
                        //各个轴的减速比
                        jReducRatio[i] = Convert.ToInt32(this.Controls["ReducRatio" + i.ToString()].Text);

                        //各个轴的软件限位
                        f_Limit[i] = Convert.ToSingle(this.Controls["AxisFsLimit" + i.ToString()].Text);
                        r_Limit[i] = Convert.ToSingle(this.Controls["AxisRsLimit" + i.ToString()].Text);
                    }

                    Set_parameter();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        private void Btn_dir(object sender, EventArgs e)
        {

            int Id = 0;

            for (int i = 0; i < 18; i++)
            {
                if (((UIButton)sender).Name == ("Btn_dir" + i))
                {
                    Id = i;

                    if (((UIButton)sender).Text=="正向")
                    {

                        axisDir[Id]=-1;

                        ((UIButton)sender).Text = "负向";

                        ((UIButton)sender).ForeColor = Color.Black;

                        return;

                    }
                    else if (((UIButton)sender).Text == "负向")
                    {
                        axisDir[Id] = 1;

                        ((UIButton)sender).Text = "正向";

                        ((UIButton)sender).ForeColor = Color.White;

                        return;
                    }

                }
            }
        }



        private int Read_parameter()
        {
            int _res;

            _res = mainForm.Delta.ReadVR(0, 4, ref temp);     //读取关节轴编号
            if (_res != 0)
            {
                return _res;
            }
            jAxisList = Array.ConvertAll<float, int>(temp, (x) => {
                return Convert.ToInt32(x);
            });


            _res = mainForm.Delta.ReadVR( 10, 4, ref jOrigiNum);     //读取各轴原点io编号
            if (_res != 0)
            {
                return _res;
            }

            _res = mainForm.Delta.ReadVR(20, 4, ref jNumPulses);     //读取电机的一圈脉冲数
            if (_res != 0)
            {
                return _res;
            }

            _res = mainForm.Delta.ReadVR(30, 4, ref jReducRatio);     //读取电机减速比
            if (_res != 0)
            {
                return _res;
            }

            _res = mainForm.Delta.ReadVR(40, 4, ref temp);     //读取电机方向
            if (_res != 0)
            {
                return _res;
            }
            else
            {
                axisDir = Array.ConvertAll<float, int>(temp, (x) => {
                    return Convert.ToInt32(x);
                });

                for (int i = 0; i < 4; i++)
                {
                    if (axisDir[i] == 0)
                    {
                        axisDir[i] = 1;
                    }
                    else if (axisDir[i] == 1)
                    {
                        axisDir[i] = -1;
                    }
                }
            }

            _res = mainForm.Delta.ReadVR(70, 4, ref f_Limit);     //读取软件正限位
            if (_res != 0)
            {
                return _res;
            }

            _res = mainForm.Delta.ReadVR(80, 4, ref r_Limit);     //读取软件负限位
            if (_res != 0)
            {
                return _res;
            }


            _res = mainForm.Delta.ReadVR(100, 4, ref jOrigiInvert);     //读取各轴原点io编号反转情况
            if (_res != 0)
            {
                return _res;
            }


            this.Invoke(new Action(() =>
            {
                try
                {
                    // 更新Delta参数设置界面的参数信息
                    for (int i = 0; i < 4; i++)
                    {
                        //轴列表情况
                        (this.Controls["AxisID" + i.ToString()].Text) = jAxisList[i].ToString();
                        //原点io映射
                        (this.Controls["origin" + i.ToString()].Text) = jOrigiNum[i].ToString();
                        //电机一圈脉冲数                  
                        (this.Controls["PulseNum" + i.ToString()].Text) = jNumPulses[i].ToString();
                        //各个轴的减速比             
                        (this.Controls["ReducRatio" + i.ToString()].Text) = jReducRatio[i].ToString();
                        //各个轴的软件限位                
                        (this.Controls["AxisFsLimit" + i.ToString()].Text) = f_Limit[i].ToString();
                        (this.Controls["AxisRsLimit" + i.ToString()].Text) = r_Limit[i].ToString();

                        if (axisDir[i] == -1)
                        {
                            (this.Controls["Btn_dir" + i.ToString()].Text) = "负向";

                            (this.Controls["Btn_dir" + i.ToString()].ForeColor) = Color.Black;
                        }
                        else
                        {
                            (this.Controls["Btn_dir" + i.ToString()].Text) = "正向";

                            (this.Controls["Btn_dir" + i.ToString()].ForeColor) = Color.White;
                        }

                        if (jOrigiInvert[i] == 0)
                        {
                            (this.Controls["Btn_invert" + i.ToString()].Text) = "不反转";

                            (this.Controls["Btn_invert" + i.ToString()].ForeColor) = Color.WhiteSmoke;
                        }
                        else
                        {
                            (this.Controls["Btn_invert" + i.ToString()].Text) = "反转";

                            (this.Controls["Btn_invert" + i.ToString()].ForeColor) = Color.White;
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


        private int Set_parameter()
        {

            int _res;

            temp = Array.ConvertAll<int, float>(jAxisList, (x) => {
                return Convert.ToInt32(x);
            });

            _res = mainForm.Delta.SetVR(0, 4, temp);     
            if (_res != 0)
            {
                return _res;
            }
          

            _res = mainForm.Delta.SetVR(10, 4, jOrigiNum);     
            if (_res != 0)
            {
                return _res;
            }

            _res = mainForm.Delta.SetVR(20, 4, jNumPulses);    
            if (_res != 0)
            {
                return _res;
            }

            _res = mainForm.Delta.SetVR(30, 4, jReducRatio);   
            if (_res != 0)
            {
                return _res;
            }


            temp  = Array.ConvertAll<int, float>(axisDir, (x) => {
                return Convert.ToInt32(x);
            });
            for (int i = 0; i < 4; i++)
            {
                if (temp[i] == 1)
                {
                    temp[i] = 0;
                }
                else if (temp[i] == -1)
                {
                    temp[i] = 1;
                }
            }

            _res = mainForm.Delta.SetVR(40, 4, temp);     
            if (_res != 0)
            {
                return _res;
            }
            
            _res = mainForm.Delta.SetVR(70, 4, f_Limit);     
            if (_res != 0)
            {
                return _res;
            }

            _res = mainForm.Delta.SetVR(80, 4, r_Limit);     
            if (_res != 0)
            {
                return _res;
            }

            _res = mainForm.Delta.SetVR(100, 4, jOrigiInvert);
            if (_res != 0)
            {
                return _res;
            }

            _res = mainForm.Delta.Initialize();
            if (_res != 0)
            {
                return _res;
            }

            return 0;
        }

        private void 驱动器参数_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnBindTimers();
        }

        private void Btn_invert(object sender, EventArgs e)
        {
            int Id = 0;

            for (int i = 0; i < 18; i++)
            {
                if (((UIButton)sender).Name == ("Btn_invert" + i))
                {
                    Id = i;

                    if (((UIButton)sender).Text == "反转")
                    {

                        jOrigiInvert[Id] = 0;

                        ((UIButton)sender).Text = "不反转";

                        ((UIButton)sender).ForeColor = Color.WhiteSmoke;

                        return;

                    }
                    else if (((UIButton)sender).Text == "不反转")
                    {

                        jOrigiInvert[Id] = 1;

                        ((UIButton)sender).Text = "反转";

                        ((UIButton)sender).ForeColor = Color.White;

                        return;
                    }

                }
            }
        }
    }
}
