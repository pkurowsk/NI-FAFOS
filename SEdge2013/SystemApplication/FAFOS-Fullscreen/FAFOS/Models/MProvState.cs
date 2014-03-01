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
    class MProvState : Model
    {

        public override void Set(String[] values)
        {
            throw new NotImplementedException();
        }

        public static DataTable GetList()
        {
            return GetColumn("Province","province_id", "province_nm");
        }
        public static DataTable GetFilteredList(String countryID)
        {
            return GetColumn("Province", "province_id", "province_nm", "country_id", countryID);
        }
        public static string GetName(String id)
        {
            String name = GetRow(id, "Province", "province_id")[1];
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
            name = GetRow(name, "Province", "province_nm")[0];
            return name;
        }
        public static String toName(String id)
        {
            id = GetRow(id, "Province", "province_id")[1];
            return id;
        }

        public static DataTable GetData(String id)
        {
            return GetDT(id, "Province", "province_id");
        }
    }
}
