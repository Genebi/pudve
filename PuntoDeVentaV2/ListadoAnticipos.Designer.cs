namespace PuntoDeVentaV2
{
    partial class ListadoAnticipos
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
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.DGVListaAnticipos = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Aplicar = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtbusqueda = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGVListaAnticipos)).BeginInit();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(201, 37);
            this.tituloSeccion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(398, 38);
            this.tituloSeccion.TabIndex = 6;
            this.tituloSeccion.Text = "ANTICIPOS POR APLICAR";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DGVListaAnticipos
            // 
            this.DGVListaAnticipos.AllowUserToAddRows = false;
            this.DGVListaAnticipos.AllowUserToDeleteRows = false;
            this.DGVListaAnticipos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVListaAnticipos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Concepto,
            this.Importe,
            this.Cliente,
            this.Fecha,
            this.Aplicar});
            this.DGVListaAnticipos.Location = new System.Drawing.Point(18, 126);
            this.DGVListaAnticipos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DGVListaAnticipos.Name = "DGVListaAnticipos";
            this.DGVListaAnticipos.ReadOnly = true;
            this.DGVListaAnticipos.RowHeadersVisible = false;
            this.DGVListaAnticipos.RowHeadersWidth = 62;
            this.DGVListaAnticipos.Size = new System.Drawing.Size(765, 338);
            this.DGVListaAnticipos.TabIndex = 7;
            this.DGVListaAnticipos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVListaAnticipos_CellClick);
            this.DGVListaAnticipos.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVListaAnticipos_CellMouseEnter);
            this.DGVListaAnticipos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGVListaAnticipos_KeyDown);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 8;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 50;
            // 
            // Concepto
            // 
            this.Concepto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Concepto.HeaderText = "Concepto";
            this.Concepto.MinimumWidth = 8;
            this.Concepto.Name = "Concepto";
            this.Concepto.ReadOnly = true;
            // 
            // Importe
            // 
            this.Importe.HeaderText = "Importe";
            this.Importe.MinimumWidth = 8;
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            this.Importe.Width = 50;
            // 
            // Cliente
            // 
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.MinimumWidth = 8;
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            this.Cliente.Width = 105;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.MinimumWidth = 8;
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 110;
            // 
            // Aplicar
            // 
            this.Aplicar.HeaderText = "";
            this.Aplicar.MinimumWidth = 8;
            this.Aplicar.Name = "Aplicar";
            this.Aplicar.ReadOnly = true;
            this.Aplicar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Aplicar.Width = 50;
            // 
            // txtbusqueda
            // 
            this.txtbusqueda.Location = new System.Drawing.Point(204, 86);
            this.txtbusqueda.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtbusqueda.Name = "txtbusqueda";
            this.txtbusqueda.Size = new System.Drawing.Size(385, 26);
            this.txtbusqueda.TabIndex = 8;
            this.txtbusqueda.TextChanged += new System.EventHandler(this.txtbusqueda_TextChanged);
            this.txtbusqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbusqueda_KeyDown);
            // 
            // ListadoAnticipos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(801, 483);
            this.Controls.Add(this.txtbusqueda);
            this.Controls.Add(this.DGVListaAnticipos);
            this.Controls.Add(this.tituloSeccion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "ListadoAnticipos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Lista de Anticipos";
            this.Load += new System.EventHandler(this.ListadoAnticipos_Load);
            this.Shown += new System.EventHandler(this.ListadoAnticipos_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListadoAnticipos_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVListaAnticipos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.DataGridView DGVListaAnticipos;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewImageColumn Aplicar;
        private System.Windows.Forms.TextBox txtbusqueda;
    }
}