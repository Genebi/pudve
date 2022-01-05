using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PuntoDeVentaV2
{
    public class RespadoBaseDatos
    {
        MetodosBusquedas mb = new MetodosBusquedas();
        Conexion cn = new Conexion();

        SaveFileDialog saveFile = new SaveFileDialog();


        public void crearsaveFile(int tipoRespaldo = 2)
        {//Crear un SaveFileDialog con codigo para respaldar la base de datos
            varificarCarpetaRespaldos();

            if (tipoRespaldo != 0)
            {
                DateTime fechaCreacion = DateTime.Now;

                //Stream steam;
                 
                saveFile.FileName = $"{FormPrincipal.userNickName}";
                saveFile.Filter = "SQL (*.sql)|*.sql";
                saveFile.FilterIndex = 1;
                saveFile.RestoreDirectory = true;


                //if (tipoRespaldo.Equals(2)) //Respaldar bd al correo
                //{
                //    try
                //    {
                //        var archivo = $@"C:\Archivos PUDVE\Respaldos\Terminados\{FormPrincipal.userNickName}_{fechaCreacion.ToString("yyyyMMddHHmmss")}.sql";

                //        var datoConexion = conexionRuta();

                //        using (MySqlConnection con = new MySqlConnection(datoConexion))
                //        {
                //            using (MySqlCommand cmd = new MySqlCommand())
                //            {
                //                using (MySqlBackup backup = new MySqlBackup(cmd))
                //                {
                //                    cmd.Connection = con;
                //                    con.Open();
                //                    backup.ExportToFile(archivo);
                //                    con.Close();

                //                     Thread hilo = new Thread(() => sendEmail(archivo, 2));
                //                    hilo.Start();
                //                    //steam.Close();   
                //                }
                //            }
                //        }
                //        //Thread.Sleep(50000);
                //        MessageBox.Show("Información enviada al correo exitosamente", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.ToString(), "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    }
                //}
                //else 
                //{
                    //if (saveFile.ShowDialog() == DialogResult.OK)
                    //{
                        //if ((steam = saveFile.OpenFile()) != null)
                        //{
                            try
                            {
                    //var archivo = saveFile.FileName;
                    var directorio = $@"C:\Archivos PUDVE\Respaldos\RespaldoCerrarSesion\";
                    var archivos = Directory.GetFiles(directorio,"*.sql");

                    if (!archivos.Count().Equals(0))
                    {
                        if (archivos.Count() > 100)
                        {
                            var listaArchivos = archivos.ToList();
                            listaArchivos.Sort();
                            //listaArchivos.Reverse(); //Ordenar del mas nuevo al mas antiguo 
                            //for (int i = 0; i < listaArchivos.Count-1; i++)
                            //{
                            //    File.Delete(listaArchivos[i]);
                            //}
                            File.Delete(listaArchivos[0]);
                        }
                    }


                    var fechaResp = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
                    var archivo = $@"{directorio}{FormPrincipal.userNickName}_{fechaResp}.sql";

                        var datoConexion = conexionRuta();

                            using (MySqlConnection con = new MySqlConnection(datoConexion)) 
                            {
                                using (MySqlCommand cmd = new MySqlCommand())
                                {
                                    using (MySqlBackup backup = new MySqlBackup(cmd))
                                    {
                                        cmd.Connection = con;
                                        con.Open();
                                        backup.ExportToFile(archivo);
                                        con.Close();

                                        if (tipoRespaldo.Equals(3))
                                        {
                                            Thread hilo = new Thread(() => sendEmail(archivo, 1));
                                            hilo.Start();
                                            
                                        }
                                        //steam.Close();  
                                    }
                                }
                            }
                            //Thread.Sleep(50000);
                            MessageBox.Show("Información respaldada exitosamente", "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                        catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "Mensaje del sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //}
                //}
                //}
            }
        }

        private void varificarCarpetaRespaldos()
        {//Crea la carpeta Respaldos si no existe 
            string directorio = @"C:\Archivos PUDVE\Respaldos";

            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }

            string directorioZipTerminados = @"C:\Archivos PUDVE\Respaldos\Terminados";

            if (!Directory.Exists(directorioZipTerminados))
            {
                Directory.CreateDirectory(directorioZipTerminados);
            }

            string directorioZip = @"C:\Archivos PUDVE\Respaldos\Terminados\Zip";

            if (!Directory.Exists(directorioZip))
            {
                Directory.CreateDirectory(directorioZip);
            }

            string directorioRespaldoDBSql = $@"C:\Archivos PUDVE\Respaldos\RespaldoCerrarSesion\";
            if (!Directory.Exists(directorioRespaldoDBSql))
            {
                Directory.CreateDirectory(directorioRespaldoDBSql);
            }

            //File.Delete(directorioZip);
        }

        private void sendEmail(string rutaGuardado, int tipoRespaldo = 0)
        {
            //varificarCarpetaRespaldos();
            DateTime fechaCreacion = DateTime.Now;


            //Variables con las rutas para mover los archivos
            var rutaDestino = $@"C:\Archivos PUDVE\Respaldos\Terminados\Zip\{FormPrincipal.userNickName}_{fechaCreacion.ToString("yyyyMMddHHmmss")}.sql";
            var rutaSalida = rutaGuardado;
            var segundaCarpeta = $@"C:\Archivos PUDVE\Respaldos\Terminados\{FormPrincipal.userNickName}_{fechaCreacion.ToString("yyyyMMddHHmmss")}.sql";

            var pathCorreo = $@"C:\Archivos PUDVE\Respaldos\{FormPrincipal.userNickName}_{ fechaCreacion.ToString("yyyyMMddHHmmss")}.zip";

            //Copiar la bd para poder mandarla por correo
            File.Copy(rutaSalida, rutaDestino);

            //Comprimir en archivo Sql para poder enviarlo por correo
            ZipFile.CreateFromDirectory(@"C:\Archivos PUDVE\Respaldos\Terminados\Zip", pathCorreo);

            if (!tipoRespaldo.Equals(2))
            {
                //Mover archivos para poder comprimir futuros respaldos
                File.Move(rutaDestino, segundaCarpeta);//Mover de carpeta
            }
            else
            {
                File.Delete(rutaGuardado);
            }

            //Enviar por correo la base de datos
            var correoUsuario = mb.correoUsuario();
            Utilidades.EnviarCorreoRespaldo(correoUsuario, pathCorreo);
        }


        public bool validarMandarRespaldoCorreo()
        { 
            var result = false;

            var query = cn.CargarDatos($"SELECT CorreoRespaldo FROM Configuracion WHERE IDUsuario = {FormPrincipal.userID}");

            if (!query.Rows.Count.Equals(0))
            {
                result = Convert.ToBoolean(query.Rows[0]["CorreoRespaldo"].ToString());
            }

            return result;
        }

        public bool RespaldarDBAlCerrarSesion()
        {
            var result = false;

            var query = cn.CargarDatos($"SELECT RespaldoAlCerrarSesion FROM Configuracion WHERE IDUsuario = {FormPrincipal.userID}");

            if (!query.Rows.Count.Equals(0))
            {
                if (query.Rows[0]["RespaldoAlCerrarSesion"].Equals(1))
                {
                    result = true;
                }
            }

            return result;
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
    }
}
