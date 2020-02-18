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
            this.chkBoxTipo = new System.Windows.Forms.CheckBox();
            this.fLPDetalleProducto = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbTipoFiltroCombProdServ = new System.Windows.Forms.ComboBox();
            this.cbTipoFiltroStock = new System.Windows.Forms.ComboBox();
            this.txtCantStock = new System.Windows.Forms.TextBox();
            this.txtCantPrecio = new System.Windows.Forms.TextBox();
            this.cbTipoFiltroPrecio = new System.Windows.Forms.ComboBox();
            this.chkBoxPrecio = new System.Windows.Forms.CheckBox();
            this.chkBoxStock = new System.Windows.Forms.CheckBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox2);
            this.groupBox1.Controls.Add(this.checkBox1);
            this.groupBox1.Controls.Add(this.chkBoxTipo);
            this.groupBox1.Controls.Add(this.fLPDetalleProducto);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.chkBoxPrecio);
            this.groupBox1.Controls.Add(this.chkBoxStock);
            this.groupBox1.Location = new System.Drawing.Point(27, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(524, 347);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuració de Filtrado: ";
            // 
            // chkBoxTipo
            // 
            this.chkBoxTipo.AutoSize = true;
            this.chkBoxTipo.Location = new System.Drawing.Point(19, 131);
            this.chkBoxTipo.Name = "chkBoxTipo";
            this.chkBoxTipo.Size = new System.Drawing.Size(47, 17);
            this.chkBoxTipo.TabIndex = 12;
            this.chkBoxTipo.Text = "Tipo";
            this.chkBoxTipo.UseVisualStyleBackColor = true;
            this.chkBoxTipo.CheckedChanged += new System.EventHandler(this.chkBoxTipo_CheckedChanged);
            // 
            // fLPDetalleProducto
            // 
            this.fLPDetalleProducto.AutoScroll = true;
            this.fLPDetalleProducto.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLPDetalleProducto.Location = new System.Drawing.Point(6, 191);
            this.fLPDetalleProducto.Name = "fLPDetalleProducto";
            this.fLPDetalleProducto.Size = new System.Drawing.Size(512, 150);
            this.fLPDetalleProducto.TabIndex = 11;
            this.fLPDetalleProducto.WrapContents = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.cbTipoFiltroCombProdServ);
            this.panel1.Controls.Add(this.cbTipoFiltroStock);
            this.panel1.Controls.Add(this.txtCantStock);
            this.panel1.Controls.Add(this.txtCantPrecio);
            this.panel1.Controls.Add(this.cbTipoFiltroPrecio);
            this.panel1.Location = new System.Drawing.Point(90, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(428, 172);
            this.panel1.TabIndex = 8;
            // 
            // cbTipoFiltroCombProdServ
            // 
            this.cbTipoFiltroCombProdServ.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoFiltroCombProdServ.FormattingEnabled = true;
            this.cbTipoFiltroCombProdServ.Items.AddRange(new object[] {
            "No Aplica",
            "Combo",
            "Producto",
            "Servicio"});
            this.cbTipoFiltroCombProdServ.Location = new System.Drawing.Point(38, 110);
            this.cbTipoFiltroCombProdServ.Name = "cbTipoFiltroCombProdServ";
            this.cbTipoFiltroCombProdServ.Size = new System.Drawing.Size(369, 21);
            this.cbTipoFiltroCombProdServ.TabIndex = 8;
            this.cbTipoFiltroCombProdServ.SelectedIndexChanged += new System.EventHandler(this.cbTipoFiltroCombProdServ_SelectedIndexChanged);
            this.cbTipoFiltroCombProdServ.Click += new System.EventHandler(this.cbTipoFiltroCombProdServ_Click);
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
            this.cbTipoFiltroStock.Location = new System.Drawing.Point(38, 8);
            this.cbTipoFiltroStock.Name = "cbTipoFiltroStock";
            this.cbTipoFiltroStock.Size = new System.Drawing.Size(284, 21);
            this.cbTipoFiltroStock.TabIndex = 1;
            this.cbTipoFiltroStock.SelectedIndexChanged += new System.EventHandler(this.cbTipoFiltroStock_SelectedIndexChanged);
            this.cbTipoFiltroStock.Click += new System.EventHandler(this.cbTipoFiltroStock_Click);
            // 
            // txtCantStock
            // 
            this.txtCantStock.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtCantStock.Location = new System.Drawing.Point(342, 6);
            this.txtCantStock.Name = "txtCantStock";
            this.txtCantStock.Size = new System.Drawing.Size(65, 23);
            this.txtCantStock.TabIndex = 2;
            this.txtCantStock.Text = "0.0";
            this.txtCantStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCantStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantStock_KeyPress);
            // 
            // txtCantPrecio
            // 
            this.txtCantPrecio.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtCantPrecio.Location = new System.Drawing.Point(342, 42);
            this.txtCantPrecio.Name = "txtCantPrecio";
            this.txtCantPrecio.Size = new System.Drawing.Size(65, 23);
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
            this.cbTipoFiltroPrecio.Location = new System.Drawing.Point(38, 44);
            this.cbTipoFiltroPrecio.Name = "cbTipoFiltroPrecio";
            this.cbTipoFiltroPrecio.Size = new System.Drawing.Size(284, 21);
            this.cbTipoFiltroPrecio.TabIndex = 4;
            this.cbTipoFiltroPrecio.SelectedIndexChanged += new System.EventHandler(this.cbTipoFiltroPrecio_SelectedIndexChanged);
            this.cbTipoFiltroPrecio.Click += new System.EventHandler(this.cbTipoFiltroPrecio_Click);
            // 
            // chkBoxPrecio
            // 
            this.chkBoxPrecio.AutoSize = true;
            this.chkBoxPrecio.Location = new System.Drawing.Point(19, 65);
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
            this.chkBoxStock.Location = new System.Drawing.Point(19, 29);
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
            this.btnAplicar.Location = new System.Drawing.Point(335, 385);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(95, 43);
            this.btnAplicar.TabIndex = 6;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = false;
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // btnClean
            // 
            this.btnClean.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnClean.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnClean.FlatAppearance.BorderSize = 0;
            this.btnClean.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClean.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClean.ForeColor = System.Drawing.Color.White;
            this.btnClean.Location = new System.Drawing.Point(122, 385);
            this.btnClean.Name = "btnClean";
            this.btnClean.Size = new System.Drawing.Size(95, 43);
            this.btnClean.TabIndex = 7;
            this.btnClean.Text = "Limpiar";
            this.btnClean.UseVisualStyleBackColor = false;
            this.btnClean.Click += new System.EventHandler(this.btnClean_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(19, 99);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(65, 17);
            this.checkBox1.TabIndex = 13;
            this.checkBox1.Text = "Revisón";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "No Aplica",
            "Mayor o Igual Que",
            "Menor o Igual Que",
            "Igual Que",
            "Mayor Que",
            "Menor Que"});
            this.comboBox1.Location = new System.Drawing.Point(38, 77);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(284, 21);
            this.comboBox1.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Bold);
            this.textBox1.Location = new System.Drawing.Point(342, 77);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(65, 23);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "0";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "No Aplica",
            "Con Imagen",
            "Sin Imagen",
            "Todas"});
            this.comboBox2.Location = new System.Drawing.Point(38, 144);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(369, 21);
            this.comboBox2.TabIndex = 9;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(19, 165);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(61, 17);
            this.checkBox2.TabIndex = 14;
            this.checkBox2.Text = "Imagen";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // WinQueryString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 442);
            this.Controls.Add(this.btnClean);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel fLPDetalleProducto;
        private System.Windows.Forms.CheckBox chkBoxTipo;
        private System.Windows.Forms.ComboBox cbTipoFiltroCombProdServ;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.CheckBox checkBox2;
    }
}