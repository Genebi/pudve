using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using TuesPechkin;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Drawing.Printing;
using System.Net.Mail;

namespace PuntoDeVentaV2
{
    public partial class Facturas : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        int id_usuario = FormPrincipal.userID;
        public static int[] arr_id_facturas;

        CheckBox header_checkb = null;


        public Facturas()
        {
            InitializeComponent();
        }

        private void Facturas_Load(object sender, EventArgs e)
        {
            Dictionary<int, string> t_factura = new Dictionary<int, string>();
            t_factura.Add(0, "Facturas por pagar");
            t_factura.Add(1, "Facturas abonadas");
            t_factura.Add(2, "Facturas pagadas");
            t_factura.Add(3, "Facturas canceladas");

            cmb_bx_tipo_factura.DataSource = t_factura.ToArray();
            cmb_bx_tipo_factura.DisplayMember = "Value";
            cmb_bx_tipo_factura.ValueMember = "Key";
            cmb_bx_tipo_factura.SelectedIndex = 2;

            // Crea un checkbox en la cabecera de la tabla. Será para seleccionar todo.
            ag_checkb_header();
            // Carga las facturas en la tabla 
            cargar_lista_facturas();
            
        }

        public void cargar_lista_facturas(int tipo= 0)
        {
            SQLiteConnection sql_cn;
            SQLiteCommand sql_cmd;
            SQLiteDataReader sql_dr;

            string cons = "";
            string condicional_fecha_i_f = "";

            int opc_tipo_factura = Convert.ToInt32(cmb_bx_tipo_factura.SelectedIndex);
            var fecha_inicial = datetp_fecha_inicial.Value.ToString("yyyy-MM-dd");
            var fecha_final = datetp_fecha_final.Value.ToString("yyyy-MM-dd");
            
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                sql_cn = new SQLiteConnection("Data source=//" + Properties.Settings.Default.Hosting + @"\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }
            else
            {
                sql_cn = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            }

            sql_cn.Open();


            // Se agregan fechas de busqueda cuando es desde el botón buscar
            if(tipo == 1)
            {
                condicional_fecha_i_f = "AND DATE(fecha_certificacion) BETWEEN '"+fecha_inicial+"' AND '"+fecha_final+"'";
            }

            // Por pagar
            if(opc_tipo_factura == 0)
            {
                cons = $"SELECT * FROM Facturas WHERE id_usuario='{id_usuario}' AND tipo_comprobante='I' AND timbrada=1 AND cancelada=0 AND (metodo_pago='PPD' OR forma_pago='99') AND con_complementos=0 " + condicional_fecha_i_f;
            }
            // Abonadas
            if (opc_tipo_factura == 1)
            {
                cons = $"SELECT * FROM Facturas WHERE id_usuario='{id_usuario}' AND tipo_comprobante='I' AND timbrada=1 AND cancelada=0 AND con_complementos=1 AND resta_cpago>0 " + condicional_fecha_i_f;
            }
            // Pagadas
            if (opc_tipo_factura == 2)
            {
                cons = $"SELECT * FROM Facturas WHERE id_usuario='{id_usuario}' AND tipo_comprobante='I' AND timbrada=1 AND cancelada=0 AND (metodo_pago='PUE' AND forma_pago!='99') OR (resta_cpago=0 AND (metodo_pago='PPD' OR forma_pago='99')) " + condicional_fecha_i_f;
            }
            // Canceladas
            if(opc_tipo_factura == 3)
            {
                cons = $"SELECT * FROM Facturas WHERE id_usuario='{id_usuario}' AND timbrada=1 AND cancelada=1 " + condicional_fecha_i_f;
            }

            sql_cmd = new SQLiteCommand(cons, sql_cn);
            sql_dr = sql_cmd.ExecuteReader();
            

            datagv_facturas.Rows.Clear();

            if (sql_dr.HasRows)
            {
                while (sql_dr.Read())
                {
                    int fila_id = datagv_facturas.Rows.Add();
                    DateTime fecha = Convert.ToDateTime(sql_dr.GetValue(sql_dr.GetOrdinal("fecha_certificacion")));
                    string fecha_cert = fecha.ToString("yyyy-MM-dd");

                    DataGridViewRow fila = datagv_facturas.Rows[fila_id];

                    fila.Cells["col_id"].Value = sql_dr.GetValue(sql_dr.GetOrdinal("ID"));
                    fila.Cells["col_t_comprobante"].Value = sql_dr.GetValue(sql_dr.GetOrdinal("tipo_comprobante"));
                    fila.Cells["col_checkbox"].Value = false;
                    fila.Cells["col_folio"].Value = sql_dr.GetValue(sql_dr.GetOrdinal("folio"));
                    fila.Cells["col_serie"].Value = sql_dr.GetValue(sql_dr.GetOrdinal("serie"));
                    fila.Cells["col_rfc"].Value = sql_dr.GetValue(sql_dr.GetOrdinal("r_rfc"));
                    fila.Cells["col_razon_social"].Value = sql_dr.GetValue(sql_dr.GetOrdinal("r_razon_social"));
                    fila.Cells["col_total"].Value = sql_dr.GetValue(sql_dr.GetOrdinal("total"));
                    fila.Cells["col_fecha"].Value = fecha_cert;

                    System.Drawing.Image img_pdf = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");
                    System.Drawing.Image img_cancelar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\bell-slash.png");

                    fila.Cells["col_pdf"].Value = img_pdf;
                    fila.Cells["col_cancelar"].Value = img_cancelar;



                    // Busca los complementos de pago
                    // No aplicará para las facturas por pagar 

                    if (opc_tipo_factura > 0)
                    {
                        int idf = Convert.ToInt32(sql_dr.GetValue(sql_dr.GetOrdinal("ID")));

                        // Busca si tiene complementos de pago para mostrar
                        DataTable d_cpago = cn.CargarDatos(cs.obtiene_cpagos_dfactura_princ(idf, 1));

                        if(d_cpago.Rows.Count > 0)
                        {
                            foreach (DataRow r_cpago in d_cpago.Rows)
                            {
                                int id_fcp = Convert.ToInt32(r_cpago["id_factura"]);
                                decimal importe_pagado = Convert.ToDecimal(r_cpago["importe_pagado"]);

                                // Obtiene datos pertenecientes a cada complemento
                                DataTable d_cpago_f = cn.CargarDatos(cs.obtiene_cpagos_dfactura_princ(id_fcp, 2));

                                foreach (DataRow r_cpago_f in d_cpago_f.Rows)
                                {
                                    int fila_idc = datagv_facturas.Rows.Add();

                                    DataGridViewRow filac = datagv_facturas.Rows[fila_idc];
                                    filac.DefaultCellStyle.ForeColor = Color.Blue;

                                    filac.Cells["col_id"].Value = r_cpago_f["ID"].ToString();
                                    filac.Cells["col_t_comprobante"].Value = "P";
                                    filac.Cells["col_checkbox"].Value = false;
                                    filac.Cells["col_folio"].Value = r_cpago_f["folio"].ToString();
                                    filac.Cells["col_serie"].Value = r_cpago_f["serie"].ToString();
                                    filac.Cells["col_rfc"].Value = r_cpago_f["r_rfc"].ToString();
                                    filac.Cells["col_razon_social"].Value = r_cpago_f["r_razon_social"].ToString();
                                    filac.Cells["col_total"].Value = importe_pagado.ToString();

                                    System.Drawing.Image img_pdfc = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");
                                    System.Drawing.Image img_cancelarc = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\bell-slash.png");

                                    filac.Cells["col_pdf"].Value = img_pdfc;
                                    filac.Cells["col_cancelar"].Value = img_cancelarc;

                                    //filac.Cells["col_razon_social"].Style.ForeColor = Color.Aqua;
                                }
                            }
                        }                        
                    }
                }
                
            }

            sql_cn.Close();
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
            cargar_lista_facturas(1);
        }

        private void buscar_tipo_factura(object sender, EventArgs e)
        {
            cargar_lista_facturas();
        }

        private void click_en_icono(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                int id_factura = Convert.ToInt16(datagv_facturas.Rows[e.RowIndex].Cells["col_id"].Value);
                int opc_tipo_factura = Convert.ToInt32(cmb_bx_tipo_factura.SelectedIndex);
                string t_comprobante = Convert.ToString(datagv_facturas.Rows[e.RowIndex].Cells["col_t_comprobante"].Value);


                // Ver PDF

                if (e.ColumnIndex == 8)
                {
                    string nombre_xml = "";
                    string ruta_archivo = "";

                    if(t_comprobante == "P")
                    {
                        nombre_xml = "XML_PAGO_" + id_factura;
                    }
                    if (t_comprobante == "I")
                    {
                        nombre_xml = "XML_INGRESOS_" + id_factura;
                    }
                    
                    // Verifica si el archivo pdf ya esta creado, de no ser así lo crea
                    ruta_archivo = @"C:\Archivos PUDVE\Facturas\" + nombre_xml + ".pdf";

                    if (!File.Exists(ruta_archivo))
                    {
                        MessageBox.Show("La generación del PDF tardará 10 segundos (aproximadamente) en ser visualizado. Un momento por favor...", "", MessageBoxButtons.OK);
                        //Generar_PDF_factura.generar_PDF(nombre_xml);
                        generar_PDF(nombre_xml);
                    }

                    // Ver PDF de factura
                    Visualizar_factura ver_fct = new Visualizar_factura(nombre_xml);

                    ver_fct.FormClosed += delegate
                    {
                        ver_fct.Dispose();
                    };

                    ver_fct.ShowDialog();
                }

                // Cancelar factura

                if (e.ColumnIndex == 9)
                {
                    if (opc_tipo_factura == 3)
                    {
                        MessageBox.Show("La factura ya fue cancelada con anterioridad.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void cursor_en_icono(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 & e.ColumnIndex >= 8)
            {
                datagv_facturas.Cursor = Cursors.Hand;
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
                foreach (DataGridViewRow row in datagv_facturas.Rows)
                {
                    bool estado= (bool)row.Cells["col_checkbox"].Value;
                    string tipo_comprobante = row.Cells["col_t_comprobante"].Value.ToString();

                    if(estado == true)
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


                if(cont > 0 & cont_pg == 0)
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

                    Complemento_pago c_pago = new Complemento_pago();
  
                    c_pago.FormClosed += delegate
                    {
                        int tipo_factura = Convert.ToInt32(cmb_bx_tipo_factura.SelectedIndex);
                        cargar_lista_facturas(tipo_factura);
                    };

                    c_pago.ShowDialog();
                }
                else
                {
                    MessageBox.Show(mnsj_error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void clickcellc_checkbox(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if ((bool)datagv_facturas.SelectedRows[e.ColumnIndex].Cells["col_checkbox"].Value == false)
                {
                    datagv_facturas.SelectedRows[e.ColumnIndex].Cells["col_checkbox"].Value = true;
                }
                else
                {
                    datagv_facturas.SelectedRows[e.ColumnIndex].Cells["col_checkbox"].Value = false;
                }
            }
        }

        public void generar_PDF(string nombre_xml)
        {
            // ........................................
            // .    Deserealiza el XML ya timbrado    .
            // ........................................


            Comprobante comprobante;
            string ruta_xml = @"C:\Archivos PUDVE\Facturas\" + nombre_xml + ".xml";

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
            int cont = 0;
            int en = 0;
            string mnsj_error = "";
            string[][] arr_id_env;

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

            if(cont > 0)
            {
                arr_id_env = new string[cont][];

                foreach(DataGridViewRow row in datagv_facturas.Rows)
                {
                    bool estado = (bool)row.Cells["col_checkbox"].Value;

                    if(estado == true)
                    {
                        arr_id_env[en] = new string[2];

                        arr_id_env[en][0] = Convert.ToString(row.Cells["col_id"].Value);
                        arr_id_env[en][1] = Convert.ToString(row.Cells["col_t_comprobante"].Value);
                        en++;
                    }
                }

                // Formulario envío de correo
                           
                Enviar_correo correo = new Enviar_correo(arr_id_env, "factura");

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
