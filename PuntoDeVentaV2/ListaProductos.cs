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
        int IdProd, numfila;        // variables para poder manejar las filas y poder hacer procesos

        Conexion cn = new Conexion();       // declaramos objeto para poder manejar los metodos de la clase conexion
        Consultas cs = new Consultas();     // declaramos objeto para poder manejar los metodos de la clase Consultas

        string buscarStock;         // cadena de texto para poder hacer el query en la base de datos

        // variables las cuales se pasaran a la siguiente ventana
        public int consultadoDesdeListProdFin { get; set; }         // varaible para ver si el usuario selecciono algun producto de la lista
        public string IdProdStrFin { get; set; }                    // Alamacenamos el dato en la variable del dato de ID
        public string NombreProdStrFin { get; set; }                // Alamacenamos el dato en la variable del dato de Nombre(Descripcion)
        public string StockProdStrFin { get; set; }                 // Alamacenamos el dato en la variable del dato de Stock
        public string PrecioDelProdStrFin { get; set; }             // Alamacenamos el dato en la variable del dato de Precio
        public string CategoriaProdStrFin { get; set; }             // Alamacenamos el dato en la variable del dato de Categoria
        public string ClaveInternaProdStrFin { get; set; }          // Alamacenamos el dato en la variable del dato de Clave Interna
        public string CodigoBarrasProdStrFin { get; set; }          // Alamacenamos el dato en la variable del dato de Codigo de Barras
        public int opcionGuardarFin { get; set; }                   // Alamacenamos el dato en la variable cual seria el campo donde se guardaria

        public static int consultadoDesdeListProd;                  // Variable interna para poder hacer el manejo de los datos si selecciono algun producto
        public static string IdProdStr;                             // Variable interna para poder saber que Id es del producto
        public static string NombreProdStr;                         // Variable interna para poder saber que Nombre(Descripcion)
        public static string StockProdStr;                          // Variable interna para poder saber que Stock
        public static string PrecioDelProdStr;                      // Variable interna para poder saber que Precio
        public static string CategoriaProdStr;                      // Variable interna para poder saber que Categoria
        public static string ClaveInternaProdStr;                   // Variable interna para poder saber que Clave Interna
        public static string CodigoBarrasProdStr;                   //  Variable interna para poder saber que Codigo de Barras
        public static int opcionGuardar;                            // Variable interna para poder saber que donde se va guardar el dato

        public string TypeStock { get; set; }
        public static string typeStockFinal;

        // variable de text para poder dirigirnos a la carpeta principal para
        // poder jalar las imagenes o cualquier cosa que tengamos hay en ese directorio
        //public string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

        public delegate void pasarProducto(string nombProd_Paq_Serv, string id_Prod_Paq_Serv = "");
        public event pasarProducto nombreProducto;

        public void CargarDataGridView()    // metodo para poder cargar los datos al inicio
        {
            typeStockFinal = TypeStock;
            if (typeStockFinal == "Productos")
            {
                this.Text = "Listado de Productos en Stock Existente";
                label2.Text = "Stock Existente";
                label1.Text = "Buscar Producto:";
                // el query que se usara en la base de datos
                buscarStock = $"SELECT prod.ID AS 'ID', prod.Nombre AS 'Nombre', prod.Stock AS 'Stock', prod.Precio AS 'Precio', prod.Categoria AS 'Categoria', prod.ClaveInterna AS 'Clave Interna', prod.CodigoBarras AS 'Codigo de Barras' FROM Productos prod WHERE prod.IDUsuario = '{FormPrincipal.userID}' AND Tipo = 'P' AND Status = '1'";
            }
            else if (typeStockFinal == "Combos" || typeStockFinal == "Servicios")
            {
                this.Text = "Listado de Combos/Servicios Existentes";
                label2.Text = "Combos o Servicios Existentes";
                label1.Text = "Buscar Combos/Servicios:";
                // el query que se usara en la base de datos
                buscarStock = $"SELECT prod.ID AS 'ID', prod.Nombre AS 'Nombre', prod.Stock AS 'Stock', prod.Precio AS 'Precio', prod.Categoria AS 'Categoria', prod.ClaveInterna AS 'Clave Interna', prod.CodigoBarras AS 'Codigo de Barras' FROM Productos prod WHERE prod.IDUsuario = '{FormPrincipal.userID}' AND (Tipo = 'S' OR Tipo = 'PQ') AND Status = '1'";
            }
            DGVStockProductos.DataSource = cn.GetStockProd(buscarStock);        // se rellena el DGVStockProductos con el resultado de la consulta
            DGVStockProductos.Columns["ID"].Visible = false;
        }

        public void LimpiarDGV()        // metodo para poder limpiar el DGVStockProductos
        {
            // limpiamos el DataGridView y 
            // lo dejamos sin registros
            if (DGVStockProductos.DataSource is DataTable)
            {
                ((DataTable)DGVStockProductos.DataSource).Rows.Clear();     // dejamos sin registros
                DGVStockProductos.Refresh();                                // refrescamos el DataGridView
            }
        }

        public ListaProductos()
        {
            InitializeComponent();
        }

        private void ListaProductos_Load(object sender, EventArgs e)
        {
            LimpiarDGV();       // Llamamos el metodo de limpiarDGV
            CargarDataGridView();       // Llamamos el metodo CargarDataGridView
            consultadoDesdeListProd = 0;        // Llamamos el metodo consultadoDesdeListProd
        }

        private void txtBoxSearchProd_TextChanged(object sender, EventArgs e)
        {
            LimpiarDGV();       // Llamamos el metodo de limpiarDGV
            // el query que se usara en la base de datos
            buscarStock = $"SELECT prod.ID AS 'ID', prod.Nombre AS 'Nombre', prod.Stock AS 'Stock', prod.Precio AS 'Precio', prod.Categoria AS 'Categoria', prod.ClaveInterna AS 'Clave Interna', prod.CodigoBarras AS 'Codigo de Barras' FROM Productos prod WHERE prod.IDUsuario = '{FormPrincipal.userID}' AND (prod.Nombre LIKE '%" + txtBoxSearchProd.Text + "%' AND prod.Tipo = 'P')";
            DGVStockProductos.DataSource = cn.GetStockProd(buscarStock);        // se rellena el DGVStockProductos con el resultado de la consulta
            DGVStockProductos.Columns["ID"].Visible = false;
        }
        
        private void DGVStockProductos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            numfila = DGVStockProductos.CurrentRow.Index;                           // variable para poder saber que fila fue la seleccionada
            IdProdStr = DGVStockProductos[0, numfila].Value.ToString();             // almacenamos en la variable IdProdStr del resultado de la consulta en DB
            NombreProdStr = DGVStockProductos[1, numfila].Value.ToString();         // almacenamos en la variable NombreProdStr del resultado de la consulta en DB
            StockProdStr = DGVStockProductos[2, numfila].Value.ToString();          // almacenamos en la variable StockProdStr del resultado de la consulta en DB
            PrecioDelProdStr = DGVStockProductos[3, numfila].Value.ToString();      // almacenamos en la variable PrecioDelProdStr del resultado de la consulta en DB
            CategoriaProdStr = DGVStockProductos[4, numfila].Value.ToString();      // almacenamos en la variable CategoriaProdStr del resultado de la consulta en DB
            ClaveInternaProdStr = DGVStockProductos[5, numfila].Value.ToString();   // almacenamos en la variable ClaveInternaProdStr del resultado de la consulta en DB
            CodigoBarrasProdStr = DGVStockProductos[6, numfila].Value.ToString();   // almacenamos en la variable CodigoBarrasProdStr del resultado de la consulta en DB

            /************************************************************************
            *       verificamos en que campo va ir guardado la clave interna        *
            ************************************************************************/
            if ((ClaveInternaProdStr == "") && (CodigoBarrasProdStr == ""))         // en el caso los dos campos esten en blanco por default va ir en el de clave Interna
            {
                opcionGuardar = 1;      // indicamos que el valor de la variable a donde va guardarse sera 1
            }
            else if ((ClaveInternaProdStr == "") && (CodigoBarrasProdStr != ""))    // en el caso que tenga en blanco el campo de ClaveInterna en blanco va ir en el de clave Interna
            {
                opcionGuardar = 2;      // indicamos que el valor de la variable a donde va guardarse sera 2
            }
            else if ((ClaveInternaProdStr != "") && (CodigoBarrasProdStr == ""))    // en el caso que tenga en blanco el campo de CodigoBarras en blanco va ir en el de codigo de barras
            {
                opcionGuardar = 3;      // indicamos que el valor de la variable a donde va guardarse sera 3
            }
            else                                                                    // en el caso que los dos campos tengan contenido se asigna el siguiente valor
            {
                opcionGuardar = 4;      // indicamos que el valor de la variable a donde va guardarse sera 4
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
            else if (typeStockFinal == "Paquetes" || typeStockFinal == "Servicios")
            {
                nombreProducto(NombreProdStrFin, IdProdStrFin);
            }
            
            this.Close();                                           // cerramos la ventana 
        }
    }
}
