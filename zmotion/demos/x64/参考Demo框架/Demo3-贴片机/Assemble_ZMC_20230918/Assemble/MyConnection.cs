using cszmcaux;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assemble
{
    public class MyConnection
    {
        public SocketEx socket = new SocketEx();//初始化通讯对象

        public bool start = false;

        public LogDelegate Log;

        public IntPtr g_handle;

        public MyConnection(LogDelegate Log, IntPtr g_handle)
        {
            this.Log = Log;
            this.g_handle = g_handle;
        }

        public double AngleNow = 0;

        public void CreateConnection()
        {
            socket.ReadConfigFile("SocketConfig");
            socket.InitSocket(MessageCallBack, true);

            AngleNow = 0;
        }

        public void MessageCallBack(string str)
        {
            try
            {
                if (str == null)
                {
                    return;
                }

                string Time = null; //DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss ffff ");

                if (str == "CONNECT")
                {
                    str = string.Format("{0} {1} 已上线", Time, socket.GetTCPInfo());
                    Log(str);
                    return;
                }
                if (str == "DISCONNECT")
                {
                    str = string.Format("{0} {1} 已离线！", Time, socket.GetTCPInfo());
                    Log(str);
                    return;
                }
                if (str == "CLIENT ONLINE")
                {
                    str = string.Format("{0} {1} 已上线", Time, socket.GetTCPInfo());
                    Log(str);
                    return;

                }
                if (str == "CLIENT OFFLINE")
                {
                    str = string.Format("{0} {1} 已离线！", Time, socket.GetTCPInfo());
                    Log(str);
                    return;

                }
                if (str == "SERVER ONLINE")
                {
                    str = string.Format("{0} {1}:{2}服务器已上线！", Time, socket.m_IPInfo.m_strIPAddr, socket.m_IPInfo.m_nIPPort);
                    Log(str);
                    return;
                }

                Log(string.Format("{0}:{1}", Time, str));

                //平台复位 RST
                if (str == "RST")
                {
                    int[] axislist = { 0, 1, 2 };
                    float[] poslist = { 0, 0, 0 };
                    zmcaux.ZAux_Direct_Base(g_handle, 3, axislist);
                    zmcaux.ZAux_Direct_MoveAbs(g_handle, 3, axislist, poslist);

                    WaitAllAxisStop();

                    AngleNow = 0;
                    Log(string.Format("{0}:{1} Succeed AngleNow Is Zero", Time, str));
                    socket.Send("OK AngleNow Is Zero"); 
                }

                //平台绝对运动 协议格式 ABS:X:Y
                if (str.StartsWith("ABS:"))
                {
                    string[] info = str.Split(':');
                    float X = Convert.ToSingle(info[1]);
                    float Y = Convert.ToSingle(info[2]);
                    int[] axislist = { 0, 1, 2 };
                    float[] poslist = { -X, X, Y };
                    zmcaux.ZAux_Direct_Base(g_handle, 3, axislist);
                    zmcaux.ZAux_Direct_MoveAbs(g_handle, 3, axislist, poslist);

                    WaitAllAxisStop();
                    Log(string.Format("{0}:{1} Succeed", Time, str));
                    socket.Send($"OK");
                }

                //平台相对运动 协议格式 REL:X:Y
                if (str.StartsWith("REL:"))
                {
                    string[] info = str.Split(':');
                    float X = Convert.ToSingle(info[1]);
                    float Y = Convert.ToSingle(info[2]);
                    int[] axislist = { 0, 1, 2 };
                    float[] poslist = { -X, X, Y };
                    zmcaux.ZAux_Direct_Base(g_handle, 3, axislist);
                    zmcaux.ZAux_Direct_Move(g_handle, 3, axislist, poslist);

                    WaitAllAxisStop();

                    Log(string.Format("{0}:{1} Succeed", Time, str));
                    socket.Send($"OK");
                }

                //平台旋转 格式 ROT:Angle Angle为相对值
                if (str.StartsWith("ROT:"))
                {
                    //double AngleNowRad = AngleNow * Math.PI / 180;

                    //string[] info = str.Split(':');
                    //float AngleDegIn = Convert.ToSingle(info[1]);
                    //double AngleRad = AngleDegIn * Math.PI / 180 + AngleNowRad;

                    //double X0 = 90 * Math.PI / 180;
                    //double X1 = 270 * Math.PI / 180;
                    //double Y = 180 * Math.PI / 180;
                    //double R = 52.5;
                    //int Pulse = 2000;
                    //double X0Push, X1Push, YPush = 0;

                    //double X0PushNow = R * Math.Cos(AngleNowRad + X0) - R * Math.Cos(X0);
                    //double X1PushNow = -(R * Math.Cos(AngleNowRad + X1) - R * Math.Cos(X1));
                    //double YPushNow = R * Math.Sin(AngleNowRad + Y) - R * Math.Sin(Y);

                    //X0Push = R * Math.Cos(AngleRad + X0) - R * Math.Cos(X0) - X0PushNow;
                    //X1Push = -(R * Math.Cos(AngleRad + X1) - R * Math.Cos(X1)) - X1PushNow;
                    //YPush = R * Math.Sin(AngleRad + Y) - R * Math.Sin(Y) - YPushNow;

                    //int[] axislist = { 0, 1, 2 };
                    //float[] poslist = { (float)(X0Push * Pulse), (float)(X1Push * Pulse), (float)(YPush * Pulse) };
                    //zmcaux.ZAux_Direct_Base(g_handle, 3, axislist);
                    //zmcaux.ZAux_Direct_Move(g_handle, 3, axislist, poslist);

                    //WaitAllAxisStop();

                    //AngleNow = AngleRad * 180 / Math.PI;

                    //Log(string.Format("{0}:{1} Succeed AngleNow {2}", Time, str , AngleNow));
                    //socket.Send($"OK AngleNow {AngleNow}");

                    string[] info = str.Split(':');
                    float AngleDegIn = Convert.ToSingle(info[1]);
                    double AngleRad = AngleDegIn * Math.PI / 180;

                    double X0 = 90 * Math.PI / 180;
                    double X1 = 270 * Math.PI / 180;
                    double Y = 180 * Math.PI / 180;
                    double R = 52.5;
                    int Pulse = 2000;
                    double X0Push, X1Push, YPush = 0;

                    X0Push = R * Math.Cos(AngleRad + X0) - R * Math.Cos(X0);
                    X1Push = -(R * Math.Cos(AngleRad + X1) - R * Math.Cos(X1));
                    YPush = R * Math.Sin(AngleRad + Y) - R * Math.Sin(Y);

                    int[] axislist = { 0, 1, 2 };
                    float[] poslist = { (float)(X0Push * Pulse), (float)(X1Push * Pulse), (float)(YPush * Pulse) };
                    zmcaux.ZAux_Direct_Base(g_handle, 3, axislist);
                    zmcaux.ZAux_Direct_Move(g_handle, 3, axislist, poslist);

                    WaitAllAxisStop();

                    AngleNow = AngleNow + AngleDegIn;

                    Log(string.Format("{0}:{1} Succeed AngleNow {2}", Time, str, AngleNow));
                    socket.Send($"OK AngleNow {AngleNow}");

                }

                //平台旋转平移 只有在平台复位的位置才可以使用 格式 RT:Angle:X:Y Angle为绝对值
                if (str.StartsWith("RT:"))
                {
                    if (AngleNow != 0)
                    {
                        Log(string.Format("{0}:{1} Error AngleNow Is Not Zero", Time, str));
                        socket.Send("Error AngleNow Is Not Zero");
                    }

                    string[] info = str.Split(':');
                    float AngleDegIn = Convert.ToSingle(info[1]);
                    double AngleRad = AngleDegIn * Math.PI / 180;

                    double X0 = 90 * Math.PI / 180;
                    double X1 = 270 * Math.PI / 180;
                    double Y = 180 * Math.PI / 180;
                    double R = 52.5;
                    int Pulse = 2000;
                    double X0Push, X1Push, YPush = 0;
                    
                    X0Push = R * Math.Cos(AngleRad + X0) - R * Math.Cos(X0);
                    X1Push = -(R * Math.Cos(AngleRad + X1) - R * Math.Cos(X1));
                    YPush = R * Math.Sin(AngleRad + Y) - R * Math.Sin(Y);

                    X0Push = X0Push* Pulse;
                    X1Push = X1Push * Pulse;
                    YPush = YPush * Pulse;

                    float XTrans = Convert.ToSingle(info[2]);
                    float YTrans = Convert.ToSingle(info[3]);

                    X0Push = X0Push - XTrans;
                    X1Push = X1Push + XTrans;
                    YPush = YPush + YTrans;

                    int[] axislist = { 0, 1, 2 };
                    float[] poslist = { (float)(X0Push), (float)(X1Push), (float)(YPush) };
                    zmcaux.ZAux_Direct_Base(g_handle, 3, axislist);
                    zmcaux.ZAux_Direct_Move(g_handle, 3, axislist, poslist);

                    WaitAllAxisStop();

                    AngleNow = AngleDegIn;

                    Log(string.Format("{0}:{1} Succeed AngleNow {2}", Time, str , AngleNow));
                    socket.Send($"OK AngleNow {AngleNow}");
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }

        public void WaitAllAxisStop()
        {
            while (true)
            {
                int Value = 0;
                zmcaux.ZAux_Direct_GetIfIdle(g_handle, 0, ref Value);
                Application.DoEvents();
                if (Value != 0)
                {
                    break;
                }
            }
            while (true)
            {
                int Value = 0;
                zmcaux.ZAux_Direct_GetIfIdle(g_handle, 1, ref Value);
                Application.DoEvents();
                if (Value != 0)
                {
                    break;
                }
            }
            while (true)
            {
                int Value = 0;
                zmcaux.ZAux_Direct_GetIfIdle(g_handle, 2, ref Value);
                Application.DoEvents();
                if (Value != 0)
                {
                    break;
                }
            }
        }
    }
}
