
namespace PuntoDeVentaV2
{
    partial class HistorialAnticipos
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
            this.DGVAnticipos = new System.Windows.Forms.DataGridView();
            this.idventa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comentarios = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalrecibido = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.anticipoaplicado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saldorestante = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaoperacion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imagen = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGVAnticipos)).BeginInit();
            this.SuspendLayout();
            // 
            // DGVAnticipos
            // 
            this.DGVAnticipos.AllowUserToAddRows = false;
            this.DGVAnticipos.AllowUserToDeleteRows = false;
            this.DGVAnticipos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVAnticipos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVAnticipos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idventa,
            this.empleado,
            this.concepto,
            this.cliente,
            this.comentarios,
            this.totalrecibido,
            this.anticipoaplicado,
            this.saldorestante,
            this.fechaoperacion,
            this.imagen});
            this.DGVAnticipos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGVAnticipos.Location = new System.Drawing.Point(0, 0);
            this.DGVAnticipos.Name = "DGVAnticipos";
            this.DGVAnticipos.ReadOnly = true;
            this.DGVAnticipos.RowHeadersVisible = false;
            this.DGVAnticipos.Size = new System.Drawing.Size(779, 370);
            this.DGVAnticipos.TabIndex = 0;
            this.DGVAnticipos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVAnticipos_CellClick);
            // 
            // idventa
            // 
            this.idventa.HeaderText = "ID Venta";
            this.idventa.Name = "idventa";
            this.idventa.ReadOnly = true;
            // 
            // empleado
            // 
            this.empleado.HeaderText = "Empleado";
            this.empleado.Name = "empleado";
            this.empleado.ReadOnly = true;
            // 
            // concepto
            // 
            this.concepto.HeaderText = "Concepto";
            this.concepto.Name = "concepto";
            this.concepto.ReadOnly = true;
            // 
            // cliente
            // 
            this.cliente.HeaderText = "Cliente";
            this.cliente.Name = "cliente";
            this.cliente.ReadOnly = true;
            // 
            // comentarios
            // 
            this.comentarios.HeaderText = "Comentarios";
            this.comentarios.Name = "comentarios";
            this.comentarios.ReadOnly = true;
            // 
            // totalrecibido
            // 
            this.totalrecibido.HeaderText = "Total Recibido";
            this.totalrecibido.Name = "totalrecibido";
            this.totalrecibido.ReadOnly = true;
            // 
            // anticipoaplicado
            // 
            this.anticipoaplicado.HeaderText = "Anticipo Aplicado";
            this.anticipoaplicado.Name = "anticipoaplicado";
            this.anticipoaplicado.ReadOnly = true;
            // 
            // saldorestante
            // 
            this.saldorestante.HeaderText = "Saldo Restante";
            this.saldorestante.Name = "saldorestante";
            this.saldorestante.ReadOnly = true;
            // 
            // fechaoperacion
            // 
            this.fechaoperacion.HeaderText = "Fecha de Operacion";
            this.fechaoperacion.Name = "fechaoperacion";
            this.fechaoperacion.ReadOnly = true;
            // 
            // imagen
            // 
            this.imagen.HeaderText = "Ver Ticket";
            this.imagen.Name = "imagen";
            this.imagen.ReadOnly = true;
            // 
            // HistorialAnticipos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(779, 370);
            this.Controls.Add(this.DGVAnticipos);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HistorialAnticipos";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Historial";
            this.Load += new System.EventHandler(this.HistorialAnticipos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVAnticipos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DGVAnticipos;
        private System.Windows.Forms.DataGridViewTextBoxColumn idventa;
        private System.Windows.Forms.DataGridViewTextBoxColumn empleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn comentarios;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalrecibido;
        private System.Windows.Forms.DataGridViewTextBoxColumn anticipoaplicado;
        private System.Windows.Forms.DataGridViewTextBoxColumn saldorestante;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaoperacion;
        private System.Windows.Forms.DataGridViewImageColumn imagen;
    }
}