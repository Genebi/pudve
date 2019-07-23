namespace PuntoDeVentaV2
{
    partial class datosExtraNvoProductos
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
            this.txtCantidadProdNvo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDescuentoProdNvo = new System.Windows.Forms.TextBox();
            this.txtImporteProdNvo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtCantidadProdNvo);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDescuentoProdNvo);
            this.groupBox1.Controls.Add(this.txtImporteProdNvo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 205);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Datos Complementarios del Producto: ";
            // 
            // txtCantidadProdNvo
            // 
            this.txtCantidadProdNvo.Location = new System.Drawing.Point(125, 136);
            this.txtCantidadProdNvo.Name = "txtCantidadProdNvo";
            this.txtCantidadProdNvo.Size = new System.Drawing.Size(195, 20);
            this.txtCantidadProdNvo.TabIndex = 5;
            this.txtCantidadProdNvo.Text = "0";
            this.txtCantidadProdNvo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCantidadProdNvo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCantidadProdNvo_KeyDown);
            this.txtCantidadProdNvo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadProdNvo_KeyPress);
            this.txtCantidadProdNvo.Leave += new System.EventHandler(this.txtCantidadProdNvo_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Cantidad:";
            // 
            // txtDescuentoProdNvo
            // 
            this.txtDescuentoProdNvo.Location = new System.Drawing.Point(125, 84);
            this.txtDescuentoProdNvo.Name = "txtDescuentoProdNvo";
            this.txtDescuentoProdNvo.Size = new System.Drawing.Size(195, 20);
            this.txtDescuentoProdNvo.TabIndex = 3;
            this.txtDescuentoProdNvo.Text = "0";
            this.txtDescuentoProdNvo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDescuentoProdNvo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDescuentoProdNvo_KeyDown);
            this.txtDescuentoProdNvo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescuentoProdNvo_KeyPress);
            this.txtDescuentoProdNvo.Leave += new System.EventHandler(this.txtDescuentoProdNvo_Leave);
            // 
            // txtImporteProdNvo
            // 
            this.txtImporteProdNvo.Location = new System.Drawing.Point(125, 40);
            this.txtImporteProdNvo.Name = "txtImporteProdNvo";
            this.txtImporteProdNvo.Size = new System.Drawing.Size(195, 20);
            this.txtImporteProdNvo.TabIndex = 2;
            this.txtImporteProdNvo.Text = "0";
            this.txtImporteProdNvo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtImporteProdNvo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtImporteProdNvo_KeyDown);
            this.txtImporteProdNvo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtImporteProdNvo_KeyPress);
            this.txtImporteProdNvo.Leave += new System.EventHandler(this.txtImporteProdNvo_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Descuento:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Importe:";
            // 
            // datosExtraNvoProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 229);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "datosExtraNvoProductos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Datos extra Producto Nuevo.";
            this.Load += new System.EventHandler(this.datosExtraNvoProductos_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDescuentoProdNvo;
        private System.Windows.Forms.TextBox txtImporteProdNvo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCantidadProdNvo;
        private System.Windows.Forms.Label label3;
    }
}