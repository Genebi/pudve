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
    public partial class ListaProductos : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public void CargarDataGridView()
        {
            cn.CargarInformacion(cs.Productos(FormPrincipal.userID), DGVStockProductos);
        }

        public ListaProductos()
        {
            InitializeComponent();
        }

        private void ListaProductos_Load(object sender, EventArgs e)
        {
            CargarDataGridView();
        }

        private void txtBoxSearchProd_TextChanged(object sender, EventArgs e)
        {
            string buscarStock;
            buscarStock = $"SELECT prod.Nombre, prod.Stock, prod.Precio, prod.Categoria, prod.ClaveInterna, prod.CodigoBarras FROM Productos prod WHERE prod.IDUsuario = '{FormPrincipal.userID}' AND prod.Nombre LIKE '%" + txtBoxSearchProd.Text + "%' ";
            DGVStockProductos.DataSource = cn.GetStockProd(buscarStock);
        }
    }
}
