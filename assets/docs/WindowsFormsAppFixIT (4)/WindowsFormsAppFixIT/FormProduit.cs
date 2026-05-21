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
    public partial class FormProduit : Form
    {
        private string connectionString = @"Server=.\SQLEXPRESS;Database=Fixit;Trusted_Connection=True;";

        public FormProduit()
        {
            InitializeComponent();
        }

        private void FormProduit_Load(object sender, EventArgs e)
        {
            FillProduit();
            FillComboClient();
            FillComboCat();
        }

        private void FillComboClient()
        {
            comboBoxClient.Items.Clear();

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            string sql = "select * from Client order by Nom";
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataReader drClient = cmd.ExecuteReader();
            while (drClient.Read())
            {
                int id = Convert.ToInt32(drClient["ID_CLIENT"]);
                string nom = drClient["Nom"].ToString();
                ClassItem it = new ClassItem(id, nom);
                comboBoxClient.Items.Add(it);
            }

            drClient.Close();
            cn.Close();

        }

        private void FillProduit()
        {
            listBoxProduit.Items.Clear();

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            string sql = "select * from Produit order by Nom";
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataReader drClient = cmd.ExecuteReader();
            while (drClient.Read())
            {
                string nom = drClient["Nom"].ToString();
                listBoxProduit.Items.Add(nom);
            }

            drClient.Close();
            cn.Close();

            clearfields();
        }

        private void FillComboCat()
        {
            comboBoxCat.Items.Clear();

            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            string sql = "select * from Categorie order by Nom";
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataReader drClient = cmd.ExecuteReader();
            while (drClient.Read())
            {
                int id = Convert.ToInt32(drClient["ID_CAT"]);
                string nom = drClient["Nom"].ToString();
                ClassItem it = new ClassItem(id, nom);
                comboBoxCat.Items.Add(it);
            }

    

            drClient.Close();
            cn.Close();

        }

        private void clearfields()
        {
            textBoxNom.Text = "";
            textBoxPrix.Text = "";
            dateTimePickerDI.Value= DateTime.Now;
            numericUpDownMTBF.Value = 0;
            textBoxMarque.Text = "";
            comboBoxCat.SelectedIndex = -1;
            comboBoxClient.SelectedIndex = -1;
        }

        private void listBoxProduit_SelectedIndexChanged(object sender, EventArgs e)
        {
            // remplir les champs avec les bonnes valeurs !!!!!!!!!!!
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();

            string sql = "select * from produit where nom = @nomprod";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nomprod", listBoxProduit.SelectedItem.ToString());
            SqlDataReader drproduit = cmd.ExecuteReader();
            drproduit.Read();

            textBoxNom.Text = drproduit["Nom"].ToString();
            textBoxPrix.Text = drproduit["Prix"].ToString();
            dateTimePickerDI.Value = Convert.ToDateTime(drproduit["Date_install"]);
            numericUpDownMTBF.Value = Convert.ToInt32(drproduit["MTBF"]);
            textBoxMarque.Text = drproduit["Marque"].ToString();

            int idclient = Convert.ToInt32(drproduit["ID_CLIENT"]);
            List<ClassItem> theItems = comboBoxClient.Items.Cast<ClassItem>().ToList();
            var selectedClient = (from x in theItems where x.ID == idclient select x).Single();
            comboBoxClient.SelectedItem = selectedClient;

            int idcat = Convert.ToInt32(drproduit["ID_CAT"]);
            List<ClassItem> theItemscat = comboBoxCat.Items.Cast<ClassItem>().ToList();
            var selectedCat = (from x in theItemscat where x.ID == idcat select x).Single();
            comboBoxCat.SelectedItem = selectedCat;

            drproduit.Close();
            cn.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "insert into Produit " +
                "values (@nom, @prix, @dateinstall, @mtbf, @marque, @idclient, @idcat)";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nom", textBoxNom.Text);
            cmd.Parameters.AddWithValue("@prix", Convert.ToDecimal(textBoxPrix.Text));
            cmd.Parameters.AddWithValue("@dateinstall", dateTimePickerDI.Value);
            cmd.Parameters.AddWithValue("@mtbf", numericUpDownMTBF.Value);
            cmd.Parameters.AddWithValue("@marque", textBoxMarque.Text);
            cmd.Parameters.AddWithValue("@idclient", ((ClassItem)comboBoxClient.SelectedItem).ID);
            cmd.Parameters.AddWithValue("@idcat", ((ClassItem)comboBoxCat.SelectedItem).ID);

            cmd.ExecuteNonQuery();
            cn.Close();

            FillProduit();
            clearfields();
        }

        private void buttonModifier_Click(object sender, EventArgs e)
        {
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "update Produit set " +
                "Prix = @prix, Date_install = @dateinstall, MTBF = @mtbf, Marque = @marque, " +
                "ID_CLIENT = @idclient, ID_CAT = @idcat " +
                "where Nom = @nom";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nom", textBoxNom.Text);
            cmd.Parameters.AddWithValue("@prix", Convert.ToDecimal(textBoxPrix.Text));
            cmd.Parameters.AddWithValue("@dateinstall", dateTimePickerDI.Value);
            cmd.Parameters.AddWithValue("@mtbf", numericUpDownMTBF.Value);
            cmd.Parameters.AddWithValue("@marque", textBoxMarque.Text);
            cmd.Parameters.AddWithValue("@idclient", ((ClassItem)comboBoxClient.SelectedItem).ID);
            cmd.Parameters.AddWithValue("@idcat", ((ClassItem)comboBoxCat.SelectedItem).ID);
            cmd.ExecuteNonQuery();
            cn.Close();
            FillProduit();
            clearfields();
                
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (listBoxProduit.SelectedIndex == -1)
            {
                MessageBox.Show("Veullez sélectionner un produit");
                listBoxProduit.Focus();
                return;
            }
            if (MessageBox.Show("Voulez-vous vraiment supprimer ce produit ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            SqlConnection cn = new SqlConnection(connectionString);
            cn.Open();
            string sql = "delete from Produit where Nom = @nom";
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("@nom", textBoxNom.Text);
            cmd.ExecuteNonQuery();
            cn.Close();
            FillProduit();
            clearfields();



        }
    }
}
