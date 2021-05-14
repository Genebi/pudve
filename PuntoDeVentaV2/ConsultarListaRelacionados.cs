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
    public partial class ConsultarListaRelacionados : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public string tipoOperacion = string.Empty;
        string tipo = string.Empty;
        public string[] listaServCombo;
        public List<string> listaProd = new List<string>();

        private void cargarDatos()
        {
            tipo = tipoOperacion;
        }

        private void verificarTipoYLlenadoDataGridView()
        {
            if (tipo.Equals("AGREGAR PRODUCTO"))
            {
                lblTitulo.Text = "Relación de Combos / Servicios";
                llenarDatosProductos();
            }
            if (tipo.Equals("AGREGAR COMBOS"))
            {
                lblTitulo.Text = "Relación de Productos";
                llenarDatosServicioCombo();
            }
            if (tipo.Equals("AGREGAR SERVICIOS"))
            {
                lblTitulo.Text = "Relación de Productos";
                llenarDatosServicioCombo();
            }
            
            if (tipo.Equals("EDITAR PRODUCTO") || tipo.Equals("COPIAR PRODUCTO"))
            {
                lblTitulo.Text = "Relación de Combos / Servicios";
                llenarDatosProductos();
            }

            if (tipo.Equals("EDITAR COMBOS") || tipo.Equals("COPIAR COMBOS"))
            {
                lblTitulo.Text = "Relación de Productos";
                llenarDatosServicioCombo();
            }

            if (tipo.Equals("EDITAR SERVICIOS") || tipo.Equals("COPIAR SERVICIOS"))
            {
                lblTitulo.Text = "Relación de Productos";
                llenarDatosServicioCombo();
            }
        }
        
        private void llenarDatosProductos()
        {
            using (AgregarEditarProducto addEditProd = new AgregarEditarProducto())
            {
                if(listaServCombo.Count().Equals(1))
                {
                    if (!listaServCombo[0].ToString().Equals(string.Empty))
                    {
                        DGVProdServCombo.Rows.Clear();
                        foreach (var item in listaServCombo)
                        {
                            var words = item.Split('|');
                            var numberOfRows = DGVProdServCombo.Rows.Add();
                            DataGridViewRow row = DGVProdServCombo.Rows[numberOfRows];

                            string Fecha = words[0].ToString();
                            string IDServicio = words[1].ToString();
                            string IDProducto = words[2].ToString();
                            string NombreProducto = words[3].ToString();
                            string Cantidad = words[4].ToString();

                            row.Cells["Fecha"].Value = Fecha;                       // Columna Fecha
                            row.Cells["IDServicio"].Value = IDServicio;             // Columna IDServicio
                            using (DataTable dtServComb = cn.CargarDatos(cs.obtenerServicioCombo(Convert.ToInt32(IDServicio))))
                            {
                                if (!dtServComb.Rows.Count.Equals(0))
                                {
                                    foreach(DataRow drServComb in dtServComb.Rows)
                                    {
                                        row.Cells["ServicioCombo"].Value = drServComb["Nombre"].ToString();
                                    }
                                }
                            }
                            row.Cells["IDProducto"].Value = IDProducto;             // Columna IDProducto
                            row.Cells["NombreProducto"].Value = NombreProducto;     // Columna NombreProducto
                            row.Cells["Cantidad"].Value = Cantidad;                 // Columna Cantidad
                        }
                    }
                }
            }
            DGVProdServCombo.Columns[0].Visible = false;
            DGVProdServCombo.Columns[1].Visible = false;
            DGVProdServCombo.Columns[3].Visible = false;
            DGVProdServCombo.Columns[4].Visible = false;
        }

        private void llenarDatosServicioCombo()
        {
            using (AgregarEditarProducto addEditProd = new AgregarEditarProducto())
            {
                //listaProd = addEditProd.ProductosDeServicios;
                if (!listaProd.Count.Equals(0))
                {
                    DGVProdServCombo.Rows.Clear();
                    foreach(var item in listaProd)
                    {
                        var words = item.Split('|');
                        var numberOfRows = DGVProdServCombo.Rows.Add();
                        DataGridViewRow row = DGVProdServCombo.Rows[numberOfRows];

                        string Fecha = words[0].ToString();
                        string IDServicio = words[1].ToString();
                        string IDProducto = words[2].ToString();
                        string NombreProducto = words[3].ToString();
                        string Cantidad = words[4].ToString();

                        row.Cells["Fecha"].Value = Fecha;                       // Columna Fecha
                        row.Cells["IDServicio"].Value = IDServicio;             // Columna IDServicio
                        row.Cells["IDProducto"].Value = IDProducto;             // Columna IDProducto
                        row.Cells["NombreProducto"].Value = NombreProducto;     // Columna NombreProducto
                        row.Cells["Cantidad"].Value = Cantidad;                 // Columna Cantidad
                    }
                }
            }
            DGVProdServCombo.Columns[0].Visible = false;
            DGVProdServCombo.Columns[1].Visible = false;
            DGVProdServCombo.Columns[2].Visible = false;
            DGVProdServCombo.Columns[3].Visible = false;
        }

        public ConsultarListaRelacionados()
        {
            InitializeComponent();
        }

        private void ConsultarListaRelacionados_Load(object sender, EventArgs e)
        {
            cargarDatos();
            verificarTipoYLlenadoDataGridView();
        }
    }
}
