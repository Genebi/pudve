
namespace PuntoDeVentaV2
{
    partial class ConfiguracionEditarProducto
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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.rbPeroAutomatico = new System.Windows.Forms.RadioButton();
            this.rbSoloPorEnteros = new System.Windows.Forms.RadioButton();
            this.rbVenderNormalmente = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
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
            this.btnAceptar.Location = new System.Drawing.Point(217, 292);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(176, 28);
            this.btnAceptar.TabIndex = 16;
            this.btnAceptar.Text = "Guardar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(35, 292);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(176, 28);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // rbPeroAutomatico
            // 
            this.rbPeroAutomatico.AutoSize = true;
            this.rbPeroAutomatico.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbPeroAutomatico.Location = new System.Drawing.Point(47, 16);
            this.rbPeroAutomatico.Name = "rbPeroAutomatico";
            this.rbPeroAutomatico.Size = new System.Drawing.Size(356, 22);
            this.rbPeroAutomatico.TabIndex = 19;
            this.rbPeroAutomatico.TabStop = true;
            this.rbPeroAutomatico.Text = "Tomar Peso En Automatico Al Realizar Una Venta";
            this.rbPeroAutomatico.UseVisualStyleBackColor = true;
            // 
            // rbSoloPorEnteros
            // 
            this.rbSoloPorEnteros.AutoSize = true;
            this.rbSoloPorEnteros.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbSoloPorEnteros.Location = new System.Drawing.Point(47, 52);
            this.rbSoloPorEnteros.Name = "rbSoloPorEnteros";
            this.rbSoloPorEnteros.Size = new System.Drawing.Size(191, 22);
            this.rbSoloPorEnteros.TabIndex = 20;
            this.rbSoloPorEnteros.TabStop = true;
            this.rbSoloPorEnteros.Text = "Vender Solo Por Enteros";
            this.rbSoloPorEnteros.UseVisualStyleBackColor = true;
            // 
            // rbVenderNormalmente
            // 
            this.rbVenderNormalmente.AutoSize = true;
            this.rbVenderNormalmente.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbVenderNormalmente.Location = new System.Drawing.Point(47, 89);
            this.rbVenderNormalmente.Name = "rbVenderNormalmente";
            this.rbVenderNormalmente.Size = new System.Drawing.Size(245, 22);
            this.rbVenderNormalmente.TabIndex = 21;
            this.rbVenderNormalmente.TabStop = true;
            this.rbVenderNormalmente.Text = "Vender Con Enteros y Decimales";
            this.rbVenderNormalmente.UseVisualStyleBackColor = true;
            // 
            // ConfiguracionEditarProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 332);
            this.Controls.Add(this.rbVenderNormalmente);
            this.Controls.Add(this.rbSoloPorEnteros);
            this.Controls.Add(this.rbPeroAutomatico);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnAceptar);
            this.Name = "ConfiguracionEditarProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ConfiguracionEditarProducto";
            this.Load += new System.EventHandler(this.ConfiguracionEditarProducto_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.RadioButton rbPeroAutomatico;
        private System.Windows.Forms.RadioButton rbSoloPorEnteros;
        private System.Windows.Forms.RadioButton rbVenderNormalmente;
    }
}