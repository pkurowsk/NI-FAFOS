using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace FAFOS
{
    class MFranchise : Model
    {
        public override void Set(string[] values)
        {
            throw new NotImplementedException();
        }
        public static void SetAll(String[] fields, DataTable usrs, DataTable BAddrs)
        {
            String connString = Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand FranchiseeCommand = new SqlCommand("UPDATE Franchisee SET company_name = '" + fields[1] +
                                                                          "', tax_registration = " + fields[2] +
                                                               ", business_registration_number = " + fields[3] +
                                                                           ", fiscal_year_end = '" + fields[4] +
                                                                        "' WHERE franchisee_id = " + fields[0], con);
            
            
            con.Open();
            FranchiseeCommand.ExecuteNonQuery(); 
            con.Close();

            int n = usrs.Rows.Count;
            for (int i = 0; i < n; i++)
                MUser.Set(usrs.Rows[i]);

            int x = BAddrs.Rows.Count;
            for (int i = 0; i < x; i++)
                SetBAddr(BAddrs.Rows[i]); 
        }
        public static void SetHQ(DataRow row)
        {
            String connString = Properties.Settings.Default.HQ;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand FranchiseeCommand;

            if (!CheckOriginality(row["idCol"].ToString()))
            {
                FranchiseeCommand = new SqlCommand("INSERT INTO Franchisee VALUES (" + row["idCol"].ToString() + ", '"
                                                                                     + row["ComName"].ToString() + "', "
                                                                                     + row["taxNum"].ToString() + ", "
                                                                                     + row["BusiNum"].ToString() + ", '"
                                                                                     + row["FiscalYrEnd"].ToString() + "', "
                                                                                     + row["zoneID"].ToString() + ", '"
                                                                                     + row["isHQ"].ToString() +"')", con);
            }
            else
            {
                FranchiseeCommand = new SqlCommand("UPDATE Franchisee SET company_name = '" + row["ComName"].ToString() +
                                                                   "', tax_registration = " + row["taxNum"].ToString() +
                                                        ", business_registration_number = " + row["BusiNum"].ToString() +
                                                                    ", fiscal_year_end = '" + row["FiscalYrEnd"].ToString() +
                                                                            "', zone_id = " + row["zoneID"].ToString() +
                                                                               ", isHQ = '" + row["isHQ"].ToString() +
                                                                  "' WHERE franchisee_id = " + row["idCol"].ToString(), con);
            }

            
            
            con.Open();
            FranchiseeCommand.ExecuteNonQuery();
            con.Close();
        }

        public override string FindID()
        {
            throw new NotImplementedException();
        }

        public override string[] Get()
        {
            throw new NotImplementedException();
        }
        public static string[] GetThis()
        {
            return GetRow(Properties.Settings.Default.FranchiseeID, "Franchisee", "franchisee_id");
        }
        public static DataTable GetAllFs(string zoneID)
        {
            String connString = Properties.Settings.Default.HQ;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command;
            if (zoneID == "NULL")
                command = new SqlCommand("SELECT * FROM Franchisee", con);
            else
                command = new SqlCommand("SELECT * FROM Franchisee  WHERE zone_id = " + zoneID, con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            adap.Fill(dt);
            con.Close();
            return dt;
        }
        public static void SetBAddr(DataRow bAddr)
        {
            String connString = Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand BAddrsCommand;
            MFranchise f = new MFranchise();
            if (bAddr["locID"].ToString() == "")
            {
                String newID = f.getNewID("location_id", "Franchisee_Business_Address");
                BAddrsCommand = new SqlCommand("INSERT INTO Franchisee_Business_Address VALUES (" 
                                                                            + newID + ", '"
                                                                            + bAddr["bAddr"].ToString() + "', '"
                                                                            + bAddr["bPostal"].ToString() + "', "
                                                                            + bAddr["bProvince"].ToString() + ", "
                                                                            + bAddr["bCity"].ToString() + ", "
                                                                            + bAddr["bCountry"].ToString() + ", "
                                                                            + Properties.Settings.Default.FranchiseeID + ")", con);
                
            }                
            else            
            {
                BAddrsCommand = new SqlCommand("UPDATE Franchisee_Business_Address " +
                                                  "SET address = '" + bAddr["bAddr"].ToString() +
                                               "', postal_code = '" + bAddr["bPostal"].ToString() +
                                               "', province_id = " + bAddr["bProvince"].ToString() +
                                                   ", city_id = " + bAddr["bCity"].ToString() +
                                                ", country_id = " + bAddr["bCountry"].ToString() +
                                            " WHERE location_id = " + bAddr["locID"].ToString(), con);
                                
            }
            con.Open();
            BAddrsCommand.ExecuteNonQuery();
            con.Close();

        }
        public static DataTable GetBAddrs()
        {
            return GetDT(Properties.Settings.Default.FranchiseeID, "Franchisee_Business_Address", "franchisee_id");
        }
        public static String[] GetOpReg()
        {
            string id = GetDT(Properties.Settings.Default.FranchiseeID, "Franchisee", "franchisee_id").Rows[0]["zone_id"].ToString();
            return GetRow(id, "Operational_Region", "zone_id");
        }
        public static String isFranchisor()
        {
            String x = GetRow(Properties.Settings.Default.FranchiseeID, "Franchisee", "franchisee_id")[6];
            return Regex.Replace(x, @"\s", "");
        }
        public static void DeleteBAddress(string id)
        {
            Delete(id,"Franchisee_Business_Address", "franchisee_id");
        }
        public static void DeleteFromHQ(String id)
        {
            String connString = Properties.Settings.Default.HQ;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("DELETE FROM Franchisee WHERE franchisee_id = " + id, con);
            command.ExecuteNonQuery();
            con.Close();
        }
        
        static public String[] FindCred(String ID)
        {
            String[] result = new String[6];
            String connString = Properties.Settings.Default.HQ;
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command1 = new SqlCommand("SELECT * FROM Franchisee  WHERE franchisee_id = " + ID, con);
            SqlDataAdapter adap1 = new SqlDataAdapter(command1);
            adap1.Fill(dt1);

            SqlCommand command2 = new SqlCommand("SELECT * FROM Operational_Region  WHERE zone_id = " + dt1.Rows[0]["zone_id"].ToString(), con);
            SqlDataAdapter adap2 = new SqlDataAdapter(command2);
            adap2.Fill(dt2);
            con.Close();
            
            result[0] = dt1.Rows[0]["company_name"].ToString();
            result[1] = dt1.Rows[0]["isHQ"].ToString();
            result[2] = dt2.Rows[0]["zone_title"].ToString();
            result[3] = MCountry.GetName(dt2.Rows[0]["country_id"].ToString());
            result[4] = MProvState.GetName(dt2.Rows[0]["province_id"].ToString());
            result[5] = MCity.GetName(dt2.Rows[0]["city_id"].ToString());


            return result;
        }
        static public void SetLocal(String ID)
        {
            #region Get HQ Data
            String[] result = new String[6];
            String connString = Properties.Settings.Default.HQ;
            DataTable franDT = new DataTable();
            DataTable opDT = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command1 = new SqlCommand("SELECT * FROM Franchisee  WHERE franchisee_id = " + ID, con);
            SqlDataAdapter adap1 = new SqlDataAdapter(command1);
            adap1.Fill(franDT);

            SqlCommand command2 = new SqlCommand("SELECT * FROM Operational_Region  WHERE zone_id = " + franDT.Rows[0]["zone_id"].ToString(), con);
            SqlDataAdapter adap2 = new SqlDataAdapter(command2);
            adap2.Fill(opDT);
            con.Close();
            #endregion

            #region Set Local Data
            String connString2 = Properties.Settings.Default.FAFOS;
            SqlConnection con2 = new SqlConnection(connString2);

            con2.Open();
            SqlCommand setComm1 = new SqlCommand("INSERT INTO Franchisee VALUES (" + franDT.Rows[0]["franchisee_id"].ToString() + ", '"
                                                                                    + franDT.Rows[0]["company_name"].ToString() + "', "
                                                                                    + franDT.Rows[0]["tax_registration"].ToString() + ", "
                                                                                    + franDT.Rows[0]["business_registration_number"].ToString() + ", '"
                                                                                    + franDT.Rows[0]["fiscal_year_end"].ToString() + "', "
                                                                                    + franDT.Rows[0]["zone_id"].ToString() + ", '"
                                                                                    + franDT.Rows[0]["isHQ"].ToString() + "')", con2);
            setComm1.ExecuteNonQuery();
            

            SqlCommand setComm2 = new SqlCommand("INSERT INTO Operational_Region VALUES (" + opDT.Rows[0]["zone_id"].ToString() + ", '"
                                                                                           + opDT.Rows[0]["zone_title"].ToString() + "', "
                                                                                           + opDT.Rows[0]["country_id"].ToString() + ", "
                                                                                           + opDT.Rows[0]["province_id"].ToString() + ", "
                                                                                           + opDT.Rows[0]["city_id"].ToString() + ")", con2);
            setComm2.ExecuteNonQuery();
            con2.Close();
            #endregion

        }

        public static bool CheckOriginality(string id)
        {
            String connString = Properties.Settings.Default.HQ;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT * FROM Franchisee  WHERE franchisee_id =" + id, con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            adap.Fill(dt);
            con.Close();
            
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }
        
    }
}
