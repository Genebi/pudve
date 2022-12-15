using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        int posicion;
        string annio;
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
            posicion = cbannio.SelectedIndex;
            annio = cbannio.Text;
            DeshabilitarBotones();
            Thread ProductDeleteSale = new Thread(() =>
             MessageBoxTemporal.Show("Este proceso tomara un tiempo", "Aviso del Sistema", 5, true)
            );
            ProductDeleteSale.Start();
            GenerarReporte();
            HabilitarBotones();
        }

        private void DeshabilitarBotones()
        {
            btn_aceptar.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;
            button10.Enabled = false;
            button11.Enabled = false;
        }

        private void GenerarReporte()
        {
            if (posicion.Equals(0))
            {
                MessageBox.Show("Selecciona un Año","Aviso del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Information);
                HabilitarBotones();
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
                    using (var DTFinal = cn.CargarDatos(cs.RetiroPorConcepto(conceptos, mes, annio)))
                    {
                        if (!DTFinal.Rows.Count.Equals(0) && !string.IsNullOrWhiteSpace(DTFinal.Rows[0][0].ToString()))
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

        private void HabilitarBotones()
        {
            btn_aceptar.Enabled = true;
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            button6.Enabled = true;
            button7.Enabled = true;
            button8.Enabled = true;
            button9.Enabled = true;
            button10.Enabled = true;
            button11.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mes = "2";
            posicion = cbannio.SelectedIndex;
            annio = cbannio.Text;
            DeshabilitarBotones();
            Thread ProductDeleteSale = new Thread(() =>
             MessageBoxTemporal.Show("Este proceso tomara un tiempo", "Aviso del Sistema", 5, true)
            );
            ProductDeleteSale.Start();
            GenerarReporte();
            HabilitarBotones();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mes = "3";
            posicion = cbannio.SelectedIndex;
            annio = cbannio.Text;
            DeshabilitarBotones();
            Thread ProductDeleteSale = new Thread(() =>
             MessageBoxTemporal.Show("Este proceso tomara un tiempo", "Aviso del Sistema", 5, true)
            );
            ProductDeleteSale.Start();
            GenerarReporte();
            HabilitarBotones();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            mes = "4";
            posicion = cbannio.SelectedIndex;
            annio = cbannio.Text;
            DeshabilitarBotones();
            Thread ProductDeleteSale = new Thread(() =>
             MessageBoxTemporal.Show("Este proceso tomara un tiempo", "Aviso del Sistema", 5, true)
            );
            ProductDeleteSale.Start();
            GenerarReporte();
            HabilitarBotones();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            mes = "5";
            posicion = cbannio.SelectedIndex;
            annio = cbannio.Text;
            DeshabilitarBotones();
            Thread ProductDeleteSale = new Thread(() =>
             MessageBoxTemporal.Show("Este proceso tomara un tiempo", "Aviso del Sistema", 5, true)
            );
            ProductDeleteSale.Start();
            GenerarReporte();
            HabilitarBotones();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            mes = "6";
            posicion = cbannio.SelectedIndex;
            annio = cbannio.Text;
            DeshabilitarBotones();
            Thread ProductDeleteSale = new Thread(() =>
             MessageBoxTemporal.Show("Este proceso tomara un tiempo", "Aviso del Sistema", 5, true)
            );
            ProductDeleteSale.Start();
            GenerarReporte();
            HabilitarBotones();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            mes = "7";
            posicion = cbannio.SelectedIndex;
            annio = cbannio.Text;
            DeshabilitarBotones();
            Thread ProductDeleteSale = new Thread(() =>
             MessageBoxTemporal.Show("Este proceso tomara un tiempo", "Aviso del Sistema", 5, true)
            );
            ProductDeleteSale.Start();
            GenerarReporte();
            HabilitarBotones();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            mes = "8";
            posicion = cbannio.SelectedIndex;
            annio = cbannio.Text;
            DeshabilitarBotones();
            Thread ProductDeleteSale = new Thread(() =>
             MessageBoxTemporal.Show("Este proceso tomara un tiempo", "Aviso del Sistema", 5, true)
            );
            ProductDeleteSale.Start();
            GenerarReporte();
            HabilitarBotones();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            mes = "9";
            posicion = cbannio.SelectedIndex;
            annio = cbannio.Text;
            DeshabilitarBotones();
            Thread ProductDeleteSale = new Thread(() =>
             MessageBoxTemporal.Show("Este proceso tomara un tiempo", "Aviso del Sistema", 5, true)
            );
            ProductDeleteSale.Start();
            GenerarReporte();
            HabilitarBotones();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mes = "10";
            posicion = cbannio.SelectedIndex;
            annio = cbannio.Text;
            DeshabilitarBotones();
            Thread ProductDeleteSale = new Thread(() =>
             MessageBoxTemporal.Show("Este proceso tomara un tiempo", "Aviso del Sistema", 5, true)
            );
            ProductDeleteSale.Start();
            GenerarReporte();
            HabilitarBotones();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            mes = "11";
            posicion = cbannio.SelectedIndex;
            annio = cbannio.Text;
            DeshabilitarBotones();
            Thread ProductDeleteSale = new Thread(() =>
             MessageBoxTemporal.Show("Este proceso tomara un tiempo", "Aviso del Sistema", 5, true)
            );
            ProductDeleteSale.Start();
            GenerarReporte();
            HabilitarBotones();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            mes = "12";
            posicion = cbannio.SelectedIndex;
            annio = cbannio.Text;
            DeshabilitarBotones();
            Thread ProductDeleteSale = new Thread(() =>
             MessageBoxTemporal.Show("Este proceso tomara un tiempo", "Aviso del Sistema", 5, true)
            );
            ProductDeleteSale.Start();
            GenerarReporte();
            HabilitarBotones();
        }
    }
}
