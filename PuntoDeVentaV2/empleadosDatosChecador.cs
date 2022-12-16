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

    public partial class empleadosDatosChecador : Form
    {
        private int id_empleado = 0;


        public empleadosDatosChecador(int id_emp)
        {
            InitializeComponent();
            id_empleado = id_emp;
        }

        private void btnHuella_Click(object sender, EventArgs e)
        {
            Conexion cn = new Conexion();
            Consultas cs = new Consultas();
            if (!cn.CargarDatos(cs.buscarExistenciaHuellas(id_empleado.ToString())).Rows.Count.Equals(0))
            {   //Update

                cn.EjecutarConsulta(cs.borrarHuella(id_empleado.ToString()));
            }
            empleadosAltaHuella capturar = new empleadosAltaHuella(id_empleado);
            capturar.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            empleadoVerificarHuella comparar = new empleadoVerificarHuella();
            comparar.ShowDialog();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
