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
    class ItemTransaction
    {
        SqlDataAdapter TransactionSQLDataAdapter;
        DataTable transactionsTable;

        /*ublic DataTable getTransactions(String userid)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();

            transactionsTable = new DataTable("Transactions");

            SqlCommand TransactionSelectSQLCommand = new SqlCommand();
            TransactionSQLDataAdapter = new SqlDataAdapter();

            TransactionSelectSQLCommand.CommandType = CommandType.Text;
            TransactionSelectSQLCommand.CommandText = "SELECT s.name AS supplier,t.date_issued, FROM Supplier s,Inventory_Transaction t WHERE s.supplier_id = t.supplier_id AND t.franchisee_id =" + userid;
            TransactionSelectSQLCommand.Connection = con;
            TransactionSQLDataAdapter.SelectCommand = TransactionSelectSQLCommand;

            DataSet ds = new DataSet();
            TransactionSQLDataAdapter.Fill(ds, "Inventory_Transaction");
            transactionsTable = ds.Tables["Inventory_Transaction"];
            con.Close();
            return transactionsTable;
        }*/

        public void insertItemTransaction(String inventory_transaction_id, String item_id, String cost, String quantity)
        {
            SqlConnection con = new SqlConnection(Properties.Settings.Default.FAFOS);
            con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Item_Transaction (inventory_transaction_id,item_id, cost, quantity) VALUES (@inventory_transaction_id,@item_id, @cost, @quantity)", con);
            command.Parameters.AddWithValue("@inventory_transaction_id", inventory_transaction_id);
            command.Parameters.AddWithValue("@item_id", item_id);
            command.Parameters.AddWithValue("@cost", cost);
            command.Parameters.AddWithValue("@quantity", quantity);
            command.ExecuteNonQuery();
            con.Close();
        }
    }
}