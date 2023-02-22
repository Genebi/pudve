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
    public partial class Ganancia : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        public int lugarGanancia { get; set; } // 1 = Desde Listado / 2 = Desde Carrito Ventas
        int cerrar = 0;
        public List<string> idsprods = new List<string>();
        public decimal totalVenta { get; set; }
        public static int gananciaGrafica; // para mandar la ganancia a graficar

        public Ganancia()
        {
            InitializeComponent();
        } 

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Ganancia_Load(object sender, EventArgs e)
        {
            Ventas vent = new Ventas();
            lblMensaje.Visible = false;
            if (lugarGanancia == 1)
            {
                var IdsProductVenta = cn.CargarDatos($"SELECT IDProducto FROM productosventa WHERE IDVenta = {ListadoVentas.idGananciaVenta}");
                var totalDeVenta = cn.CargarDatos($"SELECT Total FROM ventas WHERE ID = {ListadoVentas.idGananciaVenta}");
                List<string> productosEnCombos = new List<string>();
                List<string> combosEliminar = new List<string>();
                DataTable dtTablaIdsProducs = new DataTable();
                decimal precioPodsEnCombo = 0;

                foreach (DataRow item in IdsProductVenta.Rows)
                {
                    var productoPAquete = cn.CargarDatos($"SELECT Tipo FROM productos WHERE ID = {item[0]}");

                    //Validar cuando el Combo/Servicio tiene precio de Compra
                    if (productoPAquete.Rows[0]["Tipo"].ToString().Equals("PQ") || productoPAquete.Rows[0]["Tipo"].ToString().Equals("S"))
                    {
                        var idsProdCombo = cn.CargarDatos($"SELECT IDProducto, Cantidad FROM `productosdeservicios` WHERE IDServicio = {item[0]}"); //Si el combo no tiene productos se tomara el precio de compra y venta para calcular su posible ganancia
                        var idProd = idsProdCombo.Rows[0]["IDProducto"].ToString(); //Validar que se un producto y no vengan en 0 el id 
                        var precioPQ = cn.CargarDatos($"SELECT PrecioCompra FROM productos WHERE ID = {item[0]}"); //Consultar su el combo cuenta con un precio de compra

                        if (!idsProdCombo.Rows.Count.Equals(0) && precioPQ.Rows[0]["PrecioCompra"].ToString().Equals("0.00")) //Cuando el combo tiene productos y su precio de compra es 0
                        {
                            foreach (DataRow item2 in IdsProductVenta.Rows) //Foreach para recorrer los ids de productos que contiene el combo
                            {
                                var datos = cn.CargarDatos($"SELECT IDProducto, Cantidad FROM `productosdeservicios` WHERE IDServicio = {item2[0]}"); //Se obtienen los id y cantidad de cada producto que contiene el combo
                                if (!datos.Rows.Count.Equals(0))
                                {
                                    dtTablaIdsProducs = datos;
                                }
                            }
                            combosEliminar.Add(item[0].ToString());
                        }
                    }
                }

                if (!dtTablaIdsProducs.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in dtTablaIdsProducs.Rows)
                    {
                        var dato = cn.CargarDatos($"SELECT PrecioCompra FROM productos WHERE ID = {item[0]}");
                        if (!dato.Rows.Count.Equals(0))
                        {
                            var costoCompraProd = Convert.ToDecimal(dato.Rows[0]["PrecioCompra"].ToString());
                            decimal cantidadComprada = Convert.ToDecimal(item[1]);
                            precioPodsEnCombo += cantidadComprada * costoCompraProd;
                        }
                    }
                }

                decimal precioTotalDeCompra = 0;
                var VentaTotal = Convert.ToDecimal(totalDeVenta.Rows[0]["Total"]);
                int iterador = 0;
                foreach (DataRow item in IdsProductVenta.Rows)
                {
                    var precio = cn.CargarDatos($"SELECT PrecioCompra FROM productos WHERE ID = {item[0]}");
                    var cantidadComp = cn.CargarDatos($"SELECT Cantidad FROM productosventa WHERE IDVenta = {ListadoVentas.idGananciaVenta}");
                    if (!precio.Rows.Count.Equals(0))
                    {
                        decimal validacion = Convert.ToDecimal(precio.Rows[0]["PrecioCompra"]);
                        decimal cantidad = Convert.ToDecimal(cantidadComp.Rows[iterador]["Cantidad"]);
                        var tipo = cn.CargarDatos($"SELECT Tipo FROM productos WHERE ID = {item[0]}");

                        if (validacion.Equals(0) && tipo.Rows[0][0].Equals("P"))//Solo tomar en cuenta si no tiene precio de compra cuando sea un producto
                        {
                            lblMensaje.Visible = true;
                            lblGanancia.Text = "SIN PODER CALCULAR";
                            precioTotalDeCompra = 0;
                            //return;
                        }
                        else
                        {
                            precioTotalDeCompra = (validacion * cantidad);
                            VentaTotal = (VentaTotal - precioTotalDeCompra)-(precioPodsEnCombo);
                            lblGanancia.Text = (VentaTotal.ToString("C"));
                            iterador++;
                        }
                    }
                }
            }
            else if (lugarGanancia == 2)// GANANCIA POR ARTICULOS EN EL CARRITO----------------------------------------------------------------------------------------- 
            {
                var totalDeVenta = cn.CargarDatos($"SELECT Total FROM ventas WHERE ID = {ListadoVentas.idGananciaVenta}");
                List<string> productosEnCombos = new List<string>();
                DataTable dtTablaIdsProducs = new DataTable();
                List<string> combosEliminar = new List<string>();
                DataTable tablaIds = new DataTable();
                tablaIds.Columns.Add("ids");
                tablaIds.Columns.Add("Cantidad");

                foreach (var item in idsprods)
                {
                    string[] valores = item.Split('|');
                    //creas una nueva row
                    DataRow row = tablaIds.NewRow();
                    //asignas el dato a cada columna de la row
                    row["ids"] = valores[0];
                    row["Cantidad"] = valores[1];

                    tablaIds.Rows.Add(row);

                }

                foreach (DataRow item in tablaIds.Rows)
                {
                    var productoPAquete = cn.CargarDatos($"SELECT Tipo FROM productos WHERE ID = {item[0]}");

                    //Validar cuando el Combo/Servicio tiene precio de Compra
                    if (productoPAquete.Rows[0]["Tipo"].ToString().Equals("PQ") || productoPAquete.Rows[0]["Tipo"].ToString().Equals("S"))
                    {
                        var idProd = "";
                        var idsProdCombo = cn.CargarDatos($"SELECT IDProducto, Cantidad FROM `productosdeservicios` WHERE IDServicio = {item[0]}"); //Si el combo no tiene productos se tomara el precio de compra y venta para calcular su posible ganancia
                        if (!idsProdCombo.Rows.Count.Equals(0))
                        {
                            idProd = idsProdCombo.Rows[0]["IDProducto"].ToString(); //Validar que se un producto y no vengan en 0 el id      
                        }
                        
                        var precioPQ = cn.CargarDatos($"SELECT PrecioCompra FROM productos WHERE ID = {item[0]}"); //Consultar su el combo cuenta con un precio de compra

                        if (!idsProdCombo.Rows.Count.Equals(0) && !idProd.Equals("0") && precioPQ.Rows[0]["PrecioCompra"].ToString().Equals("0.00")) //Cuando el combo tiene productos y su precio de compra es 0
                        {
                            foreach (DataRow item2 in tablaIds.Rows) //Foreach para recorrer los ids de productos que contiene el combo
                            {
                                var datos = cn.CargarDatos($"SELECT IDProducto, Cantidad FROM `productosdeservicios` WHERE IDServicio = {item2[0]}"); //Se obtienen los id y cantidad de cada producto que contiene el combo
                                if (!datos.Rows.Count.Equals(0))
                                {
                                    dtTablaIdsProducs = datos;
                                }
                            }
                            combosEliminar.Add(item[0].ToString());
                        }
                    }
                }

                if (!dtTablaIdsProducs.Rows.Count.Equals(0))
                {
                    int contador = 0;

                    foreach (var item in dtTablaIdsProducs.Rows)
                    {
                        tablaIds.Rows.Add(dtTablaIdsProducs.Rows[contador]["IDProducto"], dtTablaIdsProducs.Rows[contador]["Cantidad"]);
                        contador++;
                    }


                    List<DataRow> rowsToDelete = new List<DataRow>();

                    foreach (DataRow row in tablaIds.Rows)
                    {
                        if (combosEliminar.Contains(row["ids"].ToString()))
                        {
                            rowsToDelete.Add(row);
                        }
                    }

                    foreach (DataRow row in rowsToDelete)
                    {
                        tablaIds.Rows.Remove(row);
                    }
                }

                decimal precioTotalDeCompra = 0;
                var VentaTotal = totalVenta;
                int iterador = 0;
                foreach (DataRow item in tablaIds.Rows)
                {
                    var precio = cn.CargarDatos($"SELECT PrecioCompra FROM productos WHERE ID = {item[0]}");
                    var cantidadComp = item[1];
                    if (!precio.Rows.Count.Equals(0))
                    {
                        decimal validacion = Convert.ToDecimal(precio.Rows[0]["PrecioCompra"]);
                        decimal cantidad = Convert.ToDecimal(cantidadComp);
                        var tipo = cn.CargarDatos($"SELECT Tipo FROM productos WHERE ID = {item[0]}");

                        if (validacion.Equals(0) && tipo.Rows[0][0].Equals("P"))//Solo tomar en cuenta si no tiene precio de compra cuando sea un producto
                        {
                            lblMensaje.Visible = true;
                            lblGanancia.Text = "SIN PODER CALCULAR";
                            precioTotalDeCompra = 0;
                            break;
                        }
                        else
                        {
                            precioTotalDeCompra = (validacion * cantidad);
                            VentaTotal = (VentaTotal - precioTotalDeCompra);
                            lblGanancia.Text = (VentaTotal.ToString("C"));
                            iterador++;
                        }
                    }
                }
            }
            if (gananciaGrafica == 3)
            {
                Ventas.gananciaTotalPorVenta = lblGanancia.Text;
                gananciaGrafica = 0;
                this.Close();
            }
        }
    }
}
