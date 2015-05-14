using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KV1000SpCam
{
    // C++ 構造体のマーシャリング
    [StructLayout(LayoutKind.Sequential)]
    public struct FSI_DATA
    {
        public UInt16 id;    // unsigned short
        public Byte   cam_id; //unsigned char
        public Byte   fsi_pos;
        public Byte   cmd;
        public Byte   wdt;
        public Int16  mag;
        public Double t;
        public Single az;
        public Single alt;
        public Single vaz;
        public Single valt;
        public Single az_c;
        public Single alt_c;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MOTOR_DATA_KV_SP
    {
        public Int32 cmd;      // コマンド  １：検出  16: Meteor Lost
        public Double t;       // 送信時刻
        public Single az;      // 目標方位、南が０度、西回り
        public Single alt;     // 目標高度、天頂が90度、地平が0度
        public Single vaz;
        public Single valt;
        public Single theta;
    }

    // min 64byte
    [StructLayout(LayoutKind.Sequential)]
    public struct FSI_PID_DATA
    {
        public UInt16 swid;         // ソフトウェアID
        public UInt16 id;           // パケットID
        public UInt16 vmax;         // [count]カウント値
        public Double t;            // 送信時刻
        public Single dx;           // [pix] 中心からの誤差(方位方向)　重心位置
        public Single dy;           // [pix] 中心からの誤差(高度方向)　

        public Single az;           // [deg] wide位置(方位方向)　重心位置
        public Single alt;          // [deg] wide位置(高度方向)　重心位置
        public Single vaz;          // [deg/s] 速度推定値(方位方向)　カルマンフィルタ
        public Single valt;         // [deg/s] 速度推定値(高度方向)　カルマンフィルタ

        public Byte kalman_state; // 0:無効　1:有効　
        public Byte dum1;         // 予備　
        public Byte dum1a;        // 予備　
        public Byte dum1b;        // 予備　
        public Single dum2;        // 予備　
        public Single dum3;        // 予備　
        public Single dum4;        // 予備　
        public Single dum5;        // 予備　
        public Single dum6;        // 予備　
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KV_DATA
    {
        public Byte x1, x0, x3, x2;   //unsigned char  xpos data req
        public Byte y1, y0, y3, y2;   //unsigned char  ypos status
        public Byte xx1, xx0, xx3, xx2;   //unsigned char  x2pos 
        public Byte yy1, yy0, yy3, yy2;   //unsigned char  y2pos 
        public Byte v11, v10, v13, v12;   //unsigned short x1v, y1v 
        public Byte v21, v20, v23, v22;   //unsigned short x2v, y2v
        public UInt16 UdpTimeCode;   //unsigned short x2v, y2v
        //public UInt16 x1v, y1v, x2v, y2v;    //unsigned short v
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KV_PID_DATA
    {
        public Int16 wide_time;     // 12[time code] wide Udp time code [ms]
        public Int16 wide_id;       // 34[+-ID]  wide ID
        public Int16 wide_az;       // 56[mmdeg] wide位E置u(方u位E方u向u)　@重d心S位E置u
        public Int16 wide_alt;      // 78[mmdeg] wide位E置u(高?度x方u向u)　@重d心S位E置u
        public Int16 wide_vk;       // 90[pix/fr] wide velosity(kalman filter sqrt(vx2+vy2)
        public Int16 fine_time;     // 12[time code] fine Udp time code [ms]
        public Int16 fine_id;       // 34[+-ID]   fine ID
        public Int16 fine_az;       // 56[mmdeg]  fine 位E置u(方u位E方u向u)　@重d心S位E置u
        public Int16 fine_alt;      // 78[mmdeg]  fine 位E置u(高?度x方u向u)　@重d心S位E置u
        public Int16 fine_vk;       // 90[pix/fr] fine velosity(kalman filter sqrt(vx2+vy2)
        public Int16 sf_time;       // 12[time code] sf Udp time code [ms]
        public Int16 sf_id;         // 34[+-ID]   sf ID
        public Int16 sf_az;         // 56[mmdeg]  sf 位E置u(方u位E方u向u)　@重d心S位E置u
        public Int16 sf_alt;        // 78[mmdeg]  sf 位E置u(高?度x方u向u)　@重d心S位E置u
        public Int16 sf_vk;         // 90[pix/fr] sf velosity(kalman filter sqrt(vx2+vy2)
        public Int16 mt2_wide_time; // 12[time code] MT2 wide Udp time code [ms]
        public Int16 mt2_wide_id;   // 34[+-ID]  wide ID
        public Int16 mt2_wide_az;   // 56[mmdeg] wide位E置u(方u位E方u向u)　@重d心S位E置u
        public Int16 mt2_wide_alt;  // 78[mmdeg] wide位E置u(高?度x方u向u)　@重d心S位E置u
        public Int16 mt2_wide_vk;   // 90[pix/fr] wide velosity(kalman filter sqrt(vx2+vy2)
        public Int16 fish_id;       // 12[+-ID]  fish ID
        public Int16 fish_vaz;      // 34[mmdeg/s] 速・ﾊ度x推?定e値l(方u位E方u向u)　@カJル?マ
        public Int16 fish_valt;     // 56[mmdeg/s] 速・ﾊ度x推?定e値l(方u位E方u向u)　@カJル?マ
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MT_MONITOR_DATA
    {
        public Byte id;        //unsigned char  Soft ID
        public Byte obs;       //unsigned char  観測中:1/0
        public Byte save;      //unsigned char  保存中:1/0
        public Int32 diskspace; // HDD残容量(GB)
    }


    /// <summary>
    /// UDP通信
    /// </summary>
    /// <typeparam name="T">要素の型</typeparam>
    public class Udp
    {
        public int x2pos, y2pos, x2v, y2v;
        public UInt16 udp_time_code;

        /// <summary>
        /// ポートを指定して初期化。
        /// </summary>
        /// <param name="capacity">初期載大容量</param>
        public Udp(int port)
        {
            // UDP/IPで非同期データ受信するサンプル(C#.NET/VS2005)
            // UDP/IPソケット生成
  //          UdpClient objSck = new UdpClient(port);

            // UDP/IP受信コールバック設定(System.AsyncCallback)
  //          objSck.BeginReceive(ReceiveCallback, objSck);
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
            String rrstr =  System.Text.Encoding.GetEncoding("SHIFT-JIS").GetString(rdat);

            string rstr = DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + " R:[" + rrstr + "]\n"; 
            MessageBox.Show(rstr);

            // 連続で(複数回)データ受信する為の再設定
            ((System.Net.Sockets.UdpClient)AR.AsyncState).BeginReceive(ReceiveCallback, AR.AsyncState);
        }

        /// <summary>
        // 速度データをmmdeg整数化（KV-1000に送信用）
        // 戻り：0.001deg/sec単位の整数
        /// </summary>
        public short round_d2s(double x)
        {
            if (x > 0.0)
            {
                return (short)(x * 1000 + 0.5);
            }
            else
            {
                return (short)(-1 * (short)(-x * 1000 + 0.5));
            }
        }
        /// <summary>
        // 速度データをmmdeg整数化（KV-1000に送信用）後、ushort変換
        // 戻り：0.001deg/sec単位の整数
        /// </summary>
        public short PIDPV_makedata(double daz0)
        {
            double daz = daz0;
            // 条件チェック
            const double vmax = 32.765;
            if (daz < -vmax) daz = -vmax;
            if (daz > vmax) daz = vmax;

            return round_d2s(daz);
        }
        /// <summary>
        // 速度データをmmdeg整数化（KV-1000に送信用）
        // 戻り：0.001deg/sec単位の整数
        /// </summary>
        public int round_d2i(double x)
        {
            if (x > 0.0)
            {
                return (int)(x * 1000 + 0.5);
            }
            else
            {
                return (-1 * (int)(-x * 1000 + 0.5));
            }
        }
        /// <summary>
        // 速度データをmmdeg整数化（KV-1000に送信用）後、ushort変換
        // 戻り：0.001deg/sec単位の整数
        /// </summary>
        public ushort PIDPV_makedata_us(double daz0)
        {
            double daz = daz0;
            // 条件チェック
            const double vmax = 32.765;
            if (daz < -vmax) daz = -vmax;
            if (daz > vmax) daz = vmax;

            int upos = round_d2i(daz);

            ushort p4b;
            // p4a = (unsigned short)(upos>>16) ;
            p4b = (ushort)(0xffff & upos);

            return p4b;
        }

        /// <summary>
        /// udp dataを取り込む。
        /// </summary>
        public void set_udp_kv_data(byte[] bytes, ref KV_DATA kd)
        {
            GCHandle gch = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            kd = (KV_DATA)Marshal.PtrToStructure(gch.AddrOfPinnedObject(), typeof(KV_DATA));
            gch.Free();
        }

        /// <summary>
        /// udp pid_dataを取り込む。
        /// </summary>
         public void set_udp_pid_data(byte[] bytes, ref KV_PID_DATA kpd)
        {
            GCHandle gch = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            kpd = (KV_PID_DATA)Marshal.PtrToStructure(gch.AddrOfPinnedObject(), typeof(KV_PID_DATA));
            gch.Free();
        }

         /// <summary>
         /// UInt32 -> UShort_U, UShort_L 分解
         /// </summary>
         public void TUInt2UShortUShor(UInt32 pos, out UInt16 pos_U, out UInt16 pos_L)
         {
             pos_U = (UInt16)( pos >> 16 );  // >>16 ->1/256*256
             pos_L = (UInt16)( pos &  Convert.ToUInt32("FFFF", 16) );
         }
         /// <summary>
         /// UInt32 -> UShort_U 分解
         /// </summary>
         public UInt16 TUInt2UShort_U(UInt32 pos)
         {
             return (UInt16)(pos >> 16);  // >>16 ->1/256*256
         }
         /// <summary>
         /// UInt32 -> UShort_U 分解
         /// </summary>
         public UInt16 TUInt2UShort_L(UInt32 pos)
         {
             return (UInt16)(pos & Convert.ToUInt32("FFFF", 16));
         }

        /// <summary>
        /// KV1000用byte列に変換　エンディアンも違う
        /// </summary>
        public byte[] ToBytes(KV_PID_DATA obj)
        {
            int size = Marshal.SizeOf(typeof(KV_PID_DATA));
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(obj, ptr, false);
            byte[] bytes = new byte[size];
            Marshal.Copy(ptr, bytes, 0, size);
            Marshal.FreeHGlobal(ptr);
            return bytes;
        }
        /// <summary>
        /// KV1000用 エンディアン変換
        /// </summary>
        public Int32 EndianChange(Int32 obj)
        {
            byte[] bytes = (BitConverter.GetBytes(obj));
            Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }
        /// <summary>
        /// KV1000用 エンディアン変換
        /// </summary>
        public UInt32 EndianChange(UInt32 obj)
        {
            byte[] bytes = (BitConverter.GetBytes(obj));
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }
        /// <summary>
        /// KV1000用 エンディアン変換
        /// </summary>
        public short EndianChange(short obj)
        {
            //int size = Marshal.SizeOf(obj);
            //IntPtr ptr = Marshal.AllocHGlobal(size);
            //Marshal.StructureToPtr(obj, ptr, false);
            byte[] bytes = (BitConverter.GetBytes(obj));
            Array.Reverse(bytes);
            return BitConverter.ToInt16(bytes, 0);
        }
        public ushort EndianChange(ushort obj)
        {
            byte[] bytes = (BitConverter.GetBytes(obj));
            Array.Reverse(bytes);
            return BitConverter.ToUInt16(bytes, 0);
        }


        #region cal_MT3
        /// <summary>
        /// MT3 dataの計算
        /// </summary>
        /// <remarks>
        /// KV_DATA -> az,alt etcに変換
        /// </remarks>
        public void cal_mt3(KV_DATA kd)
        {
            x2pos = (kd.xx2 << 16) + (kd.xx1 << 8) + kd.xx0; // <<16 ->256*256  <<8 ->256
            y2pos = (kd.yy2 << 16) + (kd.yy1 << 8) + kd.yy0; // <<16 ->256*256  <<8 ->256
            x2v = ((kd.v21 << 8) + kd.v20) << 6;
            y2v = ((kd.v23 << 8) + kd.v22) << 6;
            udp_time_code = EndianChange(kd.UdpTimeCode);
/*
            kv_status = (UInt16)((kd.y3 << 8) + kd.y2);      //KV1000 DM503
            data_request = (UInt16)((kd.x3 << 8) + kd.x2);   //KV1000 DM499
            binStr_status = Convert.ToString(kv_status, 2);
            binStr_request = Convert.ToString(data_request, 2);
            Pos2AzAlt2();

            mt3mode = (short)((data_request & (1 << 4)) >> 4); //Set MT3Region(0=mmWest,1=mmEast)
            if ((int)(kv_status & (1 << 10)) != 0) vaz2_kv = +x2v / 1000.0; // MR106 on:+
            else vaz2_kv = -x2v / 1000.0;
            if ((int)(kv_status & (1 << 11)) != 0)
            { //mr107:Y2モータ回転方向
                if (mt3mode == mmEast) valt2_kv = -y2v / 1000.0;
                else valt2_kv = y2v / 1000.0;
            }
            else
            {
                if (mt3mode == mmEast) valt2_kv = y2v / 1000.0;
                else valt2_kv = -y2v / 1000.0;
            }

            mt3state_move_pre = mt3state_move;
            mt3state_truck_pre = mt3state_truck;
            mt3state_center_pre = mt3state_center;
            mt3state_night_pre = mt3state_night;

            mt3state_move = (kv_status & (1 << 12)); //導入中フラグ
            mt3state_truck = (kv_status & (1 << 13)); //追尾中フラグ
            mt3state_night = (kv_status & (1 << 14)); //夜間フラグ
            mt3state_center = (data_request & (1 << 2)); //センタリング中フラグ

            // truck開始時
            if (mt3state_truck_pre == 0 && mt3state_truck != 0) kalman_init_flag = 1;

            // センタリング中 完了時
            if (mt3state_center_pre != 0 && mt3state_center == 0) kalman_init_flag = 1;
*/
        }
        #endregion
    }
}
