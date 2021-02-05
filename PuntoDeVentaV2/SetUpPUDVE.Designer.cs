namespace PuntoDeVentaV2
{
    partial class SetUpPUDVE
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
            this.cbStockNegativo = new System.Windows.Forms.CheckBox();
            this.btnRespaldo = new System.Windows.Forms.Button();
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.guardarArchivo = new System.Windows.Forms.SaveFileDialog();
            this.txtNombreServidor = new System.Windows.Forms.TextBox();
            this.btnGuardarServidor = new System.Windows.Forms.Button();
            this.lbNombreServidor = new System.Windows.Forms.Label();
            this.btnGuardarRevision = new System.Windows.Forms.Button();
            this.txtNumeroRevision = new System.Windows.Forms.TextBox();
            this.lbNumeroRevision = new System.Windows.Forms.Label();
            this.checkCBVenta = new System.Windows.Forms.CheckBox();
            this.cbCorreoPrecioProducto = new System.Windows.Forms.CheckBox();
            this.cbCorreoStockProducto = new System.Windows.Forms.CheckBox();
            this.cbCorreoStockMinimo = new System.Windows.Forms.CheckBox();
            this.cbCorreoVenderProducto = new System.Windows.Forms.CheckBox();
            this.pagWeb = new System.Windows.Forms.CheckBox();
            this.cbMostrarPrecio = new System.Windows.Forms.CheckBox();
            this.cbMostrarCB = new System.Windows.Forms.CheckBox();
            this.btnGuardarPorcentaje = new System.Windows.Forms.Button();
            this.txtPorcentajeProducto = new System.Windows.Forms.TextBox();
            this.lbPorcentajeProducto = new System.Windows.Forms.Label();
            this.checkMayoreo = new System.Windows.Forms.CheckBox();
            this.txtMinimoMayoreo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkNoVendidos = new System.Windows.Forms.CheckBox();
            this.txtNoVendidos = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbCorreoCorteCaja = new System.Windows.Forms.CheckBox();
            this.cbCorreoEliminarListaProductosVentas = new System.Windows.Forms.CheckBox();
            this.cbCorreoEliminaUltimoProductoAgregadoVentas = new System.Windows.Forms.CheckBox();
            this.cbCorreoEliminarProductoVentas = new System.Windows.Forms.CheckBox();
            this.cbCorreoRestarProductosVenta = new System.Windows.Forms.CheckBox();
            this.cbCorreoCerrarVentanaVentas = new System.Windows.Forms.CheckBox();
            this.cbCorreoRetirarDineroCaja = new System.Windows.Forms.CheckBox();
            this.cbCorreoAgregarDineroCaja = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbStockNegativo
            // 
            this.cbStockNegativo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbStockNegativo.AutoSize = true;
            this.cbStockNegativo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStockNegativo.Location = new System.Drawing.Point(584, 193);
            this.cbStockNegativo.Name = "cbStockNegativo";
            this.cbStockNegativo.Size = new System.Drawing.Size(177, 21);
            this.cbStockNegativo.TabIndex = 1;
            this.cbStockNegativo.Text = "Permitir Stock negativo";
            this.cbStockNegativo.UseVisualStyleBackColor = true;
            this.cbStockNegativo.CheckedChanged += new System.EventHandler(this.cbStockNegativo_CheckedChanged);
            // 
            // btnRespaldo
            // 
            this.btnRespaldo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRespaldo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnRespaldo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRespaldo.FlatAppearance.BorderSize = 0;
            this.btnRespaldo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnRespaldo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnRespaldo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRespaldo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRespaldo.ForeColor = System.Drawing.Color.White;
            this.btnRespaldo.Location = new System.Drawing.Point(703, 103);
            this.btnRespaldo.Name = "btnRespaldo";
            this.btnRespaldo.Size = new System.Drawing.Size(190, 25);
            this.btnRespaldo.TabIndex = 101;
            this.btnRespaldo.Text = "Respaldar información";
            this.btnRespaldo.UseVisualStyleBackColor = false;
            this.btnRespaldo.Click += new System.EventHandler(this.btnRespaldo_Click);
            this.btnRespaldo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnRespaldo_KeyDown);
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(421, 27);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(194, 25);
            this.tituloSeccion.TabIndex = 102;
            this.tituloSeccion.Text = "CONFIGURACIÓN";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // guardarArchivo
            // 
            this.guardarArchivo.DefaultExt = "db";
            this.guardarArchivo.Title = "Respaldar Información";
            // 
            // txtNombreServidor
            // 
            this.txtNombreServidor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNombreServidor.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreServidor.Location = new System.Drawing.Point(48, 105);
            this.txtNombreServidor.Name = "txtNombreServidor";
            this.txtNombreServidor.Size = new System.Drawing.Size(190, 23);
            this.txtNombreServidor.TabIndex = 104;
            this.txtNombreServidor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreServidor.Visible = false;
            // 
            // btnGuardarServidor
            // 
            this.btnGuardarServidor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGuardarServidor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnGuardarServidor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarServidor.FlatAppearance.BorderSize = 0;
            this.btnGuardarServidor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnGuardarServidor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnGuardarServidor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarServidor.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarServidor.ForeColor = System.Drawing.Color.White;
            this.btnGuardarServidor.Location = new System.Drawing.Point(48, 143);
            this.btnGuardarServidor.Name = "btnGuardarServidor";
            this.btnGuardarServidor.Size = new System.Drawing.Size(190, 25);
            this.btnGuardarServidor.TabIndex = 105;
            this.btnGuardarServidor.Text = "Guardar";
            this.btnGuardarServidor.UseVisualStyleBackColor = false;
            this.btnGuardarServidor.Visible = false;
            this.btnGuardarServidor.Click += new System.EventHandler(this.btnGuardarServidor_Click);
            this.btnGuardarServidor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnGuardarServidor_KeyDown);
            // 
            // lbNombreServidor
            // 
            this.lbNombreServidor.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbNombreServidor.AutoSize = true;
            this.lbNombreServidor.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNombreServidor.Location = new System.Drawing.Point(45, 75);
            this.lbNombreServidor.Name = "lbNombreServidor";
            this.lbNombreServidor.Size = new System.Drawing.Size(158, 17);
            this.lbNombreServidor.TabIndex = 103;
            this.lbNombreServidor.Text = "Computadora Servidor";
            this.lbNombreServidor.Visible = false;
            // 
            // btnGuardarRevision
            // 
            this.btnGuardarRevision.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGuardarRevision.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnGuardarRevision.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarRevision.FlatAppearance.BorderSize = 0;
            this.btnGuardarRevision.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnGuardarRevision.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnGuardarRevision.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarRevision.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarRevision.ForeColor = System.Drawing.Color.White;
            this.btnGuardarRevision.Location = new System.Drawing.Point(266, 143);
            this.btnGuardarRevision.Name = "btnGuardarRevision";
            this.btnGuardarRevision.Size = new System.Drawing.Size(190, 25);
            this.btnGuardarRevision.TabIndex = 108;
            this.btnGuardarRevision.Text = "Guardar";
            this.btnGuardarRevision.UseVisualStyleBackColor = false;
            this.btnGuardarRevision.Visible = false;
            this.btnGuardarRevision.Click += new System.EventHandler(this.btnGuardarRevision_Click);
            this.btnGuardarRevision.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnGuardarRevision_KeyDown);
            // 
            // txtNumeroRevision
            // 
            this.txtNumeroRevision.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNumeroRevision.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroRevision.Location = new System.Drawing.Point(266, 105);
            this.txtNumeroRevision.Name = "txtNumeroRevision";
            this.txtNumeroRevision.Size = new System.Drawing.Size(190, 23);
            this.txtNumeroRevision.TabIndex = 107;
            this.txtNumeroRevision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNumeroRevision.Visible = false;
            // 
            // lbNumeroRevision
            // 
            this.lbNumeroRevision.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbNumeroRevision.AutoSize = true;
            this.lbNumeroRevision.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumeroRevision.Location = new System.Drawing.Point(263, 75);
            this.lbNumeroRevision.Name = "lbNumeroRevision";
            this.lbNumeroRevision.Size = new System.Drawing.Size(181, 17);
            this.lbNumeroRevision.TabIndex = 106;
            this.lbNumeroRevision.Text = "Número revisión inventario";
            this.lbNumeroRevision.Visible = false;
            // 
            // checkCBVenta
            // 
            this.checkCBVenta.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkCBVenta.AutoSize = true;
            this.checkCBVenta.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkCBVenta.Location = new System.Drawing.Point(584, 223);
            this.checkCBVenta.Name = "checkCBVenta";
            this.checkCBVenta.Size = new System.Drawing.Size(245, 21);
            this.checkCBVenta.TabIndex = 110;
            this.checkCBVenta.Text = "Código de barras ticket de venta";
            this.checkCBVenta.UseVisualStyleBackColor = true;
            this.checkCBVenta.CheckedChanged += new System.EventHandler(this.checkCBVenta_CheckedChanged);
            // 
            // cbCorreoPrecioProducto
            // 
            this.cbCorreoPrecioProducto.AutoSize = true;
            this.cbCorreoPrecioProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCorreoPrecioProducto.Location = new System.Drawing.Point(15, 32);
            this.cbCorreoPrecioProducto.Name = "cbCorreoPrecioProducto";
            this.cbCorreoPrecioProducto.Size = new System.Drawing.Size(150, 21);
            this.cbCorreoPrecioProducto.TabIndex = 111;
            this.cbCorreoPrecioProducto.Text = "Al modificar precio";
            this.cbCorreoPrecioProducto.UseVisualStyleBackColor = true;
            this.cbCorreoPrecioProducto.CheckedChanged += new System.EventHandler(this.cbCorreoPrecioProducto_CheckedChanged);
            // 
            // cbCorreoStockProducto
            // 
            this.cbCorreoStockProducto.AutoSize = true;
            this.cbCorreoStockProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCorreoStockProducto.Location = new System.Drawing.Point(15, 63);
            this.cbCorreoStockProducto.Name = "cbCorreoStockProducto";
            this.cbCorreoStockProducto.Size = new System.Drawing.Size(143, 21);
            this.cbCorreoStockProducto.TabIndex = 112;
            this.cbCorreoStockProducto.Text = "Al modificar stock";
            this.cbCorreoStockProducto.UseVisualStyleBackColor = true;
            this.cbCorreoStockProducto.CheckedChanged += new System.EventHandler(this.cbCorreoStockProducto_CheckedChanged);
            // 
            // cbCorreoStockMinimo
            // 
            this.cbCorreoStockMinimo.AutoSize = true;
            this.cbCorreoStockMinimo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCorreoStockMinimo.Location = new System.Drawing.Point(15, 90);
            this.cbCorreoStockMinimo.Name = "cbCorreoStockMinimo";
            this.cbCorreoStockMinimo.Size = new System.Drawing.Size(170, 21);
            this.cbCorreoStockMinimo.TabIndex = 113;
            this.cbCorreoStockMinimo.Text = "Al llegar stock minimo";
            this.cbCorreoStockMinimo.UseVisualStyleBackColor = true;
            this.cbCorreoStockMinimo.CheckedChanged += new System.EventHandler(this.cbCorreoStockMinimo_CheckedChanged);
            // 
            // cbCorreoVenderProducto
            // 
            this.cbCorreoVenderProducto.AutoSize = true;
            this.cbCorreoVenderProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCorreoVenderProducto.Location = new System.Drawing.Point(15, 124);
            this.cbCorreoVenderProducto.Name = "cbCorreoVenderProducto";
            this.cbCorreoVenderProducto.Size = new System.Drawing.Size(166, 21);
            this.cbCorreoVenderProducto.TabIndex = 114;
            this.cbCorreoVenderProducto.Text = "Al venderse producto";
            this.cbCorreoVenderProducto.UseVisualStyleBackColor = true;
            this.cbCorreoVenderProducto.CheckedChanged += new System.EventHandler(this.cbCorreoVenderProducto_CheckedChanged);
            // 
            // pagWeb
            // 
            this.pagWeb.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pagWeb.AutoSize = true;
            this.pagWeb.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pagWeb.Location = new System.Drawing.Point(584, 259);
            this.pagWeb.Name = "pagWeb";
            this.pagWeb.Size = new System.Drawing.Size(267, 21);
            this.pagWeb.TabIndex = 115;
            this.pagWeb.Text = "Habilitar información en página web";
            this.pagWeb.UseVisualStyleBackColor = true;
            this.pagWeb.CheckedChanged += new System.EventHandler(this.pagWeb_CheckedChanged);
            // 
            // cbMostrarPrecio
            // 
            this.cbMostrarPrecio.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbMostrarPrecio.AutoSize = true;
            this.cbMostrarPrecio.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMostrarPrecio.Location = new System.Drawing.Point(584, 296);
            this.cbMostrarPrecio.Name = "cbMostrarPrecio";
            this.cbMostrarPrecio.Size = new System.Drawing.Size(277, 21);
            this.cbMostrarPrecio.TabIndex = 116;
            this.cbMostrarPrecio.Text = "Mostrar precio de productos en ventas";
            this.cbMostrarPrecio.UseVisualStyleBackColor = true;
            this.cbMostrarPrecio.CheckedChanged += new System.EventHandler(this.cbMostrarPrecio_CheckedChanged);
            // 
            // cbMostrarCB
            // 
            this.cbMostrarCB.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbMostrarCB.AutoSize = true;
            this.cbMostrarCB.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMostrarCB.Location = new System.Drawing.Point(584, 333);
            this.cbMostrarCB.Name = "cbMostrarCB";
            this.cbMostrarCB.Size = new System.Drawing.Size(283, 21);
            this.cbMostrarCB.TabIndex = 117;
            this.cbMostrarCB.Text = "Mostrar código de productos en ventas";
            this.cbMostrarCB.UseVisualStyleBackColor = true;
            this.cbMostrarCB.CheckedChanged += new System.EventHandler(this.cbMostrarCB_CheckedChanged);
            // 
            // btnGuardarPorcentaje
            // 
            this.btnGuardarPorcentaje.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGuardarPorcentaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnGuardarPorcentaje.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarPorcentaje.FlatAppearance.BorderSize = 0;
            this.btnGuardarPorcentaje.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnGuardarPorcentaje.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnGuardarPorcentaje.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarPorcentaje.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarPorcentaje.ForeColor = System.Drawing.Color.White;
            this.btnGuardarPorcentaje.Location = new System.Drawing.Point(483, 143);
            this.btnGuardarPorcentaje.Name = "btnGuardarPorcentaje";
            this.btnGuardarPorcentaje.Size = new System.Drawing.Size(190, 25);
            this.btnGuardarPorcentaje.TabIndex = 120;
            this.btnGuardarPorcentaje.Text = "Guardar";
            this.btnGuardarPorcentaje.UseVisualStyleBackColor = false;
            this.btnGuardarPorcentaje.Click += new System.EventHandler(this.btnGuardarPorcentaje_Click);
            this.btnGuardarPorcentaje.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnGuardarPorcentaje_KeyDown);
            // 
            // txtPorcentajeProducto
            // 
            this.txtPorcentajeProducto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtPorcentajeProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentajeProducto.Location = new System.Drawing.Point(483, 105);
            this.txtPorcentajeProducto.Name = "txtPorcentajeProducto";
            this.txtPorcentajeProducto.Size = new System.Drawing.Size(190, 23);
            this.txtPorcentajeProducto.TabIndex = 119;
            this.txtPorcentajeProducto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbPorcentajeProducto
            // 
            this.lbPorcentajeProducto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbPorcentajeProducto.AutoSize = true;
            this.lbPorcentajeProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPorcentajeProducto.Location = new System.Drawing.Point(480, 75);
            this.lbPorcentajeProducto.Name = "lbPorcentajeProducto";
            this.lbPorcentajeProducto.Size = new System.Drawing.Size(180, 17);
            this.lbPorcentajeProducto.TabIndex = 118;
            this.lbPorcentajeProducto.Text = "Porcentaje % de ganancia";
            // 
            // checkMayoreo
            // 
            this.checkMayoreo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkMayoreo.AutoSize = true;
            this.checkMayoreo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkMayoreo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.checkMayoreo.Location = new System.Drawing.Point(584, 370);
            this.checkMayoreo.Name = "checkMayoreo";
            this.checkMayoreo.Size = new System.Drawing.Size(273, 21);
            this.checkMayoreo.TabIndex = 121;
            this.checkMayoreo.Text = "Activar precio por mayoreo en ventas";
            this.checkMayoreo.UseVisualStyleBackColor = true;
            this.checkMayoreo.CheckedChanged += new System.EventHandler(this.checkMayoreo_CheckedChanged);
            this.checkMayoreo.Click += new System.EventHandler(this.checkMayoreo_Click);
            // 
            // txtMinimoMayoreo
            // 
            this.txtMinimoMayoreo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtMinimoMayoreo.Enabled = false;
            this.txtMinimoMayoreo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinimoMayoreo.Location = new System.Drawing.Point(736, 391);
            this.txtMinimoMayoreo.Name = "txtMinimoMayoreo";
            this.txtMinimoMayoreo.Size = new System.Drawing.Size(69, 21);
            this.txtMinimoMayoreo.TabIndex = 122;
            this.txtMinimoMayoreo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMinimoMayoreo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMinimoMayoreo_KeyUp);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(598, 393);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 17);
            this.label1.TabIndex = 123;
            this.label1.Text = "- Cantidad mínima";
            // 
            // checkNoVendidos
            // 
            this.checkNoVendidos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.checkNoVendidos.AutoSize = true;
            this.checkNoVendidos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkNoVendidos.ForeColor = System.Drawing.SystemColors.Highlight;
            this.checkNoVendidos.Location = new System.Drawing.Point(584, 421);
            this.checkNoVendidos.Name = "checkNoVendidos";
            this.checkNoVendidos.Size = new System.Drawing.Size(240, 21);
            this.checkNoVendidos.TabIndex = 124;
            this.checkNoVendidos.Text = "Avisar de productos no vendidos";
            this.checkNoVendidos.UseVisualStyleBackColor = true;
            this.checkNoVendidos.CheckedChanged += new System.EventHandler(this.checkNoVendidos_CheckedChanged);
            this.checkNoVendidos.Click += new System.EventHandler(this.checkNoVendidos_Click);
            // 
            // txtNoVendidos
            // 
            this.txtNoVendidos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtNoVendidos.Enabled = false;
            this.txtNoVendidos.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoVendidos.Location = new System.Drawing.Point(658, 441);
            this.txtNoVendidos.Name = "txtNoVendidos";
            this.txtNoVendidos.Size = new System.Drawing.Size(69, 21);
            this.txtNoVendidos.TabIndex = 125;
            this.txtNoVendidos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNoVendidos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNoVendidos_KeyUp);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(598, 443);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 126;
            this.label2.Text = "- Cada";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(733, 443);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 17);
            this.label3.TabIndex = 127;
            this.label3.Text = "días";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.groupBox1.Controls.Add(this.cbCorreoCorteCaja);
            this.groupBox1.Controls.Add(this.cbCorreoEliminarListaProductosVentas);
            this.groupBox1.Controls.Add(this.cbCorreoEliminaUltimoProductoAgregadoVentas);
            this.groupBox1.Controls.Add(this.cbCorreoEliminarProductoVentas);
            this.groupBox1.Controls.Add(this.cbCorreoRestarProductosVenta);
            this.groupBox1.Controls.Add(this.cbCorreoCerrarVentanaVentas);
            this.groupBox1.Controls.Add(this.cbCorreoRetirarDineroCaja);
            this.groupBox1.Controls.Add(this.cbCorreoAgregarDineroCaja);
            this.groupBox1.Controls.Add(this.cbCorreoPrecioProducto);
            this.groupBox1.Controls.Add(this.cbCorreoStockProducto);
            this.groupBox1.Controls.Add(this.cbCorreoStockMinimo);
            this.groupBox1.Controls.Add(this.cbCorreoVenderProducto);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(37, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(510, 239);
            this.groupBox1.TabIndex = 128;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Envio de Correo:";
            // 
            // cbCorreoCorteCaja
            // 
            this.cbCorreoCorteCaja.AutoSize = true;
            this.cbCorreoCorteCaja.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoCorteCaja.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cbCorreoCorteCaja.Location = new System.Drawing.Point(219, 203);
            this.cbCorreoCorteCaja.Name = "cbCorreoCorteCaja";
            this.cbCorreoCorteCaja.Size = new System.Drawing.Size(172, 21);
            this.cbCorreoCorteCaja.TabIndex = 121;
            this.cbCorreoCorteCaja.Text = "Al hacer corte de caja";
            this.cbCorreoCorteCaja.UseVisualStyleBackColor = true;
            this.cbCorreoCorteCaja.CheckedChanged += new System.EventHandler(this.cbCorreoCorteCaja_CheckedChanged);
            // 
            // cbCorreoEliminarListaProductosVentas
            // 
            this.cbCorreoEliminarListaProductosVentas.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoEliminarListaProductosVentas.Location = new System.Drawing.Point(219, 156);
            this.cbCorreoEliminarListaProductosVentas.Name = "cbCorreoEliminarListaProductosVentas";
            this.cbCorreoEliminarListaProductosVentas.Size = new System.Drawing.Size(283, 43);
            this.cbCorreoEliminarListaProductosVentas.TabIndex = 120;
            this.cbCorreoEliminarListaProductosVentas.Text = "Al eliminar listado de productos agregados de ventas";
            this.cbCorreoEliminarListaProductosVentas.UseVisualStyleBackColor = true;
            this.cbCorreoEliminarListaProductosVentas.CheckedChanged += new System.EventHandler(this.cbCorreoEliminarListaProductosVentas_CheckedChanged);
            // 
            // cbCorreoEliminaUltimoProductoAgregadoVentas
            // 
            this.cbCorreoEliminaUltimoProductoAgregadoVentas.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoEliminaUltimoProductoAgregadoVentas.Location = new System.Drawing.Point(219, 113);
            this.cbCorreoEliminaUltimoProductoAgregadoVentas.Name = "cbCorreoEliminaUltimoProductoAgregadoVentas";
            this.cbCorreoEliminaUltimoProductoAgregadoVentas.Size = new System.Drawing.Size(283, 42);
            this.cbCorreoEliminaUltimoProductoAgregadoVentas.TabIndex = 119;
            this.cbCorreoEliminaUltimoProductoAgregadoVentas.Text = "Al eliminar ultimo producto agregado de ventas";
            this.cbCorreoEliminaUltimoProductoAgregadoVentas.UseVisualStyleBackColor = true;
            this.cbCorreoEliminaUltimoProductoAgregadoVentas.CheckedChanged += new System.EventHandler(this.cbCorreoEliminaUltimoProductoAgregadoVentas_CheckedChanged);
            // 
            // cbCorreoEliminarProductoVentas
            // 
            this.cbCorreoEliminarProductoVentas.AutoSize = true;
            this.cbCorreoEliminarProductoVentas.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoEliminarProductoVentas.Location = new System.Drawing.Point(219, 90);
            this.cbCorreoEliminarProductoVentas.Name = "cbCorreoEliminarProductoVentas";
            this.cbCorreoEliminarProductoVentas.Size = new System.Drawing.Size(227, 21);
            this.cbCorreoEliminarProductoVentas.TabIndex = 118;
            this.cbCorreoEliminarProductoVentas.Text = "Al eliminar producto de ventas";
            this.cbCorreoEliminarProductoVentas.UseVisualStyleBackColor = true;
            this.cbCorreoEliminarProductoVentas.CheckedChanged += new System.EventHandler(this.cbCorreoEliminarProductoVentas_CheckedChanged);
            // 
            // cbCorreoRestarProductosVenta
            // 
            this.cbCorreoRestarProductosVenta.AutoSize = true;
            this.cbCorreoRestarProductosVenta.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoRestarProductosVenta.Location = new System.Drawing.Point(219, 63);
            this.cbCorreoRestarProductosVenta.Name = "cbCorreoRestarProductosVenta";
            this.cbCorreoRestarProductosVenta.Size = new System.Drawing.Size(211, 21);
            this.cbCorreoRestarProductosVenta.TabIndex = 117;
            this.cbCorreoRestarProductosVenta.Text = "Al restar producto de ventas";
            this.cbCorreoRestarProductosVenta.UseVisualStyleBackColor = true;
            this.cbCorreoRestarProductosVenta.CheckedChanged += new System.EventHandler(this.cbCorreoRestarProductosVenta_CheckedChanged);
            // 
            // cbCorreoCerrarVentanaVentas
            // 
            this.cbCorreoCerrarVentanaVentas.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoCerrarVentanaVentas.Location = new System.Drawing.Point(219, 22);
            this.cbCorreoCerrarVentanaVentas.Name = "cbCorreoCerrarVentanaVentas";
            this.cbCorreoCerrarVentanaVentas.Size = new System.Drawing.Size(283, 41);
            this.cbCorreoCerrarVentanaVentas.TabIndex = 116;
            this.cbCorreoCerrarVentanaVentas.Text = "Al cerrar la ventana de ventas cuando tiene productos";
            this.cbCorreoCerrarVentanaVentas.UseVisualStyleBackColor = true;
            this.cbCorreoCerrarVentanaVentas.CheckedChanged += new System.EventHandler(this.cbCorreoCerrarVentanaVentas_CheckedChanged);
            // 
            // cbCorreoRetirarDineroCaja
            // 
            this.cbCorreoRetirarDineroCaja.AutoSize = true;
            this.cbCorreoRetirarDineroCaja.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoRetirarDineroCaja.Location = new System.Drawing.Point(15, 203);
            this.cbCorreoRetirarDineroCaja.Name = "cbCorreoRetirarDineroCaja";
            this.cbCorreoRetirarDineroCaja.Size = new System.Drawing.Size(178, 21);
            this.cbCorreoRetirarDineroCaja.TabIndex = 115;
            this.cbCorreoRetirarDineroCaja.Text = "Al retirar dinero en caja";
            this.cbCorreoRetirarDineroCaja.UseVisualStyleBackColor = true;
            this.cbCorreoRetirarDineroCaja.CheckedChanged += new System.EventHandler(this.cbCorreoRetirarDineroCaja_CheckedChanged);
            // 
            // cbCorreoAgregarDineroCaja
            // 
            this.cbCorreoAgregarDineroCaja.AutoSize = true;
            this.cbCorreoAgregarDineroCaja.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoAgregarDineroCaja.Location = new System.Drawing.Point(15, 167);
            this.cbCorreoAgregarDineroCaja.Name = "cbCorreoAgregarDineroCaja";
            this.cbCorreoAgregarDineroCaja.Size = new System.Drawing.Size(193, 21);
            this.cbCorreoAgregarDineroCaja.TabIndex = 0;
            this.cbCorreoAgregarDineroCaja.Text = "Al agregar dinero en caja";
            this.cbCorreoAgregarDineroCaja.UseVisualStyleBackColor = true;
            this.cbCorreoAgregarDineroCaja.CheckedChanged += new System.EventHandler(this.cbCorreoAgregarDineroCaja_CheckedChanged);
            // 
            // SetUpPUDVE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 561);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNoVendidos);
            this.Controls.Add(this.checkNoVendidos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMinimoMayoreo);
            this.Controls.Add(this.checkMayoreo);
            this.Controls.Add(this.btnGuardarPorcentaje);
            this.Controls.Add(this.txtPorcentajeProducto);
            this.Controls.Add(this.lbPorcentajeProducto);
            this.Controls.Add(this.cbMostrarCB);
            this.Controls.Add(this.cbMostrarPrecio);
            this.Controls.Add(this.pagWeb);
            this.Controls.Add(this.checkCBVenta);
            this.Controls.Add(this.btnGuardarRevision);
            this.Controls.Add(this.txtNumeroRevision);
            this.Controls.Add(this.lbNumeroRevision);
            this.Controls.Add(this.btnGuardarServidor);
            this.Controls.Add(this.txtNombreServidor);
            this.Controls.Add(this.lbNombreServidor);
            this.Controls.Add(this.tituloSeccion);
            this.Controls.Add(this.btnRespaldo);
            this.Controls.Add(this.cbStockNegativo);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.KeyPreview = true;
            this.Name = "SetUpPUDVE";
            this.Text = "PUDVE - Configuración";
            this.Load += new System.EventHandler(this.SetUpPUDVE_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SetUpPUDVE_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SetUpPUDVE_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.SetUpPUDVE_PreviewKeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cbStockNegativo;
        private System.Windows.Forms.Button btnRespaldo;
        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.SaveFileDialog guardarArchivo;
        private System.Windows.Forms.TextBox txtNombreServidor;
        private System.Windows.Forms.Button btnGuardarServidor;
        private System.Windows.Forms.Label lbNombreServidor;
        private System.Windows.Forms.Button btnGuardarRevision;
        private System.Windows.Forms.TextBox txtNumeroRevision;
        private System.Windows.Forms.Label lbNumeroRevision;
        private System.Windows.Forms.CheckBox checkCBVenta;
        private System.Windows.Forms.CheckBox cbCorreoPrecioProducto;
        private System.Windows.Forms.CheckBox cbCorreoStockProducto;
        private System.Windows.Forms.CheckBox cbCorreoStockMinimo;
        private System.Windows.Forms.CheckBox cbCorreoVenderProducto;
        private System.Windows.Forms.CheckBox pagWeb;
        private System.Windows.Forms.CheckBox cbMostrarPrecio;
        private System.Windows.Forms.CheckBox cbMostrarCB;
        private System.Windows.Forms.Button btnGuardarPorcentaje;
        private System.Windows.Forms.TextBox txtPorcentajeProducto;
        private System.Windows.Forms.Label lbPorcentajeProducto;
        private System.Windows.Forms.CheckBox checkMayoreo;
        private System.Windows.Forms.TextBox txtMinimoMayoreo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkNoVendidos;
        private System.Windows.Forms.TextBox txtNoVendidos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbCorreoAgregarDineroCaja;
        private System.Windows.Forms.CheckBox cbCorreoRetirarDineroCaja;
        private System.Windows.Forms.CheckBox cbCorreoCerrarVentanaVentas;
        private System.Windows.Forms.CheckBox cbCorreoRestarProductosVenta;
        private System.Windows.Forms.CheckBox cbCorreoEliminarProductoVentas;
        private System.Windows.Forms.CheckBox cbCorreoEliminaUltimoProductoAgregadoVentas;
        private System.Windows.Forms.CheckBox cbCorreoEliminarListaProductosVentas;
        private System.Windows.Forms.CheckBox cbCorreoCorteCaja;
    }
}