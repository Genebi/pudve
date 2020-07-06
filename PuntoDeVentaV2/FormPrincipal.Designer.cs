﻿namespace PuntoDeVentaV2
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrincipal));
            this.panelMaestro = new System.Windows.Forms.Panel();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.btnCalculadora = new System.Windows.Forms.Button();
            this.menuVertical = new System.Windows.Forms.Panel();
            this.btnSesion = new System.Windows.Forms.Button();
            this.btnEmpresas = new System.Windows.Forms.Button();
            this.btnMisDatos = new System.Windows.Forms.Button();
            this.btnInventario = new System.Windows.Forms.Button();
            this.btnCaja = new System.Windows.Forms.Button();
            this.btnAnticipos = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnFacturas = new System.Windows.Forms.Button();
            this.btnProveedores = new System.Windows.Forms.Button();
            this.btnClientes = new System.Windows.Forms.Button();
            this.btnReportes = new System.Windows.Forms.Button();
            this.btnEmpleados = new System.Windows.Forms.Button();
            this.btnServicios = new System.Windows.Forms.Button();
            this.btnVentas = new System.Windows.Forms.Button();
            this.btnProductos = new System.Windows.Forms.Button();
            this.temporizador_respaldo = new System.Windows.Forms.Timer(this.components);
            this.actualizarCaja = new System.Windows.Forms.Timer(this.components);
            this.timerProductos = new System.Windows.Forms.Timer(this.components);
            this.panelMaestro.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            this.menuVertical.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMaestro
            // 
            this.panelMaestro.Controls.Add(this.panelContenedor);
            this.panelMaestro.Controls.Add(this.menuVertical);
            this.panelMaestro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMaestro.Location = new System.Drawing.Point(0, 0);
            this.panelMaestro.Name = "panelMaestro";
            this.panelMaestro.Size = new System.Drawing.Size(1084, 602);
            this.panelMaestro.TabIndex = 0;
            // 
            // panelContenedor
            // 
            this.panelContenedor.Controls.Add(this.btnCalculadora);
            this.panelContenedor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenedor.Location = new System.Drawing.Point(230, 0);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(854, 602);
            this.panelContenedor.TabIndex = 2;
            // 
            // btnCalculadora
            // 
            this.btnCalculadora.Location = new System.Drawing.Point(525, 338);
            this.btnCalculadora.Name = "btnCalculadora";
            this.btnCalculadora.Size = new System.Drawing.Size(75, 23);
            this.btnCalculadora.TabIndex = 1;
            this.btnCalculadora.Text = "Calculadora";
            this.btnCalculadora.UseVisualStyleBackColor = true;
            this.btnCalculadora.Click += new System.EventHandler(this.btnCalculadora_Click);
            // 
            // menuVertical
            // 
            this.menuVertical.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.menuVertical.Controls.Add(this.btnSesion);
            this.menuVertical.Controls.Add(this.btnEmpresas);
            this.menuVertical.Controls.Add(this.btnMisDatos);
            this.menuVertical.Controls.Add(this.btnInventario);
            this.menuVertical.Controls.Add(this.btnCaja);
            this.menuVertical.Controls.Add(this.btnAnticipos);
            this.menuVertical.Controls.Add(this.btnConfig);
            this.menuVertical.Controls.Add(this.btnFacturas);
            this.menuVertical.Controls.Add(this.btnProveedores);
            this.menuVertical.Controls.Add(this.btnClientes);
            this.menuVertical.Controls.Add(this.btnReportes);
            this.menuVertical.Controls.Add(this.btnEmpleados);
            this.menuVertical.Controls.Add(this.btnServicios);
            this.menuVertical.Controls.Add(this.btnVentas);
            this.menuVertical.Controls.Add(this.btnProductos);
            this.menuVertical.Dock = System.Windows.Forms.DockStyle.Left;
            this.menuVertical.Location = new System.Drawing.Point(0, 0);
            this.menuVertical.Name = "menuVertical";
            this.menuVertical.Size = new System.Drawing.Size(230, 602);
            this.menuVertical.TabIndex = 0;
            // 
            // btnSesion
            // 
            this.btnSesion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnSesion.FlatAppearance.BorderSize = 0;
            this.btnSesion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSesion.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSesion.ForeColor = System.Drawing.Color.White;
            this.btnSesion.Location = new System.Drawing.Point(25, 544);
            this.btnSesion.Name = "btnSesion";
            this.btnSesion.Size = new System.Drawing.Size(175, 30);
            this.btnSesion.TabIndex = 110;
            this.btnSesion.Text = "Cerrar  Sesión";
            this.btnSesion.UseVisualStyleBackColor = false;
            this.btnSesion.Click += new System.EventHandler(this.btnSesion_Click);
            // 
            // btnEmpresas
            // 
            this.btnEmpresas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmpresas.FlatAppearance.BorderSize = 0;
            this.btnEmpresas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnEmpresas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnEmpresas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpresas.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmpresas.ForeColor = System.Drawing.Color.White;
            this.btnEmpresas.Image = ((System.Drawing.Image)(resources.GetObject("btnEmpresas.Image")));
            this.btnEmpresas.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEmpresas.Location = new System.Drawing.Point(115, 463);
            this.btnEmpresas.Name = "btnEmpresas";
            this.btnEmpresas.Size = new System.Drawing.Size(115, 71);
            this.btnEmpresas.TabIndex = 13;
            this.btnEmpresas.Text = "Empresas";
            this.btnEmpresas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEmpresas.UseVisualStyleBackColor = true;
            this.btnEmpresas.Click += new System.EventHandler(this.btnEmpresas_Click);
            // 
            // btnMisDatos
            // 
            this.btnMisDatos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMisDatos.FlatAppearance.BorderSize = 0;
            this.btnMisDatos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnMisDatos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnMisDatos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMisDatos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMisDatos.ForeColor = System.Drawing.Color.White;
            this.btnMisDatos.Image = ((System.Drawing.Image)(resources.GetObject("btnMisDatos.Image")));
            this.btnMisDatos.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnMisDatos.Location = new System.Drawing.Point(115, 387);
            this.btnMisDatos.Name = "btnMisDatos";
            this.btnMisDatos.Size = new System.Drawing.Size(115, 71);
            this.btnMisDatos.TabIndex = 12;
            this.btnMisDatos.Text = "Mis Datos";
            this.btnMisDatos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnMisDatos.UseVisualStyleBackColor = true;
            this.btnMisDatos.Click += new System.EventHandler(this.btnMisDatos_Click);
            // 
            // btnInventario
            // 
            this.btnInventario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInventario.FlatAppearance.BorderSize = 0;
            this.btnInventario.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnInventario.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnInventario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventario.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInventario.ForeColor = System.Drawing.Color.White;
            this.btnInventario.Image = ((System.Drawing.Image)(resources.GetObject("btnInventario.Image")));
            this.btnInventario.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnInventario.Location = new System.Drawing.Point(0, 464);
            this.btnInventario.Name = "btnInventario";
            this.btnInventario.Size = new System.Drawing.Size(115, 69);
            this.btnInventario.TabIndex = 11;
            this.btnInventario.Text = "Inventario";
            this.btnInventario.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnInventario.UseVisualStyleBackColor = true;
            this.btnInventario.Click += new System.EventHandler(this.btnInventario_Click);
            // 
            // btnCaja
            // 
            this.btnCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCaja.FlatAppearance.BorderSize = 0;
            this.btnCaja.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnCaja.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaja.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaja.ForeColor = System.Drawing.Color.White;
            this.btnCaja.Image = ((System.Drawing.Image)(resources.GetObject("btnCaja.Image")));
            this.btnCaja.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCaja.Location = new System.Drawing.Point(115, 313);
            this.btnCaja.Name = "btnCaja";
            this.btnCaja.Size = new System.Drawing.Size(115, 68);
            this.btnCaja.TabIndex = 10;
            this.btnCaja.Text = "Caja";
            this.btnCaja.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCaja.UseVisualStyleBackColor = true;
            this.btnCaja.Click += new System.EventHandler(this.btnCaja_Click);
            // 
            // btnAnticipos
            // 
            this.btnAnticipos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnticipos.FlatAppearance.BorderSize = 0;
            this.btnAnticipos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnAnticipos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnAnticipos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnticipos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnticipos.ForeColor = System.Drawing.Color.White;
            this.btnAnticipos.Image = ((System.Drawing.Image)(resources.GetObject("btnAnticipos.Image")));
            this.btnAnticipos.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAnticipos.Location = new System.Drawing.Point(0, 387);
            this.btnAnticipos.Name = "btnAnticipos";
            this.btnAnticipos.Size = new System.Drawing.Size(115, 71);
            this.btnAnticipos.TabIndex = 9;
            this.btnAnticipos.Text = "Anticipos";
            this.btnAnticipos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAnticipos.UseVisualStyleBackColor = true;
            this.btnAnticipos.Click += new System.EventHandler(this.btnAnticipos_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConfig.FlatAppearance.BorderSize = 0;
            this.btnConfig.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnConfig.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfig.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfig.ForeColor = System.Drawing.Color.White;
            this.btnConfig.Image = ((System.Drawing.Image)(resources.GetObject("btnConfig.Image")));
            this.btnConfig.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnConfig.Location = new System.Drawing.Point(115, 238);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(115, 69);
            this.btnConfig.TabIndex = 8;
            this.btnConfig.Text = "Configuración";
            this.btnConfig.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnFacturas
            // 
            this.btnFacturas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFacturas.FlatAppearance.BorderSize = 0;
            this.btnFacturas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnFacturas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnFacturas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFacturas.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFacturas.ForeColor = System.Drawing.Color.White;
            this.btnFacturas.Image = ((System.Drawing.Image)(resources.GetObject("btnFacturas.Image")));
            this.btnFacturas.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnFacturas.Location = new System.Drawing.Point(0, 313);
            this.btnFacturas.Name = "btnFacturas";
            this.btnFacturas.Size = new System.Drawing.Size(115, 68);
            this.btnFacturas.TabIndex = 7;
            this.btnFacturas.Text = "Facturas";
            this.btnFacturas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnFacturas.UseVisualStyleBackColor = true;
            this.btnFacturas.Click += new System.EventHandler(this.btnFacturas_Click);
            // 
            // btnProveedores
            // 
            this.btnProveedores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProveedores.FlatAppearance.BorderSize = 0;
            this.btnProveedores.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnProveedores.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnProveedores.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProveedores.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProveedores.ForeColor = System.Drawing.Color.White;
            this.btnProveedores.Image = ((System.Drawing.Image)(resources.GetObject("btnProveedores.Image")));
            this.btnProveedores.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnProveedores.Location = new System.Drawing.Point(0, 164);
            this.btnProveedores.Name = "btnProveedores";
            this.btnProveedores.Size = new System.Drawing.Size(115, 68);
            this.btnProveedores.TabIndex = 6;
            this.btnProveedores.Text = "Proveedores";
            this.btnProveedores.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnProveedores.UseVisualStyleBackColor = true;
            this.btnProveedores.Click += new System.EventHandler(this.btnProveedores_Click);
            // 
            // btnClientes
            // 
            this.btnClientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClientes.FlatAppearance.BorderSize = 0;
            this.btnClientes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnClientes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnClientes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClientes.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClientes.ForeColor = System.Drawing.Color.White;
            this.btnClientes.Image = ((System.Drawing.Image)(resources.GetObject("btnClientes.Image")));
            this.btnClientes.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnClientes.Location = new System.Drawing.Point(115, 164);
            this.btnClientes.Name = "btnClientes";
            this.btnClientes.Size = new System.Drawing.Size(115, 68);
            this.btnClientes.TabIndex = 5;
            this.btnClientes.Text = "Clientes";
            this.btnClientes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnClientes.UseVisualStyleBackColor = true;
            this.btnClientes.Click += new System.EventHandler(this.btnClientes_Click);
            // 
            // btnReportes
            // 
            this.btnReportes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReportes.FlatAppearance.BorderSize = 0;
            this.btnReportes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnReportes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnReportes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReportes.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReportes.ForeColor = System.Drawing.Color.White;
            this.btnReportes.Image = ((System.Drawing.Image)(resources.GetObject("btnReportes.Image")));
            this.btnReportes.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnReportes.Location = new System.Drawing.Point(0, 238);
            this.btnReportes.Name = "btnReportes";
            this.btnReportes.Size = new System.Drawing.Size(115, 69);
            this.btnReportes.TabIndex = 4;
            this.btnReportes.Text = "Reportes";
            this.btnReportes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnReportes.UseVisualStyleBackColor = true;
            this.btnReportes.Click += new System.EventHandler(this.btnReportes_Click);
            // 
            // btnEmpleados
            // 
            this.btnEmpleados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmpleados.FlatAppearance.BorderSize = 0;
            this.btnEmpleados.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnEmpleados.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnEmpleados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmpleados.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmpleados.ForeColor = System.Drawing.Color.White;
            this.btnEmpleados.Image = ((System.Drawing.Image)(resources.GetObject("btnEmpleados.Image")));
            this.btnEmpleados.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnEmpleados.Location = new System.Drawing.Point(115, 86);
            this.btnEmpleados.Name = "btnEmpleados";
            this.btnEmpleados.Size = new System.Drawing.Size(115, 72);
            this.btnEmpleados.TabIndex = 3;
            this.btnEmpleados.Text = "Empleados";
            this.btnEmpleados.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnEmpleados.UseVisualStyleBackColor = true;
            this.btnEmpleados.Click += new System.EventHandler(this.btnEmpleados_Click);
            // 
            // btnServicios
            // 
            this.btnServicios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnServicios.FlatAppearance.BorderSize = 0;
            this.btnServicios.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnServicios.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnServicios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnServicios.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnServicios.ForeColor = System.Drawing.Color.White;
            this.btnServicios.Image = ((System.Drawing.Image)(resources.GetObject("btnServicios.Image")));
            this.btnServicios.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnServicios.Location = new System.Drawing.Point(0, 89);
            this.btnServicios.Name = "btnServicios";
            this.btnServicios.Size = new System.Drawing.Size(115, 69);
            this.btnServicios.TabIndex = 2;
            this.btnServicios.Text = "Servicios";
            this.btnServicios.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnServicios.UseVisualStyleBackColor = true;
            // 
            // btnVentas
            // 
            this.btnVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVentas.FlatAppearance.BorderSize = 0;
            this.btnVentas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnVentas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVentas.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVentas.ForeColor = System.Drawing.Color.White;
            this.btnVentas.Image = ((System.Drawing.Image)(resources.GetObject("btnVentas.Image")));
            this.btnVentas.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnVentas.Location = new System.Drawing.Point(0, 17);
            this.btnVentas.Name = "btnVentas";
            this.btnVentas.Size = new System.Drawing.Size(115, 66);
            this.btnVentas.TabIndex = 1;
            this.btnVentas.Text = "Ventas";
            this.btnVentas.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnVentas.UseVisualStyleBackColor = true;
            this.btnVentas.Click += new System.EventHandler(this.btnVentas_Click);
            // 
            // btnProductos
            // 
            this.btnProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProductos.FlatAppearance.BorderSize = 0;
            this.btnProductos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(61)))), ((int)(((byte)(92)))));
            this.btnProductos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProductos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProductos.ForeColor = System.Drawing.Color.White;
            this.btnProductos.Image = ((System.Drawing.Image)(resources.GetObject("btnProductos.Image")));
            this.btnProductos.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnProductos.Location = new System.Drawing.Point(115, 17);
            this.btnProductos.Name = "btnProductos";
            this.btnProductos.Size = new System.Drawing.Size(115, 63);
            this.btnProductos.TabIndex = 0;
            this.btnProductos.Text = "Productos";
            this.btnProductos.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnProductos.UseVisualStyleBackColor = true;
            this.btnProductos.Click += new System.EventHandler(this.btnProductos_Click);
            // 
            // temporizador_respaldo
            // 
            this.temporizador_respaldo.Interval = 900000;
            this.temporizador_respaldo.Tick += new System.EventHandler(this.temporizador_respaldo_Tick);
            // 
            // timerProductos
            // 
            this.timerProductos.Enabled = true;
            this.timerProductos.Interval = 180000;
            this.timerProductos.Tick += new System.EventHandler(this.timerProductos_Tick);
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 602);
            this.Controls.Add(this.panelMaestro);
            this.MinimumSize = new System.Drawing.Size(1100, 597);
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Punto de Venta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPrincipal_FormClosing);
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.panelMaestro.ResumeLayout(false);
            this.panelContenedor.ResumeLayout(false);
            this.menuVertical.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMaestro;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Panel menuVertical;
        private System.Windows.Forms.Button btnEmpleados;
        private System.Windows.Forms.Button btnServicios;
        private System.Windows.Forms.Button btnVentas;
        private System.Windows.Forms.Button btnProductos;
        private System.Windows.Forms.Button btnMisDatos;
        private System.Windows.Forms.Button btnInventario;
        private System.Windows.Forms.Button btnCaja;
        private System.Windows.Forms.Button btnAnticipos;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnFacturas;
        private System.Windows.Forms.Button btnProveedores;
        private System.Windows.Forms.Button btnClientes;
        private System.Windows.Forms.Button btnReportes;
        private System.Windows.Forms.Button btnEmpresas;
        private System.Windows.Forms.Timer temporizador_respaldo;
        private System.Windows.Forms.Timer actualizarCaja;
        private System.Windows.Forms.Timer timerProductos;
        private System.Windows.Forms.Button btnSesion;
        private System.Windows.Forms.Button btnCalculadora;
    }
}

