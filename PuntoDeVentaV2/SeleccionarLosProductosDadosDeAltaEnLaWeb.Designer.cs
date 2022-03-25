
namespace PuntoDeVentaV2
{
    partial class SeleccionarLosProductosDadosDeAltaEnLaWeb
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
            this.dgvNel = new System.Windows.Forms.DataGridView();
            this.dgvSis = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSis)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNel
            // 
            this.dgvNel.AllowUserToAddRows = false;
            this.dgvNel.AllowUserToDeleteRows = false;
            this.dgvNel.AllowUserToResizeColumns = false;
            this.dgvNel.AllowUserToResizeRows = false;
            this.dgvNel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvNel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNel.Location = new System.Drawing.Point(12, 35);
            this.dgvNel.MultiSelect = false;
            this.dgvNel.Name = "dgvNel";
            this.dgvNel.RowHeadersVisible = false;
            this.dgvNel.Size = new System.Drawing.Size(374, 403);
            this.dgvNel.TabIndex = 0;
            this.dgvNel.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNel_CellClick);
            // 
            // dgvSis
            // 
            this.dgvSis.AllowUserToAddRows = false;
            this.dgvSis.AllowUserToDeleteRows = false;
            this.dgvSis.AllowUserToResizeColumns = false;
            this.dgvSis.AllowUserToResizeRows = false;
            this.dgvSis.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSis.Location = new System.Drawing.Point(414, 35);
            this.dgvSis.MultiSelect = false;
            this.dgvSis.Name = "dgvSis";
            this.dgvSis.RowHeadersVisible = false;
            this.dgvSis.Size = new System.Drawing.Size(374, 403);
            this.dgvSis.TabIndex = 0;
            this.dgvSis.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvSis_CellClick);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(374, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Todos los productos";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(411, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(377, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Productos registrados en la web";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(12, 444);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(186, 28);
            this.Cancel.TabIndex = 2;
            this.Cancel.Text = "Cerrar";
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // SeleccionarLosProductosDadosDeAltaEnLaWeb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 482);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvSis);
            this.Controls.Add(this.dgvNel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SeleccionarLosProductosDadosDeAltaEnLaWeb";
            this.Text = "SeleccionarLosProductosDadosDeAltaEnLaWeb";
            this.Load += new System.EventHandler(this.SeleccionarLosProductosDadosDeAltaEnLaWeb_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNel;
        private System.Windows.Forms.DataGridView dgvSis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Cancel;
    }
}