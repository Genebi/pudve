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
    }
}
