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
    public partial class Ventana_motivo_cancelacion : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        Cancelar_XML cancela = new Cancelar_XML();

        string[][] arr_idfacturas_canc;
        int tipo;
        int id_empleado = FormPrincipal.id_empleado;


        public Ventana_motivo_cancelacion(int opc, string[][] arr_idfacturas)
        {
            InitializeComponent();

            arr_idfacturas_canc = arr_idfacturas;
            tipo = opc;
        }

        private void Ventana_motivo_cancelacion_Load(object sender, EventArgs e)
        {
            int nfila = 1;
            int location_y = 10;

            Dictionary<string, string> motivos = new Dictionary<string, string>();
            motivos.Add("01", "Comprobante emitido con errores con relación");
            motivos.Add("02", "Comprobante emitido con errores sin relación");
            motivos.Add("03", "No se llevó a cabo la operación");
            motivos.Add("04", "Operación nominativa relacionada en la factura");


            for (int f = 0; f < arr_idfacturas_canc.Length; f++)
            {
                Label lb_folio = new Label();
                lb_folio.Name = "lb_folio" + nfila;
                lb_folio.Location = new Point(10, (location_y + 3));
                lb_folio.Size = new Size(40, 25);

                ComboBox cbox_motivo = new ComboBox();
                cbox_motivo.Name = "cbx_motivo" + nfila;
                cbox_motivo.Location = new Point(73, location_y);
                cbox_motivo.Size = new Size(354, 25);
                cbox_motivo.DropDownStyle = ComboBoxStyle.DropDownList;

                TextBox txt_uuid_sust = new TextBox();
                txt_uuid_sust.Name = "txt_uuid_sust" + nfila;
                txt_uuid_sust.Location = new Point(435, location_y);
                txt_uuid_sust.Size = new Size(279, 23);
                txt_uuid_sust.MaxLength = 36;
                txt_uuid_sust.TextAlign = HorizontalAlignment.Center;
                txt_uuid_sust.Text = "UUID requerida solo si el motivo es '01'";
                txt_uuid_sust.Leave += new System.EventHandler(validauuid);
                txt_uuid_sust.GotFocus += new EventHandler(encampo_uuid);
                txt_uuid_sust.LostFocus += new EventHandler(scampo_uuid);


                lb_folio.Text = arr_idfacturas_canc[f][2].ToString();

                cbox_motivo.DataSource = motivos.ToArray();
                cbox_motivo.DisplayMember = "value";
                cbox_motivo.ValueMember = "key";


                pnl_campos.Controls.Add(lb_folio);
                pnl_campos.Controls.Add(cbox_motivo);
                pnl_campos.Controls.Add(txt_uuid_sust);

                location_y = location_y + 33;

                nfila++;
            }

        }

        private void validauuid(object sender, EventArgs e)
        {
            TextBox obtiene_uuid = (TextBox)sender;
            var uuid_sust = obtiene_uuid.Text;

            int r = formato_uuid(uuid_sust);

            if (r == 1)
            {
                MessageBox.Show("La UUID sustitución debe ser registrada siempre que el motivo sea '01', y debe cumplir con el formato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            string[][] arr_id_cancela = new string[arr_idfacturas_canc.Length][];
            int x = 0;
            int error = 0;

            // Validamos campos

            foreach(Control panel in pnl_campos.Controls)
            {
                if (panel.Name.Contains("lb_folio"))
                {
                    arr_id_cancela[x] = new string[7];

                    arr_id_cancela[x][0] = arr_idfacturas_canc[x][0]; // id factura
                    arr_id_cancela[x][1] = arr_idfacturas_canc[x][1]; // tipo comprobante
                    arr_id_cancela[x][2] = panel.Text; // folio   
                } 
                
                if (panel is ComboBox)
                {
                    var clave_motivo = "";
                                       
                    if (panel.Text == "Comprobante emitido con errores con relación")
                    {
                        clave_motivo = "01";
                    }
                    if (panel.Text == "Comprobante emitido con errores sin relación")
                    {
                        clave_motivo = "02";
                    }
                    if (panel.Text == "No se llevó a cabo la operación")
                    {
                        clave_motivo = "03";
                    }
                    if (panel.Text == "Operación nominativa relacionada en la factura")
                    {
                        clave_motivo = "04";
                    }

                    arr_id_cancela[x][3] = clave_motivo; //panel.Text; // motivo                      
                }

                if (panel.Name.Contains("txt_uuid_sust"))
                {
                    if (arr_id_cancela[x][3] == "01")
                    {
                        if (panel.Text == "")
                        {
                            error++; 
                        }
                        else
                        {
                            int r = formato_uuid(panel.Text);

                            if(r == 1)
                            {
                                error++;
                            }
                            else
                            {
                                arr_id_cancela[x][4] = panel.Text; // uuid sustitución
                            }
                        }
                    }

                    x++;
                }
            }

            if(error > 0)
            {
                MessageBox.Show("El/los folio(s) sustitución debe(n) ser registradao(s) siempre que el motivo sea 'Comrobante emitivo con errores con relación', y debe cumplir con el formato.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int v= arr_id_cancela.Length;





            // CANCELACIÓN INDIVIDUAL Y MULTIPLE
            // .................................
            

            btn_aceptar.Enabled = false;
            btn_cancelar.Enabled = false;

            for (var z = 0; z < arr_id_cancela.Length; z++)
            {                
                string[] respuesta = cancela.cancelar(Convert.ToInt32(arr_id_cancela[z][0]), arr_id_cancela[z][1], arr_id_cancela[z][3], arr_id_cancela[z][4]);

                if (respuesta[1] == "201" | respuesta[1] == "202")
                {
                    // FACTURAS

                    if (tipo == 1)
                    {
                        // Cambiar a canceladas
                        cn.EjecutarConsulta($"UPDATE Facturas SET cancelada=1, id_emp_cancela='{id_empleado}', motivo_canc='{arr_id_cancela[z][3]}', uuid_sust='{arr_id_cancela[z][4]}' WHERE ID='{arr_id_cancela[z][0]}'");
                    }


                    // COMPLEMENTO DE PAGO

                    if (tipo == 2)
                    {
                        int id_factura_princ = 0;
                        decimal importe_pg = 0;

                        // Cambiar a cancelada
                        cn.EjecutarConsulta($"UPDATE Facturas SET cancelada=1, id_emp_cancela='{id_empleado}', motivo_canc='{arr_id_cancela[z][3]}', uuid_sust='{arr_id_cancela[z][4]}' WHERE ID='{arr_id_cancela[z][0]}'");
                        cn.EjecutarConsulta($"UPDATE Facturas_complemento_pago SET cancelada=1 WHERE id_factura='{arr_id_cancela[z][0]}'");

                        // Obtener el id de la factura principal a la que se le hace el abono
                        DataTable d_datos_cp = cn.CargarDatos(cs.obtener_datos_para_gcpago(4, Convert.ToInt32(arr_id_cancela[z][0])));
                        //DataRow r_datos_cp = d_datos_cp.Rows[0];

                        if(d_datos_cp.Rows.Count > 0)
                        {
                            foreach(DataRow r_datos_cp in d_datos_cp.Rows)
                            {
                                if(r_datos_cp["id_factura_principal"].ToString() != "")
                                {
                                    id_factura_princ = Convert.ToInt32(r_datos_cp["id_factura_principal"].ToString());
                                    importe_pg = Convert.ToDecimal(r_datos_cp["importe_pagado"].ToString());


                                    // Verificar el comprobante para ver si no era el único que estaba por cancelar
                                    DataTable d_exi_complement = cn.CargarDatos(cs.obtener_datos_para_gcpago(5, id_factura_princ));
                                    int cant_exi_complement = d_exi_complement.Rows.Count;


                                    // Ver si el campo resta_pago se modifica una vez se timbra la factura principal
                                    if (cant_exi_complement == 0)
                                    {
                                        // Obtiene el total de la factura principal 
                                        DataTable d_imp_fct_p = cn.CargarDatos(cs.obtener_datos_para_gcpago(1, id_factura_princ));
                                        DataRow r_imp_fct_p = d_imp_fct_p.Rows[0];

                                        decimal importe_fct_principal = Convert.ToDecimal(r_imp_fct_p["total"].ToString());

                                        cn.EjecutarConsulta($"UPDATE Facturas SET con_complementos=0, resta_cpago='{importe_fct_principal}' WHERE ID='{id_factura_princ}'");
                                    }
                                    else
                                    {
                                        cn.EjecutarConsulta($"UPDATE Facturas SET resta_cpago=resta_cpago+'{importe_pg}' WHERE ID='{id_factura_princ}'");
                                    }
                                }
                            }
                        }
                        
                    }
                }

                arr_id_cancela[z][5] = respuesta[0];
                arr_id_cancela[z][6] = respuesta[1];
            }

           

            // Muestra resultados 

            Cancelar_XML_mensajes canc_mensaje = new Cancelar_XML_mensajes(arr_id_cancela);

            canc_mensaje.FormClosed += delegate
            {
                this.Dispose();
            };

            btn_aceptar.Enabled = true;
            btn_cancelar.Enabled = true;

            canc_mensaje.ShowDialog();
            
        }

        private int formato_uuid(string dato)
        {
            string formato_rfc = "^[a-f0-9A-F]{8}-[a-f0-9A-F]{4}-[a-f0-9A-F]{4}-[a-f0-9A-F]{4}-[a-f0-9A-F]{12}$";
            Regex regular_ex = new Regex(formato_rfc);
            
            if (regular_ex.IsMatch(dato))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        private void encampo_uuid(object sender, EventArgs e)
        {
            TextBox obtiene_uuid = (TextBox)sender;
            var txt_uuid = obtiene_uuid.Text;

            if (obtiene_uuid.Text == "UUID requerida solo si el motivo es '01'")
            {
                obtiene_uuid.Text = "";
            }
        }

        private void scampo_uuid(object sender, EventArgs e)
        {
            TextBox obtiene_uuid = (TextBox)sender;
            var txt_uuid = obtiene_uuid.Text;

            if (string.IsNullOrWhiteSpace(txt_uuid))
            {
                obtiene_uuid.Text = "UUID requerida solo si el motivo es '01'";
            }
        }
    }
}
