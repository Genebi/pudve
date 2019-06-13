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

        public AgregarProveedor()
        {
            InitializeComponent();
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

            string[] datos = new string[] { FormPrincipal.userID.ToString(), nombre, rfc, calle, noExt, noInt, colonia, municipio, estado, cp, email, telefono, fechaOperacion };

            int respuesta = cn.EjecutarConsulta(cs.GuardarProveedor(datos));

            if (respuesta > 0)
            {
                this.Dispose();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
