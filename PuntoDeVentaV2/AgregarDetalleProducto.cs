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
        MetodosBusquedas mb = new MetodosBusquedas();

        string[] listaProveedores = new string[] { };
        string[] listaCategorias = new string[] { };
        string[] listaUbicaciones = new string[] { };

        public AgregarDetalleProducto()
        {
            InitializeComponent();
        }

        private void AgregarDetalleProducto_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            CargarCategorias();
            CargarUbicaciones();
        }

        private void cargarDatosProveedor(int idProveedor)
        {
            //Para que no de error ya que nunca va a existir un proveedor con ID = 0
            if (idProveedor > 0)
            {
                var datos = mb.ObtenerDatosProveedor(idProveedor, FormPrincipal.userID);

                panelDatosProveedor.Visible = true;
                lblNombreProveedor.Text = datos[0];
                lblRFCProveedor.Text = datos[1];
                lblTelProveedor.Text = datos[10];
                cbProveedores.Text = datos[0];
            } 
        }

        private void CargarProveedores()
        {
            //Asignamos el array con los nombres de los proveedores al combobox
            listaProveedores = cn.ObtenerProveedores(FormPrincipal.userID);

            //Comprobar que ya exista al menos un proveedor
            if (listaProveedores.Length > 0)
            {
                Dictionary<string, string> proveedores = new Dictionary<string, string>();

                proveedores.Add("0", "Seleccionar un proveedor...");

                foreach (var proveedor in listaProveedores)
                {
                    var tmp = proveedor.Split('-');

                    proveedores.Add(tmp[0].Trim(), tmp[1].Trim());
                }

                cbProveedores.DataSource = proveedores.ToArray();
                cbProveedores.DisplayMember = "Value";
                cbProveedores.ValueMember = "Key";

                lbProveedor.Visible = true;
                cbProveedores.Visible = true;
                cbProveedores.SelectedValue = "0";

                //Cuando se da click en la opcion editar producto
                if (AgregarEditarProducto.DatosSourceFinal == 2)
                {
                    var idProducto = Convert.ToInt32(AgregarEditarProducto.idProductoFinal);
                    var idProveedor = mb.ObtenerIDProveedorProducto(idProducto, FormPrincipal.userID);

                    if (!string.IsNullOrEmpty(idProveedor))
                    {
                        cbProveedores.SelectedValue = idProveedor;
                        cargarDatosProveedor(Convert.ToInt32(idProveedor));
                    }
                    else
                    {
                        cbProveedores.SelectedValue = "0";
                    }
                }
            }
            else
            {
                if (cbProveedores.Items.Count == 0)
                {
                    cbProveedores.Items.Add("Seleccionar un proveedor...");
                    cbProveedores.SelectedIndex = 0;
                } 
            }
        }

        private void CargarCategorias()
        {
            listaCategorias = mb.ObtenerCategorias(FormPrincipal.userID);

            if (listaCategorias.Length > 0)
            {
                Dictionary<string, string> categorias = new Dictionary<string, string>();

                categorias.Add("0", "Seleccionar una categoría...");

                foreach (var categoria in listaCategorias)
                {
                    var auxiliar = categoria.Split('|');

                    categorias.Add(auxiliar[0], auxiliar[1]);
                }

                cbCategorias.DataSource = categorias.ToArray();
                cbCategorias.DisplayMember = "Value";
                cbCategorias.ValueMember = "Key";
            }
            else
            {
                cbCategorias.Items.Add("Seleccionar una categoría...");
            }
        }

        private void CargarUbicaciones()
        {
            listaUbicaciones = mb.ObtenerUbicaciones(FormPrincipal.userID);

            if (listaUbicaciones.Length > 0)
            {
                Dictionary<string, string> ubicaciones = new Dictionary<string, string>();

                ubicaciones.Add("0", "Seleccionar una ubicación...");

                foreach (var ubicacion in listaUbicaciones)
                {
                    var auxiliar = ubicacion.Split('|');

                    ubicaciones.Add(auxiliar[0], auxiliar[1]);
                }

                cbUbicaciones.DataSource = ubicaciones.ToArray();
                cbUbicaciones.DisplayMember = "Value";
                cbUbicaciones.ValueMember = "Key";
            }
            else
            {
                cbUbicaciones.Items.Add("Seleccionar una ubicación...");
            }
        }

        private void btnGuardarDetalles_Click(object sender, EventArgs e)
        {
            string detalles = null;

            if (checkProveedor.Checked)
            {
                detalles += cbProveedores.SelectedValue + "-" + cbProveedores.Text;
            }


            if (detalles != null)
            {
                //detalles = detalles.Remove(detalles.Length - 1);

                AgregarEditarProducto.detallesProducto = detalles;

                MessageBox.Show(detalles);
            }

            //this.Hide();
        }

        private void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            AgregarProveedor ap = new AgregarProveedor();

            ap.FormClosed += delegate
            {
                MessageBox.Show("Se cerro el form de proovedor");
            };

            ap.ShowDialog();
        }

        private void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            AgregarCategoria nuevaCategoria = new AgregarCategoria();

            nuevaCategoria.FormClosed += delegate
            {
                MessageBox.Show("Se cerro el form de categoria");
            };

            nuevaCategoria.ShowDialog();
        }

        private void btnAgregarUbicacion_Click(object sender, EventArgs e)
        {
            AgregarUbicacion nuevaUbicacion = new AgregarUbicacion();

            nuevaUbicacion.FormClosed += delegate
            {
                MessageBox.Show("Se cerro el form de ubicacion");
            };

            nuevaUbicacion.ShowDialog();
        }
    }
}
