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
    public partial class FiltroReporteProductos : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        Dictionary<string, string> opcionesDefault;

        public FiltroReporteProductos()
        {
            InitializeComponent();
        }

        private void FiltroReporteProductos_Load(object sender, EventArgs e)
        {
            opcionesDefault = new Dictionary<string, string>();

            // Datos para cargar el combobox de Stock y Precio
            var sourceOpciones = new Dictionary<string, string>();
            sourceOpciones.Add("NA", "No aplica");
            sourceOpciones.Add(">=", "Mayor o igual que");
            sourceOpciones.Add("<=", "Menor o igual que");
            sourceOpciones.Add("==", "Igual que");
            sourceOpciones.Add(">", "Mayor que");
            sourceOpciones.Add("<", "Menor que");

            cbStock.DataSource = sourceOpciones.ToArray();
            cbStock.ValueMember = "Key";
            cbStock.DisplayMember = "Value";

            cbPrecio.DataSource = sourceOpciones.ToArray();
            cbPrecio.ValueMember = "Key";
            cbPrecio.DisplayMember = "Value";

            // Cargar los proveedores para el combobox
            var proveedores = cn.ObtenerProveedores(FormPrincipal.userID);

            if (proveedores.Length > 0)
            {
                Dictionary<int, string> dicProveedores = new Dictionary<int, string>();

                dicProveedores.Add(0, "Seleccionar proveedor...");

                foreach (string proveedor in proveedores)
                {
                    var info = proveedor.Split('-');

                    dicProveedores.Add(Convert.ToInt32(info[0].Trim()), info[1].Trim());
                }

                cbProveedor.DataSource = dicProveedores.ToArray();
                cbProveedor.ValueMember = "Key";
                cbProveedor.DisplayMember = "Value";
            }
            else
            {
                Dictionary<int, string> dicProveedores = new Dictionary<int, string>();

                dicProveedores.Add(0, "No hay proveedores registrados");
                cbProveedor.DataSource = dicProveedores.ToArray();
                cbProveedor.ValueMember = "Key";
                cbProveedor.DisplayMember = "Value";
            }

            // Obtiene los detalles guardados en App.config y los muestra en el form
            ObtenerDetalles();
            VisualizarDetalles();
        }

        private void ObtenerDetalles()
        {
            XmlDocument xmlDoc = new XmlDocument();

            if (Properties.Settings.Default.TipoEjecucion == 1)
            {
                xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            }

            if (Properties.Settings.Default.TipoEjecucion == 2)
            {
                xmlDoc.Load(Properties.Settings.Default.baseDirectory + Properties.Settings.Default.archivo);
            }

            // Obtenemos el nodo principal de las propiedades del archivo App.config
            XmlNode appSettingsNode = xmlDoc.SelectSingleNode("configuration/appSettings");

            //======================================================================
            //======================================================================
            foreach (XmlNode childNode in appSettingsNode)
            {
                var key = childNode.Attributes["key"].Value;
                var value = Convert.ToBoolean(childNode.Attributes["value"].Value);

                // Ignoramos los checkbox secundarios de cada propiedad
                if (key.Substring(0, 3) == "chk")
                {
                    continue;
                }

                // Si el valor de la propiedad es true (esta habilitado)
                if (value == true)
                {
                    // Este valor de proveedor esta agregado por defecto
                    if (key == "Proveedor")
                    {
                        continue;
                    }
                    else
                    {
                        opcionesDefault.Add(key, key);
                    }
                }
            }
        }

        private void VisualizarDetalles()
        {
            if (opcionesDefault.Count > 0)
            {
                Font fuente = new Font("Century Gothic", 9.0f);

                int alturaEjeY = 140;

                foreach (var opcion in opcionesDefault)
                {
                    CheckBox checkCustom = new CheckBox();
                    checkCustom.Name = "check" + opcion.Key;
                    checkCustom.Text = opcion.Key;
                    checkCustom.Tag = "check" + opcion.Key;
                    checkCustom.AutoSize = true;
                    checkCustom.Font = fuente;
                    checkCustom.Location = new Point(20, alturaEjeY);
                    checkCustom.CheckAlign = ContentAlignment.MiddleLeft;
                    checkCustom.CheckedChanged += checkCustom_CheckedChanged;

                    // Consultar los datos para cada detalle de producto agregado
                    var datosPropiedad = mb.ObtenerOpcionesPropiedad(FormPrincipal.userID, opcion.Key);
                    var sourceCBCustom = new Dictionary<string, string>();

                    if (datosPropiedad.Count > 0)
                    {
                        sourceCBCustom.Add("NA", $"Seleccione {opcion.Key}");

                        foreach (var valor in datosPropiedad)
                        {
                            sourceCBCustom.Add(opcion.Key, valor.Value);
                        }
                    }
                    else
                    {
                        sourceCBCustom.Add("NA", $"No existe {opcion.Key.ToLower()} registrado");
                    }

                    ComboBox cbCustom = new ComboBox();
                    cbCustom.Name = "cb" + opcion.Key;
                    cbCustom.Font = fuente;
                    cbCustom.Location = new Point(108, alturaEjeY);
                    cbCustom.DropDownStyle = ComboBoxStyle.DropDownList;
                    cbCustom.Enabled = false;
                    cbCustom.Width = 350;
                    cbCustom.DataSource = sourceCBCustom.ToArray();
                    cbCustom.DisplayMember = "Value";
                    cbCustom.ValueMember = "Key";
                    //cbCustom.SelectionChangeCommitted += cbCustom_SelectionChangeCommitted;

                    panelContenedor.Controls.Add(checkCustom);
                    panelContenedor.Controls.Add(cbCustom);

                    alturaEjeY += 40;
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Aceptar");
        }

        /*private void cbCustom_SelectionChangeCommitted(object sender, EventArgs e)
        {
            var comboBoxCutom = (ComboBox)sender;
            var opcion = comboBoxCutom.SelectedValue;

            MessageBox.Show(opcion.ToString());
        }*/

        private void checkStock_CheckedChanged(object sender, EventArgs e)
        {
            if (checkStock.Checked)
            {
                cbStock.Enabled = true;
                txtStock.Enabled = true;
            }
            else
            {
                cbStock.SelectedValue = "NA";
                cbStock.Enabled = false;
                txtStock.Enabled = false;
                txtStock.Text = "0";
            }
        }

        private void checkPrecio_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPrecio.Checked)
            {
                cbPrecio.Enabled = true;
                txtPrecio.Enabled = true;
            }
            else
            {
                cbPrecio.SelectedValue = "NA";
                cbPrecio.Enabled = false;
                txtPrecio.Enabled = false;
                txtPrecio.Text = "0";
            }
        }

        private void checkProveedor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkProveedor.Checked)
            {
                cbProveedor.Enabled = true;
            }
            else
            {
                cbProveedor.SelectedValue = 0;
                cbProveedor.Enabled = false;
            }
        }

        private void checkCustom_CheckedChanged(object sender, EventArgs e)
        {
            var checkCustom = (CheckBox)sender;
            var nombreCB = checkCustom.Name;

            nombreCB = nombreCB.Replace("check", "");

            var comboBoxCustom = (ComboBox)Controls.Find("cb" + nombreCB, true).FirstOrDefault();

            if (checkCustom.Checked)
            {
                comboBoxCustom.Enabled = true;
            }
            else
            {
                comboBoxCustom.SelectedValue = "NA";
                comboBoxCustom.Enabled = false;
            }
        }
    }
}
