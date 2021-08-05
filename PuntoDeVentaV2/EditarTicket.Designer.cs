namespace PuntoDeVentaV2
{
    partial class EditarTicket
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btnEditarMensaje = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.fLPCentralDetalle = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.btnEditarMensaje);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 489);
            this.panel1.TabIndex = 28;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(260, 426);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 7;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // btnEditarMensaje
            // 
            this.btnEditarMensaje.Location = new System.Drawing.Point(120, 420);
            this.btnEditarMensaje.Name = "btnEditarMensaje";
            this.btnEditarMensaje.Size = new System.Drawing.Size(122, 23);
            this.btnEditarMensaje.TabIndex = 6;
            this.btnEditarMensaje.Text = "Editar mensaje";
            this.btnEditarMensaje.UseVisualStyleBackColor = true;
            this.btnEditarMensaje.Click += new System.EventHandler(this.button2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 424);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "Editar mesaje ticket";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 451);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(283, 35);
            this.button1.TabIndex = 4;
            this.button1.Text = "Guardar Ticket";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Image = global::PuntoDeVentaV2.Properties.Resources.eye;
            this.label6.Location = new System.Drawing.Point(257, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 23);
            this.label6.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(147, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "Mostrados\r\n(Datos a imprimir)\r\n ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Conceptos del ticket";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.fLPCentralDetalle);
            this.panel2.Location = new System.Drawing.Point(308, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(559, 489);
            this.panel2.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(210, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Vista previa de ticket";
            // 
            // fLPCentralDetalle
            // 
            this.fLPCentralDetalle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.fLPCentralDetalle.AutoScroll = true;
            this.fLPCentralDetalle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fLPCentralDetalle.Location = new System.Drawing.Point(5, 44);
            this.fLPCentralDetalle.Name = "fLPCentralDetalle";
            this.fLPCentralDetalle.Size = new System.Drawing.Size(551, 445);
            this.fLPCentralDetalle.TabIndex = 0;
            // 
            // EditarTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 495);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "EditarTicket";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditarTicket";
            this.Load += new System.EventHandler(this.EditarTicket_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.FlowLayoutPanel fLPCentralDetalle;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button btnEditarMensaje;
        private System.Windows.Forms.Label label4;
    }
}