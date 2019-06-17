namespace PuntoDeVentaV2
{
    partial class AjustarProducto
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
            this.lbProducto = new System.Windows.Forms.Label();
            this.rbProducto = new System.Windows.Forms.RadioButton();
            this.rbAjustar = new System.Windows.Forms.RadioButton();
            this.panelComprado = new System.Windows.Forms.Panel();
            this.panelAjustar = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lbComentarios = new System.Windows.Forms.Label();
            this.txtPrecioCompra = new System.Windows.Forms.TextBox();
            this.txtCantidadCompra = new System.Windows.Forms.TextBox();
            this.dpFechaCompra = new System.Windows.Forms.DateTimePicker();
            this.cbProveedor = new System.Windows.Forms.ComboBox();
            this.lbProveedor = new System.Windows.Forms.Label();
            this.lbFechaCompra = new System.Windows.Forms.Label();
            this.lbPrecioCompra = new System.Windows.Forms.Label();
            this.lbCantidadCompra = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.lbAumentar = new System.Windows.Forms.Label();
            this.lbDisminuir = new System.Windows.Forms.Label();
            this.panelComprado.SuspendLayout();
            this.panelAjustar.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbProducto
            // 
            this.lbProducto.AutoSize = true;
            this.lbProducto.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProducto.Location = new System.Drawing.Point(233, 30);
            this.lbProducto.Name = "lbProducto";
            this.lbProducto.Size = new System.Drawing.Size(167, 20);
            this.lbProducto.TabIndex = 0;
            this.lbProducto.Text = "Nombre del Producto";
            // 
            // rbProducto
            // 
            this.rbProducto.AutoSize = true;
            this.rbProducto.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbProducto.Location = new System.Drawing.Point(137, 62);
            this.rbProducto.Name = "rbProducto";
            this.rbProducto.Size = new System.Drawing.Size(147, 21);
            this.rbProducto.TabIndex = 1;
            this.rbProducto.TabStop = true;
            this.rbProducto.Text = "Producto comprado";
            this.rbProducto.UseVisualStyleBackColor = true;
            // 
            // rbAjustar
            // 
            this.rbAjustar.AutoSize = true;
            this.rbAjustar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAjustar.Location = new System.Drawing.Point(387, 62);
            this.rbAjustar.Name = "rbAjustar";
            this.rbAjustar.Size = new System.Drawing.Size(95, 21);
            this.rbAjustar.TabIndex = 2;
            this.rbAjustar.TabStop = true;
            this.rbAjustar.Text = "Solo ajustar";
            this.rbAjustar.UseVisualStyleBackColor = true;
            // 
            // panelComprado
            // 
            this.panelComprado.Controls.Add(this.lbCantidadCompra);
            this.panelComprado.Controls.Add(this.lbPrecioCompra);
            this.panelComprado.Controls.Add(this.lbFechaCompra);
            this.panelComprado.Controls.Add(this.lbProveedor);
            this.panelComprado.Controls.Add(this.cbProveedor);
            this.panelComprado.Controls.Add(this.dpFechaCompra);
            this.panelComprado.Controls.Add(this.txtCantidadCompra);
            this.panelComprado.Controls.Add(this.txtPrecioCompra);
            this.panelComprado.Location = new System.Drawing.Point(12, 99);
            this.panelComprado.Name = "panelComprado";
            this.panelComprado.Size = new System.Drawing.Size(610, 100);
            this.panelComprado.TabIndex = 3;
            // 
            // panelAjustar
            // 
            this.panelAjustar.Controls.Add(this.lbDisminuir);
            this.panelAjustar.Controls.Add(this.lbAumentar);
            this.panelAjustar.Controls.Add(this.textBox3);
            this.panelAjustar.Controls.Add(this.textBox2);
            this.panelAjustar.Location = new System.Drawing.Point(12, 217);
            this.panelAjustar.Name = "panelAjustar";
            this.panelAjustar.Size = new System.Drawing.Size(610, 100);
            this.panelAjustar.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 354);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(609, 20);
            this.textBox1.TabIndex = 5;
            // 
            // lbComentarios
            // 
            this.lbComentarios.AutoSize = true;
            this.lbComentarios.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbComentarios.Location = new System.Drawing.Point(13, 335);
            this.lbComentarios.Name = "lbComentarios";
            this.lbComentarios.Size = new System.Drawing.Size(149, 17);
            this.lbComentarios.TabIndex = 6;
            this.lbComentarios.Text = "Comentarios (opcional)";
            // 
            // txtPrecioCompra
            // 
            this.txtPrecioCompra.Location = new System.Drawing.Point(358, 40);
            this.txtPrecioCompra.Name = "txtPrecioCompra";
            this.txtPrecioCompra.Size = new System.Drawing.Size(112, 20);
            this.txtPrecioCompra.TabIndex = 2;
            // 
            // txtCantidadCompra
            // 
            this.txtCantidadCompra.Location = new System.Drawing.Point(484, 40);
            this.txtCantidadCompra.Name = "txtCantidadCompra";
            this.txtCantidadCompra.Size = new System.Drawing.Size(122, 20);
            this.txtCantidadCompra.TabIndex = 3;
            // 
            // dpFechaCompra
            // 
            this.dpFechaCompra.CustomFormat = "yyyy-MM-dd";
            this.dpFechaCompra.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpFechaCompra.Location = new System.Drawing.Point(233, 41);
            this.dpFechaCompra.Name = "dpFechaCompra";
            this.dpFechaCompra.Size = new System.Drawing.Size(111, 20);
            this.dpFechaCompra.TabIndex = 4;
            // 
            // cbProveedor
            // 
            this.cbProveedor.FormattingEnabled = true;
            this.cbProveedor.Location = new System.Drawing.Point(4, 40);
            this.cbProveedor.Name = "cbProveedor";
            this.cbProveedor.Size = new System.Drawing.Size(215, 21);
            this.cbProveedor.TabIndex = 5;
            // 
            // lbProveedor
            // 
            this.lbProveedor.AutoSize = true;
            this.lbProveedor.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProveedor.Location = new System.Drawing.Point(3, 21);
            this.lbProveedor.Name = "lbProveedor";
            this.lbProveedor.Size = new System.Drawing.Size(71, 17);
            this.lbProveedor.TabIndex = 6;
            this.lbProveedor.Text = "Proveedor";
            // 
            // lbFechaCompra
            // 
            this.lbFechaCompra.AutoSize = true;
            this.lbFechaCompra.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFechaCompra.Location = new System.Drawing.Point(230, 21);
            this.lbFechaCompra.Name = "lbFechaCompra";
            this.lbFechaCompra.Size = new System.Drawing.Size(114, 17);
            this.lbFechaCompra.TabIndex = 7;
            this.lbFechaCompra.Text = "Fecha de compra";
            // 
            // lbPrecioCompra
            // 
            this.lbPrecioCompra.AutoSize = true;
            this.lbPrecioCompra.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrecioCompra.Location = new System.Drawing.Point(355, 21);
            this.lbPrecioCompra.Name = "lbPrecioCompra";
            this.lbPrecioCompra.Size = new System.Drawing.Size(115, 17);
            this.lbPrecioCompra.TabIndex = 8;
            this.lbPrecioCompra.Text = "Precio de compra";
            // 
            // lbCantidadCompra
            // 
            this.lbCantidadCompra.AutoSize = true;
            this.lbCantidadCompra.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCantidadCompra.Location = new System.Drawing.Point(481, 21);
            this.lbCantidadCompra.Name = "lbCantidadCompra";
            this.lbCantidadCompra.Size = new System.Drawing.Size(114, 17);
            this.lbCantidadCompra.TabIndex = 9;
            this.lbCantidadCompra.Text = "Cantidad compra";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(172, 40);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 0;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(349, 40);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 1;
            // 
            // lbAumentar
            // 
            this.lbAumentar.AutoSize = true;
            this.lbAumentar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAumentar.Location = new System.Drawing.Point(172, 21);
            this.lbAumentar.Name = "lbAumentar";
            this.lbAumentar.Size = new System.Drawing.Size(67, 17);
            this.lbAumentar.TabIndex = 2;
            this.lbAumentar.Text = "Aumentar";
            // 
            // lbDisminuir
            // 
            this.lbDisminuir.AutoSize = true;
            this.lbDisminuir.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDisminuir.Location = new System.Drawing.Point(346, 21);
            this.lbDisminuir.Name = "lbDisminuir";
            this.lbDisminuir.Size = new System.Drawing.Size(60, 17);
            this.lbDisminuir.TabIndex = 3;
            this.lbDisminuir.Text = "Disminuir";
            // 
            // AjustarProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 411);
            this.Controls.Add(this.lbComentarios);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.panelAjustar);
            this.Controls.Add(this.panelComprado);
            this.Controls.Add(this.rbAjustar);
            this.Controls.Add(this.rbProducto);
            this.Controls.Add(this.lbProducto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "AjustarProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Ajustar";
            this.panelComprado.ResumeLayout(false);
            this.panelComprado.PerformLayout();
            this.panelAjustar.ResumeLayout(false);
            this.panelAjustar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbProducto;
        private System.Windows.Forms.RadioButton rbProducto;
        private System.Windows.Forms.RadioButton rbAjustar;
        private System.Windows.Forms.Panel panelComprado;
        private System.Windows.Forms.Panel panelAjustar;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lbComentarios;
        private System.Windows.Forms.Label lbCantidadCompra;
        private System.Windows.Forms.Label lbPrecioCompra;
        private System.Windows.Forms.Label lbFechaCompra;
        private System.Windows.Forms.Label lbProveedor;
        private System.Windows.Forms.ComboBox cbProveedor;
        private System.Windows.Forms.DateTimePicker dpFechaCompra;
        private System.Windows.Forms.TextBox txtCantidadCompra;
        private System.Windows.Forms.TextBox txtPrecioCompra;
        private System.Windows.Forms.Label lbDisminuir;
        private System.Windows.Forms.Label lbAumentar;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
    }
}