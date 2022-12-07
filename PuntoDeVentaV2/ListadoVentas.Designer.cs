namespace PuntoDeVentaV2
{
    partial class ListadoVentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListadoVentas));
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.DGVListadoVentas = new System.Windows.Forms.DataGridView();
            this.col_checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RFC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vendedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IVA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cancelar = new System.Windows.Forms.DataGridViewImageColumn();
            this.Factura = new System.Windows.Forms.DataGridViewImageColumn();
            this.Ticket = new System.Windows.Forms.DataGridViewImageColumn();
            this.Abono = new System.Windows.Forms.DataGridViewImageColumn();
            this.Timbrar = new System.Windows.Forms.DataGridViewImageColumn();
            this.cInformacion = new System.Windows.Forms.DataGridViewImageColumn();
            this.retomarVenta = new System.Windows.Forms.DataGridViewImageColumn();
            this.ganancia = new System.Windows.Forms.DataGridViewImageColumn();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.cbFormasPago = new System.Windows.Forms.ComboBox();
            this.cbFiltroAdminEmpleado = new System.Windows.Forms.ComboBox();
            this.dpHoraFinal = new System.Windows.Forms.DateTimePicker();
            this.dpHoraInicial = new System.Windows.Forms.DateTimePicker();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCrearVentaGlobal = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btn_descargar = new System.Windows.Forms.Button();
            this.txtBuscador = new System.Windows.Forms.TextBox();
            this.btn_enviar = new System.Windows.Forms.Button();
            this.dpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.btnNuevaVenta = new System.Windows.Forms.Button();
            this.btnBuscarVentas = new System.Windows.Forms.Button();
            this.cbVentas = new System.Windows.Forms.ComboBox();
            this.cbTipoVentas = new System.Windows.Forms.ComboBox();
            this.cbTipoRentas = new System.Windows.Forms.ComboBox();
            this.TTMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnActualizarMaximoProductos = new System.Windows.Forms.Button();
            this.txtMaximoPorPagina = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnUltimaPagina = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnPrimeraPagina = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.linkLblPaginaSiguiente = new System.Windows.Forms.LinkLabel();
            this.linkLblPaginaActual = new System.Windows.Forms.LinkLabel();
            this.linkLblPaginaAnterior = new System.Windows.Forms.LinkLabel();
            this.label7 = new System.Windows.Forms.Label();
            this.elegir_carpeta_descarga = new System.Windows.Forms.FolderBrowserDialog();
            this.pBar_descarga_verpdf = new System.Windows.Forms.ProgressBar();
            this.lb_texto_descarga_verpdf = new System.Windows.Forms.Label();
            this.lb_txt_ruta_descargar = new System.Windows.Forms.Label();
            this.chTodos = new System.Windows.Forms.CheckBox();
            this.chkHDAutlan = new System.Windows.Forms.CheckBox();
            this.rbVentas = new System.Windows.Forms.RadioButton();
            this.rbRentas = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.DGVListadoVentas)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(446, 27);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(102, 25);
            this.tituloSeccion.TabIndex = 4;
            this.tituloSeccion.Text = "VENTAS";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DGVListadoVentas
            // 
            this.DGVListadoVentas.AllowUserToAddRows = false;
            this.DGVListadoVentas.AllowUserToDeleteRows = false;
            this.DGVListadoVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVListadoVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVListadoVentas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_checkbox,
            this.ID,
            this.Cliente,
            this.RFC,
            this.Vendedor,
            this.Subtotal,
            this.IVA,
            this.Total,
            this.Folio,
            this.Serie,
            this.Fecha,
            this.Cancelar,
            this.Factura,
            this.Ticket,
            this.Abono,
            this.Timbrar,
            this.cInformacion,
            this.retomarVenta,
            this.ganancia});
            this.DGVListadoVentas.Location = new System.Drawing.Point(13, 194);
            this.DGVListadoVentas.Name = "DGVListadoVentas";
            this.DGVListadoVentas.ReadOnly = true;
            this.DGVListadoVentas.RowHeadersVisible = false;
            this.DGVListadoVentas.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVListadoVentas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVListadoVentas.Size = new System.Drawing.Size(960, 217);
            this.DGVListadoVentas.TabIndex = 5;
            this.DGVListadoVentas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVListadoVentas_CellClick);
            this.DGVListadoVentas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.clickcellc_checkbox);
            this.DGVListadoVentas.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVListadoVentas_CellMouseEnter);
            this.DGVListadoVentas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGVListadoVentas_KeyDown);
            // 
            // col_checkbox
            // 
            this.col_checkbox.HeaderText = "";
            this.col_checkbox.Name = "col_checkbox";
            this.col_checkbox.ReadOnly = true;
            this.col_checkbox.Width = 35;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Cliente
            // 
            this.Cliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            // 
            // RFC
            // 
            this.RFC.HeaderText = "RFC";
            this.RFC.Name = "RFC";
            this.RFC.ReadOnly = true;
            // 
            // Vendedor
            // 
            this.Vendedor.HeaderText = "Vendedor";
            this.Vendedor.Name = "Vendedor";
            this.Vendedor.ReadOnly = true;
            this.Vendedor.Width = 180;
            // 
            // Subtotal
            // 
            this.Subtotal.HeaderText = "Subtotal";
            this.Subtotal.Name = "Subtotal";
            this.Subtotal.ReadOnly = true;
            this.Subtotal.Visible = false;
            // 
            // IVA
            // 
            this.IVA.HeaderText = "IVA";
            this.IVA.Name = "IVA";
            this.IVA.ReadOnly = true;
            this.IVA.Visible = false;
            // 
            // Total
            // 
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // Folio
            // 
            this.Folio.HeaderText = "Folio";
            this.Folio.Name = "Folio";
            this.Folio.ReadOnly = true;
            this.Folio.Width = 50;
            // 
            // Serie
            // 
            this.Serie.HeaderText = "Serie";
            this.Serie.Name = "Serie";
            this.Serie.ReadOnly = true;
            this.Serie.Width = 50;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 150;
            // 
            // Cancelar
            // 
            this.Cancelar.HeaderText = "";
            this.Cancelar.Name = "Cancelar";
            this.Cancelar.ReadOnly = true;
            this.Cancelar.Width = 30;
            // 
            // Factura
            // 
            this.Factura.HeaderText = "";
            this.Factura.Name = "Factura";
            this.Factura.ReadOnly = true;
            this.Factura.Width = 30;
            // 
            // Ticket
            // 
            this.Ticket.HeaderText = "";
            this.Ticket.Name = "Ticket";
            this.Ticket.ReadOnly = true;
            this.Ticket.Width = 30;
            // 
            // Abono
            // 
            this.Abono.HeaderText = "";
            this.Abono.Name = "Abono";
            this.Abono.ReadOnly = true;
            this.Abono.Width = 30;
            // 
            // Timbrar
            // 
            this.Timbrar.HeaderText = "";
            this.Timbrar.Name = "Timbrar";
            this.Timbrar.ReadOnly = true;
            this.Timbrar.Width = 30;
            // 
            // cInformacion
            // 
            this.cInformacion.HeaderText = "";
            this.cInformacion.Name = "cInformacion";
            this.cInformacion.ReadOnly = true;
            this.cInformacion.Width = 30;
            // 
            // retomarVenta
            // 
            this.retomarVenta.HeaderText = "";
            this.retomarVenta.Name = "retomarVenta";
            this.retomarVenta.ReadOnly = true;
            this.retomarVenta.ToolTipText = "Retomar Venta Cancelada";
            this.retomarVenta.Width = 30;
            // 
            // ganancia
            // 
            this.ganancia.HeaderText = "";
            this.ganancia.Name = "ganancia";
            this.ganancia.ReadOnly = true;
            this.ganancia.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // panelBotones
            // 
            this.panelBotones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBotones.Controls.Add(this.cbFormasPago);
            this.panelBotones.Controls.Add(this.cbFiltroAdminEmpleado);
            this.panelBotones.Controls.Add(this.dpHoraFinal);
            this.panelBotones.Controls.Add(this.dpHoraInicial);
            this.panelBotones.Controls.Add(this.button1);
            this.panelBotones.Controls.Add(this.label2);
            this.panelBotones.Controls.Add(this.label1);
            this.panelBotones.Controls.Add(this.btnCrearVentaGlobal);
            this.panelBotones.Controls.Add(this.btnReportes);
            this.panelBotones.Controls.Add(this.btn_descargar);
            this.panelBotones.Controls.Add(this.txtBuscador);
            this.panelBotones.Controls.Add(this.btn_enviar);
            this.panelBotones.Controls.Add(this.dpFechaFinal);
            this.panelBotones.Controls.Add(this.dpFechaInicial);
            this.panelBotones.Controls.Add(this.btnNuevaVenta);
            this.panelBotones.Controls.Add(this.btnBuscarVentas);
            this.panelBotones.Controls.Add(this.cbVentas);
            this.panelBotones.Controls.Add(this.cbTipoRentas);
            this.panelBotones.Controls.Add(this.cbTipoVentas);
            this.panelBotones.Location = new System.Drawing.Point(12, 55);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(960, 110);
            this.panelBotones.TabIndex = 6;
            // 
            // cbFormasPago
            // 
            this.cbFormasPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormasPago.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFormasPago.FormattingEnabled = true;
            this.cbFormasPago.Location = new System.Drawing.Point(3, 56);
            this.cbFormasPago.Name = "cbFormasPago";
            this.cbFormasPago.Size = new System.Drawing.Size(240, 21);
            this.cbFormasPago.TabIndex = 19;
            // 
            // cbFiltroAdminEmpleado
            // 
            this.cbFiltroAdminEmpleado.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiltroAdminEmpleado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFiltroAdminEmpleado.FormattingEnabled = true;
            this.cbFiltroAdminEmpleado.Items.AddRange(new object[] {
            "Administrador",
            "Todos"});
            this.cbFiltroAdminEmpleado.Location = new System.Drawing.Point(3, 5);
            this.cbFiltroAdminEmpleado.Name = "cbFiltroAdminEmpleado";
            this.cbFiltroAdminEmpleado.Size = new System.Drawing.Size(240, 21);
            this.cbFiltroAdminEmpleado.TabIndex = 15;
            this.cbFiltroAdminEmpleado.SelectedIndexChanged += new System.EventHandler(this.cbFiltroAdminEmpleado_SelectedIndexChanged);
            // 
            // dpHoraFinal
            // 
            this.dpHoraFinal.CustomFormat = "HH:mm";
            this.dpHoraFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpHoraFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpHoraFinal.Location = new System.Drawing.Point(437, 32);
            this.dpHoraFinal.Name = "dpHoraFinal";
            this.dpHoraFinal.ShowUpDown = true;
            this.dpHoraFinal.Size = new System.Drawing.Size(55, 21);
            this.dpHoraFinal.TabIndex = 18;
            // 
            // dpHoraInicial
            // 
            this.dpHoraInicial.CustomFormat = "HH:mm";
            this.dpHoraInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpHoraInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpHoraInicial.Location = new System.Drawing.Point(437, 6);
            this.dpHoraInicial.Name = "dpHoraInicial";
            this.dpHoraInicial.ShowUpDown = true;
            this.dpHoraInicial.Size = new System.Drawing.Size(55, 21);
            this.dpHoraInicial.TabIndex = 17;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(506, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 26);
            this.button1.TabIndex = 16;
            this.button1.Text = "Asignar Cliente / Método de Pago";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(310, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "AL:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(306, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "DE:";
            // 
            // btnCrearVentaGlobal
            // 
            this.btnCrearVentaGlobal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCrearVentaGlobal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnCrearVentaGlobal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrearVentaGlobal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCrearVentaGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearVentaGlobal.ForeColor = System.Drawing.Color.White;
            this.btnCrearVentaGlobal.Location = new System.Drawing.Point(506, 0);
            this.btnCrearVentaGlobal.Name = "btnCrearVentaGlobal";
            this.btnCrearVentaGlobal.Size = new System.Drawing.Size(220, 26);
            this.btnCrearVentaGlobal.TabIndex = 10;
            this.btnCrearVentaGlobal.Text = "Crear venta global";
            this.btnCrearVentaGlobal.UseVisualStyleBackColor = false;
            this.btnCrearVentaGlobal.Click += new System.EventHandler(this.btnCrearVentaGlobal_Click);
            // 
            // btnReportes
            // 
            this.btnReportes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReportes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnReportes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnReportes.ForeColor = System.Drawing.Color.White;
            this.btnReportes.Location = new System.Drawing.Point(732, 0);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(120, 26);
            this.btnReportes.TabIndex = 12;
            this.btnReportes.Text = "Generar Reporte";
            this.btnReportes.UseVisualStyleBackColor = false;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // btn_descargar
            // 
            this.btn_descargar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_descargar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btn_descargar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_descargar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_descargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_descargar.ForeColor = System.Drawing.Color.White;
            this.btn_descargar.Location = new System.Drawing.Point(858, 30);
            this.btn_descargar.Name = "btn_descargar";
            this.btn_descargar.Size = new System.Drawing.Size(100, 26);
            this.btn_descargar.TabIndex = 10;
            this.btn_descargar.Text = "Descargar";
            this.btn_descargar.UseVisualStyleBackColor = false;
            this.btn_descargar.Click += new System.EventHandler(this.btn_descargar_Click);
            // 
            // txtBuscador
            // 
            this.txtBuscador.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscador.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscador.Location = new System.Drawing.Point(3, 81);
            this.txtBuscador.Name = "txtBuscador";
            this.txtBuscador.Size = new System.Drawing.Size(304, 21);
            this.txtBuscador.TabIndex = 9;
            this.txtBuscador.Text = "BUSCAR POR RFC, CLIENTE, EMPLEADO O FOLIO...";
            this.txtBuscador.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBuscador.TextChanged += new System.EventHandler(this.txtBuscador_TextChanged);
            this.txtBuscador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscador_KeyDown);
            // 
            // btn_enviar
            // 
            this.btn_enviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_enviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btn_enviar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_enviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_enviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_enviar.ForeColor = System.Drawing.Color.White;
            this.btn_enviar.Location = new System.Drawing.Point(858, 0);
            this.btn_enviar.Name = "btn_enviar";
            this.btn_enviar.Size = new System.Drawing.Size(100, 26);
            this.btn_enviar.TabIndex = 8;
            this.btn_enviar.Text = "Enviar";
            this.btn_enviar.UseVisualStyleBackColor = false;
            this.btn_enviar.Click += new System.EventHandler(this.btn_enviar_Click);
            // 
            // dpFechaFinal
            // 
            this.dpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dpFechaFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpFechaFinal.Location = new System.Drawing.Point(334, 32);
            this.dpFechaFinal.Name = "dpFechaFinal";
            this.dpFechaFinal.Size = new System.Drawing.Size(101, 21);
            this.dpFechaFinal.TabIndex = 7;
            // 
            // dpFechaInicial
            // 
            this.dpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dpFechaInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpFechaInicial.Location = new System.Drawing.Point(334, 6);
            this.dpFechaInicial.Name = "dpFechaInicial";
            this.dpFechaInicial.Size = new System.Drawing.Size(101, 21);
            this.dpFechaInicial.TabIndex = 6;
            // 
            // btnNuevaVenta
            // 
            this.btnNuevaVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevaVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnNuevaVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevaVenta.FlatAppearance.BorderSize = 0;
            this.btnNuevaVenta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnNuevaVenta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnNuevaVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevaVenta.ForeColor = System.Drawing.Color.White;
            this.btnNuevaVenta.Location = new System.Drawing.Point(732, 30);
            this.btnNuevaVenta.Name = "btnNuevaVenta";
            this.btnNuevaVenta.Size = new System.Drawing.Size(120, 26);
            this.btnNuevaVenta.TabIndex = 5;
            this.btnNuevaVenta.Text = "Nueva venta";
            this.btnNuevaVenta.UseVisualStyleBackColor = false;
            this.btnNuevaVenta.Click += new System.EventHandler(this.btnNuevaVenta_Click);
            // 
            // btnBuscarVentas
            // 
            this.btnBuscarVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnBuscarVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarVentas.FlatAppearance.BorderSize = 0;
            this.btnBuscarVentas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnBuscarVentas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnBuscarVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarVentas.ForeColor = System.Drawing.Color.White;
            this.btnBuscarVentas.Location = new System.Drawing.Point(319, 81);
            this.btnBuscarVentas.Name = "btnBuscarVentas";
            this.btnBuscarVentas.Size = new System.Drawing.Size(176, 21);
            this.btnBuscarVentas.TabIndex = 4;
            this.btnBuscarVentas.Text = "BUSCAR";
            this.btnBuscarVentas.UseVisualStyleBackColor = false;
            this.btnBuscarVentas.Click += new System.EventHandler(this.btnBuscarVentas_Click);
            // 
            // cbVentas
            // 
            this.cbVentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbVentas.FormattingEnabled = true;
            this.cbVentas.Items.AddRange(new object[] {
            "Todas las ventas",
            "Mis ventas"});
            this.cbVentas.Location = new System.Drawing.Point(3, 5);
            this.cbVentas.Name = "cbVentas";
            this.cbVentas.Size = new System.Drawing.Size(156, 21);
            this.cbVentas.TabIndex = 0;
            this.cbVentas.Visible = false;
            // 
            // cbTipoVentas
            // 
            this.cbTipoVentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoVentas.FormattingEnabled = true;
            this.cbTipoVentas.Location = new System.Drawing.Point(3, 30);
            this.cbTipoVentas.Name = "cbTipoVentas";
            this.cbTipoVentas.Size = new System.Drawing.Size(240, 21);
            this.cbTipoVentas.TabIndex = 1;
            this.cbTipoVentas.SelectedIndexChanged += new System.EventHandler(this.cbTipoVentas_SelectedIndexChanged);
            this.cbTipoVentas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTipoVentas_KeyDown);
            // 
            // cbTipoRentas
            // 
            this.cbTipoRentas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoRentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoRentas.FormattingEnabled = true;
            this.cbTipoRentas.Location = new System.Drawing.Point(3, 30);
            this.cbTipoRentas.Name = "cbTipoRentas";
            this.cbTipoRentas.Size = new System.Drawing.Size(240, 21);
            this.cbTipoRentas.TabIndex = 20;
            this.cbTipoRentas.Visible = false;
            this.cbTipoRentas.SelectedIndexChanged += new System.EventHandler(this.cbTipoRentas_SelectedIndexChanged);
            this.cbTipoRentas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTipoRentas_KeyDown);
            // 
            // TTMensaje
            // 
            this.TTMensaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.TTMensaje.ForeColor = System.Drawing.Color.White;
            this.TTMensaje.OwnerDraw = true;
            this.TTMensaje.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.TTMensaje_Draw);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnActualizarMaximoProductos);
            this.panel1.Controls.Add(this.txtMaximoPorPagina);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.linkLblPaginaSiguiente);
            this.panel1.Controls.Add(this.linkLblPaginaActual);
            this.panel1.Controls.Add(this.linkLblPaginaAnterior);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.panel1.ForeColor = System.Drawing.Color.Red;
            this.panel1.Location = new System.Drawing.Point(0, 495);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(984, 66);
            this.panel1.TabIndex = 7;
            // 
            // btnActualizarMaximoProductos
            // 
            this.btnActualizarMaximoProductos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnActualizarMaximoProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnActualizarMaximoProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizarMaximoProductos.FlatAppearance.BorderSize = 0;
            this.btnActualizarMaximoProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarMaximoProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarMaximoProductos.ForeColor = System.Drawing.Color.Black;
            this.btnActualizarMaximoProductos.Location = new System.Drawing.Point(833, 25);
            this.btnActualizarMaximoProductos.Name = "btnActualizarMaximoProductos";
            this.btnActualizarMaximoProductos.Size = new System.Drawing.Size(89, 23);
            this.btnActualizarMaximoProductos.TabIndex = 29;
            this.btnActualizarMaximoProductos.Text = "Actualizar";
            this.btnActualizarMaximoProductos.UseVisualStyleBackColor = false;
            this.btnActualizarMaximoProductos.Click += new System.EventHandler(this.btnActualizarMaximoProductos_Click);
            // 
            // txtMaximoPorPagina
            // 
            this.txtMaximoPorPagina.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtMaximoPorPagina.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaximoPorPagina.Location = new System.Drawing.Point(767, 26);
            this.txtMaximoPorPagina.Name = "txtMaximoPorPagina";
            this.txtMaximoPorPagina.Size = new System.Drawing.Size(56, 20);
            this.txtMaximoPorPagina.TabIndex = 28;
            this.txtMaximoPorPagina.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMaximoPorPagina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaximoPorPagina_KeyDown);
            this.txtMaximoPorPagina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaximoPorPagina_KeyPress);
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel4.Controls.Add(this.btnSiguiente);
            this.panel4.Controls.Add(this.btnUltimaPagina);
            this.panel4.Location = new System.Drawing.Point(540, 20);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(59, 32);
            this.panel4.TabIndex = 26;
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSiguiente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSiguiente.FlatAppearance.BorderSize = 0;
            this.btnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSiguiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSiguiente.Image = global::PuntoDeVentaV2.Properties.Resources.angle_right;
            this.btnSiguiente.Location = new System.Drawing.Point(6, 6);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(20, 20);
            this.btnSiguiente.TabIndex = 11;
            this.btnSiguiente.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnSiguiente.UseVisualStyleBackColor = false;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnUltimaPagina
            // 
            this.btnUltimaPagina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnUltimaPagina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUltimaPagina.FlatAppearance.BorderSize = 0;
            this.btnUltimaPagina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUltimaPagina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUltimaPagina.Image = global::PuntoDeVentaV2.Properties.Resources.angle_double_right;
            this.btnUltimaPagina.Location = new System.Drawing.Point(31, 6);
            this.btnUltimaPagina.Name = "btnUltimaPagina";
            this.btnUltimaPagina.Size = new System.Drawing.Size(20, 20);
            this.btnUltimaPagina.TabIndex = 12;
            this.btnUltimaPagina.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnUltimaPagina.UseVisualStyleBackColor = false;
            this.btnUltimaPagina.Click += new System.EventHandler(this.btnUltimaPagina_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel3.Controls.Add(this.btnPrimeraPagina);
            this.panel3.Controls.Add(this.btnAnterior);
            this.panel3.Location = new System.Drawing.Point(385, 20);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(56, 32);
            this.panel3.TabIndex = 25;
            // 
            // btnPrimeraPagina
            // 
            this.btnPrimeraPagina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnPrimeraPagina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrimeraPagina.FlatAppearance.BorderSize = 0;
            this.btnPrimeraPagina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrimeraPagina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrimeraPagina.Image = global::PuntoDeVentaV2.Properties.Resources.angle_double_left;
            this.btnPrimeraPagina.Location = new System.Drawing.Point(6, 6);
            this.btnPrimeraPagina.Name = "btnPrimeraPagina";
            this.btnPrimeraPagina.Size = new System.Drawing.Size(20, 20);
            this.btnPrimeraPagina.TabIndex = 9;
            this.btnPrimeraPagina.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrimeraPagina.UseVisualStyleBackColor = false;
            this.btnPrimeraPagina.Click += new System.EventHandler(this.btnPrimeraPagina_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAnterior.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnterior.FlatAppearance.BorderSize = 0;
            this.btnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAnterior.Image = global::PuntoDeVentaV2.Properties.Resources.angle_left;
            this.btnAnterior.Location = new System.Drawing.Point(31, 6);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(20, 20);
            this.btnAnterior.TabIndex = 10;
            this.btnAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // linkLblPaginaSiguiente
            // 
            this.linkLblPaginaSiguiente.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkLblPaginaSiguiente.AutoSize = true;
            this.linkLblPaginaSiguiente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLblPaginaSiguiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblPaginaSiguiente.Location = new System.Drawing.Point(512, 28);
            this.linkLblPaginaSiguiente.Name = "linkLblPaginaSiguiente";
            this.linkLblPaginaSiguiente.Size = new System.Drawing.Size(14, 16);
            this.linkLblPaginaSiguiente.TabIndex = 24;
            this.linkLblPaginaSiguiente.TabStop = true;
            this.linkLblPaginaSiguiente.Text = "3";
            this.linkLblPaginaSiguiente.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblPaginaSiguiente_LinkClicked);
            // 
            // linkLblPaginaActual
            // 
            this.linkLblPaginaActual.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkLblPaginaActual.AutoSize = true;
            this.linkLblPaginaActual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLblPaginaActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblPaginaActual.Location = new System.Drawing.Point(485, 28);
            this.linkLblPaginaActual.Name = "linkLblPaginaActual";
            this.linkLblPaginaActual.Size = new System.Drawing.Size(14, 16);
            this.linkLblPaginaActual.TabIndex = 23;
            this.linkLblPaginaActual.TabStop = true;
            this.linkLblPaginaActual.Text = "2";
            this.linkLblPaginaActual.Click += new System.EventHandler(this.linkLblPaginaActual_Click);
            // 
            // linkLblPaginaAnterior
            // 
            this.linkLblPaginaAnterior.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkLblPaginaAnterior.AutoSize = true;
            this.linkLblPaginaAnterior.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLblPaginaAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblPaginaAnterior.Location = new System.Drawing.Point(455, 28);
            this.linkLblPaginaAnterior.Name = "linkLblPaginaAnterior";
            this.linkLblPaginaAnterior.Size = new System.Drawing.Size(14, 16);
            this.linkLblPaginaAnterior.TabIndex = 22;
            this.linkLblPaginaAnterior.TabStop = true;
            this.linkLblPaginaAnterior.Text = "1";
            this.linkLblPaginaAnterior.Click += new System.EventHandler(this.linkLblPaginaAnterior_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(660, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 51);
            this.label7.TabIndex = 27;
            this.label7.Text = "Cantidad de ventas para mostrar: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pBar_descarga_verpdf
            // 
            this.pBar_descarga_verpdf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pBar_descarga_verpdf.Location = new System.Drawing.Point(136, 421);
            this.pBar_descarga_verpdf.Name = "pBar_descarga_verpdf";
            this.pBar_descarga_verpdf.Size = new System.Drawing.Size(742, 23);
            this.pBar_descarga_verpdf.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pBar_descarga_verpdf.TabIndex = 8;
            this.pBar_descarga_verpdf.Visible = false;
            // 
            // lb_texto_descarga_verpdf
            // 
            this.lb_texto_descarga_verpdf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_texto_descarga_verpdf.AutoSize = true;
            this.lb_texto_descarga_verpdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_texto_descarga_verpdf.ForeColor = System.Drawing.Color.Red;
            this.lb_texto_descarga_verpdf.Location = new System.Drawing.Point(393, 447);
            this.lb_texto_descarga_verpdf.Name = "lb_texto_descarga_verpdf";
            this.lb_texto_descarga_verpdf.Size = new System.Drawing.Size(157, 20);
            this.lb_texto_descarga_verpdf.TabIndex = 9;
            this.lb_texto_descarga_verpdf.Text = "Descargando nota";
            this.lb_texto_descarga_verpdf.Visible = false;
            // 
            // lb_txt_ruta_descargar
            // 
            this.lb_txt_ruta_descargar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_txt_ruta_descargar.AutoSize = true;
            this.lb_txt_ruta_descargar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_txt_ruta_descargar.ForeColor = System.Drawing.Color.Red;
            this.lb_txt_ruta_descargar.Location = new System.Drawing.Point(448, 473);
            this.lb_txt_ruta_descargar.Name = "lb_txt_ruta_descargar";
            this.lb_txt_ruta_descargar.Size = new System.Drawing.Size(57, 20);
            this.lb_txt_ruta_descargar.TabIndex = 11;
            this.lb_txt_ruta_descargar.Text = "label1";
            this.lb_txt_ruta_descargar.Visible = false;
            // 
            // chTodos
            // 
            this.chTodos.AutoSize = true;
            this.chTodos.Location = new System.Drawing.Point(15, 171);
            this.chTodos.Name = "chTodos";
            this.chTodos.Size = new System.Drawing.Size(114, 17);
            this.chTodos.TabIndex = 12;
            this.chTodos.Text = "Seleccionar todos ";
            this.chTodos.UseVisualStyleBackColor = true;
            this.chTodos.CheckedChanged += new System.EventHandler(this.chTodos_CheckedChanged);
            // 
            // chkHDAutlan
            // 
            this.chkHDAutlan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkHDAutlan.AutoSize = true;
            this.chkHDAutlan.Location = new System.Drawing.Point(802, 172);
            this.chkHDAutlan.Name = "chkHDAutlan";
            this.chkHDAutlan.Size = new System.Drawing.Size(170, 17);
            this.chkHDAutlan.TabIndex = 68;
            this.chkHDAutlan.Text = "PDF Producto Con Descuento";
            this.chkHDAutlan.UseVisualStyleBackColor = true;
            this.chkHDAutlan.Visible = false;
            this.chkHDAutlan.CheckedChanged += new System.EventHandler(this.chkHDAutlan_CheckedChanged);
            this.chkHDAutlan.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkHDAutlan_MouseClick);
            // 
            // rbVentas
            // 
            this.rbVentas.AutoSize = true;
            this.rbVentas.Checked = true;
            this.rbVentas.Location = new System.Drawing.Point(15, 34);
            this.rbVentas.Name = "rbVentas";
            this.rbVentas.Size = new System.Drawing.Size(68, 17);
            this.rbVentas.TabIndex = 69;
            this.rbVentas.TabStop = true;
            this.rbVentas.Text = "VENTAS";
            this.rbVentas.UseVisualStyleBackColor = true;
            this.rbVentas.CheckedChanged += new System.EventHandler(this.rbVentas_CheckedChanged);
            // 
            // rbRentas
            // 
            this.rbRentas.AutoSize = true;
            this.rbRentas.Location = new System.Drawing.Point(91, 34);
            this.rbRentas.Name = "rbRentas";
            this.rbRentas.Size = new System.Drawing.Size(69, 17);
            this.rbRentas.TabIndex = 70;
            this.rbRentas.Text = "RENTAS";
            this.rbRentas.UseVisualStyleBackColor = true;
            this.rbRentas.CheckedChanged += new System.EventHandler(this.rbRentas_CheckedChanged);
            // 
            // ListadoVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 561);
            this.Controls.Add(this.rbRentas);
            this.Controls.Add(this.rbVentas);
            this.Controls.Add(this.chkHDAutlan);
            this.Controls.Add(this.chTodos);
            this.Controls.Add(this.lb_txt_ruta_descargar);
            this.Controls.Add(this.lb_texto_descarga_verpdf);
            this.Controls.Add(this.pBar_descarga_verpdf);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.DGVListadoVentas);
            this.Controls.Add(this.tituloSeccion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ListadoVentas";
            this.Text = "ListadoVentas";
            this.Load += new System.EventHandler(this.ListadoVentas_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ListadoVentas_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListadoVentas_KeyDown);
            this.Resize += new System.EventHandler(this.ListadoVentas_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.DGVListadoVentas)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.DataGridView DGVListadoVentas;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnBuscarVentas;
        private System.Windows.Forms.ComboBox cbVentas;
        private System.Windows.Forms.DateTimePicker dpFechaInicial;
        private System.Windows.Forms.DateTimePicker dpFechaFinal;
        private System.Windows.Forms.ToolTip TTMensaje;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnUltimaPagina;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnPrimeraPagina;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.LinkLabel linkLblPaginaSiguiente;
        private System.Windows.Forms.LinkLabel linkLblPaginaActual;
        private System.Windows.Forms.LinkLabel linkLblPaginaAnterior;
        private System.Windows.Forms.Button btn_enviar;
        private System.Windows.Forms.TextBox txtBuscador;
        private System.Windows.Forms.Button btn_descargar;
        private System.Windows.Forms.FolderBrowserDialog elegir_carpeta_descarga;
        private System.Windows.Forms.ProgressBar pBar_descarga_verpdf;
        private System.Windows.Forms.Label lb_texto_descarga_verpdf;
        private System.Windows.Forms.Button btnCrearVentaGlobal;
        public System.Windows.Forms.Button btnNuevaVenta;
        public System.Windows.Forms.ComboBox cbTipoVentas;
        private System.Windows.Forms.Label lb_txt_ruta_descargar;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.CheckBox chTodos;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnActualizarMaximoProductos;
        private System.Windows.Forms.TextBox txtMaximoPorPagina;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.ComboBox cbFiltroAdminEmpleado;
        private System.Windows.Forms.CheckBox chkHDAutlan;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dpHoraInicial;
        private System.Windows.Forms.DateTimePicker dpHoraFinal;
        public System.Windows.Forms.ComboBox cbFormasPago;
        private System.Windows.Forms.DataGridViewCheckBoxColumn col_checkbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn RFC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vendedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Subtotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn IVA;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewImageColumn Cancelar;
        private System.Windows.Forms.DataGridViewImageColumn Factura;
        private System.Windows.Forms.DataGridViewImageColumn Ticket;
        private System.Windows.Forms.DataGridViewImageColumn Abono;
        private System.Windows.Forms.DataGridViewImageColumn Timbrar;
        private System.Windows.Forms.DataGridViewImageColumn cInformacion;
        private System.Windows.Forms.DataGridViewImageColumn retomarVenta;
        private System.Windows.Forms.DataGridViewImageColumn ganancia;
        private System.Windows.Forms.RadioButton rbVentas;
        private System.Windows.Forms.RadioButton rbRentas;
        public System.Windows.Forms.ComboBox cbTipoRentas;
    }
}