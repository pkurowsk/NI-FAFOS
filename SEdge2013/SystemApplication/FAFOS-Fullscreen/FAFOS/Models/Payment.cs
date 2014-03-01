using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
namespace FAFOS
{
    class Payment
    {
        public int set(String arg)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            int id = 1;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT MAX(payment_id) FROM Payment", con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader[0] != DBNull.Value)
                {
                    id = Convert.ToInt32(reader[0].ToString()) + 1;
                }
            }
            con.Close();

            con.Open();
            command = new SqlCommand("INSERT INTO Payment VALUES (" + id + "," + arg + ")", con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ef)
            {
                MessageBox.Show("Could not save the following payment.");
            }
            con.Close();

            return id;
        }

        public void setIP(String arg)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);

            SqlCommand command;

            con.Open();
            command = new SqlCommand("INSERT INTO Invoice_Payment VALUES (" + arg + ")", con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ef)
            {
               // MessageBox.Show(ef.ToString());
            }
            con.Close();
        }

        public DataTable getNotPaid(int userid)
        {

             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            DataTable dataTable = new DataTable("Payments");

            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("Date Issued", typeof(String));
            dataTable.Columns.Add("Date Due", typeof(String));
            dataTable.Columns.Add("Total", typeof(String));
            dataTable.Rows.Add(new String[] { "", "", "", "" });
            dataTable.Rows.Add(new String[] {"ID","Date Issued","Due", "Total"});

            con.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Invoice WHERE payed='no' AND franchisee_user_id = "+userid, con);
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
                dataTable.Rows.Add(new String[] { reader[0].ToString(), reader[1].ToString(), Convert.ToDateTime(reader[1]).AddDays(Convert.ToDouble(reader[2])).ToString(),reader[3].ToString()});
            }
            con.Close();
            return dataTable;
        }
        public DataTable getAmount(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            DataTable dataTable = new DataTable("Payments");

            dataTable.Columns.Add("Date", typeof(String));
            dataTable.Columns.Add("Type", typeof(String));
            dataTable.Columns.Add("Amount", typeof(String));
            dataTable.Columns.Add("Remarks", typeof(String));
            //dataTable.Rows.Add(new String[] { "", "", "", "" });
         //   dataTable.Rows.Add(new String[] {"ID","Date Issued","Due", "Total"});

            con.Open();

            SqlCommand command = new SqlCommand("SELECT date, type, amount, remarks FROM Payment WHERE payment_id IN (SELECT payment_id FROM Invoice_Payment WHERE invoice_id = "+id+" )", con);
            try
            {

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    dataTable.Rows.Add(new String[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString() });
                }
                con.Close();
            }
            catch (SqlException e)
            {
            //    MessageBox.Show(e.ToString());
            }
            return dataTable;
        }
    }
}
