namespace PuntoDeVentaV2
{
    partial class ReporteDineroAgregado
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.DGVDepositos = new System.Windows.Forms.DataGridView();
            this.Empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Efectivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tarjeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cheque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Trans = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnImprimirReporte = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVDepositos)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVDepositos
            // 
            this.DGVDepositos.AllowUserToAddRows = false;
            this.DGVDepositos.AllowUserToDeleteRows = false;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVDepositos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.DGVDepositos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVDepositos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Empleado,
            this.Efectivo,
            this.Tarjeta,
            this.Vales,
            this.Cheque,
            this.Trans,
            this.Fecha});
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVDepositos.DefaultCellStyle = dataGridViewCellStyle14;
            this.DGVDepositos.Location = new System.Drawing.Point(3, 3);
            this.DGVDepositos.Name = "DGVDepositos";
            this.DGVDepositos.ReadOnly = true;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVDepositos.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.DGVDepositos.RowHeadersVisible = false;
            this.DGVDepositos.Size = new System.Drawing.Size(729, 222);
            this.DGVDepositos.TabIndex = 0;
            // 
            // Empleado
            // 
            this.Empleado.HeaderText = "Empleado";
            this.Empleado.Name = "Empleado";
            this.Empleado.ReadOnly = true;
            this.Empleado.Width = 140;
            // 
            // Efectivo
            // 
            this.Efectivo.HeaderText = "Efectivo";
            this.Efectivo.Name = "Efectivo";
            this.Efectivo.ReadOnly = true;
            this.Efectivo.Width = 90;
            // 
            // Tarjeta
            // 
            this.Tarjeta.HeaderText = "Tarjeta";
            this.Tarjeta.Name = "Tarjeta";
            this.Tarjeta.ReadOnly = true;
            this.Tarjeta.Width = 90;
            // 
            // Vales
            // 
            this.Vales.HeaderText = "Vales";
            this.Vales.Name = "Vales";
            this.Vales.ReadOnly = true;
            this.Vales.Width = 90;
            // 
            // Cheque
            // 
            this.Cheque.HeaderText = "Cheque";
            this.Cheque.Name = "Cheque";
            this.Cheque.ReadOnly = true;
            this.Cheque.Width = 90;
            // 
            // Trans
            // 
            this.Trans.HeaderText = "Transferencia";
            this.Trans.Name = "Trans";
            this.Trans.ReadOnly = true;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 125;
            // 
            // btnImprimirReporte
            // 
            this.btnImprimirReporte.BackColor = System.Drawing.Color.Green;
            this.btnImprimirReporte.FlatAppearance.BorderSize = 0;
            this.btnImprimirReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimirReporte.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimirReporte.ForeColor = System.Drawing.Color.White;
            this.btnImprimirReporte.Image = global::PuntoDeVentaV2.Properties.Resources.print;
            this.btnImprimirReporte.Location = new System.Drawing.Point(322, 234);
            this.btnImprimirReporte.Name = "btnImprimirReporte";
            this.btnImprimirReporte.Size = new System.Drawing.Size(91, 31);
            this.btnImprimirReporte.TabIndex = 1;
            this.btnImprimirReporte.Text = "Imprimir";
            this.btnImprimirReporte.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnImprimirReporte.UseVisualStyleBackColor = false;
            // 
            // ReporteDineroAgregado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 276);
            this.Controls.Add(this.btnImprimirReporte);
            this.Controls.Add(this.DGVDepositos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ReporteDineroAgregado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Historial Dinero Agregado";
            this.Load += new System.EventHandler(this.ReporteDineroAgregado_Load);
            this.Shown += new System.EventHandler(this.ReporteDineroAgregado_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVDepositos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVDepositos;
        private System.Windows.Forms.DataGridViewTextBoxColumn Empleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Efectivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tarjeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vales;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cheque;
        private System.Windows.Forms.DataGridViewTextBoxColumn Trans;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.Button btnImprimirReporte;
    }
}