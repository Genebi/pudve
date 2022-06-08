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
    public partial class InformacionVenta : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        private int idVenta = 0;
        private int idCliente = 0;

        private string[] pagosConCuenta = new string[] { "02", "03", "04", "05", "06", "28", "29", "99"};

        public InformacionVenta( long idVenta = 0)
        {
            InitializeComponent();

                this.idVenta =(int)Convert.ToDouble(idVenta);
        }

        private void InformacionVenta_Load(object sender, EventArgs e)
        {
            //ComboBox Formas de pago
            Dictionary<string, string> pagos = new Dictionary<string, string>();
            pagos.Add("01", "01 - Efectivo");
            pagos.Add("02", "02 - Cheque nominativo");
            pagos.Add("03", "03 - Transferencia electrónica de fondos");
            pagos.Add("04", "04 - Tarjeta de crédito");
            pagos.Add("05", "05 - Monedero electrónico");
            pagos.Add("06", "06 - Dinero electrónico");
            pagos.Add("08", "08 - Vales de despensa");
            pagos.Add("12", "12 - Dación en pago");
            pagos.Add("13", "13 - Pago por subrogación");
            pagos.Add("14", "14 - Pago por consignación");
            pagos.Add("15", "15 - Condonación");
            pagos.Add("17", "17 - Compensación");
            pagos.Add("23", "23 - Novación");
            pagos.Add("24", "24 - Confusión");
            pagos.Add("25", "25 - Remisión de deuda");
            pagos.Add("26", "26 - Prescripción o caducidad");
            pagos.Add("27", "27 - A satisfacción del acreedor");
            pagos.Add("28", "28 - Tarjeta de débito");
            pagos.Add("29", "29 - Tarjeta de servicios");
            pagos.Add("30", "30 - Aplicación de anticipos");
            pagos.Add("99", "99 - Por definir");

            cbFormaPago.DataSource = pagos.ToArray();
            cbFormaPago.DisplayMember = "Value";
            cbFormaPago.ValueMember = "Key";

            //ComboBox Método de pago
            Dictionary<string, string> metodos = new Dictionary<string, string>();
            metodos.Add("PUE", "Pago en una sola exhibición");
            metodos.Add("PPD", "Pago en parcialidades o diferido");

            cbMetodoPago.DataSource = metodos.ToArray();
            cbMetodoPago.DisplayMember = "Value";
            cbMetodoPago.ValueMember = "Key";

            //Obtener los totales de las formas de pago de la venta
            var tmp = mb.ObtenerFormasPagoVenta(idVenta, FormPrincipal.userID);
            var valorMaximo = tmp.Max();
            var valorIndice = Array.IndexOf(tmp, valorMaximo);
            var formaPago = FormaPagoDefault(valorIndice);
            cbFormaPago.SelectedValue = formaPago;
            ValidarFormasPago(formaPago);

            //Obtener detalles de venta y cliente
            var detalles = mb.ObtenerDetallesVenta(idVenta, FormPrincipal.userID);
            idCliente = Convert.ToInt32(detalles[0]);
        }

        private string FormaPagoDefault(int indice)
        {
            string pago = string.Empty;

            if (indice == 0) { pago = "01"; } //Efectivo
            if (indice == 1) { pago = "01"; } //Tarjeta: Cuando es tarjeta ponemos efectivo por defecto
            if (indice == 2) { pago = "08"; } //Vales
            if (indice == 3) { pago = "02"; } //Cheque
            if (indice == 4) { pago = "03"; } //Transferencia
            if (indice == 5) { pago = "99"; } //Credito

            return pago;
        }

        private void btnSiguiente1_Click(object sender, EventArgs e)
        {
            //Actualizamos la forma de pago, metodo de pago y cuenta en caso de que exista
            var pago = cbFormaPago.SelectedValue.ToString();
            var metodo = cbMetodoPago.SelectedValue.ToString();

            if (txtCuenta.Visible && pago != "99")
            {
                if (string.IsNullOrWhiteSpace(txtCuenta.Text))
                {
                    MessageBox.Show("Ingrese el número de cuenta", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    txtCuenta.Text = string.Empty;
                    txtCuenta.Focus();

                    return;
                }
            }

            this.Hide();

            var cuenta = txtCuenta.Text;

            //Actualizamos
            int respuestaUno = cn.EjecutarConsulta($"UPDATE Ventas SET MetodoPago = '{metodo}', FormaPago = '{pago}' WHERE ID = {idVenta} AND IDUsuario = {FormPrincipal.userID}");
            int respuestaDos = cn.EjecutarConsulta($"UPDATE DetallesVenta SET Cuenta = '{cuenta}' WHERE IDVenta = {idVenta} AND IDUsuario = {FormPrincipal.userID}");

            if (respuestaUno > 0 && respuestaDos > 0)
            {
                if (idCliente > 0)
                {
                    AgregarCliente cliente = new AgregarCliente(3, idCliente);

                    cliente.ShowDialog();

                    this.Close();
                }
                else
                {
                    //Hacer algo en caso de que no tenga clienta (publico general)
                }
            }
        }

        private void ValidarFormasPago(string pago)
        {
            if (Array.IndexOf(pagosConCuenta, pago) > -1)
            {
                lbCuenta.Visible = true;
                lbCuenta.Text = "Cuenta";
                txtCuenta.Visible = true;
                txtCuenta.Focus();

                if (pago == "99") { lbCuenta.Text = "Cuenta (opcional)"; }
                if (pago == "05") { lbCuenta.Text = "Cuenta (alfanumérica)"; }
            }
            else
            {
                lbCuenta.Visible = false;
                lbCuenta.Text = "Cuenta";
                txtCuenta.Visible = false;
            }
        }

        private void cbFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarFormasPago(cbFormaPago.SelectedValue.ToString());
        }
    }
}
