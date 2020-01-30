namespace PuntoDeVentaV2
{
    partial class FiltroReporteProductos
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
            this.checkStock = new System.Windows.Forms.CheckBox();
            this.checkPrecio = new System.Windows.Forms.CheckBox();
            this.cbStock = new System.Windows.Forms.ComboBox();
            this.cbPrecio = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // checkStock
            // 
            this.checkStock.AutoSize = true;
            this.checkStock.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkStock.Location = new System.Drawing.Point(28, 30);
            this.checkStock.Name = "checkStock";
            this.checkStock.Size = new System.Drawing.Size(61, 21);
            this.checkStock.TabIndex = 0;
            this.checkStock.Text = "Stock";
            this.checkStock.UseVisualStyleBackColor = true;
            // 
            // checkPrecio
            // 
            this.checkPrecio.AutoSize = true;
            this.checkPrecio.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkPrecio.Location = new System.Drawing.Point(28, 66);
            this.checkPrecio.Name = "checkPrecio";
            this.checkPrecio.Size = new System.Drawing.Size(65, 21);
            this.checkPrecio.TabIndex = 1;
            this.checkPrecio.Text = "Precio";
            this.checkPrecio.UseVisualStyleBackColor = true;
            // 
            // cbStock
            // 
            this.cbStock.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStock.FormattingEnabled = true;
            this.cbStock.Location = new System.Drawing.Point(112, 30);
            this.cbStock.Name = "cbStock";
            this.cbStock.Size = new System.Drawing.Size(308, 21);
            this.cbStock.TabIndex = 2;
            // 
            // cbPrecio
            // 
            this.cbPrecio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPrecio.FormattingEnabled = true;
            this.cbPrecio.Location = new System.Drawing.Point(112, 66);
            this.cbPrecio.Name = "cbPrecio";
            this.cbPrecio.Size = new System.Drawing.Size(308, 21);
            this.cbPrecio.TabIndex = 3;
            // 
            // FiltroReporteProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 311);
            this.Controls.Add(this.cbPrecio);
            this.Controls.Add(this.cbStock);
            this.Controls.Add(this.checkPrecio);
            this.Controls.Add(this.checkStock);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FiltroReporteProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Filtro del Reporte";
            this.Load += new System.EventHandler(this.FiltroReporteProductos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkStock;
        private System.Windows.Forms.CheckBox checkPrecio;
        private System.Windows.Forms.ComboBox cbStock;
        private System.Windows.Forms.ComboBox cbPrecio;
    }
}