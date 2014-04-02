using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FAFOS
{
    public partial class FromMobileView : Form
    {
        syncController _controller;

        // This delegate enables asynchronous calls for setting
        // the text property on a InfoBox control.
        delegate void SetInfoCallback(string info);
        delegate void SetCommCallback(string request);
        delegate void SetIPServerCallback(string frame);


        public FromMobileView()
        {
            InitializeComponent();

            // add this part at the end of the "Login_btn_Click" method
            // we will hardcode the value for userid just in this sample code
            // delete the following line in the FAFOS application
            int userid = 1;
            //====sync
            _controller = new syncController(userid);
            this.Listen_btn.Click += new System.EventHandler(_controller.Listen_btn_Click);
            //====sync      

 
        }

        public String GetPortNumber()
        {
            return this.PortNumber.Text.ToString() ;
        }

        public void addIPServer(String _msg)
        {
            this.IPServer.Text = _msg;
        }


        public void add_text(String _msg)
        {
            this.InfoBox.Text += _msg;
        }

        public void add_request(String _req)
        {
            this.reqBox.Text += _req;
        }

        public void SetInfoBox(String _msg)
        {
            string text = _msg;
            SetInfoCallback d = new SetInfoCallback(add_text);
            this.Invoke(d, new object[] { text });
        }

        public void setIPServer(String _msg)
        {
            string text = _msg;
            SetIPServerCallback d = new SetIPServerCallback(addIPServer);
            this.Invoke(d, new object[] { text });
        }

        public void SetCommBox(String _msg)
        {
            string text = _msg;
            SetCommCallback d = new SetCommCallback(add_request);
            this.Invoke(d, new object[] { text });
        }

        public void Disable_Listen()
        {
            this.Listen_btn.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
