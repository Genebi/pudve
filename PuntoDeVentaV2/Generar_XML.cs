using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.ServiceModel;
using System.Diagnostics;
using System.Net;
//using PuntoDeVentaV2.ServiceReferenceTPrueba;
//using PuntoDeVentaV2.ServiceReference_produccion;


namespace PuntoDeVentaV2
{
    class Generar_XML
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public static int con_complemento_pg = 0;



        public string obtener_datos_XML(int id_f, int id_v, int con_cpg, decimal[][] arr_idf_principal_pago)
        {
            // ID
            int id_factura = id_f;
            int id_venta = id_v;
            con_complemento_pg = con_cpg;
            int id_usuario = Convert.ToInt32(FormPrincipal.userID);
            // Variables para los archivos
            string ruta_carpeta_archivos = @"C:\Archivos PUDVE\MisDatos\CSD\";
            var servidor = Properties.Settings.Default.Hosting;
            string ruta_cer = "";
            string ruta_key = "";
            string clave_privada = "";
            string numero_certificado = "";
            DataTable datos_cer;
            DataRow r_datos_cer;
            // Facturas
            DataTable d_facturas;
            DataRow r_facturas;
            string folio = "",            serie = "";
            string moneda = "";
            string forma_pago = "",       tipo_cambio = "";     
            string metodo_pago = "",      lugar_expedicion = "";
            string tipo_comprobante = "",  exportacion = ""; 
            // Emisor
            string rfc_e = "";
            string nombre_e = "";
            string regimen_e = "";
            // Receptor
            string rfc_r = "";
            string nombre_r = "";
            string uso_cfdi = "";
            string regimen_r = "";
            string domicilio_fiscal = "";
            // Información global
            string periodicidad_ct = "", meses_ct = "", anio_ct = "";
            // Productos
            DataTable d_productos;
            //DataTable d_concepto_impuesto_t;
            DataTable d_concepto_impuesto_td;
            DataTable d_concepto_impuesto_r;
            List<string> list_porprod_impuestos_trasladados = new List<string>();
            List<string> list_impuestos_local_traslado = new List<string>();
            List<string> list_impuestos_local_retenido = new List<string>();
            decimal suma_total_productos = 0; // Total de todos los productos
            decimal total_ISR = 0;
            decimal total_IVA = 0;
            decimal total_IEPS = 0;
            int agrega_nodo_concepto_traslado = 0;
            int agrega_nodo_concepto_retencion = 0;
            // Totales generales
            decimal descuento_general = 0;
            decimal suma_impuesto_traslado = 0;
            decimal suma_impuesto_retenido = 0;
            string mensaje = "";
            // Complemento de pago
            string pg_fecha_pago = "";
            decimal monto_cpago = 0;
            bool cambia_nombre_carpeta = false;




            // .................................
            // .   Consultar venta a timbrar   .
            // .................................


            // Datos del certificado

            datos_cer = cn.CargarDatos(cs.cargar_info_certificado(FormPrincipal.userID.ToString()));

            if(datos_cer.Rows.Count > 0)
            {
                r_datos_cer = datos_cer.Rows[0];

                numero_certificado = r_datos_cer["num_certificado"].ToString();
                clave_privada = r_datos_cer["password_cer"].ToString();
            }

            // Consulta datos de tabla facturas
            // Se obtiene emisor, receptor y otros datos

            d_facturas = cn.CargarDatos(cs.cargar_datos_venta_xml(1, id_factura, id_usuario));

            if(d_facturas.Rows.Count > 0)
            {
                r_facturas = d_facturas.Rows[0];

                folio = r_facturas["folio"].ToString();
                serie = r_facturas["serie"].ToString();
                forma_pago = r_facturas["forma_pago"].ToString();
                moneda = r_facturas["moneda"].ToString();
                tipo_cambio = r_facturas["tipo_cambio"].ToString();
                metodo_pago = r_facturas["metodo_pago"].ToString();
                tipo_comprobante = r_facturas["tipo_comprobante"].ToString();
                lugar_expedicion = r_facturas["e_cp"].ToString();
                exportacion = r_facturas["exportacion"].ToString();

                rfc_e = r_facturas["e_rfc"].ToString();
                nombre_e = r_facturas["e_razon_social"].ToString();
                regimen_e = Convert.ToString(cn.EjecutarSelect($"SELECT CodigoRegimen FROM RegimenFiscal WHERE Descripcion='{r_facturas["e_regimen"]}'", 12));

                rfc_r = r_facturas["r_rfc"].ToString();
                nombre_r = r_facturas["r_razon_social"].ToString();
                uso_cfdi = r_facturas["uso_cfdi"].ToString();
                regimen_r = r_facturas["r_regimen"].ToString();
                domicilio_fiscal = r_facturas["r_cp"].ToString();

                // Información global

                if(rfc_r == "XAXX010101000" & nombre_r == "PUBLICO EN GENERAL")
                {
                    periodicidad_ct = r_facturas["r_periodicidad_infog"].ToString();
                    meses_ct = r_facturas["r_meses_infog"].ToString();
                    anio_ct = r_facturas["r_anio_infog"].ToString();
;               }

                // Solo cuando es un complemento de pago

                if (con_complemento_pg == 1)
                {
                    var f_pg = r_facturas["fecha_hora_cpago"].ToString();
                    string[] fech = f_pg.Split(' ');
                    pg_fecha_pago = Convert.ToDateTime(fech[0]).ToString("yyyy-MM-dd") + "T" + fech[1];

                    monto_cpago = Convert.ToDecimal(r_facturas["monto_cpago"]);
                }
            }





            // ..................................
            // .   Obtener archivos digitales   .
            // ..................................

            // Obtiene el número de usuarios. 

            DataTable dt_usuarios = cn.CargarDatos($"SELECT ID, Usuario FROM usuarios WHERE ID = '{FormPrincipal.userID}'");
            int tam_usuarios = dt_usuarios.Rows.Count;
            DataRow dr_usuarios = dt_usuarios.Rows[0];

            if (tam_usuarios >= 1)
            {
                

                if (dr_usuarios["Usuario"].ToString() == FormPrincipal.userNickName)
                {
                    // Verifica si existe la carpeta CSD.
                    // Si la carpeta CSD no existe, entonces se deberá modificar la ruta de acceso a los archivos CSD.

                    if (!Directory.Exists(ruta_carpeta_archivos))
                    {
                        cambia_nombre_carpeta = true;
                        ruta_carpeta_archivos = @"C:\Archivos PUDVE\MisDatos\CSD_" + dr_usuarios["Usuario"].ToString() + @"\"; 
                    }
                }
                else
                {
                    cambia_nombre_carpeta = true;
                    ruta_carpeta_archivos = @"C:\Archivos PUDVE\MisDatos\CSD_" + dr_usuarios["Usuario"].ToString() + @"\";
                }
            }

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                ruta_carpeta_archivos = $@"\\{servidor}\Archivos PUDVE\MisDatos\CSD\";

                if(cambia_nombre_carpeta == true)
                {
                    ruta_carpeta_archivos = $@"\\{servidor}\Archivos PUDVE\MisDatos\CSD_" + dr_usuarios["Usuario"].ToString() + @"\";
                }
            }

