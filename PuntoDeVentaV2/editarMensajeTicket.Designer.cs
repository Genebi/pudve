namespace PuntoDeVentaV2
{
    partial class editarMensajeTicket
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
            this.txtMensajeTicket = new System.Windows.Forms.TextBox();
            this.btnGuardarMensaje = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtMensajeTicket
            // 
            this.txtMensajeTicket.Location = new System.Drawing.Point(12, 37);
            this.txtMensajeTicket.Multiline = true;
            this.txtMensajeTicket.Name = "txtMensajeTicket";
            this.txtMensajeTicket.Size = new System.Drawing.Size(338, 147);
            this.txtMensajeTicket.TabIndex = 0;
            // 
            // btnGuardarMensaje
            // 
            this.btnGuardarMensaje.Location = new System.Drawing.Point(78, 191);
            this.btnGuardarMensaje.Name = "btnGuardarMensaje";
            this.btnGuardarMensaje.Size = new System.Drawing.Size(213, 32);
            this.btnGuardarMensaje.TabIndex = 1;
            this.btnGuardarMensaje.Text = "Guardar mensaje";
            this.btnGuardarMensaje.UseVisualStyleBackColor = true;
            this.btnGuardarMensaje.Click += new System.EventHandler(this.btnGuardarMensaje_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Editar mensaje a mostrar:";
            // 
            // editarMensajeTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(363, 231);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGuardarMensaje);
            this.Controls.Add(this.txtMensajeTicket);
            this.KeyPreview = true;
            this.Name = "editarMensajeTicket";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "editarMensajeTicket";
            this.Load += new System.EventHandler(this.editarMensajeTicket_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.editarMensajeTicket_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMensajeTicket;
        private System.Windows.Forms.Button btnGuardarMensaje;
        private System.Windows.Forms.Label label1;
    }
}