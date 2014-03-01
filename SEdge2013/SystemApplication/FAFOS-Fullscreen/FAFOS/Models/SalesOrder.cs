using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace FAFOS
{
    class SalesOrder
    {
        public string set(String franchiseeUserId, String serviceAddress, String quoteId, String total, String tax, String completed)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            string id;
            con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO [Sales_Order] (total, tax_rate, "
                + "franchisee_user_id, quote_id, service_address_id, completed) VALUES"
                + "(" + total + ", " + tax + ", " + franchiseeUserId + ", " + quoteId + ", " + serviceAddress + ", " + completed + ") "
                + "SELECT SCOPE_IDENTITY()", con);

            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            id = reader[0].ToString();

            reader.Close();
            con.Close();

            return id;
        }
        public void update(String salesOrderId, String franchiseeUserId, String serviceAddress, String total, String tax, String completed)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand command = new SqlCommand("UPDATE [Sales_Order] SET total = " + total + ", "//tax_rate = NULL, "
                /*+ "franchisee_user_id = " + franchiseeUserId + "," */+ " service_address_id = " + serviceAddress// + ", date_start = '" + dateStart + "', date_end = '" + dateEnd
                + ", completed = " + completed
                + " WHERE sales_order_id = " + salesOrderId, con);
            command.ExecuteNonQuery();
            con.Close();
        }
        public void setDone(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
           
            con.Open();
            SqlCommand command = new SqlCommand("UPDATE [Sales_Order] SET completed = 1 WHERE sales_order_id = "+id, con);

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ef)
            {
                //MessageBox.Show(ef.ToString());
            }
            con.Close();

           
        }

        public DataTable get(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            DataTable d = new DataTable();
           // DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);
            
                con.Open();
               SqlCommand command = new SqlCommand("SELECT * FROM Sales_Order WHERE sales_order_id = "+id, con);
               SqlDataAdapter adap = new SqlDataAdapter (command);
            //SqlDataReader reader = command.ExecuteReader();
               try
               {
                   adap.Fill(d);
               }
            
            catch (Exception e)
            { }
              // d.Tables.Add(dt);
                con.Close();
                return d;
        }

        public String getSAddress(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String address="";
            con.Open();
            SqlCommand command = new SqlCommand("SELECT service_address_id FROM Sales_Order WHERE sales_order_id = " + id, con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                address = reader[0].ToString();

            }
            catch (Exception ef)
            {
                //MessageBox.Show(ef.ToString());
            }
            // d.Tables.Add(dt);
            con.Close();
            return address;
        }
        public bool getCompleted(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            bool completed = false;
            con.Open();
            SqlCommand command = new SqlCommand("SELECT completed FROM Sales_Order WHERE sales_order_id = " + id, con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                completed = ((int)reader[0]) == 1;

            }
            catch (Exception ef)
            {
                //MessageBox.Show(ef.ToString());
            }
            // d.Tables.Add(dt);
            con.Close();
            return completed;
        }
        public String getProvinceTax(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String tax = "";
            con.Open();
            SqlCommand command = new SqlCommand("SELECT tax_rate FROM Sales_Order WHERE sales_order_id = " + id, con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                tax = reader[0].ToString();

            }
            catch (Exception ef)
            {
                //MessageBox.Show(ef.ToString());
            }
            con.Close();
            return tax;
        }
        public DataTable getWorkOrders(int userid)
        {

             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            DataTable dataTable = new DataTable("Orders");

            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("dateIssued", typeof(String));
            dataTable.Columns.Add("address", typeof(String));
            dataTable.Columns.Add("city", typeof(String));
            dataTable.Columns.Add("province", typeof(String));
            dataTable.Columns.Add("country", typeof(String));

            con.Open();

            SqlCommand command = new SqlCommand("SELECT Sales_Order.sales_order_id, Sales_Order.date_issued, Service_Address.address,"+
                "Service_Address.city_id,Service_Address.province_id,Service_Address.country_id FROM Sales_Order, Service_Address "+
                "WHERE Sales_Order.service_address_id = Service_Address.service_address_id AND " +
                " Sales_Order.completed IS NULL AND Sales_Order.franchisee_user_id = " + userid +" ORDER BY Sales_Order.date_issued", con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dataTable.Rows.Add(new String[] { reader[0].ToString(), ((DateTime)reader[1]).ToShortDateString(), reader[2].ToString(), 
                    new Address().getCity(reader[3].ToString()), new Address().getProvince(reader[4].ToString()), 
                new Address().getCountry(reader[5].ToString())});
            }
            con.Close();
            return dataTable;
        }


        public DataSet getDone(String userid)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            DataSet dt = new DataSet();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand(" SELECT convert(varchar,Sales_Order.date_issued,107) + ', ' + Client.account_name + ', ' + Service_Address.address, "
 +" Sales_Order.sales_order_id AS varchar"
  +" FROM Service_Address,Sales_Order, Client  "
   +" where Service_Address.service_address_id = Sales_Order.service_address_id AND "
   +" Client.client_contract_id = Service_Address.client_contract_id AND"
   + " sales_order_id NOT IN (SELECT sales_order_id FROM Invoice) " +
                "AND Sales_Order.franchisee_user_id IN (SELECT user_id FROM [User] WHERE franchisee_id = " +
                "(SELECT franchisee_id FROM [User] WHERE user_id = " + userid + "))", con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            adap.Fill(dt);

            con.Close();
            return dt;
        }
        public DataTable getInProgress(String userid)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            DataTable d = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT Sales_Order.sales_order_id FROM Sales_Order WHERE NOT EXISTS "
                + "(SELECT sales_order_id FROM Invoice WHERE Sales_Order.sales_order_id = Invoice.sales_order_id)" +
                "AND franchisee_user_id IN (SELECT user_id FROM [User] WHERE franchisee_id = " +
                "(SELECT franchisee_id FROM [User] WHERE user_id = " + userid + "))", con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            try
            {
                adap.Fill(d);
            }
            catch (Exception e)
            { }
            con.Close();
            return d;
        }
    }
}
