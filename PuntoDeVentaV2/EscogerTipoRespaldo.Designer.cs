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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EscogerTipoRespaldo));
            this.rbRespaldarCerrarSesion = new System.Windows.Forms.RadioButton();
            this.rbNoRespaldar = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRespaldar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbRespaldarCerrarSesion
            // 
            this.rbRespaldarCerrarSesion.AutoSize = true;
            this.rbRespaldarCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbRespaldarCerrarSesion.Location = new System.Drawing.Point(24, 44);
            this.rbRespaldarCerrarSesion.Name = "rbRespaldarCerrarSesion";
            this.rbRespaldarCerrarSesion.Size = new System.Drawing.Size(147, 17);
            this.rbRespaldarCerrarSesion.TabIndex = 3;
            this.rbRespaldarCerrarSesion.TabStop = true;
            this.rbRespaldarCerrarSesion.Text = "Respaldar al cerrar sesion";
            this.rbRespaldarCerrarSesion.UseVisualStyleBackColor = true;
            // 
            // rbNoRespaldar
            // 
            this.rbNoRespaldar.AutoSize = true;
            this.rbNoRespaldar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbNoRespaldar.Location = new System.Drawing.Point(24, 77);
            this.rbNoRespaldar.Name = "rbNoRespaldar";
            this.rbNoRespaldar.Size = new System.Drawing.Size(85, 17);
            this.rbNoRespaldar.TabIndex = 4;
            this.rbNoRespaldar.TabStop = true;
            this.rbNoRespaldar.Text = "No respaldar";
            this.rbNoRespaldar.UseVisualStyleBackColor = true;
            this.rbNoRespaldar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbRespaldoCorreo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "Opciones de respaldo";
            // 
            // btnRespaldar
            // 
            this.btnRespaldar.Location = new System.Drawing.Point(100, 109);
            this.btnRespaldar.Name = "btnRespaldar";
            this.btnRespaldar.Size = new System.Drawing.Size(104, 32);
            this.btnRespaldar.TabIndex = 7;
            this.btnRespaldar.Text = "Respaldar ahora";
            this.btnRespaldar.UseVisualStyleBackColor = true;
            this.btnRespaldar.Click += new System.EventHandler(this.btnGuadar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(198, 152);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(79, 33);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(24, 152);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(85, 33);
            this.btnAceptar.TabIndex = 9;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // EscogerTipoRespaldo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(299, 197);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnRespaldar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbNoRespaldar);
            this.Controls.Add(this.rbRespaldarCerrarSesion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EscogerTipoRespaldo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Escoger Tipo Respaldo";
            this.Load += new System.EventHandler(this.EscogerTipoRespaldo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EscogerTipoRespaldo_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rbRespaldarCerrarSesion;
        private System.Windows.Forms.RadioButton rbNoRespaldar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRespaldar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
    }
}