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
    class ServiceAddress
    {
        public DataSet get()
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            DataSet dt = new DataSet();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT Client.account_name + ', ' + Service_Address.address, Service_Address.service_address_id AS varchar "
  + "FROM Service_Address LEFT OUTER JOIN "
  + "Client_Contract ON Service_Address.client_contract_id = Client_Contract.client_contract_id INNER JOIN "
  + "Client ON Client_Contract.client_id = Client.client_id", con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            adap.Fill(dt);

            con.Close();
            return dt;
        }
        public String get(String id)
        {
            Address ad = new Address();
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String address="";
            con.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Service_Address WHERE service_address_id = " + id, con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                address = reader[1].ToString() + "," +
                    reader[2].ToString() + "," + reader[3].ToString() + "," + ad.getCity(reader[6].ToString()) + "," +
                    ad.getProvince(reader[5].ToString()) + "," + ad.getCountry(reader[4].ToString());

            }
            catch (Exception e)
            {

            }
            // d.Tables.Add(dt);
            con.Close();
            return address;
        }
        public String getID(String Contractid)
        {
            Address ad = new Address();
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String address = "";
            con.Open();
            SqlCommand command = new SqlCommand("SELECT service_address_id FROM Service_Address WHERE client_contract_id = " + Contractid, con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                address = reader[0].ToString();

            }
            catch (Exception e)
            {

            }
            // d.Tables.Add(dt);
            con.Close();
            return address;
        }

        public DataTable getAddresses()
        {

             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            DataTable dataTable = new DataTable("Orders");

            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("address", typeof(String));

            con.Open();

            SqlCommand command = new SqlCommand("SELECT service_address_id,address,city_id FROM Service_Address", con);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                dataTable.Rows.Add(new String[] { reader[0].ToString(), reader[1].ToString()+", "+
                    new Address().getCity(reader[2].ToString())});
            }
            con.Close();
            return dataTable;
        }

        public String getContractID(String id)
        {
            Address ad = new Address();
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String address = "";
            con.Open();
            SqlCommand command = new SqlCommand("SELECT client_contract_id FROM Service_Address WHERE service_address_id = " + id, con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                address = reader[0].ToString();

            }
            catch (Exception e)
            {

            }
            // d.Tables.Add(dt);
            con.Close();
            return address;
        }
        public String getProvinceID(String id)
        {
            Address ad = new Address();
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String province = "";
            con.Open();
            SqlCommand command = new SqlCommand("SELECT province_id FROM Service_Address WHERE service_address_id = " + id, con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                province = reader[0].ToString();

            }
            catch (Exception e)
            {

            }
            // d.Tables.Add(dt);
            con.Close();
            return province;
        }
      
    }
}
