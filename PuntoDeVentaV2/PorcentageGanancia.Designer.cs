namespace PuntoDeVentaV2
{
    partial class PorcentageGanancia
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
            this.btnGuardarPorcentaje = new System.Windows.Forms.Button();
            this.txtPorcentajeProducto = new System.Windows.Forms.TextBox();
            this.lbPorcentajeProducto = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPrecio = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblResultado = new System.Windows.Forms.Label();
            this.txtPorcentaje = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGuardarPorcentaje
            // 
            this.btnGuardarPorcentaje.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnGuardarPorcentaje.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarPorcentaje.FlatAppearance.BorderSize = 0;
            this.btnGuardarPorcentaje.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Gainsboro;
            this.btnGuardarPorcentaje.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gainsboro;
            this.btnGuardarPorcentaje.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGuardarPorcentaje.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarPorcentaje.ForeColor = System.Drawing.Color.Black;
            this.btnGuardarPorcentaje.Location = new System.Drawing.Point(211, 61);
            this.btnGuardarPorcentaje.Name = "btnGuardarPorcentaje";
            this.btnGuardarPorcentaje.Size = new System.Drawing.Size(90, 25);
            this.btnGuardarPorcentaje.TabIndex = 123;
            this.btnGuardarPorcentaje.Text = "Guardar";
            this.btnGuardarPorcentaje.UseVisualStyleBackColor = false;
            this.btnGuardarPorcentaje.Click += new System.EventHandler(this.btnGuardarPorcentaje_Click);
            // 
            // txtPorcentajeProducto
            // 
            this.txtPorcentajeProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentajeProducto.Location = new System.Drawing.Point(15, 61);
            this.txtPorcentajeProducto.Name = "txtPorcentajeProducto";
            this.txtPorcentajeProducto.Size = new System.Drawing.Size(190, 23);
            this.txtPorcentajeProducto.TabIndex = 122;
            this.txtPorcentajeProducto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPorcentajeProducto.TextChanged += new System.EventHandler(this.txtPorcentajeProducto_TextChanged);
            this.txtPorcentajeProducto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPorcentajeProducto_KeyPress);
            // 
            // lbPorcentajeProducto
            // 
            this.lbPorcentajeProducto.AutoSize = true;
            this.lbPorcentajeProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPorcentajeProducto.Location = new System.Drawing.Point(73, 28);
            this.lbPorcentajeProducto.Name = "lbPorcentajeProducto";
            this.lbPorcentajeProducto.Size = new System.Drawing.Size(181, 16);
            this.lbPorcentajeProducto.TabIndex = 121;
            this.lbPorcentajeProducto.Text = "Porcentaje % de ganancia";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(131, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 124;
            this.label1.Text = "Ejemplo:";
            // 
            // txtPrecio
            // 
            this.txtPrecio.Enabled = false;
            this.txtPrecio.Location = new System.Drawing.Point(26, 198);
            this.txtPrecio.Name = "txtPrecio";
            this.txtPrecio.Size = new System.Drawing.Size(54, 20);
            this.txtPrecio.TabIndex = 125;
            this.txtPrecio.Text = "100";
            this.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(21, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 35);
            this.label2.TabIndex = 127;
            this.label2.Text = "Precio Compra:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(107, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 35);
            this.label3.TabIndex = 128;
            this.label3.Text = "Porcentaje de Ganancia";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(216, 160);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 35);
            this.label4.TabIndex = 129;
            this.label4.Text = "Precio Venta";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblResultado
            // 
            this.lblResultado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResultado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResultado.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResultado.Location = new System.Drawing.Point(224, 193);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(86, 28);
            this.lblResultado.TabIndex = 130;
            this.lblResultado.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPorcentaje
            // 
            this.txtPorcentaje.Enabled = false;
            this.txtPorcentaje.Location = new System.Drawing.Point(125, 198);
            this.txtPorcentaje.Name = "txtPorcentaje";
            this.txtPorcentaje.Size = new System.Drawing.Size(52, 20);
            this.txtPorcentaje.TabIndex = 126;
            this.txtPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(171, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 25);
            this.label5.TabIndex = 133;
            this.label5.Text = "%";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PorcentageGanancia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 236);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtPorcentaje);
            this.Controls.Add(this.txtPrecio);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGuardarPorcentaje);
            this.Controls.Add(this.txtPorcentajeProducto);
            this.Controls.Add(this.lbPorcentajeProducto);
            this.Controls.Add(this.label5);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PorcentageGanancia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Porcentaje Ganancia";
            this.Load += new System.EventHandler(this.PorcentageGanancia_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PorcentageGanancia_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardarPorcentaje;
        private System.Windows.Forms.TextBox txtPorcentajeProducto;
        private System.Windows.Forms.Label lbPorcentajeProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPrecio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.TextBox txtPorcentaje;
        private System.Windows.Forms.Label label5;
    }
}