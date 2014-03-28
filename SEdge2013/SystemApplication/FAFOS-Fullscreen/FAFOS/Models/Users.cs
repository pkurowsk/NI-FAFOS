using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace FAFOS
{
    class Users
    {

        public string get(String user)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            string result="";
            SqlCommand command = new SqlCommand("SELECT * FROM [User] WHERE user_id = " + user, con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

             result = reader[1].ToString() + "," + reader[2].ToString() + "," +
              reader[8].ToString() + "," +
              reader[11].ToString();


            }
            catch (Exception e)
            {

            }
            // d.Tables.Add(dt);
            con.Close();
            return result;
        }
        public string getFranchiseeId(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT franchisee_id FROM [User] WHERE user_id = " + id, con);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            String franchiseeId = reader[0].ToString();
            reader.Close();
            con.Close();
            return franchiseeId;
        }
        public bool check(String user, String password )
        {
           //  String connString = FAFOS.Properties.Settings.Default.FAFOS;
            String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [User] WHERE username = '" + user + "'", con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                if (reader[4].ToString() == user && reader[5].ToString() == password)
                    return true;


            }
            catch (Exception e)
            {
                MessageBox.Show("The inputted username does not currently exist.");
            }
            // d.Tables.Add(dt);
            con.Close();
            return false;
        }

        public bool checkAdmin(int userid)
        {
            // String connString = FAFOS.Properties.Settings.Default.FAFOS;
            String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand command = new SqlCommand("SELECT hq_user FROM [User] WHERE user_id = " + userid, con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                if (reader[0].ToString() == "True")
                    return true;


            }
            catch (Exception e)
            {

            }
            // d.Tables.Add(dt);
            con.Close();
            return false;
        }

         public int getId(String username)
        {
           //  String connString = FAFOS.Properties.Settings.Default.FAFOS;
            String connString = FAFOS.Properties.Settings.Default.FAFOS;
             SqlConnection con = new SqlConnection(connString);
            con.Open();
            int id=0;
            SqlCommand command = new SqlCommand("SELECT * FROM [User] WHERE username = '" + username + "'", con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                id = Convert.ToInt32(reader[0]);
                

            }
            catch (Exception e)
            {

            }
            // d.Tables.Add(dt);
            con.Close();
            return id;
        }
    
        public String getName(int id)
        {
           //  String connString = FAFOS.Properties.Settings.Default.FAFOS;
            String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            String name = "";
            SqlCommand command = new SqlCommand("SELECT * FROM [User] WHERE user_id = " + id, con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                 name = reader[1].ToString() + " " + reader[2].ToString();
                

            }
            catch (Exception e)
            {

            }
            // d.Tables.Add(dt);
            con.Close();
            return name;
        }
    }
}
