﻿namespace PuntoDeVentaV2
{
    partial class Anticipos
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
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.dpFechaFinal = new System.Windows.Forms.DateTimePicker();
            this.dpFechaInicial = new System.Windows.Forms.DateTimePicker();
            this.btnNuevoAnticipo = new System.Windows.Forms.Button();
            this.btnBuscarAnticipos = new System.Windows.Forms.Button();
            this.cbAnticipos = new System.Windows.Forms.ComboBox();
            this.DGVAnticipos = new System.Windows.Forms.DataGridView();
            this.TTMensaje = new System.Windows.Forms.ToolTip(this.components);
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Concepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Importe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cliente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Empleado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ticket = new System.Windows.Forms.DataGridViewImageColumn();
            this.Status = new System.Windows.Forms.DataGridViewImageColumn();
            this.Devolver = new System.Windows.Forms.DataGridViewImageColumn();
            this.Info = new System.Windows.Forms.DataGridViewImageColumn();
            this.IDVenta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVAnticipos)).BeginInit();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(388, 27);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(122, 25);
            this.tituloSeccion.TabIndex = 5;
            this.tituloSeccion.Text = "ANTICIPOS";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panelBotones
            // 
            this.panelBotones.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelBotones.Controls.Add(this.dpFechaFinal);
            this.panelBotones.Controls.Add(this.dpFechaInicial);
            this.panelBotones.Controls.Add(this.btnNuevoAnticipo);
            this.panelBotones.Controls.Add(this.btnBuscarAnticipos);
            this.panelBotones.Controls.Add(this.cbAnticipos);
            this.panelBotones.Location = new System.Drawing.Point(12, 77);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(845, 50);
            this.panelBotones.TabIndex = 7;
            // 
            // dpFechaFinal
            // 
            this.dpFechaFinal.CustomFormat = "yyyy-MM-dd";
            this.dpFechaFinal.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpFechaFinal.Location = new System.Drawing.Point(367, 16);
            this.dpFechaFinal.Name = "dpFechaFinal";
            this.dpFechaFinal.Size = new System.Drawing.Size(145, 23);
            this.dpFechaFinal.TabIndex = 7;
            // 
            // dpFechaInicial
            // 
            this.dpFechaInicial.CustomFormat = "yyyy-MM-dd";
            this.dpFechaInicial.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpFechaInicial.Location = new System.Drawing.Point(205, 16);
            this.dpFechaInicial.Name = "dpFechaInicial";
            this.dpFechaInicial.Size = new System.Drawing.Size(145, 23);
            this.dpFechaInicial.TabIndex = 6;
            // 
            // btnNuevoAnticipo
            // 
            this.btnNuevoAnticipo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNuevoAnticipo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnNuevoAnticipo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevoAnticipo.FlatAppearance.BorderSize = 0;
            this.btnNuevoAnticipo.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnNuevoAnticipo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnNuevoAnticipo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoAnticipo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoAnticipo.ForeColor = System.Drawing.Color.White;
            this.btnNuevoAnticipo.Location = new System.Drawing.Point(716, 18);
            this.btnNuevoAnticipo.Name = "btnNuevoAnticipo";
            this.btnNuevoAnticipo.Size = new System.Drawing.Size(125, 24);
            this.btnNuevoAnticipo.TabIndex = 5;
            this.btnNuevoAnticipo.Text = "Nuevo Anticipo";
            this.btnNuevoAnticipo.UseVisualStyleBackColor = false;
            this.btnNuevoAnticipo.Click += new System.EventHandler(this.btnNuevoAnticipo_Click);
            // 
            // btnBuscarAnticipos
            // 
            this.btnBuscarAnticipos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnBuscarAnticipos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscarAnticipos.FlatAppearance.BorderSize = 0;
            this.btnBuscarAnticipos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnBuscarAnticipos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnBuscarAnticipos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscarAnticipos.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarAnticipos.ForeColor = System.Drawing.Color.White;
            this.btnBuscarAnticipos.Location = new System.Drawing.Point(530, 16);
            this.btnBuscarAnticipos.Name = "btnBuscarAnticipos";
            this.btnBuscarAnticipos.Size = new System.Drawing.Size(75, 24);
            this.btnBuscarAnticipos.TabIndex = 4;
            this.btnBuscarAnticipos.Text = "Buscar";
            this.btnBuscarAnticipos.UseVisualStyleBackColor = false;
            this.btnBuscarAnticipos.Click += new System.EventHandler(this.btnBuscarAnticipos_Click);
            // 
            // cbAnticipos
            // 
            this.cbAnticipos.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAnticipos.FormattingEnabled = true;
            this.cbAnticipos.Items.AddRange(new object[] {
            "Por usar",
            "Inhabilitados",
            "Usados"});
            this.cbAnticipos.Location = new System.Drawing.Point(3, 16);
            this.cbAnticipos.Name = "cbAnticipos";
            this.cbAnticipos.Size = new System.Drawing.Size(185, 24);
            this.cbAnticipos.TabIndex = 0;
            this.cbAnticipos.SelectedIndexChanged += new System.EventHandler(this.cbAnticipos_SelectedIndexChanged);
            // 
            // DGVAnticipos
            // 
            this.DGVAnticipos.AllowUserToAddRows = false;
            this.DGVAnticipos.AllowUserToDeleteRows = false;
            this.DGVAnticipos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVAnticipos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVAnticipos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Concepto,
            this.Importe,
            this.Cliente,
            this.Empleado,
            this.Fecha,
            this.Ticket,
            this.Status,
            this.Devolver,
            this.Info,
            this.IDVenta});
            this.DGVAnticipos.Location = new System.Drawing.Point(12, 141);
            this.DGVAnticipos.Name = "DGVAnticipos";
            this.DGVAnticipos.ReadOnly = true;
            this.DGVAnticipos.RowHeadersVisible = false;
            this.DGVAnticipos.Size = new System.Drawing.Size(845, 217);
            this.DGVAnticipos.TabIndex = 8;
            this.DGVAnticipos.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVAnticipos_CellClick);
            this.DGVAnticipos.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVAnticipos_CellMouseEnter);
            // 
            // TTMensaje
            // 
            this.TTMensaje.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.TTMensaje.ForeColor = System.Drawing.Color.White;
            this.TTMensaje.OwnerDraw = true;
            this.TTMensaje.Draw += new System.Windows.Forms.DrawToolTipEventHandler(this.TTMensaje_Draw);
            // 
            // ID
            // 
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            // 
            // Concepto
            // 
            this.Concepto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Concepto.HeaderText = "Concepto";
            this.Concepto.Name = "Concepto";
            this.Concepto.ReadOnly = true;
            // 
            // Importe
            // 
            this.Importe.HeaderText = "Importe";
            this.Importe.Name = "Importe";
            this.Importe.ReadOnly = true;
            this.Importe.Width = 135;
            // 
            // Cliente
            // 
            this.Cliente.HeaderText = "Cliente";
            this.Cliente.Name = "Cliente";
            this.Cliente.ReadOnly = true;
            this.Cliente.Width = 135;
            // 
            // Empleado
            // 
            this.Empleado.HeaderText = "Empleado";
            this.Empleado.Name = "Empleado";
            this.Empleado.ReadOnly = true;
            this.Empleado.Width = 135;
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 135;
            // 
            // Ticket
            // 
            this.Ticket.HeaderText = "";
            this.Ticket.Name = "Ticket";
            this.Ticket.ReadOnly = true;
            this.Ticket.Width = 35;
            // 
            // Status
            // 
            this.Status.HeaderText = "";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.Width = 35;
            // 
            // Devolver
            // 
            this.Devolver.HeaderText = "";
            this.Devolver.Name = "Devolver";
            this.Devolver.ReadOnly = true;
            this.Devolver.Width = 35;
            // 
            // Info
            // 
            this.Info.HeaderText = "";
            this.Info.Name = "Info";
            this.Info.ReadOnly = true;
            this.Info.Width = 35;
            // 
            // IDVenta
            // 
            this.IDVenta.HeaderText = "IDVenta";
            this.IDVenta.Name = "IDVenta";
            this.IDVenta.ReadOnly = true;
            this.IDVenta.Visible = false;
            // 
            // Anticipos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 561);
            this.Controls.Add(this.DGVAnticipos);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.tituloSeccion);
            this.Name = "Anticipos";
            this.Text = "Anticipos";
            this.Load += new System.EventHandler(this.Anticipos_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Anticipos_Paint);
            this.Resize += new System.EventHandler(this.Anticipos_Resize);
            this.panelBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVAnticipos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.DateTimePicker dpFechaFinal;
        private System.Windows.Forms.DateTimePicker dpFechaInicial;
        private System.Windows.Forms.Button btnNuevoAnticipo;
        private System.Windows.Forms.Button btnBuscarAnticipos;
        private System.Windows.Forms.ComboBox cbAnticipos;
        private System.Windows.Forms.DataGridView DGVAnticipos;
        private System.Windows.Forms.ToolTip TTMensaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Concepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Importe;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn Empleado;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewImageColumn Ticket;
        private System.Windows.Forms.DataGridViewImageColumn Status;
        private System.Windows.Forms.DataGridViewImageColumn Devolver;
        private System.Windows.Forms.DataGridViewImageColumn Info;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDVenta;
    }
}