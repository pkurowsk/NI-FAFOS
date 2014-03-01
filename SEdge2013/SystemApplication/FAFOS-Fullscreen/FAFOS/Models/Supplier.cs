using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System;

namespace FAFOS
{
    class Supplier
    {
         String connString = FAFOS.Properties.Settings.Default.FAFOS;
        DataTable dt;

        public DataTable get()
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            dt = new DataTable();
            dt.Columns.Add("service_id");
            dt.Columns.Add("name");
            SqlCommand command = new SqlCommand("SELECT * FROM Supplier", con);
            //SqlDataAdapter da = new SqlDataAdapter(command);
            //da.Fill(dt);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dt.Rows.Add(new String[] { reader[0].ToString(), reader[1].ToString()});
            }
            con.Close();
            return dt;

        }
    }
}
