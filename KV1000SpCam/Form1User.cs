using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;
using MathNet.Numerics.LinearAlgebra.Double;

namespace KV1000SpCam
{
    public partial class Form1 : Form
    {
        //観測時間帯を表す定数
        const int Daytime = 0;
        const int Nighttime = 1;
        //上の状態を保持します
        int States = 0;
        TimeSpan starttime, endtime;

        Stopwatch sw = new Stopwatch();
        long elapsed0 = 0, elapsed1 = 0, elapsed2 = 0;
        double lap0 = 0, lap1 = 0, lap2 = 0, alpha = 0.01;
        static int udp_id = 0;
        static int udp_id_next = 50;  //UDP　送信回数（１起動毎の）
        static int udp_send_on = 0;
        string cmd_str = "RD DM11390";
        int cmd_str_f = 0;
        String udp_r;

        FSI_PID_DATA pid_data = new FSI_PID_DATA();
        MT_MONITOR_DATA mtmon_data = new MT_MONITOR_DATA();
        int mmFsiUdpPortKV1000SpCam2 = 24426; //24410;            // MT3IDS （受信）
        int mmFsiUdpPortKV1000SpCam2s = 24427; //24426;            // MT3IDS （送信）
        int mmFsiUdpPortMTmonitor = 24415;
        string mmFsiCore_i5 = "192.168.1.211";
        int mmFsiUdpPortSpCam = 24410;   // SpCam（受信）
        string mmFsiSC440 = "192.168.1.206";
        System.Net.Sockets.UdpClient udpc = null;
        System.Net.Sockets.UdpClient udpc2 = null;
        DriveInfo cDrive = new DriveInfo("C");
        long diskspace;

        double mt2az, mt2alt, mt2zaz, mt2zdt;
        
