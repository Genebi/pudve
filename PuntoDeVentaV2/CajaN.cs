using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class CajaN : Form
    {

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public static bool recargarDatos = false;

        // Variables Totales
        public static float totalEfectivo = 0f;
        public static float totalTarjeta = 0f;
        public static float totalVales = 0f;
        public static float totalCheque = 0f;
        public static float totalTransferencia = 0f;
        public static float totalCredito = 0f;

        // Variables Retiro
        public static float retiroEfectivo = 0f;
        public static float retiroTarjeta = 0f;
        public static float retiroVales = 0f;
        public static float retiroCheque = 0f;
        public static float retiroTrans = 0f;

        public static DateTime fechaGeneral;

        public CajaN()
        {
            InitializeComponent();
        }

        private void CajaN_Load(object sender, EventArgs e)
        {
            
        }

        private void btnReporteAgregar_Click(object sender, EventArgs e)
        {
            ReporteDineroAgregado agregado = new ReporteDineroAgregado(fechaGeneral);

            agregado.ShowDialog();
        }

        private void btnReporteRetirar_Click(object sender, EventArgs e)
        {
            ReporteDineroRetirado retirado = new ReporteDineroRetirado(fechaGeneral);

            retirado.ShowDialog();
        }

        private void btnAgregarDinero_Click(object sender, EventArgs e)
        {
            AgregarRetirarDinero agregar = new AgregarRetirarDinero();

            agregar.FormClosed += delegate
            {
                CargarSaldo();
            };

            agregar.ShowDialog();
        }

        private void btnRetirarDinero_Click(object sender, EventArgs e)
        {
            AgregarRetirarDinero retirar = new AgregarRetirarDinero(1);

            retirar.FormClosed += delegate
            {
                CargarSaldo();
            };

            retirar.ShowDialog();
        }

        private void btnCorteCaja_Click(object sender, EventArgs e)
        {
            AgregarRetirarDinero corte = new AgregarRetirarDinero(2);

            corte.FormClosed += delegate
            {
                CargarSaldo();
            };

            corte.ShowDialog();
        }

        #region Metodo para cargar saldos y totales
        private void CargarSaldo()
        {
            SQLiteConnection sql_con;
            SQLiteCommand consultaUno, consultaDos;
            SQLiteDataReader drUno, drDos;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();

            var fechaDefault = Convert.ToDateTime("0001-01-01 00:00:00");

            var consultarFecha = $"SELECT FechaOperacion FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'corte' ORDER BY FeChaOperacion DESC LIMIT 1";
            consultaUno = new SQLiteCommand(consultarFecha, sql_con);
            drUno = consultaUno.ExecuteReader();

            if (drUno.Read())
            {
                var fechaTmp = Convert.ToDateTime(drUno.GetValue(drUno.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                fechaDefault = Convert.ToDateTime(fechaTmp);
            }

            fechaGeneral = fechaDefault;

            var consulta = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID}";
            consultaDos = new SQLiteCommand(consulta, sql_con);
            drDos = consultaDos.ExecuteReader();

            // Variables ventas
            float vEfectivo = 0f;
            float vTarjeta = 0f;
            float vVales = 0f;
            float vCheque = 0f;
            float vTrans = 0f;
            float vCredito = 0f;
            float vAnticipos = 0f;

            // Variables anticipos
            float aEfectivo = 0f;
            float aTarjeta = 0f;
            float aVales = 0f;
            float aCheque = 0f;
            float aTrans = 0f;

            // Variables depositos
            float dEfectivo = 0f;
            float dTarjeta = 0f;
            float dVales = 0f;
            float dCheque = 0f;
            float dTrans = 0f;

            // Variables caja
            float efectivo = 0f;
            float tarjeta = 0f;
            float vales = 0f;
            float cheque = 0f;
            float trans = 0f;
            float credito = 0f;
            float subtotal = 0f;
            float anticipos = 0f;

            // Variable retiro
            float dineroRetirado = 0f;
            retiroEfectivo = 0f;
            retiroTarjeta = 0f;
            retiroVales = 0f;
            retiroCheque = 0f;
            retiroTrans = 0f;

            while (drDos.Read())
            {
                string operacion = drDos.GetValue(drDos.GetOrdinal("Operacion")).ToString();
                var auxiliar = Convert.ToDateTime(drDos.GetValue(drDos.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                var fechaOperacion = Convert.ToDateTime(auxiliar);

                if (operacion == "venta" && fechaOperacion > fechaDefault)
                {
                    vEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    vTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    vVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    vCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    vTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    vCredito += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());
                    vAnticipos += float.Parse(drDos.GetValue(drDos.GetOrdinal("Anticipo")).ToString());
                }

                if (operacion == "anticipo" && fechaOperacion > fechaDefault)
                {
                    aEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    aTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    aVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    aCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    aTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                }

                if (operacion == "deposito" && fechaOperacion > fechaDefault)
                {
                    dEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    dTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    dVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    dCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    dTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                }

                if (operacion == "retiro" && fechaOperacion > fechaDefault)
                {
                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    retiroEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    retiroTarjeta  += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    retiroVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    retiroCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    retiroTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                }
            }

            // Cerramos la conexion y el datareader
            drUno.Close();
            drDos.Close();
            sql_con.Close();


            // Apartado VENTAS
            lbTEfectivo.Text = "$" + vEfectivo.ToString("0.00");
            lbTTarjeta.Text = "$" + vTarjeta.ToString("0.00");
            lbTVales.Text = "$" + vVales.ToString("0.00");
            lbTCheque.Text = "$" + vCheque.ToString("0.00");
            lbTTrans.Text = "$" + vTrans.ToString("0.00");
            lbTCredito.Text = "$" + vCredito.ToString("0.00");
            lbTAnticipos.Text = "$" + vAnticipos.ToString("0.00");
            lbTVentas.Text = "$" + (vEfectivo + vTarjeta + vVales + vCheque + vTrans + vCredito + vAnticipos).ToString("0.00");
            tituloSeccion.Text = "SALDO INICIAL: $" + (vEfectivo + vTarjeta + vVales + vCheque + vTrans + vCredito + vAnticipos).ToString("0.00");

            // Apartado ANTICIPOS RECIBIDOS
            lbTEfectivoA.Text = "$" + aEfectivo.ToString("0.00");
            lbTTarjetaA.Text = "$" + aTarjeta.ToString("0.00");
            lbTValesA.Text = "$" + aVales.ToString("0.00");
            lbTChequeA.Text = "$" + aCheque.ToString("0.00");
            lbTTransA.Text = "$" + aTrans.ToString("0.00");
            lbTAnticiposA.Text = "$" + (aEfectivo + aTarjeta + aVales + aCheque + aTrans).ToString("0.00");

            // Apartado DINERO AGREGADO
            lbTEfectivoD.Text = "$" + dEfectivo.ToString("0.00");
            lbTTarjetaD.Text = "$" + dTarjeta.ToString("0.00");
            lbTValesD.Text = "$" + dVales.ToString("0.00");
            lbTChequeD.Text = "$" + dCheque.ToString("0.00");
            lbTTransD.Text = "$" + dTrans.ToString("0.00");
            lbTAgregado.Text = "$" + (dEfectivo + dTarjeta + dVales + dCheque + dTrans).ToString("0.00");

            // Apartado TOTAL EN CAJA
            efectivo = vEfectivo + aEfectivo + dEfectivo;
            tarjeta = vTarjeta + aTarjeta + dTarjeta;
            vales = vVales + aVales + dVales;
            cheque = vCheque + aCheque + dCheque;
            trans = vTrans + aTrans + dTrans;
            credito = vCredito;
            anticipos = vAnticipos;
            subtotal = efectivo + tarjeta + vales + cheque + trans + credito;

            lbTEfectivoC.Text = "$" + efectivo.ToString("0.00");
            lbTTarjetaC.Text = "$" + tarjeta.ToString("0.00");
            lbTValesC.Text = "$" + vales.ToString("0.00");
            lbTChequeC.Text = "$" + cheque.ToString("0.00");
            lbTTransC.Text = "$" + trans.ToString("0.00");
            lbTCreditoC.Text = "$" + credito.ToString("0.00");
            lbTAnticiposC.Text = "$" + anticipos.ToString("0.00");
            lbTSubtotal.Text = "$" + subtotal.ToString("0.00");
            lbTDineroRetirado.Text = "$" + dineroRetirado.ToString("0.00");
            lbTTotalCaja.Text = "$" + (subtotal - dineroRetirado).ToString("0.00");

            // Variables de clase
            totalEfectivo = efectivo;
            totalTarjeta = tarjeta;
            totalVales = vales;
            totalCheque = cheque;
            totalTransferencia = trans;
            totalCredito = credito;
        }
        #endregion

        private void CajaN_Paint(object sender, PaintEventArgs e)
        {
            if (recargarDatos)
            {
                CargarSaldo();
                recargarDatos = false;
            }
        }

        private void CajaN_Resize(object sender, EventArgs e)
        {
            recargarDatos = false;
        }
    }
}
