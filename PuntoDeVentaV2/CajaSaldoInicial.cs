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
    public partial class CajaSaldoInicial : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public int IDUsuario { get; set; }
        public int IDEmpleado { get; set; }
        public string ultimaFechaDeCorteDeCaja { get; set; }
        public string todosLosAbonos { get; set; }

        public CajaSaldoInicial()
        {
            InitializeComponent();
        }

        private void CajaSaldoInicial_Load(object sender, EventArgs e)
        {
            if (todosLosAbonos.Equals("Si"))
            {
                List<int> IDsDeCroteDeCaja = new List<int>();
                List<int> IDEmpleados = new List<int>();

                using (DataTable dtIDsEpleados = cn.CargarDatos(cs.cargarIDsDeEmpleados()))
                {
                    if (!dtIDsEpleados.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in dtIDsEpleados.Rows)
                        {
                            IDEmpleados.Add(Convert.ToInt32(item["ID"].ToString()));
                        }
                    }
                }

                var noEstaVacia = IsEmpty(IDEmpleados);

                if (noEstaVacia)
                {
                    var resultadoIDEmpleados = string.Join(",", IDEmpleados);

                    using (DataTable dtcargarNuevoSaldoInicial = cn.CargarDatos(cs.cargarNuevoSaldoInicial(resultadoIDEmpleados)))
                    {
                        if (!dtcargarNuevoSaldoInicial.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtcargarNuevoSaldoInicial.Rows)
                            {
                                IDsDeCroteDeCaja.Add(Convert.ToInt32(item["IDCaja"].ToString()));
                            }
                        }
                    }
                }

                noEstaVacia = IsEmpty(IDsDeCroteDeCaja);

                if (noEstaVacia)
                {
                    using (DataTable dtResultadoConcentradooHistorialCorteDeCaja = cn.CargarDatos(cs.resultadoConcentradooHistorialCorteDeCaja(IDsDeCroteDeCaja.ToArray())))
                    {
                        if (!dtResultadoConcentradooHistorialCorteDeCaja.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtResultadoConcentradooHistorialCorteDeCaja.Rows)
                            {
                                lbEfectivoAbonos.Text = convertirCantidadHaciaDecimal(item["Efectivo"].ToString()).ToString("C2");
                                lbTarjetaAbonos.Text = convertirCantidadHaciaDecimal(item["Tarjeta"].ToString()).ToString("C2");
                                lbValesAbonos.Text = convertirCantidadHaciaDecimal(item["Vales"].ToString()).ToString("C2");
                                lbChequeAbonos.Text = convertirCantidadHaciaDecimal(item["Cheque"].ToString()).ToString("C2");
                                lbTransferenciaAbonos.Text = convertirCantidadHaciaDecimal(item["Transferencia"].ToString()).ToString("C2");
                                lbTCreditoC.Text = convertirCantidadHaciaDecimal(item["SaldoInicial"].ToString()).ToString("C2");
                            }
                        }
                    }
                }
            }
            else if (todosLosAbonos.Equals("No"))
            {
                if (IDEmpleado > 0)
                {
                    using (DataTable dtEmpleadoSaldoInicial = cn.CargarDatos(cs.cargarSaldoInicialEmpleado(IDEmpleado.ToString())))
                    {
                        if (!dtEmpleadoSaldoInicial.Rows.Equals(0))
                        {
                            foreach (DataRow item in dtEmpleadoSaldoInicial.Rows)
                            {
                                lbEfectivoAbonos.Text = convertirCantidadHaciaDecimal(item["Efectivo"].ToString()).ToString("C2");
                                lbTarjetaAbonos.Text = convertirCantidadHaciaDecimal(item["Tarjeta"].ToString()).ToString("C2");
                                lbValesAbonos.Text = convertirCantidadHaciaDecimal(item["Vales"].ToString()).ToString("C2");
                                lbChequeAbonos.Text = convertirCantidadHaciaDecimal(item["Cheque"].ToString()).ToString("C2");
                                lbTransferenciaAbonos.Text = convertirCantidadHaciaDecimal(item["Transferencia"].ToString()).ToString("C2");
                                lbTCreditoC.Text = convertirCantidadHaciaDecimal(item["SaldoInicial"].ToString()).ToString("C2");
                            }
                        }
                    }
                }
                else
                {
                    using (DataTable dtAdminstradorSaldoInicial = cn.CargarDatos(cs.cargarSaldoInicialAdministrador()))
                    {
                        if (!dtAdminstradorSaldoInicial.Rows.Equals(0))
                        {
                            foreach (DataRow item in dtAdminstradorSaldoInicial.Rows)
                            {
                                lbEfectivoAbonos.Text = convertirCantidadHaciaDecimal(item["Efectivo"].ToString()).ToString("C2");
                                lbTarjetaAbonos.Text = convertirCantidadHaciaDecimal(item["Tarjeta"].ToString()).ToString("C2");
                                lbValesAbonos.Text = convertirCantidadHaciaDecimal(item["Vales"].ToString()).ToString("C2");
                                lbChequeAbonos.Text = convertirCantidadHaciaDecimal(item["Cheque"].ToString()).ToString("C2");
                                lbTransferenciaAbonos.Text = convertirCantidadHaciaDecimal(item["Transferencia"].ToString()).ToString("C2");
                                lbTCreditoC.Text = convertirCantidadHaciaDecimal(item["SaldoInicial"].ToString()).ToString("C2");
                            }
                        }
                    }
                }
            }
        }

        private bool IsEmpty(List<int> iDEmpleados)
        {
            var isEmpty = true;

            if (iDEmpleados.Equals(null))
            {
                isEmpty = false;
            }

            return isEmpty;
        }

        private decimal convertirCantidadHaciaDecimal(string cantidad)
        {
            decimal cantidadParaConvertir = 0;

            if (!string.IsNullOrWhiteSpace(cantidad))
            {
                cantidadParaConvertir = Convert.ToDecimal(cantidad);
            }
            else
            {
                cantidadParaConvertir = 0;
            }

            return cantidadParaConvertir;
        }

        private void CajaSaldoInicial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }
    }
}
