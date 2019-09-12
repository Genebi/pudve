namespace PuntoDeVentaV2
{
    partial class TestModificacion
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
            this.PImagen = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBoxProducto = new System.Windows.Forms.PictureBox();
            this.btnImagenes = new System.Windows.Forms.Button();
            this.PImagen.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProducto)).BeginInit();
            this.SuspendLayout();
            // 
            // PImagen
            // 
            this.PImagen.Controls.Add(this.groupBox1);
            this.PImagen.Location = new System.Drawing.Point(159, 51);
            this.PImagen.Margin = new System.Windows.Forms.Padding(2);
            this.PImagen.Name = "PImagen";
            this.PImagen.Size = new System.Drawing.Size(149, 186);
            this.PImagen.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBoxProducto);
            this.groupBox1.Controls.Add(this.btnImagenes);
            this.groupBox1.Location = new System.Drawing.Point(9, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(128, 168);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Imagen";
            // 
            // pictureBoxProducto
            // 
            this.pictureBoxProducto.Location = new System.Drawing.Point(14, 21);
            this.pictureBoxProducto.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxProducto.Name = "pictureBoxProducto";
            this.pictureBoxProducto.Size = new System.Drawing.Size(100, 86);
            this.pictureBoxProducto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxProducto.TabIndex = 0;
            this.pictureBoxProducto.TabStop = false;
            // 
            // btnImagenes
            // 
            this.btnImagenes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImagenes.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImagenes.Location = new System.Drawing.Point(14, 113);
            this.btnImagenes.Name = "btnImagenes";
            this.btnImagenes.Size = new System.Drawing.Size(100, 46);
            this.btnImagenes.TabIndex = 10;
            this.btnImagenes.Text = "Seleccionar imagen(es)";
            this.btnImagenes.UseVisualStyleBackColor = true;
            // 
            // TestModificacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 323);
            this.Controls.Add(this.PImagen);
            this.Name = "TestModificacion";
            this.Text = "TestModificacion";
            this.PImagen.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxProducto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PImagen;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBoxProducto;
        private System.Windows.Forms.Button btnImagenes;
    }
}