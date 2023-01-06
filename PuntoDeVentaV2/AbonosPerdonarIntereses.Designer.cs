
namespace PuntoDeVentaV2
{
    partial class AbonosPerdonarIntereses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AbonosPerdonarIntereses));
            this.lblintereses = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            this.SuspendLayout();
            // 
            // lblintereses
            // 
            this.lblintereses.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lblintereses.Font = new System.Drawing.Font("Century Gothic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblintereses.Location = new System.Drawing.Point(36, 45);
            this.lblintereses.Name = "lblintereses";
            this.lblintereses.Size = new System.Drawing.Size(92, 23);
            this.lblintereses.TabIndex = 147;
            this.lblintereses.Text = "$0.00";
            this.lblintereses.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 22);
            this.label1.TabIndex = 146;
            this.label1.Text = "Intereses";
            // 
            // numCantidad
            // 
            this.numCantidad.DecimalPlaces = 2;
            this.numCantidad.Location = new System.Drawing.Point(73, 83);
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(83, 20);
            this.numCantidad.TabIndex = 148;
            this.numCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numCantidad.ThousandsSeparator = true;
            this.numCantidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numCantidad_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 149;
            this.label2.Text = "Perdonar:";
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(73, 82);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 23);
            this.label11.TabIndex = 150;
            this.label11.Text = "$";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AbonosPerdonarIntereses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(166, 110);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numCantidad);
            this.Controls.Add(this.lblintereses);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AbonosPerdonarIntereses";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Perdonar interes";
            this.Load += new System.EventHandler(this.AbonosPerdonarIntereses_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AbonosPerdonarIntereses_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblintereses;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
    }
}