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
            filtroReporte = new FiltroReporteProductos();

            filtros = new Dictionary<string, Tuple<string, float>>();
            ultimos = new List<string>();
            seleccionados = new List<string>();
            opcionesDefault = new Dictionary<string, Tuple<string, float>>();

            opcionesDefault.Add("Nombre", new Tuple<string, float>("Nombre producto", 250));
            opcionesDefault.Add("Precio", new Tuple<string, float>("Precio de venta", 60));
            opcionesDefault.Add("PrecioCompra", new Tuple<string, float>("Precio de compra", 60));
            opcionesDefault.Add("Stock", new Tuple<string, float>("Stock", 50));
            opcionesDefault.Add("StockMinimo", new Tuple<string, float>("Stock mínimo", 50));
            opcionesDefault.Add("StockNecesario", new Tuple<string, float>("Stock máximo", 50));
            opcionesDefault.Add("ClaveInterna", new Tuple<string, float>("Clave de producto", 70));
            opcionesDefault.Add("CodigoBarras", new Tuple<string, float>("Código de barras", 70));
            opcionesDefault.Add("CodigoBarraExtra", new Tuple<string, float>("Código de barras extra", 70));
            opcionesDefault.Add("Proveedor", new Tuple<string, float>("Proveedor", 180));
            opcionesDefault.Add("CantidadPedir", new Tuple<string, float>("Cantidad a pedir", 50));
            opcionesDefault.Add("Tipo", new Tuple<string, float>("Tipo", 50));
            opcionesDefault.Add("NumeroRevision", new Tuple<string, float>("Número de Revisión", 70));

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

                    opcionesDefault.Add(concepto, new Tuple<string, float>(concepto, 80));
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
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, colorFuenteBlanca);

            // Ruta donde se creara el archivo PDF
            var rutaArchivo = @"C:\Archivos PUDVE\Reportes\Pedidos\reporte_pedido_usuario_" + FormPrincipal.userID + "_fecha_" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".pdf";
            var servidor = Properties.Settings.Default.Hosting;
            var fechaHoy = DateTime.Now;

            // Obtenemos todos los productos del usuario ordenado alfabéticamente
            var consulta = string.Empty;

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
            Paragraph subTitulo = new Paragraph("REPORTE DE PRODUCTOS\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;

            float[] anchoColumnas = new float[opciones.Count];

            var contador = 0;

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

            PdfPTable tablaProductos = new PdfPTable(opciones.Count);
            //tablaProductos.WidthPercentage = 100;
            tablaProductos.SetWidths(anchoColumnas);

            // Se generan las columnas dinamicamente
            foreach (var opcion in opciones)
            {
                PdfPCell colCustom = new PdfPCell(new Phrase(opcion.Value.Item1, fuenteTotales));
                colCustom.BorderWidth = 0;
                colCustom.HorizontalAlignment = Element.ALIGN_CENTER;
                colCustom.Padding = 3;
                colCustom.BackgroundColor = new BaseColor(Color.Black);

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

                // Verificamos que solo muestre las columnas para el reporte que selecciono el cliente
                foreach (var opcion in opciones)
                {
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

                            valor = "$ " + precio.ToString("N2");

                            PdfPCell rowCustom = new PdfPCell(new Phrase(valor, fuenteNormal));
                            //rowCustom.BorderWidth = 0;
                            rowCustom.HorizontalAlignment = Element.ALIGN_CENTER;
                            tablaProductos.AddCell(rowCustom);
                        }
                        else if (opcion.Key == "PrecioCompra")
                        {
                            var precioCompraTmp = float.Parse(valor);

                            valor = "$ " + valor;

                            if (precioCompraTmp == 0)
                            {
                                valor = "---";
                            }

                            PdfPCell rowCustom = new PdfPCell(new Phrase(valor, fuenteNormal));
                            //rowCustom.BorderWidth = 0;
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

                        if (opcion.Key == "CantidadPedir")
                        {
                            var stockActual = Convert.ToInt32(listaProductos.Rows[i]["Stock"]);
                            var stockMinimo = Convert.ToInt32(listaProductos.Rows[i]["StockMinimo"]);
                            var stockMaximo = Convert.ToInt32(listaProductos.Rows[i]["StockNecesario"]);

                            if (stockMinimo > stockActual)
                            {
                                var cantidadPedir = stockMaximo - stockActual;

                                resultado = cantidadPedir.ToString();
                            }
                            else
                            {
                                resultado = "0";
                            }
                        }
                        else if (opcion.Key == "Proveedor")
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
                            var datosCodigos = mb.ObtenerCodigoBarrasExtras(idProducto);

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

            reporte.Add(titulo);
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
    }
}
