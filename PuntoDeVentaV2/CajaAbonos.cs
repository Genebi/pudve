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
        public CajaAbonos()
        {
            InitializeComponent();
        }

        private void CajaAbonos_Load(object sender, EventArgs e)
        {
            float abonoEfectivoI = 0f;
            float abonoTarjetaI = 0f;
            float abonoValesI = 0f;
            float abonoChequeI = 0f;
            float abonoTransferenciaI = 0f;

            //var consultaAnticipoAplicado = ""; //Se agrego esta linea desde esta linea...
            string ultimoDate = "";
            try
            {
                //var segundaConsulta = cn.CargarDatos($"SELECT sum(AnticipoAplicado) FROM Anticipos  WHERE IDUsuario = '{FormPrincipal.userID}'");
                //if (segundaConsulta.Rows.Count > 0 && string.IsNullOrWhiteSpace(segundaConsulta.ToString()))
                //{
                //    foreach (DataRow obtenerAnticipoAplicado in segundaConsulta.Rows)
                //    {
                //        consultaAnticipoAplicado = obtenerAnticipoAplicado["sum(AnticipoAplicado)"].ToString();
                //    }
                //    anticiposAplicados = float.Parse(consultaAnticipoAplicado); //Hasta esta linea.
                //}

                var fechaCorteUltima = cn.CargarDatos($"SELECT FechaOperacion FROM Caja WHERE IDUsuario = '{FormPrincipal.userID}' AND Operacion = 'corte' ORDER BY FechaOperacion DESC LIMIT 1");
                if (fechaCorteUltima.Rows.Count > 0 && string.IsNullOrWhiteSpace(fechaCorteUltima.ToString()))
                {
                    foreach (DataRow fechaUltimoCorte in fechaCorteUltima.Rows)
                    {
                        ultimoDate = fechaUltimoCorte["FechaOperacion"].ToString();
                    }
                    DateTime fechaFinAbonos = DateTime.Parse(ultimoDate);

                    //var fechaMovimientos = cn.CargarDatos($"SELECT sum(Total) FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{fechaFinAbonos.ToString("yyyy-MM-dd HH:mm:ss")}'");
                    //var abono = "";
                    //foreach (DataRow cantidadAbono in fechaMovimientos.Rows)
                    //{
                    //    abono = cantidadAbono["sum(Total)"].ToString();
                    //}
                    //abonos = float.Parse(abono);
                    var fechaMovimientos = cn.CargarDatos($"SELECT sum(Efectivo), sum(Tarjeta), sum(Vales), sum(Cheque), sum(Transferencia) FROM Abonos WHERE IDUsuario = '{FormPrincipal.userID}' AND FechaOperacion > '{fechaFinAbonos.ToString("yyyy-MM-dd HH:mm:ss")}'");
                    var abonoEfectivo = ""; var abonoTarjeta = ""; var abonoVales = ""; var abonoCheque = ""; var abonoTransferencia = "";
                    if (fechaMovimientos.Rows.Count > 0 && string.IsNullOrWhiteSpace(fechaMovimientos.ToString()))
                    {
                        foreach (DataRow cantidadAbono in fechaMovimientos.Rows)
                        {
                            // = cantidadAbono["sum(Total)"].ToString();
                            abonoEfectivo = cantidadAbono["sum(Efectivo)"].ToString();
                            abonoTarjeta = cantidadAbono["sum(Tarjeta)"].ToString();
                            abonoVales = cantidadAbono["sum(Vales)"].ToString();
                            abonoCheque = cantidadAbono["sum(Cheque)"].ToString();
                            abonoTransferencia = cantidadAbono["sum(Transferencia)"].ToString();
                        }
                        abonoEfectivoI = float.Parse(abonoEfectivo);
                        abonoTarjetaI = float.Parse(abonoTarjeta);
                        abonoValesI = float.Parse(abonoVales);
                        abonoChequeI = float.Parse(abonoCheque);
                        abonoTransferenciaI = float.Parse(abonoTransferencia);
                    }
                    else
                    {
                        abonoEfectivoI = 0f;
                        abonoTarjetaI = 0f;
                        abonoValesI = 0f;
                        abonoChequeI = 0f;
                        abonoTransferenciaI = 0f;
                    }
                }
            }
            catch
            {

            }

            //Variables de Abonos en Ventas
            lbEfectivoAbonos.Text = "$" + abonoEfectivoI.ToString("0.00");
            lbTarjetaAbonos.Text = "$" + abonoTarjetaI.ToString("0.00");
            lbValesAbonos.Text = "$" + abonoValesI.ToString("0.00");
            lbChequeAbonos.Text = "$" + abonoChequeI.ToString("0.00");
            lbTransferenciaAbonos.Text = "$" + abonoTransferenciaI.ToString("0.00");
            lbTCreditoC.Text = "$" + /*abonos*/(abonoEfectivoI + abonoTarjetaI + abonoValesI + abonoChequeI + abonoTransferenciaI).ToString("0.00");
        }
    }
}
