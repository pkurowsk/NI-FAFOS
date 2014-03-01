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
    abstract class Model
    {
       public abstract void Set(String[] values);

        public abstract String FindID();
        public String getNewID(String colName, String table)
        {
            String connString = Properties.Settings.Default.FAFOS;
            String id = "1";
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT MAX(" + colName + ") FROM " + table, con);
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
        public String FindGenID(String ID,String table, String idColumn)
        {
            if (ID == null)
            {
                ID = getNewID(idColumn, table);
            }
            return ID;
        }
        
        public abstract String[] Get();

        public static String[] GetRow(String id, String table, String columnName)
        {
            DataTable dt = GetDT(id, table, columnName);
            DataRow dr = dt.Rows[0];

            String[] StArr = new String[dr.ItemArray.Length];
            for (int i = 0; i < dr.ItemArray.Length; i++)
                StArr[i] = dr.ItemArray[i].ToString();

            return StArr;
        }

        public static DataTable GetDT(String id, String table, String idcolumn)
        {
            String connString = Properties.Settings.Default.FAFOS;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM " + table + "  WHERE " + idcolumn + " = " + id, con);
            try
            {
                SqlDataAdapter adap = new SqlDataAdapter(command);
                adap.Fill(dt);
            }
            catch (SqlException e)
            {

            }
            con.Close();
            return dt;
        }

        public static DataTable GetColumn(String table, String idColumn, String nameColumn)
        {
            DataTable ds = null;

            String connString = Properties.Settings.Default.FAFOS;
            ds = new DataTable("table");
            ds.Columns.Add("id");
            ds.Columns.Add("name");
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT " + idColumn + ", " + nameColumn + " FROM " + table, con);
          //  SqlDataAdapter adap = new SqlDataAdapter(command);
           // adap.Fill(ds);
            SqlDataReader rd = command.ExecuteReader();
            while(rd.Read())
                   ds.Rows.Add(new string[] {rd[0].ToString(),rd[1].ToString()});

            con.Close();

            return ds;
        }

        public static DataTable GetColumn(String table, String idColumn, String nameColumn, String parentColumn, String idOfParent)
        {
            DataTable ds = null;

            String connString = Properties.Settings.Default.FAFOS;
            ds = new DataTable("table");
            ds.Columns.Add("id");
            ds.Columns.Add("name");
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT " + idColumn + ", " + nameColumn + " FROM " + table + " WHERE " + parentColumn + " = " + idOfParent + " ORDER BY " + nameColumn, con);
            SqlDataReader rd = command.ExecuteReader();
            while (rd.Read())
                ds.Rows.Add(new string[] { rd[0].ToString(), rd[1].ToString() });

            con.Close();

            return ds;
        }

        public static void Delete(String id, String table, String columnName)
        {
            String connString = Properties.Settings.Default.FAFOS;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("DELETE FROM " + table + " WHERE "+ columnName +"= " + id, con);
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
