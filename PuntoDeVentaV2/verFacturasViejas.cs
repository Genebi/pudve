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
        Moneda oMoneda = new Moneda();
        bool esNueva = false;
        bool descarga = false;
        string path = "";
        int factura;
        string certificadoSAT = string.Empty;
        string serie = string.Empty;
        string Folio = string.Empty;
        string usuario_Nombre = string.Empty;

        string usuario_cp = string.Empty;
        string usuario_fono = string.Empty;

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
        string descuento = string.Empty;
        string tasa16 = string.Empty;
        string tasa = string.Empty;
        string total = string.Empty;
        string totalAlfa = string.Empty;
        string certificadoEmisor = string.Empty;
        string RFCPAC = string.Empty;
        string cadenaDigital = string.Empty;
        string selloDigitalSAT = string.Empty;
        string selloDigitalEmisor = string.Empty;
        string Usremail = string.Empty;

        string periodicidad = string.Empty;
        string meses = string.Empty;
        string año = string.Empty;
        string version = "3.3";
        public verFacturasViejas(int facturaC, string certificadoSAT, bool esNueva = false, bool descarga = false, string path = "")
        {
            InitializeComponent();
            factura = facturaC;
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



            DataTable impuestos = new DataTable();

            // Add columns to the DataTable
            impuestos.Columns.Add("Column1", typeof(string));
            impuestos.Columns.Add("Column2", typeof(string));
            impuestos.Columns.Add("Column3", typeof(string));



            DataTable DataTable1 = cn.CargarDatos($"select cantidad AS Cantidad, clave_producto AS ClavePS, clave_unidad AS ClaveU, descripcion as Descripcion, precio_u AS PU, cantidad*(precio_u-importe_iva) AS Importe from facturas_productos WHERE id_factura = {factura}");
            using (DataTable totales = cn.CargarDatos($"select tasa_cuota, sum(cantidad*(precio_u-importe_iva)) as monosas from facturas_productos WHERE id_factura = {factura}"))
            {
                subtotal = totales.Rows[0]["monosas"].ToString();
            }
            using (DataTable totalTT = cn.CargarDatos($"select total from facturas where id = {factura}"))
            {
                total = totalTT.Rows[0]["total"].ToString();
            }

            using (DataTable totales = cn.CargarDatos($"select sum(descuento) as monosas from facturas_productos WHERE id_factura = {factura}"))
            {
                descuento = totales.Rows[0]["monosas"].ToString();
            }

            decimal subtotalValue, descuentoValue;

            if (decimal.TryParse(subtotal, out subtotalValue))
            {
                subtotal = String.Format("{0:0.00}", subtotalValue);
            }

            impuestos.Rows.Add("", "SubTotal", subtotal);

            if (decimal.TryParse(descuento, out descuentoValue))
            {
                descuento = String.Format("{0:0.00}", descuentoValue);
                if (descuentoValue>0)
                {
                    impuestos.Rows.Add("", "Descuento", descuento);
                }
            }

            totalAlfa = oMoneda.Convertir(total, true, "PESOS");
            
            using (DataTable aaaaa = cn.CargarDatos($"SELECT tasa_cuota,SUM(importe_iva) as importe FROM facturas_productos WHERE id_factura = {factura} GROUP BY tasa_cuota"))
            {
                foreach (DataRow data in aaaaa.Rows)
                {
                    impuestos.Rows.Add("Traslado IVA", "Tasa "+data["tasa_cuota"], String.Format("{0:0.00}", decimal.Parse(data["importe"].ToString())));
                }
            }

            using (DataTable todosImpuestos = cn.CargarDatos($@"SELECT CONCAT(tipo,' ',impuesto) as impuesto, IF(facturas_impuestos.tasa_cuota = 'Definir %',CONCAT(facturas_impuestos.definir, '%'), facturas_impuestos.tasa_cuota) AS tasa_cuota, SUM( importe ) AS total_importe FROM facturas_impuestos INNER JOIN facturas_productos ON facturas_impuestos.id_factura_producto = facturas_productos.id WHERE facturas_productos.id_factura = {factura} GROUP BY impuesto,tasa_cuota"))
            {
                foreach (DataRow data in todosImpuestos.Rows)
                {
                    impuestos.Rows.Add(data["Impuesto"].ToString(), "Tasa: " + data["tasa_cuota"].ToString(), data["total_importe"].ToString());
                }
            }
            impuestos.Rows.Add(totalAlfa, "Total", total);



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
            e_correo,
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
            e_calle,
            e_num_ext,
            e_estado,
            e_municipio,
            e_cp,
            e_telefono,
            e_colonia,r_colonia,
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
                Usremail = datos.Rows[0]["e_correo"].ToString();
                usuario_cp = $"{datos.Rows[0]["e_calle"].ToString()},{datos.Rows[0]["e_num_ext"].ToString()}, Col.{datos.Rows[0]["e_colonia"].ToString()},Cp.{datos.Rows[0]["e_cp"].ToString()},{datos.Rows[0]["e_municipio"].ToString()},{datos.Rows[0]["e_estado"].ToString()}";
                usuario_fono = datos.Rows[0]["e_telefono"].ToString();

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

                selloDigitalSAT = datos.Rows[0]["selloDigitalSAT"].ToString();
                selloDigitalEmisor = datos.Rows[0]["selloDigitalEmisor"].ToString();

                cadenaDigital = $"||1.1|{cliente_FolioFiscal}|{fechaCertificacion}|{selloDigitalEmisor}|{certificadoSAT}|";

                if (esNueva)
                {
                    version = "4.0";
                    if (!string.IsNullOrEmpty(datos.Rows[0]["r_periodicidad_infog"].ToString()))
                    {
                        periodicidad = "Periodicidad: " + datos.Rows[0]["r_periodicidad_infog"].ToString();
                        meses = "Meses: " + datos.Rows[0]["r_meses_infog"].ToString();
                        año = "Año: " + datos.Rows[0]["r_anio_infog"].ToString();
                    }
                }
            }

            //imagen
            string pathLogoImage;
            string ruta_archivos_guadados;
            string DireccionLogo = string.Empty;
            var servidor = Properties.Settings.Default.Hosting;
            string saveDirectoryImg = @"C:\Archivos PUDVE\MisDatos\Usuarios\";

            using (DataTable ConsultaLogo = cn.CargarDatos(cs.buscarNombreLogoTipo2(FormPrincipal.userID)))
            {
                if (!ConsultaLogo.Rows.Count.Equals(0))
                {
                    string Logo = ConsultaLogo.Rows[0]["Logo"].ToString();
                    if (!Logo.Equals(""))
                    {

                        if (!Directory.Exists(saveDirectoryImg))    // verificamos que si no existe el directorio
                        {
                            Directory.CreateDirectory(saveDirectoryImg);    // lo crea para poder almacenar la imagen
                        }
                        if (!string.IsNullOrWhiteSpace(servidor))
                        {
                            // direccion de la carpeta donde se va poner las imagenes
                            pathLogoImage = new Uri($@"\\{servidor}\Archivos PUDVE\MisDatos\Usuarios\").AbsoluteUri;
                            // ruta donde estan guardados los archivos digitales
                            ruta_archivos_guadados = $@"\\{servidor}\Archivos PUDVE\MisDatos\CSD_{Logo}\";

                            DireccionLogo = pathLogoImage + Logo;
                        }
                        else
                        {
                            // direccion de la carpeta donde se va poner las imagenes
                            pathLogoImage = new Uri($"C:/Archivos PUDVE/MisDatos/Usuarios/").AbsoluteUri;
                            // ruta donde estan guardados los archivos digitales
                            ruta_archivos_guadados = $@"C:\Archivos PUDVE\MisDatos\CSD_{Logo}\";

                            DireccionLogo = pathLogoImage + Logo;

                        }
                    }
                }
            }
            //imagen



            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("Logo", DireccionLogo));
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

            reportParameters.Add(new ReportParameter("usuario_CP", usuario_cp));
            reportParameters.Add(new ReportParameter("usuario_fono", usuario_fono));

            reportParameters.Add(new ReportParameter("Periodicidad", periodicidad));
            reportParameters.Add(new ReportParameter("Meses", meses));
            reportParameters.Add(new ReportParameter("Año", año));
            reportParameters.Add(new ReportParameter("Version", version));
            reportParameters.Add(new ReportParameter("tasa", tasa));

            reportParameters.Add(new ReportParameter("usuario_Email", "Email: " + Usremail));

            string qr = QR();
            reportParameters.Add(new ReportParameter("qr", qr));

            LocalReport rdlc = new LocalReport();

            rdlc.EnableExternalImages = true;
            rdlc.ReportPath = FullReportPath;

            rdlc.SetParameters(reportParameters);

            this.reportViewer1.ProcessingMode = ProcessingMode.Local;
            this.reportViewer1.LocalReport.ReportPath = FullReportPath;
            this.reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource ReporteVentas = new ReportDataSource("DataTable1", DataTable1);
            ReportDataSource inpuestos = new ReportDataSource("impuestos", impuestos);

            this.reportViewer1.ZoomMode = ZoomMode.PageWidth;
            this.reportViewer1.LocalReport.DataSources.Add(ReporteVentas);
            this.reportViewer1.LocalReport.DataSources.Add(inpuestos);
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

        public string QR()
        {
            byte[] qr = Genera_QR.createBarCode("https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx?id=" + cliente_FolioFiscal + "&re=" + usuario_RFC + "&rr=" + cliente_RFC + "&tt=" + total + "&fe=" + selloDigitalEmisor.Substring(selloDigitalEmisor.Length - 9, 8));
            string b64qr = Convert.ToBase64String(qr);
            string sqr = string.Format("data:image/png;base64,{0}", b64qr);
            sqr = sqr.Replace("data:image/png;base64,", "");
            return sqr;
        }

    }
}

