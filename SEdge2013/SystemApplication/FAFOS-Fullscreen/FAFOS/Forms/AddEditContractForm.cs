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
    public partial class AddEditContractForm : FAFOS.Background
    {
        MaintainClientController my_controller;
        bool isEdit; 
        public bool noChanges;
        ComboBox nameComboBox;
        int userid;
        String contractID;
        Users user;
        /********************************* Constructor *******************************/
        public AddEditContractForm(MaintainClientController parent,bool edit, int id,String ctID)
        {
            InitializeComponent();
            my_controller = parent;
            isEdit = edit;
            userid = id;
            contractID = ctID;
            noChanges = true;

            #region Handlers


            this.Ok_Button.Click += new EventHandler(my_controller.Contract_Ok_Button_Click);
            this.FormClosing += new FormClosingEventHandler(my_controller.Contract_Closing);
            this.delete_Button.Click += new EventHandler(my_controller.Contract_Delete_Button_Click);

            this.ServiceAddrGridView.CellContentClick += new DataGridViewCellEventHandler(my_controller.Contract_Cell_Click);
            this.ServiceAddrGridView.CellValueChanged += new DataGridViewCellEventHandler(ServiceAddrGridView_CellValueChanged);
            this.ServiceAddrGridView.RowsAdded += new DataGridViewRowsAddedEventHandler(ServiceAddrGridView_RowsAdded);

            this.linkableClientBox.SelectedValueChanged += new EventHandler(my_controller.Contract_ClientBox_Select_Changed);
            this.TermsBox.TextChanged += new EventHandler(my_controller.Contract_Text_Changed);
            this.StartDatePicker.ValueChanged += new EventHandler(my_controller.Contract_Text_Changed);
            this.EndDatePicker.ValueChanged += new EventHandler(my_controller.Contract_Text_Changed);
            this.contractNameBox.TextChanged += new EventHandler(my_controller.Contract_Text_Changed);
            #endregion

            if (isEdit)
            {
                #region Set Edit Fields
                DataTable contracts = MClientContract.GetList();
                nameComboBox = new ComboBox();
                nameComboBox.Location = new Point(320, 180);
                nameComboBox.Font = new Font(nameComboBox.Font.FontFamily, 8);
                nameComboBox.Size = new Size(195, 28);
                
                DataTable dt = new DataTable();
                dt = contracts.Clone();
                dt.Rows.Add(new String[]{null,"<Select Contract>"});
                foreach (DataRow r in contracts.Rows)
                    dt.ImportRow(r);

                nameComboBox.DataSource = dt;             
                nameComboBox.DisplayMember = dt.Columns[1].ToString();
                nameComboBox.ValueMember = dt.Columns[0].ToString();

                nameComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                nameComboBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                for (int i = 0; i < contracts.Rows.Count; i++)
                    nameComboBox.AutoCompleteCustomSource.Add(contracts.Rows[i][1].ToString());

                nameComboBox.SelectedValueChanged += new EventHandler(my_controller.Edit_Contract_Populate);
                this.Controls.Add(nameComboBox);
                #endregion
            }
            if (!isEdit)
            {
                ClientOptions(true);
            }

            //User label
            userid = id;
            user = new Users();
            setup(userid.ToString(), "FAFOS Contract Form");
           // lblUserInfo.Text = "Logged in:\n " + user.getName(id);
         //   lblUserInfo.Location = new Point(Screen.PrimaryScreen.Bounds.Right - 100, 10);


        }        
        
        /********************************* Service Address Grid View *****************************/
        private void ServiceAddrGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var c = ServiceAddrGridView.Rows[e.RowIndex].Cells["countryCol"] as DataGridViewComboBoxCell;
            DataTable dt = MCountry.GetList();
            c.DataSource = dt;
            c.DisplayMember = "name";
            c.ValueMember = "id";

            MServiceAddress _srvAddr = new MServiceAddress();
            ServiceAddrGridView.Rows[e.RowIndex].Cells[0].Value = _srvAddr.FindID();            

            ServiceAddrGridView.Rows[e.RowIndex].Cells["editButton"].Value = "0";
            ServiceAddrGridView.Rows[e.RowIndex].Cells["roomButton"].Value = "0";
            ServiceAddrGridView.Rows[e.RowIndex].Cells["num_floors"].Value = "0";
        }

        void ServiceAddrGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 4) && e.RowIndex > -1)//------------------------------CountryBox
            {
                try
                {
                    String countryID = ServiceAddrGridView.Rows[e.RowIndex].Cells["countryCol"].Value.ToString();
                    var provCell = ServiceAddrGridView.Rows[e.RowIndex].Cells["provStateCol"] as DataGridViewComboBoxCell;

                    DataTable provList = MProvState.GetFilteredList(countryID);
                    provCell.DataSource = provList;
                    provCell.DisplayMember = "name";
                    provCell.ValueMember = "id";
                    noChanges = false;
                }
                catch (Exception) { }

            }

            if ((e.ColumnIndex == 5) && e.RowIndex > -1)//-------------------------------- ProvBox
            {
                try
                {
                    String provID = ServiceAddrGridView.Rows[e.RowIndex].Cells["provStateCol"].Value.ToString();
                    var cityCell = ServiceAddrGridView.Rows[e.RowIndex].Cells["cityCol"] as DataGridViewComboBoxCell;
                    DataTable cityList = MCity.GetFilteredList(provID);

                    cityCell.DataSource = cityList;
                    cityCell.DisplayMember = "name";
                    cityCell.ValueMember = "id";
                    noChanges = false;
                }
                catch (Exception) { }
            }

            if ((e.ColumnIndex == 6) && e.RowIndex > -1)//---------------------------------- City
            {
                noChanges = false;
            }
        }
        
        public void SetTableButtonMetrics(int rowIndex, String SrvAddrId)
        {
            try
            {
                ServiceAddrGridView.Rows[rowIndex].Cells["editButton"].Value = MQueryMetrics.GetCount("Contract_services", SrvAddrId, "service_addr_id");
                ServiceAddrGridView.Rows[rowIndex].Cells["roomButton"].Value = MQueryMetrics.GetCount("Room", SrvAddrId, "service_addr_id");
            }
            catch (Exception) { }
        }
        
        public void Add_Old_Row(String[] rowElements)
        {
            int index = ServiceAddrGridView.Rows.Add();
            SetTableButtonMetrics(index, rowElements[0]);

            DataTable country , province, city;
            country = MCountry.GetList();
            province = MProvState.GetFilteredList(rowElements[6]);
            city = MCity.GetFilteredList(rowElements[5]);


            //country.TableName = "country";
            var countryCell = ServiceAddrGridView.Rows[index].Cells["countryCol"] as DataGridViewComboBoxCell;
           //(ServiceAddrGridView.Rows[index].Cells[0].DataPropertyName = "Type";
            countryCell.DataSource = country;
            countryCell.DisplayMember = "name";
            countryCell.ValueMember = "id";

            var provCell = ServiceAddrGridView.Rows[index].Cells["provStateCol"] as DataGridViewComboBoxCell;
            provCell.DataSource = province;
            provCell.DisplayMember = "name";
            provCell.ValueMember = "id";

            var cityCell = ServiceAddrGridView.Rows[index].Cells["cityCol"] as DataGridViewComboBoxCell;
            cityCell.DataSource = city;
            cityCell.DisplayMember = "name";
            cityCell.ValueMember = "id";
            
            ServiceAddrGridView.Rows[index].Cells["idCol"].Value = rowElements[0];
            ServiceAddrGridView.Rows[index].Cells["address_col"].Value = rowElements[1];
            ServiceAddrGridView.Rows[index].Cells["postal_code_col"].Value = rowElements[2];
            ServiceAddrGridView.Rows[index].Cells["on_site_contact_col"].Value = rowElements[3];
            try
            {
                ServiceAddrGridView.Rows[index].Cells["countryCol"].Value = rowElements[4];
                ServiceAddrGridView.Rows[index].Cells["provStateCol"].Value = rowElements[5];
                ServiceAddrGridView.Rows[index].Cells["cityCol"].Value =rowElements[6];
            }
            catch (Exception) { }
            ServiceAddrGridView.Rows[index].Cells["num_floors"].Value = rowElements[7];
            ServiceAddrGridView.Rows[index].Cells["roomButton"].Value = rowElements[8];
            

        }

        private void Add_New_Row(object sender, EventArgs e)
        {
            int i = ServiceAddrGridView.Rows.Add();
            String newID = my_controller.AddBlankAddr();
            ServiceAddrGridView.Rows[i].Cells[0].Value = newID;
            noChanges = false;
        }

        public void ClearGridView() { ServiceAddrGridView.Rows.Clear(); }        

        /********************************* Gets *****************************/

        public String[] GetInputs()
        {
            String[] allFeilds = new String[5];

            allFeilds[1] = contractNameBox.Text.ToString();
            allFeilds[2] = this.StartDatePicker.Value.ToString();
            allFeilds[3] = this.EndDatePicker.Value.ToString();
            allFeilds[4] = this.TermsBox.Text.ToString();

            return allFeilds;
        }

        public String[,] GetViewInputs()
        {
            int rows = ServiceAddrGridView.Rows.Count;
            String[,] cells = new String[rows, 10];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    try { cells[i, j] = ServiceAddrGridView.Rows[i].Cells[j].Value.ToString(); }
                    catch (NullReferenceException) { }
                }
                cells[i, 9] = contractID;
            }


            return cells;
        }

        public DateTime GetEndDate()
        {
            return EndDatePicker.Value;
        }

        public DateTime GetStartDate()
        {
            return StartDatePicker.Value;
        }

        public String GetSelectedContract()
        {
            if (nameComboBox != null)
            {
                if (this.nameComboBox.ValueMember != null)
                    return this.nameComboBox.SelectedValue.ToString();
                else
                    return null;
            }
            else
                return null;
        }

        public String GetContractText() { return contractNameBox.Text; }

        public string GetID() { return contractID; }

        /********************************* Sets *****************************/        

        public void SetFields(String[] values, int userID)
        {

            if (isEdit)
                nameComboBox.Text = values[1];

            this.contractNameBox.Text = values[1];
            this.StartDatePicker.Value = Convert.ToDateTime(values[2]);
            this.EndDatePicker.Value = Convert.ToDateTime(values[3]);
            this.TermsBox.Text = values[4];
            SetClientLabel(MClient.GetName(values[5]));
        }
        public void SetClientLabel(string name)
        {
           // this.titleLabel.Text = titleLabel.Text +" For " + name;
        }
        public void SetID(String id) { contractID = id; }

        public void ClientLinked(String name)
        {
            this.linkableClientBox.Visible = false;
            this.new_Client_Button.Visible = false;
            SetClientLabel(name);
        }

        public void IncDecRoom(bool Inc, int rowIndex)
        {
            int n;
            if (Inc)
            {
                n = Convert.ToInt32(ServiceAddrGridView.Rows[rowIndex].Cells["roomButton"].Value.ToString());
                ++n;
                ServiceAddrGridView.Rows[rowIndex].Cells["roomButton"].Value = n.ToString();
            }
            else
            {
                n = Convert.ToInt32(ServiceAddrGridView.Rows[rowIndex].Cells["roomButton"].Value.ToString());
                --n;
                ServiceAddrGridView.Rows[rowIndex].Cells["roomButton"].Value = n.ToString();

            }
        }
        public void IncDecServices(bool Inc, int rowIndex)
        {
            int n;
            if (Inc)
            {
                n = Convert.ToInt32(ServiceAddrGridView.Rows[rowIndex].Cells["editButton"].Value.ToString());
                ++n;
                ServiceAddrGridView.Rows[rowIndex].Cells["editButton"].Value = n.ToString();
            }
            else
            {
                n = Convert.ToInt32(ServiceAddrGridView.Rows[rowIndex].Cells["editButton"].Value.ToString());
                --n;
                ServiceAddrGridView.Rows[rowIndex].Cells["editButton"].Value = n.ToString();

            }
        }

        /********************************* General Form Events *****************************/ 
        public void ClientOptions(bool isShown)
        {
            if (isShown)
            {
                linkableClientBox.Visible = true;
                DataTable clients = MClient.GetUnLinked();

                DataTable dt = new DataTable();
                dt = clients.Clone();
                dt.Rows.Add(new String[] { null, "<Select Client>" });
                foreach (DataRow r in clients.Rows)
                    dt.ImportRow(r);

                linkableClientBox.DataSource = dt;
                linkableClientBox.DisplayMember = dt.Columns[1].ToString();
                linkableClientBox.ValueMember = dt.Columns[0].ToString();
                new_Client_Button.Visible = true;
            }
            else
            {
                linkableClientBox.Visible = false;
                new_Client_Button.Visible = false;
            }


        }

    /*    void Back_Button_MouseLeave(object sender, EventArgs e)
        {
            this.Back_Button.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Back2));
            this.Back_Button.Location = new Point(65, 38);
          //  this.Back_Button.Size = new Size(84, 78);
            this.Back_Button.ImageAlign = ContentAlignment.MiddleCenter;
        }

        void Back_Button_MouseEnter(object sender, EventArgs e)
        {
            this.Back_Button.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.BackOver));
            this.Back_Button.Location = new Point(65, 38);
            //this.Back_Button.Size = new Size(84, 78);
            this.Back_Button.BackgroundImageLayout = ImageLayout.Stretch;
            this.Back_Button.ImageAlign = ContentAlignment.MiddleCenter;
        }
        */
        private void new_Client_Button_Click(object sender, EventArgs e)
        {
            ClientOptions(false);
            my_controller.Contract_New_Client_ButtonPress(sender, e);

        }

        private void ServiceAddrGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        

    }
}
