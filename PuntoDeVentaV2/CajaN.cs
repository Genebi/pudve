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

        private string[] ObtenerCantidades()
        {
            List<string> lista = new List<string>();

            lista.Add(lbTEfectivo.Text);
            lista.Add(lbTEfectivoA.Text);
            lista.Add(lbTEfectivoD.Text);
            lista.Add(lbTEfectivoC.Text);

            lista.Add(lbTTarjeta.Text);
            lista.Add(lbTTarjetaA.Text);
            lista.Add(lbTTarjetaD.Text);
            lista.Add(lbTTarjetaC.Text);

            lista.Add(lbTVales.Text);
            lista.Add(lbTValesA.Text);
            lista.Add(lbTValesD.Text);
            lista.Add(lbTValesC.Text);

            lista.Add(lbTCheque.Text);
            lista.Add(lbTChequeA.Text);
            lista.Add(lbTChequeD.Text);
            lista.Add(lbTChequeC.Text);

            lista.Add(lbTTrans.Text);
            lista.Add(lbTTransA.Text);
            lista.Add(lbTTransD.Text);
            lista.Add(lbTTransC.Text);

            lista.Add(lbTCredito.Text);
            lista.Add(lbTCreditoC.Text);

            lista.Add(lbTAnticipos.Text);
            lista.Add(lbTAnticiposC.Text);

            lista.Add(lbTSubtotal.Text);
            lista.Add(lbTDineroRetirado.Text);

            lista.Add(lbTVentas.Text);
            lista.Add(lbTAnticiposA.Text);
            lista.Add(lbTAgregado.Text);
            lista.Add(lbTTotalCaja.Text);

            return lista.ToArray();
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

            Document reporte = new Document(PageSize.A3);
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
            Paragraph subTitulo = new Paragraph("CORTE DE CAJA\nFecha: " + fechaCorte.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;

            //===========================================
            //===       TABLAS DE CORTE DE CAJA       ===
            //===========================================

            #region Tabla de Venta

            // Cantidades de las columnas
            var cantidades = ObtenerCantidades();

            float[] anchoColumnas = new float[] { 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f };

            // Encabezados
            PdfPTable tabla = new PdfPTable(8);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(anchoColumnas);

            PdfPCell colVentas = new PdfPCell(new Phrase("VENTAS", fuenteNegrita));
            colVentas.Colspan = 2;
            colVentas.BorderWidth = 1;
            colVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colVentas.Padding = 5;

            PdfPCell colAnticipos = new PdfPCell(new Phrase("ANTICIPOS RECIBIDOS", fuenteNegrita));
            colAnticipos.Colspan = 2;
            colAnticipos.BorderWidth = 1;
            colAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticipos.Padding = 5;

            PdfPCell colDinero = new PdfPCell(new Phrase("DINERO AGREGADO", fuenteNegrita));
            colDinero.Colspan = 2;
            colDinero.BorderWidth = 1;
            colDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colDinero.Padding = 5;

            PdfPCell colTotal = new PdfPCell(new Phrase("TOTAL EN CAJA", fuenteNegrita));
            colTotal.Colspan = 2;
            colTotal.BorderWidth = 1;
            colTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotal.Padding = 5;

            tabla.AddCell(colVentas);
            tabla.AddCell(colAnticipos);
            tabla.AddCell(colDinero);
            tabla.AddCell(colTotal);

            // Linea de EFECTIVO
            PdfPCell colEfectivoVentas = new PdfPCell(new Phrase($"Efectivo", fuenteNormal));
            colEfectivoVentas.BorderWidth = 1;
            colEfectivoVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoVentas.Padding = 3;

            PdfPCell colEfectivoVentasC = new PdfPCell(new Phrase($"{cantidades[0]}", fuenteNormal));
            colEfectivoVentasC.BorderWidth = 1;
            colEfectivoVentasC.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoVentasC.Padding = 3;

            PdfPCell colEfectivoAnticipos = new PdfPCell(new Phrase($"Efectivo", fuenteNormal));
            colEfectivoAnticipos.BorderWidth = 1;
            colEfectivoAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoAnticipos.Padding = 3;

            PdfPCell colEfectivoAnticiposC = new PdfPCell(new Phrase($"{cantidades[1]}", fuenteNormal));
            colEfectivoAnticiposC.BorderWidth = 1;
            colEfectivoAnticiposC.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoAnticiposC.Padding = 3;

            PdfPCell colEfectivoDinero = new PdfPCell(new Phrase($"Efectivo", fuenteNormal));
            colEfectivoDinero.BorderWidth = 1;
            colEfectivoDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoDinero.Padding = 3;

            PdfPCell colEfectivoDineroC = new PdfPCell(new Phrase($"{cantidades[2]}", fuenteNormal));
            colEfectivoDineroC.BorderWidth = 1;
            colEfectivoDineroC.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoDineroC.Padding = 3;

            PdfPCell colEfectivoTotal = new PdfPCell(new Phrase($"Efectivo", fuenteNormal));
            colEfectivoTotal.BorderWidth = 1;
            colEfectivoTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoTotal.Padding = 3;

            PdfPCell colEfectivoTotalC = new PdfPCell(new Phrase($"{cantidades[3]}", fuenteNormal));
            colEfectivoTotalC.BorderWidth = 1;
            colEfectivoTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoTotalC.Padding = 3;

            tabla.AddCell(colEfectivoVentas);
            tabla.AddCell(colEfectivoVentasC);
            tabla.AddCell(colEfectivoAnticipos);
            tabla.AddCell(colEfectivoAnticiposC);
            tabla.AddCell(colEfectivoDinero);
            tabla.AddCell(colEfectivoDineroC);
            tabla.AddCell(colEfectivoTotal);
            tabla.AddCell(colEfectivoTotalC);


            // Linea de TARJETA
            PdfPCell colTarjetaVentas = new PdfPCell(new Phrase($"Tarjeta", fuenteNormal));
            colTarjetaVentas.BorderWidth = 1;
            colTarjetaVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaVentas.Padding = 3;

            PdfPCell colTarjetaVentasC = new PdfPCell(new Phrase($"{cantidades[4]}", fuenteNormal));
            colTarjetaVentasC.BorderWidth = 1;
            colTarjetaVentasC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaVentasC.Padding = 3;

            PdfPCell colTarjetaAnticipos = new PdfPCell(new Phrase($"Tarjeta", fuenteNormal));
            colTarjetaAnticipos.BorderWidth = 1;
            colTarjetaAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaAnticipos.Padding = 3;

            PdfPCell colTarjetaAnticiposC = new PdfPCell(new Phrase($"{cantidades[5]}", fuenteNormal));
            colTarjetaAnticiposC.BorderWidth = 1;
            colTarjetaAnticiposC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaAnticiposC.Padding = 3;

            PdfPCell colTarjetaDinero = new PdfPCell(new Phrase($"Tarjeta", fuenteNormal));
            colTarjetaDinero.BorderWidth = 1;
            colTarjetaDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaDinero.Padding = 3;

            PdfPCell colTarjetaDineroC = new PdfPCell(new Phrase($"{cantidades[6]}", fuenteNormal));
            colTarjetaDineroC.BorderWidth = 1;
            colTarjetaDineroC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaDineroC.Padding = 3;

            PdfPCell colTarjetaTotal = new PdfPCell(new Phrase($"Tarjeta", fuenteNormal));
            colTarjetaTotal.BorderWidth = 1;
            colTarjetaTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaTotal.Padding = 3;

            PdfPCell colTarjetaTotalC = new PdfPCell(new Phrase($"{cantidades[7]}", fuenteNormal));
            colTarjetaTotalC.BorderWidth = 1;
            colTarjetaTotalC.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaTotalC.Padding = 3;

            tabla.AddCell(colTarjetaVentas);
            tabla.AddCell(colTarjetaVentasC);
            tabla.AddCell(colTarjetaAnticipos);
            tabla.AddCell(colTarjetaAnticiposC);
            tabla.AddCell(colTarjetaDinero);
            tabla.AddCell(colTarjetaDineroC);
            tabla.AddCell(colTarjetaTotal);
            tabla.AddCell(colTarjetaTotalC);

            // Linea de VALES
            /*PdfPCell colValesVentas = new PdfPCell(new Phrase($"Vales {cantidades[8]}", fuenteNormal));
            colValesVentas.BorderWidth = 1;
            colValesVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesVentas.Padding = 3;

            PdfPCell colValesAnticipos = new PdfPCell(new Phrase($"Vales {cantidades[9]}", fuenteNormal));
            colValesAnticipos.BorderWidth = 1;
            colValesAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesAnticipos.Padding = 3;

            PdfPCell colValesDinero = new PdfPCell(new Phrase($"Vales {cantidades[10]}", fuenteNormal));
            colValesDinero.BorderWidth = 1;
            colValesDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesDinero.Padding = 3;

            PdfPCell colValesTotal = new PdfPCell(new Phrase($"Vales {cantidades[11]}", fuenteNormal));
            colValesTotal.BorderWidth = 1;
            colValesTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesTotal.Padding = 3;

            tabla.AddCell(colValesVentas);
            tabla.AddCell(colValesAnticipos);
            tabla.AddCell(colValesDinero);
            tabla.AddCell(colValesTotal);


            // Linea de CHEQUE
            PdfPCell colChequeVentas = new PdfPCell(new Phrase($"Cheque {cantidades[12]}", fuenteNormal));
            colChequeVentas.BorderWidth = 1;
            colChequeVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeVentas.Padding = 3;

            PdfPCell colChequeAnticipos = new PdfPCell(new Phrase($"Cheque {cantidades[13]}", fuenteNormal));
            colChequeAnticipos.BorderWidth = 1;
            colChequeAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeAnticipos.Padding = 3;

            PdfPCell colChequeDinero = new PdfPCell(new Phrase($"Cheque {cantidades[14]}", fuenteNormal));
            colChequeDinero.BorderWidth = 1;
            colChequeDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeDinero.Padding = 3;

            PdfPCell colChequeTotal = new PdfPCell(new Phrase($"Cheque {cantidades[15]}", fuenteNormal));
            colChequeTotal.BorderWidth = 1;
            colChequeTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeTotal.Padding = 3;

            tabla.AddCell(colChequeVentas);
            tabla.AddCell(colChequeAnticipos);
            tabla.AddCell(colChequeDinero);
            tabla.AddCell(colChequeTotal);


            // Linea de TRANSFERENCIA
            PdfPCell colTransVentas = new PdfPCell(new Phrase($"Transferencia {cantidades[16]}", fuenteNormal));
            colTransVentas.BorderWidth = 1;
            colTransVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransVentas.Padding = 3;

            PdfPCell colTransAnticipos = new PdfPCell(new Phrase($"Transferencia {cantidades[17]}", fuenteNormal));
            colTransAnticipos.BorderWidth = 1;
            colTransAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransAnticipos.Padding = 3;

            PdfPCell colTransDinero = new PdfPCell(new Phrase($"Transferencia {cantidades[18]}", fuenteNormal));
            colTransDinero.BorderWidth = 1;
            colTransDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransDinero.Padding = 3;

            PdfPCell colTransTotal = new PdfPCell(new Phrase($"Transferencia {cantidades[19]}", fuenteNormal));
            colTransTotal.BorderWidth = 1;
            colTransTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransTotal.Padding = 3;

            tabla.AddCell(colTransVentas);
            tabla.AddCell(colTransAnticipos);
            tabla.AddCell(colTransDinero);
            tabla.AddCell(colTransTotal);


            // Linea de CREDITO
            PdfPCell colCreditoVentas = new PdfPCell(new Phrase($"Crédito {cantidades[20]}", fuenteNormal));
            colCreditoVentas.BorderWidth = 1;
            colCreditoVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoVentas.Padding = 3;

            PdfPCell colCreditoAnticipos = new PdfPCell(new Phrase("", fuenteNormal));
            colCreditoAnticipos.BorderWidth = 1;
            colCreditoAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoAnticipos.Padding = 3;

            PdfPCell colCreditoDinero = new PdfPCell(new Phrase("", fuenteNormal));
            colCreditoDinero.BorderWidth = 1;
            colCreditoDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoDinero.Padding = 3;

            PdfPCell colCreditoTotal = new PdfPCell(new Phrase($"Crédito {cantidades[21]}", fuenteNormal));
            colCreditoTotal.BorderWidth = 1;
            colCreditoTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoTotal.Padding = 3;

            tabla.AddCell(colCreditoVentas);
            tabla.AddCell(colCreditoAnticipos);
            tabla.AddCell(colCreditoDinero);
            tabla.AddCell(colCreditoTotal);

            // Linea de ANTICIPOS
            PdfPCell colAnticiposVentas = new PdfPCell(new Phrase($"Anticipos utilizados al corte {cantidades[22]}", fuenteNormal));
            colAnticiposVentas.BorderWidth = 1;
            colAnticiposVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposVentas.Padding = 3;

            PdfPCell colAnticiposUtilizados = new PdfPCell(new Phrase("", fuenteNormal));
            colAnticiposUtilizados.BorderWidth = 1;
            colAnticiposUtilizados.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposUtilizados.Padding = 3;

            PdfPCell colAnticiposDinero = new PdfPCell(new Phrase("", fuenteNormal));
            colAnticiposDinero.BorderWidth = 1;
            colAnticiposDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposDinero.Padding = 3;

            PdfPCell colAnticiposTotal = new PdfPCell(new Phrase($"Anticipos utilizados al corte {cantidades[23]}", fuenteNormal));
            colAnticiposTotal.BorderWidth = 1;
            colAnticiposTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticiposTotal.Padding = 3;

            tabla.AddCell(colAnticiposVentas);
            tabla.AddCell(colAnticiposUtilizados);
            tabla.AddCell(colAnticiposDinero);
            tabla.AddCell(colAnticiposTotal);


            // Linea de SUBTOTAL
            PdfPCell colSubVentas = new PdfPCell(new Phrase("", fuenteNormal));
            colSubVentas.BorderWidth = 1;
            colSubVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubVentas.Padding = 3;

            PdfPCell colSubAnticipos = new PdfPCell(new Phrase("", fuenteNormal));
            colSubAnticipos.BorderWidth = 1;
            colSubAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubAnticipos.Padding = 3;

            PdfPCell colSubDinero = new PdfPCell(new Phrase("", fuenteNormal));
            colSubDinero.BorderWidth = 1;
            colSubDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubDinero.Padding = 3;

            PdfPCell colSubTotal = new PdfPCell(new Phrase($"Subtotal en caja {cantidades[24]}", fuenteNormal));
            colSubTotal.BorderWidth = 1;
            colSubTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubTotal.Padding = 3;

            tabla.AddCell(colSubVentas);
            tabla.AddCell(colSubAnticipos);
            tabla.AddCell(colSubDinero);
            tabla.AddCell(colSubTotal);

            // Linea de RETIRADO
            PdfPCell colRetiradoVentas = new PdfPCell(new Phrase("", fuenteNormal));
            colRetiradoVentas.BorderWidth = 1;
            colRetiradoVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetiradoVentas.Padding = 3;

            PdfPCell colRetiradoAnticipos = new PdfPCell(new Phrase("", fuenteNormal));
            colRetiradoAnticipos.BorderWidth = 1;
            colRetiradoAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetiradoAnticipos.Padding = 3;

            PdfPCell colRetiradoDinero = new PdfPCell(new Phrase("", fuenteNormal));
            colRetiradoDinero.BorderWidth = 1;
            colRetiradoDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetiradoDinero.Padding = 3;

            PdfPCell colRetiradoTotal = new PdfPCell(new Phrase($"Dinero retirado {cantidades[25]}", fuenteNormal));
            colRetiradoTotal.BorderWidth = 1;
            colRetiradoTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetiradoTotal.Padding = 3;

            tabla.AddCell(colRetiradoVentas);
            tabla.AddCell(colRetiradoAnticipos);
            tabla.AddCell(colRetiradoDinero);
            tabla.AddCell(colRetiradoTotal);

            // Linea de TOTALES
            PdfPCell colTotalVentas = new PdfPCell(new Phrase($"Total de Ventas {cantidades[26]}", fuenteNormal));
            colTotalVentas.BorderWidth = 1;
            colTotalVentas.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalVentas.Padding = 5;

            PdfPCell colTotalAnticipos = new PdfPCell(new Phrase($"Total Anticipos {cantidades[27]}", fuenteNormal));
            colTotalAnticipos.BorderWidth = 1;
            colTotalAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalAnticipos.Padding = 5;

            PdfPCell colTotalDinero = new PdfPCell(new Phrase($"Total Agregado {cantidades[28]}", fuenteNormal));
            colTotalDinero.BorderWidth = 1;
            colTotalDinero.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalDinero.Padding = 5;

            PdfPCell colTotalFinal = new PdfPCell(new Phrase($"Total en Caja {cantidades[29]}", fuenteNormal));
            colTotalFinal.BorderWidth = 1;
            colTotalFinal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotalFinal.Padding = 5;

            tabla.AddCell(colTotalVentas);
            tabla.AddCell(colTotalAnticipos);
            tabla.AddCell(colTotalDinero);
            tabla.AddCell(colTotalFinal);

            /*PdfPCell colSeparador = new PdfPCell(new Phrase(Chunk.NEWLINE));
            colSeparador.Colspan = 10;
            colSeparador.BorderWidth = 0;*/

            #endregion
            //===========================================
            //===    FIN  TABLAS DE CORTE DE CAJA     ===
            //===========================================

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));


            reporte.Add(titulo);
            reporte.Add(subTitulo);
            reporte.Add(tabla);
            //reporte.Add(linea);

            reporte.AddTitle("Reporte Corte de Caja");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }
    }
}
