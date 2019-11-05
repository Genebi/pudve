using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data;
using System.Linq;
using System.Collections.Generic;

namespace PuntoDeVentaV2
{
    public partial class Ventas : Form
    {
        //Status 1 = Venta terminada
        //Status 2 = Venta guardada
        //Status 3 = Venta cancelada
        //Status 4 = Venta a credito
        //Status 5 = Facturas
        //Status 6 = Presupuestos

        float porcentajeGeneral = 0;
        bool ventaGuardada = false; //Para saber si la venta se guardo o no
        int cantidadExtra = 0;

        public static int indiceFila = 0; //Para guardar el indice de la fila cuando se elige agregar multiples productos
        public static int cantidadFila = 0; //Para guardar la cantidad de productos que se agregará a la fila correspondiente

        //Para las ventas guardadas
        public static int mostrarVenta = 0;

        //Estado de la venta
        public static string statusVenta = string.Empty;

        //Para los anticipos por aplicar
        public static string listaAnticipos = string.Empty;
        public static float importeAnticipo = 0f;

        //Variables para almacenar los valores agregados en el form DetalleVenta.cs
        public static string efectivoReal = string.Empty;
        public static string efectivo = string.Empty;
        public static string tarjeta = string.Empty;
        public static string vales = string.Empty;
        public static string cheque = string.Empty;
        public static string transferencia = string.Empty;
        public static string referencia = string.Empty;
        public static string cliente = string.Empty;
        public static string idCliente = string.Empty;
        public static string credito = string.Empty;
        //Para saber con que boton se cerro el form DetalleVenta.cs, en este caso saber si se cerro con el boton aceptar (terminar)
        public static bool botonAceptar = true;

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        // Almacena temporalmente los productos encontrados con las coincidencias de la busqueda
        Dictionary<int, string> productosD; 

        const string fichero = @"\PUDVE\settings\folioventa\setupFolioVenta.txt";       // directorio donde esta el archivo de numero de codigo de barras consecutivo
        string Contenido;                                                               // para obtener el numero que tiene el codigo de barras en el arhivo

        long folioVenta; // variable entera para llevar un consecutivo de codigo de barras

        string buscarvVentaGuardada = null, folio = null;

        int idProducto;
        string producto;
        string[] datosProducto;
        private bool sumarProducto = false;
        private bool restarProducto = false;
        private bool buscarVG = false; // Buscar venta guardada

        DataTable dtProdMessg;
        DataRow drProdMessg;

        public Ventas()
        {
            InitializeComponent();
        }

        private void Ventas_Load(object sender, EventArgs e)
        {
            txtBuscadorProducto.GotFocus  += new EventHandler(BuscarTieneFoco);
            txtBuscadorProducto.LostFocus += new EventHandler(BuscarPierdeFoco);
            txtDescuentoGeneral.GotFocus  += new EventHandler(DescuentoTieneFoco);
            txtDescuentoGeneral.LostFocus += new EventHandler(DescuentoPierdeFoco);

            btnProductoRapido.BackgroundImage = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\plus.png");
            btnEliminarUltimo.BackgroundImage = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");
            btnEliminarTodos.BackgroundImage  = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");
            btnUltimoTicket.BackgroundImage   = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\ticket.png");
            btnPresupuesto.BackgroundImage    = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\money.png");

            btnProductoRapido.BackgroundImageLayout = ImageLayout.Center;
            btnEliminarUltimo.BackgroundImageLayout = ImageLayout.Center;
            btnEliminarTodos.BackgroundImageLayout  = ImageLayout.Center;
            btnUltimoTicket.BackgroundImageLayout   = ImageLayout.Center;
            btnPresupuesto.BackgroundImageLayout    = ImageLayout.Center;

            /*string tmp = @"\\" + Properties.Settings.Default.Hosting + "\\Users\\Acer\\AppData\\Roaming" + fichero;
            string fileContents = File.ReadAllText(tmp);
            MessageBox.Show(fileContents);*/
        }

        private void BuscarTieneFoco(object sender, EventArgs e)
        {
            if (txtBuscadorProducto.Text == "BUSCAR PRODUCTO O SERVICIO...")
            {
                txtBuscadorProducto.Text = "";
            }
        }

        private void BuscarPierdeFoco(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscadorProducto.Text))
            {
                txtBuscadorProducto.Text = "BUSCAR PRODUCTO O SERVICIO...";
                
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

        private void ocultarResultados()
        {
            listaProductos.Visible = false;
        }

        private void txtBuscadorProducto_KeyDown(object sender, KeyEventArgs e)
        {
            // Tecla borrar y suprimir
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                if (txtBuscadorProducto.Text == string.Empty)
                {
                    nudCantidadPS.Value = 1;
                }
            }

            // Enter
            if (e.KeyData == Keys.Enter)
            {
                if (listaProductos.Visible)
                {
                    // Si busca un producto por codigo de barras o clave le da prioridad a este metodo
                    // si encuentra un producto lo va a mostrar de lo contrario la ejecucion seguira y
                    // se mostrara el primer producto que aparezca en la lista
                    var respuesta = BuscarProductoPorCodigo();

                    if (respuesta)
                    {
                        return;
                    }

                    // Verifica si la lista esta visible y contiene un producto al menos
                    // si la lista no tiene el focus le agregada el focus y selecciona el primer producto
                    // que muestra por defecto y ejecuta el evento enter de la lista para agregar el producto
                    if (!listaProductos.Focused)
                    {
                        listaProductos.SelectedIndex = 0;
                        listaProductos.Focus();
                        listaProductos_KeyDown(sender, e);
                    }
                }
                else
                {
                    // Le da prioridad a buscar un producto por codigo de barras o clave
                    BuscarProductoPorCodigo();


                    //===============================================================
                    // Este codigo es para cuando intenta agregar mas cantidad de algun producto
                    // desde el buscador de producto, solo aplica para sumar y restar producto
                    sumarProducto = true;
                    restarProducto = true;

                    txtBuscadorProducto_KeyUp(sender, e);

                    sumarProducto = false;
                    restarProducto = false;

                    //===============================================================
                    // Esto se ejecuta para si el patron de busqueda coincide con la 
                    // busqueda de una venta guardada

                    buscarVG = true;

                    txtBuscadorProducto_KeyUp(sender, e);

                    buscarVG = false;
                }  
            }
        }

