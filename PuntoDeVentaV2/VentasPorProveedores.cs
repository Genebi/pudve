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
    public partial class VentasPorProveedores : Form
    {
        Conexion cn = new Conexion();
        public VentasPorProveedores()
        {
            InitializeComponent();
        }

        private void VentasPorProveedores_Load(object sender, EventArgs e)
        {
            string FechaInicial = string.Empty;
            using (var DTFecha = cn.CargarDatos($"SELECT FechaOperacion FROM ventas WHERE IDUsuario = {FormPrincipal.userID} AND Cliente = 'Apertura de Caja' ORDER BY ID DESC LIMIT 1"))
            {
                FechaInicial = DTFecha.Rows[0]["FechaOperacion"].ToString();
            }
            string FechaFinal = DateTime.Now.ToString();

            using (var DTIdsVenta = cn.CargarDatos($"SELECT ID FROM VENTAS WHERE FechaOperacion '{FechaInicial}' BETWEEN '{FechaFinal}' AND IDUsuario = {FormPrincipal.userID} AND Cliente != 'Apertura de Caja'"))
            {

            }
        }
    }
}
