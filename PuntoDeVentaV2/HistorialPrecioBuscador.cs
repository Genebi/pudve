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
    public partial class HistorialPrecioBuscador : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        private Paginar p;

        Dictionary<int, string> listaIdEmpleados = new Dictionary<int, string>();
        Dictionary<int, string> listaIdProductos = new Dictionary<int, string>();

        int maximo_x_pagina = 10;
        int clickBoton = 0;

        string filtroConSinFiltroAvanzado = string.Empty;
        string busqueda = string.Empty;
        bool conBusqueda = false;

        private string fechaInicial = string.Empty;
        private string fechaFinal = string.Empty;

        public static string idEmpleadoObtenido { get; set; }
        public static string procedencia { get; set; }

        string tipoBuscador = string.Empty;
        string fechaInicialF = string.Empty;
        string fechaFinalF = string.Empty;

        public HistorialPrecioBuscador(string tipoBusqueda, string fechaInicial, string fechaFinal)
        {
            InitializeComponent();
            this.tipoBuscador = tipoBusqueda;
            procedencia = tipoBuscador;
            this.fechaInicial = fechaInicial;
            fechaInicialF = fechaInicial;
            this.fechaFinal = fechaFinal;
            fechaFinalF = fechaFinal;
        }

        private void HistorialPrecioBuscador_Load(object sender, EventArgs e)
        {
            //idEmpleadoObtenido = "-1";
            idEmpleadoObtenido = string.Empty;

            rbHabilitados.Checked = true;

            this.Text = $"Buscar de {tipoBuscador}";
            lbTitulo.Text = $"Buscar {tipoBuscador}";

            if (tipoBuscador.Equals("Empleados"))
            {
                DGVDatosEmpleados.Visible = true;
                DGVDatosProductos.Visible = false;
                cargarEmpleados();
            }
            else if (tipoBuscador.Equals("Productos"))
            {
                DGVDatosEmpleados.Visible = false;
                DGVDatosProductos.Visible = true;
                cargarProductos();
            }
        }

        private void cargarEmpleados(bool porBusqueda = false)
        {
            DGVDatosEmpleados.Rows.Clear();
            //DGVDatos.Columns.Clear();
            //foreach (DataGridViewRow item in DGVDatos.Rows)
            //{
            //    if (DGVDatos.Columns.Count > 0)
            //    {
            //        DGVDatos.Columns.RemoveAt(DGVDatos.Columns.Count -1);
            //    }
            //}

            DataTable query = new DataTable();

            //DGVDatos.Columns.Add("ID", "ID");
            //DGVDatos.Columns.Add("Nombre", "Nombre");

            var empleadoBuscar = txtBuscar.Text;

            var status = 0;
            if (rbHabilitados.Checked) { status = 1; } else { status = 0; }

            var consulta = $"SELECT ID, Usuario FROM Empleados WHERE IDUsuario = '{FormPrincipal.userID}' AND Estatus = '{status}' ";
            if (porBusqueda.Equals(false))
            {//Aqui va la consulta sin buscador
                consulta += "LIMIT 20";
            }
            else
            {//Aqui va la consulta con buscador 
                consulta += $"AND Nombre LIKE '%{empleadoBuscar}%' LIMIT 20";
            }

            //query = cn.CargarDatos(consulta);
            filtroConSinFiltroAvanzado = consulta;

            //if (!query.Rows.Count.Equals(0))
            //{
            //    foreach (DataRow dgv in query.Rows)
            //    {
            //        int filaId = DGVDatosEmpleados.Rows.Add();
            //        DataGridViewRow fila = DGVDatosEmpleados.Rows[filaId];

            //        fila.Cells["checkBox"].Value = false;
            //        fila.Cells["Nombre"].Value = dgv["Usuario"].ToString();
            //        fila.Cells["ID"].Value = dgv["ID"].ToString();

            //    }
            //}
            //else
            //{
            //    MessageBox.Show($"No se encontraron resultados con: {txtBuscar.Text}", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            CargarDatos();
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void cargarProductos(bool porBusqueda = false)
        {
            //DGVDatosProductos.Columns.Clear();
            DGVDatosProductos.Rows.Clear();

            DataTable query = new DataTable();

            //DGVDatosProductos.Columns.Add("ID", "ID");
            //DGVDatosProductos.Columns.Add("Nombre", "Nombre");
            //DGVDatosProductos.Columns.Add("Stock", "Stock");
            //DGVDatosProductos.Columns.Add("CBarras", "CodigoBarras");
            //DGVDatosProductos.Columns.Add("Tipo", "Tipo");

            //DGVDatosProductos.Columns[1].Width = 150;// definimos tamaño a la columna de Nombre
           
            var productoBuscar = txtBuscar.Text;

            var status = 0;
            if (rbHabilitados.Checked) { status = 1; } else { status = 0; }

            var consulta = $"SELECT ID, Nombre, Stock, CodigoBarras, Tipo, Status FROM Productos WHERE IDUsuario = '{FormPrincipal.userID}' AND Status = '{status}'";

            if (porBusqueda.Equals(false))
            {//Aqui va la consulta sin buscador
                consulta += "LIMIT 20";
            }
            else
            {//Aqui va la consulta con buscador 
                consulta += $"AND Nombre LIKE '%{productoBuscar}%' LIMIT 20";
            }

            //query = cn.CargarDatos(consulta);
            filtroConSinFiltroAvanzado = consulta;

            //if (!query.Rows.Count.Equals(0))
            //{
            //    var tipoProducto = string.Empty;
            //    var estado = string.Empty;

            //    foreach (DataRow dgv in query.Rows)
            //    {
            //        int filaId = DGVDatosProductos.Rows.Add();
            //        DataGridViewRow fila = DGVDatosProductos.Rows[filaId];

            //        fila.Cells["checkBoxProd"].Value = false;
            //        fila.Cells["IDProducto"].Value = dgv["ID"].ToString();
            //        fila.Cells["NombreProducto"].Value = dgv["Nombre"].ToString();
            //        fila.Cells["Stock"].Value = dgv["Stock"].ToString();
            //        fila.Cells["CodigoBarras"].Value = dgv["CodigoBarras"].ToString();

            //        if (dgv["Tipo"].ToString().Equals("PQ"))
            //        {
            //            tipoProducto = "Combo";
            //        }
            //        else if (dgv["Tipo"].ToString().Equals("P"))
            //        {
            //            tipoProducto = "Producto";
            //        }
            //        else if (dgv["Tipo"].ToString().Equals("S"))
            //        {
            //            tipoProducto = "Servicio";
            //        }

            //        if (dgv["Status"].ToString().Equals("1"))
            //        {
            //            estado = "Habilitado";
            //        }
            //        else if (dgv["Status"].ToString().Equals("0"))
            //        {
            //            estado = "Deshabilitado";
            //        }

            //        fila.Cells["Status"].Value = estado;
            //        fila.Cells["tipo"].Value = tipoProducto;
            //    }
            //}
            //else
            //{
            //    MessageBox.Show($"No se encontraron resultados con: {txtBuscar.Text}", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            CargarDatos();
            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            var nombreBuscar = txtBuscar.Text;

            conBusqueda = true;

            var porBusqueda = true;

            if (!string.IsNullOrEmpty(nombreBuscar))
            {
                //if (tipoBuscador.Equals("Empleados"))
                //{
                //    cargarEmpleados(porBusqueda);
                //}
                //else if (tipoBuscador.Equals("Productos"))
                //{
                //    cargarProductos(porBusqueda);
                //}
                condicionesLlenadoDGV(porBusqueda);
            }
            else
            {
                MessageBox.Show("Campo de texto vacío.\nIngrese algun dato a buscar.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            txtBuscar.CharacterCasing = CharacterCasing.Upper;
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private void DGVDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //ejecutarMovimiento();
        }

        private void ejecutarMovimiento()
        {
            //Obtiene el id del empleado o de el producto.
            if (tipoBuscador.Equals("Empleados"))
            {
                //idEmpleadoObtenido = Convert.ToInt32(DGVDatosEmpleados.Rows[DGVDatosEmpleados.CurrentRow.Index].Cells[0].Value.ToString());

                var datosGet = recorrerDiccionario(listaIdEmpleados);

                idEmpleadoObtenido = datosGet;
            }
            else if (tipoBuscador.Equals("Productos"))
            {
                //idEmpleadoObtenido = Convert.ToInt32(DGVDatosProductos.Rows[DGVDatosProductos.CurrentRow.Index].Cells[0].Value.ToString());

                var datosGet = recorrerDiccionario(listaIdProductos);

                idEmpleadoObtenido = datosGet;

            }

            this.Close();
        }

        private void DGVDatosEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var fila = DGVDatosEmpleados.CurrentCell.RowIndex;

            int idVenta = Convert.ToInt32(DGVDatosEmpleados.Rows[fila].Cells["ID"].Value);

            if (e.ColumnIndex == 0)
            {
                var estado = Convert.ToBoolean(DGVDatosEmpleados.Rows[fila].Cells["checkbox"].Value);

                //En esta condicion se ponen 
                if (estado.Equals(false))//Se pone falso por que al dar click inicialmente esta en false
                {
                    if (!listaIdEmpleados.ContainsKey(idVenta))
                    {
                        listaIdEmpleados.Add(idVenta, string.Empty);
                    }
                }
                else if (estado.Equals(true))//Se pone verdadero por que al dar click inicialmente esta en true
                {
                    listaIdEmpleados.Remove(idVenta);
                }
            }
        }

        private void DGVDatosProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var fila = DGVDatosProductos.CurrentCell.RowIndex;

            int idVenta = Convert.ToInt32(DGVDatosProductos.Rows[fila].Cells["IDProducto"].Value);

            if (e.ColumnIndex == 0)
            {
                var estado = Convert.ToBoolean(DGVDatosProductos.Rows[fila].Cells["checkBoxProd"].Value);

                //En esta condicion se ponen 
                if (estado.Equals(false))//Se pone falso por que al dar click inicialmente esta en false
                {
                    if (!listaIdProductos.ContainsKey(idVenta))
                    {
                        listaIdProductos.Add(idVenta, string.Empty);
                    }
                }
                else if (estado.Equals(true))//Se pone verdadero por que al dar click inicialmente esta en true
                {
                    listaIdProductos.Remove(idVenta);
                }
            }
        }

        private string recorrerDiccionario(Dictionary<int, string> diccionario)
        {
            //Recorre el Diccionario y agregar todo en una sola cadena para la consulta
            var cadenaCompleta = string.Empty;
            foreach (KeyValuePair<int, string> item in diccionario)
            {
                cadenaCompleta += $"{item.Key},".ToString();
            }
            cadenaCompleta = cadenaCompleta.TrimEnd(',');

            return cadenaCompleta;
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            var lista1 = listaIdEmpleados.Count();
            var lista2 = listaIdProductos.Count();

            if ((lista1 + lista2) > 0)
            {
                string datosGet = string.Empty;
                if (tipoBuscador.Equals("Empleados"))
                {
                    datosGet = recorrerDiccionario(listaIdEmpleados);

                }
                else if (tipoBuscador.Equals("Productos"))
                {
                    datosGet = recorrerDiccionario(listaIdProductos);
                }
                //var fechas = new FechasReportes();
                //var fechas = new FechasReportes();
                //fechaInicial = fechas.fechaInicial;
                //fechaFinal = fechas.fechaFinal;

                if (cs.validarInformacion(tipoBuscador, datosGet, fechaInicialF, fechaFinalF))
                {
                    ejecutarMovimiento();
                }
                else
                {
                    MessageBox.Show("No existe infomación para generar el reporte.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("No tiene clientes seleccionados.\nSeleccione un cliente para continuar con esta opcion.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DGVDatosEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DGVDatosEmpleados.Columns[0].Index)
            {
                DataGridViewCheckBoxCell celda = (DataGridViewCheckBoxCell)this.DGVDatosEmpleados.Rows[e.RowIndex].Cells[0];

                if (Convert.ToBoolean(celda.Value) == false)
                {
                    celda.Value = true;
                    DGVDatosEmpleados.Rows[e.RowIndex].Selected = true;
                }
                else
                {
                    celda.Value = false;
                    DGVDatosEmpleados.Rows[e.RowIndex].Selected = false;
                }
            }
        }

        private void DGVDatosProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == DGVDatosProductos.Columns[0].Index)
            {
                DataGridViewCheckBoxCell celda = (DataGridViewCheckBoxCell)this.DGVDatosProductos.Rows[e.RowIndex].Cells[0];

                if (Convert.ToBoolean(celda.Value) == false)
                {
                    celda.Value = true;
                    DGVDatosProductos.Rows[e.RowIndex].Selected = true;
                }
                else
                {
                    celda.Value = false;
                    DGVDatosProductos.Rows[e.RowIndex].Selected = false;
                }
            }
        }

        private void rbDeshabilitados_CheckedChanged(object sender, EventArgs e)
        {
            condicionesLlenadoDGV();
        }

        private void rbHabilitados_CheckedChanged(object sender, EventArgs e)
        {
            condicionesLlenadoDGV();
        }

        private void condicionesLlenadoDGV(bool buscar = false)
        {
            if (tipoBuscador.Equals("Empleados"))
            {
                cargarEmpleados(buscar);
            }
            else if (tipoBuscador.Equals("Productos"))
            {
                cargarProductos(buscar);
            }
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

        public void CargarDatos(int status = 1, string busquedaEnProductos = "")
        {
            busqueda = string.Empty;

            busqueda = busquedaEnProductos;

            //Condicion para paginar segun sea empleados o productos
            if (tipoBuscador.Equals("Empleados"))
            {
                if (DGVDatosEmpleados.RowCount <= 0)
                {
                    if (busqueda == "")
                    {
                        //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                        p = new Paginar(filtroConSinFiltroAvanzado, tipoBuscador, maximo_x_pagina);
                    }
                    else if (busqueda != "")
                    {
                        //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                        p = new Paginar(filtroConSinFiltroAvanzado, tipoBuscador, maximo_x_pagina);
                    }
                }
                else if (DGVDatosEmpleados.RowCount >= 1 && clickBoton == 0)
                {
                    if (busqueda == "")
                    {
                        //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                        p = new Paginar(filtroConSinFiltroAvanzado, tipoBuscador, maximo_x_pagina);
                    }
                    else if (busqueda != "")
                    {
                        //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                        p = new Paginar(filtroConSinFiltroAvanzado, tipoBuscador, maximo_x_pagina);
                    }
                }
            }
            else if (tipoBuscador.Equals("Productos"))
            {
                if (DGVDatosProductos.RowCount <= 0)
                {
                    if (busqueda == "")
                    {
                        //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                        p = new Paginar(filtroConSinFiltroAvanzado, tipoBuscador, maximo_x_pagina);
                    }
                    else if (busqueda != "")
                    {
                        //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                        p = new Paginar(filtroConSinFiltroAvanzado, tipoBuscador, maximo_x_pagina);
                    }
                }
                else if (DGVDatosProductos.RowCount >= 1 && clickBoton == 0)
                {
                    if (busqueda == "")
                    {
                        //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                        p = new Paginar(filtroConSinFiltroAvanzado, tipoBuscador, maximo_x_pagina);
                    }
                    else if (busqueda != "")
                    {
                        //filtroConSinFiltroAvanzado = cs.searchSaleProduct(busqueda);

                        p = new Paginar(filtroConSinFiltroAvanzado, tipoBuscador, maximo_x_pagina);
                    }
                }
            }

            

            DataSet datos = p.cargar();
            DataTable dtDatos = datos.Tables[0];

            DGVDatosEmpleados.Rows.Clear();
            DGVDatosProductos.Rows.Clear();

            if (tipoBuscador.Equals("Empleados"))
            {
                if (!dtDatos.Rows.Count.Equals(0))
                {
                    foreach (DataRow dgv in dtDatos.Rows)
                    {
                        int filaId = DGVDatosEmpleados.Rows.Add();
                        DataGridViewRow fila = DGVDatosEmpleados.Rows[filaId];

                        fila.Cells["checkBox"].Value = false;
                        fila.Cells["Nombre"].Value = dgv["Usuario"].ToString();
                        fila.Cells["ID"].Value = dgv["ID"].ToString();

                    }
                }
                else
                {
                    //MessageBox.Show($"No se encontraron resultados con: {txtBuscar.Text}", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else if(tipoBuscador.Equals("Productos"))
            {
                //if (!dtDatos.Rows.Count.Equals(0))
                //{
                //    var rev = string.Empty;
                //    var name = string.Empty;
                //    var fecha = string.Empty;
                //    var usr = string.Empty;

                //    foreach (DataRow filaDatos in dtDatos.Rows)
                //    {
                //        //rev = filaDatos["NoRevision"].ToString();
                //        //name = filaDatos["NameUsr"].ToString();
                //        //fecha = filaDatos["Fecha"].ToString();

                //        //usr = cs.validarEmpleadoPorID();

                //        //if (name.Equals(usr))
                //        //{
                //        //    name = $"ADMIN ({name})";
                //        //}
                //        if (tipoDatoReporte.Equals("RInventario"))
                //        {
                //            //rev = filaDatos["NoRevision"].ToString();
                //            rev = filaDatos["NumFolio"].ToString();
                //            name = filaDatos["NameUsr"].ToString();
                //            fecha = filaDatos["Fecha"].ToString();
                //            usr = cs.validarEmpleadoPorID();

                //            if (name.Equals(usr))
                //            {
                //                name = $"ADMIN ({name})";
                //            }
                //        }
                //        else
                //        {
                //            //rev = filaDatos["NoRevision"].ToString();
                //            rev = filaDatos["Folio"].ToString();
                //            name = filaDatos["IDEmpleado"].ToString();
                //            fecha = filaDatos["Fecha"].ToString();

                //            usr = cs.BuscarEmpleadoCaja(Convert.ToInt32(name));

                //            if (string.IsNullOrEmpty(usr))
                //            {
                //                var admin = FormPrincipal.userNickName.Split('@');
                //                name = $"ADMIN ({admin[0]})";
                //            }
                //            else
                //            {
                //                name = usr;
                //            }
                //        }

                //        DGVInventario.Rows.Add(rev, name, fecha, icono);
                //    }
                //}
                    var tipoProducto = string.Empty;
                    var estado = string.Empty;

                    foreach (DataRow dgv in dtDatos.Rows)
                    {
                        int filaId = DGVDatosProductos.Rows.Add();
                        DataGridViewRow fila = DGVDatosProductos.Rows[filaId];

                        fila.Cells["checkBoxProd"].Value = false;
                        fila.Cells["IDProducto"].Value = dgv["ID"].ToString();
                        fila.Cells["NombreProducto"].Value = dgv["Nombre"].ToString();
                        fila.Cells["Stock"].Value = dgv["Stock"].ToString();
                        fila.Cells["CodigoBarras"].Value = dgv["CodigoBarras"].ToString();

                        if (dgv["Tipo"].ToString().Equals("PQ"))
                        {
                            tipoProducto = "Combo";
                        }
                        else if (dgv["Tipo"].ToString().Equals("P"))
                        {
                            tipoProducto = "Producto";
                        }
                        else if (dgv["Tipo"].ToString().Equals("S"))
                        {
                            tipoProducto = "Servicio";
                        }

                        if (dgv["Status"].ToString().Equals("1"))
                        {
                            estado = "Habilitado";
                        }
                        else if (dgv["Status"].ToString().Equals("0"))
                        {
                            estado = "Deshabilitado";
                        }

                        fila.Cells["Status"].Value = estado;
                        fila.Cells["tipo"].Value = tipoProducto;
                    }
                }
            else
            {
                    MessageBox.Show($"No se encontraron resultados con: {txtBuscar.Text}", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



                //var numeroFilas = DGVInventario.Rows.Count;

                //string Nombre = filaDatos["Nombre"].ToString();
                //string Stock = filaDatos["Stock"].ToString();
                //string Precio = filaDatos["Precio"].ToString();
                //string Clave = filaDatos["ClaveInterna"].ToString();
                //string Codigo = filaDatos["CodigoBarras"].ToString();
                //string Tipo = filaDatos["Tipo"].ToString();
                //string Proveedor = filaDatos["Proveedor"].ToString();
                //string chckName = filaDatos["ChckName"].ToString();
                //string Descripcion = filaDatos["Descripcion"].ToString();

                //if (DGVInventario.Rows.Count.Equals(0))
                //{
                //    bool encontrado = Utilidades.BuscarDataGridView(Nombre, "Nombre", DGVInventario);

                //    if (encontrado.Equals(false))
                //    {
                //        var number_of_rows = DGVInventario.Rows.Add();
                //        DataGridViewRow row = DGVInventario.Rows[number_of_rows];

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

                //        if (DGVInventario.Columns.Contains(chckName))
                //        {
                //            row.Cells[chckName].Value = Descripcion;
                //        }
                //    }
                //}
                //else if (!DGVInventario.Rows.Count.Equals(0))
                //{
                //    foreach (DataGridViewRow Row in DGVInventario.Rows)
                //    {
                //        bool encontrado = Utilidades.BuscarDataGridView(Nombre, "Nombre", DGVInventario);

                //        if (encontrado.Equals(true))
                //        {
                //            var Fila = Row.Index;
                //            // Columnas Dinamicos
                //            if (DGVInventario.Columns.Contains(chckName))
                //            {
                //                DGVInventario.Rows[Fila].Cells[chckName].Value = Descripcion;
                //            }
                //        }
                //        else if (encontrado.Equals(false))
                //        {
                //            var number_of_rows = DGVInventario.Rows.Add();
                //            DataGridViewRow row = DGVInventario.Rows[number_of_rows];

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
                //            if (DGVInventario.Columns.Contains(chckName))
                //            {
                //                row.Cells[chckName].Value = Descripcion;
                //            }
                //        }
                //    }
                //}
                //}

                actualizar();

            clickBoton = 0;
        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
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
            if (string.IsNullOrEmpty(txtMaximoPorPagina.Text))
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
                if (string.IsNullOrEmpty(txtMaximoPorPagina.Text))
                {
                    txtMaximoPorPagina.Text = maximo_x_pagina.ToString();
                }
                maximo_x_pagina = Convert.ToInt32(txtMaximoPorPagina.Text);
                p.actualizarTope(maximo_x_pagina);
                CargarDatos();
                actualizar();
            }
        }

        private void HistorialPrecioBuscador_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }
    }
}
