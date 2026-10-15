using cszmcaux;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assemble
{
    public delegate void LogDelegate(string Message);

    public partial class FormMain : Form
    {
        MotionEx ZmotionCard;   // 定义运动控制卡对象

        public FormMain()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            ZmotionCard = new MotionEx(Log);
            button_InitZMotion_Click(sender, e);
            MyConnection connect = new MyConnection(Log , ZmotionCard.g_handle);
            connect.CreateConnection();
        }

        //日志输出
        public void Log(string log)
        {
            string Time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff ");
            //怎么保证窗口总显示在最下方?
            log = Time + log;
            this.toolStripStatusLabel_Log.Text = log;
            this.textBox_Log.Text += log;
            this.textBox_Log.Text += Environment.NewLine;
        }

        //运动控制卡初始化
        private void button_InitZMotion_Click(object sender, EventArgs e)
        {
            if (!ZmotionCard.InitCard())
            {
                //初始化失败，退出程序
                Log("轴参数设置失败!");
                System.Environment.Exit(0);
            }
            else
            {
                Log("轴参数设置成功!");
                timer1.Start();
            }
        }

        //定时器刷新轴相关参数
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ZmotionCard.m_bInited == false)
            {
                zmcaux.ZAux_Close(ZmotionCard.g_handle);
                ZmotionCard.g_handle = (IntPtr)0;
                Log("控制卡掉线!试重新初始化!");
                timer1.Stop();
            }

            label_Xrunstate.Text = "运动状态：" + Convert.ToString(ZmotionCard.Axis_IsMoving(ZmotionCard.m_nAxis_X) ? "运行中" : "停止中");
            label_XcurDpos.Text = "当前轴坐标：" + ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_X).ToString("f3");      //格式化输出,Lable可能显示不全
            label_Xaxisstatus.Text = "当前轴状态：" + ZmotionCard.Axis_Status(ZmotionCard.m_nAxis_X);

            label_Yrunstate.Text = "运动状态：" + Convert.ToString(ZmotionCard.Axis_IsMoving(ZmotionCard.m_nAxis_Y) ? "运行中" : "停止中");
            label_YcurDpos.Text = "当前轴坐标：" + ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_Y).ToString("f3");
            label_Yaxisstatus.Text = "当前轴状态：" + ZmotionCard.Axis_Status(ZmotionCard.m_nAxis_Y);

            label_Zrunstate.Text = "运动状态：" + Convert.ToString(ZmotionCard.Axis_IsMoving(ZmotionCard.m_nAxis_Z) ? "运行中" : "停止中");
            label_ZcurDpos.Text = "当前轴坐标：" + ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_Z).ToString("f3");
            label_Zaxisstatus.Text = "当前轴状态：" + ZmotionCard.Axis_Status(ZmotionCard.m_nAxis_Z);

            label_Urunstate.Text = "运动状态：" + Convert.ToString(ZmotionCard.Axis_IsMoving(ZmotionCard.m_nAxis_U) ? "运行中" : "停止中");
            label_UcurDpos.Text = "当前轴坐标：" + ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_U).ToString("f3");
            label_Uaxisstatus.Text = "当前轴状态：" + ZmotionCard.Axis_Status(ZmotionCard.m_nAxis_U);

            //label_XDatumInStatus.Text = "原点IN: " + Convert.ToString(ZmotionCard.GetInput(ZmotionCard.m_nAxis_X_Home) ? "ON" : "OFF");
            //label_XFwdInStatus.Text = "正限IN: " + Convert.ToString(ZmotionCard.GetInput(ZmotionCard.m_nXLimit_Positive) ? "ON" : "OFF");
            //label_XRevInStatus.Text = "负限IN: " + Convert.ToString(ZmotionCard.GetInput(ZmotionCard.m_nXLimit_Negative) ? "ON" : "OFF");

            //label_YDatumInStatus.Text = "原点IN: " + Convert.ToString(ZmotionCard.GetInput(ZmotionCard.m_nAxis_Y_Home) ? "ON" : "OFF");
            //label_YFwdInStatus.Text = "正限IN: " + Convert.ToString(ZmotionCard.GetInput(ZmotionCard.m_nYLimit_Positive) ? "ON" : "OFF");
            //label_YRevInStatus.Text = "负限IN: " + Convert.ToString(ZmotionCard.GetInput(ZmotionCard.m_nYLimit_Negative) ? "ON" : "OFF");

            //label_ZDatumInStatus.Text = "原点IN: " + Convert.ToString(ZmotionCard.GetInput(ZmotionCard.m_nAxis_Z_Home) ? "ON" : "OFF");
            //label_ZFwdInStatus.Text = "正限IN: " + Convert.ToString(ZmotionCard.GetInput(ZmotionCard.m_nZLimit_Positive) ? "ON" : "OFF");
            //label_ZRevInStatus.Text = "负限IN: " + Convert.ToString(ZmotionCard.GetInput(ZmotionCard.m_nZLimit_Negative) ? "ON" : "OFF");

            //label_UDatumInStatus.Text = "原点IN: " + Convert.ToString(ZmotionCard.GetInput(ZmotionCard.m_nAxis_U_Home) ? "ON" : "OFF");

            //bool[] bMov = { ZmotionCard.Axis_IsMoving(ZmotionCard.m_nAxis_X), ZmotionCard.Axis_IsMoving(ZmotionCard.m_nAxis_Y), ZmotionCard.Axis_IsMoving(ZmotionCard.m_nAxis_Z), ZmotionCard.Axis_IsMoving(ZmotionCard.m_nAxis_U) };

        }

        //整机回原点
        private void button_AllAxisZero_Click(object sender, EventArgs e)
        {
            ZmotionCard.AllAxis_Home();
            MessageBox.Show("所有轴回原点完成!");
        }

        //整机回零位
        private void button_AxisZero_Click(object sender, EventArgs e)
        {
            double[] dPos = { 0, 0, 0, 0 };
            double[] dVel = { ZmotionCard.m_dAxis_X_Speed, ZmotionCard.m_dAxis_Y_Speed, ZmotionCard.m_dAxis_Z_Speed, ZmotionCard.m_dAxis_U_Speed };
            ZmotionCard.Move4D(dPos, dVel);
            Log("所有轴回零位完成!");
        }

        //整机停止
        private void button_AllAxisStop_Click(object sender, EventArgs e)
        {
            ZmotionCard.AllAxis_StopMove();
        }

        //X运动+
        private void button_XForward_Click(object sender, EventArgs e)
        {
            if (radioButton_XVMove.Checked)
            {
                ZmotionCard.Axis_JogMove(ZmotionCard.m_nAxis_X,1);
            }
            else
            {
                double stepDis = Convert.ToDouble(textBox_XStep.Text);
                ZmotionCard.Axis_Move(ZmotionCard.m_nAxis_X, stepDis);
            }
        }

        //X运动-
        private void button_XReverse_Click(object sender, EventArgs e)
        {
            if (radioButton_XVMove.Checked)
            {
                ZmotionCard.Axis_JogMove(ZmotionCard.m_nAxis_X, -1);
            }
            else
            {
                double stepDis = Convert.ToDouble(textBox_XStep.Text);
                ZmotionCard.Axis_Move(ZmotionCard.m_nAxis_X, -stepDis);
            }
        }

        //X停止
        private void button_XStop_Click(object sender, EventArgs e)
        {
            ZmotionCard.Axis_StopMove(ZmotionCard.m_nAxis_X);
        }


        //X回原
        private void button_XOrigin_Click(object sender, EventArgs e)
        {
            ZmotionCard.Axis_MoveAbs(ZmotionCard.m_nAxis_X, 0);
        }

        //Y运动+
        private void button_YForward_Click(object sender, EventArgs e)
        {
            if (radioButton_YVMove.Checked)
            {
                ZmotionCard.Axis_JogMove(ZmotionCard.m_nAxis_Y, 1);
            }
            else
            {
                double stepDis = Convert.ToDouble(textBox_YStep.Text);
                ZmotionCard.Axis_Move(ZmotionCard.m_nAxis_Y, stepDis);
            }
        }

        //Y运动-
        private void button_YReverse_Click(object sender, EventArgs e)
        {
            if (radioButton_YVMove.Checked)
            {
                ZmotionCard.Axis_JogMove(ZmotionCard.m_nAxis_Y, -1);
            }
            else
            {
                double stepDis = Convert.ToDouble(textBox_YStep.Text);
                ZmotionCard.Axis_Move(ZmotionCard.m_nAxis_Y, -stepDis);
            }
        }

        //Y停止
        private void button_YStop_Click(object sender, EventArgs e)
        {
            ZmotionCard.Axis_StopMove(ZmotionCard.m_nAxis_Y);
        }

        //Y回原
        private void button_YOrigin_Click(object sender, EventArgs e)
        {
            ZmotionCard.Axis_MoveAbs(ZmotionCard.m_nAxis_Y, 0);
        }

        //Z运动+
        private void button_ZForward_Click(object sender, EventArgs e)
        {
            if (radioButton_ZVMove.Checked)
            {
                ZmotionCard.Axis_JogMove(ZmotionCard.m_nAxis_Z, 1);
            }
            else
            {
                double stepDis = Convert.ToDouble(textBox_ZStep.Text);
                ZmotionCard.Axis_Move(ZmotionCard.m_nAxis_Z, stepDis);
            }
        }

        //Z运动-
        private void button_ZReverse_Click(object sender, EventArgs e)
        {
            if (radioButton_ZVMove.Checked)
            {
                ZmotionCard.Axis_JogMove(ZmotionCard.m_nAxis_Z, -1);
            }
            else
            {
                double stepDis = Convert.ToDouble(textBox_ZStep.Text);
                ZmotionCard.Axis_Move(ZmotionCard.m_nAxis_Z, -stepDis);
            }
        }

        //Z停止
        private void button_ZStop_Click(object sender, EventArgs e)
        {
            ZmotionCard.Axis_StopMove(ZmotionCard.m_nAxis_Z);
        }

        //Z回原
        private void button_ZOrigin_Click(object sender, EventArgs e)
        {
            ZmotionCard.Axis_MoveAbs(ZmotionCard.m_nAxis_Z, 0);
        }

        //U运动+
        private void button_UForward_Click(object sender, EventArgs e)
        {
            if (radioButton_UVMove.Checked)
            {
                ZmotionCard.Axis_JogMove(ZmotionCard.m_nAxis_U, 1);
            }
            else
            {
                double stepDis = Convert.ToDouble(textBox_UStep.Text);
                ZmotionCard.Axis_Move(ZmotionCard.m_nAxis_U, stepDis);
            }
        }

        //U运动-
        private void button_UReverse_Click(object sender, EventArgs e)
        {
            if (radioButton_UVMove.Checked)
            {
                ZmotionCard.Axis_JogMove(ZmotionCard.m_nAxis_U, -1);
            }
            else
            {
                double stepDis = Convert.ToDouble(textBox_UStep.Text);
                ZmotionCard.Axis_Move(ZmotionCard.m_nAxis_U, -stepDis);
            }
        }

        //U停止
        private void button_UStop_Click(object sender, EventArgs e)
        {
            ZmotionCard.Axis_StopMove(ZmotionCard.m_nAxis_U);
        }

        //U回原
        private void button_UOrigin_Click(object sender, EventArgs e)
        {
            ZmotionCard.Axis_MoveAbs(ZmotionCard.m_nAxis_U, 0);
        }

        //绝对位置定位
        private void button_MoveAbsPos_Click(object sender, EventArgs e)
        {
            double[] dPos = { Convert.ToDouble(textBox_XTargetPos.Text), Convert.ToDouble(textBox_YTargetPos.Text), Convert.ToDouble(textBox_ZTargetPos.Text), Convert.ToDouble(textBox_UTargetPos.Text) };
            double[] dVel = { ZmotionCard.m_dAxis_X_Speed, ZmotionCard.m_dAxis_Y_Speed, ZmotionCard.m_dAxis_Z_Speed, ZmotionCard.m_dAxis_U_Speed };
            ZmotionCard.Move4D(dPos, dVel);
        }

        //取料点取点
        private void button_GetPos1_Click(object sender, EventArgs e)
        {
            textBox_XAutoPos1.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_X).ToString("f3");
            textBox_YAutoPos1.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_Y).ToString("f3");
            textBox_ZAutoPos1.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_Z).ToString("f3");
            textBox_UAutoPos1.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_U).ToString("f3");
        }

        //取料点定位
        private void button_MoveToPos1_Click(object sender, EventArgs e)
        {
            double[] dPos = { Convert.ToDouble(textBox_XAutoPos1.Text), Convert.ToDouble(textBox_YAutoPos1.Text), Convert.ToDouble(textBox_ZAutoPos1.Text), Convert.ToDouble(textBox_UAutoPos1.Text) };
            double[] dVel = { ZmotionCard.m_dAxis_X_Speed, ZmotionCard.m_dAxis_Y_Speed, ZmotionCard.m_dAxis_Z_Speed, ZmotionCard.m_dAxis_U_Speed };
            ZmotionCard.Move4D(dPos, dVel);
        }

        //取料拍照点取点
        private void button_GetPos2_Click(object sender, EventArgs e)
        {
            textBox_XAutoPos2.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_X).ToString("f3");
            textBox_YAutoPos2.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_Y).ToString("f3");
            textBox_ZAutoPos2.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_Z).ToString("f3");
            textBox_UAutoPos2.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_U).ToString("f3");
        }

        //取料拍照点定位
        private void button_MoveToPos2_Click(object sender, EventArgs e)
        {
            double[] dPos = { Convert.ToDouble(textBox_XAutoPos2.Text), Convert.ToDouble(textBox_YAutoPos2.Text), Convert.ToDouble(textBox_ZAutoPos2.Text), Convert.ToDouble(textBox_UAutoPos2.Text) };
            double[] dVel = { ZmotionCard.m_dAxis_X_Speed, ZmotionCard.m_dAxis_Y_Speed, ZmotionCard.m_dAxis_Z_Speed, ZmotionCard.m_dAxis_U_Speed };
            ZmotionCard.Move4D(dPos, dVel);
        }

        //放料点取点
        private void button_GetPos3_Click(object sender, EventArgs e)
        {
            textBox_XAutoPos3.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_X).ToString("f3");
            textBox_YAutoPos3.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_Y).ToString("f3");
            textBox_ZAutoPos3.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_Z).ToString("f3");
            textBox_UAutoPos3.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_U).ToString("f3");
        }

        //放料点定位
        private void button_MoveToPos3_Click(object sender, EventArgs e)
        {
            double[] dPos = { Convert.ToDouble(textBox_XAutoPos3.Text), Convert.ToDouble(textBox_YAutoPos3.Text), Convert.ToDouble(textBox_ZAutoPos3.Text), Convert.ToDouble(textBox_UAutoPos3.Text) };
            double[] dVel = { ZmotionCard.m_dAxis_X_Speed, ZmotionCard.m_dAxis_Y_Speed, ZmotionCard.m_dAxis_Z_Speed, ZmotionCard.m_dAxis_U_Speed };
            ZmotionCard.Move4D(dPos, dVel);
        }

        //放料拍照点取点
        private void button_GetPos4_Click(object sender, EventArgs e)
        {
            textBox_XAutoPos4.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_X).ToString("f3");
            textBox_YAutoPos4.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_Y).ToString("f3");
            textBox_ZAutoPos4.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_Z).ToString("f3");
            textBox_UAutoPos4.Text = ZmotionCard.Axis_GetCmdPos(ZmotionCard.m_nAxis_U).ToString("f3");
        }

        //放料拍照点定位
        private void button_MoveToPos4_Click(object sender, EventArgs e)
        {
            double[] dPos = { Convert.ToDouble(textBox_XAutoPos4.Text), Convert.ToDouble(textBox_YAutoPos4.Text), Convert.ToDouble(textBox_ZAutoPos4.Text), Convert.ToDouble(textBox_UAutoPos4.Text) };
            double[] dVel = { ZmotionCard.m_dAxis_X_Speed, ZmotionCard.m_dAxis_Y_Speed, ZmotionCard.m_dAxis_Z_Speed, ZmotionCard.m_dAxis_U_Speed };
            ZmotionCard.Move4D(dPos, dVel);
        }

        //自动启动
        private void button_AutoStart_Click(object sender, EventArgs e)
        {
            //若是暂停则恢复运动
            if (ZmotionCard.m_bPaused == true)
            {
                ZmotionCard.AllAxis_ResumeMove();
            }
            else
            {
                Log("自动运行中...");
                button_MoveToPos1_Click(sender, e);     //取料点定位
                button_MoveToPos2_Click(sender, e);     //取料拍照点定位
                button_MoveToPos4_Click(sender, e);     //放料拍照点定位
                button_MoveToPos3_Click(sender, e);     //放料点定位
            }
            
        }

        //自动暂停
        private void button_AutoPause_Click(object sender, EventArgs e)
        {
            //暂停不用重复触发
            if (ZmotionCard.m_bPaused == false)
            {
                ZmotionCard.AllAxis_PauseMove();
                Log("自动暂停中...");
            }
            else
            {
                Log("自动已暂停!");
            }
        }
    }
}