        //        string KV_remoteHost = "192.168.1.10"; // KV1000;
//        int KV_remotePort = 8503;  // 8503 UDP  8501 CMD
        KV_DATA kd = new KV_DATA();
        Udp udpkv = new Udp(7777);
        KV_PID_DATA kv_pid_data   = new KV_PID_DATA();  //送信用
        KV_PID_DATA kv_pid_data_r = new KV_PID_DATA();  //受信データ

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
                //Invoke(new dlgSetString(ShowLabelText), new object[] { label_x2pos, udpkv.x2pos.ToString() });
                //Invoke(new dlgSetString(ShowLabelText), new object[] { label_y2pos, udpkv.y2pos.ToString() });
            }
            else           
            // KV_PID_DATA
            if (rdat.Length == Marshal.SizeOf(kv_pid_data_r))
            {
                udpkv.set_udp_pid_data(rdat, ref kv_pid_data_r);

                udp_id_next = udp_id + 50;
                udp_send_on = 1; // pid data 送信実行
                Pid_Data_Set(kv_pid_data_r);

                String rrstr = " w:"+kv_pid_data_r.wide_id.ToString()+" f:"+ kv_pid_data_r.fine_id.ToString()+" "+kv_pid_data_r.fine_az.ToString() ;
                string s = "R:(" + rdat.Length + ")" + ipAny.Address + "(" + ipAny.Port.ToString() + ")";
                rstr = LogString(rrstr, s);
                Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, rstr });
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
            if (kpdr.mt2_wide_id != 0)
            {
                kv_pid_data.mt2_wide_time = kpdr.mt2_wide_time;
                kv_pid_data.mt2_wide_id   = kpdr.mt2_wide_id;
                kv_pid_data.mt2_wide_az   = kpdr.mt2_wide_az;
                kv_pid_data.mt2_wide_alt  = kpdr.mt2_wide_alt;
                kv_pid_data.mt2_wide_vk   = kpdr.mt2_wide_vk;
            }
            if (kpdr.wide_id != 0)
            {
                kv_pid_data.wide_time = kpdr.wide_time;
                kv_pid_data.wide_id   = kpdr.wide_id;
                kv_pid_data.wide_az   = kpdr.wide_az;
                kv_pid_data.wide_alt  = kpdr.wide_alt;
                kv_pid_data.wide_vk   = kpdr.wide_vk;
            }
            if (kpdr.sf_id != 0)
            {
                kv_pid_data.sf_time = kpdr.sf_time;
                kv_pid_data.sf_id = kpdr.sf_id;
                kv_pid_data.sf_az = kpdr.sf_az;
                kv_pid_data.sf_alt = kpdr.sf_alt;
                kv_pid_data.sf_vk = kpdr.sf_vk;
            }
            if (kpdr.fine_id != 0)
            {
                kv_pid_data.fine_time = kpdr.fine_time;
                kv_pid_data.fine_id   = kpdr.fine_id;
                kv_pid_data.fine_az   = kpdr.fine_az;
                kv_pid_data.fine_alt  = kpdr.fine_alt;
                kv_pid_data.fine_vk   = kpdr.fine_vk;

                send_udp_data();
                udp_send_on = 0;
            }
        }

        /// <summary>
        /// PID data送信データセット(Wide)
        /// </summary>
        private void Pid_Data_Set_Wide(short id, double daz, double dalt, double vk)
        {
            //送信するデータを読み込む
            kv_pid_data.wide_time = udpkv.EndianChange((short)udpkv.udp_time_code);
            kv_pid_data.wide_id = udpkv.EndianChange(id);
            kv_pid_data.wide_az = udpkv.EndianChange(udpkv.PIDPV_makedata(daz));
            kv_pid_data.wide_alt = udpkv.EndianChange(udpkv.PIDPV_makedata(dalt));
            kv_pid_data.wide_vk = udpkv.EndianChange(udpkv.PIDPV_makedata(vk));

            label_wide_f.Text = id.ToString("00000") + " " + ((short)udpkv.udp_time_code).ToString("00000");
            label_wide_daz.Text  = udpkv.PIDPV_makedata(daz).ToString();
            label_wide_dalt.Text = udpkv.PIDPV_makedata(dalt).ToString();
            label_wide_vk.Text   = udpkv.PIDPV_makedata(vk).ToString();
        }
        /// <summary>
        /// PID data送信データセット(fine)
        /// </summary>
        private void Pid_Data_Set_Fine(short id, double daz, double dalt, double vk)
        {
            //送信するデータを読み込む
            kv_pid_data.fine_time = udpkv.EndianChange((short)udpkv.udp_time_code);
            kv_pid_data.fine_id = udpkv.EndianChange(id);
            kv_pid_data.fine_az = udpkv.EndianChange(udpkv.PIDPV_makedata(daz));
            kv_pid_data.fine_alt = udpkv.EndianChange(udpkv.PIDPV_makedata(dalt));
            kv_pid_data.fine_vk = udpkv.EndianChange(udpkv.PIDPV_makedata(vk));

            label_fine_f.Text = id.ToString("00000") + " " + ((short)udpkv.udp_time_code).ToString("00000");
            label_fine_daz.Text  = udpkv.PIDPV_makedata(daz).ToString();
            label_fine_dalt.Text = udpkv.PIDPV_makedata(dalt).ToString();
            label_fine_vk.Text   = udpkv.PIDPV_makedata(vk).ToString();
        }
        /// <summary>
        /// PID data送信データセット(sfine)
        /// </summary>
        private void Pid_Data_Set_SF(short id, double daz, double dalt, double vk)
        {
            //送信するデータを読み込む
            kv_pid_data.sf_time = udpkv.EndianChange((short)udpkv.udp_time_code); 
            kv_pid_data.sf_id   = udpkv.EndianChange(id);
            kv_pid_data.sf_az   = udpkv.EndianChange(udpkv.PIDPV_makedata(daz));
            kv_pid_data.sf_alt  = udpkv.EndianChange(udpkv.PIDPV_makedata(dalt));
            kv_pid_data.sf_vk   = udpkv.EndianChange(udpkv.PIDPV_makedata(vk));

            label_sf_f.Text    = id.ToString("00000") + " " + ((short)udpkv.udp_time_code).ToString("00000");
            label_sf_daz.Text  = udpkv.PIDPV_makedata(daz).ToString();
            label_sf_dalt.Text = udpkv.PIDPV_makedata(dalt).ToString();
            label_sf_vk.Text   = udpkv.PIDPV_makedata(vk).ToString();
        }
        /// <summary>
        /// PID data送信データセット(MT2 Wide)
        /// </summary>
        private void Pid_Data_Set_MT2Wide(short id, double daz, double dalt, double vk)
        {
            //送信するデータを読み込む
            kv_pid_data.mt2_wide_time = udpkv.EndianChange((short)udpkv.udp_time_code);
            kv_pid_data.mt2_wide_id   = udpkv.EndianChange(id);
            kv_pid_data.mt2_wide_az   = udpkv.EndianChange(udpkv.PIDPV_makedata(daz));
            kv_pid_data.mt2_wide_alt  = udpkv.EndianChange(udpkv.PIDPV_makedata(dalt));
            kv_pid_data.mt2_wide_vk   = udpkv.EndianChange(udpkv.PIDPV_makedata(vk));

            label_MT2wide_f.Text      = id.ToString("00000") + " " + ((short)udpkv.udp_time_code).ToString("00000");
            label_MT2wide_daz.Text    = udpkv.PIDPV_makedata(daz).ToString();
            label_MT2wide_dalt.Text   = udpkv.PIDPV_makedata(dalt).ToString();
            label_MT2wide_vk.Text     = udpkv.PIDPV_makedata(vk).ToString();
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
            string s1 = kv_pid_data.wide_id.ToString() + " " + kv_pid_data.wide_az.ToString() + " " + kv_pid_data.wide_alt.ToString() + " " + kv_pid_data.wide_vk.ToString();
            s1       += kv_pid_data.fine_id.ToString() + " " + kv_pid_data.fine_az.ToString() + " " + kv_pid_data.fine_alt.ToString() + " " + kv_pid_data.fine_vk.ToString();
            string s2 = LogString(s1, s);
          //  this.Invoke(new dlgSetString(ShowLabelText), new object[] { label_UdpSendData, s2 });
          //  this.Invoke(new dlgSetString(ShowRText), new object[] { richTextBox1, s2 });
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
        /// <summary>
        /// 地平座標->方向余弦
        /// </summary>
        public Vector eq_directional_cosine(double az, double alt )
        {
            var v = Vector.Build.Dense(3);
            //DenseVector ve = ve.Dense(10);
            const double RAD = Math.PI/180.0 ;
            
            // 地平座標の方向余弦
            v[0] = Math.Cos(alt * RAD) * Math.Cos(az * RAD);
            v[1] =-Math.Cos(alt * RAD) * Math.Sin(az * RAD);
            v[2] = Math.Sin(alt * RAD);

            return (Vector)v;
        }
        /// <summary>
        /// 地平座標<-方向余弦
        /// </summary>
        public void eq_rev_directional_cosine(Vector v, out double az, out double alt)
        {            
            const double RAD = Math.PI / 180.0;

            alt = Math.Asin(v[2])/RAD ;
            az = 0;
            if (Math.Abs(v[0]) < 1e-9)
            {
                if (-v[1] >= 0) az = 90;
                if (-v[1] < 0) az = -90;
            }
            else
            {
                az = Math.Atan2(-v[1], v[0]) / RAD;
            }

            while (az < 0) az += 360;
            while (az >= 360) az -= 360;
        }
        /// <summary>
        /// X軸回転
        /// </summary>
        public Matrix Rotate_X(double theta)
        {
            var m = Matrix.Build.Dense(3, 3);
            const double RAD = Math.PI / 180.0;
            double sinth = Math.Sin(theta * RAD);
            double costh = Math.Cos(theta * RAD);

            m[0, 0] = 1;
            m[0, 1] = 0;
            m[0, 2] = 0;

            m[1, 0] = 0;
            m[1, 1] = costh;
            m[1, 2] = -sinth;

            m[2, 0] = 0;
            m[2, 1] = sinth;
            m[2, 2] = costh;

            return (Matrix)m;
        }
        /// <summary>
        /// Y軸回転
        /// </summary>
        public Matrix Rotate_Y(double theta)
        {
            var m = Matrix.Build.Dense(3, 3);
            const double RAD = Math.PI / 180.0;
            double sinth = Math.Sin(theta * RAD);
            double costh = Math.Cos(theta * RAD);

            m[0, 0] = costh;
            m[0, 1] = 0;
            m[0, 2] = sinth;

            m[1, 0] = 0;
            m[1, 1] = 1;
            m[1, 2] = 0;

            m[2, 0] = -sinth;
            m[2, 1] = 0;
            m[2, 2] = costh;

            return (Matrix)m;
        }
        /// <summary>
        /// Z軸回転
        /// </summary>
        public Matrix Rotate_Z(double theta)
        {
            var m = Matrix.Build.Dense(3,3);
            const double RAD = Math.PI / 180.0;
            double sinth = Math.Sin(theta * RAD);
            double costh = Math.Cos(theta * RAD);

            m[0, 0] = costh;
            m[0, 1] = -sinth;
            m[0, 2] = 0;

            m[1, 0] = sinth;
            m[1, 1] = costh;
            m[1, 2] = 0;

            m[2, 0] = 0;
            m[2, 1] = 0;
            m[2, 2] = 1;

            return (Matrix)m;
        }
        /// <summary>
        /// Az軸方向と天頂との誤差の補正
        /// Zaz：天頂から見たAz軸方位(deg)
        /// dat：天頂から見たAz軸距離(deg)
        /// </summary>
        public void z_correct(double az, double alt, double zaz, double dzt, out double az_zc, out double alt_zc)
        {
            Vector vt = eq_directional_cosine(az, alt);

            Matrix my = Rotate_Y(-dzt); // 天頂づれ補正　dzt:北側が＋
            Matrix mz1 = Rotate_Z(zaz); // 
            Matrix mz2 = Rotate_Z(-zaz); // 

            var v1 = mz1.Multiply(vt);
            eq_rev_directional_cosine((Vector)v1, out az_zc, out alt_zc);
            var v2 = my.Multiply(v1);
            eq_rev_directional_cosine((Vector)v2, out az_zc, out alt_zc);
            var v3 = mz2.Multiply(v2);
            eq_rev_directional_cosine((Vector)v3, out az_zc, out alt_zc);
        }
        /// <summary>
        /// Az軸とAlt軸の直交誤差の補正
        /// </summary>
        public void azalt_correct(double az, double alt, double zaz, double dzt, out double az_zc, out double alt_zc)
        {
            Vector vt = eq_directional_cosine(az, alt);

            Matrix my = Rotate_Y(-dzt); // 天頂づれ補正　dzt:北側が＋
            Matrix mz1 = Rotate_Z(zaz); // 
            Matrix mz2 = Rotate_Z(-zaz); // 

            var v1 = mz1.Multiply(vt);
            eq_rev_directional_cosine((Vector)v1, out az_zc, out alt_zc);
            var v2 = my.Multiply(v1);
            eq_rev_directional_cosine((Vector)v2, out az_zc, out alt_zc);
            var v3 = mz2.Multiply(v2);
            eq_rev_directional_cosine((Vector)v3, out az_zc, out alt_zc);
        }

    }
}
