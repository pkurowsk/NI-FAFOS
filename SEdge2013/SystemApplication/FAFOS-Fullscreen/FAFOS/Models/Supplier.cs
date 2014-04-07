using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System;

namespace FAFOS
{
    class Supplier : Model
    {
         String connString = FAFOS.Properties.Settings.Default.FAFOS;
        DataTable dt;
        private static String supplierID;
        private static bool old;
        MaintainClientController my_controller;

         public Supplier()
        {
            
            supplierID = null;
            
            old = false;
        }

        public Supplier(string id)
        {
            old = true;
            
            supplierID = null;
           
            Get();
        }

        public DataTable get()
        {
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            dt = new DataTable();
            dt.Columns.Add("service_id");
            dt.Columns.Add("name");
            SqlCommand command = new SqlCommand("SELECT * FROM Supplier", con);
            //SqlDataAdapter da = new SqlDataAdapter(command);
            //da.Fill(dt);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dt.Rows.Add(new String[] { reader[0].ToString(), reader[1].ToString()});
            }
            con.Close();
            return dt;

        }
        public void changeSupplier(String newID)
        {
            supplierID = newID;
        }

        public override String[] Get()
        {
            if (old)
            {
                String[] returnRow = GetRow(supplierID, "Supplier", "name");
                supplierID = returnRow[1];
               

                return returnRow;// Query DB for the row data
            }
            else return null;
        }
       
        public override void Set(string[] values)
        {
            MessageBox.Show("change this to set 1");
            throw new NotImplementedException();
        }
        public override String FindID()
        {
            supplierID = FindGenID(supplierID, "Supplier", "supplier_id");
            return supplierID;
        }
        public static DataTable GetList()
        {
            return GetColumn("Supplier", "supplier_id", "name");
        }
       

        public bool Set1(String value)
        {
              String connString = FAFOS.Properties.Settings.Default.FAFOS;
            int id = 1;
            
            SqlCommand command ;
            SqlDataReader reader;
            SqlConnection con = new SqlConnection(connString);
            bool result=true;

            if (value == "")
                result = false;
            if (result)
            {
                con.Open();
                command = new SqlCommand("SELECT MAX(supplier_id) FROM Supplier", con);
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

                command = new SqlCommand("INSERT INTO Supplier VALUES (" + id.ToString() + ",'" + value + "')", con);
                try
                {

                    command.ExecuteNonQuery();
                }
                catch (SqlException e)
                {
                    MessageBox.Show("Could Not Update Supplier");
                }
                con.Close();
                return result;
            }
            else
            {
                MessageBox.Show("please enter a new supplier");
                return result;
            }
            

            
        }

        public void Delete(String id)
        {
            int id2 = Convert.ToInt32(id);
            String connString = Properties.Settings.Default.FAFOS;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("DELETE FROM Supplier WHERE supplier_id = " + id, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                // MessageBox.Show(e.ToString());
            }
            con.Close();

        }
    
    }
}
