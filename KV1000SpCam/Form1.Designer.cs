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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 226);
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
    }
}

