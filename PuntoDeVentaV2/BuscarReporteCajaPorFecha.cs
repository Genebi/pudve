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
            primerDatePicker.Value = DateTime.Today.AddDays(-30);
            segundoDatePicker.Value = DateTime.Now;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DGVReporteCaja.Rows.Clear();

            var datoBuscar = txtBuscador.Text.ToString().Replace("\r\n", string.Empty);
            var primerFecha = primerDatePicker.Value.ToString("yyyy/MM/dd");
            var segundaFecha = segundoDatePicker.Value.ToString("yyyy/MM/dd");

            var name = string.Empty; var fecha = string.Empty; var empleado = string.Empty; var idCorte = string.Empty; var idEmpleado = 0;
            var nombreUser = string.Empty;
            var querry = cn.CargarDatos(cs.BuscadorDeReportesCaja(datoBuscar, primerFecha, segundaFecha));

            if (!querry.Rows.Count.Equals(0))
            {
                foreach (DataRow iterar in querry.Rows)
                {
                    idCorte = iterar["ID"].ToString();
                    fecha = iterar["FechaOPeracion"].ToString();
                    idEmpleado = Convert.ToInt32(iterar["IdEmpleado"].ToString());
                    empleado = iterar["nombre"].ToString();
                    nombreUser = iterar["Usuario"].ToString();

                    if (idEmpleado > 0 )//Cuando es Empleado
                    {
                        name = empleado;
                        DGVReporteCaja.Rows.Add(idCorte, name, fecha, pdf, pdf, pdf);
                        
                    }
                    else if (idEmpleado.Equals(0)) //Cuando es Admin
                    {
                        name = $"ADMIN ({nombreUser})";
                        DGVReporteCaja.Rows.Add(idCorte, name, fecha, pdf, pdf, pdf);
                    }
                }
                txtBuscador.Text = string.Empty;
                txtBuscador.Focus();
            }
            else
            {
                MessageBox.Show($"No se encontraron resultados", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuscador.Text = string.Empty;
                txtBuscador.Focus();
            }
        }

        private void cargarDGVInicial()
        {//Cargar el DGV al Abrir el Form
            var primerFecha = primerDatePicker.Value.ToString("yyyy/MM/dd");
            var segundaFecha = segundoDatePicker.Value.ToString("yyyy/MM/dd");

            var name = string.Empty; var fecha = string.Empty; var empleado = string.Empty; var idCorte = string.Empty; var idEmpleado = 0;
            var consulta = cn.CargarDatos(cs.CargarDatosIniciarFormReportesCaja(primerFecha, segundaFecha));

            if (!consulta.Rows.Count.Equals(0))
            {
                foreach (DataRow iterar in consulta.Rows)
                {
                    idCorte = iterar["ID"].ToString();
                    fecha = iterar["FechaOPeracion"].ToString();
                    idEmpleado = Convert.ToInt32(iterar["IdEmpleado"].ToString());
                    empleado = iterar["nombre"].ToString();

                    if (idEmpleado > 0)
                    {
                        name = empleado;
                    }
                    else
                    {
                        name = $"ADMIN {FormPrincipal.userNickName.ToString()}";
                    }

                    //var empleado = validarEmpleado(Convert.ToInt32(obtenerEmpleado));

                    DGVReporteCaja.Rows.Add(idCorte, name, fecha, pdf, pdf, pdf);
                }
            }
        }

        private void txtBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            txtBuscador.CharacterCasing = CharacterCasing.Upper;
        }

        private void BuscarReporteCajaPorFecha_Shown(object sender, EventArgs e)
        {
            txtBuscador.Focus();
        }

        private void DGVReporteCaja_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(3))//Corte de Caja
            {
                MessageBox.Show("Corte de Caja");
            }
            else if (e.ColumnIndex.Equals(4))//Dinero Agregado
            {

            }
            else if (e.ColumnIndex.Equals(5))//Dinero Retirado
            {

            }
        }


    }
}
