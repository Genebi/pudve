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
    public partial class Agregar_empleado_permisos : Form
    {

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        private int id_empleado = 0; 


        public Agregar_empleado_permisos(int id_emp)
        {
            InitializeComponent();

            id_empleado = id_emp;
        }

        private void cargar_datos(object sender, EventArgs e)
        {
            var datos_e= mb.obtener_permisos_empleado(id_empleado, FormPrincipal.userID);
            cargar_checkbox_permisos(datos_e);
        }

        private void cargar_checkbox_permisos(string[] datos_e)
        {
            cbox_anticipos.Checked = Convert.ToBoolean(Convert.ToInt32(datos_e[0])); 
            cbox_caja.Checked = Convert.ToBoolean(Convert.ToInt32(datos_e[1]));
            cbox_clientes.Checked = Convert.ToBoolean(Convert.ToInt32(datos_e[2]));
            cbox_configuracion.Checked = Convert.ToBoolean(Convert.ToInt32(datos_e[3]));
            cbox_empleados.Checked = Convert.ToBoolean(Convert.ToInt32(datos_e[4]));
            cbox_empresas.Checked = Convert.ToBoolean(Convert.ToInt32(datos_e[5]));
            cbox_facturas.Checked = Convert.ToBoolean(Convert.ToInt32(datos_e[6]));
            cbox_inventario.Checked = Convert.ToBoolean(Convert.ToInt32(datos_e[7]));
            cbox_misdatos.Checked = Convert.ToBoolean(Convert.ToInt32(datos_e[8]));
            cbox_productos.Checked = Convert.ToBoolean(Convert.ToInt32(datos_e[9]));
            cbox_proveedores.Checked = Convert.ToBoolean(Convert.ToInt32(datos_e[10]));
            cbox_reportes.Checked = Convert.ToBoolean(Convert.ToInt32(datos_e[11]));
            cbox_servicios.Checked = Convert.ToBoolean(Convert.ToInt32(datos_e[12]));
            cbox_ventas.Checked = Convert.ToBoolean(Convert.ToInt32(datos_e[13]));
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {

            string anticipo = Convert.ToString(Convert.ToInt32(cbox_anticipos.Checked));
            string caja = Convert.ToString(Convert.ToInt32(cbox_caja.Checked));
            string client = Convert.ToString(Convert.ToInt32(cbox_clientes.Checked));
            string config = Convert.ToString(Convert.ToInt32(cbox_configuracion.Checked));
            string empleado = Convert.ToString(Convert.ToInt32(cbox_empleados.Checked));
            string empresa = Convert.ToString(Convert.ToInt32(cbox_empresas.Checked));
            string factura = Convert.ToString(Convert.ToInt32(cbox_facturas.Checked));
            string inventario = Convert.ToString(Convert.ToInt32(cbox_inventario.Checked));
            string mdatos = Convert.ToString(Convert.ToInt32(cbox_misdatos.Checked));
            string producto = Convert.ToString(Convert.ToInt32(cbox_productos.Checked));
            string proveedor = Convert.ToString(Convert.ToInt32(cbox_proveedores.Checked));
            string reporte = Convert.ToString(Convert.ToInt32(cbox_reportes.Checked));
            string servicio = Convert.ToString(Convert.ToInt32(cbox_servicios.Checked));
            string venta = Convert.ToString(Convert.ToInt32(cbox_ventas.Checked));


            string[] datos = new string[]
            {
                FormPrincipal.userID.ToString(), id_empleado.ToString(), anticipo, caja, client, config, empleado,
                empresa, factura, inventario, mdatos, producto, proveedor, reporte, servicio, venta
            };

            int r = cn.EjecutarConsulta(cs.guardar_editar_empleado(datos, 2));

            if(r > 0)
            {
                this.Close();
            }
        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SeccionPermisos>().Count() == 1)
            {
                Application.OpenForms.OfType<SeccionPermisos>().First().BringToFront();
            }
            else
            {
                var permisos = new SeccionPermisos("Caja", id_empleado);

                permisos.Show();
            }
        }

        private void btnVentas_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SeccionPermisos>().Count() == 1)
            {
                Application.OpenForms.OfType<SeccionPermisos>().First().BringToFront();
            }
            else
            {
                var permisos = new SeccionPermisos("Ventas", id_empleado);

                permisos.Show();
            }
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SeccionPermisos>().Count() == 1)
            {
                Application.OpenForms.OfType<SeccionPermisos>().First().BringToFront();
            }
            else
            {
                var permisos = new SeccionPermisos("Inventario", id_empleado);

                permisos.Show();
            }
        }

        private void btnAnticipos_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SeccionPermisos>().Count() == 1)
            {
                Application.OpenForms.OfType<SeccionPermisos>().First().BringToFront();
            }
            else
            {
                var permisos = new SeccionPermisos("Anticipos", id_empleado);

                permisos.Show();
            }
        }

        private void btnMisDatos_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SeccionPermisos>().Count() == 1)
            {
                Application.OpenForms.OfType<SeccionPermisos>().First().BringToFront();
            }
            else
            {
                var permisos = new SeccionPermisos("MisDatos", id_empleado);

                permisos.Show();
            }
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SeccionPermisos>().Count() == 1)
            {
                Application.OpenForms.OfType<SeccionPermisos>().First().BringToFront();
            }
            else
            {
                var permisos = new SeccionPermisos("Facturas", id_empleado);

                permisos.Show();
            }
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SeccionPermisos>().Count() == 1)
            {
                Application.OpenForms.OfType<SeccionPermisos>().First().BringToFront();
            }
            else
            {
                var permisos = new SeccionPermisos("Configuracion", id_empleado);

                permisos.Show();
            }
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SeccionPermisos>().Count() == 1)
            {
                Application.OpenForms.OfType<SeccionPermisos>().First().BringToFront();
            }
            else
            {
                var permisos = new SeccionPermisos("Reportes", id_empleado);

                permisos.Show();
            }
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SeccionPermisos>().Count() == 1)
            {
                Application.OpenForms.OfType<SeccionPermisos>().First().BringToFront();
            }
            else
            {
                var permisos = new SeccionPermisos("Clientes", id_empleado);

                permisos.Show();
            }
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SeccionPermisos>().Count() == 1)
            {
                Application.OpenForms.OfType<SeccionPermisos>().First().BringToFront();
            }
            else
            {
                var permisos = new SeccionPermisos("Proveedores", id_empleado);

                permisos.Show();
            }
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<SeccionPermisos>().Count() == 1)
            {
                Application.OpenForms.OfType<SeccionPermisos>().First().BringToFront();
            }
            else
            {
                var permisos = new SeccionPermisos("Empleados", id_empleado);

                permisos.Show();
            }
        }
    }
}
