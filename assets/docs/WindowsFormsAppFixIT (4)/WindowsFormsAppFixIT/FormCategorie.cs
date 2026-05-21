using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppFixIT
{
    public partial class FormCategorie : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=Fixit;Trusted_Connection=True;";
        public FormCategorie()
        {
            InitializeComponent();
        }

        private void FormCategorie_Load(object sender, EventArgs e)
        {
            FillCat();
        }

        private void FillCat()
        {
            listBoxCategorie.Items.Clear();

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            string sql = "select * from Categorie order by Nom";
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataReader drcat = cmd.ExecuteReader();
            while (drcat.Read())
            {
                string nom = drcat["Nom"].ToString();
                listBoxCategorie.Items.Add(nom);
            }

            drcat.Close();
            cn.Close();

            textBoxCategorie.Text = "";
        }

        private void listBoxCategorie_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxCategorie.Text = listBoxCategorie.SelectedItem.ToString();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (IsPresent(textBoxCategorie.Text))
            {
                MessageBox.Show("Catégorie déjà présente");
                return;
            }
            if (textBoxCategorie.Text == "")
            {
                MessageBox.Show("Catégorie vide");
                textBoxCategorie.Focus();
                return;
            }
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "INSERT INTO CATEGORIE VALUES(@thecat)";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@thecat", textBoxCategorie.Text);
            cmd.ExecuteNonQuery();
            cn.Close();
            FillCat();
        }
        private bool IsPresent(string cat)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "SELECT COUNT(*) FROM CATEGORIE WHERE Nom = @thecat";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@thecat", cat);
            int count = (int)cmd.ExecuteScalar();
            cn.Close();
            return count > 0;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonModifier_Click(object sender, EventArgs e)
        {
            if (IsPresent(textBoxCategorie.Text))
            {
                MessageBox.Show("Catégorie déjà présente");
                return;
            }
            if (textBoxCategorie.Text == "")
            {
                MessageBox.Show("Catégorie vide");
                textBoxCategorie.Focus();
                return;
            }
            if (listBoxCategorie.SelectedIndex == -1)
            {
                MessageBox.Show("Veullez sélectionner une catégorie");
                listBoxCategorie.Focus();
                return;
            }

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "UPDATE CATEGORIE SET Nom = @nouvcat WHERE Nom = @oldcat";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nouvcat", textBoxCategorie.Text);
            cmd.Parameters.AddWithValue("@oldcat", listBoxCategorie.SelectedItem.ToString());
            cmd.ExecuteNonQuery();
            cn.Close();
            FillCat();
        }

        private void buttonSupprimer_Click(object sender, EventArgs e)
        {
            if (listBoxCategorie.SelectedIndex == -1)
            {
                MessageBox.Show("Veullez sélectionner une catégorie");
                listBoxCategorie.Focus();
                return;
            }
            if (MessageBox.Show("Voulez-vous vraiment supprimer cette catégorie ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "DELETE FROM CATEGORIE WHERE Nom = @oldcat";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@oldcat", listBoxCategorie.SelectedItem.ToString());
            cmd.ExecuteNonQuery();
            cn.Close();
            FillCat();
        }



    }
}
