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
            this.SuspendLayout();
            // 
            // txtAgregarPM
            // 
            this.txtAgregarPM.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAgregarPM.Location = new System.Drawing.Point(74, 65);
            this.txtAgregarPM.Name = "txtAgregarPM";
            this.txtAgregarPM.Size = new System.Drawing.Size(135, 23);
            this.txtAgregarPM.TabIndex = 0;
            this.txtAgregarPM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtAgregarPM.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAgregarPM_KeyDown);
            // 
            // lbAgregarMultiple
            // 
            this.lbAgregarMultiple.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAgregarMultiple.Location = new System.Drawing.Point(3, 20);
            this.lbAgregarMultiple.Name = "lbAgregarMultiple";
            this.lbAgregarMultiple.Size = new System.Drawing.Size(278, 23);
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
            this.btnAceptarAM.Location = new System.Drawing.Point(74, 94);
            this.btnAceptarAM.Name = "btnAceptarAM";
            this.btnAceptarAM.Size = new System.Drawing.Size(135, 28);
            this.btnAceptarAM.TabIndex = 29;
            this.btnAceptarAM.Text = "Aceptar";
            this.btnAceptarAM.UseVisualStyleBackColor = false;
            this.btnAceptarAM.Click += new System.EventHandler(this.btnAceptarAM_Click);
            // 
            // AgregarMultiplesProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 161);
            this.Controls.Add(this.btnAceptarAM);
            this.Controls.Add(this.lbAgregarMultiple);
            this.Controls.Add(this.txtAgregarPM);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgregarMultiplesProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PUDVE - Agregar multiple";
            this.Load += new System.EventHandler(this.AgregarMultiplesProductos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAgregarPM;
        private System.Windows.Forms.Label lbAgregarMultiple;
        private System.Windows.Forms.Button btnAceptarAM;
    }
}