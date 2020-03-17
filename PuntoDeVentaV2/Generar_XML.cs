﻿using System;
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


namespace PuntoDeVentaV2
{
    class Generar_XML
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public static int con_complemento_pg = 0;



        public string obtener_datos_XML(int id_f, int id_v, int con_cpg, decimal[][] arr_idf_principal_pago)
        {
            //Console.WriteLine("DATO ARR"+arr_idf_principal_pago[0][0]);
            // ID
            int id_factura = id_f;
            int id_venta = id_v;
            con_complemento_pg = con_cpg;
            int id_usuario = Convert.ToInt32(FormPrincipal.userID);
            // Variables para los archivos
            string ruta_carpeta_archivos = @"C:\Archivos PUDVE\MisDatos\CSD\";
            string ruta_cer = "";
            string ruta_key = "";
            string clave_privada = "";
            string numero_certificado = "";
            DataTable datos_cer;
            DataRow r_datos_cer;
            // Facturas
            DataTable d_facturas;
            DataRow r_facturas;
            string folio = "",           serie = "";
            string fecha = "",           moneda = "";
            string forma_pago = "",      tipo_cambio = "";     
            string metodo_pago = "",     lugar_expedicion = "49000";
            string tipo_comprobante = "";
            // Emisor
            string rfc_e = "";
            string nombre_e = "";
            string regimen = "";
            // Receptor
            string rfc_r = "";
            string nombre_r = "";
            string uso_cfdi = "";
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
            double descuento_general = 0;
            decimal suma_impuesto_traslado = 0;
            decimal suma_impuesto_retenido = 0;
            string mensaje = "";
            // Complemento de pago
            string pg_fecha_pago = "";
            decimal monto_cpago = 0;






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
                //lugar_expedicion = r_ventas[""].ToString();

                rfc_e = r_facturas["e_rfc"].ToString();
                nombre_e = r_facturas["e_razon_social"].ToString();
                regimen = r_facturas["e_regimen"].ToString();

                rfc_r = r_facturas["r_rfc"].ToString();
                nombre_r = r_facturas["r_razon_social"].ToString();
                uso_cfdi = r_facturas["uso_cfdi"].ToString();

                // Solo cuando es un complemento de pago

                if(con_complemento_pg == 1)
                {
                    var f_pg = r_facturas["fecha_hora_cpago"].ToString();
                    string[] fech = f_pg.Split(' ');
                    pg_fecha_pago = Convert.ToDateTime(fech[0]).ToString("yyyy-MM-dd") + "T" + fech[1];

                    monto_cpago = Convert.ToDecimal(r_facturas["monto_cpago"]);
                }
            }

            //Consulta datos emisor

            /*d_emisor = cn.CargarDatos(cs.cargar_datos_venta_xml(2, 0, id_usuario));

            if(d_emisor.Rows.Count > 0)
            {
                r_emisor = d_emisor.Rows[0];

                rfc_e = r_emisor["RFC"].ToString();
                nombre_e = r_emisor["RazonSocial"].ToString();
                regimen = r_emisor["Regimen"].ToString();
            }

            //Consulta datos receptor

            int id_cliente = Convert.ToInt32(cn.EjecutarSelect($"SELECT IDCliente FROM DetallesVenta WHERE IDVenta='{id_venta}'", 6));

            d_receptor = cn.CargarDatos(cs.cargar_datos_venta_xml(3, id_cliente, id_usuario));

            if (d_receptor.Rows.Count > 0)
            {
                r_receptor = d_receptor.Rows[0];

                rfc_r = r_receptor["RFC"].ToString();
                nombre_r = r_receptor["RazonSocial"].ToString();
                uso_cfdi = r_receptor["UsoCFDI"].ToString();
            }*/





            // ..................................
            // .   Obtener archivos digitales   .
            // ..................................

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
            emisor.RegimenFiscal = regimen;


            // NODO RECEPTOR
            //--------------

            ComprobanteReceptor receptor = new ComprobanteReceptor();
            receptor.Rfc = rfc_r;
            receptor.Nombre = nombre_r;
            receptor.UsoCFDI = uso_cfdi;


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
                    int id_producto = Convert.ToInt32(r_productos["ID"]);

                    ComprobanteConcepto concepto = new ComprobanteConcepto();

                    //Consulta claves
                    //string claves = Convert.ToString(cn.EjecutarSelect($"SELECT ClaveProducto, UnidadMedida FROM Productos WHERE ID='{id_producto}'", 7));
                    //string[] clave = claves.Split('-'); 

