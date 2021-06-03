namespace PuntoDeVentaV2
{
    partial class TipoReportesInventario
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
            this.btnRevisarInventario = new System.Windows.Forms.Button();
            this.btnAIAumentar = new System.Windows.Forms.Button();
            this.btnAIDisminuir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnRevisarInventario
            // 
            this.btnRevisarInventario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(181)))));
            this.btnRevisarInventario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRevisarInventario.FlatAppearance.BorderSize = 0;
            this.btnRevisarInventario.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevisarInventario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevisarInventario.ForeColor = System.Drawing.Color.White;
            this.btnRevisarInventario.Image = global::PuntoDeVentaV2.Properties.Resources.sliders1;
            this.btnRevisarInventario.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRevisarInventario.Location = new System.Drawing.Point(34, 66);
            this.btnRevisarInventario.Name = "btnRevisarInventario";
            this.btnRevisarInventario.Size = new System.Drawing.Size(140, 90);
            this.btnRevisarInventario.TabIndex = 108;
            this.btnRevisarInventario.Text = " Revisar Inventario";
            this.btnRevisarInventario.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRevisarInventario.UseVisualStyleBackColor = false;
            this.btnRevisarInventario.Click += new System.EventHandler(this.btnRevisarInventario_Click);
            // 
            // btnAIAumentar
            // 
            this.btnAIAumentar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(67)))), ((int)(((byte)(52)))));
            this.btnAIAumentar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAIAumentar.FlatAppearance.BorderSize = 0;
            this.btnAIAumentar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAIAumentar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAIAumentar.ForeColor = System.Drawing.Color.White;
            this.btnAIAumentar.Image = global::PuntoDeVentaV2.Properties.Resources.sliders1;
            this.btnAIAumentar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAIAumentar.Location = new System.Drawing.Point(194, 66);
            this.btnAIAumentar.Name = "btnAIAumentar";
            this.btnAIAumentar.Size = new System.Drawing.Size(140, 90);
            this.btnAIAumentar.TabIndex = 109;
            this.btnAIAumentar.Text = "Actualizar Inventario (Aumentar)";
            this.btnAIAumentar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAIAumentar.UseVisualStyleBackColor = false;
            this.btnAIAumentar.Click += new System.EventHandler(this.btnAIAumentar_Click);
            // 
            // btnAIDisminuir
            // 
            this.btnAIDisminuir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(67)))), ((int)(((byte)(52)))));
            this.btnAIDisminuir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAIDisminuir.FlatAppearance.BorderSize = 0;
            this.btnAIDisminuir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAIDisminuir.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAIDisminuir.ForeColor = System.Drawing.Color.White;
            this.btnAIDisminuir.Image = global::PuntoDeVentaV2.Properties.Resources.sliders1;
            this.btnAIDisminuir.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAIDisminuir.Location = new System.Drawing.Point(355, 66);
            this.btnAIDisminuir.Name = "btnAIDisminuir";
            this.btnAIDisminuir.Size = new System.Drawing.Size(140, 90);
            this.btnAIDisminuir.TabIndex = 110;
            this.btnAIDisminuir.Text = "Actualizar Inventario (Disminuir)";
            this.btnAIDisminuir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnAIDisminuir.UseVisualStyleBackColor = false;
            this.btnAIDisminuir.Click += new System.EventHandler(this.btnAIDisminuir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(164, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 20);
            this.label1.TabIndex = 111;
            this.label1.Text = "Seleccione una Opcion ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TipoReportesInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 199);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAIDisminuir);
            this.Controls.Add(this.btnAIAumentar);
            this.Controls.Add(this.btnRevisarInventario);
            this.Name = "TipoReportesInventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TipoReportesInventario";
            this.Load += new System.EventHandler(this.TipoReportesInventario_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnRevisarInventario;
        private System.Windows.Forms.Button btnAIAumentar;
        private System.Windows.Forms.Button btnAIDisminuir;
        private System.Windows.Forms.Label label1;
    }
}