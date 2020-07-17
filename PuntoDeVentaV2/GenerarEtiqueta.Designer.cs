namespace PuntoDeVentaV2
{
    partial class GenerarEtiqueta
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GenerarEtiqueta));
            this.panelPropiedades = new System.Windows.Forms.Panel();
            this.panelEtiqueta = new System.Windows.Forms.Panel();
            this.btnReducir = new System.Windows.Forms.Button();
            this.btnAumentar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.cbFuentes = new System.Windows.Forms.ComboBox();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.txtNombreArchivo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelPropiedades
            // 
            this.panelPropiedades.Location = new System.Drawing.Point(2, 2);
            this.panelPropiedades.Name = "panelPropiedades";
            this.panelPropiedades.Size = new System.Drawing.Size(230, 357);
            this.panelPropiedades.TabIndex = 0;
            // 
            // panelEtiqueta
            // 
            this.panelEtiqueta.BackColor = System.Drawing.SystemColors.Window;
            this.panelEtiqueta.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelEtiqueta.Location = new System.Drawing.Point(315, 20);
            this.panelEtiqueta.Name = "panelEtiqueta";
            this.panelEtiqueta.Size = new System.Drawing.Size(280, 200);
            this.panelEtiqueta.TabIndex = 1;
            this.panelEtiqueta.Click += new System.EventHandler(this.panelEtiqueta_Click);
            // 
            // btnReducir
            // 
            this.btnReducir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReducir.Image = global::PuntoDeVentaV2.Properties.Resources.search_minus;
            this.btnReducir.Location = new System.Drawing.Point(601, 51);
            this.btnReducir.Name = "btnReducir";
            this.btnReducir.Size = new System.Drawing.Size(25, 25);
            this.btnReducir.TabIndex = 1;
            this.btnReducir.UseVisualStyleBackColor = true;
            this.btnReducir.Click += new System.EventHandler(this.btnReducir_Click);
            // 
            // btnAumentar
            // 
            this.btnAumentar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAumentar.Image = ((System.Drawing.Image)(resources.GetObject("btnAumentar.Image")));
            this.btnAumentar.Location = new System.Drawing.Point(601, 20);
            this.btnAumentar.Name = "btnAumentar";
            this.btnAumentar.Size = new System.Drawing.Size(25, 25);
            this.btnAumentar.TabIndex = 0;
            this.btnAumentar.UseVisualStyleBackColor = true;
            this.btnAumentar.Click += new System.EventHandler(this.btnAumentar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnGuardar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(491, 305);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(150, 23);
            this.btnGuardar.TabIndex = 4;
            this.btnGuardar.Text = "Guardar Plantilla";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // cbFuentes
            // 
            this.cbFuentes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFuentes.FormattingEnabled = true;
            this.cbFuentes.Location = new System.Drawing.Point(315, 226);
            this.cbFuentes.Name = "cbFuentes";
            this.cbFuentes.Size = new System.Drawing.Size(280, 25);
            this.cbFuentes.TabIndex = 5;
            this.cbFuentes.SelectedIndexChanged += new System.EventHandler(this.cbFuentes_SelectedIndexChanged);
            // 
            // btnEliminar
            // 
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.Image = global::PuntoDeVentaV2.Properties.Resources.remove;
            this.btnEliminar.Location = new System.Drawing.Point(601, 82);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(25, 25);
            this.btnEliminar.TabIndex = 6;
            this.btnEliminar.UseVisualStyleBackColor = true;
            this.btnEliminar.Click += new System.EventHandler(this.btnEliminar_Click);
            // 
            // txtNombreArchivo
            // 
            this.txtNombreArchivo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreArchivo.Location = new System.Drawing.Point(315, 305);
            this.txtNombreArchivo.Name = "txtNombreArchivo";
            this.txtNombreArchivo.Size = new System.Drawing.Size(170, 23);
            this.txtNombreArchivo.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(248, 308);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 8;
            this.label1.Text = "Nombre";
            // 
            // label9
            // 
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label9.Location = new System.Drawing.Point(238, 274);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(440, 2);
            this.label9.TabIndex = 19;
            // 
            // GenerarEtiqueta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNombreArchivo);
            this.Controls.Add(this.btnEliminar);
            this.Controls.Add(this.cbFuentes);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnReducir);
            this.Controls.Add(this.btnAumentar);
            this.Controls.Add(this.panelEtiqueta);
            this.Controls.Add(this.panelPropiedades);
            this.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GenerarEtiqueta";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Personalizar etiqueta";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GenerarEtiqueta_FormClosed);
            this.Load += new System.EventHandler(this.GenerarEtiqueta_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelPropiedades;
        private System.Windows.Forms.Panel panelEtiqueta;
        private System.Windows.Forms.Button btnReducir;
        private System.Windows.Forms.Button btnAumentar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.ComboBox cbFuentes;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.TextBox txtNombreArchivo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
    }
}