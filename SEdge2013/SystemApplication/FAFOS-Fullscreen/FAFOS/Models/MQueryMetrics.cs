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
    class MQueryMetrics : Model
    {
        public static String GetCount(string Table, String identifier, String idColumnName)
        {
            String count = "0";
            try
            {
                String connString = Properties.Settings.Default.FAFOS;
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(connString);
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM " + Table +
                                                                  " WHERE " + idColumnName +
                                                                      " = " + identifier, con);
                con.Open();
                SqlDataAdapter adap = new SqlDataAdapter(command);
                adap.Fill(dt);
                con.Close();

                count = dt.Rows[0][0].ToString();
            }
            catch (Exception) { }
            return count;
                       
        }

        public override void Set(string[] values)
        {
            throw new NotImplementedException();
        }
        public override string FindID()
        {
            throw new NotImplementedException();
        }
        public override string[] Get()
        {
            throw new NotImplementedException();
        }
    }
}
