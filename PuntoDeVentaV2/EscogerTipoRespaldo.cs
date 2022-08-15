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
using System.Windows.Forms;

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
            DateTime fechaCreacion = DateTime.Now;

            //Stream steam;

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
                                //backup.ExportInfo.ExcludeTables = new List<string> { "Usuarios" };
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
    }
}
