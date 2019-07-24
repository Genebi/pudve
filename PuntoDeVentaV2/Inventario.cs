using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class Inventario : Form
    {
        Conexion cn = new Conexion();
        MetodosBusquedas mb = new MetodosBusquedas();

        RevisarInventario checkInventory = new RevisarInventario();
        ReporteFinalRevisarInventario FinalReportReviewInventory = new ReporteFinalRevisarInventario();

        public static int NumRevActivo;

        //Para cargar la informacion de los productos al buscador autocompletador
        NameValueCollection datos;
        string[] datosProducto;
        string[] productos;

        public int GetNumRevActive { get; set; }

        private void CargarNumRevActivo()
        {
            NumRevActivo = GetNumRevActive;
        }

        public Inventario()
        {
            InitializeComponent();
        }

        private void Inventario_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        public void CargarProductos()
        {
            /*AutoCompleteStringCollection coleccion = new AutoCompleteStringCollection();
            datos = new NameValueCollection();

            //Cargar lista de productos actualmente registrados
            datos = cn.ObtenerProductos(FormPrincipal.userID);
            productos = new string[datos.Count];
            datos.CopyTo(productos, 0);

            coleccion.AddRange(productos);
            txtBusqueda.AutoCompleteCustomSource = coleccion;
            txtBusqueda.AutoCompleteMode = AutoCompleteMode.None;
            txtBusqueda.AutoCompleteSource = AutoCompleteSource.CustomSource;*/
        }

        private void btnRevisar_Click(object sender, EventArgs e)
        {
            panelContenedor.Visible = false;

            FormCollection fOpen = Application.OpenForms;
            List<string> tempFormOpen = new List<string>();

            checkInventory.FormClosing += delegate
            {
                GetNumRevActive = Convert.ToInt32(checkInventory.NoActualCheckStock);
                CargarNumRevActivo();

                FinalReportReviewInventory.FormClosed += delegate
                {
                    foreach (Form formToClose in fOpen)
                    {
                        if (formToClose.Name != "FormPrincipal" && formToClose.Name != "Login")
                        {
                            tempFormOpen.Add(formToClose.Name);
                        }
                    }

                    foreach (var toClose in tempFormOpen)
                    {
                        Form ventanaAbierta = Application.OpenForms[toClose];
                        ventanaAbierta.Close();
                    }
                };

                if (!FinalReportReviewInventory.Visible)
                {
                    try
                    {
                        FinalReportReviewInventory.GetFilterNumActiveRecord = NumRevActivo;
                        FinalReportReviewInventory.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message.ToString(), "Error al abrir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            };

            if (!checkInventory.Visible)
            {
                checkInventory.ShowDialog();
            }
            else
            {
                checkInventory.ShowDialog();
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            panelContenedor.Visible = true;

            txtBusqueda.Focus();
        }

        private void btnActualizarXML_Click(object sender, EventArgs e)
        {
            panelContenedor.Visible = false;
            MessageBox.Show("Actualizar desde XML");
        }

        private void txtBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            ocultarResultados();
            listaProductos.Items.Clear();

            //txtBuscadorProducto.Text = VerificarPatronesBusqueda(txtBuscadorProducto.Text);

            /*if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                ocultarResultados();
                return;
            }

            foreach (string s in txtBusqueda.AutoCompleteCustomSource)
            {
                if (s.Contains(txtBusqueda.Text))
                {
                    listaProductos.Items.Add(s);
                    listaProductos.Visible = true;
                    listaProductos.SelectedIndex = 0;
                }
            }*/

            timerBusqueda.Stop();
            timerBusqueda.Start();
        }

        private void RealizarBusqueda()
        {
            if (!string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                //Buscar por codigo o clave
                var datos = mb.BuscarProductoInventario(txtBusqueda.Text, FormPrincipal.userID, 1);

                if (datos.Length > 0)
                {
                    AjustarProducto ap = new AjustarProducto(Convert.ToInt32(datos[0]));

                    ap.FormClosed += delegate
                    {

                    };

                    ap.ShowDialog();
                }
                else
                {
                    //Buscar por nombre
                    datos = mb.BuscarProductoInventario(txtBusqueda.Text, FormPrincipal.userID, 2);

                    if (datos.Length > 0)
                    {
                        foreach (var producto in datos)
                        {
                            listaProductos.Items.Add(producto);
                        }

                        listaProductos.Visible = true;
                        listaProductos.SelectedIndex = 0;
                        listaProductos.Focus();
                    }
                }
            }
        }

        private void ocultarResultados()
        {
            listaProductos.Visible = false;
        }

        private void timerBusqueda_Tick(object sender, EventArgs e)
        {
            timerBusqueda.Stop();
            RealizarBusqueda();
        }

        private void Inventario_KeyDown(object sender, KeyEventArgs e)
        {
            if (listaProductos.Visible)
            {
                if (listaProductos.Items.Count == 0)
                {
                    return;
                }

                //Presiono hacia arriba
                if (e.KeyCode == Keys.Up)
                {
                    listaProductos.Focus();

                    if (listaProductos.SelectedIndex > 0)
                    {
                        listaProductos.SelectedIndex--;
                        e.Handled = true;
                    }
                }

                //Presiono hacia abajo
                if (e.KeyCode == Keys.Down)
                {
                    listaProductos.Focus();

                    if (listaProductos.SelectedIndex < (listaProductos.Items.Count - 1))
                    {
                        listaProductos.SelectedIndex++;
                        e.Handled = true;
                    }
                }
            }
        }

        private void listaProductos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ocultarResultados();
            var info = listaProductos.Items[listaProductos.SelectedIndex].ToString().Split('-');
            var idProducto = Convert.ToInt32(info[0]);

            AjustarProducto ap = new AjustarProducto(idProducto);

            ap.FormClosed += delegate
            {

            };

            ap.ShowDialog();
        }

        private void listaProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                ocultarResultados();
                var info = listaProductos.Items[listaProductos.SelectedIndex].ToString().Split('-');
                var idProducto = Convert.ToInt32(info[0]);

                AjustarProducto ap = new AjustarProducto(idProducto);

                ap.FormClosed += delegate
                {

                };

                ap.ShowDialog();
            }
        }
    }
}
