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
    public partial class Profile : Form
    {
        Functions fn = new Functions();
        String query;

        public Profile()
        {
            InitializeComponent();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            LoadTheme();
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
            
        }

        private void Clear()
        {
            txtCompanyAddress.Clear();
            txtCompanyID.Clear();
            txtCompanyName.Clear();
            txtCompanyTlephone.Clear();
            txtMessage1.Clear();
            txtMessage2.Clear();
            txtUsername.Clear();
            txtPassword.Clear();

        }

        private void Item()
        {

            MySqlConnection con = new MySqlConnection("server = localhost; database = grocery; uid = root; pwd = ''; CharSet = utf8");
            con.Open();


            if (txtCompanyID.Text != "")
            {

                MySqlCommand cmd = new MySqlCommand("Select company_name,company_Address,tlephone,footer_Message1,footer_Message2,username,password from profile where company_ID =@company_ID", con);
                cmd.Parameters.AddWithValue("@company_ID", (txtCompanyID.Text));
                MySqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    txtCompanyName.Text = da.GetValue(0).ToString();
                    txtCompanyAddress.Text = da.GetValue(1).ToString();
                    txtCompanyTlephone.Text = da.GetValue(2).ToString();
                    txtMessage1.Text = da.GetValue(3).ToString();
                    txtMessage2.Text = da.GetValue(4).ToString();
                    txtUsername.Text = da.GetValue(5).ToString();
                    txtPassword.Text = da.GetValue(6).ToString();


                }
                con.Close();
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                query = "insert profile values ('" + txtCompanyID.Text + "' , '" + txtCompanyName.Text + "','" + txtCompanyAddress.Text + "','" + txtCompanyTlephone.Text + "','" + txtMessage1.Text + "','" + txtMessage2.Text + "','" + txtUsername.Text + "','" + txtPassword.Text + "')";
                fn.setData(query);

                MessageBox.Show(" Company Profile Data Add succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Clear();
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
                query = "update profile set company_name ='" + txtCompanyName.Text + "', company_Address ='" + txtCompanyAddress.Text + "', tlephone ='" + txtCompanyTlephone.Text + "' , footer_Message1 ='" + txtMessage1.Text + "', footer_Message2 ='" + txtMessage2.Text + "', username ='" + txtUsername.Text + "', password ='" + txtPassword.Text + "' where company_ID ='" + txtCompanyID.Text + "'";
                fn.setData(query);

                MessageBox.Show(" Company Profile Data Update succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Clear();
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
                query = " delete  from profile where company_ID ='" + txtCompanyID.Text + "'";

                fn.setData(query);

                MessageBox.Show(" Company Profile Data Delete succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Clear();

            }

            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("There is a problem. Please contact the Software Engineer");
            }
        }

        private void txtCompanyID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCompanyName.Focus();
            }

            else if (e.KeyCode == Keys.Delete)
            {
                btnDelete_Click(sender, e);
                Clear();
            }
        }

        private void txtCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCompanyAddress.Focus();
            }
        }

        private void txtCompanyAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCompanyTlephone.Focus();
            }
        }

        private void txtCompanyTlephone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMessage1.Focus();
            }
        }

        private void txtMessage1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMessage2.Focus();
            }
        }

        private void txtMessage2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtUsername.Focus();
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(sender, e);
                Clear();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void txtCompanyTlephone_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtCompanyID_TextChanged(object sender, EventArgs e)
        {
            Item();
        }
    }
}
