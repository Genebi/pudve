using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class Proveedores : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        private Paginar p;
        string DataMemberDGV = "Proveedores";
        int maximo_x_pagina = 4;

        public Proveedores()
        {
            InitializeComponent();
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
            CargarDatos();
            ActualizarPaginador();
        }

        private void CargarDatos()
        {
            var consulta = $"SELECT * FROM Proveedores WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1";

            p = new Paginar(consulta, DataMemberDGV, maximo_x_pagina);

            DGVProveedores.Rows.Clear();

            DataSet datos = p.cargar();
            DataTable dtDatos = datos.Tables[0];

            Image editar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\edit.png");
            Image eliminar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\remove.png");

            foreach (DataRow fila in dtDatos.Rows)
            {
                int rowId = DGVProveedores.Rows.Add();

                DataGridViewRow row = DGVProveedores.Rows[rowId];

                row.Cells["ID"].Value = fila["ID"].ToString();
                row.Cells["Nombre"].Value = fila["Nombre"].ToString();
                row.Cells["RFC"].Value = fila["RFC"].ToString();
                row.Cells["Email"].Value = fila["Email"].ToString();
                row.Cells["Telefono"].Value = fila["Telefono"].ToString();
                row.Cells["Fecha"].Value = Convert.ToDateTime(fila["FechaOperacion"]).ToString("yyyy-MM-dd HH:mm:ss");
                row.Cells["Editar"].Value = editar;
                row.Cells["Eliminar"].Value = eliminar;
            }

            //DGVProveedores.ClearSelection();

            ActualizarPaginador();
        }


        private void ActualizarPaginador()
        {
            int BeforePage = 0, AfterPage = 0, LastPage = 0;

            linkLblPaginaAnterior.Visible = false;
            linkLblPaginaSiguiente.Visible = false;

            linkLblPaginaActual.Text = p.numPag().ToString();
            linkLblPaginaActual.LinkColor = Color.White;
            linkLblPaginaActual.BackColor = Color.Black;

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
        }

        private void btnNuevoProveedor_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms.OfType<AgregarProveedor>().Count() == 1)
            {
                Application.OpenForms.OfType<AgregarProveedor>().First().BringToFront();
            }
            else
            {
                AgregarProveedor ap = new AgregarProveedor();

                ap.FormClosed += delegate
                {
                    CargarDatos();
                };

                ap.Show();
            }
        }

        private void DGVProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idProveedor = Convert.ToInt32(DGVProveedores.Rows[e.RowIndex].Cells["ID"].Value);

                //Editar
                if (e.ColumnIndex == 6)
                {
                    AgregarProveedor editar = new AgregarProveedor(2, idProveedor);

                    editar.FormClosed += delegate
                    {
                        CargarDatos();
                    };

                    editar.ShowDialog();
                }

                //Eliminar
                if (e.ColumnIndex == 7)
                {
                    var respuesta = MessageBox.Show("¿Estás seguro de deshabilitar este proveedor?", "Mensaje del Sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        string[] datos = new string[] { idProveedor.ToString(), FormPrincipal.userID.ToString() };

                        int resultado = cn.EjecutarConsulta(cs.GuardarProveedor(datos, 2));

                        if (resultado > 0)
                        {
                            CargarDatos();
                        }
                    }
                }

                DGVProveedores.ClearSelection();
            }
        }

        private void DGVProveedores_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex >= 6)
                {
                    DGVProveedores.Cursor = Cursors.Hand;
                }
            }
        }

        private void DGVProveedores_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex >= 6)
                {
                    DGVProveedores.Cursor = Cursors.Default;
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var busqueda = txtBuscador.Text.Trim();

            if (string.IsNullOrWhiteSpace(busqueda))
            {
                return;
            }

            MessageBox.Show(busqueda);
        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            p.primerPagina();
            CargarDatos();
            ActualizarPaginador();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            CargarDatos();
            ActualizarPaginador();
        }

        private void linkLblPaginaAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            CargarDatos();
            ActualizarPaginador();
        }

        private void linkLblPaginaActual_Click(object sender, EventArgs e)
        {
            ActualizarPaginador();
        }

        private void linkLblPaginaSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            CargarDatos();
            ActualizarPaginador();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            CargarDatos();
            ActualizarPaginador();
        }

        private void btnUltimaPagina_Click(object sender, EventArgs e)
        {
            p.ultimaPagina();
            CargarDatos();
            ActualizarPaginador();
        }
    }
}
