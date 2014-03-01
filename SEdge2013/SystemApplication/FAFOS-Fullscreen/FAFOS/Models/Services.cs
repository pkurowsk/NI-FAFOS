using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace FAFOS
{
    class Services
    {
        public String getName(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT service_nm FROM Service WHERE service_id = " + id, con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            String name = reader[0].ToString();
            reader.Close();
            con.Close();
            return name;
        }
    }
}
