namespace PuntoDeVentaV2
{
    partial class Visualizar_factura
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Visualizar_factura));
            this.axAcroPDFf = new AxAcroPDFLib.AxAcroPDF();
            this.btn_imprimir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDFf)).BeginInit();
            this.SuspendLayout();
            // 
            // axAcroPDFf
            // 
            this.axAcroPDFf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axAcroPDFf.Enabled = true;
            this.axAcroPDFf.Location = new System.Drawing.Point(12, 19);
            this.axAcroPDFf.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.axAcroPDFf.Name = "axAcroPDFf";
            this.axAcroPDFf.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDFf.OcxState")));
            this.axAcroPDFf.Size = new System.Drawing.Size(725, 545);
            this.axAcroPDFf.TabIndex = 1;
            // 
            // btn_imprimir
            // 
            this.btn_imprimir.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_imprimir.BackColor = System.Drawing.Color.White;
            this.btn_imprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_imprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_imprimir.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_imprimir.ForeColor = System.Drawing.Color.Black;
            this.btn_imprimir.Image = global::PuntoDeVentaV2.Properties.Resources.print;
            this.btn_imprimir.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_imprimir.Location = new System.Drawing.Point(307, 583);
            this.btn_imprimir.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_imprimir.Name = "btn_imprimir";
            this.btn_imprimir.Padding = new System.Windows.Forms.Padding(10, 1, 10, 1);
            this.btn_imprimir.Size = new System.Drawing.Size(120, 35);
            this.btn_imprimir.TabIndex = 2;
            this.btn_imprimir.Text = "Imprimir";
            this.btn_imprimir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btn_imprimir.UseVisualStyleBackColor = false;
            this.btn_imprimir.Click += new System.EventHandler(this.btn_imprimir_Click);
            // 
            // Visualizar_factura
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 631);
            this.Controls.Add(this.btn_imprimir);
            this.Controls.Add(this.axAcroPDFf);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Visualizar_factura";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Factura";
            this.Load += new System.EventHandler(this.Visualizar_factura_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDFf)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxAcroPDFLib.AxAcroPDF axAcroPDFf;
        private System.Windows.Forms.Button btn_imprimir;
    }
}