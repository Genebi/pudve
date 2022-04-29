namespace PuntoDeVentaV2
{
    partial class UsuariosGuardados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsuariosGuardados));
            this.dgvUsuariosGuardados = new System.Windows.Forms.DataGridView();
            this._user = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._eliminar = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuariosGuardados)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvUsuariosGuardados
            // 
            this.dgvUsuariosGuardados.AllowUserToAddRows = false;
            this.dgvUsuariosGuardados.AllowUserToDeleteRows = false;
            this.dgvUsuariosGuardados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsuariosGuardados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuariosGuardados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._user,
            this._eliminar});
            this.dgvUsuariosGuardados.Location = new System.Drawing.Point(13, 13);
            this.dgvUsuariosGuardados.Name = "dgvUsuariosGuardados";
            this.dgvUsuariosGuardados.ReadOnly = true;
            this.dgvUsuariosGuardados.RowHeadersVisible = false;
            this.dgvUsuariosGuardados.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvUsuariosGuardados.Size = new System.Drawing.Size(360, 183);
            this.dgvUsuariosGuardados.TabIndex = 0;
            this.dgvUsuariosGuardados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuariosGuardados_CellContentClick);
            this.dgvUsuariosGuardados.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsuariosGuardados_CellMouseEnter);
            // 
            // _user
            // 
            this._user.HeaderText = "Usuario";
            this._user.Name = "_user";
            this._user.ReadOnly = true;
            // 
            // _eliminar
            // 
            this._eliminar.HeaderText = "Eliminar";
            this._eliminar.Image = global::PuntoDeVentaV2.Properties.Resources.trash;
            this._eliminar.Name = "_eliminar";
            this._eliminar.ReadOnly = true;
            // 
            // UsuariosGuardados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 208);
            this.Controls.Add(this.dgvUsuariosGuardados);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UsuariosGuardados";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UsuariosGuardados";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UsuariosGuardados_FormClosing);
            this.Load += new System.EventHandler(this.UsuariosGuardados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuariosGuardados)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvUsuariosGuardados;
        private System.Windows.Forms.DataGridViewTextBoxColumn _user;
        private System.Windows.Forms.DataGridViewImageColumn _eliminar;
    }
}