namespace PuntoDeVentaV2
{
    partial class ConsultarListaRelacionados
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
            this.DGVProdServCombo = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDServicio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ServicioCombo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Eliminar = new System.Windows.Forms.DataGridViewImageColumn();
            this.lblTitulo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProdServCombo)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVProdServCombo
            // 
            this.DGVProdServCombo.AllowUserToAddRows = false;
            this.DGVProdServCombo.AllowUserToDeleteRows = false;
            this.DGVProdServCombo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVProdServCombo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVProdServCombo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Fecha,
            this.IDServicio,
            this.ServicioCombo,
            this.IDProducto,
            this.NombreProducto,
            this.Cantidad,
            this.Type,
            this.Eliminar});
            this.DGVProdServCombo.Location = new System.Drawing.Point(12, 53);
            this.DGVProdServCombo.Name = "DGVProdServCombo";
            this.DGVProdServCombo.ReadOnly = true;
            this.DGVProdServCombo.RowHeadersVisible = false;
            this.DGVProdServCombo.Size = new System.Drawing.Size(670, 249);
            this.DGVProdServCombo.TabIndex = 0;
            this.DGVProdServCombo.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVProdServCombo_CellClick);
            this.DGVProdServCombo.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVProdServCombo_CellEndEdit);
            this.DGVProdServCombo.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVProdServCombo_CellMouseEnter);
            this.DGVProdServCombo.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.DGVProdServCombo_EditingControlShowing);
            this.DGVProdServCombo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DGVProdServCombo_MouseUp);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // IDServicio
            // 
            this.IDServicio.HeaderText = "IDServicio";
            this.IDServicio.Name = "IDServicio";
            this.IDServicio.ReadOnly = true;
            // 
            // ServicioCombo
            // 
            this.ServicioCombo.HeaderText = "Concepto";
            this.ServicioCombo.Name = "ServicioCombo";
            this.ServicioCombo.ReadOnly = true;
            // 
            // IDProducto
            // 
            this.IDProducto.HeaderText = "IDProducto";
            this.IDProducto.Name = "IDProducto";
            this.IDProducto.ReadOnly = true;
            // 
            // NombreProducto
            // 
            this.NombreProducto.HeaderText = "Concepto";
            this.NombreProducto.Name = "NombreProducto";
            this.NombreProducto.ReadOnly = true;
            // 
            // Cantidad
            // 
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.HeaderText = "Tipo (Combo / Servicio)";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // Eliminar
            // 
            this.Eliminar.HeaderText = "Eliminar";
            this.Eliminar.Name = "Eliminar";
            this.Eliminar.ReadOnly = true;
            // 
            // lblTitulo
            // 
            this.lblTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.Location = new System.Drawing.Point(12, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(670, 23);
            this.lblTitulo.TabIndex = 1;
            this.lblTitulo.Text = "label1";
            this.lblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConsultarListaRelacionados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(694, 329);
            this.Controls.Add(this.lblTitulo);
            this.Controls.Add(this.DGVProdServCombo);
            this.MaximizeBox = false;
            this.Name = "ConsultarListaRelacionados";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ConsultarListaRelacionados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVProdServCombo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVProdServCombo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDServicio;
        private System.Windows.Forms.DataGridViewTextBoxColumn ServicioCombo;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewImageColumn Eliminar;
    }
}