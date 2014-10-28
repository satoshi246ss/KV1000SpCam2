using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

namespace KV1000SpCam
{
    public partial class Form1 : Form
    {
        //観測時間帯を表す定数
        const int Daytime   = 0;
        const int Nighttime = 1;
        //上の状態を保持します
        int States = 0;

        Stopwatch sw = new Stopwatch();
        long elapsed0 = 0, elapsed1 = 0, elapsed2 = 0;
        double lap0 = 0, lap1 = 0, lap2 = 0, alpha = 0.01;
        static int udp_id=0;
        static int udp_id_next = 50;  //UDP　送信回数（１起動毎の）
        static int udp_send_on = 0;
        string cmd_str ="RD DM11390" ;
        int cmd_str_f = 0 ;
        String udp_r;

        FSI_PID_DATA pid_data = new FSI_PID_DATA();
        MT_MONITOR_DATA mtmon_data = new MT_MONITOR_DATA();
        int mmFsiUdpPortKV1000SpCam2  = 24426; //24410;            // MT3IDS （受信）
        int mmFsiUdpPortKV1000SpCam2s = 24427; //24426;            // MT3IDS （送信）
        int mmFsiUdpPortMTmonitor = 24415;
        string mmFsiCore_i5 = "192.168.1.211";
        int mmFsiUdpPortSpCam = 24410;   // SpCam（受信）
        string mmFsiSC440 = "192.168.1.206";
        System.Net.Sockets.UdpClient udpc  = null;
        System.Net.Sockets.UdpClient udpc2 = null;
        DriveInfo cDrive = new DriveInfo("C");
        long diskspace;

        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern uint timeBeginPeriod(uint uMilliseconds);

        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern uint timeEndPeriod(uint uMilliseconds);
        uint time_period = 1;

        public Form1()
        {
            InitializeComponent();
    
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // ソケット生成
            udpc  = new System.Net.Sockets.UdpClient(mmFsiUdpPortKV1000SpCam2);
            udpc2 = new System.Net.Sockets.UdpClient(mmFsiUdpPortKV1000SpCam2s);
            // ソケット非同期受信(System.AsyncCallback)
            udpc.BeginReceive(ReceiveCallback, udpc);

            timeBeginPeriod(time_period);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            timeBeginPeriod(16);
            // ソケットクローズ
            udpc.Close();
            udpc2.Close();
        }

        #region UDP
        // 別スレッド処理（UDP） //IP 192.168.1.209
        private void worker_udp_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;

