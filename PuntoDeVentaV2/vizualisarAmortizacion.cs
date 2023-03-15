using Microsoft.Reporting.WinForms;
using MySql.Data.MySqlClient;
using PuntoDeVentaV2.ReportesImpresion;
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
    public partial class vizualisarAmortizacion : Form
    {
        Consultas cs = new Consultas();
        Conexion cn = new Conexion();
        Moneda oMoneda = new Moneda();
        int IDVenta;
        decimal Total = 0;
        string resultado;
        public static string ruta_archivos_guadados = @"C:\Archivos PUDVE\MisDatos\CSD\";
        string[] usuario;
        string nombreUsuario;
        string DireccionLogo;
        bool SiHayLogo = false;
        string pathLogoImage;

        decimal total;
        public static bool fuePorVenta = false;
        public vizualisarAmortizacion(int IDDeLaVEnta, decimal totalOriginal)
        {
            InitializeComponent();
            this.IDVenta = IDDeLaVEnta;

            total = totalOriginal;
        }

        private void FormNotaDeVenta_Load(object sender, EventArgs e)
        {
            CargarNotaDeVenta();
            
        }

        private void CargarNotaDeVenta()
        {
            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\Amortizacion\Amortizacion.rdlc";

            decimal porcentajeinteres = 0;
            DataTable amortizacion1 = cn.CargarDatos($@"SELECT
                creditoMinimoAbono AS 'ABONO',
                SUBSTRING_INDEX( SUBSTRING_INDEX( FechaInteres, '%', n ), '%', - 1 ) AS 'Fechas' 
            FROM
                reglascreditoventa
                CROSS JOIN (
                SELECT
                    ROW_NUMBER() OVER (ORDER BY FechaInteres) AS n 
                FROM
                    reglascreditoventa
                ) AS t2 
            WHERE
                IDVenta = {IDVenta}
                AND n <= LENGTH( FechaInteres ) - LENGTH( REPLACE ( FechaInteres, '%', '' ) ) + 1");
            using (DataTable interesesamortizacion = cn.CargarDatos($"SELECT creditoPorcentajeinteres FROM reglascreditoventa WHERE IDVenta = {IDVenta}"))
            {
             porcentajeinteres = decimal.Parse(interesesamortizacion.Rows[0][0].ToString())/100;

            }

            // Create a new column for "INTERES"
            amortizacion1.Columns.Add("INTERES", typeof(decimal));
            //amortizacion1.Columns.Add("PAGADO", typeof(Image));
            amortizacion1.Columns.Add("PAGADO", typeof(string));
            amortizacion1.Columns.Add("DIAS", typeof(int));

            Image check = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\checkG.png");
            Image uncheck = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\removeG.png");
            Image timer = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\timerG.png");

            decimal sumAbonoMinimo = 0; // Initialize the sum of "ABONO MINIMO" to zero

            // Loop through each row in the DataTable
            foreach (DataRow row in amortizacion1.Rows)
            {
                // Calculate the value of "INTERES"
                decimal interes = (total - sumAbonoMinimo) * porcentajeinteres;
                // Set the value of "INTERES" for this row
                row["INTERES"] = interes;
                // Add the value of "ABONO MINIMO" for this row to the sum
                sumAbonoMinimo += (decimal)row["ABONO"];
                
            }
            using (DataTable dt = cn.CargarDatos($"SELECT sum(Total-(intereses+cambio)) AS Monosas FROM abonos WHERE IDVenta={IDVenta}"))
            {
                decimal abonado = 0;
                if (!dt.Rows.Count.Equals(0))
                {
                    abonado = decimal.Parse(dt.Rows[0][0].ToString());
                }
                decimal abonos = decimal.Parse(amortizacion1.Rows[0][0].ToString());
                foreach (DataRow dataRow in amortizacion1.Rows)
                {
                    if (abonado > abonos)
                    {
                        dataRow["PAGADO"] = "Pagado";
                    }
                    else
                    {
                        if (DateTime.Parse(dataRow["FECHAS"].ToString()) > DateTime.Now)
                        {
                            dataRow["PAGADO"] = "A tiempo";
                        }
                        else
                        {
                            dataRow["PAGADO"] = "Atrasado";
                        }
                    }
                }
            }

            using (DataTable dtt = cn.CargarDatos($"SELECT sum(Total-intereses-cambio) AS Cantidad,FechaOperacion AS Fecha FROM abonos WHERE IDVenta={IDVenta} GROUP BY FechaOperacion"))
            {
                decimal countuh = 0;
                decimal abonos = decimal.Parse(amortizacion1.Rows[0][0].ToString());
                int row = 0;

                

                foreach (DataRow dataRow in amortizacion1.Rows)
                {
                    if (DateTime.Parse(dataRow["FECHAS"].ToString()) > DateTime.Now)
                    {
                        dataRow["DIAS"] = 0;
                    }
                    else
                    {
                        bool checker = true;
                        foreach (DataRow data in dtt.Rows)
                        {
                            if (checker)
                            {
                                countuh += decimal.Parse(data["Cantidad"].ToString());
                                if (countuh >= abonos * (row + 1))
                                {
                                    abonos += abonos;
                                    int dias = DateTime.Parse(data["Fecha"].ToString()).Day - DateTime.Parse(dataRow["FECHAS"].ToString()).Day;
                                    checker = false;
                                    dataRow["DIAS"] = dias;
                                }
                                if (row+1 == amortizacion1.Rows.Count)
                                {
                                    dataRow["DIAS"] = DateTime.Now.Day - DateTime.Parse(dataRow["FECHAS"].ToString()).Day;
                                }
                            }
                        }
                        row++;
                    }
                }

            }
            
            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource amoritzacion = new ReportDataSource("amortizacion1", amortizacion1);

            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.LocalReport.DataSources.Add(amoritzacion);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.RefreshReport();
        }

       
        private void FormNotaDeVenta_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Close();
            }

        }
    }
}
