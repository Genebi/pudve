namespace PuntoDeVentaV2
{
    partial class HistorialPrecioBuscador
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
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.DGVDatosEmpleados = new System.Windows.Forms.DataGridView();
            this.DGVDatosProductos = new System.Windows.Forms.DataGridView();
            this.checkBoxProd = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IDProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoBarras = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnGenerarReporte = new System.Windows.Forms.Button();
            this.checkBox = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGVDatosEmpleados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVDatosProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(27, 79);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(512, 20);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.txtBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyDown);
            // 
            // lbTitulo
            // 
            this.lbTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.Location = new System.Drawing.Point(223, 9);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(246, 23);
            this.lbTitulo.TabIndex = 1;
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::PuntoDeVentaV2.Properties.Resources.search;
            this.btnBuscar.Location = new System.Drawing.Point(561, 79);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 2;
            this.btnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.btnBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnBuscar_KeyDown);
            // 
            // DGVDatosEmpleados
            // 
            this.DGVDatosEmpleados.AllowUserToAddRows = false;
            this.DGVDatosEmpleados.AllowUserToDeleteRows = false;
            this.DGVDatosEmpleados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVDatosEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVDatosEmpleados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkBox,
            this.Nombre,
            this.Id});
            this.DGVDatosEmpleados.Location = new System.Drawing.Point(27, 128);
            this.DGVDatosEmpleados.Name = "DGVDatosEmpleados";
            this.DGVDatosEmpleados.ReadOnly = true;
            this.DGVDatosEmpleados.RowHeadersVisible = false;
            this.DGVDatosEmpleados.Size = new System.Drawing.Size(609, 225);
            this.DGVDatosEmpleados.TabIndex = 3;
            this.DGVDatosEmpleados.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVDatosEmpleados_CellClick);
            this.DGVDatosEmpleados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVDatosEmpleados_CellContentClick);
            this.DGVDatosEmpleados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVDatos_CellDoubleClick);
            // 
            // DGVDatosProductos
            // 
            this.DGVDatosProductos.AllowUserToAddRows = false;
            this.DGVDatosProductos.AllowUserToDeleteRows = false;
            this.DGVDatosProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVDatosProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVDatosProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.checkBoxProd,
            this.IDProducto,
            this.NombreProducto,
            this.Stock,
            this.CodigoBarras,
            this.Status,
            this.tipo});
            this.DGVDatosProductos.Location = new System.Drawing.Point(27, 128);
            this.DGVDatosProductos.Name = "DGVDatosProductos";
            this.DGVDatosProductos.ReadOnly = true;
            this.DGVDatosProductos.RowHeadersVisible = false;
            this.DGVDatosProductos.Size = new System.Drawing.Size(609, 225);
            this.DGVDatosProductos.TabIndex = 4;
            this.DGVDatosProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVDatosProductos_CellClick);
            this.DGVDatosProductos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVDatosProductos_CellContentClick);
            // 
            // checkBoxProd
            // 
            this.checkBoxProd.FillWeight = 31.33294F;
            this.checkBoxProd.HeaderText = "";
            this.checkBoxProd.Name = "checkBoxProd";
            this.checkBoxProd.ReadOnly = true;
            // 
            // IDProducto
            // 
            this.IDProducto.HeaderText = "ID";
            this.IDProducto.Name = "IDProducto";
            this.IDProducto.ReadOnly = true;
            this.IDProducto.Visible = false;
            // 
            // NombreProducto
            // 
            this.NombreProducto.HeaderText = "Nombre";
            this.NombreProducto.Name = "NombreProducto";
            this.NombreProducto.ReadOnly = true;
            // 
            // Stock
            // 
            this.Stock.HeaderText = "Stock";
            this.Stock.Name = "Stock";
            this.Stock.ReadOnly = true;
            // 
            // CodigoBarras
            // 
            this.CodigoBarras.HeaderText = "Codigo de Barras";
            this.CodigoBarras.Name = "CodigoBarras";
            this.CodigoBarras.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Estado";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // tipo
            // 
            this.tipo.HeaderText = "Tipo";
            this.tipo.Name = "tipo";
            this.tipo.ReadOnly = true;
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.AutoSize = true;
            this.btnGenerarReporte.Location = new System.Drawing.Point(540, 359);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(96, 23);
            this.btnGenerarReporte.TabIndex = 5;
            this.btnGenerarReporte.Text = "Generar Reporte";
            this.btnGenerarReporte.UseVisualStyleBackColor = true;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // checkBox
            // 
            this.checkBox.FillWeight = 31.33294F;
            this.checkBox.HeaderText = "";
            this.checkBox.Name = "checkBox";
            this.checkBox.ReadOnly = true;
            // 
            // Nombre
            // 
            this.Nombre.FillWeight = 138.6483F;
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // Id
            // 
            this.Id.FillWeight = 138.6483F;
            this.Id.HeaderText = "Numero Empleado";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            // 
            // HistorialPrecioBuscador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 390);
            this.Controls.Add(this.btnGenerarReporte);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.lbTitulo);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.DGVDatosEmpleados);
            this.Controls.Add(this.DGVDatosProductos);
            this.Name = "HistorialPrecioBuscador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HistorialPrecioBuscador";
            this.Load += new System.EventHandler(this.HistorialPrecioBuscador_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVDatosEmpleados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVDatosProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.DataGridView DGVDatosEmpleados;
        private System.Windows.Forms.DataGridView DGVDatosProductos;
        private System.Windows.Forms.Button btnGenerarReporte;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkBoxProd;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoBarras;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipo;
        private System.Windows.Forms.DataGridViewCheckBoxColumn checkBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
    }
}