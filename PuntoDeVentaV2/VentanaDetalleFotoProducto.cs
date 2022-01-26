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
    public partial class VentanaDetalleFotoProducto : Form
    {
        Conexion cn = new Conexion();

        DataTable Producto;

        string queryProducto;

        public int IDProducto { get; set; }

        public static int IDProductoFinal;

        public VentanaDetalleFotoProducto()
        {
            InitializeComponent();
        }

        private void VentanaDetalleFotoProducto_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            IDProductoFinal = IDProducto;
            queryProducto = $"SELECT P.Nombre, P.Stock, P.Precio, P.Categoria, P.ClaveInterna AS 'Clave Interna', P.CodigoBarras AS 'Codigo de Barras' FROM Productos P WHERE P.IDUsuario = '{FormPrincipal.userID}' AND P.ID = '{IDProductoFinal}'";
            Producto = cn.CargarDatos(queryProducto);
            DataRow row = Producto.Rows[0];
            lblNombre.Text = row["Nombre"].ToString();
            lblPrecio.Text = Convert.ToDecimal(row["Precio"]).ToString("N2");
            lblStock.Text = row["Stock"].ToString();
            lblCategoria.Text = row["Categoria"].ToString();
            lblClaveInterna.Text = row["Clave Interna"].ToString();
            lblCodigoBarras.Text = row["Codigo de Barras"].ToString();
        }

        private void VentanaDetalleFotoProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }
    }
}
