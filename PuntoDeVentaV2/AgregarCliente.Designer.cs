namespace PuntoDeVentaV2
{
    partial class AgregarCliente
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
            this.txtRazonSocial = new System.Windows.Forms.TextBox();
            this.txtNombreComercial = new System.Windows.Forms.TextBox();
            this.txtRFC = new System.Windows.Forms.TextBox();
            this.txtPais = new System.Windows.Forms.TextBox();
            this.txtEstado = new System.Windows.Forms.TextBox();
            this.txtMunicipio = new System.Windows.Forms.TextBox();
            this.txtLocalidad = new System.Windows.Forms.TextBox();
            this.txtCP = new System.Windows.Forms.TextBox();
            this.txtColonia = new System.Windows.Forms.TextBox();
            this.txtCalle = new System.Windows.Forms.TextBox();
            this.txtNumExt = new System.Windows.Forms.TextBox();
            this.txtNumInt = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cbCliente = new System.Windows.Forms.CheckBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.label18 = new System.Windows.Forms.Label();
            this.cbUsoCFDI = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.cbTipoCliente = new System.Windows.Forms.ComboBox();
            this.lAgregarClienteNuevo = new System.Windows.Forms.Label();
            this.gbContenedor = new System.Windows.Forms.GroupBox();
            this.btnPublicoGeneral = new System.Windows.Forms.Button();
            this.gbContenedor.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtRazonSocial
            // 
            this.txtRazonSocial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRazonSocial.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRazonSocial.Location = new System.Drawing.Point(152, 20);
            this.txtRazonSocial.Name = "txtRazonSocial";
            this.txtRazonSocial.Size = new System.Drawing.Size(446, 21);
            this.txtRazonSocial.TabIndex = 1;
            this.txtRazonSocial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRazonSocial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRazonSocial_KeyDown);
            // 
            // txtNombreComercial
            // 
            this.txtNombreComercial.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNombreComercial.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreComercial.Location = new System.Drawing.Point(152, 65);
            this.txtNombreComercial.Name = "txtNombreComercial";
            this.txtNombreComercial.Size = new System.Drawing.Size(446, 21);
            this.txtNombreComercial.TabIndex = 2;
            this.txtNombreComercial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNombreComercial.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNombreComercial_KeyDown);
            // 
            // txtRFC
            // 
            this.txtRFC.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRFC.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRFC.Location = new System.Drawing.Point(152, 110);
            this.txtRFC.MaxLength = 13;
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.Size = new System.Drawing.Size(180, 21);
            this.txtRFC.TabIndex = 3;
            this.txtRFC.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRFC.TextChanged += new System.EventHandler(this.txtRFC_TextChanged);
            this.txtRFC.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRFC_KeyDown);
            this.txtRFC.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRFC_KeyPress);
            this.txtRFC.Leave += new System.EventHandler(this.valida_longitud);
            // 
            // txtPais
            // 
            this.txtPais.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtPais.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPais.Location = new System.Drawing.Point(152, 155);
            this.txtPais.Name = "txtPais";
            this.txtPais.Size = new System.Drawing.Size(180, 21);
            this.txtPais.TabIndex = 5;
            this.txtPais.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPais.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPais_KeyDown);
            // 
            // txtEstado
            // 
            this.txtEstado.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtEstado.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEstado.Location = new System.Drawing.Point(418, 155);
            this.txtEstado.Name = "txtEstado";
            this.txtEstado.Size = new System.Drawing.Size(180, 21);
            this.txtEstado.TabIndex = 6;
            this.txtEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEstado.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEstado_KeyDown);
            // 
            // txtMunicipio
            // 
            this.txtMunicipio.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtMunicipio.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMunicipio.Location = new System.Drawing.Point(152, 200);
            this.txtMunicipio.Name = "txtMunicipio";
            this.txtMunicipio.Size = new System.Drawing.Size(180, 21);
            this.txtMunicipio.TabIndex = 7;
            this.txtMunicipio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtMunicipio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMunicipio_KeyDown);
            // 
            // txtLocalidad
            // 
            this.txtLocalidad.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLocalidad.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLocalidad.Location = new System.Drawing.Point(418, 200);
            this.txtLocalidad.Name = "txtLocalidad";
            this.txtLocalidad.Size = new System.Drawing.Size(180, 21);
            this.txtLocalidad.TabIndex = 8;
            this.txtLocalidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtLocalidad.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtLocalidad_KeyDown);
            // 
            // txtCP
            // 
            this.txtCP.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCP.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCP.Location = new System.Drawing.Point(152, 245);
            this.txtCP.MaxLength = 7;
            this.txtCP.Name = "txtCP";
            this.txtCP.Size = new System.Drawing.Size(180, 21);
            this.txtCP.TabIndex = 9;
            this.txtCP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCP.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCP_KeyDown);
            // 
            // txtColonia
            // 
            this.txtColonia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtColonia.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtColonia.Location = new System.Drawing.Point(418, 245);
            this.txtColonia.Name = "txtColonia";
            this.txtColonia.Size = new System.Drawing.Size(180, 21);
            this.txtColonia.TabIndex = 10;
            this.txtColonia.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtColonia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtColonia_KeyDown);
            // 
            // txtCalle
            // 
            this.txtCalle.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtCalle.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCalle.Location = new System.Drawing.Point(152, 290);
            this.txtCalle.Name = "txtCalle";
            this.txtCalle.Size = new System.Drawing.Size(180, 21);
            this.txtCalle.TabIndex = 11;
            this.txtCalle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCalle.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCalle_KeyDown);
            // 
            // txtNumExt
            // 
            this.txtNumExt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumExt.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumExt.Location = new System.Drawing.Point(418, 290);
            this.txtNumExt.Name = "txtNumExt";
            this.txtNumExt.Size = new System.Drawing.Size(50, 21);
            this.txtNumExt.TabIndex = 12;
            this.txtNumExt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNumExt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumExt_KeyDown);
            // 
            // txtNumInt
            // 
            this.txtNumInt.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNumInt.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNumInt.Location = new System.Drawing.Point(548, 290);
            this.txtNumInt.Name = "txtNumInt";
            this.txtNumInt.Size = new System.Drawing.Size(50, 21);
            this.txtNumInt.TabIndex = 13;
            this.txtNumInt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNumInt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNumInt_KeyDown);
            // 
            // txtEmail
            // 
            this.txtEmail.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtEmail.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(152, 335);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(180, 21);
            this.txtEmail.TabIndex = 14;
            this.txtEmail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtEmail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmail_KeyDown);
            // 
            // txtTelefono
            // 
            this.txtTelefono.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTelefono.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.Location = new System.Drawing.Point(418, 335);
            this.txtTelefono.MaxLength = 10;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(180, 21);
            this.txtTelefono.TabIndex = 15;
            this.txtTelefono.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtTelefono.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTelefono_KeyDown);
            this.txtTelefono.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTelefono_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(31, 22);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(83, 17);
            this.label1.TabIndex = 52;
            this.label1.Text = "Razón Social";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(31, 52);
            this.label2.Name = "label2";
            this.label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label2.Size = new System.Drawing.Size(83, 34);
            this.label2.TabIndex = 54;
            this.label2.Text = "Nombre Comercial";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(84, 111);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label3.Size = new System.Drawing.Size(30, 17);
            this.label3.TabIndex = 56;
            this.label3.Text = "RFC";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(80, 157);
            this.label5.Name = "label5";
            this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label5.Size = new System.Drawing.Size(31, 17);
            this.label5.TabIndex = 58;
            this.label5.Text = "País";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(364, 157);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(48, 17);
            this.label6.TabIndex = 59;
            this.label6.Text = "Estado";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(45, 202);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(66, 17);
            this.label7.TabIndex = 60;
            this.label7.Text = "Municipio";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(344, 202);
            this.label8.Name = "label8";
            this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label8.Size = new System.Drawing.Size(68, 17);
            this.label8.TabIndex = 61;
            this.label8.Text = "Localidad";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(81, 247);
            this.label9.Name = "label9";
            this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label9.Size = new System.Drawing.Size(30, 17);
            this.label9.TabIndex = 62;
            this.label9.Text = "C.P.";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(358, 247);
            this.label10.Name = "label10";
            this.label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label10.Size = new System.Drawing.Size(54, 17);
            this.label10.TabIndex = 63;
            this.label10.Text = "Colonia";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(72, 292);
            this.label11.Name = "label11";
            this.label11.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label11.Size = new System.Drawing.Size(39, 17);
            this.label11.TabIndex = 64;
            this.label11.Text = "Calle";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(351, 292);
            this.label12.Name = "label12";
            this.label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label12.Size = new System.Drawing.Size(61, 17);
            this.label12.TabIndex = 65;
            this.label12.Text = "Núm. Ext.";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(483, 292);
            this.label13.Name = "label13";
            this.label13.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label13.Size = new System.Drawing.Size(59, 17);
            this.label13.TabIndex = 66;
            this.label13.Text = "Núm. Int.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(68, 337);
            this.label15.Name = "label15";
            this.label15.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label15.Size = new System.Drawing.Size(43, 17);
            this.label15.TabIndex = 68;
            this.label15.Text = "E-mail";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(353, 337);
            this.label16.Name = "label16";
            this.label16.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label16.Size = new System.Drawing.Size(59, 17);
            this.label16.TabIndex = 69;
            this.label16.Text = "Teléfono";
            // 
            // cbCliente
            // 
            this.cbCliente.AutoSize = true;
            this.cbCliente.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCliente.Location = new System.Drawing.Point(418, 114);
            this.cbCliente.Name = "cbCliente";
            this.cbCliente.Size = new System.Drawing.Size(176, 21);
            this.cbCliente.TabIndex = 4;
            this.cbCliente.Text = "Agregar cliente repetido";
            this.cbCliente.UseVisualStyleBackColor = true;
            this.cbCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbCliente_KeyDown);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(306, 526);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(144, 28);
            this.btnCancelar.TabIndex = 19;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnAceptar
            // 
            this.btnAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAceptar.BackColor = System.Drawing.Color.Green;
            this.btnAceptar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAceptar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAceptar.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAceptar.ForeColor = System.Drawing.Color.White;
            this.btnAceptar.Location = new System.Drawing.Point(454, 526);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(144, 28);
            this.btnAceptar.TabIndex = 18;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = false;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Red;
            this.label18.Location = new System.Drawing.Point(110, 24);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(14, 17);
            this.label18.TabIndex = 72;
            this.label18.Text = "*";
            // 
            // cbUsoCFDI
            // 
            this.cbUsoCFDI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbUsoCFDI.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbUsoCFDI.FormattingEnabled = true;
            this.cbUsoCFDI.Location = new System.Drawing.Point(152, 464);
            this.cbUsoCFDI.Name = "cbUsoCFDI";
            this.cbUsoCFDI.Size = new System.Drawing.Size(446, 24);
            this.cbUsoCFDI.TabIndex = 16;
            this.cbUsoCFDI.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbUsoCFDI_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 466);
            this.label4.Name = "label4";
            this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label4.Size = new System.Drawing.Size(81, 17);
            this.label4.TabIndex = 78;
            this.label4.Text = "Uso del CFDI";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(245, 427);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(185, 17);
            this.label14.TabIndex = 80;
            this.label14.Text = "DATOS EXTRA FACTURACIÓN";
            // 
            // label21
            // 
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label21.Location = new System.Drawing.Point(16, 414);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(580, 2);
            this.label21.TabIndex = 81;
            // 
            // label23
            // 
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label23.Location = new System.Drawing.Point(15, 505);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(580, 2);
            this.label23.TabIndex = 82;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(33, 376);
            this.label17.Name = "label17";
            this.label17.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label17.Size = new System.Drawing.Size(78, 17);
            this.label17.TabIndex = 83;
            this.label17.Text = "Tipo Cliente";
            // 
            // cbTipoCliente
            // 
            this.cbTipoCliente.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoCliente.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTipoCliente.FormattingEnabled = true;
            this.cbTipoCliente.Location = new System.Drawing.Point(152, 375);
            this.cbTipoCliente.Name = "cbTipoCliente";
            this.cbTipoCliente.Size = new System.Drawing.Size(180, 24);
            this.cbTipoCliente.TabIndex = 84;
            this.cbTipoCliente.Click += new System.EventHandler(this.cbTipoCliente_Click_1);
            this.cbTipoCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbTipoCliente_KeyDown);
            // 
            // lAgregarClienteNuevo
            // 
            this.lAgregarClienteNuevo.AutoSize = true;
            this.lAgregarClienteNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lAgregarClienteNuevo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lAgregarClienteNuevo.Location = new System.Drawing.Point(354, 382);
            this.lAgregarClienteNuevo.Name = "lAgregarClienteNuevo";
            this.lAgregarClienteNuevo.Size = new System.Drawing.Size(153, 13);
            this.lAgregarClienteNuevo.TabIndex = 85;
            this.lAgregarClienteNuevo.Text = "Agregar Nuevo Tipo de Cliente";
            this.lAgregarClienteNuevo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lAgregarClienteNuevo.Click += new System.EventHandler(this.lAgregarClienteNuevo_Click);
            // 
            // gbContenedor
            // 
            this.gbContenedor.Controls.Add(this.btnPublicoGeneral);
            this.gbContenedor.Controls.Add(this.lAgregarClienteNuevo);
            this.gbContenedor.Controls.Add(this.cbTipoCliente);
            this.gbContenedor.Controls.Add(this.label17);
            this.gbContenedor.Controls.Add(this.label23);
            this.gbContenedor.Controls.Add(this.label21);
            this.gbContenedor.Controls.Add(this.label14);
            this.gbContenedor.Controls.Add(this.label4);
            this.gbContenedor.Controls.Add(this.cbUsoCFDI);
            this.gbContenedor.Controls.Add(this.label18);
            this.gbContenedor.Controls.Add(this.btnAceptar);
            this.gbContenedor.Controls.Add(this.btnCancelar);
            this.gbContenedor.Controls.Add(this.cbCliente);
            this.gbContenedor.Controls.Add(this.label16);
            this.gbContenedor.Controls.Add(this.label15);
            this.gbContenedor.Controls.Add(this.label13);
            this.gbContenedor.Controls.Add(this.label12);
            this.gbContenedor.Controls.Add(this.label11);
            this.gbContenedor.Controls.Add(this.label10);
            this.gbContenedor.Controls.Add(this.label9);
            this.gbContenedor.Controls.Add(this.label8);
            this.gbContenedor.Controls.Add(this.label7);
            this.gbContenedor.Controls.Add(this.label6);
            this.gbContenedor.Controls.Add(this.label5);
            this.gbContenedor.Controls.Add(this.label3);
            this.gbContenedor.Controls.Add(this.label2);
            this.gbContenedor.Controls.Add(this.label1);
            this.gbContenedor.Controls.Add(this.txtTelefono);
            this.gbContenedor.Controls.Add(this.txtEmail);
            this.gbContenedor.Controls.Add(this.txtNumInt);
            this.gbContenedor.Controls.Add(this.txtNumExt);
            this.gbContenedor.Controls.Add(this.txtCalle);
            this.gbContenedor.Controls.Add(this.txtColonia);
            this.gbContenedor.Controls.Add(this.txtCP);
            this.gbContenedor.Controls.Add(this.txtLocalidad);
            this.gbContenedor.Controls.Add(this.txtMunicipio);
            this.gbContenedor.Controls.Add(this.txtEstado);
            this.gbContenedor.Controls.Add(this.txtPais);
            this.gbContenedor.Controls.Add(this.txtRFC);
            this.gbContenedor.Controls.Add(this.txtNombreComercial);
            this.gbContenedor.Controls.Add(this.txtRazonSocial);
            this.gbContenedor.Location = new System.Drawing.Point(12, 12);
            this.gbContenedor.Name = "gbContenedor";
            this.gbContenedor.Size = new System.Drawing.Size(610, 570);
            this.gbContenedor.TabIndex = 0;
            this.gbContenedor.TabStop = false;
            this.gbContenedor.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.gbContenedor_PreviewKeyDown);
            // 
            // btnPublicoGeneral
            // 
            this.btnPublicoGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPublicoGeneral.BackColor = System.Drawing.Color.Green;
            this.btnPublicoGeneral.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPublicoGeneral.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LimeGreen;
            this.btnPublicoGeneral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPublicoGeneral.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPublicoGeneral.ForeColor = System.Drawing.Color.White;
            this.btnPublicoGeneral.Location = new System.Drawing.Point(454, 89);
            this.btnPublicoGeneral.Name = "btnPublicoGeneral";
            this.btnPublicoGeneral.Size = new System.Drawing.Size(106, 28);
            this.btnPublicoGeneral.TabIndex = 86;
            this.btnPublicoGeneral.Text = "Publico General";
            this.btnPublicoGeneral.UseVisualStyleBackColor = false;
            this.btnPublicoGeneral.Click += new System.EventHandler(this.button1_Click);
            // 
            // AgregarCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 591);
            this.Controls.Add(this.gbContenedor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "AgregarCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PUDVE - Nuevo Cliente";
            this.Load += new System.EventHandler(this.AgregarCliente_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AgregarCliente_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.AgregarCliente_PreviewKeyDown);
            this.gbContenedor.ResumeLayout(false);
            this.gbContenedor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtRazonSocial;
        private System.Windows.Forms.TextBox txtNombreComercial;
        private System.Windows.Forms.TextBox txtRFC;
        private System.Windows.Forms.TextBox txtPais;
        private System.Windows.Forms.TextBox txtEstado;
        private System.Windows.Forms.TextBox txtMunicipio;
        private System.Windows.Forms.TextBox txtLocalidad;
        private System.Windows.Forms.TextBox txtCP;
        private System.Windows.Forms.TextBox txtColonia;
        private System.Windows.Forms.TextBox txtCalle;
        private System.Windows.Forms.TextBox txtNumExt;
        private System.Windows.Forms.TextBox txtNumInt;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.CheckBox cbCliente;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox cbUsoCFDI;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox cbTipoCliente;
        private System.Windows.Forms.Label lAgregarClienteNuevo;
        private System.Windows.Forms.GroupBox gbContenedor;
        private System.Windows.Forms.Button btnPublicoGeneral;
    }
}