using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FAFOS
{
    class QuoteItems
    {
        public DataTable get(string id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            DataSet dt = new DataSet();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT quote_item_id, item_id, quantity, hours, price FROM Quote_Items WHERE quote_id = " + id, con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            adap.Fill(dt);

            con.Close();
            return dt.Tables[0];
        }

        public void set(String quoteItemsId, String quantity, String hours, String price, String itemId, String quoteId)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Quote_Items] (quote_item_id"
                + ", quantity, hours, price, item_id, quote_id) VALUES"
                + "(" + quoteItemsId + ", " + quantity + ", " + hours + ", " + price + ", " + itemId + ", " + quoteId + ") ", con);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ef)
            {
                MessageBox.Show("Could not save the following quote items.");
            }

            con.Close();
        }

        public bool setUpdate(String quoteItemsId, String quantity, String hours, String price, String itemId, String quoteId)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Quote_Items] (quote_item_id"
                + ", quantity, hours, price, item_id, quote_id) VALUES"
                + "(" + quoteItemsId + ", " + quantity + ", " + hours + ", " + price + ", " + itemId + ", " + quoteId + ") ", con);

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

            command = new SqlCommand("UPDATE [Quote_Items] SET "
                + "quantity = " + quantity + ", hours = " + hours + ", price = " + price + ", item_id = " + itemId
                + " WHERE quote_id = " + quoteId + " AND quote_item_id = " + quoteItemsId, con);
            command.ExecuteNonQuery();

            con.Close();
            return fail;
        }

        public void delete(String quoteItemsId, String quoteId)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand command = new SqlCommand("DELETE FROM [Quote_Items] "
                + "WHERE quote_item_id = " + quoteItemsId + " AND quote_id = " + quoteId, con);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ef)
            {
             //   MessageBox.Show(ef.ToString());
            }

            con.Close();
        }
    }
}
