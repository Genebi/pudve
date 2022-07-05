﻿using Microsoft.Reporting.WinForms;
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
    public partial class VerTicketCajaAbierta8cmListadoVentas : Form
    {
        Consultas cs = new Consultas();
        Conexion cn = new Conexion();

        public int idVentaRealizada { get; set; }

        public VerTicketCajaAbierta8cmListadoVentas()
        {
            InitializeComponent();
        }

        private void VerTicketCajaAbierta8cmListadoVentas_Load(object sender, EventArgs e)
        {
            CargarDatosTicket();
        }

        private void CargarDatosTicket()
        {
            var servidor = Properties.Settings.Default.Hosting;
            string cadenaConn = string.Empty;
            string queryPresupuestoRealizado = cs.TicketAperturaCajaComoAdministrador(idVentaRealizada);
            MySqlConnection conn = new MySqlConnection();
            string pathApplication = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\AbrirCaja\ReporteTicketAbrirCaja80mm.rdlc";
            string DirectoryImage = string.Empty;
            string path = string.Empty;
            string pathBarCode = $@"C:\Archivos PUDVE\Ventas\Tickets\BarCode\";
            var folioVentaRealizada = string.Empty;
            var finalLogoTipoPath = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                cadenaConn = $"datasource={servidor};port=6666;username=root;password=;database=pudve;";
            }
            else
            {
                cadenaConn = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve";
            }

            conn.ConnectionString = cadenaConn;

            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            MySqlDataAdapter abrirCajaDA = new MySqlDataAdapter(queryPresupuestoRealizado, conn);
            DataTable abrirCajaDT = new DataTable();

            abrirCajaDA.Fill(abrirCajaDT);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            #region Impresion Ticket de 8 cm (80 mm)
            ReportDataSource rp = new ReportDataSource("TicketAbrirCaja", abrirCajaDT);

            this.reportViewer1.LocalReport.EnableExternalImages = true;

            ReportParameterCollection reportParameters = new ReportParameterCollection();

            if (!Directory.Exists(pathBarCode))
            {
                Directory.CreateDirectory(pathBarCode);
            }

            using (DataTable dtFolioVenta = cn.CargarDatos(cs.folioDeLaVentaParaElTicket(idVentaRealizada)))
            {
                if (!dtFolioVenta.Rows.Count.Equals(0))
                {
                    DataRow drFolioVenta = dtFolioVenta.Rows[0];
                    folioVentaRealizada = drFolioVenta["Folio"].ToString();
                }
            }

            var codigoBarraTicket = GenerarCodigoBarras(folioVentaRealizada, 170);
            codigoBarraTicket.Save($"{pathBarCode}{folioVentaRealizada}.png");

            var pathBarCodeFull = new Uri($"C:/Archivos PUDVE/Ventas/Tickets/BarCode/{folioVentaRealizada}.png").AbsoluteUri;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                var logoTipo = string.Empty;

                using (DataTable dtLogoTipo = cn.CargarDatos(cs.getLogoTipoUsuario()))
                {
                    if (!dtLogoTipo.Rows.Count.Equals(0))
                    {
                        DataRow drLogoTipoUsuario = dtLogoTipo.Rows[0];
                        logoTipo = drLogoTipoUsuario["LogoTipo"].ToString();
                    }
                }

                if (!string.IsNullOrWhiteSpace(logoTipo))
                {
                    string fileName = logoTipo;
                    string sourcePath = $@"\\{servidor}\Archivos PUDVE\MisDatos\Usuarios\";
                    string targetPath = @"C:\Archivos PUDVE\Ventas\Tickets\BarCode\";

                    // Use Path class to manipulate file and directory paths.
                    string sourceFile = Path.Combine(sourcePath, fileName);
                    string destFile = Path.Combine(targetPath, fileName);
                    finalLogoTipoPath = destFile;

                    // To copy a file to another location and
                    // overwrite the destination file if it already exists.
                    File.Copy(sourceFile, destFile, true);
                }

                path = new Uri($"C:/Archivos PUDVE/Ventas/Tickets/BarCode/").AbsoluteUri;
            }
            else
            {
                path = new Uri("C:/Archivos PUDVE/MisDatos/Usuarios/").AbsoluteUri;
            }

            //01 parametro string para mostrar / ocultar Path Logo
            reportParameters.Add(new ReportParameter("PathLogo", path));

            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.LocalReport.DataSources.Add(rp);
            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.RefreshReport();
            #endregion
        }

        private Image GenerarCodigoBarras(string txtCodigo, int ancho)
        {
            Image imagen;

            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();

            try
            {
                var anchoTmp = ancho / 2;
                var auxiliar = anchoTmp;

                anchoTmp = auxiliar / 2;
                ancho = ancho - anchoTmp;

                imagen = codigo.Encode(BarcodeLib.TYPE.CODE128, txtCodigo, Color.Black, Color.White, ancho, 40);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar código de barras para el ticket", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                imagen = null;
            }

            return imagen;
        }

        private void btnImprimirTicketAbrirCaja_Click(object sender, EventArgs e)
        {
            var servidor = Properties.Settings.Default.Hosting;
            string cadenaConn = string.Empty;
            string queryPresupuestoRealizado = cs.TicketAperturaCajaComoAdministrador(idVentaRealizada);
            MySqlConnection conn = new MySqlConnection();
            string pathApplication = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\AbrirCaja\ReporteTicketAbrirCaja80mm.rdlc";
            string DirectoryImage = string.Empty;
            string path = string.Empty;
            string pathBarCode = $@"C:\Archivos PUDVE\Ventas\Tickets\BarCode\";
            var folioVentaRealizada = string.Empty;
            var finalLogoTipoPath = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                cadenaConn = $"datasource={servidor};port=6666;username=root;password=;database=pudve;";
            }
            else
            {
                cadenaConn = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve";
            }

            conn.ConnectionString = cadenaConn;

            try
            {
                conn.Open();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            MySqlDataAdapter abrirCajaDA = new MySqlDataAdapter(queryPresupuestoRealizado, conn);
            DataTable abrirCajaDT = new DataTable();

            abrirCajaDA.Fill(abrirCajaDT);

            #region Impresion Ticket de 8 cm (80 mm)
            ReportDataSource rp = new ReportDataSource("TicketAbrirCaja", abrirCajaDT);

            ReportParameterCollection reportParameters = new ReportParameterCollection();

            if (!Directory.Exists(pathBarCode))
            {
                Directory.CreateDirectory(pathBarCode);
            }

            using (DataTable dtFolioVenta = cn.CargarDatos(cs.folioDeLaVentaParaElTicket(idVentaRealizada)))
            {
                if (!dtFolioVenta.Rows.Count.Equals(0))
                {
                    DataRow drFolioVenta = dtFolioVenta.Rows[0];
                    folioVentaRealizada = drFolioVenta["Folio"].ToString();
                }
            }

            var codigoBarraTicket = GenerarCodigoBarras(folioVentaRealizada, 170);
            codigoBarraTicket.Save($"{pathBarCode}{folioVentaRealizada}.png");

            var pathBarCodeFull = new Uri($"C:/Archivos PUDVE/Ventas/Tickets/BarCode/{folioVentaRealizada}.png").AbsoluteUri;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                var logoTipo = string.Empty;

                using (DataTable dtLogoTipo = cn.CargarDatos(cs.getLogoTipoUsuario()))
                {
                    if (!dtLogoTipo.Rows.Count.Equals(0))
                    {
                        DataRow drLogoTipoUsuario = dtLogoTipo.Rows[0];
                        logoTipo = drLogoTipoUsuario["LogoTipo"].ToString();
                    }
                }

                if (!string.IsNullOrWhiteSpace(logoTipo))
                {
                    string fileName = logoTipo;
                    string sourcePath = $@"\\{servidor}\Archivos PUDVE\MisDatos\Usuarios\";
                    string targetPath = @"C:\Archivos PUDVE\Ventas\Tickets\BarCode\";

                    // Use Path class to manipulate file and directory paths.
                    string sourceFile = Path.Combine(sourcePath, fileName);
                    string destFile = Path.Combine(targetPath, fileName);
                    finalLogoTipoPath = destFile;

                    // To copy a file to another location and
                    // overwrite the destination file if it already exists.
                    File.Copy(sourceFile, destFile, true);
                }

                path = new Uri($"C:/Archivos PUDVE/Ventas/Tickets/BarCode/").AbsoluteUri;
            }
            else
            {
                path = new Uri("C:/Archivos PUDVE/MisDatos/Usuarios/").AbsoluteUri;
            }

            //01 parametro string para mostrar / ocultar Path Logo
            reportParameters.Add(new ReportParameter("PathLogo", path));

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);
            rdlc.DataSources.Add(rp);
            #endregion

            EnviarImprimir imp = new EnviarImprimir();
            imp.Imprime(rdlc);

            File.Delete($"{pathBarCode}{folioVentaRealizada}.png");
            if (File.Exists(finalLogoTipoPath))
            {
                File.Delete(finalLogoTipoPath);
            }

            this.Close();
        }
    }
}
