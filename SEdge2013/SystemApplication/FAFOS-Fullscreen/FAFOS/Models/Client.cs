using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
namespace FAFOS
{
    class Client
    {
        public DataSet get()
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            DataSet dt = new DataSet();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT account_name, client_id FROM Client", con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            adap.Fill(dt);

            con.Close();
            return dt;
        }

        public String get(String id)
        {
            Address ad = new Address();
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String clientInfo="";
            con.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Client WHERE client_id = " + id, con);
            try{
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            clientInfo =  reader[1].ToString() + "," +reader[3].ToString() + "," + reader[4].ToString() + "," +
                reader[5].ToString() + "," + reader[9].ToString() + "," + reader[10].ToString() + "," + 
                ad.getCity(reader[13].ToString()) + "," +ad.getProvince(reader[12].ToString()) + "," + ad.getCountry(reader[11].ToString());
            }
            catch (Exception e)
            { }

            // d.Tables.Add(dt);
            con.Close();
            return clientInfo;
        }
        public String getName(String id)
        {
            
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String clientInfo = "";
            con.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Client WHERE client_id = " + id, con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                clientInfo = reader[1].ToString();
            }
            catch (Exception e)
            { }

            // d.Tables.Add(dt);
            con.Close();
            return clientInfo;
        }
    }
}
