using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
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
    public partial class VisualizadorReporteVentas : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        DataTable DTFinal = new DataTable();
        DataTable DTGrafica = new DataTable();
        string codigosBuscar = "";
        int TipoDeVenta;
        bool ValorNull = false;

        public VisualizadorReporteVentas(string IDVentas,int TipoVenta)
        {
            InitializeComponent();
            this.codigosBuscar = IDVentas;
            this.TipoDeVenta = TipoVenta;
        }

        private void VisualizadorReporteVentas_Load(object sender, EventArgs e)
        {
            CargarDatos();
            CargarGrafica();
            CargarRDLC();
            this.reportViewer1.RefreshReport();
        }

        private void CargarGrafica()
        {
            var ajustarQuery = cn.CargarDatos( $"SELECT FechaOperacion,Ganancia FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID IN ({codigosBuscar}) ORDER BY FechaOperacion ASC");
            var ajustarQuery2 = cn.CargarDatos($"SELECT FechaOperacion,Ganancia FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID IN ({codigosBuscar}) ORDER BY FechaOperacion DESC");
            DateTime FechaInicial = Convert.ToDateTime(ajustarQuery.Rows[0]["FechaOperacion"]);
            DateTime FechaFinal = Convert.ToDateTime(ajustarQuery2.Rows[0]["FechaOperacion"]);
            var incio = FechaInicial.ToString("dd-MM-yyyy").Split('-');
            var final = FechaFinal.ToString("dd-MM-yyyy").Split('-');
            DTGrafica.Columns.Add("Tiempo", typeof(String));
            DTGrafica.Columns.Add("TotalVendido", typeof(String));
            DTGrafica.Columns.Add("Ganancia", typeof(String));
            if (incio[0].Equals(final[0]) && incio[1].Equals(final[1]) && incio[2].Equals(final[2]))
            {
                var DTPorHora = cn.CargarDatos($"SELECT Total,FechaOperacion,Ganancia,Cliente FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID IN ({codigosBuscar}) ORDER BY FechaOperacion ASC");
                int horas = 1;
                int rows = 0;
                int agregarRows = 0;
                for (int i = 0; i < 24; i++)
                {
                    decimal TotalHora = 0;
                    decimal ganancia = 0;
                    foreach (var item in DTPorHora.Rows)
                    {
                        DateTime Fecha = Convert.ToDateTime(DTPorHora.Rows[rows]["FechaOperacion"]);
                        string hora = Fecha.ToString("HH");
                        if (Convert.ToInt32(hora).Equals(horas))
                        {
                            TotalHora += Convert.ToDecimal(DTPorHora.Rows[rows]["Total"]);
                            if (!DTPorHora.Rows[rows]["Cliente"].ToString().Equals("Apertura de Caja"))
                            {
                                if (string.IsNullOrWhiteSpace(DTPorHora.Rows[rows]["Ganancia"].ToString()) || DTPorHora.Rows[rows]["Ganancia"].ToString().Equals("SIN PODER CALCULAR"))
                                {
                                    ganancia += 0;
                                    ValorNull = true;
                                }
                                else
                                {
                                    var sinsigno = DTPorHora.Rows[rows]["Ganancia"].ToString().Split('$');
                                    ganancia += Convert.ToDecimal(sinsigno[1]);
                                }
                            }
                            rows++;
                        }
                        else
                        {
                            rows++;
                        }
                    }
                    rows = 0;
                    string Columna;
                    if (TotalHora > 0)
                    {
                        if (horas < 12 || horas > 23)
                        {
                            Columna = $"{horas.ToString()}:00 am";
                        }
                        else
                        {
                            Columna = $"{horas.ToString()}:00 pm";
                        }
                        DTGrafica.Rows.Add();
                        DTGrafica.Rows[agregarRows]["Tiempo"] = Columna;
                        DTGrafica.Rows[agregarRows]["TotalVendido"] = TotalHora.ToString();
                        DTGrafica.Rows[agregarRows]["Ganancia"] = ganancia.ToString();
                        agregarRows++;
                    }
                    horas++;
                   
                    TotalHora = 0;
                }

                horas = 1;
            }
            else if (!incio[0].Equals(final[0]) && incio[1].Equals(final[1]) && incio[2].Equals(final[2]))
            {
                var DTPorDia = cn.CargarDatos($"SELECT Total,FechaOperacion,Ganancia,Cliente FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID IN ({codigosBuscar}) ORDER BY FechaOperacion ASC");
                var PrimerDia = FechaInicial.ToString("dd");
                int rows = 0;
                int dias = 1;
                decimal TotalDia = 0;
                decimal gananciaDia = 0;
                int agregarRows = 0;
                for (int i = 0; i < 31; i++)
                {
                    foreach (var item in DTPorDia.Rows)
                    {
                        DateTime Fecha = Convert.ToDateTime(DTPorDia.Rows[rows]["FechaOperacion"]);
                        string dia = Fecha.ToString("dd");
                        if (Convert.ToInt32(dia).Equals(dias))
                        {
                            TotalDia += Convert.ToDecimal(DTPorDia.Rows[rows]["Total"]);
                            if (!DTPorDia.Rows[rows]["Cliente"].ToString().Equals("Apertura de Caja"))
                            {
                                if (string.IsNullOrWhiteSpace((DTPorDia.Rows[rows]["Ganancia"].ToString())) || DTPorDia.Rows[rows]["Ganancia"].ToString().Equals("SIN PODER CALCULAR"))
                                {
                                    gananciaDia += 0;
                                    ValorNull = true;
                                }
                                else
                                {
                                    var sinsigno = DTPorDia.Rows[rows]["Ganancia"].ToString().Split('$');
                                    gananciaDia += Convert.ToDecimal(sinsigno[1]);
                                }
                            }

                          
                            rows++;
                        }
                        else
                        {
                            rows++;
                        }
                    }
                    rows = 0;
                    string Columna;
                    if (TotalDia > 0)
                    {
                        Columna = $" Dia: {dias.ToString()}";

                        DTGrafica.Rows.Add();
                        DTGrafica.Rows[agregarRows]["Tiempo"] = Columna;
                        DTGrafica.Rows[agregarRows]["TotalVendido"] = TotalDia.ToString();
                        DTGrafica.Rows[agregarRows]["Ganancia"] = gananciaDia.ToString();
                        agregarRows++;
                    }
                    dias++;
                    TotalDia = 0;
                }
            }
            else if (!incio[1].Equals(final[1]) && incio[2].Equals(final[2]))
            {
                var DTPorMes = cn.CargarDatos($"SELECT Total,FechaOperacion,Ganancia,Cliente FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID IN ({codigosBuscar}) ORDER BY FechaOperacion ASC");
                int rows = 0;
                int meses = 1;
                decimal TotalMes = 0;
                decimal GananciaMes = 0;
                int agregarRows = 0;
                
                for (int i = 0; i < 12; i++)
                {
                    foreach (var item in DTPorMes.Rows)
                    {
                        DateTime Fecha = Convert.ToDateTime(DTPorMes.Rows[rows]["FechaOperacion"]);
                        string mes = Fecha.ToString("MM");
                        if (Convert.ToInt32(mes).Equals(meses))
                        {
                            TotalMes += Convert.ToDecimal(DTPorMes.Rows[rows]["Total"]);

                            if (!DTPorMes.Rows[rows]["Cliente"].ToString().Equals("Apertura de Caja"))
                            {
                                if (string.IsNullOrWhiteSpace((DTPorMes.Rows[rows]["Ganancia"].ToString())) || DTPorMes.Rows[rows]["Ganancia"].ToString().Equals("SIN PODER CALCULAR"))
                                {
                                    GananciaMes += 0;
                                    ValorNull = true;
                                }
                                else
                                {
                                    var sinsigno = DTPorMes.Rows[rows]["Ganancia"].ToString().Split('$');
                                    GananciaMes += Convert.ToDecimal(sinsigno[1]);
                                }
                            }

                          
                            rows++;
                        }
                        else
                        {
                            rows++;
                        }
                    }
                    rows = 0;
                    string columna = "";
                    if (TotalMes > 0)
                    {
                        if (meses == 1)
                        {
                            columna = "Enero";
                        }
                        else if (meses == 2)
                        {
                            columna = "Febrero";
                        }
                        else if (meses == 3)
                        {
                            columna = "Marzo";
                        }
                        else if (meses == 4)
                        {
                            columna = "Abril";
                        }
                        else if (meses == 5)
                        {
                            columna = "Mayo";
                        }
                        else if (meses == 6)
                        {
                            columna = "Junio";
                        }
                        else if (meses == 7)
                        {
                            columna = "Julio";
                        }
                        else if (meses == 8)
                        {
                            columna = "Agosto";
                        }
                        else if (meses == 9)
                        {
                            columna = "Septiembre";
                        }
                        else if (meses == 10)
                        {
                            columna = "Octubre";
                        }
                        else if (meses == 11)
                        {
                            columna = "Noviembre";
                        }
                        else if (meses == 12)
                        {
                            columna = "Diciembre";
                        }
                        DTGrafica.Rows.Add();
                        DTGrafica.Rows[agregarRows]["Tiempo"] = columna;
                        DTGrafica.Rows[agregarRows]["TotalVendido"] = TotalMes.ToString();
                        DTGrafica.Rows[agregarRows]["Ganancia"] = GananciaMes.ToString();
                        agregarRows++;
                    }
                    TotalMes = 0;
                    meses++;
                }
            }
            else if (!incio[2].Equals(final[2]))
            {
                var DTPorAnno = cn.CargarDatos($"SELECT Total,FechaOperacion,Ganancia FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID IN ({codigosBuscar}) ORDER BY FechaOperacion ASC");
                var DTPorAnno2 = cn.CargarDatos($"SELECT Total,FechaOperacion,Ganancia FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID IN ({codigosBuscar}) ORDER BY FechaOperacion DESC");
                DateTime PrimerAnno = Convert.ToDateTime(DTPorAnno.Rows[0]["FechaOperacion"]);
                DateTime UltimoAnno = Convert.ToDateTime(DTPorAnno2.Rows[0]["FechaOperacion"]);

                string primero = PrimerAnno.ToString("yyyyy");
                string segundo = UltimoAnno.ToString("yyyyy");
                int Diferiencia = Convert.ToInt32(segundo) - Convert.ToInt32(primero) + 1;
                int annos = Convert.ToInt32(primero);
                int rows = 0;
                decimal TotalAnno = 0;
                decimal gananciaAnno = 0; ;
                int agregarRows = 0;
                for (int i = 0; i < Diferiencia; i++)
                {
                    foreach (var item in DTPorAnno.Rows)
                    {
                        DateTime fecha = Convert.ToDateTime(DTPorAnno.Rows[rows]["FechaOperacion"]);
                        string anno = fecha.ToString("yyyyy");
                        if (Convert.ToInt32(anno).Equals(annos))
                        {
                            TotalAnno += Convert.ToDecimal(DTPorAnno.Rows[rows]["Total"]);
                            if (!DTPorAnno.Rows[rows]["Cliente"].ToString().Equals("Apertura de Caja"))
                            {
                                if (string.IsNullOrWhiteSpace((DTPorAnno.Rows[rows]["Ganancia"].ToString())) || DTPorAnno.Rows[rows]["Ganancia"].ToString().Equals("SIN PODER CALCULAR"))
                                {
                                    gananciaAnno += 0;
                                    ValorNull = true;
                                }
                                else
                                {
                                    var sinsigno = DTPorAnno.Rows[rows]["Ganancia"].ToString().Split('$');
                                    gananciaAnno += Convert.ToDecimal(sinsigno[1]);
                                }
                            }
                            rows++;
                        }
                        else
                        {
                            rows++;
                        }
                    }
                    rows = 0;
                    string Columna = $"Año: {annos}";
                    if (TotalAnno > 0)
                    {
                        DTGrafica.Rows.Add();
                        DTGrafica.Rows[agregarRows]["Tiempo"] = Columna;
                        DTGrafica.Rows[agregarRows]["TotalVendido"] = TotalAnno.ToString();
                        DTGrafica.Rows[agregarRows]["Ganancia"] = gananciaAnno.ToString();
                        agregarRows++;
                    }
                    TotalAnno = 0;
                    annos++;
                }
            }
            if (ValorNull == true)
            {
                int contador = 0;
                foreach (var item in DTGrafica.Rows)
                {
                    DTGrafica.Rows[contador]["Ganancia"] = "";
                    contador++;
                }
            }
        }

        private void CargarRDLC()
        {
            string cadenaConn = string.Empty;
            string queryVentas = string.Empty;
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                cadenaConn = $"datasource={Properties.Settings.Default.Hosting};port=6666;username=root;password=;database=pudve;";
            }
            else
            {
                cadenaConn = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
            }
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = cadenaConn;
            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\ReporteVentas\VentasReporte.rdlc";
            string TipoVENTATexto;
            if (TipoDeVenta.Equals(0))
            {
                TipoVENTATexto = "SECCIÓN ELEGIDA VENTAS PAGADAS";
            }
            else if (TipoDeVenta.Equals(1))
            {
                TipoVENTATexto = "SECCIÓN ELEGIDA VENTAS GUARDADAS";
            }
            else if (TipoDeVenta.Equals(2))
            {
                TipoVENTATexto = "SECCIÓN ELEGIDA VENTAS CANCELADAS";
            }
            else if (TipoDeVenta.Equals(3))
            {
                TipoVENTATexto = "SECCIÓN ELEGIDA VENTAS A CREDITO";
            }
            else
            {
                TipoVENTATexto = "SECCIÓN ELEGIDA VENTAS GLOBALES";
            }
            string usuario;
            if (!FormPrincipal.userNickName.Contains('@'))
            {
                usuario = $"ADMIN({FormPrincipal.userNickName})";
            }
            else
            {
                var DTNombre = cn.CargarDatos($"SELECT nombre FROM empleados WHERE IDUsuario = {FormPrincipal.userID} AND ID = {FormPrincipal.id_empleado}");
                usuario = DTNombre.Rows[0]["nombre"].ToString();
            }
            string Fecha = $"Fecha: {DateTime.Now.ToString("dd-MM-yyyy HH:mm")}";

            decimal SumaTotal = 0;
            int rows = 0;
            foreach (var item in DTFinal.Rows)
            {
                SumaTotal += Convert.ToDecimal(DTFinal.Rows[rows]["Total"]);
                rows++;
            }
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("TipoVenta", TipoVENTATexto));
            reportParameters.Add(new ReportParameter("usuario", usuario));
            reportParameters.Add(new ReportParameter("Fecha", Fecha));
            reportParameters.Add(new ReportParameter("SumaTotal", SumaTotal.ToString("0.00"))); ;


            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource ReporteVentas = new ReportDataSource("DTReporteVenta", DTFinal);
            ReportDataSource ReporteGrafica = new ReportDataSource("DTGrafica", DTGrafica);

            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.LocalReport.DataSources.Add(ReporteVentas);
            this.reportViewer1.LocalReport.DataSources.Add(ReporteGrafica);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }

        private void CargarDatos()
        {
            var ajustarQuery = $"SELECT Cliente, RFC, IDEmpleado, Total, Folio, Serie,Ganancia FechaOperacion FROM Ventas WHERE IDUsuario = '{FormPrincipal.userID}' AND ID IN ({codigosBuscar})";
            var query = cn.CargarDatos(ajustarQuery);
            DTFinal = query;
            DTFinal.Columns.Add("No", typeof(String));
            DTFinal.Columns.Add("Empleado", typeof(String));
            int Numero = 1;
            int Rows = 0;
            foreach (var item in DTFinal.Rows)
            {
                int IDEmpleado = Convert.ToInt32(DTFinal.Rows[Rows]["IDEmpleado"]);
                if (!string.IsNullOrWhiteSpace(IDEmpleado.ToString()))
                {
                    if (IDEmpleado.Equals(0))
                    {
                        DTFinal.Rows[Rows]["Empleado"] = FormPrincipal.userNickName;
                    }
                    else
                    {
                        var NombreEmpleado = cn.CargarDatos($"SELECT nombre FROM empleados WHERE IDUsuario = {FormPrincipal.userID} AND ID = {DTFinal.Rows[Rows]["IDEmpleado"].ToString()}");
                        string nombre = NombreEmpleado.Rows[0]["nombre"].ToString();
                        DTFinal.Rows[Rows]["Empleado"] = nombre;
                    }
                }
                DTFinal.Rows[Rows]["No"] = Numero.ToString();
                Numero++;
                Rows++;
            }
        }
    }
}

