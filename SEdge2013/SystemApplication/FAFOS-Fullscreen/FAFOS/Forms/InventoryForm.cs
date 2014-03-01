using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;


namespace FAFOS
{
    public partial class InventoryForm : FAFOS.Background
    {
        InventoryController my_controller;
        int userid;
        Users user;

        public InventoryForm(int id)
        {
            InitializeComponent();
            my_controller = new InventoryController();
            userid = id;
            
            this.Save_btn.Click += new System.EventHandler(my_controller.Save_btn_Click);
            this.AddProduct_btn.Click += new System.EventHandler(my_controller.addProduct_btn_Click);
            this.AddService_btn.Click += new EventHandler(my_controller.addService_btn_Click);
            this.Load += new System.EventHandler(my_controller.Load);
            this.productsdgv.ColumnHeaderMouseClick +=new DataGridViewCellMouseEventHandler(productsdgv_ColumnHeaderMouseClick);
            this.productsdgv.CellContentClick+=new DataGridViewCellEventHandler(my_controller.Delete_Product_btn_Click);
            this.servicesdgv.CellContentClick+=new DataGridViewCellEventHandler(my_controller.Delete_Service_btn_Click);
            this.productsdgv.AutoGenerateColumns = false;
            productsdgv.DataError += new DataGridViewDataErrorEventHandler(productsdgv_DataError);
            servicesdgv.DataError += new DataGridViewDataErrorEventHandler(servicesdgv_DataError);
            productsdgv.CellEndEdit += new DataGridViewCellEventHandler(productsdgv_CellEndEdit);
            servicesdgv.CellEndEdit += new DataGridViewCellEventHandler(servicesdgv_CellEndEdit);

            //User label
            user = new Users();
            userid = id;
            setup(userid.ToString(), "FAFOS Inventory");

        }

        public String getUser()
        {
            return userid.ToString();
        }

        /************DataGridViewFunctions*******************/
        public void SetProductsTable(DataTable dt)
        {
            productsdgv.DataSource = dt;
            productsdgv = setNumberColumn(productsdgv);
        }
        public void SetServicesTable(DataTable dt)
        {
            servicesdgv.DataSource = dt;
            servicesdgv = setNumberColumn(servicesdgv);
        }
        public DataGridView getProductdgv()
        {
            return productsdgv;
        }
        public DataGridView getServicesdgv()
        {
            return servicesdgv;
        }
        public DataGridView setNumberColumn(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                if (dgv.Rows[i].Visible == true)
                dgv.Rows[i].Cells[0].Value = i + 1;
            }
            return dgv;
        }
        public ComboBox getProductsComboBox()
        {
            return productsearch;
        }
        public void setProductsComboBox(ComboBox combo)
        {
            productsearch = combo;
        }
        public ComboBox getServicesComboBox()
        {
            return serviceSearch;
        }
        public void setServicesComboBox(ComboBox combo)
        {
            serviceSearch = combo;
        }
        public void productsdgv_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            return;
        }

        /***************DataGridViewInputCheck**************/
        void productsdgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string message;
            switch (e.ColumnIndex)
            {
                case 2:
                    // bound to integer field
                    message = "Invalid Entry. The value should be a valid number.";
                    e.Cancel = false;
                    break;
                    
                case 3:
                    // bound to date time field
                    message = "Invalid Entry";
                    e.Cancel = false;
                 
                    break;
                case 4:
                    // bound to date time field
                    message = "Invalid Entry";
                    e.Cancel = false;
                    break;
                case 5:
                    // bound to date time field
                    message = "Invalid Entry. The value should be a valid number";
                    e.Cancel = false;
                    break;
                case 6:
                    // bound to date time field
                    message = "Invalid Entry. The value should be a valid number";
                    e.Cancel = false;
                    break;
                case 7:
                    // bound to date time field
                    message = "Invalid Entry. The value should be a valid number";
                    e.Cancel = false;
                    break;
                case 8:
                    message = "Invalid Entry. The value should be a valid number";
                    e.Cancel = false;
                    break;
               
                // other columns
                default:
                    message = "Invalid data";
                    e.Cancel = true;
                    break;
            }

            MessageBox.Show(message);
            
        }
        void servicesdgv_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            string message;
            switch (e.ColumnIndex)
            {
                case 2:
                    // bound to integer field
                    message = "Invalid Entry. The value should be a valid number.";
                    e.Cancel = false;
                    break;
                case 3:
                    // bound to date time field
                    message = "Invalid Entry";
                    e.Cancel = false;
                    break;
                case 4:
                    // bound to date time field
                    message = "Invalid Entry";
                    e.Cancel = false;
                    break;
                case 5:
                    // bound to date time field
                    message = "Invalid Entry. The value should be a valid number";
                    e.Cancel = false;
                    break;
                // other columns
                default:
                    message = "Invalid data";
                    e.Cancel = true;
                    break;
            }

            MessageBox.Show(message);

        }
        private void productsdgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell currentCell = productsdgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (e.ColumnIndex == 6 || e.ColumnIndex == 5)
            {
                try
                {
                    int cellValue = Convert.ToInt32(currentCell.Value);
                    if (cellValue < 0)
                    {
                        currentCell.Value = "0";
                        if (e.ColumnIndex == 6) MessageBox.Show("Please enter a valid quantity");
                        else if (e.ColumnIndex == 5) MessageBox.Show("Please enter a valid price");
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("a");
                    return;
                }
            }
        }
        private void servicesdgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell currentCell = servicesdgv.Rows[e.RowIndex].Cells[e.ColumnIndex];

            if (e.ColumnIndex == 5)
            {
                try
                {
                    int cellValue = Convert.ToInt32(currentCell.Value);
                    if (cellValue < 0)
                    {
                        currentCell.Value = "0";
                        if (e.ColumnIndex == 5) MessageBox.Show ("Please enter a valid price");
                       
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please enter a valid value");
                    return;
                }
                catch (InvalidCastException)
                {
                  //  MessageBox.Show("Please enter a valid value");
                   // return;
                }
            }
        }

        /***********AddProductFunctions*************/
        public String getProductName()
        {
            return productnametextBox.Text;
        }
        public String getProductDescription()
        {
            return productdescriptiontextBox.Text;
        }
        public String getProductPrice()
        {
            return productpricetextBox.Text;
        }
        public ComboBox getCategory()
        {
            return category;
        }
        public void setCategory(ComboBox combo)
        {
            category = combo;
        }
        public ComboBox getSupplier()
        {
            return supplier;
        }
        public void setSupplier(ComboBox combo)
        {
            supplier = combo;
        }

        /*************AddServiceFunctions***********/
        public String getServiceName()
        {
            return servicenametextbox.Text;
        }
        public String getServiceDescription()
        {
            return servicedescriptiontextbox.Text;
        }
        public String getServicePrice()
        {
            return servicepricetextbox.Text;
        }
       
     

       
     


      
  

  



       

      

     


   
      

    
    

     
 

      

     


     
       

 
    }
}
