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
    public partial class ReportesRetirosConceptosPorMes : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        string mes = "";
        decimal total = 0;
        DataTable DTDatos = new DataTable();
        public ReportesRetirosConceptosPorMes()
        {
            InitializeComponent();
        }

        private void ReportesRetirosConceptosPorMes_Load(object sender, EventArgs e)
        {
            DTDatos.Columns.Add("Concepto", typeof(String));
            DTDatos.Columns.Add("Fecha", typeof(String));
            DTDatos.Columns.Add("Cantidad", typeof(String));
            DTDatos.Columns.Add("Status", typeof(String));
            cbannio.SelectedIndex = 0;
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            mes = "1";
            GenerarReporte();
        }

        private void GenerarReporte()
        {
            if (cbannio.SelectedIndex.Equals(0))
            {
                MessageBox.Show("Selecciona un Año","Aviso del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Information);
                cbannio.Focus();
                return;
            }
            string conceptos = string.Empty;
            using (var DTConceptos = cn.CargarDatos($"SELECT ID,Concepto FROM conceptosdinamicos WHERE IDUsuario = {FormPrincipal.userID}"))
            {
                int contador = 0;
                int contador2 = 0;
                foreach (var item in DTConceptos.Rows)
                {
                    conceptos = DTConceptos.Rows[contador]["Concepto"].ToString();
                    using (var DTFinal = cn.CargarDatos(cs.RetiroPorConcepto(conceptos, mes, cbannio.Text)))
                    {
                        if (!DTFinal.Rows.Count.Equals(0))
                        {
                            int contador3 = 0;
                            foreach (var DT in DTFinal.Rows)
                            {
                                DTDatos.Rows.Add();
                                DTDatos.Rows[contador2]["Concepto"] = DTFinal.Rows[contador3]["Concepto"].ToString();
                                DTDatos.Rows[contador2]["Fecha"] = DTFinal.Rows[contador3]["Fecha"].ToString();
                                DTDatos.Rows[contador2]["Cantidad"] = DTFinal.Rows[contador3]["Cantidad"].ToString();
                                string Status;
                                if (DTFinal.Rows[contador3]["Status"].ToString().Equals("1"))
                                {
                                    Status = "HABILITADO";
                                }
                                else
                                {
                                    Status = "DESHABILITADO";
                                }
                                DTDatos.Rows[contador2]["Status"] = Status;
                                contador2 ++;
                                contador3++;
                            }
                        }
                      
                    }
                    contador++;
                }
            }
            int contadorCantidad = 0;
            if (DTDatos.Rows.Count.Equals(0))
            {
                MessageBox.Show("No se encuentran registros en esta Fecha","Aviso del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            foreach (var item in DTDatos.Rows)
            {
                decimal cantidades = Convert.ToDecimal(DTDatos.Rows[contadorCantidad]["Cantidad"]);
                total += cantidades;
                contadorCantidad++;
            }

            FormReporteConceptosRetiro form = new FormReporteConceptosRetiro(DTDatos,total.ToString()); ;
            form.ShowDialog();
            total = 0;
            DTDatos.Clear();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mes = "2";
            GenerarReporte();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mes = "3";
            GenerarReporte();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mes = "4";
            GenerarReporte();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mes = "5";
            GenerarReporte();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mes = "6";
            GenerarReporte();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mes = "7";
            GenerarReporte();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            mes = "8";
            GenerarReporte();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mes = "9";
            GenerarReporte();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mes = "10";
            GenerarReporte();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            mes = "11";
            GenerarReporte();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            mes = "12";
            GenerarReporte();
        }
    }
}
