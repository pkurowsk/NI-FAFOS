using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FAFOS
{
    class MUser : Model
    {
        int userID;
        float perOwnership;
        bool isAdmin, isHQ;
        String[] _values;

        public MUser(int Userid)
        {
            userID = Userid;
            
            try
            {
                _values = GetRow(userID.ToString(), "[User]", "user_id");
                if (String.Equals(Regex.Replace(_values[15], @"\s", ""), "true", StringComparison.OrdinalIgnoreCase))
                    isAdmin = true;
                if (String.Equals(Regex.Replace(_values[15], @"\s", ""), "false", StringComparison.OrdinalIgnoreCase))
                    isAdmin = false;

                SetHQ(_values[14]);
                perOwnership = Convert.ToSingle(_values[16]);
            }
            catch (Exception)
            {
                _values = new string[22];
                String[] op = MFranchise.GetOpReg();
                _values[19] = op[2];
                _values[17] = op[3];
                _values[18] = op[4];
                String connString = Properties.Settings.Default.FAFOS;
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection(connString);

                con.Open();
                SqlCommand command = new SqlCommand("INSERT INTO [User] VALUES (" + userID + ",'<empty>','<empty>','<empty>','<empty>','<empty>','<empty>','<empty>','<empty>','<empty>','<empty>','<empty>','<empty>','<empty>','False','False',0," + _values[17] + "," + _values[18] + "," + _values[19] + "," + Properties.Settings.Default.FranchiseeID + ",0)", con);
                command.ExecuteNonQuery();                
                con.Close();
            }
        }
        public static void DeleteBlanks()
        {
            Delete("'<empty>'", "[User]", "first_nm");
        }
        public override void Set(string[] values)
        {
            values[14] = isAdmin.ToString();
            values[15] = isHQ.ToString();
            values[16] = perOwnership.ToString();

            String connString = Properties.Settings.Default.FAFOS;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);

            for (int i = 0; i < values.Length; i++)
                if (values[i] == null)
                    values[i] = "NULL";

            con.Open();

            SqlCommand command = new SqlCommand("UPDATE [User] SET first_nm = '" + values[1] +
                                                                "', last_nm = '" + values[2] +
                                                              "', middle_nm = '" + values[3] +
                                                               "', username = '" + values[4] +
                                                               "', password = '" + values[5] +
                                                                "', address = '" + values[6] +
                                                            "', postal_code = '" + values[7] +
                                                             "', home_phone = '" + values[8] +
                                                             "', cell_phone = '" + values[9] +
                                                                   "', fax = '" + values[10] +
                                                                 "', email = '" + values[11] +
                                                              "', skype_id = '" + values[12] +
                                                              "', position = '" + values[13] +
                                                               "', hq_user = '" + values[14] +
                                                            "', admin_user = '" + values[15] +
                                                  "', percentage_ownership = '" + values[16] +
                                                            "', province_id = " + values[17] +
                                                                 ", city_id = " + values[18] +
                                                              ", country_id = " + values[19] +
                                                           ", franchisee_id = " + values[20] +
                                                               ", proPic_id = " + values[21] +
                                                          " WHERE user_id = " + values[0], con);


            command.ExecuteNonQuery();
            
            con.Close();
        }
        public static void Set(DataRow usr)
        {
            MFranchise f = new MFranchise();
            String connString = Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand UsersCommand;
            int n = usr.ItemArray.Length;
            for (int i = 0; i < n; i++)
                if (usr[i].ToString() == "")
                    usr[i] = "NULL";

            if (usr["usrIDCol"].ToString() == "NULL") // Create new User               
            {
                string newID = f.getNewID("user_id", "[User]");// assign user id
                List<Bitmap> piclist = LoadImages();
                string newPicID = piclist.Count.ToString();
                piclist.Add(Properties.Resources.DefaultProPic);
                SaveImages(piclist);

                List<Bitmap> test = LoadImages();
                
                UsersCommand = new SqlCommand("INSERT INTO [User] VALUES (" + newID + ", '"
                                                                            + usr["fName"].ToString() + "', '"
                                                                            + usr["lName"].ToString() + "', '"
                                                                            + usr["mName"].ToString() + "', '"
                                                                            + usr["usrName"].ToString() + "', '"
                                                                            + usr["passSetCol"].ToString() + "', "
                                                                            + "'<empty>','<empty>','<empty>','<empty>','<empty>','<empty>','<empty>','<empty>', '" 
                                                                            + usr["hqCol"].ToString() + "', '"
                                                                            + usr["adminCol"].ToString() + "', "
                                                                            + usr["PerOwnCol"].ToString() 
                                                                            + ",1,1,1, "
                                                                            + Properties.Settings.Default.FranchiseeID + ", "
                                                                            + newPicID + ")", con);
                
            }                
            else            
            {            
                UsersCommand = new SqlCommand("UPDATE [User] SET first_nm = '" + usr["fName"].ToString() +
                                                                "', last_nm = '" + usr["lName"].ToString() +
                                                              "', middle_nm = '" + usr["mName"].ToString() +
                                                               "', username = '" + usr["usrName"].ToString() +
                                                               "', password = '" + usr["passSetCol"].ToString() +
                                                                "', hq_user = '" + usr["hqCol"].ToString() +
                                                             "', admin_user = '" + usr["adminCol"].ToString() +
                                                   "', percentage_ownership = " + usr["PerOwnCol"].ToString() +
                                                             " WHERE user_id = " + usr["usrIDCol"].ToString(), con);
                                
            } 
            con.Open();
            UsersCommand.ExecuteNonQuery();
            con.Close();
        }
        public override string FindID()
        {
            throw new NotImplementedException();
        }
        public override string[] Get()
        {
            return _values;
        }
        public String[] GetLocation()
        {
            return new String[] { _values[19], _values[17], _values[18] };
        }
        public static void Delete(String id)
        {
            List<Bitmap> picList = LoadImages();
            int index = Convert.ToInt32(GetPicID(id));
            picList[index] = null;
            Delete(id,"[User]","user_id");
        }

        public static int GetPicID(String userID)
        {
            DataTable dt = GetDT(userID, "[User]", "user_id");

            int picID = Convert.ToInt32(dt.Rows[0]["proPic_id"].ToString());
            return picID;
        }
        public static DataTable GetUserList()
        {
            DataTable dt = new DataTable();

            String connString = Properties.Settings.Default.FAFOS;            
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT user_id, first_nm, middle_nm, last_nm FROM [User] ", con);
            SqlDataAdapter adap = new SqlDataAdapter(command);
            adap.Fill(dt);
            con.Close();

            return dt;
        }
        public static DataTable GetAllUsers()
        {
            return GetDT(Properties.Settings.Default.FranchiseeID, "[User]", "franchisee_id");
        }

        public void giveAdmin() { isAdmin = true; }
        public void revokeAdmin() { isAdmin = false;}
        public bool IsAdmin() { return isAdmin; }

        public void SetHQ(String hqStatus)
        {
            if (String.Equals(hqStatus, "true", StringComparison.OrdinalIgnoreCase))
                isHQ = true;

            if (String.Equals(hqStatus, "false", StringComparison.OrdinalIgnoreCase))
                isHQ = false;
        }
        public bool IsHQ() { return isHQ; }

        public static void SaveImages(List<Bitmap> imgs)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamReader sr = new StreamReader(ms))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, imgs);
                    ms.Position = 0;
                    byte[] buffer = new byte[(int)ms.Length];
                    ms.Read(buffer, 0, buffer.Length);
                    Properties.Settings.Default.ProfilePics = Convert.ToBase64String(buffer);
                    Properties.Settings.Default.Save();
                }
            }
        }
        public static List<Bitmap> LoadImages()
        {
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(Properties.Settings.Default.ProfilePics)))
            {
                BinaryFormatter bf = new BinaryFormatter();
                return (List<Bitmap>)bf.Deserialize(ms);
            }
        }

        public static String ConBlank(String s)
        {
            if(string.Equals(s,"NULL"))
                return "";
            if(string.Equals(s,"<empty>"))
                return "";
            else
                return s;
        }

    }
}
