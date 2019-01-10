namespace PuntoDeVentaV2
{
    partial class AgregarDescuentoProducto
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
            this.radioCliente = new System.Windows.Forms.RadioButton();
            this.radioMayoreo = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCancelarDesc = new System.Windows.Forms.Button();
            this.btnAceptarDesc = new System.Windows.Forms.Button();
            this.segundaPagina = new System.Windows.Forms.TabPage();
            this.lbRango1 = new System.Windows.Forms.Label();
            this.cbRango1 = new System.Windows.Forms.CheckBox();
            this.txtRango1_3 = new System.Windows.Forms.TextBox();
            this.txtRango1_2 = new System.Windows.Forms.TextBox();
            this.txtRango1_1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panelRangos = new System.Windows.Forms.FlowLayoutPanel();
            this.primeraPagina = new System.Windows.Forms.TabPage();
            this.txtDescuentoTotal = new System.Windows.Forms.TextBox();
            this.txtPrecioDescuento = new System.Windows.Forms.TextBox();
            this.txtPorcentajeDescuento = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.segundaPagina.SuspendLayout();
            this.primeraPagina.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioCliente
            // 
            this.radioCliente.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioCliente.AutoSize = true;
            this.radioCliente.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioCliente.Location = new System.Drawing.Point(149, 13);
            this.radioCliente.Name = "radioCliente";
            this.radioCliente.Size = new System.Drawing.Size(171, 21);
            this.radioCliente.TabIndex = 0;
            this.radioCliente.Text = "Descuento por Cliente";
            this.radioCliente.UseVisualStyleBackColor = true;
            this.radioCliente.CheckedChanged += new System.EventHandler(this.radioCliente_CheckedChanged);
            // 
            // radioMayoreo
            // 
            this.radioMayoreo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.radioMayoreo.AutoSize = true;
            this.radioMayoreo.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioMayoreo.Location = new System.Drawing.Point(366, 13);
            this.radioMayoreo.Name = "radioMayoreo";
            this.radioMayoreo.Size = new System.Drawing.Size(181, 21);
            this.radioMayoreo.TabIndex = 1;
            this.radioMayoreo.Text = "Descuento por Mayoreo";
            this.radioMayoreo.UseVisualStyleBackColor = true;
            this.radioMayoreo.CheckedChanged += new System.EventHandler(this.radioMayoreo_CheckedChanged);
            // 
            // label8
            // 
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(9, 39);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(665, 2);
            this.label8.TabIndex = 21;
            // 
            // btnCancelarDesc
            // 
            this.btnCancelarDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelarDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelarDesc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarDesc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnCancelarDesc.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnCancelarDesc.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarDesc.ForeColor = System.Drawing.Color.White;
            this.btnCancelarDesc.Location = new System.Drawing.Point(320, 308);
            this.btnCancelarDesc.Name = "btnCancelarDesc";
            this.btnCancelarDesc.Size = new System.Drawing.Size(165, 27);
            this.btnCancelarDesc.TabIndex = 22;
            this.btnCancelarDesc.Text = "Cancelar";
            this.btnCancelarDesc.UseVisualStyleBackColor = false;
            this.btnCancelarDesc.Click += new System.EventHandler(this.btnCancelarDesc_Click);
            // 
            // btnAceptarDesc
            // 
            this.btnAceptarDesc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptarDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnAceptarDesc.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptarDesc.FlatAppearance.BorderSize = 0;
            this.btnAceptarDesc.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnAceptarDesc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAceptarDesc.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptarDesc.ForeColor = System.Drawing.Color.White;
            this.btnAceptarDesc.Location = new System.Drawing.Point(491, 308);
            this.btnAceptarDesc.Name = "btnAceptarDesc";
            this.btnAceptarDesc.Size = new System.Drawing.Size(176, 27);
            this.btnAceptarDesc.TabIndex = 23;
            this.btnAceptarDesc.Text = "Aceptar";
            this.btnAceptarDesc.UseVisualStyleBackColor = false;
            this.btnAceptarDesc.Visible = false;
            this.btnAceptarDesc.Click += new System.EventHandler(this.btnAceptarDesc_Click);
            // 
            // segundaPagina
            // 
            this.segundaPagina.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.segundaPagina.Controls.Add(this.cbRango1);
            this.segundaPagina.Controls.Add(this.lbRango1);
            this.segundaPagina.Controls.Add(this.txtRango1_3);
            this.segundaPagina.Controls.Add(this.txtRango1_2);
            this.segundaPagina.Controls.Add(this.txtRango1_1);
            this.segundaPagina.Controls.Add(this.label5);
            this.segundaPagina.Controls.Add(this.label1);
            this.segundaPagina.Controls.Add(this.panelRangos);
            this.segundaPagina.Location = new System.Drawing.Point(4, 5);
            this.segundaPagina.Name = "segundaPagina";
            this.segundaPagina.Padding = new System.Windows.Forms.Padding(3);
            this.segundaPagina.Size = new System.Drawing.Size(654, 246);
            this.segundaPagina.TabIndex = 1;
            this.segundaPagina.Text = "tabPage2";
            // 
            // lbRango1
            // 
            this.lbRango1.AutoSize = true;
            this.lbRango1.Location = new System.Drawing.Point(128, 66);
            this.lbRango1.Name = "lbRango1";
            this.lbRango1.Size = new System.Drawing.Size(0, 13);
            this.lbRango1.TabIndex = 4;
            // 
            // cbRango1
            // 
            this.cbRango1.AutoSize = true;
            this.cbRango1.Location = new System.Drawing.Point(106, 66);
            this.cbRango1.Name = "cbRango1";
            this.cbRango1.Size = new System.Drawing.Size(15, 14);
            this.cbRango1.TabIndex = 3;
            this.cbRango1.UseVisualStyleBackColor = true;
            // 
            // txtRango1_3
            // 
            this.txtRango1_3.Location = new System.Drawing.Point(408, 40);
            this.txtRango1_3.Margin = new System.Windows.Forms.Padding(70, 3, 3, 3);
            this.txtRango1_3.Name = "txtRango1_3";
            this.txtRango1_3.ReadOnly = true;
            this.txtRango1_3.Size = new System.Drawing.Size(100, 20);
            this.txtRango1_3.TabIndex = 2;
            this.txtRango1_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRango1_2
            // 
            this.txtRango1_2.Location = new System.Drawing.Point(239, 40);
            this.txtRango1_2.Name = "txtRango1_2";
            this.txtRango1_2.Size = new System.Drawing.Size(100, 20);
            this.txtRango1_2.TabIndex = 1;
            this.txtRango1_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRango1_2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRango1_2_KeyDown);
            // 
            // txtRango1_1
            // 
            this.txtRango1_1.Location = new System.Drawing.Point(106, 40);
            this.txtRango1_1.Margin = new System.Windows.Forms.Padding(30, 3, 30, 3);
            this.txtRango1_1.Name = "txtRango1_1";
            this.txtRango1_1.ReadOnly = true;
            this.txtRango1_1.Size = new System.Drawing.Size(100, 20);
            this.txtRango1_1.TabIndex = 0;
            this.txtRango1_1.Text = "1";
            this.txtRango1_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(432, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 16);
            this.label5.TabIndex = 2;
            this.label5.Text = "Precios";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(159, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Rango de Productos";
            // 
            // panelRangos
            // 
            this.panelRangos.AutoScroll = true;
            this.panelRangos.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelRangos.Location = new System.Drawing.Point(1, 82);
            this.panelRangos.Name = "panelRangos";
            this.panelRangos.Size = new System.Drawing.Size(651, 164);
            this.panelRangos.TabIndex = 0;
            this.panelRangos.WrapContents = false;
            // 
            // primeraPagina
            // 
            this.primeraPagina.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.primeraPagina.Controls.Add(this.txtDescuentoTotal);
            this.primeraPagina.Controls.Add(this.txtPrecioDescuento);
            this.primeraPagina.Controls.Add(this.txtPorcentajeDescuento);
            this.primeraPagina.Controls.Add(this.label4);
            this.primeraPagina.Controls.Add(this.label3);
            this.primeraPagina.Controls.Add(this.label2);
            this.primeraPagina.Location = new System.Drawing.Point(4, 5);
            this.primeraPagina.Name = "primeraPagina";
            this.primeraPagina.Padding = new System.Windows.Forms.Padding(3);
            this.primeraPagina.Size = new System.Drawing.Size(654, 166);
            this.primeraPagina.TabIndex = 0;
            this.primeraPagina.Text = "tabPage1";
            // 
            // txtDescuentoTotal
            // 
            this.txtDescuentoTotal.Location = new System.Drawing.Point(453, 50);
            this.txtDescuentoTotal.Name = "txtDescuentoTotal";
            this.txtDescuentoTotal.ReadOnly = true;
            this.txtDescuentoTotal.Size = new System.Drawing.Size(150, 20);
            this.txtDescuentoTotal.TabIndex = 7;
            // 
            // txtPrecioDescuento
            // 
            this.txtPrecioDescuento.Location = new System.Drawing.Point(260, 50);
            this.txtPrecioDescuento.Name = "txtPrecioDescuento";
            this.txtPrecioDescuento.ReadOnly = true;
            this.txtPrecioDescuento.Size = new System.Drawing.Size(150, 20);
            this.txtPrecioDescuento.TabIndex = 5;
            // 
            // txtPorcentajeDescuento
            // 
            this.txtPorcentajeDescuento.Location = new System.Drawing.Point(63, 50);
            this.txtPorcentajeDescuento.Name = "txtPorcentajeDescuento";
            this.txtPorcentajeDescuento.Size = new System.Drawing.Size(150, 20);
            this.txtPorcentajeDescuento.TabIndex = 3;
            this.txtPorcentajeDescuento.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPorcentajeDescuento_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(471, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Descuento Total";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(261, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Precio con descuento";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(90, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "% Descuento";
            // 
            // tabControl
            // 
            this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl.Controls.Add(this.primeraPagina);
            this.tabControl.Controls.Add(this.segundaPagina);
            this.tabControl.ItemSize = new System.Drawing.Size(0, 1);
            this.tabControl.Location = new System.Drawing.Point(12, 44);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(662, 255);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 3;
            this.tabControl.TabStop = false;
            this.tabControl.Visible = false;
            // 
            // AgregarDescuentoProducto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(680, 357);
            this.Controls.Add(this.btnAceptarDesc);
            this.Controls.Add(this.btnCancelarDesc);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.radioMayoreo);
            this.Controls.Add(this.radioCliente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "AgregarDescuentoProducto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Descuento Producto";
            this.Load += new System.EventHandler(this.AgregarDescuentoProducto_Load);
            this.segundaPagina.ResumeLayout(false);
            this.segundaPagina.PerformLayout();
            this.primeraPagina.ResumeLayout(false);
            this.primeraPagina.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioCliente;
        private System.Windows.Forms.RadioButton radioMayoreo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCancelarDesc;
        private System.Windows.Forms.Button btnAceptarDesc;
        private System.Windows.Forms.TabPage segundaPagina;
        private System.Windows.Forms.TabPage primeraPagina;
        private System.Windows.Forms.TextBox txtDescuentoTotal;
        private System.Windows.Forms.TextBox txtPrecioDescuento;
        private System.Windows.Forms.TextBox txtPorcentajeDescuento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.FlowLayoutPanel panelRangos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRango1_1;
        private System.Windows.Forms.TextBox txtRango1_2;
        private System.Windows.Forms.TextBox txtRango1_3;
        private System.Windows.Forms.Label lbRango1;
        private System.Windows.Forms.CheckBox cbRango1;
    }
}