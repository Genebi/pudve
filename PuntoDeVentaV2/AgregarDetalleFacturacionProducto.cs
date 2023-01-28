using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class AgregarDetalleFacturacionProducto : Form
    {
        bool primera = true;
        int seleccionado = 0;
        int valorDefault = 0;
        static public int id = 1;
        public static bool ejecutarMetodos = false;

        string tipoImpuesto = null;
        string tipoPorcentaje = null;
        string porcentajeSeleccionado = null;

        private MySqlConnection sql_con;
        private MySqlCommand sql_cmd;

        List<string> clavesUnidad = new List<string>();
        List<string> impuestos = new List<string>();
        List<string> factores = new List<string>();
        List<string> tasasCuotas = new List<string>();

        //Impuestos locales
        List<string> impuestosL = new List<string>();
        List<string> tasaL = new List<string>();

        double precioProducto = 0;

        double porcentaje = 0, totalProcentaje;

        // variables para saber si viene de dar click en Icono de Editar Producto
        public int typeOriginData { get; set; }
        public string UnidadMedida { get; set; }
        public string ClaveProducto { get; set; }
        public int id_producto_edit { get; set; }

        
        static public int typeOriginDataFinal;
        static public string UnidadMedidaFinal;
        static public string ClaveMedidaFinal;

        static public bool editado = false; // Indicará si se dio clic en botón aceptar 
        static public double tmp_edit_base = 0;
        static public double tmp_edit_IVA = 0;
        static public string tmp_edit_impuesto = "";
        static public string tmp_edit_cadena_impuestos= "";


        public int numero_actual_productoxml = 0;


        Conexion cn = new Conexion();
        Consultas cs = new Consultas();


        public void cargarDatos()
        {
            string nombre_tmp_ct = "";
            string rfc_tmp_ct = "";
            string cp_tmp_ct = "";
            string regimen_tmp_ct = "";


            typeOriginDataFinal = typeOriginData;
            UnidadMedidaFinal = UnidadMedida;
            ClaveMedidaFinal = ClaveProducto;

            // Miri.
            // Sección "A cuenta terceros"
            // Agregar, editar, copiar

            if (AgregarEditarProducto.DatosSourceFinal != 3)
            {
                nombre_tmp_ct= Productos.nombre_cnt_3ro;
                rfc_tmp_ct = Productos.rfc_cnt_3ro;
                cp_tmp_ct= Productos.cp_cnt_3ro;
                regimen_tmp_ct = Productos.regimen_cnt_3ro;
            }

            // Agregar por XML

            if (AgregarEditarProducto.DatosSourceFinal == 3)
            {
                nombre_tmp_ct = AgregarStockXML.razon_social_cnt_3ro_delxml;
                rfc_tmp_ct = AgregarStockXML.rfc_cnt_3ro_delxml;
                cp_tmp_ct = AgregarStockXML.cp_cnt_3ro_delxml;
                regimen_tmp_ct = AgregarStockXML.regimen_cnt_3ro_delxml;
            }


            txt_nombre_cterceros.Text = nombre_tmp_ct;
            txt_rfc_cterceros.Text = rfc_tmp_ct;
            txt_cp_cterceros.Text = cp_tmp_ct;

            int longitud= txt_rfc_cterceros.Text.Length;
                
            if(longitud == 12)
            {
                carga_regimen("M");
            }
            if (longitud == 13)
            {
                carga_regimen("F");
            }

            cmb_bx_regimen_cterceros.SelectedValue = regimen_tmp_ct;
        }

        public void limpiarCampos()
        {
            txtBoxBase.Text = "0.0";
            txtIVA.Text = "0.0";
        }

        public void checarRadioButtons()
        {
            //Editar
            /*if (typeOriginData == 2)
            {
                var impuestoSeleccionado = AgregarEditarProducto.impuestoProductoFinal;

                if (impuestoSeleccionado == "0%")
                {
                    rb0porCiento.Checked = true;
                }
                else if (impuestoSeleccionado == "16%")
                {
                    rb16porCiento.Checked = true;
                }
                else if (impuestoSeleccionado == "8%")
                {
                    rb8porCiento.Checked = true;
                }
                else if (impuestoSeleccionado == "Exento")
                {
                    rbExcento.Checked = true;
                }
            }*/

            double porcentajeTmp = 0;
            double precioTmp = 0;

            if (rb0porCiento.Checked == true)
            {
                porcentaje = 0;
                totalProcentaje = precioProducto * porcentaje;
                txtIVA.Text = totalProcentaje.ToString("N2");
            }
            else if (rb8porCiento.Checked == true)
            {
                porcentaje = 0.08;
                porcentajeTmp = 1.08;

                precioTmp = precioProducto / porcentajeTmp;
                totalProcentaje = precioTmp * porcentaje;

                txtIVA.Text = totalProcentaje.ToString("N2");
            }
            else if (rb16porCiento.Checked == true)
            {
                porcentaje = 0.16;
                porcentajeTmp = 1.16;

                precioTmp = precioProducto / porcentajeTmp;
                totalProcentaje = precioTmp * porcentaje;

                txtIVA.Text = totalProcentaje.ToString("N2");
            }
            else if (rbExcento.Checked == true)
            {
                porcentaje = 0;
                totalProcentaje = precioProducto * porcentaje;
                txtIVA.Text = totalProcentaje.ToString("N2");
            }

            if (!string.IsNullOrEmpty(txtIVA.Text))
            {
                var cantidadBase = precioProducto - float.Parse(txtIVA.Text);
                var cantidadTotal = cantidadBase + float.Parse(txtIVA.Text);

                txtBoxBase.Text = cantidadBase.ToString("0.00");
                txtTotal.Text = cantidadTotal.ToString("0.00");


                RecalcularCambioPorcentaje();
                RecalcularTotal();
            }
            
        }

        #region Constructor ===========================================================
        public AgregarDetalleFacturacionProducto()
        {
            InitializeComponent();
            this.ControlBox = false;

            try
            {
                sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = "SELECT * FROM CatalogoUnidadesMedida ORDER BY LOWER(Nombre) ASC";
                sql_cmd.ExecuteNonQuery();

                MySqlDataReader dr = sql_cmd.ExecuteReader();

                ComboboxItem item2 = new ComboboxItem();
                item2.Text = "Selecciona una opción";
                item2.Value = "";
                clavesUnidad.Add("");
                valorDefault = cbUnidadMedida.Items.Add(item2);

                while (dr.Read())
                {
                    string nombreTmp = dr[1] + " - " + dr[2];

                    ComboboxItem item = new ComboboxItem();
                    item.Text = nombreTmp;
                    item.Value = dr[1];

                    clavesUnidad.Add(dr[1].ToString());
                    
                    cbUnidadMedida.Items.Add(item);
                }
            }
            catch (Exception)
            {
                //Falta agregar una variable para manejar la excepcion en caso de ser requerido
            }
        }
        #endregion

        private void btnCancelarDetalle_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AgregarDetalleFacturacionProducto_Load(object sender, EventArgs e)
        {
            cargarDatos();
            cbUnidadMedida.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbLinea1_16.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbLinea1_26.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbLinea1_36.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbLinea1_46.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cmb_bx_incluye_impuestos.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            //Se definen los valores que tendran los ComboBox y TextBox por default
            //al abrir la ventana por primera vez
            precioProducto = Convert.ToDouble(AgregarEditarProducto.precioProducto);


            cbUnidadMedida.SelectedIndex = valorDefault;

            cbUnidadMedida.DropDownStyle = ComboBoxStyle.DropDownList;



            /***************************
             *** Para los ComboBoxes ***
             ***************************/

            impuestos.Add("...");
            impuestos.Add("ISR");
            impuestos.Add("IVA");
            impuestos.Add("IEPS");
            
            factores.Add("...");
            factores.Add("Tasa");
            factores.Add("Cuota");
            factores.Add("Exento");

            tasasCuotas.Add("...");
            tasasCuotas.Add("0 %");
            tasasCuotas.Add("16 %");
            tasasCuotas.Add("Definir %");
            tasasCuotas.Add("26.5 %");
            tasasCuotas.Add("30 %");
            tasasCuotas.Add("53 %");
            tasasCuotas.Add("50 %");
            tasasCuotas.Add("1.600000");
            tasasCuotas.Add("30.4 %");
            tasasCuotas.Add("25 %");
            tasasCuotas.Add("9 %");
            tasasCuotas.Add("8 %");
            tasasCuotas.Add("7 %");
            tasasCuotas.Add("6 %");
            tasasCuotas.Add("3 %");
            
            //Impuestos locales
            impuestosL.Add("...");
            impuestosL.Add("ISH");
            impuestosL.Add("IMCD");
            impuestosL.Add("Bienestar Social");
            impuestosL.Add("Millar");
            impuestosL.Add("Otro");

            tasaL.Add("...");
            tasaL.Add("1 %");
            tasaL.Add("2 %");
            tasaL.Add("3 %");
            tasaL.Add("5 %");
            tasaL.Add("Definir %");

            txtBoxBase.Text = precioProducto.ToString("N2");

            // Miri
            // Incluye impuestos

            Dictionary<string, string> inc_impuestos = new Dictionary<string, string>();
            inc_impuestos.Add("01", "No objeto de impuesto.");
            inc_impuestos.Add("02", "Sí objeto de impuesto.");
            inc_impuestos.Add("03", "Sí objeto del impuesto y no obligado al desglose.");
            inc_impuestos.Add("04", "Sí objeto del impuesto y no causa impuesto.");

            cmb_bx_incluye_impuestos.DataSource = inc_impuestos.ToArray();
            cmb_bx_incluye_impuestos.DisplayMember = "value";
            cmb_bx_incluye_impuestos.ValueMember = "key";


            // Miri.
            // No incluye calcular el IVA de los radios porque hasta este punto no se sabe que 
            // impuesto tiene agregado el producto que se esta registrando desde el XML.

            //if (AgregarEditarProducto.DatosSourceFinal != 3)

            // Se modifica la anterior condicional para nueva versión de CFDI 4.0
            // esto para que no afecte al nuevo campo de "Incluye impuestos".
            // Para registros anteriores se debe primero validar que se tengan o no impuestos agregados.

            if (AgregarEditarProducto.DatosSourceFinal == 1)
            {
                //checarRadioButtons();
                des_habilita_elementos_impuestos(0);
            }
                

            if (typeOriginDataFinal == 2)
            {
                txtClaveProducto.Text = ClaveMedidaFinal;
                txtClaveUnidad.Text = UnidadMedidaFinal;
                CargarClaveUnidad();
            }

            // Miri.
            // Copiar. Se agregan nuevas validaciones para impuestos. Se anexa datos de sección "A cuenta terceros"

            if (AgregarEditarProducto.DatosSourceFinal == 4)
            {
                string incluye_impuestos = Productos.incluye_impuestos;
                var impuestoSeleccionado = AgregarEditarProducto.impuestoProductoFinal;

                // Deshabilita todos los impuestos
                des_habilita_elementos_impuestos(0);

                if(impuestoSeleccionado != "" | incluye_impuestos == "02")
                {
                    des_habilita_elementos_impuestos(1);

                    cmb_bx_incluye_impuestos.SelectedValue = "02";

                    
                    if (impuestoSeleccionado == "0%")
                    {
                        rb0porCiento.Checked = true;
                    }
                    else if (impuestoSeleccionado == "16%")
                    {
                        rb16porCiento.Checked = true;
                    }
                    else if (impuestoSeleccionado == "8%")
                    {
                        rb8porCiento.Checked = true;
                    }
                    else if (impuestoSeleccionado == "Exento")
                    {
                        rbExcento.Checked = true;
                    }

                    cargar_impuestos();
                }
                else
                {
                    cmb_bx_incluye_impuestos.SelectedValue = incluye_impuestos;
                }
            }

            // Miri.
            // Solo para cuando se edite un producto. Carga los impuestos guardados  

            if (AgregarEditarProducto.DatosSourceFinal == 2)
            {
                // Obtiene la lista de impuestos guardados
                //cargar_impuestos();
                
                
                // Este fragmento de código es cambiado aquí, y quitado en "checarRadioButtons()". Se hizo para que al momento de querer seleccionar otro te permita hacerlo, porque no dejaba elegir otro.   
                var impuestoSeleccionado = AgregarEditarProducto.impuestoProductoFinal;
                string incluye_impuestos = Productos.incluye_impuestos;

                // No tiene impuestos incluidos, por lo tanto se desactiva todo lo relacionado a impuestos
                if (impuestoSeleccionado == "")
                {
                    des_habilita_elementos_impuestos(0);
                    cmb_bx_incluye_impuestos.SelectedValue = incluye_impuestos;
                }
                else
                {
                    // Incluye impuestos, por lo que todo lo relacionado queda habilitado

                    des_habilita_elementos_impuestos(1);
                    cmb_bx_incluye_impuestos.SelectedValue = "02";

                    // Obtiene la lista de impuestos guardados
                    cargar_impuestos();


                    if (impuestoSeleccionado == "0%")
                    {
                        rb0porCiento.Checked = true;
                    }
                    else if (impuestoSeleccionado == "16%")
                    {
                        rb16porCiento.Checked = true;
                    }
                    else if (impuestoSeleccionado == "8%")
                    {
                        rb8porCiento.Checked = true;
                    }
                    else if (impuestoSeleccionado == "Exento")
                    {
                        rbExcento.Checked = true;
                    }
                }
            }


            // Miri.
            // El producto se esta registrando desde agregar con XML.

            if (AgregarEditarProducto.DatosSourceFinal == 3)
            {
                cargar_impuestos_dexml();
            }

        }

        private void btnExtra_Click(object sender, EventArgs e)
        {
            if (panelContenedor.Controls.Count > 0)
            {
                ComprobarImpuestos(1);
            }
            else
            {
                GenerarCampos(1);
            }
        }

        private void btnImpLocal_Click(object sender, EventArgs e)
        {
            if (panelContenedor.Controls.Count > 0)
            {
                ComprobarImpuestos(2);
            }
            else
            {
                GenerarCampos(2);
            }
        }

        private void ComprobarImpuestos(int operacion)
        {
            var tipo = string.Empty;
            var importe = string.Empty;

            foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
            {
                foreach (Control item in panel.Controls.OfType<Control>())
                {
                    if (item.Name.Contains("cbLinea"))
                    {
                        var info = item.Name.Split('_');

                        if (info[1].Equals("1"))
                        {
                            tipo = item.Text;
                        }
                    }

                    if (item.Name.Contains("tbLinea"))
                    {
                        var info = item.Name.Split('_');

                        if (info[1].Equals("2"))
                        {
                            importe = item.Text;
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(tipo) && !string.IsNullOrWhiteSpace(importe))
            {
                GenerarCampos(operacion);
            }
            else
            {
                MessageBox.Show("Para agregar otro impuesto se requiere que\ncomplete la información del impuesto anterior", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        //Genera los campos dinamicamente dependiendo de la opcion seleccionada
        private void GenerarCampos(int tipo)
        {
            FlowLayoutPanel panelHijo = new FlowLayoutPanel();
            panelHijo.Name = "panelGeneradoR" + id;
            panelHijo.Height = 25;
            panelHijo.Width = 750;

            string etiqueta1, etiqueta2 = null;

            if (tipo == 1)
            {
                etiqueta1 = "cbLinea";
                etiqueta2 = "tbLinea";
            }
            else
            {
                etiqueta1 = "cbLineaL";
                etiqueta2 = "tbLineaL";
            }

            //Primer ComboBox
            ComboBox cb1 = new ComboBox();
            cb1.Name = etiqueta1 + id + "_1";
            
            if (tipo == 1)
            {
                cb1.Items.Add("...");
                cb1.Items.Add("Traslado");
                cb1.Items.Add("Retención");
            }
            else
            {
                cb1.Items.Add("...");
                cb1.Items.Add("Loc. Traslado");
                cb1.Items.Add("Loc. Retenido");
            }

            cb1.SelectedIndex = 0;
            cb1.DropDownStyle = ComboBoxStyle.DropDownList;
            cb1.MouseWheel += new MouseEventHandler(DeshabilitarMouseWheel);
            cb1.SelectedIndexChanged += new EventHandler(ProcesarComboBoxes_selectedIndexChanged);
            cb1.Width = 100;
            cb1.Margin = new Padding(15, 0, 0, 0);

            //Segundo ComboBox
            ComboBox cb2 = new ComboBox();
            cb2.Name = etiqueta1 + id + "_2";
            cb2.DropDownStyle = ComboBoxStyle.DropDownList;
            cb2.MouseWheel += new MouseEventHandler(DeshabilitarMouseWheel);
            cb2.SelectedIndexChanged += new EventHandler(ProcesarComboBoxes_selectedIndexChanged);
            cb2.Width = 100;
            cb2.Margin = new Padding(20, 0, 0, 0);
            cb2.Enabled = false;

            //Tercer ComboBox
            ComboBox cb3 = new ComboBox();
            cb3.Name = etiqueta1 + id + "_3";
            cb3.DropDownStyle = ComboBoxStyle.DropDownList;
            cb3.MouseWheel += new MouseEventHandler(DeshabilitarMouseWheel);
            cb3.SelectedIndexChanged += new EventHandler(ProcesarComboBoxes_selectedIndexChanged);
            cb3.Width = 100;
            cb3.Margin = new Padding(20, 0, 0, 0);
            cb3.Enabled = false;

            //Cuarto ComboBox
            ComboBox cb4 = new ComboBox();
            cb4.Name = etiqueta1 + id + "_4";
            cb4.DropDownStyle = ComboBoxStyle.DropDownList;
            cb4.MouseWheel += new MouseEventHandler(DeshabilitarMouseWheel);
            cb4.SelectedIndexChanged += new EventHandler(ProcesarComboBoxes_selectedIndexChanged);
            cb4.Width = 100;
            cb4.Margin = new Padding(20, 0, 0, 0);
            cb4.Enabled = false;

            //TextBox para el porcentaje
            TextBox tb1 = new TextBox();
            tb1.Name = etiqueta2 + id + "_1";
            tb1.Width = 100;
            tb1.Height = 20;
            tb1.Margin = new Padding(20, 0, 0, 0);
            tb1.Enabled = false;
            tb1.TextAlign = HorizontalAlignment.Center;
            tb1.KeyUp += new KeyEventHandler(PorcentajeManual_KeyUp);
            tb1.KeyPress += new KeyPressEventHandler(SoloDecimales);

            //TextBox para el importe
            TextBox tb2 = new TextBox();
            tb2.Name = etiqueta2 + id + "_2";
            tb2.Width = 100;
            tb2.Height = 20;
            tb2.Margin = new Padding(20, 0, 0, 0);
            tb2.ReadOnly = true;
            tb2.TextAlign = HorizontalAlignment.Center;

            //Boton eliminar impuesto
            Button bt = new Button();
            bt.Cursor = Cursors.Hand;
            bt.Text = "X";
            bt.Name = "btnGeneradoR" + id;
            bt.Width = 20;
            bt.Height = 20;
            bt.BackColor = ColorTranslator.FromHtml("#C00000");
            bt.ForeColor = ColorTranslator.FromHtml("white");
            bt.FlatStyle = FlatStyle.Flat;
            bt.Click += new EventHandler(EliminarImpuesto);
            bt.Margin = new Padding(5, 0, 0, 0);


            panelHijo.Controls.Add(cb1);
            panelHijo.Controls.Add(cb2);
            panelHijo.Controls.Add(cb3);
            panelHijo.Controls.Add(cb4);
            panelHijo.Controls.Add(tb1);
            panelHijo.Controls.Add(tb2);
            panelHijo.Controls.Add(bt);
            panelHijo.FlowDirection = FlowDirection.LeftToRight;

            panelContenedor.Controls.Add(panelHijo);
            panelContenedor.FlowDirection = FlowDirection.TopDown;

            cb1.Focus();

            id++;
        }

        private void EliminarImpuesto(object sender, EventArgs e)
        {
            Button bt = sender as Button;

            string idBoton = bt.Name.Substring(12);
            string nombrePanel = "panelGeneradoR" + idBoton;

            foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
            {
                if (panel.Name == nombrePanel)
                {
                    RecalcularTotal();

                    panelContenedor.Controls.Remove(panel);
                }
            }

            RecalcularTotal();
        }

        private void DeshabilitarMouseWheel(object sender, EventArgs e)
        {
            HandledMouseEventArgs ee = (HandledMouseEventArgs)e;
            ee.Handled = true;
        }

        private void btnClaveGenerica_Click(object sender, EventArgs e)
        {
            txtClaveProducto.Text = "01010101";
        }

        private void txtClaveUnidad_Leave(object sender, EventArgs e)
        {
            CargarClaveUnidad();
        }

        private void CargarClaveUnidad()
        {
            string[] claves = clavesUnidad.ToArray();

            string valor = txtClaveUnidad.Text;

            int posicion = Array.IndexOf(claves, valor);

            if (posicion > -1)
            {
                cbUnidadMedida.SelectedIndex = posicion;
            }
            else
            {
                MessageBox.Show("La clave " + valor + " no es válida.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbUnidadMedida.SelectedIndex = valorDefault;
            }
        }

        private void cbUnidadMedida_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!primera)
            {
                seleccionado = cbUnidadMedida.SelectedIndex;

                string[] claves = clavesUnidad.ToArray();

                txtClaveUnidad.Text = claves[seleccionado];
            }

            primera = false;
        }

        private void btnAceptarDetalle_Click(object sender, EventArgs e)
        {
            var clavePS = txtClaveProducto.Text;
            var claveUnidad = txtClaveUnidad.Text;

            if (string.IsNullOrWhiteSpace(clavePS))
            {
                clavePS = string.Empty;
            }
            else if (clavePS.Length < 8)
            {
                MessageBox.Show("La clave de producto debe contener 8 dígitos.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(claveUnidad))
            {
                MessageBox.Show("La clave de unidad es requerida.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Miri.
            // Valida datos de a cuenta terceros

            if(!string.IsNullOrEmpty(txt_nombre_cterceros.Text) | !string.IsNullOrEmpty(txt_rfc_cterceros.Text) | !string.IsNullOrEmpty(txt_cp_cterceros.Text))
            {
                if (string.IsNullOrEmpty(txt_nombre_cterceros.Text) | string.IsNullOrEmpty(txt_rfc_cterceros.Text) | string.IsNullOrEmpty(txt_cp_cterceros.Text))
                {
                    MessageBox.Show("No debe haber campos vacíos en la sección 'A cuenta terceros'. Sin un campo de esa sección es llenado, los demás también deberán serlo.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (string.IsNullOrEmpty(cmb_bx_regimen_cterceros.SelectedValue.ToString()) | cmb_bx_regimen_cterceros.SelectedValue.ToString() == "...")
                {
                    MessageBox.Show("Debe haber un tipo de régimen fiscal seleccionado.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // RFC

                string resultado = formato_rfc();

                if(resultado != "")
                {
                    MessageBox.Show(resultado, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Código postal

                string formato_cp = "^[0-9]{5}$";

                Regex exp_cp = new Regex(formato_cp);

                if (!exp_cp.IsMatch(txt_cp_cterceros.Text))
                {
                    MessageBox.Show("El formato del código postal no es valido", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }


            string cadenaImpuestos = null;
            var impuesto = string.Empty;



            // Miri.
            // Los impuestos serán agregados solo si es objeto de impuestos

            if (cmb_bx_incluye_impuestos.SelectedValue.ToString() == "02")
            {
                //Leer todos los datos de los ComboBox y TextBox que se hayan agregado para el producto
                if (panelContenedor.Controls.Count > 0)
                {
                    foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
                    {
                        foreach (Control item in panel.Controls.OfType<Control>())
                        {
                            if (item.Name.Contains("cbLinea"))
                            {
                                cadenaImpuestos += ValidarCampos(item.Text) + ",";
                            }

                            if (item.Name.Contains("tbLinea"))
                            {
                                cadenaImpuestos += ValidarCampos(item.Text) + ",";
                            }
                        }

                        cadenaImpuestos = cadenaImpuestos.Remove(cadenaImpuestos.Length - 1);
                        cadenaImpuestos += "|";
                    }
                }

                //Eliminamos el ultimo caracter de la cadena impuestos "|"
                if (!string.IsNullOrWhiteSpace(cadenaImpuestos))
                {
                    cadenaImpuestos = cadenaImpuestos.Remove(cadenaImpuestos.Length - 1);
                }

                //Verificamos cual impuesto de los radio buttons fue seleccionado
                
                if (rb0porCiento.Checked)
                {
                    impuesto = "0%";
                }
                else if (rb16porCiento.Checked)
                {
                    impuesto = "16%";
                }
                else if (rb8porCiento.Checked)
                {
                    impuesto = "8%";
                }
                else if (rbExcento.Checked)
                {
                    impuesto = "Exento";
                }
            }

            // Miri.
            // A cuenta terceros

            if (!string.IsNullOrEmpty(txt_nombre_cterceros.Text) & !string.IsNullOrEmpty(txt_rfc_cterceros.Text))
            {
                AgregarEditarProducto.nombre_cterceros = txt_nombre_cterceros.Text;
                AgregarEditarProducto.rfc_cterceros = txt_rfc_cterceros.Text;
                AgregarEditarProducto.cp_cterceros = txt_cp_cterceros.Text;

                var regimen = cmb_bx_regimen_cterceros.SelectedValue;
                var r = cmb_bx_regimen_cterceros.SelectedItem;

                AgregarEditarProducto.regimen_cterceros = cmb_bx_regimen_cterceros.SelectedValue.ToString();
            }
            

            AgregarEditarProducto.incluye_impuestos = cmb_bx_incluye_impuestos.SelectedValue.ToString();


            AgregarEditarProducto.datosImpuestos = cadenaImpuestos;
            AgregarEditarProducto.claveProducto = txtClaveProducto.Text;
            AgregarEditarProducto.claveUnidadMedida = txtClaveUnidad.Text;
            AgregarEditarProducto.baseProducto = txtBoxBase.Text;
            AgregarEditarProducto.ivaProducto = txtIVA.Text;
            AgregarEditarProducto.impuestoProducto = impuesto;

            // Miri.
            // Agregado para que al momento de editar el producto también se guarde el impuesto que se cambio de los radio.
            
            if(txtBoxBase.Text != "")
            {
                tmp_edit_base = Convert.ToDouble(txtBoxBase.Text);
                tmp_edit_IVA = Convert.ToDouble(txtIVA.Text);
            }
            
            tmp_edit_impuesto = impuesto;
            tmp_edit_cadena_impuestos = cadenaImpuestos;
            
            // Se agrega variable para identificar si al momento de editar se guardan los cambios o se cancelan.
            // Esto es para evitar que se eliminen los impuestos si no se da clic en este botón 
            editado = true;
              
            this.Hide();
        }


        //ES LA FUNCION DEL EVENTO QUE SE EJECUTA CUANDO SE ELIGE UNA OPCION DEL COMBOBOX
        private void ProcesarComboBoxes_selectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string nombreCB = cb.Name;

            //Identificar tipo impuesto
            //string tipoImpuesto = nombreCB.Substring(0, 8);
            int rango = 0;

            // Miri.
            // Obtención del tipo de impuesto modificado debido a que falla cuando se tiene mas de
            // un digito. Ejemplo, cbLinea110_3. También se modifica el valor de la variable "rango".

            string[] nombre_cbox = nombreCB.Split('_');
            int long_nombre_cbox = nombre_cbox[0].Length;

            rango = long_nombre_cbox + 1;

           /* string tipo_cbox = nombre_cbox[0].Substring(0, 8);


            if (tipo_cbox == "cbLineaL")
            {
                rango = 10;
            }
            else
            {
                rango = 9;
            }*/
            
            string subNombreCB = nombreCB.Substring(0, rango);
            string seleccionado = cb.GetItemText(cb.SelectedItem);

            int indice  = cb.SelectedIndex;
            int subindice = Convert.ToInt32(nombreCB.Substring(rango));

            //El subindice hace referencia al numero de ComboBox que esta haciendo la operacion
            //Puede ir desde el 1 al 4
            if (subindice == 1)
            {   
                //El indice es la opcion que selecciono en el ComboBox
                subNombreCB = subNombreCB + "2";
                AccederComboBox(subNombreCB, 2, indice, seleccionado);
            }

            if (subindice == 2)
            {
                subNombreCB = subNombreCB + "3";
                AccederComboBox(subNombreCB, 3, indice, seleccionado);
            }

            if (subindice == 3)
            {
                subNombreCB = subNombreCB + "4";
                AccederComboBox(subNombreCB, 4, indice, seleccionado);
            }

            if (subindice == 4)
            {
                AccederComboBox(subNombreCB, 5, indice, seleccionado);
            }
        }

        #region Metodo para manejar cada combobox y textbox generado dinamicamente
        private void AccederComboBox(string nombre, int numeroCB, int opcion = 0, string seleccionado = "")
        {
            ComboBox cbTmp = (ComboBox)this.Controls.Find(nombre, true).FirstOrDefault();
            

            /****************************
             **** PARA EL COMBOBOX 2 ****
             ****************************/

            //Aqui se pone en la condicion el numero del combobox del que se quiere habilitar las opciones
            if (numeroCB == 2)
            {
                tipoImpuesto = seleccionado;

                // Limpia los combobox siempre para que no ponga opciones que no pertenecen a determinados impuestos 
                // Se verifica si es local o no
                string tipo = "n";
                bool activo = true;

                if (tipoImpuesto == "Loc. Retenido" | tipoImpuesto == "Loc. Traslado")
                {
                    tipo = "l";
                }
                if(opcion == 0) { activo = false; }

                LimpiarComboBox(cbTmp, tipo, activo);

                //Cuando se esta agregando un impuesto local
                if (tipoImpuesto == "Loc. Retenido" || tipoImpuesto == "Loc. Traslado")
                {
                    //La opcion es el numero del indice del item seleccionado
                    //if (opcion == 0)
                    //{
                        //LimpiarComboBox(cbTmp, "l", false);
                    //}

                    if (opcion == 1)
                    {
                        //LimpiarComboBox(cbTmp, "l", true);
                        cbTmp.Items.Add(impuestosL[0]); //...
                        cbTmp.Items.Add(impuestosL[1]); //ISH
                        cbTmp.Items.Add(impuestosL[5]); //Otro
                        cbTmp.SelectedIndex = 0;
                    }

                    if (opcion == 2)
                    {
                        //LimpiarComboBox(cbTmp, "l", true);
                        cbTmp.Items.Add(impuestosL[0]); //...
                        cbTmp.Items.Add(impuestosL[2]); //IMCD
                        cbTmp.Items.Add(impuestosL[3]); //Bienestar social
                        cbTmp.Items.Add(impuestosL[4]); //Millar
                        cbTmp.Items.Add(impuestosL[5]); // Otro
                        cbTmp.SelectedIndex = 0;
                    }
                }
                else
                {
                    //La opcion es el numero del indice del item seleccionado
                    //if (opcion == 0)
                    //{
                        //LimpiarComboBox(cbTmp, "n", false);
                    //}

                    if (opcion == 1)
                    {
                        //LimpiarComboBox(cbTmp, "n", true);
                        cbTmp.Items.Add(impuestos[0]); //...
                        cbTmp.Items.Add(impuestos[2]); //IVA
                        cbTmp.Items.Add(impuestos[3]); //IEPS
                        cbTmp.SelectedIndex = 0;
                    }

                    if (opcion == 2)
                    {
                        //LimpiarComboBox(cbTmp, "n", true);
                        cbTmp.Items.Add(impuestos[0]); //...
                        cbTmp.Items.Add(impuestos[1]); //ISR
                        cbTmp.Items.Add(impuestos[2]); //IVA
                        cbTmp.Items.Add(impuestos[3]); //IEPS
                        cbTmp.SelectedIndex = 0;
                    }
                }
            }

            /****************************
             **** PARA EL COMBOBOX 3 ****
             ****************************/

            if (numeroCB == 3)
            {
                // Limpia los combobox siempre para que no ponga opciones que no pertenecen a determinados impuestos 
                // Se verifica si es local o no
                string tipo = "n";

                if (tipoImpuesto == "Loc. Retenido" | tipoImpuesto == "Loc. Traslado")
                {
                    tipo = "l";
                }

                LimpiarComboBox(cbTmp, tipo, true);


                if (tipoImpuesto == "Loc. Retenido")
                {
                    tipoPorcentaje = seleccionado;

                    //LimpiarComboBox(cbTmp, "l", true);
                    cbTmp.Items.Add("..."); //...
                    cbTmp.SelectedIndex = 0;
                }

                if (tipoImpuesto == "Loc. Traslado")
                {
                    tipoPorcentaje = seleccionado;

                    //LimpiarComboBox(cbTmp, "l", true);
                    cbTmp.Items.Add("..."); //...
                    cbTmp.SelectedIndex = 0;
                }

                if (tipoImpuesto == "Traslado")
                {
                    tipoPorcentaje = seleccionado;

                    if (opcion == 1)
                    {
                        //LimpiarComboBox(cbTmp, "n", true);
                        cbTmp.Items.Add(factores[0]); //...
                        cbTmp.Items.Add(factores[1]); //Tasa
                        cbTmp.Items.Add(factores[3]); //Exento
                        cbTmp.SelectedIndex = 0;
                    }

                    if (opcion == 2)
                    {
                        //LimpiarComboBox(cbTmp, "n", true);
                        cbTmp.Items.Add(factores[0]); //...
                        cbTmp.Items.Add(factores[1]); //Tasa
                        cbTmp.Items.Add(factores[2]); //Cuota
                        cbTmp.SelectedIndex = 0;
                    }
                }

                if (tipoImpuesto == "Retención")
                {
                    tipoPorcentaje = seleccionado;

                    if (opcion == 1)
                    {
                        //LimpiarComboBox(cbTmp, "n", true);
                        cbTmp.Items.Add(factores[0]); //...
                        cbTmp.Items.Add(factores[1]); //Tasa
                        cbTmp.SelectedIndex = 0;
                    }

                    if (opcion == 2)
                    {
                        //LimpiarComboBox(cbTmp, "n", true);
                        cbTmp.Items.Add(factores[0]); //...
                        cbTmp.Items.Add(factores[1]); //Tasa
                        cbTmp.SelectedIndex = 0;
                    }

                    if (opcion == 3)
                    {
                        //LimpiarComboBox(cbTmp, "n", true);
                        cbTmp.Items.Add(factores[0]); //...
                        cbTmp.Items.Add(factores[1]); //Tasa
                        cbTmp.Items.Add(factores[2]); //Cuota
                        cbTmp.SelectedIndex = 0;
                    }
                }
            }

            /****************************
             **** PARA EL COMBOBOX 4 ****
             ****************************/

            if (numeroCB == 4)
            {
                // Limpia los combobox siempre para que no ponga opciones que no pertenecen a determinados impuestos 
                // Se verifica si es local o no
                string tipo = "n";

                if (tipoImpuesto == "Loc. Retenido" | tipoImpuesto == "Loc. Traslado")
                {
                    tipo = "l";
                }

                LimpiarComboBox(cbTmp, tipo, true);


                if (tipoImpuesto == "Loc. Retenido")
                {
                    if (tipoPorcentaje == "IMCD")
                    {
                        if (opcion == 0)
                        {
                            //LimpiarComboBox(cbTmp, "l", true);
                            cbTmp.Items.Add(tasaL[0]); //...
                            cbTmp.Items.Add(tasaL[1]); //1%
                            cbTmp.Items.Add(tasaL[5]); //Definir %
                            cbTmp.SelectedIndex = 0;
                        }
                    }

                    if (tipoPorcentaje == "Bienestar Social")
                    {
                        if (opcion == 0)
                        {
                            //LimpiarComboBox(cbTmp, "l", true);
                            cbTmp.Items.Add(tasaL[0]); //...
                            cbTmp.Items.Add(tasaL[1]); //1%
                            cbTmp.Items.Add(tasaL[5]); //Definir %
                            cbTmp.SelectedIndex = 0;
                        }
                    }

                    if (tipoPorcentaje == "Millar")
                    {
                        if (opcion == 0)
                        {
                            //LimpiarComboBox(cbTmp, "l", true);
                            cbTmp.Items.Add(tasaL[0]); //...
                            cbTmp.Items.Add(tasaL[2]); //2%
                            cbTmp.Items.Add(tasaL[3]); //3%
                            cbTmp.Items.Add(tasaL[4]); //5%
                            cbTmp.Items.Add(tasaL[5]); //Definir %
                            cbTmp.SelectedIndex = 0;
                        }
                    }

                    if (tipoPorcentaje == "Otro")
                    {
                        if (opcion == 0)
                        {
                            //LimpiarComboBox(cbTmp, "l", true);
                            cbTmp.Items.Add(tasaL[0]); //...
                            cbTmp.Items.Add(tasaL[5]); //Definir %
                            cbTmp.SelectedIndex = 0;
                        }
                    }
                }

                if (tipoImpuesto == "Loc. Traslado")
                {
                    if (tipoPorcentaje == "ISH")
                    {
                        if (opcion == 0)
                        {
                            //LimpiarComboBox(cbTmp, "l", true);
                            cbTmp.Items.Add(tasaL[0]); //...
                            cbTmp.Items.Add(tasaL[3]); //3%
                            cbTmp.Items.Add(tasaL[5]); //Definir %
                            cbTmp.SelectedIndex = 0;
                        }
                    }

                    if (tipoPorcentaje == "Otro")
                    {
                        //LimpiarComboBox(cbTmp, "l", true);
                        cbTmp.Items.Add(tasaL[0]); //...
                        cbTmp.Items.Add(tasaL[5]); //Definir %
                        cbTmp.SelectedIndex = 0;
                    }
                }

                if (tipoImpuesto == "Traslado")
                {
                    if (tipoPorcentaje == "IVA")
                    {
                        if (opcion == 1)
                        {
                            //LimpiarComboBox(cbTmp, "n", true);
                            cbTmp.Items.Add(tasasCuotas[0]); //...
                            cbTmp.Items.Add(tasasCuotas[1]); //0%
                            cbTmp.Items.Add(tasasCuotas[12]); //8%
                            cbTmp.Items.Add(tasasCuotas[2]); //16%
                            cbTmp.SelectedIndex = 0;
                        }

                        if (opcion == 2)
                        {
                            //LimpiarComboBox(cbTmp, "n", true);
                            cbTmp.Items.Add(tasasCuotas[0]); //...
                            cbTmp.SelectedIndex = 0;
                        }
                    }

                    if (tipoPorcentaje == "IEPS")
                    {
                        if (opcion == 1)
                        {
                            //LimpiarComboBox(cbTmp, "n", true);
                            cbTmp.Items.Add(tasasCuotas[0]); //...
                            cbTmp.Items.Add(tasasCuotas[1]); //0%
                            cbTmp.Items.Add(tasasCuotas[4]); //26.5%
                            cbTmp.Items.Add(tasasCuotas[5]); //30%
                            cbTmp.Items.Add(tasasCuotas[6]); //53%
                            cbTmp.Items.Add(tasasCuotas[7]); //50%
                            cbTmp.Items.Add(tasasCuotas[8]); //1.600000%
                            cbTmp.Items.Add(tasasCuotas[9]); //30.4%
                            cbTmp.Items.Add(tasasCuotas[10]); //25%
                            cbTmp.Items.Add(tasasCuotas[11]); //9%
                            cbTmp.Items.Add(tasasCuotas[12]); //8%
                            cbTmp.Items.Add(tasasCuotas[13]); //7%
                            cbTmp.Items.Add(tasasCuotas[14]); //6%
                            cbTmp.Items.Add(tasasCuotas[15]); //3%
                            cbTmp.SelectedIndex = 0;
                        }

                        if (opcion == 2)
                        {
                            //LimpiarComboBox(cbTmp, "n", true);
                            cbTmp.Items.Add(tasasCuotas[0]); //...
                            cbTmp.Items.Add(tasasCuotas[3]); //Definir %
                            cbTmp.SelectedIndex = 0;
                        }
                    }
                }

                if (tipoImpuesto == "Retención")
                {
                    if (tipoPorcentaje == "ISR")
                    {
                        if (opcion == 1)
                        {
                            //LimpiarComboBox(cbTmp, "n", true);
                            cbTmp.Items.Add(tasasCuotas[0]); //...
                            cbTmp.Items.Add(tasasCuotas[3]); //Definir %
                            cbTmp.SelectedIndex = 0;
                        }
                    }

                    if (tipoPorcentaje == "IVA")
                    {
                        if (opcion == 1)
                        {
                            //LimpiarComboBox(cbTmp, "n", true);
                            cbTmp.Items.Add(tasasCuotas[0]); //...
                            cbTmp.Items.Add(tasasCuotas[3]); //Definir %
                            cbTmp.SelectedIndex = 0;
                        }
                    }

                    if (tipoPorcentaje == "IEPS")
                    {
                        if (opcion == 1)
                        {
                            //LimpiarComboBox(cbTmp, "n", true);
                            cbTmp.Items.Add(tasasCuotas[0]); //...
                            cbTmp.Items.Add(tasasCuotas[4]); //26.5%
                            cbTmp.Items.Add(tasasCuotas[5]); //30%
                            cbTmp.Items.Add(tasasCuotas[6]); //53%
                            cbTmp.Items.Add(tasasCuotas[7]); //50%
                            cbTmp.Items.Add(tasasCuotas[8]); //1.600000%
                            cbTmp.Items.Add(tasasCuotas[9]); //30.4%
                            cbTmp.Items.Add(tasasCuotas[10]); //25%
                            cbTmp.Items.Add(tasasCuotas[11]); //9%
                            cbTmp.Items.Add(tasasCuotas[12]); //8%
                            cbTmp.Items.Add(tasasCuotas[13]); //7%
                            cbTmp.Items.Add(tasasCuotas[14]); //6%
                            cbTmp.SelectedIndex = 0;
                        }

                        if (opcion == 2)
                        {
                            //LimpiarComboBox(cbTmp, "n", true);
                            cbTmp.Items.Add(tasasCuotas[0]); //...
                            cbTmp.Items.Add(tasasCuotas[3]); //Definir %
                            cbTmp.SelectedIndex = 0;
                        }
                    }
                }
            }


            /******************************************************
             **** PARA LAS OPCIONES INDIVIUALES DEL COMBOBOX 4 ****
             ******************************************************/
            if (numeroCB == 5)
            {
                porcentajeSeleccionado = seleccionado;

                nombre = nombre.Replace("cbLinea", "tbLinea");

                if (porcentajeSeleccionado == "Definir %")
                {
                    nombre += "1";

                    TextBox tbTmp = (TextBox)this.Controls.Find(nombre, true).FirstOrDefault();
                    tbTmp.Enabled = true;
                    tbTmp.Focus();
                }
                else
                {
                    nombre += "2";

                    string[] cantidadTmp = porcentajeSeleccionado.Split(' ');

                    if (cantidadTmp[0] == "...")
                    {
                        cantidadTmp[0] = "0";
                    }

                    //float porcentaje = CantidadPorcentaje(cantidadTmp[0]);
                    double porcentaje = convertir_porcentaje(Convert.ToDouble(cantidadTmp[0]), tipoPorcentaje);
                    double importe = 0;

                    if (txtBoxBase.Text != "")
                    {
                        double precioProductoTmp = Convert.ToDouble(txtBoxBase.Text);
                        importe = precioProductoTmp * porcentaje;

                        TextBox tbTmp = (TextBox)this.Controls.Find(nombre, true).FirstOrDefault();
                        tbTmp.Text = importe.ToString("0.00");
                    }
                    

                    //Para sumar o restar la cantidad del impuesto al total final
                    if (tipoImpuesto == "Traslado" || tipoImpuesto == "Loc. Traslado")
                    {
                        var cantidad = float.Parse(txtTotal.Text) + importe;
                        txtTotal.Text = cantidad.ToString("0.00");
                    }

                    if (tipoImpuesto == "Retención" || tipoImpuesto == "Loc. Retenido")
                    {
                        var cantidad = float.Parse(txtTotal.Text) - importe;
                        txtTotal.Text = cantidad.ToString("0.00");
                    }
                }
            }

            RecalcularTotal();
        }
        #endregion

        private void LimpiarComboBox(ComboBox cb, string es_nl, bool habilitado = true)
        {
            // Miri.
            // Se modifica todo.

            string nombre_cmb= cb.Name;
            int tam = nombre_cmb.Length;
            var columna = nombre_cmb.Split('_');
            int tam_col0 = columna[0].Length;
            int tam_pfila = 0;
            int col = 0;
            string fila = "";
            string esLoc = "";


            // Se obtiene el número de fila y columna 

            if (es_nl == "n")
            {
                tam_pfila = tam_col0 - 7;
                fila = columna[0].Substring(7, tam_pfila);
                col = Convert.ToInt32(columna[1]);

                if (Convert.ToInt32(columna[1]) > 1)
                {
                    col = Convert.ToInt32(columna[1]) - 1;
                }                
            }
            if (es_nl == "l")
            {
                tam_pfila = tam_col0 - 8;
                fila = columna[0].Substring(8, tam_pfila);
                esLoc = "L";
                col = Convert.ToInt32(columna[1]);

                if (Convert.ToInt32(columna[1]) > 1)
                {
                    col = Convert.ToInt32(columna[1]) - 1;
                }
            }

            // Inicia limpieza de campos cada vez que se cambia la opción de alguno de los combobox

            string nombre_cmb_sel_actual = "cbLinea" + esLoc + fila + "_";
            string nombre_txt = "tbLinea" + esLoc + fila + "_";
            //ComboBox cmb_f_c = (ComboBox)this.Controls.Find(nombre_cmb_sel_actual + col, true).FirstOrDefault();
            
            int ncol = 4;
            int ini = col + 1;

            for (int c = ini; c <= ncol; c++)
            {
                ComboBox cmb_f_c_sig = (ComboBox)this.Controls.Find(nombre_cmb_sel_actual + c, true).FirstOrDefault();

                cmb_f_c_sig.DataSource = null;
                cmb_f_c_sig.Items.Clear();
                cmb_f_c_sig.Enabled = false;
            }
            
            TextBox txt_f_c1 = (TextBox)this.Controls.Find(nombre_txt + "1", true).FirstOrDefault();
            TextBox txt_f_c2 = (TextBox)this.Controls.Find(nombre_txt + "2", true).FirstOrDefault();
            txt_f_c1.Text = string.Empty;
            txt_f_c2.Text = string.Empty;

            
            cb.Enabled = habilitado;
        }

        /******************************************************************
         **** FUNCION PARA CONVERTIR LOS PORCENTAJES AL VALOR CORRECTO ****
         ******************************************************************/

        /*private float CantidadPorcentaje(string sCantidad)
        {
            int longitud = sCantidad.Length;

            float resultado = 0;

            //Si la cantidad por defecto es una cifra de dos digitos o mas
            if (longitud > 1)
            {
                //Si contiene punto la convertimos en array
                if (sCantidad.Contains('.'))
                {
                    string[] valorTmp = sCantidad.Split('.');

                    //Si es la cantidad de 1.600000 entrara aqui
                    if (valorTmp[0] == "1")
                    {
                        resultado = float.Parse(sCantidad);

                    }
                    else
                    {
                        sCantidad = sCantidad.Replace(".", "");
                        sCantidad = "0." + sCantidad;

                        resultado = float.Parse(sCantidad);
                    }
                }
                else
                {
                    sCantidad = "0." + sCantidad;
                    resultado = float.Parse(sCantidad);
                }
            }
            else
            {
                sCantidad = "0.0" + sCantidad;
                resultado = float.Parse(sCantidad);
            }
            
            return resultado;
        }
*/         

        /********************************************************
         **** FUNCION PARA CALCULAR LOS PORCENTAJES MANUALES ****
         ********************************************************/

        private void PorcentajeManual_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb == null)
            {
                tb = Controls.Find(sender.ToString(), true).FirstOrDefault() as TextBox;
            }

            var nombre = tb.Name.Remove(tb.Name.Length - 1);
                    
            var cantidad = tb.Text;

            int numero_fila_actual = 0;
            string txt_timpuest= nombre.Substring(0, 7);
            string txt_timpuestLoc = nombre.Substring(0, 8);

            if (txt_timpuestLoc == "tbLineaL")
            {
                numero_fila_actual = Convert.ToInt32(nombre.Substring(8, 1));
            }
            else
            {
                if (txt_timpuest == "tbLinea")
                {
                    numero_fila_actual = Convert.ToInt32(nombre.Substring(7, 1));
                }
            }
            
            

            if (cantidad.Equals("."))
            {
                tb.Text = string.Empty;
                return;
            }

            if (string.IsNullOrWhiteSpace(cantidad))
            {
                cantidad = "0";
            }
            

            // Obtiene el porcentaje máximo y minimo cuando se definido. 
            // Dependera del tipo de impuesto.   
            double lim_porc_minimo = 0;
            double lim_porc_maximo = 0;
            string cmb_impuesto_actual = "";
            string cmb_tfactor_actual = "";
            string cmb_es_actual = "";
            string txt_lim_maxmin = "";

            // Fila fija 
            /***if(cbLinea1_16.Text != "..." & numero_fila_actual == 1)
            {
                cmb_es_actual = cbLinea1_16.GetItemText(cbLinea1_16.SelectedItem);
                cmb_impuesto_actual = cbLinea1_26.GetItemText(cbLinea1_26.SelectedItem);
                cmb_tfactor_actual = cbLinea1_36.GetItemText(cbLinea1_36.SelectedItem);
            }*/
            // Filas dinamicas
            foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
            {
                foreach (Control item in panel.Controls.OfType<Control>())
                {
                    if (item.Name.Contains("cbLinea") | item.Name.Contains("cbLineaL"))
                    {
                        int fila = 0;
                        var col = item.Name.Split('_');
                        string d_txt_timpuest = item.Name.Substring(0, 7);
                        string d_txt_timpuestLoc = item.Name.Substring(0, 8);

                        if (d_txt_timpuestLoc == "cbLineaL")
                        {
                            fila = Convert.ToInt32(item.Name.Substring(8, 1));
                        }
                        else
                        {
                            if (d_txt_timpuest == "cbLinea")
                            {
                                fila = Convert.ToInt32(item.Name.Substring(7, 1));
                            }
                        }

                        if (fila.Equals(numero_fila_actual))
                        {
                            if (col[1].Equals("1"))
                            {
                                cmb_es_actual = item.Text;
                            }
                            if (col[1].Equals("2"))
                            {
                                cmb_impuesto_actual = item.Text;
                            }
                            if (col[1].Equals("3"))
                            {
                                cmb_tfactor_actual = item.Text;
                            }
                        }

                        if (item.Name.Contains("tbLinea") | item.Name.Contains("tbLineaL"))
                        {   
                            
                        }
                    }
                }
            }


            if (cmb_impuesto_actual == "IVA" & cmb_tfactor_actual == "Tasa" & cmb_es_actual == "Retención"){
                lim_porc_minimo = 0;
                lim_porc_maximo = 16;
                txt_lim_maxmin = "0% y 16%.";
            }
            if (cmb_impuesto_actual == "ISR" & cmb_tfactor_actual == "Tasa"){
                lim_porc_minimo = 0;
                lim_porc_maximo = 35;
                txt_lim_maxmin = "0% y 35%.";
            }
            if (cmb_impuesto_actual == "IEPS" & cmb_tfactor_actual == "Cuota"){
                lim_porc_minimo = 0;
                lim_porc_maximo = 50.320;
                txt_lim_maxmin = "0% y 50.320000%.";
            }
            if (cmb_es_actual == "Loc. Traslado" | cmb_es_actual == "Loc. Retenido" & cmb_impuesto_actual != "otro"){
                lim_porc_minimo = 0;
                lim_porc_maximo = 16;
                txt_lim_maxmin = "0% y 16%.";
            }
            if (cmb_impuesto_actual == "otro" & (cmb_es_actual == "Loc. Traslado" | cmb_es_actual == "Loc. Retenido")){
                lim_porc_minimo = 0;
                lim_porc_maximo = 100;
                txt_lim_maxmin = "0% y 100%.";
            }

            //float porcentaje = CantidadPorcentaje(cantidad);
            double porcentaje = convertir_porcentaje(Convert.ToDouble(cantidad), cmb_impuesto_actual);

            if (porcentaje < lim_porc_minimo || porcentaje > lim_porc_maximo)
            {
                MessageBox.Show("El porcentaje debe ser entre " + txt_lim_maxmin, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double precioProductoTmp = Convert.ToDouble(txtBoxBase.Text);
            double importe = precioProductoTmp * porcentaje;
            //double importe = precioProducto * porcentaje;

            TextBox tbImporte = (TextBox)this.Controls.Find(nombre + "2", true).FirstOrDefault();
            tbImporte.Text = importe.ToString("0.00");

            RecalcularCambioPorcentaje();
            RecalcularTotal();
        }

        private void rb8porCiento_CheckedChanged(object sender, EventArgs e)
        {
            checarRadioButtons();
        }

        private void rb16porCiento_CheckedChanged(object sender, EventArgs e)
        {
            checarRadioButtons();
        }

        private void rb0porCiento_CheckedChanged(object sender, EventArgs e)
        {
            checarRadioButtons();
        }

        private void rbExcento_CheckedChanged(object sender, EventArgs e)
        {
            checarRadioButtons();
        }

        private void AgregarDetalleFacturacionProducto_FormClosing(object sender, FormClosingEventArgs e)
        {
            limpiarCampos();
        }

        private void btnKeyWordSearch_Click(object sender, EventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al intentar abrir el enlace: " + ex, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void VisitLink()
        {
            System.Diagnostics.Process.Start("https://sifo.com.mx/buscador_de_claves_de_productos_y_servicios_de_el_sat_cfdi_33_para_facturar.php");
        }

        private string ValidarCampos(string campo, int tipo = 0)
        {
            if (campo == "" || campo == "...")
            {
                campo = " - ";
            }

            return campo;
        }


        private void AgregarDetalleFacturacionProducto_Paint(object sender, PaintEventArgs e)
        {
            if (ejecutarMetodos)
            {
                precioProducto = Convert.ToDouble(AgregarEditarProducto.precioProducto);


                // Miri.
                // No incluye calcular el IVA de los radios porque hasta este punto no se sabe que 
                // impuesto tiene agregado el producto que se esta registrando desde el XML.

                //if (AgregarEditarProducto.DatosSourceFinal != 3)

                // Se modifica la anterior condicional para nueva versión de CFDI 4.0
                // esto para que no afecte al nuevo campo de "Incluye impuestos".
                // Para registros anteriores se debe primero validar que se tengan o no impuestos agregados.

                if (AgregarEditarProducto.DatosSourceFinal == 1)
                {
                    //checarRadioButtons(); 
                    des_habilita_elementos_impuestos(0);

                    if (editado == true)
                    {
                        editado = false; 

                        if (tmp_edit_impuesto != "")
                        {
                            des_habilita_elementos_impuestos(1);

                            if (tmp_edit_impuesto == "0%")
                            {
                                rb0porCiento.Checked = true;
                            }
                            if (tmp_edit_impuesto == "16%")
                            {
                                rb16porCiento.Checked = true;
                            }
                            if (tmp_edit_impuesto == "8%")
                            {
                                rb8porCiento.Checked = true;
                            }
                            if (tmp_edit_impuesto == "Exento")
                            {
                                rbExcento.Checked = true;
                            }

                            // Verifica si tiene mas impuestos

                            if (tmp_edit_cadena_impuestos != "")
                            {
                                string[] lista_impuestos_extra = tmp_edit_cadena_impuestos.Split('|');
                                int longitud = lista_impuestos_extra.Length;
                                int id_tmp = 0;
                                string nombre_cb = "";
                                string nombre_txt = "";
                                string loc = "";


                                for (int i = 0; i < longitud; i++)
                                {
                                    string[] dato = lista_impuestos_extra[i].Split(',');
                                    
                                    if (dato[0] == "Traslado" | dato[0] == "Retención")
                                    {
                                        GenerarCampos(1);
                                    }
                                    if (dato[0] == "Loc. Traslado" | dato[0] == "Loc. Retenido")
                                    {
                                        GenerarCampos(0);
                                        loc = "L";
                                    }

                                    id_tmp = id - 1;
                                    nombre_cb = "cbLinea" + loc + id_tmp + "_";
                                    nombre_txt = "tbLinea" + loc + id_tmp + "_";

                                    

                                    
                                    // Combobox: Es...
                                    
                                    ComboBox cb1 = (ComboBox)this.Controls.Find(nombre_cb + 1, true).FirstOrDefault();
                                    cb1.SelectedItem = dato[0];
                                    int index1 = cb1.SelectedIndex;

                                    AccederComboBox(nombre_cb + 2, 2, index1, dato[0]);

                                    // Combobox: impuesto 

                                    ComboBox cb2 = (ComboBox)this.Controls.Find(nombre_cb + 2, true).FirstOrDefault();
                                    cb2.SelectedItem = dato[1];
                                    int index2 = cb2.SelectedIndex;

                                    AccederComboBox(nombre_cb + 3, 3, index2, dato[1]);

                                    // Combobox: tipo factor

                                    ComboBox cb3 = (ComboBox)this.Controls.Find(nombre_cb + 3, true).FirstOrDefault();
                                    cb3.SelectedItem = dato[2];
                                    int index3 = cb3.SelectedIndex;

                                    AccederComboBox(nombre_cb + 4, 4, index3, dato[2]);

                                    // Combobox: tasa/cuota

                                    ComboBox cb4 = (ComboBox)this.Controls.Find(nombre_cb + 4, true).FirstOrDefault();
                                    cb4.SelectedItem = dato[3] + " %";
                                    int index4 = cb4.SelectedIndex;

                                    AccederComboBox(nombre_cb, 5, index4, dato[3] + " %");


                                    // Textbox: Definir impuesto

                                    if (dato[3] == "Definir %")
                                    {
                                        TextBox tb1 = (TextBox)this.Controls.Find(nombre_txt + 1, true).FirstOrDefault();
                                        tb1.Text = dato[4];


                                        // Textbox: Importe

                                        double importe = 0;

                                        if (dato[2] == "Tasa")
                                        {
                                            importe = Convert.ToDouble(dato[4]) * Convert.ToDouble(txtBoxBase.Text);

                                            if (Convert.ToDecimal(dato[4]) > 1)
                                            {
                                                importe = importe / 100;
                                            }
                                        }
                                        if (dato[2] == "Cuota")
                                        {
                                            importe = Convert.ToDouble(dato[4]) * 1;
                                        }

                                        TextBox tb2 = (TextBox)this.Controls.Find(nombre_txt + 2, true).FirstOrDefault();
                                        tb2.Text = importe.ToString("0.00");
                                    }

                                    //RecalcularTotal();

                                }
                            }
                        }

                        if (tmp_edit_impuesto == "")
                        {

                        }
                    }
                }
                
                //RecalcularTotal();

                cargarDatos();
                if (typeOriginDataFinal == 2)
                {
                    txtClaveProducto.Text = ClaveMedidaFinal;
                    txtClaveUnidad.Text = UnidadMedidaFinal;
                    CargarClaveUnidad();
                }

                // Miri.
                // Carga los impuestos incluidos en el producto que se esta registrando desde un XML.  
                if (AgregarEditarProducto.DatosSourceFinal == 3)
                {
                    cargar_impuestos_dexml();
                }

                ejecutarMetodos = false;
            }
        }

        private void SoloDecimales(object sender, KeyPressEventArgs e)
        {
            //permite 0-9, eliminar y decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            //verifica que solo un decimal este permitido
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                {
                    e.Handled = true;
                }
            }
        }


        #region Metodo para recalcular el total de todos los impuestos
        private void RecalcularTotal()
        {
            float totalFinal = 0;
            double total_ret = 0;
            double total_tra = 0;

            // Miri.
            // Modificado. 
            foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
            {
                int tipo = 0;
                float importe = 0;

                foreach (Control item in panel.Controls.OfType<Control>())
                {
                    if (item.Text == "Traslado" || item.Text == "Loc. Traslado")
                    {
                        tipo = 1;
                    }

                    if (item.Text == "Retención" || item.Text == "Loc. Retenido")
                    {
                        tipo = 2;
                    }

                    if (item.Name.Contains("tbLinea"))
                    {
                        var tb = item.Name.Split('_');

                        if (tb[1] == "2")
                        {
                            if (!string.IsNullOrWhiteSpace(item.Text))
                            {
                                importe = float.Parse(item.Text);
                            }
                        }
                    }
                }

                if (tipo == 1)
                {
                    total_tra += importe; 
                }
                else
                {
                    total_ret += importe;
                }
            }

            /***if (cbLinea1_16.Text == "Traslado" || cbLinea1_16.Text == "Loc. Traslado")
            {
                //totalFinal += float.Parse(tbLinea1_2.Text);
                if(tbLinea1_26.Text != "")
                {
                    total_tra += Convert.ToDouble(tbLinea1_26.Text);
                }
            }
            else if (cbLinea1_16.Text == "Retención" || cbLinea1_16.Text == "Loc. Retenido")
            {
                ///totalFinal -= float.Parse(tbLinea1_2.Text);
                ///
                if (tbLinea1_26.Text != "")
                {
                    total_ret += Convert.ToDouble(tbLinea1_26.Text);
                }
            }*/

            double total_nuevo = 0;

            if (txtIVA.Text != "" & txtBoxBase.Text != "")
            {
                total_nuevo = Convert.ToDouble(txtIVA.Text) + Convert.ToDouble(txtBoxBase.Text);
            }

            //double total_nuevo = Convert.ToDouble(txtIVA.Text) + Convert.ToDouble(txtBoxBase.Text);
            total_nuevo = (total_nuevo + total_tra) - total_ret;
            txtTotal.Text = total_nuevo.ToString("0.00");
        }
        #endregion

        #region Metodo para recalcular los impuestos al cambiar de porcentaje de IVA
        private void RecalcularCambioPorcentaje()
        {
            float cantidadBase = float.Parse(txtBoxBase.Text);


            //Dinamicos
            foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
            {
                var manual = false; //Si el porcentaje es por definir cambiar a true
                var auxiliar = string.Empty; //Auxiliar para almacenar el porcentaje del impuesto
                string cmb_col_2 = "";
                string cmb_col_3 = "";


                foreach (Control item in panel.Controls.OfType<Control>())
                {
                    //Combobox
                    if (item.Name.Contains("cbLinea"))
                    {
                        var tmp = item.Name.Split('_');

                        if(tmp[1] == "2")
                        {
                            cmb_col_2 = item.Text;
                        }
                        if (tmp[1] == "3")
                        {
                            cmb_col_3 = item.Text;
                        }

                        if (tmp[1] == "4")
                        {
                            if (item.Text == "Definir %")
                            {
                                manual = true;
                            }
                            else
                            {
                                auxiliar = item.Text;
                            }
                        }
                    }

                    //Textbox
                    if (item.Name.Contains("tbLinea"))
                    {
                        var tmp = item.Name.Split('_');

                        if (tmp[1] == "1")
                        {
                            if (manual)
                            {
                                auxiliar = item.Text;
                            }
                        }

                        if (tmp[1] == "2")
                        {
                            var porcentajeTmp = auxiliar.Split(' ');

                            // Miri.
                            // Si el tipo de impuesto es un IEPS y el tipo factor es Cuota,
                            // entonces el calculo para el importe es diferente, se calcula con la cantidad de unidades y no a la base.

                            if (cmb_col_2 == "IEPS" & cmb_col_3== "Cuota")
                            {
                                double importe = 0;

                                if (porcentajeTmp[0] != "" & porcentajeTmp[0] != "...")
                                {
                                    importe = 1 * Convert.ToDouble(porcentajeTmp[0]);
                                }

                                item.Text = importe.ToString("0.00");
                            }
                            else
                            {
                                //var porcentaje = CantidadPorcentaje(porcentajeTmp[0]);

                                double porcentaje = 0;

                                // Miri.
                                // Se agrega 2da condicional porque marca error cuando el valor de es: porcentajeTmp[0] == "..."
                                if (porcentajeTmp[0] != "")
                                {
                                    if (porcentajeTmp[0] == "...")
                                    {
                                        porcentajeTmp[0] = "0";
                                    }
                                    
                                    porcentaje = convertir_porcentaje(Convert.ToDouble(porcentajeTmp[0]), cmb_col_2);
                                                                            
                                }

                                var importe = cantidadBase * porcentaje;

                                item.Text = importe.ToString("0.00");
                            }
                            
                            cmb_col_2 = "";
                            cmb_col_3 = "";
                        }
                    }
                }
            }
        }
        #endregion

        private double convertir_porcentaje(double cant, string tipo_imp = "")
        {
            double r = cant;

            if (cant >= 1)
            {
                if(tipo_imp == "Millar")
                {
                    r = cant / 1000;
                }
                else
                {
                    r = cant / 100;
                }
            }

            return r;
        }

        private void cargar_impuestos()
        {
            // Busca si tiene mas de un impuesto
            int cant_impuestos = Convert.ToInt32(cn.EjecutarSelect($"SELECT count(ID) AS ID FROM detallesfacturacionproductos WHERE IDProducto", 1));


            if (cant_impuestos > 0)
            {
                int i = 1;

                DataTable d_impuestos_ext = cn.CargarDatos(cs.cargar_impuestos_en_editar_producto(id_producto_edit));

                if (d_impuestos_ext.Rows.Count > 0)
                {
                    foreach (DataRow r_impuestos_ext in d_impuestos_ext.Rows)
                    {
                        string nombre_cb = "";
                        string nombre_tb = "";

                        if (r_impuestos_ext["Tipo"].ToString() == "Traslado" | r_impuestos_ext["Tipo"].ToString() == "Retención")
                        {
                            GenerarCampos(1);
                            nombre_cb = "cbLinea" + i + "_";
                            nombre_tb = "tbLinea" + i + "_";
                        }
                        if (r_impuestos_ext["Tipo"].ToString() == "Loc. Traslado" | r_impuestos_ext["Tipo"].ToString() == "Loc. Retenido")
                        {
                            GenerarCampos(2);
                            nombre_cb = "cbLineaL" + i + "_";
                            nombre_tb = "tbLineaL" + i + "_";
                        }


                        // Combobox: Es...

                        ComboBox cb1 = (ComboBox)this.Controls.Find(nombre_cb + 1, true).FirstOrDefault();
                        cb1.SelectedItem = r_impuestos_ext["Tipo"].ToString();
                        int index1 = cb1.SelectedIndex;

                        AccederComboBox(nombre_cb + 2, 2, index1, r_impuestos_ext["Tipo"].ToString());

                        // Combobox: impuesto 

                        ComboBox cb2 = (ComboBox)this.Controls.Find(nombre_cb + 2, true).FirstOrDefault();
                        cb2.SelectedItem = r_impuestos_ext["Impuesto"].ToString();
                        int index2 = cb2.SelectedIndex;

                        AccederComboBox(nombre_cb + 3, 3, index2, r_impuestos_ext["Impuesto"].ToString());

                        // Combobox: tipo factor 

                        ComboBox cb3 = (ComboBox)this.Controls.Find(nombre_cb + 3, true).FirstOrDefault();
                        cb3.SelectedItem = r_impuestos_ext["TipoFactor"].ToString();
                        int index3 = cb3.SelectedIndex;

                        AccederComboBox(nombre_cb + 4, 4, index3, r_impuestos_ext["TipoFactor"].ToString());
                        
                        // Combobox: tasa/cuota

                        ComboBox cb4 = (ComboBox)this.Controls.Find(nombre_cb + 4, true).FirstOrDefault();

                        string tasaCuotaAux = "...";

                        if (r_impuestos_ext["tasacuota"].ToString().Equals("0.00")) { tasaCuotaAux = "0 %"; }
                        if (r_impuestos_ext["tasacuota"].ToString().Equals("26.50")) { tasaCuotaAux = "26.5 %"; }
                        if (r_impuestos_ext["tasacuota"].ToString().Equals("30.00")) { tasaCuotaAux = "30 %"; }
                        if (r_impuestos_ext["tasacuota"].ToString().Equals("53.00")) { tasaCuotaAux = "53 %"; }
                        if (r_impuestos_ext["tasacuota"].ToString().Equals("50.00")) { tasaCuotaAux = "50 %"; }
                        if (r_impuestos_ext["tasacuota"].ToString().Equals("1.60")) { tasaCuotaAux = "1.600000"; }
                        if (r_impuestos_ext["tasacuota"].ToString().Equals("30.40")) { tasaCuotaAux = "30.4 %"; }
                        if (r_impuestos_ext["tasacuota"].ToString().Equals("25.00")) { tasaCuotaAux = "25 %"; }
                        if (r_impuestos_ext["tasacuota"].ToString().Equals("9.00")) { tasaCuotaAux = "9 %"; }
                        if (r_impuestos_ext["tasacuota"].ToString().Equals("8.00")) { tasaCuotaAux = "8 %"; }
                        if (r_impuestos_ext["tasacuota"].ToString().Equals("7.00")) { tasaCuotaAux = "7 %"; }
                        if (r_impuestos_ext["tasacuota"].ToString().Equals("6.00")) { tasaCuotaAux = "6 %"; }
                        if (r_impuestos_ext["tasacuota"].ToString().Equals("3.00")) { tasaCuotaAux = "3 %"; }
                        if (r_impuestos_ext["tasacuota"].ToString().Equals("0.00") && r_impuestos_ext["Definir"].ToString() != "0.00") { tasaCuotaAux = "Definir %"; }

                        cb4.SelectedItem = tasaCuotaAux;
                        int index4 = cb4.SelectedIndex;

                        AccederComboBox(nombre_cb, 5, index4, r_impuestos_ext["tasacuota"].ToString()); //+" %"


                        // Textbox: Definir impuesto
                        // Antes comparaba con Definir %
                        if (r_impuestos_ext["tasacuota"].ToString() == "0.00" && r_impuestos_ext["Definir"].ToString() != "0.00")
                        {
                            TextBox tb1 = (TextBox)this.Controls.Find(nombre_tb + 1, true).FirstOrDefault();
                            tb1.Text = r_impuestos_ext["Definir"].ToString();
                        }

                        // Textbox: Importe

                        TextBox tb2 = (TextBox)this.Controls.Find(nombre_tb + 2, true).FirstOrDefault();
                        tb2.Text = r_impuestos_ext["Importe"].ToString();

                        PorcentajeManual_KeyUp(nombre_tb + 2, new KeyEventArgs(Keys.Up));

                        i++;
                    }
                }
            }
        }

        private void AgregarDetalleFacturacionProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }

        private void txtClaveProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 22)
            //{
            //    e.Handled = true;
            //}

            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            //{
            //    e.Handled = true;
            //}

            //// solo 1 punto decimal
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        private void txtIVA_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 22)
            //{
            //    e.Handled = true;
            //}

            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            //{
            //    e.Handled = true;
            //}

            //// solo 1 punto decimal
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == 22)
            //{
            //    e.Handled = true;
            //}

            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            //{
            //    e.Handled = true;
            //}

            //// solo 1 punto decimal
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        private void txtClaveProducto_TextChanged(object sender, EventArgs e)
        {
            ValidarEntradaDeTexto(sender, e);
        }

        private void ValidarEntradaDeTexto(object sender, EventArgs e)
        {
            var resultado = string.Empty;
            var txtValidarTexto = (TextBox)sender;
            resultado = txtValidarTexto.Text;

            if (!string.IsNullOrWhiteSpace(resultado))
            {
                var resultadoAuxialiar = Regex.Replace(resultado, @"[^0-9.]", "").Trim();
                resultado = resultadoAuxialiar;
                txtValidarTexto.Text = resultado;
                //txtValidarTexto.Focus();
                txtValidarTexto.Select(txtValidarTexto.Text.Length, 0);
            }
            else
            {
                //txtValidarTexto.Focus();
                txtValidarTexto.Select(txtValidarTexto.Text.Length, 0);
            }
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            ValidarEntradaDeTexto(sender, e);
        }

        private void txtIVA_TextChanged(object sender, EventArgs e)
        {
            ValidarEntradaDeTexto(sender, e);
        }

       
        private void cargar_impuestos_dexml()
        {
            // Miri.
            // Todo modificado

            //int t = 1;
            int e = 1;
            string incluye_impuesto = AgregarStockXML.incluye_impuestos_delxml;

            // Inicia limpiando el área de impuestos.
            int cant_filas = panelContenedor.Controls.OfType<FlowLayoutPanel>().Count();

            if (cant_filas > 0)
            {
                while (cant_filas >= e)
                {
                    string nombre_panel = "panelGeneradoR" + e;

                    foreach (Control panel in panelContenedor.Controls.OfType<FlowLayoutPanel>())
                    {
                        if (nombre_panel == panel.Name)
                        {
                            panelContenedor.Controls.Remove(panel);
                        }
                    }

                    e++;
                }

                RecalcularTotal();
            }

            // Deshabilita todo lo de impuestos
            des_habilita_elementos_impuestos(0);

            // Solo aplica para el impuesto de los radio. 

            if (AgregarStockXML.tipo_impuesto_delxml != "" | incluye_impuesto == "02")
            {

                // Habilita todo lo de impuestos 
                des_habilita_elementos_impuestos(1);

                cmb_bx_incluye_impuestos.SelectedValue = incluye_impuesto;


                if (AgregarStockXML.tipo_impuesto_delxml == "0")
                {
                    rb0porCiento.Checked = true;
                }
                if (AgregarStockXML.tipo_impuesto_delxml == "8")
                {
                    rb8porCiento.Checked = true;
                }
                if (AgregarStockXML.tipo_impuesto_delxml == "16")
                {
                    rb16porCiento.Checked = true;
                }
                if (AgregarStockXML.tipo_impuesto_delxml == "Exento")
                {
                    rbExcento.Checked = true;
                }

                checarRadioButtons();
            }
            /*else
            {
                rb0porCiento.Checked = false;
                rb8porCiento.Checked = false;
                rb16porCiento.Checked = false;
                rbExcento.Checked = false;
            }*/

            
            // Trasladados y retenidos

            if (AgregarStockXML.list_impuestos_traslado_retenido.Count > 0)
            {
                foreach (var list_tras in AgregarStockXML.list_impuestos_traslado_retenido)
                {

                    GenerarCampos(1);


                    var id_tmp = id - 1;

                    string nombre_cb = "cbLinea" + id_tmp + "_";
                    string nombre_tb = "tbLinea" + id_tmp + "_";
                    string tra_ret = "";
                    string t_impuesto = "";
                    string tasa_cuota = "";
                    string tasa_cuota_porc = "";

                    var dato = list_tras.Split('-');

                  

                    // Traslado - retención
                    if (dato[0] == "t") { tra_ret = "Traslado"; }
                    if (dato[0] == "r") { tra_ret = "Retención"; }

                    // Tipo impuesto
                    if (dato[1] == "001") { t_impuesto = "ISR"; }
                    if (dato[1] == "002") { t_impuesto = "IVA"; }
                    if (dato[1] == "003") { t_impuesto = "IEPS"; }

                    // Tasa/cuota
                    tasa_cuota = dato[3];

                    if (dato[2] == "Tasa")
                    {
                        if (Convert.ToDecimal(dato[3]) < 1)
                        {
                            tasa_cuota = Convert.ToString(Convert.ToDecimal(dato[3]) * 100);

                            var indice_pdec = tasa_cuota.IndexOf('.');

                            if (indice_pdec > 0)
                            {
                                // Número decimal
                                int hasta = tasa_cuota.Length - (indice_pdec + 1);
                                string decim = tasa_cuota.Substring((indice_pdec + 1), hasta);

                                if (Convert.ToDecimal(decim) == 0)
                                {
                                    var num_entero = tasa_cuota.Split('.');
                                    tasa_cuota = num_entero[0]; 
                                }
                            }
                        }
                    }

                    var exi = tasasCuotas.IndexOf(tasa_cuota + " %");
                    if (exi < 0)
                    {
                        tasa_cuota_porc = tasa_cuota;
                        tasa_cuota = "Definir %";
                    }
                    else
                    {
                        tasa_cuota += " %";
                    }





                    // Combobox: Es...

                    ComboBox cb1 = (ComboBox)this.Controls.Find(nombre_cb + 1, true).FirstOrDefault();
                    cb1.SelectedItem = tra_ret;
                    int index1 = cb1.SelectedIndex;

                    AccederComboBox(nombre_cb + 2, 2, index1, tra_ret);

                    // Combobox: impuesto 

                    ComboBox cb2 = (ComboBox)this.Controls.Find(nombre_cb + 2, true).FirstOrDefault();
                    cb2.SelectedItem = t_impuesto;
                    int index2 = cb2.SelectedIndex;

                    AccederComboBox(nombre_cb + 3, 3, index2, t_impuesto);

                    // Combobox: tipo factor

                    ComboBox cb3 = (ComboBox)this.Controls.Find(nombre_cb + 3, true).FirstOrDefault();
                    cb3.SelectedItem = dato[2];
                    int index3 = cb3.SelectedIndex;

                    AccederComboBox(nombre_cb + 4, 4, index3, dato[1]);

                    // Combobox: tasa/cuota

                    ComboBox cb4 = (ComboBox)this.Controls.Find(nombre_cb + 4, true).FirstOrDefault();
                    cb4.SelectedItem = tasa_cuota;
                    int index4 = cb4.SelectedIndex;

                    AccederComboBox(nombre_cb, 5, index4, tasa_cuota);


                    // Textbox: Definir impuesto

                    if (tasa_cuota == "Definir %")
                    {
                        TextBox tb1 = (TextBox)this.Controls.Find(nombre_tb + 1, true).FirstOrDefault();
                        tb1.Text = tasa_cuota_porc;


                        // Textbox: Importe

                        double importe = 0;

                        if (dato[2] == "Tasa")
                        {
                            importe = Convert.ToDouble(dato[3]) * Convert.ToDouble(txtBoxBase.Text);

                            if (Convert.ToDecimal(dato[3]) > 1)
                            {
                                importe = importe / 100;
                            }
                        }
                        if (dato[2] == "Cuota")
                        {
                            importe = Convert.ToDouble(dato[3]) * 1;
                        }

                        TextBox tb2 = (TextBox)this.Controls.Find(nombre_tb + 2, true).FirstOrDefault();
                        tb2.Text = importe.ToString("0.00");
                    }

                    RecalcularTotal();

                    //t++;
                }                
            }
        }

        private void sel_incluye_impuestos(object sender, EventArgs e)
        {
            string valor = cmb_bx_incluye_impuestos.SelectedValue.ToString();

            
            // No incluye impuestos, o no esta obligado a desglosarlos

            if (valor == "01" | valor == "03" | valor == "04")
            {
                des_habilita_elementos_impuestos(0);
            }

            // Incluye impuestos

            // Agregar
            if(valor == "02" & AgregarEditarProducto.DatosSourceFinal == 1)
            {
                des_habilita_elementos_impuestos(1);
                rb16porCiento.Checked = true;
            }

            // Editar 
            if (valor == "02" & AgregarEditarProducto.DatosSourceFinal == 2)
            {
                var impuestoSeleccionado = AgregarEditarProducto.impuestoProductoFinal;

                des_habilita_elementos_impuestos(1);

                // Obtiene la lista de impuestos guardados
                cargar_impuestos();


                if (impuestoSeleccionado == "0%")
                {
                    rb0porCiento.Checked = true;
                }
                else if (impuestoSeleccionado == "16%")
                {
                    rb16porCiento.Checked = true;
                }
                else if (impuestoSeleccionado == "8%")
                {
                    rb8porCiento.Checked = true;
                }
                else if (impuestoSeleccionado == "Exento")
                {
                    rbExcento.Checked = true;
                }

                // Aplica para productos donde no se tenia impuestos agregados

                if(impuestoSeleccionado == "")
                {
                    rb16porCiento.Checked = true;
                }
            }
        }

        private void valida_rfc(object sender, EventArgs e)
        {
            string resul= formato_rfc();

            if(resul != "")
            {
                MessageBox.Show(resul, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void des_habilita_elementos_impuestos(int opcion)
        {
            // Deshabilita todo lo de impuestos

            if(opcion == 0)
            {
                btnExtra.Enabled = false;
                btnImpLocal.Enabled = false;
                gbx_impuestos_radios.Enabled = false;
                panelContenedor.Controls.Clear();
                txtIVA.Text = string.Empty;
                txtBoxBase.Text = string.Empty;
                txtTotal.Text = string.Empty;
                txtTotal.Enabled = false;

                rb0porCiento.Checked = false;
                rb8porCiento.Checked = false;
                rb16porCiento.Checked = false;
                rbExcento.Checked = false;
            }

            // Habilita todo lo de impuestos

            if (opcion == 1)
            {
                btnExtra.Enabled = true;
                btnImpLocal.Enabled = true;
                gbx_impuestos_radios.Enabled = true;
                txtIVA.Enabled = true;
                txtTotal.Enabled = true;
            }
        }

        private string formato_rfc()
        {
            string mensaje = "";
            int long_rfc = txt_rfc_cterceros.Text.Length;

            if (!string.IsNullOrEmpty(txt_rfc_cterceros.Text))
            {
                string formato_rfc = "^[A-Z&Ñ]{3,4}[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[A-Z0-9]{2}[0-9A]$";

                Regex exp = new Regex(formato_rfc);

                if (!exp.IsMatch(txt_rfc_cterceros.Text))
                {
                    return "El formato del RFC no es valido.";
                }

                // Carga opciones del régimen fiscal

                if (long_rfc == 12)
                {
                    carga_regimen("M");
                }
                if (long_rfc == 13)
                {
                    carga_regimen("F");
                }
            }
            
            if (long_rfc < 12 & long_rfc > 13)
            {
                return "La longitud del RFC es incorrecta.";
            }

            if (string.IsNullOrEmpty(txt_rfc_cterceros.Text))
            {
                carga_regimen("");
            }
            

            return mensaje;
        }

        private void solo_numeros_cp_cterceros(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) | Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void carga_regimen(string tipo)
        {
            Dictionary<string, string> regimenf = new Dictionary<string, string>();
            DataTable d_regimen_fiscal;


            if (tipo != "")
            {
                d_regimen_fiscal = cn.CargarDatos(cs.obtener_regimen_fiscal(tipo));

                foreach (DataRow r_regimen_fiscal in d_regimen_fiscal.Rows)
                {
                    regimenf.Add(r_regimen_fiscal["CodigoRegimen"].ToString(), r_regimen_fiscal["Descripcion"].ToString());
                }
            }
            else
            {
                regimenf.Add("", "...");
            }

            cmb_bx_regimen_cterceros.DataSource = regimenf.ToArray();
            cmb_bx_regimen_cterceros.DisplayMember = "value";
            cmb_bx_regimen_cterceros.ValueMember = "key";

        }
    }
}
