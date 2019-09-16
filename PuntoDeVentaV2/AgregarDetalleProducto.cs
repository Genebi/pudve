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

        List<string> optionList;

        string[] listaProveedores = new string[] { };
        string[] listaCategorias = new string[] { };
        string[] listaUbicaciones = new string[] { };

        bool habilitarComboBoxes = false;

        public AgregarDetalleProducto()
        {
            InitializeComponent();
        }

        private void AgregarDetalleProducto_Load(object sender, EventArgs e)
        {
            CargarProveedores();
            CargarCategorias();
            CargarUbicaciones();

            checkProveedor.Checked = Properties.Settings.Default.checkProveedor;
            checkCategoria.Checked = Properties.Settings.Default.checkCategoria;
            checkUbicacion.Checked = Properties.Settings.Default.checkUbicacion;

            chkProveedor.Checked = Properties.Settings.Default.checkShowProveedor;
            chkCategoria.Checked = Properties.Settings.Default.checkShowCategoria;
            chkUbicacion.Checked = Properties.Settings.Default.checkShowUbicacion;

            VisualizarContentido();
            //verificarCheckboxLista();
        }

        //private void verificarCheckboxLista()
        //{
        //    optionList = new List<string>();
        //    foreach (Control cComponente in panelMenu.Controls)
        //    {
        //        if (cComponente is CheckBox)
        //        {
        //            CheckBox chk;
        //            chk = (CheckBox)cComponente;
        //            if (chk.Checked == true)
        //            {
        //                optionList.Add(chk.Text+"-"+chk.Checked.ToString());
        //            }
        //        }
        //    }
        //}

        private void cargarDatosProveedor(int idProveedor)
        {
            //Para que no de error ya que nunca va a existir un proveedor con ID = 0
            if (idProveedor > 0)
            {
                var datos = mb.ObtenerDatosProveedor(idProveedor, FormPrincipal.userID);

                panelProveedor.Visible = true;
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
                cbProveedores.SelectedValue = "0";

                // Cuando se da click en la opcion editar producto
                if (AgregarEditarProducto.DatosSourceFinal == 2)
                {
                    var idProducto = Convert.ToInt32(AgregarEditarProducto.idProductoFinal);
                    var idProveedor = mb.DetallesProducto(idProducto, FormPrincipal.userID);

                    //MessageBox.Show(idProveedor[0].ToString());
                    if (idProveedor.Length > 0)
                    {
                        if (Convert.ToInt32(idProveedor[0].ToString()) > 0)
                        {
                            cbProveedores.SelectedValue = idProveedor[0];
                            cargarDatosProveedor(Convert.ToInt32(idProveedor[0]));
                        }
                    }
                    else
                    {
                        cbProveedores.SelectedValue = "0";
                    }

                    //cbProveedores_SelectedIndexChanged(this, EventArgs.Empty);
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

                // Cuando se da click en la opcion editar producto
                if (AgregarEditarProducto.DatosSourceFinal == 2)
                {
                    var idProducto = Convert.ToInt32(AgregarEditarProducto.idProductoFinal);
                    var idCategoria = mb.DetallesProducto(idProducto, FormPrincipal.userID);

                    if (idCategoria.Length > 0)
                    {
                        if (Convert.ToInt32(idCategoria[2].ToString()) > 0)
                        {
                            cbCategorias.SelectedValue = idCategoria[2];
                        }
                    }
                    else
                    {
                        cbCategorias.SelectedValue = "0";
                    }
                }
            }
            else
            {
                cbCategorias.Items.Add("Seleccionar una categoría...");
                cbCategorias.SelectedIndex = 0;
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

                // Cuando se da click en la opcion editar producto
                if (AgregarEditarProducto.DatosSourceFinal == 2)
                {
                    var idProducto = Convert.ToInt32(AgregarEditarProducto.idProductoFinal);
                    var idUbicacion = mb.DetallesProducto(idProducto, FormPrincipal.userID);

                    if (idUbicacion.Length > 0)
                    {
                        if (Convert.ToInt32(idUbicacion[4].ToString()) > 0)
                        {
                            cbUbicaciones.SelectedValue = idUbicacion[4];
                        }
                    }
                    else
                    {
                        cbUbicaciones.SelectedValue = "0";
                    }
                }
            }
            else
            {
                cbUbicaciones.Items.Add("Seleccionar una ubicación...");
                cbUbicaciones.SelectedIndex = 0;
            }
        }

        private void btnGuardarDetalles_Click(object sender, EventArgs e)
        {
            if (checkProveedor.Checked)
            {
                if (Convert.ToInt32(cbProveedores.SelectedValue.ToString()) > 0)
                {
                    AgregarEditarProducto.infoProveedor = cbProveedores.SelectedValue + "|" + cbProveedores.Text;
                } 
            }

            if (checkCategoria.Checked)
            {
                if (Convert.ToInt32(cbCategorias.SelectedValue.ToString()) > 0)
                {
                    AgregarEditarProducto.infoCategoria = cbCategorias.SelectedValue + "|" + cbCategorias.Text;
                } 
            }

            if (checkUbicacion.Checked)
            {
                if (Convert.ToInt32(cbUbicaciones.SelectedValue.ToString()) > 0)
                {
                    AgregarEditarProducto.infoUbicacion = cbUbicaciones.SelectedValue + "|" + cbUbicaciones.Text;
                }  
            }

            this.Hide();
        }

        private void btnAgregarProveedor_Click(object sender, EventArgs e)
        {
            habilitarComboBoxes = false;

            AgregarProveedor ap = new AgregarProveedor();

            ap.FormClosed += delegate
            {
                CargarProveedores();

                habilitarComboBoxes = true;
            };

            ap.ShowDialog();
        }

        private void btnAgregarCategoria_Click(object sender, EventArgs e)
        {
            habilitarComboBoxes = false;

            AgregarCategoria nuevaCategoria = new AgregarCategoria();

            nuevaCategoria.FormClosed += delegate
            {
                CargarCategorias();

                habilitarComboBoxes = true;
            };

            nuevaCategoria.ShowDialog();
        }

        private void btnAgregarUbicacion_Click(object sender, EventArgs e)
        {
            habilitarComboBoxes = false;

            AgregarUbicacion nuevaUbicacion = new AgregarUbicacion();

            nuevaUbicacion.FormClosed += delegate
            {
                CargarUbicaciones();

                habilitarComboBoxes = true;
            };

            nuevaUbicacion.ShowDialog();
        }

        private void checkProveedor_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.checkProveedor = checkProveedor.Checked;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
            VisualizarContentido();
        }

        private void checkCategoria_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.checkCategoria = checkCategoria.Checked;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
            VisualizarContentido();
        }

        private void checkUbicacion_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.checkUbicacion = checkUbicacion.Checked;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
            VisualizarContentido();
        }

        private void VisualizarContentido()
        {
            if (checkProveedor.Checked)
            {
                panelProveedor.Visible = true;
            }
            else
            {
                panelProveedor.Visible = false;
            }

            if (checkCategoria.Checked)
            {
                panelCategoria.Visible = true;
            }
            else
            {
                panelCategoria.Visible = false;
            }

            if (checkUbicacion.Checked)
            {
                panelUbicacion.Visible = true;
            }
            else
            {
                panelUbicacion.Visible = false;
            }
        }

        private void cbProveedores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (habilitarComboBoxes)
            {
                if (listaProveedores.Length > 0)
                {
                    var idProveedor = Convert.ToInt32(cbProveedores.SelectedValue.ToString());

                    if (idProveedor > 0)
                    {
                        cargarDatosProveedor(Convert.ToInt32(idProveedor));
                        lblNombreProveedor.Visible = true;
                        lblRFCProveedor.Visible = true;
                        lblTelProveedor.Visible = true;
                    }
                    else
                    {
                        lblNombreProveedor.Visible = false;
                        lblRFCProveedor.Visible = false;
                        lblTelProveedor.Visible = false;
                    }
                }
            }
        }

        private void cbCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (habilitarComboBoxes)
            {
                if (listaCategorias.Length > 0)
                {
                    var opcion = Convert.ToInt32(cbCategorias.SelectedValue.ToString());

                    if (opcion > 0)
                    {
                        lbNombreCategoria.Text = cbCategorias.GetItemText(cbCategorias.SelectedItem);
                        lbNombreCategoria.Visible = true;
                    }
                    else
                    {
                        lbNombreCategoria.Visible = false;
                    }
                }
            }
        }

        private void cbUbicaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (habilitarComboBoxes)
            {
                if (listaUbicaciones.Length > 0)
                {
                    var opcion = Convert.ToInt32(cbUbicaciones.SelectedValue.ToString());

                    if (opcion > 0)
                    {
                        lbNombreUbicacion.Text = cbUbicaciones.GetItemText(cbUbicaciones.SelectedItem);
                        lbNombreUbicacion.Visible = true;
                    }
                    else
                    {
                        lbNombreUbicacion.Visible = false;
                    }
                }
            }
        }

        private void AgregarDetalleProducto_Shown(object sender, EventArgs e)
        {
            habilitarComboBoxes = true;

            if (AgregarEditarProducto.DatosSourceFinal == 2)
            {
                cbProveedores_SelectedIndexChanged(this, EventArgs.Empty);
                cbCategorias_SelectedIndexChanged(this, EventArgs.Empty);
                cbUbicaciones_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        private void chkProveedor_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.checkShowProveedor = chkProveedor.Checked;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void chkCategoria_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.checkShowCategoria = chkCategoria.Checked;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void chkUbicacion_CheckedChanged(object sender, EventArgs e)
        {
            Properties.Settings.Default.checkShowUbicacion = chkUbicacion.Checked;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }
    }
}
