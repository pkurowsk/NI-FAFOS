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
    public partial class InvoiceForm : FAFOS.Background
    {
        InvoiceController my_controller;
        Term t;
        Users user;
        private int userid;
        public InvoiceForm(int id)
        {
            InitializeComponent();


            user = new Users();

           
            my_controller = new InvoiceController();

            this.Find_btn.Click += new System.EventHandler(my_controller.Find_btn_Click);
            this.Send_btn.Click += new System.EventHandler(my_controller.Send_btn_Click);
            this.Preview_btn.Click += new System.EventHandler(my_controller.Preview_btn_Click);
            Issued.Value = DateTime.Today;

            //Combobox datasource fill in
            t = new Term();
            DataTable ts = new DataTable();
            ts = t.get().Tables[0];
            txtTerm.DataSource = ts;
            txtTerm.DisplayMember = ts.Columns[0].ToString();

            //User label
            userid = id;
            setup(userid.ToString(), "FAFOS Invoice Form");


            DataTable dt = new SalesOrder().getDone(userid.ToString()).Tables[0];
            DataTable orders = dt.Clone();
            orders.Rows.Add(new String[] { "", "0" });
            foreach (DataRow r in dt.Rows)
                orders.ImportRow(r);

            txtSalesOrder.DataSource = orders;
            txtSalesOrder.DisplayMember = orders.Columns[0].ToString();
            txtSalesOrder.ValueMember = orders.Columns[1].ToString();
        }
        private void AdjustWidthComboBox_DropDown(object sender, EventArgs e)
        {
            ComboBox senderComboBox = (ComboBox)sender;
            int width = senderComboBox.DropDownWidth;
            Graphics g = senderComboBox.CreateGraphics();
            Font font = senderComboBox.Font;
            int vertScrollBarWidth =
                (senderComboBox.Items.Count > senderComboBox.MaxDropDownItems)
                ? SystemInformation.VerticalScrollBarWidth : 0;

            int newWidth;
            foreach (object item in ((ComboBox)sender).Items)
            {
                // instead of treating an item as a string, treat it as an object and use GetItemText to fetch the item's textvalue
                string s = senderComboBox.GetItemText(item);
                newWidth = (int)g.MeasureString(s, font).Width
                    + vertScrollBarWidth;
                if (width < newWidth)
                {
                    width = newWidth;
                }
            }
            senderComboBox.DropDownWidth = width;
        }
        private void View_Load(object sender, EventArgs e)
        {

        }

        public String GetMsg()
        {
            return this.txtTerm.Text.ToString();
        }
        public int getUserId()
        {
            return userid;
        }

        public String GetText()
        {
            return this.txtSalesOrder.SelectedValue.ToString();
        }
        public String GetTermText()
        {
            return this.txtTerm.Text.ToString();
        }
        public String GetIssuedText()
        {
            return this.Issued.Value.ToShortDateString();
        }
        public String GetTotalText()
        {
            return this.txtTotal.Text.ToString();
        }
        public String GetPaymentText()
        {
            return this.txtAmount.Text.ToString();
        }
        public String GetTypeText()
        {
            return this.txtType.Text.ToString();
        }
        public String GetRemarksText()
        {
            return this.txtRemarks.Text.ToString();
        }

        // update the view
        public void Preview(String address, String clientInfo, String franchiseeInfo)
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
                Courier.CreateFontDict("T4", "Courier");

                //Set the info Dictionary. xxx will be the invoice number
                infoDict.SetInfo("Invoice " + invoice.getID(), "System Generated", "Fire-Alert");

                //Create a utility object
                Utility pdfUtility = new Utility();
                String FilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\Invoice.pdf";

                //Open a file specifying the file name as the output pdf file

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
                content.SetStream(pi.ShowImage("I1", 400, 680, 155, 85));   //tell the PDF we want to draw an image called 'I1', where and what size

                String[] sAd = new String[6];
                sAd = address.Split(',');

                String[] client = new String[9];
                client = clientInfo.Split(',');
                
                
                String[] franchisee = new String[6];
                franchisee = franchiseeInfo.Split(',');

                String userInfo = new Users().get(userid.ToString());
                String[] user = new String[4];
                user = userInfo.Split(',');

                //Add text to the page
                textAndtable.AddText(50, 50, "Invoice: "+invoice.getID(), 10, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 60, "Total: " + txtTotal.Text, 10, "T3", Align.LeftAlign);
                
                
                textAndtable.AddText(50, 100, "Bill To: "+client[0], 10, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 110, client[4], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(50, 120, client[6] + ", " + client[7] + ", " + client[8] + " " + client[2], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(50, 130, "Primary Contact: "+client[5], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(50, 140, "Ph: "+client[3], 10, "T1", Align.LeftAlign);


                textAndtable.AddText(300, 100, "Forward Payment To: ", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(300, 110, franchisee[5], 10, "T3", Align.LeftAlign);
                textAndtable.AddText(300, 120, franchisee[0], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 130, franchisee[2] + ", " + franchisee[3] + ", " + franchisee[4] + " " + franchisee[1], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 140, "Contact: " + user[0]+ " "+ user[1], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 150, "Ph: " + user[2], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 160, "Email: " + user[3], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 170, "Web: http://www.fire-alert.ca", 10, "T1", Align.LeftAlign);

                textAndtable.AddText(50, 150, "WO, ID: "+txtSalesOrder.SelectedValue.ToString(), 10, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 160, "Service Address: ", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 170, sAd[0], 10, "T1", Align.LeftAlign);
               // textAndtable.AddText(50, 180, sAd[1], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(50, 180, sAd[3]+ ", "+sAd[4]+", "+sAd[5]+" "+sAd[1], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(50, 190, "On-site Contact: ", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 200, sAd[2], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(50, 210, "Date of Service: ", 10, "T1", Align.LeftAlign);


                //Add table to the page
                Align[] alignC = new Align[4];
                alignC[0] = Align.CenterAlign;
                alignC[1] = Align.CenterAlign;
                alignC[2] = Align.CenterAlign;
                alignC[3] = Align.CenterAlign;

                Align[] alignR = new Align[4];
                alignR[0] = Align.LeftAlign;
                alignR[1] = Align.LeftAlign;
                alignR[2] = Align.LeftAlign;
                alignR[3] = Align.LeftAlign;

                //Specify the color for the cell and the line
                ColorSpec cellColor = new ColorSpec(196, 34, 34);
                ColorSpec lineColor = new ColorSpec(1, 1, 1);

                //Fill in the parameters for the table
                TableParams table = new TableParams(4, 235, 90, 75, 75);
                table.yPos = 550;
                table.xPos = 50;
                table.rowHeight = 15;

                //Set the parameters of this table
                textAndtable.SetParams(table, cellColor, Align.CenterAlign, 3);
                textAndtable.AddRow(false, 8, "T3", alignC, true, "Terms", "Purchase Order", "Date Issued", "Due Date");
                //After drawing table and text add them to the page 
                content.SetStream(textAndtable.EndTable(lineColor,true));

                TextAndTables textAndtable2 = new TextAndTables(pSize);
                //Specify the color for the cell and the line
                ColorSpec cell1 = new ColorSpec(255, 255, 255);
                ColorSpec line1 = new ColorSpec(1, 1, 1);
                TableParams table2 = new TableParams(4, 235, 90, 75, 75);
                table2.yPos = 535;
                table2.xPos = 50;
                table2.rowHeight = 15;
                textAndtable2.SetParams(table2, cell1, Align.CenterAlign, 3);
                textAndtable2.AddRow(false, 8, "T3", alignR, false, "Net"+txtTerm.Text, txtSalesOrder.SelectedValue.ToString(),
                    Issued.Value.ToShortDateString(), Issued.Value.AddDays(Convert.ToDouble(txtTerm.Text)).ToShortDateString());
                content.SetStream(textAndtable2.EndTable(line1, true));

                Align[] alignC2 = new Align[7];
                alignC2[0] = Align.CenterAlign;
                alignC2[1] = Align.CenterAlign;
                alignC2[2] = Align.CenterAlign;
                alignC2[3] = Align.CenterAlign;
                alignC2[4] = Align.CenterAlign;
                alignC2[5] = Align.CenterAlign;
                alignC2[6] = Align.CenterAlign;


                Align[] alignR2 = new Align[7];
                alignR2[0] = Align.LeftAlign;
                alignR2[1] = Align.LeftAlign;
                alignR2[2] = Align.LeftAlign;
                alignR2[3] = Align.LeftAlign;
                alignR2[4] = Align.RightAlign;
                alignR2[5] = Align.RightAlign;
                alignR2[6] = Align.RightAlign;

                TextAndTables textAndtable3 = new TextAndTables(pSize);
                //Specify the color for the cell and the line
                ColorSpec cell2 = new ColorSpec(196, 34, 34);
                ColorSpec line2 = new ColorSpec(1, 1, 1);
                TableParams table3 = new TableParams(7, 15, 100, 160, 40,40,60,60);
                table3.yPos = 510;
                table3.xPos = 50;
                table3.rowHeight = 15;
                textAndtable3.SetParams(table3, cell2, Align.CenterAlign, 3);
                textAndtable3.AddRow(false, 8, "T3", alignC2, true, "#", "Item", "Description", "Hours", "Qty", "Price", "Line Total");
                content.SetStream(textAndtable3.EndTable(line2, true));


                TextAndTables textAndtable4 = new TextAndTables(pSize);
                //Specify the color for the cell and the line
                ColorSpec cell3 = new ColorSpec(255, 255, 255);
                ColorSpec line3 = new ColorSpec(1, 1, 1);
                TableParams table4 = new TableParams(7, 15, 100, 160, 40, 40, 60, 60);
                table4.yPos = 495;
                table4.xPos = 50;
                table4.rowHeight = 15;
                textAndtable4.SetParams(table4, cell3, Align.CenterAlign, 3);
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    textAndtable4.AddRow(false, 8, "T3", alignR2, false, dataGridView1.Rows[i].Cells[0].Value.ToString(),
                        dataGridView1.Rows[i].Cells[1].Value.ToString(), dataGridView1.Rows[i].Cells[2].Value.ToString(),
                        dataGridView1.Rows[i].Cells[3].Value.ToString(), dataGridView1.Rows[i].Cells[4].Value.ToString(),
                        dataGridView1.Rows[i].Cells[5].Value.ToString(), dataGridView1.Rows[i].Cells[6].Value.ToString());
                }
                content.SetStream(textAndtable4.EndTable(line3, true));



                textAndtable.AddText(400, 650, "Subtotal ", 10, "T1", Align.LeftAlign);
                textAndtable.AddText(400, 665, "HST ", 10, "T1", Align.LeftAlign);
                textAndtable.AddText(400, 680, "Total ", 10, "T1", Align.LeftAlign);
                textAndtable.AddText(400, 695, "Payment Made ", 10, "T1", Align.LeftAlign);
                textAndtable.AddText(400, 710, "Balance Due ", 10, "T1", Align.LeftAlign);


                TextAndTables textAndtable5 = new TextAndTables(pSize);
                Align[] align = new Align[1];
                align[0] = Align.RightAlign;
                //Specify the color for the cell and the line
                TableParams table5 = new TableParams(1,60);
                table5.yPos = 152;
                table5.xPos = 100;
                table5.rowHeight = 15;
                textAndtable5.SetParams(table5, cell3, Align.RightAlign, 3);
                textAndtable5.AddRow(false, 10, "T3", align, false, txtSub.Text);
                textAndtable5.AddRow(false, 10, "T3", align, false, txtTax.Text);
                content.SetStream(textAndtable5.EndTable(line3, true));

                TextAndTables textAndtable6 = new TextAndTables(pSize);
                //Specify the color for the cell and the line
                TableParams table6 = new TableParams(1, 60);
                table6.yPos = 122;
                table6.xPos = 100;
                table6.rowHeight = 15;
                textAndtable6.SetParams(table6, cell2, Align.RightAlign, 3);
                textAndtable6.AddRow(false, 10, "T3", align, true, txtTotal.Text);
                
                if (txtAmount.Text =="")
                    txtAmount.Text="$0.00";
                textAndtable6.AddRow(false, 10, "T3", align, true, "$" + String.Format("{0:0.00}", Math.Round(Convert.ToDouble(txtAmount.Text.Replace("$", "")))));
                textAndtable6.AddRow(false, 10, "T3", align, true, "$" + String.Format("{0:0.00}", Math.Round(Convert.ToDouble(txtTotal.Text.Replace("$", "")) -
                    Convert.ToDouble(txtAmount.Text.Replace("$", "")), 2)));
                content.SetStream(textAndtable6.EndTable(line2, true));


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

                //Messages.Visible = true;
                Preview testDialog = new Preview(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\Invoice.pdf");
                testDialog.ShowDialog(this);
            }
            catch (Exception ex)
            {
               MessageBox.Show("Could not display the document with the incorrect Information");
            }

        }

        public void SetTable(DataTable dt)
        {

            dataGridView1.DataSource = dt;
        }

        public void SetTotal(double total,double tax)
        {
            txtSub.Text = "$" + String.Format("{0:0.00}", Math.Round(total, 2));
            txtTax.Text = "$"+String.Format("{0:0.00}", Math.Round(total * tax,2));
            txtTotal.Text = "$"+String.Format("{0:0.00}", Math.Round(total * (1 + tax),2));
            
        }

     
     
    }
}
