using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PuntoDeVentaV2
{
    public partial class Complemento_pago : Form 
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        int id_usuario = FormPrincipal.userID;
        int id_empleado = FormPrincipal.id_empleado;
        int tam_arr = Facturas.arr_id_facturas.Length;
        string[][] arr_totales;

        static public string clave_moneda = "";
        static public bool ban_moneda = false;
        static public string[][][] arr_impuestos;
        static public int[] arr_nfilas;




        public Complemento_pago()
        {
            InitializeComponent();
        }
               

        private void Complemento_pago_Load(object sender, EventArgs e)
        {
            cmb_bx_forma_pago.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);

            txt_tipo_cambio_pago.KeyPress += new KeyPressEventHandler(solo_decimales);


            // Forma de pago

            Dictionary<string, string> forma_pago = new Dictionary<string, string>();
            forma_pago.Add("01", "01 - Efectivo");
            forma_pago.Add("02", "02 - Cheque nominativo");
            forma_pago.Add("03", "03 - Transferencia electrónica de fondos");
            forma_pago.Add("04", "04 - Tarjeta de crédito");
            forma_pago.Add("05", "05 - Monedero electrónico");
            forma_pago.Add("06", "06 - Dinero electrónico");
            forma_pago.Add("08", "08 - Vales de despensa");
            forma_pago.Add("12", "12 - Dación en pago");
            forma_pago.Add("13", "13 - Pago por subrogación");
            forma_pago.Add("14", "14 - Pago por consignación");
            forma_pago.Add("15", "15 - Condonación");
            forma_pago.Add("17", "17 - Compensación");
            forma_pago.Add("23", "23 - Novación");
            forma_pago.Add("24", "24 - Confusión");
            forma_pago.Add("25", "25 - Remisión de deuda");
            forma_pago.Add("26", "26 - Prescripción o caducidad");
            forma_pago.Add("27", "27 - A satisfacción del acreedor");
            forma_pago.Add("28", "28 - Tarjeta de débito");
            forma_pago.Add("29", "29 - Tarjeta de servicios");
            forma_pago.Add("30", "30 - Aplicación de anticipos");
            forma_pago.Add("31", "31 - Intermediario pagos");

            cmb_bx_forma_pago.DataSource = forma_pago.ToArray();
            cmb_bx_forma_pago.DisplayMember = "Value";
            cmb_bx_forma_pago.ValueMember = "Key";
            cmb_bx_forma_pago.SelectedIndex = 0;


            // Datos de la forma de pago

            txt_cuenta.GotFocus += new EventHandler(encampo_cuenta);
            txt_cuenta.LostFocus += new EventHandler(scampo_cuenta);
            txt_cuenta.KeyPress += new KeyPressEventHandler(cta_ordenante);

            txt_rfc_ordenante.GotFocus += new EventHandler(encampo_cuenta);
            txt_rfc_ordenante.LostFocus += new EventHandler(scampo_cuenta);

            txt_banco.GotFocus += new EventHandler(encampo_cuenta);
            txt_banco.LostFocus += new EventHandler(scampo_cuenta);
            
            txt_cuenta_beneficiario.GotFocus += new EventHandler(encampo_cuenta);
            txt_cuenta_beneficiario.LostFocus += new EventHandler(scampo_cuenta);
            txt_cuenta_beneficiario.KeyPress += new KeyPressEventHandler(cta_beneficiario);

            txt_rfc_beneficiario.GotFocus += new EventHandler(encampo_cuenta);
            txt_rfc_beneficiario.LostFocus += new EventHandler(scampo_cuenta);


            // Fecha actual

            DateTime f_actual = DateTime.Now;
            string fecha_actual = f_actual.ToShortDateString();
            datetime_fecha_pago.Text = fecha_actual;


            // Facturas a pagar/abonar

            if (tam_arr > 0)
            {
                int location_y = 6;
                int nfila = 1;

                for (int i=0; i<tam_arr; i++)
                {
                    int id_f = Facturas.arr_id_facturas[i];


                    // Busca total, folio y serie de la factura a pagar/abonar
                    DataTable d_factura = cn.CargarDatos(cs.obtener_datos_para_gcpago(1, id_f));

                    if (d_factura.Rows.Count > 0)
                    {
                        DataRow r_factura = d_factura.Rows[0];

                        string total_f = r_factura["total"].ToString();
                        decimal suma_importe_pagado = 0;

                        if (Convert.ToInt32(r_factura["con_complementos"]) == 1)
                        {
                            //decimal tf_apagar = Convert.ToDecimal(cn.EjecutarSelect($"SELECT importe_pagado FROM Facturas_complemento_pago WHERE id_factura_principal='{id_f}' AND timbrada=1 AND cancelada=0 ORDER BY ID DESC LIMIT 1", 9));
                            DataTable d_comp_pago = cn.CargarDatos(cs.obtener_datos_para_gcpago(3, id_f));
                            
                            if(d_comp_pago.Rows.Count > 0)
                            {
                                foreach(DataRow r_comp_pago in d_comp_pago.Rows)
                                {
                                    suma_importe_pagado += Convert.ToDecimal(r_comp_pago["importe_pagado"].ToString());
                                }
                            }
                            if(suma_importe_pagado > 0)
                            {
                                decimal restan = Convert.ToDecimal(total_f) - suma_importe_pagado;
                                total_f = Convert.ToString(restan);
                            }
                            /*total_f = tf_apagar.ToString();
                            
                            if(total_f == "0")
                            {
                                total_f = r_factura["total"].ToString();
                            }*/
                        }

                        Label lb_c_folio_serie = new Label();
                        lb_c_folio_serie.Name = "lb_folio_serie" + nfila;
                        lb_c_folio_serie.Location = new Point(12, location_y);
                        lb_c_folio_serie.Text = r_factura["folio"].ToString() + " " + r_factura["serie"].ToString();
                        
                        Label lb_c_total = new Label();
                        lb_c_total.Name = "lb_total" + nfila;
                        lb_c_total.Location = new Point(97, location_y);
                        lb_c_total.Text = total_f;

                        TextBox txt_c_total = new TextBox();
                        txt_c_total.Name = "txt_total" + nfila;
                        txt_c_total.Location = new Point(207, location_y);
                        txt_c_total.Size = new Size(100, 22);
                        txt_c_total.TextAlign = HorizontalAlignment.Center;
                        txt_c_total.Text = total_f;
                        txt_c_total.KeyPress += new KeyPressEventHandler(solo_decimales);

                        TextBox txt_c_moneda_dr = new TextBox();
                        txt_c_moneda_dr.Name = "txt_moneda_dr" + nfila;
                        txt_c_moneda_dr.Location = new Point(320, location_y);
                        txt_c_moneda_dr.Size = new Size(60, 22);
                        txt_c_moneda_dr.TextAlign = HorizontalAlignment.Center;
                        txt_c_moneda_dr.ReadOnly = true;
                        txt_c_moneda_dr.Click += new EventHandler(abrir_vnt_moneda);

                        TextBox txt_c_tcambio_dr = new TextBox();
                        txt_c_tcambio_dr.Name = "txt_tcambio_dr" + nfila;
                        txt_c_tcambio_dr.Location = new Point(390, location_y);
                        txt_c_tcambio_dr.Size = new Size(100, 22);
                        txt_c_tcambio_dr.TextAlign = HorizontalAlignment.Center;
                        txt_c_tcambio_dr.Enabled = false;
                        txt_c_tcambio_dr.KeyPress += new KeyPressEventHandler(solo_decimales);

                        ComboBox cmb_bx_c_inc_impuestos = new ComboBox();
                        cmb_bx_c_inc_impuestos.Name = "cmb_bx_incluye_impuestos-" + nfila;
                        cmb_bx_c_inc_impuestos.Items.Add("No objeto de impuesto.");
                        cmb_bx_c_inc_impuestos.Items.Add("Sí objeto de impuesto.");
                        cmb_bx_c_inc_impuestos.Items.Add("Sí objeto del impuesto y no obligado al desglose.");
                        cmb_bx_c_inc_impuestos.Items.Add("Sí objeto del impuesto y no causa impuesto.");
                        cmb_bx_c_inc_impuestos.SelectedIndex = 0;
                        cmb_bx_c_inc_impuestos.DropDownStyle = ComboBoxStyle.DropDownList;
                        cmb_bx_c_inc_impuestos.Width = 300;
                        cmb_bx_c_inc_impuestos.Location = new Point(500, location_y);
                        cmb_bx_c_inc_impuestos.SelectedValueChanged += new EventHandler(sel_inc_impuestos);

                        Button btn_agregar_impuestos = new Button();
                        btn_agregar_impuestos.Name = "btn_agregar_impuestos-" + nfila;
                        btn_agregar_impuestos.Location = new Point(815, location_y);
                        btn_agregar_impuestos.Size = new Size(70, 28);
                        btn_agregar_impuestos.Text = "Agregar";
                        btn_agregar_impuestos.BackColor = Color.Teal;
                        btn_agregar_impuestos.ForeColor = Color.White;
                        btn_agregar_impuestos.Cursor = Cursors.Hand;
                        btn_agregar_impuestos.FlatStyle = FlatStyle.Flat;
                        btn_agregar_impuestos.Enabled = false;
                        btn_agregar_impuestos.Click += new EventHandler(abrir_vnt_impuestos);

                        Label lb_idf = new Label();
                        lb_idf.Name = "lb_idf-" + nfila;
                        lb_idf.Location = new Point(890, location_y);
                        lb_idf.Size = new Size(10, 28);
                        lb_idf.Text = id_f.ToString();
                        lb_idf.Visible = false;


                        pnl_info.Controls.Add(lb_c_folio_serie);
                        pnl_info.Controls.Add(lb_c_total);
                        pnl_info.Controls.Add(txt_c_total);
                        pnl_info.Controls.Add(txt_c_moneda_dr);
                        pnl_info.Controls.Add(txt_c_tcambio_dr);
                        pnl_info.Controls.Add(cmb_bx_c_inc_impuestos);
                        pnl_info.Controls.Add(btn_agregar_impuestos);
                        pnl_info.Controls.Add(lb_idf);


                        location_y = location_y + 30;
                        nfila++;
                    }
                }

                arr_impuestos = new string[tam_arr][][];
                arr_nfilas = new int[tam_arr];
            }
        }

        private void solo_decimales(object sender, KeyPressEventArgs e)
        {
            if(Char.IsDigit(e.KeyChar) | Char.IsPunctuation(e.KeyChar) | Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            // Si se tiene una conexión a internet procede a realizar el complemento.
            if (Conexion.ConectadoInternet())
            {
                int t = 0, error = 0, error_monto_mayor = 0, erro_mon_tcam = 0, error_inc_impuestos = 0;
                arr_totales = new string[tam_arr][];


                // ...............................
                // .   Validar y guardar datos   .
                // ...............................

                var resp = val_campos_forma_pago_moneda();

                if(resp != "")
                {
                    MessageBox.Show(resp, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }

                foreach (Control panel in pnl_info.Controls)
                {
                    if (panel.Name.Contains("lb_total"))
                    {
                        arr_totales[t] = new string[6];

                        arr_totales[t][1] = panel.Text;
                    }
                    if (panel.Name.Contains("txt_total"))
                    {
                        arr_totales[t][2] = panel.Text;

                        if (panel.Text.Trim() == "")
                        {
                            error++;
                        }
                        if (panel.Text.Trim() != "")
                        {
                            if (Convert.ToDecimal(panel.Text) == 0)
                            {
                                error++;
                            }
                        }

                        // Valida que el abono no sea mayor a lo que debe

                        if(error == 0)
                        {
                            decimal resta = Convert.ToDecimal(arr_totales[t][1]);
                            decimal abono = Convert.ToDecimal(arr_totales[t][2]);

                            if (abono > 0)
                            {
                                if (resta >= abono)
                                {
                                    arr_totales[t][0] = Facturas.arr_id_facturas[t].ToString();
                                }
                                else
                                {
                                    error_monto_mayor++;
                                }
                            }
                            else
                            {
                                error_monto_mayor++;
                            }
                        }                        
                    }
                    if (panel.Name.Contains("txt_moneda_dr"))
                    {
                        if (string.IsNullOrEmpty(panel.Text)){
                            erro_mon_tcam++;
                        }

                        arr_totales[t][3] = panel.Text;
                    }
                    if (panel.Name.Contains("txt_tcambio_dr"))
                    {
                        if (string.IsNullOrEmpty(panel.Text))
                        {
                            erro_mon_tcam++;
                        }

                        arr_totales[t][4] = panel.Text;
                    }
                    if (panel.Name.Contains("cmb_bx_incluye_impuestos"))
                    {
                        if (panel.Text == "Sí objeto de impuesto.")
                        {
                            // Busca si la factura a pagar incluye impuestos. Si los incluye se habilita el botón 

                            int id_fct = Convert.ToInt32(arr_totales[t][0]);


                            bool exi = (bool)cn.EjecutarSelect($"SELECT ID FROM Facturas_productos WHERE id_factura='{id_fct}' AND incluye_impuestos='02'", 0);

                            if(exi == false & panel.Text == "Sí objeto de impuesto.")
                            {
                                error_inc_impuestos++;
                            }
                        }

                        arr_totales[t][5] = panel.Text;

                        t++;
                    }
                }

                if (error > 0)
                {
                    MessageBox.Show("Algún campo abono esta vacío, o el valor es menor a cero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (error_monto_mayor > 0)
                {
                    MessageBox.Show("El abono en alguna factura es mayor al total restante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (erro_mon_tcam > 0)
                {
                    MessageBox.Show("Se debe especificar una moneda y tipo de cambio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if(error_inc_impuestos > 0)
                {
                    MessageBox.Show("Alguno de los documentos tiene especificado que es objeto de impuesto, sin embargo no todos los documentos seleccionados lo incluye.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                MessageBox.Show("El complemento de pago tardará 10 segundos (aproximadamente) en ser creado y timbrado, un momento por favor.", "Mensaje del sistema", MessageBoxButtons.OK);

                btn_aceptar.Cursor = Cursors.No;
                btn_cancelar.Cursor = Cursors.No;
                btn_aceptar.Enabled = false;
                btn_cancelar.Enabled = false;




                // .........................  
                // .   Crear complemento   .
                // .........................


                // Crea registro en tabla factura

                // ID de la factura donde se obtendran los datos del receptor
                int id_f_receptor = Convert.ToInt32(arr_totales[0][0]);
                

                // Buscamos los datos del receptor

                DataTable d_receptor = cn.CargarDatos(cs.obtener_datos_para_gcpago(1, id_f_receptor));
                DataRow r_recetor = d_receptor.Rows[0];

                string folio = r_recetor["folio"].ToString();
                string serie = r_recetor["serie"].ToString();

                // Datos del emisor

                DataTable d_emisor = cn.CargarDatos(cs.cargar_datos_venta_xml(2, 0, id_usuario));
                DataRow r_emisor = d_emisor.Rows[0];

                // Se obtiene la cantidad de complementos generados para continuar con el consecutivo

                int cant_complementos = Convert.ToInt32(cn.EjecutarSelect($"SELECT COUNT(ID) AS ID FROM Facturas_complemento_pago WHERE id_factura_principal='{id_f_receptor}'", 1));
                cant_complementos = cant_complementos + 1;
                serie = serie + "-" + cant_complementos;

                string[] datos_f = new string[]
                {
                id_usuario.ToString(), id_empleado.ToString(), folio, serie, "1",// fecha_hora_pago,
                r_recetor["r_rfc"].ToString(), r_recetor["r_razon_social"].ToString(), r_recetor["r_nombre_comercial"].ToString(), r_recetor["r_correo"].ToString(), r_recetor["r_telefono"].ToString(), r_recetor["r_pais"].ToString(), r_recetor["r_estado"].ToString(),
                r_recetor["r_municipio"].ToString(), r_recetor["r_localidad"].ToString(), r_recetor["r_cp"].ToString(), r_recetor["r_colonia"].ToString(), r_recetor["r_calle"].ToString(), r_recetor["r_num_ext"].ToString(), r_recetor["r_num_int"].ToString(),
                r_emisor["RFC"].ToString(), r_emisor["RazonSocial"].ToString(), r_emisor["Regimen"].ToString(), r_emisor["Email"].ToString(), r_emisor["Telefono"].ToString(), r_emisor["CodigoPostal"].ToString(),
                r_emisor["Estado"].ToString(), r_emisor["Municipio"].ToString(), r_emisor["Colonia"].ToString(), r_emisor["Calle"].ToString(), r_emisor["NoExterior"].ToString(), r_emisor["NoInterior"].ToString(),
                 r_recetor["r_regimen"].ToString()}; // cmb_bx_forma_pago.SelectedValue.ToString(), txt_cuenta.Text

                cn.EjecutarConsulta(cs.crear_complemento_pago(1, datos_f));


                // Consulta id de la factura recien creada
                int id_factura_pago = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Facturas WHERE id_usuario='{id_usuario}' AND tipo_comprobante='P' ORDER BY ID DESC LIMIT 1", 1));


                // Se agrega registro a tabla de Facturas_productos

                string[] datos_fp = new string[] { id_factura_pago.ToString() };
                cn.EjecutarConsulta(cs.crear_complemento_pago(2, datos_fp));




                // Se agrega registro con los datos del complemento de pago

                decimal monto_pago = 0;
                decimal[][] arr_idf_principal_pago = new decimal[arr_totales.Length][];
                int x = 0;
                string fecha_pago = datetime_fecha_pago.Value.ToString("yyy-MM-dd");
                string hora_pago = datetime_hora_pago.Value.ToString("hh:mm:ss");
                string cuenta_or = "", rfc_or = "";
                string banco = "";
                string cuenta_be = "", rfc_be = "";
                string forma_pago = cmb_bx_forma_pago.SelectedValue.ToString();


                if (forma_pago == "02" | forma_pago == "03" | forma_pago == "04" | forma_pago == "28" | forma_pago == "29") // Cheque y transferencia
                {
                    if (txt_cuenta.Text != "(Opcional) No. cuenta ordenante")
                    {
                        cuenta_or = txt_cuenta.Text;
                    }
                    if (txt_rfc_ordenante.Text != "(OPCIONAL) RFC")
                    {
                        rfc_or = txt_rfc_ordenante.Text;
                    }
                    if(txt_banco.Text != "(Opcional) Nombre banco")
                    {
                        banco = txt_banco.Text;
                    }
                    if (txt_cuenta_beneficiario.Text != "(Opcional) No. cuenta beneficiario")
                    {
                        cuenta_be = txt_cuenta_beneficiario.Text;
                    }
                    if (txt_rfc_beneficiario.Text != "(OPCIONAL) RFC.")
                    {
                        rfc_be = txt_rfc_beneficiario.Text;
                    }
                        
                }
                if (forma_pago == "05" | forma_pago == "06") 
                {
                    if (txt_cuenta.Text != "(Opcional) No. cuenta ordenante")
                    {
                        cuenta_or = txt_cuenta.Text;
                    }
                    if (txt_rfc_ordenante.Text != "(OPCIONAL) RFC")
                    {
                        rfc_or = txt_rfc_ordenante.Text;
                    }

                    if(forma_pago == "05")
                    {
                        if (txt_cuenta_beneficiario.Text != "(Opcional) No. cuenta beneficiario")
                        {
                            cuenta_be = txt_cuenta_beneficiario.Text;
                        }
                        if (txt_rfc_beneficiario.Text != "(OPCIONAL) RFC.")
                        {
                            rfc_be = txt_rfc_beneficiario.Text;
                        }
                    }                    
                }

                // Agrega primer registro donde se almacenarán datos para el nodo pago

                string[] datos_nd_pago = new string[]
                {
                    id_factura_pago.ToString(), fecha_pago, hora_pago, txt_moneda_pago.Text, txt_tipo_cambio_pago.Text, forma_pago, cuenta_or, rfc_or, banco, cuenta_be, rfc_be
                };

                cn.EjecutarConsulta(cs.crear_complemento_pago(7, datos_nd_pago));

                // Consulta id del registro recién agregado con datos del nodo pago
                int id_pago = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Facturas_complemento_pago WHERE id_factura='{id_factura_pago}' ORDER BY ID DESC LIMIT 1", 1));


                // Agrega registro con información del documento relacionado

                for (int r = 0; r < arr_totales.Length; r++)
                {
                    // Obtiene UUID, folio y serie de la factura que se esta abonando/pagando

                    //string uuid = Convert.ToString(cn.EjecutarSelect($"SELECT uuid FROM Facturas WHERE ID='{arr_totales[r][0]}'", 10));
                    DataTable d_dts_facturap= cn.CargarDatos(cs.obtener_datos_para_gcpago(6, Convert.ToInt32(arr_totales[r][0])));
                    DataRow r_dts_facturap = d_dts_facturap.Rows[0];

                    string uuid = r_dts_facturap["uuid"].ToString();
                    string folio_fp = r_dts_facturap["folio"].ToString();
                    string incluye_impuestos = "";

                    if (arr_totales[r][5].ToString() == "No objeto de impuesto.")
                    {
                        incluye_impuestos = "01";
                    }
                    if (arr_totales[r][5].ToString() == "Sí objeto de impuesto.")
                    {
                        incluye_impuestos = "02";
                    }
                    if (arr_totales[r][5].ToString() == "Sí objeto del impuesto y no obligado al desglose.")
                    {
                        incluye_impuestos = "03";
                    }
                    if (arr_totales[r][5].ToString() == "Sí objeto del impuesto y no causa impuesto.")
                    {
                        incluye_impuestos = "04";
                    }

                    // Obtiene el número de la parcialidad anterior

                    int n_parcialidad = Convert.ToInt32(cn.EjecutarSelect($"SELECT COUNT(ID) AS ID FROM Facturas_complemento_pago WHERE id_factura_principal='{arr_totales[r][0]}'  AND timbrada=1 AND cancelada=0", 1));
                    n_parcialidad = n_parcialidad + 1;

                    decimal saldo_insoluto = Convert.ToDecimal(arr_totales[r][1]) - Convert.ToDecimal(arr_totales[r][2]);


                    string[] datos_nd_drelac = new string[]
                    {
                        id_factura_pago.ToString(), id_pago.ToString(), arr_totales[r][0].ToString(), uuid, arr_totales[r][3], arr_totales[r][4], n_parcialidad.ToString(), arr_totales[r][1], arr_totales[r][2], saldo_insoluto.ToString(), folio_fp, incluye_impuestos
                    };

                    cn.EjecutarConsulta(cs.crear_complemento_pago(3, datos_nd_drelac));


                    // Obtiene el id del documento relacionado recién agregado.
                    int id_drelac = Convert.ToInt32(cn.EjecutarSelect($"SELECT ID FROM Facturas_complemento_pago WHERE id_factura='{id_factura_pago}' AND  id_doc_relac='{id_pago}' AND id_factura_principal='{arr_totales[r][0]}' ORDER BY ID DESC LIMIT 1", 1));


                    // Guarda el id de la factura principal de la que se esta haciendo el pago/abono.
                    // Si la factura se timbra, se indica que tiene complemento de pago
                    arr_idf_principal_pago[x] = new decimal[2];
                    arr_idf_principal_pago[x][0] = Convert.ToDecimal(arr_totales[r][0]);
                    arr_idf_principal_pago[x][1] = Convert.ToDecimal(saldo_insoluto);
                    x++;


                    // Agrega impuestos 

                    if (arr_totales[r][5].ToString() == "Sí objeto de impuesto.")
                    {
                        
                        int tam_arr_i = arr_impuestos[r].Count();

                        for (int j = 0; j < tam_arr_i; j++) 
                        {
                            if(arr_impuestos[r][j][0] == arr_totales[r][0])
                            {
                                string[] datos_nd_impuests = new string[]
                                {
                                    id_factura_pago.ToString(), id_drelac.ToString(),
                                    arr_impuestos[r][j][1].ToString(), arr_impuestos[r][j][2].ToString(), arr_impuestos[r][j][3].ToString(), arr_impuestos[r][j][4].ToString(),
                                    arr_impuestos[r][j][5].ToString(), arr_impuestos[r][j][6].ToString(), arr_impuestos[r][j][7].ToString()
                                };

                                cn.EjecutarConsulta(cs.crear_complemento_pago(8, datos_nd_impuests));
                            }
                        }
                    }
                }


                // Agrega el monto pagado a la factura principal 
                /*string[] datos_fm = new string[]
                {
                    id_factura_pago.ToString(), monto_pago.ToString()
                };

                cn.EjecutarConsulta(cs.crear_complemento_pago(6, datos_fm));*/




                // ...........................
                // .   Timbrar complemento   .
                // ...........................


                Generar_XML xml_complemento = new Generar_XML();
                string respuesta_xml = xml_complemento.obtener_datos_XML(id_factura_pago, 0, 1, arr_idf_principal_pago);

                btn_aceptar.Enabled = true;
                btn_aceptar.Cursor = Cursors.Hand;
                btn_cancelar.Enabled = true;
                btn_cancelar.Cursor = Cursors.Hand;

                if (respuesta_xml == "")
                {
                    var r = MessageBox.Show("El complemento de pago ha sido creado y timbrado con éxito.", "Éxito", MessageBoxButtons.OK);

                    if (r == DialogResult.OK)
                    {
                        this.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show(respuesta_xml, "Error", MessageBoxButtons.OK);
                }

                Complemento_pago_impuestos.dats_en_arr = false;
            }
            else
            {
                MessageBox.Show("Sin conexión a internet. Esta accción requiere una conexión.", "", MessageBoxButtons.OK);
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void sel_forma_pago(object sender, EventArgs e)
        {
            int clave = Convert.ToInt32(cmb_bx_forma_pago.SelectedValue);

            if(clave == 01 | clave == 08 | (clave >= 12 & clave <= 27) | clave == 30)
            {
                limpiar_campos_forma_pago();

                txt_cuenta.Enabled = false;
                txt_cuenta.Cursor = Cursors.No;

                txt_rfc_ordenante.Enabled = false;
                txt_banco.Enabled = false;
                txt_cuenta_beneficiario.Enabled = false;
                txt_rfc_beneficiario.Enabled = false;
            }
            else
            {
                limpiar_campos_forma_pago();

                txt_cuenta.Enabled = true;
                txt_cuenta.Cursor = Cursors.IBeam;

                if (clave == 02 | clave == 03) // Dígitos. Cheque y transferencia
                {
                    txt_cuenta.MaxLength = 18;

                    if(clave == 02)
                    {
                        txt_cuenta_beneficiario.MaxLength = 50;
                    }
                    if (clave == 03)
                    {
                        txt_cuenta_beneficiario.MaxLength = 18;
                    }

                    txt_rfc_ordenante.Enabled = true;
                    txt_banco.Enabled = true;
                    txt_cuenta_beneficiario.Enabled = true;
                    txt_rfc_beneficiario.Enabled = true;
                }
                if (clave == 04 | clave == 28 | clave == 29) // Dígitos. Tarjeta de crédito, debito y servicios
                {
                    txt_cuenta.MaxLength = 16;
                    txt_cuenta_beneficiario.MaxLength = 50;

                    txt_rfc_ordenante.Enabled = true;
                    txt_banco.Enabled = true;
                    txt_cuenta_beneficiario.Enabled = true;
                    txt_rfc_beneficiario.Enabled = true;
                }
                if (clave == 05) // Alfanumérica. Monedero electrónico
                {
                    txt_cuenta.MaxLength = 50;
                    txt_cuenta_beneficiario.MaxLength = 50;

                    txt_rfc_ordenante.Enabled = true;
                    txt_cuenta_beneficiario.Enabled = true;
                    txt_rfc_beneficiario.Enabled = true;

                    txt_banco.Enabled = false;
                }
                if (clave == 06) // Dígitos. Dinero electrónico
                {
                    txt_cuenta.MaxLength = 10;

                    txt_rfc_ordenante.Enabled = true;
                    txt_banco.Enabled = false;
                    txt_cuenta_beneficiario.Enabled = false;
                    txt_rfc_beneficiario.Enabled = false;
                }
            }
        }

        private void Complemento_pago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void encampo_cuenta(object sender, EventArgs e)
        {
            TextBox campo_avalidar = (TextBox)sender;

            if (campo_avalidar.Text == "(Opcional) No. cuenta ordenante")
            {
                campo_avalidar.Text = "";
            }
            if (campo_avalidar.Text == "(OPCIONAL) RFC")
            {
                campo_avalidar.Text = "";
            }
            if (campo_avalidar.Text == "(Opcional) Nombre banco")
            {
                campo_avalidar.Text = "";
            }
            if (campo_avalidar.Text == "(Opcional) No. cuenta beneficiario")
            {
                campo_avalidar.Text = "";
            }
            if (campo_avalidar.Text == "(OPCIONAL) RFC.")
            {
                campo_avalidar.Text = "";
            }
        }

        private void scampo_cuenta(object sender, EventArgs e)
        {
            TextBox campo_avalidar = (TextBox)sender;
            var nombre_campo = campo_avalidar.Name;

            if (string.IsNullOrWhiteSpace(campo_avalidar.Text))
            {
                if (nombre_campo == "txt_cuenta")
                {
                    campo_avalidar.Text = "(Opcional) No. cuenta ordenante";
                }
                if (nombre_campo == "txt_rfc_ordenante")
                {
                    campo_avalidar.Text = "(OPCIONAL) RFC";
                }
                if (nombre_campo == "txt_banco")
                {
                    campo_avalidar.Text = "(Opcional) Nombre banco";
                }                
                if (nombre_campo == "txt_cuenta_beneficiario")
                {
                    campo_avalidar.Text = "(Opcional) No. cuenta beneficiario";
                }
                if (nombre_campo == "txt_rfc_beneficiario")
                {
                    campo_avalidar.Text = "(OPCIONAL) RFC.";
                }
            }
        }

        private void cta_ordenante(object sender, KeyPressEventArgs e)
        {
            string clave = cmb_bx_forma_pago.SelectedValue.ToString();

                        
            if (clave == "02" | clave == "03" | clave == "04" | clave == "06" | clave == "28" | clave == "29")
            {
                if (Char.IsNumber(e.KeyChar) | Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }

            if (clave == "05")
            {
                if (Char.IsLetterOrDigit(e.KeyChar) | Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void cta_beneficiario(object sender, KeyPressEventArgs e)
        {
            string clave = cmb_bx_forma_pago.SelectedValue.ToString();

            if (clave == "02" | clave == "04" | clave == "05" | clave == "28" | clave == "29") 
            {
                if (Char.IsLetterOrDigit(e.KeyChar) | Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            if (clave == "03")
            {
                if (Char.IsNumber(e.KeyChar) | Char.IsControl(e.KeyChar))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void limpiar_campos_forma_pago()
        {
            txt_cuenta.Text = string.Empty;
            txt_rfc_ordenante.Text = string.Empty;
            txt_banco.Text = string.Empty;
            txt_cuenta_beneficiario.Text = string.Empty;
            txt_rfc_beneficiario.Text = string.Empty;

            txt_cuenta.Text = "(Opcional) No. cuenta ordenante";
            txt_rfc_ordenante.Text = "(OPCIONAL) RFC";
            txt_banco.Text = "(Opcional) Nombre banco";
            txt_cuenta_beneficiario.Text = "(Opcional) No. cuenta beneficiario";
            txt_rfc_beneficiario.Text = "(OPCIONAL) RFC.";
        }

        private string val_campos_forma_pago_moneda()
        {
            string mensaje = "";
            string clave = cmb_bx_forma_pago.SelectedValue.ToString();
            string formato_rfc_or = "^XEXX010101000|[A-Z&amp;Ñ]{3}[0-9]{2}(0[1-9]| 1[012])(0[1-9]|[12][0-9]|3[01])[A-Z0-9]{2}[0-9A]$";
            string formato_rfc_be = "^[A-Z&Ñ]{3}[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[A-Z0-9]{2}[0-9A]$";
            string formato_cnt_or = "";
            string formato_cnt_be = "^[0-9]{10,11}|[0-9]{15,16}|[0-9]{18}|[A-Z0-9_]{10,50}$";

            

            if (clave == "02") // Cheque
            {
                formato_cnt_or = "^[0-9]{16,18}$";
            }
            if (clave == "03") // Transferencia
            {
                formato_cnt_or = "^[0-9]{10}|[0-9]{16}|[0-9]{18}$";
                formato_cnt_be = "^[0-9]{10,18}$";
            }
            if (clave == "04") // Tarjeta de crédito
            {
                formato_cnt_or = "^[0-9]{16}$";
            }
            if (clave == "05") // Monedero electronico
            {
                formato_cnt_or = "^[0-9]{10,11}|[0-9]{15,16}|[0-9]{18}|[A-Z0-9_]{10,50}$";
            }
            if (clave == "06") // Dinero electronico
            {
                formato_cnt_or = "^[0-9]{10}$";
            }
            if (clave == "28") // Tarjeta de debito
            {
                formato_cnt_or = "^[0-9]{16}$";
            }
            if (clave == "29") // Tarjeta de servicio
            {
                formato_cnt_or = "^[0-9]{15,16}$";
            }


            if(clave == "02" | clave == "03" | clave == "04" | clave == "05" | clave == "06" | clave == "28" | clave == "29")
            {
                // Valida cuenta ordenante
                
                if (txt_cuenta.Text != "(Opcional) No. cuenta ordenante")
                {
                    Regex exp_cuenta_or = new Regex(formato_cnt_or);

                    if (exp_cuenta_or.IsMatch(txt_cuenta.Text))
                    {
                    }
                    else
                    {
                        mensaje = "El formato de la cuenta ordenante no es valida.";
                    }
                }                

                // Valida RFC ordenante

                if(txt_rfc_ordenante.Text != "(OPCIONAL) RFC")
                {
                    Regex exp_rfc_or = new Regex(formato_rfc_or);

                    if (exp_rfc_or.IsMatch(txt_rfc_ordenante.Text))
                    {
                    }
                    else
                    {
                        mensaje = "El formato del RFC ordenante no es valido.";
                    }
                }
            }


            if (clave == "02" | clave == "03" | clave == "04" | clave == "05" | clave == "28" | clave == "29")
            {
                // Valida cuenta beneficiario

                if (txt_cuenta_beneficiario.Text != "(Opcional) No. cuenta beneficiario")
                {
                    Regex exp_cuenta_be = new Regex(formato_cnt_be);

                    if (exp_cuenta_be.IsMatch(txt_cuenta_beneficiario.Text))
                    {
                    }
                    else
                    {
                        mensaje = "El formato de la cuenta beneficiario no es valida.";
                    }
                }

                // Valida RFC beneficiario

                if(txt_rfc_beneficiario.Text != "(OPCIONAL) RFC.")
                {
                    Regex exp_rfc_be = new Regex(formato_rfc_be);

                    if (exp_rfc_be.IsMatch(txt_rfc_beneficiario.Text))
                    {
                    }
                    else
                    {
                        mensaje = "El formato del RFC beneficiario no es valido.";
                    }
                }
            }


            if (clave == "02" | clave == "03" | clave == "04" | clave == "28" | clave == "29")
            {
                if(txt_cuenta.Text == "XEXX010101000" & string.IsNullOrEmpty(txt_banco.Text))
                {
                    mensaje = "El nombre del banco es obligatorio cuando el RFC ordenante es 'XEXX010101000'.";
                }
            }


            // Validar moneda y tipo cambio

            if (string.IsNullOrEmpty(txt_moneda_pago.Text))
            {
                mensaje = "El campo moneda pago no debe estar vacío.";
            }
            else
            {
                if (txt_moneda_pago.Text != "MXN" & string.IsNullOrEmpty(txt_tipo_cambio_pago.Text))
                {
                    mensaje = "El campo tipo de cambio del pago no debe estar vacío.";
                }
            }

            return mensaje;

        } 

        private void sel_inc_impuestos(object sender, EventArgs e)
        {
            ComboBox cmb_box = (ComboBox)sender;
            var nom = cmb_box.Name;
            var nfila = nom.Split('-');
            int id_fct = 0;


            Button btn_des_habilita = (Button)this.Controls.Find("btn_agregar_impuestos-" + nfila[1], true).FirstOrDefault();

            
            if (cmb_box.SelectedItem.ToString() == "Sí objeto de impuesto.")
            {
                // Busca si la factura a pagar incluye impuestos. Si los incluye se habilita el botón 

                Label lb_idf = (Label)this.Controls.Find("lb_idf-" + nfila[1], true).FirstOrDefault();
                id_fct = Convert.ToInt32(lb_idf.Text);


                bool exi = (bool)cn.EjecutarSelect($"SELECT ID FROM Facturas_productos WHERE id_factura='{id_fct}' AND incluye_impuestos='02'", 0);

                if (exi == true)
                {
                    btn_des_habilita.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Los impuestos no son aplicables para este documento. La factura no es '02 - Sí objeto de impuesto'.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }                                
            }
            else
            {
                btn_des_habilita.Enabled = false;
            }
        }

        private void abrir_vnt_moneda(object sender, EventArgs e)
        {
            TextBox  campo = (TextBox)sender;
            string dato_tcambio = "";
            string nom_cmp_tcambio = "";

            Elegir_moneda vnt_moneda = new Elegir_moneda();

            vnt_moneda.FormClosed += delegate
            {
                if(ban_moneda == true)
                {
                    campo.Text = clave_moneda;
                    
                    // Define la acción para el campo tipo de cambio

                    if (campo.Name == "txt_moneda_pago")
                    {
                        nom_cmp_tcambio = "txt_tipo_cambio_pago";
                    }
                    else
                    {
                        var nf = campo.Name.Split('r');
                        nom_cmp_tcambio = "txt_tcambio_dr" + nf[1];
                    }

                    TextBox txt_tcambio = (TextBox)this.Controls.Find(nom_cmp_tcambio, true).FirstOrDefault();

                    if (clave_moneda == "MXN")
                    {
                        txt_tcambio.Text = "1";
                    }
                    else
                    {
                        txt_tcambio.Enabled = true;
                        txt_tcambio.Text = string.Empty;
                        txt_tcambio.Focus();
                    }

                }
                
                vnt_moneda.Dispose();
            };

            vnt_moneda.ShowDialog();
        }

        private void abrir_vnt_impuestos(object sender, EventArgs e)
        {
            Button boton = (Button)sender;
            var nombre_boton = boton.Name; 
            var num_dr = nombre_boton.Split('-');
            
            Label lb_id = (Label)this.Controls.Find("lb_idf-" + num_dr[1], true).FirstOrDefault();
            int id_fct = Convert.ToInt32(lb_id.Text);

            TextBox txt_abono = (TextBox)this.Controls.Find("txt_total" + num_dr[1], true).FirstOrDefault();
            var abono = txt_abono.Text;

            if(string.IsNullOrEmpty(txt_abono.Text))
            {
                MessageBox.Show("Primero debe ingresar el abono.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Complemento_pago_impuestos vnt_impuestos = new Complemento_pago_impuestos(Convert.ToInt32(num_dr[1]), id_fct, Convert.ToDecimal(txt_abono.Text));

            vnt_impuestos.FormClosed += delegate
             {
                 vnt_impuestos.fila_im = 1;
             };

            vnt_impuestos.ShowDialog();
        }
    }
}
