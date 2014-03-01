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

using InvoicePDF;

namespace FAFOS
{
    public partial class InspectionForm : Background
    {
        Thread listenThread;
        TCPModel _TCPModel = null;
        Thread clientThread;
        string userid;
        public InspectionForm(string id)
        {
            InitializeComponent();
            userid = id;
            setup(userid, "FAFOS Inspection Form");

            panel1.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width / 2 - Convert.ToInt32(panel1.Size.Width) / 2,
            System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height / 2 - Convert.ToInt32(panel1.Size.Height) / 2);

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
              + "\\Resources\\"+inspectionType.Text+"_" + DateTime.Today.ToShortDateString() + ".pdf");
            testDialog.ShowDialog(this);

          //  clientThread.Abort();
           // listenThread.Abort();
            

        }
        private void generateExtinguisher()
        {
            try
            {
               


                //start creating the PDF

                //Create a Catalog Dictionary
                CatalogDict catalogDict = new CatalogDict();

                //Create a Page Tree Dictionary
                PageTreeDict pageTreeDict = new PageTreeDict();

                //Create a Font Dictionary - Only the standard fonts Time, Helvetica and courier etc can be created by this method.
                //See Adobe doco for more info on other fonts
                FontDict TimesRoman = new FontDict();
                FontDict TimesItalic = new FontDict();
                FontDict TimesBold = new FontDict();
                FontDict Courier = new FontDict();

                //Create the info Dictionary
                InfoDict infoDict = new InfoDict();
                Invoice invoice = new Invoice();
                //Create the font called Times Roman
                TimesRoman.CreateFontDict("T1", "Times-Roman");

                //Create the font called Times Italic
                TimesItalic.CreateFontDict("T2", "Times-Italic");

                //Create the font called Times Bold
                TimesBold.CreateFontDict("T3", "Times-Bold");

                //Create the font called Courier
                Courier.CreateFontDict("T4", "Times-Roman");

                //Set the info Dictionary. xxx will be the invoice number
                infoDict.SetInfo( inspectionType.Text, "System Generated", "Fire-Alert");

                //Create a utility object
                Utility pdfUtility = new Utility();
                String FilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\" + inspectionType.Text + "_"+DateTime.Today.ToShortDateString()+".pdf";

                //Open a file specifying the file name as the output pdf file
                //String FilePath = @"C:\Users\Hassan\Desktop\Preview.pdf";

                FileStream file = new FileStream(FilePath, FileMode.Create);
                int size = 0;
                file.Write(pdfUtility.GetHeader("1.5", out size), 0, size);
                file.Close();

                //Finished the first step



                //Create a Page Dictionary , this represents a visible page
                PageDict page = new PageDict();
                ContentDict content = new ContentDict();

                //The page size object will hold all the page size information
                //also holds the dictionary objects for font, images etc.
                //A4 595,842
                //Letter 612,792
                PageSize pSize = new PageSize(612, 792); //A4 paper portrait in 1/72" measurements
                pSize.SetMargins(10, 10, 10, 10);

                //create the page main details
                page.CreatePage(pageTreeDict.objectNum, pSize);

                //add a page
                pageTreeDict.AddPage(page.objectNum);

                //add the fonts to this page
                page.AddResource(TimesRoman, content.objectNum);
                page.AddResource(TimesItalic, content.objectNum);
                page.AddResource(TimesBold, content.objectNum);
                page.AddResource(Courier, content.objectNum);

                //Create a Text And Table Object that presents the text elements in the page
                TextAndTables textAndtable = new TextAndTables(pSize);


                //create the reference to an image and the data that represents it
                String ImagePath2 = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\logo.jpg";   //file path to image source
                ImageDict I2 = new ImageDict();                     //new image dictionary object
                I2.CreateImageDict("I2", ImagePath2);                //create the object which describes the image
                page.AddImageResource(I2.PDFImageName, I2, content.objectNum);  //create a reference where the PDF can identify which object
                //describes the image when we want to draw it on the page

                /*
                 * draw the image to page (add the instruction to the content stream which says draw the image called I1 starting
                 * at X = 269, Y = 20 and with an ACTUAL image size on the page of w = 144 and h = 100)
                 */
                PageImages pi2 = new PageImages();
                content.SetStream(pi2.ShowImage("I2", 400, 680, 155, 85));   //tell the PDF we want to draw an image called 'I1', where and what size



                String address = new ServiceAddress().get(addressBox.SelectedValue.ToString());
                String[] ad = new String[6];
                ad = address.Split(',');

             //   client.get(contract.getClient(sales_order.getSAddress(_view.GetText())))
                String clientInfo = new Client().get(new ClientContract().getClient(addressBox.SelectedValue.ToString()));
                String[] client = new String[9];
                client = clientInfo.Split(',');

                //Add text to the page
                textAndtable.AddText(60, 70, "Report of Inspection/Test", 16, "T3", Align.LeftAlign);
                textAndtable.AddText(60, 85, "Extinguisher", 10, "T3", Align.LeftAlign);
                string format = "MMMM d, yyyy";
                textAndtable.AddText(60, 100, DateTime.Today.ToString(format), 10, "T3", Align.LeftAlign);
                textAndtable.AddText(60, 115, "Property", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(65, 130, ad[0], 10, "T4", Align.LeftAlign);
                textAndtable.AddText(65, 140, ad[3]+", "+ad[4], 10, "T4", Align.LeftAlign);
                textAndtable.AddText(65, 150, ad[5]+ " "+ad[1], 10, "T4", Align.LeftAlign);
                textAndtable.AddText(65, 180, ad[2], 10, "T4", Align.LeftAlign);
                textAndtable.AddText(65, 190, "N/A", 10, "T4", Align.LeftAlign);

                textAndtable.AddText(200, 115, "Owner/Agent", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(205, 130, client[0], 10, "T4", Align.LeftAlign);
                textAndtable.AddText(205, 140, client[1], 10, "T4", Align.LeftAlign);
                textAndtable.AddText(205, 150, client[6]+", "+client[7], 10, "T4", Align.LeftAlign);
                textAndtable.AddText(205, 180, client[5], 10, "T4", Align.LeftAlign);
                textAndtable.AddText(205, 190, client[3], 10, "T4", Align.LeftAlign);

                textAndtable.AddText(370, 160, "Conducted by:", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(370, 170, "Inspection Ref:", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(370, 180, "Contact:", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(450, 160, new Users().getName(Convert.ToInt32(userid)), 10, "T4", Align.LeftAlign);
                textAndtable.AddText(450, 170, "456", 10, "T4", Align.LeftAlign);
                textAndtable.AddText(450, 180, "N/A", 10, "T4", Align.LeftAlign);



                //create the colours for the round rectangles
                ColorSpec rrBorder = new ColorSpec(0, 0, 0);        //main border colour
                ColorSpec rrMainBG = new ColorSpec(204, 204, 204);  //background colour of the round rectangle
                ColorSpec rrTBBG = new ColorSpec(255, 255, 255);    //background colour of the rectangle on top of the round rectangle


                //create a new round rectangle object
                RoundRectangle rr = new RoundRectangle();

                //initialise special graphics state (graphics cursor)
                content.SetStream("q\r\n");


                content.SetStream(rr.DrawRoundRectangle(55, 460, 510, 130, 5, 0.55, 20, 90, 1, rrBorder, rrMainBG, rrTBBG));

                //begin drawing any required lines inside the round rectangle


                StraightLine line = new StraightLine();             //new straight line object
                ColorSpec vline = new ColorSpec(0, 0, 0);     //line colour - in this case Red

                //draw the line
                content.SetStream(line.DrawLine(180, 570, 180, 480, 1, vline));
                content.SetStream(line.DrawLine(320, 570, 320, 480, 1, vline));
                content.SetStream(line.DrawLine(390, 570, 390, 480, 1, vline));

                content.SetStream(line.DrawLine(55, 525, 565, 525, 1, vline));
               
                //close the graphics cursor in PDF
                content.SetStream("Q\r\n");

                //add in box headers and contents
                textAndtable.AddText(65, 215, "Signatures", 11, "T3", Align.LeftAlign);
                textAndtable.AddText(60, 232, "Inspector - Printed", 8, "T4", Align.LeftAlign);
                textAndtable.AddText(60, 245, new Users().getName(Convert.ToInt32(userid)), 8, "T3", Align.LeftAlign);
                textAndtable.AddText(60, 275, "Owner - Printed", 8, "T4", Align.LeftAlign);
                textAndtable.AddText(60, 288, client[5], 8, "T3", Align.LeftAlign);

                textAndtable.AddText(181, 232, "Inspector - Signature", 8, "T4", Align.LeftAlign);
                textAndtable.AddText(181, 275, "Owner - Signature", 8, "T4", Align.LeftAlign);

                textAndtable.AddText(321, 232, "Date", 8, "T4", Align.LeftAlign);
                textAndtable.AddText(321, 275, "Date", 8, "T4", Align.LeftAlign);

                textAndtable.AddText(391, 232, "Conditions", 8, "T4", Align.LeftAlign);
                textAndtable.AddText(391, 275, "Conditions", 8, "T4", Align.LeftAlign);

                textAndtable.AddText(65, 350, "Fire Extinguisher Inspection List", 11, "T3", Align.LeftAlign);

                //create the reference to an image and the data that represents it
                String ImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\Columns.jpg";   //file path to image source
                ImageDict I1 = new ImageDict();                     //new image dictionary object
                I1.CreateImageDict("I1", ImagePath);                //create the object which describes the image
                page.AddImageResource(I1.PDFImageName, I1, content.objectNum);  //create a reference where the PDF can identify which object
                //describes the image when we want to draw it on the page

                /*
                 * draw the image to page (add the instruction to the content stream which says draw the image called I1 starting
                 * at X = 269, Y = 20 and with an ACTUAL image size on the page of w = 144 and h = 100)
                 */
                PageImages pi = new PageImages();
                content.SetStream(pi.ShowImage("I1", 58, 355, 510, 78));   //tell the PDF we want to draw an image called 'I1', where and what size

                //Specify the color for the cell and the line
                ColorSpec cellColor = new ColorSpec(255, 255, 255);
                ColorSpec lineColor = new ColorSpec(0, 0, 0);


           //     textAndtable.AddText(50, 275, "Page:    1", 10, "T4", Align.LeftAlign);
                Align[] alignC1 = new Align[16];
                alignC1[0] = Align.LeftAlign;
                alignC1[1] = Align.LeftAlign;
                alignC1[2] = Align.LeftAlign;
                alignC1[3] = Align.LeftAlign;
                alignC1[4] = Align.CenterAlign;
                alignC1[5] = Align.LeftAlign;
                alignC1[6] = Align.LeftAlign;
                alignC1[7] = Align.CenterAlign;
                alignC1[8] = Align.CenterAlign;
                alignC1[9] = Align.CenterAlign;
                alignC1[10] = Align.CenterAlign;
                alignC1[11] = Align.CenterAlign;
                alignC1[12] = Align.CenterAlign;
                alignC1[13] = Align.CenterAlign;
                alignC1[14] = Align.CenterAlign;
                alignC1[15] = Align.CenterAlign;

               

                string url = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
                   + "\\Resources\\inspection.xml";
                XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(url);
                XmlElement docElement = doc.DocumentElement;

                // loop through all childNodes
                String floor="";
                uint height = 0 ;
                XmlNode start = docElement.FirstChild;
                foreach (XmlNode c1 in start)//contract
                {
                    XmlNode addresses = c1.FirstChild;
                    foreach (XmlNode c2 in addresses.ChildNodes)//address
                    {
                        if (Convert.ToInt32(c2.Attributes["id"].InnerText) == Convert.ToInt32(addressBox.SelectedValue))
                        {
                            XmlNode floors = c2.FirstChild;
                            foreach (XmlNode c3 in floors.ChildNodes)
                            {
                                //Fill in the parameters for the table
                                TableParams table2 = new TableParams(16, 25, 60, 80, 25, 30, 80, 55,
                                                                17, 17, 17, 17, 17, 17, 17, 17, 17);
                                table2.yPos = 340 - height;
                                table2.xPos = 49;
                                table2.rowHeight = 15;
                                textAndtable.SetParams(table2, cellColor, Align.LeftAlign, 3);

                                floor = Convert.ToInt32(c3.Attributes["id"].InnerText).ToString();
                                XmlNode rooms = c3.FirstChild;
                                foreach (XmlNode c4 in rooms.ChildNodes)
                                {
                                    XmlNode items = c4.FirstChild;
                                    foreach (XmlNode c5 in items.ChildNodes)
                                    {
                                        if (c5.Attributes["class"].InnerText == "com.sedge.fireinspectionapp.Extinguisher")
                                        {
                                            textAndtable.AddRow(false, 10, "T3", alignC1, false, floor, c5.Attributes["id"].InnerText,
                                                c5.Attributes["location"].InnerText, c5.Attributes["size"].InnerText,
                                                c5.Attributes["type"].InnerText, c5.Attributes["model"].InnerText, "", c5.Attributes["t1"].InnerText,
                                                c5.Attributes["t2"].InnerText, c5.Attributes["t3"].InnerText, c5.Attributes["t4"].InnerText, c5.Attributes["t5"].InnerText,
                                                c5.Attributes["t6"].InnerText, c5.Attributes["t7"].InnerText, c5.Attributes["t8"].InnerText, c5.Attributes["t9"].InnerText);
                                            height += table2.rowHeight;
                                        }
                                        //After drawing table and text add them to the page 
                                       

                                    }
                                }
                                height += 20;
                                content.SetStream(textAndtable.EndTable(lineColor, true));
                            }
                        }
                    }
                }
                textAndtable.AddText(65, 720, "Print ", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(125, 720, DateTime.Now.ToString("dd/MM/yyyy"), 10, "T4", Align.LeftAlign);
                textAndtable.AddText(500, 720, "Page 1 of 1", 10, "T3", Align.LeftAlign);
               
          
                content.SetStream(textAndtable.EndText());


                //All done - send the information to the PDF file

                size = 0;
                file = new FileStream(FilePath, FileMode.Append);
                file.Write(page.GetPageDict(file.Length, out size), 0, size);
                file.Write(content.GetContentDict(file.Length, out size), 0, size);
                file.Close();

                file = new FileStream(FilePath, FileMode.Append);
                file.Write(catalogDict.GetCatalogDict(pageTreeDict.objectNum, file.Length, out size), 0, size);
                file.Write(pageTreeDict.GetPageTree(file.Length, out size), 0, size);
                file.Write(TimesRoman.GetFontDict(file.Length, out size), 0, size);
                file.Write(TimesItalic.GetFontDict(file.Length, out size), 0, size);
                file.Write(TimesBold.GetFontDict(file.Length, out size), 0, size);
                file.Write(Courier.GetFontDict(file.Length, out size), 0, size);

                //write image dict
                 file.Write(I1.GetImageDict(file.Length, out size), 0, size);
                 file.Write(I2.GetImageDict(file.Length, out size), 0, size);

                file.Write(infoDict.GetInfoDict(file.Length, out size), 0, size);
                file.Write(pdfUtility.CreateXrefTable(file.Length, out size), 0, size);
                file.Write(pdfUtility.GetTrailer(catalogDict.objectNum, infoDict.objectNum, out size), 0, size);
                file.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not display the document because " + ex.ToString());
            }
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
