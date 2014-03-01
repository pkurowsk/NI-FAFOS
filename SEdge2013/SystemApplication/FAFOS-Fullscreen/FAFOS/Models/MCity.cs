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
    class MCity : Model
    {

        public override void Set(String[] values)
        {
            throw new NotImplementedException();
        }

        public static DataTable GetList()
        {
            return GetColumn("City","city_id", "city_nm");
        }
        public static DataTable GetFilteredList(String ProvStateID)
        {
            return GetColumn("City", "city_id", "city_nm", "province_id", ProvStateID);
        }
        public static string GetName(String id)
        {
            String name = GetRow(id, "City", "city_id")[1];
            return name;
        }

        public override string[] Get()
        {
            throw new NotImplementedException();
        }

        public override string FindID()
        {
            throw new NotImplementedException();
        }

        public static String toID(String name)
        {
            name = GetRow(name, "City", "city_nm")[0];
            return name;
        }
        public static String toName(String id)
        {
            id = GetRow(id, "City", "city_id")[1];
            return id;
        }
        public static DataTable GetData(String id)
        {
            return GetDT(id, "City", "city_id");
        }
    }
}
