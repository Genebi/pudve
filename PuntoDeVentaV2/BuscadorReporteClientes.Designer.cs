namespace PuntoDeVentaV2
{
    partial class BuscadorReporteClientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BuscadorReporteClientes));
            this.DGVReportesClientes = new System.Windows.Forms.DataGridView();
            this.marcar = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RFC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ArticulosBuy = new System.Windows.Forms.DataGridViewImageColumn();
            this.ArticulosNotBuy = new System.Windows.Forms.DataGridViewImageColumn();
            this.DatosCliente = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
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
            this.btnActualizarMaximoProductos = new System.Windows.Forms.Button();
            this.txtMaximoPorPagina = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCantidadRegistros = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnComprados = new PuntoDeVentaV2.BotonRedondo();
            this.btnDatosCLiente = new PuntoDeVentaV2.BotonRedondo();
            this.btnNoComprados = new PuntoDeVentaV2.BotonRedondo();
            ((System.ComponentModel.ISupportInitialize)(this.DGVReportesClientes)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGVReportesClientes
            // 
            this.DGVReportesClientes.AllowUserToAddRows = false;
            this.DGVReportesClientes.AllowUserToDeleteRows = false;
            this.DGVReportesClientes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVReportesClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVReportesClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.marcar,
            this.ID,
            this.Nombre,
            this.RFC,
            this.ArticulosBuy,
            this.ArticulosNotBuy,
            this.DatosCliente});
            this.DGVReportesClientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.DGVReportesClientes.Location = new System.Drawing.Point(12, 101);
            this.DGVReportesClientes.Name = "DGVReportesClientes";
            this.DGVReportesClientes.RowHeadersVisible = false;
            this.DGVReportesClientes.Size = new System.Drawing.Size(893, 283);
            this.DGVReportesClientes.TabIndex = 0;
            this.DGVReportesClientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVReportesClientes_CellClick);
            this.DGVReportesClientes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVReportesClientes_CellContentClick);
            // 
            // marcar
            // 
            this.marcar.HeaderText = "";
            this.marcar.Name = "marcar";
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // Nombre
            // 
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // RFC
            // 
            this.RFC.HeaderText = "RFC";
            this.RFC.Name = "RFC";
            this.RFC.ReadOnly = true;
            // 
            // ArticulosBuy
            // 
            this.ArticulosBuy.HeaderText = "Articulos comprados";
            this.ArticulosBuy.Name = "ArticulosBuy";
            this.ArticulosBuy.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ArticulosBuy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ArticulosNotBuy
            // 
            this.ArticulosNotBuy.HeaderText = "Articulos no comprados";
            this.ArticulosNotBuy.Name = "ArticulosNotBuy";
            this.ArticulosNotBuy.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ArticulosNotBuy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DatosCliente
            // 
            this.DatosCliente.HeaderText = "Datos del cliente";
            this.DatosCliente.Name = "DatosCliente";
            this.DatosCliente.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DatosCliente.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::PuntoDeVentaV2.Properties.Resources.search;
            this.btnBuscar.Location = new System.Drawing.Point(818, 58);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(87, 23);
            this.btnBuscar.TabIndex = 1;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(12, 60);
            this.txtBuscar.Multiline = true;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(755, 23);
            this.txtBuscar.TabIndex = 2;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.txtBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(401, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Reportes de clientes";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel4);
            this.panel5.Controls.Add(this.panel3);
            this.panel5.Controls.Add(this.linkLblPaginaSiguiente);
            this.panel5.Controls.Add(this.linkLblPaginaActual);
            this.panel5.Controls.Add(this.linkLblPaginaAnterior);
            this.panel5.Controls.Add(this.btnActualizarMaximoProductos);
            this.panel5.Controls.Add(this.txtMaximoPorPagina);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.lblCantidadRegistros);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Location = new System.Drawing.Point(168, 398);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(703, 43);
            this.panel5.TabIndex = 40;
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel4.Controls.Add(this.linkLblUltimaPagina);
            this.panel4.Controls.Add(this.btnSiguiente);
            this.panel4.Controls.Add(this.btnUltimaPagina);
            this.panel4.Location = new System.Drawing.Point(337, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(59, 32);
            this.panel4.TabIndex = 46;
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
            this.panel3.Controls.Add(this.linkLblPrimeraPagina);
            this.panel3.Location = new System.Drawing.Point(203, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(56, 32);
            this.panel3.TabIndex = 45;
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
            this.linkLblPaginaSiguiente.Location = new System.Drawing.Point(314, 16);
            this.linkLblPaginaSiguiente.Name = "linkLblPaginaSiguiente";
            this.linkLblPaginaSiguiente.Size = new System.Drawing.Size(15, 16);
            this.linkLblPaginaSiguiente.TabIndex = 44;
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
            this.linkLblPaginaActual.Location = new System.Drawing.Point(291, 16);
            this.linkLblPaginaActual.Name = "linkLblPaginaActual";
            this.linkLblPaginaActual.Size = new System.Drawing.Size(15, 16);
            this.linkLblPaginaActual.TabIndex = 43;
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
            this.linkLblPaginaAnterior.Location = new System.Drawing.Point(267, 16);
            this.linkLblPaginaAnterior.Name = "linkLblPaginaAnterior";
            this.linkLblPaginaAnterior.Size = new System.Drawing.Size(15, 16);
            this.linkLblPaginaAnterior.TabIndex = 42;
            this.linkLblPaginaAnterior.TabStop = true;
            this.linkLblPaginaAnterior.Text = "2";
            this.linkLblPaginaAnterior.Click += new System.EventHandler(this.linkLblPaginaAnterior_Click);
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
            this.btnActualizarMaximoProductos.Location = new System.Drawing.Point(603, 11);
            this.btnActualizarMaximoProductos.Name = "btnActualizarMaximoProductos";
            this.btnActualizarMaximoProductos.Size = new System.Drawing.Size(80, 23);
            this.btnActualizarMaximoProductos.TabIndex = 41;
            this.btnActualizarMaximoProductos.Text = "Actualizar";
            this.btnActualizarMaximoProductos.UseVisualStyleBackColor = false;
            this.btnActualizarMaximoProductos.Click += new System.EventHandler(this.btnActualizarMaximoProductos_Click);
            // 
            // txtMaximoPorPagina
            // 
            this.txtMaximoPorPagina.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtMaximoPorPagina.Location = new System.Drawing.Point(537, 12);
            this.txtMaximoPorPagina.Name = "txtMaximoPorPagina";
            this.txtMaximoPorPagina.Size = new System.Drawing.Size(56, 20);
            this.txtMaximoPorPagina.TabIndex = 40;
            this.txtMaximoPorPagina.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMaximoPorPagina.TextChanged += new System.EventHandler(this.txtMaximoPorPagina_TextChanged);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.Location = new System.Drawing.Point(410, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 32);
            this.label7.TabIndex = 39;
            this.label7.Text = "Cantidad de productos para mostrar: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // lblCantidadRegistros
            // 
            this.lblCantidadRegistros.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCantidadRegistros.BackColor = System.Drawing.SystemColors.Control;
            this.lblCantidadRegistros.ForeColor = System.Drawing.Color.Blue;
            this.lblCantidadRegistros.Location = new System.Drawing.Point(134, 11);
            this.lblCantidadRegistros.Name = "lblCantidadRegistros";
            this.lblCantidadRegistros.Size = new System.Drawing.Size(62, 23);
            this.lblCantidadRegistros.TabIndex = 38;
            this.lblCantidadRegistros.Text = "0";
            this.lblCantidadRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCantidadRegistros.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 26);
            this.label4.TabIndex = 37;
            this.label4.Text = "Total de productos \r\nencontrados:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // btnComprados
            // 
            this.btnComprados.BackColor = System.Drawing.Color.Red;
            this.btnComprados.BackGroundColor = System.Drawing.Color.Red;
            this.btnComprados.BorderColor = System.Drawing.Color.Red;
            this.btnComprados.BorderRadius = 40;
            this.btnComprados.BorderSize = 0;
            this.btnComprados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnComprados.FlatAppearance.BorderSize = 0;
            this.btnComprados.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnComprados.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnComprados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnComprados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComprados.ForeColor = System.Drawing.Color.White;
            this.btnComprados.Location = new System.Drawing.Point(44, 447);
            this.btnComprados.Name = "btnComprados";
            this.btnComprados.Size = new System.Drawing.Size(215, 42);
            this.btnComprados.TabIndex = 44;
            this.btnComprados.Text = "Articulos Comprados";
            this.btnComprados.TextColor = System.Drawing.Color.White;
            this.btnComprados.UseVisualStyleBackColor = false;
            this.btnComprados.Click += new System.EventHandler(this.btnComprados_Click_1);
            // 
            // btnDatosCLiente
            // 
            this.btnDatosCLiente.BackColor = System.Drawing.Color.Red;
            this.btnDatosCLiente.BackGroundColor = System.Drawing.Color.Red;
            this.btnDatosCLiente.BorderColor = System.Drawing.Color.Red;
            this.btnDatosCLiente.BorderRadius = 40;
            this.btnDatosCLiente.BorderSize = 0;
            this.btnDatosCLiente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDatosCLiente.FlatAppearance.BorderSize = 0;
            this.btnDatosCLiente.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnDatosCLiente.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnDatosCLiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDatosCLiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDatosCLiente.ForeColor = System.Drawing.Color.White;
            this.btnDatosCLiente.Location = new System.Drawing.Point(672, 448);
            this.btnDatosCLiente.Name = "btnDatosCLiente";
            this.btnDatosCLiente.Size = new System.Drawing.Size(215, 42);
            this.btnDatosCLiente.TabIndex = 45;
            this.btnDatosCLiente.Text = "Datos de clientes";
            this.btnDatosCLiente.TextColor = System.Drawing.Color.White;
            this.btnDatosCLiente.UseVisualStyleBackColor = false;
            this.btnDatosCLiente.Click += new System.EventHandler(this.btnDatosCLiente_Click_1);
            // 
            // btnNoComprados
            // 
            this.btnNoComprados.BackColor = System.Drawing.Color.Red;
            this.btnNoComprados.BackGroundColor = System.Drawing.Color.Red;
            this.btnNoComprados.BorderColor = System.Drawing.Color.Red;
            this.btnNoComprados.BorderRadius = 40;
            this.btnNoComprados.BorderSize = 0;
            this.btnNoComprados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNoComprados.FlatAppearance.BorderSize = 0;
            this.btnNoComprados.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnNoComprados.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnNoComprados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNoComprados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNoComprados.ForeColor = System.Drawing.Color.White;
            this.btnNoComprados.Location = new System.Drawing.Point(371, 448);
            this.btnNoComprados.Name = "btnNoComprados";
            this.btnNoComprados.Size = new System.Drawing.Size(215, 42);
            this.btnNoComprados.TabIndex = 46;
            this.btnNoComprados.Text = "Articulos no Comprados";
            this.btnNoComprados.TextColor = System.Drawing.Color.White;
            this.btnNoComprados.UseVisualStyleBackColor = false;
            this.btnNoComprados.Click += new System.EventHandler(this.btnNoComprados_Click_1);
            // 
            // BuscadorReporteClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 502);
            this.Controls.Add(this.btnNoComprados);
            this.Controls.Add(this.btnDatosCLiente);
            this.Controls.Add(this.btnComprados);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.DGVReportesClientes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BuscadorReporteClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "BuscadorReporteClientes";
            this.Load += new System.EventHandler(this.BuscadorReporteClientes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BuscadorReporteClientes_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVReportesClientes)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVReportesClientes;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
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
        private System.Windows.Forms.Button btnActualizarMaximoProductos;
        private System.Windows.Forms.TextBox txtMaximoPorPagina;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCantidadRegistros;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn marcar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn RFC;
        private System.Windows.Forms.DataGridViewImageColumn ArticulosBuy;
        private System.Windows.Forms.DataGridViewImageColumn ArticulosNotBuy;
        private System.Windows.Forms.DataGridViewImageColumn DatosCliente;
        private BotonRedondo btnComprados;
        private BotonRedondo btnDatosCLiente;
        private BotonRedondo btnNoComprados;
    }
}