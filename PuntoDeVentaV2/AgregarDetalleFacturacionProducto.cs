using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class AgregarDetalleFacturacionProducto : Form
    {
        
        bool primera = true;
        int seleccionado = 0;
        int valorDefault = 0;
        static private int id = 2;

        string tipoImpuesto = null;
        string tipoPorcentaje = null;
        string porcentajeSeleccionado = null;

        private string rutaDirectorio = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;

        List<string> clavesUnidad = new List<string>();
        List<string> impuestos = new List<string>();
        List<string> factores = new List<string>();
        List<string> tasasCuotas = new List<string>();

        //Impuestos locales
        List<string> impuestosL = new List<string>();
        List<string> tasaL = new List<string>();

        double precioProducto = Convert.ToDouble(AgregarEditarProducto.precioProducto);

        public AgregarDetalleFacturacionProducto()
        {
            InitializeComponent();
            this.ControlBox = false;

            try
            {
                sql_con = new SQLiteConnection("Data source=" + rutaDirectorio + "\\BD\\pudveDB.db; Version=3; New=False;Compress=True;");
                sql_con.Open();
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = "SELECT * FROM CatalogoUnidadesMedida ORDER BY LOWER(Nombre) ASC";
                sql_cmd.ExecuteNonQuery();

                SQLiteDataReader dr = sql_cmd.ExecuteReader();

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

        private void btnCancelarDetalle_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AgregarDetalleFacturacionProducto_Load(object sender, EventArgs e)
        {
            //Se definen los valores que tendran los ComboBox y TextBox por default
            //al abrir la ventana por primera vez

            cbLinea1_1.SelectedIndex = 0;

            cbUnidadMedida.SelectedIndex = valorDefault;

            cbLinea1_2.Enabled = false;
            cbLinea1_3.Enabled = false;
            cbLinea1_4.Enabled = false;

            cbLinea1_1.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLinea1_2.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLinea1_3.DropDownStyle = ComboBoxStyle.DropDownList;
            cbLinea1_4.DropDownStyle = ComboBoxStyle.DropDownList;
            cbUnidadMedida.DropDownStyle = ComboBoxStyle.DropDownList;

            cbLinea1_1.MouseWheel += new MouseEventHandler(DeshabilitarMouseWheel);
            cbLinea1_2.MouseWheel += new MouseEventHandler(DeshabilitarMouseWheel);
            cbLinea1_3.MouseWheel += new MouseEventHandler(DeshabilitarMouseWheel);
            cbLinea1_4.MouseWheel += new MouseEventHandler(DeshabilitarMouseWheel);

            cbLinea1_1.SelectedIndexChanged += new EventHandler(ProcesarComboBoxes_selectedIndexChanged);
            cbLinea1_2.SelectedIndexChanged += new EventHandler(ProcesarComboBoxes_selectedIndexChanged);
            cbLinea1_3.SelectedIndexChanged += new EventHandler(ProcesarComboBoxes_selectedIndexChanged);
            cbLinea1_4.SelectedIndexChanged += new EventHandler(ProcesarComboBoxes_selectedIndexChanged);

            tbLinea1_1.KeyUp += new KeyEventHandler(PorcentajeManual_KeyUp);

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
        }

        private void btnExtra_Click(object sender, EventArgs e)
        {
            GenerarCampos(1);
        }

        private void btnImpLocal_Click(object sender, EventArgs e)
        {
            GenerarCampos(2);
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
                    panelContenedor.Controls.Remove(panel);
                }
            }
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
            string clavePS = txtClaveProducto.Text;
            string claveUnidad = txtClaveUnidad.Text;

            if (clavePS.Length < 8)
            {
                MessageBox.Show("La clave de producto debe contener 8 dígitos.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (claveUnidad == "")
            {
                MessageBox.Show("La clave de unidad es requerida.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //Realiza la conexion y realiza la consulta para verificar si la clave del producto es válida
           
        }


        /*****************************************************************************************
         **** ES LA FUNCION DEL EVENTO QUE SE EJECUTA CUANDO SE ELIGE UNA OPCION DEL COMBOBOX ****
         *****************************************************************************************/
        private void ProcesarComboBoxes_selectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            string nombreCB = cb.Name;

            //Identificar tipo impuesto
            string tipoImpuesto = nombreCB.Substring(0, 8);
            int rango = 0;

            if (tipoImpuesto == "cbLineaL")
            {
                rango = 10;
            }
            else
            {
                rango = 9;
            }
            
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

                //Cuando se esta agregando un impuesto local
                if (tipoImpuesto == "Loc. Retenido" || tipoImpuesto == "Loc. Traslado")
                {
                    //La opcion es el numero del indice del item seleccionado
                    if (opcion == 0)
                    {
                        LimpiarComboBox(cbTmp, false);
                    }

                    if (opcion == 1)
                    {
                        LimpiarComboBox(cbTmp, true);
                        cbTmp.Items.Add(impuestosL[0]); //...
                        cbTmp.Items.Add(impuestosL[1]); //ISH
                        cbTmp.Items.Add(impuestosL[5]); //Otro
                        cbTmp.SelectedIndex = 0;
                    }

                    if (opcion == 2)
                    {
                        LimpiarComboBox(cbTmp, true);
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
                    if (opcion == 0)
                    {
                        LimpiarComboBox(cbTmp, false);
                    }

                    if (opcion == 1)
                    {
                        LimpiarComboBox(cbTmp, true);
                        cbTmp.Items.Add(impuestos[0]); //...
                        cbTmp.Items.Add(impuestos[2]); //IVA
                        cbTmp.Items.Add(impuestos[3]); //IEPS
                        cbTmp.SelectedIndex = 0;
                    }

                    if (opcion == 2)
                    {
                        LimpiarComboBox(cbTmp, true);
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
                
                if (tipoImpuesto == "Loc. Retenido")
                {
                    tipoPorcentaje = seleccionado;

                    LimpiarComboBox(cbTmp, true);
                    cbTmp.Items.Add("..."); //...
                    cbTmp.SelectedIndex = 0;
                }

                if (tipoImpuesto == "Loc. Traslado")
                {
                    tipoPorcentaje = seleccionado;

                    LimpiarComboBox(cbTmp, true);
                    cbTmp.Items.Add("..."); //...
                    cbTmp.SelectedIndex = 0;
                }

                if (tipoImpuesto == "Traslado")
                {
                    tipoPorcentaje = seleccionado;

                    if (opcion == 1)
                    {
                        LimpiarComboBox(cbTmp, true);
                        cbTmp.Items.Add(factores[0]); //...
                        cbTmp.Items.Add(factores[1]); //Tasa
                        cbTmp.Items.Add(factores[3]); //Exento
                        cbTmp.SelectedIndex = 0;
                    }

                    if (opcion == 2)
                    {
                        LimpiarComboBox(cbTmp, true);
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
                        LimpiarComboBox(cbTmp, true);
                        cbTmp.Items.Add(factores[0]); //...
                        cbTmp.Items.Add(factores[1]); //Tasa
                        cbTmp.SelectedIndex = 0;
                    }

                    if (opcion == 2)
                    {
                        LimpiarComboBox(cbTmp, true);
                        cbTmp.Items.Add(factores[0]); //...
                        cbTmp.Items.Add(factores[1]); //Tasa
                        cbTmp.SelectedIndex = 0;
                    }

                    if (opcion == 3)
                    {
                        LimpiarComboBox(cbTmp, true);
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
                if (tipoImpuesto == "Loc. Retenido")
                {
                    if (tipoPorcentaje == "IMCD")
                    {
                        if (opcion == 0)
                        {
                            LimpiarComboBox(cbTmp, true);
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
                            LimpiarComboBox(cbTmp, true);
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
                            LimpiarComboBox(cbTmp, true);
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
                            LimpiarComboBox(cbTmp, true);
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
                            LimpiarComboBox(cbTmp, true);
                            cbTmp.Items.Add(tasaL[0]); //...
                            cbTmp.Items.Add(tasaL[3]); //3%
                            cbTmp.Items.Add(tasaL[5]); //Definir %
                            cbTmp.SelectedIndex = 0;
                        }
                    }

                    if (tipoPorcentaje == "Otro")
                    {
                        LimpiarComboBox(cbTmp, true);
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
                            LimpiarComboBox(cbTmp, true);
                            cbTmp.Items.Add(tasasCuotas[0]); //...
                            cbTmp.Items.Add(tasasCuotas[1]); //0%
                            cbTmp.Items.Add(tasasCuotas[2]); //16%
                            cbTmp.SelectedIndex = 0;
                        }

                        if (opcion == 2)
                        {
                            LimpiarComboBox(cbTmp, true);
                            cbTmp.Items.Add(tasasCuotas[0]); //...
                            cbTmp.SelectedIndex = 0;
                        }
                    }

                    if (tipoPorcentaje == "IEPS")
                    {
                        if (opcion == 1)
                        {
                            LimpiarComboBox(cbTmp, true);
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
                            LimpiarComboBox(cbTmp, true);
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
                            LimpiarComboBox(cbTmp, true);
                            cbTmp.Items.Add(tasasCuotas[0]); //...
                            cbTmp.Items.Add(tasasCuotas[3]); //Definir %
                            cbTmp.SelectedIndex = 0;
                        }
                    }

                    if (tipoPorcentaje == "IVA")
                    {
                        if (opcion == 1)
                        {
                            LimpiarComboBox(cbTmp, true);
                            cbTmp.Items.Add(tasasCuotas[0]); //...
                            cbTmp.Items.Add(tasasCuotas[3]); //Definir %
                            cbTmp.SelectedIndex = 0;
                        }
                    }

                    if (tipoPorcentaje == "IEPS")
                    {
                        if (opcion == 1)
                        {
                            LimpiarComboBox(cbTmp, true);
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
                            LimpiarComboBox(cbTmp, true);
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

                    float porcentaje = CantidadPorcentaje(cantidadTmp[0]);

                    double importe = precioProducto * porcentaje;

                    TextBox tbTmp = (TextBox)this.Controls.Find(nombre, true).FirstOrDefault();
                    tbTmp.Text = importe.ToString("0.00");
                }
            }
        }

        private void LimpiarComboBox(ComboBox cb, bool habilitado = true)
        {
            cb.DataSource = null;
            cb.Items.Clear();
            cb.Enabled = habilitado;
        }

        /******************************************************************
         **** FUNCION PARA CONVERTIR LOS PORCENTAJES AL VALOR CORRECTO ****
         ******************************************************************/

        private float CantidadPorcentaje(string sCantidad)
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

                    } else
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

        /********************************************************
         **** FUNCION PARA CALCULAR LOS PORCENTAJES MANUALES ****
         ********************************************************/

        private void PorcentajeManual_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;

            var nombre = tb.Name.Remove(tb.Name.Length - 1);

            var cantidad = tb.Text;

            if (cantidad == "")
            {
                cantidad = "0";
            }

            float porcentaje = CantidadPorcentaje(cantidad);

            if (porcentaje < 0 || porcentaje > 0.43770000)
            {
                MessageBox.Show("El porcentaje debe ser entre 0 % y 43.770000 %", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double importe = precioProducto * porcentaje;

            TextBox tbImporte = (TextBox)this.Controls.Find(nombre + "2", true).FirstOrDefault();
            tbImporte.Text = importe.ToString("0.00");
        }
    }
}
