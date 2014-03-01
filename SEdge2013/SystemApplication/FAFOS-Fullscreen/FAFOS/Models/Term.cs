using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace FAFOS
{
    class Term
    {
        public DataSet get()
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            DataSet s = new DataSet();
            con.Open();
            SqlCommand command = new SqlCommand("SELECT terms FROM Terms ", con);
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter (command);
            //SqlDataReader reader = command.ExecuteReader();

                   adap.Fill(s);
               
            

            }
            catch (Exception ef)
            {
                //MessageBox.Show(ef.ToString());
            }
            // d.Tables.Add(dt);
            con.Close();
            return s;
        }

    }
}
