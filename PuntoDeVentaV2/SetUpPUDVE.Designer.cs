namespace PuntoDeVentaV2
{
    partial class SetUpPUDVE
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
            this.cbStockNegativo = new System.Windows.Forms.CheckBox();
            this.btnRespaldo = new System.Windows.Forms.Button();
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cbStockNegativo
            // 
            this.cbStockNegativo.AutoSize = true;
            this.cbStockNegativo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbStockNegativo.Location = new System.Drawing.Point(52, 155);
            this.cbStockNegativo.Name = "cbStockNegativo";
            this.cbStockNegativo.Size = new System.Drawing.Size(177, 21);
            this.cbStockNegativo.TabIndex = 1;
            this.cbStockNegativo.Text = "Permitir Stock negativo";
            this.cbStockNegativo.UseVisualStyleBackColor = true;
            this.cbStockNegativo.CheckedChanged += new System.EventHandler(this.cbStockNegativo_CheckedChanged);
            // 
            // btnRespaldo
            // 
            this.btnRespaldo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnRespaldo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRespaldo.FlatAppearance.BorderSize = 0;
            this.btnRespaldo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnRespaldo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnRespaldo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRespaldo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRespaldo.ForeColor = System.Drawing.Color.White;
            this.btnRespaldo.Location = new System.Drawing.Point(52, 99);
            this.btnRespaldo.Name = "btnRespaldo";
            this.btnRespaldo.Size = new System.Drawing.Size(190, 30);
            this.btnRespaldo.TabIndex = 101;
            this.btnRespaldo.Text = "Respaldar información";
            this.btnRespaldo.UseVisualStyleBackColor = false;
            this.btnRespaldo.Click += new System.EventHandler(this.btnRespaldo_Click);
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(388, 27);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(194, 25);
            this.tituloSeccion.TabIndex = 102;
            this.tituloSeccion.Text = "CONFIGURACIÓN";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // SetUpPUDVE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 561);
            this.Controls.Add(this.tituloSeccion);
            this.Controls.Add(this.btnRespaldo);
            this.Controls.Add(this.cbStockNegativo);
            this.Name = "SetUpPUDVE";
            this.Text = "PUDVE - Configuración";
            this.Load += new System.EventHandler(this.SetUpPUDVE_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox cbStockNegativo;
        private System.Windows.Forms.Button btnRespaldo;
        private System.Windows.Forms.Label tituloSeccion;
    }
}