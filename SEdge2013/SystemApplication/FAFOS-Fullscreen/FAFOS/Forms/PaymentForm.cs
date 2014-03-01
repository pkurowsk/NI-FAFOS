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
    public partial class PaymentForm : FAFOS.Background
    {
        private int userid;
        Users user;
        Payment payment;

        public PaymentForm(int id)
        {

            InitializeComponent();

            //User label
            userid = id;
            user = new Users();
            setup(userid.ToString(), "FAFOS Payment Form");


            payment = new Payment();
            txtInvoice.DataSource = payment.getNotPaid(userid);
            txtInvoice.DisplayMember = "id";
            txtInvoice.ValueMember = "id";

            // Enable the owner draw on the ComboBox.
            this.txtInvoice.DrawMode = DrawMode.OwnerDrawFixed;
            // Handle the DrawItem event to draw the items.
            this.txtInvoice.DrawItem += delegate(object cmb, DrawItemEventArgs args)
            {
                // Draw the default background
                args.DrawBackground();


                // The ComboBox is bound to a DataTable,
                // so the items are DataRowView objects.
                DataRowView drv = (DataRowView)this.txtInvoice.Items[args.Index];

                // Retrieve the value of each column.
                string paymentid = drv["id"].ToString();
                string date = drv["Date Issued"].ToString();
                string due = drv["Date Due"].ToString();
                string total = drv["Total"].ToString();
                // Get the bounds for the first column
                Rectangle r1 = args.Bounds;
                r1.Width =400/ 4;

                // Draw the text on the first column
                using (SolidBrush sb = new SolidBrush(args.ForeColor))
                {
                    args.Graphics.DrawString(paymentid,  new Font(args.Font.FontFamily, args.Font.Size, FontStyle.Bold), sb, r1);
                }

                Rectangle r2 = drawColumns(args, r1, date, 100);
               Rectangle r3 = drawColumns(args, r2, due,200);
               drawColumns(args, r3, total, 300);

               txtInvoice.DropDownWidth = 450;

            };


        }
        int DropDownWidth(ComboBox myCombo)
        {
            int maxWidth = 0, temp = 0;
            foreach (var obj in myCombo.Items)
            {
                temp = TextRenderer.MeasureText(obj.ToString(), myCombo.Font).Width;
                if (temp > maxWidth)
                {
                    maxWidth = temp;
                }
            }
            return maxWidth;
        }
        private Rectangle drawColumns(DrawItemEventArgs args, Rectangle r1, string value, int x)
        {
            // Draw a line to isolate the columns 
            using (Pen p = new Pen(Color.Black))
            {
                args.Graphics.DrawLine(p, r1.Right, 0, r1.Right, r1.Bottom);
            }

            // Get the bounds for the second column
            Rectangle r2 = args.Bounds;
            r2.X = x;
            r2.Width = 400/ 4;

            // Draw the text on the second column
            using (SolidBrush sb = new SolidBrush(args.ForeColor))
            {
                args.Graphics.DrawString(value, args.Font, sb, r2);
            }
            return r2;
        }
      /*  private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void button1_MouseLeave(object sender, EventArgs e)
        {
            this.button1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.Back2));
            this.button1.Location = new Point(65, 38);
            this.button1.Size = new Size(84, 78);
            this.button1.ImageAlign = ContentAlignment.MiddleCenter;
        }


        void button1_MouseEnter(object sender, EventArgs e)
        {
            this.button1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.BackOver));
            this.button1.Location = new Point(65, 38);
            this.button1.Size = new Size(84, 78);
            this.button1.ImageAlign = ContentAlignment.MiddleCenter;
        }*/

        private void txtInvoice_SelectedValueChanged(object sender, EventArgs e)
        {
            if (txtInvoice.SelectedValue.ToString() == "" || txtInvoice.SelectedValue.ToString() == "ID")
            {
                InvoiceBox.Visible = false;
                PaymentBox.Visible = false;
                Submit_btn.Visible = false;
            }
            else
            {
                InvoiceBox.Visible = true;
                PaymentBox.Visible = true;
                Submit_btn.Visible = true;

               DataRowView drv = (DataRowView)this.txtInvoice.Items[txtInvoice.SelectedIndex];
                double paid=0;
                txtTotal.Text = drv["Total"].ToString();
               // MessageBox.Show(drv["id"].ToString());
                if (drv["id"].ToString() != "")
                {
                    DataTable dt = payment.getAmount(drv["id"].ToString());
                    paymentTable.DataSource = dt;
                    for (int i = 0; i < dt.Rows.Count; i++)
                        paid += Convert.ToDouble(dt.Rows[i][2]);
                    txtBalance.Text = (Convert.ToDouble(drv["Total"].ToString()) - paid).ToString();
                }
            }
            
        }

        private void Submit_btn_Click(object sender, EventArgs e)
        {
            //Add Payment
            if (txtAmount.Text != "0" && txtAmount.Text != "" && txtAmount.Text != "$0.00")
            {
                Payment pay = new Payment();
                int payId;

                payId = pay.set("'" + DateTime.Today.Date.ToString() + "','" + txtType.Text + "'," +
                            txtAmount.Text + ",'" + txtRemarks.Text + "'," +
                            new ClientContract().getClient(new SalesOrder().getSAddress(new Invoice().getSalesOrderID(txtInvoice.SelectedValue.ToString()).ToString())));

                pay.setIP(txtInvoice.SelectedValue.ToString() + "," + payId);

                //Check if all of invoice is paid off
                if (txtAmount.Text == txtBalance.Text)
                {
                    new Invoice().update(txtInvoice.SelectedValue.ToString());
                    MessageBox.Show("Invoice has been fully paid.");
                }
                else
                    MessageBox.Show("Payment has been processed.");
                this.Close();
            }
            else
            {
                MessageBox.Show("The payment amount must be greater than 0.");
            }
        }
    }
}