                    concepto.ClaveProdServ = r_productos["clave_producto"].ToString();
                    //concepto.NoIdentificacion = "151515"; //En duda si se pondrá
                    concepto.Cantidad = seis_decimales(Convert.ToDecimal(r_productos["cantidad"]));
                    concepto.ClaveUnidad = r_productos["clave_unidad"].ToString();
                    //concepto.Unidad = "Numero de paquetes"; //no creo que se agregue
                    concepto.Descripcion = r_productos["descripcion"].ToString();

                    if(con_complemento_pg == 0)
                    {
                        concepto.ValorUnitario = seis_decimales(Convert.ToDecimal(r_productos["precio_u"]));
                    }
                    else
                    {
                        concepto.ValorUnitario = 0;
                    }

                    decimal importe_p = Convert.ToDecimal(r_productos["cantidad"]) * Convert.ToDecimal(r_productos["precio_u"]);

                    if (con_complemento_pg == 0)
                    {
                        concepto.Importe = seis_decimales(importe_p);
                    }
                    else
                    {
                        concepto.Importe = 0;
                    }
                    //concepto.Descuento = 2; //De donde se tomará el descuento??

                    suma_total_productos += importe_p;


                    string d_tasa_c = "";
                    decimal d_base_i = 0,  d_imp_iva = 0,  cantidad = 0;

                    if (con_complemento_pg == 0)
                    {
                        d_base_i = Convert.ToDecimal(r_productos["base"]);
                        d_tasa_c = r_productos["tasa_cuota"].ToString();
                        d_imp_iva = Convert.ToDecimal(r_productos["importe_iva"]);
                        cantidad = seis_decimales(Convert.ToDecimal(r_productos["cantidad"]));
                    }


                    


                    // Si la factura es diferente de un complemento de pago, agrega los nodos impuestos para el concepto

                    if (con_complemento_pg == 0)
                    {

                        // NODO IMPUESTOS TRASLADADOS POR PRODUCTO
                        //----------------------------------------


                        List<ComprobanteConceptoImpuestosTraslado> list_concepto_impuestos_traslados = new List<ComprobanteConceptoImpuestosTraslado>();


                        // Consulta impuestos trasladados  

                        // Se agrega primero uno de los siguientes impuestos; 16, 8, 0 porciento                  
                        //d_concepto_impuesto_t = cn.CargarDatos(cs.cargar_datos_venta_xml(5, id_producto, 0));

                        decimal importe_base = 0;

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
                                
                                concepto_traslado.Importe = seis_decimales(importe);
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

                                list_porprod_impuestos_trasladados.RemoveAt(indice);
                                list_porprod_impuestos_trasladados.Insert(indice, Convert.ToString(monto_nuevo));
                            }
                            else
                            {
                                list_porprod_impuestos_trasladados.Add(cadena);
                                list_porprod_impuestos_trasladados.Add(seis_decimales(importe).ToString());
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
                                            //porcentaje = porcentaje.Substring(0, -2);  
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

                                        list_porprod_impuestos_trasladados.RemoveAt(indice);
                                        list_porprod_impuestos_trasladados.Insert(indice, Convert.ToString(monto_nuevo));
                                    }
                                    else
                                    {
                                        list_porprod_impuestos_trasladados.Add(cadena);
                                        list_porprod_impuestos_trasladados.Add(seis_decimales(importe).ToString());
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

                        /* if (agrega_nodo_concepto_traslado == 1 )
                         {
                             concepto.Impuestos = new ComprobanteConceptoImpuestos();

                             if (agrega_nodo_concepto_traslado == 1)
                             {
                                 concepto.Impuestos.Traslados = list_concepto_impuestos_traslados.ToArray();
                             }
                         }*/


                        
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

                    listaConceptos.Add(concepto);
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
                    if (indice_bs >= 0) // Existe el impuesto exento
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
                    {
                        agregar_nodo_impuestos = 1;
                    }
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

                            if (t_factor != "Exento")
                            {
                                ComprobanteImpuestosTraslado impuestos_traslado = new ComprobanteImpuestosTraslado();

                                impuestos_traslado.Impuesto = dato_it[0];
                                impuestos_traslado.TipoFactor = t_factor;
                                impuestos_traslado.TasaOCuota = seis_decimales(t_cuota);
                                impuestos_traslado.Importe = dos_decimales(Convert.ToDecimal(list_porprod_impuestos_trasladados[c_impt + 1]));

                                list_impuestos_traslado.Add(impuestos_traslado);

                                suma_impuesto_traslado += seis_decimales(Convert.ToDecimal(list_porprod_impuestos_trasladados[c_impt + 1]));
                            }

                            c_impt += 2;
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

                        nd_impuestos_g.Traslados = list_impuestos_traslado.ToArray();
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


            decimal total_general = (suma_total_productos + dos_decimales(suma_impuesto_traslado)) - dos_decimales(suma_impuesto_retenido);


            comprobante.Version = "3.3";
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
                comprobante.Descuento = Convert.ToInt32(descuento_general);
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
                comprobante.TipoCambio = Convert.ToDecimal(tipo_cambio);
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
                comprobante.xsiSchemaLocation = "http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd";
            }
                



