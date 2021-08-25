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
    public partial class PorcentageGanancia : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        int opcion3 = 1;
        public PorcentageGanancia() 
        {
            InitializeComponent();
        }

        private void btnGuardarPorcentaje_Click(object sender, EventArgs e)
        {
            var validacionPunto = string.Empty;
            validacionPunto = txtPorcentajeProducto.Text;

            if (!validacionPunto.Equals("."))
            {
                if (opcion3 == 0)
                {
                    Utilidades.MensajePermiso();
                    return;
                }

                var porcentaje = txtPorcentajeProducto.Text.Trim();

                if (string.IsNullOrWhiteSpace(porcentaje))
                {
                    MessageBox.Show("Ingrese la cantidad de porcentaje", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPorcentajeProducto.Focus();
                    return;
                }

                var respuesta = cn.EjecutarConsulta($"UPDATE Configuracion SET PorcentajePrecio = {porcentaje} WHERE IDUsuario = {FormPrincipal.userID}");

                if (respuesta > 0)
                {
                    MessageBox.Show("Información guardada", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {

                MessageBox.Show("Por favor ingrese numeros", "¡Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPorcentajeProducto.Focus();
            }
        }

        private void VerificarConfiguracion()
        {
            var existe = (bool)cn.EjecutarSelect($"SELECT * FROM Configuracion WHERE IDUsuario = {FormPrincipal.userID}");

            if (existe)
            {
                var datosConfig = mb.ComprobarConfiguracion();

                txtPorcentajeProducto.Text = datosConfig[8].ToString();
            }
            else
            {
                cn.EjecutarConsulta($"INSERT INTO Configuracion (IDUsuario) VALUES ('{FormPrincipal.userID}')");
            }
        }
        private void PorcentageGanancia_Load(object sender, EventArgs e)
        {
            VerificarConfiguracion();
        }
    }
}
