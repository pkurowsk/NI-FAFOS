using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FAFOS
{
    public partial class ToMobileView : Form
    {
        public ToMobileView()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSEND_Validating(object sender, CancelEventArgs e)
        {
            if (Convert.ToInt32(PortNumber.Text) <= 1025)
            {
                e.Cancel = true;
                MessageBox.Show("You must enter a valid port number");
            }
            if (IPAddress.Text == "")
            {
                e.Cancel = true;
                MessageBox.Show("You must enter a valid IP address");
            }
        }

        private void btnSEND_Click(object sender, EventArgs e)
        {

        }
    }
}
