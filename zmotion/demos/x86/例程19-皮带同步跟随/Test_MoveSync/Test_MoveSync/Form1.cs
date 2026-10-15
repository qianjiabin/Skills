using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cszmcaux;
using System.Threading;

namespace Test_MoveSync
{
    public partial class Form1 : Form
    {
        //控件数组定义
        TextBox[] TextUnits = new TextBox[3];
        TextBox[] TextSpeed = new TextBox[3];
        TextBox[] TextAccel = new TextBox[3];



        public IntPtr G_CardHandle;

        public bool m_Regist_IfOpen = false;
        public int m_RegistCount = 0;
        public int m_RegistMode = 1;
        public int m_RegistShow = 0;

        public int m_SyncMode = 0;
        public int iWorkCount = 0;         //已经加工数量



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

             //控件初始化
            TextUnits[0] = text_Units0;
            TextUnits[1] = text_Units1;
            TextUnits[2] = text_Units2;
            TextSpeed[0] = text_Speed0;
            TextSpeed[1] = text_Speed1;
            TextSpeed[2] = text_Speed2;
            TextAccel[0] = text_Accel0;
            TextAccel[1] = text_Accel1;
            TextAccel[2] = text_Accel2;


            Cbox_RigeistMode.Items.Clear();
            Cbox_RigeistMode.Items.Add("103 R0上升沿锁存");
            Cbox_RigeistMode.Items.Add("104 R0下降沿锁存");
            Cbox_RigeistMode.Items.Add("114 R1上升沿锁存");
            Cbox_RigeistMode.Items.Add("115 R2=1下降沿锁存");
            Cbox_RigeistMode.SelectedIndex = 0;

            DataGridView2.Rows.Clear();
            string[] tempstr = new string[2];

            tempstr[0] = "触发次数";
            tempstr[1] = "0";
            DataGridView2.Rows.Add(tempstr);
            for (int i = 1; i < Convert.ToInt32(Text_TabNum.Text); i++)
            {
                tempstr[0] = i.ToString();
                tempstr[1] = "";
                DataGridView2.Rows.Add(tempstr);
            }
        }


        //下拉搜索IP
        private void IPADDRESS_DropDown(object sender, EventArgs e)
        {
            int i, num;
            string[] sArray;
            StringBuilder buffer = new StringBuilder(10240);
            string buff = new string('0', 10240);
            zmcaux.ZAux_SearchEthlist(buffer, 10230, 200);
            buff = "" + buffer;
            sArray = buff.Split(' ');
            num = buff.Split(' ').Length;

            IPADDRESS.Items.Clear();

            IPADDRESS.Items.Insert(0, "192.168.0.11");
            for (i = 0; i < num - 1; i++)
            {
                IPADDRESS.Items.Insert(i + 1, sArray[i].ToString());
            }
        }

        //链接控制器
        private void btn_Connet_Click(object sender, EventArgs e)
        {
            int iret = 0;
            string Adrr = IPADDRESS.Text;
            if ((int)G_CardHandle == 0)
            {
                iret = zmcaux.ZAux_OpenEth(Adrr, out G_CardHandle);
            }

            if (G_CardHandle != (IntPtr)0)
            {

                this.Text = Adrr + "控制器已连接";



                timer1.Enabled = true;
            }
            else
            {
                this.Text = "未连接";
                MessageBox.Show("连接失败");
            }


            //初始化轴基本参数
            for (int iaxis = 0; iaxis < 3; iaxis++)
            {
                iret = zmcaux.ZAux_Direct_SetAtype(G_CardHandle, iaxis, 1);         //轴类型

                iret = zmcaux.ZAux_Direct_SetUnits(G_CardHandle, iaxis, Convert.ToSingle(TextUnits[iaxis].Text));         //脉冲当量
                iret = zmcaux.ZAux_Direct_SetSpeed(G_CardHandle, iaxis, Convert.ToSingle(TextSpeed[iaxis].Text));         //设置轴类型
                iret = zmcaux.ZAux_Direct_SetAccel(G_CardHandle, iaxis, Convert.ToSingle(TextAccel[iaxis].Text));         //设置轴类型
            }

            for (int iaxis = 4; iaxis < 6; iaxis++)             //设定虚拟轴XY轴，同步轨迹时用虚拟轴去做轨迹运动
            {
                iret = zmcaux.ZAux_Direct_SetAtype(G_CardHandle, iaxis, 0);         //轴类型

                iret = zmcaux.ZAux_Direct_SetUnits(G_CardHandle, iaxis, Convert.ToSingle(TextUnits[iaxis -4].Text));         //运动参数与实际XY一致
                iret = zmcaux.ZAux_Direct_SetSpeed(G_CardHandle, iaxis, Convert.ToSingle(TextSpeed[iaxis-4].Text));         //
                iret = zmcaux.ZAux_Direct_SetAccel(G_CardHandle, iaxis, Convert.ToSingle(TextAccel[iaxis-4].Text));         //
            }


        }

