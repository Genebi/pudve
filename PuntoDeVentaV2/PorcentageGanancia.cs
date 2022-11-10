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

        private void PorcentageGanancia_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtPorcentajeProducto_TextChanged(object sender, EventArgs e)
        {
            validarSoloNumeros(sender, e);
            float porcentaje,procedimiento, resultado;
            bool porcent = false;
            porcent = float.TryParse(txtPorcentajeProducto.Text, out porcentaje);
            
            if (porcent)
            {
                lblPorcentaje.Text = porcentaje.ToString() + "%";
                if (txtPorcentajeProducto.Text == ".")
                {
                    lblPorcentaje.Text = "";
                    lblResultado.Text = "";
                }
                else
                {
                    procedimiento = (porcentaje * 100) / 100;
                    resultado = procedimiento +100;
                    lblResultado.Text = "$"+resultado.ToString();
                }

            }
            else
            {
                lblPorcentaje.Text = "";
                lblResultado.Text = "";
            }
                    

        }
        private void validarSoloNumeros(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            string texto = txt.Text;
            bool esNum = decimal.TryParse(texto, out decimal algo);
            if (esNum.Equals(false))
            {
                txt.Text = "";
            }
        }

        private void txtPorcentajeProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Solo numeros enteros
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // solo 1 punto decimal
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}
