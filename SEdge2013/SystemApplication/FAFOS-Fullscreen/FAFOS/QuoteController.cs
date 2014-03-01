using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.IO;
using InvoicePDF;
using FAFOS.Forms;
namespace FAFOS
{
    public class QuoteController
    {
       // private static FAFOS _view;
        private static String franchiseeUserId;
        private static String franchiseeId;
        private int type;
        private double tax;
        private int[] rowsDeleted = new int[10];
        private int rowsDeletedCounter = 0;

        public QuoteController(String franchiseeUserID)
        {
            franchiseeUserId = franchiseeUserID;
            franchiseeId = new Users().getFranchiseeId(franchiseeUserID);
        }

        public void pullQuoteData(object sender, EventArgs e)
        {
            QuoteForm quote = (QuoteForm)((Button)sender).FindForm();
            quote.clearData();
            quote.fillData(new Quote().get(quote.getId()), new QuoteItems().get(quote.getId()));
        }

        public void quote(int type)
        {
            this.type = type;
            QuoteForm newQuote = new QuoteForm(this, franchiseeUserId, type);
            newQuote.fillItemList(new Item().get(franchiseeId).Tables[0]);
            //newQuote.fillClientList(new Client().get().Tables[0]);
            newQuote.fillServiceAddressList(new ServiceAddress().get().Tables[0]);
            if (type == 2)
                newQuote.fillQuoteIDs(new Quote().getInProgress(franchiseeUserId));
            newQuote.Show();
        }

        public void createQuote(object sender, EventArgs e)
        {
            QuoteForm newQuote = (QuoteForm)((Button)sender).FindForm();
            //save
            QuoteItems items = new QuoteItems();
            string id = new Quote().set(franchiseeUserId, newQuote.getServiceAddressId(), tax.ToString(), newQuote.getTotal());
            DataGridView dt = newQuote.getQuoteItems();
            for (int i = 0; i < dt.Rows.Count-1; i++)
            {
               items.set(dt.Rows[i].Cells[0].Value.ToString(), dt.Rows[i].Cells[4].Value != null ? dt.Rows[i].Cells[4].Value.ToString() : "NULL",
                    dt.Rows[i].Cells[3].Value != null ? dt.Rows[i].Cells[3].Value.ToString() : "NULL",
                    dt.Rows[i].Cells[5].Value.ToString(), dt.Rows[i].Cells[1].Value.ToString(), id);
            }
            newQuote.Dispose();
            MessageBox.Show("The Quote ID is " + id + "!");
           // _view.Show();
        }

        public void saveQuote(object sender, EventArgs e)
        {
            QuoteForm quote = (QuoteForm)((Button)sender).FindForm();
            //save
            QuoteItems items = new QuoteItems();
            new Quote().update(quote.getId(), franchiseeUserId, quote.getServiceAddressId(), tax.ToString(), quote.getTotal());
            for (int i = 0; i < rowsDeletedCounter; i++)
            {
                items.delete(rowsDeleted[i].ToString(), quote.getId());
            }
            DataGridView dt = quote.getQuoteItems();
            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                items.setUpdate(dt.Rows[i].Cells[0].Value.ToString(), dt.Rows[i].Cells[4].Value != null && dt.Rows[i].Cells[4].Value.ToString() != "" ? dt.Rows[i].Cells[4].Value.ToString() : "NULL",
                    dt.Rows[i].Cells[3].Value != null && dt.Rows[i].Cells[3].Value.ToString() != "" ? dt.Rows[i].Cells[3].Value.ToString() : "NULL",
                    dt.Rows[i].Cells[5].Value.ToString(), dt.Rows[i].Cells[1].Value.ToString(), quote.getId());
            }
            quote.Dispose();
            // _view.Show();
        }

        public void DataGridView1_UserDeletedRow(Object sender, DataGridViewRowEventArgs e)
        {
            //MessageBox.Show(e.Row.Cells[0].Value.ToString());
            rowsDeleted[rowsDeletedCounter] = Convert.ToInt32(e.Row.Cells[0].Value);
            rowsDeletedCounter++;
        }

        public void cancelSalesOrder(object sender, EventArgs e)
        {
            QuoteForm newSalesOrder = (QuoteForm)((Button)sender).FindForm();
            //save
            newSalesOrder.Dispose();
         //   _view.Show();
        }

