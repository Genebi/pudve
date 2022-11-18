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
            var IdsProductVenta = cn.CargarDatos($"SELECT IDProducto FROM productosventa WHERE IDVenta = {ListadoVentas.idGananciaVenta}");
            var totalDeVenta = cn.CargarDatos($"SELECT Total FROM ventas WHERE ID = {ListadoVentas.idGananciaVenta}");
            List<string> productosEnCombos = new List<string>();
            List<string> combosEliminar = new List<string>();


            foreach (DataRow item in IdsProductVenta.Rows)
            {
                var productoPAquete = cn.CargarDatos($"SELECT Tipo FROM productos WHERE ID = {item[0]}");
                if (productoPAquete.Rows[0]["Tipo"].ToString().Equals("PQ"))
                {
                    var idsProdCombo = cn.CargarDatos($"SELECT IDProducto FROM `productosdeservicios` WHERE IDServicio = {item[0]}");
                    for (int i = 0; i < idsProdCombo.Rows.Count; i++)
                    {
                        productosEnCombos.Add(idsProdCombo.Rows[i][0].ToString());
                    }
                    combosEliminar.Add(item[0].ToString());
                }
            }

            if (!productosEnCombos.Count.Equals(0))
            {
                for (int i = 0; i < productosEnCombos.Count; i++)
                {
                    IdsProductVenta.Rows.Add(productosEnCombos[i]);
                }


                List<DataRow> rowsToDelete = new List<DataRow>();

                foreach (DataRow row in IdsProductVenta.Rows)
                {
                    if (combosEliminar.Contains(row["IDProducto"].ToString()))
                    {
                        rowsToDelete.Add(row);
                    }
                }

                foreach (DataRow row in rowsToDelete)
                {
                    IdsProductVenta.Rows.Remove(row);
                }
            }

            decimal precioTotalDeCompra = 0;
            foreach (DataRow item in IdsProductVenta.Rows)
            {
                int iterador = 0;
                var precio = cn.CargarDatos($"SELECT PrecioCompra FROM productos WHERE ID = {item[0]}");
                var cantidadComp = cn.CargarDatos($"SELECT Cantidad FROM productosventa WHERE IDVenta = {ListadoVentas.idGananciaVenta}");
                decimal validacion = Convert.ToDecimal(precio.Rows[0]["PrecioCompra"]);
                decimal cantidad = Convert.ToDecimal(cantidadComp.Rows[iterador]["Cantidad"]);

                if (validacion.Equals(Convert.ToDecimal(0.00)))
                {
                    lblGanancia.Text = "SIN PODER CALCULAR";
                    precioTotalDeCompra = 0;
                    return;
                }
                else
                {
                    var VentaTotal = Convert.ToDecimal(totalDeVenta.Rows[0]["Total"]);
                    precioTotalDeCompra = precioTotalDeCompra + (validacion * cantidad);
                    lblGanancia.Text = (VentaTotal - precioTotalDeCompra).ToString("C");
                    iterador++;
                }

            }
        }
    }
}
