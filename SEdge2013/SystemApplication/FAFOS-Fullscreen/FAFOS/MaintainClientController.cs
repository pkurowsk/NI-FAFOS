using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using tiles;

namespace FAFOS
{
    public class MaintainClientController
    {
        private static View _mainForm;
        private static AddEditClientForm _clientForm;
        private static AddEditContractForm _contractForm;
        private static AddEditRoomForm _roomForm;
        private static AddEditSrvAddrForm _srvAddrForm;
        private static MClient _client;
        private static MClientContract _contract;
        private static MServiceAddress _srvAddr;
        private int userID;
        private bool okDone=false;
        public MaintainClientController() { }
/************************* Instantiating Models *************************************************/
        private static void NewClient() { _client = null; _client = new MClient(); }
        private static void OldClient(String id) { _client = null; _client = new MClient(id); }
        private static void NewContract() { _contract = new MClientContract();}
        private static void OldContract(String id) { _contract = new MClientContract(id); }
        private static void NewSrvAddr() {_srvAddr = new MServiceAddress();}
/************************* Other Functions ****************************************************/
        

/************************* Main Form Events ****************************************************/
 
        public void New_client_button_Click(tile sender, int id)
        {
            userID = id;
            NewClient();
            _mainForm = (View)sender.FindForm();

            _clientForm = new AddEditClientForm(this, false, userID);
            _clientForm.Activate();
            _clientForm.Show();
        }

        public void Edit_Client_Button_Click(tile sender, int id)
        {
            _mainForm = (View)sender.FindForm();
            _clientForm = new AddEditClientForm(this, true,id);
            _clientForm.Activate();
            _clientForm.Show();
        }

        public void Add_contract_Button_Click(tile sender, int id)
        {
            _mainForm = (View)sender.FindForm();
            userID = id;
            NewContract();
            _contractForm = new AddEditContractForm(this, false, userID, _contract.FindID());
            _contractForm.Activate();
            _contractForm.Show(); 


        }
        public void Edit_contract_Button_Click(tile sender, int id)
        {
            _mainForm = (View)sender.FindForm();
            userID = id;
            _contractForm = new AddEditContractForm(this, true, userID, "0");
            _contractForm.Show();
        }

/************************* Add Edit Client Form Events *****************************************/

        public void Country_Changed(object sender, EventArgs e)
        {
            if(_clientForm != null)
                if (_clientForm.GetCountryBox() != "System.Data.DataRowView") 
                    if (_clientForm.GetCountryBox() != "-1")
                    {
                    String countryID = _clientForm.GetCountryBox();
                    if (countryID != null)
                        {
                            _client.changeCountry(countryID);
                            _clientForm.SetProvStateBox(MProvState.GetFilteredList(countryID));
                        }
                    }
        }
        public void Province_Changed(object sender, EventArgs e)
        {
            if (_clientForm.GetProvStateBox() != "System.Data.DataRowView") 
                if (_clientForm.GetProvStateBox() != "-1")
                {
                    String provID = _clientForm.GetProvStateBox();
                    if (provID != null)
                    {
                        _client.changeProvince(provID);
                        _clientForm.SetCityBox(MCity.GetFilteredList(provID));
                    }
                }
        }
        public void City_Changed(object sender, EventArgs e)
        {
            if (_clientForm.GetCityBox() != "System.Data.DataRowView") 
                if (_clientForm.GetCityBox() != "-1")
                {
                    _clientForm.noChanges = false;
                    _client.changeCity(_clientForm.GetCityBox());
                }
        }

