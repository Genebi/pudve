namespace PuntoDeVentaV2
{
    partial class AgregarDetalleProducto
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
            this.separadorInicial = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnAgregarUbicacion = new System.Windows.Forms.Button();
            this.btnAgregarCategoria = new System.Windows.Forms.Button();
            this.checkUbicacion = new System.Windows.Forms.CheckBox();
            this.checkCategoria = new System.Windows.Forms.CheckBox();
            this.btnAgregarProveedor = new System.Windows.Forms.Button();
            this.checkProveedor = new System.Windows.Forms.CheckBox();
            this.lbProveedor = new System.Windows.Forms.Label();
            this.cbProveedores = new System.Windows.Forms.ComboBox();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.panelDatosProveedor = new System.Windows.Forms.Panel();
            this.lblTelProveedor = new System.Windows.Forms.Label();
            this.lblRFCProveedor = new System.Windows.Forms.Label();
            this.lblNombreProveedor = new System.Windows.Forms.Label();
            this.btnGuardarDetalles = new System.Windows.Forms.Button();
            this.lbCategoria = new System.Windows.Forms.Label();
            this.cbCategorias = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbUbicacion = new System.Windows.Forms.Label();
            this.cbUbicaciones = new System.Windows.Forms.ComboBox();
            this.panelUbicacion = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.panelDatosProveedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // separadorInicial
            // 
            this.separadorInicial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separadorInicial.Location = new System.Drawing.Point(5, 48);
            this.separadorInicial.Name = "separadorInicial";
            this.separadorInicial.Size = new System.Drawing.Size(830, 2);
            this.separadorInicial.TabIndex = 19;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(11, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(148, 22);
            this.textBox1.TabIndex = 20;
            this.textBox1.Text = "Filtrar...";
            // 
            // panelMenu
            // 
            this.panelMenu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelMenu.Controls.Add(this.btnAgregarUbicacion);
            this.panelMenu.Controls.Add(this.btnAgregarCategoria);
            this.panelMenu.Controls.Add(this.checkUbicacion);
            this.panelMenu.Controls.Add(this.checkCategoria);
            this.panelMenu.Controls.Add(this.btnAgregarProveedor);
            this.panelMenu.Controls.Add(this.checkProveedor);
            this.panelMenu.Location = new System.Drawing.Point(6, 55);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(186, 609);
            this.panelMenu.TabIndex = 21;
            // 
            // btnAgregarUbicacion
            // 
            this.btnAgregarUbicacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarUbicacion.Image = global::PuntoDeVentaV2.Properties.Resources.plus_square;
            this.btnAgregarUbicacion.Location = new System.Drawing.Point(154, 66);
            this.btnAgregarUbicacion.Name = "btnAgregarUbicacion";
            this.btnAgregarUbicacion.Size = new System.Drawing.Size(20, 20);
            this.btnAgregarUbicacion.TabIndex = 5;
            this.btnAgregarUbicacion.UseVisualStyleBackColor = true;
            this.btnAgregarUbicacion.Click += new System.EventHandler(this.btnAgregarUbicacion_Click);
            // 
            // btnAgregarCategoria
            // 
            this.btnAgregarCategoria.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarCategoria.Image = global::PuntoDeVentaV2.Properties.Resources.plus_square;
            this.btnAgregarCategoria.Location = new System.Drawing.Point(154, 36);
            this.btnAgregarCategoria.Name = "btnAgregarCategoria";
            this.btnAgregarCategoria.Size = new System.Drawing.Size(20, 20);
            this.btnAgregarCategoria.TabIndex = 4;
            this.btnAgregarCategoria.UseVisualStyleBackColor = true;
            this.btnAgregarCategoria.Click += new System.EventHandler(this.btnAgregarCategoria_Click);
            // 
            // checkUbicacion
            // 
            this.checkUbicacion.AutoSize = true;
            this.checkUbicacion.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkUbicacion.Location = new System.Drawing.Point(6, 66);
            this.checkUbicacion.Name = "checkUbicacion";
            this.checkUbicacion.Size = new System.Drawing.Size(88, 21);
            this.checkUbicacion.TabIndex = 3;
            this.checkUbicacion.Text = "Ubicación";
            this.checkUbicacion.UseVisualStyleBackColor = true;
            this.checkUbicacion.CheckedChanged += new System.EventHandler(this.checkUbicacion_CheckedChanged);
            // 
            // checkCategoria
            // 
            this.checkCategoria.AutoSize = true;
            this.checkCategoria.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkCategoria.Location = new System.Drawing.Point(6, 36);
            this.checkCategoria.Name = "checkCategoria";
            this.checkCategoria.Size = new System.Drawing.Size(88, 21);
            this.checkCategoria.TabIndex = 2;
            this.checkCategoria.Text = "Categoría";
            this.checkCategoria.UseVisualStyleBackColor = true;
            this.checkCategoria.CheckedChanged += new System.EventHandler(this.checkCategoria_CheckedChanged);
            // 
            // btnAgregarProveedor
            // 
            this.btnAgregarProveedor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarProveedor.Image = global::PuntoDeVentaV2.Properties.Resources.plus_square;
            this.btnAgregarProveedor.Location = new System.Drawing.Point(154, 6);
            this.btnAgregarProveedor.Name = "btnAgregarProveedor";
            this.btnAgregarProveedor.Size = new System.Drawing.Size(20, 20);
            this.btnAgregarProveedor.TabIndex = 1;
            this.btnAgregarProveedor.UseVisualStyleBackColor = true;
            this.btnAgregarProveedor.Click += new System.EventHandler(this.btnAgregarProveedor_Click);
            // 
            // checkProveedor
            // 
            this.checkProveedor.AutoSize = true;
            this.checkProveedor.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkProveedor.Location = new System.Drawing.Point(6, 6);
            this.checkProveedor.Name = "checkProveedor";
            this.checkProveedor.Size = new System.Drawing.Size(90, 21);
            this.checkProveedor.TabIndex = 0;
            this.checkProveedor.Text = "Proveedor";
            this.checkProveedor.UseVisualStyleBackColor = true;
            this.checkProveedor.CheckedChanged += new System.EventHandler(this.checkProveedor_CheckedChanged);
            // 
            // lbProveedor
            // 
            this.lbProveedor.AutoSize = true;
            this.lbProveedor.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProveedor.Location = new System.Drawing.Point(5, 8);
            this.lbProveedor.Name = "lbProveedor";
            this.lbProveedor.Size = new System.Drawing.Size(71, 17);
            this.lbProveedor.TabIndex = 23;
            this.lbProveedor.Text = "Proveedor";
            this.lbProveedor.Visible = false;
            // 
            // cbProveedores
            // 
            this.cbProveedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProveedores.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProveedores.FormattingEnabled = true;
            this.cbProveedores.Location = new System.Drawing.Point(82, 6);
            this.cbProveedores.Name = "cbProveedores";
            this.cbProveedores.Size = new System.Drawing.Size(552, 24);
            this.cbProveedores.TabIndex = 24;
            this.cbProveedores.Visible = false;
            // 
            // panelContenido
            // 
            this.panelContenido.Controls.Add(this.panelUbicacion);
            this.panelContenido.Controls.Add(this.cbUbicaciones);
            this.panelContenido.Controls.Add(this.lbUbicacion);
            this.panelContenido.Controls.Add(this.panel1);
            this.panelContenido.Controls.Add(this.cbCategorias);
            this.panelContenido.Controls.Add(this.lbCategoria);
            this.panelContenido.Controls.Add(this.panelDatosProveedor);
            this.panelContenido.Controls.Add(this.cbProveedores);
            this.panelContenido.Controls.Add(this.lbProveedor);
            this.panelContenido.Location = new System.Drawing.Point(198, 55);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(638, 546);
            this.panelContenido.TabIndex = 25;
            // 
            // panelDatosProveedor
            // 
            this.panelDatosProveedor.Controls.Add(this.lblTelProveedor);
            this.panelDatosProveedor.Controls.Add(this.lblRFCProveedor);
            this.panelDatosProveedor.Controls.Add(this.lblNombreProveedor);
            this.panelDatosProveedor.Location = new System.Drawing.Point(8, 31);
            this.panelDatosProveedor.Name = "panelDatosProveedor";
            this.panelDatosProveedor.Size = new System.Drawing.Size(626, 40);
            this.panelDatosProveedor.TabIndex = 0;
            this.panelDatosProveedor.Visible = false;
            // 
            // lblTelProveedor
            // 
            this.lblTelProveedor.BackColor = System.Drawing.Color.White;
            this.lblTelProveedor.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblTelProveedor.Location = new System.Drawing.Point(473, 9);
            this.lblTelProveedor.Name = "lblTelProveedor";
            this.lblTelProveedor.Size = new System.Drawing.Size(150, 20);
            this.lblTelProveedor.TabIndex = 95;
            this.lblTelProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRFCProveedor
            // 
            this.lblRFCProveedor.BackColor = System.Drawing.Color.White;
            this.lblRFCProveedor.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFCProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblRFCProveedor.Location = new System.Drawing.Point(306, 9);
            this.lblRFCProveedor.Name = "lblRFCProveedor";
            this.lblRFCProveedor.Size = new System.Drawing.Size(150, 20);
            this.lblRFCProveedor.TabIndex = 92;
            this.lblRFCProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNombreProveedor
            // 
            this.lblNombreProveedor.BackColor = System.Drawing.Color.White;
            this.lblNombreProveedor.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblNombreProveedor.Location = new System.Drawing.Point(3, 9);
            this.lblNombreProveedor.Name = "lblNombreProveedor";
            this.lblNombreProveedor.Size = new System.Drawing.Size(285, 20);
            this.lblNombreProveedor.TabIndex = 87;
            this.lblNombreProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnGuardarDetalles
            // 
            this.btnGuardarDetalles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGuardarDetalles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarDetalles.FlatAppearance.BorderSize = 0;
            this.btnGuardarDetalles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarDetalles.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarDetalles.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnGuardarDetalles.Location = new System.Drawing.Point(656, 621);
            this.btnGuardarDetalles.Name = "btnGuardarDetalles";
            this.btnGuardarDetalles.Size = new System.Drawing.Size(180, 28);
            this.btnGuardarDetalles.TabIndex = 26;
            this.btnGuardarDetalles.Text = "Guardar";
            this.btnGuardarDetalles.UseVisualStyleBackColor = false;
            this.btnGuardarDetalles.Click += new System.EventHandler(this.btnGuardarDetalles_Click);
            // 
            // lbCategoria
            // 
            this.lbCategoria.AutoSize = true;
            this.lbCategoria.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCategoria.Location = new System.Drawing.Point(5, 89);
            this.lbCategoria.Name = "lbCategoria";
            this.lbCategoria.Size = new System.Drawing.Size(69, 17);
            this.lbCategoria.TabIndex = 25;
            this.lbCategoria.Text = "Categoría";
            // 
            // cbCategorias
            // 
            this.cbCategorias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategorias.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCategorias.FormattingEnabled = true;
            this.cbCategorias.Location = new System.Drawing.Point(82, 87);
            this.cbCategorias.Name = "cbCategorias";
            this.cbCategorias.Size = new System.Drawing.Size(236, 24);
            this.cbCategorias.TabIndex = 26;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(8, 117);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(310, 40);
            this.panel1.TabIndex = 27;
            // 
            // lbUbicacion
            // 
            this.lbUbicacion.AutoSize = true;
            this.lbUbicacion.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUbicacion.Location = new System.Drawing.Point(324, 89);
            this.lbUbicacion.Name = "lbUbicacion";
            this.lbUbicacion.Size = new System.Drawing.Size(69, 17);
            this.lbUbicacion.TabIndex = 28;
            this.lbUbicacion.Text = "Ubicación";
            // 
            // cbUbicaciones
            // 
            this.cbUbicaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUbicaciones.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUbicaciones.FormattingEnabled = true;
            this.cbUbicaciones.Location = new System.Drawing.Point(398, 87);
            this.cbUbicaciones.Name = "cbUbicaciones";
            this.cbUbicaciones.Size = new System.Drawing.Size(236, 24);
            this.cbUbicaciones.TabIndex = 29;
            // 
            // panelUbicacion
            // 
            this.panelUbicacion.Location = new System.Drawing.Point(328, 117);
            this.panelUbicacion.Name = "panelUbicacion";
            this.panelUbicacion.Size = new System.Drawing.Size(306, 40);
            this.panelUbicacion.TabIndex = 28;
            // 
            // AgregarDetalleProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 666);
            this.Controls.Add(this.btnGuardarDetalles);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.separadorInicial);
            this.Controls.Add(this.panelContenido);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "AgregarDetalleProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Detalles Producto";
            this.Load += new System.EventHandler(this.AgregarDetalleProducto_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            this.panelContenido.PerformLayout();
            this.panelDatosProveedor.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label separadorInicial;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Label lbProveedor;
        private System.Windows.Forms.ComboBox cbProveedores;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.Button btnGuardarDetalles;
        private System.Windows.Forms.Panel panelDatosProveedor;
        private System.Windows.Forms.Label lblNombreProveedor;
        private System.Windows.Forms.Label lblTelProveedor;
        private System.Windows.Forms.Label lblRFCProveedor;
        private System.Windows.Forms.Button btnAgregarProveedor;
        private System.Windows.Forms.CheckBox checkProveedor;
        private System.Windows.Forms.Button btnAgregarUbicacion;
        private System.Windows.Forms.Button btnAgregarCategoria;
        private System.Windows.Forms.CheckBox checkUbicacion;
        private System.Windows.Forms.CheckBox checkCategoria;
        private System.Windows.Forms.Panel panelUbicacion;
        private System.Windows.Forms.ComboBox cbUbicaciones;
        private System.Windows.Forms.Label lbUbicacion;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbCategorias;
        private System.Windows.Forms.Label lbCategoria;
    }
}