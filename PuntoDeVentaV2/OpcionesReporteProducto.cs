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

        private Dictionary<string, string> opcionesDefault;
        private List<string> seleccionados;
        private List<string> ultimos;
        private string ultimoSeleccionado = string.Empty;
        private int contador = 1;

        public Dictionary<string, Tuple<string, float>> filtros;

        public OpcionesReporteProducto()
        {
            InitializeComponent();
        }

        private void OpcionesReporteProducto_Load(object sender, EventArgs e)
        {
            filtros = new Dictionary<string, Tuple<string, float>>();
            ultimos = new List<string>();
            seleccionados = new List<string>();
            opcionesDefault = new Dictionary<string, string>();

            opcionesDefault.Add("Nombre", "Nombre producto");
            opcionesDefault.Add("Precio", "Precio de venta");
            opcionesDefault.Add("PrecioCompra", "Precio de compra");
            opcionesDefault.Add("Stock", "Stock");
            opcionesDefault.Add("StockMinimo", "Stock mínimo");
            opcionesDefault.Add("StockNecesario", "Stock máximo");
            opcionesDefault.Add("ClaveInterna", "Clave de producto");
            opcionesDefault.Add("CodigoBarras", "Código de barras");
            opcionesDefault.Add("CodigoBarraExtra", "Código de barras extra");
            opcionesDefault.Add("Proveedor", "Proveedor");
            opcionesDefault.Add("CantidadPedir", "Cantidad a pedir");

            ObtenerDetalles();
            VisualizarDetalles();
        }

        private void ObtenerDetalles()
        {
            XmlDocument xmlDoc = new XmlDocument();

            if (Properties.Settings.Default.TipoEjecucion == 1)
            {
                xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            }

            if (Properties.Settings.Default.TipoEjecucion == 2)
            {
                xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            }

            // Obtenemos el nodo principal de las propiedades del archivo App.config
            XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

            //======================================================================
            //======================================================================
            foreach (XmlNode childNode in appSettingsNode)
            {
                var key = childNode.Attributes["key"].Value;
                var value = Convert.ToBoolean(childNode.Attributes["value"].Value);

                // Ignoramos los checkbox secundarios de cada propiedad
                if (key.Substring(0, 3) == "chk")
                {
                    continue;
                }

                // Si el valor de la propiedad es true (esta habilitado)
                if (value == true)
                {
                    // Este valor de proveedor esta agregado por defecto
                    if (key == "Proveedor")
                    {
                        continue;
                    }
                    else
                    {
                        opcionesDefault.Add(key, key);
                    }
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
                    lbCustom.Text = opcion.Value;
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
                Dictionary<string, string> opcionesFinales = new Dictionary<string, string>();

                foreach (var opcion in seleccionados)
                {
                    if (opcionesDefault.ContainsKey(opcion))
                    {
                        opcionesFinales.Add(opcion, opcionesDefault[opcion]);
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

        private void GenerarReporte(Dictionary<string, string> opciones)
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
            var consulta = $"SELECT * FROM Productos WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND Tipo = 'P' ORDER BY Nombre ASC";
            var listaProductos = cn.CargarDatos(consulta);


            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
            Paragraph subTitulo = new Paragraph("REPORTE DE PRODUCTOS\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;

            float[] anchoColumnas = new float[] { 270f, 80f, 80f, 80f, 80f, 90f, 70f, 70f, 80f, 100f };

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //===========================
            //=== TABLA DE PRODUCTOS  ===
            //===========================

            PdfPTable tablaProductos = new PdfPTable(opciones.Count);
            tablaProductos.WidthPercentage = 100;
            //tablaProductos.SetWidths(anchoColumnas);

            // Se generan las columnas dinamicamente
            foreach (var opcion in opciones)
            {
                PdfPCell colCustom = new PdfPCell(new Phrase(opcion.Value, fuenteTotales));
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
                        if (filtro.Key == "Stock" || filtro.Key == "StockMinimo" || filtro.Key == "StockNecesario" || filtro.Key == "Precio")
                        {
                            var cantidad = float.Parse(listaProductos.Rows[i][filtro.Key].ToString());

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
                        else if (opcion.Key == "PrecioCompra")
                        {
                            var precioCompraTmp = float.Parse(valor);

                            if (precioCompraTmp == 0)
                            {
                                var precioCompra = float.Parse(listaProductos.Rows[i]["Precio"].ToString()) / 1.60;
                                valor = precioCompra.ToString("N2");
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
                            // Cuando son los valores del App.config
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

        private void btnFiltroReporte_Click(object sender, EventArgs e)
        {
            using (var filtroReporte = new FiltroReporteProductos())
            {
                var resultado = filtroReporte.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    this.filtros = filtroReporte.filtros;
                }
            }
        }
    }
}
