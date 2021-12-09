namespace PuntoDeVentaV2
{
    partial class Reportes
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
            this.primerSeparador = new System.Windows.Forms.Label();
            this.btnHistorialDineroAgregado = new System.Windows.Forms.Button();
            this.Panel = new System.Windows.Forms.Panel();
            this.DGVInventario = new System.Windows.Forms.DataGridView();
            this.numRevision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mostrar = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnMenosVendidos = new PuntoDeVentaV2.BotonRedondo();
            this.btnClientes = new PuntoDeVentaV2.BotonRedondo();
            this.btnReporteVentas = new PuntoDeVentaV2.BotonRedondo();
            this.btnReporteInventario = new PuntoDeVentaV2.BotonRedondo();
            this.btnCaja = new PuntoDeVentaV2.BotonRedondo();
            this.btnHistorialPrecios = new PuntoDeVentaV2.BotonRedondo();
            this.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVInventario)).BeginInit();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(103, 19);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(900, 25);
            this.tituloSeccion.TabIndex = 24;
            this.tituloSeccion.Text = "REPORTES";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // primerSeparador
            // 
            this.primerSeparador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.primerSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.primerSeparador.Location = new System.Drawing.Point(42, 61);
            this.primerSeparador.Name = "primerSeparador";
            this.primerSeparador.Size = new System.Drawing.Size(1023, 2);
            this.primerSeparador.TabIndex = 23;
            this.primerSeparador.Text = "REPORTES";
            this.primerSeparador.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnHistorialDineroAgregado
            // 
            this.btnHistorialDineroAgregado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnHistorialDineroAgregado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorialDineroAgregado.FlatAppearance.BorderSize = 0;
            this.btnHistorialDineroAgregado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialDineroAgregado.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnHistorialDineroAgregado.ForeColor = System.Drawing.Color.White;
            this.btnHistorialDineroAgregado.Location = new System.Drawing.Point(42, 14);
            this.btnHistorialDineroAgregado.Name = "btnHistorialDineroAgregado";
            this.btnHistorialDineroAgregado.Size = new System.Drawing.Size(190, 30);
            this.btnHistorialDineroAgregado.TabIndex = 1;
            this.btnHistorialDineroAgregado.Text = "Historial Dinero Agreado";
            this.btnHistorialDineroAgregado.UseVisualStyleBackColor = false;
            this.btnHistorialDineroAgregado.Visible = false;
            this.btnHistorialDineroAgregado.Click += new System.EventHandler(this.btnHistorialDineroAgregado_Click);
            this.btnHistorialDineroAgregado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnHistorialDineroAgregado_KeyDown);
            // 
            // Panel
            // 
            this.Panel.Controls.Add(this.DGVInventario);
            this.Panel.Location = new System.Drawing.Point(42, 202);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(900, 447);
            this.Panel.TabIndex = 106;
            this.Panel.Visible = false;
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
            this.DGVInventario.Location = new System.Drawing.Point(0, 0);
            this.DGVInventario.Name = "DGVInventario";
            this.DGVInventario.RowHeadersVisible = false;
            this.DGVInventario.Size = new System.Drawing.Size(897, 444);
            this.DGVInventario.TabIndex = 0;
            this.DGVInventario.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVInventario_CellClick);
            this.DGVInventario.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVInventario_CellMouseEnter);
            this.DGVInventario.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVInventario_CellMouseLeave);
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
            // btnMenosVendidos
            // 
            this.btnMenosVendidos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnMenosVendidos.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnMenosVendidos.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnMenosVendidos.BorderRadius = 20;
            this.btnMenosVendidos.BorderSize = 0;
            this.btnMenosVendidos.FlatAppearance.BorderSize = 0;
            this.btnMenosVendidos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenosVendidos.ForeColor = System.Drawing.Color.White;
            this.btnMenosVendidos.Image = global::PuntoDeVentaV2.Properties.Resources.decrease;
            this.btnMenosVendidos.Location = new System.Drawing.Point(915, 74);
            this.btnMenosVendidos.Name = "btnMenosVendidos";
            this.btnMenosVendidos.Size = new System.Drawing.Size(150, 68);
            this.btnMenosVendidos.TabIndex = 115;
            this.btnMenosVendidos.Text = "Reporte Más/Menos Vendidos";
            this.btnMenosVendidos.TextColor = System.Drawing.Color.White;
            this.btnMenosVendidos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMenosVendidos.UseVisualStyleBackColor = false;
            this.btnMenosVendidos.Click += new System.EventHandler(this.btnMenosVendidos_Click);
            // 
            // btnClientes
            // 
            this.btnClientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnClientes.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnClientes.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnClientes.BorderRadius = 20;
            this.btnClientes.BorderSize = 0;
            this.btnClientes.FlatAppearance.BorderSize = 0;
            this.btnClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClientes.ForeColor = System.Drawing.Color.White;
            this.btnClientes.Image = global::PuntoDeVentaV2.Properties.Resources.report_user;
            this.btnClientes.Location = new System.Drawing.Point(739, 74);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(150, 68);
            this.btnClientes.TabIndex = 114;
            this.btnClientes.Text = "Reporte Clientes";
            this.btnClientes.TextColor = System.Drawing.Color.White;
            this.btnClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClientes.UseVisualStyleBackColor = false;
            this.btnClientes.Click += new System.EventHandler(this.botonRedondo5_Click);
            // 
            // btnReporteVentas
            // 
            this.btnReporteVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnReporteVentas.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnReporteVentas.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnReporteVentas.BorderRadius = 20;
            this.btnReporteVentas.BorderSize = 0;
            this.btnReporteVentas.FlatAppearance.BorderSize = 0;
            this.btnReporteVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporteVentas.ForeColor = System.Drawing.Color.White;
            this.btnReporteVentas.Image = global::PuntoDeVentaV2.Properties.Resources.report;
            this.btnReporteVentas.Location = new System.Drawing.Point(559, 74);
            this.btnReporteVentas.Name = "btnReporteVentas";
            this.btnReporteVentas.Size = new System.Drawing.Size(150, 68);
            this.btnReporteVentas.TabIndex = 113;
            this.btnReporteVentas.Text = "Reporte Ventas";
            this.btnReporteVentas.TextColor = System.Drawing.Color.White;
            this.btnReporteVentas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReporteVentas.UseVisualStyleBackColor = false;
            this.btnReporteVentas.Click += new System.EventHandler(this.botonRedondo4_Click);
            // 
            // btnReporteInventario
            // 
            this.btnReporteInventario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnReporteInventario.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnReporteInventario.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnReporteInventario.BorderRadius = 20;
            this.btnReporteInventario.BorderSize = 0;
            this.btnReporteInventario.FlatAppearance.BorderSize = 0;
            this.btnReporteInventario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporteInventario.ForeColor = System.Drawing.Color.White;
            this.btnReporteInventario.Image = global::PuntoDeVentaV2.Properties.Resources.report_2_;
            this.btnReporteInventario.Location = new System.Drawing.Point(385, 74);
            this.btnReporteInventario.Name = "btnReporteInventario";
            this.btnReporteInventario.Size = new System.Drawing.Size(150, 68);
            this.btnReporteInventario.TabIndex = 112;
            this.btnReporteInventario.Text = "Reportes de Inventario";
            this.btnReporteInventario.TextColor = System.Drawing.Color.White;
            this.btnReporteInventario.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnReporteInventario.UseVisualStyleBackColor = false;
            this.btnReporteInventario.Click += new System.EventHandler(this.botonRedondo3_Click);
            // 
            // btnCaja
            // 
            this.btnCaja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnCaja.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnCaja.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnCaja.BorderRadius = 20;
            this.btnCaja.BorderSize = 0;
            this.btnCaja.FlatAppearance.BorderSize = 0;
            this.btnCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaja.ForeColor = System.Drawing.Color.White;
            this.btnCaja.Image = global::PuntoDeVentaV2.Properties.Resources.cash_register;
            this.btnCaja.Location = new System.Drawing.Point(209, 74);
            this.btnCaja.Name = "btnCaja";
            this.btnCaja.Size = new System.Drawing.Size(150, 68);
            this.btnCaja.TabIndex = 111;
            this.btnCaja.Text = "Caja";
            this.btnCaja.TextColor = System.Drawing.Color.White;
            this.btnCaja.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnCaja.UseVisualStyleBackColor = false;
            this.btnCaja.Click += new System.EventHandler(this.botonRedondo2_Click);
            // 
            // btnHistorialPrecios
            // 
            this.btnHistorialPrecios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnHistorialPrecios.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnHistorialPrecios.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnHistorialPrecios.BorderRadius = 20;
            this.btnHistorialPrecios.BorderSize = 0;
            this.btnHistorialPrecios.FlatAppearance.BorderSize = 0;
            this.btnHistorialPrecios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialPrecios.ForeColor = System.Drawing.Color.White;
            this.btnHistorialPrecios.Image = global::PuntoDeVentaV2.Properties.Resources.research;
            this.btnHistorialPrecios.Location = new System.Drawing.Point(42, 74);
            this.btnHistorialPrecios.Name = "btnHistorialPrecios";
            this.btnHistorialPrecios.Size = new System.Drawing.Size(150, 68);
            this.btnHistorialPrecios.TabIndex = 110;
            this.btnHistorialPrecios.Text = "Historial de Precios";
            this.btnHistorialPrecios.TextColor = System.Drawing.Color.White;
            this.btnHistorialPrecios.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnHistorialPrecios.UseVisualStyleBackColor = false;
            this.btnHistorialPrecios.Click += new System.EventHandler(this.botonRedondo1_Click);
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 661);
            this.Controls.Add(this.btnMenosVendidos);
            this.Controls.Add(this.btnClientes);
            this.Controls.Add(this.btnReporteVentas);
            this.Controls.Add(this.btnReporteInventario);
            this.Controls.Add(this.btnCaja);
            this.Controls.Add(this.btnHistorialPrecios);
            this.Controls.Add(this.btnHistorialDineroAgregado);
            this.Controls.Add(this.Panel);
            this.Controls.Add(this.tituloSeccion);
            this.Controls.Add(this.primerSeparador);
            this.Name = "Reportes";
            this.Text = "Reportes";
            this.Load += new System.EventHandler(this.Reportes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Reportes_KeyDown);
            this.Panel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVInventario)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.Label primerSeparador;
        private System.Windows.Forms.Button btnHistorialDineroAgregado;
        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.DataGridView DGVInventario;
        private System.Windows.Forms.DataGridViewTextBoxColumn numRevision;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewImageColumn mostrar;
        private BotonRedondo btnHistorialPrecios;
        private BotonRedondo btnCaja;
        private BotonRedondo btnReporteInventario;
        private BotonRedondo btnReporteVentas;
        private BotonRedondo btnClientes;
        private BotonRedondo btnMenosVendidos;
    }
}