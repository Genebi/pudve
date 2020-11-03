﻿namespace PuntoDeVentaV2
{
    partial class Agregar_empleado
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
            this.lbTitulo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_nombre = new System.Windows.Forms.TextBox();
            this.txt_usuario = new System.Windows.Forms.TextBox();
            this.lb_usuario = new System.Windows.Forms.Label();
            this.txt_conttraseña = new System.Windows.Forms.TextBox();
            this.lb_usuario_completo = new System.Windows.Forms.Label();
            this.btn_aceptar = new System.Windows.Forms.Button();
            this.btn_cancelar = new System.Windows.Forms.Button();
            this.lb_permisos_contraseña = new System.Windows.Forms.Label();
            this.cmb_bx_permisos = new System.Windows.Forms.ComboBox();
            this.txt_autorizar = new System.Windows.Forms.TextBox();
            this.picturebx_editar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picturebx_editar)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitulo
            // 
            this.lbTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbTitulo.AutoSize = true;
            this.lbTitulo.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.Location = new System.Drawing.Point(95, 25);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(203, 25);
            this.lbTitulo.TabIndex = 0;
            this.lbTitulo.Text = "NUEVO EMPLEADO";
            this.lbTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 80);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 123);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(51, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Usuario";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 184);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label4.Size = new System.Drawing.Size(77, 17);
            this.label4.TabIndex = 3;
            this.label4.Text = "Contraseña";
            // 
            // txt_nombre
            // 
            this.txt_nombre.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_nombre.Location = new System.Drawing.Point(140, 77);
            this.txt_nombre.MaxLength = 100;
            this.txt_nombre.Name = "txt_nombre";
            this.txt_nombre.Size = new System.Drawing.Size(190, 22);
            this.txt_nombre.TabIndex = 4;
            this.txt_nombre.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_usuario
            // 
            this.txt_usuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txt_usuario.Location = new System.Drawing.Point(140, 120);
            this.txt_usuario.MaxLength = 30;
            this.txt_usuario.Name = "txt_usuario";
            this.txt_usuario.Size = new System.Drawing.Size(190, 22);
            this.txt_usuario.TabIndex = 5;
            this.txt_usuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_usuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.solo_letras_digitos);
            this.txt_usuario.KeyUp += new System.Windows.Forms.KeyEventHandler(this.muestra_usuarioc);
            this.txt_usuario.Leave += new System.EventHandler(this.verifica_usuario_empleado);
            // 
            // lb_usuario
            // 
            this.lb_usuario.AutoSize = true;
            this.lb_usuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lb_usuario.Location = new System.Drawing.Point(28, 155);
            this.lb_usuario.Name = "lb_usuario";
            this.lb_usuario.Size = new System.Drawing.Size(51, 17);
            this.lb_usuario.TabIndex = 6;
            this.lb_usuario.Text = "Usuario";
            this.lb_usuario.Visible = false;
            // 
            // txt_conttraseña
            // 
            this.txt_conttraseña.Location = new System.Drawing.Point(140, 184);
            this.txt_conttraseña.MaxLength = 100;
            this.txt_conttraseña.Name = "txt_conttraseña";
            this.txt_conttraseña.PasswordChar = '*';
            this.txt_conttraseña.Size = new System.Drawing.Size(190, 22);
            this.txt_conttraseña.TabIndex = 7;
            this.txt_conttraseña.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_conttraseña.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.solo_letras_digitos);
            // 
            // lb_usuario_completo
            // 
            this.lb_usuario_completo.AutoSize = true;
            this.lb_usuario_completo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lb_usuario_completo.Location = new System.Drawing.Point(140, 155);
            this.lb_usuario_completo.Name = "lb_usuario_completo";
            this.lb_usuario_completo.Size = new System.Drawing.Size(18, 17);
            this.lb_usuario_completo.TabIndex = 8;
            this.lb_usuario_completo.Text = "@";
            this.lb_usuario_completo.Visible = false;
            // 
            // btn_aceptar
            // 
            this.btn_aceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_aceptar.BackColor = System.Drawing.Color.Green;
            this.btn_aceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_aceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_aceptar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_aceptar.ForeColor = System.Drawing.Color.White;
            this.btn_aceptar.Location = new System.Drawing.Point(200, 314);
            this.btn_aceptar.Name = "btn_aceptar";
            this.btn_aceptar.Size = new System.Drawing.Size(119, 30);
            this.btn_aceptar.TabIndex = 9;
            this.btn_aceptar.Text = "Aceptar";
            this.btn_aceptar.UseVisualStyleBackColor = false;
            this.btn_aceptar.Click += new System.EventHandler(this.btn_aceptar_Click);
            // 
            // btn_cancelar
            // 
            this.btn_cancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancelar.BackColor = System.Drawing.Color.Red;
            this.btn_cancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_cancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancelar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancelar.ForeColor = System.Drawing.Color.White;
            this.btn_cancelar.Location = new System.Drawing.Point(53, 314);
            this.btn_cancelar.Name = "btn_cancelar";
            this.btn_cancelar.Size = new System.Drawing.Size(119, 30);
            this.btn_cancelar.TabIndex = 10;
            this.btn_cancelar.Text = "Cancelar";
            this.btn_cancelar.UseVisualStyleBackColor = false;
            this.btn_cancelar.Click += new System.EventHandler(this.btn_cancelar_Click);
            // 
            // lb_permisos_contraseña
            // 
            this.lb_permisos_contraseña.AutoSize = true;
            this.lb_permisos_contraseña.Location = new System.Drawing.Point(25, 231);
            this.lb_permisos_contraseña.Name = "lb_permisos_contraseña";
            this.lb_permisos_contraseña.Size = new System.Drawing.Size(107, 17);
            this.lb_permisos_contraseña.TabIndex = 11;
            this.lb_permisos_contraseña.Text = "Asignar permisos";
            // 
            // cmb_bx_permisos
            // 
            this.cmb_bx_permisos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_bx_permisos.FormattingEnabled = true;
            this.cmb_bx_permisos.Items.AddRange(new object[] {
            "Todos los permisos",
            "Permisos limitados",
            "Elegir permisos"});
            this.cmb_bx_permisos.Location = new System.Drawing.Point(140, 228);
            this.cmb_bx_permisos.Name = "cmb_bx_permisos";
            this.cmb_bx_permisos.Size = new System.Drawing.Size(190, 25);
            this.cmb_bx_permisos.TabIndex = 13;
            // 
            // txt_autorizar
            // 
            this.txt_autorizar.Location = new System.Drawing.Point(140, 233);
            this.txt_autorizar.MaxLength = 100;
            this.txt_autorizar.Name = "txt_autorizar";
            this.txt_autorizar.PasswordChar = '*';
            this.txt_autorizar.Size = new System.Drawing.Size(190, 22);
            this.txt_autorizar.TabIndex = 14;
            this.txt_autorizar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_autorizar.Visible = false;
            // 
            // picturebx_editar
            // 
            this.picturebx_editar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picturebx_editar.Image = global::PuntoDeVentaV2.Properties.Resources.edit;
            this.picturebx_editar.Location = new System.Drawing.Point(336, 186);
            this.picturebx_editar.Name = "picturebx_editar";
            this.picturebx_editar.Size = new System.Drawing.Size(18, 18);
            this.picturebx_editar.TabIndex = 15;
            this.picturebx_editar.TabStop = false;
            this.picturebx_editar.Visible = false;
            this.picturebx_editar.Click += new System.EventHandler(this.click_editar_contraseña);
            // 
            // Agregar_empleado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 356);
            this.Controls.Add(this.picturebx_editar);
            this.Controls.Add(this.txt_autorizar);
            this.Controls.Add(this.cmb_bx_permisos);
            this.Controls.Add(this.lb_permisos_contraseña);
            this.Controls.Add(this.btn_cancelar);
            this.Controls.Add(this.btn_aceptar);
            this.Controls.Add(this.lb_usuario_completo);
            this.Controls.Add(this.txt_conttraseña);
            this.Controls.Add(this.lb_usuario);
            this.Controls.Add(this.txt_usuario);
            this.Controls.Add(this.txt_nombre);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbTitulo);
            this.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Agregar_empleado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Agregar Empleado";
            this.Load += new System.EventHandler(this.Agregar_empleado_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picturebx_editar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_nombre;
        private System.Windows.Forms.TextBox txt_usuario;
        private System.Windows.Forms.Label lb_usuario;
        private System.Windows.Forms.TextBox txt_conttraseña;
        private System.Windows.Forms.Label lb_usuario_completo;
        private System.Windows.Forms.Button btn_aceptar;
        private System.Windows.Forms.Button btn_cancelar;
        private System.Windows.Forms.Label lb_permisos_contraseña;
        private System.Windows.Forms.ComboBox cmb_bx_permisos;
        private System.Windows.Forms.TextBox txt_autorizar;
        private System.Windows.Forms.PictureBox picturebx_editar;
    }
}