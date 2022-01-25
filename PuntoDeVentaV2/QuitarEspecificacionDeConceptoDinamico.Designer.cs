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
            this.IDHabiltado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConceptoHabiltado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsuarioHabiltado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InhabilitarHabiltado = new System.Windows.Forms.DataGridViewImageColumn();
            this.lblConceptoDinamico = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DGVEspecificacionesInactivas = new System.Windows.Forms.DataGridView();
            this.IDInhabiltado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConceptoInhabiltado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UsuarioInhabiltado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HabilitarInhabiltado = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGVEspecificacionesActivas)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVEspecificacionesInactivas)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVEspecificacionesActivas
            // 
            this.DGVEspecificacionesActivas.AllowUserToAddRows = false;
            this.DGVEspecificacionesActivas.AllowUserToDeleteRows = false;
            this.DGVEspecificacionesActivas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVEspecificacionesActivas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVEspecificacionesActivas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDHabiltado,
            this.ConceptoHabiltado,
            this.UsuarioHabiltado,
            this.InhabilitarHabiltado});
            this.DGVEspecificacionesActivas.Location = new System.Drawing.Point(6, 18);
            this.DGVEspecificacionesActivas.Name = "DGVEspecificacionesActivas";
            this.DGVEspecificacionesActivas.ReadOnly = true;
            this.DGVEspecificacionesActivas.RowHeadersVisible = false;
            this.DGVEspecificacionesActivas.Size = new System.Drawing.Size(544, 171);
            this.DGVEspecificacionesActivas.TabIndex = 3;
            this.DGVEspecificacionesActivas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVEspecificacionesActivas_CellClick);
            this.DGVEspecificacionesActivas.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVEspecificacionesActivas_CellMouseEnter);
            // 
            // IDHabiltado
            // 
            this.IDHabiltado.FillWeight = 101.5228F;
            this.IDHabiltado.HeaderText = "ID";
            this.IDHabiltado.Name = "IDHabiltado";
            this.IDHabiltado.ReadOnly = true;
            this.IDHabiltado.Visible = false;
            // 
            // ConceptoHabiltado
            // 
            this.ConceptoHabiltado.FillWeight = 98.47716F;
            this.ConceptoHabiltado.HeaderText = "Concepto";
            this.ConceptoHabiltado.Name = "ConceptoHabiltado";
            this.ConceptoHabiltado.ReadOnly = true;
            // 
            // UsuarioHabiltado
            // 
            this.UsuarioHabiltado.HeaderText = "Usuario";
            this.UsuarioHabiltado.Name = "UsuarioHabiltado";
            this.UsuarioHabiltado.ReadOnly = true;
            this.UsuarioHabiltado.Visible = false;
            // 
            // InhabilitarHabiltado
            // 
            this.InhabilitarHabiltado.HeaderText = "Inhabilitar";
            this.InhabilitarHabiltado.Name = "InhabilitarHabiltado";
            this.InhabilitarHabiltado.ReadOnly = true;
            // 
            // lblConceptoDinamico
            // 
            this.lblConceptoDinamico.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblConceptoDinamico.Location = new System.Drawing.Point(11, 14);
            this.lblConceptoDinamico.Name = "lblConceptoDinamico";
            this.lblConceptoDinamico.Size = new System.Drawing.Size(568, 27);
            this.lblConceptoDinamico.TabIndex = 2;
            this.lblConceptoDinamico.Text = "Especificaciones existentes del concepto ";
            this.lblConceptoDinamico.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(15, 49);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(564, 221);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DGVEspecificacionesActivas);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(556, 195);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Habilitados";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DGVEspecificacionesInactivas);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(556, 195);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Inhabilitados";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DGVEspecificacionesInactivas
            // 
            this.DGVEspecificacionesInactivas.AllowUserToAddRows = false;
            this.DGVEspecificacionesInactivas.AllowUserToDeleteRows = false;
            this.DGVEspecificacionesInactivas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVEspecificacionesInactivas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVEspecificacionesInactivas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDInhabiltado,
            this.ConceptoInhabiltado,
            this.UsuarioInhabiltado,
            this.HabilitarInhabiltado});
            this.DGVEspecificacionesInactivas.Location = new System.Drawing.Point(6, 12);
            this.DGVEspecificacionesInactivas.Name = "DGVEspecificacionesInactivas";
            this.DGVEspecificacionesInactivas.ReadOnly = true;
            this.DGVEspecificacionesInactivas.RowHeadersVisible = false;
            this.DGVEspecificacionesInactivas.Size = new System.Drawing.Size(544, 171);
            this.DGVEspecificacionesInactivas.TabIndex = 4;
            this.DGVEspecificacionesInactivas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVEspecificacionesInactivas_CellClick);
            this.DGVEspecificacionesInactivas.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVEspecificacionesInactivas_CellMouseEnter);
            // 
            // IDInhabiltado
            // 
            this.IDInhabiltado.FillWeight = 101.5228F;
            this.IDInhabiltado.HeaderText = "ID";
            this.IDInhabiltado.Name = "IDInhabiltado";
            this.IDInhabiltado.ReadOnly = true;
            this.IDInhabiltado.Visible = false;
            // 
            // ConceptoInhabiltado
            // 
            this.ConceptoInhabiltado.FillWeight = 98.47716F;
            this.ConceptoInhabiltado.HeaderText = "Concepto";
            this.ConceptoInhabiltado.Name = "ConceptoInhabiltado";
            this.ConceptoInhabiltado.ReadOnly = true;
            // 
            // UsuarioInhabiltado
            // 
            this.UsuarioInhabiltado.HeaderText = "Usuario";
            this.UsuarioInhabiltado.Name = "UsuarioInhabiltado";
            this.UsuarioInhabiltado.ReadOnly = true;
            this.UsuarioInhabiltado.Visible = false;
            // 
            // HabilitarInhabiltado
            // 
            this.HabilitarInhabiltado.HeaderText = "Habilitar";
            this.HabilitarInhabiltado.Name = "HabilitarInhabiltado";
            this.HabilitarInhabiltado.ReadOnly = true;
            // 
            // QuitarEspecificacionDeConceptoDinamico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 282);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblConceptoDinamico);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "QuitarEspecificacionDeConceptoDinamico";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Especificacion para quitar";
            this.Load += new System.EventHandler(this.QuitarEspecificacionDeConceptoDinamico_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QuitarEspecificacionDeConceptoDinamico_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVEspecificacionesActivas)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVEspecificacionesInactivas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVEspecificacionesActivas;
        private System.Windows.Forms.Label lblConceptoDinamico;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView DGVEspecificacionesInactivas;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDHabiltado;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConceptoHabiltado;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsuarioHabiltado;
        private System.Windows.Forms.DataGridViewImageColumn InhabilitarHabiltado;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDInhabiltado;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConceptoInhabiltado;
        private System.Windows.Forms.DataGridViewTextBoxColumn UsuarioInhabiltado;
        private System.Windows.Forms.DataGridViewImageColumn HabilitarInhabiltado;
    }
}