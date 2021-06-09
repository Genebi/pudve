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
    public partial class RegistroIniciosDeSesiones : Form
    {

        MetodosBusquedas mb = new MetodosBusquedas();
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        string busqueda = string.Empty;
        string filtro = string.Empty;
        private Paginar p;
        string DataMemberDGV = "iniciosdesesion";
        int maximo_x_pagina = 12;
        int clickBoton = 0;
        private List<string> propiedades = new List<string>();
        string filtroConSinFiltroAvanzado = string.Empty;

        public RegistroIniciosDeSesiones()
        {
            InitializeComponent();
        }

        private void RegistroIniciosDeSesiones_Load(object sender, EventArgs e)
        {
            filtroLoadProductos();
            CargarDatos();
            this.Focus();
        }

        private void CargarDatos(string busquedaEnEmpleados = "")
        {
            busqueda = string.Empty;

            busqueda = busquedaEnEmpleados;

            if (dgvInicios.RowCount <= 0)
            {
                if (busqueda == "")
                {
                    filtro = cs.RegistroIniciosDeSesiones();

                    p = new Paginar(filtro, DataMemberDGV, maximo_x_pagina);
                }
                else if (busqueda != "")
                {
                    filtro = cs.busquedaIniciosDeSesion(busqueda);

                    p = new Paginar(filtro, DataMemberDGV, maximo_x_pagina);
                }
            }
            else if (dgvInicios.RowCount >= 1 && clickBoton == 0)
            {
                if (busqueda == "")
                {
                    filtro = cs.RegistroIniciosDeSesiones();

                    p = new Paginar(filtro, DataMemberDGV, maximo_x_pagina);
                }
                else if (busqueda != "")
                {
                    filtro = cs.busquedaIniciosDeSesion(busqueda);

                    p = new Paginar(filtro, DataMemberDGV, maximo_x_pagina);
                }
            }

            DataSet datos = p.cargar();
            DataTable dtDatos = datos.Tables[0];
            dgvInicios.Rows.Clear();

            foreach (DataRow filaDatos in dtDatos.Rows)
            {
                var numeroFilas = dgvInicios.Rows.Count;

                string ID = filaDatos["ID"].ToString();
                string Nombre = filaDatos["Usuario"].ToString();
                string Usuario = filaDatos["Fecha"].ToString();


                if (dgvInicios.Rows.Count.Equals(0))
                {
                    var number_of_rows = dgvInicios.Rows.Add();
                    DataGridViewRow row = dgvInicios.Rows[number_of_rows];
                    row.Cells["id"].Value = ID;
                    row.Cells["usuario"].Value = Nombre;
                    row.Cells["fecha"].Value = Usuario;
                }
                else if (!dgvInicios.Rows.Count.Equals(0))
                {
                    var number_of_rows = dgvInicios.Rows.Add();
                    DataGridViewRow row = dgvInicios.Rows[number_of_rows];
                    row.Cells["id"].Value = ID;
                    row.Cells["usuario"].Value = Nombre;
                    row.Cells["fecha"].Value = Usuario;

                }
            }

            actualizar();
            clickBoton = 0;
        }

        private void actualizar()
        {
            int BeforePage = 0, AfterPage = 0, LastPage = 0;

            linkLblPaginaAnterior.Visible = false;
            linkLblPaginaSiguiente.Visible = false;

            lblCantidadRegistros.Text = p.countRow().ToString();

            linkLblPaginaActual.Text = p.numPag().ToString();
            linkLblPaginaActual.LinkColor = System.Drawing.Color.White;
            linkLblPaginaActual.BackColor = System.Drawing.Color.Black;

            BeforePage = p.numPag() - 1;
            AfterPage = p.numPag() + 1;
            LastPage = p.countPag();

            if (Convert.ToInt32(linkLblPaginaActual.Text) >= 2)
            {
                linkLblPaginaAnterior.Text = BeforePage.ToString();
                linkLblPaginaAnterior.Visible = true;
                if (AfterPage <= LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = true;
                }
                else if (AfterPage > LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = false;
                }
            }
            else if (BeforePage < 1)
            {
                linkLblPrimeraPagina.Visible = false;
                linkLblPaginaAnterior.Visible = false;
                if (AfterPage <= LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = true;
                }
                else if (AfterPage > LastPage)
                {
                    linkLblPaginaSiguiente.Text = AfterPage.ToString();
                    linkLblPaginaSiguiente.Visible = false;
                    linkLblUltimaPagina.Visible = false;
                }
            }

            txtMaximoPorPagina.Text = p.limitRow().ToString();
        }

        private void filtroLoadProductos()
        {
            busqueda = "1";

            filtroConSinFiltroAvanzado = cs.RegistroIniciosDeSesiones();

            p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);

            dgvInicios.Rows.Clear();
            CargarDatos();
        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            string tipo = string.Empty;
            CargarDatos();
            actualizar();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            string tipo = string.Empty;
            CargarDatos();
            actualizar();
        }

        private void linkLblPaginaAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            string tipo = string.Empty;
            CargarDatos();
            actualizar();
        }

        private void linkLblPaginaActual_Click(object sender, EventArgs e)
        {
            actualizar();
        }

        private void linkLblPaginaSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            string tipo = string.Empty;
            CargarDatos();
            actualizar();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            string tipo = string.Empty;
            CargarDatos();
            actualizar();
        }

        private void btnUltimaPagina_Click(object sender, EventArgs e)
        {
            p.ultimaPagina();
            clickBoton = 1;
            string tipo = string.Empty;
            CargarDatos();
            actualizar();
        }

        private void txtMaximoPorPagina_Click(object sender, EventArgs e)
        {
            if (!txtMaximoPorPagina.Text.Equals(string.Empty))
            {
                maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
                p.actualizarTope(maximo_x_pagina);
                string tipo = string.Empty;
                CargarDatos();
                actualizar();
            }
            else
            {
                txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
            }
        }

        private void txtMaximoPorPagina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!txtMaximoPorPagina.Text.Equals(String.Empty))
                {
                    maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
                    p.actualizarTope(maximo_x_pagina);
                    string tipo = string.Empty;
                    CargarDatos();
                    actualizar();
                }
                else
                {
                    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                }

            }
        }

        private void txtMaximoPorPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void btnActualizarMaximoProductos_Click(object sender, EventArgs e)
        {
            if (!txtMaximoPorPagina.Text.Equals(string.Empty))
            {
                maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
                p.actualizarTope(maximo_x_pagina);
                string tipo = string.Empty;
                CargarDatos();
                actualizar();
            }
            else
            {
                txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
            }
        }
    }
}