        public void Client_Contract_Button_Click(object sender, EventArgs e)
        {
            String id = _client.GetContract();
            if ((id == null)|| id == "")
            {
                NewContract();
                _contractForm = new AddEditContractForm(this, false, userID, _contract.FindID());
                _contractForm.ClientLinked(_clientForm.GetName());
                _contract.SetClient(_client.FindID());
                _contractForm.ShowDialog();

                if (MClientContract.GetDT(_contract.FindID(), "Client_Contract", "client_contract_id").Rows.Count > 0)
                {
                    MClient.SetContract(_client.FindID(), _contract.FindID());
                    _client.changeContract(_contract.FindID());
                    _clientForm.SetContractButton(MClientContract.GetName(_contract.FindID()));
                }

            }
            else
            {
                OldContract(id);
                _contractForm = new AddEditContractForm(this, false, userID, id);
                _contractForm.ClientLinked(_clientForm.GetName());
                _contractForm.SetFields(_contract.Get(),userID);
                Populate_AddrGridView(id);
                _contractForm.Activate();
                _contractForm.ShowDialog();
            }
        }

        public void Edit_Client_Populate(object sender, EventArgs e)
        {
            String clientID = _clientForm.GetSelectedClient();
            if (clientID != "")
            {
                OldClient(clientID);
                String[] fields = _client.Get();

                _clientForm.setFields(fields);
                _clientForm.SetCountryBox(MCountry.GetList(), fields[11]);
                _clientForm.SetProvStateBox(fields[12]);
                _clientForm.SetCityBox(fields[13]);
            }
        }

        public void Client_Text_Changed(object sender, EventArgs e)
        {
            _clientForm.noChanges = false;
        }

