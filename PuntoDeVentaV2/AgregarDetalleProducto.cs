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

        // Variables para mostrar el Proveedor asignado
        public string IDProveedorAsignado { get; set; }
        static public string IDProveedorAsignadoFinal = "";

        // Variable para saber si es para mostrar o no proveedor asignado
        public int typeDatoProveedor { get; set; }
        static public int typeDatoProveedorFinal;

        string queryProveedor;
        DataTable dtProveedor;
        DataRow rowProveedor;

        private void cargarDatosProveedor()
        {
            IDProveedorAsignadoFinal = IDProveedorAsignado;
            typeDatoProveedorFinal = typeDatoProveedor;

            if (typeDatoProveedorFinal == 1)
            {
                panelDatos.Visible = false;
                cbProveedores.SelectedIndex = 0;
            }
            if (typeDatoProveedorFinal == 2)
            {
                panelDatos.Visible = true;
                queryProveedor = $"SELECT * FROM Proveedores WHERE ID = '{IDProveedorAsignadoFinal}'";
                dtProveedor = cn.CargarDatos(queryProveedor);
                rowProveedor = dtProveedor.Rows[0];
                lblNombreProveedor.Text = rowProveedor["Nombre"].ToString();
                lblRFCProveedor.Text = rowProveedor["RFC"].ToString();
                lblCalleProveedor.Text = rowProveedor["Calle"].ToString();
                lblNoExtProveedor.Text = rowProveedor["NoExterior"].ToString();
                lblNoInterProveedor.Text = rowProveedor["NoInterior"].ToString();
                lblColoniaProveedor.Text = rowProveedor["Colonia"].ToString();
                lblMunicipioProveedor.Text = rowProveedor["Municipio"].ToString();
                lblEstadoProveedor.Text = rowProveedor["Estado"].ToString();
                lblCPProveedor.Text = rowProveedor["CodigoPostal"].ToString();
                lblEmailProveedor.Text = rowProveedor["Email"].ToString();
                lblTelProveedor.Text = rowProveedor["Telefono"].ToString();

                cbProveedores.Text = rowProveedor["Nombre"].ToString();
            }
        }

        //Almacenar lista de proveedores del usuario
        string[] listaProveedores = new string[] { };

        public AgregarDetalleProducto()
        {
            InitializeComponent();
        }

        private void AgregarDetalleProducto_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            cargarDatosProveedor();
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


                listaOpciones.SetItemChecked(0, true);
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

                    if (listaProveedores.Length == 0)
                    {
                        AgregarProveedor ap = new AgregarProveedor();

                        ap.FormClosed += delegate
                        {
                            CargarProveedores();
                            cbProveedores.SelectedIndex = 0;
                        };

                        ap.ShowDialog();
                    }
                }
                else
                {
                    lbProveedor.Visible = false;
                    cbProveedores.Visible = false;
                }
            }
        }

        private void btnGuardarDetalles_Click(object sender, EventArgs e)
        {
            string detalles = null;

            foreach (int indice in listaOpciones.CheckedIndices)
            {
                //Proveedor
                if (indice == 0)
                {
                    detalles += cbProveedores.SelectedValue + "-" + cbProveedores.Text;// + "|";
                }
            }

            if (detalles != null)
            {
                //detalles = detalles.Remove(detalles.Length - 1);

                AgregarEditarProducto.detallesProducto = detalles;
            }

            this.Hide();
        }
    }
}
