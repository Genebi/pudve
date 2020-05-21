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

namespace PuntoDeVentaV2
{
    public partial class ReporteFinalRevisarInventario : Form
    {
        Conexion cn = new Conexion();
        DataTable dtFinalReportCheckStockToDay;

        MetodosBusquedas mb = new MetodosBusquedas();

        public static int FilterNumActiveRecord;

        public int GetFilterNumActiveRecord { get; set; }
        public bool limpiarTabla { get; set; }

        bool IsEmpty;
        int CantidadAlmacen, CantidadFisico, Diferencias;
        string queryFiltroReporteStock, tabla, queryUpdateCalculos, FechaRevision, soloFechaRevision, fechaActual;

        string consultaStockPaqServ = string.Empty;
        string nombrePC = string.Empty;

        private void FiltroNumRevisionActiva()
        {
            FilterNumActiveRecord = GetFilterNumActiveRecord;
        }

        public ReporteFinalRevisarInventario()
        {
            InitializeComponent();
        }

        private void ReporteFinalRevisarInventario_Load(object sender, EventArgs e)
        {
            nombrePC = Environment.MachineName;

            IsEmpty = false;
            tabla = "RevisarInventario";
            FiltroNumRevisionActiva();
            cargarTabla();
            checkEmpty(tabla);
            llenarDataGriView();

            if (IsEmpty)
            {
                OcultarColumnasDGV();
                hacerCalculosDGVRevisionStock();
            }
            
            if (limpiarTabla)
            {
                VaciarTabla();

                if (Utilidades.AdobeReaderInstalado())
                {
                    GenerarReporte();
                }
                else
                {
                    Utilidades.MensajeAdobeReader();
                }
            }
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            if (DGVRevisionStock.RowCount > 0)
            {
                if (Utilidades.AdobeReaderInstalado())
                {
                    GenerarReporte();
                }
                else
                {
                    Utilidades.MensajeAdobeReader();
                }
            }
        }

        private void VaciarTabla()
        {
            cn.EjecutarConsulta($"DELETE FROM RevisarInventario WHERE NoRevision = {FilterNumActiveRecord} AND IDUsuario = {FormPrincipal.userID} AND IDComputadora = '{nombrePC}'");
        }

        private void hacerCalculosDGVRevisionStock()
        {
            DGVRevisionStock.Columns["IDAlmacen"].Width = 65;
            DGVRevisionStock.Columns["Nombre"].Width = 190;
            DGVRevisionStock.Columns["ClaveInterna"].Width = 65;
            DGVRevisionStock.Columns["CodigoBarras"].Width = 65;
            DGVRevisionStock.Columns["StockAlmacen"].Width = 65;
            DGVRevisionStock.Columns["StockFisico"].Width = 65;
            DGVRevisionStock.Columns["Fecha"].Width = 90;
            DGVRevisionStock.Columns["Vendido"].Width = 65;
            DGVRevisionStock.Columns["Diferencia"].Width = 65;

            foreach (DataGridViewRow row in DGVRevisionStock.Rows)
            {
                CantidadAlmacen = Convert.ToInt32(row.Cells["StockAlmacen"].Value);
                CantidadFisico = Convert.ToInt32(row.Cells["StockFisico"].Value);
                FechaRevision = row.Cells["Fecha"].Value.ToString();
                soloFechaRevision = FechaRevision.Substring(0, 10);
                fechaActual = DateTime.Now.ToString("dd/MM/yyyy");
                Diferencias = CantidadFisico - CantidadAlmacen;
                row.Cells["Diferencia"].Value = Diferencias;

                var precioProducto = float.Parse(row.Cells["PrecioProducto"].Value.ToString());
                var cantidadPerdida = 0f;
                var cantidadRecuperada = 0f;

                // Si es negativa
                if (Diferencias < 0)
                {
                    var diferenciaTmp = Math.Abs(Diferencias);
                    cantidadPerdida = diferenciaTmp * precioProducto;
                    row.Cells["Perdida"].Value = cantidadPerdida.ToString("0.00");
                    row.Cells["Recuperada"].Value = "---";
                }

                // Si es positiva
                if (Diferencias > 0)
                {
                    cantidadRecuperada = Diferencias * precioProducto;
                    row.Cells["Recuperada"].Value = cantidadRecuperada.ToString("0.00");
                    row.Cells["Perdida"].Value = "---";
                }

                // Cuando no se le modifica nada un producto y da cero
                if (Diferencias == 0)
                {
                    row.Cells["Recuperada"].Value = "---";
                    row.Cells["Perdida"].Value = "---";
                }

                if (soloFechaRevision == fechaActual)
                {
                    queryUpdateCalculos = $"UPDATE '{tabla}' SET Diferencia = '{row.Cells["Diferencia"].Value.ToString()}' WHERE ID = '{row.Cells["ID"].Value.ToString()}' AND IDComputadora = '{nombrePC}'";
                    cn.EjecutarConsulta(queryUpdateCalculos);
                }
            }
        }

