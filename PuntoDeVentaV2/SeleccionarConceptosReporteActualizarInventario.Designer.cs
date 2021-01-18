namespace PuntoDeVentaV2
{
    partial class SeleccionarConceptosReporteActualizarInventario
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
            this.CLBConceptosExistentes = new System.Windows.Forms.CheckedListBox();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkSelectOnly = new System.Windows.Forms.CheckBox();
            this.chkSelectAllOrNot = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // CLBConceptosExistentes
            // 
            this.CLBConceptosExistentes.CheckOnClick = true;
            this.CLBConceptosExistentes.FormattingEnabled = true;
            this.CLBConceptosExistentes.Location = new System.Drawing.Point(12, 157);
            this.CLBConceptosExistentes.Name = "CLBConceptosExistentes";
            this.CLBConceptosExistentes.Size = new System.Drawing.Size(200, 154);
            this.CLBConceptosExistentes.TabIndex = 0;
            // 
            // btnAceptar
            // 
            this.btnAceptar.BackColor = System.Drawing.SystemColors.Highlight;
            this.btnAceptar.FlatAppearance.BorderSize = 0;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Century", 9.75F);
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(137, 322);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 1;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Century", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 65);
            this.label1.TabIndex = 3;
            this.label1.Text = "Selecciona los  conceptos que deseas que salgan en el repote";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSelectAllOrNot);
            this.groupBox1.Controls.Add(this.chkSelectOnly);
            this.groupBox1.Location = new System.Drawing.Point(12, 81);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 66);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // chkSelectOnly
            // 
            this.chkSelectOnly.AutoSize = true;
            this.chkSelectOnly.Checked = true;
            this.chkSelectOnly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSelectOnly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.chkSelectOnly.Location = new System.Drawing.Point(6, 19);
            this.chkSelectOnly.Name = "chkSelectOnly";
            this.chkSelectOnly.Size = new System.Drawing.Size(170, 17);
            this.chkSelectOnly.TabIndex = 0;
            this.chkSelectOnly.Text = "Aplicar solo selccionados";
            this.chkSelectOnly.UseVisualStyleBackColor = true;
            // 
            // chkSelectAllOrNot
            // 
            this.chkSelectAllOrNot.AutoSize = true;
            this.chkSelectAllOrNot.Location = new System.Drawing.Point(6, 42);
            this.chkSelectAllOrNot.Name = "chkSelectAllOrNot";
            this.chkSelectAllOrNot.Size = new System.Drawing.Size(173, 17);
            this.chkSelectAllOrNot.TabIndex = 1;
            this.chkSelectAllOrNot.Text = "Seleccionar todas las opciones";
            this.chkSelectAllOrNot.UseVisualStyleBackColor = true;
            this.chkSelectAllOrNot.CheckedChanged += new System.EventHandler(this.chkSelectAllOrNot_CheckedChanged);
            // 
            // SeleccionarConceptosReporteActualizarInventario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(229, 358);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.CLBConceptosExistentes);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SeleccionarConceptosReporteActualizarInventario";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Coenceptos";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox CLBConceptosExistentes;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkSelectAllOrNot;
        private System.Windows.Forms.CheckBox chkSelectOnly;
    }
}