            // COMPLEMENTO PAGO
            //-----------------


            if (con_complemento_pg == 1)
            {
                comprobante.xsiSchemaLocation = "http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd http://www.sat.gob.mx/Pagos http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos10.xsd";

                // NODO PAGOS, PAGO
                //-----------------

                Pagos pagos = new Pagos();
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
                xml_namespaces_pago.Add("pago10", "http://www.sat.gob.mx/Pagos");

                using (XmlWriter write_pago = c_pago.CreateNavigator().AppendChild())
                {
                    new XmlSerializer(pagos.GetType()).Serialize(write_pago, pagos, xml_namespaces_pago);
                }

                comprobante.Complemento[0].Any = new XmlElement[1];
                comprobante.Complemento[0].Any[0] = c_pago.DocumentElement;
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

            // Verifica si la careta donde serán guardados los XML existen. Si no existe la agrega

            string ruta_carpeta = @"C:\Archivos PUDVE\Facturas\";

            if (!Directory.Exists(ruta_carpeta))
            {
                Directory.CreateDirectory(ruta_carpeta);
            }

            string rutaXML = @"C:\Archivos PUDVE\Facturas\XML_" + nombre_xml + id_factura + ".xml";

            


            GenerarXML(comprobante, rutaXML);




            // .............................................................
            // .    Geración de la cadena original, sello y certificado    .
            // .............................................................


            string cadenaOriginal = string.Empty;
            string rutaXSLT = Properties.Settings.Default.rutaDirectorio + @"\xslt\cadenaoriginal_3_3.xslt";
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
            
            GenerarXML(comprobante, rutaXML);

            

            // ......................
            // .    Timbrar CFDI    .
            // ......................

            var bXML = File.ReadAllBytes(rutaXML);
            string usuario = "NUSN900420SS5";
            string clave_u = "c.ofis09NSUNotcatno5SS0240";
            // Crear el objeto cliente
            ServiceReferenceTPrueba.timbrado_cfdi33_portClient cliente_timbrar = new ServiceReferenceTPrueba.timbrado_cfdi33_portClient();
            // Crear el objeto de la respuesta
            ServiceReferenceTPrueba.timbrar_cfdi_result respuesta = new ServiceReferenceTPrueba.timbrar_cfdi_result();
            // Llamar al metodo de timbrado
            try
            {
                respuesta = cliente_timbrar.timbrar_cfdi(usuario, clave_u, Convert.ToBase64String(bXML));
                MessageBox.Show(respuesta.xml, "XML", MessageBoxButtons.OK);
                File.WriteAllText(rutaXML, respuesta.xml);

                // Cambia a timbrada la nota de venta

                if(con_complemento_pg == 0)
                {
                    string[] datos = new string[] { id_venta.ToString() };

                    cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(4, datos));
                }

                // Cambia a timbrada la factura

                string[] dat_f = new string[] { id_factura.ToString() };

                cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(8, dat_f));

                // Cambia a timbrado cada documento relacionado del complemento de pago

                if(con_complemento_pg == 1)
                {
                    string[] dat_cp = new string[] { id_factura.ToString() };
                    cn.EjecutarConsulta(cs.crear_complemento_pago(5, dat_cp));

                    // Cambia variable a 1 para indicar que la factura principal tienen complementos de pago

                    for(int x=0; x<arr_idf_principal_pago.Length; x++)
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

                string uuid = "",       fecha_cer = "";
                string sello_cfd = "",  rfc_pac = "";
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

            }
            catch (FaultException fex)
            {
                var codigo = fex.Code.ToString();

                mensaje = "CODIGO ERROR= " + codigo + " --- " + fex.Message;
            }
            catch (XmlException e_xml)
            {
                mensaje = e_xml.Message;
            }


            return mensaje;
        }


        private void GenerarXML(Comprobante comprobante, string rutaXML)
        {
            string xml = string.Empty;

            XmlSerializerNamespaces xmlNameSpaces = new XmlSerializerNamespaces();
            xmlNameSpaces.Add("cfdi", "http://www.sat.gob.mx/cfd/3");
            xmlNameSpaces.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            

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
            decimal cantidad = Decimal.Round(c, 2);

            return cantidad;
        }
    }
}
