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
    public partial class canditadPVF : Form
    {
        string promptMsg = string.Empty, 
                titleWindow = string.Empty,
                strDefaultResponse = string.Empty;
        string validGuion = string.Empty;
        string validPunto = string.Empty;
        int conteoPunto, conteoGuion;
        int calcu = 0;
        public string cantidad = string.Empty;
        public static string cantidadMayoraStock = string.Empty;

        public canditadPVF(string _Prompt, string _Title, string _DefaultResponse)
        {
            InitializeComponent();
            this.promptMsg = _Prompt;
            this.titleWindow = _Title;
            this.strDefaultResponse = _DefaultResponse;
        } 

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Ventas.sonido = false;
            cantidad = "Cancelar";
            ConsultarProductoVentas.AcepOCanc = false;
            this.Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
             Ventas.sonido = true;
             cantidad = num1.Value.ToString();
             this.Close();
        }

        private void txtCantidad_KeyDown(object sender, KeyEventArgs e)
        {
                if (e.KeyCode == Keys.Enter)
            {
                
                btnAceptar.PerformClick();
            }

        }

        

        private void inputMessageBoxVentas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                btnCancelar.PerformClick();
            }
        }

        private void num1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                btnAceptar.PerformClick();
            }
        }


        private void inputMessageBoxVentas_Load(object sender, EventArgs e)
        {
            cargarValores();
        }

        private void cargarValores()
        {
            lblPrompt.Text = promptMsg;
            this.Text = titleWindow;
            num1.Focus();
            num1.Select(0, 100);
        }
    }
}
