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
        private iTextSharp.text.Font TimesTitle = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 20, iTextSharp.text.Font.BOLD);
        private iTextSharp.text.Font WhiteTimes = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.TIMES_ROMAN, 10, iTextSharp.text.Font.BOLD, BaseColor.WHITE);
        

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
            if (inspectionType.Text == "Extinguisher Report")
            {
                generateExtinguisher();
            }
            Preview testDialog = new Preview(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
              + "\\Resources\\" + inspectionType.Text + "_" + DateTime.Today.ToString("yyyy-MM-dd") + ".pdf");
            testDialog.ShowDialog(this);

          //  clientThread.Abort();
           // listenThread.Abort();
            

        }

        private void generateExtinguisher()
        {
            try
            {
                document = new Document(PageSize.LETTER);
                
                String FilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\" + inspectionType.Text + "_" + DateTime.Today.ToString("yyyy-MM-dd") +".pdf";

                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(FilePath, FileMode.OpenOrCreate));
                document.Open();
                
                document.AddTitle("Report");
                document.AddSubject("Extinguisher");
                document.AddKeywords("Csharp, PDF, iText");
                document.AddAuthor("");
                document.AddCreator("");

                iTextSharp.text.Image pdfLogo = iTextSharp.text.Image.GetInstance(AppDomain.CurrentDomain.BaseDirectory + "\\Resources\\" + "logo.JPG");

                pdfLogo.Alignment = iTextSharp.text.Image.ALIGN_RIGHT;
                pdfLogo.ScaleAbsolute(150, 85);

                document.Add(pdfLogo);
                Paragraph preface = new Paragraph();
                
                preface.Add(new Paragraph("Report of Inpection/Test", TimesTitle));
                preface.Add(new Paragraph("Extinguisher", Times));
                preface.Add(new Paragraph(DateTime.Now.ToString("MMMM d, yyyy"), Times));
                preface.Add(new Paragraph("\n", Times));

                String address = new ServiceAddress().get(addressBox.SelectedValue.ToString());
                String[] ad = new String[6];
                ad = address.Split(',');

                String clientInfo = new Client().get(new ClientContract().getClient(addressBox.SelectedValue.ToString()));
                String[] client = new String[9];
                client = clientInfo.Split(',');
                
                PdfPTable addrTable = new PdfPTable(3);
                addrTable.HorizontalAlignment = Element.ALIGN_LEFT;
                addrTable.TotalWidth = 530f;
                addrTable.LockedWidth = true;
                
                float[] addrWidths = new float[] {100f, 100f, 100f};
                addrTable.SetWidths(addrWidths);

                addCell(addrTable, "Property", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_LEFT);
                addCell(addrTable, "Owner/Agent", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_LEFT);
                addCell(addrTable, " ", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_LEFT);

                addCell(addrTable, ad[0] + "\n" +
                                   ad[3] + ", " + ad[4] + "\n" +
                                   ad[5] + ", " + ad[1] + "\n"
                                   , 2, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);

                addCell(addrTable, client[0] + "\n" +
                                   client[1] + "\n" +
                                   client[6] + ", " + client[7] + "\n"
                                   , 2, 1, 0, BaseColor.WHITE, TimesRegular, PdfPCell.ALIGN_LEFT);

                addCell(addrTable, " ", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_LEFT);

                addCell(addrTable, ad[2] + "\n" +
                                   "N/A \n", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_LEFT);
                addCell(addrTable, client[5] + "\n" +
                                   client[3], 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_LEFT);
                addCell(addrTable, "Conducted by: " + new Users().getName(Convert.ToInt32(userid)) + "\n" +
                                    "Inspection Ref: " + "456" + "\n" + 
                                    "Contact: " + "N/A", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_LEFT);

                for (int i = 0; i < addrTable.Rows.Count; i++)
                    for (int j = 0; j < addrTable.Rows[i].GetCells().Length; j++)
                    {
                        if (addrTable.Rows[i].GetCells()[j] != null)
                            addrTable.Rows[i].GetCells()[j].Border = iTextSharp.text.Rectangle.NO_BORDER;
                    }

                document.Add(preface);

                document.Add(addrTable);
                document.Add(new Paragraph(" "));
                
                PdfPTable sigHead = new PdfPTable(1);
                sigHead.HorizontalAlignment = 0;
                sigHead.TotalWidth = 530f;
                sigHead.LockedWidth = true;
                float[] sigWidths = new float[] { 530f };
                sigHead.SetWidths(sigWidths);

                addCell(sigHead, "Signatures", 2, 1, 0, BaseColor.GRAY, Times, PdfPCell.ALIGN_LEFT);
                document.Add(sigHead);
                
                PdfPTable signaturesTable = new PdfPTable(4);
                signaturesTable.HorizontalAlignment = 0;
                signaturesTable.TotalWidth = 530f;
                signaturesTable.LockedWidth = true;
                float[] signaWidths = new float[] { 80f, 80f, 80f, 80f};
                signaturesTable.SetWidths(signaWidths);

                addCell(signaturesTable, "Inspector - Printed\n" + new Users().getName(Convert.ToInt32(userid)) + "\n\n\n", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_LEFT);
                addCell(signaturesTable, "Inspector - Signature\n", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_TOP);
                addCell(signaturesTable, "Date", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_TOP);
                addCell(signaturesTable, "Conditions", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_TOP);

                addCell(signaturesTable, "Owner - Printed\n" + client[5] + "\n\n\n", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_LEFT);
                addCell(signaturesTable, "Owner - Signature\n" + "", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_TOP);
                addCell(signaturesTable, "Date", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_TOP);
                addCell(signaturesTable, "Conditions", 2, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_TOP);

                document.Add(signaturesTable);

                sigHead.GetRow(0).GetCells()[0].Phrase = new Phrase("");
                document.Add(sigHead);
                document.Add(new Paragraph(" "));
                
                #region Inspection table

                PdfPTable table = new PdfPTable(16);
                table.HorizontalAlignment = 0;
                table.TotalWidth = 530f;
                table.LockedWidth = true;
                float[] widths = new float[] { 15f, 35f, 50f, 15f, 20f, 40f, 25f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f, 10f };
                table.SetWidths(widths);

                createHeader(table);
                
                string url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\inspection.xml";
                XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(url);
                XmlElement docElement = doc.DocumentElement;

                // loop through all childNodes
                String floor="";
                XmlNode start = docElement.FirstChild;
                foreach (XmlNode c1 in start)//contract
                {
                    XmlNode addresses = c1.FirstChild;
                    foreach (XmlNode c2 in addresses.ChildNodes)//address
                    {
                        int i = Convert.ToInt32(c2.Attributes["id"].InnerText);
                        int j = Convert.ToInt32(addressBox.SelectedValue);

                        if (Convert.ToInt32(c2.Attributes["id"].InnerText) == Convert.ToInt32(addressBox.SelectedValue))
                        {
                            XmlNode floors = c2.FirstChild;
                            foreach (XmlNode c3 in floors.ChildNodes)
                            {
                                floor = Convert.ToInt32(c3.Attributes["id"].InnerText).ToString();
                                XmlNode rooms = c3.FirstChild;

                                foreach (XmlNode c4 in rooms.ChildNodes)
                                {
                                    XmlNode items = c4.FirstChild;
                                    foreach (XmlNode c5 in items.ChildNodes)
                                    {
                                        if (c5.Attributes["class"].InnerText == "com.sedge.fireinspectionapp.Extinguisher")
                                        {
                                            addDataRow(table, c5.Attributes);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                document.Add(table);

                #endregion

                Paragraph footer = new Paragraph(document.RightMargin, "Print \t" + DateTime.Now.ToString("dd/MM/yyyy") + "\t Page 1 of 1", Times);

                document.Add(footer);

                document.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not display the document because " + ex.ToString());
            }
        }
       
        
        private void createHeader(PdfPTable table)
        {
            addCell(table, "Item\n#", 2, 1, 0, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Equip ID", 2, 1, 0, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Location", 2, 1, 0, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Size", 2, 1, 0, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Type", 2, 1, 0, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Manufacturer\nModel", 2, 1, 0, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Serial #", 2, 1, 0, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Inspection - Service", 1, 9, 0, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Hydro Test", 1, 1, 90, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "6 Year Insep", 1, 1, 90, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Weight", 1, 1, 90, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Bracket", 1, 1, 90, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Gauge", 1, 1, 90, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Pull Pin", 1, 1, 90, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Signage", 1, 1, 90, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Collar", 1, 1, 90, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);
            addCell(table, "Hose", 1, 1, 90, BaseColor.RED, WhiteTimes, PdfPCell.ALIGN_CENTER);

        }

        private void addDataRow(PdfPTable table, XmlAttributeCollection attrs)
        {
            addCell(table, "item #", 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, attrs["id"].InnerText, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, attrs["location"].InnerText, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, attrs["size"].InnerText, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, attrs["type"].InnerText, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, attrs["model"].InnerText, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, "serial #", 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, attrs["t1"].InnerText, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, attrs["t2"].InnerText, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, attrs["t3"].InnerText, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, attrs["t4"].InnerText, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, attrs["t5"].InnerText, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, attrs["t6"].InnerText, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, attrs["t7"].InnerText, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, attrs["t8"].InnerText, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);
            addCell(table, attrs["t9"].InnerText, 1, 1, 0, BaseColor.WHITE, Times, PdfPCell.ALIGN_CENTER);

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
