using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using TuesPechkin;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Drawing.Printing;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Xml;

namespace PuntoDeVentaV2
{
    public partial class Facturas : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();
        //Cancelar_XML cancela = new Cancelar_XML(); 

        int id_usuario = FormPrincipal.userID;
        int id_empleado = FormPrincipal.id_empleado;
        public static int[] arr_id_facturas;
        public static bool volver_a_recargar_datos = false;
        bool ban = false;

        CheckBox header_checkb = null;

        private Paginar p;
        string DataMemberDGV = "Facturas";
        int maximo_x_pagina = 16;
        string FiltroAvanzado = string.Empty;
        int clickBoton = 0;

        // Permisos botones
        int opcion1 = 1; // Ver factura
        int opcion2 = 1; // Descargar factura
        int opcion3 = 1; // Cancelar factura
        int opcion4 = 1; // Ver pagos
        int opcion5 = 1; // Buscar facturas
        int opcion6 = 1; // Enviar facturas
        int opcion7 = 1; // Generar complemento

        List<int> listaCheckBox = new List<int>();
        Dictionary<int, string> facturasD = new Dictionary<int, string>();

        public Facturas()
        {
            InitializeComponent();

            MostrarCheckBox();
        }

        private void Facturas_Load(object sender, EventArgs e)
        {
            cmb_bx_tipo_factura.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            DateTime date = DateTime.Now;
            DateTime PrimerDia;
            if (!date.Month.Equals(1))
            {
                PrimerDia = new DateTime(date.Year, date.Month - 1, 1);
            }
            else
            {
                PrimerDia = new DateTime(date.Year - 1, date.Month + 11, 1);
            }
            datetp_fecha_inicial.Value = PrimerDia;
            datetp_fecha_final.Value = DateTime.Now;
            /*Dictionary<int, string> t_factura = new Dictionary<int, string>();
            t_factura.Add(0, "Facturas por pagar");
            t_factura.Add(1, "Facturas abonadas");
            t_factura.Add(2, "Facturas pagadas");
            t_factura.Add(3, "Facturas canceladas");

            cmb_bx_tipo_factura.DataSource = t_factura.ToArray();
            cmb_bx_tipo_factura.DisplayMember = "Value";
            cmb_bx_tipo_factura.ValueMember = "Key";*/
            cmb_bx_tipo_factura.SelectedIndex = 2;

            // Placeholder del campo buscar por...
            txt_buscar_por.GotFocus += new EventHandler(buscar_por_confoco);
            txt_buscar_por.LostFocus += new EventHandler(buscar_por_sinfoco);

            // Crea un checkbox en la cabecera de la tabla. Será para seleccionar todo.
            ag_checkb_header();

            // Obtenemos la cantidad de timbres
            actualizar_timbres();

            // Carga las facturas en la tabla 
            cargar_lista_facturas(0, 2);

            //datagv_facturas.ClearSelection();
            clickBoton = 0;
            actualizar();
            //+++btn_ultima_pag.PerformClick();
            btn_primera_pag.PerformClick();


            if (FormPrincipal.id_empleado > 0)
            {
                var permisos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Facturas");

                opcion1 = permisos[0];
                opcion2 = permisos[1];
                opcion3 = permisos[2];
                opcion4 = permisos[3];
                opcion5 = permisos[4];
                opcion6 = permisos[5];
                opcion7 = permisos[6];
            }
            this.Focus();
        }

        public void cargar_lista_facturas(int tipo = 0, int opc_tipo_f = 0)
        {
            int opc_tipo_factura = Convert.ToInt32(cmb_bx_tipo_factura.SelectedIndex);
            var fecha_inicial = datetp_fecha_inicial.Value.ToString("yyyy-MM-dd");
            var fecha_final = datetp_fecha_final.Value.ToString("yyyy-MM-dd");
            var buscar_por = txt_buscar_por.Text.Trim();

            if (opc_tipo_f == 2)
            {
                opc_tipo_factura = opc_tipo_f;
            }

            if (clickBoton == 0)
            {
                string cons = "";
                string condicional_fecha_i_f = "";
                string condicional_xempleado = "";
                string condicional_buscarpor = "";

                // Comprueba si la sesión esta activa por un empleado o no

                if (id_empleado != 0)
                {
                    condicional_xempleado = " AND id_empleado='" + id_empleado + "'";
                }

                // Se agregan fechas de busqueda cuando es desde el botón buscar

                if (tipo == 1)
                {
                    condicional_fecha_i_f = "AND DATE(fecha_certificacion) BETWEEN '" + fecha_inicial + "' AND '" + fecha_final + "'";
                }

                // Se busca por folio, razón social o RFC cuando es desde el botón de buscar

                if (tipo == 1)
                {
                    condicional_buscarpor = "AND (folio LIKE '%" + buscar_por + "%' OR r_rfc LIKE '%" + buscar_por + "%' OR r_razon_social LIKE '%" + buscar_por + "%')";
                }

                // Por pagar
                if (opc_tipo_factura == 0)
                {
                    cons = $"SELECT * FROM Facturas WHERE id_usuario='{id_usuario}' " + condicional_xempleado + " AND tipo_comprobante='I' AND timbrada=1 AND cancelada=0 AND (metodo_pago='PPD' OR forma_pago='99') AND con_complementos=0 " + condicional_fecha_i_f + condicional_buscarpor + " ORDER BY ID DESC";
                }
                // Abonadas
                if (opc_tipo_factura == 1)
                {
                    cons = $"SELECT * FROM Facturas WHERE id_usuario='{id_usuario}' " + condicional_xempleado + " AND tipo_comprobante='I' AND timbrada=1 AND cancelada=0 AND con_complementos=1 AND resta_cpago>0 " + condicional_fecha_i_f + condicional_buscarpor + " ORDER BY ID DESC";
                }
                // Pagadas
                if (opc_tipo_factura == 2)
                {
                    cons = $"SELECT * FROM Facturas WHERE id_usuario='{id_usuario}' " + condicional_xempleado + " AND tipo_comprobante='I' AND timbrada=1 AND cancelada=0 AND ( (metodo_pago='PUE' AND forma_pago!='99') OR (resta_cpago=0 AND (metodo_pago='PPD' OR forma_pago='99')) ) " + condicional_fecha_i_f + condicional_buscarpor + " ORDER BY ID DESC";
                }
                // Canceladas
                if (opc_tipo_factura == 3)
                {
                    cons = $"SELECT * FROM Facturas WHERE id_usuario='{id_usuario}' " + condicional_xempleado + " AND timbrada=1 AND cancelada=1 " + condicional_fecha_i_f + condicional_buscarpor + " ORDER BY ID DESC";
                }

                FiltroAvanzado = cons;

                p = new Paginar(FiltroAvanzado, DataMemberDGV, maximo_x_pagina);
            }

            datagv_facturas.Rows.Clear();

            DataSet datos = p.cargar();
            DataTable d_facturas = datos.Tables[0];


            if (d_facturas.Rows.Count > 0)
            {
                foreach (DataRow r_facturas in d_facturas.Rows)
                {
                    int fila_id = datagv_facturas.Rows.Add();

                    DateTime fecha = Convert.ToDateTime(r_facturas["fecha_certificacion"]);
                    string fecha_cert = fecha.ToString("yyyy-MM-dd");

                    DataGridViewRow fila = datagv_facturas.Rows[fila_id];

                    // Nombre de empleado
                    /*string user_empleado = "";

                    if (id_empleado == 0)
                    {
                        string id_empleado_fct = r_facturas["id_empleado"].ToString();

                        string[] r = new string[] { id_empleado_fct };
                        DataTable d_emp = cn.CargarDatos(cs.guardar_editar_empleado(r, 3));

                        if (d_emp.Rows.Count > 0)
                        {
                            DataRow r_emp = d_emp.Rows[0];
                            user_empleado = r_emp["usuario"].ToString();

                            var pos = user_empleado.IndexOf("@");
                            user_empleado = user_empleado.Substring(pos + 1, user_empleado.Length - (pos + 1));
                        }
                    }*/


                    fila.Cells["col_id"].Value = r_facturas["ID"].ToString();
                    fila.Cells["col_t_comprobante"].Value = r_facturas["tipo_comprobante"].ToString();
                    fila.Cells["col_checkbox"].Value = false;
                    fila.Cells["col_folio"].Value = r_facturas["folio"].ToString();
                    fila.Cells["col_serie"].Value = r_facturas["serie"].ToString();
                    /*if (id_empleado > 0)
                    {
                        this.datagv_facturas.Columns["col_empleado"].Visible = false;
                    }
                    else
                    {
                        fila.Cells["col_empleado"].Value = user_empleado;
                    }*/
                    fila.Cells["col_rfc"].Value = r_facturas["r_rfc"].ToString();
                    fila.Cells["col_razon_social"].Value = r_facturas["r_razon_social"].ToString();
                    fila.Cells["col_total"].Value = r_facturas["total"].ToString();
                    fila.Cells["col_fecha"].Value = fecha_cert;
                    fila.Cells["col_conpago"].Value = "0";

                    Image img_cpago = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black\blanco.png");

                    if (opc_tipo_factura == 1 | (opc_tipo_factura == 2 & (r_facturas["metodo_pago"].ToString() == "PPD" | r_facturas["forma_pago"].ToString() == "99")))
                    {
                        img_cpago = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\eye.png");


                        if (opc_tipo_factura == 2 & (r_facturas["metodo_pago"].ToString() == "PPD" | r_facturas["forma_pago"].ToString() == "99"))
                        {
                            fila.Cells["col_conpago"].Value = "1";
                        }
                    }

                    Image img_pdf = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");
                    Image img_descargar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\download.png");
                    Image img_cancelar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\bell-slash.png");
                    Image img_info_emp = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\info-circle.png");

                    if (opc_tipo_factura == 3)
                    {
                        this.datagv_facturas.Columns["col_cpago"].Visible = false;
                    }
                    else
                    {
                        this.datagv_facturas.Columns["col_cpago"].Visible = true;
                        fila.Cells["col_cpago"].Value = img_cpago;
                    }

                    fila.Cells["col_pdf"].Value = img_pdf;
                    fila.Cells["col_descargar"].Value = img_descargar;
                    fila.Cells["col_cancelar"].Value = img_cancelar;
                    fila.Cells["col_empleado"].Value = img_info_emp;
                }
            }

            llenarGDV();
        }

