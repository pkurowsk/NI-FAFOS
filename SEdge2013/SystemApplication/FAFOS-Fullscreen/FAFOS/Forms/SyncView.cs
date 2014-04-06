using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FAFOS
{
    public partial class SyncView : Form
    {
        //====sync
        syncController my_sync_controller = null;
        //====sync

        public SyncView()
        {
            InitializeComponent();


            // add this part at the end of the "Login_btn_Click" method
            // we will hardcode the value for userid just in this sample code
            // delete the following line in the FAFOS application
            int userid = 1;
            //====sync
            my_sync_controller = new syncController(userid);
            this.syncFromAndroid.Click += new System.EventHandler(my_sync_controller.syncFromAndroid_Click);
            this.syncToAndroid.Click += new System.EventHandler(my_sync_controller.syncToAndroid_Click);
            //====sync

        }
    }
}
