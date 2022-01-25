namespace PuntoDeVentaV2
{
    partial class AgregarDetalleGeneral
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
            this.btnQuitarDetalle = new PuntoDeVentaV2.BotonRedondo();
            this.btnAgregarDetalle = new PuntoDeVentaV2.BotonRedondo();
            this.SuspendLayout();
            // 
            // btnQuitarDetalle
            // 
            this.btnQuitarDetalle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnQuitarDetalle.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnQuitarDetalle.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnQuitarDetalle.BorderRadius = 20;
            this.btnQuitarDetalle.BorderSize = 0;
            this.btnQuitarDetalle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnQuitarDetalle.FlatAppearance.BorderSize = 0;
            this.btnQuitarDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuitarDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnQuitarDetalle.ForeColor = System.Drawing.Color.White;
            this.btnQuitarDetalle.Image = global::PuntoDeVentaV2.Properties.Resources.notebook;
            this.btnQuitarDetalle.Location = new System.Drawing.Point(244, 23);
            this.btnQuitarDetalle.Name = "btnQuitarDetalle";
            this.btnQuitarDetalle.Size = new System.Drawing.Size(150, 85);
            this.btnQuitarDetalle.TabIndex = 67;
            this.btnQuitarDetalle.Text = "Quitar especificación";
            this.btnQuitarDetalle.TextColor = System.Drawing.Color.White;
            this.btnQuitarDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnQuitarDetalle.UseVisualStyleBackColor = false;
            this.btnQuitarDetalle.Click += new System.EventHandler(this.btnQuitarDetalle_Click);
            // 
            // btnAgregarDetalle
            // 
            this.btnAgregarDetalle.BackColor = System.Drawing.Color.Green;
            this.btnAgregarDetalle.BackGroundColor = System.Drawing.Color.Green;
            this.btnAgregarDetalle.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnAgregarDetalle.BorderRadius = 20;
            this.btnAgregarDetalle.BorderSize = 0;
            this.btnAgregarDetalle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarDetalle.FlatAppearance.BorderSize = 0;
            this.btnAgregarDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnAgregarDetalle.ForeColor = System.Drawing.Color.White;
            this.btnAgregarDetalle.Image = global::PuntoDeVentaV2.Properties.Resources.anadir;
            this.btnAgregarDetalle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAgregarDetalle.Location = new System.Drawing.Point(28, 23);
            this.btnAgregarDetalle.Name = "btnAgregarDetalle";
            this.btnAgregarDetalle.Size = new System.Drawing.Size(154, 85);
            this.btnAgregarDetalle.TabIndex = 66;
            this.btnAgregarDetalle.Text = "Agregar especificación";
            this.btnAgregarDetalle.TextColor = System.Drawing.Color.White;
            this.btnAgregarDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAgregarDetalle.UseVisualStyleBackColor = false;
            this.btnAgregarDetalle.Click += new System.EventHandler(this.btnAgregarDetalle_Click);
            // 
            // AgregarDetalleGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(424, 134);
            this.Controls.Add(this.btnQuitarDetalle);
            this.Controls.Add(this.btnAgregarDetalle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgregarDetalleGeneral";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Nuevo Detalle";
            this.Load += new System.EventHandler(this.AgregarDetalleGeneral_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AgregarDetalleGeneral_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private BotonRedondo btnAgregarDetalle;
        private BotonRedondo btnQuitarDetalle;
    }
}