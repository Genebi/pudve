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
        string SearchBarCode, queryTaerStock, queryUpdateStock, tablaProductos, tablaRevisarInventario, buscarStock;
        string cadenaClavInterna, cadenaAuxClavInterna, cadenaCodigoBarras, cadenaAuxCodigoBarras;
        string ID, Nombre, Stock, ClaveInterna, CodigoBarras, Fecha, IDUsuario;
        DataTable dtRevisarStockResultado;
        DataRow dr;
        int NoReg, index, LaPosicion, registro;
        bool IsEmpty;

        private void llenarTabla()
        {
            queryTaerStock = $"SELECT * FROM RevisarInventario";
            dtRevisarStockResultado = cn.CargarDatos(queryTaerStock);
        }

        private void cargardatos()
        {
            if (NoReg == 1)
            {
                registro = 0;
                dr = dtRevisarStockResultado.Rows[LaPosicion];
                lblNombreProducto.Text = dr["Nombre"].ToString();
                if (dr["ClaveInterna"].ToString() != "")
                {
                    lblCodigoDeBarras.Text = dr["ClaveInterna"].ToString();
                    Stock = dr["Stock"].ToString();
                } 
                else
                {
                    lblCodigoDeBarras.Text = dr["CodigoBarras"].ToString();
                    Stock = dr["Stock"].ToString();
                }
                txtCantidadStock.Text = dr["Stock"].ToString();
                registro = LaPosicion + 1;
                lblNoRegistro.Text = "Registro: " + registro + " de: " + dtRevisarStockResultado.Rows.Count;
                txtBoxBuscarCodigoBarras.Text = string.Empty;
                txtCantidadStock.Focus();
                txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
            }
            else if (NoReg == 0 && buscarStock == "")
            {
                registro = 0;
                dr = dtRevisarStockResultado.Rows[LaPosicion];
                lblNombreProducto.Text = dr["Nombre"].ToString();
                lblCodigoDeBarras.Text = buscarStock;
                txtCantidadStock.Text = dr["Stock"].ToString();
                registro = LaPosicion + 1;
                lblNoRegistro.Text = "Registro: " + registro + " de: " + dtRevisarStockResultado.Rows.Count;
                txtBoxBuscarCodigoBarras.Text = string.Empty;
                txtCantidadStock.Focus();
                txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
            }
            else if (NoReg == 0 && buscarStock != null)
            {
                txtBoxBuscarCodigoBarras.Text = string.Empty;
                txtBoxBuscarCodigoBarras.Focus();
                MessageBox.Show("Producto no encontrado.", "Error de busqueda.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBoxBuscarCodigoBarras_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                btnSiguiente.PerformClick();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (Stock != txtCantidadStock.Text)
            {
                queryUpdateStock = $"UPDATE RevisarInventario SET Stock = '{txtCantidadStock.Text}' WHERE ID = '{ID}'";
                cn.EjecutarConsulta(queryUpdateStock);
                if (LaPosicion == dtRevisarStockResultado.Rows.Count - 1)
                {
                    MessageBox.Show("Esté es el último Registro", "Último Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    LaPosicion += 1;
                    cargardatos();
                }
            }
            else if (Stock == txtCantidadStock.Text)
            {
                if (LaPosicion == dtRevisarStockResultado.Rows.Count - 1)
                {
                    MessageBox.Show("Esté es el último Registro", "Último Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    LaPosicion += 1;
                    cargardatos();
                }
            }
            //LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            NoReg = 0;
            LaPosicion = 0;
            IsEmpty = false;

            ID = string.Empty;
            Nombre = string.Empty;
            Stock = string.Empty;
            ClaveInterna = string.Empty;
            CodigoBarras = string.Empty;
            Fecha = string.Empty;
            IDUsuario = string.Empty;

            buscarStock = string.Empty;

            lblNombreProducto.Text = string.Empty;
            lblCodigoDeBarras.Text = string.Empty;
            txtCantidadStock.Text = string.Empty;
            txtCantidadStock.Text = "0";
            txtBoxBuscarCodigoBarras.Focus();
        }

        private void txtCantidadStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Para obligar a que sólo se introduzcan números
            if (Char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (Char.IsControl(e.KeyChar))  // permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;   // el resto de teclas pulsadas se desactivan
                }
            }
            if ((int)e.KeyChar == (int)Keys.Enter)
            {
                btnSiguiente.PerformClick();
                txtBoxBuscarCodigoBarras.Text = string.Empty;
                txtBoxBuscarCodigoBarras.Focus();
            }
        }

        private void RevisarInventario_FormClosing(object sender, FormClosingEventArgs e)
        {
            ClearTable(dtRevisarStockResultado);
        }

        public RevisarInventario()
        {
            InitializeComponent();
            cantidadStock = 0;
            SearchBarCode = string.Empty;
            NoReg = 0;
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
                    txtCantidadStock.Focus();
                    txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearTable(dtRevisarStockResultado);
            this.Hide();
            this.Close();
        }

        private void txtBoxBuscarCodigoBarras_TextChanged(object sender, EventArgs e)
        {
            index = 0;
            
            if (txtBoxBuscarCodigoBarras.Text != string.Empty)
            {
                buscarStock = txtBoxBuscarCodigoBarras.Text;
                try
                {
                    foreach (DataRow row in dtRevisarStockResultado.Rows)
                    {
                        cadenaClavInterna = row["ClaveInterna"].ToString();
                        cadenaAuxClavInterna = cadenaClavInterna.Replace("\r\n", " ");

                        cadenaCodigoBarras = row["CodigoBarras"].ToString();
                        cadenaAuxCodigoBarras = cadenaCodigoBarras.Replace("\r\n", " ");

                        if (cadenaAuxClavInterna.Trim() == buscarStock)
                        {
                            LaPosicion = dtRevisarStockResultado.Rows.IndexOf(row);
                            ID = row["ID"].ToString();
                            Stock = row["Stock"].ToString();
                            NoReg = 1;
                            break;
                        }
                        else if (cadenaAuxCodigoBarras.Trim() == buscarStock)
                        {
                            LaPosicion = dtRevisarStockResultado.Rows.IndexOf(row);
                            ID = row["ID"].ToString();
                            Stock = row["Stock"].ToString();
                            NoReg = 1;
                            break;
                        }
                        else
                        {
                            NoReg = 0;
                        }
                    }
                    cargardatos();
                }
                catch (DataException ex)
                {
                    txtBoxBuscarCodigoBarras.Text = string.Empty;
                    txtBoxBuscarCodigoBarras.Focus();
                    MessageBox.Show("Producto no encontrado.\nError: " + ex.Message.ToString(), "Error de busqueda.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAumentarStock_Click(object sender, EventArgs e)
        {
            cantidadStock = Convert.ToInt32(txtCantidadStock.Text);
            cantidadStock++;
            txtCantidadStock.Text = Convert.ToString(cantidadStock);
            txtCantidadStock.Focus();
            txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
        }

        private void RevisarInventario_Load(object sender, EventArgs e)
        {
            CargarStockExistente();
            llenarTabla();
            cargardatos();
            LimpiarCampos();
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
                checkEmpty(tablaRevisarInventario);
                if (IsEmpty == false)
                {
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
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al llenar registros de la tabla de: " + tablaProductos.ToString() + "\nhacia la tabla de: " + tablaRevisarInventario.ToString() + "\n" + ex.Message.ToString(), "Error al borrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (DataException ex)
            {
                MessageBox.Show("Error al Checar registros de la tabla de: " + tablaRevisarInventario.ToString() + "\n" + ex.Message.ToString(), "Error al Checar Registros", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool checkEmpty(string tabla)
        {
            string queryTableCheck = $"SELECT * FROM '{tabla}'";
            IsEmpty = cn.IsEmptyTable(queryTableCheck);
            return IsEmpty;
        }
    }
}
