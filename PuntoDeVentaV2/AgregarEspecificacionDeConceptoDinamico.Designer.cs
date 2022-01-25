namespace PuntoDeVentaV2
{
    partial class AgregarEspecificacionDeConceptoDinamico
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
            this.txtEspecificacion = new System.Windows.Forms.TextBox();
            this.btnAgregarEspecificacion = new PuntoDeVentaV2.BotonRedondo();
            this.SuspendLayout();
            // 
            // txtEspecificacion
            // 
            this.txtEspecificacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEspecificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.txtEspecificacion.Location = new System.Drawing.Point(31, 32);
            this.txtEspecificacion.Name = "txtEspecificacion";
            this.txtEspecificacion.Size = new System.Drawing.Size(375, 26);
            this.txtEspecificacion.TabIndex = 2;
            this.txtEspecificacion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEspecificacion.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEspecificacion_KeyDown);
            // 
            // btnAgregarEspecificacion
            // 
            this.btnAgregarEspecificacion.BackColor = System.Drawing.Color.Blue;
            this.btnAgregarEspecificacion.BackGroundColor = System.Drawing.Color.Blue;
            this.btnAgregarEspecificacion.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnAgregarEspecificacion.BorderRadius = 15;
            this.btnAgregarEspecificacion.BorderSize = 0;
            this.btnAgregarEspecificacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarEspecificacion.FlatAppearance.BorderSize = 0;
            this.btnAgregarEspecificacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarEspecificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnAgregarEspecificacion.ForeColor = System.Drawing.Color.White;
            this.btnAgregarEspecificacion.Image = global::PuntoDeVentaV2.Properties.Resources.add;
            this.btnAgregarEspecificacion.Location = new System.Drawing.Point(214, 96);
            this.btnAgregarEspecificacion.Name = "btnAgregarEspecificacion";
            this.btnAgregarEspecificacion.Size = new System.Drawing.Size(192, 61);
            this.btnAgregarEspecificacion.TabIndex = 3;
            this.btnAgregarEspecificacion.Text = "Añadir especificación";
            this.btnAgregarEspecificacion.TextColor = System.Drawing.Color.White;
            this.btnAgregarEspecificacion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAgregarEspecificacion.UseVisualStyleBackColor = false;
            this.btnAgregarEspecificacion.Click += new System.EventHandler(this.btnAgregarEspecificacion_Click);
            // 
            // AgregarEspecificacionDeConceptoDinamico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 169);
            this.Controls.Add(this.btnAgregarEspecificacion);
            this.Controls.Add(this.txtEspecificacion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgregarEspecificacionDeConceptoDinamico";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Agregar especificación";
            this.Load += new System.EventHandler(this.AgregarEspecificacionDeConceptoDinamico_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AgregarEspecificacionDeConceptoDinamico_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtEspecificacion;
        private BotonRedondo btnAgregarEspecificacion;
    }
}