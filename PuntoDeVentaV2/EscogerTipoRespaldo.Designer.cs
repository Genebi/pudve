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
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnRespaldarSU = new System.Windows.Forms.Button();
            this.btnImportarExcel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbRespaldarCerrarSesion
            // 
            this.rbRespaldarCerrarSesion.AutoSize = true;
            this.rbRespaldarCerrarSesion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbRespaldarCerrarSesion.Location = new System.Drawing.Point(4, 49);
            this.rbRespaldarCerrarSesion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbRespaldarCerrarSesion.Name = "rbRespaldarCerrarSesion";
            this.rbRespaldarCerrarSesion.Size = new System.Drawing.Size(218, 24);
            this.rbRespaldarCerrarSesion.TabIndex = 3;
            this.rbRespaldarCerrarSesion.TabStop = true;
            this.rbRespaldarCerrarSesion.Text = "Respaldar al cerrar sesion";
            this.rbRespaldarCerrarSesion.UseVisualStyleBackColor = true;
            this.rbRespaldarCerrarSesion.Visible = false;
            // 
            // rbNoRespaldar
            // 
            this.rbNoRespaldar.AutoSize = true;
            this.rbNoRespaldar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.rbNoRespaldar.Location = new System.Drawing.Point(4, 85);
            this.rbNoRespaldar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.rbNoRespaldar.Name = "rbNoRespaldar";
            this.rbNoRespaldar.Size = new System.Drawing.Size(124, 24);
            this.rbNoRespaldar.TabIndex = 4;
            this.rbNoRespaldar.TabStop = true;
            this.rbNoRespaldar.Text = "No respaldar";
            this.rbNoRespaldar.UseVisualStyleBackColor = true;
            this.rbNoRespaldar.Visible = false;
            this.rbNoRespaldar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rbRespaldoCorreo_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(98, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(272, 29);
            this.label1.TabIndex = 6;
            this.label1.Text = "Opciones de respaldo";
            // 
            // btnRespaldar
            // 
            this.btnRespaldar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRespaldar.Location = new System.Drawing.Point(138, 54);
            this.btnRespaldar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRespaldar.Name = "btnRespaldar";
            this.btnRespaldar.Size = new System.Drawing.Size(176, 49);
            this.btnRespaldar.TabIndex = 7;
            this.btnRespaldar.Text = "Respaldar ahora";
            this.btnRespaldar.UseVisualStyleBackColor = true;
            this.btnRespaldar.Click += new System.EventHandler(this.btnGuadar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.Location = new System.Drawing.Point(291, 320);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(118, 51);
            this.btnCancelar.TabIndex = 8;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.Location = new System.Drawing.Point(30, 320);
            this.btnAceptar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(128, 51);
            this.btnAceptar.TabIndex = 9;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnImportar
            // 
            this.btnImportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportar.Location = new System.Drawing.Point(137, 172);
            this.btnImportar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(177, 49);
            this.btnImportar.TabIndex = 10;
            this.btnImportar.Text = "Importar ahora";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            // 
            // btnRespaldarSU
            // 
            this.btnRespaldarSU.AutoSize = true;
            this.btnRespaldarSU.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRespaldarSU.Location = new System.Drawing.Point(137, 113);
            this.btnRespaldarSU.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnRespaldarSU.Name = "btnRespaldarSU";
            this.btnRespaldarSU.Size = new System.Drawing.Size(177, 49);
            this.btnRespaldarSU.TabIndex = 11;
            this.btnRespaldarSU.Text = "Respaldar sin usuario";
            this.btnRespaldarSU.UseVisualStyleBackColor = true;
            this.btnRespaldarSU.Click += new System.EventHandler(this.btnRespaldarSU_Click);
            // 
            // btnImportarExcel
            // 
            this.btnImportarExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportarExcel.Location = new System.Drawing.Point(138, 231);
            this.btnImportarExcel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImportarExcel.Name = "btnImportarExcel";
            this.btnImportarExcel.Size = new System.Drawing.Size(177, 49);
            this.btnImportarExcel.TabIndex = 10;
            this.btnImportarExcel.Text = "Importar productos desde Excel";
            this.btnImportarExcel.UseVisualStyleBackColor = true;
            this.btnImportarExcel.Click += new System.EventHandler(this.btnImportarExcel_Click);
            // 
            // EscogerTipoRespaldo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 396);
            this.Controls.Add(this.btnRespaldarSU);
            this.Controls.Add(this.btnImportarExcel);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnRespaldar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rbNoRespaldar);
            this.Controls.Add(this.rbRespaldarCerrarSesion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
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
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button btnRespaldarSU;
        private System.Windows.Forms.Button btnImportarExcel;
    }
}