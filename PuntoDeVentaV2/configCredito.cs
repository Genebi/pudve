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
    public partial class configCredito : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public configCredito()
        {
            InitializeComponent();

        }

        private void configCredito_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            numTotaldecredito.Controls[0].Visible = false;
            dt = cn.CargarDatos($"SELECT creditoHuella, creditoMoratorio, creditoAplicarpordefecto, creditoPorcentajeinteres, creditoAplicarpagoinicial, creditoPagoinicial, creditomodolimiteventas, creditolimiteventas, creditomodototalcredito, creditototalcredito, creditoperiodocobro, creditomodocobro, creditodiassincobro FROM configuracion WHERE IDUsuario = {FormPrincipal.userID}");
            //A checar datos
            if (!dt.Rows[0]["creditoHuella"].ToString().Equals("0"))
            {
                cbHuella.Checked = true;
            }
            if (!dt.Rows[0]["creditoMoratorio"].ToString().Equals("0"))
            {
                cbMoratorio.Checked = true;
            }
            if (!dt.Rows[0]["creditoAplicarpordefecto"].ToString().Equals("0"))
            {
                cbAplicarPorcentajePorDefecto.Checked = true;
            }

            numInteresDefecto.Value = Decimal.Parse(dt.Rows[0]["creditoPorcentajeinteres"].ToString());

            if (!dt.Rows[0]["creditoAplicarpagoinicial"].ToString().Equals("0"))
            {
                cbPagoInicial.Checked = true;
                numPagoInicial.Enabled = true;
            }

            combVentasAbiertas.Text = dt.Rows[0]["creditomodolimiteventas"].ToString();
            combTotalCredito.Text = dt.Rows[0]["creditomodototalcredito"].ToString();
            combPeriododecobro.Text = dt.Rows[0]["creditoperiodocobro"].ToString();
            combMododecobro.Text= dt.Rows[0]["creditomodocobro"].ToString();
            numPagoInicial.Value = Decimal.Parse(dt.Rows[0]["creditoPagoinicial"].ToString());
            numDiasdecobrosininteres.Value = Int32.Parse(dt.Rows[0]["creditodiassincobro"].ToString());

            numVentasAbiertas.Value = Decimal.Parse(dt.Rows[0]["creditolimiteventas"].ToString());
            numTotaldecredito.Value = Decimal.Parse(dt.Rows[0]["creditototalcredito"].ToString());

        }

        private void combVentasAbiertas_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void combTotalCredito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combTotalCredito.SelectedIndex == 1)
            {
                numTotaldecredito.Enabled = true;
            }
            else
            {
                numTotaldecredito.Enabled = false;
            }
        }

        private void cbPagoInicial_CheckedChanged(object sender, EventArgs e)
        {
            if (cbPagoInicial.Checked)
            {
                numPagoInicial.Enabled = true;
            }
            else
            {
                numPagoInicial.Enabled = false;
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //Es el momento de actualizar los datos
            //, , , , , , , , , , ,   IDUsuario
            string consulta = "UPDATE configuracion SET ";

            if (cbHuella.Checked)
            {
                consulta = consulta += $"creditoHuella = 1, ";
            }
            else
            {
                consulta = consulta += $"creditoHuella = 0, ";
            }

            if (cbMoratorio.Checked)
            {
                consulta = consulta += $"creditoMoratorio = 1, ";
            }
            else
            {
                consulta = consulta += $"creditoMoratorio = 0, ";
            }

            if (cbAplicarPorcentajePorDefecto.Checked)
            {
                consulta = consulta += $"creditoAplicarpordefecto = 1, ";
            }
            else
            {
                consulta = consulta += $"creditoAplicarpordefecto = 0, ";
            }

            consulta = consulta += $"creditoPorcentajeinteres = {numInteresDefecto.Value.ToString()}, ";

            if (cbPagoInicial.Checked)
            {
                consulta = consulta += $"creditoAplicarpagoinicial = 1, ";
            }
            else
            {
                consulta = consulta += $"creditoAplicarpagoinicial = 0, ";
            }

            consulta = consulta += $"creditomodolimiteventas = '{combVentasAbiertas.Text}', ";

            consulta = consulta += $"creditolimiteventas = {numVentasAbiertas.Value.ToString()}, ";

            consulta = consulta += $"creditomodototalcredito = '{combTotalCredito.Text}', ";

            consulta = consulta += $"creditototalcredito = {numTotaldecredito.Value.ToString()}, ";

            consulta = consulta += $"creditoperiodocobro = '{combPeriododecobro.Text}', ";

            consulta = consulta += $"creditomodocobro = '{combMododecobro.Text}', ";

            consulta = consulta += $"creditodiassincobro = {numDiasdecobrosininteres.Value.ToString()} ";

            consulta = consulta += $"WHERE IDUsuario = {FormPrincipal.userID}";
            cn.EjecutarConsulta(consulta);

            MessageBox.Show("Configuracion Guardada con exito", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void combVentasAbiertas_SelectedValueChanged(object sender, EventArgs e)
        {
            if (combVentasAbiertas.SelectedIndex == 0)
            {
                numVentasAbiertas.Enabled = true;
            }
            else
            {
                numVentasAbiertas.Enabled = false;
            }
            if (combVentasAbiertas.SelectedIndex == 2)
            {
                lblDef1.Visible = true;
            }
            else
            {
                lblDef1.Visible = true;
            }
        }

        private void combTotalCredito_SelectedValueChanged(object sender, EventArgs e)
        {
            if (combTotalCredito.SelectedIndex == 0)
            {
                numTotaldecredito.Enabled = true;
            }
            else
            {
                numTotaldecredito.Enabled = false;
            }
            if (combTotalCredito.SelectedIndex == 2)
            {
                lbdef2.Visible = true;
            }
            else
            {
                lbdef2.Visible = false;
            }
        }
    }
}
