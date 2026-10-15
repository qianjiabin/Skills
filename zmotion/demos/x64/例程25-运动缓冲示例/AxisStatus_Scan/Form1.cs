using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Collections;
using cszmcaux;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.SqlServer.Server;

namespace AxisStatus_Scan
{

    public partial class Form1 : Form
    {
        public IntPtr g_handle;         //链接返回的句柄，可以作为卡号

        Boolean controllerConnected = false;

        public const UInt16 controllerAxisCount = 32;  //AxisStatus检测程序循环检测的轴数

        //轴的停止原因(是否正常停止),可以用作回零标志位,也可用于其他场合debug
        Int32[] axisStopReason = new int[controllerAxisCount];

        //运动缓冲区满了或者其他原因造成运动指令下发不下去, 暂停一点时间再下发, 以防堵塞通信
        int pauseBtweenRetry = 10;

        CancellationTokenSource[] myCancelSource = new CancellationTokenSource[2];

        //运动指令返回值, 定义成全局变量方便界面显示
        int[] moveCmdRet = new int[2];

        int[] axisGroup0 = new int[2] { 0, 1 };
        int[] axisGroup1 = new int[2] { 2, 3 };
        int[] axisGroup1_Conflict = new int[2] { 2, 1 };



        public Form1()
        {
            InitializeComponent();

            myCancelSource[0] = null;
            myCancelSource[1] = null;

        }

        //关闭窗口，关闭链接，释放HANDLE
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //断开链接
            CloseControllerCleanUP();
        }

        private void ConnectControllerBtn_Click(object sender, EventArgs e)
        {
            if (!controllerConnected)
            {
                if (RealControllerRbt.Checked)
                {
                    if (ControllerIPTbox.Text != "")
                    {
                        //zmcaux.ZAux_Close(g_handle);  //先销毁handle,以防控制器断线已经有handle
                        g_handle = (IntPtr)0;
                        //链接真实控制器 
                        int ret = zmcaux.ZAux_OpenEth(ControllerIPTbox.Text, out g_handle);
                        if (ret == 0)
                        {
                            OpenControllerIni();
                        }
                        else
                        {
                            MessageBox.Show("控制器链接失败，请检查IP地址和接线正常!", "警告");
                            ControllerStateLable.Text = "控制器连接失败";
                            ControllerStateLable.ForeColor = Color.Red;
                        }

                    }
                }
                else if (SoftControllerRbn.Checked)
                {
                    //127.0.0.1可以既可以链接仿真器, 也可以链接软控制器, 所以不要同时打开
                    //int ret = zmcaux.ZAux_OpenEth("127.0.0.1", out g_handle);
                    //模式5实际上不需要第二个参数, 但必须给个空字符串
                    int ret = zmcaux.ZAux_FastOpen(5, string.Empty, 1000, out g_handle);
                    if (ret == 0)
                    {
                        OpenControllerIni();
                    }
                    else
                    {
                        MessageBox.Show("RT7连接失败，请检查RT7是否已运行!", "警告");
                        ControllerStateLable.Text = "RT7连接失败";
                        ControllerStateLable.ForeColor = Color.Red;
                    }
                }
            }
        }

        private void OpenControllerIni()
        {
            controllerConnected = true;
            ControllerStateLable.Text = "控制器已连接";
            ControllerStateLable.ForeColor = Color.Black;
            WatchDogTimer.Enabled = true;

            CheckBufferTimer.Enabled = true;
        }

        private void DisconControllerBtn_Click(object sender, EventArgs e)
        {
            CloseControllerCleanUP();
        }

        private void CloseControllerCleanUP()
        {
            zmcaux.ZAux_Close(g_handle);
            g_handle = (IntPtr)0;
            controllerConnected = false;
            WatchDogTimer.Enabled = false;
            ControllerStateLable.Text = "控制器已断开连接";
            ControllerStateLable.ForeColor = Color.Red;

            CheckBufferTimer.Enabled = false;


        }

        //检测axis_stopreason比起axisstatus的好处是可以低频检测, 控制器自身在高频检测
        //凡是电机报警/限位等问题造成有个运动指令没有正常完成, 都会造成stopreason非0
        //注意根据自己的控制器情况修改最大轴数
        //因为轴状态检测放在form线程了,所以尽量不要堵塞form线程,否则请将检测程序放入单独线程中
        private void WatchDogTimer_Tick(object sender, EventArgs e)
        {
            //批量读取轴axisstatus状态
            int ret = ZMCHelperClass.GetMultiParam(g_handle, "Axis_StopReason", controllerAxisCount, axisStopReason);
            if (ret != 0)
            {
                //控制器和PC通信故障
                CloseControllerCleanUP();
            }
            else
            {
                for (int axisNum = 0; axisNum < controllerAxisCount; axisNum++)
                {   //判断是否有常规的错误
                    if ((axisStopReason[axisNum] & (UInt32)ZMCAxisMask.CommFault) != 0)
                    {
                        ControllerStateLable.Text = string.Format("{0}号轴历史报警码:0x{1:X}.请循环发送急停指令(最好拍急停按钮), 电机需重新回零. ", axisNum, axisStopReason[axisNum]);
                        ControllerStateLable.ForeColor = Color.Red;
                        break;

                    }
                }
            }
        }

