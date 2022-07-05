
namespace PuntoDeVentaV2
{
    partial class VerTicketCajaAbierta8cmListadoVentas
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnImprimirTicketAbrirCaja = new PuntoDeVentaV2.BotonRedondo();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.reportViewer1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(661, 391);
            this.panel1.TabIndex = 0;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(661, 391);
            this.reportViewer1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnImprimirTicketAbrirCaja);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 391);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(661, 75);
            this.panel2.TabIndex = 1;
            // 
            // btnImprimirTicketAbrirCaja
            // 
            this.btnImprimirTicketAbrirCaja.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.btnImprimirTicketAbrirCaja.BackGroundColor = System.Drawing.Color.MediumSlateBlue;
            this.btnImprimirTicketAbrirCaja.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnImprimirTicketAbrirCaja.BorderRadius = 20;
            this.btnImprimirTicketAbrirCaja.BorderSize = 0;
            this.btnImprimirTicketAbrirCaja.FlatAppearance.BorderSize = 0;
            this.btnImprimirTicketAbrirCaja.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirTicketAbrirCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirTicketAbrirCaja.ForeColor = System.Drawing.Color.White;
            this.btnImprimirTicketAbrirCaja.Location = new System.Drawing.Point(488, 17);
            this.btnImprimirTicketAbrirCaja.Name = "btnImprimirTicketAbrirCaja";
            this.btnImprimirTicketAbrirCaja.Size = new System.Drawing.Size(150, 40);
            this.btnImprimirTicketAbrirCaja.TabIndex = 0;
            this.btnImprimirTicketAbrirCaja.Text = "Imprimir";
            this.btnImprimirTicketAbrirCaja.TextColor = System.Drawing.Color.White;
            this.btnImprimirTicketAbrirCaja.UseVisualStyleBackColor = false;
            this.btnImprimirTicketAbrirCaja.Click += new System.EventHandler(this.btnImprimirTicketAbrirCaja_Click);
            // 
            // VerTicketCajaAbierta8cmListadoVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(661, 466);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VerTicketCajaAbierta8cmListadoVentas";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VerTicketCajaAbierta8cmListadoVentas";
            this.Load += new System.EventHandler(this.VerTicketCajaAbierta8cmListadoVentas_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private BotonRedondo btnImprimirTicketAbrirCaja;
    }
}