namespace WindowsFormsAppFixIT
{
    partial class FormHistorique
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewHisto = new System.Windows.Forms.ListView();
            this.iprix = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.icomment = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.idateinter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pnom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pinstall = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.plife = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cnom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ctel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.canom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewHisto
            // 
            this.listViewHisto.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idateinter,
            this.iprix,
            this.icomment,
            this.pnom,
            this.pinstall,
            this.plife,
            this.cnom,
            this.ctel,
            this.canom});
            this.listViewHisto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewHisto.FullRowSelect = true;
            this.listViewHisto.HideSelection = false;
            this.listViewHisto.Location = new System.Drawing.Point(0, 0);
            this.listViewHisto.Name = "listViewHisto";
            this.listViewHisto.Size = new System.Drawing.Size(1178, 499);
            this.listViewHisto.TabIndex = 0;
            this.listViewHisto.UseCompatibleStateImageBehavior = false;
            this.listViewHisto.View = System.Windows.Forms.View.Details;
            // 
            // iprix
            // 
            this.iprix.Text = "Prix";
            this.iprix.Width = 100;
            // 
            // icomment
            // 
            this.icomment.Text = "Commentaire";
            this.icomment.Width = 160;
            // 
            // idateinter
            // 
            this.idateinter.Text = "Date Intervention";
            this.idateinter.Width = 100;
            // 
            // pnom
            // 
            this.pnom.Text = "Nom du produit";
            this.pnom.Width = 120;
            // 
            // pinstall
            // 
            this.pinstall.Text = "Date d\'installation";
            this.pinstall.Width = 100;
            // 
            // plife
            // 
            this.plife.Text = "MTBF";
            // 
            // cnom
            // 
            this.cnom.Text = "Client";
            this.cnom.Width = 100;
            // 
            // ctel
            // 
            this.ctel.Text = "Tel";
            this.ctel.Width = 80;
            // 
            // canom
            // 
            this.canom.Text = "Catégorie";
            this.canom.Width = 100;
            // 
            // FormHistorique
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1178, 499);
            this.Controls.Add(this.listViewHisto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormHistorique";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Historique";
            this.Load += new System.EventHandler(this.FormHistorique_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewHisto;
        private System.Windows.Forms.ColumnHeader idateinter;
        private System.Windows.Forms.ColumnHeader iprix;
        private System.Windows.Forms.ColumnHeader icomment;
        private System.Windows.Forms.ColumnHeader pnom;
        private System.Windows.Forms.ColumnHeader pinstall;
        private System.Windows.Forms.ColumnHeader plife;
        private System.Windows.Forms.ColumnHeader cnom;
        private System.Windows.Forms.ColumnHeader ctel;
        private System.Windows.Forms.ColumnHeader canom;
    }
}