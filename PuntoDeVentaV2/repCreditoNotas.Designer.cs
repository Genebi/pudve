
namespace PuntoDeVentaV2
{
    partial class repCreditoNotas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(repCreditoNotas));
            this.DGVMonosas = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Serie = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Agregar = new System.Windows.Forms.DataGridViewImageColumn();
            this.ticket = new System.Windows.Forms.DataGridViewImageColumn();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMonosas)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVMonosas
            // 
            this.DGVMonosas.AllowUserToAddRows = false;
            this.DGVMonosas.AllowUserToDeleteRows = false;
            this.DGVMonosas.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DGVMonosas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVMonosas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DGVMonosas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVMonosas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Folio,
            this.Serie,
            this.Total,
            this.Fecha,
            this.Agregar,
            this.ticket});
            this.DGVMonosas.Location = new System.Drawing.Point(12, 57);
            this.DGVMonosas.Name = "DGVMonosas";
            this.DGVMonosas.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVMonosas.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.DGVMonosas.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.DGVMonosas.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.DGVMonosas.Size = new System.Drawing.Size(652, 196);
            this.DGVMonosas.TabIndex = 1;
            this.DGVMonosas.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVMonosas_CellClick);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ID.DefaultCellStyle = dataGridViewCellStyle2;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Folio
            // 
            this.Folio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Folio.DataPropertyName = "Folio";
            this.Folio.HeaderText = "Folio";
            this.Folio.Name = "Folio";
            this.Folio.ReadOnly = true;
            // 
            // Serie
            // 
            this.Serie.DataPropertyName = "Serie";
            this.Serie.HeaderText = "Serie";
            this.Serie.Name = "Serie";
            this.Serie.ReadOnly = true;
            // 
            // Total
            // 
            this.Total.DataPropertyName = "Total";
            this.Total.HeaderText = "Total";
            this.Total.Name = "Total";
            this.Total.ReadOnly = true;
            // 
            // Fecha
            // 
            this.Fecha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Fecha.DataPropertyName = "Fecha";
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            // 
            // Agregar
            // 
            this.Agregar.Description = "Seleccionar";
            this.Agregar.HeaderText = "";
            this.Agregar.Image = global::PuntoDeVentaV2.Properties.Resources.file_text;
            this.Agregar.Name = "Agregar";
            this.Agregar.ReadOnly = true;
            this.Agregar.Width = 50;
            // 
            // ticket
            // 
            this.ticket.HeaderText = "";
            this.ticket.Image = global::PuntoDeVentaV2.Properties.Resources.ticket;
            this.ticket.Name = "ticket";
            this.ticket.ReadOnly = true;
            this.ticket.Width = 50;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(12, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(133, 17);
            this.lblTitle.TabIndex = 4;
            this.lblTitle.Text = "Ventas abiertas de:";
            // 
            // repCreditoNotas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 265);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.DGVMonosas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "repCreditoNotas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ventas abiertas";
            this.Load += new System.EventHandler(this.repCreditoNotas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVMonosas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVMonosas;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Serie;
        private System.Windows.Forms.DataGridViewTextBoxColumn Total;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewImageColumn Agregar;
        private System.Windows.Forms.DataGridViewImageColumn ticket;
    }
}