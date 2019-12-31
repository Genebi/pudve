namespace PuntoDeVentaV2
{
    partial class SetUpPUDVE
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
            this.cbStockNegativo = new System.Windows.Forms.CheckBox();
            this.btnRespaldo = new System.Windows.Forms.Button();
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.guardarArchivo = new System.Windows.Forms.SaveFileDialog();
            this.txtNombreServidor = new System.Windows.Forms.TextBox();
            this.btnGuardarServidor = new System.Windows.Forms.Button();
            this.lbNombreServidor = new System.Windows.Forms.Label();
            this.btnGuardarRevision = new System.Windows.Forms.Button();
            this.txtNumeroRevision = new System.Windows.Forms.TextBox();
            this.lbNumeroRevision = new System.Windows.Forms.Label();
            this.btnLimpiarTabla = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbStockNegativo
            // 
            this.cbStockNegativo.AutoSize = true;
            this.cbStockNegativo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStockNegativo.Location = new System.Drawing.Point(608, 120);
            this.cbStockNegativo.Name = "cbStockNegativo";
            this.cbStockNegativo.Size = new System.Drawing.Size(177, 21);
            this.cbStockNegativo.TabIndex = 1;
            this.cbStockNegativo.Text = "Permitir Stock negativo";
            this.cbStockNegativo.UseVisualStyleBackColor = true;
            this.cbStockNegativo.CheckedChanged += new System.EventHandler(this.cbStockNegativo_CheckedChanged);
            // 
            // btnRespaldo
            // 
            this.btnRespaldo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnRespaldo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRespaldo.FlatAppearance.BorderSize = 0;
            this.btnRespaldo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnRespaldo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnRespaldo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRespaldo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRespaldo.ForeColor = System.Drawing.Color.White;
            this.btnRespaldo.Location = new System.Drawing.Point(350, 114);
            this.btnRespaldo.Name = "btnRespaldo";
            this.btnRespaldo.Size = new System.Drawing.Size(190, 30);
            this.btnRespaldo.TabIndex = 101;
            this.btnRespaldo.Text = "Respaldar información";
            this.btnRespaldo.UseVisualStyleBackColor = false;
            this.btnRespaldo.Click += new System.EventHandler(this.btnRespaldo_Click);
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(388, 27);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(194, 25);
            this.tituloSeccion.TabIndex = 102;
            this.tituloSeccion.Text = "CONFIGURACIÓN";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // guardarArchivo
            // 
            this.guardarArchivo.DefaultExt = "db";
            this.guardarArchivo.Title = "Respaldar Información";
            // 
            // txtNombreServidor
            // 
            this.txtNombreServidor.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreServidor.Location = new System.Drawing.Point(55, 121);
            this.txtNombreServidor.Name = "txtNombreServidor";
            this.txtNombreServidor.Size = new System.Drawing.Size(190, 23);
            this.txtNombreServidor.TabIndex = 104;
            this.txtNombreServidor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGuardarServidor
            // 
            this.btnGuardarServidor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnGuardarServidor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarServidor.FlatAppearance.BorderSize = 0;
            this.btnGuardarServidor.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnGuardarServidor.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnGuardarServidor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarServidor.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarServidor.ForeColor = System.Drawing.Color.White;
            this.btnGuardarServidor.Location = new System.Drawing.Point(55, 159);
            this.btnGuardarServidor.Name = "btnGuardarServidor";
            this.btnGuardarServidor.Size = new System.Drawing.Size(190, 25);
            this.btnGuardarServidor.TabIndex = 105;
            this.btnGuardarServidor.Text = "Guardar";
            this.btnGuardarServidor.UseVisualStyleBackColor = false;
            this.btnGuardarServidor.Click += new System.EventHandler(this.btnGuardarServidor_Click);
            // 
            // lbNombreServidor
            // 
            this.lbNombreServidor.AutoSize = true;
            this.lbNombreServidor.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNombreServidor.Location = new System.Drawing.Point(52, 91);
            this.lbNombreServidor.Name = "lbNombreServidor";
            this.lbNombreServidor.Size = new System.Drawing.Size(158, 17);
            this.lbNombreServidor.TabIndex = 103;
            this.lbNombreServidor.Text = "Computadora Servidor";
            // 
            // btnGuardarRevision
            // 
            this.btnGuardarRevision.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnGuardarRevision.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarRevision.FlatAppearance.BorderSize = 0;
            this.btnGuardarRevision.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnGuardarRevision.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnGuardarRevision.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarRevision.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarRevision.ForeColor = System.Drawing.Color.White;
            this.btnGuardarRevision.Location = new System.Drawing.Point(55, 299);
            this.btnGuardarRevision.Name = "btnGuardarRevision";
            this.btnGuardarRevision.Size = new System.Drawing.Size(190, 25);
            this.btnGuardarRevision.TabIndex = 108;
            this.btnGuardarRevision.Text = "Guardar";
            this.btnGuardarRevision.UseVisualStyleBackColor = false;
            this.btnGuardarRevision.Click += new System.EventHandler(this.btnGuardarRevision_Click);
            // 
            // txtNumeroRevision
            // 
            this.txtNumeroRevision.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumeroRevision.Location = new System.Drawing.Point(55, 261);
            this.txtNumeroRevision.Name = "txtNumeroRevision";
            this.txtNumeroRevision.Size = new System.Drawing.Size(190, 23);
            this.txtNumeroRevision.TabIndex = 107;
            this.txtNumeroRevision.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbNumeroRevision
            // 
            this.lbNumeroRevision.AutoSize = true;
            this.lbNumeroRevision.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbNumeroRevision.Location = new System.Drawing.Point(52, 231);
            this.lbNumeroRevision.Name = "lbNumeroRevision";
            this.lbNumeroRevision.Size = new System.Drawing.Size(181, 17);
            this.lbNumeroRevision.TabIndex = 106;
            this.lbNumeroRevision.Text = "Número revisión inventario";
            // 
            // btnLimpiarTabla
            // 
            this.btnLimpiarTabla.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnLimpiarTabla.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpiarTabla.FlatAppearance.BorderSize = 0;
            this.btnLimpiarTabla.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnLimpiarTabla.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnLimpiarTabla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpiarTabla.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLimpiarTabla.ForeColor = System.Drawing.Color.White;
            this.btnLimpiarTabla.Location = new System.Drawing.Point(350, 294);
            this.btnLimpiarTabla.Name = "btnLimpiarTabla";
            this.btnLimpiarTabla.Size = new System.Drawing.Size(190, 30);
            this.btnLimpiarTabla.TabIndex = 109;
            this.btnLimpiarTabla.Text = "Limpiar tabla inventario";
            this.btnLimpiarTabla.UseVisualStyleBackColor = false;
            this.btnLimpiarTabla.Click += new System.EventHandler(this.btnLimpiarTabla_Click);
            // 
            // SetUpPUDVE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 561);
            this.Controls.Add(this.btnLimpiarTabla);
            this.Controls.Add(this.btnGuardarRevision);
            this.Controls.Add(this.txtNumeroRevision);
            this.Controls.Add(this.lbNumeroRevision);
            this.Controls.Add(this.btnGuardarServidor);
            this.Controls.Add(this.txtNombreServidor);
            this.Controls.Add(this.lbNombreServidor);
            this.Controls.Add(this.tituloSeccion);
            this.Controls.Add(this.btnRespaldo);
            this.Controls.Add(this.cbStockNegativo);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "SetUpPUDVE";
            this.Text = "PUDVE - Configuración";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SetUpPUDVE_FormClosed);
            this.Load += new System.EventHandler(this.SetUpPUDVE_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SetUpPUDVE_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cbStockNegativo;
        private System.Windows.Forms.Button btnRespaldo;
        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.SaveFileDialog guardarArchivo;
        private System.Windows.Forms.TextBox txtNombreServidor;
        private System.Windows.Forms.Button btnGuardarServidor;
        private System.Windows.Forms.Label lbNombreServidor;
        private System.Windows.Forms.Button btnGuardarRevision;
        private System.Windows.Forms.TextBox txtNumeroRevision;
        private System.Windows.Forms.Label lbNumeroRevision;
        private System.Windows.Forms.Button btnLimpiarTabla;
    }
}