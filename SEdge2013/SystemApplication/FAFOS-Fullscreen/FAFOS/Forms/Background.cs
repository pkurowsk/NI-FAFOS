using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FAFOS
{
    public partial class Background : Form
    {
        //Users user;
        //String userid;

        public Background()
        {
            InitializeComponent();

            this.button1.MouseEnter += new EventHandler(button1_MouseEnter);
            this.button1.MouseLeave += new EventHandler(button1_MouseLeave);
            this.button1.Location = new Point(65, 38);
            button1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Back2));
            button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;



            //Logo locations
            FireAlertLogo.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - 350, 40);

            pnlUser.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width -
                pnlUser.Size.Width - 15, pnlUser.Location.Y);

            SEdgeLogo.Location = new Point(System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - 250,
                System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - 100);
        }

        public void setup(String name, String title)
        {
            //User label
            //user = new Users();
            //userid = id;
            lblUserInfo.Text = "Welcome\n " + new Users().getName(Convert.ToInt32(name));
            lblPageTitle.Text = title;


            List<Bitmap> piclist = MUser.LoadImages();


            this.profilePic.BackgroundImage = piclist[MUser.GetPicID(name.ToString())];// FAFOS.Properties.Resources.Shades;
            this.profilePic.BackgroundImageLayout = ImageLayout.Stretch;
              
        }

        void button1_MouseLeave(object sender, EventArgs e)
        {
            this.button1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Back2));
            this.button1.Location = new Point(65, 38);
            this.button1.Size = new Size(60, 60);
            this.button1.ImageAlign = ContentAlignment.MiddleCenter;
        }

        void button1_MouseEnter(object sender, EventArgs e)
        {
            this.button1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.BackOver));
            this.button1.Location = new Point(65, 38);
            this.button1.Size = new Size(60, 60);
            this.button1.ImageAlign = ContentAlignment.MiddleCenter;
        }

        public void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
               private bool isLoaded;
               private bool isComplete=false;

    protected override void OnPaint( PaintEventArgs e ) {
        // the base method process the painting
        base.OnPaint( e );

        // this method can be theoretically called before the "Load" event is proceeded
        // , therefore is required to check if "isLoaded == true"
        if ( this.isLoaded ) {
            // now are all events hooked to "Load" method proceeded => the form is loaded
            this.OnLoadComplete( e );
        }
    }

    // your "special" method to handle "load is complete" event
    public void  OnLoadComplete(EventArgs e) 
    {
        isComplete = true ;
    }
    public bool complete()
    {
        return isComplete;
    }

        public void Background_Load(object sender, EventArgs e)
        {

        // notify the "Load" method is complete
        this.isLoaded = true;
        }
    }
}
