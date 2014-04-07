namespace FAFOS
{
    partial class SupplierForm
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
            this.lblSupplierName = new System.Windows.Forms.Label();
            this.txtSupplierSelect = new System.Windows.Forms.ComboBox();
            this.txtCreatSupplier = new System.Windows.Forms.TextBox();
            this.lblAddSupplier = new System.Windows.Forms.Label();
            this.btnDeleteSupplier = new System.Windows.Forms.Button();
            this.btnAddSupplier = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSupplierName
            // 
            this.lblSupplierName.AutoSize = true;
            this.lblSupplierName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.lblSupplierName.Location = new System.Drawing.Point(73, 39);
            this.lblSupplierName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSupplierName.Name = "lblSupplierName";
            this.lblSupplierName.Size = new System.Drawing.Size(103, 17);
            this.lblSupplierName.TabIndex = 40;
            this.lblSupplierName.Text = "Select Supplier";
            // 
            // txtSupplierSelect
            // 
            this.txtSupplierSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSupplierSelect.FormattingEnabled = true;
            this.txtSupplierSelect.Location = new System.Drawing.Point(76, 59);
            this.txtSupplierSelect.Name = "txtSupplierSelect";
            this.txtSupplierSelect.Size = new System.Drawing.Size(214, 25);
            this.txtSupplierSelect.TabIndex = 41;
            // 
            // txtCreatSupplier
            // 
            this.txtCreatSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtCreatSupplier.Location = new System.Drawing.Point(76, 162);
            this.txtCreatSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.txtCreatSupplier.Name = "txtCreatSupplier";
            this.txtCreatSupplier.Size = new System.Drawing.Size(214, 23);
            this.txtCreatSupplier.TabIndex = 42;
            // 
            // lblAddSupplier
            // 
            this.lblAddSupplier.AutoSize = true;
            this.lblAddSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.lblAddSupplier.Location = new System.Drawing.Point(73, 141);
            this.lblAddSupplier.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAddSupplier.Name = "lblAddSupplier";
            this.lblAddSupplier.Size = new System.Drawing.Size(89, 17);
            this.lblAddSupplier.TabIndex = 43;
            this.lblAddSupplier.Text = "Add Supplier";
            // 
            // btnDeleteSupplier
            // 
            this.btnDeleteSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.btnDeleteSupplier.FlatAppearance.BorderSize = 0;
            this.btnDeleteSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteSupplier.ForeColor = System.Drawing.Color.White;
            this.btnDeleteSupplier.Location = new System.Drawing.Point(312, 59);
            this.btnDeleteSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.btnDeleteSupplier.Name = "btnDeleteSupplier";
            this.btnDeleteSupplier.Size = new System.Drawing.Size(127, 70);
            this.btnDeleteSupplier.TabIndex = 44;
            this.btnDeleteSupplier.Text = "Delete Supplier";
            this.btnDeleteSupplier.UseVisualStyleBackColor = false;
            // 
            // btnAddSupplier
            // 
            this.btnAddSupplier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.btnAddSupplier.FlatAppearance.BorderSize = 0;
            this.btnAddSupplier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSupplier.ForeColor = System.Drawing.Color.White;
            this.btnAddSupplier.Location = new System.Drawing.Point(312, 141);
            this.btnAddSupplier.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddSupplier.Name = "btnAddSupplier";
            this.btnAddSupplier.Size = new System.Drawing.Size(127, 70);
            this.btnAddSupplier.TabIndex = 45;
            this.btnAddSupplier.Text = "Add Supplier";
            this.btnAddSupplier.UseVisualStyleBackColor = false;
            // 
            // SupplierForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 359);
            this.Controls.Add(this.btnAddSupplier);
            this.Controls.Add(this.btnDeleteSupplier);
            this.Controls.Add(this.lblAddSupplier);
            this.Controls.Add(this.txtCreatSupplier);
            this.Controls.Add(this.txtSupplierSelect);
            this.Controls.Add(this.lblSupplierName);
            this.Name = "SupplierForm";
            this.Text = "Add/Edit Supplier";
            this.Controls.SetChildIndex(this.lblSupplierName, 0);
            this.Controls.SetChildIndex(this.txtSupplierSelect, 0);
            this.Controls.SetChildIndex(this.txtCreatSupplier, 0);
            this.Controls.SetChildIndex(this.lblAddSupplier, 0);
            this.Controls.SetChildIndex(this.btnDeleteSupplier, 0);
            this.Controls.SetChildIndex(this.btnAddSupplier, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSupplierName;
        private System.Windows.Forms.ComboBox txtSupplierSelect;
        private System.Windows.Forms.TextBox txtCreatSupplier;
        private System.Windows.Forms.Label lblAddSupplier;
        private System.Windows.Forms.Button btnDeleteSupplier;
        private System.Windows.Forms.Button btnAddSupplier;
    }
}