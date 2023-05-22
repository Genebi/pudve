namespace PuntoDeVentaV2
{
    partial class EnvioDeNotaDeVentaCorreo
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
            this.lb_titulo = new System.Windows.Forms.Label();
            this.pnl_principal = new System.Windows.Forms.FlowLayoutPanel();
            this.btn_enviar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.btn_agregar = new System.Windows.Forms.Button();
            this.txt_correo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lb_titulo
            // 
            this.lb_titulo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_titulo.AutoSize = true;
            this.lb_titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_titulo.Location = new System.Drawing.Point(19, 6);
            this.lb_titulo.Name = "lb_titulo";
            this.lb_titulo.Size = new System.Drawing.Size(129, 13);
            this.lb_titulo.TabIndex = 13;
            this.lb_titulo.Text = "Enviar Nota de Venta";
            this.lb_titulo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // pnl_principal
            // 
            this.pnl_principal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.pnl_principal.AutoScroll = true;
            this.pnl_principal.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnl_principal.Location = new System.Drawing.Point(22, 58);
            this.pnl_principal.Name = "pnl_principal";
            this.pnl_principal.Size = new System.Drawing.Size(370, 153);
            this.pnl_principal.TabIndex = 12;
            this.pnl_principal.WrapContents = false;
            // 
            // btn_enviar
            // 
            this.btn_enviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_enviar.BackColor = System.Drawing.Color.Green;
            this.btn_enviar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_enviar.Enabled = false;
            this.btn_enviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_enviar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_enviar.ForeColor = System.Drawing.Color.White;
            this.btn_enviar.Location = new System.Drawing.Point(302, 228);
            this.btn_enviar.Name = "btn_enviar";
            this.btn_enviar.Size = new System.Drawing.Size(90, 29);
            this.btn_enviar.TabIndex = 11;
            this.btn_enviar.Text = "Enviar";
            this.btn_enviar.UseVisualStyleBackColor = false;
            this.btn_enviar.Click += new System.EventHandler(this.btn_enviar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancelar.BackColor = System.Drawing.Color.Red;
            this.btn_cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancelar.ForeColor = System.Drawing.Color.White;
            this.btn_cancelar.Location = new System.Drawing.Point(206, 228);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(90, 29);
            this.btn_cancelar.TabIndex = 10;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = false;
            // 
            // btn_agregar
            // 
            this.btn_agregar.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_agregar.BackColor = System.Drawing.Color.Teal;
            this.btn_agregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_agregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_agregar.ForeColor = System.Drawing.Color.White;
            this.btn_agregar.Location = new System.Drawing.Point(300, 18);
            this.btn_agregar.Name = "btn_agregar";
            this.btn_agregar.Size = new System.Drawing.Size(92, 27);
            this.btn_agregar.TabIndex = 9;
            this.btn_agregar.Text = "Agregar";
            this.btn_agregar.UseVisualStyleBackColor = false;
            this.btn_agregar.Click += new System.EventHandler(this.btn_agregar_Click);
            // 
            // txt_correo
            // 
            this.txt_correo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txt_correo.Location = new System.Drawing.Point(22, 22);
            this.txt_correo.Name = "txt_correo";
            this.txt_correo.Size = new System.Drawing.Size(272, 20);
            this.txt_correo.TabIndex = 8;
            // 
            // EnvioDeNotaDeVentaCorreo
            // 
            this.ClientSize = new System.Drawing.Size(413, 269);
            this.Controls.Add(this.lb_titulo);
            this.Controls.Add(this.pnl_principal);
            this.Controls.Add(this.btn_enviar);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_agregar);
            this.Controls.Add(this.txt_correo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EnvioDeNotaDeVentaCorreo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.EnvioDeNotaDeVentaCorreo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chRespaldo;
        private System.Windows.Forms.CheckBox cbCorreoDescuento;
        private System.Windows.Forms.CheckBox cbCorreoIniciar;
        private System.Windows.Forms.CheckBox cbCorreoVenta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbCorreoCorteCaja;
        private System.Windows.Forms.CheckBox cbCorreoEliminarListaProductosVentas;
        private System.Windows.Forms.CheckBox cbCorreoCerrarVentanaVentas;
        private System.Windows.Forms.CheckBox cbCorreoRetirarDineroCaja;
        private System.Windows.Forms.CheckBox cbCorreoAgregarDineroCaja;
        private System.Windows.Forms.CheckBox cbCorreoPrecioProducto;
        private System.Windows.Forms.CheckBox cbCorreoStockProducto;
        private System.Windows.Forms.CheckBox cbCorreoStockMinimo;
        private System.Windows.Forms.CheckBox cbCorreoVenderProducto;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.CheckBox cbRecibirAnricipo;
        private System.Windows.Forms.CheckBox CBXClienteDescuento;
        private System.Windows.Forms.CheckBox chkBoxSaldoInicial;
        private System.Windows.Forms.CheckBox chbCaducidad;
        private System.Windows.Forms.CheckBox ckbCorreoAbonos;
        private System.Windows.Forms.Label lb_titulo;
        private System.Windows.Forms.FlowLayoutPanel pnl_principal;
        private System.Windows.Forms.Button btn_enviar;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Button btn_agregar;
        private System.Windows.Forms.TextBox txt_correo;
    }
}