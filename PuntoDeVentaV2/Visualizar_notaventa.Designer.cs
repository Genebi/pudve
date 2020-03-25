namespace PuntoDeVentaV2
{
    partial class Visualizar_notaventa
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Visualizar_notaventa));
            this.axAcroPDFf = new AxAcroPDFLib.AxAcroPDF();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDFf)).BeginInit();
            this.SuspendLayout();
            // 
            // axAcroPDFf
            // 
            this.axAcroPDFf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axAcroPDFf.Enabled = true;
            this.axAcroPDFf.Location = new System.Drawing.Point(12, 20);
            this.axAcroPDFf.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.axAcroPDFf.Name = "axAcroPDFf";
            this.axAcroPDFf.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDFf.OcxState")));
            this.axAcroPDFf.Size = new System.Drawing.Size(725, 545);
            this.axAcroPDFf.TabIndex = 2;
            // 
            // Visualizar_notaventa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 621);
            this.Controls.Add(this.axAcroPDFf);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Visualizar_notaventa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Nota venta";
            this.Load += new System.EventHandler(this.Visualizar_notaventa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDFf)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxAcroPDFLib.AxAcroPDF axAcroPDFf;
    }
}