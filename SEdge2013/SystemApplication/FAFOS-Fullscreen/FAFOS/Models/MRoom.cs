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
        public static void SetMany(string[,] rooms)
        {
            String connString = Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand command;

            int nRooms = (rooms.Length / 4);
            con.Open();
            for (int i = 0; i < nRooms; i++)
            {

                command = new SqlCommand("UPDATE Room SET room_num = '" + rooms[i,1] +
                                                      "', floor = '" + rooms[i, 2] +
                                                      "', service_addr_id = " + rooms[i, 3] +
                                                  " WHERE room_id = " + rooms[i, 0], con);


                command.ExecuteNonQuery();
                
            }
            con.Close();
        }
        public static void SetExtinguishers(String[,] values)
        {
            String connString = Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand command;

            int nExt = (values.Length / 7);
            con.Open();
            for (int i = 0; i < nExt; i++)
            {
                MRoom r;
                if (values[i, 0] == null)
                {
                    r = new MRoom();
                    values[i, 0] = r.getNewID("extinguisher_id", "Extinguisher");

                    command = new SqlCommand("INSERT INTO Extinguisher VALUES (" + values[i, 0] +
                                                                            ",'" + values[i, 1] +
                                                                           "','" + values[i, 2] +
                                                                           "','" + values[i, 3] +
                                                                           "','" + values[i, 4] +
                                                                            "'," + values[i, 5] +
                                                                             "," + values[i, 6] + ")", con);
                }

                else
                {
                    command = new SqlCommand("UPDATE Extinguisher SET location = '" + values[i, 1] +
                                                          "', size = '" + values[i, 2] +
                                                          "', type = '" + values[i, 3] +
                                                          "', model = '" + values[i, 4] +
                                                          "', serial = " + values[i, 5] +
                                                           ", room_id = " + values[i, 6] +
                                                      " WHERE extinguisher_id = " + values[i, 0], con);
                }
                command.ExecuteNonQuery();

            }
            con.Close();
        }
        public static void SetHoses(String[,] values)
        {
            String connString = Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand command;

            int nHose = (values.Length / 4);
            con.Open();
            for (int i = 0; i < nHose; i++)
            {
                MRoom r;
                if (values[i, 0] == null)
                {
                    r = new MRoom();
                    values[i, 0] = r.getNewID("hose_id", "Hose");

                    command = new SqlCommand("INSERT INTO Hose VALUES (" + values[i, 0] +
                                                                     ",'" + values[i, 1] +
                                                                    "'," + values[i, 2] +
                                                                     "," + values[i, 3] + ")", con);
                }

                else
                {
                    command = new SqlCommand("UPDATE Hose SET location = '" + values[i, 1] +
                                                          "', serial = " + values[i, 2] +
                                                           ", room_id = " + values[i, 3] +
                                                      " WHERE hose_id = " + values[i, 0], con);                   
                }
                command.ExecuteNonQuery();

            }
            con.Close();
        }
        public static void SetLights(String[,] values)
        {
            String connString = Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            SqlCommand command;

            int nLight = (values.Length / 10);
            MRoom r;
            con.Open();
            for (int i = 0; i < nLight; i++)
            {
                if (values[i, 0] == null)
                {
                    r = new MRoom();
                    values[i, 0] = r.getNewID("light_id", "Light");
                    command = new SqlCommand("INSERT INTO Light VALUES (" + values[i, 0] +
                                                           ",'" + values[i, 1] +
                                                          "','" + values[i, 2] +
                                                          "','" + values[i, 3] +
                                                          "','" + values[i, 4] +
                                                          "','" + values[i, 5] +
                                                          "','" + values[i, 6] +
                                                          "','" + values[i, 7] +
                                                           "'," + values[i, 8] +
                                                            "," + values[i, 9] + ")", con);
                }
                else
                {

                    command = new SqlCommand("UPDATE Light SET location = '" + values[i, 1] +
                                                          "', model = '" + values[i, 2] +
                                                          "', make = '" + values[i, 3] +
                                                          "', heads = '" + values[i, 4] +
                                                          "', power = '" + values[i, 5] +
                                                          "', voltage = '" + values[i, 6] +
                                                          "', require_service = '" + values[i, 7] +
                                                          "', serial = " + values[i, 8] +
                                                           ", room_id = " + values[i, 9] +
                                                      " WHERE light_id = " + values[i, 0], con);
                }


                command.ExecuteNonQuery();

            }
            con.Close();
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
