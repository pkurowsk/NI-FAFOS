namespace FAFOS
{
    partial class InventoryForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.productsdgv = new System.Windows.Forms.DataGridView();
            this.Save_btn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.servicesdgv = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.productnametextBox = new System.Windows.Forms.TextBox();
            this.productdescriptiontextBox = new System.Windows.Forms.TextBox();
            this.productpricetextBox = new System.Windows.Forms.TextBox();
            this.category = new System.Windows.Forms.ComboBox();
            this.supplier = new System.Windows.Forms.ComboBox();
            this.AddProduct_btn = new System.Windows.Forms.Button();
            this.AddService_btn = new System.Windows.Forms.Button();
            this.servicepricetextbox = new System.Windows.Forms.TextBox();
            this.servicedescriptiontextbox = new System.Windows.Forms.TextBox();
            this.servicenametextbox = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.productsearch = new System.Windows.Forms.ComboBox();
            this.serviceSearch = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cost = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.retailPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SupplierName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteproduct = new System.Windows.Forms.DataGridViewButtonColumn();
            this.number1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.deleteService = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.productsdgv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.servicesdgv)).BeginInit();
            this.SuspendLayout();
            // 
            // productsdgv
            // 
            this.productsdgv.AllowUserToAddRows = false;
            this.productsdgv.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productsdgv.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.productsdgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.productsdgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.productsdgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.productsdgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.number,
            this.productID,
            this.productName,
            this.productDescription,
            this.Cost,
            this.retailPrice,
            this.productQuantity,
            this.CategoryName,
            this.SupplierName,
            this.deleteproduct});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.productsdgv.DefaultCellStyle = dataGridViewCellStyle3;
            this.productsdgv.Location = new System.Drawing.Point(51, 216);
            this.productsdgv.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.productsdgv.MultiSelect = false;
            this.productsdgv.Name = "productsdgv";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.productsdgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productsdgv.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.productsdgv.RowTemplate.Height = 24;
            this.productsdgv.Size = new System.Drawing.Size(844, 253);
            this.productsdgv.TabIndex = 13;
            // 
            // Save_btn
            // 
            this.Save_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.Save_btn.FlatAppearance.BorderSize = 0;
            this.Save_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Save_btn.ForeColor = System.Drawing.Color.White;
            this.Save_btn.Location = new System.Drawing.Point(729, 830);
            this.Save_btn.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Save_btn.Name = "Save_btn";
            this.Save_btn.Size = new System.Drawing.Size(166, 52);
            this.Save_btn.TabIndex = 17;
            this.Save_btn.Text = "Save Updates";
            this.Save_btn.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(47, 188);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 23);
            this.label1.TabIndex = 18;
            this.label1.Text = "Products";
            // 
            // servicesdgv
            // 
            this.servicesdgv.AllowUserToAddRows = false;
            this.servicesdgv.AllowUserToDeleteRows = false;
            this.servicesdgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.servicesdgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.servicesdgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.servicesdgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.number1,
            this.itemID,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.deleteService});
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.servicesdgv.DefaultCellStyle = dataGridViewCellStyle7;
            this.servicesdgv.Location = new System.Drawing.Point(51, 557);
            this.servicesdgv.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.servicesdgv.Name = "servicesdgv";
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.servicesdgv.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.servicesdgv.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.servicesdgv.RowTemplate.Height = 24;
            this.servicesdgv.Size = new System.Drawing.Size(774, 266);
            this.servicesdgv.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(47, 532);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 23);
            this.label2.TabIndex = 20;
            this.label2.Text = "Services";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(908, 217);
            this.label3.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(133, 23);
            this.label3.TabIndex = 23;
            this.label3.Text = "Add Product";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.label4.Location = new System.Drawing.Point(908, 252);
            this.label4.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 20);
            this.label4.TabIndex = 24;
            this.label4.Text = "Name";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.label5.Location = new System.Drawing.Point(907, 296);
            this.label5.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 20);
            this.label5.TabIndex = 25;
            this.label5.Text = "Description";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.label6.Location = new System.Drawing.Point(908, 339);
            this.label6.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 20);
            this.label6.TabIndex = 26;
            this.label6.Text = "Retail Price";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.label9.Location = new System.Drawing.Point(908, 381);
            this.label9.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(85, 20);
            this.label9.TabIndex = 29;
            this.label9.Text = "Category";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.label10.Location = new System.Drawing.Point(908, 421);
            this.label10.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(79, 20);
            this.label10.TabIndex = 30;
            this.label10.Text = "Supplier";
            // 
            // productnametextBox
            // 
            this.productnametextBox.Location = new System.Drawing.Point(1047, 249);
            this.productnametextBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.productnametextBox.Name = "productnametextBox";
            this.productnametextBox.Size = new System.Drawing.Size(259, 27);
            this.productnametextBox.TabIndex = 35;
            // 
            // productdescriptiontextBox
            // 
            this.productdescriptiontextBox.Location = new System.Drawing.Point(1047, 293);
            this.productdescriptiontextBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.productdescriptiontextBox.Name = "productdescriptiontextBox";
            this.productdescriptiontextBox.Size = new System.Drawing.Size(259, 27);
            this.productdescriptiontextBox.TabIndex = 36;
            // 
            // productpricetextBox
            // 
            this.productpricetextBox.Location = new System.Drawing.Point(1047, 336);
            this.productpricetextBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.productpricetextBox.Name = "productpricetextBox";
            this.productpricetextBox.Size = new System.Drawing.Size(163, 27);
            this.productpricetextBox.TabIndex = 37;
            // 
            // category
            // 
            this.category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.category.FormattingEnabled = true;
            this.category.Location = new System.Drawing.Point(1047, 378);
            this.category.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.category.Name = "category";
            this.category.Size = new System.Drawing.Size(259, 28);
            this.category.TabIndex = 39;
            // 
            // supplier
            // 
            this.supplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.supplier.FormattingEnabled = true;
            this.supplier.Location = new System.Drawing.Point(1047, 421);
            this.supplier.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.supplier.Name = "supplier";
            this.supplier.Size = new System.Drawing.Size(259, 28);
            this.supplier.TabIndex = 40;
            // 
            // AddProduct_btn
            // 
            this.AddProduct_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.AddProduct_btn.FlatAppearance.BorderSize = 0;
            this.AddProduct_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddProduct_btn.ForeColor = System.Drawing.Color.White;
            this.AddProduct_btn.Location = new System.Drawing.Point(1129, 459);
            this.AddProduct_btn.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.AddProduct_btn.Name = "AddProduct_btn";
            this.AddProduct_btn.Size = new System.Drawing.Size(177, 51);
            this.AddProduct_btn.TabIndex = 41;
            this.AddProduct_btn.Text = "Add Product";
            this.AddProduct_btn.UseVisualStyleBackColor = false;
            // 
            // AddService_btn
            // 
            this.AddService_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(23)))), ((int)(((byte)(49)))));
            this.AddService_btn.FlatAppearance.BorderSize = 0;
            this.AddService_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddService_btn.ForeColor = System.Drawing.Color.White;
            this.AddService_btn.Location = new System.Drawing.Point(1129, 716);
            this.AddService_btn.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.AddService_btn.Name = "AddService_btn";
            this.AddService_btn.Size = new System.Drawing.Size(177, 46);
            this.AddService_btn.TabIndex = 46;
            this.AddService_btn.Text = "Add Service";
            this.AddService_btn.UseVisualStyleBackColor = false;
            // 
            // servicepricetextbox
            // 
            this.servicepricetextbox.Location = new System.Drawing.Point(1035, 679);
            this.servicepricetextbox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.servicepricetextbox.Name = "servicepricetextbox";
            this.servicepricetextbox.Size = new System.Drawing.Size(149, 27);
            this.servicepricetextbox.TabIndex = 63;
            // 
            // servicedescriptiontextbox
            // 
            this.servicedescriptiontextbox.Location = new System.Drawing.Point(1035, 642);
            this.servicedescriptiontextbox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.servicedescriptiontextbox.Name = "servicedescriptiontextbox";
            this.servicedescriptiontextbox.Size = new System.Drawing.Size(259, 27);
            this.servicedescriptiontextbox.TabIndex = 62;
            // 
            // servicenametextbox
            // 
            this.servicenametextbox.Location = new System.Drawing.Point(1035, 601);
            this.servicenametextbox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.servicenametextbox.Name = "servicenametextbox";
            this.servicenametextbox.Size = new System.Drawing.Size(259, 27);
            this.servicenametextbox.TabIndex = 61;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.label13.Location = new System.Drawing.Point(903, 686);
            this.label13.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 20);
            this.label13.TabIndex = 59;
            this.label13.Text = "Price";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.label12.Location = new System.Drawing.Point(903, 645);
            this.label12.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(105, 20);
            this.label12.TabIndex = 58;
            this.label12.Text = "Description";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.label11.Location = new System.Drawing.Point(903, 604);
            this.label11.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 20);
            this.label11.TabIndex = 57;
            this.label11.Text = "Name";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Rounded MT Bold", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(902, 557);
            this.label8.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(129, 23);
            this.label8.TabIndex = 56;
            this.label8.Text = "Add Service";
            // 
            // productsearch
            // 
            this.productsearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.productsearch.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productsearch.FormattingEnabled = true;
            this.productsearch.Location = new System.Drawing.Point(707, 188);
            this.productsearch.Name = "productsearch";
            this.productsearch.Size = new System.Drawing.Size(188, 23);
            this.productsearch.TabIndex = 65;
            // 
            // serviceSearch
            // 
            this.serviceSearch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serviceSearch.Font = new System.Drawing.Font("Arial Rounded MT Bold", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serviceSearch.FormattingEnabled = true;
            this.serviceSearch.Location = new System.Drawing.Point(637, 529);
            this.serviceSearch.Name = "serviceSearch";
            this.serviceSearch.Size = new System.Drawing.Size(188, 23);
            this.serviceSearch.TabIndex = 66;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(654, 188);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 20);
            this.label7.TabIndex = 67;
            this.label7.Text = "Find:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(581, 529);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(50, 20);
            this.label16.TabIndex = 68;
            this.label16.Text = "Find:";
            // 
            // number
            // 
            this.number.FillWeight = 50F;
            this.number.HeaderText = "#";
            this.number.Name = "number";
            this.number.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.number.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.number.Width = 50;
            // 
            // productID
            // 
            this.productID.DataPropertyName = "item_id";
            this.productID.HeaderText = "#";
            this.productID.Name = "productID";
            this.productID.ReadOnly = true;
            this.productID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.productID.Visible = false;
            this.productID.Width = 40;
            // 
            // productName
            // 
            this.productName.DataPropertyName = "name";
            this.productName.HeaderText = "Item";
            this.productName.Name = "productName";
            this.productName.ReadOnly = true;
            this.productName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // productDescription
            // 
            this.productDescription.DataPropertyName = "description";
            this.productDescription.HeaderText = "Description";
            this.productDescription.Name = "productDescription";
            this.productDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.productDescription.Width = 150;
            // 
            // Cost
            // 
            this.Cost.DataPropertyName = "cost";
            this.Cost.HeaderText = "Cost";
            this.Cost.Name = "Cost";
            this.Cost.ReadOnly = true;
            this.Cost.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Cost.Width = 70;
            // 
            // retailPrice
            // 
            this.retailPrice.DataPropertyName = "price";
            this.retailPrice.HeaderText = "Price";
            this.retailPrice.Name = "retailPrice";
            this.retailPrice.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.retailPrice.Width = 70;
            // 
            // productQuantity
            // 
            this.productQuantity.DataPropertyName = "quantity";
            this.productQuantity.HeaderText = "Quantity";
            this.productQuantity.Name = "productQuantity";
            this.productQuantity.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.productQuantity.Width = 80;
            // 
            // CategoryName
            // 
            this.CategoryName.DataPropertyName = "type";
            this.CategoryName.FillWeight = 75F;
            this.CategoryName.HeaderText = "Category";
            this.CategoryName.Name = "CategoryName";
            this.CategoryName.ReadOnly = true;
            this.CategoryName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CategoryName.Width = 90;
            // 
            // SupplierName
            // 
            this.SupplierName.DataPropertyName = "supplier";
            this.SupplierName.FillWeight = 75F;
            this.SupplierName.HeaderText = "Supplier";
            this.SupplierName.Name = "SupplierName";
            this.SupplierName.ReadOnly = true;
            this.SupplierName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SupplierName.Width = 110;
            // 
            // deleteproduct
            // 
            this.deleteproduct.FillWeight = 50F;
            this.deleteproduct.HeaderText = "Remove";
            this.deleteproduct.Name = "deleteproduct";
            this.deleteproduct.Text = "Delete";
            this.deleteproduct.UseColumnTextForButtonValue = true;
            this.deleteproduct.Width = 80;
            // 
            // number1
            // 
            this.number1.HeaderText = "#";
            this.number1.Name = "number1";
            this.number1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.number1.Width = 50;
            // 
            // itemID
            // 
            this.itemID.DataPropertyName = "item_id";
            this.itemID.HeaderText = "#";
            this.itemID.Name = "itemID";
            this.itemID.ReadOnly = true;
            this.itemID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.itemID.Visible = false;
            this.itemID.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "name";
            this.dataGridViewTextBoxColumn2.HeaderText = "Service";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 230;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn3.HeaderText = "Description";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 280;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "price";
            this.dataGridViewTextBoxColumn4.HeaderText = "Price";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn4.Width = 90;
            // 
            // deleteService
            // 
            this.deleteService.HeaderText = "Remove";
            this.deleteService.Name = "deleteService";
            this.deleteService.Text = "Delete";
            this.deleteService.UseColumnTextForButtonValue = true;
            this.deleteService.Width = 80;
            // 
            // InventoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1452, 896);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.serviceSearch);
            this.Controls.Add(this.productsearch);
            this.Controls.Add(this.servicepricetextbox);
            this.Controls.Add(this.servicedescriptiontextbox);
            this.Controls.Add(this.servicenametextbox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.AddService_btn);
            this.Controls.Add(this.AddProduct_btn);
            this.Controls.Add(this.supplier);
            this.Controls.Add(this.category);
            this.Controls.Add(this.productpricetextBox);
            this.Controls.Add(this.productdescriptiontextBox);
            this.Controls.Add(this.productnametextBox);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.servicesdgv);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Save_btn);
            this.Controls.Add(this.productsdgv);
            this.Font = new System.Drawing.Font("Arial Rounded MT Bold", 10.2F);
            this.Location = new System.Drawing.Point(0, 0);
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "InventoryForm";
            this.Text = "Maintain Inventory Form";
            this.Controls.SetChildIndex(this.productsdgv, 0);
            this.Controls.SetChildIndex(this.Save_btn, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.servicesdgv, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.productnametextBox, 0);
            this.Controls.SetChildIndex(this.productdescriptiontextBox, 0);
            this.Controls.SetChildIndex(this.productpricetextBox, 0);
            this.Controls.SetChildIndex(this.category, 0);
            this.Controls.SetChildIndex(this.supplier, 0);
            this.Controls.SetChildIndex(this.AddProduct_btn, 0);
            this.Controls.SetChildIndex(this.AddService_btn, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.servicenametextbox, 0);
            this.Controls.SetChildIndex(this.servicedescriptiontextbox, 0);
            this.Controls.SetChildIndex(this.servicepricetextbox, 0);
            this.Controls.SetChildIndex(this.productsearch, 0);
            this.Controls.SetChildIndex(this.serviceSearch, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            ((System.ComponentModel.ISupportInitialize)(this.productsdgv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.servicesdgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView productsdgv;
        private System.Windows.Forms.Button Save_btn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView servicesdgv;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox productnametextBox;
        private System.Windows.Forms.TextBox productdescriptiontextBox;
        private System.Windows.Forms.TextBox productpricetextBox;
        private System.Windows.Forms.ComboBox category;
        private System.Windows.Forms.ComboBox supplier;
        private System.Windows.Forms.Button AddProduct_btn;
        private System.Windows.Forms.Button AddService_btn;
        private System.Windows.Forms.TextBox servicepricetextbox;
        private System.Windows.Forms.TextBox servicedescriptiontextbox;
        private System.Windows.Forms.TextBox servicenametextbox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridViewTextBoxColumn ItemNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Itemname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Quantity;
        private System.Windows.Forms.ComboBox productsearch;
        private System.Windows.Forms.ComboBox serviceSearch;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn productID;
        private System.Windows.Forms.DataGridViewTextBoxColumn productName;
        private System.Windows.Forms.DataGridViewTextBoxColumn productDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cost;
        private System.Windows.Forms.DataGridViewTextBoxColumn retailPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn productQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn CategoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SupplierName;
        private System.Windows.Forms.DataGridViewButtonColumn deleteproduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn number1;
        private System.Windows.Forms.DataGridViewTextBoxColumn itemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewButtonColumn deleteService;
   
    }
}

