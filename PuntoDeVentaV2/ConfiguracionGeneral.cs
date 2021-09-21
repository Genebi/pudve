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
    public partial class ConfiguracionGeneral : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        // Permiso de botones
        int opcion1 = 1;    // Servidor
        int opcion2 = 1;    // Numero revision
        int opcion3 = 1;    // Porcentaje ganancia
        int opcion4 = 1;    // Respaldar informacion
        int opcion5 = 1;    // Correo modificar precio
        int opcion6 = 1;    // Correo modificar stock
        int opcion7 = 1;    // Correo stock minimo
        int opcion8 = 1;    // Correo vender producto
        int opcion9 = 1;    // Permitir stock negativo
        int opcion10 = 1;   // Codigo barra ticket
        int opcion11 = 1;   // Informacion pagina web
        int opcion12 = 1;   // Mostrar precio de producto
        int opcion13 = 1;   // Mostrar codigo de producto
        int opcion14 = 1;   // Activar precio mayoreo
        int opcion15 = 1;   // Avisar productos no vendidos
        int opcion16 = 1;   // Correo Agregar Dinero Caja
        int opcion17 = 1;   // Correo Retirar Dinero Caja
        int opcion18 = 1;   // Correo Cerrar Ventana Ventas
        int opcion19 = 1;   // Correo Restar Productos Ventas
        int opcion20 = 1;   // Correo Eliminar Producto Lista Ventas
        int opcion21 = 1;   // Correo Eliminar Ultimo Producto agregado listado Ventas
        int opcion22 = 1;   // Correo Eliminar Lista Producto de Ventas
        int opcion23 = 1;   // Correo al hacer Corte de Caja
        int opcion26 = 1;

        bool check5 = false;
        bool check6 = false;
        bool check7 = false;
        bool check8 = false;
        bool check9 = false;
        bool check10 = false;
        bool check11 = false;
        bool check12 = false;
        bool check13 = false;
        bool check14 = false;
        bool check15 = false;
        bool check16 = false;
        bool check17 = false;
        bool check18 = false;
        bool check19 = false;
        bool check20 = false;
        bool check21 = false;
        bool check22 = false;
        bool check23 = false;
        bool check26 = false;
        public ConfiguracionGeneral() 
        {
            InitializeComponent();
        }

        private void VerificarConfiguracion()
        {
            var existe = (bool)cn.EjecutarSelect($"SELECT * FROM Configuracion WHERE IDUsuario = {FormPrincipal.userID}");

            if (existe)
            {
                var datosConfig = mb.ComprobarConfiguracion();

                checkCBVenta.Checked = Convert.ToBoolean(datosConfig[4]);
                check10 = checkCBVenta.Checked;


                pagWeb.Checked = Convert.ToBoolean(datosConfig[5]);
                check11 = pagWeb.Checked;

                cbMostrarPrecio.Checked = Convert.ToBoolean(datosConfig[6]);
                check12 = cbMostrarPrecio.Checked;

                cbMostrarCB.Checked = Convert.ToBoolean(datosConfig[7]);
                check13 = cbMostrarCB.Checked;

                checkMayoreo.Checked = Convert.ToBoolean(datosConfig[9]);
                check14 = checkMayoreo.Checked;

                txtMinimoMayoreo.Text = datosConfig[10].ToString();

                checkNoVendidos.Checked = Convert.ToBoolean(datosConfig[11]);
                check15 = checkNoVendidos.Checked;

                txtNoVendidos.Text = datosConfig[12].ToString();

                chTicketVentas.Checked = Convert.ToBoolean(datosConfig[25]);

                chkCerrarSesionCorte.Checked = Convert.ToBoolean(datosConfig[26]);
            }
            else
            {
                cn.EjecutarConsulta($"INSERT INTO Configuracion (IDUsuario) VALUES ('{FormPrincipal.userID}')");
            }
        }

        private void checkCBVenta_CheckedChanged(object sender, EventArgs e)
        {
            //if (opcion10 == 0)
            //{
            //    checkCBVenta.CheckedChanged -= checkCBVenta_CheckedChanged;
            //    checkCBVenta.Checked = check10;
            //    Utilidades.MensajePermiso();
            //    checkCBVenta.CheckedChanged += checkCBVenta_CheckedChanged;
            //    return;
            //}

            //var ticketVenta = 0;

            //if (checkCBVenta.Checked)
            //{
            //    ticketVenta = 1;
            //}

            //cn.EjecutarConsulta($"UPDATE Configuracion SET TicketVenta = {ticketVenta} WHERE IDUsuario = {FormPrincipal.userID}");

            var valorCambioCheckBox = checkCBVenta.Checked;

            cn.EjecutarConsulta($"UPDATE Configuracion SET TicketVenta = {valorCambioCheckBox} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void pagWeb_CheckedChanged(object sender, EventArgs e)
        {
            if (opcion11 == 0)
            {
                pagWeb.CheckedChanged -= pagWeb_CheckedChanged;
                pagWeb.Checked = check11;
                Utilidades.MensajePermiso();
                pagWeb.CheckedChanged += pagWeb_CheckedChanged;
                return;
            }

            var habilitado = 0;

            if (pagWeb.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET IniciarProceso = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
            FormPrincipal.pasar = habilitado;
        }

        private void cbMostrarCB_CheckedChanged(object sender, EventArgs e)
        {
            if (opcion13 == 0)
            {
                cbMostrarCB.CheckedChanged -= cbMostrarCB_CheckedChanged;
                cbMostrarCB.Checked = check13;
                Utilidades.MensajePermiso();
                cbMostrarCB.CheckedChanged += cbMostrarCB_CheckedChanged;
                return;
            }

            var habilitado = 0;

            if (cbMostrarCB.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET MostrarCodigoProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbMostrarPrecio_CheckedChanged(object sender, EventArgs e)
        {
            if (opcion12 == 0)
            {
                cbMostrarPrecio.CheckedChanged -= cbMostrarPrecio_CheckedChanged;
                cbMostrarPrecio.Checked = check12;
                Utilidades.MensajePermiso();
                cbMostrarPrecio.CheckedChanged += cbMostrarPrecio_CheckedChanged;
                return;
            }

            var habilitado = 0;

            if (cbMostrarPrecio.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET MostrarPrecioProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbStockNegativo_CheckedChanged(object sender, EventArgs e)
        {
            if (opcion9 == 0)
            {
                cbStockNegativo.CheckedChanged -= cbStockNegativo_CheckedChanged;
                cbStockNegativo.Checked = Properties.Settings.Default.StockNegativo;
                Utilidades.MensajePermiso();
                cbStockNegativo.CheckedChanged += cbStockNegativo_CheckedChanged;
                return;
            }

            Properties.Settings.Default.StockNegativo = cbStockNegativo.Checked;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void chTicketVentas_CheckedChanged(object sender, EventArgs e)
        {
            var habilitado = 0;

            if (chTicketVentas.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET HabilitarTicketVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void checkMayoreo_CheckedChanged(object sender, EventArgs e)
        {
            if (FormPrincipal.userNickName == "MUELAS0" || FormPrincipal.userNickName == "OXXITO")
            {
                if (opcion14 == 0)
                {
                    checkMayoreo.CheckedChanged -= checkMayoreo_CheckedChanged;
                    checkMayoreo.Checked = check14;
                    Utilidades.MensajePermiso();
                    checkMayoreo.CheckedChanged += checkMayoreo_CheckedChanged;
                    return;
                }

                var habilitado = 0;

                if (checkMayoreo.Checked)
                {
                    habilitado = 1;
                    txtMinimoMayoreo.Enabled = true;
                    txtMinimoMayoreo.Focus();
                    cn.EjecutarConsulta($"UPDATE Configuracion SET PrecioMayoreo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
                }
                else
                {
                    txtMinimoMayoreo.Enabled = false;
                    txtMinimoMayoreo.Text = string.Empty;
                    cn.EjecutarConsulta($"UPDATE Configuracion SET PrecioMayoreo = {habilitado}, MinimoMayoreo = 0 WHERE IDUsuario = {FormPrincipal.userID}");
                }
            }
            else
            {
                //MessageBox.Show("Estamos trabajano en esta opción", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //if (opcion14 == 0)
                //{
                //    checkMayoreo.CheckedChanged -= checkMayoreo_CheckedChanged;
                //    checkMayoreo.Checked = check14;
                //    Utilidades.MensajePermiso();
                //    checkMayoreo.CheckedChanged += checkMayoreo_CheckedChanged;
                //    return;
                //}

                //var habilitado = 0;

                //if (checkMayoreo.Checked)
                //{
                //    habilitado = 1;
                //    txtMinimoMayoreo.Enabled = true;
                //    txtMinimoMayoreo.Focus();
                //    cn.EjecutarConsulta($"UPDATE Configuracion SET PrecioMayoreo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
                //}
                //else
                //{
                //    txtMinimoMayoreo.Enabled = false;
                //    txtMinimoMayoreo.Text = string.Empty;
                //    cn.EjecutarConsulta($"UPDATE Configuracion SET PrecioMayoreo = {habilitado}, MinimoMayoreo = 0 WHERE IDUsuario = {FormPrincipal.userID}");
                //}
            }
        }

        private void checkMayoreo_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Estamos trabajano en esta opción", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtMinimoMayoreo_KeyUp(object sender, KeyEventArgs e)
        {
            //var cantidad = txtMinimoMayoreo.Text.Trim();

            //if (string.IsNullOrWhiteSpace(cantidad))
            //{
            //    cantidad = "0";
            //}

            //cn.EjecutarConsulta($"UPDATE Configuracion SET MinimoMayoreo = {cantidad} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void checkNoVendidos_CheckedChanged(object sender, EventArgs e)
        {
            if (FormPrincipal.userNickName == "MUELAS0" || FormPrincipal.userNickName == "OXXITO")
            {
                if (opcion15 == 0)
                {
                    checkNoVendidos.CheckedChanged -= checkNoVendidos_CheckedChanged;
                    checkNoVendidos.Checked = check15;
                    Utilidades.MensajePermiso();
                    checkNoVendidos.CheckedChanged += checkNoVendidos_CheckedChanged;
                    return;
                }

                if (checkNoVendidos.Checked)
                {
                    txtNoVendidos.Enabled = true;
                    txtNoVendidos.Focus();
                    cn.EjecutarConsulta($"UPDATE Configuracion SET checkNoVendidos = 1 WHERE IDUsuario = {FormPrincipal.userID}");
                    FormPrincipal.checkNoVendidos = 1;
                }
                else
                {
                    txtNoVendidos.Enabled = false;
                    txtNoVendidos.Text = string.Empty;
                    cn.EjecutarConsulta($"UPDATE Configuracion SET checkNoVendidos = 0, diasNoVendidos = 0 WHERE IDUsuario = {FormPrincipal.userID}");
                    FormPrincipal.checkNoVendidos = 0;
                }
            }
            else
            {
                //MessageBox.Show("Estamos trabajano en esta opción", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //if (opcion15 == 0)
                //{
                //    checkNoVendidos.CheckedChanged -= checkNoVendidos_CheckedChanged;
                //    checkNoVendidos.Checked = check15;
                //    Utilidades.MensajePermiso();
                //    checkNoVendidos.CheckedChanged += checkNoVendidos_CheckedChanged;
                //    return;
                //}

                //if (checkNoVendidos.Checked)
                //{
                //    txtNoVendidos.Enabled = true;
                //    txtNoVendidos.Focus();
                //    cn.EjecutarConsulta($"UPDATE Configuracion SET checkNoVendidos = 1 WHERE IDUsuario = {FormPrincipal.userID}");
                //    FormPrincipal.checkNoVendidos = 1;
                //}
                //else
                //{
                //    txtNoVendidos.Enabled = false;
                //    txtNoVendidos.Text = string.Empty;
                //    cn.EjecutarConsulta($"UPDATE Configuracion SET checkNoVendidos = 0, diasNoVendidos = 0 WHERE IDUsuario = {FormPrincipal.userID}");
                //    FormPrincipal.checkNoVendidos = 0;
                //}
            }
            
        }

        private void checkNoVendidos_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Estamos trabajano en esta opción", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtNoVendidos_KeyUp(object sender, KeyEventArgs e)
        {
            //var cantidad = txtNoVendidos.Text.Trim();

            //if (string.IsNullOrWhiteSpace(cantidad))
            //{
            //    cantidad = "0";
            //}

            //FormPrincipal.diasNoVendidos = Convert.ToInt32(cantidad);

            //cn.EjecutarConsulta($"UPDATE Configuracion SET diasNoVendidos = {cantidad} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void ConfiguracionGeneral_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.StockNegativo)
            {
                cbStockNegativo.Checked = true;
            }

            VerificarConfiguracion();

            using (DataTable dtConfiguracion = cn.CargarDatos(cs.cargarDatosDeConfiguracion()))
            {
                if (!dtConfiguracion.Rows.Count.Equals(0))
                {
                    var valorBooleanoDelCheckBox = false;
                    foreach (DataRow item in dtConfiguracion.Rows)
                    {
                        #region Código de Barras en Ticket
                        if (item["TicketVenta"].Equals(1))
                        {
                            valorBooleanoDelCheckBox = true;
                        }
                        else if (item["TicketVenta"].Equals(0))
                        {
                            valorBooleanoDelCheckBox = false;
                        }
                        checkCBVenta.Checked = valorBooleanoDelCheckBox;
                        #endregion
                    }
                }
            }
        }

        private void chkCerrarSesionCorte_CheckedChanged(object sender, EventArgs e)
        {
            var habilitado = 0;

            if (chkCerrarSesionCorte.Checked)
            {
                habilitado = 1;
            }
            
            cn.EjecutarConsulta($"UPDATE Configuracion SET CerrarSesionAuto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }
    }
}
