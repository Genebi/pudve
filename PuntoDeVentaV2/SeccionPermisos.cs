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
    public partial class SeccionPermisos : Form
    {
        private string seccion = string.Empty;

        public SeccionPermisos(string seccion)
        {
            InitializeComponent();

            this.seccion = seccion;
        }

        private void SeccionPermisos_Load(object sender, EventArgs e)
        {
            if (seccion == "Caja")
                GenerarCaja();
        }

        private void GenerarCaja()
        {
            this.Text = "PUDVE - Permisos Caja";

            GenerarCheckbox(40, 20, 150, "Botón Agregar Dinero");
            GenerarCheckbox(40, 180, 200, "Botón Historial Dinero Agregado");
            GenerarCheckbox(80, 20, 150, "Botón Retirar Dinero");
            GenerarCheckbox(80, 180, 200, "Botón Historial Dinero Retirado");
            GenerarCheckbox(120, 20, 150, "Botón Abrir Caja");
            GenerarCheckbox(120, 180, 200, "Botón Corte Caja");
        }

        private void GenerarCheckbox(int top, int left, int ancho, string texto)
        {
            var checkbox = new CheckBox();
            checkbox.Text = texto;
            checkbox.Top = top;
            checkbox.Left = left;
            checkbox.Width = ancho;
            checkbox.Name = "testx";

            panelContenedor.Controls.Add(checkbox);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            foreach (Control item in panelContenedor.Controls)
            {
                if (item is CheckBox)
                {
                    MessageBox.Show(item.Name);
                }
            }
        }
    }
}
