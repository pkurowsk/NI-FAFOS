using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace FAFOS
{
    class OrderItems
    {
        public void set(String orderItemsId, String quantity, String hours, String price, String itemId, String salesOrderId)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Order_Items] (order_items_id"
                + ", quantity, hours, price, item_id, sales_order_id) VALUES"
                + "(" + orderItemsId + ", " + quantity + ", " + hours + ", " + price + ", " + itemId + ", " + salesOrderId + ") ", con);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ef)
            {
                MessageBox.Show("Could not save the sales order items. Please try again later.");
            }

            con.Close();
        }

        public bool setUpdate(String orderItemsId, String quantity, String hours, String price, String itemId, String salesOrderId)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Order_Items] (order_items_id"
                + ", quantity, hours, price, item_id, sales_order_id) VALUES"
                + "(" + orderItemsId + ", " + quantity + ", " + hours + ", " + price + ", " + itemId + ", " + salesOrderId + ") ", con);

            bool fail = false;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ef)
            {
                fail = true;
                //MessageBox.Show(ef.ToString());
            }

            command = new SqlCommand("UPDATE [Order_Items] SET "
                + "quantity = " + quantity + ", hours = " + hours + ", price = " + price + ", item_id = " + itemId
                + " WHERE sales_order_id = " + salesOrderId + " AND order_items_id = " + orderItemsId, con);
            command.ExecuteNonQuery();

            con.Close();
            return fail;
        }

        public void delete(String orderItemsId, String salesOrderId)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [Order_Items] "
                + "WHERE order_items_id = " + orderItemsId + " AND sales_order_id = " + salesOrderId, con);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ef)
            {
              //  MessageBox.Show(ef.ToString());
            }

            con.Close();
        }
        public DataTable get2(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            DataSet d = new DataSet();
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT order_items_id,quantity,hours,price,item_id FROM Order_Items WHERE sales_order_id = " + id, con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            try
            {
                adap.Fill(dt);
            }

            catch (Exception e)
            { }
            //d.Tables.Add(dt);
            con.Close();
            return dt;
        }
        public DataTable get(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            DataSet d = new DataSet();
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT order_items_id,item_id,quantity,hours,price FROM Order_Items WHERE sales_order_id = " + id, con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            try
            {
                adap.Fill(dt);
            }
            
            catch (Exception e)
            { }
            //d.Tables.Add(dt);
            con.Close();
            return dt;
        }

    }
}