            if (Directory.Exists(ruta_carpeta_archivos))
            {
                DirectoryInfo dir = new DirectoryInfo(ruta_carpeta_archivos);

                foreach (var arch in dir.GetFiles())
                {
                    // Obtiene extención del archivo
                    string extencion = arch.Name.Substring(arch.Name.Length - 4, 4);

                    if(extencion == ".cer")
                    {
                        ruta_cer = ruta_carpeta_archivos + arch.Name;
                    }
                    if (extencion == ".key")
                    {
                        ruta_key = ruta_carpeta_archivos + arch.Name;
                    }
                }
            }




            // ................................
            // .   Inicia formación del XML   .
            // ................................

            Comprobante comprobante = new Comprobante();


            // NODO EMISOR
            //------------

            ComprobanteEmisor emisor = new ComprobanteEmisor();
            emisor.Rfc = rfc_e;
            emisor.Nombre = nombre_e;
            emisor.RegimenFiscal = regimen_e;


            // NODO RECEPTOR
            //--------------

            ComprobanteReceptor receptor = new ComprobanteReceptor();
            receptor.Rfc = rfc_r;
            receptor.Nombre = nombre_r;
            receptor.DomicilioFiscalReceptor = domicilio_fiscal;
            receptor.RegimenFiscalReceptor = regimen_r;
            receptor.UsoCFDI = uso_cfdi;


            // NODO INFORMACIÓN GLOBAL
            //------------------------

            if (rfc_r == "XAXX010101000" & nombre_r == "PUBLICO EN GENERAL")
            {
                ComprobanteInformacionGlobal infog = new ComprobanteInformacionGlobal();

                infog.Periodicidad = periodicidad_ct;
                infog.Meses = meses_ct;
                infog.Año = anio_ct;

                comprobante.InformacionGlobal = infog;
            }

            
            comprobante.Emisor = emisor;
            comprobante.Receptor = receptor;



            // NODO CONCEPTOS
            //---------------

            List<ComprobanteConcepto> listaConceptos = new List<ComprobanteConcepto>();
            
            // Consulta lista de productos en tabla Facturas_productos

            d_productos = cn.CargarDatos(cs.cargar_datos_venta_xml(10, id_factura, id_usuario));

