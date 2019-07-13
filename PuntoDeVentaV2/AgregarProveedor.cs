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
    public partial class AgregarProveedor : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        //tipo 1 = agregar
        //tipo 2 = editar
        private int tipo = 0;
        private int idProveedor = 0;

        public AgregarProveedor(int tipo = 1, int idProveedor = 0)
        {
            InitializeComponent();

            this.tipo = tipo;
            this.idProveedor = idProveedor;
        }

        private void AgregarProveedor_Load(object sender, EventArgs e)
        {
            //El titulo que se mostrara al abrir el form
            if (tipo == 1)
            {
                this.Text = "PUDVE - Nuevo Proveedor";
                tituloSeccion.Text = "NUEVO PROVEEDOR";
            }
            else
            {
                this.Text = "PUDVE - Editar Proveedor";
                tituloSeccion.Text = "EDITAR PROVEEDOR";
            }

            //Si viene de la opcion editar buscamos los datos actuales del proveedor
            if (tipo == 2)
            {
                var proveedor = mb.ObtenerDatosProveedor(idProveedor, FormPrincipal.userID);

                CargarDatosProveedor(proveedor);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var nombre = txtNombre.Text;
            var rfc = txtRFC.Text;
            var calle = txtCalle.Text;
            var noExt = txtNoExterior.Text;
            var noInt = txtNoInterior.Text;
            var colonia = txtColonia.Text;
            var municipio = txtMunicipio.Text;
            var estado = txtEstado.Text;
            var cp = txtCodigoPostal.Text;
            var email = txtEmail.Text;
            var telefono = txtTelefono.Text;
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string[] datos = new string[] { FormPrincipal.userID.ToString(), nombre, rfc, calle, noExt, noInt, colonia, municipio, estado, cp, email, telefono, fechaOperacion, idProveedor.ToString() };

            var respuestaValidacion = ValidarFormulario(datos);

            if (respuestaValidacion != "")
            {
                MessageBox.Show(respuestaValidacion, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (tipo == 1)
            {
                //Insertar
                int respuesta = cn.EjecutarConsulta(cs.GuardarProveedor(datos));

                if (respuesta > 0)
                {
                    this.Dispose();
                }
            }
            else
            {
                //Actualizar
                int respuesta = cn.EjecutarConsulta(cs.GuardarProveedor(datos, 1));

                if (respuesta > 0)
                {
                    this.Dispose();
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string ValidarFormulario(string[] datos)
        {
            //Verificar si el RFC ya esta registrado
            if (VerificarRFC(datos[2]))
            {
                return "El RFC ya se encuentra registrado con otro proveedor";
            }

            return "";
        }

        private bool VerificarRFC(string rfc)
        {
            var respuesta = (bool)cn.EjecutarSelect($"SELECT * FROM Proveedores WHERE IDUsuario = {FormPrincipal.userID} AND RFC = '{rfc}'");

            return respuesta;
        }


        private void CargarDatosProveedor(string[] datos)
        {
            txtNombre.Text = datos[0];
            txtRFC.Text = datos[1];
            txtCalle.Text = datos[2];
            txtNoExterior.Text = datos[3];
            txtNoInterior.Text = datos[4];
            txtColonia.Text = datos[5];
            txtMunicipio.Text = datos[6];
            txtEstado.Text = datos[7];
            txtCodigoPostal.Text = datos[8];
            txtEmail.Text = datos[9];
            txtTelefono.Text = datos[10];
        }
    }
}
