
namespace PuntoDeVentaV2
{
    partial class importarExcelProductos
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CBNombre = new System.Windows.Forms.ComboBox();
            this.CBStockMin = new System.Windows.Forms.ComboBox();
            this.CBStockMax = new System.Windows.Forms.ComboBox();
            this.CBProveedor = new System.Windows.Forms.ComboBox();
            this.CBCodigo = new System.Windows.Forms.ComboBox();
            this.CBPrecioCompra = new System.Windows.Forms.ComboBox();
            this.CBPrecioVenta = new System.Windows.Forms.ComboBox();
            this.CBClaveSat = new System.Windows.Forms.ComboBox();
            this.CBUnidadM = new System.Windows.Forms.ComboBox();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(73, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(639, 29);
            this.label1.TabIndex = 7;
            this.label1.Text = "Importar lista de productos desde un archivo de Excel";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(513, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Columnas SIFO";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(482, 143);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ShortcutsEnabled = false;
            this.textBox1.Size = new System.Drawing.Size(215, 26);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Nombre del producto";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(482, 185);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(215, 26);
            this.textBox2.TabIndex = 9;
            this.textBox2.Text = "Stock mínimo";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(482, 228);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(215, 26);
            this.textBox3.TabIndex = 9;
            this.textBox3.Text = "Stock máximo ";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(482, 272);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(215, 26);
            this.textBox4.TabIndex = 9;
            this.textBox4.Text = "Proveedor";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(482, 316);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(215, 26);
            this.textBox5.TabIndex = 9;
            this.textBox5.Text = "Código";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            this.textBox6.Enabled = false;
            this.textBox6.Location = new System.Drawing.Point(482, 360);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(215, 26);
            this.textBox6.TabIndex = 9;
            this.textBox6.Text = "Precio de compra";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox7
            // 
            this.textBox7.Enabled = false;
            this.textBox7.Location = new System.Drawing.Point(482, 403);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(215, 26);
            this.textBox7.TabIndex = 9;
            this.textBox7.Text = "Precio de venta";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox8
            // 
            this.textBox8.Enabled = false;
            this.textBox8.Location = new System.Drawing.Point(482, 446);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(215, 26);
            this.textBox8.TabIndex = 9;
            this.textBox8.Text = "Clave SAT";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox9
            // 
            this.textBox9.Enabled = false;
            this.textBox9.Location = new System.Drawing.Point(482, 489);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(215, 26);
            this.textBox9.TabIndex = 9;
            this.textBox9.Text = "Unidad de medida";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(99, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(203, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "Columnas Importadas";
            // 
            // CBNombre
            // 
            this.CBNombre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBNombre.FormattingEnabled = true;
            this.CBNombre.Location = new System.Drawing.Point(93, 143);
            this.CBNombre.MaxDropDownItems = 99;
            this.CBNombre.Name = "CBNombre";
            this.CBNombre.Size = new System.Drawing.Size(215, 28);
            this.CBNombre.TabIndex = 1;
            // 
            // CBStockMin
            // 
            this.CBStockMin.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBStockMin.FormattingEnabled = true;
            this.CBStockMin.Location = new System.Drawing.Point(93, 183);
            this.CBStockMin.Name = "CBStockMin";
            this.CBStockMin.Size = new System.Drawing.Size(215, 28);
            this.CBStockMin.TabIndex = 2;
            // 
            // CBStockMax
            // 
            this.CBStockMax.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBStockMax.FormattingEnabled = true;
            this.CBStockMax.Location = new System.Drawing.Point(93, 226);
            this.CBStockMax.Name = "CBStockMax";
            this.CBStockMax.Size = new System.Drawing.Size(215, 28);
            this.CBStockMax.TabIndex = 3;
            // 
            // CBProveedor
            // 
            this.CBProveedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBProveedor.FormattingEnabled = true;
            this.CBProveedor.Location = new System.Drawing.Point(93, 270);
            this.CBProveedor.Name = "CBProveedor";
            this.CBProveedor.Size = new System.Drawing.Size(215, 28);
            this.CBProveedor.TabIndex = 4;
            // 
            // CBCodigo
            // 
            this.CBCodigo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBCodigo.FormattingEnabled = true;
            this.CBCodigo.Location = new System.Drawing.Point(93, 314);
            this.CBCodigo.Name = "CBCodigo";
            this.CBCodigo.Size = new System.Drawing.Size(215, 28);
            this.CBCodigo.TabIndex = 5;
            // 
            // CBPrecioCompra
            // 
            this.CBPrecioCompra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBPrecioCompra.FormattingEnabled = true;
            this.CBPrecioCompra.Location = new System.Drawing.Point(93, 358);
            this.CBPrecioCompra.Name = "CBPrecioCompra";
            this.CBPrecioCompra.Size = new System.Drawing.Size(215, 28);
            this.CBPrecioCompra.TabIndex = 6;
            // 
            // CBPrecioVenta
            // 
            this.CBPrecioVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBPrecioVenta.FormattingEnabled = true;
            this.CBPrecioVenta.Location = new System.Drawing.Point(93, 401);
            this.CBPrecioVenta.Name = "CBPrecioVenta";
            this.CBPrecioVenta.Size = new System.Drawing.Size(215, 28);
            this.CBPrecioVenta.TabIndex = 7;
            // 
            // CBClaveSat
            // 
            this.CBClaveSat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBClaveSat.FormattingEnabled = true;
            this.CBClaveSat.Location = new System.Drawing.Point(93, 444);
            this.CBClaveSat.Name = "CBClaveSat";
            this.CBClaveSat.Size = new System.Drawing.Size(215, 28);
            this.CBClaveSat.TabIndex = 8;
            // 
            // CBUnidadM
            // 
            this.CBUnidadM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CBUnidadM.FormattingEnabled = true;
            this.CBUnidadM.Location = new System.Drawing.Point(93, 487);
            this.CBUnidadM.Name = "CBUnidadM";
            this.CBUnidadM.Size = new System.Drawing.Size(215, 28);
            this.CBUnidadM.TabIndex = 9;
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_aceptar.BackColor = System.Drawing.Color.Green;
            this.btn_aceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_aceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_aceptar.ForeColor = System.Drawing.Color.White;
            this.btn_aceptar.Location = new System.Drawing.Point(78, 562);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(245, 72);
            this.btn_aceptar.TabIndex = 10;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = false;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancelar.BackColor = System.Drawing.Color.Red;
            this.btn_cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancelar.ForeColor = System.Drawing.Color.White;
            this.btn_cancelar.Location = new System.Drawing.Point(467, 562);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(245, 72);
            this.btn_cancelar.TabIndex = 11;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // importarExcelProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 660);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.CBUnidadM);
            this.Controls.Add(this.CBClaveSat);
            this.Controls.Add(this.CBPrecioVenta);
            this.Controls.Add(this.CBPrecioCompra);
            this.Controls.Add(this.CBCodigo);
            this.Controls.Add(this.CBProveedor);
            this.Controls.Add(this.CBStockMax);
            this.Controls.Add(this.CBStockMin);
            this.Controls.Add(this.CBNombre);
            this.Controls.Add(this.textBox9);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "importarExcelProductos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importar Productos desde Excel";
            this.Load += new System.EventHandler(this.importarExcelProductos_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox CBNombre;
        private System.Windows.Forms.ComboBox CBStockMin;
        private System.Windows.Forms.ComboBox CBStockMax;
        private System.Windows.Forms.ComboBox CBProveedor;
        private System.Windows.Forms.ComboBox CBCodigo;
        private System.Windows.Forms.ComboBox CBPrecioCompra;
        private System.Windows.Forms.ComboBox CBPrecioVenta;
        private System.Windows.Forms.ComboBox CBClaveSat;
        private System.Windows.Forms.ComboBox CBUnidadM;
        private System.Windows.Forms.Button btn_aceptar;
        private System.Windows.Forms.Button btn_cancelar;
    }
}