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
    public partial class DevolverAnticipo : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        private int idAnticipo = 0;
        private float importe = 0;
        public DevolverAnticipo(int idAnticipo, float importe)
        {
            InitializeComponent();

            this.idAnticipo = idAnticipo;
            this.importe = importe;
        }

        private void DevolverAnticipo_Load(object sender, EventArgs e)
        {
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

            int resultado = cn.EjecutarConsulta(cs.CambiarStatusAnticipo(4, idAnticipo, FormPrincipal.userID));
            
            if (resultado > 0)
            {
                //Se devuelve el dinero del anticipo y se elimina el registro de la tabla Caja para que la cantidad total
                //Que hay en caja sea correcta
                //cn.EjecutarConsulta($"DELETE FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND FechaOperacion = '{fecha}'");
                var cantidad = importe;

                string[] datos = new string[] {
                    "retiro", cantidad.ToString("0.00"), "0", "devolucion anticipo", fechaOperacion, FormPrincipal.userID.ToString(),
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
}
