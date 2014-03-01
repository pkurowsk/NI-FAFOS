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
    class Franchisee
    {
        public String get(String id)
        {
            Address ad = new Address();
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String franchisee="";
            con.Open();
            SqlCommand command = new SqlCommand("SELECT b.*, f.company_name FROM Franchisee_Business_Address b,Franchisee f WHERE b.franchisee_id = f.franchisee_id AND f.franchisee_id = " + id, con);
            try{
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            franchisee = reader[1].ToString() + "," + reader[2].ToString() + "," +
             ad.getCity(reader[4].ToString()) + "," + ad.getProvince(reader[3].ToString()) + "," + ad.getCountry(reader[5].ToString())
             + "," + reader[7].ToString();

            }
            catch (Exception e)
            { }
            // d.Tables.Add(dt);
            con.Close();
            return franchisee;
        }
        public int[] getTotal()
        {
          
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            int total=0;
            con.Open();
            SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Franchisee", con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();

                reader.Read();
                    total = Convert.ToInt32(reader[0]);

            }
            catch (Exception e)
            { }
            // d.Tables.Add(dt);
            con.Close();
            int[] ids = new int[total];
            con.Open();
            command = new SqlCommand("SELECT franchisee_id FROM Franchisee", con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    ids[i] = Convert.ToInt32(reader[0]);
                    i++;
                }

            }
            catch (Exception e)
            { }
            // d.Tables.Add(dt);
            con.Close();
            return ids;
        }
        public DataTable getAll()
        {

             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            
            DataTable dt = new DataTable("Franchisee");
            dt.Columns.Add("ID");
            dt.Columns.Add("name");
            con.Open();
            SqlCommand command = new SqlCommand("SELECT franchisee_id, company_name FROM Franchisee", con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    dt.Rows.Add(new String[] { reader[0].ToString(), reader[1].ToString() });
                }

            }
            catch (Exception e)
            { }
            // d.Tables.Add(dt);
            con.Close();
            return dt;
        }
        public String [] getAddress (int id)
        {
            Address ad = new Address();
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String []franchisee = new String [3];
            con.Open();
            SqlCommand command = new SqlCommand("SELECT Franchisee_Business_Address.* FROM Franchisee_Business_Address,[User] WHERE Franchisee_Business_Address.franchisee_id = [User].franchisee_id AND [User].user_id = " + id, con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                franchisee[0] = reader[1].ToString();
                franchisee[1] = ad.getCity(reader[4].ToString()) ;
                franchisee[2] = ad.getCountry(reader[5].ToString());
            }
            catch (Exception e)
            { //MessageBox.Show(e.ToString()); 
            }
            // d.Tables.Add(dt);
            con.Close();
          //  MessageBox.Show(franchisee[0]);
            return franchisee;
        }
    }
}
