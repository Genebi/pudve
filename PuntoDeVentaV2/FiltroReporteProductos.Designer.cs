namespace PuntoDeVentaV2
{
    partial class FiltroReporteProductos
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
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.primerSeparador = new System.Windows.Forms.Label();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.cbProveedor = new System.Windows.Forms.ComboBox();
            this.checkProveedor = new System.Windows.Forms.CheckBox();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.cbPrecio = new System.Windows.Forms.ComboBox();
            this.cbStock = new System.Windows.Forms.ComboBox();
            this.checkPrecio = new System.Windows.Forms.CheckBox();
            this.checkStock = new System.Windows.Forms.CheckBox();
            this.txtStockMinimo = new System.Windows.Forms.TextBox();
            this.cbStockMinimo = new System.Windows.Forms.ComboBox();
            this.checkStockMinimo = new System.Windows.Forms.CheckBox();
            this.txtStockMaximo = new System.Windows.Forms.TextBox();
            this.cbStockMaximo = new System.Windows.Forms.ComboBox();
            this.checkStockMaximo = new System.Windows.Forms.CheckBox();
            this.panelContenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(120, 315);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(105, 27);
            this.btnCancelar.TabIndex = 20;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.Green;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(248, 314);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(105, 28);
            this.btnAceptar.TabIndex = 19;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // primerSeparador
            // 
            this.primerSeparador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.primerSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.primerSeparador.Location = new System.Drawing.Point(18, 298);
            this.primerSeparador.Name = "primerSeparador";
            this.primerSeparador.Size = new System.Drawing.Size(450, 2);
            this.primerSeparador.TabIndex = 23;
            this.primerSeparador.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelContenedor
            // 
            this.panelContenedor.Controls.Add(this.txtStockMaximo);
            this.panelContenedor.Controls.Add(this.cbStockMaximo);
            this.panelContenedor.Controls.Add(this.checkStockMaximo);
            this.panelContenedor.Controls.Add(this.txtStockMinimo);
            this.panelContenedor.Controls.Add(this.cbStockMinimo);
            this.panelContenedor.Controls.Add(this.checkStockMinimo);
            this.panelContenedor.Controls.Add(this.cbProveedor);
            this.panelContenedor.Controls.Add(this.checkProveedor);
            this.panelContenedor.Controls.Add(this.txtPrecio);
            this.panelContenedor.Controls.Add(this.txtStock);
            this.panelContenedor.Controls.Add(this.cbPrecio);
            this.panelContenedor.Controls.Add(this.cbStock);
            this.panelContenedor.Controls.Add(this.checkPrecio);
            this.panelContenedor.Controls.Add(this.checkStock);
            this.panelContenedor.Location = new System.Drawing.Point(2, 1);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(481, 294);
            this.panelContenedor.TabIndex = 24;
            // 
            // cbProveedor
            // 
            this.cbProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProveedor.Enabled = false;
            this.cbProveedor.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProveedor.FormattingEnabled = true;
            this.cbProveedor.Location = new System.Drawing.Point(122, 180);
            this.cbProveedor.Name = "cbProveedor";
            this.cbProveedor.Size = new System.Drawing.Size(345, 24);
            this.cbProveedor.TabIndex = 33;
            // 
            // checkProveedor
            // 
            this.checkProveedor.AutoSize = true;
            this.checkProveedor.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkProveedor.Location = new System.Drawing.Point(10, 180);
            this.checkProveedor.Name = "checkProveedor";
            this.checkProveedor.Size = new System.Drawing.Size(90, 21);
            this.checkProveedor.TabIndex = 32;
            this.checkProveedor.Text = "Proveedor";
            this.checkProveedor.UseVisualStyleBackColor = true;
            this.checkProveedor.CheckedChanged += new System.EventHandler(this.checkProveedor_CheckedChanged);
            // 
            // txtPrecio
            // 
            this.txtPrecio.Enabled = false;
            this.txtPrecio.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrecio.Location = new System.Drawing.Point(387, 140);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(80, 21);
            this.txtPrecio.TabIndex = 31;
            this.txtPrecio.Text = "0";
            this.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtStock
            // 
            this.txtStock.Enabled = false;
            this.txtStock.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStock.Location = new System.Drawing.Point(387, 20);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(80, 21);
            this.txtStock.TabIndex = 30;
            this.txtStock.Text = "0";
            this.txtStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbPrecio
            // 
            this.cbPrecio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrecio.Enabled = false;
            this.cbPrecio.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPrecio.FormattingEnabled = true;
            this.cbPrecio.Location = new System.Drawing.Point(122, 140);
            this.cbPrecio.Name = "cbPrecio";
            this.cbPrecio.Size = new System.Drawing.Size(246, 24);
            this.cbPrecio.TabIndex = 29;
            // 
            // cbStock
            // 
            this.cbStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStock.Enabled = false;
            this.cbStock.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStock.FormattingEnabled = true;
            this.cbStock.Location = new System.Drawing.Point(122, 20);
            this.cbStock.Name = "cbStock";
            this.cbStock.Size = new System.Drawing.Size(246, 24);
            this.cbStock.TabIndex = 28;
            // 
            // checkPrecio
            // 
            this.checkPrecio.AutoSize = true;
            this.checkPrecio.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkPrecio.Location = new System.Drawing.Point(10, 140);
            this.checkPrecio.Name = "checkPrecio";
            this.checkPrecio.Size = new System.Drawing.Size(65, 21);
            this.checkPrecio.TabIndex = 27;
            this.checkPrecio.Text = "Precio";
            this.checkPrecio.UseVisualStyleBackColor = true;
            this.checkPrecio.CheckedChanged += new System.EventHandler(this.checkPrecio_CheckedChanged);
            // 
            // checkStock
            // 
            this.checkStock.AutoSize = true;
            this.checkStock.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkStock.Location = new System.Drawing.Point(10, 20);
            this.checkStock.Name = "checkStock";
            this.checkStock.Size = new System.Drawing.Size(61, 21);
            this.checkStock.TabIndex = 26;
            this.checkStock.Text = "Stock";
            this.checkStock.UseVisualStyleBackColor = true;
            this.checkStock.CheckedChanged += new System.EventHandler(this.checkStock_CheckedChanged);
            // 
            // txtStockMinimo
            // 
            this.txtStockMinimo.Enabled = false;
            this.txtStockMinimo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStockMinimo.Location = new System.Drawing.Point(387, 60);
            this.txtStockMinimo.Name = "txtStockMinimo";
            this.txtStockMinimo.Size = new System.Drawing.Size(80, 21);
            this.txtStockMinimo.TabIndex = 36;
            this.txtStockMinimo.Text = "0";
            this.txtStockMinimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbStockMinimo
            // 
            this.cbStockMinimo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStockMinimo.Enabled = false;
            this.cbStockMinimo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStockMinimo.FormattingEnabled = true;
            this.cbStockMinimo.Location = new System.Drawing.Point(122, 60);
            this.cbStockMinimo.Name = "cbStockMinimo";
            this.cbStockMinimo.Size = new System.Drawing.Size(246, 24);
            this.cbStockMinimo.TabIndex = 35;
            // 
            // checkStockMinimo
            // 
            this.checkStockMinimo.AutoSize = true;
            this.checkStockMinimo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkStockMinimo.Location = new System.Drawing.Point(10, 60);
            this.checkStockMinimo.Name = "checkStockMinimo";
            this.checkStockMinimo.Size = new System.Drawing.Size(107, 21);
            this.checkStockMinimo.TabIndex = 34;
            this.checkStockMinimo.Text = "Stock Mínimo";
            this.checkStockMinimo.UseVisualStyleBackColor = true;
            this.checkStockMinimo.CheckedChanged += new System.EventHandler(this.checkStockMinimo_CheckedChanged);
            // 
            // txtStockMaximo
            // 
            this.txtStockMaximo.Enabled = false;
            this.txtStockMaximo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStockMaximo.Location = new System.Drawing.Point(387, 100);
            this.txtStockMaximo.Name = "txtStockMaximo";
            this.txtStockMaximo.Size = new System.Drawing.Size(80, 21);
            this.txtStockMaximo.TabIndex = 39;
            this.txtStockMaximo.Text = "0";
            this.txtStockMaximo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cbStockMaximo
            // 
            this.cbStockMaximo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStockMaximo.Enabled = false;
            this.cbStockMaximo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStockMaximo.FormattingEnabled = true;
            this.cbStockMaximo.Location = new System.Drawing.Point(122, 100);
            this.cbStockMaximo.Name = "cbStockMaximo";
            this.cbStockMaximo.Size = new System.Drawing.Size(246, 24);
            this.cbStockMaximo.TabIndex = 38;
            // 
            // checkStockMaximo
            // 
            this.checkStockMaximo.AutoSize = true;
            this.checkStockMaximo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkStockMaximo.Location = new System.Drawing.Point(10, 100);
            this.checkStockMaximo.Name = "checkStockMaximo";
            this.checkStockMaximo.Size = new System.Drawing.Size(111, 21);
            this.checkStockMaximo.TabIndex = 37;
            this.checkStockMaximo.Text = "Stock Máximo";
            this.checkStockMaximo.UseVisualStyleBackColor = true;
            this.checkStockMaximo.CheckedChanged += new System.EventHandler(this.checkStockMaximo_CheckedChanged);
            // 
            // FiltroReporteProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 361);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.primerSeparador);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FiltroReporteProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Filtro del Reporte";
            this.Load += new System.EventHandler(this.FiltroReporteProductos_Load);
            this.panelContenedor.ResumeLayout(false);
            this.panelContenedor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label primerSeparador;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.ComboBox cbProveedor;
        private System.Windows.Forms.CheckBox checkProveedor;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.ComboBox cbPrecio;
        private System.Windows.Forms.ComboBox cbStock;
        private System.Windows.Forms.CheckBox checkPrecio;
        private System.Windows.Forms.CheckBox checkStock;
        private System.Windows.Forms.TextBox txtStockMaximo;
        private System.Windows.Forms.ComboBox cbStockMaximo;
        private System.Windows.Forms.CheckBox checkStockMaximo;
        private System.Windows.Forms.TextBox txtStockMinimo;
        private System.Windows.Forms.ComboBox cbStockMinimo;
        private System.Windows.Forms.CheckBox checkStockMinimo;
    }
}