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
    public partial class Print : Form
    {
        public Print()
        {
            InitializeComponent();
        }

        private int invoiceid;

        public int Invoiceid
        {
            get { return invoiceid; }
            set { invoiceid = value; }
        }


        private void Print_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;

            MySqlConnection con = new MySqlConnection("server=localhost;database=grocery;uid=root;pwd='';CharSet=utf8");
            MySqlCommand cmd;


            MySqlDataAdapter dr;


            con.Open();
            DataTable dt = new DataTable();
            cmd = new MySqlCommand("select * from Final_Bill where invoice_ID = '" + Invoiceid + "' ", con);

            dr = new MySqlDataAdapter(cmd);
            dr.Fill(dt);

            con.Close();

            CrystalReport2 cr = new CrystalReport2();
            cr.Database.Tables["Final_Bill"].SetDataSource(dt);

            this.crystalReportViewer1.ReportSource = cr;

            cr.PrintToPrinter(1, false, 0, 0);
        }
    }
}
