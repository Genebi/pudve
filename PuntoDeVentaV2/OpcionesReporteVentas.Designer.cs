
namespace PuntoDeVentaV2
{
    partial class OpcionesReporteVentas
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpcionesReporteVentas));
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.cbkGraficaVentas = new System.Windows.Forms.CheckBox();
            this.cbkTablaGraicaVentas = new System.Windows.Forms.CheckBox();
            this.cbkTablaProveedores = new System.Windows.Forms.CheckBox();
            this.cblGraficaProveedores = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Opciones del Reporte Ventas";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(54, 79);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(161, 17);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "Mostrar ventas desglosadas.";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(147, 154);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 34);
            this.button1.TabIndex = 2;
            this.button1.Text = "Aceptar";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(32, 154);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 34);
            this.button2.TabIndex = 3;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // cbkGraficaVentas
            // 
            this.cbkGraficaVentas.AutoSize = true;
            this.cbkGraficaVentas.Checked = true;
            this.cbkGraficaVentas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbkGraficaVentas.Location = new System.Drawing.Point(69, 33);
            this.cbkGraficaVentas.Name = "cbkGraficaVentas";
            this.cbkGraficaVentas.Size = new System.Drawing.Size(131, 17);
            this.cbkGraficaVentas.TabIndex = 4;
            this.cbkGraficaVentas.Text = "Mostrar grafica ventas";
            this.cbkGraficaVentas.UseVisualStyleBackColor = true;
            this.cbkGraficaVentas.CheckedChanged += new System.EventHandler(this.cbkGraficaVentas_CheckedChanged);
            // 
            // cbkTablaGraicaVentas
            // 
            this.cbkTablaGraicaVentas.AutoSize = true;
            this.cbkTablaGraicaVentas.Checked = true;
            this.cbkTablaGraicaVentas.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbkTablaGraicaVentas.Location = new System.Drawing.Point(32, 56);
            this.cbkTablaGraicaVentas.Name = "cbkTablaGraicaVentas";
            this.cbkTablaGraicaVentas.Size = new System.Drawing.Size(205, 17);
            this.cbkTablaGraicaVentas.TabIndex = 5;
            this.cbkTablaGraicaVentas.Text = "Mostrar Tabla de la Grafica de Ventas";
            this.cbkTablaGraicaVentas.UseVisualStyleBackColor = true;
            this.cbkTablaGraicaVentas.CheckedChanged += new System.EventHandler(this.cbkTablaGraicaVentas_CheckedChanged);
            // 
            // cbkTablaProveedores
            // 
            this.cbkTablaProveedores.AutoSize = true;
            this.cbkTablaProveedores.Checked = true;
            this.cbkTablaProveedores.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbkTablaProveedores.Location = new System.Drawing.Point(52, 102);
            this.cbkTablaProveedores.Name = "cbkTablaProveedores";
            this.cbkTablaProveedores.Size = new System.Drawing.Size(164, 17);
            this.cbkTablaProveedores.TabIndex = 6;
            this.cbkTablaProveedores.Text = "Mostrar tabla de proveedores";
            this.cbkTablaProveedores.UseVisualStyleBackColor = true;
            this.cbkTablaProveedores.CheckedChanged += new System.EventHandler(this.cbkTablaProveedores_CheckedChanged);
            // 
            // cblGraficaProveedores
            // 
            this.cblGraficaProveedores.AutoSize = true;
            this.cblGraficaProveedores.Checked = true;
            this.cblGraficaProveedores.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cblGraficaProveedores.Location = new System.Drawing.Point(48, 125);
            this.cblGraficaProveedores.Name = "cblGraficaProveedores";
            this.cblGraficaProveedores.Size = new System.Drawing.Size(173, 17);
            this.cblGraficaProveedores.TabIndex = 7;
            this.cblGraficaProveedores.Text = "Mostrar grafica de proveedores";
            this.cblGraficaProveedores.UseVisualStyleBackColor = true;
            this.cblGraficaProveedores.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // OpcionesReporteVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 195);
            this.Controls.Add(this.cblGraficaProveedores);
            this.Controls.Add(this.cbkTablaProveedores);
            this.Controls.Add(this.cbkTablaGraicaVentas);
            this.Controls.Add(this.cbkGraficaVentas);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpcionesReporteVentas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reporte Ventas";
            this.Load += new System.EventHandler(this.OpcionesReporteVentas_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox cbkGraficaVentas;
        private System.Windows.Forms.CheckBox cbkTablaGraicaVentas;
        private System.Windows.Forms.CheckBox cbkTablaProveedores;
        private System.Windows.Forms.CheckBox cblGraficaProveedores;
    }
}