            //バインドするローカルポート番号
            int localPort = mmFsiUdpPortKV1000SpCam2; //  24410;// broadcast mmFsiUdpPortMT3IDS2;
            System.Net.Sockets.UdpClient udpc = null; ;
            try
            {
                udpc = new System.Net.Sockets.UdpClient(localPort);
            }
            catch (Exception ex)
            {
                //匿名デリゲートで表示する
                this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, ex.ToString() });
            }

            //文字コードを指定する
            System.Text.Encoding enc = System.Text.Encoding.UTF8;
            //データを送信するリモートホストとポート番号
            //string remoteHost = "localhost";
            string remoteHost = "192.168.1.10"; // KV1000;
            int remotePort = 8501;


            string str;
            MOTOR_DATA_KV_SP kmd3 = new MOTOR_DATA_KV_SP();
            int size = Marshal.SizeOf(kmd3);
            KV_DATA kd = new KV_DATA();
            int sizekd = Marshal.SizeOf(kd);


            // loop
            System.Net.IPEndPoint remoteEP = new System.Net.IPEndPoint(System.Net.IPAddress.Any, localPort);
            while (bw.CancellationPending == false)
            {
                //データを受信する
                byte[] rcvBytes = udpc.Receive(ref remoteEP);

                if (remoteEP.Address.ToString() == remoteHost && remoteEP.Port == remotePort )
                {
                    //kd = ToStruct1(rcvBytes);
                    //bw.ReportProgress(0, kd);

                    string rcvMsg = enc.GetString(rcvBytes);
                    str = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + "受信したデータ:[" + rcvMsg + "]\n";
                    this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, str });
                }
                 else
                {
                    string rcvMsg = enc.GetString(rcvBytes);
                    str = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + "送信元アドレス:{0}/ポート番号:{1}/Size:{2}\n" + remoteEP.Address + "/" + remoteEP.Port + "/" + rcvBytes.Length + "[" + rcvMsg + "]\n";
                    this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, str });
                }

                //データを送信する
                if (cmd_str_f != 0)
                {
                    //送信するデータを読み込む
                    string sendMsg = cmd_str + "\r";// "test送信するデータ";
                    byte[] sendBytes = enc.GetBytes(sendMsg);

                    //リモートホストを指定してデータを送信する
                    udpc.Send(sendBytes, sendBytes.Length, remoteHost, remotePort);
                    cmd_str_f = 0;
                }
            }

            //UDP接続を終了
         //   udpc.Close();
        }
        #endregion

        private void button_test_Click(object sender, EventArgs e)
        {
            cmd_str_f = 1;

            int idd = 32765;
            sw.Start();
            for (short i = 0; i < 30; i++)
            {
                idd++;
                //this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, idd });
                Pid_Data_Set_Wide((short)((i & 32767)), +i / 1000.0,  - i / 1000.0, i/1000.0); // 32767 == 7FFF
                Pid_Data_Set_Fine((short)((i & 32767)), +i / 1000.0, -i / 1000.0, i / 1000.0); // 32767 == 7FFF
                int j = i % 3;
                if (j == 0) Pid_Data_Send_KV1000("192.168.1.11"); //UDP2 
                if (j == 1) Pid_Data_Send_KV1000("192.168.1.11"); //UDP2 
                if (j == 2) Pid_Data_Send_KV1000("192.168.1.10"); //UDP1 

                //Pid_Data_Send_cmd_KV1000((short)-(short)((idd & 32767)), 32.765 + i / 1000.0, -32.765 - i / 1000.0); // 32767 == 7FFF
                System.Threading.Thread.Sleep(13); // 13ms->75Hz
            }
            sw.Stop();
            long millisec = sw.ElapsedMilliseconds;
            this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, millisec.ToString()+"ms\n" });

          //  Pid_Data_Send_cmd_KV1000((short)( 100 ), 32.765 , -32.765 ); // 32767 == 7FFF

        }


        private void timer_udp_Tick(object sender, EventArgs e)
        {
            if (udp_send_on == 1)
            {
                int j = udp_id % 3;
                if (j == 0) Pid_Data_Send_KV1000("192.168.1.11"); //UDP2 
                if (j == 1) Pid_Data_Send_KV1000("192.168.1.11"); //UDP2 
                if (j == 2) Pid_Data_Send_KV1000("192.168.1.10"); //UDP1

                ++udp_id;
                if (udp_id >= udp_id_next) udp_send_on = 0;
            }

        }

        private void button_UDP_on_Click(object sender, EventArgs e)
        {
            if (timer_udp.Enabled == true)
            {
                timer_udp.Enabled = false;
            }
            else
            {
                timer_udp.Enabled = true;
            }
        }

        private void button_px_MouseDown(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("ST 01212\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        private void button_px_MouseUp(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("RS 01212\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        private void button_mx_MouseDown(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("ST 01213\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        private void button_mx_MouseUp(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("RS 01213\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        private void button_py_MouseDown(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("ST 01214\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        private void button_py_MouseUp(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("RS 01214\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        private void button_my_MouseDown(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("ST 01215\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        private void button_my_MouseUp(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("RS 01215\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        /// <summary>
        /// 観測時間にfine起動
        /// </summary>
        private void timerObsOnOff_Tick(object sender, EventArgs e)
        {
            TimeSpan nowtime = DateTime.Now - DateTime.Today;
            TimeSpan endtime = new TimeSpan(7, 0, 0);
            TimeSpan starttime = new TimeSpan(18,05, 0); //17 3 0

            if (nowtime.CompareTo(endtime) >= 0 && nowtime.CompareTo(starttime) <= 0)
            {
                // DayTime
                if (this.States == Daytime && checkBoxObsAuto.Checked)
                {
                }
                this.States = Daytime;
            }
            else
            {
                //NightTime
                if (this.States == Daytime && checkBoxObsAuto.Checked)
                {
                    System.Diagnostics.Process p = System.Diagnostics.Process.Start(@"""C:\Users\root\Documents\Visual Studio 2010\Projects\MT3Fine\PictureViewer\bin\Release\MT3Fine.exe""");
                }
                this.States = Nighttime;
            }
        }

     }
}
