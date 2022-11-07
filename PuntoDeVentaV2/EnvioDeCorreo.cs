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
    public partial class EnvioDeCorreo : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

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
        int opcion24 = 1;  // Recibir Anricipo

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
        bool check24 = false;
        bool check25 = false;
        int guardado =0;

        List<String> confiCorreo;

        List<string> usuariosPermitidos = new List<string>()
        {
            "OXXITO",
            "MUELAS0",
            "HOUSEDEPOTAUTLAN", 
            "HOUSEDEPOTGRULLO",
            "HOUSEDEPOTREPARTO"
        }; 
        public EnvioDeCorreo() 
        { 
            InitializeComponent(); 

        }
        private void VerificarConfiguracion()
        {
            var existe = (bool)cn.EjecutarSelect($"SELECT * FROM Configuracion WHERE IDUsuario = {FormPrincipal.userID}");

            if (existe)
            {
                var datosConfig = mb.ComprobarConfiguracion();

                cbCorreoPrecioProducto.Checked = Convert.ToBoolean(datosConfig[0]);
                check5 = cbCorreoPrecioProducto.Checked;

                cbCorreoStockProducto.Checked = Convert.ToBoolean(datosConfig[1]);
                check6 = cbCorreoStockProducto.Checked;

                cbCorreoStockMinimo.Checked = Convert.ToBoolean(datosConfig[2]);
                check7 = cbCorreoStockMinimo.Checked;

                cbCorreoVenderProducto.Checked = Convert.ToBoolean(datosConfig[3]);
                check8 = cbCorreoVenderProducto.Checked;

                cbCorreoAgregarDineroCaja.Checked = Convert.ToBoolean(datosConfig[13]);
                check16 = cbCorreoAgregarDineroCaja.Checked;

                cbCorreoRetirarDineroCaja.Checked = Convert.ToBoolean(datosConfig[14]);
                check17 = cbCorreoRetirarDineroCaja.Checked;

                cbCorreoCerrarVentanaVentas.Checked = Convert.ToBoolean(datosConfig[15]);
                check18 = cbCorreoCerrarVentanaVentas.Checked;

                cbCorreoEliminarListaProductosVentas.Checked = Convert.ToBoolean(datosConfig[19]);
                check22 = cbCorreoEliminarListaProductosVentas.Checked;

                //cbCorreoCorteCaja.Checked = Convert.ToBoolean(datosConfig[20]);
                //check23 = cbCorreoCorteCaja.Checked;

                cbCorreoVenta.Checked = Convert.ToBoolean(datosConfig[21]);

                cbCorreoIniciar.Checked = Convert.ToBoolean(datosConfig[22]);

                cbCorreoDescuento.Checked = Convert.ToBoolean(datosConfig[23]);

                chRespaldo.Checked = Convert.ToBoolean(datosConfig[24]);

                cbRecibirAnricipo.Checked = Convert.ToBoolean(datosConfig[27]);
                check24 = cbRecibirAnricipo.Checked;
                CBXClienteDescuento.Checked = Convert.ToBoolean(datosConfig[28]);
                check25 = CBXClienteDescuento.Checked;
            }
            else
            {
                cn.EjecutarConsulta($"INSERT INTO Configuracion (IDUsuario) VALUES ('{FormPrincipal.userID}')");
            }
        }

        private void cbCorreoAgregarDineroCaja_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void cbCorreoRetirarDineroCaja_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void cbCorreoCorteCaja_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCorreoCorteCaja.Checked.Equals(true))
            {
                MessageBox.Show("Estamos trabajando en esta opción", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cbCorreoCorteCaja.Checked = false;
            }
            
            //if (!usuariosPermitidos.Contains(FormPrincipal.userNickName))
            //{
            //    MessageBox.Show("Estamos trabajando en esta opción", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void cbCorreoCorteCaja_Click(object sender, EventArgs e)
        {
            //if (!usuariosPermitidos.Contains(FormPrincipal.userNickName))
            //{
            //    MessageBox.Show("Estamos trabajando en esta opción", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void cbCorreoStockMinimo_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void cbCorreoStockProducto_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void cbCorreoPrecioProducto_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void cbCorreoVenderProducto_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void cbCorreoCerrarVentanaVentas_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void cbCorreoEliminarListaProductosVentas_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void cbCorreoVenta_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void cbCorreoIniciar_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void cbCorreoDescuento_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void chRespaldo_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void EnvioDeCorreo_Load(object sender, EventArgs e)
        {
            VerificarConfiguracion();
            confiCorreo = new List<string>();
        }

        private void cbCorreoAgregarDineroCaja_MouseClick(object sender, MouseEventArgs e)
        {

            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("AgregarDineroCaja", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
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

                    string consulta = $"UPDATE Configuracion SET CorreoAgregarDineroCaja = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
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

                            string consulta= $"UPDATE Configuracion SET CorreoAgregarDineroCaja = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta);
                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbCorreoAgregarDineroCaja.Checked == true)
                            {
                                cbCorreoAgregarDineroCaja.Checked = false;
                                return;
                            }
                            else
                            {
                                cbCorreoAgregarDineroCaja.Checked = true;
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

        private void cbCorreoRetirarDineroCaja_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("RetirarDineroCaja", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
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

                    string Consulta=$"UPDATE Configuracion SET CorreoRetiroDineroCaja = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(Consulta);
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
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

                            string consulta=$"UPDATE Configuracion SET CorreoRetiroDineroCaja = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta);
                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbCorreoRetirarDineroCaja.Checked == true)
                            {
                                cbCorreoRetirarDineroCaja.Checked = false;
                                return;
                            }
                            else
                            {
                                cbCorreoRetirarDineroCaja.Checked = true;
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

        private void cbCorreoCorteCaja_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("HacerCorteCaja", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
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

                        string consulta = $"UPDATE Configuracion SET CorreoCorteDeCaja = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                        confiCorreo.Add(consulta);
                    }
                    else if (!permisoEmpleado.Rows.Count.Equals(0))
                    {
                        foreach (DataRow item in permisoEmpleado.Rows)
                        {
                            if (item[0].ToString().Equals("1"))
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

                                    string consulta = $"UPDATE Configuracion SET CorreoCorteDeCaja = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                                    confiCorreo.Add(consulta);
                                }

                            }
                            else
                            {
                                MessageBox.Show("No tienes permisos para modificar esta opcion");
                                if (cbCorreoCorteCaja.Checked == true)
                                {
                                    cbCorreoCorteCaja.Checked = false;
                                    return;
                                }
                                else
                                {
                                    cbCorreoCorteCaja.Checked = true;
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
        }

        private void cbCorreoStockMinimo_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("LlegarStockMinimo", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
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

                    string consulta = $"UPDATE Configuracion SET CorreoStockMinimo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
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

                            string consulta = $"UPDATE Configuracion SET CorreoStockMinimo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta);

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbCorreoStockMinimo.Checked == true)
                            {
                                cbCorreoStockMinimo.Checked = false;
                                return;
                            }
                            else
                            {
                                cbCorreoStockMinimo.Checked = true;
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

        private void cbCorreoPrecioProducto_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("ModificarPrecio", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
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

                    string consulta = $"UPDATE Configuracion SET CorreoPrecioProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
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

                            string consulta = $"UPDATE Configuracion SET CorreoPrecioProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta);

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbCorreoPrecioProducto.Checked == true)
                            {
                                cbCorreoPrecioProducto.Checked = false;
                                return;
                            }
                            else
                            {
                                cbCorreoPrecioProducto.Checked = true;
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

        private void cbCorreoStockProducto_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("ModificarStock", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
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

                    string consulta = $"UPDATE Configuracion SET CorreoStockProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
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

                            string consulta =$"UPDATE Configuracion SET CorreoStockProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta);

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbCorreoStockProducto.Checked == true)
                            {
                                cbCorreoStockProducto.Checked = false;
                                return;
                            }
                            else
                            {
                                cbCorreoStockProducto.Checked = true;
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

        private void cbCorreoVenderProducto_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("VenderseProducto", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
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

                    string consulta = $"UPDATE Configuracion SET CorreoVentaProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);
                 }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
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

                            string conulta =$"UPDATE Configuracion SET CorreoVentaProducto = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(conulta);

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbCorreoVenderProducto.Checked == true)
                            {
                                cbCorreoVenderProducto.Checked = false;
                                return;
                            }
                            else
                            {
                                cbCorreoVenderProducto.Checked = true;
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

        private void cbCorreoCerrarVentanaVentas_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("CerrarVentanaVentasCuandoSeTienenProductos", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
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

                    string consulta=$"UPDATE Configuracion SET CorreoCerrarVentanaVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
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

                            string consulta = $"UPDATE Configuracion SET CorreoCerrarVentanaVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta);

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbCorreoCerrarVentanaVentas.Checked == true)
                            {
                                cbCorreoCerrarVentanaVentas.Checked = false;
                                return;
                            }
                            else
                            {
                                cbCorreoCerrarVentanaVentas.Checked = true;
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

        private void cbCorreoEliminarListaProductosVentas_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("EliminarProductosVentas", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
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
                    var consulta = string.Empty;

                    consulta=$"UPDATE Configuracion SET CorreoEliminarListaProductoVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);

                    consulta= $"UPDATE Configuracion SET CorreoEliminarProductoVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);

                    consulta=$"UPDATE Configuracion SET CorreoEliminarUltimoProductoAgregadoVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);

                    consulta= $"UPDATE Configuracion SET CorreoRestarProductoVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
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
                            var consulta = string.Empty;

                            consulta = $"UPDATE Configuracion SET CorreoEliminarListaProductoVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta);

                            consulta = $"UPDATE Configuracion SET CorreoEliminarProductoVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta);

                            consulta = $"UPDATE Configuracion SET CorreoEliminarUltimoProductoAgregadoVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta);

                            consulta = $"UPDATE Configuracion SET CorreoRestarProductoVentas = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta);

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbCorreoEliminarListaProductosVentas.Checked == true)
                            {
                                cbCorreoEliminarListaProductosVentas.Checked = false;
                                return;
                            }
                            else
                            {
                                cbCorreoEliminarListaProductosVentas.Checked = true;
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

        private void cbCorreoVenta_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("HacerVenta", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    var habilitado = 0;

                    if (cbCorreoVenta.Checked)
                    {
                        habilitado = 1;
                    }

                    string consulta=$"UPDATE Configuracion SET CorreoVenta = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            var habilitado = 0;

                            if (cbCorreoVenta.Checked)
                            {
                                habilitado = 1;
                            }

                            var consulta =$"UPDATE Configuracion SET CorreoVenta = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta);


                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbCorreoVenta.Checked == true)
                            {
                                cbCorreoVenta.Checked = false;
                                return;
                            }
                            else
                            {
                                cbCorreoVenta.Checked = true;
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

        private void cbCorreoIniciar_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("IniciarSesion", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    var habilitado = 0;

                    if (cbCorreoIniciar.Checked)
                    {
                        habilitado = 1;
                    }

                    string consulta = $"UPDATE Configuracion SET CorreoIniciarSesion = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);
                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            var habilitado = 0;

                            if (cbCorreoIniciar.Checked)
                            {
                                habilitado = 1;
                            }

                            string consulta = $"UPDATE Configuracion SET CorreoIniciarSesion = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta);

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbCorreoIniciar.Checked == true)
                            {
                                cbCorreoIniciar.Checked = false;
                                return;
                            }
                            else
                            {
                                cbCorreoIniciar.Checked = true;
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

        private void cbCorreoDescuento_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("HacerVentaDescuento", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    var habilitado = 0;

                    if (cbCorreoDescuento.Checked)
                    {
                        habilitado = 1;
                    }
                    string consulta =$"UPDATE Configuracion SET CorreoVentaDescuento = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);

                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            var habilitado = 0;

                            if (cbCorreoDescuento.Checked)
                            {
                                habilitado = 1;
                            }

                            string consulta = $"UPDATE Configuracion SET CorreoVentaDescuento = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta); 

                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbCorreoDescuento.Checked == true)
                            {
                                cbCorreoDescuento.Checked = false;
                                return;
                            }
                            else
                            {
                                cbCorreoDescuento.Checked = true;
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

        private void chRespaldo_MouseClick(object sender, MouseEventArgs e)
        {
            //using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("EviarRespaldoCerrarSesion", FormPrincipal.id_empleado)))
            //{
            //    if (FormPrincipal.id_empleado.Equals(0))
            //    {
            //        //contadorValidarCambioCheckBoxRespaldo += 1;
            //        var habilitado = 0;

            //        if (chRespaldo.Checked)
            //        {
            //            habilitado = 1;
            //            //if (contadorValidarCambioCheckBoxRespaldo > 0 && chRespaldo.Checked)
            //            //{
            //            //    EscogerTipoRespaldo tipoRespaldo = new EscogerTipoRespaldo();

            //            //    tipoRespaldo.ShowDialog();

            //            //    tipoRespaldo.FormClosed += delegate
            //            //    {
            //            //        habilitado = EscogerTipoRespaldo.typeBackUp;
            //            //    };
            //            //}
            //        }
            //        cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoRespaldo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");
            //    }
            //    else if (!permisoEmpleado.Rows.Count.Equals(0))
            //    {
            //        foreach (DataRow item in permisoEmpleado.Rows)
            //        {
            //            if (item[0].ToString().Equals("1"))
            //            {

            //                //contadorValidarCambioCheckBoxRespaldo += 1;
            //                var habilitado = 0;

            //                if (chRespaldo.Checked)
            //                {
            //                    habilitado = 1;
            //                    //if (contadorValidarCambioCheckBoxRespaldo > 0 && chRespaldo.Checked)
            //                    //{
            //                    //    EscogerTipoRespaldo tipoRespaldo = new EscogerTipoRespaldo();

            //                    //    tipoRespaldo.ShowDialog();

            //                    //    tipoRespaldo.FormClosed += delegate
            //                    //    {
            //                    //        habilitado = EscogerTipoRespaldo.typeBackUp;
            //                    //    };
            //                    //}
            //                }
            //                cn.EjecutarConsulta($"UPDATE Configuracion SET CorreoRespaldo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}");

            //            }
            //            else
            //            {
            //                MessageBox.Show("No tienes permisos para modificar esta opcion");
            //                if (chRespaldo.Checked == true)
            //                {
            //                    chRespaldo.Checked = false;
            //                    return;
            //                }
            //                else
            //                {
            //                    chRespaldo.Checked = true;
            //                    return;
            //                }
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

        private void EnvioDeCorreo_KeyDown(object sender, KeyEventArgs e)
        {
            if (confiCorreo.Count==0)
            {
                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                }
            }
            else
            {
                var result = MessageBox.Show("¿Desea guardar los cambios realizados?", "Aviso de Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result.Equals(DialogResult.Yes))
                {
                    btnAceptar.PerformClick();
                    
                }
                else
                {
                    guardado = 1;
                    this.Close();
                }
            }
            
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            foreach (var item in confiCorreo)
            {
                cn.EjecutarConsulta(item);
            }

            MessageBox.Show("Configuracion Guardada con Exito", "Mensaje de sistema", MessageBoxButtons.OK,MessageBoxIcon.Information);
            guardado = 1;

            this.Close();
        }

        private void EnvioDeCorreo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (guardado==0)
            {
                if (confiCorreo.Count == 0)
                {

                }
                else
                {
                    var result = MessageBox.Show("¿Desea guaradar los cambios realizados?", "Aviso de Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result.Equals(DialogResult.Yes))
                    {
                        btnAceptar.PerformClick();

                    }
                    else
                    {

                    }
                }
            }
            
        }

        private void EnvioDeCorreo_FormClosed(object sender, FormClosedEventArgs e)
        {
            //if ()
            //{

            //}
            //if (confiCorreo.Count == 0)
            //{




            //}
            //else
            //{
            //    var result = MessageBox.Show("¿Desea guaradar los cambios realizados?", "Aviso de Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //    if (result.Equals(DialogResult.Yes))
            //    {
            //        btnAceptar.PerformClick();

            //    }
            //    else
            //    {

            //    }
            //}
        }

        private void cbCorreoAgregarDineroCaja_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void cbCorreoRetirarDineroCaja_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void cbRecibirAnricipo_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("PermisoCorreoAnticipo", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    var habilitado = 0;

                    if (cbRecibirAnricipo.Checked)
                    {
                        habilitado = 1;
                    }

                    string consulta = $"UPDATE Configuracion SET CorreoAnticipo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);

                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            var habilitado = 0;

                            if (cbRecibirAnricipo.Checked)
                            {
                                habilitado = 1;
                            }

                            string consulta = $"UPDATE Configuracion SET CorreoAnticipo = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta);
                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (cbRecibirAnricipo.Checked == true)
                            {
                                cbRecibirAnricipo.Checked = false;
                                return;
                            }
                            else
                            {
                                cbRecibirAnricipo.Checked = true;
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

        private void checkBox1_MouseClick(object sender, MouseEventArgs e)
        {
            using (DataTable permisoEmpleado = cn.CargarDatos(cs.permisosEmpleado("VentaClienteDescuento", FormPrincipal.id_empleado)))
            {
                if (FormPrincipal.id_empleado.Equals(0))
                {
                    var habilitado = 0;

                    if (CBXClienteDescuento.Checked)
                    {
                        habilitado = 1;
                    }

                    string consulta = $"UPDATE Configuracion SET CorreoVentaClienteDescuento = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                    confiCorreo.Add(consulta);

                }
                else if (!permisoEmpleado.Rows.Count.Equals(0))
                {
                    foreach (DataRow item in permisoEmpleado.Rows)
                    {
                        if (item[0].ToString().Equals("1"))
                        {

                            var habilitado = 0;

                            if (CBXClienteDescuento.Checked)
                            {
                                habilitado = 1;
                            }

                            string consulta = $"UPDATE Configuracion SET CorreoVentaClienteDescuento = {habilitado} WHERE IDUsuario = {FormPrincipal.userID}";
                            confiCorreo.Add(consulta);
                        }
                        else
                        {
                            MessageBox.Show("No tienes permisos para modificar esta opcion");
                            if (CBXClienteDescuento.Checked == true)
                            {
                                CBXClienteDescuento.Checked = false;
                                return;
                            }
                            else
                            {
                                CBXClienteDescuento.Checked = true;
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

        private void CBXClienteDescuento_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
