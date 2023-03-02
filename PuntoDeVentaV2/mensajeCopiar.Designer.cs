
namespace PuntoDeVentaV2
{
    partial class mensajeCopiar
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
            this.messageLabel = new System.Windows.Forms.Label();
            this.textToCopyLabel = new System.Windows.Forms.Label();
            this.copyButton = new PuntoDeVentaV2.BotonRedondo();
            this.button2 = new PuntoDeVentaV2.BotonRedondo();
            this.SuspendLayout();
            // 
            // messageLabel
            // 
            this.messageLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.messageLabel.Location = new System.Drawing.Point(12, 9);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(352, 25);
            this.messageLabel.TabIndex = 0;
            this.messageLabel.Text = "Texto de ejemplo";
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // textToCopyLabel
            // 
            this.textToCopyLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textToCopyLabel.Location = new System.Drawing.Point(17, 51);
            this.textToCopyLabel.Name = "textToCopyLabel";
            this.textToCopyLabel.Size = new System.Drawing.Size(347, 62);
            this.textToCopyLabel.TabIndex = 1;
            this.textToCopyLabel.Text = "label1";
            this.textToCopyLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // copyButton
            // 
            this.copyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.copyButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.copyButton.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.copyButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.copyButton.BorderRadius = 20;
            this.copyButton.BorderSize = 0;
            this.copyButton.FlatAppearance.BorderSize = 0;
            this.copyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.copyButton.ForeColor = System.Drawing.Color.White;
            this.copyButton.Image = global::PuntoDeVentaV2.Properties.Resources.clipboard1;
            this.copyButton.Location = new System.Drawing.Point(17, 145);
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(143, 53);
            this.copyButton.TabIndex = 114;
            this.copyButton.Text = "Copiar";
            this.copyButton.TextColor = System.Drawing.Color.White;
            this.copyButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.copyButton.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.BackColor = System.Drawing.Color.ForestGreen;
            this.button2.BackGroundColor = System.Drawing.Color.ForestGreen;
            this.button2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.button2.BorderRadius = 20;
            this.button2.BorderSize = 0;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(221, 145);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 53);
            this.button2.TabIndex = 115;
            this.button2.Text = "Cerrar";
            this.button2.TextColor = System.Drawing.Color.White;
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // mensajeCopiar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 210);
            this.ControlBox = false;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.copyButton);
            this.Controls.Add(this.textToCopyLabel);
            this.Controls.Add(this.messageLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "mensajeCopiar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "mensajeCopiar";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Label textToCopyLabel;
        private BotonRedondo copyButton;
        private BotonRedondo button2;
    }
}