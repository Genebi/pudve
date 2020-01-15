using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PuntoDeVentaV2
{
    public partial class ConsultarProductoVentas : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();
        private List<string> propiedades = new List<string>();

        public ConsultarProductoVentas()
        {
            InitializeComponent();
        }

        private void ConsultarProductoVentas_Load(object sender, EventArgs e)
        {
            GenerarColumnas();
        }

        private void GenerarColumnas()
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
                    if (key == "Proveedor")
                    {
                        continue;
                    }

                    // Este valor de proveedor esta agregado por defecto
                    DataGridViewColumn columna = new DataGridViewTextBoxColumn();
                    columna.HeaderText = key;
                    columna.Name = key;
                    DGVProductos.Columns.Add(columna);

                    // Guardamos los nombres de las propiedades en la lista
                    propiedades.Add(key);
                }
            }
        }

        private void ConsultarProductoVentas_Shown(object sender, EventArgs e)
        {
            txtBuscar.Focus();
        }

        private void timerBusqueda_Tick(object sender, EventArgs e)
        {
            timerBusqueda.Stop();
            BuscarProductos();
        }

        private void BuscarProductos()
        {
            var busqueda = txtBuscar.Text.Trim();

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                var coincidencias = mb.BusquedaCoincidenciasVentas(busqueda);

                if (coincidencias.Count > 0)
                {
                    DGVProductos.Rows.Clear();

                    foreach (var producto in coincidencias)
                    {
                        var datos = mb.ProductoConsultadoVentas(producto.Key, propiedades);

                        AgregarProducto(datos);
                    }
                }
            }
        }

        private void AgregarProducto(Dictionary<string, string> datos)
        {
            if (datos.Count > 0)
            {
                int rowId = DGVProductos.Rows.Add();
                DataGridViewRow row = DGVProductos.Rows[rowId];

                foreach (var propiedad in datos)
                {
                    row.Cells[propiedad.Key].Value = propiedad.Value;
                }
            }
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            timerBusqueda.Stop();
            timerBusqueda.Start();
        }
    }
}
