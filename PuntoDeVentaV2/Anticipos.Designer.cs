namespace PuntoDeVentaV2
{
    partial class Anticipos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Anticipos));
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBuscarAnticipo = new System.Windows.Forms.TextBox();
            this.dpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.btnNuevoAnticipo = new System.Windows.Forms.Button();
            this.btnBuscarAnticipos = new System.Windows.Forms.Button();
            this.cbAnticipos = new System.Windows.Forms.ComboBox();
            this.DGVAnticipos = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ticket = new System.Windows.Forms.DataGridViewImageColumn();
            this.Status = new System.Windows.Forms.DataGridViewImageColumn();
            this.Devolver = new System.Windows.Forms.DataGridViewImageColumn();
            this.Info = new System.Windows.Forms.DataGridViewImageColumn();
            this.IDVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FormaPago = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TTMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.btnActualizarMaximoProductos = new System.Windows.Forms.Button();
            this.txtMaximoPorPagina = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.linkLblUltimaPagina = new System.Windows.Forms.LinkLabel();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnUltimaPagina = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnPrimeraPagina = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.linkLblPrimeraPagina = new System.Windows.Forms.LinkLabel();
            this.linkLblPaginaSiguiente = new System.Windows.Forms.LinkLabel();
            this.linkLblPaginaActual = new System.Windows.Forms.LinkLabel();
            this.linkLblPaginaAnterior = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panelBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVAnticipos)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(388, 27);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(132, 25);
            this.tituloSeccion.TabIndex = 5;
            this.tituloSeccion.Text = "ANTICIPOS";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelBotones
            // 
            this.panelBotones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBotones.Controls.Add(this.label2);
            this.panelBotones.Controls.Add(this.label1);
            this.panelBotones.Controls.Add(this.txtBuscarAnticipo);
            this.panelBotones.Controls.Add(this.dpFechaFinal);
            this.panelBotones.Controls.Add(this.dpFechaInicial);
            this.panelBotones.Controls.Add(this.btnNuevoAnticipo);
            this.panelBotones.Controls.Add(this.btnBuscarAnticipos);
            this.panelBotones.Controls.Add(this.cbAnticipos);
            this.panelBotones.Location = new System.Drawing.Point(12, 58);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(845, 91);
            this.panelBotones.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(656, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Fecha Final";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(512, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Fecha Inicial";
            // 
            // txtBuscarAnticipo
            // 
            this.txtBuscarAnticipo.Location = new System.Drawing.Point(3, 59);
            this.txtBuscarAnticipo.Name = "txtBuscarAnticipo";
            this.txtBuscarAnticipo.Size = new System.Drawing.Size(466, 20);
            this.txtBuscarAnticipo.TabIndex = 8;
            this.txtBuscarAnticipo.TextChanged += new System.EventHandler(this.txtBuscarAnticipo_TextChanged);
            this.txtBuscarAnticipo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscarAnticipo_KeyDown);
            // 
            // dpFechaFinal
            // 
            this.dpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dpFechaFinal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpFechaFinal.Location = new System.Drawing.Point(627, 59);
            this.dpFechaFinal.Name = "dpFechaFinal";
            this.dpFechaFinal.Size = new System.Drawing.Size(120, 22);
            this.dpFechaFinal.TabIndex = 7;
            // 
            // dpFechaInicial
            // 
            this.dpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dpFechaInicial.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpFechaInicial.Location = new System.Drawing.Point(485, 59);
            this.dpFechaInicial.Name = "dpFechaInicial";
            this.dpFechaInicial.Size = new System.Drawing.Size(120, 22);
            this.dpFechaInicial.TabIndex = 6;
            // 
            // btnNuevoAnticipo
            // 
            this.btnNuevoAnticipo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevoAnticipo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnNuevoAnticipo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoAnticipo.FlatAppearance.BorderSize = 0;
            this.btnNuevoAnticipo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnNuevoAnticipo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnNuevoAnticipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoAnticipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoAnticipo.ForeColor = System.Drawing.Color.White;
            this.btnNuevoAnticipo.Location = new System.Drawing.Point(716, 9);
            this.btnNuevoAnticipo.Name = "btnNuevoAnticipo";
            this.btnNuevoAnticipo.Size = new System.Drawing.Size(125, 24);
            this.btnNuevoAnticipo.TabIndex = 5;
            this.btnNuevoAnticipo.Text = "Nuevo Anticipo";
            this.btnNuevoAnticipo.UseVisualStyleBackColor = false;
            this.btnNuevoAnticipo.Click += new System.EventHandler(this.btnNuevoAnticipo_Click);
            this.btnNuevoAnticipo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnNuevoAnticipo_KeyDown);
            // 
            // btnBuscarAnticipos
            // 
            this.btnBuscarAnticipos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnBuscarAnticipos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarAnticipos.FlatAppearance.BorderSize = 0;
            this.btnBuscarAnticipos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnBuscarAnticipos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnBuscarAnticipos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarAnticipos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarAnticipos.ForeColor = System.Drawing.Color.White;
            this.btnBuscarAnticipos.Location = new System.Drawing.Point(766, 59);
            this.btnBuscarAnticipos.Name = "btnBuscarAnticipos";
            this.btnBuscarAnticipos.Size = new System.Drawing.Size(75, 24);
            this.btnBuscarAnticipos.TabIndex = 4;
            this.btnBuscarAnticipos.Text = "Buscar";
            this.btnBuscarAnticipos.UseVisualStyleBackColor = false;
            this.btnBuscarAnticipos.Click += new System.EventHandler(this.btnBuscarAnticipos_Click);
            this.btnBuscarAnticipos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnBuscarAnticipos_KeyDown);
            // 
            // cbAnticipos
            // 
            this.cbAnticipos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAnticipos.FormattingEnabled = true;
            this.cbAnticipos.Items.AddRange(new object[] {
            "Por usar",
            "Inhabilitados",
            "Usados",
            "Devueltos",
            "Parciales"});
            this.cbAnticipos.Location = new System.Drawing.Point(12, 18);
            this.cbAnticipos.Name = "cbAnticipos";
            this.cbAnticipos.Size = new System.Drawing.Size(185, 21);
            this.cbAnticipos.TabIndex = 0;
            this.cbAnticipos.SelectedIndexChanged += new System.EventHandler(this.cbAnticipos_SelectedIndexChanged);
            this.cbAnticipos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbAnticipos_KeyDown);
            this.cbAnticipos.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cbAnticipos_KeyPress);
            // 
            // DGVAnticipos
            // 
            this.DGVAnticipos.AllowUserToAddRows = false;
            this.DGVAnticipos.AllowUserToDeleteRows = false;
            this.DGVAnticipos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVAnticipos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVAnticipos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Concepto,
            this.Importe,
            this.Cliente,
            this.Empleado,
            this.Fecha,
            this.Ticket,
            this.Status,
            this.Devolver,
            this.Info,
            this.IDVenta,
            this.FormaPago});
            this.DGVAnticipos.Location = new System.Drawing.Point(12, 155);
            this.DGVAnticipos.Name = "DGVAnticipos";
            this.DGVAnticipos.ReadOnly = true;
            this.DGVAnticipos.RowHeadersVisible = false;
            this.DGVAnticipos.Size = new System.Drawing.Size(845, 301);
            this.DGVAnticipos.TabIndex = 8;
            this.DGVAnticipos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVAnticipos_CellClick);
            this.DGVAnticipos.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVAnticipos_CellMouseEnter);
            this.DGVAnticipos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGVAnticipos_KeyDown);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Concepto
            // 
            this.Concepto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Concepto.HeaderText = "Concepto";
            this.Concepto.Name = "Concepto";
            this.Concepto.ReadOnly = true;
            // 
            // Importe
            // 
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            this.Importe.Width = 135;
            // 
            // Cliente
            // 
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            this.Cliente.Width = 135;
            // 
            // Empleado
            // 
            this.Empleado.HeaderText = "Empleado";
            this.Empleado.Name = "Empleado";
            this.Empleado.ReadOnly = true;
            this.Empleado.Width = 135;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 135;
            // 
            // Ticket
            // 
            this.Ticket.HeaderText = "";
            this.Ticket.Name = "Ticket";
            this.Ticket.ReadOnly = true;
            this.Ticket.Width = 35;
            // 
            // Status
            // 
            this.Status.HeaderText = "";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 35;
            // 
            // Devolver
            // 
            this.Devolver.HeaderText = "";
            this.Devolver.Name = "Devolver";
            this.Devolver.ReadOnly = true;
            this.Devolver.Width = 35;
            // 
            // Info
            // 
            this.Info.HeaderText = "";
            this.Info.Name = "Info";
            this.Info.ReadOnly = true;
            this.Info.Width = 35;
            // 
            // IDVenta
            // 
            this.IDVenta.HeaderText = "IDVenta";
            this.IDVenta.Name = "IDVenta";
            this.IDVenta.ReadOnly = true;
            this.IDVenta.Visible = false;
            // 
            // FormaPago
            // 
            this.FormaPago.HeaderText = "FormaPago";
            this.FormaPago.Name = "FormaPago";
            this.FormaPago.ReadOnly = true;
            this.FormaPago.Visible = false;
            // 
            // TTMensaje
            // 
            this.TTMensaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.TTMensaje.ForeColor = System.Drawing.Color.White;
            this.TTMensaje.OwnerDraw = true;
            this.TTMensaje.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.TTMensaje_Draw);
            // 
            // btnActualizarMaximoProductos
            // 
            this.btnActualizarMaximoProductos.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnActualizarMaximoProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnActualizarMaximoProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizarMaximoProductos.FlatAppearance.BorderSize = 0;
            this.btnActualizarMaximoProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarMaximoProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnActualizarMaximoProductos.ForeColor = System.Drawing.Color.Black;
            this.btnActualizarMaximoProductos.Location = new System.Drawing.Point(605, 40);
            this.btnActualizarMaximoProductos.Name = "btnActualizarMaximoProductos";
            this.btnActualizarMaximoProductos.Size = new System.Drawing.Size(80, 23);
            this.btnActualizarMaximoProductos.TabIndex = 52;
            this.btnActualizarMaximoProductos.Text = "Actualizar";
            this.btnActualizarMaximoProductos.UseVisualStyleBackColor = false;
            this.btnActualizarMaximoProductos.Click += new System.EventHandler(this.btnActualizarMaximoProductos_Click_1);
            // 
            // txtMaximoPorPagina
            // 
            this.txtMaximoPorPagina.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtMaximoPorPagina.Location = new System.Drawing.Point(505, 41);
            this.txtMaximoPorPagina.Name = "txtMaximoPorPagina";
            this.txtMaximoPorPagina.Size = new System.Drawing.Size(91, 20);
            this.txtMaximoPorPagina.TabIndex = 51;
            this.txtMaximoPorPagina.Text = "14";
            this.txtMaximoPorPagina.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMaximoPorPagina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaximoPorPagina_KeyPress);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.Location = new System.Drawing.Point(378, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 56);
            this.label7.TabIndex = 50;
            this.label7.Text = "Cantidad de proveedores para mostrar: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel4.Controls.Add(this.linkLblUltimaPagina);
            this.panel4.Controls.Add(this.btnSiguiente);
            this.panel4.Controls.Add(this.btnUltimaPagina);
            this.panel4.Location = new System.Drawing.Point(304, 33);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(59, 32);
            this.panel4.TabIndex = 49;
            // 
            // linkLblUltimaPagina
            // 
            this.linkLblUltimaPagina.AutoSize = true;
            this.linkLblUltimaPagina.Location = new System.Drawing.Point(60, 10);
            this.linkLblUltimaPagina.Name = "linkLblUltimaPagina";
            this.linkLblUltimaPagina.Size = new System.Drawing.Size(25, 13);
            this.linkLblUltimaPagina.TabIndex = 17;
            this.linkLblUltimaPagina.TabStop = true;
            this.linkLblUltimaPagina.Text = "100";
            this.linkLblUltimaPagina.Visible = false;
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSiguiente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSiguiente.FlatAppearance.BorderSize = 0;
            this.btnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSiguiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSiguiente.Image = global::PuntoDeVentaV2.Properties.Resources.angle_right;
            this.btnSiguiente.Location = new System.Drawing.Point(4, 6);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(20, 20);
            this.btnSiguiente.TabIndex = 11;
            this.btnSiguiente.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnSiguiente.UseVisualStyleBackColor = false;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click_1);
            // 
            // btnUltimaPagina
            // 
            this.btnUltimaPagina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnUltimaPagina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUltimaPagina.FlatAppearance.BorderSize = 0;
            this.btnUltimaPagina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUltimaPagina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUltimaPagina.Image = global::PuntoDeVentaV2.Properties.Resources.angle_double_right;
            this.btnUltimaPagina.Location = new System.Drawing.Point(29, 6);
            this.btnUltimaPagina.Name = "btnUltimaPagina";
            this.btnUltimaPagina.Size = new System.Drawing.Size(20, 20);
            this.btnUltimaPagina.TabIndex = 12;
            this.btnUltimaPagina.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnUltimaPagina.UseVisualStyleBackColor = false;
            this.btnUltimaPagina.Click += new System.EventHandler(this.btnUltimaPagina_Click_1);
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel3.Controls.Add(this.btnPrimeraPagina);
            this.panel3.Controls.Add(this.btnAnterior);
            this.panel3.Controls.Add(this.linkLblPrimeraPagina);
            this.panel3.Location = new System.Drawing.Point(170, 33);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(56, 32);
            this.panel3.TabIndex = 48;
            // 
            // btnPrimeraPagina
            // 
            this.btnPrimeraPagina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnPrimeraPagina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrimeraPagina.FlatAppearance.BorderSize = 0;
            this.btnPrimeraPagina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrimeraPagina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrimeraPagina.Image = global::PuntoDeVentaV2.Properties.Resources.angle_double_left;
            this.btnPrimeraPagina.Location = new System.Drawing.Point(-6, 6);
            this.btnPrimeraPagina.Name = "btnPrimeraPagina";
            this.btnPrimeraPagina.Size = new System.Drawing.Size(20, 20);
            this.btnPrimeraPagina.TabIndex = 9;
            this.btnPrimeraPagina.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrimeraPagina.UseVisualStyleBackColor = false;
            this.btnPrimeraPagina.Click += new System.EventHandler(this.btnPrimeraPagina_Click_1);
            // 
            // btnAnterior
            // 
            this.btnAnterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAnterior.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnterior.FlatAppearance.BorderSize = 0;
            this.btnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAnterior.Image = global::PuntoDeVentaV2.Properties.Resources.angle_left;
            this.btnAnterior.Location = new System.Drawing.Point(19, 6);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(20, 20);
            this.btnAnterior.TabIndex = 10;
            this.btnAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click_1);
            // 
            // linkLblPrimeraPagina
            // 
            this.linkLblPrimeraPagina.AutoSize = true;
            this.linkLblPrimeraPagina.Location = new System.Drawing.Point(38, 10);
            this.linkLblPrimeraPagina.Name = "linkLblPrimeraPagina";
            this.linkLblPrimeraPagina.Size = new System.Drawing.Size(13, 13);
            this.linkLblPrimeraPagina.TabIndex = 13;
            this.linkLblPrimeraPagina.TabStop = true;
            this.linkLblPrimeraPagina.Text = "1";
            this.linkLblPrimeraPagina.Visible = false;
            this.linkLblPrimeraPagina.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblPrimeraPagina_LinkClicked);
            // 
            // linkLblPaginaSiguiente
            // 
            this.linkLblPaginaSiguiente.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLblPaginaSiguiente.AutoSize = true;
            this.linkLblPaginaSiguiente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLblPaginaSiguiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblPaginaSiguiente.Location = new System.Drawing.Point(281, 43);
            this.linkLblPaginaSiguiente.Name = "linkLblPaginaSiguiente";
            this.linkLblPaginaSiguiente.Size = new System.Drawing.Size(15, 16);
            this.linkLblPaginaSiguiente.TabIndex = 47;
            this.linkLblPaginaSiguiente.TabStop = true;
            this.linkLblPaginaSiguiente.Text = "4";
            // 
            // linkLblPaginaActual
            // 
            this.linkLblPaginaActual.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLblPaginaActual.AutoSize = true;
            this.linkLblPaginaActual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLblPaginaActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblPaginaActual.Location = new System.Drawing.Point(258, 43);
            this.linkLblPaginaActual.Name = "linkLblPaginaActual";
            this.linkLblPaginaActual.Size = new System.Drawing.Size(15, 16);
            this.linkLblPaginaActual.TabIndex = 46;
            this.linkLblPaginaActual.TabStop = true;
            this.linkLblPaginaActual.Text = "3";
            // 
            // linkLblPaginaAnterior
            // 
            this.linkLblPaginaAnterior.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.linkLblPaginaAnterior.AutoSize = true;
            this.linkLblPaginaAnterior.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLblPaginaAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblPaginaAnterior.Location = new System.Drawing.Point(234, 43);
            this.linkLblPaginaAnterior.Name = "linkLblPaginaAnterior";
            this.linkLblPaginaAnterior.Size = new System.Drawing.Size(15, 16);
            this.linkLblPaginaAnterior.TabIndex = 45;
            this.linkLblPaginaAnterior.TabStop = true;
            this.linkLblPaginaAnterior.Text = "2";
            this.linkLblPaginaAnterior.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblPaginaAnterior_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnActualizarMaximoProductos);
            this.panel1.Controls.Add(this.linkLblPaginaAnterior);
            this.panel1.Controls.Add(this.txtMaximoPorPagina);
            this.panel1.Controls.Add(this.linkLblPaginaActual);
            this.panel1.Controls.Add(this.linkLblPaginaSiguiente);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Location = new System.Drawing.Point(12, 473);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(845, 100);
            this.panel1.TabIndex = 53;
            // 
            // Anticipos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 645);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DGVAnticipos);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.tituloSeccion);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Anticipos";
            this.Load += new System.EventHandler(this.Anticipos_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Anticipos_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Anticipos_KeyDown);
            this.Resize += new System.EventHandler(this.Anticipos_Resize);
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVAnticipos)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.DateTimePicker dpFechaFinal;
        private System.Windows.Forms.DateTimePicker dpFechaInicial;
        private System.Windows.Forms.Button btnNuevoAnticipo;
        private System.Windows.Forms.Button btnBuscarAnticipos;
        private System.Windows.Forms.ComboBox cbAnticipos;
        private System.Windows.Forms.DataGridView DGVAnticipos;
        private System.Windows.Forms.ToolTip TTMensaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Empleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewImageColumn Ticket;
        private System.Windows.Forms.DataGridViewImageColumn Status;
        private System.Windows.Forms.DataGridViewImageColumn Devolver;
        private System.Windows.Forms.DataGridViewImageColumn Info;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDVenta;
        private System.Windows.Forms.DataGridViewTextBoxColumn FormaPago;
        private System.Windows.Forms.TextBox txtBuscarAnticipo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnActualizarMaximoProductos;
        private System.Windows.Forms.TextBox txtMaximoPorPagina;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.LinkLabel linkLblUltimaPagina;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnUltimaPagina;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnPrimeraPagina;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.LinkLabel linkLblPrimeraPagina;
        private System.Windows.Forms.LinkLabel linkLblPaginaSiguiente;
        private System.Windows.Forms.LinkLabel linkLblPaginaActual;
        private System.Windows.Forms.LinkLabel linkLblPaginaAnterior;
        private System.Windows.Forms.Panel panel1;
    }
}