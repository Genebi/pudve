﻿using System;
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
    public partial class Clientes : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        private Paginar paginar;
        string DataMemberDGV = "Clientes";
        int maximo_x_pagina = 17;
        int clickBoton = 0;

       public static int editarFor { get; set; } 

        // Permisos botones
        int opcion1 = 1; // Boton buscar
        int opcion2 = 1; // Nuevo tipo cliente
        int opcion3 = 1; // Listado tipo cliente
        int opcion4 = 1; // Nuevo cliente

        IEnumerable<AgregarCliente> FormCliente = Application.OpenForms.OfType<AgregarCliente>();

        public Clientes()
        {
            InitializeComponent();
        }

        private void Clientes_Load(object sender, EventArgs e)
        {
            CargarDatos();

            if (FormPrincipal.id_empleado > 0)
            {
                var permisos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Clientes");

                opcion1 = permisos[0];
                opcion2 = permisos[1];
                opcion3 = permisos[2];
                opcion4 = permisos[3];
            }
        }

        private void CargarDatos(string busqueda = "")
        {
            this.Focus();
            var consulta = string.Empty;

            if (string.IsNullOrWhiteSpace(busqueda))
            {
                consulta = $"SELECT * FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1";
            }
            else
            {
                var extra = $"AND (RazonSocial LIKE '%{busqueda}%' OR NombreComercial LIKE '%{busqueda}%' OR RFC LIKE '%{busqueda}%')";

                consulta = $"SELECT * FROM Clientes WHERE IDUsuario = {FormPrincipal.userID} AND Status = 1 {extra}";
            }

            if (DGVClientes.Rows.Count.Equals(0) || clickBoton.Equals(0))
            {
                paginar = new Paginar(consulta, DataMemberDGV, maximo_x_pagina);
            }

            DGVClientes.Rows.Clear();

            DataSet datos = paginar.cargar();
            DataTable dtDatos = datos.Tables[0];

            foreach (DataRow fila in dtDatos.Rows)
            {
                int rowId = DGVClientes.Rows.Add();

                DataGridViewRow row = DGVClientes.Rows[rowId];

                var tipoClienteAux = Convert.ToInt16(fila["TipoCliente"]);
                var tipoCliente = string.Empty;

                var datosTipoCliente = mb.ObtenerTipoCliente(tipoClienteAux);
                if (datosTipoCliente.Length > 0)
                {
                    tipoCliente = datosTipoCliente[0];
                }
                else
                {
                    tipoCliente = "N/A";
                }

                row.Cells["ID"].Value = fila["ID"];
                row.Cells["RFC"].Value = fila["RFC"];
                row.Cells["Cliente"].Value = fila["RazonSocial"];
                row.Cells["NombreComercial"].Value = fila["NombreComercial"];
                row.Cells["Tipo"].Value = tipoCliente;
                row.Cells["NoCliente"].Value = fila["NumeroCliente"];
                row.Cells["Fecha"].Value = Convert.ToDateTime(fila["FechaOperacion"]).ToString("yyyy-MM-dd HH:mm:ss");
                var idClientesPorID = row.Cells["ID"].Value = fila["ID"];/////////////////////////////////////////////////////////////////

                Image editar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\edit.png");
                Image eliminar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\remove.png");

                row.Cells["Editar"].Value = editar;
                row.Cells["Eliminar"].Value = eliminar;
            }

            clickBoton = 0;

            DGVClientes.ClearSelection();

            ActualizarPaginador();
        }

        private void btnNuevoCliente_Click(object sender, EventArgs e)
        {
            if (opcion4 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (FormCliente.Count() == 1)
            {
                if (FormCliente.First().WindowState == FormWindowState.Normal)
                {
                    FormCliente.First().BringToFront();
                }

                if (FormCliente.First().WindowState == FormWindowState.Minimized)
                {
                    FormCliente.First().WindowState = FormWindowState.Normal;
                }
            }
            else
            {
                AgregarCliente cliente = new AgregarCliente();

                cliente.FormClosed += delegate
                {
                    CargarDatos();
                };

                cliente.Show();
            }
        }

        private void DGVClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idCliente = Convert.ToInt32(DGVClientes.Rows[e.RowIndex].Cells["ID"].Value);

                //Editar cliente
                if (e.ColumnIndex == 7)
                {
                    AgregarCliente editar = new AgregarCliente(2, idCliente);
                    editarFor = 0;

                    editar.FormClosed += delegate
                    {
                        CargarDatos();
                        editarFor = 1;
                    };

                    editar.ShowDialog();
                }

                //Eliminar cliente
                if (e.ColumnIndex == 8)
                {
                    var respuesta = MessageBox.Show("¿Estás seguro de deshabilitar este cliente?", "Mensaje del sistema", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (respuesta == DialogResult.Yes)
                    {
                        string[] datos = new string[] { idCliente.ToString(), FormPrincipal.userID.ToString() };

                        int resultado = cn.EjecutarConsulta(cs.GuardarCliente(datos, 2));

                        if (resultado > 0)
                        {
                            CargarDatos();
                        }
                    }
                }

                DGVClientes.ClearSelection();
            }
        }

        private void DGVClientes_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex >= 7)
                {
                    DGVClientes.Cursor = Cursors.Hand;
                }
            }
        }

        private void DGVClientes_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex >= 7)
                {
                    DGVClientes.Cursor = Cursors.Default;
                }
            }
        }

        public void btnTipoCliente_Click(object sender, EventArgs e)
        {
            if (opcion2 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<AgregarTipoCliente>().Count() == 1)
            {
                Application.OpenForms.OfType<AgregarTipoCliente>().First().BringToFront();
            }
            else
            {
                var tipoCliente = new AgregarTipoCliente();
                tipoCliente.Show();
            }
        }

        private void btnListaDescuentos_Click(object sender, EventArgs e)
        {
            if (opcion3 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<ListadoTipoClientes>().Count() == 1)
            {
                Application.OpenForms.OfType<ListadoTipoClientes>().First().BringToFront();
            }
            else
            {
                var existen = mb.ObtenerTipoClientes();

                if (existen.Count > 0)
                {
                    var listado = new ListadoTipoClientes();

                    listado.Show();
                    listado.FormClosed += delegate
                    {
                        CargarDatos();
                    };
                }
                else
                {
                    MessageBox.Show("No hay información disponible actualmente\n\nNOTA: No hay registros para tipo de clientes", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } 
        }

        private void ActualizarPaginador()
        {
            this.Focus();
            int BeforePage = 0, AfterPage = 0, LastPage = 0;

            linkLblPaginaAnterior.Visible = false;
            linkLblPaginaSiguiente.Visible = false;

            linkLblPaginaActual.Text = paginar.numPag().ToString();
            linkLblPaginaActual.LinkColor = Color.White;
            linkLblPaginaActual.BackColor = Color.Black;

            BeforePage = paginar.numPag() - 1;
            AfterPage = paginar.numPag() + 1;
            LastPage = paginar.countPag();

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

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            paginar.primerPagina();
            clickBoton = 1;
            CargarDatos();
            ActualizarPaginador();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            paginar.atras();
            clickBoton = 1;
            CargarDatos();
            ActualizarPaginador();
        }

        private void linkLblPaginaAnterior_Click(object sender, EventArgs e)
        {
            paginar.atras();
            clickBoton = 1;
            CargarDatos();
            ActualizarPaginador();
        }

        private void linkLblPaginaActual_Click(object sender, EventArgs e)
        {
            ActualizarPaginador();
        }

        private void linkLblPaginaSiguiente_Click(object sender, EventArgs e)
        {
            paginar.adelante();
            clickBoton = 1;
            CargarDatos();
            ActualizarPaginador();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            paginar.adelante();
            clickBoton = 1;
            CargarDatos();
            ActualizarPaginador();
        }

        private void btnUltimaPagina_Click(object sender, EventArgs e)
        {
            paginar.ultimaPagina();
            clickBoton = 1;
            CargarDatos();
            ActualizarPaginador();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (opcion1 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            var busqueda = txtBuscador.Text.Trim();

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                txtBuscador.SelectionStart = 0;
                txtBuscador.SelectionLength = txtBuscador.Text.Length;
            }

            CargarDatos(busqueda);
        }

        private void txtBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnNuevoCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnListaDescuentos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnTipoCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void DGVClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void Clientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }
    }
}
