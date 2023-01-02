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
    public partial class categoriaSubdetalle : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        public categoriaSubdetalle()
        {
            InitializeComponent();
           
        }

        private void categoriaSubdetalle_Load(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var datos = cn.CargarDatos($"SELECT stock FROM productos WHERE ID = '{Productos.idProductoAgregarSubdetalle}' AND IDUsuario = '{FormPrincipal.userID}' AND `Status` = 1");
            decimal stockActual = 0;

            if (!datos.Rows.Count.Equals(0))
            {
                stockActual = Convert.ToDecimal(datos.Rows[0]["stock"].ToString());
                var subDetalle = cn.EjecutarSelect($"SELECT subDetalle FROM subdetallesdeproducto WHERE IDProducto = '{Productos.idProductoAgregarSubdetalle}' AND IDUsuario = '{FormPrincipal.userID}' AND subDetalle = '{txtSubDetalle.Text}'");
            }
           

            if (true)
            {
                MessageBox.Show("Ya cuenta con un sub detalle con este concepto");
            }
            else
            {
                cn.EjecutarConsulta($"INSERT INTO subdetallesdeproducto (IDProducto, IDUsuario, Categoria, Subdetalle, Stock, TipoDato) VALUES ('{Productos.idProductoAgregarSubdetalle}', '{FormPrincipal.userID}', '{txtSubDetalle.Text}', '{"NA"}', '{stockActual}', '{cbTipoDeDatos.SelectedIndex}')");
            }

        }
    }
}
