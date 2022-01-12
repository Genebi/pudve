namespace PuntoDeVentaV2
{
    partial class FechasReportes
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
            this.primerDatePicker = new System.Windows.Forms.DateTimePicker();
            this.segundoDatePicker = new System.Windows.Forms.DateTimePicker();
            this.lbPrimerDP = new System.Windows.Forms.Label();
            this.lbSegundoDP = new System.Windows.Forms.Label();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.primerSeparador = new System.Windows.Forms.Label();
            this.cbConceptos = new System.Windows.Forms.ComboBox();
            this.cbEmpleados = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // primerDatePicker
            // 
            this.primerDatePicker.CalendarFont = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.primerDatePicker.CustomFormat = "yyyy-MM-dd";
            this.primerDatePicker.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.primerDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.primerDatePicker.Location = new System.Drawing.Point(133, 59);
            this.primerDatePicker.Name = "primerDatePicker";
            this.primerDatePicker.Size = new System.Drawing.Size(181, 23);
            this.primerDatePicker.TabIndex = 0;
            // 
            // segundoDatePicker
            // 
            this.segundoDatePicker.CalendarFont = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.segundoDatePicker.CustomFormat = "yyyy-MM-dd";
            this.segundoDatePicker.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.segundoDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.segundoDatePicker.Location = new System.Drawing.Point(133, 92);
            this.segundoDatePicker.Name = "segundoDatePicker";
            this.segundoDatePicker.Size = new System.Drawing.Size(181, 23);
            this.segundoDatePicker.TabIndex = 1;
            // 
            // lbPrimerDP
            // 
            this.lbPrimerDP.AutoSize = true;
            this.lbPrimerDP.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrimerDP.Location = new System.Drawing.Point(60, 61);
            this.lbPrimerDP.Name = "lbPrimerDP";
            this.lbPrimerDP.Size = new System.Drawing.Size(39, 20);
            this.lbPrimerDP.TabIndex = 2;
            this.lbPrimerDP.Text = "DEL:";
            // 
            // lbSegundoDP
            // 
            this.lbSegundoDP.AutoSize = true;
            this.lbSegundoDP.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSegundoDP.Location = new System.Drawing.Point(60, 94);
            this.lbSegundoDP.Name = "lbSegundoDP";
            this.lbSegundoDP.Size = new System.Drawing.Size(57, 20);
            this.lbSegundoDP.TabIndex = 3;
            this.lbSegundoDP.Text = "HASTA:";
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(64, 185);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(108, 25);
            this.btnCancelar.TabIndex = 106;
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
            this.btnAceptar.Location = new System.Drawing.Point(206, 185);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(108, 25);
            this.btnAceptar.TabIndex = 113;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // primerSeparador
            // 
            this.primerSeparador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.primerSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.primerSeparador.Location = new System.Drawing.Point(8, 170);
            this.primerSeparador.Name = "primerSeparador";
            this.primerSeparador.Size = new System.Drawing.Size(360, 2);
            this.primerSeparador.TabIndex = 114;
            this.primerSeparador.Text = "REPORTES";
            this.primerSeparador.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbConceptos
            // 
            this.cbConceptos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbConceptos.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbConceptos.FormattingEnabled = true;
            this.cbConceptos.Location = new System.Drawing.Point(64, 23);
            this.cbConceptos.Name = "cbConceptos";
            this.cbConceptos.Size = new System.Drawing.Size(250, 25);
            this.cbConceptos.TabIndex = 115;
            this.cbConceptos.Visible = false;
            // 
            // cbEmpleados
            // 
            this.cbEmpleados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEmpleados.FormattingEnabled = true;
            this.cbEmpleados.Location = new System.Drawing.Point(64, 134);
            this.cbEmpleados.Name = "cbEmpleados";
            this.cbEmpleados.Size = new System.Drawing.Size(250, 21);
            this.cbEmpleados.TabIndex = 116;
            this.cbEmpleados.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cbEmpleados_DrawItem);
            // 
            // FechasReportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 236);
            this.Controls.Add(this.cbEmpleados);
            this.Controls.Add(this.cbConceptos);
            this.Controls.Add(this.primerSeparador);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.lbSegundoDP);
            this.Controls.Add(this.lbPrimerDP);
            this.Controls.Add(this.segundoDatePicker);
            this.Controls.Add(this.primerDatePicker);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FechasReportes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Fechas de Reporte";
            this.Load += new System.EventHandler(this.FechasReportes_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FechasReportes_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker primerDatePicker;
        private System.Windows.Forms.DateTimePicker segundoDatePicker;
        private System.Windows.Forms.Label lbPrimerDP;
        private System.Windows.Forms.Label lbSegundoDP;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label primerSeparador;
        private System.Windows.Forms.ComboBox cbConceptos;
        private System.Windows.Forms.ComboBox cbEmpleados;
    }
}