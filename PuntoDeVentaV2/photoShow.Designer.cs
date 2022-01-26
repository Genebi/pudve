namespace PuntoDeVentaV2
{
    partial class photoShow
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
            this.pictureBoxProducto = new System.Windows.Forms.PictureBox();
            this.lblNombreProducto = new System.Windows.Forms.Label();
            this.btnImagen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProducto)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxProducto
            // 
            this.pictureBoxProducto.Location = new System.Drawing.Point(20, 53);
            this.pictureBoxProducto.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxProducto.Name = "pictureBoxProducto";
            this.pictureBoxProducto.Size = new System.Drawing.Size(319, 269);
            this.pictureBoxProducto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxProducto.TabIndex = 0;
            this.pictureBoxProducto.TabStop = false;
            // 
            // lblNombreProducto
            // 
            this.lblNombreProducto.Location = new System.Drawing.Point(20, 17);
            this.lblNombreProducto.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNombreProducto.Name = "lblNombreProducto";
            this.lblNombreProducto.Size = new System.Drawing.Size(319, 19);
            this.lblNombreProducto.TabIndex = 1;
            this.lblNombreProducto.Text = "label1";
            this.lblNombreProducto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnImagen
            // 
            this.btnImagen.BackColor = System.Drawing.Color.Green;
            this.btnImagen.FlatAppearance.BorderSize = 0;
            this.btnImagen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImagen.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImagen.ForeColor = System.Drawing.Color.White;
            this.btnImagen.Location = new System.Drawing.Point(110, 331);
            this.btnImagen.Name = "btnImagen";
            this.btnImagen.Size = new System.Drawing.Size(139, 41);
            this.btnImagen.TabIndex = 2;
            this.btnImagen.Text = "Borrar imagen";
            this.btnImagen.UseVisualStyleBackColor = false;
            this.btnImagen.Click += new System.EventHandler(this.btnImagen_Click);
            // 
            // photoShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 380);
            this.Controls.Add(this.btnImagen);
            this.Controls.Add(this.lblNombreProducto);
            this.Controls.Add(this.pictureBoxProducto);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "photoShow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Imagen  de Producto";
            this.Load += new System.EventHandler(this.photoShow_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.photoShow_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProducto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxProducto;
        private System.Windows.Forms.Label lblNombreProducto;
        private System.Windows.Forms.Button btnImagen;
    }
}