        public void Client_Delete_Button_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want Delete this client?", "Confirm Deletion", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                String id = _client.FindID();
                _client.Delete();
                MClientContract.DeleteAll(id);
               // _mainForm.SetClientBox(MClient.GetList());
                _clientForm.Close();
            }
        }

        public void Client_Ok_Button_Click(object sender, EventArgs e)
        {
            if (_clientForm.noChanges)
            {
                
                _clientForm.Close();
            }
            else
            {

                String[] values = _clientForm.GetInputs();

                bool okToSubmit = true;
                for (int i = 0; i < values.Length; i++)
                    if (values[i] == "Fail")
                        okToSubmit = false;

                if (okToSubmit)
                {
                    if (MessageBox.Show("Are you sure you want to submit these changes?", "Confirm Submission", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {// if we are good, submit changes to dataBase        

                        _client.Set(values);
                        // _mainForm.SetClientBox(MClient.GetList());
                       
                        _clientForm.Close();
                    }
                }

                else
                    MessageBox.Show("Some Fields Have Errors", "Errors");
            }     
        }

        public void Client_Cancel_Button_Click(object sender, EventArgs e)
        {            
            if (_clientForm.noChanges)
                _clientForm.Close();
            else
            {
                if (MessageBox.Show("Are you sure you want to discard?", "Confirm Cancel", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    _clientForm.Close();
                    if (_client.isNew())
                        MClientContract.DeleteAll(_client.FindID());
                    return;
                }
                    

            }          
        }

/************************* Add Edit Contract Form Events *****************************************/        

        public void Contract_Cell_Click(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            if ((e.ColumnIndex == 9) && (e.RowIndex > -1))//-----------------------------------Edit
            {
                #region Edit    
                if (_contractForm.GetEndDate().ToShortDateString() != DateTime.Today.ToShortDateString())
                {
                    NewSrvAddr();
                    string addr = "<No Address>";
                    String addrID = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
                    try { addr = dgv.Rows[e.RowIndex].Cells["address_col"].Value.ToString(); }
                    catch (NullReferenceException) { }

                    _srvAddrForm = new AddEditSrvAddrForm(this, addr, addrID, e.RowIndex);
                    try { _srvAddrForm.setFields(MContractServices.GetAll(addrID)); }
                    catch (Exception) { }

                  //  _srvAddrForm.Activate();
                    _srvAddrForm.ShowDialog();
                    _srvAddrForm.setDate(_contractForm.GetStartDate());
                    _contractForm.SetTableButtonMetrics(e.RowIndex, addrID);
                }
                else
                    MessageBox.Show("Please specify the end date of the contract.", "Incomplete Contract", MessageBoxButtons.OKCancel);
                #endregion
            }
            if ((e.ColumnIndex == 8) && (e.RowIndex > -1))// ----------------------------------Room
           {
               #region Room
               //int k = arg.RowIndex;
               String addrID = dgv.Rows[e.RowIndex].Cells["idCol"].Value.ToString();
                NewSrvAddr();
                _roomForm = new AddEditRoomForm(this, addrID, e.RowIndex);
                _roomForm.Activate();    
                _roomForm.ShowDialog();

                _contractForm.SetTableButtonMetrics(e.RowIndex, addrID);
                return;
               #endregion
           }

            if ((e.ColumnIndex == 10) && (e.RowIndex > -1))//-----------------------------------Remove
            {
                #region Remove
                try
                {
                    string id = dgv.Rows[e.RowIndex].Cells["idCol"].Value.ToString();
                    MServiceAddress.Delete(id, "Service_Address", "service_addres_id"); 
                }
                catch (Exception) { }
                dgv.Rows.RemoveAt(e.RowIndex);
                #endregion
            }


            else//---------------------------------------------------------------------------Other
                return;
                
                

        }

        public void Contract_ClientBox_Select_Changed(object sender, EventArgs e)
        {
            var clients = sender as ComboBox;
            if (clients.SelectedValue != "")
                _contract.SetClient(clients.SelectedValue.ToString());
        }

        public void Contract_New_Client_ButtonPress(object sender, EventArgs e)
        {
            NewClient();
            _clientForm = new AddEditClientForm(this, false, userID);
            _clientForm.Activate();
            _clientForm.DisableContract(_contractForm.GetContractText());
            _clientForm.ShowDialog();

            if (_client.GetID() == null)
                _contractForm.ClientOptions(true);
            else
            {
                _contract.SetClient(_client.GetID());
                _contractForm.ClientLinked(_client.GetID());
                _contractForm.noChanges = false;
            }
        }

        public void Contract_Text_Changed(object sender, EventArgs e)
        {
            _contractForm.noChanges = false;
        }

        public void Contract_Delete_Button_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want Delete this client?\nThis will delete Service Addresses connected to is as well", "Confirm Deletion", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                _contract.RemoveFromClient();
                _contract.Delete();
              //  _mainForm.SetContractsBox(MClientContract.GetList());
                _contractForm.Close();
            }
        }//Make it Cascade

        public void Contract_Ok_Button_Click(object sender, EventArgs e)
        {
            if (_contractForm.noChanges)
                _contractForm.Close();
            
            else
            {
                if (_contract.getClientID() == "")
                {
                    MessageBox.Show("A contract requires a Client to be created");
                    return;
                }

                String[] values = _contractForm.GetInputs();
                String[,] srvAddrs = _contractForm.GetViewInputs();

                bool okToSubmit = true;
                for (int i = 0; i < values.Length; i++)
                    if (values[i] == "Fail")
                        okToSubmit = false;

                if (okToSubmit)
                {
                    if (MessageBox.Show("Are you sure you want to submit these changes?", "Confirm Submission", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        _contract.Set(values);// if we are good, submit changes to dataBase
                        NewSrvAddr();
                        String[] row; 
                        for (int i = 0; i < (srvAddrs.Length/10);i++)
                        {
                            row = new String[10];
                            for (int j = 0; j < 10; j++)
                                row[j] = srvAddrs[i, j];

                            _srvAddr.Set(row);
                        }
                        OldClient(_contract.getClientID());
                        MClient.SetContract(_contract.getClientID(),_contract.FindID());
                        okDone = true;
                        _contractForm.Close();



                    }
                    else
                        return;
                }
            }
        }

        public void Contract_Closing(object sender, EventArgs e)
        {
            if (!okDone && _contractForm.GetSelectedContract()!="")
            {
                String name;
                if (_contractForm.GetSelectedContract() != null)
                    name = MClientContract.GetName(_contractForm.GetSelectedContract());
                else
                    name = "0";
                if (name != _contractForm.GetContractText() && _contractForm.GetContractText()!="")
                {
                    if (!_contractForm.noChanges)
                    {
                        String contractID = _contractForm.GetSelectedContract();
                        if ((contractID == null) || (contractID == "0") || (contractID == ""))
                        {
                            MServiceAddress.DeleteBlanks();
                        }
                        else
                        {
                            _contract.RemoveFromClient();
                            _contract.Delete();
                            MServiceAddress.DeleteBlanks();
                        }

                    }
                    // _contractForm.Close(); 
                }
            }
            okDone = false;      
        }//make Cascade

        private void Populate_AddrGridView(String contractID)
        {
            if (_contractForm != null)
            {
                try { _contractForm.ClearGridView(); }
                catch (Exception) { }

                DataTable srvAddrs = MServiceAddress.GetAllAddrs(contractID);
                String rowID;
                String[] rowData;
                for (int i = 0; i < srvAddrs.Rows.Count; i++)
                {
                    rowID = srvAddrs.Rows[i].ItemArray[0].ToString();
                    NewSrvAddr();
                    rowData = _srvAddr.Get(rowID);
                    _contractForm.Add_Old_Row(rowData);
                }
            }
        }

        public void Edit_Contract_Populate(object sender, EventArgs e)
        {
            String contractID = _contractForm.GetSelectedContract();
            if ((contractID == null) || (contractID == "0") || (contractID == "")) return;

            OldContract(contractID);

            String[] contractFields = _contract.Get();

            _contractForm.SetFields(contractFields, userID);
            _contractForm.SetID(contractID);

            Populate_AddrGridView(contractID);
            _contractForm.noChanges = true;

        }

        public string AddBlankAddr()
        {
            MServiceAddress sa = new MServiceAddress();
            return sa.AddBlank();
        }

        public void IncDecRoom(bool Inc, int rowIndex) { _contractForm.IncDecRoom(Inc, rowIndex); }
        public void IncDecServices(bool Inc, int rowIndex) { _contractForm.IncDecServices(Inc, rowIndex); }
        

/************************* Maintain Service Address Form Events ***********************************/

        public void TermsView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            if ((e.ColumnIndex == 3) && (e.RowIndex > -1))//-----------------------------------DateCell
            {
                _srvAddrForm.ShowPicker(true, e.RowIndex);
                return;
            }
            if ((e.ColumnIndex == 5) && (e.RowIndex > -1))//-----------------------------------Remove
            {
                _srvAddrForm.ShowPicker(false, -1);
                try
                {
                    MContractServices.Delete(dgv.Rows[e.RowIndex].Cells["TermID"].Value.ToString());
                    dgv.Rows.RemoveAt(e.RowIndex);
                    IncDecServices(false, _srvAddrForm.GetAddrIndex());
                }
                catch (Exception ed) { }
            }
            else
            {
                
                _srvAddrForm.ShowPicker(false, -1);
            }

        }

        public void SrvAddr_Ok_Button_Click(object sender, EventArgs e)
        {
            if (_srvAddrForm.noChanges)
            {
                _srvAddrForm.Close();
                return;
            }

            else
            {
                String[,] values = _srvAddrForm.GetInputs();
                bool okToSubmit = true;

                if (okToSubmit)
                {
                    if (MessageBox.Show("Are you sure you want to submit these changes?", "Confirm Submission", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        MContractServices cs = new MContractServices();
                        try {
                            
                            cs.SetMany(values, userID, _contractForm.GetEndDate()); 
                        }
                        catch (Exception) { MessageBox.Show("Error Updating Database"); }
                        _srvAddrForm.Close();


                    }
                    else
                    {
                        return;
                    }
                }
            }                

        }

        public void SrvAddr_Cancel_Button_Click(object sender, EventArgs e)
        {
            if (_srvAddrForm.noChanges)
                _srvAddrForm.Close();
           
            else
            {
                if (MessageBox.Show("Are you sure you want to discard?", "Confirm Cancel", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    MServiceAddress.DeleteBlanks();
                    _srvAddrForm.Close();
                }
            }
                
        }

/******************************Maintain Rooms Events**********************************************/

        public void Room_Cell_Click(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.ColumnIndex == 3) && (e.RowIndex > -1))// ------------------------------Extinguisher
            {
                _roomForm.ShowServiceItemView("extinguisher", e.RowIndex);
            }
            if ((e.ColumnIndex == 4) && (e.RowIndex > -1))// ------------------------------Hose
            {
                _roomForm.ShowServiceItemView("hose", e.RowIndex);
            }
            if ((e.ColumnIndex == 5) && (e.RowIndex > -1))// ------------------------------Light
            {
                _roomForm.ShowServiceItemView("light", e.RowIndex);
            }
            if ((e.ColumnIndex == 6) && (e.RowIndex > -1))// ------------------------------Remove
            {
                var dgv = sender as DataGridView;
                _roomForm.ShowServiceItemView("none", e.RowIndex);
                string roomID = dgv.Rows[e.RowIndex].Cells["idCol"].Value.ToString();
                MRoom.Delete(roomID);
                dgv.Rows.RemoveAt(e.RowIndex);                
                _roomForm.noChanges = false;
            }
        }

        public void ExtinguisherView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if ((e.ColumnIndex == 7) && (e.RowIndex > -1))
            {
                var dgv = sender as DataGridView;
                try
                {
                    string extID = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
                    MRoom.DeleteExtinguisher(extID);
                }
                catch (Exception) { }
                dgv.Rows.RemoveAt(e.RowIndex);
                _roomForm.noChanges = false;
            }
            else
                _roomForm.noChanges = false;
        }

        public void HoseView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if ((e.ColumnIndex == 4) && (e.RowIndex > -1))
            {
                var dgv = sender as DataGridView;
                try
                {
                    string hoseID = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
                    MRoom.DeleteHose(hoseID);
                }
                catch (Exception) { }
                dgv.Rows.RemoveAt(e.RowIndex);
                _roomForm.noChanges = false;
            }
            else
                _roomForm.noChanges = false;
        }

        public void LightView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if ((e.ColumnIndex == 10) && (e.RowIndex > -1))
            {
                var dgv = sender as DataGridView;
                try
                {
                    string lightID = dgv.Rows[e.RowIndex].Cells[0].Value.ToString();
                    MRoom.DeleteLight(lightID);
                }
                catch (Exception) { }
                dgv.Rows.RemoveAt(e.RowIndex);
                _roomForm.noChanges = false;
            }
            else
                _roomForm.noChanges = false;
        }

        public void Room_Ok_Button_Click(object sender, EventArgs e)
        {
            if (_roomForm.noChanges)
            {
                _roomForm.Close();
                return;
            }
            else
            {
                String[,] rooms = _roomForm.GetRooms();
                int nRooms = rooms.Length/4;

                bool okToSubmit = true;
                /*
                 * Validate....okToSubmit = False;
                */
                if (okToSubmit)
                {
                    if (MessageBox.Show("Are you sure you want to submit these changes?", "Confirm Submission", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {// if we are good, submit changes to dataBase
                        String[,] ext, hoses, lights;
                        for (int i = 0; i < nRooms; i++)
                        {
                            ext = _roomForm.GetExtinguishers(i);
                            hoses = _roomForm.GetHoses(i);
                            lights = _roomForm.GetLights(i);
                            /*
                            * Validate....okToSubmit = False;
                            */
                            if (okToSubmit)
                            {
                                MRoom.SetExtinguishers(ext);
                                MRoom.SetHoses(hoses);
                                MRoom.SetLights(lights);
                            }
                            else
                                return;
                        }

                        MRoom.SetMany(rooms);
                        _contractForm.noChanges = false;
                        _roomForm.Close();
                    }
                    else
                        return;                    
                }
            }
        }

        public void Room_Cancel_Button_Click(object sender, EventArgs e)
        {
            if (_roomForm.noChanges)
            {
                _roomForm.Close();
                return;
            }
            else
            {
                if (MessageBox.Show("Are you sure you want to discard?", "Confirm Cancel", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    MRoom.RemoveBlanks();
                    _roomForm.Close();
                }
            }                
        }
    }
}
