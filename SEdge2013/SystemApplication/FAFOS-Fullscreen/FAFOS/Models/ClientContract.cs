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
    class ClientContract
    {
        public String getClient(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String client="";
            con.Open();
            SqlCommand command = new SqlCommand("SELECT client_id FROM Client_Contract WHERE client_contract_id = (SELECT client_contract_id FROM Service_Address WHERE service_address_id = " + id+")", con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                client = reader[0].ToString();

            }
            catch (Exception e)
            { }
            // d.Tables.Add(dt);
            con.Close();
            return client;
        }

        public DateTime getEndDate(String id, int userid)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            DateTime date = DateTime.Today;
            con.Open();
            SqlCommand command = new SqlCommand("SELECT Client_Contract.end_date  "+
" FROM Client_Contract WHERE Client_Contract.client_contract_id = "+id, con); 
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                date = Convert.ToDateTime(reader[0]);

            }
            catch (Exception e)
            { }
            // d.Tables.Add(dt);
            con.Close();
            return date;
        }

       

        public int getServiceID(String id, int rowID)
        {

            DataTable dataTable = new DataTable("Services");

            dataTable.Columns.Add("id", typeof(String));
            dataTable.Columns.Add("period", typeof(String));
            dataTable.Columns.Add("nextDate", typeof(String));
            dataTable.Columns.Add("notes", typeof(String));
            dataTable.Columns.Add("address", typeof(String));
            dataTable.Columns.Add("city", typeof(String));
            dataTable.Columns.Add("province", typeof(String));
            dataTable.Columns.Add("country", typeof(String));


             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);

            // MessageBox.Show(DateTime.Today.ToShortDateString()); 
            con.Open();
            SqlCommand command = new SqlCommand(" SELECT Contract_Services.con_serv_id, Contract_Services.period_id,ServiceItinerary.date_due, Contract_Services.notes, " +
" Service_Address.address,Service_Address.city_id,Service_Address.province_id,Service_Address.country_id  " +
 "FROM Contract_Services, Service_Address,  ServiceItinerary " +
" WHERE Contract_Services.con_serv_id = ServiceItinerary.con_serv_id AND " +
" Contract_Services.service_addr_id IN  " +
   "				   (SELECT service_address_id  " +
   "					FROM Service_Address  " +
   "					WHERE client_contract_id IN  " +
   "							(SELECT client_contract_id  " +
   "							FROM Client_Contract  " +
   "							WHERE franchisee_id = (SELECT franchisee_id  " +
       "											  FROM [User] " +
      "											   WHERE user_id = " + id + ") " + "AND end_date > '" + DateTime.Today + "'" +
       "						) " +
       "				) " +
"	AND Contract_Services.service_addr_id = Service_Address.service_address_id  " +
"	AND ServiceItinerary.date_due <= (select cast (DATEADD(DAY," + 30 + ",GETDATE()) as DATE))" +
"	AND ServiceItinerary.completed = 0" +
" ORDER BY  ServiceItinerary.date_due", con);
            try
            {
                SqlDataReader reader3 = command.ExecuteReader();
                while (reader3.Read())
                {

                    dataTable.Rows.Add(new String[] { reader3[0].ToString(), new Period().getName(reader3[1].ToString()), 
                        Convert.ToDateTime(reader3[2]).ToShortDateString(), reader3[3].ToString(),reader3[4].ToString(),
                    new Address().getCity(reader3[5].ToString()), new Address().getProvince(reader3[6].ToString()), 
                new Address().getCountry(reader3[7].ToString())});
                }
            }
            catch (Exception e)
            {
              //  MessageBox.Show(e.ToString());

            }
            con.Close();


            int result = Convert.ToInt32(dataTable.Rows[rowID][0]);

            return result;
        }


        public DataTable getServices(String id)
        {

            DataTable dataTable = new DataTable("Services");

            dataTable.Columns.Add("service", typeof(String));
            dataTable.Columns.Add("period", typeof(String));
            dataTable.Columns.Add("nextDate", typeof(String));
            dataTable.Columns.Add("notes", typeof(String));
            dataTable.Columns.Add("address", typeof(String));
            dataTable.Columns.Add("city", typeof(String));
            dataTable.Columns.Add("province", typeof(String));
            dataTable.Columns.Add("country", typeof(String));


             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
           
           // MessageBox.Show(DateTime.Today.ToShortDateString()); 
            con.Open();
            SqlCommand command = new SqlCommand(" SELECT Contract_Services.service_id, Contract_Services.period_id,ServiceItinerary.date_due, Contract_Services.notes, " +
 " Service_Address.address,Service_Address.city_id,Service_Address.province_id,Service_Address.country_id  "+
  "FROM Contract_Services, Service_Address,  ServiceItinerary "+
 " WHERE Contract_Services.con_serv_id = ServiceItinerary.con_serv_id AND "+
 " Contract_Services.service_addr_id IN  "+
	"				   (SELECT service_address_id  "+
	"					FROM Service_Address  "+
	"					WHERE client_contract_id IN  "+
	"							(SELECT client_contract_id  "+
	"							FROM Client_Contract  "+
	"							WHERE franchisee_id = (SELECT franchisee_id  "+
        "											  FROM [User] " +
       "											   WHERE user_id = " + id + ") " + "AND end_date > '" + DateTime.Today + "'" +
		"						) "+
		"				) "+
"	AND Contract_Services.service_addr_id = Service_Address.service_address_id  "+
"	AND ServiceItinerary.date_due <= (select cast (DATEADD(DAY,"+60+",GETDATE()) as DATE))"+
"	AND ServiceItinerary.completed = 0"+
 " ORDER BY  ServiceItinerary.date_due", con);
            try
            {
                SqlDataReader reader3 = command.ExecuteReader();
                while (reader3.Read())
                {

                    dataTable.Rows.Add(new String[] { new Services().getName(reader3[0].ToString()), new Period().getName(reader3[1].ToString()), 
                        Convert.ToDateTime(reader3[2]).ToShortDateString(), reader3[3].ToString(),reader3[4].ToString(),
                    new Address().getCity(reader3[5].ToString()), new Address().getProvince(reader3[6].ToString()), 
                new Address().getCountry(reader3[7].ToString())});
                }

            }
            catch (Exception e)
            { 
                MessageBox.Show("Could not load the contract services, please contact the database adminstrator."); 
            
            }
            con.Close();

            return dataTable;
        }

        public String getFranchisee(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String franchisee="";
            con.Open();
            SqlCommand command = new SqlCommand("SELECT franchisee_id FROM Client_Contract WHERE client_contract_id = (SELECT client_contract_id FROM Service_Address WHERE service_address_id = " + id + ")", con);
             try
            {
            SqlDataReader reader = command.ExecuteReader();
            reader.Read();

            franchisee = reader[0].ToString();
            }
             catch (Exception e)
             { }

            // d.Tables.Add(dt);
            con.Close();
            return franchisee;
        }

        public String getFranchiseeOfClient(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String franchisee = "";
            con.Open();
            SqlCommand command = new SqlCommand("SELECT franchisee_id FROM Client_Contract WHERE client_id = " + id, con);
            try
            {
                SqlDataReader reader = command.ExecuteReader();
                reader.Read();

                franchisee = reader[0].ToString();
            }
            catch (Exception e)
            { }

            // d.Tables.Add(dt);
            con.Close();
            return franchisee;
        }
    }
}
