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
    public partial class AgregarMensajeInventario : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        // 0 = insert
        // 1 = update
        private int operacion = 0;
        
        public int idProducto { get; set; }

        public AgregarMensajeInventario()
        {
            InitializeComponent();
        }

        private void AgregarMensajeInventario_Load(object sender, EventArgs e)
        {
            // Verificamos si ya tiene un mensaje asignado o no
            if (string.IsNullOrEmpty(mb.MensajeInventario(idProducto)))
            {
                operacion = 0;
                lbMensaje.Text = "Asignar Mensaje";
            }
            else
            {
                operacion = 1;
                lbMensaje.Text = "Actualizar Mensaje";
            }
        }

        private void AgregarMensajeInventario_Shown(object sender, EventArgs e)
        {
            txtMensaje.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var mensaje = txtMensaje.Text;

            if (string.IsNullOrWhiteSpace(mensaje))
            {
                MessageBox.Show("El campo de mensaje no puede estar vacío", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMensaje.Focus();
                return;
            }

            // Insert
            if (operacion == 0)
            {
                var consulta = "INSERT INTO MensajesInventario (IDUsuario, IDProducto, Mensaje, Activo)";
                    consulta += $"VALUES ('{FormPrincipal.userID}', '{idProducto}', '{mensaje}', 1)";

                cn.EjecutarConsulta(consulta);
            }

            // Update
            if (operacion == 1)
            {
                var consulta = $"UPDATE MensajesInventario SET Mensaje = '{mensaje}' WHERE IDUsuario = {FormPrincipal.userID} AND IDProducto = {idProducto}";

                cn.EjecutarConsulta(consulta);
            }

            Close();
        }
    }
}