        private void ag_checkb_header()
        {
            header_checkb = new CheckBox();
            header_checkb.Name = "cbox_seleccionar_todo";
            header_checkb.Size = new Size(15, 15);
            header_checkb.Location = new Point(12, 6);
            header_checkb.CheckedChanged += new EventHandler(des_activa_t_checkbox);
            datagv_facturas.Controls.Add(header_checkb);
        }

        private void des_activa_t_checkbox(object sender, EventArgs e)
        {
            CheckBox headerBox = ((CheckBox)datagv_facturas.Controls.Find("cbox_seleccionar_todo", true)[0]);

            foreach (DataGridViewRow row in datagv_facturas.Rows)
            {
                row.Cells["col_checkbox"].Value = headerBox.Checked;
            }
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            if (opcion5 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            cargar_lista_facturas(1);
            btn_primera_pag.PerformClick();
        }

        private void buscar_tipo_factura(object sender, EventArgs e)
        {
            cargar_lista_facturas();
        }

        private void buscar_por(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (opcion5 == 1)
                {
                    btn_buscar.PerformClick();
                }
            }
        }

        private void click_en_icono(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id_factura = Convert.ToInt16(datagv_facturas.Rows[e.RowIndex].Cells["col_id"].Value);
                int opc_tipo_factura = Convert.ToInt32(cmb_bx_tipo_factura.SelectedIndex);
                int con_cpago = Convert.ToInt16(datagv_facturas.Rows[e.RowIndex].Cells["col_conpago"].Value);
                string t_comprobante = Convert.ToString(datagv_facturas.Rows[e.RowIndex].Cells["col_t_comprobante"].Value);
                string folio_f = Convert.ToString(datagv_facturas.Rows[e.RowIndex].Cells["col_folio"].Value);
                var servidor = Properties.Settings.Default.Hosting;

                if (e.ColumnIndex == 0)
                {
                    var estado = Convert.ToBoolean(datagv_facturas.Rows[e.RowIndex].Cells["col_checkbox"].Value);

                    //En esta condicion se ponen 
                    if (estado.Equals(false))//Se pone falso por que al dar click inicialmente esta en false
                    {
                        if (!facturasD.ContainsKey(id_factura))
                        {
                            facturasD.Add(id_factura, string.Empty);
                        }
                    }
                    else if (estado.Equals(true))//Se pone verdadero por que al dar click inicialmente esta en true
                    {
                        facturasD.Remove(id_factura);
                    }
                }

                // Lista complementos de pago
                if (e.ColumnIndex == 8)
                {
                    if (opcion4 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }


                    if (opc_tipo_factura == 1 | (opc_tipo_factura == 2 & con_cpago == 1))
                    {
                        Lista_complementos_pago ver_cpago = new Lista_complementos_pago(id_factura, id_empleado);

                        ver_cpago.FormClosed += delegate
                        {
                            // Cargar consulta
                            int tipo_factura = Convert.ToInt32(cmb_bx_tipo_factura.SelectedIndex);
                            cargar_lista_facturas(tipo_factura);
                        };

                        ver_cpago.ShowDialog();
                    }
                }

                // Ver PDF
                if (e.ColumnIndex == 9)
                {
                    if (opcion1 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }





                    //Old af legacy code lmaooo https://media.tenor.com/TGghI4NZHrwAAAAC/lmaoo.gif
                    //if (!Utilidades.AdobeReaderInstalado())
                    //{
                    //    Utilidades.MensajeAdobeReader();
                    //    return;
                    //}

                    string nombre_xml = "";
                    string ruta_archivo = "";

                    if (t_comprobante == "P")
                    {
                        nombre_xml = "XML_PAGO_" + id_factura;
                    }
                    if (t_comprobante == "I")
                    {
                        nombre_xml = "XML_INGRESOS_" + id_factura;
                    }

                    if (!string.IsNullOrWhiteSpace(servidor))
                    {
                        ruta_archivo = $@"\\{servidor}\Archivos PUDVE\Facturas\" + nombre_xml + ".xml";
                    }
                    else
                    {
                        ruta_archivo = @"C:\Archivos PUDVE\Facturas\" + nombre_xml + ".xml";
                    }

                    if (ReadXmlFile(ruta_archivo) == "null")
                    {
                        MessageBox.Show("No se encontro el archivo XML en su equipo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (ReadXmlFile(ruta_archivo) == "3.3")
                    {
                        verFacturasViejas verfacvieja = new verFacturasViejas(id_factura, ReadXmlFile(ruta_archivo, "NoCertificadoSAT"));
                        verfacvieja.ShowDialog();
                    }
                    else
                    {
                        verFacturasViejas verfacNueva = new verFacturasViejas(id_factura, ReadXmlFile(ruta_archivo, "NoCertificadoSAT"), true);
                        verfacNueva.ShowDialog();
                    }
                    //// Verifica si el archivo pdf ya esta creado, de no ser así lo crea
                    //ruta_archivo = @"C:\Archivos PUDVE\Facturas\" + nombre_xml + ".pdf";

                    //if (!string.IsNullOrWhiteSpace(servidor))
                    //{
                    //    ruta_archivo = $@"\\{servidor}\Archivos PUDVE\Facturas\" + nombre_xml + ".pdf";
                    //}

                    //Thread hilo;

                    //if (!File.Exists(ruta_archivo) || File.Exists(ruta_archivo))
                    //{
                    //    //MessageBox.Show("La generación del PDF tardará 10 segundos (aproximadamente) en ser visualizado. Un momento por favor...", "", MessageBoxButtons.OK);
                    //    //Generar_PDF_factura.generar_PDF(nombre_xml);
                    //    //generar_PDF(nombre_xml, id_factura);

                    //    if (string.IsNullOrWhiteSpace(servidor) && (!File.Exists(ruta_archivo) || File.Exists(ruta_archivo)))
                    //    {
                    //        hilo = new Thread(() => mnsj());
                    //        hilo.Start();

                    //        hilo = new Thread(() => generarPDF(nombre_xml, id_factura));
                    //        hilo.Start();


                    //        hilo.Join();
                    //    }
                    //    else if (!string.IsNullOrWhiteSpace(servidor) && !File.Exists(ruta_archivo))
                    //    {
                    //        MessageBox.Show("El archivo de la nota no esta descargado en el servidor, abrirlo primero en el servidor para verlo en red", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        return;
                    //    }
                    //}

                    //// Ver PDF de factura
                    //Visualizar_factura ver_fct = new Visualizar_factura(nombre_xml);

                    //ver_fct.FormClosed += delegate
                    //{
                    //    ver_fct.Dispose();
                    //};

                    //ver_fct.ShowDialog();
                }

                // Descargar factura
                if (e.ColumnIndex == 10)
                {
                    if (opcion2 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    string tipo = "INGRESOS_";
                    string estatus = "";
                    int idf = id_factura;

                    if (t_comprobante == "P")
                    {
                        tipo = "PAGO_";
                    }
                    if (opc_tipo_factura == 3)
                    {
                        estatus = "ACUSE_";
                    }


                    // Elige carpeta donde guardar el comprimido
                    if (elegir_carpeta_descarga.ShowDialog() == DialogResult.OK)
                    {
                        string carpeta = elegir_carpeta_descarga.SelectedPath;

                        inicia_descargaf(tipo, idf, estatus, carpeta);
                    }
                }

                // Cancelar factura
                if (e.ColumnIndex == 11)
                {
                    if (opcion3 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }


                    // Si se tiene una conexión a internet procede a realizar la cancelación.
                    if (Conexion.ConectadoInternet())
                    {

                        if (opc_tipo_factura == 3)
                        {
                            MessageBox.Show("La factura ya fue cancelada con anterioridad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                        {
                            // Obtiene cantidad de timbres disponibles
                            int tiene_timbres = mb.obtener_cantidad_timbres();

                            if (tiene_timbres > 0)
                            {
                                // Comprueba que la factura no tenga complementos de pago

                                var uuidf = cn.EjecutarSelect($"SELECT UUID FROM Facturas WHERE ID='{id_factura}'", 13);

                                var existe_complemento = cn.EjecutarSelect($"SELECT * FROM Facturas_complemento_pago WHERE uuid='{uuidf}' AND timbrada=1 AND cancelada=0");


                                if (Convert.ToBoolean(existe_complemento) == true)
                                {
                                    MessageBox.Show("La factura no puede ser cancelada porque tiene complementos de pago timbrados. \n Primero debe cancelar los complementos de pago pertenecientes a esta factura y posterior cancelarla", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                {
                                    var resp = MessageBox.Show("La cancelación tardará 5 segundos (aproximadamente) en ser cancelada, un momento por favor. \n\n ¿Esta seguro que desea cancelar la factura?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                                    if (resp == DialogResult.Yes)
                                    {
                                        // Abre ventana para definir el motivo de cancelación 

                                        string[][] arr_id_cmult = new string[1][];
                                        arr_id_cmult[0] = new string[4];

                                        arr_id_cmult[0][0] = id_factura.ToString();
                                        arr_id_cmult[0][1] = t_comprobante;
                                        arr_id_cmult[0][2] = folio_f;

                                        Ventana_motivo_cancelacion vnt_motivo_canc = new Ventana_motivo_cancelacion(1, arr_id_cmult);

                                        vnt_motivo_canc.FormClosed += delegate
                                        {
                                            // Cargar consulta
                                            int tipo_factura = Convert.ToInt32(cmb_bx_tipo_factura.SelectedIndex);
                                            cargar_lista_facturas(tipo_factura);
                                        };

                                        vnt_motivo_canc.ShowDialog();

                                        /*string[] respuesta = cancela.cance
                                         * lar(id_factura, t_comprobante);

                                        if (respuesta[1] == "201")
                                        {
                                            MessageBox.Show(respuesta[0], "Mensaje del sistema", MessageBoxButtons.OK);

                                            // Cambiar a canceladas
                                            cn.EjecutarConsulta($"UPDATE Facturas SET cancelada=1, id_emp_cancela='{id_empleado}' WHERE ID='{id_factura}'");

                                            // Cargar consulta
                                            int tipo_factura = Convert.ToInt32(cmb_bx_tipo_factura.SelectedIndex);
                                            cargar_lista_facturas(tipo_factura);
                                        }
                                        else
                                        {
                                            MessageBox.Show(respuesta[0], "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }*/
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("No cuenta con timbres para cancelar el documento. Le sugerimos realizar una compra de timbres.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }

                            //cancela.Sellar(ruta_key, clave);
                        }

                        // Obtenemos la cantidad de timbres
                        actualizar_timbres();
                    }
                    else
                    {
                        MessageBox.Show("Sin conexión a internet. Esta accción requiere una conexión.", "", MessageBoxButtons.OK);
                    }
                }

                // Información empleado
                if (e.ColumnIndex == 12)
                {
                    Empleado_muestra_acciones_factura vnt = new Empleado_muestra_acciones_factura(id_factura, opc_tipo_factura);
                    vnt.ShowDialog();
                }


                datagv_facturas.ClearSelection();
            }
        }

        public void mnsj()
        {
            MessageBox.Show("La generación del PDF tardará 10 segundos (aproximadamente) en ser visualizado. Un momento por favor...", "", MessageBoxButtons.OK);
        }

        private void generarPDF(string nombre_xml, int id_f)
        {
            generar_PDF(nombre_xml, id_f);
        }

        //Claramente yo hise este codigo, desde luego no me lo pepene de internet 8)
        public static string ReadXmlFile(string filePath, string attribute = "Version")
        {
            if (attribute == "Version")
            {
                // Create a new XmlDocument object.
                XmlDocument xmlDocument = new XmlDocument();


                try
                {
                    // Load the XML file from the specified file path.
                    xmlDocument.Load(filePath);
                }
                catch (Exception)
                {

                    return "null";
                }
                // Get the root element of the XML document.
                XmlElement rootElement = xmlDocument.DocumentElement;

                // Get the attributes of the root element.
                string xdddd = rootElement.GetAttribute("Version");
                return xdddd;
            }
            else
            {

                // Load the XML file
                XmlDocument doc = new XmlDocument();
                try
                {
                    // Load the XML file from the specified file path.
                    doc.Load(filePath);
                }
                catch (Exception)
                {

                    return "null";
                }

                // Get the root element
                XmlElement root = doc.DocumentElement;

                // Get the value of the NoCertificadoSAT attribute
                string noCertificadoSAT = root.GetAttribute("NoCertificado");
                return noCertificadoSAT;
            }


        }

        private void cursor_en_icono(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 & e.ColumnIndex >= 8)
            {
                datagv_facturas.Cursor = Cursors.Hand;

                Rectangle cellRect = datagv_facturas.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);


                var txt_toolt = string.Empty;
                int coordenadaX = 0;
                var permitir = true;

                if (e.ColumnIndex == 8)
                {
                    coordenadaX = 140;
                    txt_toolt = "Ver complementos de pago";

                    int opc_tipo_factura = Convert.ToInt32(cmb_bx_tipo_factura.SelectedIndex);

                    if (opc_tipo_factura == 0 | opc_tipo_factura == 3)
                    {
                        permitir = false;
                    }
                }
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
                if (e.ColumnIndex == 12)
                {
                    coordenadaX = 80;
                    txt_toolt = "Información";
                }

                VerToolTip(txt_toolt, cellRect.X, coordenadaX, cellRect.Y, permitir);
            }
        }

        private void cursor_no_icono(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 & e.ColumnIndex >= 8)
            {
                datagv_facturas.Cursor = Cursors.Default;
            }
        }

        private void btn_cpago_Click(object sender, EventArgs e)
        {
            if (opcion7 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            int cont = 0;
            int cont_pg = 0;
            string mnsj_error = "";
            int opc_tipo_factura = Convert.ToInt32(cmb_bx_tipo_factura.SelectedIndex);

            if (opc_tipo_factura == 2 | opc_tipo_factura == 3)
            {
                MessageBox.Show("La generación de complementos de pago no es aplicable a facturas pagadas y/o canceladas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                int tiene_timbres = mb.obtener_cantidad_timbres();

                if (tiene_timbres <= 0)
                {
                    MessageBox.Show("No cuenta con timbres para timbrar el documento. Le sugerimos realizar una compra de timbres.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                if (FormPrincipal.userNickName == "MIRI3" | FormPrincipal.userNickName == "SOLRAC")
                {
                    foreach (DataGridViewRow row in datagv_facturas.Rows)
                    {
                        bool estado = (bool)row.Cells["col_checkbox"].Value;
                        string tipo_comprobante = row.Cells["col_t_comprobante"].Value.ToString();

                        if (estado == true)
                        {
                            cont++;

                            if (tipo_comprobante == "P")
                            {
                                cont_pg++;
                                mnsj_error = "No debe haber facturas de 'Pago' seleccionadas. Los complementos de pago solo se generan a facturas de 'Ingresos'.";
                            }
                        }
                        else
                        {
                            mnsj_error = "No ha seleccionado alguna factura para complemento de pago.";
                        }
                    }


                    if (cont > 0 & cont_pg == 0)
                    {
                        arr_id_facturas = new int[cont];
                        int p = 0;

                        foreach (DataGridViewRow row in datagv_facturas.Rows)
                        {
                            bool estado = (bool)row.Cells["col_checkbox"].Value;

                            if (estado == true)
                            {
                                arr_id_facturas[p] = Convert.ToInt32(row.Cells["col_id"].Value);

                                p++;
                            }
                        }

                        int tipo_factura = Convert.ToInt32(cmb_bx_tipo_factura.SelectedIndex);
                        Complemento_pago c_pago = new Complemento_pago();

                        c_pago.FormClosed += delegate
                        {
                            cargar_lista_facturas(tipo_factura);

                            Complemento_pago_impuestos.dats_en_arr = false;
                        };

                        c_pago.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show(mnsj_error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Acción no disponible.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            // Obtenemos la cantidad de timbres
            actualizar_timbres();
        }

        private void clickcellc_checkbox(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == datagv_facturas.Columns[0].Index)
            {
                DataGridViewCheckBoxCell celda = (DataGridViewCheckBoxCell)this.datagv_facturas.Rows[e.RowIndex].Cells[0];

                if (Convert.ToBoolean(celda.Value) == false)
                {
                    celda.Value = true;
                    datagv_facturas.Rows[e.RowIndex].Selected = true;
                }
                else
                {
                    celda.Value = false;
                    datagv_facturas.Rows[e.RowIndex].Selected = false;
                }
            }
        }

        public void generar_PDF(string nombre_xml, int id_fct)
        {
            var servidor = Properties.Settings.Default.Hosting;

            // ........................................
            // .    Deserealiza el XML ya timbrado    .
            // ........................................

            /*
            Comprobante comprobante;
            string ruta_xml = @"C:\Archivos PUDVE\Facturas\" + nombre_xml + ".xml";

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                ruta_xml = $@"\\{servidor}\Archivos PUDVE\Facturas\" + nombre_xml + ".xml";
            }


            if (!File.Exists(ruta_xml))
            {
                MessageBox.Show("No se encuentró el documento que intenta abrir.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Comprobante));

                // Desserealizar el xml
                using (StreamReader sr = new StreamReader(ruta_xml))
                {
                    comprobante = (Comprobante)serializer.Deserialize(sr);

                    // Dessearializar complementos
                    foreach (var complementos in comprobante.Complemento)
                    {
                        foreach (var complemento in complementos.Any)
                        {
                            if (complemento.Name.Contains("TimbreFiscalDigital"))
                            {
                                XmlSerializer serializer_complemento = new XmlSerializer(typeof(TimbreFiscalDigital));

                                using (var sr_c = new StringReader(complemento.OuterXml))
                                {
                                    comprobante.timbre_fiscal_digital = (TimbreFiscalDigital)serializer_complemento.Deserialize(sr_c);
                                }
                            }

                            if (complemento.Name.Contains("Pagos"))
                            {
                                XmlSerializer serializer_complemento_pagos = new XmlSerializer(typeof(Pagos));

                                using (var sr_cp = new StringReader(complemento.OuterXml))
                                {
                                    comprobante.cpagos = (Pagos)serializer_complemento_pagos.Deserialize(sr_cp);


                                    foreach (var cpagos_pg in comprobante.cpagos.Pago)
                                    {
                                        comprobante.cpagos.cpagos_pago = cpagos_pg;

                                        foreach (var cpagos_pg_docrel in comprobante.cpagos.cpagos_pago.DoctoRelacionado)
                                        {
                                            comprobante.cpagos.cpagos_pago_docrelacionado = cpagos_pg_docrel;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                // Comprueba si el comprobante esta cancelado.
                // Obtiene el domicilio del receptor.
                DataTable d_factura = cn.CargarDatos(cs.cargar_datos_venta_xml(1, id_fct, id_usuario));
                DataRow r_factura = d_factura.Rows[0];

                if (Convert.ToInt32(r_factura["cancelada"]) == 1)
                {
                    comprobante.CanceladaSpecified = true;
                    comprobante.Cancelada = 1;
                }

                string domicilio_receptor = "";
                string correo_tel_receptor = "";

                if (r_factura["r_calle"].ToString() != "")
                {
                    domicilio_receptor = r_factura["r_calle"].ToString();
                }
                if (r_factura["r_num_ext"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", "; }

                    domicilio_receptor += r_factura["r_num_ext"].ToString();
                }
                if (r_factura["r_num_int"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", int. "; }

                    domicilio_receptor += r_factura["r_num_int"].ToString();
                }
                if (r_factura["r_colonia"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", Col. "; }

                    domicilio_receptor += r_factura["r_colonia"].ToString();
                }
                if (r_factura["r_cp"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", CP. "; }

                    domicilio_receptor += r_factura["r_cp"].ToString();
                }
                if (r_factura["r_localidad"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", "; }

                    domicilio_receptor += r_factura["r_localidad"].ToString();
                }
                if (r_factura["r_municipio"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", "; }

                    domicilio_receptor += r_factura["r_municipio"].ToString();
                }
                if (r_factura["r_estado"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", "; }

                    domicilio_receptor += r_factura["r_estado"].ToString();
                }
                if (r_factura["r_pais"].ToString() != "")
                {
                    if (domicilio_receptor != "") { domicilio_receptor += ", "; }

                    domicilio_receptor += r_factura["r_pais"].ToString();
                }
                if (domicilio_receptor != "")
                {
                    comprobante.Receptor.domicilioReceptorSpecified = true;
                    comprobante.Receptor.DomicilioReceptor = domicilio_receptor;
                }
                // Correo y telefono
                if (r_factura["r_correo"].ToString() != "")
                {
                    correo_tel_receptor = "Correo:" + r_factura["r_correo"].ToString();
                }
                if (r_factura["r_telefono"].ToString() != "")
                {
                    if (correo_tel_receptor != "") { correo_tel_receptor += ", "; }

                    correo_tel_receptor = "Tel.:" + r_factura["r_telefono"].ToString();
                }
                if (correo_tel_receptor != "")
                {
                    comprobante.Receptor.correoTelefonoReceptorSpecified = true;
                    comprobante.Receptor.CorreoTelefonoReceptor = correo_tel_receptor;
                }

                // Obtiene el nombre comercial del emisor
                if (r_factura["e_nombre_comercial"].ToString() != "" & r_factura["e_nombre_comercial"].ToString() != null)
                {
                    comprobante.Emisor.nombreComercialEmisorSpecified = true;
                    comprobante.Emisor.NombreComercialEmisor = r_factura["e_nombre_comercial"].ToString();
                }

                // Motivo de cancelación
                comprobante.Motivoc = "";

                if (r_factura["motivo_canc"].ToString() != null & r_factura["motivo_canc"].ToString() != "")
                {
                    comprobante.Motivoc = r_factura["motivo_canc"].ToString();
                }


                if (r_factura["motivo_canc"].ToString() == "01")
                {
                    comprobante.FoliosustSpecified = true;
                    comprobante.Foliosust = r_factura["uuid_sust"].ToString();
                }






                // .....................................................................
                // .    Inicia con la generación de la plantilla y conversión a PDF    .
                // .....................................................................

                string origen_pdf_temp = nombre_xml + ".pdf";
                string destino_pdf = @"C:\Archivos PUDVE\Facturas\" + nombre_xml + ".pdf";

                string ruta = AppDomain.CurrentDomain.BaseDirectory + "/";
                // Creación de un arhivo html temporal
                string ruta_html_temp = ruta + "facturahtml.html";
                // Plantilla que contiene el acomodo del PDF
                string ruta_plantilla_html = ruta + "Plantilla_factura.html";
                string s_html = GetStringOfFile(ruta_plantilla_html);
                string result_html = "";

                result_html = RazorEngine.Razor.Parse(s_html, comprobante);


                // La ruta cambiará si la variable servidor tiene algo
                if (!string.IsNullOrWhiteSpace(servidor))
                {
                    destino_pdf = $@"\\{servidor}\Archivos PUDVE\Facturas\" + nombre_xml + ".pdf";
                }


                // Configuracion de footer y header
                var _footerSettings = new FooterSettings
                {
                    ContentSpacing = 10,
                    FontSize = 10,
                    RightText = "[page] / [topage]"
                };
                var _headerSettings = new HeaderSettings
                {
                    ContentSpacing = 8,
                    FontSize = 9,
                    FontName = "Lucida Sans",
                    LeftText = "Folio " + comprobante.Folio + "  Serie " + comprobante.Serie
                };


                var document = new HtmlToPdfDocument
                {
                    GlobalSettings =
                    {
                        ProduceOutline = true,
                        PaperSize = PaperKind.Letter,
                        Margins =
                        {
                            Top = 2.3,
                            Right = 1.2,
                            Bottom = 2.3,
                            Left = 1.2,
                            Unit = Unit.Centimeters,
                        }
                    },
                    Objects = {
                        new ObjectSettings
                        {
                            HtmlText = result_html,
                            HeaderSettings = _headerSettings,
                            FooterSettings = _footerSettings
                        }
                    }
                };


                // Convertir el documento
                byte[] result = converter.Convert(document);

                ByteArrayToFile(result, destino_pdf);
            }*/

            // .    CODIGO DE LA LIBRERIA WKHTMLTOPDF   .
            // ..........................................

            // Se crea archivo temporal
            /*File.WriteAllText(ruta_html_temp, result_html);

            // Ruta de archivo conversor
            string ruta_wkhtml_topdf = Properties.Settings.Default.rutaDirectorio + @"\wkhtmltopdf\bin\wkhtmltopdf.exe";

            ProcessStartInfo proc_start_info = new ProcessStartInfo();
            proc_start_info.UseShellExecute = false;
            proc_start_info.FileName = ruta_wkhtml_topdf;
            proc_start_info.Arguments = "facturahtml.html " + origen_pdf_temp;

            using (Process process = Process.Start(proc_start_info))
            {
                process.WaitForExit();
            }

            // Copiar el PDF a otra carpeta

            if (File.Exists(origen_pdf_temp))
            {
                File.Copy(origen_pdf_temp, destino_pdf);
            }

            // Eliminar archivo temporal
            File.Delete(ruta_html_temp);
            // Elimina el PDF creado
            File.Delete(origen_pdf_temp);*/
        }

        private static string GetStringOfFile(string ruta_arch)
        {
            string cont = File.ReadAllText(ruta_arch);

            return cont;
        }

        public static IConverter converter =
                new ThreadSafeConverter(
                    new RemotingToolset<PdfToolset>(
                        new Win32EmbeddedDeployment(
                            new TempFolderDeployment()
                        )
                    )
                );


        public static bool ByteArrayToFile(byte[] _ByteArray, string _FileName)
        {
            try
            {
                // Abre el archivo
                FileStream _FileStream = new FileStream(_FileName, FileMode.Create, FileAccess.Write);
                // Escribe un bloque de bytes para este stream usando datos de una matriz de bytes
                _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

                _FileStream.Close();

                return true;
            }
            catch (Exception _Exception)
            {
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            return false;
        }

        private void btn_enviar_Click(object sender, EventArgs e)
        {
            if (opcion6 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            int cont = 0;
            int en = 0;
            string mnsj_error = "";
            string[][] arr_id_env;
            int opc_tipo_factura = Convert.ToInt32(cmb_bx_tipo_factura.SelectedIndex);


            foreach (DataGridViewRow row in datagv_facturas.Rows)
            {
                bool estado = (bool)row.Cells["col_checkbox"].Value;

                if (estado == true)
                {
                    cont++;
                }
                else
                {
                    mnsj_error = "No ha seleccionado alguna factura para enviar.";
                }
            }

            // Obtener el id de la factura a enviar

            if (cont > 0)
            {
                arr_id_env = new string[cont][];

                foreach (DataGridViewRow row in datagv_facturas.Rows)
                {
                    bool estado = (bool)row.Cells["col_checkbox"].Value;

                    if (estado == true)
                    {
                        arr_id_env[en] = new string[2];

                        arr_id_env[en][0] = Convert.ToString(row.Cells["col_id"].Value);
                        arr_id_env[en][1] = Convert.ToString(row.Cells["col_t_comprobante"].Value);
                        en++;
                    }
                }

                // Formulario envío de correo

                Enviar_correo correo = new Enviar_correo(arr_id_env, "factura", opc_tipo_factura);

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

        private void VerToolTip(string texto, int cellRectX, int coordX, int cellRectY, bool mostrar)
        {
            if (mostrar)
            {
                TTMensaje.Show(texto, this, datagv_facturas.Location.X + cellRectX - coordX, datagv_facturas.Location.Y + cellRectY, 1500);
            }
        }

        public void TTMensaje_Draw(object sender, DrawToolTipEventArgs e)
        {
            e.DrawBackground();
            e.DrawBorder();
            e.DrawText();
        }

        private bool descargar_factura(string tipo, int idf, string estatus, int opc, string carpeta_elegida)
        {
            string nombrexml = tipo + idf;
            string n_user = Environment.UserName;
            string ruta_archivos = @"C:\Archivos PUDVE\Facturas\XML_" + nombrexml;
            string ruta_new_carpeta = @"C:\Archivos PUDVE\Facturas\XML_" + nombrexml;
            int opc_tipo_factura = Convert.ToInt32(cmb_bx_tipo_factura.SelectedIndex);
            var servidor = Properties.Settings.Default.Hosting;

            if (ban == false)
            {
                MessageBox.Show("El archivo comprimido con su XML y PDF seran descargados en la carpeta." + carpeta_elegida, "Mensaje del sistema", MessageBoxButtons.OK);

                ban = true;
            }


            // Si la conexión es en red cambia ruta de guardado
            if (!string.IsNullOrWhiteSpace(servidor))
            {
                ruta_new_carpeta = $@"\\{servidor}\Archivos PUDVE\Facturas\XML_" + nombrexml;
                ruta_archivos = $@"\\{servidor}\Archivos PUDVE\Facturas\XML_" + nombrexml;
            }

            // Crear carpeta a comprimir

            if (opc == 3)
            {
                if (!Directory.Exists(ruta_new_carpeta))
                {
                    Directory.CreateDirectory(ruta_new_carpeta);
                }
                else
                {
                    if (opc_tipo_factura == 3)
                    {
                        DirectoryInfo dir_arch = new DirectoryInfo(ruta_new_carpeta);

                        foreach (FileInfo f in dir_arch.GetFiles())
                        {
                            f.Delete();
                        }
                    }
                }
            }

            // Verifica que el PDF ya este creado, de no ser así lo crea

            if (opc == 4 & opc_tipo_factura != 3)
            {
                if (!File.Exists(ruta_archivos + ".pdf"))
                {

                    if (ReadXmlFile(ruta_archivos + ".xml") == "null")
                    {
                        MessageBox.Show("No se encontro el archivo XML en su equipo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    //generar_PDF("XML_" + nombrexml, idf);
                    if (ReadXmlFile(ruta_archivos + ".xml") == "3.3")
                    {
                        verFacturasViejas descargarFac = new verFacturasViejas(idf, ReadXmlFile(ruta_archivos + ".xml", "NoCertificadoSAT"), false, true, ruta_archivos + ".pdf");
                        descargarFac.ShowDialog();
                    }
                    else
                    {
                        verFacturasViejas verfacNueva = new verFacturasViejas(idf, ReadXmlFile(ruta_archivos + ".xml", "NoCertificadoSAT"), true, true, ruta_archivos + ".pdf");
                        verfacNueva.ShowDialog();
                    }


                }
            }

            // Copiar archivos a la carpeta

            if (opc == 5)
            {
                if (opc_tipo_factura < 3)
                {
                    if (!File.Exists(ruta_new_carpeta + "\\XML_" + nombrexml + ".pdf"))
                    {
                        File.Copy(ruta_archivos + ".pdf", ruta_new_carpeta + "\\XML_" + nombrexml + ".pdf");
                    }
                    if (!File.Exists(ruta_new_carpeta + "\\XML_" + nombrexml + ".xml"))
                    {
                        File.Copy(ruta_archivos + ".xml", ruta_new_carpeta + "\\XML_" + nombrexml + ".xml");
                    }
                }
                else
                {
                    if (!File.Exists(ruta_new_carpeta + "\\XML_" + tipo + estatus + idf + ".xml"))
                    {
                        File.Copy(ruta_archivos + ".xml", ruta_new_carpeta + "\\XML_" + tipo + estatus + idf + ".xml");
                    }
                }
            }

            // Comprimir carpeta

            if (opc == 6)
            {
                DateTime fecha_actual = DateTime.UtcNow;
                string fech = fecha_actual.ToString("yyyyMMddhhmmss");

                //string ruta_carpet_comprimida = "C:\\Users\\" + n_user + "\\Desktop\\" + nombrexml + "_" + fech + ".zip";
                string ruta_carpet_comprimida = $@"{carpeta_elegida}\" + nombrexml + "_" + fech + ".zip";


                ZipFile.CreateFromDirectory(ruta_new_carpeta, ruta_carpet_comprimida);
            }


            return true;
        }

        public void inicia_descargaf(string tipo, int idf, string estatus, string carpeta_elegida)
        {
            pBar1.Visible = true;
            lb_texto_descarga.Visible = true;

            pBar1.Minimum = 1;
            pBar1.Maximum = 6;
            pBar1.Value = 2; // Valor inicial
            pBar1.Step = 1;

            for (int x = 3; x <= 6; x++)
            {
                if (descargar_factura(tipo, idf, estatus, x, carpeta_elegida) == true)
                {
                    // Incrementa la barra
                    pBar1.PerformStep();
                }
            }

            ban = false;

            MessageBox.Show("La factura ha sido descargada.", "Mensaje del sistema", MessageBoxButtons.OK);

            pBar1.Visible = false;
            lb_texto_descarga.Visible = false;
        }

        private void btn_primera_pag_Click(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            cargar_lista_facturas();
            actualizar();
            clickBoton = 0;
        }

        private void btn_anterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            cargar_lista_facturas();
            actualizar();
            clickBoton = 0;
        }

        private void btn_siguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            cargar_lista_facturas();
            actualizar();
            clickBoton = 0;
        }

        private void btn_ultima_pag_Click(object sender, EventArgs e)
        {
            p.ultimaPagina();
            clickBoton = 1;
            cargar_lista_facturas();
            actualizar();
            clickBoton = 0;
        }

        private void linklb_pag_anterior_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            p.atras();
            clickBoton = 1;
            cargar_lista_facturas();
            actualizar();
            clickBoton = 0;
        }

        private void linklb_pag_actual_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            actualizar();
        }

        private void linklb_pag_siguiente_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            cargar_lista_facturas();
            actualizar();
            clickBoton = 0;
        }

        private void actualizar()
        {
            int BeforePage = 0, AfterPage = 0, LastPage = 0;

            linklb_pag_anterior.Visible = false;
            linklb_pag_siguiente.Visible = false;

            linklb_pag_actual.Text = p.numPag().ToString();
            linklb_pag_actual.LinkColor = System.Drawing.Color.White;
            linklb_pag_actual.BackColor = System.Drawing.Color.Black;

            BeforePage = p.numPag() - 1;
            AfterPage = p.numPag() + 1;
            LastPage = p.countPag();

            if (Convert.ToInt32(linklb_pag_actual.Text) >= 2)
            {
                linklb_pag_anterior.Text = BeforePage.ToString();
                linklb_pag_anterior.Visible = true;

                if (AfterPage <= LastPage)
                {
                    linklb_pag_siguiente.Text = AfterPage.ToString();
                    linklb_pag_siguiente.Visible = true;
                }
                else if (AfterPage > LastPage)
                {
                    linklb_pag_siguiente.Text = AfterPage.ToString();
                    linklb_pag_siguiente.Visible = false;
                }
            }
            else if (BeforePage < 1)
            {
                linklb_pag_anterior.Visible = false;

                if (AfterPage <= LastPage)
                {
                    linklb_pag_siguiente.Text = AfterPage.ToString();
                    linklb_pag_siguiente.Visible = true;
                }
                else if (AfterPage > LastPage)
                {
                    linklb_pag_siguiente.Text = AfterPage.ToString();
                    linklb_pag_siguiente.Visible = false;
                }
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            int cont = 0;
            int c = 0;
            string mnsj_error = "";
            string[][] arr_id_cmult;
            int opc_tipo_factura = Convert.ToInt32(cmb_bx_tipo_factura.SelectedIndex);
            int tiene_complementos = 0;

            if (opc_tipo_factura == 3)
            {
                MessageBox.Show("La factura ya ha sido cancelada con anterioridad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {

                int tiene_timbres = mb.obtener_cantidad_timbres();

                if (tiene_timbres <= 0)
                {
                    MessageBox.Show("No cuenta con timbres para cancelar el documento. Le sugerimos realizar una compra de timbres.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                foreach (DataGridViewRow row in datagv_facturas.Rows)
                {
                    bool estado = (bool)row.Cells["col_checkbox"].Value;

                    if (estado == true)
                    {
                        var resp = MessageBox.Show("La cancelación tardará unos segundos. Un momento por favor. \n\n ¿Esta seguro que desea cancelar las facturas?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                        if (resp == DialogResult.No)
                        {
                            return;
                        }
                        cont++;

                        // Comprueba que la factura no tenga complementos de pago

                        if (opc_tipo_factura == 1)
                        {
                            var uuidf = cn.EjecutarSelect($"SELECT UUID FROM Facturas WHERE ID='{row.Cells["col_id"].Value}'", 13);

                            var existe_complemento = cn.EjecutarSelect($"SELECT * FROM Facturas_complemento_pago WHERE uuid='{uuidf}' AND timbrada=1 AND cancelada=0");


                            if (Convert.ToBoolean(existe_complemento) == true)
                            {
                                tiene_complementos = 1;
                            }
                        }
                    }
                    else
                    {
                        mnsj_error = "No ha seleccionado alguna factura para cancelar.";
                    }
                }

                if (cont == 0)
                {
                    MessageBox.Show(mnsj_error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (tiene_complementos == 1)
                {
                    MessageBox.Show("Alguna de las facturas no puede ser cancelada porque tiene complementos de pago timbrados. Para continuar por favor desmarque las facturas con complementos de pagos vigentes.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                // Obtener el id de la factura a enviar

                arr_id_cmult = new string[cont][];

                foreach (DataGridViewRow row in datagv_facturas.Rows)
                {
                    bool estado = (bool)row.Cells["col_checkbox"].Value;

                    if (estado == true)
                    {

                        arr_id_cmult[c] = new string[5];

                        arr_id_cmult[c][0] = Convert.ToString(row.Cells["col_id"].Value);
                        arr_id_cmult[c][1] = Convert.ToString(row.Cells["col_t_comprobante"].Value);
                        arr_id_cmult[c][2] = Convert.ToString(row.Cells["col_folio"].Value);
                        c++;
                    }
                }


                // Abre ventana para definir el motivo de cancelación 

                Ventana_motivo_cancelacion vnt_motivo_canc = new Ventana_motivo_cancelacion(1, arr_id_cmult);

                vnt_motivo_canc.FormClosed += delegate
                {
                        // Cargar consulta
                        cargar_lista_facturas(opc_tipo_factura);

                        // Obtenemos la cantidad de timbres
                        actualizar_timbres();
                };

                vnt_motivo_canc.ShowDialog();

                // Mandar a cancelar

                /*for (var z=0; z<arr_id_cmult.Length; z++)
                {
                    string[] respuesta = cancela.cancelar(Convert.ToInt32(arr_id_cmult[z][0]), arr_id_cmult[z][1]);

                    if (respuesta[1] == "201" | respuesta[1] == "202")
                    {
                        // Cambiar a canceladas
                        cn.EjecutarConsulta($"UPDATE Facturas SET cancelada=1, id_emp_cancela='{id_empleado}' WHERE ID='{arr_id_cmult[z][0]}'");
                    }

                    arr_id_cmult[z][3] = respuesta[0];
                    arr_id_cmult[z][4] = respuesta[1];
                }


                // Muestra resultados 

                Cancelar_XML_mensajes canc_mensaje = new Cancelar_XML_mensajes(arr_id_cmult);

                canc_mensaje.FormClosed += delegate
                {
                    // Cargar consulta
                    cargar_lista_facturas(opc_tipo_factura);
                };

                canc_mensaje.ShowDialog();

                // Obtenemos la cantidad de timbres
                actualizar_timbres();*/

            }
        }

        private void Facturas_paint(object sender, PaintEventArgs e)
        {
            if (volver_a_recargar_datos == true)
            {
                cargar_lista_facturas(0, 2);
                volver_a_recargar_datos = false;

                btn_primera_pag.PerformClick();
            }

            cmb_bx_tipo_factura.SelectedIndex = 2;

            // Obtenemos la cantidad de timbres
            actualizar_timbres();
        }

        private void btn_actualizar_timbres_Click(object sender, EventArgs e)
        {
            if (Registro.ConectadoInternet())
            {
                MySqlConnection c = new MySqlConnection();
                c.ConnectionString = "server=74.208.135.60;database=pudve;uid=pudvesoftware;pwd=Steroids12;";


                try
                {
                    int cantidad_timbres = 0;
                    string fecha_actual = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");


                    c.Open();

                    MySqlCommand consulta = c.CreateCommand();

                    // MySQL: Consulta si se han agregado timbres al usuario
                    consulta.CommandText = $"SELECT * FROM historial_timbres WHERE usuario='{FormPrincipal.userNickName}' AND se_agregaron='0'";
                    MySqlDataReader dr = consulta.ExecuteReader();


                    while (dr.Read())
                    {
                        //Console.WriteLine("WHILE" + dr.GetString(6) + " convertido= " + Convert.ToInt32(dr.GetString(6)));
                        cantidad_timbres += Convert.ToInt32(dr.GetString(6));
                    }

                    dr.Close();


                    if (cantidad_timbres > 0)
                    {
                        // SQLite: Agrega timbres a usuario
                        cn.EjecutarConsulta($"UPDATE Usuarios SET timbres= timbres + '{cantidad_timbres}' WHERE ID='{FormPrincipal.userID}'");

                        // MySQL: Indicamos que los timbres han sido agregados
                        string editar = $"UPDATE historial_timbres SET se_agregaron='1', fecha_asignacion='{fecha_actual}' WHERE usuario='{FormPrincipal.userNickName}' AND se_agregaron='0'";

                        MySqlCommand edi = new MySqlCommand(editar, c);
                        edi.ExecuteReader();
                    }

                    c.Close();

                    // Obtenemos la cantidad de timbres
                    actualizar_timbres();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Sin conexión a internet. Esta accción requiere una conexión.", "", MessageBoxButtons.OK);

            }
        }

        private void btn_comprar_timbres_Click(object sender, EventArgs e)
        {
            Process.Start("https://sifo.com.mx/puntodeventa.php");
        }


        public void actualizar_timbres()
        {
            // Obtenemos la cantidad de timbres
            int timbres_disponibles = mb.obtener_cantidad_timbres();
            lb_timbres.Text = timbres_disponibles.ToString();
            ActualizarTimbresOnline(timbres_disponibles);
        }

        private void ActualizarTimbresOnline(int timbres)
        {
            if (Registro.ConectadoInternet())
            {
                MySqlConnection conexion = new MySqlConnection();

                conexion.ConnectionString = "server=74.208.135.60;database=pudve;uid=pudvesoftware;pwd=Steroids12;";

                try
                {
                    conexion.Open();

                    MySqlCommand actualizar = new MySqlCommand("UPDATE usuarios SET timbres = @timbres WHERE usuario = @usuario", conexion);
                    actualizar.Parameters.AddWithValue("@timbres", timbres);
                    actualizar.Parameters.AddWithValue("@usuario", FormPrincipal.userNickName);
                    actualizar.ExecuteNonQuery();

                    //Cerramos la conexion de MySQL
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void buscar_por_confoco(object sender, EventArgs e)
        {
            if (txt_buscar_por.Text == "Buscar por folio, razón social o RFC")
            {
                txt_buscar_por.Text = "";
            }
        }

        private void buscar_por_sinfoco(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_buscar_por.Text))
            {
                txt_buscar_por.Text = "Buscar por folio, razón social o RFC";
            }
        }

        private void datagv_facturas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btn_cancelar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btn_enviar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btn_cpago_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btn_buscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void cmb_bx_tipo_factura_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btn_actualizar_timbres_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btn_comprar_timbres_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void Facturas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void chTodos_CheckedChanged(object sender, EventArgs e)
        {
            obtenerIDSeleccionados();
            //if (chTodos.Checked)
            //{
            //    foreach (DataGridViewRow dgv in datagv_facturas.Rows)
            //    {
            //        try
            //        {
            //            //var idVenta = Convert.ToInt32(dgv.Cells["ID"].Value.ToString());
            //            dgv.Cells["col_checkbox"].Value = true;
            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //    }
            //}
            //else if (!chTodos.Checked)
            //{
            //    foreach (DataGridViewRow dgv in datagv_facturas.Rows)
            //    {
            //        try
            //        {
            //            //var idVenta = Convert.ToInt32(dgv.Cells["ID"].Value.ToString());
            //            dgv.Cells["col_checkbox"].Value = false;
            //        }
            //        catch (Exception ex)
            //        {

            //        }
            //    }
            //}
        }

        private void obtenerIDSeleccionados()
        {
            var incremento = -1;
            if (chTodos.Checked)
            {
                var query = cn.CargarDatos(FiltroAvanzado);

                if (!query.Rows.Count.Equals(0))
                {
                    foreach (DataRow dgv in query.Rows)
                    {
                        if (!facturasD.ContainsKey(Convert.ToInt32(dgv["ID"].ToString())))
                        {
                            facturasD.Add(Convert.ToInt32(dgv["ID"].ToString()), string.Empty);
                        }
                    }
                }

                foreach (DataGridViewRow marcarDGV in datagv_facturas.Rows)
                {
                    try
                    {
                        incremento += 1;
                        datagv_facturas.Rows[incremento].Cells["col_checkbox"].Value = true;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else if (!chTodos.Checked)
            {
                facturasD.Clear();
                foreach (DataGridViewRow desmarcarDGV in datagv_facturas.Rows)
                {
                    try
                    {
                        incremento += 1;
                        datagv_facturas.Rows[incremento].Cells["col_checkbox"].Value = false;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            var datoCombo = cmb_bx_tipo_factura.Text;
            var codigosBuscar = recorrerDGV();

            if (!string.IsNullOrEmpty(codigosBuscar))
            {
                string consultaReportes = $"SELECT folio, serie, r_rfc, r_razon_social, total, fecha_certificacion, id_empleado FROM Facturas WHERE id_usuario =  '{FormPrincipal.userID}' AND ID IN ({codigosBuscar})";

                var query = cn.CargarDatos(consultaReportes);

                if (!query.Rows.Count.Equals(0))
                {
                    Utilidades.GenerarReporteFacturas(datoCombo, query);
                }
            }
            else
            {
                MessageBox.Show("No tiene ninguna factura seleccionada", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string recorrerDGV()
        {
            //listaCheckBox.Clear();

            //foreach (DataGridViewRow dgv in datagv_facturas.Rows)
            //{
            //    try
            //    {
            //        var idFactura = Convert.ToInt32(dgv.Cells["col_id"].Value.ToString());
            //        var checkBox = Convert.ToBoolean(dgv.Cells["col_checkbox"].Value.ToString());

            //        if (checkBox.Equals(true))
            //        {
            //            listaCheckBox.Add(idFactura);
            //        }

            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}
            //Recorre la lista y agregar todo en una sola cadena para la consulta
            var cadenaCompleta = string.Empty;
            foreach (KeyValuePair<int, string> item in facturasD)
            {
                cadenaCompleta += $"{item},".ToString();
            }

            //remplazar caracteress y comas para hacer el filtro por ID
            cadenaCompleta = cadenaCompleta.Replace("[", "");
            cadenaCompleta = cadenaCompleta.Replace("]", "");
            cadenaCompleta = cadenaCompleta.Replace(", ,", ",");
            cadenaCompleta = cadenaCompleta.TrimEnd(',');


            return cadenaCompleta;
        }

        private void llenarGDV()
        {//Los try son para las finas que son para totales que no se marquen

            var incremento = -1;
            foreach (DataGridViewRow dgv in datagv_facturas.Rows)
            {
                try
                {
                    incremento += 1;
                    var idRevision = Convert.ToInt32(dgv.Cells["col_id"].Value.ToString());

                    if (facturasD.ContainsKey(idRevision))
                    {
                        datagv_facturas.Rows[incremento].Cells["col_checkbox"].Value = true;
                    }
                    else
                    {
                        datagv_facturas.Rows[incremento].Cells["col_checkbox"].Value = false;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void MostrarCheckBox()
        {
            System.Drawing.Rectangle rect = datagv_facturas.GetCellDisplayRectangle(0, -1, true);
            // set checkbox header to center of header cell. +1 pixel to position 
            rect.Y = 5;
            rect.X = 10;// rect.Location.X + (rect.Width / 4);
            CheckBox checkBoxHeader = new CheckBox();
            checkBoxHeader.Name = "checkBoxMaster";
            checkBoxHeader.Size = new Size(15, 15);
            checkBoxHeader.Location = rect.Location;
            checkBoxHeader.CheckedChanged += new EventHandler(checkBoxMaster_CheckedChanged);
            datagv_facturas.Controls.Add(checkBoxHeader);
        }

        private void checkBoxMaster_CheckedChanged(object sender, EventArgs e)
        {
            var incremento = -1;

            CheckBox headerBox = ((CheckBox)datagv_facturas.Controls.Find("checkBoxMaster", true)[0]);

            foreach (DataGridViewRow dgv in datagv_facturas.Rows)
            {
                incremento += 1;

                try
                {
                    //var recorrerCheckBox = Convert.ToBoolean(DGVListadoVentas.Rows[incremento].Cells["col_checkbox"].Value);

                    var idRevision = Convert.ToInt32(dgv.Cells["col_id"].Value.ToString());

                    if (headerBox.Checked)
                    {
                        if (!facturasD.ContainsKey(idRevision))
                        {
                            facturasD.Add(idRevision, string.Empty);
                            datagv_facturas.Rows[incremento].Cells["col_checkbox"].Value = true;
                        }
                    }
                    else if (!headerBox.Checked)
                    {
                        facturasD.Remove(idRevision);
                        datagv_facturas.Rows[incremento].Cells["col_checkbox"].Value = false;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        //private void datetp_fecha_inicial_ValueChanged(object sender, EventArgs e)
        //{
        //    DateTime date = DateTime.Now;
        //    DateTime PrimerDia = new DateTime(date.Year, date.Month, 1);
        //    datetp_fecha_inicial.Value = PrimerDia;
        //}

        //private void datetp_fecha_final_ValueChanged(object sender, EventArgs e)
        //{
        //    datetp_fecha_final.Value = DateTime.Now;
        //}
    }
}
