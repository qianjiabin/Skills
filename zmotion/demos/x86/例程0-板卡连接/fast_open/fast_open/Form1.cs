using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cszmcaux;
using System.IO.Ports;
using System.Diagnostics;
using System.Threading.Tasks;

namespace fast_open
{
    public partial class Form1 : Form
    {
        public IntPtr g_handle;
       private void C_connect_type_TextChanged(object sender, EventArgs e)
       {
            string connectType = C_connect_type.Text;
            cc = new ConnectContext(connectType);
       }
        public Form1()
        {
            InitializeComponent();
            C_connect_type.SelectedIndex = 0;
        }

        private void OnSelect(object sender, EventArgs e)
        {
            C_Ip_Address.SelectedIndex = -1;
        }

        ConnectContext cc;
        private void OnScan(object sender, EventArgs e)
        {

            string connectType = C_connect_type.Text;
            C_Ip_Address.Items.Clear();
            try
            {
                cc = new ConnectContext(connectType);
            }
            catch (ArgumentException ae)
            {
                MessageBox.Show(string.Format("{0}: {1}", ae.GetType().Name, ae.Message));
                return;
            }
            string[] scanResult = cc.ScanConnect();
            for (int i = 0; i < scanResult.Length; i++)
            {
                C_Ip_Address.Items.Insert(i, scanResult[i].ToString());
            }
        }

        private void OnOpen(object sender, EventArgs e)
        {
            string Buffer = C_Ip_Address.Text;

            try
            {
                cc.OpenConnect(ref g_handle, Buffer);

            }
            catch (NullReferenceException ne)
            {
                MessageBox.Show(string.Format("{0}: {1}", ne.GetType().Name, ne.Message));
                return;
            }

            //ControllerInit();

        }

        private void OnClose(object sender, EventArgs e)
        {
            if (g_handle == (IntPtr)0)
                return;

            cc.CloseConnect(g_handle);
            g_handle = (IntPtr)0;
        }

        //Great moment
        Task taskA;
        private bool isTaskARunning = false;

