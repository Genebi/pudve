namespace PuntoDeVentaV2
{
    partial class NvoProduct
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
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.lblTipoProdPaq = new System.Windows.Forms.Label();
            this.txtNombreProducto = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxProducto = new System.Windows.Forms.PictureBox();
            this.btnImagenes = new System.Windows.Forms.Button();
            this.PCodigoBarras = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panelContenedor = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGenerarCB = new System.Windows.Forms.Button();
            this.txtCodigoBarras = new System.Windows.Forms.TextBox();
            this.PClaveInterna = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtClaveProducto = new System.Windows.Forms.TextBox();
            this.PCategoria = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCategoriaProducto = new System.Windows.Forms.TextBox();
            this.PStock = new System.Windows.Forms.Panel();
            this.txtStockProducto = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.PPrecio = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPrecioVentaProducto = new System.Windows.Forms.TextBox();
            this.btnGuardarProducto = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrecioCompra = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProducto)).BeginInit();
            this.PCodigoBarras.SuspendLayout();
            this.PClaveInterna.SuspendLayout();
            this.PCategoria.SuspendLayout();
            this.PStock.SuspendLayout();
            this.PPrecio.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(352, 13);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(235, 25);
            this.tituloSeccion.TabIndex = 22;
            this.tituloSeccion.Text = "AGREGAR PRODUCTO";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblTipoProdPaq
            // 
            this.lblTipoProdPaq.AutoSize = true;
            this.lblTipoProdPaq.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTipoProdPaq.Location = new System.Drawing.Point(44, 48);
            this.lblTipoProdPaq.Name = "lblTipoProdPaq";
            this.lblTipoProdPaq.Size = new System.Drawing.Size(68, 17);
            this.lblTipoProdPaq.TabIndex = 23;
            this.lblTipoProdPaq.Text = "Producto";
            // 
            // txtNombreProducto
            // 
            this.txtNombreProducto.Location = new System.Drawing.Point(116, 68);
            this.txtNombreProducto.Name = "txtNombreProducto";
            this.txtNombreProducto.Size = new System.Drawing.Size(740, 20);
            this.txtNombreProducto.TabIndex = 24;
            this.txtNombreProducto.Enter += new System.EventHandler(this.txtNombreProducto_Enter);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.PCodigoBarras);
            this.panel1.Controls.Add(this.PClaveInterna);
            this.panel1.Controls.Add(this.PCategoria);
            this.panel1.Controls.Add(this.PStock);
            this.panel1.Controls.Add(this.PPrecio);
            this.panel1.Controls.Add(this.tituloSeccion);
            this.panel1.Controls.Add(this.lblTipoProdPaq);
            this.panel1.Controls.Add(this.txtNombreProducto);
            this.panel1.Location = new System.Drawing.Point(2, 2);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(926, 535);
            this.panel1.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxProducto);
            this.groupBox1.Controls.Add(this.btnImagenes);
            this.groupBox1.Location = new System.Drawing.Point(647, 211);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(209, 221);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Imagen";
            // 
            // pictureBoxProducto
            // 
            this.pictureBoxProducto.Location = new System.Drawing.Point(38, 21);
            this.pictureBoxProducto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBoxProducto.Name = "pictureBoxProducto";
            this.pictureBoxProducto.Size = new System.Drawing.Size(132, 132);
            this.pictureBoxProducto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxProducto.TabIndex = 0;
            this.pictureBoxProducto.TabStop = false;
            // 
            // btnImagenes
            // 
            this.btnImagenes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImagenes.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImagenes.Location = new System.Drawing.Point(15, 176);
            this.btnImagenes.Name = "btnImagenes";
            this.btnImagenes.Size = new System.Drawing.Size(179, 28);
            this.btnImagenes.TabIndex = 9;
            this.btnImagenes.Text = "Seleccionar imagen(es)";
            this.btnImagenes.UseVisualStyleBackColor = true;
            this.btnImagenes.Click += new System.EventHandler(this.btnImagenes_Click);
            // 
            // PCodigoBarras
            // 
            this.PCodigoBarras.Controls.Add(this.label2);
            this.PCodigoBarras.Controls.Add(this.panelContenedor);
            this.PCodigoBarras.Controls.Add(this.btnGenerarCB);
            this.PCodigoBarras.Controls.Add(this.txtCodigoBarras);
            this.PCodigoBarras.Location = new System.Drawing.Point(264, 209);
            this.PCodigoBarras.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PCodigoBarras.Name = "PCodigoBarras";
            this.PCodigoBarras.Size = new System.Drawing.Size(302, 221);
            this.PCodigoBarras.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(145, 17);
            this.label2.TabIndex = 10;
            this.label2.Text = "Código de Barras";
            // 
            // panelContenedor
            // 
            this.panelContenedor.AutoScroll = true;
            this.panelContenedor.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelContenedor.Location = new System.Drawing.Point(8, 64);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(230, 149);
            this.panelContenedor.TabIndex = 20;
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
            this.btnGenerarCB.Image = global::PuntoDeVentaV2.Properties.Resources.barcode1;
            this.btnGenerarCB.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGenerarCB.Location = new System.Drawing.Point(184, 18);
            this.btnGenerarCB.Name = "btnGenerarCB";
            this.btnGenerarCB.Size = new System.Drawing.Size(112, 23);
            this.btnGenerarCB.TabIndex = 8;
            this.btnGenerarCB.Text = "Generar";
            this.btnGenerarCB.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnGenerarCB.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGenerarCB.UseVisualStyleBackColor = false;
            this.btnGenerarCB.Click += new System.EventHandler(this.btnGenerarCB_Click);
            // 
            // txtCodigoBarras
            // 
            this.txtCodigoBarras.Location = new System.Drawing.Point(14, 22);
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.Size = new System.Drawing.Size(165, 20);
            this.txtCodigoBarras.TabIndex = 6;
            this.txtCodigoBarras.Enter += new System.EventHandler(this.txtCodigoBarras_Enter);
            this.txtCodigoBarras.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigoBarras_KeyDown);
            // 
            // PClaveInterna
            // 
            this.PClaveInterna.Controls.Add(this.label5);
            this.PClaveInterna.Controls.Add(this.txtClaveProducto);
            this.PClaveInterna.Location = new System.Drawing.Point(46, 209);
            this.PClaveInterna.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PClaveInterna.Name = "PClaveInterna";
            this.PClaveInterna.Size = new System.Drawing.Size(120, 49);
            this.PClaveInterna.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 1);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Clave Interna";
            // 
            // txtClaveProducto
            // 
            this.txtClaveProducto.Location = new System.Drawing.Point(3, 21);
            this.txtClaveProducto.Name = "txtClaveProducto";
            this.txtClaveProducto.Size = new System.Drawing.Size(107, 20);
            this.txtClaveProducto.TabIndex = 5;
            this.txtClaveProducto.Enter += new System.EventHandler(this.txtClaveProducto_Enter);
            // 
            // PCategoria
            // 
            this.PCategoria.Controls.Add(this.label3);
            this.PCategoria.Controls.Add(this.txtCategoriaProducto);
            this.PCategoria.Location = new System.Drawing.Point(738, 130);
            this.PCategoria.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PCategoria.Name = "PCategoria";
            this.PCategoria.Size = new System.Drawing.Size(118, 50);
            this.PCategoria.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Categoría";
            // 
            // txtCategoriaProducto
            // 
            this.txtCategoriaProducto.Location = new System.Drawing.Point(3, 24);
            this.txtCategoriaProducto.Name = "txtCategoriaProducto";
            this.txtCategoriaProducto.Size = new System.Drawing.Size(107, 20);
            this.txtCategoriaProducto.TabIndex = 4;
            this.txtCategoriaProducto.TextChanged += new System.EventHandler(this.txtCategoriaProducto_TextChanged);
            this.txtCategoriaProducto.Enter += new System.EventHandler(this.txtCategoriaProducto_Enter);
            // 
            // PStock
            // 
            this.PStock.Controls.Add(this.txtStockProducto);
            this.PStock.Controls.Add(this.label6);
            this.PStock.Location = new System.Drawing.Point(46, 130);
            this.PStock.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PStock.Name = "PStock";
            this.PStock.Size = new System.Drawing.Size(120, 50);
            this.PStock.TabIndex = 0;
            // 
            // txtStockProducto
            // 
            this.txtStockProducto.Location = new System.Drawing.Point(4, 24);
            this.txtStockProducto.Name = "txtStockProducto";
            this.txtStockProducto.Size = new System.Drawing.Size(107, 20);
            this.txtStockProducto.TabIndex = 0;
            this.txtStockProducto.Enter += new System.EventHandler(this.txtStockProducto_Enter);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(2, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(43, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "Stock";
            // 
            // PPrecio
            // 
            this.PPrecio.Controls.Add(this.label4);
            this.PPrecio.Controls.Add(this.txtPrecioVentaProducto);
            this.PPrecio.Location = new System.Drawing.Point(506, 130);
            this.PPrecio.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PPrecio.Name = "PPrecio";
            this.PPrecio.Size = new System.Drawing.Size(118, 50);
            this.PPrecio.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Precio Venta";
            // 
            // txtPrecioVentaProducto
            // 
            this.txtPrecioVentaProducto.Location = new System.Drawing.Point(3, 24);
            this.txtPrecioVentaProducto.Name = "txtPrecioVentaProducto";
            this.txtPrecioVentaProducto.Size = new System.Drawing.Size(107, 20);
            this.txtPrecioVentaProducto.TabIndex = 3;
            this.txtPrecioVentaProducto.Enter += new System.EventHandler(this.txtPrecioProducto_Enter);
            // 
            // btnGuardarProducto
            // 
            this.btnGuardarProducto.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGuardarProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGuardarProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarProducto.FlatAppearance.BorderSize = 0;
            this.btnGuardarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarProducto.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnGuardarProducto.Location = new System.Drawing.Point(686, 552);
            this.btnGuardarProducto.Name = "btnGuardarProducto";
            this.btnGuardarProducto.Size = new System.Drawing.Size(172, 28);
            this.btnGuardarProducto.TabIndex = 26;
            this.btnGuardarProducto.Text = "Guardar Producto";
            this.btnGuardarProducto.UseVisualStyleBackColor = false;
            this.btnGuardarProducto.Click += new System.EventHandler(this.btnGuardarProducto_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.txtPrecioCompra);
            this.panel2.Location = new System.Drawing.Point(264, 130);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(126, 50);
            this.panel2.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Precio Compra";
            // 
            // txtPrecioCompra
            // 
            this.txtPrecioCompra.Location = new System.Drawing.Point(3, 24);
            this.txtPrecioCompra.Name = "txtPrecioCompra";
            this.txtPrecioCompra.Size = new System.Drawing.Size(107, 20);
            this.txtPrecioCompra.TabIndex = 1;
            // 
            // NvoProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 593);
            this.Controls.Add(this.btnGuardarProducto);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NvoProduct";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nuevo Producto";
            this.Load += new System.EventHandler(this.NvoProduct_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProducto)).EndInit();
            this.PCodigoBarras.ResumeLayout(false);
            this.PCodigoBarras.PerformLayout();
            this.PClaveInterna.ResumeLayout(false);
            this.PClaveInterna.PerformLayout();
            this.PCategoria.ResumeLayout(false);
            this.PCategoria.PerformLayout();
            this.PStock.ResumeLayout(false);
            this.PStock.PerformLayout();
            this.PPrecio.ResumeLayout(false);
            this.PPrecio.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.Label lblTipoProdPaq;
        public System.Windows.Forms.TextBox txtNombreProducto;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel PStock;
        public System.Windows.Forms.TextBox txtStockProducto;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel PPrecio;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox txtPrecioVentaProducto;
        private System.Windows.Forms.Panel PCategoria;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox txtCategoriaProducto;
        private System.Windows.Forms.Panel PClaveInterna;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.TextBox txtClaveProducto;
        private System.Windows.Forms.Panel PCodigoBarras;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel panelContenedor;
        private System.Windows.Forms.Button btnGenerarCB;
        public System.Windows.Forms.TextBox txtCodigoBarras;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBoxProducto;
        private System.Windows.Forms.Button btnImagenes;
        private System.Windows.Forms.Button btnGuardarProducto;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtPrecioCompra;
    }
}