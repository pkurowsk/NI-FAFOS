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
    class RoyaltyFee
    {
        public DataTable get(String id,String year)
        {

             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            DataTable dataTable = new DataTable("Orders");

            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("dateIssued", typeof(String));
            dataTable.Columns.Add("month", typeof(String));
            dataTable.Columns.Add("percentage", typeof(String));
            dataTable.Columns.Add("amount", typeof(String));

            con.Open();

            SqlCommand command = new SqlCommand("SELECT royalty_fee_id,dateIssued,monthFor,royaltyFeePercentage,balance "+
  "FROM [RoyaltyFee],Franchisee_Contract "+
  "WhERE RoyaltyFee.franchisee_id=Franchisee_Contract.franchisee_id "+
 " AND RoyaltyFee.franchisee_id="+id +" "+
  "AND  datepart(yyyy,dateIssued) = "+year+" "+
  "ORDER BY dateIssued desc", con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dataTable.Rows.Add(new String[] { reader[0].ToString(), ((DateTime)reader[1]).ToShortDateString(), reader[2].ToString(), 
                    reader[3].ToString(), reader[4].ToString()});
            }
            con.Close();
            return dataTable;
        }
        public bool check(String id, String date)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM [RoyaltyFee] WHERE franchisee_id = "+id+" AND dateIssued = '" + date + "'", con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    if (reader[0] != DBNull.Value)
                    {
                        return true;
                    }
                }

                
            }
            catch (Exception e)
            {
               // MessageBox.Show(e.ToString());
            }
            // d.Tables.Add(dt);
            con.Close();
            return false;
        }
        public void set(String [] data)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            int id = 1;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT MAX(royalty_fee_id) FROM RoyaltyFee", con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader[0] != DBNull.Value)
                {
                    id = Convert.ToInt32(reader[0].ToString()) + 1;
                }
            }
            con.Close();
            String arg;
            arg = "'" + data[3] +"-"+data[2]+"-"+data[1]+ "','" 
                + data[4] + "',"
                + data[0] + ",'" 
                + data[5] + "'";
            
                //Console.WriteLine(data[k] + ",");

            con.Open();
            command = new SqlCommand("INSERT INTO RoyaltyFee VALUES (" + id + "," + arg + ")", con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ef)
            {
                MessageBox.Show("Could not save the following royalty fee");
            }
            con.Close();
        
        }
    }
}
