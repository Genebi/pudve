namespace PuntoDeVentaV2
{
    partial class ConfiguracionOrdenes
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbTitulo = new System.Windows.Forms.Label();
            this.lbMinutos = new System.Windows.Forms.Label();
            this.lbHoras = new System.Windows.Forms.Label();
            this.lbDias = new System.Windows.Forms.Label();
            this.nudMinutos = new System.Windows.Forms.NumericUpDown();
            this.nudHoras = new System.Windows.Forms.NumericUpDown();
            this.nudDias = new System.Windows.Forms.NumericUpDown();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHoras)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDias)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAceptar);
            this.groupBox1.Controls.Add(this.lbTitulo);
            this.groupBox1.Controls.Add(this.lbMinutos);
            this.groupBox1.Controls.Add(this.lbHoras);
            this.groupBox1.Controls.Add(this.lbDias);
            this.groupBox1.Controls.Add(this.nudMinutos);
            this.groupBox1.Controls.Add(this.nudHoras);
            this.groupBox1.Controls.Add(this.nudDias);
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(345, 200);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // lbTitulo
            // 
            this.lbTitulo.AutoSize = true;
            this.lbTitulo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.Location = new System.Drawing.Point(15, 27);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(193, 17);
            this.lbTitulo.TabIndex = 16;
            this.lbTitulo.Text = "Tiempo Aproximado Entrega";
            // 
            // lbMinutos
            // 
            this.lbMinutos.AutoSize = true;
            this.lbMinutos.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMinutos.Location = new System.Drawing.Point(159, 63);
            this.lbMinutos.Name = "lbMinutos";
            this.lbMinutos.Size = new System.Drawing.Size(49, 16);
            this.lbMinutos.TabIndex = 15;
            this.lbMinutos.Text = "Minutos";
            // 
            // lbHoras
            // 
            this.lbHoras.AutoSize = true;
            this.lbHoras.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHoras.Location = new System.Drawing.Point(96, 63);
            this.lbHoras.Name = "lbHoras";
            this.lbHoras.Size = new System.Drawing.Size(37, 16);
            this.lbHoras.TabIndex = 14;
            this.lbHoras.Text = "Horas";
            // 
            // lbDias
            // 
            this.lbDias.AutoSize = true;
            this.lbDias.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDias.Location = new System.Drawing.Point(31, 63);
            this.lbDias.Name = "lbDias";
            this.lbDias.Size = new System.Drawing.Size(29, 16);
            this.lbDias.TabIndex = 13;
            this.lbDias.Text = "Días";
            // 
            // nudMinutos
            // 
            this.nudMinutos.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.nudMinutos.Location = new System.Drawing.Point(154, 82);
            this.nudMinutos.Maximum = new decimal(new int[] {
            59,
            0,
            0,
            0});
            this.nudMinutos.Name = "nudMinutos";
            this.nudMinutos.ReadOnly = true;
            this.nudMinutos.Size = new System.Drawing.Size(62, 20);
            this.nudMinutos.TabIndex = 12;
            this.nudMinutos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudHoras
            // 
            this.nudHoras.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.nudHoras.Location = new System.Drawing.Point(86, 82);
            this.nudHoras.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.nudHoras.Name = "nudHoras";
            this.nudHoras.ReadOnly = true;
            this.nudHoras.Size = new System.Drawing.Size(62, 20);
            this.nudHoras.TabIndex = 11;
            this.nudHoras.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudDias
            // 
            this.nudDias.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.nudDias.Location = new System.Drawing.Point(18, 82);
            this.nudDias.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudDias.Name = "nudDias";
            this.nudDias.ReadOnly = true;
            this.nudDias.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.nudDias.Size = new System.Drawing.Size(62, 20);
            this.nudDias.TabIndex = 10;
            this.nudDias.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(239, 168);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(100, 26);
            this.btnAceptar.TabIndex = 17;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // ConfiguracionOrdenes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 214);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ConfiguracionOrdenes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuración Órdenes";
            this.Load += new System.EventHandler(this.ConfiguracionOrdenes_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHoras)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDias)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Label lbMinutos;
        private System.Windows.Forms.Label lbHoras;
        private System.Windows.Forms.Label lbDias;
        private System.Windows.Forms.NumericUpDown nudMinutos;
        private System.Windows.Forms.NumericUpDown nudHoras;
        private System.Windows.Forms.NumericUpDown nudDias;
        private System.Windows.Forms.Button btnAceptar;
    }
}