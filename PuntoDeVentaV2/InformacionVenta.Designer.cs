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
            this.lbFormaPago = new System.Windows.Forms.Label();
            this.cbFormaPago = new System.Windows.Forms.ComboBox();
            this.btnSiguiente1 = new System.Windows.Forms.Button();
            this.lbMetodoPago = new System.Windows.Forms.Label();
            this.cbMetodoPago = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbCuenta = new System.Windows.Forms.Label();
            this.txtCuenta = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbFormaPago
            // 
            this.lbFormaPago.AutoSize = true;
            this.lbFormaPago.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFormaPago.Location = new System.Drawing.Point(3, 18);
            this.lbFormaPago.Name = "lbFormaPago";
            this.lbFormaPago.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbFormaPago.Size = new System.Drawing.Size(98, 17);
            this.lbFormaPago.TabIndex = 71;
            this.lbFormaPago.Text = "Forma de Pago";
            // 
            // cbFormaPago
            // 
            this.cbFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFormaPago.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFormaPago.FormattingEnabled = true;
            this.cbFormaPago.Location = new System.Drawing.Point(6, 38);
            this.cbFormaPago.Name = "cbFormaPago";
            this.cbFormaPago.Size = new System.Drawing.Size(348, 24);
            this.cbFormaPago.TabIndex = 21;
            this.cbFormaPago.SelectedIndexChanged += new System.EventHandler(this.cbFormaPago_SelectedIndexChanged);
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
            this.btnSiguiente1.Location = new System.Drawing.Point(267, 253);
            this.btnSiguiente1.Name = "btnSiguiente1";
            this.btnSiguiente1.Size = new System.Drawing.Size(105, 26);
            this.btnSiguiente1.TabIndex = 20;
            this.btnSiguiente1.Text = "Siguiente";
            this.btnSiguiente1.UseVisualStyleBackColor = false;
            this.btnSiguiente1.Click += new System.EventHandler(this.btnSiguiente1_Click);
            // 
            // lbMetodoPago
            // 
            this.lbMetodoPago.AutoSize = true;
            this.lbMetodoPago.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMetodoPago.Location = new System.Drawing.Point(3, 16);
            this.lbMetodoPago.Name = "lbMetodoPago";
            this.lbMetodoPago.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbMetodoPago.Size = new System.Drawing.Size(109, 17);
            this.lbMetodoPago.TabIndex = 72;
            this.lbMetodoPago.Text = "Método de Pago";
            // 
            // cbMetodoPago
            // 
            this.cbMetodoPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMetodoPago.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMetodoPago.FormattingEnabled = true;
            this.cbMetodoPago.Location = new System.Drawing.Point(6, 36);
            this.cbMetodoPago.Name = "cbMetodoPago";
            this.cbMetodoPago.Size = new System.Drawing.Size(348, 24);
            this.cbMetodoPago.TabIndex = 73;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCuenta);
            this.groupBox1.Controls.Add(this.lbCuenta);
            this.groupBox1.Controls.Add(this.lbFormaPago);
            this.groupBox1.Controls.Add(this.cbFormaPago);
            this.groupBox1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(360, 136);
            this.groupBox1.TabIndex = 74;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbMetodoPago);
            this.groupBox2.Controls.Add(this.lbMetodoPago);
            this.groupBox2.Location = new System.Drawing.Point(12, 163);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(360, 81);
            this.groupBox2.TabIndex = 75;
            this.groupBox2.TabStop = false;
            // 
            // lbCuenta
            // 
            this.lbCuenta.AutoSize = true;
            this.lbCuenta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCuenta.Location = new System.Drawing.Point(3, 82);
            this.lbCuenta.Name = "lbCuenta";
            this.lbCuenta.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbCuenta.Size = new System.Drawing.Size(52, 17);
            this.lbCuenta.TabIndex = 72;
            this.lbCuenta.Text = "Cuenta";
            this.lbCuenta.Visible = false;
            // 
            // txtCuenta
            // 
            this.txtCuenta.Location = new System.Drawing.Point(6, 102);
            this.txtCuenta.Name = "txtCuenta";
            this.txtCuenta.Size = new System.Drawing.Size(348, 22);
            this.txtCuenta.TabIndex = 73;
            this.txtCuenta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCuenta.Visible = false;
            // 
            // InformacionVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 291);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSiguiente1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InformacionVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PUDVE - Verificando información";
            this.Load += new System.EventHandler(this.InformacionVenta_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSiguiente1;
        private System.Windows.Forms.ComboBox cbFormaPago;
        private System.Windows.Forms.Label lbFormaPago;
        private System.Windows.Forms.ComboBox cbMetodoPago;
        private System.Windows.Forms.Label lbMetodoPago;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCuenta;
        private System.Windows.Forms.Label lbCuenta;
    }
}