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
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.User = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.corteCaja = new System.Windows.Forms.DataGridViewImageColumn();
            this.dineroAgregado = new System.Windows.Forms.DataGridViewImageColumn();
            this.dineroRetirado = new System.Windows.Forms.DataGridViewImageColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.primerDatePicker = new System.Windows.Forms.DateTimePicker();
            this.segundoDatePicker = new System.Windows.Forms.DateTimePicker();
            this.txtBuscador = new System.Windows.Forms.TextBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGVReporteCaja)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVReporteCaja
            // 
            this.DGVReporteCaja.AllowUserToAddRows = false;
            this.DGVReporteCaja.AllowUserToDeleteRows = false;
            this.DGVReporteCaja.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVReporteCaja.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVReporteCaja.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.User,
            this.date,
            this.corteCaja,
            this.dineroAgregado,
            this.dineroRetirado,
            this.fecha});
            this.DGVReporteCaja.Cursor = System.Windows.Forms.Cursors.Default;
            this.DGVReporteCaja.Location = new System.Drawing.Point(12, 119);
            this.DGVReporteCaja.Name = "DGVReporteCaja";
            this.DGVReporteCaja.RowHeadersVisible = false;
            this.DGVReporteCaja.Size = new System.Drawing.Size(1018, 245);
            this.DGVReporteCaja.TabIndex = 3;
            this.DGVReporteCaja.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVReporteCaja_CellClick);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // User
            // 
            this.User.FillWeight = 230.667F;
            this.User.HeaderText = "Usuario";
            this.User.Name = "User";
            this.User.ReadOnly = true;
            // 
            // date
            // 
            this.date.FillWeight = 101.5228F;
            this.date.HeaderText = "Fecha";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // corteCaja
            // 
            this.corteCaja.FillWeight = 55.93673F;
            this.corteCaja.HeaderText = "Corte de Caja";
            this.corteCaja.Name = "corteCaja";
            this.corteCaja.ReadOnly = true;
            this.corteCaja.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.corteCaja.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dineroAgregado
            // 
            this.dineroAgregado.FillWeight = 55.93673F;
            this.dineroAgregado.HeaderText = "Dinero Agregaro";
            this.dineroAgregado.Name = "dineroAgregado";
            this.dineroAgregado.ReadOnly = true;
            // 
            // dineroRetirado
            // 
            this.dineroRetirado.FillWeight = 55.93673F;
            this.dineroRetirado.HeaderText = "Dinero Retirado";
            this.dineroRetirado.Name = "dineroRetirado";
            this.dineroRetirado.ReadOnly = true;
            // 
            // fecha
            // 
            this.fecha.HeaderText = "fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            this.fecha.Visible = false;
            // 
            // primerDatePicker
            // 
            this.primerDatePicker.CalendarFont = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.primerDatePicker.CustomFormat = "yyyy-MM-dd";
            this.primerDatePicker.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.primerDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.primerDatePicker.Location = new System.Drawing.Point(697, 81);
            this.primerDatePicker.Name = "primerDatePicker";
            this.primerDatePicker.Size = new System.Drawing.Size(114, 23);
            this.primerDatePicker.TabIndex = 8;
            // 
            // segundoDatePicker
            // 
            this.segundoDatePicker.CalendarFont = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.segundoDatePicker.CustomFormat = "yyyy-MM-dd";
            this.segundoDatePicker.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.segundoDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.segundoDatePicker.Location = new System.Drawing.Point(828, 81);
            this.segundoDatePicker.Name = "segundoDatePicker";
            this.segundoDatePicker.Size = new System.Drawing.Size(114, 23);
            this.segundoDatePicker.TabIndex = 9;
            // 
            // txtBuscador
            // 
            this.txtBuscador.Location = new System.Drawing.Point(12, 81);
            this.txtBuscador.Multiline = true;
            this.txtBuscador.Name = "txtBuscador";
            this.txtBuscador.Size = new System.Drawing.Size(657, 23);
            this.txtBuscador.TabIndex = 10;
            this.txtBuscador.TextChanged += new System.EventHandler(this.txtBuscador_TextChanged);
            this.txtBuscador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscador_KeyDown);
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::PuntoDeVentaV2.Properties.Resources.search;
            this.btnBuscar.Location = new System.Drawing.Point(967, 81);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(63, 23);
            this.btnBuscar.TabIndex = 11;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(454, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(141, 24);
            this.label3.TabIndex = 12;
            this.label3.Text = "Reportes Caja";
            // 
            // BuscarReporteCajaPorFecha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 411);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBuscador);
            this.Controls.Add(this.segundoDatePicker);
            this.Controls.Add(this.primerDatePicker);
            this.Controls.Add(this.DGVReporteCaja);
            this.Name = "BuscarReporteCajaPorFecha";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte de Caja";
            this.Load += new System.EventHandler(this.BuscarReporteCajaPorFecha_Load);
            this.Shown += new System.EventHandler(this.BuscarReporteCajaPorFecha_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVReporteCaja)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView DGVReporteCaja;
        private System.Windows.Forms.DateTimePicker primerDatePicker;
        private System.Windows.Forms.DateTimePicker segundoDatePicker;
        private System.Windows.Forms.TextBox txtBuscador;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn User;
        private System.Windows.Forms.DataGridViewTextBoxColumn date;
        private System.Windows.Forms.DataGridViewImageColumn corteCaja;
        private System.Windows.Forms.DataGridViewImageColumn dineroAgregado;
        private System.Windows.Forms.DataGridViewImageColumn dineroRetirado;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
    }
}