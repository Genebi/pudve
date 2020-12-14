﻿namespace PuntoDeVentaV2
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
            this.btnUltimoTicket = new System.Windows.Forms.Button();
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
            this.cOtrosImpuestos = new System.Windows.Forms.Label();
            this.lbOtrosImpuestos = new System.Windows.Forms.Label();
            this.lbEliminarCliente = new System.Windows.Forms.Label();
            this.cAnticipoUtilizado = new System.Windows.Forms.Label();
            this.lbAnticipoUtilizado = new System.Windows.Forms.Label();
            this.lbDatosCliente = new System.Windows.Forms.Label();
            this.lbMayoreo = new System.Windows.Forms.Label();
            this.btnEliminarDescuentos = new System.Windows.Forms.Button();
            this.btnAplicarDescuento = new System.Windows.Forms.Button();
            this.PBImagen = new System.Windows.Forms.PictureBox();
            this.lbPS = new System.Windows.Forms.Label();
            this.nudCantidadPS = new System.Windows.Forms.NumericUpDown();
            this.lbCantidad = new System.Windows.Forms.Label();
            this.btnTerminarVenta = new System.Windows.Forms.Button();
            this.txtDescuentoGeneral = new System.Windows.Forms.TextBox();
            this.timerBusqueda = new System.Windows.Forms.Timer(this.components);
            this.checkCancelar = new System.Windows.Forms.CheckBox();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lFolio = new System.Windows.Forms.TextBox();
            this.timer_img_producto = new System.Windows.Forms.Timer(this.components);
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
            this.tituloSeccion.Location = new System.Drawing.Point(537, 14);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(157, 25);
            this.tituloSeccion.TabIndex = 4;
            this.tituloSeccion.Text = "NUEVA VENTA";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnEliminarAnticipos
            // 
            this.btnEliminarAnticipos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarAnticipos.Location = new System.Drawing.Point(451, 80);
            this.btnEliminarAnticipos.Name = "btnEliminarAnticipos";
            this.btnEliminarAnticipos.Size = new System.Drawing.Size(105, 28);
            this.btnEliminarAnticipos.TabIndex = 10;
            this.btnEliminarAnticipos.Text = "Eliminar Anticipos";
            this.tituloBoton.SetToolTip(this.btnEliminarAnticipos, "Eliminar todos los anticipos de esta venta");
            this.btnEliminarAnticipos.UseVisualStyleBackColor = true;
            this.btnEliminarAnticipos.Visible = false;
            this.btnEliminarAnticipos.Click += new System.EventHandler(this.btnEliminarAnticipos_Click);
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
            this.btnUltimoTicket.Location = new System.Drawing.Point(854, 47);
            this.btnUltimoTicket.Name = "btnUltimoTicket";
            this.btnUltimoTicket.Size = new System.Drawing.Size(37, 40);
            this.btnUltimoTicket.TabIndex = 18;
            this.tituloBoton.SetToolTip(this.btnUltimoTicket, "Imprimir último ticket");
            this.btnUltimoTicket.UseVisualStyleBackColor = true;
            this.btnUltimoTicket.Click += new System.EventHandler(this.btnUltimoTicket_Click);
            // 
            // txtBuscadorProducto
            // 
            this.txtBuscadorProducto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscadorProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscadorProducto.Location = new System.Drawing.Point(94, 31);
            this.txtBuscadorProducto.Name = "txtBuscadorProducto";
            this.txtBuscadorProducto.Size = new System.Drawing.Size(855, 23);
            this.txtBuscadorProducto.TabIndex = 5;
            this.txtBuscadorProducto.Text = "BUSCAR PRODUCTO O SERVICIO...";
            this.txtBuscadorProducto.TextChanged += new System.EventHandler(this.txtBuscadorProducto_TextChanged);
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
            this.DGVentas.Location = new System.Drawing.Point(3, 114);
            this.DGVentas.Name = "DGVentas";
            this.DGVentas.RowHeadersVisible = false;
            this.DGVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVentas.Size = new System.Drawing.Size(646, 203);
            this.DGVentas.TabIndex = 6;
            this.DGVentas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVentas_CellClick);
            this.DGVentas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVentas_CellContentClick);
            this.DGVentas.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVentas_CellEndEdit);
            this.DGVentas.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVentas_CellMouseEnter);
            this.DGVentas.SelectionChanged += new System.EventHandler(this.DGVentas_SelectionChanged);
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
            this.Cantidad.ReadOnly = false;
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
            this.Descuento.ReadOnly = false;
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
            this.btnCancelarVenta.Location = new System.Drawing.Point(897, 47);
            this.btnCancelarVenta.Name = "btnCancelarVenta";
            this.btnCancelarVenta.Size = new System.Drawing.Size(105, 40);
            this.btnCancelarVenta.TabIndex = 13;
            this.btnCancelarVenta.Text = "        Salir";
            this.btnCancelarVenta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelarVenta.UseVisualStyleBackColor = true;
            this.btnCancelarVenta.Click += new System.EventHandler(this.btnCancelarVenta_Click);
            // 
            // btnGuardarVenta
            // 
            this.btnGuardarVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardarVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarVenta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarVenta.Image = global::PuntoDeVentaV2.Properties.Resources.save1;
            this.btnGuardarVenta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGuardarVenta.Location = new System.Drawing.Point(506, 47);
            this.btnGuardarVenta.Name = "btnGuardarVenta";
            this.btnGuardarVenta.Size = new System.Drawing.Size(181, 40);
            this.btnGuardarVenta.TabIndex = 14;
            this.btnGuardarVenta.Text = "Guardar / Presupuesto";
            this.btnGuardarVenta.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnGuardarVenta.UseVisualStyleBackColor = true;
            this.btnGuardarVenta.Click += new System.EventHandler(this.btnGuardarVenta_Click);
            // 
            // btnAnticipos
            // 
            this.btnAnticipos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnticipos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnticipos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnticipos.Image = global::PuntoDeVentaV2.Properties.Resources.handshake_o1;
            this.btnAnticipos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAnticipos.Location = new System.Drawing.Point(277, 47);
            this.btnAnticipos.Name = "btnAnticipos";
            this.btnAnticipos.Size = new System.Drawing.Size(112, 40);
            this.btnAnticipos.TabIndex = 15;
            this.btnAnticipos.Text = "Anticipos";
            this.btnAnticipos.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnAnticipos.UseVisualStyleBackColor = true;
            this.btnAnticipos.Click += new System.EventHandler(this.btnAnticipos_Click);
            // 
            // btnAbrirCaja
            // 
            this.btnAbrirCaja.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAbrirCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAbrirCaja.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAbrirCaja.Image = global::PuntoDeVentaV2.Properties.Resources.hdd_o1;
            this.btnAbrirCaja.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAbrirCaja.Location = new System.Drawing.Point(395, 47);
            this.btnAbrirCaja.Name = "btnAbrirCaja";
            this.btnAbrirCaja.Size = new System.Drawing.Size(105, 40);
            this.btnAbrirCaja.TabIndex = 16;
            this.btnAbrirCaja.Text = "Abrir Caja";
            this.btnAbrirCaja.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnAbrirCaja.UseVisualStyleBackColor = true;
            this.btnAbrirCaja.Click += new System.EventHandler(this.btnAbrirCaja_Click);
            // 
            // btnVentasGuardadas
            // 
            this.btnVentasGuardadas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVentasGuardadas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVentasGuardadas.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVentasGuardadas.Image = global::PuntoDeVentaV2.Properties.Resources.clipboard1;
            this.btnVentasGuardadas.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVentasGuardadas.Location = new System.Drawing.Point(693, 47);
            this.btnVentasGuardadas.Name = "btnVentasGuardadas";
            this.btnVentasGuardadas.Size = new System.Drawing.Size(155, 40);
            this.btnVentasGuardadas.TabIndex = 17;
            this.btnVentasGuardadas.Text = "Ventas guardadas";
            this.btnVentasGuardadas.TextAlign = System.Drawing.ContentAlignment.TopRight;
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
            this.listaProductos.Size = new System.Drawing.Size(855, 21);
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
            this.lbNumeroArticulos.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumeroArticulos.Location = new System.Drawing.Point(672, 90);
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
            this.lbSubtotal.Location = new System.Drawing.Point(673, 112);
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
            this.lbIVA.Location = new System.Drawing.Point(673, 134);
            this.lbIVA.Name = "lbIVA";
            this.lbIVA.Size = new System.Drawing.Size(92, 22);
            this.lbIVA.TabIndex = 22;
            this.lbIVA.Text = "IVA 16%:";
            // 
            // lbAnticipo
            // 
            this.lbAnticipo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAnticipo.AutoSize = true;
            this.lbAnticipo.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAnticipo.Location = new System.Drawing.Point(673, 202);
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
            this.lbDescuento.Location = new System.Drawing.Point(673, 247);
            this.lbDescuento.Name = "lbDescuento";
            this.lbDescuento.Size = new System.Drawing.Size(115, 22);
            this.lbDescuento.TabIndex = 24;
            this.lbDescuento.Text = "Descuento:";
            this.lbDescuento.Visible = false;
            // 
            // lbTotal
            // 
            this.lbTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTotal.AutoSize = true;
            this.lbTotal.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.Location = new System.Drawing.Point(669, 274);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(125, 39);
            this.lbTotal.TabIndex = 25;
            this.lbTotal.Text = "Total $:";
            // 
            // cNumeroArticulos
            // 
            this.cNumeroArticulos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cNumeroArticulos.AutoSize = true;
            this.cNumeroArticulos.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cNumeroArticulos.Location = new System.Drawing.Point(877, 92);
            this.cNumeroArticulos.Name = "cNumeroArticulos";
            this.cNumeroArticulos.Size = new System.Drawing.Size(21, 22);
            this.cNumeroArticulos.TabIndex = 26;
            this.cNumeroArticulos.Text = "0";
            // 
            // cSubtotal
            // 
            this.cSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cSubtotal.AutoSize = true;
            this.cSubtotal.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cSubtotal.Location = new System.Drawing.Point(877, 112);
            this.cSubtotal.Name = "cSubtotal";
            this.cSubtotal.Size = new System.Drawing.Size(48, 22);
            this.cSubtotal.TabIndex = 27;
            this.cSubtotal.Text = "0.00";
            // 
            // cIVA
            // 
            this.cIVA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cIVA.AutoSize = true;
            this.cIVA.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cIVA.Location = new System.Drawing.Point(877, 134);
            this.cIVA.Name = "cIVA";
            this.cIVA.Size = new System.Drawing.Size(48, 22);
            this.cIVA.TabIndex = 28;
            this.cIVA.Text = "0.00";
            // 
            // cAnticipo
            // 
            this.cAnticipo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cAnticipo.AutoSize = true;
            this.cAnticipo.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cAnticipo.Location = new System.Drawing.Point(877, 202);
            this.cAnticipo.Name = "cAnticipo";
            this.cAnticipo.Size = new System.Drawing.Size(48, 22);
            this.cAnticipo.TabIndex = 29;
            this.cAnticipo.Text = "0.00";
            this.cAnticipo.Visible = false;
            // 
            // cDescuento
            // 
            this.cDescuento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cDescuento.AutoSize = true;
            this.cDescuento.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cDescuento.Location = new System.Drawing.Point(877, 247);
            this.cDescuento.Name = "cDescuento";
            this.cDescuento.Size = new System.Drawing.Size(48, 22);
            this.cDescuento.TabIndex = 30;
            this.cDescuento.Text = "0.00";
            this.cDescuento.Visible = false;
            // 
            // cTotal
            // 
            this.cTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cTotal.AutoSize = true;
            this.cTotal.Font = new System.Drawing.Font("Century Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cTotal.Location = new System.Drawing.Point(785, 274);
            this.cTotal.Name = "cTotal";
            this.cTotal.Size = new System.Drawing.Size(80, 39);
            this.cTotal.TabIndex = 31;
            this.cTotal.Text = "0.00";
            // 
            // lbIVA8
            // 
            this.lbIVA8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbIVA8.AutoSize = true;
            this.lbIVA8.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIVA8.Location = new System.Drawing.Point(673, 156);
            this.lbIVA8.Name = "lbIVA8";
            this.lbIVA8.Size = new System.Drawing.Size(81, 22);
            this.lbIVA8.TabIndex = 32;
            this.lbIVA8.Text = "IVA 8%:";
            this.lbIVA8.Visible = false;
            // 
            // cIVA8
            // 
            this.cIVA8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cIVA8.AutoSize = true;
            this.cIVA8.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cIVA8.Location = new System.Drawing.Point(877, 156);
            this.cIVA8.Name = "cIVA8";
            this.cIVA8.Size = new System.Drawing.Size(48, 22);
            this.cIVA8.TabIndex = 33;
            this.cIVA8.Text = "0.00";
            this.cIVA8.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.cOtrosImpuestos);
            this.panel1.Controls.Add(this.lbOtrosImpuestos);
            this.panel1.Controls.Add(this.lbEliminarCliente);
            this.panel1.Controls.Add(this.cAnticipoUtilizado);
            this.panel1.Controls.Add(this.lbAnticipoUtilizado);
            this.panel1.Controls.Add(this.lbDatosCliente);
            this.panel1.Controls.Add(this.listaProductos);
            this.panel1.Controls.Add(this.lbMayoreo);
            this.panel1.Controls.Add(this.btnEliminarDescuentos);
            this.panel1.Controls.Add(this.btnAplicarDescuento);
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
            this.panel1.Controls.Add(this.btnEliminarAnticipos);
            this.panel1.Controls.Add(this.cAnticipo);
            this.panel1.Controls.Add(this.cIVA);
            this.panel1.Controls.Add(this.cSubtotal);
            this.panel1.Controls.Add(this.cNumeroArticulos);
            this.panel1.Controls.Add(this.lbTotal);
            this.panel1.Controls.Add(this.lbDescuento);
            this.panel1.Controls.Add(this.lbAnticipo);
            this.panel1.Controls.Add(this.lbIVA);
            this.panel1.Controls.Add(this.lbSubtotal);
            this.panel1.Controls.Add(this.lbNumeroArticulos);
            this.panel1.Location = new System.Drawing.Point(7, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1221, 372);
            this.panel1.TabIndex = 34;
            // 
            // cOtrosImpuestos
            // 
            this.cOtrosImpuestos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cOtrosImpuestos.AutoSize = true;
            this.cOtrosImpuestos.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cOtrosImpuestos.Location = new System.Drawing.Point(877, 179);
            this.cOtrosImpuestos.Name = "cOtrosImpuestos";
            this.cOtrosImpuestos.Size = new System.Drawing.Size(48, 22);
            this.cOtrosImpuestos.TabIndex = 59;
            this.cOtrosImpuestos.Text = "0.00";
            this.cOtrosImpuestos.Visible = false;
            // 
            // lbOtrosImpuestos
            // 
            this.lbOtrosImpuestos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbOtrosImpuestos.AutoSize = true;
            this.lbOtrosImpuestos.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbOtrosImpuestos.Location = new System.Drawing.Point(673, 179);
            this.lbOtrosImpuestos.Name = "lbOtrosImpuestos";
            this.lbOtrosImpuestos.Size = new System.Drawing.Size(159, 22);
            this.lbOtrosImpuestos.TabIndex = 58;
            this.lbOtrosImpuestos.Text = "Otros impuestos:";
            this.lbOtrosImpuestos.Visible = false;
            // 
            // lbEliminarCliente
            // 
            this.lbEliminarCliente.AutoSize = true;
            this.lbEliminarCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbEliminarCliente.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEliminarCliente.ForeColor = System.Drawing.Color.Red;
            this.lbEliminarCliente.Location = new System.Drawing.Point(256, 8);
            this.lbEliminarCliente.Name = "lbEliminarCliente";
            this.lbEliminarCliente.Size = new System.Drawing.Size(17, 16);
            this.lbEliminarCliente.TabIndex = 57;
            this.lbEliminarCliente.Text = "X";
            this.lbEliminarCliente.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbEliminarCliente.Visible = false;
            this.lbEliminarCliente.Click += new System.EventHandler(this.lbEliminarCliente_Click);
            // 
            // cAnticipoUtilizado
            // 
            this.cAnticipoUtilizado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cAnticipoUtilizado.AutoSize = true;
            this.cAnticipoUtilizado.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cAnticipoUtilizado.Location = new System.Drawing.Point(877, 225);
            this.cAnticipoUtilizado.Name = "cAnticipoUtilizado";
            this.cAnticipoUtilizado.Size = new System.Drawing.Size(48, 22);
            this.cAnticipoUtilizado.TabIndex = 56;
            this.cAnticipoUtilizado.Text = "0.00";
            this.cAnticipoUtilizado.Visible = false;
            // 
            // lbAnticipoUtilizado
            // 
            this.lbAnticipoUtilizado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAnticipoUtilizado.AutoSize = true;
            this.lbAnticipoUtilizado.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAnticipoUtilizado.Location = new System.Drawing.Point(673, 225);
            this.lbAnticipoUtilizado.Name = "lbAnticipoUtilizado";
            this.lbAnticipoUtilizado.Size = new System.Drawing.Size(171, 22);
            this.lbAnticipoUtilizado.TabIndex = 55;
            this.lbAnticipoUtilizado.Text = "Anticipo utilizado:";
            this.lbAnticipoUtilizado.Visible = false;
            // 
            // lbDatosCliente
            // 
            this.lbDatosCliente.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDatosCliente.ForeColor = System.Drawing.Color.Red;
            this.lbDatosCliente.Location = new System.Drawing.Point(271, 5);
            this.lbDatosCliente.Name = "lbDatosCliente";
            this.lbDatosCliente.Size = new System.Drawing.Size(621, 23);
            this.lbDatosCliente.TabIndex = 45;
            this.lbDatosCliente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbMayoreo
            // 
            this.lbMayoreo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMayoreo.ForeColor = System.Drawing.Color.DarkGreen;
            this.lbMayoreo.Image = global::PuntoDeVentaV2.Properties.Resources.check1;
            this.lbMayoreo.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbMayoreo.Location = new System.Drawing.Point(3, 95);
            this.lbMayoreo.Name = "lbMayoreo";
            this.lbMayoreo.Size = new System.Drawing.Size(138, 16);
            this.lbMayoreo.TabIndex = 45;
            this.lbMayoreo.Text = "Mayoreo aplicado";
            this.lbMayoreo.Visible = false;
            // 
            // btnEliminarDescuentos
            // 
            this.btnEliminarDescuentos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarDescuentos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarDescuentos.Location = new System.Drawing.Point(311, 321);
            this.btnEliminarDescuentos.Name = "btnEliminarDescuentos";
            this.btnEliminarDescuentos.Size = new System.Drawing.Size(113, 25);
            this.btnEliminarDescuentos.TabIndex = 44;
            this.btnEliminarDescuentos.Text = "Eliminar (Alt + 1)";
            this.btnEliminarDescuentos.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEliminarDescuentos.UseVisualStyleBackColor = true;
            this.btnEliminarDescuentos.Click += new System.EventHandler(this.btnEliminarDescuentos_Click);
            // 
            // btnAplicarDescuento
            // 
            this.btnAplicarDescuento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAplicarDescuento.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicarDescuento.Location = new System.Drawing.Point(535, 321);
            this.btnAplicarDescuento.Name = "btnAplicarDescuento";
            this.btnAplicarDescuento.Size = new System.Drawing.Size(114, 25);
            this.btnAplicarDescuento.TabIndex = 43;
            this.btnAplicarDescuento.Text = "Aplicar (Alt + 3)";
            this.btnAplicarDescuento.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAplicarDescuento.UseVisualStyleBackColor = true;
            this.btnAplicarDescuento.Click += new System.EventHandler(this.btnAplicarDescuento_Click);
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
            this.btnTerminarVenta.Location = new System.Drawing.Point(820, 321);
            this.btnTerminarVenta.Name = "btnTerminarVenta";
            this.btnTerminarVenta.Size = new System.Drawing.Size(105, 25);
            this.btnTerminarVenta.TabIndex = 37;
            this.btnTerminarVenta.Text = "Terminar \"Fin\"";
            this.btnTerminarVenta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnTerminarVenta.UseVisualStyleBackColor = true;
            this.btnTerminarVenta.Click += new System.EventHandler(this.btnTerminarVenta_Click);
            // 
            // txtDescuentoGeneral
            // 
            this.txtDescuentoGeneral.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescuentoGeneral.Location = new System.Drawing.Point(430, 322);
            this.txtDescuentoGeneral.Multiline = true;
            this.txtDescuentoGeneral.Name = "txtDescuentoGeneral";
            this.txtDescuentoGeneral.Size = new System.Drawing.Size(100, 23);
            this.txtDescuentoGeneral.TabIndex = 35;
            this.txtDescuentoGeneral.Text = "% descuento";
            this.txtDescuentoGeneral.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDescuentoGeneral.Enter += new System.EventHandler(this.txtDescuentoGeneral_Enter);
            this.txtDescuentoGeneral.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDescuentoGeneral_KeyUp);
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
            this.checkCancelar.Location = new System.Drawing.Point(1097, 47);
            this.checkCancelar.Name = "checkCancelar";
            this.checkCancelar.Size = new System.Drawing.Size(123, 21);
            this.checkCancelar.TabIndex = 35;
            this.checkCancelar.Text = "Cancelar VENTA";
            this.checkCancelar.UseVisualStyleBackColor = true;
            this.checkCancelar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkCancelar_MouseClick);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultar.Image = global::PuntoDeVentaV2.Properties.Resources.search1;
            this.btnConsultar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsultar.Location = new System.Drawing.Point(5, 47);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(100, 40);
            this.btnConsultar.TabIndex = 43;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnClientes
            // 
            this.btnClientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClientes.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClientes.Image = global::PuntoDeVentaV2.Properties.Resources.discount;
            this.btnClientes.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClientes.Location = new System.Drawing.Point(111, 47);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(160, 40);
            this.btnClientes.TabIndex = 44;
            this.btnClientes.Text = "Descuento cliente";
            this.btnClientes.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.btnClientes.UseVisualStyleBackColor = true;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label1.Location = new System.Drawing.Point(49, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 45;
            this.label1.Text = "Ctrl + B";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label2.Location = new System.Drawing.Point(177, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 46;
            this.label2.Text = "Ctrl + D";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Location = new System.Drawing.Point(945, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "ESC";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Location = new System.Drawing.Point(576, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "Ctrl + G";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label5.Location = new System.Drawing.Point(330, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 49;
            this.label5.Text = "Ctrl + A";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label6.Location = new System.Drawing.Point(455, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 50;
            this.label6.Text = "F2";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label7.Location = new System.Drawing.Point(758, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 51;
            this.label7.Text = "Ctrl + M";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // lFolio
            // 
            this.lFolio.Location = new System.Drawing.Point(1097, 67);
            this.lFolio.Name = "lFolio";
            this.lFolio.Size = new System.Drawing.Size(123, 20);
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
            // Ventas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 596);
            this.Controls.Add(this.lFolio);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnClientes);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.checkCancelar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tituloSeccion);
            this.Controls.Add(this.btnCancelarVenta);
            this.Controls.Add(this.btnGuardarVenta);
            this.Controls.Add(this.btnAnticipos);
            this.Controls.Add(this.btnAbrirCaja);
            this.Controls.Add(this.btnVentasGuardadas);
            this.Controls.Add(this.btnUltimoTicket);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(1027, 597);
            this.Name = "Ventas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventas_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Ventas_FormClosed);
            this.Load += new System.EventHandler(this.Ventas_Load);
            this.Shown += new System.EventHandler(this.Ventas_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Ventas_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Ventas_KeyPress_1);
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
        private System.Windows.Forms.Button btnEliminarAnticipos;
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
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Label lbDatosCliente;
        private System.Windows.Forms.Button btnEliminarDescuentos;
        private System.Windows.Forms.DataGridViewTextBoxColumn AplicarDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioMayoreo;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrecioAuxiliar;
        private System.Windows.Forms.DataGridViewTextBoxColumn TipoDescuento;
        private System.Windows.Forms.DataGridViewTextBoxColumn Impuesto;
        private System.Windows.Forms.Label lbMayoreo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox lFolio;
        private System.Windows.Forms.Label cAnticipoUtilizado;
        private System.Windows.Forms.Label lbAnticipoUtilizado;
        private System.Windows.Forms.Label lbEliminarCliente;
        private System.Windows.Forms.Label cOtrosImpuestos;
        private System.Windows.Forms.Label lbOtrosImpuestos;
        private System.Windows.Forms.Timer timer_img_producto;
    }
}