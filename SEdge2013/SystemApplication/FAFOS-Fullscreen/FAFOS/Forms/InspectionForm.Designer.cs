namespace FAFOS
{
    partial class InspectionForm
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
            this.inspectionType = new System.Windows.Forms.ComboBox();
            this.generate_btn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlInspection = new System.Windows.Forms.Panel();
            this.addressBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlInspection.SuspendLayout();
            this.SuspendLayout();
            // 
            // inspectionType
            // 
            this.inspectionType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inspectionType.FormattingEnabled = true;
            this.inspectionType.Items.AddRange(new object[] {
            "Extinguisher Report"});
            this.inspectionType.Location = new System.Drawing.Point(25, 154);
            this.inspectionType.Name = "inspectionType";
            this.inspectionType.Size = new System.Drawing.Size(298, 25);
            this.inspectionType.TabIndex = 39;
            // 
            // generate_btn
            // 
            this.generate_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.generate_btn.FlatAppearance.BorderSize = 0;
            this.generate_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.generate_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generate_btn.ForeColor = System.Drawing.Color.White;
            this.generate_btn.Location = new System.Drawing.Point(349, 142);
            this.generate_btn.Margin = new System.Windows.Forms.Padding(4);
            this.generate_btn.Name = "generate_btn";
            this.generate_btn.Size = new System.Drawing.Size(182, 50);
            this.generate_btn.TabIndex = 52;
            this.generate_btn.Text = "Generate Report";
            this.generate_btn.UseVisualStyleBackColor = false;
            this.generate_btn.Click += new System.EventHandler(this.generate_btn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(23, 130);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(123, 17);
            this.label8.TabIndex = 53;
            this.label8.Text = "Inspection Report:";
            // 
            // pnlInspection
            // 
            this.pnlInspection.Controls.Add(this.addressBox);
            this.pnlInspection.Controls.Add(this.label1);
            this.pnlInspection.Controls.Add(this.inspectionType);
            this.pnlInspection.Controls.Add(this.label8);
            this.pnlInspection.Controls.Add(this.generate_btn);
            this.pnlInspection.Location = new System.Drawing.Point(65, 38);
            this.pnlInspection.Name = "pnlInspection";
            this.pnlInspection.Size = new System.Drawing.Size(551, 217);
            this.pnlInspection.TabIndex = 54;
            // 
            // addressBox
            // 
            this.addressBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addressBox.FormattingEnabled = true;
            this.addressBox.Items.AddRange(new object[] {
            "",
            "Extinguisher Report",
            "Hose Report",
            "Emergency Light Report"});
            this.addressBox.Location = new System.Drawing.Point(27, 59);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(298, 25);
            this.addressBox.TabIndex = 54;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 35);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 17);
            this.label1.TabIndex = 55;
            this.label1.Text = "Service Address:";
            // 
            // InspectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1386, 788);
            this.Controls.Add(this.pnlInspection);
            this.Name = "InspectionForm";
            this.Text = "InspectionForm";
            this.Controls.SetChildIndex(this.pnlInspection, 0);
            this.pnlInspection.ResumeLayout(false);
            this.pnlInspection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox inspectionType;
        private System.Windows.Forms.Button generate_btn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlInspection;
        private System.Windows.Forms.ComboBox addressBox;
        private System.Windows.Forms.Label label1;
    }
}