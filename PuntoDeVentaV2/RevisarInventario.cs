using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class RevisarInventario : Form
    {
        Conexion cn = new Conexion();

        const string fichero = @"\PUDVE\settings\noCheckStock\checkStock.txt";  // directorio donde esta el archivo de numero de codigo de barras consecutivo
        string Contenido;                                                       // para obtener el numero que tiene el codigo de barras en el arhivo

        long NumCheckStock;

        int cantidadStock;
        string SearchBarCode, queryTaerStock, queryUpdateStock, tablaProductos, tablaRevisarInventario, buscarStock;
        string cadenaClavInterna, cadenaAuxClavInterna, cadenaCodigoBarras, cadenaAuxCodigoBarras;
        string ID, Nombre, Stock, ClaveInterna, CodigoBarras, Fecha, IDUsuario;
        DataTable dtRevisarStockResultado;
        DataRow dr;
        int NoReg, index, LaPosicion, registro, StatusRev;
        bool IsEmpty;

        private void llenarTabla()
        {
            queryTaerStock = $"SELECT * FROM RevisarInventario WHERE IDUsuario = '{FormPrincipal.userID}' AND StatusRevision = '0' AND Fecha IS NULL";
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
                    Stock = dr["StockFisico"].ToString();
                } 
                else
                {
                    lblCodigoDeBarras.Text = dr["CodigoBarras"].ToString();
                    Stock = dr["StockFisico"].ToString();
                }
                txtCantidadStock.Text = dr["StockFisico"].ToString();
                registro = LaPosicion + 1;
                //lblNoRegistro.Text = "Registro: " + registro + " de: " + dtRevisarStockResultado.Rows.Count;
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
                txtCantidadStock.Text = dr["StockFisico"].ToString();
                registro = LaPosicion + 1;
                //lblNoRegistro.Text = "Registro: " + registro + " de: " + dtRevisarStockResultado.Rows.Count;
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
            StatusRev = 1;
            if (Stock != txtCantidadStock.Text)
            {
                queryUpdateStock = $"UPDATE RevisarInventario SET StockFisico = '{txtCantidadStock.Text}', Fecha = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', StatusRevision = '{StatusRev}', StatusInventariado = '{StatusRev}' WHERE ID = '{ID}'";
                cn.EjecutarConsulta(queryUpdateStock);
                if (LaPosicion == dtRevisarStockResultado.Rows.Count - 1)
                {
                    //MessageBox.Show("Esté es el último Registro", "Último Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    LaPosicion += 1;
                }
            }
            else if (Stock == txtCantidadStock.Text)
            {
                queryUpdateStock = $"UPDATE RevisarInventario SET Fecha = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', StatusRevision = '{StatusRev}', StatusInventariado = '{StatusRev}' WHERE ID = '{ID}'";
                cn.EjecutarConsulta(queryUpdateStock);
                if (LaPosicion == dtRevisarStockResultado.Rows.Count - 1)
                {
                    //MessageBox.Show("Esté es el último Registro", "Último Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    LaPosicion += 1;
                }
            }
            llenarTabla();
            LimpiarCampos();
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
            button1.PerformClick();
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
            StatusRev = 0;
            queryUpdateStock = $"UPDATE RevisarInventario SET StatusRevision = '{StatusRev}' WHERE IDUsuario = '{FormPrincipal.userID}'";
            cn.EjecutarConsulta(queryUpdateStock);
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
                            Stock = row["StockFisico"].ToString();
                            NoReg = 1;
                            break;
                        }
                        else if (cadenaAuxCodigoBarras.Trim() == buscarStock)
                        {
                            LaPosicion = dtRevisarStockResultado.Rows.IndexOf(row);
                            ID = row["ID"].ToString();
                            Stock = row["StockFisico"].ToString();
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
            iniciarConteo();
            
        }

        private void iniciarConteo()
        {
            using (StreamReader readfile = new StreamReader(Properties.Settings.Default.rutaDirectorio + fichero))
            {
                Contenido = readfile.ReadToEnd();   // se lee todo el archivo y se almacena en la variable Contenido
            }
            if (Contenido == "")        // si el contenido es vacio 
            {
                PrimerCodBarras();      // iniciamos el conteo del codigo de barras
                AumentarCodBarras();    // Aumentamos el codigo de barras para la siguiente vez que se utilice
            }
            else if (Contenido != "")   // si el contenido no es vacio
            {
                //MessageBox.Show("Trabajando en el Proceso");
                AumentarCodBarras();    // Aumentamos el codigo de barras para la siguiente vez que se utilice
            }
        }

        private void AumentarCodBarras()
        {
            try
            {
                NumCheckStock = long.Parse(Contenido);
                NumCheckStock++;
                Contenido = NumCheckStock.ToString();

                using (StreamWriter outfile = new StreamWriter(Properties.Settings.Default.rutaDirectorio + fichero))
                {
                    outfile.WriteLine(Contenido);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar número de Revisión de Stock\nError número: " + ex.Message.ToString(), "Error Número de Revisón", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrimerCodBarras()
        {
            Contenido = "0";
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
                        queryTaerStock = $@"INSERT INTO '{tablaRevisarInventario}' (IDAlmacen, 
                                                                                    Nombre, 
                                                                                    ClaveInterna, 
                                                                                    CodigoBarras, 
                                                                                    StockAlmacen, 
                                                                                    StockFisico, 
                                                                                    IDUsuario, 
                                                                                    Tipo)
                                                                             SELECT prod.ID, 
                                                                                    prod.Nombre, 
                                                                                    prod.ClaveInterna,  
                                                                                    prod.CodigoBarras, 
                                                                                    prod.Stock, 
                                                                                    prod.Stock, 
                                                                                    prod.IDUsuario, 
                                                                                    prod.Tipo
                                                                             FROM '{tablaProductos}' prod 
                                                                             WHERE NOT EXISTS (
	                                                                             SELECT RIt.IDAlmacen, 
                                                                                        RIt.Nombre, 
                                                                                        RIt.ClaveInterna, 
                                                                                        RIt.CodigoBarras, 
                                                                                        RIt.StockAlmacen, 
                                                                                        RIt.StockFisico, 
                                                                                        RIt.IDUsuario, 
                                                                                        RIt.Tipo 
	                                                                             FROM '{tablaRevisarInventario}' RIt 
	                                                                             WHERE prod.ID = RIt.IDAlmacen
                                                                                 AND prod.Nombre = RIt.Nombre);";
                        cn.EjecutarConsulta(queryTaerStock);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al llenar registros de la tabla de: " + tablaProductos.ToString() + "\nhacia la tabla de: " + tablaRevisarInventario.ToString() + "\n" + ex.Message.ToString(), "Error al borrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    try
                    {
                        queryTaerStock = $@"INSERT INTO '{tablaRevisarInventario}' (IDAlmacen, 
                                                                                    Nombre, 
                                                                                    ClaveInterna, 
                                                                                    CodigoBarras, 
                                                                                    StockAlmacen, 
                                                                                    StockFisico, 
                                                                                    IDUsuario, 
                                                                                    Tipo)
                                                                             SELECT prod.ID, 
                                                                                    prod.Nombre, 
                                                                                    prod.ClaveInterna,  
                                                                                    prod.CodigoBarras, 
                                                                                    prod.Stock, 
                                                                                    prod.Stock, 
                                                                                    prod.IDUsuario, 
                                                                                    prod.Tipo
                                                                             FROM '{tablaProductos}' prod 
                                                                             WHERE NOT EXISTS (
	                                                                             SELECT RIt.IDAlmacen, 
                                                                                        RIt.Nombre, 
                                                                                        RIt.ClaveInterna, 
                                                                                        RIt.CodigoBarras, 
                                                                                        RIt.StockAlmacen, 
                                                                                        RIt.StockFisico, 
                                                                                        RIt.IDUsuario, 
                                                                                        RIt.Tipo 
	                                                                             FROM '{tablaRevisarInventario}' RIt 
	                                                                             WHERE prod.ID = RIt.IDAlmacen
                                                                                 AND prod.Nombre = RIt.Nombre);";
                        cn.EjecutarConsulta(queryTaerStock);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al rellenar registros de la tabla de: " + tablaProductos.ToString() + "\nhacia la tabla de: " + tablaRevisarInventario.ToString() + "\n" + ex.Message.ToString(), "Error al borrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
