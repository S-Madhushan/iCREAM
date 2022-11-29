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
    public partial class Category : Form
    {
        Functions fn = new Functions();
        String query;

        public Category()
        {
            InitializeComponent();
            
        }

        private void Category_Load(object sender, EventArgs e)
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
            txtCategoryID.Clear();
            txtCategoryName.Clear();
            txtCompanyName.Clear();
        }

        private void Item()
        {

            MySqlConnection con = new MySqlConnection("server = localhost; database = grocery; uid = root; pwd = ''; CharSet = utf8");
            con.Open();


            if (txtCategoryID.Text != "")
            {

                MySqlCommand cmd = new MySqlCommand("Select category_Name,company_Name from category where category_ID =@ID", con);
                cmd.Parameters.AddWithValue("@ID", (txtCategoryID.Text));
                MySqlDataReader da = cmd.ExecuteReader();
                while (da.Read())
                {
                    txtCategoryName.Text = da.GetValue(0).ToString();
                    txtCompanyName.Text = da.GetValue(1).ToString();


                }
                con.Close();
            }

        }

        private void populateItem()
        {
            MySqlConnection con = new MySqlConnection("server = localhost; database = grocery; uid = root; pwd = ''; CharSet = utf8");

            string query = " select * from category ";
            MySqlDataAdapter adp = new MySqlDataAdapter(query, con);

            con.Open();
            DataTable tb = new DataTable();
            adp.Fill(tb);

            guna2DataGridView1.DataSource = tb;
            con.Close();
        }


        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                query = "insert into category values ('" + txtCategoryID.Text + "' , '" + txtCategoryName.Text + "','" + txtCompanyName.Text + "')";
                fn.setData(query);

                MessageBox.Show(" Category Add succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                query = "update category set category_Name ='" + txtCategoryName.Text + "', company_Name ='" + txtCompanyName.Text + "' where category_ID = '" + txtCategoryID.Text + "' ";
                fn.setData(query);

                MessageBox.Show(" Category Update succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                query = " delete  from category where category_ID = '" + txtCategoryID.Text + "' ";

                fn.setData(query);

                MessageBox.Show(" Category Delete succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                populateItem();
                Clear();

            }

            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("There is a problem. Please contact the Software Engineer");
            }
        }

        private void txtCategoryID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCategoryName.Focus();
            }

            else if (e.KeyCode == Keys.Delete)
            {
                btnDelete_Click(sender, e);
                Clear();
            }
        }

        private void txtCategoryName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCompanyName.Focus();
            }
        }

        private void txtCompanyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd_Click(sender, e);
                txtCategoryID.Focus();
                Clear();
            }
        }

        private void txtCategoryID_TextChanged(object sender, EventArgs e)
        {
            Item();
        }
    }
}
