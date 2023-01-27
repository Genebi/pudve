
namespace PuntoDeVentaV2
{
    partial class WebUploader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebUploader));
            this.PBProgreso = new System.Windows.Forms.ProgressBar();
            this.lblprogreso = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // PBProgreso
            // 
            this.PBProgreso.Location = new System.Drawing.Point(15, 66);
            this.PBProgreso.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PBProgreso.Name = "PBProgreso";
            this.PBProgreso.Size = new System.Drawing.Size(545, 38);
            this.PBProgreso.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.PBProgreso.TabIndex = 0;
            // 
            // lblprogreso
            // 
            this.lblprogreso.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblprogreso.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblprogreso.Location = new System.Drawing.Point(15, 12);
            this.lblprogreso.Multiline = true;
            this.lblprogreso.Name = "lblprogreso";
            this.lblprogreso.ReadOnly = true;
            this.lblprogreso.Size = new System.Drawing.Size(210, 35);
            this.lblprogreso.TabIndex = 1;
            this.lblprogreso.Text = "Realizando respaldo";
            this.lblprogreso.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // WebUploader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 118);
            this.ControlBox = false;
            this.Controls.Add(this.lblprogreso);
            this.Controls.Add(this.PBProgreso);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "WebUploader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subida de datos en curso";
            this.Load += new System.EventHandler(this.WebUploader_Load);
            this.Shown += new System.EventHandler(this.WebUploader_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar PBProgreso;
        private System.Windows.Forms.TextBox lblprogreso;
    }
}