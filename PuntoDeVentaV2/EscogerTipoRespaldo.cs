using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    public partial class EscogerTipoRespaldo : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        SaveFileDialog saveFile = new SaveFileDialog();

        public static bool estadoBoton; 
         
        public static int typeBackUp { get; set; }

        public EscogerTipoRespaldo()
        {
            InitializeComponent();
            this.Text = "Escoger tipo de respaldo";
        }

        private void btnGuadar_Click(object sender, EventArgs e)
        {
            RespaldarBaseDatos();
        }

        private void btnRespaldarSU_Click(object sender, EventArgs e)
        {
            RespaldarBaseDatos(true);
        }

        private void RespaldarBaseDatos(bool conUsuario = false)
        {
            DateTime fechaCreacion = DateTime.Now;

            saveFile.FileName = $"{FormPrincipal.userNickName}";
            saveFile.Filter = "SQL (*.sql)|*.sql";
            saveFile.FilterIndex = 1;
            saveFile.RestoreDirectory = true;

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var archivo = saveFile.FileName;
                    var datoConexion = conexionRuta();

                    using (MySqlConnection con = new MySqlConnection(datoConexion))
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            using (MySqlBackup backup = new MySqlBackup(cmd))
                            {
                                cmd.Connection = con;
                                con.Open();

                                if (conUsuario)
                                {
                                    backup.ExportInfo.ExcludeTables = new List<string> { "Usuarios", "basculas" };
                                    backup.ExportInfo.RowsExportMode = RowsDataExportMode.InsertIgnore;
                                }

                                backup.ExportToFile(archivo);
                                con.Close();
                            }
                        }
                    }
                    MessageBox.Show("Información respaldada exitosamente", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            this.Close();
        }

        private string conexionRuta()
        {
            string conexion = string.Empty;

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
            {
                conexion = "datasource=" + Properties.Settings.Default.Hosting + ";port=6666;username=root;password=;database=pudve;";
            }
            else
            {
                conexion = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
            }

            // Important Additional Connection Options
            conexion += "charset=utf8;convertzerodatetime=true;";

            return conexion;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            typeBackUp = 0;

            this.Close();
        }

        private void rbRespaldoEquipo_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void rbRespaldoCorreo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRespaldar.PerformClick();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                btnCancelar.PerformClick();
            }
        }

        private void rbAmbos_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void EscogerTipoRespaldo_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyValue.Equals(Keys.Escape))
            //{
            //    this.Close();
            //}

            if (e.KeyCode.Equals(Keys.Escape))
            {
                this.Close();
            }
        }

        private void EscogerTipoRespaldo_Load(object sender, EventArgs e)
        {
            //var activadoDesactivado = cn.CargarDatos($"SELECT RespaldoAlCerrarSesion FROM configuracion WHERE IDUsuario = {FormPrincipal.userID}");
            //int Estado = Convert.ToInt32(activadoDesactivado.Rows[0]["RespaldoAlCerrarSesion"]);
            //if (Estado == 1)
            //{
            //    rbRespaldarCerrarSesion.Checked = true;
            //    rbNoRespaldar.Checked = false;
            //}
            //else
            //{
            //    rbRespaldarCerrarSesion.Checked = false;
            //    rbNoRespaldar.Checked = true;
            //}
            rbNoRespaldar.Checked = true;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            //if (rbRespaldarCerrarSesion.Checked)
            //{
            //    cn.EjecutarConsulta($"UPDATE Configuracion SET RespaldoAlCerrarSesion = 1 WHERE IDUsuario = {FormPrincipal.userID}");
            //}
            //else
            //{
            //    cn.EjecutarConsulta($"UPDATE Configuracion SET RespaldoAlCerrarSesion = 0 WHERE IDUsuario = {FormPrincipal.userID}");
            //}

            cn.EjecutarConsulta($"UPDATE Configuracion SET RespaldoAlCerrarSesion = 0 WHERE IDUsuario = {FormPrincipal.userID}");

            this.Close();
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            var datos = cn.CargarDatos("SELECT * FROM Productos");

            if (datos.Rows.Count > 0)
            {
                MessageBox.Show("No se puede importar la información debido a que\nactualmente el sistema ya cuenta con información\nregistrada.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Inicializamos los valores por defecto del openFileDialog
                OpenFileDialog buscarArchivoBD = new OpenFileDialog();
                buscarArchivoBD.FileName = string.Empty;
                buscarArchivoBD.Filter = "SQL (*.sql)|*.sql";
                buscarArchivoBD.FilterIndex = 1;
                buscarArchivoBD.RestoreDirectory = true;

                // Si ya selecciona el archivo de la base de datos se le muestra el siguiente mensaje
                if (buscarArchivoBD.ShowDialog() == DialogResult.OK)
                {
                    var mensaje = string.Join(
                        Environment.NewLine,
                        "¿Estás seguro de importar el siguiente archivo",
                        $"{buscarArchivoBD.FileName}?"
                    );

                    var respuesta = MessageBox.Show(mensaje, "Mensaje de confirmación", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                    // Si acepta el mensaje de confirmación realiza el siguiente procedimiento
                    if (respuesta == DialogResult.OK)
                    {
                        // Se guarda la ruta completa junto con el nombre del archivo que se selecciono
                        var rutaArchivo = buscarArchivoBD.FileName;

                        try
                        {
                            string conexion = string.Empty;

                            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.Hosting))
                            {
                                conexion = "datasource=" + Properties.Settings.Default.Hosting + ";port=6666;username=root;password=;database=pudve;";
                            }
                            else
                            {
                                conexion = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";
                            }

                            // Important Additional Connection Options
                            conexion += "charset=utf8;convertzerodatetime=true;";

                            using (MySqlConnection con = new MySqlConnection(conexion))
                            {
                                using (MySqlCommand cmd = new MySqlCommand())
                                {
                                    using (MySqlBackup backup = new MySqlBackup(cmd))
                                    {
                                        cmd.Connection = con;
                                        con.Open();
                                        backup.ImportFromFile(rutaArchivo);
                                        con.Close();
                                    }
                                }
                            }

                            var respuestaImportacion = MessageBox.Show("Importación realizada con éxito, espere un\nmomento los datos serán actualizados.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            if (respuestaImportacion == DialogResult.OK)
                            {
                                var actualizacion = ActualizarTablasConUsuario();

                                if (actualizacion)
                                {
                                    MessageBox.Show("Proceso finalizado correctamente.", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Mensaje del Sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private bool ActualizarTablasConUsuario()
        {
            Dictionary<string, string> tablas = new Dictionary<string, string>();
            tablas.Add("Productos", "IDUsuario");
            tablas.Add("ProductoRelacionadoXML", "IDUsuario");
            tablas.Add("HistorialCompras", "IDUsuario");
            tablas.Add("HistorialModificacionRecordProduct", "IDUsuario");
            tablas.Add("HistorialPrecios", "IDUsuario");
            tablas.Add("MensajesInventario", "IDUsuario");
            tablas.Add("Clientes", "IDUsuario");
            tablas.Add("Empleados", "IDUsuario");
            tablas.Add("Ventas", "IDUsuario");
            tablas.Add("Proveedores", "IDUsuario");
            tablas.Add("DetallesProducto", "IDUsuario");
            tablas.Add("DetalleGeneral", "IDUsuario");
            tablas.Add("DetallesProductoGenerales", "IDUsuario");
            tablas.Add("DetallesVenta", "IDUsuario");
            tablas.Add("Abonos", "IDUsuario");
            tablas.Add("Anticipos", "IDUsuario");
            tablas.Add("appSettings", "IDUsuario");
            tablas.Add("Caja", "IDUsuario");
            tablas.Add("CodigoBarrasGenerado", "IDUsuario");
            tablas.Add("ConceptosDinamicos", "IDUsuario");
            tablas.Add("Configuracion", "IDUsuario");
            tablas.Add("CorreosProducto", "IDUsuario");
            tablas.Add("EmpleadosPermisos", "IDUsuario");
            tablas.Add("Empresas", "ID_Usuarios");
            tablas.Add("Facturas", "id_usuario");
            tablas.Add("FiltroDinamico", "IDUsuario");
            tablas.Add("FiltroProducto", "IDUsuario");
            tablas.Add("FiltrosDinamicosVetanaFiltros", "IDUsuario");
            tablas.Add("RegimenDeUsuarios", "Usuario_ID");
            tablas.Add("RevisarInventario", "IDUsuario");
            tablas.Add("RevisarInventarioReportes", "IDUsuario");
            tablas.Add("TipoClientes", "IDUsuario");
            tablas.Add("Devoluciones", "IDUsuario");
            tablas.Add("basculas", "idUsuario");

            foreach (var tabla in tablas)
            {
                cn.EjecutarConsulta($"UPDATE {tabla.Key} SET {tabla.Value} = {FormPrincipal.userID}");
            }

            return true;
        }

        private void btnImportarExcel_Click(object sender, EventArgs e)
        {
            List<string> columnasyPathFile = new List<string>();
            string pathfile = abrirArchivo();
            if (pathfile!="")
            {
                columnasyPathFile = getExcelFile(pathfile);
                importarExcelProductos ventanaImprotar = new importarExcelProductos(columnasyPathFile, pathfile);
                ventanaImprotar.ShowDialog();
            }
            else
            {
                MessageBox.Show("Se cancelo la operación");
            }
        }

        public static List<string> getExcelFile(string filepath)
        {
            //Claramente yo hise este método desde 0 
            List<string> columnas = new List<string>();

            //Create COM Objects. Create a COM object for everything that is referenced
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbooks xlWorkbookS = xlApp.Workbooks;

            Excel.Workbook xlWorkbook = xlWorkbookS.Open(filepath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            //iterate over the rows and columns and print to the console as it appears in the file
            //excel is not zero based!!
                for (int j = 1; j <= colCount; j++)
                {
                    //write the value to the console
                    if (xlRange.Cells[1, j] != null && xlRange.Cells[1, j].Value2 != null) ;
                    columnas.Add(xlRange.Cells[1, j].Value2.ToString());
                }
            



            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:
            //  never use two dots, all COM objects must be referenced and released individually
            //  ex: [somthing].[something].[something] is bad

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            return columnas;
        }

        private string abrirArchivo()
        {
            string fileName = string.Empty;

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Abrir Archivo";
            dialog.Filter = "Archivos de Excel|*.xls;*.xlsx|Archivos CSV (*.csv)|*.csv";

            if (dialog.ShowDialog().Equals(DialogResult.OK))
            {
                fileName = dialog.FileName;

            }
            return fileName;
        }
    }
}
