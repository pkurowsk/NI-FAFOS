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
    public partial class AddEditClientForm : FAFOS.Background
    {
        MaintainClientController my_controller;
        bool isEdit;
        public bool noChanges;
        ComboBox nameComboBox;
        int userid;
        Users user;

        public AddEditClientForm(MaintainClientController parent, bool edit, int id)
        {
            InitializeComponent();
            my_controller = parent;
            isEdit = edit;
            userid = id;
            noChanges = true;

            #region Form Event Handlers
            
           /* this.Back_Button.Click += new System.EventHandler(my_controller.Client_Cancel_Button_Click);
            this.Back_Button.MouseEnter += new EventHandler(button1_MouseEnter);
            this.Back_Button.MouseLeave += new EventHandler(button1_MouseLeave);
            this.Back_Button.Location = new Point(65, 38);
            this.Back_Button.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Back2));
            Back_Button.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            Back_Button.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            */
            this.contract_Button.Click += new EventHandler(my_controller.Client_Contract_Button_Click);
            this.Ok_Button.Click += new System.EventHandler(my_controller.Client_Ok_Button_Click);
           // this.Cancel_Button.Click += new System.EventHandler(my_controller.Client_Cancel_Button_Click);
            this.delete_Button.Click += new EventHandler(my_controller.Client_Delete_Button_Click);
            this.CountryBox.SelectedValueChanged += new System.EventHandler(my_controller.Country_Changed);
            this.ProvStateBox.SelectedValueChanged += new EventHandler(my_controller.Province_Changed);
            this.CityBox.SelectedValueChanged += new EventHandler(my_controller.City_Changed);

            this.nameTxtBox.TextChanged += new EventHandler(my_controller.Client_Text_Changed);
            this.typeTextBox.TextChanged += new EventHandler(my_controller.Client_Text_Changed);
            this.addrTextBox.TextChanged += new EventHandler(my_controller.Client_Text_Changed);
            this.postalCodeTextBox.TextChanged += new EventHandler(my_controller.Client_Text_Changed);
            this.PoBoxTextBox.TextChanged += new EventHandler(my_controller.Client_Text_Changed);
            this.EmailTextBox.TextChanged += new EventHandler(my_controller.Client_Text_Changed);
            this.mainPhoneTxtBox.TextChanged += new EventHandler(my_controller.Client_Text_Changed);
            this.SecondPhoneTextBox.TextChanged += new EventHandler(my_controller.Client_Text_Changed);
            this.FaxTextBox.TextChanged += new EventHandler(my_controller.Client_Text_Changed);
            this.PrimContactTextBox.TextChanged += new EventHandler(my_controller.Client_Text_Changed);
            #endregion

            if (isEdit)
            {
                #region Create and set a combo selection box
                DataTable clients = MClient.GetList();
                nameComboBox = new ComboBox();
                nameComboBox.Location = new Point(490, 192);
                nameComboBox.Font = new Font(nameComboBox.Font.FontFamily, 10);
                nameComboBox.Size = new Size(195, 28);

                DataTable dt = new DataTable();
                dt = clients.Clone();
                dt.Rows.Add(new String[] { null, "<Select Client>" });
                foreach (DataRow r in clients.Rows)
                    dt.ImportRow(r);

                nameComboBox.DataSource = dt;
                nameComboBox.DisplayMember = dt.Columns[1].ToString();                
                nameComboBox.ValueMember = dt.Columns[0].ToString();

                nameComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                nameComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                for (int i = 0; i < clients.Rows.Count; i++)
                    nameComboBox.AutoCompleteCustomSource.Add(clients.Rows[i][1].ToString());

                nameComboBox.TextChanged += new EventHandler(my_controller.Client_Text_Changed);
                nameComboBox.SelectedValueChanged += new EventHandler(my_controller.Edit_Client_Populate);
                this.Controls.Add(nameComboBox);

                Label nameSelection = new Label();
                nameSelection.Text = "Select Client:";
                nameSelection.Font = new Font(nameComboBox.Font.FontFamily, 10);
                nameSelection.Location = new Point(490, 166);
                this.Controls.Add(nameSelection);
                #endregion
            }

        here: try { SetCountryBox(MCountry.GetList(), "0"); }
            catch (Exception)
            {
                if (MessageBox.Show("Error connecting to Database\nDo you want to retry?", "Connection Problems", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    goto here;
            }

            //User label
            user = new Users();
            userid = id;
            setup(userid.ToString(), "FAFOS Client Form");

        }

        public void setFields(String[] values)
        {

            if (isEdit)
                nameComboBox.Text = values[1];

            this.nameTxtBox.Text = values[1];

            this.typeTextBox.Text = values[2];

            this.addrTextBox.Text = values[3];
            this.postalCodeTextBox.Text = values[4];
            this.mainPhoneTxtBox.Text = values[5];
            this.SecondPhoneTextBox.Text = values[6];
            this.FaxTextBox.Text = values[7];
            this.EmailTextBox.Text = values[8];
            this.PoBoxTextBox.Text = values[9];
            this.PrimContactTextBox.Text = values[10];
            
            if (values[14] == "")
                contract_Button.Text = "<Click to Add>";
            else{
                try { contract_Button.Text = MClientContract.GetName(values[14]); }
                catch (Exception) { contract_Button.Text = "Error"; }
                }
            noChanges = true;

        }

        public void SetCountryBox(DataTable country, String countryID)
        {
            this.CountryBox.DataSource = country;
            this.CountryBox.DisplayMember = country.Columns[1].ToString();
            this.CountryBox.ValueMember = country.Columns[0].ToString();
            this.CountryBox.SelectedValue = "-1";
            this.CountryBox.SelectedValue = countryID;
        }
        public void SetProvStateBox(DataTable provState)
        {
            this.ProvStateBox.DataSource = provState;
            this.ProvStateBox.DisplayMember = provState.Columns[1].ToString();
            this.ProvStateBox.ValueMember = provState.Columns[0].ToString();            
        }
        public void SetProvStateBox(String provID)
        {
            this.ProvStateBox.SelectedValue = "-1";            
            this.ProvStateBox.SelectedValue = provID;
        }
        public void SetCityBox( DataTable city)
        {
            this.CityBox.DataSource = city;
            this.CityBox.DisplayMember = city.Columns[1].ToString();
            this.CityBox.ValueMember = city.Columns[0].ToString();           
        }
        public void SetCityBox(String cityID)
        {
            this.CityBox.SelectedValue = "-1";
            this.CityBox.SelectedValue = cityID;
        }

        public String GetCountryBox()
        {
            if (this.CountryBox.SelectedValue != null)
                return this.CountryBox.SelectedValue.ToString();
            else return null;
        }
        public String GetProvStateBox()
        {
            if(this.ProvStateBox.SelectedValue != null)
                return this.ProvStateBox.SelectedValue.ToString();
            else return null;
        }
        public String GetCityBox()
        {
            if(this.CityBox.SelectedValue != null) 
                return this.CityBox.SelectedValue.ToString();
            else return null;
        }
        public String GetName()
        {
            return nameTxtBox.Text;
        }

        public String[] GetInputs()
        {
            String[] allFeilds = new String[15];

            allFeilds[1] = nameTxtBox.Text.ToString();
            allFeilds[2] = this.typeTextBox.Text.ToString();
            allFeilds[3] = this.addrTextBox.Text.ToString();
            allFeilds[4] = this.postalCodeTextBox.Text.ToString();
            allFeilds[5] = this.mainPhoneTxtBox.Text.ToString();
            allFeilds[6] = this.SecondPhoneTextBox.Text.ToString();
            allFeilds[7] = this.FaxTextBox.Text.ToString();
            allFeilds[8] = this.EmailTextBox.Text.ToString();
            allFeilds[9] = this.PoBoxTextBox.Text.ToString();
            allFeilds[10] = this.PrimContactTextBox.Text.ToString();
            if (this.CountryBox.SelectedValue != null)
                allFeilds[11] = this.CountryBox.SelectedValue.ToString();
            if (this.ProvStateBox.SelectedValue != null)
                allFeilds[12] = this.ProvStateBox.SelectedValue.ToString();
            if (this.CityBox.SelectedValue != null)
                allFeilds[13] = this.CityBox.SelectedValue.ToString();          
            
            return allFeilds;
        }
        public void SetError(String name, String message)
        {
            switch (name)
            {
               /* case "accountName"://Account name
                    ErrProvider.SetError(nameComboBox, message);
                    return;*/
                case "type"://Type
                    ErrProvider.SetError(typeTextBox, message);
                    return;
                case "address"://Address
                    ErrProvider.SetError(addrTextBox, message);
                    return;
                case "city"://City
                    ErrProvider.SetError(CityBox, message);
                    return;
                case "province"://Province or State
                    ErrProvider.SetError(ProvStateBox, message);
                    return;
                case "country"://Country
                    ErrProvider.SetError(CountryBox, message);
                    return;
                case "postalCode"://Postal Code
                    ErrProvider.SetError(postalCodeTextBox, message);
                    return;
                case "mainPhone"://Main phone number
                    ErrProvider.SetError(mainPhoneTxtBox, message);
                    return;
                case "secondPhone"://Secondary phone
                    ErrProvider.SetError(SecondPhoneTextBox, message);
                    return;
                case "fax"://Fax Number
                    ErrProvider.SetError(FaxTextBox, message);
                    return;
                case "email"://Email
                    ErrProvider.SetError(EmailTextBox, message);
                    return;
                case "poBox"://Post Office Box
                     ErrProvider.SetError(EmailTextBox, message);
                    return;
                case "primaryContact"://Primary Contact Name
                    ErrProvider.SetError(PrimContactTextBox, message);
                    return;                    

                default:
                    return;
            }
        }
        public void ClearErrors() { ErrProvider.Clear(); return; }

        public String GetSelectedClient()
        {
            if (isEdit)
            {
                if (this.nameComboBox.SelectedValue != null)
                    return this.nameComboBox.SelectedValue.ToString();
                else
                    return null;
            }
            else
            {
                if (this.nameTxtBox.Text != null)
                    return this.nameTxtBox.Text.ToString();
                else
                    return null;
            }
        }

        public void DisableContract(String Contract)
        {
            this.contract_Button.Enabled = false;
            this.contract_Button.Text = Contract;
        }
        public void SetContractButton(String ContractName)
        {
            contract_Button.Text = ContractName;
        }

       /* void button1_MouseLeave(object sender, EventArgs e)
        {
            this.Back_Button.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Back2));
            this.Back_Button.Location = new Point(65, 38);
            this.Back_Button.Size = new Size(84, 78);
            this.Back_Button.ImageAlign = ContentAlignment.MiddleCenter;
        }
        void button1_MouseEnter(object sender, EventArgs e)
        {
            this.Back_Button.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.BackOver));
            this.Back_Button.Location = new Point(65, 38);
            this.Back_Button.Size = new Size(84, 78);
            this.Back_Button.ImageAlign = ContentAlignment.MiddleCenter;
        }*/

    }
}
