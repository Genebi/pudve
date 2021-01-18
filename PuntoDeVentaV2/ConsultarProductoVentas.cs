using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PuntoDeVentaV2
{
    public partial class ConsultarProductoVentas : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();
        private List<string> propiedades = new List<string>();

        public static List<string> datosDeProducto = new List<string>();
        public static int idABuscar { get; set; }

        public ConsultarProductoVentas()
        {
            InitializeComponent();
        }

        private void ConsultarProductoVentas_Load(object sender, EventArgs e)
        {
            GenerarColumnas();
        }

        private void GenerarColumnas()
        {
            var conceptos = mb.ConceptosAppSettings();

            foreach (var concepto in conceptos)
            {
                if (concepto == "Proveedor")
                {
                    continue;
                }

                // Este valor de proveedor esta agregado por defecto
                DataGridViewColumn columna = new DataGridViewTextBoxColumn();
                columna.HeaderText = concepto;
                columna.Name = concepto;
                DGVProductos.Columns.Add(columna);

                // Guardamos los nombres de las propiedades en la lista
                propiedades.Add(concepto);
            }
        }

        private void ConsultarProductoVentas_Shown(object sender, EventArgs e)
        {
            txtBuscar.Focus();
        }

        private void BuscarProductos()
        {
            var busqueda = txtBuscar.Text.Trim();

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                var coincidencias = mb.BusquedaCoincidenciasVentas(busqueda);

                if (coincidencias.Count > 0)
                {
                    DGVProductos.Rows.Clear();

                    foreach (var producto in coincidencias)
                    {
                        var datos = mb.ProductoConsultadoVentas(producto.Key, propiedades);

                        AgregarProducto(datos);
                    }
                }
                else
                {
                    MessageBox.Show($"No se encontraron productos con {txtBuscar.Text}","Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void AgregarProducto(Dictionary<string, string> datos)
        {
            if (datos.Count > 0)
            {
                int rowId = DGVProductos.Rows.Add();
                DataGridViewRow row = DGVProductos.Rows[rowId];

                foreach (var propiedad in datos)
                {
                    var valor = propiedad.Value;

                    if (propiedad.Key == "Tipo")
                    {
                        if (valor == "P") { valor = "PRODUCTO"; }
                        if (valor == "S") { valor = "SERVICIO"; }
                        if (valor == "PQ") { valor = "PAQUETE"; }
                    }

                    if (propiedad.Key == "Precio")
                    {
                        var precio = float.Parse(valor);
                        valor = precio.ToString("0.00");
                    }

                    if (propiedad.Key == "Stock")
                    {
                        valor = Utilidades.RemoverCeroStock(valor);
                    }

                    row.Cells[propiedad.Key].Value = valor;

                    //DGVProductos.Focus();
                    //DGVProductos.CurrentRow.Selected = true;
                    DGVProductos.ClearSelection();
                }
            }
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                BuscarProductos();
            }

            if (e.KeyCode == Keys.Down && !DGVProductos.Rows.Count.Equals(0))
            {
                DGVProductos.Focus();
                DGVProductos.CurrentRow.Selected = true;
            }
        }

        private void ConsultarProductoVentas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void DGVProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up && DGVProductos.CurrentRow.Index == 0)
            {
                txtBuscar.Focus();
                DGVProductos.ClearSelection();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                obtenerDatoProductoSeleccionado();
            }
        }

        private void obtenerDatoProductoSeleccionado()
        {
            datosDeProducto.Clear();

            if (!DGVProductos.Rows.Count.Equals(0))
            {
                var codigoProd = DGVProductos.CurrentRow.Cells[4].Value.ToString();
                //idABuscar = Convert.ToInt32(idProd);

                var datosProducto = cn.CargarDatos($"SELECT ID, Nombre, Precio, TipoDescuento, Stock, Tipo, ClaveInterna, CodigoBarras, StockNecesario, ProdImage, StockMinimo, PrecioCompra, PrecioMayoreo, Impuesto, Categoria, ProdImage, ClaveProducto, UnidadMedida  FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND CodigoBarras = '{codigoProd}'");

                var id = string.Empty; var nombre = string.Empty; var precio = string.Empty; var tipoDescuento = string.Empty; var stock = string.Empty; var tipo = string.Empty; var claveInterna = string.Empty; var codigoBarras = string.Empty; var stockNecesario = string.Empty; var prodImage = string.Empty; var stockMinimo = string.Empty; var precioCompra = string.Empty; var precioMayoreo = string.Empty; var impuesto = string.Empty; var categoria = string.Empty; var prodimage = string.Empty; var claveProducto = string.Empty; var unidadMedida = string.Empty;

                if (!datosProducto.Rows.Count.Equals(0))
                {
                    id = datosProducto.Rows[0]["ID"].ToString();
                    nombre = datosProducto.Rows[0]["Nombre"].ToString();
                    precio = datosProducto.Rows[0]["Precio"].ToString();
                    tipoDescuento = datosProducto.Rows[0]["TipoDescuento"].ToString();
                    stock = datosProducto.Rows[0]["Stock"].ToString();
                    tipo = datosProducto.Rows[0]["Tipo"].ToString();
                    claveInterna = datosProducto.Rows[0]["ClaveInterna"].ToString();
                    codigoBarras = datosProducto.Rows[0]["CodigoBarras"].ToString();
                    stockNecesario = datosProducto.Rows[0]["StockNecesario"].ToString();
                    prodImage = datosProducto.Rows[0]["ProdImage"].ToString();
                    stockMinimo = datosProducto.Rows[0]["StockMinimo"].ToString();
                    precioCompra = datosProducto.Rows[0]["PrecioCompra"].ToString();
                    precioMayoreo = datosProducto.Rows[0]["PrecioMayoreo"].ToString();
                    impuesto = datosProducto.Rows[0]["Impuesto"].ToString();
                    categoria = datosProducto.Rows[0]["Categoria"].ToString();
                    prodimage = datosProducto.Rows[0]["ProdImage"].ToString();
                    claveProducto = datosProducto.Rows[0]["ClaveProducto"].ToString();
                    unidadMedida = datosProducto.Rows[0]["UnidadMedida"].ToString();

                }
                datosProducto.Clear();

                datosDeProducto.Add(id);
                datosDeProducto.Add(nombre);
                datosDeProducto.Add(precio);
                datosDeProducto.Add(tipoDescuento);
                datosDeProducto.Add(stock);
                datosDeProducto.Add(tipo);
                datosDeProducto.Add(claveInterna);
                datosDeProducto.Add(codigoBarras);
                datosDeProducto.Add(stockNecesario);
                datosDeProducto.Add(prodImage);
                datosDeProducto.Add(stockMinimo);
                datosDeProducto.Add(precioCompra);
                datosDeProducto.Add(precioMayoreo);
                datosDeProducto.Add(impuesto);
                datosDeProducto.Add(categoria);
                datosDeProducto.Add(prodimage);
                datosDeProducto.Add(claveProducto);
                datosDeProducto.Add(unidadMedida);

                this.Close();
            }
        }

        private void DGVProductos_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            obtenerDatoProductoSeleccionado();
        }
    }
}
