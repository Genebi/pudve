namespace PuntoDeVentaV2
{
    partial class Loading
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
            this.picbox_loading = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_loading)).BeginInit();
            this.SuspendLayout();
            // 
            // picbox_loading
            // 
            this.picbox_loading.Location = new System.Drawing.Point(12, 12);
            this.picbox_loading.Name = "picbox_loading";
            this.picbox_loading.Size = new System.Drawing.Size(310, 287);
            this.picbox_loading.TabIndex = 0;
            this.picbox_loading.TabStop = false;
            // 
            // Loading
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(334, 311);
            this.Controls.Add(this.picbox_loading);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Loading";
            this.Opacity = 0.7D;
            this.Text = "Loading";
            this.Load += new System.EventHandler(this.Loading_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picbox_loading)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picbox_loading;
    }
}