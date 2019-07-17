﻿namespace PuntoDeVentaV2
{
    partial class Productos
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
            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.tituloBusqueda = new System.Windows.Forms.Label();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.cbOrden = new System.Windows.Forms.ComboBox();
            this.cbMostrar = new System.Windows.Forms.ComboBox();
            this.panelShowDGVProductosView = new System.Windows.Forms.Panel();
            this.DGVProductos = new System.Windows.Forms.DataGridView();
            this.CheckProducto = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column12 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Ajustar = new System.Windows.Forms.DataGridViewImageColumn();
            this._IDProducto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelShowPhotoView = new System.Windows.Forms.Panel();
            this.fLPShowPhoto = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbProductoComprado = new System.Windows.Forms.CheckBox();
            this.btnListView = new System.Windows.Forms.Button();
            this.btnPhotoView = new System.Windows.Forms.Button();
            this.btnModificarEstado = new System.Windows.Forms.Button();
            this.btnAgregarXML = new System.Windows.Forms.Button();
            this.TTipButtonText = new System.Windows.Forms.ToolTip(this.components);
            this.panelShowDGVProductosView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVProductos)).BeginInit();
            this.panelShowPhotoView.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAgregarProducto
            // 
            this.btnAgregarProducto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAgregarProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnAgregarProducto.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarProducto.FlatAppearance.BorderSize = 0;
            this.btnAgregarProducto.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnAgregarProducto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnAgregarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarProducto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarProducto.ForeColor = System.Drawing.Color.White;
            this.btnAgregarProducto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAgregarProducto.Location = new System.Drawing.Point(622, 125);
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.Size = new System.Drawing.Size(175, 27);
            this.btnAgregarProducto.TabIndex = 1;
            this.btnAgregarProducto.Text = "Agregar  producto +";
            this.btnAgregarProducto.UseVisualStyleBackColor = false;
            this.btnAgregarProducto.Click += new System.EventHandler(this.btnAgregarProducto_Click);
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(349, 8);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(137, 25);
            this.tituloSeccion.TabIndex = 3;
            this.tituloSeccion.Text = "PRODUCTOS";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tituloBusqueda
            // 
            this.tituloBusqueda.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloBusqueda.AutoSize = true;
            this.tituloBusqueda.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloBusqueda.Location = new System.Drawing.Point(292, 39);
            this.tituloBusqueda.Name = "tituloBusqueda";
            this.tituloBusqueda.Size = new System.Drawing.Size(264, 20);
            this.tituloBusqueda.TabIndex = 4;
            this.tituloBusqueda.Text = "Búsqueda avanzada de productos";
            this.tituloBusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusqueda.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusqueda.Location = new System.Drawing.Point(140, 67);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(595, 23);
            this.txtBusqueda.TabIndex = 5;
            this.txtBusqueda.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBusqueda.TextChanged += new System.EventHandler(this.txtBusqueda_TextChanged);
            // 
            // cbOrden
            // 
            this.cbOrden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbOrden.DisplayMember = "Prueba";
            this.cbOrden.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbOrden.FormattingEnabled = true;
            this.cbOrden.Items.AddRange(new object[] {
            "Ordenar por:",
            "A - Z",
            "Z - A",
            "Mayor precio",
            "Menor precio"});
            this.cbOrden.Location = new System.Drawing.Point(342, 127);
            this.cbOrden.Name = "cbOrden";
            this.cbOrden.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbOrden.Size = new System.Drawing.Size(137, 25);
            this.cbOrden.TabIndex = 6;
            this.cbOrden.SelectedIndexChanged += new System.EventHandler(this.cbOrden_SelectedIndexChanged);
            // 
            // cbMostrar
            // 
            this.cbMostrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMostrar.DisplayMember = "Prueba";
            this.cbMostrar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMostrar.FormattingEnabled = true;
            this.cbMostrar.Items.AddRange(new object[] {
            "Habilitados",
            "Deshabilitados",
            "Todos"});
            this.cbMostrar.Location = new System.Drawing.Point(486, 127);
            this.cbMostrar.Name = "cbMostrar";
            this.cbMostrar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbMostrar.Size = new System.Drawing.Size(131, 25);
            this.cbMostrar.TabIndex = 7;
            this.cbMostrar.SelectedIndexChanged += new System.EventHandler(this.cbMostrar_SelectedIndexChanged);
            // 
            // panelShowDGVProductosView
            // 
            this.panelShowDGVProductosView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelShowDGVProductosView.Controls.Add(this.DGVProductos);
            this.panelShowDGVProductosView.Location = new System.Drawing.Point(14, 196);
            this.panelShowDGVProductosView.Margin = new System.Windows.Forms.Padding(2);
            this.panelShowDGVProductosView.Name = "panelShowDGVProductosView";
            this.panelShowDGVProductosView.Size = new System.Drawing.Size(814, 356);
            this.panelShowDGVProductosView.TabIndex = 13;
            // 
            // DGVProductos
            // 
            this.DGVProductos.AllowUserToAddRows = false;
            this.DGVProductos.AllowUserToDeleteRows = false;
            this.DGVProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CheckProducto,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10,
            this.Column11,
            this.Column12,
            this.Column13,
            this.Column14,
            this.Column15,
            this.Column16,
            this.Ajustar,
            this._IDProducto});
            this.DGVProductos.Location = new System.Drawing.Point(10, 15);
            this.DGVProductos.Name = "DGVProductos";
            this.DGVProductos.RowHeadersVisible = false;
            this.DGVProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVProductos.Size = new System.Drawing.Size(792, 327);
            this.DGVProductos.TabIndex = 2;
            this.DGVProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVProductos_CellClick);
            this.DGVProductos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVProductos_CellContentClick);
            this.DGVProductos.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVProductos_CellMouseEnter);
            // 
            // CheckProducto
            // 
            this.CheckProducto.HeaderText = "";
            this.CheckProducto.Name = "CheckProducto";
            this.CheckProducto.Width = 30;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Nombre";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Stock";
            this.Column2.Name = "Column2";
            this.Column2.Width = 65;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Precio";
            this.Column3.Name = "Column3";
            this.Column3.Width = 65;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Categoria";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.HeaderText = "Clave";
            this.Column5.Name = "Column5";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Código";
            this.Column6.Name = "Column6";
            this.Column6.Width = 102;
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Editar";
            this.Column7.Name = "Column7";
            this.Column7.Width = 50;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Estado";
            this.Column8.Name = "Column8";
            this.Column8.Width = 50;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Historial";
            this.Column9.Name = "Column9";
            this.Column9.Width = 50;
            // 
            // Column10
            // 
            this.Column10.HeaderText = "Generar";
            this.Column10.Name = "Column10";
            this.Column10.Width = 50;
            // 
            // Column11
            // 
            this.Column11.HeaderText = "Imagen";
            this.Column11.Name = "Column11";
            this.Column11.Width = 50;
            // 
            // Column12
            // 
            this.Column12.HeaderText = "Etiqueta";
            this.Column12.Name = "Column12";
            this.Column12.Width = 50;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "Copiar";
            this.Column13.Name = "Column13";
            this.Column13.Width = 50;
            // 
            // Column14
            // 
            this.Column14.HeaderText = "Status";
            this.Column14.Name = "Column14";
            this.Column14.Visible = false;
            this.Column14.Width = 50;
            // 
            // Column15
            // 
            this.Column15.HeaderText = "ProdImage";
            this.Column15.Name = "Column15";
            this.Column15.Visible = false;
            this.Column15.Width = 50;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "Tipo";
            this.Column16.Name = "Column16";
            this.Column16.Width = 50;
            // 
            // Ajustar
            // 
            this.Ajustar.HeaderText = "Ajustar";
            this.Ajustar.Name = "Ajustar";
            this.Ajustar.Width = 50;
            // 
            // _IDProducto
            // 
            this._IDProducto.HeaderText = "ID";
            this._IDProducto.Name = "_IDProducto";
            this._IDProducto.Visible = false;
            // 
            // panelShowPhotoView
            // 
            this.panelShowPhotoView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelShowPhotoView.Controls.Add(this.fLPShowPhoto);
            this.panelShowPhotoView.Location = new System.Drawing.Point(12, 198);
            this.panelShowPhotoView.Margin = new System.Windows.Forms.Padding(2);
            this.panelShowPhotoView.Name = "panelShowPhotoView";
            this.panelShowPhotoView.Size = new System.Drawing.Size(814, 356);
            this.panelShowPhotoView.TabIndex = 14;
            // 
            // fLPShowPhoto
            // 
            this.fLPShowPhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fLPShowPhoto.AutoScroll = true;
            this.fLPShowPhoto.Location = new System.Drawing.Point(10, 15);
            this.fLPShowPhoto.Margin = new System.Windows.Forms.Padding(2);
            this.fLPShowPhoto.Name = "fLPShowPhoto";
            this.fLPShowPhoto.Size = new System.Drawing.Size(792, 327);
            this.fLPShowPhoto.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.cbProductoComprado);
            this.panel1.Controls.Add(this.cbMostrar);
            this.panel1.Controls.Add(this.tituloSeccion);
            this.panel1.Controls.Add(this.tituloBusqueda);
            this.panel1.Controls.Add(this.txtBusqueda);
            this.panel1.Controls.Add(this.btnListView);
            this.panel1.Controls.Add(this.btnAgregarProducto);
            this.panel1.Controls.Add(this.btnPhotoView);
            this.panel1.Controls.Add(this.cbOrden);
            this.panel1.Controls.Add(this.btnModificarEstado);
            this.panel1.Controls.Add(this.btnAgregarXML);
            this.panel1.Location = new System.Drawing.Point(12, 6);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(817, 184);
            this.panel1.TabIndex = 15;
            // 
            // cbProductoComprado
            // 
            this.cbProductoComprado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbProductoComprado.AutoSize = true;
            this.cbProductoComprado.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbProductoComprado.Location = new System.Drawing.Point(622, 159);
            this.cbProductoComprado.Name = "cbProductoComprado";
            this.cbProductoComprado.Size = new System.Drawing.Size(138, 20);
            this.cbProductoComprado.TabIndex = 13;
            this.cbProductoComprado.Text = "Producto comprado";
            this.cbProductoComprado.UseVisualStyleBackColor = true;
            this.cbProductoComprado.CheckedChanged += new System.EventHandler(this.cbProductoComprado_CheckedChanged);
            // 
            // btnListView
            // 
            this.btnListView.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnListView.Image = global::PuntoDeVentaV2.Properties.Resources.list;
            this.btnListView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnListView.Location = new System.Drawing.Point(12, 142);
            this.btnListView.Margin = new System.Windows.Forms.Padding(2);
            this.btnListView.Name = "btnListView";
            this.btnListView.Size = new System.Drawing.Size(136, 28);
            this.btnListView.TabIndex = 12;
            this.btnListView.Text = "Lista";
            this.btnListView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnListView.UseVisualStyleBackColor = true;
            this.btnListView.Click += new System.EventHandler(this.btnListView_Click);
            // 
            // btnPhotoView
            // 
            this.btnPhotoView.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPhotoView.Image = global::PuntoDeVentaV2.Properties.Resources.th;
            this.btnPhotoView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPhotoView.Location = new System.Drawing.Point(152, 142);
            this.btnPhotoView.Margin = new System.Windows.Forms.Padding(2);
            this.btnPhotoView.Name = "btnPhotoView";
            this.btnPhotoView.Size = new System.Drawing.Size(159, 28);
            this.btnPhotoView.TabIndex = 11;
            this.btnPhotoView.Text = "Mosaico";
            this.btnPhotoView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPhotoView.UseVisualStyleBackColor = true;
            this.btnPhotoView.Click += new System.EventHandler(this.btnPhotoView_Click);
            // 
            // btnModificarEstado
            // 
            this.btnModificarEstado.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModificarEstado.Image = global::PuntoDeVentaV2.Properties.Resources.cogs;
            this.btnModificarEstado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificarEstado.Location = new System.Drawing.Point(152, 110);
            this.btnModificarEstado.Margin = new System.Windows.Forms.Padding(2);
            this.btnModificarEstado.Name = "btnModificarEstado";
            this.btnModificarEstado.Size = new System.Drawing.Size(159, 28);
            this.btnModificarEstado.TabIndex = 10;
            this.btnModificarEstado.Text = "Modificar Estado";
            this.btnModificarEstado.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnModificarEstado.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnModificarEstado.UseVisualStyleBackColor = true;
            this.btnModificarEstado.Click += new System.EventHandler(this.btnModificarEstado_Click);
            // 
            // btnAgregarXML
            // 
            this.btnAgregarXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnAgregarXML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarXML.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarXML.Image = global::PuntoDeVentaV2.Properties.Resources.cart_plus;
            this.btnAgregarXML.Location = new System.Drawing.Point(12, 110);
            this.btnAgregarXML.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregarXML.Name = "btnAgregarXML";
            this.btnAgregarXML.Size = new System.Drawing.Size(136, 28);
            this.btnAgregarXML.TabIndex = 8;
            this.btnAgregarXML.Text = "Agregar XML";
            this.btnAgregarXML.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregarXML.UseVisualStyleBackColor = false;
            this.btnAgregarXML.Click += new System.EventHandler(this.btnAgregarXML_Click);
            // 
            // TTipButtonText
            // 
            this.TTipButtonText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.TTipButtonText.ForeColor = System.Drawing.Color.White;
            this.TTipButtonText.OwnerDraw = true;
            this.TTipButtonText.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.TTipButtonText_Draw);
            // 
            // Productos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 564);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelShowDGVProductosView);
            this.Controls.Add(this.panelShowPhotoView);
            this.Name = "Productos";
            this.Text = "Productos";
            this.Load += new System.EventHandler(this.Productos_Load);
            this.panelShowDGVProductosView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVProductos)).EndInit();
            this.panelShowPhotoView.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnAgregarProducto;
        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.Label tituloBusqueda;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.ComboBox cbOrden;
        private System.Windows.Forms.ComboBox cbMostrar;
        private System.Windows.Forms.Button btnAgregarXML;
        private System.Windows.Forms.Button btnModificarEstado;
        private System.Windows.Forms.Button btnPhotoView;
        private System.Windows.Forms.Button btnListView;
        private System.Windows.Forms.Panel panelShowDGVProductosView;
        private System.Windows.Forms.DataGridView DGVProductos;
        private System.Windows.Forms.Panel panelShowPhotoView;
        private System.Windows.Forms.FlowLayoutPanel fLPShowPhoto;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolTip TTipButtonText;
        private System.Windows.Forms.CheckBox cbProductoComprado;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CheckProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewImageColumn Column7;
        private System.Windows.Forms.DataGridViewImageColumn Column8;
        private System.Windows.Forms.DataGridViewImageColumn Column9;
        private System.Windows.Forms.DataGridViewImageColumn Column10;
        private System.Windows.Forms.DataGridViewImageColumn Column11;
        private System.Windows.Forms.DataGridViewImageColumn Column12;
        private System.Windows.Forms.DataGridViewImageColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewImageColumn Column16;
        private System.Windows.Forms.DataGridViewImageColumn Ajustar;
        private System.Windows.Forms.DataGridViewTextBoxColumn _IDProducto;
    }
}