
namespace PuntoDeVentaV2
{
    partial class traspaso
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(traspaso));
            this.label1 = new System.Windows.Forms.Label();
            this.DGVTraspaso = new System.Windows.Forms.DataGridView();
            this.NombreT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CantidadT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ajuste = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGVTraspaso)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1123, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ajuste de productos a base local";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DGVTraspaso
            // 
            this.DGVTraspaso.AllowUserToAddRows = false;
            this.DGVTraspaso.AllowUserToDeleteRows = false;
            this.DGVTraspaso.AllowUserToResizeColumns = false;
            this.DGVTraspaso.AllowUserToResizeRows = false;
            this.DGVTraspaso.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.DGVTraspaso.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVTraspaso.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NombreT,
            this.CodigoT,
            this.CantidadT,
            this.NombreL,
            this.CodigoL,
            this.PCompra,
            this.PVenta,
            this.Ajuste});
            this.DGVTraspaso.Location = new System.Drawing.Point(12, 128);
            this.DGVTraspaso.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DGVTraspaso.Name = "DGVTraspaso";
            this.DGVTraspaso.RowHeadersVisible = false;
            this.DGVTraspaso.RowHeadersWidth = 62;
            this.DGVTraspaso.Size = new System.Drawing.Size(1105, 399);
            this.DGVTraspaso.TabIndex = 10;
            this.DGVTraspaso.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVTraspaso_CellValueChanged);
            // 
            // NombreT
            // 
            this.NombreT.DataPropertyName = "Nombre";
            this.NombreT.HeaderText = "Nombre";
            this.NombreT.MinimumWidth = 8;
            this.NombreT.Name = "NombreT";
            this.NombreT.ReadOnly = true;
            this.NombreT.Width = 200;
            // 
            // CodigoT
            // 
            this.CodigoT.DataPropertyName = "Codigo";
            this.CodigoT.HeaderText = "Código de barras";
            this.CodigoT.MinimumWidth = 8;
            this.CodigoT.Name = "CodigoT";
            this.CodigoT.ReadOnly = true;
            this.CodigoT.Width = 130;
            // 
            // CantidadT
            // 
            this.CantidadT.DataPropertyName = "Cantidad";
            this.CantidadT.HeaderText = "Cantidad";
            this.CantidadT.MinimumWidth = 8;
            this.CantidadT.Name = "CantidadT";
            this.CantidadT.ReadOnly = true;
            this.CantidadT.Width = 80;
            // 
            // NombreL
            // 
            this.NombreL.HeaderText = "Nombre";
            this.NombreL.MinimumWidth = 8;
            this.NombreL.Name = "NombreL";
            this.NombreL.ReadOnly = true;
            this.NombreL.Width = 200;
            // 
            // CodigoL
            // 
            this.CodigoL.HeaderText = "Código de barras";
            this.CodigoL.MinimumWidth = 8;
            this.CodigoL.Name = "CodigoL";
            this.CodigoL.ReadOnly = true;
            this.CodigoL.Width = 130;
            // 
            // PCompra
            // 
            this.PCompra.HeaderText = "Precio de compra";
            this.PCompra.MinimumWidth = 8;
            this.PCompra.Name = "PCompra";
            this.PCompra.ReadOnly = true;
            this.PCompra.Width = 120;
            // 
            // PVenta
            // 
            this.PVenta.HeaderText = "Precio de venta";
            this.PVenta.MinimumWidth = 8;
            this.PVenta.Name = "PVenta";
            this.PVenta.ReadOnly = true;
            this.PVenta.Width = 120;
            // 
            // Ajuste
            // 
            this.Ajuste.HeaderText = "Ajuste";
            this.Ajuste.Items.AddRange(new object[] {
            "Auto",
            "Manual",
            "Omitir"});
            this.Ajuste.MaxDropDownItems = 3;
            this.Ajuste.MinimumWidth = 8;
            this.Ajuste.Name = "Ajuste";
            this.Ajuste.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Ajuste.Width = 120;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(414, 42);
            this.label2.TabIndex = 11;
            this.label2.Text = "Datos de traspaso";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(423, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(694, 42);
            this.label3.TabIndex = 11;
            this.label3.Text = "Datos locales";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // traspaso
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1135, 566);
            this.Controls.Add(this.DGVTraspaso);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "traspaso";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Traspaso";
            this.Load += new System.EventHandler(this.traspaso_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVTraspaso)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView DGVTraspaso;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadT;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreL;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoL;
        private System.Windows.Forms.DataGridViewTextBoxColumn PCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn PVenta;
        private System.Windows.Forms.DataGridViewComboBoxColumn Ajuste;
    }
}