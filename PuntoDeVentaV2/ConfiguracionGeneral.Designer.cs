﻿namespace PuntoDeVentaV2
{
    partial class ConfiguracionGeneral
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfiguracionGeneral));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkOrdenes = new System.Windows.Forms.CheckBox();
            this.checkRentas = new System.Windows.Forms.CheckBox();
            this.numDiasCad = new System.Windows.Forms.NumericUpDown();
            this.cbkMostrarIVA = new System.Windows.Forms.CheckBox();
            this.cbWebReportesPeriodicos = new System.Windows.Forms.CheckBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.chWebTotal = new System.Windows.Forms.CheckBox();
            this.chbCaducidad = new System.Windows.Forms.CheckBox();
            this.pagWeb = new System.Windows.Forms.CheckBox();
            this.chWebCerrar = new System.Windows.Forms.CheckBox();
            this.chbVentaFacil = new System.Windows.Forms.CheckBox();
            this.CHKMostrarStock = new System.Windows.Forms.CheckBox();
            this.chbTraspasoManual = new System.Windows.Forms.CheckBox();
            this.chTraspasos = new System.Windows.Forms.CheckBox();
            this.chkCerrarSesionCorte = new System.Windows.Forms.CheckBox();
            this.checkCBVenta = new System.Windows.Forms.CheckBox();
            this.cbMostrarCB = new System.Windows.Forms.CheckBox();
            this.cbMostrarPrecio = new System.Windows.Forms.CheckBox();
            this.cbStockNegativo = new System.Windows.Forms.CheckBox();
            this.chkMensajeVenderProducto = new System.Windows.Forms.CheckBox();
            this.chkMensajeRealizarInventario = new System.Windows.Forms.CheckBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNoVendidos = new System.Windows.Forms.TextBox();
            this.checkNoVendidos = new System.Windows.Forms.CheckBox();
            this.checkMayoreo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMinimoMayoreo = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiasCad)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.groupBox2.Controls.Add(this.checkOrdenes);
            this.groupBox2.Controls.Add(this.checkRentas);
            this.groupBox2.Controls.Add(this.numDiasCad);
            this.groupBox2.Controls.Add(this.cbkMostrarIVA);
            this.groupBox2.Controls.Add(this.cbWebReportesPeriodicos);
            this.groupBox2.Controls.Add(this.linkLabel2);
            this.groupBox2.Controls.Add(this.chWebTotal);
            this.groupBox2.Controls.Add(this.chbCaducidad);
            this.groupBox2.Controls.Add(this.pagWeb);
            this.groupBox2.Controls.Add(this.chWebCerrar);
            this.groupBox2.Controls.Add(this.chbVentaFacil);
            this.groupBox2.Controls.Add(this.CHKMostrarStock);
            this.groupBox2.Controls.Add(this.chbTraspasoManual);
            this.groupBox2.Controls.Add(this.chTraspasos);
            this.groupBox2.Controls.Add(this.chkCerrarSesionCorte);
            this.groupBox2.Controls.Add(this.checkCBVenta);
            this.groupBox2.Controls.Add(this.cbMostrarCB);
            this.groupBox2.Controls.Add(this.cbMostrarPrecio);
            this.groupBox2.Controls.Add(this.cbStockNegativo);
            this.groupBox2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(9, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(648, 288);
            this.groupBox2.TabIndex = 130;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CONFIGURACION GENERAL";
            // 
            // checkOrdenes
            // 
            this.checkOrdenes.AutoSize = true;
            this.checkOrdenes.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkOrdenes.Location = new System.Drawing.Point(308, 225);
            this.checkOrdenes.Name = "checkOrdenes";
            this.checkOrdenes.Size = new System.Drawing.Size(211, 21);
            this.checkOrdenes.TabIndex = 137;
            this.checkOrdenes.Text = "Este negocio realiza órdenes";
            this.checkOrdenes.UseVisualStyleBackColor = true;
            this.checkOrdenes.Visible = false;
            this.checkOrdenes.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkOrdenes_MouseClick);
            // 
            // checkRentas
            // 
            this.checkRentas.AutoSize = true;
            this.checkRentas.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkRentas.Location = new System.Drawing.Point(20, 225);
            this.checkRentas.Name = "checkRentas";
            this.checkRentas.Size = new System.Drawing.Size(199, 21);
            this.checkRentas.TabIndex = 136;
            this.checkRentas.Text = "Este negocio realiza rentas";
            this.checkRentas.UseVisualStyleBackColor = true;
            this.checkRentas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkRentas_MouseClick);
            // 
            // numDiasCad
            // 
            this.numDiasCad.Enabled = false;
            this.numDiasCad.Location = new System.Drawing.Point(224, 259);
            this.numDiasCad.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numDiasCad.Name = "numDiasCad";
            this.numDiasCad.Size = new System.Drawing.Size(76, 23);
            this.numDiasCad.TabIndex = 135;
            this.numDiasCad.Visible = false;
            // 
            // cbkMostrarIVA
            // 
            this.cbkMostrarIVA.AutoSize = true;
            this.cbkMostrarIVA.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbkMostrarIVA.Location = new System.Drawing.Point(20, 84);
            this.cbkMostrarIVA.Name = "cbkMostrarIVA";
            this.cbkMostrarIVA.Size = new System.Drawing.Size(154, 21);
            this.cbkMostrarIVA.TabIndex = 135;
            this.cbkMostrarIVA.Text = "Mostra IVA en ventas";
            this.cbkMostrarIVA.UseVisualStyleBackColor = true;
            this.cbkMostrarIVA.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbkMostrarIVA_MouseClick);
            // 
            // cbWebReportesPeriodicos
            // 
            this.cbWebReportesPeriodicos.AutoSize = true;
            this.cbWebReportesPeriodicos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWebReportesPeriodicos.Location = new System.Drawing.Point(308, 168);
            this.cbWebReportesPeriodicos.Name = "cbWebReportesPeriodicos";
            this.cbWebReportesPeriodicos.Size = new System.Drawing.Size(300, 21);
            this.cbWebReportesPeriodicos.TabIndex = 0;
            this.cbWebReportesPeriodicos.Text = "Realizar reportes a la nube automáticamente ";
            this.cbWebReportesPeriodicos.UseVisualStyleBackColor = true;
            this.cbWebReportesPeriodicos.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbWebReportesPeriodicos_MouseClick);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(540, 138);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(86, 16);
            this.linkLabel2.TabIndex = 134;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "página web";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // chWebTotal
            // 
            this.chWebTotal.AutoSize = true;
            this.chWebTotal.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chWebTotal.Location = new System.Drawing.Point(308, 195);
            this.chWebTotal.Name = "chWebTotal";
            this.chWebTotal.Size = new System.Drawing.Size(262, 21);
            this.chWebTotal.TabIndex = 0;
            this.chWebTotal.Text = "Realizar respaldos totales (base entera)";
            this.chWebTotal.UseVisualStyleBackColor = true;
            this.chWebTotal.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chWebTotal_MouseClick);
            // 
            // chbCaducidad
            // 
            this.chbCaducidad.AutoSize = true;
            this.chbCaducidad.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbCaducidad.Location = new System.Drawing.Point(20, 260);
            this.chbCaducidad.Name = "chbCaducidad";
            this.chbCaducidad.Size = new System.Drawing.Size(593, 21);
            this.chbCaducidad.TabIndex = 115;
            this.chbCaducidad.Text = "Notificarme cuando falten                        días o menos para que caduque un" +
    " producto";
            this.chbCaducidad.UseVisualStyleBackColor = true;
            this.chbCaducidad.Visible = false;
            this.chbCaducidad.CheckedChanged += new System.EventHandler(this.chbCaducidad_CheckedChanged);
            this.chbCaducidad.Click += new System.EventHandler(this.chbCaducidad_Click);
            // 
            // pagWeb
            // 
            this.pagWeb.AutoSize = true;
            this.pagWeb.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pagWeb.Location = new System.Drawing.Point(308, 137);
            this.pagWeb.Name = "pagWeb";
            this.pagWeb.Size = new System.Drawing.Size(237, 21);
            this.pagWeb.TabIndex = 115;
            this.pagWeb.Text = "Habilitar envío de información a";
            this.pagWeb.UseVisualStyleBackColor = true;
            this.pagWeb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pagWeb_MouseClick);
            // 
            // chWebCerrar
            // 
            this.chWebCerrar.AutoSize = true;
            this.chWebCerrar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chWebCerrar.Location = new System.Drawing.Point(308, 110);
            this.chWebCerrar.Name = "chWebCerrar";
            this.chWebCerrar.Size = new System.Drawing.Size(231, 21);
            this.chWebCerrar.TabIndex = 0;
            this.chWebCerrar.Text = "Reportar a la nube antes de cerrar";
            this.chWebCerrar.UseVisualStyleBackColor = true;
            this.chWebCerrar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chWebCerrar_MouseClick);
            // 
            // chbVentaFacil
            // 
            this.chbVentaFacil.AutoSize = true;
            this.chbVentaFacil.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbVentaFacil.Location = new System.Drawing.Point(20, 195);
            this.chbVentaFacil.Name = "chbVentaFacil";
            this.chbVentaFacil.Size = new System.Drawing.Size(287, 21);
            this.chbVentaFacil.TabIndex = 133;
            this.chbVentaFacil.Text = "Habilitar venta fácil en detalle de producto";
            this.chbVentaFacil.UseVisualStyleBackColor = true;
            this.chbVentaFacil.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chbVentaRapida_MouseClick);
            // 
            // CHKMostrarStock
            // 
            this.CHKMostrarStock.AutoSize = true;
            this.CHKMostrarStock.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKMostrarStock.Location = new System.Drawing.Point(20, 168);
            this.CHKMostrarStock.Name = "CHKMostrarStock";
            this.CHKMostrarStock.Size = new System.Drawing.Size(243, 21);
            this.CHKMostrarStock.TabIndex = 133;
            this.CHKMostrarStock.Text = "Mostrar Stock en Consultar Precio";
            this.CHKMostrarStock.UseVisualStyleBackColor = true;
            this.CHKMostrarStock.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CHKMostrarStock_MouseClick);
            // 
            // chbTraspasoManual
            // 
            this.chbTraspasoManual.AutoSize = true;
            this.chbTraspasoManual.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbTraspasoManual.Location = new System.Drawing.Point(20, 137);
            this.chbTraspasoManual.Name = "chbTraspasoManual";
            this.chbTraspasoManual.Size = new System.Drawing.Size(232, 21);
            this.chbTraspasoManual.TabIndex = 130;
            this.chbTraspasoManual.Text = "Revisar traspasos manualmente";
            this.chbTraspasoManual.UseVisualStyleBackColor = true;
            this.chbTraspasoManual.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chbTraspasoManual_MouseClick);
            // 
            // chTraspasos
            // 
            this.chTraspasos.AutoSize = true;
            this.chTraspasos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chTraspasos.Location = new System.Drawing.Point(20, 110);
            this.chTraspasos.Name = "chTraspasos";
            this.chTraspasos.Size = new System.Drawing.Size(149, 21);
            this.chTraspasos.TabIndex = 130;
            this.chTraspasos.Text = "Multiples sucursales";
            this.chTraspasos.UseVisualStyleBackColor = true;
            this.chTraspasos.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chTraspasos_MouseClick);
            // 
            // chkCerrarSesionCorte
            // 
            this.chkCerrarSesionCorte.AutoSize = true;
            this.chkCerrarSesionCorte.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCerrarSesionCorte.Location = new System.Drawing.Point(308, 84);
            this.chkCerrarSesionCorte.Name = "chkCerrarSesionCorte";
            this.chkCerrarSesionCorte.Size = new System.Drawing.Size(258, 21);
            this.chkCerrarSesionCorte.TabIndex = 129;
            this.chkCerrarSesionCorte.Text = "Cerrar sesion al hacer corte de caja";
            this.chkCerrarSesionCorte.UseVisualStyleBackColor = true;
            this.chkCerrarSesionCorte.CheckedChanged += new System.EventHandler(this.chkCerrarSesionCorte_CheckedChanged);
            this.chkCerrarSesionCorte.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkCerrarSesionCorte_MouseClick);
            // 
            // checkCBVenta
            // 
            this.checkCBVenta.AutoSize = true;
            this.checkCBVenta.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkCBVenta.Location = new System.Drawing.Point(20, 29);
            this.checkCBVenta.Name = "checkCBVenta";
            this.checkCBVenta.Size = new System.Drawing.Size(245, 21);
            this.checkCBVenta.TabIndex = 110;
            this.checkCBVenta.Text = "Código de barras ticket de venta";
            this.checkCBVenta.UseVisualStyleBackColor = true;
            this.checkCBVenta.CheckedChanged += new System.EventHandler(this.checkCBVenta_CheckedChanged);
            this.checkCBVenta.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkCBVenta_MouseClick);
            // 
            // cbMostrarCB
            // 
            this.cbMostrarCB.AutoSize = true;
            this.cbMostrarCB.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMostrarCB.Location = new System.Drawing.Point(20, 51);
            this.cbMostrarCB.Name = "cbMostrarCB";
            this.cbMostrarCB.Size = new System.Drawing.Size(283, 21);
            this.cbMostrarCB.TabIndex = 117;
            this.cbMostrarCB.Text = "Mostrar código de productos en ventas";
            this.cbMostrarCB.UseVisualStyleBackColor = true;
            this.cbMostrarCB.CheckedChanged += new System.EventHandler(this.cbMostrarCB_CheckedChanged);
            this.cbMostrarCB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbMostrarCB_MouseClick);
            // 
            // cbMostrarPrecio
            // 
            this.cbMostrarPrecio.AutoSize = true;
            this.cbMostrarPrecio.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMostrarPrecio.Location = new System.Drawing.Point(308, 29);
            this.cbMostrarPrecio.Name = "cbMostrarPrecio";
            this.cbMostrarPrecio.Size = new System.Drawing.Size(277, 21);
            this.cbMostrarPrecio.TabIndex = 116;
            this.cbMostrarPrecio.Text = "Mostrar precio de productos en ventas";
            this.cbMostrarPrecio.UseVisualStyleBackColor = true;
            this.cbMostrarPrecio.CheckedChanged += new System.EventHandler(this.cbMostrarPrecio_CheckedChanged);
            this.cbMostrarPrecio.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbMostrarPrecio_MouseClick);
            // 
            // cbStockNegativo
            // 
            this.cbStockNegativo.AutoSize = true;
            this.cbStockNegativo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStockNegativo.Location = new System.Drawing.Point(308, 57);
            this.cbStockNegativo.Name = "cbStockNegativo";
            this.cbStockNegativo.Size = new System.Drawing.Size(177, 21);
            this.cbStockNegativo.TabIndex = 1;
            this.cbStockNegativo.Text = "Permitir Stock negativo";
            this.cbStockNegativo.UseVisualStyleBackColor = true;
            this.cbStockNegativo.CheckedChanged += new System.EventHandler(this.cbStockNegativo_CheckedChanged);
            this.cbStockNegativo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbStockNegativo_MouseClick);
            // 
            // chkMensajeVenderProducto
            // 
            this.chkMensajeVenderProducto.AutoSize = true;
            this.chkMensajeVenderProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMensajeVenderProducto.Location = new System.Drawing.Point(299, 1);
            this.chkMensajeVenderProducto.Name = "chkMensajeVenderProducto";
            this.chkMensajeVenderProducto.Size = new System.Drawing.Size(262, 21);
            this.chkMensajeVenderProducto.TabIndex = 130;
            this.chkMensajeVenderProducto.Text = "Mostrar mensaje al vender producto";
            this.chkMensajeVenderProducto.UseVisualStyleBackColor = true;
            this.chkMensajeVenderProducto.Visible = false;
            this.chkMensajeVenderProducto.CheckedChanged += new System.EventHandler(this.chkMensajeVenderProducto_CheckedChanged);
            this.chkMensajeVenderProducto.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkMensajeVenderProducto_MouseClick);
            // 
            // chkMensajeRealizarInventario
            // 
            this.chkMensajeRealizarInventario.AutoSize = true;
            this.chkMensajeRealizarInventario.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMensajeRealizarInventario.Location = new System.Drawing.Point(319, 4);
            this.chkMensajeRealizarInventario.Name = "chkMensajeRealizarInventario";
            this.chkMensajeRealizarInventario.Size = new System.Drawing.Size(267, 21);
            this.chkMensajeRealizarInventario.TabIndex = 131;
            this.chkMensajeRealizarInventario.Text = "Mostrar mensaje al realizar inventario";
            this.chkMensajeRealizarInventario.UseVisualStyleBackColor = true;
            this.chkMensajeRealizarInventario.Visible = false;
            this.chkMensajeRealizarInventario.CheckedChanged += new System.EventHandler(this.chkMensajeRealizarInventario_CheckedChanged);
            this.chkMensajeRealizarInventario.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkMensajeRealizarInventario_MouseClick);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.BackColor = System.Drawing.Color.Green;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(274, 312);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(117, 29);
            this.btnAceptar.TabIndex = 132;
            this.btnAceptar.Text = "Guardar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label3.Location = new System.Drawing.Point(90, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 17);
            this.label3.TabIndex = 127;
            this.label3.Text = "días";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label2.Location = new System.Drawing.Point(90, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 17);
            this.label2.TabIndex = 126;
            this.label2.Text = "- Cada";
            this.label2.Visible = false;
            // 
            // txtNoVendidos
            // 
            this.txtNoVendidos.Enabled = false;
            this.txtNoVendidos.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNoVendidos.Location = new System.Drawing.Point(168, 2);
            this.txtNoVendidos.Name = "txtNoVendidos";
            this.txtNoVendidos.Size = new System.Drawing.Size(69, 21);
            this.txtNoVendidos.TabIndex = 125;
            this.txtNoVendidos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNoVendidos.Visible = false;
            this.txtNoVendidos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtNoVendidos_KeyUp);
            // 
            // checkNoVendidos
            // 
            this.checkNoVendidos.AutoSize = true;
            this.checkNoVendidos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkNoVendidos.ForeColor = System.Drawing.SystemColors.Highlight;
            this.checkNoVendidos.Location = new System.Drawing.Point(73, 3);
            this.checkNoVendidos.Name = "checkNoVendidos";
            this.checkNoVendidos.Size = new System.Drawing.Size(240, 21);
            this.checkNoVendidos.TabIndex = 124;
            this.checkNoVendidos.Text = "Avisar de productos no vendidos";
            this.checkNoVendidos.UseVisualStyleBackColor = true;
            this.checkNoVendidos.Visible = false;
            this.checkNoVendidos.CheckedChanged += new System.EventHandler(this.checkNoVendidos_CheckedChanged);
            this.checkNoVendidos.Click += new System.EventHandler(this.checkNoVendidos_Click);
            this.checkNoVendidos.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkNoVendidos_MouseClick);
            // 
            // checkMayoreo
            // 
            this.checkMayoreo.AutoSize = true;
            this.checkMayoreo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkMayoreo.ForeColor = System.Drawing.SystemColors.Highlight;
            this.checkMayoreo.Location = new System.Drawing.Point(40, 7);
            this.checkMayoreo.Name = "checkMayoreo";
            this.checkMayoreo.Size = new System.Drawing.Size(273, 21);
            this.checkMayoreo.TabIndex = 121;
            this.checkMayoreo.Text = "Activar precio por mayoreo en ventas";
            this.checkMayoreo.UseVisualStyleBackColor = true;
            this.checkMayoreo.Visible = false;
            this.checkMayoreo.CheckedChanged += new System.EventHandler(this.checkMayoreo_CheckedChanged);
            this.checkMayoreo.Click += new System.EventHandler(this.checkMayoreo_Click);
            this.checkMayoreo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.checkMayoreo_MouseClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label1.Location = new System.Drawing.Point(105, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 17);
            this.label1.TabIndex = 123;
            this.label1.Text = "- Cantidad mínima";
            this.label1.Visible = false;
            // 
            // txtMinimoMayoreo
            // 
            this.txtMinimoMayoreo.Enabled = false;
            this.txtMinimoMayoreo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinimoMayoreo.Location = new System.Drawing.Point(93, 3);
            this.txtMinimoMayoreo.Name = "txtMinimoMayoreo";
            this.txtMinimoMayoreo.Size = new System.Drawing.Size(69, 21);
            this.txtMinimoMayoreo.TabIndex = 122;
            this.txtMinimoMayoreo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMinimoMayoreo.Visible = false;
            this.txtMinimoMayoreo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMinimoMayoreo_KeyUp);
            // 
            // ConfiguracionGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(663, 347);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.checkMayoreo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtMinimoMayoreo);
            this.Controls.Add(this.chkMensajeVenderProducto);
            this.Controls.Add(this.checkNoVendidos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNoVendidos);
            this.Controls.Add(this.chkMensajeRealizarInventario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfiguracionGeneral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuracion General";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfiguracionGeneral_FormClosing);
            this.Load += new System.EventHandler(this.ConfiguracionGeneral_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConfiguracionGeneral_KeyDown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiasCad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkCBVenta;
        private System.Windows.Forms.CheckBox pagWeb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox cbMostrarCB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox cbMostrarPrecio;
        private System.Windows.Forms.TextBox txtNoVendidos;
        private System.Windows.Forms.CheckBox cbStockNegativo;
        private System.Windows.Forms.CheckBox checkNoVendidos;
        private System.Windows.Forms.CheckBox checkMayoreo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMinimoMayoreo;
        private System.Windows.Forms.CheckBox chkCerrarSesionCorte;
        private System.Windows.Forms.CheckBox chkMensajeRealizarInventario;
        private System.Windows.Forms.CheckBox chkMensajeVenderProducto;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.CheckBox chTraspasos;
        private System.Windows.Forms.CheckBox CHKMostrarStock;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.CheckBox chWebTotal;
        private System.Windows.Forms.CheckBox chWebCerrar;
        private System.Windows.Forms.CheckBox cbWebReportesPeriodicos;
        private System.Windows.Forms.NumericUpDown numDiasCad;
        private System.Windows.Forms.CheckBox chbCaducidad;
        private System.Windows.Forms.CheckBox chbTraspasoManual;
        private System.Windows.Forms.CheckBox cbkMostrarIVA;
        private System.Windows.Forms.CheckBox chbVentaFacil;
        private System.Windows.Forms.CheckBox checkOrdenes;
        private System.Windows.Forms.CheckBox checkRentas;
    }
}