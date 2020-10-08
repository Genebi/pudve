using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using MySql.Data.MySqlClient;
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
    public partial class ReporteDineroAgregado : Form
    {
        DateTime fecha = Convert.ToDateTime("0001-01-01 00:00:00");
        public ReporteDineroAgregado(DateTime fecha)
        {
            InitializeComponent();

            this.fecha = fecha;
        }

        private void ReporteDineroAgregado_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }

        private void CargarDatos()
        {
            MySqlConnection sql_con;
            MySqlCommand sql_cmd;
            MySqlDataReader dr;

            var fechaBusqueda = fecha.ToString("yyyy-MM-dd HH:mm:ss");
            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                sql_con = new MySqlConnection($"datasource={servidor};port=6666;username=root;password=;database=pudve;");
            }
            else
            {
                sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }

            sql_con.Open();
            sql_cmd = new MySqlCommand($"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'deposito' AND FechaOperacion > '{fechaBusqueda}'", sql_con);
            dr = sql_cmd.ExecuteReader();

            DGVDepositos.Rows.Clear();

            while (dr.Read())
            {
                int rowId = DGVDepositos.Rows.Add();

                DataGridViewRow row = DGVDepositos.Rows[rowId];

                row.Cells["Empleado"].Value = "ADMIN";
                row.Cells["Efectivo"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Efectivo"))).ToString("0.00");
                row.Cells["Tarjeta"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Tarjeta"))).ToString("0.00");
                row.Cells["Vales"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Vales"))).ToString("0.00");
                row.Cells["Cheque"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Cheque"))).ToString("0.00");
                row.Cells["Trans"].Value = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Transferencia"))).ToString("0.00");
                row.Cells["Fecha"].Value = Convert.ToDateTime(dr.GetValue(dr.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
            }

            dr.Close();
            sql_con.Close();
        }

        private void ReporteDineroAgregado_Shown(object sender, EventArgs e)
        {
            if (DGVDepositos.Rows.Count == 0)
            {
                var resultado = MessageBox.Show("No existen nuevos depósitos", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (resultado == DialogResult.OK)
                {
                    Dispose();
                }
            }
        }

        private void btnImprimirReporte_Click(object sender, EventArgs e)
        {
            if (!Utilidades.AdobeReaderInstalado())
            {
                Utilidades.MensajeAdobeReader();
                return;
            }

            if (DGVDepositos.RowCount > 0)
            {
                //MessageBox.Show("Fecha: " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss"));

                // Varaibles para los Totales
                float totalEfectivo = 0, 
                        totalTarjeta = 0, 
                        totalVales = 0, 
                        totalCheque = 0, 
                        totalTransferencia = 0;
                
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
                    rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Caja\reporte_Dinero_Agregado_" + DateTime.Now.ToString("ddddddMMMMyyyyHHmmss") + ".pdf";
                }
                else
                {
                    rutaArchivo = @"C:\Archivos PUDVE\Reportes\Caja\reporte_Dinero_Agregado_" + DateTime.Now.ToString("ddddddMMMMyyyyHHmmss") + ".pdf";
                }

                Document reporte = new Document(PageSize.A3);
                PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

                reporte.Open();

                Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
                Paragraph subTitulo = new Paragraph("DINERO AGREGADO\nFecha: " + DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss") + "\n\n\n", fuenteNormal);

                titulo.Alignment = Element.ALIGN_CENTER;
                subTitulo.Alignment = Element.ALIGN_CENTER;

                reporte.Add(titulo);
                reporte.Add(subTitulo);

                //=====================================
                //===    TABLA DE DINERO AGREGADO   ===
                //=====================================
                #region Tabla de Dinero Agregado
                float[] anchoColumnas = new float[] { 100f, 100f, 100f, 100f, 100f, 100f, 100f };

                Paragraph tituloDineroAgregado = new Paragraph("HISTORIAL DE DINERO AGREGADO\n\n", fuenteGrande);
                tituloDineroAgregado.Alignment = Element.ALIGN_CENTER;

                reporte.Add(tituloDineroAgregado);

                // Linea serapadora
                Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

                reporte.Add(linea);

                //PdfPCell colSeparador = new PdfPCell(new Phrase(Chunk.NEWLINE));
                //colSeparador.Colspan = 10;
                //colSeparador.BorderWidth = 0;

                //reporte.Add(colSeparador);

                PdfPTable tablaDineroAgregado = new PdfPTable(7);
                tablaDineroAgregado.WidthPercentage = 100;
                tablaDineroAgregado.SetWidths(anchoColumnas);

                PdfPCell colEmpleado = new PdfPCell(new Phrase("EMPLEADO", fuenteNegrita));
                colEmpleado.BorderWidth = 0;
                colEmpleado.HorizontalAlignment = Element.ALIGN_CENTER;
                colEmpleado.Padding = 3;

                PdfPCell colDepositoEfectivo = new PdfPCell(new Phrase("EFECTIVO", fuenteNegrita));
                colDepositoEfectivo.BorderWidth = 0;
                colDepositoEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoEfectivo.Padding = 3;

                PdfPCell colDepositoTarjeta = new PdfPCell(new Phrase("TARJETA", fuenteNegrita));
                colDepositoTarjeta.BorderWidth = 0;
                colDepositoTarjeta.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoTarjeta.Padding = 3;

                PdfPCell colDepositoVales = new PdfPCell(new Phrase("VALES", fuenteNegrita));
                colDepositoVales.BorderWidth = 0;
                colDepositoVales.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoVales.Padding = 3;

                PdfPCell colDepositoCheque = new PdfPCell(new Phrase("CHEQUE", fuenteNegrita));
                colDepositoCheque.BorderWidth = 0;
                colDepositoCheque.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoCheque.Padding = 3;

                PdfPCell colDepositoTrans = new PdfPCell(new Phrase("TRANSFERENCIA", fuenteNegrita));
                colDepositoTrans.BorderWidth = 0;
                colDepositoTrans.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoTrans.Padding = 3;

                PdfPCell colDepositoFecha = new PdfPCell(new Phrase("FECHA", fuenteNegrita));
                colDepositoFecha.BorderWidth = 0;
                colDepositoFecha.HorizontalAlignment = Element.ALIGN_CENTER;
                colDepositoFecha.Padding = 3;

                tablaDineroAgregado.AddCell(colEmpleado);
                tablaDineroAgregado.AddCell(colDepositoEfectivo);
                tablaDineroAgregado.AddCell(colDepositoTarjeta);
                tablaDineroAgregado.AddCell(colDepositoVales);
                tablaDineroAgregado.AddCell(colDepositoCheque);
                tablaDineroAgregado.AddCell(colDepositoTrans);
                tablaDineroAgregado.AddCell(colDepositoFecha);

                foreach (DataGridViewRow row in DGVDepositos.Rows)
                {
                    string  Empleado = string.Empty,
                            Efectivo = string.Empty,
                            Tarjeta = string.Empty,
                            Vales = string.Empty,
                            Cheque = string.Empty,
                            Transferencia = string.Empty,
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

                    Fecha = row.Cells["Fecha"].Value.ToString();

                    PdfPCell colEmpleadoTmp = new PdfPCell(new Phrase(Empleado, fuenteNormal));
                    colEmpleadoTmp.BorderWidth = 0;
                    colEmpleadoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoEfectivoTmp = new PdfPCell(new Phrase("$ " + Efectivo, fuenteNormal));
                    colDepositoEfectivoTmp.BorderWidth = 0;
                    colDepositoEfectivoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoTarjetaTmp = new PdfPCell(new Phrase("$ " + Tarjeta, fuenteNormal));
                    colDepositoTarjetaTmp.BorderWidth = 0;
                    colDepositoTarjetaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoValesTmp = new PdfPCell(new Phrase("$ " + Vales, fuenteNormal));
                    colDepositoValesTmp.BorderWidth = 0;
                    colDepositoValesTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoChequeTmp = new PdfPCell(new Phrase("$ " + Cheque, fuenteNormal));
                    colDepositoChequeTmp.BorderWidth = 0;
                    colDepositoChequeTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoTransTmp = new PdfPCell(new Phrase("$ " + Transferencia, fuenteNormal));
                    colDepositoTransTmp.BorderWidth = 0;
                    colDepositoTransTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colDepositoFechaTmp = new PdfPCell(new Phrase(Fecha, fuenteNormal));
                    colDepositoFechaTmp.BorderWidth = 0;
                    colDepositoFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    tablaDineroAgregado.AddCell(colEmpleadoTmp);
                    tablaDineroAgregado.AddCell(colDepositoEfectivoTmp);
                    tablaDineroAgregado.AddCell(colDepositoTarjetaTmp);
                    tablaDineroAgregado.AddCell(colDepositoValesTmp);
                    tablaDineroAgregado.AddCell(colDepositoChequeTmp);
                    tablaDineroAgregado.AddCell(colDepositoTransTmp);
                    tablaDineroAgregado.AddCell(colDepositoFechaTmp);
                }
                reporte.Add(tablaDineroAgregado);
                reporte.Add(linea);

                PdfPTable tablaTotalesDineroAgregado = new PdfPTable(7);
                tablaTotalesDineroAgregado.WidthPercentage = 100;
                tablaTotalesDineroAgregado.SetWidths(anchoColumnas);

                // Linea de TOTALES
                PdfPCell colEmpleadoTotal = new PdfPCell(new Phrase($"Total Dinero Agregado", fuenteTotales));
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

                PdfPCell colFechaTotal = new PdfPCell(new Phrase("", fuenteTotales));
                colFechaTotal.BorderWidth = 0;
                colFechaTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                colFechaTotal.Padding = 3;
                colFechaTotal.BackgroundColor = new BaseColor(Color.Red);

                tablaTotalesDineroAgregado.AddCell(colEmpleadoTotal);
                tablaTotalesDineroAgregado.AddCell(colEfectivoTotal);
                tablaTotalesDineroAgregado.AddCell(colTarjetaTotal);
                tablaTotalesDineroAgregado.AddCell(colValesTotal);
                tablaTotalesDineroAgregado.AddCell(colChequeTotal);
                tablaTotalesDineroAgregado.AddCell(colTransaccionTotal);
                tablaTotalesDineroAgregado.AddCell(colFechaTotal);

                reporte.Add(tablaTotalesDineroAgregado);

                #endregion Tabla de Dinero Agregado
                //=====================================
                //=== FIN TABLA DE DINERO AGREGADO  ===
                //=====================================
                reporte.AddTitle("Reporte Dinero Agregado");
                reporte.AddAuthor("PUDVE");
                reporte.Close();
                writer.Close();

                VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
                vr.ShowDialog();
            }
        }
    }
}
