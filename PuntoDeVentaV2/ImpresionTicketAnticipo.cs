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
    public partial class ImpresionTicketAnticipo : Form
    {
        public int idAnticipo { get; set; }
        public int anticipoSinHistorial { get; set; }
        Consultas cs = new Consultas();
        Conexion cn = new Conexion();
        int idVentaViz = 0;
        public ImpresionTicketAnticipo()
        {
            InitializeComponent();
        }

        private void ImpresionTicketAnticipo_Load(object sender, EventArgs e)
        {
            cargarDatosTicket();

        }

        private void cargarDatosTicket()
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
                queryVenta = cs.visualizadorTicketAnticipo(idAnticipo);
            }
            else
            {
                queryVenta = cs.impresionTicketAnticipo(idAnticipo);
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
                    using (var DT = cn.CargarDatos($"SELECT ImporteOriginal,AnticipoAplicado FROM anticipos WHERE ID ={idAnticipo}"))
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

            string mensaje = "Ticket Anticipo";
            if (ventaDT.Rows[0]["Status"].ToString().Equals("4"))
            {
                mensaje = "Ticket Anticipo Devuelto";
            }
            reportParameters.Add(new ReportParameter("Mensaje", mensaje));

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.LocalReport.DataSources.Clear();

            #region Impresion Ticket de 80 mm
            ReportDataSource rp = new ReportDataSource("TicketAnticipo", ventaDT);

            string DirectoryImage = string.Empty;

            this.reportViewer1.LocalReport.EnableExternalImages = true;

            this.reportViewer1.LocalReport.DataSources.Add(rp);
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);
            rdlc.DataSources.Add(rp);
            #endregion

            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);
            this.Close();
        }
    }
}
