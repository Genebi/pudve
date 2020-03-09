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
    public partial class AgregarTipoCliente : Form
    {
        Conexion cn = new Conexion();

        public AgregarTipoCliente()
        {
            InitializeComponent();
        }

        private void AgregarTipoCliente_Load(object sender, EventArgs e)
        {
            txtDescuento.KeyPress += new KeyPressEventHandler(SoloDecimales);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var nombre = txtNombre.Text.Trim();
            var descuento = txtDescuento.Text.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                MessageBox.Show("El nombre para tipo de cliente es obligatorio", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(descuento))
            {
                MessageBox.Show("Ingrese la cantidad de porcentaje para descuento", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDescuento.Focus();
                return;
            }

            var consulta = $"INSERT INTO TipoClientes (IDUsuario, Nombre, DescuentoPorcentaje) VALUES ('{FormPrincipal.userID}', '{nombre}', '{descuento}')";

            var resultado = cn.EjecutarConsulta(consulta);

            if (resultado > 0)
            {
                Close();
            }
            else
            {
                MessageBox.Show("Ha ocurrido un error al registrar el tipo de cliente", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SoloDecimales(object sender, KeyPressEventArgs e)
        {
            //permite 0-9, eliminar y decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            //verifica que solo un decimal este permitido
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                {
                    e.Handled = true;
                }
            }
        }
    }
}
