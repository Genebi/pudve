
namespace PuntoDeVentaV2
{
    partial class PreguntasDevolucionProductos
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
            this.label1 = new System.Windows.Forms.Label();
            this.rbDevolverDinero = new System.Windows.Forms.RadioButton();
            this.rbGenerarTicket = new System.Windows.Forms.RadioButton();
            this.rbNada = new System.Windows.Forms.RadioButton();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(342, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "Se automentara el stock del producto devuelto,  \r\nademas de eso desea hacer algun" +
    "as de las\r\n de las siguientes operaciones..\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rbDevolverDinero
            // 
            this.rbDevolverDinero.AutoSize = true;
            this.rbDevolverDinero.Location = new System.Drawing.Point(136, 76);
            this.rbDevolverDinero.Name = "rbDevolverDinero";
            this.rbDevolverDinero.Size = new System.Drawing.Size(128, 17);
            this.rbDevolverDinero.TabIndex = 1;
            this.rbDevolverDinero.TabStop = true;
            this.rbDevolverDinero.Text = "DEVOLVER DINERO";
            this.rbDevolverDinero.UseVisualStyleBackColor = true;
            // 
            // rbGenerarTicket
            // 
            this.rbGenerarTicket.AutoSize = true;
            this.rbGenerarTicket.Location = new System.Drawing.Point(106, 110);
            this.rbGenerarTicket.Name = "rbGenerarTicket";
            this.rbGenerarTicket.Size = new System.Drawing.Size(183, 17);
            this.rbGenerarTicket.TabIndex = 2;
            this.rbGenerarTicket.TabStop = true;
            this.rbGenerarTicket.Text = "GENERAR TIKET DE ANTICIPO";
            this.rbGenerarTicket.UseVisualStyleBackColor = true;
            // 
            // rbNada
            // 
            this.rbNada.AutoSize = true;
            this.rbNada.Location = new System.Drawing.Point(103, 143);
            this.rbNada.Name = "rbNada";
            this.rbNada.Size = new System.Drawing.Size(189, 17);
            this.rbNada.TabIndex = 3;
            this.rbNada.TabStop = true;
            this.rbNada.Text = "NINGUNA DE LAS ANTERIORES";
            this.rbNada.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.BackColor = System.Drawing.Color.Green;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(156, 169);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(87, 29);
            this.btnAceptar.TabIndex = 67;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // PreguntasDevolucionProductos
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 210);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.rbNada);
            this.Controls.Add(this.rbGenerarTicket);
            this.Controls.Add(this.rbDevolverDinero);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PreguntasDevolucionProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PreguntasDevolucionProductos";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbDevolverDinero;
        private System.Windows.Forms.RadioButton rbGenerarTicket;
        private System.Windows.Forms.RadioButton rbNada;
        private System.Windows.Forms.Button btnAceptar;
    }
}