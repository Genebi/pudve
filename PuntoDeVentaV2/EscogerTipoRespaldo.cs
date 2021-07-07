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
    public partial class EscogerTipoRespaldo : Form
    {

        public static int typeBackUp { get; set; }

        public EscogerTipoRespaldo()
        {
            InitializeComponent();
            this.Text = "Escoger tipo de respaldo";
        }

        private void EscogerTipoRespaldo_Load(object sender, EventArgs e)
        {

        }

        private void btnGuadar_Click(object sender, EventArgs e)
        {
            var tipoRespaldo = 0;

            if (rbRespaldoEquipo.Checked) //Solo se hace respaldo en el equipo
            {
                tipoRespaldo = 1;
            }
            else if (rbRespaldoCorreo.Checked)//El respaldo se guarda en una carpeta de archivos pudve  
            {                                 // y luego se manda por correo
                tipoRespaldo = 2;
            }
            else if (rbAmbos.Checked)//Se respalda en el equipo y se envia por correo
            {
                tipoRespaldo = 3;
            }

            typeBackUp = tipoRespaldo;

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            typeBackUp = 0;

            this.Close();
        }

        private void rbRespaldoEquipo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGuadar.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnCancelar.PerformClick();
            }
        }

        private void rbRespaldoCorreo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGuadar.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnCancelar.PerformClick();
            }
        }

        private void rbAmbos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGuadar.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnCancelar.PerformClick();
            }
        }
    }
}
