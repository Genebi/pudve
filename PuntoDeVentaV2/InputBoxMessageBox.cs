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
    public partial class InputBoxMessageBox : Form
    {
        string  promptMsg = string.Empty, 
                titleWindow = string.Empty, 
                strDefaultResponse = string.Empty;
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        public string retornoNombreConcepto = string.Empty;

        private void cargarValores()
        {
            lblPrompt.Text = promptMsg;
            this.Text = titleWindow;
            txtDefaultResponse.Text = strDefaultResponse;
            txtDefaultResponse.Focus();
            txtDefaultResponse.SelectAll();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string  textoDefaultResponse = string.Empty, 
                    auxComparacionAgregar = string.Empty;

            textoDefaultResponse = txtDefaultResponse.Text;
            auxComparacionAgregar = strDefaultResponse;

            if (txtDefaultResponse.Text.Equals(string.Empty))
            {
                retornoNombreConcepto = string.Empty;
            }
            else if(!textoDefaultResponse.Contains(auxComparacionAgregar))
            {
                retornoNombreConcepto = txtDefaultResponse.Text;
                retornoNombreConcepto.Trim();
                retornoNombreConcepto = retornoNombreConcepto.Replace("\r\n", " ");
            }

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDefaultResponse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAceptar.PerformClick();
            }
        }

        private void InputBoxMessageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }

        private void InputBoxMessageBox_Load(object sender, EventArgs e)
        {
            cargarValores();
        }

        public InputBoxMessageBox(string _Prompt, string _Title, string _DefaultResponse)
        {
            InitializeComponent();
            this.promptMsg = _Prompt;
            this.titleWindow = _Title;
            this.strDefaultResponse = _DefaultResponse;
        }
    }
} 
