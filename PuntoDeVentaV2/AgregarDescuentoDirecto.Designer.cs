namespace PuntoDeVentaV2
{
    partial class AgregarDescuentoDirecto
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
            this.lbProducto = new System.Windows.Forms.Label();
            this.lbPrecio = new System.Windows.Forms.Label();
            this.lbDescuento = new System.Windows.Forms.Label();
            this.txtPorcentaje = new System.Windows.Forms.TextBox();
            this.lbCantidad = new System.Windows.Forms.Label();
            this.lbPorcentaje = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.primerSeparador = new System.Windows.Forms.Label();
            this.lbTotalDescuento = new System.Windows.Forms.Label();
            this.lbTotal = new System.Windows.Forms.Label();
            this.lbTotalFinal = new System.Windows.Forms.Label();
            this.segundoSeparador = new System.Windows.Forms.Label();
            this.lbCantidadProducto = new System.Windows.Forms.Label();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lbProducto
            // 
            this.lbProducto.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProducto.Location = new System.Drawing.Point(18, 21);
            this.lbProducto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbProducto.Name = "lbProducto";
            this.lbProducto.Size = new System.Drawing.Size(350, 17);
            this.lbProducto.TabIndex = 0;
            this.lbProducto.Text = "Nombre del producto";
            this.lbProducto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPrecio
            // 
            this.lbPrecio.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrecio.Location = new System.Drawing.Point(18, 47);
            this.lbPrecio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPrecio.Name = "lbPrecio";
            this.lbPrecio.Size = new System.Drawing.Size(350, 17);
            this.lbPrecio.TabIndex = 1;
            this.lbPrecio.Text = "Precio";
            this.lbPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbDescuento
            // 
            this.lbDescuento.AutoSize = true;
            this.lbDescuento.Location = new System.Drawing.Point(77, 177);
            this.lbDescuento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDescuento.Name = "lbDescuento";
            this.lbDescuento.Size = new System.Drawing.Size(127, 17);
            this.lbDescuento.TabIndex = 2;
            this.lbDescuento.Text = "Descuento total $:";
            this.lbDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPorcentaje
            // 
            this.txtPorcentaje.Location = new System.Drawing.Point(236, 128);
            this.txtPorcentaje.Margin = new System.Windows.Forms.Padding(4);
            this.txtPorcentaje.MaxLength = 5;
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.ShortcutsEnabled = false;
            this.txtPorcentaje.Size = new System.Drawing.Size(132, 23);
            this.txtPorcentaje.TabIndex = 4;
            this.txtPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPorcentaje.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPorcentaje_KeyDown);
            this.txtPorcentaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPorcentaje_KeyPress);
            this.txtPorcentaje.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPorcentaje_KeyUp);
            // 
            // lbCantidad
            // 
            this.lbCantidad.Location = new System.Drawing.Point(21, 107);
            this.lbCantidad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCantidad.Name = "lbCantidad";
            this.lbCantidad.Size = new System.Drawing.Size(132, 17);
            this.lbCantidad.TabIndex = 5;
            this.lbCantidad.Text = "Cantidad";
            this.lbCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPorcentaje
            // 
            this.lbPorcentaje.Location = new System.Drawing.Point(236, 107);
            this.lbPorcentaje.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPorcentaje.Name = "lbPorcentaje";
            this.lbPorcentaje.Size = new System.Drawing.Size(132, 17);
            this.lbPorcentaje.TabIndex = 6;
            this.lbPorcentaje.Text = "Porcentaje";
            this.lbPorcentaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.BackColor = System.Drawing.Color.Green;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(224, 251);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(144, 28);
            this.btnAceptar.TabIndex = 28;
            this.btnAceptar.Text = "Aplicar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // primerSeparador
            // 
            this.primerSeparador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.primerSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.primerSeparador.Location = new System.Drawing.Point(18, 92);
            this.primerSeparador.Name = "primerSeparador";
            this.primerSeparador.Size = new System.Drawing.Size(350, 2);
            this.primerSeparador.TabIndex = 29;
            this.primerSeparador.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbTotalDescuento
            // 
            this.lbTotalDescuento.Location = new System.Drawing.Point(201, 177);
            this.lbTotalDescuento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTotalDescuento.Name = "lbTotalDescuento";
            this.lbTotalDescuento.Size = new System.Drawing.Size(167, 17);
            this.lbTotalDescuento.TabIndex = 30;
            this.lbTotalDescuento.Text = "0.00";
            this.lbTotalDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTotal
            // 
            this.lbTotal.AutoSize = true;
            this.lbTotal.Location = new System.Drawing.Point(49, 204);
            this.lbTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(155, 17);
            this.lbTotal.TabIndex = 31;
            this.lbTotal.Text = "Total con descuento $:";
            this.lbTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTotalFinal
            // 
            this.lbTotalFinal.Location = new System.Drawing.Point(201, 204);
            this.lbTotalFinal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTotalFinal.Name = "lbTotalFinal";
            this.lbTotalFinal.Size = new System.Drawing.Size(167, 17);
            this.lbTotalFinal.TabIndex = 32;
            this.lbTotalFinal.Text = "0.00";
            this.lbTotalFinal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // segundoSeparador
            // 
            this.segundoSeparador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.segundoSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.segundoSeparador.Location = new System.Drawing.Point(17, 235);
            this.segundoSeparador.Name = "segundoSeparador";
            this.segundoSeparador.Size = new System.Drawing.Size(350, 2);
            this.segundoSeparador.TabIndex = 34;
            this.segundoSeparador.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbCantidadProducto
            // 
            this.lbCantidadProducto.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCantidadProducto.Location = new System.Drawing.Point(18, 70);
            this.lbCantidadProducto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCantidadProducto.Name = "lbCantidadProducto";
            this.lbCantidadProducto.Size = new System.Drawing.Size(350, 17);
            this.lbCantidadProducto.TabIndex = 35;
            this.lbCantidadProducto.Text = "Cantidad";
            this.lbCantidadProducto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbCantidadProducto.Visible = false;
            // 
            // btnEliminar
            // 
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(17, 251);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(144, 28);
            this.btnEliminar.TabIndex = 36;
            this.btnEliminar.Text = "Eliminar descuento";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(21, 128);
            this.txtCantidad.Margin = new System.Windows.Forms.Padding(4);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.ShortcutsEnabled = false;
            this.txtCantidad.Size = new System.Drawing.Size(132, 23);
            this.txtCantidad.TabIndex = 37;
            this.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCantidad_KeyDown_1);
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
            this.txtCantidad.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCantidad_KeyUp);
            this.txtCantidad.Leave += new System.EventHandler(this.txtCantidad_Leave);
            // 
            // AgregarDescuentoDirecto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 291);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.lbCantidadProducto);
            this.Controls.Add(this.segundoSeparador);
            this.Controls.Add(this.lbTotalFinal);
            this.Controls.Add(this.lbTotal);
            this.Controls.Add(this.lbTotalDescuento);
            this.Controls.Add(this.primerSeparador);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.lbPorcentaje);
            this.Controls.Add(this.lbCantidad);
            this.Controls.Add(this.txtPorcentaje);
            this.Controls.Add(this.lbDescuento);
            this.Controls.Add(this.lbPrecio);
            this.Controls.Add(this.lbProducto);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgregarDescuentoDirecto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AgregarDescuentoDirecto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbProducto;
        private System.Windows.Forms.Label lbPrecio;
        private System.Windows.Forms.Label lbDescuento;
        private System.Windows.Forms.TextBox txtPorcentaje;
        private System.Windows.Forms.Label lbCantidad;
        private System.Windows.Forms.Label lbPorcentaje;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label primerSeparador;
        private System.Windows.Forms.Label lbTotalDescuento;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.Label lbTotalFinal;
        private System.Windows.Forms.Label segundoSeparador;
        private System.Windows.Forms.Label lbCantidadProducto;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.TextBox txtCantidad;
    }
}