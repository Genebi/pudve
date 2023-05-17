namespace PuntoDeVentaV2
{
    partial class Inventario
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
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.btnRevisar = new System.Windows.Forms.Button();
            this.btnActualizarXML = new System.Windows.Forms.Button();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.botonRedondo2 = new PuntoDeVentaV2.BotonRedondo();
            this.botonRedondo3 = new PuntoDeVentaV2.BotonRedondo();
            this.btnMensajeVenta = new PuntoDeVentaV2.BotonRedondo();
            this.botonRedondo1 = new PuntoDeVentaV2.BotonRedondo();
            this.btnConceptosReporte = new System.Windows.Forms.Button();
            this.gBSeleccionActualizarInventario = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Traspaso = new System.Windows.Forms.CheckBox();
            this.txtClaveTraspaso = new System.Windows.Forms.TextBox();
            this.rbDisminuirProducto = new System.Windows.Forms.RadioButton();
            this.rbAumentarProducto = new System.Windows.Forms.RadioButton();
            this.panelContenedor = new System.Windows.Forms.Panel();
            this.listaProductos = new System.Windows.Forms.ListBox();
            this.DGVInventario2 = new System.Windows.Forms.DataGridView();
            this.ID2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiferenciaUnidades2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NuevoStock2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Clave2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comentarios2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDTabla2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.bntTerminar = new System.Windows.Forms.Button();
            this.tituloBusqueda = new System.Windows.Forms.Label();
            this.txtBusqueda = new System.Windows.Forms.TextBox();
            this.DGVInventario = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiferenciaUnidades = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NuevoStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comentarios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDTabla = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelBotones.SuspendLayout();
            this.gBSeleccionActualizarInventario.SuspendLayout();
            this.panelContenedor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVInventario2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVInventario)).BeginInit();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(6, 6);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(840, 25);
            this.tituloSeccion.TabIndex = 6;
            this.tituloSeccion.Text = "INVENTARIO";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnRevisar
            // 
            this.btnRevisar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRevisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnRevisar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRevisar.FlatAppearance.BorderSize = 0;
            this.btnRevisar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnRevisar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnRevisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRevisar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRevisar.ForeColor = System.Drawing.Color.White;
            this.btnRevisar.Location = new System.Drawing.Point(-3, 7);
            this.btnRevisar.Name = "btnRevisar";
            this.btnRevisar.Size = new System.Drawing.Size(230, 30);
            this.btnRevisar.TabIndex = 101;
            this.btnRevisar.Text = "Revisar Inventario";
            this.btnRevisar.UseVisualStyleBackColor = false;
            this.btnRevisar.Visible = false;
            this.btnRevisar.Click += new System.EventHandler(this.btnRevisar_Click);
            this.btnRevisar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnRevisar_KeyDown);
            // 
            // btnActualizarXML
            // 
            this.btnActualizarXML.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnActualizarXML.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnActualizarXML.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizarXML.FlatAppearance.BorderSize = 0;
            this.btnActualizarXML.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnActualizarXML.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnActualizarXML.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizarXML.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizarXML.ForeColor = System.Drawing.Color.White;
            this.btnActualizarXML.Location = new System.Drawing.Point(606, 7);
            this.btnActualizarXML.Name = "btnActualizarXML";
            this.btnActualizarXML.Size = new System.Drawing.Size(230, 30);
            this.btnActualizarXML.TabIndex = 102;
            this.btnActualizarXML.Text = "Actualizar Inventario XML";
            this.btnActualizarXML.UseVisualStyleBackColor = false;
            this.btnActualizarXML.Visible = false;
            this.btnActualizarXML.Click += new System.EventHandler(this.btnActualizarXML_Click);
            this.btnActualizarXML.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnActualizarXML_KeyDown);
            // 
            // btnActualizar
            // 
            this.btnActualizar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnActualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnActualizar.FlatAppearance.BorderSize = 0;
            this.btnActualizar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnActualizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnActualizar.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnActualizar.ForeColor = System.Drawing.Color.White;
            this.btnActualizar.Location = new System.Drawing.Point(308, 7);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.Size = new System.Drawing.Size(230, 30);
            this.btnActualizar.TabIndex = 103;
            this.btnActualizar.Text = "Actualizar Inventario";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Visible = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            this.btnActualizar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnActualizar_KeyDown);
            // 
            // panelBotones
            // 
            this.panelBotones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBotones.Controls.Add(this.botonRedondo2);
            this.panelBotones.Controls.Add(this.botonRedondo3);
            this.panelBotones.Controls.Add(this.btnMensajeVenta);
            this.panelBotones.Controls.Add(this.botonRedondo1);
            this.panelBotones.Controls.Add(this.btnConceptosReporte);
            this.panelBotones.Controls.Add(this.gBSeleccionActualizarInventario);
            this.panelBotones.Controls.Add(this.btnRevisar);
            this.panelBotones.Controls.Add(this.btnActualizarXML);
            this.panelBotones.Controls.Add(this.btnActualizar);
            this.panelBotones.Location = new System.Drawing.Point(9, 36);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(834, 142);
            this.panelBotones.TabIndex = 104;
            // 
            // botonRedondo2
            // 
            this.botonRedondo2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.botonRedondo2.BackColor = System.Drawing.Color.Crimson;
            this.botonRedondo2.BackGroundColor = System.Drawing.Color.Crimson;
            this.botonRedondo2.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.botonRedondo2.BorderRadius = 20;
            this.botonRedondo2.BorderSize = 0;
            this.botonRedondo2.FlatAppearance.BorderSize = 0;
            this.botonRedondo2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonRedondo2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonRedondo2.ForeColor = System.Drawing.Color.White;
            this.botonRedondo2.Location = new System.Drawing.Point(301, 7);
            this.botonRedondo2.Name = "botonRedondo2";
            this.botonRedondo2.Size = new System.Drawing.Size(209, 47);
            this.botonRedondo2.TabIndex = 108;
            this.botonRedondo2.Text = "Actualizar Inventario";
            this.botonRedondo2.TextColor = System.Drawing.Color.White;
            this.botonRedondo2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.botonRedondo2.UseVisualStyleBackColor = false;
            this.botonRedondo2.Click += new System.EventHandler(this.botonRedondo2_Click);
            // 
            // botonRedondo3
            // 
            this.botonRedondo3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.botonRedondo3.BackColor = System.Drawing.Color.Crimson;
            this.botonRedondo3.BackGroundColor = System.Drawing.Color.Crimson;
            this.botonRedondo3.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.botonRedondo3.BorderRadius = 20;
            this.botonRedondo3.BorderSize = 0;
            this.botonRedondo3.FlatAppearance.BorderSize = 0;
            this.botonRedondo3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonRedondo3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonRedondo3.ForeColor = System.Drawing.Color.White;
            this.botonRedondo3.Location = new System.Drawing.Point(601, 3);
            this.botonRedondo3.Name = "botonRedondo3";
            this.botonRedondo3.Size = new System.Drawing.Size(209, 47);
            this.botonRedondo3.TabIndex = 109;
            this.botonRedondo3.Text = "Actualizar Inventario XML";
            this.botonRedondo3.TextColor = System.Drawing.Color.White;
            this.botonRedondo3.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.botonRedondo3.UseVisualStyleBackColor = false;
            this.botonRedondo3.Click += new System.EventHandler(this.botonRedondo3_Click);
            // 
            // btnMensajeVenta
            // 
            this.btnMensajeVenta.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnMensajeVenta.BackColor = System.Drawing.Color.Crimson;
            this.btnMensajeVenta.BackGroundColor = System.Drawing.Color.Crimson;
            this.btnMensajeVenta.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnMensajeVenta.BorderRadius = 20;
            this.btnMensajeVenta.BorderSize = 0;
            this.btnMensajeVenta.FlatAppearance.BorderSize = 0;
            this.btnMensajeVenta.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMensajeVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMensajeVenta.ForeColor = System.Drawing.Color.White;
            this.btnMensajeVenta.Location = new System.Drawing.Point(18, 80);
            this.btnMensajeVenta.Name = "btnMensajeVenta";
            this.btnMensajeVenta.Size = new System.Drawing.Size(209, 47);
            this.btnMensajeVenta.TabIndex = 106;
            this.btnMensajeVenta.Text = "Devolver Producto";
            this.btnMensajeVenta.TextColor = System.Drawing.Color.White;
            this.btnMensajeVenta.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnMensajeVenta.UseVisualStyleBackColor = false;
            this.btnMensajeVenta.Click += new System.EventHandler(this.btnMensajeVenta_Click);
            // 
            // botonRedondo1
            // 
            this.botonRedondo1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.botonRedondo1.BackColor = System.Drawing.Color.Crimson;
            this.botonRedondo1.BackGroundColor = System.Drawing.Color.Crimson;
            this.botonRedondo1.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.botonRedondo1.BorderRadius = 20;
            this.botonRedondo1.BorderSize = 0;
            this.botonRedondo1.FlatAppearance.BorderSize = 0;
            this.botonRedondo1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.botonRedondo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.botonRedondo1.ForeColor = System.Drawing.Color.White;
            this.botonRedondo1.Location = new System.Drawing.Point(19, 3);
            this.botonRedondo1.Name = "botonRedondo1";
            this.botonRedondo1.Size = new System.Drawing.Size(209, 47);
            this.botonRedondo1.TabIndex = 107;
            this.botonRedondo1.Text = "Revisar Inventario";
            this.botonRedondo1.TextColor = System.Drawing.Color.White;
            this.botonRedondo1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.botonRedondo1.UseVisualStyleBackColor = false;
            this.botonRedondo1.Click += new System.EventHandler(this.botonRedondo1_Click);
            // 
            // btnConceptosReporte
            // 
            this.btnConceptosReporte.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnConceptosReporte.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnConceptosReporte.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConceptosReporte.FlatAppearance.BorderSize = 0;
            this.btnConceptosReporte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConceptosReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConceptosReporte.ForeColor = System.Drawing.Color.White;
            this.btnConceptosReporte.Location = new System.Drawing.Point(0, 47);
            this.btnConceptosReporte.Name = "btnConceptosReporte";
            this.btnConceptosReporte.Size = new System.Drawing.Size(227, 31);
            this.btnConceptosReporte.TabIndex = 105;
            this.btnConceptosReporte.Text = "Conceptos del Reporte";
            this.btnConceptosReporte.UseVisualStyleBackColor = false;
            this.btnConceptosReporte.Visible = false;
            this.btnConceptosReporte.Click += new System.EventHandler(this.btnConceptosReporte_Click);
            // 
            // gBSeleccionActualizarInventario
            // 
            this.gBSeleccionActualizarInventario.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.gBSeleccionActualizarInventario.Controls.Add(this.label1);
            this.gBSeleccionActualizarInventario.Controls.Add(this.Traspaso);
            this.gBSeleccionActualizarInventario.Controls.Add(this.txtClaveTraspaso);
            this.gBSeleccionActualizarInventario.Controls.Add(this.rbDisminuirProducto);
            this.gBSeleccionActualizarInventario.Controls.Add(this.rbAumentarProducto);
            this.gBSeleccionActualizarInventario.Location = new System.Drawing.Point(274, 53);
            this.gBSeleccionActualizarInventario.Name = "gBSeleccionActualizarInventario";
            this.gBSeleccionActualizarInventario.Size = new System.Drawing.Size(305, 83);
            this.gBSeleccionActualizarInventario.TabIndex = 104;
            this.gBSeleccionActualizarInventario.TabStop = false;
            this.gBSeleccionActualizarInventario.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(136, 49);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Clave:";
            // 
            // Traspaso
            // 
            this.Traspaso.AutoSize = true;
            this.Traspaso.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Traspaso.Location = new System.Drawing.Point(33, 49);
            this.Traspaso.Margin = new System.Windows.Forms.Padding(2);
            this.Traspaso.Name = "Traspaso";
            this.Traspaso.Size = new System.Drawing.Size(87, 21);
            this.Traspaso.TabIndex = 3;
            this.Traspaso.Text = "Traspaso";
            this.Traspaso.UseVisualStyleBackColor = true;
            this.Traspaso.CheckedChanged += new System.EventHandler(this.Traspaso_CheckedChanged);
            // 
            // txtClaveTraspaso
            // 
            this.txtClaveTraspaso.Enabled = false;
            this.txtClaveTraspaso.Location = new System.Drawing.Point(186, 50);
            this.txtClaveTraspaso.Margin = new System.Windows.Forms.Padding(2);
            this.txtClaveTraspaso.Name = "txtClaveTraspaso";
            this.txtClaveTraspaso.Size = new System.Drawing.Size(78, 20);
            this.txtClaveTraspaso.TabIndex = 2;
            this.txtClaveTraspaso.TextChanged += new System.EventHandler(this.txtClaveTraspaso_TextChanged);
            // 
            // rbDisminuirProducto
            // 
            this.rbDisminuirProducto.AutoSize = true;
            this.rbDisminuirProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDisminuirProducto.Location = new System.Drawing.Point(210, 14);
            this.rbDisminuirProducto.Name = "rbDisminuirProducto";
            this.rbDisminuirProducto.Size = new System.Drawing.Size(92, 24);
            this.rbDisminuirProducto.TabIndex = 1;
            this.rbDisminuirProducto.TabStop = true;
            this.rbDisminuirProducto.Text = "Disminuir";
            this.rbDisminuirProducto.UseVisualStyleBackColor = true;
            this.rbDisminuirProducto.CheckedChanged += new System.EventHandler(this.rbDisminuirProducto_CheckedChanged);
            this.rbDisminuirProducto.Click += new System.EventHandler(this.rbDisminuirProducto_Click);
            // 
            // rbAumentarProducto
            // 
            this.rbAumentarProducto.AutoSize = true;
            this.rbAumentarProducto.Checked = true;
            this.rbAumentarProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbAumentarProducto.Location = new System.Drawing.Point(13, 14);
            this.rbAumentarProducto.Name = "rbAumentarProducto";
            this.rbAumentarProducto.Size = new System.Drawing.Size(191, 24);
            this.rbAumentarProducto.TabIndex = 0;
            this.rbAumentarProducto.TabStop = true;
            this.rbAumentarProducto.Text = "Aumentar (COMPRAS)";
            this.rbAumentarProducto.UseVisualStyleBackColor = true;
            this.rbAumentarProducto.CheckedChanged += new System.EventHandler(this.rbAumentarProducto_CheckedChanged);
            this.rbAumentarProducto.Click += new System.EventHandler(this.rbAumentarProducto_Click);
            // 
            // panelContenedor
            // 
            this.panelContenedor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelContenedor.Controls.Add(this.listaProductos);
            this.panelContenedor.Controls.Add(this.DGVInventario2);
            this.panelContenedor.Controls.Add(this.btnBuscar);
            this.panelContenedor.Controls.Add(this.bntTerminar);
            this.panelContenedor.Controls.Add(this.tituloBusqueda);
            this.panelContenedor.Controls.Add(this.txtBusqueda);
            this.panelContenedor.Controls.Add(this.DGVInventario);
            this.panelContenedor.Cursor = System.Windows.Forms.Cursors.Default;
            this.panelContenedor.Location = new System.Drawing.Point(9, 178);
            this.panelContenedor.Name = "panelContenedor";
            this.panelContenedor.Size = new System.Drawing.Size(834, 283);
            this.panelContenedor.TabIndex = 105;
            this.panelContenedor.Visible = false;
            // 
            // listaProductos
            // 
            this.listaProductos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listaProductos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listaProductos.FormattingEnabled = true;
            this.listaProductos.ItemHeight = 16;
            this.listaProductos.Location = new System.Drawing.Point(0, 45);
            this.listaProductos.Name = "listaProductos";
            this.listaProductos.Size = new System.Drawing.Size(583, 116);
            this.listaProductos.TabIndex = 12;
            this.listaProductos.Visible = false;
            this.listaProductos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listaProductos_KeyDown);
            this.listaProductos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listaProductos_MouseDoubleClick);
            // 
            // DGVInventario2
            // 
            this.DGVInventario2.AllowUserToAddRows = false;
            this.DGVInventario2.AllowUserToDeleteRows = false;
            this.DGVInventario2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVInventario2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVInventario2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID2,
            this.Nombre2,
            this.Stock2,
            this.DiferenciaUnidades2,
            this.NuevoStock2,
            this.Precio2,
            this.Clave2,
            this.Codigo2,
            this.Fecha2,
            this.Comentarios2,
            this.IDTabla2});
            this.DGVInventario2.Location = new System.Drawing.Point(0, 65);
            this.DGVInventario2.Name = "DGVInventario2";
            this.DGVInventario2.ReadOnly = true;
            this.DGVInventario2.RowHeadersVisible = false;
            this.DGVInventario2.RowHeadersWidth = 62;
            this.DGVInventario2.Size = new System.Drawing.Size(834, 177);
            this.DGVInventario2.TabIndex = 106;
            // 
            // ID2
            // 
            this.ID2.HeaderText = "ID";
            this.ID2.MinimumWidth = 8;
            this.ID2.Name = "ID2";
            this.ID2.ReadOnly = true;
            this.ID2.Visible = false;
            this.ID2.Width = 150;
            // 
            // Nombre2
            // 
            this.Nombre2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nombre2.HeaderText = "Nombre";
            this.Nombre2.MinimumWidth = 8;
            this.Nombre2.Name = "Nombre2";
            this.Nombre2.ReadOnly = true;
            // 
            // Stock2
            // 
            this.Stock2.HeaderText = "Stock Anterior";
            this.Stock2.MinimumWidth = 8;
            this.Stock2.Name = "Stock2";
            this.Stock2.ReadOnly = true;
            this.Stock2.Width = 70;
            // 
            // DiferenciaUnidades2
            // 
            this.DiferenciaUnidades2.HeaderText = "Diferencia de Unidades";
            this.DiferenciaUnidades2.MinimumWidth = 8;
            this.DiferenciaUnidades2.Name = "DiferenciaUnidades2";
            this.DiferenciaUnidades2.ReadOnly = true;
            this.DiferenciaUnidades2.Width = 90;
            // 
            // NuevoStock2
            // 
            this.NuevoStock2.HeaderText = "Stock Actual";
            this.NuevoStock2.MinimumWidth = 8;
            this.NuevoStock2.Name = "NuevoStock2";
            this.NuevoStock2.ReadOnly = true;
            this.NuevoStock2.Width = 70;
            // 
            // Precio2
            // 
            this.Precio2.HeaderText = "Precio";
            this.Precio2.MinimumWidth = 8;
            this.Precio2.Name = "Precio2";
            this.Precio2.ReadOnly = true;
            this.Precio2.Width = 70;
            // 
            // Clave2
            // 
            this.Clave2.HeaderText = "Clave";
            this.Clave2.MinimumWidth = 8;
            this.Clave2.Name = "Clave2";
            this.Clave2.ReadOnly = true;
            this.Clave2.Width = 150;
            // 
            // Codigo2
            // 
            this.Codigo2.HeaderText = "Código";
            this.Codigo2.MinimumWidth = 8;
            this.Codigo2.Name = "Codigo2";
            this.Codigo2.ReadOnly = true;
            this.Codigo2.Width = 95;
            // 
            // Fecha2
            // 
            this.Fecha2.HeaderText = "Fecha";
            this.Fecha2.MinimumWidth = 8;
            this.Fecha2.Name = "Fecha2";
            this.Fecha2.ReadOnly = true;
            this.Fecha2.Width = 95;
            // 
            // Comentarios2
            // 
            this.Comentarios2.HeaderText = "Comentarios";
            this.Comentarios2.MinimumWidth = 8;
            this.Comentarios2.Name = "Comentarios2";
            this.Comentarios2.ReadOnly = true;
            this.Comentarios2.Visible = false;
            this.Comentarios2.Width = 150;
            // 
            // IDTabla2
            // 
            this.IDTabla2.HeaderText = "IDTabla";
            this.IDTabla2.MinimumWidth = 8;
            this.IDTabla2.Name = "IDTabla2";
            this.IDTabla2.ReadOnly = true;
            this.IDTabla2.Visible = false;
            this.IDTabla2.Width = 150;
            // 
            // btnBuscar
            // 
            this.btnBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBuscar.BackColor = System.Drawing.Color.Green;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(601, 26);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(105, 23);
            this.btnBuscar.TabIndex = 105;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            this.btnBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnBuscar_KeyDown);
            // 
            // bntTerminar
            // 
            this.bntTerminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bntTerminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.bntTerminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.bntTerminar.FlatAppearance.BorderSize = 0;
            this.bntTerminar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.bntTerminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.bntTerminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bntTerminar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bntTerminar.ForeColor = System.Drawing.Color.White;
            this.bntTerminar.Location = new System.Drawing.Point(683, 248);
            this.bntTerminar.Name = "bntTerminar";
            this.bntTerminar.Size = new System.Drawing.Size(150, 30);
            this.bntTerminar.TabIndex = 104;
            this.bntTerminar.Text = "Terminar";
            this.bntTerminar.UseVisualStyleBackColor = false;
            this.bntTerminar.Click += new System.EventHandler(this.bntTerminar_Click);
            this.bntTerminar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.bntTerminar_KeyDown);
            // 
            // tituloBusqueda
            // 
            this.tituloBusqueda.AutoSize = true;
            this.tituloBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloBusqueda.Location = new System.Drawing.Point(-4, 3);
            this.tituloBusqueda.Name = "tituloBusqueda";
            this.tituloBusqueda.Size = new System.Drawing.Size(232, 18);
            this.tituloBusqueda.TabIndex = 10;
            this.tituloBusqueda.Text = "Búsqueda avanzada de productos";
            this.tituloBusqueda.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBusqueda
            // 
            this.txtBusqueda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusqueda.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtBusqueda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusqueda.Location = new System.Drawing.Point(0, 26);
            this.txtBusqueda.Name = "txtBusqueda";
            this.txtBusqueda.Size = new System.Drawing.Size(583, 22);
            this.txtBusqueda.TabIndex = 11;
            this.txtBusqueda.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBusqueda.TextChanged += new System.EventHandler(this.txtBusqueda_TextChanged);
            this.txtBusqueda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBusqueda_KeyDown);
            this.txtBusqueda.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtBusqueda_KeyUp);
            // 
            // DGVInventario
            // 
            this.DGVInventario.AllowUserToAddRows = false;
            this.DGVInventario.AllowUserToDeleteRows = false;
            this.DGVInventario.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVInventario.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Nombre,
            this.Stock,
            this.DiferenciaUnidades,
            this.NuevoStock,
            this.Precio,
            this.Clave,
            this.Codigo,
            this.Fecha,
            this.Comentarios,
            this.IDTabla});
            this.DGVInventario.Location = new System.Drawing.Point(0, 65);
            this.DGVInventario.Name = "DGVInventario";
            this.DGVInventario.ReadOnly = true;
            this.DGVInventario.RowHeadersVisible = false;
            this.DGVInventario.RowHeadersWidth = 62;
            this.DGVInventario.Size = new System.Drawing.Size(834, 177);
            this.DGVInventario.TabIndex = 9;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 8;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 150;
            // 
            // Nombre
            // 
            this.Nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.MinimumWidth = 8;
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            // 
            // Stock
            // 
            this.Stock.HeaderText = "Stock Anterior";
            this.Stock.MinimumWidth = 8;
            this.Stock.Name = "Stock";
            this.Stock.ReadOnly = true;
            this.Stock.Width = 70;
            // 
            // DiferenciaUnidades
            // 
            this.DiferenciaUnidades.HeaderText = "Diferencia de Unidades";
            this.DiferenciaUnidades.MinimumWidth = 8;
            this.DiferenciaUnidades.Name = "DiferenciaUnidades";
            this.DiferenciaUnidades.ReadOnly = true;
            this.DiferenciaUnidades.Width = 90;
            // 
            // NuevoStock
            // 
            this.NuevoStock.HeaderText = "Stock Actual";
            this.NuevoStock.MinimumWidth = 8;
            this.NuevoStock.Name = "NuevoStock";
            this.NuevoStock.ReadOnly = true;
            this.NuevoStock.Width = 70;
            // 
            // Precio
            // 
            this.Precio.HeaderText = "Precio";
            this.Precio.MinimumWidth = 8;
            this.Precio.Name = "Precio";
            this.Precio.ReadOnly = true;
            this.Precio.Width = 70;
            // 
            // Clave
            // 
            this.Clave.HeaderText = "Clave";
            this.Clave.MinimumWidth = 8;
            this.Clave.Name = "Clave";
            this.Clave.ReadOnly = true;
            this.Clave.Width = 150;
            // 
            // Codigo
            // 
            this.Codigo.HeaderText = "Código";
            this.Codigo.MinimumWidth = 8;
            this.Codigo.Name = "Codigo";
            this.Codigo.ReadOnly = true;
            this.Codigo.Width = 95;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.MinimumWidth = 8;
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 95;
            // 
            // Comentarios
            // 
            this.Comentarios.HeaderText = "Comentarios";
            this.Comentarios.MinimumWidth = 8;
            this.Comentarios.Name = "Comentarios";
            this.Comentarios.ReadOnly = true;
            this.Comentarios.Visible = false;
            this.Comentarios.Width = 150;
            // 
            // IDTabla
            // 
            this.IDTabla.HeaderText = "IDTabla";
            this.IDTabla.MinimumWidth = 8;
            this.IDTabla.Name = "IDTabla";
            this.IDTabla.ReadOnly = true;
            this.IDTabla.Visible = false;
            this.IDTabla.Width = 150;
            // 
            // Inventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(858, 485);
            this.Controls.Add(this.panelContenedor);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.tituloSeccion);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Inventario";
            this.ShowIcon = false;
            this.Text = "PUDVE - Inventario";
            this.Activated += new System.EventHandler(this.Inventario_Activated);
            this.Load += new System.EventHandler(this.Inventario_Load);
            this.DragLeave += new System.EventHandler(this.Inventario_DragLeave);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Inventario_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Inventario_KeyDown);
            this.panelBotones.ResumeLayout(false);
            this.gBSeleccionActualizarInventario.ResumeLayout(false);
            this.gBSeleccionActualizarInventario.PerformLayout();
            this.panelContenedor.ResumeLayout(false);
            this.panelContenedor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVInventario2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DGVInventario)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnRevisar;
        private System.Windows.Forms.Button btnActualizarXML;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Panel panelContenedor;
        private System.Windows.Forms.DataGridView DGVInventario;
        private System.Windows.Forms.Label tituloBusqueda;
        private System.Windows.Forms.TextBox txtBusqueda;
        private System.Windows.Forms.ListBox listaProductos;
        private System.Windows.Forms.Button bntTerminar;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.GroupBox gBSeleccionActualizarInventario;
        private System.Windows.Forms.RadioButton rbDisminuirProducto;
        private System.Windows.Forms.RadioButton rbAumentarProducto;
        private System.Windows.Forms.Button btnConceptosReporte;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiferenciaUnidades;
        private System.Windows.Forms.DataGridViewTextBoxColumn NuevoStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comentarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDTabla;
        private System.Windows.Forms.TextBox txtClaveTraspaso;
        private System.Windows.Forms.CheckBox Traspaso;
        private System.Windows.Forms.Label label1;
        private BotonRedondo btnMensajeVenta;
        private BotonRedondo botonRedondo1;
        private BotonRedondo botonRedondo3;
        private BotonRedondo botonRedondo2;
        private System.Windows.Forms.DataGridView DGVInventario2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock2;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiferenciaUnidades2;
        private System.Windows.Forms.DataGridViewTextBoxColumn NuevoStock2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Codigo2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comentarios2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDTabla2;
        public System.Windows.Forms.Label tituloSeccion;
        public System.Windows.Forms.Button btnActualizar;
    }
}