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

        Dictionary<string, string> opcionesDefault;

        public FiltroReporteProductos()
        {
            InitializeComponent();
        }

        private void FiltroReporteProductos_Load(object sender, EventArgs e)
        {
            opcionesDefault = new Dictionary<string, string>();

            // Datos para cargar el combobox de Stock y Precio
            Dictionary<string, string> condiciones = new Dictionary<string, string>();
            condiciones.Add("NA", "No aplica");
            condiciones.Add(">=", "Mayor o igual que");
            condiciones.Add("<=", "Menor o igual que");
            condiciones.Add("==", "Igual que");
            condiciones.Add(">", "Mayor que");
            condiciones.Add("<", "Menor que");

            cbStock.DataSource = condiciones.ToArray();
            cbStock.ValueMember = "Key";
            cbStock.DisplayMember = "Value";

            cbPrecio.DataSource = condiciones.ToArray();
            cbPrecio.ValueMember = "Key";
            cbPrecio.DisplayMember = "Value";

            // Cargar los proveedores para el combobox
            var proveedores = cn.ObtenerProveedores(FormPrincipal.userID);

            if (proveedores.Length > 0)
            {
                //cbProveedor.Items.Add()
            }
            else
            {

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
                    checkCustom.Name = opcion.Key;
                    checkCustom.Text = opcion.Key;
                    checkCustom.Tag = "cb" + opcion.Key;
                    checkCustom.AutoSize = true;
                    checkCustom.Font = fuente;
                    checkCustom.Location = new Point(20, alturaEjeY);
                    //checkCustom.CheckAlign = ContentAlignment.MiddleLeft;
                    //cbCustom.CheckedChanged += cbCustom_CheckedChanged;

                    ComboBox cbCustom = new ComboBox();
                    cbCustom.Font = fuente;
                    cbCustom.Location = new Point(108, alturaEjeY);
                    cbCustom.DropDownStyle = ComboBoxStyle.DropDownList;
                    cbCustom.Width = 350;

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
    }
}
