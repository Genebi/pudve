using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    public partial class s : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        MetodosBusquedas mb = new MetodosBusquedas();

        private string concepto = string.Empty;
        private string fechaInicial = string.Empty;
        private string fechaFinal = string.Empty;

        private int idAMostrar = 0;
        private string tipoBusquedaHistorialPrcios = string.Empty;

        public static bool botonAceptar = false;

        // Permisos botones
        int opcion1 = 1; // Historial precios
        int opcion2 = 1; // Historial dinero agregado
        int opcion3 = 1;
        int opcion4 = 1;
        int opcion5 = 1;
        int opcion6 = 1;

        public s()
        {
            InitializeComponent();
        }

        private void Reportes_Load(object sender, EventArgs e)
        {
            if (FormPrincipal.id_empleado > 0)
            {
                var permisos = mb.ObtenerPermisosEmpleado(FormPrincipal.id_empleado, "Reportes");

                opcion1 = permisos[0];
                opcion2 = permisos[1];
                opcion3 = permisos[2];
                opcion4 = permisos[3];
                opcion5 = permisos[4];
                opcion6 = permisos[5];
            }
            this.Focus();
        }

        private void btnHistorialPrecios_Click(object sender, EventArgs e)
        {
            if (opcion1 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<FechasReportes>().Count() == 1)
            {
                Application.OpenForms.OfType<FechasReportes>().First().BringToFront();
            }
            else
            {
                var fechas = new FechasReportes();

                fechas.FormClosed += delegate
                {
                    if (botonAceptar)
                    {
                        botonAceptar = false;

                        fechaInicial = fechas.fechaInicial;
                        fechaFinal = fechas.fechaFinal;

                        if (!string.IsNullOrWhiteSpace(fechaInicial))
                        {
                            if (!string.IsNullOrWhiteSpace(fechaFinal))
                            {
                                if (Utilidades.AdobeReaderInstalado())
                                {
                                    //if (cs.validarInformacio(FechasReportes.lugarProcedencia, FechasReportes.idEncontrado))
                                    //{
                                    //    GenerarReportePrecios(FechasReportes.lugarProcedencia, FechasReportes.idEncontrado);
                                    //}
                                    //else
                                    //{
                                    //    MessageBox.Show("No existe infomación para generar el reporte.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //}
                                    GenerarReportePrecios(FechasReportes.lugarProcedencia, FechasReportes.idEncontrado);
                                }
                                else
                                {
                                    Utilidades.MensajeAdobeReader();
                                }
                            }
                        }
                    }
                };

                fechas.Show();
            }
        }

        //public bool validarInformacion(string procedencia, string idEmp)
        //{
        //    bool result = false;

        //    var consulta = string.Empty;

        //    if (procedencia.Equals("Seleccionar Empleado/Producto"))//consulta Normal
        //    {
        //        consulta = $"SELECT * FROM HistorialPrecios WHERE IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' ORDER BY FechaOperacion DESC";
        //    }
        //    else if (procedencia.Equals("Empleados"))// Consulta segun empleado
        //    {
        //        var validarId = string.Empty;
        //        if (!string.IsNullOrEmpty(idEmp))
        //        {
        //            validarId = idEmp;
        //        }

        //        consulta = $"SELECT * FROM HistorialPrecios WHERE IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' AND IDEmpleado IN ({validarId}) ORDER BY FechaOperacion DESC";
        //    }
        //    else if (procedencia.Equals("Productos"))//Consulta por producto
        //    {
        //        consulta = $"SELECT * FROM HistorialPrecios WHERE IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' AND IDProducto IN ({idEmp}) ORDER BY FechaOperacion DESC";
        //    }

        //    var datosVerificar = cn.CargarDatos(consulta);

        //    if (!datosVerificar.Rows.Count.Equals(0))
        //    {
        //        result = true;
        //    }
        //    else
        //    {
        //        result = false;
        //    }


        //    return result;
        //}

        private void GenerarReportePrecios(string procedencia, string idEmp)
        {
            var precioAnteriorSuma = 0.00;
            var precioNuevoSuma = 0.00;

            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            var datos = FormPrincipal.datosUsuario;

            var colorFuenteNegrita = new BaseColor(Color.Black);

            var fuenteNormal = FontFactory.GetFont(FontFactory.HELVETICA, 8);
            var fuenteNegrita = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, 1, colorFuenteNegrita);
            var fuenteGrande = FontFactory.GetFont(FontFactory.HELVETICA, 10);
            var fuenteMensaje = FontFactory.GetFont(FontFactory.HELVETICA, 10);


            int anchoLogo = 110;
            int altoLogo = 60;

            var fechaActual = DateTime.Now;
            var servidor = Properties.Settings.Default.Hosting;
            var rutaArchivo = string.Empty;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\Historial\reporte_historial_precios_{fechaActual.ToString("yyyyMMddHHmmss")}.pdf";
            }
            else
            {
                rutaArchivo = $@"C:\Archivos PUDVE\Reportes\Historial\reporte_historial_precios_{fechaActual.ToString("yyyyMMddHHmmss")}.pdf";
            }

            Document reporte = new Document(PageSize.A3);
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            Paragraph Usuario = new Paragraph("");

            string UsuarioActivo = string.Empty;
            string datoEmpleado = string.Empty;

            string logotipo = datos[11];
            //string encabezado = $"\n{datos[1]} {datos[2]} {datos[3]}, {datos[4]}, {datos[5]}\nCol. {datos[6]} C.P. {datos[7]}\nRFC: {datos[8]}\n{datos[9]}\nTel. {datos[10]}\n\n";

            reporte.Open();

            //Validación para verificar si existe logotipo
            if (logotipo != "")
            {
                //logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + logotipo;

                if (string.IsNullOrWhiteSpace(servidor))
                {
                    logotipo = $@"\\{servidor}\Archivos PUDVE\MisDatos\Usuarios\" + logotipo;
                }
                else
                {
                    logotipo = @"C:\Archivos PUDVE\MisDatos\Usuarios\" + logotipo;
                }

                if (File.Exists(logotipo))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logotipo);
                    logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
                    logo.ScaleAbsolute(anchoLogo, altoLogo);
                    reporte.Add(logo);
                }
            }

            if (FormPrincipal.userNickName.Contains('@'))
            {
                datoEmpleado = cs.buscarNombreCliente(FormPrincipal.userNickName);
            }

            var getNombreAdmin = cn.CargarDatos(cs.UsuarioRazonSocialNombreCompleto(FormPrincipal.userID.ToString()));
            var nameAdmin = string.Empty;
            if (!getNombreAdmin.Rows.Count.Equals(0)) { nameAdmin = getNombreAdmin.Rows[0]["Usuario"].ToString(); }

            Usuario = new Paragraph("");

            Paragraph Empleado = new Paragraph($"Empleado: {datoEmpleado}", fuenteNormal);

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);

            Usuario = new Paragraph($"USUARIO: ADMIN ({nameAdmin})", fuenteNegrita);

            Paragraph status1 = new Paragraph($"Productos habilitados", fuenteNegrita);

            Paragraph subTitulo = new Paragraph("REPORTE HISTORIAL PRECIOS\nFecha: " + fechaActual.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);
            //Paragraph domicilio = new Paragraph(encabezado, fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            if (FormPrincipal.userNickName.Contains('@')) { Empleado.Alignment = Element.ALIGN_CENTER; }
            subTitulo.Alignment = Element.ALIGN_CENTER;
            status1.Alignment = Element.ALIGN_CENTER;
            //domicilio.Alignment = Element.ALIGN_CENTER;
            //domicilio.SetLeading(10, 0);

            /***************************************
             ** Tabla con los productos ajustados **
             ***************************************/
            float[] anchoColumnas = new float[] { 50f, 300f, 60f, 150f, 100f, 80f, 80f, 100f, 120f };

            PdfPTable tabla = new PdfPTable(9);
            tabla.WidthPercentage = 100;
            tabla.SetWidths(anchoColumnas);

            PdfPCell colNumeroFila = new PdfPCell(new Phrase("No.", fuenteNegrita));
            colNumeroFila.BorderWidth = 1;
            colNumeroFila.BackgroundColor = new BaseColor(Color.SkyBlue);
            colNumeroFila.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colProducto = new PdfPCell(new Phrase("Producto / Servicio / Combo", fuenteNegrita));
            colProducto.BorderWidth = 1;
            colProducto.BackgroundColor = new BaseColor(Color.SkyBlue);
            colProducto.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colTipoProducto = new PdfPCell(new Phrase("Tipo", fuenteNegrita));
            colTipoProducto.BorderWidth = 1;
            colTipoProducto.BackgroundColor = new BaseColor(Color.SkyBlue);
            colTipoProducto.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colCodigoAsociado = new PdfPCell(new Phrase("Código Asociado", fuenteNegrita));
            colCodigoAsociado.BorderWidth = 1;
            colCodigoAsociado.BackgroundColor = new BaseColor(Color.SkyBlue);
            colCodigoAsociado.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colEmpleado = new PdfPCell(new Phrase("Empleado", fuenteNegrita));
            colEmpleado.BorderWidth = 1;
            colEmpleado.BackgroundColor = new BaseColor(Color.SkyBlue);
            colEmpleado.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colPrecioAnterior = new PdfPCell(new Phrase("Precio Anterior", fuenteNegrita));
            colPrecioAnterior.BorderWidth = 1;
            colPrecioAnterior.BackgroundColor = new BaseColor(Color.SkyBlue);
            colPrecioAnterior.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colPrecioNuevo = new PdfPCell(new Phrase("Precio Nuevo", fuenteNegrita));
            colPrecioNuevo.BorderWidth = 1;
            colPrecioNuevo.BackgroundColor = new BaseColor(Color.SkyBlue);
            colPrecioNuevo.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colOrigen = new PdfPCell(new Phrase("Origen", fuenteNegrita));
            colOrigen.BorderWidth = 1;
            colOrigen.BackgroundColor = new BaseColor(Color.SkyBlue);
            colOrigen.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colFechaOperacion = new PdfPCell(new Phrase("Fecha de Operación", fuenteNegrita));
            colFechaOperacion.BorderWidth = 1;
            colFechaOperacion.BackgroundColor = new BaseColor(Color.SkyBlue);
            colFechaOperacion.HorizontalAlignment = Element.ALIGN_CENTER;

            tabla.AddCell(colNumeroFila);
            tabla.AddCell(colProducto);
            tabla.AddCell(colTipoProducto);
            tabla.AddCell(colCodigoAsociado);
            tabla.AddCell(colEmpleado);
            tabla.AddCell(colPrecioAnterior);
            tabla.AddCell(colPrecioNuevo);
            tabla.AddCell(colOrigen);
            tabla.AddCell(colFechaOperacion);

            //Consulta para obtener los registros del Historial de compras
            MySqlConnection sql_con;
            MySqlCommand sql_cmd;
            MySqlDataReader dr;

            if (!string.IsNullOrWhiteSpace(servidor))
            {
                sql_con = new MySqlConnection($"datasource={servidor};port=6666;username=root;password=;database=pudve;");
            }
            else
            {
                sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            }

            sql_con.Open();

            var consulta = string.Empty;
            if (procedencia.Equals("Seleccionar Empleado/Producto"))//consulta Normal
            {
                consulta = $"SELECT * FROM HistorialPrecios WHERE IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' ORDER BY FechaOperacion DESC";
            }
            else if (procedencia.Equals("Empleados"))// Consulta segun empleado
            {
                var validarId = string.Empty;
                if (!string.IsNullOrEmpty(idEmp))
                {
                    validarId = idEmp;
                }

                consulta = $"SELECT * FROM HistorialPrecios WHERE IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' AND IDEmpleado IN ({validarId}) ORDER BY FechaOperacion DESC";
            }
            else if (procedencia.Equals("Productos"))//Consulta por producto
            {
                consulta = $"SELECT * FROM HistorialPrecios WHERE IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' AND IDProducto IN ({idEmp}) ORDER BY FechaOperacion DESC";
            }

            sql_cmd = new MySqlCommand(consulta, sql_con);
            dr = sql_cmd.ExecuteReader();

            int numRow = 0;

            while (dr.Read())
            {
                numRow += 1;
                var idAutor = 0;
                var idDeUsuario = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDUsuario")));
                var idEmpleado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDEmpleado")));
                string emp = string.Empty;
                if (idEmpleado != 0)
                {
                    idAutor = idEmpleado;
                    emp = "empleado";
                }
                else
                {
                    idAutor = idDeUsuario;
                    emp = "admin";
                }

                var consultaEmp = cs.consultarUsuarioEmpleado(idAutor, emp);
                if (emp.Equals("admin")) { consultaEmp = $"ADMIN ({consultaEmp})"; }

                var idProducto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDProducto")));
                var datosProducto = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                var nombreProducto = datosProducto[1];
                var tipoProducto = datosProducto[5];

                var codigosAsociados = string.Empty;
                if (tipoProducto.Equals("PQ")) { codigosAsociados = cs.ObtenerCodigosAsociados(idProducto); } else { codigosAsociados = "N/A"; }

                if (tipoProducto.Equals("PQ")) { tipoProducto = "Combo"; } else if (tipoProducto.Equals("P")) { tipoProducto = "Producto"; } else if (tipoProducto.Equals("S")) { tipoProducto = "Servicio"; }

                var precioAnterior = float.Parse(dr.GetValue(dr.GetOrdinal("PrecioAnterior")).ToString());
                var precioNuevo = float.Parse(dr.GetValue(dr.GetOrdinal("PrecioNuevo")).ToString());
                var origen = dr.GetValue(dr.GetOrdinal("Origen")).ToString();

                var estado = cs.buscarEstadoProducto(idProducto);

                if (estado.Equals(0))
                {
                    continue;
                }

                precioAnteriorSuma += precioAnterior;
                precioNuevoSuma += precioNuevo;

                DateTime fechaOp = (DateTime)dr.GetValue(dr.GetOrdinal("FechaOperacion"));
                var fechaOperacion = fechaOp.ToString("yyyy-MM-dd HH:mm tt");

                PdfPCell colIncrementoRow = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                colIncrementoRow.BorderWidth = 1;
                colIncrementoRow.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colProductoTmp = new PdfPCell(new Phrase(nombreProducto, fuenteNormal));
                colProductoTmp.BorderWidth = 1;
                colProductoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colTipoProductoTmp = new PdfPCell(new Phrase(tipoProducto, fuenteNormal));
                colTipoProductoTmp.BorderWidth = 1;
                colTipoProductoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigosAsociadosTmp = new PdfPCell(new Phrase(codigosAsociados, fuenteNormal));
                colCodigosAsociadosTmp.BorderWidth = 1;
                colCodigosAsociadosTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colEmpleadoTmp = new PdfPCell(new Phrase(consultaEmp, fuenteNormal));
                colEmpleadoTmp.BorderWidth = 1;
                colEmpleadoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioAnteriorTmp = new PdfPCell(new Phrase("$" + precioAnterior.ToString("N2"), fuenteNormal));
                colPrecioAnteriorTmp.BorderWidth = 1;
                colPrecioAnteriorTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioNuevoTmp = new PdfPCell(new Phrase("$" + precioNuevo.ToString("N2"), fuenteNormal));
                colPrecioNuevoTmp.BorderWidth = 1;
                colPrecioNuevoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colOrigenTmp = new PdfPCell(new Phrase(origen, fuenteNormal));
                colOrigenTmp.BorderWidth = 1;
                colOrigenTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaOperacionTmp = new PdfPCell(new Phrase(fechaOperacion, fuenteNormal));
                colFechaOperacionTmp.BorderWidth = 1;
                colFechaOperacionTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tabla.AddCell(colIncrementoRow);
                tabla.AddCell(colProductoTmp);
                tabla.AddCell(colTipoProductoTmp);
                tabla.AddCell(colCodigosAsociadosTmp);
                tabla.AddCell(colEmpleadoTmp);
                tabla.AddCell(colPrecioAnteriorTmp);
                tabla.AddCell(colPrecioNuevoTmp);
                tabla.AddCell(colOrigenTmp);
                tabla.AddCell(colFechaOperacionTmp);
                }

                //Columna para total de dinero
                PdfPCell colNumFilatempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNumFilatempTotal.BorderWidth = 0;
                colNumFilatempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colClienteTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colClienteTempTotal.BorderWidth = 0;
                colClienteTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRFCTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colRFCTempTotal.BorderWidth = 0;
                colRFCTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colTotalTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colTotalTempTotal.BorderWidth = 0;
                colTotalTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFolioTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colFolioTempTotal.BorderWidth = 0;
                colFolioTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colSerieTempTotal = new PdfPCell(new Phrase("$" + precioAnteriorSuma.ToString("0.00"), fuenteNormal));
                colSerieTempTotal.BorderWidth = 0;
                colSerieTempTotal.BorderWidthBottom = 1;
                colSerieTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                colSerieTempTotal.BackgroundColor = new BaseColor(Color.SkyBlue);

                PdfPCell colFechaTempTotal = new PdfPCell(new Phrase("$" + precioNuevoSuma.ToString("0.00"), fuenteNormal));
                colFechaTempTotal.BorderWidth = 0;
                colFechaTempTotal.BorderWidthBottom = 1;
                colFechaTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                colFechaTempTotal.BackgroundColor = new BaseColor(Color.SkyBlue);

                PdfPCell colEmpleadoTempTotal = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colEmpleadoTempTotal.BorderWidth = 0;
                colEmpleadoTempTotal.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colEmpleadoTempTotalsegundo = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colEmpleadoTempTotalsegundo.BorderWidth = 0;
                colEmpleadoTempTotalsegundo.HorizontalAlignment = Element.ALIGN_CENTER;

                tabla.AddCell(colNumFilatempTotal);
                tabla.AddCell(colClienteTempTotal);
                tabla.AddCell(colRFCTempTotal);
                tabla.AddCell(colTotalTempTotal);
                tabla.AddCell(colFolioTempTotal);
                tabla.AddCell(colSerieTempTotal);
                tabla.AddCell(colFechaTempTotal);
                tabla.AddCell(colEmpleadoTempTotal);
                tabla.AddCell(colEmpleadoTempTotalsegundo);

            sql_con.Close();

            //reporte.Add(new Chunk("/n"));
            //Paragraph linea = new Paragraph(new Chunk(new Chunk(new Chunk(new LineSeparator(0.0f, 100.0f, (new BaseColor(Color.Black)), Element.ALIGN_LEFT, 1)))));

            //reporte.Add(linea);




            ////Consulta para obtener los registros del Historial de compras
            //MySqlConnection sql_con;
            //MySqlCommand sql_cmd;
            //MySqlDataReader dr;

            //if (!string.IsNullOrWhiteSpace(servidor))
            //{
            //    sql_con = new MySqlConnection($"datasource={servidor};port=6666;username=root;password=;database=pudve;");
            //}
            //else
            //{
            //    sql_con = new MySqlConnection("datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;");
            //}

            /******************************************
             ** Fin de la tabla                      **
             ******************************************/

            reporte.Add(titulo);
            reporte.Add(Usuario);
            if (FormPrincipal.userNickName.Contains('@')) { reporte.Add(Empleado); }
            reporte.Add(subTitulo);
            reporte.Add(status1);
            reporte.Add(Chunk.NEWLINE);

            //reporte.Add(domicilio);
            reporte.Add(tabla);

            //reporte.Add(linea);
        
            //sql_con.Close();


            Paragraph status0 = new Paragraph($"Productos deshabilitados", fuenteNegrita);

            status0.Alignment = Element.ALIGN_CENTER;

            sql_con.Open();

            var consultaStatus = string.Empty;
            if (procedencia.Equals("Seleccionar Empleado/Producto"))//consulta Normal
            {
                consulta = $"SELECT * FROM HistorialPrecios WHERE IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' ORDER BY FechaOperacion DESC";
            }
            else if (procedencia.Equals("Empleados"))// Consulta segun empleado
            {
                var validarId = string.Empty;
                if (!string.IsNullOrEmpty(idEmp))
                {
                    validarId = idEmp;
                }

                consulta = $"SELECT * FROM HistorialPrecios WHERE IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' AND IDEmpleado = '{validarId}' ORDER BY FechaOperacion DESC";
            }
            else if (procedencia.Equals("Productos"))//Consulta por producto
            {
                consulta = $"SELECT * FROM HistorialPrecios WHERE IDUsuario = {FormPrincipal.userID} AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' AND IDProducto = '{idEmp}'ORDER BY FechaOperacion DESC";
            }

            sql_cmd = new MySqlCommand(consulta, sql_con);
            dr = sql_cmd.ExecuteReader();

            int numRowStatus = 0;

            var precioAnteriorSumaStatus = 0.00;
            var precioNuevoSumaStatus = 0.00;
            if (dr.HasRows)
            {
                var idEstadoProd = 0;
                bool estado = false;

                var query = cn.CargarDatos(consulta);


                if (!query.Rows.Count.Equals(0))
                {
                    foreach (DataRow datoStatus in query.Rows)
                    {
                        idEstadoProd = Convert.ToInt32(datoStatus["IDProducto"]);

                        var estado2 = cs.buscarEstadoProducto(idEstadoProd);

                        if (estado2.Equals(0))
                        {
                            estado = true;
                        }
                    }
                }

                float[] anchoColumnasStatus = new float[] { 50f, 300f, 60f, 150f, 100f, 80f, 80f, 100f, 120f };

                PdfPTable tablasStatus = new PdfPTable(9);
                tablasStatus.WidthPercentage = 100;
                tablasStatus.SetWidths(anchoColumnas);

                PdfPCell colNumeroFilaStatus = new PdfPCell(new Phrase("No.", fuenteNegrita));
                colNumeroFilaStatus.BorderWidth = 1;
                colNumeroFilaStatus.BackgroundColor = new BaseColor(Color.SkyBlue);
                colNumeroFilaStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colProductoStatus = new PdfPCell(new Phrase("Producto / Servicio / Combo", fuenteNegrita));
                colProductoStatus.BorderWidth = 1;
                colProductoStatus.BackgroundColor = new BaseColor(Color.SkyBlue);
                colProductoStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colTipoProductoStatus = new PdfPCell(new Phrase("Tipo", fuenteNegrita));
                colTipoProductoStatus.BorderWidth = 1;
                colTipoProductoStatus.BackgroundColor = new BaseColor(Color.SkyBlue);
                colTipoProductoStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoAsociadoStatus = new PdfPCell(new Phrase("Código Asociado", fuenteNegrita));
                colCodigoAsociadoStatus.BorderWidth = 1;
                colCodigoAsociadoStatus.BackgroundColor = new BaseColor(Color.SkyBlue);
                colCodigoAsociadoStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colEmpleadoStatus = new PdfPCell(new Phrase("Empleado", fuenteNegrita));
                colEmpleadoStatus.BorderWidth = 1;
                colEmpleadoStatus.BackgroundColor = new BaseColor(Color.SkyBlue);
                colEmpleadoStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioAnteriorStatus = new PdfPCell(new Phrase("Precio Anterior", fuenteNegrita));
                colPrecioAnteriorStatus.BorderWidth = 1;
                colPrecioAnteriorStatus.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPrecioAnteriorStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioNuevoStatus = new PdfPCell(new Phrase("Precio Nuevo", fuenteNegrita));
                colPrecioNuevoStatus.BorderWidth = 1;
                colPrecioNuevoStatus.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPrecioNuevoStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colOrigenStatus = new PdfPCell(new Phrase("Origen", fuenteNegrita));
                colOrigenStatus.BorderWidth = 1;
                colOrigenStatus.BackgroundColor = new BaseColor(Color.SkyBlue);
                colOrigenStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaOperacionStatus = new PdfPCell(new Phrase("Fecha de Operación", fuenteNegrita));
                colFechaOperacionStatus.BorderWidth = 1;
                colFechaOperacionStatus.BackgroundColor = new BaseColor(Color.SkyBlue);
                colFechaOperacionStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                if (estado)
                {
                    tablasStatus.AddCell(colNumeroFilaStatus);
                    tablasStatus.AddCell(colProductoStatus);
                    tablasStatus.AddCell(colTipoProductoStatus);
                    tablasStatus.AddCell(colCodigoAsociadoStatus);
                    tablasStatus.AddCell(colEmpleadoStatus);
                    tablasStatus.AddCell(colPrecioAnteriorStatus);
                    tablasStatus.AddCell(colPrecioNuevoStatus);
                    tablasStatus.AddCell(colOrigenStatus);
                    tablasStatus.AddCell(colFechaOperacionStatus);
                }



                while (dr.Read())
                {
                    numRowStatus += 1;
                    var idAutor = 0;
                    var idDeUsuario = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDUsuario")));
                    var idEmpleado = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDEmpleado")));
                    string emp = string.Empty;
                    if (idEmpleado != 0)
                    {
                        idAutor = idEmpleado;
                        emp = "empleado";
                    }
                    else
                    {
                        idAutor = idDeUsuario;
                        emp = "admin";
                    }

                    var consultaEmp = cs.consultarUsuarioEmpleado(idAutor, emp);
                    if (emp.Equals("admin")) { consultaEmp = $"ADMIN ({consultaEmp})"; }

                    var idProducto = Convert.ToInt32(dr.GetValue(dr.GetOrdinal("IDProducto")));
                    var datosProducto = cn.BuscarProducto(idProducto, FormPrincipal.userID);
                    var nombreProducto = datosProducto[1];
                    var tipoProducto = datosProducto[5];

                    var codigosAsociados = string.Empty;
                    if (tipoProducto.Equals("PQ")) { codigosAsociados = cs.ObtenerCodigosAsociados(idProducto); } else { codigosAsociados = "N/A"; }

                    if (tipoProducto.Equals("PQ")) { tipoProducto = "Combo"; } else if (tipoProducto.Equals("P")) { tipoProducto = "Producto"; } else if (tipoProducto.Equals("S")) { tipoProducto = "Servicio"; }

                    var precioAnteriorStatus = float.Parse(dr.GetValue(dr.GetOrdinal("PrecioAnterior")).ToString());
                    var precioNuevoStatus = float.Parse(dr.GetValue(dr.GetOrdinal("PrecioNuevo")).ToString());
                    var origenStatus = dr.GetValue(dr.GetOrdinal("Origen")).ToString();

                    //var estado = cs.buscarEstadoProducto(idProducto);

                    if (!estado)
                    {
                        continue;
                    }

                    precioAnteriorSumaStatus += precioAnteriorStatus;
                    precioNuevoSumaStatus += precioNuevoStatus;

                    DateTime fechaOp = (DateTime)dr.GetValue(dr.GetOrdinal("FechaOperacion"));
                    var fechaOperacion = fechaOp.ToString("yyyy-MM-dd HH:mm tt");

                    PdfPCell colIncrementoRow = new PdfPCell(new Phrase(numRowStatus.ToString(), fuenteNormal));
                    colIncrementoRow.BorderWidth = 1;
                    colIncrementoRow.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colProductoTmp = new PdfPCell(new Phrase(nombreProducto, fuenteNormal));
                    colProductoTmp.BorderWidth = 1;
                    colProductoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colTipoProductoTmp = new PdfPCell(new Phrase(tipoProducto, fuenteNormal));
                    colTipoProductoTmp.BorderWidth = 1;
                    colTipoProductoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colCodigosAsociadosTmp = new PdfPCell(new Phrase(codigosAsociados, fuenteNormal));
                    colCodigosAsociadosTmp.BorderWidth = 1;
                    colCodigosAsociadosTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colEmpleadoTmp = new PdfPCell(new Phrase(consultaEmp, fuenteNormal));
                    colEmpleadoTmp.BorderWidth = 1;
                    colEmpleadoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colPrecioAnteriorTmp = new PdfPCell(new Phrase("$" + precioAnteriorStatus.ToString("N2"), fuenteNormal));
                    colPrecioAnteriorTmp.BorderWidth = 1;
                    colPrecioAnteriorTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colPrecioNuevoTmp = new PdfPCell(new Phrase("$" + precioNuevoStatus.ToString("N2"), fuenteNormal));
                    colPrecioNuevoTmp.BorderWidth = 1;
                    colPrecioNuevoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colOrigenTmp = new PdfPCell(new Phrase(origenStatus, fuenteNormal));
                    colOrigenTmp.BorderWidth = 1;
                    colOrigenTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    PdfPCell colFechaOperacionTmp = new PdfPCell(new Phrase(fechaOperacion, fuenteNormal));
                    colFechaOperacionTmp.BorderWidth = 1;
                    colFechaOperacionTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                    tablasStatus.AddCell(colIncrementoRow);
                    tablasStatus.AddCell(colProductoTmp);
                    tablasStatus.AddCell(colTipoProductoTmp);
                    tablasStatus.AddCell(colCodigosAsociadosTmp);
                    tablasStatus.AddCell(colEmpleadoTmp);
                    tablasStatus.AddCell(colPrecioAnteriorTmp);
                    tablasStatus.AddCell(colPrecioNuevoTmp);
                    tablasStatus.AddCell(colOrigenTmp);
                    tablasStatus.AddCell(colFechaOperacionTmp);
                }

                //Columna para total de dinero
                PdfPCell colNumFilatempTotalStatus = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNumFilatempTotalStatus.BorderWidth = 0;
                colNumFilatempTotalStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colClienteTempTotalStatus = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colClienteTempTotalStatus.BorderWidth = 0;
                colClienteTempTotalStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRFCTempTotalStatus = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colRFCTempTotalStatus.BorderWidth = 0;
                colRFCTempTotalStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colTotalTempTotalStatus = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colTotalTempTotalStatus.BorderWidth = 0;
                colTotalTempTotalStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFolioTempTotalStatus = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colFolioTempTotalStatus.BorderWidth = 0;
                colFolioTempTotalStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colSerieTempTotalStatus = new PdfPCell(new Phrase("$" + precioAnteriorSumaStatus.ToString("0.00"), fuenteNormal));
                colSerieTempTotalStatus.BorderWidth = 0;
                colSerieTempTotalStatus.BorderWidthBottom = 1;
                colSerieTempTotalStatus.HorizontalAlignment = Element.ALIGN_CENTER;
                colSerieTempTotalStatus.BackgroundColor = new BaseColor(Color.SkyBlue);

                PdfPCell colFechaTempTotalStatus = new PdfPCell(new Phrase("$" + precioNuevoSumaStatus.ToString("0.00"), fuenteNormal));
                colFechaTempTotalStatus.BorderWidth = 0;
                colFechaTempTotalStatus.BorderWidthBottom = 1;
                colFechaTempTotalStatus.HorizontalAlignment = Element.ALIGN_CENTER;
                colFechaTempTotalStatus.BackgroundColor = new BaseColor(Color.SkyBlue);

                PdfPCell colEmpleadoTempTotalStatus = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colEmpleadoTempTotalStatus.BorderWidth = 0;
                colEmpleadoTempTotalStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colEmpleadoTempTotalsegundoStatus = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colEmpleadoTempTotalsegundoStatus.BorderWidth = 0;
                colEmpleadoTempTotalsegundoStatus.HorizontalAlignment = Element.ALIGN_CENTER;

                if (estado)
                {
                    tablasStatus.AddCell(colNumFilatempTotalStatus);
                    tablasStatus.AddCell(colClienteTempTotalStatus);
                    tablasStatus.AddCell(colRFCTempTotalStatus);
                    tablasStatus.AddCell(colTotalTempTotalStatus);
                    tablasStatus.AddCell(colFolioTempTotalStatus);
                    tablasStatus.AddCell(colSerieTempTotalStatus);
                    tablasStatus.AddCell(colFechaTempTotalStatus);
                    tablasStatus.AddCell(colEmpleadoTempTotalStatus);
                    tablasStatus.AddCell(colEmpleadoTempTotalsegundoStatus);

                    reporte.Add(status0);
                    reporte.Add(Chunk.NEWLINE);


                }
                reporte.Add(tablasStatus);
            }

            reporte.AddTitle("Reporte Historial Precios");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.ShowDialog();
        }



        private void btnHistorialDineroAgregado_Click(object sender, EventArgs e)
        {
            if (opcion2 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<FechasReportes>().Count() == 1)
            {
                Application.OpenForms.OfType<FechasReportes>().First().BringToFront();
            }
            else
            {
                var fechas = new FechasReportes("CAJA");

                fechas.FormClosed += delegate
                {
                    if (botonAceptar)
                    {
                        botonAceptar = false;

                        concepto = fechas.concepto;
                        fechaInicial = fechas.fechaInicial;
                        fechaFinal = fechas.fechaFinal;

                        if (!string.IsNullOrWhiteSpace(fechaInicial))
                        {
                            if (!string.IsNullOrWhiteSpace(fechaFinal))
                            {
                                if (Utilidades.AdobeReaderInstalado())
                                {
                                    GenerarReporteDineroAgregado();
                                }
                                else
                                {
                                    Utilidades.MensajeAdobeReader();
                                }
                            }
                        }
                    }
                };

                fechas.Show();
            }
        }

        private void GenerarReporteDineroAgregado()
        {
            var consultaGeneral = string.Empty;

            if (concepto.Equals("Seleccionar concepto..."))
            {
                concepto = string.Empty;
            }

            if (string.IsNullOrWhiteSpace(concepto))
            {
                consultaGeneral = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'deposito' AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' ORDER BY FechaOperacion ASC";
            }
            else
            {
                consultaGeneral = $"SELECT * FROM Caja WHERE IDUsuario = {FormPrincipal.userID} AND Operacion = 'deposito' AND Concepto = '{concepto}' AND DATE(FechaOperacion) BETWEEN '{fechaInicial}' AND '{fechaFinal}' ORDER BY FechaOperacion ASC";
            }

            //==================================================================================================

            using (DataTable dtDineroAgregadoResultado = cn.CargarDatos(consultaGeneral))
            {
                if (dtDineroAgregadoResultado.Rows.Count > 0)
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
                    var fuenteTotales = FontFactory.GetFont(FontFactory.HELVETICA, 10, 1, colorFuenteBlanca);

                    // Ruta donde se creara el archivo PDF
                    var rutaArchivo = @"C:\Archivos PUDVE\Reportes\Caja\reporte_Dinero_Agregado_Por_Fechas_" + fechaInicial + "_Al_" + fechaFinal + ".pdf";

                    Document reporte = new Document(PageSize.A3);
                    PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

                    reporte.Open();

                    Paragraph titulo = new Paragraph(datos[0], fuenteGrande);
                    Paragraph subTitulo = new Paragraph("DINERO AGREGADO\nFechas: " + fechaInicial + " al " + fechaFinal + "\n\n", fuenteNormal);

                    titulo.Alignment = Element.ALIGN_CENTER;
                    subTitulo.Alignment = Element.ALIGN_CENTER;

                    reporte.Add(titulo);
                    reporte.Add(subTitulo);

                    //=====================================
                    //===    TABLA DE DINERO AGREGADO   ===
                    //=====================================
                    #region Tabla de Dinero Agregado
                    float[] anchoColumnas = new float[] { 100f, 100f, 100f, 100f, 100f, 100f, 100f };

                    Paragraph tituloDineroAgregado = new Paragraph("HISTORIAL DE DINERO AGREGADO", fuenteGrande);
                    tituloDineroAgregado.Alignment = Element.ALIGN_CENTER;

                    reporte.Add(tituloDineroAgregado);

                    // Linea serapadora
                    Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

                    //reporte.Add(linea);

                    PdfPTable tablaDineroAgregado = new PdfPTable(7);
                    tablaDineroAgregado.WidthPercentage = 100;
                    tablaDineroAgregado.SetWidths(anchoColumnas);

                    PdfPCell colEmpleado = new PdfPCell(new Phrase("EMPLEADO", fuenteNegrita));
                    colEmpleado.BorderWidth = 0;
                    colEmpleado.HorizontalAlignment = Element.ALIGN_CENTER;
                    colEmpleado.Padding = 3;

                    PdfPCell colDepositoEfectivo = new PdfPCell(new Phrase("EFECTIVO", fuenteNegrita));
                    colDepositoEfectivo.BorderWidth = 0;
                    colDepositoEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;
                    colDepositoEfectivo.Padding = 3;

                    PdfPCell colDepositoTarjeta = new PdfPCell(new Phrase("TARJETA", fuenteNegrita));
                    colDepositoTarjeta.BorderWidth = 0;
                    colDepositoTarjeta.HorizontalAlignment = Element.ALIGN_CENTER;
                    colDepositoTarjeta.Padding = 3;

                    PdfPCell colDepositoVales = new PdfPCell(new Phrase("VALES", fuenteNegrita));
                    colDepositoVales.BorderWidth = 0;
                    colDepositoVales.HorizontalAlignment = Element.ALIGN_CENTER;
                    colDepositoVales.Padding = 3;

                    PdfPCell colDepositoCheque = new PdfPCell(new Phrase("CHEQUE", fuenteNegrita));
                    colDepositoCheque.BorderWidth = 0;
                    colDepositoCheque.HorizontalAlignment = Element.ALIGN_CENTER;
                    colDepositoCheque.Padding = 3;

                    PdfPCell colDepositoTrans = new PdfPCell(new Phrase("TRANSFERENCIA", fuenteNegrita));
                    colDepositoTrans.BorderWidth = 0;
                    colDepositoTrans.HorizontalAlignment = Element.ALIGN_CENTER;
                    colDepositoTrans.Padding = 3;

                    PdfPCell colDepositoFecha = new PdfPCell(new Phrase("FECHA", fuenteNegrita));
                    colDepositoFecha.BorderWidth = 0;
                    colDepositoFecha.HorizontalAlignment = Element.ALIGN_CENTER;
                    colDepositoFecha.Padding = 3;

                    tablaDineroAgregado.AddCell(colEmpleado);
                    tablaDineroAgregado.AddCell(colDepositoEfectivo);
                    tablaDineroAgregado.AddCell(colDepositoTarjeta);
                    tablaDineroAgregado.AddCell(colDepositoVales);
                    tablaDineroAgregado.AddCell(colDepositoCheque);
                    tablaDineroAgregado.AddCell(colDepositoTrans);
                    tablaDineroAgregado.AddCell(colDepositoFecha);

                    //Varaibles para los Totales
                    float totalEfectivo = 0,
                          totalTarjeta = 0,
                          totalVales = 0,
                          totalCheque = 0,
                          totalTransferencia = 0;

                    float subEfectivo = 0,
                          subTarjeta = 0,
                          subVales = 0,
                          subCheque = 0,
                          subTransferencia = 0;


                    var fechaAuxiliar = "0000-00-00";
                    var longitud = dtDineroAgregadoResultado.Rows.Count - 1;
                    var contador = 0;

                    foreach (DataRow row in dtDineroAgregadoResultado.Rows)
                    {

                        string Empleado = string.Empty,
                                Efectivo = string.Empty,
                                Tarjeta = string.Empty,
                                Vales = string.Empty,
                                Cheque = string.Empty,
                                Transferencia = string.Empty,
                                Fecha = string.Empty;

                        var fechaAux = Convert.ToDateTime(row["FechaOperacion"].ToString());
                        var fecha = fechaAux.ToString("yyyy-MM-dd");

                        if (fechaAuxiliar != "0000-00-00")
                        {
                            if (fecha != fechaAuxiliar)
                            {
                                fechaAuxiliar = fecha;

                                var cantidades = new float[] { subEfectivo, subTarjeta, subVales, subCheque, subTransferencia };

                                ColumnaSubtotal(cantidades, fuenteTotales, tablaDineroAgregado);

                                subEfectivo = 0;
                                subTarjeta = 0;
                                subVales = 0;
                                subCheque = 0;
                                subTransferencia = 0;

                                tablaDineroAgregado.AddCell(colEmpleado);
                                tablaDineroAgregado.AddCell(colDepositoEfectivo);
                                tablaDineroAgregado.AddCell(colDepositoTarjeta);
                                tablaDineroAgregado.AddCell(colDepositoVales);
                                tablaDineroAgregado.AddCell(colDepositoCheque);
                                tablaDineroAgregado.AddCell(colDepositoTrans);
                                tablaDineroAgregado.AddCell(colDepositoFecha);
                            }
                        }
                        else
                        {
                            fechaAuxiliar = fecha;
                        }


                        Empleado = "ADMIN";

                        Efectivo = row["Efectivo"].ToString();
                        totalEfectivo += float.Parse(Efectivo);
                        subEfectivo += float.Parse(Efectivo);

                        Tarjeta = row["Tarjeta"].ToString();
                        totalTarjeta += float.Parse(Tarjeta);
                        subTarjeta += float.Parse(Tarjeta);

                        Vales = row["Vales"].ToString();
                        totalVales += float.Parse(Vales);
                        subVales += float.Parse(Vales);

                        Cheque = row["Cheque"].ToString();
                        totalCheque += float.Parse(Cheque);
                        subCheque += float.Parse(Cheque);

                        Transferencia = row["Transferencia"].ToString();
                        totalTransferencia += float.Parse(Transferencia);
                        subTransferencia += float.Parse(Transferencia);

                        Fecha = row["FechaOperacion"].ToString();

                        PdfPCell colEmpleadoTmp = new PdfPCell(new Phrase(Empleado, fuenteNormal));
                        colEmpleadoTmp.BorderWidth = 0;
                        colEmpleadoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colDepositoEfectivoTmp = new PdfPCell(new Phrase("$ " + Efectivo, fuenteNormal));
                        colDepositoEfectivoTmp.BorderWidth = 0;
                        colDepositoEfectivoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colDepositoTarjetaTmp = new PdfPCell(new Phrase("$ " + Tarjeta, fuenteNormal));
                        colDepositoTarjetaTmp.BorderWidth = 0;
                        colDepositoTarjetaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colDepositoValesTmp = new PdfPCell(new Phrase("$ " + Vales, fuenteNormal));
                        colDepositoValesTmp.BorderWidth = 0;
                        colDepositoValesTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colDepositoChequeTmp = new PdfPCell(new Phrase("$ " + Cheque, fuenteNormal));
                        colDepositoChequeTmp.BorderWidth = 0;
                        colDepositoChequeTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colDepositoTransTmp = new PdfPCell(new Phrase("$ " + Transferencia, fuenteNormal));
                        colDepositoTransTmp.BorderWidth = 0;
                        colDepositoTransTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                        PdfPCell colDepositoFechaTmp = new PdfPCell(new Phrase(Fecha, fuenteNormal));
                        colDepositoFechaTmp.BorderWidth = 0;
                        colDepositoFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                        tablaDineroAgregado.AddCell(colEmpleadoTmp);
                        tablaDineroAgregado.AddCell(colDepositoEfectivoTmp);
                        tablaDineroAgregado.AddCell(colDepositoTarjetaTmp);
                        tablaDineroAgregado.AddCell(colDepositoValesTmp);
                        tablaDineroAgregado.AddCell(colDepositoChequeTmp);
                        tablaDineroAgregado.AddCell(colDepositoTransTmp);
                        tablaDineroAgregado.AddCell(colDepositoFechaTmp);


                        if (contador == longitud)
                        {
                            var cantidades = new float[] { subEfectivo, subTarjeta, subVales, subCheque, subTransferencia };

                            ColumnaSubtotal(cantidades, fuenteTotales, tablaDineroAgregado);
                        }

                        contador++;
                    }

                    reporte.Add(tablaDineroAgregado);
                    //reporte.Add(linea);

                    PdfPTable tablaTotalesDineroAgregado = new PdfPTable(7);
                    tablaTotalesDineroAgregado.WidthPercentage = 100;
                    tablaTotalesDineroAgregado.SpacingBefore = 10;
                    tablaTotalesDineroAgregado.SetWidths(anchoColumnas);

                    // Linea de TOTALES
                    PdfPCell colEmpleadoTotal = new PdfPCell(new Phrase($"TOTAL", fuenteTotales));
                    colEmpleadoTotal.BorderWidth = 0;
                    colEmpleadoTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                    colEmpleadoTotal.Padding = 3;
                    colEmpleadoTotal.BackgroundColor = new BaseColor(Color.Red);

                    PdfPCell colEfectivoTotal = new PdfPCell(new Phrase("$ " + totalEfectivo.ToString("N2"), fuenteTotales));
                    colEfectivoTotal.BorderWidth = 0;
                    colEfectivoTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                    colEfectivoTotal.Padding = 3;
                    colEfectivoTotal.BackgroundColor = new BaseColor(Color.Red);

                    PdfPCell colTarjetaTotal = new PdfPCell(new Phrase("$ " + totalTarjeta.ToString("N2"), fuenteTotales));
                    colTarjetaTotal.BorderWidth = 0;
                    colTarjetaTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                    colTarjetaTotal.Padding = 3;
                    colTarjetaTotal.BackgroundColor = new BaseColor(Color.Red);

                    PdfPCell colValesTotal = new PdfPCell(new Phrase("$ " + totalVales.ToString("N2"), fuenteTotales));
                    colValesTotal.BorderWidth = 0;
                    colValesTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                    colValesTotal.Padding = 3;
                    colValesTotal.BackgroundColor = new BaseColor(Color.Red);

                    PdfPCell colChequeTotal = new PdfPCell(new Phrase("$ " + totalCheque.ToString("N2"), fuenteTotales));
                    colChequeTotal.BorderWidth = 0;
                    colChequeTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                    colChequeTotal.Padding = 3;
                    colChequeTotal.BackgroundColor = new BaseColor(Color.Red);

                    PdfPCell colTransaccionTotal = new PdfPCell(new Phrase("$ " + totalTransferencia.ToString("N2"), fuenteTotales));
                    colTransaccionTotal.BorderWidth = 0;
                    colTransaccionTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                    colTransaccionTotal.Padding = 3;
                    colTransaccionTotal.BackgroundColor = new BaseColor(Color.Red);

                    PdfPCell colFechaTotal = new PdfPCell(new Phrase("", fuenteTotales));
                    colFechaTotal.BorderWidth = 0;
                    colFechaTotal.HorizontalAlignment = Element.ALIGN_CENTER;
                    colFechaTotal.Padding = 3;
                    colFechaTotal.BackgroundColor = new BaseColor(Color.Red);

                    tablaTotalesDineroAgregado.AddCell(colEmpleadoTotal);
                    tablaTotalesDineroAgregado.AddCell(colEfectivoTotal);
                    tablaTotalesDineroAgregado.AddCell(colTarjetaTotal);
                    tablaTotalesDineroAgregado.AddCell(colValesTotal);
                    tablaTotalesDineroAgregado.AddCell(colChequeTotal);
                    tablaTotalesDineroAgregado.AddCell(colTransaccionTotal);
                    tablaTotalesDineroAgregado.AddCell(colFechaTotal);

                    reporte.Add(tablaTotalesDineroAgregado);
                    #endregion Tabla de Dinero Agregado
                    //=====================================
                    //=== FIN TABLA DE DINERO AGREGADO  ===
                    //=====================================
                    reporte.AddTitle("Reporte Dinero Agregado Por Fechas");
                    reporte.AddAuthor("PUDVE");
                    reporte.Close();
                    writer.Close();

                    VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
                    vr.ShowDialog();
                }
                else if (dtDineroAgregadoResultado.Rows.Count <= 0)
                {
                    MessageBox.Show("El rango de fechas que usted ha seleccionado\nNo contiene información para generar el reporte", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ColumnaSubtotal(float[] datos, iTextSharp.text.Font fuenteTotales, PdfPTable tabla)
        {
            PdfPCell colSubTotal = new PdfPCell(new Phrase($"SUBTOTAL", fuenteTotales));
            colSubTotal.BorderWidth = 0;
            colSubTotal.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubTotal.Padding = 3;
            colSubTotal.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colSubEfectivo = new PdfPCell(new Phrase("$ " + datos[0].ToString("N2"), fuenteTotales));
            colSubEfectivo.BorderWidth = 0;
            colSubEfectivo.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubEfectivo.Padding = 3;
            colSubEfectivo.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colSubTarjeta = new PdfPCell(new Phrase("$ " + datos[1].ToString("N2"), fuenteTotales));
            colSubTarjeta.BorderWidth = 0;
            colSubTarjeta.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubTarjeta.Padding = 3;
            colSubTarjeta.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colSubVales = new PdfPCell(new Phrase("$ " + datos[2].ToString("N2"), fuenteTotales));
            colSubVales.BorderWidth = 0;
            colSubVales.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubVales.Padding = 3;
            colSubVales.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colSubCheque = new PdfPCell(new Phrase("$ " + datos[3].ToString("N2"), fuenteTotales));
            colSubCheque.BorderWidth = 0;
            colSubCheque.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubCheque.Padding = 3;
            colSubCheque.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colSubTrans = new PdfPCell(new Phrase("$ " + datos[4].ToString("N2"), fuenteTotales));
            colSubTrans.BorderWidth = 0;
            colSubTrans.HorizontalAlignment = Element.ALIGN_CENTER;
            colSubTrans.Padding = 3;
            colSubTrans.BackgroundColor = new BaseColor(Color.Red);

            PdfPCell colAuxiliar = new PdfPCell(new Phrase("", fuenteTotales));
            colAuxiliar.BorderWidth = 0;
            colAuxiliar.HorizontalAlignment = Element.ALIGN_CENTER;
            colAuxiliar.Padding = 3;
            colAuxiliar.BackgroundColor = new BaseColor(Color.Red);

            tabla.AddCell(colSubTotal);
            tabla.AddCell(colSubEfectivo);
            tabla.AddCell(colSubTarjeta);
            tabla.AddCell(colSubVales);
            tabla.AddCell(colSubCheque);
            tabla.AddCell(colSubTrans);
            tabla.AddCell(colAuxiliar);
        }

        private void Reportes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnHistorialPrecios_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void btnHistorialDineroAgregado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                Ventas mostrarVentas = new Ventas();
                mostrarVentas.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnReporteInventario_Click(object sender, EventArgs e)
        {
            TipoReportesInventario TRPInventario = new TipoReportesInventario();

            TRPInventario.ShowDialog();

            //BuscadorReporteInventario BRInventario = new BuscadorReporteInventario();
            //BRInventario.ShowDialog();
        }

        private void cargarDatos()
        {
            DGVInventario.Rows.Clear();

            var numRevision = string.Empty;
            var nameUser = string.Empty;
            var fecha = string.Empty;
            System.Drawing.Image icono = System.Drawing.Image.FromFile(Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\black16\file-pdf-o.png");


            var query = cn.CargarDatos($"SELECT NoRevision, NameUsr, Fecha FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}' GROUP BY NoRevision ORDER BY Fecha DESC");

            if (!query.Rows.Count.Equals(0))
            {
                foreach (DataRow id in query.Rows)
                {
                    numRevision = id["NoRevision"].ToString();
                    nameUser = id["NameUsr"].ToString();
                    fecha = id["Fecha"].ToString();

                    DGVInventario.Rows.Add(numRevision, nameUser, fecha, icono);
                }
            }
        }

        private void DGVInventario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                var mostrarClave = FormPrincipal.clave;
                int numRev = Convert.ToInt32(DGVInventario.Rows[e.RowIndex].Cells[0].Value.ToString());

                if (mostrarClave == 0)
                {
                    GenerarReporteSinCLaveInterna(numRev);
                }
                else if (mostrarClave == 1)
                {
                    GenerarReporte(numRev);
                }
            }
        }

        private void GenerarReporte(int num)
        {
            var mostrarClave = FormPrincipal.clave;

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
            //var servidor = Properties.Settings.Default.Hosting;
            //var rutaArchivo = string.Empty;
            /*if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            }*/

            var fechaHoy = DateTime.Now;
            var rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            string UsuarioActivo = string.Empty;

            string tipoReporte = string.Empty,
                    encabezadoTipoReporte = string.Empty;

            float PuntoDeVenta = 0,
                    StockFisico = 0,
                    Diferencia = 0,
                    Precio = 0,
                    CantidadPerdida = 0,
                    CantidadRecuperada = 0;

            tipoReporte = Inventario.filtradoParaRealizar;

            if (!tipoReporte.Equals(string.Empty))
            {
                if (tipoReporte.Equals("Normal"))
                {
                    encabezadoTipoReporte = "Normal";
                }
                if (tipoReporte.Equals("Stock"))
                {
                    encabezadoTipoReporte = "Stock";
                }
                if (tipoReporte.Equals("StockMinimo"))
                {
                    encabezadoTipoReporte = "Stock Minimo";
                }
                if (tipoReporte.Equals("StockNecesario"))
                {
                    encabezadoTipoReporte = "Stock Necesario";
                }
                if (tipoReporte.Equals("NumeroRevision"))
                {
                    encabezadoTipoReporte = "Numero Revision";
                }
                if (tipoReporte.Equals("Filtros"))
                {
                    encabezadoTipoReporte = "Filtros";
                }
            }

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

            Paragraph subTitulo = new Paragraph("REPORTE DE REVISAR INVENTARIO\nSECCIÓN ELEGIDA " + encabezadoTipoReporte.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;


            float[] anchoColumnas = new float[] { 30f, 270f, 80f, 80f, 80f, 80f, 90f, 70f, 70f, 80f, 100f };

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //============================
            //=== TABLA DE INVENTARIO  ===
            //============================

            PdfPTable tablaInventario = new PdfPTable(11);
            tablaInventario.WidthPercentage = 100;
            tablaInventario.SetWidths(anchoColumnas);

            PdfPCell colNoConcepto = new PdfPCell(new Phrase("No:", fuenteNegrita));
            colNoConcepto.BorderWidth = 1;
            colNoConcepto.BackgroundColor = new BaseColor(Color.SkyBlue);
            colNoConcepto.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colNombre = new PdfPCell(new Phrase("NOMBRE", fuenteTotales));
            colNombre.BorderWidth = 1;
            colNombre.HorizontalAlignment = Element.ALIGN_CENTER;
            colNombre.Padding = 3;
            colNombre.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colClave = new PdfPCell(new Phrase("CLAVE", fuenteTotales));
            colClave.BorderWidth = 1;
            colClave.HorizontalAlignment = Element.ALIGN_CENTER;
            colClave.Padding = 3;
            colClave.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colCodigo = new PdfPCell(new Phrase("CÓDIGO", fuenteTotales));
            colCodigo.BorderWidth = 1;
            colCodigo.HorizontalAlignment = Element.ALIGN_CENTER;
            colCodigo.Padding = 3;
            colCodigo.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPuntoVenta = new PdfPCell(new Phrase("PUNTO DE VENTA", fuenteTotales));
            colPuntoVenta.BorderWidth = 1;
            colPuntoVenta.HorizontalAlignment = Element.ALIGN_CENTER;
            colPuntoVenta.Padding = 3;
            colPuntoVenta.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colStockFisico = new PdfPCell(new Phrase("STOCK FISICO", fuenteTotales));
            colStockFisico.BorderWidth = 1;
            colStockFisico.HorizontalAlignment = Element.ALIGN_CENTER;
            colStockFisico.Padding = 3;
            colStockFisico.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colFecha = new PdfPCell(new Phrase("FECHA", fuenteTotales));
            colFecha.BorderWidth = 1;
            colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            colFecha.Padding = 3;
            colFecha.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colDiferencia = new PdfPCell(new Phrase("DIFERENCIA", fuenteTotales));
            colDiferencia.BorderWidth = 1;
            colDiferencia.HorizontalAlignment = Element.ALIGN_CENTER;
            colDiferencia.Padding = 3;
            colDiferencia.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPrecio = new PdfPCell(new Phrase("PRECIO", fuenteTotales));
            colPrecio.BorderWidth = 1;
            colPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
            colPrecio.Padding = 3;
            colPrecio.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPerdida = new PdfPCell(new Phrase("CANTIDAD PERDIDA", fuenteTotales));
            colPerdida.BorderWidth = 1;
            colPerdida.HorizontalAlignment = Element.ALIGN_CENTER;
            colPerdida.Padding = 3;
            colPerdida.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colRecuperada = new PdfPCell(new Phrase("CANTIDAD RECUPERADA", fuenteTotales));
            colRecuperada.BorderWidth = 1;
            colRecuperada.HorizontalAlignment = Element.ALIGN_CENTER;
            colRecuperada.Padding = 3;
            colRecuperada.BackgroundColor = new BaseColor(Color.SkyBlue);

            tablaInventario.AddCell(colNoConcepto);
            tablaInventario.AddCell(colNombre);
            tablaInventario.AddCell(colClave);
            tablaInventario.AddCell(colCodigo);
            tablaInventario.AddCell(colPuntoVenta);
            tablaInventario.AddCell(colStockFisico);
            tablaInventario.AddCell(colFecha);
            tablaInventario.AddCell(colDiferencia);
            tablaInventario.AddCell(colPrecio);
            tablaInventario.AddCell(colPerdida);
            tablaInventario.AddCell(colRecuperada);

            var consulta = cn.CargarDatos($"SELECT * FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}' AND NoRevision = '{num}'");

            foreach (DataRow row in consulta.Rows)
            {
                //var nombre = row.Cells["Nombre"].Value.ToString();
                //var clave = row.Cells["ClaveInterna"].Value.ToString();
                //var codigo = row.Cells["CodigoBarras"].Value.ToString();
                ////var almacen = Utilidades.RemoverCeroStock(row.Cells["StockAlmacen"].Value.ToString());
                ////var fisico = Utilidades.RemoverCeroStock(row.Cells["StockFisico"].Value.ToString());
                //var almacen = row.Cells["StockAlmacen"].Value.ToString();
                //var fisico = row.Cells["StockFisico"].Value.ToString();
                //var fecha = row.Cells["Fecha"].Value.ToString();
                //var diferencia = row.Cells["Diferencia"].Value.ToString();
                //var precio = float.Parse(row.Cells["PrecioProducto"].Value.ToString());
                //var perdida = row.Cells["Perdida"].Value.ToString();
                //var recuperada = row.Cells["Recuperada"].Value.ToString();

                var nombre = row["Nombre"].ToString();
                var clave = row["ClaveInterna"].ToString();
                var codigo = row["CodigoBarras"].ToString();
                var almacen = row["StockAlmacen"].ToString();
                var fisico = row["StockFisico"].ToString();
                var fecha = row["Fecha"].ToString();
                var diferencia = row["Diferencia"].ToString();
                var precio = float.Parse(row["PrecioProducto"].ToString());
                var perdida = string.Empty;
                var recuperada = string.Empty;

                if (float.Parse(diferencia) < 0)
                {
                    perdida = (float.Parse(diferencia) * precio).ToString();
                    recuperada = "0";
                }
                else if (float.Parse(diferencia) > 0)
                {
                    recuperada = (float.Parse(diferencia) * precio).ToString();
                    perdida = "0";
                }
                else
                {
                    recuperada = "0";
                    perdida = "0";
                }

                /*var perdida =*/ /*row["Perdida"].ToString();*/
                                  /* var recuperada =*/ /*row["Recuperada"].ToString();*/

                numRow++;
                PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                colNoConceptoTmp.BorderWidth = 1;
                colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colNombreTmp = new PdfPCell(new Phrase(nombre, fuenteNormal));
                colNombreTmp.BorderWidth = 1;
                colNombreTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colClaveTmp = new PdfPCell(new Phrase(clave, fuenteNormal));
                colClaveTmp.BorderWidth = 1;
                colClaveTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoTmp = new PdfPCell(new Phrase(codigo, fuenteNormal));
                colCodigoTmp.BorderWidth = 1;
                colCodigoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPuntoVentaTmp = new PdfPCell(new Phrase(almacen, fuenteNormal));
                PuntoDeVenta += (float)Convert.ToDouble(almacen);
                colPuntoVentaTmp.BorderWidth = 1;
                colPuntoVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmp = new PdfPCell(new Phrase(fisico, fuenteNormal));
                StockFisico += (float)Convert.ToDouble(fisico);
                colStockFisicoTmp.BorderWidth = 1;
                colStockFisicoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
                colFechaTmp.BorderWidth = 1;
                colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDiferenciaTmp = new PdfPCell(new Phrase(diferencia, fuenteNormal));
                Diferencia += (float)Convert.ToDouble(diferencia);
                colDiferenciaTmp.BorderWidth = 1;
                colDiferenciaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmp = new PdfPCell(new Phrase(precio.ToString("0.00"), fuenteNormal));
                Precio += (float)Convert.ToDouble(precio);
                colPrecioTmp.BorderWidth = 1;
                colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPerdidaTmp = new PdfPCell(new Phrase(perdida, fuenteNormal));
                if (!perdida.Equals("---"))
                {
                    CantidadPerdida += (float)Convert.ToDouble(perdida);
                }
                colPerdidaTmp.BorderWidth = 1;
                colPerdidaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRecuperadaTmp = new PdfPCell(new Phrase(recuperada, fuenteNormal));
                if (!recuperada.Equals("---"))
                {
                    CantidadRecuperada += (float)Convert.ToDouble(recuperada);
                }
                colRecuperadaTmp.BorderWidth = 1;
                colRecuperadaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmp);
                tablaInventario.AddCell(colNombreTmp);
                tablaInventario.AddCell(colClaveTmp);
                tablaInventario.AddCell(colCodigoTmp);
                tablaInventario.AddCell(colPuntoVentaTmp);
                tablaInventario.AddCell(colStockFisicoTmp);
                tablaInventario.AddCell(colFechaTmp);
                tablaInventario.AddCell(colDiferenciaTmp);
                tablaInventario.AddCell(colPrecioTmp);
                tablaInventario.AddCell(colPerdidaTmp);
                tablaInventario.AddCell(colRecuperadaTmp);
            }

            if (PuntoDeVenta > 0 || StockFisico > 0)
            {
                PdfPCell colNoConceptoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNoConceptoTmpExtra.BorderWidth = 0;
                colNoConceptoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colNombreTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNombreTmpExtra.BorderWidth = 0;
                colNombreTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colClaveTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colClaveTmpExtra.BorderWidth = 0;
                colClaveTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colCodigoTmpExtra.BorderWidth = 0;
                colCodigoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPuntoVentaTmpExtra = new PdfPCell(new Phrase(PuntoDeVenta.ToString("N2"), fuenteNormal));
                colPuntoVentaTmpExtra.BorderWidthTop = 0;
                colPuntoVentaTmpExtra.BorderWidthLeft = 0;
                colPuntoVentaTmpExtra.BorderWidthRight = 0;
                colPuntoVentaTmpExtra.BorderWidthBottom = 1;
                colPuntoVentaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPuntoVentaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmpExtra = new PdfPCell(new Phrase(StockFisico.ToString("N2"), fuenteNormal));
                colStockFisicoTmpExtra.BorderWidthTop = 0;
                colStockFisicoTmpExtra.BorderWidthLeft = 0;
                colStockFisicoTmpExtra.BorderWidthRight = 0;
                colStockFisicoTmpExtra.BorderWidthBottom = 1;
                colStockFisicoTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colStockFisicoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colFechaTmpExtra.BorderWidth = 0;
                colFechaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDiferenciaTmpExtra = new PdfPCell(new Phrase(Diferencia.ToString("N2"), fuenteNormal));
                colDiferenciaTmpExtra.BorderWidthTop = 0;
                colDiferenciaTmpExtra.BorderWidthLeft = 0;
                colDiferenciaTmpExtra.BorderWidthRight = 0;
                colDiferenciaTmpExtra.BorderWidthBottom = 1;
                colDiferenciaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colDiferenciaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmpExtra = new PdfPCell(new Phrase(Precio.ToString("C"), fuenteNormal));
                colPrecioTmpExtra.BorderWidthTop = 0;
                colPrecioTmpExtra.BorderWidthLeft = 0;
                colPrecioTmpExtra.BorderWidthRight = 0;
                colPrecioTmpExtra.BorderWidthBottom = 1;
                colPrecioTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPrecioTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPerdidaTmpExtra = new PdfPCell(new Phrase(CantidadPerdida.ToString("N2"), fuenteNormal));
                colPerdidaTmpExtra.BorderWidthTop = 0;
                colPerdidaTmpExtra.BorderWidthLeft = 0;
                colPerdidaTmpExtra.BorderWidthRight = 0;
                colPerdidaTmpExtra.BorderWidthBottom = 1;
                colPerdidaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPerdidaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRecuperadaTmpExtra = new PdfPCell(new Phrase(CantidadRecuperada.ToString("N2"), fuenteNormal));
                colRecuperadaTmpExtra.BorderWidthTop = 0;
                colRecuperadaTmpExtra.BorderWidthLeft = 0;
                colRecuperadaTmpExtra.BorderWidthRight = 0;
                colRecuperadaTmpExtra.BorderWidthBottom = 1;
                colRecuperadaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colRecuperadaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmpExtra);
                tablaInventario.AddCell(colNombreTmpExtra);
                tablaInventario.AddCell(colClaveTmpExtra);
                tablaInventario.AddCell(colCodigoTmpExtra);
                tablaInventario.AddCell(colPuntoVentaTmpExtra);
                tablaInventario.AddCell(colStockFisicoTmpExtra);
                tablaInventario.AddCell(colFechaTmpExtra);
                tablaInventario.AddCell(colDiferenciaTmpExtra);
                tablaInventario.AddCell(colPrecioTmpExtra);
                tablaInventario.AddCell(colPerdidaTmpExtra);
                tablaInventario.AddCell(colRecuperadaTmpExtra);
            }

            reporte.Add(titulo);
            reporte.Add(Usuario);
            reporte.Add(subTitulo);
            reporte.Add(tablaInventario);

            //================================
            //=== FIN TABLA DE INVENTARIO  ===
            //================================

            reporte.AddTitle("Reporte Inventario");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.Show();
        }

        private void GenerarReporteSinCLaveInterna(int num)
        {
            var mostrarClave = FormPrincipal.clave;

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
            //var servidor = Properties.Settings.Default.Hosting;
            //var rutaArchivo = string.Empty;
            /*if (!string.IsNullOrWhiteSpace(servidor))
            {
                rutaArchivo = $@"\\{servidor}\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            }
            else
            {
                rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";
            }*/

            var fechaHoy = DateTime.Now;
            var rutaArchivo = @"C:\Archivos PUDVE\Reportes\reporte_inventario.pdf";

            Document reporte = new Document(PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(reporte, new FileStream(rutaArchivo, FileMode.Create));

            reporte.Open();

            Paragraph titulo = new Paragraph(datos[0], fuenteGrande);

            Paragraph Usuario = new Paragraph("");

            string UsuarioActivo = string.Empty;

            string tipoReporte = string.Empty,
                    encabezadoTipoReporte = string.Empty;

            float PuntoDeVenta = 0,
                    StockFisico = 0,
                    Diferencia = 0,
                    Precio = 0,
                    CantidadPerdida = 0,
                    CantidadRecuperada = 0;

            tipoReporte = Inventario.filtradoParaRealizar;

            if (!tipoReporte.Equals(string.Empty))
            {
                if (tipoReporte.Equals("Normal"))
                {
                    encabezadoTipoReporte = "Normal";
                }
                if (tipoReporte.Equals("Stock"))
                {
                    encabezadoTipoReporte = "Stock";
                }
                if (tipoReporte.Equals("StockMinimo"))
                {
                    encabezadoTipoReporte = "Stock Minimo";
                }
                if (tipoReporte.Equals("StockNecesario"))
                {
                    encabezadoTipoReporte = "Stock Necesario";
                }
                if (tipoReporte.Equals("NumeroRevision"))
                {
                    encabezadoTipoReporte = "Numero Revision";
                }
                if (tipoReporte.Equals("Filtros"))
                {
                    encabezadoTipoReporte = "Filtros";
                }
            }

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

            Paragraph subTitulo = new Paragraph("REPORTE INVENTARIO\nSECCIÓN ELEGIDA " + encabezadoTipoReporte.ToUpper() + "\n\nFecha: " + fechaHoy.ToString("yyyy-MM-dd HH:mm:ss") + "\n\n\n", fuenteNormal);

            titulo.Alignment = Element.ALIGN_CENTER;
            Usuario.Alignment = Element.ALIGN_CENTER;
            subTitulo.Alignment = Element.ALIGN_CENTER;


            float[] anchoColumnas = new float[] { 30f, 270f, 80f, 80f, 80f, 90f, 70f, 70f, 80f, 100f };

            // Linea serapadora
            Paragraph linea = new Paragraph(new Chunk(new LineSeparator(0.0F, 100.0F, new BaseColor(Color.Black), Element.ALIGN_LEFT, 1)));

            //============================
            //=== TABLA DE INVENTARIO  ===
            //============================

            PdfPTable tablaInventario = new PdfPTable(10);
            tablaInventario.WidthPercentage = 100;
            tablaInventario.SetWidths(anchoColumnas);

            PdfPCell colNoConcepto = new PdfPCell(new Phrase("No:", fuenteNegrita));
            colNoConcepto.BorderWidth = 1;
            colNoConcepto.BackgroundColor = new BaseColor(Color.SkyBlue);
            colNoConcepto.HorizontalAlignment = Element.ALIGN_CENTER;

            PdfPCell colNombre = new PdfPCell(new Phrase("NOMBRE", fuenteTotales));
            colNombre.BorderWidth = 0;
            colNombre.HorizontalAlignment = Element.ALIGN_CENTER;
            colNombre.Padding = 3;
            colNombre.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colCodigo = new PdfPCell(new Phrase("CÓDIGO", fuenteTotales));
            colCodigo.BorderWidth = 0;
            colCodigo.HorizontalAlignment = Element.ALIGN_CENTER;
            colCodigo.Padding = 3;
            colCodigo.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPuntoVenta = new PdfPCell(new Phrase("PUNTO DE VENTA", fuenteTotales));
            colPuntoVenta.BorderWidth = 0;
            colPuntoVenta.HorizontalAlignment = Element.ALIGN_CENTER;
            colPuntoVenta.Padding = 3;
            colPuntoVenta.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colStockFisico = new PdfPCell(new Phrase("STOCK FISICO", fuenteTotales));
            colStockFisico.BorderWidth = 0;
            colStockFisico.HorizontalAlignment = Element.ALIGN_CENTER;
            colStockFisico.Padding = 3;
            colStockFisico.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colFecha = new PdfPCell(new Phrase("FECHA", fuenteTotales));
            colFecha.BorderWidth = 0;
            colFecha.HorizontalAlignment = Element.ALIGN_CENTER;
            colFecha.Padding = 3;
            colFecha.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colDiferencia = new PdfPCell(new Phrase("DIFERENCIA", fuenteTotales));
            colDiferencia.BorderWidth = 0;
            colDiferencia.HorizontalAlignment = Element.ALIGN_CENTER;
            colDiferencia.Padding = 3;
            colDiferencia.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPrecio = new PdfPCell(new Phrase("PRECIO", fuenteTotales));
            colPrecio.BorderWidth = 0;
            colPrecio.HorizontalAlignment = Element.ALIGN_CENTER;
            colPrecio.Padding = 3;
            colPrecio.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colPerdida = new PdfPCell(new Phrase("CANTIDAD PERDIDA", fuenteTotales));
            colPerdida.BorderWidth = 0;
            colPerdida.HorizontalAlignment = Element.ALIGN_CENTER;
            colPerdida.Padding = 3;
            colPerdida.BackgroundColor = new BaseColor(Color.SkyBlue);

            PdfPCell colRecuperada = new PdfPCell(new Phrase("CANTIDAD RECUPERADA", fuenteTotales));
            colRecuperada.BorderWidth = 0;
            colRecuperada.HorizontalAlignment = Element.ALIGN_CENTER;
            colRecuperada.Padding = 3;
            colRecuperada.BackgroundColor = new BaseColor(Color.SkyBlue);

            tablaInventario.AddCell(colNoConcepto);
            tablaInventario.AddCell(colNombre);
            tablaInventario.AddCell(colCodigo);
            tablaInventario.AddCell(colPuntoVenta);
            tablaInventario.AddCell(colStockFisico);
            tablaInventario.AddCell(colFecha);
            tablaInventario.AddCell(colDiferencia);
            tablaInventario.AddCell(colPrecio);
            tablaInventario.AddCell(colPerdida);
            tablaInventario.AddCell(colRecuperada);

            var consulta = cn.CargarDatos($"SELECT * FROM RevisarInventarioReportes WHERE IDUsuario = '{FormPrincipal.userID}' AND NoRevision = '{num}'");

            foreach (DataRow row in consulta.Rows)
            {
                var nombre = row["Nombre"].ToString();
                var clave = row["ClaveInterna"].ToString();
                var codigo = row["CodigoBarras"].ToString();
                var almacen = row["StockAlmacen"].ToString();
                var fisico = row["StockFisico"].ToString();
                var fecha = row["Fecha"].ToString();
                var diferencia = row["Diferencia"].ToString();
                var precio = float.Parse(row["PrecioProducto"].ToString());
                var perdida = string.Empty;
                var recuperada = string.Empty;

                if (float.Parse(diferencia) < 0)
                {
                    perdida = (float.Parse(diferencia) * precio).ToString();
                    recuperada = "0";
                }
                else if (float.Parse(diferencia) > 0)
                {
                    recuperada = (float.Parse(diferencia) * precio).ToString();
                    perdida = "0";
                }
                else
                {
                    recuperada = "0";
                    perdida = "0";
                }

                numRow++;
                PdfPCell colNoConceptoTmp = new PdfPCell(new Phrase(numRow.ToString(), fuenteNormal));
                colNoConceptoTmp.BorderWidth = 1;
                colNoConceptoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colNombreTmp = new PdfPCell(new Phrase(nombre, fuenteNormal));
                colNombreTmp.BorderWidth = 1;
                colNombreTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoTmp = new PdfPCell(new Phrase(codigo, fuenteNormal));
                colCodigoTmp.BorderWidth = 1;
                colCodigoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPuntoVentaTmp = new PdfPCell(new Phrase(almacen, fuenteNormal));
                PuntoDeVenta += (float)Convert.ToDouble(almacen);
                colPuntoVentaTmp.BorderWidth = 1;
                colPuntoVentaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmp = new PdfPCell(new Phrase(fisico, fuenteNormal));
                StockFisico += (float)Convert.ToDouble(fisico);
                colStockFisicoTmp.BorderWidth = 1;
                colStockFisicoTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmp = new PdfPCell(new Phrase(fecha, fuenteNormal));
                colFechaTmp.BorderWidth = 1;
                colFechaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDiferenciaTmp = new PdfPCell(new Phrase(diferencia, fuenteNormal));
                Diferencia += (float)Convert.ToDouble(diferencia);
                colDiferenciaTmp.BorderWidth = 1;
                colDiferenciaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmp = new PdfPCell(new Phrase(precio.ToString("0.00"), fuenteNormal));
                Precio += (float)Convert.ToDouble(precio);
                colPrecioTmp.BorderWidth = 1;
                colPrecioTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPerdidaTmp = new PdfPCell(new Phrase(perdida, fuenteNormal));
                if (!perdida.Equals("---"))
                {
                    CantidadPerdida += (float)Convert.ToDouble(perdida);
                }
                colPerdidaTmp.BorderWidth = 1;
                colPerdidaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRecuperadaTmp = new PdfPCell(new Phrase(recuperada, fuenteNormal));
                if (!recuperada.Equals("---"))
                {
                    CantidadRecuperada += (float)Convert.ToDouble(recuperada);
                }
                colRecuperadaTmp.BorderWidth = 1;
                colRecuperadaTmp.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmp);          // 01
                tablaInventario.AddCell(colNombreTmp);              // 02
                tablaInventario.AddCell(colCodigoTmp);              // 03
                tablaInventario.AddCell(colPuntoVentaTmp);          // 04
                tablaInventario.AddCell(colStockFisicoTmp);         // 05
                tablaInventario.AddCell(colFechaTmp);               // 06
                tablaInventario.AddCell(colDiferenciaTmp);          // 07
                tablaInventario.AddCell(colPrecioTmp);              // 08
                tablaInventario.AddCell(colPerdidaTmp);             // 09
                tablaInventario.AddCell(colRecuperadaTmp);          // 10
            }

            if (PuntoDeVenta > 0 || StockFisico > 0)
            {
                PdfPCell colNoConceptoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNoConceptoTmpExtra.BorderWidth = 0;
                colNoConceptoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colNombreTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colNombreTmpExtra.BorderWidth = 0;
                colNombreTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colCodigoTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colCodigoTmpExtra.BorderWidth = 0;
                colCodigoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPuntoVentaTmpExtra = new PdfPCell(new Phrase(PuntoDeVenta.ToString("N2"), fuenteNormal));
                colPuntoVentaTmpExtra.BorderWidthTop = 0;
                colPuntoVentaTmpExtra.BorderWidthLeft = 0;
                colPuntoVentaTmpExtra.BorderWidthRight = 0;
                colPuntoVentaTmpExtra.BorderWidthBottom = 1;
                colPuntoVentaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPuntoVentaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colStockFisicoTmpExtra = new PdfPCell(new Phrase(StockFisico.ToString("N2"), fuenteNormal));
                colStockFisicoTmpExtra.BorderWidthTop = 0;
                colStockFisicoTmpExtra.BorderWidthLeft = 0;
                colStockFisicoTmpExtra.BorderWidthRight = 0;
                colStockFisicoTmpExtra.BorderWidthBottom = 1;
                colStockFisicoTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colStockFisicoTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colFechaTmpExtra = new PdfPCell(new Phrase(string.Empty, fuenteNormal));
                colFechaTmpExtra.BorderWidth = 0;
                colFechaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colDiferenciaTmpExtra = new PdfPCell(new Phrase(Diferencia.ToString("N2"), fuenteNormal));
                colDiferenciaTmpExtra.BorderWidthTop = 0;
                colDiferenciaTmpExtra.BorderWidthLeft = 0;
                colDiferenciaTmpExtra.BorderWidthRight = 0;
                colDiferenciaTmpExtra.BorderWidthBottom = 1;
                colDiferenciaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colDiferenciaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPrecioTmpExtra = new PdfPCell(new Phrase(Precio.ToString("C"), fuenteNormal));
                colPrecioTmpExtra.BorderWidthTop = 0;
                colPrecioTmpExtra.BorderWidthLeft = 0;
                colPrecioTmpExtra.BorderWidthRight = 0;
                colPrecioTmpExtra.BorderWidthBottom = 1;
                colPrecioTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPrecioTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colPerdidaTmpExtra = new PdfPCell(new Phrase(CantidadPerdida.ToString("N2"), fuenteNormal));
                colPerdidaTmpExtra.BorderWidthTop = 0;
                colPerdidaTmpExtra.BorderWidthLeft = 0;
                colPerdidaTmpExtra.BorderWidthRight = 0;
                colPerdidaTmpExtra.BorderWidthBottom = 1;
                colPerdidaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colPerdidaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell colRecuperadaTmpExtra = new PdfPCell(new Phrase(CantidadRecuperada.ToString("N2"), fuenteNormal));
                colRecuperadaTmpExtra.BorderWidthTop = 0;
                colRecuperadaTmpExtra.BorderWidthLeft = 0;
                colRecuperadaTmpExtra.BorderWidthRight = 0;
                colRecuperadaTmpExtra.BorderWidthBottom = 1;
                colRecuperadaTmpExtra.BackgroundColor = new BaseColor(Color.SkyBlue);
                colRecuperadaTmpExtra.HorizontalAlignment = Element.ALIGN_CENTER;

                tablaInventario.AddCell(colNoConceptoTmpExtra);
                tablaInventario.AddCell(colNombreTmpExtra);
                tablaInventario.AddCell(colCodigoTmpExtra);
                tablaInventario.AddCell(colPuntoVentaTmpExtra);
                tablaInventario.AddCell(colStockFisicoTmpExtra);
                tablaInventario.AddCell(colFechaTmpExtra);
                tablaInventario.AddCell(colDiferenciaTmpExtra);
                tablaInventario.AddCell(colPrecioTmpExtra);
                tablaInventario.AddCell(colPerdidaTmpExtra);
                tablaInventario.AddCell(colRecuperadaTmpExtra);
            }

            reporte.Add(titulo);
            reporte.Add(Usuario);
            reporte.Add(subTitulo);
            reporte.Add(tablaInventario);

            //================================
            //=== FIN TABLA DE INVENTARIO  ===
            //================================

            reporte.AddTitle("Reporte Inventario");
            reporte.AddAuthor("PUDVE");
            reporte.Close();
            writer.Close();

            VisualizadorReportes vr = new VisualizadorReportes(rutaArchivo);
            vr.Show();

        }

        private void DGVInventario_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex.Equals(3))
            {
                DGVInventario.Cursor = Cursors.Hand;
            }
        }

        private void DGVInventario_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex != 3)
            {
                DGVInventario.Cursor = Cursors.Default;
            }
        }

        private void btnCaja_Click(object sender, EventArgs e)
        {
            //if (FormPrincipal.userNickName.Equals("OXXOCLARA3") || FormPrincipal.userNickName.Equals("ALEXHIT"))
            //{
            BuscarReporteCajaPorFecha reporteCaja = new BuscarReporteCajaPorFecha();

            reporteCaja.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("Estamos trabajando en este apartado", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void btnReporteVentas_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Para generar reportes de ventas deberá ir \nal apartado Ventas y dar click en el botón \n\"Generar Reporte\".", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            BuscadorReporteClientes reporteClientes = new BuscadorReporteClientes();

            reporteClientes.ShowDialog();
        }

        private void botonRedondo1_Click(object sender, EventArgs e)
        {
            if (opcion1 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }

            if (Application.OpenForms.OfType<FechasReportes>().Count() == 1)
            {
                Application.OpenForms.OfType<FechasReportes>().First().BringToFront();
            }
            else
            {
                var fechas = new FechasReportes();

                fechas.FormClosed += delegate
                {
                    if (botonAceptar)
                    {
                        botonAceptar = false;

                        fechaInicial = fechas.fechaInicial;
                        fechaFinal = fechas.fechaFinal;

                        if (!string.IsNullOrWhiteSpace(fechaInicial))
                        {
                            if (!string.IsNullOrWhiteSpace(fechaFinal))
                            {
                                if (Utilidades.AdobeReaderInstalado())
                                {
                                    //if (cs.validarInformacio(FechasReportes.lugarProcedencia, FechasReportes.idEncontrado))
                                    //{
                                    //    GenerarReportePrecios(FechasReportes.lugarProcedencia, FechasReportes.idEncontrado);
                                    //}
                                    //else
                                    //{
                                    //    MessageBox.Show("No existe infomación para generar el reporte.", "Mensaje de sistema", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    //}
                                    GenerarReportePrecios(FechasReportes.lugarProcedencia, FechasReportes.idEncontrado);
                                }
                                else
                                {
                                    Utilidades.MensajeAdobeReader();
                                }
                            }
                        }
                    }
                };

                fechas.Show();
            }
        }

        private void botonRedondo2_Click(object sender, EventArgs e)
        {
            if (opcion2 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }
            //if (FormPrincipal.userNickName.Equals("OXXOCLARA3") || FormPrincipal.userNickName.Equals("ALEXHIT"))
            //{
            BuscarReporteCajaPorFecha reporteCaja = new BuscarReporteCajaPorFecha();

            reporteCaja.ShowDialog();
            //}
            //else
            //{
            //    MessageBox.Show("Estamos trabajando en este apartado", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        private void botonRedondo3_Click(object sender, EventArgs e)
        {
            if (opcion3 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }
            TipoReportesInventario TRPInventario = new TipoReportesInventario();

            TRPInventario.ShowDialog();

            //BuscadorReporteInventario BRInventario = new BuscadorReporteInventario();
            //BRInventario.ShowDialog();
        }

        private void botonRedondo4_Click(object sender, EventArgs e)
        {
            if (opcion4 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }
            MessageBox.Show("Para generar reportes de ventas deberá ir \nal apartado Ventas y dar click en el botón \n\"Generar Reporte\".", "Mensaje de Sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void botonRedondo5_Click(object sender, EventArgs e)
        {
            if (opcion5 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }
            
            BuscadorReporteClientes reporteClientes = new BuscadorReporteClientes();

            reporteClientes.ShowDialog();
        }

        private void btnMenosVendidos_Click(object sender, EventArgs e)
        {
            if (opcion6 == 0)
            {
                Utilidades.MensajePermiso();
                return;
            }
            RangosReporteProductosMenosVendidos rangoReporte = new RangosReporteProductosMenosVendidos();

            rangoReporte.ShowDialog();
        }

        private void btnRetiroConcepto_Click(object sender, EventArgs e)
        {
            ReportesRetirosConceptosPorMes porMes = new ReportesRetirosConceptosPorMes();
            porMes.ShowDialog();

        }
    }
}