        private void OcultarColumnasDGV()
        {
            // Ocultamos las columnas que no seran de utilidad para el usuario
            DGVRevisionStock.Columns["ID"].Visible = false;
            DGVRevisionStock.Columns["NoRevision"].Visible = false;
            DGVRevisionStock.Columns["IDUsuario"].Visible = false;
            DGVRevisionStock.Columns["Tipo"].Visible = false;
            DGVRevisionStock.Columns["StatusRevision"].Visible = false;
            DGVRevisionStock.Columns["StatusInventariado"].Visible = false;
            DGVRevisionStock.Columns["Vendido"].Visible = false;
            DGVRevisionStock.Columns["IDComputadora"].Visible = false;

            // Cambiamos el texto de la columbas para mejor visualizacion
            DGVRevisionStock.Columns["IDAlmacen"].HeaderText = "ID";
            DGVRevisionStock.Columns["ClaveInterna"].HeaderText = "Clave";
            DGVRevisionStock.Columns["CodigoBarras"].HeaderText = "Código";
            DGVRevisionStock.Columns["StockAlmacen"].HeaderText = "Punto de Venta";
            DGVRevisionStock.Columns["StockFisico"].HeaderText = "Stock Físico";
            DGVRevisionStock.Columns["Fecha"].HeaderText = "Revisado el";
        }

