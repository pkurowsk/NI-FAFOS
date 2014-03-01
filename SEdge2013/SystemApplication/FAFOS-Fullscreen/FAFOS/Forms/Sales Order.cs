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
    public partial class Sales_Order : Background
    {
        public Sales_Order(SalesOrderController my_controller, String id, int type)
        {
            InitializeComponent();
            setup(id, "FAFOS Sales Order Form");

            //this.btnCancel.Click += new System.EventHandler(my_controller.cancelSalesOrder);
            this.dgvSalesOrder.CellEndEdit += new DataGridViewCellEventHandler(my_controller.dgvSalesOrder_CellValueChanged);
            this.dgvSalesOrder.CellValueChanged += new DataGridViewCellEventHandler(my_controller.dgvSalesOrder_CellValueChanged);
            this.btnPreview.Click += new System.EventHandler(my_controller.preview);
            if (type == 2)
            {
                setupQuoteForm();
                this.btnPullData.Click += new System.EventHandler(my_controller.pullQuoteData);
                this.btnSubmit.Click += new System.EventHandler(my_controller.createSalesOrder);
            }
            else if (type == 3)
            {
                setupEditSalesOrderForm();
                this.dgvSalesOrder.UserDeletedRow += new DataGridViewRowEventHandler(my_controller.DataGridView1_UserDeletedRow);
                this.btnPullData.Click += new System.EventHandler(my_controller.pullSalesOrder);
                this.btnSubmit.Click += new System.EventHandler(my_controller.saveSalesOrder);
            }
            else
                this.btnSubmit.Click += new System.EventHandler(my_controller.createSalesOrder);
        }

        public void fillItemList(DataTable ts)
        {
            item.DataSource = ts;
            item.DisplayMember = ts.Columns[0].ToString();
            item.ValueMember = ts.Columns[1].ToString();
        }

        public void fillClientList(DataTable ts)
        {
            ddlClient.DataSource = ts;
            ddlClient.DisplayMember = ts.Columns[0].ToString();
            ddlClient.ValueMember = ts.Columns[1].ToString();
        }

        public void fillServiceAddressList(DataTable ts)
        {
            ddlServiceAddress.DataSource = ts;
            ddlServiceAddress.DisplayMember = ts.Columns[0].ToString();
            ddlServiceAddress.ValueMember = ts.Columns[1].ToString();
        }

        public void fillSalesOrdersIDs(DataTable ts)
        {
            txtGetID.DataSource = ts;
            txtGetID.DisplayMember = ts.Columns[0].ToString();
            txtGetID.ValueMember = ts.Columns[0].ToString();
        }

        public string getServiceAddressId()
        {
            return ddlServiceAddress.SelectedValue.ToString();
        }

        public string getClientId()
        {
            return ddlClient.SelectedValue.ToString();
        }
        /*
        public string getStartDate()
        {
            return dtpStart.Value.ToString("yyyyMMdd HH:mm:ss"); ;
        }

        public string getEndDate()
        {
            return dtpEnd.Value.ToString("yyyyMMdd HH:mm:ss"); ;
        }*/

        public DataGridView getOrderItems()
        {
            return dgvSalesOrder;
        }

        public void setupQuoteForm()
        {
            lblGetID.Visible = true;
            txtGetID.Visible = true;
            btnPullData.Visible = true;
            lblGetID.Text = "Quote ID";
            btnPullData.Text = "Load Quote";
            btnSubmit.Text = "Create";
        }

        public void setupEditSalesOrderForm()
        {
            lblGetID.Visible = true;
            txtGetID.Visible = true;
            btnPullData.Visible = true;
            lblGetID.Text = "Sales Order ID";
            btnPullData.Text = "Load Order";
            btnSubmit.Text = "Save";
        }

        public void clearData()
        {
            dgvSalesOrder.RowCount = 1;
            dgvSalesOrder.Rows.Clear();
        }

        public void fillData(string serviceAddressId, bool completed, DataTable quoteItems)
        {
            ddlServiceAddress.SelectedValue = serviceAddressId;
            chkCompleted.Checked = completed;
            for (int i = 0; i < quoteItems.Rows.Count; i++)
            {
                int index = dgvSalesOrder.Rows.Add();
                dgvSalesOrder.Rows[index].Cells[0].Value = quoteItems.Rows[i][0];
                dgvSalesOrder.Rows[index].Cells[1].Value = quoteItems.Rows[i][1];
                dgvSalesOrder.Rows[index].Cells[3].Value = quoteItems.Rows[i][3];
                dgvSalesOrder.Rows[index].Cells[4].Value = quoteItems.Rows[i][2];
                dgvSalesOrder.Rows[index].Cells[5].Value = quoteItems.Rows[i][4];
            }
        }

        public string getId()
        {
            return txtGetID.Text;
        }

        public void setTotal(double total, double tax)
        {
            txtSubtotal.Text = "$" + total.ToString();
            txtHST.Text = "$" + (Math.Round(total * tax, 2)).ToString();
            txtTotal.Text = "$" + (Math.Round(total * (1 + tax), 2)).ToString();
        }

        public string getSubtotal()
        {
            return txtSubtotal.Text;
        }

        public string getHST()
        {
            return txtHST.Text;
        }

        public string getTotal()
        {
            return txtTotal.Text.Substring(1);
        }

        public bool getCompleted()
        {
            return chkCompleted.Checked;
        }
    }
}
