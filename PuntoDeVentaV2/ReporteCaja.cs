using DocumentFormat.OpenXml.Office2010.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class ReporteCaja : Form
    {
        MetodosBusquedas mb = new MetodosBusquedas();

        private string operacion = string.Empty;

        public ReporteCaja()
        {
            InitializeComponent();
        }

        private void ReporteCaja_Load(object sender, EventArgs e)
        {
            Dictionary<int, int> years = new Dictionary<int, int>();
            years.Add(2019, 2019);
            years.Add(2020, 2020);
            years.Add(2021, 2021);
            years.Add(2022, 2022);
            years.Add(2023, 2023);

            Dictionary<string, string> meses = new Dictionary<string, string>();
            meses.Add("01", "ENERO");
            meses.Add("02", "FEBRERO");
            meses.Add("03", "MARZO");
            meses.Add("04", "ABRIL");
            meses.Add("05", "MAYO");
            meses.Add("06", "JUNIO");
            meses.Add("07", "JULIO");
            meses.Add("08", "AGOSTO");
            meses.Add("09", "SEPTIEMBRE");
            meses.Add("10", "OCTUBRE");
            meses.Add("11", "NOVIEMBRE");
            meses.Add("12", "DICIEMBRE");

            cbYear.DataSource = years.ToArray();
            cbYear.ValueMember = "Key";
            cbYear.DisplayMember = "Value";

            cbMonth.DataSource = meses.ToArray();
            cbMonth.ValueMember = "Key";
            cbMonth.DisplayMember = "Value";

            // Seleccionamos año por defecto
            cbYear.SelectedValue = DateTime.Now.Year;


            cbYear.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbMonth.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
        }

        private void rbDineroAgregado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDineroAgregado.Checked || rbDineroRetirado.Checked)
            {
                rbHabilitados.Enabled = true;
                rbDeshabilitados.Enabled = true;

                ObtenerOperacion();
            }
        }

        private void rbDineroRetirado_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDineroAgregado.Checked || rbDineroRetirado.Checked)
            {
                rbHabilitados.Enabled = true;
                rbDeshabilitados.Enabled = true;

                ObtenerOperacion();
            }
        }

        private void rbHabilitados_CheckedChanged(object sender, EventArgs e)
        {
            var conceptos = mb.ObtenerConceptosDinamicos(origen: "CAJA", reporte: true);

            if (conceptos.Count > 0)
            {
                clbConceptos.Items.Clear();

                foreach (var concepto in conceptos)
                {
                    clbConceptos.Items.Add(concepto.Value);
                }

                if (!cbTodos.Visible) cbTodos.Visible = true;
                if (!clbConceptos.Visible) clbConceptos.Visible = true;
            }
        }

        private void rbDeshabilitados_CheckedChanged(object sender, EventArgs e)
        {
            var conceptos = mb.ObtenerConceptosDinamicos(0, "CAJA", true);

            if (conceptos.Count > 0)
            {
                clbConceptos.Items.Clear();

                foreach (var concepto in conceptos)
                {
                    clbConceptos.Items.Add(concepto.Value);
                }

                if (!cbTodos.Visible) cbTodos.Visible = true;
                if (!clbConceptos.Visible) clbConceptos.Visible = true;
            }
        }

        private string ObtenerOperacion()
        {
            operacion = rbDineroAgregado.Checked ? "deposito" : "retiro";

            return operacion;
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Test");
        }
    }
}
