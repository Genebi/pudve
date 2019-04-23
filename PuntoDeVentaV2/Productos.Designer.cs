namespace PuntoDeVentaV2
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
            this.btnAgregarProducto = new System.Windows.Forms.Button();
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.tituloBusqueda = new System.Windows.Forms.Label();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.cbOrden = new System.Windows.Forms.ComboBox();
            this.cbMostrar = new System.Windows.Forms.ComboBox();
            this.btnListView = new System.Windows.Forms.Button();
            this.btnPhotoView = new System.Windows.Forms.Button();
            this.btnModificarEstado = new System.Windows.Forms.Button();
            this.btnAgregarXML = new System.Windows.Forms.Button();
            this.panelShowDGVProductosView = new System.Windows.Forms.Panel();
            this.DGVProductos = new System.Windows.Forms.DataGridView();
            this.chk = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panelShowPhotoView = new System.Windows.Forms.Panel();
            this.fLPShowPhoto = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.btnAgregarProducto.Location = new System.Drawing.Point(830, 154);
            this.btnAgregarProducto.Margin = new System.Windows.Forms.Padding(4);
            this.btnAgregarProducto.Name = "btnAgregarProducto";
            this.btnAgregarProducto.Size = new System.Drawing.Size(233, 33);
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
            this.tituloSeccion.Location = new System.Drawing.Point(465, 10);
            this.tituloSeccion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(175, 32);
            this.tituloSeccion.TabIndex = 3;
            this.tituloSeccion.Text = "PRODUCTOS";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tituloBusqueda
            // 
            this.tituloBusqueda.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloBusqueda.AutoSize = true;
            this.tituloBusqueda.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloBusqueda.Location = new System.Drawing.Point(389, 48);
            this.tituloBusqueda.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.tituloBusqueda.Name = "tituloBusqueda";
            this.tituloBusqueda.Size = new System.Drawing.Size(335, 22);
            this.tituloBusqueda.TabIndex = 4;
            this.tituloBusqueda.Text = "Búsqueda avanzada de productos";
            this.tituloBusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusqueda.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusqueda.Location = new System.Drawing.Point(187, 83);
            this.txtBusqueda.Margin = new System.Windows.Forms.Padding(4);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(792, 27);
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
            this.cbOrden.Location = new System.Drawing.Point(456, 156);
            this.cbOrden.Margin = new System.Windows.Forms.Padding(4);
            this.cbOrden.Name = "cbOrden";
            this.cbOrden.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbOrden.Size = new System.Drawing.Size(181, 29);
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
            this.cbMostrar.Location = new System.Drawing.Point(648, 156);
            this.cbMostrar.Margin = new System.Windows.Forms.Padding(4);
            this.cbMostrar.Name = "cbMostrar";
            this.cbMostrar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cbMostrar.Size = new System.Drawing.Size(173, 29);
            this.cbMostrar.TabIndex = 7;
            this.cbMostrar.SelectedIndexChanged += new System.EventHandler(this.cbMostrar_SelectedIndexChanged);
            // 
            // btnListView
            // 
            this.btnListView.Font = new System.Drawing.Font("Century Gothic", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnListView.Image = global::PuntoDeVentaV2.Properties.Resources.list;
            this.btnListView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnListView.Location = new System.Drawing.Point(16, 175);
            this.btnListView.Name = "btnListView";
            this.btnListView.Size = new System.Drawing.Size(181, 34);
            this.btnListView.TabIndex = 12;
            this.btnListView.Text = "Lista";
            this.btnListView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnListView.UseVisualStyleBackColor = true;
            this.btnListView.Click += new System.EventHandler(this.btnListView_Click);
            // 
            // btnPhotoView
            // 
            this.btnPhotoView.Font = new System.Drawing.Font("Century Gothic", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnPhotoView.Image = global::PuntoDeVentaV2.Properties.Resources.th;
            this.btnPhotoView.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPhotoView.Location = new System.Drawing.Point(203, 175);
            this.btnPhotoView.Name = "btnPhotoView";
            this.btnPhotoView.Size = new System.Drawing.Size(212, 34);
            this.btnPhotoView.TabIndex = 11;
            this.btnPhotoView.Text = "Mosaico";
            this.btnPhotoView.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPhotoView.UseVisualStyleBackColor = true;
            this.btnPhotoView.Click += new System.EventHandler(this.btnPhotoView_Click);
            // 
            // btnModificarEstado
            // 
            this.btnModificarEstado.Font = new System.Drawing.Font("Century Gothic", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.btnModificarEstado.Image = global::PuntoDeVentaV2.Properties.Resources.cogs;
            this.btnModificarEstado.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnModificarEstado.Location = new System.Drawing.Point(203, 135);
            this.btnModificarEstado.Name = "btnModificarEstado";
            this.btnModificarEstado.Size = new System.Drawing.Size(212, 35);
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
            this.btnAgregarXML.Font = new System.Drawing.Font("Century Gothic", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarXML.Image = global::PuntoDeVentaV2.Properties.Resources.cart_plus;
            this.btnAgregarXML.Location = new System.Drawing.Point(16, 135);
            this.btnAgregarXML.Name = "btnAgregarXML";
            this.btnAgregarXML.Size = new System.Drawing.Size(181, 34);
            this.btnAgregarXML.TabIndex = 8;
            this.btnAgregarXML.Text = "Agregar XML";
            this.btnAgregarXML.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAgregarXML.UseVisualStyleBackColor = false;
            this.btnAgregarXML.Click += new System.EventHandler(this.btnAgregarXML_Click);
            // 
            // panelShowDGVProductosView
            // 
            this.panelShowDGVProductosView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelShowDGVProductosView.Controls.Add(this.DGVProductos);
            this.panelShowDGVProductosView.Location = new System.Drawing.Point(19, 241);
            this.panelShowDGVProductosView.Name = "panelShowDGVProductosView";
            this.panelShowDGVProductosView.Size = new System.Drawing.Size(1086, 438);
            this.panelShowDGVProductosView.TabIndex = 13;
            // 
            // DGVProductos
            // 
            this.DGVProductos.AllowUserToAddRows = false;
            this.DGVProductos.AllowUserToDeleteRows = false;
            this.DGVProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chk});
            this.DGVProductos.Location = new System.Drawing.Point(14, 18);
            this.DGVProductos.Margin = new System.Windows.Forms.Padding(4);
            this.DGVProductos.MultiSelect = false;
            this.DGVProductos.Name = "DGVProductos";
            this.DGVProductos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVProductos.Size = new System.Drawing.Size(1056, 403);
            this.DGVProductos.TabIndex = 2;
            this.DGVProductos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVProductos_CellClick_1);
            this.DGVProductos.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVProductos_CellContentClick_1);
            this.DGVProductos.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVProductos_CellMouseEnter_1);
            this.DGVProductos.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DGVProductos_CellPainting_1);
            // 
            // chk
            // 
            this.chk.HeaderText = "Seleccionar";
            this.chk.MinimumWidth = 50;
            this.chk.Name = "chk";
            // 
            // panelShowPhotoView
            // 
            this.panelShowPhotoView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelShowPhotoView.Controls.Add(this.fLPShowPhoto);
            this.panelShowPhotoView.Location = new System.Drawing.Point(16, 244);
            this.panelShowPhotoView.Name = "panelShowPhotoView";
            this.panelShowPhotoView.Size = new System.Drawing.Size(1086, 438);
            this.panelShowPhotoView.TabIndex = 14;
            // 
            // fLPShowPhoto
            // 
            this.fLPShowPhoto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fLPShowPhoto.AutoScroll = true;
            this.fLPShowPhoto.Location = new System.Drawing.Point(14, 18);
            this.fLPShowPhoto.Name = "fLPShowPhoto";
            this.fLPShowPhoto.Size = new System.Drawing.Size(1056, 403);
            this.fLPShowPhoto.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.panel1.Location = new System.Drawing.Point(16, 8);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1089, 227);
            this.panel1.TabIndex = 15;
            // 
            // Productos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1120, 694);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panelShowDGVProductosView);
            this.Controls.Add(this.panelShowPhotoView);
            this.Margin = new System.Windows.Forms.Padding(4);
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
        private System.Windows.Forms.DataGridViewCheckBoxColumn chk;
        private System.Windows.Forms.Panel panelShowPhotoView;
        private System.Windows.Forms.FlowLayoutPanel fLPShowPhoto;
        private System.Windows.Forms.Panel panel1;
    }
}