using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace FAFOS
{
    class InventoryTransaction
    {
        SqlDataAdapter TransactionSQLDataAdapter;
        DataTable transactionsTable;

        public DataTable getTransactions(String userid)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();

            transactionsTable = new DataTable("Transactions");

            SqlCommand TransactionSelectSQLCommand = new SqlCommand();
            TransactionSQLDataAdapter = new SqlDataAdapter();

            TransactionSelectSQLCommand.CommandType = CommandType.Text;
            TransactionSelectSQLCommand.CommandText = "SELECT s.name AS supplier,t.date_issued FROM Supplier s,Inventory_Transaction t WHERE s.supplier_id = t.supplier_id AND t.franchisee_id =" + userid;
            TransactionSelectSQLCommand.Connection = con;
            TransactionSQLDataAdapter.SelectCommand = TransactionSelectSQLCommand;

            DataSet ds = new DataSet();
            TransactionSQLDataAdapter.Fill(ds, "Inventory_Transaction");
            transactionsTable = ds.Tables["Inventory_Transaction"];
            con.Close();
            return transactionsTable;
        }
        public void insertInventoryTransaction(String franchisee_id, String supplier_id, String date_issued)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Inventory_Transaction (franchisee_id,supplier_id, date_issued) VALUES (@franchisee_id,@supplier_id, @date_issued)", con);
            command.Parameters.AddWithValue("@date_issued", date_issued);
            command.Parameters.AddWithValue("@supplier_id", supplier_id);
            command.Parameters.AddWithValue("@franchisee_id", franchisee_id);
            command.ExecuteNonQuery();
            con.Close();
        }
    }
}
