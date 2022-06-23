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
    public partial class PreciosProducto : Form
    {
        Conexion cn = new Conexion();
        Consultas cs = new Consultas();
        private int counter = 5;
        string saveDirectoryImg = string.Empty;
        public PreciosProducto()
        {
            InitializeComponent();
            timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; // 1 second
            timer1.Start();
            lblTiempo.Text = counter.ToString();
            
        }

        private void PreciosProducto_Load(object sender, EventArgs e)
        {
            lblCodigoDeBarras.Text = ConsultaPrecio.CodigoDeBarras;
            DataTable producto;
            using (producto = cn.CargarDatos(cs.BuscarProductoPorCodigoDeBarras(ConsultaPrecio.CodigoDeBarras)))
            {
                foreach (DataRow item in producto.Rows)
                {
                    lblNombre.Text = item["Nombre"].ToString();
                    decimal precio =Convert.ToDecimal(item["Precio"]);
                    lblPrecio.Text = precio.ToString("C2");

                    var servidor = Properties.Settings.Default.Hosting;
                    string DirectorioImagen;
                    if (!string.IsNullOrWhiteSpace(servidor))
                    {

                        saveDirectoryImg = $@"\\{servidor}\Archivos PUDVE\MisDatos\Usuarios\";
                        DirectorioImagen = $@"\\{servidor}\pudve\Productos\";

                    }
                    else
                    {
                        DirectorioImagen = Properties.Settings.Default.rutaDirectorio + @"\PUDVE\Productos\";
                    }
                    string nombrIemagen = item["Nombre"].ToString();
                    var nombrefinal = nombrIemagen.Replace('/', '_').Replace('\\', '_').Replace(':', '_').Replace('*', '_').Replace('?', '_').Replace('\"', '_').Replace('<', '_').Replace('>', '_').Replace('|', '_').Replace('-', '_').Replace(' ', '_');
                    string directorfinal = DirectorioImagen + nombrefinal+".jpg";

                    using (DataTable consultalogo = cn.CargarDatos(cs.buscarNombreLogoTipo(FormPrincipal.userID)))
                    {
                        string NombreLogo = consultalogo.Rows[0]["LogoTipo"].ToString();

                        if (File.Exists(directorfinal))
                        {
                            pictureBox1.Image = Image.FromFile(directorfinal);
                        }
                        else if (!NombreLogo.Equals(""))
                        {
                            pictureBox1.Image = Image.FromFile(saveDirectoryImg+NombreLogo);
                        }
                        else
                        {
                            pictureBox1.Visible = false;
                            pictureBox2.Visible = true;
                        }
                    }
                  

                }
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            counter--;
            if (counter == 0)
            {
                timer1.Stop();
                this.Close();
            }  
            lblTiempo.Text = counter.ToString();
        }
    }
}
