namespace PuntoDeVentaV2
{
    partial class AgregarDetalleProducto
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
            this.separadorInicial = new System.Windows.Forms.Label();
            this.txtFiltrar = new System.Windows.Forms.TextBox();
            this.btnGuardarDetalles = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.fLPLateralConcepto = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.fLPCentralDetalle = new System.Windows.Forms.FlowLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // separadorInicial
            // 
            this.separadorInicial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separadorInicial.Location = new System.Drawing.Point(5, 48);
            this.separadorInicial.Name = "separadorInicial";
            this.separadorInicial.Size = new System.Drawing.Size(830, 2);
            this.separadorInicial.TabIndex = 19;
            // 
            // txtFiltrar
            // 
            this.txtFiltrar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltrar.Location = new System.Drawing.Point(11, 18);
            this.txtFiltrar.Name = "txtFiltrar";
            this.txtFiltrar.Size = new System.Drawing.Size(168, 22);
            this.txtFiltrar.TabIndex = 20;
            this.txtFiltrar.Text = "Filtrar...";
            // 
            // btnGuardarDetalles
            // 
            this.btnGuardarDetalles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGuardarDetalles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarDetalles.FlatAppearance.BorderSize = 0;
            this.btnGuardarDetalles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarDetalles.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarDetalles.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnGuardarDetalles.Location = new System.Drawing.Point(656, 621);
            this.btnGuardarDetalles.Name = "btnGuardarDetalles";
            this.btnGuardarDetalles.Size = new System.Drawing.Size(180, 28);
            this.btnGuardarDetalles.TabIndex = 26;
            this.btnGuardarDetalles.Text = "Guardar";
            this.btnGuardarDetalles.UseVisualStyleBackColor = false;
            this.btnGuardarDetalles.Click += new System.EventHandler(this.btnGuardarDetalles_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.fLPLateralConcepto);
            this.panel1.Location = new System.Drawing.Point(3, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 610);
            this.panel1.TabIndex = 27;
            // 
            // fLPLateralConcepto
            // 
            this.fLPLateralConcepto.BackColor = System.Drawing.SystemColors.Control;
            this.fLPLateralConcepto.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLPLateralConcepto.Location = new System.Drawing.Point(4, 5);
            this.fLPLateralConcepto.Name = "fLPLateralConcepto";
            this.fLPLateralConcepto.Size = new System.Drawing.Size(207, 601);
            this.fLPLateralConcepto.TabIndex = 0;
            this.fLPLateralConcepto.WrapContents = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.fLPCentralDetalle);
            this.panel2.Location = new System.Drawing.Point(223, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(618, 545);
            this.panel2.TabIndex = 28;
            // 
            // fLPCentralDetalle
            // 
            this.fLPCentralDetalle.Location = new System.Drawing.Point(5, 5);
            this.fLPCentralDetalle.Name = "fLPCentralDetalle";
            this.fLPCentralDetalle.Size = new System.Drawing.Size(608, 533);
            this.fLPCentralDetalle.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.ForestGreen;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(470, 621);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(180, 28);
            this.button1.TabIndex = 29;
            this.button1.Text = "Agregar Detalle";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // AgregarDetalleProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 666);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnGuardarDetalles);
            this.Controls.Add(this.txtFiltrar);
            this.Controls.Add(this.separadorInicial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "AgregarDetalleProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Detalles Producto";
            this.Load += new System.EventHandler(this.AgregarDetalleProducto_Load);
            this.Shown += new System.EventHandler(this.AgregarDetalleProducto_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label separadorInicial;
        private System.Windows.Forms.TextBox txtFiltrar;
        private System.Windows.Forms.Button btnGuardarDetalles;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel fLPLateralConcepto;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel fLPCentralDetalle;
        private System.Windows.Forms.Button button1;
    }
}