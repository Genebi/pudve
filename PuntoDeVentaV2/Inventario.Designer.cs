namespace PuntoDeVentaV2
{
    partial class Inventario
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
            this.btnRevisar = new System.Windows.Forms.Button();
            this.btnActualizarXML = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.btnConceptosReporte = new System.Windows.Forms.Button();
            this.gBSeleccionActualizarInventario = new System.Windows.Forms.GroupBox();
            this.rbDisminuirProducto = new System.Windows.Forms.RadioButton();
            this.rbAumentarProducto = new System.Windows.Forms.RadioButton();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.bntTerminar = new System.Windows.Forms.Button();
            this.listaProductos = new System.Windows.Forms.ListBox();
            this.tituloBusqueda = new System.Windows.Forms.Label();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.DGVInventario = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiferenciaUnidades = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NuevoStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comentarios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDTabla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelBotones.SuspendLayout();
            this.gBSeleccionActualizarInventario.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVInventario)).BeginInit();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(388, 18);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(148, 25);
            this.tituloSeccion.TabIndex = 6;
            this.tituloSeccion.Text = "INVENTARIO";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnRevisar
            // 
            this.btnRevisar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRevisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnRevisar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRevisar.FlatAppearance.BorderSize = 0;
            this.btnRevisar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnRevisar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnRevisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevisar.ForeColor = System.Drawing.Color.White;
            this.btnRevisar.Location = new System.Drawing.Point(3, 7);
            this.btnRevisar.Name = "btnRevisar";
            this.btnRevisar.Size = new System.Drawing.Size(230, 30);
            this.btnRevisar.TabIndex = 101;
            this.btnRevisar.Text = "Revisar Inventario";
            this.btnRevisar.UseVisualStyleBackColor = false;
            this.btnRevisar.Click += new System.EventHandler(this.btnRevisar_Click);
            this.btnRevisar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnRevisar_KeyDown);
            // 
            // btnActualizarXML
            // 
            this.btnActualizarXML.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnActualizarXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnActualizarXML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizarXML.FlatAppearance.BorderSize = 0;
            this.btnActualizarXML.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnActualizarXML.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnActualizarXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarXML.ForeColor = System.Drawing.Color.White;
            this.btnActualizarXML.Location = new System.Drawing.Point(612, 7);
            this.btnActualizarXML.Name = "btnActualizarXML";
            this.btnActualizarXML.Size = new System.Drawing.Size(230, 30);
            this.btnActualizarXML.TabIndex = 102;
            this.btnActualizarXML.Text = "Actualizar Inventario XML";
            this.btnActualizarXML.UseVisualStyleBackColor = false;
            this.btnActualizarXML.Click += new System.EventHandler(this.btnActualizarXML_Click);
            this.btnActualizarXML.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnActualizarXML_KeyDown);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatAppearance.BorderSize = 0;
            this.btnActualizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnActualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.Location = new System.Drawing.Point(314, 7);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(230, 30);
            this.btnActualizar.TabIndex = 103;
            this.btnActualizar.Text = "Actualizar Inventario";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            this.btnActualizar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnActualizar_KeyDown);
            // 
            // panelBotones
            // 
            this.panelBotones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBotones.Controls.Add(this.btnConceptosReporte);
            this.panelBotones.Controls.Add(this.gBSeleccionActualizarInventario);
            this.panelBotones.Controls.Add(this.btnRevisar);
            this.panelBotones.Controls.Add(this.btnActualizarXML);
            this.panelBotones.Controls.Add(this.btnActualizar);
            this.panelBotones.Location = new System.Drawing.Point(12, 67);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(845, 83);
            this.panelBotones.TabIndex = 104;
            // 
            // btnConceptosReporte
            // 
            this.btnConceptosReporte.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnConceptosReporte.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnConceptosReporte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConceptosReporte.FlatAppearance.BorderSize = 0;
            this.btnConceptosReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConceptosReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConceptosReporte.ForeColor = System.Drawing.Color.White;
            this.btnConceptosReporte.Location = new System.Drawing.Point(83, 43);
            this.btnConceptosReporte.Name = "btnConceptosReporte";
            this.btnConceptosReporte.Size = new System.Drawing.Size(227, 31);
            this.btnConceptosReporte.TabIndex = 105;
            this.btnConceptosReporte.Text = "Conceptos del Reporte";
            this.btnConceptosReporte.UseVisualStyleBackColor = false;
            this.btnConceptosReporte.Visible = false;
            this.btnConceptosReporte.Click += new System.EventHandler(this.btnConceptosReporte_Click);
            // 
            // gBSeleccionActualizarInventario
            // 
            this.gBSeleccionActualizarInventario.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gBSeleccionActualizarInventario.Controls.Add(this.rbDisminuirProducto);
            this.gBSeleccionActualizarInventario.Controls.Add(this.rbAumentarProducto);
            this.gBSeleccionActualizarInventario.Location = new System.Drawing.Point(316, 38);
            this.gBSeleccionActualizarInventario.Name = "gBSeleccionActualizarInventario";
            this.gBSeleccionActualizarInventario.Size = new System.Drawing.Size(227, 36);
            this.gBSeleccionActualizarInventario.TabIndex = 104;
            this.gBSeleccionActualizarInventario.TabStop = false;
            this.gBSeleccionActualizarInventario.Visible = false;
            // 
            // rbDisminuirProducto
            // 
            this.rbDisminuirProducto.AutoSize = true;
            this.rbDisminuirProducto.Location = new System.Drawing.Point(135, 12);
            this.rbDisminuirProducto.Name = "rbDisminuirProducto";
            this.rbDisminuirProducto.Size = new System.Drawing.Size(67, 17);
            this.rbDisminuirProducto.TabIndex = 1;
            this.rbDisminuirProducto.TabStop = true;
            this.rbDisminuirProducto.Text = "Disminuir";
            this.rbDisminuirProducto.UseVisualStyleBackColor = true;
            this.rbDisminuirProducto.CheckedChanged += new System.EventHandler(this.rbDisminuirProducto_CheckedChanged);
            // 
            // rbAumentarProducto
            // 
            this.rbAumentarProducto.AutoSize = true;
            this.rbAumentarProducto.Checked = true;
            this.rbAumentarProducto.Location = new System.Drawing.Point(27, 12);
            this.rbAumentarProducto.Name = "rbAumentarProducto";
            this.rbAumentarProducto.Size = new System.Drawing.Size(70, 17);
            this.rbAumentarProducto.TabIndex = 0;
            this.rbAumentarProducto.TabStop = true;
            this.rbAumentarProducto.Text = "Aumentar";
            this.rbAumentarProducto.UseVisualStyleBackColor = true;
            this.rbAumentarProducto.CheckedChanged += new System.EventHandler(this.rbAumentarProducto_CheckedChanged);
            // 
            // panelContenedor
            // 
            this.panelContenedor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContenedor.Controls.Add(this.btnBuscar);
            this.panelContenedor.Controls.Add(this.bntTerminar);
            this.panelContenedor.Controls.Add(this.listaProductos);
            this.panelContenedor.Controls.Add(this.tituloBusqueda);
            this.panelContenedor.Controls.Add(this.txtBusqueda);
            this.panelContenedor.Controls.Add(this.DGVInventario);
            this.panelContenedor.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelContenedor.Location = new System.Drawing.Point(12, 154);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(845, 395);
            this.panelContenedor.TabIndex = 105;
            this.panelContenedor.Visible = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.BackColor = System.Drawing.Color.Green;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(612, 26);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(105, 23);
            this.btnBuscar.TabIndex = 105;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.btnBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnBuscar_KeyDown);
            // 
            // bntTerminar
            // 
            this.bntTerminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bntTerminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.bntTerminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntTerminar.FlatAppearance.BorderSize = 0;
            this.bntTerminar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.bntTerminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.bntTerminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntTerminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntTerminar.ForeColor = System.Drawing.Color.White;
            this.bntTerminar.Location = new System.Drawing.Point(695, 360);
            this.bntTerminar.Name = "bntTerminar";
            this.bntTerminar.Size = new System.Drawing.Size(150, 30);
            this.bntTerminar.TabIndex = 104;
            this.bntTerminar.Text = "Terminar";
            this.bntTerminar.UseVisualStyleBackColor = false;
            this.bntTerminar.Click += new System.EventHandler(this.bntTerminar_Click);
            this.bntTerminar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bntTerminar_KeyDown);
            // 
            // listaProductos
            // 
            this.listaProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listaProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaProductos.FormattingEnabled = true;
            this.listaProductos.ItemHeight = 16;
            this.listaProductos.Location = new System.Drawing.Point(3, 49);
            this.listaProductos.Name = "listaProductos";
            this.listaProductos.Size = new System.Drawing.Size(595, 132);
            this.listaProductos.TabIndex = 12;
            this.listaProductos.Visible = false;
            this.listaProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listaProductos_KeyDown);
            this.listaProductos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listaProductos_MouseDoubleClick);
            // 
            // tituloBusqueda
            // 
            this.tituloBusqueda.AutoSize = true;
            this.tituloBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloBusqueda.Location = new System.Drawing.Point(-4, 3);
            this.tituloBusqueda.Name = "tituloBusqueda";
            this.tituloBusqueda.Size = new System.Drawing.Size(232, 18);
            this.tituloBusqueda.TabIndex = 10;
            this.tituloBusqueda.Text = "Búsqueda avanzada de productos";
            this.tituloBusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusqueda.Location = new System.Drawing.Point(0, 26);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(595, 22);
            this.txtBusqueda.TabIndex = 11;
            this.txtBusqueda.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBusqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBusqueda_KeyDown);
            this.txtBusqueda.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBusqueda_KeyUp);
            // 
            // DGVInventario
            // 
            this.DGVInventario.AllowUserToAddRows = false;
            this.DGVInventario.AllowUserToDeleteRows = false;
            this.DGVInventario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVInventario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Nombre,
            this.Stock,
            this.DiferenciaUnidades,
            this.NuevoStock,
            this.Precio,
            this.Clave,
            this.Codigo,
            this.Fecha,
            this.Comentarios,
            this.IDTabla});
            this.DGVInventario.Location = new System.Drawing.Point(0, 65);
            this.DGVInventario.Name = "DGVInventario";
            this.DGVInventario.ReadOnly = true;
            this.DGVInventario.RowHeadersVisible = false;
            this.DGVInventario.Size = new System.Drawing.Size(845, 289);
            this.DGVInventario.TabIndex = 9;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Nombre
            // 
            this.Nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // Stock
            // 
            this.Stock.HeaderText = "Stock Anterior";
            this.Stock.Name = "Stock";
            this.Stock.ReadOnly = true;
            this.Stock.Width = 70;
            // 
            // DiferenciaUnidades
            // 
            this.DiferenciaUnidades.HeaderText = "Diferencia de Unidades";
            this.DiferenciaUnidades.Name = "DiferenciaUnidades";
            this.DiferenciaUnidades.ReadOnly = true;
            this.DiferenciaUnidades.Width = 90;
            // 
            // NuevoStock
            // 
            this.NuevoStock.HeaderText = "Stock Actual";
            this.NuevoStock.Name = "NuevoStock";
            this.NuevoStock.ReadOnly = true;
            this.NuevoStock.Width = 70;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            this.Precio.Width = 70;
            // 
            // Clave
            // 
            this.Clave.HeaderText = "Clave";
            this.Clave.Name = "Clave";
            this.Clave.ReadOnly = true;
            // 
            // Codigo
            // 
            this.Codigo.HeaderText = "Código";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 95;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 95;
            // 
            // Comentarios
            // 
            this.Comentarios.HeaderText = "Comentarios";
            this.Comentarios.Name = "Comentarios";
            this.Comentarios.ReadOnly = true;
            this.Comentarios.Visible = false;
            // 
            // IDTabla
            // 
            this.IDTabla.HeaderText = "IDTabla";
            this.IDTabla.Name = "IDTabla";
            this.IDTabla.ReadOnly = true;
            this.IDTabla.Visible = false;
            // 
            // Inventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 561);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.tituloSeccion);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Inventario";
            this.ShowIcon = false;
            this.Text = "PUDVE - Inventario";
            this.Load += new System.EventHandler(this.Inventario_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Inventario_KeyDown);
            this.panelBotones.ResumeLayout(false);
            this.gBSeleccionActualizarInventario.ResumeLayout(false);
            this.gBSeleccionActualizarInventario.PerformLayout();
            this.panelContenedor.ResumeLayout(false);
            this.panelContenedor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVInventario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.Button btnRevisar;
        private System.Windows.Forms.Button btnActualizarXML;
        private System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.DataGridView DGVInventario;
        private System.Windows.Forms.Label tituloBusqueda;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.ListBox listaProductos;
        private System.Windows.Forms.Button bntTerminar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox gBSeleccionActualizarInventario;
        private System.Windows.Forms.RadioButton rbDisminuirProducto;
        private System.Windows.Forms.RadioButton rbAumentarProducto;
        private System.Windows.Forms.Button btnConceptosReporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiferenciaUnidades;
        private System.Windows.Forms.DataGridViewTextBoxColumn NuevoStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comentarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDTabla;
    }
}