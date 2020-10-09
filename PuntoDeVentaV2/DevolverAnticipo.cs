using MySql.Data.MySqlClient;
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
    public partial class DevolverAnticipo : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        // Variables ventas
        float vEfectivo = 0f;
        float vTarjeta = 0f;
        float vVales = 0f;
        float vCheque = 0f;
        float vTrans = 0f;
        float vCredito = 0f;
        float vAnticipos = 0f;
        float totalVentas = 0f;

        // Variables anticipos
        float aEfectivo = 0f;
        float aTarjeta = 0f;
        float aVales = 0f;
        float aCheque = 0f;
        float aTrans = 0f;
        float totalAnticipos = 0f;

        // Variables depositos-Dinero Agregado
        float dEfectivo = 0f;
        float dTarjeta = 0f;
        float dVales = 0f;
        float dCheque = 0f;
        float dTrans = 0f;
        float totalDineroAgregado = 0f;

        // Variables caja
        float efectivo1 = 0f;
        float tarjeta1 = 0f;
        float vales1 = 0f;
        float cheque1 = 0f;
        float trans1 = 0f;
        float credito = 0f;
        float anticipos1 = 0f;
        float saldoInicial = 0f;
        float subtotal = 0f;
        float dineroRetirado = 0f;
        float totalCaja = 0f;

        // Variable retiro
        float retiroEfectivo = 0f;
        float retiroTarjeta = 0f;
        float retiroVales = 0f;
        float retiroCheque = 0f;
        float retiroTrans = 0f;
        float retiroCredito = 0f;

        // Variables de la seccionProductos
        string nombreP = "";
        string nombreAlterno1P = "";
        string nombreAlterno2P = "";
        float stockP = 0f;
        float precioP = 0f;
        float revisionP = 0f;
        string claveP = "";
        string codigoP = "";
        string historialP = "";
        string tipoP = "";

        string userNickName = "";
        //string nickUsuarioP = "";
        //string nickUsuarioC = "";

        public static DateTime fechaGeneral;

        private int idAnticipo = 0;
        private float importe = 0;
        private int tipo = 0;

        public DevolverAnticipo(int idAnticipo, float importe, int tipo = 1)
        {
            InitializeComponent();

            this.idAnticipo = idAnticipo;
            this.importe = importe;
            this.tipo = tipo;
        }

        private void DevolverAnticipo_Load(object sender, EventArgs e)
        {
            if (tipo == 1)
            {
                this.Text = "PUDVE - Devolver Anticipo";
                lbTitulo.Text = "Forma de pago para devolución";
            }

            if (tipo == 2)
            {
                this.Text = "PUDVE - Habilitar Anticipo";
                lbTitulo.Text = "Forma de pago para anticipo";
            }

            //ComboBox Formas de pago
            Dictionary<string, string> pagos = new Dictionary<string, string>();
            pagos.Add("01", "01 - Efectivo");
            pagos.Add("02", "02 - Cheque nominativo");
            pagos.Add("03", "03 - Transferencia electrónica de fondos");
            pagos.Add("04", "04 - Tarjeta de crédito");
            pagos.Add("08", "08 - Vales de despensa");

            cbFormaPago.DataSource = pagos.ToArray();
            cbFormaPago.DisplayMember = "Value";
            cbFormaPago.ValueMember = "Key";
        }

        private void CargarSaldo()
        {
            MySqlConnection sql_con; 
            MySqlCommand consultaUno, consultaDos;
            MySqlDataReader drUno, drDos;

            var servidor = Properties.Settings.Default.Hosting;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                sql_con = new MySqlConnection($"datasource={servidor};port=6666;username=root;password=;database=pudve;");
            }
            else
            {
                sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }

            sql_con.Open();

            var fechaDefault = Convert.ToDateTime("0001-01-01 00:00:00");

            var consultarFecha = $"SELECT FechaOperacion FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'corte' ORDER BY FeChaOperacion DESC LIMIT 1";
            consultaUno = new MySqlCommand(consultarFecha, sql_con);
            drUno = consultaUno.ExecuteReader();

            if (drUno.Read())
            {
                var fechaTmp = Convert.ToDateTime(drUno.GetValue(drUno.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                fechaDefault = Convert.ToDateTime(fechaTmp);
            }

            fechaGeneral = fechaDefault;

            var consulta = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID}";
            consultaDos = new MySqlCommand(consulta, sql_con);
            drDos = consultaDos.ExecuteReader();

            int saltar = 0;

            while (drDos.Read())
            {
                string operacion = drDos.GetValue(drDos.GetOrdinal("Operacion")).ToString();
                var auxiliar = Convert.ToDateTime(drDos.GetValue(drDos.GetOrdinal("FechaOperacion"))).ToString("yyyy-MM-dd HH:mm:ss");
                var fechaOperacion = Convert.ToDateTime(auxiliar);

                if (operacion == "venta" && fechaOperacion > fechaDefault)
                {
                    if (saltar == 0)
                    {
                        saltar++;
                        continue;
                    }

                    vEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    vTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    vVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    vCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    vTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    vCredito += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());
                    vAnticipos += float.Parse(drDos.GetValue(drDos.GetOrdinal("Anticipo")).ToString());
                    totalVentas = (vEfectivo + vTarjeta + vVales + vCheque + vTrans + vCredito + vAnticipos);
                }

                if (operacion == "anticipo" && fechaOperacion > fechaDefault)
                {
                    aEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    aTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    aVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    aCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    aTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    totalAnticipos = (aEfectivo + aTarjeta + aVales + aCheque + aTrans);

                }

                if (operacion == "deposito" && fechaOperacion > fechaDefault)
                {
                    dEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    dTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    dVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    dCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    dTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    totalDineroAgregado = (dEfectivo + dTarjeta + dVales + dCheque + dTrans);
                }

                if (operacion == "retiro" && fechaOperacion > fechaDefault)
                {
                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());
                    retiroEfectivo += float.Parse(drDos.GetValue(drDos.GetOrdinal("Efectivo")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());
                    retiroTarjeta += float.Parse(drDos.GetValue(drDos.GetOrdinal("Tarjeta")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());
                    retiroVales += float.Parse(drDos.GetValue(drDos.GetOrdinal("Vales")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());
                    retiroCheque += float.Parse(drDos.GetValue(drDos.GetOrdinal("Cheque")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());
                    retiroTrans += float.Parse(drDos.GetValue(drDos.GetOrdinal("Transferencia")).ToString());

                    dineroRetirado += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());
                    retiroCredito += float.Parse(drDos.GetValue(drDos.GetOrdinal("Credito")).ToString());


                }
            }

            // Apartado TOTAL EN CAJA
            efectivo1 = (vEfectivo + aEfectivo + dEfectivo) - retiroEfectivo;
            tarjeta1 = (vTarjeta + aTarjeta + dTarjeta) - retiroTarjeta;
            vales1 = (vVales + aVales + dVales) - retiroVales;
            cheque1 = (vCheque + aCheque + dCheque) - retiroCheque;
            trans1 = (vTrans + aTrans + dTrans) - retiroTrans;
            credito = vCredito;
            anticipos1 = vAnticipos;
            subtotal = efectivo1 + tarjeta1 + vales1 + cheque1 + trans1 + credito + saldoInicial;
            totalCaja = (subtotal - dineroRetirado);

            // Cerramos la conexion y el datareader
            drUno.Close();
            drDos.Close();
            sql_con.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var formaPago = cbFormaPago.SelectedValue.ToString();

            var efectivo = "0";
            var cheque = "0";
            var transferencia = "0";
            var tarjeta = "0";
            var vales = "0";
            var credito = "0";

            //Operacion para afectar la Caja
            if (formaPago == "01") { efectivo = importe.ToString(); }
            if (formaPago == "02") { cheque = importe.ToString(); }
            if (formaPago == "03") { transferencia = importe.ToString(); }
            if (formaPago == "04") { tarjeta = importe.ToString(); }
            if (formaPago == "08") { vales = importe.ToString(); }

            var status = 0;
            var operacion = string.Empty;
            var comentario = string.Empty;

            var efe = float.Parse(efectivo);
            var che = float.Parse(cheque);
            var tra = float.Parse(transferencia);
            var tar = float.Parse(tarjeta);
            var val = float.Parse(vales);

            // Devolver anticipo
            if (tipo == 1)
            {
                status = 4;
                operacion = "retiro";
                comentario = "devolucion anticipo";
                CargarSaldo();
            }

            // Habilitar anticipo
            if (tipo == 2)
            {
                status = 1;
                operacion = "deposito";
                comentario = "anticipo habilitado";
            }
            if (tipo == 1 && cbFormaPago.SelectedIndex == 0 && efe > efectivo1 || cbFormaPago.SelectedIndex == 1 && che > cheque1 ||
                cbFormaPago.SelectedIndex == 2 && tra > trans1 || cbFormaPago.SelectedIndex == 3 && tar > tarjeta1 ||
                cbFormaPago.SelectedIndex == 4 && val > vales1)
            {
                MessageBox.Show("Dinero Insuficuente", "Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {

            
            int resultado = cn.EjecutarConsulta(cs.CambiarStatusAnticipo(status, idAnticipo, FormPrincipal.userID));

                if (resultado > 0)
                {
                    
                    //if ()
                    //{

                    //}
                    //var datoObtenido = "";
                    //var consultarVacio = cn.CargarDatos($"SELECT Efectivo, Tarjeta, Vales, Cheque, Transferencia, Credito, FechaOperacion FROM Caja WHERE IDUsuario = {FormPrincipal.userID} ORDER BY FechaOperacion DESC LIMIT 1");
                    //for (int i=0; i<consultarVacio.Rows.Count; i++)
                    //{
                    //    //datoObtenido = consultarVacio.Rows[i][0,1].ToString();
                    //}
                    cn.EjecutarConsulta($"UPDATE Anticipos SET FormaPago = '{formaPago}' WHERE ID = {idAnticipo} AND IDUsuario = {FormPrincipal.userID}");

                    //Se devuelve el dinero del anticipo y se elimina el registro de la tabla Caja para que la cantidad total
                    //Que hay en caja sea correcta
                    //cn.EjecutarConsulta($"DELETE FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND FechaOperacion = '{fecha}'");
                    var cantidad = importe;

                    string[] datos = new string[] {
                    operacion, cantidad.ToString("0.00"), "0", comentario, fechaOperacion, FormPrincipal.userID.ToString(),
                    efectivo, tarjeta, vales, cheque, transferencia, credito, "0"
                };

                    resultado = cn.EjecutarConsulta(cs.OperacionCaja(datos));
                    if (resultado > 0)
                    {
                        this.Dispose();
                    }
                }
            }
        }

        private void iniciarVariablesWebService()
        {
            //  Apartado de Caja  //
            vEfectivo = 0f;
            vTarjeta = 0f;
            vVales = 0f;
            vCheque = 0f;
            vTrans = 0f;
            vCredito = 0f;
            vAnticipos = 0f;
            totalVentas = 0f;

            aEfectivo = 0f;
            aTarjeta = 0f;
            aVales = 0f;
            aCheque = 0f;
            aTrans = 0f;
            totalAnticipos = 0f;

            dEfectivo = 0f;
            dTarjeta = 0f;
            dVales = 0f;
            dCheque = 0f;
            dTrans = 0f;
            totalDineroAgregado = 0f;

            efectivo1 = 0f;
            tarjeta1 = 0f;
            vales1 = 0f;
            cheque1 = 0f;
            trans1 = 0f;
            credito = 0f;
            anticipos1 = 0f;
            saldoInicial = 0f;
            subtotal = 0f;
            dineroRetirado = 0f;
            totalCaja = 0f;

            //  Apartado de Productos  //
            nombreP = "";
            nombreAlterno1P = "";
            nombreAlterno2P = "";
            stockP = 0f;
            precioP = 0f;
            revisionP = 0f;
            claveP = "";
            codigoP = "";
            historialP = "";
            tipoP = "";

            userNickName = FormPrincipal.userNickName;

        }

        private void cbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
