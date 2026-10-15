using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cszmcaux;
namespace Robot_5
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
        public float[] Robot_para = new float[4];
        public float[] Axis_Speed = new float[12];
        public float[] Axis_Acc = new float[12];
        public int Robot_status = 0;
        public int[] button_status = new int[12];


        public int gc_AxisVx = 10;   //虚拟轴

        public int gc_AxisVy = 11;


        public int gc_AxisJx = 0;  

        public int gc_AxisJy = 1;

        public int gc_AxisJr = 2;

        public int Axis;




        public float xPulse=10000;

        public float yPulse=10000;

        public float zPulse=10000;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)             //初始化
        {   int i ;
            for (i = 0; i < 12; i++)
            {
                button_status[i] = 0;
            }
            Robot_para[0] = 10.0f;
            Robot_para[1] = 10000.0f;
            Robot_para[2] = 10000.0f;
            Robot_para[3] = 10000.0f;
            for (i = 0; i < 12; i++)
            {
                Axis_Speed[i] = 100;
                Axis_Acc[i] = 1000;
            }

           GroupBox4.Enabled = false;
           GroupBox3.Enabled = false;

        }





        private void C_Open_Com_Click(object sender, EventArgs e)           //COM口连接控制器
        {
            if (g_handle == (IntPtr)0)
            {
                C_Close_Card_Click(sender, e);              //断开前面的连接   
            }

            UInt32 comid = Convert.ToUInt32(C_COM_ID.Text);
            zmcaux.ZAux_OpenCom(comid, out g_handle);           
            if ((int)g_handle != 0)
            {
                this.Text = "已连接";
                timer1.Enabled = true;
                // C_Move_Axis_TextChanged(sender, e);


                GroupBox3.Enabled = true;


            }
        }

        private void C_Close_Card_Click(object sender, EventArgs e)                 //断开连接
        {
            //断开链接
            timer1.Enabled = false;
            zmcaux.ZAux_Close(g_handle);
            g_handle = (IntPtr)0;
            this.Text = "未连接";
        }

        private void C_Open_Eth_Click(object sender, EventArgs e)               //以太网连接控制器
        {
            if (g_handle == (IntPtr)0)
            {
                C_Close_Card_Click(sender, e);              //断开前面的连接
            }
            zmcaux.ZAux_OpenEth(C_Ip_Address.Text, out g_handle);           //网口连接控制器
            if (g_handle != (IntPtr)0)
            {
                this.Text = "已连接";
                timer1.Enabled = true;

                GroupBox3.Enabled = true;
                //C_Move_Axis_TextChanged(sender, e);
            }
        }

        private void C_Ip_Address_DropDown(object sender, EventArgs e)              //下拉搜索IP
        {
            int i, num;
            string[] sArray;
            StringBuilder buffer = new StringBuilder(10240);
            string buff = "";
            zmcaux.ZAux_SearchEthlist(buffer, 10230, 200);              //搜索IP地址列表
            buff += buffer;
            sArray = buff.Split(' ');
            num = buff.Split(' ').Length;

            C_Ip_Address.Items.Clear();

            C_Ip_Address.Items.Insert(0, "127.0.0.1");
            for (i = 0; i < num - 1; i++)
            {
                C_Ip_Address.Items.Insert(i + 1, sArray[i].ToString());
            }
        }

        private void timer1_Tick(object sender, EventArgs e)                //定时刷新
        {
            update_value();
            int iret = 0;
            float[] fDpos = new float[12];
            int[] iAxisMtype = new int[2];
            string tempstr;
            iret = zmcaux.ZAux_Direct_GetAllAxisPara(g_handle, "dpos", 12, fDpos);
            iret = zmcaux.ZAux_Direct_GetMtype(g_handle, 0, ref iAxisMtype[0]);
            iret = zmcaux.ZAux_Direct_GetMtype(g_handle, 10, ref iAxisMtype[1]);

            TextBox1.Text = "X:" + fDpos[0].ToString() + "  Y:" + fDpos[1].ToString() + "  R:" + fDpos[2].ToString();
          

            if (iAxisMtype[0] == 33)
            { 
                tempstr = "逆解状态";
                Robot_status = 1;
              
            }
            else if (iAxisMtype[1] == 34)
            {
                tempstr = "正解状态";
                Robot_status = 2;
             
            }
            else
            { 
                tempstr = "未映射";
                Robot_status = 0;
             
            }
            TextBox3.Text = tempstr;
        }

        private void update_value()             //更新参数
        { 
            Robot_para[0] = Convert.ToSingle(TextBox4.Text);
            Robot_para[1] = Convert.ToSingle(TextBox5.Text);
            Robot_para[2] = Convert.ToSingle(TextBox6.Text);
            Robot_para[3] = Convert.ToSingle(TextBox7.Text);
          
        }

        private void Button1_Click(object sender, EventArgs e)          //配置修改机械手参数
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int i;
            int iret = 0;
            int iaxis = 0;
            int[] jAxis_Units = { 1000, 1000, 27 };
            int[] VirAxis_Units = { 1000, 1000};      //虚拟XY设置成1000表示精确到0.001MM，旋转轴与实际R轴一致

            //初始化关节轴参数
            for (i = 0; i < 3; i++)
            {
                iret = zmcaux.ZAux_Direct_SetAtype(g_handle, i, 1);
                iret = zmcaux.ZAux_Direct_SetUnits(g_handle, i, jAxis_Units[i]);
                iret = zmcaux.ZAux_Direct_SetSpeed(g_handle, i, 10);
                // iret = zmcaux.ZAux_Direct_SetClutchRate(g_handle, i, 0);             //使用关节轴的速度和加速度限制 虚拟轴速度
            }
            for (i = 0; i < 2; i++)
            {
                iaxis = i + 10;
                iret = zmcaux.ZAux_Direct_SetAtype(g_handle, iaxis, 0);          //设置成虚拟轴
                iret = zmcaux.ZAux_Direct_SetUnits(g_handle, iaxis, VirAxis_Units[i]);
                iret = zmcaux.ZAux_Direct_SetSpeed(g_handle, iaxis, 10);
            }
            iret = zmcaux.ZAux_Direct_SetTable(g_handle, 0, 4, Robot_para);

            GroupBox4.Enabled = true;
            MessageBox.Show("写入成功");
        }

        private void Button3_Click(object sender, EventArgs e)              //机械手逆解
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int iret = 0;
            int[] jAxis_List = { 0, 1};
            int[] VirAxis_List = {10,11,2};
            if (Robot_status != 1)                  //当前不在逆解状态
            {
                iret = zmcaux.ZAux_Direct_Connframe(g_handle, 2, jAxis_List, 11, 0, 3, VirAxis_List);
            }



        }

        private void Button2_Click(object sender, EventArgs e)              //机械手正解
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int iret = 0;
            int[] jAxis_List = { 0, 1,2 };
            int[] VirAxis_List = { 10,11};
            if (Robot_status <= 1 )                  //当前在逆解状态
            {
                iret = zmcaux.ZAux_Direct_Connreframe(g_handle, 2, VirAxis_List, 11, 0, 3, jAxis_List);
            }
        }
        
    
       

        private void button24_Click(object sender, EventArgs e)   //设定速度
        {
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 0, Convert.ToSingle(textBox31.Text));
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 1, Convert.ToSingle(textBox31.Text));
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 2, Convert.ToSingle(textBox31.Text));
            zmcaux.ZAux_Direct_SetAccel(g_handle, 0, Convert.ToSingle(textBox31.Text) * 10);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 1, Convert.ToSingle(textBox31.Text) * 10);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 2, Convert.ToSingle(textBox31.Text) * 10);
            zmcaux.ZAux_Direct_SetDecel(g_handle, 0, Convert.ToSingle(textBox31.Text) * 10);
            zmcaux.ZAux_Direct_SetDecel(g_handle, 1, Convert.ToSingle(textBox31.Text) * 10);
            zmcaux.ZAux_Direct_SetDecel(g_handle, 2, Convert.ToSingle(textBox31.Text) * 10);


            MessageBox.Show("设定成功");
        }






        public void Move(int axisId,int mode)
        {
            if (Robot_status == 1)
            {
                if (axisId == 0)
                {
                    Axis = gc_AxisVx;
                }
                else if (axisId == 1)
                {
                    Axis = gc_AxisVy;
                }
                else if(axisId == 2)
                {
                    Axis = gc_AxisJr;
                }
            }
            else if(Robot_status == 2)
            {
                if (axisId == 0)
                {
                    Axis = gc_AxisJx;
                }
                else if (axisId == 1)
                {
                    Axis = gc_AxisJy;
                }
                else if (axisId == 2)
                {
                    Axis = gc_AxisJr;
                }
            }

            if( mode == 1)
            {
                zmcaux.ZAux_Direct_Single_Vmove(g_handle, Axis, 1);
            }
            else if( mode == -1)
            {
                zmcaux.ZAux_Direct_Single_Vmove(g_handle, Axis, -1);
            }
        }

        //按钮

       

        #region x轴

        private void button23_MouseDown(object sender, MouseEventArgs e) 
        {
            Move(0, 1);
        }

        private void button23_MouseUp(object sender, MouseEventArgs e)   
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, Axis, 2);
        }

        private void button20_MouseDown(object sender, MouseEventArgs e) 
        {
            Move(0, -1);
        }

        private void button20_MouseUp(object sender, MouseEventArgs e)   
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, Axis, 2);
        }



        #endregion

        #region y轴
        private void button19_MouseDown(object sender, MouseEventArgs e)  
        {
            Move(1, 1);
        }

        private void button19_MouseUp(object sender, MouseEventArgs e)   
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, Axis, 2);
        }

        private void button18_MouseDown(object sender, MouseEventArgs e)   
        {
            Move(1, -1);
        }

        private void button18_MouseUp(object sender, MouseEventArgs e)  
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, Axis, 2);
        }

        #endregion

        #region z轴

        private void button21_MouseDown(object sender, MouseEventArgs e)  
        {
            Move(2, 1);
        }

        private void button21_MouseUp(object sender, MouseEventArgs e)  
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, Axis, 2);
        }

        private void button22_MouseDown(object sender, MouseEventArgs e) 
        {
            Move(2, -1);
        }

        private void button22_MouseUp(object sender, MouseEventArgs e)  
        {
            zmcaux.ZAux_Direct_Single_Cancel(g_handle, Axis, 2);
        }

        #endregion



    }
}
