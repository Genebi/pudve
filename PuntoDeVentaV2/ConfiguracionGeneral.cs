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

        bool valorCambioCheckBox = false;

        int contador = 0;

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
            contador++;
            if (contador > 0)
            {
                //using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("CodigoBarrasTicketVenta", FormPrincipal.id_empleado)))
                //{
                //    if (!permisoEmpleado.Rows.Count.Equals(0))
                //    {
                //        foreach (DataRow item in permisoEmpleado.Rows)
                //        {
                //            if (item[0].ToString().Equals("1"))
                //            {
                //                if (opcion10.Equals(0))
                //                {
                //                    Utilidades.MensajePermiso();
                //                    return;
                //                }

                //                var ticketVenta = 0;

                //                valorCambioCheckBox = checkCBVenta.Checked;

                //                if (valorCambioCheckBox.Equals(true))
                //                {
                //                    ticketVenta = 1;
                //                }
                //                else
                //                {
                //                    ticketVenta = 0;
                //                }

                //                cn.EjecutarConsulta($"UPDATE Configuracion SET TicketVenta = {ticketVenta} WHERE IDUsuario = {FormPrincipal.userID}");
                //            }
                //            else
                //            {
                //                MessageBox.Show("No tienes permisos para modificar esta opcion");
                //            }
                //        }
                //    }
                //    else
                //    {
                //        MessageBox.Show("No tienes permisos para modificar esta opcion");
                //        return;
                //    }
                //}
            }
           
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

          
        }

        private void pagWeb_CheckedChanged(object sender, EventArgs e)
        {
            //if (opcion11 == 0)
            //{
            //    pagWeb.CheckedChanged -= pagWeb_CheckedChanged;
            //    pagWeb.Checked = check11;
            //    Utilidades.MensajePermiso();
            //    pagWeb.CheckedChanged += pagWeb_CheckedChanged;
            //    return;
            //}

            //var habilitado = 0;

            //if (pagWeb.Checked)
            //{
            //    habilitado = 1;
            //}

            //cn.EjecutarConsulta($"UPDATE Configuracion SET IniciarProceso = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
            //FormPrincipal.pasar = habilitado;
        }

        private void cbMostrarCB_CheckedChanged(object sender, EventArgs e)
        {
            //if (opcion13 == 0)
            //{
            //    cbMostrarCB.CheckedChanged -= cbMostrarCB_CheckedChanged;
            //    cbMostrarCB.Checked = check13;
            //    Utilidades.MensajePermiso();
            //    cbMostrarCB.CheckedChanged += cbMostrarCB_CheckedChanged;
            //    return;
            //}

            //var habilitado = 0;

            //if (cbMostrarCB.Checked)
            //{
            //    habilitado = 1;
            //}

            //cn.EjecutarConsulta($"UPDATE Configuracion SET MostrarCodigoProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
              
        }

        private void cbMostrarPrecio_CheckedChanged(object sender, EventArgs e)
        {
            //if (opcion12 == 0)
            //{
            //    cbMostrarPrecio.CheckedChanged -= cbMostrarPrecio_CheckedChanged;
            //    cbMostrarPrecio.Checked = check12;
            //    Utilidades.MensajePermiso();
            //    cbMostrarPrecio.CheckedChanged += cbMostrarPrecio_CheckedChanged;
            //    return;
            //}

            //var habilitado = 0;

            //if (cbMostrarPrecio.Checked)
            //{
            //    habilitado = 1;
            //}

            //cn.EjecutarConsulta($"UPDATE Configuracion SET MostrarPrecioProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");

           
            
        }

        private void cbStockNegativo_CheckedChanged(object sender, EventArgs e)
        {
            //if (opcion9 == 0)
            //{
            //    cbStockNegativo.CheckedChanged -= cbStockNegativo_CheckedChanged;
            //    cbStockNegativo.Checked = Properties.Settings.Default.StockNegativo;
            //    Utilidades.MensajePermiso();
            //    cbStockNegativo.CheckedChanged += cbStockNegativo_CheckedChanged;
            //    return;
            //}

            //Properties.Settings.Default.StockNegativo = cbStockNegativo.Checked;
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Reload();

            
           
        }

        private void chTicketVentas_CheckedChanged(object sender, EventArgs e)
        {
            //var habilitado = 0;

            //if (chTicketVentas.Checked)
            //{
            //    habilitado = 1;
            //}

            //cn.EjecutarConsulta($"UPDATE Configuracion SET HabilitarTicketVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");

           
            
        }

        private void checkMayoreo_CheckedChanged(object sender, EventArgs e)
        {
            if (FormPrincipal.userNickName == "MUELAS0" || FormPrincipal.userNickName == "OXXITO")
            {
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

            using (var permisosMensajes = cn.CargarDatos(cs.permisosMensajeVentasInventario()))
            {
                if (permisosMensajes.Rows.Count > 0)
                {
                    int estadoMensajes = Convert.ToInt32(permisosMensajes.Rows[0].ItemArray[0]);

                    if (estadoMensajes == 1)
                    {
                        chkMensajeVenderProducto.Checked = true;
                    }
                    else
                    {
                        chkMensajeVenderProducto.Checked = false;
                    }
                }
                
            }

            using (var permisosInventario = cn.CargarDatos(cs.permisoRealizarInventario()))
            {
                if (permisosInventario.Rows.Count > 0)
                {
                    int estadoMensaje = Convert.ToInt32(permisosInventario.Rows[0].ItemArray[0]);
                    if (estadoMensaje == 1)
                    {
                        chkMensajeRealizarInventario.Checked = true;
                    }
                    else
                    {
                        chkMensajeRealizarInventario.Checked = false;
                    }
                }
            }




            //VerificarConfiguracion();

            using (DataTable dtConfiguracion = cn.CargarDatos(cs.cargarDatosDeConfiguracion()))
            {
                if (!dtConfiguracion.Rows.Count.Equals(0))
                {
                    var valorBooleanoDelCheckBox = false;
                    foreach (DataRow item in dtConfiguracion.Rows)
                    {
                        // secciones por Cada uno de los 
                        // CheckBox de Configuración General
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
                        #region Envió de información a la WEB
                        if (item["IniciarProceso"].Equals(1))
                        {
                            valorBooleanoDelCheckBox = true;
                        }
                        else if (item["IniciarProceso"].Equals(0))
                        {
                            valorBooleanoDelCheckBox = false;
                        }
                        pagWeb.Checked = valorBooleanoDelCheckBox;
                        #endregion
                        #region Mostrar código de productos en ventas
                        if (item["MostrarCodigoProducto"].Equals(1))
                        {
                            valorBooleanoDelCheckBox = true;
                        }
                        else if (item["MostrarCodigoProducto"].Equals(0))
                        {
                            valorBooleanoDelCheckBox = false;
                        }
                        cbMostrarCB.Checked = valorBooleanoDelCheckBox;
                        #endregion
                        #region Cerrar Sesion al Hacer Corte de Caja
                        if (item["CerrarSesionAuto"].Equals(1))
                        {
                            valorBooleanoDelCheckBox = true;
                        }
                        else if (item["CerrarSesionAuto"].Equals(0))
                        {
                            valorBooleanoDelCheckBox = false;
                        }
                        chkCerrarSesionCorte.Checked = valorBooleanoDelCheckBox;
                        #endregion
                        #region Mostrar Precio de Productos en Ventas
                        if (item["MostrarPrecioProducto"].Equals(1))
                        {
                            valorBooleanoDelCheckBox = true;
                        }
                        else if (item["MostrarPrecioProducto"].Equals(0))
                        {
                            valorBooleanoDelCheckBox = false;
                        }
                        cbMostrarPrecio.Checked = valorBooleanoDelCheckBox;
                        #endregion
                        #region Permitir Stock Negativo
                        if (item["StockNegativo"].Equals(1))
                        {
                            valorBooleanoDelCheckBox = true;
                        }
                        else if (item["StockNegativo"].Equals(0))
                        {
                            valorBooleanoDelCheckBox = false;
                        }
                        cbStockNegativo.Checked = valorBooleanoDelCheckBox;
                        #endregion
                        #region Generar Ticket al Realizar Ventas
                        if (item["HabilitarTicketVentas"].Equals(true))
                        {
                            valorBooleanoDelCheckBox = true;
                        }
                        else if (item["HabilitarTicketVentas"].Equals(false))
                        {
                            valorBooleanoDelCheckBox = false;
                        }
                        chTicketVentas.Checked = valorBooleanoDelCheckBox;
                        #endregion
                        #region Activar Precio por Mayoreo en Ventas
                        if (item["PrecioMayoreo"].Equals(1))
                        {
                            valorBooleanoDelCheckBox = true;
                            txtMinimoMayoreo.Enabled = true;
                        }
                        else if (item["PrecioMayoreo"].Equals(0))
                        {
                            valorBooleanoDelCheckBox = false;
                            txtMinimoMayoreo.Enabled = false;
                        }
                        checkMayoreo.Checked = valorBooleanoDelCheckBox;
                        #endregion
                        #region Avisar de Productos no Vendidos
                        if (item["checkNoVendidos"].Equals(1))
                        {
                            valorBooleanoDelCheckBox = true;
                            txtNoVendidos.Enabled = true;
                            txtNoVendidos.Focus();
                        }
                        else if (item["checkNoVendidos"].Equals(0))
                        {
                            valorBooleanoDelCheckBox = false;
                            txtNoVendidos.Enabled = false;
                            txtNoVendidos.Text = string.Empty;
                        }
                        checkNoVendidos.Checked = valorBooleanoDelCheckBox;
                        #endregion
                    }
                }
            }
        }

        private void chkCerrarSesionCorte_CheckedChanged(object sender, EventArgs e)
        {

            
            //var habilitado = 0;

            //if (chkCerrarSesionCorte.Checked)
            //{
            //    habilitado = 1;
            //}

            //cn.EjecutarConsulta($"UPDATE Configuracion SET CerrarSesionAuto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void checkCBVenta_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("CodigoBarrasTicketVenta", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    if (opcion10.Equals(0))
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    var ticketVenta = 0;

                    valorCambioCheckBox = checkCBVenta.Checked;

                    if (valorCambioCheckBox.Equals(true))
                    {
                        ticketVenta = 1;
                    }
                    else
                    {
                        ticketVenta = 0;
                    }

                    cn.EjecutarConsulta($"UPDATE Configuracion SET TicketVenta = {ticketVenta} WHERE IDUsuario = {FormPrincipal.userID}");
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {
                            if (opcion10.Equals(0))
                            {
                                Utilidades.MensajePermiso();
                                return;
                            }

                            var ticketVenta = 0;

                            valorCambioCheckBox = checkCBVenta.Checked;

                            if (valorCambioCheckBox.Equals(true))
                            {
                                ticketVenta = 1;
                            }
                            else
                            {
                                ticketVenta = 0;
                            }

                            cn.EjecutarConsulta($"UPDATE Configuracion SET TicketVenta = {ticketVenta} WHERE IDUsuario = {FormPrincipal.userID}");
                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (checkCBVenta.Checked == true)
                            {
                                checkCBVenta.Checked = false;
                                return;
                            }
                            else
                            {
                                checkCBVenta.Checked = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No tienes permisos para modificar esta opcion");
                    return;
                }
            }
        }

        private void chkCerrarSesionCorte_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("CerrarSesionCorteCaja", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    var habilitado = 0;

                    valorCambioCheckBox = chkCerrarSesionCorte.Checked;

                    if (valorCambioCheckBox.Equals(true))
                    {
                        habilitado = 1;
                    }
                    else
                    {
                        habilitado = 0;
                    }

                    cn.EjecutarConsulta($"UPDATE Configuracion SET CerrarSesionAuto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            var habilitado = 0;

                            valorCambioCheckBox = chkCerrarSesionCorte.Checked;

                            if (valorCambioCheckBox.Equals(true))
                            {
                                habilitado = 1;
                            }
                            else
                            {
                                habilitado = 0;
                            }

                            cn.EjecutarConsulta($"UPDATE Configuracion SET CerrarSesionAuto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (chkCerrarSesionCorte.Checked == true)
                            {
                                chkCerrarSesionCorte.Checked = false;
                                return;
                            }
                            else
                            {
                                chkCerrarSesionCorte.Checked = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No tienes permisos para modificar esta opcion");
                    return;
                }
            }
        }

        private void cbMostrarCB_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("MostrarCodigoProductoVentas", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    if (opcion13.Equals(0))
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    var habilitado = 0;

                    valorCambioCheckBox = cbMostrarCB.Checked;

                    if (valorCambioCheckBox.Equals(true))
                    {
                        habilitado = 1;
                    }
                    else
                    {
                        habilitado = 0;
                    }

                    cn.EjecutarConsulta($"UPDATE Configuracion SET MostrarCodigoProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            if (opcion13.Equals(0))
                            {
                                Utilidades.MensajePermiso();
                                return;
                            }

                            var habilitado = 0;

                            valorCambioCheckBox = cbMostrarCB.Checked;

                            if (valorCambioCheckBox.Equals(true))
                            {
                                habilitado = 1;
                            }
                            else
                            {
                                habilitado = 0;
                            }

                            cn.EjecutarConsulta($"UPDATE Configuracion SET MostrarCodigoProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbMostrarCB.Checked == true)
                            {
                                cbMostrarCB.Checked = false;
                                return;
                            }
                            else
                            {
                                cbMostrarCB.Checked = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No tienes permisos para modificar esta opcion");
                    return;
                }
            }
        }

        private void cbMostrarPrecio_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("MostrarPrecioProductoVentas", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    if (opcion12 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    var habilitado = 0;

                    valorCambioCheckBox = cbMostrarPrecio.Checked;

                    if (valorCambioCheckBox.Equals(true))
                    {
                        habilitado = 1;
                    }
                    else
                    {
                        habilitado = 0;
                    }

                    cn.EjecutarConsulta($"UPDATE Configuracion SET MostrarPrecioProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            if (opcion12 == 0)
                            {
                                Utilidades.MensajePermiso();
                                return;
                            }

                            var habilitado = 0;

                            valorCambioCheckBox = cbMostrarPrecio.Checked;

                            if (valorCambioCheckBox.Equals(true))
                            {
                                habilitado = 1;
                            }
                            else
                            {
                                habilitado = 0;
                            }

                            cn.EjecutarConsulta($"UPDATE Configuracion SET MostrarPrecioProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbMostrarPrecio.Checked == true)
                            {
                                cbMostrarPrecio.Checked = false;
                                return;
                            }
                            else
                            {
                                cbMostrarPrecio.Checked = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No tienes permisos para modificar esta opcion");
                    return;
                }
            }
        }

        private void cbStockNegativo_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("PermitirStockNegativo", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    if (opcion9 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    var habilitado = 0;

                    valorCambioCheckBox = cbStockNegativo.Checked;

                    if (valorCambioCheckBox.Equals(true))
                    {
                        habilitado = 1;
                    }
                    else
                    {
                        habilitado = 0;
                    }

                    cn.EjecutarConsulta($"UPDATE Configuracion SET StockNegativo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            if (opcion9 == 0)
                            {
                                Utilidades.MensajePermiso();
                                return;
                            }

                            var habilitado = 0;

                            valorCambioCheckBox = cbStockNegativo.Checked;

                            if (valorCambioCheckBox.Equals(true))
                            {
                                habilitado = 1;
                            }
                            else
                            {
                                habilitado = 0;
                            }

                            cn.EjecutarConsulta($"UPDATE Configuracion SET StockNegativo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbStockNegativo.Checked == true)
                            {
                                cbStockNegativo.Checked = false;
                                return;
                            }
                            else
                            {
                                cbStockNegativo.Checked = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No tienes permisos para modificar esta opcion");
                    return;
                }
            }
        }

        private void chTicketVentas_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("GenerarTicketAlRealizarVenta", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    var habilitado = false;

                    valorCambioCheckBox = chTicketVentas.Checked;

                    if (valorCambioCheckBox.Equals(true))
                    {
                        habilitado = true;
                    }
                    else
                    {
                        habilitado = false;
                    }

                    cn.EjecutarConsulta($"UPDATE Configuracion SET HabilitarTicketVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            var habilitado = false;

                            valorCambioCheckBox = chTicketVentas.Checked;

                            if (valorCambioCheckBox.Equals(true))
                            {
                                habilitado = true;
                            }
                            else
                            {
                                habilitado = false;
                            }

                            cn.EjecutarConsulta($"UPDATE Configuracion SET HabilitarTicketVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (chTicketVentas.Checked == true)
                            {
                                chTicketVentas.Checked = false;
                                return;
                            }
                            else
                            {
                                chTicketVentas.Checked = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No tienes permisos para modificar esta opcion");
                    return;
                }
            }
        }

        private void checkMayoreo_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("ActivarPrecioMayoreoVentas", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    if (opcion14 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    var habilitado = 0;

                    valorCambioCheckBox = checkMayoreo.Checked;

                    if (valorCambioCheckBox.Equals(true))
                    {
                        habilitado = 1;
                        txtMinimoMayoreo.Enabled = true;
                        txtMinimoMayoreo.Focus();
                    }
                    else
                    {
                        habilitado = 0;
                        txtMinimoMayoreo.Enabled = false;
                        txtMinimoMayoreo.Text = string.Empty;
                    }

                    cn.EjecutarConsulta($"UPDATE Configuracion SET PrecioMayoreo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            if (opcion14 == 0)
                            {
                                Utilidades.MensajePermiso();
                                return;
                            }

                            var habilitado = 0;

                            valorCambioCheckBox = checkMayoreo.Checked;

                            if (valorCambioCheckBox.Equals(true))
                            {
                                habilitado = 1;
                                txtMinimoMayoreo.Enabled = true;
                                txtMinimoMayoreo.Focus();
                            }
                            else
                            {
                                habilitado = 0;
                                txtMinimoMayoreo.Enabled = false;
                                txtMinimoMayoreo.Text = string.Empty;
                            }

                            cn.EjecutarConsulta($"UPDATE Configuracion SET PrecioMayoreo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (checkMayoreo.Checked == true)
                            {
                                checkMayoreo.Checked = false;
                                return;
                            }
                            else
                            {
                                checkMayoreo.Checked = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No tienes permisos para modificar esta opcion");
                    return;
                }
            }
        }

        private void checkNoVendidos_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("AvisarProductosNoVendidos", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    if (opcion15 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    valorCambioCheckBox = checkNoVendidos.Checked;

                    if (valorCambioCheckBox.Equals(true))
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
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            if (opcion15 == 0)
                            {
                                Utilidades.MensajePermiso();
                                return;
                            }

                            valorCambioCheckBox = checkNoVendidos.Checked;

                            if (valorCambioCheckBox.Equals(true))
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
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (checkNoVendidos.Checked == true)
                            {
                                checkNoVendidos.Checked = false;
                                return;
                            }
                            else
                            {
                                checkNoVendidos.Checked = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No tienes permisos para modificar esta opcion");
                    return;
                }
            }
        }

        private void pagWeb_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("HabilitarInfoPaginaWeb", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    if (opcion11.Equals(0))
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    var habilitado = 0;

                    valorCambioCheckBox = pagWeb.Checked;

                    if (valorCambioCheckBox.Equals(true))
                    {
                        habilitado = 1;
                    }
                    else
                    {
                        habilitado = 0;
                    }

                    cn.EjecutarConsulta($"UPDATE Configuracion SET IniciarProceso = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
                    FormPrincipal.pasar = habilitado;
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            if (opcion11.Equals(0))
                            {
                                Utilidades.MensajePermiso();
                                return;
                            }

                            var habilitado = 0;

                            valorCambioCheckBox = pagWeb.Checked;

                            if (valorCambioCheckBox.Equals(true))
                            {
                                habilitado = 1;
                            }
                            else
                            {
                                habilitado = 0;
                            }

                            cn.EjecutarConsulta($"UPDATE Configuracion SET IniciarProceso = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
                            FormPrincipal.pasar = habilitado;

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (pagWeb.Checked == true)
                            {
                                pagWeb.Checked = false;
                                return;
                            }
                            else
                            {
                                pagWeb.Checked = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No tienes permisos para modificar esta opcion");
                    return;
                }
            }
        }

        private void ConfiguracionGeneral_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void chkMensajeVenderProducto_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("MensajeVentas", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    var habilitado = false;

                    valorCambioCheckBox = chkMensajeVenderProducto.Checked;

                    if (valorCambioCheckBox.Equals(true))
                    {
                        habilitado = true;
                    }
                    else
                    {
                        habilitado = false;
                    }

                    cn.EjecutarConsulta($"UPDATE Configuracion SET HabilitarTicketVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            var habilitado = false;

                            valorCambioCheckBox = chkMensajeVenderProducto.Checked;

                            if (valorCambioCheckBox.Equals(true))
                            {
                                habilitado = true;
                            }
                            else
                            {
                                habilitado = false;
                            }

                            cn.EjecutarConsulta($"UPDATE Configuracion SET HabilitarTicketVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");

                            if (chkMensajeVenderProducto.Checked == true)
                            {
                                cn.EjecutarConsulta($"UPDATE productmessage SET ProductMessageActivated = 1");
                            }
                            else
                            {
                                cn.EjecutarConsulta($"UPDATE productmessage SET ProductMessageActivated = 0");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (chkMensajeVenderProducto.Checked == true)
                            {
                                chkMensajeVenderProducto.Checked = false;
                                return;
                            }
                            else
                            {
                                chkMensajeVenderProducto.Checked = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No tienes permisos para modificar esta opcion");
                    return;
                }
            }
        }

        private void chkMensajeRealizarInventario_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("MensajeInventario", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    var habilitado = false;

                    valorCambioCheckBox = chkMensajeRealizarInventario.Checked;

                    if (valorCambioCheckBox.Equals(true))
                    {
                        habilitado = true;
                    }
                    else
                    {
                        habilitado = false;
                    }

                    cn.EjecutarConsulta($"UPDATE Configuracion SET HabilitarTicketVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            var habilitado = false;

                            valorCambioCheckBox = chkMensajeRealizarInventario.Checked;

                            if (valorCambioCheckBox.Equals(true))
                            {
                                habilitado = true;
                            }
                            else
                            {
                                habilitado = false;
                            }

                            cn.EjecutarConsulta($"UPDATE Configuracion SET HabilitarTicketVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");

                            if (chkMensajeRealizarInventario.Checked == true)
                            {
                                cn.EjecutarConsulta($"UPDATE mensajesinventario SET Activo = 1");
                            }
                            else
                            {
                                cn.EjecutarConsulta($"UPDATE mensajesinventario SET Activo = 0");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (chkMensajeRealizarInventario.Checked == true)
                            {
                                chkMensajeRealizarInventario.Checked = false;
                                return;
                            }
                            else
                            {
                                chkMensajeRealizarInventario.Checked = true;
                                return;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No tienes permisos para modificar esta opcion");
                    return;
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Configuracion Guardada con exito");
            this.Close();
        }
    }
}
