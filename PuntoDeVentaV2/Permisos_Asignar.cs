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
            checkbox.Text = texto.Replace("_", " ");
            checkbox.Top = top;
            checkbox.Left = left;
            checkbox.Width = ancho;
            checkbox.CheckedChanged += new EventHandler(checkbox_click);
            checkbox.Checked = Convert.ToBoolean(estado);

            panelContenedor2.Controls.Add(checkbox);
        }

        private void checkbox_click(object sender, EventArgs e)
        {
            CheckBox chkPermisos = (CheckBox)sender;
            var concepto = chkPermisos.Text.Trim();

            if (concepto.Equals("Mensaje Ventas") || concepto.Equals("Mensaje Inventario") || concepto.Equals("Stock") || concepto.Equals("Stock Minimo") || concepto.Equals("Stock Maximo") || concepto.Equals("Precio") || concepto.Equals("Número de Revisión") || concepto.Equals("Tipo de IVA") || concepto.Equals("Clave de Producto") || concepto.Equals("Clave de Unidad") || concepto.Equals("Correos") || concepto.Equals("Marcar todos") || concepto.Equals("Desmarcar todos"))
            {

            }
            else
            {
                var value = chkPermisos.Checked;
                int estado = 0;
                if (value.Equals(true))
                {
                    estado = 1;
                }
                else
                {
                    estado = 0;
                }
                concepto = concepto.Replace(" ", "_");
                cn.EjecutarConsulta(cs.permisisAsignarDinamicos(concepto, estado, id_empleado.ToString()));
            }
            //=====================================================================================
            CheckBox checkbox = (CheckBox)sender;
            foreach (Control objetos in panelContenedor2.Controls)
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
            //=====================================================================================
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
            var datos2 = mb.ObtenerPermisosEmpleado(id_empleado, "Productos");

            GenerarCheckbox(10, 45, 150, "Marcar todos", 0);
            //=============================================================
            GenerarCheckbox(30, 45, 150, "Mensaje Ventas", datos2[24]);
            GenerarCheckbox(30, 200, 150, "Mensaje Inventario", datos2[25]);
            //=============================================================
            GenerarCheckbox(50, 45, 150, "Stock", datos2[26]);
            GenerarCheckbox(50, 200, 150, "Stock Minimo", datos2[27]);
            //=============================================================
            GenerarCheckbox(70, 45, 150, "Stock Maximo", datos2[28]);
            GenerarCheckbox(70, 200, 150, "Precio", datos2[29]);
            //=============================================================
            GenerarCheckbox(90, 45, 150, "Número de Revisión", datos2[30]);
            GenerarCheckbox(90, 200, 150, "Tipo de IVA", datos2[31]);
            //=============================================================
            GenerarCheckbox(110, 45, 150, "Clave de Producto", datos2[32]);
            GenerarCheckbox(110, 200, 150, "Clave de Unidad", datos2[33]);
            //=============================================================
            GenerarCheckbox(130, 45, 150, "Correos", datos2[34]);
            GenerarCheckbox(130, 200, 150, "Agregar Descuento", datos2[45]);
            //=============================================================
            GenerarCheckbox(150, 45, 150, "Eliminar Descuento", datos2[46]);

            int contador = 0;
            int dato2 = 41;
            int top = 150, left = 200, ancho = 150;
            using (DataTable dtPermisosDinamicos = cn.CargarDatos(cs.verificarPermisosDinamicos(FormPrincipal.userID)))
            {
                if (!dtPermisosDinamicos.Rows.Count.Equals(0))
                {
                    foreach (DataRow drConcepto in dtPermisosDinamicos.Rows)
                    {
                        var concepto = drConcepto["concepto"].ToString();
                        
                        if (concepto == "Proveedor" && top == 130)
                        {
                            GenerarCheckbox(top, left, ancho, concepto, datos2[dato2]);
                            top += 20;
                            dato2++;
                            contador++;
                        }
                        else if (top == 130)
                        { 
                            GenerarCheckbox(top, left, ancho, concepto, datos2[dato2]);
                            top += 20;
                            dato2++;
                            contador++;
                        }

                        if (left == 45 && contador == 0)
                        {
                            left = 200;
                            GenerarCheckbox(top, left, ancho, concepto, datos2[dato2]);
                            dato2++;
                            top += 20;
                        }
                        else if (left == 200 && contador == 0)
                        {
                            left = 45;
                            GenerarCheckbox(top, left, ancho, concepto, datos2[dato2]);
                            dato2++;

                        }
                        contador = 0;

                    }
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
