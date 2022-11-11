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
            this.cbYear = new System.Windows.Forms.ComboBox();
            this.cbMonth = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            // 
            // cbYear
            // 
            this.cbYear.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbYear.FormattingEnabled = true;
            this.cbYear.Items.AddRange(new object[] {
            "2019",
            "2020",
            "2021",
            "2022",
            "2023"});
            this.cbYear.Location = new System.Drawing.Point(15, 347);
            this.cbYear.Name = "cbYear";
            this.cbYear.Size = new System.Drawing.Size(153, 24);
            this.cbYear.TabIndex = 11;
            // 
            // cbMonth
            // 
            this.cbMonth.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMonth.FormattingEnabled = true;
            this.cbMonth.Items.AddRange(new object[] {
            "ENERO",
            "FEBRERO",
            "MARZO",
            "ABRIL",
            "MAYO",
            "JUNIO",
            "JULIO",
            "AGOSTO",
            "SEPTIEMBRE",
            "OCTUBRE",
            "NOVIEMBRE",
            "DICIEMBRE"});
            this.cbMonth.Location = new System.Drawing.Point(187, 347);
            this.cbMonth.Name = "cbMonth";
            this.cbMonth.Size = new System.Drawing.Size(154, 24);
            this.cbMonth.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 327);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 13;
            this.label2.Text = "AÑO";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(184, 327);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "MES";
            // 
            // ReporteCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 453);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbMonth);
            this.Controls.Add(this.cbYear);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
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
        private System.Windows.Forms.ComboBox cbYear;
        private System.Windows.Forms.ComboBox cbMonth;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}