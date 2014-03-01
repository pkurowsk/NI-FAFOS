namespace FAFOS
{
    partial class AddEditContractForm
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
            this.Ok_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.StartDatePicker = new System.Windows.Forms.DateTimePicker();
            this.EndDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.TermsBox = new System.Windows.Forms.TextBox();
            this.delete_Button = new System.Windows.Forms.Button();
            this.contractNameBox = new System.Windows.Forms.TextBox();
            this.linkableClientBox = new System.Windows.Forms.ComboBox();
            this.new_Client_Button = new System.Windows.Forms.Button();
            this.ServiceAddrGridView = new System.Windows.Forms.DataGridView();
            this.idCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.address_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.postal_code_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.on_site_contact_col = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countryCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.provStateCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.cityCol = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.num_floors = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.editButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.deleteAddr = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Add_Row_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceAddrGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Ok_Button
            // 
            this.Ok_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.Ok_Button.FlatAppearance.BorderSize = 0;
            this.Ok_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Ok_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.Ok_Button.ForeColor = System.Drawing.Color.White;
            this.Ok_Button.Location = new System.Drawing.Point(1091, 662);
            this.Ok_Button.Margin = new System.Windows.Forms.Padding(4);
            this.Ok_Button.Name = "Ok_Button";
            this.Ok_Button.Size = new System.Drawing.Size(131, 57);
            this.Ok_Button.TabIndex = 0;
            this.Ok_Button.Text = "OK";
            this.Ok_Button.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(143, 159);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Contract Name:";
            // 
            // StartDatePicker
            // 
            this.StartDatePicker.Location = new System.Drawing.Point(143, 236);
            this.StartDatePicker.Margin = new System.Windows.Forms.Padding(4);
            this.StartDatePicker.Name = "StartDatePicker";
            this.StartDatePicker.Size = new System.Drawing.Size(168, 22);
            this.StartDatePicker.TabIndex = 6;
            // 
            // EndDatePicker
            // 
            this.EndDatePicker.Location = new System.Drawing.Point(321, 236);
            this.EndDatePicker.Margin = new System.Windows.Forms.Padding(4);
            this.EndDatePicker.Name = "EndDatePicker";
            this.EndDatePicker.Size = new System.Drawing.Size(168, 22);
            this.EndDatePicker.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(143, 216);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "Start Date:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(315, 216);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "End Date:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(139, 274);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 17);
            this.label4.TabIndex = 11;
            this.label4.Text = "Terms/Notes:";
            // 
            // TermsBox
            // 
            this.TermsBox.Location = new System.Drawing.Point(143, 294);
            this.TermsBox.Margin = new System.Windows.Forms.Padding(4);
            this.TermsBox.Multiline = true;
            this.TermsBox.Name = "TermsBox";
            this.TermsBox.Size = new System.Drawing.Size(369, 93);
            this.TermsBox.TabIndex = 10;
            // 
            // delete_Button
            // 
            this.delete_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.delete_Button.FlatAppearance.BorderSize = 0;
            this.delete_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.delete_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.delete_Button.ForeColor = System.Drawing.Color.White;
            this.delete_Button.Location = new System.Drawing.Point(147, 662);
            this.delete_Button.Margin = new System.Windows.Forms.Padding(4);
            this.delete_Button.Name = "delete_Button";
            this.delete_Button.Size = new System.Drawing.Size(131, 57);
            this.delete_Button.TabIndex = 32;
            this.delete_Button.Text = "Delete";
            this.delete_Button.UseVisualStyleBackColor = false;
            // 
            // contractNameBox
            // 
            this.contractNameBox.Location = new System.Drawing.Point(147, 180);
            this.contractNameBox.Margin = new System.Windows.Forms.Padding(4);
            this.contractNameBox.Name = "contractNameBox";
            this.contractNameBox.Size = new System.Drawing.Size(151, 22);
            this.contractNameBox.TabIndex = 38;
            // 
            // linkableClientBox
            // 
            this.linkableClientBox.FormattingEnabled = true;
            this.linkableClientBox.Location = new System.Drawing.Point(645, 178);
            this.linkableClientBox.Margin = new System.Windows.Forms.Padding(4);
            this.linkableClientBox.Name = "linkableClientBox";
            this.linkableClientBox.Size = new System.Drawing.Size(160, 24);
            this.linkableClientBox.TabIndex = 39;
            this.linkableClientBox.Visible = false;
            // 
            // new_Client_Button
            // 
            this.new_Client_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.new_Client_Button.FlatAppearance.BorderSize = 0;
            this.new_Client_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.new_Client_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.new_Client_Button.ForeColor = System.Drawing.Color.White;
            this.new_Client_Button.Location = new System.Drawing.Point(841, 171);
            this.new_Client_Button.Margin = new System.Windows.Forms.Padding(4);
            this.new_Client_Button.Name = "new_Client_Button";
            this.new_Client_Button.Size = new System.Drawing.Size(145, 37);
            this.new_Client_Button.TabIndex = 40;
            this.new_Client_Button.Text = "New Client";
            this.new_Client_Button.UseVisualStyleBackColor = false;
            this.new_Client_Button.Visible = false;
            this.new_Client_Button.Click += new System.EventHandler(this.new_Client_Button_Click);
            // 
            // ServiceAddrGridView
            // 
            this.ServiceAddrGridView.AllowUserToAddRows = false;
            this.ServiceAddrGridView.AllowUserToDeleteRows = false;
            this.ServiceAddrGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ServiceAddrGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idCol,
            this.address_col,
            this.postal_code_col,
            this.on_site_contact_col,
            this.countryCol,
            this.provStateCol,
            this.cityCol,
            this.num_floors,
            this.roomButton,
            this.editButton,
            this.deleteAddr});
            this.ServiceAddrGridView.Location = new System.Drawing.Point(147, 395);
            this.ServiceAddrGridView.Margin = new System.Windows.Forms.Padding(4);
            this.ServiceAddrGridView.Name = "ServiceAddrGridView";
            this.ServiceAddrGridView.Size = new System.Drawing.Size(1075, 245);
            this.ServiceAddrGridView.TabIndex = 12;
            this.ServiceAddrGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.ServiceAddrGridView_DataError);
            // 
            // idCol
            // 
            this.idCol.HeaderText = "ID";
            this.idCol.Name = "idCol";
            this.idCol.Visible = false;
            // 
            // address_col
            // 
            this.address_col.HeaderText = "Address";
            this.address_col.Name = "address_col";
            this.address_col.Width = 160;
            // 
            // postal_code_col
            // 
            this.postal_code_col.HeaderText = "Postal Code";
            this.postal_code_col.Name = "postal_code_col";
            this.postal_code_col.Width = 80;
            // 
            // on_site_contact_col
            // 
            this.on_site_contact_col.HeaderText = "OnSite Contact";
            this.on_site_contact_col.Name = "on_site_contact_col";
            this.on_site_contact_col.Width = 120;
            // 
            // countryCol
            // 
            this.countryCol.DataPropertyName = "table";
            this.countryCol.HeaderText = "Country";
            this.countryCol.Name = "countryCol";
            this.countryCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.countryCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // provStateCol
            // 
            this.provStateCol.DataPropertyName = "table";
            this.provStateCol.HeaderText = "Province/State";
            this.provStateCol.Name = "provStateCol";
            this.provStateCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.provStateCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.provStateCol.Width = 120;
            // 
            // cityCol
            // 
            this.cityCol.DataPropertyName = "table";
            this.cityCol.HeaderText = "City";
            this.cityCol.Name = "cityCol";
            this.cityCol.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cityCol.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // num_floors
            // 
            this.num_floors.HeaderText = "Floors";
            this.num_floors.Name = "num_floors";
            this.num_floors.Width = 60;
            // 
            // roomButton
            // 
            this.roomButton.HeaderText = "Rooms";
            this.roomButton.Name = "roomButton";
            this.roomButton.Width = 60;
            // 
            // editButton
            // 
            this.editButton.HeaderText = "Edit Services";
            this.editButton.Name = "editButton";
            this.editButton.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.editButton.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.editButton.Width = 140;
            // 
            // deleteAddr
            // 
            this.deleteAddr.HeaderText = "Remove";
            this.deleteAddr.Name = "deleteAddr";
            this.deleteAddr.Width = 90;
            // 
            // Add_Row_Button
            // 
            this.Add_Row_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.Add_Row_Button.FlatAppearance.BorderSize = 0;
            this.Add_Row_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Add_Row_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.Add_Row_Button.ForeColor = System.Drawing.Color.White;
            this.Add_Row_Button.Location = new System.Drawing.Point(1077, 350);
            this.Add_Row_Button.Margin = new System.Windows.Forms.Padding(4);
            this.Add_Row_Button.Name = "Add_Row_Button";
            this.Add_Row_Button.Size = new System.Drawing.Size(145, 37);
            this.Add_Row_Button.TabIndex = 42;
            this.Add_Row_Button.Text = "Add Address";
            this.Add_Row_Button.UseVisualStyleBackColor = false;
            this.Add_Row_Button.Click += new System.EventHandler(this.Add_New_Row);
            // 
            // AddEditContractForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1452, 912);
            this.Controls.Add(this.Add_Row_Button);
            this.Controls.Add(this.new_Client_Button);
            this.Controls.Add(this.linkableClientBox);
            this.Controls.Add(this.contractNameBox);
            this.Controls.Add(this.delete_Button);
            this.Controls.Add(this.ServiceAddrGridView);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TermsBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.EndDatePicker);
            this.Controls.Add(this.StartDatePicker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ok_Button);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "AddEditContractForm";
            this.Controls.SetChildIndex(this.Ok_Button, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.StartDatePicker, 0);
            this.Controls.SetChildIndex(this.EndDatePicker, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.TermsBox, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.ServiceAddrGridView, 0);
            this.Controls.SetChildIndex(this.delete_Button, 0);
            this.Controls.SetChildIndex(this.contractNameBox, 0);
            this.Controls.SetChildIndex(this.linkableClientBox, 0);
            this.Controls.SetChildIndex(this.new_Client_Button, 0);
            this.Controls.SetChildIndex(this.Add_Row_Button, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ServiceAddrGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ok_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker StartDatePicker;
        private System.Windows.Forms.DateTimePicker EndDatePicker;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TermsBox;
        private System.Windows.Forms.Button delete_Button;
        private System.Windows.Forms.TextBox contractNameBox;
        private System.Windows.Forms.ComboBox linkableClientBox;
        private System.Windows.Forms.Button new_Client_Button;
        private System.Windows.Forms.DataGridView ServiceAddrGridView;
        private System.Windows.Forms.Button Add_Row_Button;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn address_col;
        private System.Windows.Forms.DataGridViewTextBoxColumn postal_code_col;
        private System.Windows.Forms.DataGridViewTextBoxColumn on_site_contact_col;
        private System.Windows.Forms.DataGridViewComboBoxColumn countryCol;
        private System.Windows.Forms.DataGridViewComboBoxColumn provStateCol;
        private System.Windows.Forms.DataGridViewComboBoxColumn cityCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn num_floors;
        private System.Windows.Forms.DataGridViewButtonColumn roomButton;
        private System.Windows.Forms.DataGridViewButtonColumn editButton;
        private System.Windows.Forms.DataGridViewButtonColumn deleteAddr;
    }
}