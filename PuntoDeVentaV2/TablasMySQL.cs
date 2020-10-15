using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuntoDeVentaV2
{
    class TablasMySQL
    {
        Conexion cn = new Conexion();

        List<string> tables = new List<string>();

        string connectionString = string.Empty;

        public TablasMySQL()
        {

        }

        private async Task CrearTablas()
        {
            string conexion = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";

            string rutaArchivo = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\BD\Tablas.sql";

            using (MySqlConnection con = new MySqlConnection(conexion))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    using (MySqlBackup backup = new MySqlBackup(cmd))
                    {
                        cmd.Connection = con;
                        await con.OpenAsync();
                        backup.ImportFromFile(rutaArchivo);
                        con.Close();
                    }
                }
            }
        }

        private async Task InsertarDatos()
        {
            string[] archivos = new string[] { "RegimenFiscal", "CatalogoMonedas", "UnidadesMedida", "ClavesProducto" };

            string conexion = "datasource=127.0.0.1;port=6666;username=root;password=;database=pudve;";

            foreach (var archivo in archivos)
            {
                string rutaArchivo = Properties.Settings.Default.rutaDirectorio + $@"\PUDVE\BD\{archivo}.sql";

                using (MySqlConnection con = new MySqlConnection(conexion))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        using (MySqlBackup backup = new MySqlBackup(cmd))
                        {
                            cmd.Connection = con;
                            await con.OpenAsync();
                            backup.ImportFromFile(rutaArchivo);
                            con.Close();
                        }
                    }
                }
            }
        }

        public async Task buildTables(bool insertar = true)
        {
            connectionString = cn.getStringConnection() + "database=pudve;";

            try
            {
                await CrearTablas();

                if (insertar)
                    await InsertarDatos();
            }
            catch (MySqlException mysqlex)
            {
                System.Windows.Forms.MessageBox.Show("Excepción de MySQL al Crear la Base de datos: " + mysqlex.Message.ToString());
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Excepción general: " + ex.Message.ToString());
            }
        }
    }
}
