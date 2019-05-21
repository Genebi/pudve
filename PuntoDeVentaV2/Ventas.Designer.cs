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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCancelarVenta = new System.Windows.Forms.Button();
            this.btnGuardarVenta = new System.Windows.Forms.Button();
            this.btnAnticipos = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
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
            this.lbPS = new System.Windows.Forms.Label();
            this.nudCantidadPS = new System.Windows.Forms.NumericUpDown();
            this.lbCantidad = new System.Windows.Forms.Label();
            this.btnTerminarVenta = new System.Windows.Forms.Button();
            this.cbEstadoVenta = new System.Windows.Forms.ComboBox();
            this.txtDescuentoGeneral = new System.Windows.Forms.TextBox();
            this.btnDetallesVenta = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVentas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudCantidadPS)).BeginInit();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(760, 27);
            this.tituloSeccion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(198, 32);
            this.tituloSeccion.TabIndex = 4;
            this.tituloSeccion.Text = "NUEVA VENTA";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnProductoRapido
            // 
            this.btnProductoRapido.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProductoRapido.Location = new System.Drawing.Point(540, 98);
            this.btnProductoRapido.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnProductoRapido.Name = "btnProductoRapido";
            this.btnProductoRapido.Size = new System.Drawing.Size(53, 34);
            this.btnProductoRapido.TabIndex = 10;
            this.tituloBoton.SetToolTip(this.btnProductoRapido, "Agregar producto o servicio rápido");
            this.btnProductoRapido.UseVisualStyleBackColor = true;
            // 
            // btnEliminarUltimo
            // 
            this.btnEliminarUltimo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarUltimo.Location = new System.Drawing.Point(601, 98);
            this.btnEliminarUltimo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEliminarUltimo.Name = "btnEliminarUltimo";
            this.btnEliminarUltimo.Size = new System.Drawing.Size(53, 34);
            this.btnEliminarUltimo.TabIndex = 7;
            this.tituloBoton.SetToolTip(this.btnEliminarUltimo, "Eliminar último agregado");
            this.btnEliminarUltimo.UseVisualStyleBackColor = true;
            this.btnEliminarUltimo.Click += new System.EventHandler(this.btnEliminarUltimo_Click);
            // 
            // btnEliminarTodos
            // 
            this.btnEliminarTodos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarTodos.Location = new System.Drawing.Point(663, 98);
            this.btnEliminarTodos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnEliminarTodos.Name = "btnEliminarTodos";
            this.btnEliminarTodos.Size = new System.Drawing.Size(53, 34);
            this.btnEliminarTodos.TabIndex = 8;
            this.tituloBoton.SetToolTip(this.btnEliminarTodos, "Eliminar todos los agregados");
            this.btnEliminarTodos.UseVisualStyleBackColor = true;
            this.btnEliminarTodos.Click += new System.EventHandler(this.btnEliminarTodos_Click);
            // 
            // btnUltimoTicket
            // 
            this.btnUltimoTicket.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUltimoTicket.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUltimoTicket.Location = new System.Drawing.Point(1576, 38);
            this.btnUltimoTicket.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnUltimoTicket.Name = "btnUltimoTicket";
            this.btnUltimoTicket.Size = new System.Drawing.Size(49, 28);
            this.btnUltimoTicket.TabIndex = 18;
            this.tituloBoton.SetToolTip(this.btnUltimoTicket, "Imprimir último ticket");
            this.btnUltimoTicket.UseVisualStyleBackColor = true;
            this.btnUltimoTicket.Click += new System.EventHandler(this.btnUltimoTicket_Click);
            // 
            // btnPresupuesto
            // 
            this.btnPresupuesto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPresupuesto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPresupuesto.Location = new System.Drawing.Point(1627, 38);
            this.btnPresupuesto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPresupuesto.Name = "btnPresupuesto";
            this.btnPresupuesto.Size = new System.Drawing.Size(49, 28);
            this.btnPresupuesto.TabIndex = 19;
            this.tituloBoton.SetToolTip(this.btnPresupuesto, "Guardar como presupuesto");
            this.btnPresupuesto.UseVisualStyleBackColor = true;
            this.btnPresupuesto.Click += new System.EventHandler(this.btnPresupuesto_Click);
            // 
            // txtBuscadorProducto
            // 
            this.txtBuscadorProducto.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtBuscadorProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscadorProducto.Location = new System.Drawing.Point(125, 38);
            this.txtBuscadorProducto.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtBuscadorProducto.Name = "txtBuscadorProducto";
            this.txtBuscadorProducto.Size = new System.Drawing.Size(589, 27);
            this.txtBuscadorProducto.TabIndex = 5;
            this.txtBuscadorProducto.Text = "buscar producto o servicio...";
            this.txtBuscadorProducto.TextChanged += new System.EventHandler(this.txtBuscadorProducto_TextChanged);
            this.txtBuscadorProducto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscadorProducto_KeyDown);
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
            this.EliminarIndividual});
            this.DGVentas.Location = new System.Drawing.Point(4, 140);
            this.DGVentas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DGVentas.Name = "DGVentas";
            this.DGVentas.Size = new System.Drawing.Size(712, 250);
            this.DGVentas.TabIndex = 6;
            this.DGVentas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVentas_CellClick);
            this.DGVentas.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVentas_CellMouseEnter);
            // 
            // IDProducto
            // 
            this.IDProducto.HeaderText = "_ID";
            this.IDProducto.Name = "IDProducto";
            this.IDProducto.Visible = false;
            // 
            // Stock
            // 
            this.Stock.HeaderText = "_Stock";
            this.Stock.Name = "Stock";
            this.Stock.Visible = false;
            // 
            // PrecioOriginal
            // 
            this.PrecioOriginal.HeaderText = "_PrecioOriginal";
            this.PrecioOriginal.Name = "PrecioOriginal";
            this.PrecioOriginal.Visible = false;
            // 
            // DescuentoTipo
            // 
            this.DescuentoTipo.HeaderText = "_TipoDescuento";
            this.DescuentoTipo.Name = "DescuentoTipo";
            this.DescuentoTipo.Visible = false;
            // 
            // TipoPS
            // 
            this.TipoPS.HeaderText = "_TIpoPS";
            this.TipoPS.Name = "TipoPS";
            this.TipoPS.Visible = false;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.Width = 65;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.Width = 65;
            // 
            // Descripcion
            // 
            this.Descripcion.HeaderText = "Descripción";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.Width = 130;
            // 
            // Descuento
            // 
            this.Descuento.HeaderText = "Descuento";
            this.Descuento.Name = "Descuento";
            this.Descuento.Width = 65;
            // 
            // Importe
            // 
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            this.Importe.Width = 65;
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
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(739, 38);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(113, 105);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // btnCancelarVenta
            // 
            this.btnCancelarVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarVenta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarVenta.Location = new System.Drawing.Point(925, 38);
            this.btnCancelarVenta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelarVenta.Name = "btnCancelarVenta";
            this.btnCancelarVenta.Size = new System.Drawing.Size(100, 28);
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
            this.btnGuardarVenta.Location = new System.Drawing.Point(1033, 38);
            this.btnGuardarVenta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnGuardarVenta.Name = "btnGuardarVenta";
            this.btnGuardarVenta.Size = new System.Drawing.Size(100, 28);
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
            this.btnAnticipos.Location = new System.Drawing.Point(1141, 38);
            this.btnAnticipos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAnticipos.Name = "btnAnticipos";
            this.btnAnticipos.Size = new System.Drawing.Size(100, 28);
            this.btnAnticipos.TabIndex = 15;
            this.btnAnticipos.Text = "Anticipos";
            this.btnAnticipos.UseVisualStyleBackColor = true;
            this.btnAnticipos.Click += new System.EventHandler(this.btnAnticipos_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Location = new System.Drawing.Point(1249, 38);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 28);
            this.button4.TabIndex = 16;
            this.button4.Text = "Abrir Caja";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // btnVentasGuardadas
            // 
            this.btnVentasGuardadas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVentasGuardadas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVentasGuardadas.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVentasGuardadas.Location = new System.Drawing.Point(1359, 38);
            this.btnVentasGuardadas.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnVentasGuardadas.Name = "btnVentasGuardadas";
            this.btnVentasGuardadas.Size = new System.Drawing.Size(209, 28);
            this.btnVentasGuardadas.TabIndex = 17;
            this.btnVentasGuardadas.Text = "Ventas guardadas";
            this.btnVentasGuardadas.UseVisualStyleBackColor = true;
            this.btnVentasGuardadas.Click += new System.EventHandler(this.btnVentasGuardadas_Click);
            // 
            // listaProductos
            // 
            this.listaProductos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaProductos.FormattingEnabled = true;
            this.listaProductos.ItemHeight = 21;
            this.listaProductos.Location = new System.Drawing.Point(125, 65);
            this.listaProductos.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.listaProductos.Name = "listaProductos";
            this.listaProductos.Size = new System.Drawing.Size(589, 67);
            this.listaProductos.TabIndex = 9;
            this.listaProductos.Visible = false;
            this.listaProductos.SelectedIndexChanged += new System.EventHandler(this.listaProductos_SelectedIndexChanged);
            // 
            // lbNumeroArticulos
            // 
            this.lbNumeroArticulos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbNumeroArticulos.AutoSize = true;
            this.lbNumeroArticulos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumeroArticulos.Location = new System.Drawing.Point(921, 140);
            this.lbNumeroArticulos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbNumeroArticulos.Name = "lbNumeroArticulos";
            this.lbNumeroArticulos.Size = new System.Drawing.Size(182, 21);
            this.lbNumeroArticulos.TabIndex = 20;
            this.lbNumeroArticulos.Text = "Número de artículos:";
            // 
            // lbSubtotal
            // 
            this.lbSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSubtotal.AutoSize = true;
            this.lbSubtotal.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSubtotal.Location = new System.Drawing.Point(921, 167);
            this.lbSubtotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbSubtotal.Name = "lbSubtotal";
            this.lbSubtotal.Size = new System.Drawing.Size(85, 21);
            this.lbSubtotal.TabIndex = 21;
            this.lbSubtotal.Text = "Subtotal:";
            // 
            // lbIVA
            // 
            this.lbIVA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbIVA.AutoSize = true;
            this.lbIVA.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIVA.Location = new System.Drawing.Point(921, 194);
            this.lbIVA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbIVA.Name = "lbIVA";
            this.lbIVA.Size = new System.Drawing.Size(82, 21);
            this.lbIVA.TabIndex = 22;
            this.lbIVA.Text = "IVA 16%:";
            // 
            // lbAnticipo
            // 
            this.lbAnticipo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAnticipo.AutoSize = true;
            this.lbAnticipo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAnticipo.Location = new System.Drawing.Point(921, 249);
            this.lbAnticipo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbAnticipo.Name = "lbAnticipo";
            this.lbAnticipo.Size = new System.Drawing.Size(85, 21);
            this.lbAnticipo.TabIndex = 23;
            this.lbAnticipo.Text = "Anticipo:";
            // 
            // lbDescuento
            // 
            this.lbDescuento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDescuento.AutoSize = true;
            this.lbDescuento.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescuento.Location = new System.Drawing.Point(921, 276);
            this.lbDescuento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDescuento.Name = "lbDescuento";
            this.lbDescuento.Size = new System.Drawing.Size(106, 21);
            this.lbDescuento.TabIndex = 24;
            this.lbDescuento.Text = "Descuento:";
            // 
            // lbTotal
            // 
            this.lbTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTotal.AutoSize = true;
            this.lbTotal.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotal.Location = new System.Drawing.Point(921, 303);
            this.lbTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(56, 21);
            this.lbTotal.TabIndex = 25;
            this.lbTotal.Text = "Total:";
            // 
            // cNumeroArticulos
            // 
            this.cNumeroArticulos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cNumeroArticulos.AutoSize = true;
            this.cNumeroArticulos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cNumeroArticulos.Location = new System.Drawing.Point(1137, 143);
            this.cNumeroArticulos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cNumeroArticulos.Name = "cNumeroArticulos";
            this.cNumeroArticulos.Size = new System.Drawing.Size(19, 21);
            this.cNumeroArticulos.TabIndex = 26;
            this.cNumeroArticulos.Text = "0";
            // 
            // cSubtotal
            // 
            this.cSubtotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cSubtotal.AutoSize = true;
            this.cSubtotal.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cSubtotal.Location = new System.Drawing.Point(1137, 167);
            this.cSubtotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cSubtotal.Name = "cSubtotal";
            this.cSubtotal.Size = new System.Drawing.Size(42, 21);
            this.cSubtotal.TabIndex = 27;
            this.cSubtotal.Text = "0.00";
            // 
            // cIVA
            // 
            this.cIVA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cIVA.AutoSize = true;
            this.cIVA.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cIVA.Location = new System.Drawing.Point(1137, 194);
            this.cIVA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cIVA.Name = "cIVA";
            this.cIVA.Size = new System.Drawing.Size(42, 21);
            this.cIVA.TabIndex = 28;
            this.cIVA.Text = "0.00";
            // 
            // cAnticipo
            // 
            this.cAnticipo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cAnticipo.AutoSize = true;
            this.cAnticipo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cAnticipo.Location = new System.Drawing.Point(1137, 249);
            this.cAnticipo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cAnticipo.Name = "cAnticipo";
            this.cAnticipo.Size = new System.Drawing.Size(42, 21);
            this.cAnticipo.TabIndex = 29;
            this.cAnticipo.Text = "0.00";
            // 
            // cDescuento
            // 
            this.cDescuento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cDescuento.AutoSize = true;
            this.cDescuento.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cDescuento.Location = new System.Drawing.Point(1137, 276);
            this.cDescuento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cDescuento.Name = "cDescuento";
            this.cDescuento.Size = new System.Drawing.Size(42, 21);
            this.cDescuento.TabIndex = 30;
            this.cDescuento.Text = "0.00";
            // 
            // cTotal
            // 
            this.cTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cTotal.AutoSize = true;
            this.cTotal.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cTotal.Location = new System.Drawing.Point(1137, 303);
            this.cTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cTotal.Name = "cTotal";
            this.cTotal.Size = new System.Drawing.Size(42, 21);
            this.cTotal.TabIndex = 31;
            this.cTotal.Text = "0.00";
            // 
            // lbIVA8
            // 
            this.lbIVA8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbIVA8.AutoSize = true;
            this.lbIVA8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIVA8.Location = new System.Drawing.Point(921, 222);
            this.lbIVA8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbIVA8.Name = "lbIVA8";
            this.lbIVA8.Size = new System.Drawing.Size(73, 21);
            this.lbIVA8.TabIndex = 32;
            this.lbIVA8.Text = "IVA 8%:";
            // 
            // cIVA8
            // 
            this.cIVA8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cIVA8.AutoSize = true;
            this.cIVA8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cIVA8.Location = new System.Drawing.Point(1137, 222);
            this.cIVA8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cIVA8.Name = "cIVA8";
            this.cIVA8.Size = new System.Drawing.Size(42, 21);
            this.cIVA8.TabIndex = 33;
            this.cIVA8.Text = "0.00";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.listaProductos);
            this.panel1.Controls.Add(this.lbPS);
            this.panel1.Controls.Add(this.nudCantidadPS);
            this.panel1.Controls.Add(this.lbCantidad);
            this.panel1.Controls.Add(this.btnTerminarVenta);
            this.panel1.Controls.Add(this.cbEstadoVenta);
            this.panel1.Controls.Add(this.txtDescuentoGeneral);
            this.panel1.Controls.Add(this.btnDetallesVenta);
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
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.cSubtotal);
            this.panel1.Controls.Add(this.cNumeroArticulos);
            this.panel1.Controls.Add(this.btnCancelarVenta);
            this.panel1.Controls.Add(this.lbTotal);
            this.panel1.Controls.Add(this.btnGuardarVenta);
            this.panel1.Controls.Add(this.lbDescuento);
            this.panel1.Controls.Add(this.btnAnticipos);
            this.panel1.Controls.Add(this.lbAnticipo);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.lbIVA);
            this.panel1.Controls.Add(this.btnVentasGuardadas);
            this.panel1.Controls.Add(this.lbSubtotal);
            this.panel1.Controls.Add(this.btnUltimoTicket);
            this.panel1.Controls.Add(this.lbNumeroArticulos);
            this.panel1.Controls.Add(this.btnPresupuesto);
            this.panel1.Location = new System.Drawing.Point(16, 91);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1680, 521);
            this.panel1.TabIndex = 34;
            // 
            // lbPS
            // 
            this.lbPS.AutoSize = true;
            this.lbPS.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPS.Location = new System.Drawing.Point(121, 14);
            this.lbPS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPS.Name = "lbPS";
            this.lbPS.Size = new System.Drawing.Size(149, 20);
            this.lbPS.TabIndex = 41;
            this.lbPS.Text = "Producto / servicio";
            // 
            // nudCantidadPS
            // 
            this.nudCantidadPS.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nudCantidadPS.Location = new System.Drawing.Point(4, 38);
            this.nudCantidadPS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.nudCantidadPS.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.nudCantidadPS.Name = "nudCantidadPS";
            this.nudCantidadPS.Size = new System.Drawing.Size(95, 26);
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
            this.lbCantidad.Location = new System.Drawing.Point(4, 14);
            this.lbCantidad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCantidad.Name = "lbCantidad";
            this.lbCantidad.Size = new System.Drawing.Size(78, 20);
            this.lbCantidad.TabIndex = 39;
            this.lbCantidad.Text = "Cantidad";
            // 
            // btnTerminarVenta
            // 
            this.btnTerminarVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTerminarVenta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTerminarVenta.Location = new System.Drawing.Point(1440, 364);
            this.btnTerminarVenta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnTerminarVenta.Name = "btnTerminarVenta";
            this.btnTerminarVenta.Size = new System.Drawing.Size(100, 28);
            this.btnTerminarVenta.TabIndex = 37;
            this.btnTerminarVenta.Text = "Terminar";
            this.btnTerminarVenta.UseVisualStyleBackColor = true;
            this.btnTerminarVenta.Click += new System.EventHandler(this.btnTerminarVenta_Click);
            // 
            // cbEstadoVenta
            // 
            this.cbEstadoVenta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbEstadoVenta.FormattingEnabled = true;
            this.cbEstadoVenta.Items.AddRange(new object[] {
            "Pagada",
            "Pendiente por pagar",
            "Parcialmente pagada"});
            this.cbEstadoVenta.Location = new System.Drawing.Point(1205, 363);
            this.cbEstadoVenta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbEstadoVenta.Name = "cbEstadoVenta";
            this.cbEstadoVenta.Size = new System.Drawing.Size(212, 28);
            this.cbEstadoVenta.TabIndex = 36;
            // 
            // txtDescuentoGeneral
            // 
            this.txtDescuentoGeneral.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescuentoGeneral.Location = new System.Drawing.Point(1048, 364);
            this.txtDescuentoGeneral.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDescuentoGeneral.Name = "txtDescuentoGeneral";
            this.txtDescuentoGeneral.Size = new System.Drawing.Size(132, 26);
            this.txtDescuentoGeneral.TabIndex = 35;
            this.txtDescuentoGeneral.Text = "% descuento";
            this.txtDescuentoGeneral.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDescuentoGeneral_KeyUp);
            // 
            // btnDetallesVenta
            // 
            this.btnDetallesVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDetallesVenta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetallesVenta.Location = new System.Drawing.Point(925, 362);
            this.btnDetallesVenta.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDetallesVenta.Name = "btnDetallesVenta";
            this.btnDetallesVenta.Size = new System.Drawing.Size(100, 28);
            this.btnDetallesVenta.TabIndex = 34;
            this.btnDetallesVenta.Text = "Detalles";
            this.btnDetallesVenta.UseVisualStyleBackColor = true;
            // 
            // Ventas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1712, 752);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tituloSeccion);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MinimumSize = new System.Drawing.Size(1727, 789);
            this.Name = "Ventas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ventas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Ventas_FormClosing);
            this.Load += new System.EventHandler(this.Ventas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVentas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCancelarVenta;
        private System.Windows.Forms.Button btnGuardarVenta;
        private System.Windows.Forms.Button btnAnticipos;
        private System.Windows.Forms.Button button4;
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
        private System.Windows.Forms.ComboBox cbEstadoVenta;
        private System.Windows.Forms.TextBox txtDescuentoGeneral;
        private System.Windows.Forms.Button btnDetallesVenta;
        private System.Windows.Forms.Button btnTerminarVenta;
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
        private System.Windows.Forms.Label lbCantidad;
        private System.Windows.Forms.NumericUpDown nudCantidadPS;
        private System.Windows.Forms.Label lbPS;
    }
}