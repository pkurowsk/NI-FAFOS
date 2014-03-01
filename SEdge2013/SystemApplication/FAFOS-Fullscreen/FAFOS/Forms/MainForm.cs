using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows;
using tiles;
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

        delegate void SetSizeCallback(int w, int h, tile T);
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

            notificationPanel.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - Convert.ToInt32(notificationPanel.Size.Width)-20,
               notificationPanel.Location.Y);

            pnlUser.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - Convert.ToInt32(pnlUser.Size.Width) - 15,
              pnlUser.Location.Y);


            this.quote.Enter += new System.EventHandler(Tile_Enter);
            this.editQuote.Enter += new System.EventHandler(Tile_Enter);
            this.salesOrder.Enter += new System.EventHandler(Tile_Enter);
            this.editSalesOrder.Enter += new System.EventHandler(Tile_Enter);
            this.convertSalesOrder.Enter += new System.EventHandler(Tile_Enter);
            this.invoice.Enter += new System.EventHandler(Tile_Enter);
            this.inventory.Enter += new System.EventHandler(Tile_Enter);
            this.purchaseRecord.Enter += new System.EventHandler(Tile_Enter);
            this.payment.Enter += new System.EventHandler(Tile_Enter);
            this.itinerary.Enter += new System.EventHandler(Tile_Enter);
            this.inspection.Enter += new System.EventHandler(Tile_Enter);

            this.addClient.Enter += new System.EventHandler(Tile_Enter);
            this.editClient.Enter += new System.EventHandler(Tile_Enter);
            this.addContract.Enter += new System.EventHandler(Tile_Enter);
            this.editContract.Enter += new System.EventHandler(Tile_Enter);

            this.statement.Enter += new System.EventHandler(Tile_Enter);
            this.jobReport.Enter += new System.EventHandler(Tile_Enter);
            this.royaltyFee.Enter += new System.EventHandler(Tile_Enter);
            this.revenueReport.Enter += new System.EventHandler(Tile_Enter);
          //  this.allRevenue.Enter += new System.EventHandler(Tile_Enter);

            user = new Users();

            screenWidth = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width;
            screenHeight = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height; 
            this.Size = new System.Drawing.Size((int)screenWidth, (int)screenHeight);

            this.quote.tileTimer_Interval = 40;
            this.quote.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.editQuote.tileTimer_Interval = 40;
            this.editQuote.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.salesOrder.tileTimer_Interval = 40;
            this.salesOrder.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.editSalesOrder.tileTimer_Interval = 40;
            this.editSalesOrder.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.convertSalesOrder.tileTimer_Interval = 40;
            this.convertSalesOrder.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.invoice.tileTimer_Interval = 40;
            this.invoice.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.inventory.tileTimer_Interval = 40;
            this.inventory.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.purchaseRecord.tileTimer_Interval = 40;
            this.purchaseRecord.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.payment.tileTimer_Interval = 40;
            this.payment.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.itinerary.tileTimer_Interval = 40;
            this.itinerary.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.inspection.tileTimer_Interval = 40;
            this.inspection.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);

            this.addClient.tileTimer_Interval = 40;
            this.addClient.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.editClient.tileTimer_Interval = 40;
            this.editClient.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.addContract.tileTimer_Interval = 40;
            this.addContract.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.editContract.tileTimer_Interval = 40;
            this.editContract.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);

            this.statement.tileTimer_Interval = 40;
            this.statement.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.royaltyFee.tileTimer_Interval = 40;
            this.royaltyFee.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.jobReport.tileTimer_Interval = 40;
            this.jobReport.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
            this.revenueReport.tileTimer_Interval = 40;
            this.revenueReport.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);
          //  this.allRevenue.tileTimer_Interval = 40;
          //  this.allRevenue.RaisetileTimer_Elapsed += new System.Timers.ElapsedEventHandler(RaisetileTimer_Elapsed);

            FireAlertLogo.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - 350,40);
            SEdgeLogo.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - 250,
                System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - 100);
            Exit_btn.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - 430,
                System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - 160);

            
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
                    serviceNotification.Text += "\n" + service + " needs to be completed by today at ";
                    serviceNotification.Text += dt2.Rows[i][4].ToString() + ", " + dt2.Rows[i][5].ToString() + "\n";
                }
            }
            if (serviceNotification.Text == "")
                serviceNotification.Text = "None";




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

            tile _tile = (tile)sender;
            _tile.Tag = "0";

            screenCenterLeft = (int)((screenWidth / 2) - (_tile.tileWidth / 2));
            screenCenterTop = (int)((screenHeight / 2) - (_tile.tileHieght / 2));

            _tile.XScale = ((int)(formWidth - _tile.tileWidth) / 15);
            _tile.YScale = ((int)(formHeight - _tile.tileHieght) / 15);




            this.Controls[_tile.Name].BringToFront();

            _tile.timerStart();


        }


        private void RaisetileTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            tile _tile = (tile)sender;
            TileSizeControl(_tile.tileWidth + _tile.XScale, _tile.tileHieght + _tile.YScale, _tile);
        }

        public void TileSizeControl(int w, int h, tile T)
        {
            SetSizeCallback d = new SetSizeCallback(SetSize);
            this.Invoke(d, new object[] { w, h, T });
        }


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
                if (T.Name == "invoice")
                {
                    InvoiceForm invoice_form = new InvoiceForm(userid);
                    invoice_form.Show();
                }
                else if (T.Name == "quote")
                {
                    QuoteController qc = new QuoteController(userid.ToString());
                    qc.quote(1);
                }
                else if (T.Name == "editQuote")
                {
                    QuoteController qc = new QuoteController(userid.ToString());
                    qc.quote(2);
                }
                else if (T.Name == "salesOrder")
                {
                    my_controller.salesOrder(1);
                    //payment_form.Show();
                }
                else if (T.Name == "convertSalesOrder")
                {
                    my_controller.salesOrder(2);
                    //payment_form.Show();
                }
                else if (T.Name == "editSalesOrder")
                {
                    my_controller.salesOrder(3);
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
                    form.Show();*/
                }
                else if (T.Name == "purchaseRecord")
                {
                    PurchaseRecord form = new PurchaseRecord(userid);
                    form.Show();
                    // T.timerStop();

                }
                else if (T.Name == "itinerary")
                {
                    MapsForm form = new MapsForm(userid, orders, services);
                    
                   
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
                    r.report(1) ;
                  //  Report form = new InspectionForm(userid.ToString());
                   // form.Show();
                }
                T.tileReset();
                T.showFrame(0);

            }
        }


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
               // userid = 1;
                pnlLogin.Visible = false;
                this.Exit_btn.Focus();
                quote.Visible = true;
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

                syncAndroid.Visible = true;
                syncHQ.Visible = true;

                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label9.Visible = true;

                
                lblUsername.Visible = false;
                txtUsername.Visible = false;
                lblPassword.Visible = false;
                txtPassword.Visible = false;
                Login_btn.Visible = false;
                Logout_btn.Visible = true;
                userSettings.Visible = true;
                lblUserInfo.Visible = true;
                notificationPanel.Visible = true;
                profilePic.Visible = true;
                pnlUser.Visible = true;

                
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
                
                string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory);

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
            quote.Visible = false;
            editQuote.Visible = false;
            salesOrder.Visible = false;
            editSalesOrder.Visible = false;
            convertSalesOrder.Visible = false;

            invoice.Visible = false;
            inventory.Visible = false;
            purchaseRecord.Visible = false;
            payment.Visible = false;
            itinerary.Visible = false;
            inspection.Visible = false;

            addClient.Visible = false;
            addContract.Visible = false;
            editClient.Visible = false;
            editContract.Visible = false;
            statement.Visible = false;
            jobReport.Visible = false;
            revenueReport.Visible = false;
            royaltyFee.Visible = false;
           // allRevenue.Visible = false;

            syncAndroid.Visible = false;
            syncHQ.Visible = false;

            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            pnlUser.Visible = false;

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

 


    }
}
