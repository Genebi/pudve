namespace PuntoDeVentaV2
{
    partial class AgregarBasculas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AgregarBasculas));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddEditBascula = new System.Windows.Forms.Button();
            this.cbBasculaRegistrada = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnTomarPeso = new System.Windows.Forms.Button();
            this.lblPeso = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSendData = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cbParidad = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbHandshake = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbDatos = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbPuerto = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddEditBascula);
            this.groupBox1.Controls.Add(this.cbBasculaRegistrada);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 105);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(681, 177);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Basculas Registradas: ";
            // 
            // btnAddEditBascula
            // 
            this.btnAddEditBascula.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(153)))), ((int)(((byte)(199)))));
            this.btnAddEditBascula.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddEditBascula.FlatAppearance.BorderSize = 0;
            this.btnAddEditBascula.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddEditBascula.Font = new System.Drawing.Font("Century", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddEditBascula.ForeColor = System.Drawing.Color.White;
            this.btnAddEditBascula.Location = new System.Drawing.Point(369, 52);
            this.btnAddEditBascula.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAddEditBascula.Name = "btnAddEditBascula";
            this.btnAddEditBascula.Size = new System.Drawing.Size(284, 86);
            this.btnAddEditBascula.TabIndex = 2;
            this.btnAddEditBascula.Text = "Agregar - Editar / Predeterminada";
            this.btnAddEditBascula.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.btnAddEditBascula.UseVisualStyleBackColor = false;
            this.btnAddEditBascula.Click += new System.EventHandler(this.btnAddEditBascula_Click);
            // 
            // cbBasculaRegistrada
            // 
            this.cbBasculaRegistrada.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBasculaRegistrada.FormattingEnabled = true;
            this.cbBasculaRegistrada.Location = new System.Drawing.Point(92, 81);
            this.cbBasculaRegistrada.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbBasculaRegistrada.Name = "cbBasculaRegistrada";
            this.cbBasculaRegistrada.Size = new System.Drawing.Size(256, 28);
            this.cbBasculaRegistrada.TabIndex = 1;
            this.cbBasculaRegistrada.TextChanged += new System.EventHandler(this.cbBasculaRegistrada_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 85);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Modelo:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(681, 46);
            this.label1.TabIndex = 1;
            this.label1.Text = "Asignar Bascula";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnTomarPeso);
            this.groupBox2.Controls.Add(this.lblPeso);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtSendData);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.cbStopBits);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.cbParidad);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.cbHandshake);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cbDatos);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.cbBaudRate);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cbPuerto);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(719, 105);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox2.Size = new System.Drawing.Size(831, 366);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Configuración Bascula: ";
            // 
            // btnTomarPeso
            // 
            this.btnTomarPeso.BackColor = System.Drawing.Color.Green;
            this.btnTomarPeso.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTomarPeso.FlatAppearance.BorderSize = 0;
            this.btnTomarPeso.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTomarPeso.Font = new System.Drawing.Font("Century", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTomarPeso.ForeColor = System.Drawing.Color.White;
            this.btnTomarPeso.Location = new System.Drawing.Point(370, 295);
            this.btnTomarPeso.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnTomarPeso.Name = "btnTomarPeso";
            this.btnTomarPeso.Size = new System.Drawing.Size(258, 45);
            this.btnTomarPeso.TabIndex = 16;
            this.btnTomarPeso.Text = "Tomar Peso";
            this.btnTomarPeso.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnTomarPeso.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTomarPeso.UseVisualStyleBackColor = false;
            this.btnTomarPeso.Click += new System.EventHandler(this.btnTomarPeso_Click);
            // 
            // lblPeso
            // 
            this.lblPeso.BackColor = System.Drawing.Color.White;
            this.lblPeso.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeso.Location = new System.Drawing.Point(572, 225);
            this.lblPeso.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(232, 35);
            this.lblPeso.TabIndex = 15;
            this.lblPeso.Text = "0";
            this.lblPeso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(462, 232);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 20);
            this.label10.TabIndex = 14;
            this.label10.Text = "Peso:";
            // 
            // txtSendData
            // 
            this.txtSendData.Enabled = false;
            this.txtSendData.Location = new System.Drawing.Point(200, 228);
            this.txtSendData.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtSendData.Name = "txtSendData";
            this.txtSendData.Size = new System.Drawing.Size(226, 26);
            this.txtSendData.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(39, 232);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 20);
            this.label9.TabIndex = 12;
            this.label9.Text = "Enviar Dato:";
            // 
            // cbStopBits
            // 
            this.cbStopBits.Enabled = false;
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(576, 169);
            this.cbStopBits.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(226, 28);
            this.cbStopBits.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(462, 174);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 20);
            this.label8.TabIndex = 10;
            this.label8.Text = "StopBits:";
            // 
            // cbParidad
            // 
            this.cbParidad.Enabled = false;
            this.cbParidad.FormattingEnabled = true;
            this.cbParidad.Location = new System.Drawing.Point(200, 169);
            this.cbParidad.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbParidad.Name = "cbParidad";
            this.cbParidad.Size = new System.Drawing.Size(226, 28);
            this.cbParidad.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 174);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 20);
            this.label7.TabIndex = 8;
            this.label7.Text = "Parity:";
            // 
            // cbHandshake
            // 
            this.cbHandshake.Enabled = false;
            this.cbHandshake.FormattingEnabled = true;
            this.cbHandshake.Location = new System.Drawing.Point(576, 109);
            this.cbHandshake.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbHandshake.Name = "cbHandshake";
            this.cbHandshake.Size = new System.Drawing.Size(226, 28);
            this.cbHandshake.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(462, 114);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 20);
            this.label6.TabIndex = 6;
            this.label6.Text = "Handshake:";
            // 
            // cbDatos
            // 
            this.cbDatos.Enabled = false;
            this.cbDatos.FormattingEnabled = true;
            this.cbDatos.Location = new System.Drawing.Point(200, 109);
            this.cbDatos.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbDatos.Name = "cbDatos";
            this.cbDatos.Size = new System.Drawing.Size(226, 28);
            this.cbDatos.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(39, 114);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 20);
            this.label5.TabIndex = 4;
            this.label5.Text = "DataBits:";
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.Enabled = false;
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Location = new System.Drawing.Point(576, 54);
            this.cbBaudRate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(226, 28);
            this.cbBaudRate.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(462, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "BaudRate:";
            // 
            // cbPuerto
            // 
            this.cbPuerto.Enabled = false;
            this.cbPuerto.FormattingEnabled = true;
            this.cbPuerto.Location = new System.Drawing.Point(200, 54);
            this.cbPuerto.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbPuerto.Name = "cbPuerto";
            this.cbPuerto.Size = new System.Drawing.Size(226, 28);
            this.cbPuerto.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 55);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Puerto:";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Century Gothic", 18F);
            this.label11.Location = new System.Drawing.Point(719, 34);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(831, 46);
            this.label11.TabIndex = 3;
            this.label11.Text = "Datos de Configuración";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AgregarBasculas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1702, 1017);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "AgregarBasculas";
            this.Text = "AgregarBasculas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AgregarBasculas_FormClosing);
            this.Load += new System.EventHandler(this.AgregarBasculas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbBasculaRegistrada;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddEditBascula;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbPuerto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbDatos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbHandshake;
        private System.Windows.Forms.ComboBox cbParidad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtSendData;
        private System.Windows.Forms.Label lblPeso;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnTomarPeso;
        private System.Windows.Forms.Label label11;
    }
}