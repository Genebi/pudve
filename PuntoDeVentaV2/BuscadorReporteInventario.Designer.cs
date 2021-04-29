namespace PuntoDeVentaV2
{
    partial class BuscadorReporteInventario
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
            this.DGVInventario = new System.Windows.Forms.DataGridView();
            this.numRevision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mostrar = new System.Windows.Forms.DataGridViewImageColumn();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGVInventario)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVInventario
            // 
            this.DGVInventario.AllowUserToAddRows = false;
            this.DGVInventario.AllowUserToDeleteRows = false;
            this.DGVInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVInventario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numRevision,
            this.usuario,
            this.fecha,
            this.mostrar});
            this.DGVInventario.Location = new System.Drawing.Point(11, 95);
            this.DGVInventario.Name = "DGVInventario";
            this.DGVInventario.RowHeadersVisible = false;
            this.DGVInventario.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGVInventario.Size = new System.Drawing.Size(896, 366);
            this.DGVInventario.TabIndex = 1;
            this.DGVInventario.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVInventario_CellClick);
            // 
            // numRevision
            // 
            this.numRevision.HeaderText = "No. Revision";
            this.numRevision.Name = "numRevision";
            this.numRevision.ReadOnly = true;
            this.numRevision.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.numRevision.Width = 70;
            // 
            // usuario
            // 
            this.usuario.HeaderText = "Usuario";
            this.usuario.Name = "usuario";
            this.usuario.ReadOnly = true;
            this.usuario.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.usuario.Width = 627;
            // 
            // fecha
            // 
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.fecha.Width = 137;
            // 
            // mostrar
            // 
            this.mostrar.HeaderText = "Mostrar";
            this.mostrar.Name = "mostrar";
            this.mostrar.ReadOnly = true;
            this.mostrar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.mostrar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.mostrar.Width = 60;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(376, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 24);
            this.label3.TabIndex = 13;
            this.label3.Text = "Reportes Inventario";
            // 
            // BuscadorReporteInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(919, 473);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DGVInventario);
            this.Name = "BuscadorReporteInventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reportes de Inventario";
            this.Load += new System.EventHandler(this.BuscadorReporteInventario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVInventario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVInventario;
        private System.Windows.Forms.DataGridViewTextBoxColumn numRevision;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewImageColumn mostrar;
        private System.Windows.Forms.Label label3;
    }
}