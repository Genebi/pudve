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
    public partial class DetalleDescripcion : Form
    {
        Conexion cn = new Conexion();

        static public string ID_Producto;

        public string IDProducto { set; get; }

        DataTable dtProductoDescripcion;

        public DetalleDescripcion()
        {
            InitializeComponent();
        }

        private void DetalleDescripcion_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void cargarDatos()
        {
            
        }

        public void limpiarDGV()
        {
            //if (DGVDetalle.DataSource is DataTable)
            //{
            //    ((DataTable)DGVDetalle.DataSource).Rows.Clear();
            //    DGVDetalle.Refresh();
            //}
        }
    }
}
