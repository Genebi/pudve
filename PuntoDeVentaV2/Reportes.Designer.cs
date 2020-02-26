namespace PuntoDeVentaV2
{
    partial class Reportes
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
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.primerSeparador = new System.Windows.Forms.Label();
            this.btnHistorialPrecios = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnHistorialDineroAgregado = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(42, 19);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(900, 25);
            this.tituloSeccion.TabIndex = 24;
            this.tituloSeccion.Text = "REPORTES";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // primerSeparador
            // 
            this.primerSeparador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.primerSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.primerSeparador.Location = new System.Drawing.Point(42, 61);
            this.primerSeparador.Name = "primerSeparador";
            this.primerSeparador.Size = new System.Drawing.Size(900, 2);
            this.primerSeparador.TabIndex = 23;
            this.primerSeparador.Text = "REPORTES";
            this.primerSeparador.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnHistorialPrecios
            // 
            this.btnHistorialPrecios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnHistorialPrecios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistorialPrecios.FlatAppearance.BorderSize = 0;
            this.btnHistorialPrecios.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnHistorialPrecios.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnHistorialPrecios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialPrecios.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHistorialPrecios.ForeColor = System.Drawing.Color.White;
            this.btnHistorialPrecios.Location = new System.Drawing.Point(42, 91);
            this.btnHistorialPrecios.Name = "btnHistorialPrecios";
            this.btnHistorialPrecios.Size = new System.Drawing.Size(190, 30);
            this.btnHistorialPrecios.TabIndex = 102;
            this.btnHistorialPrecios.Text = "Historial de Precios";
            this.btnHistorialPrecios.UseVisualStyleBackColor = false;
            this.btnHistorialPrecios.Click += new System.EventHandler(this.btnHistorialPrecios_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnHistorialDineroAgregado);
            this.groupBox1.Location = new System.Drawing.Point(285, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 129);
            this.groupBox1.TabIndex = 103;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " CAJA ";
            // 
            // btnHistorialDineroAgregado
            // 
            this.btnHistorialDineroAgregado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnHistorialDineroAgregado.FlatAppearance.BorderSize = 0;
            this.btnHistorialDineroAgregado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistorialDineroAgregado.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnHistorialDineroAgregado.ForeColor = System.Drawing.Color.White;
            this.btnHistorialDineroAgregado.Location = new System.Drawing.Point(34, 24);
            this.btnHistorialDineroAgregado.Name = "btnHistorialDineroAgregado";
            this.btnHistorialDineroAgregado.Size = new System.Drawing.Size(190, 30);
            this.btnHistorialDineroAgregado.TabIndex = 1;
            this.btnHistorialDineroAgregado.Text = "Historial Dinero Agreado";
            this.btnHistorialDineroAgregado.UseVisualStyleBackColor = false;
            this.btnHistorialDineroAgregado.Click += new System.EventHandler(this.btnHistorialDineroAgregado_Click);
            // 
            // Reportes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnHistorialPrecios);
            this.Controls.Add(this.tituloSeccion);
            this.Controls.Add(this.primerSeparador);
            this.Name = "Reportes";
            this.Text = "Reportes";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.Label primerSeparador;
        private System.Windows.Forms.Button btnHistorialPrecios;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnHistorialDineroAgregado;
    }
}