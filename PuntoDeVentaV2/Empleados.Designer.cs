namespace PuntoDeVentaV2
{
    partial class Empleados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Empleados));
            this.label1 = new System.Windows.Forms.Label();
            this.btn_agregar_empleado = new System.Windows.Forms.Button();
            this.dgv_empleados = new System.Windows.Forms.DataGridView();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.cboMostrados = new System.Windows.Forms.ComboBox();
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
            this.label2 = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editar = new System.Windows.Forms.DataGridViewImageColumn();
            this.permisos = new System.Windows.Forms.DataGridViewImageColumn();
            this.Checador = new System.Windows.Forms.DataGridViewImageColumn();
            this.deshabilitar = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_empleados)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(385, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(202, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "EMPLEADOS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_agregar_empleado
            // 
            this.btn_agregar_empleado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_agregar_empleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btn_agregar_empleado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_agregar_empleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_agregar_empleado.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_agregar_empleado.ForeColor = System.Drawing.Color.White;
            this.btn_agregar_empleado.Location = new System.Drawing.Point(811, 68);
            this.btn_agregar_empleado.Name = "btn_agregar_empleado";
            this.btn_agregar_empleado.Size = new System.Drawing.Size(156, 36);
            this.btn_agregar_empleado.TabIndex = 1;
            this.btn_agregar_empleado.Text = "Nuevo empleado";
            this.btn_agregar_empleado.UseVisualStyleBackColor = false;
            this.btn_agregar_empleado.Click += new System.EventHandler(this.btn_agregar_empleado_Click);
            this.btn_agregar_empleado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btn_agregar_empleado_KeyDown);
            // 
            // dgv_empleados
            // 
            this.dgv_empleados.AllowUserToAddRows = false;
            this.dgv_empleados.AllowUserToDeleteRows = false;
            this.dgv_empleados.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_empleados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_empleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_empleados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.nombre,
            this.usuario,
            this.editar,
            this.permisos,
            this.Checador,
            this.deshabilitar});
            this.dgv_empleados.Location = new System.Drawing.Point(14, 118);
            this.dgv_empleados.Name = "dgv_empleados";
            this.dgv_empleados.ReadOnly = true;
            this.dgv_empleados.RowHeadersVisible = false;
            this.dgv_empleados.RowHeadersWidth = 62;
            this.dgv_empleados.Size = new System.Drawing.Size(953, 335);
            this.dgv_empleados.TabIndex = 2;
            this.dgv_empleados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.click_en_icono);
            this.dgv_empleados.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.cursor_en_icono);
            this.dgv_empleados.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.cursor_no_icono);
            this.dgv_empleados.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_empleados_KeyDown);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(18, 79);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(222, 30);
            this.txtBuscar.TabIndex = 3;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.txtBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // cboMostrados
            // 
            this.cboMostrados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMostrados.FormattingEnabled = true;
            this.cboMostrados.Items.AddRange(new object[] {
            "Habilitados",
            "Deshabilitados"});
            this.cboMostrados.Location = new System.Drawing.Point(249, 79);
            this.cboMostrados.Name = "cboMostrados";
            this.cboMostrados.Size = new System.Drawing.Size(121, 29);
            this.cboMostrados.TabIndex = 5;
            this.cboMostrados.SelectedValueChanged += new System.EventHandler(this.cboMostrados_SelectedValueChanged);
            // 
            // panel5
            // 
            this.panel5.Anchor = System.Windows.Forms.AnchorStyles.Top;
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
            this.panel5.Location = new System.Drawing.Point(14, 452);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(953, 65);
            this.panel5.TabIndex = 39;
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel4.Controls.Add(this.linkLblUltimaPagina);
            this.panel4.Controls.Add(this.btnSiguiente);
            this.panel4.Controls.Add(this.btnUltimaPagina);
            this.panel4.Location = new System.Drawing.Point(361, 18);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(59, 32);
            this.panel4.TabIndex = 46;
            // 
            // linkLblUltimaPagina
            // 
            this.linkLblUltimaPagina.AutoSize = true;
            this.linkLblUltimaPagina.Location = new System.Drawing.Point(60, 10);
            this.linkLblUltimaPagina.Name = "linkLblUltimaPagina";
            this.linkLblUltimaPagina.Size = new System.Drawing.Size(40, 21);
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
            this.panel3.Location = new System.Drawing.Point(227, 18);
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
            this.linkLblPrimeraPagina.Size = new System.Drawing.Size(20, 21);
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
            this.linkLblPaginaSiguiente.Location = new System.Drawing.Point(338, 28);
            this.linkLblPaginaSiguiente.Name = "linkLblPaginaSiguiente";
            this.linkLblPaginaSiguiente.Size = new System.Drawing.Size(23, 25);
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
            this.linkLblPaginaActual.Location = new System.Drawing.Point(315, 28);
            this.linkLblPaginaActual.Name = "linkLblPaginaActual";
            this.linkLblPaginaActual.Size = new System.Drawing.Size(23, 25);
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
            this.linkLblPaginaAnterior.Location = new System.Drawing.Point(291, 28);
            this.linkLblPaginaAnterior.Name = "linkLblPaginaAnterior";
            this.linkLblPaginaAnterior.Size = new System.Drawing.Size(23, 25);
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
            this.btnActualizarMaximoProductos.Location = new System.Drawing.Point(662, 21);
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
            this.txtMaximoPorPagina.Location = new System.Drawing.Point(561, 22);
            this.txtMaximoPorPagina.Name = "txtMaximoPorPagina";
            this.txtMaximoPorPagina.Size = new System.Drawing.Size(91, 30);
            this.txtMaximoPorPagina.TabIndex = 40;
            this.txtMaximoPorPagina.Text = "14";
            this.txtMaximoPorPagina.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMaximoPorPagina.Click += new System.EventHandler(this.txtMaximoPorPagina_Click);
            this.txtMaximoPorPagina.TextChanged += new System.EventHandler(this.txtMaximoPorPagina_TextChanged);
            this.txtMaximoPorPagina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaximoPorPagina_KeyDown);
            this.txtMaximoPorPagina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaximoPorPagina_KeyPress);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.Location = new System.Drawing.Point(434, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(121, 56);
            this.label7.TabIndex = 39;
            this.label7.Text = "Cantidad de empleados para mostrar: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCantidadRegistros
            // 
            this.lblCantidadRegistros.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCantidadRegistros.BackColor = System.Drawing.SystemColors.Control;
            this.lblCantidadRegistros.ForeColor = System.Drawing.Color.Blue;
            this.lblCantidadRegistros.Location = new System.Drawing.Point(180, 22);
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
            this.label4.Location = new System.Drawing.Point(57, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 42);
            this.label4.TabIndex = 37;
            this.label4.Text = "Total de empleados \r\nencontrados:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 21);
            this.label2.TabIndex = 40;
            this.label2.Text = "Buscar:";
            // 
            // id
            // 
            this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.id.FillWeight = 31.81818F;
            this.id.Frozen = true;
            this.id.HeaderText = "ID";
            this.id.MinimumWidth = 8;
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Width = 158;
            // 
            // nombre
            // 
            this.nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.nombre.FillWeight = 31.81818F;
            this.nombre.Frozen = true;
            this.nombre.HeaderText = "Nombre";
            this.nombre.MinimumWidth = 8;
            this.nombre.Name = "nombre";
            this.nombre.ReadOnly = true;
            this.nombre.Width = 159;
            // 
            // usuario
            // 
            this.usuario.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.usuario.FillWeight = 31.81818F;
            this.usuario.Frozen = true;
            this.usuario.HeaderText = "Usuario";
            this.usuario.MinimumWidth = 8;
            this.usuario.Name = "usuario";
            this.usuario.ReadOnly = true;
            this.usuario.Width = 158;
            // 
            // editar
            // 
            this.editar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.editar.FillWeight = 31.81818F;
            this.editar.Frozen = true;
            this.editar.HeaderText = "Editar";
            this.editar.MinimumWidth = 8;
            this.editar.Name = "editar";
            this.editar.ReadOnly = true;
            this.editar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.editar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.editar.Width = 158;
            // 
            // permisos
            // 
            this.permisos.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.permisos.FillWeight = 31.81818F;
            this.permisos.Frozen = true;
            this.permisos.HeaderText = "Permisos";
            this.permisos.MinimumWidth = 8;
            this.permisos.Name = "permisos";
            this.permisos.ReadOnly = true;
            this.permisos.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.permisos.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.permisos.Width = 120;
            // 
            // Checador
            // 
            this.Checador.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Checador.FillWeight = 31.81818F;
            this.Checador.Frozen = true;
            this.Checador.HeaderText = "Checador";
            this.Checador.MinimumWidth = 8;
            this.Checador.Name = "Checador";
            this.Checador.ReadOnly = true;
            this.Checador.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Checador.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Checador.Width = 120;
            // 
            // deshabilitar
            // 
            this.deshabilitar.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.deshabilitar.FillWeight = 31.81818F;
            this.deshabilitar.Frozen = true;
            this.deshabilitar.HeaderText = "Deshabilitar";
            this.deshabilitar.MinimumWidth = 8;
            this.deshabilitar.Name = "deshabilitar";
            this.deshabilitar.ReadOnly = true;
            this.deshabilitar.Width = 158;
            // 
            // Empleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(977, 522);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.cboMostrados);
            this.Controls.Add(this.dgv_empleados);
            this.Controls.Add(this.btn_agregar_empleado);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel5);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Empleados";
            this.Text = "S";
            this.Load += new System.EventHandler(this.cargar_empleados);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Empleados_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_empleados)).EndInit();
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

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_agregar_empleado;
        private System.Windows.Forms.DataGridView dgv_empleados;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.ComboBox cboMostrados;
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
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuario;
        private System.Windows.Forms.DataGridViewImageColumn editar;
        private System.Windows.Forms.DataGridViewImageColumn permisos;
        private System.Windows.Forms.DataGridViewImageColumn Checador;
        private System.Windows.Forms.DataGridViewImageColumn deshabilitar;
    }
}