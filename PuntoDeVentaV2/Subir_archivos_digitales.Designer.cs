namespace PuntoDeVentaV2
{
    partial class Subir_archivos_digitales
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_certificado = new System.Windows.Forms.TextBox();
            this.txt_llave = new System.Windows.Forms.TextBox();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_rfc = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_llave_pem = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txt_certificado_pem = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.lb_fecha_caducidad = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_subir_archivos = new System.Windows.Forms.TextBox();
            this.btn_subir_archivos = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btn_borrar_archivos = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.openfiled_archivos = new System.Windows.Forms.OpenFileDialog();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Certificado (.cer)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Llave (.key)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Contraseña";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "RFC";
            // 
            // txt_certificado
            // 
            this.txt_certificado.Cursor = System.Windows.Forms.Cursors.No;
            this.txt_certificado.Location = new System.Drawing.Point(11, 31);
            this.txt_certificado.Name = "txt_certificado";
            this.txt_certificado.ReadOnly = true;
            this.txt_certificado.Size = new System.Drawing.Size(343, 22);
            this.txt_certificado.TabIndex = 4;
            this.txt_certificado.TabStop = false;
            // 
            // txt_llave
            // 
            this.txt_llave.Cursor = System.Windows.Forms.Cursors.No;
            this.txt_llave.Location = new System.Drawing.Point(11, 141);
            this.txt_llave.Name = "txt_llave";
            this.txt_llave.ReadOnly = true;
            this.txt_llave.Size = new System.Drawing.Size(343, 22);
            this.txt_llave.TabIndex = 5;
            this.txt_llave.TabStop = false;
            // 
            // txt_password
            // 
            this.txt_password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_password.Location = new System.Drawing.Point(11, 234);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.ReadOnly = true;
            this.txt_password.Size = new System.Drawing.Size(343, 22);
            this.txt_password.TabIndex = 6;
            this.txt_password.Leave += new System.EventHandler(this.guardar_password);
            // 
            // txt_rfc
            // 
            this.txt_rfc.Cursor = System.Windows.Forms.Cursors.No;
            this.txt_rfc.Location = new System.Drawing.Point(11, 281);
            this.txt_rfc.MaxLength = 13;
            this.txt_rfc.Name = "txt_rfc";
            this.txt_rfc.ReadOnly = true;
            this.txt_rfc.Size = new System.Drawing.Size(343, 22);
            this.txt_rfc.TabIndex = 7;
            this.txt_rfc.TabStop = false;
            this.txt_rfc.Leave += new System.EventHandler(this.validar_formato_rfc);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_llave_pem);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.txt_certificado_pem);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Controls.Add(this.lb_fecha_caducidad);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.txt_rfc);
            this.panel1.Controls.Add(this.txt_certificado);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txt_password);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.txt_llave);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(12, 119);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(367, 313);
            this.panel1.TabIndex = 8;
            // 
            // txt_llave_pem
            // 
            this.txt_llave_pem.Cursor = System.Windows.Forms.Cursors.No;
            this.txt_llave_pem.Location = new System.Drawing.Point(11, 188);
            this.txt_llave_pem.Name = "txt_llave_pem";
            this.txt_llave_pem.ReadOnly = true;
            this.txt_llave_pem.Size = new System.Drawing.Size(343, 22);
            this.txt_llave_pem.TabIndex = 13;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(8, 169);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(133, 17);
            this.label21.TabIndex = 12;
            this.label21.Text = "Llave PEM (.key.pem)";
            // 
            // txt_certificado_pem
            // 
            this.txt_certificado_pem.Cursor = System.Windows.Forms.Cursors.No;
            this.txt_certificado_pem.Location = new System.Drawing.Point(11, 95);
            this.txt_certificado_pem.Name = "txt_certificado_pem";
            this.txt_certificado_pem.ReadOnly = true;
            this.txt_certificado_pem.Size = new System.Drawing.Size(343, 22);
            this.txt_certificado_pem.TabIndex = 11;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(8, 76);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(168, 17);
            this.label20.TabIndex = 10;
            this.label20.Text = "Certificado PEM (.cer.pem)";
            // 
            // lb_fecha_caducidad
            // 
            this.lb_fecha_caducidad.AutoSize = true;
            this.lb_fecha_caducidad.ForeColor = System.Drawing.Color.Blue;
            this.lb_fecha_caducidad.Location = new System.Drawing.Point(223, 55);
            this.lb_fecha_caducidad.Name = "lb_fecha_caducidad";
            this.lb_fecha_caducidad.Size = new System.Drawing.Size(11, 17);
            this.lb_fecha_caducidad.TabIndex = 9;
            this.lb_fecha_caducidad.Text = ".";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Blue;
            this.label6.Location = new System.Drawing.Point(8, 55);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(206, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "Fecha caducidad del certificado";
            // 
            // txt_subir_archivos
            // 
            this.txt_subir_archivos.Cursor = System.Windows.Forms.Cursors.No;
            this.txt_subir_archivos.Location = new System.Drawing.Point(8, 11);
            this.txt_subir_archivos.Name = "txt_subir_archivos";
            this.txt_subir_archivos.ReadOnly = true;
            this.txt_subir_archivos.Size = new System.Drawing.Size(343, 22);
            this.txt_subir_archivos.TabIndex = 10;
            this.txt_subir_archivos.TabStop = false;
            // 
            // btn_subir_archivos
            // 
            this.btn_subir_archivos.BackColor = System.Drawing.Color.Teal;
            this.btn_subir_archivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_subir_archivos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_subir_archivos.ForeColor = System.Drawing.Color.White;
            this.btn_subir_archivos.Location = new System.Drawing.Point(230, 39);
            this.btn_subir_archivos.Name = "btn_subir_archivos";
            this.btn_subir_archivos.Size = new System.Drawing.Size(121, 29);
            this.btn_subir_archivos.TabIndex = 11;
            this.btn_subir_archivos.Text = "Subir archivo";
            this.btn_subir_archivos.UseVisualStyleBackColor = false;
            this.btn_subir_archivos.Click += new System.EventHandler(this.btn_subir_archivos_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.txt_subir_archivos);
            this.panel2.Controls.Add(this.btn_subir_archivos);
            this.panel2.Location = new System.Drawing.Point(410, 137);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(361, 78);
            this.panel2.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.MistyRose;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel3.Controls.Add(this.label14);
            this.panel3.Controls.Add(this.label13);
            this.panel3.Controls.Add(this.label11);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.ForeColor = System.Drawing.Color.DarkRed;
            this.panel3.Location = new System.Drawing.Point(410, 245);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(361, 126);
            this.panel3.TabIndex = 13;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(5, 66);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(353, 51);
            this.label14.TabIndex = 14;
            this.label14.Text = "Si sus archivos digitales recientemente fueron tramitados\r\ny/o actualizados, esto" +
    "s se activarán aproximadamente en\r\n3 días hábiles (para el SAT).";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(4, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(0, 17);
            this.label13.TabIndex = 5;
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(312, 17);
            this.label11.TabIndex = 3;
            this.label11.Text = "Recuerde subir unicamente el archivo comprimido.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(5, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 16);
            this.label8.TabIndex = 0;
            this.label8.Text = "NOTA:";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Location = new System.Drawing.Point(12, 438);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(759, 55);
            this.panel4.TabIndex = 14;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.btn_borrar_archivos);
            this.panel5.Controls.Add(this.label15);
            this.panel5.Location = new System.Drawing.Point(6, 7);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(748, 40);
            this.panel5.TabIndex = 0;
            // 
            // btn_borrar_archivos
            // 
            this.btn_borrar_archivos.BackColor = System.Drawing.Color.Teal;
            this.btn_borrar_archivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_borrar_archivos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_borrar_archivos.ForeColor = System.Drawing.Color.White;
            this.btn_borrar_archivos.Location = new System.Drawing.Point(572, 5);
            this.btn_borrar_archivos.Name = "btn_borrar_archivos";
            this.btn_borrar_archivos.Size = new System.Drawing.Size(130, 30);
            this.btn_borrar_archivos.TabIndex = 1;
            this.btn_borrar_archivos.Text = "Borrar archivos";
            this.btn_borrar_archivos.UseVisualStyleBackColor = false;
            this.btn_borrar_archivos.Click += new System.EventHandler(this.btn_borrar_archivos_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(549, 17);
            this.label15.TabIndex = 0;
            this.label15.Text = "Si desea cambiar y/o reemplazar sus archivos digitales, de clic en el botón para " +
    "eliminarlos.";
            // 
            // openfiled_archivos
            // 
            this.openfiled_archivos.FileName = "Seleccionar archivo";
            this.openfiled_archivos.Filter = "Archivo ZIP (*.zip)|*.zip";
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_aceptar.BackColor = System.Drawing.Color.Green;
            this.btn_aceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_aceptar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_aceptar.ForeColor = System.Drawing.Color.White;
            this.btn_aceptar.Location = new System.Drawing.Point(652, 519);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(119, 30);
            this.btn_aceptar.TabIndex = 15;
            this.btn_aceptar.Text = "Aceptar";
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
            this.btn_cancelar.Location = new System.Drawing.Point(517, 519);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(119, 30);
            this.btn_cancelar.TabIndex = 16;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Visible = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Lavender;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel6.Controls.Add(this.label19);
            this.panel6.Controls.Add(this.label18);
            this.panel6.Controls.Add(this.label17);
            this.panel6.Controls.Add(this.label16);
            this.panel6.Controls.Add(this.label7);
            this.panel6.ForeColor = System.Drawing.Color.RoyalBlue;
            this.panel6.Location = new System.Drawing.Point(12, 29);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(759, 82);
            this.panel6.TabIndex = 17;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label19.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Red;
            this.label19.Location = new System.Drawing.Point(405, 4);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(242, 16);
            this.label19.TabIndex = 4;
            this.label19.Text = "\"Generar archivos PEM y validar mi CSD\"";
            this.label19.Click += new System.EventHandler(this.ir_a_sifo);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Navy;
            this.label18.Location = new System.Drawing.Point(6, 61);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(421, 17);
            this.label18.TabIndex = 3;
            this.label18.Text = "Una vez realizado lo anterior  proceder a subir el archivo comprimido.";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Navy;
            this.label17.Location = new System.Drawing.Point(6, 41);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(575, 17);
            this.label17.TabIndex = 2;
            this.label17.Text = "generará 2 nuevos archivos, estos archivos serán utilizados para la cancelación d" +
    "e sus facturas.";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Navy;
            this.label16.Location = new System.Drawing.Point(6, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(741, 17);
            this.label16.TabIndex = 1;
            this.label16.Text = "Le mandará a una página en donde deberá subir su certificado y llave (CSD), los m" +
    "ismos serán validados y apartir de ellos le";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Navy;
            this.label7.Location = new System.Drawing.Point(6, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(400, 17);
            this.label7.TabIndex = 0;
            this.label7.Text = "Antes de subir sus archivos digitales, de clic en el siguiente enlace";
            // 
            // Subir_archivos_digitales
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 561);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Subir_archivos_digitales";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Actualizar archivos digitales en 3 dias";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.cerrando);
            this.Load += new System.EventHandler(this.Subir_archivos_digitales_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Subir_archivos_digitales_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_certificado;
        private System.Windows.Forms.TextBox txt_llave;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_rfc;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txt_subir_archivos;
        private System.Windows.Forms.Button btn_subir_archivos;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lb_fecha_caducidad;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btn_borrar_archivos;
        private System.Windows.Forms.OpenFileDialog openfiled_archivos;
        private System.Windows.Forms.Button btn_aceptar;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txt_certificado_pem;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txt_llave_pem;
    }
}