using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using InvoicePDF;

namespace FAFOS
{
    public partial class Statements : FAFOS.Background
    {
        int userid;

        public Statements(int id)
        {
            InitializeComponent();

            userid = id;
            setup(userid.ToString(), "FAFOS Statement");

            pnlStatement.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width/2-Convert.ToInt32(pnlStatement.Size.Width)/2,
                System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height / 2 - Convert.ToInt32(pnlStatement.Size.Height) / 2);


            string [] clients = new Invoice().getOutstandingClients();
            string[] names = new string[clients.Length];
            DataTable db = new DataTable();
            db.Columns.Add("id");
            db.Columns.Add("name");
            db.Rows.Add();
            for (int i = 0; i < clients.Length; i++)
            {
                names[i] = new Client().getName(clients[i]);
                db.Rows.Add(clients[i], names[i]);
            }
            cbClients.DataSource = db;
            cbClients.DisplayMember = "name";
            cbClients.ValueMember = "id";
        }

        private void btnStatement_Click(object sender, EventArgs e)
        {
           
            if (cbClients.Text != "")
            {

                generateStatement();
               // MessageBox.Show(cbClients.Text + " -/ " + cbClients.SelectedValue);

                Preview testDialog = new Preview(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory)
                + "\\Resources\\Statement_"+cbClients.SelectedValue+".pdf");
                 testDialog.ShowDialog(this);
            }
          
           
        }

        
        private void generateStatement()
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
                infoDict.SetInfo("Statement of " + cbClients.Text, "System Generated", "Fire-Alert");

                //Create a utility object
                Utility pdfUtility = new Utility();
                String FilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\Statement_"+cbClients.SelectedValue+".pdf";

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
                String ImagePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\logo.jpg";   //file path to image source
                ImageDict I1 = new ImageDict();                     //new image dictionary object
                I1.CreateImageDict("I1", ImagePath);                //create the object which describes the image
                page.AddImageResource(I1.PDFImageName, I1, content.objectNum);  //create a reference where the PDF can identify which object
                //describes the image when we want to draw it on the page

                /*
                 * draw the image to page (add the instruction to the content stream which says draw the image called I1 starting
                 * at X = 269, Y = 20 and with an ACTUAL image size on the page of w = 144 and h = 100)
                 */
                PageImages pi = new PageImages();
                content.SetStream(pi.ShowImage("I1", 450, 690, 155, 85));   //tell the PDF we want to draw an image called 'I1', where and what size



                String clientInfo = new Client().get(cbClients.SelectedValue.ToString());
                String[] client = new String[9];
                client = clientInfo.Split(',');

                String franchiseeInfo = new Franchisee().get(new ClientContract().getFranchiseeOfClient(cbClients.SelectedValue.ToString()));
                String[] franchisee = new String[6];
                franchisee = franchiseeInfo.Split(',');

                String userInfo = new Users().get(userid.ToString());
                String[] user = new String[4];
                user = userInfo.Split(',');

                //Add text to the page
                textAndtable.AddText(260, 50, "STATEMENT", 20, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 80, franchisee[5], 16, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 90, franchisee[0], 10, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 100, franchisee[2] + ", " + franchisee[3] + ", " + franchisee[4] + " " + franchisee[1], 10, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 110, user[2], 10, "T3", Align.LeftAlign);
               // textAndtable.AddText(50, 60, "Total: " + txtTotal.Text, 10, "T3", Align.LeftAlign);


                textAndtable.AddText(50, 190, client[0], 10, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 205, client[5], 10, "T4", Align.LeftAlign);
                textAndtable.AddText(50, 215, client[1], 10, "T4", Align.LeftAlign);
                textAndtable.AddText(50, 225, client[6] + ", " + client[7] + " " + client[2], 10, "T4", Align.LeftAlign);
                textAndtable.AddText(50, 235, client[8], 10, "T4", Align.LeftAlign);

                textAndtable.AddText(430, 150, "PLEASE RETURN THIS PORTION WITH", 8, "T4", Align.LeftAlign);
                textAndtable.AddText(430, 160, "YOUR PAYMENT", 8, "T4", Align.LeftAlign);
                textAndtable.AddText(400, 175, client[0], 10, "T3", Align.LeftAlign);
                textAndtable.AddText(430, 210, "IF PAYING BY INVOICE CHECK", 8, "T4", Align.LeftAlign);
                textAndtable.AddText(430, 220, "INDIVIDUAL INVOICES PAID", 8, "T4", Align.LeftAlign);
                textAndtable.AddText(435, 240, "AMOUNT REMITTED __________", 8, "T4", Align.LeftAlign);

                Align[] alignC = new Align[1];
                alignC[0] = Align.CenterAlign;

                //Specify the color for the cell and the line
                ColorSpec cellColor = new ColorSpec(255, 255, 255);
                ColorSpec lineColor = new ColorSpec(1, 1, 1);

                //Fill in the parameters for the table
                TableParams table = new TableParams(1, 100);
                table.yPos = 700;
                table.xPos = 50;
                table.rowHeight = 15;

                //Set the parameters of this table
                textAndtable.SetParams(table, cellColor, Align.RightAlign, 3);
                textAndtable.AddRow(false, 10, "T3", alignC, false, "Statement Date");
                textAndtable.AddRow(false, 10, "T4", alignC, false, DateTime.Today.ToShortDateString());
                
                //After drawing table and text add them to the page 
                content.SetStream(textAndtable.EndTable(lineColor, true));

                table.yPos = 630;
                table.xPos = 0;
                //Set the parameters of this table
                textAndtable.SetParams(table, cellColor, Align.CenterAlign, 3);
                textAndtable.AddRow(false, 10, "T3", alignC, false, "Statement Date");
                textAndtable.AddRow(false, 10, "T4", alignC, false, DateTime.Today.ToShortDateString());

                //After drawing table and text add them to the page 
                content.SetStream(textAndtable.EndTable(lineColor, true));

                textAndtable.AddText(50, 275, "Page:    1", 10, "T4", Align.LeftAlign);
                Align[] alignC1 = new Align[5];
                alignC1[0] = Align.CenterAlign;
                alignC1[1] = Align.CenterAlign;
                alignC1[2] = Align.CenterAlign;
                alignC1[3] = Align.CenterAlign;
                alignC1[4] = Align.CenterAlign;

                Align[] alignC2 = new Align[5];
                alignC2[0] = Align.LeftAlign;
                alignC2[1] = Align.LeftAlign;
                alignC2[2] = Align.RightAlign;
                alignC2[3] = Align.RightAlign;
                alignC2[4] = Align.CenterAlign;

                //Fill in the parameters for the table
                TableParams table2 = new TableParams(5, 120,100,100,100,40);
                table2.yPos = 510;
                table2.xPos = 37;
                table2.rowHeight = 15;

                //Set the parameters of this table
                textAndtable.SetParams(table2, cellColor, Align.LeftAlign, 3);
               // MessageBox.Show("\u221A");
                textAndtable.AddRow(false, 10, "T3", alignC1, false, "Statement Date", "Invoice No.", "Balance", "Total Due", "");
                String[] rows = new Invoice().getOutstandingInvoices(cbClients.SelectedValue.ToString());
                double total = 0;
                double interestTotal = 0;
                double interest = 0;
                double below30 = 0;
                double between3160 = 0;
                double over60 = 0;
                DateTime interestDate = DateTime.Today.Date.AddDays(-30);
                for (int i = 0; i < rows.Length; i++)
                {
                    String[] cells = new String[3];
                    cells = rows[i].Split(',');
                    total += Convert.ToDouble(cells[2]);
                    if (Convert.ToDateTime(cells[0]) < interestDate)
                        interestTotal += Convert.ToDouble(cells[2]);
                    if (Convert.ToDateTime(cells[0]) > interestDate)
                        below30 += Convert.ToDouble(cells[2]);
                    if (Convert.ToDateTime(cells[0]) < DateTime.Today.Date.AddDays(-30) && Convert.ToDateTime(cells[0]) >= DateTime.Today.Date.AddDays(-60))
                        between3160 += Convert.ToDouble(cells[2]);
                    if (Convert.ToDateTime(cells[0]) < DateTime.Today.Date.AddDays(-60))
                        over60 += Convert.ToDouble(cells[2]);
                    textAndtable.AddRow(false, 10, "T4", alignC2, false, cells[0], cells[1], cells[2], total.ToString(), "");
                }
                interest = interestTotal * 0.02;
                total += interest;
                below30 += interest;
                textAndtable.AddRow(false, 10, "T4", alignC2, false, "2% interest on balance over 30 days", "", (interest).ToString(), total.ToString(), "");

                //After drawing table and text add them to the page 
                content.SetStream(textAndtable.EndTable(lineColor, true));

                Align[] alignC3 = new Align[5];
                alignC3[0] = Align.LeftAlign;
                alignC3[1] = Align.RightAlign;
                alignC3[2] = Align.RightAlign;
                alignC3[3] = Align.RightAlign;
                alignC3[4] = Align.CenterAlign;

                //Fill in the parameters for the table  120,100,100,100,40 = 460
                TableParams table3 = new TableParams(5, 100, 90, 90, 90, 90);
                table3.yPos = 100;
                table3.xPos = 37;
                table3.rowHeight = 15;

                //Set the parameters of this table
                textAndtable.SetParams(table3, cellColor, Align.LeftAlign, 3);
                // MessageBox.Show("\u221A");
                textAndtable.AddRow(false, 10, "T3", alignC3, false, "Age", "Current ", "31-60 ", "Over 60 ", "Total");
                textAndtable.AddRow(false, 10, "T3", alignC3, false, "Amount", below30.ToString(), between3160.ToString(), over60.ToString(), total.ToString());
                content.SetStream(textAndtable.EndTable(lineColor, true));
              
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
}
