namespace PuntoDeVentaV2
{
    partial class OpcionesReporteProducto
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
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.primerSeparador = new System.Windows.Forms.Label();
            this.btnFiltroReporte = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelContenedor
            // 
            this.panelContenedor.AutoScroll = true;
            this.panelContenedor.Location = new System.Drawing.Point(2, 7);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(379, 301);
            this.panelContenedor.TabIndex = 0;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.Green;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(272, 321);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(105, 28);
            this.btnAceptar.TabIndex = 17;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(161, 322);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(105, 27);
            this.btnCancelar.TabIndex = 18;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // primerSeparador
            // 
            this.primerSeparador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.primerSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.primerSeparador.Location = new System.Drawing.Point(7, 314);
            this.primerSeparador.Name = "primerSeparador";
            this.primerSeparador.Size = new System.Drawing.Size(370, 2);
            this.primerSeparador.TabIndex = 22;
            this.primerSeparador.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnFiltroReporte
            // 
            this.btnFiltroReporte.BackColor = System.Drawing.Color.YellowGreen;
            this.btnFiltroReporte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltroReporte.FlatAppearance.BorderSize = 0;
            this.btnFiltroReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltroReporte.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFiltroReporte.ForeColor = System.Drawing.Color.White;
            this.btnFiltroReporte.Image = global::PuntoDeVentaV2.Properties.Resources.filter;
            this.btnFiltroReporte.Location = new System.Drawing.Point(7, 322);
            this.btnFiltroReporte.Name = "btnFiltroReporte";
            this.btnFiltroReporte.Size = new System.Drawing.Size(70, 27);
            this.btnFiltroReporte.TabIndex = 23;
            this.btnFiltroReporte.Text = "Filtro";
            this.btnFiltroReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFiltroReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFiltroReporte.UseVisualStyleBackColor = false;
            this.btnFiltroReporte.Click += new System.EventHandler(this.btnFiltroReporte_Click);
            // 
            // OpcionesReporteProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 361);
            this.Controls.Add(this.btnFiltroReporte);
            this.Controls.Add(this.primerSeparador);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.panelContenedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpcionesReporteProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Opciones Reporte";
            this.Load += new System.EventHandler(this.OpcionesReporteProducto_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label primerSeparador;
        private System.Windows.Forms.Button btnFiltroReporte;
    }
}