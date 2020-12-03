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
    public partial class BusquedaRevisionInventario : Form
    {
        Conexion cn = new Conexion();

        public static string codigoBarras { get; set; }

        public BusquedaRevisionInventario()
        {
            InitializeComponent();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string obtenerDatosTxt = txtBuscar.Text;
                string cadena = obtenerDatosTxt.Trim();
                char delimitador = (' ');

                string[] separarPalabras = cadena.Split(delimitador);

                foreach (var iterar in separarPalabras)
                {   //Consulta para el buscador
                    using (var buscarDatos = cn.CargarDatos($"SELECT IDAlmacen, Nombre, StockFisico, PrecioProducto, Tipo, ClaveInterna, CodigoBarras FROM RevisarInventario WHERE IDUsuario = '{FormPrincipal.userID}' AND Nombre LIKE '%{iterar}%'"))
                    {
                        if (!buscarDatos.Rows.Count.Equals(0))
                        {
                            dgvRevisarInventario.Rows.Clear();
                            foreach (DataRow llenarCampos in buscarDatos.Rows)
                            {   //Cambiar la abreviacion por la palabra completa en el Tipo
                                var categoria = string.Empty;
                                if (llenarCampos["Tipo"].ToString().Equals("P"))
                                {
                                    categoria = "PRODUCTO";
                                }else if (llenarCampos["Tipo"].ToString().Equals("PQ"))
                                {
                                    categoria = "COMBO";
                                }else if (llenarCampos["Tipo"].ToString().Equals("S"))
                                {
                                    categoria = "SERVICIO";
                                }
                                //Agregar los datos al DataGridView
                                dgvRevisarInventario.Rows.Add(llenarCampos["IDAlmacen"].ToString(), llenarCampos["Nombre"].ToString(), llenarCampos["StockFisico"].ToString(), llenarCampos["PrecioProducto"].ToString(), categoria, llenarCampos["ClaveInterna"].ToString(), llenarCampos["CodigoBarras"].ToString());
                            }
                        }
                    }
                }
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            txtBuscar.CharacterCasing = CharacterCasing.Upper;
        }

        private void dgvRevisarInventario_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            codigoBarras = dgvRevisarInventario.Rows[dgvRevisarInventario.CurrentRow.Index].Cells[6].Value.ToString();
            this.Dispose();
        }

        private void BusquedaRevisionInventario_Load(object sender, EventArgs e)
        {
            txtBuscar.Focus();
        }
    }
}
