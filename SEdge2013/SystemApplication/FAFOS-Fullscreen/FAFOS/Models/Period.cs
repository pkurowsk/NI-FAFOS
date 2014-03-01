using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace FAFOS
{
    class Period
    {
        public double getPerYear(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT period_per_year FROM Time_Periods WHERE period_nm = '" + id+"'", con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            double name = Convert.ToDouble(reader[0]);
            reader.Close();
            con.Close();
            return name;
        }

        public String getName(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT period_nm FROM Time_Periods WHERE period_id = " + id, con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            String name = reader[0].ToString();
            reader.Close();
            con.Close();
            return name;
        }
    }
}
