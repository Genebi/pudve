
namespace PuntoDeVentaV2
{
    partial class VerTicket80mmListadoVentas
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnReImprimirTicket = new PuntoDeVentaV2.BotonRedondo();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Location = new System.Drawing.Point(12, 12);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(633, 382);
            this.reportViewer1.TabIndex = 2;
            // 
            // btnReImprimirTicket
            // 
            this.btnReImprimirTicket.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnReImprimirTicket.BackGroundColor = System.Drawing.Color.MediumSlateBlue;
            this.btnReImprimirTicket.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnReImprimirTicket.BorderRadius = 20;
            this.btnReImprimirTicket.BorderSize = 0;
            this.btnReImprimirTicket.FlatAppearance.BorderSize = 0;
            this.btnReImprimirTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReImprimirTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReImprimirTicket.ForeColor = System.Drawing.Color.White;
            this.btnReImprimirTicket.Location = new System.Drawing.Point(495, 400);
            this.btnReImprimirTicket.Name = "btnReImprimirTicket";
            this.btnReImprimirTicket.Size = new System.Drawing.Size(150, 40);
            this.btnReImprimirTicket.TabIndex = 3;
            this.btnReImprimirTicket.Text = "Imprimir";
            this.btnReImprimirTicket.TextColor = System.Drawing.Color.White;
            this.btnReImprimirTicket.UseVisualStyleBackColor = false;
            this.btnReImprimirTicket.Click += new System.EventHandler(this.btnReImprimirTicket_Click);
            // 
            // VerTicket80mmListadoVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 450);
            this.Controls.Add(this.btnReImprimirTicket);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VerTicket80mmListadoVentas";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VerTicket80mmListadoVentas";
            this.Load += new System.EventHandler(this.VerTicket80mmListadoVentas_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BotonRedondo btnReImprimirTicket;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}