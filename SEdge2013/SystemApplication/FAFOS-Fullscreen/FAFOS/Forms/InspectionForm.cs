using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Xml;
using System.IO;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace FAFOS
{
    public partial class InspectionForm : Background
    {
        Thread listenThread;
        TCPModel _TCPModel = null;
        Thread clientThread;
        string userid;

        Document document;

        private iTextSharp.text.Font Times = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD);
        private iTextSharp.text.Font TimesRegular = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, iTextSharp.text.Font.NORMAL);
        private iTextSharp.text.Font TimesSmall = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, iTextSharp.text.Font.NORMAL);
        private iTextSharp.text.Font TimesTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 20, iTextSharp.text.Font.BOLD);
        
        private iTextSharp.text.Font WhiteTimes = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
        

        public InspectionForm(string id)
        {
            InitializeComponent();
            userid = id;
            setup(userid, "FAFOS Inspection Form");

            //pnlInspection.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width / 2 - Convert.ToInt32(pnlInspection.Size.Width) / 2,
            //    System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height / 2 - Convert.ToInt32(pnlInspection.Size.Height) / 2);

            DataTable dt = new ServiceAddress().getAddresses();
            addressBox.DataSource = dt;
            addressBox.DisplayMember = "address";
            addressBox.ValueMember = "id";


            // Create a thread to accept from connected clients 
            this.listenThread = new Thread(ListenForClients);
            listenThread.IsBackground = true; // to stop all threads when application is terminated
            this.listenThread.Start();
           // ListenForClients();
        }

        public void ListenForClients()
        {
            // Create a model to listen from clients
            _TCPModel = new TCPModel(Int32.Parse("3000"));

//            while (true)
         //   {
                //blocks until a client has connected to the server
                Socket TCPsocket = _TCPModel.AcceptOneClient();

                //create a thread to handle communication with connected client                
                clientThread = new Thread(new ParameterizedThreadStart(Communications));
                clientThread.IsBackground = true; // to stop all threads when application is terminated
                clientThread.Start(TCPsocket);
           // }
        }
        public void Communications(object socket)
        {
            Socket TCP_socket = (Socket)socket;

            // Create a new client object 
            ClientModel _ClientModel = new ClientModel(TCP_socket);

            while (true)
            {
                //Wait for the client data
                string XMLData = _ClientModel.ReceiveFromClient();   // block until client sends XML message
                if (XMLData != null)
                {
                    string url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
                   + "\\Resources\\inspection.xml";
                    using (StreamWriter writer = new StreamWriter(url))
                    {
                        writer.Write(XMLData);
                        writer.Flush();
                    }
                    break;
                }
                else
                {
                    break;
                }
            }
        }

        private void generate_btn_Click(object sender, EventArgs e)
        {
            if (inspectionType.Text != "")
            {
                generateReport();
            }

            Preview testDialog = new Preview(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
              + "\\Resources\\" + inspectionType.Text + "_" + DateTime.Today.ToString("yyyy-MM-dd") + ".pdf");
            testDialog.ShowDialog(this);

          //  clientThread.Abort();
           // listenThread.Abort();
            

        }

        private void generateReport()
        {
            try
            {
                String equipmentName = "";
                
                document = new Document(PageSize.LETTER);
                
                String FilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\" + inspectionType.Text + "_" + DateTime.Today.ToString("yyyy-MM-dd") +".pdf";

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(FilePath, FileMode.OpenOrCreate));
                document.Open();
                
                document.AddTitle("Report");
                document.AddSubject("Equipment Report");
                document.AddKeywords("Csharp, PDF, iText");
                document.AddAuthor("");
                document.AddCreator("");

                iTextSharp.text.Image pdfLogo = iTextSharp.text.Image.GetInstance(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\" + "logo.JPG");

                pdfLogo.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
                pdfLogo.ScaleAbsolute(150, 85);

                document.Add(pdfLogo);
                Paragraph preface = new Paragraph("Fire-Alert" + "\n" + "Report of " + inspectionType.Text + "\n", TimesTitle);

                preface.Alignment = Element.ALIGN_CENTER;

                document.Add(preface);
                document.Add(new Paragraph(" "));

                #region Inspection table

                PdfPTable inspectionTable = new PdfPTable(1);

                if (inspectionType.Text.Contains("Extinguisher"))
                    equipmentName = "Extinguisher";
                else if (inspectionType.Text.Contains("Hose"))
                    equipmentName = "FireHoseCabinet";
                else if (inspectionType.Text.Contains("Light"))
                    equipmentName = "EmergencyLight";
                
                // Load XML inspection file from resources
                string url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\inspection.xml";
                XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(url);
                XmlElement docElement = doc.DocumentElement;

                // loop through all childNodes
                XmlNode start = docElement.FirstChild;              // franchisee
                foreach (XmlNode c1 in start)                       // contracts
                {
                    foreach (XmlNode c2 in c1.ChildNodes)        // addresses
                    {

                        // Skip if not matching contract ID  
                        if (Convert.ToInt32(c2.Attributes["id"].InnerText) == Convert.ToInt32(addressBox.SelectedValue))
                        {
                            Console.WriteLine(Convert.ToInt32(c1.Attributes["id"].InnerText));
                            Console.WriteLine(Convert.ToInt32(addressBox.SelectedValue));

                            #region Address info table

                            PdfPTable addrTable = new PdfPTable(4);
                            addrTable.HorizontalAlignment = Element.ALIGN_LEFT;
                            addrTable.TotalWidth = 530f;
                            addrTable.LockedWidth = true;

                            float[] addrWidths = new float[] { 50f, 100f, 50f, 100f };
                            addrTable.SetWidths(addrWidths);

                            XmlAttributeCollection billTo = c1.ParentNode.Attributes;
                            XmlAttributeCollection location = c2.Attributes;

                            string[] billToAddr = billTo["address"].InnerText.Split(',');

                            addCell(addrTable, "Bill To:", 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_RIGHT);
                            addCell(addrTable, billTo["name"].InnerText, 1, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);

                            addCell(addrTable, "Location:", 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_RIGHT);

                            addCell(addrTable, location["contact"].InnerText, 1, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);
                            addCell(addrTable, " ", 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_RIGHT);
                            addCell(addrTable, billToAddr[0], 1, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);
                            
                            addCell(addrTable, " ", 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_RIGHT);
                            addCell(addrTable, location["address"].InnerText, 1, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);
                            addCell(addrTable, " ", 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_RIGHT);
                            addCell(addrTable, billToAddr[2] + "," + billToAddr[1], 1, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);

                            addCell(addrTable, " ", 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_RIGHT);
                            addCell(addrTable, location["city"].InnerText, 1, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);
                            addCell(addrTable, " ", 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_RIGHT);
                            addCell(addrTable, billToAddr[3], 1, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);

                            addCell(addrTable, " ", 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_RIGHT);
                            addCell(addrTable, location["postalCode"].InnerText, 1, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);

                            addCell(addrTable, "Tel:", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_RIGHT);
                            addCell(addrTable, new Franchisee().getAddress(Convert.ToInt32(start.Attributes["id"].InnerText))[0], 2, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);

                            String clientInfo = new Client().get(new ClientContract().getClient(addressBox.SelectedValue.ToString()));
                            String[] client = new String[9];
                            client = clientInfo.Split(',');

                            if (client.Length < 6)
                                client = new String[6] {"", "", "", "", "", "No client found" };

                            addCell(addrTable, "Contact:", 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_RIGHT);
                            addCell(addrTable, client[5], 1, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);
                            addCell(addrTable, "Tel:", 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_RIGHT);
                            addCell(addrTable, client[3], 1, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);

                            addCell(addrTable, " ", 4, 4, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);

                            string technicianID = c2.Attributes["InspectorID"].InnerText;

                            string technicianName = "";
                            try
                            {
                                technicianName = new Users().getName(Convert.ToInt32(technicianID));
                            }
                            catch   {}

                            if (technicianName == null || technicianName == "")
                                technicianName = "Technician Not Found";

                            addCell(addrTable, "Technician: ", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_RIGHT);
                            addCell(addrTable, technicianName, 2, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);

                            addCell(addrTable, "Date: ", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_RIGHT);
                            addCell(addrTable, DateTime.Now.ToString("MMMM d, yyyy"), 2, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);

                            for (int i = 0; i < addrTable.Rows.Count; i++)
                                for (int j = 0; j < addrTable.Rows[i].GetCells().Length; j++)
                                {
                                    if (addrTable.Rows[i].GetCells()[j] == null)
                                        continue;

                                    if (j % 2 != 0)     // Add bottom border to info cells
                                        addrTable.Rows[i].GetCells()[j].Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                                    else                // Remove bottom border from titles
                                        addrTable.Rows[i].GetCells()[j].Border = iTextSharp.text.Rectangle.NO_BORDER;
                                }

                            #endregion

                            document.Add(addrTable);

                            foreach (XmlNode c3 in c2.ChildNodes)   // floors 
                            {
                                foreach (XmlNode c4 in c3.ChildNodes)    //rooms
                                {
                                    bool isFirstEquipment = true;
                                    int itemNum = 1;
                                    foreach (XmlNode c5 in c4.ChildNodes)    // equipment
                                    {
                                        if (c5.Name.Equals(equipmentName))
                                        {
                                            #region Set up header

                                            if (isFirstEquipment)   // Set up table header based on first piece of equipment
                                            {
                                                isFirstEquipment = false;

                                                inspectionTable = new PdfPTable(c5.Attributes.Count + c5.ChildNodes.Count + 1);
                                                inspectionTable.HorizontalAlignment = 0;
                                                inspectionTable.TotalWidth = 530f;
                                                inspectionTable.LockedWidth = true;
                                                float []iWidths = new float[c5.Attributes.Count + c5.ChildNodes.Count + 1];

                                                iWidths[0] = 20f;
                                                for (int i = 1; i < iWidths.Length; i++)
                                                {
                                                    if (i < c5.Attributes.Count + 1)
                                                        iWidths[i] = 35f;
                                                    else
                                                        iWidths[i] = 20f;
                                                }
                                                inspectionTable.SetWidths(iWidths);

                                                createHeader(inspectionTable, c5);
                                            }

                                            #endregion

                                            addEquipmentRow(inspectionTable, c5, itemNum);
                                            itemNum++;
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
                document.Add(new Paragraph(" "));
                document.Add(inspectionTable);

                #endregion

                document.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not display the document because " + ex.ToString());
            }
        }
        private void createHeader(PdfPTable table, XmlNode equipment)
        {
            // Equipment info titles
            addCell(table, "Item #", 2, 1, 0, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            for (int i = 0; i < equipment.Attributes.Count; i++)
                addCell(table, formatTitle(equipment.Attributes[i].Name), 2, 1, 0, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);

            // Add cell header for extinguisher 
            int rotation = 0, 
                rowSpan = 2;
            if (equipment.Name.Equals("Extinguisher"))
            {
                addCell(table, "Inspection - Service", 1, equipment.ChildNodes.Count, 0, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
                rotation = 90;
                rowSpan = 1;
            }

            // Inspection titles
            foreach (XmlNode inspection in equipment.ChildNodes)
                addCell(table, inspection.Attributes["name"].InnerText, rowSpan, 1, rotation, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);

        }

        /// <summary>
        /// Formats a string to that there are spaces before capital letters
        /// </summary>
        /// <param name="title">The string to be formatted</param>
        /// <returns>The formatted string</returns>
        private string formatTitle(string title)
        {
            string formatted = "";

            formatted = "" + Char.ToUpper(title[0]);

            for (int i = 1; i < title.Length; i++)
            {
                if (Char.IsUpper(title[i])) 
                    formatted += " ";

                formatted += title[i];
            }

            return formatted;
                 
        }


        private void addEquipmentRow(PdfPTable table, XmlNode equip, int itemNum)
        {
            // Add equipment info cells
            addCell(table, "" + itemNum, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            for (int i = 0; i < equip.Attributes.Count; i++ )
                addCell(table, equip.Attributes[i].InnerText, 1, 1, 0, BaseColor.WHITE, TimesSmall, PdfPCell.ALIGN_CENTER);

            // Add cells for test results
            XmlNodeList inspections = equip.ChildNodes;
            foreach (XmlNode inspection in inspections)
                addCell(table, inspection.Attributes["testResult"].InnerText, 1, 1, 0, BaseColor.WHITE, TimesSmall, PdfPCell.ALIGN_CENTER);
            
            // Add cells for mistakes
            foreach (XmlNode inspection in inspections)
                if (inspection.Attributes["testNote"].InnerText != "")
                    addCell(table,inspection.Attributes["name"].InnerText + ": " + inspection.Attributes["testNote"].InnerText, 1, table.NumberOfColumns, 0, BaseColor.WHITE, TimesSmall, PdfPCell.ALIGN_LEFT);
            
        }

        private void addCell(PdfPTable table, string text, int rowspan, int colpan, int rotate, BaseColor backgroundColor, iTextSharp.text.Font font, int hAlignment)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, font));
            cell.Rowspan = rowspan;
            cell.Colspan = colpan;
            cell.HorizontalAlignment = hAlignment;
            cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            cell.Rotation = rotate;
            cell.BackgroundColor = backgroundColor;
            table.AddCell(cell);
        }
        
    }



    class ClientModel
    {
        private Socket ClientSocket;
        private const int BufferSize = 15000;
        private byte[] buffer = new byte[BufferSize];

        public ClientModel(Socket s)
        {
            this.ClientSocket = s;
            ClientIP = ((IPEndPoint)s.RemoteEndPoint).Address;
            ClientPort = ((IPEndPoint)s.RemoteEndPoint).Port;
        }

        public string ReceiveFromClient()
        {
            byte[] data = new byte[BufferSize];
            int byteRecv = 0;
            string receivedData = null;
            try
            {
                byteRecv = ClientSocket.Receive(data, 0, BufferSize, SocketFlags.None);
                if (byteRecv > 0)
                {
                    return receivedData = Encoding.ASCII.GetString(data, 0, byteRecv);
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public IPAddress ClientIP { get; set; }
        public int ClientPort { get; set; }
        public IPEndPoint clientEndPoint { get; set; }

    }
    class TCPModel
    {
        private Socket listening_RTSPsocket = null;
        private IPAddress ServerIPAddr = null;
        private int RTSPport;

        public TCPModel(int GivenPort)
        {
            // Creating TCP listening socket
            if (listening_RTSPsocket == null)
                listening_RTSPsocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Creating Endpoint to the localhost address with the given port number
                IPHostEntry host;
                host = Dns.GetHostEntry(Dns.GetHostName());
                foreach (IPAddress ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        ServerIP = ip;
                    }
                }

                RTSPport = GivenPort;
                IPEndPoint listenEndPoint = new IPEndPoint(ServerIPAddr, RTSPport);

                // Bind server socket to Endpoint object
                listening_RTSPsocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                listening_RTSPsocket.Bind(listenEndPoint);
                
                listening_RTSPsocket.Listen(int.MaxValue);
            
            
        }
       ~TCPModel()
        {
            listening_RTSPsocket.Close();
            
        }

        public Socket AcceptOneClient()
        {
            //blocks until a client has connected to the server
            return this.listening_RTSPsocket.Accept();
        }

        public IPAddress ServerIP
        {
            get { return ServerIPAddr; }
            set { ServerIPAddr = value; }
        }

    }
}
