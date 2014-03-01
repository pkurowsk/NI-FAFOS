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
    class Item
    {
        public void set(String msg)
        {

            MessageBox.Show("The string \"" + msg + "\" will be sent to TCP connection\n\nThis message from the Model");
        }

        public DataRow getRow(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            DataTable dt = new DataTable();
            DataRow dr = null;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT item_id,name,description FROM Franchisee_Item WHERE item_id = " + id, con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            try{
            adap.Fill(dt);
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();
            int fieldCount = reader.FieldCount;
            dr = dt.NewRow();
            for (int i = 0; i < fieldCount; i++)
            {
                dr[i] = reader[i].ToString();
            }
            }
            catch (Exception e)
            { }
           // d.Tables.Add(dt);
            con.Close();
            return dr;
        }
        public DataSet get(string id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            DataSet dt = new DataSet();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT name, item_id FROM Franchisee_Item WHERE franchisee_id = " + id, con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            adap.Fill(dt);

            con.Close();
            return dt;
        }

        public string getDescription(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT description FROM Franchisee_Item WHERE item_id = " + id, con);
            SqlDataReader reader = command.ExecuteReader();

            String description = "";
            if (reader.Read())
            {
                description = reader[0].ToString();
            }
            reader.Close();
            con.Close();
            return description;
        }
             public string getPrice(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT price FROM Franchisee_Item WHERE item_id = " + id, con);
            SqlDataReader reader = command.ExecuteReader();

            String description = "";
            if (reader.Read())
            {
                description = reader[0].ToString();
            }
            reader.Close();
            con.Close();
            return description;
        }
    }
}
