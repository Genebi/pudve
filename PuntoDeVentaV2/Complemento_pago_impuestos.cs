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
    public partial class Complemento_pago_impuestos : Form
    {
        int fila_dr = 0;
        int fila_im = 1;

        public Complemento_pago_impuestos(int num_dr)
        {
            InitializeComponent();

            // Obtiene el número de fila del documento relacionado
            fila_dr = num_dr; Console.WriteLine("fila_dr=" + fila_dr);
        }

        private void Complemento_pago_impuestos_Load(object sender, EventArgs e)
        {

        }

        private void agregar_nuevo_impuesto(object sender, EventArgs e)
        {
            FlowLayoutPanel flpanel_fila = new FlowLayoutPanel();
            flpanel_fila.Name = "pnl_xfila_impuesto-" + fila_im;
            flpanel_fila.Height = 28;
            flpanel_fila.Width = 630;


            // ComboBox: Es...

            ComboBox cmb_box_es = new ComboBox();
            cmb_box_es.Name = "cmb_bx_es-" + fila_im + "_1";
            cmb_box_es.DropDownStyle = ComboBoxStyle.DropDownList;            
            cmb_box_es.Width = 87;
            cmb_box_es.Margin = new Padding(7, 0, 0, 0);
            
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


            // TextBox: Importe

            TextBox txt_importe = new TextBox();
            txt_importe.Name = "txt_importe-" + fila_im + "_6";
            txt_importe.Size = new Size(119, 22);
            txt_importe.TextAlign = HorizontalAlignment.Center;
            txt_importe.Margin = new Padding(28, 0, 0, 0);
            txt_importe.Enabled = false;
            txt_importe.ReadOnly = true;


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
                        cmb_bx_opciones.Add("definir", "Definir %");
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

                    
                    if (opcion_act == "definir")
                    {
                        TextBox txt_definir = (TextBox)this.Controls.Find("txt_definir-" + fila_im + "_5", true).FirstOrDefault();
                        txt_definir.Enabled = true;
                        txt_definir.Focus();
                    }
                    if (opcion_act != "definir")
                    {
                        calcular_importe_ximpuesto(fila_im);
                    }

                    TextBox txt_importe = (TextBox)this.Controls.Find("txt_importe-" + fila_im + "_6", true).FirstOrDefault();
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

                TextBox txt_importe = (TextBox)this.Controls.Find("txt_importe-" + nfila + "_6", true).FirstOrDefault();
                txt_importe.Text = string.Empty;
                txt_importe.Enabled = false;
            }
            
        }

        private void calcular_importe_ximpuesto(int nfila)
        {
            float base_tmp = 0;

        }
    }
}
