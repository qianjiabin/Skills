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
using System.Timers;
using System.Threading;

namespace Delta
{
    public partial class 示教系统 : UIPage
    {

        MainForm mainForm;

        float[] posNowJ = new float[4];
        float[] posNowL = new float[4];
        float[] axisPara = new float[3];

        public Motion.IniFile IniFilePara = new Motion.IniFile("DeltaPos");    // 

        #region 文件变量

        public string LpAppname = "示教点";
       
        public string Lpkeynum = "pos";

        #endregion

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
                        mainForm.Delta.Read_PosJ(ref posNowJ);

                        for (int i = 0; i < 4; i++)
                        {
                            (this.uiGroupBox4.Controls["TX_MposJ" + i.ToString()].Text) = posNowJ[i].ToString();
                        }

                        mainForm.Delta.Read_PosL(ref posNowL);

                        for (int i = 0; i < 4; i++)
                        {
                            (this.uiGroupBox3.Controls["TX_MposL" + i.ToString()].Text) = posNowL[i].ToString();
                        }

                    }));
                }
                catch (Exception ex)
                {

                }

            }

            this.Mpos_Tri.AutoReset = true;

        }

        public 示教系统(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            this.InitTimers();
            BindTimers();
            ReadPos();
        }

        private void Btn_TeachPos_Click(object sender, EventArgs e)
        {
            string strText = string.Empty;
            int _res = 0;
            if (InputDialog.show(out strText) == DialogResult.OK)
            {
                DataGridViewRow dr = new DataGridViewRow();
                dr.CreateCells(this.DGV_Motion);

               _res = RepeatDetect(strText);
                if (_res == 0)
                {
                    dr.Cells[0].Value = this.DGV_Motion.Rows.Count - 1;
                    dr.Cells[1].Value = strText;

                    mainForm.Delta.Read_PosJ(ref posNowJ);
                    mainForm.Delta.Read_PosL(ref posNowL);

                    for (int i = 0; i < 4; i++)
                    {
                        dr.Cells[i + 2].Value = posNowL[i];
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        dr.Cells[i + 6].Value = posNowJ[i];
                    }

                    this.DGV_Motion.Rows.Add(dr);

                }
                else
                {
                    MessageBox.Show("点位名称重复");
                    return;
                }           
            }
            else
            {
                return;
            }        
        }


        private void Btn_ClearPos_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("确定删除？", "删除确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {

                int i = this.DGV_Motion.SelectedCells[0].RowIndex;

                this.DGV_Motion.Rows.RemoveAt(i);

                for (int j = 0; j < DGV_Motion.Rows.Count - i - 1; j++)
                {
                    this.DGV_Motion.Rows[i + j].Cells[0].Value = i + j;
                }

            }
                
        }


        private void Btn_SavePos_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定保存？", "保存确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    string[] temp = new string[10];
                    string value;

                    IniFilePara.DeleteSection(LpAppname);

                    for (int i = 0; i < DGV_Motion.Rows.Count - 1; i++)
                    {
                        Lpkeynum = "pos" + i;

                        for (int j = 0; j <10; j++)
                        {
                            temp[j] = (DGV_Motion.Rows[i].Cells[j].Value).ToString();
                        }

                        value = temp[0] + "," + temp[1] + "," + temp[2] + "," + temp[3] + "," + temp[4] + "," + temp[5] + "," + temp[6] + "," + temp[7] + "," + temp[8] + "," + temp[9];

                        IniFilePara.Write(Lpkeynum, value , LpAppname);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            mainForm.Delta.getpos();    //保存后更新一次文件数据，保证列表刷新正常

        }


        /// <summary>
        /// 重复性检测
        /// </summary>
        public int RepeatDetect(string value)
        {
            
            object obj = null;

            for (int i = 0; i < DGV_Motion.Rows.Count - 1; i++)
            {
                obj = DGV_Motion.Rows[i].Cells[1].Value;
             
                if (obj.ToString() == value)
                {
                    return -1;
                }
            }

            return 0;
        }


        public void ReadPos()
        {

            try
            {
                string[] temp = new string[10];
                string value;

                for (int i = 0; i < 100; i++)
                {
                    Lpkeynum = "pos" + i;

                    bool exist = IniFilePara.KeyExists(Lpkeynum, LpAppname);

                    if (exist==true)
                    {   

                        value = IniFilePara.Read(Lpkeynum, LpAppname);

                        temp= (value.Split(','));

                        DataGridViewRow dr = new DataGridViewRow();
                        dr.CreateCells(this.DGV_Motion);

                        dr.Cells[0].Value = temp[0];
                        dr.Cells[1].Value = temp[1];

                        for (int j = 0; j < 4; j++)
                        {
                            dr.Cells[j + 2].Value = temp[j+2];
                        }
                        for (int j = 0; j < 4; j++)
                        {
                            dr.Cells[j + 6].Value = temp[j +6];
                        }

                        this.DGV_Motion.Rows.Add(dr);

                    }
                    else
                    {
                        return;
                    }
                 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void CB_PosList_DropDown(object sender, EventArgs e)
        {
            ListPos();
            mainForm.Delta.getpos();
        }


        public void ListPos()
        {

            CB_PosList.Items.Clear();

            for (int i = 0; i < DGV_Motion.Rows.Count - 1; i++)
            {
                CB_PosList.Items.Insert(i, DGV_Motion.Rows[i].Cells[1].Value);
            }

        }

        private void Btn_MovePos_Click(object sender, EventArgs e)
        {

            int _res=0;

            string strText = string.Empty;
            
            if (InputDialog.paraSet(ref axisPara) == DialogResult.OK)
            {
                if (TX_Status.Text != "运动中")
                {

                    mainForm.Delta.AxisSpeed(axisPara[1], axisPara[2]);

                    if (axisPara[0] == 0)
                    {
                        _res = mainForm.Delta.MoveLAbs(CB_PosList.Text);
                        if(_res == -1)
                        {
                            MessageBox.Show("点位异常，请确认保存成功");
                            return;
                        }
                     
                    }
                    else if (axisPara[0] == 1)
                    {
                        _res = mainForm.Delta.MoveJAbs(CB_PosList.Text);
                        if (_res == -1)
                        {
                            MessageBox.Show("点位名称未找到，请确认保存成功");
                            return;
                        }
                    
                    }
                  
                    if (_res ==0)
                    {

                        TX_Status.Text = "运动中";
                        TX_Status.ForeReadOnlyColor = Color.Orange;
                        task();                     
                    }
                    else
                    {
                        MessageBox.Show("运动失败");
                    }
                }
            }
            else
            {
                return;
            }

        }


        private void Btn_StopPos_Click(object sender, EventArgs e)
        { 
            int _res = mainForm.Delta.Stop();
            if (_res == 0)
            {
                TX_Status.Text = "停止中";
                TX_Status.ForeReadOnlyColor = Color.Black;
            }
        }


        private void task()
        {

            int _res = 0;

            Task.Run(() =>
            {   
                _res = mainForm.Delta.WaitStop();              
                this.Invoke((Action)(() =>
                {
                                  
                    if (_res == 0)
                    {
                        TX_Status.Text = "运动到位";
                        TX_Status.ForeReadOnlyColor = Color.Green;
                    }
                    else
                    {
                        TX_Status.Text = "运动超时";
                        TX_Status.ForeReadOnlyColor = Color.Red;
                    }

                }));

            });

        }    
    }
}
