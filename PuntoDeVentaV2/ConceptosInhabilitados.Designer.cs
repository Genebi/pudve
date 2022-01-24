namespace PuntoDeVentaV2
{
    partial class ConceptosInhabilitados
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
            this.DGVConceptosInhabilitados = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Habilitar = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGVConceptosInhabilitados)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(142, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Inhabilitados del Sistema";
            // 
            // DGVConceptosInhabilitados
            // 
            this.DGVConceptosInhabilitados.AllowUserToAddRows = false;
            this.DGVConceptosInhabilitados.AllowUserToDeleteRows = false;
            this.DGVConceptosInhabilitados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVConceptosInhabilitados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVConceptosInhabilitados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Concepto,
            this.Usuario,
            this.Habilitar});
            this.DGVConceptosInhabilitados.Location = new System.Drawing.Point(12, 58);
            this.DGVConceptosInhabilitados.Name = "DGVConceptosInhabilitados";
            this.DGVConceptosInhabilitados.ReadOnly = true;
            this.DGVConceptosInhabilitados.RowHeadersVisible = false;
            this.DGVConceptosInhabilitados.Size = new System.Drawing.Size(524, 191);
            this.DGVConceptosInhabilitados.TabIndex = 1;
            this.DGVConceptosInhabilitados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVConceptosInhabilitados_CellClick);
            this.DGVConceptosInhabilitados.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVConceptosInhabilitados_CellMouseEnter);
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
            // Habilitar
            // 
            this.Habilitar.HeaderText = "Habilitar";
            this.Habilitar.Name = "Habilitar";
            this.Habilitar.ReadOnly = true;
            this.Habilitar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Habilitar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ConceptosInhabilitados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 261);
            this.Controls.Add(this.DGVConceptosInhabilitados);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConceptosInhabilitados";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Conceptos Inhabilitados";
            this.Load += new System.EventHandler(this.ConceptosInhabilitados_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConceptosInhabilitados_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVConceptosInhabilitados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DGVConceptosInhabilitados;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usuario;
        private System.Windows.Forms.DataGridViewImageColumn Habilitar;
    }
}