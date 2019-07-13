using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class AgregarCliente : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        //tipo 1 = agregar
        //tipo 2 = editar
        private int tipo = 0;
        private int idCliente = 0;

        public AgregarCliente(int tipo = 1, int idCliente = 0)
        {
            InitializeComponent();

            this.tipo = tipo;
            this.idCliente = idCliente;
        }

        private void AgregarCliente_Load(object sender, EventArgs e)
        {
            //El titulo que se mostrara al abrir el form
            if (tipo == 1)
            {
                this.Text = "PUDVE - Nuevo Cliente";
            }
            else
            {
                this.Text = "PUDVE - Editar Cliente";
            }

            //ComboBox usos de CFDI
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

            //ComboBox Formas de pago
            Dictionary<string, string> pagos = new Dictionary<string, string>();
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
            cbFormaPago.ValueMember = "Key";

            //Si viene de la opcion editar buscamos los datos actuales del cliente
            if (tipo == 2)
            {
                var cliente = mb.ObtenerDatosCliente(idCliente, FormPrincipal.userID);

                CargarDatosCliente(cliente);

                //Deshabilitamos el checkbox para permitir agregar RFC repetido
                cbCliente.Enabled = false;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
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
            var formaPago = cbFormaPago.SelectedValue;
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            int idCliente = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} AND RFC = '{rfc}' ORDER BY FechaOperacion DESC LIMIT 1", 1));

            if (string.IsNullOrWhiteSpace(razon))
            {
                MessageBox.Show("La razón social es obligatoria", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            if (string.IsNullOrWhiteSpace(rfc))
            {
                MessageBox.Show("El RFC es un campo obligatorio", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            string[] datos = new string[]
            {
                FormPrincipal.userID.ToString(), razon, comercial, rfc, usoCFDI.ToString(), pais, estado, municipio, localidad,
                cp, colonia, calle, noExt, noInt, regimen.ToString(), email, telefono, formaPago.ToString(), fechaOperacion, idCliente.ToString()
            };

            //Si el checkbox de agregar cliente repetido esta marcado
            if (cbCliente.Checked)
            {
                bool respuesta = (bool)cn.EjecutarSelect($"SELECT * FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} AND RFC = '{rfc}'");

                if (respuesta)
                {
                    var mensaje = MessageBox.Show("Ya existe un cliente registrado con el mismo RFC.\n\n¿Desea actualizarlo con esta información?", "Mensaje del Sistema", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (mensaje == DialogResult.Yes)
                    {
                        //Si selecciona SI se hace una actualizacion con la informacion del formulario al usuario que tiene el mismo RFC
                        int resultado = cn.EjecutarConsulta(cs.GuardarCliente(datos, 1));

                        if (resultado > 0)
                        {
                            this.Close();
                        }
                    }
                    else if (mensaje == DialogResult.No)
                    {
                        //Si selecciona NO se hace un nuevo registro
                        int resultado = cn.EjecutarConsulta(cs.GuardarCliente(datos));

                        if (resultado > 0)
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    int resultado = cn.EjecutarConsulta(cs.GuardarCliente(datos));

                    if (resultado > 0)
                    {
                        this.Close();
                    }
                }
            }
            else
            {
                if (tipo == 1)
                {
                    //Insertar
                    int resultado = cn.EjecutarConsulta(cs.GuardarCliente(datos));

                    if (resultado > 0)
                    {
                        this.Close();
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
            cbFormaPago.SelectedValue = datos[15];
        }
    }
}
