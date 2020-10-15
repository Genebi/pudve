using Microsoft.Win32;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public partial class ConfiguracionMariaDB : Form
    {
        static string rutaDirectorio = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PUDVE\";
        string direccionURL = "https://sifo.com.mx/updatepudve/";

        BaseDatosMySQL db = new BaseDatosMySQL();
        TablasMySQL td = new TablasMySQL();

        private bool ejecutarShown = true;

        public ConfiguracionMariaDB(bool ejecutarShown = true)
        {
            InitializeComponent();

            this.ejecutarShown = ejecutarShown;
        }

        private void ConfiguracionMariaDB_Load(object sender, EventArgs e)
        {
            var imagen = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\icon\gifs\cargando.gif";

            PBLoading.Load(imagen);
        }

        private async void ConfiguracionMariaDB_Shown(object sender, EventArgs e)
        {
            if (ejecutarShown)
            {
                var respuesta = await Cargando();

                if (respuesta)
                {
                    this.Close();
                }
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

                if (File.Exists(rutaDirectorio + segundoArchivo))
                {
                    // Instala Visual C++ 2013
                    await InstalarComponentes(rutaDirectorio + segundoArchivo);
                }

                if (File.Exists(rutaDirectorio + primerArchivo))
                {
                    await InstalarMariaDB();
                    await PrivilegiosUsuario();
                    await db.buildDataBase();

                    // Se utilizo un hilo en lugar de usar async/await 
                    // por problemas conlos ciclos asincronos
                    Thread crear = new Thread(async () => await td.buildTables());
                    crear.Start();
                }
            }

            return true;
        }

        static Task<int> InstalarMariaDB()
        {
            var tcs = new TaskCompletionSource<int>();

            string rutaMSI = rutaDirectorio + "mariadb-10.5.5-win32.msi";
            string rutaInstalacion = @"C:\Program Files (x86)\PudveBD\";
            string servicio = "PudveBD";
            string argumentos = string.Format("/qn /i \"{0}\" INSTALLDIR=\"{1}\" ADDLOCAL=ALL REMOVE=HeidiSQL ALLUSERS=1 PORT=6666 SERVICENAME=\"{2}\"", rutaMSI, rutaInstalacion, servicio);

            Process proceso = new Process
            {
                StartInfo = {
                    FileName = "msiexec.exe",
                    Arguments = argumentos,
                    Verb = "runas"
                },
                EnableRaisingEvents = true
            };

            proceso.Exited += (sender, args) =>
            {
                tcs.SetResult(proceso.ExitCode);
                proceso.Dispose();
            };

            proceso.Start();

            return tcs.Task;
        }

        static Task<int> InstalarComponentes(string archivo)
        {
            var tcs = new TaskCompletionSource<int>();

            Process proceso = new Process
            {
                StartInfo =
                {
                    Arguments = "/s /v /qn /min",
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    FileName = archivo,
                    UseShellExecute = false
                },
                EnableRaisingEvents = true
            };

            proceso.Exited += (sender, args) =>
            {
                tcs.SetResult(proceso.ExitCode);
                proceso.Dispose();
            };

            proceso.Start();

            return tcs.Task;
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
