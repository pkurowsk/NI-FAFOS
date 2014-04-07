using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FAFOS
{
    public partial class SupplierForm : FAFOS.Background
    {
        private int userid;
        Users user;
        MaintainClientController my_controller;
        bool isEdit;
        public bool noChanges;
        ComboBox nameComboBox;
        
        

        public SupplierForm(MaintainClientController parent)
        {
            InitializeComponent();
            my_controller = parent;
           
            noChanges = true;

            #region Form Event Handlers

            this.txtSupplierSelect.SelectedValueChanged += new System.EventHandler(my_controller.Supplier_Changed);
            this.btnAddSupplier.Click += new System.EventHandler(my_controller.add_supplier_btn_click);
            #endregion

        here: try { SetSupplierBox(Supplier.GetList(), "0"); }
            catch (Exception)
            {
                if (MessageBox.Show("Error connecting to Database\nDo you want to retry?", "Connection Problems", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    goto here;
            }

            //User label
            user = new Users();
          
            setup(userid.ToString(), "FAFOS Supplier Form");
          
            

        }

        public String GetSupplierBox()
        {
            if (this.txtSupplierSelect.SelectedValue != null)
                return this.txtSupplierSelect.SelectedValue.ToString();
            else return null;
        }
        public void SetSupplierBox(DataTable supplier, String supplierID)
        {
            this.txtSupplierSelect.DataSource = supplier;
            this.txtSupplierSelect.DisplayMember = supplier.Columns[1].ToString();
            this.txtSupplierSelect.ValueMember = supplier.Columns[0].ToString();
            this.txtSupplierSelect.SelectedValue = "-1";
            this.txtSupplierSelect.SelectedValue = supplierID;
        }

        public String GetInput1()
        {
            //if (txtCreatSupplier.Text != null)
           // this.txtCreatSupplier.Text = "hey";
                return this.txtCreatSupplier.Text;
           // else
               // return "hey";
        }

               
    }
}