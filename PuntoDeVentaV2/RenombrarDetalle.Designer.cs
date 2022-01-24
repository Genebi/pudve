namespace PuntoDeVentaV2
{
    partial class RenombrarDetalle
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
            this.label1 = new System.Windows.Forms.Label();
            this.DGVConceptosRenombrar = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Renombrar = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGVConceptosRenombrar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(212, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Renombrar";
            // 
            // DGVConceptosRenombrar
            // 
            this.DGVConceptosRenombrar.AllowUserToAddRows = false;
            this.DGVConceptosRenombrar.AllowUserToDeleteRows = false;
            this.DGVConceptosRenombrar.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVConceptosRenombrar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVConceptosRenombrar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Concepto,
            this.Usuario,
            this.Renombrar});
            this.DGVConceptosRenombrar.Location = new System.Drawing.Point(12, 66);
            this.DGVConceptosRenombrar.Name = "DGVConceptosRenombrar";
            this.DGVConceptosRenombrar.ReadOnly = true;
            this.DGVConceptosRenombrar.RowHeadersVisible = false;
            this.DGVConceptosRenombrar.Size = new System.Drawing.Size(524, 183);
            this.DGVConceptosRenombrar.TabIndex = 1;
            this.DGVConceptosRenombrar.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVConceptosRenombrar_CellClick);
            this.DGVConceptosRenombrar.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVConceptosRenombrar_CellMouseEnter);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Concepto
            // 
            this.Concepto.HeaderText = "Concepto";
            this.Concepto.Name = "Concepto";
            this.Concepto.ReadOnly = true;
            // 
            // Usuario
            // 
            this.Usuario.HeaderText = "Usuario";
            this.Usuario.Name = "Usuario";
            this.Usuario.ReadOnly = true;
            this.Usuario.Visible = false;
            // 
            // Renombrar
            // 
            this.Renombrar.HeaderText = "Renombrar";
            this.Renombrar.Name = "Renombrar";
            this.Renombrar.ReadOnly = true;
            this.Renombrar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Renombrar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // RenombrarDetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 261);
            this.Controls.Add(this.DGVConceptosRenombrar);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RenombrarDetalle";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Renombrar Detalle";
            this.Load += new System.EventHandler(this.RenombrarDetalle_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RenombrarDetalle_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVConceptosRenombrar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DGVConceptosRenombrar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usuario;
        private System.Windows.Forms.DataGridViewImageColumn Renombrar;
    }
}