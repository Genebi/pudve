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
            this.components = new System.ComponentModel.Container();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAceptarDesc = new System.Windows.Forms.Button();
            this.btnCancelarDesc = new System.Windows.Forms.Button();
            this.txtTituloDescuento = new System.Windows.Forms.Label();
            this.panelContenedor = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.rbCliente = new System.Windows.Forms.RadioButton();
            this.rbMayoreo = new System.Windows.Forms.RadioButton();
            this.btnEliminarDescuentos = new System.Windows.Forms.Button();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(17, 92);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(750, 2);
            this.label8.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(15, 450);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(750, 2);
            this.label1.TabIndex = 22;
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
            this.btnAceptarDesc.Location = new System.Drawing.Point(623, 476);
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
            this.btnCancelarDesc.Location = new System.Drawing.Point(473, 476);
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
            this.txtTituloDescuento.Location = new System.Drawing.Point(13, 24);
            this.txtTituloDescuento.Name = "txtTituloDescuento";
            this.txtTituloDescuento.Size = new System.Drawing.Size(754, 23);
            this.txtTituloDescuento.TabIndex = 28;
            this.txtTituloDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelContenedor
            // 
            this.panelContenedor.AutoScroll = true;
            this.panelContenedor.Location = new System.Drawing.Point(17, 104);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(748, 280);
            this.panelContenedor.TabIndex = 29;
            this.panelContenedor.WrapContents = false;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(15, 395);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(750, 2);
            this.label2.TabIndex = 30;
            // 
            // rbCliente
            // 
            this.rbCliente.AutoSize = true;
            this.rbCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbCliente.Location = new System.Drawing.Point(173, 412);
            this.rbCliente.Name = "rbCliente";
            this.rbCliente.Size = new System.Drawing.Size(200, 24);
            this.rbCliente.TabIndex = 31;
            this.rbCliente.TabStop = true;
            this.rbCliente.Text = "Descuento por Producto";
            this.rbCliente.UseVisualStyleBackColor = true;
            this.rbCliente.CheckedChanged += new System.EventHandler(this.rbCliente_CheckedChanged);
            this.rbCliente.Click += new System.EventHandler(this.rbCliente_Click);
            // 
            // rbMayoreo
            // 
            this.rbMayoreo.AutoSize = true;
            this.rbMayoreo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMayoreo.Location = new System.Drawing.Point(404, 412);
            this.rbMayoreo.Name = "rbMayoreo";
            this.rbMayoreo.Size = new System.Drawing.Size(197, 24);
            this.rbMayoreo.TabIndex = 32;
            this.rbMayoreo.TabStop = true;
            this.rbMayoreo.Text = "Descuento por Mayoreo";
            this.rbMayoreo.UseVisualStyleBackColor = true;
            this.rbMayoreo.CheckedChanged += new System.EventHandler(this.rbMayoreo_CheckedChanged);
            this.rbMayoreo.Click += new System.EventHandler(this.rbMayoreo_Click);
            // 
            // btnEliminarDescuentos
            // 
            this.btnEliminarDescuentos.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminarDescuentos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnEliminarDescuentos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminarDescuentos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnEliminarDescuentos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminarDescuentos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarDescuentos.ForeColor = System.Drawing.Color.White;
            this.btnEliminarDescuentos.Location = new System.Drawing.Point(17, 476);
            this.btnEliminarDescuentos.Name = "btnEliminarDescuentos";
            this.btnEliminarDescuentos.Size = new System.Drawing.Size(156, 28);
            this.btnEliminarDescuentos.TabIndex = 33;
            this.btnEliminarDescuentos.Text = "Eliminar descuentos";
            this.btnEliminarDescuentos.UseVisualStyleBackColor = false;
            this.btnEliminarDescuentos.Click += new System.EventHandler(this.btnEliminarDescuentos_Click);
            // 
            // lblMensaje
            // 
            this.lblMensaje.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblMensaje.Location = new System.Drawing.Point(13, 61);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(754, 23);
            this.lblMensaje.TabIndex = 34;
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AgregarDescuentoProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(780, 520);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.btnEliminarDescuentos);
            this.Controls.Add(this.rbMayoreo);
            this.Controls.Add(this.rbCliente);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.txtTituloDescuento);
            this.Controls.Add(this.btnAceptarDesc);
            this.Controls.Add(this.btnCancelarDesc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 399);
            this.Name = "AgregarDescuentoProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Descuento Producto";
            this.Activated += new System.EventHandler(this.AgregarDescuentoProducto_Activated);
            this.Load += new System.EventHandler(this.AgregarDescuentoProducto_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AgregarDescuentoProducto_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAceptarDesc;
        private System.Windows.Forms.Button btnCancelarDesc;
        private System.Windows.Forms.Label txtTituloDescuento;
        private System.Windows.Forms.FlowLayoutPanel panelContenedor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbCliente;
        private System.Windows.Forms.RadioButton rbMayoreo;
        private System.Windows.Forms.Button btnEliminarDescuentos;
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Timer timer1;
    }
}