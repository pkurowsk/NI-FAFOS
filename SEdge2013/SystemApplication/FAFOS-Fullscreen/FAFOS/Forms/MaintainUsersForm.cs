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
    public partial class MaintainUsersForm : Form
    {
        UserController my_controller;
        int UserID, picID;
        public bool noChanges;
/************************************* Contructor *****************************/
        public MaintainUsersForm(int idOfUser, int ProPicID)
        {
            InitializeComponent();
            UserID = idOfUser;
            picID = ProPicID;


            this.Back_Button.MouseEnter += new EventHandler(button1_MouseEnter);
            this.Back_Button.MouseLeave += new EventHandler(button1_MouseLeave);
            this.Back_Button.Location = new Point(15, 15);
            Back_Button.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Back2));
            Back_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            Back_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;



            //this.Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            my_controller = new UserController(this,idOfUser,picID);

            #region Handlers
            this.Back_Button.Click += new EventHandler(my_controller.back_Button);
            this.UploadPicButton.Click += new EventHandler(my_controller.UploadPic_click);
            this.AdminButton.Click +=new EventHandler(my_controller.AdminButton_Click);
            this.HQButton.Click +=new EventHandler(my_controller.HQButton_Click);
            this.CountryBox.SelectedValueChanged += new EventHandler(CountryBoxChanged);
            this.ProvStateBox.SelectedValueChanged += new EventHandler(ProvBoxChanged);
            this.SaveButton.Click +=new EventHandler(my_controller.SaveButton_Click);
            this.LocationChanged += new EventHandler(my_controller.LocationChanged);
            
            foreach (Control c in this.Controls)
                c.TextChanged += new EventHandler(somethingChanged);
            #endregion

            if (UserID == 1) primUsrLabel.Visible = true;
            noChanges = true;

        }
/************************************* Sets *****************************/
        public void setPic(Image pic)
        {
            this.PicBox.BackgroundImage = pic;
            this.PicBox.BackgroundImageLayout = ImageLayout.Stretch;
        }
        public void SetButtons(bool admin, bool hq)
        {
            if (admin)
            {
                this.AdminButton.Visible = true;
            }
            if (hq)
            {
                this.HQButton.Visible = true;
            }
        }
        public void SetFields(String[] values)
        {
            fNameTxtBox.Text = MUser.ConBlank(values[1]);
            lNameTxtBox.Text = MUser.ConBlank(values[2]);
            mNameTxtBox.Text = MUser.ConBlank(values[3]);
            usernameTextBox.Text = MUser.ConBlank(values[4]);
            PassTxtBox.Text = values[5];
            cnfTxtBox.Text = values[5];
            addrTextBox.Text = MUser.ConBlank(values[6]);
            postalCodeTextBox.Text = MUser.ConBlank(values[7]);
            homeTxtBox.Text = MUser.ConBlank(values[8]);
            cellTextBox.Text = MUser.ConBlank(values[9]);
            FaxTextBox.Text = MUser.ConBlank(values[10]);
            EmailTextBox.Text = MUser.ConBlank(values[11]);
            SkypeBox.Text = MUser.ConBlank(values[12]);
            PositionTxBox.Text = MUser.ConBlank(values[13]);
            bool adminUsr = false, hqUsr = false;
            if (String.Equals(values[14], "True",StringComparison.OrdinalIgnoreCase))
                hqUsr = true;
            if (String.Equals(values[15], "True", StringComparison.OrdinalIgnoreCase))
                adminUsr = true;
            SetButtons(adminUsr, hqUsr);
            CountryBox.SelectedValue = values[19];
            ProvStateBox.SelectedValue = values[17];
            CityBox.SelectedValue = values[18];
            
        }
        void button1_MouseLeave(object sender, EventArgs e)
        {
            this.Back_Button.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Back2));
            this.Back_Button.Location = new Point(15, 15);
            this.Back_Button.Size = new Size(45, 45);
            this.Back_Button.ImageAlign = ContentAlignment.MiddleCenter;
        }

        void button1_MouseEnter(object sender, EventArgs e)
        {
            this.Back_Button.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.BackOver));
            this.Back_Button.Location = new Point(15, 15);
            this.Back_Button.Size = new Size(45, 45);
            this.Back_Button.ImageAlign = ContentAlignment.MiddleCenter;
        }
        public void InitializeCombos(String countryID, String ProvID, String CityID)
        {
            DataTable country = MCountry.GetList();
            DataTable province;
            DataTable city;

            if(String.Equals(countryID,"noChange"))
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
                

            if(String.Equals(ProvID,"noChange"))
                city = MCity.GetFilteredList(ProvStateBox.SelectedValue.ToString());
            else
            {
                ProvStateBox.SelectedValueChanged -= ProvBoxChanged;
                ProvStateBox.DataSource = province;
                ProvStateBox.DisplayMember = province.Columns[1].ToString();
                ProvStateBox.ValueMember = province.Columns[0].ToString();
                city = MCity.GetFilteredList(ProvID);
                ProvStateBox.SelectedValueChanged += new EventHandler(ProvBoxChanged);            
            }          
                

            CityBox.DataSource = city;
            CityBox.DisplayMember = city.Columns[1].ToString();
            CityBox.ValueMember = city.Columns[0].ToString();

        }
        public void ToggleAdminButton()
        {
            if (AdminButton.BackColor == Color.FromArgb(250, 23, 49))
            {
                AdminButton.BackColor = Color.Black;
                return;
            }

            if (AdminButton.BackColor == Color.Black)
            {
                AdminButton.BackColor = Color.FromArgb(250, 23, 49);
                return;
            }
        }
        public void ToggleHQButton()
        {
            if (HQButton.BackColor == Color.FromArgb(250, 23, 49))
            {
                HQButton.BackColor = Color.Black;
                return;
            }
            if (HQButton.BackColor == Color.Black)
            {
                HQButton.BackColor = Color.FromArgb(250, 23, 49);
                return;
            }
        }
