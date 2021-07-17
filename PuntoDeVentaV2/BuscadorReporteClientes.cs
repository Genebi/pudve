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

        string filtroConSinFiltroAvanzado = string.Empty;
        string DataMemberDGV = "Clientes";
        string busqueda = string.Empty;

        bool conBusqueda = false;
        string tipoDatoReporte = string.Empty;

        // Variables de tipo Int
        int maximo_x_pagina = 10;
        int clickBoton = 0;

        public BuscadorReporteClientes()
        {
            InitializeComponent();
        }

        private void BuscadorReporteClientes_Load(object sender, EventArgs e)
        {
            DGVReportesClientes.Columns[3].Width = 50;
            DGVReportesClientes.Columns[4].Width = 50;
            DGVReportesClientes.Columns[5].Width = 60;

            txtBuscar.Focus();
            cargarDatos();
        }

        private void cargarDatos()
        {
            var query = $"SELECT ID, RazonSocial, RFC FROM Clientes WHERE IDUsuario = '{FormPrincipal.userID}'";

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
                        idObtenido = filaDatos["ID"].ToString();
                        name = filaDatos["RazonSocial"].ToString();
                        rfc = filaDatos["RFC"].ToString();

                        DGVReportesClientes.Rows.Add(idObtenido, name, rfc, icono, icono, icono);
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
                        idObtenido = filaDatos["ID"].ToString();
                        name = filaDatos["RazonSocial"].ToString();
                        rfc = filaDatos["RFC"].ToString();

                        DGVReportesClientes.Rows.Add(idObtenido, name, rfc, icono, icono, icono);
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

            var query = $"SELECT ID, RazonSocial, RFC FROM Clientes WHERE IDUsuario = '{FormPrincipal.userID}' AND RazonSocial LIKE '%{datoBuscar}%'";

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
            txtBuscar.CharacterCasing = CharacterCasing.Upper;
        }

        private void DGVReportesClientes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = Convert.ToInt32(DGVReportesClientes.CurrentRow.Cells[0].Value.ToString());
            var nameCliente = DGVReportesClientes.CurrentRow.Cells[1].Value.ToString();

            if (e.ColumnIndex.Equals(3))//Articulos Comprados
            {
                GenerarReporteComprado(id, nameCliente);
            }
            else if (e.ColumnIndex.Equals(4))//Articulos no comprados
            {

            }
            else if (e.ColumnIndex.Equals(5))//Datos del cliente
            {

            }
        }

        #region Reportes de articulos comprados
        private void GenerarReporteComprado(int idCliente, string nameCliente)
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

            var numRow = 0;

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


            float[] anchoColumnas = new float[] { 30f, 270f, 60f, 80f};

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //============================
            //=== TABLA DE INVENTARIO  ===
            //============================

            PdfPTable tablaClientes = new PdfPTable(4);
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

            tablaClientes.AddCell(colNoConcepto);
            tablaClientes.AddCell(colNombre);
            tablaClientes.AddCell(colCantidad);
            tablaClientes.AddCell(colFecha);

            var consulta = cn.CargarDatos($"SELECT PV.Nombre AS Nombre, SUM(PV.Cantidad) AS Cantidad, V.Cliente AS Cliente, V.FechaOperacion AS Fecha FROM ProductosVenta AS PV INNER JOIN Ventas AS V ON PV.IDVenta = V.ID WHERE V.IDUsuario = '{FormPrincipal.userID}' AND V.Cliente = '{nameCliente}' GROUP BY Nombre ORDER BY Cantidad DESC");

            foreach (DataRow row in consulta.Rows)
            {
                var nombre = row["Nombre"].ToString();
                var cantidad = row["Cantidad"].ToString();
                var fecha = row["Fecha"].ToString();

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

            }

            reporte.Add(titulo);
            reporte.Add(Usuario);
            //reporte.Add(numeroFolio);
            reporte.Add(subTitulo);
            reporte.Add(tablaClientes);

            //================================
            //=== FIN TABLA DE INVENTARIO  ===
            //================================

            reporte.AddTitle("Reporte Inventario");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }
        #endregion
    }
}
