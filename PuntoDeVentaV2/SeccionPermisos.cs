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
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        private string seccion = string.Empty;
        private int id_empleado = 0;
        private string[] secciones;
        private string[] datosUpdate;
        int contador = 0;

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
                "MisDatos", "Facturas", "Configuracion", "Reportes",
                "Clientes", "Proveedores", "Empleados", "Productos", "Bascula" ,"Permisos"
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

            if (seccion.Equals("Reportes"))
                GenerarReportes();

            if (seccion.Equals("Clientes"))
                GenerarClientes();

            if (seccion.Equals("Proveedores"))
                GenerarProveedores();

            if (seccion.Equals("Empleados"))
                GenerarEmpleados();
            
            if (seccion.Equals("Productos"))
                GenerarProductos();

            if (seccion.Equals("Bascula"))
                GenerarBascula();
        }

        private void GenerarBascula()
        {
            Text = "PUDVE - Permisos Bascula";
           
            if (Agregar_empleado_permisos.IDPlantilla.Equals(0))
            {
                var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Bascula");
                GenerarCheckbox(10, 35, 150, "Marcar todos", 0);
                GenerarCheckbox(30, 35, 250, "Agregar/Editar Predeterminada", datos[0]);
            }
            else
            {
                var DTColumnas = cn.CargarDatos($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'subpermisos' AND COLUMN_NAME LIKE 'BAS_%'");
                var rows = DTColumnas.AsEnumerable()
                             .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var columnas = string.Format("{0}", string.Join(",", rows.ToArray()));

                var DTPermisos = cn.CargarDatos($"SELECT {columnas} FROM subpermisos WHERE IDPlantilla = {Agregar_empleado_permisos.IDPlantilla}");

                var rows2 = DTPermisos.AsEnumerable()
                            .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var permisos = string.Format("{0}", string.Join(",", rows2.ToArray()));

                var datos = permisos.Split(',');
                GenerarCheckbox(10, 35, 150, "Marcar todos", 0);
                GenerarCheckbox(30, 35, 250, "Agregar/Editar Predeterminada",Convert.ToInt32(datos[0]));

            }


        }

        private void GenerarProductos()
        {
            Text = "PUDVE - Permisos Productos";
            if (Agregar_empleado_permisos.IDPlantilla.Equals(0))
            {
                var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Productos");

                GenerarCheckbox(10, 35, 150, "Marcar todos", 0);
                //============================================================
                GenerarCheckbox(30, 35, 100, "Agregar XML", datos[0]);
                GenerarCheckbox(30, 200, 200, "Deshabilitar Seleccionados", datos[1]);
                //=============================================================
                GenerarCheckbox(50, 35, 150, "Cambiar Tipo", datos[2]);
                GenerarCheckbox(50, 200, 150, "Mostrar en Lista", datos[3]);
                //=============================================================
                GenerarCheckbox(70, 35, 150, "Botón Asignar", datos[4]);
                Generarbutton(70, 10, 20, 20, "permisosProductos");
                GenerarCheckbox(70, 200, 150, "Mostrar en Mosaico", datos[5]);
                //=============================================================
                GenerarCheckbox(90, 35, 150, "Botón Etiqueta", datos[6]);
                GenerarCheckbox(90, 200, 150, "Botón Reporte", datos[7]);
                //=============================================================
                GenerarCheckbox(110, 35, 150, "Botón Imprimir", datos[8]);
                GenerarCheckbox(110, 200, 150, "Agregar Producto", datos[9]);
                //=============================================================
                GenerarCheckbox(130, 35, 150, "Agregar Combo", datos[10]);
                GenerarCheckbox(130, 200, 150, "Agregar Servicio", datos[11]);
                //=============================================================
                GenerarCheckbox(150, 35, 150, "Botón Filtro", datos[12]);
                GenerarCheckbox(150, 200, 150, "Botón Borrar Filtro", datos[13]);
                //=============================================================
                GenerarCheckbox(170, 35, 150, "Opción Editar", datos[14]);
                GenerarCheckbox(170, 200, 150, "Opción Estado", datos[15]);
                //=============================================================
                GenerarCheckbox(190, 35, 150, "Opción Historal", datos[16]);
                GenerarCheckbox(190, 200, 150, "Generar Código Barras", datos[17]);
                //=============================================================
                GenerarCheckbox(210, 35, 150, "Cargar Imagen", datos[18]);
                GenerarCheckbox(210, 200, 150, "Opción Etiqueta", datos[19]);
                //=============================================================
                GenerarCheckbox(230, 35, 150, "Opción Copiar", datos[20]);
                GenerarCheckbox(230, 200, 150, "Opción Ajustar", datos[21]);
            }
            else
            {
                var DTColumnas = cn.CargarDatos($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'subpermisos' AND COLUMN_NAME LIKE 'PROD_%'");
                var rows = DTColumnas.AsEnumerable()
                             .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var columnas = string.Format("{0}", string.Join(",", rows.ToArray()));

                var DTPermisos = cn.CargarDatos($"SELECT {columnas} FROM subpermisos WHERE IDPlantilla = {Agregar_empleado_permisos.IDPlantilla}");

                var rows2 = DTPermisos.AsEnumerable()
                            .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var permisos = string.Format("{0}", string.Join(",", rows2.ToArray()));

                var datos = permisos.Split(',');


                GenerarCheckbox(10, 35, 150, "Marcar todos", 0);
                //============================================================
                GenerarCheckbox(30, 35, 100, "Agregar XML",Convert.ToInt32(datos[0]));
                GenerarCheckbox(30, 200, 200, "Deshabilitar Seleccionados", Convert.ToInt32(datos[11]));
                //=============================================================
                GenerarCheckbox(50, 35, 150, "Cambiar Tipo", Convert.ToInt32(datos[1]));
                GenerarCheckbox(50, 200, 150, "Mostrar en Lista", Convert.ToInt32(datos[12]));
                //=============================================================
                GenerarCheckbox(70, 35, 150, "Botón Asignar", Convert.ToInt32(datos[2]));
                Generarbutton(70, 10, 20, 20, "permisosProductos");
                GenerarCheckbox(70, 200, 150, "Mostrar en Mosaico", Convert.ToInt32(datos[13]));
                //=============================================================
                GenerarCheckbox(90, 35, 150, "Botón Etiqueta", Convert.ToInt32(datos[3]));
                GenerarCheckbox(90, 200, 150, "Botón Reporte", Convert.ToInt32(datos[14]));
                //=============================================================
                GenerarCheckbox(110, 35, 150, "Botón Imprimir", Convert.ToInt32(datos[4]));
                GenerarCheckbox(110, 200, 150, "Agregar Producto", Convert.ToInt32(datos[15]));
                //=============================================================
                GenerarCheckbox(130, 35, 150, "Agregar Combo", Convert.ToInt32(datos[5]));
                GenerarCheckbox(130, 200, 150, "Agregar Servicio", Convert.ToInt32(datos[16]));
                //=============================================================
                GenerarCheckbox(150, 35, 150, "Botón Filtro", Convert.ToInt32(datos[6]));
                GenerarCheckbox(150, 200, 150, "Botón Borrar Filtro", Convert.ToInt32(datos[17]));
                //=============================================================
                GenerarCheckbox(170, 35, 150, "Opción Editar", Convert.ToInt32(datos[7]));
                GenerarCheckbox(170, 200, 150, "Opción Estado", Convert.ToInt32(datos[18]));
                //=============================================================
                GenerarCheckbox(190, 35, 150, "Opción Historal", Convert.ToInt32(datos[8]));
                GenerarCheckbox(190, 200, 150, "Generar Código Barras", Convert.ToInt32(datos[19]));
                //=============================================================
                GenerarCheckbox(210, 35, 150, "Cargar Imagen", Convert.ToInt32(datos[9]));
                GenerarCheckbox(210, 200, 150, "Opción Etiqueta", Convert.ToInt32(datos[20]));
                //=============================================================
                GenerarCheckbox(230, 35, 150, "Opción Copiar", Convert.ToInt32(datos[10]));
                GenerarCheckbox(230, 200, 150, "Opción Ajustar", Convert.ToInt32(datos[21]));
            }

        }

        private void GenerarEmpleados()
        {
            Text = "PUDVE - Permisos Empleados";

            if (Agregar_empleado_permisos.IDPlantilla.Equals(0))
            {
                var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Empleados");
                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(50, 130, 150, "Nuevo Empleado", datos[0]);
                GenerarCheckbox(90, 130, 150, "Editar Empleado", datos[1]);
                GenerarCheckbox(130, 130, 150, "Permisos Empleado", datos[2]);
            }
            else
            {
                var DTColumnas = cn.CargarDatos($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'subpermisos' AND COLUMN_NAME LIKE 'EMP_%'");
                var rows = DTColumnas.AsEnumerable()
                             .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var columnas = string.Format("{0}", string.Join(",", rows.ToArray()));

                var DTPermisos = cn.CargarDatos($"SELECT {columnas} FROM subpermisos WHERE IDPlantilla = {Agregar_empleado_permisos.IDPlantilla}");

                var rows2 = DTPermisos.AsEnumerable()
                            .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var permisos = string.Format("{0}", string.Join(",", rows2.ToArray()));

                var datos = permisos.Split(',');

                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(50, 130, 150, "Nuevo Empleado", Convert.ToInt32(datos[0]));
                GenerarCheckbox(90, 130, 150, "Editar Empleado", Convert.ToInt32(datos[1]));
                GenerarCheckbox(130, 130, 150, "Permisos Empleado", Convert.ToInt32(datos[2]));
            }
        }

        private void GenerarProveedores()
        {
            Text = "PUDVE - Permisos Proveedores";
            if (Agregar_empleado_permisos.IDPlantilla.Equals(0))
            {
                var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Proveedores");
                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(50, 130, 150, "Botón Buscar", datos[0]);
                GenerarCheckbox(90, 130, 150, "Nuevo Proveedor", datos[1]);
                GenerarCheckbox(130, 130, 150, "Deshabilitar", datos[2]);
                GenerarCheckbox(170, 130, 150, "Habilitar", datos[3]);
            }
            else
            {
                var DTColumnas = cn.CargarDatos($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'subpermisos' AND COLUMN_NAME LIKE 'PROV_%'");
                var rows = DTColumnas.AsEnumerable()
                             .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var columnas = string.Format("{0}", string.Join(",", rows.ToArray()));

                var DTPermisos = cn.CargarDatos($"SELECT {columnas} FROM subpermisos WHERE IDPlantilla = {Agregar_empleado_permisos.IDPlantilla}");

                var rows2 = DTPermisos.AsEnumerable()
                            .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var permisos = string.Format("{0}", string.Join(",", rows2.ToArray()));

                var datos = permisos.Split(',');

                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(50, 130, 150, "Botón Buscar", Convert.ToInt32(datos[0]));
                GenerarCheckbox(90, 130, 150, "Nuevo Proveedor", Convert.ToInt32(datos[1]));
                GenerarCheckbox(130, 130, 150, "Deshabilitar", Convert.ToInt32(datos[2]));
                GenerarCheckbox(170, 130, 150, "Habilitar", Convert.ToInt32(datos[3]));
            }
            

        }

        private void GenerarClientes()
        {
            Text = "PUDVE - Permisos Clientes";
            if (Agregar_empleado_permisos.IDPlantilla.Equals(0))
            {
                var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Clientes");
                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(40, 130, 150, "Botón Buscar", datos[0]);
                GenerarCheckbox(80, 130, 150, "Nuevo Tipo Cliente", datos[1]);
                GenerarCheckbox(120, 130, 150, "Listado Tipo Cliente", datos[2]);
                GenerarCheckbox(160, 130, 150, "Nuevo Cliente", datos[3]);
                GenerarCheckbox(200, 130, 150, "Deshabilitar", datos[4]);
                GenerarCheckbox(235, 130, 150, "Habilitar", datos[5]);
            }
            else
            {

                var DTColumnas = cn.CargarDatos($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'subpermisos' AND COLUMN_NAME LIKE 'CLI_%'");
                var rows = DTColumnas.AsEnumerable()
                             .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var columnas = string.Format("{0}", string.Join(",", rows.ToArray()));

                var DTPermisos = cn.CargarDatos($"SELECT {columnas} FROM subpermisos WHERE IDPlantilla = {Agregar_empleado_permisos.IDPlantilla}");

                var rows2 = DTPermisos.AsEnumerable()
                            .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var permisos = string.Format("{0}", string.Join(",", rows2.ToArray()));

                var datos = permisos.Split(',');

                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(40, 130, 150, "Botón Buscar", Convert.ToInt32(datos[0]));
                GenerarCheckbox(80, 130, 150, "Nuevo Tipo Cliente", Convert.ToInt32(datos[1]));
                GenerarCheckbox(120, 130, 150, "Listado Tipo Cliente", Convert.ToInt32(datos[2]));
                GenerarCheckbox(160, 130, 150, "Nuevo Cliente", Convert.ToInt32(datos[3]));
                GenerarCheckbox(200, 130, 150, "Deshabilitar", Convert.ToInt32(datos[4]));
                GenerarCheckbox(235, 130, 150, "Habilitar", Convert.ToInt32(datos[5]));
            }
        }
            

        private void GenerarReportes()
        {
            Text = "PUDVE - Permisos Reportes";
            if (Agregar_empleado_permisos.IDPlantilla.Equals(0))
            {
                var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Reportes");
                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(90, 23, 150, "Historial de Precios", datos[0]);
                GenerarCheckbox(130, 23, 150, "Caja", datos[1]);
                GenerarCheckbox(170, 23, 150, "Reportes de Inventario", datos[2]);
                GenerarCheckbox(90, 215, 150, "Reportes de Ventas", datos[3]);
                GenerarCheckbox(130, 215, 150, "Reportes de Clientes", datos[4]);
                GenerarCheckbox(170, 215, 150, "Reportes de Mas/Menos Vnedido", datos[5]);
                GenerarCheckbox(210, 215, 150, "Reporte de Adeudos", datos[48]);
            }
            else
            {
                var DTColumnas = cn.CargarDatos($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'subpermisos' AND COLUMN_NAME LIKE 'REP_%'");
                var rows = DTColumnas.AsEnumerable()
                             .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var columnas = string.Format("{0}", string.Join(",", rows.ToArray()));

                var DTPermisos = cn.CargarDatos($"SELECT {columnas} FROM subpermisos WHERE IDPlantilla = {Agregar_empleado_permisos.IDPlantilla}");

                var rows2 = DTPermisos.AsEnumerable()
                            .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var permisos = string.Format("{0}", string.Join(",", rows2.ToArray()));

                var datos = permisos.Split(',');

                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(90, 23, 150, "Historial de Precios",Convert.ToInt32(datos[0]));
                GenerarCheckbox(130, 23, 150, "Caja", Convert.ToInt32(datos[1]));
                GenerarCheckbox(170, 23, 150, "Reportes de Inventario", Convert.ToInt32(datos[2]));
                GenerarCheckbox(90, 215, 150, "Reportes de Ventas", Convert.ToInt32(datos[3]));
                GenerarCheckbox(130, 215, 150, "Reportes de Clientes", Convert.ToInt32(datos[4]));
                GenerarCheckbox(170, 215, 150, "Reportes de Mas/Menos Vnedido", Convert.ToInt32(datos[5]));
            }
           
        }

        private void GenerarConfiguracion()
        {
            Text = "PUDVE - Permisos Configuración";
            if (Agregar_empleado_permisos.IDPlantilla.Equals(0))
            {
                var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Configuracion");

                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);

                ////GenerarCheckbox(30, 20, 150, "Guardar Servidor", datos[0]);
                ////GenerarCheckbox(30, 180, 150, "Número de Revisión", datos[1]);
                //////=============================================================
                ////GenerarCheckbox(50, 20, 150, "Porcentaje Ganancia", datos[2]);
                ////GenerarCheckbox(50, 180, 150, "Respaldar Información", datos[3]);
                //////=============================================================
                ////GenerarCheckbox(80, 20, 150, "Correo Modificar Precio", datos[4]);
                ////GenerarCheckbox(80, 180, 150, "Correo Modificar Stock", datos[5]);
                //////=============================================================
                ////GenerarCheckbox(110, 20, 150, "Correo Stock Mínimo", datos[6]);
                ////GenerarCheckbox(110, 180, 150, "Correo Vender Producto", datos[7]);
                //////=============================================================
                ////GenerarCheckbox(140, 20, 150, "Permitir Stock Negativo", datos[8]);
                ////GenerarCheckbox(140, 180, 150, "Código Barras Ticket", datos[9]);
                //////=============================================================
                ////GenerarCheckbox(170, 20, 150, "Información Página Web", datos[10]);
                ////GenerarCheckbox(170, 180, 150, "Precio Productos Ventas", datos[11]);
                //////=============================================================
                ////GenerarCheckbox(200, 20, 150, "Código Producto Venta", datos[12]);
                ////GenerarCheckbox(200, 180, 150, "Precio Mayoreo Ventas", datos[13]);
                //////=============================================================
                ////GenerarCheckbox(230, 20, 150, "Producto No Vendido", datos[14]);

                GenerarCheckbox(70, 35, 150, "Editar Ticket", datos[35]);
                //=============================================================
                Generarbutton(40, 185, 20, 20, "envioCorreo");
                GenerarCheckbox(40, 210, 150, "Envio de Correo", datos[36]);
                //=============================================================
                Generarbutton(40, 10, 20, 20, "configuracionGeneral");
                GenerarCheckbox(40, 35, 150, "Configuracion General", datos[37]);
                //=============================================================
                GenerarCheckbox(70, 210, 150, "Porcentaje de Ganancia", datos[38]);
                //=============================================================
                GenerarCheckbox(100, 35, 150, "Tipo de Moneda", datos[39]);
                //=============================================================
                GenerarCheckbox(100, 210, 150, "Respaldar Informacion", datos[40]);


            }
            else
            {
                var DTColumnas = cn.CargarDatos($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'subpermisos' AND COLUMN_NAME LIKE 'CONF_%'");
                var rows = DTColumnas.AsEnumerable()
                             .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var columnas = string.Format("{0}", string.Join(",", rows.ToArray()));

                var DTPermisos = cn.CargarDatos($"SELECT {columnas} FROM subpermisos WHERE IDPlantilla = {Agregar_empleado_permisos.IDPlantilla}");

                var rows2 = DTPermisos.AsEnumerable()
                            .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var permisos = string.Format("{0}", string.Join(",", rows2.ToArray()));

                var datos = permisos.Split(',');

                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);

                ////GenerarCheckbox(30, 20, 150, "Guardar Servidor", datos[0]);
                ////GenerarCheckbox(30, 180, 150, "Número de Revisión", datos[1]);
                //////=============================================================
                ////GenerarCheckbox(50, 20, 150, "Porcentaje Ganancia", datos[2]);
                ////GenerarCheckbox(50, 180, 150, "Respaldar Información", datos[3]);
                //////=============================================================
                ////GenerarCheckbox(80, 20, 150, "Correo Modificar Precio", datos[4]);
                ////GenerarCheckbox(80, 180, 150, "Correo Modificar Stock", datos[5]);
                //////=============================================================
                ////GenerarCheckbox(110, 20, 150, "Correo Stock Mínimo", datos[6]);
                ////GenerarCheckbox(110, 180, 150, "Correo Vender Producto", datos[7]);
                //////=============================================================
                ////GenerarCheckbox(140, 20, 150, "Permitir Stock Negativo", datos[8]);
                ////GenerarCheckbox(140, 180, 150, "Código Barras Ticket", datos[9]);
                //////=============================================================
                ////GenerarCheckbox(170, 20, 150, "Información Página Web", datos[10]);
                ////GenerarCheckbox(170, 180, 150, "Precio Productos Ventas", datos[11]);
                //////=============================================================
                ////GenerarCheckbox(200, 20, 150, "Código Producto Venta", datos[12]);
                ////GenerarCheckbox(200, 180, 150, "Precio Mayoreo Ventas", datos[13]);
                //////=============================================================
                ////GenerarCheckbox(230, 20, 150, "Producto No Vendido", datos[14]);

                GenerarCheckbox(70, 35, 150, "Editar Ticket", Convert.ToInt32(datos[2]));
                //=============================================================
                Generarbutton(40, 185, 20, 20, "envioCorreo");
                GenerarCheckbox(40, 210, 150, "Envio de Correo", Convert.ToInt32(datos[1]));
                //=============================================================
                Generarbutton(40, 10, 20, 20, "configuracionGeneral");
                GenerarCheckbox(40, 35, 150, "Configuracion General", Convert.ToInt32(datos[0]));
                //=============================================================
                GenerarCheckbox(70, 210, 150, "Porcentaje de Ganancia", Convert.ToInt32(datos[3]));
                //=============================================================
                GenerarCheckbox(100, 35, 150, "Tipo de Moneda", Convert.ToInt32(datos[4]));
                //=============================================================
                GenerarCheckbox(100, 210, 150, "Respaldar Informacion", Convert.ToInt32(datos[5]));
            }
           


            seccion = "Configuracion";
            datosUpdate = new string[] { "editarTicket","EnvioCorreo","confiGeneral","porcentajeGanancia","tipoMoneda", "RespaldarInfo","MensajeVentas","MensajeInventario" };
        }

        private void GenerarCaja()
        {
            Text = "PUDVE - Permisos Caja";
            if (Agregar_empleado_permisos.IDPlantilla.Equals(0))
            {
                var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Caja");

                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(40, 20, 150, "Botón Agregar Dinero", datos[0]);
                Generarbutton(40, 0, 20, 20, "permisoConcepto");
                GenerarCheckbox(40, 180, 200, "Botón Historial Dinero Agregado", datos[1]);
                GenerarCheckbox(80, 20, 150, "Botón Retirar Dinero", datos[2]);
                Generarbutton(80, 0, 20, 20, "permisoConcepto");
                GenerarCheckbox(80, 180, 200, "Botón Historial Dinero Retirado", datos[3]);
                GenerarCheckbox(120, 20, 150, "Botón Abrir Caja", datos[4]);
                GenerarCheckbox(120, 180, 200, "Botón Corte Caja", datos[5]);
                GenerarCheckbox(160, 20, 150, "Mostrar Saldo Inicial", datos[6]);
                GenerarCheckbox(160, 180, 200, "Mostrar Panel Ventas", datos[7]);
                GenerarCheckbox(200, 20, 150, "Mostrar Panel Anticipos", datos[8]);
                GenerarCheckbox(200, 180, 200, "Mostrar Panel Dinero Agregado", datos[9]);
                GenerarCheckbox(235, 20, 150, "Mostrar Panel Total Caja", datos[10]);
            }
            else
            {
                var DTColumnas = cn.CargarDatos($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'subpermisos' AND COLUMN_NAME LIKE 'Caja_%'");
                var rows = DTColumnas.AsEnumerable()
                             .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var columnas = string.Format("{0}", string.Join(",", rows.ToArray()));

                var DTPermisos = cn.CargarDatos($"SELECT {columnas} FROM subpermisos WHERE IDPlantilla = {Agregar_empleado_permisos.IDPlantilla}");
                
                var rows2 = DTPermisos.AsEnumerable()
                            .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var permisos = string.Format("{0}", string.Join(",", rows2.ToArray()));

                var datos = permisos.Split(',');

                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(40, 20, 150, "Botón Agregar Dinero", Convert.ToInt32(datos[0]));
                GenerarCheckbox(40, 180, 200, "Botón Historial Dinero Agregado", Convert.ToInt32(datos[6]));
                GenerarCheckbox(80, 20, 150, "Botón Retirar Dinero", Convert.ToInt32(datos[1]));
                GenerarCheckbox(80, 180, 200, "Botón Historial Dinero Retirado", Convert.ToInt32(datos[7]));
                GenerarCheckbox(120, 20, 150, "Botón Abrir Caja", Convert.ToInt32(datos[2]));
                GenerarCheckbox(120, 180, 200, "Botón Corte Caja", Convert.ToInt32(datos[8]));
                GenerarCheckbox(160, 20, 150, "Mostrar Saldo Inicial", Convert.ToInt32(datos[3]));
                GenerarCheckbox(160, 180, 200, "Mostrar Panel Ventas", Convert.ToInt32(datos[9]));
                GenerarCheckbox(200, 20, 150, "Mostrar Panel Anticipos", Convert.ToInt32(datos[4]));
                GenerarCheckbox(200, 180, 200, "Mostrar Panel Dinero Agregado", Convert.ToInt32(datos[10]));
                GenerarCheckbox(235, 20, 150, "Mostrar Panel Total Caja", Convert.ToInt32(datos[5]));
            }

        }

        private void GenerarVentas()
        {
            Text = "PUDVE - Permisos Ventas";
            if (Agregar_empleado_permisos.IDPlantilla.Equals(0))
            {
                var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Ventas");

                GenerarCheckbox(10, 10, 150, "Marcar todos", 0);

                GenerarCheckbox(40, 10, 110, "Cancelar Venta", datos[0]);
                GenerarCheckbox(40, 130, 110, "Ver Nota Venta", datos[1]);
                GenerarCheckbox(40, 250, 125, "Ver Ticket Venta", datos[2]);
                //=============================================================
                GenerarCheckbox(80, 10, 110, "Ver Info Venta", datos[3]);
                GenerarCheckbox(80, 130, 110, "Timbrar Factura", datos[4]);
                GenerarCheckbox(80, 250, 125, "Botón Enviar Nota", datos[5]);
                //=============================================================
                GenerarCheckbox(110, 10, 110, "Buscar Venta", datos[6]);
                GenerarCheckbox(110, 130, 110, "Nueva Venta", datos[7]);
                GenerarCheckbox(110, 250, 125, "Botón Cancelar", datos[8]);
                //=============================================================
                GenerarCheckbox(140, 10, 110, "Guardar Venta", datos[9]);
                GenerarCheckbox(140, 130, 110, "Botón Anticipos", datos[10]);
                GenerarCheckbox(140, 250, 125, "Abrir Caja", datos[11]);
                //=============================================================
                GenerarCheckbox(170, 10, 115, "Ventas Guardadas", datos[12]);
                GenerarCheckbox(170, 130, 110, "Ver Último Ticket", datos[13]);
                GenerarCheckbox(170, 250, 135, "Guardar Presupuesto", datos[14]);
                //=============================================================
                GenerarCheckbox(200, 10, 115, "Asignar Cliente con Descuento", datos[15]);
                GenerarCheckbox(200, 130, 110, "Elimininar Último", datos[16]);
                GenerarCheckbox(200, 250, 135, "Eliminar Todos", datos[17]);
                //=============================================================
                GenerarCheckbox(230, 10, 115, "Aplicar Descuento", datos[18]);
                GenerarCheckbox(230, 130, 110, "Terminar Venta", datos[19]);
                GenerarCheckbox(230, 250, 200, "Venta con descuento", datos[43]);
                //=============================================================

                GenerarCheckbox(260, 10, 220, "Hacer venta a crédito", datos[47]);
                //GenerarCheckbox(260, 10, 250, "Venta a Cliente con descuento sin autorizacion", datos[44]);
            }
            else
            {
                var DTColumnas = cn.CargarDatos($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'subpermisos' AND COLUMN_NAME LIKE 'VEN_%'");
                var rows = DTColumnas.AsEnumerable()
                             .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var columnas = string.Format("{0}", string.Join(",", rows.ToArray()));

                var DTPermisos = cn.CargarDatos($"SELECT {columnas} FROM subpermisos WHERE IDPlantilla = {Agregar_empleado_permisos.IDPlantilla}");

                var rows2 = DTPermisos.AsEnumerable()
                            .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var permisos = string.Format("{0}", string.Join(",", rows2.ToArray()));

                var datos = permisos.Split(',');

                GenerarCheckbox(10, 10, 150, "Marcar todos", 0);

                GenerarCheckbox(40, 10, 110, "Cancelar Venta", Convert.ToInt32(datos[0]));
                GenerarCheckbox(40, 130, 110, "Ver Nota Venta", Convert.ToInt32(datos[7]));
                GenerarCheckbox(40, 250, 125, "Ver Ticket Venta", Convert.ToInt32(datos[14]));
                //=============================================================
                GenerarCheckbox(80, 10, 110, "Ver Info Venta", Convert.ToInt32(datos[1]));
                GenerarCheckbox(80, 130, 110, "Timbrar Factura", Convert.ToInt32(datos[8]));
                GenerarCheckbox(80, 250, 125, "Botón Enviar Nota", Convert.ToInt32(datos[15]));
                //=============================================================
                GenerarCheckbox(110, 10, 110, "Buscar Venta", Convert.ToInt32(datos[2]));
                GenerarCheckbox(110, 130, 110, "Nueva Venta", Convert.ToInt32(datos[9]));
                GenerarCheckbox(110, 250, 125, "Botón Cancelar", Convert.ToInt32(datos[16]));
                //=============================================================
                GenerarCheckbox(140, 10, 110, "Guardar Venta", Convert.ToInt32(datos[3]));
                GenerarCheckbox(140, 130, 110, "Botón Anticipos", Convert.ToInt32(datos[10]));
                GenerarCheckbox(140, 250, 125, "Abrir Caja", Convert.ToInt32(datos[17]));
                //=============================================================
                GenerarCheckbox(170, 10, 115, "Ventas Guardadas", Convert.ToInt32(datos[4]));
                GenerarCheckbox(170, 130, 110, "Ver Último Ticket", Convert.ToInt32(datos[11]));
                GenerarCheckbox(170, 250, 135, "Guardar Presupuesto", Convert.ToInt32(datos[18]));
                //=============================================================
                GenerarCheckbox(200, 10, 115, "Asignar Cliente con Descuento", Convert.ToInt32(datos[5]));
                GenerarCheckbox(200, 130, 110, "Elimininar Último", Convert.ToInt32(datos[12]));
                GenerarCheckbox(200, 250, 135, "Eliminar Todos", Convert.ToInt32(datos[19]));
                //=============================================================
                GenerarCheckbox(230, 10, 115, "Aplicar Descuento", Convert.ToInt32(datos[6]));
                GenerarCheckbox(230, 130, 110, "Terminar Venta", Convert.ToInt32(datos[13]));
                GenerarCheckbox(230, 250, 200, "Venta con descuento", Convert.ToInt32(datos[20]));
            }

        }

        private void GenerarInventario()
        {
            Text = "PUDVE - Permisos Inventario";
            if (Agregar_empleado_permisos.IDPlantilla.Equals(0))
            {
                var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Inventario");
                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(40, 130, 120, "Revisar Inventario", datos[0]);
                GenerarCheckbox(80, 130, 125, "Actualizar Inventario", datos[1]);
                GenerarCheckbox(120, 130, 150, "Actualizar Inventario XML", datos[2]);
                GenerarCheckbox(160, 130, 100, "Botón Buscar", datos[3]);
                GenerarCheckbox(200, 130, 120, "Botón Terminar", datos[4]);
                GenerarCheckbox(240, 130, 120, "Devolver Producto", datos[49]);
            }
            else
            {
                var DTColumnas = cn.CargarDatos($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'subpermisos' AND COLUMN_NAME LIKE 'INV_%'");
                var rows = DTColumnas.AsEnumerable()
                             .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var columnas = string.Format("{0}", string.Join(",", rows.ToArray()));

                var DTPermisos = cn.CargarDatos($"SELECT {columnas} FROM subpermisos WHERE IDPlantilla = {Agregar_empleado_permisos.IDPlantilla}");

                var rows2 = DTPermisos.AsEnumerable()
                            .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var permisos = string.Format("{0}", string.Join(",", rows2.ToArray()));

                var datos = permisos.Split(',');

                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(40, 130, 120, "Revisar Inventario",Convert.ToInt32(datos[0]));
                GenerarCheckbox(80, 130, 125, "Actualizar Inventario", Convert.ToInt32(datos[1]));
                GenerarCheckbox(120, 130, 150, "Actualizar Inventario XML", Convert.ToInt32(datos[2]));
                GenerarCheckbox(160, 130, 100, "Botón Buscar", Convert.ToInt32(datos[3]));
                GenerarCheckbox(200, 130, 120, "Botón Terminar", Convert.ToInt32(datos[4]));

            }
          
        }

        private void GenerarAnticipos()
        {
            Text = "PUDVE - Permisos Anticipos";
            if (Agregar_empleado_permisos.IDPlantilla.Equals(0))
            {
                var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Anticipos");
                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(40, 130, 120, "Generar Ticket", datos[0]);
                GenerarCheckbox(80, 130, 125, "Habilitar/Deshabilitar", datos[1]);
                GenerarCheckbox(120, 130, 150, "Devolver Anticipo", datos[2]);
                GenerarCheckbox(160, 130, 100, "Botón Buscar", datos[3]);
                GenerarCheckbox(200, 130, 120, "Nuevo Anticipo", datos[4]);
            }
            else
            {
                var DTColumnas = cn.CargarDatos($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'subpermisos' AND COLUMN_NAME LIKE 'ANT_%'");
                var rows = DTColumnas.AsEnumerable()
                             .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var columnas = string.Format("{0}", string.Join(",", rows.ToArray()));

                var DTPermisos = cn.CargarDatos($"SELECT {columnas} FROM subpermisos WHERE IDPlantilla = {Agregar_empleado_permisos.IDPlantilla}");

                var rows2 = DTPermisos.AsEnumerable()
                            .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var permisos = string.Format("{0}", string.Join(",", rows2.ToArray()));

                var datos = permisos.Split(',');

                GenerarCheckbox(40, 130, 120, "Generar Ticket", Convert.ToInt32(datos[0]));
                GenerarCheckbox(80, 130, 125, "Habilitar/Deshabilitar", Convert.ToInt32(datos[1]));
                GenerarCheckbox(120, 130, 150, "Devolver Anticipo", Convert.ToInt32(datos[2]));
                GenerarCheckbox(160, 130, 100, "Botón Buscar", Convert.ToInt32(datos[3]));
                GenerarCheckbox(200, 130, 120, "Nuevo Anticipo", Convert.ToInt32(datos[4]));
            }
            
        }

        private void GenerarMisDatos()
        {
            Text = "PUDVE - Permisos Mis Datos";
            if (Agregar_empleado_permisos.IDPlantilla.Equals(0))
            {
                var datos = mb.ObtenerPermisosEmpleado(id_empleado, "MisDatos");
                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(40, 130, 150, "Guardar Datos", datos[0]);
                GenerarCheckbox(80, 130, 150, "Subir Imagen", datos[1]);
                GenerarCheckbox(120, 130, 150, "Eliminar Imagen", datos[2]);
                GenerarCheckbox(160, 130, 150, "Actualizar Contraseña", datos[3]);
                GenerarCheckbox(200, 130, 150, "Actualizar Archivos", datos[4]);
            }
            else
            {
                var DTColumnas = cn.CargarDatos($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'subpermisos' AND COLUMN_NAME LIKE 'MISDA_%'");
                var rows = DTColumnas.AsEnumerable()
                             .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var columnas = string.Format("{0}", string.Join(",", rows.ToArray()));

                var DTPermisos = cn.CargarDatos($"SELECT {columnas} FROM subpermisos WHERE IDPlantilla = {Agregar_empleado_permisos.IDPlantilla}");

                var rows2 = DTPermisos.AsEnumerable()
                            .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var permisos = string.Format("{0}", string.Join(",", rows2.ToArray()));

                var datos = permisos.Split(',');
                GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
                GenerarCheckbox(40, 130, 150, "Guardar Datos",Convert.ToInt32(datos[0]));
                GenerarCheckbox(80, 130, 150, "Subir Imagen", Convert.ToInt32(datos[1]));
                GenerarCheckbox(120, 130, 150, "Eliminar Imagen", Convert.ToInt32(datos[2]));
                GenerarCheckbox(160, 130, 150, "Actualizar Contraseña", Convert.ToInt32(datos[3]));
                GenerarCheckbox(200, 130, 150, "Actualizar Archivos", Convert.ToInt32(datos[4]));
            }
           
        }

        private void GenerarFacturas()
        {
            Text = "PUDVE - Permisos Facturas";
            if (Agregar_empleado_permisos.IDPlantilla.Equals(0))
            {
                var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Facturas");
                GenerarCheckbox(10, 10, 150, "Marcar todos", 0);
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
            else
            {

                var DTColumnas = cn.CargarDatos($"SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'subpermisos' AND COLUMN_NAME LIKE 'FAC_%'");
                var rows = DTColumnas.AsEnumerable()
                             .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var columnas = string.Format("{0}", string.Join(",", rows.ToArray()));

                var DTPermisos = cn.CargarDatos($"SELECT {columnas} FROM subpermisos WHERE IDPlantilla = {Agregar_empleado_permisos.IDPlantilla}");

                var rows2 = DTPermisos.AsEnumerable()
                            .Select(r => string.Format("{0}", string.Join(",", r.ItemArray)));

                var permisos = string.Format("{0}", string.Join(",", rows2.ToArray()));

                var datos = permisos.Split(',');
                GenerarCheckbox(10, 10, 150, "Marcar todos", 0);
                GenerarCheckbox(40, 10, 100, "Ver Factura",Convert.ToInt32(datos[0]));
                GenerarCheckbox(40, 120, 120, "Descargar Factura", Convert.ToInt32(datos[1]));
                GenerarCheckbox(40, 240, 130, "Cancelar Factura", Convert.ToInt32(datos[2]));
                //=============================================================
                GenerarCheckbox(80, 10, 100, "Ver Pagos", Convert.ToInt32(datos[3]));
                GenerarCheckbox(80, 120, 120, "Buscar Factura", Convert.ToInt32(datos[4]));
                GenerarCheckbox(80, 240, 130, "Enviar Factura", Convert.ToInt32(datos[5]));
                //=============================================================
                GenerarCheckbox(120, 10, 150, "Generar Complemento", Convert.ToInt32(datos[6]));
            }
           
        }

        private void GenerarCheckbox(int top, int left, int ancho, string texto, int estado)
        {
            var checkbox = new CheckBox();
            checkbox.Text = texto;
            checkbox.Top = top;
            checkbox.Left = left;
            checkbox.Width = ancho;
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

        private void Generarbutton(int top, int left, int alto,int ancho,string nombreButton)
        {
            var button = new Button();
            button.Top = top;
            button.Left = left;
            button.Height = alto;
            button.Width = ancho;
            button.Name = nombreButton;
            button.Image = global::PuntoDeVentaV2.Properties.Resources.gear;
            button.Click += new EventHandler(btClick);
            panelContenedor.Controls.Add(button);
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
                if (seccion.Equals("Configuracion"))
                {
                    var datos = PermisosElegidos();

                    foreach (var opcion in datos)
                    {
                        string dato = datosUpdate[contador].ToString();
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET {dato} = {opcion} WHERE IDEmpleado = {id_empleado} AND IDUsuario = {FormPrincipal.userID} ");
                        contador++;
                    }
                    contador = 0;
                }
                if (seccion.Equals(apartado) && !seccion.Equals("Configuracion"))
                {
                    var datos = PermisosElegidos();
                    var numero = 1;

                    foreach (var opcion in datos)
                    {
                        cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion{numero} = {opcion} WHERE IDEmpleado = {id_empleado} AND IDUsuario = {FormPrincipal.userID} AND Seccion = '{apartado}'");
                        
                        numero++;
                    }

                    //De aqui para abajo tuve que agregar otra validacion para permisos porque solo dios sabe que programador hizo la consulta de arriba con las patas :D
                    if (seccion.Equals("Reportes") || seccion.Equals("Inventario"))
                    {
                        foreach (Control item in panelContenedor.Controls)
                        {
                            if (item is CheckBox && !item.Text.Equals("Marcar todos"))
                            {
                                if (item is CheckBox && !item.Text.Equals("Desmarcar todos"))
                                {
                                    if (item.Text.Equals("Reporte de Adeudos"))
                                    {
                                        var cb = (CheckBox)item;

                                        var seleccionado = 0;

                                        if (cb.Checked)
                                        {
                                            seleccionado = 1;
                                        }

                                        cn.EjecutarConsulta($"UPDATE empleadospermisos SET ReporteDeudas = {seleccionado} WHERE IDEmpleado = {id_empleado} AND Seccion = '{seccion}' AND IDUsuario = {FormPrincipal.userID}");
                                    }
                                    else if (item.Text.Equals("Devolver Producto"))
                                    {
                                        var cb = (CheckBox)item;

                                        var seleccionado = 0;

                                        if (cb.Checked)
                                        {
                                            seleccionado = 1;
                                        }

                                        cn.EjecutarConsulta($"UPDATE empleadospermisos SET RegresarProducto = {seleccionado} WHERE IDEmpleado = {id_empleado} AND Seccion = '{seccion}' AND IDUsuario = {FormPrincipal.userID}");
                                    }
                                }
                            }
                        }
                    }
                }
                if (seccion.Equals("Ventas"))
                {
                    var datos = PermisosElegidos();
                    //AQUI SE PONE LA COLUMNA A MODIFICAR FIJAMENTE
                    cn.EjecutarConsulta($"UPDATE empleadospermisos SET PermisoVentaClienteDescuento = {datos[20]} WHERE IDEmpleado = {id_empleado} AND IDUsuario = {FormPrincipal.userID} AND Seccion = '{apartado}'");

                    cn.EjecutarConsulta($"UPDATE empleadospermisos SET VentasACredito = {datos[21]} WHERE IDEmpleado = {id_empleado} AND IDUsuario = {FormPrincipal.userID} AND Seccion = '{apartado}'");
                    //
                    //cn.EjecutarConsulta($"UPDATE empleadospermisos SET PermisoVentaClienteDescuentoSinAutorizacion = {datos[21]} WHERE IDEmpleado = {id_empleado} AND IDUsuario = {FormPrincipal.userID} AND Seccion = '{apartado}'");
                }
            }

            Close();
        }

        private int[] PermisosElegidos()
        {
            List<int> opciones = new List<int>();

            if (seccion.Equals("Caja"))
            {
                Agregar_empleado_permisos.Caja.Clear();
            }
            if (seccion.Equals("Ventas"))
            {
                Agregar_empleado_permisos.Ventas.Clear();
            }
            if (seccion.Equals("Inventario"))
            {
                Agregar_empleado_permisos.Inventario.Clear();
            }
            if (seccion.Equals("Anticipos"))
            {
                Agregar_empleado_permisos.Anticipos.Clear();
            }
            if (seccion.Equals("MisDatos"))
            {
                Agregar_empleado_permisos.MisDatos.Clear();
            }
            if (seccion.Equals("Facturas"))
            {
                Agregar_empleado_permisos.Facturas.Clear();
            }
            if (seccion.Equals("Configuracion"))
            {
                Agregar_empleado_permisos.Configuracion.Clear();
            }
            if (seccion.Equals("Reportes"))
            {
                Agregar_empleado_permisos.Reportes.Clear();
            }
            if (seccion.Equals("Clientes"))
            {
                Agregar_empleado_permisos.Clientes.Clear();
            }
            if (seccion.Equals("Proveedores"))
            {
                Agregar_empleado_permisos.Proveedores.Clear();
            }
            if (seccion.Equals("Empleados"))
            {
                Agregar_empleado_permisos.Empleados.Clear();
            }
            if (seccion.Equals("Productos"))
            {
                Agregar_empleado_permisos.Productos.Clear();
            }
            if (seccion.Equals("Bascula"))
            {
                Agregar_empleado_permisos.Bascula.Clear();
            }

            foreach (Control item in panelContenedor.Controls)
            {
                if (item is CheckBox && !item.Text.Equals("Marcar todos"))
                {
                    if (item is CheckBox && !item.Text.Equals("Desmarcar todos") && !item.Text.Equals("Reporte de Adeudos") && !item.Text.Equals("Devolver Producto"))
                    {
                        var cb = (CheckBox)item;

                        var seleccionado = 0;

                        if (cb.Checked)
                        {
                            seleccionado = 1;
                        }

                        opciones.Add(seleccionado);

                        if (seccion.Equals("Caja"))
                        {
                            Agregar_empleado_permisos.Caja.Add(seleccionado);
                        }
                        if (seccion.Equals("Ventas"))
                        {
                            Agregar_empleado_permisos.Ventas.Add(seleccionado);
                        }
                        if (seccion.Equals("Inventario"))
                        {
                            Agregar_empleado_permisos.Inventario.Add(seleccionado);
                        }
                        if (seccion.Equals("Anticipos"))
                        {
                            Agregar_empleado_permisos.Anticipos.Add(seleccionado);
                        }
                        if (seccion.Equals("MisDatos"))
                        {
                            Agregar_empleado_permisos.MisDatos.Add(seleccionado);
                        }
                        if (seccion.Equals("Facturas"))
                        {
                            Agregar_empleado_permisos.Facturas.Add(seleccionado);
                        }
                        if (seccion.Equals("Configuracion"))
                        {
                            Agregar_empleado_permisos.Configuracion.Add(seleccionado);
                        }
                        if (seccion.Equals("Reportes"))
                        {
                            Agregar_empleado_permisos.Reportes.Add(seleccionado);
                        }
                        if (seccion.Equals("Clientes"))
                        {
                            Agregar_empleado_permisos.Clientes.Add(seleccionado);
                        }
                        if (seccion.Equals("Proveedores"))
                        {
                            Agregar_empleado_permisos.Proveedores.Add(seleccionado);
                        }
                        if (seccion.Equals("Empleados"))
                        {
                            Agregar_empleado_permisos.Empleados.Add(seleccionado);
                        }
                        if (seccion.Equals("Productos"))
                        {
                            Agregar_empleado_permisos.Productos.Add(seleccionado);
                        }
                        if (seccion.Equals("Bascula"))
                        {
                            Agregar_empleado_permisos.Bascula.Add(seleccionado);
                        }
                    }
                    else
                    {

                    }
                }
             }
            return opciones.ToArray();
        }
        //private void GenerarPermisos()
        //{
        //    Text = "PUDVE - Permisos Asignar";
        //    var datos2 = mb.ObtenerPermisosEmpleado(id_empleado, "Permisos");

        //    GenerarCheckbox(10, 45, 100, "Mensaje Ventas", datos2[0]);
        //    GenerarCheckbox(10, 200, 200, "Mensaje Inventario", datos2[1]);
        //    =============================================================
        //    GenerarCheckbox(30, 45, 150, "Stock", datos2[2]);
        //    GenerarCheckbox(30, 200, 150, "Stock Minimo", datos2[3]);
        //    =============================================================
        //    GenerarCheckbox(50, 45, 150, "Stock Maximo", datos2[4]);
        //    GenerarCheckbox(50, 200, 150, "Precio", datos2[5]);
        //    =============================================================
        //    GenerarCheckbox(70, 45, 150, "Número de Revisión", datos2[6]);
        //    GenerarCheckbox(70, 200, 150, "Tipo de IVA", datos2[7]);
        //    =============================================================
        //    GenerarCheckbox(90, 45, 150, "Clave de Producto", datos2[8]);
        //    GenerarCheckbox(90, 200, 150, "Clave de Unidad", datos2[9]);
        //    =============================================================
        //    GenerarCheckbox(110, 45, 150, "Correos", datos2[10]);
        //}

        private void btClick(object sender, EventArgs e)
        {
            PermisosDinamicosConfiguracion permisos = new PermisosDinamicosConfiguracion();
            Text = "PUDVE - Permisos Asignar Ticket";
            
            Button btnSeleccionado = (Button)sender;
            var nombreBoton = btnSeleccionado.Name.ToString();


            //if (nombreBoton == "editarTicket")
            //{
            //    //condicion permisos
            //   var dato = cn.CargarDatos($"SELECT editarTicket FROM EmpleadosPermisos WHERE IDEmpleado = {id_empleado} AND IDUsuario = {FormPrincipal.idUsuarioPermisosParaConfiguracion} AND Seccion = '{nombreBoton}'");
            //    if (true)
            //    {

            //    }
            //    permisos.tipoPermisos = nombreBoton;
            //    permisos.ShowDialog();
            //}
            if (nombreBoton == "envioCorreo")
            {
                
                permisos.tipoPermisos = nombreBoton;
                permisos.ShowDialog();
            }
            if (nombreBoton == "configuracionGeneral")
            {
                permisos.tipoPermisos = nombreBoton;
                permisos.ShowDialog();
            }
            if (nombreBoton == "permisosProductos")
            {
                Permisos_Asignar asig = new Permisos_Asignar("Permisos", id_empleado);
                asig.Show();
            }
            if (nombreBoton == "permisoConcepto")
            {
                permisos.tipoPermisos = nombreBoton;
                permisos.ShowDialog();
            }
            //if (nombreBoton == "porcentageGanancia")
            //{
            //    //condicion permisos
            //    permisos.tipoPermisos = nombreBoton;
            //    permisos.ShowDialog();
            //}
            //if (nombreBoton == "tipoMoneda")
            //{
            //    //condicion permisos
            //    permisos.tipoPermisos = nombreBoton;
            //    permisos.ShowDialog();
            //}
            //if (nombreBoton == "respaldarInformacion")
            //{
            //    //condicion permisos
            //    permisos.tipoPermisos = nombreBoton;
            //    permisos.ShowDialog();
            //}
        }

        private void SeccionPermisos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        
    }
}
