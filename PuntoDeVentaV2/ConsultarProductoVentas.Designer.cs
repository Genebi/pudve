namespace PuntoDeVentaV2
{
    partial class ConsultarProductoVentas
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
            this.DGVProductos = new System.Windows.Forms.DataGridView();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Proveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.linkLblUltimaPagina = new System.Windows.Forms.LinkLabel();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.btnUltimaPagina = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnPrimeraPagina = new System.Windows.Forms.Button();
            this.btnAnterior = new System.Windows.Forms.Button();
            this.linkLblPrimeraPagina = new System.Windows.Forms.LinkLabel();
            this.linkLblPaginaSiguiente = new System.Windows.Forms.LinkLabel();
            this.linkLblPaginaActual = new System.Windows.Forms.LinkLabel();
            this.linkLblPaginaAnterior = new System.Windows.Forms.LinkLabel();
            this.btnActualizarMaximoProductos = new System.Windows.Forms.Button();
            this.txtMaximoPorPagina = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCantidadRegistros = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CBTipo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProductos)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // DGVProductos
            // 
            this.DGVProductos.AllowUserToAddRows = false;
            this.DGVProductos.AllowUserToDeleteRows = false;
            this.DGVProductos.AllowUserToResizeColumns = false;
            this.DGVProductos.AllowUserToResizeRows = false;
            this.DGVProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.DGVProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.Stock,
            this.Precio,
            this.Clave,
            this.Codigo,
            this.Tipo,
            this.Proveedor,
            this._id});
            this.DGVProductos.Location = new System.Drawing.Point(12, 82);
            this.DGVProductos.Name = "DGVProductos";
            this.DGVProductos.ReadOnly = true;
            this.DGVProductos.RowHeadersVisible = false;
            this.DGVProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVProductos.Size = new System.Drawing.Size(1208, 377);
            this.DGVProductos.TabIndex = 1;
            this.DGVProductos.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DGVProductos_CellMouseDoubleClick);
            this.DGVProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DGVProductos_KeyDown);
            // 
            // Nombre
            // 
            this.Nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // Stock
            // 
            this.Stock.HeaderText = "Stock";
            this.Stock.Name = "Stock";
            this.Stock.ReadOnly = true;
            this.Stock.Width = 60;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            this.Precio.Width = 62;
            // 
            // Clave
            // 
            this.Clave.HeaderText = "Clave";
            this.Clave.Name = "Clave";
            this.Clave.ReadOnly = true;
            this.Clave.Width = 59;
            // 
            // Codigo
            // 
            this.Codigo.HeaderText = "Código";
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 65;
            // 
            // Tipo
            // 
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            this.Tipo.Width = 53;
            // 
            // Proveedor
            // 
            this.Proveedor.HeaderText = "Proveedor";
            this.Proveedor.Name = "Proveedor";
            this.Proveedor.ReadOnly = true;
            this.Proveedor.Width = 81;
            // 
            // _id
            // 
            this._id.HeaderText = "ID";
            this._id.Name = "_id";
            this._id.ReadOnly = true;
            this._id.Width = 43;
            // 
            // txtBuscar
            // 
            this.txtBuscar.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(289, 55);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(609, 21);
            this.txtBuscar.TabIndex = 0;
            this.txtBuscar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            this.txtBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBuscar_KeyDown);
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(463, 17);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(299, 25);
            this.tituloSeccion.TabIndex = 7;
            this.tituloSeccion.Text = "CONSULTAR PRODUCTOS";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel4);
            this.panel5.Controls.Add(this.panel3);
            this.panel5.Controls.Add(this.linkLblPaginaSiguiente);
            this.panel5.Controls.Add(this.linkLblPaginaActual);
            this.panel5.Controls.Add(this.linkLblPaginaAnterior);
            this.panel5.Controls.Add(this.btnActualizarMaximoProductos);
            this.panel5.Controls.Add(this.txtMaximoPorPagina);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.lblCantidadRegistros);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Location = new System.Drawing.Point(12, 484);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(1208, 43);
            this.panel5.TabIndex = 38;
            // 
            // panel4
            // 
            this.panel4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel4.Controls.Add(this.linkLblUltimaPagina);
            this.panel4.Controls.Add(this.btnSiguiente);
            this.panel4.Controls.Add(this.btnUltimaPagina);
            this.panel4.Location = new System.Drawing.Point(590, 6);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(59, 32);
            this.panel4.TabIndex = 46;
            // 
            // linkLblUltimaPagina
            // 
            this.linkLblUltimaPagina.AutoSize = true;
            this.linkLblUltimaPagina.Location = new System.Drawing.Point(60, 10);
            this.linkLblUltimaPagina.Name = "linkLblUltimaPagina";
            this.linkLblUltimaPagina.Size = new System.Drawing.Size(25, 13);
            this.linkLblUltimaPagina.TabIndex = 17;
            this.linkLblUltimaPagina.TabStop = true;
            this.linkLblUltimaPagina.Text = "100";
            this.linkLblUltimaPagina.Visible = false;
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnSiguiente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSiguiente.FlatAppearance.BorderSize = 0;
            this.btnSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSiguiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSiguiente.Image = global::PuntoDeVentaV2.Properties.Resources.angle_right;
            this.btnSiguiente.Location = new System.Drawing.Point(6, 6);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(20, 20);
            this.btnSiguiente.TabIndex = 11;
            this.btnSiguiente.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnSiguiente.UseVisualStyleBackColor = false;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // btnUltimaPagina
            // 
            this.btnUltimaPagina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnUltimaPagina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnUltimaPagina.FlatAppearance.BorderSize = 0;
            this.btnUltimaPagina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUltimaPagina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUltimaPagina.Image = global::PuntoDeVentaV2.Properties.Resources.angle_double_right;
            this.btnUltimaPagina.Location = new System.Drawing.Point(31, 6);
            this.btnUltimaPagina.Name = "btnUltimaPagina";
            this.btnUltimaPagina.Size = new System.Drawing.Size(20, 20);
            this.btnUltimaPagina.TabIndex = 12;
            this.btnUltimaPagina.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnUltimaPagina.UseVisualStyleBackColor = false;
            this.btnUltimaPagina.Click += new System.EventHandler(this.btnUltimaPagina_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel3.Controls.Add(this.btnPrimeraPagina);
            this.panel3.Controls.Add(this.btnAnterior);
            this.panel3.Controls.Add(this.linkLblPrimeraPagina);
            this.panel3.Location = new System.Drawing.Point(456, 6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(56, 32);
            this.panel3.TabIndex = 45;
            // 
            // btnPrimeraPagina
            // 
            this.btnPrimeraPagina.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnPrimeraPagina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrimeraPagina.FlatAppearance.BorderSize = 0;
            this.btnPrimeraPagina.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrimeraPagina.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrimeraPagina.Image = global::PuntoDeVentaV2.Properties.Resources.angle_double_left;
            this.btnPrimeraPagina.Location = new System.Drawing.Point(6, 6);
            this.btnPrimeraPagina.Name = "btnPrimeraPagina";
            this.btnPrimeraPagina.Size = new System.Drawing.Size(20, 20);
            this.btnPrimeraPagina.TabIndex = 9;
            this.btnPrimeraPagina.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrimeraPagina.UseVisualStyleBackColor = false;
            this.btnPrimeraPagina.Click += new System.EventHandler(this.btnPrimeraPagina_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAnterior.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAnterior.FlatAppearance.BorderSize = 0;
            this.btnAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAnterior.Image = global::PuntoDeVentaV2.Properties.Resources.angle_left;
            this.btnAnterior.Location = new System.Drawing.Point(31, 6);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.Size = new System.Drawing.Size(20, 20);
            this.btnAnterior.TabIndex = 10;
            this.btnAnterior.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAnterior.UseVisualStyleBackColor = false;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // linkLblPrimeraPagina
            // 
            this.linkLblPrimeraPagina.AutoSize = true;
            this.linkLblPrimeraPagina.Location = new System.Drawing.Point(38, 10);
            this.linkLblPrimeraPagina.Name = "linkLblPrimeraPagina";
            this.linkLblPrimeraPagina.Size = new System.Drawing.Size(13, 13);
            this.linkLblPrimeraPagina.TabIndex = 13;
            this.linkLblPrimeraPagina.TabStop = true;
            this.linkLblPrimeraPagina.Text = "1";
            this.linkLblPrimeraPagina.Visible = false;
            // 
            // linkLblPaginaSiguiente
            // 
            this.linkLblPaginaSiguiente.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkLblPaginaSiguiente.AutoSize = true;
            this.linkLblPaginaSiguiente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLblPaginaSiguiente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblPaginaSiguiente.Location = new System.Drawing.Point(567, 16);
            this.linkLblPaginaSiguiente.Name = "linkLblPaginaSiguiente";
            this.linkLblPaginaSiguiente.Size = new System.Drawing.Size(15, 16);
            this.linkLblPaginaSiguiente.TabIndex = 44;
            this.linkLblPaginaSiguiente.TabStop = true;
            this.linkLblPaginaSiguiente.Text = "4";
            this.linkLblPaginaSiguiente.Click += new System.EventHandler(this.linkLblPaginaSiguiente_Click);
            // 
            // linkLblPaginaActual
            // 
            this.linkLblPaginaActual.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkLblPaginaActual.AutoSize = true;
            this.linkLblPaginaActual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLblPaginaActual.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblPaginaActual.Location = new System.Drawing.Point(544, 16);
            this.linkLblPaginaActual.Name = "linkLblPaginaActual";
            this.linkLblPaginaActual.Size = new System.Drawing.Size(15, 16);
            this.linkLblPaginaActual.TabIndex = 43;
            this.linkLblPaginaActual.TabStop = true;
            this.linkLblPaginaActual.Text = "3";
            this.linkLblPaginaActual.Click += new System.EventHandler(this.linkLblPaginaActual_Click);
            // 
            // linkLblPaginaAnterior
            // 
            this.linkLblPaginaAnterior.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.linkLblPaginaAnterior.AutoSize = true;
            this.linkLblPaginaAnterior.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkLblPaginaAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLblPaginaAnterior.Location = new System.Drawing.Point(520, 16);
            this.linkLblPaginaAnterior.Name = "linkLblPaginaAnterior";
            this.linkLblPaginaAnterior.Size = new System.Drawing.Size(15, 16);
            this.linkLblPaginaAnterior.TabIndex = 42;
            this.linkLblPaginaAnterior.TabStop = true;
            this.linkLblPaginaAnterior.Text = "2";
            this.linkLblPaginaAnterior.Click += new System.EventHandler(this.linkLblPaginaAnterior_Click);
            // 
            // btnActualizarMaximoProductos
            // 
            this.btnActualizarMaximoProductos.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnActualizarMaximoProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnActualizarMaximoProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizarMaximoProductos.FlatAppearance.BorderSize = 0;
            this.btnActualizarMaximoProductos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarMaximoProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnActualizarMaximoProductos.ForeColor = System.Drawing.Color.Black;
            this.btnActualizarMaximoProductos.Location = new System.Drawing.Point(856, 11);
            this.btnActualizarMaximoProductos.Name = "btnActualizarMaximoProductos";
            this.btnActualizarMaximoProductos.Size = new System.Drawing.Size(80, 23);
            this.btnActualizarMaximoProductos.TabIndex = 41;
            this.btnActualizarMaximoProductos.Text = "Actualizar";
            this.btnActualizarMaximoProductos.UseVisualStyleBackColor = false;
            this.btnActualizarMaximoProductos.Click += new System.EventHandler(this.btnActualizarMaximoProductos_Click);
            // 
            // txtMaximoPorPagina
            // 
            this.txtMaximoPorPagina.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtMaximoPorPagina.Location = new System.Drawing.Point(790, 12);
            this.txtMaximoPorPagina.Name = "txtMaximoPorPagina";
            this.txtMaximoPorPagina.Size = new System.Drawing.Size(56, 20);
            this.txtMaximoPorPagina.TabIndex = 40;
            this.txtMaximoPorPagina.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMaximoPorPagina.Click += new System.EventHandler(this.txtMaximoPorPagina_Click);
            this.txtMaximoPorPagina.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaximoPorPagina_KeyDown);
            this.txtMaximoPorPagina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtMaximoPorPagina_KeyPress);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.Location = new System.Drawing.Point(663, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 32);
            this.label7.TabIndex = 39;
            this.label7.Text = "Cantidad de productos para mostrar: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCantidadRegistros
            // 
            this.lblCantidadRegistros.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCantidadRegistros.BackColor = System.Drawing.SystemColors.Control;
            this.lblCantidadRegistros.ForeColor = System.Drawing.Color.Blue;
            this.lblCantidadRegistros.Location = new System.Drawing.Point(387, 11);
            this.lblCantidadRegistros.Name = "lblCantidadRegistros";
            this.lblCantidadRegistros.Size = new System.Drawing.Size(62, 23);
            this.lblCantidadRegistros.TabIndex = 38;
            this.lblCantidadRegistros.Text = "0";
            this.lblCantidadRegistros.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCantidadRegistros.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(283, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 26);
            this.label4.TabIndex = 37;
            this.label4.Text = "Total de productos \r\nencontrados:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Visible = false;
            // 
            // CBTipo
            // 
            this.CBTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBTipo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CBTipo.FormattingEnabled = true;
            this.CBTipo.Items.AddRange(new object[] {
            "Todos",
            "Productos",
            "Servicios",
            "Combos"});
            this.CBTipo.Location = new System.Drawing.Point(45, 55);
            this.CBTipo.Name = "CBTipo";
            this.CBTipo.Size = new System.Drawing.Size(121, 21);
            this.CBTipo.TabIndex = 39;
            this.CBTipo.TextChanged += new System.EventHandler(this.CBTipo_TextChanged);
            // 
            // ConsultarProductoVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 540);
            this.Controls.Add(this.CBTipo);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.tituloSeccion);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.DGVProductos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConsultarProductoVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ConsultarProductoVentas_Load);
            this.Shown += new System.EventHandler(this.ConsultarProductoVentas_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConsultarProductoVentas_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.DGVProductos)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVProductos;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.LinkLabel linkLblUltimaPagina;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Button btnUltimaPagina;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnPrimeraPagina;
        private System.Windows.Forms.Button btnAnterior;
        private System.Windows.Forms.LinkLabel linkLblPrimeraPagina;
        private System.Windows.Forms.LinkLabel linkLblPaginaSiguiente;
        private System.Windows.Forms.LinkLabel linkLblPaginaActual;
        private System.Windows.Forms.LinkLabel linkLblPaginaAnterior;
        private System.Windows.Forms.Button btnActualizarMaximoProductos;
        private System.Windows.Forms.TextBox txtMaximoPorPagina;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCantidadRegistros;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Proveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn _id;
        private System.Windows.Forms.ComboBox CBTipo;
    }
}