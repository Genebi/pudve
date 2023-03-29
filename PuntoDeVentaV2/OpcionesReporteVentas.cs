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
        public static bool MostrarGraficaVentas = true;
        public static bool MostrarTablaGraficasVenta = true;
        public static bool SiVentasDesglosadas = false;
        public static bool MostrarTablaProveedoers = true;
        public static bool MostrarGraficaProveedoes = true;
        string CodigoBarras = "";
        int tipoVenta = 0;
        Conexion cn = new Conexion();


        public OpcionesReporteVentas(string codigosBuscar, int tipoV)
        {
            InitializeComponent();
            this.CodigoBarras = codigosBuscar;
            this.tipoVenta = tipoV;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (cbkGraficaVentas.Checked.Equals(false) && cbkTablaGraicaVentas.Checked.Equals(false) && checkBox1.Checked.Equals(false) && cbkTablaProveedores.Checked.Equals(false) && cblGraficaProveedores.Checked.Equals(false))
            {
                MessageBox.Show("Sellecione almenos una opcion", "Aviso del Sistema", MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
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
                SiVentasDesglosadas = true;
            }
            else
            {
                SiVentasDesglosadas = false;
            }
        }

        private void OpcionesReporteVentas_Load(object sender, EventArgs e)
        {
            MostrarGraficaVentas = true;
            MostrarTablaGraficasVenta = true;
            SiVentasDesglosadas = false;
            MostrarTablaProveedoers = true;
            MostrarGraficaProveedoes = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbkTablaProveedores_CheckedChanged(object sender, EventArgs e)
        {
            if (cbkTablaGraicaVentas.Checked.Equals(true))
            {
                MostrarTablaProveedoers = true;
            }
            else
            {
                MostrarTablaProveedoers = false;
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (cblGraficaProveedores.Checked.Equals(true))
            {
                MostrarGraficaProveedoes = true;
            }
            else
            {
                MostrarGraficaProveedoes = false;
            }
        }

        private void cbkTablaGraicaVentas_CheckedChanged(object sender, EventArgs e)
        {
            if (cbkTablaGraicaVentas.Checked.Equals(true))
            {
                MostrarTablaGraficasVenta = true;
            }
            else
            {
                MostrarTablaGraficasVenta = false;
            }
        }

        private void cbkGraficaVentas_CheckedChanged(object sender, EventArgs e)
        {
            if (cbkGraficaVentas.Checked.Equals(true))
            {
                MostrarGraficaVentas = true;
            }
            else
            {
                MostrarGraficaVentas = false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
