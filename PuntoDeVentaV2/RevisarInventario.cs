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
        string SearchBarCode, queryTaerStock, tablaProductos, tablaRevisarInventario;

        public RevisarInventario()
        {
            InitializeComponent();
            cantidadStock = 0;
            SearchBarCode = string.Empty;
        }

        private void btnReducirStock_Click(object sender, EventArgs e)
        {
            if (lblCantidadStock.Text == "0")
            {
                MessageBox.Show("No se permite tener stock menor a = " + lblCantidadStock.Text, "Error al Disminuir Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!lblCantidadStock.Equals("0"))
            {
                cantidadStock = Convert.ToInt32(lblCantidadStock.Text);
                cantidadStock--;
                if (cantidadStock >= 0)
                {
                    lblCantidadStock.Text = Convert.ToString(cantidadStock);
                }
            }
        }

        private void btnAumentarStock_Click(object sender, EventArgs e)
        {
            cantidadStock = Convert.ToInt32(lblCantidadStock.Text);
            cantidadStock++;
            lblCantidadStock.Text = Convert.ToString(cantidadStock);
        }

        private void RevisarInventario_Load(object sender, EventArgs e)
        {
            CargarStockExistente();
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
                                                                            IDUsuario) 
                                                                     SELECT ID,
                                                                            Nombre,
                                                                            Stock,
                                                                            ClaveInterna,
                                                                            CodigoBarras,
                                                                            IDUsuario 
                                                                       FROM '{tablaProductos}' WHERE IDUsuario = '{FormPrincipal.userID}';";
                cn.EjecutarConsulta(queryTaerStock);
                queryTaerStock = $"UPDATE '{tablaRevisarInventario}' SET Fecha = '{DateTime.Now.ToString("yyyy-mm-dd hh:mm:ss")}' WHERE IDUsuario = '{FormPrincipal.userID}';";
                cn.EjecutarConsulta(queryTaerStock);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar registros de la tabla de: " + tablaProductos.ToString() + "\nhacia la tabla de: " + tablaRevisarInventario.ToString() + "\n" + ex.Message.ToString(), "Error al borrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
