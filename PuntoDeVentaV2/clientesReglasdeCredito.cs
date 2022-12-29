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
    public partial class clientesReglasdeCredito : Form
    {
        Conexion cn = new Conexion();
        string IDCliente;
        public clientesReglasdeCredito(string id_cliente)
        {
            InitializeComponent();
            IDCliente = id_cliente;
        }

        private void clientesReglasdeCredito_Load(object sender, EventArgs e)
        {
            using (DataTable dtReglasCreditoVenta = cn.CargarDatos($"SELECT * FROM configuracion WHERE IDUsuario = {FormPrincipal.userID}"))
            {
                using (DataTable dtReglas = cn.CargarDatos($"SELECT * FROM clienteReglasCredito WHERE IDCliente = {IDCliente} AND IDUsuario = {FormPrincipal.userID}"))
                {
                    if (dtReglasCreditoVenta.Rows[0]["creditomodolimiteventas"].ToString().Equals("Por Cliente"))
                    {
                        numVentasAbiertas.Enabled = true;
                    }
                    if (dtReglasCreditoVenta.Rows[0]["creditoAplicarpordefecto"].ToString().Equals("0"))
                    {
                        numInteresDefecto.Enabled = true;
                    }
                    if (dtReglasCreditoVenta.Rows[0]["creditomodototalcredito"].ToString().Equals("Por Cliente"))
                    {
                        numTotaldecredito.Enabled = true;
                    }
                }
            }
        }
    }
}
