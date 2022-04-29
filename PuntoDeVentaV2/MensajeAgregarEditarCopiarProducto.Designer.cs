
namespace PuntoDeVentaV2
{
    partial class MensajeAgregarEditarCopiarProducto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MensajeAgregarEditarCopiarProducto));
            this.lblMensaje = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pbImagen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMensaje
            // 
            this.lblMensaje.AutoSize = true;
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(67, 50);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(112, 16);
            this.lblMensaje.TabIndex = 1;
            this.lblMensaje.Text = "Tengo Hambre";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(132)))), ((int)(((byte)(49)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(265, 93);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 31);
            this.button1.TabIndex = 2;
            this.button1.Text = "Aceptar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pbImagen
            // 
            this.pbImagen.Image = ((System.Drawing.Image)(resources.GetObject("pbImagen.Image")));
            this.pbImagen.Location = new System.Drawing.Point(26, 41);
            this.pbImagen.Name = "pbImagen";
            this.pbImagen.Size = new System.Drawing.Size(35, 35);
            this.pbImagen.TabIndex = 3;
            this.pbImagen.TabStop = false;
            // 
            // MensajeAgregarEditarCopiarProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(376, 134);
            this.Controls.Add(this.pbImagen);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblMensaje);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MensajeAgregarEditarCopiarProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.MensajeAgregarEditarCopiarProducto_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MensajeAgregarEditarCopiarProducto_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbImagen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblMensaje;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pbImagen;
    }
}