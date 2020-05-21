namespace PuntoDeVentaV2
{
    partial class ListaClientes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DGVClientes = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RFC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RazonSocial = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumeroCliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Agregar = new System.Windows.Forms.DataGridViewImageColumn();
            this.lbAgregarCliente = new System.Windows.Forms.LinkLabel();
            this.txtBuscador = new System.Windows.Forms.TextBox();
            this.lbBuscar = new System.Windows.Forms.Label();
            this.btnPublicoG = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVClientes
            // 
            this.DGVClientes.AllowUserToAddRows = false;
            this.DGVClientes.AllowUserToDeleteRows = false;
            this.DGVClientes.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DGVClientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.DGVClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVClientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.RFC,
            this.RazonSocial,
            this.NumeroCliente,
            this.Agregar});
            this.DGVClientes.Location = new System.Drawing.Point(2, 84);
            this.DGVClientes.Name = "DGVClientes";
            this.DGVClientes.ReadOnly = true;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVClientes.RowHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.DGVClientes.RowHeadersVisible = false;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DGVClientes.RowsDefaultCellStyle = dataGridViewCellStyle12;
            this.DGVClientes.Size = new System.Drawing.Size(579, 168);
            this.DGVClientes.TabIndex = 0;
            this.DGVClientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVClientes_CellClick);
            this.DGVClientes.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVClientes_CellMouseEnter);
            this.DGVClientes.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVClientes_CellMouseLeave);
            // 
            // ID
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ID.DefaultCellStyle = dataGridViewCellStyle8;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // RFC
            // 
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RFC.DefaultCellStyle = dataGridViewCellStyle9;
            this.RFC.HeaderText = "RFC";
            this.RFC.Name = "RFC";
            this.RFC.ReadOnly = true;
            this.RFC.Width = 150;
            // 
            // RazonSocial
            // 
            this.RazonSocial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RazonSocial.DefaultCellStyle = dataGridViewCellStyle10;
            this.RazonSocial.HeaderText = "Razón Social";
            this.RazonSocial.Name = "RazonSocial";
            this.RazonSocial.ReadOnly = true;
            // 
            // NumeroCliente
            // 
            this.NumeroCliente.HeaderText = "No. Cliente";
            this.NumeroCliente.Name = "NumeroCliente";
            this.NumeroCliente.ReadOnly = true;
            // 
            // Agregar
            // 
            this.Agregar.Description = "Seleccionar";
            this.Agregar.HeaderText = "";
            this.Agregar.Name = "Agregar";
            this.Agregar.ReadOnly = true;
            this.Agregar.Width = 50;
            // 
            // lbAgregarCliente
            // 
            this.lbAgregarCliente.AutoSize = true;
            this.lbAgregarCliente.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAgregarCliente.Location = new System.Drawing.Point(217, 274);
            this.lbAgregarCliente.Name = "lbAgregarCliente";
            this.lbAgregarCliente.Size = new System.Drawing.Size(152, 17);
            this.lbAgregarCliente.TabIndex = 1;
            this.lbAgregarCliente.TabStop = true;
            this.lbAgregarCliente.Text = "Agregar nuevo cliente";
            this.lbAgregarCliente.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbAgregarCliente_LinkClicked);
            // 
            // txtBuscador
            // 
            this.txtBuscador.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscador.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscador.Location = new System.Drawing.Point(136, 32);
            this.txtBuscador.Name = "txtBuscador";
            this.txtBuscador.Size = new System.Drawing.Size(287, 23);
            this.txtBuscador.TabIndex = 2;
            this.txtBuscador.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBuscador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscador_KeyDown);
            // 
            // lbBuscar
            // 
            this.lbBuscar.AutoSize = true;
            this.lbBuscar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBuscar.Location = new System.Drawing.Point(34, 35);
            this.lbBuscar.Name = "lbBuscar";
            this.lbBuscar.Size = new System.Drawing.Size(96, 17);
            this.lbBuscar.TabIndex = 3;
            this.lbBuscar.Text = "Buscar cliente";
            // 
            // btnPublicoG
            // 
            this.btnPublicoG.BackColor = System.Drawing.Color.Green;
            this.btnPublicoG.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPublicoG.FlatAppearance.BorderSize = 0;
            this.btnPublicoG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPublicoG.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPublicoG.ForeColor = System.Drawing.Color.White;
            this.btnPublicoG.Location = new System.Drawing.Point(454, 31);
            this.btnPublicoG.Name = "btnPublicoG";
            this.btnPublicoG.Size = new System.Drawing.Size(118, 25);
            this.btnPublicoG.TabIndex = 113;
            this.btnPublicoG.Text = "Público general";
            this.btnPublicoG.UseVisualStyleBackColor = false;
            this.btnPublicoG.Click += new System.EventHandler(this.btnPublicoG_Click);
            // 
            // ListaClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 311);
            this.Controls.Add(this.btnPublicoG);
            this.Controls.Add(this.lbBuscar);
            this.Controls.Add(this.txtBuscador);
            this.Controls.Add(this.lbAgregarCliente);
            this.Controls.Add(this.DGVClientes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ListaClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Lista de Clientes";
            this.Load += new System.EventHandler(this.ListaClientes_Load);
            this.Shown += new System.EventHandler(this.ListaClientes_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVClientes;
        private System.Windows.Forms.LinkLabel lbAgregarCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn RFC;
        private System.Windows.Forms.DataGridViewTextBoxColumn RazonSocial;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumeroCliente;
        private System.Windows.Forms.DataGridViewImageColumn Agregar;
        private System.Windows.Forms.TextBox txtBuscador;
        private System.Windows.Forms.Label lbBuscar;
        private System.Windows.Forms.Button btnPublicoG;
    }
}