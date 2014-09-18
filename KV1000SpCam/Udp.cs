using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

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
        //public UInt16 x1v, y1v, x2v, y2v;    //unsigned short v
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KV_PID_DATA
    {
        public Int16 wide_id;       // [+-ID]  wide ID
        public Int16 wide_az;       // [mmdeg] wide位置(方位方向)　重心位置
        public Int16 wide_alt;      // [mmdeg] wide位置(高度方向)　重心位置
        public Int16 fine_id;       // [+-ID]  fine ID
        public Int16 fine_az;       // [mmdeg] wide位置(方位方向)　重心位置
        public Int16 fine_alt;      // [mmdeg] wide位置(高度方向)　重心位置
        public Int16 sf_id;         // [+-ID]  sf ID
        public Int16 sf_az;         // [mmdeg] wide位置(方位方向)　重心位置
        public Int16 sf_alt;        // [mmdeg] wide位置(高度方向)　重心位置
        public Int16 fish_id;       // [+-ID]  fish ID
        public Int16 fish_vaz;      // [mmdeg/s] 速度推定値(方位方向)　カルマンフィルタ
        public Int16 fish_valt;     // [mmdeg/s] 速度推定値(高度方向)　カルマンフィルタ
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
        public Udp() : this(7777) { }

        /// <summary>
        /// 初期最大容量を指定して初期化。
        /// </summary>
        /// <param name="capacity">初期載大容量</param>
        public Udp(int port)
        {
            // UDP/IPで非同期データ受信するサンプル(C#.NET/VS2005)
            // UDP/IPソケット生成
            UdpClient objSck = new UdpClient(port);

            // UDP/IP受信コールバック設定(System.AsyncCallback)
            objSck.BeginReceive(ReceiveCallback, objSck);
        }

        // UDP/IP受信コールバック関数
        public void ReceiveCallback(IAsyncResult AR)
        {
            // UDP/IP受信
            System.Net.IPEndPoint ipAny = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);
            Byte[] rdat = ((System.Net.Sockets.UdpClient)AR.AsyncState).EndReceive(AR, ref ipAny);
            String rstr =  System.Text.Encoding.GetEncoding("SHIFT-JIS").GetString(rdat);
            //WriteLine(rstr);

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
        public ushort PIDPV_makedata(double daz0)
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
        /// <remarks>
        /// Set save dir name
        /// </remarks>
        public void set_udp_kv_data(byte[] bytes)
        {
            GCHandle gch = GCHandle.Alloc(bytes, GCHandleType.Pinned);
     //       kd = (KV_DATA)Marshal.PtrToStructure(gch.AddrOfPinnedObject(), typeof(KV_DATA));
            gch.Free();
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
    }
}
