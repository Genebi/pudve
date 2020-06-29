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
    public partial class SeccionPermisos : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        private string seccion = string.Empty;
        private int id_empleado = 0;
        private string[] secciones;

        public SeccionPermisos(string seccion, int id_empleado)
        {
            InitializeComponent();

            this.seccion = seccion;
            this.id_empleado = id_empleado;
        }

        private void SeccionPermisos_Load(object sender, EventArgs e)
        {
            secciones = new string[] {
                "Caja", "Ventas", "Inventario", "Anticipos",
                "MisDatos", "Facturas", "Configuracion"
            };

            VerificarSecciones();

            if (seccion.Equals("Caja"))
                GenerarCaja();

            if (seccion.Equals("Ventas"))
                GenerarVentas();

            if (seccion.Equals("Inventario"))
                GenerarInventario();

            if (seccion.Equals("Anticipos"))
                GenerarAnticipos();

            if (seccion.Equals("MisDatos"))
                GenerarMisDatos();

            if (seccion.Equals("Facturas"))
                GenerarFacturas();

            if (seccion.Equals("Configuracion"))
                GenerarConfiguracion();
        }

        private void GenerarConfiguracion()
        {
            Text = "PUDVE - Permisos Configuración";

            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Configuracion");

            GenerarCheckbox(20, 20, 150, "Guardar Servidor", datos[0]);
            GenerarCheckbox(20, 180, 150, "Numero de Revision", datos[1]);
            GenerarCheckbox(50, 20, 150, "Porcentaje Ganancia", datos[2]);
            GenerarCheckbox(50, 180, 150, "Respaldar Informacion", datos[3]);
            GenerarCheckbox(80, 20, 150, "Correo Modificar Precio", datos[4]);
            GenerarCheckbox(80, 180, 150, "Correo Modificar Stock", datos[5]);
            GenerarCheckbox(110, 20, 150, "Correo Stock Minimo", datos[6]);
            GenerarCheckbox(110, 180, 150, "Correo Vender Producto", datos[7]);
            GenerarCheckbox(140, 20, 150, "Permitir Stock Negativo", datos[8]);
            GenerarCheckbox(140, 180, 150, "Codigo Barras Ticket", datos[9]);
            GenerarCheckbox(170, 20, 150, "Informacion Pagina Web", datos[10]);
            GenerarCheckbox(170, 180, 150, "Precio Productos Ventas", datos[11]);
            GenerarCheckbox(200, 20, 150, "Codigo Producto Venta", datos[12]);
            GenerarCheckbox(200, 180, 150, "Precio Mayoreo Ventas", datos[13]);
            GenerarCheckbox(230, 20, 150, "Producto No Vendido", datos[14]);
        }

        private void GenerarCaja()
        {
            Text = "PUDVE - Permisos Caja";

            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Caja");

            GenerarCheckbox(40, 20, 150, "Botón Agregar Dinero", datos[0]);
            GenerarCheckbox(40, 180, 200, "Botón Historial Dinero Agregado", datos[1]);
            GenerarCheckbox(80, 20, 150, "Botón Retirar Dinero", datos[2]);
            GenerarCheckbox(80, 180, 200, "Botón Historial Dinero Retirado", datos[3]);
            GenerarCheckbox(120, 20, 150, "Botón Abrir Caja", datos[4]);
            GenerarCheckbox(120, 180, 200, "Botón Corte Caja", datos[5]);
            GenerarCheckbox(160, 20, 150, "Mostrar Saldo Inicial", datos[6]);
            GenerarCheckbox(160, 180, 200, "Mostrar Panel Ventas", datos[7]);
            GenerarCheckbox(200, 20, 150, "Mostrar Panel Anticipos", datos[8]);
            GenerarCheckbox(200, 180, 200, "Mostrar Panel Dinero Agregado", datos[9]);
            GenerarCheckbox(240, 20, 150, "Mostrar Panel Total Caja", datos[10]);
        }

        private void GenerarVentas()
        {
            Text = "PUDVE - Permisos Ventas";

            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Ventas");

            GenerarCheckbox(20, 10, 110, "Cancelar Venta", datos[0]);
            GenerarCheckbox(20, 130, 110, "Ver Nota Venta", datos[1]);
            GenerarCheckbox(20, 250, 125, "Ver Ticket Venta", datos[2]);
            //=============================================================
            GenerarCheckbox(50, 10, 110, "Ver Info Venta", datos[3]);
            GenerarCheckbox(50, 130, 110, "Timbrar Factura", datos[4]);
            GenerarCheckbox(50, 250, 125, "Botón Enviar Nota", datos[5]);
            //=============================================================
            GenerarCheckbox(80, 10, 110, "Buscar Venta", datos[6]);
            GenerarCheckbox(80, 130, 110, "Nueva Venta", datos[7]);
            GenerarCheckbox(80, 250, 125, "Botón Cancelar", datos[8]);
            //=============================================================
            GenerarCheckbox(110, 10, 110, "Guardar Venta", datos[9]);
            GenerarCheckbox(110, 130, 110, "Botón Anticipos", datos[10]);
            GenerarCheckbox(110, 250, 125, "Abrir Caja", datos[11]);
            //=============================================================
            GenerarCheckbox(140, 10, 115, "Ventas Guardadas", datos[12]);
            GenerarCheckbox(140, 130, 110, "Ver Último Ticket", datos[13]);
            GenerarCheckbox(140, 250, 135, "Guardar Presupuesto", datos[14]);
            //=============================================================
            GenerarCheckbox(170, 10, 115, "Descuento Cliente", datos[15]);
            GenerarCheckbox(170, 130, 110, "Elimininar Último", datos[16]);
            GenerarCheckbox(170, 250, 135, "Eliminar Todos", datos[17]);
            //=============================================================
            GenerarCheckbox(200, 10, 115, "Aplicar Descuento", datos[18]);
            GenerarCheckbox(200, 130, 110, "Terminar Venta", datos[19]);
        }

        private void GenerarInventario()
        {
            Text = "PUDVE - Permisos Inventario";

            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Inventario");

            GenerarCheckbox(40, 130, 120, "Revisar Inventario", datos[0]);
            GenerarCheckbox(80, 130, 125, "Actualizar Inventario", datos[1]);
            GenerarCheckbox(120, 130, 150, "Actualizar Inventario XML", datos[2]);
            GenerarCheckbox(160, 130, 100, "Botón Buscar", datos[3]);
            GenerarCheckbox(200, 130, 120, "Botón Terminar", datos[4]);
        }

        private void GenerarAnticipos()
        {
            Text = "PUDVE - Permisos Anticipos";

            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Anticipos");

            GenerarCheckbox(40, 130, 120, "Generar Ticket", datos[0]);
            GenerarCheckbox(80, 130, 125, "Habilitar/Deshabilitar", datos[1]);
            GenerarCheckbox(120, 130, 150, "Devolver Anticipo", datos[2]);
            GenerarCheckbox(160, 130, 100, "Botón Buscar", datos[3]);
            GenerarCheckbox(200, 130, 120, "Nuevo Anticipo", datos[4]);
        }

        private void GenerarMisDatos()
        {
            Text = "PUDVE - Permisos Mis Datos";

            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "MisDatos");

            GenerarCheckbox(40, 130, 150, "Guardar Datos", datos[0]);
            GenerarCheckbox(80, 130, 150, "Subir Imagen", datos[1]);
            GenerarCheckbox(120, 130, 150, "Eliminar Imagen", datos[2]);
            GenerarCheckbox(160, 130, 150, "Actualizar Contraseña", datos[3]);
            GenerarCheckbox(200, 130, 150, "Actualizar Archivos", datos[4]);
        }

        private void GenerarFacturas()
        {
            Text = "PUDVE - Permisos Facturas";

            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Facturas");

            GenerarCheckbox(40, 10, 100, "Ver Factura", datos[0]);
            GenerarCheckbox(40, 120, 120, "Descargar Factura", datos[1]);
            GenerarCheckbox(40, 240, 130, "Cancelar Factura", datos[2]);
            //=============================================================
            GenerarCheckbox(80, 10, 100, "Ver Pagos", datos[3]);
            GenerarCheckbox(80, 120, 120, "Buscar Factura", datos[4]);
            GenerarCheckbox(80, 240, 130, "Enviar Factura", datos[5]);
            //=============================================================
            GenerarCheckbox(120, 10, 150, "Generar Complemento", datos[6]);
        }

        private void GenerarCheckbox(int top, int left, int ancho, string texto, int estado)
        {
            var checkbox = new CheckBox();
            checkbox.Text = texto;
            checkbox.Top = top;
            checkbox.Left = left;
            checkbox.Width = ancho;
            checkbox.Checked = Convert.ToBoolean(estado);

            panelContenedor.Controls.Add(checkbox);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void VerificarSecciones()
        {
            var existe = false;

            foreach (var apartado in secciones)
            {
                existe = (bool)cn.EjecutarSelect($"SELECT * FROM EmpleadosPermisos WHERE IDEmpleado = {id_empleado} AND IDUsuario = {FormPrincipal.userID} AND Seccion = '{apartado}'");

                if (!existe)
                {
                    cn.EjecutarConsulta($"INSERT INTO EmpleadosPermisos (IDEmpleado, IDUsuario, Seccion) VALUES ('{id_empleado}', '{FormPrincipal.userID}', '{apartado}')");
                }
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            foreach (var apartado in secciones)
            {
                if (seccion.Equals(apartado))
                {
                    var datos = PermisosElegidos();
                    var numero = 1;

                    foreach (var opcion in datos)
                    {
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion{numero} = {opcion} WHERE IDEmpleado = {id_empleado} AND IDUsuario = {FormPrincipal.userID} AND Seccion = '{apartado}'");

                        numero++;
                    }
                }
            }

            Close();
        }

        private int[] PermisosElegidos()
        {
            List<int> opciones = new List<int>();

            foreach (Control item in panelContenedor.Controls)
            {
                if (item is CheckBox)
                {
                    var cb = (CheckBox)item;

                    var seleccionado = 0;

                    if (cb.Checked)
                    {
                        seleccionado = 1;
                    }

                    opciones.Add(seleccionado);
                }
            }

            return opciones.ToArray();
        }
    }
}
