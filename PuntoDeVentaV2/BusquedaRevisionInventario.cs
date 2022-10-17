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
        public static string id { get; set; }

        public BusquedaRevisionInventario()
        {
            InitializeComponent();
            txtBuscar.Select();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
            else if (e.KeyCode == Keys.Down && !dgvRevisarInventario.Rows.Count.Equals(0))
            {
                dgvRevisarInventario.Focus();
                dgvRevisarInventario.FirstDisplayedScrollingRowIndex = dgvRevisarInventario.Rows.Count - 1;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Contains("\'"))
            {
                string producto = txtBuscar.Text.Replace("\'", ""); ;
                txtBuscar.Text = producto;
                txtBuscar.Select(txtBuscar.Text.Length, 0);
            }
            //Poner todas las letras mayusculas
            txtBuscar.CharacterCasing = CharacterCasing.Upper;

        }

        private void dgvRevisarInventario_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ejecutarAccion();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string obtenerDatosTxt = txtBuscar.Text;

            if (!string.IsNullOrEmpty(obtenerDatosTxt))
            {
                string cadena = obtenerDatosTxt.Trim();
                char delimitador = (' ');

                string[] separarPalabras = cadena.Split(delimitador);

                foreach (var iterar in separarPalabras)
                {   //Consulta para el buscador
                    using (var buscarDatos = cn.CargarDatos($"SELECT ID, Nombre, Stock, Precio, Tipo, ClaveInterna, CodigoBarras FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND Status = 1 AND Nombre LIKE '%{iterar}%'"))
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
                                }
                                else if (llenarCampos["Tipo"].ToString().Equals("PQ"))
                                {
                                    categoria = "COMBO";
                                }
                                else if (llenarCampos["Tipo"].ToString().Equals("S"))
                                {
                                    categoria = "SERVICIO";
                                }
                                //Agregar los datos al DataGridView
                                dgvRevisarInventario.Rows.Add(llenarCampos["ID"].ToString(), llenarCampos["Nombre"].ToString(), llenarCampos["Stock"].ToString(), llenarCampos["Precio"].ToString(), categoria, llenarCampos["ClaveInterna"].ToString(), llenarCampos["CodigoBarras"].ToString());
                                dgvRevisarInventario.Focus();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Ingrese el nombre de algun producto", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Question);
                txtBuscar.Focus();
            }
        }


        private void ejecutarAccion()
        {
            //Obtiene el codigo de barras de la fila seleccionada
            codigoBarras = dgvRevisarInventario.Rows[dgvRevisarInventario.CurrentRow.Index].Cells[6].Value.ToString();
            id = dgvRevisarInventario.Rows[dgvRevisarInventario.CurrentRow.Index].Cells[0].Value.ToString();

            this.Dispose();
        }

        private void dgvRevisarInventario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ejecutarAccion();
            }
            else if (e.KeyCode == Keys.Up && dgvRevisarInventario.CurrentRow.Index == 0)
            {
                txtBuscar.Focus();
            }
        }

        private void BusquedaRevisionInventario_Load(object sender, EventArgs e)
        {
            var mostrarClave = FormPrincipal.clave;

            if (mostrarClave == 0)
            {
                dgvRevisarInventario.Columns[5].Visible = false;
            }
            else if (mostrarClave == 1)
            {
                dgvRevisarInventario.Columns[5].Visible = true;
            }
        }
    }
}
