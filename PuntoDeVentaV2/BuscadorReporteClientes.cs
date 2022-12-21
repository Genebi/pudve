using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class BuscadorReporteClientes : Form
    {
        private Paginar p;

        Conexion cn = new Conexion();
        Consultas cs = new Consultas();

        System.Drawing.Image icono = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");

        Dictionary<int, string> IDClientes = new Dictionary<int, string>();

        List<string> listaProductosTemporal = new List<string>();

        string filtroConSinFiltroAvanzado = string.Empty;
        string DataMemberDGV = "Clientes";
        string busqueda = string.Empty;

        bool conBusqueda = false;
        string tipoDatoReporte = string.Empty;

        // Variables de tipo Int
        int maximo_x_pagina = 10;
        int clickBoton = 0;


        string mensajeParaMostrar = string.Empty;


        public BuscadorReporteClientes()
        {
            InitializeComponent();

            MostrarCheckBox();
        }

        private void BuscadorReporteClientes_Load(object sender, EventArgs e)
        {

            DGVReportesClientes.Columns[0].Width = 20;
            DGVReportesClientes.Columns[4].Width = 70;
            DGVReportesClientes.Columns[5].Width = 70;
            DGVReportesClientes.Columns[6].Width = 70;

            this.Focus();
            cargarDatos();

        }

        private void cargarDatos()
        {
            var query = $"SELECT ID, RazonSocial, RFC FROM Clientes WHERE IDUsuario = '{FormPrincipal.userID}' AND `Status` = 1";

            filtroConSinFiltroAvanzado = query;

            CargarDatos();
        }

        private void btnPrimeraPagina_Click(object sender, EventArgs e)
        {
            p.primerPagina();
            clickBoton = 1;
            CargarDatos();
            actualizar();
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

            if (DGVReportesClientes.RowCount <= 0)
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
            else if (DGVReportesClientes.RowCount >= 1 && clickBoton == 0)
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

            DGVReportesClientes.Rows.Clear();

            if (conBusqueda.Equals(true))
            {
                if (!dtDatos.Rows.Count.Equals(0))
                {
                    var idObtenido = string.Empty;
                    var name = string.Empty;
                    var rfc = string.Empty;

                    foreach (DataRow filaDatos in dtDatos.Rows)
                    {
                        int filaID = DGVReportesClientes.Rows.Add();
                        DataGridViewRow fila = DGVReportesClientes.Rows[filaID];

                        //idObtenido = filaDatos["ID"].ToString();
                        //name = filaDatos["RazonSocial"].ToString();
                        //rfc = filaDatos["RFC"].ToString();

                        fila.Cells["marcar"].Value = false;
                        fila.Cells["ID"].Value = filaDatos["ID"].ToString();
                        fila.Cells["Nombre"].Value = filaDatos["RazonSocial"].ToString();
                        fila.Cells["RFC"].Value = filaDatos["RFC"].ToString();
                        fila.Cells["ArticulosBuy"].Value = icono;
                        fila.Cells["ArticulosNotBuy"].Value = icono;
                        fila.Cells["DatosCliente"].Value = icono;

                        //DGVReportesClientes.Rows.Add(idObtenido, name, rfc, icono, icono, icono);
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
                    var idObtenido = string.Empty;
                    var name = string.Empty;
                    var rfc = string.Empty;

                    foreach (DataRow filaDatos in dtDatos.Rows)
                    {
                        int filaID = DGVReportesClientes.Rows.Add();
                        DataGridViewRow fila = DGVReportesClientes.Rows[filaID];

                        //idObtenido = filaDatos["ID"].ToString();
                        //name = filaDatos["RazonSocial"].ToString();
                        //rfc = filaDatos["RFC"].ToString();

                        fila.Cells["marcar"].Value = false;
                        fila.Cells["ID"].Value = filaDatos["ID"].ToString();
                        fila.Cells["Nombre"].Value = filaDatos["RazonSocial"].ToString();
                        fila.Cells["RFC"].Value = filaDatos["RFC"].ToString();
                        fila.Cells["ArticulosBuy"].Value = icono;
                        fila.Cells["ArticulosNotBuy"].Value = icono;
                        fila.Cells["DatosCliente"].Value = icono;

                        //DGVReportesClientes.Rows.Add(idObtenido, name, rfc, icono, icono, icono);
                    }
                }
            }
            actualizar();

            clickBoton = 0;
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            conBusqueda = true;

            var datoBuscar = txtBuscar.Text.ToString().Replace("\r\n", string.Empty);

            var query = $"SELECT ID, RazonSocial, RFC FROM Clientes WHERE IDUsuario = '{FormPrincipal.userID}' AND RazonSocial LIKE '%{datoBuscar}%' AND `Status` = 1";

            filtroConSinFiltroAvanzado = query;

            txtBuscar.Text = string.Empty;
            txtBuscar.Focus();
            CargarDatos();
        }

        private void txtBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnBuscar.PerformClick();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text.Contains("\'"))
            {
                string producto = txtBuscar.Text.Replace("\'", ""); ;
                txtBuscar.Text = producto;
                txtBuscar.Select(txtBuscar.Text.Length, 0);
            }
            txtBuscar.CharacterCasing = CharacterCasing.Upper;
        }

        private void DGVReportesClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = Convert.ToInt32(DGVReportesClientes.CurrentRow.Cells[1].Value.ToString());
            var nameCliente = DGVReportesClientes.CurrentRow.Cells[2].Value.ToString();
            var fila = DGVReportesClientes.CurrentCell.RowIndex;


            if (e.ColumnIndex.Equals(0))// columna del CheckBox
            {
               
                var estado = Convert.ToBoolean(DGVReportesClientes.Rows[fila].Cells["marcar"].Value);

                //En esta condicion se ponen 
                if (estado.Equals(false))//Se pone falso por que al dar click inicialmente esta en false
                {
                    if (!IDClientes.ContainsKey(id))
                    {
                        IDClientes.Add(id, string.Empty);
                    }
                }
                else if (estado.Equals(true))//Se pone verdadero por que al dar click inicialmente esta en true
                {
                    IDClientes.Remove(id);
                }
            }
            else if (e.ColumnIndex.Equals(4))//Articulos Comprados
            {
                GenerarReporteComprado(false, string.Empty, nameCliente);
            }
            else if (e.ColumnIndex.Equals(5))//Articulos no comprados
            {
                GenerarReporteNoComprado(false, nameCliente, id);
            }
            else if (e.ColumnIndex.Equals(6))//Datos del cliente
            {
                GenerarReporteDatosCliente(false, id.ToString());
            }
        }

        #region Reporte de articulos no comprados por el cliente
        private void GenerarReporteNoComprado(bool multiplesID, string idMultiples = "", int id = 0)
        {
            var mostrarClave = FormPrincipal.clave;
            //var numFolio = obtenerFolio(num);

            // Datos del usuario
            var datos = FormPrincipal.datosUsuario;

            // Fuentes y Colores
            var colorFuenteNegrita = new BaseColor(Color.Black);
            var colorFuenteBlanca = new BaseColor(Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, colorFuenteNegrita);



            // Ruta donde se creara el archivo PDF
            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;
            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\clientes.pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\clientes.pdf";
            }

            var fechaHoy = DateTime.Now;
            //var rutaArchivo = @"C:\Archivos PUDVE\Reportes\clientes.pdf";

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            Paragraph nombreClienteHeader = new Paragraph("");


            //Paragraph numeroFolio = new Paragraph("");

            string UsuarioActivo = string.Empty;

            string tipoReporte = string.Empty,
                    encabezadoTipoReporte = string.Empty;

            using (DataTable dtDataUsr = cn.CargarDatos(cs.UsuarioRazonSocialNombreCompleto(Convert.ToString(FormPrincipal.userID))))
            {
                if (!dtDataUsr.Rows.Count.Equals(0))
                {
                    foreach (DataRow drDataUsr in dtDataUsr.Rows)
                    {
                        UsuarioActivo = drDataUsr["Usuario"].ToString();
                    }
                }
            }

            Usuario = new Paragraph("USUARIO: " + UsuarioActivo, fuenteNegrita);

            Paragraph subTitulo = new Paragraph("REPORTE DE ARTICULOS NO COMPRADOS\nSECCIÓN ELEGIDA " + encabezadoTipoReporte.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            //numeroFolio = new Paragraph("No. FOLIO: " + num, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;
            //numeroFolio.Alignment = Element.ALIGN_CENTER;


            float[] anchoColumnas = new float[] { 30f, 270f, 60f, 80f };

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            PdfPTable tablaClientes = new PdfPTable(4);

            //if (!consultaCliente.Rows.Count.Equals(0))
            //{
            var nombreTemporal = string.Empty;

            int incremento = 0;
            //foreach (DataRow item in consultaCliente.Rows)
            //{
            var numRow = 0;

            DataTable consulta = new DataTable();


            VentanaDeCarga ventanaDeCarga = new VentanaDeCarga();
            Thread btnClearAllItemSale = new Thread(
                                () => ventanaDeCarga.ShowDialog());

            btnClearAllItemSale.Start();

            //MessageBox.Show("Este proceso tardará unos segundos", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            var nombreClienteEncabezado = string.Empty;

            if (multiplesID)
            {
                //consulta = cn.CargarDatos($"SELECT Prod.Nombre AS Nombre, Prod.Precio AS Price,	Prod.CodigoBarras AS Codigo, Vendidos.Cliente AS Cliente, IFNULL(Vendidos.Cliente, 'Sin Comprar') AS Situacion FROM	Productos AS Prod LEFT JOIN (SELECT	Prod.ID AS NoProducto,	Prod.Nombre AS Nombre, Prod.Precio AS Price, Prod.CodigoBarras AS Codigo,	Vent.Cliente AS Cliente	FROM Productos AS Prod LEFT JOIN ProductosVenta AS ProdVent ON ProdVent.IDProducto = Prod.ID LEFT JOIN Ventas AS Vent ON Vent.ID = ProdVent.IDVenta WHERE Vent.IDCliente IN ({idMultiples}) AND Prod.`Status` = '1' AND Prod.IDUsuario = '{FormPrincipal.userID}' ) AS Vendidos ON Prod.ID = Vendidos.NoProducto WHERE Vendidos.Cliente IS NULL AND Prod.`Status` = '1' AND Prod.IDUsuario = '{FormPrincipal.userID}'");

                 consulta = cn.CargarDatos($"SELECT DISTINCT Prod.Nombre AS Nombre, Prod.Precio AS Price, Prod.CodigoBarras AS Codigo, Vent.Cliente AS Cliente FROM Productos AS Prod LEFT JOIN ProductosVenta AS ProdVent ON Prod.ID = ProdVent.IDProducto LEFT JOIN Ventas AS Vent ON Vent.ID = ProdVent.IDVenta WHERE Prod.IDUsuario = '{FormPrincipal.userID}' AND Vent.IDCliente NOT IN ({idMultiples}) AND Prod.`Status` = 1;"); //AND Prod.`Status` = 1; En caso de solo requerir los que esten en satatus 1
            }
            else
            {

                //consulta = cn.CargarDatos($"SELECT Prod.Nombre AS Nombre, Prod.Precio AS Price,	Prod.CodigoBarras AS Codigo, Vendidos.Cliente AS Cliente, IFNULL(Vendidos.Cliente, 'Sin Comprar') AS Situacion FROM	Productos AS Prod LEFT JOIN (SELECT	Prod.ID AS NoProducto,	Prod.Nombre AS Nombre, Prod.Precio AS Price, Prod.CodigoBarras AS Codigo,	Vent.Cliente AS Cliente	FROM Productos AS Prod LEFT JOIN ProductosVenta AS ProdVent ON ProdVent.IDProducto = Prod.ID LEFT JOIN Ventas AS Vent ON Vent.ID = ProdVent.IDVenta WHERE Vent.IDCliente = '{id}' AND Prod.`Status` = '1' AND Prod.IDUsuario = '{FormPrincipal.userID}' ) AS Vendidos ON Prod.ID = Vendidos.NoProducto WHERE Vendidos.Cliente IS NULL AND Prod.`Status` = '1' AND Prod.IDUsuario = '{FormPrincipal.userID}'");
                consulta = cn.CargarDatos($"SELECT DISTINCT Prod.Nombre AS Nombre, Prod.Precio AS Price, Prod.CodigoBarras AS Codigo, Vent.Cliente AS Cliente FROM Productos AS Prod LEFT JOIN ProductosVenta AS ProdVent ON Prod.ID = ProdVent.IDProducto LEFT JOIN Ventas AS Vent ON Vent.ID = ProdVent.IDVenta WHERE Prod.IDUsuario = '{FormPrincipal.userID}' AND Vent.IDCliente != '{id}' AND Prod.`Status` = 1;"); //AND Prod.`Status` = 1; En caso de solo requerir los que esten en satatus 1
            }

            var nombre = string.Empty;
            var precio = string.Empty;
            var codigo = string.Empty;
            var nombreCliente = string.Empty;

            var validarHeader = string.Empty;

            nombreClienteEncabezado = obtenerNombreEmpleados(id.ToString());
            if (id != 0) { nombreCliente = nombreClienteEncabezado; }


            // if (string.IsNullOrEmpty(nombreCliente)) { continue; }
            //Valida si es diferente nombre de cliente para agregar un nuevo header
            //if (!nombreCliente.Equals(validarHeader))
            //{
                    PdfPCell colSeparador = new PdfPCell(new Phrase(Chunk.NEWLINE));
                    colSeparador.Colspan = 10;
                    colSeparador.BorderWidth = 0;

                    tablaClientes.WidthPercentage = 70;
                    tablaClientes.SetWidths(anchoColumnas);

                    PdfPCell colNombreCliente = new PdfPCell(new Phrase(nombreCliente, fuenteNegrita));
                    colNombreCliente.Colspan = 10;
                    colNombreCliente.BorderWidth = 0;
                    colNombreCliente.HorizontalAlignment = Element.ALIGN_LEFT;

                    PdfPCell colNoConcepto = new PdfPCell(new Phrase("No:", fuenteNegrita));
                    colNoConcepto.BorderWidth = 1;
                    colNoConcepto.BackgroundColor = new BaseColor(Color.SkyBlue);
                    colNoConcepto.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colNombre = new PdfPCell(new Phrase("NOMBRE", fuenteTotales));
                    colNombre.BorderWidth = 1;
                    colNombre.HorizontalAlignment = Element.ALIGN_CENTER;
                    colNombre.Padding = 3;
                    colNombre.BackgroundColor = new BaseColor(Color.SkyBlue);

                    PdfPCell colPrecio = new PdfPCell(new Phrase("PRECIO", fuenteTotales));
                    colPrecio.BorderWidth = 1;
                    colPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
                    colPrecio.Padding = 3;
                    colPrecio.BackgroundColor = new BaseColor(Color.SkyBlue);

                    PdfPCell colCodigoBarras = new PdfPCell(new Phrase("CODIGO DE BARRAS", fuenteTotales));
                    colCodigoBarras.BorderWidth = 1;
                    colCodigoBarras.HorizontalAlignment = Element.ALIGN_CENTER;
                    colCodigoBarras.Padding = 3;
                    colCodigoBarras.BackgroundColor = new BaseColor(Color.SkyBlue);

                    tablaClientes.AddCell(colSeparador);
                    tablaClientes.AddCell(colNombreCliente);
                    tablaClientes.AddCell(colNoConcepto);
                    tablaClientes.AddCell(colNombre);
                    tablaClientes.AddCell(colPrecio);
                    tablaClientes.AddCell(colCodigoBarras);

                    //numRow = 0;
                //}
                foreach (DataRow row in consulta.Rows)
                {
                    nombre = row["Nombre"].ToString();
                    precio = row["Price"].ToString();
                    codigo = row["Codigo"].ToString();
                    nombreCliente = row["Cliente"].ToString();

                    numRow++;
                PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                colNoConceptoTmp.BorderWidth = 1;
                colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colNombreTmp = new PdfPCell(new Phrase(nombre.ToString(), fuenteNormal));
                colNombreTmp.BorderWidth = 1;
                colNombreTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmp = new PdfPCell(new Phrase("$ " + precio.ToString(), fuenteNormal));
                colPrecioTmp.BorderWidth = 1;
                colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoBarrasTmp = new PdfPCell(new Phrase(codigo.ToString(), fuenteNormal));
                colCodigoBarrasTmp.BorderWidth = 1;
                colCodigoBarrasTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaClientes.AddCell(colNoConceptoTmp);
                tablaClientes.AddCell(colNombreTmp);
                tablaClientes.AddCell(colPrecioTmp);
                tablaClientes.AddCell(colCodigoBarrasTmp);

                validarHeader = nombre;
            }
            reporte.Add(titulo);
            reporte.Add(Usuario);
            //reporte.Add(numeroFolio);
            reporte.Add(subTitulo);
            reporte.Add(tablaClientes);

            //================================
            //=== FIN TABLA CLIENTES  =======
            //================================

            reporte.AddTitle("Reporte Inventario");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }
        #endregion

        #region Reporte datos del cliente
        private void GenerarReporteDatosCliente(bool idMulttiples, string idObtenido)
        {
            // Datos del usuario
            var datos = FormPrincipal.datosUsuario;

            // Fuentes y Colores
            var colorFuenteNegrita = new BaseColor(Color.Black);
            var colorFuenteBlanca = new BaseColor(Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, colorFuenteNegrita);

            //var numRow = 0;

            // Ruta donde se creara el archivo PDF
            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;
            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\datosClientes.pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\datosClientes.pdf";
            }

            var fechaHoy = DateTime.Now;

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            //Paragraph numeroFolio = new Paragraph("");

            string UsuarioActivo = string.Empty;

            string tipoReporte = string.Empty,
                    encabezadoTipoReporte = string.Empty;

            using (DataTable dtDataUsr = cn.CargarDatos(cs.UsuarioRazonSocialNombreCompleto(Convert.ToString(FormPrincipal.userID))))
            {
                if (!dtDataUsr.Rows.Count.Equals(0))
                {
                    foreach (DataRow drDataUsr in dtDataUsr.Rows)
                    {
                        UsuarioActivo = drDataUsr["Usuario"].ToString();
                    }
                }
            }

            Usuario = new Paragraph("USUARIO: " + UsuarioActivo, fuenteNegrita);

            Paragraph subTitulo = new Paragraph("REPORTE DE CLIENTES\nSECCIÓN ELEGIDA " + encabezadoTipoReporte.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            //numeroFolio = new Paragraph("No. FOLIO: " + num, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;
            //numeroFolio.Alignment = Element.ALIGN_CENTER;


            float[] anchoColumnas = new float[] { 190f, 90f, 80f, 80f, 120f, 70f, 70f, 120f, 80f};

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //============================
            //=== TABLA DE Clientes  =====
            //============================

            PdfPTable tablaClientes = new PdfPTable(9);
            tablaClientes.WidthPercentage = 70;
            tablaClientes.SetWidths(anchoColumnas);

            //PdfPCell colNoConcepto = new PdfPCell(new Phrase("No:", fuenteNegrita));
            //colNoConcepto.BorderWidth = 1;
            //colNoConcepto.BackgroundColor = new BaseColor(Color.SkyBlue);
            //colNoConcepto.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colNombre = new PdfPCell(new Phrase("NOMBRE COMERCIAL", fuenteTotales));
            colNombre.BorderWidth = 1;
            colNombre.HorizontalAlignment = Element.ALIGN_CENTER;
            colNombre.Padding = 3;
            colNombre.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colRFC = new PdfPCell(new Phrase("RFC", fuenteTotales));
            colRFC.BorderWidth = 1;
            colRFC.HorizontalAlignment = Element.ALIGN_CENTER;
            colRFC.Padding = 3;
            colRFC.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPais = new PdfPCell(new Phrase("PAIS", fuenteTotales));
            colPais.BorderWidth = 1;
            colPais.HorizontalAlignment = Element.ALIGN_CENTER;
            colPais.Padding = 3;
            colPais.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colEstado = new PdfPCell(new Phrase("ESTADO", fuenteTotales));
            colEstado.BorderWidth = 1;
            colEstado.HorizontalAlignment = Element.ALIGN_CENTER;
            colEstado.Padding = 3;
            colEstado.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colMunicipio = new PdfPCell(new Phrase("MUNICIPIO", fuenteTotales));
            colMunicipio.BorderWidth = 1;
            colMunicipio.HorizontalAlignment = Element.ALIGN_CENTER;
            colMunicipio.Padding = 3;
            colMunicipio.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colCodigoPostal = new PdfPCell(new Phrase("CODIGO POSTAL", fuenteTotales));
            colCodigoPostal.BorderWidth = 1;
            colCodigoPostal.HorizontalAlignment = Element.ALIGN_CENTER;
            colCodigoPostal.Padding = 3;
            colCodigoPostal.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colRegimenFiscal = new PdfPCell(new Phrase("REGIMEN FISCAL", fuenteTotales));
            colRegimenFiscal.BorderWidth = 1;
            colRegimenFiscal.HorizontalAlignment = Element.ALIGN_CENTER;
            colRegimenFiscal.Padding = 3;
            colRegimenFiscal.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colEmail = new PdfPCell(new Phrase("EMAIL", fuenteTotales));
            colEmail.BorderWidth = 1;
            colEmail.HorizontalAlignment = Element.ALIGN_CENTER;
            colEmail.Padding = 3;
            colEmail.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colTelefono = new PdfPCell(new Phrase("TELEFONO", fuenteTotales));
            colTelefono.BorderWidth = 1;
            colTelefono.HorizontalAlignment = Element.ALIGN_CENTER;
            colTelefono.Padding = 3;
            colTelefono.BackgroundColor = new BaseColor(Color.SkyBlue);

            //tablaClientes.AddCell(colNoConcepto);
            tablaClientes.AddCell(colNombre);
            tablaClientes.AddCell(colRFC);
            tablaClientes.AddCell(colPais);
            tablaClientes.AddCell(colEstado);
            tablaClientes.AddCell(colMunicipio);
            tablaClientes.AddCell(colCodigoPostal);
            tablaClientes.AddCell(colRegimenFiscal);
            tablaClientes.AddCell(colEmail);
            tablaClientes.AddCell(colTelefono);

            DataTable consulta = new DataTable();

            if (idMulttiples)
            {
                consulta = cn.CargarDatos($"SELECT * FROM Clientes WHERE IDUsuario = '{FormPrincipal.userID}' AND ID IN ({idObtenido})");
            }
            else
            {
                consulta = cn.CargarDatos($"SELECT * FROM Clientes WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idObtenido}'");
            }

            //foreach (DataRow row in consulta.Rows)
            //{
            //var nombre = row["NombreComercial"].ToString();
            //var rfc = row["RFC"].ToString();
            //var pais = row["Pais"].ToString();
            //var estado = row["Estado"].ToString();
            //var municipio = row["Municipio"].ToString();
            //var codigoPostal = row["CodigoPostal"].ToString();
            //var regimenFiscal = row["RegimenFiscal"].ToString();
            //var email = row["Email"].ToString();
            //var telefono = row["Telefono"].ToString();
            //var tipoCliente = row["TipoCliente"].ToString();
            if (!consulta.Rows.Count.Equals(0))
            {
                foreach (DataRow item in consulta.Rows)
                {
                    var nombre = item["RazonSocial"].ToString();
                    var rfc = item["RFC"].ToString();
                    var pais = item["Pais"].ToString();
                    var estado = item["Estado"].ToString();
                    var municipio = item["Municipio"].ToString();
                    var codigoPostal = item["CodigoPostal"].ToString();
                    var regimenFiscal = item["RegimenFiscal"].ToString();
                    var email = item["Email"].ToString();
                    var telefono = item["Telefono"].ToString();

                    //numRow++;
                    //PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                    //colNoConceptoTmp.BorderWidth = 1;
                    //colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colNombreTmp = new PdfPCell(new Phrase(nombre.ToString(), fuenteNormal));
                    colNombreTmp.BorderWidth = 1;
                    colNombreTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colRFCTmp = new PdfPCell(new Phrase(rfc.ToString(), fuenteNormal));
                    colRFCTmp.BorderWidth = 1;
                    colRFCTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colPaisTmp = new PdfPCell(new Phrase(pais.ToString(), fuenteNormal));
                    colPaisTmp.BorderWidth = 1;
                    colPaisTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colEstadoTmp = new PdfPCell(new Phrase(estado.ToString(), fuenteNormal));
                    colEstadoTmp.BorderWidth = 1;
                    colEstadoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colMunicipioTmp = new PdfPCell(new Phrase(municipio.ToString(), fuenteNormal));
                    colMunicipioTmp.BorderWidth = 1;
                    colMunicipioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colCodigoPostalTmp = new PdfPCell(new Phrase(codigoPostal.ToString(), fuenteNormal));
                    colCodigoPostalTmp.BorderWidth = 1;
                    colCodigoPostalTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colRegimenFiscalTmp = new PdfPCell(new Phrase(regimenFiscal.ToString(), fuenteNormal));
                    colRegimenFiscalTmp.BorderWidth = 1;
                    colRegimenFiscalTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colEmailTmp = new PdfPCell(new Phrase(email.ToString(), fuenteNormal));
                    colEmailTmp.BorderWidth = 1;
                    colEmailTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colTelefonoTmp = new PdfPCell(new Phrase(telefono.ToString(), fuenteNormal));
                    colTelefonoTmp.BorderWidth = 1;
                    colTelefonoTmp.HorizontalAlignment = Element.ALIGN_CENTER;


                    //tablaClientes.AddCell(colNoConceptoTmp);
                    tablaClientes.AddCell(colNombreTmp);
                    tablaClientes.AddCell(colRFCTmp);
                    tablaClientes.AddCell(colPaisTmp);
                    tablaClientes.AddCell(colEstadoTmp);
                    tablaClientes.AddCell(colMunicipioTmp);
                    tablaClientes.AddCell(colCodigoPostalTmp);
                    tablaClientes.AddCell(colRegimenFiscalTmp);
                    tablaClientes.AddCell(colEmailTmp);
                    tablaClientes.AddCell(colTelefonoTmp);
                }
            }
            //}

            reporte.Add(titulo);
            reporte.Add(Usuario);
            //reporte.Add(numeroFolio);
            reporte.Add(subTitulo);
            reporte.Add(tablaClientes);

            //================================
            //=== FIN TABLA DE Clientes  =====
            //================================

            reporte.AddTitle("Reporte Inventario");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();


        }
        #endregion

        #region Reportes de articulos comprados
        private void GenerarReporteComprado(bool multiplesId, string idCliente = "", string nameCliente = "")
        {
            var mostrarClave = FormPrincipal.clave;
            //var numFolio = obtenerFolio(num);

            // Datos del usuario
            var datos = FormPrincipal.datosUsuario;

            // Fuentes y Colores
            var colorFuenteNegrita = new BaseColor(Color.Black);
            var colorFuenteBlanca = new BaseColor(Color.White);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 8, 1, colorFuenteNegrita);

            

            // Ruta donde se creara el archivo PDF
            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;
            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\clientes.pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\clientes.pdf";
            }

            var fechaHoy = DateTime.Now;
            //var rutaArchivo = @"C:\Archivos PUDVE\Reportes\clientes.pdf";

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            Paragraph nombreClienteHeader = new Paragraph("");


            //Paragraph numeroFolio = new Paragraph("");

            string UsuarioActivo = string.Empty;

            string tipoReporte = string.Empty,
                    encabezadoTipoReporte = string.Empty;

            using (DataTable dtDataUsr = cn.CargarDatos(cs.UsuarioRazonSocialNombreCompleto(Convert.ToString(FormPrincipal.userID))))
            {
                if (!dtDataUsr.Rows.Count.Equals(0))
                {
                    foreach (DataRow drDataUsr in dtDataUsr.Rows)
                    {
                        UsuarioActivo = drDataUsr["Usuario"].ToString();
                    }
                }
            }

            Usuario = new Paragraph("USUARIO: " + UsuarioActivo, fuenteNegrita);

            Paragraph subTitulo = new Paragraph("REPORTE DE ARTICULOS COMPRADOS\nSECCIÓN ELEGIDA " + encabezadoTipoReporte.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            //numeroFolio = new Paragraph("No. FOLIO: " + num, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;
            //numeroFolio.Alignment = Element.ALIGN_CENTER;


            float[] anchoColumnas = new float[] { 30f, 270f, 60f, 80f };

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            PdfPTable tablaClientes = new PdfPTable(4);

            //if (!consultaCliente.Rows.Count.Equals(0))
            //{
                var nombreTemporal = string.Empty;

                int incremento = 0;
                //foreach (DataRow item in consultaCliente.Rows)
                //{
                    var numRow = 0;

            DataTable consulta = new DataTable();

            if (multiplesId)
            {
                //consulta = cn.CargarDatos($"SELECT PV.Nombre AS Nombre, SUM(PV.Cantidad) AS Cantidad, V.Cliente AS Cliente, V.FechaOperacion AS Fecha FROM ProductosVenta AS PV INNER JOIN Ventas AS V ON PV.IDVenta = V.ID WHERE V.IDUsuario = '{FormPrincipal.userID}' AND V.IDCliente IN ({idCliente}) GROUP BY Nombre ORDER BY Cantidad DESC");

                consulta = cn.CargarDatos($"SELECT PV.Nombre AS Nombre, SUM(PV.Cantidad) AS Cantidad, V.Cliente AS Cliente, V.FechaOperacion AS Fecha FROM ProductosVenta AS PV INNER JOIN Ventas AS V ON PV.IDVenta = V.ID INNER JOIN Productos AS Prod ON PV.IDProducto = Prod.ID WHERE V.IDUsuario = '{FormPrincipal.userID}' AND Prod.`Status` = 1 AND V.IDCliente IN ({idCliente}) GROUP BY Nombre ORDER BY Cantidad DESC");
            }
            else
            {
                consulta = cn.CargarDatos($"SELECT PV.Nombre AS Nombre, SUM(PV.Cantidad) AS Cantidad, V.Cliente AS Cliente, V.FechaOperacion AS Fecha FROM ProductosVenta AS PV INNER JOIN Ventas AS V ON PV.IDVenta = V.ID INNER JOIN Productos AS Prod ON PV.IDProducto = Prod.ID WHERE V.IDUsuario = '{FormPrincipal.userID}' AND Prod.`Status` = 1 AND V.Cliente = '{nameCliente}' GROUP BY Nombre ORDER BY Cantidad DESC");

            }

            var nombre = string.Empty;
            var cantidad = string.Empty;
            var fecha = string.Empty;
            var nombreCliente = string.Empty;

            var validarHeader = string.Empty;

            foreach (DataRow row in consulta.Rows)
            {
                nombre = row["Nombre"].ToString();
                cantidad = row["Cantidad"].ToString();
                fecha = row["Fecha"].ToString();
                nombreCliente = row["Cliente"].ToString();

                //Valida si es diferente nombre de cliente para agregar un nuevo header
                if (!nombreCliente.Equals(validarHeader))
                {
                    PdfPCell colSeparador = new PdfPCell(new Phrase(Chunk.NEWLINE));
                    colSeparador.Colspan = 10;
                    colSeparador.BorderWidth = 0;

                    //nombreClienteHeader = new Paragraph($"{item["RazonSocial"].ToString()}");
                    //nombreClienteHeader.Alignment = Element.ALIGN_CENTER;
                    PdfPCell colNombreCliente = new PdfPCell(new Phrase(nombreCliente, fuenteNegrita));
                    colNombreCliente.Colspan = 10;
                    colNombreCliente.BorderWidth = 0;
                    colNombreCliente.HorizontalAlignment = Element.ALIGN_LEFT;

                    tablaClientes.WidthPercentage = 70;
                    tablaClientes.SetWidths(anchoColumnas);

                    PdfPCell colNoConcepto = new PdfPCell(new Phrase("No:", fuenteNegrita));
                    colNoConcepto.BorderWidth = 1;
                    colNoConcepto.BackgroundColor = new BaseColor(Color.SkyBlue);
                    colNoConcepto.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colNombre = new PdfPCell(new Phrase("NOMBRE", fuenteTotales));
                    colNombre.BorderWidth = 1;
                    colNombre.HorizontalAlignment = Element.ALIGN_CENTER;
                    colNombre.Padding = 3;
                    colNombre.BackgroundColor = new BaseColor(Color.SkyBlue);

                    PdfPCell colCantidad = new PdfPCell(new Phrase("CANTIDAD VECES VENDIDO", fuenteTotales));
                    colCantidad.BorderWidth = 1;
                    colCantidad.HorizontalAlignment = Element.ALIGN_CENTER;
                    colCantidad.Padding = 3;
                    colCantidad.BackgroundColor = new BaseColor(Color.SkyBlue);

                    PdfPCell colFecha = new PdfPCell(new Phrase("FECHA ULTIMA VENTA", fuenteTotales));
                    colFecha.BorderWidth = 1;
                    colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
                    colFecha.Padding = 3;
                    colFecha.BackgroundColor = new BaseColor(Color.SkyBlue);

                    tablaClientes.AddCell(colSeparador);
                    tablaClientes.AddCell(colNombreCliente);
                    tablaClientes.AddCell(colNoConcepto);
                    tablaClientes.AddCell(colNombre);
                    tablaClientes.AddCell(colCantidad);
                    tablaClientes.AddCell(colFecha);

                    numRow = 0;
                }
                
                    numRow++;
                    PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                    colNoConceptoTmp.BorderWidth = 1;
                    colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colNombreTmp = new PdfPCell(new Phrase(nombre.ToString(), fuenteNormal));
                    colNombreTmp.BorderWidth = 1;
                    colNombreTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colCantidadTmp = new PdfPCell(new Phrase(cantidad.ToString(), fuenteNormal));
                    colCantidadTmp.BorderWidth = 1;
                    colCantidadTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colFechaTmp = new PdfPCell(new Phrase(fecha.ToString(), fuenteNormal));
                    colFechaTmp.BorderWidth = 1;
                    colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    tablaClientes.AddCell(colNoConceptoTmp);
                    tablaClientes.AddCell(colNombreTmp);
                    tablaClientes.AddCell(colCantidadTmp);
                    tablaClientes.AddCell(colFechaTmp);

                validarHeader = nombreCliente;
            }
                    reporte.Add(titulo);
                    reporte.Add(Usuario);
                    //reporte.Add(numeroFolio);
                    reporte.Add(subTitulo);
                    reporte.Add(tablaClientes);
        
            //================================
            //=== FIN TABLA CLIENTES  =======
            //================================

            reporte.AddTitle("Reporte Inventario");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }
        #endregion

        #region Funcionalidad del checkBoxMaster
        private void MostrarCheckBox()
        {
            System.Drawing.Rectangle rect = DGVReportesClientes.GetCellDisplayRectangle(0, -1, true);
            // set checkbox header to center of header cell. +1 pixel to position 
            rect.Y = 5;
            rect.X = 10;// rect.Location.X + (rect.Width / 4);
            CheckBox checkBoxHeader = new CheckBox();
            checkBoxHeader.Name = "checkBoxMaster";
            checkBoxHeader.Size = new Size(15, 15);
            checkBoxHeader.Location = rect.Location;
            checkBoxHeader.CheckedChanged += new EventHandler(checkBoxMaster_CheckedChanged);
            DGVReportesClientes.Controls.Add(checkBoxHeader);

        }

        private void checkBoxMaster_CheckedChanged(object sender, EventArgs e)
        {
            txtBuscar.Focus();

            var incremento = -1;

            CheckBox headerBox = ((CheckBox)DGVReportesClientes.Controls.Find("checkBoxMaster", true)[0]);

            //DGVReportesClientes.Rows[0].Cells["marcar"].Value = true;
            foreach (DataGridViewRow dgv in DGVReportesClientes.Rows)
            {
                incremento += 1;

                try
                {
                    //var recorrerCheckBox = Convert.ToBoolean(DGVReportesClientes.Rows[incremento].Cells["mostrar"].Value);

                    var idRevision = Convert.ToInt32(dgv.Cells["ID"].Value.ToString());

                    if (headerBox.Checked)
                    {
                        if (!IDClientes.ContainsKey(idRevision))
                        {
                            IDClientes.Add(idRevision, string.Empty);
                            DGVReportesClientes.Rows[incremento].Cells["marcar"].Value = true;
                        }
                    }
                    else if (!headerBox.Checked)
                    {
                        IDClientes.Remove(idRevision);
                        DGVReportesClientes.Rows[incremento].Cells["marcar"].Value = false;
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        #endregion

        private void DGVReportesClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        //    if (e.ColumnIndex == DGVReportesClientes.Columns[0].Index)
        //    {
        //        DataGridViewCheckBoxCell celda = (DataGridViewCheckBoxCell)this.DGVReportesClientes.Rows[e.RowIndex].Cells[0];

        //        if (Convert.ToBoolean(celda.Value) == false)
        //        {
        //            celda.Value = true;
        //            DGVReportesClientes.Rows[e.RowIndex].Selected = true;
        //        }
        //        else
        //        {
        //            celda.Value = false;
        //            DGVReportesClientes.Rows[e.RowIndex].Selected = false;
        //        }
        //    }
        }

        private void realizarReporteBotones(string tipoReporte)
        {
            var idClientesObtenidos = recorrerDiccionario();

            if (!IDClientes.Count.Equals(0))
            {
                if (tipoReporte.Equals("comprados"))
                {
                    GenerarReporteComprado(true, idClientesObtenidos);
                }
                else if (tipoReporte.Equals("noComprados"))
                {
                    GenerarReporteNoComprado(true, idClientesObtenidos);
                }
                else if (tipoReporte.Equals("datosCliente"))
                {
                    GenerarReporteDatosCliente(true, idClientesObtenidos);
                }
            }
            else
            {
                MessageBox.Show("No tiene clientes seleccionado para esta opción", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string recorrerDiccionario()
        {
            var result = string.Empty;

            foreach (var item in IDClientes)
            {
                result += item.Key.ToString();
                result += ",";
            }

            result = result.Remove(result.Length -1);

            return result;
        }

        private void btnComprados_Click_1(object sender, EventArgs e)
        {
            if (!IDClientes.Count.Equals(0))
            {
                realizarReporteBotones("comprados");
            }
            else
            {
                MessageBox.Show("No tiene clientes seleccionados.\nSeleccione un cliente para continuar con esta opcion.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnNoComprados_Click_1(object sender, EventArgs e)
        {
            //MessageBox.Show("Estamos trabajando en este apartado", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (!IDClientes.Count.Equals(0))
            {
                realizarReporteBotones("noComprados");
            }
            else
            {
                MessageBox.Show("No tiene clientes seleccionados.\nSeleccione un cliente para continuar con esta opcion.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDatosCLiente_Click_1(object sender, EventArgs e)
        {
            if (!IDClientes.Count.Equals(0))
            {
                realizarReporteBotones("datosCliente");
            }
            else
            {
                MessageBox.Show("No tiene clientes seleccionados.\nSeleccione un cliente para continuar con esta opcion.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string obtenerNombreEmpleados(string idCliente)
        {
            string result = string.Empty;
            
                var query = cn.CargarDatos($"SELECT RazonSocial FROM Clientes WHERE IDUsuario = '{FormPrincipal.userID}' AND ID = '{idCliente}'");

                if (!query.Rows.Count.Equals(0))
                {
                    result = query.Rows[0]["RazonSocial"].ToString();
                }

            return result;
        }

        private void BuscadorReporteClientes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
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
                int tipo = 1;
                string busqueda = txtBuscar.Text;
            

                CargarDatos();
                ActualizarPaginador();
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

        private void txtMaximoPorPagina_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtMaximoPorPagina_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnActualizarMaximoProductos.PerformClick();
            }
        }
    }
}
