namespace PuntoDeVentaV2
{
    partial class RevisarInventario
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBusqueda = new System.Windows.Forms.Button();
            this.txtBoxBuscarCodigoBarras = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnVerCBExtra = new System.Windows.Forms.Button();
            this.txtCodigoBarras = new System.Windows.Forms.TextBox();
            this.txtNombreProducto = new System.Windows.Forms.TextBox();
            this.lblStockMaximo = new System.Windows.Forms.Label();
            this.lblStockMinimo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnOmitir = new System.Windows.Forms.Button();
            this.btnTerminar = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblPrecioProducto = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblNoRegistro = new System.Windows.Forms.Label();
            this.txtCantidadStock = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lCodigoClave = new System.Windows.Forms.Label();
            this.btnAumentarStock = new System.Windows.Forms.Button();
            this.btnReducirStock = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbBackground = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNoRevision = new System.Windows.Forms.Label();
            this.timerBusqueda = new System.Windows.Forms.Timer(this.components);
            this.lbCantidadFiltro = new System.Windows.Forms.Label();
            this.btnDeshabilitarProducto = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBusqueda);
            this.groupBox1.Controls.Add(this.txtBoxBuscarCodigoBarras);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 74);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Código de Barras:";
            // 
            // btnBusqueda
            // 
            this.btnBusqueda.BackgroundImage = global::PuntoDeVentaV2.Properties.Resources.search;
            this.btnBusqueda.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnBusqueda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBusqueda.Location = new System.Drawing.Point(390, 23);
            this.btnBusqueda.Name = "btnBusqueda";
            this.btnBusqueda.Size = new System.Drawing.Size(30, 32);
            this.btnBusqueda.TabIndex = 1;
            this.btnBusqueda.UseVisualStyleBackColor = true;
            this.btnBusqueda.Click += new System.EventHandler(this.btnBusqueda_Click);
            // 
            // txtBoxBuscarCodigoBarras
            // 
            this.txtBoxBuscarCodigoBarras.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtBoxBuscarCodigoBarras.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxBuscarCodigoBarras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtBoxBuscarCodigoBarras.Location = new System.Drawing.Point(40, 22);
            this.txtBoxBuscarCodigoBarras.Name = "txtBoxBuscarCodigoBarras";
            this.txtBoxBuscarCodigoBarras.Size = new System.Drawing.Size(344, 31);
            this.txtBoxBuscarCodigoBarras.TabIndex = 0;
            this.txtBoxBuscarCodigoBarras.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBoxBuscarCodigoBarras.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBoxBuscarCodigoBarras_KeyDown);
            this.txtBoxBuscarCodigoBarras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxBuscarCodigoBarras_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.AutoSize = true;
            this.groupBox2.Controls.Add(this.btnVerCBExtra);
            this.groupBox2.Controls.Add(this.txtCodigoBarras);
            this.groupBox2.Controls.Add(this.txtNombreProducto);
            this.groupBox2.Controls.Add(this.lblStockMaximo);
            this.groupBox2.Controls.Add(this.lblStockMinimo);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblPrecioProducto);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.lblNoRegistro);
            this.groupBox2.Controls.Add(this.txtCantidadStock);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.lCodigoClave);
            this.groupBox2.Controls.Add(this.btnAumentarStock);
            this.groupBox2.Controls.Add(this.btnReducirStock);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lbBackground);
            this.groupBox2.Location = new System.Drawing.Point(12, 131);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(426, 512);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // btnVerCBExtra
            // 
            this.btnVerCBExtra.BackgroundImage = global::PuntoDeVentaV2.Properties.Resources.info_circle;
            this.btnVerCBExtra.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnVerCBExtra.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVerCBExtra.Location = new System.Drawing.Point(196, 130);
            this.btnVerCBExtra.Name = "btnVerCBExtra";
            this.btnVerCBExtra.Size = new System.Drawing.Size(24, 32);
            this.btnVerCBExtra.TabIndex = 20;
            this.btnVerCBExtra.UseVisualStyleBackColor = true;
            this.btnVerCBExtra.Click += new System.EventHandler(this.btnVerCBExtra_Click);
            // 
            // txtCodigoBarras
            // 
            this.txtCodigoBarras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtCodigoBarras.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCodigoBarras.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoBarras.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.txtCodigoBarras.Location = new System.Drawing.Point(18, 137);
            this.txtCodigoBarras.Name = "txtCodigoBarras";
            this.txtCodigoBarras.ReadOnly = true;
            this.txtCodigoBarras.Size = new System.Drawing.Size(176, 19);
            this.txtCodigoBarras.TabIndex = 18;
            this.txtCodigoBarras.TabStop = false;
            this.txtCodigoBarras.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtNombreProducto
            // 
            this.txtNombreProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
            this.txtNombreProducto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNombreProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreProducto.ForeColor = System.Drawing.Color.White;
            this.txtNombreProducto.Location = new System.Drawing.Point(18, 44);
            this.txtNombreProducto.Multiline = true;
            this.txtNombreProducto.Name = "txtNombreProducto";
            this.txtNombreProducto.ReadOnly = true;
            this.txtNombreProducto.Size = new System.Drawing.Size(387, 64);
            this.txtNombreProducto.TabIndex = 17;
            this.txtNombreProducto.TabStop = false;
            this.txtNombreProducto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblStockMaximo
            // 
            this.lblStockMaximo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblStockMaximo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockMaximo.Location = new System.Drawing.Point(262, 201);
            this.lblStockMaximo.Name = "lblStockMaximo";
            this.lblStockMaximo.Size = new System.Drawing.Size(134, 32);
            this.lblStockMaximo.TabIndex = 16;
            this.lblStockMaximo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStockMinimo
            // 
            this.lblStockMinimo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblStockMinimo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStockMinimo.Location = new System.Drawing.Point(18, 201);
            this.lblStockMinimo.Name = "lblStockMinimo";
            this.lblStockMinimo.Size = new System.Drawing.Size(176, 32);
            this.lblStockMinimo.TabIndex = 15;
            this.lblStockMinimo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(263, 180);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Stock Maximo:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnOmitir);
            this.groupBox3.Controls.Add(this.btnTerminar);
            this.groupBox3.Controls.Add(this.btnAnterior);
            this.groupBox3.Controls.Add(this.btnSiguiente);
            this.groupBox3.Location = new System.Drawing.Point(0, 358);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(420, 135);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // btnOmitir
            // 
            this.btnOmitir.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOmitir.Location = new System.Drawing.Point(23, 83);
            this.btnOmitir.Name = "btnOmitir";
            this.btnOmitir.Size = new System.Drawing.Size(171, 48);
            this.btnOmitir.TabIndex = 8;
            this.btnOmitir.Text = "Omitir";
            this.btnOmitir.UseVisualStyleBackColor = true;
            this.btnOmitir.Click += new System.EventHandler(this.btnOmitir_Click);
            // 
            // btnTerminar
            // 
            this.btnTerminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTerminar.ForeColor = System.Drawing.Color.Red;
            this.btnTerminar.Location = new System.Drawing.Point(225, 83);
            this.btnTerminar.Name = "btnTerminar";
            this.btnTerminar.Size = new System.Drawing.Size(171, 48);
            this.btnTerminar.TabIndex = 5;
            this.btnTerminar.Text = "Terminar";
            this.btnTerminar.UseVisualStyleBackColor = true;
            this.btnTerminar.Click += new System.EventHandler(this.btnTerminar_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnterior.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAnterior.Location = new System.Drawing.Point(23, 19);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(171, 48);
            this.btnAnterior.TabIndex = 5;
            this.btnAnterior.Text = "Anterior";
            this.btnAnterior.UseVisualStyleBackColor = true;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSiguiente.Location = new System.Drawing.Point(225, 19);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(171, 48);
            this.btnSiguiente.TabIndex = 4;
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Stock Minimo:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(240, 137);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(19, 20);
            this.label4.TabIndex = 12;
            this.label4.Text = "$";
            // 
            // lblPrecioProducto
            // 
            this.lblPrecioProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblPrecioProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrecioProducto.ForeColor = System.Drawing.Color.DarkRed;
            this.lblPrecioProducto.Location = new System.Drawing.Point(262, 130);
            this.lblPrecioProducto.Name = "lblPrecioProducto";
            this.lblPrecioProducto.Size = new System.Drawing.Size(134, 32);
            this.lblPrecioProducto.TabIndex = 11;
            this.lblPrecioProducto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(263, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Precio:";
            // 
            // lblNoRegistro
            // 
            this.lblNoRegistro.AutoSize = true;
            this.lblNoRegistro.Location = new System.Drawing.Point(270, 22);
            this.lblNoRegistro.Name = "lblNoRegistro";
            this.lblNoRegistro.Size = new System.Drawing.Size(0, 13);
            this.lblNoRegistro.TabIndex = 9;
            this.lblNoRegistro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtCantidadStock
            // 
            this.txtCantidadStock.Font = new System.Drawing.Font("Microsoft Sans Serif", 45.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadStock.ForeColor = System.Drawing.Color.Blue;
            this.txtCantidadStock.Location = new System.Drawing.Point(99, 268);
            this.txtCantidadStock.Name = "txtCantidadStock";
            this.txtCantidadStock.Size = new System.Drawing.Size(225, 77);
            this.txtCantidadStock.TabIndex = 1;
            this.txtCantidadStock.Text = "0";
            this.txtCantidadStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCantidadStock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCantidadStock_KeyDown);
            this.txtCantidadStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadStock_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 246);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Stock Existente:";
            // 
            // lCodigoClave
            // 
            this.lCodigoClave.AutoSize = true;
            this.lCodigoClave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lCodigoClave.Location = new System.Drawing.Point(15, 111);
            this.lCodigoClave.Name = "lCodigoClave";
            this.lCodigoClave.Size = new System.Drawing.Size(166, 13);
            this.lCodigoClave.TabIndex = 6;
            this.lCodigoClave.Text = "Código de Barras o Clave Interna:";
            // 
            // btnAumentarStock
            // 
            this.btnAumentarStock.Image = global::PuntoDeVentaV2.Properties.Resources.plus_square1;
            this.btnAumentarStock.Location = new System.Drawing.Point(330, 267);
            this.btnAumentarStock.Name = "btnAumentarStock";
            this.btnAumentarStock.Size = new System.Drawing.Size(75, 85);
            this.btnAumentarStock.TabIndex = 3;
            this.btnAumentarStock.UseVisualStyleBackColor = true;
            this.btnAumentarStock.Click += new System.EventHandler(this.btnAumentarStock_Click);
            // 
            // btnReducirStock
            // 
            this.btnReducirStock.Image = global::PuntoDeVentaV2.Properties.Resources.minus_square1;
            this.btnReducirStock.Location = new System.Drawing.Point(18, 267);
            this.btnReducirStock.Name = "btnReducirStock";
            this.btnReducirStock.Size = new System.Drawing.Size(75, 85);
            this.btnReducirStock.TabIndex = 2;
            this.btnReducirStock.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnReducirStock.UseVisualStyleBackColor = true;
            this.btnReducirStock.Click += new System.EventHandler(this.btnReducirStock_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre : ";
            // 
            // lbBackground
            // 
            this.lbBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lbBackground.Location = new System.Drawing.Point(18, 130);
            this.lbBackground.Name = "lbBackground";
            this.lbBackground.Size = new System.Drawing.Size(176, 32);
            this.lbBackground.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(164, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Número de Revisión";
            // 
            // lblNoRevision
            // 
            this.lblNoRevision.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblNoRevision.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoRevision.ForeColor = System.Drawing.Color.Blue;
            this.lblNoRevision.Location = new System.Drawing.Point(190, 30);
            this.lblNoRevision.Name = "lblNoRevision";
            this.lblNoRevision.Size = new System.Drawing.Size(64, 23);
            this.lblNoRevision.TabIndex = 4;
            this.lblNoRevision.Text = "0";
            this.lblNoRevision.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbCantidadFiltro
            // 
            this.lbCantidadFiltro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCantidadFiltro.ForeColor = System.Drawing.Color.Red;
            this.lbCantidadFiltro.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lbCantidadFiltro.Location = new System.Drawing.Point(305, 28);
            this.lbCantidadFiltro.Name = "lbCantidadFiltro";
            this.lbCantidadFiltro.Size = new System.Drawing.Size(127, 32);
            this.lbCantidadFiltro.TabIndex = 5;
            this.lbCantidadFiltro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDeshabilitarProducto
            // 
            this.btnDeshabilitarProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeshabilitarProducto.Image = global::PuntoDeVentaV2.Properties.Resources.trash;
            this.btnDeshabilitarProducto.Location = new System.Drawing.Point(407, 5);
            this.btnDeshabilitarProducto.Name = "btnDeshabilitarProducto";
            this.btnDeshabilitarProducto.Size = new System.Drawing.Size(25, 23);
            this.btnDeshabilitarProducto.TabIndex = 6;
            this.toolTip1.SetToolTip(this.btnDeshabilitarProducto, "Deshabilitar Producto");
            this.btnDeshabilitarProducto.UseVisualStyleBackColor = true;
            this.btnDeshabilitarProducto.Click += new System.EventHandler(this.btnDeshabilitarProducto_Click);
            // 
            // RevisarInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 632);
            this.Controls.Add(this.btnDeshabilitarProducto);
            this.Controls.Add(this.lbCantidadFiltro);
            this.Controls.Add(this.lblNoRevision);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RevisarInventario";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Revisar Stock Fisico";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RevisarInventario_FormClosing);
            this.Load += new System.EventHandler(this.RevisarInventario_Load);
            this.Shown += new System.EventHandler(this.RevisarInventario_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RevisarInventario_KeyDown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnReducirStock;
        private System.Windows.Forms.Button btnAumentarStock;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lCodigoClave;
        private System.Windows.Forms.TextBox txtCantidadStock;
        private System.Windows.Forms.Button btnTerminar;
        private System.Windows.Forms.Label lblNoRegistro;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNoRevision;
        private System.Windows.Forms.Timer timerBusqueda;
        private System.Windows.Forms.Label lblPrecioProducto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblStockMaximo;
        private System.Windows.Forms.Label lblStockMinimo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbCantidadFiltro;
        private System.Windows.Forms.TextBox txtNombreProducto;
        private System.Windows.Forms.TextBox txtCodigoBarras;
        private System.Windows.Forms.Label lbBackground;
        private System.Windows.Forms.Button btnVerCBExtra;
        private System.Windows.Forms.Button btnDeshabilitarProducto;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.TextBox txtBoxBuscarCodigoBarras;
        private System.Windows.Forms.Button btnBusqueda;
        private System.Windows.Forms.Button btnOmitir;
    }
}