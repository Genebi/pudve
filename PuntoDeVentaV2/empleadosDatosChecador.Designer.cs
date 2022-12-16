
namespace PuntoDeVentaV2
{
    partial class empleadosDatosChecador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(empleadosDatosChecador));
            this.ComboHabilittados = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dDGVDeshabilitados = new System.Windows.Forms.DataGridView();
            this.IDDeshabilitado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreDesha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ImagenDesha = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.btnHuella = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_agregar_empleado = new System.Windows.Forms.Button();
            this.btnPlantilla = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dDGVDeshabilitados)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // ComboHabilittados
            // 
            this.ComboHabilittados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboHabilittados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ComboHabilittados.FormattingEnabled = true;
            this.ComboHabilittados.Items.AddRange(new object[] {
            "Habilitados",
            "Deshabilitados"});
            this.ComboHabilittados.Location = new System.Drawing.Point(3, 3);
            this.ComboHabilittados.Name = "ComboHabilittados";
            this.ComboHabilittados.Size = new System.Drawing.Size(318, 30);
            this.ComboHabilittados.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.dDGVDeshabilitados);
            this.panel1.Controls.Add(this.ComboHabilittados);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(326, 484);
            this.panel1.TabIndex = 125;
            // 
            // dDGVDeshabilitados
            // 
            this.dDGVDeshabilitados.AllowUserToAddRows = false;
            this.dDGVDeshabilitados.AllowUserToDeleteRows = false;
            this.dDGVDeshabilitados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dDGVDeshabilitados.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dDGVDeshabilitados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dDGVDeshabilitados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IDDeshabilitado,
            this.NombreDesha,
            this.ImagenDesha});
            this.dDGVDeshabilitados.Location = new System.Drawing.Point(3, 44);
            this.dDGVDeshabilitados.Margin = new System.Windows.Forms.Padding(1, 8, 3, 0);
            this.dDGVDeshabilitados.Name = "dDGVDeshabilitados";
            this.dDGVDeshabilitados.ReadOnly = true;
            this.dDGVDeshabilitados.RowHeadersVisible = false;
            this.dDGVDeshabilitados.RowHeadersWidth = 62;
            this.dDGVDeshabilitados.Size = new System.Drawing.Size(318, 437);
            this.dDGVDeshabilitados.TabIndex = 125;
            this.dDGVDeshabilitados.Visible = false;
            // 
            // IDDeshabilitado
            // 
            this.IDDeshabilitado.FillWeight = 54.72959F;
            this.IDDeshabilitado.HeaderText = "No.";
            this.IDDeshabilitado.MinimumWidth = 8;
            this.IDDeshabilitado.Name = "IDDeshabilitado";
            this.IDDeshabilitado.ReadOnly = true;
            // 
            // NombreDesha
            // 
            this.NombreDesha.FillWeight = 258.2662F;
            this.NombreDesha.HeaderText = "Plantillas";
            this.NombreDesha.MinimumWidth = 8;
            this.NombreDesha.Name = "NombreDesha";
            this.NombreDesha.ReadOnly = true;
            // 
            // ImagenDesha
            // 
            this.ImagenDesha.FillWeight = 46.39511F;
            this.ImagenDesha.HeaderText = "";
            this.ImagenDesha.MinimumWidth = 8;
            this.ImagenDesha.Name = "ImagenDesha";
            this.ImagenDesha.ReadOnly = true;
            this.ImagenDesha.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ImagenDesha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnHuella);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btn_agregar_empleado);
            this.panel2.Controls.Add(this.btnPlantilla);
            this.panel2.Controls.Add(this.btn_cancelar);
            this.panel2.Controls.Add(this.btn_aceptar);
            this.panel2.Location = new System.Drawing.Point(344, 15);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(243, 481);
            this.panel2.TabIndex = 126;
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.BackColor = System.Drawing.Color.Black;
            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(46, 436);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(220, 45);
            this.button3.TabIndex = 130;
            this.button3.Text = "TEST";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnHuella
            // 
            this.btnHuella.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuella.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnHuella.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHuella.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHuella.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHuella.ForeColor = System.Drawing.Color.White;
            this.btnHuella.Location = new System.Drawing.Point(10, 216);
            this.btnHuella.Name = "btnHuella";
            this.btnHuella.Size = new System.Drawing.Size(220, 80);
            this.btnHuella.TabIndex = 3;
            this.btnHuella.Text = "Registrar huella";
            this.btnHuella.UseVisualStyleBackColor = false;
            this.btnHuella.Click += new System.EventHandler(this.btnHuella_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(10, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(220, 80);
            this.button1.TabIndex = 1;
            this.button1.Text = "Reglas de salario";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btn_agregar_empleado
            // 
            this.btn_agregar_empleado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_agregar_empleado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btn_agregar_empleado.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_agregar_empleado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_agregar_empleado.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_agregar_empleado.ForeColor = System.Drawing.Color.White;
            this.btn_agregar_empleado.Location = new System.Drawing.Point(10, 116);
            this.btn_agregar_empleado.Name = "btn_agregar_empleado";
            this.btn_agregar_empleado.Size = new System.Drawing.Size(220, 80);
            this.btn_agregar_empleado.TabIndex = 2;
            this.btn_agregar_empleado.Text = "Reglas de horario";
            this.btn_agregar_empleado.UseVisualStyleBackColor = false;
            // 
            // btnPlantilla
            // 
            this.btnPlantilla.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPlantilla.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnPlantilla.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlantilla.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlantilla.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlantilla.ForeColor = System.Drawing.Color.White;
            this.btnPlantilla.Location = new System.Drawing.Point(10, 376);
            this.btnPlantilla.Name = "btnPlantilla";
            this.btnPlantilla.Size = new System.Drawing.Size(220, 39);
            this.btnPlantilla.TabIndex = 5;
            this.btnPlantilla.Text = "Guardar Plantilla";
            this.btnPlantilla.UseVisualStyleBackColor = false;
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancelar.BackColor = System.Drawing.Color.Red;
            this.btn_cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancelar.ForeColor = System.Drawing.Color.White;
            this.btn_cancelar.Location = new System.Drawing.Point(10, 439);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(220, 39);
            this.btn_cancelar.TabIndex = 6;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_aceptar.BackColor = System.Drawing.Color.Green;
            this.btn_aceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_aceptar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_aceptar.ForeColor = System.Drawing.Color.White;
            this.btn_aceptar.Location = new System.Drawing.Point(10, 318);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(220, 39);
            this.btn_aceptar.TabIndex = 4;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = false;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // empleadosDatosChecador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(591, 508);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "empleadosDatosChecador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reglas de entrada y salida";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dDGVDeshabilitados)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboHabilittados;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dDGVDeshabilitados;
        private System.Windows.Forms.DataGridViewTextBoxColumn IDDeshabilitado;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreDesha;
        private System.Windows.Forms.DataGridViewImageColumn ImagenDesha;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPlantilla;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Button btn_aceptar;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button btnHuella;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_agregar_empleado;
    }
}