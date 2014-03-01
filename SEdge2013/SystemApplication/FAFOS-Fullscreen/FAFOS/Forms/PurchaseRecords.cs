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
    public partial class PurchaseRecord: FAFOS.Background
    {
        InventoryController my_controller;
        Users user;
        int userid;
        
        public PurchaseRecord(int id)
        {
            InitializeComponent();
            my_controller = new InventoryController();

            this.Load += new EventHandler(my_controller.purchaseLoad);
            this.comboSupplier.SelectedIndexChanged +=new EventHandler(my_controller.fillItemList);
            this.purchaseRecordsdgv.CellValueChanged +=new DataGridViewCellEventHandler(my_controller.PurchaseRecords_CellValueChanged);
            this.purchaseRecordsdgv.CellValidated += new DataGridViewCellEventHandler(PurchaseRecords_CellValidated);
            this.SavePurchase_btn.Click +=new EventHandler(my_controller.purchaseRecord_btn_Click);
          
            //User label
            user = new Users();
            userid = id;
           // lblUserInfo.Text = "Logged in:\n " + user.getName(Convert.ToInt32(userid));
            setup(userid.ToString(), "FAFOS Purchase Records");

         
        }
     
        public String getUser()
        {
            return userid.ToString();
        }
        public ComboBox getSupplier()
        {
            return comboSupplier;
        }
        public void fillItemList(DataTable dt)
        {
            item.DataSource = dt;
            item.DisplayMember = dt.Columns[1].ToString();
            item.ValueMember = dt.Columns[0].ToString();
        }
        public string getDate()
        {
            return date.Value.ToString();
        }
        public DataGridView getPurchaseRecords()
        {
            return purchaseRecordsdgv;
        }
        public DataGridView setNumberColumn(DataGridView dgv)
        {
            for (int i = 0; i < dgv.Rows.Count-1; i++)
            {
                dgv.Rows[i].Cells[0].Value = i + 1;
            }
            return dgv;
        }
        private void PurchaseRecords_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                try
                {
                    DataGridViewRow row = purchaseRecordsdgv.Rows[e.RowIndex];
                    String valueA = row.Cells[3].Value.ToString();
                    String valueB = row.Cells[4].Value.ToString();
                    int result;
                    int total = 0;
                    if (Int32.TryParse(valueA, out result) && Int32.TryParse(valueB, out result))
                    {
                        row.Cells[5].Value = Convert.ToInt32(valueA) * Convert.ToInt32(valueB);
                    }
                    for (int i = 0; i < purchaseRecordsdgv.Rows.Count - 1; i++)
                    {
                        total += Convert.ToInt32(purchaseRecordsdgv.Rows[i].Cells[5].Value);
                    }
                    txtTotal.Text = "$"+total.ToString();
                }
                catch
                {
                }
            }
        }
        public TextBox getTotal()
        {
            return txtTotal;
        }
       

    }
}
