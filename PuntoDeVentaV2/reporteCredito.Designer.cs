namespace PuntoDeVentaV2
{
    partial class reporteCredito
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DGVClientes = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RFC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RazonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeudaTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Agregar = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtBuscador = new System.Windows.Forms.TextBox();
            this.lbBuscar = new System.Windows.Forms.Label();
            this.btnBucarCliente = new System.Windows.Forms.Button();
            this.btnGenerarReporte = new PuntoDeVentaV2.BotonRedondo();
            ((System.ComponentModel.ISupportInitialize)(this.DGVClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVClientes
            // 
            this.DGVClientes.AllowUserToAddRows = false;
            this.DGVClientes.AllowUserToDeleteRows = false;
            this.DGVClientes.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DGVClientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.RFC,
            this.RazonSocial,
            this.DeudaTotal,
            this.Agregar});
            this.DGVClientes.Location = new System.Drawing.Point(2, 84);
            this.DGVClientes.Name = "DGVClientes";
            this.DGVClientes.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVClientes.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.DGVClientes.RowHeadersVisible = false;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DGVClientes.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.DGVClientes.Size = new System.Drawing.Size(579, 215);
            this.DGVClientes.TabIndex = 0;
            this.DGVClientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVClientes_CellClick);
            this.DGVClientes.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVClientes_CellMouseEnter);
            this.DGVClientes.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVClientes_CellMouseLeave);
            this.DGVClientes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGVClientes_KeyDown);
            // 
            // ID
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ID.DefaultCellStyle = dataGridViewCellStyle2;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // RFC
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RFC.DefaultCellStyle = dataGridViewCellStyle3;
            this.RFC.HeaderText = "RFC";
            this.RFC.Name = "RFC";
            this.RFC.ReadOnly = true;
            this.RFC.Width = 150;
            // 
            // RazonSocial
            // 
            this.RazonSocial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RazonSocial.DefaultCellStyle = dataGridViewCellStyle4;
            this.RazonSocial.HeaderText = "Razón Social";
            this.RazonSocial.Name = "RazonSocial";
            this.RazonSocial.ReadOnly = true;
            // 
            // DeudaTotal
            // 
            this.DeudaTotal.DataPropertyName = "DeudaTotal";
            this.DeudaTotal.HeaderText = "Deuda Total";
            this.DeudaTotal.Name = "DeudaTotal";
            this.DeudaTotal.ReadOnly = true;
            // 
            // Agregar
            // 
            this.Agregar.Description = "Seleccionar";
            this.Agregar.HeaderText = "Detalles";
            this.Agregar.Name = "Agregar";
            this.Agregar.ReadOnly = true;
            this.Agregar.Width = 50;
            // 
            // txtBuscador
            // 
            this.txtBuscador.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscador.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscador.Location = new System.Drawing.Point(121, 32);
            this.txtBuscador.Name = "txtBuscador";
            this.txtBuscador.Size = new System.Drawing.Size(421, 23);
            this.txtBuscador.TabIndex = 2;
            this.txtBuscador.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBuscador.TextChanged += new System.EventHandler(this.txtBuscador_TextChanged);
            this.txtBuscador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscador_KeyDown);
            // 
            // lbBuscar
            // 
            this.lbBuscar.AutoSize = true;
            this.lbBuscar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBuscar.Location = new System.Drawing.Point(12, 35);
            this.lbBuscar.Name = "lbBuscar";
            this.lbBuscar.Size = new System.Drawing.Size(96, 17);
            this.lbBuscar.TabIndex = 3;
            this.lbBuscar.Text = "Buscar cliente";
            // 
            // btnBucarCliente
            // 
            this.btnBucarCliente.Image = global::PuntoDeVentaV2.Properties.Resources.search;
            this.btnBucarCliente.Location = new System.Drawing.Point(548, 31);
            this.btnBucarCliente.Name = "btnBucarCliente";
            this.btnBucarCliente.Size = new System.Drawing.Size(25, 24);
            this.btnBucarCliente.TabIndex = 114;
            this.btnBucarCliente.UseVisualStyleBackColor = true;
            this.btnBucarCliente.Click += new System.EventHandler(this.btnBucarCliente_Click);
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.BackColor = System.Drawing.Color.Firebrick;
            this.btnGenerarReporte.BackGroundColor = System.Drawing.Color.Firebrick;
            this.btnGenerarReporte.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnGenerarReporte.BorderRadius = 40;
            this.btnGenerarReporte.BorderSize = 0;
            this.btnGenerarReporte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarReporte.FlatAppearance.BorderSize = 0;
            this.btnGenerarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarReporte.ForeColor = System.Drawing.Color.White;
            this.btnGenerarReporte.Location = new System.Drawing.Point(216, 407);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(150, 40);
            this.btnGenerarReporte.TabIndex = 115;
            this.btnGenerarReporte.Text = "GENERAR REPORTE";
            this.btnGenerarReporte.TextColor = System.Drawing.Color.White;
            this.btnGenerarReporte.UseVisualStyleBackColor = false;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // reporteCredito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 459);
            this.Controls.Add(this.btnGenerarReporte);
            this.Controls.Add(this.btnBucarCliente);
            this.Controls.Add(this.lbBuscar);
            this.Controls.Add(this.txtBuscador);
            this.Controls.Add(this.DGVClientes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "reporteCredito";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de creditos";
            this.Load += new System.EventHandler(this.ListaClientes_Load);
            this.Shown += new System.EventHandler(this.ListaClientes_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListaClientes_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVClientes;
        private System.Windows.Forms.TextBox txtBuscador;
        private System.Windows.Forms.Label lbBuscar;
        private System.Windows.Forms.Button btnBucarCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RFC;
        private System.Windows.Forms.DataGridViewTextBoxColumn RazonSocial;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeudaTotal;
        private System.Windows.Forms.DataGridViewImageColumn Agregar;
        private BotonRedondo btnGenerarReporte;
    }
}