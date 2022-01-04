using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
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
using System.Xml;

namespace PuntoDeVentaV2
{
    public partial class OpcionesReporteProducto : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        private Dictionary<string, Tuple<string, float>> opcionesDefault;
        private List<string> seleccionados;
        private List<string> ultimos;
        private string ultimoSeleccionado = string.Empty;

        // Instancia del filtro para el reporte de productos
        FiltroReporteProductos filtroReporte;

        public static Dictionary<string, Tuple<string, float>> filtros;
        public static bool filtroAbierto = false;

        public OpcionesReporteProducto()
        {
            InitializeComponent();
        }

        private void OpcionesReporteProducto_Load(object sender, EventArgs e)
        {
            var mostrarClave = FormPrincipal.clave;

            filtroReporte = new FiltroReporteProductos();

            filtros = new Dictionary<string, Tuple<string, float>>();
            ultimos = new List<string>();
            seleccionados = new List<string>();
            opcionesDefault = new Dictionary<string, Tuple<string, float>>();

            opcionesDefault.Add("Nombre", new Tuple<string, float>("Nombre producto", 210));
            opcionesDefault.Add("Precio", new Tuple<string, float>("Precio de venta", 80));
            opcionesDefault.Add("PrecioCompra", new Tuple<string, float>("Precio de compra", 80));
            opcionesDefault.Add("Stock", new Tuple<string, float>("Stock", 80));
            opcionesDefault.Add("StockMinimo", new Tuple<string, float>("Stock mínimo", 80));
            opcionesDefault.Add("StockNecesario", new Tuple<string, float>("Stock máximo", 80));
            if (mostrarClave == 0) { } else if (mostrarClave == 1) { opcionesDefault.Add("ClaveInterna", new Tuple<string, float>("Clave de producto", 70)); }
            opcionesDefault.Add("CodigoBarras", new Tuple<string, float>("Código de barras", 70));
            opcionesDefault.Add("CodigoBarraExtra", new Tuple<string, float>("Código de barras extra", 70));
            opcionesDefault.Add("Proveedor", new Tuple<string, float>("Proveedor", 160));
            opcionesDefault.Add("CantidadPedir", new Tuple<string, float>("Cantidad a pedir", 70));
            opcionesDefault.Add("Tipo", new Tuple<string, float>("Tipo", 40));
            opcionesDefault.Add("NumeroRevision", new Tuple<string, float>("Número de Revisión", 60));

            ObtenerDetalles();
            VisualizarDetalles();
        }

        private void ObtenerDetalles()
        {
            var conceptos = mb.ConceptosAppSettings();

            if (conceptos.Count > 0)
            {
                foreach (var concepto in conceptos)
                {
                    if (concepto == "Proveedor")
                    {
                        continue;
                    }

                    opcionesDefault.Add(concepto, new Tuple<string, float>(concepto.Replace("_"," "), 80));
                }
            }
        }

