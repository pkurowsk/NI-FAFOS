namespace FAFOS
{
    partial class AdminUserForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.UserGridView = new System.Windows.Forms.DataGridView();
            this.usrName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usrIDCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PassCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.passSetCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PerOwnCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.adminCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.hqCol = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.uDelete = new System.Windows.Forms.DataGridViewButtonColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CompanyNameTxtBox = new System.Windows.Forms.TextBox();
            this.TaxNumBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.BusinessNumTxtBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BAddrGridView = new System.Windows.Forms.DataGridView();
            this.locID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bAddr = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bPostal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bCountry = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.bProvince = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.bCity = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.removeBtn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.addUserButton = new System.Windows.Forms.Button();
            this.AddAddrButton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.FiscalPicker = new System.Windows.Forms.DateTimePicker();
            this.SaveBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.UserGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BAddrGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // UserGridView
            // 
            this.UserGridView.AllowUserToAddRows = false;
            this.UserGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UserGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.UserGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UserGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.usrName,
            this.usrIDCol,
            this.PassCol,
            this.passSetCol,
            this.fName,
            this.lName,
            this.mName,
            this.PerOwnCol,
            this.adminCol,
            this.hqCol,
            this.uDelete});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.UserGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.UserGridView.Location = new System.Drawing.Point(13, 150);
            this.UserGridView.Margin = new System.Windows.Forms.Padding(4);
            this.UserGridView.Name = "UserGridView";
            this.UserGridView.Size = new System.Drawing.Size(653, 128);
            this.UserGridView.TabIndex = 38;
            this.UserGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.UserGridView_CellValueChanged);
            // 
            // usrName
            // 
            this.usrName.HeaderText = "Username";
            this.usrName.Name = "usrName";
            // 
            // usrIDCol
            // 
            this.usrIDCol.HeaderText = "id";
            this.usrIDCol.Name = "usrIDCol";
            this.usrIDCol.ReadOnly = true;
            this.usrIDCol.Visible = false;
            // 
            // PassCol
            // 
            this.PassCol.HeaderText = "Pass.";
            this.PassCol.Name = "PassCol";
            this.PassCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PassCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.PassCol.Width = 60;
            // 
            // passSetCol
            // 
            this.passSetCol.HeaderText = "Passtxt";
            this.passSetCol.Name = "passSetCol";
            this.passSetCol.Visible = false;
            // 
            // fName
            // 
            this.fName.HeaderText = "First Name";
            this.fName.Name = "fName";
            // 
            // lName
            // 
            this.lName.HeaderText = "Last Name";
            this.lName.Name = "lName";
            // 
            // mName
            // 
            this.mName.HeaderText = "Middle Name";
            this.mName.Name = "mName";
            // 
            // PerOwnCol
            // 
            this.PerOwnCol.HeaderText = "% Own";
            this.PerOwnCol.Name = "PerOwnCol";
            this.PerOwnCol.Width = 40;
            // 
            // adminCol
            // 
            this.adminCol.HeaderText = "Admin";
            this.adminCol.Name = "adminCol";
            this.adminCol.Width = 50;
            // 
            // hqCol
            // 
            this.hqCol.HeaderText = "HQ Admin";
            this.hqCol.Name = "hqCol";
            this.hqCol.Visible = false;
            this.hqCol.Width = 50;
            // 
            // uDelete
            // 
            this.uDelete.HeaderText = "Delete";
            this.uDelete.Name = "uDelete";
            this.uDelete.Width = 60;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(10, 128);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 39;
            this.label1.Text = "Users:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(13, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 20);
            this.label2.TabIndex = 40;
            this.label2.Text = "Company Name:";
            // 
            // CompanyNameTxtBox
            // 
            this.CompanyNameTxtBox.Location = new System.Drawing.Point(17, 30);
            this.CompanyNameTxtBox.Name = "CompanyNameTxtBox";
            this.CompanyNameTxtBox.Size = new System.Drawing.Size(273, 27);
            this.CompanyNameTxtBox.TabIndex = 41;
            // 
            // TaxNumBox
            // 
            this.TaxNumBox.Location = new System.Drawing.Point(17, 88);
            this.TaxNumBox.Name = "TaxNumBox";
            this.TaxNumBox.Size = new System.Drawing.Size(197, 27);
            this.TaxNumBox.TabIndex = 43;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(14, 67);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(200, 20);
            this.label3.TabIndex = 42;
            this.label3.Text = "Tax Registration Number:";
            // 
            // BusinessNumTxtBox
            // 
            this.BusinessNumTxtBox.Location = new System.Drawing.Point(246, 88);
            this.BusinessNumTxtBox.Name = "BusinessNumTxtBox";
            this.BusinessNumTxtBox.Size = new System.Drawing.Size(239, 27);
            this.BusinessNumTxtBox.TabIndex = 45;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(243, 68);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(243, 20);
            this.label4.TabIndex = 44;
            this.label4.Text = "Business Registration Number:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(335, 6);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 20);
            this.label5.TabIndex = 46;
            this.label5.Text = "Fiscal year end:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(18, 302);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(169, 20);
            this.label6.TabIndex = 49;
            this.label6.Text = "Business Addresses:";
            // 
            // BAddrGridView
            // 
            this.BAddrGridView.AllowUserToAddRows = false;
            this.BAddrGridView.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.BAddrGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.BAddrGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BAddrGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.locID,
            this.bAddr,
            this.bPostal,
            this.bCountry,
            this.bProvince,
            this.bCity,
            this.removeBtn});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.BAddrGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.BAddrGridView.Location = new System.Drawing.Point(21, 327);
            this.BAddrGridView.Margin = new System.Windows.Forms.Padding(4);
            this.BAddrGridView.Name = "BAddrGridView";
            this.BAddrGridView.Size = new System.Drawing.Size(645, 128);
            this.BAddrGridView.TabIndex = 48;
            this.BAddrGridView.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.BAddrGridView_CellValueChanged);
            this.BAddrGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.BAddrGridView_DataError);
            // 
            // locID
            // 
            this.locID.HeaderText = "ID";
            this.locID.Name = "locID";
            this.locID.ReadOnly = true;
            this.locID.Visible = false;
            // 
            // bAddr
            // 
            this.bAddr.HeaderText = "Address";
            this.bAddr.Name = "bAddr";
            this.bAddr.Width = 150;
            // 
            // bPostal
            // 
            this.bPostal.HeaderText = "Postal Code";
            this.bPostal.Name = "bPostal";
            // 
            // bCountry
            // 
            this.bCountry.HeaderText = "Country";
            this.bCountry.Name = "bCountry";
            // 
            // bProvince
            // 
            this.bProvince.HeaderText = "Province";
            this.bProvince.Name = "bProvince";
            // 
            // bCity
            // 
            this.bCity.HeaderText = "City";
            this.bCity.Name = "bCity";
            // 
            // removeBtn
            // 
            this.removeBtn.HeaderText = "Delete";
            this.removeBtn.Name = "removeBtn";
            this.removeBtn.Width = 50;
            // 
            // addUserButton
            // 
            this.addUserButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.addUserButton.FlatAppearance.BorderSize = 0;
            this.addUserButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addUserButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.addUserButton.ForeColor = System.Drawing.Color.White;
            this.addUserButton.Location = new System.Drawing.Point(543, 107);
            this.addUserButton.Name = "addUserButton";
            this.addUserButton.Size = new System.Drawing.Size(124, 36);
            this.addUserButton.TabIndex = 65;
            this.addUserButton.Text = "Add User";
            this.addUserButton.UseVisualStyleBackColor = false;
            this.addUserButton.Click += new System.EventHandler(this.addUserButton_Click);
            // 
            // AddAddrButton
            // 
            this.AddAddrButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.AddAddrButton.FlatAppearance.BorderSize = 0;
            this.AddAddrButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddAddrButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.AddAddrButton.ForeColor = System.Drawing.Color.White;
            this.AddAddrButton.Location = new System.Drawing.Point(543, 284);
            this.AddAddrButton.Name = "AddAddrButton";
            this.AddAddrButton.Size = new System.Drawing.Size(124, 36);
            this.AddAddrButton.TabIndex = 66;
            this.AddAddrButton.Text = "Add Address";
            this.AddAddrButton.UseVisualStyleBackColor = false;
            this.AddAddrButton.Click += new System.EventHandler(this.AddAddrButton_Click);
            // 
            // FiscalPicker
            // 
            this.FiscalPicker.Location = new System.Drawing.Point(338, 28);
            this.FiscalPicker.Name = "FiscalPicker";
            this.FiscalPicker.Size = new System.Drawing.Size(177, 27);
            this.FiscalPicker.TabIndex = 67;
            this.FiscalPicker.Value = new System.DateTime(2013, 4, 7, 0, 0, 0, 0);
            // 
            // SaveBtn
            // 
            this.SaveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.SaveBtn.FlatAppearance.BorderSize = 0;
            this.SaveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SaveBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.SaveBtn.ForeColor = System.Drawing.Color.White;
            this.SaveBtn.Location = new System.Drawing.Point(589, 462);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(77, 36);
            this.SaveBtn.TabIndex = 68;
            this.SaveBtn.Text = "Save";
            this.SaveBtn.UseVisualStyleBackColor = false;
            // 
            // AdminUserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(679, 502);
            this.ControlBox = false;
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.FiscalPicker);
            this.Controls.Add(this.AddAddrButton);
            this.Controls.Add(this.addUserButton);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BAddrGridView);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BusinessNumTxtBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TaxNumBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CompanyNameTxtBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UserGridView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AdminUserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Edit Franchise Details";
            ((System.ComponentModel.ISupportInitialize)(this.UserGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BAddrGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView UserGridView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox CompanyNameTxtBox;
        private System.Windows.Forms.TextBox TaxNumBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox BusinessNumTxtBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView BAddrGridView;
        private System.Windows.Forms.Button addUserButton;
        private System.Windows.Forms.Button AddAddrButton;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn locID;
        private System.Windows.Forms.DataGridViewTextBoxColumn bAddr;
        private System.Windows.Forms.DataGridViewTextBoxColumn bPostal;
        private System.Windows.Forms.DataGridViewComboBoxColumn bCountry;
        private System.Windows.Forms.DataGridViewComboBoxColumn bProvince;
        private System.Windows.Forms.DataGridViewComboBoxColumn bCity;
        private System.Windows.Forms.DataGridViewButtonColumn removeBtn;
        private System.Windows.Forms.DateTimePicker FiscalPicker;
        private System.Windows.Forms.DataGridViewTextBoxColumn usrName;
        private System.Windows.Forms.DataGridViewTextBoxColumn usrIDCol;
        private System.Windows.Forms.DataGridViewButtonColumn PassCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn passSetCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn fName;
        private System.Windows.Forms.DataGridViewTextBoxColumn lName;
        private System.Windows.Forms.DataGridViewTextBoxColumn mName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PerOwnCol;
        private System.Windows.Forms.DataGridViewCheckBoxColumn adminCol;
        private System.Windows.Forms.DataGridViewCheckBoxColumn hqCol;
        private System.Windows.Forms.DataGridViewButtonColumn uDelete;
        private System.Windows.Forms.Button SaveBtn;
    }
}