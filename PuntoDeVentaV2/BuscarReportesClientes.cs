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
    public partial class BuscarReportesClientes : Form
    {
        System.Drawing.Image pdf = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");

        private Paginar p;

        // Variables de tipo String
        string filtroConSinFiltroAvanzado = string.Empty;
        string DataMemberDGV = "Caja";
        string busqueda = string.Empty;

        // Variables de tipo Int
        int maximo_x_pagina = 10;
        int clickBoton = 0;

        bool conBusqueda = false;

        public BuscarReportesClientes()
        {
            InitializeComponent();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private void BuscarReportesClientes_Load(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        public void CargarDatos(int status = 1, string busquedaEnProductos = "")
        {
            busqueda = string.Empty;

            busqueda = busquedaEnProductos;

            if (dataGridView1.RowCount <= 0)
            {
                if (busqueda == "")
                {
                    //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
                else if (busqueda != "")
                {
                    //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
            }
            else if (dataGridView1.RowCount >= 1 && clickBoton == 0)
            {
                if (busqueda == "")
                {
                    //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
                else if (busqueda != "")
                {
                    //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                    p = new Paginar(filtroConSinFiltroAvanzado, DataMemberDGV, maximo_x_pagina);
                }
            }

            DataSet datos = p.cargar();
            DataTable dtDatos = datos.Tables[0];

            dataGridView1.Rows.Clear();

            if (conBusqueda.Equals(true))
            {
                if (!dtDatos.Rows.Count.Equals(0))
                {
                    foreach (DataRow filaDatos in dtDatos.Rows)
                    {
                        var idCorte = filaDatos["ID"].ToString();
                        var fecha = filaDatos["FechaOPeracion"].ToString();
                        var idEmpleado = Convert.ToInt32(filaDatos["IdEmpleado"].ToString());
                        var empleado = filaDatos["nombre"].ToString();
                        var fechaOp = filaDatos["FechaOperacion"].ToString();
                        var name = string.Empty;

                        if (idEmpleado > 0)
                        {
                            name = empleado;
                        }
                        else
                        {
                            name = $"ADMIN {FormPrincipal.userNickName.ToString()}";
                        }

                        //var empleado = validarEmpleado(Convert.ToInt32(obtenerEmpleado));

                        dataGridView1.Rows.Add(idCorte, name, fecha, pdf, pdf, pdf, fechaOp);
                    }
                }
                else
                {
                    MessageBox.Show($"No se encontraron resultados", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtBuscar.Text = string.Empty;
                    txtBuscar.Focus();
                }
            }
            else
            {
                if (!dtDatos.Rows.Count.Equals(0))
                {
                    foreach (DataRow filaDatos in dtDatos.Rows)
                    {
                        var idCorte = filaDatos["ID"].ToString();
                        var fecha = filaDatos["FechaOPeracion"].ToString();
                        var idEmpleado = Convert.ToInt32(filaDatos["IdEmpleado"].ToString());
                        var empleado = filaDatos["nombre"].ToString();
                        var fechaOp = filaDatos["FechaOperacion"].ToString();
                        var name = string.Empty;

                        if (idEmpleado > 0)
                        {
                            name = empleado;
                        }
                        else
                        {
                            name = $"ADMIN {FormPrincipal.userNickName.ToString()}";
                        }

                        //var empleado = validarEmpleado(Convert.ToInt32(obtenerEmpleado));

                        dataGridView1.Rows.Add(idCorte, name, fecha, pdf, pdf, pdf, fechaOp);
                    }
                }

                //var numeroFilas = dataGridView1.Rows.Count;

                //string Nombre = filaDatos["Nombre"].ToString();
                //string Stock = filaDatos["Stock"].ToString();
                //string Precio = filaDatos["Precio"].ToString();
                //string Clave = filaDatos["ClaveInterna"].ToString();
                //string Codigo = filaDatos["CodigoBarras"].ToString();
                //string Tipo = filaDatos["Tipo"].ToString();
                //string Proveedor = filaDatos["Proveedor"].ToString();
                //string chckName = filaDatos["ChckName"].ToString();
                //string Descripcion = filaDatos["Descripcion"].ToString();

                //if (dataGridView1.Rows.Count.Equals(0))
                //{
                //    bool encontrado = Utilidades.BuscarDataGridView(Nombre, "Nombre", dataGridView1);

                //    if (encontrado.Equals(false))
                //    {
                //        var number_of_rows = dataGridView1.Rows.Add();
                //        DataGridViewRow row = dataGridView1.Rows[number_of_rows];

                //        row.Cells["Nombre"].Value = Nombre;     // Columna Nombre
                //        row.Cells["Stock"].Value = Stock;       // Columna Stock
                //        row.Cells["Precio"].Value = Precio;     // Columna Precio
                //        row.Cells["Clave"].Value = Clave;       // Columna Clave
                //        row.Cells["Codigo"].Value = Codigo;     // Columna Codigo

                //        // Columna Tipo
                //        if (Tipo.Equals("P"))
                //        {
                //            row.Cells["Tipo"].Value = "PRODUCTO";
                //        }
                //        else if (Tipo.Equals("S"))
                //        {
                //            row.Cells["Tipo"].Value = "SERVICIO";
                //        }
                //        else if (Tipo.Equals("PQ"))
                //        {
                //            row.Cells["Tipo"].Value = "COMBO";
                //        }

                //        row.Cells["Proveedor"].Value = Proveedor;   // Columna Proveedor

                //        if (dataGridView1.Columns.Contains(chckName))
                //        {
                //            row.Cells[chckName].Value = Descripcion;
                //        }
                //    }
                //}
                //else if (!dataGridView1.Rows.Count.Equals(0))
                //{
                //    foreach (DataGridViewRow Row in dataGridView1.Rows)
                //    {
                //        bool encontrado = Utilidades.BuscarDataGridView(Nombre, "Nombre", dataGridView1);

                //        if (encontrado.Equals(true))
                //        {
                //            var Fila = Row.Index;
                //            // Columnas Dinamicos
                //            if (dataGridView1.Columns.Contains(chckName))
                //            {
                //                dataGridView1.Rows[Fila].Cells[chckName].Value = Descripcion;
                //            }
                //        }
                //        else if (encontrado.Equals(false))
                //        {
                //            var number_of_rows = dataGridView1.Rows.Add();
                //            DataGridViewRow row = dataGridView1.Rows[number_of_rows];

                //            row.Cells["Nombre"].Value = Nombre;         // Columna Nombre
                //            row.Cells["Stock"].Value = Stock;           // Columna Stock
                //            row.Cells["Precio"].Value = Precio;         // Columna Precio
                //            row.Cells["Clave"].Value = Clave;           // Columna Clave
                //            row.Cells["Codigo"].Value = Codigo;         // Columna Codigo

                //            // Columna Tipo
                //            if (Tipo.Equals("P"))
                //            {
                //                row.Cells["Tipo"].Value = "PRODUCTO";
                //            }
                //            else if (Tipo.Equals("S"))
                //            {
                //                row.Cells["Tipo"].Value = "SERVICIO";
                //            }
                //            else if (Tipo.Equals("PQ"))
                //            {
                //                row.Cells["Tipo"].Value = "COMBO";
                //            }

                //            // Columna Proveedor
                //            row.Cells["Proveedor"].Value = Proveedor;

                //            // Columnas Dinamicos
                //            if (dataGridView1.Columns.Contains(chckName))
                //            {
                //                row.Cells[chckName].Value = Descripcion;
                //            }
                //        }
                //    }
                //}
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

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void linkLblPaginaAnterior_Click(object sender, EventArgs e)
        {
            p.atras();
            clickBoton = 1;
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
            CargarDatos();
            actualizar();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            p.adelante();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void btnUltimaPagina_Click(object sender, EventArgs e)
        {
            p.ultimaPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
        }

        private void btnActualizarMaximoProductos_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaximoPorPagina.ToString()))
            {
                txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
            }

            maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
            p.actualizarTope(maximo_x_pagina);
            CargarDatos();
            actualizar();
        }

        private void txtMaximoPorPagina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtMaximoPorPagina.ToString()))
                {
                    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                }
                maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
                p.actualizarTope(maximo_x_pagina);
                CargarDatos();
                actualizar();
            }
        }
    }
}
