namespace PuntoDeVentaV2
{
    partial class ConsultarProductosVentaFacil
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsultarProductosVentaFacil));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.CBTipo = new System.Windows.Forms.ComboBox();
            this.dgvFast = new System.Windows.Forms.DataGridView();
            this.Name1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.IDC1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col2 = new System.Windows.Forms.DataGridViewImageColumn();
            this.IDC2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col3 = new System.Windows.Forms.DataGridViewImageColumn();
            this.IDC3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.IDC4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Lista = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvLista = new System.Windows.Forms.DataGridView();
            this.Producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.dgvOpciones = new System.Windows.Forms.DataGridView();
            this.Cat1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cat1Img = new System.Windows.Forms.DataGridViewImageColumn();
            this.Cat2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cat2Img = new System.Windows.Forms.DataGridViewImageColumn();
            this.Cat3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cat3Img = new System.Windows.Forms.DataGridViewImageColumn();
            this.Cat4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cat4Img = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFast)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpciones)).BeginInit();
            this.SuspendLayout();
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
            this.CBTipo.Visible = false;
            this.CBTipo.SelectedIndexChanged += new System.EventHandler(this.CBTipo_SelectedIndexChanged);
            // 
            // dgvFast
            // 
            this.dgvFast.AllowUserToAddRows = false;
            this.dgvFast.AllowUserToDeleteRows = false;
            this.dgvFast.AllowUserToOrderColumns = true;
            this.dgvFast.AllowUserToResizeColumns = false;
            this.dgvFast.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(500);
            this.dgvFast.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFast.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFast.ColumnHeadersVisible = false;
            this.dgvFast.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Name1,
            this.Col1,
            this.IDC1,
            this.Name2,
            this.Col2,
            this.IDC2,
            this.Name3,
            this.Col3,
            this.IDC3,
            this.Name4,
            this.Col4,
            this.IDC4});
            this.dgvFast.Location = new System.Drawing.Point(45, 102);
            this.dgvFast.Name = "dgvFast";
            this.dgvFast.ReadOnly = true;
            this.dgvFast.RowHeadersVisible = false;
            this.dgvFast.Size = new System.Drawing.Size(929, 426);
            this.dgvFast.TabIndex = 40;
            this.dgvFast.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFast_CellMouseClick);
            this.dgvFast.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvFast_CellMouseDoubleClick);
            this.dgvFast.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvFast_CellPainting);
            // 
            // Name1
            // 
            this.Name1.DataPropertyName = "Name1";
            this.Name1.HeaderText = "Name1";
            this.Name1.Name = "Name1";
            this.Name1.ReadOnly = true;
            this.Name1.Visible = false;
            // 
            // Col1
            // 
            this.Col1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Col1.DataPropertyName = "Col1";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Silver;
            this.Col1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Col1.HeaderText = "Col1";
            this.Col1.Image = global::PuntoDeVentaV2.Properties.Resources.ban1;
            this.Col1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Col1.Name = "Col1";
            this.Col1.ReadOnly = true;
            // 
            // IDC1
            // 
            this.IDC1.DataPropertyName = "IDC1";
            this.IDC1.HeaderText = "IDC1";
            this.IDC1.Name = "IDC1";
            this.IDC1.ReadOnly = true;
            this.IDC1.Visible = false;
            // 
            // Name2
            // 
            this.Name2.DataPropertyName = "Name2";
            this.Name2.HeaderText = "Name2";
            this.Name2.Name = "Name2";
            this.Name2.ReadOnly = true;
            this.Name2.Visible = false;
            // 
            // Col2
            // 
            this.Col2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Col2.DataPropertyName = "Col2";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle3.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Silver;
            this.Col2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Col2.HeaderText = "Col2";
            this.Col2.Image = global::PuntoDeVentaV2.Properties.Resources.ban1;
            this.Col2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Col2.Name = "Col2";
            this.Col2.ReadOnly = true;
            // 
            // IDC2
            // 
            this.IDC2.DataPropertyName = "IDC2";
            this.IDC2.HeaderText = "IDC2";
            this.IDC2.Name = "IDC2";
            this.IDC2.ReadOnly = true;
            this.IDC2.Visible = false;
            // 
            // Name3
            // 
            this.Name3.DataPropertyName = "Name3";
            this.Name3.HeaderText = "Name3";
            this.Name3.Name = "Name3";
            this.Name3.ReadOnly = true;
            this.Name3.Visible = false;
            // 
            // Col3
            // 
            this.Col3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Col3.DataPropertyName = "Col3";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle4.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Silver;
            this.Col3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Col3.HeaderText = "Col3";
            this.Col3.Image = global::PuntoDeVentaV2.Properties.Resources.ban1;
            this.Col3.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Col3.Name = "Col3";
            this.Col3.ReadOnly = true;
            // 
            // IDC3
            // 
            this.IDC3.DataPropertyName = "IDC3";
            this.IDC3.HeaderText = "IDC3";
            this.IDC3.Name = "IDC3";
            this.IDC3.ReadOnly = true;
            this.IDC3.Visible = false;
            // 
            // Name4
            // 
            this.Name4.DataPropertyName = "Name4";
            this.Name4.HeaderText = "Name4";
            this.Name4.Name = "Name4";
            this.Name4.ReadOnly = true;
            this.Name4.Visible = false;
            // 
            // Col4
            // 
            this.Col4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Col4.DataPropertyName = "Col4";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle5.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Silver;
            this.Col4.DefaultCellStyle = dataGridViewCellStyle5;
            this.Col4.HeaderText = "Col4";
            this.Col4.Image = global::PuntoDeVentaV2.Properties.Resources.ban1;
            this.Col4.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Col4.Name = "Col4";
            this.Col4.ReadOnly = true;
            // 
            // IDC4
            // 
            this.IDC4.DataPropertyName = "IDC4";
            this.IDC4.HeaderText = "IDC4";
            this.IDC4.Name = "IDC4";
            this.IDC4.ReadOnly = true;
            this.IDC4.Visible = false;
            // 
            // Lista
            // 
            this.Lista.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Lista.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lista.Location = new System.Drawing.Point(980, 52);
            this.Lista.Name = "Lista";
            this.Lista.Size = new System.Drawing.Size(240, 25);
            this.Lista.TabIndex = 7;
            this.Lista.Text = "Lista de productos";
            this.Lista.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(980, 140);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 239);
            this.label1.TabIndex = 7;
            // 
            // dgvLista
            // 
            this.dgvLista.AllowUserToAddRows = false;
            this.dgvLista.AllowUserToDeleteRows = false;
            this.dgvLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLista.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Producto,
            this.Cantidad,
            this.ID});
            this.dgvLista.Location = new System.Drawing.Point(984, 102);
            this.dgvLista.Name = "dgvLista";
            this.dgvLista.ReadOnly = true;
            this.dgvLista.RowHeadersVisible = false;
            this.dgvLista.Size = new System.Drawing.Size(236, 382);
            this.dgvLista.TabIndex = 41;
            // 
            // Producto
            // 
            this.Producto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Producto.HeaderText = "Producto";
            this.Producto.Name = "Producto";
            this.Producto.ReadOnly = true;
            // 
            // Cantidad
            // 
            this.Cantidad.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cantidad.HeaderText = "Cantidad";
            this.Cantidad.Name = "Cantidad";
            this.Cantidad.ReadOnly = true;
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.Green;
            this.btnOk.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOk.ForeColor = System.Drawing.Color.White;
            this.btnOk.Location = new System.Drawing.Point(1128, 490);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(92, 38);
            this.btnOk.TabIndex = 42;
            this.btnOk.Text = "Aceptar";
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCerrar.Location = new System.Drawing.Point(984, 490);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(92, 38);
            this.btnCerrar.TabIndex = 43;
            this.btnCerrar.Text = "Cancelar";
            this.btnCerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // dgvOpciones
            // 
            this.dgvOpciones.AllowUserToAddRows = false;
            this.dgvOpciones.AllowUserToDeleteRows = false;
            this.dgvOpciones.AllowUserToOrderColumns = true;
            this.dgvOpciones.AllowUserToResizeColumns = false;
            this.dgvOpciones.AllowUserToResizeRows = false;
            this.dgvOpciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOpciones.ColumnHeadersVisible = false;
            this.dgvOpciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cat1,
            this.Cat1Img,
            this.Cat2,
            this.Cat2Img,
            this.Cat3,
            this.Cat3Img,
            this.Cat4,
            this.Cat4Img});
            this.dgvOpciones.Location = new System.Drawing.Point(45, 102);
            this.dgvOpciones.MultiSelect = false;
            this.dgvOpciones.Name = "dgvOpciones";
            this.dgvOpciones.ReadOnly = true;
            this.dgvOpciones.RowHeadersVisible = false;
            this.dgvOpciones.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvOpciones.Size = new System.Drawing.Size(929, 426);
            this.dgvOpciones.TabIndex = 40;
            this.dgvOpciones.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOpciones_CellClick);
            this.dgvOpciones.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvOpciones_CellPainting);
            // 
            // Cat1
            // 
            this.Cat1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cat1.DataPropertyName = "Cat1";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Silver;
            this.Cat1.DefaultCellStyle = dataGridViewCellStyle6;
            this.Cat1.HeaderText = "Cat1";
            this.Cat1.Name = "Cat1";
            this.Cat1.ReadOnly = true;
            this.Cat1.Visible = false;
            // 
            // Cat1Img
            // 
            this.Cat1Img.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cat1Img.DataPropertyName = "Cat1Img";
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle7.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle7.NullValue")));
            dataGridViewCellStyle7.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Silver;
            this.Cat1Img.DefaultCellStyle = dataGridViewCellStyle7;
            this.Cat1Img.HeaderText = "Cat1Img";
            this.Cat1Img.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Cat1Img.Name = "Cat1Img";
            this.Cat1Img.ReadOnly = true;
            // 
            // Cat2
            // 
            this.Cat2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cat2.DataPropertyName = "Cat2";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cat2.DefaultCellStyle = dataGridViewCellStyle8;
            this.Cat2.HeaderText = "Cat2";
            this.Cat2.Name = "Cat2";
            this.Cat2.ReadOnly = true;
            this.Cat2.Visible = false;
            // 
            // Cat2Img
            // 
            this.Cat2Img.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cat2Img.DataPropertyName = "Cat2Img";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle9.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle9.NullValue")));
            dataGridViewCellStyle9.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Silver;
            this.Cat2Img.DefaultCellStyle = dataGridViewCellStyle9;
            this.Cat2Img.HeaderText = "Cat2Img";
            this.Cat2Img.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Cat2Img.Name = "Cat2Img";
            this.Cat2Img.ReadOnly = true;
            // 
            // Cat3
            // 
            this.Cat3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cat3.DataPropertyName = "Cat3";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cat3.DefaultCellStyle = dataGridViewCellStyle10;
            this.Cat3.HeaderText = "Cat3";
            this.Cat3.Name = "Cat3";
            this.Cat3.ReadOnly = true;
            this.Cat3.Visible = false;
            // 
            // Cat3Img
            // 
            this.Cat3Img.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cat3Img.DataPropertyName = "Cat3Img";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle11.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle11.NullValue")));
            dataGridViewCellStyle11.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.Silver;
            this.Cat3Img.DefaultCellStyle = dataGridViewCellStyle11;
            this.Cat3Img.HeaderText = "Cat3Img";
            this.Cat3Img.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Cat3Img.Name = "Cat3Img";
            this.Cat3Img.ReadOnly = true;
            // 
            // Cat4
            // 
            this.Cat4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cat4.DataPropertyName = "Cat4";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Cat4.DefaultCellStyle = dataGridViewCellStyle12;
            this.Cat4.HeaderText = "Cat4";
            this.Cat4.Name = "Cat4";
            this.Cat4.ReadOnly = true;
            this.Cat4.Visible = false;
            // 
            // Cat4Img
            // 
            this.Cat4Img.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Cat4Img.DataPropertyName = "Cat4Img";
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.Silver;
            dataGridViewCellStyle13.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle13.NullValue")));
            dataGridViewCellStyle13.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.Silver;
            this.Cat4Img.DefaultCellStyle = dataGridViewCellStyle13;
            this.Cat4Img.HeaderText = "Cat4Img";
            this.Cat4Img.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Stretch;
            this.Cat4Img.Name = "Cat4Img";
            this.Cat4Img.ReadOnly = true;
            // 
            // ConsultarProductosVentaFacil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 540);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.dgvLista);
            this.Controls.Add(this.dgvOpciones);
            this.Controls.Add(this.dgvFast);
            this.Controls.Add(this.CBTipo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Lista);
            this.Controls.Add(this.tituloSeccion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConsultarProductosVentaFacil";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ConsultarProductosVentaFacil_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFast)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLista)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOpciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.ComboBox CBTipo;
        private System.Windows.Forms.DataGridView dgvFast;
        private System.Windows.Forms.Label Lista;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvLista;
        private System.Windows.Forms.DataGridViewTextBoxColumn Producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.DataGridView dgvOpciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name1;
        private System.Windows.Forms.DataGridViewImageColumn Col1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDC1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name2;
        private System.Windows.Forms.DataGridViewImageColumn Col2;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDC2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name3;
        private System.Windows.Forms.DataGridViewImageColumn Col3;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDC3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name4;
        private System.Windows.Forms.DataGridViewImageColumn Col4;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDC4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cat1;
        private System.Windows.Forms.DataGridViewImageColumn Cat1Img;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cat2;
        private System.Windows.Forms.DataGridViewImageColumn Cat2Img;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cat3;
        private System.Windows.Forms.DataGridViewImageColumn Cat3Img;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cat4;
        private System.Windows.Forms.DataGridViewImageColumn Cat4Img;
    }
}