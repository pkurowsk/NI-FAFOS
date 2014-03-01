using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FAFOS.Forms
{
    public partial class Reports : Background
    {
        String userid;
        String balanceS;
        public Reports(ReportsController my_controller, String id, int type)
        {
            InitializeComponent();
            userid=id;
            setup(id, "FAFOS Report");
            this.ddlPickReport.SelectedIndexChanged += new EventHandler(my_controller.prepareReport);

            
            chartReport.ChartAreas[0].AxisY.Minimum = 0;

        }

        public void fillData(DataTable dt)
        {
            balanceS = dt.Rows[0][1].ToString();
            dgvReport.DataSource = dt;
            chartReport.DataSource = dt;
            chartReport.Series[0].YValueMembers = "Revenue";
            //chartReport.Series[0].ChartType = SeriesChartType.Line;
            //chartReport.Series["Threshold"].Points.Clear();
            chartReport.ChartAreas[0].AxisY.StripLines.Clear();
            StripLine stripLineTarget = new StripLine();
            stripLineTarget.BorderColor = System.Drawing.Color.Red;

            switch (ddlPickReport.SelectedIndex)
            {
                case 0:
                    chartReport.Series[0].XValueMember = "Day";
                    //chartReport.Series["Threshold"].Points.AddXY(0, 333);
                    //chartReport.Series["Threshold"].Points.AddXY(chartReport.ChartAreas[0].AxisX.Maximum, 333);
                    //chartReport.ChartAreas[0].AxisX.Minimum = 0;
                    stripLineTarget.IntervalOffset = 333;
                    chartReport.ChartAreas[0].AxisY.Maximum = 700;
                    break;
                case 1:
                    chartReport.Series[0].XValueMember = "Week #";
                    //chartReport.Series["Threshold"].Points.AddXY(0, 2500);
                    //chartReport.Series["Threshold"].Points.AddXY(chartReport.ChartAreas[0].AxisX.Maximum, 2500);
                    ///chartReport.ChartAreas[0].AxisX.Minimum = 0;
                    stripLineTarget.IntervalOffset = 2500;
                    chartReport.ChartAreas[0].AxisY.Maximum = 4000;
                    break;
                case 2:
                    chartReport.Series[0].XValueMember = "Month";
                    //chartReport.Series["Threshold"].Points.AddXY(0, 10000);
                    ///chartReport.Series["Threshold"].Points.AddXY(chartReport.ChartAreas[0].AxisX.Maximum, 10000);
                    //chartReport.ChartAreas[0].AxisX.Minimum = 0;
                    stripLineTarget.IntervalOffset = 10000;
                    chartReport.ChartAreas[0].AxisY.Maximum = 12000;
                    break;
                case 3:
                    chartReport.Series[0].XValueMember = "Quarter";
                    //chartReport.Series["Threshold"].Points.AddXY(0, 30000);
                    //chartReport.Series["Threshold"].Points.AddXY(chartReport.ChartAreas[0].AxisX.Maximum, 30000);
                    //chartReport.ChartAreas[0].AxisX.Minimum = 0;
                    stripLineTarget.IntervalOffset = 30000;
                    chartReport.ChartAreas[0].AxisY.Maximum = 50000;
                    break;
                case 4:
                    chartReport.Series[0].XValueMember = "Year";
                    //chartReport.Series["Threshold"].Points.AddXY(2012, 120000);
                    //chartReport.Series["Threshold"].Points.AddXY(2020, 120000);
                    //chartReport.ChartAreas[0].AxisX.Minimum = 2012;
                    //chartReport.ChartAreas[0].AxisX.Maximum = 2020;
                    //chartReport.ChartAreas[0].AxisX.MajorGrid.Interval = 1;
                    stripLineTarget.IntervalOffset = 120000;
                    chartReport.ChartAreas[0].AxisY.Maximum = 150000;
                    break;
                case 5:
                    chartReport.Series[0].XValueMember = "Year";
                    break;
                case 6:
                    chartReport.Series[0].XValueMember = "Year";
                    break;
            }

            chartReport.ChartAreas[0].AxisY.StripLines.Add(stripLineTarget);
            chartReport.DataBind();

        }

        public string getStartDate()
        {
            return dtpStartDate.Value.ToString("yyyyMMdd HH:mm:ss"); ;
        }

        public string getEndDate()
        {
            return dtpEndDate.Value.ToString("yyyyMMdd HH:mm:ss"); ;
        }

        private void generate_btn_Click(object sender, EventArgs e)
        {
            XmlDocument xmldoc;// = new XmlDocument();
            xmldoc=new XmlDocument();
            //let's add the XML declaration section
         //   XmlNode xmlnode=xmldoc.CreateNode(XmlNodeType.XmlDeclaration,"","");
          //  xmldoc.AppendChild(xmlnode);
            //let's add the root element
            XmlElement xmlelem=xmldoc.CreateElement("","RoyaltyFee","");
            xmldoc.AppendChild(xmlelem);
            //let's add another element (child of the root)
            XmlElement xmlelem2=xmldoc.CreateElement("","Franchisee","");
            XmlText xmltext=xmldoc.CreateTextNode(new Users().getFranchiseeId(userid));
            xmlelem2.AppendChild(xmltext);

            XmlElement xmlelem3=xmldoc.CreateElement("","dateIssued","");
            XmlElement day=xmldoc.CreateElement("","day","");
            XmlText daytext=xmldoc.CreateTextNode(DateTime.Today.Day.ToString());
            day.AppendChild(daytext);

            XmlElement month=xmldoc.CreateElement("","month","");
            XmlText monthtext=xmldoc.CreateTextNode(DateTime.Today.Month.ToString());
            month.AppendChild(monthtext);

            XmlElement year=xmldoc.CreateElement("","year","");
            XmlText yeartext=xmldoc.CreateTextNode(DateTime.Today.Year.ToString());
            year.AppendChild(yeartext);
            

            xmlelem3.AppendChild(day);
             xmlelem3.AppendChild(month);
             xmlelem3.AppendChild(year);

         
            XmlElement monthFor=xmldoc.CreateElement("","monthFor","");
            XmlText monthFortext=xmldoc.CreateTextNode("March");
            monthFor.AppendChild(monthFortext);

            XmlElement balance=xmldoc.CreateElement("","balance","");
            double royaltyFee = Convert.ToDouble(balanceS) > 10000 ? 500 + Convert.ToDouble(balanceS) * 0.12 : 500;
            XmlText balancetext = xmldoc.CreateTextNode(royaltyFee.ToString());
            balance.AppendChild(balancetext);
            xmldoc.ChildNodes.Item(0).AppendChild(xmlelem2);
            xmldoc.ChildNodes.Item(0).AppendChild(xmlelem3);
            xmldoc.ChildNodes.Item(0).AppendChild(monthFor);
            xmldoc.ChildNodes.Item(0).AppendChild(balance);



            //let's try to save the XML document in a file: C:\pavel.xml
            try
            {
            xmldoc.Save(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
                   + "\\Resources\\royaltyFee_" + new Users().getFranchiseeId(userid) + ".xml"); //I've chosen the c:\ for the resulting file pavel.xml
            }
            catch (Exception)
            {
           // Console.WriteLine(e.Message);
            }
            MessageBox.Show("It has successfully generated and sent this month's royalty fee.", "FAFOS Message Box");

        }
    }
}
