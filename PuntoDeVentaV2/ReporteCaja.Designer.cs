namespace PuntoDeVentaV2
{
    partial class ReporteCaja
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
            this.lbTitulo = new System.Windows.Forms.Label();
            this.rbDineroAgregado = new System.Windows.Forms.RadioButton();
            this.rbDineroRetirado = new System.Windows.Forms.RadioButton();
            this.lbSeparador = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rbHabilitados = new System.Windows.Forms.RadioButton();
            this.rbDeshabilitados = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lbConceptos = new System.Windows.Forms.Label();
            this.clbConceptos = new System.Windows.Forms.CheckedListBox();
            this.cbTodos = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panelFechaHora = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpHoraFin = new System.Windows.Forms.DateTimePicker();
            this.dtpHoraInicio = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.btnGenerarReporte = new PuntoDeVentaV2.BotonRedondo();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelFechaHora.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbTitulo
            // 
            this.lbTitulo.AutoEllipsis = true;
            this.lbTitulo.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.Location = new System.Drawing.Point(15, 26);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(450, 21);
            this.lbTitulo.TabIndex = 1;
            this.lbTitulo.Text = "REPORTE DE CAJA";
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbDineroAgregado
            // 
            this.rbDineroAgregado.AutoSize = true;
            this.rbDineroAgregado.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDineroAgregado.Location = new System.Drawing.Point(35, 14);
            this.rbDineroAgregado.Name = "rbDineroAgregado";
            this.rbDineroAgregado.Size = new System.Drawing.Size(135, 20);
            this.rbDineroAgregado.TabIndex = 2;
            this.rbDineroAgregado.Text = "DINERO AGREGADO";
            this.rbDineroAgregado.UseVisualStyleBackColor = true;
            this.rbDineroAgregado.CheckedChanged += new System.EventHandler(this.rbDineroAgregado_CheckedChanged);
            // 
            // rbDineroRetirado
            // 
            this.rbDineroRetirado.AutoSize = true;
            this.rbDineroRetirado.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDineroRetirado.Location = new System.Drawing.Point(256, 14);
            this.rbDineroRetirado.Name = "rbDineroRetirado";
            this.rbDineroRetirado.Size = new System.Drawing.Size(123, 20);
            this.rbDineroRetirado.TabIndex = 3;
            this.rbDineroRetirado.Text = "DINERO RETIRADO";
            this.rbDineroRetirado.UseVisualStyleBackColor = true;
            this.rbDineroRetirado.CheckedChanged += new System.EventHandler(this.rbDineroRetirado_CheckedChanged);
            // 
            // lbSeparador
            // 
            this.lbSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbSeparador.Location = new System.Drawing.Point(15, 59);
            this.lbSeparador.Name = "lbSeparador";
            this.lbSeparador.Size = new System.Drawing.Size(450, 2);
            this.lbSeparador.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbDineroAgregado);
            this.groupBox1.Controls.Add(this.rbDineroRetirado);
            this.groupBox1.Location = new System.Drawing.Point(15, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 40);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbHabilitados);
            this.groupBox2.Controls.Add(this.rbDeshabilitados);
            this.groupBox2.Location = new System.Drawing.Point(15, 121);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(450, 40);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            // 
            // rbHabilitados
            // 
            this.rbHabilitados.AutoSize = true;
            this.rbHabilitados.Enabled = false;
            this.rbHabilitados.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbHabilitados.Location = new System.Drawing.Point(35, 14);
            this.rbHabilitados.Name = "rbHabilitados";
            this.rbHabilitados.Size = new System.Drawing.Size(93, 20);
            this.rbHabilitados.TabIndex = 4;
            this.rbHabilitados.Text = "HABILITADOS";
            this.rbHabilitados.UseVisualStyleBackColor = true;
            this.rbHabilitados.CheckedChanged += new System.EventHandler(this.rbHabilitados_CheckedChanged);
            // 
            // rbDeshabilitados
            // 
            this.rbDeshabilitados.AutoSize = true;
            this.rbDeshabilitados.Enabled = false;
            this.rbDeshabilitados.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDeshabilitados.Location = new System.Drawing.Point(256, 14);
            this.rbDeshabilitados.Name = "rbDeshabilitados";
            this.rbDeshabilitados.Size = new System.Drawing.Size(113, 20);
            this.rbDeshabilitados.TabIndex = 5;
            this.rbDeshabilitados.Text = "DESHABILITADOS";
            this.rbDeshabilitados.UseVisualStyleBackColor = true;
            this.rbDeshabilitados.CheckedChanged += new System.EventHandler(this.rbDeshabilitados_CheckedChanged);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(14, 170);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(450, 2);
            this.label1.TabIndex = 6;
            // 
            // lbConceptos
            // 
            this.lbConceptos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConceptos.Location = new System.Drawing.Point(15, 104);
            this.lbConceptos.Name = "lbConceptos";
            this.lbConceptos.Size = new System.Drawing.Size(450, 23);
            this.lbConceptos.TabIndex = 8;
            this.lbConceptos.Text = "CONCEPTOS";
            this.lbConceptos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // clbConceptos
            // 
            this.clbConceptos.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clbConceptos.FormattingEnabled = true;
            this.clbConceptos.Location = new System.Drawing.Point(15, 204);
            this.clbConceptos.Name = "clbConceptos";
            this.clbConceptos.Size = new System.Drawing.Size(449, 116);
            this.clbConceptos.TabIndex = 9;
            this.clbConceptos.Visible = false;
            // 
            // cbTodos
            // 
            this.cbTodos.AutoSize = true;
            this.cbTodos.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTodos.Location = new System.Drawing.Point(18, 181);
            this.cbTodos.Name = "cbTodos";
            this.cbTodos.Size = new System.Drawing.Size(140, 19);
            this.cbTodos.TabIndex = 10;
            this.cbTodos.Text = "SELECCIONAR TODOS";
            this.cbTodos.UseVisualStyleBackColor = true;
            this.cbTodos.Visible = false;
            this.cbTodos.CheckedChanged += new System.EventHandler(this.cbTodos_CheckedChanged);
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(13, 450);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(450, 2);
            this.label8.TabIndex = 22;
            // 
            // panelFechaHora
            // 
            this.panelFechaHora.Controls.Add(this.label7);
            this.panelFechaHora.Controls.Add(this.label6);
            this.panelFechaHora.Controls.Add(this.label5);
            this.panelFechaHora.Controls.Add(this.label4);
            this.panelFechaHora.Controls.Add(this.dtpHoraFin);
            this.panelFechaHora.Controls.Add(this.dtpHoraInicio);
            this.panelFechaHora.Controls.Add(this.label3);
            this.panelFechaHora.Controls.Add(this.label2);
            this.panelFechaHora.Controls.Add(this.cbMonth);
            this.panelFechaHora.Controls.Add(this.cbYear);
            this.panelFechaHora.Location = new System.Drawing.Point(15, 326);
            this.panelFechaHora.Name = "panelFechaHora";
            this.panelFechaHora.Size = new System.Drawing.Size(448, 121);
            this.panelFechaHora.TabIndex = 23;
            this.panelFechaHora.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(286, 17);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 16);
            this.label7.TabIndex = 31;
            this.label7.Text = "SELECCIONAR HORARIO";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(21, 17);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(121, 16);
            this.label6.TabIndex = 30;
            this.label6.Text = "SELECCIONAR FECHA";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(286, 82);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 16);
            this.label5.TabIndex = 29;
            this.label5.Text = "A LAS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(286, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 16);
            this.label4.TabIndex = 28;
            this.label4.Text = "DE";
            // 
            // dtpHoraFin
            // 
            this.dtpHoraFin.CalendarFont = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHoraFin.CustomFormat = "HH:mm:ss";
            this.dtpHoraFin.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHoraFin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHoraFin.Location = new System.Drawing.Point(328, 82);
            this.dtpHoraFin.Name = "dtpHoraFin";
            this.dtpHoraFin.ShowUpDown = true;
            this.dtpHoraFin.Size = new System.Drawing.Size(96, 21);
            this.dtpHoraFin.TabIndex = 27;
            // 
            // dtpHoraInicio
            // 
            this.dtpHoraInicio.CalendarFont = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHoraInicio.CustomFormat = "HH:mm:ss";
            this.dtpHoraInicio.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHoraInicio.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHoraInicio.Location = new System.Drawing.Point(328, 52);
            this.dtpHoraInicio.Name = "dtpHoraInicio";
            this.dtpHoraInicio.ShowUpDown = true;
            this.dtpHoraInicio.Size = new System.Drawing.Size(96, 21);
            this.dtpHoraInicio.TabIndex = 26;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(21, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 16);
            this.label3.TabIndex = 25;
            this.label3.Text = "MES";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 24;
            this.label2.Text = "AÑO";
            // 
            // cbMonth
            // 
            this.cbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbMonth.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Location = new System.Drawing.Point(57, 79);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(144, 24);
            this.cbMonth.TabIndex = 23;
            // 
            // cbYear
            // 
            this.cbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbYear.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Location = new System.Drawing.Point(57, 49);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(144, 24);
            this.cbYear.TabIndex = 22;
            // 
            // btnGenerarReporte
            // 
            this.btnGenerarReporte.BackColor = System.Drawing.Color.Firebrick;
            this.btnGenerarReporte.BackGroundColor = System.Drawing.Color.Firebrick;
            this.btnGenerarReporte.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnGenerarReporte.BorderRadius = 40;
            this.btnGenerarReporte.BorderSize = 0;
            this.btnGenerarReporte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGenerarReporte.FlatAppearance.BorderSize = 0;
            this.btnGenerarReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGenerarReporte.ForeColor = System.Drawing.Color.White;
            this.btnGenerarReporte.Location = new System.Drawing.Point(168, 459);
            this.btnGenerarReporte.Name = "btnGenerarReporte";
            this.btnGenerarReporte.Size = new System.Drawing.Size(150, 40);
            this.btnGenerarReporte.TabIndex = 15;
            this.btnGenerarReporte.Text = "GENERAR REPORTE";
            this.btnGenerarReporte.TextColor = System.Drawing.Color.White;
            this.btnGenerarReporte.UseVisualStyleBackColor = false;
            this.btnGenerarReporte.Click += new System.EventHandler(this.btnGenerarReporte_Click);
            // 
            // ReporteCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 511);
            this.Controls.Add(this.panelFechaHora);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnGenerarReporte);
            this.Controls.Add(this.cbTodos);
            this.Controls.Add(this.clbConceptos);
            this.Controls.Add(this.lbConceptos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbSeparador);
            this.Controls.Add(this.lbTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ReporteCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ReporteCaja_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelFechaHora.ResumeLayout(false);
            this.panelFechaHora.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.RadioButton rbDineroAgregado;
        private System.Windows.Forms.RadioButton rbDineroRetirado;
        private System.Windows.Forms.Label lbSeparador;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rbHabilitados;
        private System.Windows.Forms.RadioButton rbDeshabilitados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbConceptos;
        private System.Windows.Forms.CheckedListBox clbConceptos;
        private System.Windows.Forms.CheckBox cbTodos;
        private BotonRedondo btnGenerarReporte;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panelFechaHora;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpHoraFin;
        private System.Windows.Forms.DateTimePicker dtpHoraInicio;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.ComboBox cbYear;
    }
}