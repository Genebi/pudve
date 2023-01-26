namespace PuntoDeVentaV2
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RBTicket = new System.Windows.Forms.RadioButton();
            this.RBpdf = new System.Windows.Forms.RadioButton();
            this.chkPreguntar = new System.Windows.Forms.CheckBox();
            this.chTicketVentas = new System.Windows.Forms.CheckBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.CHKMostrarStock = new System.Windows.Forms.CheckBox();
            this.chkMensajeRealizarInventario = new System.Windows.Forms.CheckBox();
            this.chTraspasos = new System.Windows.Forms.CheckBox();
            this.chkMensajeVenderProducto = new System.Windows.Forms.CheckBox();
            this.chkCerrarSesionCorte = new System.Windows.Forms.CheckBox();
            this.checkCBVenta = new System.Windows.Forms.CheckBox();
            this.cbMostrarCB = new System.Windows.Forms.CheckBox();
            this.cbMostrarPrecio = new System.Windows.Forms.CheckBox();
            this.cbStockNegativo = new System.Windows.Forms.CheckBox();
            this.pagWeb = new System.Windows.Forms.CheckBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNoVendidos = new System.Windows.Forms.TextBox();
            this.checkNoVendidos = new System.Windows.Forms.CheckBox();
            this.checkMayoreo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMinimoMayoreo = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.linkLabel2);
            this.groupBox2.Controls.Add(this.CHKMostrarStock);
            this.groupBox2.Controls.Add(this.chkMensajeRealizarInventario);
            this.groupBox2.Controls.Add(this.chTraspasos);
            this.groupBox2.Controls.Add(this.chkMensajeVenderProducto);
            this.groupBox2.Controls.Add(this.chkCerrarSesionCorte);
            this.groupBox2.Controls.Add(this.checkCBVenta);
            this.groupBox2.Controls.Add(this.cbMostrarCB);
            this.groupBox2.Controls.Add(this.cbMostrarPrecio);
            this.groupBox2.Controls.Add(this.cbStockNegativo);
            this.groupBox2.Controls.Add(this.pagWeb);
            this.groupBox2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(878, 174);
            this.groupBox2.TabIndex = 130;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CONFIGURACION GENERAL";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RBTicket);
            this.groupBox1.Controls.Add(this.RBpdf);
            this.groupBox1.Controls.Add(this.chkPreguntar);
            this.groupBox1.Controls.Add(this.chTicketVentas);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(608, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(257, 79);
            this.groupBox1.TabIndex = 133;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ticket Venta";
            // 
            // RBTicket
            // 
            this.RBTicket.AutoSize = true;
            this.RBTicket.Location = new System.Drawing.Point(7, 54);
            this.RBTicket.Name = "RBTicket";
            this.RBTicket.Size = new System.Drawing.Size(100, 19);
            this.RBTicket.TabIndex = 137;
            this.RBTicket.TabStop = true;
            this.RBTicket.Text = "Imprimir Ticket";
            this.RBTicket.UseVisualStyleBackColor = true;
            this.RBTicket.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RBTicket_MouseClick_1);
            // 
            // RBpdf
            // 
            this.RBpdf.AutoSize = true;
            this.RBpdf.Location = new System.Drawing.Point(110, 54);
            this.RBpdf.Name = "RBpdf";
            this.RBpdf.Size = new System.Drawing.Size(143, 19);
            this.RBpdf.TabIndex = 136;
            this.RBpdf.TabStop = true;
            this.RBpdf.Text = "Imprimir nota de venta";
            this.RBpdf.UseVisualStyleBackColor = true;
            this.RBpdf.MouseClick += new System.Windows.Forms.MouseEventHandler(this.RBpdf_MouseClick_1);
            // 
            // chkPreguntar
            // 
            this.chkPreguntar.AutoSize = true;
            this.chkPreguntar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkPreguntar.Location = new System.Drawing.Point(15, 33);
            this.chkPreguntar.Name = "chkPreguntar";
            this.chkPreguntar.Size = new System.Drawing.Size(233, 21);
            this.chkPreguntar.TabIndex = 135;
            this.chkPreguntar.Text = "Pregutar si desea imprimir ticket";
            this.chkPreguntar.UseVisualStyleBackColor = true;
            this.chkPreguntar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkPreguntar_MouseClick);
            // 
            // chTicketVentas
            // 
            this.chTicketVentas.AutoSize = true;
            this.chTicketVentas.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chTicketVentas.Location = new System.Drawing.Point(15, 13);
            this.chTicketVentas.Name = "chTicketVentas";
            this.chTicketVentas.Size = new System.Drawing.Size(232, 21);
            this.chTicketVentas.TabIndex = 128;
            this.chTicketVentas.Text = "Generar ticket al realizar ventas";
            this.chTicketVentas.UseVisualStyleBackColor = true;
            this.chTicketVentas.CheckedChanged += new System.EventHandler(this.chTicketVentas_CheckedChanged);
            this.chTicketVentas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chTicketVentas_MouseClick);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(212, 54);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(87, 16);
            this.linkLabel2.TabIndex = 134;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "página web";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // CHKMostrarStock
            // 
            this.CHKMostrarStock.AutoSize = true;
            this.CHKMostrarStock.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CHKMostrarStock.Location = new System.Drawing.Point(325, 80);
            this.CHKMostrarStock.Name = "CHKMostrarStock";
            this.CHKMostrarStock.Size = new System.Drawing.Size(243, 21);
            this.CHKMostrarStock.TabIndex = 133;
            this.CHKMostrarStock.Text = "Mostrar Stock en Consultar Precio";
            this.CHKMostrarStock.UseVisualStyleBackColor = true;
            this.CHKMostrarStock.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CHKMostrarStock_MouseClick);
            // 
            // chkMensajeRealizarInventario
            // 
            this.chkMensajeRealizarInventario.AutoSize = true;
            this.chkMensajeRealizarInventario.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMensajeRealizarInventario.Location = new System.Drawing.Point(175, 133);
            this.chkMensajeRealizarInventario.Name = "chkMensajeRealizarInventario";
            this.chkMensajeRealizarInventario.Size = new System.Drawing.Size(267, 21);
            this.chkMensajeRealizarInventario.TabIndex = 131;
            this.chkMensajeRealizarInventario.Text = "Mostrar mensaje al realizar inventario";
            this.chkMensajeRealizarInventario.UseVisualStyleBackColor = true;
            this.chkMensajeRealizarInventario.Visible = false;
            this.chkMensajeRealizarInventario.CheckedChanged += new System.EventHandler(this.chkMensajeRealizarInventario_CheckedChanged);
            this.chkMensajeRealizarInventario.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkMensajeRealizarInventario_MouseClick);
            // 
            // chTraspasos
            // 
            this.chTraspasos.AutoSize = true;
            this.chTraspasos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chTraspasos.Location = new System.Drawing.Point(325, 109);
            this.chTraspasos.Name = "chTraspasos";
            this.chTraspasos.Size = new System.Drawing.Size(149, 21);
            this.chTraspasos.TabIndex = 130;
            this.chTraspasos.Text = "Multiples sucursales";
            this.chTraspasos.UseVisualStyleBackColor = true;
            this.chTraspasos.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chTraspasos_MouseClick);
            // 
            // chkMensajeVenderProducto
            // 
            this.chkMensajeVenderProducto.AutoSize = true;
            this.chkMensajeVenderProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMensajeVenderProducto.Location = new System.Drawing.Point(37, 133);
            this.chkMensajeVenderProducto.Name = "chkMensajeVenderProducto";
            this.chkMensajeVenderProducto.Size = new System.Drawing.Size(262, 21);
            this.chkMensajeVenderProducto.TabIndex = 130;
            this.chkMensajeVenderProducto.Text = "Mostrar mensaje al vender producto";
            this.chkMensajeVenderProducto.UseVisualStyleBackColor = true;
            this.chkMensajeVenderProducto.Visible = false;
            this.chkMensajeVenderProducto.CheckedChanged += new System.EventHandler(this.chkMensajeVenderProducto_CheckedChanged);
            this.chkMensajeVenderProducto.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkMensajeVenderProducto_MouseClick);
            // 
            // chkCerrarSesionCorte
            // 
            this.chkCerrarSesionCorte.AutoSize = true;
            this.chkCerrarSesionCorte.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCerrarSesionCorte.Location = new System.Drawing.Point(37, 109);
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
            this.checkCBVenta.Location = new System.Drawing.Point(37, 26);
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
            this.cbMostrarCB.Location = new System.Drawing.Point(37, 80);
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
            this.cbMostrarPrecio.Location = new System.Drawing.Point(325, 26);
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
            this.cbStockNegativo.Location = new System.Drawing.Point(325, 53);
            this.cbStockNegativo.Name = "cbStockNegativo";
            this.cbStockNegativo.Size = new System.Drawing.Size(177, 21);
            this.cbStockNegativo.TabIndex = 1;
            this.cbStockNegativo.Text = "Permitir Stock negativo";
            this.cbStockNegativo.UseVisualStyleBackColor = true;
            this.cbStockNegativo.CheckedChanged += new System.EventHandler(this.cbStockNegativo_CheckedChanged);
            this.cbStockNegativo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbStockNegativo_MouseClick);
            // 
            // pagWeb
            // 
            this.pagWeb.AutoSize = true;
            this.pagWeb.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pagWeb.Location = new System.Drawing.Point(37, 53);
            this.pagWeb.Name = "pagWeb";
            this.pagWeb.Size = new System.Drawing.Size(267, 21);
            this.pagWeb.TabIndex = 115;
            this.pagWeb.Text = "Habilitar información en página web";
            this.pagWeb.UseVisualStyleBackColor = true;
            this.pagWeb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pagWeb_MouseClick);
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
            this.btnAceptar.Location = new System.Drawing.Point(340, 204);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(176, 28);
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
            this.ClientSize = new System.Drawing.Size(893, 235);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.checkMayoreo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.txtMinimoMayoreo);
            this.Controls.Add(this.checkNoVendidos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNoVendidos);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chTicketVentas;
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
        private System.Windows.Forms.CheckBox chkPreguntar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RBTicket;
        private System.Windows.Forms.RadioButton RBpdf;
    }
}