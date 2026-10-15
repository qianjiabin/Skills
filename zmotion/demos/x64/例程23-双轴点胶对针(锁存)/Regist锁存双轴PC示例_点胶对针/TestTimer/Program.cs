using System;
using System.Diagnostics;
using cszmcaux;
using AxisStatus_Scan;
using System.Reflection.Metadata;
using System.Reflection;
using System.Threading;

namespace TestTimer
{
    class Program
    {
        static void Main(string[] args)
        {
            IntPtr g_handle;
            int ret = zmcaux.ZAux_OpenEth("192.168.0.11", out g_handle);
            if(ret != 0)
            {
                Console.WriteLine("Connect Zmotion Controller Errror!");
                return;
            }
            else
                Console.WriteLine("Connect Zmotion Controller Success.");


            Console.WriteLine("Press Enter key to start test.");
            Console.ReadLine();
            Console.WriteLine("Start....");

            int[] axisNum = new int[2] { 0, 1};  //要锁存位置的轴号
            //脉冲轴单轴的常用锁存模式为3/4, 具体是上升沿还是下降沿要测试, 不同型号控制器不一样
            //验证方法是要么用真实设备测试, 要么把一个输出接输入上, 看是按钮按钮触发还是松开按钮触发
            //npn导通后是低电平, 所以按下按钮相当于产生一次下降沿, 松开产生一次上升沿
            //在默认的输入配置下(本示例没有修改, 也不建议修改), 脉冲轴锁存时R0对应输入口0, R1对应输入口1
            int[] regMode = new int[2]{ 4, 11 };  //模式3(R0)和模式10(R1)都是上升沿, 4(R0)和11(R1)都是下降沿   
            int[] markState = new int[2];
            float[] regValue = new float[2];
            bool[] printOnce = new bool[2];

            //如果使用总线轴, 则要更换锁存模式, 锁存标志变量和锁存寄存器地址
            //总线轴既可以把io信号接在驱动上(主流驱动一般支持), 也可以接在控制器上(低端系列控制器可能不支持)
            
            while (true)
            {
                zmcaux.ZAux_Direct_Regist(g_handle, axisNum[0], regMode[0]);
                zmcaux.ZAux_Direct_Regist(g_handle, axisNum[1], regMode[1]);
                printOnce[0] = false;
                printOnce[1] = false;
                Console.WriteLine("等待触发信号.....");
                while (true)
                {   
                    //注意, regMode不一样, 对应下面的mark状态对应的标志变量也不一样, 这里分别分别对应了Mark和MarkB
                    zmcaux.ZAux_Direct_GetMark(g_handle, axisNum[0], ref markState[0]);
                    zmcaux.ZAux_Direct_GetMarkB(g_handle, axisNum[1], ref markState[1]);
                    if (markState[0] != 0 && printOnce[0] == false)
                    {
                        //锁存数值的变量也不一样, 这里分别对应了RegPos和RegPosB
                        zmcaux.ZAux_Direct_GetRegPos(g_handle, axisNum[0], ref regValue[0]);
                        Console.WriteLine("{0}轴锁存信号已触发, 锁存位置为:{1}", axisNum[0], regValue[0]);
                        printOnce[0] = true;
                    }
                    if (markState[1] != 0 && printOnce[1] == false)
                    {
                        zmcaux.ZAux_Direct_GetRegPosB(g_handle, axisNum[1], ref regValue[1]);
                        Console.WriteLine("{0}轴锁存信号已触发, 锁存位置为:{1}", axisNum[1], regValue[1]); 
                        printOnce[1] = true;
                    }
                    if (printOnce[0] == true && printOnce[1] == true)
                    { break; }  //退出循环
                    //锁存自身是靠硬件实现的, 实时性很高, 所以检测是否锁存成功了并不需要很高频率去采集
                    //所以如果设备上还有其他任务需要和控制器通信, 就不需要死循环读取占用所有通信资源.
                    Thread.Sleep(50); 
                }

            }

            
            //zmcaux.ZAux_Close(g_handle);
            //return;

        }

    }
}
