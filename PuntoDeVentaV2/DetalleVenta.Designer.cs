namespace PuntoDeVentaV2
{
    partial class DetalleVenta
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
            this.lbEliminarCliente = new System.Windows.Forms.LinkLabel();
            this.lbTotalCambio = new System.Windows.Forms.Label();
            this.lbCambio = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbTotalCredito = new System.Windows.Forms.Label();
            this.lbCredito = new System.Windows.Forms.Label();
            this.btnCredito = new System.Windows.Forms.Button();
            this.lbCliente = new System.Windows.Forms.LinkLabel();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lbSeparadorDetalle2 = new System.Windows.Forms.Label();
            this.lbSeparadorDetalle3 = new System.Windows.Forms.Label();
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
            this.txtTotalVenta = new System.Windows.Forms.Label();
            this.tituloDetalle = new System.Windows.Forms.Label();
            this.txtCredito = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbEliminarCliente
            // 
            this.lbEliminarCliente.AutoSize = true;
            this.lbEliminarCliente.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEliminarCliente.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.lbEliminarCliente.LinkColor = System.Drawing.Color.Red;
            this.lbEliminarCliente.Location = new System.Drawing.Point(10, 217);
            this.lbEliminarCliente.Name = "lbEliminarCliente";
            this.lbEliminarCliente.Size = new System.Drawing.Size(15, 16);
            this.lbEliminarCliente.TabIndex = 121;
            this.lbEliminarCliente.TabStop = true;
            this.lbEliminarCliente.Text = "X";
            this.lbEliminarCliente.Visible = false;
            this.lbEliminarCliente.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbEliminarCliente_LinkClicked);
            this.lbEliminarCliente.Enter += new System.EventHandler(this.lbEliminarCliente_Enter);
            // 
            // lbTotalCambio
            // 
            this.lbTotalCambio.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbTotalCambio.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalCambio.Location = new System.Drawing.Point(161, 271);
            this.lbTotalCambio.Name = "lbTotalCambio";
            this.lbTotalCambio.Size = new System.Drawing.Size(256, 25);
            this.lbTotalCambio.TabIndex = 120;
            this.lbTotalCambio.Text = "$0.00";
            this.lbTotalCambio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCambio
            // 
            this.lbCambio.AutoSize = true;
            this.lbCambio.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCambio.Location = new System.Drawing.Point(258, 248);
            this.lbCambio.Name = "lbCambio";
            this.lbCambio.Size = new System.Drawing.Size(62, 17);
            this.lbCambio.TabIndex = 119;
            this.lbCambio.Text = "Cambio";
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(14, 239);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(550, 2);
            this.label1.TabIndex = 118;
            // 
            // lbTotalCredito
            // 
            this.lbTotalCredito.AutoSize = true;
            this.lbTotalCredito.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalCredito.ForeColor = System.Drawing.Color.Red;
            this.lbTotalCredito.Location = new System.Drawing.Point(104, 249);
            this.lbTotalCredito.Name = "lbTotalCredito";
            this.lbTotalCredito.Size = new System.Drawing.Size(29, 16);
            this.lbTotalCredito.TabIndex = 117;
            this.lbTotalCredito.Text = "0.00";
            this.lbTotalCredito.Visible = false;
            // 
            // lbCredito
            // 
            this.lbCredito.AutoSize = true;
            this.lbCredito.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCredito.ForeColor = System.Drawing.Color.Red;
            this.lbCredito.Location = new System.Drawing.Point(11, 249);
            this.lbCredito.Name = "lbCredito";
            this.lbCredito.Size = new System.Drawing.Size(98, 16);
            this.lbCredito.TabIndex = 116;
            this.lbCredito.Text = "Total a crédito: $";
            this.lbCredito.Visible = false;
            // 
            // btnCredito
            // 
            this.btnCredito.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCredito.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCredito.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCredito.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnCredito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCredito.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCredito.ForeColor = System.Drawing.Color.White;
            this.btnCredito.Location = new System.Drawing.Point(13, 273);
            this.btnCredito.Name = "btnCredito";
            this.btnCredito.Size = new System.Drawing.Size(125, 28);
            this.btnCredito.TabIndex = 103;
            this.btnCredito.Text = "Asignar Crédito";
            this.btnCredito.UseVisualStyleBackColor = false;
            this.btnCredito.Visible = false;
            this.btnCredito.Click += new System.EventHandler(this.btnCredito_Click);
            this.btnCredito.Enter += new System.EventHandler(this.btnCredito_Enter);
            // 
            // lbCliente
            // 
            this.lbCliente.AutoSize = true;
            this.lbCliente.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCliente.Location = new System.Drawing.Point(22, 217);
            this.lbCliente.Name = "lbCliente";
            this.lbCliente.Size = new System.Drawing.Size(88, 15);
            this.lbCliente.TabIndex = 102;
            this.lbCliente.TabStop = true;
            this.lbCliente.Text = "Asignar cliente";
            this.lbCliente.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lbCliente_LinkClicked);
            this.lbCliente.Enter += new System.EventHandler(this.lbCliente_Enter);
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
            this.btnAceptar.Location = new System.Drawing.Point(434, 320);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(130, 28);
            this.btnAceptar.TabIndex = 104;
            this.btnAceptar.Text = "Terminar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            this.btnAceptar.Enter += new System.EventHandler(this.btnAceptar_Enter);
            // 
            // lbSeparadorDetalle2
            // 
            this.lbSeparadorDetalle2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSeparadorDetalle2.Location = new System.Drawing.Point(13, 209);
            this.lbSeparadorDetalle2.Name = "lbSeparadorDetalle2";
            this.lbSeparadorDetalle2.Size = new System.Drawing.Size(550, 2);
            this.lbSeparadorDetalle2.TabIndex = 115;
            // 
            // lbSeparadorDetalle3
            // 
            this.lbSeparadorDetalle3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSeparadorDetalle3.Location = new System.Drawing.Point(13, 310);
            this.lbSeparadorDetalle3.Name = "lbSeparadorDetalle3";
            this.lbSeparadorDetalle3.Size = new System.Drawing.Size(550, 2);
            this.lbSeparadorDetalle3.TabIndex = 114;
            // 
            // lbSeparadorDetalle1
            // 
            this.lbSeparadorDetalle1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSeparadorDetalle1.Location = new System.Drawing.Point(14, 70);
            this.lbSeparadorDetalle1.Name = "lbSeparadorDetalle1";
            this.lbSeparadorDetalle1.Size = new System.Drawing.Size(550, 2);
            this.lbSeparadorDetalle1.TabIndex = 113;
            // 
            // txtReferencia
            // 
            this.txtReferencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtReferencia.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReferencia.Location = new System.Drawing.Point(109, 168);
            this.txtReferencia.Name = "txtReferencia";
            this.txtReferencia.Size = new System.Drawing.Size(454, 22);
            this.txtReferencia.TabIndex = 101;
            this.txtReferencia.Enter += new System.EventHandler(this.txtReferencia_Enter);
            // 
            // lbReferencia
            // 
            this.lbReferencia.AutoSize = true;
            this.lbReferencia.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbReferencia.Location = new System.Drawing.Point(21, 172);
            this.lbReferencia.Name = "lbReferencia";
            this.lbReferencia.Size = new System.Drawing.Size(66, 16);
            this.lbReferencia.TabIndex = 112;
            this.lbReferencia.Text = "Referencia";
            // 
            // lbTransferencia
            // 
            this.lbTransferencia.AutoSize = true;
            this.lbTransferencia.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTransferencia.Location = new System.Drawing.Point(202, 95);
            this.lbTransferencia.Name = "lbTransferencia";
            this.lbTransferencia.Size = new System.Drawing.Size(79, 16);
            this.lbTransferencia.TabIndex = 109;
            this.lbTransferencia.Text = "Transferencia";
            // 
            // lbCheque
            // 
            this.lbCheque.AutoSize = true;
            this.lbCheque.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCheque.Location = new System.Drawing.Point(307, 95);
            this.lbCheque.Name = "lbCheque";
            this.lbCheque.Size = new System.Drawing.Size(53, 16);
            this.lbCheque.TabIndex = 110;
            this.lbCheque.Text = "Cheque";
            // 
            // lbVales
            // 
            this.lbVales.AutoSize = true;
            this.lbVales.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVales.Location = new System.Drawing.Point(415, 95);
            this.lbVales.Name = "lbVales";
            this.lbVales.Size = new System.Drawing.Size(36, 16);
            this.lbVales.TabIndex = 111;
            this.lbVales.Text = "Vales";
            // 
            // lbTarjeta
            // 
            this.lbTarjeta.AutoSize = true;
            this.lbTarjeta.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTarjeta.Location = new System.Drawing.Point(124, 95);
            this.lbTarjeta.Name = "lbTarjeta";
            this.lbTarjeta.Size = new System.Drawing.Size(45, 16);
            this.lbTarjeta.TabIndex = 108;
            this.lbTarjeta.Text = "Tarjeta";
            // 
            // lbEfectivo
            // 
            this.lbEfectivo.AutoSize = true;
            this.lbEfectivo.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEfectivo.Location = new System.Drawing.Point(28, 95);
            this.lbEfectivo.Name = "lbEfectivo";
            this.lbEfectivo.Size = new System.Drawing.Size(52, 16);
            this.lbEfectivo.TabIndex = 107;
            this.lbEfectivo.Text = "Efectivo";
            // 
            // txtTransferencia
            // 
            this.txtTransferencia.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTransferencia.Location = new System.Drawing.Point(204, 126);
            this.txtTransferencia.MaxLength = 10;
            this.txtTransferencia.Name = "txtTransferencia";
            this.txtTransferencia.Size = new System.Drawing.Size(80, 22);
            this.txtTransferencia.TabIndex = 98;
            this.txtTransferencia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTransferencia.Click += new System.EventHandler(this.txtTransferencia_Click);
            this.txtTransferencia.TextChanged += new System.EventHandler(this.txtTransferencia_TextChanged);
            this.txtTransferencia.Enter += new System.EventHandler(this.txtTransferencia_Enter);
            this.txtTransferencia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTransferencia_KeyDown);
            this.txtTransferencia.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTransferencia_KeyUp);
            // 
            // txtCheque
            // 
            this.txtCheque.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheque.Location = new System.Drawing.Point(299, 126);
            this.txtCheque.MaxLength = 10;
            this.txtCheque.Name = "txtCheque";
            this.txtCheque.Size = new System.Drawing.Size(80, 22);
            this.txtCheque.TabIndex = 99;
            this.txtCheque.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCheque.Click += new System.EventHandler(this.txtCheque_Click);
            this.txtCheque.TextChanged += new System.EventHandler(this.txtCheque_TextChanged);
            this.txtCheque.Enter += new System.EventHandler(this.txtCheque_Enter);
            this.txtCheque.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheque_KeyDown);
            this.txtCheque.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCheque_KeyUp);
            // 
            // txtVales
            // 
            this.txtVales.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVales.Location = new System.Drawing.Point(393, 126);
            this.txtVales.MaxLength = 10;
            this.txtVales.Name = "txtVales";
            this.txtVales.Size = new System.Drawing.Size(80, 22);
            this.txtVales.TabIndex = 100;
            this.txtVales.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtVales.Click += new System.EventHandler(this.txtVales_Click);
            this.txtVales.TextChanged += new System.EventHandler(this.txtVales_TextChanged);
            this.txtVales.Enter += new System.EventHandler(this.txtVales_Enter);
            this.txtVales.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVales_KeyDown);
            this.txtVales.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtVales_KeyUp);
            // 
            // txtTarjeta
            // 
            this.txtTarjeta.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTarjeta.Location = new System.Drawing.Point(109, 126);
            this.txtTarjeta.MaxLength = 10;
            this.txtTarjeta.Name = "txtTarjeta";
            this.txtTarjeta.Size = new System.Drawing.Size(80, 22);
            this.txtTarjeta.TabIndex = 97;
            this.txtTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTarjeta.Click += new System.EventHandler(this.txtTarjeta_Click);
            this.txtTarjeta.TextChanged += new System.EventHandler(this.txtTarjeta_TextChanged);
            this.txtTarjeta.Enter += new System.EventHandler(this.txtTarjeta_Enter);
            this.txtTarjeta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTarjeta_KeyDown);
            this.txtTarjeta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTarjeta_KeyUp);
            // 
            // txtEfectivo
            // 
            this.txtEfectivo.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEfectivo.Location = new System.Drawing.Point(13, 126);
            this.txtEfectivo.MaxLength = 10;
            this.txtEfectivo.Name = "txtEfectivo";
            this.txtEfectivo.Size = new System.Drawing.Size(80, 22);
            this.txtEfectivo.TabIndex = 96;
            this.txtEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEfectivo.Click += new System.EventHandler(this.txtEfectivo_Click);
            this.txtEfectivo.TextChanged += new System.EventHandler(this.txtEfectivo_TextChanged);
            this.txtEfectivo.Enter += new System.EventHandler(this.txtEfectivo_Enter);
            this.txtEfectivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEfectivo_KeyDown);
            this.txtEfectivo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEfectivo_KeyUp);
            // 
            // txtTotalVenta
            // 
            this.txtTotalVenta.AutoSize = true;
            this.txtTotalVenta.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalVenta.Location = new System.Drawing.Point(256, 37);
            this.txtTotalVenta.Name = "txtTotalVenta";
            this.txtTotalVenta.Size = new System.Drawing.Size(66, 25);
            this.txtTotalVenta.TabIndex = 106;
            this.txtTotalVenta.Text = "$0.00";
            // 
            // tituloDetalle
            // 
            this.tituloDetalle.AutoSize = true;
            this.tituloDetalle.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloDetalle.Location = new System.Drawing.Point(241, 12);
            this.tituloDetalle.Name = "tituloDetalle";
            this.tituloDetalle.Size = new System.Drawing.Size(96, 17);
            this.tituloDetalle.TabIndex = 105;
            this.tituloDetalle.Text = "Total a pagar";
            // 
            // txtCredito
            // 
            this.txtCredito.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCredito.Location = new System.Drawing.Point(486, 126);
            this.txtCredito.MaxLength = 10;
            this.txtCredito.Name = "txtCredito";
            this.txtCredito.Size = new System.Drawing.Size(80, 22);
            this.txtCredito.TabIndex = 101;
            this.txtCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCredito.TextChanged += new System.EventHandler(this.txtCredito_TextChanged);
            this.txtCredito.Enter += new System.EventHandler(this.txtCredito_Enter);
            this.txtCredito.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCredito_KeyDown);
            this.txtCredito.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCredito_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(502, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 123;
            this.label2.Text = "Crédito";
            // 
            // DetalleVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 361);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCredito);
            this.Controls.Add(this.lbEliminarCliente);
            this.Controls.Add(this.lbTotalCambio);
            this.Controls.Add(this.lbCambio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbTotalCredito);
            this.Controls.Add(this.lbCredito);
            this.Controls.Add(this.btnCredito);
            this.Controls.Add(this.lbCliente);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.lbSeparadorDetalle2);
            this.Controls.Add(this.lbSeparadorDetalle3);
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
            this.Controls.Add(this.txtTotalVenta);
            this.Controls.Add(this.tituloDetalle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "DetalleVenta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Detalles de Venta";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DetalleVenta_FormClosing);
            this.Load += new System.EventHandler(this.DetalleVenta_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DetalleVenta_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel lbEliminarCliente;
        private System.Windows.Forms.Label lbTotalCambio;
        private System.Windows.Forms.Label lbCambio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbTotalCredito;
        private System.Windows.Forms.Label lbCredito;
        private System.Windows.Forms.Button btnCredito;
        private System.Windows.Forms.LinkLabel lbCliente;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lbSeparadorDetalle2;
        private System.Windows.Forms.Label lbSeparadorDetalle3;
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
        private System.Windows.Forms.Label txtTotalVenta;
        private System.Windows.Forms.Label tituloDetalle;
        private System.Windows.Forms.TextBox txtCredito;
        private System.Windows.Forms.Label label2;
    }
}