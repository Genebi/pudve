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
using System.Drawing.Printing;

namespace PuntoDeVentaV2
{
    public partial class Lista_complementos_pago : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        Facturas fct = new Facturas();

        int id_factura_p = 0;
        int id_empleado = 0;

        CheckBox header_checkb = null;


        public Lista_complementos_pago(int id_f, int id_e)
        {
            InitializeComponent();

            id_factura_p = id_f;
            id_empleado = id_e;
        }
        

        private void Lista_complementos_pago_Load(object sender, EventArgs e)
        {
            cargar_lista_complementos();
        }

        private void cargar_lista_complementos()
        {
            datagv_complementospg.Rows.Clear();

            DataTable d_cpago = cn.CargarDatos(cs.obtiene_cpagos_dfactura_princ(id_factura_p, 1));

            if (d_cpago.Rows.Count > 0)
            {
                foreach (DataRow r_cpago in d_cpago.Rows)
                {
                    int id_fcp = Convert.ToInt32(r_cpago["id_factura"]);
                    decimal importe_pagado = Convert.ToDecimal(r_cpago["importe_pagado"]);

                    DateTime fechacp = Convert.ToDateTime(r_cpago["fecha_certificacion"].ToString());
                    string fecha_certcp = fechacp.ToString("yyyy-MM-dd");

                    // Nombre del empleado
                    string idemp = "";

                    if (id_empleado == 0)
                    {
                        string id_empleado_fct = r_cpago["id_empleado"].ToString();

                        string[] rr = new string[] { id_empleado_fct };
                        DataTable d_empl = cn.CargarDatos(cs.guardar_editar_empleado(rr, 3));

                        if (d_empl.Rows.Count > 0)
                        {
                            DataRow r_empl = d_empl.Rows[0];
                            idemp = r_empl["id_empleado"].ToString();

                            var pos = idemp.IndexOf("@");
                            idemp = idemp.Substring(pos + 1, idemp.Length - (pos + 1));
                        }
                    }


                    // Obtiene datos pertenecientes a cada complemento
                    DataTable d_cpago_f = cn.CargarDatos(cs.obtiene_cpagos_dfactura_princ(id_fcp, 2));

                    foreach (DataRow r_cpago_f in d_cpago_f.Rows)
                    {
                        int fila_idc = datagv_complementospg.Rows.Add();

                        DataGridViewRow filac = datagv_complementospg.Rows[fila_idc];

                        filac.Cells["col_id"].Value = r_cpago_f["ID"].ToString();
                        filac.Cells["col_checkbox"].Value = false;
                        filac.Cells["col_folio"].Value = r_cpago_f["folio"].ToString();
                        filac.Cells["col_serie"].Value = r_cpago_f["serie"].ToString();
                        if (id_empleado > 0)
                        {
                            this.datagv_complementospg.Columns["col_empleado"].Visible = false;
                        }
                        else
                        {
                            filac.Cells["col_empleado"].Value = idemp;
                        }
                        filac.Cells["col_rfc"].Value = r_cpago_f["r_rfc"].ToString();
                        filac.Cells["col_razon_social"].Value = r_cpago_f["r_razon_social"].ToString();
                        filac.Cells["col_total"].Value = importe_pagado.ToString();
                        filac.Cells["col_fecha"].Value = fecha_certcp;

                        Image img_pdf = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");
                        Image img_descargar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\download.png");
                        Image img_cancelar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\bell-slash.png");

                        filac.Cells["col_pdf"].Value = img_pdf;
                        filac.Cells["col_descargar"].Value = img_descargar;
                        filac.Cells["col_cancelar"].Value = img_cancelar;
                    }
                }
            }
        }

        private void ag_checkb_header()
        {
            header_checkb = new CheckBox();
            header_checkb.Name = "cbox_seleccionar_todo";
            header_checkb.Size = new Size(15, 15);
            header_checkb.Location = new Point(12, 6);
            header_checkb.CheckedChanged += new EventHandler(des_activa_t_checkbox);
            datagv_complementospg.Controls.Add(header_checkb);
        }

