using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    public partial class Ventas : Form
    {
        string[] productos;
        float porcentajeGeneral = 0;
        bool ventaGuardada = false; //Para saber si la venta se guardo o no

        public static int indiceFila = 0; //Para guardar el indice de la fila cuando se elige agregar multiples productos
        public static int cantidadFila = 0; //Para guardar la cantidad de productos que se agregará a la fila correspondiente

        //Para las ventas guardadas
        public static int mostrarVenta = 0;

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        NameValueCollection datos;

        public Ventas()
        {
            InitializeComponent();

            CargarProductosServicios();
        }

        public void CargarProductosServicios()
        {
            AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            datos = new NameValueCollection();
            //productos = new string[] { };

            //Cargar lista de productos actuales
            datos = cn.ObtenerProductos(FormPrincipal.userID);
            productos = new string[datos.Count];
            datos.CopyTo(productos, 0);

            coleccion.AddRange(productos);
            txtBuscadorProducto.AutoCompleteCustomSource = coleccion;
            txtBuscadorProducto.AutoCompleteMode = AutoCompleteMode.None;
            txtBuscadorProducto.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            tituloSeccion.Focus();
            txtBuscadorProducto.GotFocus  += new EventHandler(BuscarTieneFoco);
            txtBuscadorProducto.LostFocus += new EventHandler(BuscarPierdeFoco);
            txtDescuentoGeneral.GotFocus  += new EventHandler(DescuentoTieneFoco);
            txtDescuentoGeneral.LostFocus += new EventHandler(DescuentoPierdeFoco);

            cbEstadoVenta.SelectedIndex = 0;
            cbEstadoVenta.DropDownStyle = ComboBoxStyle.DropDownList;

            btnProductoRapido.BackgroundImage = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\plus.png");
            btnServicioRapido.BackgroundImage = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\plus.png");
            btnEliminarUltimo.BackgroundImage = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\trash.png");
            btnEliminarTodos.BackgroundImage  = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\trash.png");
            btnUltimoTicket.BackgroundImage   = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\ticket.png");
            btnPresupuesto.BackgroundImage    = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\money.png");

            btnProductoRapido.BackgroundImageLayout = ImageLayout.Center;
            btnServicioRapido.BackgroundImageLayout = ImageLayout.Center;
            btnEliminarUltimo.BackgroundImageLayout = ImageLayout.Center;
            btnEliminarTodos.BackgroundImageLayout  = ImageLayout.Center;
            btnUltimoTicket.BackgroundImageLayout   = ImageLayout.Center;
            btnPresupuesto.BackgroundImageLayout    = ImageLayout.Center;
        }

        private void BuscarTieneFoco(object sender, EventArgs e)
        {
            if (txtBuscadorProducto.Text == "buscar producto...")
            {
                txtBuscadorProducto.Text = "";
            }
        }

        private void BuscarPierdeFoco(object sender, EventArgs e)
        {
            if (txtBuscadorProducto.Text == "")
            {
                txtBuscadorProducto.Text = "buscar producto...";
            }
        }

        private void DescuentoTieneFoco(object sender, EventArgs e)
        {
            if (txtDescuentoGeneral.Text == "% descuento")
            {
                txtDescuentoGeneral.Text = "";
            }
        }

        private void DescuentoPierdeFoco(object sender, EventArgs e)
        {
            if (txtDescuentoGeneral.Text == "")
            {
                txtDescuentoGeneral.Text = "% descuento";
            }
        }

        private void txtBuscadorProducto_TextChanged(object sender, EventArgs e)
        {
            listaProductos.Items.Clear();

            if (txtBuscadorProducto.Text.Length == 0)
            {
                ocultarResultados();
                return;
            }

            foreach (string s in txtBuscadorProducto.AutoCompleteCustomSource)
            {
                if (s.Contains(txtBuscadorProducto.Text))
                {
                    listaProductos.Items.Add(s);
                    listaProductos.Visible = true;
                }
            }
        }

        private void listaProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listaProductos.SelectedIndex > -1)
            {
                
                //Se obtiene el texto del item seleccionado del ListBox
                string producto = listaProductos.Items[listaProductos.SelectedIndex].ToString();

                //Se obtiene el indice del array donde se encuentra el producto seleccionado
                int idProducto = Convert.ToInt32(datos.GetKey(Array.IndexOf(productos, producto)));

                string[] datosProducto = cn.BuscarProducto(idProducto, FormPrincipal.userID);

                txtBuscadorProducto.Text = "";
                txtBuscadorProducto.Focus();
                ocultarResultados();

                AgregarProducto(datosProducto);
            }
        }

        private void listaProductos_LostFocus(object sender, EventArgs e)
        {
            ocultarResultados();
        }

        private void ocultarResultados()
        {
            listaProductos.Visible = false;
        }

        private void txtBuscadorProducto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
            {
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Down)
            {
                listaProductos.Focus();
            }
        }

        private void AgregarProducto(string[] datosProducto)
        {
            if (DGVentas.Rows.Count > 0)
            {
                bool existe = false;

                foreach (DataGridViewRow fila in DGVentas.Rows)
                {
                    //Compara el valor de la celda con el nombre del producto (Descripcion)
                    if (fila.Cells["Descripcion"].Value.Equals(datosProducto[1]))
                    {
                        int cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value) + 1;
                        float importe = cantidad * float.Parse(fila.Cells["Precio"].Value.ToString());

                        fila.Cells["Cantidad"].Value = cantidad;
                        fila.Cells["Importe"].Value = importe;
                        existe = true;

                        int idProducto = Convert.ToInt32(datosProducto[0]);
                        int tipoDescuento = Convert.ToInt32(datosProducto[3]);

                        if (tipoDescuento > 0)
                        {
                            string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                            CalcularDescuento(datosDescuento, tipoDescuento, cantidad);
                        }
                    }
                }

                if (!existe)
                {
                    AgregarProductoLista(datosProducto);
                }
            }
            else
            {
                AgregarProductoLista(datosProducto);
            }

            CantidadesFinalesVenta();
        }

        private void AgregarProductoLista(string[] datosProducto, int cantidad = 1)
        {
            //Se agrega la nueva fila y se obtiene el ID que tendrá
            int rowId = DGVentas.Rows.Add();

            //Obtener la nueva fila
            DataGridViewRow row = DGVentas.Rows[rowId];

            //Agregamos la información
            row.Cells["IDProducto"].Value = datosProducto[0]; //Este campo no es visible
            row.Cells["PrecioOriginal"].Value = datosProducto[2]; //Este campo no es visible
            row.Cells["DescuentoTipo"].Value = datosProducto[3]; //Este campo tampoco es visible
            row.Cells["Stock"].Value = datosProducto[4]; //Este campo no es visible
            row.Cells["Cantidad"].Value = cantidad;
            row.Cells["Precio"].Value = datosProducto[2];
            row.Cells["Descripcion"].Value = datosProducto[1];
            row.Cells["Descuento"].Value = 0;
            row.Cells["Importe"].Value = datosProducto[2];

            Image img1 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\plus-square.png");
            Image img2 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\plus.png");
            Image img3 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\minus.png");
            Image img4 = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\icon\black16\remove.png");

            DGVentas.Rows[rowId].Cells["AgregarMultiple"].Value = img1;
            DGVentas.Rows[rowId].Cells["AgregarIndividual"].Value = img2;
            DGVentas.Rows[rowId].Cells["RestarIndividual"].Value = img3;
            DGVentas.Rows[rowId].Cells["EliminarIndividual"].Value = img4;
        }

        private void DGVentas_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            //Iconos operaciones por producto
            if (e.ColumnIndex >= 5)
            {
                DGVentas.Cursor = Cursors.Hand;
            }
            else
            {
                DGVentas.Cursor = Cursors.Default;
            }
        }

        private void DGVentas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var celda = DGVentas.CurrentCell.RowIndex;

            //Agregar multiple
            if (e.ColumnIndex == 9)
            {
                indiceFila = e.RowIndex;

                AgregarMultiplesProductos agregarMultiple = new AgregarMultiplesProductos();

                agregarMultiple.FormClosed += delegate
                {
                    AgregarMultiplesProductos();
                    agregarMultiple.Dispose();
                };

                agregarMultiple.ShowDialog();
            }

            //Agregar individual
            if (e.ColumnIndex == 10)
            {
                int cantidad  = Convert.ToInt32(DGVentas.Rows[celda].Cells["Cantidad"].Value) + 1;
                float importe = cantidad * float.Parse(DGVentas.Rows[celda].Cells["Precio"].Value.ToString());

                DGVentas.Rows[celda].Cells["Cantidad"].Value = cantidad;
                DGVentas.Rows[celda].Cells["Importe"].Value = importe;

                int idProducto = Convert.ToInt32(DGVentas.Rows[celda].Cells["IDProducto"].Value);
                int tipoDescuento = Convert.ToInt32(DGVentas.Rows[celda].Cells["DescuentoTipo"].Value);

                if (tipoDescuento > 0)
                {
                    string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                    CalcularDescuento(datosDescuento, tipoDescuento, cantidad, celda);
                }
            }

            //Restar individual
            if (e.ColumnIndex == 11)
            {
                int cantidad = Convert.ToInt32(DGVentas.Rows[celda].Cells["Cantidad"].Value);

                if (cantidad > 0)
                {
                    cantidad -= 1;
                    float importe = cantidad * float.Parse(DGVentas.Rows[celda].Cells["Precio"].Value.ToString());

                    DGVentas.Rows[celda].Cells["Cantidad"].Value = cantidad;
                    DGVentas.Rows[celda].Cells["Importe"].Value = importe;

                    int idProducto = Convert.ToInt32(DGVentas.Rows[celda].Cells["IDProducto"].Value);
                    int tipoDescuento = Convert.ToInt32(DGVentas.Rows[celda].Cells["DescuentoTipo"].Value);

                    if (tipoDescuento > 0)
                    {
                        string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                        CalcularDescuento(datosDescuento, tipoDescuento, cantidad, celda);
                    }
                }
            }

            //Eliminar individual
            if (e.ColumnIndex == 12)
            {
                DGVentas.Rows.RemoveAt(celda);
            }

            DGVentas.ClearSelection();
            CantidadesFinalesVenta();
        }

        private void AgregarMultiplesProductos()
        {
            int cantidad = Convert.ToInt32(DGVentas.Rows[indiceFila].Cells["Cantidad"].Value) + cantidadFila;
            float importe = cantidad * float.Parse(DGVentas.Rows[indiceFila].Cells["Precio"].Value.ToString());

            DGVentas.Rows[indiceFila].Cells["Cantidad"].Value = cantidad;
            DGVentas.Rows[indiceFila].Cells["Importe"].Value = importe;

            int idProducto = Convert.ToInt32(DGVentas.Rows[indiceFila].Cells["IDProducto"].Value);
            int tipoDescuento = Convert.ToInt32(DGVentas.Rows[indiceFila].Cells["DescuentoTipo"].Value);

            if (tipoDescuento > 0)
            {
                string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);
                CalcularDescuento(datosDescuento, tipoDescuento, cantidad, indiceFila);
            }
            
            indiceFila = 0;
            cantidadFila = 0;   
        }

        private void CalcularDescuento(string[] datosDescuento, int tipo, int cantidad, int fila = 0)
        {
            //Cliente
            if (tipo == 1)
            {

            }

            //Mayoreo
            if (tipo == 2)
            {
                string[] tmp;
                int index = 0;
                int checkbox = 0;
                int sobrantes = 0;
                float totalImporte = 0;

                //Comprobar si hay al menos un checkbox seleccionado en este descuento
                foreach (string descuento in datosDescuento)
                {
                    string[] cb = descuento.Split('-');
                    
                    if (Convert.ToInt32(cb[3]) == 1)
                    {
                        checkbox = 1;
                        break;
                    }
                }


                int longitud = datosDescuento.Length;

                for (int i = 0; i < longitud; i++)
                {
                    string[] desc = datosDescuento[i].Split('-');

                    var rangoInicial = Convert.ToInt32(desc[0]);
                    var rangoFinal = desc[1];
                    var precio = float.Parse(desc[2]);
  
                    //Entra cuando se establecen precios en base en los checkbox
                    if (checkbox == 1)
                    {
                        //Este es para saber si es el ultimo descuento que se agrego (registro en la BD)
                        if (rangoFinal == "N")
                        {
                            if (cantidad > rangoInicial)
                            {
                                index = i + 1;

                                if (index >= 0 && index < longitud)
                                {
                                    //Obtener el rango final del descuento previo al actual
                                    tmp = datosDescuento[index].Split('-');
                                    var rangoFinalTmp = tmp[1];

                                    sobrantes = cantidad - Convert.ToInt32(rangoFinalTmp);

                                    totalImporte += sobrantes * precio;

                                    cantidad -= sobrantes;
                                }
                            }
                            else
                            {
                                if (cantidad == rangoInicial)
                                {
                                    sobrantes = rangoInicial - 1;
                                    sobrantes = rangoInicial - sobrantes;

                                    cantidad -= sobrantes;

                                    totalImporte += sobrantes * precio;
                                }
                            }
                        }
                        else
                        {
                            if (cantidad >= rangoInicial && cantidad <= Convert.ToInt32(rangoFinal))
                            {
                                index = i + 1;

                                //Para saber si existe el indice en el Array principal
                                if (index >= 0 && index < longitud)
                                {
                                    //Obtener el rango final del descuento previo al actual
                                    tmp = datosDescuento[index].Split('-');
                                    var rangoFinalTmp = tmp[1];

                                    sobrantes = cantidad - Convert.ToInt32(rangoFinalTmp);

                                    cantidad -= sobrantes;

                                    totalImporte += sobrantes * precio;
                                }
                                else
                                {
                                    totalImporte += cantidad * precio;
                                }
                            }
                            else
                            {
                                if (cantidad > Convert.ToInt32(rangoFinal))
                                {
                                    sobrantes = cantidad - Convert.ToInt32(rangoFinal);

                                    totalImporte += sobrantes * precio;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (rangoFinal == "N")
                        {
                            if (cantidad >= rangoInicial)
                            {
                                totalImporte = cantidad * precio;
                            }
                        }
                        else
                        {
                            if (cantidad >= rangoInicial && cantidad <= Convert.ToInt32(rangoFinal))
                            {
                                totalImporte = cantidad * precio;
                            }
                        }
                    }
                }
                
                float importe = float.Parse(DGVentas.Rows[fila].Cells["Importe"].Value.ToString());
                float descuentoFinal = importe - totalImporte;

                DGVentas.Rows[fila].Cells["Descuento"].Value = descuentoFinal;
                DGVentas.Rows[fila].Cells["Importe"].Value = totalImporte;
            }
        }

        private void CantidadesFinalesVenta()
        {
            int    totalArticulos = 0;
            double totalImporte   = 0;
            double totalDescuento = 0;
            double totalSubtotal  = 0;
            double totalIVA16     = 0;

            foreach (DataGridViewRow fila in DGVentas.Rows)
            {
                if (porcentajeGeneral > 0)
                {
                    var precioOriginal = Convert.ToDouble(fila.Cells["PrecioOriginal"].Value); //Precio original del producto
                    var cantidadProducto = Convert.ToInt32(fila.Cells["Cantidad"].Value); //Cantidad de producto
                    var cantidadDescuento = Convert.ToDouble(fila.Cells["Descuento"].Value); //Cantidad descuento del producto

                    var descuento = (precioOriginal * cantidadProducto) - cantidadDescuento;
                    descuento *= porcentajeGeneral;

                    var importeProducto = precioOriginal * cantidadProducto;
                    importeProducto -= descuento;
                    importeProducto -= cantidadDescuento;

                    fila.Cells["Importe"].Value = importeProducto.ToString("0.00");

                    totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
                    totalArticulos += cantidadProducto;
                    totalDescuento += descuento + cantidadDescuento;
                }
                else
                {
                    var precioOriginal = Convert.ToDouble(fila.Cells["PrecioOriginal"].Value);
                    var cantidadProducto = Convert.ToInt32(fila.Cells["Cantidad"].Value);
                    var cantidadDescuento = Convert.ToDouble(fila.Cells["Descuento"].Value);

                    var importeProducto = (precioOriginal * cantidadProducto) - cantidadDescuento;

                    fila.Cells["Importe"].Value = importeProducto.ToString("0.00");

                    totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
                    totalArticulos += cantidadProducto;
                    totalDescuento += cantidadDescuento;
                }
            }

            totalSubtotal = totalImporte / 1.16;
            totalIVA16    = totalSubtotal * 0.16;

            cIVA.Text = totalIVA16.ToString("0.00");
            cTotal.Text = totalImporte.ToString("0.00");
            cSubtotal.Text = totalSubtotal.ToString("0.00");
            cDescuento.Text = totalDescuento.ToString("0.00");
            cNumeroArticulos.Text = totalArticulos.ToString();
        }

        private void btnEliminarUltimo_Click(object sender, EventArgs e)
        {
            if (DGVentas.Rows.Count > 0)
            {
                DGVentas.Rows.RemoveAt(DGVentas.Rows.Count - 1);
                CantidadesFinalesVenta();
            }
        }

        private void btnEliminarTodos_Click(object sender, EventArgs e)
        {
            DGVentas.Rows.Clear();
            CantidadesFinalesVenta();
        }

        private void btnCancelarVenta_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnTerminarVenta_Click(object sender, EventArgs e)
        {
            //Datos generales de la venta
            var IdEmpresa = FormPrincipal.userID.ToString();
            var Subtotal = cSubtotal.Text;
            var IVA16 = cIVA.Text;
            var Descuento = cDescuento.Text;
            var Total = cTotal.Text;
            var DescuentoGeneral = porcentajeGeneral.ToString("0.00");
            var Status = "1";
            var FechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (ventaGuardada) { Status = "2"; }

            string[] guardar = new string[] { IdEmpresa, IdEmpresa, Subtotal, IVA16, Total, Descuento, DescuentoGeneral, Status, FechaOperacion };

            if (VerificarStockProducto())
            {
                //Se hace el guardado de la informacion general de la venta
                int respuesta = cn.EjecutarConsulta(cs.GuardarVenta(guardar, mostrarVenta));
                
                if (respuesta > 0)
                {
                    //Obtener ID de la venta
                    string idVenta = cn.EjecutarSelect("SELECT ID FROM Ventas ORDER BY ID DESC LIMIT 1", 1).ToString();

                    //Si mostrarVenta contine un valor mayor a cero quiere decir que es una venta guardada con la que se esta trabajando
                    if (mostrarVenta > 0)
                    {
                        idVenta = mostrarVenta.ToString();

                        cn.EjecutarConsulta(cs.EliminarProductosVenta(Convert.ToInt32(idVenta)));
                    }

                    //Datos de los productos vendidos
                    foreach (DataGridViewRow fila in DGVentas.Rows)
                    {
                        var IDProducto = fila.Cells["IDProducto"].Value.ToString();
                        var Nombre = fila.Cells["Descripcion"].Value.ToString();
                        var Cantidad = fila.Cells["Cantidad"].Value.ToString();
                        var Precio = fila.Cells["Precio"].Value.ToString();

                        guardar = new string[] { idVenta, IDProducto, Nombre, Cantidad, Precio };

                        cn.EjecutarConsulta(cs.GuardarProductosVenta(guardar));

                        if (!ventaGuardada)
                        {
                            //Actualizar stock de productos
                            var stock = Convert.ToInt32(fila.Cells["Stock"].Value);
                            var vendidos = Convert.ToInt32(fila.Cells["Cantidad"].Value);
                            var restantes = (stock - vendidos).ToString();

                            guardar = new string[] { IDProducto, restantes };

                            cn.EjecutarConsulta(cs.ActualizarStockProductos(guardar));
                        } 
                    }
                }

                ListadoVentas.abrirNuevaVenta = true;

                ventaGuardada = false;

                mostrarVenta = 0;

                this.Dispose();
            }
        }

        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            ventaGuardada = true;
            btnTerminarVenta.PerformClick();
        }

        private void txtDescuentoGeneral_KeyUp(object sender, KeyEventArgs e)
        {
            string valor = (sender as TextBox).Text;

            if (valor != "")
            {
                porcentajeGeneral = cn.CalcularPorcentaje(valor);
            }
            else
            {
                porcentajeGeneral = 0;
            }

            CantidadesFinalesVenta();
        }

        private bool VerificarStockProducto()
        {
            bool respuesta = true;

            if (DGVentas.Rows.Count > 0)
            {
                foreach (DataGridViewRow fila in DGVentas.Rows)
                {
                    var stock = Convert.ToInt32(fila.Cells["Stock"].Value);
                    var cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value);

                    if (stock < cantidad)
                    {
                        var producto = fila.Cells["Descripcion"].Value;

                        MessageBox.Show($"El stock de {producto} es insuficiente\nStock actual: {stock}\nRequerido: {cantidad}", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        respuesta = false;

                        break;
                    }
                }
            }
            else
            {
                respuesta = false;
            }

            return respuesta;
        }

        private void btnVentasGuardadas_Click(object sender, EventArgs e)
        {
            ListadoVentasGuardadas venta = new ListadoVentasGuardadas();

            venta.FormClosed += delegate
            {
                if (mostrarVenta > 0)
                {
                    CargarVentaGuardada();
                }
            };

            venta.ShowDialog();
        }


        private void CargarVentaGuardada()
        {
            string[] datos = cn.BuscarVentaGuardada(mostrarVenta);

            cSubtotal.Text = datos[0];
            cIVA.Text = datos[1];
            cTotal.Text = datos[2];
            cDescuento.Text = datos[3];

            if (Convert.ToInt32(datos[4]) > 0)
            {
                txtDescuentoGeneral.Text = datos[4];
            }

            //Verificar si tiene productos la venta
            bool tieneProductos = (bool)cn.EjecutarSelect($"SELECT * FROM ProductosVenta WHERE IDVenta = '{mostrarVenta}'");

            if (tieneProductos)
            {
                string[] productos = cn.ObtenerProductosVenta(mostrarVenta);

                foreach (string producto in productos)
                {
                    string[] info = producto.Split('|');

                    string[] datosProducto = cn.BuscarProducto(Convert.ToInt32(info[0]), FormPrincipal.userID);

                    int cantidad = Convert.ToInt32(info[2]);

                    AgregarProductoLista(datosProducto, cantidad);
                }
            }
        }
    }
}