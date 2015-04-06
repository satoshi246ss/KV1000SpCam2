namespace KV1000SpCam
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button_test = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.timer_udp = new System.Windows.Forms.Timer(this.components);
            this.label_x2pos = new System.Windows.Forms.Label();
            this.label_y2pos = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_wide_f = new System.Windows.Forms.Label();
            this.label_wide_daz = new System.Windows.Forms.Label();
            this.label_wide_dalt = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label_fine_dalt = new System.Windows.Forms.Label();
            this.label_fine_daz = new System.Windows.Forms.Label();
            this.label_fine_f = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label_sf_dalt = new System.Windows.Forms.Label();
            this.label_sf_daz = new System.Windows.Forms.Label();
            this.label_sf_f = new System.Windows.Forms.Label();
            this.button_UDP_on = new System.Windows.Forms.Button();
            this.button_px = new System.Windows.Forms.Button();
            this.button_mx = new System.Windows.Forms.Button();
            this.button_py = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_my = new System.Windows.Forms.Button();
            this.label_UdpSendData = new System.Windows.Forms.Label();
            this.timerObsOnOff = new System.Windows.Forms.Timer(this.components);
            this.checkBoxObsAuto = new System.Windows.Forms.CheckBox();
            this.label_wide_vk = new System.Windows.Forms.Label();
            this.label_fine_vk = new System.Windows.Forms.Label();
            this.label_sf_vk = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_MT2wide_f = new System.Windows.Forms.Label();
            this.label_MT2wide_daz = new System.Windows.Forms.Label();
            this.label_MT2wide_dalt = new System.Windows.Forms.Label();
            this.label_MT2wide_vk = new System.Windows.Forms.Label();
            this.textBox_MT2Az = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_MT2Alt = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox_MT2ZAz = new System.Windows.Forms.TextBox();
            this.textBox_MT2ｄZT = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button_SetMT2Pos = new System.Windows.Forms.Button();
            this.timerDisp = new System.Windows.Forms.Timer(this.components);
            this.radioButton_MT2 = new System.Windows.Forms.RadioButton();
            this.radioButton_MT3 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_test
            // 
            this.button_test.BackColor = System.Drawing.SystemColors.Control;
            this.button_test.Location = new System.Drawing.Point(12, 12);
            this.button_test.Name = "button_test";
            this.button_test.Size = new System.Drawing.Size(44, 42);
            this.button_test.TabIndex = 0;
            this.button_test.Text = "Test";
            this.button_test.UseVisualStyleBackColor = false;
            this.button_test.Click += new System.EventHandler(this.button_test_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(0, 153);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(505, 117);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // timer_udp
            // 
            this.timer_udp.Enabled = true;
            this.timer_udp.Interval = 13;
            this.timer_udp.Tick += new System.EventHandler(this.timer_udp_Tick);
            // 
            // label_x2pos
            // 
            this.label_x2pos.AutoSize = true;
            this.label_x2pos.Location = new System.Drawing.Point(13, 78);
            this.label_x2pos.Name = "label_x2pos";
            this.label_x2pos.Size = new System.Drawing.Size(35, 12);
            this.label_x2pos.TabIndex = 2;
            this.label_x2pos.Text = "x2pos";
            // 
            // label_y2pos
            // 
            this.label_y2pos.AutoSize = true;
            this.label_y2pos.Location = new System.Drawing.Point(64, 78);
            this.label_y2pos.Name = "label_y2pos";
            this.label_y2pos.Size = new System.Drawing.Size(35, 12);
            this.label_y2pos.TabIndex = 3;
            this.label_y2pos.Text = "y2pos";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "Flag";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(207, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "dAz[mmDeg]";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(293, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "dAlt[mmDeg]";
            // 
            // label_wide_f
            // 
            this.label_wide_f.AutoSize = true;
            this.label_wide_f.Location = new System.Drawing.Point(155, 30);
            this.label_wide_f.Name = "label_wide_f";
            this.label_wide_f.Size = new System.Drawing.Size(22, 12);
            this.label_wide_f.TabIndex = 7;
            this.label_wide_f.Text = "W_f";
            // 
            // label_wide_daz
            // 
            this.label_wide_daz.AutoSize = true;
            this.label_wide_daz.Location = new System.Drawing.Point(207, 30);
            this.label_wide_daz.Name = "label_wide_daz";
            this.label_wide_daz.Size = new System.Drawing.Size(35, 12);
            this.label_wide_daz.TabIndex = 8;
            this.label_wide_daz.Text = "W_daz";
            // 
            // label_wide_dalt
            // 
            this.label_wide_dalt.AutoSize = true;
            this.label_wide_dalt.Location = new System.Drawing.Point(293, 30);
            this.label_wide_dalt.Name = "label_wide_dalt";
            this.label_wide_dalt.Size = new System.Drawing.Size(37, 12);
            this.label_wide_dalt.TabIndex = 9;
            this.label_wide_dalt.Text = "W_dalt";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(122, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(31, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Wide:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(122, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "Fine:";
            // 
            // label_fine_dalt
            // 
            this.label_fine_dalt.AutoSize = true;
            this.label_fine_dalt.Location = new System.Drawing.Point(293, 48);
            this.label_fine_dalt.Name = "label_fine_dalt";
            this.label_fine_dalt.Size = new System.Drawing.Size(37, 12);
            this.label_fine_dalt.TabIndex = 13;
            this.label_fine_dalt.Text = "W_dalt";
            // 
            // label_fine_daz
            // 
            this.label_fine_daz.AutoSize = true;
            this.label_fine_daz.Location = new System.Drawing.Point(207, 48);
            this.label_fine_daz.Name = "label_fine_daz";
            this.label_fine_daz.Size = new System.Drawing.Size(35, 12);
            this.label_fine_daz.TabIndex = 12;
            this.label_fine_daz.Text = "W_daz";
            // 
            // label_fine_f
            // 
            this.label_fine_f.AutoSize = true;
            this.label_fine_f.Location = new System.Drawing.Point(155, 48);
            this.label_fine_f.Name = "label_fine_f";
            this.label_fine_f.Size = new System.Drawing.Size(22, 12);
            this.label_fine_f.TabIndex = 11;
            this.label_fine_f.Text = "W_f";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(122, 66);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "SF:";
            // 
            // label_sf_dalt
            // 
            this.label_sf_dalt.AutoSize = true;
            this.label_sf_dalt.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label_sf_dalt.Location = new System.Drawing.Point(293, 66);
            this.label_sf_dalt.Name = "label_sf_dalt";
            this.label_sf_dalt.Size = new System.Drawing.Size(37, 12);
            this.label_sf_dalt.TabIndex = 17;
            this.label_sf_dalt.Text = "W_dalt";
            // 
            // label_sf_daz
            // 
            this.label_sf_daz.AutoSize = true;
            this.label_sf_daz.Location = new System.Drawing.Point(207, 66);
            this.label_sf_daz.Name = "label_sf_daz";
            this.label_sf_daz.Size = new System.Drawing.Size(35, 12);
            this.label_sf_daz.TabIndex = 16;
            this.label_sf_daz.Text = "W_daz";
            // 
            // label_sf_f
            // 
            this.label_sf_f.AutoSize = true;
            this.label_sf_f.Location = new System.Drawing.Point(155, 66);
            this.label_sf_f.Name = "label_sf_f";
            this.label_sf_f.Size = new System.Drawing.Size(22, 12);
            this.label_sf_f.TabIndex = 15;
            this.label_sf_f.Text = "W_f";
            // 
            // button_UDP_on
            // 
            this.button_UDP_on.BackColor = System.Drawing.SystemColors.Control;
            this.button_UDP_on.Location = new System.Drawing.Point(62, 12);
            this.button_UDP_on.Name = "button_UDP_on";
            this.button_UDP_on.Size = new System.Drawing.Size(44, 42);
            this.button_UDP_on.TabIndex = 19;
            this.button_UDP_on.Text = "UDP on";
            this.button_UDP_on.UseVisualStyleBackColor = false;
            this.button_UDP_on.Click += new System.EventHandler(this.button_UDP_on_Click);
            // 
            // button_px
            // 
            this.button_px.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.button_px.Location = new System.Drawing.Point(0, 19);
            this.button_px.Name = "button_px";
            this.button_px.Size = new System.Drawing.Size(26, 21);
            this.button_px.TabIndex = 20;
            this.button_px.Text = "+X";
            this.button_px.UseVisualStyleBackColor = true;
            this.button_px.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_px_MouseDown);
            this.button_px.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_px_MouseUp);
            // 
            // button_mx
            // 
            this.button_mx.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.button_mx.Location = new System.Drawing.Point(56, 19);
            this.button_mx.Name = "button_mx";
            this.button_mx.Size = new System.Drawing.Size(26, 21);
            this.button_mx.TabIndex = 21;
            this.button_mx.Text = "-X";
            this.button_mx.UseVisualStyleBackColor = true;
            this.button_mx.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_mx_MouseDown);
            this.button_mx.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_mx_MouseUp);
            // 
            // button_py
            // 
            this.button_py.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.button_py.Location = new System.Drawing.Point(28, 0);
            this.button_py.Name = "button_py";
            this.button_py.Size = new System.Drawing.Size(26, 21);
            this.button_py.TabIndex = 22;
            this.button_py.Text = "+Y";
            this.button_py.UseVisualStyleBackColor = true;
            this.button_py.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_py_MouseDown);
            this.button_py.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_py_MouseUp);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_my);
            this.panel1.Controls.Add(this.button_mx);
            this.panel1.Controls.Add(this.button_py);
            this.panel1.Controls.Add(this.button_px);
            this.panel1.Location = new System.Drawing.Point(411, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(82, 61);
            this.panel1.TabIndex = 23;
            // 
            // button_my
            // 
            this.button_my.Font = new System.Drawing.Font("MS UI Gothic", 8F);
            this.button_my.Location = new System.Drawing.Point(28, 40);
            this.button_my.Name = "button_my";
            this.button_my.Size = new System.Drawing.Size(26, 21);
            this.button_my.TabIndex = 23;
            this.button_my.Text = "-Y";
            this.button_my.UseVisualStyleBackColor = true;
            this.button_my.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button_my_MouseDown);
            this.button_my.MouseUp += new System.Windows.Forms.MouseEventHandler(this.button_my_MouseUp);
            // 
            // label_UdpSendData
            // 
            this.label_UdpSendData.AutoSize = true;
            this.label_UdpSendData.Location = new System.Drawing.Point(13, 90);
            this.label_UdpSendData.Name = "label_UdpSendData";
            this.label_UdpSendData.Size = new System.Drawing.Size(50, 12);
            this.label_UdpSendData.TabIndex = 24;
            this.label_UdpSendData.Text = "UDPdata";
            // 
            // timerObsOnOff
            // 
            this.timerObsOnOff.Enabled = true;
            this.timerObsOnOff.Interval = 10000;
            this.timerObsOnOff.Tick += new System.EventHandler(this.timerObsOnOff_Tick);
            // 
            // checkBoxObsAuto
            // 
            this.checkBoxObsAuto.AutoSize = true;
            this.checkBoxObsAuto.Checked = true;
            this.checkBoxObsAuto.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxObsAuto.Location = new System.Drawing.Point(15, 56);
            this.checkBoxObsAuto.Name = "checkBoxObsAuto";
            this.checkBoxObsAuto.Size = new System.Drawing.Size(80, 16);
            this.checkBoxObsAuto.TabIndex = 25;
            this.checkBoxObsAuto.Text = "checkBox1";
            this.checkBoxObsAuto.UseVisualStyleBackColor = true;
            // 
            // label_wide_vk
            // 
            this.label_wide_vk.AutoSize = true;
            this.label_wide_vk.Location = new System.Drawing.Point(353, 30);
            this.label_wide_vk.Name = "label_wide_vk";
            this.label_wide_vk.Size = new System.Drawing.Size(30, 12);
            this.label_wide_vk.TabIndex = 26;
            this.label_wide_vk.Text = "W_vk";
            // 
            // label_fine_vk
            // 
            this.label_fine_vk.AutoSize = true;
            this.label_fine_vk.Location = new System.Drawing.Point(353, 48);
            this.label_fine_vk.Name = "label_fine_vk";
            this.label_fine_vk.Size = new System.Drawing.Size(28, 12);
            this.label_fine_vk.TabIndex = 27;
            this.label_fine_vk.Text = "F_vk";
            // 
            // label_sf_vk
            // 
            this.label_sf_vk.AutoSize = true;
            this.label_sf_vk.Location = new System.Drawing.Point(353, 66);
            this.label_sf_vk.Name = "label_sf_vk";
            this.label_sf_vk.Size = new System.Drawing.Size(35, 12);
            this.label_sf_vk.TabIndex = 28;
            this.label_sf_vk.Text = "SF_vk";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(122, 90);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 12);
            this.label6.TabIndex = 29;
            this.label6.Text = "MT2W:";
            // 
            // label_MT2wide_f
            // 
            this.label_MT2wide_f.AutoSize = true;
            this.label_MT2wide_f.Location = new System.Drawing.Point(160, 90);
            this.label_MT2wide_f.Name = "label_MT2wide_f";
            this.label_MT2wide_f.Size = new System.Drawing.Size(22, 12);
            this.label_MT2wide_f.TabIndex = 30;
            this.label_MT2wide_f.Text = "W_f";
            // 
            // label_MT2wide_daz
            // 
            this.label_MT2wide_daz.AutoSize = true;
            this.label_MT2wide_daz.Location = new System.Drawing.Point(207, 92);
            this.label_MT2wide_daz.Name = "label_MT2wide_daz";
            this.label_MT2wide_daz.Size = new System.Drawing.Size(35, 12);
            this.label_MT2wide_daz.TabIndex = 31;
            this.label_MT2wide_daz.Text = "W_daz";
            // 
            // label_MT2wide_dalt
            // 
            this.label_MT2wide_dalt.AutoSize = true;
            this.label_MT2wide_dalt.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label_MT2wide_dalt.Location = new System.Drawing.Point(293, 90);
            this.label_MT2wide_dalt.Name = "label_MT2wide_dalt";
            this.label_MT2wide_dalt.Size = new System.Drawing.Size(37, 12);
            this.label_MT2wide_dalt.TabIndex = 32;
            this.label_MT2wide_dalt.Text = "W_dalt";
            // 
            // label_MT2wide_vk
            // 
            this.label_MT2wide_vk.AutoSize = true;
            this.label_MT2wide_vk.Location = new System.Drawing.Point(351, 90);
            this.label_MT2wide_vk.Name = "label_MT2wide_vk";
            this.label_MT2wide_vk.Size = new System.Drawing.Size(30, 12);
            this.label_MT2wide_vk.TabIndex = 33;
            this.label_MT2wide_vk.Text = "W_vk";
            // 
            // textBox_MT2Az
            // 
            this.textBox_MT2Az.Location = new System.Drawing.Point(124, 107);
            this.textBox_MT2Az.Name = "textBox_MT2Az";
            this.textBox_MT2Az.Size = new System.Drawing.Size(53, 19);
            this.textBox_MT2Az.TabIndex = 34;
            this.textBox_MT2Az.Text = "90.0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(64, 110);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 12);
            this.label7.TabIndex = 35;
            this.label7.Text = "MT2 Az";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(64, 128);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 12);
            this.label8.TabIndex = 36;
            this.label8.Text = "MT2 Alt";
            // 
            // textBox_MT2Alt
            // 
            this.textBox_MT2Alt.Location = new System.Drawing.Point(124, 128);
            this.textBox_MT2Alt.Name = "textBox_MT2Alt";
            this.textBox_MT2Alt.Size = new System.Drawing.Size(53, 19);
            this.textBox_MT2Alt.TabIndex = 37;
            this.textBox_MT2Alt.Text = "80.0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(207, 110);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 12);
            this.label10.TabIndex = 38;
            this.label10.Text = "MT2 ZAz";
            // 
            // textBox_MT2ZAz
            // 
            this.textBox_MT2ZAz.Location = new System.Drawing.Point(264, 107);
            this.textBox_MT2ZAz.Name = "textBox_MT2ZAz";
            this.textBox_MT2ZAz.Size = new System.Drawing.Size(53, 19);
            this.textBox_MT2ZAz.TabIndex = 39;
            this.textBox_MT2ZAz.Text = "180.0";
            // 
            // textBox_MT2ｄZT
            // 
            this.textBox_MT2ｄZT.Location = new System.Drawing.Point(264, 128);
            this.textBox_MT2ｄZT.Name = "textBox_MT2ｄZT";
            this.textBox_MT2ｄZT.Size = new System.Drawing.Size(53, 19);
            this.textBox_MT2ｄZT.TabIndex = 40;
            this.textBox_MT2ｄZT.Text = "2.08";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(207, 128);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 12);
            this.label11.TabIndex = 41;
            this.label11.Text = "MT2 dZθ";
            // 
            // button_SetMT2Pos
            // 
            this.button_SetMT2Pos.BackColor = System.Drawing.SystemColors.Control;
            this.button_SetMT2Pos.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_SetMT2Pos.Location = new System.Drawing.Point(12, 105);
            this.button_SetMT2Pos.Name = "button_SetMT2Pos";
            this.button_SetMT2Pos.Size = new System.Drawing.Size(44, 42);
            this.button_SetMT2Pos.TabIndex = 42;
            this.button_SetMT2Pos.Text = "Set MT2 Pos";
            this.button_SetMT2Pos.UseVisualStyleBackColor = false;
            this.button_SetMT2Pos.Click += new System.EventHandler(this.button_SetMT2Pos_Click);
            // 
            // timerDisp
            // 
            this.timerDisp.Enabled = true;
            this.timerDisp.Interval = 500;
            this.timerDisp.Tick += new System.EventHandler(this.timerDisp_Tick);
            // 
            // radioButton_MT2
            // 
            this.radioButton_MT2.AutoSize = true;
            this.radioButton_MT2.Checked = true;
            this.radioButton_MT2.Location = new System.Drawing.Point(0, 12);
            this.radioButton_MT2.Name = "radioButton_MT2";
            this.radioButton_MT2.Size = new System.Drawing.Size(45, 16);
            this.radioButton_MT2.TabIndex = 43;
            this.radioButton_MT2.TabStop = true;
            this.radioButton_MT2.Text = "MT2";
            this.radioButton_MT2.UseVisualStyleBackColor = true;
            // 
            // radioButton_MT3
            // 
            this.radioButton_MT3.AutoSize = true;
            this.radioButton_MT3.Location = new System.Drawing.Point(0, 27);
            this.radioButton_MT3.Name = "radioButton_MT3";
            this.radioButton_MT3.Size = new System.Drawing.Size(45, 16);
            this.radioButton_MT3.TabIndex = 44;
            this.radioButton_MT3.Text = "MT3";
            this.radioButton_MT3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_MT2);
            this.groupBox1.Controls.Add(this.radioButton_MT3);
            this.groupBox1.Location = new System.Drawing.Point(411, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(82, 44);
            this.groupBox1.TabIndex = 45;
            this.groupBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 272);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_SetMT2Pos);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.textBox_MT2ｄZT);
            this.Controls.Add(this.textBox_MT2ZAz);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.textBox_MT2Alt);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_MT2Az);
            this.Controls.Add(this.label_MT2wide_vk);
            this.Controls.Add(this.label_MT2wide_dalt);
            this.Controls.Add(this.label_MT2wide_daz);
            this.Controls.Add(this.label_MT2wide_f);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label_sf_vk);
            this.Controls.Add(this.label_fine_vk);
            this.Controls.Add(this.label_wide_vk);
            this.Controls.Add(this.checkBoxObsAuto);
            this.Controls.Add(this.label_UdpSendData);
            this.Controls.Add(this.button_UDP_on);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label_sf_dalt);
            this.Controls.Add(this.label_sf_daz);
            this.Controls.Add(this.label_sf_f);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label_fine_dalt);
            this.Controls.Add(this.label_fine_daz);
            this.Controls.Add(this.label_fine_f);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_wide_dalt);
            this.Controls.Add(this.label_wide_daz);
            this.Controls.Add(this.label_wide_f);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_y2pos);
            this.Controls.Add(this.label_x2pos);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button_test);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "KV1000SpCam_2   (192.168.1.204:24426)";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_test;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Timer timer_udp;
        private System.Windows.Forms.Label label_x2pos;
        private System.Windows.Forms.Label label_y2pos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_wide_f;
        private System.Windows.Forms.Label label_wide_daz;
        private System.Windows.Forms.Label label_wide_dalt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_fine_dalt;
        private System.Windows.Forms.Label label_fine_daz;
        private System.Windows.Forms.Label label_fine_f;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label_sf_dalt;
        private System.Windows.Forms.Label label_sf_daz;
        private System.Windows.Forms.Label label_sf_f;
        private System.Windows.Forms.Button button_UDP_on;
        private System.Windows.Forms.Button button_px;
        private System.Windows.Forms.Button button_mx;
        private System.Windows.Forms.Button button_py;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_my;
        private System.Windows.Forms.Label label_UdpSendData;
        private System.Windows.Forms.Timer timerObsOnOff;
        private System.Windows.Forms.CheckBox checkBoxObsAuto;
        private System.Windows.Forms.Label label_wide_vk;
        private System.Windows.Forms.Label label_fine_vk;
        private System.Windows.Forms.Label label_sf_vk;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_MT2wide_f;
        private System.Windows.Forms.Label label_MT2wide_daz;
        private System.Windows.Forms.Label label_MT2wide_dalt;
        private System.Windows.Forms.Label label_MT2wide_vk;
        private System.Windows.Forms.TextBox textBox_MT2Az;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_MT2Alt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox_MT2ZAz;
        private System.Windows.Forms.TextBox textBox_MT2ｄZT;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button button_SetMT2Pos;
        private System.Windows.Forms.Timer timerDisp;
        private System.Windows.Forms.RadioButton radioButton_MT2;
        private System.Windows.Forms.RadioButton radioButton_MT3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

