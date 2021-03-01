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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.cbBasculaRegistrada = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
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
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.cbBasculaRegistrada);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(511, 97);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Basculas Registradas: ";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::PuntoDeVentaV2.Properties.Resources.weighingMachine;
            this.button1.Location = new System.Drawing.Point(294, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(174, 68);
            this.button1.TabIndex = 2;
            this.button1.Text = "Otra";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // cbBasculaRegistrada
            // 
            this.cbBasculaRegistrada.FormattingEnabled = true;
            this.cbBasculaRegistrada.Location = new System.Drawing.Point(77, 45);
            this.cbBasculaRegistrada.Name = "cbBasculaRegistrada";
            this.cbBasculaRegistrada.Size = new System.Drawing.Size(185, 21);
            this.cbBasculaRegistrada.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Modelo:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(511, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Asignar Bascula";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
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
            this.groupBox2.Location = new System.Drawing.Point(556, 68);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(554, 268);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Configuración Bascula: ";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Century", 11.25F);
            this.button2.Image = global::PuntoDeVentaV2.Properties.Resources.saveNew;
            this.button2.Location = new System.Drawing.Point(384, 189);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(152, 61);
            this.button2.TabIndex = 17;
            this.button2.Text = "Configuración";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btnTomarPeso
            // 
            this.btnTomarPeso.Font = new System.Drawing.Font("Century", 11.25F);
            this.btnTomarPeso.Image = global::PuntoDeVentaV2.Properties.Resources.scale;
            this.btnTomarPeso.Location = new System.Drawing.Point(133, 189);
            this.btnTomarPeso.Name = "btnTomarPeso";
            this.btnTomarPeso.Size = new System.Drawing.Size(152, 61);
            this.btnTomarPeso.TabIndex = 16;
            this.btnTomarPeso.Text = "Tomar Peso";
            this.btnTomarPeso.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnTomarPeso.UseVisualStyleBackColor = true;
            this.btnTomarPeso.Click += new System.EventHandler(this.btnTomarPeso_Click);
            // 
            // lblPeso
            // 
            this.lblPeso.BackColor = System.Drawing.Color.White;
            this.lblPeso.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPeso.Location = new System.Drawing.Point(381, 146);
            this.lblPeso.Name = "lblPeso";
            this.lblPeso.Size = new System.Drawing.Size(155, 23);
            this.lblPeso.TabIndex = 15;
            this.lblPeso.Text = "0";
            this.lblPeso.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(308, 151);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Peso:";
            // 
            // txtSendData
            // 
            this.txtSendData.Location = new System.Drawing.Point(133, 148);
            this.txtSendData.Name = "txtSendData";
            this.txtSendData.Size = new System.Drawing.Size(152, 20);
            this.txtSendData.TabIndex = 13;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(26, 151);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Enviar Dato:";
            // 
            // cbStopBits
            // 
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(384, 110);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(152, 21);
            this.cbStopBits.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(308, 113);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "StopBits:";
            // 
            // cbParidad
            // 
            this.cbParidad.FormattingEnabled = true;
            this.cbParidad.Location = new System.Drawing.Point(133, 110);
            this.cbParidad.Name = "cbParidad";
            this.cbParidad.Size = new System.Drawing.Size(152, 21);
            this.cbParidad.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Parity:";
            // 
            // cbHandshake
            // 
            this.cbHandshake.FormattingEnabled = true;
            this.cbHandshake.Location = new System.Drawing.Point(384, 71);
            this.cbHandshake.Name = "cbHandshake";
            this.cbHandshake.Size = new System.Drawing.Size(152, 21);
            this.cbHandshake.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(308, 74);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Handshake:";
            // 
            // cbDatos
            // 
            this.cbDatos.FormattingEnabled = true;
            this.cbDatos.Location = new System.Drawing.Point(133, 71);
            this.cbDatos.Name = "cbDatos";
            this.cbDatos.Size = new System.Drawing.Size(152, 21);
            this.cbDatos.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "DataBits:";
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Location = new System.Drawing.Point(384, 35);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(152, 21);
            this.cbBaudRate.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(308, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "BaudRate:";
            // 
            // cbPuerto
            // 
            this.cbPuerto.FormattingEnabled = true;
            this.cbPuerto.Location = new System.Drawing.Point(133, 35);
            this.cbPuerto.Name = "cbPuerto";
            this.cbPuerto.Size = new System.Drawing.Size(152, 21);
            this.cbPuerto.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Puerto:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(553, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "label11";
            // 
            // AgregarBasculas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1135, 661);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "AgregarBasculas";
            this.Text = "AgregarBasculas";
            this.Load += new System.EventHandler(this.AgregarBasculas_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbBasculaRegistrada;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label11;
    }
}