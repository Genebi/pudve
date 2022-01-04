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
        Consultas cs = new Consultas();
        DataTable dtFinalReportCheckStockToDay;

        MetodosBusquedas mb = new MetodosBusquedas();

        public static int FilterNumActiveRecord;

        public int GetFilterNumActiveRecord { get; set; }
        public bool limpiarTabla { get; set; }

        bool IsEmpty;
        float CantidadAlmacen, CantidadFisico, Diferencias;
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
                    var mostrarClave = FormPrincipal.clave;

                    if (mostrarClave == 0)
                    {
                        GenerarReporteSinCLaveInterna();
                    }
                    else if (mostrarClave == 1)
                    {
                        GenerarReporte();
                    }
                }
                else
                {
                    Utilidades.MensajeAdobeReader();
                }
            }
        }

        private void agregarDatosTabla()
        {
            var id = string.Empty;  
		    var idAlmacen = string.Empty;
            var nombre = string.Empty;
            var claveInterna = string.Empty;
            var codigoBarras = string.Empty;
            var stockAlmacen = string.Empty;
            var stockFisico = string.Empty;
            var noRevision = string.Empty;
            var fecha = string.Empty;
            var vendido = string.Empty;
            var diferencia = string.Empty;
            var idUsuario = string.Empty;
            var tipo = string.Empty;
            var statusRevision = string.Empty;
            var statusInventariado = string.Empty;
            var precioProducto = string.Empty;
            var idComputadora = string.Empty;

            var query = cn.CargarDatos($"SELECT * FROM RevisarInventario WHERE IDUsuario = '{FormPrincipal.userID}' AND NoRevision = '{FilterNumActiveRecord}' AND IDComputadora = '{nombrePC}'");

            if (!query.Rows.Count.Equals(0))
            {
                foreach (DataRow iterador in query.Rows)
                {
                    id = iterador["ID"].ToString(); 
                    idAlmacen = iterador["IDAlmacen"].ToString(); 
                    nombre = iterador["Nombre"].ToString(); 
                    claveInterna = iterador["ClaveInterna"].ToString(); 
                    codigoBarras = iterador["CodigoBarras"].ToString(); 
                    stockAlmacen = iterador["StockAlmacen"].ToString(); 
                    stockFisico = iterador["StockFisico"].ToString(); 
                    noRevision = iterador["NoRevision"].ToString(); 
                    fecha = iterador["Fecha"].ToString();
                    DateTime date = Convert.ToDateTime(fecha); 
                    vendido = iterador["Vendido"].ToString(); 
                    diferencia = iterador["Diferencia"].ToString(); 
                    idUsuario = iterador["IDUsuario"].ToString(); 
                    tipo = iterador["Tipo"].ToString(); 
                    statusRevision = iterador["StatusRevision"].ToString(); 
                    statusInventariado = iterador["StatusInventariado"].ToString();
                    precioProducto = iterador["PrecioProducto"].ToString(); 
                    idComputadora = iterador["IDComputadora"].ToString();

                    using (DataTable dtReportesInventario = cn.CargarDatos($"SELECT ID FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{id}'"))
                    {
                        if (dtReportesInventario.Rows.Count.Equals(0))
                        {
                            cn.EjecutarConsulta($"INSERT INTO RevisarInventarioReportes (ID, NameUsr, IDAlmacen, Nombre, ClaveInterna, CodigoBarras, StockAlmacen, StockFisico, NoRevision, Fecha, Vendido, Diferencia, IDUsuario, Tipo, StatusRevision, StatusInventariado, PrecioProducto, IDComputadora) VALUES ('{id}', '{FormPrincipal.userNickName}', '{idAlmacen}','{nombre}','{claveInterna}','{codigoBarras}','{stockAlmacen}','{stockFisico}','{noRevision}','{date.ToString("yyyy-MM-dd hh:mm:ss")}','{vendido}','{diferencia}','{idUsuario}','{tipo}','{statusRevision}','{statusInventariado}','{precioProducto}','{idComputadora}')");
                        }
                    }
                }
            }
        }

        private void btnGenerarPDF_Click(object sender, EventArgs e)
        {
            agregarDatosTabla();

            if (DGVRevisionStock.RowCount > 0)
            {
                if (Utilidades.AdobeReaderInstalado())
                {
                    var mostrarClave = FormPrincipal.clave;

                    if (mostrarClave == 0)
                    {
                        GenerarReporteSinCLaveInterna();
                    }
                    else if (mostrarClave == 1)
                    {
                        GenerarReporte();
                    }
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
            var mostrarClave = FormPrincipal.clave;

            DGVRevisionStock.Columns["IDAlmacen"].Width = 65;
            DGVRevisionStock.Columns["Nombre"].Width = 190;

            if (mostrarClave == 0)
            {
            }
            else if (mostrarClave == 1)
            {
                DGVRevisionStock.Columns["ClaveInterna"].Width = 65;
            }
             
            DGVRevisionStock.Columns["CodigoBarras"].Width = 65;
            DGVRevisionStock.Columns["StockAlmacen"].Width = 65;
            DGVRevisionStock.Columns["StockFisico"].Width = 65;
            DGVRevisionStock.Columns["Fecha"].Width = 90;
            DGVRevisionStock.Columns["Vendido"].Width = 65;
            DGVRevisionStock.Columns["Diferencia"].Width = 65;

            foreach (DataGridViewRow row in DGVRevisionStock.Rows)
            {
                //CantidadAlmacen = Convert.ToInt32(row.Cells["StockAlmacen"].Value);
                //CantidadFisico = Convert.ToInt32(row.Cells["StockFisico"].Value);
                CantidadAlmacen = float.Parse(row.Cells["StockAlmacen"].Value.ToString());
                CantidadFisico = float.Parse(row.Cells["StockFisico"].Value.ToString());
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
                    queryUpdateCalculos = $"UPDATE {tabla} SET Diferencia = '{row.Cells["Diferencia"].Value.ToString()}' WHERE ID = '{row.Cells["ID"].Value.ToString()}' AND IDComputadora = '{nombrePC}'";
                    cn.EjecutarConsulta(queryUpdateCalculos);
                }
            }
        }

        private void OcultarColumnasDGV()
        {
            var mostrarClave = FormPrincipal.clave;

            // Ocultamos las columnas que no seran de utilidad para el usuario
            DGVRevisionStock.Columns["ID"].Visible = false;
            DGVRevisionStock.Columns["NoRevision"].Visible = false;
            DGVRevisionStock.Columns["IDUsuario"].Visible = false;
            DGVRevisionStock.Columns["Tipo"].Visible = false;
            DGVRevisionStock.Columns["StatusRevision"].Visible = false;
            DGVRevisionStock.Columns["StatusInventariado"].Visible = false;
            DGVRevisionStock.Columns["Vendido"].Visible = false;
            DGVRevisionStock.Columns["IDComputadora"].Visible = false;

            if (mostrarClave == 0)
            {
                DGVRevisionStock.Columns["ClaveInterna"].Visible = false;
            }
            else if (mostrarClave == 1)
            {
                DGVRevisionStock.Columns["ClaveInterna"].Visible = true;
            }

            // Cambiamos el texto de la columbas para mejor visualizacion
            DGVRevisionStock.Columns["IDAlmacen"].HeaderText = "ID";
            if (mostrarClave == 0) { } else if (mostrarClave == 1) { DGVRevisionStock.Columns["ClaveInterna"].HeaderText = "Clave"; }
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

                        //row.Cells["StockAlmacen"].Value = Utilidades.RemoverCeroStock(row.Cells["StockAlmacen"].Value.ToString());
                        //row.Cells["StockFisico"].Value = Utilidades.RemoverCeroStock(row.Cells["StockFisico"].Value.ToString());
                        row.Cells["StockAlmacen"].Value = row.Cells["StockAlmacen"].Value.ToString();
                        row.Cells["StockFisico"].Value = row.Cells["StockFisico"].Value.ToString();

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
            queryFiltroReporteStock = $"SELECT * FROM {tabla} WHERE IDUsuario = {FormPrincipal.userID} AND NoRevision = '{FilterNumActiveRecord}' AND IDComputadora = '{nombrePC}' ORDER BY Fecha DESC, Nombre ASC";
            //queryFiltroReporteStock = $"SELECT * FROM '{tabla}' WHERE IDUsuario = '{FormPrincipal.userID}' AND StatusInventariado = '1' ORDER BY Fecha DESC, Nombre ASC";
            dtFinalReportCheckStockToDay = cn.CargarDatos(queryFiltroReporteStock);
        }

        private bool checkEmpty(string tabla)
        {
            string queryTableCheck = $"SELECT * FROM {tabla}";
            IsEmpty = cn.IsEmptyTable(queryTableCheck);
            return IsEmpty;
        }

        private void GenerarReporte()
        {
            var mostrarClave = FormPrincipal.clave;

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

            var numRow = 0;

            // Ruta donde se creara el archivo PDF
            var servidor = Properties.Settings.Default.Hosting;
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
            //var rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            var rutaArchivo = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo += $@"\\{servidor}\Archivos PUDVE\Reportes\RevisarInventario\{FormPrincipal.userNickName}\";
            }
            else
            {
                rutaArchivo = $@"C:\Archivos PUDVE\Reportes\RevisarInventario\{FormPrincipal.userNickName}\";
            }

            if (!Directory.Exists(rutaArchivo))
            {
                Directory.CreateDirectory(rutaArchivo);
                rutaArchivo += $"reporte_inventario_{FilterNumActiveRecord}.pdf";
            }
            else
            {
                rutaArchivo += $"reporte_inventario_{FilterNumActiveRecord}.pdf";
            }

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            string UsuarioActivo = string.Empty;

            string tipoReporte = string.Empty,
                    encabezadoTipoReporte = string.Empty;

            float PuntoDeVenta = 0,
                    StockFisico = 0,
                    Diferencia = 0,
                    Precio = 0,
                    CantidadPerdida = 0,
                    CantidadRecuperada = 0;

            tipoReporte = Inventario.filtradoParaRealizar;

            if (!tipoReporte.Equals(string.Empty))
            {
                if (tipoReporte.Equals("Normal"))
                {
                    encabezadoTipoReporte = "Normal";
                }
                if (tipoReporte.Equals("Stock"))
                {
                    encabezadoTipoReporte = "Stock";
                }
                if (tipoReporte.Equals("StockMinimo"))
                {
                    encabezadoTipoReporte = "Stock Minimo";
                }
                if (tipoReporte.Equals("StockNecesario"))
                {
                    encabezadoTipoReporte = "Stock Necesario";
                }
                if (tipoReporte.Equals("NumeroRevision"))
                {
                    encabezadoTipoReporte = "Numero Revision";
                }
                if (tipoReporte.Equals("Filtros"))
                {
                    encabezadoTipoReporte = "Filtros";
                }
            }

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

            Paragraph subTitulo = new Paragraph("REPORTE DE REVISAR INVENTARIO\nSECCIÓN ELEGIDA " + encabezadoTipoReporte.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;


            float[] anchoColumnas = new float[] { 30f, 270f, 80f, 80f, 80f, 80f, 90f, 70f, 70f, 80f, 100f };

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //============================
            //=== TABLA DE INVENTARIO  ===
            //============================

            PdfPTable tablaInventario = new PdfPTable(11);
            tablaInventario.WidthPercentage = 100;
            tablaInventario.SetWidths(anchoColumnas);

            PdfPCell colNoConcepto = new PdfPCell(new Phrase("No:", fuenteNegrita));
            colNoConcepto.BorderWidth = 1;
            colNoConcepto.BackgroundColor = new BaseColor(Color.SkyBlue);
            colNoConcepto.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colNombre = new PdfPCell(new Phrase("NOMBRE", fuenteTotales));
            colNombre.BorderWidth = 1;
            colNombre.HorizontalAlignment = Element.ALIGN_CENTER;
            colNombre.Padding = 3;
            colNombre.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colClave = new PdfPCell(new Phrase("CLAVE", fuenteTotales));
            colClave.BorderWidth = 1;
            colClave.HorizontalAlignment = Element.ALIGN_CENTER;
            colClave.Padding = 3;
            colClave.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colCodigo = new PdfPCell(new Phrase("CÓDIGO", fuenteTotales));
            colCodigo.BorderWidth = 1;
            colCodigo.HorizontalAlignment = Element.ALIGN_CENTER;
            colCodigo.Padding = 3;
            colCodigo.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPuntoVenta = new PdfPCell(new Phrase("PUNTO DE VENTA", fuenteTotales));
            colPuntoVenta.BorderWidth = 1;
            colPuntoVenta.HorizontalAlignment = Element.ALIGN_CENTER;
            colPuntoVenta.Padding = 3;
            colPuntoVenta.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colStockFisico = new PdfPCell(new Phrase("STOCK FISICO", fuenteTotales));
            colStockFisico.BorderWidth = 1;
            colStockFisico.HorizontalAlignment = Element.ALIGN_CENTER;
            colStockFisico.Padding = 3;
            colStockFisico.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colFecha = new PdfPCell(new Phrase("FECHA", fuenteTotales));
            colFecha.BorderWidth = 1;
            colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            colFecha.Padding = 3;
            colFecha.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colDiferencia = new PdfPCell(new Phrase("DIFERENCIA", fuenteTotales));
            colDiferencia.BorderWidth = 1;
            colDiferencia.HorizontalAlignment = Element.ALIGN_CENTER;
            colDiferencia.Padding = 3;
            colDiferencia.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPrecio = new PdfPCell(new Phrase("PRECIO", fuenteTotales));
            colPrecio.BorderWidth = 1;
            colPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
            colPrecio.Padding = 3;
            colPrecio.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPerdida = new PdfPCell(new Phrase("CANTIDAD PERDIDA", fuenteTotales));
            colPerdida.BorderWidth = 1;
            colPerdida.HorizontalAlignment = Element.ALIGN_CENTER;
            colPerdida.Padding = 3;
            colPerdida.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colRecuperada = new PdfPCell(new Phrase("CANTIDAD RECUPERADA", fuenteTotales));
            colRecuperada.BorderWidth = 1;
            colRecuperada.HorizontalAlignment = Element.ALIGN_CENTER;
            colRecuperada.Padding = 3;
            colRecuperada.BackgroundColor = new BaseColor(Color.SkyBlue);

            tablaInventario.AddCell(colNoConcepto);
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
                //var almacen = Utilidades.RemoverCeroStock(row.Cells["StockAlmacen"].Value.ToString());
                //var fisico = Utilidades.RemoverCeroStock(row.Cells["StockFisico"].Value.ToString());
                var almacen = row.Cells["StockAlmacen"].Value.ToString();
                var fisico = row.Cells["StockFisico"].Value.ToString();
                var fecha = row.Cells["Fecha"].Value.ToString();
                var diferencia = row.Cells["Diferencia"].Value.ToString();
                var precio = float.Parse(row.Cells["PrecioProducto"].Value.ToString());
                var perdida = row.Cells["Perdida"].Value.ToString();
                var recuperada = row.Cells["Recuperada"].Value.ToString();

                numRow++;
                PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                colNoConceptoTmp.BorderWidth = 1;
                colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colNombreTmp = new PdfPCell(new Phrase(nombre, fuenteNormal));
                colNombreTmp.BorderWidth = 1;
                colNombreTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colClaveTmp = new PdfPCell(new Phrase(clave, fuenteNormal));
                colClaveTmp.BorderWidth = 1;
                colClaveTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoTmp = new PdfPCell(new Phrase(codigo, fuenteNormal));
                colCodigoTmp.BorderWidth = 1;
                colCodigoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPuntoVentaTmp = new PdfPCell(new Phrase(almacen, fuenteNormal));
                PuntoDeVenta += (float)Convert.ToDouble(almacen);
                colPuntoVentaTmp.BorderWidth = 1;
                colPuntoVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmp = new PdfPCell(new Phrase(fisico, fuenteNormal));
                StockFisico += (float)Convert.ToDouble(fisico);
                colStockFisicoTmp.BorderWidth = 1;
                colStockFisicoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
                colFechaTmp.BorderWidth = 1;
                colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDiferenciaTmp = new PdfPCell(new Phrase(diferencia, fuenteNormal));
                Diferencia += (float)Convert.ToDouble(diferencia);
                colDiferenciaTmp.BorderWidth = 1;
                colDiferenciaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmp = new PdfPCell(new Phrase(precio.ToString("0.00"), fuenteNormal));
                Precio += (float)Convert.ToDouble(precio);
                colPrecioTmp.BorderWidth = 1;
                colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPerdidaTmp = new PdfPCell(new Phrase(perdida, fuenteNormal));
                if (!perdida.Equals("---"))
                {
                    CantidadPerdida += (float)Convert.ToDouble(perdida);
                }
                colPerdidaTmp.BorderWidth = 1;
                colPerdidaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRecuperadaTmp = new PdfPCell(new Phrase(recuperada, fuenteNormal));
                if (!recuperada.Equals("---"))
                {
                    CantidadRecuperada += (float)Convert.ToDouble(recuperada);
                }
                colRecuperadaTmp.BorderWidth = 1;
                colRecuperadaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmp);
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

            if (PuntoDeVenta > 0 || StockFisico > 0)
            {
                PdfPCell colNoConceptoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNoConceptoTmpExtra.BorderWidth = 0;
                colNoConceptoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colNombreTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNombreTmpExtra.BorderWidth = 0;
                colNombreTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colClaveTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colClaveTmpExtra.BorderWidth = 0;
                colClaveTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colCodigoTmpExtra.BorderWidth = 0;
                colCodigoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPuntoVentaTmpExtra = new PdfPCell(new Phrase(PuntoDeVenta.ToString("N2"), fuenteNormal));
                colPuntoVentaTmpExtra.BorderWidthTop = 0;
                colPuntoVentaTmpExtra.BorderWidthLeft = 0;
                colPuntoVentaTmpExtra.BorderWidthRight = 0;
                colPuntoVentaTmpExtra.BorderWidthBottom = 1;
                colPuntoVentaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPuntoVentaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmpExtra = new PdfPCell(new Phrase(StockFisico.ToString("N2"), fuenteNormal));
                colStockFisicoTmpExtra.BorderWidthTop = 0;
                colStockFisicoTmpExtra.BorderWidthLeft = 0;
                colStockFisicoTmpExtra.BorderWidthRight = 0;
                colStockFisicoTmpExtra.BorderWidthBottom = 1;
                colStockFisicoTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colStockFisicoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colFechaTmpExtra.BorderWidth = 0;
                colFechaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDiferenciaTmpExtra = new PdfPCell(new Phrase(Diferencia.ToString("N2"), fuenteNormal));
                colDiferenciaTmpExtra.BorderWidthTop = 0;
                colDiferenciaTmpExtra.BorderWidthLeft = 0;
                colDiferenciaTmpExtra.BorderWidthRight = 0;
                colDiferenciaTmpExtra.BorderWidthBottom = 1;
                colDiferenciaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colDiferenciaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmpExtra = new PdfPCell(new Phrase(Precio.ToString("C"), fuenteNormal));
                colPrecioTmpExtra.BorderWidthTop = 0;
                colPrecioTmpExtra.BorderWidthLeft = 0;
                colPrecioTmpExtra.BorderWidthRight = 0;
                colPrecioTmpExtra.BorderWidthBottom = 1;
                colPrecioTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPrecioTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPerdidaTmpExtra = new PdfPCell(new Phrase(CantidadPerdida.ToString("N2"), fuenteNormal));
                colPerdidaTmpExtra.BorderWidthTop = 0;
                colPerdidaTmpExtra.BorderWidthLeft = 0;
                colPerdidaTmpExtra.BorderWidthRight = 0;
                colPerdidaTmpExtra.BorderWidthBottom = 1;
                colPerdidaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPerdidaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRecuperadaTmpExtra = new PdfPCell(new Phrase(CantidadRecuperada.ToString("N2"), fuenteNormal));
                colRecuperadaTmpExtra.BorderWidthTop = 0;
                colRecuperadaTmpExtra.BorderWidthLeft = 0;
                colRecuperadaTmpExtra.BorderWidthRight = 0;
                colRecuperadaTmpExtra.BorderWidthBottom = 1;
                colRecuperadaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colRecuperadaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmpExtra);
                tablaInventario.AddCell(colNombreTmpExtra);
                tablaInventario.AddCell(colClaveTmpExtra);
                tablaInventario.AddCell(colCodigoTmpExtra);
                tablaInventario.AddCell(colPuntoVentaTmpExtra);
                tablaInventario.AddCell(colStockFisicoTmpExtra);
                tablaInventario.AddCell(colFechaTmpExtra);
                tablaInventario.AddCell(colDiferenciaTmpExtra);
                tablaInventario.AddCell(colPrecioTmpExtra);
                tablaInventario.AddCell(colPerdidaTmpExtra);
                tablaInventario.AddCell(colRecuperadaTmpExtra);
            }

            reporte.Add(titulo);
            reporte.Add(Usuario);
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

        private void GenerarReporteSinCLaveInterna()
        {
            var mostrarClave = FormPrincipal.clave;

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

            var numRow = 0;

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
            //var rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";

            var servidor = Properties.Settings.Default.Hosting;

            var rutaArchivo = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo += $@"\\{servidor}\Archivos PUDVE\Reportes\Historial\{FormPrincipal.userNickName}\ActualizarInvetario\";
            }
            else
            {
                rutaArchivo = $@"C:\Archivos PUDVE\Reportes\Historial\{FormPrincipal.userNickName}\ActualizarInvetario\";
            }

            var numeroDeRevision = 0;

            var datosInventario = mb.DatosRevisionInventario();

            if (datosInventario.Length > 0)
            {
                numeroDeRevision = Convert.ToInt32(datosInventario[1].ToString()) - 1;
            }
            else
            {
                numeroDeRevision = 0;
            }

            if (!Directory.Exists(rutaArchivo))
            {
                Directory.CreateDirectory(rutaArchivo);
                rutaArchivo += $"reporte_inventario_NoRevision{numeroDeRevision}_NoFolio{FilterNumActiveRecord}.pdf";
            }
            else
            {
                rutaArchivo += $"reporte_inventario_NoRevision{numeroDeRevision}_NoFolio{FilterNumActiveRecord}.pdf";
            }

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            string UsuarioActivo = string.Empty;

            string tipoReporte = string.Empty, encabezadoTipoReporte = string.Empty;

            float PuntoDeVenta = 0, StockFisico = 0, Diferencia = 0, Precio = 0, CantidadPerdida = 0, CantidadRecuperada = 0;

            tipoReporte = Inventario.filtradoParaRealizar;

            if (!tipoReporte.Equals(string.Empty))
            {
                if (tipoReporte.Equals("Normal"))
                {
                    encabezadoTipoReporte = "Normal";
                }
                if (tipoReporte.Equals("Stock"))
                {
                    encabezadoTipoReporte = "Stock";
                }
                if (tipoReporte.Equals("StockMinimo"))
                {
                    encabezadoTipoReporte = "Stock Minimo";
                }
                if (tipoReporte.Equals("StockNecesario"))
                {
                    encabezadoTipoReporte = "Stock Necesario";
                }
                if (tipoReporte.Equals("NumeroRevision"))
                {
                    encabezadoTipoReporte = "Numero Revision";
                }
                if (tipoReporte.Equals("Filtros"))
                {
                    encabezadoTipoReporte = "Filtros";
                }
            }

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

            Paragraph subTitulo = new Paragraph("REPORTE INVENTARIO\nSECCIÓN ELEGIDA " + encabezadoTipoReporte.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;


            float[] anchoColumnas = new float[] { 30f, 270f, 80f, 80f, 80f, 90f, 70f, 70f, 80f, 100f };

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //============================
            //=== TABLA DE INVENTARIO  ===
            //============================

            PdfPTable tablaInventario = new PdfPTable(10);
            tablaInventario.WidthPercentage = 100;
            tablaInventario.SetWidths(anchoColumnas);

            PdfPCell colNoConcepto = new PdfPCell(new Phrase("No:", fuenteNegrita));
            colNoConcepto.BorderWidth = 1;
            colNoConcepto.BackgroundColor = new BaseColor(Color.SkyBlue);
            colNoConcepto.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colNombre = new PdfPCell(new Phrase("NOMBRE", fuenteTotales));
            colNombre.BorderWidth = 0;
            colNombre.HorizontalAlignment = Element.ALIGN_CENTER;
            colNombre.Padding = 3;
            colNombre.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colCodigo = new PdfPCell(new Phrase("CÓDIGO", fuenteTotales));
            colCodigo.BorderWidth = 0;
            colCodigo.HorizontalAlignment = Element.ALIGN_CENTER;
            colCodigo.Padding = 3;
            colCodigo.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPuntoVenta = new PdfPCell(new Phrase("PUNTO DE VENTA", fuenteTotales));
            colPuntoVenta.BorderWidth = 0;
            colPuntoVenta.HorizontalAlignment = Element.ALIGN_CENTER;
            colPuntoVenta.Padding = 3;
            colPuntoVenta.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colStockFisico = new PdfPCell(new Phrase("STOCK FISICO", fuenteTotales));
            colStockFisico.BorderWidth = 0;
            colStockFisico.HorizontalAlignment = Element.ALIGN_CENTER;
            colStockFisico.Padding = 3;
            colStockFisico.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colFecha = new PdfPCell(new Phrase("FECHA", fuenteTotales));
            colFecha.BorderWidth = 0;
            colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            colFecha.Padding = 3;
            colFecha.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colDiferencia = new PdfPCell(new Phrase("DIFERENCIA", fuenteTotales));
            colDiferencia.BorderWidth = 0;
            colDiferencia.HorizontalAlignment = Element.ALIGN_CENTER;
            colDiferencia.Padding = 3;
            colDiferencia.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPrecio = new PdfPCell(new Phrase("PRECIO", fuenteTotales));
            colPrecio.BorderWidth = 0;
            colPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
            colPrecio.Padding = 3;
            colPrecio.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPerdida = new PdfPCell(new Phrase("CANTIDAD PERDIDA", fuenteTotales));
            colPerdida.BorderWidth = 0;
            colPerdida.HorizontalAlignment = Element.ALIGN_CENTER;
            colPerdida.Padding = 3;
            colPerdida.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colRecuperada = new PdfPCell(new Phrase("CANTIDAD RECUPERADA", fuenteTotales));
            colRecuperada.BorderWidth = 0;
            colRecuperada.HorizontalAlignment = Element.ALIGN_CENTER;
            colRecuperada.Padding = 3;
            colRecuperada.BackgroundColor = new BaseColor(Color.SkyBlue);

            tablaInventario.AddCell(colNoConcepto);
            tablaInventario.AddCell(colNombre);
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
                var codigo = row.Cells["CodigoBarras"].Value.ToString();
                var almacen = row.Cells["StockAlmacen"].Value.ToString();
                var fisico = row.Cells["StockFisico"].Value.ToString();
                var fecha = row.Cells["Fecha"].Value.ToString();
                var diferencia = row.Cells["Diferencia"].Value.ToString();
                var precio = float.Parse(row.Cells["PrecioProducto"].Value.ToString());
                var perdida = row.Cells["Perdida"].Value.ToString();
                var recuperada = row.Cells["Recuperada"].Value.ToString();

                numRow++;
                PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                colNoConceptoTmp.BorderWidth = 1;
                colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colNombreTmp = new PdfPCell(new Phrase(nombre, fuenteNormal));
                colNombreTmp.BorderWidth = 1;
                colNombreTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoTmp = new PdfPCell(new Phrase(codigo, fuenteNormal));
                colCodigoTmp.BorderWidth = 1;
                colCodigoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPuntoVentaTmp = new PdfPCell(new Phrase(almacen, fuenteNormal));
                PuntoDeVenta += (float)Convert.ToDouble(almacen);
                colPuntoVentaTmp.BorderWidth = 1;
                colPuntoVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmp = new PdfPCell(new Phrase(fisico, fuenteNormal));
                StockFisico += (float)Convert.ToDouble(fisico);
                colStockFisicoTmp.BorderWidth = 1;
                colStockFisicoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
                colFechaTmp.BorderWidth = 1;
                colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDiferenciaTmp = new PdfPCell(new Phrase(diferencia, fuenteNormal));
                Diferencia += (float)Convert.ToDouble(diferencia);
                colDiferenciaTmp.BorderWidth = 1;
                colDiferenciaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmp = new PdfPCell(new Phrase(precio.ToString("0.00"), fuenteNormal));
                Precio += (float)Convert.ToDouble(precio);
                colPrecioTmp.BorderWidth = 1;
                colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPerdidaTmp = new PdfPCell(new Phrase(perdida, fuenteNormal));
                if (!perdida.Equals("---"))
                {
                    CantidadPerdida += (float)Convert.ToDouble(perdida);
                }
                colPerdidaTmp.BorderWidth = 1;
                colPerdidaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRecuperadaTmp = new PdfPCell(new Phrase(recuperada, fuenteNormal));
                if (!recuperada.Equals("---"))
                {
                    CantidadRecuperada += (float)Convert.ToDouble(recuperada);
                }
                colRecuperadaTmp.BorderWidth = 1;
                colRecuperadaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmp);          // 01
                tablaInventario.AddCell(colNombreTmp);              // 02
                tablaInventario.AddCell(colCodigoTmp);              // 03
                tablaInventario.AddCell(colPuntoVentaTmp);          // 04
                tablaInventario.AddCell(colStockFisicoTmp);         // 05
                tablaInventario.AddCell(colFechaTmp);               // 06
                tablaInventario.AddCell(colDiferenciaTmp);          // 07
                tablaInventario.AddCell(colPrecioTmp);              // 08
                tablaInventario.AddCell(colPerdidaTmp);             // 09
                tablaInventario.AddCell(colRecuperadaTmp);          // 10
            }

            if (PuntoDeVenta > 0 || StockFisico > 0)
            {
                PdfPCell colNoConceptoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNoConceptoTmpExtra.BorderWidth = 0;
                colNoConceptoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colNombreTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNombreTmpExtra.BorderWidth = 0;
                colNombreTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colCodigoTmpExtra.BorderWidth = 0;
                colCodigoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPuntoVentaTmpExtra = new PdfPCell(new Phrase(PuntoDeVenta.ToString("N2"), fuenteNormal));
                colPuntoVentaTmpExtra.BorderWidthTop = 0;
                colPuntoVentaTmpExtra.BorderWidthLeft = 0;
                colPuntoVentaTmpExtra.BorderWidthRight = 0;
                colPuntoVentaTmpExtra.BorderWidthBottom = 1;
                colPuntoVentaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPuntoVentaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmpExtra = new PdfPCell(new Phrase(StockFisico.ToString("N2"), fuenteNormal));
                colStockFisicoTmpExtra.BorderWidthTop = 0;
                colStockFisicoTmpExtra.BorderWidthLeft = 0;
                colStockFisicoTmpExtra.BorderWidthRight = 0;
                colStockFisicoTmpExtra.BorderWidthBottom = 1;
                colStockFisicoTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colStockFisicoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colFechaTmpExtra.BorderWidth = 0;
                colFechaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDiferenciaTmpExtra = new PdfPCell(new Phrase(Diferencia.ToString("N2"), fuenteNormal));
                colDiferenciaTmpExtra.BorderWidthTop = 0;
                colDiferenciaTmpExtra.BorderWidthLeft = 0;
                colDiferenciaTmpExtra.BorderWidthRight = 0;
                colDiferenciaTmpExtra.BorderWidthBottom = 1;
                colDiferenciaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colDiferenciaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmpExtra = new PdfPCell(new Phrase(Precio.ToString("C"), fuenteNormal));
                colPrecioTmpExtra.BorderWidthTop = 0;
                colPrecioTmpExtra.BorderWidthLeft = 0;
                colPrecioTmpExtra.BorderWidthRight = 0;
                colPrecioTmpExtra.BorderWidthBottom = 1;
                colPrecioTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPrecioTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPerdidaTmpExtra = new PdfPCell(new Phrase(CantidadPerdida.ToString("N2"), fuenteNormal));
                colPerdidaTmpExtra.BorderWidthTop = 0;
                colPerdidaTmpExtra.BorderWidthLeft = 0;
                colPerdidaTmpExtra.BorderWidthRight = 0;
                colPerdidaTmpExtra.BorderWidthBottom = 1;
                colPerdidaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPerdidaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRecuperadaTmpExtra = new PdfPCell(new Phrase(CantidadRecuperada.ToString("N2"), fuenteNormal));
                colRecuperadaTmpExtra.BorderWidthTop = 0;
                colRecuperadaTmpExtra.BorderWidthLeft = 0;
                colRecuperadaTmpExtra.BorderWidthRight = 0;
                colRecuperadaTmpExtra.BorderWidthBottom = 1;
                colRecuperadaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colRecuperadaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmpExtra);
                tablaInventario.AddCell(colNombreTmpExtra);
                tablaInventario.AddCell(colCodigoTmpExtra);
                tablaInventario.AddCell(colPuntoVentaTmpExtra);
                tablaInventario.AddCell(colStockFisicoTmpExtra);
                tablaInventario.AddCell(colFechaTmpExtra);
                tablaInventario.AddCell(colDiferenciaTmpExtra);
                tablaInventario.AddCell(colPrecioTmpExtra);
                tablaInventario.AddCell(colPerdidaTmpExtra);
                tablaInventario.AddCell(colRecuperadaTmpExtra);
            }

            reporte.Add(titulo);
            reporte.Add(Usuario);
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

            //var mostrarClave = FormPrincipal.clave;

            //// Datos del usuario
            //var datos = FormPrincipal.datosUsuario;

            //// Fuentes y Colores
            //var colorFuenteNegrita = new BaseColor(Color.Black);
            //var colorFuenteBlanca = new BaseColor(Color.White);

            //var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            //var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            //var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            //var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            //var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, colorFuenteNegrita);

            //// Ruta donde se creara el archivo PDF
            ////var servidor = Properties.Settings.Default.Hosting;
            ////var rutaArchivo = string.Empty;
            ///*if (!string.IsNullOrWhiteSpace(servidor))
            //{
            //    rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            //}
            //else
            //{
            //    rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            //}*/

            //var fechaHoy = DateTime.Now;
            //var rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";

            //Document reporte = new Document(PageSize.A3.Rotate());
            //PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            //reporte.Open();

            //Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
            //Paragraph subTitulo = new Paragraph("REPORTE INVENTARIO\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            //titulo.Alignment = Element.ALIGN_CENTER;
            //subTitulo.Alignment = Element.ALIGN_CENTER;


            //float[] anchoColumnas = new float[] { 270f, 80f, 80f, 80f, 80f, 90f, 70f, 70f, 80f, 100f };

            //// Linea serapadora
            //Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            ////============================
            ////=== TABLA DE INVENTARIO  ===
            ////============================

            //PdfPTable tablaInventario = new PdfPTable(10);
            //tablaInventario.WidthPercentage = 100;
            //tablaInventario.SetWidths(anchoColumnas);

            //PdfPCell colNombre = new PdfPCell(new Phrase("NOMBRE", fuenteTotales));
            //colNombre.BorderWidth = 0;
            //colNombre.HorizontalAlignment = Element.ALIGN_CENTER;
            //colNombre.Padding = 3;
            //colNombre.BackgroundColor = new BaseColor(Color.Black);

            ////PdfPCell colClave = new PdfPCell(new Phrase("CLAVE", fuenteTotales));
            ////colClave.BorderWidth = 0;
            ////colClave.HorizontalAlignment = Element.ALIGN_CENTER;
            ////colClave.Padding = 3;
            ////colClave.BackgroundColor = new BaseColor(Color.Black);

            //PdfPCell colCodigo = new PdfPCell(new Phrase("CÓDIGO", fuenteTotales));
            //colCodigo.BorderWidth = 0;
            //colCodigo.HorizontalAlignment = Element.ALIGN_CENTER;
            //colCodigo.Padding = 3;
            //colCodigo.BackgroundColor = new BaseColor(Color.Black);

            //PdfPCell colPuntoVenta = new PdfPCell(new Phrase("PUNTO DE VENTA", fuenteTotales));
            //colPuntoVenta.BorderWidth = 0;
            //colPuntoVenta.HorizontalAlignment = Element.ALIGN_CENTER;
            //colPuntoVenta.Padding = 3;
            //colPuntoVenta.BackgroundColor = new BaseColor(Color.Black);

            //PdfPCell colStockFisico = new PdfPCell(new Phrase("STOCK FISICO", fuenteTotales));
            //colStockFisico.BorderWidth = 0;
            //colStockFisico.HorizontalAlignment = Element.ALIGN_CENTER;
            //colStockFisico.Padding = 3;
            //colStockFisico.BackgroundColor = new BaseColor(Color.Black);

            //PdfPCell colFecha = new PdfPCell(new Phrase("FECHA", fuenteTotales));
            //colFecha.BorderWidth = 0;
            //colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            //colFecha.Padding = 3;
            //colFecha.BackgroundColor = new BaseColor(Color.Black);

            //PdfPCell colDiferencia = new PdfPCell(new Phrase("DIFERENCIA", fuenteTotales));
            //colDiferencia.BorderWidth = 0;
            //colDiferencia.HorizontalAlignment = Element.ALIGN_CENTER;
            //colDiferencia.Padding = 3;
            //colDiferencia.BackgroundColor = new BaseColor(Color.Black);

            //PdfPCell colPrecio = new PdfPCell(new Phrase("PRECIO", fuenteTotales));
            //colPrecio.BorderWidth = 0;
            //colPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
            //colPrecio.Padding = 3;
            //colPrecio.BackgroundColor = new BaseColor(Color.Black);

            //PdfPCell colPerdida = new PdfPCell(new Phrase("CANTIDAD PERDIDA", fuenteTotales));
            //colPerdida.BorderWidth = 0;
            //colPerdida.HorizontalAlignment = Element.ALIGN_CENTER;
            //colPerdida.Padding = 3;
            //colPerdida.BackgroundColor = new BaseColor(Color.Black);

            //PdfPCell colRecuperada = new PdfPCell(new Phrase("CANTIDAD RECUPERADA", fuenteTotales));
            //colRecuperada.BorderWidth = 0;
            //colRecuperada.HorizontalAlignment = Element.ALIGN_CENTER;
            //colRecuperada.Padding = 3;
            //colRecuperada.BackgroundColor = new BaseColor(Color.Black);

            //tablaInventario.AddCell(colNombre);
            ////tablaInventario.AddCell(colClave);
            //tablaInventario.AddCell(colCodigo);
            //tablaInventario.AddCell(colPuntoVenta);
            //tablaInventario.AddCell(colStockFisico);
            //tablaInventario.AddCell(colFecha);
            //tablaInventario.AddCell(colDiferencia);
            //tablaInventario.AddCell(colPrecio);
            //tablaInventario.AddCell(colPerdida);
            //tablaInventario.AddCell(colRecuperada);


            //foreach (DataGridViewRow row in DGVRevisionStock.Rows)
            //{
            //    var nombre = row.Cells["Nombre"].Value.ToString();
            //    //var clave = row.Cells["ClaveInterna"].Value.ToString();
            //    var codigo = row.Cells["CodigoBarras"].Value.ToString();
            //    //var almacen = Utilidades.RemoverCeroStock(row.Cells["StockAlmacen"].Value.ToString());
            //    //var fisico = Utilidades.RemoverCeroStock(row.Cells["StockFisico"].Value.ToString());
            //    var almacen = row.Cells["StockAlmacen"].Value.ToString();
            //    var fisico = row.Cells["StockFisico"].Value.ToString();
            //    var fecha = row.Cells["Fecha"].Value.ToString();
            //    var diferencia = row.Cells["Diferencia"].Value.ToString();
            //    var precio = float.Parse(row.Cells["PrecioProducto"].Value.ToString());
            //    var perdida = row.Cells["Perdida"].Value.ToString();
            //    var recuperada = row.Cells["Recuperada"].Value.ToString();

            //    PdfPCell colNombreTmp = new PdfPCell(new Phrase(nombre, fuenteNormal));
            //    colNombreTmp.BorderWidth = 0;
            //    colNombreTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    //PdfPCell colClaveTmp = new PdfPCell(new Phrase(clave, fuenteNormal));
            //    //colClaveTmp.BorderWidth = 0;
            //    //colClaveTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colCodigoTmp = new PdfPCell(new Phrase(codigo, fuenteNormal));
            //    colCodigoTmp.BorderWidth = 0;
            //    colCodigoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colPuntoVentaTmp = new PdfPCell(new Phrase(almacen, fuenteNormal));
            //    colPuntoVentaTmp.BorderWidth = 0;
            //    colPuntoVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colStockFisicoTmp = new PdfPCell(new Phrase(fisico, fuenteNormal));
            //    colStockFisicoTmp.BorderWidth = 0;
            //    colStockFisicoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
            //    colFechaTmp.BorderWidth = 0;
            //    colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colDiferenciaTmp = new PdfPCell(new Phrase(diferencia, fuenteNormal));
            //    colDiferenciaTmp.BorderWidth = 0;
            //    colDiferenciaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colPrecioTmp = new PdfPCell(new Phrase(precio.ToString("0.00"), fuenteNormal));
            //    colPrecioTmp.BorderWidth = 0;
            //    colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colPerdidaTmp = new PdfPCell(new Phrase(perdida, fuenteNormal));
            //    colPerdidaTmp.BorderWidth = 0;
            //    colPerdidaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colRecuperadaTmp = new PdfPCell(new Phrase(recuperada, fuenteNormal));
            //    colRecuperadaTmp.BorderWidth = 0;
            //    colRecuperadaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    tablaInventario.AddCell(colNombreTmp);
            //    //tablaInventario.AddCell(colClaveTmp);
            //    tablaInventario.AddCell(colCodigoTmp);
            //    tablaInventario.AddCell(colPuntoVentaTmp);
            //    tablaInventario.AddCell(colStockFisicoTmp);
            //    tablaInventario.AddCell(colFechaTmp);
            //    tablaInventario.AddCell(colDiferenciaTmp);
            //    tablaInventario.AddCell(colPrecioTmp);
            //    tablaInventario.AddCell(colPerdidaTmp);
            //    tablaInventario.AddCell(colRecuperadaTmp);
            //}

            //reporte.Add(titulo);
            //reporte.Add(subTitulo);
            //reporte.Add(tablaInventario);

            ////================================
            ////=== FIN TABLA DE INVENTARIO  ===
            ////================================

            //reporte.AddTitle("Reporte Inventario");
            //reporte.AddAuthor("PUDVE");
            //reporte.Close();
            //writer.Close();

            //VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            //vr.Show();
        }
    }
}
