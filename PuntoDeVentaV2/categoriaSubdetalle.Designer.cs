
namespace PuntoDeVentaV2
{
    partial class categoriaSubdetalle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(categoriaSubdetalle));
            this.panel3 = new System.Windows.Forms.Panel();
            this.pboxBorrar = new System.Windows.Forms.PictureBox();
            this.txtSubDetalle = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.cbTipoDeDatos = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chbCaducidad = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.gbCad = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxBorrar)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.gbCad.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.pboxBorrar);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtSubDetalle);
            this.panel3.Location = new System.Drawing.Point(12, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(404, 77);
            this.panel3.TabIndex = 5;
            // 
            // pboxBorrar
            // 
            this.pboxBorrar.Image = global::PuntoDeVentaV2.Properties.Resources.trash;
            this.pboxBorrar.Location = new System.Drawing.Point(365, 32);
            this.pboxBorrar.Margin = new System.Windows.Forms.Padding(2);
            this.pboxBorrar.Name = "pboxBorrar";
            this.pboxBorrar.Size = new System.Drawing.Size(27, 26);
            this.pboxBorrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pboxBorrar.TabIndex = 8;
            this.pboxBorrar.TabStop = false;
            this.pboxBorrar.Click += new System.EventHandler(this.pboxBorrar_Click);
            // 
            // txtSubDetalle
            // 
            this.txtSubDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubDetalle.Location = new System.Drawing.Point(7, 32);
            this.txtSubDetalle.Name = "txtSubDetalle";
            this.txtSubDetalle.Size = new System.Drawing.Size(385, 26);
            this.txtSubDetalle.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCancelar);
            this.panel2.Controls.Add(this.btnAceptar);
            this.panel2.Location = new System.Drawing.Point(11, 161);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(405, 55);
            this.panel2.TabIndex = 4;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.DarkRed;
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Image = global::PuntoDeVentaV2.Properties.Resources.window_close_o1;
            this.btnCancelar.Location = new System.Drawing.Point(10, 7);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(157, 41);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Image = global::PuntoDeVentaV2.Properties.Resources.check_square_o1;
            this.btnAceptar.Location = new System.Drawing.Point(238, 7);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(157, 41);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // cbTipoDeDatos
            // 
            this.cbTipoDeDatos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoDeDatos.FormattingEnabled = true;
            this.cbTipoDeDatos.Items.AddRange(new object[] {
            "Fecha",
            "Numerico",
            "Texto"});
            this.cbTipoDeDatos.Location = new System.Drawing.Point(12, 120);
            this.cbTipoDeDatos.Name = "cbTipoDeDatos";
            this.cbTipoDeDatos.Size = new System.Drawing.Size(183, 21);
            this.cbTipoDeDatos.TabIndex = 6;
            this.cbTipoDeDatos.SelectedIndexChanged += new System.EventHandler(this.cbTipoDeDatos_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Formato:";
            // 
            // chbCaducidad
            // 
            this.chbCaducidad.AutoSize = true;
            this.chbCaducidad.Location = new System.Drawing.Point(207, 96);
            this.chbCaducidad.Name = "chbCaducidad";
            this.chbCaducidad.Size = new System.Drawing.Size(91, 17);
            this.chbCaducidad.TabIndex = 8;
            this.chbCaducidad.Text = "Es caducidad";
            this.chbCaducidad.UseVisualStyleBackColor = true;
            this.chbCaducidad.Visible = false;
            this.chbCaducidad.CheckedChanged += new System.EventHandler(this.chbCaducidad_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Aviso a                        días para caducar";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(48, 19);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(60, 20);
            this.numericUpDown1.TabIndex = 10;
            // 
            // gbCad
            // 
            this.gbCad.Controls.Add(this.numericUpDown1);
            this.gbCad.Controls.Add(this.label2);
            this.gbCad.Enabled = false;
            this.gbCad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbCad.Location = new System.Drawing.Point(201, 100);
            this.gbCad.Name = "gbCad";
            this.gbCad.Size = new System.Drawing.Size(215, 53);
            this.gbCad.TabIndex = 11;
            this.gbCad.TabStop = false;
            this.gbCad.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Nombre de la categoria:";
            // 
            // categoriaSubdetalle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 236);
            this.Controls.Add(this.chbCaducidad);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbTipoDeDatos);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.gbCad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "categoriaSubdetalle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Categoria";
            this.Load += new System.EventHandler(this.categoriaSubdetalle_Load);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pboxBorrar)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.gbCad.ResumeLayout(false);
            this.gbCad.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox txtSubDetalle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.ComboBox cbTipoDeDatos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pboxBorrar;
        private System.Windows.Forms.CheckBox chbCaducidad;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.GroupBox gbCad;
        private System.Windows.Forms.Label label3;
    }
}