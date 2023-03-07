using MySql.Data.MySqlClient;
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
    public partial class WebUploader : Form
    {
        private FormPrincipal mainForm = null;
        string title=string.Empty;
        Conexion cn = new Conexion();
        ConexionAPPWEB con = new ConexionAPPWEB();

        public WebUploader(bool topmost=true, Form callingForm=null)
        {
            InitializeComponent();
            this.TopMost = topmost;
            this.ShowInTaskbar = false;
            if (callingForm!=null)
            {
                mainForm = callingForm as FormPrincipal;
            }

            if (!topmost)
            {
                this.Opacity = 0;
            }

        }

        private void RespaldarBaseDatos()
        {

            var archivo = @"C:\Archivos PUDVE\tempBackup.sql";
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
                    }
                }
            }
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

        public static void SplitFile(string inputFile, int chunkSize, string path)
        {
            const int BUFFER_SIZE = 20 * 1024;
            byte[] buffer = new byte[BUFFER_SIZE];

            using (Stream input = System.IO.File.OpenRead(inputFile))
            {
                int index = 0;
                while (input.Position < input.Length)
                {
                    using (Stream output = System.IO.File.Create(path + "\\" + index + ".sifo"))
                    {
                        int remaining = chunkSize, bytesRead;
                        while (remaining > 0 && (bytesRead = input.Read(buffer, 0,
                                Math.Min(remaining, BUFFER_SIZE))) > 0)
                        {
                            output.Write(buffer, 0, bytesRead);
                            remaining -= bytesRead;
                        }
                    }
                    index++;
                    Thread.Sleep(500); // experimental; perhaps try it
                }
            }
        }

        private void sqlTxt(DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers    
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write("|");
                }
            }
            sw.Write("^");
            foreach (DataRow dr in dtDataTable.Rows)
            {
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write("~");
                    }
                }
                sw.Write("^");
            }
            sw.Close();


        }

        public async Task bulkInsertAsync(string tablename)
        {
            string connStr = "server=74.208.135.60;user=app;database=pudve;port=3306;password=12Steroids12;AllowLoadLocalInfile=true;";
            MySqlConnection conn = new MySqlConnection(connStr);

            MySqlBulkLoader bl = new MySqlBulkLoader(conn);
            bl.Local = true;

            bl.TableName = tablename;


            bl.FileName = @"C:\Archivos PUDVE\export.txt";
            bl.NumberOfLinesToSkip = 1;
            switch (tablename)
            {
                case "Respaldos":
                    bl.Columns.AddRange(new List<string>() { "IDCliente", "FECHA", "Datos" });
                    bl.FieldTerminator = "~";
                    bl.LineTerminator = "^";
                    break;
                case "mirrorproductosdatos":
                    bl.Columns.AddRange(new List<string>() { "IDregistro", "Nombre", "Stock", "Precio", "Codigo" });
                    bl.FieldTerminator = "+";
                    bl.LineTerminator = "\n";
                    break;
                default:
                    break;
            }

            bl.Timeout = 50000;
            try
            {
                conn.Open();
                int count = bl.Load();

                conn.Close();
            }
            catch (Exception ex)
            {
                return;
            }
            if (System.IO.File.Exists(@"C:\Archivos PUDVE\export.txt"))
            {
                System.IO.File.Delete(@"C:\Archivos PUDVE\export.txt");
            }
        }

        private void WebUploader_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            try
            {
                enviarRespaldo();
            }
            catch (Exception)
            {
                //Error de conexion
                this.Close();
            }
        }

        private async void enviarRespaldo()
        {
            await Task.Run(() => DoThis(PBProgreso, mainForm,this));
        }

        private void WebUploader_Load(object sender, EventArgs e)
        {
            title = mainForm.Text;
            mainForm.Text = $"{title}| ▁▁▁▁▁▁▁▁▁▁ Actualizando...";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cn.EjecutarConsulta($"DELETE FROM WebRespaldosBuilder WHERE IDCliente ='{FormPrincipal.userNickName.Split('@')[0]}'");
            mainForm.Text = title;
            this.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            cn.EjecutarConsulta($"DELETE FROM WebRespaldosBuilder WHERE IDCliente ='{FormPrincipal.userNickName.Split('@')[0]}'");
            this.Close();
        }

        private void DoThis(ProgressBar PBProgreso, FormPrincipal mainForm, WebUploader esto)
        {
            try
            {
                //Invoke(new Action(() =>
                //{
                //    PBProgreso.Value = 10;
                //    mainForm.Text = $"{title}| █▁▁▁▁▁▁▁▁▁ Creando respaldo... ";
                //}));
                //cn.EjecutarConsulta($"DELETE FROM WebRespaldosBuilder WHERE IDCliente ='{FormPrincipal.userNickName.Split('@')[0]}'");
                //RespaldarBaseDatos();


                //Invoke(new Action(() =>
                //{
                //    PBProgreso.Value = 20;
                //    mainForm.Text = $"{title}| ██▁▁▁▁▁▁▁▁ Preparando respaldo...";

                //}));
                //string[] Oldfiles = System.IO.Directory.GetFiles(@"C:\Archivos PUDVE\", "*.sifo");
                //foreach (string file in Oldfiles)
                //{
                //    System.IO.File.Delete(file);
                //}
                //Invoke(new Action(() =>
                //{
                //    PBProgreso.Value = 25;

                //}));

                //SplitFile(@"C:\Archivos PUDVE\tempBackup.sql", 30485760, @"C:\Archivos PUDVE\");

                //DateTime monosas = DateTime.Now;
                //string[] files = System.IO.Directory.GetFiles(@"C:\Archivos PUDVE\", "*.sifo");

                //Invoke(new Action(() =>
                //{
                //    PBProgreso.Value = 30;
                //    mainForm.Text = $"{title}| ███▁▁▁▁▁▁▁ Dando formato a los datos...";

                //}));
                //foreach (string file in files)
                //{
                //    Invoke(new Action(() =>
                //    {
                //        PBProgreso.Value++;
                //    }));

                //    StreamReader reader = new StreamReader(file);
                //    cn.insertarUnPincheTextoAcaTremendoAaaaaa(reader.ReadToEnd(), monosas);
                //}
                //System.IO.File.Delete(@"C:\Archivos PUDVE\tempBackup.sql");
                //ConexionAPPWEB con = new ConexionAPPWEB();

                //Invoke(new Action(() =>
                //{
                //    PBProgreso.Value = 55;
                //    mainForm.Text = $"{title}| ██████▁▁▁▁ Conectando al servidor de Sifo.com.mx ...";

                //}));
                //sqlTxt(cn.CargarDatos($"SELECT IDCliente,Fecha,Datos FROM WebRespaldosBuilder WHERE IDCliente ='{FormPrincipal.userNickName.Split('@')[0]}'"), @"C:\Archivos PUDVE\export.txt");

                //Invoke(new Action(() =>
                //{
                //    PBProgreso.Value = 70;
                //    mainForm.Text = $"{title}| ███████▁▁▁ Enviando datos a la nube...";

                //}));
                //bulkInsertAsync("Respaldos");


                //Invoke(new Action(() =>
                //{
                //    PBProgreso.Value = 90;
                //    mainForm.Text = $"{title}| █████████▁ Finalizando...";

                //}));

                using (DataTable dt = con.CargarDatos($"SELECT DISTINCT(Fecha) FROM Respaldos WHERE IDCliente = '{FormPrincipal.userNickName.Split('@')[0]}' ORDER BY Fecha DESC"))
                {
                    if (dt.Rows.Count > 4)
                    {
                        string consulta = $"DELETE FROM Respaldos WHERE IDCliente = '{FormPrincipal.userNickName.Split('@')[0]}' AND fecha < '{DateTime.Parse(dt.Rows[3]["fecha"].ToString()).ToString("yyyy-MM-dd HH:mm:ss")}'";
                        con.EjecutarConsulta(consulta);
                    }
                }
                cn.EjecutarConsulta($"DELETE FROM WebRespaldosBuilder WHERE IDCliente ='{FormPrincipal.userNickName.Split('@')[0]}'");

                Invoke(new Action(() =>
                {
                    PBProgreso.Value = 100;
                    mainForm.Text = $"{title}";

                }));
            }
            catch (Exception)
            {
                //Error de conexion
            }
            Invoke(new Action(() =>
            {
                if (esto.TopMost)
                {
                    Environment.Exit(0);
                }
                else
                {
                    esto.Close();
                }

            }));
            
        }

        private void WebUploader_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
