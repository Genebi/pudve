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
            this.lblConceptoDinamico = new System.Windows.Forms.Label();
            this.DGVEspecificacionesActivas = new System.Windows.Forms.DataGridView();
            this.txtEspecificacion = new System.Windows.Forms.TextBox();
            this.btnAgregarEspecificacion = new PuntoDeVentaV2.BotonRedondo();
            ((System.ComponentModel.ISupportInitialize)(this.DGVEspecificacionesActivas)).BeginInit();
            this.SuspendLayout();
            // 
            // lblConceptoDinamico
            // 
            this.lblConceptoDinamico.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblConceptoDinamico.Location = new System.Drawing.Point(2, 12);
            this.lblConceptoDinamico.Name = "lblConceptoDinamico";
            this.lblConceptoDinamico.Size = new System.Drawing.Size(568, 27);
            this.lblConceptoDinamico.TabIndex = 0;
            this.lblConceptoDinamico.Text = "Especificaciones existentes del concepto ";
            this.lblConceptoDinamico.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DGVEspecificacionesActivas
            // 
            this.DGVEspecificacionesActivas.AllowUserToAddRows = false;
            this.DGVEspecificacionesActivas.AllowUserToDeleteRows = false;
            this.DGVEspecificacionesActivas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVEspecificacionesActivas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVEspecificacionesActivas.Location = new System.Drawing.Point(12, 47);
            this.DGVEspecificacionesActivas.Name = "DGVEspecificacionesActivas";
            this.DGVEspecificacionesActivas.ReadOnly = true;
            this.DGVEspecificacionesActivas.RowHeadersVisible = false;
            this.DGVEspecificacionesActivas.Size = new System.Drawing.Size(558, 150);
            this.DGVEspecificacionesActivas.TabIndex = 1;
            // 
            // txtEspecificacion
            // 
            this.txtEspecificacion.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEspecificacion.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEspecificacion.Location = new System.Drawing.Point(12, 226);
            this.txtEspecificacion.Name = "txtEspecificacion";
            this.txtEspecificacion.Size = new System.Drawing.Size(407, 27);
            this.txtEspecificacion.TabIndex = 2;
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
            this.btnAgregarEspecificacion.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarEspecificacion.ForeColor = System.Drawing.Color.White;
            this.btnAgregarEspecificacion.Image = global::PuntoDeVentaV2.Properties.Resources.add;
            this.btnAgregarEspecificacion.Location = new System.Drawing.Point(425, 204);
            this.btnAgregarEspecificacion.Name = "btnAgregarEspecificacion";
            this.btnAgregarEspecificacion.Size = new System.Drawing.Size(145, 73);
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
            this.ClientSize = new System.Drawing.Size(591, 293);
            this.Controls.Add(this.btnAgregarEspecificacion);
            this.Controls.Add(this.txtEspecificacion);
            this.Controls.Add(this.DGVEspecificacionesActivas);
            this.Controls.Add(this.lblConceptoDinamico);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgregarEspecificacionDeConceptoDinamico";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Especificacion para agregar";
            this.Load += new System.EventHandler(this.AgregarEspecificacionDeConceptoDinamico_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVEspecificacionesActivas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblConceptoDinamico;
        private System.Windows.Forms.DataGridView DGVEspecificacionesActivas;
        private System.Windows.Forms.TextBox txtEspecificacion;
        private BotonRedondo btnAgregarEspecificacion;
    }
}