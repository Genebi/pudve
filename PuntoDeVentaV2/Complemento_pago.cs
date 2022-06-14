using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class Complemento_pago : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        int id_usuario = FormPrincipal.userID;
        int id_empleado = FormPrincipal.id_empleado;
        int tam_arr = Facturas.arr_id_facturas.Length;
        decimal[][] arr_totales;


        public Complemento_pago()
        {
            InitializeComponent();
        }
               

        private void Complemento_pago_Load(object sender, EventArgs e)
        {
            cmb_bx_forma_pago.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
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

            cmb_bx_forma_pago.DataSource = forma_pago.ToArray();
            cmb_bx_forma_pago.DisplayMember = "Value";
            cmb_bx_forma_pago.ValueMember = "Key";
            cmb_bx_forma_pago.SelectedIndex = 0;

            // Fecha actual

            DateTime f_actual = DateTime.Now;
            string fecha_actual = f_actual.ToShortDateString();
            datetime_fecha_pago.Text = fecha_actual;

            // Facturas a pagar/abonar

            if (tam_arr > 0)
            {
                int location_y = 6;

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
                        lb_c_folio_serie.Name = "lb_folio_serie" + i;
                        lb_c_folio_serie.Location = new Point(15, location_y);
                        lb_c_folio_serie.Text = r_factura["folio"].ToString() + " " + r_factura["serie"].ToString();
                        
                        Label lb_c_total = new Label();
                        lb_c_total.Name = "lb_total" + i;
                        lb_c_total.Location = new Point(112, location_y);
                        lb_c_total.Text = total_f;

                        TextBox txt_c_total = new TextBox();
                        txt_c_total.Name = "txt_total" + i;
                        txt_c_total.Location = new Point(235, location_y);
                        txt_c_total.Size = new Size(115, 22);
                        txt_c_total.TextAlign = HorizontalAlignment.Center;
                        txt_c_total.Text = total_f;
                        txt_c_total.KeyPress += new KeyPressEventHandler(solo_decimales);
                   

                        pnl_info.Controls.Add(lb_c_folio_serie);
                        pnl_info.Controls.Add(lb_c_total);
                        pnl_info.Controls.Add(txt_c_total);
                        

                        location_y = location_y + 24;
                    }
                }
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
                int t = 0, error = 0, error_monto_mayor = 0;
                arr_totales = new decimal[tam_arr][];


                // ...............................
                // .   Validar y guardar datos   .
                // ...............................

                foreach (Control panel in pnl_info.Controls)
                {
                    if (panel.Name.Contains("lb_total"))
                    {
                        arr_totales[t] = new decimal[4];

                        arr_totales[t][1] = Convert.ToDecimal(panel.Text);
                    }
                    if (panel.Name.Contains("txt_total"))
                    {
                        arr_totales[t][2] = Convert.ToDecimal(panel.Text);

                        if (panel.Text.Trim() == "" | Convert.ToDecimal(panel.Text) == 0)
                        {
                            error++;
                        }

                        // Valida que el abono no sea mayor a lo que debe

                        decimal resta = arr_totales[t][1];
                        decimal abono = arr_totales[t][2];

                        if (abono > 0)
                        {
                            if (resta >= abono)
                            {
                                arr_totales[t][0] = Facturas.arr_id_facturas[t];
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
                // Fecha y hora en que se hizo el pago
                string fecha_hora_pago = datetime_fecha_pago.Value.ToString("yyy-MM-dd") + " " + datetime_hora_pago.Value.ToString("hh:mm:ss");

                // Buscamos los datos del receptor
                DataTable d_receptor = cn.CargarDatos(cs.obtener_datos_para_gcpago(1, id_f_receptor));
                DataRow r_recetor = d_receptor.Rows[0];

                string folio = r_recetor["folio"].ToString();
                string serie = r_recetor["serie"].ToString();
                //string id_venta = r_recetor["id_venta"].ToString();

                // Datos del emisor
                DataTable d_emisor = cn.CargarDatos(cs.cargar_datos_venta_xml(2, 0, id_usuario));
                DataRow r_emisor = d_emisor.Rows[0];

                // Se obtiene la cantidad de complementos generados para continuar con el consecutivo
                int cant_complementos = Convert.ToInt32(cn.EjecutarSelect($"SELECT COUNT(ID) AS ID FROM Facturas_complemento_pago WHERE id_factura_principal='{id_f_receptor}'", 1));
                cant_complementos = cant_complementos + 1;
                serie = serie + "-" + cant_complementos;

                string[] datos_f = new string[]
                {
                id_usuario.ToString(), id_empleado.ToString(), cmb_bx_forma_pago.SelectedValue.ToString(), folio, serie, fecha_hora_pago,
                r_recetor["r_rfc"].ToString(), r_recetor["r_razon_social"].ToString(), r_recetor["r_nombre_comercial"].ToString(), r_recetor["r_correo"].ToString(), r_recetor["r_telefono"].ToString(), r_recetor["r_pais"].ToString(), r_recetor["r_estado"].ToString(),
                r_recetor["r_municipio"].ToString(), r_recetor["r_localidad"].ToString(), r_recetor["r_cp"].ToString(), r_recetor["r_colonia"].ToString(), r_recetor["r_calle"].ToString(), r_recetor["r_num_ext"].ToString(), r_recetor["r_num_int"].ToString(),
                r_emisor["RFC"].ToString(), r_emisor["RazonSocial"].ToString(), r_emisor["Regimen"].ToString(), r_emisor["Email"].ToString(), r_emisor["Telefono"].ToString(), r_emisor["CodigoPostal"].ToString(),
                r_emisor["Estado"].ToString(), r_emisor["Municipio"].ToString(), r_emisor["Colonia"].ToString(), r_emisor["Calle"].ToString(), r_emisor["NoExterior"].ToString(), r_emisor["NoInterior"].ToString(),
                txt_cuenta.Text
                };

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

                for (int r = 0; r < arr_totales.Length; r++)
                {
                    // Obtiene el folio fiscal de la factura que se esta abonando/pagando
                    string uuid = Convert.ToString(cn.EjecutarSelect($"SELECT uuid FROM Facturas WHERE ID='{arr_totales[r][0]}'", 10));

                    // Obtiene el número de la parcialidad anterior
                    int n_parcialidad = Convert.ToInt32(cn.EjecutarSelect($"SELECT COUNT(ID) AS ID FROM Facturas_complemento_pago WHERE id_factura_principal='{arr_totales[r][0]}'", 1));
                    n_parcialidad = n_parcialidad + 1;

                    decimal saldo_insoluto = 0;
                    saldo_insoluto = arr_totales[r][1] - arr_totales[r][2];

                    monto_pago += arr_totales[r][2];

                    string[] datos_cp = new string[]
                    {
                    id_factura_pago.ToString(), arr_totales[r][0].ToString(), n_parcialidad.ToString(), arr_totales[r][1].ToString(), arr_totales[r][2].ToString(), saldo_insoluto.ToString(), uuid
                    };

                    cn.EjecutarConsulta(cs.crear_complemento_pago(3, datos_cp));


                    // Guarda el id de la factura principal de la que se esta haciendo el pago/abono.
                    // Si la factura se timbra, se indica que tiene complemento de pago
                    arr_idf_principal_pago[x] = new decimal[2];
                    arr_idf_principal_pago[x][0] = Convert.ToDecimal(arr_totales[r][0]);
                    arr_idf_principal_pago[x][1] = Convert.ToDecimal(saldo_insoluto);
                    x++;

                    // Cambia variable a 1 para indicar que la factura principal tienen complementos de pago

                    //string[] datos_v = new string[] { arr_totales[r][0].ToString() };

                    //cn.EjecutarConsulta(cs.crear_complemento_pago(4, datos_v));
                }


                // Agrega el monto pagado a la factura principal  

                string[] datos_fm = new string[]
                {
                id_factura_pago.ToString(), monto_pago.ToString()
                };

                cn.EjecutarConsulta(cs.crear_complemento_pago(6, datos_fm));




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
                txt_cuenta.Text = string.Empty;
                txt_cuenta.Enabled = false;
                txt_cuenta.Cursor = Cursors.No;
            }
            else
            {
                txt_cuenta.Enabled = true;
                txt_cuenta.Cursor = Cursors.IBeam;

                if (clave == 02 | clave == 03) // Dígitos. Cheque y transferencia
                {
                    txt_cuenta.MaxLength = 18;
                }
                if (clave == 04 | clave == 28 | clave == 29) // Dígitos. Tarjeta de crédito, debito y servicios
                {
                    txt_cuenta.MaxLength = 16;
                }
                if (clave == 05) // Alfanumérica. Monedero electrónico
                {
                    txt_cuenta.MaxLength = 50;
                }
                if (clave == 06) // Dígitos. Dinero electrónico
                {
                    txt_cuenta.MaxLength = 10;
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
    }
}
