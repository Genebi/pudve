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
    public partial class BuscadorReporteInventario : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        System.Drawing.Image icono = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");

        private Paginar p;

        string filtroConSinFiltroAvanzado = string.Empty;
        string DataMemberDGV = "RevisarInventarioReportes";
        string busqueda = string.Empty;

        // Variables de tipo Int
        int maximo_x_pagina = 10;
        int clickBoton = 0;

        bool conBusqueda = false;

        string tipoDatoReporte = string.Empty;

        string mensajeParaMostrar = string.Empty;
        
        string TipoUser = string.Empty;

        //Revisar Inventario               = RInventario
        //Actualizar Inventario Aumentar   = AIAumentar
        //Actualizar Inventario Disminuir  = AIDisminuir

        public BuscadorReporteInventario(string procedencia)
        {
            InitializeComponent();

            this.tipoDatoReporte = procedencia;
        }

        private void BuscadorReporteInventario_Load(object sender, EventArgs e)
        {
            //Poner el texto en la etiqueta segun sea el tipo de busqueda
            if (tipoDatoReporte.Equals("RInventario"))
            {
                label3.Text = "Reportes Revisar Inventario";
                TipoUser = "NameUsr";
            }
            else if (tipoDatoReporte.Equals("AIAumentar"))
            {
                label3.Text = "Reportes Actualizar Inventario (Aumentar)";
                TipoUser = "NameUsr";
            }
            else if (tipoDatoReporte.Equals("AIDisminuir"))
            {
                label3.Text = "Reportes Actualizar Inventario (Disminuir)";
                TipoUser = "NameUse";
            }

            cargarDatosDGV();
            DateTime date = DateTime.Now;
            DateTime PrimerDia = new DateTime(date.Year, date.Month -1, 1);
            primerDatePicker.Value = PrimerDia;
            segundoDatePicker.Value = DateTime.Now;
            
            conBusqueda = true;
            DGVInventario.Rows.Clear();

            var datoBuscar = txtBuscador.Text.ToString().Replace("\r\n", string.Empty);
            var primerFecha = primerDatePicker.Value.ToString("yyyy/MM/dd");
            var segundaFecha = segundoDatePicker.Value.AddDays(1).ToString("yyyy/MM/dd");


            var rev = string.Empty; var name = string.Empty; var fecha = string.Empty;
            

            filtroConSinFiltroAvanzado = cs.BuscadorDeInventario(datoBuscar, primerFecha, segundaFecha, tipoDatoReporte);

            
            txtBuscador.Text = string.Empty;
            txtBuscador.Focus();
            
            CargarDatos();

        }

        private void cargarDatosDGV()
        {
            DGVInventario.Rows.Clear();

            var numRevision = string.Empty;
            var nameUser = string.Empty;
            var fecha = string.Empty;
            var query = string.Empty;

            if (tipoDatoReporte.Equals("RInventario"))//Revisar inventario
            {
                query = cs.consultaReporteGeneralRevisarInventario();
            }
            else if (tipoDatoReporte.Equals("AIAumentar"))//Actualizar Inventario (Aumentar)
            {
                query = cs.consultaReporteGeneralAumentarInventario();
            }
            else if (tipoDatoReporte.Equals("AIDisminuir"))//Actualizar Inventario (Disminuir)
            {
                query = cs.consultaReporteGeneralDisminuirInventario();
            }

            filtroConSinFiltroAvanzado = query;

            CargarDatos();
        }


        private void DGVInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                if (e.RowIndex >= 0)
                {
                    var mostrarClave = FormPrincipal.clave;
                    int numFolio = Convert.ToInt32(DGVInventario.Rows[e.RowIndex].Cells[0].Value.ToString());
                    int numRev = Convert.ToInt32(DGVInventario.Rows[e.RowIndex].Cells[1].Value.ToString());

                    var servidor = Properties.Settings.Default.Hosting;

                    var rutaArchivo = string.Empty;
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
                        rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Historial\{usuario}\";
                    }
                    else
                    {
                        rutaArchivo = $@"C:\Archivos PUDVE\Reportes\Historial\{usuario}\";
                    }

                    if (tipoDatoReporte.Equals("RInventario"))
                    {
                        rutaArchivo += @"ActualizarInvetario\";
                    }
                    else if (tipoDatoReporte.Equals("AIAumentar"))
                    {
                        rutaArchivo += @"AumentarInventario\";
                    }
                    else if (tipoDatoReporte.Equals("AIDisminuir"))
                    {
                        rutaArchivo += @"DisminuirInventario\";
                    }

                    if (!Directory.Exists(rutaArchivo))
                    {
                        Directory.CreateDirectory(rutaArchivo);
                    }

                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        if (tipoDatoReporte.Equals("RInventario"))
                        {
                            rutaArchivo += $"reporte_inventario_NoRevision{numRev}_NoFolio{numFolio}.pdf";
                        }
                        else
                        {
                            rutaArchivo += $"reporte_actualizar_inventarioNoRevision{numRev}_NoFolio{numFolio}.pdf";
                        }
                    }
                    else
                    {
                        if (tipoDatoReporte.Equals("RInventario"))
                        {
                            rutaArchivo += $"reporte_inventario_NoRevision{numRev}_NoFolio{numFolio}.pdf";
                        }
                        else
                        {
                            rutaArchivo += $"reporte_actualizar_inventario_NoRevision{numRev}_NoFolio{numFolio}.pdf";
                        }
                    }

                    if (tipoDatoReporte.Equals("RInventario"))
                    {
                        if (mostrarClave == 0)
                        {
                            if (!File.Exists(rutaArchivo))
                            {
                                GenerarReporteSinCLaveInterna(numRev, numFolio);
                            }
                        }
                        else if (mostrarClave == 1)
                        {
                            if (!File.Exists(rutaArchivo))
                            {
                                GenerarReporte(numRev);
                            }
                        }
                    }
                    else
                    {
                        //GenerarReporteActualizarInventario(numRev);
                        if (!File.Exists(rutaArchivo))
                        {
                            //reconstruirReporteSinLaClaveInterna(numRev, FormPrincipal.userID, numFolio, rutaArchivo);
                            GenerarReporteActualizarInventario(numRev, numFolio, rutaArchivo);
                        }
                    }

                    VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
                    vr.ShowDialog();
                }
            }
        }

        #region Generar los reportes sin la clave interna.
        private void GenerarReporteSinCLaveInterna(int numRevision, int numFolio)
        {
            var servidor = Properties.Settings.Default.Hosting;

            var rutaArchivo = string.Empty;
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
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Historial\{usuario}\ActualizarInvetario\";
            }
            else
            {
                rutaArchivo = $@"C:\Archivos PUDVE\Reportes\Historial\{usuario}\ActualizarInvetario\";
            }

            if (!Directory.Exists(rutaArchivo))
            {
                Directory.CreateDirectory(rutaArchivo);
            }

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo += $"reporte_inventario_NoRevision{numRevision}_NoFolio{numFolio}.pdf";
            }
            else
            {
                rutaArchivo += $"reporte_inventario_NoRevision{numRevision}_NoFolio{numFolio}.pdf";
            }

            if (!File.Exists(rutaArchivo))
            {
                reconstruirReporteSinLaClaveInterna(numRevision, FormPrincipal.userID, numFolio, rutaArchivo);
            }

            //VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            //vr.ShowDialog();

            //var mostrarClave = FormPrincipal.clave;

            ////var numFolio = obtenerFolio(num);

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

            //var numRow = 0;

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

            //Paragraph Usuario = new Paragraph("");

            //Paragraph numeroFolio = new Paragraph("");

            //string UsuarioActivo = string.Empty;

            //string tipoReporte = string.Empty,
            //        encabezadoTipoReporte = string.Empty;

            //float PuntoDeVenta = 0,
            //        StockFisico = 0,
            //        Diferencia = 0,
            //        Precio = 0,
            //        CantidadPerdida = 0,
            //        CantidadRecuperada = 0;

            //tipoReporte = Inventario.filtradoParaRealizar;

            //if (!tipoReporte.Equals(string.Empty))
            //{
            //    if (tipoReporte.Equals("Normal"))
            //    {
            //        encabezadoTipoReporte = "Normal";
            //    }
            //    if (tipoReporte.Equals("Stock"))
            //    {
            //        encabezadoTipoReporte = "Stock";
            //    }
            //    if (tipoReporte.Equals("StockMinimo"))
            //    {
            //        encabezadoTipoReporte = "Stock Minimo";
            //    }
            //    if (tipoReporte.Equals("StockNecesario"))
            //    {
            //        encabezadoTipoReporte = "Stock Necesario";
            //    }
            //    if (tipoReporte.Equals("NumeroRevision"))
            //    {
            //        encabezadoTipoReporte = "Numero Revision";
            //    }
            //    if (tipoReporte.Equals("Filtros"))
            //    {
            //        encabezadoTipoReporte = "Filtros";
            //    }
            //}

            //using (DataTable dtDataUsr = cn.CargarDatos(cs.UsuarioRazonSocialNombreCompleto(Convert.ToString(FormPrincipal.userID))))
            //{
            //    if (!dtDataUsr.Rows.Count.Equals(0))
            //    {
            //        foreach (DataRow drDataUsr in dtDataUsr.Rows)
            //        {
            //            UsuarioActivo = drDataUsr["Usuario"].ToString();
            //        }
            //    }
            //}

            //Usuario = new Paragraph("USUARIO: " + UsuarioActivo, fuenteNegrita);

            //numeroFolio = new Paragraph("No. FOLIO: " + num, fuenteNormal);

            //Paragraph subTitulo = new Paragraph("REPORTE INVENTARIO\nSECCIÓN ELEGIDA " + encabezadoTipoReporte.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            //titulo.Alignment = Element.ALIGN_CENTER;
            //Usuario.Alignment = Element.ALIGN_CENTER;
            //subTitulo.Alignment = Element.ALIGN_CENTER;
            //numeroFolio.Alignment = Element.ALIGN_CENTER;


            //float[] anchoColumnas = new float[] { 30f, 270f, 80f, 80f, 80f, 90f, 70f, 70f, 80f, 100f };

            //// Linea serapadora
            //Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            ////============================
            ////=== TABLA DE INVENTARIO  ===
            ////============================

            //PdfPTable tablaInventario = new PdfPTable(10);
            //tablaInventario.WidthPercentage = 100;
            //tablaInventario.SetWidths(anchoColumnas);

            //PdfPCell colNoConcepto = new PdfPCell(new Phrase("No:", fuenteNegrita));
            //colNoConcepto.BorderWidth = 1;
            //colNoConcepto.BackgroundColor = new BaseColor(Color.SkyBlue);
            //colNoConcepto.HorizontalAlignment = Element.ALIGN_CENTER;

            //PdfPCell colNombre = new PdfPCell(new Phrase("NOMBRE", fuenteTotales));
            //colNombre.BorderWidth = 0;
            //colNombre.HorizontalAlignment = Element.ALIGN_CENTER;
            //colNombre.Padding = 3;
            //colNombre.BackgroundColor = new BaseColor(Color.SkyBlue);

            //PdfPCell colCodigo = new PdfPCell(new Phrase("CÓDIGO", fuenteTotales));
            //colCodigo.BorderWidth = 0;
            //colCodigo.HorizontalAlignment = Element.ALIGN_CENTER;
            //colCodigo.Padding = 3;
            //colCodigo.BackgroundColor = new BaseColor(Color.SkyBlue);

            //PdfPCell colPuntoVenta = new PdfPCell(new Phrase("PUNTO DE VENTA", fuenteTotales));
            //colPuntoVenta.BorderWidth = 0;
            //colPuntoVenta.HorizontalAlignment = Element.ALIGN_CENTER;
            //colPuntoVenta.Padding = 3;
            //colPuntoVenta.BackgroundColor = new BaseColor(Color.SkyBlue);

            //PdfPCell colStockFisico = new PdfPCell(new Phrase("STOCK FISICO", fuenteTotales));
            //colStockFisico.BorderWidth = 0;
            //colStockFisico.HorizontalAlignment = Element.ALIGN_CENTER;
            //colStockFisico.Padding = 3;
            //colStockFisico.BackgroundColor = new BaseColor(Color.SkyBlue);

            //PdfPCell colFecha = new PdfPCell(new Phrase("FECHA", fuenteTotales));
            //colFecha.BorderWidth = 0;
            //colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            //colFecha.Padding = 3;
            //colFecha.BackgroundColor = new BaseColor(Color.SkyBlue);

            //PdfPCell colDiferencia = new PdfPCell(new Phrase("DIFERENCIA", fuenteTotales));
            //colDiferencia.BorderWidth = 0;
            //colDiferencia.HorizontalAlignment = Element.ALIGN_CENTER;
            //colDiferencia.Padding = 3;
            //colDiferencia.BackgroundColor = new BaseColor(Color.SkyBlue);

            //PdfPCell colPrecio = new PdfPCell(new Phrase("PRECIO", fuenteTotales));
            //colPrecio.BorderWidth = 0;
            //colPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
            //colPrecio.Padding = 3;
            //colPrecio.BackgroundColor = new BaseColor(Color.SkyBlue);

            //PdfPCell colPerdida = new PdfPCell(new Phrase("CANTIDAD PERDIDA", fuenteTotales));
            //colPerdida.BorderWidth = 0;
            //colPerdida.HorizontalAlignment = Element.ALIGN_CENTER;
            //colPerdida.Padding = 3;
            //colPerdida.BackgroundColor = new BaseColor(Color.SkyBlue);

            //PdfPCell colRecuperada = new PdfPCell(new Phrase("CANTIDAD RECUPERADA", fuenteTotales));
            //colRecuperada.BorderWidth = 0;
            //colRecuperada.HorizontalAlignment = Element.ALIGN_CENTER;
            //colRecuperada.Padding = 3;
            //colRecuperada.BackgroundColor = new BaseColor(Color.SkyBlue);

            //tablaInventario.AddCell(colNoConcepto);
            //tablaInventario.AddCell(colNombre);
            //tablaInventario.AddCell(colCodigo);
            //tablaInventario.AddCell(colPuntoVenta);
            //tablaInventario.AddCell(colStockFisico);
            //tablaInventario.AddCell(colFecha);
            //tablaInventario.AddCell(colDiferencia);
            //tablaInventario.AddCell(colPrecio);
            //tablaInventario.AddCell(colPerdida);
            //tablaInventario.AddCell(colRecuperada);

            //var consulta = cn.CargarDatos($"SELECT * FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}' AND NumFolio = '{num}'");

            //foreach (DataRow row in consulta.Rows)
            //{
            //    var nombre = row["Nombre"].ToString();
            //    var clave = row["ClaveInterna"].ToString();
            //    var codigo = row["CodigoBarras"].ToString();
            //    var almacen = row["StockAlmacen"].ToString();
            //    var fisico = row["StockFisico"].ToString();
            //    var fecha = row["Fecha"].ToString();
            //    var diferencia = row["Diferencia"].ToString();
            //    var precio = float.Parse(row["PrecioProducto"].ToString());
            //    var perdida = string.Empty;
            //    var recuperada = string.Empty;

            //    if (float.Parse(diferencia) < 0)
            //    {
            //        perdida = (float.Parse(diferencia) * precio).ToString();
            //        recuperada = "0";
            //    }
            //    else if (float.Parse(diferencia) > 0)
            //    {
            //        recuperada = (float.Parse(diferencia) * precio).ToString();
            //        perdida = "0";
            //    }
            //    else
            //    {
            //        recuperada = "0";
            //        perdida = "0";
            //    }

            //    numRow++;
            //    PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
            //    colNoConceptoTmp.BorderWidth = 1;
            //    colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colNombreTmp = new PdfPCell(new Phrase(nombre, fuenteNormal));
            //    colNombreTmp.BorderWidth = 1;
            //    colNombreTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colCodigoTmp = new PdfPCell(new Phrase(codigo, fuenteNormal));
            //    colCodigoTmp.BorderWidth = 1;
            //    colCodigoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colPuntoVentaTmp = new PdfPCell(new Phrase(almacen, fuenteNormal));
            //    PuntoDeVenta += (float)Convert.ToDouble(almacen);
            //    colPuntoVentaTmp.BorderWidth = 1;
            //    colPuntoVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colStockFisicoTmp = new PdfPCell(new Phrase(fisico, fuenteNormal));
            //    StockFisico += (float)Convert.ToDouble(fisico);
            //    colStockFisicoTmp.BorderWidth = 1;
            //    colStockFisicoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
            //    colFechaTmp.BorderWidth = 1;
            //    colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colDiferenciaTmp = new PdfPCell(new Phrase(diferencia, fuenteNormal));
            //    Diferencia += (float)Convert.ToDouble(diferencia);
            //    colDiferenciaTmp.BorderWidth = 1;
            //    colDiferenciaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colPrecioTmp = new PdfPCell(new Phrase(precio.ToString("0.00"), fuenteNormal));
            //    Precio += (float)Convert.ToDouble(precio);
            //    colPrecioTmp.BorderWidth = 1;
            //    colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colPerdidaTmp = new PdfPCell(new Phrase(perdida, fuenteNormal));
            //    if (!perdida.Equals("---"))
            //    {
            //        CantidadPerdida += (float)Convert.ToDouble(perdida);
            //    }
            //    colPerdidaTmp.BorderWidth = 1;
            //    colPerdidaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colRecuperadaTmp = new PdfPCell(new Phrase(recuperada, fuenteNormal));
            //    if (!recuperada.Equals("---"))
            //    {
            //        CantidadRecuperada += (float)Convert.ToDouble(recuperada);
            //    }
            //    colRecuperadaTmp.BorderWidth = 1;
            //    colRecuperadaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

            //    tablaInventario.AddCell(colNoConceptoTmp);          // 01
            //    tablaInventario.AddCell(colNombreTmp);              // 02
            //    tablaInventario.AddCell(colCodigoTmp);              // 03
            //    tablaInventario.AddCell(colPuntoVentaTmp);          // 04
            //    tablaInventario.AddCell(colStockFisicoTmp);         // 05
            //    tablaInventario.AddCell(colFechaTmp);               // 06
            //    tablaInventario.AddCell(colDiferenciaTmp);          // 07
            //    tablaInventario.AddCell(colPrecioTmp);              // 08
            //    tablaInventario.AddCell(colPerdidaTmp);             // 09
            //    tablaInventario.AddCell(colRecuperadaTmp);          // 10
            //}

            //if (PuntoDeVenta > 0 || StockFisico > 0)
            //{
            //    PdfPCell colNoConceptoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            //    colNoConceptoTmpExtra.BorderWidth = 0;
            //    colNoConceptoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colNombreTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            //    colNombreTmpExtra.BorderWidth = 0;
            //    colNombreTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colCodigoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            //    colCodigoTmpExtra.BorderWidth = 0;
            //    colCodigoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colPuntoVentaTmpExtra = new PdfPCell(new Phrase(PuntoDeVenta.ToString("N2"), fuenteNormal));
            //    colPuntoVentaTmpExtra.BorderWidthTop = 0;
            //    colPuntoVentaTmpExtra.BorderWidthLeft = 0;
            //    colPuntoVentaTmpExtra.BorderWidthRight = 0;
            //    colPuntoVentaTmpExtra.BorderWidthBottom = 1;
            //    colPuntoVentaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
            //    colPuntoVentaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colStockFisicoTmpExtra = new PdfPCell(new Phrase(StockFisico.ToString("N2"), fuenteNormal));
            //    colStockFisicoTmpExtra.BorderWidthTop = 0;
            //    colStockFisicoTmpExtra.BorderWidthLeft = 0;
            //    colStockFisicoTmpExtra.BorderWidthRight = 0;
            //    colStockFisicoTmpExtra.BorderWidthBottom = 1;
            //    colStockFisicoTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
            //    colStockFisicoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colFechaTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            //    colFechaTmpExtra.BorderWidth = 0;
            //    colFechaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colDiferenciaTmpExtra = new PdfPCell(new Phrase(Diferencia.ToString("N2"), fuenteNormal));
            //    colDiferenciaTmpExtra.BorderWidthTop = 0;
            //    colDiferenciaTmpExtra.BorderWidthLeft = 0;
            //    colDiferenciaTmpExtra.BorderWidthRight = 0;
            //    colDiferenciaTmpExtra.BorderWidthBottom = 1;
            //    colDiferenciaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
            //    colDiferenciaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colPrecioTmpExtra = new PdfPCell(new Phrase(Precio.ToString("C"), fuenteNormal));
            //    colPrecioTmpExtra.BorderWidthTop = 0;
            //    colPrecioTmpExtra.BorderWidthLeft = 0;
            //    colPrecioTmpExtra.BorderWidthRight = 0;
            //    colPrecioTmpExtra.BorderWidthBottom = 1;
            //    colPrecioTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
            //    colPrecioTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colPerdidaTmpExtra = new PdfPCell(new Phrase(CantidadPerdida.ToString("N2"), fuenteNormal));
            //    colPerdidaTmpExtra.BorderWidthTop = 0;
            //    colPerdidaTmpExtra.BorderWidthLeft = 0;
            //    colPerdidaTmpExtra.BorderWidthRight = 0;
            //    colPerdidaTmpExtra.BorderWidthBottom = 1;
            //    colPerdidaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
            //    colPerdidaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colRecuperadaTmpExtra = new PdfPCell(new Phrase(CantidadRecuperada.ToString("N2"), fuenteNormal));
            //    colRecuperadaTmpExtra.BorderWidthTop = 0;
            //    colRecuperadaTmpExtra.BorderWidthLeft = 0;
            //    colRecuperadaTmpExtra.BorderWidthRight = 0;
            //    colRecuperadaTmpExtra.BorderWidthBottom = 1;
            //    colRecuperadaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
            //    colRecuperadaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

            //    tablaInventario.AddCell(colNoConceptoTmpExtra);
            //    tablaInventario.AddCell(colNombreTmpExtra);
            //    tablaInventario.AddCell(colCodigoTmpExtra);
            //    tablaInventario.AddCell(colPuntoVentaTmpExtra);
            //    tablaInventario.AddCell(colStockFisicoTmpExtra);
            //    tablaInventario.AddCell(colFechaTmpExtra);
            //    tablaInventario.AddCell(colDiferenciaTmpExtra);
            //    tablaInventario.AddCell(colPrecioTmpExtra);
            //    tablaInventario.AddCell(colPerdidaTmpExtra);
            //    tablaInventario.AddCell(colRecuperadaTmpExtra);
            //}

            //reporte.Add(titulo);
            //reporte.Add(Usuario);
            //reporte.Add(numeroFolio);
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
            //vr.ShowDialog();

        }

        private void reconstruirReporteSinLaClaveInterna(int numRevision, int userID, int numFolio, string rutaDelArchivo)
        {
            using (DataTable dtReporteSinClaveInterna = cn.CargarDatos(cs.regenerarReporteGeneralRevisarInventario(numRevision, userID, numFolio)))
            {
                if (!dtReporteSinClaveInterna.Rows.Count.Equals(0))
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

                    var numRow = 0;

                    DateTime fechaHoy = new DateTime();

                    Document reporte = new Document(PageSize.A3.Rotate());
                    PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaDelArchivo, FileMode.Create));

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

                    using (DataTable dtFechaReporte = cn.CargarDatos(cs.sacarFechaReporte(numRevision, numFolio)))
                    {
                        if (!dtFechaReporte.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtFechaReporte.Rows)
                            {
                                fechaHoy = Convert.ToDateTime(item["Fecha"].ToString());
                                encabezadoTipoReporte = item["TipoRevision"].ToString();
                            }
                        }
                    }

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
                    colNoConcepto.BorderWidth = 0;
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

                    foreach (DataRow item in dtReporteSinClaveInterna.Rows)
                    {
                        var nombre = item["Nombre"].ToString();
                        var codigo = item["CodigoBarras"].ToString();
                        var almacen = float.Parse(item["StockAlmacen"].ToString());
                        var fisico = float.Parse(item["StockFisico"].ToString());
                        var fecha = item["Fecha"].ToString();
                        var diferencia = float.Parse(item["Diferencia"].ToString());
                        var precio = float.Parse(item["PrecioProducto"].ToString());
                        var perdida = string.Empty;
                        var recuperada = string.Empty;
                        var resultadoPerdida = almacen - fisico;
                        var resultadoRecuperadas = almacen - fisico;
                        
                        if (fisico < almacen)
                        {
                            perdida = Convert.ToString(Math.Abs(resultadoPerdida * precio));
                            recuperada = "---";
                        }
                        else if (fisico > almacen)
                        {
                            perdida = "---";
                            recuperada = Convert.ToString(Math.Abs(resultadoRecuperadas * precio));
                        }
                        else if (fisico.Equals(almacen) || (fisico.Equals(string.Empty) && almacen.Equals(string.Empty)))
                        {
                            recuperada = "---";
                            perdida = "---";
                        }

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

                        PdfPCell colPuntoVentaTmp = new PdfPCell(new Phrase(almacen.ToString(), fuenteNormal));
                        PuntoDeVenta += (float)Convert.ToDecimal(almacen);
                        colPuntoVentaTmp.BorderWidth = 1;
                        colPuntoVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colStockFisicoTmp = new PdfPCell(new Phrase(fisico.ToString(), fuenteNormal));
                        StockFisico += (float)Convert.ToDouble(fisico);
                        colStockFisicoTmp.BorderWidth = 1;
                        colStockFisicoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
                        colFechaTmp.BorderWidth = 1;
                        colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colDiferenciaTmp = new PdfPCell(new Phrase((diferencia * -1).ToString(), fuenteNormal));
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
                }
            }
        }
        #endregion

        #region Generar los reportes con clave interna. 
        private void GenerarReporte(int num)
        {
            var mostrarClave = FormPrincipal.clave;
            //var numFolio = obtenerFolio(num);

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
            var rutaArchivo = string.Empty;
            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            }

            var fechaHoy = DateTime.Now;
            //var rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            Paragraph numeroFolio = new Paragraph("");

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

            numeroFolio = new Paragraph("No. FOLIO: " + num, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;
            numeroFolio.Alignment = Element.ALIGN_CENTER;


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

            var consulta = cn.CargarDatos($"SELECT * FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}' AND NumFolio = '{num}'");

            foreach (DataRow row in consulta.Rows)
            {
                var nombre = row["Nombre"].ToString();
                var clave = row["ClaveInterna"].ToString();
                var codigo = row["CodigoBarras"].ToString();
                var almacen = row["StockAlmacen"].ToString();
                var fisico = row["StockFisico"].ToString();
                var fecha = row["Fecha"].ToString();
                var diferencia = row["Diferencia"].ToString();
                var precio = float.Parse(row["PrecioProducto"].ToString());
                var perdida = string.Empty;
                var recuperada = string.Empty;

                if (float.Parse(diferencia) < 0)
                {
                    perdida = (float.Parse(diferencia) * precio).ToString();
                    recuperada = "0";
                }
                else if (float.Parse(diferencia) > 0)
                {
                    recuperada = (float.Parse(diferencia) * precio).ToString();
                    perdida = "0";
                }
                else
                {
                    recuperada = "0";
                    perdida = "0";
                }

                /*var perdida =*/ /*row["Perdida"].ToString();*/
                                  /* var recuperada =*/ /*row["Recuperada"].ToString();*/

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
            reporte.Add(numeroFolio);
            reporte.Add(subTitulo);
            reporte.Add(tablaInventario);

            //================================
            //=== FIN TABLA DE INVENTARIO  ===
            //================================

            reporte.AddTitle("Reporte Inventario");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            //VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            //vr.ShowDialog();
        }
        #endregion

        #region Generar Reporte de actualizar Inventario. 
        private void GenerarReporteActualizarInventario(int num, int numRev, string rutaArchivo)
        {
            // Datos del usuario
            var datos = FormPrincipal.datosUsuario;
            //var numFolio = obtenerFolio(num);

            var actualRevision = string.Empty;
            if (tipoDatoReporte.Equals("AIAumentar"))
            {
                actualRevision = "ACTUALIZAR INVENTARIO (Aumentar)";
            }
            else
            {
                actualRevision = "ACTUALIZAR INVENTARIO (Disminuir)";
            }

            var tablaBuscar = string.Empty;

            if (tipoDatoReporte.Equals("AIAumentar"))
            {
                tablaBuscar = "dgvaumentarinventario";
            }
            else
            {
                tablaBuscar = "dgvdisminuirinventario";
            }

            // Fuentes y Colores
            var colorFuenteNegrita = new BaseColor(Color.Black);
            var colorFuenteBlanca = new BaseColor(Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, colorFuenteNegrita);

            var numRow = 0;

            var fechaHoy = DateTime.Now;
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
            
            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            Paragraph numeroFolio = new Paragraph("");

            

            string tipoReporte = string.Empty,
            encabezadoTipoReporte = string.Empty;

            float PuntoDeVenta = 0,
                    StockFisico = 0,
                    Diferencia = 0,
                    Precio = 0,
                    CantidadPerdida = 0,
                    CantidadRecuperada = 0;
            //var numerodeFolio = string.Empty;
            //var obtenerFolio = cn.CargarDatos($"SELECT Folio FROM {tablaBuscar} WHERE IDUsuario = '{FormPrincipal.userID}' AND NoRevision = '{num}'");

            //if (!obtenerFolio.Rows.Count.Equals(0))
            //{
            //    numerodeFolio = obtenerFolio.Rows[0]["Folio"].ToString();
            //}

            Usuario = new Paragraph("USUARIO: " + UsuarioActivo, fuenteNegrita);

            Paragraph subTitulo = new Paragraph($"REPORTE DE INVENTARIO\nSECCIÓN ELEGIDA " + actualRevision.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            numeroFolio = new Paragraph("No. FOLIO: " + num, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;
            numeroFolio.Alignment = Element.ALIGN_CENTER;

            float[] anchoColumnas = new float[] { 30f, 270f, 80f, 80f, 80f, 80f, 90f, 70f, 70f, 100f };

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

            PdfPCell colPuntoVenta = new PdfPCell(new Phrase("STOCK ACTUAL", fuenteTotales));
            colPuntoVenta.BorderWidth = 1;
            colPuntoVenta.HorizontalAlignment = Element.ALIGN_CENTER;
            colPuntoVenta.Padding = 3;
            colPuntoVenta.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colStockFisico = new PdfPCell(new Phrase("STOCK ANTERIOR", fuenteTotales));
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

            PdfPCell colComentario = new PdfPCell(new Phrase("COMENTARIO", fuenteTotales));
            colComentario.BorderWidth = 1;
            colComentario.HorizontalAlignment = Element.ALIGN_CENTER;
            colComentario.Padding = 3;
            colComentario.BackgroundColor = new BaseColor(Color.SkyBlue);

            //PdfPCell colPerdida = new PdfPCell(new Phrase("CANTIDAD PERDIDA", fuenteTotales));
            //colPerdida.BorderWidth = 1;
            //colPerdida.HorizontalAlignment = Element.ALIGN_CENTER;
            //colPerdida.Padding = 3;
            //colPerdida.BackgroundColor = new BaseColor(Color.SkyBlue);

            //PdfPCell colRecuperada = new PdfPCell(new Phrase("CANTIDAD RECUPERADA", fuenteTotales));
            //colRecuperada.BorderWidth = 1;
            //colRecuperada.HorizontalAlignment = Element.ALIGN_CENTER;
            //colRecuperada.Padding = 3;
            //colRecuperada.BackgroundColor = new BaseColor(Color.SkyBlue);

            tablaInventario.AddCell(colNoConcepto);
            tablaInventario.AddCell(colNombre);
            tablaInventario.AddCell(colClave);
            tablaInventario.AddCell(colCodigo);
            tablaInventario.AddCell(colPuntoVenta);
            tablaInventario.AddCell(colStockFisico);
            tablaInventario.AddCell(colFecha);
            tablaInventario.AddCell(colDiferencia);
            tablaInventario.AddCell(colPrecio);
            tablaInventario.AddCell(colComentario);
            //tablaInventario.AddCell(colPerdida);
            //tablaInventario.AddCell(colRecuperada);

            var consulta = cn.CargarDatos($"SELECT * FROM {tablaBuscar} WHERE IDUsuario = '{FormPrincipal.userID}' AND Folio = '{num}' AND NoRevision = '{numRev}'");

            foreach (DataRow row in consulta.Rows)
            {
                var nombre = row["NombreProducto"].ToString();
                var clave = row["Clave"].ToString();
                var codigo = row["Codigo"].ToString();
                var stockAnterior = row["StockActual"].ToString();
                var StockActual = row["NuevoStock"].ToString();
                var fecha = row["Fecha"].ToString();
                var diferencia = row["DiferenciaUnidades"].ToString();
                var precio = float.Parse(row["Precio"].ToString());
                var comentarios = row["Comentarios"].ToString();
                //var folio = row["Folio"].ToString();
                var perdida = string.Empty;
                var recuperada = string.Empty;

                //if (float.Parse(diferencia) < 0)
                //{
                //    perdida = (float.Parse(diferencia) * precio).ToString();
                //    recuperada = "0";
                //}
                //else if (float.Parse(diferencia) > 0)
                //{
                //    recuperada = (float.Parse(diferencia) * precio).ToString();
                //    perdida = "0";
                //}
                //else
                //{
                //    recuperada = "0";
                //    perdida = "0";
                //}

                /*var perdida =*/ /*row["Perdida"].ToString();*/
                                  /* var recuperada =*/ /*row["Recuperada"].ToString();*/

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

                PdfPCell colPuntoVentaTmp = new PdfPCell(new Phrase(StockActual, fuenteNormal));
                PuntoDeVenta += (float)Convert.ToDouble(StockActual);
                colPuntoVentaTmp.BorderWidth = 1;
                colPuntoVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmp = new PdfPCell(new Phrase(stockAnterior, fuenteNormal));
                StockFisico += (float)Convert.ToDouble(stockAnterior);
                colStockFisicoTmp.BorderWidth = 1;
                colStockFisicoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
                colFechaTmp.BorderWidth = 1;
                colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDiferenciaTmp = new PdfPCell(new Phrase(diferencia, fuenteNormal));
                if (diferencia == "") { diferencia = "0"; }
                Diferencia += (float)Convert.ToDouble(diferencia);
                colDiferenciaTmp.BorderWidth = 1;
                colDiferenciaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmp = new PdfPCell(new Phrase(precio.ToString("0.00"), fuenteNormal));
                Precio += (float)Convert.ToDouble(precio);
                colPrecioTmp.BorderWidth = 1;
                colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colComentarioTmp = new PdfPCell(new Phrase(comentarios, fuenteNormal));
                colPrecioTmp.BorderWidth = 1;
                colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                //PdfPCell colPerdidaTmp = new PdfPCell(new Phrase(.ToString("0.00"), fuenteNormal));
                //if (!perdida.Equals("---"))
                //{
                //    CantidadPerdida += (float)Convert.ToDouble(perdida);
                //}
                //colPerdidaTmp.BorderWidth = 1;
                //colPerdidaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                //PdfPCell colRecuperadaTmp = new PdfPCell(new Phrase(recuperada, fuenteNormal));
                //if (!recuperada.Equals("---"))
                //{
                //    CantidadRecuperada += (float)Convert.ToDouble(recuperada);
                //}
                //colRecuperadaTmp.BorderWidth = 1;
                //colRecuperadaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmp);
                tablaInventario.AddCell(colNombreTmp);
                tablaInventario.AddCell(colClaveTmp);
                tablaInventario.AddCell(colCodigoTmp);
                tablaInventario.AddCell(colPuntoVentaTmp);
                tablaInventario.AddCell(colStockFisicoTmp);
                tablaInventario.AddCell(colFechaTmp);
                tablaInventario.AddCell(colDiferenciaTmp);
                tablaInventario.AddCell(colPrecioTmp);
                tablaInventario.AddCell(colComentarioTmp); 
                //tablaInventario.AddCell(colPerdidaTmp);
                //tablaInventario.AddCell(colRecuperadaTmp);
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

                PdfPCell colPuntoVentaTmpExtra = new PdfPCell(new Phrase(/*PuntoDeVenta.ToString("N2")*/string.Empty, fuenteNormal));
                colPuntoVentaTmpExtra.BorderWidthTop = 0;
                colPuntoVentaTmpExtra.BorderWidthLeft = 0;
                colPuntoVentaTmpExtra.BorderWidthRight = 0;
                colPuntoVentaTmpExtra.BorderWidthBottom = 0;
                //colPuntoVentaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPuntoVentaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmpExtra = new PdfPCell(new Phrase(/*StockFisico.ToString("N2")*/string.Empty, fuenteNormal));
                colStockFisicoTmpExtra.BorderWidthTop = 0;
                colStockFisicoTmpExtra.BorderWidthLeft = 0;
                colStockFisicoTmpExtra.BorderWidthRight = 0;
                colStockFisicoTmpExtra.BorderWidthBottom = 0;
                //colStockFisicoTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
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

                //PdfPCell colPerdidaTmpExtra = new PdfPCell(new Phrase(CantidadPerdida.ToString("N2"), fuenteNormal));
                //colPerdidaTmpExtra.BorderWidthTop = 0;
                //colPerdidaTmpExtra.BorderWidthLeft = 0;
                //colPerdidaTmpExtra.BorderWidthRight = 0;
                //colPerdidaTmpExtra.BorderWidthBottom = 1;
                //colPerdidaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                //colPerdidaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                //PdfPCell colRecuperadaTmpExtra = new PdfPCell(new Phrase(CantidadRecuperada.ToString("N2"), fuenteNormal));
                //colRecuperadaTmpExtra.BorderWidthTop = 0;
                //colRecuperadaTmpExtra.BorderWidthLeft = 0;
                //colRecuperadaTmpExtra.BorderWidthRight = 0;
                //colRecuperadaTmpExtra.BorderWidthBottom = 1;
                //colRecuperadaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                //colRecuperadaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colComentarioTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colComentarioTmpExtra.BorderWidth = 0;
                colComentarioTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmpExtra);
                tablaInventario.AddCell(colNombreTmpExtra);
                tablaInventario.AddCell(colClaveTmpExtra);
                tablaInventario.AddCell(colCodigoTmpExtra);
                tablaInventario.AddCell(colPuntoVentaTmpExtra);
                tablaInventario.AddCell(colStockFisicoTmpExtra);
                tablaInventario.AddCell(colFechaTmpExtra);
                tablaInventario.AddCell(colDiferenciaTmpExtra);
                tablaInventario.AddCell(colPrecioTmpExtra);
                tablaInventario.AddCell(colComentarioTmpExtra);
                //tablaInventario.AddCell(colPerdidaTmpExtra);
                //tablaInventario.AddCell(colRecuperadaTmpExtra);
            }

            reporte.Add(titulo);
            reporte.Add(Usuario);
            reporte.Add(numeroFolio);
            reporte.Add(subTitulo);
            reporte.Add(tablaInventario);

            //================================
            //=== FIN TABLA DE INVENTARIO  ===
            //================================

            reporte.AddTitle("Reporte Inventario");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            //VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            //vr.ShowDialog();
        }
        #endregion

        private void txtBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private string obtenerFolio(int num)
        {
            var result = string.Empty;
            var tipoConsulta = string.Empty;

            if (tipoDatoReporte.Equals("RInventario"))
            {
                tipoConsulta = $"SELECT NumFolio FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}' AND NoRevision = '{num}'";
            }
            else if (tipoDatoReporte.Equals("AIAumentar"))
            {
                //tipoConsulta = $"SELECT NumFolio FROM dgvaumentarinventario WHERE IDUsuario = '{FormPrincipal.userID}' AND NoRevision = '{num}'";
                tipoConsulta = $"SELECT NumFolio FROM dgvaumentarinventario WHERE IDUsuario = '{FormPrincipal.userID}' AND Folio != 0";
            }
            else if (tipoDatoReporte.Equals("AIDisminuir"))
            {
                //tipoConsulta = $"SELECT NumFolio FROM dgvdisminuirinventario WHERE IDUsuario = '{FormPrincipal.userID}' AND NoRevision = '{num}'";
                tipoConsulta = $"SELECT NumFolio FROM dgvdisminuirinventario WHERE IDUsuario = '{FormPrincipal.userID}' AND Folio != 0";
            }

            var query = cn.CargarDatos(tipoConsulta);

            if (!query.Rows.Count.Equals(0))
            {
                if (tipoDatoReporte.Equals("RInventario"))
                {
                    result = query.Rows[0]["NumFolio"].ToString();
                }
                else
                {
                    result = query.Rows[0]["NoRevision"].ToString();
                }
            }

            return result;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            conBusqueda = true;
            DGVInventario.Rows.Clear();

            var datoBuscar = txtBuscador.Text.ToString().Replace("\r\n", string.Empty);
            var primerFecha = primerDatePicker.Value.ToString("yyyy/MM/dd");
            var segundaFecha = segundoDatePicker.Value.AddDays(1).ToString("yyyy/MM/dd");


            var rev = string.Empty; var name = string.Empty; var fecha = string.Empty;
            //var query = cn.CargarDatos(cs.BuscadorDeInventario(datoBuscar, primerFecha, segundaFecha));

            filtroConSinFiltroAvanzado = cs.BuscadorDeInventario(datoBuscar, primerFecha, segundaFecha, tipoDatoReporte);

            //if (!query.Rows.Count.Equals(0))
            //{
            //    foreach (DataRow iterar in query.Rows)
            //    {
            //        rev = iterar["NoRevision"].ToString();
            //        name = iterar["NameUsr"].ToString();
            //        fecha = iterar["Fecha"].ToString();

            //        var usr = cs.validarEmpleadoPorID();

            //        if (name.Equals(usr))
            //        {
            //            name = $"ADMIN ({name})";
            //        }

            //        DGVInventario.Rows.Add(rev, name, fecha, icono);
            //    }

            //    txtBuscador.Text = string.Empty;
            //    txtBuscador.Focus();
            //}
            //else
            //{
            //    MessageBox.Show($"No se encontraron resultados", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtBuscador.Text = string.Empty;
            txtBuscador.Focus();
            //}
            CargarDatos();
        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscador.Text.Contains("\'"))
            {
                string producto = txtBuscador.Text.Replace("\'", ""); ;
                txtBuscador.Text = producto;
                txtBuscador.Select(txtBuscador.Text.Length, 0);
            }
            txtBuscador.CharacterCasing = CharacterCasing.Upper;
        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
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

        public void CargarDatos(int status = 1, string busquedaEnProductos = "")
        {
            busqueda = string.Empty;

            busqueda = busquedaEnProductos;

            if (DGVInventario.RowCount <= 0)
            {
                if (busqueda == "")
                {
                    //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
                else if (busqueda != "")
                {
                    //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
            }
            else if (DGVInventario.RowCount >= 1 && clickBoton == 0)
            {
                if (busqueda == "")
                {
                    //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
                else if (busqueda != "")
                {
                    //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
            }

            DataSet datos = p.cargar();
            DataTable dtDatos = datos.Tables[0];

            DGVInventario.Rows.Clear();

            if (conBusqueda.Equals(true))
            {
                if (!dtDatos.Rows.Count.Equals(0))
                {
                    var rev = string.Empty;
                    var folio = string.Empty;
                    var name = string.Empty;
                    var fecha = string.Empty;
                    var usr = string.Empty;
                    var nameUsuario = string.Empty;
                    
                    var idObtenido = 0;

                    foreach (DataRow filaDatos in dtDatos.Rows)
                    {
                        if (tipoDatoReporte.Equals("RInventario"))
                        {
                            rev = filaDatos["NoRevision"].ToString();
                            folio = filaDatos["NumFolio"].ToString();
                            name = filaDatos["NameUsr"].ToString();
                            fecha = filaDatos["Fecha"].ToString();
                            usr = cs.validarEmpleadoPorID();

                            if (name.Equals(usr))
                            {
                                name = $"ADMIN ({name})";
                            }
                        }
                        else 
                        {
                            folio = filaDatos["NoRevision"].ToString();
                            rev = filaDatos["Folio"].ToString();
                            idObtenido = Convert.ToInt32(filaDatos["IDEmpleado"].ToString());
                            fecha = filaDatos["Fecha"].ToString();
                            nameUsuario = filaDatos[TipoUser].ToString();
                            

                            usr = cs.BuscarEmpleadoCaja(Convert.ToInt32(idObtenido));

                            if (/*string.IsNullOrEmpty(usr) && idObtenido.Equals("0")*/idObtenido == 0) //Admin
                            {
                                //var admin = FormPrincipal.userNickName.Split('@');
                                //name = $"ADMIN ({admin[0]})";
                                name = $"ADMIN ({nameUsuario})";
                            }
                            else if (idObtenido > 0)//Empleado
                            {
                                //var separar = nameUsuario.Split('@');
                                //name = separar[1];
                                name = nameUsuario;

                            }
                        }
                        
                        DGVInventario.Rows.Add(folio, rev, name, fecha, icono);
                    }
                }
                else
                {
                    MessageBox.Show($"No se encontraron resultados", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBuscador.Text = string.Empty;
                    txtBuscador.Focus();
                }
            }
            else
            {
                if (!dtDatos.Rows.Count.Equals(0))
                {
                    var rev = string.Empty;
                    var folio = string.Empty;
                    var name = string.Empty;
                    var fecha = string.Empty;
                    var usr = string.Empty;

                    foreach (DataRow filaDatos in dtDatos.Rows)
                    {
                        //rev = filaDatos["NoRevision"].ToString();
                        //name = filaDatos["NameUsr"].ToString();
                        //fecha = filaDatos["Fecha"].ToString();

                        //usr = cs.validarEmpleadoPorID();

                        //if (name.Equals(usr))
                        //{
                        //    name = $"ADMIN ({name})";
                        //}
                        if (tipoDatoReporte.Equals("RInventario"))
                        {
                            rev = filaDatos["NoRevision"].ToString();
                            folio = filaDatos["NumFolio"].ToString();
                            name = filaDatos["NameUsr"].ToString();
                            fecha = filaDatos["Fecha"].ToString();
                            usr = cs.validarEmpleadoPorID();

                            if (name.Equals(usr))
                            {
                                name = $"ADMIN ({name})";
                            }
                        }
                        else //Aumentar-Disminuir Inventario.
                        {
                            folio = filaDatos["NoRevision"].ToString();
                            rev = filaDatos["Folio"].ToString();
                            name = filaDatos["IDEmpleado"].ToString();
                            fecha = filaDatos["Fecha"].ToString();

                            usr = cs.BuscarEmpleadoCaja(Convert.ToInt32(name));

                            if (string.IsNullOrEmpty(usr))
                            {
                                var admin = FormPrincipal.userNickName.Split('@');
                                name = $"ADMIN ({admin[0]})";
                            }
                            else
                            {
                                name = usr;
                            }
                        }

                        DGVInventario.Rows.Add(folio, rev, name, fecha, icono);
                    }
                }
            }



            //var numeroFilas = DGVInventario.Rows.Count;

            //string Nombre = filaDatos["Nombre"].ToString();
            //string Stock = filaDatos["Stock"].ToString();
            //string Precio = filaDatos["Precio"].ToString();
            //string Clave = filaDatos["ClaveInterna"].ToString();
            //string Codigo = filaDatos["CodigoBarras"].ToString();
            //string Tipo = filaDatos["Tipo"].ToString();
            //string Proveedor = filaDatos["Proveedor"].ToString();
            //string chckName = filaDatos["ChckName"].ToString();
            //string Descripcion = filaDatos["Descripcion"].ToString();

            //if (DGVInventario.Rows.Count.Equals(0))
            //{
            //    bool encontrado = Utilidades.BuscarDataGridView(Nombre, "Nombre", DGVInventario);

            //    if (encontrado.Equals(false))
            //    {
            //        var number_of_rows = DGVInventario.Rows.Add();
            //        DataGridViewRow row = DGVInventario.Rows[number_of_rows];

            //        row.Cells["Nombre"].Value = Nombre;     // Columna Nombre
            //        row.Cells["Stock"].Value = Stock;       // Columna Stock
            //        row.Cells["Precio"].Value = Precio;     // Columna Precio
            //        row.Cells["Clave"].Value = Clave;       // Columna Clave
            //        row.Cells["Codigo"].Value = Codigo;     // Columna Codigo

            //        // Columna Tipo
            //        if (Tipo.Equals("P"))
            //        {
            //            row.Cells["Tipo"].Value = "PRODUCTO";
            //        }
            //        else if (Tipo.Equals("S"))
            //        {
            //            row.Cells["Tipo"].Value = "SERVICIO";
            //        }
            //        else if (Tipo.Equals("PQ"))
            //        {
            //            row.Cells["Tipo"].Value = "COMBO";
            //        }

            //        row.Cells["Proveedor"].Value = Proveedor;   // Columna Proveedor

            //        if (DGVInventario.Columns.Contains(chckName))
            //        {
            //            row.Cells[chckName].Value = Descripcion;
            //        }
            //    }
            //}
            //else if (!DGVInventario.Rows.Count.Equals(0))
            //{
            //    foreach (DataGridViewRow Row in DGVInventario.Rows)
            //    {
            //        bool encontrado = Utilidades.BuscarDataGridView(Nombre, "Nombre", DGVInventario);

            //        if (encontrado.Equals(true))
            //        {
            //            var Fila = Row.Index;
            //            // Columnas Dinamicos
            //            if (DGVInventario.Columns.Contains(chckName))
            //            {
            //                DGVInventario.Rows[Fila].Cells[chckName].Value = Descripcion;
            //            }
            //        }
            //        else if (encontrado.Equals(false))
            //        {
            //            var number_of_rows = DGVInventario.Rows.Add();
            //            DataGridViewRow row = DGVInventario.Rows[number_of_rows];

            //            row.Cells["Nombre"].Value = Nombre;         // Columna Nombre
            //            row.Cells["Stock"].Value = Stock;           // Columna Stock
            //            row.Cells["Precio"].Value = Precio;         // Columna Precio
            //            row.Cells["Clave"].Value = Clave;           // Columna Clave
            //            row.Cells["Codigo"].Value = Codigo;         // Columna Codigo

            //            // Columna Tipo
            //            if (Tipo.Equals("P"))
            //            {
            //                row.Cells["Tipo"].Value = "PRODUCTO";
            //            }
            //            else if (Tipo.Equals("S"))
            //            {
            //                row.Cells["Tipo"].Value = "SERVICIO";
            //            }
            //            else if (Tipo.Equals("PQ"))
            //            {
            //                row.Cells["Tipo"].Value = "COMBO";
            //            }

            //            // Columna Proveedor
            //            row.Cells["Proveedor"].Value = Proveedor;

            //            // Columnas Dinamicos
            //            if (DGVInventario.Columns.Contains(chckName))
            //            {
            //                row.Cells[chckName].Value = Descripcion;
            //            }
            //        }
            //    }
            //}
            //}

            actualizar();

            clickBoton = 0;
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

            //if (string.IsNullOrEmpty(txtMaximoPorPagina.Text))
            //{
            //    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
            //}
            //maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
            //p.actualizarTope(maximo_x_pagina);
            //CargarDatos();
            //actualizar();
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

                //if (string.IsNullOrEmpty(txtMaximoPorPagina.Text))
                //{
                //    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                //}
                //maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
                //p.actualizarTope(maximo_x_pagina);
                //CargarDatos();
                //actualizar();
            }
        }

        private void BuscadorReporteInventario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void DGVInventario_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                DGVInventario.Cursor = Cursors.Hand;
            }
            else
            {
                DGVInventario.Cursor = Cursors.Default;
            }
        }

        private void primerDatePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTime date = DateTime.Now;
            DateTime PrimerDia = new DateTime(date.Year, date.Month, 1);
            primerDatePicker.Value = PrimerDia;
        }

        private void segundoDatePicker_ValueChanged(object sender, EventArgs e)
        {
            segundoDatePicker.Value = DateTime.Now;
        }
    }
}
