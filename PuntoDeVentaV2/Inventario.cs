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

        List<string> idProductoDelCombo;

        public int GetNumRevActive { get; set; }

        // Permisos de los botones
        int opcion1 = 1; // Boton revisar inventario
        int opcion2 = 1; // Boton actualizar inventario
        int opcion3 = 1; // Boton actualizar inventario XML
        int opcion4 = 1; // Boton buscar
        int opcion5 = 1; // Boton terminar

        public Inventario()
        {
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
                        string filtradoParaRealizar = filtro.tipoFiltro;

                        if (filtradoParaRealizar.Equals("Filtros"))
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
                                reporte.Show();
                            };

                            revisar.Show();
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
                                reporte.Show();
                            };

                            revisar.Show();
                        }
                    }
                };

                filtro.Show();
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
                panelContenedor.Visible = false;

                AgregarStockXML inventarioXML = new AgregarStockXML();

                inventarioXML.FormClosed += delegate
                {
                    GenerarReporte(idReporte);

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

                        // si es que encontro algún Combo relacionado
                        if (idProducto > 0)
                        {
                            // Almacenamos los datos del Combo
                            var datosCombo = mb.BuscarProductosDeServicios(Convert.ToString(idProducto));
                            if (!datosCombo.Equals(null) || datosCombo.Count() > 0)
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

                        // si es que encontro algún Combo relacionado
                        if (idProducto > 0)
                        {
                            // Almacenamos los datos del Combo
                            var datosCombo = mb.BuscarProductosDeServicios(Convert.ToString(idProducto));
                            if (!datosCombo.Equals(null) || datosCombo.Count() > 0)
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

                        // si es que encontro algún Combo relacionado
                        if (idProducto > 0)
                        {
                            // Almacenamos los datos del Combo
                            var datosCombo = mb.BuscarProductosDeServicios(Convert.ToString(idProducto));
                            if (!datosCombo.Equals(null) || datosCombo.Count() > 0)
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
                                    DialogResult result = MessageBox.Show("El Código o Clave buscada pertenece a un combo\nEl producto relacionado es:\n\n" + nombresProductos[0].ToString() + "\n\nDesea actualizar el Stock",
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

                                    MessageBox.Show("Resultado del Código o Clave buscada pertenece a un combo;\nel cual contiene más de un Producto por favor debe de realizar\nla actualización de cada uno de ellos:\n\n" + message,
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
                    AjustarProducto ap = new AjustarProducto(idProducto, 2);

                    ap.FormClosed += delegate
                    {
                        if (botonAceptar)
                        {
                            txtBusqueda.Text = string.Empty;
                            var producto = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                            AgregarProductoDGV(producto);
                            botonAceptar = false;
                            idProductoDelCombo.Clear();
                        }
                    };

                    if (idProductoDelCombo.Count > 1)
                    {
                        if (datosSeparados.Length > 1)
                        {
                            ap.cantidadPasadaProductoCombo = Convert.ToInt32(datosSeparados[0].ToString().Trim()) * Convert.ToInt32(idProductoDelCombo[1].ToString());
                        }
                        else if (datosSeparados.Length == 1)
                        {
                            ap.cantidadPasadaProductoCombo = Convert.ToInt32(idProductoDelCombo[1].ToString()) * 1;
                        }
                    }
                    if (idProductoDelCombo.Count == 0)
                    {
                        if (datosSeparados.Length > 1)
                        {
                            ap.cantidadPasadaProductoCombo = Convert.ToInt32(datosSeparados[0].ToString().Trim()) * 1;
                        }
                        else if (datosSeparados.Length == 1)
                        {
                            ap.cantidadPasadaProductoCombo = 0;
                        }
                    }
                    ap.ShowDialog();
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

            AjustarProducto ap = new AjustarProducto(idProducto, 2);

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

            if (!aumentar.Equals("0"))
            {
                diferenciaUnidades = aumentar;
            }
            else if (!decrementar.Equals("0"))
            {
                diferenciaUnidades = decrementar;
            }

            DGVInventario.Rows.Add(id, nombre, stockActual, diferenciaUnidades, nuevoStock, precio, clave, codigo, fecha);

            if (!aumentar.Equals("0"))
            {
                DGVInventario.Rows[DGVInventario.RowCount - 1].Cells[3].Style.ForeColor = Color.DodgerBlue;
            }
            else if (!decrementar.Equals("0"))
            {
                DGVInventario.Rows[DGVInventario.RowCount - 1].Cells[3].Style.ForeColor = Color.OrangeRed;
            }

            DGVInventario.Rows[DGVInventario.RowCount - 1].Cells[3].Style.Font = new System.Drawing.Font(DGVInventario.Font, FontStyle.Bold);
            DGVInventario.Sort(DGVInventario.Columns["Fecha"], ListSortDirection.Descending);
            DGVInventario.ClearSelection(); 
        }

        private void bntTerminar_Click(object sender, EventArgs e)
        {
            if (opcion5 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Utilidades.AdobeReaderInstalado())
            {
                GenerarReporte(idReporte);
            }
            else
            {
                Utilidades.MensajeAdobeReader();
            }

            DGVInventario.Rows.Clear();

            idReporte++;
        }

        private void GenerarReporte(int idReporte)
        {
            var datos = FormPrincipal.datosUsuario;

            var colorFuenteNegrita = new BaseColor(Color.Black);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            int anchoLogo = 110;
            int altoLogo = 60;

            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Historial\reporte_actualizar_inventario_" + idReporte + ".pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\Historial\reporte_actualizar_inventario_" + idReporte + ".pdf";
            }

            float[] anchoColumnas = new float[] { 245f, 200f, 80f, 70f, 70f, 55f, 80f, 80f };

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
            Paragraph subTitulo = new Paragraph("REPORTE ACTUALIZAR INVENTARIO\nFecha: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);
            //Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;
            //domicilio.Alignment = Element.ALIGN_CENTER;
            //domicilio.SetLeading(10, 0);

            /***************************************
             ** Tabla con los productos ajustados **
             ***************************************/
            PdfPTable tabla = new PdfPTable(8);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(anchoColumnas);

            PdfPCell colProducto = new PdfPCell(new Phrase("Producto", fuenteNegrita));
            colProducto.BorderWidth = 1;
            colProducto.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colProveedor = new PdfPCell(new Phrase("Proveedor", fuenteNegrita));
            colProveedor.BorderWidth = 1;
            colProveedor.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colUnidades = new PdfPCell(new Phrase("Unidades compradas", fuenteNegrita));
            colUnidades.BorderWidth = 1;
            colUnidades.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colPrecioCompra = new PdfPCell(new Phrase("Precio compra", fuenteNegrita));
            colPrecioCompra.BorderWidth = 1;
            colPrecioCompra.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colPrecioVenta = new PdfPCell(new Phrase("Precio venta", fuenteNegrita));
            colPrecioVenta.BorderWidth = 1;
            colPrecioVenta.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colStock = new PdfPCell(new Phrase("Stock actual", fuenteNegrita));
            colStock.BorderWidth = 1;
            colStock.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colFechaCompra = new PdfPCell(new Phrase("Fecha de compra", fuenteNegrita));
            colFechaCompra.BorderWidth = 1;
            colFechaCompra.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colFechaOperacion = new PdfPCell(new Phrase("Fecha de operación", fuenteNegrita));
            colFechaOperacion.BorderWidth = 1;
            colFechaOperacion.HorizontalAlignment = Element.ALIGN_CENTER;

            tabla.AddCell(colProducto);
            tabla.AddCell(colProveedor);
            tabla.AddCell(colUnidades);
            tabla.AddCell(colPrecioCompra);
            tabla.AddCell(colPrecioVenta);
            tabla.AddCell(colStock);
            tabla.AddCell(colFechaCompra);
            tabla.AddCell(colFechaOperacion);


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
            sql_cmd = new MySqlCommand($"SELECT * FROM HistorialCompras WHERE IDUsuario = {FormPrincipal.userID} AND IDReporte = {idReporte}", sql_con);
            dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                var idProducto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDProducto")));
                var producto = dr.GetValue(dr.GetOrdinal("Concepto")).ToString();
                var proveedor = dr.GetValue(dr.GetOrdinal("NomEmisor")).ToString();
                var unidades = dr.GetValue(dr.GetOrdinal("Cantidad")).ToString();
                var compra = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ValorUnitario"))).ToString("0.00");
                var venta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Precio"))).ToString("0.00");

                var tmp = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                var stock = tmp[4];

                DateTime fecha = (DateTime)dr.GetValue(dr.GetOrdinal("FechaLarga"));
                var fechaCompra = fecha.ToString("yyyy-MM-dd");

                DateTime fechaOp = (DateTime)dr.GetValue(dr.GetOrdinal("FechaOperacion"));
                var fechaOperacion = fechaOp.ToString("yyyy-MM-dd HH:mm tt");

                PdfPCell colProductoTmp = new PdfPCell(new Phrase(producto, fuenteNormal));
                colProductoTmp.BorderWidth = 1;
                colProductoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colProveedorTmp = new PdfPCell(new Phrase(proveedor, fuenteNormal));
                colProveedorTmp.BorderWidth = 1;
                colProveedorTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colUnidadesTmp = new PdfPCell(new Phrase(unidades, fuenteNormal));
                colUnidadesTmp.BorderWidth = 1;
                colUnidadesTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioCompraTmp = new PdfPCell(new Phrase("$" + compra, fuenteNormal));
                colPrecioCompraTmp.BorderWidth = 1;
                colPrecioCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioVentaTmp = new PdfPCell(new Phrase("$" + venta, fuenteNormal));
                colPrecioVentaTmp.BorderWidth = 1;
                colPrecioVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockTmp = new PdfPCell(new Phrase(stock, fuenteNormal));
                colStockTmp.BorderWidth = 1;
                colStockTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaCompraTmp = new PdfPCell(new Phrase(fechaCompra, fuenteNormal));
                colFechaCompraTmp.BorderWidth = 1;
                colFechaCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaOperacionTmp = new PdfPCell(new Phrase(fechaOperacion, fuenteNormal));
                colFechaOperacionTmp.BorderWidth = 1;
                colFechaOperacionTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tabla.AddCell(colProductoTmp);
                tabla.AddCell(colProveedorTmp);
                tabla.AddCell(colUnidadesTmp);
                tabla.AddCell(colPrecioCompraTmp);
                tabla.AddCell(colPrecioVentaTmp);
                tabla.AddCell(colStockTmp);
                tabla.AddCell(colFechaCompraTmp);
                tabla.AddCell(colFechaOperacionTmp);
            }

            /******************************************
             ** Fin de la tabla                      **
             ******************************************/

            reporte.Add(titulo);
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
    }
}
