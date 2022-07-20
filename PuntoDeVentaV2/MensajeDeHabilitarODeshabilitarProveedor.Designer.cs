
namespace PuntoDeVentaV2
{
    partial class MensajeDeHabilitarODeshabilitarProveedor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MensajeDeHabilitarODeshabilitarProveedor));
            this.btnAceptarDesc = new System.Windows.Forms.Button();
            this.btnCancelarDesc = new System.Windows.Forms.Button();
            this.lblTexto = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAceptarDesc
            // 
            this.btnAceptarDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptarDesc.BackColor = System.Drawing.Color.Green;
            this.btnAceptarDesc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptarDesc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnAceptarDesc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptarDesc.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptarDesc.ForeColor = System.Drawing.Color.White;
            this.btnAceptarDesc.Location = new System.Drawing.Point(76, 93);
            this.btnAceptarDesc.Name = "btnAceptarDesc";
            this.btnAceptarDesc.Size = new System.Drawing.Size(89, 28);
            this.btnAceptarDesc.TabIndex = 32;
            this.btnAceptarDesc.Text = "Si";
            this.btnAceptarDesc.UseVisualStyleBackColor = false;
            this.btnAceptarDesc.Click += new System.EventHandler(this.btnAceptarDesc_Click);
            // 
            // btnCancelarDesc
            // 
            this.btnCancelarDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelarDesc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarDesc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnCancelarDesc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarDesc.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarDesc.ForeColor = System.Drawing.Color.White;
            this.btnCancelarDesc.Location = new System.Drawing.Point(170, 93);
            this.btnCancelarDesc.Name = "btnCancelarDesc";
            this.btnCancelarDesc.Size = new System.Drawing.Size(90, 28);
            this.btnCancelarDesc.TabIndex = 31;
            this.btnCancelarDesc.Text = "No";
            this.btnCancelarDesc.UseVisualStyleBackColor = false;
            this.btnCancelarDesc.Click += new System.EventHandler(this.btnCancelarDesc_Click);
            // 
            // lblTexto
            // 
            this.lblTexto.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTexto.Location = new System.Drawing.Point(8, 27);
            this.lblTexto.Name = "lblTexto";
            this.lblTexto.Size = new System.Drawing.Size(244, 45);
            this.lblTexto.TabIndex = 30;
            this.lblTexto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MensajeDeHabilitarODeshabilitarProveedor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 131);
            this.Controls.Add(this.btnAceptarDesc);
            this.Controls.Add(this.btnCancelarDesc);
            this.Controls.Add(this.lblTexto);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MensajeDeHabilitarODeshabilitarProveedor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Aviso del Sistema";
            this.Load += new System.EventHandler(this.MensajeDeHabilitarODeshabilitarProveedor_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAceptarDesc;
        private System.Windows.Forms.Button btnCancelarDesc;
        private System.Windows.Forms.Label lblTexto;
    }
}