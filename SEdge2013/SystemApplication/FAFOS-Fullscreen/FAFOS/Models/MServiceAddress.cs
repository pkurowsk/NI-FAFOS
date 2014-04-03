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
    class MServiceAddress : Model
    {
        String srvAddrID;
        public override void Set(string[] values)
        {
            String connString = Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            int temp = 1;
            if (values[1] == null)
            {
                temp = 0;
            }
            if (values[2] == null)
            {
                temp = 0;
            }
            if (values[4] == null)
            {
                temp = 0;
            }
            if (values[5] == null)
            {
                temp = 0;
            }
            if (values[6] == null)
            {
                temp = 0;
            }
            if (values[7] == null)
            {
                temp = 0;
            }
            if (values[8] == null)
            {
                temp = 0;
            }
            if (values[9] == null)
            {
                temp = 0;
            }
            if (values[0] == null)
            {
                temp = 0;
            }
            if(temp == 0){
                MessageBox.Show("please enter all required fields");
            }
            else if (temp == 1)
            {
                SqlCommand command = new SqlCommand("UPDATE Service_Address SET address = '" + values[1] +
                                                                        "', postal_code = '" + values[2] +
                                                                        "', on_site_contact = '" + values[3] +
                                                                        "', country_id = " + values[4] +
                                                                        ", province_id = " + values[5] +
                                                                        ", city_id = " + values[6] +
                                                                        ", num_floors = " + values[7] +
                                                                        ", num_rooms = " + values[8] +
                                                                        ", client_contract_id = " + values[9] +
                                                                        " WHERE service_address_id = " + values[0], con);


                command.ExecuteNonQuery();
            }
            con.Close();
            return;
        }

        public string AddBlank()
        {
            String connString = Properties.Settings.Default.FAFOS;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            srvAddrID = FindID();
            con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Service_Address VALUES (" + srvAddrID + ", '"
                                                                                + "<empty>" + "', '"
                                                                                + "<empty>" + "', '"
                                                                                + "<empty>" + "', "                                                                                
                                                                                + "0" + ", "
                                                                                + "0" + ", "
                                                                                + "0" + ", "
                                                                                + "0" + ", "
                                                                                + "0" + ", "
                                                                                + "0" + ")", con);
            command.ExecuteNonQuery();
            con.Close();
            return srvAddrID;

        }
        
        public override string FindID()
        {
            srvAddrID = FindGenID(srvAddrID, "Service_Address", "service_address_id");
            return srvAddrID;
        }        

        public string[] Get(String id)
        {
            String[] returnVal = GetRow(id, "Service_Address", "service_address_id");
            srvAddrID = id;
            return returnVal;
        }

        public static DataTable GetAllAddrs(String id)
        {
            DataTable dt = null;

            String connString = Properties.Settings.Default.FAFOS;
            dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT service_address_id FROM Service_Address WHERE client_contract_id = " + id, con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            adap.Fill(dt);
            con.Close();

            return dt;

        }

        public static void Delete(String id)
        {
            MContractServices.DeleteAll(id);
            Delete(id, "Service_Address", "service_address_id");
            
        }

        public static void DeleteBlanks()
        {
            DataTable dt = GetColumn("Service_Address", "service_address_id", "address", "address", "'<empty>'");
            int r = dt.Rows.Count;
            if (r > 0)
            {
                for (int i = 0; i < r; i++)
                {
                    try { MContractServices.DeleteAll(dt.Rows[i][0].ToString()); }
                    catch (Exception) { }
                }

                Delete("'<empty>'", "Service_Address", "address");
            }
        }
        
        public static void DeleteAll(String parentID)
        {
            Delete(parentID, "Service_Address", "client_contract_id");
        }

        public override string[] Get()
        {
            throw new NotImplementedException();
        }
    }
}
