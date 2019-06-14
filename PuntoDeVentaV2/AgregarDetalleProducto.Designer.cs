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
            this.listaOpciones = new System.Windows.Forms.CheckedListBox();
            this.separadorInicial = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.lbProveedor = new System.Windows.Forms.Label();
            this.cbProveedores = new System.Windows.Forms.ComboBox();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.btnGuardarDetalles = new System.Windows.Forms.Button();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // listaOpciones
            // 
            this.listaOpciones.BackColor = System.Drawing.SystemColors.Control;
            this.listaOpciones.CheckOnClick = true;
            this.listaOpciones.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaOpciones.FormattingEnabled = true;
            this.listaOpciones.Items.AddRange(new object[] {
            "Proveedor"});
            this.listaOpciones.Location = new System.Drawing.Point(5, 3);
            this.listaOpciones.Name = "listaOpciones";
            this.listaOpciones.Size = new System.Drawing.Size(148, 599);
            this.listaOpciones.TabIndex = 0;
            this.listaOpciones.SelectedIndexChanged += new System.EventHandler(this.listaOpciones_SelectedIndexChanged);
            // 
            // separadorInicial
            // 
            this.separadorInicial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separadorInicial.Location = new System.Drawing.Point(11, 48);
            this.separadorInicial.Name = "separadorInicial";
            this.separadorInicial.Size = new System.Drawing.Size(825, 2);
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
            this.panelMenu.Controls.Add(this.listaOpciones);
            this.panelMenu.Location = new System.Drawing.Point(6, 55);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(186, 609);
            this.panelMenu.TabIndex = 21;
            // 
            // lbProveedor
            // 
            this.lbProveedor.AutoSize = true;
            this.lbProveedor.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProveedor.Location = new System.Drawing.Point(203, 60);
            this.lbProveedor.Name = "lbProveedor";
            this.lbProveedor.Size = new System.Drawing.Size(71, 17);
            this.lbProveedor.TabIndex = 23;
            this.lbProveedor.Text = "Proveedor";
            this.lbProveedor.Visible = false;
            // 
            // cbProveedores
            // 
            this.cbProveedores.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProveedores.FormattingEnabled = true;
            this.cbProveedores.Items.AddRange(new object[] {
            "Seleccionar un proveedor..."});
            this.cbProveedores.Location = new System.Drawing.Point(280, 59);
            this.cbProveedores.Name = "cbProveedores";
            this.cbProveedores.Size = new System.Drawing.Size(552, 21);
            this.cbProveedores.TabIndex = 24;
            this.cbProveedores.Visible = false;
            // 
            // panelContenido
            // 
            this.panelContenido.Location = new System.Drawing.Point(198, 55);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(638, 546);
            this.panelContenido.TabIndex = 25;
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
            // AgregarDetalleProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 666);
            this.Controls.Add(this.btnGuardarDetalles);
            this.Controls.Add(this.cbProveedores);
            this.Controls.Add(this.lbProveedor);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox listaOpciones;
        private System.Windows.Forms.Label separadorInicial;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Label lbProveedor;
        private System.Windows.Forms.ComboBox cbProveedores;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.Button btnGuardarDetalles;
    }
}