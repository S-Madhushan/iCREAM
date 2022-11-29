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
    public partial class Store : Form
    {
        public Store()
        {
            InitializeComponent();
        }

        private void Store_Load(object sender, EventArgs e)
        {
            populateItem();
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
            lblList.ForeColor = Themecolor.secondaryColor;
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
    }
}
