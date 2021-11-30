namespace PuntoDeVentaV2
{
    partial class RangosReporteProductosMenosVendidos
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dtpFin = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpInicio = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtCantidadMostar = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.botonRedondo1 = new PuntoDeVentaV2.BotonRedondo();
            this.botonRedondo2 = new PuntoDeVentaV2.BotonRedondo();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(154, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rangos para la consulta";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dtpFin);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.dtpInicio);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 54);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(486, 68);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Fechas: ";
            // 
            // dtpFin
            // 
            this.dtpFin.Location = new System.Drawing.Point(301, 24);
            this.dtpFin.Name = "dtpFin";
            this.dtpFin.Size = new System.Drawing.Size(175, 20);
            this.dtpFin.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(253, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Final:";
            // 
            // dtpInicio
            // 
            this.dtpInicio.Location = new System.Drawing.Point(58, 24);
            this.dtpInicio.Name = "dtpInicio";
            this.dtpInicio.Size = new System.Drawing.Size(175, 20);
            this.dtpInicio.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Inicial: ";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCantidadMostar);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(486, 54);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            // 
            // txtCantidadMostar
            // 
            this.txtCantidadMostar.Location = new System.Drawing.Point(255, 18);
            this.txtCantidadMostar.Name = "txtCantidadMostar";
            this.txtCantidadMostar.Size = new System.Drawing.Size(110, 20);
            this.txtCantidadMostar.TabIndex = 1;
            this.txtCantidadMostar.Text = "0";
            this.txtCantidadMostar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCantidadMostar.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadMostar_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(82, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(155, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Cantidad de productos:";
            // 
            // botonRedondo1
            // 
            this.botonRedondo1.BackColor = System.Drawing.Color.Red;
            this.botonRedondo1.BackGroundColor = System.Drawing.Color.Red;
            this.botonRedondo1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.botonRedondo1.BorderRadius = 30;
            this.botonRedondo1.BorderSize = 0;
            this.botonRedondo1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.botonRedondo1.FlatAppearance.BorderSize = 0;
            this.botonRedondo1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonRedondo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonRedondo1.ForeColor = System.Drawing.Color.White;
            this.botonRedondo1.Image = global::PuntoDeVentaV2.Properties.Resources.close3;
            this.botonRedondo1.Location = new System.Drawing.Point(79, 191);
            this.botonRedondo1.Name = "botonRedondo1";
            this.botonRedondo1.Size = new System.Drawing.Size(150, 50);
            this.botonRedondo1.TabIndex = 3;
            this.botonRedondo1.Text = "Cancelar";
            this.botonRedondo1.TextColor = System.Drawing.Color.White;
            this.botonRedondo1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonRedondo1.UseVisualStyleBackColor = false;
            this.botonRedondo1.Click += new System.EventHandler(this.botonRedondo1_Click);
            // 
            // botonRedondo2
            // 
            this.botonRedondo2.BackColor = System.Drawing.Color.Blue;
            this.botonRedondo2.BackGroundColor = System.Drawing.Color.Blue;
            this.botonRedondo2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.botonRedondo2.BorderRadius = 30;
            this.botonRedondo2.BorderSize = 0;
            this.botonRedondo2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.botonRedondo2.FlatAppearance.BorderSize = 0;
            this.botonRedondo2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonRedondo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonRedondo2.ForeColor = System.Drawing.Color.White;
            this.botonRedondo2.Image = global::PuntoDeVentaV2.Properties.Resources.check_square1;
            this.botonRedondo2.Location = new System.Drawing.Point(268, 191);
            this.botonRedondo2.Name = "botonRedondo2";
            this.botonRedondo2.Size = new System.Drawing.Size(150, 50);
            this.botonRedondo2.TabIndex = 4;
            this.botonRedondo2.Text = "Aceptar";
            this.botonRedondo2.TextColor = System.Drawing.Color.White;
            this.botonRedondo2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.botonRedondo2.UseVisualStyleBackColor = false;
            this.botonRedondo2.Click += new System.EventHandler(this.botonRedondo2_Click);
            // 
            // RangosReporteProductosMenosVendidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(513, 257);
            this.Controls.Add(this.botonRedondo2);
            this.Controls.Add(this.botonRedondo1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RangosReporteProductosMenosVendidos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Parametros de busqueda del reporte";
            this.Load += new System.EventHandler(this.RangosReporteProductosMenosVendidos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dtpFin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtpInicio;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCantidadMostar;
        private BotonRedondo botonRedondo1;
        private BotonRedondo botonRedondo2;
    }
}