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
    class MRoom : Model
    {

        public override void Set(string[] values)
        {
            throw new NotImplementedException();
        }
        public static int SetMany(string[,] rooms)
        {
            String connString = Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand command;
            int temp = 1;
            int nRooms = (rooms.Length / 4);
            con.Open();
            for (int i = 0; i < nRooms; i++)
            {
                
                if (rooms[i, 1] == "")
                {
                    temp = 0;
                }
                if (rooms[i, 2] == "")
                {
                    temp = 0;
                }
                if (rooms[i, 3] == "")
                {
                    temp = 0;
                }
                if (rooms[i, 0] == "")
                {
                    temp = 0;
                }

                if (temp == 0)
                {
                    MessageBox.Show("please fill in all fields");
                }
                else if (temp == 1)
                {

                    command = new SqlCommand("UPDATE Room SET room_num = '" + rooms[i, 1] +
                                                          "', floor = '" + rooms[i, 2] +
                                                          "', service_addr_id = " + rooms[i, 3] +
                                                      " WHERE room_id = " + rooms[i, 0], con);


                    command.ExecuteNonQuery();
                }
            }
            con.Close();
            return temp;
        }
        public static int SetExtinguishers(String[,] values)
        {
            String connString = Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand command;

            int temp = 1;
            int nExt = (values.Length / 9);
            con.Open();
            for (int i = 0; i < nExt; i++)
            {
                MRoom r;
                if (values[i, 0] == null)
                {
                    r = new MRoom();
                    values[i, 0] = r.getNewID("extinguisher_id", "Extinguisher");
                   
                    if (values[i, 0] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 1] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 2] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 3] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 4] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 5] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 6] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 7] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 8] == null)
                    {
                        temp = 0;
                    }
                    if (temp == 0)
                    {
                        MessageBox.Show("please fill in all fields");
                    }
                    else if (temp == 1)
                    {

                        command = new SqlCommand("INSERT INTO Extinguisher VALUES (" + values[i, 0] +
                                                                                ",'" + values[i, 1] +
                                                                               "','" + values[i, 2] +
                                                                               "','" + values[i, 3] +
                                                                               "','" + values[i, 4] +
                                                                                "'," + values[i, 5] +
                                                                                 "," + values[i, 6] +
                                                                                 "," + values[i, 7] +
                                                                                 ",'" + values[i, 8] + "')", con);
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    
                    if (values[i, 0] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 1] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 2] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 3] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 4] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 5] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 6] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 7] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 8] == null)
                    {
                        temp = 0;
                    }
                    if (temp == 0)
                    {
                        MessageBox.Show("please fill in all fields");
                    }
                    else if (temp == 1)
                    {
                        command = new SqlCommand("UPDATE Extinguisher SET location = '" + values[i, 1] +
                                                              "', size = '" + values[i, 2] +
                                                              "', type = '" + values[i, 3] +
                                                              "', model = '" + values[i, 4] +
                                                              "', serial = " + values[i, 5] +
                                                               ", room_id = " + values[i, 6] +
                                                               ", bar_code = " + values[i, 7] +
                                                               ", manufacture_date = '" + values[i, 8] +
                                                          "' WHERE extinguisher_id = " + values[i, 0], con);
                        command.ExecuteNonQuery();
                    }
                }
              

            }
            con.Close();
            return temp;
            
        }
        public static int SetHoses(String[,] values)
        {
            String connString = Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand command;

            int temp = 1;
            int nHose = (values.Length / 6);
            con.Open();
            for (int i = 0; i < nHose; i++)
            {
                MRoom r;
                if (values[i, 0] == null)
                {
                    r = new MRoom();
                    values[i, 0] = r.getNewID("hose_id", "Hose");
                    
                    if (values[i, 0] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 1] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 2] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 3] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 4] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 5] == null)
                    {
                        temp = 0;
                    }
                    if (temp == 0)
                    {
                        MessageBox.Show("please fill in all fields");
                    }
                    else if (temp == 1)
                    {
                        command = new SqlCommand("INSERT INTO Hose VALUES (" + values[i, 0] +
                                                                         ",'" + values[i, 1] +
                                                                        "'," + values[i, 2] +
                                                                         "," + values[i, 3] +
                                                                         "," + values[i, 4] +
                                                                          ",'" + values[i, 5] + "')", con);
                        command.ExecuteNonQuery();
                    }
                }

                else
                {
                    if (values[i, 0] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 1] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 2] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 3] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 4] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 5] == null)
                    {
                        temp = 0;
                    }
                    if (temp == 0)
                    {
                        MessageBox.Show("please fill in all fields");
                    }
                    else if (temp == 1)
                    {
                        command = new SqlCommand("UPDATE Hose SET location = '" + values[i, 1] +
                                                              "', serial = " + values[i, 2] +
                                                               ", room_id = " + values[i, 3] +
                                                               ", bar_code = " + values[i, 4] +
                                                               ", manufacture_date = '" + values[i, 5] +
                                                          "' WHERE hose_id = " + values[i, 0], con);
                       command.ExecuteNonQuery();
                    }
                }
                

            }
            con.Close();
            return temp;
        }
        public static int SetLights(String[,] values)
        {
            String connString = Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand command;
            int temp = 1;

            int nLight = (values.Length / 12);
            MRoom r;
            con.Open();
            for (int i = 0; i < nLight; i++)
            {
                if (values[i, 0] == null)
                {
                    r = new MRoom();
                    values[i, 0] = r.getNewID("light_id", "Light");
                    
                    if (values[i, 0] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 1] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 2] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 3] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 4] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 5] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 6] == null)
                    {
                        temp = 0;
                    }
               
                    if (values[i, 8] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 9] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 10] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 11] == null)
                    {
                        temp = 0;
                    }
                    if (temp == 0)
                    {
                        MessageBox.Show("please fill in all fields");
                    }
                    else if (temp == 1)
                    {
                        if (values[i, 7] == null)
                            values[i, 7] = "F";

                        command = new SqlCommand("INSERT INTO Light VALUES (" + values[i, 0] +
                                                               ",'" + values[i, 1] +
                                                              "','" + values[i, 2] +
                                                              "','" + values[i, 3] +
                                                              "','" + values[i, 4] +
                                                              "','" + values[i, 5] +
                                                              "','" + values[i, 6] +
                                                              "','" + values[i, 7][0] +
                                                               "'," + values[i, 8] +
                                                                "," + values[i, 9] +
                                                                "," + values[i, 10] +
                                                                ",'" + values[i, 11] + "')", con);
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    
                    if (values[i, 0] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 1] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 2] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 3] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 4] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 5] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 6] == null)
                    {
                        temp = 0;
                    }
                   
                    if (values[i, 8] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 9] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 10] == null)
                    {
                        temp = 0;
                    }
                    if (values[i, 11] == null)
                    {
                        temp = 0;
                    }
                    if (temp == 0)
                    {
                        MessageBox.Show("please fill in all fields");
                    }
                    else if (temp == 1)
                    {
                        if (values[i, 7] == null)
                            values[i, 7] = "F";
                        command = new SqlCommand("UPDATE Light SET location = '" + values[i, 1] +
                                                              "', model = '" + values[i, 2] +
                                                              "', make = '" + values[i, 3] +
                                                              "', heads = '" + values[i, 4] +
                                                              "', power = '" + values[i, 5] +
                                                              "', voltage = '" + values[i, 6] +
                                                              "', require_service = '" + values[i, 7][0] +
                                                              "', serial = " + values[i, 8] +
                                                               ", room_id = " + values[i, 9] +
                                                               ", bar_code = " + values[i, 10] +
                                                               ", manufacture_date = '" + values[i, 11] +
                                                          "' WHERE light_id = " + values[i, 0], con);
                        command.ExecuteNonQuery();
                    }

                }
                

            }
            con.Close();
            return temp;
        }

        public override string FindID()
        {
            return FindGenID(null, "Room", "room_id");
        }

        public override string[] Get()
        {
            throw new NotImplementedException();
        }

        public static DataTable GetRoomsForAddr(String AddrID)
        {
            return GetDT(AddrID, "Room", "service_addr_id");
        }

        public static DataTable GetExtinguishers(String roomID)
        {
            return GetDT(roomID, "Extinguisher", "room_id");
        }
        public static DataTable GetHoses(String roomID)
        {
            return GetDT(roomID, "Hose", "room_id");
        }
        public static DataTable GetLights(String roomID)
        {
            return GetDT(roomID, "Light", "room_id");
        }

        public static void Delete(String id)
        {
            Delete(id, "Extinguisher", "room_id");
            Delete(id, "Hose", "room_id");
            Delete(id, "Light", "room_id");
            Delete(id, "Room", "room_id");
        }

        public static String AddBlank()
        {
            MRoom r = new MRoom();
            String newID = r.FindID();

            String connString = Properties.Settings.Default.FAFOS;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand command = new SqlCommand("INSERT INTO Room VALUES (" + newID + ", '"
                                                                                + "<empty>" + "', '"
                                                                                + "<empty>" + "', "
                                                                                + "0" + ")", con);
            command.ExecuteNonQuery();
            con.Close();

            return newID;
        }
        public static void RemoveBlanks()
        {
            DataTable blankRooms = GetDT("'<empty>'", "room", "room_num");
            int r = blankRooms.Rows.Count;
            String roomID;
            for (int i = 0; i < r; i++)
            {
                roomID = blankRooms.Rows[i][0].ToString();
                Delete(roomID);
            }
        }

        public static void DeleteExtinguisher(String eID)
        {
            Delete(eID, "Extinguisher", "extinguisher_id");
        }
        public static void DeleteHose(String hID)
        {
            Delete(hID, "Hose", "hose_id");
        }
        public static void DeleteLight(String lID)
        {
            Delete(lID, "Light", "light_id");
        }
    }
}
