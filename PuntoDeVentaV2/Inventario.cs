using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public partial class Inventario : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        public static int NumRevActivo = 0;
        public static bool limpiarTabla = false;

        public static string proveedorElegido = string.Empty;
        public static int idReporte = 0;
        public static bool botonAceptar = false;
        public static bool aceptarFiltro = false;

        public float getSuma { get; set; }
        public static float suma = 0;

        public float getResta { get; set; }
        public static float resta = 0;

        public float getStockAnterior { get; set; }
        public static float stockAnterior = 0;

        // Almacena temporalmente los productos encontrados con las coincidencias de la busqueda
        Dictionary<int, string> productos;

        static public List<string> idProductoDelCombo;

        public int GetNumRevActive { get; set; }

        // Permisos de los botones
        int opcion1 = 1; // Boton revisar inventario
        int opcion2 = 1; // Boton actualizar inventario
        int opcion3 = 1; // Boton actualizar inventario XML
        int opcion4 = 1; // Boton buscar
        int opcion5 = 1; // Boton terminar

        // tipo de selección Aumentar, Disminuir
        int tipoSeleccion = 0;

        // Lista para pasar conceptos seleccionados desde la venta de selección conceptos
        public static List<string> listaConceptosSeleccionados;

        // variables para ber que conceptos son los que estaran activos para el reporte
        bool Producto = false;
        bool Proveedor = false;
        bool UnidadesCompradas = false;
        bool UnidadesDisminuidas = false;
        bool PrecioCompra = false;
        bool PrecioVenta = false;
        bool StockAnterior = false;
        bool StockActual = false;
        bool FechaCompra = false;
        bool FechaOperacion = false;
        bool Comentario = false;

        int columnasConcepto = 0;

        public static string filtradoParaRealizar = string.Empty;

        public Inventario()
        {
            listaConceptosSeleccionados = new List<string>();
            InitializeComponent();
        }

        private void Inventario_Load(object sender, EventArgs e)
        {
            idReporte = cn.ObtenerUltimoIdReporte(FormPrincipal.userID) + 1;

            if (FormPrincipal.id_empleado > 0)
            {
                var permisos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Inventario");

                opcion1 = permisos[0];
                opcion2 = permisos[1];
                opcion3 = permisos[2];
                opcion4 = permisos[3];
                opcion5 = permisos[4];
            }
            this.Focus();

            if (rbAumentarProducto.Checked)
            {
                populateAumentarDGVInventario();
            }
            else if (rbDisminuirProducto.Checked)
            {
                populateDisminuirDGVInventario();
            }

            if (columnasConcepto.Equals(0))
            {
                columnasConcepto = 10;
                listaConceptosSeleccionados.Clear();
            }

            var mostrarClave = FormPrincipal.clave;

            if (mostrarClave == 0)
            {
                DGVInventario.Columns[6].Visible = false;
            }
            else if (mostrarClave == 1)
            {
                DGVInventario.Columns[6].Visible = true;
            }


            // Solo para dos cuentas
            //if (FormPrincipal.userNickName == "MI_RI" | FormPrincipal.userNickName == "OXXOCLARA6")
            //{
            //    btnActualizarXML.Visible = true;
            //}
        }

        private void populateDisminuirDGVInventario()
        {
            using (DataTable dtRetriveDisminuirInventario = cn.CargarDatos(cs.GetDisminuirInventario()))
            {
                if (!dtRetriveDisminuirInventario.Rows.Count.Equals(0))
                {
                    DGVInventario.Rows.Clear();
                    foreach (DataRow dr in dtRetriveDisminuirInventario.Rows)
                    {
                        int rowId = DGVInventario.Rows.Add();
                        DataGridViewRow row = DGVInventario.Rows[rowId];
                        row.Cells["ID"].Value = dr["IdProducto"].ToString();
                        row.Cells["Nombre"].Value = dr["NombreProducto"].ToString();
                        row.Cells["Stock"].Value = dr["StockActual"].ToString();
                        row.Cells["DiferenciaUnidades"].Value = dr["DiferenciaUnidades"].ToString();
                        row.Cells["DiferenciaUnidades"].Style.ForeColor = Color.OrangeRed;
                        row.Cells["DiferenciaUnidades"].Style.Font = new System.Drawing.Font(DGVInventario.Font, FontStyle.Bold);
                        row.Cells["NuevoStock"].Value = dr["NuevoStock"].ToString();
                        row.Cells["Precio"].Value = dr["Precio"].ToString();
                        row.Cells["Clave"].Value = dr["Clave"].ToString();
                        row.Cells["Codigo"].Value = dr["Codigo"].ToString();
                        row.Cells["Fecha"].Value = dr["Fecha"].ToString();
                        row.Cells["IDTabla"].Value = dr["ID"].ToString();
                        if (!dr["Comentarios"].ToString().Equals(""))
                        {
                            DGVInventario.Columns["Comentarios"].Visible = true;
                            row.Cells["Comentarios"].Value = dr["Comentarios"].ToString();
                        }
                    }
                }
            }
            DGVInventario.Sort(DGVInventario.Columns["Fecha"], ListSortDirection.Descending);
        }

        public void populateAumentarDGVInventario()
        {
            using (DataTable dtRetriveAumentarInventario = cn.CargarDatos(cs.GetAumentarInventario()))
            {
                if (!dtRetriveAumentarInventario.Rows.Count.Equals(0))
                {
                    DGVInventario.Rows.Clear();
                    foreach (DataRow dr in dtRetriveAumentarInventario.Rows)
                    {
                        int rowId = DGVInventario.Rows.Add();

                        DataGridViewRow row = DGVInventario.Rows[rowId];

                        row.Cells["ID"].Value = dr["IdProducto"].ToString();
                        row.Cells["Nombre"].Value = dr["NombreProducto"].ToString();
                        row.Cells["Stock"].Value = dr["StockActual"].ToString();
                        row.Cells["DiferenciaUnidades"].Value = dr["DiferenciaUnidades"].ToString();
                        row.Cells["DiferenciaUnidades"].Style.ForeColor = Color.DodgerBlue;
                        row.Cells["DiferenciaUnidades"].Style.Font = new System.Drawing.Font(DGVInventario.Font, FontStyle.Bold);
                        row.Cells["NuevoStock"].Value = dr["NuevoStock"].ToString();
                        row.Cells["IDTabla"].Value = dr["ID"].ToString();
                        //if (!dr["ValorUnitario"].ToString().Equals("0.00"))
                        //{
                        //    row.Cells["Precio"].Value = dr["ValorUnitario"].ToString();
                        //}
                        //else if (dr["ValorUnitario"].ToString().Equals("0.00"))
                        //{
                        //    row.Cells["Precio"].Value = dr["Precio"].ToString();
                        //}
                        row.Cells["Precio"].Value = dr["Precio"].ToString();
                        row.Cells["Clave"].Value = dr["Clave"].ToString();
                        row.Cells["Codigo"].Value = dr["Codigo"].ToString();
                        row.Cells["Fecha"].Value = dr["Fecha"].ToString();
                        if (!dr["Comentarios"].ToString().Equals(""))
                        {
                            DGVInventario.Columns["Comentarios"].Visible = true;
                            row.Cells["Comentarios"].Value = dr["Comentarios"].ToString();
                        }
                    }
                }
            }
            DGVInventario.Sort(DGVInventario.Columns["Fecha"], ListSortDirection.Descending);
        }

        private bool ExistenProductos(string nombre)
        {
            var tieneProductos = mb.TieneProductos();

            if (!tieneProductos)
            {
                var mensaje = string.Join(
                    Environment.NewLine,
                    $"Para poder {nombre} inventario es necesario registrar",
                    "productos, ya que actualmente el sistema ha detectado",
                    "que no hay productos registrados."
                );

                MessageBox.Show(mensaje, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return tieneProductos;
        }

        private void btnRevisar_Click(object sender, EventArgs e)
        {
            gBSeleccionActualizarInventario.Visible = false;
            panelContenedor.Visible = false;

            if (opcion1 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (!ExistenProductos("revisar"))
            {
                return;
            }

            if (Application.OpenForms.OfType<FiltroRevisarInventario>().Count() == 1)
            {
                Application.OpenForms.OfType<FiltroRevisarInventario>().First().BringToFront();
            }
            else
            {
                var filtro = new FiltroRevisarInventario();

                filtro.FormClosed += delegate
                {
                    if (aceptarFiltro)
                    {
                        filtradoParaRealizar = filtro.tipoFiltro;

                        if (filtradoParaRealizar.Equals("Filtros"))
                        {
                            if (Application.OpenForms.OfType<RevisarInventario>().Count() == 1)
                            {
                                Application.OpenForms.OfType<RevisarInventario>().First().BringToFront();
                            }
                            else
                            {
                                var datos = new string[] { filtro.tipoFiltro, filtro.operadorFiltro, filtro.textoFiltroDinamico.ToString() };

                                panelContenedor.Visible = false;
                                aceptarFiltro = false;

                                RevisarInventario revisar = new RevisarInventario(datos);

                                revisar.FormClosed += delegate
                                {
                                    ReporteFinalRevisarInventario reporte = new ReporteFinalRevisarInventario();
                                    reporte.GetFilterNumActiveRecord = NumRevActivo;
                                    reporte.limpiarTabla = limpiarTabla;
                                    limpiarTabla = false;
                                    reporte.ShowDialog();
                                };

                                revisar.ShowDialog();
                            }
                        }
                        else
                        {
                            if (Application.OpenForms.OfType<RevisarInventario>().Count() == 1)
                            {
                                Application.OpenForms.OfType<RevisarInventario>().First().BringToFront();
                            }
                            else
                            {
                                var datos = new string[] { filtro.tipoFiltro, filtro.operadorFiltro, filtro.cantidadFiltro.ToString() };

                                panelContenedor.Visible = false;
                                aceptarFiltro = false;

                                RevisarInventario revisar = new RevisarInventario(datos);

                                revisar.FormClosed += delegate
                                {
                                    ReporteFinalRevisarInventario reporte = new ReporteFinalRevisarInventario();
                                    reporte.GetFilterNumActiveRecord = NumRevActivo;
                                    reporte.limpiarTabla = limpiarTabla;
                                    limpiarTabla = false;
                                    reporte.ShowDialog();
                                };

                                revisar.ShowDialog();
                            }
                        }
                    }
                };

                filtro.ShowDialog();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (opcion2 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (!ExistenProductos("actualizar"))
            {
                return;
            }

            gBSeleccionActualizarInventario.Visible = true;

            tipoSeleccion = 0;

            panelContenedor.Visible = true;

            txtBusqueda.Focus();
        }

        private void btnActualizarXML_Click(object sender, EventArgs e)
        {
            if (opcion3 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<AgregarStockXML>().Count() == 1)
            {
                Application.OpenForms.OfType<AgregarStockXML>().First().BringToFront();
            }
            else
            {
                gBSeleccionActualizarInventario.Visible = false;
                panelContenedor.Visible = false;

                AgregarStockXML inventarioXML = new AgregarStockXML();

                inventarioXML.FormClosed += delegate
                {
                    GenerarReporte(idReporte, 1);

                    idReporte++;
                };

                inventarioXML.Show();
            }
        }

        private void txtBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            //ocultarResultados();
            //listaProductos.Items.Clear();
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                RealizarBusqueda();
                txtBusqueda.Text = string.Empty;
            }

            if (e.KeyData == Keys.Back || e.KeyData == Keys.Delete || e.KeyData == Keys.Escape)
            {
                var busqueda = txtBusqueda.Text.Trim();

                if (busqueda.Length == 0)
                {
                    ocultarResultados();
                    listaProductos.Items.Clear();
                }
            }
            else if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void RealizarBusqueda()
        {
            if (!string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                int found = 0;
                char separador = '*';
                string[] datosSeparados;
                string textoABuscar = string.Empty;

                textoABuscar = txtBusqueda.Text;

                found = textoABuscar.IndexOf(separador);

                // Dividimos lo que puso el usuario en el TextBox
                // todo en base al caracter del *
                datosSeparados = textoABuscar.Split(separador);

                listaProductos.Items.Clear();

                idProductoDelCombo = new List<string>();

                int idProducto = 0;

                // Vemos si el usuario puso el caracter especial de 
                // * antes que el codigo de barras para separarlos
                // y verificamos sí hay mas de un elemento en Array
                if (datosSeparados.Length > 1)
                {
                    // Verificamos si es codigo de barra o clave
                    idProducto = mb.BuscarProductoInventario(datosSeparados[1].Trim());

                    // Verificamos si existe en la tabla de codigos de barra extra
                    var datosTmp = mb.BuscarCodigoBarrasExtra(datosSeparados[1].Trim());

                    if (datosTmp.Length > 0)
                    {
                        foreach (var id in datosTmp)
                        {
                            // Verificar que pertenece al usuario
                            var verificarUsuario = (bool)cn.EjecutarSelect($"SELECT * FROM Productos WHERE ID = {id} AND IDUsuario = {FormPrincipal.userID} AND Status = 1");

                            if (verificarUsuario)
                            {
                                idProducto = Convert.ToInt32(id);
                            }
                        }
                    }

                    // Verificamos si es que no encontro el codigo de barra o clave en productos
                    // o en código de barra extra podemos hacer la busqueda en Combos
                    if (idProducto.Equals(0))
                    {
                        idProducto = mb.BuscarComboInventario(datosSeparados[1].Trim());

                        var datosProd = mb.ObtenerDatosProductoPaqueteServicio(idProducto, FormPrincipal.userID);

                        // si es que encontro algún Combo relacionado
                        if (idProducto > 0)
                        {
                            // Almacenamos los datos del Combo
                            var datosCombo = mb.BuscarProductosDeServicios(Convert.ToString(idProducto));
                            if (datosCombo.Count().Equals(0) && (datosProd[4].ToString().Equals("PQ") || datosProd[4].ToString().Equals("S")))
                            {
                                DialogResult result = MessageBox.Show("El Código o Clave buscada pertenece a un Paquete\nNo tiene producto relacionado \n\n" + "\n\nDesea actualizar el Stock", "Aviso de Actualziación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (result == DialogResult.Yes)
                                {

                                }
                                else if (result == DialogResult.No)
                                {
                                    idProducto = 0;
                                }
                            }
                            else if (datosCombo.Count() > 0)
                            {
                                if (datosCombo.Count().Equals(1))
                                {
                                    List<string> nombresProductos = new List<string>();
                                    string[] str;

                                    foreach (var item in datosCombo)
                                    {
                                        str = item.Split('|');
                                        nombresProductos.Add(str[2].ToString());
                                        idProductoDelCombo.Add(str[1].ToString());
                                        idProductoDelCombo.Add(str[3].ToString());
                                    }
                                    DialogResult result = MessageBox.Show("El Código o Clave buscada pertenece a un combo\nEl producto relacionado es:\n\n" + nombresProductos[0].ToString() + "\n\nDesea actualizar el Stock", "Aviso de Actualziación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (result == DialogResult.Yes)
                                    {
                                        idProducto = Convert.ToInt32(idProductoDelCombo[0].ToString());
                                    }
                                    else if (result == DialogResult.No)
                                    {
                                        idProducto = 0;
                                    }
                                }
                                else if (datosCombo.Count() > 1)
                                {
                                    List<string> nombresProductos = new List<string>();
                                    string[] str;

                                    idProducto = 0;

                                    foreach (var item in datosCombo)
                                    {
                                        str = item.Split('|');
                                        nombresProductos.Add(str[2].ToString() + "\n");
                                    }

                                    var message = string.Join(Environment.NewLine, nombresProductos);

                                    nombresProductos.Clear();

                                    MessageBox.Show("Resultado del Código o Clave buscada pertenece a un combo;\nel cual contiene más de un Producto por favor debe de realizar\nla actualización de cada uno de ellos:\n\n" + message, "Aviso de Actualziación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }
                }
                else if (datosSeparados.Length == 1)
                {
                    if (idProducto.Equals(0))
                    {
                        idProducto = mb.BuscarProductoInventario(datosSeparados[0].Trim());

                        // Verificamos si existe en la tabla de codigos de barra extra
                        var datosTmp = mb.BuscarCodigoBarrasExtra(datosSeparados[0].Trim());

                        if (datosTmp.Length > 0)
                        {
                            foreach (var id in datosTmp)
                            {
                                // Verificar que pertenece al usuario
                                var verificarUsuario = (bool)cn.EjecutarSelect($"SELECT * FROM Productos WHERE ID = {id} AND IDUsuario = {FormPrincipal.userID} AND Status = 1");

                                if (verificarUsuario)
                                {
                                    idProducto = Convert.ToInt32(id);
                                }
                            }
                        }

                        var datosProd = mb.ObtenerDatosProductoPaqueteServicio(idProducto, FormPrincipal.userID);

                        // si es que encontro algún Combo relacionado
                        if (idProducto > 0)
                        {
                            // Almacenamos los datos del Combo
                            var datosCombo = mb.BuscarProductosDeServicios(Convert.ToString(idProducto));
                            if (datosCombo.Count().Equals(0) && (datosProd[4].ToString().Equals("PQ") || datosProd[4].ToString().Equals("S")))
                            {
                                DialogResult result = MessageBox.Show("El Código o Clave buscada pertenece a un Paquete\nNo tiene producto relacionado \n\n" + "\n\nDesea actualizar el Stock", "Aviso de Actualziación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (result == DialogResult.Yes)
                                {

                                }
                                else if (result == DialogResult.No)
                                {
                                    idProducto = 0;
                                }
                            }
                            else if (datosCombo.Count() > 0)
                            {
                                if (datosCombo.Count().Equals(1))
                                {
                                    List<string> nombresProductos = new List<string>();
                                    string[] str;

                                    foreach (var item in datosCombo)
                                    {
                                        str = item.Split('|');
                                        nombresProductos.Add(str[2].ToString());
                                        idProductoDelCombo.Add(str[1].ToString());
                                        idProductoDelCombo.Add(str[3].ToString());
                                    }
                                    DialogResult result = MessageBox.Show("El Código o Clave buscada pertenece a un combo\nEl producto relacionado es:\n\n" + nombresProductos[0].ToString() + "\n\nDesea actualizar el Stock", "Aviso de Actualziación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (result == DialogResult.Yes)
                                    {
                                        idProducto = Convert.ToInt32(idProductoDelCombo[0].ToString());
                                    }
                                    else if (result == DialogResult.No)
                                    {
                                        idProducto = 0;
                                    }
                                }
                                else if (datosCombo.Count() > 1)
                                {
                                    List<string> nombresProductos = new List<string>();
                                    string[] str;

                                    idProducto = 0;

                                    foreach (var item in datosCombo)
                                    {
                                        str = item.Split('|');
                                        nombresProductos.Add(str[2].ToString() + "\n");
                                    }

                                    var message = string.Join(Environment.NewLine, nombresProductos);

                                    nombresProductos.Clear();

                                    MessageBox.Show("Resultado del Código o Clave buscada pertenece a un combo;\nel cual contiene más de un Producto por favor debe de realizar\nla actualización de cada uno de ellos:\n\n" + message, "Aviso de Actualziación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }

                    if (idProducto.Equals(0))
                    {
                        idProducto = mb.BuscarComboInventario(datosSeparados[0].Trim());

                        var datosProd = mb.ObtenerDatosProductoPaqueteServicio(idProducto, FormPrincipal.userID);

                        // si es que encontro algún Combo relacionado
                        if (idProducto > 0)
                        {
                            // Almacenamos los datos del Combo
                            var datosCombo = mb.BuscarProductosDeServicios(Convert.ToString(idProducto));
                            if (datosCombo.Count().Equals(0) && (datosProd[4].ToString().Equals("PQ") || datosProd[4].ToString().Equals("S")))
                            {
                                DialogResult result = MessageBox.Show("El Código o Clave buscada pertenece a un Paquete\nNo tiene producto relacionado \n\n" + "\n\nDesea actualizar el Stock", "Aviso de Actualziación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (result == DialogResult.Yes)
                                {

                                }
                                else if (result == DialogResult.No)
                                {
                                    idProducto = 0;
                                }
                            }
                            else if (datosCombo.Count() > 0)
                            {
                                if (datosCombo.Count().Equals(1))
                                {
                                    List<string> nombresProductos = new List<string>();
                                    string[] str;

                                    foreach (var item in datosCombo)
                                    {
                                        str = item.Split('|');
                                        nombresProductos.Add(str[2].ToString());
                                        idProductoDelCombo.Add(str[1].ToString());
                                        idProductoDelCombo.Add(str[3].ToString());
                                    }
                                    DialogResult result = MessageBox.Show("El Código o Clave buscada pertenece a un Paquete\nEl producto relacionado es:\n\n" + nombresProductos[0].ToString() + "\n\nDesea actualizar el Stock",
                                                                          "Aviso de Actualziación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                    if (result == DialogResult.Yes)
                                    {
                                        idProducto = Convert.ToInt32(idProductoDelCombo[0].ToString());
                                    }
                                    else if (result == DialogResult.No)
                                    {
                                        idProducto = 0;
                                    }
                                }
                                else if (datosCombo.Count() > 1)
                                {
                                    List<string> nombresProductos = new List<string>();
                                    string[] str;

                                    idProducto = 0;

                                    foreach (var item in datosCombo)
                                    {
                                        str = item.Split('|');
                                        nombresProductos.Add(str[2].ToString() + "\n");
                                    }

                                    var message = string.Join(Environment.NewLine, nombresProductos);

                                    nombresProductos.Clear();

                                    MessageBox.Show("Resultado del Código o Clave buscada pertenece a un Paquete;\nel cual contiene más de un Producto por favor debe de realizar\nla actualización de cada uno de ellos:\n\n" + message,
                                                    "Aviso de Actualziación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                        }
                    }
                }

                // Si es mayor a cero es un producto y lo mostramos directamente en la ventana de ajustar
                if (idProducto > 0)
                {
                    if (rbAumentarProducto.Checked)
                    {
                        tipoSeleccion = 1;
                    }
                    else if (rbDisminuirProducto.Checked)
                    {
                        tipoSeleccion = 2;
                    }

                    AjustarProducto ap = new AjustarProducto(idProducto, 2, tipoSeleccion);

                    ap.FormClosed += delegate
                    {
                        if (botonAceptar)
                        {
                            txtBusqueda.Text = string.Empty;
                            var producto = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                            suma = getSuma;
                            resta = getResta;
                            stockAnterior = getStockAnterior;
                            AgregarProductoDGV(producto);
                            botonAceptar = false;
                            idProductoDelCombo.Clear();
                        }
                    };

                    if (idProductoDelCombo.Count > 1)
                    {
                        if (datosSeparados.Length > 1)
                        {
                            ap.cantidadPasadaProductoCombo = (int)Convert.ToDouble(datosSeparados[0].ToString().Trim()) * (int)Convert.ToDouble(idProductoDelCombo[1].ToString());
                        }
                        else if (datosSeparados.Length == 1)
                        {
                            ap.cantidadPasadaProductoCombo = (int)Convert.ToDouble(idProductoDelCombo[1].ToString()) * 1;
                        }
                    }
                    if (idProductoDelCombo.Count == 0)
                    {
                        if (datosSeparados.Length > 1)
                        {
                            ap.cantidadPasadaProductoCombo = (int)Convert.ToDouble(datosSeparados[0].ToString().Trim()) * 1;
                        }
                        else if (datosSeparados.Length == 1)
                        {
                            ap.cantidadPasadaProductoCombo = 0;
                        }
                    }

                    ap.ShowDialog();
                    MessageBox.Show("Test mensaje ap 2");
                }
                else
                {
                    if (datosSeparados.Length > 1)
                    {
                        var resultados = mb.BusquedaCoincidenciasInventario(datosSeparados[1].Trim());
                        int coincidencias = resultados.Count;

                        if (coincidencias > 0)
                        {
                            productos = resultados;

                            listaProductos.Visible = true;
                            listaProductos.Focus();

                            foreach (var item in resultados)
                            {
                                listaProductos.Items.Add(item.Value);
                                listaProductos.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"No se encontraron resultados para \nla búsqueda '{datosSeparados[1].Trim()}'", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else if (datosSeparados.Length == 1)
                    {
                        var resultados = mb.BusquedaCoincidenciasInventario(datosSeparados[0].Trim());
                        int coincidencias = resultados.Count;

                        if (coincidencias > 0)
                        {
                            productos = resultados;

                            listaProductos.Visible = true;
                            listaProductos.Focus();

                            foreach (var item in resultados)
                            {
                                listaProductos.Items.Add(item.Value);
                                listaProductos.SelectedIndex = 0;
                            }
                        }
                        else
                        {
                            MessageBox.Show($"No se encontraron resultados para \nla búsqueda '{datosSeparados[0].Trim()}'", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (opcion4 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            RealizarBusqueda();
            txtBusqueda.Text = string.Empty;

            if (listaProductos.Items.Count > 0)
            {
                listaProductos.Focus();
                listaProductos.SelectedIndex = 0;
            }
        }

        private void ocultarResultados()
        {
            listaProductos.Visible = false;
        }

        private void Inventario_KeyDown(object sender, KeyEventArgs e)
        {
            if (panelContenedor.Visible)
            {
                if (listaProductos.Visible)
                {
                    if (listaProductos.Items.Count == 0)
                    {
                        return;
                    }
                    //Presiono hacia arriba
                    else if (e.KeyCode == Keys.Up)
                    {
                        if (listaProductos.SelectedIndex > 0)
                        {
                            listaProductos.SelectedIndex--;
                            e.Handled = true;
                        }
                        else if (listaProductos.SelectedIndex == 0)
                        {
                            txtBusqueda.Focus();
                        }
                    }
                    //Presiono hacia abajo
                    else if (e.KeyCode == Keys.Down)
                    {
                        listaProductos.Focus();

                        if (listaProductos.SelectedIndex < (listaProductos.Items.Count - 1))
                        {
                            listaProductos.SelectedIndex++;
                            e.Handled = true;
                        }
                    }
                    else if (e.KeyCode == Keys.F2)
                    {
                        Ventas mostrarVentas = new Ventas();
                        mostrarVentas.Show();
                    }
                }
            }
        }

        private void listaProductos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            CargarDatosProducto();
        }

        private void listaProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                CargarDatosProducto();
            }
        }

        private void CargarDatosProducto()
        {
            ocultarResultados();
            txtBusqueda.Text = "";
            txtBusqueda.Focus();

            var productoSeleccionado = listaProductos.Items[listaProductos.SelectedIndex].ToString();
            var idProducto = productos.FirstOrDefault(x => x.Value == productoSeleccionado).Key;

            if (rbAumentarProducto.Checked)
            {
                tipoSeleccion = 1;
            }
            else if (rbDisminuirProducto.Checked)
            {
                tipoSeleccion = 2;
            }

            AjustarProducto ap = new AjustarProducto(idProducto, 2, tipoSeleccion);
            ap.FormClosed += delegate
            {
                if (botonAceptar)
                {
                    var producto = cn.BuscarProducto(idProducto, FormPrincipal.userID);

                    suma = getSuma;
                    resta = getResta;
                    stockAnterior = getStockAnterior;

                    AgregarProductoDGV(producto);
                    botonAceptar = false;
                }
            };
            ap.ShowDialog();
        }

        private void AgregarProductoDGV(string[] producto)
        {
            var id = producto[0];
            var nombre = producto[1];
            var stockActual = Convert.ToString(stockAnterior);
            var diferenciaUnidades = string.Empty;
            var nuevoStock = producto[4];
            var precio = producto[2];
            var clave = producto[6];
            var codigo = producto[7];
            var fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var decrementar = string.Empty;
            decrementar = Convert.ToString(resta);
            var aumentar = string.Empty;
            aumentar = Convert.ToString(suma);

            var NoRev = string.Empty;

            var NombreEmisor = string.Empty;
            var Comentarios = string.Empty;
            var ValorUnitario = string.Empty;

            if (!aumentar.Equals("0"))
            {
                diferenciaUnidades = aumentar;
            }
            else if (!decrementar.Equals("0"))
            {
                diferenciaUnidades = decrementar;
            }

            if (rbAumentarProducto.Checked)
            {
                using (DataTable dtEmisorComentarios = cn.CargarDatos(cs.NomEmisorComentariosHistorialCompras(id)))
                {
                    if (!dtEmisorComentarios.Rows.Count.Equals(0))
                    {
                        foreach (DataRow drEmisorComentario in dtEmisorComentarios.Rows)
                        {
                            NombreEmisor = drEmisorComentario["NomEmisor"].ToString();
                            Comentarios = drEmisorComentario["Comentarios"].ToString();
                            ValorUnitario = drEmisorComentario["ValorUnitario"].ToString();
                        }
                    }
                }

                NoRev = NoRevAumentarInventario();

                var empleadoFinal = string.Empty;
                var idEmplado = cs.buscarIDEmpleado(FormPrincipal.userNickName);
                var separarNameEmpleado = FormPrincipal.userNickName.Split('@');
                if (FormPrincipal.userNickName.Contains("@"))
                {
                    empleadoFinal = separarNameEmpleado[1];
                }
                else
                {
                    empleadoFinal = FormPrincipal.userNickName;
                }
                if (string.IsNullOrEmpty(idEmplado)) { idEmplado = "0"; }

                string[] datosAumentarInventario = { id, nombre, stockActual, diferenciaUnidades, nuevoStock, precio, clave, codigo, fecha, NoRev, "1", NombreEmisor, Comentarios, ValorUnitario, FormPrincipal.userID.ToString(), idEmplado, empleadoFinal };
                var insertAumentarInventario = cs.InsertIntoAumentarInventario(datosAumentarInventario);
                cn.EjecutarConsulta(insertAumentarInventario);
                using (DataTable dtRetriveAumentarInventario = cn.CargarDatos(cs.GetAumentarInventario()))
                {
                    if (!dtRetriveAumentarInventario.Rows.Count.Equals(0))
                    {
                        DGVInventario.Rows.Clear();
                        foreach (DataRow dr in dtRetriveAumentarInventario.Rows)
                        {
                            int rowId = DGVInventario.Rows.Add();

                            DataGridViewRow row = DGVInventario.Rows[rowId];

                            row.Cells["ID"].Value = dr["IdProducto"].ToString();
                            row.Cells["Nombre"].Value = dr["NombreProducto"].ToString();
                            row.Cells["Stock"].Value = dr["StockActual"].ToString();
                            row.Cells["DiferenciaUnidades"].Value = dr["DiferenciaUnidades"].ToString();
                            row.Cells["DiferenciaUnidades"].Style.ForeColor = Color.DodgerBlue;
                            row.Cells["DiferenciaUnidades"].Style.Font = new System.Drawing.Font(DGVInventario.Font, FontStyle.Bold);
                            row.Cells["NuevoStock"].Value = dr["NuevoStock"].ToString();
                            row.Cells["IDTabla"].Value = dr["ID"].ToString();
                            //if (!dr["ValorUnitario"].ToString().Equals("0.00"))
                            //{
                            //    row.Cells["Precio"].Value = dr["ValorUnitario"].ToString();
                            //}
                            //else if (dr["ValorUnitario"].ToString().Equals("0.00"))
                            //{
                            //    row.Cells["Precio"].Value = dr["Precio"].ToString();
                            //}
                            row.Cells["Precio"].Value = dr["Precio"].ToString();
                            row.Cells["Clave"].Value = dr["Clave"].ToString();
                            row.Cells["Codigo"].Value = dr["Codigo"].ToString();
                            row.Cells["Fecha"].Value = dr["Fecha"].ToString();
                            if (!dr["Comentarios"].ToString().Equals(""))
                            {
                                DGVInventario.Columns["Comentarios"].Visible = true;
                                row.Cells["Comentarios"].Value = dr["Comentarios"].ToString();
                            }
                        }
                    }
                }
                DGVInventario.Sort(DGVInventario.Columns["Fecha"], ListSortDirection.Descending);
            }
            else if (rbDisminuirProducto.Checked)
            {
                using (DataTable dtEmisorComentarios = cn.CargarDatos(cs.NomEmisorComentariosHistorialCompras(id)))
                {
                    if (!dtEmisorComentarios.Rows.Count.Equals(0))
                    {
                        foreach (DataRow drEmisorComentario in dtEmisorComentarios.Rows)
                        {
                            NombreEmisor = drEmisorComentario["NomEmisor"].ToString();
                            Comentarios = drEmisorComentario["Comentarios"].ToString();
                            ValorUnitario = drEmisorComentario["ValorUnitario"].ToString();
                        }
                    }
                }

                NoRev = NoRevDisminuirInventario();

                var idEmpleado = cs.buscarIDEmpleado(FormPrincipal.userNickName);
                if (string.IsNullOrEmpty(idEmpleado)) { idEmpleado = "0"; }

                string[] datosDisminuirInventario = { id, nombre, stockActual, diferenciaUnidades, nuevoStock, precio, clave, codigo, fecha, NoRev, "1", NombreEmisor, Comentarios, ValorUnitario, FormPrincipal.userID.ToString(), idEmpleado };

                var insertarDisminuirInventario = cs.InsertarIntoDisminuirInventario(datosDisminuirInventario);

                cn.EjecutarConsulta(insertarDisminuirInventario);

                using (DataTable dtRetriveDisminuirInventario = cn.CargarDatos(cs.GetDisminuirInventario()))
                {
                    if (!dtRetriveDisminuirInventario.Rows.Count.Equals(0))
                    {
                        DGVInventario.Rows.Clear();
                        foreach (DataRow dr in dtRetriveDisminuirInventario.Rows)
                        {
                            int rowId = DGVInventario.Rows.Add();

                            DataGridViewRow row = DGVInventario.Rows[rowId];

                            row.Cells["ID"].Value = dr["IdProducto"].ToString();
                            row.Cells["Nombre"].Value = dr["NombreProducto"].ToString();
                            row.Cells["Stock"].Value = dr["StockActual"].ToString();
                            row.Cells["DiferenciaUnidades"].Value = dr["DiferenciaUnidades"].ToString();
                            row.Cells["DiferenciaUnidades"].Style.ForeColor = Color.OrangeRed;
                            row.Cells["DiferenciaUnidades"].Style.Font = new System.Drawing.Font(DGVInventario.Font, FontStyle.Bold);
                            row.Cells["NuevoStock"].Value = dr["NuevoStock"].ToString();
                            row.Cells["Precio"].Value = dr["Precio"].ToString();
                            row.Cells["Clave"].Value = dr["Clave"].ToString();
                            row.Cells["Codigo"].Value = dr["Codigo"].ToString();
                            row.Cells["Fecha"].Value = dr["Fecha"].ToString();
                            row.Cells["IDTabla"].Value = dr["ID"].ToString();
                            if (!dr["Comentarios"].ToString().Equals(""))
                            {
                                DGVInventario.Columns["Comentarios"].Visible = true;
                                row.Cells["Comentarios"].Value = dr["Comentarios"].ToString();
                            }
                        }
                    }
                }
                DGVInventario.Sort(DGVInventario.Columns["Fecha"], ListSortDirection.Descending);
            }

            DGVInventario.ClearSelection();

            //DGVInventario.Rows[DGVInventario.RowCount - 1].Cells[3].Style.Font = new System.Drawing.Font(DGVInventario.Font, FontStyle.Bold);
            //DGVInventario.Sort(DGVInventario.Columns["Fecha"], ListSortDirection.Descending);

            //if (!aumentar.Equals("0"))
            //{
            //    DGVInventario.Rows[DGVInventario.RowCount - 1].Cells[3].Style.ForeColor = Color.DodgerBlue;
            //}
            //else if (!decrementar.Equals("0"))
            //{
            //    DGVInventario.Rows[DGVInventario.RowCount - 1].Cells[3].Style.ForeColor = Color.OrangeRed;
            //}

            //DGVInventario.Rows[DGVInventario.RowCount - 1].Cells[3].Style.Font = new System.Drawing.Font(DGVInventario.Font, FontStyle.Bold);
            //DGVInventario.Sort(DGVInventario.Columns["Fecha"], ListSortDirection.Descending);
            //DGVInventario.ClearSelection(); 
        }

        private string NoRevAumentarInventario()
        {
            var NumRev = string.Empty;

            NumRev = cs.GetNoRevAumentarInventario();

            if (NumRev.Equals(string.Empty))
            {
                NumRev = "0";
                cn.EjecutarConsulta(cs.InsertIntoNoRevAumentarInventario(NumRev));
            }

            return NumRev;
        }

        private string NoRevDisminuirInventario()
        {
            var NumRev = string.Empty;

            NumRev = cs.GetNoRevDisminuirInventario();

            if (NumRev.Equals(string.Empty))
            {
                NumRev = "0";
                cn.EjecutarConsulta(cs.InsertIntoNoRevDisminuirInvntario(NumRev));
            }

            return NumRev;
        }

        private void bntTerminar_Click(object sender, EventArgs e)
        {
            if (!DGVInventario.Rows.Count.Equals(0))
            {
                if (opcion5 == 0)
                {
                    Utilidades.MensajePermiso();
                    return;
                }

                SeleccionarConceptosReporteActualizarInventario SCRA = new SeleccionarConceptosReporteActualizarInventario();

                SCRA.FormClosed += delegate
                {
                    ConceptosSeleccionados();
                    ValidarParaTerminarRevision();
                };

                SCRA.ShowDialog();

                if (Utilidades.AdobeReaderInstalado())
                {
                    var servidor = Properties.Settings.Default.Hosting;
                    var rutaArchivo = string.Empty;

                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Historial\";
                        if (Directory.Exists(rutaArchivo))
                        {
                            GenerarReporte(idReporte);
                            if (rbAumentarProducto.Checked)
                            {
                                var NewNoRev = Convert.ToInt32(cs.GetNoRevAumentarInventario());
                                cn.EjecutarConsulta(cs.UpdateNoRevAumentarInventario(NewNoRev + 1));
                                cn.EjecutarConsulta(cs.UpdateStatusActualizacionAumentarInventario());
                            }
                            else if (rbDisminuirProducto.Checked)
                            {
                                var NewNoRev = Convert.ToInt32(cs.GetNoRevDisminuirInventario());
                                cn.EjecutarConsulta(cs.UpdateNoRevDisminuirInventario(NewNoRev + 1));
                                cn.EjecutarConsulta(cs.UpdateStatusActualizacionDisminuirInventario());
                            }
                        }
                        else if (!Directory.Exists(rutaArchivo))
                        {
                            MessageBox.Show("Verificar si las carpetas en la MAQUINA SERVIDOR\nestan compartidas para almacenar los archivos", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(servidor))
                    {
                        GenerarReporte(idReporte);
                        if (rbAumentarProducto.Checked)
                        {
                            var NewNoRev = Convert.ToInt32(cs.GetNoRevAumentarInventario());
                            cn.EjecutarConsulta(cs.UpdateNoRevAumentarInventario(NewNoRev + 1));
                            cn.EjecutarConsulta(cs.UpdateStatusActualizacionAumentarInventario());
                        }
                        else if (rbDisminuirProducto.Checked)
                        {
                            var NewNoRev = Convert.ToInt32(cs.GetNoRevDisminuirInventario());
                            cn.EjecutarConsulta(cs.UpdateNoRevDisminuirInventario(NewNoRev + 1));
                            cn.EjecutarConsulta(cs.UpdateStatusActualizacionDisminuirInventario());
                        }
                    }
                }
                else
                {
                    Utilidades.MensajeAdobeReader();
                }

                DGVInventario.Rows.Clear();

                idReporte++;
            }
            else
            {
                MessageBox.Show("No existen ajustes realizados.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ValidarParaTerminarRevision()
        {
            List<int> idObtenidosAumentar = new List<int>();
            List<int> idObtenidosDisminuir = new List<int>();

            string tablaAumentar = "dgvaumentarinventario";
            string tablaDisminuir = "dgvdisminuirinventario"; 

            if (rbAumentarProducto.Checked)
            {
                var numFolio = ObtenerUltimoFolio(tablaAumentar);
                numFolio += 1;

                foreach (DataGridViewRow dgv in DGVInventario.Rows)
                {
                    idObtenidosAumentar.Add(Convert.ToInt32(dgv.Cells[10].Value));
                }

                var codigosBuscar = RecorrerLista(idObtenidosAumentar); 
                cn.EjecutarConsulta($"UPDATE {tablaAumentar} SET Folio = '{numFolio}' WHERE IDUsuario = '{FormPrincipal.userID}' AND ID IN ({codigosBuscar}) OR Folio = 0");
            }
            else if (rbDisminuirProducto.Checked)
            {
                var numFolio = ObtenerUltimoFolio(tablaDisminuir);
                numFolio += 1;

                foreach (DataGridViewRow dgv in DGVInventario.Rows)
                {
                    idObtenidosDisminuir.Add(Convert.ToInt32(dgv.Cells[10].Value));
                }

                var codigosBuscar = RecorrerLista(idObtenidosDisminuir);
                cn.EjecutarConsulta($"UPDATE {tablaDisminuir} SET Folio = '{numFolio}' WHERE IDUsuario = '{FormPrincipal.userID}' AND ID IN ({codigosBuscar})");
            }
        }

        private string RecorrerLista(List<int> tipoLista)
        {
            var result = string.Empty;

            foreach (var lista in tipoLista)
            {
                result += $"{lista},";
            }

            result = result.TrimEnd(',');

            return result;
        }

        private int ObtenerUltimoFolio(string tabla)
        {
            int result = 0;

            var query = cn.CargarDatos($"SELECT Folio FROM {tabla} WHERE IDUsuario = '{FormPrincipal.userID}' ORDER BY Folio DESC LIMIT 2");

            if (!query.Rows.Count.Equals(0))    
            {
                //if (query.Rows.Count.Equals(1))
                //{
                //    result = Convert.ToInt32(query.Rows[0]["Folio"].ToString());
                //}
                //else
                //{
                //    result = Convert.ToInt32(query.Rows[1]["Folio"].ToString());
                //}
                result = Convert.ToInt32(query.Rows[0]["Folio"].ToString());
            }

            return result;
        }

        private void GenerarReporte(int idReporte, int tipo = 0)
        {
            int anchoLogo = 110;
            int altoLogo = 60;
            int posicionDeColumnas = 1;
            int posisionColumna = 1;
            float unitsBoughtDiminished = 0,
                  boughtPrice = 0,
                  salesPrice = 0,
                  lastStock = 0,
                  currentStock = 0;
            var datos = FormPrincipal.datosUsuario;
            var colorFuenteNegrita = new BaseColor(Color.Black);
            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;
            var numRow = 0;
            var diccionarioConDatosStaticosDinamicos = new Dictionary<Tuple<string, string>, float>();
            var columnasDinamicas = 0;
            string[] ValueConceptos = { };
            float[] anchoColumnas;
            var position = 0;

            int i = 0;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo += $@"\\{servidor}\Archivos PUDVE\Reportes\Historial\{FormPrincipal.userNickName}\";
            }
            else
            {
                rutaArchivo = $@"C:\Archivos PUDVE\Reportes\Historial\{FormPrincipal.userNickName}\";
            }

            if (rbAumentarProducto.Checked)
            {
                rutaArchivo += $@"AumentarInventario\";
            }
            else if (rbDisminuirProducto.Checked)
            {
                rutaArchivo += $@"DisminuirInventario\";
            }
            else
            {
                rutaArchivo += $@"ActualizarInvetario\";
            }

            var checarNoRev = Convert.ToInt32(cs.GetNoRevAumentarInventario());
            var numeroDeFolio = cn.ObtenerUltimoIdReporte(FormPrincipal.userID);

            if (!Directory.Exists(rutaArchivo))
            {
                Directory.CreateDirectory(rutaArchivo);
                rutaArchivo += $"reporte_actualizar_inventario_NoRevision{checarNoRev}_NoFolio{numeroDeFolio}.pdf";
            }
            else
            {
                rutaArchivo += $"reporte_actualizar_inventario_NoRevision{checarNoRev}_NoFolio{numeroDeFolio}.pdf";
            }

            #region aumentarDisminurInventario
            if (panelContenedor.Visible)
            {
                if (rbAumentarProducto.Checked)
                {
                    var NoRev = Convert.ToInt32(cs.GetNoRevAumentarInventario());

                    using (DataTable dtAumentarInventario = cn.CargarDatos(cs.SearchDGVAumentarInventario(NoRev)))
                    {
                        if (!dtAumentarInventario.Rows.Count.Equals(0))
                        {
                            var numeroColumnasDinamicas = dtAumentarInventario.Columns.Count;
                            string[] columnasReporte = { };
                            var incremento = 1;

                            columnasDinamicas = numeroColumnasDinamicas;

                            foreach (DataColumn item in dtAumentarInventario.Columns)
                            {
                                var concepto = item.ToString();

                                if (concepto.Equals("No"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "30"), 30f);
                                }
                                else if (concepto.Equals("Producto"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "245"), 245f);
                                }
                                else if (concepto.Equals("Proveedor"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "200"), 200f);
                                }
                                else if (concepto.Equals("Unidades_Compradas"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "80"), 80f);
                                }
                                else if (concepto.Equals("Precio_Compra"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "70"), 70f);
                                }
                                else if (concepto.Equals("Precio_Venta"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "70"), 70f);
                                }
                                else if (concepto.Equals("Stock_Anterior"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "55"), 55f);
                                }
                                else if (concepto.Equals("Stock_Actual"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "55"), 55f);
                                }
                                else if (concepto.Equals("Fecha_Compra"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "80"), 80f);
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>("Fecha_Operacion", "80"), 80f);
                                }
                                else if (concepto.Equals("Comentarios"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "100"), 100f);
                                }
                                else if (!concepto.Equals("id") &&
                                         !concepto.Equals("No") &&
                                         !concepto.Equals("Producto") &&
                                         !concepto.Equals("Proveedor") &&
                                         !concepto.Equals("Unidades_Compradas") &&
                                         !concepto.Equals("Precio_Compra") &&
                                         !concepto.Equals("Precio_Venta") &&
                                         !concepto.Equals("Stock_Anterior") &&
                                         !concepto.Equals("Stock_Actual") &&
                                         !concepto.Equals("Fecha_Compra") &&
                                         !concepto.Equals("Comentarios"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "100"), 100f);
                                }
                            }
                        }
                    }
                }
                else if (rbDisminuirProducto.Checked)
                {
                    var NoRev = Convert.ToInt32(cs.GetNoRevDisminuirInventario());

                    using (DataTable dtDisminuirInventario = cn.CargarDatos(cs.SearchDGVDisminuirInventario(NoRev)))
                    {
                        if (!dtDisminuirInventario.Rows.Count.Equals(0))
                        {
                            var numeroColumnasDinamicas = dtDisminuirInventario.Columns.Count;
                            string[] columnasReporte = { };
                            var incremento = 1;

                            columnasDinamicas = numeroColumnasDinamicas;

                            foreach (DataColumn item in dtDisminuirInventario.Columns)
                            {
                                var concepto = item.ToString();

                                if (concepto.Equals("No"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "30"), 30f);
                                }
                                else if (concepto.Equals("Producto"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "245"), 245f);
                                }
                                else if (concepto.Equals("Proveedor"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "200"), 200f);
                                }
                                else if (concepto.Equals("Unidades_Compradas"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "80"), 80f);
                                }
                                else if (concepto.Equals("Precio_Compra"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "70"), 70f);
                                }
                                else if (concepto.Equals("Precio_Venta"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "70"), 70f);
                                }
                                else if (concepto.Equals("Stock_Anterior"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "55"), 55f);
                                }
                                else if (concepto.Equals("Stock_Actual"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "55"), 55f);
                                }
                                else if (concepto.Equals("Fecha_Compra"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "80"), 80f);
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>("Fecha_Operacion", "80"), 80f);
                                }
                                else if (concepto.Equals("Comentarios"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "100"), 100f);
                                }
                                else if (!concepto.Equals("id") &&
                                         !concepto.Equals("No") &&
                                         !concepto.Equals("Producto") &&
                                         !concepto.Equals("Proveedor") &&
                                         !concepto.Equals("Unidades_Compradas") &&
                                         !concepto.Equals("Precio_Compra") &&
                                         !concepto.Equals("Precio_Venta") &&
                                         !concepto.Equals("Stock_Anterior") &&
                                         !concepto.Equals("Stock_Actual") &&
                                         !concepto.Equals("Fecha_Compra") &&
                                         !concepto.Equals("Comentarios"))
                                {
                                    diccionarioConDatosStaticosDinamicos.Add(new Tuple<string, string>(concepto, "100"), 100f);
                                }
                            }
                        }
                    }
                }

                // se agrego una columna nueva al reporte la de stock anterior ahora son 9 Columnas
                // Producto = 245f,       Proveedor = 200f,     Unidades Compradas = 80f,     Precio compra = 70f,      Precio venta = 70f,
                // Stock anterior = 55f   Stock actual = 55f,   Fecha de compra = 80f,        Fecha de operación = 80f  Comentarios = 200f
                // float[] anchoColumnas = new float[] { 245f, 200f, 80f, 70f, 70f, 55f, 55f, 80f, 95f, 200f };
                //string[] ValueConceptos = new string[] {  "30", "245", "200", "80", "70", "70", "55", "55", "80", "80", "200" };

                ValueConceptos = new string[columnasDinamicas];
                anchoColumnas = new float[columnasDinamicas];

                foreach (var item in diccionarioConDatosStaticosDinamicos)
                {
                    var concepto = item.Key.Item2.ToString();
                    var ancho = item.Value.ToString();
                    var valueAncho = (float)Convert.ToDouble(ancho);
                    ValueConceptos[i] = concepto;
                    anchoColumnas[i] = valueAncho;
                    i++;
                }

                #region Sección Botón Actualizar Invetario
                if (tipo.Equals(0))
                {
                    #region Conceptos completos y Datos Dinamicos
                    if (columnasConcepto >= 10)
                    {
                        Document reporte = new Document(PageSize.A3.Rotate());
                        PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));
                        string logotipo = datos[11];
                        reporte.Open();

                        // Validación para verificar si existe logotipo
                        if (logotipo != "")
                        {
                            logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + logotipo;
                            if (File.Exists(logotipo))
                            {
                                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logotipo);
                                logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                                logo.ScaleAbsolute(anchoLogo, altoLogo);
                                reporte.Add(logo);
                            }
                        }

                        Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
                        Paragraph Usuario = new Paragraph("");
                        Paragraph subTitulo = new Paragraph("");

                        string UsuarioActivo = string.Empty;

                        using (DataTable dtDataUsr = cn.CargarDatos(cs.UsuarioRazonSocialNombreCompleto(Convert.ToString(FormPrincipal.userID))))
                        {
                            if (!dtDataUsr.Rows.Count.Equals(0))
                            {
                                foreach (DataRow drDataUsr in dtDataUsr.Rows)
                                {
                                    UsuarioActivo = drDataUsr["Usuario"].ToString();
                                }
                            }
                        }

                        Usuario = new Paragraph("USUARIO: " + UsuarioActivo, fuenteNegrita);

                        if (rbAumentarProducto.Checked)
                        {
                            subTitulo = new Paragraph("REPORTE DE ACTUALIZAR INVENTARIO\nSECCIÓN DE AUMENTAR\n\nFecha: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);
                        }
                        else if (rbDisminuirProducto.Checked)
                        {
                            subTitulo = new Paragraph("REPORTE DE ACTUALIZAR INVENTARIO\nSECCIÓN DE DISMINUIR\n\nFecha: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);
                        }

                        titulo.Alignment = Element.ALIGN_CENTER;
                        Usuario.Alignment = Element.ALIGN_CENTER;
                        subTitulo.Alignment = Element.ALIGN_CENTER;

                        PdfPTable tabla = new PdfPTable(anchoColumnas.Count());
                        tabla.WidthPercentage = 100;
                        tabla.SetWidths(anchoColumnas);

                        PdfPCell colNoConcepto = new PdfPCell(new Phrase("No:", fuenteNegrita));
                        colNoConcepto.BorderWidth = 1;
                        colNoConcepto.BackgroundColor = new BaseColor(Color.SkyBlue);
                        colNoConcepto.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colProducto = new PdfPCell(new Phrase("Producto", fuenteNegrita));
                        colProducto.BorderWidth = 1;
                        colProducto.BackgroundColor = new BaseColor(Color.SkyBlue);
                        colProducto.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colProveedor = new PdfPCell(new Phrase("Proveedor", fuenteNegrita));
                        colProveedor.BorderWidth = 1;
                        colProveedor.BackgroundColor = new BaseColor(Color.SkyBlue);
                        colProveedor.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colUnidades = new PdfPCell(new Phrase("", fuenteNegrita));

                        if (rbAumentarProducto.Checked)
                        {
                            colUnidades = new PdfPCell(new Phrase("Unidades compradas", fuenteNegrita));
                            colUnidades.BorderWidth = 1;
                            colUnidades.BackgroundColor = new BaseColor(Color.SkyBlue);
                            colUnidades.HorizontalAlignment = Element.ALIGN_CENTER;
                        }
                        else if (rbDisminuirProducto.Checked)
                        {
                            colUnidades = new PdfPCell(new Phrase("Unidades disminuidas", fuenteNegrita));
                            colUnidades.BorderWidth = 1;
                            colUnidades.BackgroundColor = new BaseColor(Color.SkyBlue);
                            colUnidades.HorizontalAlignment = Element.ALIGN_CENTER;
                        }


                        PdfPCell colPrecioCompra = new PdfPCell(new Phrase("Precio compra", fuenteNegrita));
                        colPrecioCompra.BorderWidth = 1;
                        colPrecioCompra.BackgroundColor = new BaseColor(Color.SkyBlue);
                        colPrecioCompra.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colPrecioVenta = new PdfPCell(new Phrase("Precio venta", fuenteNegrita));
                        colPrecioVenta.BorderWidth = 1;
                        colPrecioVenta.BackgroundColor = new BaseColor(Color.SkyBlue);
                        colPrecioVenta.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colStockAnterior = new PdfPCell(new Phrase("Stock anterior", fuenteNegrita));
                        colStockAnterior.BorderWidth = 1;
                        colStockAnterior.BackgroundColor = new BaseColor(Color.SkyBlue);
                        colStockAnterior.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colStock = new PdfPCell(new Phrase("Stock actual", fuenteNegrita));
                        colStock.BorderWidth = 1;
                        colStock.BackgroundColor = new BaseColor(Color.SkyBlue);
                        colStock.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colFechaCompra = new PdfPCell(new Phrase("Fecha de compra", fuenteNegrita));
                        colFechaCompra.BorderWidth = 1;
                        colFechaCompra.BackgroundColor = new BaseColor(Color.SkyBlue);
                        colFechaCompra.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colFechaOperacion = new PdfPCell(new Phrase("Fecha de operación", fuenteNegrita));
                        colFechaOperacion.BorderWidth = 1;
                        colFechaOperacion.BackgroundColor = new BaseColor(Color.SkyBlue);
                        colFechaOperacion.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colComentarios = new PdfPCell(new Phrase("Comentarios", fuenteNegrita));
                        colComentarios.BorderWidth = 1;
                        colComentarios.BackgroundColor = new BaseColor(Color.SkyBlue);
                        colComentarios.HorizontalAlignment = Element.ALIGN_CENTER;

                        tabla.AddCell(colNoConcepto);
                        tabla.AddCell(colProducto);
                        tabla.AddCell(colProveedor);
                        tabla.AddCell(colUnidades);
                        tabla.AddCell(colPrecioCompra);
                        tabla.AddCell(colPrecioVenta);
                        tabla.AddCell(colStockAnterior);
                        tabla.AddCell(colStock);
                        tabla.AddCell(colFechaCompra);
                        tabla.AddCell(colFechaOperacion);
                        tabla.AddCell(colComentarios);

                        using (DataTable dtConceptosDinamicosActivos = cn.CargarDatos(cs.verConceptosDinamicosActivos()))
                        {
                            if (!dtConceptosDinamicosActivos.Rows.Count.Equals(0))
                            {
                                foreach (DataRow item in dtConceptosDinamicosActivos.Rows)
                                {
                                    var columnaDinamica = item["concepto"].ToString().Replace("_", " ");
                                    tabla.AddCell
                                    (
                                        new PdfPCell
                                        (
                                            new Phrase(columnaDinamica, fuenteNegrita)
                                        )
                                        {
                                            BorderWidth = 1,
                                            BackgroundColor = new BaseColor(Color.SkyBlue),
                                            HorizontalAlignment = Element.ALIGN_CENTER
                                        }
                                    );
                                }
                            }
                        }

                        //Consulta para obtener los registros del Historial de compras
                        MySqlConnection sql_con;
                        MySqlCommand sql_cmd;
                        MySqlDataReader dr;

                        if (!string.IsNullOrWhiteSpace(servidor))
                        {
                            sql_con = new MySqlConnection("datasource=" + servidor + ";port=6666;username=root;password=;database=pudve;");
                        }
                        else
                        {
                            sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");

                        }

                        sql_con.Open();
                        //sql_cmd = new MySqlCommand($"SELECT * FROM HistorialCompras WHERE IDUsuario = {FormPrincipal.userID} AND IDReporte = {idReporte}", sql_con);

                        #region Sección Aumentar Invetario
                        if (rbAumentarProducto.Checked)
                        {
                            var NoRev = Convert.ToInt32(cs.GetNoRevAumentarInventario());
                            sql_cmd = new MySqlCommand(cs.SearchDGVAumentarInventario(NoRev), sql_con);
                            dr = sql_cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                posisionColumna = 1;

                                var idProducto = Convert.ToInt32(dr[posisionColumna].ToString());
                                posisionColumna++;
                                var producto = dr[posisionColumna].ToString();
                                posisionColumna++;
                                var proveedor = dr[posisionColumna].ToString();
                                posisionColumna++;
                                var unidades = string.Empty;

                                if (dr[posisionColumna].ToString().Equals(string.Empty))
                                {
                                    unidades = "0.00";
                                    posisionColumna++;
                                }
                                else if (!dr[posisionColumna].ToString().Equals(string.Empty))
                                {
                                    unidades = dr[posisionColumna].ToString();
                                    posisionColumna++;
                                }

                                var compra = Convert.ToDouble(dr[posisionColumna].ToString()).ToString("0.00");
                                posisionColumna++;
                                var venta = Convert.ToDouble(dr[posisionColumna].ToString()).ToString("0.00");
                                posisionColumna++;

                                var tmp = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                                //var stock = tmp[4];
                                var stock = Convert.ToDouble(dr[posisionColumna].ToString()).ToString("0.00");
                                posisionColumna++;

                                //var stockAnterior = (Convert.ToDouble(stock) - Convert.ToDouble(unidades)).ToString("0.00");
                                //var stockAnterior = (Convert.ToDouble(stock) + Convert.ToDouble(unidades)).ToString("0.00");
                                var stockAnterior = Convert.ToDouble(dr[posisionColumna].ToString()).ToString("0.00");
                                posisionColumna++;

                                DateTime fecha = (DateTime)dr[posisionColumna];
                                var fechaCompra = fecha.ToString("yyyy-MM-dd");

                                DateTime fechaOp = (DateTime)dr[posisionColumna];
                                var fechaOperacion = fechaOp.ToString("yyyy-MM-dd HH:mm tt");
                                posisionColumna++;

                                var comentarios = dr[posisionColumna].ToString();
                                posisionColumna++;
                                posicionDeColumnas = posisionColumna;

                                numRow++;

                                PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                                colNoConceptoTmp.BorderWidth = 1;
                                colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colProductoTmp;
                                if (!producto.Equals(string.Empty))
                                {
                                    colProductoTmp = new PdfPCell(new Phrase(producto, fuenteNormal));
                                }
                                else
                                {
                                    colProductoTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colProductoTmp.BorderWidth = 1;
                                colProductoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colProveedorTmp;
                                if (!proveedor.Equals(string.Empty))
                                {
                                    colProveedorTmp = new PdfPCell(new Phrase(proveedor, fuenteNormal));
                                }
                                else
                                {
                                    colProveedorTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colProveedorTmp.BorderWidth = 1;
                                colProveedorTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colUnidadesTmp;
                                if (!unidades.Equals(string.Empty))
                                {
                                    colUnidadesTmp = new PdfPCell(new Phrase(unidades, fuenteNormal));
                                    unitsBoughtDiminished += (float)Convert.ToDouble(unidades);
                                }
                                else
                                {
                                    colUnidadesTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colUnidadesTmp.BorderWidth = 1;
                                colUnidadesTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colPrecioCompraTmp;
                                if (!compra.Equals(string.Empty))
                                {
                                    colPrecioCompraTmp = new PdfPCell(new Phrase("$" + compra, fuenteNormal));
                                    boughtPrice += (float)Convert.ToDouble(compra);
                                }
                                else
                                {
                                    colPrecioCompraTmp = new PdfPCell(new Phrase("$ ---", fuenteNormal));
                                }
                                colPrecioCompraTmp.BorderWidth = 1;
                                colPrecioCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colPrecioVentaTmp;
                                if (!venta.Equals(string.Empty))
                                {
                                    colPrecioVentaTmp = new PdfPCell(new Phrase("$" + venta, fuenteNormal));
                                    salesPrice += (float)Convert.ToDouble(venta);
                                }
                                else
                                {
                                    colPrecioVentaTmp = new PdfPCell(new Phrase("$ ---", fuenteNormal));
                                }
                                colPrecioVentaTmp.BorderWidth = 1;
                                colPrecioVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colStockTmpAnterior;
                                if (!stockAnterior.Equals(string.Empty))
                                {
                                    colStockTmpAnterior = new PdfPCell(new Phrase(stockAnterior, fuenteNormal));
                                    lastStock += (float)Convert.ToDouble(stockAnterior);
                                }
                                else
                                {
                                    colStockTmpAnterior = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colStockTmpAnterior.BorderWidth = 1;
                                colStockTmpAnterior.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colStockTmp;
                                if (!stock.Equals(string.Empty))
                                {
                                    colStockTmp = new PdfPCell(new Phrase(stock, fuenteNormal));
                                    currentStock += (float)Convert.ToDouble(stock);
                                }
                                else
                                {
                                    colStockTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colStockTmp.BorderWidth = 1;
                                colStockTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colFechaCompraTmp;
                                if (!fechaCompra.Equals(string.Empty))
                                {
                                    colFechaCompraTmp = new PdfPCell(new Phrase(fechaCompra, fuenteNormal));
                                }
                                else
                                {
                                    colFechaCompraTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colFechaCompraTmp.BorderWidth = 1;
                                colFechaCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colFechaOperacionTmp;
                                if (!fechaOperacion.Equals(string.Empty))
                                {
                                    colFechaOperacionTmp = new PdfPCell(new Phrase(fechaOperacion, fuenteNormal));
                                }
                                else
                                {
                                    colFechaOperacionTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colFechaOperacionTmp.BorderWidth = 1;
                                colFechaOperacionTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colComentariosTmp;
                                if (!comentarios.Equals(string.Empty))
                                {
                                    colComentariosTmp = new PdfPCell(new Phrase(comentarios, fuenteNormal));
                                }
                                else
                                {
                                    colComentariosTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colComentariosTmp.BorderWidth = 1;
                                colComentariosTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                tabla.AddCell(colNoConceptoTmp);
                                tabla.AddCell(colProductoTmp);
                                tabla.AddCell(colProveedorTmp);
                                tabla.AddCell(colUnidadesTmp);
                                tabla.AddCell(colPrecioCompraTmp);
                                tabla.AddCell(colPrecioVentaTmp);
                                tabla.AddCell(colStockTmpAnterior);
                                tabla.AddCell(colStockTmp);
                                tabla.AddCell(colFechaCompraTmp);
                                tabla.AddCell(colFechaOperacionTmp);
                                tabla.AddCell(colComentariosTmp);

                                for (int j = posisionColumna; j < columnasDinamicas; j++)
                                {
                                    var datoDinamico = string.Empty;

                                    if (!dr[j].ToString().Equals(string.Empty))
                                    {
                                        datoDinamico = dr[j].ToString();
                                    }
                                    else
                                    {
                                        datoDinamico = "N/A";
                                    }

                                    tabla.AddCell
                                    (
                                        new PdfPCell
                                        (
                                            new Phrase(datoDinamico.Replace("_", " "), fuenteNormal)
                                        )
                                        {
                                            BorderWidth = 1,
                                            HorizontalAlignment = Element.ALIGN_CENTER
                                        }
                                    );
                                }
                            }

                            if (unitsBoughtDiminished > 0 || boughtPrice > 0)
                            {
                                PdfPCell colNoConceptoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colNoConceptoTmpExtra.BorderWidth = 0;
                                colNoConceptoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colProductoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colProductoTmpExtra.BorderWidth = 0;
                                colProductoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colProveedorTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colProveedorTmpExtra.BorderWidth = 0;
                                colProveedorTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colUnidadesTmpExtra = new PdfPCell(new Phrase(unitsBoughtDiminished.ToString("N2"), fuenteNormal));
                                colUnidadesTmpExtra.BorderWidthTop = 0;
                                colUnidadesTmpExtra.BorderWidthLeft = 0;
                                colUnidadesTmpExtra.BorderWidthRight = 0;
                                colUnidadesTmpExtra.BorderWidthBottom = 1;
                                colUnidadesTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colUnidadesTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colPrecioCompraTmpExtra = new PdfPCell(new Phrase(boughtPrice.ToString("C"), fuenteNormal));
                                colPrecioCompraTmpExtra.BorderWidthTop = 0;
                                colPrecioCompraTmpExtra.BorderWidthLeft = 0;
                                colPrecioCompraTmpExtra.BorderWidthRight = 0;
                                colPrecioCompraTmpExtra.BorderWidthBottom = 1;
                                colPrecioCompraTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colPrecioCompraTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colPrecioVentaTmpExtra = new PdfPCell(new Phrase(salesPrice.ToString("C"), fuenteNormal));
                                colPrecioVentaTmpExtra.BorderWidthTop = 0;
                                colPrecioVentaTmpExtra.BorderWidthLeft = 0;
                                colPrecioVentaTmpExtra.BorderWidthRight = 0;
                                colPrecioVentaTmpExtra.BorderWidthBottom = 1;
                                colPrecioVentaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colPrecioVentaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colStockTmpAnteriorExtra = new PdfPCell(new Phrase(lastStock.ToString("N2"), fuenteNormal));
                                colStockTmpAnteriorExtra.BorderWidthTop = 0;
                                colStockTmpAnteriorExtra.BorderWidthLeft = 0;
                                colStockTmpAnteriorExtra.BorderWidthRight = 0;
                                colStockTmpAnteriorExtra.BorderWidthBottom = 1;
                                colStockTmpAnteriorExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colStockTmpAnteriorExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colStockTmpExtra = new PdfPCell(new Phrase(currentStock.ToString("N2"), fuenteNormal));
                                colStockTmpExtra.BorderWidthTop = 0;
                                colStockTmpExtra.BorderWidthLeft = 0;
                                colStockTmpExtra.BorderWidthRight = 0;
                                colStockTmpExtra.BorderWidthBottom = 1;
                                colStockTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colStockTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colFechaCompraTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colFechaCompraTmpExtra.BorderWidth = 0;
                                colFechaCompraTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colFechaOperacionTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colFechaOperacionTmpExtra.BorderWidth = 0;
                                colFechaOperacionTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colComentariosTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colComentariosTmpExtra.BorderWidth = 0;
                                colComentariosTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                tabla.AddCell(colNoConceptoTmpExtra);
                                tabla.AddCell(colProductoTmpExtra);
                                tabla.AddCell(colProveedorTmpExtra);
                                tabla.AddCell(colUnidadesTmpExtra);
                                tabla.AddCell(colPrecioCompraTmpExtra);
                                tabla.AddCell(colPrecioVentaTmpExtra);
                                tabla.AddCell(colStockTmpAnteriorExtra);
                                tabla.AddCell(colStockTmpExtra);
                                tabla.AddCell(colFechaCompraTmpExtra);
                                tabla.AddCell(colFechaOperacionTmpExtra);
                                tabla.AddCell(colComentariosTmpExtra);

                                for (int j = posicionDeColumnas; j < columnasDinamicas; j++)
                                {
                                    tabla.AddCell
                                    (
                                        new PdfPCell
                                        (
                                            new Phrase(string.Empty, fuenteNormal)
                                        )
                                        {
                                            BorderWidth = 0,
                                            HorizontalAlignment = Element.ALIGN_CENTER
                                        }
                                    );
                                }
                            }
                            diccionarioConDatosStaticosDinamicos.Clear();
                        }
                        #endregion
                        #region Sección Disminuir Invetario
                        else if (rbDisminuirProducto.Checked)
                        {
                            var NoRev = Convert.ToInt32(cs.GetNoRevDisminuirInventario());

                            sql_cmd = new MySqlCommand(cs.SearchDGVDisminuirInventario(NoRev), sql_con);

                            dr = sql_cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                posisionColumna = 1;

                                var idProducto = Convert.ToInt32(dr[posisionColumna].ToString());
                                posisionColumna++;
                                var producto = dr[posisionColumna].ToString();
                                posisionColumna++;
                                var proveedor = dr[posisionColumna].ToString();
                                posisionColumna++;
                                var unidades = string.Empty;

                                if (dr[posisionColumna].ToString().Equals(string.Empty))
                                {
                                    unidades = "0.00";
                                    posisionColumna++;
                                }
                                else if (!dr[posisionColumna].ToString().Equals(string.Empty))
                                {
                                    unidades = dr[posisionColumna].ToString();
                                    posisionColumna++;
                                }

                                var compra = Convert.ToDouble(dr[posisionColumna].ToString()).ToString("0.00");
                                posisionColumna++;
                                var venta = Convert.ToDouble(dr[posisionColumna].ToString()).ToString("0.00");
                                posisionColumna++;

                                var tmp = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                                //var stock = tmp[4];
                                var stock = Convert.ToDouble(dr[posisionColumna].ToString()).ToString("0.00");
                                posisionColumna++;

                                //var stockAnterior = (Convert.ToDouble(stock) - Convert.ToDouble(unidades)).ToString("0.00");
                                //var stockAnterior = (Convert.ToDouble(stock) + Convert.ToDouble(unidades)).ToString("0.00");
                                var stockAnterior = Convert.ToDouble(dr[posisionColumna].ToString()).ToString("0.00");
                                posisionColumna++;

                                DateTime fecha = (DateTime)dr[posisionColumna];
                                var fechaCompra = fecha.ToString("yyyy-MM-dd");

                                DateTime fechaOp = (DateTime)dr[posisionColumna];
                                var fechaOperacion = fechaOp.ToString("yyyy-MM-dd HH:mm tt");
                                posisionColumna++;

                                var comentarios = dr[posisionColumna].ToString();
                                posisionColumna++;
                                posicionDeColumnas = posisionColumna;

                                numRow++;

                                PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                                colNoConceptoTmp.BorderWidth = 1;
                                colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colProductoTmp;
                                if (!producto.Equals(string.Empty))
                                {
                                    colProductoTmp = new PdfPCell(new Phrase(producto, fuenteNormal));
                                }
                                else
                                {
                                    colProductoTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colProductoTmp.BorderWidth = 1;
                                colProductoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colProveedorTmp;
                                if (!proveedor.Equals(string.Empty))
                                {
                                    colProveedorTmp = new PdfPCell(new Phrase(proveedor, fuenteNormal));
                                }
                                else
                                {
                                    colProveedorTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colProveedorTmp.BorderWidth = 1;
                                colProveedorTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colUnidadesTmp;
                                if (!unidades.Equals(string.Empty))
                                {
                                    colUnidadesTmp = new PdfPCell(new Phrase(unidades, fuenteNormal));
                                    unitsBoughtDiminished += (float)Convert.ToDouble(unidades);
                                }
                                else
                                {
                                    colUnidadesTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colUnidadesTmp.BorderWidth = 1;
                                colUnidadesTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colPrecioCompraTmp;
                                if (!compra.Equals(string.Empty))
                                {
                                    colPrecioCompraTmp = new PdfPCell(new Phrase("$" + compra, fuenteNormal));
                                    boughtPrice += (float)Convert.ToDouble(compra);
                                }
                                else
                                {
                                    colPrecioCompraTmp = new PdfPCell(new Phrase("$ ---", fuenteNormal));
                                }
                                colPrecioCompraTmp.BorderWidth = 1;
                                colPrecioCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colPrecioVentaTmp;
                                if (!venta.Equals(string.Empty))
                                {
                                    colPrecioVentaTmp = new PdfPCell(new Phrase("$" + venta, fuenteNormal));
                                    salesPrice += (float)Convert.ToDouble(venta);
                                }
                                else
                                {
                                    colPrecioVentaTmp = new PdfPCell(new Phrase("$ ---", fuenteNormal));
                                }
                                colPrecioVentaTmp.BorderWidth = 1;
                                colPrecioVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colStockTmpAnterior;
                                if (!stockAnterior.Equals(string.Empty))
                                {
                                    colStockTmpAnterior = new PdfPCell(new Phrase(stockAnterior, fuenteNormal));
                                    lastStock += (float)Convert.ToDouble(stockAnterior);
                                }
                                else
                                {
                                    colStockTmpAnterior = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colStockTmpAnterior.BorderWidth = 1;
                                colStockTmpAnterior.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colStockTmp;
                                if (!stock.Equals(string.Empty))
                                {
                                    colStockTmp = new PdfPCell(new Phrase(stock, fuenteNormal));
                                    currentStock += (float)Convert.ToDouble(stock);
                                }
                                else
                                {
                                    colStockTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colStockTmp.BorderWidth = 1;
                                colStockTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colFechaCompraTmp;
                                if (!fechaCompra.Equals(string.Empty))
                                {
                                    colFechaCompraTmp = new PdfPCell(new Phrase(fechaCompra, fuenteNormal));
                                }
                                else
                                {
                                    colFechaCompraTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colFechaCompraTmp.BorderWidth = 1;
                                colFechaCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colFechaOperacionTmp;
                                if (!fechaOperacion.Equals(string.Empty))
                                {
                                    colFechaOperacionTmp = new PdfPCell(new Phrase(fechaOperacion, fuenteNormal));
                                }
                                else
                                {
                                    colFechaOperacionTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colFechaOperacionTmp.BorderWidth = 1;
                                colFechaOperacionTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colComentariosTmp;
                                if (!comentarios.Equals(string.Empty))
                                {
                                    colComentariosTmp = new PdfPCell(new Phrase(comentarios, fuenteNormal));
                                }
                                else
                                {
                                    colComentariosTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                                }
                                colComentariosTmp.BorderWidth = 1;
                                colComentariosTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                                tabla.AddCell(colNoConceptoTmp);
                                tabla.AddCell(colProductoTmp);
                                tabla.AddCell(colProveedorTmp);
                                tabla.AddCell(colUnidadesTmp);
                                tabla.AddCell(colPrecioCompraTmp);
                                tabla.AddCell(colPrecioVentaTmp);
                                tabla.AddCell(colStockTmpAnterior);
                                tabla.AddCell(colStockTmp);
                                tabla.AddCell(colFechaCompraTmp);
                                tabla.AddCell(colFechaOperacionTmp);
                                tabla.AddCell(colComentariosTmp);

                                for (int j = posisionColumna; j < columnasDinamicas; j++)
                                {
                                    var datoDinamico = string.Empty;

                                    if (!dr[j].ToString().Equals(string.Empty))
                                    {
                                        datoDinamico = dr[j].ToString();
                                    }
                                    else
                                    {
                                        datoDinamico = "N/A";
                                    }

                                    tabla.AddCell
                                    (
                                        new PdfPCell
                                        (
                                            new Phrase(datoDinamico.Replace("_", " "), fuenteNormal)
                                        )
                                        {
                                            BorderWidth = 1,
                                            HorizontalAlignment = Element.ALIGN_CENTER
                                        }
                                    );
                                }
                                diccionarioConDatosStaticosDinamicos.Clear();
                            }

                            if (unitsBoughtDiminished > 0 || boughtPrice > 0)
                            {
                                PdfPCell colNoConceptoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colNoConceptoTmpExtra.BorderWidth = 0;
                                colNoConceptoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colProductoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colProductoTmpExtra.BorderWidth = 0;
                                colProductoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colProveedorTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colProveedorTmpExtra.BorderWidth = 0;
                                colProveedorTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colUnidadesTmpExtra = new PdfPCell(new Phrase(unitsBoughtDiminished.ToString("N2"), fuenteNormal));
                                colUnidadesTmpExtra.BorderWidthTop = 0;
                                colUnidadesTmpExtra.BorderWidthLeft = 0;
                                colUnidadesTmpExtra.BorderWidthRight = 0;
                                colUnidadesTmpExtra.BorderWidthBottom = 1;
                                colUnidadesTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colUnidadesTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colPrecioCompraTmpExtra = new PdfPCell(new Phrase(boughtPrice.ToString("C"), fuenteNormal));
                                colPrecioCompraTmpExtra.BorderWidthTop = 0;
                                colPrecioCompraTmpExtra.BorderWidthLeft = 0;
                                colPrecioCompraTmpExtra.BorderWidthRight = 0;
                                colPrecioCompraTmpExtra.BorderWidthBottom = 1;
                                colPrecioCompraTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colPrecioCompraTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colPrecioVentaTmpExtra = new PdfPCell(new Phrase(salesPrice.ToString("C"), fuenteNormal));
                                colPrecioVentaTmpExtra.BorderWidthTop = 0;
                                colPrecioVentaTmpExtra.BorderWidthLeft = 0;
                                colPrecioVentaTmpExtra.BorderWidthRight = 0;
                                colPrecioVentaTmpExtra.BorderWidthBottom = 1;
                                colPrecioVentaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colPrecioVentaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colStockTmpAnteriorExtra = new PdfPCell(new Phrase(lastStock.ToString("N2"), fuenteNormal));
                                colStockTmpAnteriorExtra.BorderWidthTop = 0;
                                colStockTmpAnteriorExtra.BorderWidthLeft = 0;
                                colStockTmpAnteriorExtra.BorderWidthRight = 0;
                                colStockTmpAnteriorExtra.BorderWidthBottom = 1;
                                colStockTmpAnteriorExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colStockTmpAnteriorExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colStockTmpExtra = new PdfPCell(new Phrase(currentStock.ToString("N2"), fuenteNormal));
                                colStockTmpExtra.BorderWidthTop = 0;
                                colStockTmpExtra.BorderWidthLeft = 0;
                                colStockTmpExtra.BorderWidthRight = 0;
                                colStockTmpExtra.BorderWidthBottom = 1;
                                colStockTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colStockTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colFechaCompraTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colFechaCompraTmpExtra.BorderWidth = 0;
                                colFechaCompraTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colFechaOperacionTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colFechaOperacionTmpExtra.BorderWidth = 0;
                                colFechaOperacionTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                PdfPCell colComentariosTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colComentariosTmpExtra.BorderWidth = 0;
                                colComentariosTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                                tabla.AddCell(colNoConceptoTmpExtra);
                                tabla.AddCell(colProductoTmpExtra);
                                tabla.AddCell(colProveedorTmpExtra);
                                tabla.AddCell(colUnidadesTmpExtra);
                                tabla.AddCell(colPrecioCompraTmpExtra);
                                tabla.AddCell(colPrecioVentaTmpExtra);
                                tabla.AddCell(colStockTmpAnteriorExtra);
                                tabla.AddCell(colStockTmpExtra);
                                tabla.AddCell(colFechaCompraTmpExtra);
                                tabla.AddCell(colFechaOperacionTmpExtra);
                                tabla.AddCell(colComentariosTmpExtra);

                                for (int j = posicionDeColumnas; j < columnasDinamicas; j++)
                                {
                                    tabla.AddCell
                                    (
                                        new PdfPCell
                                        (
                                            new Phrase(string.Empty, fuenteNormal)
                                        )
                                        {
                                            BorderWidth = 0,
                                            HorizontalAlignment = Element.ALIGN_CENTER
                                        }
                                    );
                                }
                            }
                        }
                        #endregion

                        reporte.Add(titulo);
                        reporte.Add(Usuario);
                        reporte.Add(subTitulo);
                        //reporte.Add(domicilio);
                        reporte.Add(tabla);

                        reporte.AddTitle("Reporte Historial");
                        reporte.AddAuthor("PUDVE");
                        reporte.Close();
                        writer.Close();

                        VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
                        vr.ShowDialog();
                    }
                    #endregion
                    #region Conceptos seleccionados y Datos Dinamicos
                    else if (columnasConcepto < 10)
                    {
                        anchoColumnas = new float[(columnasConcepto + 1) + (columnasDinamicas - 11)];

                        anchoColumnas[position] = (float)Convert.ToDouble(ValueConceptos[0].ToString());
                        position++;

                        if (Producto)
                        {
                            anchoColumnas[position] = (float)Convert.ToDouble(ValueConceptos[1].ToString());
                            position++;
                        }
                        if (Proveedor)
                        {
                            anchoColumnas[position] = (float)Convert.ToDouble(ValueConceptos[2].ToString());
                            position++;
                        }
                        if (UnidadesCompradas.Equals(true) || UnidadesDisminuidas.Equals(true))
                        {
                            anchoColumnas[position] = (float)Convert.ToDouble(ValueConceptos[3].ToString());
                            position++;
                        }
                        if (PrecioCompra)
                        {
                            anchoColumnas[position] = (float)Convert.ToDouble(ValueConceptos[4].ToString());
                            position++;
                        }
                        if (PrecioVenta)
                        {
                            anchoColumnas[position] = (float)Convert.ToDouble(ValueConceptos[5].ToString());
                            position++;
                        }
                        if (StockAnterior)
                        {
                            anchoColumnas[position] = (float)Convert.ToDouble(ValueConceptos[6].ToString());
                            position++;
                        }
                        if (StockActual)
                        {
                            anchoColumnas[position] = (float)Convert.ToDouble(ValueConceptos[7].ToString());
                            position++;
                        }
                        if (FechaCompra)
                        {
                            anchoColumnas[position] = (float)Convert.ToDouble(ValueConceptos[8].ToString());
                            position++;
                        }
                        if (FechaOperacion)
                        {
                            anchoColumnas[position] = (float)Convert.ToDouble(ValueConceptos[9].ToString());
                            position++;
                        }
                        if (Comentario)
                        {
                            anchoColumnas[position] = (float)Convert.ToDouble(ValueConceptos[10].ToString());
                            position++;
                        }

                        for (int j = 11; j < columnasDinamicas; j++)
                        {
                            anchoColumnas[position] = (float)Convert.ToDouble(ValueConceptos[j].ToString());
                            position++;
                        }

                        Document reporte = new Document(PageSize.A3.Rotate());
                        PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

                        string logotipo = datos[11];
                        //string encabezado = $"\n{datos[1]} {datos[2]} {datos[3]}, {datos[4]}, {datos[5]}\nCol. {datos[6]} C.P. {datos[7]}\nRFC: {datos[8]}\n{datos[9]}\nTel. {datos[10]}\n\n";

                        reporte.Open();

                        //Validación para verificar si existe logotipo
                        if (logotipo != "")
                        {
                            logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + logotipo;

                            if (File.Exists(logotipo))
                            {
                                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logotipo);
                                logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                                logo.ScaleAbsolute(anchoLogo, altoLogo);
                                reporte.Add(logo);
                            }
                        }

                        Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
                        Paragraph Usuario = new Paragraph("");
                        Paragraph subTitulo = new Paragraph("");

                        string UsuarioActivo = string.Empty;

                        using (DataTable dtDataUsr = cn.CargarDatos(cs.UsuarioRazonSocialNombreCompleto(Convert.ToString(FormPrincipal.userID))))
                        {
                            if (!dtDataUsr.Rows.Count.Equals(0))
                            {
                                foreach (DataRow drDataUsr in dtDataUsr.Rows)
                                {
                                    UsuarioActivo = drDataUsr["Usuario"].ToString();
                                }
                            }
                        }

                        Usuario = new Paragraph("USUARIO: " + UsuarioActivo, fuenteNegrita);

                        //Paragraph subTitulo = new Paragraph("REPORTE ACTUALIZAR INVENTARIO\nFecha: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);
                        if (rbAumentarProducto.Checked)
                        {
                            subTitulo = new Paragraph("REPORTE DE ACTUALIZAR INVENTARIO\nSECCIÓN DE AUMENTAR\n\nFecha:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);
                        }
                        else if (rbDisminuirProducto.Checked)
                        {
                            subTitulo = new Paragraph("REPORTE ACTUALIZAR INVENTARIO\nSECCIÓN DE DISMINUIR\n\nFecha:" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);
                        }
                        //Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

                        titulo.Alignment = Element.ALIGN_CENTER;
                        Usuario.Alignment = Element.ALIGN_CENTER;
                        subTitulo.Alignment = Element.ALIGN_CENTER;
                        //domicilio.Alignment = Element.ALIGN_CENTER;
                        //domicilio.SetLeading(10, 0);

                        PdfPTable tabla = new PdfPTable(anchoColumnas.Count());
                        tabla.WidthPercentage = 100;
                        tabla.SetWidths(anchoColumnas);

                        PdfPCell colProducto = new PdfPCell(new Phrase("", fuenteNegrita));
                        PdfPCell colProveedor = new PdfPCell(new Phrase("", fuenteNegrita));
                        PdfPCell colUnidades = new PdfPCell(new Phrase("", fuenteNegrita));
                        PdfPCell colPrecioCompra = new PdfPCell(new Phrase("", fuenteNegrita));
                        PdfPCell colPrecioVenta = new PdfPCell(new Phrase("", fuenteNegrita));
                        PdfPCell colStockAnterior = new PdfPCell(new Phrase("", fuenteNegrita));
                        PdfPCell colStock = new PdfPCell(new Phrase("", fuenteNegrita));
                        PdfPCell colFechaCompra = new PdfPCell(new Phrase("", fuenteNegrita));
                        PdfPCell colFechaOperacion = new PdfPCell(new Phrase("", fuenteNegrita));
                        PdfPCell colComentarios = new PdfPCell(new Phrase("", fuenteNegrita));

                        PdfPCell colNoConcepto = new PdfPCell(new Phrase("No:", fuenteNegrita));
                        colNoConcepto.BorderWidth = 1;
                        colNoConcepto.BackgroundColor = new BaseColor(Color.SkyBlue);
                        colNoConcepto.HorizontalAlignment = Element.ALIGN_CENTER;
                        tabla.AddCell(colNoConcepto);

                        if (Producto)
                        {
                            colProducto = new PdfPCell(new Phrase("Producto", fuenteNegrita));
                            colProducto.BorderWidth = 1;
                            colProducto.BackgroundColor = new BaseColor(Color.SkyBlue);
                            colProducto.HorizontalAlignment = Element.ALIGN_CENTER;
                            tabla.AddCell(colProducto);
                        }
                        if (Proveedor)
                        {
                            colProveedor = new PdfPCell(new Phrase("Proveedor", fuenteNegrita));
                            colProveedor.BorderWidth = 1;
                            colProveedor.BackgroundColor = new BaseColor(Color.SkyBlue);
                            colProveedor.HorizontalAlignment = Element.ALIGN_CENTER;
                            tabla.AddCell(colProveedor);
                        }
                        if (UnidadesCompradas)
                        {
                            if (rbAumentarProducto.Checked)
                            {
                                colUnidades = new PdfPCell(new Phrase("Unidades compradas", fuenteNegrita));
                                colUnidades.BorderWidth = 1;
                                colUnidades.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colUnidades.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colUnidades);
                            }
                            else if (rbDisminuirProducto.Checked)
                            {
                                colUnidades = new PdfPCell(new Phrase("Unidades disminuidas", fuenteNegrita));
                                colUnidades.BorderWidth = 1;
                                colUnidades.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colUnidades.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colUnidades);
                            }
                        }
                        if (PrecioCompra)
                        {
                            colPrecioCompra = new PdfPCell(new Phrase("Precio compra", fuenteNegrita));
                            colPrecioCompra.BorderWidth = 1;
                            colPrecioCompra.BackgroundColor = new BaseColor(Color.SkyBlue);
                            colPrecioCompra.HorizontalAlignment = Element.ALIGN_CENTER;
                            tabla.AddCell(colPrecioCompra);
                        }
                        if (PrecioVenta)
                        {
                            colPrecioVenta = new PdfPCell(new Phrase("Precio venta", fuenteNegrita));
                            colPrecioVenta.BorderWidth = 1;
                            colPrecioVenta.BackgroundColor = new BaseColor(Color.SkyBlue);
                            colPrecioVenta.HorizontalAlignment = Element.ALIGN_CENTER;
                            tabla.AddCell(colPrecioVenta);
                        }
                        if (StockAnterior)
                        {
                            colStockAnterior = new PdfPCell(new Phrase("Stock anterior", fuenteNegrita));
                            colStockAnterior.BorderWidth = 1;
                            colStockAnterior.BackgroundColor = new BaseColor(Color.SkyBlue);
                            colStockAnterior.HorizontalAlignment = Element.ALIGN_CENTER;
                            tabla.AddCell(colStockAnterior);
                        }
                        if (StockActual)
                        {
                            colStock = new PdfPCell(new Phrase("Stock actual", fuenteNegrita));
                            colStock.BorderWidth = 1;
                            colStock.BackgroundColor = new BaseColor(Color.SkyBlue);
                            colStock.HorizontalAlignment = Element.ALIGN_CENTER;
                            tabla.AddCell(colStock);
                        }
                        if (FechaCompra)
                        {
                            colFechaCompra = new PdfPCell(new Phrase("Fecha de compra", fuenteNegrita));
                            colFechaCompra.BorderWidth = 1;
                            colFechaCompra.BackgroundColor = new BaseColor(Color.SkyBlue);
                            colFechaCompra.HorizontalAlignment = Element.ALIGN_CENTER;
                            tabla.AddCell(colFechaCompra);
                        }
                        if (FechaOperacion)
                        {
                            colFechaOperacion = new PdfPCell(new Phrase("Fecha de operación", fuenteNegrita));
                            colFechaOperacion.BorderWidth = 1;
                            colFechaOperacion.BackgroundColor = new BaseColor(Color.SkyBlue);
                            colFechaOperacion.HorizontalAlignment = Element.ALIGN_CENTER;
                            tabla.AddCell(colFechaOperacion);
                        }
                        if (Comentario)
                        {
                            colComentarios = new PdfPCell(new Phrase("Comentarios", fuenteNegrita));
                            colComentarios.BorderWidth = 1;
                            colComentarios.BackgroundColor = new BaseColor(Color.SkyBlue);
                            colComentarios.HorizontalAlignment = Element.ALIGN_CENTER;
                            tabla.AddCell(colComentarios);
                        }

                        using (DataTable dtConceptosDinamicosActivos = cn.CargarDatos(cs.verConceptosDinamicosActivos()))
                        {
                            if (!dtConceptosDinamicosActivos.Rows.Count.Equals(0))
                            {
                                foreach (DataRow item in dtConceptosDinamicosActivos.Rows)
                                {
                                    var columnaDinamica = item["concepto"].ToString().Replace("_", " ");
                                    tabla.AddCell
                                    (
                                        new PdfPCell
                                        (
                                            new Phrase(columnaDinamica, fuenteNegrita)
                                        )
                                        {
                                            BorderWidth = 1,
                                            BackgroundColor = new BaseColor(Color.SkyBlue),
                                            HorizontalAlignment = Element.ALIGN_CENTER
                                        }
                                    );
                                }
                            }
                        }

                        //Consulta para obtener los registros del Historial de compras
                        MySqlConnection sql_con;
                        MySqlCommand sql_cmd;
                        MySqlDataReader dr;

                        if (!string.IsNullOrWhiteSpace(servidor))
                        {
                            sql_con = new MySqlConnection("datasource=" + servidor + ";port=6666;username=root;password=;database=pudve;");
                        }
                        else
                        {
                            sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");

                        }

                        sql_con.Open();
                        //sql_cmd = new MySqlCommand($"SELECT * FROM HistorialCompras WHERE IDUsuario = {FormPrincipal.userID} AND IDReporte = {idReporte}", sql_con);

                        #region Sección Aumentar Invetario
                        if (rbAumentarProducto.Checked)
                        {
                            var NoRev = Convert.ToInt32(cs.GetNoRevAumentarInventario());
                            sql_cmd = new MySqlCommand(cs.SearchDGVAumentarInventario(NoRev), sql_con);
                            dr = sql_cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                int idProducto = 0;
                                string producto = string.Empty, proveedor = string.Empty, unidades = string.Empty, compra = string.Empty, venta = string.Empty, stockAnterior = string.Empty, fechaCompra = string.Empty, fechaOperacion = string.Empty, comentarios = string.Empty;

                                idProducto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("No")));
                                producto = dr.GetValue(dr.GetOrdinal("Producto")).ToString();
                                proveedor = dr.GetValue(dr.GetOrdinal("Proveedor")).ToString();
                                if (dr.GetValue(dr.GetOrdinal("Unidades_Compradas")).ToString().Equals(string.Empty))
                                {
                                    unidades = "0.00";
                                }
                                else if (!dr.GetValue(dr.GetOrdinal("Unidades_Compradas")).ToString().Equals(string.Empty))
                                {
                                    unidades = dr.GetValue(dr.GetOrdinal("Unidades_Compradas")).ToString();
                                }
                                compra = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Precio_Compra"))).ToString("0.00");
                                venta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Precio_Venta"))).ToString("0.00");

                                var tmp = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                                var stock = tmp[4];

                                stockAnterior = (Convert.ToDouble(stock) - Convert.ToDouble(unidades)).ToString("0.00");

                                DateTime fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Fecha_Compra"));
                                fechaCompra = fecha.ToString("yyyy-MM-dd");

                                DateTime fechaOp = (DateTime)dr.GetValue(dr.GetOrdinal("Fecha_Compra"));
                                fechaOperacion = fechaOp.ToString("yyyy-MM-dd HH:mm tt");

                                comentarios = dr.GetValue(dr.GetOrdinal("Comentarios")).ToString();

                                PdfPCell colProductoTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colProveedorTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colUnidadesTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colPrecioCompraTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colPrecioVentaTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colStockTmpAnterior = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colStockTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colFechaCompraTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colFechaOperacionTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colComentariosTmp = new PdfPCell(new Phrase("", fuenteNormal));

                                numRow++;

                                PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                                colNoConceptoTmp.BorderWidth = 1;
                                colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colNoConceptoTmp);

                                if (Producto)
                                {
                                    colProductoTmp = new PdfPCell(new Phrase(producto, fuenteNormal));
                                    colProductoTmp.BorderWidth = 1;
                                    colProductoTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colProductoTmp);
                                }
                                if (Proveedor)
                                {
                                    colProveedorTmp = new PdfPCell(new Phrase(proveedor, fuenteNormal));
                                    colProveedorTmp.BorderWidth = 1;
                                    colProveedorTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colProveedorTmp);
                                }
                                if (UnidadesCompradas)
                                {
                                    colUnidadesTmp = new PdfPCell(new Phrase(unidades, fuenteNormal));
                                    colUnidadesTmp.BorderWidth = 1;
                                    colUnidadesTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colUnidadesTmp);
                                    unitsBoughtDiminished += (float)Convert.ToDouble(unidades);
                                }
                                if (PrecioCompra)
                                {
                                    colPrecioCompraTmp = new PdfPCell(new Phrase("$" + compra, fuenteNormal));
                                    colPrecioCompraTmp.BorderWidth = 1;
                                    colPrecioCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colPrecioCompraTmp);
                                    boughtPrice += (float)Convert.ToDouble(compra);
                                }
                                if (PrecioVenta)
                                {
                                    colPrecioVentaTmp = new PdfPCell(new Phrase("$" + venta, fuenteNormal));
                                    colPrecioVentaTmp.BorderWidth = 1;
                                    colPrecioVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colPrecioVentaTmp);
                                    salesPrice += (float)Convert.ToDouble(venta);
                                }
                                if (StockAnterior)
                                {
                                    colStockTmpAnterior = new PdfPCell(new Phrase(stockAnterior, fuenteNormal));
                                    colStockTmpAnterior.BorderWidth = 1;
                                    colStockTmpAnterior.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colStockTmpAnterior);
                                    lastStock += (float)Convert.ToDouble(stockAnterior);
                                }
                                if (StockActual)
                                {
                                    colStockTmp = new PdfPCell(new Phrase(stock, fuenteNormal));
                                    colStockTmp.BorderWidth = 1;
                                    colStockTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colStockTmp);
                                    currentStock += (float)Convert.ToDouble(stock);
                                }
                                if (FechaCompra)
                                {
                                    colFechaCompraTmp = new PdfPCell(new Phrase(fechaCompra, fuenteNormal));
                                    colFechaCompraTmp.BorderWidth = 1;
                                    colFechaCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colFechaCompraTmp);
                                }
                                if (FechaOperacion)
                                {
                                    colFechaOperacionTmp = new PdfPCell(new Phrase(fechaOperacion, fuenteNormal));
                                    colFechaOperacionTmp.BorderWidth = 1;
                                    colFechaOperacionTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colFechaOperacionTmp);
                                }
                                if (Comentario)
                                {
                                    colComentariosTmp = new PdfPCell(new Phrase(comentarios, fuenteNormal));
                                    colComentariosTmp.BorderWidth = 1;
                                    colComentariosTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colComentariosTmp);
                                }

                                for (int j = 11; j < columnasDinamicas; j++)
                                {
                                    var datoDinamico = string.Empty;

                                    if (!dr[j].ToString().Equals(string.Empty))
                                    {
                                        datoDinamico = dr[j].ToString();
                                    }
                                    else
                                    {
                                        datoDinamico = "N/A";
                                    }

                                    tabla.AddCell
                                    (
                                        new PdfPCell
                                        (
                                            new Phrase(datoDinamico.Replace("_", " "), fuenteNormal)
                                        )
                                        {
                                            BorderWidth = 1,
                                            HorizontalAlignment = Element.ALIGN_CENTER
                                        }
                                    );
                                }
                            }

                            PdfPCell colNoConceptoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                            colNoConceptoTmpExtra.BorderWidth = 0;
                            colNoConceptoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                            tabla.AddCell(colNoConceptoTmpExtra);

                            if (Producto)
                            {
                                PdfPCell colProductoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colProductoTmpExtra.BorderWidth = 0;
                                colProductoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colProductoTmpExtra);
                            }
                            if (Proveedor)
                            {
                                PdfPCell colProveedorTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colProveedorTmpExtra.BorderWidth = 0;
                                colProveedorTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colProveedorTmpExtra);
                            }
                            if (UnidadesCompradas)
                            {
                                PdfPCell colUnidadesTmpExtra = new PdfPCell(new Phrase(unitsBoughtDiminished.ToString("N2"), fuenteNormal));
                                colUnidadesTmpExtra.BorderWidthTop = 0;
                                colUnidadesTmpExtra.BorderWidthLeft = 0;
                                colUnidadesTmpExtra.BorderWidthRight = 0;
                                colUnidadesTmpExtra.BorderWidthBottom = 1;
                                colUnidadesTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colUnidadesTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colUnidadesTmpExtra);
                            }
                            if (PrecioCompra)
                            {
                                PdfPCell colPrecioCompraTmpExtra = new PdfPCell(new Phrase(boughtPrice.ToString("C"), fuenteNormal));
                                colPrecioCompraTmpExtra.BorderWidthTop = 0;
                                colPrecioCompraTmpExtra.BorderWidthLeft = 0;
                                colPrecioCompraTmpExtra.BorderWidthRight = 0;
                                colPrecioCompraTmpExtra.BorderWidthBottom = 1;
                                colPrecioCompraTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colPrecioCompraTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colPrecioCompraTmpExtra);
                            }
                            if (PrecioVenta)
                            {
                                PdfPCell colPrecioVentaTmpExtra = new PdfPCell(new Phrase(salesPrice.ToString("C"), fuenteNormal));
                                colPrecioVentaTmpExtra.BorderWidthTop = 0;
                                colPrecioVentaTmpExtra.BorderWidthLeft = 0;
                                colPrecioVentaTmpExtra.BorderWidthRight = 0;
                                colPrecioVentaTmpExtra.BorderWidthBottom = 1;
                                colPrecioVentaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colPrecioVentaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colPrecioVentaTmpExtra);
                            }
                            if (StockAnterior)
                            {
                                PdfPCell colStockTmpAnteriorExtra = new PdfPCell(new Phrase(lastStock.ToString("N2"), fuenteNormal));
                                colStockTmpAnteriorExtra.BorderWidthTop = 0;
                                colStockTmpAnteriorExtra.BorderWidthLeft = 0;
                                colStockTmpAnteriorExtra.BorderWidthRight = 0;
                                colStockTmpAnteriorExtra.BorderWidthBottom = 1;
                                colStockTmpAnteriorExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colStockTmpAnteriorExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colStockTmpAnteriorExtra);
                            }
                            if (StockActual)
                            {
                                PdfPCell colStockTmpExtra = new PdfPCell(new Phrase(currentStock.ToString("N2"), fuenteNormal));
                                colStockTmpExtra.BorderWidthTop = 0;
                                colStockTmpExtra.BorderWidthLeft = 0;
                                colStockTmpExtra.BorderWidthRight = 0;
                                colStockTmpExtra.BorderWidthBottom = 1;
                                colStockTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colStockTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colStockTmpExtra);
                            }
                            if (FechaCompra)
                            {
                                PdfPCell colFechaCompraTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colFechaCompraTmpExtra.BorderWidth = 0;
                                colFechaCompraTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colFechaCompraTmpExtra);
                            }
                            if (FechaOperacion)
                            {
                                PdfPCell colFechaOperacionTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colFechaOperacionTmpExtra.BorderWidth = 0;
                                colFechaOperacionTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colFechaOperacionTmpExtra);
                            }
                            if (Comentario)
                            {
                                PdfPCell colComentariosTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colComentariosTmpExtra.BorderWidth = 0;
                                colComentariosTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colComentariosTmpExtra);
                            }

                            for (int j = 11; j < columnasDinamicas; j++)
                            {
                                tabla.AddCell
                                (
                                    new PdfPCell
                                    (
                                        new Phrase(string.Empty, fuenteNormal)
                                    )
                                    {
                                        BorderWidth = 0,
                                        HorizontalAlignment = Element.ALIGN_CENTER
                                    }
                                );
                            }
                        }
                        #endregion
                        #region Sección Disminuir Invetario
                        else if (rbDisminuirProducto.Checked)
                        {
                            var NoRev = Convert.ToInt32(cs.GetNoRevDisminuirInventario());
                            sql_cmd = new MySqlCommand(cs.SearchDGVDisminuirInventario(NoRev), sql_con);
                            dr = sql_cmd.ExecuteReader();

                            while (dr.Read())
                            {
                                int idProducto = 0;
                                string producto = string.Empty, proveedor = string.Empty, unidades = string.Empty, compra = string.Empty, venta = string.Empty, stockAnterior = string.Empty, fechaCompra = string.Empty, fechaOperacion = string.Empty, comentarios = string.Empty;

                                idProducto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("No")));
                                producto = dr.GetValue(dr.GetOrdinal("Producto")).ToString();
                                proveedor = dr.GetValue(dr.GetOrdinal("Proveedor")).ToString();
                                if (dr.GetValue(dr.GetOrdinal("Unidades_Compradas")).ToString().Equals(string.Empty))
                                {
                                    unidades = "0.00";
                                }
                                else if (!dr.GetValue(dr.GetOrdinal("Unidades_Compradas")).ToString().Equals(string.Empty))
                                {
                                    unidades = dr.GetValue(dr.GetOrdinal("Unidades_Compradas")).ToString();
                                }
                                compra = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Precio_Compra"))).ToString("0.00");
                                venta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Precio_Venta"))).ToString("0.00");

                                var tmp = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                                var stock = tmp[4];

                                stockAnterior = (Convert.ToDouble(stock) - Convert.ToDouble(unidades)).ToString("0.00");

                                DateTime fecha = (DateTime)dr.GetValue(dr.GetOrdinal("Fecha_Compra"));
                                fechaCompra = fecha.ToString("yyyy-MM-dd");

                                DateTime fechaOp = (DateTime)dr.GetValue(dr.GetOrdinal("Fecha_Compra"));
                                fechaOperacion = fechaOp.ToString("yyyy-MM-dd HH:mm tt");

                                comentarios = dr.GetValue(dr.GetOrdinal("Comentarios")).ToString();

                                PdfPCell colProductoTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colProveedorTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colUnidadesTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colPrecioCompraTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colPrecioVentaTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colStockTmpAnterior = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colStockTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colFechaCompraTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colFechaOperacionTmp = new PdfPCell(new Phrase("", fuenteNormal));
                                PdfPCell colComentariosTmp = new PdfPCell(new Phrase("", fuenteNormal));

                                numRow++;

                                PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                                colNoConceptoTmp.BorderWidth = 1;
                                colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colNoConceptoTmp);

                                if (Producto)
                                {
                                    colProductoTmp = new PdfPCell(new Phrase(producto, fuenteNormal));
                                    colProductoTmp.BorderWidth = 1;
                                    colProductoTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colProductoTmp);
                                }
                                if (Proveedor)
                                {
                                    colProveedorTmp = new PdfPCell(new Phrase(proveedor, fuenteNormal));
                                    colProveedorTmp.BorderWidth = 1;
                                    colProveedorTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colProveedorTmp);
                                }
                                if (UnidadesCompradas)
                                {
                                    colUnidadesTmp = new PdfPCell(new Phrase(unidades, fuenteNormal));
                                    colUnidadesTmp.BorderWidth = 1;
                                    colUnidadesTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colUnidadesTmp);
                                    unitsBoughtDiminished += (float)Convert.ToDouble(unidades);
                                }
                                if (PrecioCompra)
                                {
                                    colPrecioCompraTmp = new PdfPCell(new Phrase("$" + compra, fuenteNormal));
                                    colPrecioCompraTmp.BorderWidth = 1;
                                    colPrecioCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colPrecioCompraTmp);
                                    boughtPrice += (float)Convert.ToDouble(compra);
                                }
                                if (PrecioVenta)
                                {
                                    colPrecioVentaTmp = new PdfPCell(new Phrase("$" + venta, fuenteNormal));
                                    colPrecioVentaTmp.BorderWidth = 1;
                                    colPrecioVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colPrecioVentaTmp);
                                    salesPrice += (float)Convert.ToDouble(venta);
                                }
                                if (StockAnterior)
                                {
                                    colStockTmpAnterior = new PdfPCell(new Phrase(stockAnterior, fuenteNormal));
                                    colStockTmpAnterior.BorderWidth = 1;
                                    colStockTmpAnterior.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colStockTmpAnterior);
                                    lastStock += (float)Convert.ToDouble(stockAnterior);
                                }
                                if (StockActual)
                                {
                                    colStockTmp = new PdfPCell(new Phrase(stock, fuenteNormal));
                                    colStockTmp.BorderWidth = 1;
                                    colStockTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colStockTmp);
                                    currentStock += (float)Convert.ToDouble(stock);
                                }
                                if (FechaCompra)
                                {
                                    colFechaCompraTmp = new PdfPCell(new Phrase(fechaCompra, fuenteNormal));
                                    colFechaCompraTmp.BorderWidth = 1;
                                    colFechaCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colFechaCompraTmp);
                                }
                                if (FechaOperacion)
                                {
                                    colFechaOperacionTmp = new PdfPCell(new Phrase(fechaOperacion, fuenteNormal));
                                    colFechaOperacionTmp.BorderWidth = 1;
                                    colFechaOperacionTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colFechaOperacionTmp);
                                }
                                if (Comentario)
                                {
                                    colComentariosTmp = new PdfPCell(new Phrase(comentarios, fuenteNormal));
                                    colComentariosTmp.BorderWidth = 1;
                                    colComentariosTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                                    tabla.AddCell(colComentariosTmp);
                                }

                                for (int j = 11; j < columnasDinamicas; j++)
                                {
                                    var datoDinamico = string.Empty;

                                    if (!dr[j].ToString().Equals(string.Empty))
                                    {
                                        datoDinamico = dr[j].ToString();
                                    }
                                    else
                                    {
                                        datoDinamico = "N/A";
                                    }

                                    tabla.AddCell
                                    (
                                        new PdfPCell
                                        (
                                            new Phrase(datoDinamico.Replace("_", " "), fuenteNormal)
                                        )
                                        {
                                            BorderWidth = 1,
                                            HorizontalAlignment = Element.ALIGN_CENTER
                                        }
                                    );
                                }
                            }

                            PdfPCell colNoConceptoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                            colNoConceptoTmpExtra.BorderWidth = 0;
                            colNoConceptoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                            tabla.AddCell(colNoConceptoTmpExtra);

                            if (Producto)
                            {
                                PdfPCell colProductoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colProductoTmpExtra.BorderWidth = 0;
                                colProductoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colProductoTmpExtra);
                            }
                            if (Proveedor)
                            {
                                PdfPCell colProveedorTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colProveedorTmpExtra.BorderWidth = 0;
                                colProveedorTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colProveedorTmpExtra);
                            }
                            if (UnidadesCompradas)
                            {
                                PdfPCell colUnidadesTmpExtra = new PdfPCell(new Phrase(unitsBoughtDiminished.ToString("N2"), fuenteNormal));
                                colUnidadesTmpExtra.BorderWidthTop = 0;
                                colUnidadesTmpExtra.BorderWidthLeft = 0;
                                colUnidadesTmpExtra.BorderWidthRight = 0;
                                colUnidadesTmpExtra.BorderWidthBottom = 1;
                                colUnidadesTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colUnidadesTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colUnidadesTmpExtra);
                            }
                            if (PrecioCompra)
                            {
                                PdfPCell colPrecioCompraTmpExtra = new PdfPCell(new Phrase(boughtPrice.ToString("C"), fuenteNormal));
                                colPrecioCompraTmpExtra.BorderWidthTop = 0;
                                colPrecioCompraTmpExtra.BorderWidthLeft = 0;
                                colPrecioCompraTmpExtra.BorderWidthRight = 0;
                                colPrecioCompraTmpExtra.BorderWidthBottom = 1;
                                colPrecioCompraTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colPrecioCompraTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colPrecioCompraTmpExtra);
                            }
                            if (PrecioVenta)
                            {
                                PdfPCell colPrecioVentaTmpExtra = new PdfPCell(new Phrase(salesPrice.ToString("C"), fuenteNormal));
                                colPrecioVentaTmpExtra.BorderWidthTop = 0;
                                colPrecioVentaTmpExtra.BorderWidthLeft = 0;
                                colPrecioVentaTmpExtra.BorderWidthRight = 0;
                                colPrecioVentaTmpExtra.BorderWidthBottom = 1;
                                colPrecioVentaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colPrecioVentaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colPrecioVentaTmpExtra);
                            }
                            if (StockAnterior)
                            {
                                PdfPCell colStockTmpAnteriorExtra = new PdfPCell(new Phrase(lastStock.ToString("N2"), fuenteNormal));
                                colStockTmpAnteriorExtra.BorderWidthTop = 0;
                                colStockTmpAnteriorExtra.BorderWidthLeft = 0;
                                colStockTmpAnteriorExtra.BorderWidthRight = 0;
                                colStockTmpAnteriorExtra.BorderWidthBottom = 1;
                                colStockTmpAnteriorExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colStockTmpAnteriorExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colStockTmpAnteriorExtra);
                            }
                            if (StockActual)
                            {
                                PdfPCell colStockTmpExtra = new PdfPCell(new Phrase(currentStock.ToString("N2"), fuenteNormal));
                                colStockTmpExtra.BorderWidthTop = 0;
                                colStockTmpExtra.BorderWidthLeft = 0;
                                colStockTmpExtra.BorderWidthRight = 0;
                                colStockTmpExtra.BorderWidthBottom = 1;
                                colStockTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                                colStockTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colStockTmpExtra);
                            }
                            if (FechaCompra)
                            {
                                PdfPCell colFechaCompraTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colFechaCompraTmpExtra.BorderWidth = 0;
                                colFechaCompraTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colFechaCompraTmpExtra);
                            }
                            if (FechaOperacion)
                            {
                                PdfPCell colFechaOperacionTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colFechaOperacionTmpExtra.BorderWidth = 0;
                                colFechaOperacionTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colFechaOperacionTmpExtra);
                            }
                            if (Comentario)
                            {
                                PdfPCell colComentariosTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                                colComentariosTmpExtra.BorderWidth = 0;
                                colComentariosTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                                tabla.AddCell(colComentariosTmpExtra);
                            }

                            for (int j = 11; j < columnasDinamicas; j++)
                            {
                                tabla.AddCell
                                (
                                    new PdfPCell
                                    (
                                        new Phrase(string.Empty, fuenteNormal)
                                    )
                                    {
                                        BorderWidth = 0,
                                        HorizontalAlignment = Element.ALIGN_CENTER
                                    }
                                );
                            }
                        }
                        #endregion

                        reporte.Add(titulo);
                        reporte.Add(Usuario);
                        reporte.Add(subTitulo);
                        //reporte.Add(domicilio);
                        reporte.Add(tabla);

                        reporte.AddTitle("Reporte Historial");
                        reporte.AddAuthor("PUDVE");
                        reporte.Close();
                        writer.Close();

                        VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
                        vr.ShowDialog();
                    }
                    #endregion
                }
                #endregion
            }
            #endregion
            #region Sección Botón Actualizar Invetario XML
            if (tipo.Equals(1))
            {
                servidor = Properties.Settings.Default.Hosting;

                var usuario = string.Empty;

                if (FormPrincipal.userNickName.Contains("@"))
                {
                    var palabras = FormPrincipal.userNickName.Split('@');
                    usuario = palabras[0].ToString();
                }
                else
                {
                    usuario = FormPrincipal.userNickName;
                }

                if (!string.IsNullOrWhiteSpace(servidor))
                {
                    rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Historial\{usuario}\ActualizarInvetarioXML\";
                }
                else
                {
                    rutaArchivo = $@"C:\Archivos PUDVE\Reportes\Historial\{usuario}\ActualizarInvetarioXML\";
                }

                if (!Directory.Exists(rutaArchivo))
                {
                    Directory.CreateDirectory(rutaArchivo);
                }

                if (!string.IsNullOrWhiteSpace(servidor))
                {
                    rutaArchivo += $"reporte_actualizar_inventario_{idReporte}.pdf";
                }
                else
                {
                    rutaArchivo += $"reporte_actualizar_inventario_{idReporte}.pdf";
                }

                anchoColumnas = new float[] { 30f, 245f, 200f, 80f, 70f, 70f, 55f, 55f, 80f, 95f };

                Document reporte = new Document(PageSize.A3.Rotate());
                PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

                string logotipo = datos[10];
                //string encabezado = $"\n{datos[1]} {datos[2]} {datos[3]}, {datos[4]}, {datos[5]}\nCol. {datos[6]} C.P. {datos[7]}\nRFC: {datos[8]}\n{datos[9]}\nTel. {datos[10]}\n\n";

                reporte.Open();

                //Validación para verificar si existe logotipo
                if (logotipo != "")
                {
                    logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + logotipo;

                    if (File.Exists(logotipo))
                    {
                        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logotipo);
                        logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                        logo.ScaleAbsolute(anchoLogo, altoLogo);
                        reporte.Add(logo);
                    }
                }

                Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
                Paragraph Usuario = new Paragraph("");
                Paragraph subTitulo = new Paragraph("");

                string UsuarioActivo = string.Empty;

                using (DataTable dtDataUsr = cn.CargarDatos(cs.UsuarioRazonSocialNombreCompleto(Convert.ToString(FormPrincipal.userID))))
                {
                    if (!dtDataUsr.Rows.Count.Equals(0))
                    {
                        foreach (DataRow drDataUsr in dtDataUsr.Rows)
                        {
                            UsuarioActivo = drDataUsr["Usuario"].ToString();
                        }
                    }
                }

                Usuario = new Paragraph("USUARIO: " + UsuarioActivo, fuenteNegrita);

                //Paragraph subTitulo = new Paragraph("REPORTE ACTUALIZAR INVENTARIO\nFecha: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);
                if (rbAumentarProducto.Checked)
                {
                    subTitulo = new Paragraph("REPORTE DE ACTUALIZAR INVENTARIO\nSECCIÓN DE AUMENTAR\n\nFecha: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);
                }
                else if (rbDisminuirProducto.Checked)
                {
                    subTitulo = new Paragraph("REPORTE DE ACTUALIZAR INVENTARIO\nSECCIÓN DE DISMINUIR\n\nFecha: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);
                }
                //Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

                titulo.Alignment = Element.ALIGN_CENTER;
                Usuario.Alignment = Element.ALIGN_CENTER;
                subTitulo.Alignment = Element.ALIGN_CENTER;
                //domicilio.Alignment = Element.ALIGN_CENTER;
                //domicilio.SetLeading(10, 0);

                /***************************************
                ** Tabla con los productos ajustados **
                ***************************************/
                PdfPTable tabla = new PdfPTable(10);
                tabla.WidthPercentage = 100;
                tabla.SetWidths(anchoColumnas);

                PdfPCell colNoConcepto = new PdfPCell(new Phrase("No:", fuenteNegrita));
                colNoConcepto.BorderWidth = 1;
                colNoConcepto.BackgroundColor = new BaseColor(Color.SkyBlue);
                colNoConcepto.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colProducto = new PdfPCell(new Phrase("Producto", fuenteNegrita));
                colProducto.BorderWidth = 1;
                colProducto.BackgroundColor = new BaseColor(Color.SkyBlue);
                colProducto.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colUnidadCompradas = new PdfPCell(new Phrase("Unidades compradas", fuenteNegrita));
                colUnidadCompradas.BorderWidth = 1;
                colUnidadCompradas.BackgroundColor = new BaseColor(Color.SkyBlue);
                colUnidadCompradas.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioCompra = new PdfPCell(new Phrase("Precio compra", fuenteNegrita));
                colPrecioCompra.BorderWidth = 1;
                colPrecioCompra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPrecioCompra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDescuento = new PdfPCell(new Phrase("Descuento", fuenteNegrita));
                colDescuento.BorderWidth = 1;
                colDescuento.BackgroundColor = new BaseColor(Color.SkyBlue);
                colDescuento.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioVenta = new PdfPCell(new Phrase("Precio venta", fuenteNegrita));
                colPrecioVenta.BorderWidth = 1;
                colPrecioVenta.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPrecioVenta.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaOperacion = new PdfPCell(new Phrase("Fecha operación", fuenteNegrita));
                colFechaOperacion.BorderWidth = 1;
                colFechaOperacion.BackgroundColor = new BaseColor(Color.SkyBlue);
                colFechaOperacion.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colProveedor = new PdfPCell(new Phrase("Proveedor", fuenteNegrita));
                colProveedor.BorderWidth = 1;
                colProveedor.BackgroundColor = new BaseColor(Color.SkyBlue);
                colProveedor.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRFC = new PdfPCell(new Phrase("RFC", fuenteNegrita));
                colRFC.BorderWidth = 1;
                colRFC.BackgroundColor = new BaseColor(Color.SkyBlue);
                colRFC.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colReporte = new PdfPCell(new Phrase("Reporte", fuenteNegrita));
                colReporte.BorderWidth = 1;
                colReporte.BackgroundColor = new BaseColor(Color.SkyBlue);
                colReporte.HorizontalAlignment = Element.ALIGN_CENTER;

                tabla.AddCell(colNoConcepto);
                tabla.AddCell(colProducto);
                tabla.AddCell(colUnidadCompradas);
                tabla.AddCell(colPrecioCompra);
                tabla.AddCell(colDescuento);
                tabla.AddCell(colPrecioVenta);
                tabla.AddCell(colFechaOperacion);
                tabla.AddCell(colProveedor);
                tabla.AddCell(colRFC);
                tabla.AddCell(colReporte);


                //Consulta para obtener los registros del Historial de compras
                MySqlConnection sql_con;
                MySqlCommand sql_cmd;
                MySqlDataReader dr;

                if (!string.IsNullOrWhiteSpace(servidor))
                {
                    sql_con = new MySqlConnection("datasource=" + servidor + ";port=6666;username=root;password=;database=pudve;");
                }
                else
                {
                    sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");

                }

                float unidadesCompradasReporte = 0,
                        precioCompraReporte = 0,
                        descuentoReporte = 0,
                        precioVentaReporte = 0;

                sql_con.Open();
                sql_cmd = new MySqlCommand(cs.reporteActualizarInventarioDesdeXML(FormPrincipal.userID, idReporte), sql_con);

                dr = sql_cmd.ExecuteReader();

                while (dr.Read())
                {
                    var producto = dr.GetValue(dr.GetOrdinal("Producto")).ToString();
                    var unidadesCompradas = dr.GetValue(dr.GetOrdinal("UnidadesCompradas")).ToString();
                    var precioCompra = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PrecioCompra"))).ToString("0.00");
                    var descuento = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Descuento"))).ToString("0.00");
                    var precioVenta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("PrecioVenta"))).ToString("0.00");
                    var fechaOperacion = dr.GetValue(dr.GetOrdinal("FechaOperacion")).ToString();
                    var proveedor = dr.GetValue(dr.GetOrdinal("Proveedor")).ToString();
                    var rfc = dr.GetValue(dr.GetOrdinal("RFC")).ToString();
                    var numReporte = dr.GetValue(dr.GetOrdinal("Reporte")).ToString();

                    numRow++;

                    PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                    colNoConceptoTmp.BorderWidth = 1;
                    colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colProductoTmp;
                    if (!producto.Equals(string.Empty))
                    {
                        colProductoTmp = new PdfPCell(new Phrase(producto, fuenteNormal));
                    }
                    else
                    {
                        colProductoTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                    }
                    colProductoTmp.BorderWidth = 1;
                    colProductoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colUnidadesCompradasTmp;
                    if (!unidadesCompradas.Equals(string.Empty))
                    {
                        colUnidadesCompradasTmp = new PdfPCell(new Phrase(unidadesCompradas, fuenteNormal));
                        unidadesCompradasReporte += (float)Convert.ToDouble(unidadesCompradas);
                    }
                    else
                    {
                        colUnidadesCompradasTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                    }
                    colUnidadesCompradasTmp.BorderWidth = 1;
                    colUnidadesCompradasTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colPrecioCompraTmp;
                    if (!precioCompra.Equals(string.Empty))
                    {
                        colPrecioCompraTmp = new PdfPCell(new Phrase("$" + precioCompra, fuenteNormal));
                        precioCompraReporte += (float)Convert.ToDouble(precioCompra);
                    }
                    else
                    {
                        colPrecioCompraTmp = new PdfPCell(new Phrase("$ ---", fuenteNormal));
                    }
                    colPrecioCompraTmp.BorderWidth = 1;
                    colPrecioCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDescuetoTmp;
                    if (!descuento.Equals(string.Empty))
                    {
                        colDescuetoTmp = new PdfPCell(new Phrase("$" + descuento, fuenteNormal));
                        descuentoReporte += (float)Convert.ToDouble(descuento);
                    }
                    else
                    {
                        colDescuetoTmp = new PdfPCell(new Phrase("$ ---", fuenteNormal));
                    }
                    colDescuetoTmp.BorderWidth = 1;
                    colDescuetoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colPrecioVentaTmp;
                    if (!precioVenta.Equals(string.Empty))
                    {
                        colPrecioVentaTmp = new PdfPCell(new Phrase("$" + precioVenta, fuenteNormal));
                        precioVentaReporte += (float)Convert.ToDouble(precioVenta);
                    }
                    else
                    {
                        colPrecioVentaTmp = new PdfPCell(new Phrase("$ ---", fuenteNormal));
                    }
                    colPrecioVentaTmp.BorderWidth = 1;
                    colPrecioVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colFechaOperacionTmp;
                    if (!fechaOperacion.Equals(string.Empty))
                    {
                        colFechaOperacionTmp = new PdfPCell(new Phrase(fechaOperacion, fuenteNormal));
                    }
                    else
                    {
                        colFechaOperacionTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                    }
                    colFechaOperacionTmp.BorderWidth = 1;
                    colFechaOperacionTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colProveedorTmp;
                    if (!proveedor.Equals(string.Empty))
                    {
                        colProveedorTmp = new PdfPCell(new Phrase(proveedor, fuenteNormal));
                    }
                    else
                    {
                        colProveedorTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                    }
                    colProveedorTmp.BorderWidth = 1;
                    colProveedorTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colRFCTmp;
                    if (!rfc.Equals(string.Empty))
                    {
                        colRFCTmp = new PdfPCell(new Phrase(rfc, fuenteNormal));
                    }
                    else
                    {
                        colRFCTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                    }
                    colRFCTmp.BorderWidth = 1;
                    colRFCTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colNumReporteTmp;
                    if (!numReporte.Equals(string.Empty))
                    {
                        colNumReporteTmp = new PdfPCell(new Phrase(numReporte, fuenteNormal));
                    }
                    else
                    {
                        colNumReporteTmp = new PdfPCell(new Phrase("---", fuenteNormal));
                    }
                    colNumReporteTmp.BorderWidth = 1;
                    colNumReporteTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    tabla.AddCell(colNoConceptoTmp);
                    tabla.AddCell(colProductoTmp);
                    tabla.AddCell(colUnidadesCompradasTmp);
                    tabla.AddCell(colPrecioCompraTmp);
                    tabla.AddCell(colDescuetoTmp);
                    tabla.AddCell(colPrecioVentaTmp);
                    tabla.AddCell(colFechaOperacionTmp);
                    tabla.AddCell(colProveedorTmp);
                    tabla.AddCell(colRFCTmp);
                    tabla.AddCell(colNumReporteTmp);
                }

                if (unidadesCompradasReporte > 0 || precioCompraReporte > 0)
                {

                    PdfPCell colNoConceptoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    colNoConceptoTmpExtra.BorderWidth = 0;
                    colNoConceptoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colProductoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    colProductoTmpExtra.BorderWidth = 0;
                    colProductoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colUnidadesTmpExtra = new PdfPCell(new Phrase(unidadesCompradasReporte.ToString("N2"), fuenteNormal));
                    colUnidadesTmpExtra.BorderWidthTop = 0;
                    colUnidadesTmpExtra.BorderWidthLeft = 0;
                    colUnidadesTmpExtra.BorderWidthRight = 0;
                    colUnidadesTmpExtra.BorderWidthBottom = 1;
                    colUnidadesTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                    colUnidadesTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colPrecioCompraTmpExtra = new PdfPCell(new Phrase(precioCompraReporte.ToString("C"), fuenteNormal));
                    colPrecioCompraTmpExtra.BorderWidthTop = 0;
                    colPrecioCompraTmpExtra.BorderWidthLeft = 0;
                    colPrecioCompraTmpExtra.BorderWidthRight = 0;
                    colPrecioCompraTmpExtra.BorderWidthBottom = 1;
                    colPrecioCompraTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                    colPrecioCompraTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDescuentoTempExtra = new PdfPCell(new Phrase(descuentoReporte.ToString("C"), fuenteNormal));
                    colDescuentoTempExtra.BorderWidthTop = 0;
                    colDescuentoTempExtra.BorderWidthLeft = 0;
                    colDescuentoTempExtra.BorderWidthRight = 0;
                    colDescuentoTempExtra.BorderWidthBottom = 1;
                    colDescuentoTempExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                    colDescuentoTempExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colPrecioVentaTempExtra = new PdfPCell(new Phrase(precioVentaReporte.ToString("C"), fuenteNormal));
                    colPrecioVentaTempExtra.BorderWidthTop = 0;
                    colPrecioVentaTempExtra.BorderWidthLeft = 0;
                    colPrecioVentaTempExtra.BorderWidthRight = 0;
                    colPrecioVentaTempExtra.BorderWidthBottom = 1;
                    colPrecioVentaTempExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                    colPrecioVentaTempExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colFechaOperacionTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    colFechaOperacionTmpExtra.BorderWidth = 0;
                    colFechaOperacionTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colProveedorTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    colProveedorTmpExtra.BorderWidth = 0;
                    colProveedorTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colRFCTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    colRFCTmpExtra.BorderWidth = 0;
                    colRFCTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colReporteTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    colReporteTmpExtra.BorderWidth = 0;
                    colReporteTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                    tabla.AddCell(colNoConceptoTmpExtra);
                    tabla.AddCell(colProductoTmpExtra);
                    tabla.AddCell(colUnidadesTmpExtra);
                    tabla.AddCell(colPrecioCompraTmpExtra);
                    tabla.AddCell(colDescuentoTempExtra);
                    tabla.AddCell(colPrecioVentaTempExtra);
                    tabla.AddCell(colFechaOperacionTmpExtra);
                    tabla.AddCell(colProveedorTmpExtra);
                    tabla.AddCell(colRFCTmpExtra);
                    tabla.AddCell(colReporteTmpExtra);
                }

                /******************************************
                ** Fin de la tabla                      **
                ******************************************/

                reporte.Add(titulo);
                reporte.Add(Usuario);
                reporte.Add(subTitulo);
                reporte.Add(tabla);

                reporte.AddTitle("Reporte Historial");
                reporte.AddAuthor("PUDVE");
                reporte.Close();
                writer.Close();

                VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
                vr.ShowDialog();
            }
            #endregion
        }

        private void btnRevisar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.ShowDialog();
            }
        }

        private void btnActualizar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.ShowDialog();
            }
        }

        private void btnActualizarXML_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.ShowDialog();
            }
        }

        private void bntTerminar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.ShowDialog();
            }
        }

        private void btnBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.ShowDialog();
            }
        }

        private void rbAumentarProducto_CheckedChanged(object sender, EventArgs e)
        {
            var num = NoRevAumentarInventario();
            DGVInventario.Rows.Clear();
            populateAumentarDGVInventario();
            txtBusqueda.Focus();
        }

        private void rbDisminuirProducto_CheckedChanged(object sender, EventArgs e)
        {
            var num = NoRevDisminuirInventario();
            DGVInventario.Rows.Clear();
            populateDisminuirDGVInventario();
            txtBusqueda.Focus();
        }

        private void btnConceptosReporte_Click(object sender, EventArgs e)
        {
            SeleccionarConceptosReporteActualizarInventario SCRA = new SeleccionarConceptosReporteActualizarInventario();

            SCRA.FormClosed += delegate
            {
                ConceptosSeleccionados();
            };

            SCRA.ShowDialog();
        }

        private void ConceptosSeleccionados()
        {
            columnasConcepto = 0;
            iniciarVariablesBool();

            if (!listaConceptosSeleccionados.Count.Equals(0))
            {
                foreach (var item in listaConceptosSeleccionados)
                {
                    //MessageBox.Show("Concepto: " + item.ToString());
                    if (item.Equals("Producto") && Producto.Equals(false))
                    {
                        Producto = true;
                        columnasConcepto++;
                    }
                    else if (item.Equals("Proveedor") && Proveedor.Equals(false))
                    {
                        Proveedor = true;
                        columnasConcepto++;
                    }
                    else if (item.Equals("Unidades Compradas/Disminuidas"))
                    {
                        if ((rbAumentarProducto.Checked && UnidadesCompradas.Equals(false)) ||
                            (rbDisminuirProducto.Checked && UnidadesCompradas.Equals(false)))
                        {
                            UnidadesCompradas = true;
                            columnasConcepto++;
                        }
                    }
                    else if (item.Equals("Precio Compra") && PrecioCompra.Equals(false))
                    {
                        PrecioCompra = true;
                        columnasConcepto++;
                    }
                    else if (item.Equals("Precio Venta") && PrecioVenta.Equals(false))
                    {
                        PrecioVenta = true;
                        columnasConcepto++;
                    }
                    else if (item.Equals("Stock Anterior") && StockAnterior.Equals(false))
                    {
                        StockAnterior = true;
                        columnasConcepto++;
                    }
                    else if (item.Equals("Stock Actual") && StockActual.Equals(false))
                    {
                        StockActual = true;
                        columnasConcepto++;
                    }
                    else if (item.Equals("Fecha de Compra") && FechaCompra.Equals(false))
                    {
                        FechaCompra = true;
                        columnasConcepto++;
                    }
                    else if (item.Equals("Fecha de Operacion") && FechaOperacion.Equals(false))
                    {
                        FechaOperacion = true;
                        columnasConcepto++;
                    }
                    else if (item.Equals("Comentarios") && Comentario.Equals(false))
                    {
                        Comentario = true;
                        columnasConcepto++;
                    }
                }
            }
            else if (listaConceptosSeleccionados.Count.Equals(0))
            {
                columnasConcepto = 10;
            }
        }

        private void iniciarVariablesBool()
        {
            Producto = false;
            Proveedor = false;
            UnidadesCompradas = false;
            PrecioCompra = false;
            PrecioVenta = false;
            StockAnterior = false;
            StockActual = false;
            FechaCompra = false;
            FechaOperacion = false;
            Comentario = false;
        }
    }
}
