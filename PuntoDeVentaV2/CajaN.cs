using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class CajaN : Form
    {

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        public static bool recargarDatos = false;

        // Variables Totales
        public static float totalEfectivo = 0f;
        public static float totalTarjeta = 0f;
        public static float totalVales = 0f;
        public static float totalCheque = 0f;
        public static float totalTransferencia = 0f;
        public static float totalCredito = 0f;

        // Variables Retiro
        public static float retiroEfectivo = 0f;
        public static float retiroTarjeta = 0f;
        public static float retiroVales = 0f;
        public static float retiroCheque = 0f;
        public static float retiroTrans = 0f;

        public static DateTime fechaGeneral;
        public static DateTime fechaCorte = Convert.ToDateTime("2019-10-10 12:00:35");

        public CajaN()
        {
            InitializeComponent();
        }

        private void CajaN_Load(object sender, EventArgs e)
        {
            
        }

        private void btnReporteAgregar_Click(object sender, EventArgs e)
        {
            ReporteDineroAgregado agregado = new ReporteDineroAgregado(fechaGeneral);

            agregado.ShowDialog();
        }

        private void btnReporteRetirar_Click(object sender, EventArgs e)
        {
            ReporteDineroRetirado retirado = new ReporteDineroRetirado(fechaGeneral);

            retirado.ShowDialog();
        }

        private void btnAgregarDinero_Click(object sender, EventArgs e)
        {
            AgregarRetirarDinero agregar = new AgregarRetirarDinero();

            agregar.FormClosed += delegate
            {
                CargarSaldo();
            };

            agregar.ShowDialog();
        }

        private void btnRetirarDinero_Click(object sender, EventArgs e)
        {
            AgregarRetirarDinero retirar = new AgregarRetirarDinero(1);

            retirar.FormClosed += delegate
            {
                CargarSaldo();
            };

            retirar.ShowDialog();
        }

        private void btnCorteCaja_Click(object sender, EventArgs e)
        {
            AgregarRetirarDinero corte = new AgregarRetirarDinero(2);

            /*corte.FormClosed += delegate
            {
                CargarSaldo();
                GenerarReporte(1);
            };

            corte.ShowDialog();*/

            GenerarReporte(1);
        }

        #region Metodo para cargar saldos y totales
        private void CargarSaldo()
        {
            SQLiteConnection sql_con;
            SQLiteCommand consultaUno, consultaDos;
            SQLiteDataReader drUno, drDos;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();

            var fechaDefault = Convert.ToDateTime("0001-01-01 00:00:00");

            var consultarFecha = $"SELECT FechaOperacion FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'corte' ORDER BY FeChaOperacion DESC LIMIT 1";
            consultaUno = new SQLiteCommand(consultarFecha, sql_con);
            drUno = consultaUno.ExecuteReader();

            if (drUno.Read())
            {
                var fechaTmp = Convert.ToDateTime(drUno.GetValue(drUno.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                fechaDefault = Convert.ToDateTime(fechaTmp);
            }

            fechaGeneral = fechaDefault;

            var consulta = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID}";
            consultaDos = new SQLiteCommand(consulta, sql_con);
            drDos = consultaDos.ExecuteReader();

            // Variables ventas
            float vEfectivo = 0f;
            float vTarjeta = 0f;
            float vVales = 0f;
            float vCheque = 0f;
            float vTrans = 0f;
            float vCredito = 0f;
            float vAnticipos = 0f;

            // Variables anticipos
            float aEfectivo = 0f;
            float aTarjeta = 0f;
            float aVales = 0f;
            float aCheque = 0f;
            float aTrans = 0f;

            // Variables depositos
            float dEfectivo = 0f;
            float dTarjeta = 0f;
            float dVales = 0f;
            float dCheque = 0f;
            float dTrans = 0f;

            // Variables caja
            float efectivo = 0f;
            float tarjeta = 0f;
            float vales = 0f;
            float cheque = 0f;
            float trans = 0f;
            float credito = 0f;
            float subtotal = 0f;
            float anticipos = 0f;

            // Variable retiro
            float dineroRetirado = 0f;
            retiroEfectivo = 0f;
            retiroTarjeta = 0f;
            retiroVales = 0f;
            retiroCheque = 0f;
            retiroTrans = 0f;

            while (drDos.Read())
            {
                string operacion = drDos.GetValue(drDos.GetOrdinal("Operacion")).ToString();
                var auxiliar = Convert.ToDateTime(drDos.GetValue(drDos.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                var fechaOperacion = Convert.ToDateTime(auxiliar);

                if (operacion == "venta" && fechaOperacion > fechaDefault)
                {
                    vEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    vTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    vVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    vCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    vTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    vCredito += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());
                    vAnticipos += float.Parse(drDos.GetValue(drDos.GetOrdinal("Anticipo")).ToString());
                }

                if (operacion == "anticipo" && fechaOperacion > fechaDefault)
                {
                    aEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    aTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    aVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    aCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    aTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                }

                if (operacion == "deposito" && fechaOperacion > fechaDefault)
                {
                    dEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    dTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    dVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    dCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    dTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                }

                if (operacion == "retiro" && fechaOperacion > fechaDefault)
                {
                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    retiroEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    retiroTarjeta  += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    retiroVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    retiroCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    retiroTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                }
            }

            // Cerramos la conexion y el datareader
            drUno.Close();
            drDos.Close();
            sql_con.Close();


            // Apartado VENTAS
            lbTEfectivo.Text = "$" + vEfectivo.ToString("0.00");
            lbTTarjeta.Text = "$" + vTarjeta.ToString("0.00");
            lbTVales.Text = "$" + vVales.ToString("0.00");
            lbTCheque.Text = "$" + vCheque.ToString("0.00");
            lbTTrans.Text = "$" + vTrans.ToString("0.00");
            lbTCredito.Text = "$" + vCredito.ToString("0.00");
            lbTAnticipos.Text = "$" + vAnticipos.ToString("0.00");
            lbTVentas.Text = "$" + (vEfectivo + vTarjeta + vVales + vCheque + vTrans + vCredito + vAnticipos).ToString("0.00");
            tituloSeccion.Text = "SALDO INICIAL: $" + (vEfectivo + vTarjeta + vVales + vCheque + vTrans + vCredito + vAnticipos).ToString("0.00");

            // Apartado ANTICIPOS RECIBIDOS
            lbTEfectivoA.Text = "$" + aEfectivo.ToString("0.00");
            lbTTarjetaA.Text = "$" + aTarjeta.ToString("0.00");
            lbTValesA.Text = "$" + aVales.ToString("0.00");
            lbTChequeA.Text = "$" + aCheque.ToString("0.00");
            lbTTransA.Text = "$" + aTrans.ToString("0.00");
            lbTAnticiposA.Text = "$" + (aEfectivo + aTarjeta + aVales + aCheque + aTrans).ToString("0.00");

            // Apartado DINERO AGREGADO
            lbTEfectivoD.Text = "$" + dEfectivo.ToString("0.00");
            lbTTarjetaD.Text = "$" + dTarjeta.ToString("0.00");
            lbTValesD.Text = "$" + dVales.ToString("0.00");
            lbTChequeD.Text = "$" + dCheque.ToString("0.00");
            lbTTransD.Text = "$" + dTrans.ToString("0.00");
            lbTAgregado.Text = "$" + (dEfectivo + dTarjeta + dVales + dCheque + dTrans).ToString("0.00");

            // Apartado TOTAL EN CAJA
            efectivo = vEfectivo + aEfectivo + dEfectivo;
            tarjeta = vTarjeta + aTarjeta + dTarjeta;
            vales = vVales + aVales + dVales;
            cheque = vCheque + aCheque + dCheque;
            trans = vTrans + aTrans + dTrans;
            credito = vCredito;
            anticipos = vAnticipos;
            subtotal = efectivo + tarjeta + vales + cheque + trans + credito;

            lbTEfectivoC.Text = "$" + efectivo.ToString("0.00");
            lbTTarjetaC.Text = "$" + tarjeta.ToString("0.00");
            lbTValesC.Text = "$" + vales.ToString("0.00");
            lbTChequeC.Text = "$" + cheque.ToString("0.00");
            lbTTransC.Text = "$" + trans.ToString("0.00");
            lbTCreditoC.Text = "$" + credito.ToString("0.00");
            lbTAnticiposC.Text = "$" + anticipos.ToString("0.00");
            lbTSubtotal.Text = "$" + subtotal.ToString("0.00");
            lbTDineroRetirado.Text = "$" + dineroRetirado.ToString("0.00");
            lbTTotalCaja.Text = "$" + (subtotal - dineroRetirado).ToString("0.00");

            // Variables de clase
            totalEfectivo = efectivo;
            totalTarjeta = tarjeta;
            totalVales = vales;
            totalCheque = cheque;
            totalTransferencia = trans;
            totalCredito = credito;
        }
        #endregion

        private void CajaN_Paint(object sender, PaintEventArgs e)
        {
            if (recargarDatos)
            {
                CargarSaldo();
                recargarDatos = false;
            }
        }

        private void CajaN_Resize(object sender, EventArgs e)
        {
            recargarDatos = false;
        }

        private void GenerarReporte(int idReporte)
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
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, colorFuenteBlanca);

            // Ruta donde se creara el archivo PDF
            var rutaArchivo = @"C:\Archivos PUDVE\Reportes\Caja\reporte_" + idReporte + ".pdf";

            float[] anchoColumnas = new float[] { 30f, 30f, 20f, 20f, 20f, 40f, 20f, 20f, 30f, 30f };

            Document reporte = new Document(PageSize.A3);
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
            Paragraph subTitulo = new Paragraph("CORTE DE CAJA\nFecha: " + fechaCorte.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;

            //==============================================
            //===       TABLA DE VENTAS REALIZADAS       ===
            //==============================================

            #region Tabla de Venta
            PdfPTable tabla = new PdfPTable(10);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(anchoColumnas);

            PdfPCell colCliente = new PdfPCell(new Phrase("Cliente", fuenteNegrita));
            colCliente.BorderWidth = 0;
            colCliente.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colRFC = new PdfPCell(new Phrase("RFC", fuenteNegrita));
            colRFC.BorderWidth = 0;
            colRFC.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colSubtotal = new PdfPCell(new Phrase("Subtotal", fuenteNegrita));
            colSubtotal.BorderWidth = 0;
            colSubtotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colIVA = new PdfPCell(new Phrase("IVA", fuenteNegrita));
            colIVA.BorderWidth = 0;
            colIVA.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colTotal = new PdfPCell(new Phrase("Total", fuenteNegrita));
            colTotal.BorderWidth = 0;
            colTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colFecha = new PdfPCell(new Phrase("Fecha", fuenteNegrita));
            colFecha.BorderWidth = 0;
            colFecha.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colFolio = new PdfPCell(new Phrase("Folio", fuenteNegrita));
            colFolio.BorderWidth = 0;
            colFolio.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colSerie = new PdfPCell(new Phrase("Serie", fuenteNegrita));
            colSerie.BorderWidth = 0;
            colSerie.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colPago = new PdfPCell(new Phrase("Pago", fuenteNegrita));
            colPago.BorderWidth = 0;
            colPago.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colEmpleado = new PdfPCell(new Phrase("Empleado", fuenteNegrita));
            colEmpleado.BorderWidth = 0;
            colEmpleado.HorizontalAlignment = Element.ALIGN_CENTER;

            tabla.AddCell(colCliente);
            tabla.AddCell(colRFC);
            tabla.AddCell(colSubtotal);
            tabla.AddCell(colIVA);
            tabla.AddCell(colTotal);
            tabla.AddCell(colFecha);
            tabla.AddCell(colFolio);
            tabla.AddCell(colSerie);
            tabla.AddCell(colPago);
            tabla.AddCell(colEmpleado);

            // Consulta para obtener los registros de las ventas
            SQLiteConnection sql_con;
            SQLiteCommand primerConsulta, segundaConsulta;
            SQLiteDataReader drUno, drDos;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();
            primerConsulta = new SQLiteCommand($"SELECT * FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 AND FechaOperacion < '{fechaCorte.ToString("yyyy-MM-dd HH:mm:ss")}'", sql_con);
            drUno = primerConsulta.ExecuteReader();

            var sumaSubtotal = 0f;
            var sumaIVA = 0f;
            var sumaTotal = 0f;

            while (drUno.Read())
            {
                var idVenta = Convert.ToInt32(drUno.GetValue(drUno.GetOrdinal("ID")));
                var subtotal = float.Parse(drUno.GetValue(drUno.GetOrdinal("Subtotal")).ToString());
                var iva = float.Parse(drUno.GetValue(drUno.GetOrdinal("IVA16")).ToString());
                var total = float.Parse(drUno.GetValue(drUno.GetOrdinal("Total")).ToString());
                var folio = drUno.GetValue(drUno.GetOrdinal("Folio"));
                var serie = drUno.GetValue(drUno.GetOrdinal("Serie"));
                var fecha = Convert.ToDateTime(drUno.GetValue(drUno.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");

                sumaSubtotal += subtotal;
                sumaIVA += iva;
                sumaTotal += total;

                segundaConsulta = new SQLiteCommand($"SELECT * FROM DetallesVenta WHERE IDVenta = {idVenta} AND IDUsuario = {FormPrincipal.userID}", sql_con);
                drDos = segundaConsulta.ExecuteReader();

                var clienteNombre = string.Empty;
                var clienteRFC = string.Empty;
                var metodoPago = string.Empty;

                if (drDos.Read())
                {
                    var idCliente = Convert.ToInt32(drDos.GetValue(drDos.GetOrdinal("IDCliente")));
                    var efectivo = float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    var tarjeta = float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    var vales = float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    var cheque = float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    var trans = float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    var credito = float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());

                    metodoPago = (new[] {
                        Tuple.Create("Efectivo", efectivo),
                        Tuple.Create("Tarjeta", tarjeta),
                        Tuple.Create("Vales", vales),
                        Tuple.Create("Cheque", cheque),
                        Tuple.Create("Transferencia", trans),
                        Tuple.Create("Crédito", credito)
                    }).OrderByDescending(t => t.Item2).First().Item1;

                    
                    // Obtener datos del Cliente
                    if (idCliente > 0)
                    {
                        var clienteTmp = mb.ObtenerDatosCliente(idCliente, FormPrincipal.userID);
                        clienteNombre = clienteTmp[0];
                        clienteRFC = clienteTmp[1];
                    }
                    else
                    {
                        clienteNombre = "Público General";
                        clienteRFC = "XAXX010101000";
                    }
                }

                PdfPCell colClienteTmp = new PdfPCell(new Phrase(clienteNombre, fuenteNormal));
                colClienteTmp.BorderWidth = 0;
                colClienteTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRFCTmp = new PdfPCell(new Phrase(clienteRFC, fuenteNormal));
                colRFCTmp.BorderWidth = 0;
                colRFCTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colSubtotalTmp = new PdfPCell(new Phrase(subtotal.ToString(), fuenteNormal));
                colSubtotalTmp.BorderWidth = 0;
                colSubtotalTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colIVATmp = new PdfPCell(new Phrase(iva.ToString(), fuenteNormal));
                colIVATmp.BorderWidth = 0;
                colIVATmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colTotalTmp = new PdfPCell(new Phrase(total.ToString(), fuenteNormal));
                colTotalTmp.BorderWidth = 0;
                colTotalTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
                colFechaTmp.BorderWidth = 0;
                colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFolioTmp = new PdfPCell(new Phrase(folio.ToString(), fuenteNormal));
                colFolioTmp.BorderWidth = 0;
                colFolioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colSerieTmp = new PdfPCell(new Phrase(serie.ToString(), fuenteNormal));
                colSerieTmp.BorderWidth = 0;
                colSerieTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPagoTmp = new PdfPCell(new Phrase(metodoPago, fuenteNormal));
                colPagoTmp.BorderWidth = 0;
                colPagoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colEmpleadoTmp = new PdfPCell(new Phrase("Admin", fuenteNormal));
                colEmpleadoTmp.BorderWidth = 0;
                colEmpleadoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tabla.AddCell(colClienteTmp);
                tabla.AddCell(colRFCTmp);
                tabla.AddCell(colSubtotalTmp);
                tabla.AddCell(colIVATmp);
                tabla.AddCell(colTotalTmp);
                tabla.AddCell(colFechaTmp);
                tabla.AddCell(colFolioTmp);
                tabla.AddCell(colSerieTmp);
                tabla.AddCell(colPagoTmp);
                tabla.AddCell(colEmpleadoTmp);

                drDos.Close();
            }

            PdfPCell colSeparador = new PdfPCell(new Phrase(Chunk.NEWLINE));
            colSeparador.Colspan = 10;
            colSeparador.BorderWidth = 0;

            PdfPCell colTituloTotal = new PdfPCell(new Phrase("TOTAL", fuenteTotales));
            colTituloTotal.BorderWidth = 0;
            colTituloTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTituloTotal.BackgroundColor = new BaseColor(Color.Red);

            // Columna temporal para hacer espacio en los totales
            PdfPCell colTmp1 = new PdfPCell(new Phrase("", fuenteTotales));
            colTmp1.BorderWidth = 0;
            colTmp1.HorizontalAlignment = Element.ALIGN_CENTER;
            colTmp1.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colCantidadSubtotal = new PdfPCell(new Phrase(sumaSubtotal.ToString(), fuenteTotales));
            colCantidadSubtotal.BorderWidth = 0;
            colCantidadSubtotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colCantidadSubtotal.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colCantidadIVA = new PdfPCell(new Phrase(sumaIVA.ToString(), fuenteTotales));
            colCantidadIVA.BorderWidth = 0;
            colCantidadIVA.HorizontalAlignment = Element.ALIGN_CENTER;
            colCantidadIVA.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colCantidadTotal = new PdfPCell(new Phrase(sumaTotal.ToString(), fuenteTotales));
            colCantidadTotal.BorderWidth = 0;
            colCantidadTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colCantidadTotal.BackgroundColor = new BaseColor(Color.Red);

            // Columna temporal para hacer espacio en los totales
            PdfPCell colTmp2 = new PdfPCell(new Phrase("", fuenteTotales));
            colTmp2.Colspan = 5;
            colTmp2.BorderWidth = 0;
            colTmp2.HorizontalAlignment = Element.ALIGN_CENTER;
            colTmp2.BackgroundColor = new BaseColor(Color.Red);

            tabla.AddCell(colSeparador);
            tabla.AddCell(colTituloTotal);
            tabla.AddCell(colTmp1);
            tabla.AddCell(colCantidadSubtotal);
            tabla.AddCell(colCantidadIVA);
            tabla.AddCell(colCantidadTotal);
            tabla.AddCell(colTmp2);

            drUno.Close();
            sql_con.Close();
            #endregion
            //==============================================
            //===    FIN  TABLA DE VENTAS REALIZADAS     ===
            //==============================================

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //==============================================
            //=== TABLA HISTORIAL DE DEPOSITOS Y RETIROS ===
            //==============================================
            Paragraph tituloHistorial = new Paragraph("HISTORIAL DE DEPOSITOS Y RETIROS\n\n", fuenteGrande);
            tituloHistorial.Alignment = Element.ALIGN_CENTER;

            anchoColumnas = new float[] { 30f, 30f, 20f, 20f, 20f, 40f };

            PdfPTable tablaHistorial = new PdfPTable(6);
            tablaHistorial.WidthPercentage = 80;
            tablaHistorial.SetWidths(anchoColumnas);

            PdfPCell cEmpleado = new PdfPCell(new Phrase("Empleado", fuenteNegrita));
            cEmpleado.BorderWidth = 0;
            cEmpleado.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell cTipo = new PdfPCell(new Phrase("Tipo", fuenteNegrita));
            cTipo.BorderWidth = 0;
            cTipo.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell cCantidad = new PdfPCell(new Phrase("Cantidad", fuenteNegrita));
            cCantidad.BorderWidth = 0;
            cCantidad.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell cSaldo = new PdfPCell(new Phrase("Saldo", fuenteNegrita));
            cSaldo.BorderWidth = 0;
            cSaldo.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell cFecha = new PdfPCell(new Phrase("Fecha", fuenteNegrita));
            cFecha.BorderWidth = 0;
            cFecha.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell cConcepto = new PdfPCell(new Phrase("Concepto", fuenteNegrita));
            cConcepto.BorderWidth = 0;
            cConcepto.HorizontalAlignment = Element.ALIGN_CENTER;

            tablaHistorial.AddCell(cEmpleado);
            tablaHistorial.AddCell(cTipo);
            tablaHistorial.AddCell(cCantidad);
            tablaHistorial.AddCell(cSaldo);
            tablaHistorial.AddCell(cFecha);
            tablaHistorial.AddCell(cConcepto);


            reporte.Add(titulo);
            reporte.Add(subTitulo);
            reporte.Add(tabla);
            reporte.Add(linea);
            reporte.Add(tituloHistorial);
            reporte.Add(tablaHistorial);

            reporte.AddTitle("Reporte Corte de Caja");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }
    }
}
