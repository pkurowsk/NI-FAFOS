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
    public partial class AddEditRoomForm : Form
    {
        MaintainClientController my_controller;
        public bool noChanges;
        int currentRow, AddrRow;
        String AddressID;
        List<DataGridView> extViews = new List<DataGridView>();
        List<DataGridView> hoseViews = new List<DataGridView>();
        List<DataGridView> lightViews = new List<DataGridView>();

/**************************************** Constructor *************************************/
        public AddEditRoomForm(MaintainClientController parent, String id, int AddrIndex)
        {
            InitializeComponent();
            my_controller = parent;
            AddressID = id;
            AddrRow = AddrIndex;
            noChanges = true;
            this.Ok_Button.Click += new EventHandler(my_controller.Room_Ok_Button_Click);
            this.Cancel_Button.Click += new EventHandler(my_controller.Room_Cancel_Button_Click);
            this.RoomGridView.CellContentClick += new DataGridViewCellEventHandler(my_controller.Room_Cell_Click);

            PopulateRoomGridView(MRoom.GetRoomsForAddr(id));

        }        
/**************************************** Gets *******************************************/
        public String[,] GetRooms()
        {
            int n = RoomGridView.Rows.Count;
            String[,] rooms = new String[n,4];
            for (int i = 0; i < n; i++)
            {
                rooms[i, 0] = RoomGridView.Rows[i].Cells["idCol"].Value.ToString();
                rooms[i, 1] = RoomGridView.Rows[i].Cells["roomNum"].Value.ToString();
                rooms[i, 2] = RoomGridView.Rows[i].Cells["floor"].Value.ToString();
                rooms[i, 3] = AddressID;
            }
            return rooms;
        }
        public String[,] GetExtinguishers(int index)
        {
            int n = extViews[index].Rows.Count;
            String[,] extinguishers = new String[n, 7];
            for (int i = 0; i < n; i++)
                for(int j = 0; j < 7;j++)
                {
                    try { extinguishers[i, j] = extViews[index].Rows[i].Cells[j].Value.ToString(); }
                    catch (NullReferenceException) { extinguishers[i, j] = null; }
                }

            return extinguishers;
        }
        public String[,] GetHoses(int index)
        {
            int n = hoseViews[index].Rows.Count;
            String[,] hoses = new String[n, 4];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < 4; j++)
                {
                    try { hoses[i, j] = hoseViews[index].Rows[i].Cells[j].Value.ToString(); }
                    catch (NullReferenceException) { hoses[i, j] = null; }
                }

            return hoses;
        }
        public String[,] GetLights(int index)
        {
            int n = lightViews[index].Rows.Count;
            String[,] lights = new String[n, 10];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < 10; j++)
                {
                    try { lights[i, j] = lightViews[index].Rows[i].Cells[j].Value.ToString(); }
                    catch (NullReferenceException) { lights[i, j] = null; }
                }

            return lights;
        }
/**************************************** Sets *******************************************/
        public void SetAddrLabel(String address)
        {
            if(address != "")
                this.Text = "Maintain Rooms For "+ address;
        }
        public void PopulateRoomGridView(DataTable rooms)
        {
            int nRows = rooms.Rows.Count;
            String roomID;
            for (int i = 0; i < nRows; i++)
            {
                roomID = rooms.Rows[i][0].ToString();
                AddRoom();
                RoomGridView.Rows[i].Cells["idCol"].Value = rooms.Rows[i][0];
                RoomGridView.Rows[i].Cells["roomNum"].Value = rooms.Rows[i][1];
                RoomGridView.Rows[i].Cells["floor"].Value = rooms.Rows[i][2];
                PopulateExtinguisherView(i, MRoom.GetExtinguishers(roomID));
                PopulateHoseView(i, MRoom.GetHoses(roomID));
                PopulateLightView(i, MRoom.GetLights(roomID));


            }
            noChanges = true;

        }
        private void PopulateExtinguisherView(int index, DataTable extinguishers)
        {

            int n = extinguishers.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                AddExtinguisher(index);
                for (int j = 0; j < 7; j++)
                {
                    extViews[index].Rows[i].Cells[j].Value = extinguishers.Rows[i][j];
                }
            }
        }
        private void PopulateHoseView(int index, DataTable hoses)
        {

            int n = hoses.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                AddHose(index);
                for (int j = 0; j < 4; j++)
                {
                    hoseViews[index].Rows[i].Cells[j].Value = hoses.Rows[i][j];
                }
            }
        }
        private void PopulateLightView(int index, DataTable lights)
        {

            int n = lights.Rows.Count;
            for (int i = 0; i < n; i++)
            {
                AddLight(index);
                for (int j = 0; j < 10; j++)
                {
                    lightViews[index].Rows[i].Cells[j].Value = lights.Rows[i][j];
                }
            }
        }