/************************************* Gets *****************************/
        public String[] GetFields()
        {
            String[] values = new String[22];
            values[0] = UserID.ToString();
            values[1] = fNameTxtBox.Text.ToString();
            values[2] = lNameTxtBox.Text.ToString();
            values[3] = mNameTxtBox.Text.ToString();
            values[4] = usernameTextBox.Text.ToString();
            values[5] = PassTxtBox.Text.ToString();
            values[6] = addrTextBox.Text.ToString();
            values[7] = postalCodeTextBox.Text.ToString();
            values[8] = homeTxtBox.Text.ToString();
            values[9] = cellTextBox.Text.ToString();
            values[10] = FaxTextBox.Text.ToString();
            values[11] = EmailTextBox.Text.ToString();
            values[12] = SkypeBox.Text.ToString();
            values[13] = PositionTxBox.Text.ToString();
            //     14 HQ User
            //     15 Admin
            //     16 percentage Ownership
            values[19] = CountryBox.SelectedValue.ToString();
            values[17] = ProvStateBox.SelectedValue.ToString();
            values[18] = CityBox.SelectedValue.ToString();
            values[20] = Properties.Settings.Default.FranchiseeID;
            values[21] = picID.ToString();
            return values;
        }
        public bool ValidToSet()
        {
            bool isgood = true;
            if (!String.Equals(PassTxtBox.Text.ToString(), cnfTxtBox.Text.ToString()))
            { isgood = false; errProv.SetError(cnfTxtBox, "Passwords Must Match!");}
            if (String.Equals(PassTxtBox.Text.ToString(), ""))
            { isgood = false; errProv.SetError(PassTxtBox, "Password cannot be blank!"); }

            return isgood;
        }
        public Point GetWindowMidRight()
        {
            Point p = this.Location;
            Size s = this.Size;

            return new Point(p.X + s.Width,p.Y + (s.Height/2));
        }
/************************************* Events *****************************/
        private void PassTxtBox_TextChanged(object sender, EventArgs e)
        {

            cnfTxtBox.Visible = true;            
            if (PassTxtBox.Text == cnfTxtBox.Text)
            {
                cnfTxtBox.Visible = false;
                confirmLabel.Text = "Password Matches";
            }
            else
            {
                confirmLabel.Visible = true;
                confirmLabel.Text = "Confirm:";
            }

            if (PassTxtBox.Text == "")
            {
                PassLabel.Text = "Password: Cannot be blank!";
                cnfTxtBox.Visible = false;
                confirmLabel.Visible = false;

            }
            else
                PassLabel.Text = "Password:";

        }
        private void cnfTxtBox_TextChanged(object sender, EventArgs e)
        {
            if (PassTxtBox.Text == cnfTxtBox.Text)
            {
                cnfTxtBox.Visible = false;
                confirmLabel.Text = "Password Matches";
            }
            else
            {
                confirmLabel.Visible = true;
                confirmLabel.Text = "Confirm:";
            }
        }
        private void CountryBoxChanged(object sender, EventArgs e)
        {
            InitializeCombos("noChange", ProvStateBox.SelectedValue.ToString(), CityBox.SelectedValue.ToString());
        }
        private void ProvBoxChanged(object sendr, EventArgs e)
        {
            InitializeCombos("noChange", "noChange", CityBox.SelectedValue.ToString());
        }
        private void somethingChanged(object sender, EventArgs e)
        {
            noChanges = false;
            errProv.Clear();
        }
    }
}