        private void VisualizarDetalles()
        {
            if (opcionesDefault.Count > 0)
            {
                int alturaEjeY = 10;

                foreach (var opcion in opcionesDefault)
                {
                    FlowLayoutPanel panelCustom = new FlowLayoutPanel();
                    panelCustom.Name = opcion.Key;
                    panelCustom.Tag = "panel" + opcion.Key;
                    panelCustom.Width = 350;
                    panelCustom.Height = 30;
                    panelCustom.FlowDirection = FlowDirection.LeftToRight;
                    panelCustom.Location = new Point(3, alturaEjeY);
                    //panelCustom.BorderStyle = BorderStyle.FixedSingle;

                    CheckBox cbCustom = new CheckBox();
                    cbCustom.Name = opcion.Key;
                    cbCustom.Tag = "cb" + opcion.Key;
                    cbCustom.AutoSize = true;
                    cbCustom.CheckAlign = ContentAlignment.MiddleLeft;
                    cbCustom.CheckedChanged += cbCustom_CheckedChanged;

                    Label lbCustom = new Label();
                    lbCustom.Text = opcion.Value.Item1;
                    lbCustom.Name = opcion.Key;
                    lbCustom.Tag = "lb" + opcion.Key;
                    lbCustom.Width = 200;
                    lbCustom.Height = 20;
                    lbCustom.TextAlign = ContentAlignment.MiddleLeft;

                    panelCustom.Controls.Add(cbCustom);
                    panelCustom.Controls.Add(lbCustom);
                    panelContenedor.Controls.Add(panelCustom);

                    alturaEjeY += 30;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (seleccionados.Count > 0)
            {
                if (!Utilidades.AdobeReaderInstalado())
                {
                    Utilidades.MensajeAdobeReader();
                    return;
                }

                Dictionary<string, Tuple<string, float>> opcionesFinales = new Dictionary<string, Tuple<string, float>>();

                foreach (var opcion in seleccionados)
                {
                    if (opcionesDefault.ContainsKey(opcion))
                    {
                        opcionesFinales.Add(opcion, new Tuple<string, float>(opcionesDefault[opcion].Item1, opcionesDefault[opcion].Item2));
                    }
                }

                GenerarReporte(opcionesFinales);
            }
            else
            {
                MessageBox.Show("Es necesario seleccionar al menos una opción", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbCustom_CheckedChanged(object sender, EventArgs e)
        {
            var checkbox = (CheckBox)sender;

            if (checkbox.Checked)
            {
                // Agregamos el nombre de la opcion marcada en el checkbox en caso de que no exista
                // previamente en la lista
                if (!seleccionados.Contains(checkbox.Name))
                {
                    seleccionados.Add(checkbox.Name);
                }

                // Si la lista de seleccionados tiene mas de un elemento
                if (seleccionados.Count > 1)
                {
                    // Comprueba que no este en la lista de los ultimos que han sido agregados
                    if (!ultimos.Contains(checkbox.Name))
                    {
                        // Se obtiene el penultimo agregado en la lista de seleccionados para saber
                        // cual sera el que se va a remover de la lista de ultimos al momento de desmarcar
                        // un checkbox y saber cual sera el proximo que será habilitado
                        var penultimoAgregado = seleccionados[seleccionados.Count - 2];

                        ultimos.Add(penultimoAgregado);
                    }
                }

                foreach (Control panelHijo in panelContenedor.Controls)
                {
                    foreach (Control cb in panelHijo.Controls)
                    {
                        // Despues de recorrer los paneles verificamos si el control analizado es un checkbox
                        if (cb is CheckBox)
                        {
                            var cbTmp = (CheckBox)cb;

                            // Si el nombre del checkbox seleccionado es diferentea a los checkbox marcados
                            // previamente los deshabiltamos
                            if (checkbox.Name != cbTmp.Name && cbTmp.Checked == true)
                            {
                                cbTmp.Enabled = false;
                            }
                        }
                    }
                }
            }
            else
            {
                seleccionados.Remove(checkbox.Name);

                // Se comprueba que la lista no este vacia
                if (ultimos.Count > 0)
                {
                    // Obtenemos el nombre del penultimo checkbox marcado
                    var ultimoAgregado = ultimos[ultimos.Count - 1];
                    // Obtenemos el indice de ese checkbox con su nombre
                    var ultimoIndice = ultimos.LastIndexOf(ultimoAgregado);
                    // Finalmente lo removemos de la lista para que no se duplique
                    ultimos.RemoveAt(ultimoIndice);

                    foreach (Control panelHijo in panelContenedor.Controls)
                    {
                        foreach (Control cb in panelHijo.Controls)
                        {
                            if (cb is CheckBox)
                            {
                                var cbTmp = (CheckBox)cb;

                                // Buscamos el penultimo checkbox por su nombre aqui y lo habilitamos
                                if (cbTmp.Name.Equals(ultimoAgregado))
                                {
                                    cbTmp.Enabled = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void GenerarReporte(Dictionary<string, Tuple<string, float>> opciones)
        {
            // Datos del usuario
            var datos = FormPrincipal.datosUsuario;

            // Fuentes y Colores
            var colorFuenteNegrita = new BaseColor(Color.Black);
            var colorFuenteBlanca = new BaseColor(Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, colorFuenteNegrita);

            // Ruta donde se creara el archivo PDF
            var rutaArchivo = @"C:\Archivos PUDVE\Reportes\Pedidos\reporte_pedido_usuario_" + FormPrincipal.userID + "_fecha_" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".pdf";
            var servidor = Properties.Settings.Default.Hosting;
            var fechaHoy = DateTime.Now;

            // Obtenemos todos los productos del usuario ordenado alfabéticamente
            var consulta = string.Empty;

            var numRow = 0;

            float boughtPrice = 0,
                    salesPrice = 0,
                    Stock = 0,
                    minimumStock = 0,
                    maximumStock = 0,
                    QuantityToOrder = 0;

            if (cbSeleccionados.Checked)
            {
                var productos = Productos.productosSeleccionados;

                if (productos.Count() > 0)
                {
                    var extra = "IN (";

                    foreach (var producto in productos)
                    {
                        extra += $"{producto.Key},";
                    }

                    extra = extra.Substring(0, extra.Length - 1);

                    extra += ")";

                    consulta = $"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND ID {extra}";
                }
                else
                {
                    consulta = $"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 ORDER BY Nombre ASC";
                }
            }
            else
            {
                consulta = $"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 ORDER BY Nombre ASC";
            }

            var listaProductos = cn.CargarDatos(consulta);


            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);

            Paragraph Usuario = new Paragraph("");

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

            string conceptosFiltros = string.Empty;

            if (filtros.Count > 0)
            {
                foreach (var filtro in filtros)
                {
                    if (filtro.Key.Equals("Stock"))
                    {
                        if (conceptosFiltros.Equals(string.Empty))
                        {
                            conceptosFiltros += "FILTRADO POR (Stock";
                        }
                        else
                        {
                            conceptosFiltros += ", Stock";
                        }
                    }
                    if (filtro.Key.Equals("StockMinimo"))
                    {
                        if (conceptosFiltros.Equals(string.Empty))
                        {
                            conceptosFiltros += "FILTRADO POR (Stock Minimo";
                        }
                        else
                        {
                            conceptosFiltros += ", Stock Minimo";
                        }
                    }
                    if (filtro.Key.Equals("StockNecesario"))
                    {
                        if (conceptosFiltros.Equals(string.Empty))
                        {
                            conceptosFiltros += "FILTRADO POR (Stock Máximo";
                        }
                        else
                        {
                            conceptosFiltros += ", Stock Máximo";
                        }
                    }
                    if (filtro.Key.Equals("Precio"))
                    {
                        if (conceptosFiltros.Equals(string.Empty))
                        {
                            conceptosFiltros += "FILTRADO POR (Precio";
                        }
                        else
                        {
                            conceptosFiltros += ", Precio";
                        }
                    }
                    if (filtro.Key.Equals("CantidadPedir"))
                    {
                        if (conceptosFiltros.Equals(string.Empty))
                        {
                            conceptosFiltros += "FILTRADO POR (Cantidad a pedir";
                        }
                        else
                        {
                            conceptosFiltros += ", Cantidad a pedir";
                        }
                    }
                    if (filtro.Key.Equals("NumeroRevision"))
                    {
                        if (conceptosFiltros.Equals(string.Empty))
                        {
                            conceptosFiltros += "FILTRADO POR (Número revisión";
                        }
                        else
                        {
                            conceptosFiltros += ", Número revisión";
                        }
                    }
                    if (filtro.Key.Equals("Proveedor"))
                    {
                        if (conceptosFiltros.Equals(string.Empty))
                        {
                            conceptosFiltros += "FILTRADO POR (Proveedor";
                        }
                        else
                        {
                            conceptosFiltros += ", Proveedor";
                        }
                    }
                    if (filtro.Key.Equals("Tipo"))
                    {
                        if (conceptosFiltros.Equals(string.Empty))
                        {
                            conceptosFiltros += "FILTRADO POR (Tipo";
                        }
                        else
                        {
                            conceptosFiltros += ", Tipo";
                        }
                    }
                    if (filtro.Key.Equals("Imagen"))
                    {
                        if (conceptosFiltros.Equals(string.Empty))
                        {
                            conceptosFiltros += "FILTRADO POR (Imagen";
                        }
                        else
                        {
                            conceptosFiltros += ", Imagen";
                        }
                    }
                }
            }
            else
            {
                conceptosFiltros = "general";
            }

            Paragraph subTitulo = new Paragraph(string.Empty);

            if (!conceptosFiltros.Equals("general"))
            {
                subTitulo = new Paragraph("REPORTE DE PRODUCTOS\n" + conceptosFiltros.ToUpper() + ")\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);
            }
            else
            {
                subTitulo = new Paragraph("REPORTE DE PRODUCTOS\n" + conceptosFiltros.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);
            }

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;

            float[] anchoColumnas = new float[opciones.Count + 1];

            anchoColumnas[0] = 30f;

            var contador = 1;

            foreach (var ancho in opciones)
            {
                anchoColumnas[contador] = ancho.Value.Item2;

                contador++;
            }

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //===========================
            //=== TABLA DE PRODUCTOS  ===
            //===========================

            PdfPTable tablaProductos = new PdfPTable(opciones.Count + 1);
            //tablaProductos.WidthPercentage = 100;
            tablaProductos.SetWidths(anchoColumnas);

            PdfPCell colNoProducto = new PdfPCell(new Phrase("No:", fuenteTotales));
            colNoProducto.BorderWidth = 0;
            colNoProducto.HorizontalAlignment = Element.ALIGN_CENTER;
            colNoProducto.Padding = 3;
            colNoProducto.BackgroundColor = new BaseColor(Color.SkyBlue);

            tablaProductos.AddCell(colNoProducto);

            // Se generan las columnas dinamicamente
            foreach (var opcion in opciones)
            {
                PdfPCell colCustom = new PdfPCell(new Phrase(opcion.Value.Item1.Replace("_"," "), fuenteTotales));
                colCustom.BorderWidth = 0;
                colCustom.HorizontalAlignment = Element.ALIGN_CENTER;
                colCustom.Padding = 3;
                colCustom.BackgroundColor = new BaseColor(Color.SkyBlue);

                tablaProductos.AddCell(colCustom);
            }

            // Si es PrecioCompra y CantidadPedir se deben de ignorar ya que estos se calculan manualmente
            // no existen como tal en alguna de las tablas en la base de datos

            var longitud = listaProductos.Rows.Count;

            for (int i = 0; i < longitud; i++)
            {
                var respuesta = true;

                // Comprobar los filtros seleccionados
                if (filtros.Count > 0)
                {
                    foreach (var filtro in filtros)
                    {
                        // Busca el valor de cualquiera de estas columnas y aplica las condiciones
                        // elegidas por el usuario para comparar las cantidades
                        if (filtro.Key == "Stock" || filtro.Key == "StockMinimo" || filtro.Key == "StockNecesario")
                        {
                            var cantidad = float.Parse(listaProductos.Rows[i][filtro.Key].ToString());

                            respuesta = OperadoresComparacion(filtro, cantidad);
                        }
                        else if (filtro.Key == "Precio" || filtro.Key == "NumeroRevision")
                        {
                            var cantidad = float.Parse(listaProductos.Rows[i][filtro.Key].ToString());

                            respuesta = OperadoresComparacion(filtro, cantidad);
                        }
                        else if (filtro.Key == "CantidadPedir")
                        {
                            long cantidad = 0;

                            var stockActual = Convert.ToInt64(listaProductos.Rows[i]["Stock"]);
                            var stockMinimo = Convert.ToInt64(listaProductos.Rows[i]["StockMinimo"]);
                            var stockMaximo = Convert.ToInt64(listaProductos.Rows[i]["StockNecesario"]);

                            if (stockMinimo > stockActual)
                            {
                                var cantidadPedir = stockMaximo - stockActual;

                                cantidad = cantidadPedir;
                            }
                            else
                            {
                                cantidad = 0;
                            }

                            respuesta = OperadoresComparacion(filtro, cantidad);
                        }
                        else if (filtro.Key == "Proveedor")
                        {
                            var idProducto = Convert.ToInt32(listaProductos.Rows[i]["ID"]);
                            var datosProveedor = mb.DetallesProducto(idProducto, FormPrincipal.userID);

                            if (datosProveedor.Length > 0)
                            {
                                // Compara el ID del proveedor (los dos valores son string)
                                respuesta = filtro.Value.Item1.Equals(datosProveedor[1]);
                            }
                            else if (datosProveedor.Length == 0 && Convert.ToInt32(filtro.Value.Item1) == -1)
                            {
                                // Esto es cuando el filtro de proveedor es para obtener los que no tienen proveedor
                                respuesta = true;
                            }
                            else
                            {
                                respuesta = false;
                            }

                        }
                        else if (filtro.Key == "Tipo")
                        {
                            var tipo = listaProductos.Rows[i][filtro.Key].ToString();

                            if (!tipo.Equals(filtro.Value.Item1))
                            {
                                respuesta = false;
                            }
                        }
                        else if (filtro.Key == "Imagen")
                        {
                            var imagen = listaProductos.Rows[i]["ProdImage"].ToString();

                            // Con imagen
                            if (filtro.Value.Item1 == "1")
                            {
                                if (string.IsNullOrWhiteSpace(imagen))
                                {
                                    respuesta = false;
                                }
                            }

                            // Sin imagen
                            if (filtro.Value.Item1 == "0")
                            {
                                if (!string.IsNullOrWhiteSpace(imagen))
                                {
                                    respuesta = false;
                                }
                            }
                        }
                        else
                        {
                            // Busca si el valor del detalle de producto esta asignado a este producto
                            var idProducto = Convert.ToInt32(listaProductos.Rows[i]["ID"]);
                            var detalle = mb.DatosDetallesProducto(idProducto, filtro.Key);

                            respuesta = filtro.Value.Item1.Equals(detalle);
                        }

                        if (respuesta == false)
                        {
                            break;
                        }
                    }
                }

                // Si cualquiera de los filtros devuelve "false" se salta la iteracion actual
                // para buscar en el siguiente producto
                if (respuesta == false)
                {
                    continue;
                }

                numRow++;
                PdfPCell rowNumCount = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                tablaProductos.AddCell(rowNumCount);


                // Verificamos que solo muestre las columnas para el reporte que selecciono el cliente
                foreach (var opcion in opciones)
                {
                    bool isNumber = false;

                    int idProducto = Convert.ToInt32(listaProductos.Rows[i]["ID"]);

                    if (listaProductos.Columns.Contains(opcion.Key))
                    {
                        var valor = listaProductos.Rows[i][opcion.Key].ToString();

                        if (opcion.Key == "Nombre")
                        {
                            PdfPCell rowCustom = new PdfPCell(new Phrase(valor, fuenteNormal));
                            //rowCustom.BorderWidth = 0;
                            //rowCustom.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaProductos.AddCell(rowCustom);
                        }
                        else if (opcion.Key == "Precio")
                        {
                            var precio = float.Parse(listaProductos.Rows[i]["Precio"].ToString());

                            salesPrice += precio;

                            var tipodeMoneda = FormPrincipal.Moneda.Split('-');
                            var moneda = tipodeMoneda[1].ToString().Trim().Replace("(", "").Replace(")", " ");

                            valor = moneda + " " + precio.ToString("N2");

                            PdfPCell rowCustom = new PdfPCell(new Phrase(valor, fuenteNormal));
                            //rowCustom.BorderWidth = 0;
                            rowCustom.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaProductos.AddCell(rowCustom);
                        }
                        else if (opcion.Key == "PrecioCompra")
                        {
                            var precioCompraTmp = float.Parse(valor);

                            boughtPrice += precioCompraTmp;

                            var tipodeMoneda = FormPrincipal.Moneda.Split('-');
                            var moneda = tipodeMoneda[1].ToString().Trim().Replace("(", "").Replace(")", " ");

                            valor = moneda +" "+ valor;

                            if (precioCompraTmp == 0)
                            {
                                valor = "---";
                            }

                            PdfPCell rowCustom = new PdfPCell(new Phrase(valor, fuenteNormal));
                            //rowCustom.BorderWidth = 0;
                            rowCustom.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaProductos.AddCell(rowCustom);
                        }
                        else if (opcion.Key == "ClaveInterna")
                        {
                            if (valor == "")
                            {
                                valor = "---";
                            }

                            PdfPCell rowCustom = new PdfPCell(new Phrase(valor, fuenteNormal));
                            rowCustom.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaProductos.AddCell(rowCustom);
                        }
                        else if (opcion.Key == "Tipo")
                        {
                            var tipo = listaProductos.Rows[i]["Tipo"].ToString();

                            if (tipo.Equals("P"))
                            {
                                tipo = "PRODUCTO";
                            }
                            else if (tipo.Equals("PQ"))
                            {
                                tipo = "COMBO";
                            }
                            else if (tipo.Equals("S"))
                            {
                                tipo = "SERVICIO";
                            }

                            PdfPCell rowCustom = new PdfPCell(new Phrase(tipo, fuenteNormal));
                            //rowCustom.BorderWidth = 0;
                            rowCustom.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaProductos.AddCell(rowCustom);
                        }
                        else if (opcion.Key == "CantidadPedir")
                        {
                            var stockActual = Convert.ToInt32(listaProductos.Rows[i]["Stock"]);
                            var stockMinimo = Convert.ToInt32(listaProductos.Rows[i]["StockMinimo"]);
                            var stockMaximo = Convert.ToInt32(listaProductos.Rows[i]["StockNecesario"]);

                            Stock += (float)stockActual;
                            minimumStock += (float)stockMinimo;
                            maximumStock += (float)stockMaximo;

                            if (stockMinimo > stockActual)
                            {
                                var cantidadPedir = stockMaximo - stockActual;

                                QuantityToOrder += cantidadPedir;

                                valor = cantidadPedir.ToString();
                            }
                            else
                            {
                                valor = "0";
                                QuantityToOrder += (float)Convert.ToDouble(valor);
                            }

                            PdfPCell rowCustom = new PdfPCell(new Phrase(valor, fuenteNormal));
                            //rowCustom.BorderWidth = 0;
                            rowCustom.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaProductos.AddCell(rowCustom);
                        }
                        else
                        {
                            PdfPCell rowCustom = new PdfPCell(new Phrase(valor, fuenteNormal));
                            //rowCustom.BorderWidth = 0;
                            rowCustom.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaProductos.AddCell(rowCustom);
                        }
                    }
                    else
                    {
                        var resultado = string.Empty;

                        if (opcion.Key == "Proveedor")
                        {
                            var datosProveedor = mb.DetallesProducto(idProducto, FormPrincipal.userID);

                            if (datosProveedor.Length > 0)
                            {
                                if (!string.IsNullOrWhiteSpace(datosProveedor[2]))
                                {
                                    resultado = datosProveedor[2];
                                }
                                else
                                {
                                    resultado = "N/A";
                                }
                            }
                            else
                            {
                                resultado = "N/A";
                            }
                        }
                        else if (opcion.Key == "CodigoBarraExtra")
                        {
                            var datosCodigos = mb.ObtenerCodigoBarrasExtras(idProducto, 1);

                            if (datosCodigos.Length > 0)
                            {
                                foreach (var codigo in datosCodigos)
                                {
                                    if (!string.IsNullOrWhiteSpace(codigo))
                                    {
                                        resultado += codigo + "\n";
                                    }
                                }

                                if (string.IsNullOrWhiteSpace(resultado))
                                {
                                    resultado = "N/A";
                                }
                            }
                            else
                            {
                                resultado = "N/A";
                            }
                        }
                        else
                        {
                            // Cuando son los valores de AppSettings
                            resultado = mb.DatosDetallesProducto(idProducto, opcion.Key);
                        }

                        PdfPCell rowCustom = new PdfPCell(new Phrase(resultado, fuenteNormal));
                        //rowCustom.BorderWidth = 0;
                        rowCustom.HorizontalAlignment = Element.ALIGN_CENTER;
                        tablaProductos.AddCell(rowCustom);
                    }
                }
            }

            PdfPCell rowNumCountExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            rowNumCountExtra.BorderWidth = 0;
            rowNumCountExtra.HorizontalAlignment = Element.ALIGN_CENTER;
            tablaProductos.AddCell(rowNumCountExtra);               // 01 No

            foreach (var opcion in opciones)
            {
                if (opcion.Key.Equals("Nombre"))
                {
                    PdfPCell rowNomProdExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    rowNomProdExtra.BorderWidth = 0;
                    rowNomProdExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaProductos.AddCell(rowNomProdExtra);                // 02 Nom Prod
                }
                else if (opcion.Key.Equals("Precio"))
                {
                    PdfPCell rowPrecioVentaProdExtra = new PdfPCell(new Phrase(salesPrice.ToString("C"), fuenteNormal));
                    rowPrecioVentaProdExtra.BorderWidthTop = 0;
                    rowPrecioVentaProdExtra.BorderWidthLeft = 0;
                    rowPrecioVentaProdExtra.BorderWidthRight = 0;
                    rowPrecioVentaProdExtra.BorderWidthBottom = 1;
                    rowPrecioVentaProdExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                    rowPrecioVentaProdExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaProductos.AddCell(rowPrecioVentaProdExtra);        // 03 Precio Venta
                }
                else if (opcion.Key.Equals("PrecioCompra"))
                {
                    PdfPCell rowPrecioCompraProdExtra = new PdfPCell(new Phrase(boughtPrice.ToString("C"), fuenteNormal));
                    rowPrecioCompraProdExtra.BorderWidthTop = 0;
                    rowPrecioCompraProdExtra.BorderWidthLeft = 0;
                    rowPrecioCompraProdExtra.BorderWidthRight = 0;
                    rowPrecioCompraProdExtra.BorderWidthBottom = 1;
                    rowPrecioCompraProdExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                    rowPrecioCompraProdExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaProductos.AddCell(rowPrecioCompraProdExtra);       // 04 Precio Compra
                }
                else if (opcion.Key.Equals("Stock"))
                {
                    PdfPCell rowStockProdExtra = new PdfPCell(new Phrase(Stock.ToString("N2"), fuenteNormal));
                    rowStockProdExtra.BorderWidthTop = 0;
                    rowStockProdExtra.BorderWidthLeft = 0;
                    rowStockProdExtra.BorderWidthRight = 0;
                    rowStockProdExtra.BorderWidthBottom = 1;
                    rowStockProdExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                    rowStockProdExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaProductos.AddCell(rowStockProdExtra);              // 05 Stock
                }
                else if (opcion.Key.Equals("StockMinimo"))
                {
                    PdfPCell rowMinimumStockProdExtra = new PdfPCell(new Phrase(minimumStock.ToString("N2"), fuenteNormal));
                    rowMinimumStockProdExtra.BorderWidthTop = 0;
                    rowMinimumStockProdExtra.BorderWidthLeft = 0;
                    rowMinimumStockProdExtra.BorderWidthRight = 0;
                    rowMinimumStockProdExtra.BorderWidthBottom = 1;
                    rowMinimumStockProdExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                    rowMinimumStockProdExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaProductos.AddCell(rowMinimumStockProdExtra);       // 06 Stock Minimo
                }
                else if (opcion.Key.Equals("StockNecesario"))
                {
                    PdfPCell rowMaximumStockProdExtra = new PdfPCell(new Phrase(maximumStock.ToString("N2"), fuenteNormal));
                    rowMaximumStockProdExtra.BorderWidthTop = 0;
                    rowMaximumStockProdExtra.BorderWidthLeft = 0;
                    rowMaximumStockProdExtra.BorderWidthRight = 0;
                    rowMaximumStockProdExtra.BorderWidthBottom = 1;
                    rowMaximumStockProdExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                    rowMaximumStockProdExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaProductos.AddCell(rowMaximumStockProdExtra);       // 07 Stock Máximo
                }
                else if (opcion.Key.Equals("ClaveInterna"))
                {
                    PdfPCell rowClaveProdExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    rowClaveProdExtra.BorderWidth = 0;
                    rowClaveProdExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaProductos.AddCell(rowClaveProdExtra);              // 08 Clave Prod
                }
                else if (opcion.Key.Equals("CodigoBarras"))
                {
                    PdfPCell rowBarCodeProdExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    rowBarCodeProdExtra.BorderWidth = 0;
                    rowBarCodeProdExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaProductos.AddCell(rowBarCodeProdExtra);            // 09 Código Barras
                }
                else if (opcion.Key.Equals("CodigoBarraExtra"))
                {
                    PdfPCell rowBarCodePlusProdExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    rowBarCodePlusProdExtra.BorderWidth = 0;
                    rowBarCodePlusProdExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaProductos.AddCell(rowBarCodePlusProdExtra);        // 10 Código barras extra
                }
                else if (opcion.Key.Equals("Proveedor"))
                {
                    PdfPCell rowProveedorProdExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    rowProveedorProdExtra.BorderWidth = 0;
                    rowProveedorProdExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaProductos.AddCell(rowProveedorProdExtra);          // 11 Proveedor
                }
                else if (opcion.Key.Equals("CantidadPedir"))
                {
                    PdfPCell rowQuantityToOrderProdExtra = new PdfPCell(new Phrase(QuantityToOrder.ToString("N2"), fuenteNormal));
                    rowQuantityToOrderProdExtra.BorderWidthTop = 0;
                    rowQuantityToOrderProdExtra.BorderWidthLeft = 0;
                    rowQuantityToOrderProdExtra.BorderWidthRight = 0;
                    rowQuantityToOrderProdExtra.BorderWidthBottom = 1;
                    rowQuantityToOrderProdExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                    rowQuantityToOrderProdExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaProductos.AddCell(rowQuantityToOrderProdExtra);    // 12 Cantidad a pedir
                }
                else if (opcion.Key.Equals("Tipo"))
                {
                    PdfPCell rowTypeProdExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    rowTypeProdExtra.BorderWidth = 0;
                    rowTypeProdExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaProductos.AddCell(rowTypeProdExtra);               // 13 Tipo de Producto
                }
                else if (opcion.Key.Equals("NumeroRevision"))
                {
                    PdfPCell rowIventoryNumProdExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                    rowIventoryNumProdExtra.BorderWidth = 0;
                    rowIventoryNumProdExtra.HorizontalAlignment = Element.ALIGN_CENTER;
                    tablaProductos.AddCell(rowIventoryNumProdExtra);        // 14 Numero Revision
                }
            }

            reporte.Add(titulo);
            reporte.Add(Usuario);
            reporte.Add(subTitulo);
            reporte.Add(tablaProductos);

            //===============================
            //=== FIN TABLA DE PRODUCTOS  ===
            //===============================

            reporte.AddTitle("Reporte Productos");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }


        private bool OperadoresComparacion(KeyValuePair<string, Tuple<string, float>> filtro, float cantidad)
        {
            var respuesta = true;

            if (filtro.Value.Item1 == ">=")
            {
                respuesta = cantidad >= filtro.Value.Item2;
            }
            else if (filtro.Value.Item1 == "<=")
            {
                respuesta = cantidad <= filtro.Value.Item2;
            }
            else if (filtro.Value.Item1 == "==")
            {
                respuesta = cantidad == filtro.Value.Item2;
            }
            else if (filtro.Value.Item1 == ">")
            {
                respuesta = cantidad > filtro.Value.Item2;
            }
            else if (filtro.Value.Item1 == "<")
            {
                respuesta = cantidad < filtro.Value.Item2;
            }

            return respuesta;
        }


        private void btnFiltroReporte_Click(object sender, EventArgs e)
        {
            filtroReporte.ShowDialog();
        }

        private void OpcionesReporteProducto_FormClosing(object sender, FormClosingEventArgs e)
        {
            filtroAbierto = false;
        }

        private void cbSeleccionarTodas_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSeleccionarTodas.Checked)
            {
                MarcarDesmarcar();
            }
            else
            {
                MarcarDesmarcar(false);
            }
        }

        private void MarcarDesmarcar(bool activo = true)
        {
            foreach (Control panel in panelContenedor.Controls)
            {
                foreach (Control cb in panel.Controls)
                {
                    if (cb is CheckBox)
                    {
                        ((CheckBox)cb).Checked = activo;
                    }
                }
            }
        }

        private void OpcionesReporteProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
