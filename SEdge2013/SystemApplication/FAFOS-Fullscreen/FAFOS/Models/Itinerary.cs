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
    class Itinerary
    {
        public void set(int oID, int done,int user_id, string due)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            int id = 1;
            SqlCommand command ;
            SqlDataReader reader;
            SqlConnection con = new SqlConnection(connString);
            bool result=true;

            if (result)
            {
                con.Open();
                command = new SqlCommand("SELECT MAX(itinerary_id) FROM ServiceItinerary", con);
                reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader[0] != DBNull.Value)
                    {
                        id = Convert.ToInt32(reader[0].ToString()) + 1;
                    }
                }
                con.Close();

                con.Open();
                command = new SqlCommand("INSERT INTO ServiceItinerary VALUES (" + id + ",'" + due +
                     "'," + done + "," + user_id + "," + oID + ")", con);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ef)
                {
                    MessageBox.Show("Could not automatically schdeule all of the contract services");
                }
                con.Close();
            }
            
           // return id;
        }

        //public bool checkExists(int id, string date)
        //{
        //     String connString = FAFOS.Properties.Settings.Default.FAFOS;
        //    SqlConnection con = new SqlConnection(connString);

        //    con.Open();
        //    bool result =false;
        //    SqlCommand command = new SqlCommand("SELECT * FROM ServiceItinerary " +
        //        "WHERE con_serv_id = " + id, con);
        //    SqlDataReader reader = command.ExecuteReader();
        //    if (reader.Read())
        //    {
        //        result = true;
        //    }
        //    con.Close();

        //    return result;
        //}
        public void complete(int id, string date)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlCommand command;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            command = new SqlCommand("UPDATE ServiceItinerary SET completed = 1 WHERE con_serv_id = " + id + " AND date_due = '" + date+"'", con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ef)
            {
              //  MessageBox.Show(ef.ToString());
            }
            con.Close();
        }




    }
}
