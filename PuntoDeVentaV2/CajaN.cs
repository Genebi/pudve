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

        private float totalEfectivo = 0f;
        private float totalTarjeta = 0f;
        private float totalVales = 0f;
        private float totalCheque = 0f;
        private float totalTransferencia = 0f;
        private float totalCredito = 0f;

        public CajaN()
        {
            InitializeComponent();
        }

        private void CajaN_Load(object sender, EventArgs e)
        {
            CargarSaldo();
        }

        private void btnReporteAgregar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reporte agregar");
        }

        private void btnReporteRetirar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Reporte retirar");
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
            MessageBox.Show("Corte caja");
        }

        private void CargarSaldo()
        {
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();

            var consulta = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID}";

            sql_cmd = new SQLiteCommand(consulta, sql_con);

            dr = sql_cmd.ExecuteReader();

            float saldo = 0f;
            float efectivo = 0f;
            float tarjeta = 0f;
            float vales = 0f;
            float cheque = 0f;
            float trans = 0f;
            float credito = 0f;
            float anticipos = 0f;

            while (dr.Read())
            {
                string operacion = dr.GetValue(dr.GetOrdinal("Operacion")).ToString();

                if (operacion == "deposito")
                {
                    saldo += float.Parse(dr.GetValue(dr.GetOrdinal("Cantidad")).ToString());

                    efectivo += float.Parse(dr.GetValue(dr.GetOrdinal("Efectivo")).ToString());
                    tarjeta += float.Parse(dr.GetValue(dr.GetOrdinal("Tarjeta")).ToString());
                    vales += float.Parse(dr.GetValue(dr.GetOrdinal("Vales")).ToString());
                    cheque += float.Parse(dr.GetValue(dr.GetOrdinal("Cheque")).ToString());
                    trans += float.Parse(dr.GetValue(dr.GetOrdinal("Transferencia")).ToString());
                    credito += float.Parse(dr.GetValue(dr.GetOrdinal("Credito")).ToString());
                    anticipos += float.Parse(dr.GetValue(dr.GetOrdinal("Anticipo")).ToString());
                }

                if (operacion == "retiro")
                {
                    saldo -= float.Parse(dr.GetValue(dr.GetOrdinal("Cantidad")).ToString());

                    efectivo -= float.Parse(dr.GetValue(dr.GetOrdinal("Efectivo")).ToString());
                    tarjeta -= float.Parse(dr.GetValue(dr.GetOrdinal("Tarjeta")).ToString());
                    vales -= float.Parse(dr.GetValue(dr.GetOrdinal("Vales")).ToString());
                    cheque -= float.Parse(dr.GetValue(dr.GetOrdinal("Cheque")).ToString());
                    trans -= float.Parse(dr.GetValue(dr.GetOrdinal("Transferencia")).ToString());
                    credito -= float.Parse(dr.GetValue(dr.GetOrdinal("Credito")).ToString());
                }
            }

            // Cerramos la conexion y el datareader
            dr.Close();
            sql_con.Close();

            //tituloSeccion.Text = "CAJA: $" + saldo.ToString("0.00");

            // Apartado VENTAS

            // Apartado ANTICIPOS RECIBIDOS

            // Apartado DINERO AGREGADO
            lbTEfectivoD.Text = "$" + efectivo.ToString("0.00");
            lbTTarjetaD.Text = "$" + tarjeta.ToString("0.00");
            lbTValesD.Text = "$" + vales.ToString("0.00");
            lbTChequeD.Text = "$" + cheque.ToString("0.00");
            lbTTransD.Text = "$" + trans.ToString("0.00");

            // Apartado TOTAL EN CAJA

            // Variables de clase
            /*totalEfectivo = efectivo;
            totalTarjeta = tarjeta;
            totalVales = vales;
            totalCheque = cheque;
            totalTransferencia = trans;
            totalCredito = credito;*/
        }

        private void CajaN_Paint(object sender, PaintEventArgs e)
        {
            CargarSaldo();
        }
    }
}