        private void des_activa_t_checkbox(object sender, EventArgs e)
        {
            CheckBox headerBox = ((CheckBox)datagv_complementospg.Controls.Find("cbox_seleccionar_todo", true)[0]);

            foreach (DataGridViewRow row in datagv_complementospg.Rows)
            {
                row.Cells["col_checkbox"].Value = headerBox.Checked;
            }
        }
        
        private void click_en_icono(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id_factura = Convert.ToInt16(datagv_complementospg.Rows[e.RowIndex].Cells["col_id"].Value);
                var servidor = Properties.Settings.Default.Hosting;


                // Ver PDF

                if (e.ColumnIndex == 9)
                {
                    if (!Utilidades.AdobeReaderInstalado())
                    {
                        Utilidades.MensajeAdobeReader();
                        return;
                    }
                    
                    string nombre_xml = "XML_PAGO_" + id_factura;

                    // Verifica si el archivo pdf ya esta creado, de no ser así lo crea
                    string ruta_archivo = @"C:\Archivos PUDVE\Facturas\" + nombre_xml + ".pdf";

                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        ruta_archivo = $@"\\{servidor}\Archivos PUDVE\Facturas\" + nombre_xml + ".pdf";
                    }

                    if (!File.Exists(ruta_archivo))
                    {
                        MessageBox.Show("La generación del PDF tardará 10 segundos (aproximadamente) en ser visualizado. Un momento por favor...", "", MessageBoxButtons.OK);

                        fct.generar_PDF(nombre_xml);
                    }

                    // Ver PDF de factura
                    Visualizar_factura ver_fct = new Visualizar_factura(nombre_xml);

                    ver_fct.FormClosed += delegate
                    {
                        ver_fct.Dispose();
                    };

                    ver_fct.ShowDialog();
                }

                // Descargar factura

                if (e.ColumnIndex == 10)
                {
                    string tipo = "PAGO_";
                    string estatus = "";
                    int idf = id_factura;
                    
                    fct.inicia_descargaf(tipo, idf, estatus);
                }

                // Cancelar factura

