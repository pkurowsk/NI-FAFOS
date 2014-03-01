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
    public class SalesOrderController
    {
        // private static FAFOS _view;
        private static String franchiseeUserId;
        private static String franchiseeId;
        private int type;
        private double tax;
        private int[] rowsDeleted = new int[10];
        private int rowsDeletedCounter = 0;

        public SalesOrderController(String franchiseeUserID)
        {
            franchiseeUserId = franchiseeUserID;
            franchiseeId = new Users().getFranchiseeId(franchiseeUserID);
        }

        public void pullQuoteData(object sender, EventArgs e)
        {
            Sales_Order newSalesOrder = (Sales_Order)((Button)sender).FindForm();
            newSalesOrder.clearData();
            newSalesOrder.fillData(new Quote().get(newSalesOrder.getId()), false, new QuoteItems().get(newSalesOrder.getId()));
        }

        public void salesOrder(int type)//object sender, EventArgs e)
        {
            // _view = (Sales_Order)((Button)sender).FindForm();
            // _view.Hide();
            this.type = type;
            Sales_Order newSalesOrder = new Sales_Order(this, franchiseeUserId, type);
            newSalesOrder.fillItemList(new Item().get(franchiseeId).Tables[0]);
            newSalesOrder.fillClientList(new Client().get().Tables[0]);
            newSalesOrder.fillServiceAddressList(new ServiceAddress().get().Tables[0]);
            if (type == 2)
                newSalesOrder.fillSalesOrdersIDs(new Quote().getInProgress(franchiseeUserId));
            else if (type == 3)
                newSalesOrder.fillSalesOrdersIDs(new SalesOrder().getInProgress(franchiseeUserId));
            newSalesOrder.Show();
        }

        public void createSalesOrder(object sender, EventArgs e)
        {
            Sales_Order newSalesOrder = (Sales_Order)((Button)sender).FindForm();
            //save
            OrderItems items = new OrderItems();
            string id = new SalesOrder().set(franchiseeUserId, newSalesOrder.getServiceAddressId(),
                type == 2 ? newSalesOrder.getId() : "NULL", newSalesOrder.getTotal(), tax.ToString(), newSalesOrder.getCompleted() == true ? "1" : "NULL");
            DataGridView dt = newSalesOrder.getOrderItems();
            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                items.set(dt.Rows[i].Cells[0].Value.ToString(), dt.Rows[i].Cells[4].Value != null && dt.Rows[i].Cells[4].Value.ToString() != "" ? dt.Rows[i].Cells[4].Value.ToString() : "NULL",
                    dt.Rows[i].Cells[3].Value != null && dt.Rows[i].Cells[3].Value.ToString() != "" ? dt.Rows[i].Cells[3].Value.ToString() : "NULL",
                    dt.Rows[i].Cells[5].Value.ToString(), dt.Rows[i].Cells[1].Value.ToString(), id);
            }
            newSalesOrder.Dispose();
            MessageBox.Show("The Sales Order ID is " + id + "!");
            // _view.Show();
        }

        public void saveSalesOrder(object sender, EventArgs e)
        {
            Sales_Order newSalesOrder = (Sales_Order)((Button)sender).FindForm();
            //save
            OrderItems items = new OrderItems();
            new SalesOrder().update(newSalesOrder.getId(), franchiseeUserId, newSalesOrder.getServiceAddressId(), newSalesOrder.getTotal(), tax.ToString(), newSalesOrder.getCompleted() == true ? "1" : "NULL");
            for (int i = 0; i < rowsDeletedCounter; i++)
            {
                items.delete(rowsDeleted[i].ToString(), newSalesOrder.getId());
            }
            DataGridView dt = newSalesOrder.getOrderItems();
            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                items.setUpdate(dt.Rows[i].Cells[0].Value.ToString(), dt.Rows[i].Cells[4].Value != null && dt.Rows[i].Cells[4].Value.ToString() != "" ? dt.Rows[i].Cells[4].Value.ToString() : "NULL",
                    dt.Rows[i].Cells[3].Value != null && dt.Rows[i].Cells[3].Value.ToString() != "" ? dt.Rows[i].Cells[3].Value.ToString() : "NULL",
                    dt.Rows[i].Cells[5].Value.ToString(), dt.Rows[i].Cells[1].Value.ToString(), newSalesOrder.getId());
            }
            newSalesOrder.Dispose();
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
            Sales_Order newSalesOrder = (Sales_Order)((Button)sender).FindForm();
            //save
            newSalesOrder.Dispose();
            //   _view.Show();
        }

        public void dgvSalesOrder_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            Sales_Order newSalesOrder = (Sales_Order)((DataGridView)sender).FindForm();
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

        public void pullSalesOrder(object sender, EventArgs e)
        {
            Sales_Order salesOrder = (Sales_Order)((Button)sender).FindForm();
            salesOrder.clearData();
            salesOrder.fillData(new SalesOrder().getSAddress(salesOrder.getId()), new SalesOrder().getCompleted(salesOrder.getId()), new OrderItems().get(salesOrder.getId()));
        }

        // update the view
        public void preview(object sender, EventArgs e)
        {
            Sales_Order createSalesOrder = (Sales_Order)((Button)sender).FindForm();
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
                infoDict.SetInfo("Sales Order xxx", "System Generated", "My Company Name");

                //Create a utility object
                Utility pdfUtility = new Utility();

                //Open a file specifying the file name as the output pdf file
                String FilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\SalesOrder.pdf";

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
                sAd = new ServiceAddress().get(createSalesOrder.getServiceAddressId()).Split(',');

                String[] client = new String[9];
                client = new Client().get(new ClientContract().getClient(createSalesOrder.getServiceAddressId())).Split(',');

                //Add text to the page
                textAndtable.AddText(50, 100, "Work Order ID: " + createSalesOrder.getId(), 10, "T3", Align.LeftAlign);
                //textAndtable.AddText(50, 60, "Total: " + txtTotal.Text, 10, "T3", Align.LeftAlign);

                textAndtable.AddText(50, 150, "General", 10, "T3", Align.LeftAlign);
                //textAndtable.AddText(50, 160, "Label: ", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 170, "Client: " + client[0], 10, "T3", Align.LeftAlign);
                textAndtable.AddText(50, 180, "Phone: " + client[3], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(50, 190, "Booked By: ", 10, "T1", Align.LeftAlign);

                textAndtable.AddText(300, 150, "Place and Time", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(300, 160, "Service Address: ", 10, "T1", Align.LeftAlign);
                textAndtable.AddText(370, 160, sAd[0], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(370, 170, sAd[3] + ", " + sAd[4] + ", " + sAd[5] + " " + sAd[1], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 180, "On-site Contact: " + sAd[2], 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 190, "Start: ", 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 200, "End: ", 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 210, "Duration: ", 10, "T1", Align.LeftAlign);

                textAndtable.AddText(300, 230, "Special Instructions", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(300, 240, "Call Ahead Required", 10, "T1", Align.LeftAlign);
                textAndtable.AddText(300, 250, "Crews Assigned", 10, "T3", Align.LeftAlign);
                textAndtable.AddText(300, 260, "None Assigned", 10, "T1", Align.LeftAlign);

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
                //After drawing table and text add them to the page 
                content.SetStream(textAndtable.EndTable(lineColor, true));

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
                ColorSpec cell2 = new ColorSpec(128, 128, 128);
                ColorSpec line2 = new ColorSpec(1, 1, 1);
                TableParams table3 = new TableParams(7, 15, 100, 160, 40, 40, 60, 60);
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
                DataGridView dgvSalesOrder = createSalesOrder.getOrderItems();
                for (int i = 0; i < dgvSalesOrder.Rows.Count - 1; i++)
                {
                    textAndtable4.AddRow(false, 8, "T3", alignR2, false, dgvSalesOrder.Rows[i].Cells[0].Value.ToString(),
                        dgvSalesOrder.Rows[i].Cells[1].FormattedValue.ToString(), dgvSalesOrder.Rows[i].Cells[2].Value.ToString(),
                        (dgvSalesOrder.Rows[i].Cells[3].Value == null || dgvSalesOrder.Rows[i].Cells[3].Value.ToString() == "") ? "-" : dgvSalesOrder.Rows[i].Cells[3].Value.ToString(),
                        (dgvSalesOrder.Rows[i].Cells[4].Value == null || dgvSalesOrder.Rows[i].Cells[4].Value.ToString() == "") ? "-" : dgvSalesOrder.Rows[i].Cells[4].Value.ToString(),
                        dgvSalesOrder.Rows[i].Cells[5].Value.ToString(), dgvSalesOrder.Rows[i].Cells[6].Value.ToString());
                }
                content.SetStream(textAndtable4.EndTable(line3, true));



                textAndtable.AddText(400, 650, "Subtotal: ", 10, "T1", Align.LeftAlign);
                textAndtable.AddText(400, 665, "HST(" + (tax * 100) + "%): ", 10, "T1", Align.LeftAlign);
                textAndtable.AddText(400, 680, "Total: ", 10, "T1", Align.LeftAlign);

                TextAndTables textAndtable5 = new TextAndTables(pSize);
                Align[] align = new Align[1];
                align[0] = Align.RightAlign;
                //Specify the color for the cell and the line
                TableParams table5 = new TableParams(1, 60);
                table5.yPos = 152;
                table5.xPos = 100;
                table5.rowHeight = 15;
                textAndtable5.SetParams(table5, cell3, Align.RightAlign, 3);
                textAndtable5.AddRow(false, 10, "T3", align, false, createSalesOrder.getSubtotal());
                textAndtable5.AddRow(false, 10, "T3", align, false, createSalesOrder.getHST());
                content.SetStream(textAndtable5.EndTable(line3, true));

                TextAndTables textAndtable6 = new TextAndTables(pSize);
                //Specify the color for the cell and the line
                TableParams table6 = new TableParams(1, 60);
                table6.yPos = 122;
                table6.xPos = 100;
                table6.rowHeight = 15;
                textAndtable6.SetParams(table6, cell2, Align.RightAlign, 3);
                textAndtable6.AddRow(false, 10, "T3", align, true, "$" + createSalesOrder.getTotal());
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
                Preview testDialog = new Preview(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory) + "\\Resources\\SalesOrder.pdf");
                testDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not display the document because " + ex.ToString());
            }

        }
    }
}
