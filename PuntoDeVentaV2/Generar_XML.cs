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


namespace PuntoDeVentaV2
{
    class Generar_XML
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        

        public string obtener_datos_XML(int id_v)
        {
            // ID
            int id_venta = id_v;
            int id_usuario = Convert.ToInt32(FormPrincipal.userID);
            // Variables para los archivos
            string ruta_carpeta_archivos = @"C:\Archivos PUDVE\MisDatos\CSD\";
            string ruta_cer = "";
            string ruta_key = "";
            string clave_privada = "";
            string numero_certificado = "";
            DataTable datos_cer;
            DataRow r_datos_cer;
            // ventas
            DataTable d_ventas;
            DataRow r_ventas;
            string folio = "",           serie = "";
            string fecha = "",           moneda = "MXN";
            string forma_pago = "",      tipo_cambio = "";     
            string metodo_pago = "",     lugar_expedicion = "49000";
            //double subtotal = 0,         total = 0;
            // Emisor
            DataTable d_emisor;
            DataRow r_emisor;
            string rfc_e = "";
            string nombre_e = "";
            string regimen = "";
            // Receptor
            DataTable d_receptor;
            DataRow r_receptor;
            string rfc_r = "";
            string nombre_r = "";
            string uso_cfdi = "";
            // Productos
            DataTable d_productos;
            DataTable d_concepto_impuesto_t;
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

            // Consulta datos guardados de la venta

            d_ventas = cn.CargarDatos(cs.cargar_datos_venta_xml(1, id_venta, id_usuario));

            if(d_ventas.Rows.Count > 0)
            {
                r_ventas = d_ventas.Rows[0];

                folio = r_ventas["Folio"].ToString();
                serie = r_ventas["serie"].ToString();
                forma_pago = r_ventas["FormaPago"].ToString();
                ///moneda = r_ventas[""].ToString();
                //tipo_cambio = r_ventas[""].ToString();
                metodo_pago = r_ventas["MetodoPago"].ToString();
                //lugar_expedicion = r_ventas[""].ToString();
            }

            //Consulta datos emisor

            d_emisor = cn.CargarDatos(cs.cargar_datos_venta_xml(2, 0, id_usuario));

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
            }





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
            
            // Consulta lista de productos 

            d_productos = cn.CargarDatos(cs.cargar_datos_venta_xml(4, id_venta, id_usuario));

