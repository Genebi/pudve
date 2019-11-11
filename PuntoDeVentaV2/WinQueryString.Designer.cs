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
            this.txtCantStock = new System.Windows.Forms.TextBox();
            this.chkBoxStock = new System.Windows.Forms.CheckBox();
            this.cbTipoFiltro = new System.Windows.Forms.ComboBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCantStock);
            this.groupBox1.Controls.Add(this.chkBoxStock);
            this.groupBox1.Location = new System.Drawing.Point(27, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Seleccion de Filtrado: ";
            // 
            // txtCantStock
            // 
            this.txtCantStock.Location = new System.Drawing.Point(117, 27);
            this.txtCantStock.Name = "txtCantStock";
            this.txtCantStock.Size = new System.Drawing.Size(77, 20);
            this.txtCantStock.TabIndex = 1;
            this.txtCantStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantStock_KeyPress);
            // 
            // chkBoxStock
            // 
            this.chkBoxStock.AutoSize = true;
            this.chkBoxStock.Location = new System.Drawing.Point(26, 29);
            this.chkBoxStock.Name = "chkBoxStock";
            this.chkBoxStock.Size = new System.Drawing.Size(54, 17);
            this.chkBoxStock.TabIndex = 0;
            this.chkBoxStock.Text = "Stock";
            this.chkBoxStock.UseVisualStyleBackColor = true;
            // 
            // cbTipoFiltro
            // 
            this.cbTipoFiltro.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbTipoFiltro.FormattingEnabled = true;
            this.cbTipoFiltro.Items.AddRange(new object[] {
            "No Aplica",
            "Mayor Igual",
            "Menor Igual",
            "Igual Que",
            "Mayor Que",
            "Menor Que"});
            this.cbTipoFiltro.Location = new System.Drawing.Point(27, 158);
            this.cbTipoFiltro.Name = "cbTipoFiltro";
            this.cbTipoFiltro.Size = new System.Drawing.Size(216, 21);
            this.cbTipoFiltro.TabIndex = 1;
            this.cbTipoFiltro.Click += new System.EventHandler(this.cbTipoFiltro_Click);
            // 
            // btnAplicar
            // 
            this.btnAplicar.BackColor = System.Drawing.Color.ForestGreen;
            this.btnAplicar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAplicar.FlatAppearance.BorderSize = 0;
            this.btnAplicar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicar.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAplicar.ForeColor = System.Drawing.Color.White;
            this.btnAplicar.Location = new System.Drawing.Point(148, 196);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(95, 43);
            this.btnAplicar.TabIndex = 2;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(36, 196);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 43);
            this.btnCancelar.TabIndex = 3;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.button2_Click);
            // 
            // WinQueryString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(275, 261);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAplicar);
            this.Controls.Add(this.cbTipoFiltro);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WinQueryString";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro Avanzado de Productos";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCantStock;
        private System.Windows.Forms.CheckBox chkBoxStock;
        private System.Windows.Forms.ComboBox cbTipoFiltro;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.Button btnCancelar;
    }
}