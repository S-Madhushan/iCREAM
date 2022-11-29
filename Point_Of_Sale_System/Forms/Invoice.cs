using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Point_Of_Sale_System.Forms
{
    public partial class Invoice : Form
    {
        public Invoice()
        {
            InitializeComponent();
        }

        private void Invoice_Load(object sender, EventArgs e)
        {
            LoadTheme();
            lblUsername.Text = Login.Username;
        }

        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = Themecolor.primaryColor;
                    btn.ForeColor = Color.Black;
                    btn.FlatAppearance.BorderColor = Themecolor.secondaryColor;

                }
            }
            lblUsername.ForeColor = Themecolor.secondaryColor;
            label11.ForeColor = Themecolor.secondaryColor;

        }

        private void list()
        {
            if (guna2DataGridView1.Rows.Count > 0)
            {
                try
                {
                    string connectionString = "server=localhost;database=grocery;uid=root;pwd='';CharSet=utf8";

                    MySqlConnection con = new MySqlConnection(connectionString);
                    MySqlCommand cmd = con.CreateCommand();

                    con.Open();
                    cmd.CommandText = "Insert into invoice (Total,Discount_Total,Paid,Balance,Username) values " +
                        "('" + txtTotal.Text + "','" + txtDiscountTotal.Text + "','" + txtPaid.Text + "','" + txtBalance.Text + "','" + lblUsername.Text + "'); SELECT LAST_INSERT_ID()";

                    int invoice_ID = int.Parse(cmd.ExecuteScalar().ToString());
                    int last = guna2DataGridView1.Rows.Count;

                    for (int i = 0; i < (last - 1); i++)
                    {
                        String s = "Insert into bill (Master_ID ,Name_English ,Price,Discount,Quantity,Bill_total) values " +
                        "('" + invoice_ID + "','" + guna2DataGridView1.Rows[i].Cells["Iname"].Value + "','" + guna2DataGridView1.Rows[i].Cells["Price"].Value + "','" + guna2DataGridView1.Rows[i].Cells["Discount"].Value + "','" + guna2DataGridView1.Rows[i].Cells["Quantity"].Value + "','" + guna2DataGridView1.Rows[i].Cells["Total"].Value + "')";

                        Console.WriteLine(s);
                        cmd.CommandText = s;
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                    MessageBox.Show("Invoice Created Succesfully With Invoice No = " + invoice_ID);
                    Print p = new Print();
                    p.Invoiceid = invoice_ID;
                    p.ShowDialog();


                    guna2DataGridView1.Rows.Clear();
                    clear();

                }


                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    MessageBox.Show("There is a problem. Please contact the Software Engineer");
                }
            }


            else
            {
                MessageBox.Show("Must Add Item in the list");
            }


        }

        private void Item()
        {

            MySqlConnection con = new MySqlConnection("server=localhost;database=grocery;uid=root;pwd='';CharSet=utf8");
            con.Open();


            if (txtItemID.Text != "")
            {

                MySqlCommand cmd = new MySqlCommand("Select Name_English,Price,Discount from item where ID =@ID", con);
                cmd.Parameters.AddWithValue("@ID", (txtItemID.Text));
                MySqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    txtItemNameSinhala.Text = da.GetValue(0).ToString();
                    txtItemPrice.Text = da.GetValue(1).ToString();
                    txtDiscount.Text = da.GetValue(2).ToString();




                }
                con.Close();
            }

        }

        private void updateqty()
        {

            double quantity = double.Parse(numericUpDown1.Text);
            try
            {
                MySqlConnection con = new MySqlConnection("server=localhost;database=grocery;uid=root;pwd='';CharSet=utf8");

                string query = "update item set quantity = quantity - " + numericUpDown1.Text + " where ID ='" + txtItemID.Text + "'";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();


            }

            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("There is a problem. Please contact the Software Engineer");
            }
        }




        private void bill()
        {

            string Name = txtItemNameSinhala.Text;
            double Price = double.Parse(txtItemPrice.Text);
            double Discount = double.Parse(txtDiscount.Text);
            double Quantity = double.Parse(numericUpDown1.Value.ToString());


            double dis = Discount * Quantity;
            double tot = Price * Quantity - dis;


            this.guna2DataGridView1.Rows.Add(Name, Price, dis, Quantity, tot);

            double discounttotal = 0, subtotal = 0;

            for (int row = 0; row < guna2DataGridView1.Rows.Count; row++)
            {

                subtotal = subtotal + Convert.ToDouble(guna2DataGridView1.Rows[row].Cells[4].Value);
                discounttotal = discounttotal + Convert.ToDouble(guna2DataGridView1.Rows[row].Cells[2].Value);

            }
            txtDiscountTotal.Text = discounttotal.ToString();
            txtTotal.Text = subtotal.ToString();



            updateqty();



        }

        private void clear()
        {

            txtItemID.Clear();
            txtItemNameSinhala.Clear();
            txtItemPrice.Clear();
            numericUpDown1.Value = 1;
            txtDiscount.Clear();

            
        }

        private void btnAddBill_Click(object sender, EventArgs e)
        {
            bill();
            clear();
            txtItemID.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == guna2DataGridView1.Columns["Delete"].Index && e.RowIndex >= 0)
            {
                guna2DataGridView1.Rows.Remove(guna2DataGridView1.Rows[e.RowIndex]);


                double discounttotal = 0, subtotal = 0;

                for (int row = 0; row < guna2DataGridView1.Rows.Count; row++)
                {

                    subtotal = subtotal + Convert.ToDouble(guna2DataGridView1.Rows[row].Cells[4].Value);
                    discounttotal = discounttotal + Convert.ToDouble(guna2DataGridView1.Rows[row].Cells[2].Value);

                }
                txtDiscountTotal.Text = discounttotal.ToString();
                txtTotal.Text = subtotal.ToString();


            }
        }

        private void btnPaid_Click(object sender, EventArgs e)
        {
            double total = double.Parse(txtTotal.Text);
            double pay = double.Parse(txtPaid.Text);
            double balance = pay - total;
            txtBalance.Text = balance.ToString();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            list();
        }

        private void txtItemID_TextChanged(object sender, EventArgs e)
        {
            Item();
        }

        private void txtItemPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtDiscount.Focus();
            }
        }

        private void txtDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                numericUpDown1.Focus();
                numericUpDown1.Select(0, numericUpDown1.Text.Length);
            }
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                bill();
                clear();
                txtItemID.Focus();

            }
        }

        private void txtPaid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {


                btnPaid_Click(sender, e);
                txtBalance.Focus();
                this.Refresh();

            }
        }

        private void txtBalance_KeyDown(object sender, KeyEventArgs e)
        {
            btnPrint_Click(sender, e);

            txtTotal.Clear();
            txtBalance.Clear();
            txtPaid.Clear();
            txtDiscountTotal.Clear();


            txtItemID.Focus();
        }

        private void txtItemID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                numericUpDown1.Focus();
                numericUpDown1.Select(0, numericUpDown1.Text.Length);
            }
        }

        private void txtItemPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar)
                    && e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if (e.KeyChar == '.'
                    && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
            catch
            {

            }
        }

        private void txtDiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar)
                    && e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if (e.KeyChar == '.'
                    && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
            catch
            {

            }
        }

        private void numericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar)
                    && e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if (e.KeyChar == '.'
                    && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
            catch
            {

            }
        }

        private void txtPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar)
                    && !char.IsDigit(e.KeyChar)
                    && e.KeyChar != '.')
                {
                    e.Handled = true;
                }

                // only allow one decimal point
                if (e.KeyChar == '.'
                    && (sender as TextBox).Text.IndexOf('.') > -1)
                {
                    e.Handled = true;
                }
            }
            catch
            {

            }
        }
    }
}
