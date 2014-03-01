using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace FAFOS
{
    class InventoryController
    {
        private static InventoryForm _view;
        private static PurchaseRecord _purchaseRecord;
        private InventoryTransaction inventory_transaction;
        private ItemTransaction item_transaction;
        private Inventory franchisee_inventory;
        private Category category;
        private Supplier supplier;
        DataTable servicesTable;
        DataTable productsTable;
        DataTable supplierTable;
        Users user;

        public InventoryController()
        {
            franchisee_inventory = new Inventory();
            //inventory_transaction = new InventoryTransaction();
            category = new Category();
            supplier = new Supplier();
            user = new Users();
        }
        /************************************Inventory************************************************************/
        public void Load(object sender, EventArgs e)
        {
            _view = (InventoryForm)((InventoryForm)sender).FindForm();
            servicesTable = franchisee_inventory.getServices(user.getFranchiseeId(_view.getUser()));
            productsTable = franchisee_inventory.getProducts(user.getFranchiseeId(_view.getUser()));
            DataTable categoryTable = category.get();
            DataTable supplierTable = supplier.get();
            _view.getCategory().DataSource = categoryTable;
            _view.getCategory().DisplayMember = "type";
            _view.getCategory().ValueMember = "id";
            _view.getSupplier().DataSource = supplierTable;
            _view.getSupplier().DisplayMember = "name";
            _view.getSupplier().ValueMember = "service_id";

            fillProductcombo();
            fillServicecombo();
            try
            {
                _view.SetProductsTable(productsTable);
                _view.SetServicesTable(servicesTable);
            }
            catch (InvalidCastException ef)
            {
                MessageBox.Show("Incorrect data was loaded in this form.");
            }

        }
        public void Save_btn_Click(object sender, EventArgs e)
        {
            franchisee_inventory.updateServices();
            franchisee_inventory.updateProducts();
            return;
        }
        public void Delete_Product_btn_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView = _view.getProductdgv();
            if (dataGridView.CurrentCell.ColumnIndex == 9)
            {
                if (MessageBox.Show("Do you want to delete this row ?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        dataGridView.Rows.RemoveAt(dataGridView.SelectedCells[0].OwningRow.Index);
                        franchisee_inventory.deleteProduct(dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells[1].Value.ToString());
                        fillProductcombo();
                    }
                    catch
                    {
                        MessageBox.Show("No row can be deleted");
                    }
                }
            }
        }
        public void Delete_Service_btn_Click(object sender, EventArgs e)
        {
            DataGridView dataGridView = _view.getServicesdgv();
            if (dataGridView.CurrentCell.ColumnIndex == 1)
            {
                if (MessageBox.Show("Do you want to delete this row ?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        dataGridView.Rows.RemoveAt(dataGridView.SelectedCells[0].OwningRow.Index);
                        franchisee_inventory.deleteService(dataGridView.Rows[dataGridView.CurrentCell.RowIndex].Cells[2].Value.ToString());
                        fillServicecombo();
                    }
                    catch
                    {
                        MessageBox.Show("No row can be deleted");
                    }
                }
            }
        }
        public void addProduct_btn_Click(object sender, EventArgs e)
        {
            _view = (InventoryForm)((Button)sender).FindForm();
            //_view.getCategory().ValueMember = "category_id";
            String category_id = _view.getCategory().SelectedValue.ToString();
           // _view.getCategory().DisplayMember = "type";
          //  _view.getSupplier().ValueMember = "supplier_id";
            String supplier_id = _view.getSupplier().SelectedValue.ToString();
            //_view.getSupplier().DisplayMember = "name";
            franchisee_inventory.insertProduct(user.getFranchiseeId(_view.getUser()), _view.getProductName(), _view.getProductDescription(), _view.getProductPrice(), category_id, supplier_id);
            productsTable = franchisee_inventory.getProducts(user.getFranchiseeId(_view.getUser()));
            _view.SetProductsTable(productsTable);
            fillProductcombo();
        }
        public void addService_btn_Click(object sender, EventArgs e)
        {
            _view = (InventoryForm)((Button)sender).FindForm();
            franchisee_inventory.insertService(user.getFranchiseeId(_view.getUser()), _view.getServiceName(), _view.getServiceDescription(), _view.getServicePrice());
            servicesTable = franchisee_inventory.getServices(user.getFranchiseeId(_view.getUser()));
            _view.SetServicesTable(servicesTable);
            fillServicecombo();
        }
        public void fillProductcombo()
        {
            _view.getProductsComboBox().DataSource = productsTable;
            _view.getProductsComboBox().DisplayMember = "name";
        }
        public void fillServicecombo()
        {
            _view.getServicesComboBox().DataSource = servicesTable;
            _view.getServicesComboBox().DisplayMember = "name";
        }
        /************************************PurchaseRecords*********************************************************/
        public void purchaseLoad(object sender, EventArgs e)
        {
            _purchaseRecord = (PurchaseRecord)((PurchaseRecord)sender).FindForm();
            supplierTable = supplier.get();
            _purchaseRecord.getSupplier().DataSource = supplierTable;
            _purchaseRecord.getSupplier().ValueMember = "service_id";
            _purchaseRecord.getSupplier().DisplayMember = "name";
            String supplier_id = "1";
         //   MessageBox.Show(supplier_id);
            
           DataTable itemTable = franchisee_inventory.getProducts(user.getFranchiseeId(_purchaseRecord.getUser()), supplier_id);
            _purchaseRecord.fillItemList(itemTable);
        }
        public void fillItemList(object sender, EventArgs e)
        {
            _purchaseRecord.getPurchaseRecords().Rows.Clear();
            _purchaseRecord.getTotal().Text = "$0";
            _purchaseRecord.getSupplier().DisplayMember = "name";
            _purchaseRecord.getSupplier().ValueMember = "service_id";

            String supplier_id =  _purchaseRecord.getSupplier().SelectedValue.ToString();
          //  MessageBox.Show(supplier_id);
            DataTable itemTable = franchisee_inventory.getProducts(user.getFranchiseeId(_purchaseRecord.getUser()), supplier_id);
            _purchaseRecord.fillItemList(itemTable);
        }
        public void PurchaseRecords_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = _purchaseRecord.getPurchaseRecords();
            if (e.ColumnIndex == 1 && e.RowIndex != -1)
            {
                dgv.Rows[e.RowIndex].Cells[2].Value = franchisee_inventory.getDescription(dgv.Rows[e.RowIndex].Cells[1].Value.ToString());
                dgv = _purchaseRecord.setNumberColumn(dgv);
            }
            if (e.ColumnIndex == 4 || e.ColumnIndex == 3 && e.RowIndex != -1)
            {
                DataGridViewCell currentCell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
                try
                {
                    int cellValue = Convert.ToInt32(currentCell.Value);
                    if (cellValue < 0)
                    {
                        currentCell.Value = "0";
                        if (e.ColumnIndex == 3)
                            MessageBox.Show("Please enter a valid cost");
                        else if (e.ColumnIndex == 4)
                            MessageBox.Show("Please enter a valid quantity");
                    }
                }
                catch (FormatException)
                {
                    currentCell.Value = "0";
                    MessageBox.Show("Please enter a valid number");
                    return;
                }
            }
        }
        public void purchaseRecord_btn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to save this purchase record?", "Save", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                inventory_transaction = new InventoryTransaction();
                item_transaction = new ItemTransaction();
                _purchaseRecord = (PurchaseRecord)((Button)sender).FindForm();
                String supplier_id = _purchaseRecord.getSupplier().SelectedValue.ToString();
                inventory_transaction.insertInventoryTransaction(user.getFranchiseeId(_purchaseRecord.getUser()), supplier_id, _purchaseRecord.getDate());
                DataGridView dgv = _purchaseRecord.getPurchaseRecords();
                for (int i = 0; i < dgv.Rows.Count - 1; i++)
                {
                    franchisee_inventory.updateQuantity(dgv.Rows[i].Cells[1].Value.ToString(), dgv.Rows[i].Cells[4].Value.ToString());
                    franchisee_inventory.updateCost(dgv.Rows[i].Cells[1].Value.ToString(), dgv.Rows[i].Cells[3].Value.ToString());
                    item_transaction.insertItemTransaction(inventory_transaction.getTransactions(user.getFranchiseeId(_purchaseRecord.getUser())).Rows.Count.ToString(), dgv.Rows[i].Cells[1].Value.ToString(), dgv.Rows[i].Cells[3].Value.ToString(), dgv.Rows[i].Cells[4].Value.ToString());
                }
                _purchaseRecord.getPurchaseRecords().Rows.Clear();
                _purchaseRecord.getTotal().Text = "$0";
            }
        }
    }
}
