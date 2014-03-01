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
    class Category
    {
         String connString = FAFOS.Properties.Settings.Default.FAFOS;
      

        public DataTable get()
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("type");
            SqlCommand command = new SqlCommand("SELECT * FROM Category WHERE category_id !=2", con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dt.Rows.Add(new String[] { reader[0].ToString(), reader[1].ToString() });
            }
            con.Close();
            return dt;
        }

       


    }
}
