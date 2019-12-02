namespace PuntoDeVentaV2
{
    partial class Ventas
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
            this.components = new System.ComponentModel.Container();
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.tituloBoton = new System.Windows.Forms.ToolTip(this.components);
            this.btnProductoRapido = new System.Windows.Forms.Button();
            this.btnEliminarUltimo = new System.Windows.Forms.Button();
            this.btnEliminarTodos = new System.Windows.Forms.Button();
            this.btnUltimoTicket = new System.Windows.Forms.Button();
            this.btnPresupuesto = new System.Windows.Forms.Button();
            this.txtBuscadorProducto = new System.Windows.Forms.TextBox();
            this.DGVentas = new System.Windows.Forms.DataGridView();
            this.IDProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioOriginal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescuentoTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoPS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgregarMultiple = new System.Windows.Forms.DataGridViewImageColumn();
            this.AgregarIndividual = new System.Windows.Forms.DataGridViewImageColumn();
            this.RestarIndividual = new System.Windows.Forms.DataGridViewImageColumn();
            this.EliminarIndividual = new System.Windows.Forms.DataGridViewImageColumn();
            this.NumeroColumna = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImagenProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancelarVenta = new System.Windows.Forms.Button();
            this.btnGuardarVenta = new System.Windows.Forms.Button();
            this.btnAnticipos = new System.Windows.Forms.Button();
            this.btnAbrirCaja = new System.Windows.Forms.Button();
            this.btnVentasGuardadas = new System.Windows.Forms.Button();
            this.listaProductos = new System.Windows.Forms.ListBox();
            this.lbNumeroArticulos = new System.Windows.Forms.Label();
            this.lbSubtotal = new System.Windows.Forms.Label();
            this.lbIVA = new System.Windows.Forms.Label();
            this.lbAnticipo = new System.Windows.Forms.Label();
            this.lbDescuento = new System.Windows.Forms.Label();
            this.lbTotal = new System.Windows.Forms.Label();
            this.cNumeroArticulos = new System.Windows.Forms.Label();
            this.cSubtotal = new System.Windows.Forms.Label();
            this.cIVA = new System.Windows.Forms.Label();
            this.cAnticipo = new System.Windows.Forms.Label();
            this.cDescuento = new System.Windows.Forms.Label();
            this.cTotal = new System.Windows.Forms.Label();
            this.lbIVA8 = new System.Windows.Forms.Label();
            this.cIVA8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PBImagen = new System.Windows.Forms.PictureBox();
            this.lbPS = new System.Windows.Forms.Label();
            this.nudCantidadPS = new System.Windows.Forms.NumericUpDown();
            this.lbCantidad = new System.Windows.Forms.Label();
            this.btnTerminarVenta = new System.Windows.Forms.Button();
            this.txtDescuentoGeneral = new System.Windows.Forms.TextBox();
            this.timerBusqueda = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DGVentas)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBImagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadPS)).BeginInit();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(537, 22);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(157, 25);
            this.tituloSeccion.TabIndex = 4;
            this.tituloSeccion.Text = "NUEVA VENTA";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnProductoRapido
            // 
            this.btnProductoRapido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProductoRapido.Location = new System.Drawing.Point(516, 80);
            this.btnProductoRapido.Name = "btnProductoRapido";
            this.btnProductoRapido.Size = new System.Drawing.Size(40, 28);
            this.btnProductoRapido.TabIndex = 10;
            this.tituloBoton.SetToolTip(this.btnProductoRapido, "Agregar producto o servicio rápido");
            this.btnProductoRapido.UseVisualStyleBackColor = true;
            // 
            // btnEliminarUltimo
            // 
            this.btnEliminarUltimo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarUltimo.Location = new System.Drawing.Point(562, 80);
            this.btnEliminarUltimo.Name = "btnEliminarUltimo";
            this.btnEliminarUltimo.Size = new System.Drawing.Size(40, 28);
            this.btnEliminarUltimo.TabIndex = 7;
            this.tituloBoton.SetToolTip(this.btnEliminarUltimo, "Eliminar último agregado");
            this.btnEliminarUltimo.UseVisualStyleBackColor = true;
            this.btnEliminarUltimo.Click += new System.EventHandler(this.btnEliminarUltimo_Click);
            // 
            // btnEliminarTodos
            // 
            this.btnEliminarTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarTodos.Location = new System.Drawing.Point(608, 80);
            this.btnEliminarTodos.Name = "btnEliminarTodos";
            this.btnEliminarTodos.Size = new System.Drawing.Size(40, 28);
            this.btnEliminarTodos.TabIndex = 8;
            this.tituloBoton.SetToolTip(this.btnEliminarTodos, "Eliminar todos los agregados");
            this.btnEliminarTodos.UseVisualStyleBackColor = true;
            this.btnEliminarTodos.Click += new System.EventHandler(this.btnEliminarTodos_Click);
            // 
            // btnUltimoTicket
            // 
            this.btnUltimoTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUltimoTicket.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUltimoTicket.Location = new System.Drawing.Point(1143, 31);
            this.btnUltimoTicket.Name = "btnUltimoTicket";
            this.btnUltimoTicket.Size = new System.Drawing.Size(37, 23);
            this.btnUltimoTicket.TabIndex = 18;
            this.tituloBoton.SetToolTip(this.btnUltimoTicket, "Imprimir último ticket");
            this.btnUltimoTicket.UseVisualStyleBackColor = true;
            this.btnUltimoTicket.Click += new System.EventHandler(this.btnUltimoTicket_Click);
            // 
            // btnPresupuesto
            // 
            this.btnPresupuesto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPresupuesto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPresupuesto.Location = new System.Drawing.Point(1181, 31);
            this.btnPresupuesto.Name = "btnPresupuesto";
            this.btnPresupuesto.Size = new System.Drawing.Size(37, 23);
            this.btnPresupuesto.TabIndex = 19;
            this.tituloBoton.SetToolTip(this.btnPresupuesto, "Guardar como presupuesto");
            this.btnPresupuesto.UseVisualStyleBackColor = true;
            this.btnPresupuesto.Click += new System.EventHandler(this.btnPresupuesto_Click);
            // 
            // txtBuscadorProducto
            // 
            this.txtBuscadorProducto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscadorProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscadorProducto.Location = new System.Drawing.Point(94, 31);
            this.txtBuscadorProducto.Name = "txtBuscadorProducto";
            this.txtBuscadorProducto.Size = new System.Drawing.Size(555, 23);
            this.txtBuscadorProducto.TabIndex = 5;
            this.txtBuscadorProducto.Text = "BUSCAR PRODUCTO O SERVICIO...";
            this.txtBuscadorProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscadorProducto_KeyDown);
            this.txtBuscadorProducto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBuscadorProducto_KeyUp);
            // 
            // DGVentas
            // 
            this.DGVentas.AllowUserToAddRows = false;
            this.DGVentas.AllowUserToDeleteRows = false;
            this.DGVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVentas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDProducto,
            this.Stock,
            this.PrecioOriginal,
            this.DescuentoTipo,
            this.TipoPS,
            this.Cantidad,
            this.Precio,
            this.Descripcion,
            this.Descuento,
            this.Importe,
            this.AgregarMultiple,
            this.AgregarIndividual,
            this.RestarIndividual,
            this.EliminarIndividual,
            this.NumeroColumna,
            this.ImagenProducto});
            this.DGVentas.Location = new System.Drawing.Point(3, 114);
            this.DGVentas.Name = "DGVentas";
            this.DGVentas.ReadOnly = true;
            this.DGVentas.RowHeadersVisible = false;
            this.DGVentas.Size = new System.Drawing.Size(646, 203);
            this.DGVentas.TabIndex = 6;
            this.DGVentas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVentas_CellClick);
            this.DGVentas.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVentas_CellMouseEnter);
            // 
            // IDProducto
            // 
            this.IDProducto.HeaderText = "_ID";
            this.IDProducto.Name = "IDProducto";
            this.IDProducto.ReadOnly = true;
            this.IDProducto.Visible = false;
            // 
            // Stock
            // 
            this.Stock.HeaderText = "_Stock";
            this.Stock.Name = "Stock";
            this.Stock.ReadOnly = true;
            this.Stock.Visible = false;
            // 
            // PrecioOriginal
            // 
            this.PrecioOriginal.HeaderText = "_PrecioOriginal";
            this.PrecioOriginal.Name = "PrecioOriginal";
            this.PrecioOriginal.ReadOnly = true;
            this.PrecioOriginal.Visible = false;
            // 
            // DescuentoTipo
            // 
            this.DescuentoTipo.HeaderText = "_TipoDescuento";
            this.DescuentoTipo.Name = "DescuentoTipo";
            this.DescuentoTipo.ReadOnly = true;
            this.DescuentoTipo.Visible = false;
            // 
            // TipoPS
            // 
            this.TipoPS.HeaderText = "_TIpoPS";
            this.TipoPS.Name = "TipoPS";
            this.TipoPS.ReadOnly = true;
            this.TipoPS.Visible = false;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            this.Cantidad.Width = 55;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            this.Precio.Width = 50;
            // 
            // Descripcion
            // 
            this.Descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Descripcion.HeaderText = "Descripción";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            // 
            // Descuento
            // 
            this.Descuento.HeaderText = "Descuento";
            this.Descuento.Name = "Descuento";
            this.Descuento.ReadOnly = true;
            this.Descuento.Width = 65;
            // 
            // Importe
            // 
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            this.Importe.Width = 50;
            // 
            // AgregarMultiple
            // 
            this.AgregarMultiple.HeaderText = "";
            this.AgregarMultiple.Name = "AgregarMultiple";
            this.AgregarMultiple.ReadOnly = true;
            this.AgregarMultiple.Width = 25;
            // 
            // AgregarIndividual
            // 
            this.AgregarIndividual.HeaderText = "";
            this.AgregarIndividual.Name = "AgregarIndividual";
            this.AgregarIndividual.ReadOnly = true;
            this.AgregarIndividual.Width = 25;
            // 
            // RestarIndividual
            // 
            this.RestarIndividual.HeaderText = "";
            this.RestarIndividual.Name = "RestarIndividual";
            this.RestarIndividual.ReadOnly = true;
            this.RestarIndividual.Width = 25;
            // 
            // EliminarIndividual
            // 
            this.EliminarIndividual.HeaderText = "";
            this.EliminarIndividual.Name = "EliminarIndividual";
            this.EliminarIndividual.ReadOnly = true;
            this.EliminarIndividual.Width = 25;
            // 
            // NumeroColumna
            // 
            this.NumeroColumna.HeaderText = "NumeroColumna";
            this.NumeroColumna.Name = "NumeroColumna";
            this.NumeroColumna.ReadOnly = true;
            this.NumeroColumna.Visible = false;
            // 
            // ImagenProducto
            // 
            this.ImagenProducto.HeaderText = "ImagenProducto";
            this.ImagenProducto.Name = "ImagenProducto";
            this.ImagenProducto.ReadOnly = true;
            this.ImagenProducto.Visible = false;
            // 
            // btnCancelarVenta
            // 
            this.btnCancelarVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarVenta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarVenta.Location = new System.Drawing.Point(655, 31);
            this.btnCancelarVenta.Name = "btnCancelarVenta";
            this.btnCancelarVenta.Size = new System.Drawing.Size(75, 23);
            this.btnCancelarVenta.TabIndex = 13;
            this.btnCancelarVenta.Text = "Cancelar";
            this.btnCancelarVenta.UseVisualStyleBackColor = true;
            this.btnCancelarVenta.Click += new System.EventHandler(this.btnCancelarVenta_Click);
            // 
            // btnGuardarVenta
            // 
            this.btnGuardarVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardarVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarVenta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarVenta.Location = new System.Drawing.Point(736, 31);
            this.btnGuardarVenta.Name = "btnGuardarVenta";
            this.btnGuardarVenta.Size = new System.Drawing.Size(75, 23);
            this.btnGuardarVenta.TabIndex = 14;
            this.btnGuardarVenta.Text = "Guardar";
            this.btnGuardarVenta.UseVisualStyleBackColor = true;
            this.btnGuardarVenta.Click += new System.EventHandler(this.btnGuardarVenta_Click);
            // 
            // btnAnticipos
            // 
            this.btnAnticipos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnticipos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnticipos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnticipos.Location = new System.Drawing.Point(817, 31);
            this.btnAnticipos.Name = "btnAnticipos";
            this.btnAnticipos.Size = new System.Drawing.Size(75, 23);
            this.btnAnticipos.TabIndex = 15;
            this.btnAnticipos.Text = "Anticipos";
            this.btnAnticipos.UseVisualStyleBackColor = true;
            this.btnAnticipos.Click += new System.EventHandler(this.btnAnticipos_Click);
            // 
            // btnAbrirCaja
            // 
            this.btnAbrirCaja.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbrirCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirCaja.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrirCaja.Location = new System.Drawing.Point(898, 31);
            this.btnAbrirCaja.Name = "btnAbrirCaja";
            this.btnAbrirCaja.Size = new System.Drawing.Size(75, 23);
            this.btnAbrirCaja.TabIndex = 16;
            this.btnAbrirCaja.Text = "Abrir Caja";
            this.btnAbrirCaja.UseVisualStyleBackColor = true;
            this.btnAbrirCaja.Click += new System.EventHandler(this.btnAbrirCaja_Click);
            // 
            // btnVentasGuardadas
            // 
            this.btnVentasGuardadas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVentasGuardadas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVentasGuardadas.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVentasGuardadas.Location = new System.Drawing.Point(980, 31);
            this.btnVentasGuardadas.Name = "btnVentasGuardadas";
            this.btnVentasGuardadas.Size = new System.Drawing.Size(157, 23);
            this.btnVentasGuardadas.TabIndex = 17;
            this.btnVentasGuardadas.Text = "Ventas guardadas";
            this.btnVentasGuardadas.UseVisualStyleBackColor = true;
            this.btnVentasGuardadas.Click += new System.EventHandler(this.btnVentasGuardadas_Click);
            // 
            // listaProductos
            // 
            this.listaProductos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaProductos.FormattingEnabled = true;
            this.listaProductos.ItemHeight = 17;
            this.listaProductos.Location = new System.Drawing.Point(94, 53);
            this.listaProductos.Name = "listaProductos";
            this.listaProductos.Size = new System.Drawing.Size(555, 55);
            this.listaProductos.TabIndex = 9;
            this.listaProductos.Visible = false;
            this.listaProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listaProductos_KeyDown);
            this.listaProductos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listaProductos_MouseDoubleClick);
            this.listaProductos.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.listaProductos_PreviewKeyDown);
            // 
            // lbNumeroArticulos
            // 
            this.lbNumeroArticulos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNumeroArticulos.AutoSize = true;
            this.lbNumeroArticulos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumeroArticulos.Location = new System.Drawing.Point(740, 114);
            this.lbNumeroArticulos.Name = "lbNumeroArticulos";
            this.lbNumeroArticulos.Size = new System.Drawing.Size(143, 17);
            this.lbNumeroArticulos.TabIndex = 20;
            this.lbNumeroArticulos.Text = "Número de artículos:";
            // 
            // lbSubtotal
            // 
            this.lbSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSubtotal.AutoSize = true;
            this.lbSubtotal.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSubtotal.Location = new System.Drawing.Point(740, 136);
            this.lbSubtotal.Name = "lbSubtotal";
            this.lbSubtotal.Size = new System.Drawing.Size(66, 17);
            this.lbSubtotal.TabIndex = 21;
            this.lbSubtotal.Text = "Subtotal:";
            // 
            // lbIVA
            // 
            this.lbIVA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbIVA.AutoSize = true;
            this.lbIVA.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIVA.Location = new System.Drawing.Point(740, 158);
            this.lbIVA.Name = "lbIVA";
            this.lbIVA.Size = new System.Drawing.Size(61, 17);
            this.lbIVA.TabIndex = 22;
            this.lbIVA.Text = "IVA 16%:";
            // 
            // lbAnticipo
            // 
            this.lbAnticipo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAnticipo.AutoSize = true;
            this.lbAnticipo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAnticipo.Location = new System.Drawing.Point(740, 202);
            this.lbAnticipo.Name = "lbAnticipo";
            this.lbAnticipo.Size = new System.Drawing.Size(66, 17);
            this.lbAnticipo.TabIndex = 23;
            this.lbAnticipo.Text = "Anticipo:";
            // 
            // lbDescuento
            // 
            this.lbDescuento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDescuento.AutoSize = true;
            this.lbDescuento.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescuento.Location = new System.Drawing.Point(740, 224);
            this.lbDescuento.Name = "lbDescuento";
            this.lbDescuento.Size = new System.Drawing.Size(81, 17);
            this.lbDescuento.TabIndex = 24;
            this.lbDescuento.Text = "Descuento:";
            // 
            // lbTotal
            // 
            this.lbTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTotal.AutoSize = true;
            this.lbTotal.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.Location = new System.Drawing.Point(740, 246);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(43, 17);
            this.lbTotal.TabIndex = 25;
            this.lbTotal.Text = "Total:";
            // 
            // cNumeroArticulos
            // 
            this.cNumeroArticulos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cNumeroArticulos.AutoSize = true;
            this.cNumeroArticulos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cNumeroArticulos.Location = new System.Drawing.Point(902, 116);
            this.cNumeroArticulos.Name = "cNumeroArticulos";
            this.cNumeroArticulos.Size = new System.Drawing.Size(15, 17);
            this.cNumeroArticulos.TabIndex = 26;
            this.cNumeroArticulos.Text = "0";
            // 
            // cSubtotal
            // 
            this.cSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cSubtotal.AutoSize = true;
            this.cSubtotal.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cSubtotal.Location = new System.Drawing.Point(902, 136);
            this.cSubtotal.Name = "cSubtotal";
            this.cSubtotal.Size = new System.Drawing.Size(33, 17);
            this.cSubtotal.TabIndex = 27;
            this.cSubtotal.Text = "0.00";
            // 
            // cIVA
            // 
            this.cIVA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cIVA.AutoSize = true;
            this.cIVA.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cIVA.Location = new System.Drawing.Point(902, 158);
            this.cIVA.Name = "cIVA";
            this.cIVA.Size = new System.Drawing.Size(33, 17);
            this.cIVA.TabIndex = 28;
            this.cIVA.Text = "0.00";
            // 
            // cAnticipo
            // 
            this.cAnticipo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cAnticipo.AutoSize = true;
            this.cAnticipo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cAnticipo.Location = new System.Drawing.Point(902, 202);
            this.cAnticipo.Name = "cAnticipo";
            this.cAnticipo.Size = new System.Drawing.Size(33, 17);
            this.cAnticipo.TabIndex = 29;
            this.cAnticipo.Text = "0.00";
            // 
            // cDescuento
            // 
            this.cDescuento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cDescuento.AutoSize = true;
            this.cDescuento.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cDescuento.Location = new System.Drawing.Point(902, 224);
            this.cDescuento.Name = "cDescuento";
            this.cDescuento.Size = new System.Drawing.Size(33, 17);
            this.cDescuento.TabIndex = 30;
            this.cDescuento.Text = "0.00";
            // 
            // cTotal
            // 
            this.cTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cTotal.AutoSize = true;
            this.cTotal.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cTotal.Location = new System.Drawing.Point(902, 246);
            this.cTotal.Name = "cTotal";
            this.cTotal.Size = new System.Drawing.Size(33, 17);
            this.cTotal.TabIndex = 31;
            this.cTotal.Text = "0.00";
            // 
            // lbIVA8
            // 
            this.lbIVA8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbIVA8.AutoSize = true;
            this.lbIVA8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIVA8.Location = new System.Drawing.Point(740, 180);
            this.lbIVA8.Name = "lbIVA8";
            this.lbIVA8.Size = new System.Drawing.Size(54, 17);
            this.lbIVA8.TabIndex = 32;
            this.lbIVA8.Text = "IVA 8%:";
            // 
            // cIVA8
            // 
            this.cIVA8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cIVA8.AutoSize = true;
            this.cIVA8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cIVA8.Location = new System.Drawing.Point(902, 180);
            this.cIVA8.Name = "cIVA8";
            this.cIVA8.Size = new System.Drawing.Size(33, 17);
            this.cIVA8.TabIndex = 33;
            this.cIVA8.Text = "0.00";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.listaProductos);
            this.panel1.Controls.Add(this.PBImagen);
            this.panel1.Controls.Add(this.lbPS);
            this.panel1.Controls.Add(this.nudCantidadPS);
            this.panel1.Controls.Add(this.lbCantidad);
            this.panel1.Controls.Add(this.btnTerminarVenta);
            this.panel1.Controls.Add(this.txtDescuentoGeneral);
            this.panel1.Controls.Add(this.DGVentas);
            this.panel1.Controls.Add(this.cIVA8);
            this.panel1.Controls.Add(this.txtBuscadorProducto);
            this.panel1.Controls.Add(this.lbIVA8);
            this.panel1.Controls.Add(this.btnEliminarUltimo);
            this.panel1.Controls.Add(this.cTotal);
            this.panel1.Controls.Add(this.btnEliminarTodos);
            this.panel1.Controls.Add(this.cDescuento);
            this.panel1.Controls.Add(this.btnProductoRapido);
            this.panel1.Controls.Add(this.cAnticipo);
            this.panel1.Controls.Add(this.cIVA);
            this.panel1.Controls.Add(this.cSubtotal);
            this.panel1.Controls.Add(this.cNumeroArticulos);
            this.panel1.Controls.Add(this.btnCancelarVenta);
            this.panel1.Controls.Add(this.lbTotal);
            this.panel1.Controls.Add(this.btnGuardarVenta);
            this.panel1.Controls.Add(this.lbDescuento);
            this.panel1.Controls.Add(this.btnAnticipos);
            this.panel1.Controls.Add(this.lbAnticipo);
            this.panel1.Controls.Add(this.btnAbrirCaja);
            this.panel1.Controls.Add(this.lbIVA);
            this.panel1.Controls.Add(this.btnVentasGuardadas);
            this.panel1.Controls.Add(this.lbSubtotal);
            this.panel1.Controls.Add(this.btnUltimoTicket);
            this.panel1.Controls.Add(this.lbNumeroArticulos);
            this.panel1.Controls.Add(this.btnPresupuesto);
            this.panel1.Location = new System.Drawing.Point(5, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1221, 423);
            this.panel1.TabIndex = 34;
            // 
            // PBImagen
            // 
            this.PBImagen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PBImagen.Location = new System.Drawing.Point(998, 99);
            this.PBImagen.Name = "PBImagen";
            this.PBImagen.Size = new System.Drawing.Size(220, 220);
            this.PBImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBImagen.TabIndex = 42;
            this.PBImagen.TabStop = false;
            // 
            // lbPS
            // 
            this.lbPS.AutoSize = true;
            this.lbPS.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPS.Location = new System.Drawing.Point(91, 11);
            this.lbPS.Name = "lbPS";
            this.lbPS.Size = new System.Drawing.Size(121, 17);
            this.lbPS.TabIndex = 41;
            this.lbPS.Text = "Producto / servicio";
            // 
            // nudCantidadPS
            // 
            this.nudCantidadPS.DecimalPlaces = 2;
            this.nudCantidadPS.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudCantidadPS.Location = new System.Drawing.Point(3, 31);
            this.nudCantidadPS.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudCantidadPS.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudCantidadPS.Name = "nudCantidadPS";
            this.nudCantidadPS.Size = new System.Drawing.Size(71, 22);
            this.nudCantidadPS.TabIndex = 40;
            this.nudCantidadPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudCantidadPS.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lbCantidad
            // 
            this.lbCantidad.AutoSize = true;
            this.lbCantidad.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCantidad.Location = new System.Drawing.Point(3, 11);
            this.lbCantidad.Name = "lbCantidad";
            this.lbCantidad.Size = new System.Drawing.Size(64, 17);
            this.lbCantidad.TabIndex = 39;
            this.lbCantidad.Text = "Cantidad";
            // 
            // btnTerminarVenta
            // 
            this.btnTerminarVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTerminarVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTerminarVenta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTerminarVenta.Location = new System.Drawing.Point(881, 297);
            this.btnTerminarVenta.Name = "btnTerminarVenta";
            this.btnTerminarVenta.Size = new System.Drawing.Size(75, 23);
            this.btnTerminarVenta.TabIndex = 37;
            this.btnTerminarVenta.Text = "Terminar";
            this.btnTerminarVenta.UseVisualStyleBackColor = true;
            this.btnTerminarVenta.Click += new System.EventHandler(this.btnTerminarVenta_Click);
            // 
            // txtDescuentoGeneral
            // 
            this.txtDescuentoGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescuentoGeneral.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescuentoGeneral.Location = new System.Drawing.Point(743, 297);
            this.txtDescuentoGeneral.Name = "txtDescuentoGeneral";
            this.txtDescuentoGeneral.Size = new System.Drawing.Size(100, 22);
            this.txtDescuentoGeneral.TabIndex = 35;
            this.txtDescuentoGeneral.Text = "% descuento";
            this.txtDescuentoGeneral.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDescuentoGeneral_KeyUp);
            // 
            // timerBusqueda
            // 
            this.timerBusqueda.Interval = 1000;
            this.timerBusqueda.Tick += new System.EventHandler(this.timerBusqueda_Tick);
            // 
            // Ventas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 596);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tituloSeccion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1027, 597);
            this.Name = "Ventas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventas_FormClosing);
            this.Load += new System.EventHandler(this.Ventas_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventas_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVentas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PBImagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadPS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.ToolTip tituloBoton;
        private System.Windows.Forms.Button btnProductoRapido;
        private System.Windows.Forms.TextBox txtBuscadorProducto;
        private System.Windows.Forms.DataGridView DGVentas;
        private System.Windows.Forms.Button btnEliminarUltimo;
        private System.Windows.Forms.Button btnEliminarTodos;
        private System.Windows.Forms.Button btnCancelarVenta;
        private System.Windows.Forms.Button btnGuardarVenta;
        private System.Windows.Forms.Button btnAnticipos;
        private System.Windows.Forms.Button btnAbrirCaja;
        private System.Windows.Forms.Button btnVentasGuardadas;
        private System.Windows.Forms.Button btnUltimoTicket;
        private System.Windows.Forms.Button btnPresupuesto;
        private System.Windows.Forms.ListBox listaProductos;
        private System.Windows.Forms.Label lbNumeroArticulos;
        private System.Windows.Forms.Label lbSubtotal;
        private System.Windows.Forms.Label lbIVA;
        private System.Windows.Forms.Label lbAnticipo;
        private System.Windows.Forms.Label lbDescuento;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.Label cNumeroArticulos;
        private System.Windows.Forms.Label cSubtotal;
        private System.Windows.Forms.Label cIVA;
        private System.Windows.Forms.Label cAnticipo;
        private System.Windows.Forms.Label cDescuento;
        private System.Windows.Forms.Label cTotal;
        private System.Windows.Forms.Label lbIVA8;
        private System.Windows.Forms.Label cIVA8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtDescuentoGeneral;
        private System.Windows.Forms.Button btnTerminarVenta;
        private System.Windows.Forms.Label lbCantidad;
        private System.Windows.Forms.NumericUpDown nudCantidadPS;
        private System.Windows.Forms.Label lbPS;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioOriginal;
        private System.Windows.Forms.DataGridViewTextBoxColumn DescuentoTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoPS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Importe;
        private System.Windows.Forms.DataGridViewImageColumn AgregarMultiple;
        private System.Windows.Forms.DataGridViewImageColumn AgregarIndividual;
        private System.Windows.Forms.DataGridViewImageColumn RestarIndividual;
        private System.Windows.Forms.DataGridViewImageColumn EliminarIndividual;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroColumna;
        private System.Windows.Forms.DataGridViewTextBoxColumn ImagenProducto;
        private System.Windows.Forms.Timer timerBusqueda;
        private System.Windows.Forms.PictureBox PBImagen;
    }
}