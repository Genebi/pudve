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

        string tipoBuscador = string.Empty;

        public HistorialPrecioBuscador(string tipoBusqueda)
        {
            InitializeComponent();
            this.tipoBuscador = tipoBusqueda;
        }

        private void HistorialPrecioBuscador_Load(object sender, EventArgs e)
        {
            if (tipoBuscador.Equals("Empleados"))
            {
                cargarEmpleados();
            }
            else if (tipoBuscador.Equals("Productos"))
            {
                cargarProductos();
            }
        }

        private void cargarEmpleados()
        {
            DGVDatos.Columns.Add("ID", "ID");
            DGVDatos.Columns.Add("Nombre", "Nombre");

            var query = cn.CargarDatos($"SELECT ID, Nombre FROM Empleados WHERE IDUsuario = '{FormPrincipal.userID}' AND Estatus = 1 LIMIT 20");

            if (!query.Rows.Count.Equals(0))
            {
                foreach (DataRow dgv in query.Rows)
                {
                    DGVDatos.Rows.Add(dgv["ID"].ToString(), dgv["Nombre"].ToString());
                }
            }
        }

        private void cargarProductos()
        {
            DGVDatos.Columns.Clear();

            DGVDatos.Columns.Add("ID", "ID");
            DGVDatos.Columns.Add("Nombre", "Nombre");
            DGVDatos.Columns.Add("Stock", "Stock");
            DGVDatos.Columns.Add("CBarras", "CodigoBarras");
            DGVDatos.Columns.Add("Tipo", "Tipo");

            //DGVDatos.Columns[0].Width = 10;
            DGVDatos.Columns[1].Width = 250;
            //DGVDatos.Columns[2].Width = 10;
            //DGVDatos.Columns[3].Width = 10;
            //DGVDatos.Columns[4].Width = 15;

            //DGVDatos.Columns.Add("", "");

            var query = cn.CargarDatos($"SELECT ID, Nombre, Stock, CodigoBarras, Tipo FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND Status = 1 LIMIT 20");

            if (!query.Rows.Count.Equals(0))
            {
                foreach (DataRow dgv in query.Rows)
                {
                    DGVDatos.Rows.Add(dgv["ID"].ToString(), dgv["Nombre"].ToString(), dgv["Stock"].ToString(), dgv["CodigoBarras"].ToString(), dgv["Tipo"].ToString());
        
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var nombreBuscar = txtBuscar.Text;

            if (!string.IsNullOrEmpty(nombreBuscar))
            {

            }
        }

        private void btnBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }
    }
}
