namespace FAFOS
{
    partial class Statements
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
            this.cbClients = new System.Windows.Forms.ComboBox();
            this.pnlStatement = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnStatement = new System.Windows.Forms.Button();
            this.pnlStatement.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbClients
            // 
            this.cbClients.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbClients.FormattingEnabled = true;
            this.cbClients.Location = new System.Drawing.Point(14, 47);
            this.cbClients.Name = "cbClients";
            this.cbClients.Size = new System.Drawing.Size(234, 31);
            this.cbClients.TabIndex = 39;
            // 
            // pnlStatement
            // 
            this.pnlStatement.Controls.Add(this.label1);
            this.pnlStatement.Controls.Add(this.btnStatement);
            this.pnlStatement.Controls.Add(this.cbClients);
            this.pnlStatement.Location = new System.Drawing.Point(527, 340);
            this.pnlStatement.Name = "pnlStatement";
            this.pnlStatement.Size = new System.Drawing.Size(388, 124);
            this.pnlStatement.TabIndex = 41;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 20);
            this.label1.TabIndex = 42;
            this.label1.Text = "Outstanding Accounts:";
            // 
            // btnStatement
            // 
            this.btnStatement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.btnStatement.FlatAppearance.BorderSize = 0;
            this.btnStatement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatement.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStatement.ForeColor = System.Drawing.Color.White;
            this.btnStatement.Location = new System.Drawing.Point(257, 32);
            this.btnStatement.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnStatement.Name = "btnStatement";
            this.btnStatement.Size = new System.Drawing.Size(125, 62);
            this.btnStatement.TabIndex = 42;
            this.btnStatement.Text = "Generate Statement";
            this.btnStatement.UseVisualStyleBackColor = false;
            this.btnStatement.Click += new System.EventHandler(this.btnStatement_Click);
            // 
            // Statements
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1459, 814);
            this.Controls.Add(this.pnlStatement);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Statements";
            this.Text = "Statements";
            this.Controls.SetChildIndex(this.pnlStatement, 0);
            this.pnlStatement.ResumeLayout(false);
            this.pnlStatement.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbClients;
        private System.Windows.Forms.Panel pnlStatement;
        private System.Windows.Forms.Button btnStatement;
        private System.Windows.Forms.Label label1;
    }
}