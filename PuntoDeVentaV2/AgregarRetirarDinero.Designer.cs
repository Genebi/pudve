namespace PuntoDeVentaV2
{
    partial class AgregarRetirarDinero
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
            this.gbContenedor = new System.Windows.Forms.GroupBox();
            this.chkBoxDepositoSaldoInicial = new System.Windows.Forms.CheckBox();
            this.btnRetirarTodoElDinero = new System.Windows.Forms.Button();
            this.cbConceptoConBusqueda = new CustomControlPUDVE.ComboBoxPUDVE();
            this.btnAgregarConcepto = new System.Windows.Forms.Button();
            this.lbCredito = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtVales = new System.Windows.Forms.TextBox();
            this.txtTarjeta = new System.Windows.Forms.TextBox();
            this.txtEfectivo = new System.Windows.Forms.TextBox();
            this.txtCredito = new System.Windows.Forms.TextBox();
            this.txtTrans = new System.Windows.Forms.TextBox();
            this.lbSubtitulo = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.txtCheque = new System.Windows.Forms.TextBox();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.cbConceptos = new System.Windows.Forms.ComboBox();
            this.gbContenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbContenedor
            // 
            this.gbContenedor.Controls.Add(this.chkBoxDepositoSaldoInicial);
            this.gbContenedor.Controls.Add(this.btnRetirarTodoElDinero);
            this.gbContenedor.Controls.Add(this.cbConceptoConBusqueda);
            this.gbContenedor.Controls.Add(this.btnAgregarConcepto);
            this.gbContenedor.Controls.Add(this.lbCredito);
            this.gbContenedor.Controls.Add(this.label9);
            this.gbContenedor.Controls.Add(this.label7);
            this.gbContenedor.Controls.Add(this.label6);
            this.gbContenedor.Controls.Add(this.label5);
            this.gbContenedor.Controls.Add(this.label4);
            this.gbContenedor.Controls.Add(this.txtVales);
            this.gbContenedor.Controls.Add(this.txtTarjeta);
            this.gbContenedor.Controls.Add(this.txtEfectivo);
            this.gbContenedor.Controls.Add(this.txtCredito);
            this.gbContenedor.Controls.Add(this.txtTrans);
            this.gbContenedor.Controls.Add(this.lbSubtitulo);
            this.gbContenedor.Controls.Add(this.btnCancelar);
            this.gbContenedor.Controls.Add(this.btnAceptar);
            this.gbContenedor.Controls.Add(this.txtCheque);
            this.gbContenedor.Controls.Add(this.lbTitulo);
            this.gbContenedor.Location = new System.Drawing.Point(12, 4);
            this.gbContenedor.Name = "gbContenedor";
            this.gbContenedor.Size = new System.Drawing.Size(410, 315);
            this.gbContenedor.TabIndex = 0;
            this.gbContenedor.TabStop = false;
            // 
            // chkBoxDepositoSaldoInicial
            // 
            this.chkBoxDepositoSaldoInicial.AutoSize = true;
            this.chkBoxDepositoSaldoInicial.Location = new System.Drawing.Point(126, 166);
            this.chkBoxDepositoSaldoInicial.Margin = new System.Windows.Forms.Padding(2);
            this.chkBoxDepositoSaldoInicial.Name = "chkBoxDepositoSaldoInicial";
            this.chkBoxDepositoSaldoInicial.Size = new System.Drawing.Size(167, 17);
            this.chkBoxDepositoSaldoInicial.TabIndex = 221;
            this.chkBoxDepositoSaldoInicial.Text = "Agregar a saldo inicial de caja";
            this.chkBoxDepositoSaldoInicial.UseVisualStyleBackColor = true;
            this.chkBoxDepositoSaldoInicial.Visible = false;
            // 
            // btnRetirarTodoElDinero
            // 
            this.btnRetirarTodoElDinero.BackColor = System.Drawing.Color.Green;
            this.btnRetirarTodoElDinero.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRetirarTodoElDinero.FlatAppearance.BorderSize = 0;
            this.btnRetirarTodoElDinero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetirarTodoElDinero.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnRetirarTodoElDinero.ForeColor = System.Drawing.Color.White;
            this.btnRetirarTodoElDinero.Location = new System.Drawing.Point(125, 162);
            this.btnRetirarTodoElDinero.Name = "btnRetirarTodoElDinero";
            this.btnRetirarTodoElDinero.Size = new System.Drawing.Size(160, 24);
            this.btnRetirarTodoElDinero.TabIndex = 220;
            this.btnRetirarTodoElDinero.Text = "Retirar Todo";
            this.btnRetirarTodoElDinero.UseVisualStyleBackColor = false;
            this.btnRetirarTodoElDinero.Click += new System.EventHandler(this.btnRetirarTodoElDinero_Click);
            // 
            // cbConceptoConBusqueda
            // 
            this.cbConceptoConBusqueda.FormattingEnabled = true;
            this.cbConceptoConBusqueda.Location = new System.Drawing.Point(56, 227);
            this.cbConceptoConBusqueda.MaxDropDownItems = 12;
            this.cbConceptoConBusqueda.Name = "cbConceptoConBusqueda";
            this.cbConceptoConBusqueda.Size = new System.Drawing.Size(293, 21);
            this.cbConceptoConBusqueda.TabIndex = 219;
            // 
            // btnAgregarConcepto
            // 
            this.btnAgregarConcepto.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnAgregarConcepto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarConcepto.FlatAppearance.BorderSize = 0;
            this.btnAgregarConcepto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarConcepto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarConcepto.ForeColor = System.Drawing.Color.White;
            this.btnAgregarConcepto.Image = global::PuntoDeVentaV2.Properties.Resources.plus_square;
            this.btnAgregarConcepto.Location = new System.Drawing.Point(355, 225);
            this.btnAgregarConcepto.Name = "btnAgregarConcepto";
            this.btnAgregarConcepto.Size = new System.Drawing.Size(28, 25);
            this.btnAgregarConcepto.TabIndex = 218;
            this.btnAgregarConcepto.UseVisualStyleBackColor = false;
            this.btnAgregarConcepto.Click += new System.EventHandler(this.btnAgregarConcepto_Click);
            // 
            // lbCredito
            // 
            this.lbCredito.AutoSize = true;
            this.lbCredito.Enabled = false;
            this.lbCredito.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCredito.Location = new System.Drawing.Point(200, 116);
            this.lbCredito.Name = "lbCredito";
            this.lbCredito.Size = new System.Drawing.Size(57, 17);
            this.lbCredito.TabIndex = 216;
            this.lbCredito.Text = "Crédito";
            this.lbCredito.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(200, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(91, 17);
            this.label9.TabIndex = 215;
            this.label9.Text = "Transferencia";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(200, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 17);
            this.label7.TabIndex = 214;
            this.label7.Text = "Cheque";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 17);
            this.label6.TabIndex = 213;
            this.label6.Text = "Vales";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 17);
            this.label5.TabIndex = 212;
            this.label5.Text = "Tarjeta";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(9, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 17);
            this.label4.TabIndex = 211;
            this.label4.Text = "Efectivo";
            // 
            // txtVales
            // 
            this.txtVales.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtVales.Location = new System.Drawing.Point(71, 113);
            this.txtVales.Name = "txtVales";
            this.txtVales.ShortcutsEnabled = false;
            this.txtVales.Size = new System.Drawing.Size(103, 23);
            this.txtVales.TabIndex = 3;
            this.txtVales.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtVales.TextChanged += new System.EventHandler(this.txtVales_TextChanged);
            this.txtVales.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVales_KeyDown);
            this.txtVales.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtVales_KeyPress);
            this.txtVales.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtVales_KeyUp);
            // 
            // txtTarjeta
            // 
            this.txtTarjeta.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTarjeta.Location = new System.Drawing.Point(71, 78);
            this.txtTarjeta.Name = "txtTarjeta";
            this.txtTarjeta.ShortcutsEnabled = false;
            this.txtTarjeta.Size = new System.Drawing.Size(103, 23);
            this.txtTarjeta.TabIndex = 2;
            this.txtTarjeta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTarjeta.TextChanged += new System.EventHandler(this.txtTarjeta_TextChanged);
            this.txtTarjeta.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTarjeta_KeyDown);
            this.txtTarjeta.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTarjeta_KeyPress);
            this.txtTarjeta.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTarjeta_KeyUp);
            // 
            // txtEfectivo
            // 
            this.txtEfectivo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEfectivo.Location = new System.Drawing.Point(71, 42);
            this.txtEfectivo.Name = "txtEfectivo";
            this.txtEfectivo.ShortcutsEnabled = false;
            this.txtEfectivo.Size = new System.Drawing.Size(103, 23);
            this.txtEfectivo.TabIndex = 1;
            this.txtEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEfectivo.TextChanged += new System.EventHandler(this.txtEfectivo_TextChanged);
            this.txtEfectivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEfectivo_KeyDown);
            this.txtEfectivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEfectivo_KeyPress);
            this.txtEfectivo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEfectivo_KeyUp);
            // 
            // txtCredito
            // 
            this.txtCredito.Enabled = false;
            this.txtCredito.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCredito.Location = new System.Drawing.Point(297, 113);
            this.txtCredito.Name = "txtCredito";
            this.txtCredito.ShortcutsEnabled = false;
            this.txtCredito.Size = new System.Drawing.Size(103, 23);
            this.txtCredito.TabIndex = 6;
            this.txtCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCredito.Visible = false;
            this.txtCredito.TextChanged += new System.EventHandler(this.txtCredito_TextChanged);
            this.txtCredito.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCredito_KeyPress);
            this.txtCredito.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCredito_KeyUp);
            // 
            // txtTrans
            // 
            this.txtTrans.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTrans.Location = new System.Drawing.Point(297, 78);
            this.txtTrans.Name = "txtTrans";
            this.txtTrans.ShortcutsEnabled = false;
            this.txtTrans.Size = new System.Drawing.Size(103, 23);
            this.txtTrans.TabIndex = 5;
            this.txtTrans.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTrans.TextChanged += new System.EventHandler(this.txtTrans_TextChanged);
            this.txtTrans.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTrans_KeyDown);
            this.txtTrans.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTrans_KeyPress);
            this.txtTrans.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTrans_KeyUp);
            // 
            // lbSubtitulo
            // 
            this.lbSubtitulo.AutoSize = true;
            this.lbSubtitulo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSubtitulo.Location = new System.Drawing.Point(135, 198);
            this.lbSubtitulo.Name = "lbSubtitulo";
            this.lbSubtitulo.Size = new System.Drawing.Size(160, 17);
            this.lbSubtitulo.TabIndex = 210;
            this.lbSubtitulo.Text = "Concepto del depósito";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancelar.Location = new System.Drawing.Point(56, 273);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(160, 24);
            this.btnCancelar.TabIndex = 9;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.Green;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(222, 273);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(160, 24);
            this.btnAceptar.TabIndex = 8;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // txtCheque
            // 
            this.txtCheque.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheque.Location = new System.Drawing.Point(297, 41);
            this.txtCheque.Name = "txtCheque";
            this.txtCheque.ShortcutsEnabled = false;
            this.txtCheque.Size = new System.Drawing.Size(103, 23);
            this.txtCheque.TabIndex = 4;
            this.txtCheque.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCheque.TextChanged += new System.EventHandler(this.txtCheque_TextChanged);
            this.txtCheque.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCheque_KeyDown);
            this.txtCheque.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCheque_KeyPress);
            this.txtCheque.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCheque_KeyUp);
            // 
            // lbTitulo
            // 
            this.lbTitulo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbTitulo.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.Location = new System.Drawing.Point(111, 11);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(201, 20);
            this.lbTitulo.TabIndex = 209;
            this.lbTitulo.Text = "Cantidad a depositar";
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbConceptos
            // 
            this.cbConceptos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConceptos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbConceptos.FormattingEnabled = true;
            this.cbConceptos.Location = new System.Drawing.Point(0, 2);
            this.cbConceptos.Name = "cbConceptos";
            this.cbConceptos.Size = new System.Drawing.Size(293, 25);
            this.cbConceptos.TabIndex = 217;
            this.cbConceptos.Visible = false;
            // 
            // AgregarRetirarDinero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 331);
            this.Controls.Add(this.gbContenedor);
            this.Controls.Add(this.cbConceptos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgregarRetirarDinero";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AgregarRetirarDinero_FormClosed);
            this.Load += new System.EventHandler(this.AgregarRetirarDinero_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AgregarRetirarDinero_KeyDown);
            this.gbContenedor.ResumeLayout(false);
            this.gbContenedor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbContenedor;
        private System.Windows.Forms.Label lbCredito;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtVales;
        private System.Windows.Forms.TextBox txtTarjeta;
        private System.Windows.Forms.TextBox txtEfectivo;
        private System.Windows.Forms.TextBox txtCredito;
        private System.Windows.Forms.TextBox txtTrans;
        private System.Windows.Forms.Label lbSubtitulo;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TextBox txtCheque;
        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Button btnAgregarConcepto;
        private System.Windows.Forms.ComboBox cbConceptos;
        private CustomControlPUDVE.ComboBoxPUDVE cbConceptoConBusqueda;
        private System.Windows.Forms.Button btnRetirarTodoElDinero;
        private System.Windows.Forms.CheckBox chkBoxDepositoSaldoInicial;
    }
}