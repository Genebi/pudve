namespace PuntoDeVentaV2
{
    partial class AjustarProducto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AjustarProducto));
            this.lbProduct = new System.Windows.Forms.Label();
            this.rbProducto = new System.Windows.Forms.RadioButton();
            this.rbAjustar = new System.Windows.Forms.RadioButton();
            this.panelComprado = new System.Windows.Forms.Panel();
            this.btnActualiza = new System.Windows.Forms.Button();
            this.cantidadStockActual = new System.Windows.Forms.Label();
            this.lbCantidadCompra = new System.Windows.Forms.Label();
            this.lbPrecioCompra = new System.Windows.Forms.Label();
            this.lbStockActual = new System.Windows.Forms.Label();
            this.lbFechaCompra = new System.Windows.Forms.Label();
            this.lbProveedor = new System.Windows.Forms.Label();
            this.cbProveedores = new System.Windows.Forms.ComboBox();
            this.dpFechaCompra = new System.Windows.Forms.DateTimePicker();
            this.txtCantidadCompra = new System.Windows.Forms.TextBox();
            this.txtPrecioCompra = new System.Windows.Forms.TextBox();
            this.panelAjustar = new System.Windows.Forms.Panel();
            this.lbConcepto = new System.Windows.Forms.Label();
            this.btnAgregarConcepto = new System.Windows.Forms.Button();
            this.cbConceptos = new System.Windows.Forms.ComboBox();
            this.txt_en_stock = new System.Windows.Forms.TextBox();
            this.lb_en_stock = new System.Windows.Forms.Label();
            this.lb_disminuir_stock_total = new System.Windows.Forms.Label();
            this.lb_disminuir_stock = new System.Windows.Forms.Label();
            this.lb_aumentar_stock_total = new System.Windows.Forms.Label();
            this.lb_aumentar_stock = new System.Windows.Forms.Label();
            this.lbDisminuir = new System.Windows.Forms.Label();
            this.lbAumentar = new System.Windows.Forms.Label();
            this.txtDisminuir = new System.Windows.Forms.TextBox();
            this.txtAumentar = new System.Windows.Forms.TextBox();
            this.txtComentarios = new System.Windows.Forms.TextBox();
            this.lbComentarios = new System.Windows.Forms.Label();
            this.lbSeparador1 = new System.Windows.Forms.Label();
            this.lbSeparador2 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lbEditarPrecio = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.pnlOpcionesRadioButtons = new System.Windows.Forms.Panel();
            this.pnlMensajeOperacionInventario = new System.Windows.Forms.Panel();
            this.lblOperacionInventario = new System.Windows.Forms.Label();
            this.lbProducto = new System.Windows.Forms.TextBox();
            this.panelComprado.SuspendLayout();
            this.panelAjustar.SuspendLayout();
            this.pnlOpcionesRadioButtons.SuspendLayout();
            this.pnlMensajeOperacionInventario.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbProduct
            // 
            this.lbProduct.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProduct.Location = new System.Drawing.Point(13, 14);
            this.lbProduct.Name = "lbProduct";
            this.lbProduct.Size = new System.Drawing.Size(610, 28);
            this.lbProduct.TabIndex = 0;
            this.lbProduct.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lbProduct.Visible = false;
            // 
            // rbProducto
            // 
            this.rbProducto.AutoSize = true;
            this.rbProducto.Checked = true;
            this.rbProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbProducto.Location = new System.Drawing.Point(116, 6);
            this.rbProducto.Name = "rbProducto";
            this.rbProducto.Size = new System.Drawing.Size(133, 19);
            this.rbProducto.TabIndex = 1;
            this.rbProducto.TabStop = true;
            this.rbProducto.Text = "Producto comprado";
            this.rbProducto.UseVisualStyleBackColor = true;
            this.rbProducto.CheckedChanged += new System.EventHandler(this.rbProducto_CheckedChanged);
            // 
            // rbAjustar
            // 
            this.rbAjustar.AutoSize = true;
            this.rbAjustar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAjustar.Location = new System.Drawing.Point(341, 6);
            this.rbAjustar.Name = "rbAjustar";
            this.rbAjustar.Size = new System.Drawing.Size(90, 19);
            this.rbAjustar.TabIndex = 2;
            this.rbAjustar.Text = "Solo ajustar";
            this.rbAjustar.UseVisualStyleBackColor = true;
            this.rbAjustar.CheckedChanged += new System.EventHandler(this.rbAjustar_CheckedChanged);
            // 
            // panelComprado
            // 
            this.panelComprado.Controls.Add(this.cantidadStockActual);
            this.panelComprado.Controls.Add(this.lbCantidadCompra);
            this.panelComprado.Controls.Add(this.lbPrecioCompra);
            this.panelComprado.Controls.Add(this.lbStockActual);
            this.panelComprado.Controls.Add(this.lbFechaCompra);
            this.panelComprado.Controls.Add(this.lbProveedor);
            this.panelComprado.Controls.Add(this.cbProveedores);
            this.panelComprado.Controls.Add(this.dpFechaCompra);
            this.panelComprado.Controls.Add(this.txtCantidadCompra);
            this.panelComprado.Controls.Add(this.txtPrecioCompra);
            this.panelComprado.Location = new System.Drawing.Point(13, 139);
            this.panelComprado.Name = "panelComprado";
            this.panelComprado.Size = new System.Drawing.Size(610, 162);
            this.panelComprado.TabIndex = 3;
            // 
            // btnActualiza
            // 
            this.btnActualiza.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnActualiza.BackColor = System.Drawing.Color.DarkCyan;
            this.btnActualiza.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualiza.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnActualiza.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualiza.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualiza.ForeColor = System.Drawing.Color.White;
            this.btnActualiza.Location = new System.Drawing.Point(383, 43);
            this.btnActualiza.Name = "btnActualiza";
            this.btnActualiza.Size = new System.Drawing.Size(106, 28);
            this.btnActualiza.TabIndex = 33;
            this.btnActualiza.Text = "Actualizar";
            this.btnActualiza.UseVisualStyleBackColor = false;
            this.btnActualiza.Click += new System.EventHandler(this.btnActualiza_Click);
            // 
            // cantidadStockActual
            // 
            this.cantidadStockActual.BackColor = System.Drawing.Color.White;
            this.cantidadStockActual.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantidadStockActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cantidadStockActual.ForeColor = System.Drawing.Color.Red;
            this.cantidadStockActual.Location = new System.Drawing.Point(21, 87);
            this.cantidadStockActual.Name = "cantidadStockActual";
            this.cantidadStockActual.Size = new System.Drawing.Size(84, 22);
            this.cantidadStockActual.TabIndex = 11;
            this.cantidadStockActual.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCantidadCompra
            // 
            this.lbCantidadCompra.AutoSize = true;
            this.lbCantidadCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCantidadCompra.Location = new System.Drawing.Point(474, 64);
            this.lbCantidadCompra.Name = "lbCantidadCompra";
            this.lbCantidadCompra.Size = new System.Drawing.Size(101, 15);
            this.lbCantidadCompra.TabIndex = 9;
            this.lbCantidadCompra.Text = "Cantidad compra";
            // 
            // lbPrecioCompra
            // 
            this.lbPrecioCompra.AutoSize = true;
            this.lbPrecioCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrecioCompra.Location = new System.Drawing.Point(321, 64);
            this.lbPrecioCompra.Name = "lbPrecioCompra";
            this.lbPrecioCompra.Size = new System.Drawing.Size(104, 15);
            this.lbPrecioCompra.TabIndex = 8;
            this.lbPrecioCompra.Text = "Precio de compra";
            // 
            // lbStockActual
            // 
            this.lbStockActual.AutoSize = true;
            this.lbStockActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbStockActual.Location = new System.Drawing.Point(0, 0);
            this.lbStockActual.Name = "lbStockActual";
            this.lbStockActual.Size = new System.Drawing.Size(73, 15);
            this.lbStockActual.TabIndex = 10;
            this.lbStockActual.Text = "Stock actual";
            // 
            // lbFechaCompra
            // 
            this.lbFechaCompra.AutoSize = true;
            this.lbFechaCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFechaCompra.Location = new System.Drawing.Point(166, 64);
            this.lbFechaCompra.Name = "lbFechaCompra";
            this.lbFechaCompra.Size = new System.Drawing.Size(103, 15);
            this.lbFechaCompra.TabIndex = 7;
            this.lbFechaCompra.Text = "Fecha de compra";
            // 
            // lbProveedor
            // 
            this.lbProveedor.AutoSize = true;
            this.lbProveedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProveedor.Location = new System.Drawing.Point(4, 12);
            this.lbProveedor.Name = "lbProveedor";
            this.lbProveedor.Size = new System.Drawing.Size(63, 15);
            this.lbProveedor.TabIndex = 6;
            this.lbProveedor.Text = "Proveedor";
            // 
            // cbProveedores
            // 
            this.cbProveedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProveedores.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProveedores.FormattingEnabled = true;
            this.cbProveedores.Items.AddRange(new object[] {
            "Seleccione un proveedor..."});
            this.cbProveedores.Location = new System.Drawing.Point(7, 32);
            this.cbProveedores.Name = "cbProveedores";
            this.cbProveedores.Size = new System.Drawing.Size(595, 21);
            this.cbProveedores.TabIndex = 5;
            // 
            // dpFechaCompra
            // 
            this.dpFechaCompra.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpFechaCompra.CustomFormat = "yyyy-MM-dd";
            this.dpFechaCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpFechaCompra.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpFechaCompra.Location = new System.Drawing.Point(169, 87);
            this.dpFechaCompra.Name = "dpFechaCompra";
            this.dpFechaCompra.Size = new System.Drawing.Size(110, 21);
            this.dpFechaCompra.TabIndex = 4;
            // 
            // txtCantidadCompra
            // 
            this.txtCantidadCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadCompra.Location = new System.Drawing.Point(477, 87);
            this.txtCantidadCompra.MaxLength = 9;
            this.txtCantidadCompra.Name = "txtCantidadCompra";
            this.txtCantidadCompra.Size = new System.Drawing.Size(110, 21);
            this.txtCantidadCompra.TabIndex = 3;
            this.txtCantidadCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCantidadCompra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCantidadCompra_KeyDown);
            this.txtCantidadCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadCompra_KeyPress);
            // 
            // txtPrecioCompra
            // 
            this.txtPrecioCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecioCompra.Location = new System.Drawing.Point(324, 87);
            this.txtPrecioCompra.MaxLength = 9;
            this.txtPrecioCompra.Name = "txtPrecioCompra";
            this.txtPrecioCompra.Size = new System.Drawing.Size(108, 21);
            this.txtPrecioCompra.TabIndex = 2;
            this.txtPrecioCompra.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrecioCompra.Enter += new System.EventHandler(this.txtPrecioCompra_Enter);
            this.txtPrecioCompra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrecioCompra_KeyDown);
            this.txtPrecioCompra.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrecioCompra_KeyPress);
            // 
            // panelAjustar
            // 
            this.panelAjustar.Controls.Add(this.lbConcepto);
            this.panelAjustar.Controls.Add(this.btnAgregarConcepto);
            this.panelAjustar.Controls.Add(this.cbConceptos);
            this.panelAjustar.Controls.Add(this.txt_en_stock);
            this.panelAjustar.Controls.Add(this.lb_en_stock);
            this.panelAjustar.Controls.Add(this.lb_disminuir_stock_total);
            this.panelAjustar.Controls.Add(this.lb_disminuir_stock);
            this.panelAjustar.Controls.Add(this.lb_aumentar_stock_total);
            this.panelAjustar.Controls.Add(this.lb_aumentar_stock);
            this.panelAjustar.Controls.Add(this.lbDisminuir);
            this.panelAjustar.Controls.Add(this.lbAumentar);
            this.panelAjustar.Controls.Add(this.txtDisminuir);
            this.panelAjustar.Controls.Add(this.txtAumentar);
            this.panelAjustar.Location = new System.Drawing.Point(13, 139);
            this.panelAjustar.Name = "panelAjustar";
            this.panelAjustar.Size = new System.Drawing.Size(610, 162);
            this.panelAjustar.TabIndex = 4;
            this.panelAjustar.Visible = false;
            // 
            // lbConcepto
            // 
            this.lbConcepto.AutoSize = true;
            this.lbConcepto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConcepto.Location = new System.Drawing.Point(4, 105);
            this.lbConcepto.Name = "lbConcepto";
            this.lbConcepto.Size = new System.Drawing.Size(59, 15);
            this.lbConcepto.TabIndex = 224;
            this.lbConcepto.Text = "Concepto";
            // 
            // btnAgregarConcepto
            // 
            this.btnAgregarConcepto.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnAgregarConcepto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarConcepto.FlatAppearance.BorderSize = 0;
            this.btnAgregarConcepto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarConcepto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarConcepto.ForeColor = System.Drawing.Color.White;
            this.btnAgregarConcepto.Image = global::PuntoDeVentaV2.Properties.Resources.plus_square;
            this.btnAgregarConcepto.Location = new System.Drawing.Point(574, 125);
            this.btnAgregarConcepto.Name = "btnAgregarConcepto";
            this.btnAgregarConcepto.Size = new System.Drawing.Size(28, 25);
            this.btnAgregarConcepto.TabIndex = 223;
            this.btnAgregarConcepto.UseVisualStyleBackColor = false;
            this.btnAgregarConcepto.Click += new System.EventHandler(this.btnAgregarConcepto_Click);
            // 
            // cbConceptos
            // 
            this.cbConceptos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConceptos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbConceptos.FormattingEnabled = true;
            this.cbConceptos.Location = new System.Drawing.Point(7, 125);
            this.cbConceptos.Name = "cbConceptos";
            this.cbConceptos.Size = new System.Drawing.Size(563, 23);
            this.cbConceptos.TabIndex = 222;
            // 
            // txt_en_stock
            // 
            this.txt_en_stock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_en_stock.Enabled = false;
            this.txt_en_stock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_en_stock.Location = new System.Drawing.Point(75, 58);
            this.txt_en_stock.Name = "txt_en_stock";
            this.txt_en_stock.Size = new System.Drawing.Size(50, 21);
            this.txt_en_stock.TabIndex = 10;
            this.txt_en_stock.Text = "12";
            this.txt_en_stock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lb_en_stock
            // 
            this.lb_en_stock.AutoSize = true;
            this.lb_en_stock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_en_stock.Location = new System.Drawing.Point(72, 34);
            this.lb_en_stock.Name = "lb_en_stock";
            this.lb_en_stock.Size = new System.Drawing.Size(53, 15);
            this.lb_en_stock.TabIndex = 8;
            this.lb_en_stock.Text = "En stock";
            // 
            // lb_disminuir_stock_total
            // 
            this.lb_disminuir_stock_total.AutoSize = true;
            this.lb_disminuir_stock_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_disminuir_stock_total.ForeColor = System.Drawing.Color.Red;
            this.lb_disminuir_stock_total.Location = new System.Drawing.Point(485, 86);
            this.lb_disminuir_stock_total.Name = "lb_disminuir_stock_total";
            this.lb_disminuir_stock_total.Size = new System.Drawing.Size(15, 15);
            this.lb_disminuir_stock_total.TabIndex = 7;
            this.lb_disminuir_stock_total.Text = "0";
            this.lb_disminuir_stock_total.Visible = false;
            // 
            // lb_disminuir_stock
            // 
            this.lb_disminuir_stock.AutoSize = true;
            this.lb_disminuir_stock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_disminuir_stock.ForeColor = System.Drawing.Color.Red;
            this.lb_disminuir_stock.Location = new System.Drawing.Point(407, 86);
            this.lb_disminuir_stock.Name = "lb_disminuir_stock";
            this.lb_disminuir_stock.Size = new System.Drawing.Size(65, 15);
            this.lb_disminuir_stock.TabIndex = 6;
            this.lb_disminuir_stock.Text = "Total stock";
            this.lb_disminuir_stock.Visible = false;
            // 
            // lb_aumentar_stock_total
            // 
            this.lb_aumentar_stock_total.AutoSize = true;
            this.lb_aumentar_stock_total.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_aumentar_stock_total.ForeColor = System.Drawing.Color.Red;
            this.lb_aumentar_stock_total.Location = new System.Drawing.Point(269, 85);
            this.lb_aumentar_stock_total.Name = "lb_aumentar_stock_total";
            this.lb_aumentar_stock_total.Size = new System.Drawing.Size(15, 15);
            this.lb_aumentar_stock_total.TabIndex = 5;
            this.lb_aumentar_stock_total.Text = "0";
            this.lb_aumentar_stock_total.Visible = false;
            // 
            // lb_aumentar_stock
            // 
            this.lb_aumentar_stock.AutoSize = true;
            this.lb_aumentar_stock.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_aumentar_stock.ForeColor = System.Drawing.Color.Red;
            this.lb_aumentar_stock.Location = new System.Drawing.Point(191, 85);
            this.lb_aumentar_stock.Name = "lb_aumentar_stock";
            this.lb_aumentar_stock.Size = new System.Drawing.Size(65, 15);
            this.lb_aumentar_stock.TabIndex = 4;
            this.lb_aumentar_stock.Text = "Total stock";
            this.lb_aumentar_stock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lb_aumentar_stock.Visible = false;
            // 
            // lbDisminuir
            // 
            this.lbDisminuir.AutoSize = true;
            this.lbDisminuir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDisminuir.Location = new System.Drawing.Point(397, 34);
            this.lbDisminuir.Name = "lbDisminuir";
            this.lbDisminuir.Size = new System.Drawing.Size(110, 15);
            this.lbDisminuir.TabIndex = 3;
            this.lbDisminuir.Text = "Disminuir cantidad";
            // 
            // lbAumentar
            // 
            this.lbAumentar.AutoSize = true;
            this.lbAumentar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAumentar.Location = new System.Drawing.Point(181, 34);
            this.lbAumentar.Name = "lbAumentar";
            this.lbAumentar.Size = new System.Drawing.Size(110, 15);
            this.lbAumentar.TabIndex = 2;
            this.lbAumentar.Text = "Aumentar cantidad";
            // 
            // txtDisminuir
            // 
            this.txtDisminuir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDisminuir.Location = new System.Drawing.Point(400, 58);
            this.txtDisminuir.MaxLength = 10;
            this.txtDisminuir.Name = "txtDisminuir";
            this.txtDisminuir.Size = new System.Drawing.Size(125, 21);
            this.txtDisminuir.TabIndex = 1;
            this.txtDisminuir.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDisminuir.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDisminuir_KeyDown);
            this.txtDisminuir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDisminuir_KeyPress);
            this.txtDisminuir.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDisminuir_KeyUp);
            // 
            // txtAumentar
            // 
            this.txtAumentar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAumentar.Location = new System.Drawing.Point(184, 58);
            this.txtAumentar.MaxLength = 10;
            this.txtAumentar.Name = "txtAumentar";
            this.txtAumentar.Size = new System.Drawing.Size(125, 21);
            this.txtAumentar.TabIndex = 0;
            this.txtAumentar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAumentar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAumentar_KeyDown);
            this.txtAumentar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAumentar_KeyPress);
            this.txtAumentar.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtAumentar_KeyUp);
            // 
            // txtComentarios
            // 
            this.txtComentarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtComentarios.Location = new System.Drawing.Point(20, 328);
            this.txtComentarios.Name = "txtComentarios";
            this.txtComentarios.Size = new System.Drawing.Size(595, 21);
            this.txtComentarios.TabIndex = 5;
            // 
            // lbComentarios
            // 
            this.lbComentarios.AutoSize = true;
            this.lbComentarios.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbComentarios.Location = new System.Drawing.Point(17, 308);
            this.lbComentarios.Name = "lbComentarios";
            this.lbComentarios.Size = new System.Drawing.Size(135, 15);
            this.lbComentarios.TabIndex = 6;
            this.lbComentarios.Text = "Comentarios (opcional)";
            // 
            // lbSeparador1
            // 
            this.lbSeparador1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSeparador1.Location = new System.Drawing.Point(13, 74);
            this.lbSeparador1.Name = "lbSeparador1";
            this.lbSeparador1.Size = new System.Drawing.Size(610, 2);
            this.lbSeparador1.TabIndex = 19;
            // 
            // lbSeparador2
            // 
            this.lbSeparador2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSeparador2.Location = new System.Drawing.Point(12, 119);
            this.lbSeparador2.Name = "lbSeparador2";
            this.lbSeparador2.Size = new System.Drawing.Size(610, 2);
            this.lbSeparador2.TabIndex = 20;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.BackColor = System.Drawing.Color.Green;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(471, 369);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(144, 28);
            this.btnAceptar.TabIndex = 29;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(323, 369);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(144, 28);
            this.btnCancelar.TabIndex = 28;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lbEditarPrecio
            // 
            this.lbEditarPrecio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbEditarPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEditarPrecio.Image = global::PuntoDeVentaV2.Properties.Resources.pencil1;
            this.lbEditarPrecio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbEditarPrecio.Location = new System.Drawing.Point(363, 47);
            this.lbEditarPrecio.Name = "lbEditarPrecio";
            this.lbEditarPrecio.Size = new System.Drawing.Size(24, 23);
            this.lbEditarPrecio.TabIndex = 31;
            this.lbEditarPrecio.Click += new System.EventHandler(this.lbEditarPrecio_Click);
            // 
            // txtPrecio
            // 
            this.txtPrecio.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPrecio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecio.Location = new System.Drawing.Point(257, 47);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.ReadOnly = true;
            this.txtPrecio.Size = new System.Drawing.Size(100, 19);
            this.txtPrecio.TabIndex = 32;
            this.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrecio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrecio_KeyDown);
            this.txtPrecio.Leave += new System.EventHandler(this.txtPrecio_Leave);
            // 
            // pnlOpcionesRadioButtons
            // 
            this.pnlOpcionesRadioButtons.Controls.Add(this.rbAjustar);
            this.pnlOpcionesRadioButtons.Controls.Add(this.rbProducto);
            this.pnlOpcionesRadioButtons.Location = new System.Drawing.Point(13, 81);
            this.pnlOpcionesRadioButtons.Name = "pnlOpcionesRadioButtons";
            this.pnlOpcionesRadioButtons.Size = new System.Drawing.Size(610, 34);
            this.pnlOpcionesRadioButtons.TabIndex = 34;
            // 
            // pnlMensajeOperacionInventario
            // 
            this.pnlMensajeOperacionInventario.Controls.Add(this.lblOperacionInventario);
            this.pnlMensajeOperacionInventario.Location = new System.Drawing.Point(13, 81);
            this.pnlMensajeOperacionInventario.Name = "pnlMensajeOperacionInventario";
            this.pnlMensajeOperacionInventario.Size = new System.Drawing.Size(610, 34);
            this.pnlMensajeOperacionInventario.TabIndex = 35;
            // 
            // lblOperacionInventario
            // 
            this.lblOperacionInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperacionInventario.Location = new System.Drawing.Point(4, 5);
            this.lblOperacionInventario.Name = "lblOperacionInventario";
            this.lblOperacionInventario.Size = new System.Drawing.Size(603, 23);
            this.lblOperacionInventario.TabIndex = 0;
            this.lblOperacionInventario.Text = "label1";
            this.lblOperacionInventario.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbProducto
            // 
            this.lbProducto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProducto.Location = new System.Drawing.Point(13, 13);
            this.lbProducto.Name = "lbProducto";
            this.lbProducto.ReadOnly = true;
            this.lbProducto.Size = new System.Drawing.Size(609, 22);
            this.lbProducto.TabIndex = 36;
            this.lbProducto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // AjustarProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 412);
            this.Controls.Add(this.btnActualiza);
            this.Controls.Add(this.lbProducto);
            this.Controls.Add(this.pnlOpcionesRadioButtons);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.lbEditarPrecio);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lbSeparador2);
            this.Controls.Add(this.lbSeparador1);
            this.Controls.Add(this.lbComentarios);
            this.Controls.Add(this.txtComentarios);
            this.Controls.Add(this.lbProduct);
            this.Controls.Add(this.panelAjustar);
            this.Controls.Add(this.pnlMensajeOperacionInventario);
            this.Controls.Add(this.panelComprado);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AjustarProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Ajustar";
            this.Load += new System.EventHandler(this.AjustarProducto_Load);
            this.Shown += new System.EventHandler(this.AjustarProducto_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AjustarProducto_KeyDown);
            this.panelComprado.ResumeLayout(false);
            this.panelComprado.PerformLayout();
            this.panelAjustar.ResumeLayout(false);
            this.panelAjustar.PerformLayout();
            this.pnlOpcionesRadioButtons.ResumeLayout(false);
            this.pnlOpcionesRadioButtons.PerformLayout();
            this.pnlMensajeOperacionInventario.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbProduct;
        private System.Windows.Forms.RadioButton rbProducto;
        private System.Windows.Forms.RadioButton rbAjustar;
        private System.Windows.Forms.Panel panelComprado;
        private System.Windows.Forms.Panel panelAjustar;
        private System.Windows.Forms.TextBox txtComentarios;
        private System.Windows.Forms.Label lbComentarios;
        private System.Windows.Forms.Label lbCantidadCompra;
        private System.Windows.Forms.Label lbPrecioCompra;
        private System.Windows.Forms.Label lbFechaCompra;
        private System.Windows.Forms.Label lbProveedor;
        private System.Windows.Forms.ComboBox cbProveedores;
        private System.Windows.Forms.DateTimePicker dpFechaCompra;
        private System.Windows.Forms.TextBox txtCantidadCompra;
        private System.Windows.Forms.TextBox txtPrecioCompra;
        private System.Windows.Forms.Label lbDisminuir;
        private System.Windows.Forms.Label lbAumentar;
        private System.Windows.Forms.TextBox txtDisminuir;
        private System.Windows.Forms.TextBox txtAumentar;
        private System.Windows.Forms.Label lbSeparador1;
        private System.Windows.Forms.Label lbSeparador2;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lb_disminuir_stock_total;
        private System.Windows.Forms.Label lb_disminuir_stock;
        private System.Windows.Forms.Label lb_aumentar_stock;
        private System.Windows.Forms.Label lb_en_stock;
        private System.Windows.Forms.TextBox txt_en_stock;
        private System.Windows.Forms.Label lb_aumentar_stock_total;
        private System.Windows.Forms.Label lbStockActual;
        private System.Windows.Forms.Label cantidadStockActual;
        private System.Windows.Forms.Label lbEditarPrecio;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label lbConcepto;
        private System.Windows.Forms.Button btnAgregarConcepto;
        private System.Windows.Forms.ComboBox cbConceptos;
        private System.Windows.Forms.Button btnActualiza;
        private System.Windows.Forms.Panel pnlOpcionesRadioButtons;
        private System.Windows.Forms.Panel pnlMensajeOperacionInventario;
        private System.Windows.Forms.Label lblOperacionInventario;
        private System.Windows.Forms.TextBox lbProducto;
    }
}