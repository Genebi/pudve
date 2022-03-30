namespace PuntoDeVentaV2
{
    partial class Clientes
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
            this.DGVClientes = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RFC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreComercial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NoCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Editar = new System.Windows.Forms.DataGridViewImageColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewImageColumn();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscador = new System.Windows.Forms.TextBox();
            this.btnListaDescuentos = new System.Windows.Forms.Button();
            this.btnTipoCliente = new System.Windows.Forms.Button();
            this.btnNuevoCliente = new System.Windows.Forms.Button();
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.panelAbajo = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.btnActualizarMaximoProductos = new System.Windows.Forms.Button();
            this.txtMaximoPorPagina = new System.Windows.Forms.TextBox();
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
            ((System.ComponentModel.ISupportInitialize)(this.DGVClientes)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.panelAbajo.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGVClientes
            // 
            this.DGVClientes.AllowUserToAddRows = false;
            this.DGVClientes.AllowUserToDeleteRows = false;
            this.DGVClientes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.RFC,
            this.Cliente,
            this.NombreComercial,
            this.Tipo,
            this.NoCliente,
            this.Fecha,
            this.Editar,
            this.Eliminar});
            this.DGVClientes.Location = new System.Drawing.Point(12, 157);
            this.DGVClientes.Name = "DGVClientes";
            this.DGVClientes.ReadOnly = true;
            this.DGVClientes.RowHeadersVisible = false;
            this.DGVClientes.Size = new System.Drawing.Size(1201, 200);
            this.DGVClientes.TabIndex = 11;
            this.DGVClientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVClientes_CellClick);
            this.DGVClientes.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVClientes_CellMouseEnter);
            this.DGVClientes.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVClientes_CellMouseLeave);
            this.DGVClientes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGVClientes_KeyDown);
            this.DGVClientes.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.DGVClientes_MouseDoubleClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 200;
            // 
            // RFC
            // 
            this.RFC.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.RFC.HeaderText = "RFC";
            this.RFC.MinimumWidth = 100;
            this.RFC.Name = "RFC";
            this.RFC.ReadOnly = true;
            this.RFC.Width = 120;
            // 
            // Cliente
            // 
            this.Cliente.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cliente.FillWeight = 23.2687F;
            this.Cliente.HeaderText = "Razón Social";
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            // 
            // NombreComercial
            // 
            this.NombreComercial.FillWeight = 249.0413F;
            this.NombreComercial.HeaderText = "Nombre Comercial";
            this.NombreComercial.Name = "NombreComercial";
            this.NombreComercial.ReadOnly = true;
            this.NombreComercial.Width = 257;
            // 
            // Tipo
            // 
            this.Tipo.FillWeight = 106.0011F;
            this.Tipo.HeaderText = "Tipo Cliente";
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            this.Tipo.Width = 109;
            // 
            // NoCliente
            // 
            this.NoCliente.FillWeight = 73.20717F;
            this.NoCliente.HeaderText = "No. Cliente";
            this.NoCliente.Name = "NoCliente";
            this.NoCliente.ReadOnly = true;
            this.NoCliente.Width = 76;
            // 
            // Fecha
            // 
            this.Fecha.FillWeight = 125.0244F;
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 129;
            // 
            // Editar
            // 
            this.Editar.FillWeight = 44.92147F;
            this.Editar.HeaderText = "Editar";
            this.Editar.Name = "Editar";
            this.Editar.ReadOnly = true;
            this.Editar.Width = 46;
            // 
            // Eliminar
            // 
            this.Eliminar.FillWeight = 78.53589F;
            this.Eliminar.HeaderText = "Deshabilitar";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.ReadOnly = true;
            this.Eliminar.Width = 81;
            // 
            // panelBotones
            // 
            this.panelBotones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBotones.Controls.Add(this.cbStatus);
            this.panelBotones.Controls.Add(this.btnBuscar);
            this.panelBotones.Controls.Add(this.txtBuscador);
            this.panelBotones.Controls.Add(this.btnListaDescuentos);
            this.panelBotones.Controls.Add(this.btnTipoCliente);
            this.panelBotones.Controls.Add(this.btnNuevoCliente);
            this.panelBotones.Location = new System.Drawing.Point(12, 72);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(1201, 79);
            this.panelBotones.TabIndex = 10;
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "Habilitados",
            "Deshabilitados"});
            this.cbStatus.Location = new System.Drawing.Point(3, 14);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(187, 25);
            this.cbStatus.TabIndex = 24;
            this.cbStatus.SelectionChangeCommitted += new System.EventHandler(this.cbStatus_SelectionChangeCommitted);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(301, 50);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 24);
            this.btnBuscar.TabIndex = 23;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.btnBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnBuscar_KeyDown);
            // 
            // txtBuscador
            // 
            this.txtBuscador.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscador.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscador.Location = new System.Drawing.Point(3, 51);
            this.txtBuscador.Name = "txtBuscador";
            this.txtBuscador.Size = new System.Drawing.Size(292, 23);
            this.txtBuscador.TabIndex = 22;
            this.txtBuscador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscador_KeyDown);
            // 
            // btnListaDescuentos
            // 
            this.btnListaDescuentos.BackColor = System.Drawing.Color.Green;
            this.btnListaDescuentos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnListaDescuentos.FlatAppearance.BorderSize = 0;
            this.btnListaDescuentos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnListaDescuentos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListaDescuentos.ForeColor = System.Drawing.Color.White;
            this.btnListaDescuentos.Location = new System.Drawing.Point(536, 50);
            this.btnListaDescuentos.Name = "btnListaDescuentos";
            this.btnListaDescuentos.Size = new System.Drawing.Size(140, 24);
            this.btnListaDescuentos.TabIndex = 21;
            this.btnListaDescuentos.Text = "Listado tipo cliente";
            this.btnListaDescuentos.UseVisualStyleBackColor = false;
            this.btnListaDescuentos.Click += new System.EventHandler(this.btnListaDescuentos_Click);
            this.btnListaDescuentos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnListaDescuentos_KeyDown);
            // 
            // btnTipoCliente
            // 
            this.btnTipoCliente.BackColor = System.Drawing.Color.Green;
            this.btnTipoCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTipoCliente.FlatAppearance.BorderSize = 0;
            this.btnTipoCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTipoCliente.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTipoCliente.ForeColor = System.Drawing.Color.White;
            this.btnTipoCliente.Location = new System.Drawing.Point(390, 50);
            this.btnTipoCliente.Name = "btnTipoCliente";
            this.btnTipoCliente.Size = new System.Drawing.Size(140, 24);
            this.btnTipoCliente.TabIndex = 20;
            this.btnTipoCliente.Text = "Nuevo tipo cliente";
            this.btnTipoCliente.UseVisualStyleBackColor = false;
            this.btnTipoCliente.Click += new System.EventHandler(this.btnTipoCliente_Click);
            this.btnTipoCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnTipoCliente_KeyDown);
            // 
            // btnNuevoCliente
            // 
            this.btnNuevoCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevoCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnNuevoCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoCliente.FlatAppearance.BorderSize = 0;
            this.btnNuevoCliente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnNuevoCliente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnNuevoCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoCliente.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoCliente.ForeColor = System.Drawing.Color.White;
            this.btnNuevoCliente.Location = new System.Drawing.Point(1072, 50);
            this.btnNuevoCliente.Name = "btnNuevoCliente";
            this.btnNuevoCliente.Size = new System.Drawing.Size(125, 24);
            this.btnNuevoCliente.TabIndex = 5;
            this.btnNuevoCliente.Text = "Nuevo Cliente";
            this.btnNuevoCliente.UseVisualStyleBackColor = false;
            this.btnNuevoCliente.Click += new System.EventHandler(this.btnNuevoCliente_Click);
            this.btnNuevoCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnNuevoCliente_KeyDown);
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(566, 27);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(101, 25);
            this.tituloSeccion.TabIndex = 9;
            this.tituloSeccion.Text = "CLIENTES";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelAbajo
            // 
            this.panelAbajo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panelAbajo.Controls.Add(this.label7);
            this.panelAbajo.Controls.Add(this.btnActualizarMaximoProductos);
            this.panelAbajo.Controls.Add(this.txtMaximoPorPagina);
            this.panelAbajo.Controls.Add(this.panel4);
            this.panelAbajo.Controls.Add(this.panel3);
            this.panelAbajo.Controls.Add(this.linkLblPaginaSiguiente);
            this.panelAbajo.Controls.Add(this.linkLblPaginaActual);
            this.panelAbajo.Controls.Add(this.linkLblPaginaAnterior);
            this.panelAbajo.Location = new System.Drawing.Point(12, 361);
            this.panelAbajo.Name = "panelAbajo";
            this.panelAbajo.Size = new System.Drawing.Size(1201, 100);
            this.panelAbajo.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.Location = new System.Drawing.Point(742, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 56);
            this.label7.TabIndex = 44;
            this.label7.Text = "Cantidad de clientes para mostrar: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnActualizarMaximoProductos
            // 
            this.btnActualizarMaximoProductos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnActualizarMaximoProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnActualizarMaximoProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizarMaximoProductos.FlatAppearance.BorderSize = 0;
            this.btnActualizarMaximoProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarMaximoProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnActualizarMaximoProductos.ForeColor = System.Drawing.Color.Black;
            this.btnActualizarMaximoProductos.Location = new System.Drawing.Point(975, 41);
            this.btnActualizarMaximoProductos.Name = "btnActualizarMaximoProductos";
            this.btnActualizarMaximoProductos.Size = new System.Drawing.Size(80, 23);
            this.btnActualizarMaximoProductos.TabIndex = 43;
            this.btnActualizarMaximoProductos.Text = "Actualizar";
            this.btnActualizarMaximoProductos.UseVisualStyleBackColor = false;
            this.btnActualizarMaximoProductos.Click += new System.EventHandler(this.btnActualizarMaximoProductos_Click);
            // 
            // txtMaximoPorPagina
            // 
            this.txtMaximoPorPagina.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtMaximoPorPagina.Location = new System.Drawing.Point(874, 42);
            this.txtMaximoPorPagina.Name = "txtMaximoPorPagina";
            this.txtMaximoPorPagina.Size = new System.Drawing.Size(91, 20);
            this.txtMaximoPorPagina.TabIndex = 42;
            this.txtMaximoPorPagina.Text = "14";
            this.txtMaximoPorPagina.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMaximoPorPagina.TextChanged += new System.EventHandler(this.txtMaximoPorPagina_TextChanged);
            this.txtMaximoPorPagina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaximoPorPagina_KeyDown);
            this.txtMaximoPorPagina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaximoPorPagina_KeyPress);
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel4.Controls.Add(this.linkLblUltimaPagina);
            this.panel4.Controls.Add(this.btnSiguiente);
            this.panel4.Controls.Add(this.btnUltimaPagina);
            this.panel4.Location = new System.Drawing.Point(651, 34);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(59, 32);
            this.panel4.TabIndex = 26;
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
            this.btnSiguiente.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
            this.btnUltimaPagina.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
            this.panel3.Controls.Add(this.linkLblPrimeraPagina);
            this.panel3.Location = new System.Drawing.Point(517, 34);
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
            this.btnPrimeraPagina.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
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
            this.btnAnterior.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAnterior.Image = global::PuntoDeVentaV2.Properties.Resources.angle_left;
            this.btnAnterior.Location = new System.Drawing.Point(31, 6);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(20, 20);
            this.btnAnterior.TabIndex = 10;
            this.btnAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
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
            // 
            // linkLblPaginaSiguiente
            // 
            this.linkLblPaginaSiguiente.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkLblPaginaSiguiente.AutoSize = true;
            this.linkLblPaginaSiguiente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLblPaginaSiguiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblPaginaSiguiente.Location = new System.Drawing.Point(628, 44);
            this.linkLblPaginaSiguiente.Name = "linkLblPaginaSiguiente";
            this.linkLblPaginaSiguiente.Size = new System.Drawing.Size(15, 16);
            this.linkLblPaginaSiguiente.TabIndex = 24;
            this.linkLblPaginaSiguiente.TabStop = true;
            this.linkLblPaginaSiguiente.Text = "4";
            this.linkLblPaginaSiguiente.Click += new System.EventHandler(this.linkLblPaginaSiguiente_Click);
            // 
            // linkLblPaginaActual
            // 
            this.linkLblPaginaActual.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkLblPaginaActual.AutoSize = true;
            this.linkLblPaginaActual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLblPaginaActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblPaginaActual.Location = new System.Drawing.Point(605, 44);
            this.linkLblPaginaActual.Name = "linkLblPaginaActual";
            this.linkLblPaginaActual.Size = new System.Drawing.Size(15, 16);
            this.linkLblPaginaActual.TabIndex = 23;
            this.linkLblPaginaActual.TabStop = true;
            this.linkLblPaginaActual.Text = "3";
            this.linkLblPaginaActual.Click += new System.EventHandler(this.linkLblPaginaActual_Click);
            // 
            // linkLblPaginaAnterior
            // 
            this.linkLblPaginaAnterior.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkLblPaginaAnterior.AutoSize = true;
            this.linkLblPaginaAnterior.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLblPaginaAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblPaginaAnterior.Location = new System.Drawing.Point(581, 44);
            this.linkLblPaginaAnterior.Name = "linkLblPaginaAnterior";
            this.linkLblPaginaAnterior.Size = new System.Drawing.Size(15, 16);
            this.linkLblPaginaAnterior.TabIndex = 22;
            this.linkLblPaginaAnterior.TabStop = true;
            this.linkLblPaginaAnterior.Text = "2";
            this.linkLblPaginaAnterior.Click += new System.EventHandler(this.linkLblPaginaAnterior_Click);
            // 
            // Clientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1225, 561);
            this.Controls.Add(this.panelAbajo);
            this.Controls.Add(this.DGVClientes);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.tituloSeccion);
            this.Name = "Clientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Clientes";
            this.Load += new System.EventHandler(this.Clientes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Clientes_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVClientes)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.panelAbajo.ResumeLayout(false);
            this.panelAbajo.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVClientes;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnNuevoCliente;
        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.Button btnListaDescuentos;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscador;
        private System.Windows.Forms.Panel panelAbajo;
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
        public System.Windows.Forms.Button btnTipoCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RFC;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreComercial;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NoCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewImageColumn Editar;
        private System.Windows.Forms.DataGridViewImageColumn Eliminar;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Button btnActualizarMaximoProductos;
        private System.Windows.Forms.TextBox txtMaximoPorPagina;
        private System.Windows.Forms.Label label7;
    }
}