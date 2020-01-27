namespace PuntoDeVentaV2
{
    partial class Crear_factura
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
            this.cmb_bx_clientes = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupb_clientes = new System.Windows.Forms.GroupBox();
            this.btn_crear_cliente = new System.Windows.Forms.Button();
            this.groupb_pago = new System.Windows.Forms.GroupBox();
            this.txt_cuenta = new System.Windows.Forms.TextBox();
            this.txt_tipo_cambio = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmb_bx_moneda = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_bx_forma_pago = new System.Windows.Forms.ComboBox();
            this.cmb_bx_metodo_pago = new System.Windows.Forms.ComboBox();
            this.groupb_productos = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pnl_productos = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btn_facturar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.groupb_clientes.SuspendLayout();
            this.groupb_pago.SuspendLayout();
            this.groupb_productos.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(312, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Datos facturación";
            // 
            // cmb_bx_clientes
            // 
            this.cmb_bx_clientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmb_bx_clientes.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_bx_clientes.FormattingEnabled = true;
            this.cmb_bx_clientes.Location = new System.Drawing.Point(14, 25);
            this.cmb_bx_clientes.Name = "cmb_bx_clientes";
            this.cmb_bx_clientes.Size = new System.Drawing.Size(707, 25);
            this.cmb_bx_clientes.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(6, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Método de pago";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(343, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Forma de pago";
            // 
            // groupb_clientes
            // 
            this.groupb_clientes.Controls.Add(this.btn_crear_cliente);
            this.groupb_clientes.Controls.Add(this.cmb_bx_clientes);
            this.groupb_clientes.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupb_clientes.ForeColor = System.Drawing.Color.Navy;
            this.groupb_clientes.Location = new System.Drawing.Point(12, 82);
            this.groupb_clientes.Name = "groupb_clientes";
            this.groupb_clientes.Size = new System.Drawing.Size(856, 59);
            this.groupb_clientes.TabIndex = 8;
            this.groupb_clientes.TabStop = false;
            this.groupb_clientes.Text = "Cliente";
            // 
            // btn_crear_cliente
            // 
            this.btn_crear_cliente.BackColor = System.Drawing.Color.Teal;
            this.btn_crear_cliente.Cursor = System.Windows.Forms.Cursors.No;
            this.btn_crear_cliente.Enabled = false;
            this.btn_crear_cliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_crear_cliente.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_crear_cliente.ForeColor = System.Drawing.Color.White;
            this.btn_crear_cliente.Location = new System.Drawing.Point(740, 21);
            this.btn_crear_cliente.Name = "btn_crear_cliente";
            this.btn_crear_cliente.Size = new System.Drawing.Size(110, 30);
            this.btn_crear_cliente.TabIndex = 3;
            this.btn_crear_cliente.Text = "Ir a clientes";
            this.btn_crear_cliente.UseVisualStyleBackColor = false;
            this.btn_crear_cliente.Click += new System.EventHandler(this.ir_a_clientes);
            // 
            // groupb_pago
            // 
            this.groupb_pago.Controls.Add(this.txt_cuenta);
            this.groupb_pago.Controls.Add(this.txt_tipo_cambio);
            this.groupb_pago.Controls.Add(this.label5);
            this.groupb_pago.Controls.Add(this.cmb_bx_moneda);
            this.groupb_pago.Controls.Add(this.label2);
            this.groupb_pago.Controls.Add(this.cmb_bx_forma_pago);
            this.groupb_pago.Controls.Add(this.cmb_bx_metodo_pago);
            this.groupb_pago.Controls.Add(this.label4);
            this.groupb_pago.Controls.Add(this.label3);
            this.groupb_pago.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupb_pago.ForeColor = System.Drawing.Color.Navy;
            this.groupb_pago.Location = new System.Drawing.Point(12, 160);
            this.groupb_pago.Name = "groupb_pago";
            this.groupb_pago.Size = new System.Drawing.Size(856, 121);
            this.groupb_pago.TabIndex = 9;
            this.groupb_pago.TabStop = false;
            this.groupb_pago.Text = "Forma de pago";
            // 
            // txt_cuenta
            // 
            this.txt_cuenta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_cuenta.Location = new System.Drawing.Point(714, 34);
            this.txt_cuenta.MaxLength = 50;
            this.txt_cuenta.Name = "txt_cuenta";
            this.txt_cuenta.ReadOnly = true;
            this.txt_cuenta.Size = new System.Drawing.Size(136, 22);
            this.txt_cuenta.TabIndex = 16;
            this.txt_cuenta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tipo_de_datos);
            // 
            // txt_tipo_cambio
            // 
            this.txt_tipo_cambio.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_tipo_cambio.ForeColor = System.Drawing.Color.Black;
            this.txt_tipo_cambio.Location = new System.Drawing.Point(722, 71);
            this.txt_tipo_cambio.MaxLength = 200;
            this.txt_tipo_cambio.Name = "txt_tipo_cambio";
            this.txt_tipo_cambio.ReadOnly = true;
            this.txt_tipo_cambio.Size = new System.Drawing.Size(128, 22);
            this.txt_tipo_cambio.TabIndex = 15;
            this.txt_tipo_cambio.Text = "1.000000";
            this.txt_tipo_cambio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.solo_numeros_tcambio);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(635, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 17);
            this.label5.TabIndex = 14;
            this.label5.Text = "Tipo cambio";
            // 
            // cmb_bx_moneda
            // 
            this.cmb_bx_moneda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmb_bx_moneda.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_bx_moneda.ForeColor = System.Drawing.Color.Black;
            this.cmb_bx_moneda.FormattingEnabled = true;
            this.cmb_bx_moneda.Location = new System.Drawing.Point(122, 71);
            this.cmb_bx_moneda.Name = "cmb_bx_moneda";
            this.cmb_bx_moneda.Size = new System.Drawing.Size(507, 25);
            this.cmb_bx_moneda.TabIndex = 13;
            this.cmb_bx_moneda.SelectionChangeCommitted += new System.EventHandler(this.sel_moneda);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(58, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 17);
            this.label2.TabIndex = 12;
            this.label2.Text = "Moneda";
            // 
            // cmb_bx_forma_pago
            // 
            this.cmb_bx_forma_pago.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmb_bx_forma_pago.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_bx_forma_pago.ForeColor = System.Drawing.Color.Black;
            this.cmb_bx_forma_pago.FormattingEnabled = true;
            this.cmb_bx_forma_pago.Location = new System.Drawing.Point(448, 31);
            this.cmb_bx_forma_pago.Name = "cmb_bx_forma_pago";
            this.cmb_bx_forma_pago.Size = new System.Drawing.Size(260, 25);
            this.cmb_bx_forma_pago.TabIndex = 11;
            this.cmb_bx_forma_pago.SelectionChangeCommitted += new System.EventHandler(this.sel_forma_pago);
            // 
            // cmb_bx_metodo_pago
            // 
            this.cmb_bx_metodo_pago.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmb_bx_metodo_pago.Enabled = false;
            this.cmb_bx_metodo_pago.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_bx_metodo_pago.ForeColor = System.Drawing.Color.Black;
            this.cmb_bx_metodo_pago.FormattingEnabled = true;
            this.cmb_bx_metodo_pago.Items.AddRange(new object[] {
            "Pago en una sola exhibición",
            "Pago en parcialidades o diferido"});
            this.cmb_bx_metodo_pago.Location = new System.Drawing.Point(122, 31);
            this.cmb_bx_metodo_pago.Name = "cmb_bx_metodo_pago";
            this.cmb_bx_metodo_pago.Size = new System.Drawing.Size(215, 25);
            this.cmb_bx_metodo_pago.TabIndex = 10;
            // 
            // groupb_productos
            // 
            this.groupb_productos.BackColor = System.Drawing.SystemColors.Control;
            this.groupb_productos.Controls.Add(this.label9);
            this.groupb_productos.Controls.Add(this.pnl_productos);
            this.groupb_productos.Controls.Add(this.label7);
            this.groupb_productos.Controls.Add(this.label8);
            this.groupb_productos.Controls.Add(this.label6);
            this.groupb_productos.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupb_productos.ForeColor = System.Drawing.Color.Navy;
            this.groupb_productos.Location = new System.Drawing.Point(12, 307);
            this.groupb_productos.Name = "groupb_productos";
            this.groupb_productos.Size = new System.Drawing.Size(856, 174);
            this.groupb_productos.TabIndex = 10;
            this.groupb_productos.TabStop = false;
            this.groupb_productos.Text = "Productos";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(15, 35);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 15);
            this.label9.TabIndex = 14;
            this.label9.Text = "unidad";
            // 
            // pnl_productos
            // 
            this.pnl_productos.AutoScroll = true;
            this.pnl_productos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnl_productos.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnl_productos.Location = new System.Drawing.Point(9, 54);
            this.pnl_productos.Name = "pnl_productos";
            this.pnl_productos.Size = new System.Drawing.Size(839, 110);
            this.pnl_productos.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(74, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 15);
            this.label7.TabIndex = 12;
            this.label7.Text = "Clave producto";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(293, 30);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(72, 15);
            this.label8.TabIndex = 13;
            this.label8.Text = "Descripción";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(18, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 15);
            this.label6.TabIndex = 11;
            this.label6.Text = "Clave";
            // 
            // btn_facturar
            // 
            this.btn_facturar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_facturar.BackColor = System.Drawing.Color.Green;
            this.btn_facturar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_facturar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_facturar.ForeColor = System.Drawing.Color.White;
            this.btn_facturar.Location = new System.Drawing.Point(749, 504);
            this.btn_facturar.Name = "btn_facturar";
            this.btn_facturar.Size = new System.Drawing.Size(119, 30);
            this.btn_facturar.TabIndex = 11;
            this.btn_facturar.Text = "Facturar";
            this.btn_facturar.UseVisualStyleBackColor = false;
            this.btn_facturar.Click += new System.EventHandler(this.btn_facturar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancelar.BackColor = System.Drawing.Color.Red;
            this.btn_cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancelar.ForeColor = System.Drawing.Color.White;
            this.btn_cancelar.Location = new System.Drawing.Point(614, 504);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(119, 30);
            this.btn_cancelar.TabIndex = 12;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // Crear_factura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 546);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_facturar);
            this.Controls.Add(this.groupb_productos);
            this.Controls.Add(this.groupb_pago);
            this.Controls.Add(this.groupb_clientes);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Crear_factura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Crear Factura";
            this.Load += new System.EventHandler(this.Crear_factura_Load);
            this.groupb_clientes.ResumeLayout(false);
            this.groupb_pago.ResumeLayout(false);
            this.groupb_pago.PerformLayout();
            this.groupb_productos.ResumeLayout(false);
            this.groupb_productos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_bx_clientes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupb_clientes;
        private System.Windows.Forms.GroupBox groupb_pago;
        private System.Windows.Forms.TextBox txt_tipo_cambio;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmb_bx_moneda;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_bx_forma_pago;
        private System.Windows.Forms.ComboBox cmb_bx_metodo_pago;
        private System.Windows.Forms.GroupBox groupb_productos;
        private System.Windows.Forms.Panel pnl_productos;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_facturar;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.TextBox txt_cuenta;
        private System.Windows.Forms.Button btn_crear_cliente;
    }
}