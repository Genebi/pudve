using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using PuntoDeVentaV2.ReportesImpresion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class VisualizadorTicketAnticipo : Form
    {
        Consultas cs = new Consultas();
        Conexion cn = new Conexion();
        public int idAnticipoViz { get; set; }
        public int idVentaViz { get; set; }
        public int anticipoSinHistorial { get; set; }
        public VisualizadorTicketAnticipo()
        {
            InitializeComponent();
        }

        private void VisualizadorTicketAnticipo_Load(object sender, EventArgs e)
        {
            CargarDatosAnticipo();
            if (Ventas.EsEnVentas.Equals(true) || Anticipos.SeCancelo.Equals(true))
            {
                Ventas.EsEnVentas = false;
                Anticipos.SeCancelo = false;
                btnReImprimirTicket.PerformClick();
                this.Close();
            }
        }

        private void CargarDatosAnticipo()
        {
            string cadenaConn = string.Empty;
            string queryVenta = string.Empty;

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                cadenaConn = $"datasource={Properties.Settings.Default.Hosting};port=6666;username=root;password=;database=pudve;";
            }
            else
            {
                cadenaConn = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
            }
            if (anticipoSinHistorial == 1)
            {
                queryVenta = cs.visualizadorTicketAnticipo(idAnticipoViz);
            }
            else
            {
                queryVenta = cs.impresionTicketAnticipo(idAnticipoViz, idVentaViz);
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
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\TicketAnticipo\ReporteTicketAnticipo.rdlc";

            MySqlDataAdapter ventaDA = new MySqlDataAdapter(queryVenta, conn);
            DataTable ventaDT = new DataTable();

            ventaDA.Fill(ventaDT);
            ReportParameterCollection reportParameters = new ReportParameterCollection();

            decimal AnticipoUtilizadoold = 0;
            if (!ventaDT.Rows[0]["AnticipoAplicado"].Equals("N/A"))
            {
                using (var otradt = cn.CargarDatos($"SELECT * FROM ventas WHERE IDAnticipo = {ventaDT.Rows[0]["IDAnticipo"].ToString()}"))
                {
                    if (otradt.Rows.Count > 1)
                    {
                        using (var dt = cn.CargarDatos($"SELECT SUM(Subtotal + IVA16 + IVA8) AS 'AnticipoAplicado' FROM ventas WHERE IDAnticipo = {ventaDT.Rows[0]["IDAnticipo"].ToString()} AND ID != {idVentaViz}"))
                        {
                            if (!dt.Rows.Count.Equals(0))
                            {
                                if (!string.IsNullOrWhiteSpace(dt.Rows[0][0].ToString()))
                                {
                                    AnticipoUtilizadoold = Convert.ToDecimal(dt.Rows[0][0]);
                                }

                            }
                        }
                    }
                    else
                    {
                        AnticipoUtilizadoold = 0;
                    }
                }

            }
            decimal AnticipoUtilizadoEnLaVenta = 0;
            if (!idVentaViz.Equals(0))
            {
                if (AnticipoUtilizadoold == 0)
                {
                    using (var dt = cn.CargarDatos($"SELECT SUM( vent.Subtotal + vent.IVA16 + vent.IVA8 ) AS 'Total', ant.ImporteOriginal FROM ventas as vent INNER JOIN anticipos AS ant ON (ant.ID = vent.IDAnticipo) WHERE vent.ID = {idVentaViz}"))
                    {
                        decimal total = Convert.ToDecimal(dt.Rows[0]["Total"]);
                        decimal AnticipoOriginal = Convert.ToDecimal(dt.Rows[0]["ImporteOriginal"]);
                        if (total >= AnticipoOriginal)
                        {
                            AnticipoUtilizadoEnLaVenta = AnticipoOriginal;
                        }
                        else
                        {
                            AnticipoUtilizadoEnLaVenta = total;
                        }
                    }
                }
                else
                {
                    using (var DT = cn.CargarDatos($"SELECT ImporteOriginal,AnticipoAplicado FROM anticipos WHERE ID ={idAnticipoViz}"))
                    {
                        decimal AnticipoUtilizadoNew = Convert.ToDecimal(DT.Rows[0]["AnticipoAplicado"]);
                        AnticipoUtilizadoEnLaVenta = AnticipoUtilizadoNew - AnticipoUtilizadoold;

                    }
                }
                reportParameters.Add(new ReportParameter("Actual", AnticipoUtilizadoEnLaVenta.ToString("0.00")));

            }
            else
            {
                reportParameters.Add(new ReportParameter("Actual", "N/A"));
            }
            reportParameters.Add(new ReportParameter("Anterior", AnticipoUtilizadoold.ToString("0.00")));
           
            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.LocalReport.DataSources.Clear();

            #region Impresion Ticket de 80 mm
            ReportDataSource rp = new ReportDataSource("TicketAnticipo", ventaDT);

            string DirectoryImage = string.Empty;

            this.reportViewer1.LocalReport.EnableExternalImages = true;

            this.reportViewer1.LocalReport.DataSources.Add(rp);
            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();

            //LocalReport rdlc = new LocalReport();
            //rdlc.EnableExternalImages = true;
            //rdlc.ReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\AbonoRealizado\ReporteAbonos.rdlc";
            //rdlc.DataSources.Add(rp);
            #endregion

            //EnviarImprimir imp = new EnviarImprimir();
            //imp.Imprime(rdlc);
            //this.reportViewer1.RefreshReport();
        }

        private void btnReImprimirTicket_Click(object sender, EventArgs e)
        {
            string cadenaConn = string.Empty;
            string queryVenta = string.Empty;

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                cadenaConn = $"datasource={Properties.Settings.Default.Hosting};port=6666;username=root;password=;database=pudve;";
            }
            else
            {
                cadenaConn = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
            }
            if (anticipoSinHistorial == 1)
            {
                queryVenta = cs.visualizadorTicketAnticipo(idAnticipoViz);
            }
            else
            {
                queryVenta = cs.impresionTicketAnticipo(idAnticipoViz, idVentaViz);
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
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\TicketAnticipo\ReporteTicketAnticipo.rdlc";

            MySqlDataAdapter ventaDA = new MySqlDataAdapter(queryVenta, conn);
            DataTable ventaDT = new DataTable();

            ventaDA.Fill(ventaDT);
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            decimal AnticipoUtilizadoold = 0;
            if (!ventaDT.Rows[0]["AnticipoAplicado"].Equals("N/A"))
            {
                using (var otradt = cn.CargarDatos($"SELECT * FROM ventas WHERE IDAnticipo = {ventaDT.Rows[0]["IDAnticipo"].ToString()}"))
                {
                    if (otradt.Rows.Count > 1)
                    {
                        using (var dt = cn.CargarDatos($"SELECT SUM(Subtotal + IVA16 + IVA8) AS 'AnticipoAplicado' FROM ventas WHERE IDAnticipo = {ventaDT.Rows[0]["IDAnticipo"].ToString()} AND ID != {idVentaViz}"))
                        {
                            if (!dt.Rows.Count.Equals(0))
                            {
                                if (!string.IsNullOrWhiteSpace(dt.Rows[0][0].ToString()))
                                {
                                    AnticipoUtilizadoold = Convert.ToDecimal(dt.Rows[0][0]);
                                }

                            }
                        }
                    }
                    else
                    {
                        AnticipoUtilizadoold = 0;
                    }
                }

            }
            decimal AnticipoUtilizadoEnLaVenta = 0;
            if (!idVentaViz.Equals(0))
            {
                if (AnticipoUtilizadoold == 0)
                {
                    using (var dt = cn.CargarDatos($"SELECT SUM( vent.Subtotal + vent.IVA16 + vent.IVA8 ) AS 'Total', ant.ImporteOriginal FROM ventas as vent INNER JOIN anticipos AS ant ON (ant.ID = vent.IDAnticipo) WHERE vent.ID = {idVentaViz}"))
                    {
                        decimal total = Convert.ToDecimal(dt.Rows[0]["Total"]);
                        decimal AnticipoOriginal = Convert.ToDecimal(dt.Rows[0]["ImporteOriginal"]);
                        if (total >= AnticipoOriginal)
                        {
                            AnticipoUtilizadoEnLaVenta = AnticipoOriginal;
                        }
                        else
                        {
                            AnticipoUtilizadoEnLaVenta = total;
                        }
                    }
                }
                else
                {
                    using (var DT = cn.CargarDatos($"SELECT ImporteOriginal,AnticipoAplicado FROM anticipos WHERE ID ={idAnticipoViz}"))
                    {
                        decimal AnticipoUtilizadoNew = Convert.ToDecimal(DT.Rows[0]["AnticipoAplicado"]);
                        AnticipoUtilizadoEnLaVenta = AnticipoUtilizadoNew - AnticipoUtilizadoold;

                    }
                }
                reportParameters.Add(new ReportParameter("Actual", AnticipoUtilizadoEnLaVenta.ToString("0.00")));

            }
            else
            {
                reportParameters.Add(new ReportParameter("Actual", "N/A"));
            }
            reportParameters.Add(new ReportParameter("Anterior", AnticipoUtilizadoold.ToString("0.00")));
            //this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            //this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            //this.reportViewer1.LocalReport.DataSources.Clear();

            #region Impresion Ticket de 80 mm
            ReportDataSource rp = new ReportDataSource("TicketAnticipo", ventaDT);

            string DirectoryImage = string.Empty;

            //this.reportViewer1.LocalReport.EnableExternalImages = true;

            //this.reportViewer1.LocalReport.DataSources.Add(rp);
            //this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            //this.reportViewer1.RefreshReport();

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\TicketAnticipo\ReporteTicketAnticipo.rdlc";
            rdlc.DataSources.Add(rp);
            rdlc.SetParameters(reportParameters);
            #endregion

            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);
            //this.reportViewer1.RefreshReport();
        }
    }
}
