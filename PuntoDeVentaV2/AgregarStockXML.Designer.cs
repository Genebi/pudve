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
            this.lblIVAFactura = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblDescuento = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblSubTot = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.DGVConceptoXMLFile = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label19 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.txtCodigoBarras = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnLoadXML = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVConceptoXMLFile)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(12, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(332, 126);
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
            this.lblFecha.Size = new System.Drawing.Size(217, 23);
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
            this.groupBox2.Location = new System.Drawing.Point(366, 63);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(608, 126);
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
            this.lblRFCEmisor.Size = new System.Drawing.Size(479, 23);
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
            this.lblNomEmisor.Size = new System.Drawing.Size(482, 23);
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
            this.groupBox3.Location = new System.Drawing.Point(12, 200);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(486, 136);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " Receptor: ";
            // 
            // lblRFCReceptor
            // 
            this.lblRFCReceptor.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lblRFCReceptor.Location = new System.Drawing.Point(99, 87);
            this.lblRFCReceptor.Name = "lblRFCReceptor";
            this.lblRFCReceptor.Size = new System.Drawing.Size(362, 23);
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
            this.lblNomReceptor.Size = new System.Drawing.Size(363, 23);
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
            this.groupBox4.Controls.Add(this.lblIVAFactura);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.lblTotal);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.lblDescuento);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.lblSubTot);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Location = new System.Drawing.Point(519, 200);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(455, 136);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = " Totale de la Factura: ";
            // 
            // lblIVAFactura
            // 
            this.lblIVAFactura.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lblIVAFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
            this.lblIVAFactura.Location = new System.Drawing.Point(99, 87);
            this.lblIVAFactura.Name = "lblIVAFactura";
            this.lblIVAFactura.Size = new System.Drawing.Size(108, 23);
            this.lblIVAFactura.TabIndex = 7;
            this.lblIVAFactura.Text = "IVA";
            this.lblIVAFactura.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "IVA:";
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(314, 84);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(116, 23);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Total";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(228, 87);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(44, 17);
            this.label17.TabIndex = 4;
            this.label17.Text = "Total:";
            // 
            // lblDescuento
            // 
            this.lblDescuento.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.lblDescuento.Location = new System.Drawing.Point(311, 42);
            this.lblDescuento.Name = "lblDescuento";
            this.lblDescuento.Size = new System.Drawing.Size(119, 23);
            this.lblDescuento.TabIndex = 3;
            this.lblDescuento.Text = "Descuento";
            this.lblDescuento.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(223, 44);
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
            this.lblSubTot.Size = new System.Drawing.Size(108, 23);
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
            // DGVConceptoXMLFile
            // 
            this.DGVConceptoXMLFile.AllowUserToAddRows = false;
            this.DGVConceptoXMLFile.AllowUserToDeleteRows = false;
            this.DGVConceptoXMLFile.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVConceptoXMLFile.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column6,
            this.Column9});
            this.DGVConceptoXMLFile.Location = new System.Drawing.Point(12, 503);
            this.DGVConceptoXMLFile.Name = "DGVConceptoXMLFile";
            this.DGVConceptoXMLFile.RowTemplate.Height = 24;
            this.DGVConceptoXMLFile.Size = new System.Drawing.Size(962, 238);
            this.DGVConceptoXMLFile.TabIndex = 4;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 2.644103F;
            this.Column1.HeaderText = "No:";
            this.Column1.Name = "Column1";
            this.Column1.Width = 35;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 28.28627F;
            this.Column2.HeaderText = "Descripcion";
            this.Column2.Name = "Column2";
            this.Column2.Width = 300;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 29.93935F;
            this.Column3.HeaderText = "Stock Entrante";
            this.Column3.Name = "Column3";
            this.Column3.Width = 80;
            // 
            // Column4
            // 
            this.Column4.FillWeight = 53.81427F;
            this.Column4.HeaderText = "Precio";
            this.Column4.Name = "Column4";
            this.Column4.Width = 80;
            // 
            // Column6
            // 
            this.Column6.FillWeight = 119.8337F;
            this.Column6.HeaderText = "Clave Interna";
            this.Column6.Name = "Column6";
            this.Column6.Width = 90;
            // 
            // Column9
            // 
            this.Column9.FillWeight = 365.4821F;
            this.Column9.HeaderText = "Unidad de Medida";
            this.Column9.Name = "Column9";
            this.Column9.Width = 90;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Century Schoolbook", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.label19.Location = new System.Drawing.Point(205, 17);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(590, 35);
            this.label19.TabIndex = 5;
            this.label19.Text = "Agregar productos mediante un Archivo XML";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel1.Controls.Add(this.textBox3);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.txtCodigoBarras);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(519, 348);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(455, 138);
            this.panel1.TabIndex = 7;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(154, 99);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(287, 22);
            this.textBox3.TabIndex = 9;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(93, 60);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(348, 22);
            this.textBox2.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.Image = global::PuntoDeVentaV2.Properties.Resources.barcode1;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(299, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(142, 37);
            this.button1.TabIndex = 7;
            this.button1.Text = "Generar Código";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtCodigoBarras
            // 
            this.txtCodigoBarras.Location = new System.Drawing.Point(135, 18);
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.Size = new System.Drawing.Size(155, 22);
            this.txtCodigoBarras.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 17);
            this.label8.TabIndex = 5;
            this.label8.Text = "Clave del Producto:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "Código de Barra:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Categoria:";
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Location = new System.Drawing.Point(245, 358);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(241, 128);
            this.groupBox5.TabIndex = 8;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Archvios XML";
            // 
            // label11
            // 
            this.label11.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(42, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(151, 61);
            this.label11.TabIndex = 0;
            this.label11.Text = "Click aquí para agregar XML";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label11.Click += new System.EventHandler(this.label11_Click);
            this.label11.MouseEnter += new System.EventHandler(this.label11_MouseEnter);
            this.label11.MouseLeave += new System.EventHandler(this.label11_MouseLeave);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Green;
            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button2.Font = new System.Drawing.Font("Century Schoolbook", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.button2.Image = global::PuntoDeVentaV2.Properties.Resources.save;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(12, 428);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(186, 58);
            this.button2.TabIndex = 9;
            this.button2.Text = "Guardar";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            // 
            // btnLoadXML
            // 
            this.btnLoadXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnLoadXML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLoadXML.Font = new System.Drawing.Font("Century Schoolbook", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLoadXML.Image = global::PuntoDeVentaV2.Properties.Resources.file_code_o;
            this.btnLoadXML.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLoadXML.Location = new System.Drawing.Point(12, 355);
            this.btnLoadXML.Name = "btnLoadXML";
            this.btnLoadXML.Size = new System.Drawing.Size(186, 58);
            this.btnLoadXML.TabIndex = 6;
            this.btnLoadXML.Text = "XML";
            this.btnLoadXML.UseVisualStyleBackColor = false;
            this.btnLoadXML.Click += new System.EventHandler(this.btnLoadXML_Click);
            // 
            // AgregarStockXML
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(988, 753);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnLoadXML);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.DGVConceptoXMLFile);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AgregarStockXML";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Agregar Stock mediante archivo XML";
            this.Load += new System.EventHandler(this.AgregarStockXML_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVConceptoXMLFile)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
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
        private System.Windows.Forms.DataGridView DGVConceptoXMLFile;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnLoadXML;
        private System.Windows.Forms.Label lblIVAFactura;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtCodigoBarras;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
    }
}