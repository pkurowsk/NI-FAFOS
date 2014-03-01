namespace FAFOS
{
    partial class Background
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
            this.button1 = new System.Windows.Forms.Button();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.profilePic = new System.Windows.Forms.PictureBox();
            this.SEdgeLogo = new System.Windows.Forms.Label();
            this.FireAlertLogo = new System.Windows.Forms.PictureBox();
            this.pnlUser = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FireAlertLogo)).BeginInit();
            this.pnlUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(65, 38);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 60);
            this.button1.TabIndex = 30;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserInfo.Location = new System.Drawing.Point(52, 3);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(136, 50);
            this.lblUserInfo.TabIndex = 36;
            this.lblUserInfo.Text = "Welcome ";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Arial Rounded MT Bold", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPageTitle.Location = new System.Drawing.Point(140, 100);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(129, 38);
            this.lblPageTitle.TabIndex = 37;
            this.lblPageTitle.Text = "FAFOS";
            // 
            // profilePic
            // 
            this.profilePic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.profilePic.Location = new System.Drawing.Point(3, 3);
            this.profilePic.Name = "profilePic";
            this.profilePic.Size = new System.Drawing.Size(43, 50);
            this.profilePic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilePic.TabIndex = 38;
            this.profilePic.TabStop = false;
            // 
            // SEdgeLogo
            // 
            this.SEdgeLogo.Image = global::FAFOS.Properties.Resources.CompanyLogo;
            this.SEdgeLogo.Location = new System.Drawing.Point(1468, 710);
            this.SEdgeLogo.Name = "SEdgeLogo";
            this.SEdgeLogo.Size = new System.Drawing.Size(180, 71);
            this.SEdgeLogo.TabIndex = 35;
            // 
            // FireAlertLogo
            // 
            this.FireAlertLogo.ErrorImage = null;
            this.FireAlertLogo.Image = global::FAFOS.Properties.Resources.Logo;
            this.FireAlertLogo.InitialImage = null;
            this.FireAlertLogo.Location = new System.Drawing.Point(1570, 23);
            this.FireAlertLogo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.FireAlertLogo.Name = "FireAlertLogo";
            this.FireAlertLogo.Size = new System.Drawing.Size(298, 144);
            this.FireAlertLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.FireAlertLogo.TabIndex = 34;
            this.FireAlertLogo.TabStop = false;
            // 
            // pnlUser
            // 
            this.pnlUser.Controls.Add(this.profilePic);
            this.pnlUser.Controls.Add(this.lblUserInfo);
            this.pnlUser.Location = new System.Drawing.Point(1227, 205);
            this.pnlUser.Name = "pnlUser";
            this.pnlUser.Size = new System.Drawing.Size(213, 57);
            this.pnlUser.TabIndex = 39;
            // 
            // Background
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1452, 815);
            this.ControlBox = false;
            this.Controls.Add(this.pnlUser);
            this.Controls.Add(this.lblPageTitle);
            this.Controls.Add(this.SEdgeLogo);
            this.Controls.Add(this.FireAlertLogo);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Background";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Background_Load);
            ((System.ComponentModel.ISupportInitialize)(this.profilePic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FireAlertLogo)).EndInit();
            this.pnlUser.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox FireAlertLogo;
        private System.Windows.Forms.Label SEdgeLogo;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.PictureBox profilePic;
        private System.Windows.Forms.Panel pnlUser;
    }
}

