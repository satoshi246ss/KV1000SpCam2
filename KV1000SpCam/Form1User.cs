using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KV1000SpCam
{
    public partial class Form1 : Form
    {
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

        /// <summary>
        /// 非同期受信に成功した場合に呼び出されます。
        /// </summary>
        /// <param name="ar"></param>
        public void ReceiveCallback(IAsyncResult AR)
        {
            // UDP/IP受信
            System.Net.IPEndPoint ipAny = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);
            Byte[] rdat = ((System.Net.Sockets.UdpClient)AR.AsyncState).EndReceive(AR, ref ipAny);
            String rrstr = System.Text.Encoding.GetEncoding("SHIFT-JIS").GetString(rdat);
            string s2 = rrstr.Replace("\r", "\\r");
            string s3 = s2.Replace("\n", "\\n");

            rstr = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + " R:[" + s3 + "]\n";
            Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, rstr });
            //MessageBox.Show(rstr);

            // 連続で(複数回)データ受信する為の再設定
            ((System.Net.Sockets.UdpClient)AR.AsyncState).BeginReceive(ReceiveCallback, AR.AsyncState);
        }

        /// <summary>
        /// PID data送信ルーチン(KV1000 UDPバイナリ) DM937
        /// </summary>
        private void Pid_Data_Send_KV1000(short id, double daz, double dalt)
        {
            // PID data send for UDP
            //データを送信するリモートホストとポート番号
            string remoteHost = "192.168.1.10";
            int remotePort = 8503; //KV1000 UDP   8501(KV1000 cmd); // KV1000SpCam

            //送信するデータを読み込む
            string s1 = string.Format("WRS DM937 3 {0} {1} {2}\r", (ushort)id, udpkv.PIDPV_makedata(daz), udpkv.PIDPV_makedata(dalt));
            //byte[] sendBytes = Encoding.ASCII.GetBytes(s1);
            kv_pid_data.wide_id = udpkv.EndianChange(id);
            kv_pid_data.wide_az = udpkv.EndianChange(udpkv.PIDPV_makedata(daz));
            kv_pid_data.wide_alt = udpkv.EndianChange(udpkv.PIDPV_makedata(dalt));

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

            this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, s1 });
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
            string s1 = string.Format("WRS DM937 3 {0} {1} {2}\r", (ushort)id, udpkv.PIDPV_makedata(daz), udpkv.PIDPV_makedata(dalt));
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

            this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, s1 });
        }
    }
}
