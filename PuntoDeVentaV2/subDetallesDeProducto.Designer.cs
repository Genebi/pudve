
namespace PuntoDeVentaV2
{
    partial class subDetallesDeProducto
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(subDetallesDeProducto));
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.LbNombreCategoria = new System.Windows.Forms.Label();
            this.pboxEditar = new System.Windows.Forms.PictureBox();
            this.dgvDetallesSubdetalle = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblStockRestanteText = new System.Windows.Forms.Label();
            this.lblStockRestanteNum = new System.Windows.Forms.Label();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnAgregarSubDetalle = new System.Windows.Forms.Button();
            this.lblNombreProducto = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddDetalle = new System.Windows.Forms.Button();
            this.fLPLateralCategorias = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGuardarDetalles = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalStok = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Deshabilitar = new System.Windows.Forms.DataGridViewImageColumn();
            this.SubID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxEditar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallesSubdetalle)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Controls.Add(this.dgvDetallesSubdetalle);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Location = new System.Drawing.Point(253, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(565, 514);
            this.panel2.TabIndex = 30;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.LbNombreCategoria);
            this.flowLayoutPanel1.Controls.Add(this.pboxEditar);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 2);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(557, 32);
            this.flowLayoutPanel1.TabIndex = 36;
            this.flowLayoutPanel1.Visible = false;
            // 
            // LbNombreCategoria
            // 
            this.LbNombreCategoria.AutoSize = true;
            this.LbNombreCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbNombreCategoria.Location = new System.Drawing.Point(3, 0);
            this.LbNombreCategoria.Name = "LbNombreCategoria";
            this.LbNombreCategoria.Size = new System.Drawing.Size(170, 20);
            this.LbNombreCategoria.TabIndex = 2;
            this.LbNombreCategoria.Text = "Nombre Sub Detalle";
            this.LbNombreCategoria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pboxEditar
            // 
            this.pboxEditar.Image = global::PuntoDeVentaV2.Properties.Resources.pencil1;
            this.pboxEditar.Location = new System.Drawing.Point(178, 2);
            this.pboxEditar.Margin = new System.Windows.Forms.Padding(2);
            this.pboxEditar.Name = "pboxEditar";
            this.pboxEditar.Size = new System.Drawing.Size(21, 17);
            this.pboxEditar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pboxEditar.TabIndex = 3;
            this.pboxEditar.TabStop = false;
            this.pboxEditar.Visible = false;
            this.pboxEditar.Click += new System.EventHandler(this.pboxEditar_Click);
            // 
            // dgvDetallesSubdetalle
            // 
            this.dgvDetallesSubdetalle.AllowUserToAddRows = false;
            this.dgvDetallesSubdetalle.AllowUserToDeleteRows = false;
            this.dgvDetallesSubdetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetallesSubdetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Valor,
            this.Stock,
            this.Cantidad,
            this.TotalStok,
            this.TD,
            this.Deshabilitar,
            this.SubID});
            this.dgvDetallesSubdetalle.Location = new System.Drawing.Point(3, 39);
            this.dgvDetallesSubdetalle.Name = "dgvDetallesSubdetalle";
            this.dgvDetallesSubdetalle.RowHeadersVisible = false;
            this.dgvDetallesSubdetalle.RowHeadersWidth = 62;
            this.dgvDetallesSubdetalle.Size = new System.Drawing.Size(556, 408);
            this.dgvDetallesSubdetalle.TabIndex = 0;
            this.dgvDetallesSubdetalle.Visible = false;
            this.dgvDetallesSubdetalle.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetallesSubdetalle_CellClick);
            this.dgvDetallesSubdetalle.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetallesSubdetalle_CellEndEdit);
            this.dgvDetallesSubdetalle.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgvDetallesSubdetalle_DataError);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblStockRestanteText);
            this.groupBox4.Controls.Add(this.lblStockRestanteNum);
            this.groupBox4.Controls.Add(this.btnGuardar);
            this.groupBox4.Location = new System.Drawing.Point(242, 453);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(317, 57);
            this.groupBox4.TabIndex = 35;
            this.groupBox4.TabStop = false;
            this.groupBox4.Visible = false;
            // 
            // lblStockRestanteText
            // 
            this.lblStockRestanteText.Location = new System.Drawing.Point(232, 36);
            this.lblStockRestanteText.Name = "lblStockRestanteText";
            this.lblStockRestanteText.Size = new System.Drawing.Size(81, 18);
            this.lblStockRestanteText.TabIndex = 39;
            this.lblStockRestanteText.Text = "Por asignar";
            this.lblStockRestanteText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblStockRestanteNum
            // 
            this.lblStockRestanteNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockRestanteNum.Location = new System.Drawing.Point(233, 10);
            this.lblStockRestanteNum.Name = "lblStockRestanteNum";
            this.lblStockRestanteNum.Size = new System.Drawing.Size(78, 17);
            this.lblStockRestanteNum.TabIndex = 38;
            this.lblStockRestanteNum.Text = "500";
            this.lblStockRestanteNum.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblStockRestanteNum.TextChanged += new System.EventHandler(this.lblStockRestanteNum_TextChanged);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.Orange;
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.Enabled = false;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Image = global::PuntoDeVentaV2.Properties.Resources.save1;
            this.btnGuardar.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnGuardar.Location = new System.Drawing.Point(6, 10);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(221, 41);
            this.btnGuardar.TabIndex = 33;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click_1);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnAgregarSubDetalle);
            this.groupBox3.Location = new System.Drawing.Point(3, 453);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(233, 57);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Visible = false;
            // 
            // btnAgregarSubDetalle
            // 
            this.btnAgregarSubDetalle.BackColor = System.Drawing.Color.ForestGreen;
            this.btnAgregarSubDetalle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarSubDetalle.FlatAppearance.BorderSize = 0;
            this.btnAgregarSubDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarSubDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnAgregarSubDetalle.ForeColor = System.Drawing.Color.White;
            this.btnAgregarSubDetalle.Image = global::PuntoDeVentaV2.Properties.Resources.list_ul1;
            this.btnAgregarSubDetalle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAgregarSubDetalle.Location = new System.Drawing.Point(6, 10);
            this.btnAgregarSubDetalle.Name = "btnAgregarSubDetalle";
            this.btnAgregarSubDetalle.Size = new System.Drawing.Size(221, 41);
            this.btnAgregarSubDetalle.TabIndex = 33;
            this.btnAgregarSubDetalle.Text = "Agregar";
            this.btnAgregarSubDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregarSubDetalle.UseVisualStyleBackColor = false;
            this.btnAgregarSubDetalle.Click += new System.EventHandler(this.btnAgregarSubDetalle_Click);
            // 
            // lblNombreProducto
            // 
            this.lblNombreProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreProducto.Location = new System.Drawing.Point(135, 9);
            this.lblNombreProducto.Name = "lblNombreProducto";
            this.lblNombreProducto.Size = new System.Drawing.Size(559, 23);
            this.lblNombreProducto.TabIndex = 1;
            this.lblNombreProducto.Text = "Detalles Seleccionados";
            this.lblNombreProducto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.fLPLateralCategorias);
            this.panel1.Location = new System.Drawing.Point(3, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 514);
            this.panel1.TabIndex = 29;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(71, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sub Detalles";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddDetalle);
            this.groupBox1.Location = new System.Drawing.Point(6, 453);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(233, 57);
            this.groupBox1.TabIndex = 35;
            this.groupBox1.TabStop = false;
            // 
            // btnAddDetalle
            // 
            this.btnAddDetalle.BackColor = System.Drawing.Color.ForestGreen;
            this.btnAddDetalle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddDetalle.FlatAppearance.BorderSize = 0;
            this.btnAddDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnAddDetalle.ForeColor = System.Drawing.Color.White;
            this.btnAddDetalle.Image = global::PuntoDeVentaV2.Properties.Resources.list_ul1;
            this.btnAddDetalle.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnAddDetalle.Location = new System.Drawing.Point(6, 10);
            this.btnAddDetalle.Name = "btnAddDetalle";
            this.btnAddDetalle.Size = new System.Drawing.Size(221, 41);
            this.btnAddDetalle.TabIndex = 33;
            this.btnAddDetalle.Text = "Agregar";
            this.btnAddDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAddDetalle.UseVisualStyleBackColor = false;
            this.btnAddDetalle.Click += new System.EventHandler(this.btnAddDetalle_Click);
            // 
            // fLPLateralCategorias
            // 
            this.fLPLateralCategorias.BackColor = System.Drawing.SystemColors.Control;
            this.fLPLateralCategorias.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fLPLateralCategorias.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLPLateralCategorias.Location = new System.Drawing.Point(6, 39);
            this.fLPLateralCategorias.Name = "fLPLateralCategorias";
            this.fLPLateralCategorias.Size = new System.Drawing.Size(233, 408);
            this.fLPLateralCategorias.TabIndex = 0;
            this.fLPLateralCategorias.WrapContents = false;
            // 
            // btnGuardarDetalles
            // 
            this.btnGuardarDetalles.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnGuardarDetalles.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnGuardarDetalles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarDetalles.FlatAppearance.BorderSize = 0;
            this.btnGuardarDetalles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarDetalles.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.btnGuardarDetalles.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnGuardarDetalles.Image = global::PuntoDeVentaV2.Properties.Resources.check_circle_o1;
            this.btnGuardarDetalles.Location = new System.Drawing.Point(6, 14);
            this.btnGuardarDetalles.Name = "btnGuardarDetalles";
            this.btnGuardarDetalles.Size = new System.Drawing.Size(221, 41);
            this.btnGuardarDetalles.TabIndex = 31;
            this.btnGuardarDetalles.Text = "Aceptar";
            this.btnGuardarDetalles.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnGuardarDetalles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardarDetalles.UseVisualStyleBackColor = false;
            this.btnGuardarDetalles.Click += new System.EventHandler(this.btnGuardarDetalles_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGuardarDetalles);
            this.groupBox2.Location = new System.Drawing.Point(579, 555);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 61);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 8;
            this.ID.Name = "ID";
            this.ID.Visible = false;
            this.ID.Width = 150;
            // 
            // Valor
            // 
            this.Valor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Valor.DataPropertyName = "Valor";
            this.Valor.HeaderText = "Valor";
            this.Valor.MinimumWidth = 8;
            this.Valor.Name = "Valor";
            // 
            // Stock
            // 
            this.Stock.DataPropertyName = "Stock";
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            this.Stock.DefaultCellStyle = dataGridViewCellStyle1;
            this.Stock.HeaderText = "Stock";
            this.Stock.MinimumWidth = 8;
            this.Stock.Name = "Stock";
            this.Stock.Width = 150;
            // 
            // Cantidad
            // 
            this.Cantidad.DataPropertyName = "0";
            this.Cantidad.HeaderText = "Cantidad a Vender";
            this.Cantidad.MinimumWidth = 8;
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.Visible = false;
            this.Cantidad.Width = 150;
            // 
            // TotalStok
            // 
            this.TotalStok.DataPropertyName = "TotalStock";
            this.TotalStok.HeaderText = "TotalStok";
            this.TotalStok.MinimumWidth = 8;
            this.TotalStok.Name = "TotalStok";
            this.TotalStok.Visible = false;
            this.TotalStok.Width = 150;
            // 
            // TD
            // 
            this.TD.DataPropertyName = "TipoDato";
            this.TD.HeaderText = "TD";
            this.TD.MinimumWidth = 8;
            this.TD.Name = "TD";
            this.TD.Visible = false;
            this.TD.Width = 150;
            // 
            // Deshabilitar
            // 
            this.Deshabilitar.HeaderText = "Deshabilitar";
            this.Deshabilitar.Image = ((System.Drawing.Image)(resources.GetObject("Deshabilitar.Image")));
            this.Deshabilitar.MinimumWidth = 8;
            this.Deshabilitar.Name = "Deshabilitar";
            this.Deshabilitar.Width = 150;
            // 
            // SubID
            // 
            this.SubID.DataPropertyName = "SubID";
            this.SubID.HeaderText = "SubID";
            this.SubID.MinimumWidth = 8;
            this.SubID.Name = "SubID";
            this.SubID.Visible = false;
            this.SubID.Width = 150;
            // 
            // subDetallesDeProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 621);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lblNombreProducto);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "subDetallesDeProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalles de producto";
            this.Load += new System.EventHandler(this.subDetallesDeProducto_Load);
            this.panel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxEditar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallesSubdetalle)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblNombreProducto;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel fLPLateralCategorias;
        private System.Windows.Forms.Button btnAddDetalle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LbNombreCategoria;
        private System.Windows.Forms.Button btnGuardarDetalles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnAgregarSubDetalle;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblStockRestanteText;
        private System.Windows.Forms.Label lblStockRestanteNum;
        private System.Windows.Forms.DataGridView dgvDetallesSubdetalle;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.PictureBox pboxEditar;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalStok;
        private System.Windows.Forms.DataGridViewTextBoxColumn TD;
        private System.Windows.Forms.DataGridViewImageColumn Deshabilitar;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubID;
    }
}