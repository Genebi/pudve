namespace PuntoDeVentaV2
{
    partial class AsignarAbonos
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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lbSeparadorDetalle2 = new System.Windows.Forms.Label();
            this.lbSeparadorDetalle1 = new System.Windows.Forms.Label();
            this.txtReferencia = new System.Windows.Forms.TextBox();
            this.lbReferencia = new System.Windows.Forms.Label();
            this.lbTransferencia = new System.Windows.Forms.Label();
            this.lbCheque = new System.Windows.Forms.Label();
            this.lbVales = new System.Windows.Forms.Label();
            this.lbTarjeta = new System.Windows.Forms.Label();
            this.lbEfectivo = new System.Windows.Forms.Label();
            this.txtTransferencia = new System.Windows.Forms.TextBox();
            this.txtCheque = new System.Windows.Forms.TextBox();
            this.txtVales = new System.Windows.Forms.TextBox();
            this.txtTarjeta = new System.Windows.Forms.TextBox();
            this.txtEfectivo = new System.Windows.Forms.TextBox();
            this.txtPendiente = new System.Windows.Forms.Label();
            this.tituloAbono = new System.Windows.Forms.Label();
            this.lbVerAbonos = new System.Windows.Forms.LinkLabel();
            this.txtTotalOriginal = new System.Windows.Forms.Label();
            this.tituloTotal = new System.Windows.Forms.Label();
            this.lbTotalCambio = new System.Windows.Forms.Label();
            this.lbCambio = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.BackColor = System.Drawing.Color.Green;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(342, 269);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(130, 28);
            this.btnAceptar.TabIndex = 129;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lbSeparadorDetalle2
            // 
            this.lbSeparadorDetalle2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSeparadorDetalle2.Location = new System.Drawing.Point(13, 250);
            this.lbSeparadorDetalle2.Name = "lbSeparadorDetalle2";
            this.lbSeparadorDetalle2.Size = new System.Drawing.Size(460, 2);
            this.lbSeparadorDetalle2.TabIndex = 139;
            // 
            // lbSeparadorDetalle1
            // 
            this.lbSeparadorDetalle1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSeparadorDetalle1.Location = new System.Drawing.Point(13, 86);
            this.lbSeparadorDetalle1.Name = "lbSeparadorDetalle1";
            this.lbSeparadorDetalle1.Size = new System.Drawing.Size(460, 2);
            this.lbSeparadorDetalle1.TabIndex = 138;
            // 
            // txtReferencia
            // 
            this.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReferencia.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferencia.Location = new System.Drawing.Point(107, 195);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(364, 22);
            this.txtReferencia.TabIndex = 126;
            // 
            // lbReferencia
            // 
            this.lbReferencia.AutoSize = true;
            this.lbReferencia.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReferencia.Location = new System.Drawing.Point(19, 199);
            this.lbReferencia.Name = "lbReferencia";
            this.lbReferencia.Size = new System.Drawing.Size(66, 16);
            this.lbReferencia.TabIndex = 137;
            this.lbReferencia.Text = "Referencia";
            // 
            // lbTransferencia
            // 
            this.lbTransferencia.AutoSize = true;
            this.lbTransferencia.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTransferencia.Location = new System.Drawing.Point(389, 109);
            this.lbTransferencia.Name = "lbTransferencia";
            this.lbTransferencia.Size = new System.Drawing.Size(79, 16);
            this.lbTransferencia.TabIndex = 136;
            this.lbTransferencia.Text = "Transferencia";
            // 
            // lbCheque
            // 
            this.lbCheque.AutoSize = true;
            this.lbCheque.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCheque.Location = new System.Drawing.Point(305, 109);
            this.lbCheque.Name = "lbCheque";
            this.lbCheque.Size = new System.Drawing.Size(53, 16);
            this.lbCheque.TabIndex = 135;
            this.lbCheque.Text = "Cheque";
            // 
            // lbVales
            // 
            this.lbVales.AutoSize = true;
            this.lbVales.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVales.Location = new System.Drawing.Point(225, 109);
            this.lbVales.Name = "lbVales";
            this.lbVales.Size = new System.Drawing.Size(36, 16);
            this.lbVales.TabIndex = 134;
            this.lbVales.Text = "Vales";
            // 
            // lbTarjeta
            // 
            this.lbTarjeta.AutoSize = true;
            this.lbTarjeta.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTarjeta.Location = new System.Drawing.Point(122, 109);
            this.lbTarjeta.Name = "lbTarjeta";
            this.lbTarjeta.Size = new System.Drawing.Size(45, 16);
            this.lbTarjeta.TabIndex = 133;
            this.lbTarjeta.Text = "Tarjeta";
            // 
            // lbEfectivo
            // 
            this.lbEfectivo.AutoSize = true;
            this.lbEfectivo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEfectivo.Location = new System.Drawing.Point(26, 109);
            this.lbEfectivo.Name = "lbEfectivo";
            this.lbEfectivo.Size = new System.Drawing.Size(52, 16);
            this.lbEfectivo.TabIndex = 132;
            this.lbEfectivo.Text = "Efectivo";
            // 
            // txtTransferencia
            // 
            this.txtTransferencia.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransferencia.Location = new System.Drawing.Point(391, 140);
            this.txtTransferencia.Name = "txtTransferencia";
            this.txtTransferencia.Size = new System.Drawing.Size(80, 22);
            this.txtTransferencia.TabIndex = 125;
            this.txtTransferencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTransferencia.TextChanged += new System.EventHandler(this.txtTransferencia_TextChanged);
            this.txtTransferencia.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTransferencia_KeyUp);
            // 
            // txtCheque
            // 
            this.txtCheque.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheque.Location = new System.Drawing.Point(297, 140);
            this.txtCheque.Name = "txtCheque";
            this.txtCheque.Size = new System.Drawing.Size(80, 22);
            this.txtCheque.TabIndex = 124;
            this.txtCheque.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCheque.TextChanged += new System.EventHandler(this.txtCheque_TextChanged);
            this.txtCheque.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCheque_KeyUp);
            // 
            // txtVales
            // 
            this.txtVales.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVales.Location = new System.Drawing.Point(203, 140);
            this.txtVales.Name = "txtVales";
            this.txtVales.Size = new System.Drawing.Size(80, 22);
            this.txtVales.TabIndex = 123;
            this.txtVales.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtVales.TextChanged += new System.EventHandler(this.txtVales_TextChanged);
            this.txtVales.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtVales_KeyUp);
            // 
            // txtTarjeta
            // 
            this.txtTarjeta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTarjeta.Location = new System.Drawing.Point(107, 140);
            this.txtTarjeta.Name = "txtTarjeta";
            this.txtTarjeta.Size = new System.Drawing.Size(80, 22);
            this.txtTarjeta.TabIndex = 122;
            this.txtTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTarjeta.TextChanged += new System.EventHandler(this.txtTarjeta_TextChanged);
            this.txtTarjeta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTarjeta_KeyUp);
            // 
            // txtEfectivo
            // 
            this.txtEfectivo.AcceptsReturn = true;
            this.txtEfectivo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEfectivo.Location = new System.Drawing.Point(13, 140);
            this.txtEfectivo.Name = "txtEfectivo";
            this.txtEfectivo.Size = new System.Drawing.Size(80, 22);
            this.txtEfectivo.TabIndex = 121;
            this.txtEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEfectivo.TextChanged += new System.EventHandler(this.txtEfectivo_TextChanged);
            this.txtEfectivo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEfectivo_KeyUp);
            // 
            // txtPendiente
            // 
            this.txtPendiente.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtPendiente.Font = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPendiente.Location = new System.Drawing.Point(191, 45);
            this.txtPendiente.Name = "txtPendiente";
            this.txtPendiente.Size = new System.Drawing.Size(131, 23);
            this.txtPendiente.TabIndex = 131;
            this.txtPendiente.Text = "$0.00";
            this.txtPendiente.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtPendiente.Click += new System.EventHandler(this.txtPendiente_Click);
            // 
            // tituloAbono
            // 
            this.tituloAbono.AutoSize = true;
            this.tituloAbono.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloAbono.Location = new System.Drawing.Point(188, 19);
            this.tituloAbono.Name = "tituloAbono";
            this.tituloAbono.Size = new System.Drawing.Size(134, 17);
            this.tituloAbono.TabIndex = 130;
            this.tituloAbono.Text = "Pendiente de pago";
            // 
            // lbVerAbonos
            // 
            this.lbVerAbonos.AutoSize = true;
            this.lbVerAbonos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVerAbonos.Location = new System.Drawing.Point(10, 275);
            this.lbVerAbonos.Name = "lbVerAbonos";
            this.lbVerAbonos.Size = new System.Drawing.Size(76, 17);
            this.lbVerAbonos.TabIndex = 140;
            this.lbVerAbonos.TabStop = true;
            this.lbVerAbonos.Text = "Ver abonos";
            this.lbVerAbonos.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbVerAbonos_LinkClicked);
            // 
            // txtTotalOriginal
            // 
            this.txtTotalOriginal.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtTotalOriginal.Font = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalOriginal.Location = new System.Drawing.Point(13, 45);
            this.txtTotalOriginal.Name = "txtTotalOriginal";
            this.txtTotalOriginal.Size = new System.Drawing.Size(123, 23);
            this.txtTotalOriginal.TabIndex = 142;
            this.txtTotalOriginal.Text = "$0.00";
            this.txtTotalOriginal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tituloTotal
            // 
            this.tituloTotal.AutoSize = true;
            this.tituloTotal.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloTotal.Location = new System.Drawing.Point(35, 19);
            this.tituloTotal.Name = "tituloTotal";
            this.tituloTotal.Size = new System.Drawing.Size(91, 17);
            this.tituloTotal.TabIndex = 141;
            this.tituloTotal.Text = "Total original";
            // 
            // lbTotalCambio
            // 
            this.lbTotalCambio.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbTotalCambio.Font = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalCambio.Location = new System.Drawing.Point(328, 45);
            this.lbTotalCambio.Name = "lbTotalCambio";
            this.lbTotalCambio.Size = new System.Drawing.Size(123, 23);
            this.lbTotalCambio.TabIndex = 144;
            this.lbTotalCambio.Text = "$0.00";
            this.lbTotalCambio.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbCambio
            // 
            this.lbCambio.AutoSize = true;
            this.lbCambio.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCambio.Location = new System.Drawing.Point(389, 19);
            this.lbCambio.Name = "lbCambio";
            this.lbCambio.Size = new System.Drawing.Size(62, 17);
            this.lbCambio.TabIndex = 143;
            this.lbCambio.Text = "Cambio";
            // 
            // AsignarAbonos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 321);
            this.Controls.Add(this.lbTotalCambio);
            this.Controls.Add(this.lbCambio);
            this.Controls.Add(this.txtTotalOriginal);
            this.Controls.Add(this.tituloTotal);
            this.Controls.Add(this.lbVerAbonos);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.lbSeparadorDetalle2);
            this.Controls.Add(this.lbSeparadorDetalle1);
            this.Controls.Add(this.txtReferencia);
            this.Controls.Add(this.lbReferencia);
            this.Controls.Add(this.lbTransferencia);
            this.Controls.Add(this.lbCheque);
            this.Controls.Add(this.lbVales);
            this.Controls.Add(this.lbTarjeta);
            this.Controls.Add(this.lbEfectivo);
            this.Controls.Add(this.txtTransferencia);
            this.Controls.Add(this.txtCheque);
            this.Controls.Add(this.txtVales);
            this.Controls.Add(this.txtTarjeta);
            this.Controls.Add(this.txtEfectivo);
            this.Controls.Add(this.txtPendiente);
            this.Controls.Add(this.tituloAbono);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AsignarAbonos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PUDVE - Abono";
            this.Load += new System.EventHandler(this.AsignarAbonos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lbSeparadorDetalle2;
        private System.Windows.Forms.Label lbSeparadorDetalle1;
        private System.Windows.Forms.TextBox txtReferencia;
        private System.Windows.Forms.Label lbReferencia;
        private System.Windows.Forms.Label lbTransferencia;
        private System.Windows.Forms.Label lbCheque;
        private System.Windows.Forms.Label lbVales;
        private System.Windows.Forms.Label lbTarjeta;
        private System.Windows.Forms.Label lbEfectivo;
        private System.Windows.Forms.TextBox txtTransferencia;
        private System.Windows.Forms.TextBox txtCheque;
        private System.Windows.Forms.TextBox txtVales;
        private System.Windows.Forms.TextBox txtTarjeta;
        private System.Windows.Forms.TextBox txtEfectivo;
        private System.Windows.Forms.Label txtPendiente;
        private System.Windows.Forms.Label tituloAbono;
        private System.Windows.Forms.LinkLabel lbVerAbonos;
        private System.Windows.Forms.Label txtTotalOriginal;
        private System.Windows.Forms.Label tituloTotal;
        private System.Windows.Forms.Label lbTotalCambio;
        private System.Windows.Forms.Label lbCambio;
    }
}