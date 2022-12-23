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
        MetodosBusquedas mb = new MetodosBusquedas();
        private Paginar p;
        string DataMemberDGV = "Proveedores";
        int maximo_x_pagina = 14;
        int clickBoton = 0;

        int datoEncontrado = 0;
        // Permisos botones 
        int opcion1 = 1; // Boton buscar
        int opcion2 = 1; // Nuevo proveedor
        int opcion3 = 1; // Deshabilitar
        int opcion4 = 1; // Habilitar

        public static bool HabilitarODeshabilitar = false;
        string mensajeParaMostrar = string.Empty;
        bool YaExiste = false;
        public Proveedores()
        {
            InitializeComponent();
        }

        private void Proveedores_Load(object sender, EventArgs e)
        {
            cbStatus.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            cbStatus.SelectedIndex = 0;

            CargarDatos();

            ActualizarPaginador();

            if (FormPrincipal.id_empleado > 0)
            {
                var permisos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Proveedores");

                opcion1 = permisos[0];
                opcion2 = permisos[1];
                opcion3 = permisos[2];
                opcion4 = permisos[3];
            }
        }

        private void CargarDatos(string busqueda = "", int status = 1)
        {
            var consulta = string.Empty;
            status = cbStatus.SelectedIndex + 1;
            if (string.IsNullOrWhiteSpace(busqueda))
            {
                consulta = $"SELECT * FROM Proveedores WHERE IDUsuario = {FormPrincipal.userID} AND Status = {status}";
            }
            else
            {
                var extra = $"AND (Nombre LIKE '%{busqueda}%' OR RFC LIKE '%{busqueda}%' OR Email LIKE '%{busqueda}%' OR Telefono LIKE '%{busqueda}%')";

                consulta = $"SELECT * FROM Proveedores WHERE IDUsuario = {FormPrincipal.userID} AND Status = {status} {extra}";
            }

            if (DGVProveedores.Rows.Count.Equals(0) || clickBoton.Equals(0))
            {
                p = new Paginar(consulta, DataMemberDGV, maximo_x_pagina);
            }
            
            DGVProveedores.Rows.Clear();

            DataSet datos = p.cargar();
            DataTable dtDatos = datos.Tables[0];



            string nombreIcono = "remove.png";

            if (status == 1)
            {
                DGVProveedores.Columns["Eliminar"].HeaderText = "Deshabilitar";
            }

            if (status == 2)
            {
                DGVProveedores.Columns["Eliminar"].HeaderText = "Habilitar";
                nombreIcono = "check.png";
            }

            Image editar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\edit.png");
            //Image eliminar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\" + nombreIcono);
            System.Drawing.Image deshabilitar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");
            System.Drawing.Image habilitar = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\arrow-up.png");

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

                if (status == 1)
                {
                    row.Cells["Eliminar"].Value = deshabilitar;
                }else
                {
                    row.Cells["Eliminar"].Value = habilitar;
                }

                
            }

            clickBoton = 0;

            DGVProveedores.ClearSelection();

            ActualizarPaginador();

            if (dtDatos.Rows.Count > 0)
            {
                datoEncontrado = 1;
            }
            else
            {
                datoEncontrado = 0;
            }
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
            AgregarProveedor.editarAgregar = "agregar";
            if (opcion2 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<AgregarProveedor>().Count() == 1)
            {
                Application.OpenForms.OfType<AgregarProveedor>().First().BringToFront();
            }
            else
            {
                AgregarProveedor ap = new AgregarProveedor();

                ap.FormClosed += delegate
                {
                    //clickBoton = 0;
                    CargarDatos();
                    //ActualizarPaginador();
                };

                ap.Show();
            }
        }

        private void DGVProveedores_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string columnHeader = DGVProveedores.Columns[7].HeaderText;
            if (opcion3 == 0 && columnHeader == "Deshabilitar" )
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else if(opcion4 == 0 && columnHeader == "Habilitar")
            {
                MessageBox.Show("No tiene permisos para acceder a este apartado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (e.RowIndex >= 0)
            {
                int idProveedor = Convert.ToInt32(DGVProveedores.Rows[e.RowIndex].Cells["ID"].Value);

                //Editar
                if (e.ColumnIndex == 6)
                {
                    AgregarProveedor.editarAgregar = "editar";
                    AgregarProveedor editar = new AgregarProveedor(2, idProveedor);

                    editar.FormClosed += delegate
                    {
                        CargarDatos("",cbStatus.SelectedIndex+1);
                    };

                    editar.ShowDialog();
                }

                //Eliminar
                if (e.ColumnIndex == 7)
                {
                    int status = 0;

                    string textoStatus = string.Empty;

                    if (cbStatus.SelectedIndex + 1 == 1)
                    {
                        textoStatus = "¿Estás seguro de deshabilitar este proveedor?";

                        status = 2;
                        
                    }
                    else
                    {
                        textoStatus = "¿Estás seguro de habilitar este proveedor?";

                        status = 1;
                        
                    }

                    if (status.Equals(1))
                    {
                        string Nombre = DGVProveedores.CurrentRow.Cells[1].Value.ToString();
                        using (var ConsultaNombre = cn.CargarDatos($"SELECT Nombre FROM proveedores WHERE `Status` = 1 AND IDUsuario = {FormPrincipal.userID}"))
                        {
                            foreach (DataRow Nombres in ConsultaNombre.Rows)
                            {
                                string unNombre = Nombres[0].ToString();
                                if (Nombre.Equals(unNombre))
                                {
                                    YaExiste = true;
                                    MessageBox.Show("Este Proveedor no puede Habilitarse ya que\nexiste otro con el mismo nombre", "Aviso del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }

                            }
                        }
                    }
                    
                    if (YaExiste.Equals(false))
                    {
                        MensajeDeHabilitarODeshabilitarProveedor Mensaje = new MensajeDeHabilitarODeshabilitarProveedor(textoStatus);
                        Mensaje.ShowDialog();

                        if (HabilitarODeshabilitar.Equals(true))
                        {

                            string[] datos = new string[] { idProveedor.ToString(), FormPrincipal.userID.ToString(), status.ToString() };

                            int resultado = cn.EjecutarConsulta(cs.GuardarProveedor(datos, 2));

                            if (resultado > 0)
                            {
                                CargarDatos();
                            }
                            cbStatus.SelectedIndex = 0;
                        }
                    }
                   
                }
                YaExiste = false;
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

            int status = cbStatus.SelectedIndex + 1;

            CargarDatos(busqueda,status);
            if (datoEncontrado != 1)
            {
                MessageBox.Show($"No se encontraron resultados con {busqueda}", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtBuscador.Text = string.Empty;
                txtBuscador.Focus();
                CargarDatos(busqueda, status);
            }
        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            CargarDatos();
            ActualizarPaginador();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            CargarDatos();
            ActualizarPaginador();
        }

        private void linkLblPaginaAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
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
            p.adelante();
            clickBoton = 1;
            CargarDatos();
            ActualizarPaginador();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            CargarDatos();
            ActualizarPaginador();
        }

        private void btnUltimaPagina_Click(object sender, EventArgs e)
        {
            p.ultimaPagina();
            clickBoton = 1;
            CargarDatos();
            ActualizarPaginador();
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

        private void Proveedores_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnNuevoProveedor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void DGVProveedores_KeyDown(object sender, KeyEventArgs e)
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

        private void cbStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int status = cbStatus.SelectedIndex + 1;

            CargarDatos(status: status);
        }

        private void btnActualizarMaximoProductos_Click(object sender, EventArgs e)
        {
            if (!txtMaximoPorPagina.Text.Equals(string.Empty))
            {
                
                var cantidadAMostrar = Convert.ToInt32(txtMaximoPorPagina.Text);

                if (cantidadAMostrar <= 0)
                {
                    mensajeParaMostrar = "Catidad a mostrar debe ser mayor a 0";
                    Utilidades.MensajeCuandoSeaCeroEnElListado(mensajeParaMostrar);
                    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                    return;
                }
                

                maximo_x_pagina = cantidadAMostrar;
                p.actualizarTope(maximo_x_pagina);
                int tipo = 1;
                string busqueda = txtBuscador.Text;
                if (cbStatus.Text == "Habilitados")
                {
                    tipo = 1;
                }
                else if (cbStatus.Text == "Deshabilitados")
                {
                    tipo = 0;
                }


                CargarDatos(busqueda,tipo);
                ActualizarPaginador();
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
                btnActualizarMaximoProductos.PerformClick();

            }
        }

        private void txtMaximoPorPagina_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsNumber(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                e.Handled = true;
            }
        }

        private void linkLblPrimeraPagina_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscador.Text.Contains("\'"))
            {
                string producto = txtBuscador.Text.Replace("\'", ""); ;
                txtBuscador.Text = producto;
                txtBuscador.Select(txtBuscador.Text.Length, 0);
            }
        }
    }
}
