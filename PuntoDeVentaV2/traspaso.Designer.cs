
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
            this.spacer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CodigoL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ajuste = new System.Windows.Forms.DataGridViewImageColumn();
            this.Omitir = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.bntTerminar = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGVTraspaso)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1105, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "AJUSTE DE PRODUCTOS A BASE LOCAL";
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
            this.spacer,
            this.NombreL,
            this.CodigoL,
            this.PCompra,
            this.PVenta,
            this.Ajuste,
            this.Omitir});
            this.DGVTraspaso.Location = new System.Drawing.Point(28, 136);
            this.DGVTraspaso.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.DGVTraspaso.MultiSelect = false;
            this.DGVTraspaso.Name = "DGVTraspaso";
            this.DGVTraspaso.RowHeadersVisible = false;
            this.DGVTraspaso.RowHeadersWidth = 62;
            this.DGVTraspaso.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGVTraspaso.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DGVTraspaso.Size = new System.Drawing.Size(1131, 399);
            this.DGVTraspaso.TabIndex = 10;
            this.DGVTraspaso.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVTraspaso_CellClick_1);
            // 
            // NombreT
            // 
            this.NombreT.DataPropertyName = "Nombre";
            this.NombreT.Frozen = true;
            this.NombreT.HeaderText = "Nombre";
            this.NombreT.MinimumWidth = 8;
            this.NombreT.Name = "NombreT";
            this.NombreT.ReadOnly = true;
            this.NombreT.Width = 200;
            // 
            // CodigoT
            // 
            this.CodigoT.DataPropertyName = "Codigo";
            this.CodigoT.Frozen = true;
            this.CodigoT.HeaderText = "Código de barras";
            this.CodigoT.MinimumWidth = 8;
            this.CodigoT.Name = "CodigoT";
            this.CodigoT.ReadOnly = true;
            this.CodigoT.Width = 130;
            // 
            // CantidadT
            // 
            this.CantidadT.DataPropertyName = "Cantidad";
            this.CantidadT.Frozen = true;
            this.CantidadT.HeaderText = "Cantidad";
            this.CantidadT.MinimumWidth = 8;
            this.CantidadT.Name = "CantidadT";
            this.CantidadT.ReadOnly = true;
            this.CantidadT.Width = 80;
            // 
            // spacer
            // 
            this.spacer.Frozen = true;
            this.spacer.HeaderText = "";
            this.spacer.MinimumWidth = 8;
            this.spacer.Name = "spacer";
            this.spacer.ReadOnly = true;
            this.spacer.Width = 20;
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
            this.Ajuste.Image = global::PuntoDeVentaV2.Properties.Resources.search1;
            this.Ajuste.MinimumWidth = 8;
            this.Ajuste.Name = "Ajuste";
            this.Ajuste.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Ajuste.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Ajuste.Width = 65;
            // 
            // Omitir
            // 
            this.Omitir.DataPropertyName = "Omitir";
            this.Omitir.HeaderText = "Omitir";
            this.Omitir.MinimumWidth = 8;
            this.Omitir.Name = "Omitir";
            this.Omitir.Width = 65;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Yellow;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(28, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(413, 49);
            this.label2.TabIndex = 11;
            this.label2.Text = "Sucursal Origen";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Gold;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(456, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(703, 49);
            this.label3.TabIndex = 11;
            this.label3.Text = "Sucursal Destino";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.BackColor = System.Drawing.Color.Green;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(934, 545);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(225, 46);
            this.btnAceptar.TabIndex = 107;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // bntTerminar
            // 
            this.bntTerminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bntTerminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.bntTerminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntTerminar.FlatAppearance.BorderSize = 0;
            this.bntTerminar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.bntTerminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.bntTerminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntTerminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntTerminar.ForeColor = System.Drawing.Color.White;
            this.bntTerminar.Location = new System.Drawing.Point(28, 545);
            this.bntTerminar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bntTerminar.Name = "bntTerminar";
            this.bntTerminar.Size = new System.Drawing.Size(225, 46);
            this.bntTerminar.TabIndex = 106;
            this.bntTerminar.Text = "Cancelar";
            this.bntTerminar.UseVisualStyleBackColor = false;
            this.bntTerminar.Click += new System.EventHandler(this.bntTerminar_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Location = new System.Drawing.Point(435, 91);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 49);
            this.label5.TabIndex = 109;
            // 
            // traspaso
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 605);
            this.Controls.Add(this.DGVTraspaso);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.bntTerminar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
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
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button bntTerminar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoT;
        private System.Windows.Forms.DataGridViewTextBoxColumn CantidadT;
        private System.Windows.Forms.DataGridViewTextBoxColumn spacer;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreL;
        private System.Windows.Forms.DataGridViewTextBoxColumn CodigoL;
        private System.Windows.Forms.DataGridViewTextBoxColumn PCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn PVenta;
        private System.Windows.Forms.DataGridViewImageColumn Ajuste;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Omitir;
    }
}