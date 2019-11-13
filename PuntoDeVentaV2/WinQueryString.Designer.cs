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
            this.cbTipoFiltroPrecio = new System.Windows.Forms.ComboBox();
            this.txtCantPrecio = new System.Windows.Forms.TextBox();
            this.chkBoxPrecio = new System.Windows.Forms.CheckBox();
            this.txtCantStock = new System.Windows.Forms.TextBox();
            this.chkBoxStock = new System.Windows.Forms.CheckBox();
            this.cbTipoFiltroStock = new System.Windows.Forms.ComboBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnClean = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbTipoFiltroPrecio);
            this.groupBox1.Controls.Add(this.txtCantPrecio);
            this.groupBox1.Controls.Add(this.chkBoxPrecio);
            this.groupBox1.Controls.Add(this.txtCantStock);
            this.groupBox1.Controls.Add(this.chkBoxStock);
            this.groupBox1.Controls.Add(this.cbTipoFiltroStock);
            this.groupBox1.Location = new System.Drawing.Point(27, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(343, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Seleccion de Filtrado: ";
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
            this.cbTipoFiltroPrecio.Location = new System.Drawing.Point(112, 63);
            this.cbTipoFiltroPrecio.Name = "cbTipoFiltroPrecio";
            this.cbTipoFiltroPrecio.Size = new System.Drawing.Size(116, 21);
            this.cbTipoFiltroPrecio.TabIndex = 4;
            this.cbTipoFiltroPrecio.Click += new System.EventHandler(this.cbTipoFiltroPrecio_Click);
            // 
            // txtCantPrecio
            // 
            this.txtCantPrecio.Location = new System.Drawing.Point(249, 63);
            this.txtCantPrecio.Name = "txtCantPrecio";
            this.txtCantPrecio.Size = new System.Drawing.Size(77, 20);
            this.txtCantPrecio.TabIndex = 5;
            this.txtCantPrecio.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantPrecio_KeyPress);
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
            // txtCantStock
            // 
            this.txtCantStock.Location = new System.Drawing.Point(249, 27);
            this.txtCantStock.Name = "txtCantStock";
            this.txtCantStock.Size = new System.Drawing.Size(77, 20);
            this.txtCantStock.TabIndex = 2;
            this.txtCantStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantStock_KeyPress);
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
            this.cbTipoFiltroStock.Location = new System.Drawing.Point(112, 27);
            this.cbTipoFiltroStock.Name = "cbTipoFiltroStock";
            this.cbTipoFiltroStock.Size = new System.Drawing.Size(116, 21);
            this.cbTipoFiltroStock.TabIndex = 1;
            this.cbTipoFiltroStock.SelectedIndexChanged += new System.EventHandler(this.cbTipoFiltroStock_SelectedIndexChanged);
            this.cbTipoFiltroStock.Click += new System.EventHandler(this.cbTipoFiltroStock_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnAplicar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAplicar.FlatAppearance.BorderSize = 0;
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicar.ForeColor = System.Drawing.Color.White;
            this.btnAplicar.Location = new System.Drawing.Point(275, 142);
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
            this.btnClean.Location = new System.Drawing.Point(154, 142);
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
            this.btnClose.Location = new System.Drawing.Point(27, 143);
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
            this.ClientSize = new System.Drawing.Size(407, 206);
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
    }
}