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
    public partial class AgregarDetalleGeneral : Form
    {
        Conexion cn = new Conexion();

        public string getIdUsr { get; set; }
        public string getChkName { get; set; }
        public string getRealChkName { get; set; }

        string IdUsr = string.Empty, ChkName = string.Empty;

        public AgregarDetalleGeneral()
        {
            InitializeComponent();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            
        }

        private void AgregarDetalleGeneral_Load(object sender, EventArgs e)
        {
            cargarDatos();
        }

        private void txtNombre_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void btnAgregarDetalle_Click(object sender, EventArgs e)
        {
            AgregarEspecificacionDeConceptoDinamico registrarEspcificacion = new AgregarEspecificacionDeConceptoDinamico();

            registrarEspcificacion.FormClosed += delegate
            {
                //this.Close();
            };

            registrarEspcificacion.getChkName = getRealChkName;
            registrarEspcificacion.ShowDialog();
        }

        private void btnQuitarDetalle_Click(object sender, EventArgs e)
        {
            QuitarEspecificacionDeConceptoDinamico quitarEspecificacion = new QuitarEspecificacionDeConceptoDinamico();

            quitarEspecificacion.FormClosed += delegate
            {
                //this.Close();
            };

            quitarEspecificacion.getChkName = getRealChkName;
            quitarEspecificacion.ShowDialog();
        }

        private void AgregarDetalleGeneral_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }

        private void cargarDatos()
        {
            IdUsr = getIdUsr;
            ChkName = getChkName;
            //label1.Text = "Concepto de: " + ChkName;
        }
    }
}
