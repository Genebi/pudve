namespace PuntoDeVentaV2
{
    partial class ListadoVentasGuardadas
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
            this.DGVListaVentasGuardadas = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Mostrar = new System.Windows.Forms.DataGridViewImageColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewImageColumn();
            this.IDCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_buscar_por = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGVListaVentasGuardadas)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVListaVentasGuardadas
            // 
            this.DGVListaVentasGuardadas.AllowUserToAddRows = false;
            this.DGVListaVentasGuardadas.AllowUserToDeleteRows = false;
            this.DGVListaVentasGuardadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVListaVentasGuardadas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Folio,
            this.Cliente,
            this.Importe,
            this.Fecha,
            this.Mostrar,
            this.Eliminar,
            this.IDCliente});
            this.DGVListaVentasGuardadas.Location = new System.Drawing.Point(12, 71);
            this.DGVListaVentasGuardadas.Name = "DGVListaVentasGuardadas";
            this.DGVListaVentasGuardadas.ReadOnly = true;
            this.DGVListaVentasGuardadas.RowHeadersVisible = false;
            this.DGVListaVentasGuardadas.Size = new System.Drawing.Size(510, 178);
            this.DGVListaVentasGuardadas.TabIndex = 0;
            this.DGVListaVentasGuardadas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVListaVentasGuardadas_CellClick);
            this.DGVListaVentasGuardadas.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVListaVentasGuardadas_CellMouseEnter);
            this.DGVListaVentasGuardadas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGVListaVentasGuardadas_KeyDown);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 50;
            // 
            // Folio
            // 
            this.Folio.HeaderText = "Folio";
            this.Folio.Name = "Folio";
            this.Folio.ReadOnly = true;
            // 
            // Cliente
            // 
            this.Cliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            // 
            // Importe
            // 
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 110;
            // 
            // Mostrar
            // 
            this.Mostrar.HeaderText = "Mostrar";
            this.Mostrar.Name = "Mostrar";
            this.Mostrar.ReadOnly = true;
            this.Mostrar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Mostrar.Width = 50;
            // 
            // Eliminar
            // 
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.ReadOnly = true;
            this.Eliminar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Eliminar.Width = 50;
            // 
            // IDCliente
            // 
            this.IDCliente.HeaderText = "ID Cliente";
            this.IDCliente.Name = "IDCliente";
            this.IDCliente.ReadOnly = true;
            this.IDCliente.Visible = false;
            // 
            // txt_buscar_por
            // 
            this.txt_buscar_por.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_buscar_por.Location = new System.Drawing.Point(12, 34);
            this.txt_buscar_por.Name = "txt_buscar_por";
            this.txt_buscar_por.Size = new System.Drawing.Size(510, 22);
            this.txt_buscar_por.TabIndex = 6;
            this.txt_buscar_por.Text = "Buscar por cliente o folio";
            this.txt_buscar_por.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_buscar_por_KeyDown);
            // 
            // ListadoVentasGuardadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 261);
            this.Controls.Add(this.txt_buscar_por);
            this.Controls.Add(this.DGVListaVentasGuardadas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ListadoVentasGuardadas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ventas guardadas";
            this.Load += new System.EventHandler(this.ListadoVentasGuardadas_Load);
            this.Shown += new System.EventHandler(this.ListadoVentasGuardadas_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListadoVentasGuardadas_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVListaVentasGuardadas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVListaVentasGuardadas;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewImageColumn Mostrar;
        private System.Windows.Forms.DataGridViewImageColumn Eliminar;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDCliente;
        private System.Windows.Forms.TextBox txt_buscar_por;
    }
}