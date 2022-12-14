
namespace PuntoDeVentaV2
{
    partial class Ganancia
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
            this.btnAceptar = new System.Windows.Forms.Button();
            this.lblGanancia = new System.Windows.Forms.Label();
            this.lblTextoGanancia = new System.Windows.Forms.Label();
            this.lblMensaje = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(110, 104);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // lblGanancia
            // 
            this.lblGanancia.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGanancia.Location = new System.Drawing.Point(0, 49);
            this.lblGanancia.Name = "lblGanancia";
            this.lblGanancia.Size = new System.Drawing.Size(294, 26);
            this.lblGanancia.TabIndex = 1;
            this.lblGanancia.Text = "GANANCIA";
            this.lblGanancia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTextoGanancia
            // 
            this.lblTextoGanancia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextoGanancia.Location = new System.Drawing.Point(35, 9);
            this.lblTextoGanancia.Name = "lblTextoGanancia";
            this.lblTextoGanancia.Size = new System.Drawing.Size(224, 40);
            this.lblTextoGanancia.TabIndex = 2;
            this.lblTextoGanancia.Text = "Ganancia total por venta:";
            this.lblTextoGanancia.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMensaje
            // 
            this.lblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMensaje.Location = new System.Drawing.Point(27, 75);
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(237, 14);
            this.lblMensaje.TabIndex = 3;
            this.lblMensaje.Text = "(Algun producto no cuenta con precio de compra)";
            this.lblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ganancia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 139);
            this.Controls.Add(this.lblMensaje);
            this.Controls.Add(this.lblTextoGanancia);
            this.Controls.Add(this.lblGanancia);
            this.Controls.Add(this.btnAceptar);
            this.Name = "Ganancia";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ganancia";
            this.Load += new System.EventHandler(this.Ganancia_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label lblGanancia;
        private System.Windows.Forms.Label lblTextoGanancia;
        private System.Windows.Forms.Label lblMensaje;
    }
}