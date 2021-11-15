namespace PuntoDeVentaV2
{
    partial class QuitarEspecificacionDeConceptoDinamico
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
            this.DGVEspecificacionesActivas = new System.Windows.Forms.DataGridView();
            this.lblConceptoDinamico = new System.Windows.Forms.Label();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Inhabilitar = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGVEspecificacionesActivas)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVEspecificacionesActivas
            // 
            this.DGVEspecificacionesActivas.AllowUserToAddRows = false;
            this.DGVEspecificacionesActivas.AllowUserToDeleteRows = false;
            this.DGVEspecificacionesActivas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVEspecificacionesActivas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVEspecificacionesActivas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Concepto,
            this.Usuario,
            this.Inhabilitar});
            this.DGVEspecificacionesActivas.Location = new System.Drawing.Point(16, 49);
            this.DGVEspecificacionesActivas.Name = "DGVEspecificacionesActivas";
            this.DGVEspecificacionesActivas.ReadOnly = true;
            this.DGVEspecificacionesActivas.RowHeadersVisible = false;
            this.DGVEspecificacionesActivas.Size = new System.Drawing.Size(558, 221);
            this.DGVEspecificacionesActivas.TabIndex = 3;
            this.DGVEspecificacionesActivas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVEspecificacionesActivas_CellClick);
            this.DGVEspecificacionesActivas.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVEspecificacionesActivas_CellMouseEnter);
            // 
            // lblConceptoDinamico
            // 
            this.lblConceptoDinamico.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConceptoDinamico.Location = new System.Drawing.Point(11, 14);
            this.lblConceptoDinamico.Name = "lblConceptoDinamico";
            this.lblConceptoDinamico.Size = new System.Drawing.Size(568, 27);
            this.lblConceptoDinamico.TabIndex = 2;
            this.lblConceptoDinamico.Text = "Especificaciones existentes del concepto ";
            this.lblConceptoDinamico.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ID
            // 
            this.ID.FillWeight = 101.5228F;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Concepto
            // 
            this.Concepto.FillWeight = 98.47716F;
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
            // Inhabilitar
            // 
            this.Inhabilitar.HeaderText = "Inhabilitar";
            this.Inhabilitar.Name = "Inhabilitar";
            this.Inhabilitar.ReadOnly = true;
            // 
            // QuitarEspecificacionDeConceptoDinamico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 282);
            this.Controls.Add(this.DGVEspecificacionesActivas);
            this.Controls.Add(this.lblConceptoDinamico);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuitarEspecificacionDeConceptoDinamico";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Especificacion para quitar";
            this.Load += new System.EventHandler(this.QuitarEspecificacionDeConceptoDinamico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVEspecificacionesActivas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVEspecificacionesActivas;
        private System.Windows.Forms.Label lblConceptoDinamico;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Usuario;
        private System.Windows.Forms.DataGridViewImageColumn Inhabilitar;
    }
}