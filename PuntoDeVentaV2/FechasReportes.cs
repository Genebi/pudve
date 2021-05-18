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
    public partial class FechasReportes : Form
    {
        MetodosBusquedas mb = new MetodosBusquedas();
        Conexion cn = new Conexion();

        public string concepto { get; set; }
        public string fechaInicial { get; set; }
        public string fechaFinal { get; set; }

        private string origen = string.Empty;

        public FechasReportes(string origen = "")
        {
            InitializeComponent();

            this.origen = origen;
        }

        private void FechasReportes_Load(object sender, EventArgs e)
        {
            primerDatePicker.Value = DateTime.Today.AddDays(-30);

            if (!string.IsNullOrEmpty(origen))
            {
                var conceptos = mb.ObtenerConceptosDinamicos(origen: origen);

                cbConceptos.Visible = true;
                cbConceptos.DataSource = conceptos.ToArray();
                cbConceptos.DisplayMember = "Value";
                cbConceptos.ValueMember = "Key";
            }

            cargarDatosCombo();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var tipoBusqurda = cbEmpleados.SelectedItem.ToString();
            HistorialPrecioBuscador hpBuscador = new HistorialPrecioBuscador(tipoBusqurda);

            if (tipoBusqurda.Equals("Seleccionar Empleado/Producto"))
            {
                terminarOperaciones();
            }
            else
            {
                hpBuscador.ShowDialog();

                hpBuscador.FormClosed += delegate
                {

                    terminarOperaciones();
                };
            }

        }

        private void cargarDatosCombo()
        {
            cbEmpleados.Items.Add("Seleccionar Empleado/Producto");
            cbEmpleados.Items.Add("Empleados");
            cbEmpleados.Items.Add("Productos");

            cbEmpleados.SelectedIndex = 0;
            //var query = cn.CargarDatos($"SELECT Nombre FROM Empleados WHERE IDUsuario = '{FormPrincipal.userID}'");
            //cbEmpleados.Items.Add("Seleccionar concepto...");
            //if (!query.Rows.Count.Equals(0))
            //{
            //    foreach (DataRow empleados in query.Rows)
            //    {
            //        cbEmpleados.Items.Add(empleados["Nombre"].ToString());
            //    }
            //}
        }

        private void terminarOperaciones()
        {
            concepto = cbConceptos.GetItemText(cbConceptos.SelectedItem);
            fechaInicial = primerDatePicker.Value.ToString("yyyy-MM-dd");
            fechaFinal = segundoDatePicker.Value.ToString("yyyy-MM-dd");

            DialogResult = DialogResult.OK;
            Reportes.botonAceptar = true;
            Close();
        }
    }
}
