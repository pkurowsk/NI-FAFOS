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
    public partial class SupplierForm : FAFOS.Background
    {
        private int userid;
        Users user;
        Supplier supplier;
        public SupplierForm(int id)
        {
            InitializeComponent();

            //User label
            userid = id;
            user = new Users();
            setup(userid.ToString(), "FAFOS Supplier Form");

            supplier = new Supplier();


        }

        private void txtCreatSupplier_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