            if(d_productos.Rows.Count > 0)
            {
                foreach (DataRow r_productos in d_productos.Rows)
                {
                    ComprobanteConcepto concepto = new ComprobanteConcepto();

                    string d_tasa_c = "";
                    decimal d_base_i = 0, d_imp_iva = 0, cantidad = 0;
                    decimal descuento_xproducto_xml = 0;
                    decimal precio_unitario_xml = 0;

                    int id_producto = Convert.ToInt32(r_productos["ID"]);
                    decimal precio_unitario = Convert.ToDecimal(r_productos["precio_u"]);
                    decimal cantidad_xproducto_xml = Convert.ToDecimal(r_productos["cantidad"]);
                    string incluye_impuestos = r_productos["incluye_impuestos"].ToString();
                    
   
                    if (con_complemento_pg == 0 & incluye_impuestos == "02")
                    {
                        d_base_i = Convert.ToDecimal(r_productos["base"]);
                        d_tasa_c = r_productos["tasa_cuota"].ToString();
                        d_imp_iva = Convert.ToDecimal(r_productos["importe_iva"]);
                        cantidad = seis_decimales(Convert.ToDecimal(r_productos["cantidad"]));
                    }

                    precio_unitario_xml = d_base_i;

                    // Obtención del: 
                    //  - Descuento
                    //  - Base
                    string des = r_productos["descuento"].ToString();

                    if (des != "" & des != "0" & des != "0.00")
                    {
                        var desc = (r_productos["descuento"].ToString()).IndexOf("%");
                       
                        if (desc > -1)
                        {
                            // Descuento en porcentaje

                            string d = r_productos["descuento"].ToString();
                            string porcentaje = d.Substring(0, (d.Length - 1));
                            decimal p = Convert.ToDecimal(porcentaje);

                            if (Convert.ToDecimal(porcentaje) > 1)
                            {
                                p = Convert.ToDecimal(porcentaje) / 100;
                            }

                            decimal descuento_xunidad = seis_decimales(precio_unitario) * p;
                            descuento_xproducto_xml = descuento_xunidad * cantidad_xproducto_xml;
                            precio_unitario_xml = d_base_i + descuento_xunidad;

                            var x = Convert.ToString(descuento_xproducto_xml).IndexOf(".");
                            if (x <= -1)
                            {
                                string idsc = Convert.ToString(descuento_xproducto_xml) + ".000000";
                                descuento_xproducto_xml = Convert.ToDecimal(idsc);
                            }
                        }
                        else
                        {
                            // Descuento en cantidad $

                            descuento_xproducto_xml = Convert.ToDecimal(r_productos["descuento"].ToString());
                            decimal descuento_xunidad = descuento_xproducto_xml / cantidad_xproducto_xml;
                            precio_unitario_xml = d_base_i + descuento_xunidad;

                            var x = Convert.ToString(descuento_xproducto_xml).IndexOf(".");
                            if (x <= -1)
                            {
                                string idsc = Convert.ToString(descuento_xproducto_xml) + ".000000";
                                descuento_xproducto_xml = Convert.ToDecimal(idsc);
                            }
                        }
                    }


                    // Agrega datos al nodo concepto

                    concepto.ClaveProdServ = r_productos["clave_producto"].ToString();
                    concepto.Cantidad = seis_decimales(cantidad_xproducto_xml);
                    concepto.ClaveUnidad = r_productos["clave_unidad"].ToString();
                    concepto.Descripcion = r_productos["descripcion"].ToString();

                    // Precio unitario e importe
                    decimal importe_p = cantidad_xproducto_xml * precio_unitario_xml;
                    
                    if (con_complemento_pg == 0 & incluye_impuestos == "02")
                    {
                        var x = Convert.ToString(importe_p).IndexOf(".");
                        if (x <= -1)
                        {
                            string ip = Convert.ToString(importe_p) + ".000000";
                            importe_p = Convert.ToDecimal(ip);
                        }

                        concepto.ValorUnitario = seis_decimales(precio_unitario_xml);
                        concepto.Importe = seis_decimales(importe_p);
                    }
                    if (con_complemento_pg == 0 & (incluye_impuestos == "01" | incluye_impuestos == "03" | incluye_impuestos == "04"))
                    {
                        importe_p = cantidad_xproducto_xml * precio_unitario;

                        concepto.ValorUnitario = seis_decimales(precio_unitario);
                        concepto.Importe = seis_decimales(importe_p);
                    }
                    if (con_complemento_pg == 1)
                    {
                        concepto.ValorUnitario = 0;
                        concepto.Importe = 0;
                    }

                    // Descuento
                    if(descuento_xproducto_xml > 0)
                    {
                        concepto.Descuento = seis_decimales(descuento_xproducto_xml);
                    }

                    // Objeto de impuestos
                    concepto.ObjetoImp = incluye_impuestos;


                    suma_total_productos += importe_p;
                    descuento_general += seis_decimales(descuento_xproducto_xml);




                    // Agrega el nodo impuestos para el concepto solo si:
                    //  - es diferente de un complemento de pago
                    //  - el producto incluye impuestos

                    if (con_complemento_pg == 0 & incluye_impuestos == "02")
                    {

                        // NODO IMPUESTOS TRASLADADOS POR PRODUCTO
                        //----------------------------------------


                        List<ComprobanteConceptoImpuestosTraslado> list_concepto_impuestos_traslados = new List<ComprobanteConceptoImpuestosTraslado>();


                        // Consulta impuestos trasladados  

                        // Se agrega primero uno de los siguientes impuestos; 16, 8, 0 porciento                  
                        //d_concepto_impuesto_t = cn.CargarDatos(cs.cargar_datos_venta_xml(5, id_producto, 0));

                        decimal importe_base = 0;
                        decimal base_total_xprod = 0;

                        if (d_base_i > 0 & d_tasa_c != "" & d_imp_iva >= 0)
                        {
                            //DataRow r_concepto_impuesto_t = d_concepto_impuesto_t.Rows[0];
                            decimal tasacuota = 0;
                            decimal importe = 0;
                            string tipo_factor = "Tasa";
                            agrega_nodo_concepto_traslado = 1;


                            importe_base = d_base_i;
                            

                            if (d_tasa_c == "16%") { tasacuota = 0.160000m; }
                            if (d_tasa_c == "8%") { tasacuota = 0.080000m; }
                            if (d_tasa_c == "0%" | d_tasa_c == "Exento")
                            {
                                tasacuota = 0.000000m;

                                if (d_tasa_c == "Exento")
                                {
                                    tipo_factor = "Exento";
                                }
                            }
                            

                            ComprobanteConceptoImpuestosTraslado concepto_traslado = new ComprobanteConceptoImpuestosTraslado();

                             
                            concepto_traslado.Base = seis_decimales(importe_base * cantidad);
                            concepto_traslado.Impuesto = "002";
                            concepto_traslado.TipoFactor = tipo_factor;

                            


                            if (tipo_factor != "Exento")
                            {
                                concepto_traslado.TasaOCuota = tasacuota;

                                // Importe
                                if(cantidad == 1)
                                {
                                    importe = d_imp_iva; // * cantidad;
                                }
                                else
                                {
                                    importe = (importe_base * cantidad) * tasacuota;
                                }

                                // Verifica que el importe se encuentre dentro de los limites
                                decimal env_base = seis_decimales(importe_base * cantidad);
                                string[] res = calculo_limites(env_base, 6, tasacuota, seis_decimales(importe));
                                
                                if (res[0] == "true")
                                {
                                    concepto_traslado.Importe = seis_decimales(importe);
                                }
                                else
                                {
                                    decimal nuevo_importe= recalcular_datos_impuestos(id_producto, seis_decimales(importe), cantidad, res[1], res[2]);

                                    concepto_traslado.Importe = nuevo_importe;
                                    importe = nuevo_importe;
                                }

                                base_total_xprod = importe_base * cantidad;
                            }

                            list_concepto_impuestos_traslados.Add(concepto_traslado);


                            // Guarda en la lista el tipo de impuesto

                            string cadena = "002-" + tipo_factor + "-" + tasacuota;

                            // Busca si la cadena existe en la lista
                            var indice = list_porprod_impuestos_trasladados.IndexOf(cadena);

                            // Si la cadena existe aumenta el importe del impuesto, de lo contrario la agrega como nueva
                            if (indice >= 0)
                            {
                                indice = indice + 1;

                                decimal monto_actual = Convert.ToDecimal(list_porprod_impuestos_trasladados[indice]);
                                decimal monto_nuevo = monto_actual + seis_decimales(importe);

                                decimal base_actual= Convert.ToDecimal(list_porprod_impuestos_trasladados[indice + 1]);
                                decimal base_nueva = base_actual + seis_decimales(base_total_xprod);


                                list_porprod_impuestos_trasladados.RemoveAt(indice);
                                list_porprod_impuestos_trasladados.Insert(indice, Convert.ToString(monto_nuevo));

                                list_porprod_impuestos_trasladados.RemoveAt(indice + 1);
                                list_porprod_impuestos_trasladados.Insert(indice + 1, Convert.ToString(base_nueva));
                            }
                            else
                            {
                                list_porprod_impuestos_trasladados.Add(cadena);
                                list_porprod_impuestos_trasladados.Add(seis_decimales(importe).ToString());
                                list_porprod_impuestos_trasladados.Add(seis_decimales(base_total_xprod).ToString());
                            }
                        }

                        // Busca si hay impuestos guardados en tabla Facturas_impuestos
                        // De igual manera busca si hay impuestos locales

                        d_concepto_impuesto_td = cn.CargarDatos(cs.cargar_datos_venta_xml(11, id_producto, 0));

                        if (d_concepto_impuesto_td.Rows.Count > 0)
                        {
                            agrega_nodo_concepto_traslado = 1;

                            foreach (DataRow r_concepto_impuesto_td in d_concepto_impuesto_td.Rows)
                            {
                                decimal tasacuota = 0;
                                decimal importe = 0;
                                string t_impuesto = "";

                                if (r_concepto_impuesto_td["tipo"].ToString() == "Traslado")
                                {
                                    ComprobanteConceptoImpuestosTraslado concepto_traslado = new ComprobanteConceptoImpuestosTraslado();

                                    // Base
                                    if (r_concepto_impuesto_td["tipo_factor"].ToString() == "Cuota")
                                    {
                                        concepto_traslado.Base = cantidad;
                                    }
                                    else
                                    {
                                        concepto_traslado.Base = seis_decimales(importe_base * cantidad);
                                    }

                                    // Impuesto
                                    if (r_concepto_impuesto_td["impuesto"].ToString() == "IVA") { t_impuesto = "002"; }
                                    if (r_concepto_impuesto_td["impuesto"].ToString() == "IEPS") { t_impuesto = "003"; }

                                    concepto_traslado.Impuesto = t_impuesto;

                                    // Tipo factor
                                    concepto_traslado.TipoFactor = r_concepto_impuesto_td["tipo_factor"].ToString();


                                    if (r_concepto_impuesto_td["tipo_factor"].ToString() != "Exento")
                                    {
                                        // Tasa o cuota
                                        if (r_concepto_impuesto_td["tasa_cuota"].ToString() == "Definir %")
                                        {
                                            tasacuota = Convert.ToDecimal(r_concepto_impuesto_td["definir"]);

                                            if (r_concepto_impuesto_td["tipo_factor"].ToString() != "Cuota")
                                            {
                                                if (tasacuota > 1)
                                                {
                                                    tasacuota = tasacuota / 100;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string porcentaje = r_concepto_impuesto_td["tasa_cuota"].ToString();

                                            var exi_porc= porcentaje.IndexOf('%');

                                            if(exi_porc >= 0)
                                            {
                                                porcentaje = porcentaje.Substring(0, (3 - 1));
                                            }
                                                                                        
                                            tasacuota = Convert.ToDecimal(porcentaje);                                            

                                            if (tasacuota > 1)
                                            {
                                                tasacuota = tasacuota / 100;
                                            }
                                        }

                                        concepto_traslado.TasaOCuota = seis_decimales(tasacuota);
                                        // Importe
                                        //importe = Convert.ToDecimal(r_concepto_impuesto_td["importe"]) * cantidad;
                                        importe= (importe_base * cantidad) * tasacuota;
                                        concepto_traslado.Importe = seis_decimales(importe);
                                    }


                                    list_concepto_impuestos_traslados.Add(concepto_traslado);


                                    // Guarda en la lista el tipo de impuesto

                                    string cadena = t_impuesto + "-" + r_concepto_impuesto_td["tipo_factor"].ToString() + "-" + tasacuota;

                                    // Busca si la cadena existe en la lista
                                    var indice = list_porprod_impuestos_trasladados.IndexOf(cadena);

                                    // Si la cadena existe aumenta el importe del impuesto, de lo contrario la agrega como nueva
                                    if (indice >= 0)
                                    {
                                        indice = indice + 1;
                                        decimal monto_actual = Convert.ToDecimal(list_porprod_impuestos_trasladados[indice]);
                                        decimal monto_nuevo = monto_actual + seis_decimales(importe);

                                        decimal base_actual = Convert.ToDecimal(list_porprod_impuestos_trasladados[indice + 1]);
                                        decimal base_nueva = base_actual + seis_decimales(base_total_xprod);


                                        list_porprod_impuestos_trasladados.RemoveAt(indice);
                                        list_porprod_impuestos_trasladados.Insert(indice, Convert.ToString(monto_nuevo));

                                        list_porprod_impuestos_trasladados.RemoveAt(indice + 1);
                                        list_porprod_impuestos_trasladados.Insert(indice + 1, Convert.ToString(base_nueva));
                                    }
                                    else
                                    {
                                        list_porprod_impuestos_trasladados.Add(cadena);
                                        list_porprod_impuestos_trasladados.Add(seis_decimales(importe).ToString());
                                        list_porprod_impuestos_trasladados.Add(seis_decimales(base_total_xprod).ToString());
                                    }
                                }

                                // Impuestos locales

                                if (r_concepto_impuesto_td["tipo"].ToString() == "Loc. Traslado" | r_concepto_impuesto_td["tipo"].ToString() == "Loc. Retenido")
                                {
                                    decimal tasa = 0;

                                    if (r_concepto_impuesto_td["tasa_cuota"].ToString() == "Definir %")
                                    {
                                        tasa = Convert.ToDecimal(r_concepto_impuesto_td["definir"]);

                                        if (Convert.ToDecimal(r_concepto_impuesto_td["definir"]) < 1)
                                        {
                                            tasa = Convert.ToDecimal(r_concepto_impuesto_td["definir"]) * 100;
                                        }
                                    }
                                    else
                                    {
                                        tasa = Convert.ToDecimal(r_concepto_impuesto_td["tasa_cuota"]);

                                        if (tasa < 1)
                                        {
                                            tasa = Convert.ToDecimal(r_concepto_impuesto_td["tasa_cuota"]) * 100;
                                        }
                                    }

                                    string cadena = r_concepto_impuesto_td["impuesto"].ToString() + "-" + tasa;

                                    if (r_concepto_impuesto_td["tipo"].ToString() == "Loc. Traslado")
                                    {
                                        list_impuestos_local_traslado.Add(cadena);
                                        list_impuestos_local_traslado.Add(r_concepto_impuesto_td["importe"].ToString());
                                    }
                                    if (r_concepto_impuesto_td["tipo"].ToString() == "Loc. Retenido")
                                    {
                                        list_impuestos_local_traslado.Add(cadena);
                                        list_impuestos_local_traslado.Add(r_concepto_impuesto_td["importe"].ToString());
                                    }
                                }
                            }
                        }


                        
                        // NODO IMPUESTOS RETENIDOS POR PRODUCTO
                        //----------------------------------------


                        agrega_nodo_concepto_retencion = 0;

                        List<ComprobanteConceptoImpuestosRetencion> list_concepto_impuestos_retenidos = new List<ComprobanteConceptoImpuestosRetencion>();


                        // Consulta si hay impuestos retenidos en tabla Facturas_impuestos
                        d_concepto_impuesto_r = cn.CargarDatos(cs.cargar_datos_venta_xml(11, id_producto, 0));

                        if (d_concepto_impuesto_r.Rows.Count > 0)
                        {
                            string r_impuesto = "";


                            foreach (DataRow r_concepto_impuesto_r in d_concepto_impuesto_r.Rows)
                            {
                                decimal importe = 0;

                                if (r_concepto_impuesto_r["tipo"].ToString() == "Retención")
                                {
                                    agrega_nodo_concepto_retencion = 1;

                                    ComprobanteConceptoImpuestosRetencion concepto_retencion = new ComprobanteConceptoImpuestosRetencion();

                                    // Base
                                    if (r_concepto_impuesto_r["tipo_factor"].ToString() == "Cuota")
                                    {
                                        concepto_retencion.Base = cantidad;
                                    }
                                    else
                                    {
                                        concepto_retencion.Base = seis_decimales(importe_base * cantidad);
                                    }

                                    // Impuesto
                                    if (r_concepto_impuesto_r["impuesto"].ToString() == "ISR") { r_impuesto = "001"; }
                                    if (r_concepto_impuesto_r["impuesto"].ToString() == "IVA") { r_impuesto = "002"; }
                                    if (r_concepto_impuesto_r["impuesto"].ToString() == "IEPS") { r_impuesto = "003"; }

                                    concepto_retencion.Impuesto = r_impuesto;

                                    // Tipo factor
                                    concepto_retencion.TipoFactor = r_concepto_impuesto_r["tipo_factor"].ToString();

                                    // Tasa o cuota
                                    decimal tasa_cuota = 0;

                                    //if (r_concepto_impuesto_r["TasaCuota"].ToString() == "Definir %")
                                    if (Convert.ToDecimal(r_concepto_impuesto_r["definir"]) > 0)
                                    {
                                        if (r_concepto_impuesto_r["tipo_factor"].ToString() == "Cuota")
                                        {
                                            tasa_cuota = Convert.ToDecimal(r_concepto_impuesto_r["definir"]);
                                        }
                                        else
                                        {
                                            if (Convert.ToDecimal(r_concepto_impuesto_r["definir"]) > 1)
                                            {
                                                decimal porcentaje = Convert.ToDecimal(r_concepto_impuesto_r["definir"]);
                                                tasa_cuota = porcentaje / 100;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string porcentaje = r_concepto_impuesto_r["tasa_cuota"].ToString();

                                        if (Convert.ToDecimal(porcentaje) > 1)
                                        {
                                            tasa_cuota = Convert.ToDecimal(porcentaje) / 100;
                                        }
                                    }
                                    concepto_retencion.TasaOCuota = seis_decimales(tasa_cuota);
                                    // Importe
                                    //importe = Convert.ToDecimal(r_concepto_impuesto_r["importe"]) * cantidad;
                                    importe = (importe_base * cantidad) * tasa_cuota;
                                    concepto_retencion.Importe = seis_decimales(importe);


                                    list_concepto_impuestos_retenidos.Add(concepto_retencion);


                                    // Hace sumatoria de impuestos
                                    if (r_impuesto == "001")
                                    {
                                        total_ISR += seis_decimales(importe);
                                    }
                                    if (r_impuesto == "002")
                                    {
                                        total_IVA += dos_decimales(importe);
                                    }
                                    if (r_impuesto == "003")
                                    {
                                        total_IEPS += seis_decimales(importe);
                                    }
                                }
                            }
                        }


                        
                        // AGREGA AL NODO IMPUESTOS LOS TRASLADADOS Y RETENIDOS
                        //-----------------------------------------------------


                        if (agrega_nodo_concepto_traslado == 1 | agrega_nodo_concepto_retencion == 1)
                        {
                            concepto.Impuestos = new ComprobanteConceptoImpuestos();

                            if (agrega_nodo_concepto_traslado == 1)
                            {
                                concepto.Impuestos.Traslados = list_concepto_impuestos_traslados.ToArray();
                            }
                            if (agrega_nodo_concepto_retencion == 1)
                            {
                                concepto.Impuestos.Retenciones = list_concepto_impuestos_retenidos.ToArray();
                            }
                        }
                        
                    }


                    // NODO A CUENTA TERCEROS
                    //-----------------------

                    if (r_productos["nombre_ctercero"].ToString() != "" & r_productos["nombre_ctercero"].ToString() != null)
                    {
                        ComprobanteConceptoACuentaTerceros cnt_terceros = new ComprobanteConceptoACuentaTerceros();

                        cnt_terceros.RfcACuentaTerceros = r_productos["rfc_ctercero"].ToString();
                        cnt_terceros.NombreACuentaTerceros = r_productos["nombre_ctercero"].ToString();
                        cnt_terceros.RegimenFiscalACuentaTerceros = r_productos["regimen_ctercero"].ToString();
                        cnt_terceros.DomicilioFiscalACuentaTerceros = r_productos["cp_ctercero"].ToString();

                        concepto.ACuentaTerceros = cnt_terceros;
                    }


                    listaConceptos.Add(concepto);

                    agrega_nodo_concepto_traslado = 0;
                    agrega_nodo_concepto_retencion = 0;
                }

                comprobante.Conceptos = listaConceptos.ToArray();
            }

            
            

            // Si factura es diferente de comlemento de pago, agrega el nodo impuestos

            if(con_complemento_pg == 0)
            {

                // NODO IMPUESTOS GENERALES
                //-------------------------


                // Verificar si existe algún impuesto exento, si solo hay impuestos exentos no se agrega el nodo impuestos

                string buscar_cadena = "002-Tasa-Exento-0";
                var indice_bs = list_porprod_impuestos_trasladados.IndexOf(buscar_cadena);
                int tam_list_imp_t = list_porprod_impuestos_trasladados.Count;
                //int tam_list_imp_r = 0;
                int agregar_nodo_impuestos = 0;
                int no_agrega_nodo_traslados = 0;
                int no_agrega_nodo_retenido = 1;

                // Traslados

                if (tam_list_imp_t > 0)
                {
                   /* if (indice_bs >= 0) // Existe el impuesto exento
                    {
                        if (tam_list_imp_t > 1)
                        {
                            agregar_nodo_impuestos = 1;
                        }
                        else
                        {
                            no_agrega_nodo_traslados = 1;
                        }
                    }
                    else
                    {*/
                        agregar_nodo_impuestos = 1;
                   // }
                }
                else
                {
                    no_agrega_nodo_traslados = 1;
                }


                // Retenciones

                if (total_ISR > 0 | total_IVA > 0 | total_IEPS > 0)
                {
                    agregar_nodo_impuestos = 1;
                    no_agrega_nodo_retenido = 0;
                }


                if (agregar_nodo_impuestos == 1)
                {
                    List<ComprobanteImpuestosTraslado> list_impuestos_traslado = new List<ComprobanteImpuestosTraslado>();
                    List<ComprobanteImpuestosRetencion> list_impuestos_retenido = new List<ComprobanteImpuestosRetencion>();

                    // Trasladados

                    if (no_agrega_nodo_traslados == 0)
                    {
                        int c_impt = 0;

                        while (c_impt < tam_list_imp_t)
                        {
                            string[] dato_it = list_porprod_impuestos_trasladados[c_impt].Split('-');
                            string t_factor = dato_it[1];
                            decimal t_cuota = Convert.ToDecimal(dato_it[2]);


                            ComprobanteImpuestosTraslado impuestos_traslado = new ComprobanteImpuestosTraslado();

                            impuestos_traslado.Base = dos_decimales(Convert.ToDecimal(list_porprod_impuestos_trasladados[c_impt + 2]));
                            impuestos_traslado.Impuesto = dato_it[0];
                            impuestos_traslado.TipoFactor = t_factor;

                            if (t_factor != "Exento")
                            {
                                impuestos_traslado.TasaOCuota = seis_decimales(t_cuota);
                                impuestos_traslado.Importe = dos_decimales(Convert.ToDecimal(list_porprod_impuestos_trasladados[c_impt + 1]));
                            }

                            list_impuestos_traslado.Add(impuestos_traslado);

                            suma_impuesto_traslado += seis_decimales(Convert.ToDecimal(list_porprod_impuestos_trasladados[c_impt + 1]));

                            c_impt += 3;
                        }
                    }

                    // Retenidos

                    if (no_agrega_nodo_retenido == 0)
                    {
                        if (total_ISR > 0)
                        {
                            ComprobanteImpuestosRetencion impuestos_retenidos = new ComprobanteImpuestosRetencion();

                            impuestos_retenidos.Impuesto = "001";
                            impuestos_retenidos.Importe = total_ISR;

                            list_impuestos_retenido.Add(impuestos_retenidos);
                        }
                        if (total_IVA > 0)
                        {
                            ComprobanteImpuestosRetencion impuestos_retenidos = new ComprobanteImpuestosRetencion();

                            impuestos_retenidos.Impuesto = "002";
                            impuestos_retenidos.Importe = total_IVA;

                            list_impuestos_retenido.Add(impuestos_retenidos);
                        }
                        if (total_IEPS > 0)
                        {
                            ComprobanteImpuestosRetencion impuestos_retenidos = new ComprobanteImpuestosRetencion();

                            impuestos_retenidos.Impuesto = "003";
                            impuestos_retenidos.Importe = total_IEPS;

                            list_impuestos_retenido.Add(impuestos_retenidos);
                        }

                        suma_impuesto_retenido = total_ISR + total_IVA + total_IEPS;
                    }



                    ComprobanteImpuestos nd_impuestos_g = new ComprobanteImpuestos();

                    if (no_agrega_nodo_traslados == 0) // Nodo impuestos trasladados
                    {
                        nd_impuestos_g.TotalImpuestosTrasladadosSpecified = true;
                        nd_impuestos_g.TotalImpuestosTrasladados = dos_decimales(suma_impuesto_traslado);

                        if(list_impuestos_traslado.Count > 0)
                        {
                            nd_impuestos_g.Traslados = list_impuestos_traslado.ToArray();
                        }
                    }
                    if (no_agrega_nodo_retenido == 0)
                    {
                        nd_impuestos_g.TotalImpuestosRetenidosSpecified = true;
                        nd_impuestos_g.TotalImpuestosRetenidos = dos_decimales(suma_impuesto_retenido);

                        nd_impuestos_g.Retenciones = list_impuestos_retenido.ToArray();
                    }

                    comprobante.Impuestos = nd_impuestos_g;
                }
            }




            // NODO IMPUESTOS LOCALES
            //------------------------


            // Consulta si hay impuestos retenidos
            //d_concepto_impuesto_r = cn.CargarDatos(cs.cargar_datos_venta_xml(8, id_producto, 0));





            // DATOS DEL NODO PRINCIPAL "COMPROBANTE"
            //---------------------------------------

            decimal total_general = (suma_total_productos + dos_decimales(suma_impuesto_traslado)) - (dos_decimales(suma_impuesto_retenido) + dos_decimales(descuento_general));
           

            comprobante.Version = "4.0";
            // Serie
            if (serie != "")
            {
                comprobante.Serie = serie;
            }
            // Folio
            if (folio != "")
            {
                comprobante.Folio = folio;
            }
            // Fecha
            comprobante.Fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");
            // Forma de pago
            if (con_complemento_pg == 0) // No aplica para complementos de pago
            {
                if (forma_pago != "")
                {
                    comprobante.FormaPago = forma_pago;
                }
            }
            //comprobante.CondicionesDePago = "Vacio"; //EN DUDA SI SE PONDRÁ
            comprobante.NoCertificado = numero_certificado;
            // Subtotal
            if (con_complemento_pg == 0)
            {
                comprobante.SubTotal = dos_decimales(suma_total_productos);
            }
            else
            { // Es complemento de pago
                comprobante.SubTotal = 0;
            }
            // Descuento general    
            if (descuento_general > 0)
            {
                comprobante.Descuento = dos_decimales(descuento_general);
            }
            // Moneda
            if(con_complemento_pg == 0)
            {
                comprobante.Moneda = moneda;
            }
            else
            {
                comprobante.Moneda = "XXX";
            }
            // Tipo cambio
            if(con_complemento_pg == 0)
            {
                if(moneda != "XXX")
                {
                    comprobante.TipoCambio = Convert.ToDecimal(tipo_cambio);
                }
            }
            // Total
            if(con_complemento_pg == 0)
            {
                comprobante.Total = dos_decimales(total_general);
            }
            else
            {
                comprobante.Total = 0;
            }
            // Tipo de comprobante
            comprobante.TipoDeComprobante = tipo_comprobante;
            // Exportación
            comprobante.Exportacion = exportacion;
            // Método de pago
            if(con_complemento_pg == 0)
            {
                comprobante.MetodoPago = metodo_pago;
            }
            // Lugar de expedición
            comprobante.LugarExpedicion = lugar_expedicion;


        //comprobante.TipoCambioSpecified = true;

            if (con_complemento_pg == 0)
            {
                comprobante.xsiSchemaLocation = "http://www.sat.gob.mx/cfd/4 http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd";
            }
                



            // COMPLEMENTO PAGO
            //-----------------


            if (con_complemento_pg == 1)
            {
                comprobante.xsiSchemaLocation = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd http://www.sat.gob.mx/Pagos http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos10.xsd";

                // NODO PAGOS, PAGO
                //-----------------

                /*Pagos pagos = new Pagos();
                PagosPago pago = new PagosPago();
                List<PagosPago> list_pagos_pago = new List<PagosPago>();

                pago.FechaPago = pg_fecha_pago;
                pago.FormaDePagoP = forma_pago;
                pago.MonedaP = moneda;
                //pago.TipoCambioP = ;
                pago.Monto = dos_decimales(monto_cpago);



                // NODO DOCUMENTOS RELACIONADOS
                //-----------------------------

            
                DataTable d_cpago = cn.CargarDatos(cs.obtener_datos_para_gcpago(2, id_factura));

                if (d_cpago.Rows.Count > 0)
                {
                    List<PagosPagoDoctoRelacionado> list_doc_relacionados = new List<PagosPagoDoctoRelacionado>();

                    foreach (DataRow r_cpago in d_cpago.Rows)
                    {
                        PagosPagoDoctoRelacionado doc_relacionados = new PagosPagoDoctoRelacionado();

                        doc_relacionados.IdDocumento = r_cpago["uuid"].ToString();
                        doc_relacionados.MonedaDR = r_cpago["moneda"].ToString();

                        if(r_cpago["moneda"].ToString() != "")
                        {
                            if (r_cpago["moneda"].ToString() != "MXN")
                            {
                                doc_relacionados.TipoCambioDR = Convert.ToDecimal(r_cpago["tipo_cambio"]);
                                doc_relacionados.TipoCambioDRSpecified = true;
                            }
                        }
                        
                        doc_relacionados.MetodoDePagoDR = r_cpago["metodo_pago"].ToString();
                        doc_relacionados.NumParcialidad = r_cpago["num_parcialidad"].ToString();
                        doc_relacionados.ImpSaldoAnt = dos_decimales(Convert.ToDecimal(r_cpago["saldo_anterior"]));
                        doc_relacionados.ImpPagado = dos_decimales(Convert.ToDecimal(r_cpago["importe_pagado"]));
                        doc_relacionados.ImpSaldoInsoluto = dos_decimales(Convert.ToDecimal(r_cpago["saldo_insoluto"]));

                        list_doc_relacionados.Add(doc_relacionados);
                    }

                    pago.DoctoRelacionado = list_doc_relacionados.ToArray();
                }                



                list_pagos_pago.Add(pago);
                pagos.Pago = list_pagos_pago.ToArray();


                comprobante.Complemento = new ComprobanteComplemento[1]; // El 1 se agrega por default porque solo se hará este tipo de complemento. Si fuera a ver más de un complemento se pone la cantidad que habrá.
                comprobante.Complemento[0] = new ComprobanteComplemento();


                // Serializamos antes de asignarselo al comprobante

                XmlDocument c_pago = new XmlDocument();
                XmlSerializerNamespaces xml_namespaces_pago = new XmlSerializerNamespaces();
                //xml_namespaces_pago.Add("pago10", ".http://www.sat.gob.mx/Pagos");

                using (XmlWriter write_pago = c_pago.CreateNavigator().AppendChild())
                {
                    new XmlSerializer(pagos.GetType()).Serialize(write_pago, pagos, xml_namespaces_pago);
                }

                comprobante.Complemento[0].Any = new XmlElement[1];
                comprobante.Complemento[0].Any[0] = c_pago.DocumentElement;*/
            }




            // Define el tipo de nombre a incorporar en el archivo

            string nombre_xml = "";

            if(con_complemento_pg == 0)
            {
                nombre_xml = "INGRESOS_";
            }
            else
            {
                nombre_xml = "PAGO_";
            }


            // Verifica si la carpeta donde serán guardados los XML existen. Si no existe la agrega

            string ruta_carpeta = @"C:\Archivos PUDVE\Facturas\";

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                ruta_carpeta = $@"\\{servidor}\Archivos PUDVE\Facturas\";
            }

            if (!Directory.Exists(ruta_carpeta))
            {
                Directory.CreateDirectory(ruta_carpeta);
            }


            string rutaXML = @"C:\Archivos PUDVE\Facturas\XML_" + nombre_xml + id_factura + ".xml";

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaXML = $@"\\{servidor}\Archivos PUDVE\Facturas\XML_" + nombre_xml + id_factura + ".xml";
            }




            GenerarXML(comprobante, rutaXML, con_complemento_pg);




            // .............................................................
            // .    Generación de la cadena original, sello y certificado    .
            // .............................................................


            string cadenaOriginal = string.Empty;
            string rutaXSLT = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\xslt\cadenaoriginal_4_0.xslt";
            System.Xml.Xsl.XslCompiledTransform transformador = new System.Xml.Xsl.XslCompiledTransform(true);
            transformador.Load(rutaXSLT);

            using (StringWriter sw = new StringWriter())
            using (XmlWriter xw = XmlWriter.Create(sw, transformador.OutputSettings))
            {
                transformador.Transform(rutaXML, xw);
                cadenaOriginal = sw.ToString();
            }

            CFDI.SelloDigital selloDigital = new CFDI.SelloDigital();
            comprobante.Certificado = selloDigital.Certificado(ruta_cer);
            comprobante.Sello = selloDigital.Sellar(cadenaOriginal, ruta_key, clave_privada);
            
            GenerarXML(comprobante, rutaXML, con_complemento_pg);

            

            // ......................
            // .    Timbrar CFDI    .
            // ......................

           /* string usuario = "EWE1709045U0.Test";
            string clave_u = "Prueba$1";
            int id_servicio = 194876591;*/
            string usuario = "NUSN900420SS5";
            string clave_u = "Acceso$1";
            int id_servicio = 196789671;
            byte[] XML40 = File.ReadAllBytes(rutaXML);


            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //Convierte archivo en bytes
            var servicio = new FH_CFDI40_produccion.WsEmisionTimbrado40();
            //var servicio = new FH_CFDI40_test.WsEmisionTimbrado40();


            try
            {
                //Crea un objeto del web service
                var respuesta = servicio.EmitirTimbrar(usuario, clave_u, id_servicio, XML40);
                

                if (!respuesta.isError)
                {
                    //Si no hay errores guarda el xml timbrado
                    File.WriteAllBytes(rutaXML, respuesta.XML);

                    // mensaje = "Exíto: " + "\n" +                            
                    //respuesta.folioUDDI + "\n" +
                    //respuesta.cadenaOriginal + "\n" +
                    // respuesta.selloDigitalEmisor + "\n" +
                    //respuesta.selloDigitalTimbreSAT + "\n" +
                    // respuesta.fechaHoraTimbrado;


                    // Cambia a timbrada la nota de venta

                    if (con_complemento_pg == 0)
                    {
                        string[] datos = new string[] { id_venta.ToString() };

                        cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(4, datos));
                    }

                    // Cambia a timbrada la factura

                    string[] dat_f = new string[] { id_factura.ToString() };

                    cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(8, dat_f));

                    // Cambia a timbrado cada documento relacionado del complemento de pago

                    if (con_complemento_pg == 1)
                    {
                        string[] dat_cp = new string[] { id_factura.ToString() };
                        cn.EjecutarConsulta(cs.crear_complemento_pago(5, dat_cp));

                        // Cambia variable a 1 para indicar que la factura principal tienen complementos de pago

                        for (int x = 0; x < arr_idf_principal_pago.Length; x++)
                        {
                            string[] datos_v = new string[]
                            {
                                arr_idf_principal_pago[x][0].ToString(), arr_idf_principal_pago[x][1].ToString()
                            };

                            cn.EjecutarConsulta(cs.crear_complemento_pago(4, datos_v));
                        }
                    }


                    // Leer XML para obtener total, UUID, sellos 

                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(rutaXML);

                    // Total
                    XmlAttributeCollection c_total = xdoc.DocumentElement.Attributes;
                    string obt_total = ((XmlAttribute)c_total.GetNamedItem("Total")).Value;

                    // Datos del nodo timbre fiscal

                    string uuid = "", fecha_cer = "";
                    string sello_cfd = "", rfc_pac = "";
                    string sello_sat = "";

                    XmlNodeList nod_list = xdoc.GetElementsByTagName("tfd:TimbreFiscalDigital");
                    XmlAttributeCollection rr = nod_list[0].Attributes;

                    uuid = ((XmlAttribute)rr.GetNamedItem("UUID")).Value;
                    fecha_cer = ((XmlAttribute)rr.GetNamedItem("FechaTimbrado")).Value;
                    rfc_pac = ((XmlAttribute)rr.GetNamedItem("RfcProvCertif")).Value;
                    sello_cfd = ((XmlAttribute)rr.GetNamedItem("SelloCFD")).Value;
                    sello_sat = ((XmlAttribute)rr.GetNamedItem("SelloSAT")).Value;


                    string[] datos_xml = new string[]
                    {
                        id_factura.ToString(), fecha_cer, uuid, rfc_pac, sello_sat, sello_cfd, obt_total
                    };
                    cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(9, datos_xml));


                    // Resta timbre

                    cn.EjecutarConsulta(cs.descontar_timbres(id_usuario));

                }
                else
                {
                    mensaje = respuesta.message;
                }

            }
            catch (FaultException fex)
            {                
                mensaje = fex.Message;

                // Elimina la factura que fue creada
                //error_eliminar_factura(id_factura, con_complemento_pg);
            }
            catch (XmlException e_xml)
            {
                mensaje = e_xml.Message;
            }
            catch(CommunicationException com)
            {
                mensaje = com.Message;
            }
            catch (WebException we)
            {
                mensaje = we.Message;
            }



            return mensaje;
        }


        private void GenerarXML(Comprobante comprobante, string rutaXML, int complemento_pg)
        {
            string xml = string.Empty;

            XmlSerializerNamespaces xmlNameSpaces = new XmlSerializerNamespaces();
            xmlNameSpaces.Add("cfdi", "http://www.sat.gob.mx/cfd/4");
            xmlNameSpaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            if(complemento_pg == 1)
            {
                xmlNameSpaces.Add("pago10", "http://www.sat.gob.mx/Pagos");
            }
            

            //Generacion del XML

            XmlSerializer xmlSerializador = new XmlSerializer(typeof(Comprobante));

            using (var sw = new CFDI.StringWriterConEncoding(Encoding.UTF8))
            {
                using (XmlWriter writter = XmlWriter.Create(sw))
                {
                    xmlSerializador.Serialize(writter, comprobante, xmlNameSpaces);
                    xml = sw.ToString();
                }
            }

            File.WriteAllText(rutaXML, xml);
        }

        private decimal seis_decimales(decimal c)
        {
            decimal cantidad = Decimal.Round(c, 6);

            if(cantidad % 2 == 0)
            {
            }
            else
            {
                cantidad = Convert.ToDecimal(cantidad.ToString(".000000"));
            }

            return cantidad;
        }

        private decimal dos_decimales(decimal c)
        {
            //decimal cantidad = Decimal.Round(c, 2);
            decimal cantidad = Math.Round(c, 2, MidpointRounding.AwayFromZero);

            return cantidad;
        }

        private string[] calculo_limites(decimal ibase, int ndecimal, decimal tcuota, decimal imp_impuesto)
        {
            string[] r = new string[3];

            double limiteI = Math.Pow(10, -(ndecimal)) / 2;
            limiteI = (Convert.ToDouble(ibase) - limiteI ) * Convert.ToDouble(tcuota);
            
            double limiteS = Math.Pow(10, -(ndecimal))  /  (2 - Math.Pow(10, -(12)));
            limiteS = (Convert.ToDouble(ibase) + limiteS) * Convert.ToDouble(tcuota);


            decimal limite_i = seis_decimales(Convert.ToDecimal(limiteI));
            decimal limite_s = seis_decimales(Convert.ToDecimal(limiteS));

            if(imp_impuesto >= limite_i  &  imp_impuesto <= limite_s)
            {
                r[0] = "true";
            }
            r[1] = Convert.ToString(limite_i);
            r[2] = Convert.ToString(limite_s);

            return r;
        }

        private decimal recalcular_datos_impuestos(int idp, decimal importe, decimal cantp, string limI, string limS)
        {
            decimal importe_li = Convert.ToDecimal(limI);
            decimal importe_ls = Convert.ToDecimal(limS);
            decimal media = (importe_li + importe_ls) / 2;

            if (importe < importe_li | importe > importe_ls)
            {
                media = media / cantp;
                media = seis_decimales(media);

                cn.EjecutarConsulta($"UPDATE Facturas_productos SET importe_iva='{media}' WHERE ID='{idp}'");
            }
            
            return media;
        }


            /*private void error_eliminar_factura(int idf, int complemento)
            {
                // Complemento pago
                if(complemento == 1)
                {
                    cn.EjecutarConsulta($"DELETE Facturas_complemento_pago WHERE id_factura='{idf}'");
                }

                // Impuestos extras
                DataTable d_product = cn.CargarDatos(cs.cargar_datos_venta_xml(10, idf, 0));

                if(d_product.Rows.Count > 0)
                {
                    foreach(DataRow r_product in d_product.Rows)
                    {
                        int id_fact_producto = Convert.ToInt32(r_product["ID"].ToString());

                        cn.EjecutarConsulta($"DELETE Facturas_impuestos WHERE id_factura_producto='{id_fact_producto}'");
                    }
                }

                //Productos
                cn.EjecutarConsulta($"DELETE Facturas_productos WHERE id_factura='{idf}'");

                //Factura principal
                cn.EjecutarConsulta($"DELETE Facturas WHERE ID='{idf}'");
            }*/
        }
    }
