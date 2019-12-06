namespace PuntoDeVentaV2
{
    partial class ImprimirEtiqueta
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
            this.panelEtiqueta = new System.Windows.Forms.Panel();
            this.cbPlantillas = new System.Windows.Forms.ComboBox();
            this.btnImprimir = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelEtiqueta
            // 
            this.panelEtiqueta.BackColor = System.Drawing.SystemColors.Window;
            this.panelEtiqueta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEtiqueta.Location = new System.Drawing.Point(77, 7);
            this.panelEtiqueta.Name = "panelEtiqueta";
            this.panelEtiqueta.Size = new System.Drawing.Size(280, 200);
            this.panelEtiqueta.TabIndex = 2;
            // 
            // cbPlantillas
            // 
            this.cbPlantillas.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPlantillas.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPlantillas.FormattingEnabled = true;
            this.cbPlantillas.Location = new System.Drawing.Point(77, 225);
            this.cbPlantillas.Name = "cbPlantillas";
            this.cbPlantillas.Size = new System.Drawing.Size(280, 25);
            this.cbPlantillas.TabIndex = 3;
            this.cbPlantillas.SelectedIndexChanged += new System.EventHandler(this.cbPlantillas_SelectedIndexChanged);
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnImprimir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Location = new System.Drawing.Point(77, 262);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(280, 25);
            this.btnImprimir.TabIndex = 5;
            this.btnImprimir.Text = "Imprimir Plantilla";
            this.btnImprimir.UseVisualStyleBackColor = false;
            // 
            // ImprimirEtiqueta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 311);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.cbPlantillas);
            this.Controls.Add(this.panelEtiqueta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImprimirEtiqueta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Imprimir plantilla";
            this.Load += new System.EventHandler(this.ImprimirEtiqueta_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelEtiqueta;
        private System.Windows.Forms.ComboBox cbPlantillas;
        private System.Windows.Forms.Button btnImprimir;
    }
}