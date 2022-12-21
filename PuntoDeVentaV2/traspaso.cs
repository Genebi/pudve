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

        private void DGVTraspaso_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex>=0)
            //{
            //    if (e.ColumnIndex==4)
            //    {
            //    string seleccion =(DGVTraspaso[e.ColumnIndex, e.RowIndex+1].Value.ToString());
            //        if (seleccion=="Manual")
            //        {
            //            consultarProductoTraspaso consultaProducto = new consultarProductoTraspaso();
            //            consultaProducto.ShowDialog();
            //        }
                     
            //    }
            //}
        }

        private void DGVTraspaso_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //if (init)
            //{
            //    if (e.RowIndex >= 0)
            //    {
            //        if (e.ColumnIndex == 5)
            //        {
            //            string seleccion = (DGVTraspaso[e.ColumnIndex, e.RowIndex].Value.ToString());
            //            if (seleccion == "Manual")
            //            {
            //                ConsultarProductoTraspaso consultarProducto = new ConsultarProductoTraspaso();

            //                consultarProducto.FormClosed += delegate
            //                {
            //                    if (!ID.Equals(""))
            //                    {
            //                        DataTable dt = new DataTable();
            //                        dt = cn.CargarDatos($"SELECT * FROM Productos WHERE ID = {ID}");
            //                        DGVTraspaso["NombreL", e.RowIndex].Value = dt.Rows[0]["Nombre"].ToString();
            //                        DGVTraspaso["CodigoL", e.RowIndex].Value = dt.Rows[0]["CodigoBarras"].ToString();
            //                        DGVTraspaso["PCompra", e.RowIndex].Value = dt.Rows[0]["PrecioCompra"].ToString();
            //                        DGVTraspaso["PVenta", e.RowIndex].Value = dt.Rows[0]["Precio"].ToString();
            //                    }

            //                    ID = "";
            //                };
            //                consultarProducto.ShowDialog();
            //            }

            //        }
            //    }
            //}

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
                if (!Convert.ToBoolean(row.Cells["Omitir"].Value))
                {
                    Inventario.productosTraspaso.Add($"{row.Cells["NombreL"].Value.ToString()}%{row.Cells["CodigoL"].Value.ToString()}%{row.Cells["PCompra"].Value.ToString()}%{row.Cells["PVenta"].Value.ToString()}%{row.Cells["CantidadT"].Value.ToString()}");
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
    }
}
