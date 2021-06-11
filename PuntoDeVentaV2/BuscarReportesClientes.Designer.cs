namespace PuntoDeVentaV2
{
    partial class BuscarReportesClientes
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
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dtpSegundaFecha = new System.Windows.Forms.DateTimePicker();
            this.dtpPrimerFecha = new System.Windows.Forms.DateTimePicker();
            this.btnBuscar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(12, 78);
            this.txtBuscar.Multiline = true;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(406, 23);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyDown);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 119);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(768, 268);
            this.dataGridView1.TabIndex = 1;
            // 
            // dtpSegundaFecha
            // 
            this.dtpSegundaFecha.CalendarFont = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSegundaFecha.CustomFormat = "yyyy-MM-dd";
            this.dtpSegundaFecha.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSegundaFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpSegundaFecha.Location = new System.Drawing.Point(565, 78);
            this.dtpSegundaFecha.Name = "dtpSegundaFecha";
            this.dtpSegundaFecha.Size = new System.Drawing.Size(114, 23);
            this.dtpSegundaFecha.TabIndex = 9;
            // 
            // dtpPrimerFecha
            // 
            this.dtpPrimerFecha.CalendarFont = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPrimerFecha.CustomFormat = "yyyy-MM-dd";
            this.dtpPrimerFecha.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpPrimerFecha.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpPrimerFecha.Location = new System.Drawing.Point(433, 78);
            this.dtpPrimerFecha.Name = "dtpPrimerFecha";
            this.dtpPrimerFecha.Size = new System.Drawing.Size(114, 23);
            this.dtpPrimerFecha.TabIndex = 10;
            // 
            // btnBuscar
            // 
            this.btnBuscar.AutoSize = true;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.Image = global::PuntoDeVentaV2.Properties.Resources.search;
            this.btnBuscar.Location = new System.Drawing.Point(705, 78);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(75, 23);
            this.btnBuscar.TabIndex = 11;
            this.btnBuscar.UseVisualStyleBackColor = true;
            // 
            // BuscarReportesClientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 419);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.dtpPrimerFecha);
            this.Controls.Add(this.dtpSegundaFecha);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtBuscar);
            this.Name = "BuscarReportesClientes";
            this.Text = "BuscarReportesClientes";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DateTimePicker dtpSegundaFecha;
        private System.Windows.Forms.DateTimePicker dtpPrimerFecha;
        private System.Windows.Forms.Button btnBuscar;
    }
}