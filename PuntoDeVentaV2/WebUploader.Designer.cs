
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
            this.btnCerrar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // PBProgreso
            // 
            this.PBProgreso.Location = new System.Drawing.Point(15, 11);
            this.PBProgreso.Margin = new System.Windows.Forms.Padding(2);
            this.PBProgreso.Name = "PBProgreso";
            this.PBProgreso.Size = new System.Drawing.Size(381, 38);
            this.PBProgreso.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.PBProgreso.TabIndex = 0;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.Red;
            this.btnCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.Location = new System.Drawing.Point(15, 56);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(381, 55);
            this.btnCerrar.TabIndex = 1;
            this.btnCerrar.Text = "Cancelar de manera segura";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // WebUploader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCerrar;
            this.ClientSize = new System.Drawing.Size(407, 123);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.PBProgreso);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WebUploader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Respaldo a la nube en curso... Use \"Esc\" para cancelar";
            this.Load += new System.EventHandler(this.WebUploader_Load);
            this.Shown += new System.EventHandler(this.WebUploader_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.WebUploader_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar PBProgreso;
        private System.Windows.Forms.Button btnCerrar;
    }
}