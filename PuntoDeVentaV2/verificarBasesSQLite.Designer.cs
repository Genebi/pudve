namespace PuntoDeVentaV2
{
    partial class verificarBasesSQLite
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pBCheckDB = new System.Windows.Forms.ProgressBar();
            this.verificarDBSQLiteSegundoPlano = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.pBCheckDB);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 102);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cargando la Configuracón";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(167, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Iniciando Verficación";
            // 
            // pBCheckDB
            // 
            this.pBCheckDB.Location = new System.Drawing.Point(18, 53);
            this.pBCheckDB.Name = "pBCheckDB";
            this.pBCheckDB.Size = new System.Drawing.Size(457, 23);
            this.pBCheckDB.TabIndex = 0;
            // 
            // verificarDBSQLiteSegundoPlano
            // 
            this.verificarDBSQLiteSegundoPlano.WorkerReportsProgress = true;
            this.verificarDBSQLiteSegundoPlano.WorkerSupportsCancellation = true;
            this.verificarDBSQLiteSegundoPlano.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.verificarDBSQLiteSegundoPlano_RunWorkerCompleted);
            // 
            // verificarBasesSQLite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 158);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "verificarBasesSQLite";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Verificar y Actualizar Configuración del Sistema";
            this.Shown += new System.EventHandler(this.verificarBasesSQLite_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar pBCheckDB;
        private System.ComponentModel.BackgroundWorker verificarDBSQLiteSegundoPlano;
    }
}