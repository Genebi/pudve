namespace PuntoDeVentaV2
{
    partial class DatosOrden
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
            this.lbMinutos = new System.Windows.Forms.Label();
            this.lbHoras = new System.Windows.Forms.Label();
            this.lbDias = new System.Windows.Forms.Label();
            this.nudMinutos = new System.Windows.Forms.NumericUpDown();
            this.nudHoras = new System.Windows.Forms.NumericUpDown();
            this.nudDias = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaEntrega = new System.Windows.Forms.DateTimePicker();
            this.btnAceptar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHoras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDias)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitulo
            // 
            this.lbTitulo.AutoSize = true;
            this.lbTitulo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.Location = new System.Drawing.Point(89, 40);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(203, 17);
            this.lbTitulo.TabIndex = 23;
            this.lbTitulo.Text = "Tiempo estimado elaboración";
            // 
            // lbMinutos
            // 
            this.lbMinutos.AutoSize = true;
            this.lbMinutos.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMinutos.Location = new System.Drawing.Point(233, 76);
            this.lbMinutos.Name = "lbMinutos";
            this.lbMinutos.Size = new System.Drawing.Size(49, 16);
            this.lbMinutos.TabIndex = 22;
            this.lbMinutos.Text = "Minutos";
            // 
            // lbHoras
            // 
            this.lbHoras.AutoSize = true;
            this.lbHoras.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHoras.Location = new System.Drawing.Point(170, 76);
            this.lbHoras.Name = "lbHoras";
            this.lbHoras.Size = new System.Drawing.Size(37, 16);
            this.lbHoras.TabIndex = 21;
            this.lbHoras.Text = "Horas";
            // 
            // lbDias
            // 
            this.lbDias.AutoSize = true;
            this.lbDias.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDias.Location = new System.Drawing.Point(105, 76);
            this.lbDias.Name = "lbDias";
            this.lbDias.Size = new System.Drawing.Size(29, 16);
            this.lbDias.TabIndex = 20;
            this.lbDias.Text = "Días";
            // 
            // nudMinutos
            // 
            this.nudMinutos.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.nudMinutos.Location = new System.Drawing.Point(228, 95);
            this.nudMinutos.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudMinutos.Name = "nudMinutos";
            this.nudMinutos.ReadOnly = true;
            this.nudMinutos.Size = new System.Drawing.Size(62, 20);
            this.nudMinutos.TabIndex = 19;
            this.nudMinutos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudHoras
            // 
            this.nudHoras.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.nudHoras.Location = new System.Drawing.Point(160, 95);
            this.nudHoras.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudHoras.Name = "nudHoras";
            this.nudHoras.ReadOnly = true;
            this.nudHoras.Size = new System.Drawing.Size(62, 20);
            this.nudHoras.TabIndex = 18;
            this.nudHoras.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudDias
            // 
            this.nudDias.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.nudDias.Location = new System.Drawing.Point(92, 95);
            this.nudDias.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDias.Name = "nudDias";
            this.nudDias.ReadOnly = true;
            this.nudDias.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nudDias.Size = new System.Drawing.Size(62, 20);
            this.nudDias.TabIndex = 17;
            this.nudDias.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(89, 153);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 17);
            this.label1.TabIndex = 24;
            this.label1.Text = "Fecha de entrega";
            // 
            // dtpFechaEntrega
            // 
            this.dtpFechaEntrega.CustomFormat = "yyyy-MM-dd";
            this.dtpFechaEntrega.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpFechaEntrega.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaEntrega.Location = new System.Drawing.Point(92, 184);
            this.dtpFechaEntrega.Name = "dtpFechaEntrega";
            this.dtpFechaEntrega.Size = new System.Drawing.Size(198, 22);
            this.dtpFechaEntrega.TabIndex = 25;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(92, 233);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(200, 26);
            this.btnAceptar.TabIndex = 26;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // DatosOrden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 271);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.dtpFechaEntrega);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbTitulo);
            this.Controls.Add(this.lbMinutos);
            this.Controls.Add(this.lbHoras);
            this.Controls.Add(this.lbDias);
            this.Controls.Add(this.nudMinutos);
            this.Controls.Add(this.nudHoras);
            this.Controls.Add(this.nudDias);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DatosOrden";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PUDVE - Datos Orden";
            this.Load += new System.EventHandler(this.DatosOrden_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHoras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDias)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Label lbMinutos;
        private System.Windows.Forms.Label lbHoras;
        private System.Windows.Forms.Label lbDias;
        private System.Windows.Forms.NumericUpDown nudMinutos;
        private System.Windows.Forms.NumericUpDown nudHoras;
        private System.Windows.Forms.NumericUpDown nudDias;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpFechaEntrega;
        private System.Windows.Forms.Button btnAceptar;
    }
}