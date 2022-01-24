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
            this.components = new System.ComponentModel.Container();
            this.separadorInicial = new System.Windows.Forms.Label();
            this.btnGuardarDetalles = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.fLPLateralConcepto = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.fLPCentralDetalle = new System.Windows.Forms.FlowLayoutPanel();
            this.txtStockMinimo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtStockNecesario = new System.Windows.Forms.TextBox();
            this.btnAddDetalle = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnInhabilitados = new System.Windows.Forms.Button();
            this.btnDeleteDetalle = new System.Windows.Forms.Button();
            this.btnRenameDetalle = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.timerStockMaximo = new System.Windows.Forms.Timer(this.components);
            this.chkMensajeInventario = new System.Windows.Forms.CheckBox();
            this.chkBoxProductMessage = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // separadorInicial
            // 
            this.separadorInicial.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.separadorInicial.Location = new System.Drawing.Point(5, 7);
            this.separadorInicial.Name = "separadorInicial";
            this.separadorInicial.Size = new System.Drawing.Size(928, 2);
            this.separadorInicial.TabIndex = 19;
            // 
            // btnGuardarDetalles
            // 
            this.btnGuardarDetalles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnGuardarDetalles.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardarDetalles.FlatAppearance.BorderSize = 0;
            this.btnGuardarDetalles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarDetalles.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardarDetalles.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnGuardarDetalles.Image = global::PuntoDeVentaV2.Properties.Resources.save1;
            this.btnGuardarDetalles.Location = new System.Drawing.Point(11, 14);
            this.btnGuardarDetalles.Name = "btnGuardarDetalles";
            this.btnGuardarDetalles.Size = new System.Drawing.Size(201, 41);
            this.btnGuardarDetalles.TabIndex = 26;
            this.btnGuardarDetalles.Text = "Guardar";
            this.btnGuardarDetalles.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnGuardarDetalles.UseVisualStyleBackColor = false;
            this.btnGuardarDetalles.Click += new System.EventHandler(this.btnGuardarDetalles_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.fLPLateralConcepto);
            this.panel1.Location = new System.Drawing.Point(3, 18);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 524);
            this.panel1.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.Image = global::PuntoDeVentaV2.Properties.Resources.eye;
            this.label6.Location = new System.Drawing.Point(224, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 23);
            this.label6.TabIndex = 3;
            this.toolTip1.SetToolTip(this.label6, "Activar casilla para, mostrar en ventana (Afuera) de productos");
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(159, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 39);
            this.label2.TabIndex = 2;
            this.label2.Text = "Especifica\r\nciones";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre";
            // 
            // fLPLateralConcepto
            // 
            this.fLPLateralConcepto.BackColor = System.Drawing.SystemColors.Control;
            this.fLPLateralConcepto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fLPLateralConcepto.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.fLPLateralConcepto.Location = new System.Drawing.Point(4, 44);
            this.fLPLateralConcepto.Name = "fLPLateralConcepto";
            this.fLPLateralConcepto.Size = new System.Drawing.Size(274, 475);
            this.fLPLateralConcepto.TabIndex = 0;
            this.fLPLateralConcepto.WrapContents = false;
            this.fLPLateralConcepto.Paint += new System.Windows.Forms.PaintEventHandler(this.fLPLateralConcepto_Paint);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.fLPCentralDetalle);
            this.panel2.Location = new System.Drawing.Point(290, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(643, 526);
            this.panel2.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(239, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Detalles Seleccionados";
            // 
            // fLPCentralDetalle
            // 
            this.fLPCentralDetalle.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.fLPCentralDetalle.AutoScroll = true;
            this.fLPCentralDetalle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fLPCentralDetalle.Location = new System.Drawing.Point(5, 46);
            this.fLPCentralDetalle.Name = "fLPCentralDetalle";
            this.fLPCentralDetalle.Size = new System.Drawing.Size(632, 475);
            this.fLPCentralDetalle.TabIndex = 0;
            // 
            // txtStockMinimo
            // 
            this.txtStockMinimo.Location = new System.Drawing.Point(805, 546);
            this.txtStockMinimo.Name = "txtStockMinimo";
            this.txtStockMinimo.Size = new System.Drawing.Size(120, 20);
            this.txtStockMinimo.TabIndex = 5;
            this.txtStockMinimo.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(714, 549);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Stock Mínimo";
            this.label5.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(496, 549);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Stock Máximo";
            this.label4.Visible = false;
            // 
            // txtStockNecesario
            // 
            this.txtStockNecesario.Location = new System.Drawing.Point(588, 546);
            this.txtStockNecesario.Name = "txtStockNecesario";
            this.txtStockNecesario.Size = new System.Drawing.Size(120, 20);
            this.txtStockNecesario.TabIndex = 2;
            this.txtStockNecesario.Visible = false;
            this.txtStockNecesario.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtStockNecesario_KeyUp);
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
            this.btnAddDetalle.Location = new System.Drawing.Point(6, 12);
            this.btnAddDetalle.Name = "btnAddDetalle";
            this.btnAddDetalle.Size = new System.Drawing.Size(189, 41);
            this.btnAddDetalle.TabIndex = 29;
            this.btnAddDetalle.Text = "Agregar";
            this.btnAddDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnAddDetalle, "Desde este botón podrás agregar los detalles de tu producto o servicio,\r\npor ejem" +
        "plo: color, ubicación, gramaje, etc. ¡Lo que imagines!.");
            this.btnAddDetalle.UseVisualStyleBackColor = false;
            this.btnAddDetalle.Click += new System.EventHandler(this.btnAddDetalle_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnInhabilitados);
            this.groupBox1.Controls.Add(this.btnDeleteDetalle);
            this.groupBox1.Controls.Add(this.btnRenameDetalle);
            this.groupBox1.Controls.Add(this.btnAddDetalle);
            this.groupBox1.Location = new System.Drawing.Point(5, 546);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(397, 105);
            this.groupBox1.TabIndex = 30;
            this.groupBox1.TabStop = false;
            // 
            // btnInhabilitados
            // 
            this.btnInhabilitados.BackColor = System.Drawing.Color.Purple;
            this.btnInhabilitados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInhabilitados.FlatAppearance.BorderSize = 0;
            this.btnInhabilitados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInhabilitados.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnInhabilitados.ForeColor = System.Drawing.Color.White;
            this.btnInhabilitados.Image = global::PuntoDeVentaV2.Properties.Resources.trash1;
            this.btnInhabilitados.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInhabilitados.Location = new System.Drawing.Point(201, 57);
            this.btnInhabilitados.Name = "btnInhabilitados";
            this.btnInhabilitados.Size = new System.Drawing.Size(189, 41);
            this.btnInhabilitados.TabIndex = 32;
            this.btnInhabilitados.Text = "Inhabilitados";
            this.btnInhabilitados.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInhabilitados.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnInhabilitados, "En este botón podrás visualizar tus productos \"Inhabilitados\" y habilitarlos nuev" +
        "amente.");
            this.btnInhabilitados.UseVisualStyleBackColor = false;
            this.btnInhabilitados.Click += new System.EventHandler(this.btnInhabilitados_Click);
            // 
            // btnDeleteDetalle
            // 
            this.btnDeleteDetalle.BackColor = System.Drawing.Color.DarkRed;
            this.btnDeleteDetalle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteDetalle.FlatAppearance.BorderSize = 0;
            this.btnDeleteDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnDeleteDetalle.ForeColor = System.Drawing.Color.White;
            this.btnDeleteDetalle.Image = global::PuntoDeVentaV2.Properties.Resources.times_rectangle_o1;
            this.btnDeleteDetalle.Location = new System.Drawing.Point(6, 57);
            this.btnDeleteDetalle.Name = "btnDeleteDetalle";
            this.btnDeleteDetalle.Size = new System.Drawing.Size(189, 41);
            this.btnDeleteDetalle.TabIndex = 31;
            this.btnDeleteDetalle.Text = "Inhabilitar";
            this.btnDeleteDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnDeleteDetalle, "En este botón podrá inhabilitar los detalles de sus productos/ servicios.");
            this.btnDeleteDetalle.UseVisualStyleBackColor = false;
            this.btnDeleteDetalle.Click += new System.EventHandler(this.btnDeleteDetalle_Click);
            // 
            // btnRenameDetalle
            // 
            this.btnRenameDetalle.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnRenameDetalle.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRenameDetalle.FlatAppearance.BorderSize = 0;
            this.btnRenameDetalle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRenameDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnRenameDetalle.ForeColor = System.Drawing.Color.White;
            this.btnRenameDetalle.Image = global::PuntoDeVentaV2.Properties.Resources.edit1;
            this.btnRenameDetalle.Location = new System.Drawing.Point(201, 12);
            this.btnRenameDetalle.Name = "btnRenameDetalle";
            this.btnRenameDetalle.Size = new System.Drawing.Size(189, 41);
            this.btnRenameDetalle.TabIndex = 30;
            this.btnRenameDetalle.Text = "Renombrar";
            this.btnRenameDetalle.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.btnRenameDetalle, "Podrás cambiar el nombre del detalle de tu producto/servicio.");
            this.btnRenameDetalle.UseVisualStyleBackColor = false;
            this.btnRenameDetalle.Click += new System.EventHandler(this.btnRenameDetalle_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnCerrar);
            this.groupBox2.Controls.Add(this.btnGuardarDetalles);
            this.groupBox2.Location = new System.Drawing.Point(707, 544);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 108);
            this.groupBox2.TabIndex = 31;
            this.groupBox2.TabStop = false;
            // 
            // btnCerrar
            // 
            this.btnCerrar.BackColor = System.Drawing.Color.LightSeaGreen;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Image = global::PuntoDeVentaV2.Properties.Resources.times1;
            this.btnCerrar.Location = new System.Drawing.Point(10, 59);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(201, 41);
            this.btnCerrar.TabIndex = 27;
            this.btnCerrar.Text = "Cancelar";
            this.btnCerrar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // timerStockMaximo
            // 
            this.timerStockMaximo.Interval = 1000;
            this.timerStockMaximo.Tick += new System.EventHandler(this.timerStockMaximo_Tick);
            // 
            // chkMensajeInventario
            // 
            this.chkMensajeInventario.AutoSize = true;
            this.chkMensajeInventario.Location = new System.Drawing.Point(437, 574);
            this.chkMensajeInventario.Name = "chkMensajeInventario";
            this.chkMensajeInventario.Size = new System.Drawing.Size(199, 17);
            this.chkMensajeInventario.TabIndex = 35;
            this.chkMensajeInventario.Text = "Mostrar mensaje al realizar inventario";
            this.chkMensajeInventario.UseVisualStyleBackColor = true;
            this.chkMensajeInventario.Visible = false;
            // 
            // chkBoxProductMessage
            // 
            this.chkBoxProductMessage.AutoSize = true;
            this.chkBoxProductMessage.Location = new System.Drawing.Point(437, 597);
            this.chkBoxProductMessage.Name = "chkBoxProductMessage";
            this.chkBoxProductMessage.Size = new System.Drawing.Size(173, 17);
            this.chkBoxProductMessage.TabIndex = 34;
            this.chkBoxProductMessage.Text = "Agrega un mensaje al producto";
            this.chkBoxProductMessage.UseVisualStyleBackColor = true;
            this.chkBoxProductMessage.Visible = false;
            // 
            // AgregarDetalleProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(937, 658);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.separadorInicial);
            this.Controls.Add(this.txtStockMinimo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtStockNecesario);
            this.Controls.Add(this.chkMensajeInventario);
            this.Controls.Add(this.chkBoxProductMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "AgregarDetalleProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Detalles Producto";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AgregarDetalleProducto_FormClosed);
            this.Load += new System.EventHandler(this.AgregarDetalleProducto_Load);
            this.Shown += new System.EventHandler(this.AgregarDetalleProducto_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AgregarDetalleProducto_KeyDown);
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
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtStockNecesario;
        private System.Windows.Forms.TextBox txtStockMinimo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timerStockMaximo;
        private System.Windows.Forms.CheckBox chkMensajeInventario;
        private System.Windows.Forms.CheckBox chkBoxProductMessage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnInhabilitados;
    }
}