        //在界面上显示数值的定时器
        private void CheckBufferTimer_Tick(object sender, EventArgs e)
        {
            //为什么多个参数的读取方式不一样, 看着有点混乱, 因为有的已经封装好了, 有现成可调用
            //但不用担心, 用户一般不查询这些参数, 这是只是演示用
            int axisCount = 2;
            float[] moveMark = new float[axisCount];
            int[] moveCurMark = new int[axisCount];
            int[] remainLineBuffer = new int[axisCount];
            int[] remainBuffer = new int[axisCount];
            float[] mType = new float[axisCount];

            zmcaux.ZAux_Direct_GetParam(g_handle, "move_mark", axisGroup0[0], ref moveMark[0]);
            zmcaux.ZAux_Direct_GetParam(g_handle, "move_mark", axisGroup1[0], ref moveMark[1]);
            zmcaux.ZAux_Direct_GetMoveCurmark(g_handle, axisGroup0[0], ref moveCurMark[0]);
            zmcaux.ZAux_Direct_GetMoveCurmark(g_handle, axisGroup1[0], ref moveCurMark[1]);
            zmcaux.ZAux_Direct_GetRemain_LineBuffer(g_handle, axisGroup0[0], ref remainLineBuffer[0]);
            zmcaux.ZAux_Direct_GetRemain_LineBuffer(g_handle, axisGroup1[0], ref remainLineBuffer[1]);
            zmcaux.ZAux_Direct_GetRemain_Buffer(g_handle, axisGroup0[0], ref remainBuffer[0]);
            zmcaux.ZAux_Direct_GetRemain_Buffer(g_handle, axisGroup1[0], ref remainBuffer[1]);

            //mtype等常见轴参数支持多轴批量查询, 尽量使用批量查询, 效率更高
            zmcaux.ZAux_Direct_GetAllAxisPara(g_handle, "mtype", 2, mType);

            MoveMark_Gourp0_Label.Text = moveMark[0].ToString();
            MoveMark_Gourp1_Label.Text = moveMark[1].ToString();
            MoveCurMark_Gourp0_Label.Text = moveCurMark[0].ToString();
            MoveCurMark_Gourp1_Label.Text = moveCurMark[1].ToString();
            RemainLineBuffer_Group0_Label.Text = remainLineBuffer[0].ToString();
            RemainLineBuffer_Group1_Label.Text = remainLineBuffer[1].ToString();
            RemainBuffer_Group0_Label.Text = remainBuffer[0].ToString();
            RemainBuffer_Group1_Label.Text = remainBuffer[1].ToString();
            Mtype_Group0_Label.Text = mType[0].ToString();
            Mtype_Group1_Label.Text = mType[1].ToString();

            MoveCmdRet_Group0_Label.Text = moveCmdRet[0].ToString();
            MoveCmdRet_Group1_Label.Text = moveCmdRet[1].ToString();
            if (moveCmdRet[0] == 0 && moveCmdRet[1] == 0)
            {
                MoveCmdRet_Group0_Label.ForeColor = Color.Green;
                MoveCmdRet_Group1_Label.ForeColor = Color.Green;
            }
            else
            {
                MoveCmdRet_Group0_Label.ForeColor = Color.Red;
                MoveCmdRet_Group1_Label.ForeColor = Color.Red;
            }

        }

        //clear label text
        private void ClearLabelText()
        {
            MoveMark_Gourp0_Label.Text = "NA";
            MoveMark_Gourp1_Label.Text = "NA";
            MoveCurMark_Gourp0_Label.Text = "NA";
            MoveCurMark_Gourp1_Label.Text = "NA";
            RemainLineBuffer_Group0_Label.Text = "NA";
            RemainLineBuffer_Group1_Label.Text = "NA";
            RemainBuffer_Group0_Label.Text = "NA";
            RemainBuffer_Group1_Label.Text = "NA";
            Mtype_Group0_Label.Text = "NA";
            Mtype_Group1_Label.Text = "NA";

            moveCmdRet[0] = 0;
            moveCmdRet[1] = 0;
            MoveCmdRet_Group0_Label.Text = "NA";
            MoveCmdRet_Group1_Label.Text = "NA";
            MoveCmdRet_Group0_Label.ForeColor = Color.Black;
            MoveCmdRet_Group1_Label.ForeColor = Color.Black;
        }

        //停止按钮
        private void StopMoveCmdsAndClearBufferBtn_Click(object sender, EventArgs e)
        {
            if (!controllerConnected)
            {
                MessageBox.Show("请先连接控制器!", "警告");
                return;
            }

            //虽然是暂停, 这样响应最快, 最安全. 因为即使发了rapidstop/cancel, 另外一个线程还在不停生成运动指令, 并不安全 
            zmcaux.ZAux_Direct_MovePause(g_handle, axisGroup0[0], 0);
            zmcaux.ZAux_Direct_MovePause(g_handle, axisGroup1[0], 0);

            //停止另外一个正在发运动指令的线程
            SendCancelTokenOrCancelMotor();

            ClearLabelText();
        }

        //根据当前情况选择是发cancelToken(停止另外一个线程), 
        //还是直接发cancel运动指令(pc程序里的下发运动指令的线程已经停止, 但控制器里的运动缓冲区里可能还有大量运动指令)
        private void SendCancelTokenOrCancelMotor()
        {
            //一定要先判断cancelsource, 如果另外一个发运动指令的线程正在运行中, 那就不要直接发cancel/rapidstop，以防触发注意事项
            //通过canceltoken让那个线程自己停止(那个线程内部会自己发控制器的cancel指令)
            if (myCancelSource[0] != null)
            {
                Debug.WriteLine("调用cancelToken0");
                myCancelSource[0].Cancel();
            }
            else
            {
                Debug.WriteLine("直接对轴0发cancel指令");
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, 0, 2);
            }

