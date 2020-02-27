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
    public partial class ReporteDineroRetirado : Form
    {
        DateTime fecha = Convert.ToDateTime("0001-01-01 00:00:00");
        public ReporteDineroRetirado(DateTime fecha)
        {
            InitializeComponent();

            this.fecha = fecha;
        }

        private void ReporteDineroRetirado_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            var fechaBusqueda = fecha.ToString("yyyy-MM-dd HH:mm:ss");
            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                sql_con = new SQLiteConnection("Data source=//" + servidor + @"\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }
            else
            {
                sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }

            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'retiro' AND FechaOperacion > '{fechaBusqueda}'", sql_con);
            dr = sql_cmd.ExecuteReader();

            DGVRetiros.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVRetiros.Rows.Add();

                DataGridViewRow row = DGVRetiros.Rows[rowId];

                row.Cells["Empleado"].Value = "ADMIN";
                row.Cells["Efectivo"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Efectivo"))).ToString("0.00");
                row.Cells["Tarjeta"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Tarjeta"))).ToString("0.00");
                row.Cells["Vales"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vales"))).ToString("0.00");
                row.Cells["Cheque"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cheque"))).ToString("0.00");
                row.Cells["Trans"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Transferencia"))).ToString("0.00");
                row.Cells["Credito"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Credito"))).ToString("0.00");
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
            }

            dr.Close();
            sql_con.Close();
        }

        private void ReporteDineroRetirado_Shown(object sender, EventArgs e)
        {
            if (DGVRetiros.Rows.Count == 0)
            {
                var resultado = MessageBox.Show("No existen retiros recientes", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (resultado == DialogResult.OK)
                {
                    Dispose();
                }
            }
        }

        private void btnImprimirReporte_Click(object sender, EventArgs e)
        {
            if (DGVRetiros.RowCount > 0)
            {
                // Variables para los Totales
                float totalEfectivo = 0,
                        totalTarjeta = 0,
                        totalVales = 0,
                        totalCheque = 0,
                        totalTransferencia = 0,
                        totalCredito = 0;

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
                var rutaArchivo = string.Empty;
                var servidor = Properties.Settings.Default.Hosting;

                if (!string.IsNullOrWhiteSpace(servidor))
                {
                    rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Caja\reporte_Dinero_Retirado_" + DateTime.Now.ToString("ddddddMMMMyyyyHHmmss") + ".pdf";
                }
                else
                {
                    rutaArchivo = @"C:\Archivos PUDVE\Reportes\Caja\reporte_Dinero_Retirado_" + DateTime.Now.ToString("ddddddMMMMyyyyHHmmss") + ".pdf";
                }

                Document reporte = new Document(PageSize.A3);
                PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

                reporte.Open();

                Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
                Paragraph subTitulo = new Paragraph("DINERO RETIRADO\nFecha: " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + "\n\n\n", fuenteNormal);

                titulo.Alignment = Element.ALIGN_CENTER;
                subTitulo.Alignment = Element.ALIGN_CENTER;

                reporte.Add(titulo);
                reporte.Add(subTitulo);

                //=====================================
                //===    TABLA DE DINERO RETIRADO   ===
                //=====================================
                #region Tabla de Dinero Retirado
                float[] anchoColumnas = new float[] { 100f, 100f, 100f, 100f, 100f, 100f, 100f, 100f };

                Paragraph tituloDineroRetirado = new Paragraph("HISTORIAL DE DINERO RETIRADO\n\n", fuenteGrande);
                tituloDineroRetirado.Alignment = Element.ALIGN_CENTER;

                reporte.Add(tituloDineroRetirado);

                // Linea serapadora
                Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

                reporte.Add(linea);

                PdfPTable tablaDineroRetirado = new PdfPTable(8);
                tablaDineroRetirado.WidthPercentage = 100;
                tablaDineroRetirado.SetWidths(anchoColumnas);

                PdfPCell colEmpleado = new PdfPCell(new Phrase("EMPLEADO", fuenteNegrita));
                colEmpleado.BorderWidth = 0;
                colEmpleado.HorizontalAlignment = Element.ALIGN_CENTER;
                colEmpleado.Padding = 3;

                PdfPCell colRetiroEfectivo = new PdfPCell(new Phrase("EFECTIVO", fuenteNegrita));
                colRetiroEfectivo.BorderWidth = 0;
                colRetiroEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;
                colRetiroEfectivo.Padding = 3;

                PdfPCell colRetiroTarjeta = new PdfPCell(new Phrase("TARJETA", fuenteNegrita));
                colRetiroTarjeta.BorderWidth = 0;
                colRetiroTarjeta.HorizontalAlignment = Element.ALIGN_CENTER;
                colRetiroTarjeta.Padding = 3;

                PdfPCell colRetiroVales = new PdfPCell(new Phrase("VALES", fuenteNegrita));
                colRetiroVales.BorderWidth = 0;
                colRetiroVales.HorizontalAlignment = Element.ALIGN_CENTER;
                colRetiroVales.Padding = 3;

                PdfPCell colRetiroCheque = new PdfPCell(new Phrase("CHEQUE", fuenteNegrita));
                colRetiroCheque.BorderWidth = 0;
                colRetiroCheque.HorizontalAlignment = Element.ALIGN_CENTER;
                colRetiroCheque.Padding = 3;

                PdfPCell colRetiroTrans = new PdfPCell(new Phrase("TRANSFERENCIA", fuenteNegrita));
                colRetiroTrans.BorderWidth = 0;
                colRetiroTrans.HorizontalAlignment = Element.ALIGN_CENTER;
                colRetiroTrans.Padding = 3;

                PdfPCell colRetiroCredito = new PdfPCell(new Phrase("CREDITO", fuenteNegrita));
                colRetiroCredito.BorderWidth = 0;
                colRetiroCredito.HorizontalAlignment = Element.ALIGN_CENTER;
                colRetiroCredito.Padding = 3;

                PdfPCell colRetiroFecha = new PdfPCell(new Phrase("FECHA", fuenteNegrita));
                colRetiroFecha.BorderWidth = 0;
                colRetiroFecha.HorizontalAlignment = Element.ALIGN_CENTER;
                colRetiroFecha.Padding = 3;

                tablaDineroRetirado.AddCell(colEmpleado);
                tablaDineroRetirado.AddCell(colRetiroEfectivo);
                tablaDineroRetirado.AddCell(colRetiroTarjeta);
                tablaDineroRetirado.AddCell(colRetiroVales);
                tablaDineroRetirado.AddCell(colRetiroCheque);
                tablaDineroRetirado.AddCell(colRetiroTrans);
                tablaDineroRetirado.AddCell(colRetiroCredito);
                tablaDineroRetirado.AddCell(colRetiroFecha);

                foreach (DataGridViewRow row in DGVRetiros.Rows)
                {
                    string Empleado = string.Empty,
                            Efectivo = string.Empty,
                            Tarjeta = string.Empty,
                            Vales = string.Empty,
                            Cheque = string.Empty,
                            Transferencia = string.Empty,
                            Credito = string.Empty,
                            Fecha = string.Empty;

                    Empleado = row.Cells["Empleado"].Value.ToString();

                    Efectivo = row.Cells["Efectivo"].Value.ToString();
                    if (!Efectivo.Equals(""))
                    {
                        totalEfectivo += (float)Convert.ToDouble(Efectivo);
                    }

                    Tarjeta = row.Cells["Tarjeta"].Value.ToString();
                    if (!Tarjeta.Equals(""))
                    {
                        totalTarjeta += (float)Convert.ToDouble(Tarjeta);
                    }

                    Vales = row.Cells["Vales"].Value.ToString();
                    if (!Vales.Equals(""))
                    {
                        totalVales += (float)Convert.ToDouble(Vales);
                    }

                    Cheque = row.Cells["Cheque"].Value.ToString();
                    if (!Cheque.Equals(""))
                    {
                        totalCheque += (float)Convert.ToDouble(Cheque);
                    }

                    Transferencia = row.Cells["Trans"].Value.ToString();
                    if (!Transferencia.Equals(""))
                    {
                        totalTransferencia += (float)Convert.ToDouble(Transferencia);
                    }

                    Credito = row.Cells["Credito"].Value.ToString();
                    if (!Credito.Equals(""))
                    {
                        totalCredito += (float)Convert.ToDouble(Credito);
                    }

                    Fecha = row.Cells["Fecha"].Value.ToString();

                    PdfPCell colEmpleadoTmp = new PdfPCell(new Phrase(Empleado, fuenteNormal));
                    colEmpleadoTmp.BorderWidth = 0;
                    colEmpleadoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colRetiroEfectivoTmp = new PdfPCell(new Phrase("$ " + Efectivo, fuenteNormal));
                    colRetiroEfectivoTmp.BorderWidth = 0;
                    colRetiroEfectivoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colRetiroTarjetaTmp = new PdfPCell(new Phrase("$ " + Tarjeta, fuenteNormal));
                    colRetiroTarjetaTmp.BorderWidth = 0;
                    colRetiroTarjetaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colRetiroValesTmp = new PdfPCell(new Phrase("$ " + Vales, fuenteNormal));
                    colRetiroValesTmp.BorderWidth = 0;
                    colRetiroValesTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colRetiroChequeTmp = new PdfPCell(new Phrase("$ " + Cheque, fuenteNormal));
                    colRetiroChequeTmp.BorderWidth = 0;
                    colRetiroChequeTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colRetiroTransTmp = new PdfPCell(new Phrase("$ " + Transferencia, fuenteNormal));
                    colRetiroTransTmp.BorderWidth = 0;
                    colRetiroTransTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colRetiroCreditoTmp = new PdfPCell(new Phrase("$ " + Credito, fuenteNormal));
                    colRetiroCreditoTmp.BorderWidth = 0;
                    colRetiroCreditoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colRetiroFechaTmp = new PdfPCell(new Phrase(Fecha, fuenteNormal));
                    colRetiroFechaTmp.BorderWidth = 0;
                    colRetiroFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    tablaDineroRetirado.AddCell(colEmpleadoTmp);
                    tablaDineroRetirado.AddCell(colRetiroEfectivoTmp);
                    tablaDineroRetirado.AddCell(colRetiroTarjetaTmp);
                    tablaDineroRetirado.AddCell(colRetiroValesTmp);
                    tablaDineroRetirado.AddCell(colRetiroChequeTmp);
                    tablaDineroRetirado.AddCell(colRetiroTransTmp);
                    tablaDineroRetirado.AddCell(colRetiroCreditoTmp);
                    tablaDineroRetirado.AddCell(colRetiroFechaTmp);
                }
                reporte.Add(tablaDineroRetirado);
                reporte.Add(linea);

                PdfPTable tablaTotalesDineroRetirado = new PdfPTable(8);
                tablaTotalesDineroRetirado.WidthPercentage = 100;
                tablaTotalesDineroRetirado.SetWidths(anchoColumnas);

                // Linea de TOTALES
                PdfPCell colEmpleadoTotal = new PdfPCell(new Phrase($"Total Dinero Retirado", fuenteTotales));
                colEmpleadoTotal.BorderWidth = 0;
                colEmpleadoTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                colEmpleadoTotal.Padding = 3;
                colEmpleadoTotal.BackgroundColor = new BaseColor(Color.Red);

                PdfPCell colEfectivoTotal = new PdfPCell(new Phrase("$ " + totalEfectivo.ToString("N2"), fuenteTotales));
                colEfectivoTotal.BorderWidth = 0;
                colEfectivoTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                colEfectivoTotal.Padding = 3;
                colEfectivoTotal.BackgroundColor = new BaseColor(Color.Red);

                PdfPCell colTarjetaTotal = new PdfPCell(new Phrase("$ " + totalTarjeta.ToString("N2"), fuenteTotales));
                colTarjetaTotal.BorderWidth = 0;
                colTarjetaTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                colTarjetaTotal.Padding = 3;
                colTarjetaTotal.BackgroundColor = new BaseColor(Color.Red);

                PdfPCell colValesTotal = new PdfPCell(new Phrase("$ " + totalVales.ToString("N2"), fuenteTotales));
                colValesTotal.BorderWidth = 0;
                colValesTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                colValesTotal.Padding = 3;
                colValesTotal.BackgroundColor = new BaseColor(Color.Red);

                PdfPCell colChequeTotal = new PdfPCell(new Phrase("$ " + totalCheque.ToString("N2"), fuenteTotales));
                colChequeTotal.BorderWidth = 0;
                colChequeTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                colChequeTotal.Padding = 3;
                colChequeTotal.BackgroundColor = new BaseColor(Color.Red);

                PdfPCell colTransaccionTotal = new PdfPCell(new Phrase("$ " + totalTransferencia.ToString("N2"), fuenteTotales));
                colTransaccionTotal.BorderWidth = 0;
                colTransaccionTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                colTransaccionTotal.Padding = 3;
                colTransaccionTotal.BackgroundColor = new BaseColor(Color.Red);

                PdfPCell colCreditoTotal = new PdfPCell(new Phrase("$ " + totalCredito.ToString("N2"), fuenteTotales));
                colCreditoTotal.BorderWidth = 0;
                colCreditoTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                colCreditoTotal.Padding = 3;
                colCreditoTotal.BackgroundColor = new BaseColor(Color.Red);

                PdfPCell colFechaTotal = new PdfPCell(new Phrase("", fuenteTotales));
                colFechaTotal.BorderWidth = 0;
                colFechaTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                colFechaTotal.Padding = 3;
                colFechaTotal.BackgroundColor = new BaseColor(Color.Red);

                tablaTotalesDineroRetirado.AddCell(colEmpleadoTotal);
                tablaTotalesDineroRetirado.AddCell(colEfectivoTotal);
                tablaTotalesDineroRetirado.AddCell(colTarjetaTotal);
                tablaTotalesDineroRetirado.AddCell(colValesTotal);
                tablaTotalesDineroRetirado.AddCell(colChequeTotal);
                tablaTotalesDineroRetirado.AddCell(colTransaccionTotal);
                tablaTotalesDineroRetirado.AddCell(colCreditoTotal);
                tablaTotalesDineroRetirado.AddCell(colFechaTotal);

                reporte.Add(tablaTotalesDineroRetirado);
                #endregion Tabla de Dinero Agregado
                //=====================================
                //=== FIN TABLA DE DINERO RETIRADO  ===
                //=====================================
                reporte.AddTitle("Reporte Dinero Retirado");
                reporte.AddAuthor("PUDVE");
                reporte.Close();
                writer.Close();

                VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
                vr.ShowDialog();
            }
        }
    }
}
