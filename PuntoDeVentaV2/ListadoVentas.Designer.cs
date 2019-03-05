namespace PuntoDeVentaV2
{
    partial class ListadoVentas
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
            this.GVListadoVentas = new System.Windows.Forms.DataGridView();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.cbVentas = new System.Windows.Forms.ComboBox();
            this.cbTipoVentas = new System.Windows.Forms.ComboBox();
            this.tbFechaInicial = new System.Windows.Forms.TextBox();
            this.tbFechaFinal = new System.Windows.Forms.TextBox();
            this.btnBuscarVentas = new System.Windows.Forms.Button();
            this.btnNuevaVenta = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.GVListadoVentas)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(388, 27);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(90, 25);
            this.tituloSeccion.TabIndex = 4;
            this.tituloSeccion.Text = "VENTAS";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // GVListadoVentas
            // 
            this.GVListadoVentas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GVListadoVentas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GVListadoVentas.Location = new System.Drawing.Point(12, 131);
            this.GVListadoVentas.Name = "GVListadoVentas";
            this.GVListadoVentas.Size = new System.Drawing.Size(845, 217);
            this.GVListadoVentas.TabIndex = 5;
            // 
            // panelBotones
            // 
            this.panelBotones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBotones.Controls.Add(this.btnNuevaVenta);
            this.panelBotones.Controls.Add(this.btnBuscarVentas);
            this.panelBotones.Controls.Add(this.tbFechaFinal);
            this.panelBotones.Controls.Add(this.tbFechaInicial);
            this.panelBotones.Controls.Add(this.cbTipoVentas);
            this.panelBotones.Controls.Add(this.cbVentas);
            this.panelBotones.Location = new System.Drawing.Point(12, 77);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(845, 50);
            this.panelBotones.TabIndex = 6;
            // 
            // cbVentas
            // 
            this.cbVentas.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbVentas.FormattingEnabled = true;
            this.cbVentas.Items.AddRange(new object[] {
            "Todas las ventas",
            "Mis ventas"});
            this.cbVentas.Location = new System.Drawing.Point(3, 18);
            this.cbVentas.Name = "cbVentas";
            this.cbVentas.Size = new System.Drawing.Size(185, 24);
            this.cbVentas.TabIndex = 0;
            // 
            // cbTipoVentas
            // 
            this.cbTipoVentas.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoVentas.FormattingEnabled = true;
            this.cbTipoVentas.Items.AddRange(new object[] {
            "Notas pagadas",
            "Notas pagadas y facturadas",
            "Notas guardadas",
            "Notas canceladas",
            "Notas parcialmente pagadas",
            "Presupuestos"});
            this.cbTipoVentas.Location = new System.Drawing.Point(193, 18);
            this.cbTipoVentas.Name = "cbTipoVentas";
            this.cbTipoVentas.Size = new System.Drawing.Size(185, 24);
            this.cbTipoVentas.TabIndex = 1;
            // 
            // tbFechaInicial
            // 
            this.tbFechaInicial.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFechaInicial.Location = new System.Drawing.Point(394, 18);
            this.tbFechaInicial.Multiline = true;
            this.tbFechaInicial.Name = "tbFechaInicial";
            this.tbFechaInicial.Size = new System.Drawing.Size(100, 24);
            this.tbFechaInicial.TabIndex = 2;
            this.tbFechaInicial.Text = "Fecha inicial";
            this.tbFechaInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbFechaFinal
            // 
            this.tbFechaFinal.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbFechaFinal.Location = new System.Drawing.Point(500, 18);
            this.tbFechaFinal.Multiline = true;
            this.tbFechaFinal.Name = "tbFechaFinal";
            this.tbFechaFinal.Size = new System.Drawing.Size(100, 24);
            this.tbFechaFinal.TabIndex = 3;
            this.tbFechaFinal.Text = "Fecha final";
            this.tbFechaFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnBuscarVentas
            // 
            this.btnBuscarVentas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnBuscarVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarVentas.FlatAppearance.BorderSize = 0;
            this.btnBuscarVentas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnBuscarVentas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnBuscarVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarVentas.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarVentas.ForeColor = System.Drawing.Color.White;
            this.btnBuscarVentas.Location = new System.Drawing.Point(606, 18);
            this.btnBuscarVentas.Name = "btnBuscarVentas";
            this.btnBuscarVentas.Size = new System.Drawing.Size(75, 24);
            this.btnBuscarVentas.TabIndex = 4;
            this.btnBuscarVentas.Text = "Buscar";
            this.btnBuscarVentas.UseVisualStyleBackColor = false;
            this.btnBuscarVentas.Click += new System.EventHandler(this.btnBuscarVentas_Click);
            // 
            // btnNuevaVenta
            // 
            this.btnNuevaVenta.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevaVenta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnNuevaVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevaVenta.FlatAppearance.BorderSize = 0;
            this.btnNuevaVenta.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnNuevaVenta.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnNuevaVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaVenta.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevaVenta.ForeColor = System.Drawing.Color.White;
            this.btnNuevaVenta.Location = new System.Drawing.Point(731, 18);
            this.btnNuevaVenta.Name = "btnNuevaVenta";
            this.btnNuevaVenta.Size = new System.Drawing.Size(111, 24);
            this.btnNuevaVenta.TabIndex = 5;
            this.btnNuevaVenta.Text = "Nueva venta";
            this.btnNuevaVenta.UseVisualStyleBackColor = false;
            this.btnNuevaVenta.Click += new System.EventHandler(this.btnNuevaVenta_Click);
            // 
            // ListadoVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 561);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.GVListadoVentas);
            this.Controls.Add(this.tituloSeccion);
            this.Name = "ListadoVentas";
            this.Text = "ListadoVentas";
            this.Load += new System.EventHandler(this.ListadoVentas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.GVListadoVentas)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.panelBotones.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.DataGridView GVListadoVentas;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Button btnNuevaVenta;
        private System.Windows.Forms.Button btnBuscarVentas;
        private System.Windows.Forms.TextBox tbFechaFinal;
        private System.Windows.Forms.TextBox tbFechaInicial;
        private System.Windows.Forms.ComboBox cbTipoVentas;
        private System.Windows.Forms.ComboBox cbVentas;
    }
}