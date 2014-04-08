using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace FAFOS
{
    public partial class View : Form
    {
        private double screenWidth;
        private double screenHeight;
        MapsForm maps;
        Users user;
        int userid;
        private int screenCenterTop;
        private int screenCenterLeft;
        private int LoginRetryCounter = 0;
         WorkOrder[] orders;
         ContractService[] services;
        private DateTime t1;
        List<Bitmap> piclist = new List<Bitmap>();

        //delegate void SetSizeCallback(int w, int h, tile T);
        private bool _Authenticated = false;

        public bool Authenticated
        {
            get { return _Authenticated; }
            set { _Authenticated = value; }
        }
        private string _Username = "";

        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }

        public View()
        {
            InitializeComponent();

            services = new ContractService[30];
            orders = new WorkOrder[30];


            pnlLogin.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width / 2 - Convert.ToInt32(pnlLogin.Size.Width) / 2,
                System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height / 2 - Convert.ToInt32(pnlLogin.Size.Height) / 2);

            pnlMenu.Size = new System.Drawing.Size(pnlMenu.Size.Width, 
                System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height);
            lblTitleFAFOS.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - lblTitleFAFOS.Size.Width,
                lblTitleFAFOS.Location.Y);

            user = new Users();

            screenWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;
            screenHeight = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; 
            this.Size = new System.Drawing.Size((int)screenWidth, (int)screenHeight);

            FireAlertLogo.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - 350,40);
            SEdgeLogo.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - 250,
                System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - 100);

            pnlPage.Size = new Size(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - pnlPage.Location.X, 
                System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - pnlPage.Location.Y);
            
        }

        public void Notifications()
        {
            DataTable dt2 = new ClientContract().getServices(userid.ToString());
            serviceNotification.Text = "";
         
            for (int i = 0; i < dt2.Rows.Count; i++)
            {

                if (Convert.ToDateTime(dt2.Rows[i][2]) == DateTime.Today)
                {
                    String service = dt2.Rows[i][0].ToString();
                    serviceNotification.Text += "\n" + service + " needs to be completed by today ";
                    serviceNotification.Text += "location - " + dt2.Rows[i][5].ToString() + "\n";
                }

            } 
            if (serviceNotification.Text == "")
             serviceNotification.Text = "";




           DataTable dt = new Payment().getNotPaid(userid);
           paymentNotification.Text = "";
           for (int i = 2; i < dt.Rows.Count; i++)
           {
               if (Convert.ToDateTime(dt.Rows[i][2]) == DateTime.Today)
               {
                   String name = new Client().getName(new ClientContract().getClient(new SalesOrder().getSAddress(new Invoice().getSalesOrderID(dt.Rows[i][0].ToString()).ToString())));
                   paymentNotification.Text += "\n"+name+" has an outstanding balance of ";

                   DataTable payments = new Payment().getAmount(dt.Rows[i][0].ToString());
                   double total=0;
                   for (int j = 0; j < payments.Rows.Count; j++)
                   {
                       total += Convert.ToDouble(payments.Rows[j][2]);
                       
                   }
                   paymentNotification.Text +=  "$" +String.Format("{0:0.00}",Math.Round(Convert.ToDouble(dt.Rows[i][3].ToString()) - total,2))
                       + " on invoice #" + dt.Rows[i][0].ToString()+"\n";
               }
           }
           if (paymentNotification.Text == "")
               paymentNotification.Text = "None";
        }


        public void Tile_Enter(object sender, EventArgs e)
        {
            /*
            tile _tile = (tile)sender;
            _tile.Tag = "0";

            screenCenterLeft = (int)((screenWidth / 2) - (_tile.tileWidth / 2));
            screenCenterTop = (int)((screenHeight / 2) - (_tile.tileHieght / 2));

            _tile.XScale = ((int)(formWidth - _tile.tileWidth) / 15);
            _tile.YScale = ((int)(formHeight - _tile.tileHieght) / 15);




            pnlMenu.Controls[_tile.Name].BringToFront();

            _tile.timerStart();

            */
        }


        private void RaisetileTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //tile _tile = (tile)sender;
            //TileSizeControl(_tile.tileWidth + _tile.XScale, _tile.tileHieght + _tile.YScale, _tile);
        }

        /*public void TileSizeControl(int w, int h, tile T)
        {
            SetSizeCallback d = new SetSizeCallback(SetSize);
            this.Invoke(d, new object[] { w, h, T });
        }*/

        /*
        public void SetSize(int w, int h, tile T)
        {

            if (T.Tag.Equals("0")) // move to the center
            {
              //  T.Resize = new System.Drawing.Size(300, 300);
                T.playMovie();
                if (T.Left < screenCenterLeft)
                {
                    T.Left += (int)(screenWidth - T.tileWidth) / 20;

                }
                else
                    if (T.Left > screenCenterLeft)
                    {
                        T.Left -= (int)(screenWidth - T.tileWidth) / 20;
                    }
                if (T.Top < screenCenterTop)
                {
                    T.Top += (int)(screenHeight - T.tileHieght) / 20;

                }
                else
                    if (T.Top > screenCenterTop)
                    {
                        T.Top -= (int)(screenHeight - T.tileHieght) / 20;
                    }

                if ((T.Top <= screenCenterTop + T.tileHieght && T.Top >= screenCenterTop - T.tileHieght)
                    && (T.Left <= screenCenterLeft + T.tileWidth && T.Left >= screenCenterLeft - T.tileWidth))
                {
                    T.Tag = "1";
                    t1 = DateTime.Now;
                }
            }
            else if (T.Tag.Equals("1"))  // finish flip 
            {
                TimeSpan elapsed = DateTime.Now - t1;
                if (elapsed.Milliseconds >= 100)
                {
                    T.Tag = "2";
                    T.tileTimer_Interval = 1;
                }
            }
            else if (T.Tag.Equals("2")) // expand
            {
                T.Resize = new System.Drawing.Size(w, h);

                T.Left = (int)((screenWidth / 2) - (T.tileWidth / 2));
                T.Top = (int)((screenHeight / 2) - (T.tileHieght / 2));

                if (T.Top <= 0)
                {
                    T.Tag = "3";
                    t1 = DateTime.Now;
                }
            }
            else if (T.Tag.Equals("3")) // wait 1 second
            {
                
                TimeSpan elapsed = DateTime.Now - t1;
                if (elapsed.Seconds >= 1)
                {
                    T.Tag = "4";
                    T.tileTimer_Interval = 40;
                }
            }
            else if (T.Tag.Equals("4")) // load the desired form
            {

                T.timerStop();
                T.Tag = "0";

                this.Exit_btn.Focus();
                MaintainClientController c = new MaintainClientController();
                SalesOrderController my_controller = new SalesOrderController(userid.ToString());
                // replace the following line by calling a method in the controller with a parameter (T.Name + "_form")
                // that method should has if statment to decide which form to be opened.

                pnlPage.Controls.Clear();

                if (T.Name == "invoice")
                {
                    InvoiceForm invoice_form = new InvoiceForm(userid);
                    invoice_form.TopLevel = false;
                    //invoice_form.AutoScroll = true;
                    pnlPage.Controls.Add(invoice_form);
                    invoice_form.Show();
                }
                else if (T.Name == "quote")
                {
                    QuoteController qc = new QuoteController(userid.ToString());
                    qc.quote(1, pnlPage);
                }
                else if (T.Name == "editQuote")
                {
                    QuoteController qc = new QuoteController(userid.ToString());
                    qc.quote(2, pnlPage);
                }
                else if (T.Name == "salesOrder")
                {
                    my_controller.salesOrder(1, pnlPage);
                    //payment_form.Show();
                }
                else if (T.Name == "convertSalesOrder")
                {
                    my_controller.salesOrder(2, pnlPage);
                    //payment_form.Show();
                }
                else if (T.Name == "editSalesOrder")
                {
                    my_controller.salesOrder(3, pnlPage);
                    //payment_form.Show();
                }
                else if (T.Name == "payment")
                {
                    PaymentForm payment_form = new PaymentForm(userid);
                    payment_form.Show();
                }
                else if (T.Name == "inventory")
                {
                    InventoryForm form = new InventoryForm(userid);
                    form.Show();
                    /*MapsForm form = new MapsForm();
                    form.Show();
                }
                else if (T.Name == "purchaseRecord")
                {
                    PurchaseRecord form = new PurchaseRecord(userid);
                    form.Show();
                    // T.timerStop();

                }
                else if (T.Name == "itinerary")
                {
                    //pnlOperation.Controls.Clear();
                    MapsForm form = new MapsForm(userid, orders, services);
                    /*form.TopLevel = false;
                    form.AutoScroll = true;
                    pnlOperation.Controls.Add(form);/*
                    form.Show();
                    

                }
                else if (T.Name == "addClient")
                {
                    c.New_client_button_Click(T, userid);
                }
                else if (T.Name == "editClient")
                {
                    c.Edit_Client_Button_Click(T, userid);
                }
                else if (T.Name == "addContract")
                {
                    c.Add_contract_Button_Click(T, userid);
                }
                else if (T.Name == "editContract")
                {
                    c.Edit_contract_Button_Click(T, userid);
                }
                else if (T.Name == "statement")
                {
                    Statements form = new Statements(userid);
                    form.Show();
                }
                else if (T.Name == "royaltyFee")
                {
                    RoyaltyFeeCollection form = new RoyaltyFeeCollection(userid.ToString());
                    form.Show();
                }
                else if (T.Name == "inspection")
                {
                    InspectionForm form = new InspectionForm(userid.ToString());
                    form.Show();
                }
                else if (T.Name == "revenueReport")
                {
                    ReportsController r = new ReportsController(userid.ToString());
                    r.report(1, pnlPage);
                  //  Report form = new InspectionForm(userid.ToString());
                   // form.Show();
                }
                T.tileReset();
                T.showFrame(0);

            }
        }
        */

        public double formWidth
        {
            get { return screenWidth; }
        }

        public double formHeight
        {
            get { return screenHeight; }
        }

 
        private void View_Load(object sender, EventArgs e)
        {

            this.txtUsername.Focus();
         }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void Login_btn_Click(object sender, EventArgs e)
        {

            if (Login())
            {
                pnlLogin.Visible = false;
                this.Exit_btn.Focus();
                /*quote.Visible = true;
                editQuote.Visible = true;
                salesOrder.Visible = true;
                editSalesOrder.Visible = true;
                convertSalesOrder.Visible = true;
                invoice.Visible = true;
                inventory.Visible = true;
                purchaseRecord.Visible = true;
                payment.Visible = true;
                itinerary.Visible = true;
                inspection.Visible = true;
                
                addClient.Visible = true;
                addContract.Visible = true;
                editClient.Visible = true;
                editContract.Visible = true;
                
                statement.Visible = true;
                jobReport.Visible = true;
                revenueReport.Visible = true;
                royaltyFee.Visible = true;
                if (!user.checkAdmin(userid))
                    royaltyFee.Visible = false;
             //   allRevenue.Visible = true;
                */
                syncAndroid.Visible = true;
                btnSyncFromAndroid.Visible = true;

                lblOperations.Visible = true;
                lblDocs.Visible = true;
                lblClients.Visible = true;
                lblReports.Visible = true;

                lblUsername.Visible = false;
                txtUsername.Visible = false;
                lblPassword.Visible = false;
                txtPassword.Visible = false;
                Login_btn.Visible = false;
                Logout_btn.Visible = true;
                noteHideButton.Visible = true;
                userSettings.Visible = true;
                lblUserInfo.Visible = true;
                notificationPanel.Visible = true;
                profilePic.Visible = true;
                pnlUser.Visible = true;
                pnlPage.Visible = true;
                pnlMenu.Visible = true;
                btnMenu.Visible = true;

                
                try
                {
                    piclist = MUser.LoadImages();
                }
                catch (Exception)
                {
                    
                    piclist.Add(FAFOS.Properties.Resources.DefaultProPic);
                    MUser.SaveImages(piclist);
                }

                this.profilePic.BackgroundImage = piclist[MUser.GetPicID(userid.ToString())];// FAFOS.Properties.Resources.Shades;
                this.profilePic.BackgroundImageLayout = ImageLayout.Stretch;
              

                    lblUserInfo.Text = "Welcome\n " + user.getName(userid);

                /*
                quote.tileLocation = quote.Location;
                editQuote.tileLocation = editQuote.Location;
                salesOrder.tileLocation = salesOrder.Location;
                convertSalesOrder.tileLocation = convertSalesOrder.Location;
                editSalesOrder.tileLocation = editSalesOrder.Location;
                
                invoice.tileLocation = invoice.Location;
                inventory.tileLocation = inventory.Location;
                purchaseRecord.tileLocation = purchaseRecord.Location;
                payment.tileLocation = payment.Location;
                itinerary.tileLocation = itinerary.Location;
                inspection.tileLocation = inspection.Location;
                
                addClient.tileLocation = addClient.Location;
                editClient.tileLocation = editClient.Location;
                addContract.tileLocation = addContract.Location;
                editContract.tileLocation = editContract.Location;
                
                statement.tileLocation = statement.Location;
                jobReport.tileLocation = jobReport.Location;
                revenueReport.tileLocation = revenueReport.Location;
                royaltyFee.tileLocation = royaltyFee.Location;
             //   allRevenue.tileLocation = allRevenue.Location;
                */
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
                /*
                quote.setMovie(path + "\\Resources\\Quote1.swf");
                editQuote.setMovie(path + "\\Resources\\EditQuote.swf");
                salesOrder.setMovie(path + "\\Resources\\CreateSalesOrder1.swf");
                editSalesOrder.setMovie(path + "\\Resources\\EditSalesOrder1.swf");
                convertSalesOrder.setMovie(path + "\\Resources\\ConvertSalesOrder1.swf");
                invoice.setMovie(path + "\\Resources\\Invoice1.swf");
                inventory.setMovie(path + "\\Resources\\Inventory1.swf");
                purchaseRecord.setMovie(path + "\\Resources\\PurchaseRecord.swf");
                payment.setMovie(path + "\\Resources\\payment1.swf");
                itinerary.setMovie(path + "\\Resources\\itinerary1.swf");
                inspection.setMovie(path + "\\Resources\\Inspection.swf");
                
                addClient.setMovie(path + "\\Resources\\addClient1.swf");
                addContract.setMovie(path + "\\Resources\\addContract1.swf");
                editClient.setMovie(path + "\\Resources\\editClient1.swf");
                editContract.setMovie(path + "\\Resources\\editContract1.swf");
                
                statement.setMovie(path + "\\Resources\\Statements.swf");
                jobReport.setMovie(path + "\\Resources\\JobReports.swf");
                revenueReport.setMovie(path + "\\Resources\\RevenueReports.swf");
                royaltyFee.setMovie(path + "\\Resources\\RoyaltyFee.swf");
              //  allRevenue.setMovie(path + "\\Resources\\TotalRevenue.swf");
                */
                Notifications();
            }

        }

        public bool Login()
        {
            if (LoginRetryCounter < 2)
            {
                if (txtPassword.Text.Length > 0 && txtUsername.Text.Length > 0)
                {
                    if (UserAuthenticated(txtUsername.Text, txtPassword.Text))
                    {
                        Authenticated = true;
                        lblUserInfo.Text = "Welcome\n " + user.getName(userid);

                        return true;
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show("Username or Password not recognised");
                        txtPassword.Text = "";
                        LoginRetryCounter++; // increment retry counter
                        return false;
                    }
                }
                else // password or username is empty
                {
                    System.Windows.Forms.MessageBox.Show("You need to enter both a username and a password to continue");
                    LoginRetryCounter++;
                    return false;
                }
            }
            else // too many attempts
            {
                System.Windows.Forms.MessageBox.Show("You have failed to remember your details \n Contact Administration for further instructions");
                System.Windows.Forms.Application.Exit();
                return false;
            }
        }


        private bool UserAuthenticated(string p, string p_2)
        {
            if (user.check(p, p_2))
            {
                userid = user.getId(p);
                return true;
            }
            return false;
        }

        private void Logout_btn_Click(object sender, EventArgs e)
        {
            pnlPage.Controls.Clear();
            
            syncAndroid.Visible = false;
            btnSyncFromAndroid.Visible = false;
            //syncHQ.Visible = false;

            lblOperations.Visible = false;
            lblDocs.Visible = false;
            lblClients.Visible = false;
            lblReports.Visible = false;
            //lblSyncOpts.Visible = false;
            pnlUser.Visible = false;

            pnlMenu.Visible = false;
            btnMenu.Visible = false;

            pnlOps.Visible = false;
            pnlDocs.Visible = false;
            pnlClients.Visible = false;
            pnlReports.Visible = false;

            lblUsername.Visible = true;
            txtUsername.Visible = true;
            lblPassword.Visible = true;
            txtPassword.Visible = true;
            pnlLogin.Visible = true;

            Login_btn.Visible = true;
            Logout_btn.Visible = false;
            userSettings.Visible = false;
            lblUserInfo.Visible = false;
            profilePic.Visible = false;
            notificationPanel.Visible = false;
            lblUserInfo.Text = "";
            txtUsername.Text = "";
            txtPassword.Text="";
            userid = 0;

        }

        private void userSettings_Click(object sender, EventArgs e)
        {
            MaintainUsersForm form = new MaintainUsersForm(userid, MUser.GetPicID(userid.ToString()));
            form.Show();
        }

        private void Notifications_button_Click(object sender, EventArgs e)
        {
            notificationPanel.Visible = notificationPanel.Visible ? false : true;
            noteHideButton.Text = notificationPanel.Visible ? "Hide Notifications" : "Show Notifications";
        }

        /// <summary>
        /// Hides the menu panel if it is visible
        /// Shows the menu panel if it is invisible
        /// </summary>
        private void PanelHideShow()
        {
            if (pnlMenu.Visible)
            {
                pnlOps.Visible = false;
                pnlDocs.Visible = false;
                pnlClients.Visible = false;
                pnlReports.Visible = false;

                pnlMenu.Visible = false;
                pnlPage.Location = new Point(0, pnlPage.Location.Y);
                pnlPage.Size = new System.Drawing.Size(pnlPage.Location.X + System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width,
                    pnlPage.Size.Height);
                btnMenu.Text = "Show Menu";
            }
            else
            {
                pnlMenu.Visible = true;
                pnlPage.Location = new Point(pnlMenu.Location.X + pnlMenu.Size.Width,
                    pnlPage.Location.Y);
                pnlPage.Size = new System.Drawing.Size(pnlPage.Location.X + System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width,
                    pnlPage.Size.Height);
                btnMenu.Text = "Hide Menu";
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            PanelHideShow();
        }

        #region Documents Panel

        /// <summary>
        /// Mouse hover for documents label
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void label6_MouseHover(object sender, EventArgs e)
        {
            pnlDocs.Visible = true;
        }

        private void label6_MouseLeave(object sender, EventArgs e)
        {
            pnlDocs.Visible = false;
        }

        private void btnQuote_Click(object sender, EventArgs e)
        {
           pnlDocs.Visible = false;
           QuoteController qc = new QuoteController(userid.ToString());
           qc.quote(1, pnlPage);
        }

        private void btnEditQuote_Click(object sender, EventArgs e)
        {
            pnlDocs.Visible = false;
            QuoteController qc = new QuoteController(userid.ToString());
            qc.quote(2, pnlPage);
        }

        private void btnCreateSO_Click(object sender, EventArgs e)
        {
            SalesOrderController my_controller = new SalesOrderController(userid.ToString());
            my_controller.salesOrder(1, pnlPage);
        }

        private void btnConvertSO_Click(object sender, EventArgs e)
        {
            SalesOrderController my_controller = new SalesOrderController(userid.ToString());
            my_controller.salesOrder(2, pnlPage);
        }

        private void btnEditSO_Click(object sender, EventArgs e)
        {
            SalesOrderController my_controller = new SalesOrderController(userid.ToString());
            my_controller.salesOrder(3, pnlPage);
        }

        private void btnInvoice_Click(object sender, EventArgs e)
        {
            pnlDocs.Visible = false;
            InvoiceForm invoice_form = new InvoiceForm(userid);
            invoice_form.TopLevel = false;
            pnlPage.Controls.Add(invoice_form);
            invoice_form.Show();
        }

        #endregion


        #region Operations Panel

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            pnlOps.Visible = true;
            System.Diagnostics.Debug.Print("Enter");
        }

        private void label5_MouseLeave(object sender, EventArgs e)
        {
            pnlOps.Visible = false;

            System.Diagnostics.Debug.Print("Leave");
        }

        private void btnItinerary_Click(object sender, EventArgs e)
        {
            PanelHideShow();

            pnlOps.Visible = false;

            MapsForm form = new MapsForm(userid, orders, services);
            form.TopLevel = false;
            pnlPage.Controls.Add(form);
            form.Show();
        }

        private void btnInspection_Click(object sender, EventArgs e)
        {
            pnlOps.Visible = false;

            InspectionForm form = new InspectionForm(userid.ToString());
            form.TopLevel = false;
            pnlPage.Controls.Add(form);
            form.Show();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            pnlOps.Visible = false;

            InventoryForm form = new InventoryForm(userid);
            form.TopLevel = false;
            pnlPage.Controls.Add(form);
            form.Show();
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            PurchaseRecord form = new PurchaseRecord(userid);
            form.TopLevel = false;
            pnlPage.Controls.Add(form);
            form.Show();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            PaymentForm payment_form = new PaymentForm(userid);
            payment_form.TopLevel = false;
            pnlPage.Controls.Add(payment_form);
            payment_form.Show();
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            MaintainClientController c = new MaintainClientController();
            SupplierForm supplier_form = new SupplierForm(c);
            supplier_form.TopLevel = false;
            pnlPage.Controls.Add(supplier_form);
            supplier_form.Show();
        
        }

        #endregion

        #region Clients Panel

        private void label7_MouseEnter(object sender, EventArgs e)
        {
            pnlClients.Visible = true;
        }

        

        private void label7_MouseLeave(object sender, EventArgs e)
        {
            pnlClients.Visible = false;
        }

        #endregion

        #region Reports Panel

        private void label8_MouseEnter(object sender, EventArgs e)
        {
            pnlReports.Visible = true;
        }

        private void label8_MouseLeave(object sender, EventArgs e)
        {
            pnlReports.Visible = false; 
        }

        #endregion

        private void pnlPage_ControlAdded(object sender, ControlEventArgs e)
        {
            // Set all panels invisible
            pnlOps.Visible = 
                pnlDocs.Visible = 
                pnlReports.Visible = 
                pnlClients.Visible = false;

            // Remove the previous form that was loaded into the panel
            if (pnlPage.Controls.Count > 1)
            {
                pnlPage.Controls[0].Dispose();
            }
        }

        private void pbFALogoLogin_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.fire-alert.ca");
        }

        private void btnAClient_Click(object sender, EventArgs e)
        {
            MaintainClientController c = new MaintainClientController();

            c.New_client_button_Click(this, userid, pnlPage);
        }

        private void btnEClient_Click(object sender, EventArgs e)
        {
            MaintainClientController c = new MaintainClientController();

            c.Edit_Client_Button_Click(this, userid, pnlPage);
        }

        private void btnAContract_Click(object sender, EventArgs e)
        {
            MaintainClientController c = new MaintainClientController();

            c.Add_contract_Button_Click(this, userid, pnlPage);
        }

        private void btnEContract_Click(object sender, EventArgs e)
        {
            MaintainClientController c = new MaintainClientController();

            c.Edit_contract_Button_Click(this, userid, pnlPage);
        }

        private void btnStatement_Click(object sender, EventArgs e)
        {
            Statements form = new Statements(userid);
            form.TopLevel = false;
            pnlPage.Controls.Add(form);
            form.Show();
        }

        private void btnJob_Click(object sender, EventArgs e)
        {
            // ???
        }

        private void btnRevenue_Click(object sender, EventArgs e)
        {
            ReportsController r = new ReportsController(userid.ToString());
            r.report(1, pnlPage) ;
        }

        private void btnRoyaltee_Click(object sender, EventArgs e)
        {
            RoyaltyFeeCollection form = new RoyaltyFeeCollection(userid.ToString());
            form.TopLevel = false;
            pnlPage.Controls.Add(form);
            form.Show();
        }

        #region Menu panels opening

        private void pnlOps_VisibleChanged(object sender, EventArgs e)
        {
            if (((Panel)(sender)).Visible)
                pnlDocs.Visible = pnlReports.Visible = pnlClients.Visible = false;
        }

        private void pnlClients_VisibleChanged(object sender, EventArgs e)
        {
            if (((Panel)(sender)).Visible)
                pnlDocs.Visible = pnlReports.Visible = pnlOps.Visible = false;

        }

        private void pnlDocs_VisibleChanged(object sender, EventArgs e)
        {
            if (((Panel)(sender)).Visible)
                pnlOps.Visible = pnlReports.Visible = pnlClients.Visible = false;

        }

        private void pnlReports_VisibleChanged(object sender, EventArgs e)
        {
            if (((Panel)(sender)).Visible)
                pnlDocs.Visible = pnlOps.Visible = pnlClients.Visible = false;

        }

        #endregion

        private void pnlPage_MouseEnter(object sender, EventArgs e)
        {
            // Set all panels invisible
            pnlOps.Visible =
                pnlDocs.Visible =
                pnlReports.Visible =
                pnlClients.Visible = false;
        }

        private void syncAndroid_Click(object sender, EventArgs e)
        {
            syncController my_sync_controller = new syncController(userid);
            my_sync_controller.syncToAndroid_Click(sender, e);

        }

        private void btnSyncFromAndroid_Click(object sender, EventArgs e)
        {
            syncController my_sync_controller = new syncController(userid);
            my_sync_controller.syncFromAndroid_Click(sender, e);
        }

    }
}
