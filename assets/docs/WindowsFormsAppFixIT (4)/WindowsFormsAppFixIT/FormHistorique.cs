using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsAppFixIT
{
    public partial class FormHistorique : Form
    {
        private DateTime d1, d2;
        private string connectionString = @"Server=.\SQLEXPRESS;Database=Fixit;Trusted_Connection=True;";

        public FormHistorique()
        {
            InitializeComponent();
        }





        private void FormHistorique_Load(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            SqlCommand cmd = new SqlCommand("getint", cn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@begindate", this.d1);
            cmd.Parameters.AddWithValue("@enddate", this.d2);

            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                string c1 = Convert.ToDateTime(dr["idateinter"]).ToShortDateString();
                string c2 = Math.Round(Convert.ToDecimal(dr["iprix"]), 2).ToString();
                string c3 = dr["icomment"].ToString();
                string c4 = dr["pnom"].ToString();
                string c5 = Convert.ToDateTime(dr["pinstall"]).ToShortDateString(); 
                string c6 = dr["plife"].ToString();
                string c7 = dr["cnom"].ToString();
                string c8 = dr["ctel"].ToString();
                string c9 = dr["canom"].ToString();

                ListViewItem lv = new ListViewItem(new String[] { c1,c2,c3,c4,c5,c6,c7,c8,c9 });
                listViewHisto.Items.Add(lv);
            }

            dr.Close();
            cn.Close();
        }

        public void setDates(DateTime deb, DateTime fin)
        {
            this.d1 = deb;
            this.d2 = fin;
        }


    }
}
