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
    public partial class BuscarReporteCajaPorFecha : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        System.Drawing.Image pdf = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");

        public BuscarReporteCajaPorFecha()
        {
            InitializeComponent();
        }

        private void BuscarReporteCajaPorFecha_Load(object sender, EventArgs e)
        {
            cargarDGVInicial();
            primerDatePicker.Value = DateTime.Today.AddDays(-7);
            segundoDatePicker.Value = DateTime.Now;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DGVReporteCaja.Rows.Clear();

            var datoBuscar = txtBuscador.Text.ToString().Replace("\r\n", string.Empty);
            var primerFecha = primerDatePicker.Value.ToString("yyyy/MM/dd");
            var segundaFecha = segundoDatePicker.Value.ToString("yyyy/MM/dd");

            var name = string.Empty; var fecha = string.Empty; var empleado = string.Empty; var idCorte = string.Empty; var idEmpleado = 0;
            var nombreUser = string.Empty;
            var querry = cn.CargarDatos(cs.BuscadorDeReportesCaja(datoBuscar, primerFecha, segundaFecha));

            if (!querry.Rows.Count.Equals(0))
            {
                foreach (DataRow iterar in querry.Rows)
                {
                    idCorte = iterar["ID"].ToString();
                    fecha = iterar["FechaOPeracion"].ToString();
                    idEmpleado = Convert.ToInt32(iterar["IdEmpleado"].ToString());
                    empleado = iterar["nombre"].ToString();
                    nombreUser = iterar["Usuario"].ToString();

                    if (idEmpleado > 0 )//Cuando es Empleado
                    {
                        name = empleado;
                        DGVReporteCaja.Rows.Add(idCorte, name, fecha, pdf, pdf, pdf);
                        
                    }
                    else if (idEmpleado.Equals(0)) //Cuando es Admin
                    {
                        name = $"ADMIN ({nombreUser})";
                        DGVReporteCaja.Rows.Add(idCorte, name, fecha, pdf, pdf, pdf);
                    }
                }
                txtBuscador.Text = string.Empty;
                txtBuscador.Focus();
            }
            else
            {
                MessageBox.Show($"No se encontraron resultados", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuscador.Text = string.Empty;
                txtBuscador.Focus();
            }
        }

        private void cargarDGVInicial()
        {//Cargar el DGV al Abrir el Form
            var primerFecha = DateTime.Today.AddDays(-7).ToString("yyyy/MM/dd");
            var segundaFecha = segundoDatePicker.Value.AddDays(1).ToString("yyyy/MM/dd");

            var name = string.Empty; var fecha = string.Empty; var empleado = string.Empty; var idCorte = string.Empty; var idEmpleado = 0;
            var consulta = cn.CargarDatos(cs.CargarDatosIniciarFormReportesCaja(primerFecha, segundaFecha));

            if (!consulta.Rows.Count.Equals(0))
            {
                foreach (DataRow iterar in consulta.Rows)
                {
                    idCorte = iterar["ID"].ToString();
                    fecha = iterar["FechaOPeracion"].ToString();
                    idEmpleado = Convert.ToInt32(iterar["IdEmpleado"].ToString());
                    empleado = iterar["nombre"].ToString();

                    if (idEmpleado > 0)
                    {
                        name = empleado;
                    }
                    else
                    {
                        name = $"ADMIN {FormPrincipal.userNickName.ToString()}";
                    }

                    //var empleado = validarEmpleado(Convert.ToInt32(obtenerEmpleado));

                    DGVReporteCaja.Rows.Add(idCorte, name, fecha, pdf, pdf, pdf);
                }
            }
        }

        private void txtBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            txtBuscador.CharacterCasing = CharacterCasing.Upper;
        }

        private void BuscarReporteCajaPorFecha_Shown(object sender, EventArgs e)
        {
            txtBuscador.Focus();
        }

        private void DGVReporteCaja_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = Convert.ToInt32(DGVReporteCaja.CurrentRow.Cells[0].Value.ToString());

            if (e.ColumnIndex.Equals(3))//Corte de Caja
            {
                var dato = traerDatosCaja(id);
                GenerarReporte(dato, "Corte de Caja");
            }
            else if (e.ColumnIndex.Equals(4))//Dinero Agregado
            {
                //var dato = traerDatosCaja(id);
                //GenerarReporte(dato, "Dinero Agregado");
            }
            else if (e.ColumnIndex.Equals(5))//Dinero Retirado
            {
                //var dato = traerDatosCaja(id);
                //GenerarReporte(dato, "Dinero Retirado");
            }
        }

        private string[] traerDatosCaja(int id)
        {
            List<string> lista = new List<string>();

            var fechas =  obtenerFechas(id);
            DateTime fecha1 = Convert.ToDateTime(fechas[1]);
            DateTime fecha2 = Convert.ToDateTime(fechas[0]);

            var saldoInicial = mb.SaldoInicialCajaReportes(FormPrincipal.userID, id);

            //Consulta caja
            var totalC = string.Empty; var efectivoC = string.Empty; var tarjetaC = string.Empty; var valesC = string.Empty; var chequeC = string.Empty; var transC = string.Empty; var creditoC = string.Empty;
            var consulta = cn.CargarDatos($"SELECT IFNULL(SUM(Cantidad),0) AS Total, IFNULL(SUM(Efectivo),0) AS Efectivo, IFNULL(SUM(Tarjeta),0) AS Tarjeta, IFNULL(SUM(Vales),0) AS Vales, IFNULL(SUM(Cheque),0) AS Cheque, IFNULL(SUM(Transferencia),0) AS Trans, IFNULL(SUM(Credito),0) AS Credito FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion != 'retiro' AND (FechaOperacion BETWEEN '{fecha1.ToString("yyyy-MM-dd hh:mm:ss")}' AND  '{fecha2.ToString("yyyy-MM-dd hh:mm:ss")}')");
            
            //Consulta Abonos
            var totalA = string.Empty; var efectivoA = string.Empty; var tarjetaA = string.Empty; var valesA = string.Empty; var chequeA = string.Empty; var transA = string.Empty;
            var segundaConsulta = cn.CargarDatos($"SELECT IFNULL(SUM(Total),0) AS Total, IFNULL(SUM(Efectivo),0) AS Efectivo, IFNULL(SUM(Tarjeta),0) AS Tarjeta, IFNULL(SUM(Vales),0) AS Vales, IFNULL(SUM(Cheque),0) AS Cheque, IFNULL(SUM(Transferencia),0) AS Trans FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND (FechaOperacion BETWEEN '{fecha1.ToString("yyyy-MM-dd hh:mm:ss")}' AND  '{fecha2.ToString("yyyy-MM-dd hh:mm:ss")}')");

            //Consulta dinero Retirado
            var totalR = string.Empty; var efectivoR = string.Empty; var tarjetaR = string.Empty; var valesR = string.Empty; var chequeR = string.Empty; var transR = string.Empty; var anticiposR = string.Empty;
            var consultaRetiro = cn.CargarDatos($"SELECT IFNULL(SUM(Cantidad),0) AS Total, IFNULL(SUM(Efectivo),0) AS Efectivo, IFNULL(SUM(Tarjeta),0) AS Tarjeta, IFNULL(SUM(Vales),0) AS Vales, IFNULL(SUM(Cheque),0) AS Cheque, IFNULL(SUM(Transferencia),0) AS Trans, IFNULL(SUM(Credito),0) AS Credito, IFNULL(SUM(Anticipo),0) AS Anticipo FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'retiro' AND (FechaOperacion BETWEEN '{fecha1.ToString("yyyy-MM-dd hh:mm:ss")}' AND  '{fecha2.ToString("yyyy-MM-dd hh:mm:ss")}')");

            //Consulta Anticipos
            var totalAnt = string.Empty; var efectivoAnt = string.Empty; var tarjetaAnt = string.Empty; var valesAnt = string.Empty; var chequeAnt = string.Empty; var transAnt = string.Empty;
            var consultaAnticipos = cn.CargarDatos($"SELECT IFNULL(SUM(Cantidad),0) AS Total, IFNULL(SUM(Efectivo),0) AS Efectivo, IFNULL(SUM(Tarjeta),0) AS Tarjeta, IFNULL(SUM(Vales),0) AS Vales, IFNULL(SUM(Cheque),0) AS Cheque, IFNULL(SUM(Transferencia),0) AS Trans FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'anticipo' AND (FechaOperacion BETWEEN '{fecha1.ToString("yyyy-MM-dd hh:mm:ss")}' AND  '{fecha2.ToString("yyyy-MM-dd hh:mm:ss")}')");
            

            //Consulta Dinero Agregado
            var totalAg = string.Empty; var efectivoAg = string.Empty; var tarjetaAg = string.Empty; var valesAg = string.Empty; var chequeAg = string.Empty; var transAg = string.Empty; var anticiposA = string.Empty;
            var consultaDineroAgregado = cn.CargarDatos($"SELECT IFNULL(SUM(Cantidad),0) AS Total, IFNULL(SUM(Efectivo),0) AS Efectivo, IFNULL(SUM(Tarjeta),0) AS Tarjeta, IFNULL(SUM(Vales),0) AS Vales, IFNULL(SUM(Cheque),0) AS Cheque, IFNULL(SUM(Transferencia),0) AS Trans, IFNULL(SUM(Credito),0) AS Credito, IFNULL(SUM(Anticipo),0) AS Anticipo FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'deposito' AND (FechaOperacion BETWEEN '{fecha1.ToString("yyyy-MM-dd hh:mm:ss")}' AND  '{fecha2.ToString("yyyy-MM-dd hh:mm:ss")}')");

            if (!consulta.Rows.Count.Equals(0))//Datos caja
            {
                totalC = consulta.Rows[0]["Total"].ToString();
                efectivoC = consulta.Rows[0]["Efectivo"].ToString();
                tarjetaC = consulta.Rows[0]["Tarjeta"].ToString();
                valesC = consulta.Rows[0]["Vales"].ToString();
                chequeC = consulta.Rows[0]["Cheque"].ToString();
                transC = consulta.Rows[0]["Trans"].ToString();
                creditoC = consulta.Rows[0]["Credito"].ToString();
            }

            if (!segundaConsulta.Rows.Count.Equals(0))//Datos abonos
            {
                totalA = segundaConsulta.Rows[0]["Total"].ToString();
                //efectivoA = segundaConsulta.Rows[0]["Efectivo"].ToString();
                //tarjetaA = segundaConsulta.Rows[0]["Tarjeta"].ToString();
                //valesA = segundaConsulta.Rows[0]["Vales"].ToString();
                //chequeA = segundaConsulta.Rows[0]["Cheque"].ToString();
                //transA = segundaConsulta.Rows[0]["Trans"].ToString();
            }

            if (!consultaRetiro.Rows.Count.Equals(0))//Dinero Retirado
            {
                totalR = consultaRetiro.Rows[0]["Total"].ToString();
                efectivoR = consultaRetiro.Rows[0]["Efectivo"].ToString();
                tarjetaR = consultaRetiro.Rows[0]["Tarjeta"].ToString();
                valesR = consultaRetiro.Rows[0]["Vales"].ToString();
                chequeR = consultaRetiro.Rows[0]["Cheque"].ToString();
                transR = consultaRetiro.Rows[0]["Trans"].ToString();
                anticiposR = consultaRetiro.Rows[0]["Anticipo"].ToString();
            }

            if (!consultaAnticipos.Rows.Count.Equals(0))//Anticipos
            {
                totalAnt = consultaAnticipos.Rows[0]["Total"].ToString();
                efectivoAnt = consultaAnticipos.Rows[0]["Efectivo"].ToString();
                tarjetaAnt = consultaAnticipos.Rows[0]["Tarjeta"].ToString();
                valesAnt = consultaAnticipos.Rows[0]["Vales"].ToString();
                chequeAnt = consultaAnticipos.Rows[0]["Cheque"].ToString();
                transAnt = consultaAnticipos.Rows[0]["Trans"].ToString();
                
            }

            if (!consultaDineroAgregado.Rows.Count.Equals(0))//Dinero Agregado
            {
                totalAg = consultaDineroAgregado.Rows[0]["Total"].ToString();
                efectivoAg = consultaDineroAgregado.Rows[0]["Efectivo"].ToString();
                tarjetaAg = consultaDineroAgregado.Rows[0]["Tarjeta"].ToString();
                valesAg = consultaDineroAgregado.Rows[0]["Vales"].ToString();
                chequeAg = consultaDineroAgregado.Rows[0]["Cheque"].ToString();
                transAg = consultaDineroAgregado.Rows[0]["Trans"].ToString();
                anticiposA = consultaDineroAgregado.Rows[0]["Anticipo"].ToString();
            }

            //Sumar cantidades
            var ventas = ((float)Convert.ToDecimal(efectivoC) + (float)Convert.ToDecimal(tarjetaC) + (float)Convert.ToDecimal(valesC) + (float)Convert.ToDecimal(chequeC) + (float)Convert.ToDecimal(transC) + (float)Convert.ToDecimal(creditoC) + (float)Convert.ToDecimal(totalA) + (float)Convert.ToDecimal(anticiposA));

            var anticipos = ((float)Convert.ToDecimal(efectivoAnt) + (float)Convert.ToDecimal(tarjetaAnt) + (float)Convert.ToDecimal(valesAnt) + (float)Convert.ToDecimal(chequeAnt) + (float)Convert.ToDecimal(transAnt));

            var agregado = ((float)Convert.ToDecimal(efectivoAg) + (float)Convert.ToDecimal(tarjetaAg) + (float)Convert.ToDecimal(valesAg) + (float)Convert.ToDecimal(chequeAg) + (float)Convert.ToDecimal(transAg));

            var retirado = ((float)Convert.ToDecimal(efectivoR) + (float)Convert.ToDecimal(tarjetaR) + (float)Convert.ToDecimal(valesR) + (float)Convert.ToDecimal(chequeR) + (float)Convert.ToDecimal(transR) + (float)Convert.ToDecimal(anticiposR) + (float)Convert.ToDecimal(totalA));

            var total = ((float)Convert.ToDecimal(efectivoAnt) + (float)Convert.ToDecimal(tarjetaAnt) + (float)Convert.ToDecimal(valesAnt) + (float)Convert.ToDecimal(chequeAnt) + (float)Convert.ToDecimal(transAnt));
            //var total = (Convert.ToDecimal(totalC) + Convert.ToDecimal(totalA) - Convert.ToDecimal(totalR));
            //var efectivo = (Convert.ToDecimal(efectivoC) + Convert.ToDecimal(efectivoA) - Convert.ToDecimal(efectivoR));
            //var tarjeta = (Convert.ToDecimal(tarjetaC) + Convert.ToDecimal(tarjetaA) - Convert.ToDecimal(tarjetaR));
            //var vales = (Convert.ToDecimal(valesC) + Convert.ToDecimal(valesA) - Convert.ToDecimal(valesR));
            //var cheque = (Convert.ToDecimal(chequeC) + Convert.ToDecimal(chequeA) - Convert.ToDecimal(chequeR));
            //var transferencia = (Convert.ToDecimal(transC) + Convert.ToDecimal(transA) - Convert.ToDecimal(transR));
            //var credito = creditoC;
            //var anticipos = importeAnticipo;

            //lista.Add(efectivoC+"|"+tarjetaC+"|"+valesC+"|"+ chequeC+"|"+transC+"|"+creditoC+"|"+importeAnticipo+"|"+totalC);//Ventas

            //lista.Add(efectivoA+"|"+tarjetaA + "|" +valesA + "|" +chequeA + "|" +transA + "|" +totalA);//Abonos

            //lista.Add(efectivoR + "|" +tarjetaR + "|" +valesR + "|" +chequeR + "|" +transR + "|" +totalR);//Dinero Retirado

            //lista.Add(efectivoAg + "|" +tarjetaAg + "|" +valesAg + "|" +chequeAg + "|" +transAg + "|" +totalAg);//Dinero Agregado

            lista.Add("Efectivo:                    "+efectivoC + "|Efectivo:              " + efectivoAnt + "|Efectivo:              " + efectivoAg + "|Efectivo:                     " + efectivoR + "|Efectivo:               " + ((float)Convert.ToDecimal(efectivoC) + (float)Convert.ToDecimal(efectivoAnt) + (float)Convert.ToDecimal(efectivoAg) + (float)Convert.ToDecimal(efectivoR)));

            lista.Add("Tarjeta:                      "+tarjetaC + "|Tarjeta:                " + tarjetaAnt + "|Tarjeta:                " + tarjetaAg + "|Tarjeta:                      " + tarjetaR + "|Tarjeta:                 " + ((float)Convert.ToDecimal(tarjetaC) + (float)Convert.ToDecimal(tarjetaAnt) + (float)Convert.ToDecimal(tarjetaAg) + (float)Convert.ToDecimal(tarjetaR)));

            lista.Add("Vales:                        " + valesC + "|Vales:                  " + valesAnt + "|Vales:                  " + valesAg + "|Vales:                        " + valesR + "|Vales:                   " + ((float)Convert.ToDecimal(valesC) + (float)Convert.ToDecimal(valesAnt) + (float)Convert.ToDecimal(valesAg) + (float)Convert.ToDecimal(valesR)));

            lista.Add("Cheque:                     " + chequeC + "|Cheque:               " + chequeAnt + "|Cheque:               " + chequeAg + "|Cheque:                     " + chequeR + "|Cheque:               " + ((float)Convert.ToDecimal(chequeC) + (float)Convert.ToDecimal(chequeAnt) + (float)Convert.ToDecimal(chequeAg) + (float)Convert.ToDecimal(chequeR)));

            lista.Add("Transferencia:           " + transC + "|Transferencia:     " + transAnt + "|Transferencia:     " + transAg + "|Transferencia:           " + transR + "|Transferencia:     " + ((float)Convert.ToDecimal(transC) + (float)Convert.ToDecimal(transAnt) + (float)Convert.ToDecimal(transAg) + (float)Convert.ToDecimal(transR)));

            lista.Add("Crédito:                      " + creditoC +"|"+ string.Empty + "|"+ string.Empty + "|Anticipos Utilizados:  "+  anticiposR + "|Saldo Inicial:         " + saldoInicial.ToString());

            lista.Add("Abonos:                     " + creditoC + "|"+string.Empty + "|" +string.Empty + "|Devoluciones:            " + totalA + "|Credito:                " + creditoC);

            lista.Add("Anticipos Utilizados:  " + anticiposA + "|"+string.Empty + "|"+string.Empty + "|"+string.Empty + "|"+string.Empty);

            lista.Add("Total Ventas:             " + ventas + "|Total Anticipos:   " + anticipos + "|Total Agregado:   " + agregado + "|Total Retirado:           " + retirado + "|Total en  Caja:      " + total);

            return lista.ToArray();
        }

        private string[] obtenerFechas(int id)
        {
            List<string> lista = new List<string>();

            var fecha = string.Empty;

            var primerFecha = string.Empty; var segundaFecha = string.Empty;
            var query = cn.CargarDatos($"SELECT DISTINCT FechaOperacion FROM Caja  WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{id}'");
            var segundaQuery = cn.CargarDatos($"SELECT DISTINCT FechaOperacion FROM Caja  WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' AND ID < '{id}' ORDER BY FechaOperacion DESC LIMIT 1");

            if (!query.Rows.Count.Equals(0))
            {
                segundaFecha = query.Rows[0]["FechaOperacion"].ToString();

                if (!segundaQuery.Rows.Count.Equals(0))
                {
                    primerFecha = segundaQuery.Rows[0]["FechaOperacion"].ToString();
                }
                else
                {
                    primerFecha = $"<{segundaFecha}";
                }

                lista.Add(segundaFecha.ToString());
                lista.Add(primerFecha);
            }

            //if (tipoBusqueda.Equals("Corte"))//Cuando es corte de caja
            //{
                
            //}
            //else if (tipoBusqueda.Equals("DA"))//Cuanto es dinero agregado
            //{
               
            //}
            //else if (tipoBusqueda.Equals("DR"))//Cuando es dinero retirado
            //{
                
            //}
            return lista.ToArray();
        }

        private void GenerarReporte(string[] datosCaja, string reportType)
        {
            var mostrarClave = FormPrincipal.clave;

            //Datos del usuario
           var datos = FormPrincipal.datosUsuario;

            //Fuentes y Colores
           var colorFuenteNegrita = new BaseColor(Color.Black);
            var colorFuenteBlanca = new BaseColor(Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, colorFuenteNegrita);

            var numRow = 0;

            //Ruta donde se creara el archivo PDF
           var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;
            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\caja.pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\caja.pdf";
            }

            var fechaHoy = DateTime.Now;
            //rutaArchivo = @"C:\Archivos PUDVE\Reportes\caja.pdf";

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(reportType, fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            string UsuarioActivo = string.Empty;

            string tipoReporte = string.Empty,
                    encabezadoTipoReporte = string.Empty;

            //float PuntoDeVenta = 0,
            //        StockFisico = 0,
            //        Diferencia = 0,
            //        Precio = 0,
            //        CantidadPerdida = 0,
            //        CantidadRecuperada = 0;

            decimal totalC = 0, efectivoC = 0, tarjetaC = 0, valesC = 0, chequeC = 0, transferenciaC = 0, credito = 0, anticipos = 0, totalA = 0, efectivoA = 0, tarjetaA = 0, valesA = 0, chequeA = 0, transferenciaA = 0, totalR = 0, efectivoR = 0, tarjetaR = 0, valesR = 0, chequeR = 0, transferenciaR = 0, totalAg = 0, efectivoAg = 0, tarjetaAg = 0, valesAg = 0, chequeAg = 0, transferenciaAg = 0;

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

            //Encabezado del reporte
            encabezadoTipoReporte = reportType;

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

            Paragraph subTitulo = new Paragraph($"REPORTE DE CAJA\nSECCIÓN ELEGIDA " + encabezadoTipoReporte.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;


            float[] anchoColumnas = new float[] { 30f, 80f, 80f, 80f, 80f, 80f };

            //Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //============================
            //=== TABLA DE INVENTARIO  ===
            //============================

            PdfPTable tablaInventario = new PdfPTable(6);
            tablaInventario.WidthPercentage = 100;
            tablaInventario.SetWidths(anchoColumnas);

            PdfPCell colNum = new PdfPCell(new Phrase("No:", fuenteNegrita));
            colNum.BorderWidth = 1;
            colNum.BackgroundColor = new BaseColor(Color.SkyBlue);
            colNum.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colVentas = new PdfPCell(new Phrase("VENTAS:", fuenteNegrita));
            colVentas.BorderWidth = 1;
            colVentas.BackgroundColor = new BaseColor(Color.SkyBlue);
            colVentas.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colAnticipos = new PdfPCell(new Phrase("ANTICIPOS RECIBIDOS", fuenteTotales));
            colAnticipos.BorderWidth = 1;
            colAnticipos.HorizontalAlignment = Element.ALIGN_CENTER;
            colAnticipos.Padding = 3;
            colAnticipos.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colAgregado = new PdfPCell(new Phrase("DINERO AGREGADO", fuenteTotales));
            colAgregado.BorderWidth = 1;
            colAgregado.HorizontalAlignment = Element.ALIGN_CENTER;
            colAgregado.Padding = 3;
            colAgregado.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colRetirado = new PdfPCell(new Phrase("DINERO RETIRADO", fuenteTotales));
            colRetirado.BorderWidth = 1;
            colRetirado.HorizontalAlignment = Element.ALIGN_CENTER;
            colRetirado.Padding = 3;
            colRetirado.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colTotal = new PdfPCell(new Phrase("TOTAL DE CAJA", fuenteTotales));
            colTotal.BorderWidth = 1;
            colTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colTotal.Padding = 3;
            colTotal.BackgroundColor = new BaseColor(Color.SkyBlue);

            //PdfPCell colStockFisico = new PdfPCell(new Phrase("STOCK FISICO", fuenteTotales));
            //colStockFisico.BorderWidth = 1;
            //colStockFisico.HorizontalAlignment = Element.ALIGN_CENTER;
            //colStockFisico.Padding = 3;
            //colStockFisico.BackgroundColor = new BaseColor(Color.SkyBlue);

            //PdfPCell colFecha = new PdfPCell(new Phrase("FECHA", fuenteTotales));
            //colFecha.BorderWidth = 1;
            //colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            //colFecha.Padding = 3;
            //colFecha.BackgroundColor = new BaseColor(Color.SkyBlue);

            //PdfPCell colDiferencia = new PdfPCell(new Phrase("DIFERENCIA", fuenteTotales));
            //colDiferencia.BorderWidth = 1;
            //colDiferencia.HorizontalAlignment = Element.ALIGN_CENTER;
            //colDiferencia.Padding = 3;
            //colDiferencia.BackgroundColor = new BaseColor(Color.SkyBlue);

            //PdfPCell colPrecio = new PdfPCell(new Phrase("PRECIO", fuenteTotales));
            //colPrecio.BorderWidth = 1;
            //colPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
            //colPrecio.Padding = 3;
            //colPrecio.BackgroundColor = new BaseColor(Color.SkyBlue);

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

            tablaInventario.AddCell(colNum);
            tablaInventario.AddCell(colVentas);
            tablaInventario.AddCell(colAnticipos);
            tablaInventario.AddCell(colAgregado);
            tablaInventario.AddCell(colRetirado);
            tablaInventario.AddCell(colTotal);
            //tablaInventario.AddCell(colStockFisico);
            //tablaInventario.AddCell(colFecha);
            //tablaInventario.AddCell(colDiferencia);
            //tablaInventario.AddCell(colPrecio);
            //tablaInventario.AddCell(colPerdida);
            //tablaInventario.AddCell(colRecuperada);

            //var consulta = cn.CargarDatos($"SELECT * FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}' AND NoRevision = '{num}'");

            //foreach (DataRow row in consulta.Rows)
            //{
                //var nombre = row.Cells["Nombre"].Value.ToString();
                //var clave = row.Cells["ClaveInterna"].Value.ToString();
                //var codigo = row.Cells["CodigoBarras"].Value.ToString();
                ////var almacen = Utilidades.RemoverCeroStock(row.Cells["StockAlmacen"].Value.ToString());
                ////var fisico = Utilidades.RemoverCeroStock(row.Cells["StockFisico"].Value.ToString());
                //var almacen = row.Cells["StockAlmacen"].Value.ToString();
                //var fisico = row.Cells["StockFisico"].Value.ToString();
                //var fecha = row.Cells["Fecha"].Value.ToString();
                //var diferencia = row.Cells["Diferencia"].Value.ToString();
                //var precio = float.Parse(row.Cells["PrecioProducto"].Value.ToString());
                //var perdida = row.Cells["Perdida"].Value.ToString();
                //var recuperada = row.Cells["Recuperada"].Value.ToString();

                //var nombre = row["Nombre"].ToString();
                //var clave = row["ClaveInterna"].ToString();
                //var codigo = row["CodigoBarras"].ToString();
                //var almacen = row["StockAlmacen"].ToString();
                //var fisico = row["StockFisico"].ToString();
                //var fecha = row["Fecha"].ToString();
                //var diferencia = row["Diferencia"].ToString();
                //var precio = float.Parse(row["PrecioProducto"].ToString());
                //var perdida = string.Empty;
                //var recuperada = string.Empty;

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

                
            foreach (var iterador in datosCaja)
            {
                string[] words = iterador.Split('|');
                numRow++;
                PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                colNoConceptoTmp.BorderWidth = 1;
                colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_LEFT;

                PdfPCell colVentasTemp = new PdfPCell(new Phrase(words[0].ToString(), fuenteNormal));
                colVentasTemp.BorderWidth = 1;
                colVentasTemp.HorizontalAlignment = Element.ALIGN_LEFT;

                PdfPCell colAnticiposTemp = new PdfPCell(new Phrase(words[1].ToString(), fuenteNormal));
                colAnticiposTemp.BorderWidth = 1;
                colAnticiposTemp.HorizontalAlignment = Element.ALIGN_LEFT;

                PdfPCell colAgregadoTemp = new PdfPCell(new Phrase(words[2].ToString(), fuenteNormal));
                colAgregadoTemp.BorderWidth = 1;
                colAgregadoTemp.HorizontalAlignment = Element.ALIGN_LEFT;

                PdfPCell colRetiradoTEmp = new PdfPCell(new Phrase(words[3].ToString(), fuenteNormal));
                colRetiradoTEmp.BorderWidth = 1;
                colRetiradoTEmp.HorizontalAlignment = Element.ALIGN_LEFT;

                PdfPCell colTotalTemp = new PdfPCell(new Phrase(words[4].ToString(), fuenteNormal));
                colTotalTemp.BorderWidth = 1;
                colTotalTemp.HorizontalAlignment = Element.ALIGN_LEFT;

                tablaInventario.AddCell(colNoConceptoTmp);
                tablaInventario.AddCell(colVentasTemp);
                tablaInventario.AddCell(colAnticiposTemp);
                tablaInventario.AddCell(colAgregadoTemp);
                tablaInventario.AddCell(colRetiradoTEmp);
                tablaInventario.AddCell(colTotalTemp);
            }
                

                //PdfPCell colFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
                //colFechaTmp.BorderWidth = 1;
                //colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                //PdfPCell colDiferenciaTmp = new PdfPCell(new Phrase(diferencia, fuenteNormal));
                //Diferencia += (float)Convert.ToDouble(diferencia);
                //colDiferenciaTmp.BorderWidth = 1;
                //colDiferenciaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                //PdfPCell colPrecioTmp = new PdfPCell(new Phrase(precio.ToString("0.00"), fuenteNormal));
                //Precio += (float)Convert.ToDouble(precio);
                //colPrecioTmp.BorderWidth = 1;
                //colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                //PdfPCell colPerdidaTmp = new PdfPCell(new Phrase(perdida, fuenteNormal));
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

                
                //tablaInventario.AddCell(colFechaTmp);
                //tablaInventario.AddCell(colDiferenciaTmp);
                //tablaInventario.AddCell(colPrecioTmp);
                //tablaInventario.AddCell(colPerdidaTmp);
                //tablaInventario.AddCell(colRecuperadaTmp);
            //}


            //if (PuntoDeVenta > 0 || StockFisico > 0)
            //{
            //    PdfPCell colNoConceptoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            //    colNoConceptoTmpExtra.BorderWidth = 0;
            //    colNoConceptoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colNombreTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            //    colNombreTmpExtra.BorderWidth = 0;
            //    colNombreTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

            //    PdfPCell colClaveTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
            //    colClaveTmpExtra.BorderWidth = 0;
            //    colClaveTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

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
            //    tablaInventario.AddCell(colClaveTmpExtra);
            //    tablaInventario.AddCell(colCodigoTmpExtra);
            //    tablaInventario.AddCell(colPuntoVentaTmpExtra);
            //    tablaInventario.AddCell(colStockFisicoTmpExtra);
            //    tablaInventario.AddCell(colFechaTmpExtra);
            //    tablaInventario.AddCell(colDiferenciaTmpExtra);
            //    tablaInventario.AddCell(colPrecioTmpExtra);
            //    tablaInventario.AddCell(colPerdidaTmpExtra);
            //    tablaInventario.AddCell(colRecuperadaTmpExtra);
            //}

            reporte.Add(titulo);
            reporte.Add(Usuario);
            reporte.Add(subTitulo);
            reporte.Add(tablaInventario);

            //================================
            //=== FIN TABLA DE INVENTARIO ===
            //================================

            reporte.AddTitle("Reporte Caja");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.Show();
        }
    }
}
