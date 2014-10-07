using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace KV1000SpCam
{
    public partial class Form1 : Form
    {
//        string KV_remoteHost = "192.168.1.10"; // KV1000;
//        int KV_remotePort = 8503;  // 8503 UDP  8501 CMD
        KV_DATA kd = new KV_DATA();
        Udp udpkv = new Udp(7777);

        public String rstr;
        delegate void dlgSetString(object lbl, string text);
        //デリゲートで別スレッドから呼ばれてラベルに現在の時間又は
        //ストップウオッチの時間を表示する
        private void ShowRText(object sender, string str)
        {
            RichTextBox rtb = (RichTextBox)sender;　//objectをキャストする
            rtb.Focus();
            rtb.AppendText(str);
        }
        private void ShowLabelText(object sender, string str)
        {
            Label rtb = (Label)sender;　//objectをキャストする
            rtb.Focus();
            rtb.Text = str;
        }

        /// <summary>
        /// 非同期受信に成功した場合に呼び出されます。
        /// </summary>
        /// <param name="ar"></param>
        public void ReceiveCallback(IAsyncResult AR)
        {
            // UDP/IP受信
            System.Net.IPEndPoint ipAny = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);
            Byte[] rdat = ((System.Net.Sockets.UdpClient)AR.AsyncState).EndReceive(AR, ref ipAny);

/*            // KV_DATA
            if (ipAny.Address.ToString() == KV_remoteHost && ipAny.Port == KV_remotePort && rdat.Length == Marshal.SizeOf(kd))
            {
                // data 転送
                string remoteHost = "192.168.1.204";
                int remotePort = 24411; //Fine
                udpc2.Send(rdat, rdat.Length, remoteHost, remotePort);

                remoteHost = "192.168.1.204";
                remotePort = 24422; //SFine
                udpc2.Send(rdat, rdat.Length, remoteHost, remotePort);
*/

             // KV_DATA from local
            if (ipAny.Address.ToString() == "127.0.0.1" && rdat.Length == Marshal.SizeOf(kd))
            {
                udpkv.set_udp_kv_data(rdat, ref kd);
                udpkv.cal_mt3(kd);
                Invoke(new dlgSetString(ShowLabelText), new object[] { label_x2pos, udpkv.x2pos.ToString() });
                Invoke(new dlgSetString(ShowLabelText), new object[] { label_y2pos, udpkv.y2pos.ToString() });
            }
            else           
            // KV_PID_DATA
            if (rdat.Length == Marshal.SizeOf(kv_pid_data_r))
            {
                udpkv.set_udp_pid_data(rdat, ref kv_pid_data_r);
                Pid_Data_Set(kv_pid_data_r);
                udp_send_on = 1; // pid data 送信実行
                udp_id_next = udp_id + 50;
            }
            else // それ以外
            {
                String rrstr = System.Text.Encoding.GetEncoding("SHIFT-JIS").GetString(rdat);
                string s = "R:(" + rdat.Length +")"+ ipAny.Address + "(" + ipAny.Port.ToString() + ")";
                rstr = LogString(rrstr, s);
                Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, rstr });
                //MessageBox.Show(rstr);
            }
            // 連続で(複数回)データ受信する為の再設定
            ((System.Net.Sockets.UdpClient)AR.AsyncState).BeginReceive(ReceiveCallback, AR.AsyncState);
        }

        public string LogString(string s1, string s="R")
        {
            string s2 = s1.Replace("\r", "\\r");
            string s3 = s2.Replace("\n", "\\n");
            return  DateTime.Now.ToString("yyyyMMdd_HHmmss_fff ") + s + " [" + s3 + "]\n";
        }
        /// <summary>
        /// PID 受信データ処理
        /// </summary>
        private void Pid_Data_Set(KV_PID_DATA kpdr)
        {
            //IDがゼロでなければコピー
            if (kpdr.wide_id != 0)
            {
                kv_pid_data.wide_time = kpdr.wide_time;
                kv_pid_data.wide_id   = kpdr.wide_id;
                kv_pid_data.wide_az   = kpdr.wide_az;
                kv_pid_data.wide_alt  = kpdr.wide_alt;
                kv_pid_data.wide_vk   = kpdr.wide_vk;
            }
            if (kpdr.fine_id != 0)
            {
                kv_pid_data.fine_time = kpdr.fine_time;
                kv_pid_data.fine_id   = kpdr.fine_id;
                kv_pid_data.fine_az   = kpdr.fine_az;
                kv_pid_data.fine_alt  = kpdr.fine_alt;
                kv_pid_data.fine_vk   = kpdr.fine_vk;
            }
            if (kpdr.sf_id != 0)
            {
                kv_pid_data.sf_time = kpdr.sf_time;
                kv_pid_data.sf_id   = kpdr.sf_id;
                kv_pid_data.sf_az   = kpdr.sf_az;
                kv_pid_data.sf_alt  = kpdr.sf_alt;
                kv_pid_data.sf_vk   = kpdr.sf_vk;
            }
        }

        /// <summary>
        /// PID data送信データセット(for test)
        /// </summary>
        private void Pid_Data_Set_Wide(short id, double daz, double dalt, double vk)
        {
            //送信するデータを読み込む
            kv_pid_data.wide_time = udpkv.EndianChange((short)udpkv.udp_time_code);
            kv_pid_data.wide_id = udpkv.EndianChange(id);
            kv_pid_data.wide_az = udpkv.EndianChange(udpkv.PIDPV_makedata(daz));
            kv_pid_data.wide_alt = udpkv.EndianChange(udpkv.PIDPV_makedata(dalt));
            kv_pid_data.wide_vk = udpkv.EndianChange(udpkv.PIDPV_makedata(vk));

            label_wide_f.Text = kv_pid_data.wide_id.ToString() + " " + kv_pid_data.wide_time.ToString("00000");
            label_wide_daz.Text = kv_pid_data.wide_az.ToString();
            label_wide_dalt.Text = kv_pid_data.wide_alt.ToString();
        }
        /// <summary>
        /// PID data送信データセット
        /// </summary>
        private void Pid_Data_Set_Fine(short id, double daz, double dalt, double vk)
        {
            //送信するデータを読み込む
            kv_pid_data.fine_time = udpkv.EndianChange((short)udpkv.udp_time_code); 
            kv_pid_data.fine_id   = udpkv.EndianChange(id);
            kv_pid_data.fine_az   = udpkv.EndianChange(udpkv.PIDPV_makedata(daz));
            kv_pid_data.fine_alt  = udpkv.EndianChange(udpkv.PIDPV_makedata(dalt));
            kv_pid_data.fine_vk   = udpkv.EndianChange(udpkv.PIDPV_makedata(vk));

            label_fine_f.Text    = kv_pid_data.fine_id.ToString() + " " + kv_pid_data.fine_time.ToString("00000");
            label_fine_daz.Text  = kv_pid_data.fine_az.ToString();
            label_fine_dalt.Text = kv_pid_data.fine_alt.ToString();
        }
        /// <summary>
        /// PID data送信ルーチン(KV1000 UDPバイナリ)
        /// </summary>
        ///   KV1000 LE20(1): "192.168.1.10"    KV1000 LE20(2): "192.168.1.11"
        private void Pid_Data_Send_KV1000( string remoteHost = "192.168.1.10", int remotePort = 8503)
        {
            // PID data send for UDP
            //データを送信するリモートホストとポート番号
            //string remoteHost = "192.168.1.10";
            //int remotePort = 8503; //KV1000 UDP   8501(KV1000 cmd); // KV1000SpCam

            byte[] sendBytes = udpkv.ToBytes(kv_pid_data);

            try
            {
                //リモートホストを指定してデータを送信する
                udpc.Send(sendBytes, sendBytes.Length, remoteHost, remotePort);
            }
            catch (Exception ex)
            {
                //匿名デリゲートで表示する
                this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, ex.ToString() });
            }
            string s = "S:" + remoteHost + "(" + remotePort.ToString() + ")";
            string s1 = kv_pid_data.wide_id.ToString() + " " + kv_pid_data.wide_az.ToString() + " " + kv_pid_data.wide_alt.ToString()+" " + kv_pid_data.wide_vk.ToString();
            string s2 = LogString(s1, s);
            this.Invoke(new dlgSetString(ShowLabelText), new object[] { label_UdpSendData, s2 });
            //this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, s2 });
        }

        /// <summary>
        /// PID data送信ルーチン(KV1000　上位リンク)　DM937
        /// </summary>
        private void Pid_Data_Send_cmd_KV1000(short id, double daz, double dalt)
        {
            // PID data send for cmd
            //データを送信するリモートホストとポート番号
            string remoteHost = "192.168.1.10";
            int remotePort = 8501; //KV1000 UDP   8501(KV1000 cmd); // KV1000SpCam

            //送信するデータを読み込む
            string s1 = string.Format("WRS DM937 3 {0} {1} {2}\r", (ushort)id, (ushort)udpkv.PIDPV_makedata(daz), (ushort)udpkv.PIDPV_makedata(dalt));
            byte[] sendBytes = Encoding.ASCII.GetBytes(s1);
 
            try
            {
                //リモートホストを指定してデータを送信する
                udpc.Send(sendBytes, sendBytes.Length, remoteHost, remotePort);
            }
            catch (Exception ex)
            {
                //匿名デリゲートで表示する
                this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, ex.ToString() });
            }
            string s = "S:" + remoteHost + "(" + remotePort.ToString() + ")";
            string s2 = LogString(s1, s);
            this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, s2 });
        }
        /// <summary>
        /// PID cmd送信ルーチン(KV1000　上位リンク)
        /// </summary>
        private void Send_R_ON_cmd_KV1000(string s1)
        {
            // PID data send for cmd
            //データを送信するリモートホストとポート番号
            string remoteHost = "192.168.1.10";
            int remotePort = 8501; //KV1000 UDP   8501(KV1000 cmd); // KV1000SpCam

            //送信するデータを読み込む
            //string s1 = string.Format("STWRS DM937 3 {0} {1} {2}\r", (ushort)id, (ushort)udpkv.PIDPV_makedata(daz), (ushort)udpkv.PIDPV_makedata(dalt));
            byte[] sendBytes = Encoding.ASCII.GetBytes(s1);

            try
            {
                //リモートホストを指定してデータを送信する
                udpc.Send(sendBytes, sendBytes.Length, remoteHost, remotePort);
            }
            catch (Exception ex)
            {
                //匿名デリゲートで表示する
                this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, ex.ToString() });
            }
            string s = "S:" + remoteHost + "(" + remotePort.ToString() + ")";
            string s2 = LogString(s1, s);
            this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, s2 });
        }
    }
}
