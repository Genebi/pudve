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
    public partial class IngresarrNombrePlantilla : Form
    {
        Conexion cn = new Conexion();
        string[] datos;
        List<int> SubPermisos = new List<int>();
        public IngresarrNombrePlantilla(string[] permisos)
        {
            InitializeComponent();
            this.datos = permisos;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_aceptar_Click(object sender, EventArgs e)
        {
            string nombre;
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese un Nombre para la Plantilla","Aviso del Sistema",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                nombre = txtNombre.Text;
            }

            cn.EjecutarConsulta($"INSERT INTO plantillapermisos(Nombre,Estatus,IDUsuario,Anticipo,Caja,clientes,configuracion,empleado,factura,inventario,misdatos,productos,proveedor,reportes,precio,ventas,bascula,ConsultaPrecio)VALUES('{nombre}',1,{datos[0]},{datos[1]},{datos[2]},{datos[3]},{datos[4]},{datos[5]},{datos[6]},{datos[7]},{datos[8]},{datos[9]},{datos[10]},{datos[11]},{datos[12]},{datos[13]},{datos[14]},{datos[15]})");
            AgregarSubPermisos();
            using (var ColumnSubPermisos = cn.CargarDatos($"SELECT * FROM subpermisos ORDER BY ID DESC LIMIT  1"))
            {
                var DTPlantillas = cn.CargarDatos("SELECT ID FROM plantillapermisos ORDER BY ID DESC LIMIT 1");
                string IDPlantilla = DTPlantillas.Rows[0]["ID"].ToString();
                cn.EjecutarConsulta($"INSERT INTO subpermisos ( IDPlantilla )VALUES( {IDPlantilla} )");
                int contador = 0;
                foreach (var item in ColumnSubPermisos.Columns)
                {
                    string nombreColumna = item.ToString();
                    if (!nombreColumna.Equals("ID")&&!nombreColumna.Equals("IDPlantilla"))
                    {
                        cn.EjecutarConsulta($"UPDATE subpermisos SET {nombreColumna} = {SubPermisos[contador]} WHERE IDPlantilla = {IDPlantilla}");
                        contador++;
                    }
                }
                contador = 0;
            }
            LimpiarLista();
            MessageBox.Show("Plantilla guardad exitosamente","Aviso del Sistem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Agregar_empleado_permisos.SeGuardo = true;
            this.Close();
        }

        private void LimpiarLista()
        {
            Agregar_empleado_permisos.Anticipos.Clear();
            Agregar_empleado_permisos.Configuracion.Clear();
            Agregar_empleado_permisos.Facturas.Clear();
            Agregar_empleado_permisos.Productos.Clear();
            Agregar_empleado_permisos.Ventas.Clear();
            Agregar_empleado_permisos.Caja.Clear();
            Agregar_empleado_permisos.Empleados.Clear();
            Agregar_empleado_permisos.Inventario.Clear();
            Agregar_empleado_permisos.Proveedores.Clear();
            Agregar_empleado_permisos.Clientes.Clear();
            Agregar_empleado_permisos.Bascula.Clear();
            Agregar_empleado_permisos.MisDatos.Clear();
            Agregar_empleado_permisos.Reportes.Clear();

        }

        private void AgregarSubPermisos()
        {
            int Permiso = 1;
            if (Agregar_empleado_permisos.Anticipos.Count.Equals(0))
            {
                for (int i = 0; i < 5; i++)
                {
                    SubPermisos.Add(Permiso);
                }
            }
            else
            {
                foreach (var item in Agregar_empleado_permisos.Anticipos)
                {
                    SubPermisos.Add(item);
                }
            }
            if (Agregar_empleado_permisos.Configuracion.Count.Equals(0))
            {
                for (int i = 0; i < 6; i++)
                {
                    SubPermisos.Add(Permiso);
                }
            }
            else
            {
                foreach (var item in Agregar_empleado_permisos.Configuracion)
                {
                    SubPermisos.Add(item);
                }
            }
            if (Agregar_empleado_permisos.Facturas.Count.Equals(0))
            {
                for (int i = 0; i < 7; i++)
                {
                    SubPermisos.Add(Permiso);
                }
            }
            else
            {
                foreach (var item in Agregar_empleado_permisos.Facturas)
                {
                    SubPermisos.Add(item);
                }
            }
            if (Agregar_empleado_permisos.Productos.Count.Equals(0))
            {
                for (int i = 0; i < 22; i++)
                {
                    SubPermisos.Add(Permiso);
                }
            }
            else
            {
                foreach (var item in Agregar_empleado_permisos.Productos)
                {
                    SubPermisos.Add(item);
                }
            }
            if (Agregar_empleado_permisos.Ventas.Count.Equals(0))
            {
                for (int i = 0; i < 21; i++)
                {
                    SubPermisos.Add(Permiso);
                }
            }
            else
            {
                foreach (var item in Agregar_empleado_permisos.Ventas)
                {
                    SubPermisos.Add(item);
                }
            }
            if (Agregar_empleado_permisos.Caja.Count.Equals(0))
            {
                for (int i = 0; i < 11; i++)
                {
                    SubPermisos.Add(Permiso);
                }
            }
            else
            {
                foreach (var item in Agregar_empleado_permisos.Caja)
                {
                    SubPermisos.Add(item);
                }
            }
            if (Agregar_empleado_permisos.Empleados.Count.Equals(0))
            {
                for (int i = 0; i < 3; i++)
                {
                    SubPermisos.Add(Permiso);
                }
            }
            else
            {
                foreach (var item in Agregar_empleado_permisos.Empleados)
                {
                    SubPermisos.Add(item);
                }
            }
            if (Agregar_empleado_permisos.Inventario.Count.Equals(0))
            {
                for (int i = 0; i < 5; i++)
                {
                    SubPermisos.Add(Permiso);
                }
            }
            else
            {
                foreach (var item in Agregar_empleado_permisos.Inventario)
                {
                    SubPermisos.Add(item);
                }
            }
            if (Agregar_empleado_permisos.Proveedores.Count.Equals(0))
            {
                for (int i = 0; i < 4; i++)
                {
                    SubPermisos.Add(Permiso);
                }
            }
            else
            {
                foreach (var item in Agregar_empleado_permisos.Proveedores)
                {
                    SubPermisos.Add(item);
                }
            }
            if (Agregar_empleado_permisos.Clientes.Count.Equals(0))
            {
                for (int i = 0; i < 6; i++)
                {
                    SubPermisos.Add(Permiso);
                }
            }
            else
            {
                foreach (var item in Agregar_empleado_permisos.Clientes)
                {
                    SubPermisos.Add(item);
                }
            }
            if (Agregar_empleado_permisos.Bascula.Count.Equals(0))
            {
                for (int i = 0; i < 1; i++)
                {
                    SubPermisos.Add(Permiso);
                }
            }
            else
            {
                foreach (var item in Agregar_empleado_permisos.Bascula)
                {
                    SubPermisos.Add(item);
                }
            }
            if (Agregar_empleado_permisos.MisDatos.Count.Equals(0))
            {
                for (int i = 0; i < 5; i++)
                {
                    SubPermisos.Add(Permiso);
                }
            }
            else
            {
                foreach (var item in Agregar_empleado_permisos.MisDatos)
                {
                    SubPermisos.Add(item);
                }
            }
            if (Agregar_empleado_permisos.Reportes.Count.Equals(0))
            {
                for (int i = 0; i < 6; i++)
                {
                    SubPermisos.Add(Permiso);
                }
            }
            else
            {
                foreach (var item in Agregar_empleado_permisos.Reportes)
                {
                    SubPermisos.Add(item);
                }
            }
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            Agregar_empleado_permisos.SeGuardo = false;
            this.Close();
        }
    }
}
