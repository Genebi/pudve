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
            this.txtFiltrar = new System.Windows.Forms.TextBox();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnAgregarUbicacion = new System.Windows.Forms.Button();
            this.btnAgregarCategoria = new System.Windows.Forms.Button();
            this.checkUbicacion = new System.Windows.Forms.CheckBox();
            this.checkCategoria = new System.Windows.Forms.CheckBox();
            this.btnAgregarProveedor = new System.Windows.Forms.Button();
            this.checkProveedor = new System.Windows.Forms.CheckBox();
            this.btnGuardarDetalles = new System.Windows.Forms.Button();
            this.panelContenido = new System.Windows.Forms.FlowLayoutPanel();
            this.panelProveedor = new System.Windows.Forms.Panel();
            this.lblTelProveedor = new System.Windows.Forms.Label();
            this.lblRFCProveedor = new System.Windows.Forms.Label();
            this.lblNombreProveedor = new System.Windows.Forms.Label();
            this.lbProveedor = new System.Windows.Forms.Label();
            this.cbProveedores = new System.Windows.Forms.ComboBox();
            this.panelGrupoA = new System.Windows.Forms.FlowLayoutPanel();
            this.panelCategoria = new System.Windows.Forms.Panel();
            this.lbNombreCategoria = new System.Windows.Forms.Label();
            this.lbCategoria = new System.Windows.Forms.Label();
            this.cbCategorias = new System.Windows.Forms.ComboBox();
            this.panelUbicacion = new System.Windows.Forms.Panel();
            this.lbNombreUbicacion = new System.Windows.Forms.Label();
            this.cbUbicaciones = new System.Windows.Forms.ComboBox();
            this.lbUbicacion = new System.Windows.Forms.Label();
            this.panelMenu.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.panelProveedor.SuspendLayout();
            this.panelGrupoA.SuspendLayout();
            this.panelCategoria.SuspendLayout();
            this.panelUbicacion.SuspendLayout();
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
            // txtFiltrar
            // 
            this.txtFiltrar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltrar.Location = new System.Drawing.Point(11, 18);
            this.txtFiltrar.Name = "txtFiltrar";
            this.txtFiltrar.Size = new System.Drawing.Size(168, 22);
            this.txtFiltrar.TabIndex = 20;
            this.txtFiltrar.Text = "Filtrar...";
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
            this.btnAgregarUbicacion.Location = new System.Drawing.Point(159, 66);
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
            this.btnAgregarCategoria.Location = new System.Drawing.Point(159, 36);
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
            this.btnAgregarProveedor.Location = new System.Drawing.Point(159, 6);
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
            // panelContenido
            // 
            this.panelContenido.Controls.Add(this.panelProveedor);
            this.panelContenido.Controls.Add(this.panelGrupoA);
            this.panelContenido.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelContenido.Location = new System.Drawing.Point(198, 55);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(645, 544);
            this.panelContenido.TabIndex = 27;
            // 
            // panelProveedor
            // 
            this.panelProveedor.Controls.Add(this.lblTelProveedor);
            this.panelProveedor.Controls.Add(this.lblRFCProveedor);
            this.panelProveedor.Controls.Add(this.lblNombreProveedor);
            this.panelProveedor.Controls.Add(this.lbProveedor);
            this.panelProveedor.Controls.Add(this.cbProveedores);
            this.panelProveedor.Location = new System.Drawing.Point(3, 3);
            this.panelProveedor.Name = "panelProveedor";
            this.panelProveedor.Size = new System.Drawing.Size(639, 73);
            this.panelProveedor.TabIndex = 30;
            // 
            // lblTelProveedor
            // 
            this.lblTelProveedor.BackColor = System.Drawing.Color.White;
            this.lblTelProveedor.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTelProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblTelProveedor.Location = new System.Drawing.Point(499, 46);
            this.lblTelProveedor.Name = "lblTelProveedor";
            this.lblTelProveedor.Size = new System.Drawing.Size(130, 20);
            this.lblTelProveedor.TabIndex = 95;
            this.lblTelProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTelProveedor.Visible = false;
            // 
            // lblRFCProveedor
            // 
            this.lblRFCProveedor.BackColor = System.Drawing.Color.White;
            this.lblRFCProveedor.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFCProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblRFCProveedor.Location = new System.Drawing.Point(333, 46);
            this.lblRFCProveedor.Name = "lblRFCProveedor";
            this.lblRFCProveedor.Size = new System.Drawing.Size(145, 20);
            this.lblRFCProveedor.TabIndex = 92;
            this.lblRFCProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRFCProveedor.Visible = false;
            // 
            // lblNombreProveedor
            // 
            this.lblNombreProveedor.BackColor = System.Drawing.Color.White;
            this.lblNombreProveedor.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblNombreProveedor.Location = new System.Drawing.Point(10, 46);
            this.lblNombreProveedor.Name = "lblNombreProveedor";
            this.lblNombreProveedor.Size = new System.Drawing.Size(300, 20);
            this.lblNombreProveedor.TabIndex = 87;
            this.lblNombreProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblNombreProveedor.Visible = false;
            // 
            // lbProveedor
            // 
            this.lbProveedor.AutoSize = true;
            this.lbProveedor.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProveedor.Location = new System.Drawing.Point(7, 13);
            this.lbProveedor.Name = "lbProveedor";
            this.lbProveedor.Size = new System.Drawing.Size(71, 17);
            this.lbProveedor.TabIndex = 31;
            this.lbProveedor.Text = "Proveedor";
            // 
            // cbProveedores
            // 
            this.cbProveedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProveedores.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProveedores.FormattingEnabled = true;
            this.cbProveedores.Location = new System.Drawing.Point(79, 11);
            this.cbProveedores.Name = "cbProveedores";
            this.cbProveedores.Size = new System.Drawing.Size(552, 24);
            this.cbProveedores.TabIndex = 32;
            this.cbProveedores.SelectedIndexChanged += new System.EventHandler(this.cbProveedores_SelectedIndexChanged);
            // 
            // panelGrupoA
            // 
            this.panelGrupoA.Controls.Add(this.panelCategoria);
            this.panelGrupoA.Controls.Add(this.panelUbicacion);
            this.panelGrupoA.Location = new System.Drawing.Point(3, 82);
            this.panelGrupoA.Name = "panelGrupoA";
            this.panelGrupoA.Size = new System.Drawing.Size(642, 87);
            this.panelGrupoA.TabIndex = 37;
            // 
            // panelCategoria
            // 
            this.panelCategoria.Controls.Add(this.lbNombreCategoria);
            this.panelCategoria.Controls.Add(this.lbCategoria);
            this.panelCategoria.Controls.Add(this.cbCategorias);
            this.panelCategoria.Location = new System.Drawing.Point(3, 3);
            this.panelCategoria.Name = "panelCategoria";
            this.panelCategoria.Size = new System.Drawing.Size(315, 74);
            this.panelCategoria.TabIndex = 35;
            // 
            // lbNombreCategoria
            // 
            this.lbNombreCategoria.BackColor = System.Drawing.Color.White;
            this.lbNombreCategoria.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNombreCategoria.ForeColor = System.Drawing.Color.Blue;
            this.lbNombreCategoria.Location = new System.Drawing.Point(7, 44);
            this.lbNombreCategoria.Name = "lbNombreCategoria";
            this.lbNombreCategoria.Size = new System.Drawing.Size(300, 20);
            this.lbNombreCategoria.TabIndex = 96;
            this.lbNombreCategoria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbNombreCategoria.Visible = false;
            // 
            // lbCategoria
            // 
            this.lbCategoria.AutoSize = true;
            this.lbCategoria.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCategoria.Location = new System.Drawing.Point(4, 12);
            this.lbCategoria.Name = "lbCategoria";
            this.lbCategoria.Size = new System.Drawing.Size(69, 17);
            this.lbCategoria.TabIndex = 33;
            this.lbCategoria.Text = "Categoría";
            // 
            // cbCategorias
            // 
            this.cbCategorias.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategorias.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCategorias.FormattingEnabled = true;
            this.cbCategorias.Location = new System.Drawing.Point(76, 10);
            this.cbCategorias.Name = "cbCategorias";
            this.cbCategorias.Size = new System.Drawing.Size(231, 24);
            this.cbCategorias.TabIndex = 34;
            this.cbCategorias.SelectedIndexChanged += new System.EventHandler(this.cbCategorias_SelectedIndexChanged);
            // 
            // panelUbicacion
            // 
            this.panelUbicacion.Controls.Add(this.lbNombreUbicacion);
            this.panelUbicacion.Controls.Add(this.cbUbicaciones);
            this.panelUbicacion.Controls.Add(this.lbUbicacion);
            this.panelUbicacion.Location = new System.Drawing.Point(324, 3);
            this.panelUbicacion.Name = "panelUbicacion";
            this.panelUbicacion.Size = new System.Drawing.Size(315, 74);
            this.panelUbicacion.TabIndex = 36;
            // 
            // lbNombreUbicacion
            // 
            this.lbNombreUbicacion.BackColor = System.Drawing.Color.White;
            this.lbNombreUbicacion.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNombreUbicacion.ForeColor = System.Drawing.Color.Blue;
            this.lbNombreUbicacion.Location = new System.Drawing.Point(6, 44);
            this.lbNombreUbicacion.Name = "lbNombreUbicacion";
            this.lbNombreUbicacion.Size = new System.Drawing.Size(301, 20);
            this.lbNombreUbicacion.TabIndex = 97;
            this.lbNombreUbicacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbNombreUbicacion.Visible = false;
            // 
            // cbUbicaciones
            // 
            this.cbUbicaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUbicaciones.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUbicaciones.FormattingEnabled = true;
            this.cbUbicaciones.Location = new System.Drawing.Point(76, 10);
            this.cbUbicaciones.Name = "cbUbicaciones";
            this.cbUbicaciones.Size = new System.Drawing.Size(231, 24);
            this.cbUbicaciones.TabIndex = 38;
            this.cbUbicaciones.SelectedIndexChanged += new System.EventHandler(this.cbUbicaciones_SelectedIndexChanged);
            // 
            // lbUbicacion
            // 
            this.lbUbicacion.AutoSize = true;
            this.lbUbicacion.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUbicacion.Location = new System.Drawing.Point(3, 12);
            this.lbUbicacion.Name = "lbUbicacion";
            this.lbUbicacion.Size = new System.Drawing.Size(69, 17);
            this.lbUbicacion.TabIndex = 37;
            this.lbUbicacion.Text = "Ubicación";
            // 
            // AgregarDetalleProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 666);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.btnGuardarDetalles);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.txtFiltrar);
            this.Controls.Add(this.separadorInicial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "AgregarDetalleProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Detalles Producto";
            this.Load += new System.EventHandler(this.AgregarDetalleProducto_Load);
            this.Shown += new System.EventHandler(this.AgregarDetalleProducto_Shown);
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            this.panelProveedor.ResumeLayout(false);
            this.panelProveedor.PerformLayout();
            this.panelGrupoA.ResumeLayout(false);
            this.panelCategoria.ResumeLayout(false);
            this.panelCategoria.PerformLayout();
            this.panelUbicacion.ResumeLayout(false);
            this.panelUbicacion.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label separadorInicial;
        private System.Windows.Forms.TextBox txtFiltrar;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnGuardarDetalles;
        private System.Windows.Forms.Button btnAgregarProveedor;
        private System.Windows.Forms.CheckBox checkProveedor;
        private System.Windows.Forms.Button btnAgregarUbicacion;
        private System.Windows.Forms.Button btnAgregarCategoria;
        private System.Windows.Forms.CheckBox checkUbicacion;
        private System.Windows.Forms.CheckBox checkCategoria;
        private System.Windows.Forms.FlowLayoutPanel panelContenido;
        private System.Windows.Forms.Panel panelProveedor;
        private System.Windows.Forms.Label lblTelProveedor;
        private System.Windows.Forms.Label lblRFCProveedor;
        private System.Windows.Forms.Label lblNombreProveedor;
        private System.Windows.Forms.Label lbProveedor;
        private System.Windows.Forms.ComboBox cbProveedores;
        private System.Windows.Forms.FlowLayoutPanel panelGrupoA;
        private System.Windows.Forms.Panel panelCategoria;
        private System.Windows.Forms.Label lbNombreCategoria;
        private System.Windows.Forms.Label lbCategoria;
        private System.Windows.Forms.ComboBox cbCategorias;
        private System.Windows.Forms.Panel panelUbicacion;
        private System.Windows.Forms.Label lbNombreUbicacion;
        private System.Windows.Forms.ComboBox cbUbicaciones;
        private System.Windows.Forms.Label lbUbicacion;
    }
}