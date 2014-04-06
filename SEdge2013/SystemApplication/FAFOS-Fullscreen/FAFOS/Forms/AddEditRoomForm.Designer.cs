namespace FAFOS
{
    partial class AddEditRoomForm
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
            this.Cancel_Button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Ok_Button = new System.Windows.Forms.Button();
            this.RoomGridView = new System.Windows.Forms.DataGridView();
            this.idCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.roomNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.floor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.extCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.hoseCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lightCol = new System.Windows.Forms.DataGridViewButtonColumn();
            this.deleteButton = new System.Windows.Forms.DataGridViewButtonColumn();
            this.addRoomButton = new System.Windows.Forms.Button();
            this.AddItemButton = new System.Windows.Forms.Button();
            this.DatePicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.RoomGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // Cancel_Button
            // 
            this.Cancel_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.Cancel_Button.FlatAppearance.BorderSize = 0;
            this.Cancel_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Cancel_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.Cancel_Button.ForeColor = System.Drawing.Color.White;
            this.Cancel_Button.Location = new System.Drawing.Point(904, 337);
            this.Cancel_Button.Name = "Cancel_Button";
            this.Cancel_Button.Size = new System.Drawing.Size(98, 46);
            this.Cancel_Button.TabIndex = 59;
            this.Cancel_Button.Text = "Cancel";
            this.Cancel_Button.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.label1.Location = new System.Drawing.Point(2, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 34;
            this.label1.Text = "Rooms:";
            // 
            // Ok_Button
            // 
            this.Ok_Button.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.Ok_Button.FlatAppearance.BorderSize = 0;
            this.Ok_Button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Ok_Button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.Ok_Button.ForeColor = System.Drawing.Color.White;
            this.Ok_Button.Location = new System.Drawing.Point(799, 337);
            this.Ok_Button.Name = "Ok_Button";
            this.Ok_Button.Size = new System.Drawing.Size(98, 46);
            this.Ok_Button.TabIndex = 31;
            this.Ok_Button.Text = "Ok";
            this.Ok_Button.UseVisualStyleBackColor = false;
            // 
            // RoomGridView
            // 
            this.RoomGridView.AllowUserToAddRows = false;
            this.RoomGridView.AllowUserToDeleteRows = false;
            this.RoomGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RoomGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idCol,
            this.roomNum,
            this.floor,
            this.extCol,
            this.hoseCol,
            this.lightCol,
            this.deleteButton});
            this.RoomGridView.Location = new System.Drawing.Point(5, 29);
            this.RoomGridView.Name = "RoomGridView";
            this.RoomGridView.Size = new System.Drawing.Size(554, 145);
            this.RoomGridView.TabIndex = 60;
            this.RoomGridView.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.RoomGridView_RowsRemoved);
            // 
            // idCol
            // 
            this.idCol.HeaderText = "ID";
            this.idCol.Name = "idCol";
            this.idCol.ReadOnly = true;
            this.idCol.Visible = false;
            // 
            // roomNum
            // 
            this.roomNum.HeaderText = "Room #";
            this.roomNum.Name = "roomNum";
            this.roomNum.Width = 70;
            // 
            // floor
            // 
            this.floor.HeaderText = "Floor";
            this.floor.Name = "floor";
            this.floor.Width = 60;
            // 
            // extCol
            // 
            this.extCol.HeaderText = "Extinguishers";
            this.extCol.Name = "extCol";
            this.extCol.Width = 150;
            // 
            // hoseCol
            // 
            this.hoseCol.HeaderText = "Hoses";
            this.hoseCol.Name = "hoseCol";
            this.hoseCol.Width = 80;
            // 
            // lightCol
            // 
            this.lightCol.HeaderText = "Lights";
            this.lightCol.Name = "lightCol";
            this.lightCol.Width = 80;
            // 
            // deleteButton
            // 
            this.deleteButton.HeaderText = "Delete";
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.ReadOnly = true;
            this.deleteButton.Width = 70;
            // 
            // addRoomButton
            // 
            this.addRoomButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.addRoomButton.FlatAppearance.BorderSize = 0;
            this.addRoomButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addRoomButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.addRoomButton.ForeColor = System.Drawing.Color.White;
            this.addRoomButton.Location = new System.Drawing.Point(565, 29);
            this.addRoomButton.Name = "addRoomButton";
            this.addRoomButton.Size = new System.Drawing.Size(101, 36);
            this.addRoomButton.TabIndex = 64;
            this.addRoomButton.Text = "Add Room";
            this.addRoomButton.UseVisualStyleBackColor = false;
            this.addRoomButton.Click += new System.EventHandler(this.addRoomButton_Click);
            // 
            // AddItemButton
            // 
            this.AddItemButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.AddItemButton.FlatAppearance.BorderSize = 0;
            this.AddItemButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddItemButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.AddItemButton.ForeColor = System.Drawing.Color.White;
            this.AddItemButton.Location = new System.Drawing.Point(799, 274);
            this.AddItemButton.Name = "AddItemButton";
            this.AddItemButton.Size = new System.Drawing.Size(127, 36);
            this.AddItemButton.TabIndex = 67;
            this.AddItemButton.Text = "Add Extinguisher";
            this.AddItemButton.UseVisualStyleBackColor = false;
            this.AddItemButton.Visible = false;
            this.AddItemButton.Click += new System.EventHandler(this.AddItemButton_Click);
            // 
            // DatePicker
            // 
            this.DatePicker.Location = new System.Drawing.Point(436, 180);
            this.DatePicker.Name = "DatePicker";
            this.DatePicker.Size = new System.Drawing.Size(123, 20);
            this.DatePicker.TabIndex = 68;
            this.DatePicker.Value = new System.DateTime(2013, 4, 4, 0, 0, 0, 0);
            this.DatePicker.Visible = false;
            this.DatePicker.ValueChanged += new System.EventHandler(this.DatePicker_ValueChanged);
            // 
            // AddEditRoomForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1014, 395);
            this.ControlBox = false;
            this.Controls.Add(this.DatePicker);
            this.Controls.Add(this.AddItemButton);
            this.Controls.Add(this.addRoomButton);
            this.Controls.Add(this.RoomGridView);
            this.Controls.Add(this.Cancel_Button);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ok_Button);
            this.Name = "AddEditRoomForm";
            this.ShowInTaskbar = false;
            this.Text = "Maintain Rooms For <unkown>";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.RoomGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Cancel_Button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Ok_Button;
        private System.Windows.Forms.DataGridView RoomGridView;
        private System.Windows.Forms.Button addRoomButton;
        private System.Windows.Forms.Button AddItemButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn idCol;
        private System.Windows.Forms.DataGridViewTextBoxColumn roomNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn floor;
        private System.Windows.Forms.DataGridViewButtonColumn extCol;
        private System.Windows.Forms.DataGridViewButtonColumn hoseCol;
        private System.Windows.Forms.DataGridViewButtonColumn lightCol;
        private System.Windows.Forms.DataGridViewButtonColumn deleteButton;
        private System.Windows.Forms.DateTimePicker DatePicker;
    }
}