namespace PuntoDeVentaV2
{
    partial class AgregarDescuentoProducto
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
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnCliente = new System.Windows.Forms.Button();
            this.btnMayoreo = new System.Windows.Forms.Button();
            this.btnAceptarDesc = new System.Windows.Forms.Button();
            this.btnCancelarDesc = new System.Windows.Forms.Button();
            this.txtTituloDescuento = new System.Windows.Forms.Label();
            this.panelContenedor = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(17, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(750, 2);
            this.label8.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(15, 435);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(750, 2);
            this.label1.TabIndex = 22;
            // 
            // btnCliente
            // 
            this.btnCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCliente.FlatAppearance.BorderSize = 0;
            this.btnCliente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCliente.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCliente.ForeColor = System.Drawing.Color.White;
            this.btnCliente.Location = new System.Drawing.Point(102, 24);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(200, 35);
            this.btnCliente.TabIndex = 23;
            this.btnCliente.Text = "+ Descuento por Cliente";
            this.btnCliente.UseVisualStyleBackColor = false;
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            // 
            // btnMayoreo
            // 
            this.btnMayoreo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnMayoreo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMayoreo.FlatAppearance.BorderSize = 0;
            this.btnMayoreo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMayoreo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMayoreo.ForeColor = System.Drawing.Color.White;
            this.btnMayoreo.Location = new System.Drawing.Point(486, 24);
            this.btnMayoreo.Name = "btnMayoreo";
            this.btnMayoreo.Size = new System.Drawing.Size(200, 35);
            this.btnMayoreo.TabIndex = 24;
            this.btnMayoreo.Text = "+ Descuento por Mayoreo";
            this.btnMayoreo.UseVisualStyleBackColor = false;
            this.btnMayoreo.Click += new System.EventHandler(this.btnMayoreo_Click);
            // 
            // btnAceptarDesc
            // 
            this.btnAceptarDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptarDesc.BackColor = System.Drawing.Color.Green;
            this.btnAceptarDesc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptarDesc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnAceptarDesc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptarDesc.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptarDesc.ForeColor = System.Drawing.Color.White;
            this.btnAceptarDesc.Location = new System.Drawing.Point(623, 453);
            this.btnAceptarDesc.Name = "btnAceptarDesc";
            this.btnAceptarDesc.Size = new System.Drawing.Size(144, 28);
            this.btnAceptarDesc.TabIndex = 27;
            this.btnAceptarDesc.Text = "Aceptar";
            this.btnAceptarDesc.UseVisualStyleBackColor = false;
            this.btnAceptarDesc.Click += new System.EventHandler(this.btnAceptarDesc_Click);
            // 
            // btnCancelarDesc
            // 
            this.btnCancelarDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelarDesc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarDesc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnCancelarDesc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarDesc.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarDesc.ForeColor = System.Drawing.Color.White;
            this.btnCancelarDesc.Location = new System.Drawing.Point(473, 453);
            this.btnCancelarDesc.Name = "btnCancelarDesc";
            this.btnCancelarDesc.Size = new System.Drawing.Size(144, 28);
            this.btnCancelarDesc.TabIndex = 26;
            this.btnCancelarDesc.Text = "Cancelar";
            this.btnCancelarDesc.UseVisualStyleBackColor = false;
            this.btnCancelarDesc.Click += new System.EventHandler(this.btnCancelarDesc_Click);
            // 
            // txtTituloDescuento
            // 
            this.txtTituloDescuento.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTituloDescuento.Location = new System.Drawing.Point(17, 94);
            this.txtTituloDescuento.Name = "txtTituloDescuento";
            this.txtTituloDescuento.Size = new System.Drawing.Size(748, 23);
            this.txtTituloDescuento.TabIndex = 28;
            this.txtTituloDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelContenedor
            // 
            this.panelContenedor.Location = new System.Drawing.Point(17, 132);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(748, 291);
            this.panelContenedor.TabIndex = 29;
            // 
            // AgregarDescuentoProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(780, 497);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.txtTituloDescuento);
            this.Controls.Add(this.btnAceptarDesc);
            this.Controls.Add(this.btnCancelarDesc);
            this.Controls.Add(this.btnMayoreo);
            this.Controls.Add(this.btnCliente);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "AgregarDescuentoProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Descuento Producto";
            this.Load += new System.EventHandler(this.AgregarDescuentoProducto_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.Button btnMayoreo;
        private System.Windows.Forms.Button btnAceptarDesc;
        private System.Windows.Forms.Button btnCancelarDesc;
        private System.Windows.Forms.Label txtTituloDescuento;
        private System.Windows.Forms.FlowLayoutPanel panelContenedor;
    }
}