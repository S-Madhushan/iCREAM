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
    public partial class Employee : Form
    {
        Functions fn = new Functions();
        String query;

        public Employee()
        {
            InitializeComponent();
        }

        private void Employee_Load(object sender, EventArgs e)
        {
            LoadTheme();
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

        private void Clear()
        {
            txtEmployeeID.Clear();
            txtEmployeeName.Clear();
            guna2ComboBoxPositions.SelectedIndex = -1;
            txtPassword.Clear();
            txtUsername.Clear();
            txtEmployeeTlephone.Clear();
        }

        private void Item()
        {

            MySqlConnection con = new MySqlConnection("server = localhost; database = grocery; uid = root; pwd = ''; CharSet = utf8");
            con.Open();


            if (txtEmployeeID.Text != "")
            {

                MySqlCommand cmd = new MySqlCommand("Select Name,Position,Tlephone,Username,Password from employee where ID =@ID", con);
                cmd.Parameters.AddWithValue("@ID", (txtEmployeeID.Text));
                MySqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    txtEmployeeName.Text = da.GetValue(0).ToString();
                    guna2ComboBoxPositions.SelectedItem = da.GetValue(1).ToString();
                    txtEmployeeTlephone.Text = da.GetValue(2).ToString();
                    txtUsername.Text = da.GetValue(3).ToString();
                    txtPassword.Text = da.GetValue(4).ToString();

                }
                con.Close();
            }

        }

        private void populateItem()
        {
            MySqlConnection con = new MySqlConnection("server = localhost; database = grocery; uid = root; pwd = ''; CharSet = utf8");

            string query = " select * from employee ";
            MySqlDataAdapter adp = new MySqlDataAdapter(query, con);

            con.Open();
            DataTable tb = new DataTable();
            adp.Fill(tb);

            guna2DataGridView1.DataSource = tb;
            con.Close();
        }

        private void txtEmployeeID_TextChanged(object sender, EventArgs e)
        {
            Item();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                query = "insert into employee values ('" + txtEmployeeID.Text + "' , '" + txtEmployeeName.Text + "','" + guna2ComboBoxPositions.Text + "','" + txtEmployeeTlephone.Text + "','" + txtUsername.Text + "','" + txtPassword.Text + "')";
                fn.setData(query);

                MessageBox.Show(" Employee Data Add succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                populateItem();
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
                query = "update employee set Name ='" + txtEmployeeName.Text + "', Position ='" + guna2ComboBoxPositions.Text + "',Tlephone  ='" + txtEmployeeTlephone.Text + "',Username  ='" + txtUsername.Text + "',Password  ='" + txtPassword.Text + "'  where ID ='" + txtEmployeeID.Text + "'";
                fn.setData(query);

                MessageBox.Show(" Employee Data Update succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                populateItem();
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
                query = " delete  from employee where ID = '" + txtEmployeeID.Text + "' ";

                fn.setData(query);

                MessageBox.Show(" Employee Data Delete succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                populateItem();
                Clear();

            }

            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("There is a problem. Please contact the Software Engineer");
            }
        }

        private void txtEmployeeID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEmployeeName.Focus();
            }

            else if (e.KeyCode == Keys.Delete)
            {
                btnDelete_Click(sender, e);
                Clear();
            }
        }

        private void txtEmployeeName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                guna2ComboBoxPositions.Focus();
            }
        }

        private void guna2ComboBoxPositions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtEmployeeTlephone.Focus();
            }
        }

        private void txtEmployeeTlephone_KeyDown(object sender, KeyEventArgs e)
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
                txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(sender, e);
                txtEmployeeID.Focus();
                Clear();
            }
        }

        private void txtEmployeeTlephone_KeyPress(object sender, KeyPressEventArgs e)
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

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 5 && e.Value != null)
            {
                e.Value = new string('*', e.Value.ToString().Length);
            }
        }
    }
}
