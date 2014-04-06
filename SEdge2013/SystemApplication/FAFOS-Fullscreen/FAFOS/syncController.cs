using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace FAFOS
{
    class syncController
    {
        Users _users = new Users();
        String FranchiseeID = null;
        InspectionData XML = null;

        Thread listenThread;
        private static FromMobileView _fromMobileView;
        TCP_Model _TCPModel = null;
        Thread clientThread;

        public syncController(int UserId)
        {
            FranchiseeID = _users.getFranchiseeId(UserId.ToString());
            String connString = Properties.Settings.Default.FAFOS;
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand Franchisee_command = new SqlCommand("SELECT franchisee_id, company_name FROM Franchisee WHERE franchisee_id = " + FranchiseeID, con);
            SqlDataReader reader = Franchisee_command.ExecuteReader();
            reader.Read();
            String[] Franchisee_data = new String[2] { reader[0].ToString(), reader[1].ToString() };
            XML = new InspectionData(Franchisee_data);
        }

        public void syncToAndroid_Click(object sender, EventArgs e)
        {
            GenerateXMLData();
            SaveInspectionData();
            SendInspectionData();
        }

        public void syncFromAndroid_Click(object sender, EventArgs e)
        {
            FromMobileView fromMobileView = new FromMobileView();
            fromMobileView.Show();
        }

        public void GenerateXMLData()
        {
            // add clients nodes
            String connString = Properties.Settings.Default.FAFOS + " ; MultipleActiveResultSets=True";
            SqlConnection con = new SqlConnection(connString);
            con.Open();
            SqlCommand clients_command = new SqlCommand("SELECT client_id, account_name, address, province_id, city_id, postal_code FROM Client", con);
            SqlDataReader rd = clients_command.ExecuteReader();
            String[] par;
            while (rd.Read())
            {
                par = new String[3] { rd[0].ToString(), rd[1].ToString(), rd[2].ToString() + ", " 
                    + new Address().getProvince(rd[3].ToString())+ ", "+ new Address().getCity(rd[4].ToString()) +", "+ rd[5].ToString() };
                XML.addClient(par);
                // add client contracts

                SqlCommand client_contracts_command = new SqlCommand("SELECT client_contract_id, contract_nm, start_date, end_date, terms FROM Client_Contract WHERE client_id = " + rd[0], con);
                SqlDataReader rd1 = client_contracts_command.ExecuteReader();
                String[] Contrcat_data;
                while (rd1.Read())
                {
                    Contrcat_data = new String[5] { rd1[0].ToString(), rd1[1].ToString(), rd1[2].ToString(), rd1[3].ToString(), rd1[4].ToString() };
                    XML.addContract(Contrcat_data);
                    // add services addresses

                    SqlCommand services_addresses_command = new SqlCommand("SELECT service_address_id, address, postal_code, on_site_contact, city_id, province_id, country_id FROM Service_Address WHERE client_contract_id = " + rd1[0], con);
                    SqlDataReader rd2 = services_addresses_command.ExecuteReader();
                    String[] services_data;
                    while (rd2.Read())
                    {
                        services_data = new String[9] { rd2[0].ToString(), rd2[1].ToString(), rd2[2].ToString(), rd2[3].ToString(), 
                            new Address().getCity(rd2[4].ToString()), new Address().getProvince(rd2[5].ToString()), new Address().getCountry(rd2[6].ToString()), "", "" };
                        XML.addServiceAddress(services_data);
                        // add floors

                        SqlCommand floors_command = new SqlCommand("SELECT floor FROM Room WHERE service_addr_id = " + rd2[0], con);
                        SqlDataReader rd3 = floors_command.ExecuteReader();
                        String[] floors_data;
                        while (rd3.Read())
                        {
                            floors_data = new String[1] { rd3[0].ToString() };
                            XML.addFloors(floors_data);

                            // add rooms 

                            SqlCommand Rooms_command = new SqlCommand("SELECT room_id, room_num FROM Room WHERE floor = " + rd3[0].ToString() + " and service_addr_id = " + rd2[0], con);
                            SqlDataReader rd4 = Rooms_command.ExecuteReader();
                            String[] rooms_data;
                            while (rd4.Read())
                            {
                                rooms_data = new String[2] { rd4[0].ToString(), rd4[1].ToString() };
                                XML.addRooms(rooms_data);

                                // add Extinguishers
                                SqlCommand Extinguishers_command = new SqlCommand("SELECT bar_code, location, size, type, model, serial FROM Extinguisher WHERE room_id = " + rd4[0].ToString(), con);
                                SqlDataReader rd5 = Extinguishers_command.ExecuteReader();
                                String[] Extinguishers_data;
                                while (rd5.Read())
                                {
                                    Extinguishers_data = new String[6] { rd5[0].ToString(), rd5[1].ToString(), rd5[2].ToString(), rd5[3].ToString(), rd5[4].ToString(), rd5[5].ToString() };
                                    XML.addExtinguisher(Extinguishers_data);
                                }
                                rd5.Close();

                                // add FireHoseCabinet
                                SqlCommand FireHoseCabinet_command = new SqlCommand("SELECT bar_code, location, manufacture_date FROM Hose WHERE room_id = " + rd4[0].ToString(), con);
                                SqlDataReader rd6 = FireHoseCabinet_command.ExecuteReader();
                                String[] FireHoseCabinet_data;
                                while (rd6.Read())
                                {
                                    FireHoseCabinet_data = new String[3] { rd6[0].ToString(), rd6[1].ToString(), rd6[2].ToString() };
                                    XML.addFireHoseCabinet(FireHoseCabinet_data);
                                }
                                rd6.Close();

                                // add EmergencyLight
                                SqlCommand EmergencyLight_command = new SqlCommand("SELECT bar_code, location, model, make, heads, power, voltage FROM Light WHERE room_id = " + rd4[0].ToString(), con);
                                SqlDataReader rd7 = EmergencyLight_command.ExecuteReader();
                                String[] EmergencyLight_data;
                                while (rd7.Read())
                                {
                                    EmergencyLight_data = new String[7] { rd7[0].ToString(), rd7[1].ToString(), rd7[2].ToString(), rd7[3].ToString(), rd7[4].ToString(), rd7[5].ToString(), rd7[6].ToString() };
                                    XML.addEmergencyLight(EmergencyLight_data);
                                }
                                rd7.Close();
                            }
                            rd4.Close();
                        }
                        rd3.Close();
                    }
                    rd2.Close();
                }
                rd1.Close();
            }
            rd.Close();
            con.Close();
        }

        // Save the XML inspection data
        public void SaveInspectionData()
        {
            XML.SaveInspection();
            string message = "The inspection data has been saved successfully in\n" + System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
                   + "\\Resources\\inspection.xml";
            string caption = "Save Inspection Dialog Box";
            MessageBox.Show(message, caption);
        }

        // Send XML to Mobile Device
        public void SendInspectionData()
        {
            DialogResult dr = new DialogResult();
            ToMobileView SendInspectionDialog = new ToMobileView();
            dr = SendInspectionDialog.ShowDialog();
            if (dr == DialogResult.OK)
            {
                if (SendInspectionDialog.PortNumber.Text != "" & SendInspectionDialog.IPAddress.Text != "")
                {
                    try
                    {
                        int port = Convert.ToInt32(SendInspectionDialog.PortNumber.Text);
                        String IP = SendInspectionDialog.IPAddress.Text;
                        FAFOS.TCP_Model my_model = new FAFOS.TCP_Model(IP, port);

                        String XMLdata = XML.SendInspection();
                        my_model.Send_to_TCP(XMLdata);
                        my_model.shutdown();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Make sure the Mobile device is up and listening\r\nand the server IP address and port number are correct\n\n", "<<CONNECTION ERROR>>");
                    }
                }
                else
                {
                    MessageBox.Show("You must enter a valid port number and IP address");
                }
            }
            else if (dr == DialogResult.Cancel)
            {
                SendInspectionDialog.Dispose();
            }
        }


        public void Listen_btn_Click(object sender, EventArgs e)
        {
            // Deterime which view to control
            _fromMobileView = (FromMobileView)((Button)sender).FindForm();
            _fromMobileView.Disable_Listen();

            // Create a thread to accept from connected clients 
            this.listenThread = new Thread(ListenForClients);
            listenThread.IsBackground = true; // to stop all threads when application is terminated
            this.listenThread.Start();
        }

        public void ListenForClients()
        {
            // Create a model to listen from clients
            _TCPModel = new TCP_Model(Int32.Parse(_fromMobileView.GetPortNumber()));

            // Update the view
            UpdateServerIP(_TCPModel.ServerIP.ToString());

            while (true)
            {
                UpdateInfoBox("Server is waiting for new connection" + "\r\n");

                //blocks until a client has connected to the server
                Socket TCPsocket = _TCPModel.AcceptOneClient();

                //create a thread to handle communication with connected client                
                clientThread = new Thread(new ParameterizedThreadStart(Communications));
                clientThread.IsBackground = true; // to stop all threads when application is terminated
                clientThread.Start(TCPsocket);
            }
        }

        public void Communications(object socket)
        {
            Socket TCP_socket = (Socket)socket;

            // Create a new client object 
            TCP_Model _ClientModel = new TCP_Model(TCP_socket);

            UpdateInfoBox("The Mobile Device has been joined \r\n");
            string XMLDataFile = "";
            while (true)
            {
                //Wait for the client data

                string XMLData = _ClientModel.Get_From_TCP();   // block until client sends XML message
                if (XMLData != null)
                {
                    UpdateInfoBox("Device is sending\r\n");
                    UpdateCommBox(XMLData + "\r\n");
                    XMLDataFile += XMLData;

                }
                else
                {
                    UpdateInfoBox("No more data\r\n");
                    break;
                }
            }

            // save the file into C:\FAFOS\InspectionData\In
            System.IO.File.WriteAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
                   + "\\Resources\\inspection.xml", XMLDataFile);
            UpdateInfoBox("The Inspection results file is saved in \r\n" + System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
                   + "\\Resources\\inspection.xml");
        }


        public void UpdateInfoBox(string msg)
        {
            _fromMobileView.SetInfoBox(msg);
        }

        public void UpdateCommBox(string msg)
        {
            _fromMobileView.SetCommBox(msg);
        }

        public void UpdateServerIP(string msg)
        {
            _fromMobileView.setIPServer(msg);
        }


    }
}
