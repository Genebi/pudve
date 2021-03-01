namespace PuntoDeVentaV2
{
    partial class Lista_complementos_pago
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
            this.datagv_complementospg = new System.Windows.Forms.DataGridView();
            this.btn_enviar = new System.Windows.Forms.Button();
            this.TTMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.elegir_carpeta_descarga = new System.Windows.Forms.FolderBrowserDialog();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.col_checkbox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.col_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_rfc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_razon_social = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_pdf = new System.Windows.Forms.DataGridViewImageColumn();
            this.col_descargar = new System.Windows.Forms.DataGridViewImageColumn();
            this.col_cancelar = new System.Windows.Forms.DataGridViewImageColumn();
            this.col_empleado = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.datagv_complementospg)).BeginInit();
            this.SuspendLayout();
            // 
            // datagv_complementospg
            // 
            this.datagv_complementospg.AllowUserToAddRows = false;
            this.datagv_complementospg.AllowUserToDeleteRows = false;
            this.datagv_complementospg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.datagv_complementospg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datagv_complementospg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_checkbox,
            this.col_id,
            this.col_folio,
            this.col_serie,
            this.col_rfc,
            this.col_razon_social,
            this.col_total,
            this.col_fecha,
            this.col_pdf,
            this.col_descargar,
            this.col_cancelar,
            this.col_empleado});
            this.datagv_complementospg.Location = new System.Drawing.Point(12, 81);
            this.datagv_complementospg.Name = "datagv_complementospg";
            this.datagv_complementospg.ReadOnly = true;
            this.datagv_complementospg.RowHeadersVisible = false;
            this.datagv_complementospg.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.datagv_complementospg.Size = new System.Drawing.Size(888, 208);
            this.datagv_complementospg.TabIndex = 0;
            this.datagv_complementospg.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.click_en_icono);
            this.datagv_complementospg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.clickcellc_checkbox);
            this.datagv_complementospg.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.cursor_en_icono);
            this.datagv_complementospg.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.cursor_no_icono);
            // 
            // btn_enviar
            // 
            this.btn_enviar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_enviar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btn_enviar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_enviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_enviar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_enviar.ForeColor = System.Drawing.Color.White;
            this.btn_enviar.Location = new System.Drawing.Point(12, 34);
            this.btn_enviar.Name = "btn_enviar";
            this.btn_enviar.Size = new System.Drawing.Size(119, 30);
            this.btn_enviar.TabIndex = 1;
            this.btn_enviar.Text = "Enviar correo";
            this.btn_enviar.UseVisualStyleBackColor = false;
            this.btn_enviar.Click += new System.EventHandler(this.btn_enviar_Click);
            // 
            // TTMensaje
            // 
            this.TTMensaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.TTMensaje.ForeColor = System.Drawing.Color.White;
            this.TTMensaje.OwnerDraw = true;
            this.TTMensaje.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.TTMensaje_Draw);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btn_cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancelar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancelar.ForeColor = System.Drawing.Color.White;
            this.btn_cancelar.Location = new System.Drawing.Point(145, 34);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(119, 30);
            this.btn_cancelar.TabIndex = 3;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // col_checkbox
            // 
            this.col_checkbox.HeaderText = "";
            this.col_checkbox.Name = "col_checkbox";
            this.col_checkbox.ReadOnly = true;
            this.col_checkbox.Width = 35;
            // 
            // col_id
            // 
            this.col_id.HeaderText = "";
            this.col_id.Name = "col_id";
            this.col_id.ReadOnly = true;
            this.col_id.Visible = false;
            this.col_id.Width = 40;
            // 
            // col_folio
            // 
            this.col_folio.HeaderText = "Folio";
            this.col_folio.Name = "col_folio";
            this.col_folio.ReadOnly = true;
            this.col_folio.Width = 60;
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
            this.col_razon_social.HeaderText = "Razón social";
            this.col_razon_social.Name = "col_razon_social";
            this.col_razon_social.ReadOnly = true;
            this.col_razon_social.Width = 300;
            // 
            // col_total
            // 
            this.col_total.HeaderText = "Total";
            this.col_total.Name = "col_total";
            this.col_total.ReadOnly = true;
            this.col_total.Width = 110;
            // 
            // col_fecha
            // 
            this.col_fecha.HeaderText = "Fecha";
            this.col_fecha.Name = "col_fecha";
            this.col_fecha.ReadOnly = true;
            this.col_fecha.Width = 95;
            // 
            // col_pdf
            // 
            this.col_pdf.HeaderText = "";
            this.col_pdf.Name = "col_pdf";
            this.col_pdf.ReadOnly = true;
            this.col_pdf.Width = 30;
            // 
            // col_descargar
            // 
            this.col_descargar.HeaderText = "";
            this.col_descargar.Name = "col_descargar";
            this.col_descargar.ReadOnly = true;
            this.col_descargar.Width = 30;
            // 
            // col_cancelar
            // 
            this.col_cancelar.HeaderText = "";
            this.col_cancelar.Name = "col_cancelar";
            this.col_cancelar.ReadOnly = true;
            this.col_cancelar.Width = 30;
            // 
            // col_empleado
            // 
            this.col_empleado.HeaderText = "";
            this.col_empleado.Name = "col_empleado";
            this.col_empleado.ReadOnly = true;
            this.col_empleado.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.col_empleado.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.col_empleado.Width = 30;
            // 
            // Lista_complementos_pago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 301);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_enviar);
            this.Controls.Add(this.datagv_complementospg);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Lista_complementos_pago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Complementos de pago";
            this.Load += new System.EventHandler(this.Lista_complementos_pago_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datagv_complementospg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView datagv_complementospg;
        private System.Windows.Forms.Button btn_enviar;
        private System.Windows.Forms.ToolTip TTMensaje;
        private System.Windows.Forms.FolderBrowserDialog elegir_carpeta_descarga;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn col_checkbox;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_rfc;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_razon_social;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_total;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_fecha;
        private System.Windows.Forms.DataGridViewImageColumn col_pdf;
        private System.Windows.Forms.DataGridViewImageColumn col_descargar;
        private System.Windows.Forms.DataGridViewImageColumn col_cancelar;
        private System.Windows.Forms.DataGridViewImageColumn col_empleado;
    }
}