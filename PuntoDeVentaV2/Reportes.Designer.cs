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
            this.btnHistorialPrecios = new System.Windows.Forms.Button();
            this.btnHistorialDineroAgregado = new System.Windows.Forms.Button();
            this.btnReporteInventario = new System.Windows.Forms.Button();
            this.Panel = new System.Windows.Forms.Panel();
            this.DGVInventario = new System.Windows.Forms.DataGridView();
            this.numRevision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mostrar = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnCaja = new System.Windows.Forms.Button();
            this.btnReporteVentas = new System.Windows.Forms.Button();
            this.Panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVInventario)).BeginInit();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(42, 19);
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
            this.primerSeparador.Size = new System.Drawing.Size(900, 2);
            this.primerSeparador.TabIndex = 23;
            this.primerSeparador.Text = "REPORTES";
            this.primerSeparador.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnHistorialPrecios
            // 
            this.btnHistorialPrecios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnHistorialPrecios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorialPrecios.FlatAppearance.BorderSize = 0;
            this.btnHistorialPrecios.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnHistorialPrecios.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnHistorialPrecios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialPrecios.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorialPrecios.ForeColor = System.Drawing.Color.White;
            this.btnHistorialPrecios.Location = new System.Drawing.Point(42, 91);
            this.btnHistorialPrecios.Name = "btnHistorialPrecios";
            this.btnHistorialPrecios.Size = new System.Drawing.Size(190, 30);
            this.btnHistorialPrecios.TabIndex = 102;
            this.btnHistorialPrecios.Text = "Historial de Precios";
            this.btnHistorialPrecios.UseVisualStyleBackColor = false;
            this.btnHistorialPrecios.Click += new System.EventHandler(this.btnHistorialPrecios_Click);
            this.btnHistorialPrecios.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnHistorialPrecios_KeyDown);
            // 
            // btnHistorialDineroAgregado
            // 
            this.btnHistorialDineroAgregado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnHistorialDineroAgregado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorialDineroAgregado.FlatAppearance.BorderSize = 0;
            this.btnHistorialDineroAgregado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialDineroAgregado.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnHistorialDineroAgregado.ForeColor = System.Drawing.Color.White;
            this.btnHistorialDineroAgregado.Location = new System.Drawing.Point(304, 136);
            this.btnHistorialDineroAgregado.Name = "btnHistorialDineroAgregado";
            this.btnHistorialDineroAgregado.Size = new System.Drawing.Size(190, 30);
            this.btnHistorialDineroAgregado.TabIndex = 1;
            this.btnHistorialDineroAgregado.Text = "Historial Dinero Agreado";
            this.btnHistorialDineroAgregado.UseVisualStyleBackColor = false;
            this.btnHistorialDineroAgregado.Visible = false;
            this.btnHistorialDineroAgregado.Click += new System.EventHandler(this.btnHistorialDineroAgregado_Click);
            this.btnHistorialDineroAgregado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnHistorialDineroAgregado_KeyDown);
            // 
            // btnReporteInventario
            // 
            this.btnReporteInventario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnReporteInventario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReporteInventario.FlatAppearance.BorderSize = 0;
            this.btnReporteInventario.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnReporteInventario.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnReporteInventario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporteInventario.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporteInventario.ForeColor = System.Drawing.Color.White;
            this.btnReporteInventario.Location = new System.Drawing.Point(473, 91);
            this.btnReporteInventario.Name = "btnReporteInventario";
            this.btnReporteInventario.Size = new System.Drawing.Size(190, 30);
            this.btnReporteInventario.TabIndex = 105;
            this.btnReporteInventario.Text = "Reportes de Inventario";
            this.btnReporteInventario.UseVisualStyleBackColor = false;
            this.btnReporteInventario.Click += new System.EventHandler(this.btnReporteInventario_Click);
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
            // btnCaja
            // 
            this.btnCaja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCaja.FlatAppearance.BorderSize = 0;
            this.btnCaja.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnCaja.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaja.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaja.ForeColor = System.Drawing.Color.White;
            this.btnCaja.Location = new System.Drawing.Point(258, 91);
            this.btnCaja.Name = "btnCaja";
            this.btnCaja.Size = new System.Drawing.Size(190, 30);
            this.btnCaja.TabIndex = 107;
            this.btnCaja.Text = "Caja";
            this.btnCaja.UseVisualStyleBackColor = false;
            this.btnCaja.Click += new System.EventHandler(this.btnCaja_Click);
            // 
            // btnReporteVentas
            // 
            this.btnReporteVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnReporteVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReporteVentas.FlatAppearance.BorderSize = 0;
            this.btnReporteVentas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnReporteVentas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnReporteVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReporteVentas.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReporteVentas.ForeColor = System.Drawing.Color.White;
            this.btnReporteVentas.Location = new System.Drawing.Point(689, 91);
            this.btnReporteVentas.Name = "btnReporteVentas";
            this.btnReporteVentas.Size = new System.Drawing.Size(190, 30);
            this.btnReporteVentas.TabIndex = 108;
            this.btnReporteVentas.Text = "Reporte Ventas";
            this.btnReporteVentas.UseVisualStyleBackColor = false;
            this.btnReporteVentas.Click += new System.EventHandler(this.btnReporteVentas_Click);
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.btnReporteVentas);
            this.Controls.Add(this.btnHistorialDineroAgregado);
            this.Controls.Add(this.btnCaja);
            this.Controls.Add(this.Panel);
            this.Controls.Add(this.btnReporteInventario);
            this.Controls.Add(this.btnHistorialPrecios);
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
        private System.Windows.Forms.Button btnHistorialPrecios;
        private System.Windows.Forms.Button btnHistorialDineroAgregado;
        private System.Windows.Forms.Button btnReporteInventario;
        private System.Windows.Forms.Panel Panel;
        private System.Windows.Forms.DataGridView DGVInventario;
        private System.Windows.Forms.DataGridViewTextBoxColumn numRevision;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewImageColumn mostrar;
        private System.Windows.Forms.Button btnCaja;
        private System.Windows.Forms.Button btnReporteVentas;
    }
}