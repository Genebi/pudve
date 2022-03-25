
namespace PuntoDeVentaV2
{
    partial class SeleccionDeProductosParaExportarCSV
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
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.btnOK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.chbVerTodos = new System.Windows.Forms.CheckBox();
            this.lblEnWeb = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProductos
            // 
            this.dgvProductos.AllowUserToAddRows = false;
            this.dgvProductos.AllowUserToDeleteRows = false;
            this.dgvProductos.AllowUserToResizeColumns = false;
            this.dgvProductos.AllowUserToResizeRows = false;
            this.dgvProductos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(23, 27);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.ReadOnly = true;
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.Size = new System.Drawing.Size(577, 303);
            this.dgvProductos.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(413, 354);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(186, 28);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "ok";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(23, 354);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(186, 28);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "Cancelar";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // chbVerTodos
            // 
            this.chbVerTodos.AutoSize = true;
            this.chbVerTodos.Location = new System.Drawing.Point(23, 4);
            this.chbVerTodos.Name = "chbVerTodos";
            this.chbVerTodos.Size = new System.Drawing.Size(75, 17);
            this.chbVerTodos.TabIndex = 2;
            this.chbVerTodos.Text = "Ver Todos";
            this.chbVerTodos.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.chbVerTodos.UseVisualStyleBackColor = true;
            this.chbVerTodos.CheckedChanged += new System.EventHandler(this.chbVerTodos_CheckedChanged);
            // 
            // lblEnWeb
            // 
            this.lblEnWeb.AutoSize = true;
            this.lblEnWeb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblEnWeb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnWeb.Location = new System.Drawing.Point(394, 5);
            this.lblEnWeb.Name = "lblEnWeb";
            this.lblEnWeb.Size = new System.Drawing.Size(205, 13);
            this.lblEnWeb.TabIndex = 3;
            this.lblEnWeb.Text = "Cambiar los productos que aparecen aquí";
            this.lblEnWeb.Click += new System.EventHandler(this.label1_Click);
            // 
            // SeleccionDeProductosParaExportarCSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 394);
            this.Controls.Add(this.lblEnWeb);
            this.Controls.Add(this.chbVerTodos);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dgvProductos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SeleccionDeProductosParaExportarCSV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SeleccionDeProductosParaExportarCSV";
            this.Load += new System.EventHandler(this.SeleccionDeProductosParaExportarCSV_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.CheckBox chbVerTodos;
        private System.Windows.Forms.Label lblEnWeb;
    }
}