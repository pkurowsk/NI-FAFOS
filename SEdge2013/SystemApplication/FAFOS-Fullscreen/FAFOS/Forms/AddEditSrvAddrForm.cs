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
    public partial class AddEditSrvAddrForm : Form
    {
        int currentRow,addrRow;
        public bool noChanges;
        String addressId;        
        MaintainClientController my_controller;
        public AddEditSrvAddrForm(MaintainClientController parent,String Address,String id,int AddrIndex)
        {
            InitializeComponent();
            my_controller = parent;
            addrRow = AddrIndex;
            AddressLabel.Text = Address;
            addressId = id;
            noChanges = true;
            DatePicker.Value = DateTime.Today;

            this.Ok_Button.Click += new EventHandler(my_controller.SrvAddr_Ok_Button_Click);
            this.Cancel_Button.Click += new EventHandler(my_controller.SrvAddr_Cancel_Button_Click);
            this.TermsView.CellClick += new DataGridViewCellEventHandler(my_controller.TermsView_CellContentClick);
        }

        public void setDate(DateTime values)
        {
            DatePicker.Value = values;
        }

        public void setFields(DataTable values)
        {
            int rowIndex;
           
            for (int i = 0; i < values.Rows.Count; i++)
            {
                rowIndex = Add_Service();
                try
                {
                    TermsView.Rows[rowIndex].Cells[0].Value = values.Rows[i][0];
                    TermsView.Rows[rowIndex].Cells[1].Value = values.Rows[i][1];
                    TermsView.Rows[rowIndex].Cells[2].Value = values.Rows[i][2];
                    TermsView.Rows[rowIndex].Cells[3].Value = values.Rows[i][3];
                    TermsView.Rows[rowIndex].Cells[4].Value = values.Rows[i][4];
                }
                catch (Exception) { }
                
                
            }

            noChanges = true; 
        }

        public void ShowPicker(bool isShown,int index)
        {
            currentRow = index;
            if (isShown)
            {
                try { DatePicker.Value = Convert.ToDateTime(TermsView.Rows[index].Cells["EffectDate"].Value); }
                catch (Exception) { }

                DatePicker.Visible = true;
            }
            else
                DatePicker.Visible = false;
        }
        public String[,] GetInputs()
        {
            int rows = TermsView.Rows.Count;
            String[,] cells = new String[rows, 6];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    try { cells[i, j] = TermsView.Rows[i].Cells[j].Value.ToString(); }
                    catch (NullReferenceException) { }
                }
                cells[i, 5] = addressId;
            }

            
            return cells;
        }

        private void Add_Service_Button_Click(object sender, EventArgs e)
        {
            my_controller.IncDecServices(true, addrRow);
            Add_Service();
        }
        private int Add_Service()
        {
            int i = this.TermsView.Rows.Add();
            var services = TermsView.Rows[i].Cells["serviceBox"] as DataGridViewComboBoxCell;
            var periods = TermsView.Rows[i].Cells["recurBox"] as DataGridViewComboBoxCell;
            DataTable dt = MService.GetList();
            DataTable dtt = MTimePeriods.GetList();
            services.DataSource = dt;
            services.DisplayMember =  dt.Columns[1].ToString();
            services.ValueMember = dt.Columns[0].ToString();
            periods.DataSource = dtt;
            periods.DisplayMember =  dtt.Columns[1].ToString();
            periods.ValueMember =  dtt.Columns[0].ToString();
            noChanges = false;

            return i;
        }

        private void DatePicker_ValueChanged(object sender, EventArgs e)
        {
            if (TermsView.Rows.Count != 0 && currentRow!=-1)
            {
                TermsView.Rows[currentRow].Cells["EffectDate"].Value = DatePicker.Value.ToShortDateString();
                noChanges = false;
            }
        }

        private void TermsView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            noChanges = false;
        }
        public int GetAddrIndex() { return addrRow; }

        private void TermsView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
}
