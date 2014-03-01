using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
namespace FAFOS
{
    class Address
    {
        public String getCity(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String address;
            con.Open();
            SqlCommand command = new SqlCommand("SELECT city_nm FROM City WHERE city_id = " + id, con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            address = reader[0].ToString();


            // d.Tables.Add(dt);
            con.Close();
            return address;
        }
        public String getProvince(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String address;
            con.Open();
            SqlCommand command = new SqlCommand("SELECT province_nm FROM Province WHERE province_id = " + id, con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            address = reader[0].ToString();


            // d.Tables.Add(dt);
            con.Close();
            return address;
        }
         public String getProvinceTax(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String tax;
            con.Open();
            SqlCommand command = new SqlCommand("SELECT tax FROM Province WHERE province_id = " + id, con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            tax = reader[0].ToString();
            con.Close();
            return tax;
        }
        public String getCountry(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String address;
            con.Open();
            SqlCommand command = new SqlCommand("SELECT country_nm FROM Country WHERE country_id = " + id, con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            address = reader[0].ToString();


            // d.Tables.Add(dt);
            con.Close();
            return address;
        }
    }
}
