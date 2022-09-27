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
    public partial class SeleccionaOpcion : Form
    {
        string text1, text2, Pregunta;

        private void btnOpcion1_Click(object sender, EventArgs e)
        {
            OpcionesReporteProducto.opcionPregunta = "opcion1";
            this.Close();
        }

        private void btnOpcion2_Click(object sender, EventArgs e)
        {
            OpcionesReporteProducto.opcionPregunta = "opcion2";
            this.Close();
        }

        public SeleccionaOpcion(string boton1,string boton2,string pregunta)
        {
            InitializeComponent();
            this.text1 = boton1;
            this.text2 = boton2;
            this.Pregunta = pregunta;
        }
        private void SeleccionaOpcion_Load(object sender, EventArgs e)
        {
            lblTexto.Text = Pregunta;
            btnOpcion1.Text = text1;
            btnOpcion2.Text = text2;
        }
    }
}
