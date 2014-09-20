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
            this.richTextBox1.Location = new System.Drawing.Point(0, 107);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(505, 117);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // timer_udp
            // 
            this.timer_udp.Interval = 40;
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 226);
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
            this.Name = "Form1";
            this.Text = "KV1000SpCam_2";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
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
    }
}

