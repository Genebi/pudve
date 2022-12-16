using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace PuntoDeVentaV2
{
    public partial class Empleados : Form
    {
        MetodosBusquedas mb = new MetodosBusquedas();
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        string busqueda = string.Empty;
        string filtro = string.Empty;
        private Paginar p;
        string DataMemberDGV = "empleados";
        int maximo_x_pagina = 14;
        int clickBoton = 0;
        private List<string> propiedades = new List<string>();
        string filtroConSinFiltroAvanzado = string.Empty;
        // Permisos botones
        int opcion1 = 1; // Nuevo empleado
        int opcion2 = 1; // Editar empleado
        int opcion3 = 1; // Permisos empleado
        public static bool SIoNO = false;
        string mensajeParaMostrar = string.Empty;

        public Empleados()
        {
            InitializeComponent();
        }

        private void cargar_empleados(object sender, EventArgs e)
        {
            filtroLoadProductos();
            cboMostrados.MouseWheel += new MouseEventHandler(Utilidades.ComboBox_Quitar_MouseWheel);
            //cargar_lista_empleados();
            
            cboMostrados.SelectedIndex = 0;
            if (FormPrincipal.id_empleado > 0)
            {
                var permisos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Empleados");

                opcion1 = permisos[0];
                opcion2 = permisos[1];
                opcion3 = permisos[2];
            }
            CargarDatos();
            this.Focus();
        }

        public void filtroLoadProductos()
        {
            busqueda = "1";

            filtroConSinFiltroAvanzado = cs.mostrarUsuarios(busqueda);

            p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);

            dgv_empleados.Rows.Clear();
            CargarDatos();
        }

        public void cargar_lista_empleados()
        {
            MySqlConnection sql_con;
            MySqlCommand sql_cmd;
            MySqlDataReader dr;


            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                sql_con = new MySqlConnection("datasource=" + Properties.Settings.Default.Hosting + ";port=6666;username=root;password=;database=pudve;");
            }
            else
            {
                sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }

            sql_con.Open();


            string cons = $"SELECT * FROM Empleados WHERE IDUsuario='{FormPrincipal.userID}' AND estatus=1";
            sql_cmd = new MySqlCommand(cons, sql_con);
            dr = sql_cmd.ExecuteReader();


            dgv_empleados.Rows.Clear();


            while (dr.Read())
            {
                int fila_id = dgv_empleados.Rows.Add();

                DataGridViewRow fila = dgv_empleados.Rows[fila_id];

                fila.Cells["id"].Value = dr.GetValue(dr.GetOrdinal("ID"));
                fila.Cells["nombre"].Value = dr.GetValue(dr.GetOrdinal("nombre"));
                fila.Cells["usuario"].Value = dr.GetValue(dr.GetOrdinal("usuario"));

                Image editar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\edit.png");
                Image permisos = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\unlock-alt.png");
                Image checador = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\checador.png");
                Image deshabilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");

                fila.Cells["editar"].Value = editar;
                fila.Cells["Permisos"].Value = permisos;
                fila.Cells["Checador"].Value = checador;
                fila.Cells["deshabilitar"].Value = deshabilitar;
            }

            dgv_empleados.ClearSelection();
            sql_con.Close();

            btnActualizarMaximoProductos.PerformClick();
        }

        private void btn_agregar_empleado_Click(object sender, EventArgs e)
        {
            if (opcion1 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<Agregar_empleado>().Count() == 1)
            {
                Application.OpenForms.OfType<Agregar_empleado>().First().BringToFront();
            }
            else
            {
                Agregar_empleado agregar_emp = new Agregar_empleado();

                agregar_emp.FormClosed += delegate
                {
                    cargar_lista_empleados();
                };

                agregar_emp.Show();
            }
        }

        private void cursor_en_icono(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 & e.ColumnIndex >= 3)
            {
                dgv_empleados.Cursor = Cursors.Hand;
            }
        }

        private void cursor_no_icono(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 & e.ColumnIndex >= 3)
            {
                dgv_empleados.Cursor = Cursors.Default;
            }
        }

        private void click_en_icono(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id_empleado = Convert.ToInt32(dgv_empleados.Rows[e.RowIndex].Cells["id"].Value);

                // Editar empleado
                if (e.ColumnIndex == 3)
                {
                    if (opcion2 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    var empleado = new Agregar_empleado(2, id_empleado);

                    empleado.FormClosed += delegate
                    {
                        cargar_lista_empleados();
                    };

                    empleado.Show();
                }

                // Asignar permisos
                if (e.ColumnIndex == 4)
                {
                    if (opcion3 == 0)
                    {
                        Utilidades.MensajePermiso();
                        return;
                    }

                    FormPrincipal.id_empleado = Convert.ToInt32(dgv_empleados.Rows[e.RowIndex].Cells[0].Value.ToString());
                    Agregar_empleado_permisos permisos = new Agregar_empleado_permisos(id_empleado);

                    permisos.ShowDialog();
                }

                // Asignar reglas de entrada y salida
                if (e.ColumnIndex == 5)
                {
                    //if (opcion3 == 0)
                    //{
                    //    Utilidades.MensajePermiso();
                    //    return;
                    //}

                    FormPrincipal.id_empleado = Convert.ToInt32(dgv_empleados.Rows[e.RowIndex].Cells[0].Value.ToString());
                    empleadosDatosChecador datosChecador = new empleadosDatosChecador(id_empleado);

                    datosChecador.ShowDialog();
                }

                dgv_empleados.ClearSelection();

                //Deshabilitar Empleado
                if (e.ColumnIndex == 6)
                {
                    if (cboMostrados.Text == "Habilitados")
                    {
                        string texto = "¿Esta seguro de Deshabilitar este Empleado?";
                        MensajeConfirmacionDeHabilitarODeshabilitar mensaje = new MensajeConfirmacionDeHabilitarODeshabilitar(texto);
                        mensaje.ShowDialog();
                        if (SIoNO.Equals(true))
                        {
                            string nombre = dgv_empleados.Rows[e.RowIndex].Cells[2].Value.ToString();
                            string idemp = dgv_empleados.Rows[e.RowIndex].Cells[0].Value.ToString();
                            cn.EjecutarConsulta(cs.deshabilitarEmpleado(nombre, idemp));
                            string tipo = string.Empty;
                            if (cboMostrados.Text == "Habilitados")
                            {
                                tipo = "1";
                            }
                            else if (cboMostrados.Text == "Deshabilitados")
                            {
                                tipo = "0";
                            }
                            CargarDatos(Convert.ToInt32(tipo));

                        }
                    }
                    else if (cboMostrados.Text == "Deshabilitados")
                    {
                        string texto = "¿Esta seguro de Habilitar este Empleado?";
                        MensajeConfirmacionDeHabilitarODeshabilitar mensaje = new MensajeConfirmacionDeHabilitarODeshabilitar(texto);
                        mensaje.ShowDialog();
                        if (SIoNO.Equals(true))
                        {
                            string nombre = dgv_empleados.Rows[e.RowIndex].Cells[2].Value.ToString();
                            string idemp = dgv_empleados.Rows[e.RowIndex].Cells[0].Value.ToString();
                            cn.EjecutarConsulta(cs.habilitarEmpleado(nombre, idemp));
                            string tipo = string.Empty;
                            if (cboMostrados.Text == "Habilitados")
                            {
                                tipo = "1";
                            }
                            else if (cboMostrados.Text == "Deshabilitados")
                            {
                                tipo = "0";
                            }
                            CargarDatos(Convert.ToInt32(tipo));
                        }

                    }
                    SIoNO = false;

                }
            }

            
        }

        private void Empleados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btn_agregar_empleado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void dgv_empleados_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                //BuscarProductos();
                if (!txtBuscar.Text.Equals(string.Empty))
                {
                    if (cboMostrados.Text.Equals("Habilitados"))
                    {
                        CargarDatos(1, txtBuscar.Text);
                    }
                    else if (cboMostrados.Text.Equals("Deshabilitados"))
                    {
                        CargarDatos(0, txtBuscar.Text);
                    }
                    if (dgv_empleados.Rows.Count == 0)
                    {
                        MessageBox.Show($"No se encontro ningun resultado con \n\t\t '{txtBuscar.Text}'", "Aviso de sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                else
                {
                    if (cboMostrados.Text.Equals("Habilitados"))
                    {
                        CargarDatos(1, txtBuscar.Text);
                    }
                    else if (cboMostrados.Text.Equals("Deshabilitados"))
                    {
                        CargarDatos(0, txtBuscar.Text);
                    }
                    if (dgv_empleados.Rows.Count == 0)
                    {
                        MessageBox.Show($"La búsqueda realizada no obtuvo resultados", "Aviso de sistema!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                txtBuscar.Clear();
            }
        }

        private void CargarDatos(int status = 1, string busquedaEnEmpleados = "")
        {
            busqueda = string.Empty;

            busqueda = busquedaEnEmpleados;

            if (dgv_empleados.RowCount <= 0)
            {
                if (busqueda == "")
                {
                    //filtro = cs.busquedaEmpleado(busqueda);                 
                    filtro = cs.mostrarUsuarios(status.ToString());

                    p = new Paginar(filtro, DataMemberDGV, maximo_x_pagina);
                }
                else if (busqueda != "")
                {
                    filtro = cs.busquedaEmpleado(busqueda, status);

                    p = new Paginar(filtro, DataMemberDGV, maximo_x_pagina);
                }
            }
            else if (dgv_empleados.RowCount >= 1 && clickBoton == 0)
            {
                if (busqueda == "")
                {
                    filtro = cs.mostrarUsuarios(status.ToString());

                    p = new Paginar(filtro, DataMemberDGV, maximo_x_pagina);
                }
                else if (busqueda != "")
                {
                    filtro = cs.busquedaEmpleado(busqueda, status);

                    p = new Paginar(filtro, DataMemberDGV, maximo_x_pagina);
                }
            }

            DataSet datos = p.cargar();
            DataTable dtDatos = datos.Tables[0];
            dgv_empleados.Rows.Clear();
            Image editar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\edit.png");
            Image permisos = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\unlock-alt.png");
            Image deshabilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\trash.png");
            Image checador = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\checador.png");
            Image habilitar = Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\arrow-up.png");

            foreach (DataRow filaDatos in dtDatos.Rows)
            {
                var numeroFilas = dgv_empleados.Rows.Count;

                string ID = filaDatos["id"].ToString();
                string Nombre = filaDatos["nombre"].ToString();
                string Usuario = filaDatos["usuario"].ToString();


                if (dgv_empleados.Rows.Count.Equals(0))
                {
                    var number_of_rows = dgv_empleados.Rows.Add();
                    DataGridViewRow row = dgv_empleados.Rows[number_of_rows];
                    row.Cells["id"].Value = ID;
                    row.Cells["nombre"].Value = Nombre;
                    row.Cells["usuario"].Value = Usuario;

                    row.Cells["editar"].Value = editar;
                    row.Cells["permisos"].Value = permisos;
                    row.Cells["checador"].Value = checador;

                    if (cboMostrados.Text == "Habilitados")
                    {
                        dgv_empleados.Columns["editar"].Visible = true;
                        row.Cells["deshabilitar"].Value = deshabilitar;
                    }
                    else if (cboMostrados.Text == "Deshabilitados")
                    {
                        dgv_empleados.Columns["editar"].Visible = false;
                        row.Cells["deshabilitar"].Value = habilitar;
                    }




                }
                else if (!dgv_empleados.Rows.Count.Equals(0))
                {
                    var number_of_rows = dgv_empleados.Rows.Add();
                    DataGridViewRow row = dgv_empleados.Rows[number_of_rows];
                    row.Cells["id"].Value = ID;
                    row.Cells["nombre"].Value = Nombre;
                    row.Cells["usuario"].Value = Usuario;
                    row.Cells["editar"].Value = editar;
                    row.Cells["permisos"].Value = permisos;
                    row.Cells["Checador"].Value = checador;
                    if (cboMostrados.Text == "Habilitados")
                    {
                        row.Cells["deshabilitar"].Value = deshabilitar;
                    }
                    else if (cboMostrados.Text == "Deshabilitados")
                    {
                        row.Cells["deshabilitar"].Value = habilitar;
                    }
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

        private void txtMaximoPorPagina_Click(object sender, EventArgs e)
        {
            if (!txtMaximoPorPagina.Text.Equals(string.Empty))
            {
                maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
                p.actualizarTope(maximo_x_pagina);
                string tipo = string.Empty;
                if (cboMostrados.Text == "Habilitados")
                {
                    tipo = "1";
                }
                else if (cboMostrados.Text == "Deshabilitados")
                {
                    tipo = "0";
                }
                CargarDatos(Convert.ToInt32(tipo));
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
                    string tipo = string.Empty;
                    if (cboMostrados.Text == "Habilitados")
                    {
                        tipo = "1";
                    }
                    else if (cboMostrados.Text == "Deshabilitados")
                    {
                        tipo = "0";
                    }
                    CargarDatos(Convert.ToInt32(tipo));
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
                string tipo = string.Empty;
                if (cboMostrados.Text == "Habilitados")
                {
                    tipo = "1";
                }
                else if (cboMostrados.Text == "Deshabilitados")
                {
                    tipo = "0";
                }
                CargarDatos(Convert.ToInt32(tipo));
                actualizar();
            }
            else
            {
                txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
            }

        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            string tipo = string.Empty;
            if (cboMostrados.Text == "Habilitados")
            {
                tipo = "1";
            }
            else if (cboMostrados.Text == "Deshabilitados")
            {
                tipo = "0";
            }
            CargarDatos(Convert.ToInt32(tipo));
            actualizar();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            string tipo = string.Empty;
            if (cboMostrados.Text == "Habilitados")
            {
                tipo = "1";
            }
            else if (cboMostrados.Text == "Deshabilitados")
            {
                tipo = "0";
            }
            CargarDatos(Convert.ToInt32(tipo));
            actualizar();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            string tipo = string.Empty;
            if (cboMostrados.Text == "Habilitados")
            {
                tipo = "1";
            }
            else if (cboMostrados.Text == "Deshabilitados")
            {
                tipo = "0";
            }
            CargarDatos(Convert.ToInt32(tipo));
            actualizar();
        }

        private void btnUltimaPagina_Click(object sender, EventArgs e)
        {
            p.ultimaPagina();
            clickBoton = 1;
            string tipo = string.Empty;
            if (cboMostrados.Text == "Habilitados")
            {
                tipo = "1";
            }
            else if (cboMostrados.Text == "Deshabilitados")
            {
                tipo = "0";
            }
            CargarDatos(Convert.ToInt32(tipo));
            actualizar();
        }

        private void linkLblPaginaAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            string tipo = string.Empty;
            if (cboMostrados.Text == "Habilitados")
            {
                tipo = "1";
            }
            else if (cboMostrados.Text == "Deshabilitados")
            {
                tipo = "0";
            }
            CargarDatos(Convert.ToInt32(tipo));
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
            if (cboMostrados.Text == "Habilitados")
            {
                tipo = "1";
            }
            else if (cboMostrados.Text == "Deshabilitados")
            {
                tipo = "0";
            }
            CargarDatos(Convert.ToInt32(tipo));
            actualizar();
        }

        private void cboMostrados_SelectedValueChanged(object sender, EventArgs e)
        {
            string tipo = string.Empty;
            if (cboMostrados.Text == "Habilitados")
            {
                dgv_empleados.Columns[6].HeaderText = "Deshabilitar";
                tipo = "1";
            }
            else if (cboMostrados.Text == "Deshabilitados")
            {
                dgv_empleados.Columns[6].HeaderText = "Habilitar";
                tipo = "0";
            }
            CargarDatos(Convert.ToInt32(tipo));
        }

        private void txtMaximoPorPagina_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Contains("\'"))
            {
                string producto = txtBuscar.Text.Replace("\'", ""); ;
                txtBuscar.Text = producto;
                txtBuscar.Select(txtBuscar.Text.Length, 0);
            }
        }
    }
}