                if (e.ColumnIndex == 11)
                {
                    var resp = MessageBox.Show("La cancelación tardará 5 segundos (aproximadamente) en ser cancelada, un momento por favor. \n\n ¿Esta seguro que desea cancelar la factura?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                    if (resp == DialogResult.Yes)
                    {
                        Cancelar_XML cancela = new Cancelar_XML();
                        string[] respuesta = cancela.cancelar(id_factura, "P");

                        if (respuesta[1] == "201")
                        {
                            MessageBox.Show(respuesta[0], "Mensaje del sistema", MessageBoxButtons.OK);

                            // Cambiar a cancelada
                            cn.EjecutarConsulta($"UPDATE Facturas SET cancelada=1 WHERE ID='{id_factura}'");
                            cn.EjecutarConsulta($"UPDATE Facturas_complemento_pago SET cancelada=1 WHERE id_factura='{id_factura}'");

                            // Obtener el id de la factura principal a la que se le hace el abono
                            DataTable d_datos_cp = cn.CargarDatos(cs.obtener_datos_para_gcpago(4, id_factura));
                            DataRow r_datos_cp = d_datos_cp.Rows[0];

                            int id_factura_princ = Convert.ToInt32(r_datos_cp["id_factura_principal"].ToString());
                            decimal importe_pg = Convert.ToDecimal(r_datos_cp["importe_pagado"].ToString());

                            // Verificar el comprobante para ver si no era el único que estaba por cancelar
                            DataTable d_exi_complement = cn.CargarDatos(cs.obtener_datos_para_gcpago(5, id_factura_princ));
                            int cant_exi_complement = d_exi_complement.Rows.Count;


                            // Ver si el campo resta_pago se modifica una vez se timbra la factura principal
                            if (cant_exi_complement == 0)
                            {
                                cn.EjecutarConsulta($"UPDATE Facturas SET con_complementos=0, resta_cpago=0 WHERE ID='{id_factura_princ}'");
                            }
                            else
                            {
                                cn.EjecutarConsulta($"UPDATE Facturas SET resta_cpago=resta_cpago+'{importe_pg}' WHERE ID='{id_factura_princ}'");
                            }


                            // Cargar consulta
                            cargar_lista_complementos();
                        }
                        else
                        {
                            MessageBox.Show(respuesta[0], "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                datagv_complementospg.ClearSelection();
            }
        }

        private void cursor_en_icono(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 & e.ColumnIndex >= 9)
            {
                datagv_complementospg.Cursor = Cursors.Hand;

                Rectangle cellRect = datagv_complementospg.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);


                var txt_toolt = string.Empty;
                int coordenadaX = 0;
                var permitir = true;


                if (e.ColumnIndex == 9)
                {
                    coordenadaX = 70;
                    txt_toolt = "Ver factura";
                }
                if (e.ColumnIndex == 10)
                {
                    coordenadaX = 105;
                    txt_toolt = "Descargar factura";
                }
                if (e.ColumnIndex == 11)
                {
                    coordenadaX = 100;
                    txt_toolt = "Cancelar factura";
                }
                
                VerToolTip(txt_toolt, cellRect.X, coordenadaX, cellRect.Y, permitir);
            }
        }

        private void cursor_no_icono(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 & e.ColumnIndex >= 9)
            {
                datagv_complementospg.Cursor = Cursors.Default;
            }
        }

        private void clickcellc_checkbox(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datagv_complementospg.Columns[0].Index)
            {
                DataGridViewCheckBoxCell celda = (DataGridViewCheckBoxCell)this.datagv_complementospg.Rows[e.RowIndex].Cells[0];

                if (Convert.ToBoolean(celda.Value) == false)
                {
                    celda.Value = true;
                    datagv_complementospg.Rows[e.RowIndex].Selected = true;
                }
                else
                {
                    celda.Value = false;
                    datagv_complementospg.Rows[e.RowIndex].Selected = false;
                }
            }
        }

        private void VerToolTip(string texto, int cellRectX, int coordX, int cellRectY, bool mostrar)
        {
            if (mostrar)
            {
                TTMensaje.Show(texto, this, datagv_complementospg.Location.X + cellRectX - coordX, datagv_complementospg.Location.Y + cellRectY, 1500);
            }
        }

        private void TTMensaje_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            int cont = 0;
            int en = 0;
            string mnsj_error = "";
            string[][] arr_id_env;
            

            foreach (DataGridViewRow row in datagv_complementospg.Rows)
            {
                bool estado = (bool)row.Cells["col_checkbox"].Value;

                if (estado == true)
                {
                    cont++;
                }
                else
                {
                    mnsj_error = "No ha seleccionado algún complemento para enviar.";
                }
            }

            // Obtener el id de la factura a enviar

            if (cont > 0)
            {
                arr_id_env = new string[cont][];

                foreach (DataGridViewRow row in datagv_complementospg.Rows)
                {
                    bool estado = (bool)row.Cells["col_checkbox"].Value;

                    if (estado == true)
                    {
                        arr_id_env[en] = new string[2];

                        arr_id_env[en][0] = Convert.ToString(row.Cells["col_id"].Value);
                        arr_id_env[en][1] = "P";
                        en++;
                    }
                }

                // Formulario envío de correo

                Enviar_correo correo = new Enviar_correo(arr_id_env, "factura", 2);

                correo.FormClosed += delegate
                {

                };
                correo.ShowDialog();
            }
            else
            {
                MessageBox.Show(mnsj_error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
