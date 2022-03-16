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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.chkMensajeRealizarInventario = new System.Windows.Forms.CheckBox();
            this.chkMensajeVenderProducto = new System.Windows.Forms.CheckBox();
            this.chkCerrarSesionCorte = new System.Windows.Forms.CheckBox();
            this.chTicketVentas = new System.Windows.Forms.CheckBox();
            this.checkCBVenta = new System.Windows.Forms.CheckBox();
            this.pagWeb = new System.Windows.Forms.CheckBox();
            this.cbMostrarCB = new System.Windows.Forms.CheckBox();
            this.cbMostrarPrecio = new System.Windows.Forms.CheckBox();
            this.cbStockNegativo = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNoVendidos = new System.Windows.Forms.TextBox();
            this.checkNoVendidos = new System.Windows.Forms.CheckBox();
            this.checkMayoreo = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMinimoMayoreo = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.groupBox2.Controls.Add(this.btnAceptar);
            this.groupBox2.Controls.Add(this.chkMensajeRealizarInventario);
            this.groupBox2.Controls.Add(this.chkMensajeVenderProducto);
            this.groupBox2.Controls.Add(this.chkCerrarSesionCorte);
            this.groupBox2.Controls.Add(this.chTicketVentas);
            this.groupBox2.Controls.Add(this.checkCBVenta);
            this.groupBox2.Controls.Add(this.pagWeb);
            this.groupBox2.Controls.Add(this.cbMostrarCB);
            this.groupBox2.Controls.Add(this.cbMostrarPrecio);
            this.groupBox2.Controls.Add(this.cbStockNegativo);
            this.groupBox2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(8, 31);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(637, 195);
            this.groupBox2.TabIndex = 130;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "CONFIGURACION GENERAL";
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
            this.btnAceptar.Location = new System.Drawing.Point(456, 161);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(176, 28);
            this.btnAceptar.TabIndex = 132;
            this.btnAceptar.Text = "Guardar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // chkMensajeRealizarInventario
            // 
            this.chkMensajeRealizarInventario.AutoSize = true;
            this.chkMensajeRealizarInventario.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMensajeRealizarInventario.Location = new System.Drawing.Point(175, 136);
            this.chkMensajeRealizarInventario.Name = "chkMensajeRealizarInventario";
            this.chkMensajeRealizarInventario.Size = new System.Drawing.Size(267, 21);
            this.chkMensajeRealizarInventario.TabIndex = 131;
            this.chkMensajeRealizarInventario.Text = "Mostrar mensaje al realizar inventario";
            this.chkMensajeRealizarInventario.UseVisualStyleBackColor = true;
            this.chkMensajeRealizarInventario.CheckedChanged += new System.EventHandler(this.chkMensajeRealizarInventario_CheckedChanged);
            this.chkMensajeRealizarInventario.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chkMensajeRealizarInventario_MouseClick);
            // 
            // chkMensajeVenderProducto
            // 
            this.chkMensajeVenderProducto.AutoSize = true;
            this.chkMensajeVenderProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkMensajeVenderProducto.Location = new System.Drawing.Point(325, 109);
            this.chkMensajeVenderProducto.Name = "chkMensajeVenderProducto";
            this.chkMensajeVenderProducto.Size = new System.Drawing.Size(262, 21);
            this.chkMensajeVenderProducto.TabIndex = 130;
            this.chkMensajeVenderProducto.Text = "Mostrar mensaje al vender producto";
            this.chkMensajeVenderProducto.UseVisualStyleBackColor = true;
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
            // chTicketVentas
            // 
            this.chTicketVentas.AutoSize = true;
            this.chTicketVentas.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chTicketVentas.Location = new System.Drawing.Point(325, 80);
            this.chTicketVentas.Name = "chTicketVentas";
            this.chTicketVentas.Size = new System.Drawing.Size(232, 21);
            this.chTicketVentas.TabIndex = 128;
            this.chTicketVentas.Text = "Generar ticket al realizar ventas";
            this.chTicketVentas.UseVisualStyleBackColor = true;
            this.chTicketVentas.CheckedChanged += new System.EventHandler(this.chTicketVentas_CheckedChanged);
            this.chTicketVentas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chTicketVentas_MouseClick);
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
            this.pagWeb.CheckedChanged += new System.EventHandler(this.pagWeb_CheckedChanged);
            this.pagWeb.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pagWeb_MouseClick);
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
            this.ClientSize = new System.Drawing.Size(657, 238);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.checkMayoreo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtMinimoMayoreo);
            this.Controls.Add(this.checkNoVendidos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtNoVendidos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfiguracionGeneral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Configuración General";
            this.Load += new System.EventHandler(this.ConfiguracionGeneral_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConfiguracionGeneral_KeyDown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
    }
}