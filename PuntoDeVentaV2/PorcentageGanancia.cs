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
            float porcentaje, resultado;
            bool porcent=false;
            porcent = float.TryParse(txtPorcentajeProducto.Text, out porcentaje);
            if (porcent)
            {
                resultado = (porcentaje / 100) + 1;
                

                if (!resultado.Equals("."))
                {
                    if (opcion3 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    //var porcentaje = txtPorcentajeProducto.Text.Trim();

                    //if (string.IsNullOrWhiteSpace(porcentaje))
                    //{
                    //    MessageBox.Show("Ingrese la cantidad de porcentaje", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    txtPorcentajeProducto.Focus();
                    //    return;
                    //}

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
            else
            {
                MessageBox.Show("Ingrese un porcentaje de valor numerico");
                txtPorcentajeProducto.Focus();
                txtPorcentajeProducto.SelectAll();
            }

            
            //validacionPunto = txtPorcentajeProducto.Text;

            //if (!validacionPunto.Equals("."))
            //{
            //    if (opcion3 == 0)
            //    {
            //        Utilidades.MensajePermiso();
            //        return;
            //    }

            //    var porcentaje = txtPorcentajeProducto.Text.Trim();

            //    if (string.IsNullOrWhiteSpace(porcentaje))
            //    {
            //        MessageBox.Show("Ingrese la cantidad de porcentaje", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtPorcentajeProducto.Focus();
            //        return;
            //    }

            //    var respuesta = cn.EjecutarConsulta($"UPDATE Configuracion SET PorcentajePrecio = {porcentaje} WHERE IDUsuario = {FormPrincipal.userID}");

            //    if (respuesta > 0)
            //    {
            //        MessageBox.Show("Información guardada", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //}
            //else
            //{

            //    MessageBox.Show("Por favor ingrese numeros", "¡Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    txtPorcentajeProducto.Focus();
            //}
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

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            //float porcentaje, precio, procedimiento1,procedmiento2, resultado;
            //bool porcen, costo = false;
            //costo = float.TryParse(txtPrecio.Text, out precio);
            //porcen = float.TryParse(txtPorcentaje.Text, out porcentaje);
            //if (costo)
            //{
            //    if (porcen)
            //    {
            //        procedimiento1 = porcentaje * precio;
            //        procedmiento2 = procedimiento1 / 100;
            //        resultado = procedmiento2 + precio;
            //        lblResultado.Text = resultado.ToString();
            //    }
            //    else
            //    {
            //        if (txtPorcentaje.Text == "")
            //        {
            //            MessageBox.Show("Ingrese el Porcentaje en el apartado de Porcentaje de Ganancia", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            txtPorcentaje.Focus();
            //            txtPorcentaje.SelectAll();
            //        }
            //        else
            //        {
            //            MessageBox.Show("Ingrese un Porcentaje con valores Numericos\nen el apartado de Porcentaje de Ganancia", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //            txtPorcentaje.Focus();
            //            txtPorcentaje.SelectAll();
            //        }
            //    }
            //}
            //else
            //{
            //    if (txtPrecio.Text=="")
            //    {
            //        MessageBox.Show("Ingrece el Precio en el apartado de Precio Compra", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //        txtPrecio.Focus();
            //        txtPrecio.SelectAll();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Ingrese valores Numericos en el apartado de Precio Compra", "ADVERTENCIA" ,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            //        txtPrecio.Focus();
            //        txtPrecio.SelectAll();
            //    }
            //}
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtPorcentaje.Text = "";
            txtPrecio.Text = "";
            lblResultado.Text = "";
        }

        private void txtPorcentajeProducto_TextChanged(object sender, EventArgs e)
        {

            decimal resultado, procedimiento;
            var porcent = string.Empty;
            porcent = txtPorcentajeProducto.Text;
            if (string.IsNullOrEmpty(porcent))
            {
                txtPorcentaje.Clear();
                lblResultado.Text = "";
            }
            else
            {
                if (txtPorcentajeProducto.Text==".")
                {
                    txtPorcentaje.Text = "";
                }
                else
                {
                    porcent = txtPorcentajeProducto.Text;
                    txtPorcentaje.Text = porcent.ToString();

                    procedimiento = (Convert.ToDecimal(porcent) * 100) / 100;
                    resultado = procedimiento + 100;
                    lblResultado.Text = resultado.ToString();
                }
                

            }

            
            
        }

        private void txtPorcentajeProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //solo permitir numeros enteros
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
