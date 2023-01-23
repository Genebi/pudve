using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using iTextSharp.text.pdf.draw;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class ReporteCaja : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        private string operacion = string.Empty;
        private int status = 0;

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

            // Seleccionar horas por defecto
            dtpHoraInicio.Text = "00:00:00";
            dtpHoraFin.Text = "23:59:59";

            // Hacer que se puedan marcar los checkbox con un solo click
            clbConceptos.CheckOnClick = true;
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
                    AgregarConcepto(concepto);
                }

                if (!cbTodos.Visible) cbTodos.Visible = true;
                if (!clbConceptos.Visible) clbConceptos.Visible = true;
                if (!panelFechaHora.Visible) panelFechaHora.Visible = true;
            }

            status = 1;
        }

        private void rbDeshabilitados_CheckedChanged(object sender, EventArgs e)
        {
            var conceptos = mb.ObtenerConceptosDinamicos(0, "CAJA", true);

            if (conceptos.Count > 0)
            {
                clbConceptos.Items.Clear();

                foreach (var concepto in conceptos)
                {
                    AgregarConcepto(concepto);
                }

                if (!cbTodos.Visible) cbTodos.Visible = true;
                if (!clbConceptos.Visible) clbConceptos.Visible = true;
                if (!panelFechaHora.Visible) panelFechaHora.Visible = true;
            }

            status = 0;
        }

        private void AgregarConcepto(KeyValuePair<int, string> concepto)
        {
            ListBoxItem cb = new ListBoxItem();
            cb.Text = concepto.Value;
            cb.Tag = concepto.Key;

            clbConceptos.Items.Add(cb);
        }

        private string ObtenerOperacion()
        {
            operacion = rbDineroAgregado.Checked ? "deposito" : "retiro";

            return operacion;
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            var seleccionados = clbConceptos.CheckedItems;

            if (seleccionados.Count > 0)
            {
                string fechaInicio = dpFechaInicial.Text;
                string fechaFin = dpFechaFinal.Text;

                string primeraHora = dtpHoraInicio.Text;
                string segundaHora = dtpHoraFin.Text;

                string primeraFecha = $"{fechaInicio} {primeraHora}";
                string segundaFecha = $"{fechaFin} {segundaHora}";


                if (!Utilidades.AdobeReaderInstalado())
                {
                    Utilidades.MensajeAdobeReader();
                    return;
                }


                string queryConceptos = "IN (";

                foreach (ListBoxItem concepto in seleccionados)
                {
                    queryConceptos += $"'{concepto.Text}',";
                }

                queryConceptos = queryConceptos.Remove(queryConceptos.Length - 1);
                queryConceptos += ")";

                VisualizadorReporteCaja visualizador = new VisualizadorReporteCaja();
                visualizador.PrimeraFecha = primeraFecha;
                visualizador.SegundaFecha = segundaFecha;
                visualizador.Conceptos = queryConceptos;
                visualizador.Operacion = ObtenerOperacion();
                visualizador.Status = status.ToString();
                visualizador.ShowDialog();
                
            }
        }

        private void cbTodos_CheckedChanged(object sender, EventArgs e)
        {
            int cantidadCheckbox = clbConceptos.Items.Count;

            if (cbTodos.Checked)
            {
                for (int i = 0; i < cantidadCheckbox; i++)
                {
                    clbConceptos.SetItemChecked(i, true);
                }
            }
            else
            {
                for (int i = 0; i < cantidadCheckbox; i++)
                {
                    clbConceptos.SetItemChecked(i, false);
                }
            }
        }
    }

    public class ListBoxItem
    {
        public string Text { get; set; }
        public object Tag { get; set; }
        public override string ToString()
        {
            return Text;
        }
    }
}
