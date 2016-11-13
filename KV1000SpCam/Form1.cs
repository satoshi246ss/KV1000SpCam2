using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Diagnostics;
using System.Runtime.InteropServices;
using MtLibrary;

namespace KV1000SpCam
{
    public partial class Form1 : Form
    {
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

            string s = "KV1000SpCam_log1_" + DateTime.Today.ToString("dd") + ".txt";
            Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
            writer = new StreamWriter(@"E:\log\" + s, true, sjisEnc);
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            starttime = Planet.ObsStartTime(DateTime.Now) - DateTime.Today;
            endtime = Planet.ObsEndTime(DateTime.Now) - DateTime.Today;
            string s = string.Format("ObsStart:{0},   ObsEnd:{1}\n", starttime, endtime);
            write_log(s);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            timeBeginPeriod(16);
            // ソケットクローズ
            udpc.Close();
            udpc2.Close();
            writer.Close();
        }
        private void write_log(string s)
        {
            richTextBox1.AppendText(s);
            writer.WriteLine(s);
        }

        #region UDP
        // 別スレッド処理（UDP） //IP 192.168.1.209
        private void worker_udp_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = (BackgroundWorker)sender;

            //バインドするローカルポート番号
            int localPort = mmFsiUdpPortKV1000SpCam2; //  24410;// broadcast mmFsiUdpPortMT3IDS2;
            System.Net.Sockets.UdpClient udpc4 = null; ;
            try
            {
                udpc4 = new System.Net.Sockets.UdpClient(localPort);
            }
            catch (Exception ex)
            {
                //匿名デリゲートで表示する
                //this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, ex.ToString() });
                ShowRTextFW(ex.ToString());
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
                byte[] rcvBytes = udpc4.Receive(ref remoteEP);

                if (remoteEP.Address.ToString() == remoteHost && remoteEP.Port == remotePort )
                {
                    //kd = ToStruct1(rcvBytes);
                    //bw.ReportProgress(0, kd);

                    string rcvMsg = enc.GetString(rcvBytes);
                    str = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + "受信したデータ:[" + rcvMsg + "]\n";
                    ShowRTextFW(str);
                    //this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, str });
                }
                 else
                {
                    string rcvMsg = enc.GetString(rcvBytes);
                    str = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + "送信元アドレス:{0}/ポート番号:{1}/Size:{2}\n" + remoteEP.Address + "/" + remoteEP.Port + "/" + rcvBytes.Length + "[" + rcvMsg + "]\n";
                    ShowRTextFW(str);
                    //this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, str });
                }

                //データを送信する
                if (cmd_str_f != 0)
                {
                    //送信するデータを読み込む
                    string sendMsg = cmd_str + "\r";// "test送信するデータ";
                    byte[] sendBytes = enc.GetBytes(sendMsg);

                    //リモートホストを指定してデータを送信する
                    udpc4.Send(sendBytes, sendBytes.Length, remoteHost, remotePort);
                    cmd_str_f = 0;
                }
            }

            //UDP接続を終了
            udpc4.Close();
        }
        #endregion

        private void button_test_Click(object sender, EventArgs e)
        {
            cmd_str_f = 1;

            int idd = 32765;
            sw.Start();
            for (short i = 0; i < 100; i++)
            {
                idd++;
                //this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, idd });
                ShowRTextFW(idd.ToString());
                Pid_Data_Set_Wide((short)((i & 32767)), +i / 1000.0, -i / 1000.0, i / 1000.0); // 32767 == 7FFF
                Pid_Data_Set_Fine((short)((i & 32767)), +i / 1000.0, -i / 1000.0, i / 1000.0); // 32767 == 7FFF
                int j = i % 5;
                if (j == 0) Pid_Data_Send_KV1000("192.168.1.11"); //UDP2 
                if (j == 1) Pid_Data_Send_KV1000("192.168.1.12"); //UDP3 
                if (j == 2) Pid_Data_Send_KV1000("192.168.1.10"); //UDP1 
                if (j == 3) Pid_Data_Send_KV1000("192.168.1.11"); //UDP2 
                if (j == 4) Pid_Data_Send_KV1000("192.168.1.12"); //UDP3 

                //Pid_Data_Send_cmd_KV1000((short)-(short)((idd & 32767)), 32.765 + i / 1000.0, -32.765 - i / 1000.0); // 32767 == 7FFF
                System.Threading.Thread.Sleep(9); // 9ms->111Hz   13ms->75Hz
            }
            sw.Stop();
            long millisec = sw.ElapsedMilliseconds;
            //this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, millisec.ToString()+"ms\n" });
            ShowRTextFW(millisec.ToString() + "ms\n");

          //  Pid_Data_Send_cmd_KV1000((short)( 100 ), 32.765 , -32.765 ); // 32767 == 7FFF

        }

