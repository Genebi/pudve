namespace PuntoDeVentaV2
{
    partial class AgregarDetalleProducto
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
            this.separadorInicial = new System.Windows.Forms.Label();
            this.txtFiltrar = new System.Windows.Forms.TextBox();
            this.btnGuardarDetalles = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fLPLateralConcepto = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.fLPCentralDetalle = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddDetalle = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteDetalle = new System.Windows.Forms.Button();
            this.btnRenameDetalle = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // separadorInicial
            // 
            this.separadorInicial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separadorInicial.Location = new System.Drawing.Point(5, 48);
            this.separadorInicial.Name = "separadorInicial";
            this.separadorInicial.Size = new System.Drawing.Size(830, 2);
            this.separadorInicial.TabIndex = 19;
            // 
            // txtFiltrar
            // 
            this.txtFiltrar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFiltrar.Location = new System.Drawing.Point(11, 18);
            this.txtFiltrar.Name = "txtFiltrar";
            this.txtFiltrar.Size = new System.Drawing.Size(168, 22);
            this.txtFiltrar.TabIndex = 20;
            this.txtFiltrar.Text = "Filtrar...";
            // 
            // btnGuardarDetalles
            // 
            this.btnGuardarDetalles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGuardarDetalles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarDetalles.FlatAppearance.BorderSize = 0;
            this.btnGuardarDetalles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarDetalles.Font = new System.Drawing.Font("Century Schoolbook", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarDetalles.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnGuardarDetalles.Location = new System.Drawing.Point(57, 15);
            this.btnGuardarDetalles.Name = "btnGuardarDetalles";
            this.btnGuardarDetalles.Size = new System.Drawing.Size(180, 41);
            this.btnGuardarDetalles.TabIndex = 26;
            this.btnGuardarDetalles.Text = "Guardar";
            this.btnGuardarDetalles.UseVisualStyleBackColor = false;
            this.btnGuardarDetalles.Click += new System.EventHandler(this.btnGuardarDetalles_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.fLPLateralConcepto);
            this.panel1.Location = new System.Drawing.Point(3, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(215, 610);
            this.panel1.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Agregar";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Detalle";
            // 
            // fLPLateralConcepto
            // 
            this.fLPLateralConcepto.BackColor = System.Drawing.SystemColors.Control;
            this.fLPLateralConcepto.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLPLateralConcepto.Location = new System.Drawing.Point(4, 29);
            this.fLPLateralConcepto.Name = "fLPLateralConcepto";
            this.fLPLateralConcepto.Size = new System.Drawing.Size(207, 577);
            this.fLPLateralConcepto.TabIndex = 0;
            this.fLPLateralConcepto.WrapContents = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.fLPCentralDetalle);
            this.panel2.Location = new System.Drawing.Point(223, 55);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(618, 474);
            this.panel2.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(239, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Detalles Seleccionados";
            // 
            // fLPCentralDetalle
            // 
            this.fLPCentralDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fLPCentralDetalle.AutoScroll = true;
            this.fLPCentralDetalle.Location = new System.Drawing.Point(5, 29);
            this.fLPCentralDetalle.Name = "fLPCentralDetalle";
            this.fLPCentralDetalle.Size = new System.Drawing.Size(608, 439);
            this.fLPCentralDetalle.TabIndex = 0;
            // 
            // btnAddDetalle
            // 
            this.btnAddDetalle.BackColor = System.Drawing.Color.ForestGreen;
            this.btnAddDetalle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddDetalle.FlatAppearance.BorderSize = 0;
            this.btnAddDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDetalle.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnAddDetalle.ForeColor = System.Drawing.Color.White;
            this.btnAddDetalle.Location = new System.Drawing.Point(10, 28);
            this.btnAddDetalle.Name = "btnAddDetalle";
            this.btnAddDetalle.Size = new System.Drawing.Size(266, 28);
            this.btnAddDetalle.TabIndex = 29;
            this.btnAddDetalle.Text = "Agregar";
            this.btnAddDetalle.UseVisualStyleBackColor = false;
            this.btnAddDetalle.Click += new System.EventHandler(this.btnAddDetalle_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDeleteDetalle);
            this.groupBox1.Controls.Add(this.btnRenameDetalle);
            this.groupBox1.Controls.Add(this.btnAddDetalle);
            this.groupBox1.Location = new System.Drawing.Point(228, 546);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 115);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Procedimientos de Detalles:";
            // 
            // btnDeleteDetalle
            // 
            this.btnDeleteDetalle.BackColor = System.Drawing.Color.DarkRed;
            this.btnDeleteDetalle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteDetalle.FlatAppearance.BorderSize = 0;
            this.btnDeleteDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteDetalle.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnDeleteDetalle.ForeColor = System.Drawing.Color.White;
            this.btnDeleteDetalle.Location = new System.Drawing.Point(146, 68);
            this.btnDeleteDetalle.Name = "btnDeleteDetalle";
            this.btnDeleteDetalle.Size = new System.Drawing.Size(130, 28);
            this.btnDeleteDetalle.TabIndex = 31;
            this.btnDeleteDetalle.Text = "Eliminar";
            this.btnDeleteDetalle.UseVisualStyleBackColor = false;
            this.btnDeleteDetalle.Click += new System.EventHandler(this.btnDeleteDetalle_Click);
            // 
            // btnRenameDetalle
            // 
            this.btnRenameDetalle.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnRenameDetalle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRenameDetalle.FlatAppearance.BorderSize = 0;
            this.btnRenameDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRenameDetalle.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnRenameDetalle.ForeColor = System.Drawing.Color.White;
            this.btnRenameDetalle.Location = new System.Drawing.Point(10, 68);
            this.btnRenameDetalle.Name = "btnRenameDetalle";
            this.btnRenameDetalle.Size = new System.Drawing.Size(130, 28);
            this.btnRenameDetalle.TabIndex = 30;
            this.btnRenameDetalle.Text = "Renombrar";
            this.btnRenameDetalle.UseVisualStyleBackColor = false;
            this.btnRenameDetalle.Click += new System.EventHandler(this.btnRenameDetalle_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCerrar);
            this.groupBox2.Controls.Add(this.btnGuardarDetalles);
            this.groupBox2.Location = new System.Drawing.Point(562, 546);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(279, 115);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Century Schoolbook", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(57, 65);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(180, 41);
            this.btnCerrar.TabIndex = 27;
            this.btnCerrar.Text = "Cencelar";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // AgregarDetalleProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 666);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtFiltrar);
            this.Controls.Add(this.separadorInicial);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "AgregarDetalleProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Detalles Producto";
            this.Load += new System.EventHandler(this.AgregarDetalleProducto_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label separadorInicial;
        private System.Windows.Forms.TextBox txtFiltrar;
        private System.Windows.Forms.Button btnGuardarDetalles;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel fLPLateralConcepto;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.FlowLayoutPanel fLPCentralDetalle;
        private System.Windows.Forms.Button btnAddDetalle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDeleteDetalle;
        private System.Windows.Forms.Button btnRenameDetalle;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}