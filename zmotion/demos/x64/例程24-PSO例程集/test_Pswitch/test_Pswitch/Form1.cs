using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using cszmcaux;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace test_Pswitch
{   
    //这里添加变量和初始化
    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号
        public int ModePara1 = 0;
        public int ModePara2 = 0;
        public int ModePara3 = 0;
        public int ModePara4 = 0;
        public int ModePara5 = 0;
        public float ModePara6 = 0.0f;
        public float ModePara7 = 0.0f;
        public float ModePara8 = 0.0f;

        public float m_Start_Pos = 0.0f;
        public float m_End_Pos = 0.0f;

	    public int m_Timer_Num = 0;
        public int m_Timer_Valid = 0;
	    public int m_Timer_Cycle = 0;
	    public bool m_POS_IfOpen = true;
	    public bool m_Timer_IfOpen = true;

        //是否打开锁存
	    public bool m_Regist_IfOpen = false;
	    public int iTotalPoint = 0;
	    public int m_RegistAxis = 0;
	    public int m_RegistCount = 0;
	    public int RegistMode = 0;
        public int PsoMode = 7;

        public int AxisNum = 0;

        public float[] fPointPos = new float[1000];
        
        enum ParaName_id { opnum , tablestart, tableend,opstate, direction , vectstart,
                           repes , cycledis , ondis , ophwtimeus , maxerr , num, ophwtimes , hwcyctimeus}
        public string[] ParaName = { "输出口编号：","起始点编号：","结束点编号：","起点输出状态：",
                                     "比较方向：","起始矢量坐标：","重复周期：","周期距离：","有效状态的距离：",
                                     "脉冲时间：","脉冲偏差：","比较点个数：","脉冲个数","脉冲周期"};
        

        //增加元件

        public Form1()
        {
            InitializeComponent();
            Ini_Pso();
        }
        //添加一个pso模式的下拉框
        //锁存模式下拉列表的初始化
        private void Form1_Load(object sender, EventArgs e)             //初始化
        {
            ComboBox1.Items.Clear();
            ComboBox1.Items.Add("1 Z脉冲上升沿锁存");
            ComboBox1.Items.Add("2 Z脉冲下降沿锁存");
            ComboBox1.Items.Add("3 R0上升沿锁存");
            ComboBox1.Items.Add("4 R0下降沿锁存");
            ComboBox1.Items.Add("14 R1上升沿锁存");
            ComboBox1.Items.Add("15 R1下降沿锁存");
            ComboBox1.Items.Add("18 R2上升沿锁存");
            ComboBox1.Items.Add("19 R2下降沿锁存");
            ComboBox1.Items.Add("20 R3上升沿锁存");
            ComboBox1.Items.Add("21 R3下降沿锁存");
            ComboBox1.SelectedIndex = 0;
            PsoSele.SelectedIndex = 0;
            start_add();
        }


        private void C_Open_Eth_Click(object sender, EventArgs e)           //以太网连接控制器
        {
            int ret = zmcaux.ZAux_FastOpen(5, "LOCAL", 1000, out g_handle);
            if (ret == 0)
            {
                label24.Text = "已链接";
                timer1.Start();
                label24.BackColor = Color.Green;
                MessageBox.Show("控制器链接成功!", "提示");
            }
            else
            {
                MessageBox.Show("链接失败,请选择正确的LOCAL！");
            }
        }

        private void C_Close_Card_Click(object sender, EventArgs e)             //断开连接
        {
            //断开链接
            timer1.Enabled = false;
            timer2.Enabled = false;
            zmcaux.ZAux_Close(g_handle);
            g_handle = (IntPtr)0;
            this.Text = "未连接";
            label24.Text = " 未连接";
            label24.BackColor = Color.Red;
        }



        private void start_add()            //初始化DataGridView数据
        {   string[] tempstr = new string[2];
            int i;
            float tempvalue = 0;
            for (i = 0; i < 3; i++)
            {
                iTotalPoint++;
                tempvalue = 50 * (i + 1);
                tempstr[0] = i.ToString();
                tempstr[1] = tempvalue.ToString();
                DataGridView1.Rows.Add(tempstr);
            }
        }

        //将屏幕上的操作转化为软件中的变量
        private void timer1_Tick(object sender, EventArgs e)        //刷新状态显示
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                label24.Text = " 未连接";
                label24.BackColor = Color.Red;
                return;
            }
     
            if (RadioButton1.Checked == true)       //位置比较是否开启
                m_POS_IfOpen = true;
            if (RadioButton2.Checked == true)
                m_POS_IfOpen = false;
            if (RadioButton4.Checked == true)       //定时器输出反转是否开启
                m_Timer_IfOpen = false;
            if (RadioButton3.Checked == true)
                m_Timer_IfOpen = true;

            //状态监控界面代码
            float Axis0Dpos = 0;
            float Axis1Dpos = 0;
            float Axis2Dpos = 0;
            float Axis3Dpos = 0;
            int iret = 0;
           

            iret = zmcaux.ZAux_Direct_GetDpos(g_handle, 0, ref Axis0Dpos);
            iret = zmcaux.ZAux_Direct_GetDpos(g_handle, 1, ref Axis1Dpos);
            iret = zmcaux.ZAux_Direct_GetDpos(g_handle, 2, ref Axis2Dpos);
            iret = zmcaux.ZAux_Direct_GetDpos(g_handle, 3, ref Axis3Dpos);
            
            label_dpos0.Text = "轴" + 0.ToString() + "坐标：  " + Axis0Dpos.ToString();
            label_dpos1.Text = "轴" + 1.ToString() + "坐标：  " + Axis1Dpos.ToString();
            label_dpos2.Text = "轴" + 2.ToString() + "坐标：  " + Axis2Dpos.ToString();
            label_dpos3.Text = "轴" + 3.ToString() + "坐标：  " + Axis3Dpos.ToString();

            UInt32 Op0_status = 0;
            UInt32 Op1_status = 0;
            UInt32 Op2_status = 0;
            UInt32 Op3_status = 0;
            
            iret = zmcaux.ZAux_Direct_GetOp(g_handle, 0, ref Op0_status);
            iret = zmcaux.ZAux_Direct_GetOp(g_handle, 1, ref Op1_status);
            iret = zmcaux.ZAux_Direct_GetOp(g_handle, 2, ref Op2_status);
            iret = zmcaux.ZAux_Direct_GetOp(g_handle, 3, ref Op3_status);

            if (Op0_status == 1)
                op0.BackColor = Color.LimeGreen;
            else 
                op0.BackColor = Color.Red;

            if (Op1_status == 1)
                op1.BackColor = Color.LimeGreen;
            else
                op1.BackColor = Color.Red;

            if (Op2_status == 1)
                op2.BackColor = Color.LimeGreen;
            else
                op2.BackColor = Color.Red;

            if (Op3_status == 1)
                op3.BackColor = Color.LimeGreen;
            else
                op3.BackColor = Color.Red;

        }
        //这里修改更新参数
        private void updata_value()                 //更新参数
        {
            ModePara1 = Convert.ToInt32(Para1.Text);
            ModePara2 = Convert.ToInt32(Para2.Text);
            ModePara3 = Convert.ToInt32(Para3.Text);
            ModePara4 = Convert.ToInt32(Para4.Text);
            ModePara5 = Convert.ToInt32(Para5.Text);
            ModePara6 = Convert.ToInt32(Para6.Text);
            ModePara7 = Convert.ToInt32(Para7.Text);
            ModePara8 = Convert.ToInt32(Para8.Text);
            m_Timer_Cycle = Convert.ToInt32(TextBox10.Text);
            m_Timer_Valid = Convert.ToInt32(TextBox9.Text);
            m_Timer_Num = Convert.ToInt32(TextBox8.Text);
            m_Start_Pos = Convert.ToSingle(Para7.Text);//没有开始点坐标和结束点坐标了 只有参数 还有移动距离参数
            m_End_Pos = Convert.ToSingle(Distance0.Text);
            m_RegistAxis = Convert.ToInt32(TextBox1.Text);
            
            for (int i = 0; i < iTotalPoint; i++)
                 fPointPos[i] = Convert.ToSingle(DataGridView1.Rows[i].Cells["位置"].Value);
           
           
       
        }

        private void Button2_Click(object sender, EventArgs e)          //核心代码 运动按钮
        {

            updata_value();

            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }

            int iret = 0;
            AxisNum = 0;

            //获取轴数组和轴个数
            foreach (Control item in this.Controls)
                if (item is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)item;
                    if(checkBox.CheckState == CheckState.Checked)
                        AxisNum++;
                }

            if (AxisNum == 0)
            {
                MessageBox.Show("没选轴!", "提示");
                return;
            }


            int[] axislist = new int[AxisNum];
            float[] Distancelist = new float[AxisNum];

            foreach(int item in axislist)
                axislist[iret++] = -1;

            foreach (Control item in this.Controls)
                if (item is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)item;
                    if (checkBox.CheckState == CheckState.Checked)
                    {
                       int axis = Convert.ToInt32( checkBox.Name.Substring(checkBox.Name.Length - 1, 1));
                       int index = Array.IndexOf(axislist, -1);
                       axislist[index] = axis;

                       string DistanceName = "Distance" + axis.ToString();
                       Distancelist[index] = float.Parse(((System.Windows.Forms.TextBox)this.Controls.Find(DistanceName, false)[0]).Text);
                       
                    }
                }

            for(int i = 0;i<3;i++)
                iret = zmcaux.ZAux_Direct_SetOp(g_handle, i, 0);

            iret = zmcaux.ZAux_SetTraceFile(3, "预留");
            iret = zmcaux.ZAux_Direct_Base(g_handle, AxisNum, axislist);
           
            //这里走一个遍历赋值
            foreach (int item in axislist)
            {
                iret = zmcaux.ZAux_Direct_SetUnits(g_handle, item, 100);
                iret = zmcaux.ZAux_Direct_SetSpeed(g_handle, item, 100);
                iret = zmcaux.ZAux_Direct_SetAccel(g_handle, item, 2000);
                iret = zmcaux.ZAux_Direct_HwPswitch2(g_handle, item, 2, 0, 0, 0, 0, 0, 0,0,0);     //清除前面的比较输出指令
            }
                
                
            iret = zmcaux.ZAux_Direct_HwTimer(g_handle, 2, 0, 0, 0, 0, 0);                      //关闭硬件定时器

            //硬件定时器设置
            int tempoutstatus = 0;
            if (ModePara2 == 0)
                tempoutstatus = 1;
            else
                tempoutstatus = 0;

            if (m_Timer_IfOpen == false)
            {
                iret = zmcaux.ZAux_Direct_HwTimer(g_handle, 2, m_Timer_Cycle, m_Timer_Valid, m_Timer_Num, tempoutstatus, ModePara1);
                if (iret != 0)
                {
                    string tempstr;
                    tempstr = "HwTimer失败 返回值：" + iret.ToString();
                    MessageBox.Show(tempstr, "提示");
                    return;
                }
            }
            else
            {
                iret = zmcaux.ZAux_Direct_HwTimer(g_handle, 0, m_Timer_Cycle, m_Timer_Valid, m_Timer_Num, tempoutstatus, ModePara1);
                if (iret != 0)
                {
                    string tempstr;
                    tempstr = "HwTimer失败 返回值：" + iret.ToString();
                    MessageBox.Show(tempstr, "提示");
                    return;
                }
            }

            //HwPswitch设置
            if (m_POS_IfOpen)          //比较完成一次后需要重新调用HwPswitch  
            {

             
                switch(PsoMode)
                {
                    case 1:
                    case 3:
                        iret = zmcaux.ZAux_Direct_SetTable(g_handle, ModePara3, ModePara4 - ModePara3 + 1, fPointPos);          //将比较点填入TABLE
                        break;
                    case 25:
                    case 26:
                        iret = zmcaux.ZAux_Direct_SetTable(g_handle, ModePara5, ModePara4 * 2 , fPointPos);
                        break;
                    case 35:
                    case 36:
                        iret = zmcaux.ZAux_Direct_SetTable(g_handle, ModePara5, ModePara4 * 3 , fPointPos);
                        break;
                }

                if (iret != 0)
                {
                    string tempstr;
                    tempstr = "SetTable失败 返回值：" + iret.ToString();
                    MessageBox.Show(tempstr, "提示");
                    return;
                }

                switch (PsoMode)
                {
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7: zmcaux.ZAux_Direct_SetParam(g_handle, "VECTOR_MOVED", 0, 0); break;
                }
     
                switch(PsoMode)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6: 
                        iret = zmcaux.ZAux_Direct_HwPswitch2(g_handle, axislist[0], PsoMode, ModePara1, ModePara2, ModePara3, ModePara4, ModePara5, ModePara6, ModePara7, ModePara8);
                        break;
                    case 7:
                    case 25:
                    case 26:
                    case 35:
                    case 36:
                        iret = ZAux_Direct_HwPswitch2_2D();
                        break;
                }
                iret = zmcaux.ZAux_SetTraceFile(0, "预留");


                if (iret != 0)
                {
                    string tempstr;
                    tempstr = "HwPswitch2失败 返回值：" + iret.ToString();
                    MessageBox.Show(tempstr, "提示");
                    return;
                }
            }
            else
            {
                iret = 0;
                iret = zmcaux.ZAux_Direct_HwPswitch2(g_handle, axislist[0], 2, 0, 0, 0, 0, 0, 0, 0, 0);         //清除比较输出指令
                if (iret != 0)
                {
                    string tempstr;
                    tempstr = "HwPswitch2失败 返回值：" + iret.ToString();
                    MessageBox.Show(tempstr, "提示");
                    return;
                }
            }
            
            zmcaux.ZAux_Trigger(g_handle);
            iret = zmcaux.ZAux_Direct_Move(g_handle, AxisNum,axislist,Distancelist);


        }

        private void Button3_Click(object sender, EventArgs e)              //停止
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int iret = 0;
            iret = zmcaux.ZAux_Direct_Rapidstop(g_handle, 2);
            
            for (int i = 0; i < 3; i++)
                zmcaux.ZAux_Direct_SetDpos(g_handle, i, 0);
           
        }

        private void Button1_Click(object sender, EventArgs e)      //启动锁存
        {
            if (g_handle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }
            int iret = 0;
            if (m_Regist_IfOpen == false)
            {
                m_RegistCount = 0;

                iret = zmcaux.ZAux_Direct_SetAtype(g_handle, m_RegistAxis, 1);				//必须是编码器轴才可以锁存

                int ReglistListSel = ComboBox1.SelectedIndex;
                if (ReglistListSel >= 0 && ReglistListSel <= 3)
                {
                    RegistMode = ReglistListSel + 1;
                }
                else if (ReglistListSel == 4 || ReglistListSel == 5)
                {
                    RegistMode = 10 + ReglistListSel;
                }
                else if (ReglistListSel > 5 || ReglistListSel < 9)
                {
                    RegistMode = 12 + ReglistListSel;
                }
                iret = zmcaux.ZAux_Direct_Regist(g_handle, m_RegistAxis, RegistMode);
                timer2.Start();

                //m_Regist_IfOpen 标志位变true 表示开启了锁存
                //反转按钮的设置
                m_Regist_IfOpen = true;
                //不允许修改锁存轴轴号
                //将启动锁存按钮变成停止锁存
                ComboBox1.Enabled = false;
                Button1.Text = "停止锁存";

            }
            else
            {
                //跟上面相反
                //多了一项 清空锁存列表
                //反转按钮的设置
                timer2.Stop();
                m_Regist_IfOpen = false;
                ComboBox1.Enabled = true;
                Button1.Text = "启动锁存";
                DataGridView2.Rows.Clear();
               
            }
        }

        //第二个定时器内容
        //首先一直在根据锁存类型扫描不同的mark
        private void timer2_Tick(object sender, EventArgs e)                    //定时器刷新
        {
            int iret = 0;
			int MarkStatus = 0;
			float RegistPos=0;
            //根据不同的模式周期读取不同变量
			if(RegistMode >= 0 && RegistMode <= 4)
			{
				iret = zmcaux.ZAux_Direct_GetMark(g_handle,m_RegistAxis,ref MarkStatus);	
			}
			else if(RegistMode >= 14 || RegistMode < 16)
			{
                iret = zmcaux.ZAux_Direct_GetMarkB(g_handle, m_RegistAxis, ref MarkStatus);	
			}
			else if(RegistMode >= 18 || RegistMode < 20)
			{	
				float tempc=0;
                iret = zmcaux.ZAux_Direct_GetParam(g_handle, "MARKC", m_RegistAxis, ref tempc);	
				MarkStatus = (int)tempc;
			}
			else if(RegistMode >= 20 || RegistMode < 22)
			{
				float tempd=0;
                iret = zmcaux.ZAux_Direct_GetParam(g_handle, "MARKD", m_RegistAxis, ref tempd);	
				MarkStatus = (int)tempd;
			}

            //如果读取到值
            if (MarkStatus == -1)
            {
                if (RegistMode >= 0 && RegistMode <= 4)
                {
                    iret = zmcaux.ZAux_Direct_GetRegPos(g_handle, m_RegistAxis, ref RegistPos);				//获取锁存位置
                }
                else if (RegistMode >= 14 || RegistMode < 16)
                {
                    iret = zmcaux.ZAux_Direct_GetRegPosB(g_handle, m_RegistAxis, ref RegistPos);
                }
                else if (RegistMode >= 18 || RegistMode < 20)
                {
                    iret = zmcaux.ZAux_Direct_GetParam(g_handle, "REG_POSC", m_RegistAxis, ref RegistPos);
                }
                else if (RegistMode >= 20 || RegistMode < 22)
                {
                    iret = zmcaux.ZAux_Direct_GetParam(g_handle, "REG_POSD", m_RegistAxis, ref RegistPos);
                }
                //锁存的数量和锁存的数字放到列表中
                string[] tempstr = new string[2];
                tempstr[0] = m_RegistCount.ToString();
                tempstr[1] = RegistPos.ToString();
                DataGridView2.Rows.Add(tempstr);
                m_RegistCount = m_RegistCount + 1;
                //在计时器中再次启动锁存 为什么不使用连续锁存呢
                iret = zmcaux.ZAux_Direct_Regist(g_handle, m_RegistAxis, RegistMode);            //重新触发锁存
            }
            //string temps;
            //temps = "锁存触发状态：" + MarkStatus.ToString() + "  次数： " + m_RegistCount.ToString();
            //Label3.Text = temps;
          }

        //切换后给psomode赋值 修改lab显示的文字和隐藏相应的输入框
        private void PsoSele_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            int index = PsoSele.SelectedIndex;
            //设置模式
            switch (index) 
            {
                case 0: 
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6: { PsoMode = index + 1; } break;
                case 7:
                case 8: { PsoMode = index + 18; } break;
                case 9:
                case 10:{ PsoMode = index + 26; } break;
            }

            Ini_Pso();

           
            switch (PsoMode)
            {
                case 1:
                    Add_Psopara(1, ParaName[(int)ParaName_id.opnum]);
                    Add_Psopara(2, ParaName[(int)ParaName_id.opstate]);
                    Add_Psopara(3, ParaName[(int)ParaName_id.tablestart]);
                    Add_Psopara(4, ParaName[(int)ParaName_id.tableend]);
                    Add_Psopara(5, ParaName[(int)ParaName_id.direction]);
                    break;
                case 2:
                    Ini_Pso();
                    break;
                case 3:
                    Add_Psopara(1, ParaName[(int)ParaName_id.opnum]);
                    Add_Psopara(2, ParaName[(int)ParaName_id.opstate]);
                    Add_Psopara(3, ParaName[(int)ParaName_id.tablestart]);
                    Add_Psopara(4, ParaName[(int)ParaName_id.tableend]);
                    break;
                case 4:
                    Add_Psopara(1, ParaName[(int)ParaName_id.opnum]);
                    Add_Psopara(2, ParaName[(int)ParaName_id.opstate]);
                    Add_Psopara(3, ParaName[(int)ParaName_id.vectstart]);
                    break;
                case 5:
                    Add_Psopara(1, ParaName[(int)ParaName_id.opnum]);
                    Add_Psopara(2, ParaName[(int)ParaName_id.opstate]);
                    Add_Psopara(3, ParaName[(int)ParaName_id.vectstart]);
                    Add_Psopara(4, ParaName[(int)ParaName_id.repes]);
                    Add_Psopara(5, ParaName[(int)ParaName_id.cycledis]);
                    Add_Psopara(6, ParaName[(int)ParaName_id.ondis]);
                    break;
                case 6:
                    Add_Psopara(1, ParaName[(int)ParaName_id.opnum]);
                    Add_Psopara(2, ParaName[(int)ParaName_id.opstate]);
                    Add_Psopara(3, ParaName[(int)ParaName_id.vectstart]);
                    Add_Psopara(4, ParaName[(int)ParaName_id.repes]);
                    Add_Psopara(5, ParaName[(int)ParaName_id.cycledis]);
                    break;
                case 7:
                    Add_Psopara(1, ParaName[(int)ParaName_id.opnum]);
                    Add_Psopara(2, ParaName[(int)ParaName_id.opstate]);
                    Add_Psopara(3, ParaName[(int)ParaName_id.tablestart]);
                    Add_Psopara(4, ParaName[(int)ParaName_id.tableend]);
                    Add_Psopara(5, ParaName[(int)ParaName_id.ophwtimeus]);
                    Add_Psopara(6, ParaName[(int)ParaName_id.ophwtimes]);
                    Add_Psopara(7, ParaName[(int)ParaName_id.hwcyctimeus]);
                    break;
                case 25:
                case 35:
                    Add_Psopara(1, ParaName[(int)ParaName_id.opnum]);
                    Add_Psopara(2, ParaName[(int)ParaName_id.opstate]);
                    Add_Psopara(3, ParaName[(int)ParaName_id.maxerr]);
                    Add_Psopara(4, ParaName[(int)ParaName_id.num]);
                    Add_Psopara(5, ParaName[(int)ParaName_id.tablestart]);
                    break;
                case 26:
                case 36:
                    Add_Psopara(1, ParaName[(int)ParaName_id.opnum]);
                    Add_Psopara(2, ParaName[(int)ParaName_id.opstate]);
                    Add_Psopara(3, ParaName[(int)ParaName_id.maxerr]);
                    Add_Psopara(4, ParaName[(int)ParaName_id.num]);
                    Add_Psopara(5, ParaName[(int)ParaName_id.tablestart]);
                    Add_Psopara(6, ParaName[(int)ParaName_id.ophwtimeus]);
                    Add_Psopara(7, ParaName[(int)ParaName_id.ophwtimes]);
                    Add_Psopara(8, ParaName[(int)ParaName_id.hwcyctimeus]);
                    break;

            }
        }

       private void Ini_Pso()
       {
           Control control = null;
           Find_Item(ref control);
           foreach (Control item in control.Controls)                        
                if (item.Name.IndexOf("Para") != -1)
                    item.Visible = false;  
       }
       private void Add_Psopara(int paraNum,string Name)
       {
            string ParaName = "ParaName" + paraNum.ToString();
            string Para = "Para" + paraNum.ToString();
            Control control = null;
            Find_Item(ref control);
            foreach (Control item in control.Controls)
            {
                if (item.Name.IndexOf(ParaName) != -1){
                    item.Visible = true;
                    item.Text = Name;
                }
                else if (item.Name.IndexOf(Para) != -1)
                    item.Visible = true;
            }

                
        }

       private void Find_Item(ref Control control)
       {
            foreach (Control tmp in this.Controls)
                if (tmp.Name == "GroupBox4")
                    foreach (Control tmp2 in tmp.Controls)
                        if (tmp2.Name == "GroupBox5")
                            control = tmp2;
       }

        private void addRow_Click(object sender, EventArgs e)
        {
            int index = this.DataGridView1.Rows.Add();
            this.DataGridView1.Rows[index].Cells[0].Value = iTotalPoint.ToString();
            this.DataGridView1.Rows[index].Cells[1].Value = 0;
            iTotalPoint++;
        }

        private void deleRow_Click(object sender, EventArgs e)
        {
            if (iTotalPoint > 0)
            {
                DataGridView1.Rows.RemoveAt(DataGridView1.Rows.Count - 2);
                iTotalPoint--;
            }
        }

        
        int ZAux_Direct_HwPswitch2_2D()
        {

            string cmdbuff="";
            string tempbuff = ""; 
            StringBuilder cmdbuffAck = new StringBuilder();
            //生成命令
            
            switch (PsoMode)
            {
                case 7:
                    tempbuff = String.Format("HW_PSWITCH2({0},{1},{2},{3},{4},{5},{6},{7})", PsoMode, ModePara1, ModePara2, ModePara3, ModePara4, ModePara5, ModePara6, ModePara7);
                    break;
                case 25:          
                    tempbuff = String.Format("HW_PSWITCH2({0},{1},{2},{3},{4},{5})", PsoMode, ModePara1, ModePara2, ModePara3, ModePara4, ModePara5);
                    break;
                case 26:
                    tempbuff = String.Format("HW_PSWITCH2({0},{1},{2},{3},{4},{5},{6},{7},{8})", PsoMode, ModePara1, ModePara2, ModePara3, ModePara4, ModePara5, ModePara6, ModePara7, ModePara8);
                    break;
                case 35:
                    tempbuff = String.Format("HW_PSWITCH2({0},{1},{2},{3},{4},{5})", PsoMode, ModePara1, ModePara2, ModePara3, ModePara4, ModePara5);
                    break;
                case 36:
                    tempbuff = String.Format("HW_PSWITCH2({0},{1},{2},{3},{4},{5},{6},{7},{8})", PsoMode, ModePara1, ModePara2, ModePara3, ModePara4, ModePara5, ModePara6, ModePara7, ModePara8);
                    break;

            }
            //调用命令执行函数
            int ret = zmcaux.ZAux_Execute(g_handle, tempbuff, cmdbuffAck, 2048);

            if (cmdbuffAck.Length != 0)
                MessageBox.Show(cmdbuff, "提示");
            

            return ret;
        }


    }
}
