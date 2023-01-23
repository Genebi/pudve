namespace PuntoDeVentaV2
{
    partial class Agregar_empleado_permisos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Agregar_empleado_permisos));
            this.cbox_anticipos = new System.Windows.Forms.CheckBox();
            this.cbox_caja = new System.Windows.Forms.CheckBox();
            this.cbox_clientes = new System.Windows.Forms.CheckBox();
            this.cbox_configuracion = new System.Windows.Forms.CheckBox();
            this.cbox_empleados = new System.Windows.Forms.CheckBox();
            this.cbox_empresas = new System.Windows.Forms.CheckBox();
            this.cbox_facturas = new System.Windows.Forms.CheckBox();
            this.cbox_inventario = new System.Windows.Forms.CheckBox();
            this.cbox_misdatos = new System.Windows.Forms.CheckBox();
            this.cbox_productos = new System.Windows.Forms.CheckBox();
            this.cbox_proveedores = new System.Windows.Forms.CheckBox();
            this.cbox_reportes = new System.Windows.Forms.CheckBox();
            this.cbox_ventas = new System.Windows.Forms.CheckBox();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.btnCaja = new System.Windows.Forms.Button();
            this.btnAnticipos = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnFacturas = new System.Windows.Forms.Button();
            this.btnProductos = new System.Windows.Forms.Button();
            this.btnEmpleados = new System.Windows.Forms.Button();
            this.btnInventario = new System.Windows.Forms.Button();
            this.btnProveedores = new System.Windows.Forms.Button();
            this.btnVentas = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.btnMisDatos = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.primerSeparador = new System.Windows.Forms.Label();
            this.chkMarcarDesmarcar = new System.Windows.Forms.CheckBox();
            this.btnBascula = new System.Windows.Forms.Button();
            this.cboBascula = new System.Windows.Forms.CheckBox();
            this.chkPrecio = new System.Windows.Forms.CheckBox();
            this.cboxConsultaP = new System.Windows.Forms.CheckBox();
            this.btnPlantilla = new System.Windows.Forms.Button();
            this.DGVPlantillas = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seleccionar = new System.Windows.Forms.DataGridViewImageColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewImageColumn();
            this.ComboHabilittados = new System.Windows.Forms.ComboBox();
            this.dDGVDeshabilitados = new System.Windows.Forms.DataGridView();
            this.IDDeshabilitado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreDesha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImagenDesha = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGVPlantillas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dDGVDeshabilitados)).BeginInit();
            this.SuspendLayout();
            // 
            // cbox_anticipos
            // 
            this.cbox_anticipos.AutoSize = true;
            this.cbox_anticipos.Location = new System.Drawing.Point(371, 39);
            this.cbox_anticipos.Name = "cbox_anticipos";
            this.cbox_anticipos.Size = new System.Drawing.Size(75, 19);
            this.cbox_anticipos.TabIndex = 1;
            this.cbox_anticipos.Text = "Anticipos";
            this.cbox_anticipos.UseVisualStyleBackColor = true;
            // 
            // cbox_caja
            // 
            this.cbox_caja.AutoSize = true;
            this.cbox_caja.Location = new System.Drawing.Point(513, 39);
            this.cbox_caja.Name = "cbox_caja";
            this.cbox_caja.Size = new System.Drawing.Size(51, 19);
            this.cbox_caja.TabIndex = 2;
            this.cbox_caja.Text = "Caja";
            this.cbox_caja.UseVisualStyleBackColor = true;
            // 
            // cbox_clientes
            // 
            this.cbox_clientes.AutoSize = true;
            this.cbox_clientes.Location = new System.Drawing.Point(645, 39);
            this.cbox_clientes.Name = "cbox_clientes";
            this.cbox_clientes.Size = new System.Drawing.Size(70, 19);
            this.cbox_clientes.TabIndex = 3;
            this.cbox_clientes.Text = "Clientes";
            this.cbox_clientes.UseVisualStyleBackColor = true;
            // 
            // cbox_configuracion
            // 
            this.cbox_configuracion.AutoSize = true;
            this.cbox_configuracion.Location = new System.Drawing.Point(371, 82);
            this.cbox_configuracion.Name = "cbox_configuracion";
            this.cbox_configuracion.Size = new System.Drawing.Size(102, 19);
            this.cbox_configuracion.TabIndex = 4;
            this.cbox_configuracion.Text = "Configuración";
            this.cbox_configuracion.UseVisualStyleBackColor = true;
            // 
            // cbox_empleados
            // 
            this.cbox_empleados.AutoSize = true;
            this.cbox_empleados.Location = new System.Drawing.Point(513, 82);
            this.cbox_empleados.Name = "cbox_empleados";
            this.cbox_empleados.Size = new System.Drawing.Size(89, 19);
            this.cbox_empleados.TabIndex = 5;
            this.cbox_empleados.Text = "Empleados";
            this.cbox_empleados.UseVisualStyleBackColor = true;
            // 
            // cbox_empresas
            // 
            this.cbox_empresas.AutoSize = true;
            this.cbox_empresas.Location = new System.Drawing.Point(513, 211);
            this.cbox_empresas.Name = "cbox_empresas";
            this.cbox_empresas.Size = new System.Drawing.Size(82, 19);
            this.cbox_empresas.TabIndex = 6;
            this.cbox_empresas.Text = "Empresas";
            this.cbox_empresas.UseVisualStyleBackColor = true;
            this.cbox_empresas.Visible = false;
            // 
            // cbox_facturas
            // 
            this.cbox_facturas.AutoSize = true;
            this.cbox_facturas.Location = new System.Drawing.Point(371, 126);
            this.cbox_facturas.Name = "cbox_facturas";
            this.cbox_facturas.Size = new System.Drawing.Size(73, 19);
            this.cbox_facturas.TabIndex = 7;
            this.cbox_facturas.Text = "Facturas";
            this.cbox_facturas.UseVisualStyleBackColor = true;
            // 
            // cbox_inventario
            // 
            this.cbox_inventario.AutoSize = true;
            this.cbox_inventario.Location = new System.Drawing.Point(513, 126);
            this.cbox_inventario.Name = "cbox_inventario";
            this.cbox_inventario.Size = new System.Drawing.Size(79, 19);
            this.cbox_inventario.TabIndex = 8;
            this.cbox_inventario.Text = "Inventario";
            this.cbox_inventario.UseVisualStyleBackColor = true;
            // 
            // cbox_misdatos
            // 
            this.cbox_misdatos.AutoSize = true;
            this.cbox_misdatos.Location = new System.Drawing.Point(645, 126);
            this.cbox_misdatos.Name = "cbox_misdatos";
            this.cbox_misdatos.Size = new System.Drawing.Size(79, 19);
            this.cbox_misdatos.TabIndex = 9;
            this.cbox_misdatos.Text = "Mis datos";
            this.cbox_misdatos.UseVisualStyleBackColor = true;
            // 
            // cbox_productos
            // 
            this.cbox_productos.AutoSize = true;
            this.cbox_productos.Location = new System.Drawing.Point(371, 171);
            this.cbox_productos.Name = "cbox_productos";
            this.cbox_productos.Size = new System.Drawing.Size(81, 19);
            this.cbox_productos.TabIndex = 10;
            this.cbox_productos.Text = "Productos";
            this.cbox_productos.UseVisualStyleBackColor = true;
            // 
            // cbox_proveedores
            // 
            this.cbox_proveedores.AutoSize = true;
            this.cbox_proveedores.Location = new System.Drawing.Point(513, 171);
            this.cbox_proveedores.Name = "cbox_proveedores";
            this.cbox_proveedores.Size = new System.Drawing.Size(95, 19);
            this.cbox_proveedores.TabIndex = 11;
            this.cbox_proveedores.Text = "Proveedores";
            this.cbox_proveedores.UseVisualStyleBackColor = true;
            // 
            // cbox_reportes
            // 
            this.cbox_reportes.AutoSize = true;
            this.cbox_reportes.Location = new System.Drawing.Point(645, 171);
            this.cbox_reportes.Name = "cbox_reportes";
            this.cbox_reportes.Size = new System.Drawing.Size(76, 19);
            this.cbox_reportes.TabIndex = 12;
            this.cbox_reportes.Text = "Reportes";
            this.cbox_reportes.UseVisualStyleBackColor = true;
            // 
            // cbox_ventas
            // 
            this.cbox_ventas.AutoSize = true;
            this.cbox_ventas.Location = new System.Drawing.Point(371, 213);
            this.cbox_ventas.Name = "cbox_ventas";
            this.cbox_ventas.Size = new System.Drawing.Size(63, 19);
            this.cbox_ventas.TabIndex = 14;
            this.cbox_ventas.Text = "Ventas";
            this.cbox_ventas.UseVisualStyleBackColor = true;
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_aceptar.BackColor = System.Drawing.Color.Green;
            this.btn_aceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_aceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_aceptar.ForeColor = System.Drawing.Color.White;
            this.btn_aceptar.Location = new System.Drawing.Point(469, 267);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(119, 30);
            this.btn_aceptar.TabIndex = 15;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = false;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancelar.BackColor = System.Drawing.Color.Red;
            this.btn_cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancelar.ForeColor = System.Drawing.Color.White;
            this.btn_cancelar.Location = new System.Drawing.Point(338, 267);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(119, 30);
            this.btn_cancelar.TabIndex = 16;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // btnCaja
            // 
            this.btnCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCaja.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.btnCaja.Location = new System.Drawing.Point(487, 38);
            this.btnCaja.Name = "btnCaja";
            this.btnCaja.Size = new System.Drawing.Size(20, 20);
            this.btnCaja.TabIndex = 17;
            this.btnCaja.UseVisualStyleBackColor = true;
            this.btnCaja.Click += new System.EventHandler(this.btnCaja_Click);
            // 
            // btnAnticipos
            // 
            this.btnAnticipos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnticipos.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.btnAnticipos.Location = new System.Drawing.Point(345, 38);
            this.btnAnticipos.Name = "btnAnticipos";
            this.btnAnticipos.Size = new System.Drawing.Size(20, 20);
            this.btnAnticipos.TabIndex = 18;
            this.btnAnticipos.UseVisualStyleBackColor = true;
            this.btnAnticipos.Click += new System.EventHandler(this.btnAnticipos_Click);
            // 
            // btnClientes
            // 
            this.btnClientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClientes.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.btnClientes.Location = new System.Drawing.Point(619, 38);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(20, 20);
            this.btnClientes.TabIndex = 19;
            this.btnClientes.UseVisualStyleBackColor = true;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfig.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.btnConfig.Location = new System.Drawing.Point(345, 81);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(20, 20);
            this.btnConfig.TabIndex = 20;
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnFacturas
            // 
            this.btnFacturas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFacturas.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.btnFacturas.Location = new System.Drawing.Point(345, 125);
            this.btnFacturas.Name = "btnFacturas";
            this.btnFacturas.Size = new System.Drawing.Size(20, 20);
            this.btnFacturas.TabIndex = 21;
            this.btnFacturas.UseVisualStyleBackColor = true;
            this.btnFacturas.Click += new System.EventHandler(this.btnFacturas_Click);
            // 
            // btnProductos
            // 
            this.btnProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProductos.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.btnProductos.Location = new System.Drawing.Point(345, 170);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(20, 20);
            this.btnProductos.TabIndex = 22;
            this.btnProductos.UseVisualStyleBackColor = true;
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            // 
            // btnEmpleados
            // 
            this.btnEmpleados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmpleados.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.btnEmpleados.Location = new System.Drawing.Point(487, 81);
            this.btnEmpleados.Name = "btnEmpleados";
            this.btnEmpleados.Size = new System.Drawing.Size(20, 20);
            this.btnEmpleados.TabIndex = 24;
            this.btnEmpleados.UseVisualStyleBackColor = true;
            this.btnEmpleados.Click += new System.EventHandler(this.btnEmpleados_Click);
            // 
            // btnInventario
            // 
            this.btnInventario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInventario.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.btnInventario.Location = new System.Drawing.Point(487, 125);
            this.btnInventario.Name = "btnInventario";
            this.btnInventario.Size = new System.Drawing.Size(20, 20);
            this.btnInventario.TabIndex = 25;
            this.btnInventario.UseVisualStyleBackColor = true;
            this.btnInventario.Click += new System.EventHandler(this.btnInventario_Click);
            // 
            // btnProveedores
            // 
            this.btnProveedores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProveedores.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.btnProveedores.Location = new System.Drawing.Point(487, 170);
            this.btnProveedores.Name = "btnProveedores";
            this.btnProveedores.Size = new System.Drawing.Size(20, 20);
            this.btnProveedores.TabIndex = 26;
            this.btnProveedores.UseVisualStyleBackColor = true;
            this.btnProveedores.Click += new System.EventHandler(this.btnProveedores_Click);
            // 
            // btnVentas
            // 
            this.btnVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVentas.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.btnVentas.Location = new System.Drawing.Point(345, 212);
            this.btnVentas.Name = "btnVentas";
            this.btnVentas.Size = new System.Drawing.Size(20, 20);
            this.btnVentas.TabIndex = 27;
            this.btnVentas.UseVisualStyleBackColor = true;
            this.btnVentas.Click += new System.EventHandler(this.btnVentas_Click);
            // 
            // button11
            // 
            this.button11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button11.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.button11.Location = new System.Drawing.Point(487, 210);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(20, 20);
            this.button11.TabIndex = 28;
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Visible = false;
            // 
            // btnMisDatos
            // 
            this.btnMisDatos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMisDatos.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.btnMisDatos.Location = new System.Drawing.Point(619, 125);
            this.btnMisDatos.Name = "btnMisDatos";
            this.btnMisDatos.Size = new System.Drawing.Size(20, 20);
            this.btnMisDatos.TabIndex = 29;
            this.btnMisDatos.UseVisualStyleBackColor = true;
            this.btnMisDatos.Click += new System.EventHandler(this.btnMisDatos_Click);
            // 
            // btnReportes
            // 
            this.btnReportes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReportes.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.btnReportes.Location = new System.Drawing.Point(619, 170);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(20, 20);
            this.btnReportes.TabIndex = 30;
            this.btnReportes.UseVisualStyleBackColor = true;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // primerSeparador
            // 
            this.primerSeparador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.primerSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.primerSeparador.Location = new System.Drawing.Point(356, 245);
            this.primerSeparador.Name = "primerSeparador";
            this.primerSeparador.Size = new System.Drawing.Size(350, 2);
            this.primerSeparador.TabIndex = 115;
            this.primerSeparador.Text = "REPORTES";
            this.primerSeparador.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // chkMarcarDesmarcar
            // 
            this.chkMarcarDesmarcar.AutoSize = true;
            this.chkMarcarDesmarcar.Location = new System.Drawing.Point(469, 12);
            this.chkMarcarDesmarcar.Name = "chkMarcarDesmarcar";
            this.chkMarcarDesmarcar.Size = new System.Drawing.Size(98, 19);
            this.chkMarcarDesmarcar.TabIndex = 116;
            this.chkMarcarDesmarcar.Text = "Marcar todos";
            this.chkMarcarDesmarcar.UseVisualStyleBackColor = true;
            this.chkMarcarDesmarcar.CheckedChanged += new System.EventHandler(this.chkMarcarDesmarcar_CheckedChanged);
            // 
            // btnBascula
            // 
            this.btnBascula.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBascula.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.btnBascula.Location = new System.Drawing.Point(619, 81);
            this.btnBascula.Name = "btnBascula";
            this.btnBascula.Size = new System.Drawing.Size(20, 20);
            this.btnBascula.TabIndex = 118;
            this.btnBascula.UseVisualStyleBackColor = true;
            this.btnBascula.Click += new System.EventHandler(this.btnBascula_Click);
            // 
            // cboBascula
            // 
            this.cboBascula.AutoSize = true;
            this.cboBascula.Location = new System.Drawing.Point(645, 82);
            this.cboBascula.Name = "cboBascula";
            this.cboBascula.Size = new System.Drawing.Size(70, 19);
            this.cboBascula.TabIndex = 117;
            this.cboBascula.Text = "Bascula";
            this.cboBascula.UseVisualStyleBackColor = true;
            // 
            // chkPrecio
            // 
            this.chkPrecio.AutoSize = true;
            this.chkPrecio.Location = new System.Drawing.Point(513, 212);
            this.chkPrecio.Name = "chkPrecio";
            this.chkPrecio.Size = new System.Drawing.Size(113, 19);
            this.chkPrecio.TabIndex = 119;
            this.chkPrecio.Text = "Precio Producto";
            this.chkPrecio.UseVisualStyleBackColor = true;
            // 
            // cboxConsultaP
            // 
            this.cboxConsultaP.Location = new System.Drawing.Point(645, 201);
            this.cboxConsultaP.Name = "cboxConsultaP";
            this.cboxConsultaP.Size = new System.Drawing.Size(102, 41);
            this.cboxConsultaP.TabIndex = 120;
            this.cboxConsultaP.Text = "Consutar Precios";
            this.cboxConsultaP.UseVisualStyleBackColor = true;
            // 
            // btnPlantilla
            // 
            this.btnPlantilla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlantilla.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnPlantilla.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlantilla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlantilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlantilla.ForeColor = System.Drawing.Color.White;
            this.btnPlantilla.Location = new System.Drawing.Point(596, 267);
            this.btnPlantilla.Name = "btnPlantilla";
            this.btnPlantilla.Size = new System.Drawing.Size(119, 30);
            this.btnPlantilla.TabIndex = 121;
            this.btnPlantilla.Text = "Guardar Plantilla";
            this.btnPlantilla.UseVisualStyleBackColor = false;
            this.btnPlantilla.Click += new System.EventHandler(this.button1_Click);
            // 
            // DGVPlantillas
            // 
            this.DGVPlantillas.AllowUserToAddRows = false;
            this.DGVPlantillas.AllowUserToDeleteRows = false;
            this.DGVPlantillas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVPlantillas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.DGVPlantillas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVPlantillas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Nombre,
            this.Seleccionar,
            this.Eliminar});
            this.DGVPlantillas.Location = new System.Drawing.Point(11, 28);
            this.DGVPlantillas.Margin = new System.Windows.Forms.Padding(1, 8, 3, 0);
            this.DGVPlantillas.Name = "DGVPlantillas";
            this.DGVPlantillas.ReadOnly = true;
            this.DGVPlantillas.RowHeadersVisible = false;
            this.DGVPlantillas.Size = new System.Drawing.Size(318, 274);
            this.DGVPlantillas.TabIndex = 122;
            this.DGVPlantillas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVPlantillas_CellClick);
            // 
            // ID
            // 
            this.ID.FillWeight = 54.72959F;
            this.ID.HeaderText = "No.";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Nombre
            // 
            this.Nombre.FillWeight = 258.2662F;
            this.Nombre.HeaderText = "Plantillas";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // Seleccionar
            // 
            this.Seleccionar.FillWeight = 46.39511F;
            this.Seleccionar.HeaderText = "";
            this.Seleccionar.Name = "Seleccionar";
            this.Seleccionar.ReadOnly = true;
            this.Seleccionar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Seleccionar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Eliminar
            // 
            this.Eliminar.FillWeight = 40.60914F;
            this.Eliminar.HeaderText = "";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.ReadOnly = true;
            this.Eliminar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Eliminar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // ComboHabilittados
            // 
            this.ComboHabilittados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboHabilittados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComboHabilittados.FormattingEnabled = true;
            this.ComboHabilittados.Items.AddRange(new object[] {
            "Habilitados",
            "Deshabilitados"});
            this.ComboHabilittados.Location = new System.Drawing.Point(11, 3);
            this.ComboHabilittados.Name = "ComboHabilittados";
            this.ComboHabilittados.Size = new System.Drawing.Size(191, 23);
            this.ComboHabilittados.TabIndex = 123;
            this.ComboHabilittados.SelectedIndexChanged += new System.EventHandler(this.ComboHabilittados_SelectedIndexChanged);
            // 
            // dDGVDeshabilitados
            // 
            this.dDGVDeshabilitados.AllowUserToAddRows = false;
            this.dDGVDeshabilitados.AllowUserToDeleteRows = false;
            this.dDGVDeshabilitados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dDGVDeshabilitados.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dDGVDeshabilitados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dDGVDeshabilitados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDDeshabilitado,
            this.NombreDesha,
            this.ImagenDesha});
            this.dDGVDeshabilitados.Location = new System.Drawing.Point(10, 28);
            this.dDGVDeshabilitados.Margin = new System.Windows.Forms.Padding(1, 8, 3, 0);
            this.dDGVDeshabilitados.Name = "dDGVDeshabilitados";
            this.dDGVDeshabilitados.ReadOnly = true;
            this.dDGVDeshabilitados.RowHeadersVisible = false;
            this.dDGVDeshabilitados.Size = new System.Drawing.Size(318, 274);
            this.dDGVDeshabilitados.TabIndex = 124;
            this.dDGVDeshabilitados.Visible = false;
            this.dDGVDeshabilitados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dDGVDeshabilitados_CellClick);
            this.dDGVDeshabilitados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dDGVDeshabilitados_CellContentClick);
            // 
            // IDDeshabilitado
            // 
            this.IDDeshabilitado.FillWeight = 54.72959F;
            this.IDDeshabilitado.HeaderText = "No.";
            this.IDDeshabilitado.Name = "IDDeshabilitado";
            this.IDDeshabilitado.ReadOnly = true;
            // 
            // NombreDesha
            // 
            this.NombreDesha.FillWeight = 258.2662F;
            this.NombreDesha.HeaderText = "Plantillas";
            this.NombreDesha.Name = "NombreDesha";
            this.NombreDesha.ReadOnly = true;
            // 
            // ImagenDesha
            // 
            this.ImagenDesha.FillWeight = 46.39511F;
            this.ImagenDesha.HeaderText = "";
            this.ImagenDesha.Name = "ImagenDesha";
            this.ImagenDesha.ReadOnly = true;
            this.ImagenDesha.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ImagenDesha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Agregar_empleado_permisos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 311);
            this.Controls.Add(this.dDGVDeshabilitados);
            this.Controls.Add(this.ComboHabilittados);
            this.Controls.Add(this.DGVPlantillas);
            this.Controls.Add(this.btnPlantilla);
            this.Controls.Add(this.chkPrecio);
            this.Controls.Add(this.btnBascula);
            this.Controls.Add(this.cboBascula);
            this.Controls.Add(this.chkMarcarDesmarcar);
            this.Controls.Add(this.primerSeparador);
            this.Controls.Add(this.btnReportes);
            this.Controls.Add(this.btnMisDatos);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.btnVentas);
            this.Controls.Add(this.btnProveedores);
            this.Controls.Add(this.btnInventario);
            this.Controls.Add(this.btnEmpleados);
            this.Controls.Add(this.btnProductos);
            this.Controls.Add(this.btnFacturas);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.btnClientes);
            this.Controls.Add(this.btnAnticipos);
            this.Controls.Add(this.btnCaja);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.cbox_ventas);
            this.Controls.Add(this.cbox_reportes);
            this.Controls.Add(this.cbox_proveedores);
            this.Controls.Add(this.cbox_productos);
            this.Controls.Add(this.cbox_misdatos);
            this.Controls.Add(this.cbox_inventario);
            this.Controls.Add(this.cbox_facturas);
            this.Controls.Add(this.cbox_empresas);
            this.Controls.Add(this.cbox_empleados);
            this.Controls.Add(this.cbox_configuracion);
            this.Controls.Add(this.cbox_clientes);
            this.Controls.Add(this.cbox_caja);
            this.Controls.Add(this.cbox_anticipos);
            this.Controls.Add(this.cboxConsultaP);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Agregar_empleado_permisos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Asignar/ajustar permisos";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Agregar_empleado_permisos_FormClosing);
            this.Load += new System.EventHandler(this.cargar_datos);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Agregar_empleado_permisos_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVPlantillas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dDGVDeshabilitados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cbox_anticipos;
        private System.Windows.Forms.CheckBox cbox_caja;
        private System.Windows.Forms.CheckBox cbox_clientes;
        private System.Windows.Forms.CheckBox cbox_configuracion;
        private System.Windows.Forms.CheckBox cbox_empleados;
        private System.Windows.Forms.CheckBox cbox_empresas;
        private System.Windows.Forms.CheckBox cbox_facturas;
        private System.Windows.Forms.CheckBox cbox_inventario;
        private System.Windows.Forms.CheckBox cbox_misdatos;
        private System.Windows.Forms.CheckBox cbox_productos;
        private System.Windows.Forms.CheckBox cbox_proveedores;
        private System.Windows.Forms.CheckBox cbox_reportes;
        private System.Windows.Forms.CheckBox cbox_ventas;
        private System.Windows.Forms.Button btn_aceptar;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Button btnCaja;
        private System.Windows.Forms.Button btnAnticipos;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnFacturas;
        private System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Button btnEmpleados;
        private System.Windows.Forms.Button btnInventario;
        private System.Windows.Forms.Button btnProveedores;
        private System.Windows.Forms.Button btnVentas;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button btnMisDatos;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Label primerSeparador;
        private System.Windows.Forms.CheckBox chkMarcarDesmarcar;
        private System.Windows.Forms.Button btnBascula;
        private System.Windows.Forms.CheckBox cboBascula;
        private System.Windows.Forms.CheckBox chkPrecio;
        private System.Windows.Forms.CheckBox cboxConsultaP;
        private System.Windows.Forms.Button btnPlantilla;
        private System.Windows.Forms.DataGridView DGVPlantillas;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewImageColumn Seleccionar;
        private System.Windows.Forms.DataGridViewImageColumn Eliminar;
        private System.Windows.Forms.ComboBox ComboHabilittados;
        private System.Windows.Forms.DataGridView dDGVDeshabilitados;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDDeshabilitado;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreDesha;
        private System.Windows.Forms.DataGridViewImageColumn ImagenDesha;
    }
}