using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FAFOS
{
    public class UserController
    {
        List<Bitmap> picList;
        MaintainUsersForm _userForm;
        MUser _user;
        AdminUserForm _adminForm;
        HQUserForm _hqForm;
        int userID, picID;
        public UserController() { }

        public UserController(MaintainUsersForm child, int usrID, int profilePictureID)
        {
            userID = usrID;
            _user = new MUser(userID);
            picID = profilePictureID;
            _userForm = child;

            if (userID == 1)
            {
                _user.giveAdmin();
                _user.SetHQ(MFranchise.isFranchisor());
            }

            String[] loc = _user.GetLocation();
            if (loc[0] != "0")
                _userForm.InitializeCombos(loc[0], loc[1], loc[2]);
            else
            {
                String[] op = MFranchise.GetOpReg();
                _userForm.InitializeCombos(op[2], op[3], op[4]);
            }
            
            LoadUserPic();
            _userForm.SetButtons(_user.IsAdmin(), _user.IsHQ());
            _userForm.SetFields(_user.Get());
            /*try { _userForm.SetFields(_user.Get()); }
            catch (Exception) { MessageBox.Show("Failed to load User"); _userForm.Close(); }*/

        }
/********************************** Maintain User Form ****************************/
        public void back_Button(object sender, EventArgs e)
        {
            MUser.DeleteBlanks();
            if (_adminForm != null)
            {
                if (_adminForm.noChanges)
                    _adminForm.Close();
                else
                {
                    if (MessageBox.Show("Are u sure you want to discard admin changes?", "Confirm Close", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        _adminForm.Close();
                    else
                        return;
                }

            }
            if (_hqForm != null)
            {
                if (_hqForm.noChanges)
                    _hqForm.Close();
                else
                {
                    if (MessageBox.Show("Are u sure you want to discard admin changes?", "Confirm Close", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        _hqForm.Close();
                    else
                        return;
                }

            }
            _userForm.Close();
        }
        public void UploadPic_click(object sender, EventArgs e)
        {
            OpenFileDialog opendialog = new OpenFileDialog();
            opendialog.Filter = "Image files(*.jpg; *.jpeg; *.gif)| *.jpg; *.jpeg; *.gif";
            
            if (opendialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap img = new Bitmap(opendialog.FileName);                
                opendialog.RestoreDirectory = true;
                LoadUserPic();
                List<Bitmap> picList = MUser.LoadImages();
                picList[picID] = img;
                MUser.SaveImages(picList);
                LoadUserPic();
            }
        }
        
        public void AdminButton_Click(object sender, EventArgs e)
        {
            if (_adminForm == null)
            {
                _adminForm = new AdminUserForm(this, _userForm.GetWindowMidRight());
                _adminForm.PopulateUserGridView(MUser.GetAllUsers());
                _adminForm.PopulateBAddrsGridView(MFranchise.GetBAddrs());
                _adminForm.noChanges = true;
                _adminForm.Show();
                _userForm.ToggleAdminButton();
            }
            else
            {
                if (_adminForm.noChanges)
                {
                    _adminForm.Close();
                    _adminForm = null;
                    _userForm.ToggleAdminButton();
                }
                else
                {
                    if (MessageBox.Show("Are u sure you want to discard admin changes?", "Confirm Discard", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        _adminForm.Close();
                        _adminForm = null;
                        _userForm.ToggleAdminButton();
                    }
                    else
                        return;

                }

            }
        }
        public void HQButton_Click(object sender, EventArgs e)
        {
            if (_hqForm == null)
            {
                _hqForm = new HQUserForm(this, _userForm.GetWindowMidRight());
                _hqForm.Show();
                _userForm.ToggleHQButton();
            }
            else
            {
                _hqForm.Close();
                _hqForm = null;
                _userForm.ToggleHQButton();
            }
        }
        public void SaveButton_Click(object sender, EventArgs e)
        {
            if (_userForm.noChanges)
                _userForm.Close();
            else
            {
                if (MessageBox.Show("Are you sure you want to submit these changes?", "Confirm Submission", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {// if we are good, submit changes to dataBase
                    if (_userForm.ValidToSet())
                    {
                        _user.Set(_userForm.GetFields());
                        _userForm.Close();
                    }
                    else
                        return;
                }
            }
            
        }
        public void LocationChanged(object sender, EventArgs e)
        {
            if (_adminForm != null)
                _adminForm.SetLoc(_userForm.GetWindowMidRight());
            if (_hqForm != null)
                _hqForm.SetLoc(_userForm.GetWindowMidRight());
        }
/********************************** Profile Pics **********************************/
        public void LoadUserPic()
        {
            try { picList = MUser.LoadImages();}
            catch (Exception){picList = new List<Bitmap>();}

            Bitmap img;
            try{img = picList[picID];}
            catch(Exception)
            {
                if (picID == picList.Count)
                {
                    picList.Add(new Bitmap(Properties.Resources.DefaultProPic));
                    img = picList[picID];
                }
                else{
                    picList[picID] = new Bitmap(Properties.Resources.DefaultProPic);
                    img = picList[picID];
                }
            }
            _userForm.setPic(img);           
 
            
        }
/********************************** Admin User Form **********************************/
        public void UserGridViewClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            if ((e.ColumnIndex == 2) && (e.RowIndex > -1))//-----------------------------------password reset
            {
                if (MessageBox.Show("Are you sure you want to rest this user's Password?", "Confirm Reset", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    String newPass = GeneratePassword();
                    dgv.Rows[e.RowIndex].Cells["passSetCol"].Value = newPass;
                    MessageBox.Show("Heres the new Password: " + newPass + "\nMake sure to copy it down");
                }
            }
            if ((e.ColumnIndex == 10) && (e.RowIndex > -1))//-----------------------------------remove
            {
                if (dgv.Rows[e.RowIndex].Cells["usrIDCol"].Value != null)
                    if( dgv.Rows[e.RowIndex].Cells["usrIDCol"].Value.ToString() == "1" )
                    {
                        MessageBox.Show("Cannot remove Primary user!");
                        return;
                    }

                if (userID == 1)//elivated privileges
                {
                    if (MessageBox.Show("Delete User?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        //MUser.Delete(dgv.Rows[e.RowIndex].Cells["usrIDCol"].Value.ToString());
                        try { MUser.Delete(dgv.Rows[e.RowIndex].Cells["usrIDCol"].Value.ToString()); }
                        catch (Exception) { }
                        dgv.Rows.RemoveAt(e.RowIndex);
                    }
                }
                else // any other user
                {
                    if (string.Equals(dgv.Rows[e.RowIndex].Cells["adminCol"].ToString(), "true", StringComparison.OrdinalIgnoreCase)
                        || string.Equals(dgv.Rows[e.RowIndex].Cells["hqCol"].ToString(), "true", StringComparison.OrdinalIgnoreCase))
                    {
                        MessageBox.Show("You cannot delete users with admin priviledges!\nOnly the primary user can");
                        return;
                    }
                    else
                        if (MessageBox.Show("Delete User?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                        {
                            try { MUser.Delete(dgv.Rows[e.RowIndex].Cells["usrIDCol"].Value.ToString()); }
                            catch (Exception) { }
                            dgv.Rows.RemoveAt(e.RowIndex);
                        }
                }

            }
        }
        public void BAddrGridViewClick(object sender, DataGridViewCellEventArgs e)
        {
            var dgv = sender as DataGridView;
            if ((e.ColumnIndex == 6) && (e.RowIndex > -1))//-----------------------------------remove
            {
                if (MessageBox.Show("Delete Business Address?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try { MFranchise.DeleteBAddress(dgv.Rows[e.RowIndex].Cells["locID"].Value.ToString()); }
                    catch (Exception) { }
                    dgv.Rows.RemoveAt(e.RowIndex);
                }
            }


        }
        public string GeneratePassword()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < 6; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
        public void SaveBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are u sure you want to Save admin changes?", "Confirm Save", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (_adminForm.isOkToClose())
                {
                    MFranchise.SetAll(_adminForm.getFields(), _adminForm.GetUserView(), _adminForm.GetBAddrView());
                    _adminForm.noChanges = true;
                }
                else
                {
                    MessageBox.Show("unable to save, check for errors");
                    return;
                }
            }
            else
                return;
        }
/********************************** HQ User Form **********************************/
        public void FranchiseGridView_ContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if ((e.ColumnIndex == 7) && (e.RowIndex > -1))//----------Remove
            {
                if (MessageBox.Show("Delete Franchise?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try { MFranchise.DeleteFromHQ(dgv.Rows[e.RowIndex].Cells["idCol"].Value.ToString()); }
                    catch (Exception) { }
                    dgv.Rows.RemoveAt(e.RowIndex);
                    _hqForm.CheckFranchiseBtn();
                }
                else
                    return;
            }
        }        
        public void HQSaveBtn_Click(object sender, EventArgs e)
        {
            DataTable dt = _hqForm.GetFranchiseView();
            foreach (DataRow r in dt.Rows)
                MFranchise.SetHQ(r);
        }
        public void DeleteRegionBtn_Click(object sender, EventArgs e)
        {
            String id = _hqForm.GetSelectedRegion();
            if (id == "NULL")
                return;
            else
            {
                if (MOpReg.hasFranchise(id))
                {
                    MessageBox.Show("Cannot Delete a Region that Has a Franchise\n Delete the franchisee first.");
                    return;
                }
                else
                {
                    MOpReg.Delete(id);
                    _hqForm.LoadOpRegions();
                }
            }
        }
        public void AddRegionBtn_Click(object sender, EventArgs e)
        {
            String[] values = _hqForm.getNewRegion();
            MOpReg.SetNew(values);
            _hqForm.LoadOpRegions();
        }

        public string GenerateFranchiseID()
        {
            int n;
            do
            {
                Random random = new Random((int)DateTime.Now.Ticks);
                n = random.Next();
            } while (MFranchise.CheckOriginality(n.ToString()));

            return n.ToString();
        }
    }
}
