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
    public partial class RecordViewProduct : Form
    {
        Conexion cn = new Conexion();

        DataTable dtRecordProducto, dt;

        private int HighlightedRowIndex = -1;
        private DataGridViewCellStyle HighlightStyle;

        string queryRecord, buscar, Id_Prod_select;

        int index = 0;

        public string nombreProd { get; set; }
        public string stockProd { get; set; }
        public string precioProd { get; set; }
        public string claveInternaProd { get; set; }
        public string codigoBarrasProd { get; set; }
        public string idUsuarioProd { get; set; }

        public static string Nombre;
        public static string Stock;
        public static string Precio;
        public static string ClaveInterna;
        public static string CodigoBarras;
        public static string IdUsuario;

        public void llenarDatos()
        {
            if (DGVProductRecord.RowCount > 0)
            {
                lblFolioCompra.Text = DGVProductRecord[0, 0].Value.ToString();
                lblRFCProveedor.Text = DGVProductRecord[1, 0].Value.ToString();
                lblNombreProveedor.Text = DGVProductRecord[2, 0].Value.ToString();
                lblClaveProducto.Text = DGVProductRecord[3, 0].Value.ToString();
                lblFechaCompletaCompra.Text = DGVProductRecord[4, 0].Value.ToString();
                lblCantidadCompra.Text = DGVProductRecord[5, 0].Value.ToString();
                lblValorUnitarioProducto.Text = DGVProductRecord[6, 0].Value.ToString();
                lblDescuentoProducto.Text = DGVProductRecord[7, 0].Value.ToString();
                lblPrecioCompra.Text = DGVProductRecord[8, 0].Value.ToString();
            }
        }

        private void DGVProductRecord_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            else
            {

            }
        }

        private void DGVProductRecord_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            else
            {
                lblFolioCompra.Text = DGVProductRecord[0, e.RowIndex].Value.ToString();
                lblRFCProveedor.Text = DGVProductRecord[1, e.RowIndex].Value.ToString();
                lblNombreProveedor.Text = DGVProductRecord[2, e.RowIndex].Value.ToString();
                lblClaveProducto.Text = DGVProductRecord[3, e.RowIndex].Value.ToString();
                lblFechaCompletaCompra.Text = DGVProductRecord[4, e.RowIndex].Value.ToString();
                lblCantidadCompra.Text = DGVProductRecord[5, e.RowIndex].Value.ToString();
                lblValorUnitarioProducto.Text = DGVProductRecord[6, e.RowIndex].Value.ToString();
                lblDescuentoProducto.Text = DGVProductRecord[7, e.RowIndex].Value.ToString();
                lblPrecioCompra.Text = DGVProductRecord[8, e.RowIndex].Value.ToString();
                lblEmpleado.Text = DGVProductRecord[9, e.RowIndex].Value.ToString();
            }
        }

        public void cargarDatos()
        {
            Nombre = nombreProd;
            Stock = stockProd;
            Precio = precioProd;
            ClaveInterna = claveInternaProd;
            CodigoBarras = codigoBarrasProd;
            IdUsuario = idUsuarioProd;

            lblNombre.Text = Nombre;
            lblStock.Text = Stock;
            lblPrecio.Text = Precio;
            lblClaveInterna.Text = ClaveInterna;
            lblCodigoBarras.Text = CodigoBarras;

            // Preparamos el Query que haremos segun la fila seleccionada
            //buscar = $"SELECT * FROM Productos WHERE Nombre = '{Nombre}' AND Precio = '{Precio}' AND Stock = '{Stock}' AND ClaveInterna = '{ClaveInterna}' AND CodigoBarras = '{CodigoBarras}' AND IDUsuario = '{IdUsuario}'";
            buscar = $"SELECT * FROM Productos WHERE Nombre = '{Nombre}' AND Precio = '{Precio}' AND ClaveInterna = '{ClaveInterna}' AND CodigoBarras = '{CodigoBarras}' AND IDUsuario = '{IdUsuario}'";
            dt = cn.CargarDatos(buscar);    // almacenamos el resultado de la Funcion CargarDatos que esta en la calse Consultas
            Id_Prod_select = dt.Rows[index]["ID"].ToString();       // almacenamos el Id del producto

            queryRecord = $"SELECT hcompras.Folio AS 'Folio', hcompras.RFCEmisor AS 'RFC', hcompras.NomEmisor AS 'Proveedor', hcompras.ClaveProdEmisor AS 'Clave de Producto', hcompras.FechaLarga AS 'Fecha', hcompras.Cantidad AS 'Cantidad', hcompras.ValorUnitario AS 'Precio de Compra', hcompras.Descuento AS 'Descuento', hcompras.Precio AS 'Precio de Venta', IF(hcompras.IDEmpleado =0,'Admin',empleados.nombre) AS Empleado FROM HistorialCompras hcompras LEFT JOIN empleados ON (hcompras.IDEmpleado = empleados.id) WHERE hcompras.IDUsuario = '{IdUsuario}' AND hcompras.IDProducto = '{Id_Prod_select}' AND hcompras.Cantidad>0 ORDER BY Folio DESC;";
            dtRecordProducto = cn.CargarDatos(queryRecord);
            DGVProductRecord.DataSource = dtRecordProducto;
            DGVProductRecord.Sort(DGVProductRecord.Columns["Fecha"], ListSortDirection.Descending);
            SeleccionarFila();
            llenarDatos();
        }

        private void DGVProductRecord_CellLeave(object sender, DataGridViewCellEventArgs e)
        {

        }
        

        private void DGVProductRecord_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                return;
            }
            else
            {
                lblFolioCompra.Text = DGVProductRecord[0, e.RowIndex].Value.ToString();
                lblRFCProveedor.Text = DGVProductRecord[1, e.RowIndex].Value.ToString();
                lblNombreProveedor.Text = DGVProductRecord[2, e.RowIndex].Value.ToString();
                lblClaveProducto.Text = DGVProductRecord[3, e.RowIndex].Value.ToString();
                lblFechaCompletaCompra.Text = DGVProductRecord[4, e.RowIndex].Value.ToString();
                lblCantidadCompra.Text = DGVProductRecord[5, e.RowIndex].Value.ToString();
                lblValorUnitarioProducto.Text = DGVProductRecord[6, e.RowIndex].Value.ToString();
                lblDescuentoProducto.Text = DGVProductRecord[7, e.RowIndex].Value.ToString();
                lblPrecioCompra.Text = DGVProductRecord[8, e.RowIndex].Value.ToString();
            }
        }
        // Set the cell Styles in the given row.
        private void SetRowStyle(DataGridViewRow row, DataGridViewCellStyle style)
        {
            foreach (DataGridViewCell cell in row.Cells)
            {
                cell.Style = style;
            }
        }

        private void DGVProductRecord_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            HighlightedRowIndex = e.RowIndex;
            if (HighlightedRowIndex >= 0)
            {
                SetRowStyle(DGVProductRecord.Rows[HighlightedRowIndex], HighlightStyle);
            }
        }

        private void DGVProductRecord_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (HighlightedRowIndex >= 0)
            {
                SetRowStyle(DGVProductRecord.Rows[HighlightedRowIndex], null);
                HighlightedRowIndex = -1;
            }
        }

        private void RecordViewProduct_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }

        public void SeleccionarFila()
        {
            if (DGVProductRecord.RowCount > 0)
            {
                DGVProductRecord.MultiSelect = false;
                DGVProductRecord.MultiSelect = true;
                DGVProductRecord.Rows[0].Selected = true;
            }
        }

        public RecordViewProduct()
        {
            InitializeComponent();
        }

        private void RecordViewProduct_Load(object sender, EventArgs e)
        {
            cargarDatos();
            SeleccionarFila();

            // Define the highlight style.
            HighlightStyle = new DataGridViewCellStyle();
            HighlightStyle.ForeColor = Color.Blue;
            HighlightStyle.BackColor = Color.Beige;
            HighlightStyle.Font = new System.Drawing.Font(DGVProductRecord.Font, FontStyle.Bold);
        }
    }
}
