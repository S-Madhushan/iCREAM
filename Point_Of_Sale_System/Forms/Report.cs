using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Point_Of_Sale_System.Forms
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();

        }

        MySqlConnection con = new MySqlConnection("server=localhost;database=grocery;uid=root;pwd='';CharSet=utf8");
        MySqlCommand cmd;
        MySqlDataAdapter dr;
        string date1;
        string date2;
        string date3;

        private void Report_Load(object sender, EventArgs e)
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
            lblList.ForeColor = Themecolor.secondaryColor;

        }

        private void btnView_Click(object sender, EventArgs e)
        {
            date1 = dateTimePicker1.Value.Year + "-" + dateTimePicker1.Value.Month + "-" + dateTimePicker1.Value.Day;
            date2 = dateTimePicker2.Value.Year + "-" + dateTimePicker2.Value.Month + "-" + dateTimePicker2.Value.Day;

           
            DataTable dt = new DataTable();

            cmd = new MySqlCommand("select * from invoice where Date between '" +date1 + "' and '" + date2 + "' ", con);
            dr = new MySqlDataAdapter(cmd);
            dr.Fill(dt);

            CrystalReport1 cr = new CrystalReport1();
            cr.Database.Tables["invoice"].SetDataSource(dt);
            this.crystalReportViewer1.ReportSource = cr;

            con.Close();

        }

        private void btnDailyVView_Click(object sender, EventArgs e)
        {
            DateTime dtdate1 = DateTime.Parse(dateTimePicker3.Text);

            con.Open();

            DataTable dt = new DataTable();

            cmd = new MySqlCommand("SELECT * FROM invoice WHERE Date(date) =  '" + dtdate1.ToString("yyyy-MM-dd") + "'", con);
            dr = new MySqlDataAdapter(cmd);
            dr.Fill(dt);

            CrystalReport1 cr = new CrystalReport1();
            cr.Database.Tables["invoice"].SetDataSource(dt);
            this.crystalReportViewer1.ReportSource = cr;

            con.Close();
        }
    }
}
