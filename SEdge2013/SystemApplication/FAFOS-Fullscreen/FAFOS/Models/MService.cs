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
    class MService : Model
    {
        public static DataTable GetList()
        {
            return GetColumn("Service", "service_id", "service_nm");
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
