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
    class MClient : Model
    {
/*
        0: client_id
        1: account_name
        2: type
        3: address
        4: postal_code
        5: main_phone
        6: secondary_phone
        7: fax
        8: email
        9: poBox
        10: primary_contact
        11: country_id
        12: province_id
        13: city_id
*/
        private static bool old;
        private static String clientID, countryID, provStateID, cityID, contractID;

        public MClient()
        {
            clientID = null;
            countryID = null;
            provStateID = null;
            cityID = null;
            contractID = null;
            old = false;
        }

        public MClient(string id)
        {
            old = true;
            clientID = id;
            countryID = null;
            provStateID = null;
            cityID = null;
            contractID = null;
            Get();
        }      

        public override String[] Get() 
        {
            if (old)
            {
                String[] returnRow = GetRow(clientID, "Client", "client_id");
                countryID = returnRow[11];
                provStateID = returnRow[12];
                cityID = returnRow[13];
                contractID = returnRow[14];

                return returnRow;// Query DB for the row data
            }
            else return null;
        }

        public override void Set(String[] values)
        {
            String connString = Properties.Settings.Default.FAFOS;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);
            
            
            for (int i = 0; i < values.Length;i++ )
                if (values[i] == null)
                    values[i] = "NULL";
             

            con.Open();
            if(old)
            {

                    SqlCommand command = new SqlCommand("UPDATE Client SET account_name = '" + values[1] +
                                                                          "', type = '" + values[2] +
                                                                          "', address = '" + values[3] +
                                                                          "', postal_code = '" + values[4] +
                                                                          "', main_phone = '" + values[5] +
                                                                          "', secondary_phone = '" + values[6] +
                                                                          "', fax = '" + values[7] +
                                                                          "', email = '" + values[8] +
                                                                          "', poBox = '" + values[9] +
                                                                          "', primary_contact = '" + values[10] +
                                                                          "', country_id = " + countryID +
                                                                          ", province_id = " + provStateID +
                                                                          ", city_id = " + cityID +
                                                                          ", client_contract_id = "+ contractID +
                                                                          " WHERE client_id = " + clientID, con);
                    

                command.ExecuteNonQuery();
            }
            
            else //if(!old)
            {                
                clientID = FindID();
                if (contractID == null) contractID = "NULL";

                SqlCommand command = new SqlCommand("INSERT INTO Client VALUES ("+ clientID + ", '"
                                                                                    + values[1] + "', '"
                                                                                    + values[2] + "', '"
                                                                                    + values[3] + "', '"
                                                                                    + values[4] + "', '"
                                                                                    + values[5] + "', '"
                                                                                    + values[6] + "', '"
                                                                                    + values[7] + "', '"
                                                                                    + values[8] + "', '"
                                                                                    + values[9] + "', '"
                                                                                    + values[10] + "', "
                                                                                    + countryID + ", "
                                                                                    + provStateID + ", "
                                                                                    + cityID + ", "
                                                                                    + contractID + ")", con);
                command.ExecuteNonQuery();           
            }
            con.Close();
            return;
        }
        public static void SetContract(String cliID, String conID)
        {
            String connString = Properties.Settings.Default.FAFOS;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("UPDATE Client SET  client_contract_id = " + conID +
                                                                     " WHERE client_id = " + cliID, con);
            SqlCommand command2 = new SqlCommand("UPDATE Client_Contract SET  client_id = " + conID +
                                                                     " WHERE client_contract_id = " + cliID, con);

            command.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            con.Close();
        }

        public override String FindID()
        {
            clientID = FindGenID(clientID,"Client", "client_id");
            return clientID;
        }
        public String GetID() { return clientID; }

        public static String GetName(String id)
        {
            return GetRow(id, "Client", "client_id")[1];
        }
       
        public static DataTable GetList()
        {
            return GetColumn("Client","client_id", "account_name");
        }        

        public void Delete()
        {
            Delete(FindID(), "Client", "client_id");
        }

        public String GetCountry() { return countryID; }
        public String GetProv() { return provStateID; }
        public String GetCity() { return cityID; }
        public String GetContract() { return contractID; }
        public void changeCountry(String newID)
        {
            countryID = newID;
        }
        public void changeProvince(String newID)
        {
            provStateID = newID;
        }
        public void changeCity(String newID)
        {
            cityID = newID;
        }
        public void changeContract(String newID)
        {
            contractID = newID;
        }
        public bool isNew() { return !old; }
        public static DataTable GetUnLinked()
        {
            DataTable dt = null;

            String connString = Properties.Settings.Default.FAFOS;
            dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT client_id, account_name FROM Client WHERE client_contract_id IS NULL", con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            adap.Fill(dt);
            con.Close();

            return dt;
        }

    }
}
