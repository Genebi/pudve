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

namespace PuntoDeVentaV2
{
    public partial class ConsultarProductoVentas : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        public ConsultarProductoVentas()
        {
            InitializeComponent();
        }

        private void ConsultarProductoVentas_Load(object sender, EventArgs e)
        {
            
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
                var coincidencias = mb.BusquedaCoincidencias(busqueda);

                if (coincidencias.Count > 0)
                {
                    foreach (var producto in coincidencias)
                    {
                        var datos = cn.BuscarProducto(producto.Key, FormPrincipal.userID);

                        AgregarProducto(datos);
                    }
                }
            }
        }

        private void AgregarProducto(string[] datos)
        {
            int rowId = DGVProductos.Rows.Add();

            DataGridViewRow row = DGVProductos.Rows[rowId];

            row.Cells["Nombre"].Value = datos[1];
            row.Cells["Stock"].Value = datos[4];
            row.Cells["Precio"].Value = datos[2];
            row.Cells["Clave"].Value = datos[6];
            row.Cells["Codigo"].Value = datos[7];
            row.Cells["Tipo"].Value = datos[5];
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            timerBusqueda.Stop();
            timerBusqueda.Start();
        }
    }
}
