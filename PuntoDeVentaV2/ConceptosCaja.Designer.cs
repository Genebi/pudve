namespace PuntoDeVentaV2
{
    partial class ConceptosCaja
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
            this.DGVConceptos = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Habilitar = new System.Windows.Forms.DataGridViewImageColumn();
            this.Deshabilitar = new System.Windows.Forms.DataGridViewImageColumn();
            this.txtConcepto = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rbHabilitados = new System.Windows.Forms.RadioButton();
            this.rbDeshabilitados = new System.Windows.Forms.RadioButton();
            this.btnBuscar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVConceptos)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVConceptos
            // 
            this.DGVConceptos.AllowUserToAddRows = false;
            this.DGVConceptos.AllowUserToDeleteRows = false;
            this.DGVConceptos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVConceptos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Concepto,
            this.Fecha,
            this.Habilitar,
            this.Deshabilitar});
            this.DGVConceptos.Location = new System.Drawing.Point(12, 125);
            this.DGVConceptos.Name = "DGVConceptos";
            this.DGVConceptos.ReadOnly = true;
            this.DGVConceptos.RowHeadersVisible = false;
            this.DGVConceptos.Size = new System.Drawing.Size(460, 197);
            this.DGVConceptos.TabIndex = 0;
            this.DGVConceptos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVConceptos_CellClick);
            this.DGVConceptos.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVConceptos_CellDoubleClick);
            this.DGVConceptos.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVConceptos_CellMouseEnter);
            this.DGVConceptos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGVConceptos_KeyDown);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Concepto
            // 
            this.Concepto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Concepto.HeaderText = "Concepto";
            this.Concepto.Name = "Concepto";
            this.Concepto.ReadOnly = true;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 120;
            // 
            // Habilitar
            // 
            this.Habilitar.HeaderText = "Habilitar";
            this.Habilitar.Name = "Habilitar";
            this.Habilitar.ReadOnly = true;
            this.Habilitar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Habilitar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Habilitar.Width = 65;
            // 
            // Deshabilitar
            // 
            this.Deshabilitar.HeaderText = "Deshabilitar";
            this.Deshabilitar.Name = "Deshabilitar";
            this.Deshabilitar.ReadOnly = true;
            this.Deshabilitar.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Deshabilitar.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.Deshabilitar.Width = 70;
            // 
            // txtConcepto
            // 
            this.txtConcepto.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtConcepto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConcepto.Location = new System.Drawing.Point(16, 40);
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.Size = new System.Drawing.Size(282, 23);
            this.txtConcepto.TabIndex = 1;
            this.txtConcepto.TextChanged += new System.EventHandler(this.txtConcepto_TextChanged);
            this.txtConcepto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConcepto_KeyDown);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Green;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(385, 40);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(87, 24);
            this.btnAgregar.TabIndex = 9;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 17);
            this.label1.TabIndex = 10;
            this.label1.Text = "Concepto";
            // 
            // rbHabilitados
            // 
            this.rbHabilitados.AutoSize = true;
            this.rbHabilitados.Checked = true;
            this.rbHabilitados.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbHabilitados.Location = new System.Drawing.Point(16, 92);
            this.rbHabilitados.Name = "rbHabilitados";
            this.rbHabilitados.Size = new System.Drawing.Size(93, 21);
            this.rbHabilitados.TabIndex = 11;
            this.rbHabilitados.TabStop = true;
            this.rbHabilitados.Text = "Habilitados";
            this.rbHabilitados.UseVisualStyleBackColor = true;
            this.rbHabilitados.CheckedChanged += new System.EventHandler(this.rbHabilitados_CheckedChanged);
            // 
            // rbDeshabilitados
            // 
            this.rbDeshabilitados.AutoSize = true;
            this.rbDeshabilitados.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDeshabilitados.Location = new System.Drawing.Point(125, 92);
            this.rbDeshabilitados.Name = "rbDeshabilitados";
            this.rbDeshabilitados.Size = new System.Drawing.Size(114, 21);
            this.rbDeshabilitados.TabIndex = 12;
            this.rbDeshabilitados.Text = "Deshabilitados";
            this.rbDeshabilitados.UseVisualStyleBackColor = true;
            this.rbDeshabilitados.CheckedChanged += new System.EventHandler(this.rbDeshabilitados_CheckedChanged);
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackgroundImage = global::PuntoDeVentaV2.Properties.Resources.search;
            this.btnBuscar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBuscar.Location = new System.Drawing.Point(304, 40);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(56, 24);
            this.btnBuscar.TabIndex = 14;
            this.btnBuscar.UseVisualStyleBackColor = true;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // ConceptosCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 331);
            this.Controls.Add(this.btnBuscar);
            this.Controls.Add(this.rbDeshabilitados);
            this.Controls.Add(this.rbHabilitados);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.txtConcepto);
            this.Controls.Add(this.DGVConceptos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConceptosCaja";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ConceptosCaja_Load);
            this.Shown += new System.EventHandler(this.ConceptosCaja_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConceptosCaja_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVConceptos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVConceptos;
        private System.Windows.Forms.TextBox txtConcepto;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbHabilitados;
        private System.Windows.Forms.RadioButton rbDeshabilitados;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewImageColumn Habilitar;
        private System.Windows.Forms.DataGridViewImageColumn Deshabilitar;
        private System.Windows.Forms.Button btnBuscar;
    }
}