/**************************************** AddTos *****************************************/
        private void AddRoom()
        {
            int i = RoomGridView.Rows.Add();
            MRoom.AddBlank();
            RoomGridView.Rows[i].Cells["idCol"].Value = MRoom.AddBlank();
            RoomGridView.Rows[i].Cells["extCol"].Value = "0";
            RoomGridView.Rows[i].Cells["hoseCol"].Value = "0";
            RoomGridView.Rows[i].Cells["lightCol"].Value = "0";

            extViews.Add(NewExtinguisherView());
            this.Controls.Add(extViews[extViews.Count - 1]);

            hoseViews.Add(NewHoseView());
            this.Controls.Add(hoseViews[hoseViews.Count - 1]);

            lightViews.Add(NewLightView());
            this.Controls.Add(lightViews[lightViews.Count - 1]);

            noChanges = false;
        }
        private int AddExtinguisher(int viewIndex)
        {
            int eI = extViews[viewIndex].Rows.Add();
            extViews[viewIndex].Rows[eI].Cells["eRoom"].Value =
                RoomGridView.Rows[viewIndex].Cells["idCol"].Value;

            int n = Convert.ToInt32(RoomGridView.Rows[viewIndex].Cells["extCol"].Value);
            ++n;
            RoomGridView.Rows[viewIndex].Cells["extCol"].Value = n.ToString();
            
            noChanges = false;
            return n;
        }
        private int AddHose(int viewIndex)
        {
            int hI = hoseViews[viewIndex].Rows.Add();
            hoseViews[viewIndex].Rows[hI].Cells["hRoom"].Value =
                RoomGridView.Rows[viewIndex].Cells["idCol"].Value;

            int n = Convert.ToInt32(RoomGridView.Rows[viewIndex].Cells["hoseCol"].Value);
            ++n;
            RoomGridView.Rows[viewIndex].Cells["hoseCol"].Value = n.ToString();
            
            noChanges = false;
            return n;
        }
        private int AddLight(int viewIndex)
        {
            int lI = lightViews[viewIndex].Rows.Add();
            lightViews[viewIndex].Rows[lI].Cells["lRoom"].Value =
                RoomGridView.Rows[viewIndex].Cells["idCol"].Value;

            int n = Convert.ToInt32(RoomGridView.Rows[viewIndex].Cells["lightCol"].Value);
            ++n;
            RoomGridView.Rows[viewIndex].Cells["lightCol"].Value = n.ToString();
            noChanges = false;
            return n;
        }

/**************************************** Events *******************************************/
        private void AddItemButton_Click(object sender, EventArgs e)
        {
            var b = sender as Button;
            switch (b.Text)
            {
                case "Add Extinguisher":
                    AddExtinguisher(currentRow);
                    break;

                case "Add Hose":
                    AddHose(currentRow);
                    break;

                case "Add Light":
                    AddLight(currentRow);
                    break;
            }

        }
        public void addRoomButton_Click(object sender, EventArgs e)
        {
            my_controller.IncDecRoom(true, AddrRow);
            AddRoom();
        }
        private void RoomGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            my_controller.IncDecRoom(false, AddrRow);
            extViews.RemoveAt(e.RowIndex);
            hoseViews.RemoveAt(e.RowIndex);
            lightViews.RemoveAt(e.RowIndex);
        }
        public void extView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if ((e.ColumnIndex == 7) && (e.RowIndex > -1))
            {
                DecMetric("extinguisher", currentRow);
                my_controller.ExtinguisherView_CellClick(sender, e);
            }
        }
        public void hoseView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if ((e.ColumnIndex == 4) && (e.RowIndex > -1))
            {
                DecMetric("hose", currentRow);
                my_controller.HoseView_CellClick(sender, e);
            }
        }
        public void lightView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if ((e.ColumnIndex == 10) && (e.RowIndex > -1))
            {
                DecMetric("light", currentRow);
                my_controller.LightView_CellClick(sender, e);
            }
        }

