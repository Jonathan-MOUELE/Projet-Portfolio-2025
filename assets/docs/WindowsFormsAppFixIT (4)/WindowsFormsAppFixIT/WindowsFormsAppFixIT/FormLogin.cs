using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppFixIT
{
    public partial class FormLogin : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=Fixit;Trusted_Connection=True;";
        public string strlogin, strmdp;

        public string HacherMotDePasse(string motDePasseClair)
        {
            using (SHA256 moteurSha = SHA256.Create())
            {
                byte[] octetsEntree = Encoding.UTF8.GetBytes(motDePasseClair);
                byte[] octetsHaches = moteurSha.ComputeHash(octetsEntree);
                StringBuilder constructeurChaine = new StringBuilder();
                for (int i = 0; i < octetsHaches.Length; i++)
                {
                    constructeurChaine.Append(octetsHaches[i].ToString("x2"));
                }
                return constructeurChaine.ToString();
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.strlogin = textBoxLogin.Text;
            this.strmdp = textBoxMDP.Text;

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            string hashedPassword = HacherMotDePasse(textBoxMDP.Text);

            string sql = "select count(*) from LOGIN where Utilisateur = @leroot and PWD = @lwpd";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@leroot", textBoxLogin.Text);
            cmd.Parameters.AddWithValue("@lwpd", hashedPassword);

            SqlDataReader drcool = cmd.ExecuteReader();

            drcool.Read();
            int nb = drcool.GetInt32(0);

            if (nb == 1)
            {
                ((Form1)Application.OpenForms["Form1"]).okgo = true;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Login ou mot de passe incorrect");
            }

            drcool.Close();
            cn.Close();
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool res = ((Form1)Application.OpenForms["Form1"]).okgo;

            if (res == false)
            {
                Application.Exit();
            }
        }

        public FormLogin()
        {
            InitializeComponent();
        }
    }
}
