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
            ID_Producto = IDProducto;
            string queryDetalle = $"SELECT usr.ID AS 'No de Usuario', usr.NombreCompleto AS 'Nombre', usr.Regimen AS 'Regimen', usr.TipoPersona AS 'Tipo de Persona', prod.ID AS 'No de Producto', prod.Nombre AS 'Nombre de Producto', prod.Stock AS 'Existencia', prod.Precio AS 'Precio', prod.Categoria AS 'Categoria', prod.CodigoBarras AS 'Codigo  de Barra', prod.TipoDescuento AS 'Tipo de Descuento', prodServ.ID AS 'No de Reg Servicio', prodServ.Fecha As 'Fecha del Resgistro', prodServ.IDServicio AS 'Numero de Servicio', prod.Nombre AS 'Nombre del Serv/Paq', prodServ.IDProducto AS 'Numero de Producto', prodServ.Cantidad AS 'Cantidad del Producto', prodserv.NombreProducto AS 'Concepto del Producto' FROM Usuarios AS usr LEFT JOIN Productos AS prod ON prod.IDUsuario = usr.ID LEFT JOIN ProductosDeServicios AS prodServ ON prodServ.IDServicio = prod.ID WHERE usr.ID = '{FormPrincipal.userID}' AND prod.ID = '{ID_Producto}'";
            dtProductoDescripcion = cn.CargarDatos(queryDetalle);
            dataGridView1.DataSource = dtProductoDescripcion;
        }
    }
}