        private void send_udp_data()
        {
            if (udp_send_on == 1)
            {
                int j = udp_id % 5;
                if (j == 0) Pid_Data_Send_KV1000("192.168.1.11"); //UDP2 
                if (j == 1) Pid_Data_Send_KV1000("192.168.1.12"); //UDP3 
                if (j == 2) Pid_Data_Send_KV1000("192.168.1.10"); //UDP1 
                if (j == 3) Pid_Data_Send_KV1000("192.168.1.11"); //UDP2 
                if (j == 4) Pid_Data_Send_KV1000("192.168.1.12"); //UDP3 

                ++udp_id;
                if (udp_id >= udp_id_next) udp_send_on = 0;
            }
        }

        private void timer_udp_Tick(object sender, EventArgs e)
        {
            send_udp_data();
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

        /// <summary>
        /// インチング
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_px_MouseDown(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("ST 01212\r");
            if (radioButton_MT2.Checked) s1 = string.Format("ST 01012\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        private void button_px_MouseUp(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("RS 01212\r");
            if (radioButton_MT2.Checked) s1 = string.Format("RS 01012\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        private void button_mx_MouseDown(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("ST 01213\r");
            if (radioButton_MT2.Checked) s1 = string.Format("ST 01013\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        private void button_mx_MouseUp(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("RS 01213\r");
            if (radioButton_MT2.Checked) s1 = string.Format("RS 01013\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        private void button_py_MouseDown(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("ST 01214\r");
            if (radioButton_MT2.Checked) s1 = string.Format("ST 01014\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        private void button_py_MouseUp(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("RS 01214\r");
            if (radioButton_MT2.Checked) s1 = string.Format("RS 01014\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        private void button_my_MouseDown(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("ST 01215\r");
            if (radioButton_MT2.Checked) s1 = string.Format("ST 01015\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        private void button_my_MouseUp(object sender, MouseEventArgs e)
        {
            string s1 = string.Format("RS 01215\r");
            if (radioButton_MT2.Checked) s1 = string.Format("RS 01015\r");
            Send_R_ON_cmd_KV1000(s1);
        }

        /// <summary>
        /// 観測時刻にMTfine, Fish2 起動
        /// </summary>
        private void timerObsOnOff_Tick(object sender, EventArgs e)
        {
            TimeSpan nowtime = DateTime.Now - DateTime.Today;
            //TimeSpan endtime = new TimeSpan(7, 0, 0);
            //TimeSpan starttime = new TimeSpan(18,05, 0); //17 3 0
            string s = "KV1000SpCam_log_"+ DateTime.Today.ToString("dd") + ".txt";

            if (nowtime.CompareTo(endtime) >= 0 && nowtime.CompareTo(starttime) <= 0)
            {
                // DayTime
                if (this.States == Daytime && checkBoxObsAuto.Checked)
                {
                    richTextBox1.SaveFile(@"E:\log\"+s, RichTextBoxStreamType.PlainText);
                }
                this.States = Daytime;
            }
            else
            {
                //NightTime
                if (this.States == Daytime && checkBoxObsAuto.Checked)
                {
                    //System.Diagnostics.Process p1 = System.Diagnostics.Process.Start(@"""C:\Users\root\Documents\Visual Studio 2010\Projects\MT3Fine\PictureViewer\bin\Release\MT3Fine.exe""");
                    //"C:\Users\root\Documents\Visual Studio 2013\Projects\MT3BaslerAce640\PictureViewer\bin\Release\MT3BaslerAce.exe" /is 8
                    System.Diagnostics.Process p1 = System.Diagnostics.Process.Start(@"""C:\Users\root\Documents\Visual Studio 2013\Projects\MT3BaslerAce640\PictureViewer\bin\Release\MT3BaslerAce.exe""", "/is 8");
                    //System.Diagnostics.Process p2 = System.Diagnostics.Process.Start(@"""C:\Users\root\Documents\Visual Studio 2013\Projects\MT3BaslerAce640\PictureViewer\bin\Release\MT3BaslerAce.exe""", "/ID 7");
                    System.Diagnostics.Process p2 = System.Diagnostics.Process.Start(@"""C:\Users\root\Source\Repos\MT3BaslerAce6403\PictureViewer\bin\Release\MT3BaslerAce.exe""", "/ID 7");
                    System.Diagnostics.Process p3 = System.Diagnostics.Process.Start("C:\\Tool\\MagicSend.exe", "44-8A-5B-72-01-8A"); //MJ34LL      ID 10,11
                    System.Diagnostics.Process p6 = System.Diagnostics.Process.Start("C:\\Tool\\MagicSend.exe", "B4-B5-2F-CD-FF-E1"); //HP6300SFF-3 ID 1
                }
                this.States = Nighttime;
            }
        }

        private void button_SetMT2Pos_Click(object sender, EventArgs e)
        {
            mt2az = Convert.ToDouble(textBox_MT2Az.Text);
            mt2alt = Convert.ToDouble(textBox_MT2Alt.Text);
            mt2zaz = Convert.ToDouble(textBox_MT2ZAz.Text);
            mt2zdt = Convert.ToDouble(textBox_MT2ｄZT.Text);

            double az_zc, alt_zc;
            z_correct(mt2az, mt2alt, mt2zaz, mt2zdt, out az_zc, out alt_zc);
            string s = string.Format("Az:{0,0:F2}, {1,0:F2}  {2,0:F2}, {3,0:F2}  ans:{4,0:F2}, {5,0:F2}\n", mt2az, mt2alt, mt2zaz, mt2zdt, az_zc, alt_zc);
            write_log(s);

            UInt32 xpos = 180000;
            UInt32 ypos = 90000;
            UInt32 xpos_kv = udpkv.EndianChange(xpos);
            s = string.Format("xpos:{0}, {1}\n", xpos,xpos_kv);
            
            // DM6 Az,Alt位置決めデータ
            string s1 = string.Format("WRS DM00006 4 {0:00000} {1:00000} {2:00000} {3:00000}\r",udpkv.TUInt2UShort_L(xpos),udpkv.TUInt2UShort_U(xpos),udpkv.TUInt2UShort_L(ypos),udpkv.TUInt2UShort_U(ypos));
            Send_R_ON_cmd_KV1000(s1);

            write_log(s);
        }

        private void timerDisp_Tick(object sender, EventArgs e)
        {
            string s = string.Format("TC:[{0:D5}] MT2:({1:D5},{2:D5})v({3:D5},{4:D5}) MT3:({5:D5},{6:D5})v({7:D5},{8:D5})\n", udpkv.udp_time_code, udpkv.xpos, udpkv.ypos,udpkv.x1v,udpkv.y1v,udpkv.x2pos,udpkv.y2pos,udpkv.x2v,udpkv.y2v);
            toolStripStatusLabelX1pos.Text = s;

            label_x2pos.Text = udpkv.x2pos.ToString();
            label_y2pos.Text = udpkv.y2pos.ToString();

            label_wide_f.Text    = kv_pid_data_r.wide_id.ToString("00000");
            label_wide_daz.Text  = kv_pid_data_r.wide_az.ToString();
            label_wide_dalt.Text = kv_pid_data_r.wide_alt.ToString();
            label_wide_vk.Text   = kv_pid_data_r.wide_vk.ToString();
        }

        /// <summary>
        /// MTmon status 送信ルーチン
        /// </summary>
        /// <remarks>
        /// MTmon status send
        /// </remarks>
        private void MTmon_Data_Send(object sender)
        {
            // MTmon status for UDP
            //データを送信するリモートホストとポート番号
            string remoteHost = mmFsiCore_i5;
            int remotePort = mmFsiUdpPortMTmonitor;
            //送信するデータを読み込む
            mtmon_data.id = (byte)mt_mon_id;
            diskspace = cDrive.TotalFreeSpace;
            mtmon_data.diskspace = (int)(diskspace / (1024 * 1024 * 1024));
            if (id == id_mon)
            {
                mtmon_data.obs = (byte)0; // STOP;
            }
            else
            {
                mtmon_data.obs = (byte)this.States;
            }
            id_mon = id;
            
            // とりあえず
            mtmon_data.obs = 2 ; 

            byte[] sendBytes = ToBytes(mtmon_data);

            try
            {
                //リモートホストを指定してデータを送信する
                udpc2.Send(sendBytes, sendBytes.Length, remoteHost, remotePort);
            }
            catch (Exception ex)
            {
                //匿名デリゲートで表示する
                //this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, ex.ToString() });
                ShowRTextFW(ex.ToString());
            }
        }
        static byte[] ToBytes(MT_MONITOR_DATA obj)
        {
            int size = Marshal.SizeOf(typeof(MT_MONITOR_DATA));
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(obj, ptr, false);
            byte[] bytes = new byte[size];
            Marshal.Copy(ptr, bytes, 0, size);
            Marshal.FreeHGlobal(ptr);
            return bytes;
        }

        private void timerMTmonSend_Tick(object sender, EventArgs e)
        {
            MTmon_Data_Send(sender);
        }


     }
}
