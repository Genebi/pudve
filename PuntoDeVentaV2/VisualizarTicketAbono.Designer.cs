
namespace PuntoDeVentaV2
{
    partial class VisualizarTicketAbono
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnReImprimirTicket = new PuntoDeVentaV2.BotonRedondo();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
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
            this.panel1.Size = new System.Drawing.Size(800, 381);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnReImprimirTicket);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 381);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(800, 69);
            this.panel2.TabIndex = 1;
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
            this.btnReImprimirTicket.Location = new System.Drawing.Point(325, 14);
            this.btnReImprimirTicket.Name = "btnReImprimirTicket";
            this.btnReImprimirTicket.Size = new System.Drawing.Size(150, 40);
            this.btnReImprimirTicket.TabIndex = 4;
            this.btnReImprimirTicket.Text = "Imprimir";
            this.btnReImprimirTicket.TextColor = System.Drawing.Color.White;
            this.btnReImprimirTicket.UseVisualStyleBackColor = false;
            this.btnReImprimirTicket.Click += new System.EventHandler(this.btnReImprimirTicket_Click);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(800, 381);
            this.reportViewer1.TabIndex = 0;
            // 
            // VisualizarTicketAbono
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "VisualizarTicketAbono";
            this.Text = "VisualizarTicketAbono";
            this.Load += new System.EventHandler(this.VisualizarTicketAbono_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private BotonRedondo btnReImprimirTicket;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
    }
}