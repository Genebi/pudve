namespace PuntoDeVentaV2
{
    partial class Caja
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
            this.tituloSeccion = new System.Windows.Forms.Label();
            this.btnAgregarDinero = new System.Windows.Forms.Button();
            this.btnRetirarDinero = new System.Windows.Forms.Button();
            this.panelBotones = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.gbContenedor = new System.Windows.Forms.GroupBox();
            this.panelRetirar = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtConcepto = new System.Windows.Forms.TextBox();
            this.btnCancelarRetirar = new System.Windows.Forms.Button();
            this.btnAceptarRetirar = new System.Windows.Forms.Button();
            this.txtRetirarDinero = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panelAgregar = new System.Windows.Forms.Panel();
            this.btnCancelarDeposito = new System.Windows.Forms.Button();
            this.btnAceptarDeposito = new System.Windows.Forms.Button();
            this.txtAgregarDinero = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panelContenido = new System.Windows.Forms.Panel();
            this.panelBotones.SuspendLayout();
            this.gbContenedor.SuspendLayout();
            this.panelRetirar.SuspendLayout();
            this.panelAgregar.SuspendLayout();
            this.panelContenido.SuspendLayout();
            this.SuspendLayout();
            // 
            // tituloSeccion
            // 
            this.tituloSeccion.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tituloSeccion.AutoSize = true;
            this.tituloSeccion.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tituloSeccion.Location = new System.Drawing.Point(388, 27);
            this.tituloSeccion.Name = "tituloSeccion";
            this.tituloSeccion.Size = new System.Drawing.Size(70, 25);
            this.tituloSeccion.TabIndex = 6;
            this.tituloSeccion.Text = "CAJA";
            this.tituloSeccion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnAgregarDinero
            // 
            this.btnAgregarDinero.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAgregarDinero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnAgregarDinero.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregarDinero.FlatAppearance.BorderSize = 0;
            this.btnAgregarDinero.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnAgregarDinero.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnAgregarDinero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarDinero.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregarDinero.ForeColor = System.Drawing.Color.White;
            this.btnAgregarDinero.Location = new System.Drawing.Point(12, 19);
            this.btnAgregarDinero.Name = "btnAgregarDinero";
            this.btnAgregarDinero.Size = new System.Drawing.Size(190, 30);
            this.btnAgregarDinero.TabIndex = 7;
            this.btnAgregarDinero.Text = "Agregar Dinero";
            this.btnAgregarDinero.UseVisualStyleBackColor = false;
            this.btnAgregarDinero.Click += new System.EventHandler(this.btnAgregarDinero_Click);
            // 
            // btnRetirarDinero
            // 
            this.btnRetirarDinero.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRetirarDinero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnRetirarDinero.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRetirarDinero.FlatAppearance.BorderSize = 0;
            this.btnRetirarDinero.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnRetirarDinero.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnRetirarDinero.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetirarDinero.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetirarDinero.ForeColor = System.Drawing.Color.White;
            this.btnRetirarDinero.Location = new System.Drawing.Point(12, 65);
            this.btnRetirarDinero.Name = "btnRetirarDinero";
            this.btnRetirarDinero.Size = new System.Drawing.Size(190, 30);
            this.btnRetirarDinero.TabIndex = 8;
            this.btnRetirarDinero.Text = "Retirar Dinero";
            this.btnRetirarDinero.UseVisualStyleBackColor = false;
            this.btnRetirarDinero.Click += new System.EventHandler(this.btnRetirarDinero_Click);
            // 
            // panelBotones
            // 
            this.panelBotones.Controls.Add(this.btnAgregarDinero);
            this.panelBotones.Controls.Add(this.btnRetirarDinero);
            this.panelBotones.Location = new System.Drawing.Point(125, 113);
            this.panelBotones.Name = "panelBotones";
            this.panelBotones.Size = new System.Drawing.Size(215, 274);
            this.panelBotones.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.Location = new System.Drawing.Point(4, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(860, 2);
            this.label8.TabIndex = 20;
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // gbContenedor
            // 
            this.gbContenedor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbContenedor.Controls.Add(this.panelRetirar);
            this.gbContenedor.Controls.Add(this.panelAgregar);
            this.gbContenedor.Location = new System.Drawing.Point(39, 3);
            this.gbContenedor.Name = "gbContenedor";
            this.gbContenedor.Size = new System.Drawing.Size(445, 338);
            this.gbContenedor.TabIndex = 23;
            this.gbContenedor.TabStop = false;
            // 
            // panelRetirar
            // 
            this.panelRetirar.Controls.Add(this.label3);
            this.panelRetirar.Controls.Add(this.txtConcepto);
            this.panelRetirar.Controls.Add(this.btnCancelarRetirar);
            this.panelRetirar.Controls.Add(this.btnAceptarRetirar);
            this.panelRetirar.Controls.Add(this.txtRetirarDinero);
            this.panelRetirar.Controls.Add(this.label2);
            this.panelRetirar.Location = new System.Drawing.Point(7, 16);
            this.panelRetirar.Name = "panelRetirar";
            this.panelRetirar.Size = new System.Drawing.Size(432, 316);
            this.panelRetirar.TabIndex = 1;
            this.panelRetirar.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(138, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 17);
            this.label3.TabIndex = 20;
            this.label3.Text = "Concepto del retiro";
            // 
            // txtConcepto
            // 
            this.txtConcepto.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConcepto.Location = new System.Drawing.Point(59, 116);
            this.txtConcepto.Multiline = true;
            this.txtConcepto.Name = "txtConcepto";
            this.txtConcepto.Size = new System.Drawing.Size(327, 45);
            this.txtConcepto.TabIndex = 2;
            // 
            // btnCancelarRetirar
            // 
            this.btnCancelarRetirar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelarRetirar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarRetirar.FlatAppearance.BorderSize = 0;
            this.btnCancelarRetirar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarRetirar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarRetirar.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancelarRetirar.Location = new System.Drawing.Point(59, 181);
            this.btnCancelarRetirar.Name = "btnCancelarRetirar";
            this.btnCancelarRetirar.Size = new System.Drawing.Size(160, 24);
            this.btnCancelarRetirar.TabIndex = 4;
            this.btnCancelarRetirar.Text = "Cancelar";
            this.btnCancelarRetirar.UseVisualStyleBackColor = false;
            this.btnCancelarRetirar.Click += new System.EventHandler(this.btnCancelarRetirar_Click);
            // 
            // btnAceptarRetirar
            // 
            this.btnAceptarRetirar.BackColor = System.Drawing.Color.Green;
            this.btnAceptarRetirar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptarRetirar.FlatAppearance.BorderSize = 0;
            this.btnAceptarRetirar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptarRetirar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptarRetirar.ForeColor = System.Drawing.Color.White;
            this.btnAceptarRetirar.Location = new System.Drawing.Point(226, 181);
            this.btnAceptarRetirar.Name = "btnAceptarRetirar";
            this.btnAceptarRetirar.Size = new System.Drawing.Size(160, 24);
            this.btnAceptarRetirar.TabIndex = 3;
            this.btnAceptarRetirar.Text = "Aceptar";
            this.btnAceptarRetirar.UseVisualStyleBackColor = false;
            this.btnAceptarRetirar.Click += new System.EventHandler(this.btnAceptarRetirar_Click);
            // 
            // txtRetirarDinero
            // 
            this.txtRetirarDinero.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRetirarDinero.Location = new System.Drawing.Point(59, 51);
            this.txtRetirarDinero.Name = "txtRetirarDinero";
            this.txtRetirarDinero.Size = new System.Drawing.Size(327, 23);
            this.txtRetirarDinero.TabIndex = 1;
            this.txtRetirarDinero.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(138, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Cantidad a retirar";
            // 
            // panelAgregar
            // 
            this.panelAgregar.Controls.Add(this.btnCancelarDeposito);
            this.panelAgregar.Controls.Add(this.btnAceptarDeposito);
            this.panelAgregar.Controls.Add(this.txtAgregarDinero);
            this.panelAgregar.Controls.Add(this.label1);
            this.panelAgregar.Location = new System.Drawing.Point(7, 16);
            this.panelAgregar.Name = "panelAgregar";
            this.panelAgregar.Size = new System.Drawing.Size(432, 316);
            this.panelAgregar.TabIndex = 0;
            this.panelAgregar.Visible = false;
            // 
            // btnCancelarDeposito
            // 
            this.btnCancelarDeposito.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelarDeposito.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelarDeposito.FlatAppearance.BorderSize = 0;
            this.btnCancelarDeposito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelarDeposito.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarDeposito.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancelarDeposito.Location = new System.Drawing.Point(59, 111);
            this.btnCancelarDeposito.Name = "btnCancelarDeposito";
            this.btnCancelarDeposito.Size = new System.Drawing.Size(160, 24);
            this.btnCancelarDeposito.TabIndex = 3;
            this.btnCancelarDeposito.Text = "Cancelar";
            this.btnCancelarDeposito.UseVisualStyleBackColor = false;
            this.btnCancelarDeposito.Click += new System.EventHandler(this.btnCancelarDeposito_Click);
            // 
            // btnAceptarDeposito
            // 
            this.btnAceptarDeposito.BackColor = System.Drawing.Color.Green;
            this.btnAceptarDeposito.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptarDeposito.FlatAppearance.BorderSize = 0;
            this.btnAceptarDeposito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptarDeposito.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptarDeposito.ForeColor = System.Drawing.Color.White;
            this.btnAceptarDeposito.Location = new System.Drawing.Point(226, 111);
            this.btnAceptarDeposito.Name = "btnAceptarDeposito";
            this.btnAceptarDeposito.Size = new System.Drawing.Size(160, 24);
            this.btnAceptarDeposito.TabIndex = 2;
            this.btnAceptarDeposito.Text = "Aceptar";
            this.btnAceptarDeposito.UseVisualStyleBackColor = false;
            this.btnAceptarDeposito.Click += new System.EventHandler(this.btnAceptarDeposito_Click);
            // 
            // txtAgregarDinero
            // 
            this.txtAgregarDinero.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAgregarDinero.Location = new System.Drawing.Point(59, 65);
            this.txtAgregarDinero.Name = "txtAgregarDinero";
            this.txtAgregarDinero.Size = new System.Drawing.Size(327, 23);
            this.txtAgregarDinero.TabIndex = 1;
            this.txtAgregarDinero.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(138, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cantidad a depositar";
            // 
            // panelContenido
            // 
            this.panelContenido.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelContenido.Controls.Add(this.gbContenedor);
            this.panelContenido.Location = new System.Drawing.Point(373, 113);
            this.panelContenido.Name = "panelContenido";
            this.panelContenido.Size = new System.Drawing.Size(491, 344);
            this.panelContenido.TabIndex = 24;
            // 
            // Caja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 561);
            this.Controls.Add(this.panelContenido);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tituloSeccion);
            this.Name = "Caja";
            this.Text = "Caja";
            this.Load += new System.EventHandler(this.Caja_Load);
            this.VisibleChanged += new System.EventHandler(this.Caja_VisibleChanged);
            this.panelBotones.ResumeLayout(false);
            this.gbContenedor.ResumeLayout(false);
            this.panelRetirar.ResumeLayout(false);
            this.panelRetirar.PerformLayout();
            this.panelAgregar.ResumeLayout(false);
            this.panelAgregar.PerformLayout();
            this.panelContenido.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label tituloSeccion;
        private System.Windows.Forms.Button btnAgregarDinero;
        private System.Windows.Forms.Button btnRetirarDinero;
        private System.Windows.Forms.Panel panelBotones;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gbContenedor;
        private System.Windows.Forms.Panel panelContenido;
        private System.Windows.Forms.Panel panelAgregar;
        private System.Windows.Forms.TextBox txtAgregarDinero;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panelRetirar;
        private System.Windows.Forms.Button btnAceptarDeposito;
        private System.Windows.Forms.Button btnCancelarDeposito;
        private System.Windows.Forms.Button btnCancelarRetirar;
        private System.Windows.Forms.Button btnAceptarRetirar;
        private System.Windows.Forms.TextBox txtRetirarDinero;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtConcepto;
    }
}