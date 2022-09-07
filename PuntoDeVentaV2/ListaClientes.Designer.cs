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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.btnBucarCliente = new System.Windows.Forms.Button();
            this.btnPublicoGeneral = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.lblCliente = new System.Windows.Forms.LinkLabel();
            this.CBXConDescuento = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGVClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVClientes
            // 
            this.DGVClientes.AllowUserToAddRows = false;
            this.DGVClientes.AllowUserToDeleteRows = false;
            this.DGVClientes.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DGVClientes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle19.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
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
            dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVClientes.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
            this.DGVClientes.RowHeadersVisible = false;
            dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DGVClientes.RowsDefaultCellStyle = dataGridViewCellStyle24;
            this.DGVClientes.Size = new System.Drawing.Size(579, 168);
            this.DGVClientes.TabIndex = 0;
            this.DGVClientes.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVClientes_CellClick);
            this.DGVClientes.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVClientes_CellMouseEnter);
            this.DGVClientes.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVClientes_CellMouseLeave);
            this.DGVClientes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGVClientes_KeyDown);
            // 
            // ID
            // 
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ID.DefaultCellStyle = dataGridViewCellStyle20;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // RFC
            // 
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RFC.DefaultCellStyle = dataGridViewCellStyle21;
            this.RFC.HeaderText = "RFC";
            this.RFC.Name = "RFC";
            this.RFC.ReadOnly = true;
            this.RFC.Width = 150;
            // 
            // RazonSocial
            // 
            this.RazonSocial.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RazonSocial.DefaultCellStyle = dataGridViewCellStyle22;
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
            this.lbAgregarCliente.Location = new System.Drawing.Point(443, 31);
            this.lbAgregarCliente.Name = "lbAgregarCliente";
            this.lbAgregarCliente.Size = new System.Drawing.Size(152, 17);
            this.lbAgregarCliente.TabIndex = 1;
            this.lbAgregarCliente.TabStop = true;
            this.lbAgregarCliente.Text = "Agregar nuevo cliente";
            this.lbAgregarCliente.Visible = false;
            this.lbAgregarCliente.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbAgregarCliente_LinkClicked);
            // 
            // txtBuscador
            // 
            this.txtBuscador.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscador.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscador.Location = new System.Drawing.Point(121, 32);
            this.txtBuscador.Name = "txtBuscador";
            this.txtBuscador.Size = new System.Drawing.Size(306, 23);
            this.txtBuscador.TabIndex = 2;
            this.txtBuscador.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBuscador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscador_KeyDown);
            // 
            // lbBuscar
            // 
            this.lbBuscar.AutoSize = true;
            this.lbBuscar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBuscar.Location = new System.Drawing.Point(19, 35);
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
            this.btnPublicoG.Visible = false;
            this.btnPublicoG.Click += new System.EventHandler(this.btnPublicoG_Click);
            // 
            // btnBucarCliente
            // 
            this.btnBucarCliente.Image = global::PuntoDeVentaV2.Properties.Resources.search;
            this.btnBucarCliente.Location = new System.Drawing.Point(428, 32);
            this.btnBucarCliente.Name = "btnBucarCliente";
            this.btnBucarCliente.Size = new System.Drawing.Size(25, 24);
            this.btnBucarCliente.TabIndex = 114;
            this.btnBucarCliente.UseVisualStyleBackColor = true;
            this.btnBucarCliente.Click += new System.EventHandler(this.btnBucarCliente_Click);
            // 
            // btnPublicoGeneral
            // 
            this.btnPublicoGeneral.BackColor = System.Drawing.Color.Green;
            this.btnPublicoGeneral.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPublicoGeneral.FlatAppearance.BorderSize = 0;
            this.btnPublicoGeneral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPublicoGeneral.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPublicoGeneral.ForeColor = System.Drawing.Color.White;
            this.btnPublicoGeneral.Location = new System.Drawing.Point(336, 264);
            this.btnPublicoGeneral.Name = "btnPublicoGeneral";
            this.btnPublicoGeneral.Size = new System.Drawing.Size(118, 25);
            this.btnPublicoGeneral.TabIndex = 116;
            this.btnPublicoGeneral.Text = "Público general";
            this.btnPublicoGeneral.UseVisualStyleBackColor = false;
            this.btnPublicoGeneral.Click += new System.EventHandler(this.btnPublicoGeneral_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Green;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(98, 264);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(154, 25);
            this.button1.TabIndex = 117;
            this.button1.Text = "Agregar nuevo cliente";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Location = new System.Drawing.Point(12, 68);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(55, 13);
            this.lblCliente.TabIndex = 118;
            this.lblCliente.TabStop = true;
            this.lblCliente.Text = "linkLabel1";
            this.lblCliente.Visible = false;
            this.lblCliente.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // CBXConDescuento
            // 
            this.CBXConDescuento.AutoSize = true;
            this.CBXConDescuento.Location = new System.Drawing.Point(442, 64);
            this.CBXConDescuento.Name = "CBXConDescuento";
            this.CBXConDescuento.Size = new System.Drawing.Size(139, 17);
            this.CBXConDescuento.TabIndex = 119;
            this.CBXConDescuento.Text = "Clientes con Descuento";
            this.CBXConDescuento.UseVisualStyleBackColor = true;
            this.CBXConDescuento.Visible = false;
            this.CBXConDescuento.CheckedChanged += new System.EventHandler(this.CBXConDescuento_CheckedChanged);
            // 
            // ListaClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 311);
            this.Controls.Add(this.CBXConDescuento);
            this.Controls.Add(this.lblCliente);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnPublicoGeneral);
            this.Controls.Add(this.btnBucarCliente);
            this.Controls.Add(this.btnPublicoG);
            this.Controls.Add(this.lbBuscar);
            this.Controls.Add(this.txtBuscador);
            this.Controls.Add(this.lbAgregarCliente);
            this.Controls.Add(this.DGVClientes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "ListaClientes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Lista de Clientes";
            this.Load += new System.EventHandler(this.ListaClientes_Load);
            this.Shown += new System.EventHandler(this.ListaClientes_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ListaClientes_KeyDown);
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
        private System.Windows.Forms.Button btnBucarCliente;
        private System.Windows.Forms.Button btnPublicoGeneral;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.LinkLabel lblCliente;
        private System.Windows.Forms.CheckBox CBXConDescuento;
    }
}