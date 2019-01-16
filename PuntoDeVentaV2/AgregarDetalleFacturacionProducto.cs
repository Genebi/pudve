using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PuntoDeVentaV2
{
    public partial class AgregarDetalleFacturacionProducto : Form
    {
        static private int id = 2;
        int valorDefault = 0;
        bool primera = true;
        int previo = 0;

        List<string> clavesUnidad = new List<string>();

        double precioProducto = Convert.ToDouble(AgregarEditarProducto.precioProducto);

        public AgregarDetalleFacturacionProducto()
        {
            InitializeComponent();
            this.ControlBox = false;

            /*try
            {
                conexion = new SqlConnection(cadenaConexion);
                conexion.Open();

                comando = new SqlCommand("SELECT * FROM dbo.CatalogoUnidadMedida ORDER BY Nombre ASC", conexion);
                dr = comando.ExecuteReader();

                while (dr.Read())
                {
                    string nombreTmp = dr[1] + " - " + dr[2];

                    ComboboxItem item = new ComboboxItem();
                    item.Text = nombreTmp;
                    item.Value = dr[1];

                    clavesUnidad.Add(dr[1].ToString());
                    
                    cbUnidadMedida.Items.Add(item);
                }

                ComboboxItem item2 = new ComboboxItem();
                item2.Text = "Selecciona una opción";
                item2.Value = "";
                clavesUnidad.Add("");
                valorDefault = cbUnidadMedida.Items.Add(item2);
            }
            catch (Exception)
            {
                //Falta agregar una variable para manejar la excepcion en caso de ser requerido
            }*/
        }

        private void btnCancelarDetalle_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void AgregarDetalleFacturacionProducto_Load(object sender, EventArgs e)
        {
            cbLinea1_1.SelectedIndex = 0;
            cbLinea1_2.SelectedIndex = 0;
            cbLinea1_3.SelectedIndex = 0;
            cbLinea1_4.SelectedIndex = 0;

            //cbUnidadMedida.SelectedIndex = valorDefault;

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
        }

        private void btnExtra_Click(object sender, EventArgs e)
        {
            GenerarCampos(1);
        }

        private void btnImpLocal_Click(object sender, EventArgs e)
        {
            GenerarCampos(2);
        }

        private void GenerarCampos(int tipo)
        {
            FlowLayoutPanel panelHijo = new FlowLayoutPanel();
            panelHijo.Name = "panelGeneradoR" + id;
            panelHijo.Height = 25;
            panelHijo.Width = 750;

            ComboBox cb1 = new ComboBox();
            cb1.Name = "cbLinea" + id + "_1";
            
            if (tipo == 1)
            {
                cb1.Items.Add("...");
                cb1.Items.Add("Traslado");
                cb1.Items.Add("Retención");
            }
            else
            {
                cb1.Items.Add("Imp. local");
            }

            cb1.SelectedIndex = 0;
            cb1.DropDownStyle = ComboBoxStyle.DropDownList;
            cb1.MouseWheel += new MouseEventHandler(DeshabilitarMouseWheel);
            cb1.Width = 100;
            cb1.Margin = new Padding(15, 0, 0, 0);

            ComboBox cb2 = new ComboBox();
            cb2.Name = "cbLinea" + id + "_2";

            if (tipo == 1)
            {
                cb2.Items.Add("...");
                cb2.Items.Add("ISR");
                cb2.Items.Add("IVA");
                cb2.Items.Add("IEPS");
            }
            else
            {
                cb2.Items.Add("...");
                cb2.Items.Add("ISH");
                cb2.Items.Add("IMCD");
                cb2.Items.Add("Bienestar Social");
                cb2.Items.Add("Millar");
            }

            cb2.SelectedIndex = 0;
            cb2.DropDownStyle = ComboBoxStyle.DropDownList;
            cb2.MouseWheel += new MouseEventHandler(DeshabilitarMouseWheel);
            cb2.Width = 100;
            cb2.Margin = new Padding(20, 0, 0, 0);

            ComboBox cb3 = new ComboBox();
            cb3.Name = "cbLinea" + id + "_3";
            
            if (tipo == 1)
            {
                cb3.Items.Add("...");
                cb3.Items.Add("Tasa");
                cb3.Items.Add("Cuota");
                cb3.Items.Add("Exento");
            }
            else
            {
                cb3.Items.Add("...");
            }

            cb3.SelectedIndex = 0;
            cb3.DropDownStyle = ComboBoxStyle.DropDownList;
            cb3.MouseWheel += new MouseEventHandler(DeshabilitarMouseWheel);
            cb3.Width = 100;
            cb3.Margin = new Padding(20, 0, 0, 0);

            ComboBox cb4 = new ComboBox();
            cb4.Name = "cbLinea" + id + "_4";
            cb4.Items.Add("...");
            
            if (tipo == 1)
            {
                cb4.Items.Add("0%");
                cb4.Items.Add("16%");
                cb4.Items.Add("Definir %");
                cb4.Items.Add("26.5%");
                cb4.Items.Add("30%");
                cb4.Items.Add("53%");
                cb4.Items.Add("50%");
                cb4.Items.Add("1.60%");
                cb4.Items.Add("30.4%");
                cb4.Items.Add("25%");
                cb4.Items.Add("9%");
                cb4.Items.Add("8%");
                cb4.Items.Add("7%");
                cb4.Items.Add("6%");
                cb4.Items.Add("3%");
            }
            else
            {
                cb4.Items.Add("1%");
                cb4.Items.Add("2%");
                cb4.Items.Add("3%");
                cb4.Items.Add("5%");
                cb4.Items.Add("Definir %");
            }

            cb4.SelectedIndex = 0;
            cb4.DropDownStyle = ComboBoxStyle.DropDownList;
            cb4.MouseWheel += new MouseEventHandler(DeshabilitarMouseWheel);
            cb4.Width = 100;
            cb4.Margin = new Padding(20, 0, 0, 0);

            TextBox tb1 = new TextBox();
            tb1.Name = "tbLinea" + id + "_1";
            tb1.Width = 100;
            tb1.Height = 20;
            tb1.Margin = new Padding(20, 0, 0, 0);

            TextBox tb2 = new TextBox();
            tb2.Name = "tbLinea" + id + "_2";
            tb2.Width = 100;
            tb2.Height = 20;
            tb2.Margin = new Padding(20, 0, 0, 0);

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
                int seleccionado = cbUnidadMedida.SelectedIndex;

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

        private void cbLinea1_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!primera)
            {
                //Primer ComboBox de los impuestos estaticos
                int seleccionado = cbLinea1_1.SelectedIndex;

                if (seleccionado > 0)
                {
                    cbLinea1_2.Enabled = true;
                }
                else
                {
                    cbLinea1_2.Enabled = false;
                    cbLinea1_3.Enabled = false;
                    cbLinea1_4.Enabled = false;

                    cbLinea1_2.SelectedIndex = 0;
                    cbLinea1_3.SelectedIndex = 0;
                    cbLinea1_4.SelectedIndex = 0;
                }

                previo = seleccionado;
            }

            primera = false;
        }

        private void cbLinea1_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Segundo ComboBox de los impuestos estaticos
            int seleccionado = cbLinea1_2.SelectedIndex;

            if (seleccionado > 0)
            {
                cbLinea1_3.Enabled = true;

                //Previo es el valor de la opcion seleccionada del ComboBox anterior a este
                if (previo == 1)
                {
                    if (seleccionado == 1)
                    {
                        cbLinea1_2.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                cbLinea1_3.Enabled = false;
                cbLinea1_4.Enabled = false;

                cbLinea1_3.SelectedIndex = 0;
                cbLinea1_4.SelectedIndex = 0;
            }

            previo = seleccionado;
        }

        private void cbLinea1_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Tercer ComboBox de los impuestos estaticos
            int seleccionado = cbLinea1_3.SelectedIndex;

            if (seleccionado > 0)
            {
                cbLinea1_4.Enabled = true;

                if (previo == 2)
                {
                    if (seleccionado == 2)
                    {
                        cbLinea1_3.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                cbLinea1_4.Enabled = false;
            }
        }

        private void cbLinea1_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Cuarto ComboBox de los impuestos estaticos
            int seleccionado = cbLinea1_4.SelectedIndex;

            if (seleccionado == 3)
            {
                tbLinea1_1.Enabled = true;
            }
            else
            {
                tbLinea1_1.Enabled = false;
                tbLinea1_1.Text = "";
            }
        }
    }
}