/**************************************** Setup/Manipulation *******************************************/
        public void ShowServiceItemView(String type, int i)
        {
            currentRow = i;
            switch (type)
            {
                case "extinguisher":
                    HideAllViews();
                    extViews[i].Visible = true;
                    AddItemButton.Text = "Add Extinguisher";
                    AddItemButton.Visible = true;
                    break;

                case "hose":
                    HideAllViews();
                    hoseViews[i].Visible = true;
                    AddItemButton.Text = "Add Hose";
                    AddItemButton.Visible = true;
                    break;

                case "light":
                    HideAllViews();
                    lightViews[i].Visible = true;
                    AddItemButton.Text = "Add Light";
                    AddItemButton.Visible = true;
                    break;

                case "none":
                    HideAllViews();
                    AddItemButton.Visible = false;
                    break;

            }
        }
        public void HideAllViews()
        {
            foreach (DataGridView ext in extViews)
                ext.Visible = false;
            foreach (DataGridView hose in hoseViews)
                hose.Visible = false;
            foreach (DataGridView light in lightViews)
                light.Visible = false;
        }
        public void DecMetric(String View, int index)
        {
            int n;
            switch (View)
            {
                   
                case "extinguisher":
                    n = Convert.ToInt32(RoomGridView.Rows[index].Cells["extCol"].Value.ToString());
                    --n;
                    RoomGridView.Rows[index].Cells["extCol"].Value = n.ToString();
                    break;
                case "hose":
                    n = Convert.ToInt32(RoomGridView.Rows[index].Cells["hoseCol"].Value.ToString());
                    --n;
                    RoomGridView.Rows[index].Cells["hoseCol"].Value = n.ToString();
                    break;
                case "light":
                    n = Convert.ToInt32(RoomGridView.Rows[index].Cells["lightCol"].Value.ToString());
                    --n;
                    RoomGridView.Rows[index].Cells["lightCol"].Value = n.ToString();
                    break;
            }
        }
        private DataGridView NewExtinguisherView()
        {
            #region Columns
            DataGridViewTextBoxColumn eIDCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn eRoom= new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn eLocCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn eSizeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn eTypeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn eModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn eSerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewButtonColumn eDel = new DataGridViewButtonColumn();

            eIDCol.HeaderText = "ID";
            eIDCol.Name = "eIDCol";
            eIDCol.Visible = false;

            eRoom.HeaderText = "Room";
            eRoom.Name = "eRoom";
            eRoom.Visible = false;
            eRoom.Width = 60;

            eLocCol.HeaderText = "Location";
            eLocCol.Name = "eLocCol";
            eLocCol.Width = 80;

            eSizeCol.HeaderText = "Size";
            eSizeCol.Name = "eSizeCol";
            eSizeCol.Width = 60;

            eTypeCol.HeaderText = "Type";
            eTypeCol.Name = "eTypeCol";
            eTypeCol.Width = 60;

            eModel.HeaderText = "Model";
            eModel.Name = "eModel";
            eModel.Width = 80;

            eSerial.HeaderText = "Serial";
            eSerial.Name = "eSerial";
            eSerial.Width = 150;

            eDel.HeaderText = "Delete";
            eDel.Name = "eDel";
            eDel.Width = 40;
            #endregion

            DataGridView ExtinguisherView = new DataGridView();
            ExtinguisherView.CellClick +=new DataGridViewCellEventHandler(extView_CellClick);
            ExtinguisherView.AllowUserToAddRows = false;
            ExtinguisherView.AllowUserToDeleteRows = false;
            ExtinguisherView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ExtinguisherView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            eIDCol,
            eLocCol,
            eSizeCol,
            eTypeCol,
            eModel,
            eSerial,
            eRoom,
            eDel});

            ExtinguisherView.Location = new System.Drawing.Point(5, 220);
            ExtinguisherView.Name = "ExtinguisherView";
            ExtinguisherView.Size = new System.Drawing.Size(525, 150);
            ExtinguisherView.TabIndex = 61;
            ExtinguisherView.Visible = false;
            

            return ExtinguisherView;
        }
        private DataGridView NewHoseView()
        {
            DataGridView HoseView= new System.Windows.Forms.DataGridView();
            HoseView.CellClick +=new DataGridViewCellEventHandler(hoseView_CellClick);
            #region Columns
            DataGridViewTextBoxColumn hID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn hRoom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn hLoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn hSerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewButtonColumn hDel = new DataGridViewButtonColumn();
            
            hID.HeaderText = "ID";
            hID.Name = "hID";
            hID.Visible = false;
            hRoom.HeaderText = "Room";
            hRoom.Name = "hRoom";
            hRoom.Visible = false;
            hRoom.Width = 60;
            hLoc.HeaderText = "Location";
            hLoc.Name = "hLoc";
            hLoc.Width = 80;
            hSerial.HeaderText = "Serial";
            hSerial.Name = "hSerial";
            hSerial.Width = 150;
            hDel.HeaderText = "Delete";
            hDel.Name = "hDel";
            hDel.Width = 40;
            #endregion

            HoseView.AllowUserToAddRows = false;
            HoseView.AllowUserToDeleteRows = false;
            HoseView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            HoseView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            hID,
            hLoc,
            hSerial,
            hRoom,
            hDel});

            HoseView.Location = new System.Drawing.Point(5, 220);
            HoseView.Name = "HoseView";
            HoseView.Size = new System.Drawing.Size(320, 150);
            HoseView.TabIndex = 65;
            HoseView.Visible = false;

            return HoseView;
        }
        private DataGridView NewLightView()
        {
            DataGridView LightView = new System.Windows.Forms.DataGridView();
            LightView.CellClick +=new DataGridViewCellEventHandler(lightView_CellClick);

            #region Colums
            DataGridViewTextBoxColumn lid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn lRoom = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn lLoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn lModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn lMake = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn lHeads = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn lPow = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn lVolts= new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewCheckBoxColumn lReqServ = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            DataGridViewTextBoxColumn lSerial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            DataGridViewButtonColumn lDel = new DataGridViewButtonColumn();

            lid.HeaderText = "ID";
            lid.Name = "lid";
            lid.Visible = false;
            lLoc.HeaderText = "Location";
            lLoc.Name = "lLoc";
            lLoc.Width = 80;
            lModel.HeaderText = "Model";
            lModel.Name = "lModel";
            lModel.Width = 80;
            lMake.HeaderText = "Make";
            lMake.Name = "lMake";
            lMake.Width = 60;
            lHeads.HeaderText = "Heads";
            lHeads.Name = "lHeads";
            lHeads.Width = 60;
            lPow.HeaderText = "Watts";
            lPow.Name = "lPow";
            lPow.Width = 60;
            lVolts.HeaderText = "Volts";
            lVolts.Name = "lVolts";
            lVolts.Width = 60;
            lReqServ.HeaderText = "Service";
            lReqServ.Name = "lReqServ";
            lReqServ.Width = 60;
            lSerial.HeaderText = "Serial";
            lSerial.Name = "lSerial";
            lSerial.Width = 150;
            lRoom.HeaderText = "Room";
            lRoom.Name = "lRoom";
            lRoom.Visible = false;
            lRoom.Width = 60;
            lDel.HeaderText = "Delete";
            lDel.Name = "lDel";
            lDel.Width = 40;
            #endregion

            LightView.AllowUserToAddRows = false;
            LightView.AllowUserToDeleteRows = false;
            LightView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            LightView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            lid,
            lLoc,
            lModel,
            lMake,
            lHeads,
            lPow,
            lVolts,
            lReqServ,
            lSerial,
            lRoom,
            lDel});

            LightView.Location = new System.Drawing.Point(5, 220);
            LightView.Name = "LightView";
            LightView.Size = new System.Drawing.Size(710, 150);
            LightView.TabIndex = 66;
            LightView.Visible = false;

            return LightView;
        }
    }
}
