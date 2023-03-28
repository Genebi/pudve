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
    public partial class OpcionesReporteVentas : Form
    {
        public static bool SiVentas = false;
        string CodigoBarras = "";
        int tipoVenta = 0;
        Conexion cn = new Conexion();


        public OpcionesReporteVentas(string codigosBuscar,int tipoV)
        {
            InitializeComponent();
            this.CodigoBarras = codigosBuscar;
            this.tipoVenta = tipoV;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CodigoBarras))
            {
                //Se quita el * de la consulta para obtener solo los campos que me interesan y se guarda en una nueva variable
                //var ajustarQuery = FiltroAvanzado.Replace("*", "Cliente, RFC, IDEmpleado, Total, Folio, Serie, FechaOperacion");
                using (var dt = cn.CargarDatos($"SELECT Ganancia,Cliente FROM ventas WHERE ID IN ({CodigoBarras})"))
                {
                    int contador = 0;
                    foreach (var item in dt.Rows)
                    {
                        if (!dt.Rows[contador]["Cliente"].ToString().Equals("Apertura de Caja"))
                        {
                            if (dt.Rows[contador]["Ganancia"].ToString().Equals("SIN PODER CALCULAR") || string.IsNullOrWhiteSpace(dt.Rows[contador]["Ganancia"].ToString()))
                            {
                                MessageBox.Show("No se podra calcular la Ganancia", "Aviso de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                break;
                            }
                        }
                        contador++;
                    }
                }
            }
            VisualizadorReporteVentas VRV = new VisualizadorReporteVentas(CodigoBarras, tipoVenta);
            VRV.ShowDialog();
            this.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked.Equals(true))
            {
                SiVentas = true;
            }
            else
            {
                SiVentas = false;
            }
        }

        private void OpcionesReporteVentas_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
