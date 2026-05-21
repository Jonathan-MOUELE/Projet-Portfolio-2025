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

        public FormLogin()
        {
            InitializeComponent();
        }

        // Méthode de hachage SHA256
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

        private bool IsLoginPresent(string username)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "select count(*) as nb from LOGIN where Utilisateur = @lenom";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@lenom", username);
            int count = (int)cmd.ExecuteScalar();
            cn.Close(); 
            return count > 0;
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if( this.IsLoginPresent(textBoxLogin.Text)== true )
            {
                MessageBox.Show("Identifiant déjà utlisé", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxLogin.Focus(); return;
            }
           
            if (textBoxMDP.Text != textBox1.Text)
            {
                MessageBox.Show("Le mot de passe et la confirmation ne sont pas identiques.");
                return; // On arrête tout
            }

            this.strlogin = textBoxLogin.Text;
            this.strmdp = textBoxMDP.Text;

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    cn.Open();
                    string hashedPassword = HacherMotDePasse(this.strmdp);

                    string sql = "SELECT COUNT(*) FROM LOGIN WHERE Utilisateur = @leroot AND PWD = @lwpd";
                    using (SqlCommand cmd = new SqlCommand(sql, cn))
                    {
                        cmd.Parameters.AddWithValue("@leroot", this.strlogin);
                        cmd.Parameters.AddWithValue("@lwpd", hashedPassword);

                        int nb = (int)cmd.ExecuteScalar();

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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur de connexion : " + ex.Message);
            }
        }

        private void FormLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool res = ((Form1)Application.OpenForms["Form1"]).okgo;
            if (res == false)
            {
                Application.Exit();
            }
        }

        // Cet événement surveille la saisie du MDP
        private void textBoxMDP_TextChanged(object sender, EventArgs e)
        {
            // On peut changer la couleur si le mot de passe est trop court
            if (textBoxMDP.Text.Length < 8)
            {
                textBoxMDP.ForeColor = Color.Red;
            }
            else
            {
                textBoxMDP.ForeColor = Color.Black;
            }
        }

        // Cet événement (textBox1 est ta confirmation) compare en temps réel
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Correction ici : ajout de .Text pour éviter l'erreur de compilation
            if (textBox1.Text != textBoxMDP.Text)
            {
                textBox1.BackColor = Color.MistyRose; // Alerte visuelle
            }
            else
            {
                textBox1.BackColor = Color.White;
            }
        }
    }
}