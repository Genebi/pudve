using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class ListaProductos : Form
    {
        // variables para poder manejar las filas y poder hacer procesos
        int IdProd, numfila;

        public int idProdEdit;

        string prueba;
        string pruebados;

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

        // Variable para identificar si el llamado se hace desde la ventana de: AgregarStockXML o desde otra.
        public bool agregarstockxml = false;

        // variable de text para poder dirigirnos a la carpeta principal para
        // poder jalar las imagenes o cualquier cosa que tengamos hay en ese directorio
        //public string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

        public delegate void pasarProducto(string nombProd_Paq_Serv, string id_Prod_Paq_Serv = "");
        public event pasarProducto nombreProducto;

        // Estanciar objeto de Clase Paginar
        // para usarlo
        private Paginar p;

        //variable para saber de donde fue llamada la ventana
        public int DatosSourceFinal = 0;

        public List<string> listaServCombo = new List<string>();
        public List<string> listaProd = new List<string>();

        #region Sección de variables globales
        // Variables de tipo String
        string filtroConSinFiltroAvanzado = string.Empty;
        string DataMemberDGV = "Productos";
        string busqueda = string.Empty;

        // Variables de tipo Int
        int maximo_x_pagina = 18;
        int clickBoton = 0;
        #endregion

        private IntPtr hWin32Resources = IntPtr.Zero;

        [DllImport("kernel32.dll")]
        public static extern IntPtr LoadLibraryEx(string fileName, IntPtr hFile, long dwFlags);

        string mensajeMessageBox = "El producto que usted desea vincular ya se encuentra relacionado\n\nPodras vizualizar tus productos relacionados en el siguiente\nbotón que se encuentra en la ventana principal de Agregar o\nEditar Productos (Igual al que se muestra en esté mensaje\n\"Botón del ojito con la flecha\" del lado Izquierdo)";
        string tituloMessageBox = "Aviso del sistema";

        string mensajeParaMostrar = string.Empty;

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
            filtroLoadProductos();
            txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
            linkLblUltimaPagina.Text = p.countPag().ToString();
            actualizar();
            CargarDatos();
            // Llamamos el metodo de limpiarDGV
            //LimpiarDGV();
            // Llamamos el metodo CargarDataGridView
            //CargarDataGridView();
            // Llamamos el metodo consultadoDesdeListProd
            consultadoDesdeListProd = 0;

            if (listaProd.Count>0)
            {
                foreach (var item in listaProd)
                {
                    var words = item.Split('|');
                    var idpdroducto = words[2].ToString();
                    if (!string.IsNullOrWhiteSpace(idpdroducto))
                    {
                        foreach (DataGridViewRow DRListado in DGVStockProductos.Rows)
                        {
                            string strFila = DRListado.Cells[0].RowIndex.ToString();
                            var idproddgv = DRListado.Cells["ID"].Value.ToString();
                            if (idproddgv.Equals(idpdroducto))
                            {
                                DGVStockProductos.Rows[Convert.ToInt32(strFila)].DefaultCellStyle.BackColor = Color.Gray;
                            }
                        }
                        
                        //mensajeDeRelacionConImagenParaElUsuario(mensajeMessageBox, tituloMessageBox);
                    }
                }
            }
        }

        private void txtBoxSearchProd_KeyDown(object sender, KeyEventArgs e)
        {
            string busqueda = txtBoxSearchProd.Text;

            if (e.KeyData == Keys.Enter)
            {
                //BuscarProductos();
                CargarDatos(1, busqueda);
            }
            else if (e.KeyCode == Keys.Down && !DGVStockProductos.Rows.Count.Equals(0))
            {
                DGVStockProductos.Focus();
            }
            else if (DGVStockProductos.Rows.Count >= 1 && busqueda == "")
            {
                CargarDatos(1, busqueda);
            }

            if (DGVStockProductos.Rows.Count.Equals(0))
            {
                MessageBox.Show("La busqueda realizada no obtuvo resultados.", "Aviso del sistema.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CargarDatos();
                txtBoxSearchProd.Focus();
                txtBoxSearchProd.SelectAll();
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
                    MessageBox.Show($"No se encontraron productos con {txtBoxSearchProd.Text}", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        #region Sección de operaciones de paginador
        public void filtroLoadProductos()
        {
            string extra = string.Empty;
            string typeToSearch = string.Empty;

            typeStockFinal = TypeStock;

            if (typeStockFinal.Equals("Productos"))
            {
                typeToSearch = " P.Tipo = 'P' ";
            }
            else if (typeStockFinal.Equals("Combos"))
            {
                typeToSearch = " P.Tipo = 'PQ' OR P.Tipo = 'S' ";
            }
            else if (typeStockFinal.Equals("Servicios"))
            {
                typeToSearch = " P.Tipo = 'S' OR P.Tipo = 'PQ' ";
            }

            filtroConSinFiltroAvanzado = cs.searchProductList(typeToSearch, busqueda);

            p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);

            DGVStockProductos.Rows.Clear();
        }

        private void actualizar()
        {
            int BeforePage = 0, AfterPage = 0, LastPage = 0;

            linkLblPaginaAnterior.Visible = false;
            linkLblPaginaSiguiente.Visible = false;

            lblCantidadRegistros.Text = p.countRow().ToString();

            linkLblPaginaActual.Text = p.numPag().ToString();
            linkLblPaginaActual.LinkColor = System.Drawing.Color.White;
            linkLblPaginaActual.BackColor = System.Drawing.Color.Black;

            BeforePage = p.numPag() - 1;
            AfterPage = p.numPag() + 1;
            LastPage = p.countPag();

            if (Convert.ToInt32(linkLblPaginaActual.Text) >= 2)
            {
                linkLblPaginaAnterior.Text = BeforePage.ToString();
                linkLblPaginaAnterior.Visible = true;
                if (AfterPage <= LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = true;
                }
                else if (AfterPage > LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = false;
                }
            }
            else if (BeforePage < 1)
            {
                linkLblPrimeraPagina.Visible = false;
                linkLblPaginaAnterior.Visible = false;
                if (AfterPage <= LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = true;
                }
                else if (AfterPage > LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = false;
                    linkLblUltimaPagina.Visible = false;
                }
            }

            txtMaximoPorPagina.Text = p.limitRow().ToString();
        }

        private void btnActualizarMaximoProductos_Click(object sender, EventArgs e)
        {
            var cantidadAMostrar = Convert.ToInt32(txtMaximoPorPagina.Text);

            if (cantidadAMostrar <= 0)
            {
                mensajeParaMostrar = "Catidad a mostrar debe ser mayor a 0";
                Utilidades.MensajeCuandoSeaCeroEnElListado(mensajeParaMostrar);
                txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                return;
            }

            maximo_x_pagina = cantidadAMostrar;
            p.actualizarTope(maximo_x_pagina);
            CargarDatos();
            actualizar();

            //maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
            //p.actualizarTope(maximo_x_pagina);
            //CargarDatos();
            //actualizar();
        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void linkLblPaginaAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void linkLblPaginaActual_Click(object sender, EventArgs e)
        {
            actualizar();
        }

        private void linkLblPaginaSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void btnUltimaPagina_Click(object sender, EventArgs e)
        {
            p.ultimaPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void txtMaximoPorPagina_Click(object sender, EventArgs e)
        {
            maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
            p.actualizarTope(maximo_x_pagina);
            CargarDatos();
            actualizar();
        }

        private void txtMaximoPorPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void txtMaximoPorPagina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var cantidadAMostrar = Convert.ToInt32(txtMaximoPorPagina.Text);

                if (cantidadAMostrar <= 0)
                {
                    mensajeParaMostrar = "Catidad a mostrar debe ser mayor a 0";
                    Utilidades.MensajeCuandoSeaCeroEnElListado(mensajeParaMostrar);
                    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                    return;
                }

                maximo_x_pagina = cantidadAMostrar;
                p.actualizarTope(maximo_x_pagina);
                CargarDatos();
                actualizar();

                //maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
                //p.actualizarTope(maximo_x_pagina);
                //CargarDatos();
                //actualizar();
            }
        }
        #endregion

        #region Sección de cargar datos para el DataGridView
        /// <summary>
        /// Metodo CargarDatos
        /// </summary>
        /// <param name="status">El estatus del Producto: 1 = Activo, 0 = Inactivo, 2 = Tdodos</param>
        /// <param name="busquedaEnProductos">Cadena de texto que introduce el Usuario para coincidencias</param>
        public void CargarDatos(int status = 1, string busquedaEnProductos = "")
        {
            busqueda = string.Empty;

            busqueda = busquedaEnProductos;

            if (DGVStockProductos.RowCount <= 0)
            {
                if (busqueda == "")
                {
                    //DGVStockProductos.Rows.Clear();
                    string typeToSearch = string.Empty;

                    typeStockFinal = TypeStock;

                    if (typeStockFinal.Equals("Productos"))
                    {
                        typeToSearch = " P.Tipo = 'P' ";
                    }
                    else if (typeStockFinal.Equals("Combos"))
                    {
                        typeToSearch = " P.Tipo = 'PQ' OR P.Tipo = 'S' ";
                    }
                    else if (typeStockFinal.Equals("Servicios"))
                    {
                        typeToSearch = " P.Tipo = 'S' OR P.Tipo = 'PQ' ";
                    }

                    filtroConSinFiltroAvanzado = cs.searchProductList(typeToSearch, busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
                else if (busqueda != "")
                {
                    string typeToSearch = string.Empty;

                    if (typeStockFinal.Equals("Productos"))
                    {
                        typeToSearch = " P.Tipo = 'P' ";
                    }
                    else if (typeStockFinal.Equals("Combos"))
                    {
                        typeToSearch = " P.Tipo = 'PQ' OR P.Tipo = 'S' ";
                    }
                    else if (typeStockFinal.Equals("Servicios"))
                    {
                        typeToSearch = " P.Tipo = 'S' OR P.Tipo = 'PQ' ";
                    }

                    filtroConSinFiltroAvanzado = cs.searchProductList(typeToSearch, busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
            }
            else if (DGVStockProductos.RowCount >= 1 && clickBoton == 0)
            {
                if (busqueda == "")
                {
                    //DGVStockProductos.Rows.Clear();
                    string typeToSearch = string.Empty;

                    typeStockFinal = TypeStock;

                    if (typeStockFinal.Equals("Productos"))
                    {
                        typeToSearch = " P.Tipo = 'P' ";
                    }
                    else if (typeStockFinal.Equals("Combos"))
                    {
                        typeToSearch = " P.Tipo = 'PQ' OR P.Tipo = 'S' ";
                    }
                    else if (typeStockFinal.Equals("Servicios"))
                    {
                        typeToSearch = " P.Tipo = 'S' OR P.Tipo = 'PQ' ";
                    }

                    filtroConSinFiltroAvanzado = cs.searchProductList(typeToSearch, busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
                else if (busqueda != "")
                {
                    string typeToSearch = string.Empty;

                    if (typeStockFinal.Equals("Productos"))
                    {
                        typeToSearch = " P.Tipo = 'P' ";
                    }
                    else if (typeStockFinal.Equals("Combos"))
                    {
                        typeToSearch = " P.Tipo = 'PQ' OR P.Tipo = 'S' ";
                    }
                    else if (typeStockFinal.Equals("Servicios"))
                    {
                        typeToSearch = " P.Tipo = 'S' OR P.Tipo = 'PQ' ";
                    }

                    filtroConSinFiltroAvanzado = cs.searchProductList(typeToSearch, busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
            }

            DataSet datos = p.cargar();
            DataTable dtDatos = datos.Tables[0];

            DGVStockProductos.Rows.Clear();

            foreach (DataRow filaDatos in dtDatos.Rows)
            {
                var number_of_rows = DGVStockProductos.Rows.Add();
                DataGridViewRow row = DGVStockProductos.Rows[number_of_rows];

                // Columna IdProducto 
                string idProducto = filaDatos["ID"].ToString();
                row.Cells["ID"].Value = idProducto;

                // Columna Nombre
                string Nombre = filaDatos["Nombre"].ToString();
                row.Cells["Nombre"].Value = Nombre;

                // Columna Stock
                string Stock = filaDatos["Stock"].ToString();
                row.Cells["Stock"].Value = Stock;

                // Columna Precio
                string Precio = filaDatos["Precio"].ToString();
                row.Cells["Precio"].Value = Precio;

                // Columna Categoria
                string Categoria = filaDatos["Categoria"].ToString();
                row.Cells["Categoria"].Value = Categoria;

                // Columna ClaveInterna
                //string ClaveInterna = filaDatos["ClaveInterna"].ToString();
                //row.Cells["ClaveInterna"].Value = ClaveInterna;

                // Columna CodigBarras
                string CodigoBarras = filaDatos["CodigoBarras"].ToString();
                row.Cells["Codigo"].Value = CodigoBarras;
            }

            actualizar();

            clickBoton = 0;
        }
        #endregion

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

            if (DatosSourceFinal.Equals(1) || DatosSourceFinal.Equals(3))
            {
                if (!listaServCombo.Count().Equals(0))       // cuando es Producto
                {
                    var idServ = DGVStockProductos[0, numfila].Value.ToString();

                    foreach (var item in listaServCombo)
                    {
                        var words = item.Split('|');
                        var Fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        var IDServicio = words[1].ToString();
                        var IDProducto = words[2].ToString();
                        var NombreProducto = words[3].ToString();
                        var Cantidad = words[4].ToString();

                        if (IDServicio.Equals(idServ))
                        {
                            DGVStockProductos.Rows[idProdEdit].DefaultCellStyle.BackColor = Color.Yellow;
                            mensajeDeRelacionConImagenParaElUsuario(mensajeMessageBox, tituloMessageBox);
                           
                            //MessageBox.Show("La relación ya existe para este producto, combo ó servicio", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                if (!listaProd.Count.Equals(0))     // cuando es Combo ó Servicio
                {
                    var idServ = DGVStockProductos[0, numfila].Value.ToString();
                    foreach (var item in listaProd)
                    {
                        var words = item.Split('|');
                        var Fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        var IDServicio = words[1].ToString();
                        var IDProducto = words[2].ToString();
                        var NombreProducto = words[3].ToString();
                        var Cantidad = words[4].ToString();
                        

                        if (IDProducto.Equals(idServ))
                        {                            
                            MessageBox.Show("La relación ya existe para este producto, combo ó servicio", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
            }

            if (DatosSourceFinal.Equals(2))
            {
                if (!idProdEdit.Equals(0))
                {
                    if (typeStockFinal.Equals("Combos") || typeStockFinal.Equals("Servicios"))  // cuando es Editar Productos
                    {
                        var idServ = Convert.ToInt32(DGVStockProductos[0, numfila].Value.ToString());
                        using (DataTable dtRelacionProdComboServ = cn.CargarDatos(cs.checarSiExisteRelacionProducto(idProdEdit)))
                        {
                            if (!dtRelacionProdComboServ.Rows.Count.Equals(0))
                            {
                                foreach (DataRow drRelacion in dtRelacionProdComboServ.Rows)
                                {
                                    if (drRelacion["IDServicio"].Equals(idServ))
                                    {
                                        DGVStockProductos.Rows[idProdEdit].DefaultCellStyle.BackColor = Color.Yellow;
                                        mensajeDeRelacionConImagenParaElUsuario(mensajeMessageBox, tituloMessageBox);
                                        
                                        //MessageBox.Show("La relación ya existe para este producto", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                            }
                        }
                        if (!AgregarEditarProducto.listaProductoToCombo.Count.Equals(0))
                        {
                            foreach(var item in AgregarEditarProducto.listaProductoToCombo)
                            {
                                var claves = item.Split('|');
                                if (claves[2].Equals(Convert.ToString(idServ)))
                                {
                                    DGVStockProductos.Rows[idProdEdit].DefaultCellStyle.BackColor = Color.Yellow;
                                    mensajeDeRelacionConImagenParaElUsuario(mensajeMessageBox, tituloMessageBox);
                                    
                                    //MessageBox.Show("La relación ya existe para este producto", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    }
                    if (typeStockFinal.Equals("Productos"))
                    {
                        var idServ = Convert.ToInt32(DGVStockProductos[0, numfila].Value.ToString());
                        using (DataTable dtRelacionProdComboServ = cn.CargarDatos(cs.checarSiExisteRelacionComboServ(idProdEdit, idServ)))
                        {
                            if (!dtRelacionProdComboServ.Rows.Count.Equals(0))
                            {
                                foreach (DataRow drRelacion in dtRelacionProdComboServ.Rows)
                                {
                                    var idProducto = Convert.ToInt32(drRelacion["IDProducto"].ToString());
                                    if (idProducto.Equals(idServ))
                                    {
                                        DGVStockProductos.Rows[idProdEdit].DefaultCellStyle.BackColor = Color.Yellow;
                                        mensajeDeRelacionConImagenParaElUsuario(mensajeMessageBox, tituloMessageBox);
                                        
                                        //MessageBox.Show("La relación ya existe para este producto", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                }
                            }
                        }
                        if (!AgregarEditarProducto.ProductosDeServicios.Count.Equals(0))
                        {
                            foreach (var item in AgregarEditarProducto.ProductosDeServicios)
                            {
                                var claves = item.Split('|');
                                if (claves[2].Equals(Convert.ToString(idServ)))
                                {
                                    DGVStockProductos.Rows[idProdEdit].DefaultCellStyle.BackColor = Color.Yellow;
                                    mensajeDeRelacionConImagenParaElUsuario(mensajeMessageBox, tituloMessageBox);
                                    
                                    //MessageBox.Show("La relación ya existe para este producto", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                    }
                }
            }

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
            //ClaveInternaProdStr = DGVStockProductos[5, numfila].Value.ToString();
            // almacenamos en la variable CodigoBarrasProdStr del resultado de la consulta en DB
            CodigoBarrasProdStr = DGVStockProductos[5, numfila].Value.ToString();

            /************************************************************************
            *       verificamos en que campo va ir guardado la clave interna        *
            ************************************************************************/

            // en el caso los dos campos esten en blanco por default va ir en el de clave Interna
            if (/*(ClaveInternaProdStr == "") && */(CodigoBarrasProdStr == ""))
            {
                // indicamos que el valor de la variable a donde va guardarse sera 1
                opcionGuardar = 1;
            }
            // en el caso que tenga en blanco el campo de ClaveInterna en blanco va ir en el de clave Interna
            else if (/*(ClaveInternaProdStr == "") &&*/ (CodigoBarrasProdStr != ""))
            {
                // indicamos que el valor de la variable a donde va guardarse sera 2
                opcionGuardar = 2;
            }
            // en el caso que tenga en blanco el campo de CodigoBarras en blanco va ir en el de codigo de barras
            else if (/*(ClaveInternaProdStr != "") && */(CodigoBarrasProdStr == ""))
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
            //ClaveInternaProdStrFin = ClaveInternaProdStr;           // almacenamos el valor de ClaveInternaProd
            CodigoBarrasProdStrFin = CodigoBarrasProdStr;           // almacenamos el valor de CodigoBarrasProd
            consultadoDesdeListProd = 1;                            // almacenamos el valor si selecciono un producto
           
           
           
            consultadoDesdeListProdFin = consultadoDesdeListProd;   // almacenamos el valor de consultadoDesdeListProd
            opcionGuardarFin = opcionGuardar;                       // almacenamos el valor de opcionGuardar

            if (agregarstockxml.Equals(false))
            {
                if (typeStockFinal == "Productos")
                {
                    nombreProducto(NombreProdStrFin, IdProdStrFin);
                }
                else if (typeStockFinal == "Combos" || typeStockFinal == "Servicios")
                {
                    nombreProducto(NombreProdStrFin, IdProdStrFin);
                }
            }

            if (agregarstockxml.Equals(true))
            {
                if (typeStockFinal == "Productos")
                {
                    AgregarStockXML.StockProdStrFin = StockProdStr;  // almacenamos el valor de StockProd
                    AgregarStockXML.PrecioDelProdStrFin = PrecioDelProdStr;                 // almacenamos el valor de PrecioDelProd
                    AgregarStockXML.CategoriaProdStrFin = CategoriaProdStr;                 // almacenamos el valor de CategoriaProd
                    //AgregarStockXML.ClaveInternaProdStrFin = ClaveInternaProdStr;           // almacenamos el valor de ClaveInternaProd
                    AgregarStockXML.CodigoBarrasProdStrFin = CodigoBarrasProdStr;           // almacenamos el valor de CodigoBarrasProd
                    consultadoDesdeListProd = 1;                                            // almacenamos el valor si selecciono un producto
                    AgregarStockXML.consultadoDesdeListProdFin = consultadoDesdeListProd;   // almacenamos el valor de consultadoDesdeListProd
                    AgregarStockXML.opcionGuardarFin = opcionGuardar;                       // almacenamos el valor de opcionGuardar
                    nombreProducto(NombreProdStrFin, IdProdStrFin);
                }
            }
            

            this.Close();
        }

        private void ListaProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }

        private void mensajeDeRelacionConImagenParaElUsuario(string mensajeMessageBox, string tituloMessageBox)
        {
            //MessageBox.Show("Ruta Directorio:\n\n" + Properties.Settings.Default.rutaDirectorio + "\n" + Properties.Settings.Default.pathPUDVE);

            if (hWin32Resources == IntPtr.Zero)
            {
                hWin32Resources = LoadLibraryEx(Properties.Settings.Default.rutaDirectorio + "\\IconoRelacionProductoMessageBox.dll", IntPtr.Zero, 0);
                Debug.Assert(hWin32Resources != IntPtr.Zero);
            }

            // Identificador de recurso Win32 del icono que queremos poner en el cuadro de mensaje.
            const int Smiley = 104;
           
            MessageBoxIndirect mb = new MessageBoxIndirect(this, mensajeMessageBox, tituloMessageBox);
           


            // Cargue el icono de la DLL de recursos que cargamos.
            mb.Instance = hWin32Resources;
            mb.UserIcon = new IntPtr(Smiley);       // pasar el ID del icono como un IntPtr: el mismo resultado final que la macro MAKEINTRESOURCE en C ++
            mb.SysSmallIcon = new IntPtr(Smiley);

            mb.Modality = MessageBoxIndirect.MessageBoxExModality.SystemModal;

            mb.Show();
        }
    }
}
