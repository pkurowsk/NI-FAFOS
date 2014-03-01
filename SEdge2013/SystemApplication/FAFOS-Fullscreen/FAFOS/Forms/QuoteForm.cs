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
    public partial class QuoteForm : Background
    {
        public QuoteForm(QuoteController my_controller, String id, int type)
        {
            InitializeComponent();

            setup(id, "FAFOS Quote Form");

            this.dgvSalesOrder.CellEndEdit += new DataGridViewCellEventHandler(my_controller.dgvSalesOrder_CellValueChanged);
            this.dgvSalesOrder.CellValueChanged += new DataGridViewCellEventHandler(my_controller.dgvSalesOrder_CellValueChanged);
            this.btnPreview.Click += new System.EventHandler(my_controller.preview);
            if (type == 1)
            {
                this.btnSubmit.Click += new System.EventHandler(my_controller.createQuote);
            }
            if (type == 2)
            {
                setupEditForm();
                this.dgvSalesOrder.UserDeletedRow += new DataGridViewRowEventHandler(my_controller.DataGridView1_UserDeletedRow);
                this.btnPullQuote.Click += new System.EventHandler(my_controller.pullQuoteData);
                this.btnSubmit.Click += new System.EventHandler(my_controller.saveQuote);
            }
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

        public void fillQuoteIDs(DataTable ts)
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

        public DataGridView getQuoteItems()
        {
            return dgvSalesOrder;
        }

        public void setupEditForm()
        {
            lblQuoteID.Visible = true;
            txtGetID.Visible = true;
            btnPullQuote.Visible = true;
            btnSubmit.Text = "Save";
        }

        public void clearData()
        {
            dgvSalesOrder.RowCount = 1;
            dgvSalesOrder.Rows.Clear();
        }

        public void fillData(string serviceAddressId, DataTable quoteItems)
        {
            ddlServiceAddress.SelectedValue = serviceAddressId;
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
    }
}
