namespace PuntoDeVentaV2
{
    partial class ListaProductos
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtBoxSearchProd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.DGVStockProductos = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVStockProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtBoxSearchProd);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(410, 49);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(151, 52);
            this.panel1.TabIndex = 0;
            // 
            // txtBoxSearchProd
            // 
            this.txtBoxSearchProd.Location = new System.Drawing.Point(14, 23);
            this.txtBoxSearchProd.Margin = new System.Windows.Forms.Padding(2);
            this.txtBoxSearchProd.Name = "txtBoxSearchProd";
            this.txtBoxSearchProd.Size = new System.Drawing.Size(122, 20);
            this.txtBoxSearchProd.TabIndex = 1;
            this.txtBoxSearchProd.TextChanged += new System.EventHandler(this.txtBoxSearchProd_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Producto:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Century", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label2.Location = new System.Drawing.Point(14, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(959, 37);
            this.label2.TabIndex = 1;
            this.label2.Text = "Stock de Productos existente";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.DGVStockProductos);
            this.panel2.Location = new System.Drawing.Point(14, 117);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(959, 526);
            this.panel2.TabIndex = 2;
            // 
            // DGVStockProductos
            // 
            this.DGVStockProductos.AllowUserToAddRows = false;
            this.DGVStockProductos.AllowUserToDeleteRows = false;
            this.DGVStockProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVStockProductos.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DGVStockProductos.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.DGVStockProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVStockProductos.Location = new System.Drawing.Point(11, 11);
            this.DGVStockProductos.Margin = new System.Windows.Forms.Padding(2);
            this.DGVStockProductos.Name = "DGVStockProductos";
            this.DGVStockProductos.ReadOnly = true;
            this.DGVStockProductos.RowTemplate.Height = 24;
            this.DGVStockProductos.Size = new System.Drawing.Size(934, 504);
            this.DGVStockProductos.TabIndex = 0;
            this.DGVStockProductos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVStockProductos_CellDoubleClick);
            // 
            // ListaProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 657);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListaProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ListaProductos";
            this.Load += new System.EventHandler(this.ListaProductos_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVStockProductos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtBoxSearchProd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView DGVStockProductos;
    }
}