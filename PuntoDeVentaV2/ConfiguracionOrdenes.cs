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
    public partial class ConfiguracionOrdenes : Form
    {
        Conexion cn = new Conexion();
        public ConfiguracionOrdenes()
        {
            InitializeComponent();
        }

        private void ConfiguracionOrdenes_Load(object sender, EventArgs e)
        {
            using (DataTable configuracion = cn.CargarDatos($"SELECT * FROM ConfiguracionOrdenes WHERE IDUsuario = {FormPrincipal.userID}"))
            {
                if (configuracion.Rows.Count > 0)
                {
                    DataRow config = configuracion.Rows[0];

                    var tiempoEntrega = config["TiempoEntrega"].ToString().Split('|');

                    var dias = tiempoEntrega[0].Substring(0,1).ToString() == "0" ? tiempoEntrega[0].Substring(1,1) : tiempoEntrega[0];
                    var horas = tiempoEntrega[1].Substring(0,1).ToString() == "0" ? tiempoEntrega[1].Substring(1,1) : tiempoEntrega[1];
                    var minutos = tiempoEntrega[2].Substring(0, 1).ToString() == "0" ? tiempoEntrega[2].Substring(1, 1) : tiempoEntrega[2];

                    nudDias.Value = Convert.ToDecimal(dias);
                    nudHoras.Value = Convert.ToDecimal(horas);
                    nudMinutos.Value = Convert.ToDecimal(minutos);
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            var dias = nudDias.Value.ToString();
            var horas = nudHoras.Value.ToString();
            var minutos = nudMinutos.Value.ToString();

            dias = dias.Length == 1 ? $"0{dias}" : dias;
            horas = horas.Length == 1 ? $"0{horas}" : horas;
            minutos = minutos.Length == 1 ? $"0{minutos}" : minutos;

            var tiempoEntrega = $"{dias}|{horas}|{minutos}";
            var fechaOperacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var existe = (bool)cn.EjecutarSelect($"SELECT * FROM ConfiguracionOrdenes WHERE IDUsuario = {FormPrincipal.userID}");

            if ( existe )
            {
                cn.EjecutarConsulta($"UPDATE ConfiguracionOrdenes SET TiempoEntrega = '{tiempoEntrega}', FechaOperacion = '{fechaOperacion}' WHERE IDUsuario = {FormPrincipal.userID}");
            }
            else
            {
                cn.EjecutarConsulta($"INSERT INTO ConfiguracionOrdenes (IDUsuario, TiempoEntrega, FechaOperacion) VALUES ('{FormPrincipal.userID}', '{tiempoEntrega}', '{fechaOperacion}')");
            }

            MessageBox.Show("Configuración guardada con éxito", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();
        }
    }
}
