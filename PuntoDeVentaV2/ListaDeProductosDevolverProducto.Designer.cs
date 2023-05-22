
namespace PuntoDeVentaV2
{
    partial class ListaDeProductosDevolverProducto
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
            this.DGVProductos = new System.Windows.Forms.DataGridView();
            this.Devolver = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.btnAceptar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVProductos
            // 
            this.DGVProductos.AllowUserToAddRows = false;
            this.DGVProductos.AllowUserToDeleteRows = false;
            this.DGVProductos.AllowUserToOrderColumns = true;
            this.DGVProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGVProductos.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.DGVProductos.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.DGVProductos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.DGVProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Devolver});
            this.DGVProductos.Location = new System.Drawing.Point(1, 0);
            this.DGVProductos.Name = "DGVProductos";
            this.DGVProductos.RowHeadersVisible = false;
            this.DGVProductos.Size = new System.Drawing.Size(636, 304);
            this.DGVProductos.TabIndex = 0;
            this.DGVProductos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVProductos_CellContentClick);
            // 
            // Devolver
            // 
            this.Devolver.HeaderText = "Devolver";
            this.Devolver.Name = "Devolver";
            this.Devolver.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Devolver.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Devolver.Width = 75;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAceptar.BackColor = System.Drawing.Color.Green;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(256, 310);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(125, 30);
            this.btnAceptar.TabIndex = 6;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // ListaDeProductosDevolverProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(638, 341);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.DGVProductos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ListaDeProductosDevolverProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ListaDeProductosDevolverProducto";
            this.Load += new System.EventHandler(this.ListaDeProductosDevolverProducto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVProductos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVProductos;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Devolver;
    }
}