        public void dgvSalesOrder_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            QuoteForm newSalesOrder = (QuoteForm)((DataGridView)sender).FindForm();
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Rows[e.RowIndex].Cells[1].Value != null && e.ColumnIndex == 1 && e.RowIndex != -1)
            {
                dgv.Rows[e.RowIndex].Cells[2].Value = new Item().getDescription(dgv.Rows[e.RowIndex].Cells[1].Value.ToString());
                dgv.Rows[e.RowIndex].Cells[5].Value = new Item().getPrice(dgv.Rows[e.RowIndex].Cells[1].Value.ToString());
            }
            if ((dgv.Rows[e.RowIndex].Cells[4].Value != null && dgv.Rows[e.RowIndex].Cells[5].Value != null && (dgv.Rows[e.RowIndex].Cells[3].Value == null || dgv.Rows[e.RowIndex].Cells[3].Value.ToString() == ""))
                && (dgv.Rows[e.RowIndex].Cells[4].Value.ToString() != "" && dgv.Rows[e.RowIndex].Cells[5].Value.ToString() != ""))
            {
                dgv.Rows[e.RowIndex].Cells[6].Value = Convert.ToDouble(dgv.Rows[e.RowIndex].Cells[4].Value.ToString()) * Convert.ToDouble(dgv.Rows[e.RowIndex].Cells[5].Value.ToString());
            }
            else if ((dgv.Rows[e.RowIndex].Cells[3].Value != null && dgv.Rows[e.RowIndex].Cells[5].Value != null && (dgv.Rows[e.RowIndex].Cells[4].Value == null || dgv.Rows[e.RowIndex].Cells[4].Value.ToString() == ""))
                && (dgv.Rows[e.RowIndex].Cells[3].Value.ToString() != "" && dgv.Rows[e.RowIndex].Cells[5].Value.ToString() != ""))
            {
                dgv.Rows[e.RowIndex].Cells[6].Value = Convert.ToDouble(dgv.Rows[e.RowIndex].Cells[3].Value.ToString()) * Convert.ToDouble(dgv.Rows[e.RowIndex].Cells[5].Value.ToString());
            }
            if (dgv.Rows[e.RowIndex].Cells[6].Value != null)
            {
                double total = 0;
                for (int i = 0; i < dgv.Rows.Count - 1; i++)
                {
                    total += Convert.ToDouble(dgv.Rows[i].Cells[6].Value);
                }
                /******************************/
                if (type == 1 || type == 2)
                    tax = Convert.ToDouble(new Address().getProvinceTax(new ServiceAddress().getProvinceID(newSalesOrder.getServiceAddressId())));
                else
                    tax = Convert.ToDouble(new SalesOrder().getProvinceTax(newSalesOrder.getId()));
                /******************************/
                newSalesOrder.setTotal(total, tax);
            }
        }

        // update the view
        public void preview(object sender, EventArgs e)
        {
            QuoteForm quoteForm = (QuoteForm)((Button)sender).FindForm();
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
                //Create the font called Times Roman
                TimesRoman.CreateFontDict("T1", "Times-Roman");

                //Create the font called Times Italic
                TimesItalic.CreateFontDict("T2", "Times-Italic");

                //Create the font called Times Bold
                TimesBold.CreateFontDict("T3", "Times-Bold");

                //Create the font called Courier
                Courier.CreateFontDict("T4", "Courier");
                
                //Set the info Dictionary. xxx will be the invoice number
                infoDict.SetInfo("Quote xxx" /*+ quoteForm.getID()*/, "System Generated", "Fire-Alert");

                //Create a utility object
                Utility pdfUtility = new Utility();
                String FilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\Quote.pdf";

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
                content.SetStream(pi.ShowImage("I1", 400, 680, 155, 85));   //tell the PDF we want to draw an image called 'I1', where and what size

                String[] sAd = new String[6];
                sAd = new ServiceAddress().get(quoteForm.getServiceAddressId()).Split(',');

                String[] client = new String[9];
                client = new Client().get(new ClientContract().getClient(quoteForm.getServiceAddressId())).Split(',');

                String[] franchisee = new String[6];
                franchisee = new Franchisee().get(new ClientContract().getFranchisee(quoteForm.getServiceAddressId())).Split(',');

                String[] user = new String[4];
                user = new Users().get(franchiseeUserId).Split(',');

                //Add text to the page
                textAndtable.AddText(50, 50, "Quote: " + ((type == 2) ? quoteForm.getId() : "To Be Created"), 10, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 60, "Date Issued: "/* + quoteForm.getDateIssued()*/, 10, "T3", Align.LeftAlign);


                textAndtable.AddText(50, 100, "To: " + client[0], 10, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 110, client[4], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(50, 120, client[6] + ", " + client[7] + ", " + client[8] + " " + client[2], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(50, 130, "Primary Contact: " + client[5], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(50, 140, "Ph: " + client[3], 10, "T1", Align.LeftAlign);

                textAndtable.AddText(300, 100, "Prepared By: ", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(300, 110, franchisee[5], 10, "T3", Align.LeftAlign);
                textAndtable.AddText(300, 120, franchisee[0], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 130, franchisee[2] + ", " + franchisee[3] + ", " + franchisee[4] + " " + franchisee[1], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 140, "Contact: " + user[0] + " " + user[1], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 150, "Ph: " + user[2], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 160, "Email: " + user[3], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 170, "Web: http://www.fire-alert.ca", 10, "T1", Align.LeftAlign);

                textAndtable.AddText(50, 160, "Service Location: ", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 170, sAd[0], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(50, 180, sAd[3] + ", " + sAd[4] + ", " + sAd[5] + " " + sAd[1], 10, "T1", Align.LeftAlign);

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
                TableParams table = new TableParams(2, 235, 90, 75, 75);
                table.yPos = 550;
                table.xPos = 50;
                table.rowHeight = 15;

                //Set the parameters of this table
                textAndtable.SetParams(table, cellColor, Align.CenterAlign, 3);
                textAndtable.AddRow(false, 8, "T3", alignC, true, "Service Rep.", "Valid Until");
                //After drawing table and text add them to the page 
                content.SetStream(textAndtable.EndTable(lineColor, true));

                TextAndTables textAndtable2 = new TextAndTables(pSize);
                //Specify the color for the cell and the line
                ColorSpec cell1 = new ColorSpec(255, 255, 255);
                ColorSpec line1 = new ColorSpec(1, 1, 1);
                TableParams table2 = new TableParams(2, 235, 90, 75, 75);
                table2.yPos = 535;
                table2.xPos = 50;
                table2.rowHeight = 15;
                textAndtable2.SetParams(table2, cell1, Align.CenterAlign, 3);
                textAndtable2.AddRow(false, 8, "T3", alignR, false, "", "");
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
                TableParams table3 = new TableParams(7, 15, 100, 160, 40, 40, 60, 60);
                table3.yPos = 510;
                table3.xPos = 50;
                table3.rowHeight = 15;
                textAndtable3.SetParams(table3, cell2, Align.CenterAlign, 3);
                textAndtable3.AddRow(false, 8, "T3", alignC2, true, "#", "Charge", "Description", "Hours", "Qty", "Price", "Line Total");
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
                DataGridView dgvSalesOrder = quoteForm.getQuoteItems();
                for (int i = 0; i < dgvSalesOrder.Rows.Count - 1; i++)
                {
                    textAndtable4.AddRow(false, 8, "T3", alignR2, false, dgvSalesOrder.Rows[i].Cells[0].Value.ToString(),
                        dgvSalesOrder.Rows[i].Cells[1].FormattedValue.ToString(), dgvSalesOrder.Rows[i].Cells[2].Value.ToString(),
                        (dgvSalesOrder.Rows[i].Cells[3].Value == null || dgvSalesOrder.Rows[i].Cells[3].Value.ToString() == "") ? "-" : dgvSalesOrder.Rows[i].Cells[3].Value.ToString(),
                        (dgvSalesOrder.Rows[i].Cells[4].Value == null || dgvSalesOrder.Rows[i].Cells[4].Value.ToString() == "") ? "-" : dgvSalesOrder.Rows[i].Cells[4].Value.ToString(),
                        dgvSalesOrder.Rows[i].Cells[5].Value.ToString(), dgvSalesOrder.Rows[i].Cells[6].Value.ToString());
                }
                content.SetStream(textAndtable4.EndTable(line3, true));



                textAndtable.AddText(400, 650, "Subtotal ", 10, "T1", Align.LeftAlign);
                textAndtable.AddText(400, 665, "HST ", 10, "T1", Align.LeftAlign);
                textAndtable.AddText(400, 680, "Total ", 10, "T1", Align.LeftAlign);


                TextAndTables textAndtable5 = new TextAndTables(pSize);
                Align[] align = new Align[1];
                align[0] = Align.RightAlign;
                //Specify the color for the cell and the line
                TableParams table5 = new TableParams(1, 60);
                table5.yPos = 152;
                table5.xPos = 100;
                table5.rowHeight = 15;
                textAndtable5.SetParams(table5, cell3, Align.RightAlign, 3);
                textAndtable5.AddRow(false, 10, "T3", align, false, quoteForm.getSubtotal());
                textAndtable5.AddRow(false, 10, "T3", align, false, quoteForm.getHST());
                content.SetStream(textAndtable5.EndTable(line3, true));

                TextAndTables textAndtable6 = new TextAndTables(pSize);
                //Specify the color for the cell and the line
                TableParams table6 = new TableParams(1, 60);
                table6.yPos = 122;
                table6.xPos = 100;
                table6.rowHeight = 15;
                textAndtable6.SetParams(table6, cell2, Align.RightAlign, 3);
                textAndtable6.AddRow(false, 10, "T3", align, true, "$" + quoteForm.getTotal());


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
                Preview testDialog = new Preview(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\Quote.pdf");
                testDialog.ShowDialog(quoteForm);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not display the document because " + ex.ToString());
            }

        }
    }
}
