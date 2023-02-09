using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class SetUpPUDVE : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();
        RespadoBaseDatos backUp = new RespadoBaseDatos(); 

        private int numeroRevision = 0;

        public static bool recargarDatos = false;
         
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

        int contadorValidarCambioCheckBoxRespaldo = -1;

        List<string> usuariosPermitidos = new List<string>()
        {
            "HOUSEDEPOTAUTLAN",
            "HOUSEDEPOTGRULLO",
            "HOUSEDEPOTREPARTO"
        };

        public SetUpPUDVE()
        {
            InitializeComponent();
        }

        private void SetUpPUDVE_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.StockNegativo)
            {
                cbStockNegativo.Checked = true;
            }

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                txtNombreServidor.Text = Properties.Settings.Default.Hosting;
            }

            VerificarDatosInventario();
            VerificarConfiguracion();

            txtMinimoMayoreo.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtNoVendidos.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtPorcentajeProducto.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtNumeroRevision.KeyPress += new KeyPressEventHandler(SoloDecimales);
            txtNumeroRevision.Text = numeroRevision.ToString();

            if (FormPrincipal.id_empleado > 0)
            {
                var permisos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Configuracion");

                opcion1 = permisos[0];
                opcion2 = permisos[1];
                opcion3 = permisos[2];
                opcion4 = permisos[3];
                opcion5 = permisos[4];
                opcion6 = permisos[5];
                opcion7 = permisos[6];
                opcion8 = permisos[7];
                opcion9 = permisos[8];
                opcion10 = permisos[9];
                opcion11 = permisos[10];
                opcion12 = permisos[11];
                opcion13 = permisos[12];
                opcion14 = permisos[13];
                opcion15 = permisos[14];
                opcion16 = permisos[15];
                opcion17 = permisos[16];
                opcion18 = permisos[17];
                opcion19 = permisos[18];
                opcion20 = permisos[19];
                opcion21 = permisos[20];
                opcion22 = permisos[21];
                opcion23 = permisos[22];
            }


            if (!usuariosPermitidos.Contains(FormPrincipal.userNickName))
            {
                cbCorreoCorteCaja.Enabled = false;
            }

            this.Focus();
            SetUpPUDVE form = this;
            Utilidades.EjecutarAtajoKeyPreviewDown(SetUpPUDVE_PreviewKeyDown, form);

            if (cboTipoMoneda.Text.Equals(string.Empty))
            {
                cboTipoMoneda.SelectedIndex = 113;
                FormPrincipal.Moneda = cboTipoMoneda.Text;
            }
        }

        private void VerificarDatosInventario()
        {
            // Numero de revision inventario
            var datosInventario = mb.DatosRevisionInventario();

            // Si existe un registro en la tabla obtiene los datos de lo contrario hace un insert para
            // que exista la configuracion necesaria
            if (datosInventario.Length > 0)
            {
                datosInventario = mb.DatosRevisionInventario();
                numeroRevision = Convert.ToInt32(datosInventario[1]);
            }
            else
            {
                cn.EjecutarConsulta($"INSERT INTO CodigoBarrasGenerado (IDUsuario, FechaInventario, NoRevision) VALUES ('{FormPrincipal.userID}', '{DateTime.Now.ToString("yyyy-MM-dd")}', '1')", true);

                datosInventario = mb.DatosRevisionInventario();
                numeroRevision = Convert.ToInt32(datosInventario[1]);
            }
        }

        private void VerificarConfiguracion()
        {
            var existe = (bool)cn.EjecutarSelect($"SELECT * FROM Configuracion WHERE IDUsuario = {FormPrincipal.userID}");

            if (existe)
            {
                var datosConfig = mb.ComprobarConfiguracion();

                checkCBVenta.Checked = Convert.ToBoolean(datosConfig[4]);
                check10 = checkCBVenta.Checked;

                cbCorreoPrecioProducto.Checked = Convert.ToBoolean(datosConfig[0]);
                check5 = cbCorreoPrecioProducto.Checked;

                cbCorreoStockProducto.Checked = Convert.ToBoolean(datosConfig[1]);
                check6 = cbCorreoStockProducto.Checked;

                cbCorreoStockMinimo.Checked = Convert.ToBoolean(datosConfig[2]);
                check7 = cbCorreoStockMinimo.Checked;

                cbCorreoVenderProducto.Checked = Convert.ToBoolean(datosConfig[3]);
                check8 = cbCorreoVenderProducto.Checked;

                pagWeb.Checked = Convert.ToBoolean(datosConfig[5]);
                check11 = pagWeb.Checked;

                cbMostrarPrecio.Checked = Convert.ToBoolean(datosConfig[6]);
                check12 = cbMostrarPrecio.Checked;

                cbMostrarCB.Checked = Convert.ToBoolean(datosConfig[7]);
                check13 = cbMostrarCB.Checked;

                txtPorcentajeProducto.Text = datosConfig[8].ToString();

                checkMayoreo.Checked = Convert.ToBoolean(datosConfig[9]);
                check14 = checkMayoreo.Checked;
                txtMinimoMayoreo.Text = datosConfig[10].ToString();

                checkNoVendidos.Checked = Convert.ToBoolean(datosConfig[11]);
                check15 = checkNoVendidos.Checked;
                txtNoVendidos.Text = datosConfig[12].ToString();

                cbCorreoAgregarDineroCaja.Checked = Convert.ToBoolean(datosConfig[13]);
                check16 = cbCorreoAgregarDineroCaja.Checked;

                cbCorreoRetirarDineroCaja.Checked = Convert.ToBoolean(datosConfig[14]);
                check17 = cbCorreoRetirarDineroCaja.Checked;

                cbCorreoCerrarVentanaVentas.Checked = Convert.ToBoolean(datosConfig[15]);
                check18 = cbCorreoCerrarVentanaVentas.Checked;

                cbCorreoEliminarListaProductosVentas.Checked = Convert.ToBoolean(datosConfig[19]);
                check22 = cbCorreoEliminarListaProductosVentas.Checked;

                cbCorreoCorteCaja.Checked = Convert.ToBoolean(datosConfig[20]);
                check23 = cbCorreoCorteCaja.Checked;

                cbCorreoVenta.Checked = Convert.ToBoolean(datosConfig[21]);

                cbCorreoIniciar.Checked = Convert.ToBoolean(datosConfig[22]);

                cbCorreoDescuento.Checked = Convert.ToBoolean(datosConfig[23]);

                chRespaldo.Checked = Convert.ToBoolean(datosConfig[24]);

                chTicketVentas.Checked = Convert.ToBoolean(datosConfig[25]);
            }
            else
            {
                cn.EjecutarConsulta($"INSERT INTO Configuracion (IDUsuario) VALUES ('{FormPrincipal.userID}')");
            }
        }

        private void btnRespaldo_Click(object sender, EventArgs e)
        {
            if (opcion4 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            //guardarArchivo.FileName = $"{FormPrincipal.userNickName}";
            //guardarArchivo.Filter = "SQL (*.sql)|*.sql";
            //guardarArchivo.FilterIndex = 1;
            //guardarArchivo.RestoreDirectory = true;

            //if (guardarArchivo.ShowDialog() == DialogResult.OK)
            //{
            //    try
            //    {
            //        string conexion = string.Empty;

            //        if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            //        {
            //            conexion = "datasource=" + Properties.Settings.Default.Hosting + ";port=6666;username=root;password=;database=pudve;";
            //        }
            //        else
            //        {
            //            conexion = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
            //        }

            //        // Important Additional Connection Options
            //        conexion += "charset=utf8;convertzerodatetime=true;";

            //        string archivo = guardarArchivo.FileName;

            //        using (MySqlConnection con = new MySqlConnection(conexion))
            //        {
            //            using (MySqlCommand cmd = new MySqlCommand())
            //            {
            //                using (MySqlBackup backup = new MySqlBackup(cmd))
            //                {
            //                    cmd.Connection = con;
            //                    con.Open();
            //                    backup.ExportToFile(archivo);
            //                    con.Close();


            //                    //if (validarMandarRespaldoCorreo())
            //                    //{
            //                    //    Enviar la base de datos por correo
            //                    //    Thread hilo = new Thread(() => Utilidades.sendEmail(archivo));
            //                    //    hilo.Start();
            //                    //}
            //                }
            //            }
            //        }

            //        MessageBox.Show("Información respaldada exitosamente", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.ToString(), "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
            EscogerTipoRespaldo tipoRespaldo = new EscogerTipoRespaldo();
            var tipo = 0;

            tipoRespaldo.FormClosed += delegate
            {
                tipo = EscogerTipoRespaldo.typeBackUp;


                MessageBox.Show("Este proceso tardara unos minutos.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                backUp.crearsaveFile(tipo);
            };

            tipoRespaldo.ShowDialog();
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

        private void btnGuardarServidor_Click(object sender, EventArgs e)
        {
            if (opcion1 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            Properties.Settings.Default.Hosting = txtNombreServidor.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();

            MessageBox.Show("Información guardada", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGuardarRevision_Click(object sender, EventArgs e)
        {
            var validacionPunto = string.Empty;

            validacionPunto = txtNumeroRevision.Text;

            if (!validacionPunto.Equals("."))
            {
                if (opcion2 == 0)
                {
                    Utilidades.MensajePermiso();
                    return;
                }

                var numeroRevision = txtNumeroRevision.Text;

                if (string.IsNullOrWhiteSpace(numeroRevision))
                {
                    MessageBox.Show("Es necesario asignar un número de revisión", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var respuesta = cn.EjecutarConsulta($"UPDATE CodigoBarrasGenerado SET NoRevision = {numeroRevision} WHERE IDUsuario = {FormPrincipal.userID}", true);

                if (respuesta > 0)
                {
                    MessageBox.Show("Información guardada", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Por favor ingrese numeros", "¡Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumeroRevision.Focus();
            }
        }

        private void SoloDecimales(object sender, KeyPressEventArgs e)
        {
            //permite 0-9, eliminar y decimal
            if (((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8 && e.KeyChar != 46))
            {
                e.Handled = true;
                return;
            }

            //verifica que solo un decimal este permitido
            if (e.KeyChar == 46)
            {
                if ((sender as TextBox).Text.IndexOf(e.KeyChar) != -1)
                {
                    e.Handled = true;
                }
            }
        }

        private void SetUpPUDVE_Paint(object sender, PaintEventArgs e)
        {
            if (recargarDatos)
            {
                VerificarDatosInventario();
                txtNumeroRevision.Text = numeroRevision.ToString();
                recargarDatos = false;
            }
        }


        private void checkCBVenta_CheckedChanged(object sender, EventArgs e)
        {
            if (opcion10 == 0)
            {
                checkCBVenta.CheckedChanged -= checkCBVenta_CheckedChanged;
                checkCBVenta.Checked = check10;
                Utilidades.MensajePermiso();
                checkCBVenta.CheckedChanged += checkCBVenta_CheckedChanged;
                return;
            }

            var ticketVenta = 0;

            if (checkCBVenta.Checked)
            {
                ticketVenta = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET TicketVenta = {ticketVenta} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbCorreoPrecioProducto_CheckedChanged(object sender, EventArgs e)
        {
            if (opcion5 == 0)
            {
                cbCorreoPrecioProducto.CheckedChanged -= cbCorreoPrecioProducto_CheckedChanged;
                cbCorreoPrecioProducto.Checked = check5;
                Utilidades.MensajePermiso();
                cbCorreoPrecioProducto.CheckedChanged += cbCorreoPrecioProducto_CheckedChanged;
                return;
            }

            var habilitado = 0;

            if (cbCorreoPrecioProducto.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoPrecioProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbCorreoStockProducto_CheckedChanged(object sender, EventArgs e)
        {
            if (opcion6 == 0)
            {
                cbCorreoStockProducto.CheckedChanged -= cbCorreoStockProducto_CheckedChanged;
                cbCorreoStockProducto.Checked = check6;
                Utilidades.MensajePermiso();
                cbCorreoStockProducto.CheckedChanged += cbCorreoStockProducto_CheckedChanged;
                return;
            }

            var habilitado = 0;

            if (cbCorreoStockProducto.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoStockProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbCorreoStockMinimo_CheckedChanged(object sender, EventArgs e)
        {
            if (opcion7 == 0)
            {
                cbCorreoStockMinimo.CheckedChanged -= cbCorreoStockMinimo_CheckedChanged;
                cbCorreoStockMinimo.Checked = check7;
                Utilidades.MensajePermiso();
                cbCorreoStockMinimo.CheckedChanged += cbCorreoStockMinimo_CheckedChanged;
                return;
            }

            var habilitado = 0;

            if (cbCorreoStockMinimo.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoStockMinimo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbCorreoVenderProducto_CheckedChanged(object sender, EventArgs e)
        {
            if (opcion8 == 0)
            {
                cbCorreoVenderProducto.CheckedChanged -= cbCorreoVenderProducto_CheckedChanged;
                cbCorreoVenderProducto.Checked = check8;
                Utilidades.MensajePermiso();
                cbCorreoVenderProducto.CheckedChanged += cbCorreoVenderProducto_CheckedChanged;
                return;
            }

            var habilitado = 0;

            if (cbCorreoVenderProducto.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoVentaProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
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

        private void btnGuardarPorcentaje_Click(object sender, EventArgs e)
        {

            var validacionPunto = string.Empty;
            validacionPunto = txtPorcentajeProducto.Text;

            if (!validacionPunto.Equals("."))
            {
                if (opcion3 == 0)
                {
                    Utilidades.MensajePermiso();
                    return;
                }

                var porcentaje = txtPorcentajeProducto.Text.Trim();

                if (string.IsNullOrWhiteSpace(porcentaje))
                {
                    MessageBox.Show("Ingrese la cantidad de porcentaje", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPorcentajeProducto.Focus();
                    return;
                }

                var respuesta = cn.EjecutarConsulta($"UPDATE Configuracion SET PorcentajePrecio = {porcentaje} WHERE IDUsuario = {FormPrincipal.userID}");

                if (respuesta > 0)
                {
                    MessageBox.Show("Información guardada", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {

                MessageBox.Show("Por favor ingrese numeros", "¡Advertencia!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPorcentajeProducto.Focus();
            }

        }

        private void checkMayoreo_CheckedChanged(object sender, EventArgs e)
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

        private void SetUpPUDVE_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnGuardarServidor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnGuardarRevision_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnGuardarPorcentaje_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnRespaldo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void cbCorreoAgregarDineroCaja_CheckedChanged(object sender, EventArgs e)
        {
            if (opcion16.Equals(0))
            {
                cbCorreoAgregarDineroCaja.CheckedChanged -= cbCorreoAgregarDineroCaja_CheckedChanged;
                cbCorreoAgregarDineroCaja.Checked = check16;
                Utilidades.MensajePermiso();
                cbCorreoAgregarDineroCaja.CheckedChanged += cbCorreoAgregarDineroCaja_CheckedChanged;
                return;
            }

            var habilitado = 0;

            if (cbCorreoAgregarDineroCaja.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoAgregarDineroCaja = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbCorreoRetirarDineroCaja_CheckedChanged(object sender, EventArgs e)
        {
            if (opcion17.Equals(0))
            {
                cbCorreoRetirarDineroCaja.CheckedChanged -= cbCorreoRetirarDineroCaja_CheckedChanged;
                cbCorreoRetirarDineroCaja.Checked = check17;
                Utilidades.MensajePermiso();
                cbCorreoRetirarDineroCaja.CheckedChanged += cbCorreoRetirarDineroCaja_CheckedChanged;
                return;
            }

            var habilitado = 0;

            if (cbCorreoRetirarDineroCaja.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoRetiroDineroCaja = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbCorreoCerrarVentanaVentas_CheckedChanged(object sender, EventArgs e)
        {
            if (opcion18.Equals(0))
            {
                cbCorreoCerrarVentanaVentas.CheckedChanged -= cbCorreoCerrarVentanaVentas_CheckedChanged;
                cbCorreoCerrarVentanaVentas.Checked = check18;
                Utilidades.MensajePermiso();
                cbCorreoCerrarVentanaVentas.CheckedChanged += cbCorreoCerrarVentanaVentas_CheckedChanged;
                return;
            }

            var habilitado = 0;

            if (cbCorreoCerrarVentanaVentas.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoCerrarVentanaVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbCorreoEliminarListaProductosVentas_CheckedChanged(object sender, EventArgs e)
        {
            if (opcion22.Equals(0))
            {
                cbCorreoEliminarListaProductosVentas.CheckedChanged -= cbCorreoEliminarListaProductosVentas_CheckedChanged;
                cbCorreoEliminarListaProductosVentas.Checked = check22;
                Utilidades.MensajePermiso();
                cbCorreoEliminarListaProductosVentas.CheckedChanged += cbCorreoEliminarListaProductosVentas_CheckedChanged;
                return;
            }

            var habilitado = 0;

            if (cbCorreoEliminarListaProductosVentas.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoEliminarListaProductoVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoEliminarProductoVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoEliminarUltimoProductoAgregadoVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoRestarProductoVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbCorreoCorteCaja_CheckedChanged(object sender, EventArgs e)
        {
            if (usuariosPermitidos.Contains(FormPrincipal.userNickName))
            {
                if (opcion23.Equals(0))
                {
                    cbCorreoCorteCaja.CheckedChanged -= cbCorreoCorteCaja_CheckedChanged;
                    cbCorreoCorteCaja.Checked = check23;
                    Utilidades.MensajePermiso();
                    cbCorreoCorteCaja.CheckedChanged += cbCorreoCorteCaja_CheckedChanged;
                    return;
                }

                var habilitado = 0;

                if (cbCorreoCorteCaja.Checked)
                {
                    habilitado = 1;
                }

                cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoCorteDeCaja = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
            }            
        }

        private void SetUpPUDVE_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)

        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNombreServidor.Focus())
                {
                    btnGuardarServidor.PerformClick();
                }
                else if (txtNumeroRevision.Focus())
                {
                    btnGuardarRevision.PerformClick();
                }
                else if (txtPorcentajeProducto.Focus())
                {
                    btnGuardarPorcentaje.PerformClick();
                }

            }
        }

        private void checkMayoreo_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Estamos trabajano en esta opción", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkNoVendidos_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Estamos trabajano en esta opción", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void cbCorreoCorteCaja_Click(object sender, EventArgs e)
        {
            if (!usuariosPermitidos.Contains(FormPrincipal.userNickName))
            {
                MessageBox.Show("Estamos trabajando en esta opción", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void cbCorreoVenta_CheckedChanged(object sender, EventArgs e)
        {
            var habilitado = 0;

            if (cbCorreoVenta.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoVenta = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbCorreoIniciar_CheckedChanged(object sender, EventArgs e)
        {
            var habilitado = 0;

            if (cbCorreoIniciar.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoIniciarSesion = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cbCorreoDescuento_CheckedChanged(object sender, EventArgs e)
        {
            var habilitado = 0;

            if (cbCorreoDescuento.Checked)
            {
                habilitado = 1;
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoVentaDescuento = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void chRespaldo_CheckedChanged(object sender, EventArgs e)
        {
            //contadorValidarCambioCheckBoxRespaldo += 1;
            var habilitado = 0;
            
            if (chRespaldo.Checked)
            {
                habilitado = 1;
                //if (contadorValidarCambioCheckBoxRespaldo > 0 && chRespaldo.Checked)
                //{
                //    EscogerTipoRespaldo tipoRespaldo = new EscogerTipoRespaldo();

                //    tipoRespaldo.ShowDialog();

                //    tipoRespaldo.FormClosed += delegate
                //    {
                //        habilitado = EscogerTipoRespaldo.typeBackUp;
                //    };
                //}
            }

            cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoRespaldo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
        }

        private void cboTipoMoneda_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormPrincipal.Moneda = cboTipoMoneda.SelectedItem.ToString();
            Productos producto = Application.OpenForms.OfType<Productos>().FirstOrDefault();

            if (producto != null)
            {
                producto.recargarDGV();
            }
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

        private void botonRedondo1_Click(object sender, EventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.PermisosEmpleadosSetupPudve(FormPrincipal.id_empleado,"editarTicket")))
            {
                if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            EditarTicket editTicket = new EditarTicket();
                            editTicket.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            return;
                        }
                    }
                }
                else
                {
                    EditarTicket editTicket = new EditarTicket();
                    editTicket.ShowDialog();
                }
            }
        }

        private void btnEnvioCorreo_Click(object sender, EventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.PermisosEmpleadosSetupPudve(FormPrincipal.id_empleado, "EnvioCorreo")))
            {
                if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {
                            EnvioDeCorreo boton = new EnvioDeCorreo();
                            boton.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            return;
                        }
                    }
                }
                else
                {
                    EnvioDeCorreo boton = new EnvioDeCorreo();
                    boton.ShowDialog();
                }
            }
        }

        private void btnRespaldarInformacion_Click(object sender, EventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.PermisosEmpleadosSetupPudve(FormPrincipal.id_empleado, "RespaldarInfo")))
            {
                if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            if (opcion4 == 0)
                            {
                                Utilidades.MensajePermiso();
                                return;
                            }

                            //guardarArchivo.FileName = $"{FormPrincipal.userNickName}";
                            //guardarArchivo.Filter = "SQL (*.sql)|*.sql";
                            //guardarArchivo.FilterIndex = 1;
                            //guardarArchivo.RestoreDirectory = true;

                            //if (guardarArchivo.ShowDialog() == DialogResult.OK)
                            //{
                            //    try
                            //    {
                            //        string conexion = string.Empty;

                            //        if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
                            //        {
                            //            conexion = "datasource=" + Properties.Settings.Default.Hosting + ";port=6666;username=root;password=;database=pudve;";
                            //        }
                            //        else
                            //        {
                            //            conexion = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
                            //        }

                            //        // Important Additional Connection Options
                            //        conexion += "charset=utf8;convertzerodatetime=true;";

                            //        string archivo = guardarArchivo.FileName;

                            //        using (MySqlConnection con = new MySqlConnection(conexion))
                            //        {
                            //            using (MySqlCommand cmd = new MySqlCommand())
                            //            {
                            //                using (MySqlBackup backup = new MySqlBackup(cmd))
                            //                {
                            //                    cmd.Connection = con;
                            //                    con.Open();
                            //                    backup.ExportToFile(archivo);
                            //                    con.Close();


                            //                    //if (validarMandarRespaldoCorreo())
                            //                    //{
                            //                    //    Enviar la base de datos por correo
                            //                    //    Thread hilo = new Thread(() => Utilidades.sendEmail(archivo));
                            //                    //    hilo.Start();
                            //                    //}
                            //                }
                            //            }
                            //        }

                            //        MessageBox.Show("Información respaldada exitosamente", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        MessageBox.Show(ex.ToString(), "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            //    }
                            //}
                            EscogerTipoRespaldo tipoRespaldo = new EscogerTipoRespaldo();
                            var tipo = 0;

                            tipoRespaldo.FormClosed += delegate
                            {
                                if (EscogerTipoRespaldo.estadoBoton == true)
                                {
                                    tipo = EscogerTipoRespaldo.typeBackUp;

                                    MessageBox.Show("Este proceso tardara unos minutos.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    backUp.crearsaveFile(tipo);
                                }
                            };

                            tipoRespaldo.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            return;
                        }
                    }
                }
                else
                {
                    if (opcion4 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    //guardarArchivo.FileName = $"{FormPrincipal.userNickName}";
                    //guardarArchivo.Filter = "SQL (*.sql)|*.sql";
                    //guardarArchivo.FilterIndex = 1;
                    //guardarArchivo.RestoreDirectory = true;

                    //if (guardarArchivo.ShowDialog() == DialogResult.OK)
                    //{
                    //    try
                    //    {
                    //        string conexion = string.Empty;

                    //        if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
                    //        {
                    //            conexion = "datasource=" + Properties.Settings.Default.Hosting + ";port=6666;username=root;password=;database=pudve;";
                    //        }
                    //        else
                    //        {
                    //            conexion = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
                    //        }

                    //        // Important Additional Connection Options
                    //        conexion += "charset=utf8;convertzerodatetime=true;";

                    //        string archivo = guardarArchivo.FileName;

                    //        using (MySqlConnection con = new MySqlConnection(conexion))
                    //        {
                    //            using (MySqlCommand cmd = new MySqlCommand())
                    //            {
                    //                using (MySqlBackup backup = new MySqlBackup(cmd))
                    //                {
                    //                    cmd.Connection = con;
                    //                    con.Open();
                    //                    backup.ExportToFile(archivo);
                    //                    con.Close();


                    //                    //if (validarMandarRespaldoCorreo())
                    //                    //{
                    //                    //    Enviar la base de datos por correo
                    //                    //    Thread hilo = new Thread(() => Utilidades.sendEmail(archivo));
                    //                    //    hilo.Start();
                    //                    //}
                    //                }
                    //            }
                    //        }

                    //        MessageBox.Show("Información respaldada exitosamente", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        MessageBox.Show(ex.ToString(), "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    }
                    //}
                    EscogerTipoRespaldo tipoRespaldo = new EscogerTipoRespaldo();
                    var tipo = 0;

                    tipoRespaldo.FormClosed += delegate
                    {
                        if (EscogerTipoRespaldo.estadoBoton == true)
                        {
                            tipo = EscogerTipoRespaldo.typeBackUp;

                            MessageBox.Show("Este proceso tardara unos minutos.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            backUp.crearsaveFile(tipo);
                        }
                    };

                    tipoRespaldo.ShowDialog();
                }
            }
           
        }

        private void btnConfiguracionGeneral_Click(object sender, EventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.PermisosEmpleadosSetupPudve(FormPrincipal.id_empleado, "confiGeneral")))
            {
                if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {
                            ConfiguracionGeneral confiGeneral = new ConfiguracionGeneral();
                            confiGeneral.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            return;
                        }
                    }
                }
                else
                {
                    ConfiguracionGeneral confiGeneral = new ConfiguracionGeneral();
                    confiGeneral.ShowDialog();
                }
            }
            
        }

        private void botonRedondo4_Click(object sender, EventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.PermisosEmpleadosSetupPudve(FormPrincipal.id_empleado, "porcentajeGanancia")))
            {
                if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            PorcentageGanancia ganacia = new PorcentageGanancia();
                            ganacia.ShowDialog();

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            return;
                        }
                    }
                }
                else
                {
                    PorcentageGanancia ganacia = new PorcentageGanancia();
                    ganacia.ShowDialog();
                }
            }
           
        }

        private void botonRedondo5_Click(object sender, EventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.PermisosEmpleadosSetupPudve(FormPrincipal.id_empleado, "tipoMoneda")))
            {
                if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {
                            TipoDeMoneda moneda = new TipoDeMoneda();
                            moneda.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            return;
                        }
                    }
                }
                else
                {
                    TipoDeMoneda moneda = new TipoDeMoneda();
                    moneda.ShowDialog();
                }
            }
            
        }

        private void btnCredito_Click(object sender, EventArgs e)
        {
            configCredito creditoConfig = new configCredito();
            creditoConfig.ShowDialog();
        }

        private void botonRedondo2_Click(object sender, EventArgs e)
        {
            ConfiguracionTickets tickets = new ConfiguracionTickets();
            tickets.ShowDialog();
        }
    }
}