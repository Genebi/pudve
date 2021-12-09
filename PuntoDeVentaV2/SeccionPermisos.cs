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
            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Bascula");
            
            GenerarCheckbox(10, 35, 150, "Marcar todos", 0);
            GenerarCheckbox(30, 35, 250, "Agregar/Editar Predeterminada", datos[0]);

        }

        private void GenerarProductos()
        {
            Text = "PUDVE - Permisos Productos";
            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Productos");

            GenerarCheckbox(10,35,150,"Marcar todos",0);
            //============================================================
            GenerarCheckbox(30, 35, 100, "Agregar XML", datos[0]);
            GenerarCheckbox(30, 200, 200, "Deshabilitar Seleccionados", datos[1]);
            //=============================================================
            GenerarCheckbox(50, 35, 150, "Cambiar Tipo", datos[2]);
            GenerarCheckbox(50, 200, 150, "Mostrar en Lista", datos[3]);
            //=============================================================
            GenerarCheckbox(70, 35, 150, "Botón Asignar", datos[4]);
            Generarbutton(70, 10, 20, 20,"permisosProductos");
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

        private void GenerarEmpleados()
        {
            Text = "PUDVE - Permisos Empleados";

            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Empleados");

            GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
            GenerarCheckbox(50, 130, 150, "Nuevo Empleado", datos[0]);
            GenerarCheckbox(90, 130, 150, "Editar Empleado", datos[1]);
            GenerarCheckbox(130, 130, 150, "Permisos Empleado", datos[2]);
        }

        private void GenerarProveedores()
        {
            Text = "PUDVE - Permisos Proveedores";

            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Proveedores");
            GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
            GenerarCheckbox(50, 130, 150, "Botón Buscar", datos[0]);
            GenerarCheckbox(90, 130, 150, "Nuevo Proveedor", datos[1]);
            GenerarCheckbox(130, 130, 150, "Deshabilitar", datos[2]);
            GenerarCheckbox(170, 130, 150, "habilitar", datos[3]);

        }

        private void GenerarClientes()
        {
            Text = "PUDVE - Permisos Clientes";

            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Clientes");
            GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
            GenerarCheckbox(40, 130, 150, "Botón Buscar", datos[0]);
            GenerarCheckbox(80, 130, 150, "Nuevo Tipo Cliente", datos[1]);
            GenerarCheckbox(120, 130, 150, "Listado Tipo Cliente", datos[2]);
            GenerarCheckbox(160, 130, 150, "Nuevo Cliente", datos[3]);
            GenerarCheckbox(200, 130, 150, "Deshabilitar", datos[4]);
            GenerarCheckbox(240, 130, 150, "habilitar", datos[5]);
        }

        private void GenerarReportes()
        {
            Text = "PUDVE - Permisos Reportes";

            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Reportes");
            GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
            GenerarCheckbox(50, 130, 150, "Historial de Precios", datos[0]);
            GenerarCheckbox(90, 130, 150, "Historial Dinero Agregado", datos[1]);
        }

        private void GenerarConfiguracion()
        {
            Text = "PUDVE - Permisos Configuración";

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
            Generarbutton(40, 185, 20, 20,"envioCorreo");
            GenerarCheckbox(40, 210, 150, "Envio de Correo", datos[36]);
            //=============================================================
            Generarbutton(40, 10, 20, 20,"configuracionGeneral");
            GenerarCheckbox(40, 35, 150, "Configuracion General", datos[37]);
            //=============================================================
            GenerarCheckbox(70, 210, 150, "Porcentaje de Ganancia", datos[38]);
            //=============================================================
            GenerarCheckbox(100, 35, 150, "Tipo de Moneda", datos[39]);
            //=============================================================
            GenerarCheckbox(100, 210, 150, "Respaldar Informacion", datos[40]);


            seccion = "Configuracion";
            datosUpdate = new string[] { "editarTicket","EnvioCorreo","confiGeneral","porcentajeGanancia","tipoMoneda", "RespaldarInfo","MensajeVentas","MensajeInventario" };
        }

        private void GenerarCaja()
        {
            Text = "PUDVE - Permisos Caja";

            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Caja");

            GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
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
            GenerarCheckbox(200, 10, 115, "Descuento Cliente", datos[15]);
            GenerarCheckbox(200, 130, 110, "Elimininar Último", datos[16]);
            GenerarCheckbox(200, 250, 135, "Eliminar Todos", datos[17]);
            //=============================================================
            GenerarCheckbox(230, 10, 115, "Aplicar Descuento", datos[18]);
            GenerarCheckbox(230, 130, 110, "Terminar Venta", datos[19]);
        }

        private void GenerarInventario()
        {
            Text = "PUDVE - Permisos Inventario";

            var datos = mb.ObtenerPermisosEmpleado(id_empleado, "Inventario");
            GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
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
            GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
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
            GenerarCheckbox(10, 20, 150, "Marcar todos", 0);
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
                }
            }

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
