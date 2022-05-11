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
            this.btnEliminarAnticipos = new System.Windows.Forms.Button();
            this.btnEliminarUltimo = new System.Windows.Forms.Button();
            this.btnEliminarTodos = new System.Windows.Forms.Button();
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
            this.AplicarDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioMayoreo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrecioAuxiliar = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TipoDescuento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Impuesto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnCancelarVenta = new System.Windows.Forms.Button();
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnAplicarDescuento = new System.Windows.Forms.Button();
            this.txtDescuentoGeneral = new System.Windows.Forms.TextBox();
            this.btnEliminarDescuentos = new System.Windows.Forms.Button();
            this.lbEliminarCliente = new System.Windows.Forms.Label();
            this.lbDatosCliente = new System.Windows.Forms.Label();
            this.lbMayoreo = new System.Windows.Forms.Label();
            this.lbPS = new System.Windows.Forms.Label();
            this.nudCantidadPS = new System.Windows.Forms.NumericUpDown();
            this.lbCantidad = new System.Windows.Forms.Label();
            this.lb_cant_impuestos_retenidos = new System.Windows.Forms.Label();
            this.lb_impuestos_retenidos = new System.Windows.Forms.Label();
            this.cOtrosImpuestos = new System.Windows.Forms.Label();
            this.lbOtrosImpuestos = new System.Windows.Forms.Label();
            this.cAnticipoUtilizado = new System.Windows.Forms.Label();
            this.lbAnticipoUtilizado = new System.Windows.Forms.Label();
            this.PBImagen = new System.Windows.Forms.PictureBox();
            this.btnTerminarVenta = new System.Windows.Forms.Button();
            this.timerBusqueda = new System.Windows.Forms.Timer(this.components);
            this.checkCancelar = new System.Windows.Forms.CheckBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lFolio = new System.Windows.Forms.TextBox();
            this.timer_img_producto = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblCIVA0Exento = new System.Windows.Forms.Label();
            this.lblIVA0Exento = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnAbrirCaja = new PuntoDeVentaV2.BotonRedondo();
            this.btnAnticipos = new PuntoDeVentaV2.BotonRedondo();
            this.btnUltimoTicket = new PuntoDeVentaV2.BotonRedondo();
            this.btnClientes = new PuntoDeVentaV2.BotonRedondo();
            this.btnGuardarVenta = new PuntoDeVentaV2.BotonRedondo();
            this.btnVentasGuardadas = new PuntoDeVentaV2.BotonRedondo();
            this.btn_cancelar_venta = new PuntoDeVentaV2.BotonRedondo();
            this.btnBascula = new System.Windows.Forms.Button();
            this.lblPesoRecibido = new System.Windows.Forms.Label();
            this.btnCSV = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVentas)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadPS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBImagen)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(537, 14);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(157, 25);
            this.tituloSeccion.TabIndex = 4;
            this.tituloSeccion.Text = "NUEVA VENTA";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.tituloSeccion.Visible = false;
            // 
            // btnEliminarAnticipos
            // 
            this.btnEliminarAnticipos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(149)))), ((int)(((byte)(11)))));
            this.btnEliminarAnticipos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarAnticipos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarAnticipos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarAnticipos.ForeColor = System.Drawing.Color.White;
            this.btnEliminarAnticipos.Location = new System.Drawing.Point(289, 316);
            this.btnEliminarAnticipos.Name = "btnEliminarAnticipos";
            this.btnEliminarAnticipos.Size = new System.Drawing.Size(122, 25);
            this.btnEliminarAnticipos.TabIndex = 10;
            this.btnEliminarAnticipos.Text = "Eliminar anticipos";
            this.tituloBoton.SetToolTip(this.btnEliminarAnticipos, "Eliminar todos los anticipos de esta venta");
            this.btnEliminarAnticipos.UseVisualStyleBackColor = false;
            this.btnEliminarAnticipos.Visible = false;
            this.btnEliminarAnticipos.Click += new System.EventHandler(this.btnEliminarAnticipos_Click);
            // 
            // btnEliminarUltimo
            // 
            this.btnEliminarUltimo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(41)))), ((int)(((byte)(20)))));
            this.btnEliminarUltimo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarUltimo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarUltimo.ForeColor = System.Drawing.Color.White;
            this.btnEliminarUltimo.Location = new System.Drawing.Point(3, 7);
            this.btnEliminarUltimo.Name = "btnEliminarUltimo";
            this.btnEliminarUltimo.Size = new System.Drawing.Size(40, 26);
            this.btnEliminarUltimo.TabIndex = 7;
            this.tituloBoton.SetToolTip(this.btnEliminarUltimo, "Eliminar último agregado");
            this.btnEliminarUltimo.UseVisualStyleBackColor = false;
            this.btnEliminarUltimo.Visible = false;
            this.btnEliminarUltimo.Click += new System.EventHandler(this.btnEliminarUltimo_Click);
            // 
            // btnEliminarTodos
            // 
            this.btnEliminarTodos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(41)))), ((int)(((byte)(20)))));
            this.btnEliminarTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarTodos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarTodos.ForeColor = System.Drawing.Color.White;
            this.btnEliminarTodos.Location = new System.Drawing.Point(49, 7);
            this.btnEliminarTodos.Name = "btnEliminarTodos";
            this.btnEliminarTodos.Size = new System.Drawing.Size(40, 26);
            this.btnEliminarTodos.TabIndex = 8;
            this.tituloBoton.SetToolTip(this.btnEliminarTodos, "Eliminar todos los agregados");
            this.btnEliminarTodos.UseVisualStyleBackColor = false;
            this.btnEliminarTodos.Click += new System.EventHandler(this.btnEliminarTodos_Click);
            // 
            // txtBuscadorProducto
            // 
            this.txtBuscadorProducto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscadorProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscadorProducto.Location = new System.Drawing.Point(94, 29);
            this.txtBuscadorProducto.Name = "txtBuscadorProducto";
            this.txtBuscadorProducto.Size = new System.Drawing.Size(790, 23);
            this.txtBuscadorProducto.TabIndex = 41;
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
            this.ImagenProducto,
            this.AplicarDescuento,
            this.PrecioMayoreo,
            this.PrecioAuxiliar,
            this.TipoDescuento,
            this.Impuesto});
            this.DGVentas.Location = new System.Drawing.Point(7, 81);
            this.DGVentas.Name = "DGVentas";
            this.DGVentas.RowHeadersVisible = false;
            this.DGVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVentas.Size = new System.Drawing.Size(877, 222);
            this.DGVentas.TabIndex = 6;
            this.DGVentas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVentas_CellClick);
            this.DGVentas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVentas_CellContentClick_1);
            this.DGVentas.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVentas_CellEndEdit);
            this.DGVentas.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVentas_CellEnter);
            this.DGVentas.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVentas_CellMouseEnter);
            this.DGVentas.CellStateChanged += new System.Windows.Forms.DataGridViewCellStateChangedEventHandler(this.DGVentas_CellStateChanged);
            this.DGVentas.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVentas_CellValueChanged);
            this.DGVentas.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.DGVentas_RowPostPaint);
            this.DGVentas.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.DGVentas_RowsAdded);
            this.DGVentas.SelectionChanged += new System.EventHandler(this.DGVentas_SelectionChanged);
            this.DGVentas.Enter += new System.EventHandler(this.DGVentas_Enter);
            this.DGVentas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGVentas_MouseUp);
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
            this.Descuento.Width = 75;
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
            this.AgregarMultiple.Visible = false;
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
            // AplicarDescuento
            // 
            this.AplicarDescuento.HeaderText = "AplicarDescuento";
            this.AplicarDescuento.Name = "AplicarDescuento";
            this.AplicarDescuento.ReadOnly = true;
            this.AplicarDescuento.Visible = false;
            // 
            // PrecioMayoreo
            // 
            this.PrecioMayoreo.HeaderText = "PrecioMayoreo";
            this.PrecioMayoreo.Name = "PrecioMayoreo";
            this.PrecioMayoreo.ReadOnly = true;
            this.PrecioMayoreo.Visible = false;
            // 
            // PrecioAuxiliar
            // 
            this.PrecioAuxiliar.HeaderText = "PrecioAuxiliar";
            this.PrecioAuxiliar.Name = "PrecioAuxiliar";
            this.PrecioAuxiliar.ReadOnly = true;
            this.PrecioAuxiliar.Visible = false;
            // 
            // TipoDescuento
            // 
            this.TipoDescuento.HeaderText = "TipoDescuento";
            this.TipoDescuento.Name = "TipoDescuento";
            this.TipoDescuento.ReadOnly = true;
            this.TipoDescuento.Visible = false;
            // 
            // Impuesto
            // 
            this.Impuesto.HeaderText = "Impuesto";
            this.Impuesto.Name = "Impuesto";
            this.Impuesto.ReadOnly = true;
            this.Impuesto.Visible = false;
            // 
            // btnCancelarVenta
            // 
            this.btnCancelarVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarVenta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarVenta.Image = global::PuntoDeVentaV2.Properties.Resources.reply1;
            this.btnCancelarVenta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelarVenta.Location = new System.Drawing.Point(127, 20);
            this.btnCancelarVenta.Name = "btnCancelarVenta";
            this.btnCancelarVenta.Size = new System.Drawing.Size(105, 40);
            this.btnCancelarVenta.TabIndex = 13;
            this.btnCancelarVenta.Text = "        Salir";
            this.btnCancelarVenta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelarVenta.UseVisualStyleBackColor = true;
            this.btnCancelarVenta.Click += new System.EventHandler(this.btnCancelarVenta_Click);
            this.btnCancelarVenta.Enter += new System.EventHandler(this.btnCancelarVenta_Enter);
            // 
            // listaProductos
            // 
            this.listaProductos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaProductos.FormattingEnabled = true;
            this.listaProductos.ItemHeight = 17;
            this.listaProductos.Location = new System.Drawing.Point(94, 51);
            this.listaProductos.Name = "listaProductos";
            this.listaProductos.Size = new System.Drawing.Size(790, 21);
            this.listaProductos.TabIndex = 9;
            this.listaProductos.Visible = false;
            this.listaProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listaProductos_KeyDown);
            this.listaProductos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listaProductos_MouseDoubleClick);
            this.listaProductos.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.listaProductos_PreviewKeyDown);
            // 
            // lbNumeroArticulos
            // 
            this.lbNumeroArticulos.AutoSize = true;
            this.lbNumeroArticulos.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumeroArticulos.Location = new System.Drawing.Point(7, 240);
            this.lbNumeroArticulos.Name = "lbNumeroArticulos";
            this.lbNumeroArticulos.Size = new System.Drawing.Size(198, 22);
            this.lbNumeroArticulos.TabIndex = 20;
            this.lbNumeroArticulos.Text = "Número de artículos:";
            // 
            // lbSubtotal
            // 
            this.lbSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSubtotal.AutoSize = true;
            this.lbSubtotal.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSubtotal.Location = new System.Drawing.Point(7, 262);
            this.lbSubtotal.Name = "lbSubtotal";
            this.lbSubtotal.Size = new System.Drawing.Size(91, 22);
            this.lbSubtotal.TabIndex = 21;
            this.lbSubtotal.Text = "Subtotal:";
            // 
            // lbIVA
            // 
            this.lbIVA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbIVA.AutoSize = true;
            this.lbIVA.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIVA.Location = new System.Drawing.Point(7, 307);
            this.lbIVA.Name = "lbIVA";
            this.lbIVA.Size = new System.Drawing.Size(92, 22);
            this.lbIVA.TabIndex = 22;
            this.lbIVA.Text = "IVA 16%:";
            this.lbIVA.Visible = false;
            // 
            // lbAnticipo
            // 
            this.lbAnticipo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAnticipo.AutoSize = true;
            this.lbAnticipo.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAnticipo.Location = new System.Drawing.Point(7, 441);
            this.lbAnticipo.Name = "lbAnticipo";
            this.lbAnticipo.Size = new System.Drawing.Size(171, 22);
            this.lbAnticipo.TabIndex = 23;
            this.lbAnticipo.Text = "Anticipo recibido:";
            this.lbAnticipo.Visible = false;
            // 
            // lbDescuento
            // 
            this.lbDescuento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDescuento.AutoSize = true;
            this.lbDescuento.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescuento.Location = new System.Drawing.Point(7, 284);
            this.lbDescuento.Name = "lbDescuento";
            this.lbDescuento.Size = new System.Drawing.Size(115, 22);
            this.lbDescuento.TabIndex = 24;
            this.lbDescuento.Text = "Descuento:";
            this.lbDescuento.Visible = false;
            // 
            // lbTotal
            // 
            this.lbTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTotal.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.Location = new System.Drawing.Point(4, 494);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(126, 39);
            this.lbTotal.TabIndex = 25;
            this.lbTotal.Text = "Total $:";
            // 
            // cNumeroArticulos
            // 
            this.cNumeroArticulos.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cNumeroArticulos.Location = new System.Drawing.Point(211, 242);
            this.cNumeroArticulos.Name = "cNumeroArticulos";
            this.cNumeroArticulos.Size = new System.Drawing.Size(98, 22);
            this.cNumeroArticulos.TabIndex = 26;
            this.cNumeroArticulos.Text = "0";
            this.cNumeroArticulos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cNumeroArticulos.Click += new System.EventHandler(this.cNumeroArticulos_Click);
            // 
            // cSubtotal
            // 
            this.cSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cSubtotal.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cSubtotal.Location = new System.Drawing.Point(211, 262);
            this.cSubtotal.Name = "cSubtotal";
            this.cSubtotal.Size = new System.Drawing.Size(98, 22);
            this.cSubtotal.TabIndex = 27;
            this.cSubtotal.Text = "0.00";
            this.cSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cSubtotal.Click += new System.EventHandler(this.cSubtotal_Click);
            // 
            // cIVA
            // 
            this.cIVA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cIVA.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cIVA.Location = new System.Drawing.Point(211, 307);
            this.cIVA.Name = "cIVA";
            this.cIVA.Size = new System.Drawing.Size(98, 22);
            this.cIVA.TabIndex = 28;
            this.cIVA.Text = "0.00";
            this.cIVA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cIVA.Visible = false;
            this.cIVA.Click += new System.EventHandler(this.cIVA_Click);
            // 
            // cAnticipo
            // 
            this.cAnticipo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cAnticipo.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cAnticipo.Location = new System.Drawing.Point(211, 441);
            this.cAnticipo.Name = "cAnticipo";
            this.cAnticipo.Size = new System.Drawing.Size(98, 22);
            this.cAnticipo.TabIndex = 29;
            this.cAnticipo.Text = "0.00";
            this.cAnticipo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cAnticipo.Visible = false;
            this.cAnticipo.Click += new System.EventHandler(this.cAnticipo_Click);
            // 
            // cDescuento
            // 
            this.cDescuento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cDescuento.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cDescuento.Location = new System.Drawing.Point(211, 284);
            this.cDescuento.Name = "cDescuento";
            this.cDescuento.Size = new System.Drawing.Size(98, 22);
            this.cDescuento.TabIndex = 30;
            this.cDescuento.Text = "0.00";
            this.cDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cDescuento.Visible = false;
            // 
            // cTotal
            // 
            this.cTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cTotal.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cTotal.Location = new System.Drawing.Point(123, 494);
            this.cTotal.Name = "cTotal";
            this.cTotal.Size = new System.Drawing.Size(189, 39);
            this.cTotal.TabIndex = 31;
            this.cTotal.Text = "0.00";
            this.cTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbIVA8
            // 
            this.lbIVA8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbIVA8.AutoSize = true;
            this.lbIVA8.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIVA8.Location = new System.Drawing.Point(7, 331);
            this.lbIVA8.Name = "lbIVA8";
            this.lbIVA8.Size = new System.Drawing.Size(81, 22);
            this.lbIVA8.TabIndex = 32;
            this.lbIVA8.Text = "IVA 8%:";
            this.lbIVA8.Visible = false;
            // 
            // cIVA8
            // 
            this.cIVA8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cIVA8.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cIVA8.Location = new System.Drawing.Point(211, 331);
            this.cIVA8.Name = "cIVA8";
            this.cIVA8.Size = new System.Drawing.Size(98, 22);
            this.cIVA8.TabIndex = 33;
            this.cIVA8.Text = "0.00";
            this.cIVA8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cIVA8.Visible = false;
            this.cIVA8.Click += new System.EventHandler(this.cIVA8_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.lbEliminarCliente);
            this.panel1.Controls.Add(this.lbDatosCliente);
            this.panel1.Controls.Add(this.listaProductos);
            this.panel1.Controls.Add(this.lbMayoreo);
            this.panel1.Controls.Add(this.lbPS);
            this.panel1.Controls.Add(this.nudCantidadPS);
            this.panel1.Controls.Add(this.lbCantidad);
            this.panel1.Controls.Add(this.DGVentas);
            this.panel1.Controls.Add(this.txtBuscadorProducto);
            this.panel1.Controls.Add(this.btnEliminarAnticipos);
            this.panel1.Location = new System.Drawing.Point(12, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(890, 389);
            this.panel1.TabIndex = 34;
            this.panel1.Click += new System.EventHandler(this.panel1_Click);
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.btnEliminarTodos);
            this.panel4.Controls.Add(this.btnEliminarUltimo);
            this.panel4.Location = new System.Drawing.Point(792, 307);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(92, 40);
            this.panel4.TabIndex = 59;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.btnAplicarDescuento);
            this.panel3.Controls.Add(this.txtDescuentoGeneral);
            this.panel3.Controls.Add(this.btnEliminarDescuentos);
            this.panel3.Location = new System.Drawing.Point(423, 307);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(353, 40);
            this.panel3.TabIndex = 58;
            // 
            // btnAplicarDescuento
            // 
            this.btnAplicarDescuento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(65)))), ((int)(((byte)(131)))));
            this.btnAplicarDescuento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAplicarDescuento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicarDescuento.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicarDescuento.ForeColor = System.Drawing.Color.White;
            this.btnAplicarDescuento.Location = new System.Drawing.Point(113, 7);
            this.btnAplicarDescuento.Name = "btnAplicarDescuento";
            this.btnAplicarDescuento.Size = new System.Drawing.Size(114, 25);
            this.btnAplicarDescuento.TabIndex = 43;
            this.btnAplicarDescuento.Text = "Aplicar (Alt + 3)";
            this.btnAplicarDescuento.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAplicarDescuento.UseVisualStyleBackColor = false;
            this.btnAplicarDescuento.Click += new System.EventHandler(this.btnAplicarDescuento_Click);
            // 
            // txtDescuentoGeneral
            // 
            this.txtDescuentoGeneral.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescuentoGeneral.Location = new System.Drawing.Point(8, 8);
            this.txtDescuentoGeneral.Multiline = true;
            this.txtDescuentoGeneral.Name = "txtDescuentoGeneral";
            this.txtDescuentoGeneral.Size = new System.Drawing.Size(100, 23);
            this.txtDescuentoGeneral.TabIndex = 35;
            this.txtDescuentoGeneral.Text = "% descuento";
            this.txtDescuentoGeneral.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDescuentoGeneral.Enter += new System.EventHandler(this.txtDescuentoGeneral_Enter);
            this.txtDescuentoGeneral.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescuentoGeneral_KeyDown);
            this.txtDescuentoGeneral.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDescuentoGeneral_KeyUp);
            // 
            // btnEliminarDescuentos
            // 
            this.btnEliminarDescuentos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(83)))), ((int)(((byte)(136)))));
            this.btnEliminarDescuentos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarDescuentos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarDescuentos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarDescuentos.ForeColor = System.Drawing.Color.White;
            this.btnEliminarDescuentos.Location = new System.Drawing.Point(233, 7);
            this.btnEliminarDescuentos.Name = "btnEliminarDescuentos";
            this.btnEliminarDescuentos.Size = new System.Drawing.Size(113, 25);
            this.btnEliminarDescuentos.TabIndex = 44;
            this.btnEliminarDescuentos.Text = "Eliminar (Alt + 1)";
            this.btnEliminarDescuentos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEliminarDescuentos.UseVisualStyleBackColor = false;
            this.btnEliminarDescuentos.Click += new System.EventHandler(this.btnEliminarDescuentos_Click);
            // 
            // lbEliminarCliente
            // 
            this.lbEliminarCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbEliminarCliente.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEliminarCliente.ForeColor = System.Drawing.Color.Black;
            this.lbEliminarCliente.Location = new System.Drawing.Point(25, 358);
            this.lbEliminarCliente.Name = "lbEliminarCliente";
            this.lbEliminarCliente.Size = new System.Drawing.Size(12, 23);
            this.lbEliminarCliente.TabIndex = 57;
            this.lbEliminarCliente.Text = "X";
            this.lbEliminarCliente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbEliminarCliente.Visible = false;
            this.lbEliminarCliente.Click += new System.EventHandler(this.lbEliminarCliente_Click);
            // 
            // lbDatosCliente
            // 
            this.lbDatosCliente.AutoSize = true;
            this.lbDatosCliente.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDatosCliente.ForeColor = System.Drawing.Color.Black;
            this.lbDatosCliente.Location = new System.Drawing.Point(35, 361);
            this.lbDatosCliente.Name = "lbDatosCliente";
            this.lbDatosCliente.Size = new System.Drawing.Size(0, 16);
            this.lbDatosCliente.TabIndex = 45;
            this.lbDatosCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbDatosCliente.Visible = false;
            this.lbDatosCliente.Click += new System.EventHandler(this.lbDatosCliente_Click);
            // 
            // lbMayoreo
            // 
            this.lbMayoreo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMayoreo.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbMayoreo.Image = global::PuntoDeVentaV2.Properties.Resources.check1;
            this.lbMayoreo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbMayoreo.Location = new System.Drawing.Point(13, 309);
            this.lbMayoreo.Name = "lbMayoreo";
            this.lbMayoreo.Size = new System.Drawing.Size(138, 16);
            this.lbMayoreo.TabIndex = 45;
            this.lbMayoreo.Text = "Mayoreo aplicado";
            this.lbMayoreo.Visible = false;
            // 
            // lbPS
            // 
            this.lbPS.AutoSize = true;
            this.lbPS.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPS.Location = new System.Drawing.Point(91, 9);
            this.lbPS.Name = "lbPS";
            this.lbPS.Size = new System.Drawing.Size(121, 17);
            this.lbPS.TabIndex = 41;
            this.lbPS.Text = "Producto / servicio";
            // 
            // nudCantidadPS
            // 
            this.nudCantidadPS.DecimalPlaces = 2;
            this.nudCantidadPS.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudCantidadPS.Location = new System.Drawing.Point(7, 29);
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
            this.nudCantidadPS.Click += new System.EventHandler(this.nudCantidadPS_Click);
            // 
            // lbCantidad
            // 
            this.lbCantidad.AutoSize = true;
            this.lbCantidad.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCantidad.Location = new System.Drawing.Point(7, 9);
            this.lbCantidad.Name = "lbCantidad";
            this.lbCantidad.Size = new System.Drawing.Size(64, 17);
            this.lbCantidad.TabIndex = 39;
            this.lbCantidad.Text = "Cantidad";
            // 
            // lb_cant_impuestos_retenidos
            // 
            this.lb_cant_impuestos_retenidos.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_cant_impuestos_retenidos.Location = new System.Drawing.Point(210, 418);
            this.lb_cant_impuestos_retenidos.Name = "lb_cant_impuestos_retenidos";
            this.lb_cant_impuestos_retenidos.Size = new System.Drawing.Size(98, 22);
            this.lb_cant_impuestos_retenidos.TabIndex = 61;
            this.lb_cant_impuestos_retenidos.Text = "0.00";
            this.lb_cant_impuestos_retenidos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_cant_impuestos_retenidos.Visible = false;
            // 
            // lb_impuestos_retenidos
            // 
            this.lb_impuestos_retenidos.AutoSize = true;
            this.lb_impuestos_retenidos.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_impuestos_retenidos.Location = new System.Drawing.Point(7, 418);
            this.lb_impuestos_retenidos.Name = "lb_impuestos_retenidos";
            this.lb_impuestos_retenidos.Size = new System.Drawing.Size(196, 22);
            this.lb_impuestos_retenidos.TabIndex = 60;
            this.lb_impuestos_retenidos.Text = "Impuestos retenidos:";
            this.lb_impuestos_retenidos.Visible = false;
            // 
            // cOtrosImpuestos
            // 
            this.cOtrosImpuestos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cOtrosImpuestos.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cOtrosImpuestos.Location = new System.Drawing.Point(211, 396);
            this.cOtrosImpuestos.Name = "cOtrosImpuestos";
            this.cOtrosImpuestos.Size = new System.Drawing.Size(98, 22);
            this.cOtrosImpuestos.TabIndex = 59;
            this.cOtrosImpuestos.Text = "0.00";
            this.cOtrosImpuestos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cOtrosImpuestos.Visible = false;
            this.cOtrosImpuestos.Click += new System.EventHandler(this.cOtrosImpuestos_Click);
            // 
            // lbOtrosImpuestos
            // 
            this.lbOtrosImpuestos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOtrosImpuestos.AutoSize = true;
            this.lbOtrosImpuestos.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOtrosImpuestos.Location = new System.Drawing.Point(7, 374);
            this.lbOtrosImpuestos.Name = "lbOtrosImpuestos";
            this.lbOtrosImpuestos.Size = new System.Drawing.Size(154, 44);
            this.lbOtrosImpuestos.TabIndex = 58;
            this.lbOtrosImpuestos.Text = "Otros impuestos\r\ntraslados:";
            this.lbOtrosImpuestos.Visible = false;
            // 
            // cAnticipoUtilizado
            // 
            this.cAnticipoUtilizado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cAnticipoUtilizado.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cAnticipoUtilizado.Location = new System.Drawing.Point(211, 464);
            this.cAnticipoUtilizado.Name = "cAnticipoUtilizado";
            this.cAnticipoUtilizado.Size = new System.Drawing.Size(98, 22);
            this.cAnticipoUtilizado.TabIndex = 56;
            this.cAnticipoUtilizado.Text = "0.00";
            this.cAnticipoUtilizado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cAnticipoUtilizado.Visible = false;
            // 
            // lbAnticipoUtilizado
            // 
            this.lbAnticipoUtilizado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAnticipoUtilizado.AutoSize = true;
            this.lbAnticipoUtilizado.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAnticipoUtilizado.Location = new System.Drawing.Point(7, 464);
            this.lbAnticipoUtilizado.Name = "lbAnticipoUtilizado";
            this.lbAnticipoUtilizado.Size = new System.Drawing.Size(171, 22);
            this.lbAnticipoUtilizado.TabIndex = 55;
            this.lbAnticipoUtilizado.Text = "Anticipo utilizado:";
            this.lbAnticipoUtilizado.Visible = false;
            // 
            // PBImagen
            // 
            this.PBImagen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PBImagen.Location = new System.Drawing.Point(57, 5);
            this.PBImagen.Name = "PBImagen";
            this.PBImagen.Size = new System.Drawing.Size(220, 220);
            this.PBImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBImagen.TabIndex = 42;
            this.PBImagen.TabStop = false;
            // 
            // btnTerminarVenta
            // 
            this.btnTerminarVenta.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTerminarVenta.BackColor = System.Drawing.Color.Green;
            this.btnTerminarVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTerminarVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTerminarVenta.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTerminarVenta.ForeColor = System.Drawing.Color.White;
            this.btnTerminarVenta.Location = new System.Drawing.Point(14, 543);
            this.btnTerminarVenta.Name = "btnTerminarVenta";
            this.btnTerminarVenta.Size = new System.Drawing.Size(285, 33);
            this.btnTerminarVenta.TabIndex = 37;
            this.btnTerminarVenta.Text = "Terminar \"Fin\"";
            this.btnTerminarVenta.UseVisualStyleBackColor = false;
            this.btnTerminarVenta.Click += new System.EventHandler(this.btnTerminarVenta_Click);
            // 
            // timerBusqueda
            // 
            this.timerBusqueda.Interval = 1000;
            this.timerBusqueda.Tick += new System.EventHandler(this.timerBusqueda_Tick);
            // 
            // checkCancelar
            // 
            this.checkCancelar.AutoSize = true;
            this.checkCancelar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkCancelar.Location = new System.Drawing.Point(724, 579);
            this.checkCancelar.Name = "checkCancelar";
            this.checkCancelar.Size = new System.Drawing.Size(123, 21);
            this.checkCancelar.TabIndex = 35;
            this.checkCancelar.Text = "Cancelar VENTA";
            this.checkCancelar.UseVisualStyleBackColor = true;
            this.checkCancelar.Visible = false;
            this.checkCancelar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkCancelar_MouseClick);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultar.Image = global::PuntoDeVentaV2.Properties.Resources.search1;
            this.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.Location = new System.Drawing.Point(12, 20);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(100, 40);
            this.btnConsultar.TabIndex = 0;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            this.btnConsultar.Enter += new System.EventHandler(this.btnConsultar_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Location = new System.Drawing.Point(56, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "Ctrl + B";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Location = new System.Drawing.Point(175, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "ESC";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // lFolio
            // 
            this.lFolio.Location = new System.Drawing.Point(853, 580);
            this.lFolio.Name = "lFolio";
            this.lFolio.Size = new System.Drawing.Size(43, 20);
            this.lFolio.TabIndex = 52;
            this.lFolio.Visible = false;
            this.lFolio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lFolio_KeyDown);
            this.lFolio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lFolio_KeyPress);
            // 
            // timer_img_producto
            // 
            this.timer_img_producto.Interval = 7000;
            this.timer_img_producto.Tick += new System.EventHandler(this.timer_img_producto_Tick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.panel2.Controls.Add(this.lblCIVA0Exento);
            this.panel2.Controls.Add(this.lblIVA0Exento);
            this.panel2.Controls.Add(this.PBImagen);
            this.panel2.Controls.Add(this.lb_cant_impuestos_retenidos);
            this.panel2.Controls.Add(this.cOtrosImpuestos);
            this.panel2.Controls.Add(this.lb_impuestos_retenidos);
            this.panel2.Controls.Add(this.btnTerminarVenta);
            this.panel2.Controls.Add(this.cAnticipoUtilizado);
            this.panel2.Controls.Add(this.lbNumeroArticulos);
            this.panel2.Controls.Add(this.lbAnticipoUtilizado);
            this.panel2.Controls.Add(this.lbOtrosImpuestos);
            this.panel2.Controls.Add(this.cNumeroArticulos);
            this.panel2.Controls.Add(this.lbTotal);
            this.panel2.Controls.Add(this.lbIVA);
            this.panel2.Controls.Add(this.lbSubtotal);
            this.panel2.Controls.Add(this.cSubtotal);
            this.panel2.Controls.Add(this.cIVA);
            this.panel2.Controls.Add(this.lbIVA8);
            this.panel2.Controls.Add(this.cIVA8);
            this.panel2.Controls.Add(this.lbAnticipo);
            this.panel2.Controls.Add(this.cAnticipo);
            this.panel2.Controls.Add(this.cDescuento);
            this.panel2.Controls.Add(this.lbDescuento);
            this.panel2.Controls.Add(this.cTotal);
            this.panel2.Location = new System.Drawing.Point(908, 15);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(312, 585);
            this.panel2.TabIndex = 62;
            this.panel2.Click += new System.EventHandler(this.panel2_Click);
            // 
            // lblCIVA0Exento
            // 
            this.lblCIVA0Exento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCIVA0Exento.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCIVA0Exento.Location = new System.Drawing.Point(210, 355);
            this.lblCIVA0Exento.Name = "lblCIVA0Exento";
            this.lblCIVA0Exento.Size = new System.Drawing.Size(98, 22);
            this.lblCIVA0Exento.TabIndex = 63;
            this.lblCIVA0Exento.Text = "0.00";
            this.lblCIVA0Exento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCIVA0Exento.Visible = false;
            // 
            // lblIVA0Exento
            // 
            this.lblIVA0Exento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIVA0Exento.AutoSize = true;
            this.lblIVA0Exento.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIVA0Exento.Location = new System.Drawing.Point(7, 353);
            this.lblIVA0Exento.Name = "lblIVA0Exento";
            this.lblIVA0Exento.Size = new System.Drawing.Size(164, 22);
            this.lblIVA0Exento.TabIndex = 62;
            this.lblIVA0Exento.Text = "IVA 0% y Exento:";
            this.lblIVA0Exento.Visible = false;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.panel5.Controls.Add(this.btnAbrirCaja);
            this.panel5.Controls.Add(this.btnAnticipos);
            this.panel5.Controls.Add(this.btnUltimoTicket);
            this.panel5.Controls.Add(this.btnClientes);
            this.panel5.Controls.Add(this.btnGuardarVenta);
            this.panel5.Controls.Add(this.btnVentasGuardadas);
            this.panel5.Controls.Add(this.btn_cancelar_venta);
            this.panel5.Location = new System.Drawing.Point(12, 480);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(881, 95);
            this.panel5.TabIndex = 63;
            this.panel5.Click += new System.EventHandler(this.panel5_Click);
            // 
            // btnAbrirCaja
            // 
            this.btnAbrirCaja.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(68)))), ((int)(((byte)(53)))));
            this.btnAbrirCaja.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(68)))), ((int)(((byte)(53)))));
            this.btnAbrirCaja.BorderColor = System.Drawing.Color.Transparent;
            this.btnAbrirCaja.BorderRadius = 20;
            this.btnAbrirCaja.BorderSize = 0;
            this.btnAbrirCaja.FlatAppearance.BorderSize = 0;
            this.btnAbrirCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAbrirCaja.ForeColor = System.Drawing.Color.White;
            this.btnAbrirCaja.Image = global::PuntoDeVentaV2.Properties.Resources.box_open;
            this.btnAbrirCaja.Location = new System.Drawing.Point(10, 10);
            this.btnAbrirCaja.Name = "btnAbrirCaja";
            this.btnAbrirCaja.Size = new System.Drawing.Size(116, 73);
            this.btnAbrirCaja.TabIndex = 65;
            this.btnAbrirCaja.Text = "Abrir caja (F2)";
            this.btnAbrirCaja.TextColor = System.Drawing.Color.White;
            this.btnAbrirCaja.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAbrirCaja.UseVisualStyleBackColor = false;
            this.btnAbrirCaja.Click += new System.EventHandler(this.botonRedondo1_Click);
            // 
            // btnAnticipos
            // 
            this.btnAnticipos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(173)))), ((int)(((byte)(23)))));
            this.btnAnticipos.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(173)))), ((int)(((byte)(23)))));
            this.btnAnticipos.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnAnticipos.BorderRadius = 20;
            this.btnAnticipos.BorderSize = 0;
            this.btnAnticipos.FlatAppearance.BorderSize = 0;
            this.btnAnticipos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnticipos.ForeColor = System.Drawing.Color.White;
            this.btnAnticipos.Image = global::PuntoDeVentaV2.Properties.Resources.coins_in_hand;
            this.btnAnticipos.Location = new System.Drawing.Point(144, 10);
            this.btnAnticipos.Name = "btnAnticipos";
            this.btnAnticipos.Size = new System.Drawing.Size(102, 73);
            this.btnAnticipos.TabIndex = 66;
            this.btnAnticipos.Text = "Aplic. Anticipo (Ctrl + A)";
            this.btnAnticipos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAnticipos.TextColor = System.Drawing.Color.White;
            this.btnAnticipos.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAnticipos.UseVisualStyleBackColor = false;
            this.btnAnticipos.Click += new System.EventHandler(this.botonRedondo2_Click);
            // 
            // btnUltimoTicket
            // 
            this.btnUltimoTicket.BackColor = System.Drawing.Color.PaleGreen;
            this.btnUltimoTicket.BackGroundColor = System.Drawing.Color.PaleGreen;
            this.btnUltimoTicket.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnUltimoTicket.BorderRadius = 10;
            this.btnUltimoTicket.BorderSize = 0;
            this.btnUltimoTicket.FlatAppearance.BorderSize = 0;
            this.btnUltimoTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUltimoTicket.ForeColor = System.Drawing.Color.White;
            this.btnUltimoTicket.Image = global::PuntoDeVentaV2.Properties.Resources.ticket1;
            this.btnUltimoTicket.Location = new System.Drawing.Point(814, 26);
            this.btnUltimoTicket.Name = "btnUltimoTicket";
            this.btnUltimoTicket.Size = new System.Drawing.Size(49, 41);
            this.btnUltimoTicket.TabIndex = 71;
            this.btnUltimoTicket.TextColor = System.Drawing.Color.White;
            this.btnUltimoTicket.UseVisualStyleBackColor = false;
            this.btnUltimoTicket.Click += new System.EventHandler(this.botonRedondo7_Click);
            // 
            // btnClientes
            // 
            this.btnClientes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(112)))), ((int)(((byte)(28)))));
            this.btnClientes.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(112)))), ((int)(((byte)(28)))));
            this.btnClientes.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnClientes.BorderRadius = 20;
            this.btnClientes.BorderSize = 0;
            this.btnClientes.FlatAppearance.BorderSize = 0;
            this.btnClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClientes.ForeColor = System.Drawing.Color.White;
            this.btnClientes.Image = global::PuntoDeVentaV2.Properties.Resources.tag_yellow;
            this.btnClientes.Location = new System.Drawing.Point(268, 10);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(119, 73);
            this.btnClientes.TabIndex = 67;
            this.btnClientes.Text = "Descuento cliente (Ctrl + D)";
            this.btnClientes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClientes.TextColor = System.Drawing.Color.White;
            this.btnClientes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnClientes.UseVisualStyleBackColor = false;
            this.btnClientes.Click += new System.EventHandler(this.botonRedondo3_Click);
            // 
            // btnGuardarVenta
            // 
            this.btnGuardarVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(154)))), ((int)(((byte)(132)))));
            this.btnGuardarVenta.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(154)))), ((int)(((byte)(132)))));
            this.btnGuardarVenta.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnGuardarVenta.BorderRadius = 20;
            this.btnGuardarVenta.BorderSize = 0;
            this.btnGuardarVenta.FlatAppearance.BorderSize = 0;
            this.btnGuardarVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarVenta.ForeColor = System.Drawing.Color.White;
            this.btnGuardarVenta.Image = global::PuntoDeVentaV2.Properties.Resources.disk;
            this.btnGuardarVenta.Location = new System.Drawing.Point(408, 10);
            this.btnGuardarVenta.Name = "btnGuardarVenta";
            this.btnGuardarVenta.Size = new System.Drawing.Size(137, 73);
            this.btnGuardarVenta.TabIndex = 68;
            this.btnGuardarVenta.Text = "Guardar venta / ppto. (Ctrl + G)";
            this.btnGuardarVenta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnGuardarVenta.TextColor = System.Drawing.Color.White;
            this.btnGuardarVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnGuardarVenta.UseVisualStyleBackColor = false;
            this.btnGuardarVenta.Click += new System.EventHandler(this.botonRedondo4_Click);
            // 
            // btnVentasGuardadas
            // 
            this.btnVentasGuardadas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(119)))), ((int)(((byte)(147)))));
            this.btnVentasGuardadas.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(119)))), ((int)(((byte)(147)))));
            this.btnVentasGuardadas.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnVentasGuardadas.BorderRadius = 20;
            this.btnVentasGuardadas.BorderSize = 0;
            this.btnVentasGuardadas.FlatAppearance.BorderSize = 0;
            this.btnVentasGuardadas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVentasGuardadas.ForeColor = System.Drawing.Color.White;
            this.btnVentasGuardadas.Image = global::PuntoDeVentaV2.Properties.Resources.clipboard_invoice;
            this.btnVentasGuardadas.Location = new System.Drawing.Point(561, 10);
            this.btnVentasGuardadas.Name = "btnVentasGuardadas";
            this.btnVentasGuardadas.Size = new System.Drawing.Size(121, 73);
            this.btnVentasGuardadas.TabIndex = 69;
            this.btnVentasGuardadas.Text = "Ventas guardadas (Ctrl + M)";
            this.btnVentasGuardadas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVentasGuardadas.TextColor = System.Drawing.Color.White;
            this.btnVentasGuardadas.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnVentasGuardadas.UseVisualStyleBackColor = false;
            this.btnVentasGuardadas.Click += new System.EventHandler(this.botonRedondo5_Click);
            // 
            // btn_cancelar_venta
            // 
            this.btn_cancelar_venta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(99)))), ((int)(((byte)(63)))));
            this.btn_cancelar_venta.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(99)))), ((int)(((byte)(63)))));
            this.btn_cancelar_venta.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btn_cancelar_venta.BorderRadius = 20;
            this.btn_cancelar_venta.BorderSize = 0;
            this.btn_cancelar_venta.FlatAppearance.BorderSize = 0;
            this.btn_cancelar_venta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancelar_venta.ForeColor = System.Drawing.Color.White;
            this.btn_cancelar_venta.Image = global::PuntoDeVentaV2.Properties.Resources.page_delete;
            this.btn_cancelar_venta.Location = new System.Drawing.Point(697, 10);
            this.btn_cancelar_venta.Name = "btn_cancelar_venta";
            this.btn_cancelar_venta.Size = new System.Drawing.Size(100, 73);
            this.btn_cancelar_venta.TabIndex = 70;
            this.btn_cancelar_venta.Text = "Cancelar ventas previas";
            this.btn_cancelar_venta.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_cancelar_venta.TextColor = System.Drawing.Color.White;
            this.btn_cancelar_venta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btn_cancelar_venta.UseVisualStyleBackColor = false;
            this.btn_cancelar_venta.Click += new System.EventHandler(this.botonRedondo6_Click);
            // 
            // btnBascula
            // 
            this.btnBascula.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBascula.Image = global::PuntoDeVentaV2.Properties.Resources.peso;
            this.btnBascula.Location = new System.Drawing.Point(253, 20);
            this.btnBascula.Name = "btnBascula";
            this.btnBascula.Size = new System.Drawing.Size(105, 40);
            this.btnBascula.TabIndex = 0;
            this.btnBascula.Text = "Tomar Peso Ctrl + T";
            this.btnBascula.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBascula.UseVisualStyleBackColor = true;
            this.btnBascula.Click += new System.EventHandler(this.btnBascula_Click);
            // 
            // lblPesoRecibido
            // 
            this.lblPesoRecibido.AutoSize = true;
            this.lblPesoRecibido.Location = new System.Drawing.Point(364, 26);
            this.lblPesoRecibido.Name = "lblPesoRecibido";
            this.lblPesoRecibido.Size = new System.Drawing.Size(13, 13);
            this.lblPesoRecibido.TabIndex = 64;
            this.lblPesoRecibido.Text = "0";
            this.lblPesoRecibido.Visible = false;
            // 
            // btnCSV
            // 
            this.btnCSV.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCSV.Image = global::PuntoDeVentaV2.Properties.Resources.file_excel_o1;
            this.btnCSV.Location = new System.Drawing.Point(383, 20);
            this.btnCSV.Name = "btnCSV";
            this.btnCSV.Size = new System.Drawing.Size(105, 40);
            this.btnCSV.TabIndex = 0;
            this.btnCSV.Text = "Operación CSV";
            this.btnCSV.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCSV.UseVisualStyleBackColor = true;
            this.btnCSV.Click += new System.EventHandler(this.btnCSV_Click);
            // 
            // Ventas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.ClientSize = new System.Drawing.Size(1232, 608);
            this.Controls.Add(this.lblPesoRecibido);
            this.Controls.Add(this.btnCSV);
            this.Controls.Add(this.btnBascula);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lFolio);
            this.Controls.Add(this.checkCancelar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tituloSeccion);
            this.Controls.Add(this.btnCancelarVenta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1027, 597);
            this.Name = "Ventas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nueva venta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventas_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventas_FormClosed);
            this.Load += new System.EventHandler(this.Ventas_Load);
            this.Shown += new System.EventHandler(this.Ventas_Shown);
            this.Click += new System.EventHandler(this.Ventas_Click);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventas_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ventas_KeyPress_1);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Ventas_Layout);
            ((System.ComponentModel.ISupportInitialize)(this.DGVentas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadPS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBImagen)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.ToolTip tituloBoton;
        private System.Windows.Forms.Button btnEliminarAnticipos;
        private System.Windows.Forms.TextBox txtBuscadorProducto;
        private System.Windows.Forms.DataGridView DGVentas;
        private System.Windows.Forms.Button btnEliminarUltimo;
        private System.Windows.Forms.Button btnEliminarTodos;
        private System.Windows.Forms.Button btnCancelarVenta;
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
        private System.Windows.Forms.CheckBox checkCancelar;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Button btnAplicarDescuento;
        private System.Windows.Forms.Label lbDatosCliente;
        private System.Windows.Forms.Button btnEliminarDescuentos;
        private System.Windows.Forms.DataGridViewTextBoxColumn AplicarDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioMayoreo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioAuxiliar;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Impuesto;
        private System.Windows.Forms.Label lbMayoreo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox lFolio;
        private System.Windows.Forms.Label cAnticipoUtilizado;
        private System.Windows.Forms.Label lbAnticipoUtilizado;
        private System.Windows.Forms.Label lbEliminarCliente;
        private System.Windows.Forms.Label cOtrosImpuestos;
        private System.Windows.Forms.Label lbOtrosImpuestos;
        private System.Windows.Forms.Timer timer_img_producto;
        private System.Windows.Forms.Label lb_impuestos_retenidos;
        private System.Windows.Forms.Label lb_cant_impuestos_retenidos;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnBascula;
        private System.Windows.Forms.Label lblPesoRecibido;
        private BotonRedondo btn_cancelar_venta;
        private BotonRedondo btnVentasGuardadas;
        private BotonRedondo btnGuardarVenta;
        private BotonRedondo btnClientes;
        private BotonRedondo btnAnticipos;
        private BotonRedondo btnAbrirCaja;
        public BotonRedondo btnUltimoTicket;
        private System.Windows.Forms.Button btnCSV;
        private System.Windows.Forms.Label lblCIVA0Exento;
        private System.Windows.Forms.Label lblIVA0Exento;
    }
}