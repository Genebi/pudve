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
    public partial class AgregarDetalleProducto : Form
    {
        Conexion cn = new Conexion();

        //Almacenar lista de proveedores del usuario
        string[] listaProveedores = new string[] { };

        public AgregarDetalleProducto()
        {
            InitializeComponent();
        }

        private void AgregarDetalleProducto_Load(object sender, EventArgs e)
        {
            //Asignamos el array con los nombres de los proveedores al combobox
            listaProveedores = cn.ObtenerProveedores(FormPrincipal.userID);
            cbProveedores.Items.AddRange(listaProveedores);
            cbProveedores.SelectedIndex = 0;
        }

        private void listaOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indice = listaOpciones.SelectedIndex;

            //Proveedor
            if (indice == 0)
            {
                if (listaOpciones.GetItemChecked(indice) == true)
                {
                    lbProveedor.Visible = true;
                    cbProveedores.Visible = true;
                }
                else
                {
                    lbProveedor.Visible = false;
                    cbProveedores.Visible = false;
                }
            }
        }
    }
}
