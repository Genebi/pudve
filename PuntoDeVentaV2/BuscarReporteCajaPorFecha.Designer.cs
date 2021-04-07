namespace PuntoDeVentaV2
{
    partial class BuscarReporteCajaPorFecha
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
            this.DGVReporteCaja = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.primerDatePicker = new System.Windows.Forms.DateTimePicker();
            this.segundoDatePicker = new System.Windows.Forms.DateTimePicker();
            this.txtBuscador = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.corteCaja = new System.Windows.Forms.DataGridViewImageColumn();
            this.dineroAgregado = new System.Windows.Forms.DataGridViewImageColumn();
            this.dineroRetirado = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGVReporteCaja)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVReporteCaja
            // 
            this.DGVReporteCaja.AllowUserToAddRows = false;
            this.DGVReporteCaja.AllowUserToDeleteRows = false;
            this.DGVReporteCaja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVReporteCaja.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.User,
            this.date,
            this.corteCaja,
            this.dineroAgregado,
            this.dineroRetirado});
            this.DGVReporteCaja.Cursor = System.Windows.Forms.Cursors.Default;
            this.DGVReporteCaja.Location = new System.Drawing.Point(12, 154);
            this.DGVReporteCaja.Name = "DGVReporteCaja";
            this.DGVReporteCaja.RowHeadersVisible = false;
            this.DGVReporteCaja.Size = new System.Drawing.Size(725, 245);
            this.DGVReporteCaja.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(268, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "DEL:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label2.Location = new System.Drawing.Point(247, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 21);
            this.label2.TabIndex = 7;
            this.label2.Text = "HASTA:";
            // 
            // primerDatePicker
            // 
            this.primerDatePicker.CalendarFont = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.primerDatePicker.CustomFormat = "yyyy-MM-dd";
            this.primerDatePicker.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.primerDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.primerDatePicker.Location = new System.Drawing.Point(314, 82);
            this.primerDatePicker.Name = "primerDatePicker";
            this.primerDatePicker.Size = new System.Drawing.Size(181, 23);
            this.primerDatePicker.TabIndex = 8;
            // 
            // segundoDatePicker
            // 
            this.segundoDatePicker.CalendarFont = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.segundoDatePicker.CustomFormat = "yyyy-MM-dd";
            this.segundoDatePicker.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.segundoDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.segundoDatePicker.Location = new System.Drawing.Point(314, 116);
            this.segundoDatePicker.Name = "segundoDatePicker";
            this.segundoDatePicker.Size = new System.Drawing.Size(181, 23);
            this.segundoDatePicker.TabIndex = 9;
            // 
            // txtBuscador
            // 
            this.txtBuscador.Location = new System.Drawing.Point(209, 42);
            this.txtBuscador.Name = "txtBuscador";
            this.txtBuscador.Size = new System.Drawing.Size(343, 20);
            this.txtBuscador.TabIndex = 10;
            // 
            // button1
            // 
            this.button1.Image = global::PuntoDeVentaV2.Properties.Resources.search;
            this.button1.Location = new System.Drawing.Point(573, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(51, 24);
            this.button1.TabIndex = 11;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(291, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(214, 16);
            this.label3.TabIndex = 12;
            this.label3.Text = "Realiza la busqueda de un reporte";
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            this.id.Width = 10;
            // 
            // User
            // 
            this.User.HeaderText = "Usuario";
            this.User.Name = "User";
            this.User.ReadOnly = true;
            this.User.Width = 235;
            // 
            // date
            // 
            this.date.HeaderText = "Fecha";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            this.date.Width = 170;
            // 
            // corteCaja
            // 
            this.corteCaja.HeaderText = "Corte de Caja";
            this.corteCaja.Name = "corteCaja";
            this.corteCaja.ReadOnly = true;
            this.corteCaja.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.corteCaja.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dineroAgregado
            // 
            this.dineroAgregado.HeaderText = "Dinero Agregaro";
            this.dineroAgregado.Name = "dineroAgregado";
            this.dineroAgregado.ReadOnly = true;
            // 
            // dineroRetirado
            // 
            this.dineroRetirado.HeaderText = "Dinero Retirado";
            this.dineroRetirado.Name = "dineroRetirado";
            this.dineroRetirado.ReadOnly = true;
            // 
            // BuscarReporteCajaPorFecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 411);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtBuscador);
            this.Controls.Add(this.segundoDatePicker);
            this.Controls.Add(this.primerDatePicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DGVReporteCaja);
            this.Name = "BuscarReporteCajaPorFecha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Caja";
            this.Load += new System.EventHandler(this.BuscarReporteCajaPorFecha_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVReporteCaja)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView DGVReporteCaja;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker primerDatePicker;
        private System.Windows.Forms.DateTimePicker segundoDatePicker;
        private System.Windows.Forms.TextBox txtBuscador;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewImageColumn corteCaja;
        private System.Windows.Forms.DataGridViewImageColumn dineroAgregado;
        private System.Windows.Forms.DataGridViewImageColumn dineroRetirado;
    }
}