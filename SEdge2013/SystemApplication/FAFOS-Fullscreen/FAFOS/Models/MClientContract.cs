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
    class MClientContract : Model
    {
        bool old;
        String ContractID,clientID;
        /*
         * 0: contract_id
         * 1: contract_nm
         * 2: start_date
         * 3: end_date
         * 4: terms
        */ 
        public MClientContract()
        {
            ContractID = null;
            clientID = null;
            old = false;
        }

        public MClientContract(String id)
        {
            old = true;
            ContractID = id;
            Get();
        }

        public override void Set(string[] values)
        {
            String connString = Properties.Settings.Default.FAFOS;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            if (old)
            {

                SqlCommand command = new SqlCommand("UPDATE Client_Contract SET contract_nm = '" + values[1] +
                                                                      "', start_date = '" + values[2] +
                                                                      "', end_date = '" + values[3] +
                                                                      "', terms = '" + values[4] +
                                                                       "', client_id = '" + clientID +
                                                                        "', franchisee_id = '" + Properties.Settings.Default.FranchiseeID +  
                                                                      "' WHERE client_contract_id = " + ContractID, con);

                command.ExecuteNonQuery();
            }

            else //if(!old)
            {
                ContractID = FindID();

                SqlCommand command = new SqlCommand("INSERT INTO Client_Contract VALUES (" + ContractID + ", '"
                                                                                    + values[1] + "', '"
                                                                                    + values[2] + "', '"
                                                                                    + values[3] + "', '"
                                                                                    + values[4] + "', '"
                                                                                    + clientID + "', '"
                                                                                    + Properties.Settings.Default.FranchiseeID + "')", con);
                command.ExecuteNonQuery();

            }
            con.Close();
            return;
        }

        public static DataTable GetList()
        {
            return GetColumn("Client_Contract", "client_contract_id", "contract_nm");
        }

        public override string[] Get()
        {
            if (old)
            {
                String[] returnRow = GetRow(ContractID, "Client_Contract", "client_contract_id");
                ContractID = returnRow[0];
                clientID = returnRow[5];
                return returnRow;
            }
            else return null;
        }
        public override String FindID()
        {
            ContractID = FindGenID(ContractID,"Client_Contract", "client_contract_id");
            return ContractID;

        }
        public String getClientID(){ return clientID; }
        public void SetClient(String id)
        {
            if( id != "System.Data.DataRowView")
                clientID = id;
        }
        public static String GetName(String id)
        {
            return GetRow(id, "Client_Contract", "client_contract_id")[1];
        }

        public void Delete()
        {
            DataTable Addrs = MServiceAddress.GetAllAddrs(ContractID);
            int r = Addrs.Rows.Count;
            for (int i = 0; i < r; i++)
            {
                try { MServiceAddress.Delete(Addrs.Rows[i][0].ToString()); }
                catch (Exception) { }
            }

            Delete(ContractID, "Client_Contract", "client_contract_id");
        }
        public static void DeleteAll(String id)
        {
            Delete(id, "Client_Contract", "client_id");
        }
        public void RemoveFromClient()
        {
            try
            {
                String connString = Properties.Settings.Default.FAFOS;
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(connString);

                con.Open();
                SqlCommand command = new SqlCommand("UPDATE Client SET client_contract_id = NULL WHERE client_id = " + clientID, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch (SqlException) { }
        }

    }
}
