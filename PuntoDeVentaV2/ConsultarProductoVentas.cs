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
            var conceptos = mb.ConceptosAppSettings();

            foreach (var concepto in conceptos)
            {
                if (concepto == "Proveedor")
                {
                    continue;
                }

                // Este valor de proveedor esta agregado por defecto
                DataGridViewColumn columna = new DataGridViewTextBoxColumn();
                columna.HeaderText = concepto;
                columna.Name = concepto;
                DGVProductos.Columns.Add(columna);

                // Guardamos los nombres de las propiedades en la lista
                propiedades.Add(concepto);
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
                    var valor = propiedad.Value;

                    if (propiedad.Key == "Tipo")
                    {
                        if (valor == "P") { valor = "PRODUCTO"; }
                        if (valor == "S") { valor = "SERVICIO"; }
                        if (valor == "PQ") { valor = "PAQUETE"; }
                    }

                    if (propiedad.Key == "Precio")
                    {
                        var precio = float.Parse(valor);
                        valor = precio.ToString("0.00");
                    }

                    if (propiedad.Key == "Stock")
                    {
                        valor = Utilidades.RemoverCeroStock(valor);
                    }

                    row.Cells[propiedad.Key].Value = valor;
                }
            }
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            timerBusqueda.Stop();
            timerBusqueda.Start();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void ConsultarProductoVentas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}
