namespace PuntoDeVentaV2
{
    partial class Complemento_pago
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
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_bx_forma_pago = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.datetime_fecha_pago = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnl_info = new System.Windows.Forms.Panel();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.datetime_hora_pago = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_cuenta = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmb_bx_moneda_pago = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_tipo_cambio_pago = new System.Windows.Forms.TextBox();
            this.lb_tipo_cad_pago = new System.Windows.Forms.Label();
            this.cmb_bx_tipo_cad_pago = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_rfc_ordenante = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_banco = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_cuenta_beneficiario = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txt_rfc_beneficiario = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Forma  pago";
            // 
            // cmb_bx_forma_pago
            // 
            this.cmb_bx_forma_pago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_bx_forma_pago.FormattingEnabled = true;
            this.cmb_bx_forma_pago.Location = new System.Drawing.Point(119, 90);
            this.cmb_bx_forma_pago.Name = "cmb_bx_forma_pago";
            this.cmb_bx_forma_pago.Size = new System.Drawing.Size(264, 25);
            this.cmb_bx_forma_pago.TabIndex = 2;
            this.cmb_bx_forma_pago.SelectionChangeCommitted += new System.EventHandler(this.sel_forma_pago);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Fecha pago";
            // 
            // datetime_fecha_pago
            // 
            this.datetime_fecha_pago.CustomFormat = "yyyy-MM-dd";
            this.datetime_fecha_pago.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datetime_fecha_pago.Location = new System.Drawing.Point(119, 23);
            this.datetime_fecha_pago.Name = "datetime_fecha_pago";
            this.datetime_fecha_pago.Size = new System.Drawing.Size(104, 22);
            this.datetime_fecha_pago.TabIndex = 6;
            this.datetime_fecha_pago.Value = new System.DateTime(2020, 2, 1, 0, 0, 0, 0);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DimGray;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(23, 242);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(795, 30);
            this.panel1.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(442, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Incluye impuestos";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(242, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 2;
            this.label8.Text = "Abono";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(113, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "Total";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(9, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 16);
            this.label4.TabIndex = 0;
            this.label4.Text = "Factura";
            // 
            // pnl_info
            // 
            this.pnl_info.AutoScroll = true;
            this.pnl_info.Location = new System.Drawing.Point(23, 278);
            this.pnl_info.Name = "pnl_info";
            this.pnl_info.Size = new System.Drawing.Size(795, 117);
            this.pnl_info.TabIndex = 9;
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_aceptar.BackColor = System.Drawing.Color.Green;
            this.btn_aceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_aceptar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_aceptar.ForeColor = System.Drawing.Color.White;
            this.btn_aceptar.Location = new System.Drawing.Point(627, 444);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(180, 30);
            this.btn_aceptar.TabIndex = 7;
            this.btn_aceptar.Text = "Generar complemento";
            this.btn_aceptar.UseVisualStyleBackColor = false;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancelar.BackColor = System.Drawing.Color.Red;
            this.btn_cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancelar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancelar.ForeColor = System.Drawing.Color.White;
            this.btn_cancelar.Location = new System.Drawing.Point(502, 444);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(110, 30);
            this.btn_cancelar.TabIndex = 8;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(234, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 17);
            this.label6.TabIndex = 7;
            this.label6.Text = "Hora pago";
            // 
            // datetime_hora_pago
            // 
            this.datetime_hora_pago.CustomFormat = "hh:mm tt";
            this.datetime_hora_pago.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.datetime_hora_pago.Location = new System.Drawing.Point(311, 23);
            this.datetime_hora_pago.Name = "datetime_hora_pago";
            this.datetime_hora_pago.ShowUpDown = true;
            this.datetime_hora_pago.Size = new System.Drawing.Size(102, 22);
            this.datetime_hora_pago.TabIndex = 8;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 126);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(99, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "Cta. ordenante";
            // 
            // txt_cuenta
            // 
            this.txt_cuenta.Enabled = false;
            this.txt_cuenta.Location = new System.Drawing.Point(119, 123);
            this.txt_cuenta.MaxLength = 50;
            this.txt_cuenta.Name = "txt_cuenta";
            this.txt_cuenta.Size = new System.Drawing.Size(264, 22);
            this.txt_cuenta.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 17);
            this.label9.TabIndex = 10;
            this.label9.Text = "Moneda pago";
            // 
            // cmb_bx_moneda_pago
            // 
            this.cmb_bx_moneda_pago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_bx_moneda_pago.FormattingEnabled = true;
            this.cmb_bx_moneda_pago.Location = new System.Drawing.Point(119, 52);
            this.cmb_bx_moneda_pago.Name = "cmb_bx_moneda_pago";
            this.cmb_bx_moneda_pago.Size = new System.Drawing.Size(450, 25);
            this.cmb_bx_moneda_pago.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(594, 55);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 17);
            this.label10.TabIndex = 12;
            this.label10.Text = "Tipo cambio";
            // 
            // txt_tipo_cambio_pago
            // 
            this.txt_tipo_cambio_pago.Location = new System.Drawing.Point(681, 52);
            this.txt_tipo_cambio_pago.Name = "txt_tipo_cambio_pago";
            this.txt_tipo_cambio_pago.Size = new System.Drawing.Size(137, 22);
            this.txt_tipo_cambio_pago.TabIndex = 13;
            // 
            // lb_tipo_cad_pago
            // 
            this.lb_tipo_cad_pago.AutoSize = true;
            this.lb_tipo_cad_pago.Location = new System.Drawing.Point(397, 93);
            this.lb_tipo_cad_pago.Name = "lb_tipo_cad_pago";
            this.lb_tipo_cad_pago.Size = new System.Drawing.Size(97, 17);
            this.lb_tipo_cad_pago.TabIndex = 14;
            this.lb_tipo_cad_pago.Text = "Tipo cad. pago";
            // 
            // cmb_bx_tipo_cad_pago
            // 
            this.cmb_bx_tipo_cad_pago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_bx_tipo_cad_pago.FormattingEnabled = true;
            this.cmb_bx_tipo_cad_pago.Location = new System.Drawing.Point(500, 90);
            this.cmb_bx_tipo_cad_pago.Name = "cmb_bx_tipo_cad_pago";
            this.cmb_bx_tipo_cad_pago.Size = new System.Drawing.Size(123, 25);
            this.cmb_bx_tipo_cad_pago.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(398, 126);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 17);
            this.label11.TabIndex = 16;
            this.label11.Text = "RFC ordenante";
            // 
            // txt_rfc_ordenante
            // 
            this.txt_rfc_ordenante.Location = new System.Drawing.Point(500, 123);
            this.txt_rfc_ordenante.MaxLength = 13;
            this.txt_rfc_ordenante.Name = "txt_rfc_ordenante";
            this.txt_rfc_ordenante.Size = new System.Drawing.Size(123, 22);
            this.txt_rfc_ordenante.TabIndex = 17;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(629, 126);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(46, 17);
            this.label12.TabIndex = 18;
            this.label12.Text = "Banco";
            // 
            // txt_banco
            // 
            this.txt_banco.Location = new System.Drawing.Point(681, 123);
            this.txt_banco.MaxLength = 300;
            this.txt_banco.Name = "txt_banco";
            this.txt_banco.Size = new System.Drawing.Size(137, 22);
            this.txt_banco.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 154);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(108, 17);
            this.label13.TabIndex = 20;
            this.label13.Text = "Cta. beneficiario";
            // 
            // txt_cuenta_beneficiario
            // 
            this.txt_cuenta_beneficiario.Location = new System.Drawing.Point(119, 151);
            this.txt_cuenta_beneficiario.MaxLength = 50;
            this.txt_cuenta_beneficiario.Name = "txt_cuenta_beneficiario";
            this.txt_cuenta_beneficiario.Size = new System.Drawing.Size(264, 22);
            this.txt_cuenta_beneficiario.TabIndex = 21;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(389, 154);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(105, 17);
            this.label14.TabIndex = 22;
            this.label14.Text = "RFC beneficiario";
            // 
            // txt_rfc_beneficiario
            // 
            this.txt_rfc_beneficiario.Location = new System.Drawing.Point(500, 151);
            this.txt_rfc_beneficiario.MaxLength = 13;
            this.txt_rfc_beneficiario.Name = "txt_rfc_beneficiario";
            this.txt_rfc_beneficiario.Size = new System.Drawing.Size(123, 22);
            this.txt_rfc_beneficiario.TabIndex = 23;
            // 
            // Complemento_pago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 486);
            this.Controls.Add(this.txt_rfc_beneficiario);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txt_cuenta_beneficiario);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.txt_banco);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.txt_rfc_ordenante);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmb_bx_tipo_cad_pago);
            this.Controls.Add(this.lb_tipo_cad_pago);
            this.Controls.Add(this.txt_tipo_cambio_pago);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.cmb_bx_moneda_pago);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txt_cuenta);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.datetime_hora_pago);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.pnl_info);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.datetime_fecha_pago);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmb_bx_forma_pago);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Complemento_pago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Crear complemento de pago";
            this.Load += new System.EventHandler(this.Complemento_pago_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Complemento_pago_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_bx_forma_pago;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker datetime_fecha_pago;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnl_info;
        private System.Windows.Forms.Button btn_aceptar;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker datetime_hora_pago;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_cuenta;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmb_bx_moneda_pago;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_tipo_cambio_pago;
        private System.Windows.Forms.Label lb_tipo_cad_pago;
        private System.Windows.Forms.ComboBox cmb_bx_tipo_cad_pago;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_rfc_ordenante;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_banco;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_cuenta_beneficiario;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txt_rfc_beneficiario;
    }
}