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
using System.Security.Cryptography;

namespace WindowsFormsAppFixIT
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=Fixit;Trusted_Connection=True;";
        public bool okgo = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormLogin dlg = new FormLogin();
            DialogResult dr = dlg.ShowDialog();
        }
    }
}
