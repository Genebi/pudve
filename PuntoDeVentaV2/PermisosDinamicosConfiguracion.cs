﻿using System;
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
    public partial class PermisosDinamicosConfiguracion : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        private string[] secciones;
        private string[] datos;
        private string seccion = string.Empty;
        int contador = 0;

        public string tipoPermisos = string.Empty;
        public PermisosDinamicosConfiguracion()
        {
            InitializeComponent();
        }

        private void PermisosDinamicosConfiguracion_Load(object sender, EventArgs e)
        {
            if (tipoPermisos == "editarTicket")
            {
                seccion = "Editar Ticket";
                datos = new string[] { "EditarTicket" };

                var permisosConfiguracion = String.Join(", ", datos);
                var permisos = mb.PermisosEmpleadoConfiguracion(permisosConfiguracion, FormPrincipal.id_empleado);

                GenerarCheckbox(10, 10, 200, "Editar ticket", permisos[0]);
            }
            if (tipoPermisos == "envioCorreo")
            {
                Text = "PUDVE - Permisos Correo";
               
                seccion = "Envio de Correo";
                datos = new string[] {
                    "AgregarDineroCaja",
                    "RetirarDineroCaja",
                    "HacerCorteCaja",
                    "LlegarStockMinimo",
                    "ModificarPrecio",
                    "ModificarStock",
                    "VenderseProducto",
                    "CerrarVentanaVentasCuandoSeTienenProductos",
                    "EliminarProductosVentas",
                    "HacerVenta",
                    "IniciarSesion",
                    "HacerVentaDescuento",
                    "EviarRespaldoCerrarSesion"};

                var permisosConfiguracion = String.Join(", ", datos);
                var permisos = mb.PermisosEmpleadoConfiguracion(permisosConfiguracion, FormPrincipal.id_empleado);

                GenerarCheckbox(10, 10, 200, "Al agregar dinero en caja", permisos[0]);
                GenerarCheckbox(10, 220, 200, "Al retirar dinero en caja", permisos[1]);

                GenerarCheckbox(40, 10, 200, "Al hacer corte de caja", permisos[2]);
                GenerarCheckbox(40, 220, 200, "Al llegar al stock minimo", permisos[3]);

                GenerarCheckbox(70, 10, 200, "Al modificar el precio", permisos[4]);
                GenerarCheckbox(70, 220, 200, "Al modificar el stock", permisos[5]);

                GenerarCheckbox(100, 10, 200, "Al venderse producto", permisos[6]);
                GenerarCheckbox(100, 220, 200, "Al cerrar la ventana de ventas\ncuando tienes productos", permisos[7]);

                GenerarCheckbox(130, 10, 200, "Al eliminar producto de ventas", permisos[8]);
                GenerarCheckbox(130, 220, 200, "Al hacer una venta", permisos[9]);

                GenerarCheckbox(160, 10, 200, "Al iniciar sesion", permisos[10]);
                GenerarCheckbox(160, 220, 200, "Al hacer una venta con\ndescuento", permisos[11]);

                GenerarCheckbox(190, 10, 200, "Enviar respaldo al cerrar sesion", permisos[12]);
            }
            if (tipoPermisos == "configuracionGeneral")
            {
                Text = "PUDVE - Permisos Configuracion";

                seccion = "Configuracion General";
                datos = new string[] 
                {
                    "CodigoBarrasTicketVenta",
                    "HabilitarInfoPaginaWeb",
                    "MostrarCodigoProductoVentas",
                    "CerrarSesionCorteCaja",
                    "MostrarPrecioProductoVentas",
                    "PermitirStockNegativo",
                    "GenerarTicketAlRealizarVenta",
                    "AvisarProductosNoVendidos",
                    "ActivarPrecioMayoreoVentas"
                };

                var permisosConfiguracion = String.Join(", ",datos);
                var permisos = mb.PermisosEmpleadoConfiguracion(permisosConfiguracion,FormPrincipal.id_empleado);



                GenerarCheckbox(10, 10, 200, "Codigo de barras ticet\nventa", permisos[0]);
                GenerarCheckbox(10, 220, 200, "Habilitar informacion en\npagina web", permisos[1]);

                GenerarCheckbox(40, 10, 200, "Mostrar codigo productos\nen ventas", permisos[2]);
                GenerarCheckbox(40, 220, 200, "Cerrar sesion al hacer corte\nde caja", permisos[3]);

                GenerarCheckbox(70, 10, 200, "Mostrar precio de producto\nen ventas", permisos[4]);
                GenerarCheckbox(70, 220, 200, "Permitir stock negativo", permisos[5]);

                GenerarCheckbox(100, 10, 200, "Generar ticket al realizar venta", permisos[6]);
                GenerarCheckbox(100, 220, 200, "Avisar de productos no vendidos", permisos[7]);

                GenerarCheckbox(130, 10, 200, "Activar precio por mayoreo en ventas", permisos[8]);
            }
            if (tipoPermisos == "porcentageGanancia")
            {
                Text = "PUDVE - Porcentaje de Ganancia";

                seccion = "Porcentaje de Ganancia";
                datos = new string[] { "PorcentajeGanancia" };

                var permisosConfiguracion = String.Join(", ", datos);
                var permisos = mb.PermisosEmpleadoConfiguracion(permisosConfiguracion, FormPrincipal.id_empleado);

                GenerarCheckbox(10, 10, 200, "Porcentaje de Ganancia", permisos[0]);
            }
            if (tipoPermisos == "tipoMoneda")
            {
                Text = "PUDVE - Tipo de Moneda";

                seccion = "Tipo de Moneda";
                datos = new string[] { "TipoDeMoneda" };

                var permisosConfiguracion = String.Join(", ", datos);
                var permisos = mb.PermisosEmpleadoConfiguracion(permisosConfiguracion, FormPrincipal.id_empleado);

                GenerarCheckbox(10, 10, 200, "Tipo de moneda $", permisos[0]);
            }
            if (tipoPermisos == "respaldarInformacion")
            {
                Text = "PUDVE - Respaldar Informacion";

                seccion = "Respaldo de Informacion";
                datos = new string[] {"RespaldarInformacion"};

                var permisosConfiguracion = String.Join(", ", datos);
                var permisos = mb.PermisosEmpleadoConfiguracion(permisosConfiguracion, FormPrincipal.id_empleado);

                GenerarCheckbox(10, 10, 200, "Respaldar Informacion", permisos[0]);
            }

            secciones = new string[] {
                "Editar Tickt", "Envio de Correo", "Configuracion General", "Porcentaje de Ganancia",
                "Tipo de Moneda", "Respaldo de Informacion"
            };
        }

        private void GenerarCheckbox(int top, int left, int ancho, string texto, int estado)
        {
            var checkbox = new CheckBox();
            checkbox.Text = texto;
            checkbox.Top = top;
            checkbox.Left = left;
            checkbox.Width = ancho;
            checkbox.Height = 30;
            checkbox.AutoSize = false;
            checkbox.Checked = Convert.ToBoolean(estado);
            checkbox.CheckedChanged += new EventHandler(checkbox_CheckedChanged);
            panelContenedor.Controls.Add(checkbox);
        }
       
        private void checkbox_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            foreach (Control objetos in panelContenedor.Controls)
            {
                if (objetos is CheckBox)
                {
                    CheckBox chkObjetos = (CheckBox)objetos;
                    if ("Marcar todos" != chkObjetos.Text)
                    {
                        bool estado = chkObjetos.Checked;

                        if (checkbox.Checked == false && checkbox.Text == "Desmarcar todos")
                        {
                            chkObjetos.Checked = false;
                        }
                        else if (checkbox.Checked == true && checkbox.Text == "Marcar todos")
                        {
                            chkObjetos.Checked = true;
                        }
                    }
                }
            }
            if (checkbox.Checked == true && checkbox.Text == "Marcar todos")
            {
                checkbox.Text = "Desmarcar todos";
            }
            else if (checkbox.Checked == false && checkbox.Text == "Desmarcar todos")
            {
                checkbox.Text = "Marcar todos";
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            foreach (var apartado in secciones)
            {
                if (seccion.Equals(apartado))
                {
                    var Permisos = PermisosElegidos();

                    foreach (var opcion in Permisos)
                    {
                        string nombre = datos[contador].ToString();
                        cn.EjecutarConsulta($"UPDATE permisosconfiguracion SET {datos[contador]} = {opcion} WHERE IDEmpleado = {FormPrincipal.id_empleado} AND IDUsuario = {FormPrincipal.userID}");

                        contador++;
                    }
                }
            }
            Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private int[] PermisosElegidos()
        {
            List<int> opciones = new List<int>();
             
            foreach (Control item in panelContenedor.Controls)
            {
                if (item is CheckBox && !item.Text.Equals("Marcar todos"))
                {
                    if (item is CheckBox && !item.Text.Equals("Desmarcar todos"))
                    {
                        var cb = (CheckBox)item;

                        var seleccionado = 0;

                        if (cb.Checked)
                        {
                            seleccionado = 1;
                        }
                        opciones.Add(seleccionado);
                    }
                    else
                    {

                    }
                }
            }
            return opciones.ToArray();
        }
    }
}