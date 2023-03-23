using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class verFacturasViejas : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        bool esNueva=false;
        bool descarga = false;
        string path = "";
        int factura;
        string certificadoSAT = string.Empty;
        public verFacturasViejas(int facturaC, string certificadoSAT, bool esNueva = false, bool descarga = false, string path="")
        {
            InitializeComponent();
            factura =facturaC;
            this.esNueva = esNueva;
            this.descarga = descarga;
            this.path = path;
            this.certificadoSAT = certificadoSAT;
        }

        private void VisualizadorReporteVentas_Load(object sender, EventArgs e)
        {
            CargarRDLC();
            this.reportViewer1.RefreshReport();
        }

       
        private void CargarRDLC()
        {

            string pathApplication = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string FullReportPath = $@"{pathApplication}\ReportesImpresion\Ticket\FacturasViejas\FactuasViejas.rdlc";

            string serie = string.Empty;
            string Folio = string.Empty;
            string usuario_Nombre = string.Empty;
            string usuario_RFC = string.Empty;
            string usuario_Regimen = string.Empty;
            string fechaEmision = string.Empty;
            string fechaCertificacion = string.Empty;
            string tipoComprobante = string.Empty;
            string cliente_Nombre = string.Empty;
            string cliente_RFC = string.Empty;
            string cliente_CFDI = string.Empty;
            string cliente_Locacion = string.Empty;
            string cliente_FolioFiscal = string.Empty;
            string cliente_LugarExpedicion = string.Empty;
            string cliente_MetodoPago = string.Empty;
            string cliente_FormaPago = string.Empty;
            string cliente_Moneda = string.Empty;
            string cliente_TipoCambio = string.Empty;
            string subtotal = string.Empty;
            string tasa16 = string.Empty;
            string total = string.Empty;
            string totalAlfa = string.Empty;
            string certificadoEmisor = string.Empty;
            string RFCPAC = string.Empty;
            string cadenaDigital = string.Empty;
            string selloDigitalSAT = string.Empty;
            string selloDigitalEmisor = string.Empty;

            string periodicidad = string.Empty;
            string meses = string.Empty;
            string año = string.Empty;
            string version = "3.3";

            DataTable DataTable1 = cn.CargarDatos($"select cantidad AS Cantidad, clave_producto AS ClavePS, clave_unidad AS ClaveU, descripcion as Descripcion, precio_u AS PU, cantidad*(precio_u-importe_iva) AS Importe from facturas_productos WHERE id_factura = {factura}");
            using (DataTable totales = cn.CargarDatos($"select sum(cantidad*(precio_u-importe_iva)) as monosas, SUM(cantidad*(precio_u-importe_iva)*16/100) as sincho, sum(cantidad*(precio_u-importe_iva)) + SUM(cantidad*(precio_u-importe_iva)*16/100) AS kowka from facturas_productos WHERE id_factura = {factura}"))
            {
                subtotal = totales.Rows[0]["monosas"].ToString();
                tasa16 = totales.Rows[0]["sincho"].ToString();
                total = totales.Rows[0]["kowka"].ToString();
                

                decimal subtotalValue, tasa16Value, totalValue;

                if (decimal.TryParse(subtotal, out subtotalValue))
                {
                    subtotal = String.Format("{0:0.00}", subtotalValue);
                }

                if (decimal.TryParse(tasa16, out tasa16Value))
                {
                    tasa16 = String.Format("{0:0.00}", tasa16Value);
                }

                if (decimal.TryParse(total, out totalValue))
                {
                    total = String.Format("{0:0.00}", totalValue);
                }

                //totalAlfa = NumberToWords(decimal.Parse(total));

            }

            string segs = $@"SELECT
	        ventas.Folio,
	        ventas.Serie,
	        Usuarios.NombreCompleto AS usuario_Nombre,
	        usuarios.rfc AS usuario_RFC,
	        usuarios.Regimen AS usuario_Regimen,
            metodo_pago AS cliente_MetodoPago,
	        ventas.FechaOperacion AS fechaEmision,
	        fecha_certificacion AS fechaCertificacion,
	        facturas.r_razon_social AS cliente_Nombre,
	        r_rfc AS cliente_RFC,
	        uso_cfdi AS cliente_CFDI,
	        r_localidad AS cliente_Locacion,
	        r_telefono,
            r_regimen,
            exportacion,
            r_periodicidad_infog,
            r_meses_infog,
            r_anio_infog,
	        UUID as cliente_FolioFiscal,
	        e_cp AS cliente_LugarExpedicion,
	        forma_pago AS cliente_FormaPago,
	        facturas.moneda AS cliente_Moneda,
	        facturas.tipo_cambio AS cliente_TipoCambio,
	        num_certificado AS certificadoEmisor,
	        rfc_pac AS RFCPAC,
	        sello_cfd AS selloDigitalEmisor,
            r_calle,
            r_num_ext,
            r_pais,
            r_estado,
            r_municipio,
            r_localidad,
            r_cp,
            r_colonia,
	        sello_sat AS selloDigitalSAT
            FROM
	        facturas
	        JOIN ventas ON ventas.ID = facturas.id_venta
	        JOIN clientes ON ventas.IDCliente = clientes.id
	        JOIN usuarios ON ventas.IDUsuario = usuarios.id 
            WHERE facturas.id = {factura}";

            using (DataTable datos = cn.CargarDatos(segs))
            {
                serie = datos.Rows[0]["serie"].ToString();
                 Folio = datos.Rows[0]["Folio"].ToString();
                usuario_Nombre = datos.Rows[0]["usuario_Nombre"].ToString();
                usuario_RFC = datos.Rows[0]["usuario_RFC"].ToString();
                usuario_Regimen = datos.Rows[0]["usuario_Regimen"].ToString();
                fechaEmision = datos.Rows[0]["fechaEmision"].ToString();
                fechaCertificacion = datos.Rows[0]["fechaCertificacion"].ToString();
                //tipoComprobante = datos.Rows[0]["tipoComprobante"].ToString();
                cliente_Nombre = datos.Rows[0]["cliente_Nombre"].ToString(); 
                 cliente_RFC = datos.Rows[0]["cliente_RFC"].ToString();
                cliente_CFDI = datos.Rows[0]["cliente_CFDI"].ToString();
                cliente_Locacion = $"{datos.Rows[0]["r_calle"].ToString()},{datos.Rows[0]["r_num_ext"].ToString()}, Col.{datos.Rows[0]["r_colonia"].ToString()},Cp.{datos.Rows[0]["r_cp"].ToString()},{datos.Rows[0]["r_localidad"].ToString()},{datos.Rows[0]["r_municipio"].ToString()},{datos.Rows[0]["r_estado"].ToString()},{datos.Rows[0]["r_pais"].ToString()}";
                cliente_FolioFiscal = datos.Rows[0]["cliente_FolioFiscal"].ToString();
                cliente_LugarExpedicion = datos.Rows[0]["cliente_LugarExpedicion"].ToString();
                cliente_MetodoPago = datos.Rows[0]["cliente_MetodoPago"].ToString();
                cliente_FormaPago = datos.Rows[0]["cliente_FormaPago"].ToString();
                cliente_Moneda = datos.Rows[0]["cliente_Moneda"].ToString();
                cliente_TipoCambio = datos.Rows[0]["cliente_TipoCambio"].ToString();
                
                certificadoEmisor = datos.Rows[0]["certificadoEmisor"].ToString();
                RFCPAC = datos.Rows[0]["RFCPAC"].ToString();
                //cadenaDigital = datos.Rows[0]["cadenaDigital"].ToString();
                selloDigitalSAT = datos.Rows[0]["selloDigitalSAT"].ToString();
                selloDigitalEmisor = datos.Rows[0]["selloDigitalEmisor"].ToString();

                if (esNueva)
                {
                    cliente_Locacion = "CP: "+datos.Rows[0]["r_cp"].ToString();
                    periodicidad = datos.Rows[0]["r_periodicidad_infog"].ToString();
                    meses = datos.Rows[0]["r_meses_infog"].ToString();
                    año = datos.Rows[0]["r_anio_infog"].ToString();
                    version = "4.0";
                }
            }

            

            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("Serie", serie));
            reportParameters.Add(new ReportParameter("Folio", Folio));
            reportParameters.Add(new ReportParameter("usuario_Nombre", usuario_Nombre));
            reportParameters.Add(new ReportParameter("usuario_RFC", usuario_RFC));
            reportParameters.Add(new ReportParameter("usuario_Regimen", usuario_Regimen));
            reportParameters.Add(new ReportParameter("fechaEmision", fechaEmision));
            reportParameters.Add(new ReportParameter("fechaCertificacion", fechaCertificacion));
            reportParameters.Add(new ReportParameter("tipoComprobante", "Ingresos"));
            reportParameters.Add(new ReportParameter("cliente_Nombre", cliente_Nombre));
            reportParameters.Add(new ReportParameter("cliente_RFC", cliente_RFC));
            reportParameters.Add(new ReportParameter("cliente_CFDI", cliente_CFDI));
            reportParameters.Add(new ReportParameter("cliente_Locacion", cliente_Locacion));
            reportParameters.Add(new ReportParameter("cliente_FolioFiscal", cliente_FolioFiscal));
            reportParameters.Add(new ReportParameter("cliente_NoComprobante", Folio));
            reportParameters.Add(new ReportParameter("cliente_LugarExpedicion", cliente_LugarExpedicion));
            reportParameters.Add(new ReportParameter("cliente_MetodoPago", cliente_MetodoPago));
            reportParameters.Add(new ReportParameter("cliente_FormaPago", cliente_FormaPago));
            reportParameters.Add(new ReportParameter("cliente_Moneda", cliente_Moneda));
            reportParameters.Add(new ReportParameter("cliente_TipoCambio", cliente_TipoCambio));
            reportParameters.Add(new ReportParameter("subtotal", subtotal));
            reportParameters.Add(new ReportParameter("tasa16", tasa16));
            reportParameters.Add(new ReportParameter("total", total));
            reportParameters.Add(new ReportParameter("totalAlfa", totalAlfa));
            reportParameters.Add(new ReportParameter("certificadoEmisor", certificadoEmisor));
            reportParameters.Add(new ReportParameter("certificadoSAT", certificadoSAT));
            reportParameters.Add(new ReportParameter("RFCPAC", RFCPAC));
            reportParameters.Add(new ReportParameter("cadenaDigital", cadenaDigital));
            reportParameters.Add(new ReportParameter("selloDigitalEmisor", selloDigitalEmisor));
            reportParameters.Add(new ReportParameter("selloDigitalSAT", selloDigitalSAT));

            reportParameters.Add(new ReportParameter("Periodicidad", periodicidad));
            reportParameters.Add(new ReportParameter("Meses", meses));
            reportParameters.Add(new ReportParameter("Año", año));
            reportParameters.Add(new ReportParameter("Version", version));

            LocalReport rdlc = new LocalReport();
            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;
            rdlc.SetParameters(reportParameters);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource ReporteVentas = new ReportDataSource("DataTable1", DataTable1);

            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.LocalReport.DataSources.Add(ReporteVentas);
            this.reportViewer1.LocalReport.EnableExternalImages = true;
            this.reportViewer1.LocalReport.SetParameters(reportParameters);
            this.reportViewer1.RefreshReport();


            if (descarga)
            {
                //Este pinche codigo asi de sencillo asi de bonito neta no salia, gpt no lo pudo hacer, no estaba en ningun lugar de google tampoco. Me lo tuve que pepenar de lo mas recondito de un foro indio jajajaj https://media.tenor.com/ThX4z7W4s6IAAAAd/fr-fr-ong.gif
                byte[] Bytes = this.reportViewer1.LocalReport.Render(format: "PDF", deviceInfo: @"
            <DeviceInfo><EmbedFonts>None</EmbedFonts></DeviceInfo>
            ");

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    stream.Write(Bytes, 0, Bytes.Length);
                }
                this.Close();
            }
            

        }

        
    }
}

