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
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        const string fichero = @"\PUDVE\settings\noCheckStock\checkStock.txt";  // directorio donde esta el archivo de numero de codigo de barras consecutivo
        const string fichero1 = @"\PUDVE\settings\noCheckStock\checkDateStock.txt";

        string ContenidoNum, ContenidoDate, DateCheckStock;                                                       // para obtener el numero que tiene el codigo de barras en el arhivo
        string FechaFinal, ComprobarFecha;
        long NumCheckStock;
        public long NoActualCheckStock;
        DateTime FechaUltimaRevision, FechaActualRevision, fecha;
        int NoRevision, StatusInventariado;

        int cantidadStock;
        string SearchBarCode, queryTaerStock, queryUpdateStock, tablaProductos, tablaRevisarInventario, buscarStock;
        string cadenaClavInterna, cadenaAuxClavInterna, cadenaCodigoBarras, cadenaAuxCodigoBarras, cadenaAuxPrecioProducto;
        string ID, IDAlmacen, Nombre, StockAlmacen, Stock, ClaveInterna, CodigoBarras, Fecha, IDUsuario, NumRevInventario, FechaRegInventario, IDUser, TypeProd, StatusRevInventario, StatusInventHecho, PrecioProducto;

        DataTable dtRevisarStockResultado;
        DataRow dr;
        int NoReg, index, LaPosicion, registro, StatusRev;
        bool IsEmpty;
        string auxDate, auxCurrentDate;

        string queryNet;
        DataTable dtProductos;

        private void llenarTabla()
        {
            //queryTaerStock = $"SELECT * FROM RevisarInventario WHERE IDUsuario = '{FormPrincipal.userID}' ORDER BY Fecha DESC, Nombre ASC";
            queryTaerStock = $"SELECT * FROM RevisarInventario ORDER BY Fecha DESC, Nombre ASC";
            dtRevisarStockResultado = cn.CargarDatos(queryTaerStock);
        }

        private void cargardatos()
        {
            if (NoReg == 1)
            {
                NoRevision = 0;
                StatusInventariado = 0;
                registro = 0;
                dr = dtRevisarStockResultado.Rows[LaPosicion];
                if (dr["Tipo"].Equals("P"))
                {
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
                    lblPrecioProducto.Text = dr["PrecioProducto"].ToString();
                    txtCantidadStock.Text = dr["StockAlmacen"].ToString();
                    NoRevision = Convert.ToInt32(dr["NoRevision"].ToString());
                    ComprobarFecha = dr["Fecha"].ToString();
                    if (ComprobarFecha != "")
                    {
                        fecha = Convert.ToDateTime(dr["Fecha"].ToString());
                        auxDate = fecha.ToString();
                        ComprobarFecha = auxDate.Substring(0, 10);
                    }
                    else
                    {
                        ComprobarFecha = "";
                    }
                    StatusInventariado = Convert.ToInt32(dr["StatusInventariado"].ToString());
                    auxCurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
                    if ((NoRevision == NoActualCheckStock) && (StatusInventariado == 1))
                    {
                        DialogResult result = MessageBox.Show("Producto Inventariado\nDesea Modificarlo...", "Ya Inventariado", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            registro = LaPosicion + 1;
                            //txtBoxBuscarCodigoBarras.Text = string.Empty;
                            txtCantidadStock.Focus();
                            txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
                        }
                        else if (result == DialogResult.No)
                        {
                            LimpiarCampos();
                        }
                        else if (result == DialogResult.Cancel)
                        {
                            LimpiarCampos();
                        }
                    }
                    else
                    {
                        registro = LaPosicion + 1;
                        //txtBoxBuscarCodigoBarras.Text = string.Empty;
                        txtCantidadStock.Focus();
                        txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
                    }
                }
                else if (!dr["Tipo"].Equals("P"))
                {
                    string IDServPQ = string.Empty, ProductServPq = string.Empty;
                    DataTable dtServPQ;

                    IDServPQ = dr["IDAlmacen"].ToString();

                    using (dtServPQ = cn.CargarDatos(cs.ObtenerProductosServPaq(IDServPQ)))
                    {
                        foreach (DataRow row in dtServPQ.Rows)
                        {
                            ProductServPq += "Producto: " + row["NombreProducto"].ToString() + " Cantidad: " + row["Cantidad"].ToString() + "\n";
                        }
                    }

                    if (dr["Tipo"].Equals("PQ"))
                    {
                        MessageBox.Show("El código de barras pertenece a un Paquete\nContiene :\n" + ProductServPq, "Que tipo es Paquete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }
                    else if (dr["Tipo"].Equals("S"))
                    {
                        MessageBox.Show("El código de barras pertenece a un Servicio\nContiene :\n" + ProductServPq, "Que tipo es Servicio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

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
                    lblPrecioProducto.Text = dr["PrecioProducto"].ToString();
                    txtCantidadStock.Text = dr["StockAlmacen"].ToString();
                    NoRevision = Convert.ToInt32(dr["NoRevision"].ToString());
                    ComprobarFecha = dr["Fecha"].ToString();
                    if (ComprobarFecha != "")
                    {
                        fecha = Convert.ToDateTime(dr["Fecha"].ToString());
                        auxDate = fecha.ToString();
                        ComprobarFecha = auxDate.Substring(0, 10);
                    }
                    else
                    {
                        ComprobarFecha = "";
                    }
                    StatusInventariado = Convert.ToInt32(dr["StatusInventariado"].ToString());
                    auxCurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
                    if ((NoRevision == NoActualCheckStock) && (StatusInventariado == 1))
                    {
                        DialogResult result = MessageBox.Show("Producto Inventariado\nDesea Modificarlo...", "Ya Inventariado", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            registro = LaPosicion + 1;
                            //txtBoxBuscarCodigoBarras.Text = string.Empty;
                            txtCantidadStock.Focus();
                            txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
                        }
                        else if (result == DialogResult.No)
                        {
                            LimpiarCampos();
                        }
                        else if (result == DialogResult.Cancel)
                        {
                            LimpiarCampos();
                        }
                    }
                    else
                    {
                        registro = LaPosicion + 1;
                        //txtBoxBuscarCodigoBarras.Text = string.Empty;
                        txtCantidadStock.Focus();
                        txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
                    }
                }
            }
            else if (NoReg == 0 && buscarStock == string.Empty)
            {
                registro = 0;
                dr = dtRevisarStockResultado.Rows[LaPosicion];
                lblNombreProducto.Text = dr["Nombre"].ToString();
                lblCodigoDeBarras.Text = buscarStock;
                txtCantidadStock.Text = dr["StockFisico"].ToString();
                NoRevision = Convert.ToInt32(dr["NoRevision"].ToString());
                ComprobarFecha = dr["Fecha"].ToString();
                if (ComprobarFecha != "")
                {
                    fecha = Convert.ToDateTime(dr["Fecha"].ToString());
                    auxDate = fecha.ToString();
                    ComprobarFecha = auxDate.Substring(0, 10);
                }
                else
                {
                    ComprobarFecha = "";
                }
                StatusInventariado = Convert.ToInt32(dr["StatusInventariado"].ToString());
                auxCurrentDate = DateTime.Now.ToString("dd/MM/yyyy");
                if ((NoRevision == NoActualCheckStock) && (StatusInventariado == 1))
                {
                    DialogResult result = MessageBox.Show("Producto Inventariado\nDesea Modificarlo...", "Ya Inventariado", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        registro = LaPosicion + 1;
                        //txtBoxBuscarCodigoBarras.Text = string.Empty;
                        txtCantidadStock.Focus();
                        txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
                    }
                    else if (result == DialogResult.No)
                    {
                        LimpiarCampos();
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        LimpiarCampos();
                    }
                }
                else
                {
                    registro = LaPosicion + 1;
                    //txtBoxBuscarCodigoBarras.Text = string.Empty;
                    txtCantidadStock.Focus();
                    txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
                }
            }
            else if (NoReg == 0 && buscarStock == string.Empty)
            {
                DialogResult result = MessageBox.Show("Producto ya está inventariado\ncon fecha: " + fecha.ToString() + "\nDesea Modificarlo...", "Ya Inventariado", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    registro = LaPosicion + 1;
                    //txtBoxBuscarCodigoBarras.Text = string.Empty;
                    txtCantidadStock.Focus();
                    txtCantidadStock.Select(txtCantidadStock.Text.Length, 0);
                }
                else if (result == DialogResult.No)
                {
                    LimpiarCampos();
                }
                else if (result == DialogResult.Cancel)
                {
                    LimpiarCampos();
                }
            }
            else if (NoReg == 0 && buscarStock != string.Empty)
            {
                txtBoxBuscarCodigoBarras.Text = string.Empty;
                txtBoxBuscarCodigoBarras.Focus();
                MessageBox.Show("Producto no encontrado / deshabilitado.", "Error de busqueda.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buscarCodigoBarras()
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
                        
                        string NoRevInvent = row["NoRevision"].ToString();

                        if (cadenaAuxClavInterna.Trim() == buscarStock && NoRevInvent == NoActualCheckStock.ToString())
                        {
                            LaPosicion = dtRevisarStockResultado.Rows.IndexOf(row);
                            ID = row["ID"].ToString();
                            IDAlmacen = row["IDAlmacen"].ToString();
                            Nombre = row["Nombre"].ToString();
                            StockAlmacen = row["StockAlmacen"].ToString();
                            Stock = row["StockFisico"].ToString();
                            NumRevInventario = row["NoRevision"].ToString();
                            FechaRegInventario = row["Fecha"].ToString();
                            IDUser = row["IDUsuario"].ToString();
                            TypeProd = row["Tipo"].ToString();
                            StatusRevInventario = row["StatusRevision"].ToString();
                            StatusInventHecho = row["StatusInventariado"].ToString();
                            PrecioProducto = row["PrecioProducto"].ToString();
                            NoReg = 1;
                            break;
                        }
                        else if (cadenaAuxCodigoBarras.Trim() == buscarStock && NoRevInvent == NoActualCheckStock.ToString())
                        {
                            LaPosicion = dtRevisarStockResultado.Rows.IndexOf(row);
                            ID = row["ID"].ToString();
                            IDAlmacen = row["IDAlmacen"].ToString();
                            Nombre = row["Nombre"].ToString();
                            StockAlmacen = row["StockAlmacen"].ToString();
                            Stock = row["StockFisico"].ToString();
                            NumRevInventario = row["NoRevision"].ToString();
                            FechaRegInventario = row["Fecha"].ToString();
                            IDUser = row["IDUsuario"].ToString();
                            TypeProd = row["Tipo"].ToString();
                            StatusRevInventario = row["StatusRevision"].ToString();
                            StatusInventHecho = row["StatusInventariado"].ToString();
                            PrecioProducto = row["PrecioProducto"].ToString();
                            NoReg = 1;
                            break;
                        }
                        else if (cadenaAuxClavInterna.Trim() == buscarStock && row["NoRevision"].ToString() == "0")
                        {
                            LaPosicion = dtRevisarStockResultado.Rows.IndexOf(row);
                            ID = row["ID"].ToString();
                            IDAlmacen = row["IDAlmacen"].ToString();
                            Nombre = row["Nombre"].ToString();
                            StockAlmacen = row["StockAlmacen"].ToString();
                            Stock = row["StockFisico"].ToString();
                            NumRevInventario = row["NoRevision"].ToString();
                            FechaRegInventario = row["Fecha"].ToString();
                            IDUser = row["IDUsuario"].ToString();
                            TypeProd = row["Tipo"].ToString();
                            StatusRevInventario = row["StatusRevision"].ToString();
                            StatusInventHecho = row["StatusInventariado"].ToString();
                            PrecioProducto = row["PrecioProducto"].ToString();
                            NoReg = 1;
                            break;
                        }
                        else if (cadenaAuxCodigoBarras.Trim() == buscarStock && row["NoRevision"].ToString() == "0")
                        {
                            LaPosicion = dtRevisarStockResultado.Rows.IndexOf(row);
                            ID = row["ID"].ToString();
                            IDAlmacen = row["IDAlmacen"].ToString();
                            Nombre = row["Nombre"].ToString();
                            StockAlmacen = row["StockAlmacen"].ToString();
                            Stock = row["StockFisico"].ToString();
                            NumRevInventario = row["NoRevision"].ToString();
                            FechaRegInventario = row["Fecha"].ToString();
                            IDUser = row["IDUsuario"].ToString();
                            TypeProd = row["Tipo"].ToString();
                            StatusRevInventario = row["StatusRevision"].ToString();
                            StatusInventHecho = row["StatusInventariado"].ToString();
                            PrecioProducto = row["PrecioProducto"].ToString();
                            NoReg = 1;
                            break;
                        }
                        else if (cadenaAuxClavInterna.Trim() == buscarStock && row["NoRevision"].ToString() != "0")
                        {
                            LaPosicion = dtRevisarStockResultado.Rows.IndexOf(row);
                            ID = row["ID"].ToString();
                            IDAlmacen = row["IDAlmacen"].ToString();
                            Nombre = row["Nombre"].ToString();
                            StockAlmacen = row["StockAlmacen"].ToString();
                            Stock = row["StockFisico"].ToString();
                            NumRevInventario = row["NoRevision"].ToString();
                            FechaRegInventario = row["Fecha"].ToString();
                            IDUser = row["IDUsuario"].ToString();
                            TypeProd = row["Tipo"].ToString();
                            StatusRevInventario = row["StatusRevision"].ToString();
                            StatusInventHecho = row["StatusInventariado"].ToString();
                            PrecioProducto = row["PrecioProducto"].ToString();
                            NoReg = 1;
                            break;
                        }
                        else if (cadenaAuxCodigoBarras.Trim() == buscarStock && row["NoRevision"].ToString() != "0")
                        {
                            LaPosicion = dtRevisarStockResultado.Rows.IndexOf(row);
                            ID = row["ID"].ToString();
                            IDAlmacen = row["IDAlmacen"].ToString();
                            Nombre = row["Nombre"].ToString();
                            StockAlmacen = row["StockAlmacen"].ToString();
                            Stock = row["StockFisico"].ToString();
                            NumRevInventario = row["NoRevision"].ToString();
                            FechaRegInventario = row["Fecha"].ToString();
                            IDUser = row["IDUsuario"].ToString();
                            TypeProd = row["Tipo"].ToString();
                            StatusRevInventario = row["StatusRevision"].ToString();
                            StatusInventHecho = row["StatusInventariado"].ToString();
                            PrecioProducto = row["PrecioProducto"].ToString();
                            NoReg = 1;
                            break;
                        }
                        else
                        {
                            // Verificar si la variable busqueda es un codigo de barras y existe en la tabla CodigoBarrasExtras
                            var infoProducto = mb.BuscarCodigoBarrasExtra(buscarStock.Trim());
                            if (infoProducto.Length > 0)
                            {
                                // Verificar que el ID del producto pertenezca al usuasio
                                var verificarUsuario = cn.BuscarProducto(Convert.ToInt32(infoProducto[0]), FormPrincipal.userID);
                                // Si el producto pertenece a este usuario con el que se tiene la sesion iniciada en la consulta
                                // se busca directamente con base en su ID 
                                foreach (DataRow rowNew in dtRevisarStockResultado.Rows)
                                {
                                    string idProductoFount = rowNew["IDAlmacen"].ToString();

                                    if (idProductoFount.Trim() == verificarUsuario[0].ToString() && NoRevInvent == NoActualCheckStock.ToString())
                                    {
                                        LaPosicion = dtRevisarStockResultado.Rows.IndexOf(rowNew);
                                        ID = rowNew["ID"].ToString();
                                        IDAlmacen = rowNew["IDAlmacen"].ToString();
                                        Nombre = rowNew["Nombre"].ToString();
                                        StockAlmacen = rowNew["StockAlmacen"].ToString();
                                        Stock = rowNew["StockFisico"].ToString();
                                        NumRevInventario = rowNew["NoRevision"].ToString();
                                        FechaRegInventario = rowNew["Fecha"].ToString();
                                        IDUser = rowNew["IDUsuario"].ToString();
                                        TypeProd = rowNew["Tipo"].ToString();
                                        StatusRevInventario = rowNew["StatusRevision"].ToString();
                                        StatusInventHecho = rowNew["StatusInventariado"].ToString();
                                        PrecioProducto = rowNew["PrecioProducto"].ToString();
                                        NoReg = 1;
                                        break;
                                    }
                                    else if (idProductoFount.Trim() == verificarUsuario[0].ToString() && rowNew["NoRevision"].ToString() == "0")
                                    {
                                        LaPosicion = dtRevisarStockResultado.Rows.IndexOf(rowNew);
                                        ID = rowNew["ID"].ToString();
                                        IDAlmacen = rowNew["IDAlmacen"].ToString();
                                        Nombre = rowNew["Nombre"].ToString();
                                        StockAlmacen = rowNew["StockAlmacen"].ToString();
                                        Stock = rowNew["StockFisico"].ToString();
                                        NumRevInventario = rowNew["NoRevision"].ToString();
                                        FechaRegInventario = rowNew["Fecha"].ToString();
                                        IDUser = rowNew["IDUsuario"].ToString();
                                        TypeProd = rowNew["Tipo"].ToString();
                                        StatusRevInventario = rowNew["StatusRevision"].ToString();
                                        StatusInventHecho = rowNew["StatusInventariado"].ToString();
                                        PrecioProducto = rowNew["PrecioProducto"].ToString();
                                        NoReg = 1;
                                        break;
                                    }
                                    else if (idProductoFount.Trim() == verificarUsuario[0].ToString() && rowNew["NoRevision"].ToString() != "0")
                                    {
                                        LaPosicion = dtRevisarStockResultado.Rows.IndexOf(rowNew);
                                        ID = rowNew["ID"].ToString();
                                        IDAlmacen = rowNew["IDAlmacen"].ToString();
                                        Nombre = rowNew["Nombre"].ToString();
                                        StockAlmacen = rowNew["StockAlmacen"].ToString();
                                        Stock = rowNew["StockFisico"].ToString();
                                        NumRevInventario = rowNew["NoRevision"].ToString();
                                        FechaRegInventario = rowNew["Fecha"].ToString();
                                        IDUser = rowNew["IDUsuario"].ToString();
                                        TypeProd = rowNew["Tipo"].ToString();
                                        StatusRevInventario = rowNew["StatusRevision"].ToString();
                                        StatusInventHecho = rowNew["StatusInventariado"].ToString();
                                        PrecioProducto = rowNew["PrecioProducto"].ToString();
                                        NoReg = 1;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                NoReg = 0;
                            }
                        }
                    }
                    cargardatos();
                }
                catch (DataException ex)
                {
                    txtBoxBuscarCodigoBarras.Text = string.Empty;
                    txtBoxBuscarCodigoBarras.Focus();
                    MessageBox.Show("Al buscar el Producto paso un.\nError: " + ex.Message.ToString(), "Error de busqueda.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txtBoxBuscarCodigoBarras_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                buscarCodigoBarras();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtBoxBuscarCodigoBarras.Text))
            {
                if (!string.IsNullOrWhiteSpace(txtCantidadStock.Text))
                {
                    StatusRev = 1;
                    DataTable dtResultInsert;
                    DataRow row;
                    DateTime CurrentDate, RecordDate;
                    string ClavInterna, CodigoBarras, cadauxiliar, dia, mes, Year;

                    string consultaStock = string.Empty;

                    if (ComprobarFecha != "")
                    {
                        dia = ComprobarFecha.Substring(0, 2);
                        mes = ComprobarFecha.Substring(3, 2);
                        Year = ComprobarFecha.Substring(6, 4);
                        RecordDate = new DateTime(Convert.ToInt32(Year), Convert.ToInt32(mes), Convert.ToInt32(dia));
                    }
                    else
                    {
                        ComprobarFecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        dia = ComprobarFecha.Substring(0, 2);
                        mes = ComprobarFecha.Substring(3, 2);
                        Year = ComprobarFecha.Substring(6, 4);
                        RecordDate = new DateTime(Convert.ToInt32(Year), Convert.ToInt32(mes), Convert.ToInt32(dia));
                    }

                    CurrentDate = DateTime.Now.Date;

                    if (Stock != txtCantidadStock.Text)
                    {
                        if ((NoRevision == NoActualCheckStock) && (StatusInventariado != 0))
                        {
                            queryUpdateStock = $"UPDATE RevisarInventario SET StockFisico = '{txtCantidadStock.Text}', Fecha = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', StatusRevision = '{StatusRev}', StatusInventariado = '{StatusRev}', NoRevision = '{NoActualCheckStock}' WHERE ID = '{ID}'";

                            var info = mb.ObtenerRevisionInventario(Convert.ToInt32(ID), FormPrincipal.userID);

                            consultaStock = $"UPDATE Productos SET Stock = '{txtCantidadStock.Text}' WHERE ID = {info[0]} AND IDUsuario = {FormPrincipal.userID}";
                        }
                        else if ((NoRevision != NoActualCheckStock) && (StatusInventariado != 0))
                        {
                            queryUpdateStock = $@"INSERT INTO RevisarInventario (IDAlmacen, 
                                                                         Nombre, 
                                                                         ClaveInterna, 
                                                                         CodigoBarras, 
                                                                         StockAlmacen, 
                                                                         StockFisico, 
                                                                         NoRevision, 
                                                                         Fecha, 
                                                                         IDUsuario, 
                                                                         Tipo, 
                                                                         StatusRevision, 
                                                                         StatusInventariado)
                                                                 VALUES ('{IDAlmacen}',
                                                                         '{Nombre}',
                                                                         '{cadenaAuxClavInterna}',
                                                                         '{cadenaAuxCodigoBarras}',
                                                                         '{StockAlmacen}',
                                                                         '{txtCantidadStock.Text}',
                                                                         '{NoActualCheckStock}',
                                                                         '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',
                                                                         '{IDUser}',
                                                                         '{TypeProd}',
                                                                         '{StatusRevInventario}',
                                                                         '{StatusInventHecho}')";

                            consultaStock = $"UPDATE Productos SET Stock = '{txtCantidadStock.Text}' WHERE ID = {IDAlmacen} AND IDUsuario = {FormPrincipal.userID}";
                        }
                        else if ((NoRevision == 0) && (StatusInventariado == 0))
                        {
                            queryUpdateStock = $"UPDATE RevisarInventario SET StockFisico = '{txtCantidadStock.Text}', Fecha = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', StatusRevision = '{StatusRev}', StatusInventariado = '{StatusRev}', NoRevision = '{NoActualCheckStock}' WHERE ID = '{ID}'";

                            var info = mb.ObtenerRevisionInventario(Convert.ToInt32(ID), FormPrincipal.userID);

                            consultaStock = $"UPDATE Productos SET Stock = '{txtCantidadStock.Text}' WHERE ID = {info[0]} AND IDUsuario = {FormPrincipal.userID}";
                        }
                        else
                        {
                            queryUpdateStock = $@"UPDATE RevisarInventario SET IDAlmacen = '{IDAlmacen}', 
                                                                       Nombre = '{Nombre}', 
                                                                       ClaveInterna = '{cadenaAuxClavInterna}', 
                                                                       CodigoBarras = '{cadenaAuxCodigoBarras}', 
                                                                       StockAlmacen = '{StockAlmacen}', 
                                                                       StockFisico = '{Stock}', 
                                                                       NoRevision = '{NoActualCheckStock}', 
                                                                       Fecha = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 
                                                                       IDUsuario = '{IDUser}', 
                                                                       Tipo = '{TypeProd}', 
                                                                       StatusRevision = '{StatusRevInventario}', 
                                                                       StatusInventariado = '{StatusInventHecho}'
                                                                 WHERE ID = '{ID}'";

                            consultaStock = $"UPDATE Productos SET Stock = '{Stock}' WHERE ID = {IDAlmacen} AND IDUsuario = {FormPrincipal.userID}";
                        }

                        cn.EjecutarConsulta(queryUpdateStock);
                        cn.EjecutarConsulta(consultaStock);

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
                        if ((NoRevision == NoActualCheckStock) && (StatusInventariado != 0))
                        {
                            queryUpdateStock = $"UPDATE RevisarInventario SET Fecha = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', StatusRevision = '{StatusRev}', StatusInventariado = '{StatusRev}', NoRevision = '{NoActualCheckStock}' WHERE ID = '{ID}'";

                            consultaStock = string.Empty;
                        }
                        else if ((NoRevision != NoActualCheckStock) && (StatusInventariado != 0))
                        {
                            queryUpdateStock = $@"INSERT INTO RevisarInventario (IDAlmacen, 
                                                                         Nombre, 
                                                                         ClaveInterna, 
                                                                         CodigoBarras, 
                                                                         StockAlmacen, 
                                                                         StockFisico, 
                                                                         NoRevision, 
                                                                         Fecha, 
                                                                         IDUsuario, 
                                                                         Tipo, 
                                                                         StatusRevision, 
                                                                         StatusInventariado)
                                                                 VALUES ('{IDAlmacen}',
                                                                         '{Nombre}',
                                                                         '{cadenaAuxClavInterna}',
                                                                         '{cadenaAuxCodigoBarras}',
                                                                         '{StockAlmacen}',
                                                                         '{txtCantidadStock.Text}',
                                                                         '{NoActualCheckStock}',
                                                                         '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}',
                                                                         '{IDUser}',
                                                                         '{TypeProd}',
                                                                         '{StatusRevInventario}',
                                                                         '{StatusInventHecho}')";

                            consultaStock = $"UPDATE Productos SET Stock = '{txtCantidadStock.Text}' WHERE ID = {IDAlmacen} AND IDUsuario = {FormPrincipal.userID}";
                        }
                        else if ((NoRevision == 0) && (StatusInventariado == 0))
                        {
                            queryUpdateStock = $"UPDATE RevisarInventario SET Fecha = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', StatusRevision = '{StatusRev}', StatusInventariado = '{StatusRev}', NoRevision = '{NoActualCheckStock}' WHERE ID = '{ID}'";

                            consultaStock = string.Empty;
                        }
                        else
                        {
                            queryUpdateStock = $@"UPDATE RevisarInventario SET IDAlmacen = '{IDAlmacen}', 
                                                                       Nombre = '{Nombre}', 
                                                                       ClaveInterna = '{cadenaAuxClavInterna}', 
                                                                       CodigoBarras = '{cadenaAuxCodigoBarras}', 
                                                                       StockAlmacen = '{StockAlmacen}', 
                                                                       StockFisico = '{Stock}', 
                                                                       NoRevision = '{NoActualCheckStock}', 
                                                                       Fecha = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', 
                                                                       IDUsuario = '{IDUser}', 
                                                                       Tipo = '{TypeProd}', 
                                                                       StatusRevision = '{StatusRevInventario}', 
                                                                       StatusInventariado = '{StatusRev}'
                                                                 WHERE ID = '{ID}'";

                            consultaStock = $"UPDATE Productos SET Stock = '{Stock}' WHERE ID = {IDAlmacen} AND IDUsuario = {FormPrincipal.userID}";
                        }

                        cn.EjecutarConsulta(queryUpdateStock);

                        if (!string.IsNullOrEmpty(consultaStock))
                        {
                            cn.EjecutarConsulta(consultaStock);
                        }

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
            }
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
            txtBoxBuscarCodigoBarras.Text = string.Empty;
            txtCantidadStock.Text = "0";
            txtBoxBuscarCodigoBarras.Focus();
        }

        private void txtCantidadStock_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Para obligar a que sólo se introduzcan números
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            else
            {
                if (char.IsControl(e.KeyChar))  // permitir teclas de control como retroceso
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;   // el resto de teclas pulsadas se desactivan
                }
            }
        }

        private void txtCantidadStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                if (!string.IsNullOrWhiteSpace(txtBoxBuscarCodigoBarras.Text))
                {
                    if (!string.IsNullOrWhiteSpace(txtCantidadStock.Text))
                    {
                        btnSiguiente.PerformClick();
                        txtBoxBuscarCodigoBarras.Text = string.Empty;
                        txtBoxBuscarCodigoBarras.Focus();
                    }
                }
            }
        }

        private void RevisarInventario_FormClosing(object sender, FormClosingEventArgs e)
        {
            RiniciarStatusRevision();
            this.Hide();
        }

        public RevisarInventario()
        {
            InitializeComponent();
            cantidadStock = 0;
            SearchBarCode = string.Empty;
            NoReg = -1;
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
            RiniciarStatusRevision();
            Properties.Settings.Default.InicioFinInventario = 2;
            Properties.Settings.Default.Save();                 // Guardamos los dos Datos de las variables del sistema
            this.Hide();
            this.Close();
        }

        private void RiniciarStatusRevision()
        {
            StatusRev = 0;
            queryUpdateStock = $"UPDATE RevisarInventario SET StatusRevision = '{StatusRev}' WHERE IDUsuario = '{FormPrincipal.userID}'";
            cn.EjecutarConsulta(queryUpdateStock);
            ClearTable(dtRevisarStockResultado);
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
            lblNoRevision.Text = NoActualCheckStock.ToString();
        }

        private void iniciarConteo()
        {
            int Conteo = 0;
            // Inicio de guardar fecha de conteo
            using (StreamReader readfile = new StreamReader(Properties.Settings.Default.rutaDirectorio + fichero1))
            {
                ContenidoDate = readfile.ReadToEnd();   // se lee todo el archivo y se almacena en la variable Fecha
            }
            if (ContenidoDate != "")   // si el contenido no es vacio
            {
                FechaFinal = ContenidoDate.Replace("\r\n", "");
                DateCheckStock = ContenidoDate;
            }
            else if (ContenidoDate == "")
            {
                ContenidoDate = DateTime.Now.ToString("yyyy-MM-dd");
                DateCheckStock = ContenidoDate;
                using (StreamWriter outfile = new StreamWriter(Properties.Settings.Default.rutaDirectorio + fichero1))
                {
                    outfile.WriteLine(ContenidoDate);
                }
            }

            Conteo = Properties.Settings.Default.InicioFinInventario;

            // Inicio de cambiar numero de conteo
            using (StreamReader readfile = new StreamReader(Properties.Settings.Default.rutaDirectorio + fichero))
            {
                ContenidoNum = readfile.ReadToEnd();   // se lee todo el archivo y se almacena en la variable Contenido
            }
            if (ContenidoNum == "" && Conteo == 1)        // si el contenido es vacio 
            {
                PrimerNumConteo();      // iniciamos el conteo del codigo de barras
                AumentarNumConteo();    // Aumentamos el codigo de barras para la siguiente vez que se utilice
            }
            else if (ContenidoNum != "" && Conteo == 1)
            {
                NoActualCheckStock = long.Parse(ContenidoNum);
            }
            else if (ContenidoNum != "" && Conteo == 2)   // si el contenido no es vacio
            {
                FechaUltimaRevision = Convert.ToDateTime(DateCheckStock).Date;
                FechaActualRevision = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
                AumentarNumConteo();    // Aumentamos el codigo de barras para la siguiente vez que se utilice
                ContenidoNum = DateTime.Now.ToString("yyyy-MM-dd");
                DateCheckStock = ContenidoNum;
                using (StreamWriter outfile = new StreamWriter(Properties.Settings.Default.rutaDirectorio + fichero1))
                {
                    outfile.WriteLine(ContenidoNum);
                }
                Properties.Settings.Default.InicioFinInventario = 1;
                Properties.Settings.Default.Save();                 // Guardamos los dos Datos de las variables del sistema
                Conteo = Properties.Settings.Default.InicioFinInventario;
                using (StreamReader readfile = new StreamReader(Properties.Settings.Default.rutaDirectorio + fichero))
                {
                    ContenidoNum = readfile.ReadToEnd();   // se lee todo el archivo y se almacena en la variable Contenido
                }
                NoActualCheckStock = long.Parse(ContenidoNum);
            }
        }

        private void AumentarNumConteo()
        {
            try
            {
                NumCheckStock = long.Parse(ContenidoNum);
                NumCheckStock++;
                NoActualCheckStock = NumCheckStock;
                ContenidoNum = NumCheckStock.ToString();

                NoActualCheckStock = long.Parse(ContenidoNum);

                using (StreamWriter outfile = new StreamWriter(Properties.Settings.Default.rutaDirectorio + fichero))
                {
                    outfile.WriteLine(ContenidoNum);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar número de Revisión de Stock\nError número: " + ex.Message.ToString(), "Error Número de Revisón", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrimerNumConteo()
        {
            ContenidoNum = "0";
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
                                                                                    Tipo,
                                                                                    PrecioProducto)
                                                                             SELECT prod.ID, 
                                                                                    prod.Nombre, 
                                                                                    prod.ClaveInterna,  
                                                                                    prod.CodigoBarras, 
                                                                                    prod.Stock, 
                                                                                    prod.Stock, 
                                                                                    prod.IDUsuario, 
                                                                                    prod.Tipo,
                                                                                    prod.Precio
                                                                             FROM '{tablaProductos}' prod 
                                                                             WHERE prod.IDUsuario = {FormPrincipal.userID} AND prod.Status = '1' AND NOT EXISTS (
	                                                                             SELECT RIt.IDAlmacen, 
                                                                                        RIt.Nombre, 
                                                                                        RIt.ClaveInterna, 
                                                                                        RIt.CodigoBarras, 
                                                                                        RIt.StockAlmacen, 
                                                                                        RIt.StockFisico, 
                                                                                        RIt.IDUsuario, 
                                                                                        RIt.Tipo,
                                                                                        RIt.PrecioProducto
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
                        // Se eliminan los registros de la tabla antes de recargar de nuevo la tabla con
                        // los productos actualizados
                        cn.EjecutarConsulta($"DELETE FROM {tablaRevisarInventario}");

                        queryTaerStock = $@"INSERT INTO '{tablaRevisarInventario}' (IDAlmacen, 
                                                                                    Nombre, 
                                                                                    ClaveInterna, 
                                                                                    CodigoBarras, 
                                                                                    StockAlmacen, 
                                                                                    StockFisico, 
                                                                                    IDUsuario, 
                                                                                    Tipo,
                                                                                    PrecioProducto)
                                                                             SELECT prod.ID, 
                                                                                    prod.Nombre, 
                                                                                    prod.ClaveInterna,  
                                                                                    prod.CodigoBarras, 
                                                                                    prod.Stock, 
                                                                                    prod.Stock, 
                                                                                    prod.IDUsuario, 
                                                                                    prod.Tipo,
                                                                                    prod.Precio
                                                                             FROM '{tablaProductos}' prod 
                                                                             WHERE prod.IDUsuario = {FormPrincipal.userID} AND prod.Status = '1' AND NOT EXISTS (
	                                                                             SELECT RIt.IDAlmacen, 
                                                                                        RIt.Nombre, 
                                                                                        RIt.ClaveInterna, 
                                                                                        RIt.CodigoBarras, 
                                                                                        RIt.StockAlmacen, 
                                                                                        RIt.StockFisico, 
                                                                                        RIt.IDUsuario, 
                                                                                        RIt.Tipo,
                                                                                        RIt.PrecioProducto 
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
            string queryTableCheck = $"SELECT * FROM '{tabla}' WHERE IDUsuario = {FormPrincipal.userID}";
            IsEmpty = cn.IsEmptyTable(queryTableCheck);
            return IsEmpty;
        }
    }
}
