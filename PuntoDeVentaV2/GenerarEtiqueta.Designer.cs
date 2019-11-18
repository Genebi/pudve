namespace PuntoDeVentaV2
{
    partial class GenerarEtiqueta
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
            this.panelPropiedades = new System.Windows.Forms.Panel();
            this.panelEtiqueta = new System.Windows.Forms.Panel();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelPropiedades
            // 
            this.panelPropiedades.Location = new System.Drawing.Point(2, 2);
            this.panelPropiedades.Name = "panelPropiedades";
            this.panelPropiedades.Size = new System.Drawing.Size(200, 357);
            this.panelPropiedades.TabIndex = 0;
            // 
            // panelEtiqueta
            // 
            this.panelEtiqueta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEtiqueta.Location = new System.Drawing.Point(208, 100);
            this.panelEtiqueta.Name = "panelEtiqueta";
            this.panelEtiqueta.Size = new System.Drawing.Size(408, 122);
            this.panelEtiqueta.TabIndex = 1;
            // 
            // panelBotones
            // 
            this.panelBotones.Location = new System.Drawing.Point(622, 2);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(58, 357);
            this.panelBotones.TabIndex = 2;
            // 
            // GenerarEtiqueta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.panelEtiqueta);
            this.Controls.Add(this.panelPropiedades);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenerarEtiqueta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Personalizar etiqueta";
            this.Load += new System.EventHandler(this.GenerarEtiqueta_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelPropiedades;
        private System.Windows.Forms.Panel panelEtiqueta;
        private System.Windows.Forms.Panel panelBotones;
    }
}