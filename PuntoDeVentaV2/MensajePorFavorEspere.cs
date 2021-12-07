using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class MensajePorFavorEspere : Form
    {
        public int tiempoDeEspera { get; set; }
        public string propiedadCambiar { get; set; }

        decimal porcentaje;
        string tituloDeLaVentana = string.Empty;

        public MensajePorFavorEspere()
        {
            InitializeComponent();
        }

        private void MensajePorFavorEspere_Load(object sender, EventArgs e)
        {
            tituloDeLaVentana = string.Empty;
            tituloDeLaVentana = definirTituloDeVentana();
            this.Text = $"Asignando {tituloDeLaVentana}";
            lblAsigandoConcepto.Text = $"Asignando {tituloDeLaVentana}";
            progresoDeLaBarra();
        }

        private void progresoDeLaBarra()
        {
            activarProgreso();
        }

        private void activarProgreso()
        {
            timer1.Enabled = true;
        }

        private string definirTituloDeVentana()
        {
            var titulo = string.Empty;

            if (!string.IsNullOrWhiteSpace(propiedadCambiar))
            {
                if (propiedadCambiar.Equals("MensajeVentas"))
                {
                    titulo = "mensaje de venta";
                }
                else if (propiedadCambiar.Equals("MensajeInventario"))
                {
                    titulo = "mensaje para el inventario";
                }
                else if (propiedadCambiar.Equals("Stock"))
                {
                    titulo = "stock";
                }
                else if (propiedadCambiar.Equals("StockMinimo"))
                {
                    titulo = "stock minimo";
                }
                else if (propiedadCambiar.Equals("StockMaximo"))
                {
                    titulo = "stock maximo";
                }
                else if (propiedadCambiar.Equals("Precio"))
                {
                    titulo = "precio";
                }
                else if (propiedadCambiar.Equals("NumeroRevision"))
                {
                    titulo = "número de revisión";
                }
                else if (propiedadCambiar.Equals("TipoIVA"))
                {
                    titulo = "tipo de IVA";
                }
                else if (propiedadCambiar.Equals("ClaveProducto"))
                {
                    titulo = "clave del producto";
                }
                else if (propiedadCambiar.Equals("ClaveUnidad"))
                {
                    titulo = "clave de unidad";
                }
                else if (propiedadCambiar.Equals("Correos"))
                {
                    titulo = "correos";
                }
            }

            return titulo;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pbAvanceAsignacion.Maximum = tiempoDeEspera;
            pbAvanceAsignacion.Minimum = 0;
            pbAvanceAsignacion.Step = 1;
            pbAvanceAsignacion.PerformStep();
            if (pbAvanceAsignacion.Value.Equals(tiempoDeEspera))
            {
                //MessageBox.Show("Asginación completada exitosamente.", "Aviso del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void porcentajeDeProgreso()
        {
            porcentaje = (pbAvanceAsignacion.Value / tiempoDeEspera) * 100;
            //lblProcentaje.Text = $"{porcentaje.ToString()} %";
        }

        private void pbAvanceAsignacion_ParentChanged(object sender, EventArgs e)
        {
            
        }
    }
}
