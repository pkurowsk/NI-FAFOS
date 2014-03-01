using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Xml;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FAFOS
{
    public partial class RoyaltyFeeCollection : Background
    {
        RoyaltyFee r;
        String userid;
        public RoyaltyFeeCollection(String id)
        {
            InitializeComponent();

            //User label
            userid = id;
            setup(userid, "FAFOS Royalty Fee Collection");
             r = new RoyaltyFee();

             franchiseeBox.DataSource = new Franchisee().getAll();
             franchiseeBox.DisplayMember = "name";
             franchiseeBox.ValueMember = "ID";



        }

        private void RoyaltyFeeCollection_Load(object sender, EventArgs e)
        {
            int[] ids = new Franchisee().getTotal();
            for (int h = 0; h <ids.Length ; h++)
            {

                String[] data = new String[6];
                string url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
                    + "\\Resources\\royaltyFee_" + ids[h] + ".xml";
                XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(url);
                XmlElement docElement = doc.DocumentElement;
                int i = 0;
                /// loop through all childNodes
                foreach (XmlNode childNode in docElement.ChildNodes)
                {
                    if (childNode.HasChildNodes)
                    {
                        foreach (XmlNode c in childNode.ChildNodes)
                        {
                            data[i] = c.InnerText;
                            //Console.WriteLine(c.Name + ": " + c.InnerText);
                            i++;
                        }
                    }
                    else
                    {
                        //Console.WriteLine(childNode.Name + ": " + childNode.InnerText);
                        data[i] = childNode.InnerText;
                        i++;
                    }
                }

               
                if (!r.check(data[0], data[3] + "-" + data[2] + "-" + data[1]))
                    r.set(data);
            }
            
        }

        private void show_btn_Click(object sender, EventArgs e)
        {
            if (franchiseeBox.Text != null && franchiseeBox.Text != ""
                && yearBox.Text != null && yearBox.Text != "")
            {
                DataTable dt = r.get(franchiseeBox.SelectedValue.ToString(), yearBox.Text);
                royalteeFees.DataSource = dt;
                royalteeFees.Visible = true;
            }
            else
            {
                MessageBox.Show("Please select the Franchisee and Year", "Error", MessageBoxButtons.OKCancel);
            }
        }


    }
}