        //关闭控制器
        private void btn_Close_Click(object sender, EventArgs e)
        {
            if (G_CardHandle != (IntPtr)0)
            {
                zmcaux.ZAux_Close(G_CardHandle);
                G_CardHandle = (IntPtr)0;
            }

            this.Text = "未连接";
            timer1.Enabled = false;
        }

        //定时器刷新
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (G_CardHandle == (IntPtr)0)
            {
                MessageBox.Show("控制器未连接");
            }

            TextBox[] txBox = new TextBox[6] { text_Dpos0, text_Dpos1, text_Dpos2, text_Mpos0, text_Mpos1, text_Mpos2};

            int iret = 0;
            float[] fDpos=new float[4];
            for(int iaxis = 0;iaxis <3;iaxis ++)
            {
                iret = zmcaux.ZAux_Direct_GetDpos(G_CardHandle, iaxis, ref fDpos[iaxis]);
                txBox[iaxis].Text = fDpos[iaxis].ToString();

                iret = zmcaux.ZAux_Direct_GetMpos(G_CardHandle, iaxis, ref fDpos[iaxis]);
                txBox[iaxis + 3].Text = fDpos[iaxis].ToString();
            }


        }


        //点动按下
        private void Btn_fwd0_MouseDown(object sender, MouseEventArgs e)
        {
            if (G_CardHandle == (IntPtr)0)
            {
                MessageBox.Show("控制器未连接");
                return;
            }

            int iAxisnum = 0, idir = 1;
            string btnName = (sender as Button).Name;

            if (btnName == "Btn_Fwd0") { iAxisnum = 0; idir = 1; }
            if (btnName == "Btn_Fwd1") { iAxisnum = 1; idir = 1; }
            if (btnName == "Btn_Fwd2") { iAxisnum = 2; idir = 1; }
            if (btnName == "Btn_Rev0") { iAxisnum = 0; idir = -1; }
            if (btnName == "Btn_Rev1") { iAxisnum = 1; idir = -1; }
            if (btnName == "Btn_Rev2") { iAxisnum = 2; idir = -1; }

            int iret = zmcaux.ZAux_Direct_Single_Vmove(G_CardHandle, iAxisnum, idir);

        }

        //点动松开
        private void Btn_fwd0_MouseUp(object sender, MouseEventArgs e)
        {

            if (G_CardHandle == (IntPtr)0)
            {
                MessageBox.Show("控制器未连接");
                return;
            }

            int iAxisnum = 0;
            string btnName = (sender as Button).Name;

            if (btnName == "Btn_Fwd0") { iAxisnum = 0;}
            if (btnName == "Btn_Fwd1") { iAxisnum = 1;}
            if (btnName == "Btn_Fwd2") { iAxisnum = 2;}
            if (btnName == "Btn_Rev0") { iAxisnum = 0;}
            if (btnName == "Btn_Rev1") { iAxisnum = 1;}
            if (btnName == "Btn_Rev2") { iAxisnum = 2;}

            int iret = zmcaux.ZAux_Direct_Single_Cancel(G_CardHandle,iAxisnum,3);
        }

        private void Btn_Zero_Click(object sender, EventArgs e)
        {

            if (G_CardHandle == (IntPtr)0)
            {
                MessageBox.Show("控制器未连接");
                return;
            }

            int iret = 0;
            for (int iaxis = 0; iaxis < 3; iaxis++)
            {
                iret = zmcaux.ZAux_Direct_SetDpos(G_CardHandle, iaxis, 0);
                iret = zmcaux.ZAux_Direct_SetMpos(G_CardHandle, iaxis, 0);
            }
        }

        private void text_Units0_TextChanged(object sender, EventArgs e)
        {
            if (G_CardHandle == (IntPtr)0)
            {
                MessageBox.Show("控制器未连接");
                return;
            }

            string EditName = (sender as TextBox).Name;

            for (int iaxis = 0; iaxis < 3; iaxis++)
            {
                if (EditName == TextUnits[iaxis].Name)
                {
                    int iret = zmcaux.ZAux_Direct_SetUnits(G_CardHandle, iaxis, Convert.ToSingle(TextUnits[iaxis].Text));

                    if (iaxis == 0 || iaxis == 1)           //虚拟轴XY跟着修改
                    {
                        iret = zmcaux.ZAux_Direct_SetUnits(G_CardHandle, iaxis+4, Convert.ToSingle(TextUnits[iaxis].Text));
                    }
                    return;
                }
            }

        }

        private void text_Speed0_TextChanged(object sender, EventArgs e)
        {
            if (G_CardHandle == (IntPtr)0)
            {
                MessageBox.Show("控制器未连接");
                return;
            }

            string EditName = (sender as TextBox).Name;

            for (int iaxis = 0; iaxis < 3; iaxis++)
            {
                if (EditName == TextSpeed[iaxis].Name)
                {
                    int iret = zmcaux.ZAux_Direct_SetSpeed(G_CardHandle, iaxis, Convert.ToSingle(TextSpeed[iaxis].Text));
                    if (iaxis == 0 || iaxis == 1)           //虚拟轴XY跟着修改
                    {
                        iret = zmcaux.ZAux_Direct_SetSpeed(G_CardHandle, iaxis + 4, Convert.ToSingle(TextSpeed[iaxis].Text));
                    }
                    return;
                }
            }
        }

        private void text_Accel0_TextChanged(object sender, EventArgs e)
        {
            if (G_CardHandle == (IntPtr)0)
            {
                MessageBox.Show("控制器未连接");
                return;
            }

            string EditName = (sender as TextBox).Name;

            for (int iaxis = 0; iaxis < 3; iaxis++)
            {
                if (EditName == TextAccel[iaxis].Name)
                {
                    int iret = zmcaux.ZAux_Direct_SetAccel(G_CardHandle, iaxis, Convert.ToSingle(TextAccel[iaxis].Text));
                    if (iaxis == 0 || iaxis == 1)           //虚拟轴XY跟着修改
                    {
                        iret = zmcaux.ZAux_Direct_SetAccel(G_CardHandle, iaxis + 4, Convert.ToSingle(TextAccel[iaxis].Text));
                    }
                    return;
                }
            }
        }

        //启动连续锁存
        private void Btn_StrartRegist_Click(object sender, EventArgs e)
        {
            if (G_CardHandle == (IntPtr)0)
            {
                MessageBox.Show("未链接到控制器!", "提示");
                return;
            }

            int iret = 0;
            if (m_Regist_IfOpen == false)
            {
                m_RegistCount = 0;

                int ReglistListSel = Cbox_RigeistMode.SelectedIndex;
                if (ReglistListSel == 0)
                {
                    m_RegistMode = 103;
                }
                else if (ReglistListSel == 1)
                {
                    m_RegistMode = 104;
                }
                else if (ReglistListSel == 2)
                {
                    m_RegistMode = 114;
                }
                else if (ReglistListSel == 3)
                {
                    m_RegistMode = 115;
                }
                iret = zmcaux.ZAux_Direct_CycleRegist(G_CardHandle, 2, m_RegistMode, Convert.ToInt32(Text_TabStart.Text), Convert.ToInt32(Text_TabNum.Text));
                timer2.Start();

                m_Regist_IfOpen = true;
                Cbox_RigeistMode.Enabled = false;
                Btn_StrartRegist.Text = "停止锁存";

            }
            else
            {
                timer2.Stop();
                m_Regist_IfOpen = false;
                Cbox_RigeistMode.Enabled = true;
                Btn_StrartRegist.Text = "启动锁存";
                m_RegistCount = 0; 
                m_RegistShow = 0;
            }
        }

        //锁存状态扫描
        private void timer2_Tick(object sender, EventArgs e)
        {
            int iret = 0;
			float [] MarkNum = new float[2];
			float [] RegistPos = new float[1000];


            iret = zmcaux.ZAux_Direct_GetTable(G_CardHandle, Convert.ToInt32(Text_TabStart.Text), 1, MarkNum);				//获取锁存触发次数
            m_RegistCount = (int)MarkNum[0];
            this.DataGridView2.Rows[0].Cells[1].Value = m_RegistCount.ToString();

            //显示到列表
            if (m_RegistCount > m_RegistShow)                   //锁存数量大于显示
            {
                int iNum= m_RegistCount - m_RegistShow;

                iret = zmcaux.ZAux_Direct_GetTable(G_CardHandle, Convert.ToInt32(Text_TabStart.Text)+ 1 + m_RegistShow, iNum, RegistPos);				//获取锁存触发次数

                for(int i = 0;i<iNum;i++)
                {
                  this.DataGridView2.Rows[m_RegistShow + i + 1].Cells[1].Value = RegistPos[i].ToString();
                }
                m_RegistShow = m_RegistCount;

            }
            else if (m_RegistCount < m_RegistShow)                             //锁存循环溢出
            {
                int iNum = Convert.ToInt32(Text_TabNum.Text) - m_RegistShow - 1;

                iret = zmcaux.ZAux_Direct_GetTable(G_CardHandle, Convert.ToInt32(Text_TabStart.Text) + 1 + m_RegistShow, iNum, RegistPos);				//获取锁存触发次数

                for (int i = 0; i < iNum; i++)
                {
                    this.DataGridView2.Rows[m_RegistShow + i + 1].Cells[1].Value = RegistPos[i].ToString();
                }
                m_RegistShow = 0;   
            }



          }

       
        private void Text_TabNum_TextChanged(object sender, EventArgs e)
        {
            DataGridView2.Rows.Clear();
            string[] tempstr = new string[2];
            tempstr[0] = "触发次数";
            tempstr[1] = "0";
            DataGridView2.Rows.Add(tempstr);
            for (int i = 1; i < Convert.ToInt32(Text_TabNum.Text); i++)
            {
                tempstr[0] = i.ToString();
                tempstr[1] = "";
                DataGridView2.Rows.Add(tempstr);
            }
        }

        //跟随模式
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                m_SyncMode = 0;
                checkBox2.Checked = false;
            }
            else
            {
                m_SyncMode = 1;
                checkBox2.Checked = true;
            }


        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                m_SyncMode = 1;
                checkBox1.Checked = false;
            }
            else
            {
                m_SyncMode = 0;
                checkBox1.Checked = true;
            }
        }


        //皮带轴转动
        private void BtnVmoveR_Click(object sender, EventArgs e)
        {
            if (G_CardHandle == (IntPtr)0)
            {
                MessageBox.Show("控制器未连接");
                return;
            }

             int iret = 0;
            if(BtnVmoveR.Text == "启动皮带轴")
            {
                iret   = zmcaux.ZAux_Direct_Single_Vmove(G_CardHandle, 2, 1);
                BtnVmoveR.Text = "停止皮带轴";
            }
            else
            {
                iret   = zmcaux.ZAux_Direct_Single_Cancel(G_CardHandle, 2, 2);
                BtnVmoveR.Text = "启动皮带轴";
            }

        }


       //启动同步跟随
        private void Btn_Start_Click(object sender, EventArgs e)
        {
            if (G_CardHandle == (IntPtr)0)
            {
                MessageBox.Show("控制器未连接");
                return;
            }

            BtnVmoveR_Click(sender,e);

            Thread MoveThread = new Thread(new ThreadStart(SubMoveSync));

            if (Btn_Start.Text == "启动同步")
            {
                iWorkCount = 0;
                Btn_Start.Text = "停止同步";
                MoveThread.Start();
            }
            else
            {
                Btn_Start.Text = "启动同步";
                MoveThread.Abort();
                zmcaux.ZAux_Direct_Rapidstop(G_CardHandle, 2);
            }
        }

        //同步运动线程
        public  void SubMoveSync()
        {
            int iret = 0;
            int []iAxisList = new int[2]{0,1} ;


            int []iTime = new int[3];
            iTime[0] = Convert.ToInt32(TextAccTime.Text);
            iTime[1] = Convert.ToInt32(TextSyncTime.Text);
            iTime[2] = Convert.ToInt32(TextBackTime.Text);

            float []fWaitPos = new float[2];
            fWaitPos[0] = Convert.ToSingle (TextXpos.Text);
            fWaitPos[1] = Convert.ToSingle(TextYpos.Text);
            float fOffPos= Convert.ToSingle(TextOffpos.Text);



            float fPdAxisPos = 0;       //当前皮带轴位置
            float[] fMakrPos =  new float[2];          //当前加工产品锁存编码器的位置
            int iMaxNum = Convert.ToInt32(Text_TabNum.Text);

            float imode = 0;
            if (radioBtnX.Checked)
            {
                imode = 0 + (float)(Convert.ToSingle(TextAngle.Text) / 180.0 * Math.PI);        //X方向跟随
            }
            else
            {
                imode = 10 + (float)(Convert.ToSingle(TextAngle.Text) / 180.0 * Math.PI);       //Y方向跟随  
            }

            if (m_SyncMode == 1)                //同步轨迹运动
            {
                iret = zmcaux.ZAux_Direct_SetMerge(G_CardHandle, 4, 1);           //打开虚拟X轴连续插补
                iret = zmcaux.ZAux_Direct_Single_Addax(G_CardHandle,0,4);           //虚拟4号轴X轴的运动叠加到实际X轴
                iret = zmcaux.ZAux_Direct_Single_Addax(G_CardHandle, 1, 5);           //虚拟5号轴Y轴的运动叠加到实际Y轴

            }

            while(true)
            {


                    if( (m_RegistCount != 0) && (iWorkCount <iMaxNum))              //锁存 已经触发加工数量小于总锁存数
                    {
                        iret = zmcaux.ZAux_Direct_GetTable(G_CardHandle, Convert.ToInt32(Text_TabStart.Text) + 1 + iWorkCount, 1, fMakrPos);	//获取当前准备加工的锁存位置

                    }

                    else if (iWorkCount > iMaxNum)    //锁存坐标已经溢出，数据保存在锁存前面
                    {
                        iWorkCount = iWorkCount - m_RegistCount;        //从下个循环开始取值
                        if (iWorkCount < m_RegistCount)
                        {
                            iret = zmcaux.ZAux_Direct_GetTable(G_CardHandle, Convert.ToInt32(Text_TabStart.Text) + 1 + iWorkCount, 1, fMakrPos);	//获取当前准备加工的锁存位置
                        }
                        else
                        {
                            continue;
                        }

                    }

                    //锁存事件未触发
                    if (m_RegistCount == 0 || m_RegistCount == iWorkCount)      
                    {
                        continue;
                    }

                    //等待传送带位置运动超过开始跟随位置
                    do
                    {
                        iret = zmcaux.ZAux_Direct_GetMpos(G_CardHandle, 2, ref fPdAxisPos);            //获取当前编码轴位置

                    } while (fPdAxisPos < fOffPos + fMakrPos[0]);

                    iret = zmcaux.ZAux_Direct_MoveSync(G_CardHandle, imode, iTime[0], fMakrPos[0] + fOffPos, 2, 2, iAxisList, fWaitPos);      //同步启动加速段，

                    if (m_SyncMode == 1)                //同步轨迹运动
                    {
                        SubVirAxisMove();               //调用虚拟轴轨迹
                        iret = zmcaux.ZAux_Direct_MoveTable(G_CardHandle, (uint)iAxisList[0], 0, (float)1);      //写TABLE数据到实际轴缓冲区，速度同步后修改TABLE触发虚拟轴
                        if (iret != 0)
                        {
                            MessageBox.Show("ZAux_Direct_MoveTable失败 错误码：" + iret.ToString());
                            continue;
                        }

                        iret = zmcaux.ZAux_Direct_MoveSync(G_CardHandle, imode, iTime[1], fMakrPos[0] + fOffPos, 2, 2, iAxisList, fWaitPos);      //同步启动匀速速段，匀速时间

                        //同步结束
                        iret = zmcaux.ZAux_Direct_MoveTable(G_CardHandle, (uint)iAxisList[0], 0, (float)0);      //写TABLE数据到实际轴缓冲区，速度同步后修改TABLE触发虚拟轴
                        //iret = zmcaux.ZAux_Direct_MoveCancel(G_CardHandle, iAxisList[0], 4, 2);                 //运动完成强制停止未完成的虚拟轴运动

                    }
                    else
                    {
                        iret = zmcaux.ZAux_Direct_MoveSync(G_CardHandle, imode, iTime[1], fMakrPos[0] + fOffPos, 2, 2, iAxisList, fWaitPos);      //同步启动匀速速段，匀速时间
                    }




                    iret = zmcaux.ZAux_Direct_MoveSync(G_CardHandle, imode, iTime[2], 0, -1, 2, iAxisList, fWaitPos);      //结束同步走到待机位置

                    int Axisidle = 0;
                    do
                    {
                        iret = zmcaux.ZAux_Direct_GetIfIdle(G_CardHandle, iAxisList[0], ref Axisidle);            //等待主轴跟随完毕

                    } while (Axisidle == 0);

                    iWorkCount++;

            }
        }


        //虚拟轴轨迹运动
        public void SubVirAxisMove()
        {

            float [,] fPosList = new float[4,2]{{100,0},{0,100},{-100,0},{0,-100}};
            float[] fPosList1 = new float[2] ;
            int iret = 0;
            int[] iVirAxisList = new int[2] { 4, 5 };
            iret = zmcaux.ZAux_Direct_SetSpeed(G_CardHandle, iVirAxisList[0], Convert.ToSingle(TextMoveSp.Text));
            iret = zmcaux.ZAux_Direct_SetAccel(G_CardHandle, iVirAxisList[0], Convert.ToSingle(TextMoveAcc.Text));

            iret = zmcaux.ZAux_Direct_MoveWait(G_CardHandle,(uint)iVirAxisList[0],"TABLE",0,0,(float)1);            //等待跟随实轴达到速度同步 TABLE(0) = 1  

            for (int i = 0; i < 4; i++)
            {
                fPosList1[0] = fPosList[i,0];
                fPosList1[1] = fPosList[i,1];
                iret = zmcaux.ZAux_Direct_Move(G_CardHandle, 2, iVirAxisList, fPosList1);
            }

            iret = zmcaux.ZAux_Direct_MoveTable(G_CardHandle, (uint)iVirAxisList[0], 0, (float)0);      //运动完成重置TABLE(0) = 0
        }

        private void Btn_TestMove_Click(object sender, EventArgs e)
        {
            int iret = 0;
            iret = zmcaux.ZAux_Direct_Single_Addax(G_CardHandle, 0, 4);           //虚拟4号轴X轴的运动叠加到实际X轴
            iret = zmcaux.ZAux_Direct_Single_Addax(G_CardHandle, 1, 5);           //虚拟5号轴Y轴的运动叠加到实际Y轴

            float[,] fPosList = new float[4, 2] { { 100, 0 }, { 0, 100 }, { -100, 0 }, { 0, -100 } };
            float[] fPosList1 = new float[2];
            int[] iVirAxisList = new int[2] { 4, 5 };
          
            for (int i = 0; i < 4; i++)
            {
                fPosList1[0] = fPosList[i, 0];
                fPosList1[1] = fPosList[i, 1];
                iret = zmcaux.ZAux_Direct_Move(G_CardHandle, 2, iVirAxisList, fPosList1);
            }

        }





    }
}
