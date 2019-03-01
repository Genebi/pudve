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
    public partial class RecordViewProduct : Form
    {
        Conexion cn = new Conexion();

        DataTable dtRecordProducto, dt;

        string queryRecord, buscar, Id_Prod_select;

        int index = 0;

        public string nombreProd { get; set; }
        public string stockProd { get; set; }
        public string precioProd { get; set; }
        public string claveInternaProd { get; set; }
        public string codigoBarrasProd { get; set; }
        public string idUsuarioProd { get; set; }

        public static string Nombre;
        public static string Stock;
        public static string Precio;
        public static string ClaveInterna;
        public static string CodigoBarras;
        public static string IdUsuario;

        private void DGVProductRecord_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblFolioCompra.Text = DGVProductRecord[0, e.RowIndex].Value.ToString();
            lblRFCProveedor.Text = DGVProductRecord[1, e.RowIndex].Value.ToString();
            lblNombreProveedor.Text = DGVProductRecord[2, e.RowIndex].Value.ToString();
            lblClaveProducto.Text = DGVProductRecord[3, e.RowIndex].Value.ToString();
            lblFechaCompra.Text = DGVProductRecord[4, e.RowIndex].Value.ToString();
            lblFechaCompletaCompra.Text = DGVProductRecord[5, e.RowIndex].Value.ToString();
            lblCantidadCompra.Text = DGVProductRecord[6, e.RowIndex].Value.ToString();
            lblPrecioCompra.Text = DGVProductRecord[7, e.RowIndex].Value.ToString();
        }

        public void cargarDatos()
        {
            Nombre = nombreProd;
            Stock = stockProd;
            Precio = precioProd;
            ClaveInterna = claveInternaProd;
            CodigoBarras = codigoBarrasProd;
            IdUsuario = idUsuarioProd;

            lblNombre.Text = Nombre;
            lblStock.Text = Stock;
            lblPrecio.Text = Precio;
            lblClaveInterna.Text = ClaveInterna;
            lblCodigoBarras.Text = CodigoBarras;

            // Preparamos el Query que haremos segun la fila seleccionada
            buscar = $"SELECT * FROM Productos WHERE Nombre = '{Nombre}' AND Precio = '{Precio}' AND Stock = '{Stock}' AND ClaveInterna = '{ClaveInterna}' AND CodigoBarras = '{CodigoBarras}' AND IDUsuario = '{IdUsuario}'";
            dt = cn.CargarDatos(buscar);    // almacenamos el resultado de la Funcion CargarDatos que esta en la calse Consultas
            Id_Prod_select = dt.Rows[index]["ID"].ToString();       // almacenamos el Id del producto

            queryRecord = $"SELECT hcompras.Folio AS 'Folio de compra', hcompras.RFCEmisor AS 'RFC Proveedor', hcompras.NomEmisor AS 'Nombre Proveedor', hcompras.ClaveProdEmisor AS 'Clave_Producto Proveedor', hcompras.FechaCorta AS 'Fecha de Compra', hcompras.FechaLarga AS 'Fecha Completa de Compra', hcompras.Cantidad AS 'Cantidad de compra', hcompras.Precio AS 'Precio de compra' FROM HistorialCompras hcompras WHERE hcompras.IDUsuario = '{IdUsuario}' AND hcompras.IDProducto = '{Id_Prod_select}' ORDER BY FechaCorta DESC";
            dtRecordProducto = cn.CargarDatos(queryRecord);
            DGVProductRecord.DataSource = dtRecordProducto;
        }

        public RecordViewProduct()
        {
            InitializeComponent();
        }

        private void RecordViewProduct_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }
    }
}
