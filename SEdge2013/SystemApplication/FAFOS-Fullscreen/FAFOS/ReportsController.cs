using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FAFOS.Forms;
using System.Windows.Forms;

namespace FAFOS
{
    public class ReportsController
    {
        private static String franchiseeUserId;
        private static String franchiseeId;
        private int type;

        public ReportsController(String franchiseeUserID)
        {
            franchiseeUserId = franchiseeUserID;
            franchiseeId = new Users().getFranchiseeId(franchiseeUserID);
        }

        public void report(int type)
        {
            this.type = type;
            Reports newSalesOrder = new Reports(this, franchiseeUserId, type);
            newSalesOrder.Show();
        }

        public void prepareReport(object sender, EventArgs e)
        {
            Reports report = (Reports)((ComboBox)sender).FindForm();
            report.fillData(new FAFOS.Invoice().getReports(((ComboBox)sender).SelectedIndex, report.getStartDate(), report.getEndDate()));
        }
    }
}
