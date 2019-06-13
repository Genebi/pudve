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
            this.contenedorMenu = new System.Windows.Forms.Panel();
            this.lbProveedor = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.contenedorMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // listaOpciones
            // 
            this.listaOpciones.BackColor = System.Drawing.SystemColors.Control;
            this.listaOpciones.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaOpciones.FormattingEnabled = true;
            this.listaOpciones.Items.AddRange(new object[] {
            "Proveedor",
            "Opcion 1",
            "Opcion 2"});
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
            // contenedorMenu
            // 
            this.contenedorMenu.Controls.Add(this.listaOpciones);
            this.contenedorMenu.Location = new System.Drawing.Point(6, 55);
            this.contenedorMenu.Name = "contenedorMenu";
            this.contenedorMenu.Size = new System.Drawing.Size(186, 609);
            this.contenedorMenu.TabIndex = 21;
            // 
            // lbProveedor
            // 
            this.lbProveedor.AutoSize = true;
            this.lbProveedor.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProveedor.Location = new System.Drawing.Point(203, 58);
            this.lbProveedor.Name = "lbProveedor";
            this.lbProveedor.Size = new System.Drawing.Size(71, 17);
            this.lbProveedor.TabIndex = 23;
            this.lbProveedor.Text = "Proveedor";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(280, 57);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(518, 21);
            this.comboBox1.TabIndex = 24;
            // 
            // AgregarDetalleProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 666);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lbProveedor);
            this.Controls.Add(this.contenedorMenu);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.separadorInicial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "AgregarDetalleProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Detalles Producto";
            this.contenedorMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox listaOpciones;
        private System.Windows.Forms.Label separadorInicial;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Panel contenedorMenu;
        private System.Windows.Forms.Label lbProveedor;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}