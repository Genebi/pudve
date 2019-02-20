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
    public partial class ListaProductos : Form
    {
        int IdProd, numfila;

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        string buscarStock;

        public int consultadoDesdeListProdFin { get; set; }
        public string IdProdStrFin { get; set; }
        public string NombreProdStrFin { get; set; }
        public string StockProdStrFin { get; set; }
        public string PrecioDelProdStrFin { get; set; }
        public string CategoriaProdStrFin { get; set; }
        public string ClaveInternaProdStrFin { get; set; }
        public string CodigoBarrasProdStrFin { get; set; }

        public static int consultadoDesdeListProd;
        public static string IdProdStr;
        public static string NombreProdStr;
        public static string StockProdStr;
        public static string PrecioDelProdStr;
        public static string CategoriaProdStr;
        public static string ClaveInternaProdStr;
        public static string CodigoBarrasProdStr;

        // variable de text para poder dirigirnos a la carpeta principal para
        // poder jalar las imagenes o cualquier cosa que tengamos hay en ese directorio
        public string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

        public void CargarDataGridView()
        {
            buscarStock = $"SELECT prod.ID, prod.Nombre, prod.Stock, prod.Precio, prod.Categoria, prod.ClaveInterna, prod.CodigoBarras FROM Productos prod WHERE prod.IDUsuario = '{FormPrincipal.userID}'";
            DGVStockProductos.DataSource = cn.GetStockProd(buscarStock);
        }

        public void LimpiarDGV()
        {
            // limpiamos el DataGridView y 
            // lo dejamos sin registros
            if (DGVStockProductos.DataSource is DataTable)
            {
                // dejamos sin registros
                ((DataTable)DGVStockProductos.DataSource).Rows.Clear();
                // refrescamos el DataGridView
                DGVStockProductos.Refresh();
            }
        }

        public ListaProductos()
        {
            InitializeComponent();
        }

        private void ListaProductos_Load(object sender, EventArgs e)
        {
            LimpiarDGV();
            CargarDataGridView();
            //// para agregar dinamicamente el boton en el DataGridView
            //DataGridViewButtonColumn btnClm = new DataGridViewButtonColumn();
            //btnClm.Name = "Seleccionar";

            //// agregamos el boton en la ultima columna
            //DGVStockProductos.Columns.Add(btnClm);
            consultadoDesdeListProd = 0;
        }

        private void txtBoxSearchProd_TextChanged(object sender, EventArgs e)
        {
            LimpiarDGV();
            buscarStock = $"SELECT prod.ID, prod.Nombre, prod.Stock, prod.Precio, prod.Categoria, prod.ClaveInterna, prod.CodigoBarras FROM Productos prod WHERE prod.IDUsuario = '{FormPrincipal.userID}' AND prod.Nombre LIKE '%" + txtBoxSearchProd.Text + "%' ";
            DGVStockProductos.DataSource = cn.GetStockProd(buscarStock);
        }

        private void DGVStockProductos_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //// nos aseguramos que el DataGridView tenga por lo menos una fila en sus registros
            //if (e.ColumnIndex >= 0 && this.DGVStockProductos.Columns[e.ColumnIndex].Name == "Seleccionar" && e.RowIndex >= 0)
            //{
            //    // aqui indicamos que repinte el DataGridView 
            //    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
            //    // aqui agregamos el boton en la columna llamada Entrar
            //    DataGridViewButtonCell celBoton = this.DGVStockProductos.Rows[e.RowIndex].Cells["Seleccionar"] as DataGridViewButtonCell;

            //    // aqui tomamos un archivo .ico y lo insertamos en el boton
            //    Icon icoAtomico = new Icon(rutaDirectorio + @"\icon\black\iconfinder_Import.ico");
            //    // aqui le configuramos los margenes 
            //    e.Graphics.DrawIcon(icoAtomico, e.CellBounds.Left + 3, e.CellBounds.Top + 3);

            //    // aqui se aplica los margenes en el icono del boton
            //    this.DGVStockProductos.Rows[e.RowIndex].Height = icoAtomico.Height + 8;
            //    this.DGVStockProductos.Columns[e.ColumnIndex].Width = icoAtomico.Width + 36;

            //    e.Handled = true;
            //}
        }

        private void DGVStockProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //ActualizarStock.IdUProd=
            numfila = DGVStockProductos.CurrentRow.Index;
            IdProdStr = DGVStockProductos[0, numfila].Value.ToString();
            NombreProdStr = DGVStockProductos[1, numfila].Value.ToString();
            StockProdStr = DGVStockProductos[2, numfila].Value.ToString();
            PrecioDelProdStr = DGVStockProductos[3, numfila].Value.ToString();
            CategoriaProdStr = DGVStockProductos[4, numfila].Value.ToString();
            ClaveInternaProdStr = DGVStockProductos[5, numfila].Value.ToString();
            CodigoBarrasProdStr = DGVStockProductos[6, numfila].Value.ToString();
            IdProdStrFin = IdProdStr;
            NombreProdStrFin = NombreProdStr;
            StockProdStrFin = StockProdStr;
            PrecioDelProdStrFin = PrecioDelProdStr;
            CategoriaProdStrFin = CategoriaProdStr;
            ClaveInternaProdStrFin = ClaveInternaProdStr;
            CodigoBarrasProdStrFin = CodigoBarrasProdStr;
            consultadoDesdeListProd = 1;
            consultadoDesdeListProdFin = consultadoDesdeListProd;
            this.Close();
        }
    }
}