        private bool BuscarProductoPorCodigo()
        {
            bool existe = false;

            if (txtBuscadorProducto.Text != "BUSCAR PRODUCTO O SERVICIO...")
            {
                int idProducto = 0;

                // Verificamos si existe en la tabla de codigos de barra extra
                var datosTmp = mb.BuscarCodigoBarrasExtra(txtBuscadorProducto.Text.Trim());

                if (datosTmp.Length > 0)
                {
                    // Verificar que pertenece al usuario
                    var verificarUsuario = (bool)cn.EjecutarSelect($"SELECT * FROM Productos WHERE ID = {datosTmp[0]} AND IDUsuario = {FormPrincipal.userID}");

                    if (verificarUsuario)
                    {
                        idProducto = Convert.ToInt32(datosTmp[0]);
                        using (dtProdMessg = cn.CargarDatos(cs.ObtenerProductMessage(Convert.ToString(idProducto))))
                        {
                            if (dtProdMessg.Rows.Count > 0)
                            {
                                drProdMessg = dtProdMessg.Rows[0];
                                MessageBox.Show("" + drProdMessg["ProductOfMessage"].ToString().ToUpper(),
                                                "Mensaje para el cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (dtProdMessg.Rows.Count <= 0)
                            {
                                //MessageBox.Show("Producto sin mensaje", "Mensaje para el Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                }

                string querySearchProd = $"SELECT prod.ID FROM Productos AS prod WHERE IDUsuario = '{FormPrincipal.userID}' AND (ClaveInterna = '{txtBuscadorProducto.Text}' OR CodigoBarras = '{txtBuscadorProducto.Text}')";

                DataTable searchProd = cn.CargarDatos(querySearchProd);

                if (searchProd.Rows.Count > 0)
                {
                    idProducto = Convert.ToInt32(searchProd.Rows[0]["ID"].ToString());
                }

                if (idProducto > 0)
                {
                    using (dtProdMessg = cn.CargarDatos(cs.ObtenerProductMessage(Convert.ToString(idProducto))))
                    {
                        if (dtProdMessg.Rows.Count > 0)
                        {
                            drProdMessg = dtProdMessg.Rows[0];
                            MessageBox.Show("" + drProdMessg["ProductOfMessage"].ToString().ToUpper(),
                                            "Mensaje para el cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (dtProdMessg.Rows.Count <= 0)
                        {
                            //MessageBox.Show("Producto sin mensaje", "Mensaje para el Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    string[] datosProducto = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                    txtBuscadorProducto.Text = "";
                    txtBuscadorProducto.Focus();
                    ocultarResultados();
                    AgregarProducto(datosProducto);

                    existe = true;
                }
            }

            return existe;
        }

        private void AgregarProducto(string[] datosProducto)
        {
            if (DGVentas.Rows.Count == 0 && buscarvVentaGuardada == "#")
            {
                AgregarProductoLista(datosProducto);
            }
            else if (DGVentas.Rows.Count > 0)
            {
                bool existe = false;

                foreach (DataGridViewRow fila in DGVentas.Rows)
                {
                    //Compara el valor de la celda con el nombre del producto (Descripcion)
                    if (fila.Cells["Descripcion"].Value.Equals(datosProducto[1]))
                    {
                        decimal sumar = 1;

                        if (cantidadExtra > 0)
                        {
                            sumar = cantidadExtra;
                            nudCantidadPS.Value = 1;
                            cantidadExtra = 0;
                        }
                        else if (cantidadExtra == 0)
                        {
                            if (Convert.ToDecimal(nudCantidadPS.Value) > 0)
                            {
                                sumar = Convert.ToDecimal(nudCantidadPS.Value);
                            }

                            nudCantidadPS.Value = 1;
                        }

                        decimal cantidad = Convert.ToDecimal(fila.Cells["Cantidad"].Value) + sumar;
                        float importe = float.Parse(cantidad.ToString()) * float.Parse(fila.Cells["Precio"].Value.ToString());

                        fila.Cells["Cantidad"].Value = cantidad;
                        fila.Cells["Importe"].Value = importe;
                        existe = true;

                        int idProducto = Convert.ToInt32(datosProducto[0]);
                        int tipoDescuento = Convert.ToInt32(datosProducto[3]);

                        if (tipoDescuento > 0)
                        {
                            string[] datosDescuento = cn.BuscarDescuento(tipoDescuento, idProducto);     
                            CalcularDescuento(datosDescuento, tipoDescuento, Convert.ToInt32(cantidad));
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

        private void AgregarProductoLista(string[] datosProducto, decimal cantidad = 1, bool ignorar = false)
        {
            decimal cantidadTmp = cantidad;

            // Se agrega la nueva fila y se obtiene el ID que tendrá
            int rowId = DGVentas.Rows.Add();

            // Obtener la nueva fila
            DataGridViewRow row = DGVentas.Rows[rowId];

            if (buscarvVentaGuardada == "#")
            {
                // Agregamos la información
                row.Cells["NumeroColumna"].Value = rowId;
                row.Cells["IDProducto"].Value = datosProducto[0]; // Este campo no es visible
                row.Cells["PrecioOriginal"].Value = datosProducto[2]; // Este campo no es visible
                row.Cells["DescuentoTipo"].Value = datosProducto[3]; // Este campo tampoco es visible
                row.Cells["Stock"].Value = datosProducto[4]; // Este campo no es visible
                row.Cells["TipoPS"].Value = datosProducto[5]; // Este campo no es visible
                row.Cells["Cantidad"].Value = datosProducto[6];
                row.Cells["Precio"].Value = datosProducto[2];
                row.Cells["Descripcion"].Value = datosProducto[1];
                row.Cells["Descuento"].Value = datosProducto[3];
                row.Cells["Importe"].Value = datosProducto[2];
            }
            else
            {
                if (cantidadExtra > 0)
                {
                    cantidad = cantidadExtra;
                    nudCantidadPS.Value = 1;
                    cantidadExtra = 0;
                }
                else if (cantidadExtra == 0)
                {
                    if (Convert.ToInt32(nudCantidadPS.Value) > 0)
                    {
                        cantidad = Convert.ToDecimal(nudCantidadPS.Value);
                    }

                    nudCantidadPS.Value = 1;
                }
                

                //Esto es para cuando se agregan los productos al momento de cargar venta guardada desde la lista
                if (ignorar == true)
                {
                    cantidad = cantidadTmp;
                }

                //Agregamos la información
                row.Cells["NumeroColumna"].Value = rowId;
                row.Cells["IDProducto"].Value = datosProducto[0]; //Este campo no es visible
                row.Cells["PrecioOriginal"].Value = datosProducto[2]; //Este campo no es visible
                row.Cells["DescuentoTipo"].Value = datosProducto[3]; //Este campo tampoco es visible
                row.Cells["Stock"].Value = datosProducto[4]; //Este campo no es visible
                row.Cells["TipoPS"].Value = datosProducto[5]; //Este campo no es visible
                row.Cells["Cantidad"].Value = cantidad;
                row.Cells["Precio"].Value = datosProducto[2];
                row.Cells["Descripcion"].Value = datosProducto[1];
                row.Cells["Descuento"].Value = 0;
                row.Cells["Importe"].Value = datosProducto[2];
            }
            
            System.Drawing.Image img1 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\plus-square.png");
            System.Drawing.Image img2 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\plus.png");
            System.Drawing.Image img3 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\minus.png");
            System.Drawing.Image img4 = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\remove.png");

            DGVentas.Rows[rowId].Cells["AgregarMultiple"].Value = img1;
            DGVentas.Rows[rowId].Cells["AgregarIndividual"].Value = img2;
            DGVentas.Rows[rowId].Cells["RestarIndividual"].Value = img3;
            DGVentas.Rows[rowId].Cells["EliminarIndividual"].Value = img4;

            DGVentas.Sort(DGVentas.Columns["NumeroColumna"], System.ComponentModel.ListSortDirection.Descending);
            DGVentas.ClearSelection();
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
            if (e.ColumnIndex == 10)
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
            if (e.ColumnIndex == 11)
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
            if (e.ColumnIndex == 12)
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
            if (e.ColumnIndex == 13)
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
            decimal totalArticulos = 0;
            double totalImporte   = 0;
            double totalDescuento = 0;
            double totalSubtotal  = 0;
            double totalIVA16     = 0;
            double totalAnticipos = 0;

            foreach (DataGridViewRow fila in DGVentas.Rows)
            {
                if (buscarvVentaGuardada == "#")
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
                else if (porcentajeGeneral > 0)
                {
                    var precioOriginal = Convert.ToDouble(fila.Cells["PrecioOriginal"].Value);  //Precio original del producto
                    var cantidadProducto = Convert.ToDecimal(fila.Cells["Cantidad"].Value);       //Cantidad de producto
                    var cantidadDescuento = Convert.ToDouble(fila.Cells["Descuento"].Value);    //Cantidad descuento del producto

                    var descuento = (precioOriginal * Convert.ToDouble(cantidadProducto)) - cantidadDescuento;
                    descuento *= porcentajeGeneral;

                    var importeProducto = precioOriginal * Convert.ToDouble(cantidadProducto);
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
                    var cantidadProducto = Convert.ToDecimal(fila.Cells["Cantidad"].Value);
                    var cantidadDescuento = Convert.ToDouble(fila.Cells["Descuento"].Value);

                    var importeProducto = (precioOriginal * Convert.ToDouble(cantidadProducto)) - cantidadDescuento;

                    fila.Cells["Importe"].Value = importeProducto.ToString("0.00");

                    totalImporte += Convert.ToDouble(fila.Cells["Importe"].Value);
                    totalArticulos += cantidadProducto;
                    totalDescuento += cantidadDescuento;
                }
            }

            totalSubtotal = totalImporte / 1.16;
            totalIVA16    = totalSubtotal * 0.16;

            totalAnticipos = Convert.ToDouble(cAnticipo.Text);
            totalAnticipos += importeAnticipo;

            if (totalImporte > 0)
            {
                totalImporte -= totalAnticipos;
            }

            cIVA.Text = totalIVA16.ToString("0.00");
            cTotal.Text = totalImporte.ToString("0.00");
            cSubtotal.Text = totalSubtotal.ToString("0.00");
            cAnticipo.Text = totalAnticipos.ToString("0.00");
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
            mostrarVenta = 0;

            this.Dispose();
        }

        private void btnTerminarVenta_Click(object sender, EventArgs e)
        {
            if (VerificarStockProducto())
            {
                var totalVenta = float.Parse(cTotal.Text);

                DetalleVenta detalle = new DetalleVenta(totalVenta, idCliente);

                detalle.FormClosed += delegate
                {
                    if (botonAceptar)
                    {
                        DatosVenta();
                    }
                };

                detalle.ShowDialog();
            }
        }

        //Se procesa la informacion de los detalles de la venta para guardarse
        private void DetallesVenta(string IDVenta)
        {
            string[] info = new string[] { IDVenta, FormPrincipal.userID.ToString(), efectivo, tarjeta, vales, cheque, transferencia, credito, referencia, idCliente, cliente };

            cn.EjecutarConsulta(cs.GuardarDetallesVenta(info));
        }

        private float CantidadDecimal(string cantidad)
        {
            float resultado = 0f;

            if (string.IsNullOrWhiteSpace(cantidad))
            {
                resultado = 0;
            }
            else
            {
                resultado = float.Parse(cantidad);
            }

            return resultado;
        }

        private void DatosVenta()
        {
            // Datos generales de la venta
            var IdEmpresa = FormPrincipal.userID.ToString();
            var Subtotal = cSubtotal.Text;
            var IVA16 = cIVA.Text;
            var Descuento = cDescuento.Text;
            var Total = cTotal.Text;
            var DescuentoGeneral = porcentajeGeneral.ToString("0.00");
            var Anticipo = cAnticipo.Text;
            var FechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var Folio = "";
            var Serie = "A";
            var idClienteTmp = idCliente;

            if (ventaGuardada)
            {
                statusVenta = "2";

                if (string.IsNullOrWhiteSpace(idClienteTmp))
                {
                    idClienteTmp = "0";
                }
            }
            else
            {
                idClienteTmp = "0";
            }

            string[] guardar = null;
            aumentoFolio();
            Folio = Contenido;
            guardar = new string[] { IdEmpresa, idClienteTmp, IdEmpresa, Subtotal, IVA16, Total, Descuento, DescuentoGeneral, Anticipo, Folio, Serie, statusVenta, FechaOperacion };


            if (VerificarStockProducto())
            {
                // Se hace el guardado de la informacion general de la venta
                int respuesta = cn.EjecutarConsulta(cs.GuardarVenta(guardar, mostrarVenta));

                if (respuesta > 0)
                {
                    // Operacion para afectar la tabla de Caja
                    // var saldoActual = cn.ObtenerSaldoActual(FormPrincipal.userID);
                    // var totalTmp = saldoActual + Convert.ToDouble(Total);

                    string[] datos = new string[] {
                        "venta", Total, "0", "", FechaOperacion, FormPrincipal.userID.ToString(),
                        efectivo, tarjeta, vales, cheque, transferencia, credito, Anticipo
                    };

                    cn.EjecutarConsulta(cs.OperacionCaja(datos));


                    // Obtener ID de la venta
                    string idVenta = cn.EjecutarSelect("SELECT ID FROM Ventas ORDER BY ID DESC LIMIT 1", 1).ToString();

                    // Si mostrarVenta contine un valor mayor a cero quiere decir que es una venta guardada con la que se esta trabajando
                    if (mostrarVenta > 0)
                    {
                        idVenta = mostrarVenta.ToString();

                        cn.EjecutarConsulta(cs.EliminarProductosVenta(Convert.ToInt32(idVenta)));
                    }

                    // Array para almacenar la informacion de los productos vendidos
                    string[][] infoProductos = new string[DGVentas.Rows.Count][];

                    int contador = 0;

                    // Datos de los productos vendidos
                    foreach (DataGridViewRow fila in DGVentas.Rows)
                    {
                        var IDProducto = fila.Cells["IDProducto"].Value.ToString();
                        var Nombre = fila.Cells["Descripcion"].Value.ToString();
                        var Cantidad = fila.Cells["Cantidad"].Value.ToString();
                        var Precio = fila.Cells["Precio"].Value.ToString();
                        var Tipo = fila.Cells["TipoPS"].Value.ToString();

                        guardar = new string[] { idVenta, IDProducto, Nombre, Cantidad, Precio };

                        // Guardar info de los productos
                        infoProductos[contador] = guardar;

                        contador++;

                        // Si es un producto lo guarda en la tabla de productos de venta
                        if (Tipo == "P")
                        {
                            cn.EjecutarConsulta(cs.GuardarProductosVenta(guardar));
                        }

                        // Si la venta no fue guardada con el boton "Guardar"
                        if (!ventaGuardada)
                        {
                            // Producto
                            if (Tipo == "P")
                            {
                                // Actualizar stock de productos
                                var stock = Convert.ToDecimal(fila.Cells["Stock"].Value);
                                var vendidos = Convert.ToDecimal(fila.Cells["Cantidad"].Value);
                                var restantes = (stock - vendidos).ToString();

                                guardar = new string[] { IDProducto, restantes };

                                cn.EjecutarConsulta(cs.ActualizarStockProductos(guardar));
                            }

                            // Servicio o paquete
                            if (Tipo == "S" || Tipo == "PQ")
                            {
                                var vendidos = Convert.ToDecimal(fila.Cells["Cantidad"].Value);

                                var datosServicio = cn.ObtenerProductosServicio(Convert.ToInt32(IDProducto));

                                foreach (string producto in datosServicio)
                                {
                                    var datosProducto = producto.Split('|');
                                    var idProducto = Convert.ToInt32(datosProducto[0]);
                                    var stockRequerido = Convert.ToDecimal(datosProducto[1]) * vendidos;

                                    datosProducto = cn.VerificarStockProducto(idProducto, FormPrincipal.userID);
                                    datosProducto = datosProducto[0].Split('|');
                                    var stockActual = Convert.ToDecimal(datosProducto[1]);

                                    var restantes = (stockActual - stockRequerido).ToString();

                                    guardar = new string[] { idProducto.ToString(), restantes };

                                    cn.EjecutarConsulta(cs.ActualizarStockProductos(guardar));
                                }
                            }

                            // Guardar detalles de la venta
                            DetallesVenta(idVenta);
                        }
                    }

                    // Convertir la cadena que guarda los IDs de los anticipos usados en Array
                    if (!string.IsNullOrEmpty(listaAnticipos))
                    {
                        var auxiliar = listaAnticipos.Remove(listaAnticipos.Length - 1);

                        var anticipos = auxiliar.Split('-');

                        foreach (string anticipo in anticipos)
                        {
                            var idAnticipo = Convert.ToInt32(anticipo);

                            cn.EjecutarConsulta(cs.CambiarStatusAnticipo(3, idAnticipo, FormPrincipal.userID));
                            cn.EjecutarConsulta($"UPDATE Anticipos SET IDVenta = {idVenta} WHERE ID = {idAnticipo} AND IDUsuario = {FormPrincipal.userID}");
                        }

                        //cn.EjecutarConsulta($"UPDATE Caja SET Anticipo = {totalAnticipos} WHERE IDUsuario = {FormPrincipal.userID} AND FechaOperacion = '{FechaOperacion}'");
                        cn.EjecutarConsulta($"UPDATE DetallesVenta SET Anticipo = '{Anticipo}' WHERE IDVenta = {idVenta} AND IDUsuario = {FormPrincipal.userID}");
                    }

                    GenerarTicket(infoProductos);
                    ImprimirTicket(idVenta);
                }

                ListadoVentas.abrirNuevaVenta = true;
                ventaGuardada = false;
                mostrarVenta = 0;
                listaAnticipos = string.Empty;

                this.Dispose();
            }
        }

        private void aumentoFolio()
        {
            /*if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                Contenido = mb.ObtenerMaximoFolio(FormPrincipal.userID);
            }
            else
            {
                // leemos el archivo de codigo de barras que lleva el consecutivo
                using (StreamReader readfile = new StreamReader(Properties.Settings.Default.rutaDirectorio + fichero))
                {
                    Contenido = readfile.ReadToEnd();   // se lee todo el archivo y se almacena en la variable Contenido
                }
            }*/

            Contenido = mb.ObtenerMaximoFolio(FormPrincipal.userID);
            

            if (Contenido == "")        // si el contenido es vacio 
            {
                PrimerFolioVenta();      // iniciamos el conteo del folio de venta
                AumentarFolioVenta();    // Aumentamos el folio de venta para la siguiente vez que se utilice
            }
            else if (Contenido != "")   // si el contenido no es vacio
            {
                //MessageBox.Show("Trabajando en el Proceso");
                AumentarFolioVenta();    // Aumentamos el codigo de barras para la siguiente vez que se utilice
            }
        }

        private void AumentarFolioVenta()
        {
            folioVenta = long.Parse(Contenido);
            folioVenta++;
            Contenido = folioVenta.ToString();

            /*if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                using (StreamWriter outfile = new StreamWriter(Properties.Settings.Default.Hosting + fichero))
                {
                    outfile.WriteLine(Contenido);
                }
            }
            else
            {
                using (StreamWriter outfile = new StreamWriter(Properties.Settings.Default.rutaDirectorio + fichero))
                {
                    outfile.WriteLine(Contenido);
                }
            }*/
        }

        private void PrimerFolioVenta()
        {
            Contenido = "000000000";
        }

        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            ListaClientes cliente = new ListaClientes();

            cliente.FormClosed += delegate
            {
                ventaGuardada = true;
                DatosVenta();
            };

            cliente.ShowDialog();
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

            // Comprobamos que la opcion stock negativo sea false para que se pueda realizar la venta
            // verificando si el stock es suficiente para realizar la venta, de lo contrario se
            // permitira hacer la venta incluso si el stock es insuficiente
            if (Properties.Settings.Default.StockNegativo == false)
            {
                if (DGVentas.Rows.Count > 0)
                {
                    foreach (DataGridViewRow fila in DGVentas.Rows)
                    {
                        var stock = Convert.ToInt32(fila.Cells["Stock"].Value);
                        var cantidad = Convert.ToInt32(fila.Cells["Cantidad"].Value);
                        var tipoPS = fila.Cells["TipoPS"].Value.ToString();

                        // Es producto
                        if (tipoPS == "P")
                        {
                            if (stock < cantidad)
                            {
                                var producto = fila.Cells["Descripcion"].Value;

                                MessageBox.Show($"El stock de {producto} es insuficiente\nStock actual: {stock}\nRequerido: {cantidad}", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                respuesta = false;

                                break;
                            }
                        }

                        // Es servicio o paquete
                        if (tipoPS == "S" || tipoPS == "PQ")
                        {
                            var servicio = fila.Cells["Descripcion"].Value;
                            var idServicio = Convert.ToInt32(fila.Cells["IDProducto"].Value);
                            var categoria = string.Empty;

                            if (tipoPS == "S") { categoria = "Servicio"; }
                            if (tipoPS == "PQ") { categoria = "Paquete"; }

                            // Obtener los productos relacionados (ID, Cantidad)
                            var datosServicio = cn.ObtenerProductosServicio(idServicio);

                            if (datosServicio.Length > 0)
                            {
                                // Verificar la cantidad de cada producto con el stock actual de ese producto individual
                                foreach (string producto in datosServicio)
                                {
                                    var datosProducto = producto.Split('|');
                                    var idProducto = Convert.ToInt32(datosProducto[0]);
                                    var stockRequerido = Convert.ToInt32(datosProducto[1]) * cantidad;

                                    datosProducto = cn.VerificarStockProducto(idProducto, FormPrincipal.userID);
                                    datosProducto = datosProducto[0].Split('|');

                                    var nombreProducto = datosProducto[0];
                                    var stockActual = Convert.ToInt32(datosProducto[1]);

                                    if (stockActual < stockRequerido)
                                    {
                                        var mensaje = $"El stock de {nombreProducto} es insuficiente\n{categoria}: {servicio}\nStock actual: {stockActual}\nRequerido: {stockRequerido}";

                                        MessageBox.Show(mensaje, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                        respuesta = false;

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    respuesta = false;
                }
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
            string[] datos = cn.BuscarVentaGuardada(mostrarVenta, FormPrincipal.userID);

            cSubtotal.Text = datos[0];
            cIVA.Text = datos[1];
            cTotal.Text = datos[2];
            cDescuento.Text = datos[3];

            //Descuento general
            if (Convert.ToInt32(datos[4]) > 0)
            {
                txtDescuentoGeneral.Text = datos[4];
            }

            //Verificar si tiene productos la venta
            bool tieneProductos = (bool)cn.EjecutarSelect($"SELECT * FROM ProductosVenta WHERE IDVenta = '{mostrarVenta}'");

            if (tieneProductos)
            {
                DGVentas.Rows.Clear();

                string[] productos = cn.ObtenerProductosVenta(mostrarVenta);

                foreach (string producto in productos)
                {
                    string[] info = producto.Split('|');

                    string[] datosProducto = cn.BuscarProducto(Convert.ToInt32(info[0]), FormPrincipal.userID);

                    int cantidad = Convert.ToInt32(info[2]);

                    AgregarProductoLista(datosProducto, cantidad, true);
                }
            }
        }

        private void Ventas_FormClosing(object sender, FormClosingEventArgs e)
        {
            mostrarVenta = 0;
            listaAnticipos = string.Empty;
        }

        private void GenerarTicket(string[][] productos)
        {
            var datos = FormPrincipal.datosUsuario;

            // Medidas de ticket de 57 y 80 mm
            // 1 pulgada = 2.54 cm = 72 puntos = 25.4 mm
            // 57mm = 161.28 pt
            // 80mm = 226.08 pt

            var tipoPapel  = 80;
            var anchoPapel = Convert.ToInt32(Math.Floor((((tipoPapel * 0.10) * 72) / 2.54)));
            var altoPapel  = Convert.ToInt32(anchoPapel + 64); // 54

            if (productos.Length > 3)
            {
                var filas = productos.Length / 2.54;
                filas *= 25.4;
                altoPapel += Convert.ToInt32(filas);

                for (int i = 0; i < productos.Length; i++)
                {
                    if (productos[i][2].Length > 18)
                    {
                        altoPapel += 20;
                    }
                }
            }

            //Variables y arreglos para el contenido de la tabla
            float[] anchoColumnas = new float[] { };

            string txtDescripcion = string.Empty;
            string txtCantidad = string.Empty;
            string txtImporte = string.Empty;
            string txtPrecio = string.Empty;
            string salto = string.Empty;

            int medidaFuenteMensaje = 0;
            int medidaFuenteNegrita = 0;
            int medidaFuenteNormal = 0;
            int medidaFuenteGrande = 0;

            int separadores = 0;
            int anchoLogo = 0;
            int altoLogo = 0;
            int espacio = 0;

            if (tipoPapel == 80)
            {
                anchoColumnas = new float[] { 10f, 24f, 9f, 9f };
                txtDescripcion = "Descripción";
                txtCantidad = "Cantidad";
                txtImporte = "Importe";
                txtPrecio = "Precio";
                separadores = 81;
                anchoLogo = 110;
                altoLogo = 60;
                espacio = 10;

                medidaFuenteMensaje = 10;
                medidaFuenteGrande = 10;
                medidaFuenteNegrita = 8;
                medidaFuenteNormal = 8;

                salto = "\n";
            }
            else if (tipoPapel == 57)
            {
                anchoColumnas = new float[] { 10f, 20f, 9f, 9f };
                txtDescripcion = "Descripción";
                txtImporte = "Imp.";
                txtCantidad = "Cant.";
                txtPrecio = "Prec.";
                separadores = 75;
                anchoLogo = 80;
                altoLogo = 40;
                espacio = 8;

                medidaFuenteMensaje = 6;
                medidaFuenteGrande = 8;
                medidaFuenteNegrita = 6;
                medidaFuenteNormal = 6;

                salto = string.Empty;
            }

            Document ticket = new Document(new iTextSharp.text.Rectangle(anchoPapel, altoPapel), 3, 3, 3, 0);
            PdfWriter writer = PdfWriter.GetInstance(ticket, new FileStream(@"C:\Archivos PUDVE\Ventas\Tickets\ticket_venta_" + productos[0][0] + ".pdf", FileMode.Create));

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteNormal);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, medidaFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteGrande);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteMensaje);

            string logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + datos[11];
            string encabezado = $"{salto}{datos[1]} {datos[2]} {datos[3]}, {datos[4]}, {datos[5]}\nCol. {datos[6]} C.P. {datos[7]}\nRFC: {datos[8]}\n{datos[9]}\nTel. {datos[10]}\n\n";

            ticket.Open();

            //Validación para verificar si existe logotipo
            if (logotipo != "")
            {
                if (File.Exists(logotipo))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logotipo);
                    logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    logo.ScaleAbsolute(anchoLogo, altoLogo);
                    ticket.Add(logo);
                }
            }

            Paragraph titulo = new Paragraph(datos[0] + "\n", fuenteGrande);
            Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            domicilio.Alignment = Element.ALIGN_CENTER;
            domicilio.SetLeading(espacio, 0);

            /**************************************
             ** Tabla con los productos vendidos **
             **************************************/

            PdfPTable tabla = new PdfPTable(4);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(anchoColumnas);

            PdfPCell colCantidad = new PdfPCell(new Phrase(txtCantidad, fuenteNegrita));
            colCantidad.BorderWidth = 0;

            PdfPCell colDescripcion = new PdfPCell(new Phrase(txtDescripcion, fuenteNegrita));
            colDescripcion.BorderWidth = 0;

            PdfPCell colPrecio = new PdfPCell(new Phrase(txtPrecio, fuenteNegrita));
            colPrecio.BorderWidth = 0;

            PdfPCell colImporte = new PdfPCell(new Phrase(txtImporte, fuenteNegrita));
            colImporte.BorderWidth = 0;

            tabla.AddCell(colCantidad);
            tabla.AddCell(colDescripcion);
            tabla.AddCell(colPrecio);
            tabla.AddCell(colImporte);

            PdfPCell separadorInicial = new PdfPCell(new Phrase(new string('-', separadores), fuenteNormal));
            separadorInicial.BorderWidth = 0;
            separadorInicial.Colspan = 4;

            tabla.AddCell(separadorInicial);


            float totalTicket = 0;

            var longitud = productos.Length;

            for (int i = 0; i < longitud; i++)
            {
                var importe = float.Parse(productos[i][3]) * float.Parse(productos[i][4]);

                totalTicket += importe;

                PdfPCell colCantidadTmp = new PdfPCell(new Phrase(productos[i][3], fuenteNormal));
                colCantidadTmp.HorizontalAlignment = Element.ALIGN_CENTER;
                colCantidadTmp.BorderWidth = 0;

                PdfPCell colDescripcionTmp = new PdfPCell(new Phrase(productos[i][2], fuenteNormal));
                colDescripcionTmp.BorderWidth = 0;

                PdfPCell colPrecioTmp = new PdfPCell(new Phrase("$" + productos[i][4], fuenteNormal));
                colPrecioTmp.BorderWidth = 0;

                PdfPCell colImporteTmp = new PdfPCell(new Phrase("$" + importe.ToString("0.00"), fuenteNormal));
                colImporteTmp.BorderWidth = 0;

                tabla.AddCell(colCantidadTmp);
                tabla.AddCell(colDescripcionTmp);
                tabla.AddCell(colPrecioTmp);
                tabla.AddCell(colImporteTmp);
            }

            PdfPCell separadorFinal = new PdfPCell(new Phrase(new string('-', separadores), fuenteNormal));
            separadorFinal.BorderWidth = 0;
            separadorFinal.Colspan = 4;

            PdfPCell totalVenta = new PdfPCell(new Phrase("TOTAL: $" + totalTicket.ToString("0.00"), fuenteNormal));
            totalVenta.BorderWidth = 0;
            totalVenta.HorizontalAlignment = Element.ALIGN_RIGHT;
            totalVenta.Colspan = 4;

            tabla.AddCell(separadorFinal);
            tabla.AddCell(totalVenta);

            /******************************************
             ** Fin tabla con los productos vendidos **
             ******************************************/

            Paragraph mensaje = new Paragraph("\nCambios y Garantía máximo 7 días después de su compra, presentando el Ticket. Gracias por su preferencia.", fuenteNormal);
            mensaje.Alignment = Element.ALIGN_CENTER;

            var culture = new System.Globalization.CultureInfo("es-MX");
            var dia = culture.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek);
            var fecha = DateTime.Now.ToString("dd/MM/yyyy HH:mm tt");

            dia = cn.Capitalizar(dia);

            Paragraph diaVenta = new Paragraph($"\n{dia} - {fecha} - ID Venta: {productos[0][0]}", fuenteNormal);
            diaVenta.Alignment = Element.ALIGN_CENTER;

            ticket.Add(titulo);
            ticket.Add(domicilio);
            ticket.Add(tabla);
            ticket.Add(mensaje);
            ticket.Add(diaVenta);

            ticket.AddTitle("Ticket Venta");
            ticket.AddAuthor("PUDVE");
            ticket.Close();
            writer.Close();
        }

        private void GenerarTicketCaja()
        {
            int folioTicket = Properties.Settings.Default.folioAbrirCaja + 1;

            Properties.Settings.Default.folioAbrirCaja = folioTicket;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            var datos = FormPrincipal.datosUsuario;

            // Medidas de ticket de 57 y 80 mm
            // 1 pulgada = 2.54 cm = 72 puntos = 25.4 mm
            // 57mm = 161.28 pt
            // 80mm = 226.08 pt

            var tipoPapel = 80;
            var anchoPapel = Convert.ToInt32(Math.Floor((((tipoPapel * 0.10) * 72) / 2.54)));
            var altoPapel = Convert.ToInt32(anchoPapel + 64); // 54

            string txtDescripcion = string.Empty;
            string txtCantidad = string.Empty;
            string txtImporte = string.Empty;
            string txtPrecio = string.Empty;
            string salto = string.Empty;

            int medidaFuenteMensaje = 0;
            int medidaFuenteNegrita = 0;
            int medidaFuenteNormal = 0;
            int medidaFuenteGrande = 0;

            int separadores = 0;
            int anchoLogo = 0;
            int altoLogo = 0;
            int espacio = 0;

            if (tipoPapel == 80)
            {
                separadores = 81;
                anchoLogo = 110;
                altoLogo = 60;
                espacio = 10;

                medidaFuenteMensaje = 10;
                medidaFuenteGrande = 10;
                medidaFuenteNegrita = 8;
                medidaFuenteNormal = 8;

                salto = "\n";
            }
            else if (tipoPapel == 57)
            {

                separadores = 75;
                anchoLogo = 80;
                altoLogo = 40;
                espacio = 8;

                medidaFuenteMensaje = 6;
                medidaFuenteGrande = 8;
                medidaFuenteNegrita = 6;
                medidaFuenteNormal = 6;

                salto = string.Empty;
            }

            Document ticket = new Document(new iTextSharp.text.Rectangle(anchoPapel, altoPapel), 3, 3, 3, 0);
            PdfWriter writer = PdfWriter.GetInstance(ticket, new FileStream(@"C:\Archivos PUDVE\Ventas\Tickets\ticket_caja_abierta_" + folioTicket + ".pdf", FileMode.Create));

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteNormal);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, medidaFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteGrande);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, medidaFuenteMensaje);

            string logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + datos[11];
            string encabezado = $"{salto}{datos[1]} {datos[2]} {datos[3]}, {datos[4]}, {datos[5]}\nCol. {datos[6]} C.P. {datos[7]}\nRFC: {datos[8]}\n{datos[9]}\nTel. {datos[10]}\n\n";

            ticket.Open();

            //Validación para verificar si existe logotipo
            if (logotipo != "")
            {
                if (File.Exists(logotipo))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logotipo);
                    logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    logo.ScaleAbsolute(anchoLogo, altoLogo);
                    ticket.Add(logo);
                }
            }

            Paragraph titulo = new Paragraph(datos[0] + "\n", fuenteGrande);
            Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            domicilio.Alignment = Element.ALIGN_CENTER;
            domicilio.SetLeading(espacio, 0);

            var fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss tt");

            Paragraph mensaje = new Paragraph("\n*** CAJA ABIERTA ***\n" + fecha, fuenteGrande);
            mensaje.Alignment = Element.ALIGN_CENTER;

            ticket.Add(titulo);
            ticket.Add(domicilio);
            ticket.Add(mensaje);

            ticket.AddTitle("Ticket Caja Abierta");
            ticket.AddAuthor("PUDVE");
            ticket.Close();
            writer.Close();

            ImprimirTicket(folioTicket.ToString(), 1);
        }

        private void ImprimirTicket(string idVenta, int tipo = 0)
        {
            try
            {
                var ruta = string.Empty;

                if (tipo == 0)
                {
                    ruta = $@"C:\Archivos PUDVE\Ventas\Tickets\ticket_venta_{idVenta}.pdf";
                }

                if (tipo == 1)
                {
                    ruta = $@"C:\Archivos PUDVE\Ventas\Tickets\ticket_caja_abierta_{idVenta}.pdf";
                }

                ProcessStartInfo info = new ProcessStartInfo();
                info.Verb = "print";
                info.FileName = ruta;
                info.CreateNoWindow = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;

                Process p = new Process();
                p.StartInfo = info;
                p.Start();

                p.WaitForInputIdle();
                System.Threading.Thread.Sleep(5000);

                if (false == p.CloseMainWindow())
                {
                    p.Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error No: "+ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAnticipos_Click(object sender, EventArgs e)
        {
            ListadoAnticipos anticipo = new ListadoAnticipos();

            anticipo.FormClosed += delegate {

                if (importeAnticipo > 0)
                {
                    CantidadesFinalesVenta();
                    importeAnticipo = 0f;
                }
            };

            anticipo.ShowDialog();
        }

        private void btnUltimoTicket_Click(object sender, EventArgs e)
        {
            var idVenta = cn.EjecutarSelect($"SELECT * FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 ORDER BY ID DESC LIMIT 1", 1).ToString();

            ImprimirTicket(idVenta);
        }

        private void btnPresupuesto_Click(object sender, EventArgs e)
        {
            //Sin acciones
        }

        #region Expresiones regulares para buscar producto
        private string VerificarPatronesBusqueda(string cadena)
        {                                                   //  -----------------------------------------------------------------
                                                            //  |   Expresion Regular                       |   Ejemplo         |
                                                            //  |---------------------------------------------------------------|
            string primerPatron = @"^\d+\s\*\s";            //  |   (digito+espacioBlanco*espacioBlanco)    |   5 * 15665132    |
            string segundoPatron = @"^(\+\d+)|(\d+\+)$";    //  |   ((Signo+)digito+ || digito+(Signo+))    |   +2 || 2+        |
            string tercerPatron = @"^(\-\d+)|(\d+\-)$";     //  |   ((Signo-)digito+ || digito+(Signo-))    |   -1 || 1-        |
            string cuartoPatron = @"^\d+\*\s";              //  |   (digito+(Signo*)+espacioBlanco)         |   5* 15665132     |
            string quintoPatron = @"^\d+\s\*";              //  |   (digito+(Signo*)+espacioBlanco)         |   5 *15665132     |
            string sextoPatron = @"^\d+\*";                 //  |   (digito+(Signo*)+espacioBlanco)         |   5*15665132      |
            string septimoPatron = @"^#\s\d+";              //  |   (#+espacioBlanco)                       |   # FolioDeVenta  |
                                                            //  -----------------------------------------------------------------

            Match primeraCoincidencia = Regex.Match(cadena, primerPatron, RegexOptions.IgnoreCase);
            Match segundaCoincidencia = Regex.Match(cadena, segundoPatron, RegexOptions.IgnoreCase);
            Match terceraCoincidencia = Regex.Match(cadena, tercerPatron, RegexOptions.IgnoreCase);
            Match cuartaCoincidencia = Regex.Match(cadena, cuartoPatron, RegexOptions.IgnoreCase);
            Match quintaCoincidencia = Regex.Match(cadena, quintoPatron, RegexOptions.IgnoreCase);
            Match sextaCoincidencia = Regex.Match(cadena, sextoPatron, RegexOptions.IgnoreCase);
            Match septimaCoincidencia = Regex.Match(cadena, septimoPatron, RegexOptions.IgnoreCase);
            //Se necesita crear una clave para las ventas guardadas y cambiar la forma en que hara la buscada
            //usando enter, etc

            // Si encuentra coincidencia asigna la cantidad a la variable multiplicar 
            // y se visualiza en el campo numerico
            if (primeraCoincidencia.Success)
            {
                var resultado = primeraCoincidencia.Value.Trim().Split(' ');

                cantidadExtra = Convert.ToInt32(resultado[0]);

                if (cantidadExtra != 0)
                {
                    if (cantidadExtra >= nudCantidadPS.Minimum && cantidadExtra <= nudCantidadPS.Maximum)
                    {
                        nudCantidadPS.Value = cantidadExtra;
                    }
                }

                cadena = Regex.Replace(cadena, primerPatron, string.Empty);
            }
            else if (segundaCoincidencia.Success)
            {
                if (sumarProducto)
                {
                    var resultado = segundaCoincidencia.Value.Trim().Split('+');

                    if (resultado[0] != string.Empty)
                    {
                        cantidadExtra = Convert.ToInt32(resultado[0]);
                    }
                    else
                    {
                        cantidadExtra = Convert.ToInt32(resultado[1]);
                    }

                    cadena = Regex.Replace(cadena, segundoPatron, string.Empty);

                    //Verifica que exista algun producto o servicio en el datagridview
                    if (DGVentas.Rows.Count > 0)
                    {
                        if (cantidadExtra != 0)
                        {
                            //Si contiene un valor que este dentro del rango a los definidos del control NumericUpDown
                            if (cantidadExtra >= nudCantidadPS.Minimum && cantidadExtra <= nudCantidadPS.Maximum)
                            {
                                //Se obtiene la cantidad del ultimo producto agregado para despues sumarse la que se puso con el comando
                                var cantidad = Convert.ToInt32(DGVentas.Rows[0].Cells["Cantidad"].Value);

                                cantidad += cantidadExtra;

                                DGVentas.Rows[0].Cells["Cantidad"].Value = cantidad;

                                CantidadesFinalesVenta();

                                //nudCantidadPS.Value = cantidadExtra;

                                cantidadExtra = 0;
                            }
                        }
                    }
                }  
            }
            else if (terceraCoincidencia.Success)
            {
                if (restarProducto)
                {
                    var resultado = terceraCoincidencia.Value.Trim().Split('-');

                    if (resultado[0] != string.Empty)
                    {
                        cantidadExtra = Convert.ToInt32(resultado[0]) * -1;
                    }
                    else
                    {
                        cantidadExtra = Convert.ToInt32(resultado[1]) * -1;
                    }

                    cadena = Regex.Replace(cadena, tercerPatron, string.Empty);

                    //Verifica que exista algun producto o servicio en el datagridview
                    if (DGVentas.Rows.Count > 0)
                    {
                        if (cantidadExtra != 0)
                        {
                            //Si contiene un valor que este dentro del rango a los definidos del control NumericUpDown
                            if (cantidadExtra >= nudCantidadPS.Minimum && cantidadExtra <= nudCantidadPS.Maximum)
                            {
                                //Se obtiene la cantidad del ultimo producto agregado para despues sumarse la que se puso con el comando
                                var cantidad = Convert.ToInt32(DGVentas.Rows[0].Cells["Cantidad"].Value);

                                cantidad += cantidadExtra;

                                if (cantidad < 0) { cantidad = 1; }

                                DGVentas.Rows[0].Cells["Cantidad"].Value = cantidad;

                                CantidadesFinalesVenta();

                                //nudCantidadPS.Value = cantidadExtra;

                                cantidadExtra = 0;
                            }
                        }
                    }
                }  
            }
            else if (cuartaCoincidencia.Success)
            {
                // 5* 652651651651
                var resultado = cuartaCoincidencia.Value.Trim().Split('*');

                cantidadExtra = Convert.ToInt32(resultado[0]);

                if (cantidadExtra != 0)
                {
                    if (cantidadExtra >= nudCantidadPS.Minimum && cantidadExtra <= nudCantidadPS.Maximum)
                    {
                        nudCantidadPS.Value = cantidadExtra;
                    }
                }

                cadena = Regex.Replace(cadena, cuartoPatron, string.Empty);
            }
            else if (quintaCoincidencia.Success)
            {
                // 5 *652651651651
                var resultado = quintaCoincidencia.Value.Trim().Split('*');

                cantidadExtra = Convert.ToInt32(resultado[0]);

                if (cantidadExtra != 0)
                {
                    if (cantidadExtra >= nudCantidadPS.Minimum && cantidadExtra <= nudCantidadPS.Maximum)
                    {
                        nudCantidadPS.Value = cantidadExtra;
                    }
                }

                cadena = Regex.Replace(cadena, quintoPatron, string.Empty);
            }
            else if (sextaCoincidencia.Success)
            {
                // 5*652651651651
                var resultado = sextaCoincidencia.Value.Trim().Split('*');

                cantidadExtra = Convert.ToInt32(resultado[0]);

                if (cantidadExtra != 0)
                {
                    if (cantidadExtra >= nudCantidadPS.Minimum && cantidadExtra <= nudCantidadPS.Maximum)
                    {
                        nudCantidadPS.Value = cantidadExtra;
                    }
                }

                cadena = Regex.Replace(cadena, sextoPatron, string.Empty);
            }
            else if (septimaCoincidencia.Success)
            {
                // #$% FolioDeVenta

                if (buscarVG)
                {
                    var resultado = septimaCoincidencia.Value.Trim().Split(' ');
                    buscarvVentaGuardada = resultado[0];
                    //cadena = Regex.Replace(cadena, septimoPatron, string.Empty);
                    folio = resultado[1];
                }
            }

            ocultarResultados();

            return cadena;
        }
        #endregion

        private void Ventas_KeyDown(object sender, KeyEventArgs e)
        {
            if (listaProductos.Visible == true && !string.IsNullOrWhiteSpace(txtBuscadorProducto.Text))
            {
                if (listaProductos.Items.Count == 0)
                {
                    return;
                }

                //Presiono hacia arriba
                if (e.KeyCode == Keys.Up)
                {
                    listaProductos.Focus();

                    if (listaProductos.SelectedIndex > 0)
                    {
                        listaProductos.SelectedIndex--;
                        e.Handled = true;
                    }
                }

                //Presiono hacia abajo
                if (e.KeyCode == Keys.Down)
                {
                    listaProductos.Focus();

                    if (listaProductos.SelectedIndex < (listaProductos.Items.Count - 1))
                    {
                        listaProductos.SelectedIndex++;
                        e.Handled = true;
                    }
                }
            }

            //Cuando presiona la tecla fin hace click en el boton terminar venta
            if (e.KeyData == Keys.End)
            {
                btnTerminarVenta.PerformClick();
            }
        }

        private void txtBuscadorProducto_KeyUp(object sender, KeyEventArgs e)
        {
            listaProductos.Items.Clear();

            txtBuscadorProducto.Text = VerificarPatronesBusqueda(txtBuscadorProducto.Text);

            if (txtBuscadorProducto.Text.Length == 0)
            {
                ocultarResultados();
                return;
            }

            // Regresa un diccionario
            var resultados = mb.BuscarProducto(txtBuscadorProducto.Text);

            if (resultados.Count > 0)
            {
                // Guardamos los datos devueltos temporalmente en productosD
                productosD = resultados;

                foreach (var item in resultados)
                {
                    listaProductos.Items.Add(item.Value);
                    listaProductos.Visible = true;
                    listaProductos.SelectedIndex = 0;
                }
            }

            if (buscarvVentaGuardada == "#")
            {
                txtBuscadorProducto.Text = "";
                string[] datosVentaGuardada = cn.ObtenerVentaGuardada(FormPrincipal.userID, Convert.ToInt32(folio));
                AgregarProducto(datosVentaGuardada);
                buscarvVentaGuardada = "";
            }

            if (listaProductos.Visible == true)
            {
                this.KeyPreview = true;
            }
        }

        private void listaProductos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ProductoSeleccionado();
        }

        private void listaProductos_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    e.IsInputKey = true;
                    break;
            }
        }

        private void btnAbrirCaja_Click(object sender, EventArgs e)
        {
            GenerarTicketCaja();
        }

        private void listaProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                ProductoSeleccionado();
            }
        }

        private void ProductoSeleccionado()
        {
            //Se obtiene el texto del item seleccionado del ListBox
            producto = listaProductos.Items[listaProductos.SelectedIndex].ToString();
            //Se obtiene el indice del array donde se encuentra el producto seleccionado
            //idProducto = Convert.ToInt32(datos.GetKey(Array.IndexOf(productos, producto)));
            idProducto = productosD.FirstOrDefault(x => x.Value == producto).Key;

            datosProducto = cn.BuscarProducto(idProducto, FormPrincipal.userID);

            using (dtProdMessg = cn.CargarDatos(cs.ObtenerProductMessage(Convert.ToString(idProducto))))
            {
                if (dtProdMessg.Rows.Count > 0)
                {
                    drProdMessg = dtProdMessg.Rows[0];
                    MessageBox.Show("" + drProdMessg["ProductOfMessage"].ToString().ToUpper(),
                                    "Mensaje para el cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (dtProdMessg.Rows.Count <= 0)
                {
                    //MessageBox.Show("Producto sin mensaje", "Mensaje para el Cliente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            //borrar producto a buscar
            txtBuscadorProducto.Text = "";
            txtBuscadorProducto.Focus();
            ocultarResultados();

            AgregarProducto(datosProducto);
        }
    }
}