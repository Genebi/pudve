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
    public partial class CajaAbonos : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public int IDUsuario { get; set; }
        public int IDEmpleado { get; set; }
        public string ultimaFechaDeCorteDeCaja { get; set; }
        public string todosLosAbonos { get; set; }

        float efectivoI = 0f;
        float tarjetaI = 0f;
        float valesI = 0f;
        float chequeI = 0f;
        float transferenciaI = 0f;

        decimal efectivoAbonos = 0,
                tarjetaAbonos = 0,
                valeAbonos = 0,
                chequeAbono = 0,
                transferenciaAbono = 0;

        decimal Efectivo = 0,
                    Tarjeta = 0,
                    Vales = 0,
                    Cheque = 0,
                    Transferencia = 0,
                    Total = 0,
                    totalAbonoRealizado = 0, 
                    totalAbonoRealizadoDeOtrosUsuarios = 0,
                    totalAbonoRealizadoOtrasVentas = 0;

        public CajaAbonos()
        {
            InitializeComponent();
        }

        private bool IsEmpty(List<int> iDEmpleados)
        {
            var isEmpty = true;

            if (iDEmpleados.Equals(null) || iDEmpleados.Count.Equals(0))
            {
                isEmpty = false;
            }

            return isEmpty;
        }

        private void CajaAbonos_Load(object sender, EventArgs e)
        {
            limpiarVariablesAbonos();

            if (todosLosAbonos.Equals("Si"))
            {
                List<int> IDEmpleados = new List<int>();
                List<string> QuerysDeTodosLosTotalesAbonos = new List<string>();

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

                    using (DataTable dtCargarAbonosTodos = cn.CargarDatos(cs.CargarAbonosTodos(resultadoIDEmpleados)))
                    {
                        if (!dtCargarAbonosTodos.Rows.Count.Equals(0))
                        {
                            var idEmpleado = string.Empty;
                            var fechaUltimaDeCorteConFormatoUSA = string.Empty;

                            foreach (DataRow item in dtCargarAbonosTodos.Rows)
                            {
                                idEmpleado = item["IDEmpleado"].ToString();

                                DateTime fechaUltimaCorte = Convert.ToDateTime(item["Fecha"].ToString());
                                fechaUltimaDeCorteConFormatoUSA = fechaUltimaCorte.ToString("yyyy-MM-dd HH:mm:ss");

                                if (idEmpleado.Equals("0"))
                                {
                                    QuerysDeTodosLosTotalesAbonos.Add($"({cs.CargarAbonosTodosAdministrador(idEmpleado, fechaUltimaDeCorteConFormatoUSA)})");
                                }
                                else if (!idEmpleado.Equals("0"))
                                {
                                    QuerysDeTodosLosTotalesAbonos.Add($"({cs.CargarAbonosTodosEmpleado(idEmpleado, fechaUltimaDeCorteConFormatoUSA)})");
                                }

                                var UnionQuerysTodosLosTotales = string.Join("UNION", QuerysDeTodosLosTotalesAbonos);

                                if (!string.IsNullOrWhiteSpace(UnionQuerysTodosLosTotales))
                                {
                                    using (DataTable dtUnionQuerysTodosLosTotales = cn.CargarDatos(UnionQuerysTodosLosTotales))
                                    {
                                        if (!dtUnionQuerysTodosLosTotales.Rows.Count.Equals(0))
                                        {
                                            limpiarVariablesAbonos();
                                            foreach (DataRow itemUnionQuery in dtUnionQuerysTodosLosTotales.Rows)
                                            {
                                                Efectivo += convertirCantidadHaciaDecimal(itemUnionQuery["Efectivo"].ToString());
                                                Tarjeta += convertirCantidadHaciaDecimal(itemUnionQuery["Tarjeta"].ToString());
                                                Vales += convertirCantidadHaciaDecimal(itemUnionQuery["Vales"].ToString());
                                                Cheque += convertirCantidadHaciaDecimal(itemUnionQuery["Cheque"].ToString());
                                                Transferencia += convertirCantidadHaciaDecimal(itemUnionQuery["Transferencia"].ToString());
                                                Total += convertirCantidadHaciaDecimal(itemUnionQuery["Total"].ToString());
                                            }

                                            lbEfectivoAbonos.Text = Efectivo.ToString("C2");
                                            lbTarjetaAbonos.Text = Tarjeta.ToString("C2");
                                            lbValesAbonos.Text = Vales.ToString("C2");
                                            lbChequeAbonos.Text = Cheque.ToString("C2");
                                            lbTransferenciaAbonos.Text = Transferencia.ToString("C2");
                                            lbTCreditoC.Text = Total.ToString("C2");
                                        }
                                        else
                                        {
                                            limpiarVariablesAbonos();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //using (DataTable dtTodosLosAbonos = cn.CargarDatos(cs.cargarAbonosDesdeUltimoCorteRealizadoTodos(ultimaFechaDeCorteDeCaja)))
                //{
                //    if (!dtTodosLosAbonos.Rows[0][0].Equals(DBNull.Value) && !dtTodosLosAbonos.Rows.Equals(0))
                //    {
                //        foreach (DataRow item in dtTodosLosAbonos.Rows)
                //        {
                //            Efectivo += convertirCantidadHaciaDecimal(item["Efectivo"].ToString());
                //            Tarjeta += convertirCantidadHaciaDecimal(item["Tarjeta"].ToString());
                //            Vales += convertirCantidadHaciaDecimal(item["Vales"].ToString());
                //            Cheque += convertirCantidadHaciaDecimal(item["Cheque"].ToString());
                //            Transferencia += convertirCantidadHaciaDecimal(item["Transferencia"].ToString());
                //            Total += convertirCantidadHaciaDecimal(item["Total"].ToString());

                //            lbEfectivoAbonos.Text = Efectivo.ToString("C2");
                //            lbTarjetaAbonos.Text = Tarjeta.ToString("C2");
                //            lbValesAbonos.Text = Vales.ToString("C2");
                //            lbChequeAbonos.Text = Cheque.ToString("C2");
                //            lbTransferenciaAbonos.Text = Transferencia.ToString("C2");
                //            lbTCreditoC.Text = Total.ToString("C2");
                //        }
                //    }
                //    else
                //    {
                //        limpiarVariablesAbonos();
                //    }
                //}
            }
            else if (todosLosAbonos.Equals("No"))
            {
                if (IDEmpleado > 0)
                {
                    limpiarVariablesAbonos();

                    using (DataTable dtEmpleadoAbonos = cn.CargarDatos(cs.cargarAbonosDesdeUltimoCorteRealizadoEmpleado(IDEmpleado.ToString(), ultimaFechaDeCorteDeCaja)))
                    {
                        if (!dtEmpleadoAbonos.Rows.Equals(0))
                        {
                            foreach (DataRow item in dtEmpleadoAbonos.Rows)
                            {
                                if (!item["IDEmpleado"].ToString().Equals("0"))
                                {
                                    Efectivo += convertirCantidadHaciaDecimal(item["Efectivo"].ToString());
                                    Tarjeta += convertirCantidadHaciaDecimal(item["Tarjeta"].ToString());
                                    Vales += convertirCantidadHaciaDecimal(item["Vales"].ToString());
                                    Cheque += convertirCantidadHaciaDecimal(item["Cheque"].ToString());
                                    Transferencia += convertirCantidadHaciaDecimal(item["Transferencia"].ToString());
                                    totalAbonoRealizado += Convert.ToDecimal(item["Total"].ToString());
                                }
                                Total = totalAbonoRealizado + totalAbonoRealizadoDeOtrosUsuarios;
                            }
                        }
                    }
                    using (DataTable dtAbonosDeOtrosUsuarios = cn.CargarDatos(cs.cargarAbonosDesdeUltimoCorteRealizadoDesdeOtrosUsuarios(IDEmpleado.ToString(), ultimaFechaDeCorteDeCaja)))
                    {
                        if (!dtAbonosDeOtrosUsuarios.Rows.Count.Equals(0))
                        {
                            foreach (DataRow item in dtAbonosDeOtrosUsuarios.Rows)
                            {
                                Efectivo += convertirCantidadHaciaDecimal(item["Efectivo"].ToString());
                                Tarjeta += convertirCantidadHaciaDecimal(item["Tarjeta"].ToString());
                                Vales += convertirCantidadHaciaDecimal(item["Vales"].ToString());
                                Cheque += convertirCantidadHaciaDecimal(item["Cheque"].ToString());
                                Transferencia += convertirCantidadHaciaDecimal(item["Transferencia"].ToString());
                                totalAbonoRealizadoOtrasVentas += Convert.ToDecimal(item["Total"].ToString());
                            }
                            Total = totalAbonoRealizado + totalAbonoRealizadoOtrasVentas;
                        }
                    }

                    lbEfectivoAbonos.Text = Efectivo.ToString("C2");
                    lbTarjetaAbonos.Text = Tarjeta.ToString("C2");
                    lbValesAbonos.Text = Vales.ToString("C2");
                    lbChequeAbonos.Text = Cheque.ToString("C2");
                    lbTransferenciaAbonos.Text = Transferencia.ToString("C2");
                    lbTCreditoC.Text = Total.ToString("C2");
                }
                else
                {
                    limpiarVariablesAbonos();

                    using (DataTable dtAbonos = cn.CargarDatos(cs.cargarAbonosDesdeUltimoCorteRealizadoAdministrador(IDUsuario.ToString(), ultimaFechaDeCorteDeCaja)))
                    {
                        if (!dtAbonos.Rows.Count.Equals(0))
                        {
                            //limpiarVariablesAbonos();
                            foreach (DataRow item in dtAbonos.Rows)
                            {
                                var idEmpleadoRecibioAbonoACredito = item["IDEmpleado"].ToString();
                                var aQuienPertenece = item["Propia"].ToString();

                                if (idEmpleadoRecibioAbonoACredito.Equals("0") && aQuienPertenece.Equals("Propia"))
                                {
                                    Efectivo += Convert.ToDecimal(item["Efectivo"].ToString());
                                    Tarjeta += Convert.ToDecimal(item["Tarjeta"].ToString());
                                    Vales += Convert.ToDecimal(item["Vales"].ToString());
                                    Cheque += Convert.ToDecimal(item["Cheque"].ToString());
                                    Transferencia += Convert.ToDecimal(item["Transferencia"].ToString());
                                    Total += Convert.ToDecimal(item["Total"].ToString());
                                }
                                else
                                {
                                    totalAbonoRealizadoDeOtrosUsuarios += Convert.ToDecimal(item["Total"].ToString());
                                }
                            }
                        }
                    }

                    //using (DataTable dtAbonosRealizadosRecientementeAdministrador = cn.CargarDatos(cs.AbonosRealizadosRecientementeAdministrador(ultimaFechaDeCorteDeCaja)))
                    //{
                    //    if (!dtAbonosRealizadosRecientementeAdministrador.Rows.Count.Equals(0))
                    //    {
                    //        //limpiarVariablesAbonos();
                    //        foreach (DataRow item in dtAbonosRealizadosRecientementeAdministrador.Rows)
                    //        {
                    //            Efectivo += Convert.ToDecimal(item["Efectivo"].ToString());
                    //            Tarjeta += Convert.ToDecimal(item["Tarjeta"].ToString());
                    //            Vales += Convert.ToDecimal(item["Vales"].ToString());
                    //            Cheque += Convert.ToDecimal(item["Cheque"].ToString());
                    //            Transferencia += Convert.ToDecimal(item["Transferencia"].ToString());
                    //            Total += Convert.ToDecimal(item["Total"].ToString());
                    //        }
                    //    }
                    //}

                    lbEfectivoAbonos.Text = Efectivo.ToString("C2");
                    lbTarjetaAbonos.Text = Tarjeta.ToString("C2");
                    lbValesAbonos.Text = Vales.ToString("C2");
                    lbChequeAbonos.Text = Cheque.ToString("C2");
                    lbTransferenciaAbonos.Text = Transferencia.ToString("C2");
                    lbTCreditoC.Text = Total.ToString("C2");
                }
            }

            ////var consultaAnticipoAplicado = ""; //Se agrego esta linea desde esta linea...
            //string ultimoDate = "";
            //var abodoDevolucion = CajaN.abonos_devoluciones;
            //if (abodoDevolucion.Equals("abonos"))
            //{
            //    tituloAbonos.Visible = true;
            //    lbCreditoC.Text = "Total de Abonos";
            //    //Aqui muestra lo de Abonos
            //    try
            //    {
            //        var fechaCorteUltima = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");
            //        if (!fechaCorteUltima.Rows.Count.Equals(0))
            //        {
            //            foreach (DataRow fechaUltimoCorte in fechaCorteUltima.Rows)
            //            {
            //                ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
            //            }
            //            DateTime dateCorteCaja = DateTime.Parse(ultimoDate);

            //            var fechaMovimientos = cn.CargarDatos($"SELECT sum(Efectivo)AS Efectivo, sum(Tarjeta)AS Tarjeta, sum(Vales)AS Vales, sum(Cheque)AS Cheque, sum(Transferencia)AS Transferencia FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{dateCorteCaja.ToString("yyyy-MM-dd HH:mm:ss")}'");
            //            var abonoEfectivo = string.Empty; var abonoTarjeta = string.Empty; var abonoVales = string.Empty; var abonoCheque = string.Empty; var abonoTransferencia = string.Empty;
            //            if (!fechaMovimientos.Rows.Count.Equals(0))
            //            {
            //                foreach (DataRow cantidadAbono in fechaMovimientos.Rows)
            //                {
            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Efectivo"].ToString()))
            //                    {
            //                        abonoEfectivo = "0";
            //                    }
            //                    else
            //                    {
            //                        abonoEfectivo = cantidadAbono["Efectivo"].ToString();
            //                    }

            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Tarjeta"].ToString()))
            //                    {
            //                        abonoTarjeta = "0";
            //                    }
            //                    else
            //                    {
            //                        abonoTarjeta = cantidadAbono["Tarjeta"].ToString();
            //                    }

            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Vales"].ToString()))
            //                    {
            //                        abonoVales = "0";
            //                    }
            //                    else
            //                    {
            //                        abonoVales = cantidadAbono["Vales"].ToString();
            //                    }

            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Cheque"].ToString()))
            //                    {
            //                        abonoCheque = "0";
            //                    }
            //                    else
            //                    {
            //                        abonoCheque = cantidadAbono["Cheque"].ToString();
            //                    }

            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Transferencia"].ToString()))
            //                    {
            //                        abonoTransferencia = "0";
            //                    }
            //                    else
            //                    {
            //                        abonoTransferencia = cantidadAbono["Transferencia"].ToString();
            //                    }

            //                    // = cantidadAbono["sum(Total)"].ToString();
            //                }
            //                efectivoI = float.Parse(abonoEfectivo);
            //                tarjetaI = float.Parse(abonoTarjeta);
            //                valesI = float.Parse(abonoVales);
            //                chequeI = float.Parse(abonoCheque);
            //                transferenciaI = float.Parse(abonoTransferencia);
            //            }
            //            else
            //            {
            //                efectivoI = 0f;
            //                tarjetaI = 0f;
            //                valesI = 0f;
            //                chequeI = 0f;
            //                transferenciaI = 0f;
            //            }
            //        }
            //    }
            //    catch
            //    {

            //    }
            //}
            //else if (abodoDevolucion.Equals("devoluciones"))
            //{
            //    tituloDevoluciones.Visible = true; ;
            //    lbCreditoC.Text = "Total de devoluciones";
            //    //Aqui se muestra lo de Devoluciones
            //    try
            //    {
            //        var fechaCorteUltima = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");
            //        if (!fechaCorteUltima.Rows.Count.Equals(0))
            //        {
            //            foreach (DataRow fechaUltimoCorte in fechaCorteUltima.Rows)
            //            {
            //                ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
            //            }
            //            DateTime dateCorteCaja = DateTime.Parse(ultimoDate);

            //            var fechaMovimientos = cn.CargarDatos($"SELECT sum(Efectivo)AS Efectivo, sum(Tarjeta)AS Tarjeta, sum(Vales)AS Vales, sum(Cheque)AS Cheque, sum(Transferencia)AS Transferencia FROM Devoluciones WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{dateCorteCaja.ToString("yyyy-MM-dd HH:mm:ss")}'");
            //            var devolucionEfectivo = string.Empty; var devolucionTarjeta = string.Empty; var devolucionVales = string.Empty; var devolucionCheque = string.Empty; var devolucionTransferencia = string.Empty;
            //            if (!fechaMovimientos.Rows.Count.Equals(0))
            //            {
            //                foreach (DataRow cantidadAbono in fechaMovimientos.Rows)
            //                {
            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Efectivo"].ToString()))
            //                    {
            //                        devolucionEfectivo = "0";
            //                    }
            //                    else
            //                    {
            //                        devolucionEfectivo = cantidadAbono["Efectivo"].ToString();
            //                    }

            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Tarjeta"].ToString()))
            //                    {
            //                        devolucionTarjeta = "0";
            //                    }
            //                    else
            //                    {
            //                        devolucionTarjeta = cantidadAbono["Tarjeta"].ToString();
            //                    }

            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Vales"].ToString()))
            //                    {
            //                        devolucionVales = "0";
            //                    }
            //                    else
            //                    {
            //                        devolucionVales = cantidadAbono["Vales"].ToString();
            //                    }

            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Cheque"].ToString()))
            //                    {
            //                        devolucionCheque = "0";
            //                    }
            //                    else
            //                    {
            //                        devolucionCheque = cantidadAbono["Cheque"].ToString();
            //                    }

            //                    if (string.IsNullOrWhiteSpace(cantidadAbono["Transferencia"].ToString()))
            //                    {
            //                        devolucionTransferencia = "0";
            //                    }
            //                    else
            //                    {
            //                        devolucionTransferencia = cantidadAbono["Transferencia"].ToString();
            //                    }

            //                    // = cantidadAbono["sum(Total)"].ToString();
            //                }
            //                efectivoI = float.Parse(devolucionEfectivo);
            //                tarjetaI = float.Parse(devolucionTarjeta);
            //                valesI = float.Parse(devolucionVales);
            //                chequeI = float.Parse(devolucionCheque);
            //                transferenciaI = float.Parse(devolucionTransferencia);
            //            }
            //            else
            //            {
            //                efectivoI = 0f;
            //                tarjetaI = 0f;
            //                valesI = 0f;
            //                chequeI = 0f;
            //                transferenciaI = 0f;
            //            }
            //        }
            //    }
            //    catch
            //    {

            //    }
            //}
            //else if (abodoDevolucion.Equals("Saldo Inicial"))
            //{
            //    tituloSaldoInicial.Visible = true; ;
            //    lbCreditoC.Text = "Total Saldo Inicial";

            //    //Aqui se muestra lo de Devoluciones
            //    efectivoI = MetodosBusquedas.efectivoInicial;
            //    tarjetaI = MetodosBusquedas.tarjetaInicial;
            //    valesI = MetodosBusquedas.valesInicial;
            //    chequeI = MetodosBusquedas.chequeInicial;
            //    transferenciaI = MetodosBusquedas.transInicial;    


            //}


            ////Variables de Abonos en Ventas
            //var tipodeMoneda = FormPrincipal.Moneda.Split('-');
            //var moneda = tipodeMoneda[1].ToString().Trim().Replace("(", "").Replace(")", " ");

            //lbEfectivoAbonos.Text = moneda + efectivoI.ToString("0.00");
            //lbTarjetaAbonos.Text = moneda + tarjetaI.ToString("0.00");
            //lbValesAbonos.Text = moneda + valesI.ToString("0.00");
            //lbChequeAbonos.Text = moneda + chequeI.ToString("0.00");
            //lbTransferenciaAbonos.Text = moneda + transferenciaI.ToString("0.00");
            //lbTCreditoC.Text = moneda + /*abonos*/(efectivoI + tarjetaI + valesI + chequeI + transferenciaI).ToString("0.00");
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

        private void limpiarVariablesAbonos()
        {
            efectivoAbonos = tarjetaAbonos = valeAbonos = chequeAbono = transferenciaAbono = 0;
            Efectivo = Tarjeta = Vales = Cheque = Transferencia = Total = 0;
        }

        private void CajaAbonos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }
    }
}
