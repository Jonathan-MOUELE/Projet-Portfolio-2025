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
    public partial class FormClient : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=Fixit;Trusted_Connection=True;";

        public FormClient()
        {
            InitializeComponent();
        }

        private void FormClient_Load(object sender, EventArgs e)
        {
            FillComboVille();
            FillClient();
        }

        private void FillClient()
        {
            listBoxClient.Items.Clear();
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "SELECT * FROM Client ORDER BY Nom";
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataReader drClient = cmd.ExecuteReader();
            while (drClient.Read())
            {
                listBoxClient.Items.Add(drClient["Nom"].ToString());
            }
            drClient.Close();
            cn.Close();
        }

        private void FillComboVille()
        {
            comboBoxVille.Items.Clear();
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "SELECT * FROM Ville ORDER BY Nom";
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataReader drVille = cmd.ExecuteReader();
            while (drVille.Read())
            {
                int id = Convert.ToInt32(drVille["ID_VILLE"]);
                string nom = drVille["Nom"].ToString();
                comboBoxVille.Items.Add(new ClassItem(id, nom));
            }
            drVille.Close();
            cn.Close();
        }

        private void listBoxClient_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxClient.SelectedItem == null) return;

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "SELECT * FROM Client WHERE Nom = @nomclient";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nomclient", listBoxClient.SelectedItem.ToString());
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                textBoxNom.Text = dr["Nom"].ToString();
                textBoxMail.Text = dr["Mail"].ToString();
                textBoxTel.Text = dr["Tel"].ToString();
                textBoxAdresse.Text = dr["Adresse"].ToString();

                if (dr["ID_VILLE"] != DBNull.Value)
                {
                    int idV = Convert.ToInt32(dr["ID_VILLE"]);
                    foreach (ClassItem item in comboBoxVille.Items)
                    {
                        if (item.ID == idV)
                        {
                            comboBoxVille.SelectedItem = item;
                            break;
                        }
                    }
                }
                else { comboBoxVille.SelectedIndex = -1; }
            }
            dr.Close();
            cn.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (IsPresent(textBoxNom.Text))
            {
                MessageBox.Show("Client déjà présent");
                return;
            }
            if (comboBoxVille.SelectedIndex == -1)
            {
                MessageBox.Show("Veuillez choisir une ville");
                return;
            }

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "INSERT INTO CLIENT (Nom, Mail, Tel, Adresse, ID_VILLE) VALUES (@nom, @mail, @tel, @adr, @idv)";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nom", textBoxNom.Text);
            cmd.Parameters.AddWithValue("@mail", textBoxMail.Text);
            cmd.Parameters.AddWithValue("@tel", textBoxTel.Text);
            cmd.Parameters.AddWithValue("@adr", textBoxAdresse.Text);
            cmd.Parameters.AddWithValue("@idv", ((ClassItem)comboBoxVille.SelectedItem).ID);

            cmd.ExecuteNonQuery();
            cn.Close();
            FillClient();
            ClearFields();
        }

        private void buttonModifier_Click(object sender, EventArgs e)
        {
            if (listBoxClient.SelectedItem == null) return;

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "UPDATE CLIENT SET Nom=@nom, Mail=@mail, Tel=@tel, Adresse=@adr, ID_VILLE=@idv WHERE Nom=@old";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nom", textBoxNom.Text);
            cmd.Parameters.AddWithValue("@mail", textBoxMail.Text);
            cmd.Parameters.AddWithValue("@tel", textBoxTel.Text);
            cmd.Parameters.AddWithValue("@adr", textBoxAdresse.Text);
            cmd.Parameters.AddWithValue("@idv", ((ClassItem)comboBoxVille.SelectedItem).ID);
            cmd.Parameters.AddWithValue("@old", listBoxClient.SelectedItem.ToString());

            cmd.ExecuteNonQuery();
            cn.Close();
            FillClient();
            ClearFields();
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (listBoxClient.SelectedItem == null) return;
            if (MessageBox.Show("Supprimer ?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.No) return;

            try
            {
                SqlConnection cn = new SqlConnection(connectionString);
                cn.Open();
                string sql = "DELETE FROM Client WHERE Nom = @nom";
                SqlCommand cmd = new SqlCommand(sql, cn);
                cmd.Parameters.AddWithValue("@nom", listBoxClient.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
                cn.Close();
                FillClient();
                ClearFields();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547) MessageBox.Show("Client lié à du matériel.");
                else MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private bool IsPresent(string nom)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Client WHERE Nom=@n", cn);
            cmd.Parameters.AddWithValue("@n", nom);
            int count = (int)cmd.ExecuteScalar();
            cn.Close();
            return count > 0;
        }

        private void ClearFields()
        {
            textBoxNom.Clear(); textBoxMail.Clear(); textBoxTel.Clear(); textBoxAdresse.Clear();
            comboBoxVille.SelectedIndex = -1;
        }

        private void buttonFermer_Click(object sender, EventArgs e) => this.Close();
    }
}