namespace PuntoDeVentaV2
{
    partial class PedidoPorProducto
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
            this.btnDoPrintOrderList = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnDoPrintOrderList
            // 
            this.btnDoPrintOrderList.Font = new System.Drawing.Font("Century", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnDoPrintOrderList.Location = new System.Drawing.Point(48, 12);
            this.btnDoPrintOrderList.Name = "btnDoPrintOrderList";
            this.btnDoPrintOrderList.Size = new System.Drawing.Size(88, 46);
            this.btnDoPrintOrderList.TabIndex = 0;
            this.btnDoPrintOrderList.Text = "Imprimir Pedido";
            this.btnDoPrintOrderList.UseVisualStyleBackColor = true;
            this.btnDoPrintOrderList.Click += new System.EventHandler(this.btnDoPrintOrderList_Click);
            // 
            // PedidoPorProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(198, 70);
            this.Controls.Add(this.btnDoPrintOrderList);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PedidoPorProducto";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pedido por Producto";
            this.Load += new System.EventHandler(this.PedidoPorProducto_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDoPrintOrderList;
    }
}