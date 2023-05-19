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
using System.Xml;

namespace PuntoDeVentaV2
{
    public partial class VizualizadorComplementoDePago : Form
    {
        Conexion cn = new Conexion();
        Moneda oMoneda = new Moneda();
        string filePath = "";
        DataTable DTComPago = new DataTable();

        public VizualizadorComplementoDePago(string path)
        {
            InitializeComponent();
            filePath = path;

        }

        private void VizualizadorComplementoDePago_Load(object sender, EventArgs e)
        {
            CargarDatos();
            this.reportViewer1.RefreshReport();
        }

        private void CargarDatos()
        {
            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\ReporteComplementoDePago\ComplementoDePago.rdlc";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            // Obtener el elemento raíz
            XmlElement rootElement = xmlDoc.DocumentElement;


            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("Version", rootElement.GetAttribute("Version")));
            reportParameters.Add(new ReportParameter("Serie", rootElement.GetAttribute("Serie")));
            reportParameters.Add(new ReportParameter("Folio", rootElement.GetAttribute("Folio")));
            reportParameters.Add(new ReportParameter("Fecha", rootElement.GetAttribute("Fecha")));
            reportParameters.Add(new ReportParameter("Sello", rootElement.GetAttribute("Sello")));
            reportParameters.Add(new ReportParameter("NoCertificado", rootElement.GetAttribute("NoCertificado")));
            reportParameters.Add(new ReportParameter("Certificado", rootElement.GetAttribute("Certificado")));
            //reportParameters.Add(new ReportParameter("Sello2", rootElement.GetAttribute("Sello")));
            reportParameters.Add(new ReportParameter("SubTotal", rootElement.GetAttribute("SubTotal")));
            reportParameters.Add(new ReportParameter("Moneda", rootElement.GetAttribute("Moneda")));
            reportParameters.Add(new ReportParameter("Total", rootElement.GetAttribute("Total")));
            reportParameters.Add(new ReportParameter("TipoDeComprobante", rootElement.GetAttribute("TipoDeComprobante")));
            reportParameters.Add(new ReportParameter("Exportacion", rootElement.GetAttribute("Exportacion")));
            reportParameters.Add(new ReportParameter("LugarExpedicion", rootElement.GetAttribute("LugarExpedicion")));


            // Asume que doc es tu XmlDocument
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);

            // Asume que "http://www.sat.gob.mx/cfd/3" es tu espacio de nombres
            namespaceManager.AddNamespace("cfdi", "http://www.sat.gob.mx/cfd/3");

            // Ahora usa el XmlNamespaceManager en tu consulta XPath
            XmlNode emisorNode = xmlDoc.SelectSingleNode("//cfdi:Emisor", namespaceManager);


            XmlNode receptorNode = rootElement.SelectSingleNode("cfdi:Receptor");

            reportParameters.Add(new ReportParameter("RfcEmisor", emisorNode.Attributes["Rfc"].Value));
            reportParameters.Add(new ReportParameter("NombreEmisor", emisorNode.Attributes["Nombre"].Value));
            reportParameters.Add(new ReportParameter("RegimenFiscalEmisor", emisorNode.Attributes["RegimenFiscal"].Value));

            reportParameters.Add(new ReportParameter("RfcReceptor", emisorNode.Attributes["Rfc"].Value));
            reportParameters.Add(new ReportParameter("NombreReceptor", emisorNode.Attributes["Nombre"].Value));
            reportParameters.Add(new ReportParameter("DomicilioFiscalReceptor", emisorNode.Attributes["DomicilioFiscalReceptor"].Value));
            reportParameters.Add(new ReportParameter("RegimenFiscalReceptor", emisorNode.Attributes["RegimenFiscalReceptor"].Value));
             reportParameters.Add(new ReportParameter("UsoCFDI", emisorNode.Attributes["UsoCFDI"].Value));
            reportParameters.Add(new ReportParameter("CreadaPOr", FormPrincipal.userNickName));

            reportParameters.Add(new ReportParameter("ClaveProdServ", emisorNode.Attributes["ClaveProdServ"].Value));