            if(d_productos.Rows.Count > 0)
            {
                foreach (DataRow r_productos in d_productos.Rows)
                {
                    int id_producto = Convert.ToInt32(r_productos["IDProducto"]);
                    

                    ComprobanteConcepto concepto = new ComprobanteConcepto();

                    //Consulta claves
                    string claves = Convert.ToString(cn.EjecutarSelect($"SELECT ClaveProducto, UnidadMedida FROM Productos WHERE ID='{id_producto}'", 7));
                    string[] clave = claves.Split('-'); 

                    concepto.ClaveProdServ = clave[0];
                    //concepto.NoIdentificacion = "151515"; //En duda si se pondrá
                    concepto.Cantidad = Convert.ToDecimal(r_productos["Cantidad"]);
                    concepto.ClaveUnidad = clave[1];
                    //concepto.Unidad = "Numero de paquetes"; //no creo que se agregue
                    concepto.Descripcion = r_productos["Nombre"].ToString();
                    concepto.ValorUnitario = Convert.ToDecimal(r_productos["Precio"]);


                    decimal cantidad = Convert.ToDecimal(r_productos["Cantidad"]);
                    decimal importe_p = Convert.ToDecimal(r_productos["Cantidad"]) * Convert.ToDecimal(r_productos["Precio"]);

                    concepto.Importe = importe_p;
                    suma_total_productos += importe_p; 
                    //concepto.Descuento = 2; //De donde se tomará el descuento??

                    


                    // NODO IMPUESTOS TRASLADADOS POR PRODUCTO
                    //----------------------------------------

                    List<ComprobanteConceptoImpuestosTraslado> list_concepto_impuestos_traslados = new List<ComprobanteConceptoImpuestosTraslado>();
                    

                    // Consulta impuestos trasladados  
                    
                    // Se agrega primero uno de los siguientes impuestos; 16, 8, 0 porciento                  
                    d_concepto_impuesto_t = cn.CargarDatos(cs.cargar_datos_venta_xml(5, id_producto, 0));

                    decimal importe_base = 0;

                    if (d_concepto_impuesto_t.Rows.Count > 0)
                    {
                        DataRow r_concepto_impuesto_t = d_concepto_impuesto_t.Rows[0];
                        decimal tasacuota = 0;
                        decimal importe = 0;
                        string tipo_factor = "Tasa";
                        agrega_nodo_concepto_traslado = 1;


                        importe_base = Convert.ToDecimal(r_concepto_impuesto_t["Base"]);
                        
                        if (r_concepto_impuesto_t["Impuesto"].ToString() == "16%") { tasacuota = 0.160000m; }
                        if (r_concepto_impuesto_t["Impuesto"].ToString() == "8%") { tasacuota = 0.080000m; }
                        if (r_concepto_impuesto_t["Impuesto"].ToString() == "0%" | r_concepto_impuesto_t["Impuesto"].ToString() == "Exento")
                        {
                            tasacuota = 0.000000m;

                            if(r_concepto_impuesto_t["Impuesto"].ToString() == "Exento")
                            {
                                tipo_factor = "Exento";
                            }
                        }

                        ComprobanteConceptoImpuestosTraslado concepto_traslado = new ComprobanteConceptoImpuestosTraslado();

                        concepto_traslado.Base = importe_base * cantidad;
                        concepto_traslado.Impuesto = "002";
                        concepto_traslado.TipoFactor = tipo_factor;

                        if(tipo_factor != "Exento")
                        {
                            concepto_traslado.TasaOCuota = tasacuota;

                            // Importe
                            importe = Convert.ToDecimal(r_concepto_impuesto_t["IVA"]) * cantidad;
                            concepto_traslado.Importe = importe;
                        }

                        list_concepto_impuestos_traslados.Add(concepto_traslado);


                        // Guarda en la lista el tipo de impuesto

                        string cadena = "002-" + tipo_factor + "-" + tasacuota;

                        list_porprod_impuestos_trasladados.Add(cadena);
                        list_porprod_impuestos_trasladados.Add(importe.ToString());
                    }

                    // Busca si hay impuestos guardados en tabla DetallesFacturacionProductos
                    // De igual manera busca si hay impuestos locales

                    d_concepto_impuesto_td = cn.CargarDatos(cs.cargar_datos_venta_xml(8, id_producto, 0));

                    if(d_concepto_impuesto_td.Rows.Count > 0)
                    {
                        agrega_nodo_concepto_traslado = 1;

                        foreach (DataRow r_concepto_impuesto_td in d_concepto_impuesto_td.Rows)
                        {
                            decimal tasacuota = 0;
                            decimal importe = 0;
                            string t_impuesto = "";

                            if (r_concepto_impuesto_td["Tipo"].ToString() == "Traslado")
                            {
                                ComprobanteConceptoImpuestosTraslado concepto_traslado = new ComprobanteConceptoImpuestosTraslado();
                                
                                // Base
                                if (r_concepto_impuesto_td["TipoFactor"].ToString() == "Cuota")
                                {
                                    concepto_traslado.Base = cantidad;
                                }
                                else
                                {
                                    concepto_traslado.Base = importe_base * cantidad;
                                }
                                
                                // Impuesto
                                if (r_concepto_impuesto_td["Impuesto"].ToString() == "IVA") { t_impuesto = "002"; }
                                if (r_concepto_impuesto_td["Impuesto"].ToString() == "IEPS") { t_impuesto = "003"; }

                                concepto_traslado.Impuesto = t_impuesto;

                                // Tipo factor
                                concepto_traslado.TipoFactor = r_concepto_impuesto_td["TipoFactor"].ToString();


                                if (r_concepto_impuesto_td["TipoFactor"].ToString() != "Exento")
                                {
                                    // Tasa o cuota
                                    if (r_concepto_impuesto_td["TasaCuota"].ToString() == "Definir %")
                                    {
                                        tasacuota = Convert.ToDecimal(r_concepto_impuesto_td["Definir"]);

                                        if (r_concepto_impuesto_td["TipoFactor"].ToString() != "Cuota")
                                        {
                                            if (tasacuota > 1)
                                            {
                                                tasacuota = tasacuota / 100;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string porcentaje = r_concepto_impuesto_td["TasaCuota"].ToString(); 
                                        //porcentaje = porcentaje.Substring(0, -2);  
                                        tasacuota = Convert.ToDecimal(porcentaje);

                                        if (tasacuota > 1)
                                        {
                                            tasacuota = tasacuota / 100;
                                        }
                                    }

                                    concepto_traslado.TasaOCuota = tasacuota;
                                    // Importe
                                    importe = Convert.ToDecimal(r_concepto_impuesto_td["Importe"]) * cantidad;
                                    concepto_traslado.Importe = importe;
                                }
                                    

                                list_concepto_impuestos_traslados.Add(concepto_traslado);


                                // Guarda en la lista el tipo de impuesto
                                string cadena = t_impuesto + "-" + r_concepto_impuesto_td["TipoFactor"].ToString() + "-" + tasacuota;
                               
                                // Busca si la cadena existe en la lista
                                var indice = list_porprod_impuestos_trasladados.IndexOf(cadena);

                                // Si la cadena existe aumenta el importe del impuesto, de lo contrario la agrega como nueva
                                if (indice >= 0)
                                {
                                    indice = indice + 1;
                                    decimal monto_actual = Convert.ToDecimal(list_porprod_impuestos_trasladados[indice]);
                                    decimal monto_nuevo = monto_actual + importe;

                                    list_porprod_impuestos_trasladados.RemoveAt(indice);
                                    list_porprod_impuestos_trasladados.Insert(indice, Convert.ToString(monto_nuevo));
                                }
                                else
                                {
                                    list_porprod_impuestos_trasladados.Add(cadena);
                                    list_porprod_impuestos_trasladados.Add(importe.ToString());
                                }
                            }

                            // Impuestos locales

                            if (r_concepto_impuesto_td["Tipo"].ToString() == "Loc. Traslado" | r_concepto_impuesto_td["Tipo"].ToString() == "Loc. Retenido")
                            {
                                decimal tasa = 0;

                                if(r_concepto_impuesto_td["TasaCuota"].ToString() == "Definir %")
                                {
                                    tasa = Convert.ToDecimal(r_concepto_impuesto_td["Definir"]);

                                    if (Convert.ToDecimal(r_concepto_impuesto_td["Definir"]) < 1)
                                    {
                                        tasa = Convert.ToDecimal(r_concepto_impuesto_td["Definir"]) * 100;
                                    }
                                }
                                else
                                {
                                    tasa = Convert.ToDecimal(r_concepto_impuesto_td["TasaCuota"]);

                                    if (Convert.ToDecimal(r_concepto_impuesto_td["TasaCuota"]) < 1)
                                    {
                                        tasa = Convert.ToDecimal(r_concepto_impuesto_td["TasaCuota"]) * 100;
                                    }
                                }

                                string cadena = r_concepto_impuesto_td["Impuesto"].ToString() + "-" + tasa;

                                if (r_concepto_impuesto_td["Tipo"].ToString() == "Loc. Traslado")
                                {
                                    list_impuestos_local_traslado.Add(cadena);
                                    list_impuestos_local_traslado.Add(r_concepto_impuesto_td["Importe"].ToString());
                                }
                                if (r_concepto_impuesto_td["Tipo"].ToString() == "Loc. Retenido")
                                {
                                    list_impuestos_local_traslado.Add(cadena);
                                    list_impuestos_local_traslado.Add(r_concepto_impuesto_td["Importe"].ToString());
                                }                                
                            }
                        }
                    }



                    // NODO IMPUESTOS RETENIDOS POR PRODUCTO
                    //----------------------------------------


                    
                    List<ComprobanteConceptoImpuestosRetencion> list_concepto_impuestos_retenidos = new List<ComprobanteConceptoImpuestosRetencion>();

                    // Consulta si hay impuestos retenidos
                    d_concepto_impuesto_r = cn.CargarDatos(cs.cargar_datos_venta_xml(8, id_producto, 0));

                    if(d_concepto_impuesto_r.Rows.Count > 0)
                    {
                        string r_impuesto = "";
                        agrega_nodo_concepto_retencion = 1;

                        foreach (DataRow r_concepto_impuesto_r in d_concepto_impuesto_r.Rows)
                        {
                            decimal importe = 0;

                            if (r_concepto_impuesto_r["Tipo"].ToString() == "Retención")
                            {
                                agrega_nodo_concepto_retencion = 1;

                                ComprobanteConceptoImpuestosRetencion concepto_retencion = new ComprobanteConceptoImpuestosRetencion();

                                // Base
                                if(r_concepto_impuesto_r["TipoFactor"].ToString() == "Cuota")
                                {
                                    concepto_retencion.Base = cantidad;
                                }
                                else
                                {
                                    concepto_retencion.Base = importe_base * cantidad;
                                }

                                // Impuesto
                                if (r_concepto_impuesto_r["Impuesto"].ToString() == "ISR") { r_impuesto = "001"; }
                                if (r_concepto_impuesto_r["Impuesto"].ToString() == "IVA") { r_impuesto = "002"; }
                                if (r_concepto_impuesto_r["Impuesto"].ToString() == "IEPS") { r_impuesto = "003"; }

                                concepto_retencion.Impuesto = r_impuesto;

                                // Tipo factor
                                concepto_retencion.TipoFactor = r_concepto_impuesto_r["TipoFactor"].ToString();

                                // Tasa o cuota
                                decimal tasa_cuota = 0;

                                //if (r_concepto_impuesto_r["TasaCuota"].ToString() == "Definir %")
                                if (Convert.ToDecimal(r_concepto_impuesto_r["Definir"]) > 0)
                                {
                                    if (r_concepto_impuesto_r["TipoFactor"].ToString() == "Cuota"){
                                        tasa_cuota = Convert.ToDecimal(r_concepto_impuesto_r["Definir"]);
                                    }
                                    else
                                    {
                                        if(Convert.ToDecimal(r_concepto_impuesto_r["Definir"]) > 1)
                                        {
                                            decimal porcentaje = Convert.ToDecimal(r_concepto_impuesto_r["Definir"]);
                                            tasa_cuota = porcentaje / 100; 
                                        }
                                    }
                                }
                                else
                                {
                                    string porcentaje = r_concepto_impuesto_r["TasaCuota"].ToString();

                                    if (Convert.ToDecimal(porcentaje) > 1)
                                    {
                                        tasa_cuota = Convert.ToDecimal(porcentaje) / 100;
                                    }
                                }
                                concepto_retencion.TasaOCuota = tasa_cuota;
                                // Importe
                                importe = Convert.ToDecimal(r_concepto_impuesto_r["Importe"]) * cantidad;
                                concepto_retencion.Importe = importe;
                                       

                                list_concepto_impuestos_retenidos.Add(concepto_retencion);


                                // Hace sumatoria de impuestos
                                if (r_impuesto == "001")
                                {
                                    total_ISR += importe;
                                }
                                if (r_impuesto == "002")
                                {
                                    total_IVA += importe;
                                }
                                if (r_impuesto == "003")
                                {
                                    total_IEPS += importe;
                                }
                            }
                        }
                    }



                    // AGREGA AL NODO IMPUESTOS LOS TRASLADADOS Y RETENIDOS
                    //-----------------------------------------------------

                    if(agrega_nodo_concepto_traslado == 1 | agrega_nodo_concepto_retencion == 1)
                    {
                        concepto.Impuestos = new ComprobanteConceptoImpuestos();

                        if(agrega_nodo_concepto_traslado == 1)
                        {
                            concepto.Impuestos.Traslados = list_concepto_impuestos_traslados.ToArray();
                        }
                        if (agrega_nodo_concepto_retencion == 1)
                        {
                            concepto.Impuestos.Retenciones = list_concepto_impuestos_retenidos.ToArray();
                        }  
                    }
                    
                    listaConceptos.Add(concepto);
                }
            }

            comprobante.Conceptos = listaConceptos.ToArray();



            // NODO IMPUESTOS GENERALES
            //-------------------------
            

            // Verificar si existe algún impuesto exento, si solo hay impuestos exentos no se agrega el nodo impuestos

            string buscar_cadena = "002-Tasa-Exento-0";
            var indice_bs = list_porprod_impuestos_trasladados.IndexOf(buscar_cadena);
            int tam_list_imp_t = list_porprod_impuestos_trasladados.Count;
            int tam_list_imp_r = 0;
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

            if(total_ISR > 0 | total_IVA > 0 | total_IEPS > 0) {
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

                        if(t_factor != "Exento")
                        {
                            ComprobanteImpuestosTraslado impuestos_traslado = new ComprobanteImpuestosTraslado();
                            
                            impuestos_traslado.Impuesto = dato_it[0];
                            impuestos_traslado.TipoFactor = t_factor;
                            impuestos_traslado.TasaOCuota = t_cuota;
                            impuestos_traslado.Importe = Convert.ToDecimal(list_porprod_impuestos_trasladados[c_impt + 1]);

                            list_impuestos_traslado.Add(impuestos_traslado);

                            suma_impuesto_traslado += Convert.ToDecimal(list_porprod_impuestos_trasladados[c_impt + 1]);
                        }

                        c_impt += 2;
                    }
                }

                // Retenidos

                if(no_agrega_nodo_retenido == 0)
                {
                    if(total_ISR > 0)
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
                    nd_impuestos_g.TotalImpuestosTrasladados = suma_impuesto_traslado;

                    nd_impuestos_g.Traslados = list_impuestos_traslado.ToArray();
                }
                if(no_agrega_nodo_retenido == 0)
                {
                    nd_impuestos_g.TotalImpuestosRetenidosSpecified = true;
                    nd_impuestos_g.TotalImpuestosRetenidos = suma_impuesto_retenido;

                    nd_impuestos_g.Retenciones = list_impuestos_retenido.ToArray();
                }
                    
                comprobante.Impuestos = nd_impuestos_g;
            }





