
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(subDetallesDeProducto));
            this.panel2 = new System.Windows.Forms.Panel();
            this.LbNombreCategoria = new System.Windows.Forms.Label();
            this.fLPCentralDetalle = new System.Windows.Forms.FlowLayoutPanel();
            this.lblNombreProducto = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddDetalle = new System.Windows.Forms.Button();
            this.fLPLateralCategorias = new System.Windows.Forms.FlowLayoutPanel();
            this.btnGuardarDetalles = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvDetallesSubdetalle = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Deshabilitar = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel2.SuspendLayout();
            this.fLPCentralDetalle.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallesSubdetalle)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.LbNombreCategoria);
            this.panel2.Controls.Add(this.fLPCentralDetalle);
            this.panel2.Location = new System.Drawing.Point(253, 35);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(565, 514);
            this.panel2.TabIndex = 30;
            // 
            // LbNombreCategoria
            // 
            this.LbNombreCategoria.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LbNombreCategoria.Location = new System.Drawing.Point(184, 12);
            this.LbNombreCategoria.Name = "LbNombreCategoria";
            this.LbNombreCategoria.Size = new System.Drawing.Size(228, 20);
            this.LbNombreCategoria.TabIndex = 2;
            this.LbNombreCategoria.Text = "Nombre Sub Detalle";
            this.LbNombreCategoria.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fLPCentralDetalle
            // 
            this.fLPCentralDetalle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.fLPCentralDetalle.AutoScroll = true;
            this.fLPCentralDetalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fLPCentralDetalle.Controls.Add(this.dgvDetallesSubdetalle);
            this.fLPCentralDetalle.Location = new System.Drawing.Point(3, 40);
            this.fLPCentralDetalle.Name = "fLPCentralDetalle";
            this.fLPCentralDetalle.Size = new System.Drawing.Size(556, 470);
            this.fLPCentralDetalle.TabIndex = 0;
            // 
            // lblNombreProducto
            // 
            this.lblNombreProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreProducto.Location = new System.Drawing.Point(135, 9);
            this.lblNombreProducto.Name = "lblNombreProducto";
            this.lblNombreProducto.Size = new System.Drawing.Size(559, 18);
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
            this.groupBox2.Location = new System.Drawing.Point(585, 551);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(233, 61);
            this.groupBox2.TabIndex = 36;
            this.groupBox2.TabStop = false;
            // 
            // dgvDetallesSubdetalle
            // 
            this.dgvDetallesSubdetalle.AllowUserToAddRows = false;
            this.dgvDetallesSubdetalle.AllowUserToDeleteRows = false;
            this.dgvDetallesSubdetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetallesSubdetalle.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Nombre,
            this.Stock,
            this.Deshabilitar});
            this.fLPCentralDetalle.SetFlowBreak(this.dgvDetallesSubdetalle, true);
            this.dgvDetallesSubdetalle.Location = new System.Drawing.Point(3, 3);
            this.dgvDetallesSubdetalle.Name = "dgvDetallesSubdetalle";
            this.dgvDetallesSubdetalle.RowHeadersVisible = false;
            this.dgvDetallesSubdetalle.Size = new System.Drawing.Size(543, 459);
            this.dgvDetallesSubdetalle.TabIndex = 0;
            this.dgvDetallesSubdetalle.Visible = false;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.Visible = false;
            // 
            // Nombre
            // 
            this.Nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            // 
            // Stock
            // 
            this.Stock.HeaderText = "Stock";
            this.Stock.Name = "Stock";
            this.Stock.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Stock.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Stock.Width = 120;
            // 
            // Deshabilitar
            // 
            this.Deshabilitar.HeaderText = "Deshabilitar";
            this.Deshabilitar.Image = ((System.Drawing.Image)(resources.GetObject("Deshabilitar.Image")));
            this.Deshabilitar.Name = "Deshabilitar";
            // 
            // subDetallesDeProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 613);
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
            this.fLPCentralDetalle.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetallesSubdetalle)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblNombreProducto;
        private System.Windows.Forms.FlowLayoutPanel fLPCentralDetalle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel fLPLateralCategorias;
        private System.Windows.Forms.Button btnAddDetalle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LbNombreCategoria;
        private System.Windows.Forms.Button btnGuardarDetalles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvDetallesSubdetalle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewImageColumn Deshabilitar;
    }
}