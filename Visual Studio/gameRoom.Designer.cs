namespace CM
{
    partial class gameRoom
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.deconect = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 278);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(565, 134);
            this.listBox1.TabIndex = 12;
            // 
            // deconect
            // 
            this.deconect.Location = new System.Drawing.Point(717, 435);
            this.deconect.Name = "deconect";
            this.deconect.Size = new System.Drawing.Size(100, 40);
            this.deconect.TabIndex = 11;
            this.deconect.Text = "Deconectare";
            this.deconect.UseVisualStyleBackColor = true;
            this.deconect.Click += new System.EventHandler(this.deconect_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 423);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(465, 52);
            this.textBox2.TabIndex = 10;
            // 
            // send
            // 
            this.send.Location = new System.Drawing.Point(483, 423);
            this.send.Name = "send";
            this.send.Size = new System.Drawing.Size(94, 52);
            this.send.TabIndex = 9;
            this.send.Text = "send";
            this.send.UseVisualStyleBackColor = true;
            this.send.Click += new System.EventHandler(this.send_Click);
            // 
            // gameRoom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 487);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.deconect);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.send);
            this.Name = "gameRoom";
            this.Text = "gameRoom";
            this.Load += new System.EventHandler(this.gameRoom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button deconect;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button send;
    }
}