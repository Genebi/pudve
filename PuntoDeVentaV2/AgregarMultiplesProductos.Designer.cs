namespace PuntoDeVentaV2
{
    partial class AgregarMultiplesProductos
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
            this.txtAgregarPM = new System.Windows.Forms.TextBox();
            this.lbAgregarMultiple = new System.Windows.Forms.Label();
            this.btnAceptarAM = new System.Windows.Forms.Button();
            this.btnCancelarAM = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtAgregarPM
            // 
            this.txtAgregarPM.Location = new System.Drawing.Point(69, 86);
            this.txtAgregarPM.Name = "txtAgregarPM";
            this.txtAgregarPM.Size = new System.Drawing.Size(205, 20);
            this.txtAgregarPM.TabIndex = 0;
            this.txtAgregarPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbAgregarMultiple
            // 
            this.lbAgregarMultiple.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAgregarMultiple.Location = new System.Drawing.Point(12, 30);
            this.lbAgregarMultiple.Name = "lbAgregarMultiple";
            this.lbAgregarMultiple.Size = new System.Drawing.Size(306, 23);
            this.lbAgregarMultiple.TabIndex = 1;
            this.lbAgregarMultiple.Text = "Agregar Multiple";
            this.lbAgregarMultiple.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAceptarAM
            // 
            this.btnAceptarAM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptarAM.BackColor = System.Drawing.Color.Green;
            this.btnAceptarAM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptarAM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnAceptarAM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptarAM.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptarAM.ForeColor = System.Drawing.Color.White;
            this.btnAceptarAM.Location = new System.Drawing.Point(169, 151);
            this.btnAceptarAM.Name = "btnAceptarAM";
            this.btnAceptarAM.Size = new System.Drawing.Size(144, 28);
            this.btnAceptarAM.TabIndex = 29;
            this.btnAceptarAM.Text = "Aceptar";
            this.btnAceptarAM.UseVisualStyleBackColor = false;
            this.btnAceptarAM.Click += new System.EventHandler(this.btnAceptarAM_Click);
            // 
            // btnCancelarAM
            // 
            this.btnCancelarAM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarAM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelarAM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarAM.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnCancelarAM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarAM.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarAM.ForeColor = System.Drawing.Color.White;
            this.btnCancelarAM.Location = new System.Drawing.Point(19, 151);
            this.btnCancelarAM.Name = "btnCancelarAM";
            this.btnCancelarAM.Size = new System.Drawing.Size(144, 28);
            this.btnCancelarAM.TabIndex = 28;
            this.btnCancelarAM.Text = "Cancelar";
            this.btnCancelarAM.UseVisualStyleBackColor = false;
            this.btnCancelarAM.Click += new System.EventHandler(this.btnCancelarAM_Click);
            // 
            // AgregarMultiplesProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 207);
            this.Controls.Add(this.btnAceptarAM);
            this.Controls.Add(this.btnCancelarAM);
            this.Controls.Add(this.lbAgregarMultiple);
            this.Controls.Add(this.txtAgregarPM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "AgregarMultiplesProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Agregar multiple";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAgregarPM;
        private System.Windows.Forms.Label lbAgregarMultiple;
        private System.Windows.Forms.Button btnAceptarAM;
        private System.Windows.Forms.Button btnCancelarAM;
    }
}