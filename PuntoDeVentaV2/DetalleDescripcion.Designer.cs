namespace PuntoDeVentaV2
{
    partial class DetalleDescripcion
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
            this.DGVDetalle = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.DGVDetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVDetalle
            // 
            this.DGVDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVDetalle.Location = new System.Drawing.Point(22, 178);
            this.DGVDetalle.Name = "DGVDetalle";
            this.DGVDetalle.RowTemplate.Height = 24;
            this.DGVDetalle.Size = new System.Drawing.Size(599, 262);
            this.DGVDetalle.TabIndex = 0;
            // 
            // DetalleDescripcion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 494);
            this.Controls.Add(this.DGVDetalle);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetalleDescripcion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalle Descripcion";
            this.Load += new System.EventHandler(this.DetalleDescripcion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVDetalle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVDetalle;
    }
}