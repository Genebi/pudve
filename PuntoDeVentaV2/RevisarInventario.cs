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

namespace PuntoDeVentaV2
{
    public partial class RevisarInventario : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        // Variables iniciales
        string fechaInventario = string.Empty;
        string numeroRevision = string.Empty;

        int idProducto = 0;


        public RevisarInventario()
        {
            InitializeComponent();
        }

        private void RevisarInventario_Load(object sender, EventArgs e)
        {
            var datosInventario = mb.DatosRevisionInventario();

            // Si existe un registro en la tabla obtiene los datos de lo contrario hace un insert para
            // que exista la configuracion necesaria
            if (datosInventario.Length > 0)
            {
                if (string.IsNullOrEmpty(datosInventario[0]))
                {
                    cn.EjecutarConsulta($"UPDATE CodigoBarrasGenerado SET FechaInventario = '{DateTime.Now.ToString("yyyy-MM-dd")}' WHERE IDUsuario = {FormPrincipal.userID}");

                    datosInventario = mb.DatosRevisionInventario();
                }

                fechaInventario = datosInventario[0];
                numeroRevision = datosInventario[1];
            }
            else
            {
                cn.EjecutarConsulta($"INSERT INTO CodigoBarrasGenerado (IDUsuario, FechaInventario, NoRevision) VALUES ('{FormPrincipal.userID}', '{DateTime.Now.ToString("yyyy-MM-dd")}', '1')");

                datosInventario = mb.DatosRevisionInventario();
                fechaInventario = datosInventario[0];
                numeroRevision = datosInventario[1];
            }

            lblNoRevision.Text = numeroRevision;
        }

        private void buscarCodigoBarras()
        {
            if (txtBoxBuscarCodigoBarras.Text != string.Empty)
            {
                var codigo = txtBoxBuscarCodigoBarras.Text;

                // Verifica si el codigo existe en algun producto y si pertenece al usuario
                // Si existe se trae la informacion del producto
                var infoProducto = mb.BuscarCodigoInventario(codigo);

                if (infoProducto.Length > 0)
                {
                    lblNombreProducto.Text = infoProducto[0];

                    if (string.IsNullOrEmpty(infoProducto[3]))
                    {
                        lblCodigoDeBarras.Text = codigo;
                    }
                    else
                    {
                        lblCodigoDeBarras.Text = infoProducto[3];
                    }

                    lblPrecioProducto.Text = infoProducto[2];
                    txtCantidadStock.Text = infoProducto[1];
                    idProducto = Convert.ToInt32(infoProducto[5]);
                }
                else
                {
                    MessageBox.Show("Producto no encontrado / Deshabilitado", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBoxBuscarCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                buscarCodigoBarras();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBoxBuscarCodigoBarras.Text))
            {
                if (!string.IsNullOrWhiteSpace(txtCantidadStock.Text))
                {
                    // Comprobamos si el producto ya fue revisado
                    var existe = (bool)cn.EjecutarSelect($"SELECT * FROM RevisarInventario WHERE IDAlmacen = '{idProducto}' AND IDUsuario = {FormPrincipal.userID}");

                    if (existe)
                    {
                        MessageBox.Show("Existe");
                    }
                    else
                    {
                        var stockFisico = txtCantidadStock.Text;
                        var fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                        var info = cn.BuscarProducto(idProducto, FormPrincipal.userID);

                        var datos = new string[]
                        {
                            idProducto.ToString(), info[1], info[6], info[7], info[4], stockFisico, numeroRevision,
                            fecha, "0", "0"
                        };

                        cn.EjecutarConsulta(cs.GuardarRevisarInventario(datos));
                    }
                }
            }
        }

        private void txtCantidadStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Para obligar a que sólo se introduzcan números
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsControl(e.KeyChar))  // permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;   // el resto de teclas pulsadas se desactivan
                }
            }
        }

        private void txtCantidadStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtBoxBuscarCodigoBarras.Text))
                {
                    if (!string.IsNullOrWhiteSpace(txtCantidadStock.Text))
                    {
                        btnSiguiente.PerformClick();
                        txtBoxBuscarCodigoBarras.Text = string.Empty;
                        txtBoxBuscarCodigoBarras.Focus();
                    }
                }
            }
        }

        private void RevisarInventario_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
        }

        private void btnReducirStock_Click(object sender, EventArgs e)
        {
            var cantidad = 0;

            if (!string.IsNullOrWhiteSpace(txtCantidadStock.Text))
            {
                cantidad = Convert.ToInt32(txtCantidadStock.Text);
            }

            cantidad -= 1;

            if (cantidad < 0)
            {
                txtCantidadStock.Text = "0";
                txtCantidadStock.Focus();
                txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);

                MessageBox.Show("La cantidad de stock no puede ser menor a cero", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtCantidadStock.Text = cantidad.ToString();
            txtCantidadStock.Focus();
            txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
        }


        private void btnAumentarStock_Click(object sender, EventArgs e)
        {
            var cantidad = 0;

            if (!string.IsNullOrWhiteSpace(txtCantidadStock.Text))
            {
                cantidad = Convert.ToInt32(txtCantidadStock.Text);
            }

            cantidad += 1;

            txtCantidadStock.Text = cantidad.ToString();
            txtCantidadStock.Focus();
            txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
        }

        private void AumentarNumConteo()
        {

        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.InicioFinInventario = 2;
            Properties.Settings.Default.Save();                 // Guardamos los dos Datos de las variables del sistema
            this.Hide();
            this.Close();
        }
    }
}
