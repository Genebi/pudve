namespace PuntoDeVentaV2
{
    partial class AgregarStockXML
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblFecha = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblFolio = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblRFCEmisor = new System.Windows.Forms.Label();
            this.lblNomEmisor = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblRFCReceptor = new System.Windows.Forms.Label();
            this.lblNomReceptor = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblSubTot = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label19 = new System.Windows.Forms.Label();
            this.btnLoadXML = new System.Windows.Forms.Button();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblFecha);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblFolio);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 67);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(456, 126);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Datos en General: ";
            // 
            // lblFecha
            // 
            this.lblFecha.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lblFecha.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(77, 75);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(349, 23);
            this.lblFecha.TabIndex = 2;
            this.lblFecha.Text = "Fecha de Factura";
            this.lblFecha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Fecha:";
            // 
            // lblFolio
            // 
            this.lblFolio.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lblFolio.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFolio.Location = new System.Drawing.Point(76, 29);
            this.lblFolio.Name = "lblFolio";
            this.lblFolio.Size = new System.Drawing.Size(218, 23);
            this.lblFolio.TabIndex = 1;
            this.lblFolio.Text = "No de Folio";
            this.lblFolio.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folio:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblRFCEmisor);
            this.groupBox2.Controls.Add(this.lblNomEmisor);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(518, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(713, 126);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Emisor: ";
            // 
            // lblRFCEmisor
            // 
            this.lblRFCEmisor.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lblRFCEmisor.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRFCEmisor.Location = new System.Drawing.Point(104, 81);
            this.lblRFCEmisor.Name = "lblRFCEmisor";
            this.lblRFCEmisor.Size = new System.Drawing.Size(569, 23);
            this.lblRFCEmisor.TabIndex = 3;
            this.lblRFCEmisor.Text = "RFC de Emisor";
            this.lblRFCEmisor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNomEmisor
            // 
            this.lblNomEmisor.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lblNomEmisor.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomEmisor.Location = new System.Drawing.Point(101, 31);
            this.lblNomEmisor.Name = "lblNomEmisor";
            this.lblNomEmisor.Size = new System.Drawing.Size(572, 23);
            this.lblNomEmisor.TabIndex = 2;
            this.lblNomEmisor.Text = "Nombre de Emisor";
            this.lblNomEmisor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 80);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 17);
            this.label6.TabIndex = 1;
            this.label6.Text = "R.F.C.:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Nombre:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblRFCReceptor);
            this.groupBox3.Controls.Add(this.lblNomReceptor);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Location = new System.Drawing.Point(12, 215);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(642, 136);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Receptor: ";
            // 
            // lblRFCReceptor
            // 
            this.lblRFCReceptor.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lblRFCReceptor.Location = new System.Drawing.Point(99, 87);
            this.lblRFCReceptor.Name = "lblRFCReceptor";
            this.lblRFCReceptor.Size = new System.Drawing.Size(497, 23);
            this.lblRFCReceptor.TabIndex = 3;
            this.lblRFCReceptor.Text = "RFC de Receptor";
            this.lblRFCReceptor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNomReceptor
            // 
            this.lblNomReceptor.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lblNomReceptor.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomReceptor.Location = new System.Drawing.Point(98, 42);
            this.lblNomReceptor.Name = "lblNomReceptor";
            this.lblNomReceptor.Size = new System.Drawing.Size(498, 23);
            this.lblNomReceptor.TabIndex = 2;
            this.lblNomReceptor.Text = "Nombre de Receptor";
            this.lblNomReceptor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(51, 17);
            this.label10.TabIndex = 1;
            this.label10.Text = "R.F.C.:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(17, 42);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "Nombre:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lblTotal);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.lblDescuento);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.lblSubTot);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Location = new System.Drawing.Point(688, 215);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(543, 136);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = " Totale de la Factura: ";
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lblTotal.Location = new System.Drawing.Point(178, 84);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(199, 23);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(79, 87);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(93, 17);
            this.label17.TabIndex = 4;
            this.label17.Text = "Total Sin IVA:";
            // 
            // lblDescuento
            // 
            this.lblDescuento.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lblDescuento.Location = new System.Drawing.Point(356, 42);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(147, 23);
            this.lblDescuento.TabIndex = 3;
            this.lblDescuento.Text = "Descuento";
            this.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(258, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 17);
            this.label15.TabIndex = 2;
            this.label15.Text = "Descuento:";
            // 
            // lblSubTot
            // 
            this.lblSubTot.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lblSubTot.Location = new System.Drawing.Point(99, 41);
            this.lblSubTot.Name = "lblSubTot";
            this.lblSubTot.Size = new System.Drawing.Size(139, 23);
            this.lblSubTot.TabIndex = 1;
            this.lblSubTot.Text = "SubTotal";
            this.lblSubTot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(19, 41);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(74, 17);
            this.label13.TabIndex = 0;
            this.label13.Text = "Sub-Total:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
            this.dataGridView1.Location = new System.Drawing.Point(12, 369);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1219, 238);
            this.dataGridView1.TabIndex = 4;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Century Schoolbook", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label19.Location = new System.Drawing.Point(315, 15);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(590, 35);
            this.label19.TabIndex = 5;
            this.label19.Text = "Agregar productos mediante un Archivo XML";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnLoadXML
            // 
            this.btnLoadXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLoadXML.Font = new System.Drawing.Font("Century Schoolbook", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadXML.Image = global::PuntoDeVentaV2.Properties.Resources.file_code_o;
            this.btnLoadXML.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnLoadXML.Location = new System.Drawing.Point(537, 626);
            this.btnLoadXML.Name = "btnLoadXML";
            this.btnLoadXML.Size = new System.Drawing.Size(119, 58);
            this.btnLoadXML.TabIndex = 6;
            this.btnLoadXML.Text = "XML";
            this.btnLoadXML.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadXML.UseVisualStyleBackColor = false;
            this.btnLoadXML.Click += new System.EventHandler(this.btnLoadXML_Click);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "No:";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Descripcion";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Stock";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Precio";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Categoria";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Clave Interna";
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Codigo de Barra";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Clave del Producto";
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Unidad de Medida";
            this.Column9.Name = "Column9";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Descuento";
            this.Column10.Name = "Column10";
            // 
            // AgregarStockXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1248, 726);
            this.Controls.Add(this.btnLoadXML);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgregarStockXML";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Stock mediante archivo XML";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblFolio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFecha;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblNomEmisor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRFCEmisor;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblRFCReceptor;
        private System.Windows.Forms.Label lblNomReceptor;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblDescuento;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblSubTot;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnLoadXML;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
    }
}