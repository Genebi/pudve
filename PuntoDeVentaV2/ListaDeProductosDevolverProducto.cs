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
    public partial class ListaDeProductosDevolverProducto : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();


        
        public ListaDeProductosDevolverProducto()
        {
            InitializeComponent();
        }

        private void ListaDeProductosDevolverProducto_Load(object sender, EventArgs e)
        {
            var Listproductos = cn.CargarDatos($"SELECT Cantidad, IDProducto, Nombre, Precio, descuento FROM productosventa WHERE IDVenta = '{DevolverProductoFolio.folio}'");
            DGVProductos.DataSource = Listproductos;

            if (DGVProductos.Columns.Contains("IDProducto"))
            {
                DGVProductos.Columns["IDProducto"].Visible = false;
            }

            this.Width =  DGVProductos.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) + 20;
            btnAceptar.Anchor = AnchorStyles.None; 
            int x = (this.Width - btnAceptar.Width) / 2;
            btnAceptar.Location = new Point(x, btnAceptar.Location.Y);
        }

        private void DGVProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (DGVProductos.Columns[e.ColumnIndex] is DataGridViewCheckBoxColumn && e.RowIndex >= 0)
            {
                foreach (DataGridViewRow row in DGVProductos.Rows)
                {
                    if (row.Index != e.RowIndex)
                    {
                        DataGridViewCheckBoxCell checkBoxCell = row.Cells[e.ColumnIndex] as DataGridViewCheckBoxCell;
                        checkBoxCell.Value = false;
                    }
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> selectedRows = new List<DataGridViewRow>();

            foreach (DataGridViewRow row in DGVProductos.Rows)
            {
                DataGridViewCheckBoxCell checkBoxCell = row.Cells["Devolver"] as DataGridViewCheckBoxCell;

                if (checkBoxCell.Value != null && (bool)checkBoxCell.Value)
                {
                    selectedRows.Add(row);
                }
            }

            foreach (DataGridViewRow row in selectedRows)
            {
                
                var descuento = row.Cells["descuento"].Value.ToString();

                if (descuento.Equals("0.00"))
                {
                    Inventario.totalImporte = row.Cells["Precio"].Value.ToString();
                }
                else 
                {
                    decimal cantidad = Convert.ToDecimal(row.Cells["Cantidad"].Value.ToString());
                    decimal precio = Convert.ToDecimal(row.Cells["Precio"].Value.ToString());
                    decimal descuento2 = Convert.ToDecimal(row.Cells["descuento"].Value.ToString());

                    decimal resultado = ((cantidad * precio) - descuento2) / cantidad;

                    Inventario.totalImporte = resultado.ToString();
                }
                Inventario.nombreProd = row.Cells["Nombre"].Value.ToString();
                Inventario.idProducto = row.Cells["IDProducto"].Value.ToString();
                Inventario.cantidadComprada = row.Cells["Cantidad"].Value.ToString();

            }
            this.Close();
        }
    }
}
