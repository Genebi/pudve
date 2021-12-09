namespace PuntoDeVentaV2
{
    partial class MensajePorFavorEspere
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
            this.components = new System.ComponentModel.Container();
            this.pbAvanceAsignacion = new System.Windows.Forms.ProgressBar();
            this.lblAsigandoConcepto = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // pbAvanceAsignacion
            // 
            this.pbAvanceAsignacion.Location = new System.Drawing.Point(12, 41);
            this.pbAvanceAsignacion.Name = "pbAvanceAsignacion";
            this.pbAvanceAsignacion.Size = new System.Drawing.Size(412, 32);
            this.pbAvanceAsignacion.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbAvanceAsignacion.TabIndex = 0;
            this.pbAvanceAsignacion.ParentChanged += new System.EventHandler(this.pbAvanceAsignacion_ParentChanged);
            // 
            // lblAsigandoConcepto
            // 
            this.lblAsigandoConcepto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAsigandoConcepto.Location = new System.Drawing.Point(12, 9);
            this.lblAsigandoConcepto.Name = "lblAsigandoConcepto";
            this.lblAsigandoConcepto.Size = new System.Drawing.Size(412, 23);
            this.lblAsigandoConcepto.TabIndex = 1;
            this.lblAsigandoConcepto.Text = "Asginando Concepto";
            this.lblAsigandoConcepto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MensajePorFavorEspere
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 85);
            this.Controls.Add(this.lblAsigandoConcepto);
            this.Controls.Add(this.pbAvanceAsignacion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MensajePorFavorEspere";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MensajePorFavorEspere";
            this.Load += new System.EventHandler(this.MensajePorFavorEspere_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar pbAvanceAsignacion;
        private System.Windows.Forms.Label lblAsigandoConcepto;
        private System.Windows.Forms.Timer timer1;
    }
}