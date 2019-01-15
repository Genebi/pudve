namespace PuntoDeVentaV2
{
    partial class AgregarEditarProducto
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtNombreProducto = new System.Windows.Forms.TextBox();
            this.txtPrecioProducto = new System.Windows.Forms.TextBox();
            this.txtStockProducto = new System.Windows.Forms.TextBox();
            this.txtClaveProducto = new System.Windows.Forms.TextBox();
            this.txtCategoriaProducto = new System.Windows.Forms.TextBox();
            this.txtCodigoBarras = new System.Windows.Forms.TextBox();
            this.panelContenedor = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGenerarCB = new System.Windows.Forms.Button();
            this.btnAgregarDescuento = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnDetalleFacturacion = new System.Windows.Forms.Button();
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.btnImagenes = new System.Windows.Forms.Button();
            this.btnGuardarProducto = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(68, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Producto";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(312, 179);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Código de Barras";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(551, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Categoría";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(311, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Precio";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(67, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Clave Interna";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(68, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 17);
            this.label6.TabIndex = 6;
            this.label6.Text = "Stock";
            // 
            // txtNombreProducto
            // 
            this.txtNombreProducto.Location = new System.Drawing.Point(70, 92);
            this.txtNombreProducto.Name = "txtNombreProducto";
            this.txtNombreProducto.Size = new System.Drawing.Size(650, 20);
            this.txtNombreProducto.TabIndex = 7;
            // 
            // txtPrecioProducto
            // 
            this.txtPrecioProducto.Location = new System.Drawing.Point(315, 145);
            this.txtPrecioProducto.Name = "txtPrecioProducto";
            this.txtPrecioProducto.Size = new System.Drawing.Size(165, 20);
            this.txtPrecioProducto.TabIndex = 8;
            this.txtPrecioProducto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPrecioProducto_KeyUp);
            // 
            // txtStockProducto
            // 
            this.txtStockProducto.Location = new System.Drawing.Point(70, 145);
            this.txtStockProducto.Name = "txtStockProducto";
            this.txtStockProducto.Size = new System.Drawing.Size(165, 20);
            this.txtStockProducto.TabIndex = 9;
            // 
            // txtClaveProducto
            // 
            this.txtClaveProducto.Location = new System.Drawing.Point(70, 199);
            this.txtClaveProducto.Name = "txtClaveProducto";
            this.txtClaveProducto.Size = new System.Drawing.Size(165, 20);
            this.txtClaveProducto.TabIndex = 10;
            // 
            // txtCategoriaProducto
            // 
            this.txtCategoriaProducto.Location = new System.Drawing.Point(555, 145);
            this.txtCategoriaProducto.Name = "txtCategoriaProducto";
            this.txtCategoriaProducto.Size = new System.Drawing.Size(165, 20);
            this.txtCategoriaProducto.TabIndex = 11;
            // 
            // txtCodigoBarras
            // 
            this.txtCodigoBarras.Location = new System.Drawing.Point(315, 199);
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.Size = new System.Drawing.Size(165, 20);
            this.txtCodigoBarras.TabIndex = 12;
            this.txtCodigoBarras.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoBarras_KeyDown);
            // 
            // panelContenedor
            // 
            this.panelContenedor.AutoScroll = true;
            this.panelContenedor.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelContenedor.Location = new System.Drawing.Point(309, 225);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(411, 95);
            this.panelContenedor.TabIndex = 13;
            this.panelContenedor.WrapContents = false;
            // 
            // btnGenerarCB
            // 
            this.btnGenerarCB.BackColor = System.Drawing.SystemColors.HotTrack;
            this.btnGenerarCB.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarCB.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Highlight;
            this.btnGenerarCB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarCB.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarCB.ForeColor = System.Drawing.Color.White;
            this.btnGenerarCB.Location = new System.Drawing.Point(555, 195);
            this.btnGenerarCB.Name = "btnGenerarCB";
            this.btnGenerarCB.Size = new System.Drawing.Size(165, 27);
            this.btnGenerarCB.TabIndex = 14;
            this.btnGenerarCB.Text = "Generar Código";
            this.btnGenerarCB.UseVisualStyleBackColor = false;
            this.btnGenerarCB.Click += new System.EventHandler(this.btnGenerarCB_Click);
            // 
            // btnAgregarDescuento
            // 
            this.btnAgregarDescuento.BackColor = System.Drawing.Color.Green;
            this.btnAgregarDescuento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarDescuento.FlatAppearance.BorderSize = 0;
            this.btnAgregarDescuento.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarDescuento.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarDescuento.ForeColor = System.Drawing.Color.White;
            this.btnAgregarDescuento.Location = new System.Drawing.Point(70, 365);
            this.btnAgregarDescuento.Name = "btnAgregarDescuento";
            this.btnAgregarDescuento.Size = new System.Drawing.Size(180, 28);
            this.btnAgregarDescuento.TabIndex = 15;
            this.btnAgregarDescuento.Text = "Agregar Descuento";
            this.btnAgregarDescuento.UseVisualStyleBackColor = false;
            this.btnAgregarDescuento.Click += new System.EventHandler(this.btnAgregarDescuento_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(309, 365);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(180, 28);
            this.button2.TabIndex = 16;
            this.button2.Text = "Detalle de producto";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btnDetalleFacturacion
            // 
            this.btnDetalleFacturacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDetalleFacturacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDetalleFacturacion.FlatAppearance.BorderSize = 0;
            this.btnDetalleFacturacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetalleFacturacion.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetalleFacturacion.ForeColor = System.Drawing.Color.White;
            this.btnDetalleFacturacion.Location = new System.Drawing.Point(540, 365);
            this.btnDetalleFacturacion.Name = "btnDetalleFacturacion";
            this.btnDetalleFacturacion.Size = new System.Drawing.Size(180, 28);
            this.btnDetalleFacturacion.TabIndex = 17;
            this.btnDetalleFacturacion.Text = "Detalle de facturación";
            this.btnDetalleFacturacion.UseVisualStyleBackColor = false;
            this.btnDetalleFacturacion.Click += new System.EventHandler(this.btnDetalleFacturacion_Click);
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(300, 33);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(235, 25);
            this.tituloSeccion.TabIndex = 19;
            this.tituloSeccion.Text = "AGREGAR PRODUCTO";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnImagenes
            // 
            this.btnImagenes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImagenes.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImagenes.Location = new System.Drawing.Point(71, 446);
            this.btnImagenes.Name = "btnImagenes";
            this.btnImagenes.Size = new System.Drawing.Size(180, 28);
            this.btnImagenes.TabIndex = 21;
            this.btnImagenes.Text = "Seleccionar imagen(es)";
            this.btnImagenes.UseVisualStyleBackColor = true;
            this.btnImagenes.Click += new System.EventHandler(this.btnImagenes_Click);
            // 
            // btnGuardarProducto
            // 
            this.btnGuardarProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGuardarProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarProducto.FlatAppearance.BorderSize = 0;
            this.btnGuardarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarProducto.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnGuardarProducto.Location = new System.Drawing.Point(540, 446);
            this.btnGuardarProducto.Name = "btnGuardarProducto";
            this.btnGuardarProducto.Size = new System.Drawing.Size(180, 28);
            this.btnGuardarProducto.TabIndex = 22;
            this.btnGuardarProducto.Text = "Guardar Producto";
            this.btnGuardarProducto.UseVisualStyleBackColor = false;
            this.btnGuardarProducto.Click += new System.EventHandler(this.btnGuardarProducto_Click);
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(15, 334);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(750, 2);
            this.label8.TabIndex = 23;
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Location = new System.Drawing.Point(16, 420);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(750, 2);
            this.label9.TabIndex = 24;
            // 
            // AgregarEditarProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 497);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnGuardarProducto);
            this.Controls.Add(this.btnImagenes);
            this.Controls.Add(this.tituloSeccion);
            this.Controls.Add(this.btnDetalleFacturacion);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnAgregarDescuento);
            this.Controls.Add(this.btnGenerarCB);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.txtCodigoBarras);
            this.Controls.Add(this.txtCategoriaProducto);
            this.Controls.Add(this.txtClaveProducto);
            this.Controls.Add(this.txtStockProducto);
            this.Controls.Add(this.txtPrecioProducto);
            this.Controls.Add(this.txtNombreProducto);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 540);
            this.Name = "AgregarEditarProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Agregar producto";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtNombreProducto;
        private System.Windows.Forms.TextBox txtPrecioProducto;
        private System.Windows.Forms.TextBox txtStockProducto;
        private System.Windows.Forms.TextBox txtClaveProducto;
        private System.Windows.Forms.TextBox txtCategoriaProducto;
        private System.Windows.Forms.TextBox txtCodigoBarras;
        private System.Windows.Forms.FlowLayoutPanel panelContenedor;
        private System.Windows.Forms.Button btnGenerarCB;
        private System.Windows.Forms.Button btnAgregarDescuento;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnDetalleFacturacion;
        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.Button btnImagenes;
        private System.Windows.Forms.Button btnGuardarProducto;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}