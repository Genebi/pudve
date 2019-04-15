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
    public partial class AgregarAnticipo : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public AgregarAnticipo()
        {
            InitializeComponent();
        }

        private void AgregarAnticipo_Load(object sender, EventArgs e)
        {
            cbFormaPago.SelectedIndex = 0;
            cbFormaPago.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var concepto = txtConcepto.Text;
            var importe = Convert.ToDouble(txtImporte.Text).ToString("0.00");
            var cliente = txtCliente.Text;
            var formaPago = cbFormaPago.GetItemText(cbFormaPago.SelectedItem);
            var comentario = txtComentarios.Text;
            var status = "1";
            var FechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string[] datos = new string[] { FormPrincipal.userID.ToString(), concepto, importe, cliente, formaPago, comentario, status, FechaOperacion };

            int respuesta = cn.EjecutarConsulta(cs.GuardarAnticipo(datos));

            if (respuesta > 0)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Ocurrió un error al intentar guardar el anticipo.", "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
