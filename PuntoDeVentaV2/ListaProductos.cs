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
        // variables para poder manejar las filas y poder hacer procesos
        int IdProd, numfila;

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        // cadena de texto para poder hacer el query en la base de datos
        string buscarStock;

        // variables las cuales se pasaran a la siguiente ventana

        // variable para ver si el usuario selecciono algun producto de la lista
        public int consultadoDesdeListProdFin { get; set; }
        // Alamacenamos el dato en la variable del dato de ID
        public string IdProdStrFin { get; set; }
        // Alamacenamos el dato en la variable del dato de Nombre(Descripcion)
        public string NombreProdStrFin { get; set; }
        // Alamacenamos el dato en la variable del dato de Stock
        public string StockProdStrFin { get; set; }
        // Alamacenamos el dato en la variable del dato de Precio
        public string PrecioDelProdStrFin { get; set; }
        // Alamacenamos el dato en la variable del dato de Categoria
        public string CategoriaProdStrFin { get; set; }
        // Alamacenamos el dato en la variable del dato de Clave Interna
        public string ClaveInternaProdStrFin { get; set; }
        // Alamacenamos el dato en la variable del dato de Codigo de Barras
        public string CodigoBarrasProdStrFin { get; set; }
        // Alamacenamos el dato en la variable cual seria el campo donde se guardaria
        public int opcionGuardarFin { get; set; }

        // Variable interna para poder hacer el manejo de los datos si selecciono algun producto
        public static int consultadoDesdeListProd;
        // Variable interna para poder saber que Id es del producto             
        public static string IdProdStr;
        // Variable interna para poder saber que Nombre(Descripcion)
        public static string NombreProdStr;
        // Variable interna para poder saber que Stock
        public static string StockProdStr;
        // Variable interna para poder saber que Precio
        public static string PrecioDelProdStr;
        // Variable interna para poder saber que Categoria
        public static string CategoriaProdStr;
        // Variable interna para poder saber que Clave Interna
        public static string ClaveInternaProdStr;
        //  Variable interna para poder saber que Codigo de Barras
        public static string CodigoBarrasProdStr;
        // Variable interna para poder saber que donde se va guardar el dato
        public static int opcionGuardar;

        public string TypeStock { get; set; }
        public static string typeStockFinal;

        // variable de text para poder dirigirnos a la carpeta principal para
        // poder jalar las imagenes o cualquier cosa que tengamos hay en ese directorio
        //public string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

        public delegate void pasarProducto(string nombProd_Paq_Serv, string id_Prod_Paq_Serv = "");
        public event pasarProducto nombreProducto;

        // metodo para poder cargar los datos al inicio
        public void CargarDataGridView()
        {
            typeStockFinal = TypeStock;

            if (typeStockFinal == "Productos")
            {
                this.Text = "Listado de Productos en Stock Existente";
                label2.Text = "Stock Existente";
                label1.Text = "Buscar Producto:";
                // el query que se usara en la base de datos
                //buscarStock = $"SELECT prod.ID AS 'ID', prod.Nombre AS 'Nombre', prod.Stock AS 'Stock', prod.Precio AS 'Precio', prod.Categoria AS 'Categoria', prod.ClaveInterna AS 'Clave Interna', prod.CodigoBarras AS 'Codigo de Barras' FROM Productos prod WHERE prod.IDUsuario = '{FormPrincipal.userID}' AND Tipo = 'P' AND Status = '1'";
            }
            else if (typeStockFinal == "Combos" || typeStockFinal == "Servicios")
            {
                this.Text = "Listado de Combos/Servicios Existentes";
                label2.Text = "Combos o Servicios Existentes";
                label1.Text = "Buscar Combos/Servicios:";
                // el query que se usara en la base de datos
                //buscarStock = $"SELECT prod.ID AS 'ID', prod.Nombre AS 'Nombre', prod.Stock AS 'Stock', prod.Precio AS 'Precio', prod.Categoria AS 'Categoria', prod.ClaveInterna AS 'Clave Interna', prod.CodigoBarras AS 'Codigo de Barras' FROM Productos prod WHERE prod.IDUsuario = '{FormPrincipal.userID}' AND (Tipo = 'S' OR Tipo = 'PQ') AND Status = '1'";
            }

            //DGVStockProductos.DataSource = cn.GetStockProd(buscarStock);        // se rellena el DGVStockProductos con el resultado de la consulta
            //DGVStockProductos.Columns["ID"].Visible = false;
        }

        // metodo para poder limpiar el DGVStockProductos
        /*public void LimpiarDGV()        
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
        }*/

        public ListaProductos()
        {
            InitializeComponent();
        }

        private void ListaProductos_Load(object sender, EventArgs e)
        {
            // Llamamos el metodo de limpiarDGV
            //LimpiarDGV();
            // Llamamos el metodo CargarDataGridView
            CargarDataGridView();
            // Llamamos el metodo consultadoDesdeListProd
            consultadoDesdeListProd = 0;
        }

        private void txtBoxSearchProd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                BuscarProductos();
            }
            else if (e.KeyCode == Keys.Down && !DGVStockProductos.Rows.Count.Equals(0))
            {
                DGVStockProductos.Focus();
            }
        }

        private void BuscarProductos()
        {
            var busqueda = txtBoxSearchProd.Text.Trim();

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                var coincidencias = mb.BusquedaCoincidenciasVentas(busqueda);

                if (coincidencias.Count > 0)
                {
                    DGVStockProductos.Rows.Clear();

                    foreach (var producto in coincidencias)
                    {
                        var datos = cn.BuscarProducto(producto.Key, FormPrincipal.userID);

                        var tipo = string.Empty;

                        if (datos[5] == "P") { tipo = "Productos"; }
                        if (datos[5] == "S") { tipo = "Servicios"; }
                        if (datos[5] == "PQ") { tipo = "Combos"; }

                        if (typeStockFinal == "Productos")
                        {
                            if (datos[5] == "P")
                            {
                                AgregarProducto(datos);
                            }
                        }

                        if (typeStockFinal == "Combos")
                        {
                            if (datos[5] == "S" || datos[5] == "PQ")
                            {
                                AgregarProducto(datos);
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show($"No se encontraron productos con {txtBoxSearchProd.Text}","Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void AgregarProducto(string[] datos)
        {
            if (datos.Length > 0)
            {
                int rowId = DGVStockProductos.Rows.Add();
                DataGridViewRow row = DGVStockProductos.Rows[rowId];

                var tipo = string.Empty;

                if (datos[5] == "P") { tipo = "PRODUCTO"; }
                if (datos[5] == "S") { tipo = "SERVICIO"; }
                if (datos[5] == "PQ") { tipo = "COMBO"; }

                row.Cells["ID"].Value = datos[0];
                row.Cells["Nombre"].Value = datos[1];
                row.Cells["Stock"].Value = Utilidades.RemoverCeroStock(datos[4]);
                row.Cells["Precio"].Value = float.Parse(datos[2]).ToString("N2");
                row.Cells["Categoria"].Value = tipo;
                row.Cells["ClaveInterna"].Value = datos[6];
                row.Cells["Codigo"].Value = datos[7];

                DGVStockProductos.Focus();
                DGVStockProductos.CurrentRow.Selected = true;
            }
        }

        private void DGVStockProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && DGVStockProductos.CurrentRow.Index == 0)
            {
                txtBoxSearchProd.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                DGVStockProductos_CellDoubleClick(this, new DataGridViewCellEventArgs(0, DGVStockProductos.CurrentRow.Index));
            }
        }

        /*private void txtBoxSearchProd_TextChanged(object sender, EventArgs e)
        {
            // Llamamos el metodo de limpiarDGV
            LimpiarDGV();
            // el query que se usara en la base de datos
            buscarStock = $"SELECT prod.ID AS 'ID', prod.Nombre AS 'Nombre', prod.Stock AS 'Stock', prod.Precio AS 'Precio', prod.Categoria AS 'Categoria', prod.ClaveInterna AS 'Clave Interna', prod.CodigoBarras AS 'Codigo de Barras' FROM Productos prod WHERE prod.IDUsuario = '{FormPrincipal.userID}' AND (prod.Nombre LIKE '%" + txtBoxSearchProd.Text + "%' AND prod.Tipo = 'P')";
            // se rellena el DGVStockProductos con el resultado de la consulta
            DGVStockProductos.DataSource = cn.GetStockProd(buscarStock);
            DGVStockProductos.Columns["ID"].Visible = false;
        }*/

        private void DGVStockProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // variable para poder saber que fila fue la seleccionada
            numfila = DGVStockProductos.CurrentRow.Index;
            // almacenamos en la variable IdProdStr del resultado de la consulta en DB
            IdProdStr = DGVStockProductos[0, numfila].Value.ToString();
            // almacenamos en la variable NombreProdStr del resultado de la consulta en DB
            NombreProdStr = DGVStockProductos[1, numfila].Value.ToString();
            // almacenamos en la variable StockProdStr del resultado de la consulta en DB
            StockProdStr = DGVStockProductos[2, numfila].Value.ToString();
            // almacenamos en la variable PrecioDelProdStr del resultado de la consulta en DB
            PrecioDelProdStr = DGVStockProductos[3, numfila].Value.ToString();
            // almacenamos en la variable CategoriaProdStr del resultado de la consulta en DB
            CategoriaProdStr = DGVStockProductos[4, numfila].Value.ToString();
            // almacenamos en la variable ClaveInternaProdStr del resultado de la consulta en DB
            ClaveInternaProdStr = DGVStockProductos[5, numfila].Value.ToString();
            // almacenamos en la variable CodigoBarrasProdStr del resultado de la consulta en DB
            CodigoBarrasProdStr = DGVStockProductos[6, numfila].Value.ToString();

            /************************************************************************
            *       verificamos en que campo va ir guardado la clave interna        *
            ************************************************************************/

            // en el caso los dos campos esten en blanco por default va ir en el de clave Interna
            if ((ClaveInternaProdStr == "") && (CodigoBarrasProdStr == ""))
            {
                // indicamos que el valor de la variable a donde va guardarse sera 1
                opcionGuardar = 1;
            }
            // en el caso que tenga en blanco el campo de ClaveInterna en blanco va ir en el de clave Interna
            else if ((ClaveInternaProdStr == "") && (CodigoBarrasProdStr != ""))
            {
                // indicamos que el valor de la variable a donde va guardarse sera 2
                opcionGuardar = 2;
            }
            // en el caso que tenga en blanco el campo de CodigoBarras en blanco va ir en el de codigo de barras
            else if ((ClaveInternaProdStr != "") && (CodigoBarrasProdStr == ""))
            {
                // indicamos que el valor de la variable a donde va guardarse sera 3
                opcionGuardar = 3;
            }
            // en el caso que los dos campos tengan contenido se asigna el siguiente valor
            else
            {
                // indicamos que el valor de la variable a donde va guardarse sera 4
                opcionGuardar = 4;
            }

            /****************************************************************
            *   registramos el valor de las viariables de arriba para       *
            *   poder hacerlas publicas hacia las demas formas              *
            ****************************************************************/
            IdProdStrFin = IdProdStr;                               // almacenamos el valor de IdProducto
            NombreProdStrFin = NombreProdStr;                       // almacenamos el valor de NombreProd
            StockProdStrFin = StockProdStr;                         // almacenamos el valor de StockProd
            PrecioDelProdStrFin = PrecioDelProdStr;                 // almacenamos el valor de PrecioDelProd
            CategoriaProdStrFin = CategoriaProdStr;                 // almacenamos el valor de CategoriaProd
            ClaveInternaProdStrFin = ClaveInternaProdStr;           // almacenamos el valor de ClaveInternaProd
            CodigoBarrasProdStrFin = CodigoBarrasProdStr;           // almacenamos el valor de CodigoBarrasProd
            consultadoDesdeListProd = 1;                            // almacenamos el valor si selecciono un producto
            consultadoDesdeListProdFin = consultadoDesdeListProd;   // almacenamos el valor de consultadoDesdeListProd
            opcionGuardarFin = opcionGuardar;                       // almacenamos el valor de opcionGuardar

            if (typeStockFinal == "Productos")
            {
                nombreProducto(NombreProdStrFin, IdProdStrFin);
            }
            else if (typeStockFinal == "Combos" || typeStockFinal == "Servicios")
            {
                nombreProducto(NombreProdStrFin, IdProdStrFin);
            }
            
            this.Close();
        }
    }
}
