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
    public partial class RevisarInventario : Form
    {
        Conexion cn = new Conexion();

        int cantidadStock;
        string SearchBarCode, queryTaerStock, tablaProductos, tablaRevisarInventario, buscarStock;
        string ID, Nombre, Stock, ClaveInterna, CodigoBarras, Fecha, IDUsuario;

        private void RevisarInventario_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearTable(dtRevisarStockResultado);
        }

        DataTable dtRevisarStockResultado;

        public RevisarInventario()
        {
            InitializeComponent();
            cantidadStock = 0;
            SearchBarCode = string.Empty;
        }

        private void btnReducirStock_Click(object sender, EventArgs e)
        {
            if (txtCantidadStock.Text == "0")
            {
                MessageBox.Show("No se permite tener stock menor a = " + txtCantidadStock.Text, "Error al Disminuir Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!txtCantidadStock.Equals("0"))
            {
                cantidadStock = Convert.ToInt32(txtCantidadStock.Text);
                cantidadStock--;
                if (cantidadStock >= 0)
                {
                    txtCantidadStock.Text = Convert.ToString(cantidadStock);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearTable(dtRevisarStockResultado);
            this.Close();
        }

        private void txtBoxBuscarCodigoBarras_TextChanged(object sender, EventArgs e)
        {
            int index = 0;
            ID = string.Empty;
            Nombre = string.Empty;
            Stock = string.Empty;
            ClaveInterna = string.Empty;
            CodigoBarras = string.Empty;
            Fecha = string.Empty;
            IDUsuario = string.Empty;

            buscarStock = string.Empty;

            if (txtBoxBuscarCodigoBarras.Text != string.Empty)
            {
                buscarStock = txtBoxBuscarCodigoBarras.Text;
                queryTaerStock = $"SELECT prod.ID, prod.Nombre, prod.Stock, prod.ClaveInterna, prod.CodigoBarras, prod.Fecha, prod.IDUsuario, prod.Tipo FROM RevisarInventario prod WHERE prod.IDUsuario = '{FormPrincipal.userID}' AND prod.ClaveInterna LIKE '%" + buscarStock + "%' OR prod.CodigoBarras LIKE '%" + buscarStock + "%'";
                dtRevisarStockResultado = cn.CargarDatos(queryTaerStock);

                if (dtRevisarStockResultado.Rows.Count != 0)
                {
                    ID = dtRevisarStockResultado.Rows[index]["ID"].ToString();
                    Nombre = dtRevisarStockResultado.Rows[index]["Nombre"].ToString();
                    lblNombreProducto.Text = Nombre;
                    Stock = dtRevisarStockResultado.Rows[index]["Stock"].ToString();
                    txtCantidadStock.Text = Stock;
                    ClaveInterna = dtRevisarStockResultado.Rows[index]["ClaveInterna"].ToString();
                    CodigoBarras = dtRevisarStockResultado.Rows[index]["CodigoBarras"].ToString();
                    lblCodigoDeBarras.Text = buscarStock;

                    txtCantidadStock.Focus();
                }
                else if (dtRevisarStockResultado.Rows.Count == 0)
                {
                    MessageBox.Show("Producto no encontrado.", "Error de busqueda.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
            txtBoxBuscarCodigoBarras.Text = string.Empty;
        }

        private void btnAumentarStock_Click(object sender, EventArgs e)
        {
            cantidadStock = Convert.ToInt32(txtCantidadStock.Text);
            cantidadStock++;
            txtCantidadStock.Text = Convert.ToString(cantidadStock);
        }

        private void RevisarInventario_Load(object sender, EventArgs e)
        {
            CargarStockExistente();
        }

        private void ClearTable(DataTable table)
        {
            try
            {
                table.Clear();
                lblNombreProducto.Text = string.Empty;
                lblCodigoDeBarras.Text = string.Empty;
                txtCantidadStock.Text = string.Empty;
                txtCantidadStock.Text = "0";
                txtBoxBuscarCodigoBarras.Focus();
            }
            catch (DataException e)
            {
                MessageBox.Show("Error al Limpiar la Tabla error: {0}" + e.GetType().ToString(), "Error de limpiar Tabla", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarStockExistente()
        {
            tablaRevisarInventario = "RevisarInventario";
            tablaProductos = "Productos";
            try
            {
                queryTaerStock = $"DELETE FROM '{tablaRevisarInventario}';";
                cn.EjecutarConsulta(queryTaerStock);
                queryTaerStock = $"DELETE FROM sqlite_sequence WHERE name = '{tablaRevisarInventario}';";
                cn.EjecutarConsulta(queryTaerStock);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar registros de la tabla de: " + tablaRevisarInventario.ToString() + "\n" + ex.Message.ToString(), "Error al borrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                queryTaerStock = $@"INSERT INTO '{tablaRevisarInventario}' (ID,
                                                                            Nombre,
                                                                            Stock,
                                                                            ClaveInterna,
                                                                            CodigoBarras,
                                                                            IDUsuario,
                                                                            Tipo) 
                                                                     SELECT ID,
                                                                            Nombre,
                                                                            Stock,
                                                                            ClaveInterna,
                                                                            CodigoBarras,
                                                                            IDUsuario,
                                                                            Tipo 
                                                                       FROM '{tablaProductos}' WHERE IDUsuario = '{FormPrincipal.userID}' AND Tipo = 'P';";
                cn.EjecutarConsulta(queryTaerStock);
                //queryTaerStock = $"UPDATE '{tablaRevisarInventario}' SET Fecha = '{DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss")}' WHERE IDUsuario = '{FormPrincipal.userID}';";
                //cn.EjecutarConsulta(queryTaerStock);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar registros de la tabla de: " + tablaProductos.ToString() + "\nhacia la tabla de: " + tablaRevisarInventario.ToString() + "\n" + ex.Message.ToString(), "Error al borrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
