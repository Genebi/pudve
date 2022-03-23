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
            this.btnRespaldo = new System.Windows.Forms.Button();
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.guardarArchivo = new System.Windows.Forms.SaveFileDialog();
            this.txtNombreServidor = new System.Windows.Forms.TextBox();
            this.btnGuardarServidor = new System.Windows.Forms.Button();
            this.lbNombreServidor = new System.Windows.Forms.Label();
            this.btnGuardarRevision = new System.Windows.Forms.Button();
            this.txtNumeroRevision = new System.Windows.Forms.TextBox();
            this.lbNumeroRevision = new System.Windows.Forms.Label();
            this.cbCorreoPrecioProducto = new System.Windows.Forms.CheckBox();
            this.cbCorreoStockProducto = new System.Windows.Forms.CheckBox();
            this.cbCorreoStockMinimo = new System.Windows.Forms.CheckBox();
            this.cbCorreoVenderProducto = new System.Windows.Forms.CheckBox();
            this.btnGuardarPorcentaje = new System.Windows.Forms.Button();
            this.txtPorcentajeProducto = new System.Windows.Forms.TextBox();
            this.lbPorcentajeProducto = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chRespaldo = new System.Windows.Forms.CheckBox();
            this.cbCorreoDescuento = new System.Windows.Forms.CheckBox();
            this.cbCorreoIniciar = new System.Windows.Forms.CheckBox();
            this.cbCorreoVenta = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCorreoCorteCaja = new System.Windows.Forms.CheckBox();
            this.cbCorreoEliminarListaProductosVentas = new System.Windows.Forms.CheckBox();
            this.cbCorreoCerrarVentanaVentas = new System.Windows.Forms.CheckBox();
            this.cbCorreoRetirarDineroCaja = new System.Windows.Forms.CheckBox();
            this.cbCorreoAgregarDineroCaja = new System.Windows.Forms.CheckBox();
            this.cboTipoMoneda = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtMinimoMayoreo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkMayoreo = new System.Windows.Forms.CheckBox();
            this.checkNoVendidos = new System.Windows.Forms.CheckBox();
            this.cbStockNegativo = new System.Windows.Forms.CheckBox();
            this.txtNoVendidos = new System.Windows.Forms.TextBox();
            this.cbMostrarPrecio = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbMostrarCB = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pagWeb = new System.Windows.Forms.CheckBox();
            this.checkCBVenta = new System.Windows.Forms.CheckBox();
            this.chTicketVentas = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnConfiguracionGeneral = new PuntoDeVentaV2.BotonRedondo();
            this.botonRedondo4 = new PuntoDeVentaV2.BotonRedondo();
            this.botonRedondo1 = new PuntoDeVentaV2.BotonRedondo();
            this.btnRespaldarInformacion = new PuntoDeVentaV2.BotonRedondo();
            this.botonRedondo5 = new PuntoDeVentaV2.BotonRedondo();
            this.btnEnvioCorreo = new PuntoDeVentaV2.BotonRedondo();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRespaldo
            // 
            this.btnRespaldo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRespaldo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnRespaldo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRespaldo.FlatAppearance.BorderSize = 0;
            this.btnRespaldo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnRespaldo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnRespaldo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRespaldo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRespaldo.ForeColor = System.Drawing.Color.White;
            this.btnRespaldo.Location = new System.Drawing.Point(94, 172);
            this.btnRespaldo.Name = "btnRespaldo";
            this.btnRespaldo.Size = new System.Drawing.Size(190, 25);
            this.btnRespaldo.TabIndex = 101;
            this.btnRespaldo.Text = "Respaldar información";
            this.btnRespaldo.UseVisualStyleBackColor = false;
            this.btnRespaldo.Visible = false;
            this.btnRespaldo.Click += new System.EventHandler(this.btnRespaldo_Click);
            this.btnRespaldo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnRespaldo_KeyDown);
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.txtNombreServidor.Location = new System.Drawing.Point(765, 669);
            this.txtNombreServidor.Name = "txtNombreServidor";
            this.txtNombreServidor.Size = new System.Drawing.Size(60, 23);
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
            this.btnGuardarServidor.Location = new System.Drawing.Point(829, 667);
            this.btnGuardarServidor.Name = "btnGuardarServidor";
            this.btnGuardarServidor.Size = new System.Drawing.Size(73, 25);
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
            this.lbNombreServidor.Location = new System.Drawing.Point(780, 638);
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
            this.btnGuardarRevision.Location = new System.Drawing.Point(648, 669);
            this.btnGuardarRevision.Name = "btnGuardarRevision";
            this.btnGuardarRevision.Size = new System.Drawing.Size(91, 25);
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
            this.txtNumeroRevision.Location = new System.Drawing.Point(569, 667);
            this.txtNumeroRevision.Name = "txtNumeroRevision";
            this.txtNumeroRevision.Size = new System.Drawing.Size(73, 23);
            this.txtNumeroRevision.TabIndex = 107;
            this.txtNumeroRevision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNumeroRevision.Visible = false;
            // 
            // lbNumeroRevision
            // 
            this.lbNumeroRevision.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbNumeroRevision.AutoSize = true;
            this.lbNumeroRevision.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumeroRevision.Location = new System.Drawing.Point(382, 667);
            this.lbNumeroRevision.Name = "lbNumeroRevision";
            this.lbNumeroRevision.Size = new System.Drawing.Size(181, 17);
            this.lbNumeroRevision.TabIndex = 106;
            this.lbNumeroRevision.Text = "Número revisión inventario";
            this.lbNumeroRevision.Visible = false;
            // 
            // cbCorreoPrecioProducto
            // 
            this.cbCorreoPrecioProducto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbCorreoPrecioProducto.AutoSize = true;
            this.cbCorreoPrecioProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCorreoPrecioProducto.Location = new System.Drawing.Point(270, 61);
            this.cbCorreoPrecioProducto.Name = "cbCorreoPrecioProducto";
            this.cbCorreoPrecioProducto.Size = new System.Drawing.Size(150, 21);
            this.cbCorreoPrecioProducto.TabIndex = 111;
            this.cbCorreoPrecioProducto.Text = "Al modificar precio";
            this.cbCorreoPrecioProducto.UseVisualStyleBackColor = true;
            this.cbCorreoPrecioProducto.CheckedChanged += new System.EventHandler(this.cbCorreoPrecioProducto_CheckedChanged);
            // 
            // cbCorreoStockProducto
            // 
            this.cbCorreoStockProducto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbCorreoStockProducto.AutoSize = true;
            this.cbCorreoStockProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCorreoStockProducto.Location = new System.Drawing.Point(270, 88);
            this.cbCorreoStockProducto.Name = "cbCorreoStockProducto";
            this.cbCorreoStockProducto.Size = new System.Drawing.Size(143, 21);
            this.cbCorreoStockProducto.TabIndex = 112;
            this.cbCorreoStockProducto.Text = "Al modificar stock";
            this.cbCorreoStockProducto.UseVisualStyleBackColor = true;
            this.cbCorreoStockProducto.CheckedChanged += new System.EventHandler(this.cbCorreoStockProducto_CheckedChanged);
            // 
            // cbCorreoStockMinimo
            // 
            this.cbCorreoStockMinimo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbCorreoStockMinimo.AutoSize = true;
            this.cbCorreoStockMinimo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCorreoStockMinimo.Location = new System.Drawing.Point(270, 34);
            this.cbCorreoStockMinimo.Name = "cbCorreoStockMinimo";
            this.cbCorreoStockMinimo.Size = new System.Drawing.Size(170, 21);
            this.cbCorreoStockMinimo.TabIndex = 113;
            this.cbCorreoStockMinimo.Text = "Al llegar stock mínimo";
            this.cbCorreoStockMinimo.UseVisualStyleBackColor = true;
            this.cbCorreoStockMinimo.CheckedChanged += new System.EventHandler(this.cbCorreoStockMinimo_CheckedChanged);
            // 
            // cbCorreoVenderProducto
            // 
            this.cbCorreoVenderProducto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbCorreoVenderProducto.AutoSize = true;
            this.cbCorreoVenderProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCorreoVenderProducto.Location = new System.Drawing.Point(270, 113);
            this.cbCorreoVenderProducto.Name = "cbCorreoVenderProducto";
            this.cbCorreoVenderProducto.Size = new System.Drawing.Size(166, 21);
            this.cbCorreoVenderProducto.TabIndex = 114;
            this.cbCorreoVenderProducto.Text = "Al venderse producto";
            this.cbCorreoVenderProducto.UseVisualStyleBackColor = true;
            this.cbCorreoVenderProducto.CheckedChanged += new System.EventHandler(this.cbCorreoVenderProducto_CheckedChanged);
            // 
            // btnGuardarPorcentaje
            // 
            this.btnGuardarPorcentaje.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnGuardarPorcentaje.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarPorcentaje.FlatAppearance.BorderSize = 0;
            this.btnGuardarPorcentaje.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnGuardarPorcentaje.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnGuardarPorcentaje.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuardarPorcentaje.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarPorcentaje.ForeColor = System.Drawing.Color.Black;
            this.btnGuardarPorcentaje.Location = new System.Drawing.Point(870, 545);
            this.btnGuardarPorcentaje.Name = "btnGuardarPorcentaje";
            this.btnGuardarPorcentaje.Size = new System.Drawing.Size(90, 25);
            this.btnGuardarPorcentaje.TabIndex = 120;
            this.btnGuardarPorcentaje.Text = "Guardar";
            this.btnGuardarPorcentaje.UseVisualStyleBackColor = false;
            this.btnGuardarPorcentaje.Visible = false;
            this.btnGuardarPorcentaje.Click += new System.EventHandler(this.btnGuardarPorcentaje_Click);
            this.btnGuardarPorcentaje.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnGuardarPorcentaje_KeyDown);
            // 
            // txtPorcentajeProducto
            // 
            this.txtPorcentajeProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentajeProducto.Location = new System.Drawing.Point(674, 545);
            this.txtPorcentajeProducto.Name = "txtPorcentajeProducto";
            this.txtPorcentajeProducto.Size = new System.Drawing.Size(190, 23);
            this.txtPorcentajeProducto.TabIndex = 119;
            this.txtPorcentajeProducto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPorcentajeProducto.Visible = false;
            // 
            // lbPorcentajeProducto
            // 
            this.lbPorcentajeProducto.AutoSize = true;
            this.lbPorcentajeProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPorcentajeProducto.Location = new System.Drawing.Point(474, 549);
            this.lbPorcentajeProducto.Name = "lbPorcentajeProducto";
            this.lbPorcentajeProducto.Size = new System.Drawing.Size(180, 17);
            this.lbPorcentajeProducto.TabIndex = 118;
            this.lbPorcentajeProducto.Text = "Porcentaje % de ganancia";
            this.lbPorcentajeProducto.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.groupBox1.Controls.Add(this.chRespaldo);
            this.groupBox1.Controls.Add(this.cbCorreoDescuento);
            this.groupBox1.Controls.Add(this.cbCorreoIniciar);
            this.groupBox1.Controls.Add(this.cbCorreoVenta);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbCorreoCorteCaja);
            this.groupBox1.Controls.Add(this.cbCorreoEliminarListaProductosVentas);
            this.groupBox1.Controls.Add(this.cbCorreoCerrarVentanaVentas);
            this.groupBox1.Controls.Add(this.cbCorreoRetirarDineroCaja);
            this.groupBox1.Controls.Add(this.cbCorreoAgregarDineroCaja);
            this.groupBox1.Controls.Add(this.cbCorreoPrecioProducto);
            this.groupBox1.Controls.Add(this.cbCorreoStockProducto);
            this.groupBox1.Controls.Add(this.cbCorreoStockMinimo);
            this.groupBox1.Controls.Add(this.cbCorreoVenderProducto);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(44, 339);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(930, 200);
            this.groupBox1.TabIndex = 128;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Envío de Correo:";
            this.groupBox1.Visible = false;
            // 
            // chRespaldo
            // 
            this.chRespaldo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chRespaldo.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.chRespaldo.Location = new System.Drawing.Point(529, 159);
            this.chRespaldo.Name = "chRespaldo";
            this.chRespaldo.Size = new System.Drawing.Size(381, 21);
            this.chRespaldo.TabIndex = 126;
            this.chRespaldo.Text = "Enviar respaldo al cerrar sesion";
            this.chRespaldo.UseVisualStyleBackColor = true;
            this.chRespaldo.CheckedChanged += new System.EventHandler(this.chRespaldo_CheckedChanged);
            // 
            // cbCorreoDescuento
            // 
            this.cbCorreoDescuento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCorreoDescuento.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoDescuento.Location = new System.Drawing.Point(529, 136);
            this.cbCorreoDescuento.Name = "cbCorreoDescuento";
            this.cbCorreoDescuento.Size = new System.Drawing.Size(381, 21);
            this.cbCorreoDescuento.TabIndex = 125;
            this.cbCorreoDescuento.Text = "Al hacer venta con descuento";
            this.cbCorreoDescuento.UseVisualStyleBackColor = true;
            this.cbCorreoDescuento.CheckedChanged += new System.EventHandler(this.cbCorreoDescuento_CheckedChanged);
            // 
            // cbCorreoIniciar
            // 
            this.cbCorreoIniciar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCorreoIniciar.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoIniciar.Location = new System.Drawing.Point(529, 113);
            this.cbCorreoIniciar.Name = "cbCorreoIniciar";
            this.cbCorreoIniciar.Size = new System.Drawing.Size(381, 21);
            this.cbCorreoIniciar.TabIndex = 124;
            this.cbCorreoIniciar.Text = "Al iniciar sesión";
            this.cbCorreoIniciar.UseVisualStyleBackColor = true;
            this.cbCorreoIniciar.CheckedChanged += new System.EventHandler(this.cbCorreoIniciar_CheckedChanged);
            // 
            // cbCorreoVenta
            // 
            this.cbCorreoVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCorreoVenta.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoVenta.Location = new System.Drawing.Point(529, 88);
            this.cbCorreoVenta.Name = "cbCorreoVenta";
            this.cbCorreoVenta.Size = new System.Drawing.Size(381, 21);
            this.cbCorreoVenta.TabIndex = 123;
            this.cbCorreoVenta.Text = "Al hacer una venta";
            this.cbCorreoVenta.UseVisualStyleBackColor = true;
            this.cbCorreoVenta.CheckedChanged += new System.EventHandler(this.cbCorreoVenta_CheckedChanged);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(262, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(220, 16);
            this.label4.TabIndex = 122;
            this.label4.Text = " (producto previamente seleccionado)";
            // 
            // cbCorreoCorteCaja
            // 
            this.cbCorreoCorteCaja.AutoSize = true;
            this.cbCorreoCorteCaja.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoCorteCaja.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cbCorreoCorteCaja.Location = new System.Drawing.Point(11, 88);
            this.cbCorreoCorteCaja.Name = "cbCorreoCorteCaja";
            this.cbCorreoCorteCaja.Size = new System.Drawing.Size(172, 21);
            this.cbCorreoCorteCaja.TabIndex = 121;
            this.cbCorreoCorteCaja.Text = "Al hacer corte de caja";
            this.cbCorreoCorteCaja.UseVisualStyleBackColor = true;
            this.cbCorreoCorteCaja.CheckedChanged += new System.EventHandler(this.cbCorreoCorteCaja_CheckedChanged);
            this.cbCorreoCorteCaja.Click += new System.EventHandler(this.cbCorreoCorteCaja_Click);
            // 
            // cbCorreoEliminarListaProductosVentas
            // 
            this.cbCorreoEliminarListaProductosVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCorreoEliminarListaProductosVentas.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoEliminarListaProductosVentas.Location = new System.Drawing.Point(529, 61);
            this.cbCorreoEliminarListaProductosVentas.Name = "cbCorreoEliminarListaProductosVentas";
            this.cbCorreoEliminarListaProductosVentas.Size = new System.Drawing.Size(381, 21);
            this.cbCorreoEliminarListaProductosVentas.TabIndex = 120;
            this.cbCorreoEliminarListaProductosVentas.Text = "Al eliminar producto de ventas";
            this.cbCorreoEliminarListaProductosVentas.UseVisualStyleBackColor = true;
            this.cbCorreoEliminarListaProductosVentas.CheckedChanged += new System.EventHandler(this.cbCorreoEliminarListaProductosVentas_CheckedChanged);
            // 
            // cbCorreoCerrarVentanaVentas
            // 
            this.cbCorreoCerrarVentanaVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCorreoCerrarVentanaVentas.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoCerrarVentanaVentas.Location = new System.Drawing.Point(529, 34);
            this.cbCorreoCerrarVentanaVentas.Name = "cbCorreoCerrarVentanaVentas";
            this.cbCorreoCerrarVentanaVentas.Size = new System.Drawing.Size(395, 21);
            this.cbCorreoCerrarVentanaVentas.TabIndex = 116;
            this.cbCorreoCerrarVentanaVentas.Text = "Al cerrar la ventana de ventas cuando tiene productos";
            this.cbCorreoCerrarVentanaVentas.UseVisualStyleBackColor = true;
            this.cbCorreoCerrarVentanaVentas.CheckedChanged += new System.EventHandler(this.cbCorreoCerrarVentanaVentas_CheckedChanged);
            // 
            // cbCorreoRetirarDineroCaja
            // 
            this.cbCorreoRetirarDineroCaja.AutoSize = true;
            this.cbCorreoRetirarDineroCaja.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.cbCorreoRetirarDineroCaja.Location = new System.Drawing.Point(11, 61);
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
            this.cbCorreoAgregarDineroCaja.Location = new System.Drawing.Point(11, 34);
            this.cbCorreoAgregarDineroCaja.Name = "cbCorreoAgregarDineroCaja";
            this.cbCorreoAgregarDineroCaja.Size = new System.Drawing.Size(193, 21);
            this.cbCorreoAgregarDineroCaja.TabIndex = 0;
            this.cbCorreoAgregarDineroCaja.Text = "Al agregar dinero en caja";
            this.cbCorreoAgregarDineroCaja.UseVisualStyleBackColor = true;
            this.cbCorreoAgregarDineroCaja.CheckedChanged += new System.EventHandler(this.cbCorreoAgregarDineroCaja_CheckedChanged);
            // 
            // cboTipoMoneda
            // 
            this.cboTipoMoneda.FormattingEnabled = true;
            this.cboTipoMoneda.Items.AddRange(new object[] {
            "Afganistán / AFN - (؋)",
            "Albania / ALL - (L)",
            "Alemania/ EUR - (€)",
            "Andorra/ EUR - (€)",
            "Angola/ AOA - (Kz)",
            "Arabia Saudí/ SAR - (ر.س)",
            "Argelia/ DZD - (د.ج)",
            "Argentina/ ARS - ($)",
            "Armenia/ AMD - ",
            "Aruba/ AWG - (ƒ)",
            "Australia/ AUD - ($)",
            "Austria/ EUR - (€)",
            "Azerbayán/ AZN - ",
            "Bahamas/ BSD - ($)",
            "Bangladés, Bangladesh/ BDT - (৳)",
            "Barbados/ BBD - ($)",
            "Baréin, Bahrein/ BHD - (.د.ب)",
            "Bélgica/ EUR - (€)",
            "Belice/ BZD - ($)",
            "Bielorrusia, Belarús/ BYR - (Br)",
            "Birmania, Myanmar/ MMK - (K)",
            "Bolivia/ BOB - (Bs.)",
            "Bosnia y Herzegovina/ BAM - (KM)",
            "Botsuana, Botswana/ BWP - (P)",
            "Brasil/ BRL - (R$)",
            "Brunéi/ BND - ($)",
            "Bulgaria/ BGN - (лв)",
            "Burkina Faso/ XOF - (Fr)",
            "Burundi/ BIF - (Fr)",
            "Bután/ BTN - (Nu.)",
            "Cabo Verde/ CVE - (Esc, $)",
            "Camboya/ KHR - (៛)",
            "Canadá/ CAD - ($)",
            "Catar, Qatar/ QAR - (ر.ق)",
            "Chad/ XAF - (Fr)",
            "Chile/ CLP - ($)",
            "China/ CNY - (¥, 元)",
            "Chipre/ EUR - (€)",
            "Ciudad del Vaticano/ EUR - (€)",
            "Colombia/ COP - ($)",
            "Comores/ KMF - (Fr)",
            "Corea del Norte/ KPW - (₩)",
            "Corea del Sur/ KRW - (₩)",
            "Costa de Marfil, Côte d\'Ivoire/ XOF - (Fr)",
            "Costa Rica/ CRC - (₡)",
            "Croacia/ HRK - (kn)",
            "Cuba/ CUP - ($)",
            "Curação/ ANG - (ƒ)",
            "Dinamarca/ DKK - (kr)",
            "Ecuador/ USD - ($)",
            "Egipto/ EGP - (£, ج.م)",
            "El Salvador/ SVC - (₡)",
            "Emiratos Árabes Unidos/ AED - (د.إ)",
            "Eritrea/ ERN - (Nfk)",
            "Eslovaquia/ EUR - (€)",
            "Eslovenia/ EUR - (€)",
            "España/ EUR - (€)",
            "Estados Federados de Micronesia/ nenhum - ($)",
            "Estados Unidos/ USD - ($)",
            "Estonia/ EUR - (€)",
            "Etiopía/ ETB - (Br)",
            "Filipinas/ PHP - (₱)",
            "Finlandia/ EUR - (€)",
            "Francia/ EUR - (€)",
            "Gabón/ XAF - (Fr)",
            "Gambia/ GMD - (D)",
            "Gana/ GHS - (₵)",
            "Georgia/ GEL - (ლ)",
            "Gibraltar/ GIP - (£)",
            "Grecia/ EUR - (€)",
            "Guatemala/ GTQ - (Q)",
            "Guinea/ GNF - (Fr)",
            "Guinea Ecuatorial/ XAF - (Fr)",
            "Guinea-Bisáu, Guinea Bissau/ XOF - (Fr)",
            "Haití/ HTG - (G)",
            "Honduras/ HNL - (L)",
            "Hungria/ HUF - (Ft)",
            "India/ INR - (₹)",
            "Indonesia/ IDR - (Rp)",
            "Irak/ IQD - (ع.د)",
            "Irán/ IRR - (﷼)",
            "Irlanda/ EUR - (€)",
            "Islandia/ ISK - (kr)",
            "Islas Caimán/ KYD - ($)",
            "Islas Salomón/ SBD - ($)",
            "Israel/ ILS - (₪)",
            "Italia/ EUR - (€)",
            "Jamaica/ JMD - ($)",
            "Japón/ JPY - (¥)",
            "Jordania/ JOD - د.ا",
            "Kazajistán, Kazajstán/ KZT - (₸)",
            "Kenia, Kenya/ KES - (Sh)",
            "Kirguzistán/ KGS - (лв)",
            "Kuwait/ KWD - (د.ك)",
            "Laos/ LAK - (₭)",
            "Lesoto, Lesotho/ LSL - (L)",
            "Letonia/ LVL - (Ls)",
            "Líbano/ LBP - (ل.ل)",
            "Liberia/ LRD - ($)",
            "Libia/ LYD - (ل.د)",
            "Lituania/ LTL - (Lt)",
            "Luxemburgo/ EUR - (€)",
            "Macao/ MOP - (P)",
            "Macedonia/ MKD - (ден)",
            "Madagascar/ MGA - (Ar)",
            "Malasia/ MYR - (RM)",
            "Malaui, Malawi/ MWK - (MK)",
            "Maldivas/ MVR - (.ރ)",
            "Mali/ XOF - (Fr)",
            "Malta/ EUR - (€)",
            "Marruecos/ MAD - (د.م.)",
            "Mauricio/ MUR - (₨)",
            "Mauritania/ MRO - (UM)",
            "México/ MXN - ($)",
            "Moldavia/ MDL - (L)",
            "Mónaco/ EUR - (€)",
            "Mongolia/ MNT - (₮)",
            "Montenegro/ EUR - (€)",
            "Mozambique/ MZN - (MT)",
            "Namibia/ NAD - ($)",
            "Nauru/ Nenhum - ($)",
            "Nepal/ NPR - (₨)",
            "Nicaragua/ NIO - (C$)",
            "Níger/ XOF - (Fr)",
            "Nigeria/ NGN - (₦)",
            "Noruega/ NOK - (kr)",
            "Nueva Caledonia/ XPF - (Fr)",
            "Nueva Zelanda/ NZD - ($)",
            "Omán/ OMR - (ر.ع.)",
            "Países Bajos/ EUR - (€)",
            "Panamá/ PAB - (B/.)",
            "Papua-Nueva Guiné/ PGK - (K)",
            "Paquistán/ PKR - (₨)",
            "Paraguay/ PYG - (₲)",
            "Perú/ PEN - (S/)",
            "Polinesia Francesa/ XPF - (Fr)",
            "Polonia/ PLN - (zł)",
            "Portugal/ EUR - (€)",
            "Reino Unido / GBP - (£)",
            "República Centroafricana/ XAF - (Fr)",
            "República Checa/ CZK - (Kč)",
            "República del Congo/ XAF - (Fr)",
            "República Democrática del Congo/ CDF - (Fr)",
            "República Dominicana/ DOP - ($)",
            "Ruanda, Rwanda/ RWF - (Fr)",
            "Rumanía/ RON - (L)",
            "Rusia/ RUB - (₽)",
            "Samoa/ WST - (T)",
            "San Cristóbal y Nieves, Saint Kitts y Nevis/ XCD - ($)",
            "San Marino/ EUR - (€)",
            "San Viccente y las Granadinas/ XCD - ($)",
            "Santa Lucía/ XCD - ($)",
            "Santo Tomé y Príncipe/ STD - (Db)",
            "Seichelles/ SCR - (₨)",
            "Senegal/ XOF - (Fr)",
            "Serbia/ RSD - (дин. o din.)",
            "Sierra Leona/ SLL - (Le)",
            "Singapur/ SGD - ($)",
            "Siria/ SYP - (£ o ل.س)",
            "Somalia/ SOS - (Sh)",
            "Sri Lanka/ LKR - (Rs)",
            "Suazilandia, Swazilandia/ SZL - (L)",
            "Sudáfrica/ ZAR - (R)",
            "Sudán/ SDG - (£)",
            "Sudán del Sur/ SSP - (£)",
            "Suecia/ SEK - (kr)",
            "Suiza/ CHF - (Fr)",
            "Surinám/ SRD - ($)",
            "Tailandia/ THB - (฿)",
            "Taiwán/ TWD - ($)",
            "Tanzania/ TZS - (Sh)",
            "Tayikistán/ TJS - (ЅМ)",
            "Togo/ XOF - (Fr)",
            "Tonga/ TOP - (T$)",
            "Trindade e Tobago/ TTD - ($)",
            "Túnez/ TND - (د.ت)",
            "Turkmenistán/ TMT - (m)",
            "Turquia/ TRY - ",
            "Ucrania/ UAH - (₴)",
            "Uganda/ UGX - (Sh)",
            "Uruguay/ UYU - ($)",
            "Uzbequistán/ UZS - (лв)",
            "Vanuatu/ VUV - (Vt)",
            "Venezuela/ VEF - (Bs F)",
            "Vietnam/ VND - (₫)",
            "Yemen/ YER - (﷼)",
            "Yibuti, Djibouti/ DJF - (Fr)",
            "Zambia/ ZMK - (ZK)",
            "Zimbabue, Zimbabwe/ ZWL - ($)"});
            this.cboTipoMoneda.Location = new System.Drawing.Point(184, 593);
            this.cboTipoMoneda.Name = "cboTipoMoneda";
            this.cboTipoMoneda.Size = new System.Drawing.Size(172, 21);
            this.cboTipoMoneda.TabIndex = 130;
            this.cboTipoMoneda.Visible = false;
            this.cboTipoMoneda.SelectedIndexChanged += new System.EventHandler(this.cboTipoMoneda_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(73, 593);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 16);
            this.label6.TabIndex = 132;
            this.label6.Text = "Tipo de Moneda:";
            this.label6.Visible = false;
            // 
            // txtMinimoMayoreo
            // 
            this.txtMinimoMayoreo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMinimoMayoreo.Enabled = false;
            this.txtMinimoMayoreo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinimoMayoreo.Location = new System.Drawing.Point(806, 52);
            this.txtMinimoMayoreo.Name = "txtMinimoMayoreo";
            this.txtMinimoMayoreo.Size = new System.Drawing.Size(69, 21);
            this.txtMinimoMayoreo.TabIndex = 122;
            this.txtMinimoMayoreo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMinimoMayoreo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMinimoMayoreo_KeyUp);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(668, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 17);
            this.label1.TabIndex = 123;
            this.label1.Text = "- Cantidad mínima";
            // 
            // checkMayoreo
            // 
            this.checkMayoreo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkMayoreo.AutoSize = true;
            this.checkMayoreo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkMayoreo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.checkMayoreo.Location = new System.Drawing.Point(654, 24);
            this.checkMayoreo.Name = "checkMayoreo";
            this.checkMayoreo.Size = new System.Drawing.Size(273, 21);
            this.checkMayoreo.TabIndex = 121;
            this.checkMayoreo.Text = "Activar precio por mayoreo en ventas";
            this.checkMayoreo.UseVisualStyleBackColor = true;
            this.checkMayoreo.CheckedChanged += new System.EventHandler(this.checkMayoreo_CheckedChanged);
            this.checkMayoreo.Click += new System.EventHandler(this.checkMayoreo_Click);
            // 
            // checkNoVendidos
            // 
            this.checkNoVendidos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkNoVendidos.AutoSize = true;
            this.checkNoVendidos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkNoVendidos.ForeColor = System.Drawing.SystemColors.Highlight;
            this.checkNoVendidos.Location = new System.Drawing.Point(654, 78);
            this.checkNoVendidos.Name = "checkNoVendidos";
            this.checkNoVendidos.Size = new System.Drawing.Size(240, 21);
            this.checkNoVendidos.TabIndex = 124;
            this.checkNoVendidos.Text = "Avisar de productos no vendidos";
            this.checkNoVendidos.UseVisualStyleBackColor = true;
            this.checkNoVendidos.CheckedChanged += new System.EventHandler(this.checkNoVendidos_CheckedChanged);
            this.checkNoVendidos.Click += new System.EventHandler(this.checkNoVendidos_Click);
            // 
            // cbStockNegativo
            // 
            this.cbStockNegativo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbStockNegativo.AutoSize = true;
            this.cbStockNegativo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStockNegativo.Location = new System.Drawing.Point(333, 53);
            this.cbStockNegativo.Name = "cbStockNegativo";
            this.cbStockNegativo.Size = new System.Drawing.Size(177, 21);
            this.cbStockNegativo.TabIndex = 1;
            this.cbStockNegativo.Text = "Permitir Stock negativo";
            this.cbStockNegativo.UseVisualStyleBackColor = true;
            this.cbStockNegativo.CheckedChanged += new System.EventHandler(this.cbStockNegativo_CheckedChanged);
            // 
            // txtNoVendidos
            // 
            this.txtNoVendidos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoVendidos.Enabled = false;
            this.txtNoVendidos.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoVendidos.Location = new System.Drawing.Point(728, 98);
            this.txtNoVendidos.Name = "txtNoVendidos";
            this.txtNoVendidos.Size = new System.Drawing.Size(69, 21);
            this.txtNoVendidos.TabIndex = 125;
            this.txtNoVendidos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNoVendidos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNoVendidos_KeyUp);
            // 
            // cbMostrarPrecio
            // 
            this.cbMostrarPrecio.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbMostrarPrecio.AutoSize = true;
            this.cbMostrarPrecio.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMostrarPrecio.Location = new System.Drawing.Point(333, 24);
            this.cbMostrarPrecio.Name = "cbMostrarPrecio";
            this.cbMostrarPrecio.Size = new System.Drawing.Size(277, 21);
            this.cbMostrarPrecio.TabIndex = 116;
            this.cbMostrarPrecio.Text = "Mostrar precio de productos en ventas";
            this.cbMostrarPrecio.UseVisualStyleBackColor = true;
            this.cbMostrarPrecio.CheckedChanged += new System.EventHandler(this.cbMostrarPrecio_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(668, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 126;
            this.label2.Text = "- Cada";
            // 
            // cbMostrarCB
            // 
            this.cbMostrarCB.AutoSize = true;
            this.cbMostrarCB.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMostrarCB.Location = new System.Drawing.Point(11, 78);
            this.cbMostrarCB.Name = "cbMostrarCB";
            this.cbMostrarCB.Size = new System.Drawing.Size(283, 21);
            this.cbMostrarCB.TabIndex = 117;
            this.cbMostrarCB.Text = "Mostrar código de productos en ventas";
            this.cbMostrarCB.UseVisualStyleBackColor = true;
            this.cbMostrarCB.CheckedChanged += new System.EventHandler(this.cbMostrarCB_CheckedChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(803, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 17);
            this.label3.TabIndex = 127;
            this.label3.Text = "días";
            // 
            // pagWeb
            // 
            this.pagWeb.AutoSize = true;
            this.pagWeb.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pagWeb.Location = new System.Drawing.Point(11, 51);
            this.pagWeb.Name = "pagWeb";
            this.pagWeb.Size = new System.Drawing.Size(267, 21);
            this.pagWeb.TabIndex = 115;
            this.pagWeb.Text = "Habilitar información en página web";
            this.pagWeb.UseVisualStyleBackColor = true;
            this.pagWeb.CheckedChanged += new System.EventHandler(this.pagWeb_CheckedChanged);
            // 
            // checkCBVenta
            // 
            this.checkCBVenta.AutoSize = true;
            this.checkCBVenta.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkCBVenta.Location = new System.Drawing.Point(11, 24);
            this.checkCBVenta.Name = "checkCBVenta";
            this.checkCBVenta.Size = new System.Drawing.Size(245, 21);
            this.checkCBVenta.TabIndex = 110;
            this.checkCBVenta.Text = "Código de barras ticket de venta";
            this.checkCBVenta.UseVisualStyleBackColor = true;
            this.checkCBVenta.CheckedChanged += new System.EventHandler(this.checkCBVenta_CheckedChanged);
            // 
            // chTicketVentas
            // 
            this.chTicketVentas.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.chTicketVentas.AutoSize = true;
            this.chTicketVentas.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chTicketVentas.Location = new System.Drawing.Point(333, 80);
            this.chTicketVentas.Name = "chTicketVentas";
            this.chTicketVentas.Size = new System.Drawing.Size(232, 21);
            this.chTicketVentas.TabIndex = 128;
            this.chTicketVentas.Text = "Generar ticket al realizar ventas";
            this.chTicketVentas.UseVisualStyleBackColor = true;
            this.chTicketVentas.CheckedChanged += new System.EventHandler(this.chTicketVentas_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.groupBox2.Controls.Add(this.chTicketVentas);
            this.groupBox2.Controls.Add(this.checkCBVenta);
            this.groupBox2.Controls.Add(this.pagWeb);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cbMostrarCB);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.cbMostrarPrecio);
            this.groupBox2.Controls.Add(this.txtNoVendidos);
            this.groupBox2.Controls.Add(this.cbStockNegativo);
            this.groupBox2.Controls.Add(this.checkNoVendidos);
            this.groupBox2.Controls.Add(this.checkMayoreo);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtMinimoMayoreo);
            this.groupBox2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(44, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(933, 130);
            this.groupBox2.TabIndex = 129;
            this.groupBox2.TabStop = false;
            this.groupBox2.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.btnConfiguracionGeneral);
            this.panel1.Controls.Add(this.botonRedondo4);
            this.panel1.Controls.Add(this.botonRedondo1);
            this.panel1.Controls.Add(this.btnRespaldarInformacion);
            this.panel1.Controls.Add(this.botonRedondo5);
            this.panel1.Controls.Add(this.btnEnvioCorreo);
            this.panel1.Location = new System.Drawing.Point(133, 83);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(977, 114);
            this.panel1.TabIndex = 137;
            // 
            // btnConfiguracionGeneral
            // 
            this.btnConfiguracionGeneral.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnConfiguracionGeneral.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnConfiguracionGeneral.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnConfiguracionGeneral.BorderRadius = 40;
            this.btnConfiguracionGeneral.BorderSize = 0;
            this.btnConfiguracionGeneral.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfiguracionGeneral.FlatAppearance.BorderSize = 0;
            this.btnConfiguracionGeneral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfiguracionGeneral.ForeColor = System.Drawing.Color.White;
            this.btnConfiguracionGeneral.Image = global::PuntoDeVentaV2.Properties.Resources.gear_in;
            this.btnConfiguracionGeneral.Location = new System.Drawing.Point(315, 6);
            this.btnConfiguracionGeneral.Name = "btnConfiguracionGeneral";
            this.btnConfiguracionGeneral.Size = new System.Drawing.Size(132, 87);
            this.btnConfiguracionGeneral.TabIndex = 135;
            this.btnConfiguracionGeneral.Text = "Configuracion general";
            this.btnConfiguracionGeneral.TextColor = System.Drawing.Color.White;
            this.btnConfiguracionGeneral.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnConfiguracionGeneral.UseVisualStyleBackColor = false;
            this.btnConfiguracionGeneral.Click += new System.EventHandler(this.btnConfiguracionGeneral_Click);
            // 
            // botonRedondo4
            // 
            this.botonRedondo4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.botonRedondo4.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.botonRedondo4.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.botonRedondo4.BorderRadius = 40;
            this.botonRedondo4.BorderSize = 0;
            this.botonRedondo4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.botonRedondo4.FlatAppearance.BorderSize = 0;
            this.botonRedondo4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonRedondo4.ForeColor = System.Drawing.Color.White;
            this.botonRedondo4.Image = global::PuntoDeVentaV2.Properties.Resources.finance;
            this.botonRedondo4.Location = new System.Drawing.Point(462, 6);
            this.botonRedondo4.Name = "botonRedondo4";
            this.botonRedondo4.Size = new System.Drawing.Size(132, 87);
            this.botonRedondo4.TabIndex = 136;
            this.botonRedondo4.Text = "Porcentaje % de ganancia";
            this.botonRedondo4.TextColor = System.Drawing.Color.White;
            this.botonRedondo4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.botonRedondo4.UseVisualStyleBackColor = false;
            this.botonRedondo4.Click += new System.EventHandler(this.botonRedondo4_Click);
            // 
            // botonRedondo1
            // 
            this.botonRedondo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.botonRedondo1.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.botonRedondo1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.botonRedondo1.BorderRadius = 40;
            this.botonRedondo1.BorderSize = 0;
            this.botonRedondo1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.botonRedondo1.FlatAppearance.BorderSize = 0;
            this.botonRedondo1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.botonRedondo1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.botonRedondo1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonRedondo1.ForeColor = System.Drawing.Color.White;
            this.botonRedondo1.Image = global::PuntoDeVentaV2.Properties.Resources.cinema_ticket;
            this.botonRedondo1.Location = new System.Drawing.Point(3, 7);
            this.botonRedondo1.Name = "botonRedondo1";
            this.botonRedondo1.Size = new System.Drawing.Size(134, 87);
            this.botonRedondo1.TabIndex = 133;
            this.botonRedondo1.Text = "Editar \r\nTicket";
            this.botonRedondo1.TextColor = System.Drawing.Color.White;
            this.botonRedondo1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.botonRedondo1.UseMnemonic = false;
            this.botonRedondo1.UseVisualStyleBackColor = false;
            this.botonRedondo1.Click += new System.EventHandler(this.botonRedondo1_Click);
            // 
            // btnRespaldarInformacion
            // 
            this.btnRespaldarInformacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnRespaldarInformacion.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnRespaldarInformacion.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnRespaldarInformacion.BorderRadius = 40;
            this.btnRespaldarInformacion.BorderSize = 0;
            this.btnRespaldarInformacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRespaldarInformacion.FlatAppearance.BorderSize = 0;
            this.btnRespaldarInformacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRespaldarInformacion.ForeColor = System.Drawing.Color.White;
            this.btnRespaldarInformacion.Image = global::PuntoDeVentaV2.Properties.Resources.saved_imports;
            this.btnRespaldarInformacion.Location = new System.Drawing.Point(765, 6);
            this.btnRespaldarInformacion.Name = "btnRespaldarInformacion";
            this.btnRespaldarInformacion.Size = new System.Drawing.Size(134, 87);
            this.btnRespaldarInformacion.TabIndex = 128;
            this.btnRespaldarInformacion.Text = "Respaldar información";
            this.btnRespaldarInformacion.TextColor = System.Drawing.Color.White;
            this.btnRespaldarInformacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRespaldarInformacion.UseVisualStyleBackColor = false;
            this.btnRespaldarInformacion.Click += new System.EventHandler(this.btnRespaldarInformacion_Click);
            // 
            // botonRedondo5
            // 
            this.botonRedondo5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.botonRedondo5.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.botonRedondo5.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.botonRedondo5.BorderRadius = 40;
            this.botonRedondo5.BorderSize = 0;
            this.botonRedondo5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.botonRedondo5.FlatAppearance.BorderSize = 0;
            this.botonRedondo5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonRedondo5.ForeColor = System.Drawing.Color.White;
            this.botonRedondo5.Image = global::PuntoDeVentaV2.Properties.Resources.money_dollar;
            this.botonRedondo5.Location = new System.Drawing.Point(613, 7);
            this.botonRedondo5.Name = "botonRedondo5";
            this.botonRedondo5.Size = new System.Drawing.Size(134, 89);
            this.botonRedondo5.TabIndex = 127;
            this.botonRedondo5.Text = "Tipo de moneda ($)";
            this.botonRedondo5.TextColor = System.Drawing.Color.White;
            this.botonRedondo5.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.botonRedondo5.UseVisualStyleBackColor = false;
            this.botonRedondo5.Click += new System.EventHandler(this.botonRedondo5_Click);
            // 
            // btnEnvioCorreo
            // 
            this.btnEnvioCorreo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnEnvioCorreo.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnEnvioCorreo.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnEnvioCorreo.BorderRadius = 40;
            this.btnEnvioCorreo.BorderSize = 0;
            this.btnEnvioCorreo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEnvioCorreo.FlatAppearance.BorderSize = 0;
            this.btnEnvioCorreo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEnvioCorreo.ForeColor = System.Drawing.Color.White;
            this.btnEnvioCorreo.Image = global::PuntoDeVentaV2.Properties.Resources.email_send_receive;
            this.btnEnvioCorreo.Location = new System.Drawing.Point(159, 7);
            this.btnEnvioCorreo.Name = "btnEnvioCorreo";
            this.btnEnvioCorreo.Size = new System.Drawing.Size(132, 87);
            this.btnEnvioCorreo.TabIndex = 134;
            this.btnEnvioCorreo.Text = "Envio de Correo";
            this.btnEnvioCorreo.TextColor = System.Drawing.Color.White;
            this.btnEnvioCorreo.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnEnvioCorreo.UseVisualStyleBackColor = false;
            this.btnEnvioCorreo.Click += new System.EventHandler(this.btnEnvioCorreo_Click);
            // 
            // SetUpPUDVE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 716);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboTipoMoneda);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGuardarPorcentaje);
            this.Controls.Add(this.txtPorcentajeProducto);
            this.Controls.Add(this.lbPorcentajeProducto);
            this.Controls.Add(this.btnGuardarRevision);
            this.Controls.Add(this.txtNumeroRevision);
            this.Controls.Add(this.lbNumeroRevision);
            this.Controls.Add(this.btnGuardarServidor);
            this.Controls.Add(this.txtNombreServidor);
            this.Controls.Add(this.lbNombreServidor);
            this.Controls.Add(this.tituloSeccion);
            this.Controls.Add(this.btnRespaldo);
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
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRespaldo;
        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.SaveFileDialog guardarArchivo;
        private System.Windows.Forms.TextBox txtNombreServidor;
        private System.Windows.Forms.Button btnGuardarServidor;
        private System.Windows.Forms.Label lbNombreServidor;
        private System.Windows.Forms.Button btnGuardarRevision;
        private System.Windows.Forms.TextBox txtNumeroRevision;
        private System.Windows.Forms.Label lbNumeroRevision;
        private System.Windows.Forms.CheckBox cbCorreoPrecioProducto;
        private System.Windows.Forms.CheckBox cbCorreoStockProducto;
        private System.Windows.Forms.CheckBox cbCorreoStockMinimo;
        private System.Windows.Forms.CheckBox cbCorreoVenderProducto;
        private System.Windows.Forms.Button btnGuardarPorcentaje;
        private System.Windows.Forms.TextBox txtPorcentajeProducto;
        private System.Windows.Forms.Label lbPorcentajeProducto;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbCorreoAgregarDineroCaja;
        private System.Windows.Forms.CheckBox cbCorreoRetirarDineroCaja;
        private System.Windows.Forms.CheckBox cbCorreoCerrarVentanaVentas;
        private System.Windows.Forms.CheckBox cbCorreoEliminarListaProductosVentas;
        private System.Windows.Forms.CheckBox cbCorreoCorteCaja;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbCorreoVenta;
        private System.Windows.Forms.CheckBox cbCorreoIniciar;
        private System.Windows.Forms.CheckBox cbCorreoDescuento;
        private System.Windows.Forms.CheckBox chRespaldo;
        private System.Windows.Forms.ComboBox cboTipoMoneda;
        private System.Windows.Forms.Label label6;
        private BotonRedondo botonRedondo1;
        private BotonRedondo btnRespaldarInformacion;
        private BotonRedondo botonRedondo5;
        private System.Windows.Forms.TextBox txtMinimoMayoreo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkMayoreo;
        private System.Windows.Forms.CheckBox checkNoVendidos;
        private System.Windows.Forms.CheckBox cbStockNegativo;
        private System.Windows.Forms.TextBox txtNoVendidos;
        private System.Windows.Forms.CheckBox cbMostrarPrecio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbMostrarCB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox pagWeb;
        private System.Windows.Forms.CheckBox checkCBVenta;
        private System.Windows.Forms.CheckBox chTicketVentas;
        private System.Windows.Forms.GroupBox groupBox2;
        private BotonRedondo btnEnvioCorreo;
        private BotonRedondo btnConfiguracionGeneral;
        private BotonRedondo botonRedondo4;
        private System.Windows.Forms.Panel panel1;
    }
}