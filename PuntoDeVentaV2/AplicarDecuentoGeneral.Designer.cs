
namespace PuntoDeVentaV2
{
    partial class AplicarDecuentoGeneral
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
            this.txtCantidad = new System.Windows.Forms.TextBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.segundoSeparador = new System.Windows.Forms.Label();
            this.lbTotalFinal = new System.Windows.Forms.Label();
            this.lbTotal = new System.Windows.Forms.Label();
            this.lbTotalDescuento = new System.Windows.Forms.Label();
            this.primerSeparador = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lbPorcentaje = new System.Windows.Forms.Label();
            this.lbCantidad = new System.Windows.Forms.Label();
            this.txtPorcentaje = new System.Windows.Forms.TextBox();
            this.lbDescuento = new System.Windows.Forms.Label();
            this.lbPrecio = new System.Windows.Forms.Label();
            this.lbProducto = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCantidad
            // 
            this.txtCantidad.Location = new System.Drawing.Point(21, 123);
            this.txtCantidad.Margin = new System.Windows.Forms.Padding(4);
            this.txtCantidad.Name = "txtCantidad";
            this.txtCantidad.ShortcutsEnabled = false;
            this.txtCantidad.Size = new System.Drawing.Size(132, 20);
            this.txtCantidad.TabIndex = 52;
            this.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCantidad.TextChanged += new System.EventHandler(this.txtCantidad_TextChanged);
            this.txtCantidad.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidad_KeyPress);
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
            this.btnEliminar.Location = new System.Drawing.Point(17, 246);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(144, 28);
            this.btnEliminar.TabIndex = 51;
            this.btnEliminar.Text = "Eliminar descuento";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // segundoSeparador
            // 
            this.segundoSeparador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.segundoSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.segundoSeparador.Location = new System.Drawing.Point(17, 230);
            this.segundoSeparador.Name = "segundoSeparador";
            this.segundoSeparador.Size = new System.Drawing.Size(350, 2);
            this.segundoSeparador.TabIndex = 49;
            this.segundoSeparador.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbTotalFinal
            // 
            this.lbTotalFinal.Location = new System.Drawing.Point(201, 199);
            this.lbTotalFinal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTotalFinal.Name = "lbTotalFinal";
            this.lbTotalFinal.Size = new System.Drawing.Size(167, 17);
            this.lbTotalFinal.TabIndex = 48;
            this.lbTotalFinal.Text = "0.00";
            this.lbTotalFinal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbTotal
            // 
            this.lbTotal.AutoSize = true;
            this.lbTotal.Location = new System.Drawing.Point(49, 199);
            this.lbTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTotal.Name = "lbTotal";
            this.lbTotal.Size = new System.Drawing.Size(117, 13);
            this.lbTotal.TabIndex = 47;
            this.lbTotal.Text = "Total con descuento $:";
            this.lbTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbTotalDescuento
            // 
            this.lbTotalDescuento.Location = new System.Drawing.Point(201, 172);
            this.lbTotalDescuento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbTotalDescuento.Name = "lbTotalDescuento";
            this.lbTotalDescuento.Size = new System.Drawing.Size(167, 17);
            this.lbTotalDescuento.TabIndex = 46;
            this.lbTotalDescuento.Text = "0.00";
            this.lbTotalDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // primerSeparador
            // 
            this.primerSeparador.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.primerSeparador.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.primerSeparador.Location = new System.Drawing.Point(18, 87);
            this.primerSeparador.Name = "primerSeparador";
            this.primerSeparador.Size = new System.Drawing.Size(350, 2);
            this.primerSeparador.TabIndex = 45;
            this.primerSeparador.TextAlign = System.Drawing.ContentAlignment.TopCenter;
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
            this.btnAceptar.Location = new System.Drawing.Point(224, 246);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(144, 28);
            this.btnAceptar.TabIndex = 44;
            this.btnAceptar.Text = "Aplicar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lbPorcentaje
            // 
            this.lbPorcentaje.Location = new System.Drawing.Point(236, 102);
            this.lbPorcentaje.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPorcentaje.Name = "lbPorcentaje";
            this.lbPorcentaje.Size = new System.Drawing.Size(132, 17);
            this.lbPorcentaje.TabIndex = 43;
            this.lbPorcentaje.Text = "Porcentaje";
            this.lbPorcentaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCantidad
            // 
            this.lbCantidad.Location = new System.Drawing.Point(21, 102);
            this.lbCantidad.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbCantidad.Name = "lbCantidad";
            this.lbCantidad.Size = new System.Drawing.Size(132, 17);
            this.lbCantidad.TabIndex = 42;
            this.lbCantidad.Text = "Cantidad";
            this.lbCantidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPorcentaje
            // 
            this.txtPorcentaje.Location = new System.Drawing.Point(236, 123);
            this.txtPorcentaje.Margin = new System.Windows.Forms.Padding(4);
            this.txtPorcentaje.MaxLength = 5;
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.ShortcutsEnabled = false;
            this.txtPorcentaje.Size = new System.Drawing.Size(132, 20);
            this.txtPorcentaje.TabIndex = 41;
            this.txtPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPorcentaje.TextChanged += new System.EventHandler(this.txtPorcentaje_TextChanged);
            this.txtPorcentaje.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPorcentaje_KeyPress);
            // 
            // lbDescuento
            // 
            this.lbDescuento.AutoSize = true;
            this.lbDescuento.Location = new System.Drawing.Point(77, 172);
            this.lbDescuento.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbDescuento.Name = "lbDescuento";
            this.lbDescuento.Size = new System.Drawing.Size(94, 13);
            this.lbDescuento.TabIndex = 40;
            this.lbDescuento.Text = "Descuento total $:";
            this.lbDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPrecio
            // 
            this.lbPrecio.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrecio.Location = new System.Drawing.Point(18, 42);
            this.lbPrecio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbPrecio.Name = "lbPrecio";
            this.lbPrecio.Size = new System.Drawing.Size(350, 17);
            this.lbPrecio.TabIndex = 39;
            this.lbPrecio.Text = "Precio";
            this.lbPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbProducto
            // 
            this.lbProducto.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProducto.Location = new System.Drawing.Point(18, 16);
            this.lbProducto.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbProducto.Name = "lbProducto";
            this.lbProducto.Size = new System.Drawing.Size(350, 17);
            this.lbProducto.TabIndex = 38;
            this.lbProducto.Text = "Descuanto General";
            this.lbProducto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AplicarDecuentoGeneral
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 291);
            this.Controls.Add(this.txtCantidad);
            this.Controls.Add(this.btnEliminar);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AplicarDecuentoGeneral";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.AplicarDecuentoGeneral_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AplicarDecuentoGeneral_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCantidad;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Label segundoSeparador;
        private System.Windows.Forms.Label lbTotalFinal;
        private System.Windows.Forms.Label lbTotal;
        private System.Windows.Forms.Label lbTotalDescuento;
        private System.Windows.Forms.Label primerSeparador;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lbPorcentaje;
        private System.Windows.Forms.Label lbCantidad;
        private System.Windows.Forms.TextBox txtPorcentaje;
        private System.Windows.Forms.Label lbDescuento;
        private System.Windows.Forms.Label lbPrecio;
        private System.Windows.Forms.Label lbProducto;
    }
}