        private string test_Move() 
        {
           
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                for (int i = 0; i < 10000; i++)
                {
                    zmcaux.ZAux_Direct_Single_Move(g_handle, 0, 100);
                }

                stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                return string.Format("用时:{0:00}分钟:{1:00}秒.{2:00}毫秒", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                //textBox1.Text = string.Format("用时:{0:00}分钟:{1:00}秒.{2:00}毫秒", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
             if (g_handle != (IntPtr)0)
            {
                Task<string> taskA = new Task<string>(() => test_Move());
                 if (isTaskARunning)
                     taskA.ContinueWith(t => { isTaskARunning = false; textBox1.Text = taskA.Result; }, TaskScheduler.FromCurrentSynchronizationContext());
                else
                {
                    taskA.ContinueWith(t => { isTaskARunning = false; textBox1.Text = taskA.Result; }, TaskScheduler.FromCurrentSynchronizationContext());
                    taskA.Start();
                    isTaskARunning = true;
                }
            }

        }

        private string test_GetIn()
        {

            Stopwatch stopwatch = new Stopwatch();
            uint uval = 0;
            stopwatch.Start();

            for (int i = 0; i < 10000; i++)
            {
                zmcaux.ZAux_Direct_GetIn(g_handle, 0, ref uval);
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            return string.Format("用时:{0:00}分钟:{1:00}秒.{2:00}毫秒", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {

                Task<string> taskA = new Task<string>(() => test_GetIn());
                if (isTaskARunning)
                    taskA.ContinueWith(t => { isTaskARunning = false; textBox2.Text = taskA.Result; }, TaskScheduler.FromCurrentSynchronizationContext());
                else
                {
                    taskA.ContinueWith(t => { isTaskARunning = false; textBox2.Text = taskA.Result; }, TaskScheduler.FromCurrentSynchronizationContext());
                    taskA.Start();
                    isTaskARunning = true;
                }
            }
        }

        private string test_GetDpos()
        {

            Stopwatch stopwatch = new Stopwatch();
            float fval = 0.0F;
            stopwatch.Start();

            for (int i = 0; i < 10000; i++)
            {
                zmcaux.ZAux_Direct_GetDpos(g_handle, 0, ref fval);
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            return string.Format("用时:{0:00}分钟:{1:00}秒.{2:00}毫秒", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                Task<string> taskA = new Task<string>(() => test_GetDpos());
                if (isTaskARunning)
                    taskA.ContinueWith(t => { isTaskARunning = false; textBox3.Text = taskA.Result; }, TaskScheduler.FromCurrentSynchronizationContext());
                else
                {
                    taskA.ContinueWith(t => { isTaskARunning = false; textBox3.Text = taskA.Result; }, TaskScheduler.FromCurrentSynchronizationContext());
                    taskA.Start();
                    isTaskARunning = true;
                }       
            }
        }

        private string test_GetModbusIEEE()
        {

            Stopwatch stopwatch = new Stopwatch();
            float[] fval = { 0.0F };
            stopwatch.Start();

            for (int i = 0; i < 10000; i++)
            {
                zmcaux.ZAux_Modbus_Get4x_Float(g_handle, 0, 50, fval);
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            return string.Format("用时:{0:00}分钟:{1:00}秒.{2:00}毫秒", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                Task<string> taskA = new Task<string>(() => test_GetModbusIEEE());
                if (isTaskARunning)
                    taskA.ContinueWith(t => { isTaskARunning = false; textBox4.Text = taskA.Result; }, TaskScheduler.FromCurrentSynchronizationContext());
                else
                {
                    taskA.ContinueWith(t => { isTaskARunning = false; textBox4.Text = taskA.Result; }, TaskScheduler.FromCurrentSynchronizationContext());
                    taskA.Start();
                    isTaskARunning = true;
                }    
            }
        }

        private string test_MultiAxisMove()
        {

            Stopwatch stopwatch = new Stopwatch();
            int[] axislist = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            float[] fval = { 0.0f, 0.0f, 0.0f, 0.0f };
            stopwatch.Start();

            for (int i = 0; i < 10000; i++)
            {
                zmcaux.ZAux_Direct_Move(g_handle, 10, axislist, fval);
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            return string.Format("用时:{0:00}分钟:{1:00}秒.{2:00}毫秒", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                Task<string> taskA = new Task<string>(() => test_MultiAxisMove());
                if (isTaskARunning)
                    taskA.ContinueWith(t => { isTaskARunning = false; textBox5.Text = taskA.Result; }, TaskScheduler.FromCurrentSynchronizationContext());
                else
                {
                    taskA.ContinueWith(t => { isTaskARunning = false; textBox5.Text = taskA.Result; }, TaskScheduler.FromCurrentSynchronizationContext());
                    taskA.Start();
                    isTaskARunning = true;
                }    
            }
        }

        private string test_GetModbusIn()
        {

            Stopwatch stopwatch = new Stopwatch();
            byte[] uval = new byte[100];
            stopwatch.Start();

            for (int i = 0; i < 10000; i++)
            {
                zmcaux.ZAux_GetModbusIn(g_handle, 0, 99, uval); 
            }

            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            return string.Format("用时:{0:00}分钟:{1:00}秒.{2:00}毫秒", ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (g_handle != (IntPtr)0)
            {
                
                Task<string> taskA = new Task<string>(() => test_GetModbusIn());
                if (isTaskARunning)
                    taskA.ContinueWith(t => { isTaskARunning = false; textBox6.Text = taskA.Result; }, TaskScheduler.FromCurrentSynchronizationContext());
                else
                {
                    taskA.ContinueWith(t => { isTaskARunning = false; textBox6.Text = taskA.Result; }, TaskScheduler.FromCurrentSynchronizationContext());
                    taskA.Start();
                    isTaskARunning = true;
                }    
                
            }
        }

        
    }
}

public abstract class Connect
{
    public abstract string[] scan();
}


class LocalConect : Connect
{
    public override string[] scan()
    {
        string[] stringArray = new string[] { "LOCAL1" };
        return stringArray;
    }
}



class EthConnect : Connect
{
    public override string[] scan()
    {
        int num;
        string[] sArray;
        StringBuilder buffer = new StringBuilder(10240);
        string buff = "";
        zmcaux.ZAux_SearchEthlist(buffer, 10230, 200);
        buff += buffer;
        sArray = buff.Split(' ');
        num = buff.Split(' ').Length;
        sArray = sArray.Take(num - 1).ToArray();
        return sArray;
    }

}

class ComConnect : Connect
{
    public override string[] scan()
    {
        return SerialPort.GetPortNames();
    }


}

class PciConnect : Connect
{
    public override string[] scan()
    {
        int Card;
        Card = zmcaux.ZAux_GetMaxPciCards();
        string[] tmpstr = new string[Card];

        for (int i = 0; i < Card; i++)
        {
            tmpstr[i] = string.Format("PCI{0:D}", i + 1);
        }
        return tmpstr;
    }
}

public class ConnectContext
{
    private Connect cn = null;
    private int ctype = -1;


    public ConnectContext(string type)
    {
        switch (type)
        {
            case "网口":
                cn = new EthConnect();
                ctype = 2;
                break;
            case "串口":
                cn = new ComConnect();
                ctype = 1;
                break;
            case "PCI":
                cn = new PciConnect();
                ctype = 4;
                break;
            case "LOCAL":
                cn = new LocalConect();
                ctype = 5;
                break;
        }
    }

    public string[] ScanConnect()
    {
        return cn.scan();
    }

    public int OpenConnect(ref IntPtr g_handle, string Buffer)
    {
        if (g_handle != (IntPtr)0)
        {
            CloseConnect(g_handle);
        }
        return zmcaux.ZAux_FastOpen(ctype, Buffer, 1000, out g_handle);
    }

    public int CloseConnect(IntPtr g_handle)
    {
        return zmcaux.ZAux_Close(g_handle);
    }
}
