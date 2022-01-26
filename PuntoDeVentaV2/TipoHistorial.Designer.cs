namespace PuntoDeVentaV2
{
    partial class TipoHistorial
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TipoHistorial));
            this.btnHistorialCompras = new System.Windows.Forms.Button();
            this.btnHistorialVentas = new System.Windows.Forms.Button();
            this.botonRedondo1 = new PuntoDeVentaV2.BotonRedondo();
            this.SuspendLayout();
            // 
            // btnHistorialCompras
            // 
            this.btnHistorialCompras.BackColor = System.Drawing.Color.Transparent;
            this.btnHistorialCompras.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHistorialCompras.BackgroundImage")));
            this.btnHistorialCompras.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHistorialCompras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorialCompras.FlatAppearance.BorderSize = 0;
            this.btnHistorialCompras.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnHistorialCompras.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnHistorialCompras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialCompras.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorialCompras.ForeColor = System.Drawing.Color.White;
            this.btnHistorialCompras.Location = new System.Drawing.Point(28, 31);
            this.btnHistorialCompras.Name = "btnHistorialCompras";
            this.btnHistorialCompras.Size = new System.Drawing.Size(140, 90);
            this.btnHistorialCompras.TabIndex = 103;
            this.btnHistorialCompras.UseVisualStyleBackColor = false;
            this.btnHistorialCompras.Click += new System.EventHandler(this.btnHistorialCompras_Click);
            // 
            // btnHistorialVentas
            // 
            this.btnHistorialVentas.BackColor = System.Drawing.Color.Transparent;
            this.btnHistorialVentas.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnHistorialVentas.BackgroundImage")));
            this.btnHistorialVentas.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnHistorialVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorialVentas.FlatAppearance.BorderSize = 0;
            this.btnHistorialVentas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnHistorialVentas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnHistorialVentas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialVentas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorialVentas.ForeColor = System.Drawing.Color.White;
            this.btnHistorialVentas.Location = new System.Drawing.Point(193, 31);
            this.btnHistorialVentas.Name = "btnHistorialVentas";
            this.btnHistorialVentas.Size = new System.Drawing.Size(140, 90);
            this.btnHistorialVentas.TabIndex = 104;
            this.btnHistorialVentas.UseVisualStyleBackColor = false;
            this.btnHistorialVentas.Click += new System.EventHandler(this.btnHistorialVentas_Click);
            // 
            // botonRedondo1
            // 
            this.botonRedondo1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.botonRedondo1.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.botonRedondo1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.botonRedondo1.BorderRadius = 40;
            this.botonRedondo1.BorderSize = 0;
            this.botonRedondo1.FlatAppearance.BorderSize = 0;
            this.botonRedondo1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonRedondo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonRedondo1.ForeColor = System.Drawing.Color.White;
            this.botonRedondo1.Image = global::PuntoDeVentaV2.Properties.Resources.clipboard_invoice;
            this.botonRedondo1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.botonRedondo1.Location = new System.Drawing.Point(361, 31);
            this.botonRedondo1.Name = "botonRedondo1";
            this.botonRedondo1.Size = new System.Drawing.Size(133, 90);
            this.botonRedondo1.TabIndex = 105;
            this.botonRedondo1.Text = "Historial de Stock";
            this.botonRedondo1.TextColor = System.Drawing.Color.White;
            this.botonRedondo1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.botonRedondo1.UseVisualStyleBackColor = false;
            this.botonRedondo1.Click += new System.EventHandler(this.botonRedondo1_Click);
            // 
            // TipoHistorial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 152);
            this.Controls.Add(this.botonRedondo1);
            this.Controls.Add(this.btnHistorialVentas);
            this.Controls.Add(this.btnHistorialCompras);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TipoHistorial";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TipoHistorial_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHistorialCompras;
        private System.Windows.Forms.Button btnHistorialVentas;
        private BotonRedondo botonRedondo1;
    }
}