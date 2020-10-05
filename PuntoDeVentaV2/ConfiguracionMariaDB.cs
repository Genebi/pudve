using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class ConfiguracionMariaDB : Form
    {
        string rutaDirectorio = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\";
        string direccionURL = "https://sifo.com.mx/updatepudve/";

        BaseDatosMySQL db = new BaseDatosMySQL();
        TablasMySQL td = new TablasMySQL();

        public ConfiguracionMariaDB()
        {
            InitializeComponent();
        }

        private async void ConfiguracionMariaDB_Load(object sender, EventArgs e)
        {
            var imagen = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\gifs\cargando.gif";

            PBLoading.Load(imagen);

            var respuesta = await Cargando();

            if (respuesta)
            {
                MessageBox.Show("Test");
            }
        }


        private async Task<bool> Cargando()
        {
            var primerArchivo = "mariadb-10.5.5-win32.msi";
            var segundoArchivo = string.Empty;

            if (Environment.Is64BitOperatingSystem)
            {
                segundoArchivo = "vc2013redist_x64.exe";
            }
            else
            {
                segundoArchivo = "vc2013redist_x86.exe";
            }


            if (!Directory.Exists(@"C:\Program Files (x86)\PudveBD\"))
            {
                var archivos = new string[] { primerArchivo, segundoArchivo };

                foreach (var archivo in archivos)
                {
                    using (WebClient cliente = new WebClient())
                    {
                        await cliente.DownloadFileTaskAsync(direccionURL + archivo, rutaDirectorio + archivo);
                    }
                }

                if (File.Exists(rutaDirectorio + primerArchivo))
                {
                    InstalarMariaDB();
                    await PrivilegiosUsuario();
                    await db.buildDataBase();
                    await td.buildTables();
                }

                if (File.Exists(rutaDirectorio + segundoArchivo))
                {
                    InstalarComponentes(rutaDirectorio + segundoArchivo);
                }
            }

            return true;
        }

        private void InstalarMariaDB()
        {
            try
            {
                string rutaMSI = rutaDirectorio + "mariadb-10.5.5-win32.msi";
                string rutaInstalacion = @"C:\Program Files (x86)\PudveBD\";
                string servicio = "PudveBD";
                string argumentos = string.Format("/qn /i \"{0}\" INSTALLDIR=\"{1}\" ADDLOCAL=ALL REMOVE=HeidiSQL ALLUSERS=1 PORT=6666 SERVICENAME=\"{2}\"", rutaMSI, rutaInstalacion, servicio);

                Process proceso = new Process();
                proceso.StartInfo.FileName = "msiexec.exe";
                proceso.StartInfo.Arguments = argumentos;
                proceso.StartInfo.Verb = "runas";
                proceso.Start();
                proceso.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InstalarComponentes(string archivo)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.Arguments = "/s /v /qn /min";
                psi.CreateNoWindow = true;
                psi.WindowStyle = ProcessWindowStyle.Hidden;
                psi.FileName = archivo;
                psi.UseShellExecute = false;
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task PrivilegiosUsuario()
        {
            MySqlConnection conexion = new MySqlConnection();

            conexion.ConnectionString = "datasource=127.0.0.1;port=6666;username=root;password=;database=mysql;";

            try
            {
                await conexion.OpenAsync();
                MySqlCommand crear = conexion.CreateCommand();

                var usuario = "root";
                var primerHost = "localhost";
                var segundoHost = "%";

                //Consulta de MySQL
                var consulta = string.Format("GRANT ALL PRIVILEGES ON *.* TO '{0}'@'{1}' WITH GRANT OPTION;", usuario, primerHost);
                crear.CommandText = consulta;
                await crear.ExecuteNonQueryAsync();

                consulta = string.Format("CREATE USER '{0}'@'{1}' IDENTIFIED BY '';", usuario, segundoHost);
                crear.CommandText = consulta;
                await crear.ExecuteNonQueryAsync();

                consulta = string.Format("GRANT ALL PRIVILEGES ON *.* TO '{0}'@'{1}' WITH GRANT OPTION;", usuario, segundoHost);
                crear.CommandText = consulta;
                await crear.ExecuteNonQueryAsync();

                //Cerramos la conexion de MySQL
                conexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
