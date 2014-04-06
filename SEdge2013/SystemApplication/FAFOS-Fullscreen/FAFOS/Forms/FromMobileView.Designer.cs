namespace FAFOS
{
    partial class FromMobileView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FromMobileView));
            this.Listen_btn = new System.Windows.Forms.Button();
            this.PortNumber = new System.Windows.Forms.TextBox();
            this.Msg = new System.Windows.Forms.Label();
            this.IPServer = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.reqBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.InfoBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Listen_btn
            // 
            this.Listen_btn.Location = new System.Drawing.Point(251, 43);
            this.Listen_btn.Margin = new System.Windows.Forms.Padding(4);
            this.Listen_btn.Name = "Listen_btn";
            this.Listen_btn.Size = new System.Drawing.Size(103, 36);
            this.Listen_btn.TabIndex = 0;
            this.Listen_btn.Text = "Listen";
            this.Listen_btn.UseVisualStyleBackColor = true;
            // 
            // PortNumber
            // 
            this.PortNumber.Location = new System.Drawing.Point(132, 49);
            this.PortNumber.Margin = new System.Windows.Forms.Padding(4);
            this.PortNumber.Name = "PortNumber";
            this.PortNumber.Size = new System.Drawing.Size(93, 22);
            this.PortNumber.TabIndex = 2;
            this.PortNumber.Text = "3000";
            // 
            // Msg
            // 
            this.Msg.AutoSize = true;
            this.Msg.Location = new System.Drawing.Point(28, 53);
            this.Msg.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Msg.Name = "Msg";
            this.Msg.Size = new System.Drawing.Size(96, 17);
            this.Msg.TabIndex = 4;
            this.Msg.Text = "Listen on Port";
            // 
            // IPServer
            // 
            this.IPServer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.IPServer.Location = new System.Drawing.Point(164, 106);
            this.IPServer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.IPServer.Name = "IPServer";
            this.IPServer.Size = new System.Drawing.Size(189, 22);
            this.IPServer.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 107);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Server IP address";
            // 
            // reqBox
            // 
            this.reqBox.BackColor = System.Drawing.Color.White;
            this.reqBox.Location = new System.Drawing.Point(32, 319);
            this.reqBox.Margin = new System.Windows.Forms.Padding(4);
            this.reqBox.Multiline = true;
            this.reqBox.Name = "reqBox";
            this.reqBox.ReadOnly = true;
            this.reqBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.reqBox.Size = new System.Drawing.Size(463, 318);
            this.reqBox.TabIndex = 12;
            this.reqBox.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 298);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Mobile Device Send";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 159);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 17);
            this.label1.TabIndex = 13;
            this.label1.Text = "Connection Status ";
            // 
            // InfoBox
            // 
            this.InfoBox.BackColor = System.Drawing.Color.White;
            this.InfoBox.Location = new System.Drawing.Point(32, 178);
            this.InfoBox.Margin = new System.Windows.Forms.Padding(4);
            this.InfoBox.Multiline = true;
            this.InfoBox.Name = "InfoBox";
            this.InfoBox.ReadOnly = true;
            this.InfoBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.InfoBox.Size = new System.Drawing.Size(463, 106);
            this.InfoBox.TabIndex = 7;
            this.InfoBox.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(392, 660);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 36);
            this.button1.TabIndex = 14;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(415, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(68, 67);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // FromMobileView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 722);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reqBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.InfoBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.IPServer);
            this.Controls.Add(this.Msg);
            this.Controls.Add(this.PortNumber);
            this.Controls.Add(this.Listen_btn);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FromMobileView";
            this.Text = "Sync Inspection Results from Mobile Device";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Listen_btn;
        private System.Windows.Forms.TextBox PortNumber;
        private System.Windows.Forms.Label Msg;
        private System.Windows.Forms.Label IPServer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox reqBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox InfoBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

