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
using System.Data.SqlTypes;
using System.Threading;

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
           
            /*
            FormLogin dlg = new FormLogin();
            DialogResult dr = dlg.ShowDialog();
            */
            fillMatos();

        }

        private void fillMatos()
        {
            comboBoxMatos.Items.Clear();

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            string sql = "select * from Produit order by Nom";
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataReader drMatos = cmd.ExecuteReader();
            while (drMatos.Read())
            {
                int id = Convert.ToInt32(drMatos["ID_PROD"]);
                string nom = drMatos["Nom"].ToString();
                ClassItem it = new ClassItem(id, nom);
                comboBoxMatos.Items.Add(it);          
            }

            drMatos.Close();
            cn.Close();
        }


        private void catégorieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCategorie dlg = new FormCategorie();
            dlg.ShowDialog();
        }

        private void clientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormClient dlg = new FormClient();
            dlg.ShowDialog();
        }

        private void produitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProduit dlg = new FormProduit();
            dlg.ShowDialog();
        }

        private void buttonValider_Click(object sender, EventArgs e)
        {
            if (dateTimePickerInter.Value > DateTime.Now)
            {
                MessageBox.Show("La date d'intervention ne peut être future", "Erreur de date",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                dateTimePickerInter.Focus();
                return;
            }
       
            decimal prix;
            bool b = decimal.TryParse(textBoxPrix.Text, out prix);
            if (!b)
            {
                MessageBox.Show("Le prix doit être un nombre décimal", "Erreur de prix",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPrix.Focus();
                return;
            }

            ClassItem it = (ClassItem)comboBoxMatos.SelectedItem;
            if (it == null)
            {
                MessageBox.Show("Vous devez sélectionner un matériel", "Erreur de matériel",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                comboBoxMatos.Focus();
                return;
            }

            int idMatos = it.ID;

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "insert into Intervention values (@dateIntervention, @prix, @comment, @idProd)";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@dateIntervention", dateTimePickerInter.Value);
            cmd.Parameters.AddWithValue("@prix", prix);
            cmd.Parameters.AddWithValue("@comment", textBoxComment.Text);
            cmd.Parameters.AddWithValue("@idProd", idMatos);
            cmd.ExecuteNonQuery();


            string sqlup = "UPDATE PRODUIT SET Date_Install = @dateinter WHERE ID_PROD = @idprod";
            SqlCommand cmdUp = new SqlCommand(sqlup, cn);
            cmdUp.Parameters.AddWithValue("@dateinter", dateTimePickerInter.Value);
            cmdUp.Parameters.AddWithValue("@idProd", idMatos);
            cmdUp.ExecuteNonQuery();

            cn.Close();
            MessageBox.Show("Intervention enregistrée", "Enregistrement",
                MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void buttonFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonHistorique_Click(object sender, EventArgs e)
        {
            FormHistorique dlg = new FormHistorique();
            dlg.setDates(dateTimePickerDebut.Value, dateTimePickerFin.Value);
            dlg.ShowDialog();
        }
    }
}
