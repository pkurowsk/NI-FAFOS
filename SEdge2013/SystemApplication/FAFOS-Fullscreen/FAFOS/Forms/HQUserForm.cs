using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FAFOS
{
    public partial class HQUserForm : Form
    {
        public bool noChanges;
        UserController my_controller;
/********************************** Constructor **********************************/
        public HQUserForm(UserController parent, Point Loc)
        {
            InitializeComponent();
            SetLoc(Loc);
            my_controller = parent;
            LoadOpRegions();
            LoadFranchiseeGridView();
            InitializeCombos("1", "1", "1");


            #region Handlers
            OpRegionCombo.SelectedValueChanged += new System.EventHandler(this.OpRegionCombo_SelectedValueChanged);
            CountryBox.SelectedValueChanged += new EventHandler(CountryBoxChanged);
            ProvBox.SelectedValueChanged +=new EventHandler(ProvBoxChanged);
            CityBox.SelectedValueChanged += new EventHandler(CityBoxChanged);
            DeleteRegionBtn.Click += new EventHandler(my_controller.DeleteRegionBtn_Click);
            AddRegionBtn.Click += new EventHandler(my_controller.AddRegionBtn_Click);
            SaveBtn.Click += new EventHandler(my_controller.HQSaveBtn_Click);
            #endregion
            noChanges = true;
        }
/********************************** Gets **********************************/
        public string GetSelectedRegion()
        {
            return OpRegionCombo.SelectedValue.ToString();
        }
        public string[] getNewRegion()
        {
            String[] vals = new String[5];
            vals[0] = null;
            vals[1] = TitleTxtBox.Text.ToString();
            vals[2] = CountryBox.SelectedValue.ToString();
            vals[3] = ProvBox.SelectedValue.ToString();
            vals[4] = CityBox.SelectedValue.ToString();

            return vals;
        }
        public DataTable GetFranchiseView()
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn c in FranchiseGridView.Columns)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = c.Name;
                dt.Columns.Add(dc);
            }

            foreach (DataGridViewRow r in FranchiseGridView.Rows)
            {
                DataRow drow = dt.NewRow();
                foreach (DataGridViewCell cell in r.Cells)
                {
                    drow[cell.OwningColumn.Name] = cell.Value;
                }
                dt.Rows.Add(drow);
            }
            return dt;
        }

/********************************** Sets **********************************/
        public void SetLoc(Point loc)
        {
            this.Location = new Point(loc.X, loc.Y - (this.Height / 2));
        }
        public void LoadOpRegions()
        {
            DataTable dt = MOpReg.GetRegions();
            DataTable regs = new DataTable();
            regs.Columns.Add("ID", typeof(string));
            regs.Columns.Add("Display", typeof(string));

            regs.Rows.Add(new String[] { "NULL", "<All>" });
            foreach (DataRow r in dt.Rows)
            {
                regs.Rows.Add(r["zone_id"].ToString(), r["zone_Title"].ToString() + " ( "
                                                     + MCity.GetName(r["city_id"].ToString()) + ", "
                                                     + MProvState.GetName(r["province_id"].ToString()) + ", "
                                                     + MCountry.GetName(r["country_id"].ToString()) + ")");
            }

            OpRegionCombo.DataSource = regs;
            OpRegionCombo.ValueMember = regs.Columns["ID"].ToString();
            OpRegionCombo.DisplayMember = regs.Columns["Display"].ToString();
            OpRegionCombo.SelectedValue = "NULL";
        }
        public void LoadFranchiseeGridView()
        {
            DataTable dt = MFranchise.GetAllFs(OpRegionCombo.SelectedValue.ToString());

            int n = dt.Rows.Count;            
            for (int i = 0; i < n; i++)
            {
                FranchiseGridView.Rows.Add();
                for (int j = 0; j < 6; j++)
                {
                    FranchiseGridView.Rows[i].Cells[j].Value = dt.Rows[i][j];
                }

                var hq = FranchiseGridView.Rows[i].Cells["isHQ"] as DataGridViewCheckBoxCell;
                if (string.Equals(dt.Rows[i]["isHQ"].ToString(), "true", StringComparison.OrdinalIgnoreCase))
                {

                    hq.Value = true;
                    hq.ReadOnly = true;
                }
                else
                    hq.Value = false;
            }
            CheckFranchiseBtn();
        }
        public void InitializeCombos(String countryID, String ProvID, String CityID)
        {
            DataTable country = MCountry.GetList();
            DataTable province;
            DataTable city;

            if (String.Equals(countryID, "noChange"))
                province = MProvState.GetFilteredList(CountryBox.SelectedValue.ToString());
            else
            {
                CountryBox.SelectedValueChanged -= CountryBoxChanged;
                CountryBox.DataSource = country;
                CountryBox.DisplayMember = country.Columns[1].ToString();
                CountryBox.ValueMember = country.Columns[0].ToString();
                province = MProvState.GetFilteredList(countryID);
                CountryBox.SelectedValueChanged += new EventHandler(CountryBoxChanged);
            }


            if (String.Equals(ProvID, "noChange"))
                city = MCity.GetFilteredList(ProvBox.SelectedValue.ToString());
            else
            {
                ProvBox.SelectedValueChanged -= ProvBoxChanged;
                ProvBox.DataSource = province;
                ProvBox.DisplayMember = province.Columns[1].ToString();
                ProvBox.ValueMember = province.Columns[0].ToString();
                city = MCity.GetFilteredList(ProvID);
                ProvBox.SelectedValueChanged += new EventHandler(ProvBoxChanged);
            }


            CityBox.DataSource = city;
            CityBox.DisplayMember = city.Columns[1].ToString();
            CityBox.ValueMember = city.Columns[0].ToString();

        }
/********************************** Events **********************************/
        private void OpRegionCombo_SelectedValueChanged(object sender, EventArgs e)
        {
            FranchiseGridView.Rows.Clear();
            LoadFranchiseeGridView();
        }
        private void CountryBoxChanged(object sender, EventArgs e)
        {
            InitializeCombos("noChange", ProvBox.SelectedValue.ToString(), CityBox.SelectedValue.ToString());
            noChanges = false;
        }
        private void ProvBoxChanged(object sendr, EventArgs e)
        {
            InitializeCombos("noChange", "noChange", CityBox.SelectedValue.ToString());
            noChanges = false;
        }
        private void CityBoxChanged(object sendr, EventArgs e)
        {
            noChanges = false;
        }

        private void addFranButton_Click(object sender, EventArgs e)
        {
            FranchiseGridView.Rows.Add();
            FranchiseGridView.Rows[0].Cells["idCol"].Value = my_controller.GenerateFranchiseID();
            FranchiseGridView.Rows[0].Cells["zoneID"].Value = OpRegionCombo.SelectedValue.ToString();
            CheckFranchiseBtn();
        }
/********************************** Other **********************************/
        public void CheckFranchiseBtn()
        {
            int n = FranchiseGridView.Rows.Count;
            if (n == 0)
                addFranButton.Visible = true;
            else
                addFranButton.Visible = false;
        }

    }
}
