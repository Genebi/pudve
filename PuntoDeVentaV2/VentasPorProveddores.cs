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
    public partial class VentasPorProveddores : Form
    {
        Conexion cn = new Conexion();

        public VentasPorProveddores()
        {
            InitializeComponent();
        }

        private void VentasPorProveddores_Load(object sender, EventArgs e)
        {
            CargarDatos();
         
        }

        private void CargarDatos()
        {
            string FechaInicial = string.Empty;
            string FechaFinal = string.Empty;
            using (var DTFecha = cn.CargarDatos($"SELECT FechaOperacion from ventas where Cliente = 'Apertura de Caja' AND IDUsuario = {FormPrincipal.userID} ORDER BY ID DESC LIMIT 1"))
            {
                if (!DTFecha.Rows.Count.Equals(0))
                {
                    FechaInicial = DTFecha.Rows[0]["FechaOperacion"].ToString();
                }
                else
                {
                    MessageBox.Show("Aun no puede realizar esta opcion", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            FechaFinal = DateTime.Now.ToString();
            string IDSVentas = string.Empty;
            using (var DTidsVentas = cn.CargarDatos($"SELECT ID FROM Ventas WHERE IDUsuario = {FormPrincipal.userID} AND FechaOperacion BETWEEN '{FechaInicial}' AND '{FechaFinal}'"))
            {
                if (!DTidsVentas.Rows.Count.Equals(0))
                {
                    int contador = 0;
                    foreach (var item in DTidsVentas.Rows)
                    {
                        IDSVentas += DTidsVentas.Rows[contador]["ID"].ToString() + ",";
                        contador++;
                    }
                }
            }
            IDSVentas = IDSVentas.TrimEnd(',');
        }
    }
}
