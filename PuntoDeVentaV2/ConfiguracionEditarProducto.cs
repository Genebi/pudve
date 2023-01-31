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
    public partial class ConfiguracionEditarProducto : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public ConfiguracionEditarProducto()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ConfiguracionEditarProducto_Load(object sender, EventArgs e)
        {
            var dato = cn.CargarDatos($"SELECT FormatoDeVenta FROM productos WHERE CodigoBarras = '{AgregarEditarProducto.ProdCodBarrasFinal}' AND IDUSuario = '{FormPrincipal.userID}' AND Status = '1'");
            var estado = dato.Rows[0]["FormatoDeVenta"].ToString();

            if (estado.Equals("2"))
            {
                rbPeroAutomatico.Checked = true;
            }
            else
            {
                rbPeroAutomatico.Checked = false;
            }

            if (estado.Equals("1"))
            {
                rbSoloPorEnteros.Checked = true;
            }
            else
            {
                rbSoloPorEnteros.Checked = false;
            }

            if (estado.Equals("0"))
            {
                rbVenderNormalmente.Checked = true;
            }
            else
            {
                rbVenderNormalmente.Checked = false;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (rbPeroAutomatico.Checked == true)
            {
                cn.EjecutarConsulta($"UPDATE productos SET FormatoDeVenta = '2' WHERE CodigoBarras = '{AgregarEditarProducto.ProdCodBarrasFinal}' AND IDUsuario = {FormPrincipal.userID} AND Status = '1'");
            }
            else if (rbSoloPorEnteros.Checked == true)
            {
                cn.EjecutarConsulta($"UPDATE productos SET FormatoDeVenta = '1' WHERE CodigoBarras = '{AgregarEditarProducto.ProdCodBarrasFinal}' AND IDUsuario = {FormPrincipal.userID} AND Status = '1'");
            }
            else
            {
                cn.EjecutarConsulta($"UPDATE productos SET FormatoDeVenta = '0' WHERE CodigoBarras = '{AgregarEditarProducto.ProdCodBarrasFinal}' AND IDUsuario = {FormPrincipal.userID} AND Status = '1'");
            }

            MessageBox.Show("Guardado con Extito", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
