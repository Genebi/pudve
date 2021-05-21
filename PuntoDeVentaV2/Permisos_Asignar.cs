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
    public partial class Permisos_Asignar : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        private string seccion = string.Empty;
        private int id_empleado;
        List<int> opciones = new List<int>();
        private string[] secciones;


        public Permisos_Asignar(string seccion, int id_empleado)
        {
            InitializeComponent();
          

            this.seccion = seccion;
            this.id_empleado = id_empleado;
        }

        private void GenerarCheckbox(int top, int left, int ancho, string texto, int estado)
        {
            var checkbox = new CheckBox();
            checkbox.Text = texto;
            checkbox.Top = top;
            checkbox.Left = left;
            checkbox.Width = ancho;
            checkbox.Checked = Convert.ToBoolean(estado);

            panelContenedor2.Controls.Add(checkbox);
        }

        private void Permisos_Asignar_Load(object sender, EventArgs e)
        {
            secciones = new string[] {
                "Caja", "Ventas", "Inventario", "Anticipos",
                "MisDatos", "Facturas", "Configuracion", "Reportes",
                "Clientes", "Proveedores", "Empleados", "Productos", "Permisos"
            };

            VerificarSecciones();



            if (seccion.Equals("Permisos"))
                GenerarPermisos();

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

        private void GenerarPermisos()
        {
            Text = "PUDVE - Permisos Asignar";
            var datos2 = mb.ObtenerPermisosEmpleado(id_empleado,"Productos");

            GenerarCheckbox(10, 45, 100, "Mensaje Ventas", datos2[0]);
            GenerarCheckbox(10, 200, 200, "Mensaje Inventario", datos2[1]);
            //=============================================================
            GenerarCheckbox(30, 45, 150, "Stock", datos2[2]);
            GenerarCheckbox(30, 200, 150, "Stock Minimo", datos2[3]);
            //=============================================================
            GenerarCheckbox(50, 45, 150, "Stock Maximo", datos2[4]);
            GenerarCheckbox(50, 200, 150, "Precio", datos2[5]);
            //=============================================================
            GenerarCheckbox(70, 45, 150, "Número de Revisión", datos2[6]);
            GenerarCheckbox(70, 200, 150, "Tipo de IVA", datos2[7]);
            //=============================================================
            GenerarCheckbox(90, 45, 150, "Clave de Producto", datos2[8]);
            GenerarCheckbox(90, 200, 150, "Clave de Unidad", datos2[9]);
            //=============================================================
            GenerarCheckbox(110, 45, 150, "Correos", datos2[10]);
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
            try
            {
                cn.EjecutarConsulta(cs.permisosAsignar(opciones, id_empleado.ToString()));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            Close();
        }

        private int[] PermisosElegidos()
        {
            

            foreach (Control item in panelContenedor2.Controls)
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
