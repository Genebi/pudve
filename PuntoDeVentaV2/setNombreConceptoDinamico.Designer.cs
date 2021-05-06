namespace PuntoDeVentaV2
{
    partial class setNombreConceptoDinamico
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtConceptoActual = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtConceptoNuevo = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtConceptoNuevo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtConceptoActual);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(467, 148);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Concepto: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = " Actual: ";
            // 
            // txtConceptoActual
            // 
            this.txtConceptoActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConceptoActual.Location = new System.Drawing.Point(25, 44);
            this.txtConceptoActual.Name = "txtConceptoActual";
            this.txtConceptoActual.ReadOnly = true;
            this.txtConceptoActual.Size = new System.Drawing.Size(411, 26);
            this.txtConceptoActual.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = " Nuevo: ";
            // 
            // txtConceptoNuevo
            // 
            this.txtConceptoNuevo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConceptoNuevo.Location = new System.Drawing.Point(25, 99);
            this.txtConceptoNuevo.Name = "txtConceptoNuevo";
            this.txtConceptoNuevo.Size = new System.Drawing.Size(411, 26);
            this.txtConceptoNuevo.TabIndex = 3;
            this.txtConceptoNuevo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConceptoNuevo_KeyDown);
            // 
            // setNombreConceptoDinamico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 185);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "setNombreConceptoDinamico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambiar Nombre";
            this.Load += new System.EventHandler(this.setNombreConceptoDinamico_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtConceptoActual;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtConceptoNuevo;
        private System.Windows.Forms.Label label2;
    }
}