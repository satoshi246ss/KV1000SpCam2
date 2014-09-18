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
using System.IO;

namespace KV1000SpCam
{
    public partial class Form1 : Form
    {
        Stopwatch sw = new Stopwatch();
        long elapsed0 = 0, elapsed1 = 0, elapsed2 = 0;
        double lap0 = 0, lap1 = 0, lap2 = 0, alpha = 0.01;
        string fr_str;
        private BackgroundWorker worker_udp;
        Udp udpkv = new Udp();

        FSI_PID_DATA pid_data = new FSI_PID_DATA();
        KV_PID_DATA kv_pid_data = new KV_PID_DATA();
        MT_MONITOR_DATA mtmon_data = new MT_MONITOR_DATA();
        int mmFsiUdpPortMT3IDS2 = 24422;            // MT3IDS （受信） ブロードキャストのため不使用
        int mmFsiUdpPortMT3IDS2s = 24423;            // MT3IDS （送信）
        int mmFsiUdpPortMTmonitor = 24415;
        string mmFsiCore_i5 = "192.168.1.211";
        int mmFsiUdpPortSpCam = 24410;   // SpCam（受信）
        string mmFsiSC440 = "192.168.1.206";
        System.Net.Sockets.UdpClient udpc3 = null;
        DriveInfo cDrive = new DriveInfo("C");
        long diskspace;

        public Form1()
        {
            InitializeComponent();
        }
    }
}
