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
            this.cbSeleccionados = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbSeleccionarTodas = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelContenedor
            // 
            this.panelContenedor.AutoScroll = true;
            this.panelContenedor.Location = new System.Drawing.Point(2, 61);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(379, 279);
            this.panelContenedor.TabIndex = 0;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.Green;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(272, 353);
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
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(161, 354);
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
            this.primerSeparador.Location = new System.Drawing.Point(7, 346);
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
            this.btnFiltroReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnFiltroReporte.ForeColor = System.Drawing.Color.White;
            this.btnFiltroReporte.Image = global::PuntoDeVentaV2.Properties.Resources.filter;
            this.btnFiltroReporte.Location = new System.Drawing.Point(7, 354);
            this.btnFiltroReporte.Name = "btnFiltroReporte";
            this.btnFiltroReporte.Size = new System.Drawing.Size(70, 27);
            this.btnFiltroReporte.TabIndex = 23;
            this.btnFiltroReporte.Text = "Filtro";
            this.btnFiltroReporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFiltroReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFiltroReporte.UseVisualStyleBackColor = false;
            this.btnFiltroReporte.Visible = false;
            this.btnFiltroReporte.Click += new System.EventHandler(this.btnFiltroReporte_Click);
            // 
            // cbSeleccionados
            // 
            this.cbSeleccionados.AutoSize = true;
            this.cbSeleccionados.Checked = true;
            this.cbSeleccionados.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSeleccionados.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSeleccionados.Location = new System.Drawing.Point(7, 13);
            this.cbSeleccionados.Name = "cbSeleccionados";
            this.cbSeleccionados.Size = new System.Drawing.Size(208, 17);
            this.cbSeleccionados.TabIndex = 24;
            this.cbSeleccionados.Text = "Aplicar solo a los seleccionados";
            this.cbSeleccionados.UseVisualStyleBackColor = true;
            this.cbSeleccionados.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(7, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(370, 2);
            this.label1.TabIndex = 25;
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // cbSeleccionarTodas
            // 
            this.cbSeleccionarTodas.AutoSize = true;
            this.cbSeleccionarTodas.Location = new System.Drawing.Point(7, 36);
            this.cbSeleccionarTodas.Name = "cbSeleccionarTodas";
            this.cbSeleccionarTodas.Size = new System.Drawing.Size(173, 17);
            this.cbSeleccionarTodas.TabIndex = 26;
            this.cbSeleccionarTodas.Text = "Seleccionar todas las opciones";
            this.cbSeleccionarTodas.UseVisualStyleBackColor = true;
            this.cbSeleccionarTodas.CheckedChanged += new System.EventHandler(this.cbSeleccionarTodas_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(7, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(370, 2);
            this.label2.TabIndex = 27;
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(340, 13);
            this.label3.TabIndex = 28;
            this.label3.Text = "En el reporte solo se mostraran los articulos seleccionados";
            // 
            // OpcionesReporteProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 391);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSeleccionarTodas);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbSeleccionados);
            this.Controls.Add(this.btnFiltroReporte);
            this.Controls.Add(this.primerSeparador);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.panelContenedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpcionesReporteProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Opciones Reporte";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OpcionesReporteProducto_FormClosing);
            this.Load += new System.EventHandler(this.OpcionesReporteProducto_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OpcionesReporteProducto_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label primerSeparador;
        private System.Windows.Forms.Button btnFiltroReporte;
        private System.Windows.Forms.CheckBox cbSeleccionados;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbSeleccionarTodas;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}