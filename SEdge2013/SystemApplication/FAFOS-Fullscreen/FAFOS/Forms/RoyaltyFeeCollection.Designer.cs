namespace FAFOS
{
    partial class RoyaltyFeeCollection
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
            this.franchiseeBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.royalteeFees = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Month = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Percentage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BalanceOwed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.yearBox = new System.Windows.Forms.ComboBox();
            this.show_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.royalteeFees)).BeginInit();
            this.SuspendLayout();
            // 
            // franchiseeBox
            // 
            this.franchiseeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.franchiseeBox.FormattingEnabled = true;
            this.franchiseeBox.Location = new System.Drawing.Point(149, 247);
            this.franchiseeBox.Name = "franchiseeBox";
            this.franchiseeBox.Size = new System.Drawing.Size(295, 28);
            this.franchiseeBox.TabIndex = 39;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(145, 215);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(161, 20);
            this.label8.TabIndex = 47;
            this.label8.Text = "Select Franchisee:";
            // 
            // royalteeFees
            // 
            this.royalteeFees.AccessibleDescription = "";
            this.royalteeFees.AllowUserToAddRows = false;
            this.royalteeFees.AllowUserToDeleteRows = false;
            this.royalteeFees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.royalteeFees.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Date,
            this.Month,
            this.Percentage,
            this.BalanceOwed});
            this.royalteeFees.Location = new System.Drawing.Point(149, 335);
            this.royalteeFees.Name = "royalteeFees";
            this.royalteeFees.ReadOnly = true;
            this.royalteeFees.RowTemplate.Height = 24;
            this.royalteeFees.Size = new System.Drawing.Size(654, 433);
            this.royalteeFees.TabIndex = 48;
            this.royalteeFees.Visible = false;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "id";
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 80;
            // 
            // Date
            // 
            this.Date.DataPropertyName = "dateIssued";
            this.Date.HeaderText = "Issued Date";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            this.Date.Width = 120;
            // 
            // Month
            // 
            this.Month.DataPropertyName = "month";
            this.Month.HeaderText = "Month";
            this.Month.Name = "Month";
            this.Month.ReadOnly = true;
            this.Month.Width = 120;
            // 
            // Percentage
            // 
            this.Percentage.DataPropertyName = "percentage";
            this.Percentage.HeaderText = "Royalty Fee %";
            this.Percentage.Name = "Percentage";
            this.Percentage.ReadOnly = true;
            this.Percentage.Width = 140;
            // 
            // BalanceOwed
            // 
            this.BalanceOwed.DataPropertyName = "amount";
            this.BalanceOwed.HeaderText = "Balance Owed";
            this.BalanceOwed.Name = "BalanceOwed";
            this.BalanceOwed.ReadOnly = true;
            this.BalanceOwed.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(529, 215);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 50;
            this.label1.Text = "Select Year:";
            // 
            // yearBox
            // 
            this.yearBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.yearBox.FormattingEnabled = true;
            this.yearBox.Items.AddRange(new object[] {
            "2013"});
            this.yearBox.Location = new System.Drawing.Point(533, 247);
            this.yearBox.Name = "yearBox";
            this.yearBox.Size = new System.Drawing.Size(143, 28);
            this.yearBox.TabIndex = 49;
            // 
            // show_btn
            // 
            this.show_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.show_btn.FlatAppearance.BorderSize = 0;
            this.show_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.show_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.show_btn.ForeColor = System.Drawing.Color.White;
            this.show_btn.Location = new System.Drawing.Point(766, 235);
            this.show_btn.Margin = new System.Windows.Forms.Padding(4);
            this.show_btn.Name = "show_btn";
            this.show_btn.Size = new System.Drawing.Size(182, 50);
            this.show_btn.TabIndex = 51;
            this.show_btn.Text = "Show Collection";
            this.show_btn.UseVisualStyleBackColor = false;
            this.show_btn.Click += new System.EventHandler(this.show_btn_Click);
            // 
            // RoyaltyFeeCollection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1583, 886);
            this.Controls.Add(this.show_btn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.yearBox);
            this.Controls.Add(this.royalteeFees);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.franchiseeBox);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "RoyaltyFeeCollection";
            this.Text = "RoyalteeFeeCollection";
            this.Load += new System.EventHandler(this.RoyaltyFeeCollection_Load);
            this.Controls.SetChildIndex(this.franchiseeBox, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.royalteeFees, 0);
            this.Controls.SetChildIndex(this.yearBox, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.show_btn, 0);
            ((System.ComponentModel.ISupportInitialize)(this.royalteeFees)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox franchiseeBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView royalteeFees;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox yearBox;
        private System.Windows.Forms.Button show_btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewTextBoxColumn Month;
        private System.Windows.Forms.DataGridViewTextBoxColumn Percentage;
        private System.Windows.Forms.DataGridViewTextBoxColumn BalanceOwed;
    }
}