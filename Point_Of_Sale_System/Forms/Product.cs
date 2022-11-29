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
    public partial class Product : Form
    {
        Functions fn = new Functions();
        String query;

        public Product()
        {
            InitializeComponent();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            LoadTheme();
            category();
            populateItem();
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
            lblList.ForeColor = Themecolor.secondaryColor;
        }





        private void Item()
        {

            MySqlConnection con = new MySqlConnection("server=localhost;database=grocery;uid=root;pwd='';CharSet=utf8");
            con.Open();


            if (txtItemID.Text != "")
            {

                MySqlCommand cmd = new MySqlCommand("Select Name_English,Category,Price,Discount,Quantity from item where ID =@ID", con);
                cmd.Parameters.AddWithValue("@ID", (txtItemID.Text));
                MySqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    txtItemNameEnglish.Text = da.GetValue(0).ToString();
                    guna2ComboBoxCategory.SelectedItem = da.GetValue(1).ToString();
                    txtItemPrice.Text = da.GetValue(2).ToString();
                    numericUpDown1.Value = Convert.ToDecimal(da.GetValue(3));
                    txtQuantity.Text = da.GetValue(4).ToString();

                }
                con.Close();
            }

        }

        private void category()
        {
            MySqlConnection con = new MySqlConnection("server=localhost;database=grocery;uid=root;pwd='';CharSet=utf8");
            con.Open();
            MySqlCommand cmd = new MySqlCommand("select * from category ", con);
            MySqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("category_Name", typeof(string));
            dt.Load(rdr);
            guna2ComboBoxCategory.ValueMember = "category_Name";
            guna2ComboBoxCategory.DataSource = dt;


            con.Close();
        }

        private void populateItem()
        {
            MySqlConnection con = new MySqlConnection("server=localhost;database=grocery;uid=root;pwd='';CharSet=utf8");

            string query = " select * from item ";
            MySqlDataAdapter adp = new MySqlDataAdapter(query, con);

            con.Open();
            DataTable tb = new DataTable();
            adp.Fill(tb);

            guna2DataGridView1.DataSource = tb;
            con.Close();
        }

        private void clear()
        {
            txtItemID.Clear();
            txtItemNameEnglish.Clear();
            
            txtItemPrice.Clear();
            txtQuantity.Clear();
            numericUpDown1.Value = 0;
            guna2ComboBoxCategory.SelectedIndex = -1;


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                query = "insert into item values('" + txtItemID.Text + "','" + txtItemNameEnglish.Text + "','" + guna2ComboBoxCategory.Text + "','" + txtItemPrice.Text + "','" + numericUpDown1.Text + "','" + txtQuantity.Text + "')";
                fn.setData(query);

                MessageBox.Show(" Item Add succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                populateItem();
                clear();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("There is a problem. Please contact the Software Engineer");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                query = "update item set Name_English ='" + txtItemNameEnglish.Text + "', Category ='" + guna2ComboBoxCategory.Text + "', Price  ='" + txtItemPrice.Text + "', Discount  ='" + numericUpDown1.Text + "', Quantity ='" + txtQuantity.Text + "' where ID ='" + txtItemID.Text + "'";
                fn.setData(query);

                MessageBox.Show(" Item Update succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                populateItem();
                clear();
            }

            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("There is a problem. Please contact the Software Engineer");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                query = " delete  from item where ID = '" + txtItemID.Text + "' ";

                fn.setData(query);

                MessageBox.Show(" Item Delete succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                populateItem();
                clear();

            }

            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("There is a problem. Please contact the Software Engineer");
            }
        }

        private void txtItemID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItemNameEnglish.Focus();
            }

            else if (e.KeyCode == Keys.Delete)
            {
                btnDelete_Click(sender, e);
                clear();
            }
        }

        private void txtItemNameEnglish_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    guna2ComboBoxCategory.Focus();
                    category();
                }
            }
        }

        private void txtItemNameSinhala_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void guna2ComboBoxCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItemPrice.Focus();
            }
        }

        private void txtItemPrice_KeyDown(object sender, KeyEventArgs e)
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
                txtQuantity.Focus();
            }
        }

        private void txtQuantity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(sender, e);
                txtItemID.Focus();
                clear();
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

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtItemID_TextChanged(object sender, EventArgs e)
        {
            Item();
        }
    }
}
