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
            var name = string.Empty; var fecha = string.Empty; var user = string.Empty; var idCorte = string.Empty;  var idEmpleado = 0;
            var consulta = cn.CargarDatos($"SELECT CJ.ID, CJ.FechaOperacion, CJ.IdEmpleado, EMP.nombre FROM Caja AS CJ LEFT JOIN Empleados AS EMP ON CJ.IdEmpleado = EMP.ID WHERE CJ.IDUsuario = '14' AND CJ.Operacion = 'corte' ORDER BY CJ.FechaOperacion DESC; ");

            if (!consulta.Rows.Count.Equals(0))
            {
                foreach (DataRow iterar in consulta.Rows)
                {
                    idCorte = iterar["ID"].ToString();
                    fecha = iterar["FechaOPeracion"].ToString();
                    idEmpleado = Convert.ToInt32(iterar["IdEmpleado"].ToString());
                    user = iterar["nombre"].ToString();

                    if (idEmpleado > 0)
                    {
                        name = user;
                    }
                    else
                    {
                        name = FormPrincipal.userNickName.ToString();
                    }

                    //var empleado = validarEmpleado(Convert.ToInt32(obtenerEmpleado));

                    DGVReporteCaja.Rows.Add(idCorte, name, fecha, pdf, pdf, pdf);
                }
            }
        }

        private string validarEmpleado(int idEmpleado)
        {
            var empleado = string.Empty;
            var query = cn.CargarDatos($"");



            return empleado;
        }
    }
}
