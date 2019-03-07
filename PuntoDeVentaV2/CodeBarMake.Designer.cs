namespace PuntoDeVentaV2
{
    partial class CodeBarMake
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
            this.panelResultado = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNombreProd = new System.Windows.Forms.Label();
            this.lblCodBarPrecio = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelResultado
            // 
            this.panelResultado.Location = new System.Drawing.Point(64, 111);
            this.panelResultado.Name = "panelResultado";
            this.panelResultado.Size = new System.Drawing.Size(157, 30);
            this.panelResultado.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Century", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(249, 23);
            this.label1.TabIndex = 5;
            this.label1.Text = "www.pudve.com";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Century", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(253, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Punto de Venta Gratuito";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNombreProd
            // 
            this.lblNombreProd.Font = new System.Drawing.Font("Century", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreProd.Location = new System.Drawing.Point(16, 80);
            this.lblNombreProd.Name = "lblNombreProd";
            this.lblNombreProd.Size = new System.Drawing.Size(257, 23);
            this.lblNombreProd.TabIndex = 7;
            this.lblNombreProd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCodBarPrecio
            // 
            this.lblCodBarPrecio.Font = new System.Drawing.Font("Century", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodBarPrecio.Location = new System.Drawing.Point(12, 162);
            this.lblCodBarPrecio.Name = "lblCodBarPrecio";
            this.lblCodBarPrecio.Size = new System.Drawing.Size(261, 23);
            this.lblCodBarPrecio.TabIndex = 8;
            this.lblCodBarPrecio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CodeBarMake
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 203);
            this.Controls.Add(this.lblCodBarPrecio);
            this.Controls.Add(this.lblNombreProd);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelResultado);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CodeBarMake";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CodeBarMake";
            this.Load += new System.EventHandler(this.CodeBarMake_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelResultado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNombreProd;
        private System.Windows.Forms.Label lblCodBarPrecio;
    }
}