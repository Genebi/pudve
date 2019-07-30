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
            this.label1 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panelDatos = new System.Windows.Forms.Panel();
            this.lblNombreProveedor = new System.Windows.Forms.Label();
            this.lblCalleProveedor = new System.Windows.Forms.Label();
            this.lblColoniaProveedor = new System.Windows.Forms.Label();
            this.lblEstadoProveedor = new System.Windows.Forms.Label();
            this.lblEmailProveedor = new System.Windows.Forms.Label();
            this.lblRFCProveedor = new System.Windows.Forms.Label();
            this.lblMunicipioProveedor = new System.Windows.Forms.Label();
            this.lblCPProveedor = new System.Windows.Forms.Label();
            this.lblTelProveedor = new System.Windows.Forms.Label();
            this.lblNoExtProveedor = new System.Windows.Forms.Label();
            this.lblNoInterProveedor = new System.Windows.Forms.Label();
            this.panelMenu.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.panelDatos.SuspendLayout();
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
            this.cbProveedores.Location = new System.Drawing.Point(280, 59);
            this.cbProveedores.Name = "cbProveedores";
            this.cbProveedores.Size = new System.Drawing.Size(552, 21);
            this.cbProveedores.TabIndex = 24;
            this.cbProveedores.Visible = false;
            // 
            // panelContenido
            // 
            this.panelContenido.Controls.Add(this.panelDatos);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Datos:";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(30, 51);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(61, 17);
            this.label22.TabIndex = 76;
            this.label22.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(398, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 17);
            this.label2.TabIndex = 77;
            this.label2.Text = "RFC";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(30, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 78;
            this.label3.Text = "Calle";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(398, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 79;
            this.label4.Text = "No. Exterior";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(496, 108);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 17);
            this.label6.TabIndex = 80;
            this.label6.Text = "No. Interior";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(30, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 17);
            this.label7.TabIndex = 81;
            this.label7.Text = "Colonia";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(398, 162);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 17);
            this.label8.TabIndex = 82;
            this.label8.Text = "Municipio";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(30, 214);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 17);
            this.label9.TabIndex = 83;
            this.label9.Text = "Estado";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(398, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 17);
            this.label5.TabIndex = 84;
            this.label5.Text = "Código Postal";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(30, 264);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(130, 17);
            this.label11.TabIndex = 85;
            this.label11.Text = "Correo electrónico";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(398, 264);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 17);
            this.label10.TabIndex = 86;
            this.label10.Text = "Teléfono";
            // 
            // panelDatos
            // 
            this.panelDatos.Controls.Add(this.lblNoInterProveedor);
            this.panelDatos.Controls.Add(this.lblNoExtProveedor);
            this.panelDatos.Controls.Add(this.lblTelProveedor);
            this.panelDatos.Controls.Add(this.lblCPProveedor);
            this.panelDatos.Controls.Add(this.lblMunicipioProveedor);
            this.panelDatos.Controls.Add(this.lblRFCProveedor);
            this.panelDatos.Controls.Add(this.lblEmailProveedor);
            this.panelDatos.Controls.Add(this.lblEstadoProveedor);
            this.panelDatos.Controls.Add(this.lblColoniaProveedor);
            this.panelDatos.Controls.Add(this.lblCalleProveedor);
            this.panelDatos.Controls.Add(this.lblNombreProveedor);
            this.panelDatos.Controls.Add(this.label10);
            this.panelDatos.Controls.Add(this.label11);
            this.panelDatos.Controls.Add(this.label5);
            this.panelDatos.Controls.Add(this.label9);
            this.panelDatos.Controls.Add(this.label8);
            this.panelDatos.Controls.Add(this.label7);
            this.panelDatos.Controls.Add(this.label6);
            this.panelDatos.Controls.Add(this.label4);
            this.panelDatos.Controls.Add(this.label3);
            this.panelDatos.Controls.Add(this.label2);
            this.panelDatos.Controls.Add(this.label22);
            this.panelDatos.Controls.Add(this.label1);
            this.panelDatos.Location = new System.Drawing.Point(12, 41);
            this.panelDatos.Name = "panelDatos";
            this.panelDatos.Size = new System.Drawing.Size(613, 340);
            this.panelDatos.TabIndex = 0;
            // 
            // lblNombreProveedor
            // 
            this.lblNombreProveedor.BackColor = System.Drawing.Color.White;
            this.lblNombreProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblNombreProveedor.Location = new System.Drawing.Point(30, 77);
            this.lblNombreProveedor.Name = "lblNombreProveedor";
            this.lblNombreProveedor.Size = new System.Drawing.Size(325, 22);
            this.lblNombreProveedor.TabIndex = 87;
            this.lblNombreProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCalleProveedor
            // 
            this.lblCalleProveedor.BackColor = System.Drawing.Color.White;
            this.lblCalleProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblCalleProveedor.Location = new System.Drawing.Point(30, 131);
            this.lblCalleProveedor.Name = "lblCalleProveedor";
            this.lblCalleProveedor.Size = new System.Drawing.Size(325, 22);
            this.lblCalleProveedor.TabIndex = 88;
            this.lblCalleProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblColoniaProveedor
            // 
            this.lblColoniaProveedor.BackColor = System.Drawing.Color.White;
            this.lblColoniaProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblColoniaProveedor.Location = new System.Drawing.Point(30, 184);
            this.lblColoniaProveedor.Name = "lblColoniaProveedor";
            this.lblColoniaProveedor.Size = new System.Drawing.Size(325, 22);
            this.lblColoniaProveedor.TabIndex = 89;
            this.lblColoniaProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEstadoProveedor
            // 
            this.lblEstadoProveedor.BackColor = System.Drawing.Color.White;
            this.lblEstadoProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblEstadoProveedor.Location = new System.Drawing.Point(30, 237);
            this.lblEstadoProveedor.Name = "lblEstadoProveedor";
            this.lblEstadoProveedor.Size = new System.Drawing.Size(325, 22);
            this.lblEstadoProveedor.TabIndex = 90;
            this.lblEstadoProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblEmailProveedor
            // 
            this.lblEmailProveedor.BackColor = System.Drawing.Color.White;
            this.lblEmailProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblEmailProveedor.Location = new System.Drawing.Point(30, 294);
            this.lblEmailProveedor.Name = "lblEmailProveedor";
            this.lblEmailProveedor.Size = new System.Drawing.Size(325, 22);
            this.lblEmailProveedor.TabIndex = 91;
            this.lblEmailProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRFCProveedor
            // 
            this.lblRFCProveedor.BackColor = System.Drawing.Color.White;
            this.lblRFCProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblRFCProveedor.Location = new System.Drawing.Point(398, 78);
            this.lblRFCProveedor.Name = "lblRFCProveedor";
            this.lblRFCProveedor.Size = new System.Drawing.Size(187, 20);
            this.lblRFCProveedor.TabIndex = 92;
            this.lblRFCProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMunicipioProveedor
            // 
            this.lblMunicipioProveedor.BackColor = System.Drawing.Color.White;
            this.lblMunicipioProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblMunicipioProveedor.Location = new System.Drawing.Point(398, 184);
            this.lblMunicipioProveedor.Name = "lblMunicipioProveedor";
            this.lblMunicipioProveedor.Size = new System.Drawing.Size(187, 20);
            this.lblMunicipioProveedor.TabIndex = 93;
            this.lblMunicipioProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCPProveedor
            // 
            this.lblCPProveedor.BackColor = System.Drawing.Color.White;
            this.lblCPProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblCPProveedor.Location = new System.Drawing.Point(398, 237);
            this.lblCPProveedor.Name = "lblCPProveedor";
            this.lblCPProveedor.Size = new System.Drawing.Size(187, 20);
            this.lblCPProveedor.TabIndex = 94;
            this.lblCPProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTelProveedor
            // 
            this.lblTelProveedor.BackColor = System.Drawing.Color.White;
            this.lblTelProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblTelProveedor.Location = new System.Drawing.Point(398, 294);
            this.lblTelProveedor.Name = "lblTelProveedor";
            this.lblTelProveedor.Size = new System.Drawing.Size(187, 20);
            this.lblTelProveedor.TabIndex = 95;
            this.lblTelProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoExtProveedor
            // 
            this.lblNoExtProveedor.BackColor = System.Drawing.Color.White;
            this.lblNoExtProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblNoExtProveedor.Location = new System.Drawing.Point(398, 131);
            this.lblNoExtProveedor.Name = "lblNoExtProveedor";
            this.lblNoExtProveedor.Size = new System.Drawing.Size(81, 20);
            this.lblNoExtProveedor.TabIndex = 96;
            this.lblNoExtProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNoInterProveedor
            // 
            this.lblNoInterProveedor.BackColor = System.Drawing.Color.White;
            this.lblNoInterProveedor.ForeColor = System.Drawing.Color.Blue;
            this.lblNoInterProveedor.Location = new System.Drawing.Point(499, 131);
            this.lblNoInterProveedor.Name = "lblNoInterProveedor";
            this.lblNoInterProveedor.Size = new System.Drawing.Size(86, 20);
            this.lblNoInterProveedor.TabIndex = 97;
            this.lblNoInterProveedor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.panelContenido.ResumeLayout(false);
            this.panelDatos.ResumeLayout(false);
            this.panelDatos.PerformLayout();
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
        private System.Windows.Forms.Panel panelDatos;
        private System.Windows.Forms.Label lblNombreProveedor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNoInterProveedor;
        private System.Windows.Forms.Label lblNoExtProveedor;
        private System.Windows.Forms.Label lblTelProveedor;
        private System.Windows.Forms.Label lblCPProveedor;
        private System.Windows.Forms.Label lblMunicipioProveedor;
        private System.Windows.Forms.Label lblRFCProveedor;
        private System.Windows.Forms.Label lblEmailProveedor;
        private System.Windows.Forms.Label lblEstadoProveedor;
        private System.Windows.Forms.Label lblColoniaProveedor;
        private System.Windows.Forms.Label lblCalleProveedor;
    }
}