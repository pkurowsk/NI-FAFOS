using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace FAFOS
{
    class Invoice
    {

        public String[] getOutstandingClients()
        {

             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String[] results = null;

            con.Open();

            SqlCommand command = new SqlCommand("SELECT COUNT(DISTINCT client_id) " +
  "FROM Client_Contract, Service_Address "+
  "WHERE Client_Contract.client_contract_id = Service_Address.client_contract_id "+
  "AND Service_Address.service_address_id IN "+
	"						(SELECT service_address_id "+
	"						FROM Sales_Order "+
	"						WHERE sales_order_id IN (SELECT sales_order_id "+
	"												FROM Invoice "+
	"												WHERE payed='no'))", con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
             results = new String[Convert.ToInt32(reader[0])];
            }
            con.Close();


            con.Open();

             command = new SqlCommand("SELECT DISTINCT client_id " +
  "FROM Client_Contract, Service_Address " +
  "WHERE Client_Contract.client_contract_id = Service_Address.client_contract_id " +
  "AND Service_Address.service_address_id IN " +
    "						(SELECT service_address_id " +
    "						FROM Sales_Order " +
    "						WHERE sales_order_id IN (SELECT sales_order_id " +
    "												FROM Invoice " +
    "												WHERE payed='no'))", con);
             reader = command.ExecuteReader();
             int i = 0;
            while (reader.Read())
            {
                results[i] = reader[0].ToString();
                i++;
            }
            con.Close();
            return results;
        }

        public String[] getOutstandingInvoices(string id)
        {

             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            String[] results = null;

            con.Open();

            SqlCommand command = new SqlCommand("  SELECT COUNT(*)" +
"  FROM [Invoice]" +
"  WHERE sales_order_id IN (SELECT sales_order_id " +
"							FROM Sales_Order " +
"							WHERE service_address_id IN (SELECT service_address_id " +
"							FROM Service_Address " +
"							WHERE client_contract_id = (SELECT client_contract_id " +
"							FROM Client_Contract WHERE client_id = " + id + ")))" +
"  AND (total - (SELECT COALESCE(SUM(amount),0) FROM Payment " +
"			WHERE payment_id IN (SELECT payment_id FROM Invoice_Payment " +
"			WHERE invoice_id = Invoice.invoice_id))) !=0 ", con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                results = new String[Convert.ToInt32(reader[0])];
            }
            con.Close();


            con.Open();

            command = new SqlCommand("  SELECT date_issued, invoice_id,"+
"  (total - (SELECT COALESCE(SUM(amount),0) FROM Payment " +
"			WHERE payment_id IN (SELECT payment_id FROM Invoice_Payment "+
"			WHERE invoice_id = Invoice.invoice_id))) as balance"+
"  FROM [Invoice]"+
"  WHERE sales_order_id IN (SELECT sales_order_id "+
"							FROM Sales_Order "+
"							WHERE service_address_id IN (SELECT service_address_id "+
"							FROM Service_Address "+
"							WHERE client_contract_id = (SELECT client_contract_id "+
"							FROM Client_Contract WHERE client_id = "+id+")))"+
"  AND (total - (SELECT COALESCE(SUM(amount),0) FROM Payment " +
"			WHERE payment_id IN (SELECT payment_id FROM Invoice_Payment " +
"			WHERE invoice_id = Invoice.invoice_id))) !=0 ", con);
            reader = command.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                results[i] = reader[0].ToString() + "," + reader[1].ToString() + ","+reader[2].ToString();
                i++;
            }
            con.Close();
            return results;
        }
        public int getID()
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            int id = 1;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT MAX(invoice_id) FROM Invoice", con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader[0] != DBNull.Value)
                {
                    id = Convert.ToInt32(reader[0].ToString()) + 1;
                }
            }
            con.Close();
            return id;

        }
        public int getSalesOrderID(String Sid)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            int id = 1;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT sales_order_id FROM Invoice WHERE invoice_id = "+Sid, con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader[0] != DBNull.Value)
                {
                    id = Convert.ToInt32(reader[0].ToString()) ;
                }
            }
            con.Close();
            return id;

        }
        public int set(String arg)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            int id =1;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("SELECT MAX(invoice_id) FROM Invoice", con);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader[0] != DBNull.Value)
                {
                    id = Convert.ToInt32(reader[0].ToString()) + 1;
                }
            }
            con.Close();

            con.Open();
            command = new SqlCommand("INSERT INTO Invoice VALUES ("+id+","+arg+")", con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ef)
            {
                MessageBox.Show("The invoice could not be saved into the system");
            }
            con.Close();
            return id;
        }
        public void update(String id)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);

            con.Open();
            SqlCommand command = new SqlCommand("UPDATE Invoice SET payed = 'yes' WHERE invoice_id = " + id, con);
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException ef)
            {
                MessageBox.Show("The payment could not be processed.");
            }
            con.Close();
        }
         public DataTable getReports(int type, String dateStart, String dateEnd)
        {
             String connString = FAFOS.Properties.Settings.Default.FAFOS;
            DataTable d = new DataTable();
            SqlConnection con = new SqlConnection(connString);
            SqlCommand command = new SqlCommand("SELECT DATEPART(wk,date_issued) AS 'Week #', SUM(total) AS 'Revenue', COUNT(*) AS '# of Work Orders' "
                + "FROM [Invoice] "
                + "WHERE date_issued >= '" + dateStart + "' AND date_issued <= '" + dateEnd + "'"
                + "GROUP BY  DATEPART(YEAR,date_issued), DATEPART(wk,date_issued)", con);

            con.Open();
            switch (type)
            {
                case 0:
                    /*command = new SqlCommand("SELECT DATENAME(yyyy,date_issued) + ' D' + DATENAME(dy,date_issued) AS 'Day', SUM(total) AS 'Revenue', COUNT(*) AS '# of Work Orders' "
                        + "FROM [Invoice] "
                        + "WHERE date_issued >= '" + dateStart + "' AND date_issued <= '" + dateEnd + "'"
                        + "GROUP BY date_issued", con);*/
                    command = new SqlCommand(";WITH alltransactions AS ("
                        + "SELECT MIN(date_issued) AS continuousdate, MAX(date_issued) AS maximumdate "
                        + "FROM [Invoice] "
                        + "WHERE date_issued >= '" + dateStart + "' AND date_issued <= '" + dateEnd + "' "
                        + "UNION ALL SELECT DATEADD(dy, 1, continuousdate) AS continuousdate, maximumdate FROM alltransactions WHERE DATEADD(dy, 1, continuousdate) <= DATEADD(dy,1,maximumdate)) "
                        + "SELECT DATENAME(yyyy,at.continuousdate) + ' D' + DATENAME(dy,at.continuousdate) AS 'Day', (CASE WHEN SUM(total) IS NULL THEN 0 ELSE SUM(total) END) AS 'Revenue', "
                        + "(CASE WHEN SUM(total) IS NULL THEN 0 ELSE COUNT(*) END) AS '# of Work Orders' "
                        + "FROM alltransactions at LEFT OUTER JOIN Invoice cp ON YEAR(at.continuousdate) = YEAR(cp.date_issued) AND DATEPART(dy,at.continuousdate) = DATEPART(dy,cp.date_issued) "
                        + "GROUP BY at.continuousdate ORDER BY at.continuousdate", con);
                    break;
                case 1:
                    /*command = new SqlCommand("SELECT DATENAME(yyyy,date_issued) + ' W' + DATENAME(ww,date_issued) AS 'Week #', SUM(total) AS 'Revenue', COUNT(*) AS '# of Work Orders' "
                        + "FROM [Invoice] "
                        + "WHERE date_issued >= '" + dateStart + "' AND date_issued <= '" + dateEnd + "'"
                        + "GROUP BY date_issued", con);*/
                    command = new SqlCommand(";WITH alltransactions AS ("
                        + "SELECT MIN(date_issued) AS continuousdate, MAX(date_issued) AS maximumdate "
                        + "FROM [Invoice] "
                        + "WHERE date_issued >= '" + dateStart + "' AND date_issued <= '" + dateEnd + "' "
                        + "UNION ALL SELECT DATEADD(ww, 1, continuousdate) AS continuousdate, maximumdate FROM alltransactions WHERE DATEADD(ww, 1, continuousdate) <= DATEADD(ww,1,maximumdate)) "
                        + "SELECT DATENAME(yyyy,at.continuousdate) + ' W' + DATENAME(ww,at.continuousdate) AS 'Week #', (CASE WHEN SUM(total) IS NULL THEN 0 ELSE SUM(total) END) AS 'Revenue', "
                        + "(CASE WHEN SUM(total) IS NULL THEN 0 ELSE COUNT(*) END) AS '# of Work Orders' "
                        + "FROM alltransactions at LEFT OUTER JOIN Invoice cp ON YEAR(at.continuousdate) = YEAR(cp.date_issued) AND DATEPART(ww,at.continuousdate) = DATEPART(ww,cp.date_issued) "
                        + "GROUP BY at.continuousdate ORDER BY at.continuousdate", con);
                    break;
                case 2:
                    command = new SqlCommand(";WITH alltransactions AS ("
                        + "SELECT MIN(date_issued) AS continuousdate, MAX(date_issued) AS maximumdate "
                        + "FROM [Invoice] "
                        + "WHERE date_issued >= '" + dateStart + "' AND date_issued <= '" + dateEnd + "' "
                        + "UNION ALL SELECT DATEADD(MONTH, 1, continuousdate) AS continuousdate, maximumdate FROM alltransactions WHERE DATEADD(MONTH, 1, continuousdate) <= maximumdate) "
                        + "SELECT DATENAME(yyyy,at.continuousdate) + ' ' + DATENAME(mm,at.continuousdate) AS 'Month', (CASE WHEN SUM(total) IS NULL THEN 0 ELSE SUM(total) END) AS 'Revenue', "
                        + "(CASE WHEN SUM(total) IS NULL THEN 0 ELSE COUNT(*) END) AS '# of Work Orders' "
                        + "FROM alltransactions at LEFT OUTER JOIN Invoice cp ON YEAR(at.continuousdate) = YEAR(cp.date_issued) AND MONTH(at.continuousdate) = MONTH(cp.date_issued) "
                        + "GROUP BY at.continuousdate ORDER BY at.continuousdate", con);
                    break;
                case 3:
                    /*command = new SqlCommand("SELECT DATENAME(yyyy,date_issued) + ' Q' + DATENAME(qq,date_issued) AS 'Quarter', SUM(total) AS 'Revenue', COUNT(*) AS '# of Work Orders' " //+"DATEPART(YEAR,date_issued)*10 + DATEPART(qq,date_issued)" +
                        + "FROM [Invoice] "
                        + "WHERE date_issued >= '" + dateStart + "' AND date_issued <= '" + dateEnd + "'"
                        + "GROUP BY date_issued", con);*/
                    command = new SqlCommand(";WITH alltransactions AS ("
                        + "SELECT MIN(date_issued) AS continuousdate, MAX(date_issued) AS maximumdate "
                        + "FROM [Invoice] "
                        + "WHERE date_issued >= '" + dateStart + "' AND date_issued <= '" + dateEnd + "' "
                        + "UNION ALL SELECT DATEADD(QUARTER, 1, continuousdate) AS continuousdate, maximumdate FROM alltransactions WHERE DATEADD(QUARTER, 1, continuousdate) <= DATEADD(qq,1,maximumdate)) "
                        + "SELECT DATENAME(yyyy,at.continuousdate) + ' Q' + DATENAME(qq,at.continuousdate) AS 'Quarter', (CASE WHEN SUM(total) IS NULL THEN 0 ELSE SUM(total) END) AS 'Revenue', "
                        + "(CASE WHEN SUM(total) IS NULL THEN 0 ELSE COUNT(*) END) AS '# of Work Orders' "
                        + "FROM alltransactions at LEFT OUTER JOIN Invoice cp ON YEAR(at.continuousdate) = YEAR(cp.date_issued) AND DATEPART(qq,at.continuousdate) = DATEPART(qq,cp.date_issued) "
                        + "GROUP BY at.continuousdate ORDER BY at.continuousdate", con);
                    break;
                case 4:
                    command = new SqlCommand("SELECT DATEPART(yyyy,date_issued) AS 'Year', SUM(total) AS 'Revenue', COUNT(*) AS '# of Work Orders' "
                        + "FROM [Invoice] "
                        + "WHERE date_issued >= '" + dateStart + "' AND date_issued <= '" + dateEnd + "'"
                        + "GROUP BY DATEPART(yyyy,date_issued)", con);
                    break;
                case 5:
                    command = new SqlCommand("SELECT DATEPART(yyyy,date_issued) AS 'Year', SUM(total) AS 'Revenue', COUNT(*) AS '# of Work Orders' "
                        + "FROM [Invoice] "
                        + "WHERE date_issued >= '" + dateStart + "' AND date_issued <= '" + dateEnd + "'"
                        + "GROUP BY DATEPART(yyyy,date_issued)", con);
                    break;
                case 6:
                    command = new SqlCommand("SELECT DATEPART(yyyy,date_issued) AS 'Year', SUM(total) AS 'Revenue', COUNT(*) AS '# of Work Orders' "
                        + "FROM [Invoice] "
                        + "WHERE date_issued >= '" + dateStart + "' AND date_issued <= '" + dateEnd + "'"
                        + "GROUP BY DATEPART(yyyy,date_issued)", con);
                    break;
            }
            SqlDataAdapter adap = new SqlDataAdapter(command);
            try
            {
                adap.Fill(d);
            }
            catch (Exception e)
            { }
            con.Close();
            return d;
        }
    
    }
}
