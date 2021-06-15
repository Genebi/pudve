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
    public partial class HistorialPrecioBuscador : Form
    {
        Conexion cn = new Conexion();

        Dictionary<int, string> listaIdEmpleados = new Dictionary<int, string>();
        Dictionary<int, string> listaIdProductos = new Dictionary<int, string>();

        public static string idEmpleadoObtenido { get; set; }
        public static string procedencia { get; set; }

        string tipoBuscador = string.Empty;

        public HistorialPrecioBuscador(string tipoBusqueda)
        {
            InitializeComponent();
            this.tipoBuscador = tipoBusqueda;
            procedencia = tipoBuscador;
        }

        private void HistorialPrecioBuscador_Load(object sender, EventArgs e)
        {
            //idEmpleadoObtenido = "-1";
            idEmpleadoObtenido = string.Empty;

            this.Text = $"Buscar de {tipoBuscador}";
            lbTitulo.Text = $"Buscar {tipoBuscador}";

            if (tipoBuscador.Equals("Empleados"))
            {
                DGVDatosEmpleados.Visible = true;
                DGVDatosProductos.Visible = false;
                cargarEmpleados();
            }
            else if (tipoBuscador.Equals("Productos"))
            {
                DGVDatosEmpleados.Visible = false;
                DGVDatosProductos.Visible = true;
                cargarProductos();
            }
        }

        private void cargarEmpleados(bool porBusqueda = false)
        {
            DGVDatosEmpleados.Rows.Clear();
            //DGVDatos.Columns.Clear();
            //foreach (DataGridViewRow item in DGVDatos.Rows)
            //{
            //    if (DGVDatos.Columns.Count > 0)
            //    {
            //        DGVDatos.Columns.RemoveAt(DGVDatos.Columns.Count -1);
            //    }
            //}

            DataTable query = new DataTable();

            //DGVDatos.Columns.Add("ID", "ID");
            //DGVDatos.Columns.Add("Nombre", "Nombre");

            var empleadoBuscar = txtBuscar.Text;

            var consulta = $"SELECT ID, Nombre FROM Empleados WHERE IDUsuario = '{FormPrincipal.userID}' AND Estatus = 1 ";
            if (porBusqueda.Equals(false))
            {//Aqui va la consulta sin buscador
                consulta += "LIMIT 20";
            }
            else
            {//Aqui va la consulta con buscador 
                consulta += $"AND Nombre LIKE '%{empleadoBuscar}%' LIMIT 20";
            }

            query = cn.CargarDatos(consulta);

            if (!query.Rows.Count.Equals(0))
            {
                foreach (DataRow dgv in query.Rows)
                {
                    int filaId = DGVDatosEmpleados.Rows.Add();
                    DataGridViewRow fila = DGVDatosEmpleados.Rows[filaId];

                    fila.Cells["checkBox"].Value = false;
                    fila.Cells["ID"].Value = dgv["ID"].ToString();
                    fila.Cells["Nombre"].Value = dgv["Nombre"].ToString();
                }
            }

            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void cargarProductos(bool porBusqueda = false)
        {
            //DGVDatosProductos.Columns.Clear();
            DGVDatosEmpleados.Rows.Clear();

            DataTable query = new DataTable();

            //DGVDatosProductos.Columns.Add("ID", "ID");
            //DGVDatosProductos.Columns.Add("Nombre", "Nombre");
            //DGVDatosProductos.Columns.Add("Stock", "Stock");
            //DGVDatosProductos.Columns.Add("CBarras", "CodigoBarras");
            //DGVDatosProductos.Columns.Add("Tipo", "Tipo");

            //DGVDatosProductos.Columns[1].Width = 150;// definimos tamaño a la columna de Nombre
           
            var productoBuscar = txtBuscar.Text;

            var consulta = $"SELECT ID, Nombre, Stock, CodigoBarras, Tipo FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND Status = 1 ";

            if (porBusqueda.Equals(false))
            {//Aqui va la consulta sin buscador
                consulta += "LIMIT 20";
            }
            else
            {//Aqui va la consulta con buscador 
                consulta += $"AND Nombre LIKE '%{productoBuscar}%' LIMIT 20";
            }

            query = cn.CargarDatos(consulta);

            if (!query.Rows.Count.Equals(0))
            {
                var tipoProducto = string.Empty;
                foreach (DataRow dgv in query.Rows)
                {
                    //DGVDatosProductos.Rows.Add(dgv["ID"].ToString(), dgv["Nombre"].ToString(), dgv["Stock"].ToString(), dgv["CodigoBarras"].ToString(), dgv["Tipo"].ToString());
                    int filaId = DGVDatosProductos.Rows.Add();
                    DataGridViewRow fila = DGVDatosProductos.Rows[filaId];

                    fila.Cells["checkBoxProd"].Value = false;
                    fila.Cells["IDProducto"].Value = dgv["ID"].ToString();
                    fila.Cells["NombreProducto"].Value = dgv["Nombre"].ToString();
                    fila.Cells["Stock"].Value = dgv["Stock"].ToString();
                    fila.Cells["CodigoBarras"].Value = dgv["CodigoBarras"].ToString();

                    if (dgv["Tipo"].Equals("PQ"))
                    {
                        tipoProducto = "Combo";
                    }
                    else if (dgv["Tipo"].Equals("P"))
                    {
                        tipoProducto = "Producto";
                    }
                    else if (dgv["Tipo"].Equals("S"))
                    {
                        tipoProducto = "Servicio";
                    }

                    fila.Cells["tipo"].Value = tipoProducto;
                }
            }

            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var nombreBuscar = txtBuscar.Text;

            var porBusqueda = true;

            if (!string.IsNullOrEmpty(nombreBuscar))
            {
                if (tipoBuscador.Equals("Empleados"))
                {
                    cargarEmpleados(porBusqueda);
                }
                else if (tipoBuscador.Equals("Productos"))
                {
                    cargarProductos(porBusqueda);
                }
            }
        }

        private void btnBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            txtBuscar.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private void DGVDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //ejecutarMovimiento();
        }

        private void ejecutarMovimiento()
        {
            //Obtiene el id del empleado o de el producto.
            if (tipoBuscador.Equals("Empleados"))
            {
                //idEmpleadoObtenido = Convert.ToInt32(DGVDatosEmpleados.Rows[DGVDatosEmpleados.CurrentRow.Index].Cells[0].Value.ToString());

                var datosGet = recorrerDiccionario(listaIdEmpleados);

                idEmpleadoObtenido = datosGet;
            }
            else if (tipoBuscador.Equals("Productos"))
            {
                //idEmpleadoObtenido = Convert.ToInt32(DGVDatosProductos.Rows[DGVDatosProductos.CurrentRow.Index].Cells[0].Value.ToString());

                var datosGet = recorrerDiccionario(listaIdProductos);

                idEmpleadoObtenido = datosGet;

            }

            this.Close();
        }

        private void DGVDatosEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var fila = DGVDatosEmpleados.CurrentCell.RowIndex;

            int idVenta = Convert.ToInt32(DGVDatosEmpleados.Rows[fila].Cells["ID"].Value);

            if (e.ColumnIndex == 0)
            {
                var estado = Convert.ToBoolean(DGVDatosEmpleados.Rows[fila].Cells["checkbox"].Value);

                //En esta condicion se ponen 
                if (estado.Equals(false))//Se pone falso por que al dar click inicialmente esta en false
                {
                    if (!listaIdEmpleados.ContainsKey(idVenta))
                    {
                        listaIdEmpleados.Add(idVenta, string.Empty);
                    }
                }
                else if (estado.Equals(true))//Se pone verdadero por que al dar click inicialmente esta en true
                {
                    listaIdEmpleados.Remove(idVenta);
                }
            }
        }

        private void DGVDatosProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var fila = DGVDatosProductos.CurrentCell.RowIndex;

            int idVenta = Convert.ToInt32(DGVDatosProductos.Rows[fila].Cells["IDProducto"].Value);

            if (e.ColumnIndex == 0)
            {
                var estado = Convert.ToBoolean(DGVDatosProductos.Rows[fila].Cells["checkBoxProd"].Value);

                //En esta condicion se ponen 
                if (estado.Equals(false))//Se pone falso por que al dar click inicialmente esta en false
                {
                    if (!listaIdProductos.ContainsKey(idVenta))
                    {
                        listaIdProductos.Add(idVenta, string.Empty);
                    }
                }
                else if (estado.Equals(true))//Se pone verdadero por que al dar click inicialmente esta en true
                {
                    listaIdProductos.Remove(idVenta);
                }
            }
        }

        private string recorrerDiccionario(Dictionary<int, string> diccionario)
        {
            //Recorre el Diccionario y agregar todo en una sola cadena para la consulta
            var cadenaCompleta = string.Empty;
            foreach (KeyValuePair<int, string> item in diccionario)
            {
                cadenaCompleta += $"{item.Key},".ToString();
            }
            cadenaCompleta = cadenaCompleta.TrimEnd(',');

            return cadenaCompleta;
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            //if (tipoBuscador.Equals("Empleados"))
            //{

            //}
            //else if (tipoBuscador.Equals("Productos"))
            //{

            //}
            ejecutarMovimiento();
        }

        private void DGVDatosEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DGVDatosEmpleados.Columns[0].Index)
            {
                DataGridViewCheckBoxCell celda = (DataGridViewCheckBoxCell)this.DGVDatosEmpleados.Rows[e.RowIndex].Cells[0];

                if (Convert.ToBoolean(celda.Value) == false)
                {
                    celda.Value = true;
                    DGVDatosEmpleados.Rows[e.RowIndex].Selected = true;
                }
                else
                {
                    celda.Value = false;
                    DGVDatosEmpleados.Rows[e.RowIndex].Selected = false;
                }
            }
        }

        private void DGVDatosProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DGVDatosProductos.Columns[0].Index)
            {
                DataGridViewCheckBoxCell celda = (DataGridViewCheckBoxCell)this.DGVDatosProductos.Rows[e.RowIndex].Cells[0];

                if (Convert.ToBoolean(celda.Value) == false)
                {
                    celda.Value = true;
                    DGVDatosProductos.Rows[e.RowIndex].Selected = true;
                }
                else
                {
                    celda.Value = false;
                    DGVDatosProductos.Rows[e.RowIndex].Selected = false;
                }
            }
        }
    }
}
