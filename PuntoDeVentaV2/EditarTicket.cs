using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class EditarTicket : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        OpenFileDialog f;
        int valor;

        public EditarTicket()
        {
            InitializeComponent();
        }

        private void EditarTicket_Load(object sender, EventArgs e)
        {
            var datos = FormPrincipal.datosUsuario;
            lblNombreUs.Text = (datos[0].ToString());
            lblDireccionUs.Text = ("Direccion: " + datos[1] + ", " + datos[2] + ", " + datos[4] + ", " + datos[5].ToString());
            lblColyCPUs.Text = ("Colonia: " + datos[6] + ", " + "C.P.: " + datos[7].ToString());
            lblRFCUs.Text = ("RFC: " + datos[8].ToString());
            lblCorreoUs.Text = ("Correo: " + datos[9].ToString());
            lblTelefonoUs.Text = ("Telefono: " + datos[10].ToString());
            var nomComercial = cn.CargarDatos($"SELECT  nombre_comercial FROM usuarios WHERE ID = '{FormPrincipal.userID}' ");
            string nombreComercial = nomComercial.Rows[0]["nombre_comercial"].ToString();
            lblNombreComercial.Text = nombreComercial;

            var checkboxTicket = cn.CargarDatos($"SELECT * FROM `editarticket` WHERE IDUsuario = '{FormPrincipal.userID}'");

            foreach (DataRow item in checkboxTicket.Rows)
            {
                var datos2 = item;
                int nombre = Convert.ToInt32(datos2[3]);
                int direccion = Convert.ToInt32(datos2[4]);
                int colycp = Convert.ToInt32(datos2[5]);
                int rfc = Convert.ToInt32(datos2[6]);
                int correo = Convert.ToInt32(datos2[7]);
                int telefono = Convert.ToInt32(datos2[8]);
                 
                int nombrec = Convert.ToInt32(datos2[9]);
                int domicilioc = Convert.ToInt32(datos2[10]);
                int rfcc = Convert.ToInt32(datos2[11]);
                int correoc = Convert.ToInt32(datos2[12]);
                int telefonoc = Convert.ToInt32(datos2[13]);
                int colycpc = Convert.ToInt32(datos2[14]);
                int formapagoc = Convert.ToInt32(datos2[15]);
                int logo = Convert.ToInt32(datos2[16]);
                int nComercial = Convert.ToInt32(datos2[17]);
                int ticket58mm = Convert.ToInt32(datos2[18]);
                int ticket80mm = Convert.ToInt32(datos2[19]);

                if (nComercial == 1)//////Logo  
                {
                    chkLogoTicket.Checked = true;
                }
                else
                {
                    chkLogoTicket.Checked = false;
                }
                if (nComercial == 1)//////Nombre Comercial 
                {
                    chkNombreComercial.Checked = true;
                }
                else
                {
                    chkNombreComercial.Checked = false;
                }
                if (nombre == 1)//////Nombre Usuario
                {
                    chkNombreUs.Checked = true;
                }
                else
                {
                    chkNombreUs.Checked = false;
                }
                if (direccion == 1)//////Direccion Usuario
                {
                    chkDireccionUs.Checked = true;
                }
                else
                {
                    chkDireccionUs.Checked = false;
                }
                if (colycp == 1)/////Coloni y CP Usuario
                {
                    chkColUs.Checked = true;
                }
                else
                {
                    chkColUs.Checked = false;
                }
                if (rfc == 1)//////RFC Usuario
                {
                    chkRfcUs.Checked = true;
                }
                else
                {
                    chkRfcUs.Checked = false;
                }
                if (correo == 1)//////Correo Usuario
                {
                    chkCorreoUs.Checked = true;
                }
                else
                {
                    chkCorreoUs.Checked = false;
                }
                if (telefono == 1)/////Telefono Usuario
                {
                    chkTelefonoUs.Checked = true;
                }
                else
                {
                    chkTelefonoUs.Checked = false;
                }
                if (nombrec == 1)/////Nombre Cliente
                {
                    chkNombreCl.Checked = true;
                }
                else
                {
                    chkNombreCl.Checked = false;
                }
                if (domicilioc == 1)/////Domicilio Cliente
                {
                    chkDomicilioCl.Checked = true;
                }
                else
                {
                    chkDomicilioCl.Checked = false;
                }
                if (rfcc == 1)/////RFC Cliente
                {
                    chkRfcCl.Checked = true;
                }
                else
                {
                    chkRfcCl.Checked = false;
                }
                if (correoc == 1)/////Correo Cliente
                {
                    chkCorreoCl.Checked = true;
                }
                else
                {
                    chkCorreoCl.Checked = false;
                }
                if (telefonoc == 1)/////Telefono Cliente
                {
                    chkTelefonoCl.Checked = true;
                }
                else
                {
                    chkTelefonoCl.Checked = false;
                }
                if (colycpc == 1)/////Colonia y CP Cliente
                {
                    chkColoniaCl.Checked = true;
                }
                else
                {
                    chkColoniaCl.Checked = false;
                }
                if (formapagoc == 1)/////Forma de Pago Cliente
                {
                    chkFormaPagoCl.Checked = true;
                }
                else
                {
                    chkFormaPagoCl.Checked = false;
                }
                if (ticket58mm.Equals(1))
                {
                    rbTicket6cm.Checked = true;
                }
                else
                {
                    rbTicket6cm.Checked = false;
                }
                if (ticket80mm.Equals(1))
                {
                    rbTicket8cm.Checked = true;
                }
                else
                {
                    rbTicket8cm.Checked = false;
                }
            }
            if (File.Exists($@"C:\Archivos PUDVE\MisDatos\Usuarios\{FormPrincipal.datosUsuario[11].ToString()}"))/////Imagen
            {
                pictureBox1.Image = Image.FromFile($@"C:\Archivos PUDVE\MisDatos\Usuarios\{FormPrincipal.datosUsuario[11].ToString()}");
            }
            else
            {
            }
            //if (FormPrincipal.datosUsuario[11].ToString() != "")
            //{
            //    pictureBox1.Image = Image.FromFile($@"C:\Archivos PUDVE\MisDatos\Usuarios\{FormPrincipal.datosUsuario[11].ToString()}");
            //}
            mensajeTicket();
        }
        private void mensajeTicket()
        {
            var mensaje = cn.CargarDatos($"SELECT MensajeTicket FROM `editarticket` WHERE IDUsuario = '{FormPrincipal.userID}'");
            foreach (DataRow item in mensaje.Rows)
            {
                var mensaje2 = item;
                lblMensajeTicket.Text = mensaje2[0].ToString();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            editarMensajeTicket mensaje = new editarMensajeTicket();
            mensaje.FormClosed += delegate
            {
                mensajeTicket();
            };
            mensaje.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dato = cn.CargarDatos(cs.consultarMensajeTicket(FormPrincipal.userID));

            foreach (DataRow item in dato.Rows)
            {
                valor = Convert.ToInt32(item[0].ToString());
            }



            if (valor == 0)
            {
                cn.EjecutarConsulta(cs.insertarMensajeDeTicket(FormPrincipal.userID, "Gracias por su compra!!"));
            }

            //////Logotipo
            if (chkLogoTicket.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.lotipoTicket(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.lotipoTicket(status));
            }

            //////Nombre Usuario
            if (chkNombreUs.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.nombreusTicket(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.nombreusTicket(status));
            }
            //////Direccion de Usuario
            if (chkDireccionUs.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.direccionTicket(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.direccionTicket(status));
            }
            //////RFC Usuario
            if (chkRfcUs.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.rfcTicket(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.rfcTicket(status));
            }
            //////Colonia y CP Usuario
            if (chkColUs.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.colycpTicket(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.colycpTicket(status));
            }
            //////Correo Usuario
            if (chkCorreoUs.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.correoTicket(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.correoTicket(status));
            }
            //////Telefono Usuario
            if (chkTelefonoUs.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.telTicket(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.telTicket(status));
            }
            //////Forma Pago Cliente
            if (chkFormaPagoCl.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.formapagoCTicket(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.formapagoCTicket(status));
            }
            //////Referencia de venta 
            if (chkReferenciaVenta.Checked == true)
            {

            }
            else
            {

            }
            //////RFC Cliente
            if (chkRfcCl.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.rfcCTicket(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.rfcCTicket(status));
            }
            //////Domicilio Cliente
            if (chkDomicilioCl.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.domicilioCTicket(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.domicilioCTicket(status));
            }
            //////Correo Cliente
            if (chkCorreoCl.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.correoCTicket(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.correoCTicket(status));
            }
            //////Colonia y CP Cliente
            if (chkColoniaCl.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.colycpCTicket(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.colycpCTicket(status));
            }
            //////Nombre Cliente
            if (chkNombreCl.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.nombreCTicket(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.nombreCTicket(status));
            }
            //////Telefono Cliente
            if (chkTelefonoCl.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.telefonoCTicket(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.telefonoCTicket(status));
            }
            /////Nombre Cliente
            if (chkNombreComercial.Checked == true)
            {
                var status = 1;
                cn.EjecutarConsulta(cs.nombreComercial(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.nombreComercial(status));
            }
            /////
            if (rbTicket6cm.Checked.Equals(true))
            {
                var status = 1;
                cn.EjecutarConsulta(cs.ticket58mm(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.ticket58mm(status));
            }
            /////
            if (rbTicket8cm.Checked.Equals(true))
            {
                var status = 1;
                cn.EjecutarConsulta(cs.ticket80mm(status));
            }
            else
            {
                var status = 0;
                cn.EjecutarConsulta(cs.ticket80mm(status));
            }
            MessageBox.Show("Guardado Correctamente","Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void chkColoniaCl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkColoniaCl.Checked == false)
            {
                lblColCl.Visible = false;
                lblColCl.Height = 0;
            }
            else if (chkColoniaCl.Checked == true)
            {
                lblColCl.Visible = true;
                lblColCl.Height = 20;
            }
        }

        private void chkNombreCl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNombreCl.Checked == false)
            {
                lblNombreCl.Visible = false;
                lblNombreCl.Height = 0;
            }
            else if (chkNombreCl.Checked == true)
            {
                lblNombreCl.Visible = true;
                lblNombreCl.Height = 20;
            }
        }

        private void chkRfcCl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRfcCl.Checked == false)
            {
                lblRFCCl.Visible = false;
                lblRFCCl.Height = 0;
            }
            else if (chkRfcCl.Checked == true)
            {
                lblRFCCl.Visible = true;
                lblRFCCl.Height = 20;
            }
        }

        private void chkDomicilioCl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDomicilioCl.Checked == false)
            {
                lblDomicilioCl.Visible = false;
                lblDomicilioCl.Height = 0;
            }
            else if (chkDomicilioCl.Checked == true)
            {
                lblDomicilioCl.Visible = true;
                lblDomicilioCl.Height = 20;
            }
        }

        private void chkCorreoCl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCorreoCl.Checked == false)
            {
                lblCorreoCl.Visible = false;
                lblCorreoCl.Height = 0;
            }
            else if (chkDomicilioCl.Checked == true)
            {
                lblCorreoCl.Visible = true;
                lblCorreoCl.Height = 20;
            }
        }

        private void chkTelefonoCl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTelefonoCl.Checked == false)
            {
                lblTelefonoCl.Visible = false;
                lblTelefonoCl.Height = 0;
            }
            else if (chkTelefonoCl.Checked == true)
            {
                lblTelefonoCl.Visible = true;
                lblTelefonoCl.Height = 20;
            }
        }

        private void chkFormaPagoCl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFormaPagoCl.Checked == false)
            {
                lblFormaPagoCl.Visible = false;
                lblFormaPagoCl.Height = 0;
            }
            else if (chkFormaPagoCl.Checked == true)
            {
                lblFormaPagoCl.Visible = true;
                lblFormaPagoCl.Height = 20;
            }
        }

        private void chkNombreUs_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNombreUs.Checked == false)
            { 
                lblNombreUs.Visible = false;
                lblNombreUs.Height = 0;
            }
            else if (chkNombreUs.Checked == true)
            {
                lblNombreUs.Visible = true;
                lblNombreUs.Height = 20;
            }
        }

        private void chkDireccionUs_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDireccionUs.Checked == false)
            {
                lblDireccionUs.Visible = false;
                lblDireccionUs.Height = 0;
            }
            else if (chkDireccionUs.Checked == true)
            {
                lblDireccionUs.Visible = true;
                lblDireccionUs.Height = 20;
            }
        }

        private void chkColUs_CheckedChanged(object sender, EventArgs e)
        {
            if (chkColUs.Checked == false)
            {
                lblColyCPUs.Visible = false;
                lblColyCPUs.Height = 0;
            }
            else if (chkColUs.Checked == true)
            {
                lblColyCPUs.Visible = true;
                lblColyCPUs.Height = 20;
            }
        }

        private void chkRfcUs_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRfcUs.Checked == false)
            {
                lblRFCUs.Visible = false;
                lblRFCUs.Height = 0;
            }
            else if (chkRfcUs.Checked == true)
            {
                lblRFCUs.Visible = true;
                lblRFCUs.Height = 20;
            }
        }

        private void chkCorreoUs_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCorreoUs.Checked == false)
            {
                lblCorreoUs.Visible = false;
                lblCorreoUs.Height = 0;
            }
            else if (chkCorreoUs.Checked == true)
            {
                lblCorreoUs.Visible = true;
                lblCorreoUs.Height = 20;
            }
        }

        private void chkTelefonoUs_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTelefonoUs.Checked == false)
            {
                lblTelefonoUs.Visible = false;
                lblTelefonoUs.Height = 0;
            }
            else if (chkTelefonoUs.Checked == true)
            {
                lblTelefonoUs.Visible = true;
                lblTelefonoUs.Height = 20;
            }
        }

        private void chkLogoTicket_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLogoTicket.Checked == false)
            {
                pnlLogo.Visible = false;
                pnlLogo.Height = 0;
            }
            else if (chkLogoTicket.Checked == true)
            {
                pnlLogo.Visible = true;
                pnlLogo.Height = 61;
            }
        }

        private void chkMarcarTodosCl_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMarcarTodosCl.Checked == true)
            {
                chkNombreCl.Checked = true;
                chkDomicilioCl.Checked = true;
                chkRfcCl.Checked = true;
                chkCorreoCl.Checked = true;
                chkTelefonoCl.Checked = true;
                chkColoniaCl.Checked = true;
                chkFormaPagoCl.Checked = true;
                chkMostrarMensaje.Checked = true;
                chkReferenciaVenta.Checked = true;
            }
            else
            {
                chkNombreCl.Checked = false;
                chkDomicilioCl.Checked = false;
                chkRfcCl.Checked = false;
                chkCorreoCl.Checked = false;
                chkTelefonoCl.Checked = false;
                chkColoniaCl.Checked = false;
                chkFormaPagoCl.Checked = false;
                chkMostrarMensaje.Checked = false;
                chkReferenciaVenta.Checked = false;
            }
        }

        private void chkMarcarTodosUs_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMarcarTodosUs.Checked == true)
            {
                chkNombreUs.Checked = true;
                chkDireccionUs.Checked = true;
                chkRfcUs.Checked = true;
                chkCorreoUs.Checked = true;
                chkTelefonoUs.Checked = true;
                chkColUs.Checked = true;
                chkLogoTicket.Checked = true;
                chkNombreComercial.Checked = true;
            }
            else
            {
                chkNombreUs.Checked = false;
                chkDireccionUs.Checked = false;
                chkRfcUs.Checked = false;
                chkCorreoUs.Checked = false;
                chkTelefonoUs.Checked = false;
                chkColUs.Checked = false;
                chkLogoTicket.Checked = false;
                chkNombreComercial.Checked = false;
        }

    }

        private void chkMostrarMensaje_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMostrarMensaje.Checked == false)
            {
                lblMensajeTicket.Visible = false;
                lblMensajeTicket.Height = 0;
            }
            else if (chkMostrarMensaje.Checked == true)
            {
                lblMensajeTicket.Visible = true;
                lblMensajeTicket.Height = 33;
            }
        }

        private void EditarTicket_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNombreComercial.Checked == false)
            {
                lblNombreComercial.Visible = false;
                lblNombreComercial.Height = 0;
            }
            else if (chkNombreComercial.Checked == true)
            {
                lblNombreComercial.Visible = true;
                lblNombreComercial.Height = 25;
            }
        }

        private void chkReferenciaVenta_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReferenciaVenta.Checked == false)
            {
                lblReferenciaVenta.Visible = false;
                lblReferenciaVenta.Height = 0;
            }
            else if (chkReferenciaVenta.Checked == true)
            {
                lblReferenciaVenta.Visible = true;
                lblReferenciaVenta.Height = 20;  
            }
        }
    }
}
