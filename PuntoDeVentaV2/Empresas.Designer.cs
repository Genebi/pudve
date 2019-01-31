namespace PuntoDeVentaV2
{
    partial class Empresas
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.LblNombreUsr = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LblTipoPersona = new System.Windows.Forms.Label();
            this.LblRFC = new System.Windows.Forms.Label();
            this.DGVListaEmpresas = new System.Windows.Forms.DataGridView();
            this.btnNvaEmpresa = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.DGVListaEmpresas)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 34);
            this.label1.TabIndex = 0;
            this.label1.Text = "Empresas de:";
            // 
            // LblNombreUsr
            // 
            this.LblNombreUsr.Font = new System.Drawing.Font("Century Gothic", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblNombreUsr.Location = new System.Drawing.Point(19, 73);
            this.LblNombreUsr.Name = "LblNombreUsr";
            this.LblNombreUsr.Size = new System.Drawing.Size(1382, 57);
            this.LblNombreUsr.TabIndex = 1;
            this.LblNombreUsr.Text = "Nombre de Usuario";
            this.LblNombreUsr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Century Gothic", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label3.Location = new System.Drawing.Point(19, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(685, 38);
            this.label3.TabIndex = 2;
            this.label3.Text = "Tipo de Persona";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Century Gothic", 13.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(726, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(675, 38);
            this.label4.TabIndex = 3;
            this.label4.Text = "R.F.C.:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblTipoPersona
            // 
            this.LblTipoPersona.Font = new System.Drawing.Font("Century Gothic", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.LblTipoPersona.Location = new System.Drawing.Point(19, 220);
            this.LblTipoPersona.Name = "LblTipoPersona";
            this.LblTipoPersona.Size = new System.Drawing.Size(685, 51);
            this.LblTipoPersona.TabIndex = 4;
            this.LblTipoPersona.Text = "Física / Moral";
            this.LblTipoPersona.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LblRFC
            // 
            this.LblRFC.Font = new System.Drawing.Font("Century Gothic", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.LblRFC.Location = new System.Drawing.Point(731, 220);
            this.LblRFC.Name = "LblRFC";
            this.LblRFC.Size = new System.Drawing.Size(670, 51);
            this.LblRFC.TabIndex = 5;
            this.LblRFC.Text = "R.F.C. de la persona";
            this.LblRFC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DGVListaEmpresas
            // 
            this.DGVListaEmpresas.AllowUserToAddRows = false;
            this.DGVListaEmpresas.AllowUserToDeleteRows = false;
            this.DGVListaEmpresas.AllowUserToResizeColumns = false;
            this.DGVListaEmpresas.AllowUserToResizeRows = false;
            this.DGVListaEmpresas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.DGVListaEmpresas.BackgroundColor = System.Drawing.SystemColors.Control;
            this.DGVListaEmpresas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DGVListaEmpresas.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.SystemColors.HotTrack;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Century Schoolbook", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.MenuBar;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVListaEmpresas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.DGVListaEmpresas.ColumnHeadersHeight = 30;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Century Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.InactiveCaption;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVListaEmpresas.DefaultCellStyle = dataGridViewCellStyle14;
            this.DGVListaEmpresas.EnableHeadersVisualStyles = false;
            this.DGVListaEmpresas.Location = new System.Drawing.Point(19, 313);
            this.DGVListaEmpresas.Name = "DGVListaEmpresas";
            this.DGVListaEmpresas.ReadOnly = true;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Century Schoolbook", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVListaEmpresas.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.DGVListaEmpresas.RowTemplate.Height = 24;
            this.DGVListaEmpresas.Size = new System.Drawing.Size(1382, 321);
            this.DGVListaEmpresas.TabIndex = 6;
            this.DGVListaEmpresas.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.DGVListaEmpresas_CellPainting);
            // 
            // btnNvaEmpresa
            // 
            this.btnNvaEmpresa.BackColor = System.Drawing.Color.Green;
            this.btnNvaEmpresa.Font = new System.Drawing.Font("Century Gothic", 16.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNvaEmpresa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNvaEmpresa.Location = new System.Drawing.Point(1171, 668);
            this.btnNvaEmpresa.Name = "btnNvaEmpresa";
            this.btnNvaEmpresa.Size = new System.Drawing.Size(210, 58);
            this.btnNvaEmpresa.TabIndex = 7;
            this.btnNvaEmpresa.Text = "Empresa";
            this.btnNvaEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNvaEmpresa.UseVisualStyleBackColor = false;
            this.btnNvaEmpresa.Click += new System.EventHandler(this.btnNvaEmpresa_Click);
            // 
            // Empresas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1431, 762);
            this.Controls.Add(this.btnNvaEmpresa);
            this.Controls.Add(this.DGVListaEmpresas);
            this.Controls.Add(this.LblRFC);
            this.Controls.Add(this.LblTipoPersona);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LblNombreUsr);
            this.Controls.Add(this.label1);
            this.Name = "Empresas";
            this.Text = "Empresas";
            this.Load += new System.EventHandler(this.Empresas_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVListaEmpresas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LblNombreUsr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LblTipoPersona;
        private System.Windows.Forms.Label LblRFC;
        private System.Windows.Forms.DataGridView DGVListaEmpresas;
        private System.Windows.Forms.Button btnNvaEmpresa;
    }
}