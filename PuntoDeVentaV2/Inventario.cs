using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
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
        public static string proveedorElegido = string.Empty;
        public static int idReporte = 0;
        public static bool botonAceptar = false;

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
            idReporte = cn.ObtenerUltimoIdReporte(FormPrincipal.userID) + 1;
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

            AgregarStockXML inventarioXML = new AgregarStockXML();

            inventarioXML.FormClosed += delegate
            {
                GenerarReporte(idReporte);

                idReporte++;
            };

            inventarioXML.ShowDialog();
        }

        private void txtBusqueda_KeyUp(object sender, KeyEventArgs e)
        {
            ocultarResultados();
            listaProductos.Items.Clear();

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
                    var idProducto = Convert.ToInt32(datos[0]);

                    AjustarProducto ap = new AjustarProducto(idProducto, 2);

                    ap.FormClosed += delegate
                    {
                        if (botonAceptar)
                        {
                            var producto = cn.BuscarProducto(idProducto, FormPrincipal.userID);

                            AgregarProductoDGV(producto);

                            botonAceptar = false;
                        }
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
                        txtBusqueda.Focus();
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
            txtBusqueda.Text = "";
            txtBusqueda.Focus();

            var info = listaProductos.Items[listaProductos.SelectedIndex].ToString().Split('-');
            var idProducto = Convert.ToInt32(info[0]);

            AjustarProducto ap = new AjustarProducto(idProducto, 2);

            ap.FormClosed += delegate
            {
                if (botonAceptar)
                {
                    var producto = cn.BuscarProducto(idProducto, FormPrincipal.userID);

                    AgregarProductoDGV(producto);

                    botonAceptar = false;
                }
            };

            ap.ShowDialog();
        }

        private void listaProductos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                ocultarResultados();
                txtBusqueda.Text = "";
                txtBusqueda.Focus();

                var info = listaProductos.Items[listaProductos.SelectedIndex].ToString().Split('-');
                var idProducto = Convert.ToInt32(info[0]);

                AjustarProducto ap = new AjustarProducto(idProducto, 2);

                ap.FormClosed += delegate
                {
                    if (botonAceptar)
                    {
                        var producto = cn.BuscarProducto(idProducto, FormPrincipal.userID);

                        AgregarProductoDGV(producto);

                        botonAceptar = false;
                    }
                };

                ap.ShowDialog();
            }
        }

        private void AgregarProductoDGV(string[] producto)
        {
            var id = producto[0];
            var nombre = producto[1];
            var stock = producto[4];
            var precio = producto[2];
            var clave = producto[6];
            var codigo = producto[7];
            var fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            DGVInventario.Rows.Add(id, nombre, stock, precio, clave, codigo, fecha);
            DGVInventario.ClearSelection(); 
        }

        private void bntTerminar_Click(object sender, EventArgs e)
        {
            GenerarReporte(idReporte);

            DGVInventario.Rows.Clear();

            idReporte++;
        }

        private void GenerarReporte(int idReporte)
        {
            var datos = FormPrincipal.datosUsuario;

            var colorFuenteNegrita = new BaseColor(Color.Black);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);

            int anchoLogo = 110;
            int altoLogo = 60;

            var rutaArchivo = @"C:\Archivos PUDVE\Reportes\Historial\reporte_"+ idReporte +".pdf";

            //float[] anchoColumnas = new float[] { 10f, 24f, 9f, 9f };

            Document reporte = new Document(PageSize.A3);
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            string logotipo = datos[11];
            //string encabezado = $"\n{datos[1]} {datos[2]} {datos[3]}, {datos[4]}, {datos[5]}\nCol. {datos[6]} C.P. {datos[7]}\nRFC: {datos[8]}\n{datos[9]}\nTel. {datos[10]}\n\n";

            reporte.Open();

            //Validación para verificar si existe logotipo
            if (logotipo != "")
            {
                logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + logotipo;

                if (System.IO.File.Exists(logotipo))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logotipo);
                    logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    logo.ScaleAbsolute(anchoLogo, altoLogo);
                    reporte.Add(logo);
                }
            }

            Paragraph titulo = new Paragraph(datos[0] + "\n\n", fuenteGrande);
            //Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            //domicilio.Alignment = Element.ALIGN_CENTER;
            //domicilio.SetLeading(10, 0);

            /***************************************
             ** Tabla con los productos ajustados **
             ***************************************/
            PdfPTable tabla = new PdfPTable(7);
            tabla.WidthPercentage = 100;
            //tabla.SetWidths(anchoColumnas);

            PdfPCell colProveedor = new PdfPCell(new Phrase("Proveedor", fuenteNegrita));
            colProveedor.BorderWidth = 1;
            colProveedor.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colUnidades = new PdfPCell(new Phrase("Unidades compradas", fuenteNegrita));
            colUnidades.BorderWidth = 1;
            colUnidades.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colPrecioCompra = new PdfPCell(new Phrase("Precio compra", fuenteNegrita));
            colPrecioCompra.BorderWidth = 1;
            colPrecioCompra.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colPrecioVenta = new PdfPCell(new Phrase("Precio venta", fuenteNegrita));
            colPrecioVenta.BorderWidth = 1;
            colPrecioVenta.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colStock = new PdfPCell(new Phrase("Stock actual", fuenteNegrita));
            colStock.BorderWidth = 1;
            colStock.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colFechaCompra = new PdfPCell(new Phrase("Fecha de compra", fuenteNegrita));
            colFechaCompra.BorderWidth = 1;
            colFechaCompra.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colFechaOperacion = new PdfPCell(new Phrase("Fecha de operación", fuenteNegrita));
            colFechaOperacion.BorderWidth = 1;
            colFechaOperacion.HorizontalAlignment = Element.ALIGN_CENTER;

            tabla.AddCell(colProveedor);
            tabla.AddCell(colUnidades);
            tabla.AddCell(colPrecioCompra);
            tabla.AddCell(colPrecioVenta);
            tabla.AddCell(colStock);
            tabla.AddCell(colFechaCompra);
            tabla.AddCell(colFechaOperacion);


            //Consulta para obtener los registros del Historial de compras
            SQLiteConnection sql_con;
            SQLiteCommand sql_cmd;
            SQLiteDataReader dr;

            sql_con = new SQLiteConnection("Data source=" + Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\pudveDB.db; Version=3; New=False;Compress=True;");
            sql_con.Open();
            sql_cmd = new SQLiteCommand($"SELECT * FROM HistorialCompras WHERE IDUsuario = {FormPrincipal.userID} AND IDReporte = {idReporte}", sql_con);
            dr = sql_cmd.ExecuteReader();

            while (dr.Read())
            {
                var idProducto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDProducto")));
                var proveedor = dr.GetValue(dr.GetOrdinal("NomEmisor")).ToString();
                var unidades = dr.GetValue(dr.GetOrdinal("Cantidad")).ToString();
                var compra = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("ValorUnitario"))).ToString("0.00");
                var venta = Convert.ToDouble(dr.GetValue(dr.GetOrdinal("Precio"))).ToString("0.00");

                var tmp = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                var stock = tmp[4];

                DateTime fecha = (DateTime)dr.GetValue(dr.GetOrdinal("FechaLarga"));
                var fechaCompra = fecha.ToString("yyyy-MM-dd");

                DateTime fechaOp = (DateTime)dr.GetValue(dr.GetOrdinal("FechaOperacion"));
                var fechaOperacion = fechaOp.ToString("yyyy-MM-dd HH:mm tt");

                PdfPCell colProveedorTmp = new PdfPCell(new Phrase(proveedor, fuenteNormal));
                colProveedorTmp.BorderWidth = 1;
                colProveedorTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colUnidadesTmp = new PdfPCell(new Phrase(unidades, fuenteNormal));
                colUnidadesTmp.BorderWidth = 1;
                colUnidadesTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioCompraTmp = new PdfPCell(new Phrase("$" + compra, fuenteNormal));
                colPrecioCompraTmp.BorderWidth = 1;
                colPrecioCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioVentaTmp = new PdfPCell(new Phrase("$" + venta, fuenteNormal));
                colPrecioVentaTmp.BorderWidth = 1;
                colPrecioVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockTmp = new PdfPCell(new Phrase(stock, fuenteNormal));
                colStockTmp.BorderWidth = 1;
                colStockTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaCompraTmp = new PdfPCell(new Phrase(fechaCompra, fuenteNormal));
                colFechaCompraTmp.BorderWidth = 1;
                colFechaCompraTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaOperacionTmp = new PdfPCell(new Phrase(fechaOperacion, fuenteNormal));
                colFechaOperacionTmp.BorderWidth = 1;
                colFechaOperacionTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tabla.AddCell(colProveedorTmp);
                tabla.AddCell(colUnidadesTmp);
                tabla.AddCell(colPrecioCompraTmp);
                tabla.AddCell(colPrecioVentaTmp);
                tabla.AddCell(colStockTmp);
                tabla.AddCell(colFechaCompraTmp);
                tabla.AddCell(colFechaOperacionTmp);
            }

            /******************************************
             ** Fin de la tabla                      **
             ******************************************/

            reporte.Add(titulo);
            //reporte.Add(domicilio);
            reporte.Add(tabla);

            reporte.AddTitle("Reporte Historial");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }
    }
}
