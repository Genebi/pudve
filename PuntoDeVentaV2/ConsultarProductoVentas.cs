using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PuntoDeVentaV2
{
    public partial class ConsultarProductoVentas : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        private List<string> propiedades = new List<string>();


        string mensajeMostrar = string.Empty,
                tituloVentana = string.Empty,
                mensajeDefault = string.Empty,
                conceptoProductoAgregar = string.Empty,
                conceptoProductoEliminar = string.Empty;

        List<String> ID = new List<string>();
        int contador = 0;

        public static List<string> datosDeProducto = new List<string>();
        public static int idABuscar { get; set; }

        // Estanciar objeto de Clase Paginar
        // para usarlo
        private Paginar p;

        #region Sección de variables globales
        // Variables de tipo String
        string filtroConSinFiltroAvanzado = string.Empty;
        string DataMemberDGV = "Productos";
        string busqueda = string.Empty;

        // Variables de tipo Int
        int maximo_x_pagina = 16;
        int clickBoton = 0;
        #endregion

        int columnasAgregadas = 0;

        string mensajeParaMostrar = string.Empty;

        string filtro = string.Empty;

        #region Sección de operaciones de paginador
        public void filtroLoadProductos()
        {
            busqueda = txtBuscar.Text;

            filtroConSinFiltroAvanzado = cs.searchSaleProductAll(busqueda);
            p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);

            DGVProductos.Rows.Clear();
        }

        public static object cantidadPedida;
        public static object cancelarResta; 

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

            if (txtMaximoPorPagina.Text.Equals("0"))
            {
                txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
            }
            else
            {
                txtMaximoPorPagina.Text = p.limitRow().ToString();
            }
        }

        private void btnActualizarMaximoProductos_Click(object sender, EventArgs e)
        {
            if (!txtMaximoPorPagina.Text.Equals(string.Empty))
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

                //if (txtMaximoPorPagina.Text.Equals("0"))
                //{
                //    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                //}
                //else
                //{
                //    maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
                //    p.actualizarTope(maximo_x_pagina);
                //    CargarDatos();
                //    actualizar();
                //}
            }
            else if (txtMaximoPorPagina.Text.Equals(string.Empty))
            {
                txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
            }
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
            if (!txtMaximoPorPagina.Text.Equals(string.Empty))
            {
                if (txtMaximoPorPagina.Text.Equals("0"))
                {
                    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                }
                else
                {
                    maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
                    p.actualizarTope(maximo_x_pagina);
                    CargarDatos();
                    actualizar();
                }
            }
            else if (txtMaximoPorPagina.Text.Equals(string.Empty))
            {
                txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
            }
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
                if (!txtMaximoPorPagina.Text.Equals(string.Empty))
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
                }
                else if (txtMaximoPorPagina.Text.Equals(string.Empty))
                {
                    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                }
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

            if (DGVProductos.RowCount <= 0)
            {
                if (busqueda == "")
                {
                    if (!filtro.Equals("Todos"))
                    {
                        filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda,filtro);
                    }
                    else
                    {
                        filtroConSinFiltroAvanzado = cs.searchSaleProductAll(busqueda);
                    }

                    if (maximo_x_pagina.Equals(0))
                    {
                        MessageBox.Show("No se puede poner 0 en cantidad de filas a mostrar.", "Avertencia del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                    }
                }
                else if (busqueda != "")
                {
                    if (filtro.Equals("Todos"))
                    {
                        filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda, filtro);
                    }
                    else
                    {
                        filtroConSinFiltroAvanzado = cs.searchSaleProductAll(busqueda);
                    }

                    if (maximo_x_pagina.Equals(0))
                    {
                        MessageBox.Show("No se puede poner 0 en cantidad de filas a mostrar.", "Avertencia del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                    }
                }
            }
            else if (DGVProductos.RowCount >= 1 && clickBoton == 0)
            {
                if (busqueda == "")
                {
                    if (!filtro.Equals("Todos"))
                    {
                        filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda, filtro);
                    }
                    else
                    {
                        filtroConSinFiltroAvanzado = cs.searchSaleProductAll(busqueda);
                    }

                    if (maximo_x_pagina.Equals(0))
                    {
                        MessageBox.Show("No se puede poner 0 en cantidad de filas a mostrar.", "Avertencia del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                    }
                }
                else if (busqueda != "")
                {
                    if (!filtro.Equals("Todos"))
                    {
                        filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda, filtro);
                    }
                    else
                    {
                        filtroConSinFiltroAvanzado = cs.searchSaleProductAll(busqueda);
                    }

                    if (maximo_x_pagina.Equals(0))
                    {
                        MessageBox.Show("No se puede poner 0 en cantidad de filas a mostrar.", "Avertencia del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                    }
                }
            }

            DataSet datos = p.cargar();
            DataTable dtDatos = datos.Tables[0];

            DGVProductos.Rows.Clear();

            foreach (DataRow items in dtDatos.Rows)//OBtengo la lista de productos agregados para la venta
            {
                ID.Add(items["ID"].ToString());
            }

            if (dtDatos.Rows.Count.Equals(0))
            {
                MessageBox.Show("No se encuntra ninguna coincidencia\ncon la busqueda que desea realizar", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }


            foreach (DataRow filaDatos in dtDatos.Rows)
            {

                var tipodeMoneda = FormPrincipal.Moneda.Split('-');
                var moneda = tipodeMoneda[1].ToString().Trim().Replace("(", "").Replace(")", " ");

                var numeroFilas = DGVProductos.Rows.Count;

                string Nombre = string.Empty;
                string idProducto = string.Empty;

                Nombre = filaDatos["Nombre"].ToString();
                string Stock = filaDatos["Stock"].ToString();
                string Precio =  moneda + filaDatos["Precio"].ToString();
                string Clave = filaDatos["ClaveInterna"].ToString();
                string Codigo = filaDatos["CodigoBarras"].ToString();
                string Tipo = filaDatos["Tipo"].ToString();
                string Proveedor = filaDatos["Proveedor"].ToString();
                string chckName = filaDatos["ChckName"].ToString();
                string Descripcion = filaDatos["Descripcion"].ToString().Replace("_", " ");
                idProducto = filaDatos["ID"].ToString();

                if (Ventas.liststock2.Count.Equals(0))
                {

                }
                else  
                {

                    foreach (var item in Ventas.liststock2)
                    {
                        var producto = item.Split('|');
                        if (idProducto.Equals(producto[1]))
                        {
                            Nombre = "(" + producto[0] +") "+"* "+ Nombre;
                        }
                    }
                }
                


                if (DGVProductos.Rows.Count.Equals(0))
                {
                    bool encontrado = Utilidades.BuscarDataGridView(Nombre, "Nombre", DGVProductos, Tipo, "Tipo");

                    if (encontrado.Equals(false))
                    {
                        var number_of_rows = DGVProductos.Rows.Add();
                        DataGridViewRow row = DGVProductos.Rows[number_of_rows];

                        row.Cells["_id"].Value = idProducto;
                        row.Cells["Nombre"].Value = Nombre;     // Columna Nombre
                        row.Cells["Stock"].Value = Stock;       // Columna Stock
                        row.Cells["Precio"].Value = Precio;     // Columna Precio
                        row.Cells["Clave"].Value = Clave;       // Columna Clave
                        row.Cells["Codigo"].Value = Codigo;     // Columna Codigo

                        // Columna Tipo
                        if (Tipo.Equals("P"))
                        {
                            row.Cells["Tipo"].Value = "PRODUCTO";
                        }
                        else if (Tipo.Equals("S"))
                        {
                            row.Cells["Tipo"].Value = "SERVICIO";
                        }
                        else if (Tipo.Equals("PQ"))
                        {
                            row.Cells["Tipo"].Value = "COMBO";
                        }

                        row.Cells["Proveedor"].Value = Proveedor;   // Columna Proveedor

                        if (DGVProductos.Columns.Contains(chckName))
                        {
                            row.Cells[chckName].Value = Descripcion;
                        }
                    }
                }
                else if (!DGVProductos.Rows.Count.Equals(0))
                {
                    foreach (DataGridViewRow Row in DGVProductos.Rows)
                    {
                        bool encontrado = Utilidades.BuscarDataGridView(Nombre, "Nombre", DGVProductos, Tipo, "Tipo");

                        if (encontrado.Equals(true))
                        {
                            var Fila = Row.Index;
                            // Columnas Dinamicos
                            if (DGVProductos.Columns.Contains(chckName))
                            {
                                DGVProductos.Rows[Fila].Cells[chckName].Value = Descripcion;
                            }
                        }
                        else if (encontrado.Equals(false))
                        {
                            var number_of_rows = DGVProductos.Rows.Add();
                            DataGridViewRow row = DGVProductos.Rows[number_of_rows];

                            row.Cells["_id"].Value = idProducto;
                            row.Cells["Nombre"].Value = Nombre;         // Columna Nombre
                            row.Cells["Stock"].Value = Stock;           // Columna Stock
                            row.Cells["Precio"].Value = Precio;         // Columna Precio
                            row.Cells["Clave"].Value = Clave;           // Columna Clave
                            row.Cells["Codigo"].Value = Codigo;         // Columna Codigo

                            // Columna Tipo
                            if (Tipo.Equals("P"))
                            {
                                row.Cells["Tipo"].Value = "PRODUCTO";
                            }
                            else if (Tipo.Equals("S"))
                            {
                                row.Cells["Tipo"].Value = "SERVICIO";
                            }
                            else if (Tipo.Equals("PQ"))
                            {
                                row.Cells["Tipo"].Value = "COMBO";
                            }

                            // Columna Proveedor
                            row.Cells["Proveedor"].Value = Proveedor;

                            // Columnas Dinamicos
                            if (DGVProductos.Columns.Contains(chckName))
                            {
                                row.Cells[chckName].Value = Descripcion;
                            }
                        }
                    }
                }
            }

            actualizar();

            clickBoton = 0;
        }
        #endregion

        public ConsultarProductoVentas()
        {
            InitializeComponent();
        }

        private void ConsultarProductoVentas_Load(object sender, EventArgs e)
        {
            CBTipo.SelectedItem ="Todos";

            filtroLoadProductos();

            GenerarColumnas();

            var mostrarClave = FormPrincipal.clave;

            if (mostrarClave == 0)
            {
                DGVProductos.Columns[3].Visible = false;
            }
            else if (mostrarClave == 1)
            {
                DGVProductos.Columns[3].Visible = true;
            }

            CargarDatos();
        }

        private void GenerarColumnas()
        {
            var conceptos = mb.ConceptosAppSettingsBusqueda();

            foreach (var concepto in conceptos)
            {
                if (concepto == "Proveedor")
                {
                    continue;
                }

                // Este valor de proveedor esta agregado por defecto
                DataGridViewColumn columna = new DataGridViewTextBoxColumn();
                columna.HeaderText = concepto.Replace("_"," ");
                columna.Name = concepto;
                DGVProductos.Columns.Add(columna);

                // Guardamos los nombres de las propiedades en la lista
                propiedades.Add(concepto.Replace("_"," "));
                columnasAgregadas++;
            }
        }

        private void ConsultarProductoVentas_Shown(object sender, EventArgs e)
        {
            txtBuscar.Focus();
        }

        private void BuscarProductos()
        {
            var busqueda = txtBuscar.Text.Trim();



            //if (!string.IsNullOrWhiteSpace(busqueda))
            //{
            //    var coincidencias = mb.BusquedaCoincidenciasVentas(busqueda);

            //    if (coincidencias.Count > 0)
            //    {
            //        DGVProductos.Rows.Clear();

            //        foreach (var producto in coincidencias)
            //        {
            //            var datos = mb.ProductoConsultadoVentas(producto.Key, propiedades);

            //            AgregarProducto(datos);
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show($"No se encontraron productos con {txtBuscar.Text}","Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //}
        }

        private void AgregarProducto(Dictionary<string, string> datos)
        {
            if (datos.Count > 0)
            {
                int rowId = DGVProductos.Rows.Add();
                DataGridViewRow row = DGVProductos.Rows[rowId];

                foreach (var propiedad in datos)
                {
                    var valor = propiedad.Value;

                    if (propiedad.Key == "Tipo")
                    {
                        if (valor == "P") { valor = "PRODUCTO"; }
                        if (valor == "S") { valor = "SERVICIO"; }
                        if (valor == "PQ") { valor = "PAQUETE"; }
                    }

                    if (propiedad.Key == "Precio")
                    {
                        var precio = float.Parse(valor);
                        valor = precio.ToString("0.00");
                    }

                    if (propiedad.Key == "Stock")
                    {
                        valor = Utilidades.RemoverCeroStock(valor);
                    }

                    row.Cells[propiedad.Key].Value = valor;

                    //DGVProductos.Focus();
                    //DGVProductos.CurrentRow.Selected = true;
                    DGVProductos.ClearSelection();
                }
            }
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                //BuscarProductos();
                if (!txtBuscar.Text.Equals(string.Empty))
                {
                    CargarDatos(1, txtBuscar.Text);
                    txtBuscar.SelectAll();
                }
                else
                {
                    CargarDatos();
                }
            }

            if (e.KeyCode == Keys.Down && !DGVProductos.Rows.Count.Equals(0))
            {
                DGVProductos.Focus();
                DGVProductos.CurrentRow.Selected = true;
            }

            if (DGVProductos.Rows.Count >= 1 && txtBuscar.Text == "")
            {
                //BuscarProductos();
                CargarDatos();
            }
        }

        private void ConsultarProductoVentas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }

        private void DGVProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && DGVProductos.CurrentRow.Index == 0)
            {
                txtBuscar.Focus();
                DGVProductos.ClearSelection();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                mensajeMostrar = string.Empty;
                tituloVentana = string.Empty;
                mensajeDefault = string.Empty;

                mensajeMostrar = "Ingrese la cantidad de productos que necesita";
                tituloVentana = "Cantidad a pedir";
                mensajeDefault = Ventas.cantidadAPedir;

                inputMessageBoxVentas inputMessageBox = new inputMessageBoxVentas(mensajeMostrar, tituloVentana, mensajeDefault);

                inputMessageBox.ShowDialog();
                multiplesProductosSeleccionados();
                newObtenerDatoProductoSeleccionado();
            }
        }

        private void CBTipo_TextChanged(object sender, EventArgs e)
        {

            if (CBTipo.SelectedItem.Equals("Productos"))
            {
                filtro = "P";
            }
            else if (CBTipo.SelectedItem.Equals("Servicios"))
            {
                filtro = "S";
            }
            else if (CBTipo.SelectedItem.Equals("Combos"))
            {
                filtro = "PQ";
            }
            else if (CBTipo.SelectedItem.Equals("Todos"))
            {
                filtro = "Todos";
            }
            CargarDatos();
        }

        public void multiplesProductosSeleccionados()
        {
            List<string> productos = new List<string>();
            var rows = DGVProductos.SelectedRows;
            for (int i = 0; i < rows.Count; i++)
            {
                //var renglon = rows[0]; 
                //var codigoProd = DGVProductos.SelectedRows[i].Cells[4].Value.ToString();
                //var datosProducto = cn.CargarDatos($"SELECT ID, Nombre, Precio, TipoDescuento, Stock, Tipo, ClaveInterna, CodigoBarras, StockNecesario, ProdImage, StockMinimo, PrecioCompra, PrecioMayoreo, Impuesto, Categoria, ProdImage, ClaveProducto, UnidadMedida  FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND CodigoBarras = '{codigoProd}'");

                var idProductoComboServicio = DGVProductos.SelectedRows[i].Cells[7].Value.ToString();

                var datosProducto = cn.CargarDatos($"SELECT ID, Nombre, Precio, TipoDescuento, Stock, Tipo, ClaveInterna, CodigoBarras, StockNecesario, ProdImage, StockMinimo, PrecioCompra, PrecioMayoreo, Impuesto, Categoria, ProdImage, ClaveProducto, UnidadMedida  FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idProductoComboServicio}'");

                string idProducto = datosProducto.Rows[0]["ID"].ToString();
                productos.Add(idProducto);
            }
        }

        private void newObtenerDatoProductoSeleccionado()
        {
            datosDeProducto.Clear();

            if (!DGVProductos.Rows.Count.Equals(0))
            {
            var rows = DGVProductos.SelectedRows;
                for (int i = 0; i < rows.Count; i++)
                {
                    //var codigoProd = DGVProductos.SelectedRows[i].Cells[4].Value.ToString();//SE NECESITA UN DATO PARA VALIDAR QUE CADA PRODUCTO SEA DIFRENTE

                    //var datosProducto = cn.CargarDatos($"SELECT ID, Nombre, Precio, TipoDescuento, Stock, Tipo, ClaveInterna, CodigoBarras, StockNecesario, ProdImage, StockMinimo, PrecioCompra, PrecioMayoreo, Impuesto, Categoria, ProdImage, ClaveProducto, UnidadMedida  FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND CodigoBarras = '{codigoProd}' AND Status = '1'");

                    var idProducto = DGVProductos.SelectedRows[i].Cells[7].Value.ToString();

                    var datosProducto = cn.CargarDatos($"SELECT ID, Nombre, Precio, TipoDescuento, Stock, Tipo, ClaveInterna, CodigoBarras, StockNecesario, ProdImage, StockMinimo, PrecioCompra, PrecioMayoreo, Impuesto, Categoria, ProdImage, ClaveProducto, UnidadMedida  FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idProducto}' AND Status = '1'");

                    //var datosProducto = cn.CargarDatos(cs.searchSaleProduct(codigoProd));

                    var id = string.Empty; var nombre = string.Empty; var precio = string.Empty; var tipoDescuento = string.Empty; var stock = string.Empty; var tipo = string.Empty; var claveInterna = string.Empty; var codigoBarras = string.Empty; var stockNecesario = string.Empty; var prodImage = string.Empty; var stockMinimo = string.Empty; var precioCompra = string.Empty; var precioMayoreo = string.Empty; var impuesto = string.Empty; var categoria = string.Empty; var prodimage = string.Empty; var claveProducto = string.Empty; var unidadMedida = string.Empty;

                    if (!datosProducto.Rows.Count.Equals(0))
                    {
                        id = datosProducto.Rows[0]["ID"].ToString();
                        nombre = datosProducto.Rows[0]["Nombre"].ToString();
                        precio = datosProducto.Rows[0]["Precio"].ToString();
                        tipoDescuento = datosProducto.Rows[0]["TipoDescuento"].ToString();
                        stock = datosProducto.Rows[0]["Stock"].ToString();
                        tipo = datosProducto.Rows[0]["Tipo"].ToString();
                        claveInterna = datosProducto.Rows[0]["ClaveInterna"].ToString();
                        codigoBarras = datosProducto.Rows[0]["CodigoBarras"].ToString();
                        stockNecesario = datosProducto.Rows[0]["StockNecesario"].ToString();
                        prodImage = datosProducto.Rows[0]["ProdImage"].ToString();
                        stockMinimo = datosProducto.Rows[0]["StockMinimo"].ToString();
                        precioCompra = datosProducto.Rows[0]["PrecioCompra"].ToString();
                        precioMayoreo = datosProducto.Rows[0]["PrecioMayoreo"].ToString();
                        impuesto = datosProducto.Rows[0]["Impuesto"].ToString();
                        categoria = datosProducto.Rows[0]["Categoria"].ToString();
                        prodimage = datosProducto.Rows[0]["ProdImage"].ToString();
                        claveProducto = datosProducto.Rows[0]["ClaveProducto"].ToString();
                        unidadMedida = datosProducto.Rows[0]["UnidadMedida"].ToString();
                    }

                    datosProducto.Clear();

                    datosDeProducto.Add(id + "|" + nombre + "|" + precio+"|"+tipoDescuento+"|"+stock + "|" +tipo + "|" +claveInterna + "|" +codigoBarras + "|" +stockNecesario + "|" +prodImage + "|" +stockMinimo + "|" +precioCompra + "|" +precioMayoreo + "|" +impuesto + "|" +categoria + "|" +prodimage + "|" +claveProducto + "|" +unidadMedida);
                  
                }

                cantidadPedida = inputMessageBoxVentas.cantidad;
                foreach (DataGridViewRow item in DGVProductos.Rows)
                {
                    if (item.Selected)
                    {
                        var dato = item.Cells["Nombre"].Value.ToString();
                        if (dato.Contains('(') && dato.Contains(')') && dato.Contains('*'))
                        {
                            var Cantidad = dato.Split(')');
                            var validarCantidad = Cantidad[0].Replace("(", "");
                            if (cantidadPedida == "Cancelar")
                            {
                                cantidadPedida = 0;
                            }
                            if (cantidadPedida.ToString().Contains('-') && Convert.ToDecimal(validarCantidad) <= Math.Abs(Convert.ToDecimal(cantidadPedida) ))
                            {
                                MessageBox.Show("Uno de los productos a disminuir es menor a la                 cantidad indicada", "Aviso del sistema", MessageBoxButtons.OK,          MessageBoxIcon.Information);
                                cancelarResta = "return";
                                return;

                            }
                        }
                    }
                    
                }
                if (cantidadPedida == "Cancelar")
                {

                }
                else
                {
                    this.Close();
                }
            }
        }


        private void obtenerDatoProductoSeleccionado()
        {
            //    datosDeProducto.Clear();

            //    if (!DGVProductos.Rows.Count.Equals(0))
            //    {
            //        var codigoProd = DGVProductos.CurrentRow.Cells[4].Value.ToString();
            //        //idABuscar = Convert.ToInt32(idProd);

            //        var datosProducto = cn.CargarDatos($"SELECT ID, Nombre, Precio, TipoDescuento, Stock, Tipo, ClaveInterna, CodigoBarras, StockNecesario, ProdImage, StockMinimo, PrecioCompra, PrecioMayoreo, Impuesto, Categoria, ProdImage, ClaveProducto, UnidadMedida  FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND CodigoBarras = '{codigoProd}'");

            //        var id = string.Empty; var nombre = string.Empty; var precio = string.Empty; var tipoDescuento = string.Empty; var stock = string.Empty; var tipo = string.Empty; var claveInterna = string.Empty; var codigoBarras = string.Empty; var stockNecesario = string.Empty; var prodImage = string.Empty; var stockMinimo = string.Empty; var precioCompra = string.Empty; var precioMayoreo = string.Empty; var impuesto = string.Empty; var categoria = string.Empty; var prodimage = string.Empty; var claveProducto = string.Empty; var unidadMedida = string.Empty;

            //        if (!datosProducto.Rows.Count.Equals(0))
            //        {
            //            id = datosProducto.Rows[0]["ID"].ToString();
            //            nombre = datosProducto.Rows[0]["Nombre"].ToString();
            //            precio = datosProducto.Rows[0]["Precio"].ToString();
            //            tipoDescuento = datosProducto.Rows[0]["TipoDescuento"].ToString();
            //            stock = datosProducto.Rows[0]["Stock"].ToString();
            //            tipo = datosProducto.Rows[0]["Tipo"].ToString();
            //            claveInterna = datosProducto.Rows[0]["ClaveInterna"].ToString();
            //            codigoBarras = datosProducto.Rows[0]["CodigoBarras"].ToString();
            //            stockNecesario = datosProducto.Rows[0]["StockNecesario"].ToString();
            //            prodImage = datosProducto.Rows[0]["ProdImage"].ToString();
            //            stockMinimo = datosProducto.Rows[0]["StockMinimo"].ToString();
            //            precioCompra = datosProducto.Rows[0]["PrecioCompra"].ToString();
            //            precioMayoreo = datosProducto.Rows[0]["PrecioMayoreo"].ToString();
            //            impuesto = datosProducto.Rows[0]["Impuesto"].ToString();
            //            categoria = datosProducto.Rows[0]["Categoria"].ToString();
            //            prodimage = datosProducto.Rows[0]["ProdImage"].ToString();
            //            claveProducto = datosProducto.Rows[0]["ClaveProducto"].ToString();
            //            unidadMedida = datosProducto.Rows[0]["UnidadMedida"].ToString();
            //        }

            //        datosProducto.Clear();

            //        datosDeProducto.Add(id);
            //        datosDeProducto.Add(nombre);
            //        datosDeProducto.Add(precio);
            //        datosDeProducto.Add(tipoDescuento);
            //        datosDeProducto.Add(stock);
            //        datosDeProducto.Add(tipo);
            //        datosDeProducto.Add(claveInterna);
            //        datosDeProducto.Add(codigoBarras);
            //        datosDeProducto.Add(stockNecesario);
            //        datosDeProducto.Add(prodImage);
            //        datosDeProducto.Add(stockMinimo);
            //        datosDeProducto.Add(precioCompra);
            //        datosDeProducto.Add(precioMayoreo);
            //        datosDeProducto.Add(impuesto);
            //        datosDeProducto.Add(categoria);
            //        datosDeProducto.Add(prodimage);
            //        datosDeProducto.Add(claveProducto);
            //        datosDeProducto.Add(unidadMedida);

            //        this.Close();
            //    }
        }//Se actualizo metodo a "newObtenerDatoProductoSeleccionado()"

        private void DGVProductos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            mensajeMostrar = "Ingrese la cantidad de productos que necesita";
            tituloVentana = "Cantidad a pedir";
            mensajeDefault = Ventas.cantidadAPedir;

            inputMessageBoxVentas inputMessageBox = new inputMessageBoxVentas(mensajeMostrar, tituloVentana, mensajeDefault);
            inputMessageBox.ShowDialog();
            newObtenerDatoProductoSeleccionado();
        }
    }
}
