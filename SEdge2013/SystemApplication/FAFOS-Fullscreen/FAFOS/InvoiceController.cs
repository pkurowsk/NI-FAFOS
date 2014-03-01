using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Reflection;

namespace FAFOS
{
    class InvoiceController
    {
        
        private static InvoiceForm _view;
        private SalesOrder sales_order;
        private OrderItems order_items;
        private Item item;
        private ServiceAddress SAddress;
        private Client client;
        private ClientContract contract;
        private Franchisee franchisee;
        private Inventory inventory;
        
        public InvoiceController()
        {
            sales_order = new SalesOrder();
            order_items = new OrderItems();
            item = new Item();
            SAddress = new ServiceAddress();
            client = new Client();
            contract = new ClientContract();
            franchisee = new Franchisee();
            inventory = new Inventory();
        }

        public void Find_btn_Click(object sender, EventArgs e)
        {
            _view = (InvoiceForm)((Button)sender).FindForm();
            try
            {
                
            DataTable dt1 = order_items.get2(_view.GetText());

            DataTable dt2 = new DataTable();
            dt2.Columns.Add("item_id", typeof(int));
            dt2.Columns.Add("name", typeof(String));
            dt2.Columns.Add("description", typeof(String));

            int rows = dt1.Rows.Count;
            for (int i = 1; i <= rows; i++)
            {
                DataRow row = dt2.NewRow();
                row["item_id"] = (int)item.getRow(dt1.Rows[i - 1].ItemArray[4].ToString()).ItemArray[0];
                row["name"] = item.getRow(dt1.Rows[i - 1].ItemArray[4].ToString()).ItemArray[1].ToString();
                row["description"] = item.getRow(dt1.Rows[i - 1].ItemArray[4].ToString()).ItemArray[2].ToString();
                dt2.Rows.Add(row);

            }

            
                var results = from table1 in dt1.AsEnumerable()
                              join table2 in dt2.AsEnumerable() on (int)table1["item_id"] equals (int)table2["item_id"]
                              select new
                              {
                                  OrderItemId = table1["order_items_id"].ToString(),
                                  name = table2["name"].ToString(),
                                  description = table2["description"].ToString(),
                                  hours = table1["hours"]!=null?  table1["hours"].ToString():"",
                                  quantity = table1["quantity"] != null ? table1["quantity"].ToString() : "",
                                  
                                  price = String.Format("{0:0.00}", Math.Round((double)table1["price"],2)),
                                  total = String.Format("{0:0.00}", table1["quantity"] != DBNull.Value ?
                                  Math.Round((double)table1["price"] * Convert.ToInt32(table1["quantity"].ToString())) :
                                  Math.Round((double)table1["price"] * Convert.ToInt32(table1["hours"].ToString())), 2)
                                  /*
                                   table1["quantity"]!=null?  
                                  Math.Round((double)table1["price"] * (int)table1["quantity"]):
                                  Math.Round((double)table1["price"] * (int)table1["hours"])*/
                              };

                DataTable dt = LINQToDataTable(results);
                _view.SetTable(dt);
                
           

            double total;
            double tax;
            DataTable dtSales = sales_order.get(_view.GetText());
            total = Convert.ToDouble(String.Format("{0:0.00}", Math.Round((double)dtSales.Rows[0].ItemArray[2],2)));
            tax =  (double)dtSales.Rows[0].ItemArray[3];
            _view.SetTotal(total,tax);
            }
            catch (Exception ef)
            {
                MessageBox.Show("Incorrect data has been loaded, contact Database Administrator");
            }
        }

        public void Send_btn_Click(object sender, EventArgs e)
        {
            _view = (InvoiceForm)((Button)sender).FindForm();

            try
            {
                DataTable dtSales = sales_order.get(_view.GetText());
                double tax = (double)dtSales.Rows[0].ItemArray[3];
                String total = _view.GetTotalText().Replace("$", "");
                String paymentString = _view.GetPaymentText().Replace("$", "");
                if (paymentString == "")
                    paymentString = "0";
                Invoice inv = new Invoice();


                //Update Inventory
                if (!inventory.set(_view.GetText(),_view.getUserId()))
                    _view.Close();
                else
                {

                    //Add Invoice
                    int invId = 0;
                    if (Convert.ToDouble(total) > Convert.ToDouble(paymentString))
                        invId = inv.set("'" + _view.GetIssuedText() + "','" + _view.GetTermText() + "'," +
                            total + "," + tax + "," + _view.getUserId() + "," + _view.GetText() + ",'no'");
                    else
                        invId = inv.set("'" + _view.GetIssuedText() + "','" + _view.GetTermText() + "'," +
                            total + "," + tax + "," + _view.getUserId() + "," + _view.GetText() + ",'yes'");

                    //Add Payment
                    if (_view.GetPaymentText() != "0" && _view.GetPaymentText() != "" && _view.GetPaymentText() != "$0.00")
                    {
                        Payment pay = new Payment();
                        int payId;

                        payId = pay.set("'" + _view.GetIssuedText() + "','" + _view.GetTypeText() + "'," +
                                    _view.GetPaymentText() + ",'" + _view.GetRemarksText() + "'," + contract.getClient(sales_order.getSAddress(_view.GetText())));

                        pay.setIP(invId + "," + payId);

                    }



                    MessageBox.Show("Invoice has been saved.");
                    _view.Close();
                }

            }
            catch (Exception ed)
            { MessageBox.Show("The invoice could not be saved. Please try again later."); }
        }

        public void Preview_btn_Click(object sender, EventArgs e)
        {
            _view = (InvoiceForm)((Button)sender).FindForm();
            _view.Preview(SAddress.get(sales_order.getSAddress(_view.GetText())),
                client.get(contract.getClient(sales_order.getSAddress(_view.GetText()))),
                franchisee.get(contract.getFranchisee(sales_order.getSAddress(_view.GetText()))));
        }
        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
            DataTable dtReturn = new DataTable();

            // column names 
            PropertyInfo[] oProps = null;

            if (varlist == null) return dtReturn;

            foreach (T rec in varlist)
            {
                // Use reflection to get property names, to create table, Only first time, others will follow 
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType;

                        if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                        == typeof(Nullable<>)))
                        {
                            colType = colType.GetGenericArguments()[0];
                        }

                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }

                DataRow dr = dtReturn.NewRow();

                foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue
                    (rec, null);
                }

                dtReturn.Rows.Add(dr);
            }
            return dtReturn;
        }

    }
}
