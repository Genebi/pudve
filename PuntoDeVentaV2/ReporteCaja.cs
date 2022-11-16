using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text.pdf.draw;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class ReporteCaja : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        private string operacion = string.Empty;

        public ReporteCaja()
        {
            InitializeComponent();
        }

        private void ReporteCaja_Load(object sender, EventArgs e)
        {
            Dictionary<int, int> years = new Dictionary<int, int>();
            years.Add(2019, 2019);
            years.Add(2020, 2020);
            years.Add(2021, 2021);
            years.Add(2022, 2022);
            years.Add(2023, 2023);

            // Seleccionar horas por defecto
            dtpHoraInicio.Text = "00:00:00";
            dtpHoraFin.Text = "23:59:59";

            // Hacer que se puedan marcar los checkbox con un solo click
            clbConceptos.CheckOnClick = true;
        }

        private void rbDineroAgregado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDineroAgregado.Checked || rbDineroRetirado.Checked)
            {
                rbHabilitados.Enabled = true;
                rbDeshabilitados.Enabled = true;

                ObtenerOperacion();
            }
        }

        private void rbDineroRetirado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDineroAgregado.Checked || rbDineroRetirado.Checked)
            {
                rbHabilitados.Enabled = true;
                rbDeshabilitados.Enabled = true;

                ObtenerOperacion();
            }
        }

        private void rbHabilitados_CheckedChanged(object sender, EventArgs e)
        {
            var conceptos = mb.ObtenerConceptosDinamicos(origen: "CAJA", reporte: true);

            if (conceptos.Count > 0)
            {
                clbConceptos.Items.Clear();

                foreach (var concepto in conceptos)
                {
                    AgregarConcepto(concepto);
                }

                if (!cbTodos.Visible) cbTodos.Visible = true;
                if (!clbConceptos.Visible) clbConceptos.Visible = true;
                if (!panelFechaHora.Visible) panelFechaHora.Visible = true;
            }
        }

        private void rbDeshabilitados_CheckedChanged(object sender, EventArgs e)
        {
            var conceptos = mb.ObtenerConceptosDinamicos(0, "CAJA", true);

            if (conceptos.Count > 0)
            {
                clbConceptos.Items.Clear();

                foreach (var concepto in conceptos)
                {
                    AgregarConcepto(concepto);
                }

                if (!cbTodos.Visible) cbTodos.Visible = true;
                if (!clbConceptos.Visible) clbConceptos.Visible = true;
                if (!panelFechaHora.Visible) panelFechaHora.Visible = true;
            }
        }

        private void AgregarConcepto(KeyValuePair<int, string> concepto)
        {
            ListBoxItem cb = new ListBoxItem();
            cb.Text = concepto.Value;
            cb.Tag = concepto.Key;

            clbConceptos.Items.Add(cb);
        }

        private string ObtenerOperacion()
        {
            operacion = rbDineroAgregado.Checked ? "deposito" : "retiro";

            return operacion;
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            var seleccionados = clbConceptos.CheckedItems;

            if (seleccionados.Count > 0)
            {
                string fechaInicio = dpFechaInicial.Text;
                string fechaFin = dpFechaFinal.Text;

                string primeraHora = dtpHoraInicio.Text;
                string segundaHora = dtpHoraFin.Text;

                string primeraFecha = $"{fechaInicio} {primeraHora}";
                string segundaFecha = $"{fechaFin} {segundaHora}";


                if (!Utilidades.AdobeReaderInstalado())
                {
                    Utilidades.MensajeAdobeReader();
                    return;
                }


                string queryConceptos = "IN (";

                foreach (ListBoxItem concepto in seleccionados)
                {
                    queryConceptos += $"'{concepto.Text}',";
                }

                queryConceptos = queryConceptos.Remove(queryConceptos.Length - 1);
                queryConceptos += ")";

                //string query = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = '{ObtenerOperacion()}' AND Concepto {queryConceptos} AND FechaOperacion BETWEEN '{primeraFecha}' AND '{segundaFecha}'";
                string query = $"SELECT SUM(CA.Cantidad) AS Cantidad, SUM(CA.Efectivo) AS Efectivo, SUM(CA.Tarjeta) AS Tarjeta, SUM(CA.Vales) AS Vales, SUM(CA.Cheque) AS Cheque, SUM(CA.Transferencia) AS Transferencia, SUM(CA.Credito) AS Credito, SUM(CA.Anticipo) AS Anticipo, CA.Operacion AS Operacion, CA.Concepto AS Concepto, CA.FechaOperacion AS FechaOperacion FROM Caja CA INNER JOIN ConceptosDinamicos CD ON (CA.IDUsuario = CD.IDUsuario AND CA.Concepto = CD.Concepto) WHERE CA.IDUsuario = {FormPrincipal.userID} AND CA.Operacion = '{ObtenerOperacion()}' AND CA.Concepto {queryConceptos} AND CA.FechaOperacion BETWEEN '{primeraFecha}' AND '{segundaFecha}' GROUP BY CD.ID";
                DataTable datos = cn.CargarDatos(query);

                GenerarReporte(datos, new string[] { primeraFecha, segundaFecha });
            }
        }

        public void GenerarReporte(DataTable tablaResult, string[] fechas)
        {
            Consultas cs = new Consultas();

            // Fechas
            var primeraFecha = fechas[0];
            var segundaFecha = fechas[1];

            var mostrarClave = FormPrincipal.clave;

            //Datos del usuario
            var datos = FormPrincipal.datosUsuario;

            //Fuentes y Colores
            var colorFuenteNegrita = new BaseColor(System.Drawing.Color.Black);
            var colorFuenteBlanca = new BaseColor(System.Drawing.Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, colorFuenteNegrita);

            var numRow = 0;
            string numAperturaCaja = string.Empty;

            //Ruta donde se creara el archivo PDF
            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;
            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Caja\reporte_caja_usuario_{FormPrincipal.userID}_fecha_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf";
            }
            else
            {
                rutaArchivo = $@"C:\Archivos PUDVE\Reportes\Caja\reporte_caja_usuario_{FormPrincipal.userID}_fecha_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf";
            }

            var fechaHoy = DateTime.Now;
            //rutaArchivo = @"C:\Archivos PUDVE\Reportes\caja.pdf";

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph("REPORTE DE CAJA", fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            Paragraph Empleado = new Paragraph("");

            Paragraph NumeroFolio = new Paragraph("");

            //string UsuarioActivo = string.Empty;


            var UsuarioActivo = cs.validarEmpleado(FormPrincipal.userNickName, true);
            var obtenerUsuarioPrincipal = cs.validarEmpleadoPorID();


            Usuario = new Paragraph($"USUARIO: ADMIN ({obtenerUsuarioPrincipal})", fuenteNegrita);

            if (!string.IsNullOrEmpty(UsuarioActivo))
            {
                Empleado = new Paragraph($"EMPLEADO: {UsuarioActivo}", fuenteNegrita);
            }

            //NumeroFolio = new Paragraph("No. Folio: " + numFolio, fuenteNormal);

            Paragraph subTitulo = new Paragraph($"{ObtenerOperacion().ToUpper()}" + $"\n\nCONSULTA REALIZADA\nDEL {primeraFecha} AL {segundaFecha}\nFecha de creación: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;

            if (!string.IsNullOrEmpty(UsuarioActivo))
            {
                Empleado.Alignment = Element.ALIGN_CENTER;
            }

            NumeroFolio.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;


            float[] anchoColumnas = new float[] { 20f, 40f, 40f, 100f, 60f, 40f, 40f, 40f, 40f, 50f, 40f, 40f};

            //Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(System.Drawing.Color.Black), Element.ALIGN_LEFT, 1)));


            PdfPTable tablaInventario = new PdfPTable(12);
            tablaInventario.WidthPercentage = 100;
            tablaInventario.SetWidths(anchoColumnas);

            PdfPCell colNum = new PdfPCell(new Phrase("NO:", fuenteNegrita));
            colNum.BorderWidth = 1;
            colNum.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);
            colNum.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colOperacion = new PdfPCell(new Phrase("OPERACIÓN", fuenteNegrita));
            colOperacion.BorderWidth = 1;
            colOperacion.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);
            colOperacion.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colCantidad = new PdfPCell(new Phrase("CANTIDAD", fuenteTotales));
            colCantidad.BorderWidth = 1;
            colCantidad.HorizontalAlignment = Element.ALIGN_CENTER;
            colCantidad.Padding = 3;
            colCantidad.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colConcepto = new PdfPCell(new Phrase("CONCEPTO", fuenteTotales));
            colConcepto.BorderWidth = 1;
            colConcepto.HorizontalAlignment = Element.ALIGN_CENTER;
            colConcepto.Padding = 3;
            colConcepto.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colFecha = new PdfPCell(new Phrase("FECHA", fuenteTotales));
            colFecha.BorderWidth = 1;
            colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            colFecha.Padding = 3;
            colFecha.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colEfectivo = new PdfPCell(new Phrase("EFECTIVO", fuenteTotales));
            colEfectivo.BorderWidth = 1;
            colEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivo.Padding = 3;
            colEfectivo.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colTarjeta = new PdfPCell(new Phrase("TARJETA", fuenteTotales));
            colTarjeta.BorderWidth = 1;
            colTarjeta.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjeta.Padding = 3;
            colTarjeta.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colVales = new PdfPCell(new Phrase("VALES", fuenteTotales));
            colVales.BorderWidth = 1;
            colVales.HorizontalAlignment = Element.ALIGN_CENTER;
            colVales.Padding = 3;
            colVales.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colCheque = new PdfPCell(new Phrase("CHEQUE", fuenteTotales));
            colCheque.BorderWidth = 1;
            colCheque.HorizontalAlignment = Element.ALIGN_CENTER;
            colCheque.Padding = 3;
            colCheque.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colTransferencia = new PdfPCell(new Phrase("TRANSFERENCIA", fuenteTotales));
            colTransferencia.BorderWidth = 1;
            colTransferencia.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransferencia.Padding = 3;
            colTransferencia.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colCredito = new PdfPCell(new Phrase("CREDITO", fuenteTotales));
            colCredito.BorderWidth = 1;
            colCredito.HorizontalAlignment = Element.ALIGN_CENTER;
            colCredito.Padding = 3;
            colCredito.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colAnticipo = new PdfPCell(new Phrase("ANTICIPO", fuenteTotales));
            colAnticipo.BorderWidth = 1;
            colAnticipo.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticipo.Padding = 3;
            colAnticipo.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            tablaInventario.AddCell(colNum);
            tablaInventario.AddCell(colOperacion);
            tablaInventario.AddCell(colCantidad);
            tablaInventario.AddCell(colConcepto);
            tablaInventario.AddCell(colFecha);
            tablaInventario.AddCell(colEfectivo);
            tablaInventario.AddCell(colTarjeta);
            tablaInventario.AddCell(colVales);
            tablaInventario.AddCell(colCheque);
            tablaInventario.AddCell(colTransferencia);
            tablaInventario.AddCell(colCredito);
            tablaInventario.AddCell(colAnticipo);

            decimal totalEfectivo = 0;
            decimal totalTarjeta = 0;
            decimal totalVales = 0;
            decimal totalCheque = 0;
            decimal totalTransferencia = 0;
            decimal totalCredito = 0;
            decimal totalAnticipo = 0;

            foreach (DataRow iterador in tablaResult.Rows)
            {
                numRow += 1;

                PdfPCell colNumFilatemp = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNumFilatemp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                colNumFilatemp.BorderWidth = 1;
                colNumFilatemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colOperacionTemp = new PdfPCell(new Phrase(iterador["Operacion"].ToString().ToUpper(), fuenteNormal));
                colOperacionTemp.BorderWidth = 1;
                colOperacionTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCantidadTemp = new PdfPCell(new Phrase("$" + iterador["Cantidad"].ToString(), fuenteNormal));
                colCantidadTemp.BorderWidth = 1;
                colCantidadTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colConceptoTemp = new PdfPCell(new Phrase(iterador["Concepto"].ToString(), fuenteNormal));
                colConceptoTemp.BorderWidth = 1;
                colConceptoTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTemp = new PdfPCell(new Phrase(iterador["FechaOperacion"].ToString(), fuenteNormal));
                colFechaTemp.BorderWidth = 1;
                colFechaTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colEfectivoTemp = new PdfPCell(new Phrase("$" + iterador["Efectivo"].ToString(), fuenteNormal));
                colEfectivoTemp.BorderWidth = 1;
                colEfectivoTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colTarjetaTemp = new PdfPCell(new Phrase("$" + iterador["Tarjeta"].ToString(), fuenteNormal));
                colTarjetaTemp.BorderWidth = 1;
                colTarjetaTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colValesTemp = new PdfPCell(new Phrase("$" + iterador["Vales"].ToString(), fuenteNormal));
                colValesTemp.BorderWidth = 1;
                colValesTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colChequeTemp = new PdfPCell(new Phrase("$" + iterador["Cheque"].ToString(), fuenteNormal));
                colChequeTemp.BorderWidth = 1;
                colChequeTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colTransferenciaTemp = new PdfPCell(new Phrase("$" + iterador["Transferencia"].ToString(), fuenteNormal));
                colTransferenciaTemp.BorderWidth = 1;
                colTransferenciaTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCreditoTemp = new PdfPCell(new Phrase("$" + iterador["Credito"].ToString(), fuenteNormal));
                colCreditoTemp.BorderWidth = 1;
                colCreditoTemp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colAnticipoTemp = new PdfPCell(new Phrase("$" + iterador["Anticipo"].ToString(), fuenteNormal));
                colAnticipoTemp.BorderWidth = 1;
                colAnticipoTemp.HorizontalAlignment = Element.ALIGN_CENTER;


                totalEfectivo += Convert.ToDecimal(iterador["Efectivo"]);
                totalTarjeta += Convert.ToDecimal(iterador["Tarjeta"]);
                totalVales += Convert.ToDecimal(iterador["Vales"]);
                totalCheque += Convert.ToDecimal(iterador["Cheque"]);
                totalTransferencia += Convert.ToDecimal(iterador["Transferencia"]);
                totalCredito += Convert.ToDecimal(iterador["Credito"]);
                totalAnticipo += Convert.ToDecimal(iterador["Anticipo"]);


                tablaInventario.AddCell(colNumFilatemp);
                tablaInventario.AddCell(colOperacionTemp);
                tablaInventario.AddCell(colCantidadTemp);
                tablaInventario.AddCell(colConceptoTemp);
                tablaInventario.AddCell(colFechaTemp);
                tablaInventario.AddCell(colEfectivoTemp);
                tablaInventario.AddCell(colTarjetaTemp);
                tablaInventario.AddCell(colValesTemp);
                tablaInventario.AddCell(colChequeTemp);
                tablaInventario.AddCell(colTransferenciaTemp);
                tablaInventario.AddCell(colCreditoTemp);
                tablaInventario.AddCell(colAnticipoTemp);

            }

            //Columna para total de dinero
            PdfPCell colNumFilatempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colNumFilatempTotal.BorderWidth = 0;
            colNumFilatempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colOperacionTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colOperacionTempTotal.BorderWidth = 0;
            colOperacionTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colCantidadTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colCantidadTempTotal.BorderWidth = 0;
            colCantidadTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colConceptoTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colConceptoTempTotal.BorderWidth = 0;
            colConceptoTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colFechaTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            colFechaTempTotal.BorderWidth = 0;
            colFechaTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colEfectivoTempTotal = new PdfPCell(new Phrase("$" + totalEfectivo.ToString("0.00"), fuenteNormal));
            colEfectivoTempTotal.BorderWidth = 0;
            colEfectivoTempTotal.BorderWidthBottom = 1;
            colEfectivoTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colEfectivoTempTotal.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colTarjetaTempTotal = new PdfPCell(new Phrase("$" + totalTarjeta.ToString("0.00"), fuenteNormal));
            colTarjetaTempTotal.BorderWidth = 0;
            colTarjetaTempTotal.BorderWidthBottom = 1;
            colTarjetaTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTarjetaTempTotal.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colValesTempTotal = new PdfPCell(new Phrase("$" + totalVales.ToString("0.00"), fuenteNormal));
            colValesTempTotal.BorderWidth = 0;
            colValesTempTotal.BorderWidthBottom = 1;
            colValesTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colValesTempTotal.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colChequeTempTotal = new PdfPCell(new Phrase("$" + totalCheque.ToString("0.00"), fuenteNormal));
            colChequeTempTotal.BorderWidth = 0;
            colChequeTempTotal.BorderWidthBottom = 1;
            colChequeTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colChequeTempTotal.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colTransferenciaTempTotal = new PdfPCell(new Phrase("$" + totalTransferencia.ToString("0.00"), fuenteNormal));
            colTransferenciaTempTotal.BorderWidth = 0;
            colTransferenciaTempTotal.BorderWidthBottom = 1;
            colTransferenciaTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTransferenciaTempTotal.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colCreditoTempTotal = new PdfPCell(new Phrase("$" + totalCredito.ToString("0.00"), fuenteNormal));
            colCreditoTempTotal.BorderWidth = 0;
            colCreditoTempTotal.BorderWidthBottom = 1;
            colCreditoTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colCreditoTempTotal.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);

            PdfPCell colAnticipoTempTotal = new PdfPCell(new Phrase("$" + totalAnticipo.ToString("0.00"), fuenteNormal));
            colAnticipoTempTotal.BorderWidth = 0;
            colAnticipoTempTotal.BorderWidthBottom = 1;
            colAnticipoTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticipoTempTotal.BackgroundColor = new BaseColor(System.Drawing.Color.SkyBlue);


            tablaInventario.AddCell(colNumFilatempTotal);
            tablaInventario.AddCell(colOperacionTempTotal);
            tablaInventario.AddCell(colCantidadTempTotal);
            tablaInventario.AddCell(colConceptoTempTotal);
            tablaInventario.AddCell(colFechaTempTotal);
            tablaInventario.AddCell(colEfectivoTempTotal);
            tablaInventario.AddCell(colTarjetaTempTotal);
            tablaInventario.AddCell(colValesTempTotal);
            tablaInventario.AddCell(colChequeTempTotal);
            tablaInventario.AddCell(colTransferenciaTempTotal);
            tablaInventario.AddCell(colCreditoTempTotal);
            tablaInventario.AddCell(colAnticipoTempTotal);

            reporte.Add(titulo);
            reporte.Add(Usuario);

            if (!string.IsNullOrEmpty(UsuarioActivo))
            {
                reporte.Add(Empleado);
            }
            reporte.Add(NumeroFolio);
            reporte.Add(subTitulo);
            reporte.Add(tablaInventario);


            reporte.AddTitle("Reporte Caja");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }

        private void cbTodos_CheckedChanged(object sender, EventArgs e)
        {
            int cantidadCheckbox = clbConceptos.Items.Count;

            if (cbTodos.Checked)
            {
                for (int i = 0; i < cantidadCheckbox; i++)
                {
                    clbConceptos.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < cantidadCheckbox; i++)
                {
                    clbConceptos.SetItemChecked(i, false);
                }
            }
        }
    }

    public class ListBoxItem
    {
        public string Text { get; set; }
        public object Tag { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
}
