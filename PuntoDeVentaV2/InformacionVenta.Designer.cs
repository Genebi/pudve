namespace PuntoDeVentaV2
{
    partial class InformacionVenta
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
            this.primerPanel = new System.Windows.Forms.Panel();
            this.btnSiguiente1 = new System.Windows.Forms.Button();
            this.cbFormaPago = new System.Windows.Forms.ComboBox();
            this.lbFormaPago = new System.Windows.Forms.Label();
            this.primerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // primerPanel
            // 
            this.primerPanel.Controls.Add(this.lbFormaPago);
            this.primerPanel.Controls.Add(this.cbFormaPago);
            this.primerPanel.Controls.Add(this.btnSiguiente1);
            this.primerPanel.Location = new System.Drawing.Point(12, 12);
            this.primerPanel.Name = "primerPanel";
            this.primerPanel.Size = new System.Drawing.Size(360, 137);
            this.primerPanel.TabIndex = 0;
            // 
            // btnSiguiente1
            // 
            this.btnSiguiente1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSiguiente1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnSiguiente1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSiguiente1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnSiguiente1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSiguiente1.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSiguiente1.ForeColor = System.Drawing.Color.White;
            this.btnSiguiente1.Location = new System.Drawing.Point(252, 108);
            this.btnSiguiente1.Name = "btnSiguiente1";
            this.btnSiguiente1.Size = new System.Drawing.Size(105, 26);
            this.btnSiguiente1.TabIndex = 20;
            this.btnSiguiente1.Text = "Siguiente";
            this.btnSiguiente1.UseVisualStyleBackColor = false;
            this.btnSiguiente1.Click += new System.EventHandler(this.btnSiguiente1_Click);
            // 
            // cbFormaPago
            // 
            this.cbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormaPago.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFormaPago.FormattingEnabled = true;
            this.cbFormaPago.Location = new System.Drawing.Point(3, 53);
            this.cbFormaPago.Name = "cbFormaPago";
            this.cbFormaPago.Size = new System.Drawing.Size(354, 24);
            this.cbFormaPago.TabIndex = 21;
            // 
            // lbFormaPago
            // 
            this.lbFormaPago.AutoSize = true;
            this.lbFormaPago.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFormaPago.Location = new System.Drawing.Point(3, 23);
            this.lbFormaPago.Name = "lbFormaPago";
            this.lbFormaPago.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbFormaPago.Size = new System.Drawing.Size(158, 17);
            this.lbFormaPago.TabIndex = 71;
            this.lbFormaPago.Text = "Confirmar forma de pago";
            // 
            // InformacionVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.primerPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InformacionVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PUDVE - Verificando información";
            this.Load += new System.EventHandler(this.InformacionVenta_Load);
            this.primerPanel.ResumeLayout(false);
            this.primerPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel primerPanel;
        private System.Windows.Forms.Button btnSiguiente1;
        private System.Windows.Forms.ComboBox cbFormaPago;
        private System.Windows.Forms.Label lbFormaPago;
    }
}