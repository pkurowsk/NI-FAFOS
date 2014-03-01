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
    public partial class AdminUserForm : Form
    {
        public bool noChanges;
        UserController my_controller;
/********************************** Constructor **********************************/
        public AdminUserForm(UserController parent, Point loc)
        {            
            InitializeComponent();
            SetLoc(loc);
            my_controller = parent;
            SetFields(MFranchise.GetThis());
            if (string.Equals(MFranchise.isFranchisor(), "true", StringComparison.OrdinalIgnoreCase))
            {
                hqCol.Visible = true;
                UserGridView.Width += hqCol.Width;
                this.Width += hqCol.Width;
                addUserButton.Location = new Point(addUserButton.Location.X + hqCol.Width, addUserButton.Location.Y);
                BAddrGridView.Location = new Point(BAddrGridView.Location.X + hqCol.Width, BAddrGridView.Location.Y);
                AddAddrButton.Location = new Point(AddAddrButton.Location.X + hqCol.Width, AddAddrButton.Location.Y);
                label6.Location = new Point(label6.Location.X + hqCol.Width, label6.Location.Y);
            }
            UserGridView.CellContentClick += new DataGridViewCellEventHandler(my_controller.UserGridViewClick);
            BAddrGridView.CellContentClick += new DataGridViewCellEventHandler(my_controller.BAddrGridViewClick);
            SaveBtn.Click +=new EventHandler(my_controller.SaveBtn_Click);
            foreach (Control c in this.Controls)
            {
                try { c.TextChanged += new EventHandler(somethingChanged); }
                catch (Exception) { }
            }
            noChanges = true;
        }
/********************************** Gets **********************************/
        public String[] getFields()
        {
            String[] vals = new String[5];
            vals[0] = Properties.Settings.Default.FranchiseeID;
            vals[1] = CompanyNameTxtBox.Text.ToString();
            vals[2] = TaxNumBox.Text.ToString();
            vals[3] = BusinessNumTxtBox.Text.ToString();
            vals[4] = FiscalPicker.Value.Date.ToString();
            return vals;
        }
        public DataTable GetUserView()
        {
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn c in UserGridView.Columns)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = c.Name;
                dt.Columns.Add(dc);
            }

            foreach (DataGridViewRow r in UserGridView.Rows)
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
        public DataTable GetBAddrView()
        {
            
            DataTable dt = new DataTable();
            foreach (DataGridViewColumn c in BAddrGridView.Columns)
            {
                DataColumn dc = new DataColumn();
                dc.ColumnName = c.Name;
                dt.Columns.Add(dc);
            }

            foreach (DataGridViewRow r in BAddrGridView.Rows)
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
        public void SetFields(String[] vals)
        {
            CompanyNameTxtBox.Text = vals[1];
            TaxNumBox.Text = vals[2];
            BusinessNumTxtBox.Text = vals[3];
            try { FiscalPicker.Value = Convert.ToDateTime(vals[4]); }
            catch (FormatException) { }
        }
        public void PopulateUserGridView(DataTable users)
        {
            int n = users.Rows.Count, index;
            DataGridViewCheckBoxCell admin;
            DataGridViewCheckBoxCell hq;
            bool lockRow;
            for (int i = 0; i < n; i++)
            {
                lockRow = false;
                admin = null;
                hq = null;
                index = UserGridView.Rows.Add();
                UserGridView.Rows[index].Cells["usrIDCol"].Value = users.Rows[i]["user_id"];
                UserGridView.Rows[index].Cells["usrName"].Value = users.Rows[i]["username"];
                UserGridView.Rows[index].Cells["passSetCol"].Value = users.Rows[i]["password"];
                UserGridView.Rows[index].Cells["fName"].Value = MUser.ConBlank(users.Rows[i]["first_nm"].ToString());
                UserGridView.Rows[index].Cells["lName"].Value =  MUser.ConBlank(users.Rows[i]["last_nm"].ToString());
                UserGridView.Rows[index].Cells["mName"].Value =  MUser.ConBlank(users.Rows[i]["middle_nm"].ToString());
                UserGridView.Rows[index].Cells["PerOwnCol"].Value = users.Rows[i]["percentage_ownership"];

                admin = UserGridView.Rows[index].Cells["adminCol"] as DataGridViewCheckBoxCell;
                if (string.Equals(users.Rows[i]["admin_user"].ToString(), "true", StringComparison.OrdinalIgnoreCase))
                {   admin.Value = true; lockRow = true; }
                if (string.Equals(users.Rows[i]["admin_user"].ToString(), "false", StringComparison.OrdinalIgnoreCase))
                    admin.Value = false;
                hq = UserGridView.Rows[index].Cells["hqCol"] as DataGridViewCheckBoxCell;
                if (string.Equals(users.Rows[i]["hq_user"].ToString(), "true", StringComparison.OrdinalIgnoreCase))
                {   hq.Value = true; lockRow = true; }
                if (string.Equals(users.Rows[i]["hq_user"].ToString(), "false", StringComparison.OrdinalIgnoreCase))
                    hq.Value = false;

                if (lockRow)
                    UserGridView.Rows[index].ReadOnly = true;
            }
        }
        public void PopulateBAddrsGridView(DataTable Addrs)
        {
            DataGridViewComboBoxCell country;
            DataTable co = MCountry.GetList();
            DataGridViewComboBoxCell province;
            DataTable pr;
            DataGridViewComboBoxCell city;
            DataTable ci;
            int n = Addrs.Rows.Count, index;
            for (int i = 0; i < n; i++)
            {
                index = BAddrGridView.Rows.Add();
                BAddrGridView.Rows[index].Cells["locID"].Value = Addrs.Rows[i]["location_id"];
                BAddrGridView.Rows[index].Cells["bAddr"].Value = Addrs.Rows[i]["address"];
                BAddrGridView.Rows[index].Cells["bPostal"].Value = Addrs.Rows[i]["postal_code"];

                country = BAddrGridView.Rows[index].Cells["bCountry"] as DataGridViewComboBoxCell;
                country.DataSource = co;
                country.DisplayMember = co.Columns[1].ToString();
                country.ValueMember = co.Columns[0].ToString();
                country.Value = Addrs.Rows[i]["country_id"];

                province = BAddrGridView.Rows[index].Cells["bProvince"] as DataGridViewComboBoxCell;
                pr = MProvState.GetFilteredList(Addrs.Rows[i]["country_id"].ToString());
                province.DataSource = pr;
                province.DisplayMember = pr.Columns[1].ToString();
                province.ValueMember = pr.Columns[0].ToString();
                province.Value = Addrs.Rows[i]["province_id"];

                city = BAddrGridView.Rows[index].Cells["bCity"] as DataGridViewComboBoxCell;
                ci = MCity.GetFilteredList(Addrs.Rows[i]["province_id"].ToString());
                city.DataSource = ci;
                city.DisplayMember = ci.Columns[1].ToString();
                city.ValueMember = ci.Columns[0].ToString();
                city.Value = Addrs.Rows[i]["city_id"];
            }
        }
        public void SetLoc(Point loc)
        {
            this.Location = new Point(loc.X, loc.Y - (this.Height / 2));
        }
/********************************** Events **********************************/
        private void addUserButton_Click(object sender, EventArgs e)
        {
            int i = UserGridView.Rows.Add();
            string newpass = my_controller.GeneratePassword();
            MessageBox.Show("New User temporary password: " + newpass);
            UserGridView.Rows[i].Cells["passSetCol"].Value = newpass;
            UserGridView.Rows[i].Cells["PerOwnCol"].Value = "0";
            UserGridView.Rows[i].Cells["adminCol"].Value = "False";
            UserGridView.Rows[i].Cells["hqCol"].Value = "False";

        }
        private void AddAddrButton_Click(object sender, EventArgs e)
        {
            int i = BAddrGridView.Rows.Add();
            DataGridViewComboBoxCell country = BAddrGridView.Rows[i].Cells["bCountry"] as DataGridViewComboBoxCell;
            DataTable co = MCountry.GetList();
            country.DataSource = co;
            country.DisplayMember = co.Columns[1].ToString();
            country.ValueMember = co.Columns[0].ToString();

            DataGridViewComboBoxCell province = BAddrGridView.Rows[i].Cells["bProvince"] as DataGridViewComboBoxCell;
            DataTable pr = MProvState.GetFilteredList("1");
            province.DataSource = pr;
            province.DisplayMember = pr.Columns[1].ToString();
            province.ValueMember = pr.Columns[0].ToString();

            DataGridViewComboBoxCell city = BAddrGridView.Rows[i].Cells["bCity"] as DataGridViewComboBoxCell;
            DataTable ci = MCity.GetFilteredList("1");
            city.DataSource = ci;
            city.DisplayMember = ci.Columns[1].ToString();
            city.ValueMember = ci.Columns[0].ToString();
        }
        private void BAddrGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            BAddrGridView.CellValueChanged -= BAddrGridView_CellValueChanged;
            if ((e.ColumnIndex == 3) && (e.RowIndex > -1))//--------------------------------Country
            {
                DataGridViewComboBoxCell province = BAddrGridView.Rows[e.RowIndex].Cells["bProvince"] as DataGridViewComboBoxCell;
                DataTable pr = MProvState.GetFilteredList(BAddrGridView.Rows[e.RowIndex].Cells["bcountry"].Value.ToString());
                province.DataSource = pr;
                province.DisplayMember = pr.Columns[1].ToString();
                province.ValueMember = pr.Columns[0].ToString();
                noChanges = false;
                
            }
            if ((e.ColumnIndex == 4) && (e.RowIndex > -1))//--------------------------------Province
            {
                DataGridViewComboBoxCell city = BAddrGridView.Rows[e.RowIndex].Cells["bCity"] as DataGridViewComboBoxCell;
                DataTable ci = MCity.GetFilteredList(BAddrGridView.Rows[e.RowIndex].Cells["bProvince"].Value.ToString());
                city.DataSource = ci;
                city.DisplayMember = ci.Columns[1].ToString();
                city.ValueMember = ci.Columns[0].ToString();
                noChanges = false;
            }
            if ((e.ColumnIndex == 5) && (e.RowIndex > -1))//--------------------------------City
            {
                noChanges = false;
            }
            else
                noChanges = false;

            BAddrGridView.CellValueChanged += new DataGridViewCellEventHandler(BAddrGridView_CellValueChanged);
        }
        private void UserGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            noChanges = false;
        }
        private void somethingChanged(object sender, EventArgs e)
        {
            noChanges = false;
        }

        private void BAddrGridView_DataError(object sender, DataGridViewDataErrorEventArgs e) {}
/********************************** Other **********************************/
        public bool isOkToClose()
        {
            return true;
        }
        public String SwapBlank(String s)
        {
            if (String.Equals(s, ""))
                return "NULL";
            else
                return s;
        }
    }
}
