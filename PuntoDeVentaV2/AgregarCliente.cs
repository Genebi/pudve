using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace PuntoDeVentaV2
{
    public partial class AgregarCliente : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();
        string consultaReglas = string.Empty;

        //tipo 1 = agregar
        //tipo 2 = editar
        //tipo 3 = confirmar y editar para antes de timbrar
        private int tipo = 0;
        private int idCliente = 0;
        private int idVenta = 0;
        bool validarRFC = false;
        string verificado="0";

        //idVenta como parametro es para cuando se agrega un cliente al momento de querer timbrar
        //una venta, de esta manera se asigna el ID del cliente al terminar el registro de esta manera
        //con la venta la cual se intenta timbrar
        public AgregarCliente(int tipo = 1, int idCliente = 0, int idVenta = 0)
        {
            InitializeComponent();

            this.tipo = tipo;
            this.idCliente = idCliente;
            this.idVenta = idVenta;
        }

        private void AgregarCliente_Load(object sender, EventArgs e)
        {
            if (tipo==2)
            {
                label19.Visible = true;
                
            }
            AgregarCliente form = this;
            Utilidades.EjecutarAtajoKeyPreviewDown(gbContenedor_PreviewKeyDown, form);

            DatosAgregarCliente();
            cbCliente.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbTipoCliente.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbUsoCFDI.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);

            using (DataTable dtPublicoGeneral = cn.CargarDatos(cs.BuscarPublicaGeneral()))
            {
                if (!dtPublicoGeneral.Rows.Count.Equals(0))
                {
                    btnPublicoGeneral.Visible = false;
                }
            }
        }

        public void DatosAgregarCliente()
        {
            // El titulo que se mostrara al abrir el form
            if (tipo == 1)
            {
                this.Text = "PUDVE - Nuevo Cliente";
            }
            else if (tipo == 2)
            {
                this.Text = "PUDVE - Editar Cliente";
            }
            else if (tipo == 3)
            {
                this.Text = "PUDVE - Confirmar Cliente";
            }

            // ComboBox usos de CFDI
            Dictionary<string, string> usosCFDI = new Dictionary<string, string>();
            usosCFDI.Add("G01", "Adquisición de mercancias");
            usosCFDI.Add("G02", "Devoluciones, descuentos o bonificaciones");
            usosCFDI.Add("G03", "Gastos en general");
            usosCFDI.Add("I01", "Construcciones");
            usosCFDI.Add("I02", "Mobilario y equipo de oficina por inversiones");
            usosCFDI.Add("I03", "Equipo de transporte");
            usosCFDI.Add("I04", "Equipo de computo y accesorios");
            usosCFDI.Add("I05", "Dados, troqueles, moldes, matrices y herramental");
            usosCFDI.Add("I06", "Comunicaciones telefónica");
            usosCFDI.Add("I07", "Comunicaciones satelitale");
            usosCFDI.Add("I08", "Otra maquinaria y equipo");
            usosCFDI.Add("P01", "Por definir");

            cbUsoCFDI.DataSource = usosCFDI.ToArray();
            cbUsoCFDI.DisplayMember = "Value";
            cbUsoCFDI.ValueMember = "Key";

            // Combobox tipo de cliente
            var tipoClientes = mb.ObtenerTipoClientes(extra: true);

            cbTipoCliente.DataSource = tipoClientes.ToArray();
            cbTipoCliente.DisplayMember = "Value";
            cbTipoCliente.ValueMember = "Key";

            //ComboBox Formas de pago
            /*Dictionary<string, string> pagos = new Dictionary<string, string>();
            pagos.Add("01", "01 - Efectivo");
            pagos.Add("02", "02 - Cheque nominativo");
            pagos.Add("03", "03 - Transferencia electrónica de fondos");
            pagos.Add("04", "04 - Tarjeta de crédito");
            pagos.Add("05", "05 - Monedero electrónico");
            pagos.Add("06", "06 - Dinero electrónico");
            pagos.Add("08", "08 - Vales de despensa");
            pagos.Add("12", "12 - Dación en pago");
            pagos.Add("13", "13 - Pago por subrogación");
            pagos.Add("14", "14 - Pago por consignación");
            pagos.Add("15", "15 - Condonación");
            pagos.Add("17", "17 - Compensación");
            pagos.Add("23", "23 - Novación");
            pagos.Add("24", "24 - Confusión");
            pagos.Add("25", "25 - Remisión de deuda");
            pagos.Add("26", "26 - Prescripción o caducidad");
            pagos.Add("27", "27 - A satisfacción del acreedor");
            pagos.Add("28", "28 - Tarjeta de débito");
            pagos.Add("29", "29 - Tarjeta de servicios");
            pagos.Add("30", "30 - Aplicación de anticipos");
            pagos.Add("99", "99 - Por definir");

            cbFormaPago.DataSource = pagos.ToArray();
            cbFormaPago.DisplayMember = "Value";
            cbFormaPago.ValueMember = "Key";*/

            if (tipo == 1)
            {
                cbTipoCliente.SelectedIndex = 0;
            }

            // Si viene de la opcion editar o confirmar, buscamos los datos actuales del cliente
            if (tipo == 2 || tipo == 3)
            {
                var cliente = mb.ObtenerDatosCliente(idCliente, FormPrincipal.userID);

                CargarDatosCliente(cliente);

                //Deshabilitamos el checkbox para permitir agregar RFC repetido
                cbCliente.Enabled = false;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            valdarRFCExistente();

            if (validarRFC == true || cbCliente.Checked)
            {
                var razon = txtRazonSocial.Text;
                var comercial = txtNombreComercial.Text;
                var rfc = txtRFC.Text;
                var usoCFDI = cbUsoCFDI.SelectedValue;
                var pais = txtPais.Text;
                var estado = txtEstado.Text;
                var municipio = txtMunicipio.Text;
                var localidad = txtLocalidad.Text;
                var cp = txtCP.Text;
                var colonia = txtColonia.Text;
                var calle = txtCalle.Text;
                var noExt = txtNumExt.Text;
                var noInt = txtNumInt.Text;
                var regimen = string.Empty; //Esta vacio porque no se utiliza actualmente el campo de regimen
                var email = txtEmail.Text;
                var telefono = txtTelefono.Text;
                var tipoCliente = cbTipoCliente.SelectedValue.ToString();
                var numeroCliente = GenerarNumeroCliente();
                var formaPago = "01"; //cbFormaPago.SelectedValue;
                var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                if (tipo != 2)
                {
                    int idCliente = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} AND RFC = '{rfc}' ORDER BY FechaOperacion DESC LIMIT 1", 1));
                }
                var cantidadCamposRFC = rfc.Length;

                if (cantidadCamposRFC > 0 && cantidadCamposRFC < 12)
                {
                    txtRFC.Focus();
                    MessageBox.Show("El RFC no tiene el formato correcto", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRFC.ForeColor = Color.Red;
                   // txtRFC.Font = new Font(Label.DefaultFont, FontStyle.Bold);
                    return;
                }
                else
                {
                    txtRFC.ForeColor = Color.Black;
                    txtRFC.Font = new Font(Label.DefaultFont, FontStyle.Regular);

                }

                if (string.IsNullOrWhiteSpace(razon))
                {
                    MessageBox.Show("La razón social es obligatoria", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                /*if (string.IsNullOrWhiteSpace(rfc))
                {
                    MessageBox.Show("El RFC es un campo obligatorio", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                // Valida longitud y formato del RFC
                if (txtRFC.TextLength < 12)
                {
                    MessageBox.Show("La longitud del RFC es incorrecta.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    string formato_rfc = "^[A-Z&Ñ]{3,4}[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[A-Z0-9]{2}[0-9A]$";

                    Regex exp = new Regex(formato_rfc);

                    if (exp.IsMatch(txtRFC.Text))
                    {
                    }
                    else
                    {
                        MessageBox.Show("El formato del RFC no es valido.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;
                    }
                }*/


                string[] datos = new string[]
                {
                    FormPrincipal.userID.ToString(), razon, comercial, rfc, usoCFDI.ToString(), pais, estado, municipio, localidad,
                    cp, colonia, calle, noExt, noInt, regimen.ToString(), email, telefono, formaPago, fechaOperacion, idCliente.ToString(),
                    tipoCliente, numeroCliente, verificado
                };

                //Si el checkbox de agregar cliente repetido esta marcado
                //if (cbCliente.Checked)
                //{
                //    bool respuesta = (bool)cn.EjecutarSelect($"SELECT * FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} AND RFC = '{rfc}'");

                //    if (respuesta)
                //    {
                //        var mensaje = MessageBox.Show("Ya existe un cliente registrado con el mismo RFC.\n\n¿Desea actualizarlo con esta información?", "Mensaje del Sistema", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                //        if (mensaje == DialogResult.Yes)
                //        {
                //            //Si selecciona SI se hace una actualizacion con la informacion del formulario al usuario que tiene el mismo RFC
                //            int resultado = cn.EjecutarConsulta(cs.GuardarCliente(datos, 1));

                //            if (resultado > 0)
                //            {
                //                this.Close();
                //            }
                //        }
                //        else if (mensaje == DialogResult.No)
                //        {
                //            //Si selecciona NO se hace un nuevo registro

                //            //Insertar
                //            int resultado = cn.EjecutarConsulta(cs.GuardarCliente(datos));

                //            if (resultado > 0)
                //            {
                //                if (idVenta > 0)
                //                {
                //                    AsignarCliente(idVenta);
                //                }

                //                this.Close();
                //            }
                //        }
                //        else
                //        {
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        //Insertar
                //        int resultado = cn.EjecutarConsulta(cs.GuardarCliente(datos));

                //        if (resultado > 0)
                //        {
                //            if (idVenta > 0)
                //            {
                //                AsignarCliente(idVenta);
                //            }

                //            this.Close();
                //        }
                //    }
                //}
                //else
                //{
                    if (tipo == 1)
                    {
                        //Insertar
                        int resultado = cn.EjecutarConsulta(cs.GuardarCliente(datos));

                        if (resultado > 0)
                        {
                            if (idVenta > 0)
                            {
                                AsignarCliente(idVenta);
                            }

                            this.Close();
                        }
                    if (!string.IsNullOrEmpty(consultaReglas))
                    {

                        using (DataTable dtIDCliente = cn.CargarDatos($"SELECT MAX(ID) FROM Clientes"))
                        {
                            consultaReglas += $"{Int32.Parse(dtIDCliente.Rows[0]["MAX(ID)"].ToString())})";
                        }
                        cn.EjecutarConsulta(consultaReglas);
                    }
                    else
                    {
                        clientesReglasdeCredito reglasdeCredito = new clientesReglasdeCredito(1,matar:true);
                        reglasdeCredito.FormClosed += delegate
                        {
                            consultaReglas = reglasdeCredito.consulta;
                        };
                        reglasdeCredito.ShowDialog();
                        using (DataTable dtIDCliente = cn.CargarDatos($"SELECT MAX(ID) FROM Clientes"))
                        {
                            consultaReglas += $"{Int32.Parse(dtIDCliente.Rows[0]["MAX(ID)"].ToString())})";
                        }
                        cn.EjecutarConsulta(consultaReglas);
                    }
                    }
                    else
                    {
                        //Actualizar
                        int resultado = cn.EjecutarConsulta(cs.GuardarCliente(datos, 1));

                        if (resultado > 0)
                        {
                            this.Close();
                        }
                    }
                //}
            }
            else
            {
                MessageBox.Show("Esta RFC ya esta registrada con otro cliente", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void valdarRFCExistente()
        {
            var rfc = txtRFC.Text;
            if(Clientes.editarFor != 0)
            {
                using (var buscarRfc = cn.CargarDatos($"SELECT RFC FROM Clientes WHERE IDUsuario = '{FormPrincipal.userID}' AND RFC = '{rfc}'"))
                {
                    if (!buscarRfc.Rows.Count.Equals(0))
                    {
                        validarRFC = false;
                    }
                    else
                    {
                        validarRFC = true;
                    }
                }
            }
            else
            {
                validarRFC = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void CargarDatosCliente(string[] datos)
        {
            txtRazonSocial.Text = datos[0];
            txtNombreComercial.Text = datos[2];
            txtRFC.Text = datos[1];
            txtPais.Text = datos[4];
            txtEstado.Text = datos[5];
            txtMunicipio.Text = datos[6];
            txtLocalidad.Text = datos[7];
            txtCP.Text = datos[8];
            txtColonia.Text = datos[9];
            txtCalle.Text = datos[10];
            txtNumExt.Text = datos[11];
            txtNumInt.Text = datos[12];
            txtEmail.Text = datos[13];
            txtTelefono.Text = datos[14];
            cbUsoCFDI.SelectedValue = datos[3];
            cbTipoCliente.SelectedValue = Convert.ToInt32(datos[16]);
            if (datos[18].Equals("1"))
            {
                lblVerificar.Text = "Verificado ✓";
                lblVerificar.ForeColor = Color.Green;
                verificado = "1";
            }
            //cbFormaPago.SelectedValue = datos[15];
        }

        //Este método se utiliza cuando se quiere timbrar una factura pero no se tienen clientes registrados
        //Se abre el form de registrar nuevo cliente, al terminar el registro obtiene el ID de ese cliente
        //Busca sus datos en este caso la razon social y actualiza la tabla de detalles venta de la venta correspondiente
        private void AsignarCliente(int idVenta)
        {
            int idCliente = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} ORDER BY ID DESC LIMIT 1", 1));

            var tmp = mb.ObtenerDatosCliente(idCliente, FormPrincipal.userID);

            //Actualizar a la tabla detalle de venta
            var razonSocial = tmp[0];

            string[] datos = new string[] { idCliente.ToString(), razonSocial, idVenta.ToString(), FormPrincipal.userID.ToString() };

            cn.EjecutarConsulta(cs.GuardarDetallesVenta(datos, 1));
        }
        
        private void valida_longitud(object sender, EventArgs e)
        {
            //int tam = txtRFC.TextLength;

            //if (tam > 0 & (tam == 12 | tam == 13))
            //{
            //}
            //else
            //{
            //    MessageBox.Show("La longitud del RFC es incorrecta.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private string GenerarNumeroCliente()
        {
            var auxiliar = mb.ObtenerNumeroCliente();

            if (string.IsNullOrWhiteSpace(auxiliar))
            {
                auxiliar = "0";
            }
            else
            {
                auxiliar = auxiliar.TrimStart('0');
            }

            var numero = Convert.ToInt16(auxiliar) + 1;

            return numero.ToString("D6");
        }

        private void lAgregarClienteNuevo_Click(object sender, EventArgs e)
        {
            Clientes addTypeCliente = Application.OpenForms.OfType<Clientes>().FirstOrDefault();

            if (addTypeCliente != null)
            {
                addTypeCliente.btnTipoCliente_Click(this, null);

                //AgregarTipoCliente tCliente = new AgregarTipoCliente();

                //addTypeCliente.FormClosed += delegate
                //{
                //    addTypeCliente.ti
                //   // var tipoClientes = mb.ObtenerTipoClientes(extra: true);
                //    DatosAgregarCliente();
                //};
                
            }
        }

        private void cbTipoCliente_Click(object sender, EventArgs e)
        {

        }

        private void cbTipoCliente_Click_1(object sender, EventArgs e)
        {

            DatosAgregarCliente();
        }

        private void txtTelefono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void gbContenedor_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            
        }

        private void AgregarCliente_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtRazonSocial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtNombreComercial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtRFC_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtPais_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void cbCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtEstado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtMunicipio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtCP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtCalle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void cbTipoCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtLocalidad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtColonia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtNumExt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtNumInt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void cbUsoCFDI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.End)
            {
                btnAceptar.PerformClick();
            }
        }

        private void txtRFC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && !char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Enter)
            {
                MessageBox.Show("Solo se permiten letras y numeros", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
                return;
            }
        }

        private void AgregarCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close(); 
            }
        }

        private void txtRFC_TextChanged(object sender, EventArgs e)
        {
            var cantidadCamposRFC = txtRFC.Text.Length;


            if (cantidadCamposRFC > 11)
            {
                txtRFC.ForeColor = Color.Black;
                txtRFC.Font = new Font(Label.DefaultFont, FontStyle.Regular);
            }
            else
            {
                txtRFC.ForeColor = Color.Red;
                //txtRFC.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtRazonSocial.Text = "PUBLICO GENERAL";
            txtRFC.Text = "XAXX010101000";
        }

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if (txtTelefono.Text.Length>11)
            {
                e.Handled = false;
            }
        }

        private void label19_Click(object sender, EventArgs e)
        {
            clientesAltaHuella capturar = new clientesAltaHuella(idCliente);
            capturar.ShowDialog();
        }

        private void btnReglasdecredito_Click(object sender, EventArgs e)
        {
            if (tipo==1)
            {
                clientesReglasdeCredito reglasdeCredito = new clientesReglasdeCredito(operacion:1);

                reglasdeCredito.FormClosed += delegate
                {
                    consultaReglas = reglasdeCredito.consulta;
                };
                reglasdeCredito.ShowDialog();
            }
            else
            {
                clientesReglasdeCredito reglasdeCredito = new clientesReglasdeCredito(2, idCliente.ToString());
                reglasdeCredito.ShowDialog();
            }
            
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
            if (txtTelefono.Text.Length.Equals(10))
            {
                lblVerificar.Enabled = true;
                lblVerificar.ForeColor = Color.FromArgb(0, 0, 192);
            }
            else
            {
                lblVerificar.Enabled = false;
                lblVerificar.ForeColor = Color.Black;
            }
        }

        private void lblVerificar_Click(object sender, EventArgs e)
        {
            if (verificarFono())
            {
                lblVerificar.Text = "Verificado ✓";
                lblVerificar.ForeColor = Color.Green;
                verificado = "1";
            }
        }

        private bool verificarFono()
        {
            bool coincidencia = false;

            creditoVerificacion verificadorNum = new creditoVerificacion($"+{numRegion.Text}{txtTelefono.Text}");
            verificadorNum.FormClosed += delegate
            {
                coincidencia = verificadorNum.validado;
            };
            verificadorNum.ShowDialog();
            return coincidencia;
        }
    }
}
