namespace PuntoDeVentaV2
{
    partial class WinQueryString
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fLPDetalleProducto = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbTipoFiltroStock = new System.Windows.Forms.ComboBox();
            this.txtCantStock = new System.Windows.Forms.TextBox();
            this.txtCantPrecio = new System.Windows.Forms.TextBox();
            this.cbTipoFiltroPrecio = new System.Windows.Forms.ComboBox();
            this.comboBoxProveedor = new System.Windows.Forms.ComboBox();
            this.chckBoxProveedor = new System.Windows.Forms.CheckBox();
            this.chkBoxPrecio = new System.Windows.Forms.CheckBox();
            this.chkBoxStock = new System.Windows.Forms.CheckBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fLPDetalleProducto);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.comboBoxProveedor);
            this.groupBox1.Controls.Add(this.chckBoxProveedor);
            this.groupBox1.Controls.Add(this.chkBoxPrecio);
            this.groupBox1.Controls.Add(this.chkBoxStock);
            this.groupBox1.Location = new System.Drawing.Point(27, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 276);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Seleccion de Filtrado: ";
            // 
            // fLPDetalleProducto
            // 
            this.fLPDetalleProducto.AutoScroll = true;
            this.fLPDetalleProducto.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLPDetalleProducto.Location = new System.Drawing.Point(6, 167);
            this.fLPDetalleProducto.Name = "fLPDetalleProducto";
            this.fLPDetalleProducto.Size = new System.Drawing.Size(486, 100);
            this.fLPDetalleProducto.TabIndex = 11;
            this.fLPDetalleProducto.WrapContents = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(58, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(385, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "De Proveedor en delante esta en construccion";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbTipoFiltroStock);
            this.panel1.Controls.Add(this.txtCantStock);
            this.panel1.Controls.Add(this.txtCantPrecio);
            this.panel1.Controls.Add(this.cbTipoFiltroPrecio);
            this.panel1.Location = new System.Drawing.Point(115, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(377, 79);
            this.panel1.TabIndex = 8;
            // 
            // cbTipoFiltroStock
            // 
            this.cbTipoFiltroStock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbTipoFiltroStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoFiltroStock.FormattingEnabled = true;
            this.cbTipoFiltroStock.Items.AddRange(new object[] {
            "No Aplica",
            "Mayor o Igual Que",
            "Menor o Igual Que",
            "Igual Que",
            "Mayor Que",
            "Menor Que"});
            this.cbTipoFiltroStock.Location = new System.Drawing.Point(16, 8);
            this.cbTipoFiltroStock.Name = "cbTipoFiltroStock";
            this.cbTipoFiltroStock.Size = new System.Drawing.Size(240, 21);
            this.cbTipoFiltroStock.TabIndex = 1;
            this.cbTipoFiltroStock.SelectedIndexChanged += new System.EventHandler(this.cbTipoFiltroStock_SelectedIndexChanged);
            this.cbTipoFiltroStock.Click += new System.EventHandler(this.cbTipoFiltroStock_Click);
            // 
            // txtCantStock
            // 
            this.txtCantStock.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtCantStock.Location = new System.Drawing.Point(275, 6);
            this.txtCantStock.Name = "txtCantStock";
            this.txtCantStock.Size = new System.Drawing.Size(77, 23);
            this.txtCantStock.TabIndex = 2;
            this.txtCantStock.Text = "0.0";
            this.txtCantStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCantStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantStock_KeyPress);
            // 
            // txtCantPrecio
            // 
            this.txtCantPrecio.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtCantPrecio.Location = new System.Drawing.Point(275, 42);
            this.txtCantPrecio.Name = "txtCantPrecio";
            this.txtCantPrecio.Size = new System.Drawing.Size(77, 23);
            this.txtCantPrecio.TabIndex = 5;
            this.txtCantPrecio.Text = "0.0";
            this.txtCantPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCantPrecio.Click += new System.EventHandler(this.txtCantPrecio_Click);
            this.txtCantPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantPrecio_KeyPress);
            // 
            // cbTipoFiltroPrecio
            // 
            this.cbTipoFiltroPrecio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoFiltroPrecio.FormattingEnabled = true;
            this.cbTipoFiltroPrecio.Items.AddRange(new object[] {
            "No Aplica",
            "Mayor o Igual Que",
            "Menor o Igual Que",
            "Igual Que",
            "Mayor Que",
            "Menor Que"});
            this.cbTipoFiltroPrecio.Location = new System.Drawing.Point(16, 44);
            this.cbTipoFiltroPrecio.Name = "cbTipoFiltroPrecio";
            this.cbTipoFiltroPrecio.Size = new System.Drawing.Size(240, 21);
            this.cbTipoFiltroPrecio.TabIndex = 4;
            this.cbTipoFiltroPrecio.SelectedIndexChanged += new System.EventHandler(this.cbTipoFiltroPrecio_SelectedIndexChanged);
            this.cbTipoFiltroPrecio.Click += new System.EventHandler(this.cbTipoFiltroPrecio_Click);
            // 
            // comboBoxProveedor
            // 
            this.comboBoxProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProveedor.FormattingEnabled = true;
            this.comboBoxProveedor.Location = new System.Drawing.Point(131, 142);
            this.comboBoxProveedor.Name = "comboBoxProveedor";
            this.comboBoxProveedor.Size = new System.Drawing.Size(336, 21);
            this.comboBoxProveedor.TabIndex = 7;
            this.comboBoxProveedor.SelectedIndexChanged += new System.EventHandler(this.comboBoxProveedor_SelectedIndexChanged);
            // 
            // chckBoxProveedor
            // 
            this.chckBoxProveedor.AutoSize = true;
            this.chckBoxProveedor.Location = new System.Drawing.Point(32, 144);
            this.chckBoxProveedor.Name = "chckBoxProveedor";
            this.chckBoxProveedor.Size = new System.Drawing.Size(75, 17);
            this.chckBoxProveedor.TabIndex = 6;
            this.chckBoxProveedor.Text = "Proveedor";
            this.chckBoxProveedor.UseVisualStyleBackColor = true;
            this.chckBoxProveedor.CheckedChanged += new System.EventHandler(this.chckBoxProveedor_CheckedChanged);
            // 
            // chkBoxPrecio
            // 
            this.chkBoxPrecio.AutoSize = true;
            this.chkBoxPrecio.Location = new System.Drawing.Point(32, 65);
            this.chkBoxPrecio.Name = "chkBoxPrecio";
            this.chkBoxPrecio.Size = new System.Drawing.Size(56, 17);
            this.chkBoxPrecio.TabIndex = 3;
            this.chkBoxPrecio.Text = "Precio";
            this.chkBoxPrecio.UseVisualStyleBackColor = true;
            this.chkBoxPrecio.CheckedChanged += new System.EventHandler(this.chkBoxPrecio_CheckedChanged);
            // 
            // chkBoxStock
            // 
            this.chkBoxStock.AutoSize = true;
            this.chkBoxStock.Location = new System.Drawing.Point(32, 29);
            this.chkBoxStock.Name = "chkBoxStock";
            this.chkBoxStock.Size = new System.Drawing.Size(54, 17);
            this.chkBoxStock.TabIndex = 0;
            this.chkBoxStock.Text = "Stock";
            this.chkBoxStock.UseVisualStyleBackColor = true;
            this.chkBoxStock.CheckedChanged += new System.EventHandler(this.chkBoxStock_CheckedChanged);
            // 
            // btnAplicar
            // 
            this.btnAplicar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnAplicar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAplicar.FlatAppearance.BorderSize = 0;
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicar.ForeColor = System.Drawing.Color.White;
            this.btnAplicar.Location = new System.Drawing.Point(430, 305);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(95, 43);
            this.btnAplicar.TabIndex = 6;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // btnClean
            // 
            this.btnClean.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnClean.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnClean.FlatAppearance.BorderSize = 0;
            this.btnClean.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClean.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClean.ForeColor = System.Drawing.Color.White;
            this.btnClean.Location = new System.Drawing.Point(223, 305);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(95, 43);
            this.btnClean.TabIndex = 7;
            this.btnClean.Text = "Limpiar";
            this.btnClean.UseVisualStyleBackColor = false;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.DarkGray;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Century", 12F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(27, 305);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(95, 43);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // WinQueryString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 396);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinQueryString";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro Avanzado de Productos";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WinQueryString_FormClosed);
            this.Load += new System.EventHandler(this.WinQueryString_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCantStock;
        private System.Windows.Forms.CheckBox chkBoxStock;
        private System.Windows.Forms.ComboBox cbTipoFiltroStock;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnClean;
        private System.Windows.Forms.CheckBox chkBoxPrecio;
        private System.Windows.Forms.TextBox txtCantPrecio;
        private System.Windows.Forms.ComboBox cbTipoFiltroPrecio;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.CheckBox chckBoxProveedor;
        private System.Windows.Forms.ComboBox comboBoxProveedor;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel fLPDetalleProducto;
    }
}