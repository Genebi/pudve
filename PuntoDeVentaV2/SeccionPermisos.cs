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

        public SeccionPermisos(string seccion, int id_empleado)
        {
            InitializeComponent();

            this.seccion = seccion;
            this.id_empleado = id_empleado;
        }

        private void SeccionPermisos_Load(object sender, EventArgs e)
        {
            if (seccion == "Caja")
                GenerarCaja();
        }

        private void GenerarCaja()
        {
            this.Text = "PUDVE - Permisos Caja";

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

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (seccion == "Caja")
            {
                var existe = (bool)cn.EjecutarSelect($"SELECT * FROM EmpleadosPermisos WHERE IDEmpleado = {id_empleado} AND IDUsuario = {FormPrincipal.userID} AND Seccion = 'Caja'");

                if (!existe)
                {
                    cn.EjecutarConsulta($"INSERT INTO EmpleadosPermisos (IDEmpleado, IDUsuario, Seccion) VALUES ('{id_empleado}', '{FormPrincipal.userID}', 'Caja')");
                }

                var datos = PermisosElegidos();
                var numero = 1;

                foreach (var opcion in datos)
                {
                    cn.EjecutarConsulta($"UPDATE EmpleadosPermisos SET Opcion{numero} = {opcion} WHERE IDEmpleado = {id_empleado} AND IDUsuario = {FormPrincipal.userID} AND Seccion = 'Caja'");

                    numero++;
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
