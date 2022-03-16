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
            // 
            // lbPorcentajeProducto
            // 
            this.lbPorcentajeProducto.AutoSize = true;
            this.lbPorcentajeProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPorcentajeProducto.Location = new System.Drawing.Point(73, 28);
            this.lbPorcentajeProducto.Name = "lbPorcentajeProducto";
            this.lbPorcentajeProducto.Size = new System.Drawing.Size(180, 17);
            this.lbPorcentajeProducto.TabIndex = 121;
            this.lbPorcentajeProducto.Text = "Porcentaje % de ganancia";
            // 
            // PorcentageGanancia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 116);
            this.Controls.Add(this.btnGuardarPorcentaje);
            this.Controls.Add(this.txtPorcentajeProducto);
            this.Controls.Add(this.lbPorcentajeProducto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PorcentageGanancia";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Porcentage Ganancia";
            this.Load += new System.EventHandler(this.PorcentageGanancia_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PorcentageGanancia_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardarPorcentaje;
        private System.Windows.Forms.TextBox txtPorcentajeProducto;
        private System.Windows.Forms.Label lbPorcentajeProducto;
    }
}