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

            MessageBox.Show("Plantilla guardad exitosamente","Aviso del Sistem", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Agregar_empleado_permisos.SeGuardo = true;
            this.Close();
        }

        private void btn_cancelar_Click(object sender, EventArgs e)
        {
            Agregar_empleado_permisos.SeGuardo = false;
            this.Close();
        }
    }
}