            if (myCancelSource[1] != null)
            {
                Debug.WriteLine("调用cancelToken1");
                myCancelSource[1].Cancel();
            }
            else
            {
                //反之, 则可以安全的发cancel
                Debug.WriteLine("直接对轴2发cancel指令");
                zmcaux.ZAux_Direct_Single_Cancel(g_handle, 2, 2);
            }

        }


        //正常多工位工线程并行测试
        private void RunMultiGroupBtn_Click(object sender, EventArgs e)
        {

            if (!controllerConnected)
            {
                MessageBox.Show("请先链接控制器!", "警告");
                return;
            }
            //参数为false, 意思要做正常的不冲突的多组并行运动测试
            RunTwoGroupTest(false);

        }

        //做轴号冲突测试, 联动组2和联动组1用了同一个分轴
        private void MultiGroupConflictBtn_Click(object sender, EventArgs e)
        {
            if (!controllerConnected)
            {
                MessageBox.Show("请先链接控制器!", "警告");
                return;
            }
            //参数为true, 意思要做冲突的多组并行运动测试
            RunTwoGroupTest(true);

        }

        //运行测试之前的一些初始化
        private void RunTwoGroupTest(bool conflictTest)
        {

            if (!Int32.TryParse(MoveCmdCountsTBox.Text, out int moveCmdCounts))
            {
                MessageBox.Show("请输入正确的数值!", "警告");
                return;
            }


            if (!Int32.TryParse(PauseBetweenRetryTBox.Text, out pauseBtweenRetry))
            {
                MessageBox.Show("请输入正确的数值!", "警告");
                return;
            }

            //先停止电机, 以防当前已经有运动指令在执行
            SendCancelTokenOrCancelMotor();

            Debug.WriteLine("等待所有电机进入idle状态...");
            while (true)
            {
                float[] idleState = new float[4];
                zmcaux.ZAux_Direct_GetAllAxisPara(g_handle, "idle", 4, idleState);
                if (idleState[0] != 0 && idleState[1] != 0 && idleState[2] != 0 && idleState[3] != 0)
                {
                    break;
                }
            }

            //轴参数初始化
            AxisParamInit();
            int[] axisList0 = new int[2];
            int[] axisList1 = new int[2];
            if (conflictTest)    //轴号冲突测试
            {
                axisList0 = axisGroup0;
                axisList1 = axisGroup1_Conflict;
            }
            else
            {
                axisList0 = axisGroup0;
                axisList1 = axisGroup1;
            }


            //一定要判断为null后再新建, 重要, 否则另外一个线程可能把它清掉!!!
            Debug.WriteLine("等待cancelSource为null");
            while (true)
            {
                if (myCancelSource[0] == null && myCancelSource[1] == null)
                {
                    break;
                }
            }
            Debug.WriteLine("开始创建多线程多组运动测试....");
            //使用cancelSource, 这种方式响应更快
            myCancelSource[0] = new CancellationTokenSource();
            myCancelSource[1] = new CancellationTokenSource();
            CancellationToken myCancelToken0 = myCancelSource[0].Token;
            CancellationToken myCancelToken1 = myCancelSource[1].Token;
            //第一个联动组
            Task myTask0 = new Task(() => RunSingleGroup(0, axisList0, moveCmdCounts, pauseBtweenRetry, myCancelToken0), myCancelToken0);
            myTask0.Start();
            //第二个联动组
            Task myTask1 = new Task(() => RunSingleGroup(1, axisList1, moveCmdCounts, pauseBtweenRetry, myCancelToken1), myCancelToken1);
            myTask1.Start();

        }


        //运行运动指令的函数, 单独线程运行. 销毁cancelSource只在本线程中操作, 不在其他线程操作.
        //同样, 本线程自己发控制器的cancel指令很安全方便(因为已经能保证后续不会下发任何运动指令了)
        //(在该线程还没停止的情况下在别的线程里发控制器的cancel指令会造成电机停不稳, dpos出错)
        private void RunSingleGroup(int baseGroup, int[] axisList, int moveCmdCounts, int pauseBtweenRetry, CancellationToken cToken)
        {

            float[] movePoint = new float[axisList.Length];
            for (int moveIndex = 0; moveIndex < moveCmdCounts; moveIndex++)
            {
                movePoint = new float[] { moveIndex * 10, moveIndex * 10 };  //随意设置一个数值代表运动坐标

                do  //这里使用一个循环是因为要判断每个运动指令的返回值, 非0就要重试, 很大概率就是因为运动缓冲区满了
                {
                    //如果用户已经发了cancelToken, 退出嵌套的内循环
                    if (cToken.IsCancellationRequested)
                        break;

                    zmcaux.ZAux_Direct_SetForceSpeed(g_handle, axisList[0], 200);
                    //故意赋值给一个全局的数组, 方便在界面显示
                    moveCmdRet[baseGroup] = zmcaux.ZAux_Direct_MoveSp(g_handle, 2, axisList, movePoint);
                    if (moveCmdRet[baseGroup] == 0)
                        break;
                    else
                        //缓冲区满后加一点延时再重试, 不要一直占用通信资源
                        //如果运动距离很短, 需要的时间也很短, 就不要加太大延时, 以防电机运行停顿
                        Thread.Sleep(pauseBtweenRetry);
                } while (true);

                //退出外部循环. 只有用户发了cancelToken, 才能调用控制器的cancel指令. 
                //这里调用控制器的cancel指令就很安全, 不会出现停不稳的情况, 因为已经退出自动下发运动指令的循环了
                if (cToken.IsCancellationRequested)
                {
                    zmcaux.ZAux_Direct_Single_Cancel(g_handle, axisList[0], 2);
                    break;
                }
            }
            //重要, 一定要当前函数(当前线程)结束前depose并且设置为null
            myCancelSource[baseGroup].Dispose();
            myCancelSource[baseGroup] = null;

        }

        //演示搭配move_wait的用法
        //该示例因为考虑到运动缓冲区变满和及时停止运动线程, 所以结构略复杂. 如果想参考简单示例, 可参考Basic手册Move_Wait指令
        private void RunSingleGroupWithMoveWait(int baseGroup, int[] axisList, int moveCmdCounts, int pauseBtweenRetry, CancellationToken cToken)
        {
            //Move_Wait可以等待的条件有很多种, 常用io状态等, 这里使用modbus寄存器, 因为方便模拟测试
            //先初始化modbus寄存器, 以防里面已经有错误数据(注意, modbus寄存器地址不要冲突)
            int modbusRegAddr = 0;
            UInt16[] initialValue = new UInt16[1] { 0 };
            int equareOrMore = 1;   //在move_wait指令中, 1代表>=.
            //先把modbus_reg(0)设置为0(4x地址即modbus_reg寄存器地址)
            zmcaux.ZAux_Modbus_Set4x(g_handle, (UInt16)modbusRegAddr, 1, initialValue);
            //先通过movewait设置等待条件, 这里是等待modbus_reg(0)的数值>=1的时候触发
            zmcaux.ZAux_Direct_MoveWait(g_handle, (uint)axisGroup0[0], "modbus_reg", 0, equareOrMore, 1);

            //后面是正常的运动指令
            float[] movePoint = new float[axisList.Length];
            for (int moveIndex = 0; moveIndex < moveCmdCounts; moveIndex++)
            {
                movePoint = new float[] { moveIndex * 10, moveIndex * 10 };  //随意设置一个数值代表运动坐标

                do  //这里使用一个循环是因为要判断每个运动指令的返回值, 非0就要重试, 很大概率就是因为运动缓冲区满了
                {
                    //如果用户已经发了cancelToken, 退出嵌套的内循环
                    if (cToken.IsCancellationRequested)
                        break;

                    zmcaux.ZAux_Direct_SetForceSpeed(g_handle, axisList[0], 200);
                    //故意赋值给一个全局的数组, 方便在界面显示
                    moveCmdRet[baseGroup] = zmcaux.ZAux_Direct_MoveSp(g_handle, 2, axisList, movePoint);
                    if (moveCmdRet[baseGroup] == 0)
                        break;
                    else
                        //缓冲区满后加一点延时再重试, 不要一直占用通信资源
                        //如果运动距离很短, 需要的时间也很短, 就不要加太大延时, 以防电机运行停顿
                        Thread.Sleep(pauseBtweenRetry);
                } while (true);

                //退出外部循环. 只有用户发了cancelToken, 才能调用控制器的cancel指令. 
                //这里调用控制器的cancel指令就很安全, 不会出现停不稳的情况, 因为已经退出自动下发运动指令的循环了
                if (cToken.IsCancellationRequested)
                {
                    zmcaux.ZAux_Direct_Single_Cancel(g_handle, axisList[0], 2);
                    break;
                }
            }

            //重要, 一定要当前函数(当前线程)结束前depose并且设置为null
            myCancelSource[baseGroup].Dispose();
            myCancelSource[baseGroup] = null;

        }

        //演示搭配move_pause和move_resume的用法
        //该示例因为考虑到运动缓冲区变满和及时停止运动线程, 所以结构略复杂. 如果想参考简单示例, 可参考Basic手册Move_Pause指令
        private void RunSingleGroupWithMovePause(int baseGroup, int[] axisList, int moveCmdCounts, int pauseBtweenRetry, CancellationToken cToken)
        {
            //move_pause必须用模式3, 因为当前可能根本没运动指令, 现在是要准备暂停后续的运动指令
            zmcaux.ZAux_Direct_MovePause(g_handle, axisList[0], 3);

            //后面是正常的运动指令
            float[] movePoint = new float[axisList.Length];
            for (int moveIndex = 0; moveIndex < moveCmdCounts; moveIndex++)
            {
                movePoint = new float[] { moveIndex * 10, moveIndex * 10 };  //随意设置一个数值代表运动坐标

                do  //这里使用一个循环是因为要判断每个运动指令的返回值, 非0就要重试, 很大概率就是因为运动缓冲区满了
                {
                    //如果用户已经发了cancelToken, 退出嵌套的内循环
                    if (cToken.IsCancellationRequested)
                        break;

                    zmcaux.ZAux_Direct_SetForceSpeed(g_handle, axisList[0], 200);
                    //故意赋值给一个全局的数组, 方便在界面显示
                    moveCmdRet[baseGroup] = zmcaux.ZAux_Direct_MoveSp(g_handle, 2, axisList, movePoint);
                    if (moveCmdRet[baseGroup] == 0)
                        break;
                    else
                        //缓冲区满后加一点延时再重试, 不要一直占用通信资源
                        //如果运动距离很短, 需要的时间也很短, 就不要加太大延时, 以防电机运行停顿
                        Thread.Sleep(pauseBtweenRetry);
                } while (true);

                //退出外部循环. 只有用户发了cancelToken, 才能调用控制器的cancel指令. 
                //这里调用控制器的cancel指令就很安全, 不会出现停不稳的情况, 因为已经退出自动下发运动指令的循环了
                if (cToken.IsCancellationRequested)
                {
                    zmcaux.ZAux_Direct_Single_Cancel(g_handle, axisList[0], 2);
                    break;
                }
            }

            //加上move_resume, 前面的运动指令才能继续
            zmcaux.ZAux_Direct_MoveResume(g_handle, axisList[0]);

            //重要, 一定要当前函数(当前线程)结束前depose并且设置为null
            myCancelSource[baseGroup].Dispose();
            myCancelSource[baseGroup] = null;

        }


        //演示搭配move_op的用法, 该示例因为考虑到运动缓冲区变满和及时停止运动线程, 所以结构略复杂. 如果想参考简单示例, 可参考Basic手册Move_OP指令
        private void RunSingleGroupWithMoveOP(int baseGroup, int[] axisList, int opNum, int moveCmdCounts, int pauseBtweenRetry, CancellationToken cToken)
        {

            zmcaux.ZAux_Trigger(g_handle);  //触发示波器, 方便通过示波器观察

            //后面是正常的运动指令
            float[] movePoint = new float[axisList.Length];
            for (int moveIndex = 0; moveIndex < moveCmdCounts; moveIndex++)
            {
                movePoint = new float[] { moveIndex * 10, moveIndex * 10 };  //随意设置一个数值代表运动坐标

                do  //这里使用一个循环是因为要判断每个运动指令的返回值, 非0就要重试, 很大概率就是因为运动缓冲区满了
                {
                    //如果用户已经发了cancelToken, 退出嵌套的内循环
                    if (cToken.IsCancellationRequested)
                        break;

                    zmcaux.ZAux_Direct_SetForceSpeed(g_handle, axisList[0], 200);
                    //故意赋值给一个全局的数组, 方便在界面显示
                    moveCmdRet[baseGroup] = zmcaux.ZAux_Direct_MoveSp(g_handle, 2, axisList, movePoint);
                    if (moveCmdRet[baseGroup] == 0)
                        break;
                    else
                        //缓冲区满后加一点延时再重试, 不要一直占用通信资源
                        //如果运动距离很短, 需要的时间也很短, 就不要加太大延时, 以防电机运行停顿
                        Thread.Sleep(pauseBtweenRetry);
                } while (true);

                //在第五条运动指令后面开启io
                if (moveIndex == 5)
                {
                    do  //这里使用一个循环是因为要判断每个运动指令的返回值, 非0就要重试, 很大概率就是因为运动缓冲区满了
                    {
                        //如果用户已经发了cancelToken, 退出嵌套的内循环
                        if (cToken.IsCancellationRequested)
                            break;
                        //故意赋值给一个全局的数组, 方便在界面显示
                        moveCmdRet[baseGroup] = zmcaux.ZAux_Direct_MoveOp(g_handle, axisList[0], opNum, 1);
                        if (moveCmdRet[baseGroup] == 0)
                            break;
                        else
                            //缓冲区满后加一点延时再重试, 不要一直占用通信资源
                            //如果运动距离很短, 需要的时间也很短, 就不要加太大延时, 以防电机运行停顿
                            Thread.Sleep(pauseBtweenRetry);
                    } while (true);
                }
                else if (moveIndex == 6)  //在第六条运动指令之后关闭io
                {
                    do  //这里使用一个循环是因为要判断每个运动指令的返回值, 非0就要重试, 很大概率就是因为运动缓冲区满了
                    {
                        //如果用户已经发了cancelToken, 退出嵌套的内循环
                        if (cToken.IsCancellationRequested)
                            break;
                        //故意赋值给一个全局的数组, 方便在界面显示
                        moveCmdRet[baseGroup] = zmcaux.ZAux_Direct_MoveOp(g_handle, axisList[0], opNum, 0);
                        if (moveCmdRet[baseGroup] == 0)
                            break;
                        else
                            //缓冲区满后加一点延时再重试, 不要一直占用通信资源
                            //如果运动距离很短, 需要的时间也很短, 就不要加太大延时, 以防电机运行停顿
                            Thread.Sleep(pauseBtweenRetry);
                    } while (true);

                }

                //退出外部循环. 只有用户发了cancelToken, 才能调用控制器的cancel指令. 
                //这里调用控制器的cancel指令就很安全, 不会出现停不稳的情况, 因为已经退出自动下发运动指令的循环了
                if (cToken.IsCancellationRequested)
                {
                    zmcaux.ZAux_Direct_Single_Cancel(g_handle, axisList[0], 2);
                    zmcaux.ZAux_Direct_SetOp(g_handle, opNum, 0);  //出于安全考虑也关掉输出
                    break;
                }
            }

            //重要, 一定要当前函数(当前线程)结束前depose并且设置为null
            myCancelSource[baseGroup].Dispose();
            myCancelSource[baseGroup] = null;

        }


        //演示搭配move_op2的用法, 该示例因为考虑到运动缓冲区变满和及时停止运动线程, 所以结构略复杂. 如果想参考简单示例, 可参考Basic手册Move_OP2指令
        private void RunSingleGroupWithMoveOP2(int baseGroup, int[] axisList, int opNum, int moveCmdCounts, int pauseBtweenRetry, CancellationToken cToken)
        {

            zmcaux.ZAux_Trigger(g_handle);  //触发示波器, 方便通过示波器观察

            //后面是正常的运动指令
            float[] movePoint = new float[axisList.Length];
            for (int moveIndex = 0; moveIndex < moveCmdCounts; moveIndex++)
            {
                movePoint = new float[] { moveIndex * 10, moveIndex * 10 };  //随意设置一个数值代表运动坐标

                do  //这里使用一个循环是因为要判断每个运动指令的返回值, 非0就要重试, 很大概率就是因为运动缓冲区满了
                {
                    //如果用户已经发了cancelToken, 退出嵌套的内循环
                    if (cToken.IsCancellationRequested)
                        break;

                    zmcaux.ZAux_Direct_SetForceSpeed(g_handle, axisList[0], 200);
                    //故意赋值给一个全局的数组, 方便在界面显示
                    moveCmdRet[baseGroup] = zmcaux.ZAux_Direct_MoveSp(g_handle, 2, axisList, movePoint);
                    if (moveCmdRet[baseGroup] == 0)
                        break;
                    else
                        //缓冲区满后加一点延时再重试, 不要一直占用通信资源
                        //如果运动距离很短, 需要的时间也很短, 就不要加太大延时, 以防电机运行停顿
                        Thread.Sleep(pauseBtweenRetry);
                } while (true);

                //在第五条运动指令后面开启io
                if (moveIndex == 5)
                {
                    do  //这里使用一个循环是因为要判断每个运动指令的返回值, 非0就要重试, 很大概率就是因为运动缓冲区满了
                    {
                        //如果用户已经发了cancelToken, 退出嵌套的内循环
                        if (cToken.IsCancellationRequested)
                            break;
                        //故意赋值给一个全局的数组, 方便在界面显示
                        moveCmdRet[baseGroup] = zmcaux.ZAux_Direct_MoveOp2(g_handle, axisList[0], opNum, 1, 100);
                        if (moveCmdRet[baseGroup] == 0)
                            break;
                        else
                            //缓冲区满后加一点延时再重试, 不要一直占用通信资源
                            //如果运动距离很短, 需要的时间也很短, 就不要加太大延时, 以防电机运行停顿
                            Thread.Sleep(pauseBtweenRetry);
                    } while (true);
                }


                //退出外部循环. 只有用户发了cancelToken, 才能调用控制器的cancel指令. 
                //这里调用控制器的cancel指令就很安全, 不会出现停不稳的情况, 因为已经退出自动下发运动指令的循环了
                if (cToken.IsCancellationRequested)
                {
                    zmcaux.ZAux_Direct_Single_Cancel(g_handle, axisList[0], 2);
                    zmcaux.ZAux_Direct_SetOp(g_handle, opNum, 0);  //出于安全考虑也关掉输出
                    break;
                }
            }

            //重要, 一定要当前函数(当前线程)结束前depose并且设置为null
            myCancelSource[baseGroup].Dispose();
            myCancelSource[baseGroup] = null;

        }


        //演示搭配move_task的用法, 该示例因为考虑到运动缓冲区变满和及时停止运动线程, 所以结构略复杂. 如果想参考简单示例, 可参考Basic手册Move_Task指令
        private void RunSingleGroupWithMoveTask(int baseGroup, int[] axisList, int moveCmdCounts, int pauseBtweenRetry, CancellationToken cToken)
        {
            int basicTaskNum = 5;  //调用basic函数所用的任务号

            //后面是正常的运动指令
            float[] movePoint = new float[axisList.Length];
            for (int moveIndex = 0; moveIndex < moveCmdCounts; moveIndex++)
            {
                movePoint = new float[] { moveIndex * 10, moveIndex * 10 };  //随意设置一个数值代表运动坐标

                do  //这里使用一个循环是因为要判断每个运动指令的返回值, 非0就要重试, 很大概率就是因为运动缓冲区满了
                {
                    //如果用户已经发了cancelToken, 退出嵌套的内循环
                    if (cToken.IsCancellationRequested)
                        break;

                    zmcaux.ZAux_Direct_SetForceSpeed(g_handle, axisList[0], 200);
                    //故意赋值给一个全局的数组, 方便在界面显示
                    moveCmdRet[baseGroup] = zmcaux.ZAux_Direct_MoveSp(g_handle, 2, axisList, movePoint);
                    if (moveCmdRet[baseGroup] == 0)
                        break;
                    else
                        //缓冲区满后加一点延时再重试, 不要一直占用通信资源
                        //如果运动距离很短, 需要的时间也很短, 就不要加太大延时, 以防电机运行停顿
                        Thread.Sleep(pauseBtweenRetry);
                } while (true);

                //在第五条运动指令后面开启io
                if (moveIndex == 5)
                {
                    do  //这里使用一个循环是因为要判断每个运动指令的返回值, 非0就要重试, 很大概率就是因为运动缓冲区满了
                    {
                        //如果用户已经发了cancelToken, 退出嵌套的内循环
                        if (cToken.IsCancellationRequested)
                            break;
                        //故意赋值给一个全局的数组, 方便在界面显示
                        moveCmdRet[baseGroup] = zmcaux.ZAux_Direct_MoveTask(g_handle, (uint)axisList[0], (uint)basicTaskNum, "MyGlobalBasicSub");
                        if (moveCmdRet[baseGroup] == 0)
                            break;
                        else
                            //缓冲区满后加一点延时再重试, 不要一直占用通信资源
                            //如果运动距离很短, 需要的时间也很短, 就不要加太大延时, 以防电机运行停顿
                            Thread.Sleep(pauseBtweenRetry);
                    } while (true);
                }


                //退出外部循环. 只有用户发了cancelToken, 才能调用控制器的cancel指令. 
                //这里调用控制器的cancel指令就很安全, 不会出现停不稳的情况, 因为已经退出自动下发运动指令的循环了
                if (cToken.IsCancellationRequested)
                {
                    zmcaux.ZAux_Direct_Single_Cancel(g_handle, axisList[0], 2);
                    //安全考虑, 还要关掉basic任务
                    StringBuilder retStr = new StringBuilder(1024);
                    zmcaux.ZAux_Execute(g_handle, $"STOPTASK {basicTaskNum}", retStr, (uint)retStr.Capacity);
                    break;
                }
            }

            //重要, 一定要当前函数(当前线程)结束前depose并且设置为null
            myCancelSource[baseGroup].Dispose();
            myCancelSource[baseGroup] = null;

        }


        //测试搭配move_wait的用法
        private void MoveWaitTestBtn_Click(object sender, EventArgs e)
        {
            if (!controllerConnected)
            {
                MessageBox.Show("请先连接控制器!", "警告");
                return;
            }

            //先停止电机, 以防当前已经有运动指令在执行
            SendCancelTokenOrCancelMotor();

            Debug.WriteLine("等待所有电机进入idle状态...");
            while (true)
            {
                float[] idleState = new float[4];
                zmcaux.ZAux_Direct_GetAllAxisPara(g_handle, "idle", 4, idleState);
                if (idleState[0] != 0 && idleState[1] != 0 && idleState[2] != 0 && idleState[3] != 0)
                {
                    break;
                }
            }

            //轴参数初始化
            AxisParamInit();
            ClearLabelText();

            //一定要判断为null后再新建, 重要, 否则另外一个线程可能把它清掉!!!
            Debug.WriteLine("等待cancelSource为null");
            while (true)
            {
                if (myCancelSource[0] == null && myCancelSource[1] == null)
                {
                    break;
                }
            }
            Debug.WriteLine("开始在新线程中运行运动指令....");
            //使用cancelSource, 这种方式响应更快
            myCancelSource[0] = new CancellationTokenSource();
            CancellationToken myCancelToken0 = myCancelSource[0].Token;
            //这里下发1000个运动指令
            Task myTask0 = new Task(() => RunSingleGroupWithMoveWait(0, axisGroup0, 1000, pauseBtweenRetry, myCancelToken0), myCancelToken0);
            myTask0.Start();
        }

        //将move_wait的信号设置为true
        private void SetSignalToTrueBtn_Click(object sender, EventArgs e)
        {
            if (!controllerConnected)
            {
                MessageBox.Show("请先连接控制器!", "警告");
                return;
            }

            //这里将modbus_reg(0)设置为1, 这样move_wait等待的信号就为true了
            int modbusRegAddr = 0;
            UInt16[] setValue = new UInt16[1] { 1 };
            zmcaux.ZAux_Modbus_Set4x(g_handle, (UInt16)modbusRegAddr, 1, setValue);

        }

        //测试搭配move_pause和move_resume的用法
        private void MovePauseTestBtn_Click(object sender, EventArgs e)
        {
            if (!controllerConnected)
            {
                MessageBox.Show("请先连接控制器!", "警告");
                return;
            }

            //先停止电机, 以防当前已经有运动指令在执行
            SendCancelTokenOrCancelMotor();

            Debug.WriteLine("等待所有电机进入idle状态...");
            while (true)
            {
                float[] idleState = new float[4];
                zmcaux.ZAux_Direct_GetAllAxisPara(g_handle, "idle", 4, idleState);
                if (idleState[0] != 0 && idleState[1] != 0 && idleState[2] != 0 && idleState[3] != 0)
                {
                    break;
                }
            }

            //轴参数初始化
            AxisParamInit();
            ClearLabelText();

            //一定要判断为null后再新建, 重要, 否则另外一个线程可能把它清掉!!!
            Debug.WriteLine("等待cancelSource为null");
            while (true)
            {
                if (myCancelSource[0] == null && myCancelSource[1] == null)
                {
                    break;
                }
            }
            Debug.WriteLine("开始在新线程中运行运动指令....");
            //使用cancelSource, 这种方式响应更快
            myCancelSource[0] = new CancellationTokenSource();
            CancellationToken myCancelToken0 = myCancelSource[0].Token;
            //这里下发1000个运动指令, 注意, 下发的运动指令数量一定不能超过运动缓冲区的大小, 否则因为无法下载解析move_resume而产生死锁!!!!!!
            Task myTask0 = new Task(() => RunSingleGroupWithMovePause(0, axisGroup0, 1000, pauseBtweenRetry, myCancelToken0), myCancelToken0);
            myTask0.Start();


        }

        private void AxisParamInit()
        {
            //故意把atype设置为0(虚拟轴), 测试更安全
            zmcaux.ZAux_Direct_SetAtype(g_handle, 0, 0);
            zmcaux.ZAux_Direct_SetAtype(g_handle, 1, 0);
            zmcaux.ZAux_Direct_SetAtype(g_handle, 2, 0);
            zmcaux.ZAux_Direct_SetAtype(g_handle, 3, 0);

            zmcaux.ZAux_Direct_SetDpos(g_handle, 0, 0);
            zmcaux.ZAux_Direct_SetDpos(g_handle, 1, 0);
            zmcaux.ZAux_Direct_SetDpos(g_handle, 2, 0);
            zmcaux.ZAux_Direct_SetDpos(g_handle, 3, 0);

            //本测试只设置主轴的速度等参数即可
            zmcaux.ZAux_Direct_SetSpeed(g_handle, 0, 50);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 0, 50 * 10);
            zmcaux.ZAux_Direct_SetDecel(g_handle, 0, 50 * 10);

            zmcaux.ZAux_Direct_SetSpeed(g_handle, 2, 50);
            zmcaux.ZAux_Direct_SetAccel(g_handle, 2, 50 * 10);
            zmcaux.ZAux_Direct_SetDecel(g_handle, 2, 50 * 10);

            zmcaux.ZAux_Direct_SetMovemark(g_handle, 0, 0);
            zmcaux.ZAux_Direct_SetMovemark(g_handle, 2, 0);
        }

        //测试搭配move_op的用法
        private void MoveOPBtn_Click(object sender, EventArgs e)
        {
            if (!controllerConnected)
            {
                MessageBox.Show("请先连接控制器!", "警告");
                return;
            }

            //先停止电机, 以防当前已经有运动指令在执行
            SendCancelTokenOrCancelMotor();

            Debug.WriteLine("等待所有电机进入idle状态...");
            while (true)
            {
                float[] idleState = new float[4];
                zmcaux.ZAux_Direct_GetAllAxisPara(g_handle, "idle", 4, idleState);
                if (idleState[0] != 0 && idleState[1] != 0 && idleState[2] != 0 && idleState[3] != 0)
                {
                    break;
                }
            }

            //轴参数初始化
            AxisParamInit();
            ClearLabelText();

            //一定要判断为null后再新建, 重要, 否则另外一个线程可能把它清掉!!!
            Debug.WriteLine("等待cancelSource为null");
            while (true)
            {
                if (myCancelSource[0] == null && myCancelSource[1] == null)
                {
                    break;
                }
            }
            Debug.WriteLine("开始在新线程中运行运动指令....");
            //使用cancelSource, 这种方式响应更快
            myCancelSource[0] = new CancellationTokenSource();
            CancellationToken myCancelToken0 = myCancelSource[0].Token;
            //这里下发10个运动指令
            Task myTask0 = new Task(() => RunSingleGroupWithMoveOP(0, axisGroup0, 0, 10, pauseBtweenRetry, myCancelToken0), myCancelToken0);
            myTask0.Start();

        }

        private void MoveOP2Btn_Click(object sender, EventArgs e)
        {
            if (!controllerConnected)
            {
                MessageBox.Show("请先连接控制器!", "警告");
                return;
            }

            //先停止电机, 以防当前已经有运动指令在执行
            SendCancelTokenOrCancelMotor();

            Debug.WriteLine("等待所有电机进入idle状态...");
            while (true)
            {
                float[] idleState = new float[4];
                zmcaux.ZAux_Direct_GetAllAxisPara(g_handle, "idle", 4, idleState);
                if (idleState[0] != 0 && idleState[1] != 0 && idleState[2] != 0 && idleState[3] != 0)
                {
                    break;
                }
            }

            //轴参数初始化
            AxisParamInit();
            ClearLabelText();

            //一定要判断为null后再新建, 重要, 否则另外一个线程可能把它清掉!!!
            Debug.WriteLine("等待cancelSource为null");
            while (true)
            {
                if (myCancelSource[0] == null && myCancelSource[1] == null)
                {
                    break;
                }
            }
            Debug.WriteLine("开始在新线程中运行运动指令....");
            //使用cancelSource, 这种方式响应更快
            myCancelSource[0] = new CancellationTokenSource();
            CancellationToken myCancelToken0 = myCancelSource[0].Token;
            //这里下发10个运动指令
            Task myTask0 = new Task(() => RunSingleGroupWithMoveOP2(0, axisGroup0, 0, 10, pauseBtweenRetry, myCancelToken0), myCancelToken0);
            myTask0.Start();


        }

        //测试搭配move_task的用法, 必须搭配对应的basic程序才能正常工作. 对应的basic程序在当前c#项目目录下
        private void MoveTaskBtn_Click(object sender, EventArgs e)
        {

            if (!controllerConnected)
            {
                MessageBox.Show("请先连接控制器!", "警告");
                return;
            }

            //先停止电机, 以防当前已经有运动指令在执行
            SendCancelTokenOrCancelMotor();

            Debug.WriteLine("等待所有电机进入idle状态...");
            while (true)
            {
                float[] idleState = new float[4];
                zmcaux.ZAux_Direct_GetAllAxisPara(g_handle, "idle", 4, idleState);
                if (idleState[0] != 0 && idleState[1] != 0 && idleState[2] != 0 && idleState[3] != 0)
                {
                    break;
                }
            }

            //轴参数初始化
            AxisParamInit();
            ClearLabelText();

            //一定要判断为null后再新建, 重要, 否则另外一个线程可能把它清掉!!!
            Debug.WriteLine("等待cancelSource为null");
            while (true)
            {
                if (myCancelSource[0] == null && myCancelSource[1] == null)
                {
                    break;
                }
            }
            Debug.WriteLine("开始在新线程中运行运动指令....");
            //使用cancelSource, 这种方式响应更快
            myCancelSource[0] = new CancellationTokenSource();
            CancellationToken myCancelToken0 = myCancelSource[0].Token;
            //这里下发10个运动指令
            Task myTask0 = new Task(() => RunSingleGroupWithMoveTask(0, axisGroup0, 100, pauseBtweenRetry, myCancelToken0), myCancelToken0);
            myTask0.Start();


        }
    }
}




