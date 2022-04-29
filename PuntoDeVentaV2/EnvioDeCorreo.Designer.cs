namespace PuntoDeVentaV2
{
    partial class EnvioDeCorreo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EnvioDeCorreo));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.chRespaldo = new System.Windows.Forms.CheckBox();
            this.cbCorreoDescuento = new System.Windows.Forms.CheckBox();
            this.cbCorreoIniciar = new System.Windows.Forms.CheckBox();
            this.cbCorreoVenta = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbCorreoCorteCaja = new System.Windows.Forms.CheckBox();
            this.cbCorreoEliminarListaProductosVentas = new System.Windows.Forms.CheckBox();
            this.cbCorreoCerrarVentanaVentas = new System.Windows.Forms.CheckBox();
            this.cbCorreoRetirarDineroCaja = new System.Windows.Forms.CheckBox();
            this.cbCorreoAgregarDineroCaja = new System.Windows.Forms.CheckBox();
            this.cbCorreoPrecioProducto = new System.Windows.Forms.CheckBox();
            this.cbCorreoStockProducto = new System.Windows.Forms.CheckBox();
            this.cbCorreoStockMinimo = new System.Windows.Forms.CheckBox();
            this.cbCorreoVenderProducto = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Controls.Add(this.chRespaldo);
            this.groupBox1.Controls.Add(this.cbCorreoDescuento);
            this.groupBox1.Controls.Add(this.cbCorreoIniciar);
            this.groupBox1.Controls.Add(this.cbCorreoVenta);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cbCorreoCorteCaja);
            this.groupBox1.Controls.Add(this.cbCorreoEliminarListaProductosVentas);
            this.groupBox1.Controls.Add(this.cbCorreoCerrarVentanaVentas);
            this.groupBox1.Controls.Add(this.cbCorreoRetirarDineroCaja);
            this.groupBox1.Controls.Add(this.cbCorreoAgregarDineroCaja);
            this.groupBox1.Controls.Add(this.cbCorreoPrecioProducto);
            this.groupBox1.Controls.Add(this.cbCorreoStockProducto);
            this.groupBox1.Controls.Add(this.cbCorreoStockMinimo);
            this.groupBox1.Controls.Add(this.cbCorreoVenderProducto);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(40, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(869, 233);
            this.groupBox1.TabIndex = 129;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Envío de Correo:";
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
            this.btnAceptar.Location = new System.Drawing.Point(685, 198);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(176, 28);
            this.btnAceptar.TabIndex = 133;
            this.btnAceptar.Text = "Guardar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // chRespaldo
            // 
            this.chRespaldo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chRespaldo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.chRespaldo.Location = new System.Drawing.Point(468, 159);
            this.chRespaldo.Name = "chRespaldo";
            this.chRespaldo.Size = new System.Drawing.Size(381, 21);
            this.chRespaldo.TabIndex = 126;
            this.chRespaldo.Text = "Enviar respaldo al cerrar sesion";
            this.chRespaldo.UseVisualStyleBackColor = true;
            this.chRespaldo.Visible = false;
            this.chRespaldo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chRespaldo_MouseClick);
            // 
            // cbCorreoDescuento
            // 
            this.cbCorreoDescuento.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCorreoDescuento.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbCorreoDescuento.Location = new System.Drawing.Point(468, 136);
            this.cbCorreoDescuento.Name = "cbCorreoDescuento";
            this.cbCorreoDescuento.Size = new System.Drawing.Size(381, 21);
            this.cbCorreoDescuento.TabIndex = 125;
            this.cbCorreoDescuento.Text = "Al hacer venta con descuento";
            this.cbCorreoDescuento.UseVisualStyleBackColor = true;
            this.cbCorreoDescuento.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbCorreoDescuento_MouseClick);
            // 
            // cbCorreoIniciar
            // 
            this.cbCorreoIniciar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCorreoIniciar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbCorreoIniciar.Location = new System.Drawing.Point(468, 113);
            this.cbCorreoIniciar.Name = "cbCorreoIniciar";
            this.cbCorreoIniciar.Size = new System.Drawing.Size(381, 21);
            this.cbCorreoIniciar.TabIndex = 124;
            this.cbCorreoIniciar.Text = "Al iniciar sesión";
            this.cbCorreoIniciar.UseVisualStyleBackColor = true;
            this.cbCorreoIniciar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbCorreoIniciar_MouseClick);
            // 
            // cbCorreoVenta
            // 
            this.cbCorreoVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCorreoVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbCorreoVenta.Location = new System.Drawing.Point(468, 88);
            this.cbCorreoVenta.Name = "cbCorreoVenta";
            this.cbCorreoVenta.Size = new System.Drawing.Size(381, 21);
            this.cbCorreoVenta.TabIndex = 123;
            this.cbCorreoVenta.Text = "Al hacer una venta";
            this.cbCorreoVenta.UseVisualStyleBackColor = true;
            this.cbCorreoVenta.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbCorreoVenta_MouseClick);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label4.Location = new System.Drawing.Point(231, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 13);
            this.label4.TabIndex = 122;
            this.label4.Text = " (producto previamente seleccionado)";
            // 
            // cbCorreoCorteCaja
            // 
            this.cbCorreoCorteCaja.AutoSize = true;
            this.cbCorreoCorteCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbCorreoCorteCaja.ForeColor = System.Drawing.SystemColors.Highlight;
            this.cbCorreoCorteCaja.Location = new System.Drawing.Point(11, 88);
            this.cbCorreoCorteCaja.Name = "cbCorreoCorteCaja";
            this.cbCorreoCorteCaja.Size = new System.Drawing.Size(157, 20);
            this.cbCorreoCorteCaja.TabIndex = 121;
            this.cbCorreoCorteCaja.Text = "Al hacer corte de caja";
            this.cbCorreoCorteCaja.UseVisualStyleBackColor = true;
            this.cbCorreoCorteCaja.Click += new System.EventHandler(this.cbCorreoCorteCaja_Click);
            this.cbCorreoCorteCaja.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbCorreoCorteCaja_MouseClick);
            // 
            // cbCorreoEliminarListaProductosVentas
            // 
            this.cbCorreoEliminarListaProductosVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCorreoEliminarListaProductosVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbCorreoEliminarListaProductosVentas.Location = new System.Drawing.Point(468, 61);
            this.cbCorreoEliminarListaProductosVentas.Name = "cbCorreoEliminarListaProductosVentas";
            this.cbCorreoEliminarListaProductosVentas.Size = new System.Drawing.Size(381, 21);
            this.cbCorreoEliminarListaProductosVentas.TabIndex = 120;
            this.cbCorreoEliminarListaProductosVentas.Text = "Al eliminar producto de ventas";
            this.cbCorreoEliminarListaProductosVentas.UseVisualStyleBackColor = true;
            this.cbCorreoEliminarListaProductosVentas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbCorreoEliminarListaProductosVentas_MouseClick);
            // 
            // cbCorreoCerrarVentanaVentas
            // 
            this.cbCorreoCerrarVentanaVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbCorreoCerrarVentanaVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbCorreoCerrarVentanaVentas.Location = new System.Drawing.Point(468, 34);
            this.cbCorreoCerrarVentanaVentas.Name = "cbCorreoCerrarVentanaVentas";
            this.cbCorreoCerrarVentanaVentas.Size = new System.Drawing.Size(395, 21);
            this.cbCorreoCerrarVentanaVentas.TabIndex = 116;
            this.cbCorreoCerrarVentanaVentas.Text = "Al cerrar la ventana de ventas cuando tiene productos";
            this.cbCorreoCerrarVentanaVentas.UseVisualStyleBackColor = true;
            this.cbCorreoCerrarVentanaVentas.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbCorreoCerrarVentanaVentas_MouseClick);
            // 
            // cbCorreoRetirarDineroCaja
            // 
            this.cbCorreoRetirarDineroCaja.AutoSize = true;
            this.cbCorreoRetirarDineroCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbCorreoRetirarDineroCaja.Location = new System.Drawing.Point(11, 61);
            this.cbCorreoRetirarDineroCaja.Name = "cbCorreoRetirarDineroCaja";
            this.cbCorreoRetirarDineroCaja.Size = new System.Drawing.Size(164, 20);
            this.cbCorreoRetirarDineroCaja.TabIndex = 115;
            this.cbCorreoRetirarDineroCaja.Text = "Al retirar dinero en caja";
            this.cbCorreoRetirarDineroCaja.UseVisualStyleBackColor = true;
            this.cbCorreoRetirarDineroCaja.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbCorreoRetirarDineroCaja_MouseClick);
            // 
            // cbCorreoAgregarDineroCaja
            // 
            this.cbCorreoAgregarDineroCaja.AutoSize = true;
            this.cbCorreoAgregarDineroCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.cbCorreoAgregarDineroCaja.Location = new System.Drawing.Point(11, 34);
            this.cbCorreoAgregarDineroCaja.Name = "cbCorreoAgregarDineroCaja";
            this.cbCorreoAgregarDineroCaja.Size = new System.Drawing.Size(178, 20);
            this.cbCorreoAgregarDineroCaja.TabIndex = 0;
            this.cbCorreoAgregarDineroCaja.Text = "Al agregar dinero en caja";
            this.cbCorreoAgregarDineroCaja.UseVisualStyleBackColor = true;
            this.cbCorreoAgregarDineroCaja.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbCorreoAgregarDineroCaja_MouseClick);
            // 
            // cbCorreoPrecioProducto
            // 
            this.cbCorreoPrecioProducto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbCorreoPrecioProducto.AutoSize = true;
            this.cbCorreoPrecioProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCorreoPrecioProducto.Location = new System.Drawing.Point(239, 61);
            this.cbCorreoPrecioProducto.Name = "cbCorreoPrecioProducto";
            this.cbCorreoPrecioProducto.Size = new System.Drawing.Size(138, 20);
            this.cbCorreoPrecioProducto.TabIndex = 111;
            this.cbCorreoPrecioProducto.Text = "Al modificar precio";
            this.cbCorreoPrecioProducto.UseVisualStyleBackColor = true;
            this.cbCorreoPrecioProducto.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbCorreoPrecioProducto_MouseClick);
            // 
            // cbCorreoStockProducto
            // 
            this.cbCorreoStockProducto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbCorreoStockProducto.AutoSize = true;
            this.cbCorreoStockProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCorreoStockProducto.Location = new System.Drawing.Point(239, 88);
            this.cbCorreoStockProducto.Name = "cbCorreoStockProducto";
            this.cbCorreoStockProducto.Size = new System.Drawing.Size(132, 20);
            this.cbCorreoStockProducto.TabIndex = 112;
            this.cbCorreoStockProducto.Text = "Al modificar stock";
            this.cbCorreoStockProducto.UseVisualStyleBackColor = true;
            this.cbCorreoStockProducto.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbCorreoStockProducto_MouseClick);
            // 
            // cbCorreoStockMinimo
            // 
            this.cbCorreoStockMinimo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbCorreoStockMinimo.AutoSize = true;
            this.cbCorreoStockMinimo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCorreoStockMinimo.Location = new System.Drawing.Point(239, 34);
            this.cbCorreoStockMinimo.Name = "cbCorreoStockMinimo";
            this.cbCorreoStockMinimo.Size = new System.Drawing.Size(157, 20);
            this.cbCorreoStockMinimo.TabIndex = 113;
            this.cbCorreoStockMinimo.Text = "Al llegar stock mínimo";
            this.cbCorreoStockMinimo.UseVisualStyleBackColor = true;
            this.cbCorreoStockMinimo.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbCorreoStockMinimo_MouseClick);
            // 
            // cbCorreoVenderProducto
            // 
            this.cbCorreoVenderProducto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.cbCorreoVenderProducto.AutoSize = true;
            this.cbCorreoVenderProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCorreoVenderProducto.Location = new System.Drawing.Point(239, 113);
            this.cbCorreoVenderProducto.Name = "cbCorreoVenderProducto";
            this.cbCorreoVenderProducto.Size = new System.Drawing.Size(155, 20);
            this.cbCorreoVenderProducto.TabIndex = 114;
            this.cbCorreoVenderProducto.Text = "Al venderse producto";
            this.cbCorreoVenderProducto.UseVisualStyleBackColor = true;
            this.cbCorreoVenderProducto.MouseClick += new System.Windows.Forms.MouseEventHandler(this.cbCorreoVenderProducto_MouseClick);
            // 
            // EnvioDeCorreo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(949, 308);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnvioDeCorreo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Envio De Correo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EnvioDeCorreo_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EnvioDeCorreo_FormClosed);
            this.Load += new System.EventHandler(this.EnvioDeCorreo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EnvioDeCorreo_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chRespaldo;
        private System.Windows.Forms.CheckBox cbCorreoDescuento;
        private System.Windows.Forms.CheckBox cbCorreoIniciar;
        private System.Windows.Forms.CheckBox cbCorreoVenta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbCorreoCorteCaja;
        private System.Windows.Forms.CheckBox cbCorreoEliminarListaProductosVentas;
        private System.Windows.Forms.CheckBox cbCorreoCerrarVentanaVentas;
        private System.Windows.Forms.CheckBox cbCorreoRetirarDineroCaja;
        private System.Windows.Forms.CheckBox cbCorreoAgregarDineroCaja;
        private System.Windows.Forms.CheckBox cbCorreoPrecioProducto;
        private System.Windows.Forms.CheckBox cbCorreoStockProducto;
        private System.Windows.Forms.CheckBox cbCorreoStockMinimo;
        private System.Windows.Forms.CheckBox cbCorreoVenderProducto;
        private System.Windows.Forms.Button btnAceptar;
    }
}