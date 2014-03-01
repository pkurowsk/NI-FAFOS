using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace FAFOS
{
    class MOpReg :Model
    {
        public override void Set(string[] values)
        {
            throw new NotImplementedException();
        }
        public static void SetNew(String[] vals)
        {
            String connString = Properties.Settings.Default.HQ;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            MOpReg o = new MOpReg();
            String id = GetNewHQID();

            con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Operational_Region VALUES ("+ id +",'"
                                                                                        + vals[1] + "',"
                                                                                        + vals[2] + ","
                                                                                        + vals[3] + ","  
                                                                                        + vals[4] +" )", con);
            command.ExecuteNonQuery();
            con.Close();

        }

        public override string FindID()
        {
            throw new NotImplementedException();
        }

        public override string[] Get()
        {
            throw new NotImplementedException();
        }

        public static DataTable GetRegions()
        {
            String connString = Properties.Settings.Default.HQ;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Operational_Region", con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            adap.Fill(dt);
            con.Close();
            return dt;
        }
        public static bool hasFranchise(string zone_id)
        {
            String connString = Properties.Settings.Default.HQ;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Franchisee WHERE zone_id = "+ zone_id, con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            adap.Fill(dt);
            con.Close();
            
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public static void Delete(string zone_id)
        {
            String connString = Properties.Settings.Default.HQ;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("DELETE FROM Operational_Region WHERE zone_id = " + zone_id, con);
            command.ExecuteNonQuery();
            con.Close();
        }
        public static String GetNewHQID()
        {
            String connString = Properties.Settings.Default.HQ;
            String id = "1";
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT MAX(zone_id) FROM Operational_Region", con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader[0] != DBNull.Value)
                {
                    id = (Convert.ToInt32(reader[0].ToString()) + 1).ToString();
                }
            }
            con.Close();
            return id;
        }
    }
}
