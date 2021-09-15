namespace PuntoDeVentaV2
{
    partial class EscogerTipoRespaldo
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
            this.rbRespaldoEquipo = new System.Windows.Forms.RadioButton();
            this.rbRespaldoCorreo = new System.Windows.Forms.RadioButton();
            this.rbAmbos = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGuadar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbRespaldoEquipo
            // 
            this.rbRespaldoEquipo.AutoSize = true;
            this.rbRespaldoEquipo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbRespaldoEquipo.Location = new System.Drawing.Point(24, 59);
            this.rbRespaldoEquipo.Name = "rbRespaldoEquipo";
            this.rbRespaldoEquipo.Size = new System.Drawing.Size(146, 17);
            this.rbRespaldoEquipo.TabIndex = 3;
            this.rbRespaldoEquipo.TabStop = true;
            this.rbRespaldoEquipo.Text = "Respaldar en este equipo";
            this.rbRespaldoEquipo.UseVisualStyleBackColor = true;
            this.rbRespaldoEquipo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbRespaldoEquipo_KeyDown);
            // 
            // rbRespaldoCorreo
            // 
            this.rbRespaldoCorreo.AutoSize = true;
            this.rbRespaldoCorreo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbRespaldoCorreo.Location = new System.Drawing.Point(24, 94);
            this.rbRespaldoCorreo.Name = "rbRespaldoCorreo";
            this.rbRespaldoCorreo.Size = new System.Drawing.Size(148, 17);
            this.rbRespaldoCorreo.TabIndex = 4;
            this.rbRespaldoCorreo.TabStop = true;
            this.rbRespaldoCorreo.Text = "Mandar respaldo al correo";
            this.rbRespaldoCorreo.UseVisualStyleBackColor = true;
            this.rbRespaldoCorreo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbRespaldoCorreo_KeyDown);
            // 
            // rbAmbos
            // 
            this.rbAmbos.AutoSize = true;
            this.rbAmbos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbAmbos.Location = new System.Drawing.Point(24, 131);
            this.rbAmbos.Name = "rbAmbos";
            this.rbAmbos.Size = new System.Drawing.Size(236, 17);
            this.rbAmbos.TabIndex = 5;
            this.rbAmbos.TabStop = true;
            this.rbAmbos.Text = "Respaldar en este equipo y mandar al correo";
            this.rbAmbos.UseVisualStyleBackColor = true;
            this.rbAmbos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbAmbos_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(271, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Seleccione la forma de respaldo ";
            // 
            // btnGuadar
            // 
            this.btnGuadar.Location = new System.Drawing.Point(185, 173);
            this.btnGuadar.Name = "btnGuadar";
            this.btnGuadar.Size = new System.Drawing.Size(75, 23);
            this.btnGuadar.TabIndex = 7;
            this.btnGuadar.Text = "Respaldar";
            this.btnGuadar.UseVisualStyleBackColor = true;
            this.btnGuadar.Click += new System.EventHandler(this.btnGuadar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(24, 173);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // EscogerTipoRespaldo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 208);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnGuadar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbAmbos);
            this.Controls.Add(this.rbRespaldoCorreo);
            this.Controls.Add(this.rbRespaldoEquipo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EscogerTipoRespaldo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EscogerTipoRespaldo";
            this.Load += new System.EventHandler(this.EscogerTipoRespaldo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbRespaldoEquipo;
        private System.Windows.Forms.RadioButton rbRespaldoCorreo;
        private System.Windows.Forms.RadioButton rbAmbos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGuadar;
        private System.Windows.Forms.Button btnCancelar;
    }
}