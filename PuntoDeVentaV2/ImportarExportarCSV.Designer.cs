﻿
namespace PuntoDeVentaV2
{
    partial class ImportarExportarCSV
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
            this.btnImportar = new PuntoDeVentaV2.BotonRedondo();
            this.btnExportar = new PuntoDeVentaV2.BotonRedondo();
            this.btnTerminar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnImportar
            // 
            this.btnImportar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnImportar.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnImportar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnImportar.BorderRadius = 40;
            this.btnImportar.BorderSize = 0;
            this.btnImportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImportar.FlatAppearance.BorderSize = 0;
            this.btnImportar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnImportar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnImportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImportar.ForeColor = System.Drawing.Color.White;
            this.btnImportar.Image = global::PuntoDeVentaV2.Properties.Resources.cloud_download1;
            this.btnImportar.Location = new System.Drawing.Point(12, 12);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(134, 87);
            this.btnImportar.TabIndex = 134;
            this.btnImportar.Text = "Importar";
            this.btnImportar.TextColor = System.Drawing.Color.White;
            this.btnImportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnImportar.UseMnemonic = false;
            this.btnImportar.UseVisualStyleBackColor = false;
            this.btnImportar.Click += new System.EventHandler(this.btnCsv_Click);
            // 
            // btnExportar
            // 
            this.btnExportar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnExportar.BackGroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnExportar.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnExportar.BorderRadius = 40;
            this.btnExportar.BorderSize = 0;
            this.btnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportar.FlatAppearance.BorderSize = 0;
            this.btnExportar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.btnExportar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnExportar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportar.ForeColor = System.Drawing.Color.White;
            this.btnExportar.Image = global::PuntoDeVentaV2.Properties.Resources.cloud_upload2;
            this.btnExportar.Location = new System.Drawing.Point(175, 12);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(134, 87);
            this.btnExportar.TabIndex = 134;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.TextColor = System.Drawing.Color.White;
            this.btnExportar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnExportar.UseMnemonic = false;
            this.btnExportar.UseVisualStyleBackColor = false;
            this.btnExportar.Click += new System.EventHandler(this.btnCsv_Click);
            // 
            // btnTerminar
            // 
            this.btnTerminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTerminar.BackColor = System.Drawing.Color.Green;
            this.btnTerminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTerminar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnTerminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTerminar.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTerminar.ForeColor = System.Drawing.Color.White;
            this.btnTerminar.Location = new System.Drawing.Point(179, 132);
            this.btnTerminar.Name = "btnTerminar";
            this.btnTerminar.Size = new System.Drawing.Size(130, 28);
            this.btnTerminar.TabIndex = 135;
            this.btnTerminar.Text = "Terminar";
            this.btnTerminar.UseVisualStyleBackColor = false;
            this.btnTerminar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // ImportarExportarCSV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 172);
            this.Controls.Add(this.btnTerminar);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.btnImportar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ImportarExportarCSV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importar o Exportar CSV";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ImportarExportarCSV_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion
        private BotonRedondo btnImportar;
        private BotonRedondo btnExportar;
        private System.Windows.Forms.Button btnTerminar;
    }
}