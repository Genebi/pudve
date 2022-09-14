using Microsoft.Reporting.WinForms;
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
using PuntoDeVentaV2.ReportesImpresion;

namespace PuntoDeVentaV2
{
    public partial class verEtiquetaDelProductoCodigosDeBarras : Form
    {
        public string NombreDelProducto { get; set; }
        public string PrecioDelProducto { get; set; }
        public string CodigoBarraDelProducto { get; set; }

        string direccionCodigoBarras = string.Empty;

        public verEtiquetaDelProductoCodigosDeBarras()
        {
            InitializeComponent();
        }

        private void verEtiquetaDelProductoCodigosDeBarras_Load(object sender, EventArgs e)
        {
            cargarDatosEtiqueta();
            this.reportViewer1.RefreshReport();
        }

        private void cargarDatosEtiqueta()
        {
            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Etiqueta\BrotherQL800\EtiquetaCodigoDeBarras.rdlc";

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            #region Impresion Etiqueta Codigo de Barras
            string DirectoryImage = string.Empty;

            this.reportViewer1.LocalReport.EnableExternalImages = true;

            ReportParameterCollection reportParameters = new ReportParameterCollection();

            string pathBarCode = $@"C:\Archivos PUDVE\Ventas\Tickets\BarCode\";

            if (!Directory.Exists(pathBarCode))
            {
                Directory.CreateDirectory(pathBarCode);
            }

            var codigoBarraTicket = GenerarCodigoBarras(CodigoBarraDelProducto, 170);
            direccionCodigoBarras = $"{pathBarCode}{CodigoBarraDelProducto}.png";
            codigoBarraTicket.Save(direccionCodigoBarras);

            var pathBarCodeFull = new Uri($"C:/Archivos PUDVE/Ventas/Tickets/BarCode/{CodigoBarraDelProducto}.png").AbsoluteUri;

            //01 parametro integer para mostrar / ocultar Logo
            reportParameters.Add(new ReportParameter("NombreDelProducto", NombreDelProducto.ToString()));
            //02 parametro integer para mostrar / ocultar Nombre
            reportParameters.Add(new ReportParameter("PrecioDelProducto", PrecioDelProducto.ToString()));
            //03 parametro integer para mostrar / ocultar Nombre Comercial
            reportParameters.Add(new ReportParameter("CodigoBarraDelProducto", CodigoBarraDelProducto.ToString()));
            //18 parametro string para mostrar / ocultar Codigo de Barras
            reportParameters.Add(new ReportParameter("pathBarCodeFull", pathBarCodeFull));

            this.reportViewer1.LocalReport.SetParameters(reportParameters);
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
    }
}
