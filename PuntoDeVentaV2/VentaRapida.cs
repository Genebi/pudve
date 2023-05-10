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
    public partial class VentaRapida : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public VentaRapida()
        {
            InitializeComponent();
        }

        private void txtNombreProd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
            if (e.KeyData == Keys.Escape)
            {
                btnCancelar.PerformClick();
            }
        }

        private void txtPrecioVenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            calculadora(sender, e);
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

        private void calculadora(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            int calcu = 0;
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                calcu++;

                if (calcu == 1)
                {
                    calculadora calculadora = new calculadora();

                    calculadora.FormClosed += delegate
                    {
                        if (calculadora.seEnvia.Equals(true))
                        {
                            txt.Text = calculadora.lCalculadora.Text;
                        }
                        calcu = 0;
                    };
                    if (!calculadora.Visible)
                    {
                        calculadora.Show();
                    }
                    else
                    {
                        calculadora.Show();
                    }
                }
            }
        }

        private void txtPrecioVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
            if (e.KeyData == Keys.Escape)
            {
                btnCancelar.PerformClick();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNombreProd_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNombreProd_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 33 && e.KeyChar <= 47) || (e.KeyChar >= 58 && e.KeyChar <= 64) || (e.KeyChar >= 91 && e.KeyChar <= 96) || (e.KeyChar >= 123))
            {
                MessageBox.Show("Solo se permiten numero y letras","Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Handled = true;
                return;
            }
        }
         
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombreProd.Text) || string.IsNullOrWhiteSpace(txtPrecioVenta.Text))
            {
                MessageBox.Show("Favor de rellenar ambos campos", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Convert.ToDecimal(txtPrecioVenta.Text)<= 0)
            {
                MessageBox.Show("El precio debe ser mayor a 0", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPrecioVenta.SelectAll();
                txtPrecioVenta.Focus();
                return;
            }
            Random myObject = new Random();
            int ranNum = myObject.Next(0, 999999999);
            string nuevoCodBar = ("VR" + ranNum);
            var datos = cn.CargarDatos($"SELECT CodigoBarras FROM productos WHERE IDUsuario = '{FormPrincipal.userID}' AND `Status` = '1' AND CodigoBarras = '{nuevoCodBar}'");
            if (!datos.Rows.Count.Equals(0))
            {
                var codBarras = datos.Rows[0]["CodigoBarras"].ToString();
                while (nuevoCodBar == codBarras)
                {
                    var datos2 = cn.CargarDatos($"SELECT CodigoBarras FROM productos WHERE IDUsuario = '{FormPrincipal.userID}' AND `Status` = '1' AND CodigoBarras = '{nuevoCodBar}'");
                    codBarras = datos2.Rows[0]["CodigoBarras"].ToString();
                    ranNum = myObject.Next(0, 999999999);
                    nuevoCodBar = ("VR" + ranNum);
                }
            }
            cn.EjecutarConsulta($"INSERT INTO productos (Nombre, Stock, Precio, Categoria, ClaveInterna, CodigoBarras, ClaveProducto, UnidadMedida, TipoDescuento, IDUsuario, ProdImage, Tipo, Base, IVA, Impuesto, NombreAlterno1, NombreAlterno2, StockNecesario, StockMinimo, PrecioCompra, PrecioMayoreo) VALUES ('{txtNombreProd.Text}', 900000, {txtPrecioVenta.Text}, 'PVRNUEVO', '', '{nuevoCodBar}', '', '', 0, {FormPrincipal.userID}, '', 'VR', 0, 0, '', '{txtNombreProd.Text}', '', 1, 2, 0.00, 0)");

            Ventas.codBarProdVentaRapida = nuevoCodBar;
            this.Close();
        }

        private void VentaRapida_Load(object sender, EventArgs e)
        {

        }
    }
}
