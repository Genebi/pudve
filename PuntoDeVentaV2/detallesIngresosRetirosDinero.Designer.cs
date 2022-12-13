
namespace PuntoDeVentaV2
{
    partial class detallesIngresosRetirosDinero
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
            this.DGDetalles = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tipodemovimiento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.efectivo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tarjeta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.vales = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cheque = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.transferencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nombredeusuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechadeoperacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imprimir = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGDetalles)).BeginInit();
            this.SuspendLayout();
            // 
            // DGDetalles
            // 
            this.DGDetalles.AccessibleDescription = " ";
            this.DGDetalles.AllowUserToAddRows = false;
            this.DGDetalles.AllowUserToDeleteRows = false;
            this.DGDetalles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGDetalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGDetalles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.tipodemovimiento,
            this.concepto,
            this.cantidad,
            this.efectivo,
            this.tarjeta,
            this.vales,
            this.cheque,
            this.transferencia,
            this.nombredeusuario,
            this.fechadeoperacion,
            this.imprimir});
            this.DGDetalles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGDetalles.Location = new System.Drawing.Point(0, 0);
            this.DGDetalles.Name = "DGDetalles";
            this.DGDetalles.ReadOnly = true;
            this.DGDetalles.RowHeadersVisible = false;
            this.DGDetalles.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.DGDetalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGDetalles.Size = new System.Drawing.Size(1015, 371);
            this.DGDetalles.TabIndex = 7;
            this.DGDetalles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGDetalles_CellClick);
            // 
            // id
            // 
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // tipodemovimiento
            // 
            this.tipodemovimiento.HeaderText = "Tipo de Movimiento";
            this.tipodemovimiento.Name = "tipodemovimiento";
            this.tipodemovimiento.ReadOnly = true;
            // 
            // concepto
            // 
            this.concepto.HeaderText = "Concepto";
            this.concepto.Name = "concepto";
            this.concepto.ReadOnly = true;
            // 
            // cantidad
            // 
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            // 
            // efectivo
            // 
            this.efectivo.HeaderText = "Efectivo";
            this.efectivo.Name = "efectivo";
            this.efectivo.ReadOnly = true;
            // 
            // tarjeta
            // 
            this.tarjeta.HeaderText = "Tarjeta";
            this.tarjeta.Name = "tarjeta";
            this.tarjeta.ReadOnly = true;
            // 
            // vales
            // 
            this.vales.HeaderText = "Vales";
            this.vales.Name = "vales";
            this.vales.ReadOnly = true;
            // 
            // cheque
            // 
            this.cheque.HeaderText = "Cheque";
            this.cheque.Name = "cheque";
            this.cheque.ReadOnly = true;
            // 
            // transferencia
            // 
            this.transferencia.HeaderText = "Transferencia";
            this.transferencia.Name = "transferencia";
            this.transferencia.ReadOnly = true;
            // 
            // nombredeusuario
            // 
            this.nombredeusuario.HeaderText = "Nombre de Usuario";
            this.nombredeusuario.Name = "nombredeusuario";
            this.nombredeusuario.ReadOnly = true;
            // 
            // fechadeoperacion
            // 
            this.fechadeoperacion.HeaderText = "Fecha de Operacion";
            this.fechadeoperacion.Name = "fechadeoperacion";
            this.fechadeoperacion.ReadOnly = true;
            // 
            // imprimir
            // 
            this.imprimir.HeaderText = "Imprimir";
            this.imprimir.Name = "imprimir";
            this.imprimir.ReadOnly = true;
            // 
            // detallesIngresosRetirosDinero
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1015, 371);
            this.Controls.Add(this.DGDetalles);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "detallesIngresosRetirosDinero";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "detallesIngresosRetirosDinero";
            this.Load += new System.EventHandler(this.detallesIngresosRetirosDinero_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGDetalles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGDetalles;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn tipodemovimiento;
        private System.Windows.Forms.DataGridViewTextBoxColumn concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn efectivo;
        private System.Windows.Forms.DataGridViewTextBoxColumn tarjeta;
        private System.Windows.Forms.DataGridViewTextBoxColumn vales;
        private System.Windows.Forms.DataGridViewTextBoxColumn cheque;
        private System.Windows.Forms.DataGridViewTextBoxColumn transferencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn nombredeusuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechadeoperacion;
        private System.Windows.Forms.DataGridViewImageColumn imprimir;
    }
}