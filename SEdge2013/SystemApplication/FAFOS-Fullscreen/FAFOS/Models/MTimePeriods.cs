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
    class MTimePeriods:Model
    {
        public static DataTable GetList()
        {
            return GetColumn("Time_Periods", "period_id", "period_nm");
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