            reportParameters.Add(new ReportParameter("Cantidad", emisorNode.Attributes["Cantidad"].Value));
            reportParameters.Add(new ReportParameter("ClaveUnidad", emisorNode.Attributes["ClaveUnidad"].Value));
            reportParameters.Add(new ReportParameter("Descripcion", emisorNode.Attributes["Descripcion"].Value));
            reportParameters.Add(new ReportParameter("ValorUnitario", emisorNode.Attributes["ValorUnitario"].Value));
            reportParameters.Add(new ReportParameter("Importe", emisorNode.Attributes["Importe"].Value));
            reportParameters.Add(new ReportParameter("ObjetoImp", emisorNode.Attributes["ObjetoImp"].Value));
            reportParameters.Add(new ReportParameter("ClaveProdServ", emisorNode.Attributes["ClaveProdServ"].Value));
            reportParameters.Add(new ReportParameter("NoCertificadoSAT", emisorNode.Attributes["NoCertificadoSAT"].Value));
            reportParameters.Add(new ReportParameter("TipoCambioP", emisorNode.Attributes["TipoCambioP"].Value));
            reportParameters.Add(new ReportParameter("NumParcialidad", emisorNode.Attributes["NumParcialidad"].Value));
            

            string resultado = oMoneda.Convertir(Convert.ToDecimal(rootElement.GetAttribute("Total")).ToString(), true, "PESOS");

            reportParameters.Add(new ReportParameter("NumeroATexto", resultado));

            XmlNodeList doctoRelacionadoNodes = xmlDoc.GetElementsByTagName("pago20:DoctoRelacionado");
            DTComPago.Columns.Add("IdDocumento", typeof(string));
            DTComPago.Columns.Add("Serie", typeof(string));
            DTComPago.Columns.Add("Folio", typeof(string));
            DTComPago.Columns.Add("MonedaDR", typeof(string));
            DTComPago.Columns.Add("EquivalenciaDR", typeof(string));
            DTComPago.Columns.Add("NumParcialidad", typeof(string));
            DTComPago.Columns.Add("ImpSaldoAnt", typeof(string));
            DTComPago.Columns.Add("ImpPagado", typeof(string));
            DTComPago.Columns.Add("ImpSaldoInsoluto", typeof(string));
            DTComPago.Columns.Add("ObjetoImpDR", typeof(string));
            DTComPago.Columns.Add("base", typeof(string));
            DTComPago.Columns.Add("impuesto", typeof(string));
            DTComPago.Columns.Add("tipo_factor", typeof(string));
            DTComPago.Columns.Add("tasa_cuota", typeof(string));
            DTComPago.Columns.Add("importe_impuesto", typeof(string));
            DTComPago.Columns.Add("transladoRetencion", typeof(string));
            int contador = 0;
            foreach (XmlNode doctoRelacionadoNode in doctoRelacionadoNodes)
            {
                DTComPago.Rows.Add();

                DTComPago.Rows[contador][0] = doctoRelacionadoNode.Attributes["IdDocumento"].Value;
                DTComPago.Rows[contador][1] = rootElement.GetAttribute("Serie");
                DTComPago.Rows[contador][2] = doctoRelacionadoNode.Attributes["Folio"].Value;
                DTComPago.Rows[contador][3] = doctoRelacionadoNode.Attributes["MonedaDR"].Value;
                DTComPago.Rows[contador][4] = doctoRelacionadoNode.Attributes["EquivalenciaDR"].Value;
                DTComPago.Rows[contador][5] = doctoRelacionadoNode.Attributes["NumParcialidad"].Value;
                DTComPago.Rows[contador][6] = doctoRelacionadoNode.Attributes["ImpSaldoAnt"].Value;
                DTComPago.Rows[contador][7] = doctoRelacionadoNode.Attributes["ImpPagado"].Value;
                DTComPago.Rows[contador][8] = doctoRelacionadoNode.Attributes["ImpSaldoInsoluto"].Value;
                DTComPago.Rows[contador][9] = doctoRelacionadoNode.Attributes["ObjetoImpDR"].Value;
                using (var dt = cn.CargarDatos($"SELECT base, impuesto, tipo_factor, tasa_cuota, importe_impuesto,es_rt FROM facturas_complemento_pago WHERE id_factura =(SELECT id_factura FROM facturas_complemento_pago WHERE	uuid = '{DTComPago.Rows[contador][0].ToString()}') AND uuid is NULL AND base IS NOT NULL"))
                {
                    DTComPago.Rows[contador][10] = dt.Rows[0][0].ToString();
                    DTComPago.Rows[contador][11] = dt.Rows[0][1].ToString();
                    DTComPago.Rows[contador][12] = dt.Rows[0][2].ToString();
                    DTComPago.Rows[contador][13] = dt.Rows[0][3].ToString();
                    DTComPago.Rows[contador][14] = dt.Rows[0][4].ToString();
                    DTComPago.Rows[contador][15] = dt.Rows[0][5].ToString();
                }

            }


            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);

            ReportDataSource ComplementodePago = new ReportDataSource("DTComPago", DTComPago); 

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.LocalReport.DataSources.Add(ComplementodePago);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();
        }
    }
}
