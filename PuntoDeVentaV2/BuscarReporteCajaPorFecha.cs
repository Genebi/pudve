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
    public partial class BuscarReporteCajaPorFecha : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        System.Drawing.Image pdf = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");

        public BuscarReporteCajaPorFecha()
        {
            InitializeComponent();
        }

        private void BuscarReporteCajaPorFecha_Load(object sender, EventArgs e)
        {
            cargarDGVInicial();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var datoBuscar = txtBuscador.Text;
        }

        private void cargarDGVInicial()
        {
            var fecha = string.Empty; var user = string.Empty; var idCorte = string.Empty;
            var consulta = cn.CargarDatos($"SELECT * FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC");

            if (!consulta.Rows.Count.Equals(0))
            {
                foreach (DataRow iterar in consulta.Rows)
                {
                    idCorte = iterar["ID"].ToString();
                    user = iterar["IDUsuario"].ToString();
                    fecha = iterar["FechaOPeracion"].ToString();

                    DGVReporteCaja.Rows.Add(idCorte, user, fecha, pdf, pdf, pdf);
                }
            }
        }

        private void cargarDGV()
        {

        }
    }
}
