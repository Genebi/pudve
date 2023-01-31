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
    public partial class traspaso : Form
    {

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        bool init = false;
        public static string ID="";
        public List<string> updatesSubdetalles = new List<string>();
        public traspaso(DataTable datosTraspaso)
        {
            InitializeComponent();
            DGVTraspaso.DataSource = datosTraspaso;
        }

        private void traspaso_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DGVTraspaso.Rows)
            {

                DataTable dt = new DataTable();
                dt = cn.CargarDatos($"SELECT * FROM productos WHERE `Status` = 1 AND CodigoBarras = '{(row.Cells[8].Value.ToString())}' AND IDUsuario = {FormPrincipal.userID}");
                if (!dt.Rows.Count.Equals(0))
                {
                    row.Cells["NombreL"].Value = dt.Rows[0]["Nombre"].ToString();
                    row.Cells["CodigoL"].Value = dt.Rows[0]["CodigoBarras"].ToString();
                    row.Cells["PCompra"].Value = dt.Rows[0]["PrecioCompra"].ToString();
                    row.Cells["PVenta"].Value = dt.Rows[0]["Precio"].ToString();
                }
                else
                {
                    row.Cells["NombreL"].Value = "";
                    row.Cells["CodigoL"].Value = "";
                    row.Cells["PCompra"].Value = "";
                    row.Cells["PVenta"].Value = "";
                }

            }
            init = true;
        }



        private void bntTerminar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in DGVTraspaso.Rows)
            {

                //Buscar en blanco
                if (row.Cells["CodigoL"].Value.ToString().Equals(""))
                {
                    MessageBox.Show("No puedes dejar entradas en blanco, también puedes ómitir si lo deseas");
                    return;
                }
                if (!verificarSubDetalles(row))
                {
                    MessageBox.Show("Este producto requiere un ajuste de subdetalles", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!Convert.ToBoolean(row.Cells["Omitir"].Value))
                {
                    DataTable buscarCombo = new DataTable();
                    int IDProducto = Int32.Parse(cn.CargarDatos(cs.BuscarIDPreductoPorCodigoDeBarras(row.Cells["CodigoL"].Value.ToString())).Rows[0]["ID"].ToString());
                    buscarCombo = cn.CargarDatos($"SELECT * FROM productosdeservicios WHERE IDServicio = {IDProducto}");
                    if (buscarCombo.Rows.Count.Equals(0))
                    {
                        Inventario.productosTraspaso.Add($"{row.Cells["NombreL"].Value.ToString()}%{row.Cells["CodigoL"].Value.ToString()}%{row.Cells["PCompra"].Value.ToString()}%{row.Cells["PVenta"].Value.ToString()}%{row.Cells["CantidadT"].Value.ToString()}");
                    }
                    else
                    {
                        //Es un combo
                        foreach (DataRow relacionCombo in buscarCombo.Rows)
                        {
                            DataTable productoRelacioando = new DataTable();
                            productoRelacioando = cn.CargarDatos($"SELECT * FROM Productos WHERE ID = {relacionCombo["IDProducto"].ToString()}");
                            decimal cantidad = Decimal.Parse(relacionCombo["Cantidad"].ToString()) * Decimal.Parse(row.Cells["CantidadT"].Value.ToString());
                            Inventario.productosTraspaso.Add($"{productoRelacioando.Rows[0]["Nombre"].ToString()}%{productoRelacioando.Rows[0]["CodigoBarras"].ToString()}%{productoRelacioando.Rows[0]["PrecioCompra"].ToString()}%{productoRelacioando.Rows[0]["Precio"].ToString()}%{cantidad.ToString()}");
                        }
                    }
                }
            }
            this.Close();
        }

        private void DGVTraspaso_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (init)
                {
                    if (e.RowIndex >= 0)
                    {
                        if (e.ColumnIndex == 5)
                        {

                            ConsultarProductoTraspaso consultarProducto = new ConsultarProductoTraspaso();

                            consultarProducto.FormClosed += delegate
                            {
                                if (!ID.Equals(""))
                                {
                                    DataTable dt = new DataTable();
                                    dt = cn.CargarDatos($"SELECT * FROM Productos WHERE ID = {ID}");
                                    DGVTraspaso["NombreL", e.RowIndex].Value = dt.Rows[0]["Nombre"].ToString();
                                    DGVTraspaso["CodigoL", e.RowIndex].Value = dt.Rows[0]["CodigoBarras"].ToString();
                                    DGVTraspaso["PCompra", e.RowIndex].Value = dt.Rows[0]["PrecioCompra"].ToString();
                                    DGVTraspaso["PVenta", e.RowIndex].Value = dt.Rows[0]["Precio"].ToString();
                                }

                                ID = "";
                            };
                            consultarProducto.ShowDialog();

                        }
                    }
                }
            }
        }

        private bool verificarSubDetalles(DataGridViewRow ProductoDGV)
        {
            bool registroCorrectoDeSubdetalles = true;

            //foreach (DataGridViewRow ProductoDGV in DGVTraspaso.Rows)
            //{
                if (!Convert.ToBoolean(ProductoDGV.Cells["Omitir"].Value))
                {
                    using (DataTable ids = cn.CargarDatos($"SELECT ID,Stock FROM Productos WHERE CodigoBarras = {ProductoDGV.Cells["CodigoL"].Value.ToString()} AND Status = 1 AND IDUsuario = {FormPrincipal.userID}"))
                    {

                        subDetallesDeProducto detalles = new subDetallesDeProducto(ids.Rows[0]["ID"].ToString(), "Inventario", cantidad: decimal.Parse(ProductoDGV.Cells["CantidadT"].Value.ToString()));
                        detalles.FormClosed += delegate
                        {
                            if (!detalles.finalizado)
                            {
                                registroCorrectoDeSubdetalles = detalles.finalizado;
                                updatesSubdetalles.Clear();
                            }
                            else
                            {
                                updatesSubdetalles.AddRange(detalles.updates);
                            }
                        };
                        detalles.ShowDialog();
                    }
                }
            return registroCorrectoDeSubdetalles;
        }
    }
}
