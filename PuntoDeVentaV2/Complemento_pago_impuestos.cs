using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PuntoDeVentaV2
{
    public partial class Complemento_pago_impuestos : Form
    {
        public int fila_im = 1;
        static public bool dats_en_arr = false;

        int fila_dr = 0;        
        int id_factura = 0;
        decimal abono = 0;
        
        List<string> list_traslad_retencion = new List<string>();
                

        public Complemento_pago_impuestos(int num_dr, int id_fct, decimal abonof)
        {
            InitializeComponent();

            // Obtiene el número de fila del documento relacionado
            fila_dr = num_dr; 
            id_factura = id_fct;
            abono = abonof;
        }

        private void Complemento_pago_impuestos_Load(object sender, EventArgs e)
        {
            id_factura = 3406;
            //abono = 5850;
            Console.WriteLine("INICIO");
            int x = 0;
            int fila_dr_tmp= fila_dr - 1;

            datos_xml();


            if (dats_en_arr == true)
            {
                int tam_arr = Complemento_pago.arr_impuestos[fila_dr_tmp].Count();

                for (int i = 0; i < tam_arr; i++)
                {

                    if (Complemento_pago.arr_impuestos[fila_dr_tmp][i][0] == id_factura.ToString())
                    {
                        string g_base = Complemento_pago.arr_impuestos[fila_dr_tmp][i][1];
                        string g_es_rt = Complemento_pago.arr_impuestos[fila_dr_tmp][i][2];
                        string g_impuesto = Complemento_pago.arr_impuestos[fila_dr_tmp][i][3];
                        string g_tfactor = Complemento_pago.arr_impuestos[fila_dr_tmp][i][4];
                        string g_tasa_cuota = Complemento_pago.arr_impuestos[fila_dr_tmp][i][5];
                        string g_definir = Complemento_pago.arr_impuestos[fila_dr_tmp][i][6];
                        string g_importe = Complemento_pago.arr_impuestos[fila_dr_tmp][i][7];

                        if (x == 0)
                        {
                            txt_base0_7.Text = g_base; Console.WriteLine("base inicial="+txt_base0_7.Text);

                            if (g_tfactor == "Exento")
                            {
                                rb_exento.Checked = true;
                            }
                            else
                            {
                                if (g_tasa_cuota == "0.000000")
                                {
                                    rb_cero.Checked = true;
                                }
                                if (g_tasa_cuota == "0.080000")
                                {
                                    rb_ocho.Checked = true;
                                }
                                if (g_tasa_cuota == "0.160000")
                                {
                                    rb_dieciseis.Checked = true;
                                }
                            }

                            //txt_importe0_6.Text = Complemento_pago.arr_impuestos[fila_dr_tmp][i][7];

                            x++; Console.WriteLine("base segunda=" + txt_base0_7.Text);
                        }
                        else
                        {
                            btn_agregar.PerformClick();
                            int fila_im_tmp = fila_im - 1;

                            TextBox g_txt_base = (TextBox)this.Controls.Find("txt_base" + fila_im_tmp + "_7", true).FirstOrDefault();
                            ComboBox g_cmb_bx_es_rt = (ComboBox)this.Controls.Find("cmb_bx_es-" + fila_im_tmp + "_1", true).FirstOrDefault();
                            ComboBox g_cmb_bx_timpuesto = (ComboBox)this.Controls.Find("cmb_bx_impuesto-" + fila_im_tmp + "_2", true).FirstOrDefault();
                            ComboBox g_cmb_bx_tfactor = (ComboBox)this.Controls.Find("cmb_bx_tfactor-" + fila_im_tmp + "_3", true).FirstOrDefault();
                            ComboBox g_cmb_bx_tasa_cuota = (ComboBox)this.Controls.Find("cmb_bx_tc-" + fila_im_tmp + "_4", true).FirstOrDefault();
                            TextBox g_txt_definir = (TextBox)this.Controls.Find("txt_definir-" + fila_im_tmp + "_5", true).FirstOrDefault();
                            TextBox g_txt_importe = (TextBox)this.Controls.Find("txt_importe" + fila_im_tmp + "_6", true).FirstOrDefault();


                            // Base
                            g_txt_base.Text = g_base;

                            // Es retención o traslado
                            g_cmb_bx_es_rt.SelectedItem = g_es_rt;
                            select_tipo_deopcion_combobox(g_cmb_bx_es_rt, e);

                            // Tipo impuesto
                            g_cmb_bx_timpuesto.SelectedValue = g_impuesto;
                            select_tipo_deopcion_combobox(g_cmb_bx_timpuesto, e);

                            // Tipo factor
                            g_cmb_bx_tfactor.SelectedValue = g_tfactor;
                            select_tipo_deopcion_combobox(g_cmb_bx_tfactor, e);

                            // Tasa cuota
                            g_cmb_bx_tasa_cuota.SelectedValue = g_tasa_cuota;
                            select_tipo_deopcion_combobox(g_cmb_bx_tasa_cuota, e);

                            // Porcentaje definido

                            if (g_tasa_cuota == "Definir %")
                            {
                                g_txt_definir.Text = g_definir;
                                val_porcentaje(g_txt_definir, e);
                            }

                            // Importe
                            g_txt_importe.Text = g_importe;


                            x++;
                        }
                        Console.WriteLine("base fin=" + txt_base0_7.Text);
                    }
                }
            }
        }

        private void datos_xml()
        {
            var servidor = Properties.Settings.Default.Hosting;
            string rutaXML = @"C:\Archivos PUDVE\Facturas\XML_INGRESOS_" + id_factura + ".xml";

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaXML = $@"\\{servidor}\Archivos PUDVE\Facturas\XML_INGRESOS_" + id_factura + ".xml";
            }

            // Leer XML

            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(rutaXML);

            // Obtener total de XML y porcentaje del abono Total

            XmlAttributeCollection c_total = xdoc.DocumentElement.Attributes;
            decimal xml_total = Convert.ToDecimal(((XmlAttribute)c_total.GetNamedItem("Total")).Value);

            decimal porcentaje_abono = (abono * 100) / xml_total;
            //Console.WriteLine("ab=" + porcentaje_abono);


            // Obtener datos de los nodos impuestos y calcular base

            XmlNodeList nod_list = xdoc.GetElementsByTagName("cfdi:Conceptos");
            XmlNode K = nod_list.Item(0);

            foreach (XmlNode nd_conceptos in K.ChildNodes)
            {
                foreach (XmlNode nd_concepto in nd_conceptos.ChildNodes)
                {
                    if (nd_concepto.HasChildNodes)
                    {
                        foreach (XmlNode nd_impuestos in nd_concepto.ChildNodes)
                        {
                            foreach (XmlNode nd_tras_ret in nd_impuestos.ChildNodes)
                            {
                                var xml_base = nd_tras_ret.Attributes["Base"].Value;
                                var xml_impuesto = nd_tras_ret.Attributes["Impuesto"].Value;
                                var xml_tfactor = nd_tras_ret.Attributes["TipoFactor"].Value;
                                var xml_tcuota = "";

                                if (xml_tfactor != "Exento")
                                {
                                    xml_tcuota = nd_tras_ret.Attributes["TasaOCuota"].Value;
                                }
                                

                                var cadena = "";

                                if (nd_tras_ret.LocalName == "Traslado")
                                {
                                    cadena = "Traslado=" + xml_impuesto + "=" + xml_tfactor + "=" + xml_tcuota;
                                }
                                if (nd_tras_ret.LocalName == "Retencion")
                                {
                                    cadena = "Retención=" + xml_impuesto + "=" + xml_tfactor + "=" + xml_tcuota;
                                }


                                var indice = list_traslad_retencion.IndexOf(cadena);

                                if (indice >= 0)
                                {
                                    indice = indice + 1;

                                    decimal base_actual = Convert.ToDecimal(list_traslad_retencion[indice]);
                                    decimal base_nueva = base_actual + Convert.ToDecimal(xml_base);

                                    decimal base_pcalculos = (Convert.ToDecimal(base_nueva) * porcentaje_abono) / 100;


                                    list_traslad_retencion.RemoveAt(indice);
                                    list_traslad_retencion.Insert(indice, Convert.ToString(base_nueva));

                                    list_traslad_retencion.RemoveAt(indice + 1);
                                    list_traslad_retencion.Insert(indice + 1, Convert.ToString(base_pcalculos));

                                    Console.WriteLine("base_pcalculos==>" + base_pcalculos);
                                }
                                else
                                {
                                    decimal base_pcalculos = (Convert.ToDecimal(xml_base) * porcentaje_abono) / 100;

                                    list_traslad_retencion.Add(cadena);
                                    list_traslad_retencion.Add(xml_base);
                                    list_traslad_retencion.Add(base_pcalculos.ToString());
                                    Console.WriteLine("base_pcalculos=" + base_pcalculos);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            FlowLayoutPanel flpanel_fila = new FlowLayoutPanel();
            flpanel_fila.Name = "pnl_xfila_impuesto-" + fila_im;
            flpanel_fila.Height = 28;
            flpanel_fila.Width = 710;


            // TextBox: Base

            TextBox txt_base = new TextBox();
            txt_base.Name = "txt_base" + fila_im + "_7";
            txt_base.Size = new Size(85, 22);
            txt_base.TextAlign = HorizontalAlignment.Center;
            txt_base.Margin = new Padding(3, 0, 0, 0);
            txt_base.ReadOnly = true;
            txt_base.Cursor = Cursors.No;


            // ComboBox: Es...

            ComboBox cmb_box_es = new ComboBox();
            cmb_box_es.Name = "cmb_bx_es-" + fila_im + "_1";
            cmb_box_es.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_box_es.Width = 87;
            cmb_box_es.Margin = new Padding(10, 0, 0, 0);

            cmb_box_es.Items.Add("...");
            cmb_box_es.Items.Add("Traslado");
            cmb_box_es.Items.Add("Retención");

            cmb_box_es.SelectedIndex = 0;
            cmb_box_es.SelectionChangeCommitted += new EventHandler(select_tipo_deopcion_combobox);


            // ComboBox: Impuestos

            ComboBox cmb_box_impuest = new ComboBox();
            cmb_box_impuest.Name = "cmb_bx_impuesto-" + fila_im + "_2";
            cmb_box_impuest.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_box_impuest.Width = 55;
            cmb_box_impuest.Margin = new Padding(16, 0, 0, 0);
            cmb_box_impuest.Enabled = false;
            cmb_box_impuest.SelectionChangeCommitted += new EventHandler(select_tipo_deopcion_combobox);


            // ComboBox: Tipo factor

            ComboBox cmb_box_tfactor = new ComboBox();
            cmb_box_tfactor.Name = "cmb_bx_tfactor-" + fila_im + "_3";
            cmb_box_tfactor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_box_tfactor.Width = 74;
            cmb_box_tfactor.Margin = new Padding(10, 0, 0, 0);
            cmb_box_tfactor.Enabled = false;
            cmb_box_tfactor.SelectionChangeCommitted += new EventHandler(select_tipo_deopcion_combobox);


            // ComboBox: Tasa / cuota

            ComboBox cmb_box_tc = new ComboBox();
            cmb_box_tc.Name = "cmb_bx_tc-" + fila_im + "_4";
            cmb_box_tc.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb_box_tc.Width = 92;
            cmb_box_tc.Margin = new Padding(10, 0, 0, 0);
            cmb_box_tc.Enabled = false;
            cmb_box_tc.SelectionChangeCommitted += new EventHandler(select_tipo_deopcion_combobox);


            // TextBox: Definir

            TextBox txt_definir = new TextBox();
            txt_definir.Name = "txt_definir-" + fila_im + "_5";
            txt_definir.Size = new Size(80, 22);
            txt_definir.TextAlign = HorizontalAlignment.Center;
            txt_definir.Margin = new Padding(15, 0, 0, 0);
            txt_definir.Enabled = false;
            txt_definir.LostFocus += new EventHandler(val_porcentaje);


            // TextBox: Importe

            TextBox txt_importe = new TextBox();
            txt_importe.Name = "txt_importe" + fila_im + "_6";
            txt_importe.Size = new Size(119, 22);
            txt_importe.TextAlign = HorizontalAlignment.Center;
            txt_importe.Margin = new Padding(28, 0, 0, 0);
            txt_importe.Enabled = false;
            txt_importe.ReadOnly = true;
            txt_importe.Cursor = Cursors.No;


            // Button: Eliminar

            Button btn_eliminar = new Button();
            btn_eliminar.Name = "btn_elimiar-" + fila_im;
            btn_eliminar.Size = new Size(20, 23);
            btn_eliminar.Cursor = Cursors.Hand;
            btn_eliminar.FlatStyle = FlatStyle.Flat;
            btn_eliminar.BackColor = ColorTranslator.FromHtml("192, 0, 0");
            btn_eliminar.ForeColor = Color.White;
            btn_eliminar.Text = "X";
            btn_eliminar.Margin = new Padding(5, 0, 0, 0);
            btn_eliminar.Click += new EventHandler(eliminar_fila);



            flpanel_fila.Controls.Add(txt_base);
            flpanel_fila.Controls.Add(cmb_box_es);
            flpanel_fila.Controls.Add(cmb_box_impuest);
            flpanel_fila.Controls.Add(cmb_box_tfactor);
            flpanel_fila.Controls.Add(cmb_box_tc);
            flpanel_fila.Controls.Add(txt_definir);
            flpanel_fila.Controls.Add(txt_importe);
            flpanel_fila.Controls.Add(btn_eliminar);

            flpanel_fila.FlowDirection = FlowDirection.LeftToRight;

            pnl_impuestos.Controls.Add(flpanel_fila);

            fila_im++;
        }
        
        private void eliminar_fila(object sender, EventArgs e)
        {
            Button fila_boton = (Button)sender;

            var fila_btn = fila_boton.Name.Split('-');
            string nombre_flpanel = "pnl_xfila_impuesto-" + fila_btn[1];

            foreach (Control panel in pnl_impuestos.Controls.OfType<FlowLayoutPanel>())
            {
                if (panel.Name == nombre_flpanel)
                {
                    pnl_impuestos.Controls.Remove(panel);
                }
            }
        }

        private void select_tipo_deopcion_combobox(object sender, EventArgs e)
        {
            ComboBox cmb_box = (ComboBox)sender;                                                                

            string nombre_cmb_bx = cmb_box.Name;
            var obtener_ids = nombre_cmb_bx.Split('-');
            var obtener_num = obtener_ids[1].Split('_');
            int fila_im = Convert.ToInt32(obtener_num[0]);              
            int num_col = Convert.ToInt32(obtener_num[1]);


            if (cmb_box.GetItemText(cmb_box.SelectedItem) != "..." & cmb_box.GetItemText(cmb_box.SelectedItem) != "")
            {
                // ComboBox: Es

                if (num_col == 1)
                {
                    string opcion_act = cmb_box.GetItemText(cmb_box.SelectedItem);
                    string nombre_cmb_bx_sig = "cmb_bx_impuesto-" + fila_im + "_2";

                    limpiar_combobox(1, fila_im);

                    ComboBox cmb_bx_sig = (ComboBox)this.Controls.Find(nombre_cmb_bx_sig, true).FirstOrDefault();


                    Dictionary<string, string> cmb_bx_opciones = new Dictionary<string, string>();
                    cmb_bx_opciones.Add("", "...");

                    if(opcion_act == "Retención")
                    {
                        cmb_bx_opciones.Add("001", "ISR");
                    }

                    cmb_bx_opciones.Add("002", "IVA");
                    cmb_bx_opciones.Add("003", "IEPS");

                    cmb_bx_sig.DataSource = cmb_bx_opciones.ToArray();
                    cmb_bx_sig.DisplayMember = "Value";
                    cmb_bx_sig.ValueMember = "Key";
                    cmb_bx_sig.SelectedIndex = 0;

                    cmb_bx_sig.Enabled = true;
                }

                // ComboBox: Impuestos

                if (num_col == 2)
                {
                    string opcion_act = cmb_box.SelectedValue.ToString();  
                    string nombre_cmb_bx_sig = "cmb_bx_tfactor-" + fila_im + "_3";
                    string nombre_cmb_bx_ant = "cmb_bx_es-" + fila_im + "_1";

                    limpiar_combobox(2, fila_im);

                    ComboBox cmb_bx_sig = (ComboBox)this.Controls.Find(nombre_cmb_bx_sig, true).FirstOrDefault();
                    ComboBox cmb_bx_ant = (ComboBox)this.Controls.Find(nombre_cmb_bx_ant, true).FirstOrDefault();

                    string opcion_ant = cmb_bx_ant.GetItemText(cmb_bx_ant.SelectedItem);


                    Dictionary<string, string> cmb_bx_opciones = new Dictionary<string, string>();
                    cmb_bx_opciones.Add("", "...");
                    cmb_bx_opciones.Add("Tasa", "Tasa");

                    if (opcion_ant == "Traslado" & opcion_act == "002")
                    {
                        cmb_bx_opciones.Add("Exento", "Exento");
                    }
                    if ((opcion_ant == "Traslado" | opcion_ant == "Retención") & opcion_act == "003")
                    {
                        cmb_bx_opciones.Add("Cuota", "Cuota");
                    }                    

                    cmb_bx_sig.DataSource = cmb_bx_opciones.ToArray();
                    cmb_bx_sig.DisplayMember = "Value";
                    cmb_bx_sig.ValueMember = "Key";
                    cmb_bx_sig.SelectedIndex = 0;

                    cmb_bx_sig.Enabled = true;
                }

                // ComboBox: Tipo factor

                if (num_col == 3)
                {
                    string opcion_act = cmb_box.SelectedValue.ToString();
                    string nombre_cmb_bx_sig = "cmb_bx_tc-" + fila_im + "_4";
                    string nombre_cmb_bx_ant = "cmb_bx_impuesto-" + fila_im + "_2";
                    string nombre_cmb_bx_ini = "cmb_bx_es-" + fila_im + "_1";

                    limpiar_combobox(3, fila_im);

                    ComboBox cmb_bx_sig = (ComboBox)this.Controls.Find(nombre_cmb_bx_sig, true).FirstOrDefault();
                    ComboBox cmb_bx_ant = (ComboBox)this.Controls.Find(nombre_cmb_bx_ant, true).FirstOrDefault();
                    ComboBox cmb_bx_ini = (ComboBox)this.Controls.Find(nombre_cmb_bx_ini, true).FirstOrDefault();

                    string opcion_ant = cmb_bx_ant.SelectedValue.ToString();
                    string opcion_ini = cmb_bx_ini.GetItemText(cmb_bx_ini.SelectedItem);


                    Dictionary<string, string> cmb_bx_opciones = new Dictionary<string, string>();

                    cmb_bx_opciones.Add("", "...");

                    if(opcion_ini == "Traslado" & opcion_act == "Tasa" & (opcion_ant == "002" | opcion_ant == "003"))
                    {
                        cmb_bx_opciones.Add("0.000000", "0 %");
                    }
                    if (opcion_ini == "Traslado" & opcion_act == "Tasa" & opcion_ant == "002")
                    {
                        cmb_bx_opciones.Add("0.080000", "8 %");
                        cmb_bx_opciones.Add("0.160000", "16 %");
                    }
                    if ((opcion_ant == "003" & opcion_act == "Cuota") | 
                        (opcion_ini == "Retención" & opcion_act == "Tasa" & (opcion_ant == "001" | opcion_ant == "002")) )
                    {
                        cmb_bx_opciones.Add("Definir %", "Definir %");
                    }
                    if (opcion_ant == "003" & opcion_act == "Tasa")
                    {
                        cmb_bx_opciones.Add("0.265000", "26.5 %");
                        cmb_bx_opciones.Add("0.300000", "30 %");
                        cmb_bx_opciones.Add("0.530000", "53 %");
                        cmb_bx_opciones.Add("0.500000", "50 %");
                        cmb_bx_opciones.Add("1.600000", "1.600000");
                        cmb_bx_opciones.Add("0.304000", "30.4 %");
                        cmb_bx_opciones.Add("0.250000", "25 %");
                        cmb_bx_opciones.Add("0.090000", "9 %");
                        cmb_bx_opciones.Add("0.080000", "8 %");
                        cmb_bx_opciones.Add("0.070000", "7 %");
                        cmb_bx_opciones.Add("0.060000", "6 %");

                        if(opcion_ini == "Traslado")
                        {
                            cmb_bx_opciones.Add("0.030000", "3 %");
                        }                        
                    }

                    cmb_bx_sig.DataSource = cmb_bx_opciones.ToArray();
                    cmb_bx_sig.DisplayMember = "Value";
                    cmb_bx_sig.ValueMember = "Key";
                    cmb_bx_sig.SelectedIndex = 0;

                    cmb_bx_sig.Enabled = true;

                }

                // ComboBox: Tasa / cuota

                if (num_col == 4)
                {
                    string opcion_act = cmb_box.SelectedValue.ToString();
                    string nombre_cmb_bx_ant = "cmb_bx_tfactor-" + fila_im + "_3";
                    string nombre_cmb_bx_int = "cmb_bx_impuesto-" + fila_im + "_2";
                    string nombre_cmb_bx_ini = "cmb_bx_es-" + fila_im + "_1";

                    ComboBox cmb_bx_ant = (ComboBox)this.Controls.Find(nombre_cmb_bx_int, true).FirstOrDefault();
                    ComboBox cmb_bx_int = (ComboBox)this.Controls.Find(nombre_cmb_bx_ant, true).FirstOrDefault();
                    ComboBox cmb_bx_ini = (ComboBox)this.Controls.Find(nombre_cmb_bx_ini, true).FirstOrDefault();

                    string opcion_ant = cmb_bx_ant.SelectedValue.ToString();
                    string opcion_int = cmb_bx_ant.SelectedValue.ToString();
                    string opcion_ini = cmb_bx_ini.GetItemText(cmb_bx_ini.SelectedItem);

                    
                    if (opcion_act == "Definir %")
                    {
                        TextBox txt_definir = (TextBox)this.Controls.Find("txt_definir-" + fila_im + "_5", true).FirstOrDefault();
                        txt_definir.Enabled = true;
                        txt_definir.Focus();
                    }
                    if (opcion_act != "Definir %")
                    {
                        calcular_importe_ximpuesto(fila_im, "");
                    }

                    TextBox txt_importe = (TextBox)this.Controls.Find("txt_importe" + fila_im + "_6", true).FirstOrDefault();
                    txt_importe.Enabled = false;
                }
            }
            else
            {
                limpiar_combobox(num_col, fila_im);
            }
        }

        private void limpiar_combobox(int opc, int nfila)
        {
            if (opc == 1)
            {
                ComboBox cmb_bx_impuesto = (ComboBox)this.Controls.Find("cmb_bx_impuesto-" + nfila + "_2", true).FirstOrDefault();
                cmb_bx_impuesto.DataSource = null;
                cmb_bx_impuesto.Items.Clear();

                cmb_bx_impuesto.Enabled = false;
            }
            if (opc == 1 | opc == 2)
            {
                ComboBox cmb_bx_tfactor = (ComboBox)this.Controls.Find("cmb_bx_tfactor-" + nfila + "_3", true).FirstOrDefault();
                cmb_bx_tfactor.DataSource = null;
                cmb_bx_tfactor.Items.Clear();

                cmb_bx_tfactor.Enabled = false;
            }
            if (opc == 1 | opc == 2 | opc == 3)
            {
                ComboBox cmb_bx_tc = (ComboBox)this.Controls.Find("cmb_bx_tc-" + nfila + "_4", true).FirstOrDefault();
                cmb_bx_tc.DataSource = null;
                cmb_bx_tc.Items.Clear();

                cmb_bx_tc.Enabled = false;
            }
            if (opc == 1 | opc == 2 | opc == 3 | opc == 4)
            {
                TextBox txt_definir = (TextBox)this.Controls.Find("txt_definir-" + nfila + "_5", true).FirstOrDefault();
                txt_definir.Text = string.Empty;
                txt_definir.Enabled = false;

                TextBox txt_importe = (TextBox)this.Controls.Find("txt_importe" + nfila + "_6", true).FirstOrDefault();
                txt_importe.Text = string.Empty;
                txt_importe.Enabled = false;
            }
            
        }

        private void radio_impuest_8(object sender, EventArgs e)
        {
            if(rb_ocho.Checked == true)
            {
                calcular_importe_ximpuesto(0, "d8");
            }            
        }

        private void radio_impuest_16(object sender, EventArgs e)
        {
            if (rb_dieciseis.Checked == true)
            {
                calcular_importe_ximpuesto(0, "d16");
            }
        }

        private void radio_impuest_exento(object sender, EventArgs e)
        {
            if (rb_exento.Checked == true)
            {
                calcular_importe_ximpuesto(0, "de");
            }
        }

        private void radio_impuest_0(object sender, EventArgs e)
        {
            if (rb_cero.Checked == true)
            {
                calcular_importe_ximpuesto(0, "d0");
            }
        }

        private void val_porcentaje(object sender, EventArgs e)
        {
            TextBox txt_definir = (TextBox)sender;
            var nombre = txt_definir.Name;
            var datos = nombre.Split('-');
            var dato = datos[1].Split('_');
            int nfila = Convert.ToInt32(dato[0]);
            decimal lim_inf = 0;
            decimal lim_sup = 0;


            if(txt_definir.Text != "")
            {
                ComboBox cmb_bx_es_rt = (ComboBox)this.Controls.Find("cmb_bx_es-" + nfila + "_1", true).FirstOrDefault();
                ComboBox cmb_bx_timpuesto = (ComboBox)this.Controls.Find("cmb_bx_impuesto-" + nfila + "_2", true).FirstOrDefault();
                ComboBox cmb_bx_tfactor = (ComboBox)this.Controls.Find("cmb_bx_tfactor-" + nfila + "_3", true).FirstOrDefault();


                if (cmb_bx_timpuesto.SelectedValue.ToString() == "003" & cmb_bx_tfactor.SelectedValue.ToString() == "Cuota")
                {
                    lim_inf = 0;
                    lim_sup = 59.1449m;
                }
                if (cmb_bx_es_rt.GetItemText(cmb_bx_es_rt.SelectedItem) == "Retención" & cmb_bx_tfactor.SelectedValue.ToString() == "Tasa")
                {
                    lim_inf = 0;

                    if (cmb_bx_timpuesto.SelectedValue.ToString() == "001")
                    {
                        lim_sup = 35m;

                        if (Convert.ToDecimal(txt_definir.Text) < 1)
                        {
                            lim_sup = 0.35m;
                        }
                    }
                    if (cmb_bx_timpuesto.SelectedValue.ToString() == "002")
                    {
                        lim_sup = 16m;

                        if (Convert.ToDecimal(txt_definir.Text) < 1)
                        {
                            lim_sup = 0.16m;
                        }
                    }
                }

                if (Convert.ToDecimal(txt_definir.Text) >= lim_inf & Convert.ToDecimal(txt_definir.Text) <= lim_sup)
                {
                    calcular_importe_ximpuesto(nfila, "");
                }
                else
                {
                    MessageBox.Show("El porcentaje se debe encontrar entre el " + lim_inf.ToString() + "% y " + lim_sup.ToString() + "%", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            
        }

        private void calcular_importe_ximpuesto(int nfila, string imp_def)
        {
            float base_tmp = 0;
            string cadena = "";
            decimal porcent_impuest = 0;

            
            // Impuesto de los radio 

            if(nfila == 0 & imp_def != "")
            {
                cadena = "Traslado=002=Tasa=";

                if(imp_def == "d0")
                {
                    cadena += "0.000000";
                    porcent_impuest = 0;
                }
                if (imp_def == "d8")
                {
                    cadena += "0.080000";
                    porcent_impuest = 0.08m;
                }
                if (imp_def == "d16")
                {
                    cadena += "0.160000";
                    porcent_impuest = 0.16m;
                }
                if (imp_def == "de")
                {
                    //cadena += "0.160000";
                    cadena = "Traslado=002=Exento=";
                }
            }

            // Impuesto de los select

            if (nfila > 0 & imp_def == "")
            {
                ComboBox cmb_bx_es_rt = (ComboBox)this.Controls.Find("cmb_bx_es-" + nfila + "_1", true).FirstOrDefault();
                ComboBox cmb_bx_timpuesto = (ComboBox)this.Controls.Find("cmb_bx_impuesto-" + nfila + "_2", true).FirstOrDefault();
                ComboBox cmb_bx_tfactor = (ComboBox)this.Controls.Find("cmb_bx_tfactor-" + nfila + "_3", true).FirstOrDefault();
                ComboBox cmb_bx_tasa_cuota = (ComboBox)this.Controls.Find("cmb_bx_tc-" + nfila + "_4", true).FirstOrDefault();

                string opcion_es_rt = cmb_bx_es_rt.GetItemText(cmb_bx_es_rt.SelectedItem);
                string opcion__timpuesto = cmb_bx_timpuesto.SelectedValue.ToString();
                string opcion_tfactor = cmb_bx_tfactor.SelectedValue.ToString();
                string opcion_tasa_cuota = cmb_bx_tasa_cuota.SelectedValue.ToString();

                cadena = opcion_es_rt + "=" + opcion__timpuesto + "=" + opcion_tfactor + "=";

                if(opcion_tasa_cuota == "Definir %")
                {
                    TextBox txt_definir = (TextBox)this.Controls.Find("txt_definir-" + nfila + "_5", true).FirstOrDefault();

                    porcent_impuest = Convert.ToDecimal(txt_definir.Text);

                    if (porcent_impuest > 1)
                    {
                        porcent_impuest = porcent_impuest / 100;
                    }

                    cadena += seis_decimales(porcent_impuest);
                }
                else
                {
                    cadena += opcion_tasa_cuota;
                    porcent_impuest = Convert.ToDecimal(opcion_tasa_cuota);
                }
            }

            // Busca si el impuesto esta agregado en la lista. 
            // De ser asi, obtiene la base para sacar el impuesto.

            var indice = list_traslad_retencion.IndexOf(cadena);            

            if (indice >= 0)
            {
                decimal importe_impuesto = Convert.ToDecimal(list_traslad_retencion[indice + 2]) * porcent_impuest;

                TextBox txt_base = (TextBox)this.Controls.Find("txt_base" + nfila + "_7", true).FirstOrDefault();
                TextBox txt_importe_imp = (TextBox)this.Controls.Find("txt_importe" + nfila + "_6", true).FirstOrDefault();

                txt_base.Text = list_traslad_retencion[indice + 2];
                txt_importe_imp.Text = seis_decimales(importe_impuesto).ToString();
            }
            else
            {
                MessageBox.Show("El impuesto seleccionado no pertenece a los impuestos agregados en el XML de esta factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private decimal seis_decimales(decimal c)
        {
            decimal cantidad = Decimal.Round(c, 6);

            if (cantidad % 2 == 0)
            {
            }
            else
            {
                cantidad = Convert.ToDecimal(cantidad.ToString(".000000"));
            }

            return cantidad;
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            int cant_filas = pnl_impuestos.Controls.Count + 1;
            int i = 0;
            bool rad_imp = false;
            string tipo_imp = "";
            string tasa_cuota = "Tasa";
            int fila_dr_tmp = fila_dr - 1;
            Complemento_pago.arr_impuestos[fila_dr_tmp] = new string[cant_filas][];

            string cadena = "Traslado=002=Tasa=";


            // Impuesto de los radio

            if (rb_cero.Checked)
            {
                tipo_imp = "0.000000";
                rad_imp = true;
                cadena += "0.000000";

                // Busca si el impuesto esta agregado en la lista. 

                var indice = list_traslad_retencion.IndexOf(cadena);

                if (indice < 0)
                {
                    MessageBox.Show("El impuesto seleccionado no pertenece a los impuestos agregados en el XML de esta factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (rb_ocho.Checked)
            {
                tipo_imp = "0.080000";
                rad_imp = true;
                cadena += "0.080000";

                // Busca si el impuesto esta agregado en la lista. 

                var indice = list_traslad_retencion.IndexOf(cadena);

                if (indice < 0)
                {
                    MessageBox.Show("El impuesto seleccionado no pertenece a los impuestos agregados en el XML de esta factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (rb_dieciseis.Checked)
            {
                tipo_imp = "0.160000";
                rad_imp = true;
                cadena += "0.160000";

                // Busca si el impuesto esta agregado en la lista. 

                var indice = list_traslad_retencion.IndexOf(cadena);

                if (indice < 0)
                {
                    MessageBox.Show("El impuesto seleccionado no pertenece a los impuestos agregados en el XML de esta factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (rb_exento.Checked)
            {
                tasa_cuota = "Exento";
                rad_imp = true;
                cadena = "Traslado=002=Exento=";

                // Busca si el impuesto esta agregado en la lista. 

                var indice = list_traslad_retencion.IndexOf(cadena);

                if (indice < 0)
                {
                    MessageBox.Show("El impuesto seleccionado no pertenece a los impuestos agregados en el XML de esta factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            

            // Valida que exista un impuesto elegido de los radio

            if (rad_imp == false)
            {
                MessageBox.Show("Debe elegir un impuesto de al valor agregado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Si rad_imp == true, entonces guarda en arreglo

            if(rad_imp == true)
            {
                Complemento_pago.arr_impuestos[fila_dr_tmp][i] = new string[8];

                Complemento_pago.arr_impuestos[fila_dr_tmp][i][0] = id_factura.ToString();
                Complemento_pago.arr_impuestos[fila_dr_tmp][i][1] = txt_base0_7.Text;
                Complemento_pago.arr_impuestos[fila_dr_tmp][i][2] = "Traslado";
                Complemento_pago.arr_impuestos[fila_dr_tmp][i][3] = "002";
                Complemento_pago.arr_impuestos[fila_dr_tmp][i][4] = tasa_cuota;
                Complemento_pago.arr_impuestos[fila_dr_tmp][i][5] = tipo_imp;
                Complemento_pago.arr_impuestos[fila_dr_tmp][i][6] = "";
                Complemento_pago.arr_impuestos[fila_dr_tmp][i][7] = txt_importe0_6.Text;

                i++;
            }



            // Impuestos de los select

            if (cant_filas > 0)
            {
                foreach (Control panel_general in pnl_impuestos.Controls.OfType<FlowLayoutPanel>())
                {
                    foreach(Control panel_ximp in panel_general.Controls.OfType<Control>())
                    {
                        if (panel_ximp.Name.Contains("txt_base"))
                        {
                            if (string.IsNullOrEmpty(panel_ximp.Text))
                            {
                                MessageBox.Show("En la fila " + i + " no se ha completado toda la información del impuesto. \n Eliminar la fila si: \n - El impuesto no ha sido completado y no se usará. \n - El impuesto agregado no se encuentra en el XML de la factura a la que se hace el abono.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                // Elimina registro que se halla guardado

                                for (int j = 0; j < i; j++) 
                                {
                                    if(Complemento_pago.arr_impuestos[fila_dr_tmp][j][0] == id_factura.ToString())
                                    {
                                        Complemento_pago.arr_impuestos[fila_dr_tmp][j][0] = "";
                                    }
                                }

                                return;
                            }

                            Complemento_pago.arr_impuestos[fila_dr_tmp][i] = new string[8];

                            Complemento_pago.arr_impuestos[fila_dr_tmp][i][0] = id_factura.ToString();
                            Complemento_pago.arr_impuestos[fila_dr_tmp][i][1] = panel_ximp.Text;
                        }
                        if (panel_ximp.Name.Contains("cmb_bx_es"))
                        {
                            Complemento_pago.arr_impuestos[fila_dr_tmp][i][2] = panel_ximp.Text;
                        }
                        if (panel_ximp.Name.Contains("cmb_bx_impuesto"))
                        {
                            var t_impuesto = "";

                            if (panel_ximp.Text == "ISR") { t_impuesto = "001"; }
                            if (panel_ximp.Text == "IVA") { t_impuesto = "002"; }
                            if (panel_ximp.Text == "IEPS") { t_impuesto = "003"; }

                            Complemento_pago.arr_impuestos[fila_dr_tmp][i][3] = t_impuesto;
                        }
                        if (panel_ximp.Name.Contains("cmb_bx_tfactor"))
                        {
                            Complemento_pago.arr_impuestos[fila_dr_tmp][i][4] = panel_ximp.Text;
                        }
                        if (panel_ximp.Name.Contains("cmb_bx_tc"))
                        {
                            Complemento_pago.arr_impuestos[fila_dr_tmp][i][5] = panel_ximp.Text;
                        }
                        if (panel_ximp.Name.Contains("txt_definir"))
                        {
                            Complemento_pago.arr_impuestos[fila_dr_tmp][i][6] = panel_ximp.Text;
                        }
                        if (panel_ximp.Name.Contains("txt_importe"))
                        {
                            Complemento_pago.arr_impuestos[fila_dr_tmp][i][7] = panel_ximp.Text;

                            i++;
                        }
                    }
                }
            }

            limpiar_vnt();
            dats_en_arr = true;

            this.Dispose();
            
        }

        private void limpiar_vnt()
        {
            pnl_impuestos.Controls.Clear();
            rb_cero.Checked = false;
            rb_ocho.Checked = false;
            rb_dieciseis.Checked = false;
            rb_exento.Checked = false;
            txt_base0_7.Text = string.Empty;
            txt_importe0_6.Text = string.Empty;
        }

        private void Complemento_pago_impuestos_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
