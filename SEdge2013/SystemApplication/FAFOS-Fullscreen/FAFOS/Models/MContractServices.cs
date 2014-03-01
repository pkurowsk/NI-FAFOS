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
    class MContractServices : Model
    {
        public void SetMany(String[,] cells,int userid,DateTime end)
        {

            String connString = Properties.Settings.Default.FAFOS;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connString);
            SqlCommand command;
            con.Open();
            for (int i = 0; i < (cells.Length / 6); i++)
            {
                if (cells[i, 0] == null)//---------------------------Insert new
                {
                    cells[i, 0] = getNewID("con_serv_id", "Contract_Services");
                    command = new SqlCommand("INSERT INTO Contract_Services VALUES (" + cells[i, 0] + ", "
                                                                                      + cells[i, 1] + ", "
                                                                                      + cells[i, 2] + ", '"
                                                                                      + cells[i, 3] + "', '"
                                                                                      + cells[i, 4] + "', "
                                                                                      + cells[i, 5]  + ")", con);


                DateTime dueDate = Convert.ToDateTime(cells[i, 3]).AddDays(365 / 
                    new Period().getPerYear(new Period().getName(cells[i, 2])));

                //Set end dates
                DateTime endDate = end;//new ClientContract().getEndDate(new ServiceAddress().getContractID(cells[i,5]), userid);

                while (dueDate <= endDate)
                {
                    new Itinerary().set(Convert.ToInt32(cells[i, 0]), 0, userid, dueDate.ToShortDateString());
                    dueDate = Convert.ToDateTime(dueDate).AddDays(365 /
                    new Period().getPerYear(new Period().getName(cells[i, 2])));
                }

                }
                else//-----------------------------------------------update old
                {
                    command = new SqlCommand("UPDATE Contract_Services SET service_id = " + cells[i, 1] +
                                                                    ", period_id = " + cells[i, 2] +
                                                                    ", date = '" + cells[i, 3] +
                                                                    "', notes = '" + cells[i, 4] +
                                                                    "', service_addr_id = " + cells[i, 5] +
                                                                    " WHERE con_serv_id = " + cells[i, 0], con);


                }
                command.ExecuteNonQuery();  
            }              
            con.Close();
            return;
        }
        public static DataTable GetAll(String srvAddrId)
        {
            return GetDT(srvAddrId, "Contract_Services", "service_addr_id");
        }
        public static void Delete(String id)
        {
            Delete(id, "Contract_Services", "con_serv_id");
            Delete(id, "ServiceItinerary", "con_serv_id");
        }
        public static void DeleteAll(String ParentID)
        {
            DataTable dt = GetColumn("Contract_Services","con_serv_id" ,"service_addr_id","service_addr_id", ParentID);
            Delete(ParentID, "Contract_Services", "service_addr_id");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //MessageBox.Show(dt.Rows[i][0].ToString());
                 Delete(dt.Rows[i][0].ToString(), "ServiceItinerary", "con_serv_id");
            }
        }
        public override void Set(string[] values)
        {
            throw new NotImplementedException();
        }
        public override string FindID()
        {
            throw new NotImplementedException();
        }
        public override string[] Get()
        {
            throw new NotImplementedException();
        }
    }
}
