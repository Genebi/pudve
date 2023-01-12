
namespace PuntoDeVentaV2
{
    partial class categoriaSubdetalle
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtSubDetalle = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.cbTipoDeDatos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.txtSubDetalle);
            this.panel3.Location = new System.Drawing.Point(12, 11);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(351, 51);
            this.panel3.TabIndex = 5;
            // 
            // txtSubDetalle
            // 
            this.txtSubDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubDetalle.Location = new System.Drawing.Point(7, 11);
            this.txtSubDetalle.Name = "txtSubDetalle";
            this.txtSubDetalle.Size = new System.Drawing.Size(329, 26);
            this.txtSubDetalle.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Controls.Add(this.btnAceptar);
            this.panel2.Location = new System.Drawing.Point(11, 112);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(351, 60);
            this.panel2.TabIndex = 4;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.DarkRed;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Image = global::PuntoDeVentaV2.Properties.Resources.window_close_o1;
            this.btnCancelar.Location = new System.Drawing.Point(9, 7);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(157, 41);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Image = global::PuntoDeVentaV2.Properties.Resources.check_square_o1;
            this.btnAceptar.Location = new System.Drawing.Point(181, 7);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(157, 41);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // cbTipoDeDatos
            // 
            this.cbTipoDeDatos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoDeDatos.FormattingEnabled = true;
            this.cbTipoDeDatos.Items.AddRange(new object[] {
            "Fecha",
            "Numerico",
            "Texto"});
            this.cbTipoDeDatos.Location = new System.Drawing.Point(12, 85);
            this.cbTipoDeDatos.Name = "cbTipoDeDatos";
            this.cbTipoDeDatos.Size = new System.Drawing.Size(183, 21);
            this.cbTipoDeDatos.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Formato:";
            // 
            // categoriaSubdetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 179);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbTipoDeDatos);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "categoriaSubdetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "categoriaSubdetalle";
            this.Load += new System.EventHandler(this.categoriaSubdetalle_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtSubDetalle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.ComboBox cbTipoDeDatos;
        private System.Windows.Forms.Label label1;
    }
}