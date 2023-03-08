
namespace PuntoDeVentaV2
{
    partial class creditoFiltradorPorVencer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(creditoFiltradorPorVencer));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numPeriodos = new System.Windows.Forms.NumericUpDown();
            this.cbPeriodos = new System.Windows.Forms.ComboBox();
            this.btnVencidas = new System.Windows.Forms.Button();
            this.lblFecha = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numPeriodos)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 57);
            this.label1.TabIndex = 0;
            this.label1.Text = "Seleccione una opción para filtrar créditos por vencer";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Dentro de:";
            // 
            // numPeriodos
            // 
            this.numPeriodos.Location = new System.Drawing.Point(88, 70);
            this.numPeriodos.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPeriodos.Name = "numPeriodos";
            this.numPeriodos.Size = new System.Drawing.Size(55, 20);
            this.numPeriodos.TabIndex = 2;
            this.numPeriodos.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numPeriodos.ValueChanged += new System.EventHandler(this.numPeriodos_ValueChanged);
            this.numPeriodos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numPeriodos_KeyDown);
            // 
            // cbPeriodos
            // 
            this.cbPeriodos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPeriodos.FormattingEnabled = true;
            this.cbPeriodos.Items.AddRange(new object[] {
            "Dias",
            "Semanas",
            "Meses"});
            this.cbPeriodos.Location = new System.Drawing.Point(149, 69);
            this.cbPeriodos.Name = "cbPeriodos";
            this.cbPeriodos.Size = new System.Drawing.Size(121, 21);
            this.cbPeriodos.TabIndex = 3;
            this.cbPeriodos.SelectedIndexChanged += new System.EventHandler(this.cbPeriodos_SelectedIndexChanged);
            // 
            // btnVencidas
            // 
            this.btnVencidas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(53)))), ((int)(((byte)(20)))));
            this.btnVencidas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnVencidas.FlatAppearance.BorderSize = 0;
            this.btnVencidas.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnVencidas.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(83)))), ((int)(((byte)(79)))));
            this.btnVencidas.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVencidas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVencidas.ForeColor = System.Drawing.Color.White;
            this.btnVencidas.Location = new System.Drawing.Point(12, 133);
            this.btnVencidas.Name = "btnVencidas";
            this.btnVencidas.Size = new System.Drawing.Size(253, 42);
            this.btnVencidas.TabIndex = 5;
            this.btnVencidas.Text = "Buscar";
            this.btnVencidas.UseVisualStyleBackColor = false;
            this.btnVencidas.Click += new System.EventHandler(this.btnVencidas_Click);
            // 
            // lblFecha
            // 
            this.lblFecha.AutoSize = true;
            this.lblFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFecha.Location = new System.Drawing.Point(72, 102);
            this.lblFecha.Name = "lblFecha";
            this.lblFecha.Size = new System.Drawing.Size(135, 13);
            this.lblFecha.TabIndex = 6;
            this.lblFecha.Text = "A partir del: YYYY/MM/DD";
            // 
            // creditoFiltradorPorVencer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 187);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.btnVencidas);
            this.Controls.Add(this.cbPeriodos);
            this.Controls.Add(this.numPeriodos);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "creditoFiltradorPorVencer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Creditos a vencer";
            this.Load += new System.EventHandler(this.creditoFiltradorPorVencer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numPeriodos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numPeriodos;
        private System.Windows.Forms.ComboBox cbPeriodos;
        private System.Windows.Forms.Button btnVencidas;
        private System.Windows.Forms.Label lblFecha;
    }
}