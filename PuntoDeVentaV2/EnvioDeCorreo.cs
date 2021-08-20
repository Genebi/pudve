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

        List<string> usuariosPermitidos = new List<string>()
        {
            "HOUSEDEPOTAUTLAN",
            "HOUSEDEPOTGRULLO",
            "HOUSEDEPOTREPARTO"
        };
        public EnvioDeCorreo() 
        { 
            InitializeComponent();
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

        private void cbCorreoCorteCaja_Click(object sender, EventArgs e)
        {
            if (!usuariosPermitidos.Contains(FormPrincipal.userNickName))
            {
                MessageBox.Show("Estamos trabajando en esta opción", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
        }
    }
}
