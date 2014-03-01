namespace FAFOS
{
    partial class Sales_Order
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPreview = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtHST = new System.Windows.Forms.TextBox();
            this.lblHST = new System.Windows.Forms.Label();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.btnPullData = new System.Windows.Forms.Button();
            this.lblGetID = new System.Windows.Forms.Label();
            this.lblServiceAddress = new System.Windows.Forms.Label();
            this.lblClient = new System.Windows.Forms.Label();
            this.ddlClient = new System.Windows.Forms.ComboBox();
            this.ddlServiceAddress = new System.Windows.Forms.ComboBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.dgvSalesOrder = new System.Windows.Forms.DataGridView();
            this.orderItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.item = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lineTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chkCompleted = new System.Windows.Forms.CheckBox();
            this.txtGetID = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesOrder)).BeginInit();
            this.SuspendLayout();
            // 
            // btnPreview
            // 
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.btnPreview.FlatAppearance.BorderSize = 0;
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.btnPreview.ForeColor = System.Drawing.Color.White;
            this.btnPreview.Location = new System.Drawing.Point(796, 726);
            this.btnPreview.Margin = new System.Windows.Forms.Padding(4);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(131, 57);
            this.btnPreview.TabIndex = 53;
            this.btnPreview.Text = "Preview";
            this.btnPreview.UseVisualStyleBackColor = false;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.lblTotal.Location = new System.Drawing.Point(922, 680);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(51, 20);
            this.lblTotal.TabIndex = 52;
            this.lblTotal.Text = "Total";
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(995, 676);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(132, 27);
            this.txtTotal.TabIndex = 51;
            // 
            // txtHST
            // 
            this.txtHST.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHST.Location = new System.Drawing.Point(995, 637);
            this.txtHST.Margin = new System.Windows.Forms.Padding(4);
            this.txtHST.Name = "txtHST";
            this.txtHST.Size = new System.Drawing.Size(132, 27);
            this.txtHST.TabIndex = 50;
            // 
            // lblHST
            // 
            this.lblHST.AutoSize = true;
            this.lblHST.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.lblHST.Location = new System.Drawing.Point(925, 641);
            this.lblHST.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHST.Name = "lblHST";
            this.lblHST.Size = new System.Drawing.Size(44, 20);
            this.lblHST.TabIndex = 49;
            this.lblHST.Text = "HST";
            // 
            // txtSubtotal
            // 
            this.txtSubtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubtotal.Location = new System.Drawing.Point(995, 599);
            this.txtSubtotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubtotal.Name = "txtSubtotal";
            this.txtSubtotal.Size = new System.Drawing.Size(132, 27);
            this.txtSubtotal.TabIndex = 48;
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.lblSubtotal.Location = new System.Drawing.Point(902, 601);
            this.lblSubtotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(78, 20);
            this.lblSubtotal.TabIndex = 47;
            this.lblSubtotal.Text = "Subtotal";
            // 
            // btnPullData
            // 
            this.btnPullData.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.btnPullData.FlatAppearance.BorderSize = 0;
            this.btnPullData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPullData.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.btnPullData.ForeColor = System.Drawing.Color.White;
            this.btnPullData.Location = new System.Drawing.Point(330, 243);
            this.btnPullData.Margin = new System.Windows.Forms.Padding(4);
            this.btnPullData.Name = "btnPullData";
            this.btnPullData.Size = new System.Drawing.Size(131, 57);
            this.btnPullData.TabIndex = 46;
            this.btnPullData.UseVisualStyleBackColor = false;
            this.btnPullData.Visible = false;
            // 
            // lblGetID
            // 
            this.lblGetID.AutoSize = true;
            this.lblGetID.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.lblGetID.Location = new System.Drawing.Point(180, 212);
            this.lblGetID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGetID.Name = "lblGetID";
            this.lblGetID.Size = new System.Drawing.Size(0, 20);
            this.lblGetID.TabIndex = 44;
            this.lblGetID.Visible = false;
            // 
            // lblServiceAddress
            // 
            this.lblServiceAddress.AutoSize = true;
            this.lblServiceAddress.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.lblServiceAddress.Location = new System.Drawing.Point(826, 243);
            this.lblServiceAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServiceAddress.Name = "lblServiceAddress";
            this.lblServiceAddress.Size = new System.Drawing.Size(144, 20);
            this.lblServiceAddress.TabIndex = 43;
            this.lblServiceAddress.Text = "Service Address";
            // 
            // lblClient
            // 
            this.lblClient.AutoSize = true;
            this.lblClient.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.lblClient.Location = new System.Drawing.Point(146, 302);
            this.lblClient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(58, 20);
            this.lblClient.TabIndex = 42;
            this.lblClient.Text = "Client";
            this.lblClient.Visible = false;
            // 
            // ddlClient
            // 
            this.ddlClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlClient.FormattingEnabled = true;
            this.ddlClient.Location = new System.Drawing.Point(146, 324);
            this.ddlClient.Margin = new System.Windows.Forms.Padding(4);
            this.ddlClient.Name = "ddlClient";
            this.ddlClient.Size = new System.Drawing.Size(160, 28);
            this.ddlClient.TabIndex = 41;
            this.ddlClient.Visible = false;
            // 
            // ddlServiceAddress
            // 
            this.ddlServiceAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlServiceAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlServiceAddress.FormattingEnabled = true;
            this.ddlServiceAddress.Location = new System.Drawing.Point(826, 267);
            this.ddlServiceAddress.Margin = new System.Windows.Forms.Padding(4);
            this.ddlServiceAddress.Name = "ddlServiceAddress";
            this.ddlServiceAddress.Size = new System.Drawing.Size(275, 28);
            this.ddlServiceAddress.TabIndex = 40;
            // 
            // btnSubmit
            // 
            this.btnSubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.btnSubmit.FlatAppearance.BorderSize = 0;
            this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSubmit.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.btnSubmit.ForeColor = System.Drawing.Color.White;
            this.btnSubmit.Location = new System.Drawing.Point(997, 726);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(4);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(131, 57);
            this.btnSubmit.TabIndex = 39;
            this.btnSubmit.Text = "Create";
            this.btnSubmit.UseVisualStyleBackColor = false;
            // 
            // dgvSalesOrder
            // 
            this.dgvSalesOrder.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSalesOrder.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.orderItemID,
            this.item,
            this.description,
            this.hours,
            this.Qty,
            this.Price,
            this.lineTotal});
            this.dgvSalesOrder.Location = new System.Drawing.Point(130, 380);
            this.dgvSalesOrder.Margin = new System.Windows.Forms.Padding(4);
            this.dgvSalesOrder.Name = "dgvSalesOrder";
            this.dgvSalesOrder.Size = new System.Drawing.Size(997, 185);
            this.dgvSalesOrder.TabIndex = 38;
            // 
            // orderItemID
            // 
            this.orderItemID.HeaderText = "#";
            this.orderItemID.Name = "orderItemID";
            // 
            // item
            // 
            this.item.HeaderText = "Item";
            this.item.Name = "item";
            this.item.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.item.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.item.Width = 200;
            // 
            // description
            // 
            this.description.HeaderText = "Description";
            this.description.Name = "description";
            this.description.ReadOnly = true;
            this.description.Width = 150;
            // 
            // hours
            // 
            this.hours.HeaderText = "Hours";
            this.hours.Name = "hours";
            // 
            // Qty
            // 
            this.Qty.HeaderText = "Qty";
            this.Qty.Name = "Qty";
            // 
            // Price
            // 
            this.Price.HeaderText = "Price";
            this.Price.Name = "Price";
            // 
            // lineTotal
            // 
            this.lineTotal.HeaderText = "Line Total";
            this.lineTotal.Name = "lineTotal";
            // 
            // chkCompleted
            // 
            this.chkCompleted.AutoSize = true;
            this.chkCompleted.Location = new System.Drawing.Point(950, 351);
            this.chkCompleted.Margin = new System.Windows.Forms.Padding(4);
            this.chkCompleted.Name = "chkCompleted";
            this.chkCompleted.Size = new System.Drawing.Size(172, 21);
            this.chkCompleted.TabIndex = 54;
            this.chkCompleted.Text = "Completed Inspections";
            this.chkCompleted.UseVisualStyleBackColor = true;
            // 
            // txtGetID
            // 
            this.txtGetID.FormattingEnabled = true;
            this.txtGetID.Location = new System.Drawing.Point(146, 257);
            this.txtGetID.Margin = new System.Windows.Forms.Padding(4);
            this.txtGetID.Name = "txtGetID";
            this.txtGetID.Size = new System.Drawing.Size(160, 24);
            this.txtGetID.TabIndex = 55;
            this.txtGetID.Visible = false;
            // 
            // Sales_Order
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1452, 815);
            this.Controls.Add(this.txtGetID);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtHST);
            this.Controls.Add(this.lblHST);
            this.Controls.Add(this.txtSubtotal);
            this.Controls.Add(this.chkCompleted);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.btnPullData);
            this.Controls.Add(this.lblGetID);
            this.Controls.Add(this.lblServiceAddress);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.ddlClient);
            this.Controls.Add(this.ddlServiceAddress);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.dgvSalesOrder);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Sales_Order";
            this.Text = "Sales_Order";
            this.Controls.SetChildIndex(this.dgvSalesOrder, 0);
            this.Controls.SetChildIndex(this.btnSubmit, 0);
            this.Controls.SetChildIndex(this.ddlServiceAddress, 0);
            this.Controls.SetChildIndex(this.ddlClient, 0);
            this.Controls.SetChildIndex(this.lblClient, 0);
            this.Controls.SetChildIndex(this.lblServiceAddress, 0);
            this.Controls.SetChildIndex(this.lblGetID, 0);
            this.Controls.SetChildIndex(this.btnPullData, 0);
            this.Controls.SetChildIndex(this.lblSubtotal, 0);
            this.Controls.SetChildIndex(this.chkCompleted, 0);
            this.Controls.SetChildIndex(this.txtSubtotal, 0);
            this.Controls.SetChildIndex(this.lblHST, 0);
            this.Controls.SetChildIndex(this.txtHST, 0);
            this.Controls.SetChildIndex(this.txtTotal, 0);
            this.Controls.SetChildIndex(this.lblTotal, 0);
            this.Controls.SetChildIndex(this.btnPreview, 0);
            this.Controls.SetChildIndex(this.txtGetID, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSalesOrder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtHST;
        private System.Windows.Forms.Label lblHST;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Button btnPullData;
        private System.Windows.Forms.Label lblGetID;
        private System.Windows.Forms.Label lblServiceAddress;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.ComboBox ddlClient;
        private System.Windows.Forms.ComboBox ddlServiceAddress;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.DataGridView dgvSalesOrder;
        private System.Windows.Forms.CheckBox chkCompleted;
        private System.Windows.Forms.ComboBox txtGetID;
        private System.Windows.Forms.DataGridViewTextBoxColumn orderItemID;
        private System.Windows.Forms.DataGridViewComboBoxColumn item;
        private System.Windows.Forms.DataGridViewTextBoxColumn description;
        private System.Windows.Forms.DataGridViewTextBoxColumn hours;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn lineTotal;
    }
}