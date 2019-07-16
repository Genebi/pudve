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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtBoxBuscarCodigoBarras = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblNoRegistro = new System.Windows.Forms.Label();
            this.txtCantidadStock = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAumentarStock = new System.Windows.Forms.Button();
            this.btnReducirStock = new System.Windows.Forms.Button();
            this.lblCodigoDeBarras = new System.Windows.Forms.Label();
            this.lblNombreProducto = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnSiguiente = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNoRevision = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtBoxBuscarCodigoBarras);
            this.groupBox1.Location = new System.Drawing.Point(12, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(420, 93);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Código de Barras:";
            // 
            // txtBoxBuscarCodigoBarras
            // 
            this.txtBoxBuscarCodigoBarras.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtBoxBuscarCodigoBarras.Font = new System.Drawing.Font("Century", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBoxBuscarCodigoBarras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtBoxBuscarCodigoBarras.Location = new System.Drawing.Point(40, 27);
            this.txtBoxBuscarCodigoBarras.Name = "txtBoxBuscarCodigoBarras";
            this.txtBoxBuscarCodigoBarras.Size = new System.Drawing.Size(344, 33);
            this.txtBoxBuscarCodigoBarras.TabIndex = 0;
            this.txtBoxBuscarCodigoBarras.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtBoxBuscarCodigoBarras.TextChanged += new System.EventHandler(this.txtBoxBuscarCodigoBarras_TextChanged);
            this.txtBoxBuscarCodigoBarras.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtBoxBuscarCodigoBarras_KeyPress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblNoRegistro);
            this.groupBox2.Controls.Add(this.txtCantidadStock);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btnAumentarStock);
            this.groupBox2.Controls.Add(this.btnReducirStock);
            this.groupBox2.Controls.Add(this.lblCodigoDeBarras);
            this.groupBox2.Controls.Add(this.lblNombreProducto);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 158);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(420, 305);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Datos del Producto: ";
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
            this.txtCantidadStock.Font = new System.Drawing.Font("Century", 48F, System.Drawing.FontStyle.Bold);
            this.txtCantidadStock.ForeColor = System.Drawing.Color.Blue;
            this.txtCantidadStock.Location = new System.Drawing.Point(99, 207);
            this.txtCantidadStock.Name = "txtCantidadStock";
            this.txtCantidadStock.Size = new System.Drawing.Size(225, 84);
            this.txtCantidadStock.TabIndex = 8;
            this.txtCantidadStock.Text = "0";
            this.txtCantidadStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCantidadStock.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadStock_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 185);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 7;
            this.label6.Text = "Stock Existente : ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Código de Barras : ";
            // 
            // btnAumentarStock
            // 
            this.btnAumentarStock.Image = global::PuntoDeVentaV2.Properties.Resources.plus_square1;
            this.btnAumentarStock.Location = new System.Drawing.Point(330, 206);
            this.btnAumentarStock.Name = "btnAumentarStock";
            this.btnAumentarStock.Size = new System.Drawing.Size(75, 85);
            this.btnAumentarStock.TabIndex = 5;
            this.btnAumentarStock.UseVisualStyleBackColor = true;
            this.btnAumentarStock.Click += new System.EventHandler(this.btnAumentarStock_Click);
            // 
            // btnReducirStock
            // 
            this.btnReducirStock.Image = global::PuntoDeVentaV2.Properties.Resources.minus_square1;
            this.btnReducirStock.Location = new System.Drawing.Point(18, 206);
            this.btnReducirStock.Name = "btnReducirStock";
            this.btnReducirStock.Size = new System.Drawing.Size(75, 85);
            this.btnReducirStock.TabIndex = 4;
            this.btnReducirStock.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnReducirStock.UseVisualStyleBackColor = true;
            this.btnReducirStock.Click += new System.EventHandler(this.btnReducirStock_Click);
            // 
            // lblCodigoDeBarras
            // 
            this.lblCodigoDeBarras.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCodigoDeBarras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblCodigoDeBarras.Font = new System.Drawing.Font("Century", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCodigoDeBarras.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblCodigoDeBarras.Location = new System.Drawing.Point(18, 139);
            this.lblCodigoDeBarras.Name = "lblCodigoDeBarras";
            this.lblCodigoDeBarras.Size = new System.Drawing.Size(387, 32);
            this.lblCodigoDeBarras.TabIndex = 2;
            this.lblCodigoDeBarras.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblNombreProducto
            // 
            this.lblNombreProducto.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNombreProducto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(167)))), ((int)(((byte)(167)))), ((int)(((byte)(169)))));
            this.lblNombreProducto.Font = new System.Drawing.Font("Century Schoolbook", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreProducto.ForeColor = System.Drawing.Color.White;
            this.lblNombreProducto.Location = new System.Drawing.Point(18, 41);
            this.lblNombreProducto.Name = "lblNombreProducto";
            this.lblNombreProducto.Size = new System.Drawing.Size(387, 67);
            this.lblNombreProducto.TabIndex = 1;
            this.lblNombreProducto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre : ";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Controls.Add(this.btnSiguiente);
            this.groupBox3.Location = new System.Drawing.Point(12, 470);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(420, 100);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Century751 SeBd BT", 21.75F, System.Drawing.FontStyle.Italic);
            this.button1.Location = new System.Drawing.Point(225, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(171, 55);
            this.button1.TabIndex = 1;
            this.button1.Text = "Terminar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSiguiente
            // 
            this.btnSiguiente.Font = new System.Drawing.Font("Century751 SeBd BT", 21.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSiguiente.ForeColor = System.Drawing.Color.Red;
            this.btnSiguiente.Location = new System.Drawing.Point(23, 25);
            this.btnSiguiente.Name = "btnSiguiente";
            this.btnSiguiente.Size = new System.Drawing.Size(171, 55);
            this.btnSiguiente.TabIndex = 0;
            this.btnSiguiente.Text = "Siguiente";
            this.btnSiguiente.UseVisualStyleBackColor = true;
            this.btnSiguiente.Click += new System.EventHandler(this.btnSiguiente_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Número de Revisión:";
            // 
            // lblNoRevision
            // 
            this.lblNoRevision.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.lblNoRevision.Font = new System.Drawing.Font("Century Gothic", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoRevision.ForeColor = System.Drawing.Color.Blue;
            this.lblNoRevision.Location = new System.Drawing.Point(190, 18);
            this.lblNoRevision.Name = "lblNoRevision";
            this.lblNoRevision.Size = new System.Drawing.Size(64, 23);
            this.lblNoRevision.TabIndex = 4;
            this.lblNoRevision.Text = "0";
            this.lblNoRevision.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RevisarInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 584);
            this.Controls.Add(this.lblNoRevision);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RevisarInventario";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Revisar Stock Fisico";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RevisarInventario_FormClosing);
            this.Load += new System.EventHandler(this.RevisarInventario_Load);
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
        private System.Windows.Forms.TextBox txtBoxBuscarCodigoBarras;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblNombreProducto;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCodigoDeBarras;
        private System.Windows.Forms.Button btnReducirStock;
        private System.Windows.Forms.Button btnAumentarStock;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSiguiente;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCantidadStock;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblNoRegistro;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNoRevision;
    }
}