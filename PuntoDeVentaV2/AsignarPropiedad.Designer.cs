namespace PuntoDeVentaV2
{
    partial class AsignarPropiedad
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
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.lbNombrePropiedad = new System.Windows.Forms.Label();
            this.panelContenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContenedor
            // 
            this.panelContenedor.Controls.Add(this.lbNombrePropiedad);
            this.panelContenedor.Location = new System.Drawing.Point(2, 1);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(331, 198);
            this.panelContenedor.TabIndex = 0;
            // 
            // lbNombrePropiedad
            // 
            this.lbNombrePropiedad.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNombrePropiedad.Location = new System.Drawing.Point(10, 8);
            this.lbNombrePropiedad.Name = "lbNombrePropiedad";
            this.lbNombrePropiedad.Size = new System.Drawing.Size(310, 23);
            this.lbNombrePropiedad.TabIndex = 1;
            this.lbNombrePropiedad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AsignarPropiedad
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(334, 204);
            this.Controls.Add(this.panelContenedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AsignarPropiedad";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AsignarPropiedad_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AsignarPropiedad_FormClosed);
            this.Load += new System.EventHandler(this.AsignarPropiedad_Load);
            this.panelContenedor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Label lbNombrePropiedad;
    }
}