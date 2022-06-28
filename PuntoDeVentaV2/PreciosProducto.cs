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
        int mas = 0;
        int numerosPrecios = 1;
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
            TomarNombreCodigoDeBarrasYImagen();
            PreciosConDescuentoOSinDescuento();
        }
        

        private void PreciosConDescuentoOSinDescuento()
        {
            var ConsultaID = cn.CargarDatos(cs.BuscarIDPreductoPorCodigoDeBarras(ConsultaPrecio.CodigoDeBarras));
            string IDProducto = ConsultaID.Rows[0]["ID"].ToString();
            var ConsultaDescuantos = cn.CargarDatos(cs.BuscarDescuentosPorMayoreo(IDProducto));
            int otrosPrecios = 3;
            int contador = 0;
            if (!ConsultaDescuantos.Rows.Count.Equals(0))
            {
                foreach (DataRow item in ConsultaDescuantos.Rows)
                {
                    int precio = Convert.ToInt32(ConsultaDescuantos.Rows[mas]["Precio"]);
                    string rangoinical = ConsultaDescuantos.Rows[mas]["RangoInicial"].ToString();
                    string rangoFinal;

                    if (ConsultaDescuantos.Rows[mas]["RangoFinal"].Equals("N"))
                    {
                        rangoFinal = " En adelante";
                    }
                    else
                    {
                        rangoFinal = $"{ConsultaDescuantos.Rows[mas]["RangoFinal"].ToString()} productos";
                    }
                    Panel panelHijo = new Panel();
                    Label lblprecio = new Label();
                    lblprecio.TextAlign = ContentAlignment.MiddleCenter;
                    lblprecio.Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                    lblprecio.Width = 190;
                    lblprecio.Height = 130;
                    lblprecio.Text = $"Precio {numerosPrecios} \n{precio.ToString("C2")} \n de {rangoinical} - {rangoFinal}";
                    lblprecio.BorderStyle = BorderStyle.FixedSingle;
                    flowLayoutPanel1.Controls.Add(lblprecio);
                    mas++;
                    numerosPrecios++;
                    contador++;
                    counter += 3;
                }
            }
            else
            {
                var consutaPrecio = cn.CargarDatos(cs.BuscarPrecioPorIDdelProducto(IDProducto));
                int precio =  Convert.ToInt32(consutaPrecio.Rows[0]["Precio"]);
                flowLayoutPanel1.Visible = false;
                lblPrecioSinDescuentos.Visible = true;
                lblPrecioSinDescuentos.Text = precio.ToString("C2");
            }
        }

        private void TomarNombreCodigoDeBarrasYImagen()
        {
            DataTable producto;
            using (producto = cn.CargarDatos(cs.BuscarProductoPorCodigoDeBarras(ConsultaPrecio.CodigoDeBarras)))
            {
                foreach (DataRow item in producto.Rows)
                {
                    lblNombre.Text = item["Nombre"].ToString();
                    decimal precio = Convert.ToDecimal(item["Precio"]);

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
                    string directorfinal = DirectorioImagen + nombrefinal + ".jpg";

                    using (DataTable consultalogo = cn.CargarDatos(cs.buscarNombreLogoTipo(FormPrincipal.userID)))
                    {
                        string NombreLogo = consultalogo.Rows[0]["LogoTipo"].ToString();

                        if (File.Exists(directorfinal))
                        {
                            pictureBox1.Image = Image.FromFile(directorfinal);
                        }
                        else if (!NombreLogo.Equals(""))
                        {
                            pictureBox1.Image = Image.FromFile(saveDirectoryImg + NombreLogo);
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

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
