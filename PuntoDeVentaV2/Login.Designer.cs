namespace PuntoDeVentaV2
{
    partial class Login
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.btnCrearCuenta = new System.Windows.Forms.Label();
            this.txtMensaje = new System.Windows.Forms.Label();
            this.checkBoxRecordarDatos = new System.Windows.Forms.CheckBox();
            this.btnLimpiarDatos = new System.Windows.Forms.Button();
            this.buscarArchivoBD = new System.Windows.Forms.OpenFileDialog();
            this.mensajeBoton = new System.Windows.Forms.ToolTip(this.components);
            this.menuLogin = new System.Windows.Forms.MenuStrip();
            this.opcionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desvincularPCMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importarBaseDeDatosMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vincularPCEnRedMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRecuperarPassword = new System.Windows.Forms.Label();
            this.menuLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(60, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuario";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Password";
            // 
            // txtUsuario
            // 
            this.txtUsuario.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtUsuario.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.Location = new System.Drawing.Point(120, 42);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(190, 23);
            this.txtUsuario.TabIndex = 2;
            this.txtUsuario.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUsuario.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUsuario_KeyPress);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.Location = new System.Drawing.Point(120, 74);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(190, 23);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // btnEntrar
            // 
            this.btnEntrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEntrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(144)))));
            this.btnEntrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEntrar.FlatAppearance.BorderSize = 0;
            this.btnEntrar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(190)))));
            this.btnEntrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(104)))), ((int)(((byte)(190)))));
            this.btnEntrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEntrar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEntrar.ForeColor = System.Drawing.Color.White;
            this.btnEntrar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEntrar.Location = new System.Drawing.Point(121, 163);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(190, 27);
            this.btnEntrar.TabIndex = 4;
            this.btnEntrar.Text = "Entrar";
            this.btnEntrar.UseVisualStyleBackColor = false;
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // btnCrearCuenta
            // 
            this.btnCrearCuenta.AutoSize = true;
            this.btnCrearCuenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCrearCuenta.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCrearCuenta.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnCrearCuenta.Location = new System.Drawing.Point(139, 284);
            this.btnCrearCuenta.Name = "btnCrearCuenta";
            this.btnCrearCuenta.Size = new System.Drawing.Size(139, 17);
            this.btnCrearCuenta.TabIndex = 6;
            this.btnCrearCuenta.Text = "Crear cuenta nueva";
            this.btnCrearCuenta.Click += new System.EventHandler(this.btnCrearCuenta_Click);
            // 
            // txtMensaje
            // 
            this.txtMensaje.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMensaje.ForeColor = System.Drawing.Color.Red;
            this.txtMensaje.Location = new System.Drawing.Point(12, 241);
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(396, 36);
            this.txtMensaje.TabIndex = 7;
            this.txtMensaje.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // checkBoxRecordarDatos
            // 
            this.checkBoxRecordarDatos.AutoSize = true;
            this.checkBoxRecordarDatos.Location = new System.Drawing.Point(167, 141);
            this.checkBoxRecordarDatos.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxRecordarDatos.Name = "checkBoxRecordarDatos";
            this.checkBoxRecordarDatos.Size = new System.Drawing.Size(101, 17);
            this.checkBoxRecordarDatos.TabIndex = 8;
            this.checkBoxRecordarDatos.Text = "Recordar Datos";
            this.checkBoxRecordarDatos.UseVisualStyleBackColor = true;
            // 
            // btnLimpiarDatos
            // 
            this.btnLimpiarDatos.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnLimpiarDatos.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnLimpiarDatos.ForeColor = System.Drawing.Color.Brown;
            this.btnLimpiarDatos.Location = new System.Drawing.Point(120, 197);
            this.btnLimpiarDatos.Margin = new System.Windows.Forms.Padding(2);
            this.btnLimpiarDatos.Name = "btnLimpiarDatos";
            this.btnLimpiarDatos.Size = new System.Drawing.Size(190, 27);
            this.btnLimpiarDatos.TabIndex = 9;
            this.btnLimpiarDatos.Text = "Limpiar Datos";
            this.btnLimpiarDatos.UseVisualStyleBackColor = false;
            this.btnLimpiarDatos.Click += new System.EventHandler(this.btnLimpiarDatos_Click);
            // 
            // buscarArchivoBD
            // 
            this.buscarArchivoBD.Title = "Seleccionar archivo para importar";
            // 
            // menuLogin
            // 
            this.menuLogin.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.opcionesToolStripMenuItem});
            this.menuLogin.Location = new System.Drawing.Point(0, 0);
            this.menuLogin.Name = "menuLogin";
            this.menuLogin.Size = new System.Drawing.Size(420, 24);
            this.menuLogin.TabIndex = 11;
            this.menuLogin.Text = "Menu";
            // 
            // opcionesToolStripMenuItem
            // 
            this.opcionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.desvincularPCMenuItem,
            this.importarBaseDeDatosMenuItem,
            this.vincularPCEnRedMenuItem});
            this.opcionesToolStripMenuItem.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            this.opcionesToolStripMenuItem.Name = "opcionesToolStripMenuItem";
            this.opcionesToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.opcionesToolStripMenuItem.Size = new System.Drawing.Size(28, 20);
            // 
            // desvincularPCMenuItem
            // 
            this.desvincularPCMenuItem.Name = "desvincularPCMenuItem";
            this.desvincularPCMenuItem.Size = new System.Drawing.Size(195, 22);
            this.desvincularPCMenuItem.Text = "Desvincular PC en red";
            this.desvincularPCMenuItem.Click += new System.EventHandler(this.desvincularPCMenuItem_Click);
            // 
            // importarBaseDeDatosMenuItem
            // 
            this.importarBaseDeDatosMenuItem.Name = "importarBaseDeDatosMenuItem";
            this.importarBaseDeDatosMenuItem.Size = new System.Drawing.Size(195, 22);
            this.importarBaseDeDatosMenuItem.Text = "Importar base de datos";
            this.importarBaseDeDatosMenuItem.Click += new System.EventHandler(this.importarBaseDeDatosMenuItem_Click);
            // 
            // vincularPCEnRedMenuItem
            // 
            this.vincularPCEnRedMenuItem.Name = "vincularPCEnRedMenuItem";
            this.vincularPCEnRedMenuItem.Size = new System.Drawing.Size(195, 22);
            this.vincularPCEnRedMenuItem.Text = "Vincular PC en red";
            this.vincularPCEnRedMenuItem.Click += new System.EventHandler(this.vincularPCEnRedMenuItem_Click);
            // 
            // btnRecuperarPassword
            // 
            this.btnRecuperarPassword.AutoSize = true;
            this.btnRecuperarPassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRecuperarPassword.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRecuperarPassword.ForeColor = System.Drawing.SystemColors.Highlight;
            this.btnRecuperarPassword.Location = new System.Drawing.Point(139, 100);
            this.btnRecuperarPassword.Name = "btnRecuperarPassword";
            this.btnRecuperarPassword.Size = new System.Drawing.Size(154, 16);
            this.btnRecuperarPassword.TabIndex = 12;
            this.btnRecuperarPassword.Text = "¿Olvidaste tu contraseña?";
            this.btnRecuperarPassword.Click += new System.EventHandler(this.btnRecuperarPassword_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(420, 312);
            this.Controls.Add(this.btnRecuperarPassword);
            this.Controls.Add(this.btnLimpiarDatos);
            this.Controls.Add(this.checkBoxRecordarDatos);
            this.Controls.Add(this.txtMensaje);
            this.Controls.Add(this.btnCrearCuenta);
            this.Controls.Add(this.btnEntrar);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsuario);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuLogin);
            this.MainMenuStrip = this.menuLogin;
            this.MaximizeBox = false;
            this.Name = "Login";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Login_Load);
            this.menuLogin.ResumeLayout(false);
            this.menuLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnEntrar;
        private System.Windows.Forms.Label btnCrearCuenta;
        private System.Windows.Forms.Label txtMensaje;
        private System.Windows.Forms.CheckBox checkBoxRecordarDatos;
        private System.Windows.Forms.Button btnLimpiarDatos;
        private System.Windows.Forms.OpenFileDialog buscarArchivoBD;
        private System.Windows.Forms.ToolTip mensajeBoton;
        private System.Windows.Forms.MenuStrip menuLogin;
        private System.Windows.Forms.ToolStripMenuItem opcionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem desvincularPCMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importarBaseDeDatosMenuItem;
        private System.Windows.Forms.Label btnRecuperarPassword;
        private System.Windows.Forms.ToolStripMenuItem vincularPCEnRedMenuItem;
    }
}