        private void llenarDataGriView()
        {
            if (IsEmpty == true)
            {
                DGVRevisionStock.DataSource = dtFinalReportCheckStockToDay;

                if (DGVRevisionStock.Rows.Count > 0)
                {
                    DataGridViewColumn colPerdida = new DataGridViewTextBoxColumn();
                    colPerdida.HeaderText = "Cantidad perdida";
                    colPerdida.Name = "Perdida";
                    DGVRevisionStock.Columns.Add(colPerdida);

                    DataGridViewColumn colRecuperada = new DataGridViewTextBoxColumn();
                    colRecuperada.HeaderText = "Cantidad recuperada";
                    colRecuperada.Name = "Recuperada";
                    DGVRevisionStock.Columns.Add(colRecuperada);

                    foreach (DataGridViewRow row in DGVRevisionStock.Rows)
                    {
                        var idProducto = Convert.ToInt32(row.Cells["IDAlmacen"].Value);
                        
                        cn.EjecutarConsulta($"UPDATE Productos SET NumeroRevision = {FilterNumActiveRecord} WHERE ID = {idProducto} AND IDUsuario = {FormPrincipal.userID}");

                        var infoGetPaqServ = mb.ObtenerPaqueteServicioAsignado(Convert.ToInt32(idProducto.ToString()), FormPrincipal.userID);

                        var IsEmptyList = infoGetPaqServ.Length;

                        if (IsEmptyList > 0)
                        {
                            consultaStockPaqServ = $"UPDATE Productos SET NumeroRevision = '{infoGetPaqServ[4].ToString()}' WHERE ID = {infoGetPaqServ[6].ToString()} AND IDUsuario = {infoGetPaqServ[0].ToString()}";
                            cn.EjecutarConsulta(consultaStockPaqServ);
                        }
                    }
                }
            }
            else if (IsEmpty == false)
            {
                MessageBox.Show("La base de datos de Checar Stock verificado no tiene registros", "No tiene registros", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void cargarTabla()
        {
            queryFiltroReporteStock = $"SELECT * FROM '{tabla}' WHERE IDUsuario = '{FormPrincipal.userID}' AND NoRevision = '{FilterNumActiveRecord}' AND IDComputadora = '{nombrePC}' ORDER BY Fecha DESC, Nombre ASC";
            //queryFiltroReporteStock = $"SELECT * FROM '{tabla}' WHERE IDUsuario = '{FormPrincipal.userID}' AND StatusInventariado = '1' ORDER BY Fecha DESC, Nombre ASC";
            dtFinalReportCheckStockToDay = cn.CargarDatos(queryFiltroReporteStock);
        }

        private bool checkEmpty(string tabla)
        {
            string queryTableCheck = $"SELECT * FROM '{tabla}'";
            IsEmpty = cn.IsEmptyTable(queryTableCheck);
            return IsEmpty;
        }

        private void GenerarReporte()
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
            //var servidor = Properties.Settings.Default.Hosting;
            //var rutaArchivo = string.Empty;
            /*if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            }*/

            var fechaHoy = DateTime.Now;
            var rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
            Paragraph subTitulo = new Paragraph("REPORTE INVENTARIO\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;


            float[] anchoColumnas = new float[] { 270f, 80f, 80f, 80f, 80f, 90f, 70f, 70f, 80f, 100f };

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //============================
            //=== TABLA DE INVENTARIO  ===
            //============================

            PdfPTable tablaInventario = new PdfPTable(10);
            tablaInventario.WidthPercentage = 100;
            tablaInventario.SetWidths(anchoColumnas);

            PdfPCell colNombre = new PdfPCell(new Phrase("NOMBRE", fuenteTotales));
            colNombre.BorderWidth = 0;
            colNombre.HorizontalAlignment = Element.ALIGN_CENTER;
            colNombre.Padding = 3;
            colNombre.BackgroundColor = new BaseColor(Color.Black);

            PdfPCell colClave = new PdfPCell(new Phrase("CLAVE", fuenteTotales));
            colClave.BorderWidth = 0;
            colClave.HorizontalAlignment = Element.ALIGN_CENTER;
            colClave.Padding = 3;
            colClave.BackgroundColor = new BaseColor(Color.Black);

            PdfPCell colCodigo = new PdfPCell(new Phrase("CÓDIGO", fuenteTotales));
            colCodigo.BorderWidth = 0;
            colCodigo.HorizontalAlignment = Element.ALIGN_CENTER;
            colCodigo.Padding = 3;
            colCodigo.BackgroundColor = new BaseColor(Color.Black);

            PdfPCell colPuntoVenta = new PdfPCell(new Phrase("PUNTO DE VENTA", fuenteTotales));
            colPuntoVenta.BorderWidth = 0;
            colPuntoVenta.HorizontalAlignment = Element.ALIGN_CENTER;
            colPuntoVenta.Padding = 3;
            colPuntoVenta.BackgroundColor = new BaseColor(Color.Black);

            PdfPCell colStockFisico = new PdfPCell(new Phrase("STOCK FISICO", fuenteTotales));
            colStockFisico.BorderWidth = 0;
            colStockFisico.HorizontalAlignment = Element.ALIGN_CENTER;
            colStockFisico.Padding = 3;
            colStockFisico.BackgroundColor = new BaseColor(Color.Black);

            PdfPCell colFecha = new PdfPCell(new Phrase("FECHA", fuenteTotales));
            colFecha.BorderWidth = 0;
            colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            colFecha.Padding = 3;
            colFecha.BackgroundColor = new BaseColor(Color.Black);

            PdfPCell colDiferencia = new PdfPCell(new Phrase("DIFERENCIA", fuenteTotales));
            colDiferencia.BorderWidth = 0;
            colDiferencia.HorizontalAlignment = Element.ALIGN_CENTER;
            colDiferencia.Padding = 3;
            colDiferencia.BackgroundColor = new BaseColor(Color.Black);

            PdfPCell colPrecio = new PdfPCell(new Phrase("PRECIO", fuenteTotales));
            colPrecio.BorderWidth = 0;
            colPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
            colPrecio.Padding = 3;
            colPrecio.BackgroundColor = new BaseColor(Color.Black);

            PdfPCell colPerdida = new PdfPCell(new Phrase("CANTIDAD PERDIDA", fuenteTotales));
            colPerdida.BorderWidth = 0;
            colPerdida.HorizontalAlignment = Element.ALIGN_CENTER;
            colPerdida.Padding = 3;
            colPerdida.BackgroundColor = new BaseColor(Color.Black);

            PdfPCell colRecuperada = new PdfPCell(new Phrase("CANTIDAD RECUPERADA", fuenteTotales));
            colRecuperada.BorderWidth = 0;
            colRecuperada.HorizontalAlignment = Element.ALIGN_CENTER;
            colRecuperada.Padding = 3;
            colRecuperada.BackgroundColor = new BaseColor(Color.Black);

            tablaInventario.AddCell(colNombre);
            tablaInventario.AddCell(colClave);
            tablaInventario.AddCell(colCodigo);
            tablaInventario.AddCell(colPuntoVenta);
            tablaInventario.AddCell(colStockFisico);
            tablaInventario.AddCell(colFecha);
            tablaInventario.AddCell(colDiferencia);
            tablaInventario.AddCell(colPrecio);
            tablaInventario.AddCell(colPerdida);
            tablaInventario.AddCell(colRecuperada);


            foreach (DataGridViewRow row in DGVRevisionStock.Rows)
            {
                var nombre = row.Cells["Nombre"].Value.ToString();
                var clave = row.Cells["ClaveInterna"].Value.ToString();
                var codigo = row.Cells["CodigoBarras"].Value.ToString();
                var almacen = row.Cells["StockAlmacen"].Value.ToString();
                var fisico = row.Cells["StockFisico"].Value.ToString();
                var fecha = row.Cells["Fecha"].Value.ToString();
                var diferencia = row.Cells["Diferencia"].Value.ToString();
                var precio = float.Parse(row.Cells["PrecioProducto"].Value.ToString());
                var perdida = row.Cells["Perdida"].Value.ToString();
                var recuperada = row.Cells["Recuperada"].Value.ToString();

                PdfPCell colNombreTmp = new PdfPCell(new Phrase(nombre, fuenteNormal));
                colNombreTmp.BorderWidth = 0;
                colNombreTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colClaveTmp = new PdfPCell(new Phrase(clave, fuenteNormal));
                colClaveTmp.BorderWidth = 0;
                colClaveTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoTmp = new PdfPCell(new Phrase(codigo, fuenteNormal));
                colCodigoTmp.BorderWidth = 0;
                colCodigoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPuntoVentaTmp = new PdfPCell(new Phrase(almacen, fuenteNormal));
                colPuntoVentaTmp.BorderWidth = 0;
                colPuntoVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmp = new PdfPCell(new Phrase(fisico, fuenteNormal));
                colStockFisicoTmp.BorderWidth = 0;
                colStockFisicoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
                colFechaTmp.BorderWidth = 0;
                colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDiferenciaTmp = new PdfPCell(new Phrase(diferencia, fuenteNormal));
                colDiferenciaTmp.BorderWidth = 0;
                colDiferenciaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmp = new PdfPCell(new Phrase(precio.ToString("0.00"), fuenteNormal));
                colPrecioTmp.BorderWidth = 0;
                colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPerdidaTmp = new PdfPCell(new Phrase(perdida, fuenteNormal));
                colPerdidaTmp.BorderWidth = 0;
                colPerdidaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRecuperadaTmp = new PdfPCell(new Phrase(recuperada, fuenteNormal));
                colRecuperadaTmp.BorderWidth = 0;
                colRecuperadaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNombreTmp);
                tablaInventario.AddCell(colClaveTmp);
                tablaInventario.AddCell(colCodigoTmp);
                tablaInventario.AddCell(colPuntoVentaTmp);
                tablaInventario.AddCell(colStockFisicoTmp);
                tablaInventario.AddCell(colFechaTmp);
                tablaInventario.AddCell(colDiferenciaTmp);
                tablaInventario.AddCell(colPrecioTmp);
                tablaInventario.AddCell(colPerdidaTmp);
                tablaInventario.AddCell(colRecuperadaTmp);
            }

            reporte.Add(titulo);
            reporte.Add(subTitulo);
            reporte.Add(tablaInventario);

            //================================
            //=== FIN TABLA DE INVENTARIO  ===
            //================================

            reporte.AddTitle("Reporte Inventario");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.Show();
        }
    }
}
