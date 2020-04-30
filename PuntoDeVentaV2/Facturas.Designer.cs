namespace PuntoDeVentaV2
{
    partial class Facturas
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
            this.components = new System.ComponentModel.Container();
            this.datagv_facturas = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_bx_tipo_factura = new System.Windows.Forms.ComboBox();
            this.datetp_fecha_inicial = new System.Windows.Forms.DateTimePicker();
            this.datetp_fecha_final = new System.Windows.Forms.DateTimePicker();
            this.btn_buscar = new System.Windows.Forms.Button();
            this.btn_cpago = new System.Windows.Forms.Button();
            this.btn_enviar = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.col_checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_rfc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_razon_social = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_pdf = new System.Windows.Forms.DataGridViewImageColumn();
            this.col_cancelar = new System.Windows.Forms.DataGridViewImageColumn();
            this.col_t_comprobante = new System.Windows.Forms.DataGridViewTextBoxColumn();
<<<<<<< HEAD
            this.TTMensaje = new System.Windows.Forms.ToolTip(this.components);
=======
>>>>>>> Facturacion17
            ((System.ComponentModel.ISupportInitialize)(this.datagv_facturas)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // datagv_facturas
            // 
            this.datagv_facturas.AllowUserToAddRows = false;
            this.datagv_facturas.AllowUserToDeleteRows = false;
            this.datagv_facturas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datagv_facturas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagv_facturas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_checkbox,
            this.col_id,
            this.col_folio,
            this.col_serie,
            this.col_rfc,
            this.col_razon_social,
            this.col_total,
            this.col_fecha,
            this.col_pdf,
            this.col_cancelar,
            this.col_t_comprobante});
            this.datagv_facturas.Location = new System.Drawing.Point(12, 161);
            this.datagv_facturas.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.datagv_facturas.Name = "datagv_facturas";
            this.datagv_facturas.ReadOnly = true;
            this.datagv_facturas.RowHeadersVisible = false;
            this.datagv_facturas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datagv_facturas.Size = new System.Drawing.Size(927, 267);
            this.datagv_facturas.TabIndex = 0;
            this.datagv_facturas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.click_en_icono);
            this.datagv_facturas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.clickcellc_checkbox);
            this.datagv_facturas.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.cursor_en_icono);
            this.datagv_facturas.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.cursor_no_icono);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(388, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "FACTURAS";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cmb_bx_tipo_factura
            // 
            this.cmb_bx_tipo_factura.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_bx_tipo_factura.FormattingEnabled = true;
            this.cmb_bx_tipo_factura.Location = new System.Drawing.Point(8, 12);
            this.cmb_bx_tipo_factura.Name = "cmb_bx_tipo_factura";
            this.cmb_bx_tipo_factura.Size = new System.Drawing.Size(199, 25);
            this.cmb_bx_tipo_factura.TabIndex = 2;
            this.cmb_bx_tipo_factura.SelectionChangeCommitted += new System.EventHandler(this.buscar_tipo_factura);
            // 
            // datetp_fecha_inicial
            // 
            this.datetp_fecha_inicial.CustomFormat = "yyyy-MM-dd";
            this.datetp_fecha_inicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetp_fecha_inicial.Location = new System.Drawing.Point(231, 14);
            this.datetp_fecha_inicial.MaxDate = new System.DateTime(3000, 12, 31, 0, 0, 0, 0);
            this.datetp_fecha_inicial.MinDate = new System.DateTime(1960, 1, 1, 0, 0, 0, 0);
            this.datetp_fecha_inicial.Name = "datetp_fecha_inicial";
            this.datetp_fecha_inicial.Size = new System.Drawing.Size(112, 22);
            this.datetp_fecha_inicial.TabIndex = 3;
            this.datetp_fecha_inicial.Value = new System.DateTime(2020, 2, 13, 0, 0, 0, 0);
            // 
            // datetp_fecha_final
            // 
            this.datetp_fecha_final.CustomFormat = "yyyy-MM-dd";
            this.datetp_fecha_final.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetp_fecha_final.Location = new System.Drawing.Point(349, 14);
            this.datetp_fecha_final.MaxDate = new System.DateTime(3000, 12, 31, 0, 0, 0, 0);
            this.datetp_fecha_final.MinDate = new System.DateTime(1960, 1, 1, 0, 0, 0, 0);
            this.datetp_fecha_final.Name = "datetp_fecha_final";
            this.datetp_fecha_final.Size = new System.Drawing.Size(112, 22);
            this.datetp_fecha_final.TabIndex = 4;
            // 
            // btn_buscar
            // 
            this.btn_buscar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btn_buscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_buscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_buscar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_buscar.ForeColor = System.Drawing.Color.White;
            this.btn_buscar.Location = new System.Drawing.Point(467, 9);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(110, 30);
            this.btn_buscar.TabIndex = 5;
            this.btn_buscar.Text = "Buscar";
            this.btn_buscar.UseVisualStyleBackColor = false;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            // 
            // btn_cpago
            // 
            this.btn_cpago.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cpago.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btn_cpago.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cpago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cpago.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cpago.ForeColor = System.Drawing.Color.White;
            this.btn_cpago.Location = new System.Drawing.Point(723, 105);
            this.btn_cpago.Name = "btn_cpago";
            this.btn_cpago.Size = new System.Drawing.Size(216, 30);
            this.btn_cpago.TabIndex = 6;
            this.btn_cpago.Text = "Generar complemento pago";
            this.btn_cpago.UseVisualStyleBackColor = false;
            this.btn_cpago.Click += new System.EventHandler(this.btn_cpago_Click);
            // 
            // btn_enviar
            // 
            this.btn_enviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_enviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btn_enviar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_enviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_enviar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_enviar.ForeColor = System.Drawing.Color.White;
            this.btn_enviar.Location = new System.Drawing.Point(598, 105);
            this.btn_enviar.Name = "btn_enviar";
            this.btn_enviar.Size = new System.Drawing.Size(119, 29);
            this.btn_enviar.TabIndex = 7;
            this.btn_enviar.Text = "Enviar";
            this.btn_enviar.UseVisualStyleBackColor = false;
            this.btn_enviar.Click += new System.EventHandler(this.btn_enviar_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_buscar);
            this.panel1.Controls.Add(this.datetp_fecha_final);
            this.panel1.Controls.Add(this.datetp_fecha_inicial);
            this.panel1.Controls.Add(this.cmb_bx_tipo_factura);
            this.panel1.Location = new System.Drawing.Point(12, 95);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(585, 50);
            this.panel1.TabIndex = 8;
            // 
            // col_checkbox
            // 
            this.col_checkbox.Frozen = true;
            this.col_checkbox.HeaderText = "";
            this.col_checkbox.Name = "col_checkbox";
            this.col_checkbox.ReadOnly = true;
            this.col_checkbox.Width = 35;
            // 
            // col_id
            // 
            this.col_id.HeaderText = "ID";
            this.col_id.Name = "col_id";
            this.col_id.ReadOnly = true;
            this.col_id.Visible = false;
            this.col_id.Width = 50;
            // 
            // col_folio
            // 
            this.col_folio.HeaderText = "Folio";
            this.col_folio.Name = "col_folio";
            this.col_folio.ReadOnly = true;
            this.col_folio.Width = 45;
            // 
            // col_serie
            // 
            this.col_serie.HeaderText = "Serie";
            this.col_serie.Name = "col_serie";
            this.col_serie.ReadOnly = true;
            this.col_serie.Width = 45;
            // 
            // col_rfc
            // 
            this.col_rfc.HeaderText = "RFC";
            this.col_rfc.Name = "col_rfc";
            this.col_rfc.ReadOnly = true;
            this.col_rfc.Width = 120;
            // 
            // col_razon_social
            // 
<<<<<<< HEAD
            this.col_razon_social.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.col_razon_social.HeaderText = "Razon social";
            this.col_razon_social.Name = "col_razon_social";
            this.col_razon_social.ReadOnly = true;
=======
            this.col_razon_social.HeaderText = "Razon social";
            this.col_razon_social.Name = "col_razon_social";
            this.col_razon_social.ReadOnly = true;
            this.col_razon_social.Width = 440;
>>>>>>> Facturacion17
            // 
            // col_total
            // 
            this.col_total.HeaderText = "Total";
            this.col_total.Name = "col_total";
            this.col_total.ReadOnly = true;
            // 
            // col_fecha
            // 
            this.col_fecha.HeaderText = "Fecha";
            this.col_fecha.Name = "col_fecha";
            this.col_fecha.ReadOnly = true;
            // 
            // col_pdf
            // 
            this.col_pdf.HeaderText = "PDF";
            this.col_pdf.Name = "col_pdf";
            this.col_pdf.ReadOnly = true;
            this.col_pdf.Width = 50;
            // 
            // col_cancelar
            // 
            this.col_cancelar.HeaderText = "Cancelar";
            this.col_cancelar.Name = "col_cancelar";
            this.col_cancelar.ReadOnly = true;
            this.col_cancelar.Width = 70;
            // 
            // col_t_comprobante
            // 
            this.col_t_comprobante.HeaderText = "tc";
            this.col_t_comprobante.Name = "col_t_comprobante";
            this.col_t_comprobante.ReadOnly = true;
            this.col_t_comprobante.Visible = false;
            this.col_t_comprobante.Width = 10;
            // 
<<<<<<< HEAD
            // TTMensaje
            // 
            this.TTMensaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.TTMensaje.ForeColor = System.Drawing.Color.White;
            this.TTMensaje.OwnerDraw = true;
            this.TTMensaje.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.TTMensaje_Draw);
            // 
=======
>>>>>>> Facturacion17
            // Facturas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(951, 519);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_enviar);
            this.Controls.Add(this.btn_cpago);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.datagv_facturas);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Facturas";
            this.Text = "Facturas";
            this.Load += new System.EventHandler(this.Facturas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datagv_facturas)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView datagv_facturas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_bx_tipo_factura;
        private System.Windows.Forms.DateTimePicker datetp_fecha_inicial;
        private System.Windows.Forms.DateTimePicker datetp_fecha_final;
        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.Button btn_cpago;
        private System.Windows.Forms.Button btn_enviar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn col_checkbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_rfc;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_razon_social;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_total;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_fecha;
        private System.Windows.Forms.DataGridViewImageColumn col_pdf;
        private System.Windows.Forms.DataGridViewImageColumn col_cancelar;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_t_comprobante;
<<<<<<< HEAD
        private System.Windows.Forms.ToolTip TTMensaje;
=======
>>>>>>> Facturacion17
    }
}