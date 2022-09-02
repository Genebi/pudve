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
    public partial class Autorizacion : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        int intetnos = 0;
        public Autorizacion()
        {
            InitializeComponent();
        }

        private void Autorizacion_Load(object sender, EventArgs e)
        {

        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string Contraseña = string.Empty;
            using (DataTable DTContraseña = cn.CargarDatos(cs.ContreseñaDeUsuarioPorID(FormPrincipal.userID)))
            {
                Contraseña = DTContraseña.Rows[0]["Password"].ToString();
            }
            if (txtContraseña.Text.Equals(Contraseña))
            {
                Ventas.AutorizacionConfirmada = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Contraseña incorrecta","Aviso del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtContraseña.Clear();
                txtContraseña.Focus();
                intetnos++;
            }
            if (intetnos.Equals(3))
            {
                MessageBox.Show("Límite de intentos excedidos \n La venta se cerrara","Aviso del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                Ventas.AutorizacionConfirmada = false;
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Ventas.AutorizacionConfirmada = false;
            this.Close();
        }
    }
}