            // NODO IMPUESTOS LOCALES
            //------------------------


            // Consulta si hay impuestos retenidos
            //d_concepto_impuesto_r = cn.CargarDatos(cs.cargar_datos_venta_xml(8, id_producto, 0));




            // DATOS DEL NODO PRINCIPAL "COMPROBANTE"
            //---------------------------------------


            decimal total_general = (suma_total_productos + suma_impuesto_traslado) - suma_impuesto_retenido;


            comprobante.Version = "3.3";

            if (serie != "")
            {
                comprobante.Serie = serie;
            }
            if (folio != "")
            {
                comprobante.Folio = folio;
            }

            comprobante.Fecha = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss");

            if (forma_pago != "")
            {
                comprobante.FormaPago = forma_pago;
            }
            //comprobante.CondicionesDePago = "Vacio"; //EN DUDA SI SE PONDRÁ
            comprobante.NoCertificado = numero_certificado;
            comprobante.SubTotal = suma_total_productos;
            if (descuento_general > 0)
            {
                comprobante.Descuento = Convert.ToInt32(descuento_general);
            }

            comprobante.Moneda = moneda;
            comprobante.TipoCambio = 1.000000m;
            comprobante.Total = total_general;
            comprobante.TipoDeComprobante = "I";
            comprobante.MetodoPago = metodo_pago;
            comprobante.LugarExpedicion = lugar_expedicion;
            

            //comprobante.TipoCambioSpecified = true;




            string rutaXML = @"C:\Users\Miri\Desktop\XML_"+id_venta+".xml";

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

                string[] datos = new string[] { id_venta.ToString() };

                cn.EjecutarConsulta(cs.guarda_datos_faltantes_xml(4, datos));
            }
            catch (FaultException fex)
            {
                var codigo = fex.Code.ToString();

                //MessageBox.Show("CODIGO ERROR= "+codigo +" --- "+fex.Message, "Error en XML", MessageBoxButtons.OK);
                mensaje = "CODIGO ERROR= " + codigo + " --- " + fex.Message;
            }
            catch (XmlException e_xml)
            {
                //MessageBox.Show(e_xml.Message, "Error", MessageBoxButtons.OK);
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
    }
}
