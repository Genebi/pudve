namespace PuntoDeVentaV2
{
    partial class BuscadorReporteInventario
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
            this.DGVInventario = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.numRevision = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.usuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mostrar = new System.Windows.Forms.DataGridViewImageColumn();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.txtBuscador = new System.Windows.Forms.TextBox();
            this.segundoDatePicker = new System.Windows.Forms.DateTimePicker();
            this.primerDatePicker = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.DGVInventario)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVInventario
            // 
            this.DGVInventario.AllowUserToAddRows = false;
            this.DGVInventario.AllowUserToDeleteRows = false;
            this.DGVInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVInventario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.numRevision,
            this.usuario,
            this.fecha,
            this.mostrar});
            this.DGVInventario.Location = new System.Drawing.Point(12, 119);
            this.DGVInventario.Name = "DGVInventario";
            this.DGVInventario.RowHeadersVisible = false;
            this.DGVInventario.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DGVInventario.Size = new System.Drawing.Size(1018, 245);
            this.DGVInventario.TabIndex = 1;
            this.DGVInventario.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVInventario_CellClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(376, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 24);
            this.label3.TabIndex = 13;
            this.label3.Text = "Reportes Inventario";
            // 
            // numRevision
            // 
            this.numRevision.FillWeight = 27.5862F;
            this.numRevision.HeaderText = "No. Revision";
            this.numRevision.Name = "numRevision";
            this.numRevision.ReadOnly = true;
            this.numRevision.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.numRevision.Width = 80;
            // 
            // usuario
            // 
            this.usuario.FillWeight = 306.6767F;
            this.usuario.HeaderText = "Usuario";
            this.usuario.Name = "usuario";
            this.usuario.ReadOnly = true;
            this.usuario.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.usuario.Width = 738;
            // 
            // fecha
            // 
            this.fecha.FillWeight = 42.35772F;
            this.fecha.HeaderText = "Fecha";
            this.fecha.Name = "fecha";
            this.fecha.ReadOnly = true;
            this.fecha.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.fecha.Width = 138;
            // 
            // mostrar
            // 
            this.mostrar.FillWeight = 23.37937F;
            this.mostrar.HeaderText = "Mostrar";
            this.mostrar.Name = "mostrar";
            this.mostrar.ReadOnly = true;
            this.mostrar.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.mostrar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.mostrar.Width = 59;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Image = global::PuntoDeVentaV2.Properties.Resources.search;
            this.btnBuscar.Location = new System.Drawing.Point(967, 81);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(63, 23);
            this.btnBuscar.TabIndex = 17;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // txtBuscador
            // 
            this.txtBuscador.Location = new System.Drawing.Point(12, 81);
            this.txtBuscador.Multiline = true;
            this.txtBuscador.Name = "txtBuscador";
            this.txtBuscador.Size = new System.Drawing.Size(657, 23);
            this.txtBuscador.TabIndex = 16;
            this.txtBuscador.TextChanged += new System.EventHandler(this.txtBuscador_TextChanged);
            this.txtBuscador.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscador_KeyDown);
            // 
            // segundoDatePicker
            // 
            this.segundoDatePicker.CalendarFont = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.segundoDatePicker.CustomFormat = "yyyy-MM-dd";
            this.segundoDatePicker.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.segundoDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.segundoDatePicker.Location = new System.Drawing.Point(825, 81);
            this.segundoDatePicker.Name = "segundoDatePicker";
            this.segundoDatePicker.Size = new System.Drawing.Size(114, 23);
            this.segundoDatePicker.TabIndex = 15;
            // 
            // primerDatePicker
            // 
            this.primerDatePicker.CalendarFont = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.primerDatePicker.CustomFormat = "yyyy-MM-dd";
            this.primerDatePicker.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.primerDatePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.primerDatePicker.Location = new System.Drawing.Point(696, 81);
            this.primerDatePicker.Name = "primerDatePicker";
            this.primerDatePicker.Size = new System.Drawing.Size(114, 23);
            this.primerDatePicker.TabIndex = 14;
            // 
            // BuscadorReporteInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 411);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.txtBuscador);
            this.Controls.Add(this.segundoDatePicker);
            this.Controls.Add(this.primerDatePicker);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.DGVInventario);
            this.Name = "BuscadorReporteInventario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reportes de Inventario";
            this.Load += new System.EventHandler(this.BuscadorReporteInventario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVInventario)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVInventario;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn numRevision;
        private System.Windows.Forms.DataGridViewTextBoxColumn usuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn fecha;
        private System.Windows.Forms.DataGridViewImageColumn mostrar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.TextBox txtBuscador;
        private System.Windows.Forms.DateTimePicker segundoDatePicker;
        private System.Windows.Forms.DateTimePicker primerDatePicker;
    }
}