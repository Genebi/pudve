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
        public string consulta;
        int tipo;
        bool instkill = false;
        public clientesReglasdeCredito(int operacion, string id_cliente = "", bool matar =false)
        {
            IDCliente = id_cliente;
            tipo = operacion;
            instkill = matar;
            InitializeComponent();
        }

        private void clientesReglasdeCredito_Load(object sender, EventArgs e)
        {
            using (DataTable dtReglasCreditoVenta = cn.CargarDatos($"SELECT * FROM configuracion WHERE IDUsuario = {FormPrincipal.userID}"))
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

                if (tipo == 2)
                {
                    using (DataTable dtReglas = cn.CargarDatos($"SELECT * FROM clienteReglasCredito WHERE IDCliente = {IDCliente} AND IDUsuario = {FormPrincipal.userID}"))
                    {
                    numInteresDefecto.Value = Decimal.Parse(dtReglas.Rows[0]["interes"].ToString());
                    numVentasAbiertas.Value = Decimal.Parse(dtReglas.Rows[0]["VentasAbiertas"].ToString());
                    numTotaldecredito.Value = Decimal.Parse(dtReglas.Rows[0]["Credito"].ToString());
                } }
                    else
                    {
                        numInteresDefecto.Value = Decimal.Parse(dtReglasCreditoVenta.Rows[0]["creditoPorcentajeinteres"].ToString());
                        numVentasAbiertas.Value = Decimal.Parse(dtReglasCreditoVenta.Rows[0]["creditolimiteventas"].ToString());
                        numTotaldecredito.Value = Decimal.Parse(dtReglasCreditoVenta.Rows[0]["creditototalcredito"].ToString());
                    }

                if (instkill)
                {
                    btnAceptar.PerformClick();
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            

            if (tipo==2)
            {
                consulta = "UPDATE clienteReglasCredito SET ";
                consulta = consulta += $"Interes = {numInteresDefecto.Value.ToString()}, ";
                consulta = consulta += $"VentasAbiertas = {numVentasAbiertas.Value.ToString()}, ";
                consulta = consulta += $"Credito = {numTotaldecredito.Value.ToString()}";
                consulta = consulta += "WHERE IDCliente = ";
                consulta = consulta += $"{IDCliente}";
                cn.EjecutarConsulta(consulta);
            }
            else
            {
                //consulta = "INSERT INTO clienteReglasCredito ";
                //consulta = consulta += $"Interes = {numInteresDefecto.Value.ToString()}, ";
                //consulta = consulta += $"VentasAbiertas = {numVentasAbiertas.Value.ToString()}, ";
                //consulta = consulta += $"Credito = {numTotaldecredito.Value.ToString()}, ";
                //consulta = consulta += "IDCliente = ";

                consulta = "INSERT INTO clienteReglasCredito(IDUSuario, Interes, VentasAbiertas, Credito, IDCliente)";
                consulta += $"VALUES('{FormPrincipal.userID}','{numInteresDefecto.Value.ToString()}', '{numVentasAbiertas.Value.ToString()}', '{numTotaldecredito.Value.ToString()}', ";

            }


            if (!instkill)
            {
                 MessageBox.Show("Configuracion Guardada con exito", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }
    }
}
